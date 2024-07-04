namespace Ishtar.Abstractions;

public record struct HttpStatusCode
{
    public HttpStatusCode(int statusCode, string statusMessage)
    {
        StatusCode = statusCode;
        StatusMessage = statusMessage;
    }
    
    public int StatusCode { get; }
    
    public string StatusMessage { get; }

    public static readonly HttpStatusCode NotSet0 = new HttpStatusCode(0, "Not Set");
    
    public static readonly HttpStatusCode Continue100 = new(100,"Continue");
    
    public static readonly HttpStatusCode SwitchingProtocols101 = new(101,"Switching Protocols");
    
    public static readonly HttpStatusCode Processing102 = new(102, "Processing");
    
    public static readonly HttpStatusCode EarlyHints103 = new(103, "Early Hints");
    
    public static readonly HttpStatusCode Ok200 = new(200, "OK");
    
    public static readonly HttpStatusCode Created201 = new(201, "Created");
    
    public static readonly HttpStatusCode Accepted202 = new(202, "Accepted");
    
    public static readonly HttpStatusCode NonAuthoritativeInformation203 = new(203, "Non-Authoritative Information");
    
    public static readonly HttpStatusCode NoContent204= new(204, "No Content");
    
    public static readonly HttpStatusCode ResetContent205 = new(205, "Reset Content");
    
    public static readonly HttpStatusCode PartialContent206 = new(206, "Partial Content");
    
    public static readonly HttpStatusCode MultiStatus207 = new(207, "Multi-Status");
    
    public static readonly HttpStatusCode AlreadyReported208 = new(208, "Already Reported");
    
    public static readonly HttpStatusCode ImUsed226= new(226, "IM Used");
    
    public static readonly HttpStatusCode MultipleChoices300 = new(300, "Multiple Choices");
    
    public static readonly HttpStatusCode MovedPermanently301 = new(301, "Moved Permanently");
    
    public static readonly HttpStatusCode Found302 = new(302, "Found");
    
    public static readonly HttpStatusCode SeeOther303 = new(303, "See Other");
    
    public static readonly HttpStatusCode NotModified304 = new(304, "Not Modified");
    
    public static readonly HttpStatusCode UseProxy305 = new(305, "Use Proxy");
    
    public static readonly HttpStatusCode TemporaryRedirect307 = new(307, "Temporary Redirect");
    
    public static readonly HttpStatusCode PermanentRedirect308 = new(308, "Permanent Redirect");
    
    public static readonly HttpStatusCode BadRequest400 = new(400, "Bad Request");
    
    public static readonly HttpStatusCode Unauthorized401 = new(401, "Unauthorized");
    
    public static readonly HttpStatusCode PaymentRequired402 = new(402, "Payment Required");
    
    public static readonly HttpStatusCode Forbidden403 = new(403, "Forbidden");
    
    public static readonly HttpStatusCode NotFound404 = new(404, "Not Found");
    
    public static readonly HttpStatusCode MethodNotAllowed405 = new(405, "Method Not Allowed");
    
    public static readonly HttpStatusCode NotAcceptable406 = new(406, "Not Acceptable");
    
    public static readonly HttpStatusCode ProxyAuthenticationRequired407 = new(407, "Proxy Authentication Required");
    
    public static readonly HttpStatusCode RequestTimeout408 = new(408, "Request Timeout");
    
    public static readonly HttpStatusCode Conflict409 = new(409, "Conflict");
    
    public static readonly HttpStatusCode Gone410 = new(410, "Gone");
    
    public static readonly HttpStatusCode LengthRequired411 = new(411, "Length Required");
    
    public static readonly HttpStatusCode PreconditionFailed412 = new(412, "Precondition Failed");
    
    public static readonly HttpStatusCode PayloadTooLarge413 = new(413, "Payload Too Large");
    
    public static readonly HttpStatusCode UriTooLong414 = new(414, "URI Too Long");
    
    public static readonly HttpStatusCode UnsupportedMediaType415 = new(415, "Unsupported Media Type");
    
    public static readonly HttpStatusCode RangeNotSatisfiable416 = new(416, "Range Not Satisfiable");
    
    public static readonly HttpStatusCode ExpectationFailed417 = new(417, "Expectation Failed");
    
    public static readonly HttpStatusCode ImATeapot418 = new(418,"I'm a teapot");
    
    public static readonly HttpStatusCode MisdirectedRequest421 = new(421, "Misdirected Request");
    
    public static readonly HttpStatusCode UnprocessableContent422 = new(422, "Unprocessable Content");
    
    public static readonly HttpStatusCode Locked423 = new(423, "Locked");
    
    public static readonly HttpStatusCode FailedDependency424 = new(424, "Failed Dependency");
    
    public static readonly HttpStatusCode TooEarly425 = new(425, "Too Early");
    
    public static readonly HttpStatusCode UpgradeRequired426 = new(426, "Upgrade Required");
    
    public static readonly HttpStatusCode PreconditionRequired428 = new(428, "Precondition Required");
    
    public static readonly HttpStatusCode TooManyRequests429 = new(429, "Too Many Requests");
    
    public static readonly HttpStatusCode RequestHeaderFieldsTooLarge431 = new(431, "Request Header Fields Too Large");
    
    public static readonly HttpStatusCode UnavailableForLegalReasons451 = new(451, "Unavailable For Legal Reasons");
    
    public static readonly HttpStatusCode InternalServerError500 = new(500, "Internal Server Error");
    
    public static readonly HttpStatusCode NotImplemented501 = new(501, "Not Implemented");
    
    public static readonly HttpStatusCode BadGateway502 = new(502, "Bad Gateway");
    
    public static readonly HttpStatusCode ServiceUnavailable503 = new(503, "Service Unavailable");
    
    public static readonly HttpStatusCode GatewayTimeout504 = new(504, "Gateway Timeout");
    
    public static readonly HttpStatusCode HttpVersionNotSupported505 = new(505, "HTTP Version Not Supported");
    
    public static readonly HttpStatusCode VariantAlsoNegotiates506 = new(506, "Variant Also Negotiates");
    
    public static readonly HttpStatusCode InsufficientStorage507 = new(507, "Insufficient Storage");
    
    public static readonly HttpStatusCode LoopDetected508 = new(508, "Loop Detected");
    
    public static readonly HttpStatusCode NotExtended510 = new(510, "Not Extended");
    
    public static readonly HttpStatusCode NetworkAuthenticationRequired511 = new(511, "Network Authentication Required");
}