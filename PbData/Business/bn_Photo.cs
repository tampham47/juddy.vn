using PbData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PbData.Utility;

namespace PbData.Business
{
    public class bn_Photo
    {
        private const int width = 230;
        private const int height = 400;

        private pb_Entities db = new pb_Entities();
        public bn_Photo(pb_Entities connection = null)
        {
            if (connection != null)
            {
                db = connection;
            }
        }

        public Guid Create(Nullable<System.Guid> userId, Nullable<System.Guid> productId, string imagePath)
        {
            var photoId = Guid.NewGuid();
            int re = (int)db.pb_Photo_Create(
                photoId,
                userId,
                productId,
                imagePath).Single();

            if (re >= 0)
                return photoId;
            else
                return Guid.Empty;
        }
        public int Delete(Guid photoId)
        {
            int re = (int)db.pb_Photo_Delete(photoId).Single();
            return re;
        }
        public int UpdateProductId(Guid photoId, Guid productId)
        {
            var re = (int)db.pb_Photo_UpdateProductId(
                photoId,
                productId).Single();

            return re;
        }
        public int UpdateStatus(Guid photoId, byte status)
        {
            var re = (int)db.pb_Photo_UpdateStatus(
                photoId, status).Single();

            return re;
        }

        public int UpdateImageW1(Guid photoId, string imageW1)
        {
            var re = (int)db.pb_Photo_UpdateImageW1(
                photoId,
                imageW1).Single();

            return re;
        }
        public int UpdateImageH1(Guid photoId, string imageH1)
        {
            var re = (int)db.pb_Photo_UpdateImageH1(
                photoId,
                imageH1).Single();

            return re;
        }
        public int UpdateImageH2(Guid photoId, string imageH2)
        {
            var re = (int)db.pb_Photo_UpdateImageH2(
                photoId,
                imageH2).Single();

            return re;
        }

        public bool RezisePhoto(Guid photoId, string serverPath)
        {
            //bn_Image.FixedWidth(

            return false;
        }

        public pb_Photo GetById(Guid photoId)
        {
            var photos = db.pb_Photo_GetbyId(photoId).ToList();

            if (photos.Count > 0)
                return photos.First();
            else
                return null;
        }
        public List<pb_Photo> GetByProductId(Guid productId)
        {
            var photos = db.pb_Photo_GetbyProductId(productId).ToList();

            return photos;
        }
        public List<pb_Photo> GetAll()
        {
            var result = db.pb_Photo.ToList();

            return result;
        }

        //test
        public int DeleteByProductId(Guid productId)
        {
            return 0;
        }
    }

    //enum of photo status
    public static class EPhoto_Status
    {
        public const byte Cover = 0;
        public const byte Image = 1;
    }
}
