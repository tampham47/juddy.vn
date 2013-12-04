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
            var model = bnUser.GetAll();
            int numberOfPage = 5;
            ViewBag.CountPage = model.Count() / numberOfPage + 1;
            ViewBag.page = page;
            if(page==model.Count() / numberOfPage+1)
                model = model.GetRange((page-1)*numberOfPage-(numberOfPage-(model.Count() % numberOfPage)), numberOfPage);
            else
            model = model.GetRange((page-1)*numberOfPage,numberOfPage);
            return View(model);
        }

    }
}
