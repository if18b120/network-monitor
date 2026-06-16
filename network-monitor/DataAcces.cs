using System.Net;
using Microsoft.Data.Sqlite;

public class DataAccess
{
    SqliteConnection sqliteConnection;

    public DataAccess(string dbLocation)
    {
        sqliteConnection = new SqliteConnection("Data Source=" + dbLocation + "network-monitor.db");
        sqliteConnection.Open();

        SqliteCommand command = sqliteConnection.CreateCommand();
        command.CommandText = """
                                CREATE TABLE IF NOT EXISTS EVENTS (START, END, STATUS)
                            """;
        command.ExecuteNonQuery();
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