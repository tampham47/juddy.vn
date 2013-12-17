using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PbData.Entities;

namespace PbData.Business
{
    public static class EHashtag_Type
    {
        public const byte Public = 1;
        public const byte Private = 0;
        public const byte System = 2;
    }

    public class bn_HashTag
    {
        //type: publish 1, private 0, system 2

        private pb_Entities db = new pb_Entities();
        public bn_HashTag(pb_Entities connection = null, bool isLazy = true)
        {
            if (connection != null)
            {
                db = connection;
            }
            db.Configuration.LazyLoadingEnabled = isLazy;
        }

        public static List<string> DivTags(string tags)
        {
            List<string> temp;
            List<string> tagList = new List<string>();
            temp = tags.Split(new[] { '#', ' ', ',', '@' }).ToList();

            foreach (var item in temp)
            {
                if (item != "")
                {
                    tagList.Add(item.Replace(" ", ""));
                }
            }

            bn_HashTag bnHashTag = new bn_HashTag();
            bnHashTag.Create(tagList);

            return tagList;
        }
        public static string MergerTag(List<string> tagList)
        {
            string result = "";
            foreach (var item in tagList)
            {
                result += String.Format("#{0} ", item);
            }

            return result.Trim();
        }

        public Guid Create(Nullable<System.Guid> userId, Nullable<byte> type, string textName, string tagName, string iconPath, string description, string userTypes)
        {
            if (!IsExists(tagName))
            {
                var hashId = Guid.NewGuid();
                int re = (int)db.pb_HashTag_Create(
                    hashId,
                    userId,
                    type,
                    textName,
                    tagName.Trim().ToLower(),
                    iconPath,
                    description,
                    userTypes).Single();

                if (re >= 0)
                    return hashId;
                else
                    return Guid.Empty;
            }
            else
            {
                return Guid.Empty;
            }
        }
        public Guid Create(string tagName)
        {
            var result = Create(null, EHashtag_Type.Public,
                tagName, tagName,
                null, null, null);

            return result;
        }

        public int Create(List<string> tagList)
        {
            int result = 0;
            foreach (var item in tagList)
            {
                Create(item);
                result++;
            }

            return result;
        }
        public int Update(Guid hashTagId, byte? type, string iconPath, string description, string userTypes)
        {
            var re = (int)db.pb_HashTag_Update(
                hashTagId,
                type,
                iconPath,
                description,
                userTypes).Single();

            return re;
        }
        public bool IsExists(string tagName)
        {
            var tag = db.pb_HashTag_GetbyTagName(tagName.ToLower()).ToList();

            if (tag.Count > 0)
                return true;
            else
                return false;
        }
        public int IncrViews(Guid id)
        {
            return (int)db.pb_HashTag_IncrViews(id).Single();
        }

        public pb_HashTag GetByTagName(string tagName)
        {
            if (!IsExists(tagName))
            {
                Create(
                    null,
                    EHashtag_Type.Public,
                    null, tagName, null, null, null);
            }

            var hashTags = db.pb_HashTag_GetbyTagName(tagName.ToLower()).ToList();

            if (hashTags.Count > 0)
            {
                var tag = hashTags.First();
                return tag;
            }
            else
                return null;
        }
        public pb_HashTag GetById(Guid hashtagId)
        {
            var result = (
                    from get in db.pb_HashTag
                    where get.HashTagId == hashtagId
                    select get
                )
                .ToList();

            if (result.Count > 0)
                return result.First();
            else
                return null;
        }
        public List<pb_HashTag> GetByProductId(Guid productId)
        {
            var result = db.pb_HashTag_GetbyProductId(productId)
                    .Where(m => m.Type != EHashtag_Type.System)
                    .ToList();

            return result;
        }
        public List<pb_HashTag> GetByUserId(Guid userId, byte type)
        {
            var result = db.tp_HashTag_GetbyUserId(userId, type).ToList();

            return result;
        }

        //Than
        public List<pb_HashTag> GetAllTag()
        {
            return db.pb_HashTag.ToList();
        }
        public List<pb_HashTag> GetListInPage(int page, int count = 24)
        {
            int numSkip = (page - 1) * count;
            return db.pb_HashTag.ToList().Skip(numSkip).Take(count).ToList();

        }
        public int GetCountAll()
        {
            return db.pb_HashTag.Count();
        }

    }
}