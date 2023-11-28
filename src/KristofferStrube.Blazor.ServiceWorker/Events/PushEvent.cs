using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class PushEvent : BaseJSProxy
{
    public PushEvent(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task<PushMessageDataProxy> GetDataAsync()
    {
        await container.StartMessagesAsync();
        Guid objectId = await GetProxyAttributeAsProxy("data");
        return new PushMessageDataProxy(jSRuntime, objectId, container);
    }
}