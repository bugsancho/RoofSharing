using System;
using System.Linq;
using Roofsharing.Services.Common.Notifiers;

namespace Roofsharing.Services.Notifiers
{
    public interface INotifierService
    {
        void Notify(string message, NotificationMessageType messageType, string userName);
    }
}