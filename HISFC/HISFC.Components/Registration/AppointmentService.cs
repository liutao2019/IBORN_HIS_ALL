using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;

namespace FS.HISFC.Components.Registration
{
    public sealed class AppointmentService
    {
        public AppointmentService()
        {
            //默认预约时间段
            FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

            this.Url = ctlMgr.QueryControlerInfo("WSJURL");
            if (Url == null || Url == "-1" || Url == "") Url = "http://192.168.18.63/AppointmentService.asmx";
            
        }
        #region Request模板字符串
        private readonly string templet = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + @"
                                           <request>
                                               <method>{0}</method>
                                               {1}
                                           </request>";

        private readonly string payOrderByHisReq = @"<req>
                                                     <orderId>{0}</orderId>
                                                     <payAmout>{1}</payAmout>
                                                     <payTime>{2}</payTime>
                                                     <txnCode>{3}</txnCode>
                                                 </req>";

        private readonly string cancelOrderbyHisReq = @"<req>
                                                           <orderId>{0}</orderId>
                                                           <cancelReason>{1}</cancelReason>
                                                           <cancelTime>{2}</cancelTime>
                                                           <txnCode>{3}</txnCode>
                                                        </req>";

        private readonly string stopRegReq = @"<req>
                                                  <hospitalId>1047</hospitalId>
                                                  <deptId>{0}</deptId>
                                                  <doctorId>{1}</doctorId>
                                                  <beginDate>{2}</beginDate>
                                                  <endDate>{3}</endDate>
                                                  <timeFlag>{4}</timeFlag>
                                                  <reason>{5}</reason>
                                               </req>";
        private readonly string refundPayReq = @"<req>
                                                     <hospitalId>1047</hospitalId>
                                                     <orderId>{0}</orderId>
                                                     <refundType>{1}</refundType>
                                                     <refundTime>{2}</refundTime>
                                                     <refundReason>退费</refundReason>
                                                     <returnFee>{3}</returnFee>
                                                     <txnCode>refundPay</txnCode>
                                                </req>";
        #endregion

        #region 属性,用于反射获取SQL

        public string payOrderByHis { get { return payOrderByHisReq; } }

        public string cancelOrderbyHis { get { return cancelOrderbyHisReq; } }

        public string stopReg { get { return stopRegReq; } }

        public string refundPay { get { return refundPayReq; } }

        public string Url { get; set; }

        private string IP = string.Empty;
        #endregion

        /// <summary>
        /// 函数
        /// </summary>
        public enum funs
        {
            /// <summary>
            /// 停诊通知接口
            /// </summary>
            stopReg,
            /// <summary>
            /// 退费预检查接口
            /// </summary>
            refundPayCheck,
            /// <summary>
            /// 退费接口
            /// </summary>
            refundPay,
            /// <summary>
            /// 取号接口
            /// </summary>
            printRegInfo,
            /// <summary>
            /// 窗口支付（取号）接口
            /// </summary>
            payOrderByHis,
            /// <summary>
            /// 取消预约接口
            /// </summary>
            cancelOrderbyHis
        }
        /// <summary>
        /// 测试网络
        /// </summary>
        public string NetTest()
        {
            try
            {
                if (string.IsNullOrEmpty(IP))
                    IP = Url.Substring("http://".Length, Url.Substring("http://".Length).IndexOf("/"));

                System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
                System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
                options.DontFragment = true;
                byte[] buffer = Encoding.ASCII.GetBytes("aa");
                System.Net.NetworkInformation.PingReply reply = p.Send(IP, 1000, buffer, options);
                return reply.Status.ToString();
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }

        }

        Classes.AppointmentService service = null;

