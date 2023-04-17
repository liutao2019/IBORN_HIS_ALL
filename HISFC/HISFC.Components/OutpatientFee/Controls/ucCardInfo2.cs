using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace FS.HISFC.Components.OutpatientFee.Controls
{
    public partial class ucCardInfo2 : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCardInfo2()
        {
            InitializeComponent();//{6ABA909B-8693-46d5-B636-8C30797BAE8E}
          
        }


        public int SetInfo(FS.HISFC.Models.Ticker.CardTicketNew card)
        {
            label1.Text = "标题：" + card.Title;
            label2.Text = "姓名：" + card.PatientName;
            label3.Text = "有效开始时间：" + card.GmtStart;
            label4.Text = "有效结束时间：" + card.GmtExpiry;
            label5.Text = "可用区域：" + card.LimitArea;
            label6.Text = "卡券类型：" + card.CouponType;
            label7.Text = "卡券业务：" + card.CouponBusiness;
            label8.Text = "卡券标签：" + card.CouponLable;
            label9.Text = "使用限制："; neuTextBox2.Text = card.UseLimits; //+card.UseLimits;
            label10.Text = "简介广告："; neuTextBox1.Text= card.Advert;
            label11.Text = "核销人名称：" + card.WriteOffMsgName;
            label12.Text = "核销人手机：" + card.WriteOffMsgPhone;
            label13.Text = "核销时间：" + card.GmtWriteOff;
            label14.Text = "手机：" + card.Phone;

            
            return 1;
           
        }


        private static readonly string DefaultUserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36";

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受   
        }

         public static HttpWebResponse PostHttps(string url, IDictionary<string, string> parameters, Encoding charset)
        {
            HttpWebRequest request = null;
            CookieContainer cookie = new CookieContainer();
            //HTTPSQ请求
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            request = WebRequest.Create(url) as HttpWebRequest;
            request.CookieContainer = cookie;
            request.ProtocolVersion = HttpVersion.Version11;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = DefaultUserAgent;
            request.KeepAlive = true;
            request.Headers["Accept-Language"] = "zh-CN,zh;q=0.9";
            //request.Headers["Cookie"] = "username=aaaaaa; Language=zh_CN";
            //如果需要POST数据   
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                       // buffer.AppendFormat("&{0}={1}", key, WebUtility.UrlEncode(parameters[key]));
                        
                    }
                    else
                    {
                       // buffer.AppendFormat("{0}={1}", key, WebUtility.UrlEncode(parameters[key]));
                    }
                    i++;
                }
                byte[] data = charset.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }

            return (HttpWebResponse)request.GetResponse();
        }

         private void label5_Click(object sender, EventArgs e)
         {

         }
    
    }
}
