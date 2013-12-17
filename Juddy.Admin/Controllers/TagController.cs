using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PbData.Business;
using PbData.Entities;

namespace Juddy.Admin.Controllers
{
    public class TagController : Controller
    {
        //
        // GET: /Tag/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AllTag(int page = 1)
        {
            bn_HashTag bnHashTag = new bn_HashTag();
            int numberOfPage = 24;
            ViewBag.CountPage = (bnHashTag.GetCountAll() / numberOfPage) + 1;
            page = page > 0 ? page : 1;
            page = page < ViewBag.CountPage ? page : ViewBag.CountPage;

            ViewBag.page = page;
            var model = bnHashTag.GetListInPage(page, numberOfPage);
            return View(model);
        }


    }
}
