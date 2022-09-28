using BoardRoomSystem.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Build.Evaluation;

namespace BoardRoomSystem.Hubs
{
    public class EventHub : Hub
    {
        public async Task SendAddeEvent(bool EventId)
        {
            await Clients.Others.SendAsync("ReciveAddEvent", EventId);
        }
        public async Task SendUpdateEvent(bool EventId)
        {
            await Clients.Others.SendAsync("ReciveEvent", EventId);
        }

        public async Task DeleteEvent(bool EventId)
        {
            await Clients.Others.SendAsync("ReciveDeleteEvent", EventId);
        }
    }
}
