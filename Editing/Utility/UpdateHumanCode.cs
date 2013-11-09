using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PbData.Business;
using PbData.Utility;

namespace Editing.Utility
{
    public class UpdateHumanCode
    {
        public string CurrentCode()
        {
            bn_Reference bnReference = new bn_Reference();
            var model =  bnReference.GetByName(bn_Reference.HumanCode);

            return model.Value;
        }

        public int Update()
        {
            bn_Product bnProduct = new bn_Product();
            var products = bnProduct.GetAll()
                .Where(m => m.HumanCode == "00000")
                .ToList();

            foreach (var item in products)
            {
                bnProduct.UpdateHumanCode(
                    item.ProductId,
                    bn_HumanCode.GetCode());

                Console.WriteLine(item.ProductId);
            }

            return 0;
        }
    }
}
