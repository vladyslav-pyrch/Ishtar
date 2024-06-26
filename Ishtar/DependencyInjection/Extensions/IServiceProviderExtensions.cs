using System.Reflection;
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

    public static object InjectInto(this IServiceProvider serviceProvider, Type serviceType)
    {
        if (serviceType.IsInterface || serviceType.IsAbstract)
        {
            throw new InvalidOperationException(
                $"Service type {serviceType.FullName} may not be an interface or an abstract class");
        }
        
        ConstructorInfo[] constructors = serviceType.GetConstructors();
        if (constructors.Length >= 2)
        {
            throw new MultipleConstructorsException(serviceType, serviceType);
        }

        object[] dependencies = constructors[0].GetParameters()
            .Select(
                info => serviceProvider.GetService(info.ParameterType)  ?? throw new NoSuchServiceException(info.ParameterType)
                ).ToArray();
        
        return Activator.CreateInstance(serviceType, dependencies.ToArray())!;
    }

    public static TService InjectInto<TService>(this IServiceProvider serviceProvider)
        where TService : class
    {
        return (TService)serviceProvider.InjectInto(typeof(TService));
    }
}