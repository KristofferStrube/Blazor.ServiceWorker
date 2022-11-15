using KristofferStrube.Blazor.ServiceWorker.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class Cache : BaseJSServiceWorkerGlobalScopeProxy
{
    internal Cache(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task AddAsync(RequestInfo request)
    {
        await container.StartMessagesAsync();
        IJSObjectReference helper = await helperTask.Value;
        await helper.CallProxyAsyncMethodAsProxy(container, Id, "add", new string[] { request });
    }
}