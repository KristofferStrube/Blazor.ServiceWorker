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
        Guid? objectId = await CallProxyAsyncMethodAsNullableProxy("match", new object[] { (string)request, options });
        if (objectId is Guid id)
        {
            return new Response(jSRuntime, id, container);
        }
        return null;
    }

    public async Task<Cache> OpenAsync(string cacheName)
    {
        await container.StartMessagesAsync();
        Guid objectId = await CallProxyAsyncMethodAsProxy("open", new object[] { cacheName });
        return new Cache(jSRuntime, objectId, container);
    }
}