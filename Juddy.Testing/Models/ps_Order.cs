using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PbData.Entities;

namespace Juddy.Testing.Models
{
    public class ps_Order
    {
        public pb_Order Order { get; set; }
        public pb_Product Product { get; set; }

        public ps_Order()
        {
            Order = new pb_Order();
            Product = new pb_Product();
        }
    }
}