using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Public
{
    /// <summary>
    /// 日志记录
    /// </summary>
    public class LogManager
    {
        /// <summary>
        /// 记录日志的天数，更早的将会删除
        /// </summary>
        private static int logDays = 30;

        /// <summary>
        /// 记录日志的天数，更早的将会删除
        /// </summary>
        public static int LogDays
        {
            get
            {
                return logDays;
            }
            set
            {
                logDays = value;
            }
        }

        /// <summary>
        /// 医护日志记录
        /// </summary>
        /// <param name="logPath">日志存放路径</param>
        /// <param name="logInfo">日志内容</param>
        public static int Write(string logPath, string logInfo)
        {
            try
            {
                logPath = "./Log/" + logPath.TrimStart('/');

                //检查目录是否存在
                if (System.IO.Directory.Exists(logPath) == false)
                {
                    System.IO.Directory.CreateDirectory(logPath);
                }

                //保存一周的日志
                System.IO.File.Delete(logPath + DateTime.Now.AddDays(0D - logDays).ToString("yyyyMMdd") + ".LOG");
                string strDate = DateTime.Now.ToString("yyyyMMdd");

                logPath = logPath.TrimStart('/') + "/";

                System.IO.StreamWriter w = new System.IO.StreamWriter(logPath + strDate + ".LOG", true);
                w.WriteLine("系统日志 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + logInfo);
                w.Flush();
                w.Close();
            }
            catch
            {
                return -1;
            }

            return 1;
        }

        public static void Write(string logInfo)
        {
            Write("HL7/HL7", logInfo);
        }
    }
}
