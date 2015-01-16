namespace Roofsharing.Services.Friends
{
    using System;
    using System.Linq;
    using RoofSharing.Data;
    using Roofsharing.Services.Common.Friends;

    public class FriendshipService : IFriendsService
    {
        public FriendshipService(IRoofSharingData data)
        {
            this.Data = data;
        }

        public IRoofSharingData Data { get; private set; }

        public void AddFriend(string currentUserId, string targetUserId)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public void CandelFriendRequest(string currentUserId, string targetUserId)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public void CancelFriendship(string currentUserId, string targetUserId)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public void DenyFriendship(string currentUserId, string targetUserId)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public void AcceptFriend(string currentUserId, string targetUserId)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }
}