﻿using Microsoft.JSInterop;
using Microsoft.JSInterop.Infrastructure;

namespace KristofferStrube.Blazor.ServiceWorker;

public class ServiceWorkerContainer : BaseJSWrapper
{
    public ServiceWorkerContainer(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    public async Task<ServiceWorker?> GetControllerAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        if (await helper.InvokeAsync<bool>("isAttributeNullOrUndefined", JSReference, "controller"))
        {
            return null;
        }

        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "controller");
        return new ServiceWorker(JSRuntime, jSInstance);
    }

    public async Task<ServiceWorkerRegistration> GetReadyAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttributeAsync", JSReference, "ready");
        return new ServiceWorkerRegistration(JSRuntime, jSInstance);
    }

    public async Task<ServiceWorkerRegistration> RegisterAsync(string scriptURL, RegistrationOptions? options = null)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("register", scriptURL, options);
        return new ServiceWorkerRegistration(JSRuntime, jSInstance);
    }

    public async Task<ServiceWorkerRegistration> RegisterAsync(string scriptBootstrapperURL, string rootPath, Guid id, Func<ServiceWorkerGlobalScopeProxy, Task> script)
    {
        await ScriptManager.AddScriptAsync(id, JSRuntime, this, async (scope) =>
        {
            await script(scope);
            await scope.InitialBlazorHandshake();
        });
        IJSObjectReference helper = await helperTask.Value;
        await helper.InvokeVoidAsync("registerMessageListener", JSReference);
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("register", $"{scriptBootstrapperURL}?id={id}&root={rootPath}", new RegistrationOptions() { Type = WorkerType.Classic, UpdateViaCache = ServiceWorkerUpdateViaCache.Imports });
        var registration = new ServiceWorkerRegistration(JSRuntime, jSInstance);
        return registration;
    }

    public async Task<ServiceWorkerRegistration> GetRegistrationAsync(string clientURL = "")
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("getRegistration", clientURL);
        return new ServiceWorkerRegistration(JSRuntime, jSInstance);
    }

    public async Task StartMessagesAsync()
    {
        await JSReference.InvokeVoidAsync("startMessages");
    }
}