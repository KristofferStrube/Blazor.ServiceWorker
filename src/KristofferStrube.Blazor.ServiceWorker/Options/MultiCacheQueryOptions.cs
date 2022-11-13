using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.ServiceWorker;

public class MultiCacheQueryOptions : CacheQueryOptions
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("cacheName")]
    public string? CacheName { get; set; }
}