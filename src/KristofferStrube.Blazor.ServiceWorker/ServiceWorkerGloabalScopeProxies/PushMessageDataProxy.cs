using KristofferStrube.Blazor.FileAPI;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class PushMessageDataProxy : BaseJSProxy
{
    public PushMessageDataProxy(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task<byte[]> ArrayBufferAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSInstance = await CallProxyMethod<IJSObjectReference>("arrayBuffer");
        return await helper.InvokeAsync<byte[]>("arrayBuffer", jSInstance);
    }

    public async Task<Blob> BlobAsync()
    {
        IJSObjectReference jSInstance = await CallProxyMethod<IJSObjectReference>("blob");
        return Blob.Create(jSRuntime, jSInstance);
    }

    public async Task<JsonProxy> JsonProxyAsync()
    {
        Guid objectId = await CallProxyMethodAsProxy("json");
        return new JsonProxy(jSRuntime, objectId, container);
    }

    public async Task<string> TextAsync()
    {
        return await CallProxyMethod<string>("text");
    }
}
