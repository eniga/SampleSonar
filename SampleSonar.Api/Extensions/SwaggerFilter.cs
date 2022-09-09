using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SampleSonar.Api.Extensions
{
    public class RemoveVersionFromParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters.Count > 0)
            {
                OpenApiParameter? versionParameter = operation.Parameters.Single(p => p.Name == "version");
                operation.Parameters.Remove(versionParameter);
            }
        }
    }

    public class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            OpenApiPaths? paths = swaggerDoc.Paths;
            swaggerDoc.Paths = new OpenApiPaths();

            foreach (KeyValuePair<string, OpenApiPathItem> path in paths)
            {
                string? key = path.Key.Replace("v{version}", swaggerDoc.Info.Version);
                OpenApiPathItem? value = path.Value;
                swaggerDoc.Paths.Add(key, value);
            }
        }
    }
}
