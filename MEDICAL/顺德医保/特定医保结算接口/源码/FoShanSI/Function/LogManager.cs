using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FoShanSI.Function
{
    /// <summary>
    /// 日志记录
    /// </summary>
    class LogManager
    {
        public static void Write(string p)
        {
            //检查目录是否存在
            if (System.IO.Directory.Exists("./Plugins/SI/FoShanSILog") == false)
            {
                System.IO.Directory.CreateDirectory("./Plugins/SI/FoShanSILog");
            }
            //保存一周的日志
            System.IO.File.Delete("./Plugins/SI/FoShanSILog/" + DateTime.Now.AddDays(-7).ToString("yyyyMMdd") + ".LOG");
            string name = DateTime.Now.ToString("yyyyMMdd");
            System.IO.StreamWriter w = new System.IO.StreamWriter("./Plugins/SI/FoShanSILog/" + name + ".LOG", true);
            w.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + p);
            w.Flush();
            w.Close();
        }
    }
}
