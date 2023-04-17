using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FoShanYDSI.Business.OutPatient
{
    public class OutPatientService
    {
        FoShanYDSI.Funtion function = new Funtion();
        FoShanYDSI.FoShanYDSIDatabase SIMgr = new FoShanYDSIDatabase();
        FoShanYDSI.Business.Common.CommonService comService = new FoShanYDSI.Business.Common.CommonService();


        #region by han-zf 2014-07-17 事务传递,若不传递，取消收费后SIMgr的操作记录不会回滚
        private System.Data.IDbTransaction trans = null;
        public System.Data.IDbTransaction Trans
        {
            set { this.trans = value; }
        }

        public void SetTrans(System.Data.IDbTransaction t)
        {
            this.trans = t;
            this.SIMgr.SetTrans(t);
        }
        #endregion

        /// <summary>
        /// 门诊资格确认
        /// 1.更新参保险种——patientInfo.SIMainInfo.SiType
        /// </summary>
        /// <param name="patientInfo">患者实体</param>
        /// <param name="status">返回状态</param>
        /// <param name="msg">返回信息</param>
        /// <returns></returns>
        public string OutPatientAccreditation(Neusoft.HISFC.Models.Registration.Register patientInfo, ref FoShanYDSI.Objects.SIPersonInfo personInfo, ref string status, ref string msg)
        {
            string ser = "医院联网结算";
            string cas = "门诊资格确认";
            string parXML = function.GetOutPatientAccreditationXML(patientInfo, personInfo);//生成XML

            string sig = function.GetSig(ser, function.DEP, cas, parXML);

            if ("-1".Equals(sig))
            {
                status = "-1";
                msg = "未能读取电子签名";
                return "";
            }

            string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果

            function.GetResultStatusAndMessage(retXML, ref status, ref msg);

            if (status == null || string.IsNullOrEmpty(status))
            {
                return msg;
            }

            #region 生成医保数据

            function.GetOutpatientAccreditationResult(retXML, patientInfo, ref personInfo);

            #endregion

            return retXML;
        }

        /// <summary>
        /// 门诊结算
        /// </summary>
        /// <param name="patientInfo">患者实体</param>
        /// <param name="alFeeDetail">费用明细</param>
        /// <param name="status">返回状态</param>
        /// <param name="msg">返回信息</param>
        /// <returns></returns>
        public string OutPatientBalance(Neusoft.HISFC.Models.Registration.Register patientInfo, ArrayList alFeeDetail, ref string status, ref string msg)
        {
            return "";
        }

        /// <summary>
        /// 门诊结算【碎石、放化疗、它院检查】
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alFeeDetail"></param>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string OutPatientBalanceSpecial(Neusoft.HISFC.Models.Registration.Register patientInfo, ArrayList alFeeDetail, ref string status, ref string msg, string bxlb)
        {
            return "";
        }

        /// <summary>
        /// 门诊结算取消
        /// </summary>
        /// <param name="patientInfo">患者实体(Name|SIMainInfo.SiType|SIMainInfo.RegNo|SIMainInfo.TotCost须非空)</param>
        /// <param name="status">返回状态</param>
        /// <param name="msg">返回信息</param>
        /// <returns></returns>
        public string OutPatientCancelBalance(Neusoft.HISFC.Models.Registration.Register patientInfo, ref string status, ref string msg)
        {
            string ser = "医院联网结算";
            string cas = "门诊结算取消";

            string parXML = string.Empty;

            string bxlb = string.Empty;
            if (this.comService.CheckSpecialOutBalance(patientInfo.Pact, ref bxlb) == 1)
            {
                //放化疗、碎石、他院检查门诊结算取消
                cas = "放化疗碎石它院检查报销取消";
                parXML = function.GetOutPatientCancelBalanceXML(patientInfo, true);
            }
            else
            {
                //普通门诊结算取消
                parXML = function.GetOutPatientCancelBalanceXML(patientInfo);
            }


            string sig = function.GetSig(ser, function.DEP, cas, parXML);

            if ("-1".Equals(sig))
            {
                status = "-1";
                msg = "未能读取电子签名";
                return "";
            }

            string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果

            function.GetResultStatusAndMessage(retXML, ref status, ref msg);

            if (status == null || string.IsNullOrEmpty(status) || !"1".Equals(status))
            {
                return msg;
            }

            if (SIMgr.UpdateSIMainInfo(patientInfo, false) < 0)
            {
                status = "-1";
                msg = "本地结算数据取消结算失败";
                return "";
            }

            return retXML;
        }

        /// <summary>
        /// 门诊上传发票号
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public string OutPatientUploadInvoiceNo(Neusoft.HISFC.Models.Registration.Register patientInfo, ref string status, ref string msg)
        {
            string ser = "医院联网结算";
            string cas = "门诊上传发票号";

            string parXML = string.Empty;

            //直到我膝盖中了一剑——————合同单位改你妹啊，2014-10-10数据需要平
            //if (patientInfo.Pact.ID.Equals("19") || patientInfo.Pact.ID.Equals("20") || patientInfo.Pact.ID.Equals("21"))
            //{
            //    cas = "门诊放化疗碎石它院检查报销上传发票号";
            //    parXML = function.GetOutPatientUploadInvoiceXML(patientInfo, true);
            //}
            //else
            //{
            //    parXML = function.GetOutPatientUploadInvoiceXML(patientInfo);
            //}


            string bxlb = string.Empty;
            if (this.comService.CheckSpecialOutBalance(patientInfo.Pact, ref bxlb) == 1)
            {
                cas = "门诊放化疗碎石它院检查报销上传发票号";
                parXML = function.GetOutPatientUploadInvoiceXML(patientInfo, true);
            }
            else
            {
                parXML = function.GetOutPatientUploadInvoiceXML(patientInfo);
            }



            string sig = function.GetSig(ser, function.DEP, cas, parXML);

            if ("-1".Equals(sig))
            {
                status = "-1";
                msg = "未能读取电子签名";
                return "";
            }

            string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果

            function.GetResultStatusAndMessage(retXML, ref status, ref msg);

            if (status == null || string.IsNullOrEmpty(status) || !"1".Equals(status))
            {
                return msg;
            }


            if (SIMgr.UpdateSIMainInfoInvoiceNo(patientInfo) < 0)
            {
                status = "-1";
                msg = "本地结算数据更新发票号失败";
                return "";
            }

            return retXML;
        }

        /// <summary>
        /// 获取门诊结算基础信息
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int GetOutPatientBalanceBaseInfo(Neusoft.HISFC.Models.Registration.Register patientInfo)
        {
            return SIMgr.QueryOutBalanceBaseInfo(patientInfo);
        }

        /// <summary>
        /// 保存门诊未自动上传成功的发票
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int InsertNeedUpLoadInvoiceNo(Neusoft.HISFC.Models.Registration.Register patientInfo)
        {
            return this.SIMgr.InsertNeedUpLoadInvoiceNo(patientInfo);
        }
    }
}
