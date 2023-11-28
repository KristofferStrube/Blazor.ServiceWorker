using KristofferStrube.Blazor.ServiceWorker.ServiceWorkerGloabalScopeProxies;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class ReadableStreamProxy : BaseJSProxy, IProxyCreatable<ReadableStreamProxy>
{
    public ReadableStreamProxy(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public static ReadableStreamProxy Create(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container)
    {
        return new ReadableStreamProxy(jSRuntime, id, container);
    }
}
