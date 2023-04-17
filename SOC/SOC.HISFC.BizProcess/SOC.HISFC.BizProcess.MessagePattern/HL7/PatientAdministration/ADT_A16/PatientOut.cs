using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A16
{
    /// <summary>
    /// 出院登记
    /// </summary>
    public class PatientOut
    {
        FS.HISFC.BizProcess.Integrate.RADT RADTIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 出院登记
        /// </summary>
        /// <param name="adtA02"></param>
        /// <returns></returns>
        public  int ProcessMessage(NHapi.Model.V24.Message.ADT_A16 adtA16, ref NHapi.Base.Model.IMessage ackMessage,ref string errInfo)
        {
            if (string.IsNullOrEmpty(adtA16.PV1.VisitNumber.ID.Value))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "出院登记失败，原因：患者流水号为空";
                return -1;
            }
            //获取患者信息
            FS.HISFC.Models.RADT.PatientInfo patientInfo = RADTIntegrate.QueryPatientInfoByInpatientNO(adtA16.PV1.VisitNumber.ID.Value);
            if (patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "出院登记获取" + adtA16.PV1.VisitNumber.ID.Value + "患者信息失败，原因：" + this.RADTIntegrate.Err;
                return -1;
            }

            //出院时间
            if (adtA16.PV1.DischargeDateTimeRepetitionsUsed <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "出院登记失败，原因：出院日期没有值-PV1-45";
                return -1;
            }

            string timeOut=adtA16.PV1.GetDischargeDateTime(0).TimeOfAnEvent.Value;
            patientInfo.PVisit.PreOutTime = DateTime.ParseExact(timeOut.Trim('\r'), "yyyyMMddHHmmss", null);
            //转归信息
            patientInfo.PVisit.ZG.ID = "";

            //记录操作人
            FS.FrameWork.Management.Connection.Operator.ID = adtA16.EVN.GetOperatorID(0).IDNumber.Value;
            if (string.IsNullOrEmpty(FS.FrameWork.Management.Connection.Operator.ID))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "出院登记失败，原因：操作员编码为空";
                return -1;
            }
            FS.FrameWork.Management.Connection.Operator = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetEmployee(FS.FrameWork.Management.Connection.Operator.ID);
            if (FS.FrameWork.Management.Connection.Operator == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "出院登记失败，原因：传入的操作员编码，系统中找不到" + adtA16.EVN.GetOperatorID(0).IDNumber.Value;
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            RADTIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            ///增加固定费用的收费
            if (feeIntegrate.SupplementBedFee(patientInfo) == -1)
            {
                errInfo = this.feeIntegrate.Err;
                return -1;
            }

            int i = RADTIntegrate.OutPatient(patientInfo);
            errInfo = RADTIntegrate.Err;
            if (i == -1)　//失败
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "出院登记失败，原因：" + RADTIntegrate.Err;
                return -1;
            }
            else if (i == 0)//取消
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "出院登记失败，原因：" + RADTIntegrate.Err;
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }
    }
}
