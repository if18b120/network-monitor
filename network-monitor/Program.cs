using System.Net;
using System.Net.NetworkInformation;

string path = "";
if (args.Length > 0)
{
    path = args[0];
}

if (path != "" && !Path.EndsInDirectorySeparator(path))
{
    path += "/";
}

DataAccess dataAccess = new(path);

IPAddress ip = IPAddress.Parse("8.8.8.8");
Ping ping = new();

DateTime start = DateTime.Now;
bool error = false;
IPStatus status = IPStatus.Unknown;
while (true)
{
    if (!error)
    {
        start = DateTime.Now;
    }
    PingReply pingReply = ping.Send(ip);
    if (pingReply.Status != IPStatus.Success)
    {
        error = true;
        status = pingReply.Status;
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
                Status = status
            };
            dataAccess.SaveErrorEvent(errorEvent);
        }
        Thread.Sleep(500);
    }
}