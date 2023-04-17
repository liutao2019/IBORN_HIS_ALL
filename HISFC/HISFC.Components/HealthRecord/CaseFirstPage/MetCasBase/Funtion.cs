using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    class Funtion
    {
        /// <summary>
        /// 替换单引号
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="changeType">转换类型 true：' 转 ’ false ’转 '</param>
        /// <returns></returns>
        public static string ReplaceSingleQuotationMarks(string str, bool changeType)
        {
            string ret = string.Empty;
            if (changeType)
            {
                ret = str.Replace("'", "’");
            }
            else
            {
                ret = str.Replace("’", "'");
            }
            return ret;
        }
    }
}
