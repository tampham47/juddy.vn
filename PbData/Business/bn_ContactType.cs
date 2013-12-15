using PbData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PbData.Business
{
    public class bn_ContactType
    {
        private pb_Entities db = new pb_Entities();

        public bn_ContactType(pb_Entities connection = null, bool isLazy = true)
        {
            if (connection != null)
            {
                db = connection;
            }
            db.Configuration.LazyLoadingEnabled = isLazy;
        }

        public List<pb_ContactType> GetAll()
        {
            var contactTypes = db.pb_ContactType
                .OrderBy(m => m.Order)
                .ToList();

            return contactTypes;
        }
    }
}
