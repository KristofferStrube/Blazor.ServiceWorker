using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class CacheStorageProxy : BaseJSServiceWorkerGlobalScopeProxy
{
    public CacheStorageProxy(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task<ResponseProxy?> MatchAsync(RequestInfo request, MultiCacheQueryOptions? options = null)
    {
        await container.StartMessagesAsync();
        Guid? objectId = await CallProxyAsyncMethodAsNullableProxy("match", new object[] { (string)request, options });
        if (objectId is Guid id)
        {
            return new ResponseProxy(jSRuntime, id, container);
        }
        return null;
    }

    public async Task<CacheProxy> OpenAsync(string cacheName)
    {
        await container.StartMessagesAsync();
        Guid objectId = await CallProxyAsyncMethodAsProxy("open", new object[] { cacheName });
        return new CacheProxy(jSRuntime, objectId, container);
    }
}