using Microsoft.Data.SqlClient;

namespace AikidoMonkeyPatching;

public class SqlCommandDecorator : IDisposable
{
    private readonly SqlCommand _sqlCommand;

    public SqlCommandDecorator(SqlCommand sqlCommand)
    {
        _sqlCommand = sqlCommand;
    }

    public int ExecuteNonQuery()
    {
        Console.WriteLine("[Decorator] Intercepted SQL: " + _sqlCommand.CommandText);
        return _sqlCommand.ExecuteNonQuery();
    }


    public void Dispose()
    {
        _sqlCommand.Dispose();
    }
}
