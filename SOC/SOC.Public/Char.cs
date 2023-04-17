using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Public
{
    public class Char
    {
    
        /// <summary>
        /// 判断字符是否为字母
        /// </summary>
        /// <param name="c">字符</param>
        /// <returns></returns>
        public static bool IsLetter(char c)
        {
            if (c >= 65 && c <= 90)
            {
                return true;
            }
            if (c >= 97 && c <= 122)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断字符是否为字母或数字
        /// </summary>
        /// <param name="c">字符</param>
        /// <returns></returns>
        public static bool IsLetterOrNumber(char c)
        {
            if (c >= 65 && c <= 90)
            {
                return true;
            }
            if (c >= 97 && c <= 122)
            {
                return true;
            }
            if (c >= 48 && c <= 57)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断字符是数字
        /// </summary>
        /// <param name="c">字符</param>
        /// <returns></returns>
        public static bool IsNumber(char c)
        {
            if (c >= 48 && c <= 57)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是否为标点或表达式符合
        /// </summary>
        /// <param name="c">字符</param>
        /// <returns></returns>
        public static bool IsSign(char c)
        {
            if (c >= 32 && c <= 47)
            {
                return true;
            }
            if (c >= 58 && c <= 64)
            {
                return true;
            }
            if (c >= 91 && c <= 96)
            {
                return true;
            }
            if (c >= 123 && c <= 127)
            {
                return true;
            }
            return false;
        }


    }
}
