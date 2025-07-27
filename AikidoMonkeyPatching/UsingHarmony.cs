using HarmonyLib;
using Microsoft.Data.SqlClient;

namespace AikidoMonkeyPatching;

[HarmonyPatch]
internal class HarmonyPatches
{
    [HarmonyPatch(typeof(SqlCommand), nameof(SqlCommand.ExecuteNonQuery))]
    [HarmonyPrefix]
    static bool Prefix_ExecuteNonQuery(SqlCommand __instance, ref int __result)
    {
        Console.WriteLine("[Prefix][ExecuteNonQuery] SQL: " + __instance.CommandText);
        __result = 0; // You can set the result to a specific value if needed
        return false; // Skip original method execution
    }

    [HarmonyPatch(typeof(SqlCommand), nameof(SqlCommand.ExecuteNonQuery))]
    [HarmonyPostfix]
    static void Postfix_ExecuteNonQuery(int __result)
    {
        Console.WriteLine("[Postfix][ExecuteNonQuery] Result: " + __result);
    }
}
