using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Xml;


namespace FoShanYDSI.Business.InPatient
{
    public class YdInPatientService
    {
        FoShanYDSI.Funtion function = new Funtion();
        FoShanYDSI.FoShanYDSIDatabase SIMgr = new FoShanYDSIDatabase();
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
        /// 住院资格确认  
        /// </summary>
        /// <param name="patientInfo">患者实体</param>
        /// <param name="status">返回状态</param>
        /// <param name="msg">返回信息</param>
        /// <returns>返回交易retXML或者错误状态"3"</returns>
        public string InPatientAccreditation(Neusoft.HISFC.Models.RADT.PatientInfo patient, ref FoShanYDSI.Objects.SIPersonInfo personInfo, ref string status, ref string msg)
        {
            status = "1";

            string ser = "异地联网结算";
            string cas = "身份识别";
            string parXML = function.GetYdInPatientAccreditationXML(patient, personInfo);//生成XML

            string sig = function.GetSig(ser, function.DEP, cas, parXML);

            if ("-1".Equals(sig))
            {
                status = "3";
                msg = "未能读取电子签名";
                return "3";
            }

            string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果

            function.GetResultStatusAndMessage(retXML, ref status, ref msg);

            if (status == null || string.IsNullOrEmpty(status) || status.Equals("3"))
            {
                return "3";
            }

            #region 生成医保数据

            function.GetYdInPatientAccreditationResult(retXML, patient, ref personInfo,ref status,ref msg);

            #endregion

            return retXML;
        }
    }
}
