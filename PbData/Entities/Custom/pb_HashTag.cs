using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PbData.Entities
{
    public partial class pb_HashTag
    {
        private static string[] IconList = { "C", "F", "G", "S", "Y" };
        private static string[] ThemeList = { "theme-d", "theme-e", "theme-f", "theme-g", "theme-h" };
        private static Random random = new Random((int)DateTime.Now.Ticks);

        public string Icon
        {
            get
            {
                if (IconPath == "Default-Tag-Y.png" || IconPath == null)
                {
                    int index = random.Next(0, 5);

                    return string.Format("Default-Tag-{0}.png", IconList[index]);
                }
                else
                {
                    return IconPath;
                }
            }
        }
        public string Theme
        {
            get
            {
                var index = random.Next(0, 5);

                return ThemeList[index];
            }
        }
    }
}
