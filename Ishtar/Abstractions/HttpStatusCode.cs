namespace Ishtar.Abstractions;

public class HttpStatusCode : IEquatable<HttpStatusCode>
{
    public HttpStatusCode(int statusCode, string statusMessage)
    {
        StatusCode = statusCode;
        StatusMessage = statusMessage;
    }
    
    public int StatusCode { get; }
    
    public string StatusMessage { get; }

    public static HttpStatusCode NotSet0 => new HttpStatusCode(0, "Not Set");
    
    public static HttpStatusCode Continue100 => new(100,"Continue");
    
    public static HttpStatusCode SwitchingProtocols101 => new(101,"Switching Protocols");
    
    public static HttpStatusCode Processing102 => new(102, "Processing");
    
    public static HttpStatusCode EarlyHints103 => new(103, "Early Hints");
    
    public static HttpStatusCode OK200 => new(200, "OK");
    
    public static HttpStatusCode Created201 => new(201, "Created");
    
    public static HttpStatusCode Accepted202 => new(202, "Accepted");
    
    public static HttpStatusCode NonAuthoritativeInformation203 => new(203, "Non-Authoritative Information");
    
    public static HttpStatusCode NoContent204=> new(204, "No Content");
    
    public static HttpStatusCode ResetContent205 => new(205, "Reset Content");
    
    public static HttpStatusCode PartialContent206 => new(206, "Partial Content");
    
    public static HttpStatusCode MultiStatus207 => new(207, "Multi-Status");
    
    public static HttpStatusCode AlreadyReported208 => new(208, "Already Reported");
    
    public static HttpStatusCode IMUsed226=> new(226, "IM Used");
    
    public static HttpStatusCode MultipleChoices300 => new(300, "Multiple Choices");
    
    public static HttpStatusCode MovedPermanently301 => new(301, "Moved Permanently");
    
    public static HttpStatusCode Found302 => new(302, "Found");
    
    public static HttpStatusCode SeeOther303 => new(303, "See Other");
    
    public static HttpStatusCode NotModified304 => new(304, "Not Modified");
    
    public static HttpStatusCode UseProxy305 => new(305, "Use Proxy");
    
    public static HttpStatusCode TemporaryRedirect307 => new(307, "Temporary Redirect");
    
    public static HttpStatusCode PermanentRedirect308 => new(308, "Permanent Redirect");
    
    public static HttpStatusCode BadRequest400 => new(400, "Bad Request");
    
    public static HttpStatusCode Unauthorized401 => new(401, "Unauthorized");
    
    public static HttpStatusCode PaymentRequired402 => new(402, "Payment Required");
    
    public static HttpStatusCode Forbidden403 => new(403, "Forbidden");
    
    public static HttpStatusCode NotFound404 => new(404, "Not Found");
    
    public static HttpStatusCode MethodNotAllowed405 => new(405, "Method Not Allowed");
    
    public static HttpStatusCode NotAcceptable406 => new(406, "Not Acceptable");
    
    public static HttpStatusCode ProxyAuthenticationRequired407 => new(407, "Proxy Authentication Required");
    
    public static HttpStatusCode RequestTimeout408 => new(408, "Request Timeout");
    
    public static HttpStatusCode Conflict409 => new(409, "Conflict");
    
    public static HttpStatusCode Gone410 => new(410, "Gone");
    
    public static HttpStatusCode LengthRequired411 => new(411, "Length Required");
    
    public static HttpStatusCode PreconditionFailed412 => new(412, "Precondition Failed");
    
    public static HttpStatusCode PayloadTooLarge413 => new(413, "Payload Too Large");
    
    public static HttpStatusCode URITooLong414 => new(414, "URI Too Long");
    
    public static HttpStatusCode UnsupportedMediaType415 => new(415, "Unsupported Media Type");
    
    public static HttpStatusCode RangeNotSatisfiable416 => new(416, "Range Not Satisfiable");
    
    public static HttpStatusCode ExpectationFailed417 => new(417, "Expectation Failed");
    
    public static HttpStatusCode IAmATeapot418 => new(418,"I'm a teapot");
    
    public static HttpStatusCode MisdirectedRequest421 => new(421, "Misdirected Request");
    
    public static HttpStatusCode UnprocessableContent422 => new(422, "Unprocessable Content");
    
    public static HttpStatusCode Locked423 => new(423, "Locked");
    
    public static HttpStatusCode FailedDependency424 => new(424, "Failed Dependency");
    
    public static HttpStatusCode TooEarly425 => new(425, "Too Early");
    
    public static HttpStatusCode UpgradeRequired426 => new(426, "Upgrade Required");
    
    public static HttpStatusCode PreconditionRequired428 => new(428, "Precondition Required");
    
    public static HttpStatusCode TooManyRequests429 => new(429, "Too Many Requests");
    
    public static HttpStatusCode RequestHeaderFieldsTooLarge431 => new(431, "Request Header Fields Too Large");
    
    public static HttpStatusCode UnavailableForLegalReasons451 => new(451, "Unavailable For Legal Reasons");
    
    public static HttpStatusCode InternalServerError500 => new(500, "Internal Server Error");
    
    public static HttpStatusCode NotImplemented501 => new(501, "Not Implemented");
    
    public static HttpStatusCode BadGateway502 => new(502, "Bad Gateway");
    
    public static HttpStatusCode ServiceUnavailable503 => new(503, "Service Unavailable");
    
    public static HttpStatusCode GatewayTimeout504 => new(504, "Gateway Timeout");
    
    public static HttpStatusCode HttpVersionNotSupported505 => new(505, "HTTP Version Not Supported");
    
    public static HttpStatusCode VariantAlsoNegotiates506 => new(506, "Variant Also Negotiates");
    
    public static HttpStatusCode InsufficientStorage507 => new(507, "Insufficient Storage");
    
    public static HttpStatusCode LoopDetected508 => new(508, "Loop Detected");
    
    public static HttpStatusCode NotExtended510 => new(510, "Not Extended");
    
    public static HttpStatusCode NetworkAuthenticationRequired511 => new(511, "Network Authentication Required");

    public static bool operator ==(HttpStatusCode statusCode1, HttpStatusCode statusCode2)
    {
        return statusCode1.Equals(statusCode2);
    }

    public static bool operator !=(HttpStatusCode statusCode1, HttpStatusCode statusCode2)
    {
        return !(statusCode1 == statusCode2);
    }

    public bool Equals(HttpStatusCode? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return StatusCode == other.StatusCode && StatusMessage == other.StatusMessage;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((HttpStatusCode)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(StatusCode, StatusMessage);
    }
}