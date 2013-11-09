using PbData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PbData.Business
{
    public class bn_Comment
    {
        private pb_Entities db = new pb_Entities();
        public bn_Comment(pb_Entities connection = null)
        {
            if (connection != null)
            {
                db = connection;
            }
        }
    }
}
