using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PbData.Business;
using PbData.Entities;

namespace Juddy.Services.Controllers
{
    public class ProductController : ApiController
    {
        [HttpGet]
        public List<pb_Product> GetAll()
        {
            bn_Product bnProduct = new bn_Product(isLazy: false);
            return bnProduct.GetAll();
        }

        [HttpGet]
        public List<pb_Product> Filter(string tag)
        {
            bn_Product bnProduct = new bn_Product(isLazy: false);
            return bnProduct.GetByTagName(tag);
        }
    }
}
