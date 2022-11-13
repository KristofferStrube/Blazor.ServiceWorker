using KristofferStrube.Blazor.ServiceWorker.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class BaseJSServiceWorkerGlobalScopeProxy
{
    protected readonly IJSRuntime jSRuntime;
    protected readonly Lazy<Task<IJSObjectReference>> helperTask;
    protected readonly ServiceWorkerContainer container;

    internal BaseJSServiceWorkerGlobalScopeProxy(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container)
    {
        this.jSRuntime = jSRuntime;
        helperTask = new(jSRuntime.GetHelperAsync);
        this.container = container;
        Id = id;
    }

    public Guid Id { get; set; }
}
