using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Nodes;

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
    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type is null)
        {
            return;
        }

        try
        {
            if (schema is not OpenApiSchema concreteSchema)
                return;
            var json = _generator.GetDefaultJson(context.Type);
            concreteSchema.Example = JsonNode.Parse(json);
        }
        catch (Exception)
        {
            // Leave schema unchanged when generation or parsing fails.
        }
    }
}
