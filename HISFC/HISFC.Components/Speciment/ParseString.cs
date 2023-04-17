using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;


namespace FS.HISFC.Components.Speciment
{

    public static class ParseString
    {
        private static DisTypeManage disTypeManage = new DisTypeManage();
        private static SpecTypeManage specTypeManage = new SpecTypeManage();
     
        /// <summary>
        /// 用逗号分割字符串
        /// </summary>
        /// <param name="parseString">需要解析的字符串</param>
        /// <param name="type">字符串中存放的类型</param>
        /// <returns></returns>
        public static List<string> ParseByComma(string parseString, string type)
        {
            string[] parsed = parseString.Split(',');
            List<string> listParseString = new List<string>();
            switch (type)
            {
                case "SpecType":
                    foreach (string p in parsed)
                    {
                        listParseString.Add(specTypeManage.GetSpecTypeById(p).SpecTypeName); 
                    }
                    break;
                case "DiseaseType":
                    foreach (string d in parsed)
                    {
                        listParseString.Add(disTypeManage.SelectDisByID(d).DiseaseName);
                    }
                    break;
                default:
                    foreach (string s in parsed)
                    {
                        listParseString.Add(s);
                    }
                    break;
            }
            return listParseString;
        }
    }

}
