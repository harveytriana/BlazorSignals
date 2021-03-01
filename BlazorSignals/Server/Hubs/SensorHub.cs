using BlazorSignals.Shared;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BlazorSignals.Server.Hubs
{
    public class SensorHub : Hub
    {
        public Task Broadcast(string sender, Pulse pulse)
        {
            return Clients
                .Others // do not Broadcast to Caller:
                .SendAsync("Broadcast", sender, pulse);
        }
    }
}
