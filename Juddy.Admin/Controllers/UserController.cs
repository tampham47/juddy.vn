using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using PbData.Business;
using PbData.Entities;

namespace Juddy.Admin.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
   
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AllUser(int page = 1)
        {         
            bn_UserProfile bnUser = new bn_UserProfile();
            int numberOfPage = 24;
            ViewBag.CountPage = (bnUser.GetCountAll() / numberOfPage)+1;
            page = page > 0 ? page : 1;
            page = page < ViewBag.CountPage ? page : ViewBag.CountPage;

            ViewBag.page = page;
            var model = bnUser.GetListInPage(page, numberOfPage);
            return View(model);
        }
        public ActionResult Detail(Guid? id)
        {         
            bn_UserProfile bnUser = new bn_UserProfile();
            pb_UserProfile model = null;
            if(id.HasValue)
            model = bnUser.GetById(id.Value);
            return View(model);
        }

    }
}
