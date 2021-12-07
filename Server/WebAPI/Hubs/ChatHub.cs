using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebAPI
{
    public interface IChat
    {
        Task ReceiveMessage(string user, string message);
    }
    public class ChatHub : Hub<IChat>
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.ReceiveMessage(user, message);
        }
    }
}