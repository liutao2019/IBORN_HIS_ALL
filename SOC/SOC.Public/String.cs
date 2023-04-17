using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Public
{
    /// <summary>
    /// [功能描述: 字符串函数]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-1]<br></br>
    /// </summary>
    public class String
    {
        /// <summary>
        /// 补充字符
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="totalWith">字符串长度（1个英文字母代表的长度为基本单位）</param>
        /// <param name="paddingChar">补充字符</param>
        /// <returns>字符串</returns>
        public static string PadRight(string value, int totalWith, char paddingChar)
        {
            if (value == null)
            {
                return null;
            }
            int length = Length(value);
            if (length >= totalWith)
            {
                return value;
            }
            for (int index = 0; index < totalWith - length; index++)
            {
                value += paddingChar.ToString();
            }
            return value;
        }

        /// <summary>
        /// 获取字符串相对于英文字母的长度（汉字两个字符）
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns></returns>
        public static int Length(string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                return 0;
            }

            int length = 0;
            for (int index = 0; index < value.Length; index++)
            {
                if (value[index] < 128)
                {
                    length = length + 1;
                }
                else
                {
                    length = length + 2;
                }
            }
            return length;
        }
    }
}