        /// <summary>
        /// 调用Web函数
        /// </summary>
        /// <param name="Method">方法名</param>
        /// <param name="func">异步回调方法,为空时不回调</param>
        /// <param name="args">参数列表</param>
        public void Invoke(funs Method, InvokeCompletedEventHandler func, params string[] args)
        {
            string pingResp = NetTest();
            if (pingResp == "Success")
            {
                this.InvokeCompleted = func;
                BackgroundWorker WebInvokeWorker = new BackgroundWorker();
                WebInvokeWorker.DoWork += new DoWorkEventHandler(Async_Work);
                WebInvokeWorker.RunWorkerAsync(ParseXmlRequest(Method, args));
            }
            else
            {
                InvokeResult result = new InvokeResult();
                result.ResultCode = "1";
                result.ResultDesc = "无法连接到服务器" + IP + ",请检查网络";
                if (InvokeCompleted != null)
                {
                    InvokeCompleted(result);
                }
            }
        }
        /// <summary>
        /// 调用Web函数
        /// </summary>
        /// <param name="funs">方法名</param>
        /// <param name="args">参数列表</param>
        public InvokeResult Invoke_Sync(funs Method, params string[] args)
        {
            InvokeResult result = null;
            string pingResp = NetTest();
            if (pingResp == "Success")
            {
                service = new Classes.AppointmentService(Url);
                string Resp = service.WebInvoke(ParseXmlRequest(Method, args));
                result = ParseXmlResponse(Resp);
            }
            else
            {
                result = new InvokeResult();
                result.ResultCode = "1";
                result.ResultDesc = "无法连接到服务器" + IP + ",请检查网络";
            }

            return result;
        }
        /// <summary>
        /// 多线程异步调用
        /// </summary>
        private void Async_Work(object sender, DoWorkEventArgs e)
        {
            service = new Classes.AppointmentService(Url);
            service.WebInvokeCompleted += new FS.HISFC.Components.Registration.Classes.WebInvokeCompletedEventHandler(WebInvokeCompleted_Event);
            service.WebInvokeAsync(e.Argument.ToString());
        }

        /// <summary>
        /// 通过反射获取RequestXML
        /// </summary>
        private string ParseXmlRequest(funs Method, params string[] args)
        {
            try
            {
                string Req = string.Format(this.GetType().GetProperty(Method.ToString()).GetValue(this,null).ToString(), args);

                return string.Format(templet, Method.ToString(), Req);
            }
            catch (System.Exception ex)
            {
                return string.Format(templet, Method.ToString(), ex.Message);
            }

        }
        /// <summary>
        /// 转换Response到InvokeResult
        /// </summary>
        public InvokeResult ParseXmlResponse(string xmlResponse)
        {
            InvokeResult result = new InvokeResult();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlResponse);
                XmlNode resultCodeNode = xmlDoc.SelectSingleNode("res/resultCode");
                result.ResultCode = resultCodeNode.InnerText;
                XmlNode resultDescNode = xmlDoc.SelectSingleNode("res/resultDesc");
                result.ResultDesc = resultDescNode.InnerText;
            }
            catch (Exception ex)
            {
                result.ResultCode = "1";
                result.ResultDesc = "转换失败,原因: " + ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 事件完成
        /// </summary>
        private void WebInvokeCompleted_Event(object sender, Classes.WebInvokeCompletedEventArgs e)
        {
            InvokeResult result = null;
            try
            {
                if (e.Error != null)
                {
                    result = new InvokeResult();
                    result.ResultCode = "1";
                    result.ResultDesc = e.Error.Message;
                }
                else
                    result = ParseXmlResponse(e.Result);

            }
            catch (System.Exception ex)
            {
                result = new InvokeResult();
                result.ResultCode = "1";
                result.ResultDesc = ex.Message;
            }
            if (InvokeCompleted != null)
            {
                InvokeCompleted(result);
            }
        }
        /// <summary>
        /// 用于传递调用结果
        /// </summary>
        public class InvokeResult
        {
            /// <summary>
            /// 处理结果代码
            /// </summary>
            public string ResultCode { get; set; }
            /// <summary>
            /// 处理结果描述
            /// </summary>
            public string ResultDesc { get; set; }
        }

        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="result"></param>
        public delegate void InvokeCompletedEventHandler(InvokeResult result);
        /// <summary>
        /// 事件
        /// </summary>
        public event InvokeCompletedEventHandler InvokeCompleted;

    }
}
