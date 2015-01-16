using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using RoofSharing.Web.Infrastructure.SignalR;
using Roofsharing.Services.Common.Notifiers;

namespace RoofSharing.Web.Hubs
{
    public class SignalRNotifierServiceHub : Hub, INotifierService
    {
        private readonly static ConnectionMapping<string> connections =
            new ConnectionMapping<string>();
        public SignalRNotifierServiceHub(IHubConnectionContext<dynamic> clients)
        {
            this.Clients = clients;
        }

        public IHubConnectionContext<dynamic> Clients { get; private set; }

        public void Notify(string message, NotificationMessageType messageType, string userName)
        {
           foreach (var connectionId in connections.GetConnections(userName))
            {
                switch (messageType)
                {
                    case NotificationMessageType.Error:
                         Clients.Client(connectionId).errorMessage(message);
                        break;
                    case NotificationMessageType.Success:
                         Clients.Client(connectionId).successMessage(message);
                        break;
                    case NotificationMessageType.Warning:
                         Clients.Client(connectionId).warningMessage(message);
                        break;
                    case NotificationMessageType.Info:
                         Clients.Client(connectionId).infoMessage(message);
                        break;
                    default: throw new NotImplementedException();
                }
               
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