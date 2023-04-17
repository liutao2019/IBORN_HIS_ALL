using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;

namespace ProvinceAcrossSI
{
    public class Log
    {
        //string path = @".\Plugins\SI\ZhuHaiYDLog\";
        string fileName = "";
        /// <summary>
        /// 日志目录路径
        /// </summary>
        public string path { get; set; }

        private System.IO.TextWriter output;

        public void WriteInPut(string ser, string dep, string cas, string parXML, string cer, string sig)
        {
            fileName = System.DateTime.Now.ToString("yyyy-MM-dd") + "log.txt";
            string absPath = path + fileName;

            string fgx = "\n---------------------------------------------------------------------------";
            string dt = "\n" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string str = "\n服务名称ser  {0}\n机构编号dep  {1}\n业务名称cas  {2}\n数字证书cer  {3}";
            str = string.Format(str, ser, dep, cas, cer);
            string inputPar = "\n业务参数parXML\n" + parXML;

            bool isFileExists = File.Exists(absPath);

            if (isFileExists)
            {
                try
                {
                    output = File.AppendText(absPath);
                    output.WriteLine(fgx + dt + str + inputPar);
                    output.Close();
                }
                catch (Exception ex)
                { }
            }
            else
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                try
                {
                    output = File.AppendText(absPath);
                    output.WriteLine(dt + str + inputPar);
                    output.Close();
                }
                catch (Exception ex)
                { }
            }
        }

        public void WriteOutPut(string retXML)
        {
            fileName = System.DateTime.Now.ToString("yyyy-MM-dd") + "log.txt";
            string absPath = path + fileName;

            string dt = "\n" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string outPar = "\n返回结果retXML\n" + retXML + "\n";

            bool isFileExists = File.Exists(absPath);

            if (isFileExists)
            {
                try
                {
                    output = File.AppendText(absPath);
                    output.WriteLine(dt + outPar);
                    output.Close();
                }
                catch (Exception ex)
                { }
            }
            else
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                try
                {
                    output = File.AppendText(absPath);
                    output.WriteLine(dt + outPar);
                    output.Close();
                }
                catch (Exception ex)
                { }
            }
        }

        #region 跟踪日志
        /// <summary>
        /// 文件名
        /// </summary>
        private string cpFileName = "";

        /// <summary>
        /// 路径 @".\Plugins\SI\ChangePactLog\"
        /// </summary>
        private string cpPath = @".\Plugins\SI\ChangePactLog\";

        public string CpPath
        {
            get { return cpPath; }
            set { cpPath = value; }
        }

        public void WirteCpLog(string logStr)
        {
            this.cpFileName = System.DateTime.Now.ToString("yyyy-MM-dd") + "-cplog.txt";
            string logPath = this.cpPath + this.cpFileName;

            string dt = "\n当前时间：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (File.Exists(logPath))
            {
                try
                {
                    output = File.AppendText(logPath);
                    output.WriteLine(dt + "\n" + logStr);
                    output.Close();
                }
                catch (Exception ex)
                { }
            }
            else
            {
                if (!Directory.Exists(cpPath))
                {
                    Directory.CreateDirectory(cpPath);
                }
                try
                {
                    output = File.AppendText(logPath);
                    output.WriteLine(dt + "\n" + logStr);
                    output.Close();
                }
                catch (Exception ex)
                { }
            }
        }

        #endregion
    }
}
