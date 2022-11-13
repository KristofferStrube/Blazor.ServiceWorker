using KristofferStrube.Blazor.FileAPI;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class PushMessageData : BaseJSServiceWorkerGlobalScopeProxy
{
    internal PushMessageData(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task<byte[]> ArrayBufferAsync()
    {
        var helper = await helperTask.Value;
        var jSInstance = await helper.InvokeAsync<IJSObjectReference>("callProxyMethod", container.JSReference, Id, "arrayBuffer");
        return await helper.InvokeAsync<byte[]>("arrayBuffer", jSInstance);
    }

    public async Task<Blob> BlobAsync()
    {
        var helper = await helperTask.Value;
        var jSInstance = await helper.InvokeAsync<IJSObjectReference>("callProxyMethod", container.JSReference, Id, "blob");
        return Blob.Create(jSRuntime, jSInstance);
    }

    public async Task<JsonProxy> JsonProxyAsync()
    {
        var helper = await helperTask.Value;
        var objectId = await helper.InvokeAsync<string>("callProxyMethodAsProxy", container.JSReference, Id, "json");
        return new JsonProxy(jSRuntime, Guid.Parse(objectId), container);
    }

    public async Task<string> TextAsync()
    {
        var helper = await helperTask.Value;
        return await helper.InvokeAsync<string>("callProxyMethod", container.JSReference, Id, "text");
    }
}
