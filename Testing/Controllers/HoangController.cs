using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PbData.Business;

namespace Testing.Controllers
{
    public class HoangController : Controller
    {
        //
        // GET: /Hoang/

        public ActionResult Index()
        {
            bn_Product product = new bn_Product();
            var data = product.GetAll();
            return View(data);
        }

    }
}
