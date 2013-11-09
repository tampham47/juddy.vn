using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PbData.Entities
{
    public partial class pb_Order
    {
        public string TotalPrice()
        {
            double total = Amount * Price;
            string strTotal = total.ToString() + " VNĐ";

            return strTotal;
        }
    }
}
