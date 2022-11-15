using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker.Extensions;

internal static class IJSObjectReferenceExtensions
{
    internal static async Task<Guid> GetProxyAttributeAsProxy(this IJSObjectReference helper, ServiceWorkerContainer container, Guid objectid, string attribute)
    {
        return Guid.Parse(await helper.InvokeAsync<string>("getProxyAttributeAsProxy", container.JSReference, Guid.NewGuid(), objectid, attribute));
    }
    internal static async Task<Guid> GetProxyAsyncAttributeAsProxy(this IJSObjectReference helper, ServiceWorkerContainer container, Guid objectid, string attribute)
    {
        return Guid.Parse(await helper.InvokeAsync<string>("getProxyAsyncAttributeAsProxy", container.JSReference, Guid.NewGuid(), objectid, attribute));
    }
    internal static async Task<T> GetProxyAttribute<T>(this IJSObjectReference helper, ServiceWorkerContainer container, Guid objectid, string attribute)
    {
        return await helper.InvokeAsync<T>("getProxyAttribute", container.JSReference, Guid.NewGuid(), objectid, attribute);
    }
    internal static async Task<Guid> CallProxyMethodAsProxy(this IJSObjectReference helper, ServiceWorkerContainer container, Guid objectid, string method, object[]? args = null)
    {
        args ??= Array.Empty<object>();
        return Guid.Parse(await helper.InvokeAsync<string>("callProxyMethodAsProxy", container.JSReference, Guid.NewGuid(), objectid, method, args));
    }
    internal static async Task<Guid> CallProxyAsyncMethodAsProxy(this IJSObjectReference helper, ServiceWorkerContainer container, Guid objectid, string method, object[]? args = null)
    {
        args ??= Array.Empty<object>();
        return Guid.Parse(await helper.InvokeAsync<string>("callProxyAsyncMethodAsProxy", container.JSReference, Guid.NewGuid(), objectid, method, args));
    }
    internal static async Task<Guid?> CallProxyAsyncMethodAsNullableProxy(this IJSObjectReference helper, ServiceWorkerContainer container, Guid objectid, string method, object[]? args = null)
    {
        args ??= Array.Empty<object>();
        string proxyObjectId = await helper.InvokeAsync<string>("callProxyAsyncMethodAsProxy", container.JSReference, Guid.NewGuid(), objectid, method, args);
        bool isUndefined = await helper.InvokeAsync<bool>("isUndefined", proxyObjectId);
        if (isUndefined)
        {
            return null;
        }
        return Guid.Parse(proxyObjectId);
    }
    internal static async Task<T> CallProxyMethod<T>(this IJSObjectReference helper, ServiceWorkerContainer container, Guid objectid, string method, object[]? args = null)
    {
        args ??= Array.Empty<object>();
        return await helper.InvokeAsync<T>("callProxyMethod", container.JSReference, Guid.NewGuid(), objectid, method, args);
    }
}
