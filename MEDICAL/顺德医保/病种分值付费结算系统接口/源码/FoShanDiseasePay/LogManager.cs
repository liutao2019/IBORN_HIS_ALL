﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoShanDiseasePay
{
    /// <summary>
    /// 日志管理类
    /// </summary>
    public class LogManager
    {
        /// <summary>
        /// 传递过去的参数或者返回来的参数
        /// </summary>
        /// <param name="p"></param>
        public static void WriteLog(string p)
        {
            //检查目录是否存在
            if (System.IO.Directory.Exists("./Plugins/SI/FoShanDiseasePay") == false)
            {
                System.IO.Directory.CreateDirectory("./Plugins/SI/FoShanDiseasePay");
            }

            //保存一周的日志
            System.IO.File.Delete("./Plugins/SI/FoShanDiseasePay/" + DateTime.Now.AddDays(-7).ToString("yyyyMMdd") + ".LOG");

            string name = DateTime.Now.ToString("yyyyMMdd");
            System.IO.StreamWriter w = new System.IO.StreamWriter("./Plugins/SI/FoShanDiseasePay/" + name + ".LOG", true);
            w.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + p);
            w.Flush();
            w.Close();
        }
    }
}
