using System.Reflection;
using Ishtar.DependencyInjection.Abstractions;
using IServiceProvider = Ishtar.DependencyInjection.Abstractions.IServiceProvider;

namespace Ishtar.DependencyInjection.Extensions;

public static class ServiceProviderExtensions
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
    
    public static object InjectInto(this IServiceProvider serviceProvider, Type serviceType, params object?[]? args)
    {
        if (serviceType.IsInterface || serviceType.IsAbstract)
            throw new InvalidOperationException(
                $"Service type {serviceType.FullName} may not be an interface or an abstract class");
        
        ConstructorInfo[] constructors = serviceType.GetConstructors();

        foreach (ConstructorInfo constructor in constructors)
        {
            object[] dependencies = constructor.GetParameters()
                .Select(
                    info => serviceProvider.GetService(info.ParameterType)
                ).Except([null]).ToArray()!;

            try
            {
                return Activator.CreateInstance(serviceType, [..dependencies, ..args ?? []])!;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        throw new InvalidOperationException("Could not find a constructor.");
    }

    public static object InjectInto(this IServiceProvider serviceProvider, Type serviceType)
    {
        return serviceProvider.InjectInto(serviceType, null);
    }

    public static TService InjectInto<TService>(this IServiceProvider serviceProvider)
        where TService : class
    {
        return (TService)serviceProvider.InjectInto(typeof(TService));
    }

    public static TService InjectInto<TService>(this IServiceProvider serviceProvider, params object?[]? args)
        where TService : class
    {
        return (TService)serviceProvider.InjectInto(typeof(TService), args);
    }
}