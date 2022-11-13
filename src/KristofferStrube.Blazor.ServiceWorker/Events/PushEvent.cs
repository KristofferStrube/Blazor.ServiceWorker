using Microsoft.JSInterop;
using System.ComponentModel;

namespace KristofferStrube.Blazor.ServiceWorker;

public class PushEvent : BaseJSServiceWorkerGlobalScopeProxy
{
    internal PushEvent(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }
    public async Task<PushMessageData> GetDataAsync()
    {
        await container.StartMessagesAsync();
        var helper = await helperTask.Value;
        var objectId = await helper.InvokeAsync<string>("getProxyAttributeAsProxy", container.JSReference, Id, "data");
        return new PushMessageData(jSRuntime, Guid.Parse(objectId), container);
    }
}