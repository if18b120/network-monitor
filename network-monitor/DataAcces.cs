using System.Net;
using Microsoft.Data.Sqlite;

public class DataAccess
{
    SqliteConnection sqliteConnection;

    public DataAccess(string dbLocation)
    {
        sqliteConnection = new SqliteConnection(dbLocation + "network-monitor.db");
        sqliteConnection.Open();
    }

    public IPAddress GetIP()
    {
        SqliteCommand command = sqliteConnection.CreateCommand();
        command.CommandText = """
                                SELECT IP
                                FROM CONFIG
                            """;
        SqliteDataReader reader = command.ExecuteReader();
        reader.Read();
        return IPAddress.Parse(reader.GetString(0));
    }

    public void SaveErrorEvent(ErrorEvent errorEvent)
    {
        SqliteCommand command = sqliteConnection.CreateCommand();
        command.CommandText = """
                                INSERT INTO EVENTS (START, END, STATUS)
                                VALUES (@start, @end, @status)
                            """;
        command.Parameters.AddWithValue("@start", errorEvent.Start);
        command.Parameters.AddWithValue("@end", errorEvent.End);
        command.Parameters.AddWithValue("@status", errorEvent.Status.ToString());
        command.ExecuteNonQuery();
    }
}