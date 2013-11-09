using PbData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PbData.Business
{
    public class bn_UserContact
    {
        private pb_Entities db = new pb_Entities();
        public bn_UserContact(pb_Entities connection = null)
        {
            if (connection != null)
            {
                db = connection;
            }
        }

        public Guid Create(Guid userId, int contactTypeId, string contactName)
        {
            var userContactId = Guid.NewGuid();
            var re = (int)db.pb_UserContact_Create(
                userContactId,
                userId,
                contactTypeId,
                contactName).Single();

            if (re > 0)
                return userContactId;
            else
                return Guid.Empty;
        }
        public int Update(Guid userContactId, int contactTypeId, string contactName)
        {
            var re = (int)db.pb_UserContact_Update(
                userContactId,
                contactTypeId,
                contactName).Single();

            return re;
        }
        public int Delete(Guid userContactId)
        {
            var re = (int)db.pb_UserContact_Delete(userContactId).Single();

            return re;
        }

        public List<pb_UserContact> GetByUserId(Guid userId)
        {
            var userContacts = db.pb_UserContact_GetbyUserId(userId)
                .OrderBy(m => m.pb_ContactType.Order)
                .ToList();

            return userContacts;
        }
    }
}
