using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using RoofSharing.Data;
using RoofSharing.Data.Models;
using RoofSharing.Web.Controllers.Base;
using RoofSharing.Web.Infrastructure.ValidationAttributes;
using RoofSharing.Web.ViewModels.Account;
using RoofSharing.Web.ViewModels.Friends;
using Roofsharing.Services.Common.Notifiers;
using Roofsharing.Services.Notifiers;

namespace RoofSharing.Web.Controllers
{
    public class FriendsController : BaseController
    {
        private const string UserFriendshipName = "UserFriendship";

        public FriendsController(IRoofSharingData data, INotifierService notifier) : base(data, notifier)
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
            this.Notifier.Notify("New Friend Request!", NotificationMessageType.Info, this.CurrentUser.UserName);
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

        [Authorize]
        [HttpGet]
        public ActionResult GetFriendList()
        {
            var friendIds = this.Data.Friendships.All()
                                .Where(fr => fr.Status == FriendshipStatusType.Friends &&
                                             (fr.ToUserId == this.CurrentUser.Id || fr.FromUserId == this.CurrentUser.Id))
                                .Select(fr => fr.FromUserId == this.CurrentUser.Id ? fr.ToUserId : fr.FromUserId);

            var friends = this.Data.Users.All()
                              .Where(user => friendIds.Contains(user.Id)).Project().To<UserViewModel>();

            return Json(friends, JsonRequestBehavior.AllowGet);
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

                    //this.CurrentUser.Friendships.Add(friendship);
                    //this.Data.Users.All().Where(us => us.Id == userId).FirstOrDefault().Friendships.Add(friendship);
                    ////        this.CurrentUser.Friendships.Add(friendship);
                    ////this.Data.Users.Find(userId).Friendships.Add(friendship);
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