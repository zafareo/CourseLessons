using ClientWebAPI.Models;
using Microsoft.AspNetCore.SignalR;

namespace ClientWebAPI.HUB
{
    public class ChatHub : Hub
    {
        public async Task GetMessage(Message message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
            await Console.Out.WriteLineAsync(message.Body);
        }
    }
}
