using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PbData.Business;
using PbData.Extensions;

namespace PencilBox.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            return View();
        }

        public string GetEnumValue()
        {
            return ESpecialTag.Discount.ParseToText();
        }

        public ActionResult Tipsy()
        {
            return View();
        }

    }
}
