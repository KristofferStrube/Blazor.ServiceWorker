using KristofferStrube.Blazor.ServiceWorker.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class ServiceWorkerGlobalScopeProxy : BaseJSServiceWorkerGlobalScopeProxy
{
    public ServiceWorkerGlobalScopeProxy(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public Func<Task>? OnInstall { get; set; }

    public Func<Task>? OnActivate { get; set; }

    public Func<FetchEvent, Task>? OnFetch { get; set; }

    public Func<PushEvent, Task>? OnPush { get; set; }

    public async Task<ClientsProxy> GetClientsAsync()
    {
        return await this.MemoizedTask(async () =>
    {
        Guid objectId = await GetProxyAttributeAsProxy("clients");
        return new ClientsProxy(jSRuntime, objectId, container);
    });
    }

    public async Task SkipWaitingAsync()
    {
        await CallProxyAsyncMethodAsNullableProxy("skipWaiting");
    }

    public async Task<CacheStorageProxy> GetCachesAsync()
    {
        return await this.MemoizedTask(async () =>
    {
        Guid objectId = await GetProxyAttributeAsProxy("caches");
        return new CacheStorageProxy(jSRuntime, objectId, container);
    });
    }

    public async Task<ResponseProxy> FetchAsync(RequestInfo input, RequestInit? init = null)
    {
        Guid objectId = await CallProxyAsyncMethodAsProxy("fetch", new object[] { (string)input, init });
        return new ResponseProxy(jSRuntime, objectId, container);
    }

    public async Task<ResponseProxy> ConstructResponse(string body, ResponseInit? init = null)
    {
        Guid objectId = await CallProxyConstructorAsProxy("Response", body, init);
        return new ResponseProxy(jSRuntime, objectId, container);
    }

    public async Task<ResponseProxy> ConstructResponse(ReadableStreamProxy body, ResponseInit? init = null)
    {
        Guid objectId = await CallProxyConstructorAsProxy("Response", body.Id, init);
        return new ResponseProxy(jSRuntime, objectId, container);
    }
}
