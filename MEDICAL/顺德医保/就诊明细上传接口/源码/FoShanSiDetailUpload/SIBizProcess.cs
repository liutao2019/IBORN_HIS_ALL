using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoShanSiDetailUpload
{
    /// <summary>
    /// 佛山市定点医疗机构就诊明细上传的代理服务器的WebService发布的方法
    /// </summary>
    public class SIBizProcess
    {
        #region 变量

        /// <summary>
        /// 服务
        /// </summary>
        private SiDetailService.FacadeServiceProxyService facadeService = new FoShanSiDetailUpload.SiDetailService.FacadeServiceProxyService();

        #endregion

        #region 方法

        /// <summary>
        /// 医院登录[100]
        /// </summary>
        /// <param name="transNO"></param>
        /// <param name="inXML"></param>
        /// <returns></returns>
        public Model.ResultHead Login(string transNO, string inXML)
        {
            string errMsg = "";
            //入参
            Model.ResultHead result = InvokeWebServiceByIn("医院登录[100]", "login", transNO, inXML, ref errMsg);


            return result;
        }

        /// <summary>
        /// 医院口令修改[101]
        /// </summary>
        /// <param name="transNO"></param>
        /// <param name="inXML"></param>
        /// <returns></returns>
        public Model.ResultHead ChangePw(string transNO, string inXML)
        {
            string errMsg = "";
            Model.ResultHead result = InvokeWebServiceByIn("医院口令修改[101]:", "login", transNO, inXML, ref errMsg);


            return result;

        }

        /// <summary>
        /// 主单信息上传[JJJG01]
        /// </summary>
        /// <param name="transNO"></param>
        /// <param name="inXML"></param>
        /// <returns></returns>
        public Model.ResultHead UploadMainInfo(string transNO, string inXML)
        {
            string errMsg = "";

            Model.ResultHead result = InvokeWebServiceByIn("主单信息上传[JJJG01]:", "process", transNO, inXML, ref errMsg);


            return result;

        }

        /// <summary>
        /// 费用明细信息上传[JJJG02]
        /// </summary>
        /// <param name="transNO"></param>
        /// <param name="inXML"></param>
        /// <returns></returns>
        public Model.ResultHead UploadFeeDetail(string transNO, string inXML)
        {
            string errMsg = "";

            Model.ResultHead result = InvokeWebServiceByIn("费用明细信息上传[JJJG02]:", "process", transNO, inXML, ref errMsg);


            return result;
        }

        /// <summary>
        /// 接口服务
        /// </summary>
        /// <param name="transNO"></param>
        /// <param name="inXML"></param>
        /// <returns></returns>
        private Model.ResultHead InvokeWebServiceByIn(string transName, string methodName, string transNO, string inXML, ref string errMsg)
        {
            //入参
            LogManager.WriteLog(transName + inXML);

            string[] args = { transNO, inXML };
            Object obj = Common.WebServiceHelper.InvokeWebService(Function.WebServiceAddress, methodName, args, ref errMsg);
            if (obj == null)
            {
                return null;
            }
            string outXML = obj.ToString();
            Model.ResultHead result = Function.GetResultHead(transNO, outXML);

            //出参
            LogManager.WriteLog(transName + outXML);

            return result;
        }
        #endregion
    }
}
