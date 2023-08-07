using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace KristofferStrube.Blazor.ServiceWorker.Extensions;

/// <remarks>Adopted from <see href="https://stackoverflow.com/a/53299290">this StackOverflow answer</see>.</remarks>
public static class MemoizerExtension
{
    public static ConditionalWeakTable<object, ConcurrentDictionary<string, object>> _weakCache = new();

    public static Task<TResult> MemoizedTask<TResult>(
        this object context,
        Func<Task<TResult>> f,
        [CallerMemberName] string? cacheKey = null)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(cacheKey);

        ConcurrentDictionary<string, object> methodCaches = _weakCache.GetOrCreateValue(context);

        ConcurrentDictionary<string, Task<TResult>> methodCache = (ConcurrentDictionary<string, Task<TResult>>)methodCaches
            .GetOrAdd(cacheKey, _ => new ConcurrentDictionary<string, Task<TResult>>());
        return methodCache.GetOrAdd(cacheKey, _ => f());
    }
}
