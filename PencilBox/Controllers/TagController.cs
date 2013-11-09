using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PbData.Business;
using PbData.Entities;
using PencilBox.Helpers;

namespace PencilBox.Controllers
{
    public class TagController : Controller
    {
        //
        // GET: /Tag/

        public ActionResult p_TagHeader(string tagName)
        {
            bn_HashTag bnHashTag = new bn_HashTag();
            var model = bnHashTag.GetByTagName(tagName);

            return View(model);
        }
        public ActionResult p_ProductTags(Guid productId)
        {
            bn_HashTag bnHashTag = new bn_HashTag();
            var model = bnHashTag.GetByProductId(productId);

            return View(model);
        }

        [Authorize]
        public ActionResult Index(Guid? userId)
        {
            if (!userId.HasValue)
                userId = (Guid)ps_Membership.GetUser().UserId;

            bn_HashTag bnHashTag = new bn_HashTag();
            var model = bnHashTag.GetByUserId(
                (Guid)userId, 
                EHashtag_Type.Private);
            
            return View(model);
        }

        [Authorize]
        public ActionResult Add()
        {
            pb_HashTag model = new pb_HashTag();
            model.UserId = ps_Membership.GetUser().UserId;

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(pb_HashTag model)
        {
            bn_HashTag bnHashTag = new bn_HashTag();
            string iconPath = (string)Session["IconPath"];
            //Check image
            if (iconPath == "" || iconPath == null)
            {
                ViewBag.Message = "Phải có ít nhất một hình ảnh.";
                return View(model);
            }

            //Check hashtag
            if (bnHashTag.IsExists(model.TagName))
            {
                ViewBag.Message = "This tag has been create, choose another one, please!";
                return View(model);
            }

            if (ModelState.IsValid)
            {
                bnHashTag.Create(
                    model.UserId,
                    EHashtag_Type.Private,
                    model.TextName,
                    model.TagName,
                    iconPath,
                    model.Description);

                //clear iconpath;
                Session["IconPath"] = null;

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(string tagName)
        {
            bn_HashTag bn_hashTag = new bn_HashTag();
            var model = bn_hashTag.GetByTagName(tagName);
            if (model != null)
                return View();
            else
                return View();
        }

        [HttpPost]
        public ActionResult Edit(pb_HashTag model)
        {

            return RedirectToAction("Index");
        }
    }
}
