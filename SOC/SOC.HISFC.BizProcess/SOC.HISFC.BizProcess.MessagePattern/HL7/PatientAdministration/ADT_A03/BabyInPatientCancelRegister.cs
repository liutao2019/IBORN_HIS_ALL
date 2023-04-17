using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A03
{
    /// <summary>
    /// 取消婴儿登记
    /// </summary>
    public class BabyInPatientCancelRegister
    {
        FS.HISFC.BizProcess.Integrate.RADT RADTIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();

        public int ProcessMessage(NHapi.Model.V24.Message.ADT_A03 adtA03, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            //接收婴儿登记信息
            //母亲住院流水号
            string babyID = adtA03.PV1.VisitNumber.ID.Value;
            //找到对应的患者信息
            if (string.IsNullOrEmpty(babyID))
            {
                errInfo = "取消婴儿登记失败，原因：婴儿流水号为空";
                return -1;
            }
            //先要判断是否是婴儿先
           string inpatientNO=  radtManager.QueryBabyMotherInpatientNO(babyID);
           if (string.IsNullOrEmpty(inpatientNO)||inpatientNO.Equals("-1"))
           {
               errInfo = "取消婴儿登记失败，原因：该患者不是婴儿！";
               return -1;
           }

            FS.HISFC.Models.RADT.PatientInfo babyInfo = RADTIntegrate.QueryPatientInfoByInpatientNO(babyID);
            if (babyInfo == null || string.IsNullOrEmpty(babyInfo.ID))
            {
                errInfo = "取消婴儿登记获取" + babyID + "患者信息失败，原因：" + this.RADTIntegrate.Err;
                return -1;
            }

            //记录操作人
            FS.FrameWork.Management.Connection.Operator.ID = adtA03.EVN.GetOperatorID(0).IDNumber.Value;
            if (string.IsNullOrEmpty(FS.FrameWork.Management.Connection.Operator.ID))
            {
                errInfo = "取消婴儿登记失败，原因：操作员编码为空";
                return -1;
            }
            FS.FrameWork.Management.Connection.Operator = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetEmployee(FS.FrameWork.Management.Connection.Operator.ID);
            if (FS.FrameWork.Management.Connection.Operator == null)
            {
                errInfo = "取消婴儿登记失败，原因：传入的操作员编码，系统中找不到" + adtA03.EVN.GetOperatorID(0).IDNumber.Value;
                return -1;
            }

            if ((babyInfo.FT.TotCost + babyInfo.FT.BalancedCost) > 0)
            {
                errInfo = "取消婴儿登记失败，原因：该婴儿已经发生费用，不能取消！";
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.radtManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (radtManager.DiscardBabyRegister(babyInfo.ID) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "取消婴儿登记失败，原因：" + this.radtManager.Err;
                return -1;
            }

            FS.HISFC.Models.RADT.InStateEnumService status = new FS.HISFC.Models.RADT.InStateEnumService();
            status.ID = "C";

            if (radtManager.UpdatePatientStatus(babyInfo, status) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "取消婴儿登记失败，原因：" + this.radtManager.Err;
                return -1;
            }

            if (this.radtManager.SetShiftData(babyInfo.ID, FS.HISFC.Models.Base.EnumShiftType.OF, "取消婴儿登记",
              babyInfo.PVisit.InState, status, true) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "取消婴儿登记失败[登记]，原因：" + this.radtManager.Err;
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

    }
}
