using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class Request : BaseJSServiceWorkerGlobalScopeProxy
{
    internal Request(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task<string> GetURLAsync()
    {
        await container.StartMessagesAsync();
        var helper = await helperTask.Value;
        return await helper.InvokeAsync<string>("getProxyAttribute", container.JSReference, Id, "url");
    }
}