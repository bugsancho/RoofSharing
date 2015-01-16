using Microsoft.AspNet.SignalR;
using RoofSharing.Web.Infrastructure.Notifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoofSharing.Web.Infrastructure.Extensions;

namespace RoofSharing.Web.Infrastructure.Notifier
{
    public class SignalRNotificationService : Hub, INotifierService
    {
        private readonly static ConnectionMapping<string> connections =
            new ConnectionMapping<string>();

         public void Notify( string message, NotificationMessageType messageType, string userName )
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