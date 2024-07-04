namespace Ishtar.Abstractions;

public interface IActionResult
{
    public HttpStatusCode StatusCode { get; }
}