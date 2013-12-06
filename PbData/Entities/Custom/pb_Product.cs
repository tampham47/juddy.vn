using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PbData.Extensions;
using PbData.Business;
using PbData.Utility;

namespace PbData.Entities
{
    public partial class pb_Product
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);

        public List<pb_Photo> CoverImageList()
        {
            var result = (
                    from get in pb_Photo
                    where get.Status == EPhoto_Status.Cover
                    select get
                )
                .ToList();

            return result;
        }
        public pb_Photo CoverImage
        {
            //simple, improve later
            get
            {
                var pro = ProductId;
                var covers = CoverImageList();
                if (covers.Count == 0) return null;

                var index = random.Next(0, covers.Count);
                return covers[index];
            }
        }
        public bool IsTag(Guid? userId, ESpecialTag specialTag)
        {
            if (!userId.HasValue) return false;

            bn_ProductTag bnProductTag = new bn_ProductTag(ProductId);
            return bnProductTag.IsTag(
                (Guid)userId, 
                specialTag.ParseToText());
        }
        public bool IsTag(string tagName)
        {
            bn_ProductTag bnPtag = new bn_ProductTag(ProductId);
            return bnPtag.IsExists(tagName);
        }

        public string HumanShortPrice()
        {
            return bn_HumanPrice.GetShortPrice((float)Price);
        }
        public string HumanPrice()
        {
            return bn_HumanPrice.GetPrice((float)Price);
        }
        public string DisplayName()
        {
            if (Name.Length > 25)
                return Name.Substring(0, 25);
            else
                return Name;
        }
    }
}
