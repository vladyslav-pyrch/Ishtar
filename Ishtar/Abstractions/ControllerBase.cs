namespace Ishtar.Abstractions;

public abstract class ControllerBase
{
    public IHttpContext Context { get; internal set; } = null!;

    protected IActionResult Result(HttpStatusCode statusCode, object obj)
    {
        return new ObjectResult(statusCode, obj);
    }

    protected IActionResult Result(HttpStatusCode statusCode)
    {
        return new ActionResult(statusCode);
    }
}