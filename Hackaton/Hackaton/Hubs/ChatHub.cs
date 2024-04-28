using Hackaton.Models.Chats;
using Hackaton.Models.User;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace Hackaton.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message, string chatid, string userid)
        {
            var chat = new Chat() { Id = Convert.ToInt32(chatid) };

            Console.WriteLine("SendMessage(string user, string message, string chatid, string userid)");
            var msg = new Message() { Username = user, Text = message};
            //await JoinChat(chatid);
            await SendMessageToChat(chat, msg);

        }

        public async Task SendMessageToChat(Chat chat, Message msg)
        {
            Console.WriteLine("SendMessage(Chat chat, Message msg)");
            // Формуємо назву групи на основі імені користувача і отримувача
            string groupName = $"{chat.Id}";

            // Відправляємо повідомлення тільки учасникам цієї групи
            await Clients.Group(groupName).SendAsync("ReceiveMessage", msg.Username, msg.Text);
        }

        public async Task JoinChat(string chatid)
        {
            Console.WriteLine($"JoinChat {chatid}");
            string groupName = $"{chatid}";
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveChat(string chatid)
        {
            Console.WriteLine($"LeaveChat {chatid}");
            string groupName = $"{chatid}";
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
