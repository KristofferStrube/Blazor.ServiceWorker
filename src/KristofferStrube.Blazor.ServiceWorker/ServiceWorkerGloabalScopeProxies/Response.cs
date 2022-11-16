using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class Response : BaseJSServiceWorkerGlobalScopeProxy
{
    internal Response(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task<ushort> GetStatusAsync()
    {
        return await GetProxyAttribute<ushort>("status");
    }

    public async Task<string> GetURLAsync()
    {
        return await GetProxyAttribute<string>("url");
    }
}