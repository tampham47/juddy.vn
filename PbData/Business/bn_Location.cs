using PbData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PbData.Business
{
    public class bn_Location
    {
        private pb_Entities db = new pb_Entities();

        public bn_Location(pb_Entities connection = null, bool isLazy = true)
        {
            if (connection != null)
            {
                db = connection;
            }
            db.Configuration.LazyLoadingEnabled = isLazy;
        }

        public Guid Create(Guid userId, double latitude, double longitude, string address)
        {
            var locationId = Guid.NewGuid();
            var re = db.pb_Location_Create(
                locationId,
                userId,
                latitude,
                longitude,
                address).Single();

            if (re >= 0)
                return locationId;
            else
                return Guid.Empty;
        }
        public int Update(Guid locationId, double latitude, double longitude, string address)
        {
            var re = (int)db.pb_Location_Update(
                locationId,
                latitude,
                longitude,
                address).Single();

            return re;
        }
        public int Delete(Guid locationId)
        {
            var re = (int)db.pb_Location_Delete(locationId).Single();

            return re;
        }

        public pb_Location GetById(Guid locationId)
        {
            var locations = db.pb_Location_GetbyId(locationId).ToList();

            if (locations.Count > 0)
                return locations.First();
            else
                return null;
        }

        public List<pb_Location> GetByUserId(Guid userId)
        {
            var locations = db.pb_Location_GetbyUserId(userId).ToList();

            return locations;
        }
    }
}
