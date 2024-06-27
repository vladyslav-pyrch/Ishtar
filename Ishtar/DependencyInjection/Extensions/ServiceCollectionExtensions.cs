using Ishtar.DependencyInjection.Abstractions;
using IServiceProvider = Ishtar.DependencyInjection.Abstractions.IServiceProvider;

namespace Ishtar.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddTransient(this IServiceCollection serviceCollection, Type serviceType, Type implementationType)
    {
        serviceCollection.Add(new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Transient));
    }

    public static void AddTransient(this IServiceCollection serviceCollection, Type serviceType)
    {
        serviceCollection.AddTransient(serviceType, serviceType);
    }

    public static void AddTransient<TServiceType, TImplementationType>(this IServiceCollection serviceCollection) 
        where TImplementationType : class, TServiceType 
    {
        serviceCollection.AddTransient(typeof(TServiceType), typeof(TImplementationType));
    }

    public static void AddTransient<TServiceType>(this IServiceCollection serviceCollection) where TServiceType : class
    {
        serviceCollection.AddTransient<TServiceType, TServiceType>();
    }
    
    public static void AddScoped(this IServiceCollection serviceCollection, Type serviceType, Type implementationType)
    {
        serviceCollection.Add(new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Scoped));
    }

    public static void AddScoped(this IServiceCollection serviceCollection, Type serviceType)
    {
        serviceCollection.AddScoped(serviceType, serviceType);
    }

    public static void AddScoped<TServiceType, TImplementationType>(this IServiceCollection serviceCollection) 
        where TImplementationType : class, TServiceType 
    {
        serviceCollection.AddScoped(typeof(TServiceType), typeof(TImplementationType));
    }

    public static void AddScoped<TServiceType>(this IServiceCollection serviceCollection) where TServiceType : class
    {
        serviceCollection.AddScoped<TServiceType, TServiceType>();
    }
    
    public static void AddSingleton(this IServiceCollection serviceCollection, Type serviceType, Type implementationType)
    {
        serviceCollection.Add(new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Singleton));
    }

    public static void AddSingleton(this IServiceCollection serviceCollection, Type serviceType)
    {
        serviceCollection.AddSingleton(serviceType, serviceType);
    }

    public static void AddSingleton<TServiceType, TImplementationType>(this IServiceCollection serviceCollection)
        where TImplementationType : class, TServiceType 
    {
        serviceCollection.AddSingleton(typeof(TServiceType), typeof(TImplementationType));
    }

    public static void AddSingleton<TServiceType>(this IServiceCollection serviceCollection) where TServiceType : class
    {
        serviceCollection.AddSingleton<TServiceType, TServiceType>();
    }

    public static void AddInstance(this IServiceCollection serviceCollection, Type serviceType, object instance)
    {
        serviceCollection.Add(new ServiceDescriptor(serviceType, instance));
    }

    public static void AddInstance<TInstance>(this IServiceCollection serviceCollection, TInstance instance) 
        where TInstance : notnull 
    {
        serviceCollection.AddInstance(typeof(TInstance), instance);
    }

    public static void AddFactory(this IServiceCollection serviceCollection, Type serviceType, IServiceFactory factory,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        serviceCollection.Add(new ServiceDescriptor(serviceType, factory, lifetime));
    }

    public static void AddFactory(this IServiceCollection serviceCollection, Type serviceType,
        Func<IServiceProvider, object> factory, ServiceLifetime lifetime = ServiceLifetime.Scoped)

    {
        serviceCollection.AddFactory(serviceType, new DefaultServiceFactory(factory), lifetime);
    }

    public static void AddFactory<TService>(this IServiceCollection serviceCollection,
        Func<IServiceProvider, TService> factory, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where TService : notnull 
    {
        serviceCollection.AddFactory(typeof(TService), new DefaultServiceFactory<TService>(factory), lifetime);
    }
}