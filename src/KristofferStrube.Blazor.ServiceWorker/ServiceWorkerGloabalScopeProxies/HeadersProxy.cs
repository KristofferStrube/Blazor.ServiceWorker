using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class HeadersProxy : BaseJSServiceWorkerGlobalScopeProxy
{
    public HeadersProxy(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task AppendAsync(string name, string value)
    {
        await CallProxyMethod<bool>("append", name, value);
    }
}
