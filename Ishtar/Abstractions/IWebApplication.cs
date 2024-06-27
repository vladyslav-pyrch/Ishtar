using Ishtar.DependencyInjection.Abstractions;

namespace Ishtar.Abstractions;
using IServiceProvider = Ishtar.DependencyInjection.Abstractions.IServiceProvider;

public interface IWebApplication
{
    public IServiceProvider ServiceProvider { get; }
    
    public Task Start(CancellationToken cancellationToken = default);

    public Task Stop(CancellationToken cancellationToken = default);

    public IWebApplication Use<TMiddleware>(params object?[]? args) where TMiddleware : class, IMiddleware;
}