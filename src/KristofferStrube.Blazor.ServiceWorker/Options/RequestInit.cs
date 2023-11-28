namespace KristofferStrube.Blazor.ServiceWorker;

public class RequestInit
{
    public string Method { get; set; } = "GET";
    public BodyInit Body { get; set; }
    public string Credentials { get; set; }
    public bool KeepAlive { get; set; } = false;
    public string Duplex { get; set; }
}