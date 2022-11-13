using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class Cache : BaseJSServiceWorkerGlobalScopeProxy
{
    internal Cache(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task AddAsync(string request)
    {
        await container.StartMessagesAsync();
        IJSObjectReference helper = await helperTask.Value;
        await helper.InvokeVoidAsync("callProxyAsyncMethodAsProxy", container.JSReference, Id, "add", new string[] { request });
    }
}