using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SchemeGenerator.AspNetCore;

/// <summary>
/// Sets OpenAPI schema examples from SchemeGenerator defaults.
/// </summary>
public sealed class SchemeGeneratorSchemaFilter : ISchemaFilter
{
    private readonly ISchemeGenerator _generator;

    /// <summary>
    /// Creates a filter using the given generator.
    /// </summary>
    public SchemeGeneratorSchemaFilter(ISchemeGenerator generator)
    {
        _generator = generator;
    }

    /// <inheritdoc />
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type is null)
        {
            return;
        }

        try
        {
            var json = _generator.GetDefaultJson(context.Type);
            schema.Example = OpenApiAnyFactory.CreateFromJson(json);
        }
        catch (Exception)
        {
            // Leave schema unchanged when generation or parsing fails.
        }
    }
}
