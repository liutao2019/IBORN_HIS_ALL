using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Neusoft.SOC.Local.EMR.ZDLY.Class
{
    public class GetEmrLog
    {
        #region 输出跟踪
        public static void WriteException(String logMessage)
        {
            StreamWriter sw = File.AppendText("emrexception.txt");
            sw.Write("\r\nLog Entry : ");
            sw.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),DateTime.Now.ToLongDateString());
            sw.WriteLine("  :");
            sw.WriteLine("  :{0}", logMessage);
            sw.WriteLine("-------------------------------");
            sw.Flush();
            sw.Close();
        }

        public static void WriteSql(String logMessage)
        {
            StreamWriter sw = File.AppendText("emrsql.txt");
            sw.Write("\r\nLog Entry : ");
            sw.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            sw.WriteLine("  :");
            sw.WriteLine("  :{0}", logMessage);
            sw.WriteLine("-------------------------------");
            sw.Flush();
            sw.Close();
        }
        #endregion
    }
}
