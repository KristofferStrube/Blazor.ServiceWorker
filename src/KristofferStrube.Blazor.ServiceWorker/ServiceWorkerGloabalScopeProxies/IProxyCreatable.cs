using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker.ServiceWorkerGloabalScopeProxies;

public interface IProxyCreatable<TProxy> where TProxy : IProxyCreatable<TProxy>
{
    public static abstract TProxy Create(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container);
}
