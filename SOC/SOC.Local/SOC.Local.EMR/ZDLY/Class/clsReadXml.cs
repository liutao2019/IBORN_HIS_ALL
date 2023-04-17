using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
namespace Neusoft.SOC.Local.EMR.ZDLY.Class
{
    class clsReadXml
    {

        #region 取电子病历weburl
        public static string GetEmrWebUrl()
        {
            string url = "null";
            String str = AppDomain.CurrentDomain.BaseDirectory;
            str = str + "\\WebServerConfig.xml";
            if (!System.IO.File.Exists(str))
            {
                return url;
            };
            XElement xml=XElement.Load(str);
            url="http://"+xml.Element("ServerIP").Value.ToString();
            url = url + ":" + xml.Element("ServerPort").Value.ToString();
            url = url + "Default.aspx";
            return url;
        }

        #endregion
    }
}
