using KristofferStrube.Blazor.ServiceWorker.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class Request : BaseJSServiceWorkerGlobalScopeProxy
{
    internal Request(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task<string> GetURLAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.GetProxyAttribute<string>(container, Id, "url");
    }
}