namespace Ishtar.DependencyInjection.Abstractions;

public class MultipleServicesOfTheSameTypeException(Type serviceType) 
    : Exception($"There are multiple services for the service type {serviceType.FullName}");
