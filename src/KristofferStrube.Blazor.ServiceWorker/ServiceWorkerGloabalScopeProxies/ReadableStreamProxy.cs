using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class ReadableStreamProxy : BaseJSServiceWorkerGlobalScopeProxy
{
    public ReadableStreamProxy(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }
}
