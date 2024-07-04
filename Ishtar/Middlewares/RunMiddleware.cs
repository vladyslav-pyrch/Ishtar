using Ishtar.Abstractions;

namespace Ishtar.Middlewares;

public class RunMiddleware : Middleware
{
    private Action _action;

    public RunMiddleware(Action action)
    {
        _action = action;
    }

    public override Task Invoke(IHttpContext context) => new(_action);
}