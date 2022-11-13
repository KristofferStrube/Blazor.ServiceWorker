using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.ServiceWorker;

public class RegistrationOptions
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("scope")]
    public string? Scope { get; set; }

    [JsonPropertyName("type")]
    public WorkerType Type { get; set; } = WorkerType.Classic;

    [JsonPropertyName("updateViaCache")]
    public ServiceWorkerUpdateViaCache UpdateViaCache { get; set; } = ServiceWorkerUpdateViaCache.Imports;
}
