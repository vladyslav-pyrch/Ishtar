using System.Diagnostics.CodeAnalysis;

namespace Ishtar.DependencyInjection.Abstractions;

public class ServiceDescriptor
{
    private readonly Type _serviceType = null!;
    
    private readonly Type? _implementationType;

    private readonly object? _instance;

    private readonly IServiceFactory? _factory;
    
    private readonly ServiceLifetime _lifetime;

    private readonly ServiceDescriptorMode _mode;

    public ServiceDescriptor(Type serviceType, Type implementationType,
        ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        if (!implementationType.GetInterfaces().Contains(serviceType) &&
            !serviceType.IsSubclassOf(implementationType) && serviceType != implementationType)
        {
            throw new InvalidServiceHierarchyException(serviceType, implementationType);
        }

        if (implementationType.IsInterface || implementationType.IsAbstract)
        {
            throw new InvalidOperationException(
                $"Implementation type {implementationType.FullName} may not be an interface or an abstract class");
        }
        
        ServiceType = serviceType;
        ImplementationType = implementationType;
        Lifetime = lifetime;
        Mode = ServiceDescriptorMode.Description;
    }

    public ServiceDescriptor(Type serviceType, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        : this(serviceType, serviceType, lifetime)
    { }

    public ServiceDescriptor(Type serviceType, object instance)
    {
        if (!instance.GetType().IsSubclassOf(serviceType) && !instance.GetType().IsAssignableTo(serviceType))
        {
            throw new InvalidServiceHierarchyException(serviceType, instance.GetType());
        }
        
        ServiceType = serviceType;
        Instance = instance;
        Mode = ServiceDescriptorMode.Instance;
        Lifetime = ServiceLifetime.Singleton;
    }

    public ServiceDescriptor(Type serviceType, IServiceFactory factory, ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        ServiceType = serviceType;
        Lifetime = lifetime;
        Factory = factory;
        Mode = ServiceDescriptorMode.Factory;
    }

    public Type ServiceType
    {
        get => _serviceType;
        private init => _serviceType = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Type? ImplementationType
    {
        get => _implementationType!;
        private init => _implementationType = value ?? throw new ArgumentNullException(nameof(value), "Implementation type may not be null.");
    }

    public object? Instance
    {
        get => _instance;
        private init => _instance = value;
    }
    
    public IServiceFactory? Factory
    {
        get => _factory;
        private init => _factory = value ?? throw new ArgumentNullException(nameof(value), "Factory may not be null.");
    }

    public ServiceLifetime Lifetime
    {
        get => _lifetime;
        private init => _lifetime = value;
    }

    public ServiceDescriptorMode Mode
    {
        get => _mode;
        private init => _mode = value;
    }

    [MemberNotNullWhen(true, "ImplementationType")]
    public bool IsInDescriptionMode => Mode == ServiceDescriptorMode.Description;
    
    [MemberNotNullWhen(true, "Factory")]
    public bool IsInFactoryMode => Mode == ServiceDescriptorMode.Factory;

    public bool IsInInstanceMode => Mode == ServiceDescriptorMode.Instance;
}