namespace Ishtar.Abstractions;

public interface IObjectResult : IActionResult
{
    public object Result { get; }
}