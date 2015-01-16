using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofSharing.Web.Infrastructure.SignalR
{
    public class NotificationHub : Hub
    {
        private readonly static ConnectionMapping<string> connections =
            new ConnectionMapping<string>();

        public void SendChatMessage(string userName, string message)
        {
            foreach (var connectionId in connections.GetConnections(userName))
            {
                Clients.Client(connectionId).addChatMessage(message);
            }
        }

        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;

            connections.Add(name, Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.User.Identity.Name;

            connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string name = Context.User.Identity.Name;

            if (!connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                connections.Add(name, Context.ConnectionId);
            }

            return base.OnReconnected();
        }
    }
}