using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class FetchEvent : BaseJSServiceWorkerGlobalScopeProxy
{
    internal FetchEvent(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task<Request> GetRequestAsync()
    {
        await container.StartMessagesAsync();
        var helper = await helperTask.Value;
        var objectId = await helper.InvokeAsync<string>("getProxyAttributeAsProxy", container.JSReference, Id, "request");
        return new Request(jSRuntime, Guid.Parse(objectId), container);
    }
}