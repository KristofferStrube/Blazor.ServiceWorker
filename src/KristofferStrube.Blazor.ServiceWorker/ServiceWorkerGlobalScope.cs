using KristofferStrube.Blazor.ServiceWorker.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class ServiceWorkerGlobalScope : BaseJSServiceWorkerGlobalScopeProxy
{
    internal ServiceWorkerGlobalScope(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public Func<Task>? OnInstall { get; set; }

    public Func<Task>? OnActivate { get; set; }

    public Func<FetchEvent, Task>? OnFetch { get; set; }

    public Func<PushEvent, Task>? OnPush { get; set; }
}
