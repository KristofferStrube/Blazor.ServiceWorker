namespace KristofferStrube.Blazor.ServiceWorker
{
    public interface IServiceWorker
    {
        Func<Task> OnStateChange { get; set; }

        Task<string> GetScriptURLAsync();
        Task<ServiceWorkerState> GetStateAsync();
        Task InvokeOnStateChange();
    }
}