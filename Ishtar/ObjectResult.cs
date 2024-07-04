using Ishtar.Abstractions;

namespace Ishtar;

public class ObjectResult : ActionResult, IObjectResult
{
    public ObjectResult(HttpStatusCode statusCode, object result) : base(statusCode)
    {
        Result = result;
    }

    public object Result { get; }
}