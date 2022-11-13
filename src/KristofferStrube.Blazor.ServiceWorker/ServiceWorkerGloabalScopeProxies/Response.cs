using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class Response : BaseJSServiceWorkerGlobalScopeProxy
{
    internal Response(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task<ushort> GetStatusAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<ushort>("getProxyAttribute", container.JSReference, Id, "status");
    }
}