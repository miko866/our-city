using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Server.Config.SwaggerConfigs;

public class AcceptLanguageOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation == null)
        {
            throw new Exception("Swagger Invalid operation");
        }

        operation.Parameters.Add(
            new OpenApiParameter
            {
                In = ParameterLocation.Header,
                Name = "accept-language",
                Description = "Pass the locale here: examples like => en, en-gb, de, fr, sk",
                Schema = new OpenApiSchema { Type = "String" }
            }
        );
    }
}
