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

    public async Task<CacheStorage> GetCachesAsync()
    {
        await container.StartMessagesAsync();
        IJSObjectReference helper = await helperTask.Value;
        string objectId = await helper.InvokeAsync<string>("getProxyAttributeAsProxy", container.JSReference, Id, "caches");
        return new CacheStorage(jSRuntime, Guid.Parse(objectId), container);
    }

    public async Task<Response> FetchAsync(RequestInfo input, RequestInit? init = null)
    {
        await container.StartMessagesAsync();
        IJSObjectReference helper = await helperTask.Value;
        string objectId = await helper.InvokeAsync<string>("callProxyAsyncMethodAsProxy", container.JSReference, Id, "fetch", new object[] { (input.type is RequestInfoType.Request ? input.request.Id.ToString() : input.stringRequest), init });
        return new Response(jSRuntime, Guid.Parse(objectId), container);
    }
}
