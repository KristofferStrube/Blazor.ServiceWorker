using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class ServiceWorkerRegistration : BaseJSWrapper, IServiceWorkerRegistration
{
    internal ServiceWorkerRegistration(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    public async Task<IServiceWorker?> GetInstallingAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference? jSInstance = await helper.InvokeAsync<IJSObjectReference?>("getAttribute", JSReference, "installing");
        if (jSInstance is null)
        {
            return null;
        }
        return new ServiceWorker(jSRuntime, jSInstance);
    }

    public async Task<IServiceWorker?> GetWaitingAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference? jSInstance = await helper.InvokeAsync<IJSObjectReference?>("getAttribute", JSReference, "waiting");
        if (jSInstance is null)
        {
            return null;
        }
        return new ServiceWorker(jSRuntime, jSInstance);
    }

    public async Task<IServiceWorker?> GetActiveAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference? jSInstance = await helper.InvokeAsync<IJSObjectReference?>("getAttribute", JSReference, "active");
        if (jSInstance is null)
        {
            return null;
        }
        return new ServiceWorker(jSRuntime, jSInstance);
    }
}