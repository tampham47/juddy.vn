using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PbData.Business;
using PbData.Entities;
using PbData.Extensions;
using PencilBox.Helpers;

namespace PencilBox.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult p_UserInfomation(Guid userId)
        {
            var model = ps_Membership.GetUser();

            return View(model);
        }

        [Authorize]
        public ActionResult Index()
        {
            var model = ps_Membership.GetUser();

            return View(model);
        }

        [Authorize]
        public ActionResult Edit(Guid? userId)
        {
            if (!userId.HasValue)
                userId = ps_Membership.GetUser().UserId;

            bn_UserProfile bnUserProfile = new bn_UserProfile();
            pb_UserProfile model = new pb_UserProfile();

            model = bnUserProfile.GetById((Guid)userId);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(pb_UserProfile model)
        {
            if (ModelState.IsValid)
            {
                bn_UserProfile bnUserProfile = new bn_UserProfile();
                var re = bnUserProfile.Update(
                    ps_Membership.GetUser().UserId,
                    model.FirstName,
                    model.LastName,
                    model.DateOfBirth);

                if (re >= 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    //can't udpate
                }
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Favourite()
        {
            bn_Product bnProduct = new bn_Product();
            var model = bnProduct.GetByUserAndTagName(
                ps_Membership.GetUser().UserId,
                ESpecialTag.Favourite.ParseToText());

            return View(model);
        }
    }
}
