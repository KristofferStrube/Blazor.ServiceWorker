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

    public static implicit operator RequestInfo(Request r)
    {
        return new(r);
    }

    public static implicit operator RequestInfo(string r)
    {
        return new(r);
    }

    public static implicit operator string(RequestInfo ri)
    {
        return ri.type is RequestInfoType.Request ? ri.request.Id.ToString() : ri.stringRequest;
    }
}

public enum RequestInfoType
{
    Request,
    String
}