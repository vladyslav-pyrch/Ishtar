namespace Ishtar.DependencyInjection.Abstractions;

public class MultipleConstructorsException(Type serviceType, Type implementationType)
    : Exception($"Service {serviceType.FullName}, implemented by {implementationType.FullName}, has more than one constructor.");