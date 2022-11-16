using KristofferStrube.Blazor.ServiceWorker.Extensions;
using KristofferStrube.Blazor.ServiceWorker.Options;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class ServiceWorkerGlobalScope : BaseJSServiceWorkerGlobalScopeProxy
{
    internal ServiceWorkerGlobalScope(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public Func<Task>? OnInstall { get; set; }

    public Func<Task>? OnActivate { get; set; }

    public Func<FetchEvent, Task>? OnFetch { get; set; }

    public Func<PushEvent, Task>? OnPush { get; set; }

    public async Task<Clients> GetClientsAsync()
    {
        return await this.MemoizedTask(async () =>
    {
        Guid objectId = await GetProxyAttributeAsProxy("clients");
        return new Clients(jSRuntime, objectId, container);
    });
    }

    public async Task SkipWaitingAsync()
    {
        await CallProxyAsyncMethodAsNullableProxy("skipWaiting");
    }

    public async Task<CacheStorage> GetCachesAsync()
    {
        return await this.MemoizedTask(async () =>
    {
        Guid objectId = await GetProxyAttributeAsProxy("caches");
        return new CacheStorage(jSRuntime, objectId, container);
    });
    }

    public async Task<Response> FetchAsync(RequestInfo input, RequestInit? init = null)
    {
        Guid objectId = await CallProxyAsyncMethodAsProxy("fetch", new object[] { (string)input, init });
        return new Response(jSRuntime, objectId, container);
    }
}
