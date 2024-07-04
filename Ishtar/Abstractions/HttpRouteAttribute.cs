namespace Ishtar.Abstractions;

[AttributeUsage(AttributeTargets.Method)]
public class HttpRouteAttribute : Attribute
{
    public HttpRouteAttribute(string route = "", string httpMethod = "GET")
    {
        Route = route;
        HttpMethod = new HttpMethod(httpMethod);
    }

    public HttpMethod HttpMethod { get; }
    
    public string Route { get; }
}