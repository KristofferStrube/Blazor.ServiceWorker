using System.ComponentModel;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.ServiceWorker;

[JsonConverter(typeof(EnumDescriptionConverter<ServiceWorkerState>))]
public enum ServiceWorkerState
{
    [Description("parsed")]
    Parsed,
    [Description("installing")]
    Installing,
    [Description("installed")]
    Installed,
    [Description("activating")]
    Activating,
    [Description("activated")]
    Activated,
    [Description("redundant")]
    Redundant,
}
