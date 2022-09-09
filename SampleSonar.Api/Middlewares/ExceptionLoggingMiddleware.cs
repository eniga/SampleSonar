using Newtonsoft.Json;
using SampleSonar.Data.Models.Responses;
using System.Net;

namespace SampleSonar.Api.Middlewares
{
    public class ExceptionLoggingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionLoggingMiddleware> logger;
        private readonly IWebHostEnvironment env;

        public ExceptionLoggingMiddleware(ILogger<ExceptionLoggingMiddleware> logger, IWebHostEnvironment env)
        {
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                await WriteExceptionREsponseAsync(context, ex);
            }
        }

        private Task WriteExceptionREsponseAsync(HttpContext context, Exception exception)
        {
            // set content type to json
            context.Response.ContentType = "application/json";

            // set http status code to error 500
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string msg = env.IsDevelopment() ? exception.Message : "Request failed to process";
            var errorResponse = new GenericResponse() { Code = "96", Message = $"{msg} (Kindly try again or contact support)", Success = false };

            // serialize object to json
            string jsonMsg = JsonConvert.SerializeObject(errorResponse);

            return context.Response.WriteAsync(jsonMsg);
        }
    }
}
