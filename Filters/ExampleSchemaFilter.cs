using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using SQLCRUD.Models;

namespace SQLCRUD.Filters
{
    public class ExampleSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(Product))
            {
                schema.Example = new Microsoft.OpenApi.Any.OpenApiObject
                {
                    ["id"] = new Microsoft.OpenApi.Any.OpenApiInteger(1),
                    ["name"] = new Microsoft.OpenApi.Any.OpenApiString("Sample Product"),
                    ["description"] = new Microsoft.OpenApi.Any.OpenApiString("This is a sample product description"),
                    ["price"] = new Microsoft.OpenApi.Any.OpenApiDouble(99.99),
                    ["category"] = new Microsoft.OpenApi.Any.OpenApiString("Electronics"),
                    ["stockQuantity"] = new Microsoft.OpenApi.Any.OpenApiInteger(50),
                    ["dateCreated"] = new Microsoft.OpenApi.Any.OpenApiString("2024-01-15T10:30:00Z"),
                    ["isActive"] = new Microsoft.OpenApi.Any.OpenApiBoolean(true)
                };
            }
        }
    }
}
