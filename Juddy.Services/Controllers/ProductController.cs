using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PbData.Business;
using PbData.Entities;
using Juddy.Services.Models;

namespace Juddy.Services.Controllers
{
    public class ProductController : ApiController
    {
        [HttpGet]
        public List<sv_Product> GetAll()
        {
            bn_Product bnProduct = new bn_Product();
            return bnProduct
                .GetAll().Select(m => new sv_Product(m))
                .ToList();
        }

        [HttpGet]
        public List<sv_Product> Filter(string tag)
        {
            bn_Product bnProduct = new bn_Product();
            return bnProduct.GetByTagName(tag)
                .Select(m => new sv_Product(m))
                .ToList();
        }

        [HttpGet]
        public List<sv_Product> Category(string tag, string category)
        {
            bn_Product bnProduct = new bn_Product();
            return bnProduct.GetByCategory(tag, category)
                .Select(m => new sv_Product(m))
                .ToList();
        }

        [HttpPost]
        public bool Favourite(Guid productId, Guid userId)
        {
            return false;
        }
    }
}
