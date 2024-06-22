namespace Ishtar.DependencyInjection.Abstractions;

public class InvalidServiceHierarchyException(Type serviceType, Type implementationType)
    : Exception($"{serviceType.FullName} is not an interface or parent of {implementationType.FullName}, or the same as {implementationType.FullName}");