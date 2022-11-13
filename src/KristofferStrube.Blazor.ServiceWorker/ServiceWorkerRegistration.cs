using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class ServiceWorkerRegistration : BaseJSWrapper
{
    internal ServiceWorkerRegistration(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    public async Task<ServiceWorker?> GetActiveAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference? jSInstance = await helper.InvokeAsync<IJSObjectReference?>("getAttributeAsync", JSReference, "active");
        if (jSInstance is null)
        {
            return null;
        }

        return new ServiceWorker(jSRuntime, jSInstance);
    }
}