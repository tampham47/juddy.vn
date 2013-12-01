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

        //convert UserTypes to categories.
        public List<pb_Category> GetCategories()
        {
            var result = new List<pb_Category>();
            if (UserTypes == null) return result;

            List<string> tagList = new List<string>();
            string tag, text, str;
            int index;

            var temp = UserTypes.Split(new[] { ',' }).ToList();
            foreach (var item in temp)
            {
                str = item.Trim();
                index = str.IndexOf(' ');
                tag = str.Substring(0, index);

                text = str.Substring(index, str.Length - index);
                text = text.Replace("(", "");
                text = text.Replace(")", "");

                result.Add(new pb_Category
                {
                    TagName = tag,
                    TextName = text
                });
            }

            return result;
        }
    }

    public class pb_Category
    {
        public string TagName { get; set; }
        public string TextName { get; set; }
    }
}
