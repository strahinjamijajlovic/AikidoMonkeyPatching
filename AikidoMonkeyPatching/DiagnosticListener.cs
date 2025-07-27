using Microsoft.Data.SqlClient;

namespace AikidoMonkeyPatching;

internal class DiagnosticListener
{
    public static void Setup()
    {
        System.Diagnostics.DiagnosticListener.AllListeners.Subscribe(new DiagnosticListenerObserver());
    }

    class DiagnosticListenerObserver : IObserver<System.Diagnostics.DiagnosticListener>
    {
        public void OnNext(System.Diagnostics.DiagnosticListener listener)
        {
            if (listener.Name == "SqlClientDiagnosticListener") //don't hard-code this
            {
                listener.Subscribe(new SqlClientEventObserver());
            }
        }
        public void OnError(Exception error) { }
        public void OnCompleted() { }
    }

    class SqlClientEventObserver : IObserver<KeyValuePair<string, object>>
    {
        public void OnNext(KeyValuePair<string, object> evt)
        {
            if (evt.Key == "Microsoft.Data.SqlClient.WriteCommandBefore") //or this :)
            {
                var command = evt.Value.GetType().GetProperty("Command")?.GetValue(evt.Value) as SqlCommand;
                if (command != null)
                {
                    Console.WriteLine($"[DiagnosticListener] SQL: {command.CommandText}");
                }
            }
        }
        public void OnError(Exception error) { }
        public void OnCompleted() { }
    }
}
