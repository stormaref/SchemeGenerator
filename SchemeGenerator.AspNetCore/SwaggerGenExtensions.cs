using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SchemeGenerator.AspNetCore;

/// <summary>
/// Swashbuckle configuration helpers for SchemeGenerator.
/// </summary>
public static class SwaggerGenExtensions
{
    /// <summary>
    /// Adds <see cref="SchemeGeneratorSchemaFilter"/> to populate schema examples.
    /// Requires <see cref="ISchemeGenerator"/> to be registered (e.g. via AddSchemeGenerator).
    /// </summary>
    public static SwaggerGenOptions AddSchemeGeneratorExamples(this SwaggerGenOptions options)
    {
        options.SchemaFilter<SchemeGeneratorSchemaFilter>();
        return options;
    }
}
