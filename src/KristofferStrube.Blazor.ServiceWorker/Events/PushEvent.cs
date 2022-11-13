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
        string objectId = await helper.InvokeAsync<string>("getProxyAttributeAsProxy", container.JSReference, Id, "data");
        return new PushMessageData(jSRuntime, Guid.Parse(objectId), container);
    }
}