namespace Ishtar.DependencyInjection.Abstractions;

public class NoSuchServiceException(Type serviceType)
    : Exception($"There is no dependency of the type {serviceType.FullName}");