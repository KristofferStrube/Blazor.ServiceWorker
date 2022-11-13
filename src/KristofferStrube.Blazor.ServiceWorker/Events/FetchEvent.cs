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
        string objectId = await helper.InvokeAsync<string>("getProxyAttributeAsProxy", container.JSReference, Id, "request");
        return new Request(jSRuntime, Guid.Parse(objectId), container);
    }

    public async Task RespondWithAsync(Func<Task<Response>> r)
    {
        ResponsePromise rp = new ResponsePromise(r);
        IJSObjectReference helper = await helperTask.Value;
        await helper.InvokeVoidAsync("setupProxyPromise", container.JSReference, Id, "respondWith", rp.ObjRef);
    }
}