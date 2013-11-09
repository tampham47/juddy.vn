using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PbData.Business;
using PbData.Entities;

namespace PencilBox.Models
{
    public class ps_UserContact
    {
        public pb_UserProfile UserProfile { get; set; }
        public List<pb_UserContact> Contacts { get; set; }

        public ps_UserContact()
        {
            Contacts = new List<pb_UserContact>();
        }
    }
}