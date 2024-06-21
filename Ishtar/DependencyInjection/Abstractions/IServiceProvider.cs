namespace Ishtar.DependencyInjection.Abstractions;

public interface IServiceProvider
{
    public object? GetService(Type serviceType);
}