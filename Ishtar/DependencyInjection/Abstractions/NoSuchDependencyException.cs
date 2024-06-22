namespace Ishtar.DependencyInjection;

public class NoSuchDependencyException(Type serviceType)
    : Exception($"There is no dependency of the type {serviceType.FullName}");