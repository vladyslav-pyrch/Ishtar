using Ishtar.DependencyInjection.Abstractions;
using IServiceProvider = Ishtar.DependencyInjection.Abstractions.IServiceProvider;

namespace Ishtar.DependencyInjection;

public class DefaultServiceFactory : IServiceFactory
{
    private readonly Func<IServiceProvider, object> _factory;

    public DefaultServiceFactory(Func<IServiceProvider, object> factory)
    {
        _factory = factory;
    }

    public object Invoke(IServiceProvider serviceProvider)
    {
        return _factory(serviceProvider);
    }
}

public class DefaultServiceFactory<TService> : IServiceFactory where TService : notnull
{
    private readonly Func<IServiceProvider, TService> _factory;

    public DefaultServiceFactory(Func<IServiceProvider, TService> factory)
    {
        _factory = factory;
    }

    public object Invoke(IServiceProvider serviceProvider)
    {
        return _factory(serviceProvider);
    }
}