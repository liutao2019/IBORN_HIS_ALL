using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Security.Cryptography;

namespace API.GZSI.Class
{
    public class HttpHelper
    {
        /// <summary>
        /// 日志类
        /// </summary>
        private static Log log = new Log();

        /// <summary>
        /// 本地管理类
        /// </summary>
        private static LocalManager localManager = new LocalManager();

        /// <summary>
        /// Json格式
        /// </summary>
        private static string JSONCONTENTTYPE = "application/json";

        /// <summary>
        /// 当前错误
        /// </summary>
        public static string Err = string.Empty;

        /// <summary>
        /// Sha256加密
        /// </summary>
        /// <param name="myString"></param>
        /// <returns></returns>
        public static string GetSha256(string strData)
        {
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(strData);
            try
            {
                SHA256 sha256 = new SHA256CryptoServiceProvider();
                byte[] retVal = sha256.ComputeHash(bytValue);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetSHA256HashFromString() fail,error:" + ex.Message);
            }

        }

        /// <summary>
        /// 调用服务
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Infno"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static int RequestData(string request, string Infno, ref string response)
        {
            return postData(request, Infno, ref response);
        }


        /// <summary>
        /// 调用服务
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Infno"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static int RequestDataByte(string request, string Infno, ref byte[] response)
        {
            return postData(request, Infno, ref response);
        }

        /// <summary>
        /// 调用服务
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Infno"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        private static int postData(string request,string Infno, ref string response)
        {
            DateTime sNow = FS.FrameWork.Function.NConvert.ToDateTime(localManager.GetSysDateTime());
            string paasid = Models.UserInfo.Instance.app_code;
            string secreKey = Models.UserInfo.Instance.secret_key;
            string timestamp = ((sNow.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString();
            string nonce = Guid.NewGuid().ToString("N");
            string signature = GetSha256(timestamp + secreKey + nonce + timestamp);//签名

            WebClient wc = new WebClient();
            response = string.Empty;
            string ServiceUrl = Models.UserInfo.Instance.url + Infno;
            try
            {
                //request = request.Replace("hcv_ab", "hcv-ab");
                //request = request.Replace("hiv_ab", "hiv-ab");

                //请求日志
                log.WirteRequestLog( paasid, signature, timestamp, nonce, request);
                wc.Headers[HttpRequestHeader.ContentType] = JSONCONTENTTYPE;
                wc.Headers.Add("x-tif-paasid", paasid);
                wc.Headers.Add("x-tif-signature", signature);
                wc.Headers.Add("x-tif-timestamp", timestamp.ToString());
                wc.Headers.Add("x-tif-nonce", nonce);
                byte[] postBytes = Encoding.UTF8.GetBytes(request);
                byte[] returnBytes = wc.UploadData(ServiceUrl, "POST", postBytes);
                response = Encoding.UTF8.GetString(returnBytes);
                //返回日志
                log.WriteLog(response);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message);
                Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 调用服务
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Infno"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        private static int postData(string request, string Infno, ref byte[] response)
        {
            DateTime sNow = FS.FrameWork.Function.NConvert.ToDateTime(localManager.GetSysDateTime());
            string paasid = Models.UserInfo.Instance.app_code;
            string secreKey = Models.UserInfo.Instance.secret_key;
            string timestamp = ((sNow.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString();
            string nonce = Guid.NewGuid().ToString("N");
            string signature = GetSha256(timestamp + secreKey + nonce + timestamp);//签名

            WebClient wc = new WebClient();
            //response = string.Empty;
            string ServiceUrl = Models.UserInfo.Instance.url + Infno;
            try
            {
                //request = request.Replace("hcv_ab", "hcv-ab");
                //request = request.Replace("hiv_ab", "hiv-ab");

                //请求日志
                log.WirteRequestLog(paasid, signature, timestamp, nonce, request);
                wc.Headers[HttpRequestHeader.ContentType] = JSONCONTENTTYPE;
                wc.Headers.Add("x-tif-paasid", paasid);
                wc.Headers.Add("x-tif-signature", signature);
                wc.Headers.Add("x-tif-timestamp", timestamp.ToString());
                wc.Headers.Add("x-tif-nonce", nonce);
                byte[] postBytes = Encoding.UTF8.GetBytes(request);
                response = wc.UploadData(ServiceUrl, "POST", postBytes);
                //返回日志
                log.WriteLog(Encoding.UTF8.GetString(response));
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message);
                Err = ex.Message;
                return -1;
            }

            return 1;
        }
    }
}
