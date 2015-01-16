using System;
using System.Linq;

namespace Roofsharing.Services.Common.Notifiers
{
    public interface INotifierService
    {
        void Notify(string message, NotificationMessageType messageType, string userName);
    }
}