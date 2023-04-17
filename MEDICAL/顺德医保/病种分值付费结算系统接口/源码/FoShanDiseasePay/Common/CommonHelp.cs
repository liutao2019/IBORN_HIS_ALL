using System;
using System.Data;
using System.Security.Cryptography;

namespace FoShanDiseasePay.Common
{
    /// <summary>
    /// CommonHelp 的摘要说明。
    /// </summary>
    public class CommonHelp
    {
        public CommonHelp()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private static string xmlhead = "";

        private static string xmlfoot = "";

        public static string GetXmlStrByDataSet(DataSet ds)
        {
            // ds.to
            return ds.GetXml();
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            return BitConverter.ToString(result).Replace("-", "");
        }
    }
}
