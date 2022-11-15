using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.ServiceWorker;

public class NavigationPreloadState
{
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    [JsonPropertyName("headerValue")]
    public byte[] HeaderValue { get; set; } = Array.Empty<byte>();
}
