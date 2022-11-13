namespace KristofferStrube.Blazor.ServiceWorker;

public class RequestInfo
{
    internal readonly Request request;
    internal readonly string stringRequest;
    internal readonly RequestInfoType type;

    public RequestInfo(Request request)
    {
        this.request = request;
        type = RequestInfoType.Request;
    }

    public RequestInfo(string request)
    {
        this.stringRequest = request;
        type = RequestInfoType.String;
    }
}

public enum RequestInfoType
{
    Request,
    String
}