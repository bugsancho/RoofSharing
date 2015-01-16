using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roofsharing.Services.Common.Notifiers
{
    public interface INotifier
    {
        void ShowSuccessMessage(string message);

        void ShowErrorMessage(string message);
    }
}