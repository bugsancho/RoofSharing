namespace RoofSharing.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using RoofSharing.Common;
    using RoofSharing.Data;
    using RoofSharing.Data.Models;
    using RoofSharing.Web.Controllers.Base;
    using RoofSharing.Web.ViewModels.Host;
    using Roofsharing.Services.Common.Notifiers;
    using Roofsharing.Services.Notifiers;

    public class HostController : BaseController
    {
        private const int PageSize = 2;

        public HostController(IRoofSharingData data, INotifierService notifier) : base(data, notifier)
        {
        }
        
        public ActionResult Browse(string city = null, int page = 1)
        {
            var users = this.Data.Users.All();
            List<UserCardViewModel> results;
            if (!string.IsNullOrEmpty(city))
            {
                users = users.Where(user => user.LocationInfo.City == city);
            }

            if (this.HttpContext.User.Identity.IsAuthenticated)
            {
                users = users.Where(user => user.Id != this.CurrentUser.Id);
            }

            ViewBag.Pages = Math.Ceiling((double)users.Count() / PageSize);

            results = users.OrderBy(x => x.Id)
                           .Skip((page - 1) * PageSize)
                           .Take(PageSize)
                           .Project()
                           .To<UserCardViewModel>()
                           .ToList();
            
            return View(results);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Invite(string userId)
        {
            if (userId == this.CurrentUser.Id)
            {
                ViewData[GlobalConstants.ErrorMessage] = "You cannot send a host invitation to yourself!";
                return RedirectToAction("Browse");
            }

            if (ModelState.IsValid)
            {
                var hostNames = this.Data.Users.All().Where(u => u.Id == userId).Select(x => x.FirstName + " " + x.LastName).FirstOrDefault();
                if (hostNames == null)
                {
                    return HttpNotFound();
                }

                var model = new HostInviteViewModel()
                {
                    HostId = userId,
                    HostNames = hostNames
                };
                return View(model);
            }

            return RedirectToAction("Browse");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Invite(HostInviteViewModel input)
        {
            if (input.HostId == this.CurrentUser.Id)
            {
                ViewData[GlobalConstants.ErrorMessage] = "You cannot send a host invitation to yourself!";
                return RedirectToAction("Browse");
            }

            if (ModelState.IsValid)
            {
                var invite = Mapper.Map<HostInvitation>(input);
                invite.GuestId = this.CurrentUser.Id;
                this.Data.Invitations.Add(invite);
                this.Data.SaveChanges();    

                string hostUserName = this.Data.Users.All().Where(x => x.Id == input.HostId).Select(x => x.UserName).FirstOrDefault();
                this.Notifier.Notify(string.Format("{0} has sent you a request to host them!", this.CurrentUser.FirstName + " " + this.CurrentUser.LastName), NotificationMessageType.Info, hostUserName);
                TempData[GlobalConstants.SuccessMessage] = "You have successfully sent a host invitation to " + hostUserName;

                return RedirectToAction("Index", "Home", new { area = string.Empty });
            }
            
            return View(input);
        }

        [Authorize]
        [HttpGet]
        public ActionResult SentInvites()
        {
            var invites = this.Data.Invitations.All()
                              .Where(i => i.GuestId == this.CurrentUser.Id)
                              .Project()
                              .To<HostInviteViewModel>()
                              .ToList();
            return View(invites);
        }

        [Authorize]
        [HttpGet]
        public ActionResult ReceivedInvites()
        {
            var invites = this.Data.Invitations.All()
                              .Where(i => i.HostId == this.CurrentUser.Id &&
                                          i.Status == InvitationStatusType.Pending)
                              .Project()
                              .To<HostInviteRespondViewModel>()
                              .ToList();
            return View(invites);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var owner = this.Data.Invitations.All()
                            .Where(i => i.Id == id)
                            .Select(x => x.GuestId)
                            .FirstOrDefault();

            if (owner != this.CurrentUser.Id)
            {
                return RedirectToAction("SentInvites");
            }

            var invite = this.Data.Invitations.All()
                             .Where(i => i.Id == id)
                             .Project()
                             .To<HostInviteViewModel>()
                             .FirstOrDefault();

            return View(invite);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HostInviteViewModel input)
        {
            if (input.HostId == this.CurrentUser.Id)
            {
                TempData[GlobalConstants.ErrorMessage] = "You cannot send a host invitation to yourself!";
                return RedirectToAction("SentInvites");
            }

            if (ModelState.IsValid)
            {
                var invite = this.Data.Invitations.All()
                                 .Where(i => i.Id == input.Id)
                                 .FirstOrDefault();

                Mapper.Map(input, invite);

                this.Data.SaveChanges();    

                string hostUserName = this.Data.Users.All().Where(x => x.Id == input.HostId).Select(x => x.UserName).FirstOrDefault();
                this.Notifier.Notify(string.Format("{0} has updated his request to host them!", this.CurrentUser.FirstName + " " + this.CurrentUser.LastName), NotificationMessageType.Info, hostUserName);
                TempData[GlobalConstants.SuccessMessage] = "You have successfully updated your invitation to " + hostUserName;

                return RedirectToAction("SentInvites", new { area = string.Empty });
            }
            
            return View(input);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var owner = this.Data.Invitations.All()
                            .Where(i => i.Id == id)
                            .Select(x => x.GuestId)
                            .FirstOrDefault();

            if (owner != this.CurrentUser.Id)
            {
                TempData[GlobalConstants.ErrorMessage] = "You don't have permissions to delete that invitation!";
                return RedirectToAction("SentInvites");
            }

            this.Data.Invitations.Delete(id);
            this.Data.SaveChanges();

            TempData[GlobalConstants.SuccessMessage] = "Invitation deleted successfully!";
            return RedirectToAction("SentInvites");
        }

        [Authorize]
        public ActionResult Approve(int id)
        {
            var invitation = this.Data.Invitations.Find(id);
            if (invitation.HostId != this.CurrentUser.Id)
            {
                TempData[GlobalConstants.ErrorMessage] = "You are not authorized to approve that invitation!";
                return RedirectToAction("Invites");
            }
            invitation.Status = InvitationStatusType.Accepted;
            this.Data.SaveChanges();

            TempData[GlobalConstants.SuccessMessage] = "Invite approved successfully!";
            return RedirectToAction("ReceievedInvites");
        }

        [Authorize]
        public ActionResult Reject(int id)
        {
            var invitation = this.Data.Invitations.Find(id);
            if (invitation.HostId != this.CurrentUser.Id)
            {
                TempData[GlobalConstants.ErrorMessage] = "You are not authorized to reject that invitation!";
                return RedirectToAction("Invites");
            }
            invitation.Status = InvitationStatusType.Denied;
            this.Data.SaveChanges();

            TempData[GlobalConstants.SuccessMessage] = "Invite rejected successfully!";
            return RedirectToAction("ReceievedInvites");
        }
    }
}