using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PbData.Business;
using PbData.Utility;

namespace Testing.Controllers
{
    public class TamController : Controller
    {
        //show all product
        public ActionResult Index()
        {
            bn_Product bnProduct = new bn_Product();
            var model = bnProduct.GetAll();

            return View(model);
        }

        public ActionResult ProductDetails(Guid productId)
        {
            bn_Product bnProduct = new bn_Product();
            var product = bnProduct.GetById(productId);

            return View(product);
        }

        public ActionResult ProductByTagName(string tagName)
        {
            bn_Product bnProduct = new bn_Product();
            var model = bnProduct.GetByTagName(tagName);
            return View("Index", model);
        }

        public ActionResult TestHumanCode()
        {
            List<string> model = new List<string>();
            //HumanCode humanCode = new PbData.Utility.HumanCode();

            for (int i = 0; i < 10; i++)
            {
                model.Add(bn_HumanCode.GetCode());
            }

            return View(model);
        }

        public ActionResult Photos()
        {
            bn_Photo bnPhoto = new bn_Photo();
            var model = bnPhoto.GetAll();

            return View(model);
        }

        public int UpdatePhotoStatus(Guid photoId)
        {
            bn_Photo bnPhoto = new bn_Photo();
            var re = bnPhoto.UpdateStatus(photoId, EPhoto_Status.Image);

            return re;
        }
    }
}
