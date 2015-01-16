using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofSharing.Web.Infrastructure.Notifier
{
    public interface INotifierService
    {
        void Notify(string message, NotificationMessageType messageType, string userName);
    }
}