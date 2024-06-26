using System.Reflection;
using Ishtar.DependencyInjection.Abstractions;
using IServiceProvider = Ishtar.DependencyInjection.Abstractions.IServiceProvider;

namespace Ishtar.DependencyInjection;

internal class ServiceProvider : IServiceProvider
{
    private static readonly Dictionary<ServiceDescriptor, object> SingletonServices;
    
    private readonly IServiceCollection _services = null!;

    private readonly Dictionary<ServiceDescriptor, object> _scopedServices;

    static ServiceProvider()
    {
        SingletonServices = new Dictionary<ServiceDescriptor, object>();
    }

    public ServiceProvider(IServiceCollection services)
    {
        Services = services;
        _scopedServices = new Dictionary<ServiceDescriptor, object>();
    }

    public IServiceCollection Services
    {
        get => _services;
        private init => _services = value ?? throw new ArgumentNullException(nameof(value), "Services may not be null.");
    }

    /// <summary>
    /// Creates a requested service or returns null if there are no such service in <see cref="Services"/>.
    /// </summary>
    /// <exception cref="RecursiveDependencyException">
    /// Thrown when two dependencies are in a recursive dependency on each other.
    /// </exception>
    /// <exception cref="MultipleServicesOfTheSameTypeException">
    /// Thrown when two or more services have the same type.
    /// </exception>
    /// <exception cref="NoSuchServiceException">
    /// Thrown when a dependency was not created.
    /// </exception>
    /// <exception cref="MultipleConstructorsException">
    /// Thrown when a service has more than one constructor.
    /// </exception>
    /// <param name="serviceType">The type of service to get.</param>
    /// <returns>A requested service or null.</returns>
    public object? GetService(Type serviceType)
    {
        var usedDependencies = new List<Type>();

        return GetService(serviceType, usedDependencies);
    }

    private object? GetService(Type serviceType, List<Type> used)
    {
        object? instance;
        ServiceDescriptor? descriptor;
        
        try
        {
            descriptor = Services.SingleOrDefault(serviceDescriptor => serviceDescriptor.ServiceType == serviceType);
        }
        catch (InvalidOperationException)
        {
            throw new MultipleServicesOfTheSameTypeException(serviceType);
        }
        
        if (descriptor is null)
        {
            return null;
        }

        switch (descriptor.Lifetime)
        {
            case ServiceLifetime.Singleton when SingletonServices.TryGetValue(descriptor, out instance):
                return instance;
            case ServiceLifetime.Scoped when _scopedServices.TryGetValue(descriptor, out instance):
                return instance;
            case ServiceLifetime.Transient:
                break;
        }

        instance = GetService(descriptor, used);

        switch (descriptor.Lifetime)
        {
            case ServiceLifetime.Singleton:
                SingletonServices.Add(descriptor, instance);
                break;
            case ServiceLifetime.Scoped:
                _scopedServices.Add(descriptor, instance);
                break;
            case ServiceLifetime.Transient:
                break;
        }

        return instance;
    }

    private object? GetService(ServiceDescriptor descriptor, List<Type> used)
    {
        object? instance = null;
        
        used.Add(descriptor.ServiceType);

        if (descriptor.IsInInstanceMode)
        {
            instance = descriptor.Instance;
        }
        if (descriptor.IsInFactoryMode)
        {
            instance = descriptor.Factory.Invoke(this);
        }
        if (descriptor.IsInDescriptionMode)
        {
            instance = UsingImplementationType(descriptor, used);
        }
        
        used.Remove(descriptor.ServiceType);

        return instance;
    }

    private object? UsingImplementationType(ServiceDescriptor descriptor, List<Type> used)
    {
        if (!descriptor.IsInDescriptionMode)
        {
            throw new InvalidOperationException(
                "This method is allow to run only in when descriptor is in Description mode.");
        }

        ConstructorInfo[] constructors = descriptor.ImplementationType.GetConstructors();
        if (constructors.Length >= 2)
        {
            throw new MultipleConstructorsException(descriptor.ServiceType, descriptor.ImplementationType);
        }
        
        object[] dependencies = constructors[0].GetParameters()
            .Select(info =>
            {
                if (used.Contains(info.ParameterType))
                {
                    throw new RecursiveDependencyException(descriptor.ServiceType, info.ParameterType);
                }
                
                return info.ParameterType;
            })
            .Select(type => GetService(type, used) ?? throw new NoSuchServiceException(type))
            .ToArray();

        return Activator.CreateInstance(descriptor.ImplementationType, dependencies);
    }
}