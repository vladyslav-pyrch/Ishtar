using System.Net;
using System.Net.Sockets;
using Ishtar.Abstractions;
using Ishtar.DependencyInjection;
using Ishtar.DependencyInjection.Abstractions;

namespace Ishtar;

public class WebApplicationBuilder : IWebApplicationBuilder<WebApplication>
{
    private readonly string[]? _args;
    
    public WebApplicationBuilder(string[]? args)
    {
        _args = args;
        Services = new ServiceCollection();
    }
    
    public IServiceCollection Services { get; }
    
    public Task<WebApplication> Build()
    {
        var listener = new TcpListener(IPAddress.Any, 4221);

        return Task.FromResult(new WebApplication(listener, Services));
    }
}