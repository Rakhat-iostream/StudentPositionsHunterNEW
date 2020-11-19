using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ISPH.Infrastructure.Hubs
{
    public class ChatHub : Hub
    {
        private static string _groupname;
        private static int _number = 0;
        private readonly LinkedList<string> _users = new LinkedList<string>();
        public async Task Send(string message, string userName, string employerName, string role)
        {
            if (!string.IsNullOrEmpty(role) && role != "employer") _groupname = userName + " " + employerName; 
            if(_number != 2)
            {
                _users.AddLast(Context.ConnectionId);
                await Groups.AddToGroupAsync(Context.ConnectionId, _groupname);
                _number++;
            }
            await Clients.Group(_groupname).SendAsync("Send", message, userName);
        }

        public async Task SendResume(string url)
        {
            await Clients.Group(_groupname).SendAsync("SendResume", url);
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            foreach (var id in _users)
                Groups.RemoveFromGroupAsync(id, _groupname);
            _number = 0;
            _users.Clear();
            return base.OnDisconnectedAsync(exception);
        }
    }
}
