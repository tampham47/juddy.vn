using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PbData.Utility;
using PbData.Business;
using Editing.Utility;

namespace Editing
{
    class Program
    {
        static void Main(string[] args)
        {
            //UpdateHumanCode update = new UpdateHumanCode();
            //update.Update();

            bn_Photo bnPhoto = new bn_Photo();
            var photos = bnPhoto.GetAll();

            foreach (var item in photos)
            {
                Console.WriteLine(string.Format("{0}, {1}",
                    item.ImageFixW1(),
                    item.ImageFixH1()));
            }
            Console.WriteLine("done.");

            return;
        }

        static bool TestCode()
        {
            bn_Reference bnReference = new bn_Reference();

            Console.WriteLine("Start.");
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(bn_HumanCode.GetCode());
            }
            Console.WriteLine("Done.");
            Console.ReadKey(true);

            return true;
        }
    }
}
