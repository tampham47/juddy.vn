using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PencilBox.Helpers;
using PbData.Business;
using PbData.Entities;

namespace PencilBox.Controllers
{
    public class LocationController : Controller
    {
        public ActionResult p_UserLocation(Guid userId)
        {
            bn_Location bnLocation = new bn_Location();
            var model = bnLocation.GetByUserId((Guid)userId);

            if (model.Count > 0)
                return View(model);
            else
                return null;
        }

        public ActionResult Index(Guid? userId)
        {
            if (!userId.HasValue)
                userId = ps_Membership.GetUser().UserId;

            bn_Location bnLocation = new bn_Location();
            var model = bnLocation.GetByUserId((Guid)userId);
            ViewBag.UserId = userId;

            return View(model);
        }

        [Authorize]
        public ActionResult New()
        {
            pb_Location model = new pb_Location();
            model.UserId = ps_Membership.GetUser().UserId;

            return View(model);
        }

        [HttpPost]
        public ActionResult New(pb_Location model)
        {
            if (ModelState.IsValid && model.Address != null)
            {
                bn_Location bnLocation = new bn_Location();
                var re = bnLocation.Create(
                    model.UserId,
                    model.Latitude,
                    model.Longitude,
                    model.Address);

                if (re != Guid.Empty)
                {
                    return RedirectToAction("Index", "User");
                }

                //return fail.
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Edit(Guid locationId)
        {
            bn_Location bnLocation = new bn_Location();
            var model = bnLocation.GetById(locationId);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(pb_Location model)
        {
            if (ModelState.IsValid && model.Address != null)
            {
                bn_Location bnLocation = new bn_Location();
                var re = bnLocation.Update(
                    model.LocationId,
                    model.Latitude,
                    model.Longitude,
                    model.Address);

                if (re >= 0)
                {
                    return RedirectToAction("Index", "User");
                }

                //return fail.
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Delete(Guid locationId)
        {
            bn_Location bnLocation = new bn_Location();
            var re = bnLocation.Delete(locationId);

            if (re < 0)
            {
                ViewBag.MessError = "";
            }
            return RedirectToAction("Index", "User");
        }
    }
}
