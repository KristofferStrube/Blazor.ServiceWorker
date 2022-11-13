using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class CacheStorage : BaseJSServiceWorkerGlobalScopeProxy
{
    internal CacheStorage(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task<Response?> MatchAsync(RequestInfo request, MultiCacheQueryOptions? options = null)
    {
        await container.StartMessagesAsync();
        IJSObjectReference helper = await helperTask.Value;
        string objectId = await helper.InvokeAsync<string>("callProxyAsyncMethodAsProxy", container.JSReference, Id, "match", new object[] { (request.type is RequestInfoType.Request ? request.request.Id : request.stringRequest), options });
        bool isUndefined = await helper.InvokeAsync<bool>("isUndefined", objectId);
        if (isUndefined)
        {
            return null;
        }
        return new Response(jSRuntime, Guid.Parse(objectId), container);
    }

    public async Task<Cache> OpenAsync(string cacheName)
    {
        await container.StartMessagesAsync();
        IJSObjectReference helper = await helperTask.Value;
        string objectId = await helper.InvokeAsync<string>("callProxyAsyncMethodAsProxy", container.JSReference, Id, "open", new object[] { cacheName });
        return new Cache(jSRuntime, Guid.Parse(objectId), container);
    }
}