namespace Ishtar.DependencyInjection.Abstractions;

public interface IServiceFactory
{
    public object Invoke(IServiceProvider serviceProvider);
}