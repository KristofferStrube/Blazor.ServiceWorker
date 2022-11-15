namespace KristofferStrube.Blazor.ServiceWorker;

public class RequestInfo
{
    internal readonly object request;

    internal RequestInfo(object request)
    {
        this.request = request;
    }

    public static implicit operator RequestInfo(Request request) => new(request);

    public static implicit operator RequestInfo(string request) => new(request);

    public static implicit operator string(RequestInfo ri) => ri.request switch
    {
        Request request => request.Id.ToString(),
        string request => request,
        _ => throw new InvalidCastException("Constructed with wrong type.")
    };
}

