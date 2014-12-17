using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoofSharing.Data;
using RoofSharing.Data.Models;
using RoofSharing.Web.ViewModels;
using RoofSharing.Web.ViewModels.Friends;
using RoofSharing.Web.Infrastructure.ValidationAttributes;

namespace RoofSharing.Web.Controllers
{
    public class FriendsController : BaseController
    {
        private const string UserFriendshipName = "UserFriendship";

        public FriendsController(IRoofSharingData data) : base(data)
        {
        }

        // GET: Friends
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult FriendPartial(string userId)
        {
            var friendship = this.GetFriendshipViewModel(userId);
            return PartialView("_FriendPartial", friendship);
        }
        
        [Authorize]
        [AjaxPost]
        public ActionResult AddFriend(string userId)
        {
            var friendship = this.GetFriendship(userId);
            friendship.Status = FriendshipStatusType.Pending;
            friendship.FromUserId = this.CurrentUser.Id;
            friendship.ToUserId = userId;
            this.Data.SaveChanges();

            this.TempData[UserFriendshipName] = friendship;

            return this.FriendPartial(userId);
        }

        [Authorize]
        [AjaxPost]
        public ActionResult CancelFriendRequest(string userId)
        {
            var friendship = this.GetFriendship(userId);
            friendship.Status = FriendshipStatusType.None;
            this.Data.SaveChanges();

            this.TempData[UserFriendshipName] = friendship;

            return this.FriendPartial(userId);
        }

        [Authorize]
        [AjaxPost]
        public ActionResult CancelFriendship(string userId)
        {
            var friendship = this.GetFriendship(userId);
            friendship.Status = FriendshipStatusType.None;
            this.Data.SaveChanges();

            this.TempData[UserFriendshipName] = friendship;

            return this.FriendPartial(userId);
        }
        
        [Authorize]
        [AjaxPost]
        public ActionResult DenyFriend(string userId)
        {
            var friendship = this.GetFriendship(userId);
            friendship.Status = FriendshipStatusType.None;
            friendship.FromUserId = this.CurrentUser.Id;
            friendship.ToUserId = userId;
            this.Data.SaveChanges();

            this.TempData[UserFriendshipName] = friendship;

            return this.FriendPartial(userId);
        }

        [Authorize]
        [AjaxPost]
        public ActionResult AcceptFriend(string userId)
        {
            var friendship = this.GetFriendship(userId);
            friendship.Status = FriendshipStatusType.Friends;
            this.Data.SaveChanges();

            this.TempData[UserFriendshipName] = friendship;

            return this.FriendPartial(userId);
        }

        private Friendship GetFriendship(string userId)
        {
            var friendship = this.TempData[UserFriendshipName] as Friendship;
            if (friendship == null)
            {
                var currentUserId = this.CurrentUser.Id;
                friendship = this.Data.Friendships.All().Where(f => (f.FromUserId == currentUserId && f.ToUserId == userId) || (f.ToUserId == currentUserId && f.FromUserId == userId)).FirstOrDefault();

                if (friendship == null)
                {
                    friendship = new Friendship
                    {
                        FromUserId = currentUserId,
                        ToUserId = userId,
                        Status = FriendshipStatusType.None
                    };

                    this.Data.Friendships.Add(friendship);
                }
            }

            return friendship;
        }

        private FriendshipStatusViewModel GetFriendshipViewModel(string userId)
        {
            var friendship = this.GetFriendship(userId);
            var status = GetFriendshipStatusType(friendship, userId);
            var vieModel = new FriendshipStatusViewModel()
            {
                UserId = userId,
                Status = status
            };

            return vieModel;
        }

        private FriendshipStatusTypeViewModel GetFriendshipStatusType(Friendship friendship, string theirsId)
        {
            switch (friendship.Status)
            {
                case FriendshipStatusType.None:
                    return FriendshipStatusTypeViewModel.None;
                case FriendshipStatusType.Pending:
                    if (friendship.FromUserId == theirsId)
                    {
                        return FriendshipStatusTypeViewModel.AwaitingMyResponse;
                    }
                    else
                    {
                        return FriendshipStatusTypeViewModel.AwaitingTheirResponse;
                    }
                case FriendshipStatusType.Friends:
                    return FriendshipStatusTypeViewModel.Friends;
                default:
                    throw new InvalidOperationException("Unsupported Friendship Status!");
            }
        }
    }
}