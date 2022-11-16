using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class Request : BaseJSServiceWorkerGlobalScopeProxy
{
    internal Request(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task<string> GetURLAsync()
    {
        return await GetProxyAttribute<string>("url");
    }
}