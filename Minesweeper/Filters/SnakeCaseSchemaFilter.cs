using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using System.Linq;


namespace Minesweeper.Filters
{
    // Фильтр для преобразования стиля CamelCase в SnakeCase
    public class SnakeCaseSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null)
                return;

            var newProperties = schema.Properties
                .ToDictionary(
                    entry => ToSnakeCase(entry.Key),
                    entry => entry.Value
                );

            schema.Properties = newProperties;
        }

        private string ToSnakeCase(string str)
        {
            return string.Concat(
                str.Select((x, i) => i > 0 && char.IsUpper(x)
                    ? "_" + x.ToString().ToLower()
                    : x.ToString().ToLower())
            );
        }
    }
}
