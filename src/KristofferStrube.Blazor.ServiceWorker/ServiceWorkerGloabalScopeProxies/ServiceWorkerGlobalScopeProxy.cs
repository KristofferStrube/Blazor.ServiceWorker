using KristofferStrube.Blazor.ServiceWorker.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class ServiceWorkerGlobalScopeProxy : BaseJSProxy
{
    public ServiceWorkerGlobalScopeProxy(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public Func<InstallEvent, Task>? OnInstall { get; set; }

    public Func<Task>? OnActivate { get; set; }

    public Func<FetchEvent, Task>? OnFetch { get; set; }

    public Func<PushEvent, Task>? OnPush { get; set; }

    public Func<ExtendableMessageEvent, Task>? OnMessage { get; set; }

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
        if (init is not null)
        {
            Guid objectId = await CallProxyAsyncMethodAsProxy(
                "fetch",
                new object[] {
                    (string)input,
                    new { 
                        method = init.Method,
                        body = (string)init.Body,
                        credentials = init.Credentials,
                        keepalive = init.KeepAlive,
                        duplex = init.Duplex
                    }
                });
            return new ResponseProxy(jSRuntime, objectId, container);
        }
        else
        {
            Guid objectId = await CallProxyAsyncMethodAsProxy("fetch", new object[] { (string)input});
            return new ResponseProxy(jSRuntime, objectId, container);
        }
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
