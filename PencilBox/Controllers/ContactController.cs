using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PbData.Business;
using PbData.Entities;
using PencilBox.Helpers;
using PencilBox.Models;

namespace PencilBox.Controllers
{
    public class ContactController : Controller
    {
        public ActionResult p_UserContact(Guid userId)
        {
            bn_UserContact bnUserContact = new bn_UserContact();
            bn_UserProfile bnUserProfile = new bn_UserProfile();
            ps_UserContact model = new ps_UserContact();

            model.UserProfile = bnUserProfile.GetById(userId);
            model.Contacts = bnUserContact.GetByUserId(userId);

            return View(model);
        }

        public ActionResult Index(Guid? userId)
        {
            if (!userId.HasValue)
                userId = ps_Membership.GetUser().UserId;

            bn_UserContact bnUserContact = new bn_UserContact();
            bn_UserProfile bnUserProfile = new bn_UserProfile();
            ps_UserContact model = new ps_UserContact();

            model.Contacts = bnUserContact.GetByUserId((Guid)userId);
            model.UserProfile = bnUserProfile.GetById((Guid)userId);

            return View(model);
        }

        [Authorize]
        public ActionResult Add()
        {
            bn_ContactType bnContactType = new bn_ContactType();
            ps_ContactType model = new ps_ContactType();

            model.ContactTypes = bnContactType.GetAll();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ps_ContactType model)
        {
            if (ModelState.IsValid)
            {
                bn_UserContact bnUserContact = new bn_UserContact();

                var userContactId = bnUserContact.Create(
                    ps_Membership.GetUser().UserId,
                    model.UserContact.ContactTypeId,
                    model.UserContact.ContactName);

                return RedirectToAction("Index", "User");
            }
            return View();
        }

        public ActionResult Delete(Guid userContactId)
        {
            bn_UserContact bnUserContact = new bn_UserContact();
            var re = bnUserContact.Delete(userContactId);

            return RedirectToAction("Index", "User");
        }
    }
}
