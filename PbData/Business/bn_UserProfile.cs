using PbData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PbData.Business
{
    public class bn_UserProfile
    {
        private pb_Entities db = new pb_Entities();
        public bn_UserProfile(pb_Entities connection = null)
        {
            if (connection != null)
            {
                db = connection;
            }
        }

        public pb_UserProfile GetByUserName(string userName)
        {
            var users = db.pb_UserProfile_GetbyUserName(userName).ToList();

            if (users.Count > 0)
                return users.First();
            else
                return null;
        }
        public pb_UserProfile GetById(Guid userId)
        {
            var users = db.pb_UserProfile_GetbyId(userId).ToList();

            if (users.Count > 0)
                return users.First();
            else
                return null;
        }

        public bool IsExists(string userName)
        {
            var user = GetByUserName(userName);

            if (user != null)
                return true;
            else
                return false;
        }

        public int Update(Guid userId, string firstName, string lastName, DateTime? dateOfBirth)
        {
            var re = (int)db.pb_UserProfile_Update(
                userId,
                firstName,
                lastName,
                dateOfBirth).Single();

            return re;
        }
        public int UpdateAvatar(Guid userId, string avatar)
        {
            var re = (int)db.pb_UserProfile_UpdateAvatar(
                userId,
                avatar).Single();

            return re;
        }

        //filter

    }
}
