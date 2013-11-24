using PbData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PbData.Utility;

namespace PbData.Business
{
    public class bn_Product
    {
        private pb_Entities db = new pb_Entities();
        public bn_Product(pb_Entities connection = null)
        {
            if (connection != null)
            {
                db = connection;
            }
        }

        private bool Contain(List<pb_ProductTag> productTagList, List<Guid> tagIdList)
        {
            var tmpTagIdList =
                (
                    from get in productTagList
                    select get.HashTagId
                )
                .ToList();

            foreach (var item in tagIdList)
            {
                if (!tmpTagIdList.Contains(item))
                    return false;
            }

            return true;
        }

        public Guid Create(Nullable<System.Guid> userId, string name, Nullable<double> price, string description)
        {
            var productId = Guid.NewGuid();
            int re = (int)db.pb_Product_Create(
                productId,
                userId,
                name,
                price,
                description,
                bn_HumanCode.GetCode()).Single();

            if (re >= 0)
                return productId;
            else
                return Guid.Empty;
        }
        public int Update(Guid productId, string name, double price, string description)
        {
            var re = (int)db.pb_Product_Update(
                productId,
                name,
                price,
                description).Single();

            return re;
        }
        public int UpdateDiscountPrice(Guid productId, double discountPrice)
        {
            var re = (int)db.pb_Product_UpdateDiscountPrice(
                productId,
                discountPrice).Single();

            return re;
        }
        public int UpdateStatus(Guid productId, int status)
        {
            return (int)db.pb_Product_UpdateStatus(productId, status).Single();
        }
        public int Delete(Guid productId)
        {
            int re = (int)db.pb_Product_Delete(productId).Single();

            return re;
        }
        public int UpdateHumanCode(Guid productId, string humanCode)
        {
            var re = (int)db.pb_Product_UpdateHumanCode(
                productId,
                humanCode).Single();

            return re;
        }
        public int IncrViews(Guid id)
        {
            return (int)db.pb_Product_IncrViews(id).Single();
        }

        //make product is unavailable
        public static int Unavaiable(Guid productId)
        {
            using (pb_Entities db = new pb_Entities())
            {
                var re = (int)db.pb_Product_UpdateStatus(
                    productId,
                    EProduct_Status.UnAvailable)
                .Single();

                return re;
            }
        }

        public pb_Product GetById(Guid productId)
        {
            var products = db.pb_Product_GetbyId(productId).ToList();

            if (products.Count >= 0)
                return products.First();
            else
                return null;
        }
        public List<pb_Product> GetByHashTagId(Guid hashTagId)
        {
            var result = db.pb_Product_GetbyHashTagId(hashTagId)
                .Where(m => m.Status == EProduct_Status.Available)
                .Take(100)
                .ToList();

            return result;
        }
        public List<pb_Product> GetByUserId(Guid userId)
        {
            var products = (
                    from get in db.pb_Product_GetbyUserId(userId).ToList()
                    where
                         get.Status == EProduct_Status.Available &&
                         get.pb_Photo.Count > 0
                    select get
                ).ToList();

            return products;
        }
        public List<pb_Product> GetAll()
        {
            return db.pb_Product
                .Where(m => m.Status == EProduct_Status.Available)
                .ToList();
        }
        public List<pb_Product> GetByTagName(string tagName)
        {
            bn_HashTag bnHashTag = new bn_HashTag();
            List<pb_Product> result = new List<pb_Product>();
            var hashTag = bnHashTag.GetByTagName(tagName);

            if (hashTag != null)
                return GetByHashTagId(hashTag.HashTagId);
            else
                return result;
        }

        //filter
        public List<pb_Product> GetByTags(string tags)
        {
            var tagList = bn_HashTag.DivTags(tags);

            var hashTags =
                (
                    from get in db.pb_HashTag
                    where tagList.Contains(get.TagName)
                    select get.HashTagId
                )
                .ToList();

            List<pb_Product> result = new List<pb_Product>();
            foreach (var item in db.pb_Product.ToList())
            {
                if (Contain(item.pb_ProductTag.ToList(), hashTags))
                {
                    result.Add(item);
                }
            }

            return result;
        }
        public List<pb_Product> GetByUserAndTagId(Guid userId, Guid hashTagId)
        {
            var result = db.pb_Product_GetbyUserAndTag(
                userId,
                hashTagId).ToList();

            return result;
        }
        public List<pb_Product> GetByUserAndTagName(Guid userId, string tagName)
        {
            bn_HashTag bnHashTag = new bn_HashTag();
            List<pb_Product> result = new List<pb_Product>();
            var hashTag = bnHashTag.GetByTagName(tagName);

            if (hashTag != null)
                return GetByUserAndTagId(userId, hashTag.HashTagId);
            else
                return result;
        }
        public List<pb_Product> FilterByHumanCode(Guid userId, string humanCode)
        {
            return new List<pb_Product>();
        }

        //photos
        public int ResizePhotos(Guid productId)
        {
            
            return 0;
        }

        //test transaction
        public bool DeleteProduct(Guid productId)
        {
            bn_Photo bnPhoto = new bn_Photo(db);

            using (var transaction = db.Database.Connection.BeginTransaction())
            {
                Delete(productId);
                bnPhoto.DeleteByProductId(productId);

                transaction.Commit();
            }

            return false;
        }
    }

    public static class EProduct_Status
    {
        public const int Available = 0;
        public const int UnAvailable = 1;
    }
}
