using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PbData.Entities;

namespace PbData.Business
{
    public class bn_Reference
    {
        public const string HumanCode = "HumanCode";

        private pb_Entities db = new pb_Entities();
        public bn_Reference(pb_Entities connection = null)
        {
            if (connection != null)
            {
                db = connection;
            }
        }

        public int Create(string name, string value)
        {
            var result = db.pb_Reference_Create(
                name, value).Single();

            return (int)result;
        }
        public int Update(string name, string value)
        {
            var result = db.pb_Reference_Update(
                name, value).Single();

            return (int)result;
        }
        public int Delete(string name)
        {
            var result = db.pb_Reference_Delete(name)
                .Single();

            return (int)result;
        }

        public pb_Reference GetByName(string name)
        {
            var result = db.pb_Reference_GetByName(name).ToList();

            if (result.Count > 0)
                return result.First();
            else
                return null;
        }
    }
}
