using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace API.GZSI.Class
{
    public class Log
    {
        /// <summary>
        /// 日志文件名称
        /// </summary>
        private string fileName = "";

        /// <summary>
        /// 日志目录路径
        /// </summary>
        public string path = System.AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
        ////System.Environment.CurrentDirectory + "\\Logs";

        /// <summary>
        /// 头部格式
        /// </summary>
        public string LogHeadFormat = "x-tif-paasid:{0} \r\nx-tif-signature:{1} \r\nx-tif-timestamp:{2} \r\nx-tif-nonce:{3} \r\n";

        /// <summary>
        /// 文件流输出类
        /// </summary>
        private System.IO.TextWriter output;

        /// <summary>
        /// 请求日志
        /// </summary>
        /// <param name="paasid"></param>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="request"></param>
        public void WirteRequestLog(string paasid,string signature,string timestamp, string nonce,string request)
        {
            string headerLog = string.Format(this.LogHeadFormat, paasid, signature, timestamp, nonce);
            this.WriteLog(headerLog + request);
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="logText"></param>
        public void WriteLog(string logText)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // 判断文件是否存在，不存在则创建，否则读取值显示到窗体
            string logFullName = path + "\\Gzsiinterface_" + DateTime.Now.ToString("yyyyMMdd") + ".log";
            if (!File.Exists(logFullName))
            {
                FileStream fs1 = new FileStream(logFullName, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(logText);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                System.IO.File.AppendAllText(logFullName, "【" + DateTime.Now.ToString() + "】--> \r\n" + logText + "\r\n");
            }
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="transNo"></param>
        /// <param name="transId"></param>
        /// <param name="hosCode"></param>
        /// <param name="sign"></param>
        /// <param name="requestXml"></param>
        public void WriteInPut(string transNo, string transId, string hosCode, string sign, string requestXml)
        {
            fileName = System.DateTime.Now.ToString("yyyy-MM-dd") + "log.txt";
            string absPath = path + fileName;

            string fgx = System.Environment.NewLine + "---------------------------------------------------------------------------";
            string dt = System.Environment.NewLine + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string str = System.Environment.NewLine + "交易类别代码transNo:  {0}" + System.Environment.NewLine + "交易流水号transId:  {1}" + System.Environment.NewLine + "机构编号hosCode:  {2}" + System.Environment.NewLine + "数字签名sign:  {3}";
            str = string.Format(str, transNo, transId, hosCode, sign);
            string inputPar = System.Environment.NewLine + "业务参数requestXML:" + System.Environment.NewLine + requestXml;

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

        /// <summary>
        /// 日志输出
        /// </summary>
        /// <param name="retXML"></param>
        public void WriteOutPut(string retXML)
        {
            fileName = System.DateTime.Now.ToString("yyyy-MM-dd") + "log.txt";
            string absPath = path + fileName;

            string dt = System.Environment.NewLine + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string outPar = System.Environment.NewLine + "返回结果responseXML:" + System.Environment.NewLine + retXML;

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
    }
}
