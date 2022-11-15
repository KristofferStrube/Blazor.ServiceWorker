using KristofferStrube.Blazor.ServiceWorker.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class JsonProxy : BaseJSServiceWorkerGlobalScopeProxy
{
    internal JsonProxy(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task<string> AsStringAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.CallProxyMethod<string>(container, Id, "valueOf");
    }

    public async Task<JsonProxy> GetAttributeProxyAsync(string attribute)
    {
        IJSObjectReference helper = await helperTask.Value;
        Guid objectId = await helper.GetProxyAttributeAsProxy(container, Id, attribute);
        return new JsonProxy(jSRuntime, objectId, container);
    }

    public async Task<string> GetAttributeAsStringAsync(string attribute)
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.GetProxyAttribute<string>(container, Id, attribute);
    }
}