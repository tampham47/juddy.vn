using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PbData.Entities;
using PbData.Business;
using PbData.Extensions;
using PencilBox.Models;
using PencilBox.Helpers;

namespace PencilBox.Controllers
{
    public class ProductController : Controller
    {
        private void Photo_UpdateProductId(Guid productId, List<ps_ProductPhoto> photos)
        {
            var bnPhoto = new bn_Photo();
            foreach (var item in photos)
            {
                bnPhoto.UpdateProductId(
                    item.PhotoId,
                    productId);
            }
        }

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

        public ActionResult Filter(string request)
        {
            bn_Product bnProduct = new bn_Product();
            var model = bnProduct.GetByTags(request);

            var tags = bn_HashTag.DivTags(request);
            ViewBag.Tags = tags;
            ViewBag.FilterRequest = bn_HashTag.MergerTag(tags);

            return View(model);
        }

        [Authorize]
        public ActionResult Add(string tag)
        {
            ps_ProductCreate model = new ps_ProductCreate();

            model.Product = new pb_Product();
            model.ProductPhotos = new List<ps_ProductPhoto>();
            model.Tags = string.Format("#{0}", tag);

            Session["Photos"] = new List<ps_ProductPhoto>();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ps_ProductCreate model)
        {
            List<ps_ProductPhoto> photos = (List<ps_ProductPhoto>)Session["Photos"];
            if (ModelState.IsValid && photos.Count > 0)
            {
                var bnProduct = new bn_Product();

                var productId = bnProduct.Create(
                    ps_Membership.GetUser().UserId,
                    model.Product.Name,
                    model.Product.Price,
                    model.Product.Description);

                if (productId != null)
                {
                    var bnProductTag = new bn_ProductTag(productId);

                    //updated photos for product
                    Photo_UpdateProductId(productId, photos);

                    //tag product to some hash
                    var tagList = bn_HashTag.DivTags(model.Tags);
                    foreach (var tag in tagList)
                    {
                        bnProductTag.Tag(
                            tag,
                            ps_Membership.GetUser().UserId, null);
                    }

                    return RedirectToAction("Detail", new { productId = productId });
                }

                //alert error
                return View(model);
            }

            Session["Photos"] = photos;
            model.ProductPhotos = new List<ps_ProductPhoto>();
            return View(model);
        }

        [Authorize]
        public ActionResult Edit(Guid productId)
        {
            bn_Product bnProduct = new bn_Product();
            pb_Product product = bnProduct.GetById(productId);

            if (product != null)
            {
                bn_Photo bnPhoto = new bn_Photo();
                bn_ProductTag bnProductTag = new bn_ProductTag(productId);
                ps_ProductCreate model = new ps_ProductCreate();

                var photoList = bnPhoto.GetByProductId(productId);
                List<ps_ProductPhoto> photos = new List<ps_ProductPhoto>();
                foreach (var item in photoList)
                {
                    photos.Add(new ps_ProductPhoto {
                        PhotoId = item.PhotoId,
                        PhotoPath = item.ImagePath
                    });
                }

                model.Product = product;
                model.Product.Description = HttpUtility.HtmlDecode(model.Product.Description);
                model.Tags = bnProductTag.GetTagedList();
                model.ProductPhotos = photos;
                Session["Photos"] = new List<ps_ProductPhoto>();
                return View(model);
            }
            else
            {
                //redirect to error page
                return RedirectToAction("Error404", "Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(ps_ProductCreate model)
        {
            if (ModelState.IsValid)
            {
                bn_Product bnProduct = new bn_Product();
                bn_ProductTag bnProductTag = new bn_ProductTag(model.Product.ProductId);

                bnProduct.Update(
                    model.Product.ProductId,
                    model.Product.Name,
                    model.Product.Price,
                    model.Product.Description);

                //photos
                List<ps_ProductPhoto> photos = (List<ps_ProductPhoto>)Session["Photos"];
                Photo_UpdateProductId(
                    model.Product.ProductId,
                    photos);

                //update hashtags 
                //is under construction
                bnProductTag.RemoveAll();
                bnProductTag.TagedToSome(model.Tags, model.Product.UserId);

                return RedirectToAction("Detail", new { productId = model.Product.ProductId });
            }

            model.Product.Description = HttpUtility.HtmlDecode(model.Product.Description);
            return View(model);
        }

        //[Authorize]
        public ActionResult Detail(Guid productId)
        {
            bn_Product bnProduct = new bn_Product();
            var model = bnProduct.GetById(productId);
            bnProduct.IncrViews(model.ProductId);

            return View(model);
        }

        [Authorize]
        public ActionResult Delete(Guid id)
        {
            bn_Product bnProduct = new bn_Product();
            bnProduct.UpdateStatus(
                id, EProduct_Status.UnAvailable);
            return Redirect("/");
        }

        [Authorize]
        public ActionResult Favourite(Guid productId)
        {
            bn_ProductTag bnProductTag = new bn_ProductTag(productId);
            bnProductTag.Tag(
                ESpecialTag.Favourite.ParseToText(),
                ps_Membership.GetUser().UserId,
                null);

            return RedirectToAction("Detail", new { productId = productId });
        }

        [Authorize]
        public ActionResult Own()
        {
            bn_Product bnProduct = new bn_Product();
            var model = bnProduct.GetByUserId(
                ps_Membership.GetUser().UserId);

            return View(model);
        }
    }
}