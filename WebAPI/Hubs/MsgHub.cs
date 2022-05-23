using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Hubs
{
    public class MsgHub : Hub
    {
        public async Task SentMessage(string msg)
        {
            await Clients.All.SendAsync("ReciveMessage", msg);
        }

    }
}
