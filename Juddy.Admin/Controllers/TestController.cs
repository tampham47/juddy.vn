using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PbData.Business;
using PbData.Entities;

namespace Juddy.Admin.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            bn_Product bnProduct = new bn_Product();
            var model = bnProduct.GetByTags("pbox");
            return View(model);
        }

    }
}
