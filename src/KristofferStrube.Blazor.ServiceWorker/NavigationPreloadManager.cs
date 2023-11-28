using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.ServiceWorker;

public class NavigationPreloadManager : BaseJSWrapper
{
    public NavigationPreloadManager(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    public async Task EnableAsync()
    {
        await JSReference.InvokeVoidAsync("enable");
    }

    public async Task DisableAsync()
    {
        await JSReference.InvokeVoidAsync("disable");
    }

    /// <summary>
    /// Sets the preload header value. The initial value is <c>'true'</c>.
    /// </summary>
    /// <param name="value">It is important that the value is a ByteString. This can be interpreted as UTF-8 string.</param>
    /// <remarks>Check out the C# 11 language feature called <seealso href="https://devblogs.microsoft.com/dotnet/csharp-11-preview-updates/#utf-8-string-literals">UTF-8 String Literals</seealso>.</remarks>
    public async Task SetHeaderValueAsync(byte[] value)
    {
        await JSReference.InvokeVoidAsync("setHeaderValue", value);
    }

    public async Task<NavigationPreloadState> GetStateAsync()
    {
        return await JSReference.InvokeAsync<NavigationPreloadState>("getState");
    }
}