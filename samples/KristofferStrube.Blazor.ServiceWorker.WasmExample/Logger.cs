namespace KristofferStrube.Blazor.ServiceWorker.WasmExample
{
    public class Logger
    {
        private string log;

        public void WriteLine(string message)
        {
            log = DateTime.UtcNow.ToLongTimeString() + ": " + message + "\n" + log;
            if (OnChange is not null)
            {
                OnChange.Invoke();
            }
        }

        public Action? OnChange { get; set; }

        public string Log => log;
    }
}
