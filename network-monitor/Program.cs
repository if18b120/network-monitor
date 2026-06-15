using System.Net;
using System.Net.NetworkInformation;

string path = args[0];

if (!Path.EndsInDirectorySeparator(path))
{
    path += "/";
}

DataAccess dataAccess = new(path);

IPAddress ip = dataAccess.GetIP();
Ping ping = new();

DateTime start = DateTime.Now;
while (true)
{
    bool error = false;

    if (!error)
    {
        start = DateTime.Now;
    }
    PingReply pingReply = ping.Send(ip);
    if (pingReply.Status != IPStatus.Success)
    {
        error = true;
    }
    else
    {
        if (error)
        {
            DateTime end = DateTime.Now;
            error = false;
            ErrorEvent errorEvent = new()
            {
                Start = start,
                End = end,
                Status = pingReply.Status
            };
            dataAccess.SaveErrorEvent(errorEvent);
        }
        Thread.Sleep(100);
    }
}