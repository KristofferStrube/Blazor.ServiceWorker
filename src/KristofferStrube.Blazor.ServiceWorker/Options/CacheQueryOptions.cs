using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.ServiceWorker;

public class CacheQueryOptions
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("ignoreSearch")]
    public bool IgnoreSearch { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("ignoreMethod")]
    public bool IgnoreMethod { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("ignoreVary")]
    public bool IgnoreVary { get; set; }
}