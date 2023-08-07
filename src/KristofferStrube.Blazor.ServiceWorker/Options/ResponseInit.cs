using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.ServiceWorker;

public class ResponseInit
{
    [JsonPropertyName("status")]
    public short Status { get; set; } = 200;

    [JsonPropertyName("headers")]
    public Dictionary<string, string> Headers { get; set; } = new();
}