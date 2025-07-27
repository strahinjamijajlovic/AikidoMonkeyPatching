using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AikidoMonkeyPatching;

internal class TrueMonkeyPatching
{
    private delegate int ExecuteNonQueryDelegate(SqlCommand _this);

    private static ExecuteNonQueryDelegate? _original;

    public static void MonkeyPatch()
    {
        var method = typeof(SqlCommand).GetMethod("ExecuteNonQuery", BindingFlags.Public | BindingFlags.Instance);
        var replacement = typeof(TrueMonkeyPatching).GetMethod(nameof(Replacement), BindingFlags.NonPublic | BindingFlags.Static);

        Prepare(method);
        Prepare(replacement);

        ReplaceMethod(method, replacement);
    }

    private static int Replacement(SqlCommand _this)
    {
        Console.WriteLine("Intercepted SQL: " + _this.CommandText);
        return _original(_this);
    }

    private static void Prepare(MethodInfo method)
    {
        RuntimeHelpers.PrepareMethod(method.MethodHandle);
    }

    private static unsafe void ReplaceMethod(MethodInfo target, MethodInfo replacement)
    {
        var inj = (ulong*)target.MethodHandle.Value.ToPointer() + 1;
        var rep = (ulong*)replacement.MethodHandle.Value.ToPointer() + 1;

        _original = (ExecuteNonQueryDelegate)Marshal.GetDelegateForFunctionPointer((IntPtr)(*inj), typeof(ExecuteNonQueryDelegate));
        *inj = *rep;
    }
}
