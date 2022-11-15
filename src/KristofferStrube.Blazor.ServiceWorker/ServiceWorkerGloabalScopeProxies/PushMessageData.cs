using KristofferStrube.Blazor.FileAPI;
using KristofferStrube.Blazor.ServiceWorker.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class PushMessageData : BaseJSServiceWorkerGlobalScopeProxy
{
    internal PushMessageData(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task<byte[]> ArrayBufferAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSInstance = await helper.CallProxyMethod<IJSObjectReference>(container, Id, "arrayBuffer");
        return await helper.InvokeAsync<byte[]>("arrayBuffer", jSInstance);
    }

    public async Task<Blob> BlobAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSInstance = await helper.CallProxyMethod<IJSObjectReference>(container, Id, "blob");
        return Blob.Create(jSRuntime, jSInstance);
    }

    public async Task<JsonProxy> JsonProxyAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        Guid objectId = await helper.CallProxyMethodAsProxy(container, Id, "json");
        return new JsonProxy(jSRuntime, objectId, container);
    }

    public async Task<string> TextAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.CallProxyMethod<string>(container, Id, "text");
    }
}
