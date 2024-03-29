﻿using KristofferStrube.Blazor.ServiceWorker.ServiceWorkerGloabalScopeProxies;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class JsonProxy : BaseJSProxy
{
    public JsonProxy(IJSRuntime jSRuntime, Guid id, ServiceWorkerContainer container) : base(jSRuntime, id, container)
    {
    }

    public async Task<string> AsStringAsync()
    {
        return await CallProxyMethod<string>("valueOf");
    }

    public async Task<JsonProxy> GetAttributeProxyAsync(string attribute)
    {
        Guid objectId = await GetProxyAttributeAsProxy(attribute);
        return new JsonProxy(jSRuntime, objectId, container);
    }

    public async Task<T> GetAttributeProxyAsync<T>(string attribute) where T : IProxyCreatable<T>
    {
        Guid objectId = await GetProxyAttributeAsProxy(attribute);
        return T.Create(jSRuntime, objectId, container);
    }

    public async Task<string> GetAttributeAsStringAsync(string attribute)
    {
        return await GetProxyAttribute<string>(attribute);
    }
}