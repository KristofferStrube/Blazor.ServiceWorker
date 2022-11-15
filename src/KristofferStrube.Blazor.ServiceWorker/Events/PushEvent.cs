using KristofferStrube.Blazor.ServiceWorker.Extensions;
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
        IJSObjectReference helper = await helperTask.Value;
        Guid objectId = await helper.GetProxyAttributeAsProxy(container, Id, "data");
        return new PushMessageData(jSRuntime, objectId, container);
    }
}