using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PbData.Utility;
using PbData.Business;

namespace Testing.Controllers
{
    public class ThanController : Controller
    {
        //
        // GET: /Than/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Order()
        {
            bn_Order bnOrder = new bn_Order();
            var model = bnOrder.GetBySalesman(
                Guid.Parse("5b21943b-b9a9-4c34-b0da-1f13d1a379fb"),
                EOrder_Status.Available);

            return View(model);
        }


    }
}
