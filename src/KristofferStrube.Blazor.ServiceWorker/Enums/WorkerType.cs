using System.ComponentModel;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.ServiceWorker;

[JsonConverter(typeof(EnumDescriptionConverter<WorkerType>))]
public enum WorkerType
{
    [Description("classic")]
    Classic,
    [Description("module")]
    Module,
}
