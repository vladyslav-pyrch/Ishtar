using Ishtar.Abstractions;

namespace Ishtar;

public class ActionResult : IActionResult
{
    public ActionResult(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }
    
    public HttpStatusCode StatusCode { get; }
}