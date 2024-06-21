using System.Reflection;
using Ishtar.DependencyInjection.Abstractions;
using IServiceProvider = System.IServiceProvider;

namespace Ishtar.DependencyInjection;

public class ServiceProvider : IServiceProvider
{
    private static readonly Dictionary<ServiceDescriptor, object> SingletonServices;
    
    private readonly IReadOnlyList<ServiceDescriptor> _services = null!;

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

    public IReadOnlyList<ServiceDescriptor> Services
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
    /// <exception cref="NoSuchDependencyException">
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

        if (descriptor.Lifetime == ServiceLifetime.Singleton && SingletonServices.TryGetValue(descriptor, out instance))
        {
            return instance;
        }

        if (descriptor.Lifetime == ServiceLifetime.Scoped && _scopedServices.TryGetValue(descriptor, out instance))
        {
            return instance;
        }

        instance = GetService(descriptor, used);

        if (descriptor.Lifetime == ServiceLifetime.Singleton)
        {
            SingletonServices.Add(descriptor, instance);
        }
        
        if (descriptor.Lifetime == ServiceLifetime.Scoped)
        {
            _scopedServices.Add(descriptor, instance);
        }

        return instance;
    }

    private object? GetService(ServiceDescriptor descriptor, List<Type> used)
    {
        object? instance = null;
        
        used.Add(descriptor.ServiceType);

        ConstructorInfo[] constructors = descriptor.ImplementationType.GetConstructors();

        switch (constructors.Length)
        {
            case 0:
                instance = Activator.CreateInstance(descriptor.ImplementationType);
                break;
            case 1:
                ConstructorInfo constructor = constructors[0];
                IEnumerable<Type> typesOfDependencies = constructor.GetParameters().Select(info => info.ParameterType);
            
                Type? reoccurred = typesOfDependencies.FirstOrDefault(used.Contains);
                if (reoccurred is not null)
                {
                    throw new RecursiveDependencyException(descriptor.ServiceType, reoccurred);
                }

                IEnumerable<object> dependencies = typesOfDependencies.Select(
                    type => GetService(type, used) ?? throw new NoSuchDependencyException(type)
                );

                instance = Activator.CreateInstance(descriptor.ImplementationType, dependencies);
                break;
            case >= 2:
                throw new MultipleConstructorsException(descriptor.ServiceType, descriptor.ImplementationType);
        }
            
        used.Remove(descriptor.ServiceType);

        return instance;
    }
}