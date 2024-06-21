namespace Ishtar.DependencyInjection.Abstractions;

public class ServiceDescriptor
{
    private readonly Type _serviceType = null!;

    private readonly Type _implementationType = null!;
    
    private readonly ServiceLifetime _lifetime;

    public ServiceDescriptor(Type serviceType, Type implementationType,
        ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        ServiceType = serviceType;
        ImplementationType = implementationType;
        Lifetime = lifetime;
    }

    public ServiceDescriptor(Type serviceType, ServiceLifetime lifetime = ServiceLifetime.Singleton) 
        : this(serviceType, serviceType, lifetime)
    { }

    public Type ServiceType
    {
        get => _serviceType;
        private init => _serviceType = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Type ImplementationType
    {
        get => _implementationType;
        private init => _implementationType = value ?? throw new ArgumentNullException(nameof(value));
    }

    public ServiceLifetime Lifetime
    {
        get => _lifetime;
        private init => _lifetime = value;
    }
}