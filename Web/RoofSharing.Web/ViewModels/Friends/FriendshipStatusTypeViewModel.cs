using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoofSharing.Web.ViewModels.Friends
{
    public enum FriendshipStatusTypeViewModel
    {
        None,
        Friends,
        AwaitingTheirResponse,
        AwaitingMyResponse,
        DeniedByMe,
        DeniedByThem
    }
}