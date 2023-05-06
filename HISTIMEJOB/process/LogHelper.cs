using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace OADAL
{
    public class LogHelper
    {
        private static object obj = new object();

        private static string filePath = "/ErrorLog/";// "/ErrorLog/";//ConfigurationManager.AppSettings["LogPath"];// Utility.GetSettingByKey("LogPath");// GetConfig.GetValueByKey("LogPath");//日志路径
        private static bool isLog = true;//是否记录日志

        /// <summary>
        /// 记录日志至文本文件
        /// </summary>
        /// <param name="errorMessage">记录的内容</param>
        public static void Write(string errorMessage)
        {
            if (!isLog)
            {
                return;
            }

            string virtualPath = string.Concat(filePath, DateTime.Now.ToString("yyyyMMdd"), ".txt");
            string path = string.Concat(AppDomain.CurrentDomain.BaseDirectory, virtualPath);//如果是物理路径，请屏蔽这句话

            lock (obj)
            {
                if (File.Exists(path))
                {
                    using (StreamWriter sr = File.AppendText(path))
                    {
                        sr.WriteLine(string.Concat(Environment.NewLine, DateTime.Now.ToString(), ":", errorMessage));
                        sr.Close();
                    }
                }
                else
                {

                    using (StreamWriter sr = File.CreateText(path))
                    {
                        sr.WriteLine(string.Concat(Environment.NewLine, DateTime.Now.ToString(), ":", errorMessage));
                        sr.Close();
                    }
                }
            }
        }

        public static void WriteInsertSuceess(string errorMessage)
        {
            if (!isLog)
            {
                return;
            }
            string virtualPath = string.Concat("/Log/", DateTime.Now.ToString("yyyyMMdd") + "OA插入成功sql", ".txt");
            string path = string.Concat(AppDomain.CurrentDomain.BaseDirectory, virtualPath);//如果是物理路径，请屏蔽这句话
            lock (obj)
            {
                if (File.Exists(path))
                {
                    using (StreamWriter sr = File.AppendText(path))
                    {
                        sr.WriteLine(string.Concat(Environment.NewLine, DateTime.Now.ToString(), ":", errorMessage));
                        sr.Close();
                    }
                }
                else
                {

                    using (StreamWriter sr = File.CreateText(path))
                    {
                        sr.WriteLine(string.Concat(Environment.NewLine, DateTime.Now.ToString(), ":", errorMessage));
                        sr.Close();
                    }
                }
            }
        }

        public static void WriteInsertFail(string errorMessage)
        {
            if (!isLog)
            {
                return;
            }
            string virtualPath = string.Concat("/Log/", DateTime.Now.ToString("yyyyMMdd") + "OA插入异常sql", ".txt");
            string path = string.Concat(AppDomain.CurrentDomain.BaseDirectory, virtualPath);//如果是物理路径，请屏蔽这句话
            lock (obj)
            {
                if (File.Exists(path))
                {
                    using (StreamWriter sr = File.AppendText(path))
                    {
                        sr.WriteLine(string.Concat(Environment.NewLine, DateTime.Now.ToString(), ":", errorMessage));
                        sr.Close();
                    }
                }
                else
                {

                    using (StreamWriter sr = File.CreateText(path))
                    {
                        sr.WriteLine(string.Concat(Environment.NewLine, DateTime.Now.ToString(), ":", errorMessage));
                        sr.Close();
                    }
                }
            }
        }
    }
}
