using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class InstallEvent : ExtendableEvent
{
    public InstallEvent(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }
}