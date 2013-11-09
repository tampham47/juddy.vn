using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PbData.Business;
using PbData.Entities;
using PencilBox.Helpers;
using WebMatrix.WebData;
using PencilBox.Models;
using PbData.Extensions;

namespace PencilBox.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New(Guid productId)
        {
            bn_Product bnProduct = new bn_Product();
            ps_Order model = new ps_Order();

            var user = ps_Membership.GetUser();
            var product = bnProduct.GetById(productId);

            if (user != null)
            {
                //add some default values.
                model.Order.UserId = user.UserId;
                model.Order.CustomerName = user.FullName;
            }

            model.Order.Price = (product.DiscountPrice.HasValue) ?
                product.DiscountPrice.Value :
                product.Price;
            model.Order.Amount = 1;
            model.Order.ProductId = productId;
            model.Product = product;

            return View(model);
        }

        [HttpPost]
        public ActionResult New(ps_Order model)
        {
            if (ModelState.IsValid)
            {
                bn_Order bnOrder = new bn_Order();
                var orderId = bnOrder.Create(
                    model.Order.ProductId,
                    model.Order.UserId,
                    model.Order.CustomerName,
                    model.Order.CustomerPhone,
                    model.Order.Price,
                    model.Order.Amount,
                    model.Order.Address,
                    model.Order.Comment);

                if (orderId != Guid.Empty)
                {
                    return RedirectToAction("Done");
                }
                else
                {
                    ViewBag.Message = "Error";
                    return View(model);
                }
            }

            return View(model);
        }

        public ActionResult Done()
        {
            return View();
        }

        [Authorize]
        public ActionResult Salesman()
        {
            bn_Order bnOrder = new bn_Order();
            var user = ps_Membership.GetUser();

            var model = bnOrder.GetBySalesman(
                user.UserId, 
                EOrder_Status.Available);

            //ESpecialTag.Discount.
            return View(model);
        }

        [Authorize]
        public ActionResult Customer()
        {
            bn_Order bnOrder = new bn_Order();
            var user = ps_Membership.GetUser();

            var model = bnOrder.GetByUserId(user.UserId);

            return View(model);
        }
    }
}
