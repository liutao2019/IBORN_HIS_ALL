using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.SocialSecurity.Zdly.PublicFee
{
    /// <summary>
    /// AdjustOverTop 的摘要说明。
    /// </summary>
    public class AdjustOverTop : FS.HISFC.BizProcess.Interface.Common.IJob
    {
        public AdjustOverTop()
        {
        }

        private string myMessage = "";
        #region IJob 成员

        public string Message
        {
            get
            {
                return this.myMessage;
            }
        }

        public int Start()
        {
            // TODO:  添加 AdjustOverTop.Start 实现
            FS.HISFC.BizLogic.Fee.InPatient feeMgr = new FS.HISFC.BizLogic.Fee.InPatient();
            FS.HISFC.BizLogic.Fee.PactUnitInfo myPact = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
            FS.HISFC.BizLogic.RADT.InPatient patientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
            FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            string begin = DateTime.MinValue.ToString();
            string end = feeMgr.GetSysDateTime();
            ArrayList al =  patientMgr.PatientQuery(begin, end, "I", "03");
            //加上市公医的患者
            ArrayList alSiPatientInfo = patientMgr.PatientQuery(begin, end, "I", "02");
            
            Hashtable htPact = new Hashtable();

            if (al == null)
            {
                this.myMessage = "查询患者信息出错" + patientMgr.Err;
                return -1;
            }
            if (alSiPatientInfo != null)
            {
                foreach (FS.HISFC.Models.RADT.PatientInfo p in alSiPatientInfo)
                {
                    if (p.Pact.Name.Contains("市公医"))
                    {
                        al.Add(p);
                    }
                }
            }

            foreach (FS.HISFC.Models.RADT.PatientInfo pinfo in al)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                feeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                if (feeMgr.AdjustOverLimitFee(pinfo) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.myMessage = this.myMessage + feeMgr.Err + pinfo.ID + pinfo.Name + "/n";
                    continue;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }

            return 0;
        }

        #endregion
    }
}
