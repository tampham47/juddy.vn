using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using PbData.Entities;
using PbData.Business;
using PbData.Extensions;
using Juddy.Testing.Models;


namespace Juddy.Testing.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index(Guid? tagId, string tagName = "pbox")
        {
            bn_Product bnProduct = new bn_Product();
            List<pb_Product> model = new List<pb_Product>();

            if (!tagId.HasValue)
            {
                model = bnProduct.GetByTagName(tagName);
            }
            else
            {
                model = bnProduct.GetByHashTagId(tagId.Value);
            }

            ViewBag.TagName = tagName;
            return View(model);
        }

        public ActionResult Category(string tag, string category)
        {
            bn_Product bnProduct = new bn_Product();
            var model = bnProduct.GetByCategory(tag, category);

            ViewBag.TagName = tag;
            return View("Index", model);
        }

        public ActionResult Filter(string request)
        {
            bn_Product bnProduct = new bn_Product();
            var model = bnProduct.GetByTags(request);

            var tags = bn_HashTag.DivTags(request);
            ViewBag.Tags = tags;
            ViewBag.FilterRequest = bn_HashTag.MergerTag(tags);

            return View(model);
        }
    }
}
