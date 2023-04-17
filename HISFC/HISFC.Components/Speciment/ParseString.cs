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
        /// �ö��ŷָ��ַ���
        /// </summary>
        /// <param name="parseString">��Ҫ�������ַ���</param>
        /// <param name="type">�ַ����д�ŵ�����</param>
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
