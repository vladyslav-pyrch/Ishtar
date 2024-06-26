using Ishtar.DependencyInjection.Abstractions;

namespace Ishtar.Abstractions;

public interface IWebApplicationBuilder<TWebApplication> where TWebApplication : IWebApplication
{
    public IServiceCollection Services { get; }
    
    public Task<TWebApplication> Build();
}