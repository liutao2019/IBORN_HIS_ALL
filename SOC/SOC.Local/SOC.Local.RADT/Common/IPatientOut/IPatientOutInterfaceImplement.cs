using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.RADT.Common.IPatientOut
{
    /// <summary>
    /// 出院登记接口实现
    /// </summary>
    class IPatientOutInterfaceImplement : FS.HISFC.BizProcess.Interface.RADT.IPatientOut
    {
        /// <summary>
        /// 电子病历住院业务处理
        /// </summary>
        FS.SOC.HISFC.BizLogic.EmrNew.EMRInpatinetLogic emrLogicManager = new FS.SOC.HISFC.BizLogic.EmrNew.EMRInpatinetLogic();

        #region IPatientOut 成员

        /// <summary>
        /// 错误信息
        /// </summary>
        string errInfo = "";

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrInfo
        {
            get
            {
                return errInfo;
            }
            set
            {
                errInfo = value;
            }
        }

        /// <summary>
        /// 出院登记前调用
        /// </summary>
        /// <param name="patientInfo">住院患者实体</param>
        /// <param name="oper">当前登陆操作员</param>
        /// <returns></returns>
        public int BeforePatientOut(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject oper)
        {
            return 1;
        }

        /// <summary>
        /// 出院登记前调用
        /// </summary>
        /// <param name="patientInfo">住院患者实体</param>
        /// <param name="oper">当前登陆操作员</param>
        /// <returns></returns>
        public int OnPatientOut(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject oper)
        {
            long emrPatID = -1;

            int iEmrReturn = emrLogicManager.GetPatientId(patientInfo.ID, ref emrPatID);

            if (iEmrReturn < 0)
            {
                errInfo = "调用EMR接口GetPatientId失败！" + emrLogicManager.Err;
                return -1;
            }

            iEmrReturn = emrLogicManager.OutPTDoc(emrPatID);
            if (iEmrReturn < 0)
            {
                errInfo = "调用EMR接口OutPTDoc失败！" + emrLogicManager.Err;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 出院登记前调用
        /// </summary>
        /// <param name="patientInfo">住院患者实体</param>
        /// <param name="oper">当前登陆操作员</param>
        /// <returns></returns>
        public int AfterPatientOut(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject oper)
        {
            return 1;
        }

        #endregion
    }
}
