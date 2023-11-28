using KristofferStrube.Blazor.ServiceWorker.Extensions;
using Microsoft.JSInterop;
using System;

namespace KristofferStrube.Blazor.ServiceWorker;

public abstract class BaseJSProxy
{
    protected readonly IJSRuntime jSRuntime;
    protected readonly Lazy<Task<IJSObjectReference>> helperTask;
    protected readonly ServiceWorkerContainer container;

    public BaseJSProxy(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container)
    {
        this.jSRuntime = jSRuntime;
        helperTask = new(jSRuntime.GetHelperAsync);
        this.container = container;
        Id = id;
    }

    public Guid Id { get; set; }

    public async Task<Guid> GetProxyAttributeAsProxy(string attribute)
    {
        IJSObjectReference helper = await helperTask.Value;
        return Guid.Parse(await helper.InvokeAsync<string>("getProxyAttributeAsProxy", container.JSReference, Guid.NewGuid(), Id, attribute));
    }

    public async Task<Guid> GetProxyAsyncAttributeAsProxy(string attribute)
    {
        IJSObjectReference helper = await helperTask.Value;
        return Guid.Parse(await helper.InvokeAsync<string>("getProxyAsyncAttributeAsProxy", container.JSReference, Guid.NewGuid(), Id, attribute));
    }

    public async Task<T> GetProxyAttribute<T>(string attribute)
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<T>("getProxyAttribute", container.JSReference, Guid.NewGuid(), Id, attribute);
    }

    public async Task<Guid> CallProxyMethodAsProxy(string method, params object[]? args)
    {
        args ??= Array.Empty<object>();
        IJSObjectReference helper = await helperTask.Value;
        return Guid.Parse(await helper.InvokeAsync<string>("callProxyMethodAsProxy", container.JSReference, Guid.NewGuid(), Id, method, args));
    }

    public async Task<Guid> CallProxyAsyncMethodAsProxy(string method, params object[]? args)
    {
        args ??= Array.Empty<object>();
        IJSObjectReference helper = await helperTask.Value;
        return Guid.Parse(await helper.InvokeAsync<string>("callProxyAsyncMethodAsProxy", container.JSReference, Guid.NewGuid(), Id, method, args));
    }

    public async Task<Guid?> CallProxyAsyncMethodAsNullableProxy(string method, params object[]? args)
    {
        args ??= Array.Empty<object>();
        IJSObjectReference helper = await helperTask.Value;
        string proxyObjectId = await helper.InvokeAsync<string>("callProxyAsyncMethodAsProxy", container.JSReference, Guid.NewGuid(), Id, method, args);
        bool isUndefined = await helper.InvokeAsync<bool>("isUndefined", proxyObjectId);
        if (isUndefined)
        {
            return null;
        }
        return Guid.Parse(proxyObjectId);
    }

    public async Task<T> CallProxyMethod<T>(string method, params object[]? args)
    {
        args ??= Array.Empty<object>();
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<T>("callProxyMethod", container.JSReference, Guid.NewGuid(), Id, method, args);
    }

    public async Task<Guid> CallProxyConstructorAsProxy(string name, params object[]? args)
    {
        args ??= Array.Empty<object>();
        IJSObjectReference helper = await helperTask.Value;
        return Guid.Parse(await helper.InvokeAsync<string>("callProxyConstructorAsProxy", container.JSReference, Guid.NewGuid(), Id, name, args));
    }

    public async Task ResolveProxy(Guid objectId, params object[]? args)
    {
        IJSObjectReference helper = await helperTask.Value;
        await helper.InvokeVoidAsync("resolveProxy", container.JSReference, objectId, args);
    }

    internal async Task InitialBlazorHandshake()
    {
        IJSObjectReference helper = await helperTask.Value;
        await helper.InvokeVoidAsync("initialBlazorHandshake", container.JSReference);
    }
}
