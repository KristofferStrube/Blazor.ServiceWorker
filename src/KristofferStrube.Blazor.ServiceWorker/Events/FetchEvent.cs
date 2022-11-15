using KristofferStrube.Blazor.ServiceWorker.Extensions;
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
        IJSObjectReference helper = await helperTask.Value;
        Guid objectId = await helper.GetProxyAttributeAsProxy(container, Id, "request");
        return new Request(jSRuntime, objectId, container);
    }

    public async Task RespondWithAsync(Func<Task<Response>> r)
    {
        var response = await r();
        IJSObjectReference helper = await helperTask.Value;
        await helper.InvokeVoidAsync("resolveRespondWith", container.JSReference, Id, new string[] { response.Id.ToString() });
    }
}