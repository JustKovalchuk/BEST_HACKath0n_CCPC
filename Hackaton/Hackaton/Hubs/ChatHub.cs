using Microsoft.AspNetCore.SignalR;

namespace Hackaton.Hubs
{
    public class ChatHub : Hub
    {
        //public async Task SendMessage(string user, string message)
        //{
        //    Clients.All.SendAsync("ReceiveMessage", user, message);
        //}
        
        public async Task SendMessage(string user, string message, string receiver)
        {
            // Формуємо назву групи на основі імені користувача і отримувача
            string groupName = $"{Context.User.Identity.Name}-{receiver}";

            // Відправляємо повідомлення тільки учасникам цієї групи
            await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
        }

        public async Task JoinChat(string receiver)
        {
            // Приєднуємо користувача до групи чату
            string groupName = $"{Context.User.Identity.Name}-{receiver}";
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveChat(string receiver)
        {
            // Покидаємо користувача з групи чату
            string groupName = $"{Context.User.Identity.Name}-{receiver}";
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
