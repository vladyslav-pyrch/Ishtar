namespace Ishtar;

[AttributeUsage(AttributeTargets.Class)]
public class ApiControllerAttribute : Attribute
{
    public ApiControllerAttribute(string route = "/")
    {
        Route = route;
    }

    public string Route { get; }
}