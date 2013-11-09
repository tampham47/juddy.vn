using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PbData.Utility
{
    public static class bn_HumanPrice
    {
        //1900000 -> 1.900K
        public static string GetShortPrice(float price)
        {
            float temp = (int)price / 1000;
            string resuft = GetPrice(temp);
            resuft = resuft.Insert(resuft.Length, "K");
            return resuft;
        }

        //1900000 -> 1.900.000
        public static string GetPrice(float price)
        {
            string resuft = String.Format("{0:0,0}", price);
            resuft = resuft.Replace(',', '.');
            return resuft;
        }
    }
}
