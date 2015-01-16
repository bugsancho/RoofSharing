namespace Roofsharing.Services.Common.Friends
{
    using System;
    using System.Linq;

    public interface IFriendsService
    {
        void AddFriend(string currentUserId, string targetUserId);

        void CandelFriendRequest(string currentUserId, string targetUserId);

        void CancelFriendship(string currentUserId, string targetUserId);

        void DenyFriendship(string currentUserId, string targetUserId);

        void AcceptFriend(string currentUserId, string targetUserId);
    }
}