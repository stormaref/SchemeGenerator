using Microsoft.Extensions.DependencyInjection;

namespace SchemeGenerator;

public static class DependencyInjection
{
    public static void AddSchemeGenerator(this IServiceCollection services)
    {
        services.AddSingleton<ISchemeGenerator, SchemeGenerator>();
    }
}