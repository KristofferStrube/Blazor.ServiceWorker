using KristofferStrube.Blazor.ServiceWorker.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class Json : BaseJSWrapper, IJSCreatable<Json>
{
    public static async Task<Json> CreateAsync(IJSRuntime jSRuntime)
    {
        var helper = await jSRuntime.GetHelperAsync();
        var jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructJsonObject");
        return new(jSRuntime, jSInstance);
    }

    public static Task<Json> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new Json(jSRuntime, jSReference));
    }

    public Json(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    public async Task<string> AsStringAsync()
    {
        return await JSReference.InvokeAsync<string>("valueOf");
    }

    public async Task<Json> GetAttributeAsync(string attribute)
    {
        var helper = await helperTask.Value;
        var jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, attribute);
        return new Json(JSRuntime, jSInstance);
    }

    public async Task<string> GetAttributeAsStringAsync(string attribute)
    {
        var helper = await helperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, attribute);
    }

    public async Task SetAttributeAsync(string attribute, IJSObjectReference value)
    {
        var helper = await helperTask.Value;
        await helper.InvokeVoidAsync("setAttribute", JSReference, attribute, value);
    }

    public async Task SetAttributeAsync(string attribute, IJSWrapper value)
    {
        var helper = await helperTask.Value;
        await helper.InvokeVoidAsync("setAttribute", JSReference, attribute, value.JSReference);
    }

    public async Task SetAttributeAsync(string attribute, string value)
    {
        var helper = await helperTask.Value;
        await helper.InvokeVoidAsync("setAttribute", JSReference, attribute, value);
    }
}