using AikidoMonkeyPatching;
using HarmonyLib;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main()
    {
        // Prepare the SQL connection and command with just minimal setup
        using var conn = new SqlConnection("Server=SERVER;Database=DB;Trusted_Connection=True;");
        conn.Open();
        using var cmd = new SqlCommand("SELECT 1", conn);

        // Patch ExecuteNonQuery method using "True Monkey Patching"
        // This just gets ignored by the CLR, stuff like this is generally prevented from .NET Core onwards
        TrueMonkeyPatching.MonkeyPatch();

        // Patch ExecuteNonQuery method using Harmony
        // This is a more controlled way to patch methods
        // Still not recommended for production code
        var harmony = new Harmony("example.sqlpatch");
        harmony.PatchAll();

        // More controlled and better alternative to monkey patching
        // Of course, this doesn't let you replace the method at the IL level
        // But it allows you to intercept calls and execute custom logic without changing the original method
        DiagnosticListener.Setup();

        cmd.ExecuteNonQuery();

        // And finally, what about a simple decorator? :)
        using var decoratedCmd = new SqlCommandDecorator(cmd);

        decoratedCmd.ExecuteNonQuery();
    }
}