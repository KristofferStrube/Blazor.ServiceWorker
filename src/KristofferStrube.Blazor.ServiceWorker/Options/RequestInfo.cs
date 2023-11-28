namespace KristofferStrube.Blazor.ServiceWorker;

public class RequestInfo
{
    public readonly object value;

    public RequestInfo(Request value)
    {
        this.value = value;
    }
    public RequestInfo(string value)
    {
        this.value = value;
    }

    public static implicit operator RequestInfo(Request value)
    {
        return new(value);
    }

    public static implicit operator RequestInfo(string value)
    {
        return new(value);
    }

    public static implicit operator string(RequestInfo ri)
    {
        return ri.value switch
        {
            Request request => request.Id.ToString(),
            string request => request,
            _ => throw new InvalidCastException("Constructed with wrong type.")
        };
    }
}

