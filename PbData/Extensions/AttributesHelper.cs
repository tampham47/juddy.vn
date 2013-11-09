using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PbData.Business;

namespace PbData.Extensions
{
    public static class AttributesHelper
    {
        public static string ParseToText(this ESpecialTag value)
        {
            var descriptionAttribute =
                (DescriptionAttribute)value.GetType()
                    .GetField(value.ToString())
                    .GetCustomAttributes(false)
                    .Where(a => a is DescriptionAttribute
                ).FirstOrDefault();

            return descriptionAttribute != null ? 
                descriptionAttribute.Description : 
                value.ToString();
        }
    }
}
