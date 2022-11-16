using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class PushEvent : BaseJSServiceWorkerGlobalScopeProxy
{
    internal PushEvent(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task<PushMessageData> GetDataAsync()
    {
        await container.StartMessagesAsync();
        Guid objectId = await GetProxyAttributeAsProxy("data");
        return new PushMessageData(jSRuntime, objectId, container);
    }
}