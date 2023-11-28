using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class ResponseProxy : BaseJSProxy
{
    public ResponseProxy(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task<ushort> GetStatusAsync()
    {
        return await GetProxyAttribute<ushort>("status");
    }

    public async Task<string> GetURLAsync()
    {
        return await GetProxyAttribute<string>("url");
    }

    public async Task<HeadersProxy> GetHeadersAsync()
    {
        var id = await GetProxyAttributeAsProxy("headers");
        return new HeadersProxy(jSRuntime, id, container);
    }

    public async Task<ReadableStreamProxy> GetBodyAsync()
    {
        var id = await GetProxyAttributeAsProxy("body");
        return new ReadableStreamProxy(jSRuntime, id, container);
    }
}