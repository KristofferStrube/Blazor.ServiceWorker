using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class ClientsProxy : BaseJSServiceWorkerGlobalScopeProxy
{
    public ClientsProxy(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task ClaimAsync()
    {
        await CallProxyAsyncMethodAsNullableProxy("claim");
    }
}
