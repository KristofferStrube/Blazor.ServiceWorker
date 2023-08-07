using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class CacheProxy : BaseJSServiceWorkerGlobalScopeProxy
{
    public CacheProxy(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task AddAsync(RequestInfo request)
    {
        await container.StartMessagesAsync();
        await CallProxyAsyncMethodAsNullableProxy("add", new string[] { request });
    }
}