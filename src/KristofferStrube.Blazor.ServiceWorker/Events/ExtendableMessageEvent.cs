using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class ExtendableMessageEvent : ExtendableEvent
{
    public ExtendableMessageEvent(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task<JsonProxy> GetDataAsync()
    {
        await container.StartMessagesAsync();
        Guid objectId = await GetProxyAttributeAsProxy("data");
        return new JsonProxy(jSRuntime, objectId, container);
    }
}