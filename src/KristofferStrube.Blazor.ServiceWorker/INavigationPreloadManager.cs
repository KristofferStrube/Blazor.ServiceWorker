namespace KristofferStrube.Blazor.ServiceWorker
{
    public interface INavigationPreloadManager
    {
        Task DisableAsync();
        Task EnableAsync();
        Task<NavigationPreloadState> GetStateAsync();
        Task SetHeaderValueAsync(byte[] value);
    }
}