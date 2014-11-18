using AutoMapper;
using RoofSharing.Data.Models;
using RoofSharing.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoofSharing.Web.ViewModels.Friends
{
    public class FriendshipStatusViewModel
    {
        public string UserId { get; set; }

        public FriendshipStatusTypeViewModel Status { get; set; }
       
    }
}