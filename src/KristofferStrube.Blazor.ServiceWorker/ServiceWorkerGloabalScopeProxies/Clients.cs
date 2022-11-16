using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class Clients : BaseJSServiceWorkerGlobalScopeProxy
{
    internal Clients(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task ClaimAsync()
    {
        await CallProxyAsyncMethodAsNullableProxy("claim");
    }
}
