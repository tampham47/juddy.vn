using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PbData.Entities;

namespace Juddy.Testing.Models
{
    public class ps_ContactType
    {
        public List<pb_ContactType> ContactTypes { get; set; }
        public pb_UserContact UserContact { get; set; }

        public ps_ContactType()
        {
            ContactTypes = new List<pb_ContactType>();
            UserContact = new pb_UserContact();
        }
    }
}