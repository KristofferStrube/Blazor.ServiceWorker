using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class JsonProxy : BaseJSServiceWorkerGlobalScopeProxy
{
    internal JsonProxy(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task<string> AsStringAsync()
    {
        var helper = await helperTask.Value;
        return await helper.InvokeAsync<string>("callProxyMethod", container.JSReference, Id, "valueOf");
    }

    public async Task<JsonProxy> GetAttributeProxyAsync(string attribute)
    {
        var helper = await helperTask.Value;
        var objectId = await helper.InvokeAsync<string>("getProxyAttributeProxy", container.JSReference, Id, attribute);
        return new JsonProxy(jSRuntime, Guid.Parse(objectId), container);
    }

    public async Task<string> GetAttributeAsStringAsync(string attribute)
    {
        var helper = await helperTask.Value;
        return await helper.InvokeAsync<string>("getProxyAttribute", container.JSReference, Id, attribute);
    }
}