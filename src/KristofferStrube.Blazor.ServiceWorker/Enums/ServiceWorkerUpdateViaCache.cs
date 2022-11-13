using System.ComponentModel;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.ServiceWorker;

[JsonConverter(typeof(EnumDescriptionConverter<ServiceWorkerUpdateViaCache>))]
public enum ServiceWorkerUpdateViaCache
{
    [Description("imports")]
    Imports,
    [Description("all")]
    All,
    [Description("none")]
    None,
}
