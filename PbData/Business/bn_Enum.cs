using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PbData.Business
{
    public enum ESpecialTag
    {
        [Description("pbox_want")]
        Want,

        [Description("pbox_like")]
        Like,

        [Description("pbox_favourite")]
        Favourite,

        [Description("pbox_discount")]
        Discount,

        [Description("pbox_soldout")]
        SoldOut,

        [Description("pbox_report")]
        Report,

        [Description("pbox_shoppingcart")]
        ShoppingCart
    }
}
