using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PbData.Entities;
using PbData.Business;

namespace Juddy.Services.Models
{
    public class sv_Product
    {
        private string serverPath = System.Configuration.ConfigurationManager.AppSettings["ImagePath"];

        public sv_Product(pb_Product model)
        {
            ProductId = model.ProductId;
            UserId = model.UserId;
            Name = model.Name;
            Price = model.Price;
            HumanPrice = model.HumanPrice();
            Description = model.Description;
            DateCreated = model.DateCreated;
            DateUpdated = model.DateUpdated;
            HumanCode = model.HumanCode;
            Status = model.Status;
            Views = model.Views;

            Cover = new sv_Photo(model.CoverImage);

            bn_Photo bnPhoto = new bn_Photo();
            Photos = bnPhoto.GetByProductId(ProductId)
                .Select(m => new sv_Photo(m))
                .ToList();

        }

        public System.Guid ProductId { get; set; }
        public System.Guid UserId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string HumanPrice { get; set; }
        public Nullable<double> DiscountPrice { get; set; }
        public string Description { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public string HumanCode { get; set; }
        public int Status { get; set; }
        public long Views { get; set; }

        public List<sv_Photo> Photos { get; set; }
        public sv_Photo Cover { get; set; }
    }
}