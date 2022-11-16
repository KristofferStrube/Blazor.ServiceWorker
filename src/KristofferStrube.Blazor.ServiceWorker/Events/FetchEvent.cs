using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class FetchEvent : BaseJSServiceWorkerGlobalScopeProxy
{
    internal FetchEvent(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task<Request> GetRequestAsync()
    {
        await container.StartMessagesAsync();
        Guid objectId = await GetProxyAttributeAsProxy("request");
        return new Request(jSRuntime, objectId, container);
    }

    public async Task RespondWithAsync(Func<Task<Response>> r)
    {
        Response response = await r();
        await ResolveProxy(Id, response.Id.ToString());
    }
}