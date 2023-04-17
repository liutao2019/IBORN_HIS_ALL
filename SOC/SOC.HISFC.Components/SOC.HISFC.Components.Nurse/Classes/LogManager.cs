using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Components.Nurse.Classes
{
    /// <summary>
    /// 日志记录
    /// </summary>
    public class LogManager
    {
        public static void Write(string logInfo)
        {
            //检查目录是否存在
            if (System.IO.Directory.Exists("./Log/Nurse/Inject") == false)
            {
                System.IO.Directory.CreateDirectory("./Log/Nurse/Inject");
            }
            //保存一周的日志
            System.IO.File.Delete("./Log/Nurse/Inject/" + DateTime.Now.AddDays(-7).ToString("yyyyMMdd") + ".LOG");
            string name = DateTime.Now.ToString("yyyyMMdd");
            System.IO.StreamWriter w = new System.IO.StreamWriter("./Log/Nurse/Inject/" + name + ".LOG", true);
            w.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + logInfo);
            w.Flush();
            w.Close();
        }
    }
}
