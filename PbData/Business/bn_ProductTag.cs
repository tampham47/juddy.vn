using PbData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PbData.Business
{
    public class bn_ProductTag
    {
        private pb_Entities db = new pb_Entities();
        private Guid productId;
        public bn_ProductTag(Guid pId, pb_Entities connection = null)
        {
            if (connection != null)
            {
                db = connection;
            }

            productId = pId;
        }

        public pb_ProductTag GetByHashTagId(Guid hashTagId)
        {
            var result = db.pb_ProductTag_GetbyHashTagId(
                hashTagId,
                productId).ToList();

            if (result.Count > 0)
                return result.First();
            else
                return null;
        }

        //for this product
        public pb_ProductTag GetByUserAndTagId(Guid userId, Guid hashTagId)
        {
            var result = db.pb_ProductTag_GetbyUserAndTag(
                productId,
                userId,
                hashTagId).ToList();

            if (result.Count > 0)
                return result.First();
            else
                return null;
        }
        public pb_ProductTag GetByUserAndTagName(Guid userId, string tagName)
        {
            bn_HashTag bnHashTag = new bn_HashTag();
            var hashTag = bnHashTag.GetByTagName(tagName);

            if (hashTag != null)
                return GetByUserAndTagId(userId, hashTag.HashTagId);
            else
                return null;
        }

        public bool IsExists(Guid hashTagId)
        {
            var productTag = GetByHashTagId(hashTagId);

            if (productTag != null)
                return true;
            else
                return false;
        }
        public bool IsExists(string tagName)
        {
            bn_HashTag bnHashTag = new bn_HashTag();
            var hashTag = bnHashTag.GetByTagName(tagName);

            var productTag = GetByHashTagId(hashTag.HashTagId);

            if (productTag != null)
                return true;
            else
                return false;
        }
        public bool IsTag(Guid userId, string tagName)
        {
            var result = GetByUserAndTagName(
                userId, tagName);

            if (result != null)
                return true;
            else
                return false;
        }

        public Guid Create(Guid hashTagId, Nullable<System.Guid> userId, string comment)
        {
            bn_HashTag bnHashTag = new bn_HashTag();
            var hashtag = bnHashTag.GetById(hashTagId);
            var productTagId = Guid.NewGuid();

            if (hashtag.Type == EHashtag_Type.System)
            {
                if (userId.HasValue && !IsExistsInUser((Guid)userId, hashTagId))
                {
                    var re = (int)db.pb_ProductTag_Create(
                        productTagId,
                        productId,
                        hashTagId,
                        userId,
                        comment).Single();

                    if (re >= 0)
                        return productTagId;
                }
            }
            else
            {
                if (!IsExists(hashTagId))
                {
                    var re = (int)db.pb_ProductTag_Create(
                        productTagId,
                        productId,
                        hashTagId,
                        userId,
                        comment).Single();

                    if (re >= 0)
                        return productTagId;
                }
            }

            return Guid.Empty;
        }
        public int Delete(Guid productTagId)
        {
            var re = (int)db.pb_ProductTag_Delete(productTagId).Single();

            return re;
        }
        public int RemoveAll()
        {
            //transaction here - but don't right now
            var result = (int)db.pb_ProductTag_DeleteByProductId(
                productId).Single();

            return result;
        }

        //tag a product to tagName
        public Guid Tag(string tagName, Guid userId, string comment)
        {
            bn_HashTag bnHashTag = new bn_HashTag();
            var hashTag = bnHashTag.GetByTagName(tagName);
            Guid re = Guid.Empty;

            if (hashTag != null)
            {
                //exists tagName
                re = Create(
                    hashTag.HashTagId,
                    userId, 
                    comment);
            }
            else
            {
                //not exists
                var hashTagId = bnHashTag.Create(
                    userId,
                    EHashtag_Type.Public,
                    null,
                    tagName,
                    null, null, null);

                if (hashTagId != Guid.Empty)
                {
                    re = Create(
                        hashTagId,
                        userId,
                        comment);
                }
            }

            return re;
        }
        public bool TagedToSome(string tagNames, Guid userId)
        {
            var tagList = bn_HashTag.DivTags(tagNames);

            //transaction here - but don't right now.
            foreach (var tagName in tagList)
            {
                Tag(tagName, userId, null);
            }

            return true;
        }

        public List<pb_ProductTag> GetByProductId()
        {
            var productTags = db.pb_ProductTag_GetbyProductId(productId)
                .OrderBy(m => m.pb_HashTag.TagName)
                .ToList();

            return productTags;
        }
        public string GetTagedList()
        {
            string tagList = "";
            var tags = GetByProductId();

            foreach (var item in tags)
            {
                tagList += string.Format("#{0} ", item.pb_HashTag.TagName);
            }

            return tagList;
        }

        //for system tag
        public bool IsExistsInUser(Guid userId, Guid hashtagId)
        {
            var result = GetByUserAndTagId(userId, hashtagId);
            if (result != null)
                return true;
            else
                return false;
        }
    }
}
