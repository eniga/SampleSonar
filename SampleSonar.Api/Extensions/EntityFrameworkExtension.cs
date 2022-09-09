using SampleSonar.Data.Extensions;

namespace SampleSonar.Api.Extensions
{
    public static class EntityFrameworkExtension
    {
        public static void UseDBAutoMigration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            using (var context = serviceScope.ServiceProvider.GetService<BaseDbContext>())
            {
                if (!env.IsDevelopment())
                    context.Database.EnsureCreated();
            }
        }
    }
}
