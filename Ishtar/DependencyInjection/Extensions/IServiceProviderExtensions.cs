using Ishtar.DependencyInjection.Abstractions;
using IServiceProvider = Ishtar.DependencyInjection.Abstractions.IServiceProvider;

namespace Ishtar.DependencyInjection.Extensions;

public static class IServiceProviderExtensions
{
    public static object GetRequiredService(this IServiceProvider serviceProvider, Type serviceType)
    {
        return serviceProvider.GetService(serviceType) ?? throw new NoSuchServiceException(serviceType);
    }

    public static TService? GetService<TService>(this IServiceProvider serviceProvider)
    {
        return (TService?)serviceProvider.GetService(typeof(TService));
    }

    public static TService GetRequiredService<TService>(this IServiceProvider serviceProvider)
    {
        return (TService)serviceProvider.GetRequiredService(typeof(TService));
    }
}