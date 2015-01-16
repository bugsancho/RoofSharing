using AutoMapper;
using RoofSharing.Data;
using RoofSharing.Data.Models;
using Roofsharing.Services.Common.Notifiers;
using RoofSharing.Web.ViewModels.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;

namespace RoofSharing.Web.Controllers
{
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

            users = users.Where(user => user.Id != this.CurrentUser.Id);

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
        public ActionResult Invite(HostInviteInputViewModel input)
        {
            if (ModelState.IsValid)
            {
                var hostNames = this.Data.Users.All().Where(u => u.Id == input.UserId).Select(x => x.FirstName + " " + x.LastName).FirstOrDefault();
                if (hostNames == null)
                {
                    return HttpNotFound();
                }

                var model = new HostInviteViewModel()
                {
                    HostId = input.UserId,
                    HostNames = hostNames
                };
                return View(model);
            }
            return RedirectToAction("Plan", "Trips", null);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Invite(HostInviteViewModel input)
        {
            if (ModelState.IsValid)
            {
                var invite = Mapper.Map<HostInvitation>(input);
                invite.GuestId = this.CurrentUser.Id;
                this.Data.Invitations.Add(invite);
                this.Data.SaveChanges();    

                return RedirectToAction("Index", "Home", new { area = string.Empty });
            }
            return View(input);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Invites()
        {
            var invites = this.Data.Invitations.All().Where(i => i.HostId == this.CurrentUser.Id).Project().To<HostInviteViewModel>().ToList();
            return View(invites);
        }
    }
}