using Microsoft.Extensions.DependencyInjection;

namespace SchemeGenerator;

/// <summary>
/// DI extensions for SchemeGenerator.
/// </summary>
public static class SchemeGeneratorServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Registers <see cref="ISchemeGenerator"/> as a singleton.
        /// </summary>
        public IServiceCollection AddSchemeGenerator(Action<SchemeGeneratorOptions>? configure = null)
        {
            var options = new SchemeGeneratorOptions();
            configure?.Invoke(options);
            services.AddSingleton<ISchemeGenerator>(_ => new SchemeGenerator(options));
            return services;
        }
    }
}
