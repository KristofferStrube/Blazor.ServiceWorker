using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class ExtendableEvent : BaseJSProxy
{
    public ExtendableEvent(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }
    public async Task WaitUntil(Func<Task> f)
    {
        await f();
        await ResolveProxy(Id);
    }
}