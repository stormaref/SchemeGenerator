using Microsoft.Extensions.DependencyInjection;

namespace SchemeGenerator.AspNetCore;

/// <summary>
/// Registers SchemeGenerator and Swashbuckle schema filter services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers <see cref="ISchemeGenerator"/> and <see cref="SchemeGeneratorSchemaFilter"/> for Swashbuckle.
    /// </summary>
    public static IServiceCollection AddSchemeGeneratorOpenApi(
        this IServiceCollection services,
        Action<SchemeGeneratorOptions>? configure = null)
    {
        services.AddSchemeGenerator(configure);
        services.AddSingleton<SchemeGeneratorSchemaFilter>();
        return services;
    }
}
