using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Hubs
{
    public class ChatHub : Hub
    {
        private static string groupname;
        private static int number = 0;
        private readonly LinkedList<string> users = new LinkedList<string>();
        public async Task Send(string message, string userName, string employerName, string role)
        {
            if (!string.IsNullOrEmpty(role)) groupname = userName + " " + employerName; 
            if(number != 2)
            {
                users.AddLast(Context.ConnectionId);
                await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
                number++;
            }
            await Clients.Group(groupname).SendAsync("Send", message, userName);
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            foreach (var id in users)
                Groups.RemoveFromGroupAsync(id, groupname);
            number = 0;
            users.Clear();
            return base.OnDisconnectedAsync(exception);
        }
    }
}
