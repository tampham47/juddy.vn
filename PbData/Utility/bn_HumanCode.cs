using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PbData.Business;

namespace PbData.Utility
{
    public static class bn_HumanCode
    {
        //just call when have a new product
        public static string GetCode()
        {
            bn_Reference bnReference = new bn_Reference();
            var humanCode = bnReference.GetByName(bn_Reference.HumanCode);

            var nextCode = GetNextCode(humanCode.Value);
            bnReference.Update(bn_Reference.HumanCode, nextCode);

            return nextCode;
        }

        private static string GetNextCode(string humanCode)
        {
            string temp = humanCode.ToUpper();

            for (int i = 0; i < temp.Length; i++)
            {
                if (i + 1 == temp.Length)
                {
                    temp = ReplaceOfIndex(temp, i, NextChar(temp[i]));
                    break;
                }
                if (temp[i + 1].Equals('Z'))
                {
                    if (CheckString(temp.Substring(i + 1)))
                        temp = ReplaceOfIndex(temp, i, NextChar(temp[i]));
                }
            }

            return temp;
        }

        /// <summary>
        /// Replace a char at index of string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <param name="reChar"></param>
        /// <returns></returns>
        private static string ReplaceOfIndex(String source, int index, String reChar)
        {
            string temp = "";
            int i = 0;
            while (i < source.Length)
            {
                if (i == index)
                    temp = temp.Insert(i, reChar);
                else
                    temp = temp.Insert(i, source[i].ToString());
                i++;
            }
            return temp;
        }

        /// <summary>
        /// increas value a char
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private static string NextChar(char c)
        {
            int i = c + 0;
            if (i == 57)
                i = 65;
            else
                if (i == 90)
                    i = 48;
                else
                    i++;
            return Char.ConvertFromUtf32(i);
        }

        /// <summary>
        /// check string all char is 'Z'
        /// </summary>
        /// <param name="cString"></param>
        /// <returns></returns>
        private static bool CheckString(String cString)
        {
            for (int i = 0; i < cString.Length; i++)
            {
                if (!cString[i].Equals('Z'))
                    return false;
            }
            return true;
        }
    }
}