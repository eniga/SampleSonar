using MicroOrm.Dapper.Repositories.SqlGenerator;
using MicroOrm.Dapper.Repositories;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SampleSonar.Api.Extensions;
using SampleSonar.Data.Extensions;
using Serilog;
using System.Data;
using SampleSonar.Core.Interfaces;
using SampleSonar.Core.Repositories;
using SampleSonar.Api.Middlewares;
using SampleSonar.Core;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

var env = builder.Environment.EnvironmentName;

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

try
{
    // Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddHealthChecks();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Sample Sonar Api",
            Version = "v1",
            Description = "Sample project to display use of sonar cloud in .Net"
        });

        c.ResolveConflictingActions(a => a.First());
        c.OperationFilter<RemoveVersionFromParameter>();
        c.DocumentFilter<ReplaceVersionWithExactValueInPath>();
    });

    // Updatig the middleware to use versioning
    builder.Services.AddApiVersioning(options =>
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = Microsoft.AspNetCore.Mvc.ApiVersion.Default;
        options.ReportApiVersions = true;

        // Add url type versioning
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    });

    // Add support for CORS
    builder.Services.AddCors();

    // Inject config parameters
    builder.Services.AddOptions();

    // Register Entity Framework dependencies
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(connectionString));

    // Register Dapper Dependencies
    builder.Services.AddTransient<IDbConnection>(prov => new SqlConnection(prov.GetService<IConfiguration>().GetConnectionString("DefaultConnection")));
    builder.Services.AddTransient(typeof(ISqlGenerator<>), typeof(MySqlGenerator<>));
    builder.Services.AddTransient(typeof(DapperRepository<>));

    // Register Utils
    builder.Services.AddAutoMapper(typeof(AutoMapping));
    builder.Services.AddTransient<ExceptionLoggingMiddleware>();

    // Register Dependency services
    builder.Services.AddTransient<IUserRepository, UserRepository>();

    builder.Services.AddMediatR(typeof(MediatrEntrypoint).Assembly);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample Sonar Api");
    });

    app.UseRouting();

    app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());

    app.UseMiddleware<ExceptionLoggingMiddleware>();

    // Enable use of authentication header
    app.UseAuthentication();

    // Enable use of authorization header
    app.UseAuthorization();

    app.MapControllers();
    app.MapHealthChecks("/health");

    Log.Information("Application starting up");
    app.Run();
}
catch (Exception ex)
{
    // Log when the application fails to start
    Log.Fatal(ex, "The application failed to start correctly");
}
finally
{
    Log.CloseAndFlush();
}