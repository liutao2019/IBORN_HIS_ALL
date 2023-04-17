using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A25
{
    /// <summary>
    /// 取消出院登记
    /// </summary>
    public class PatientCallBack 
    {
        FS.HISFC.BizProcess.Integrate.RADT RADTIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 出院登记召回
        /// </summary>
        /// <param name="adtA02"></param>
        /// <returns></returns>
        public int ProcessMessage(NHapi.Model.V24.Message.ADT_A25 adtA25, ref NHapi.Base.Model.IMessage ackMessage,ref string errInfo)
        {
            if (string.IsNullOrEmpty(adtA25.PV1.VisitNumber.ID.Value))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "出院登记召回失败，原因：患者流水号为空";
                return -1;
            }
            //获取患者信息
            FS.HISFC.Models.RADT.PatientInfo patientInfo = RADTIntegrate.QueryPatientInfoByInpatientNO(adtA25.PV1.VisitNumber.ID.Value);
            if (patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "出院登记召回获取" + patientInfo.ID + "患者信息失败，原因：" + this.RADTIntegrate.Err;
                return -1;
            }

            FS.HISFC.Models.Base.Bed bed = new FS.HISFC.Models.Base.Bed();
            bed.ID = adtA25.PV1.AssignedPatientLocation.PointOfCare.Value + adtA25.PV1.AssignedPatientLocation.Bed.Value;	//床号
            if (string.IsNullOrEmpty(bed.ID))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "出院登记召回失败，原因：病床编码为空";
                return -1;
            }

            bed.InpatientNO = patientInfo.ID;		//床位上患者住院流水号

            //记录操作人
            FS.FrameWork.Management.Connection.Operator.ID = adtA25.EVN.GetOperatorID(0).IDNumber.Value;
            if (string.IsNullOrEmpty(FS.FrameWork.Management.Connection.Operator.ID))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "出院登记召回失败，原因：操作员编码为空";
                return -1;
            }
            FS.FrameWork.Management.Connection.Operator = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetEmployee(FS.FrameWork.Management.Connection.Operator.ID);
            if (FS.FrameWork.Management.Connection.Operator == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "出院登记召回失败，原因：传入的操作员编码，系统中找不到" + adtA25.EVN.GetOperatorID(0).IDNumber.Value;
                return -1;
            }


            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.RADTIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //召回退费
            if (feeIntegrate.QuitSupplementFee(patientInfo) == -1)
            {
                errInfo = this.feeIntegrate.Err;
                return -1;
            }

            if (RADTIntegrate.CallBack(patientInfo, bed) == -1)//调用召回业务
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "出院登记召回失败，原因：" + RADTIntegrate.Err;
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }
    }
}
