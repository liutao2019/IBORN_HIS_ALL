using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.OMG_O19
{
    /// <summary>
    /// 检查项目取消
    /// </summary>
    public  class HealthCheckupExaminationTerminalCancelConfirm
    {
        FS.HISFC.HealthCheckup.BizLogic.CHKFee chkMgr = new FS.HISFC.HealthCheckup.BizLogic.CHKFee();
        FS.HISFC.HealthCheckup.BizLogic.ReportRef myReport = new FS.HISFC.HealthCheckup.BizLogic.ReportRef();
        public int ProcessMessage(NHapi.Model.V24.Message.OMG_O19 omg019, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            string clinicCode = omg019.PATIENT.PATIENT_VISIT.PV1.VisitNumber.ID.Value;
            if (string.IsNullOrEmpty(clinicCode))
            {
                errInfo = "患者流水号为空！";
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            string applyNOS = "";
            string applyCancelNOS = "";

            DateTime operDate = this.chkMgr.GetDateTimeFromSysDateTime();
            for (int i = 0; i < omg019.ORDERRepetitionsUsed; i++)
            {
                NHapi.Model.V24.Group.OMG_O19_ORDER omg019Order = omg019.GetORDER(i);

                if (omg019Order.ORC.OrderControl.Value.Equals("OC"))//取消登记
                {
                    if (!applyCancelNOS.Contains(omg019Order.ORC.PlacerOrderNumber.EntityIdentifier.Value))
                    {
                        string applyNO = omg019Order.ORC.PlacerOrderNumber.EntityIdentifier.Value;
                        string confirmOperCode = omg019Order.ORC.GetOrderingProvider(0).IDNumber.Value;
                        string confirmDeptCode = omg019Order.ORC.EnteringOrganization.Identifier.Value;
                        string SequenceNO = omg019Order.ORC.PlacerOrderNumber.EntityIdentifier.Value;

                        //根据申请单号和类型更新确认人、确认数量等

                        //if (myReport.UpdateFeeItemConfirmFlag(SequenceNO, "0", "1") == -1)
                        //{
                        //    FS.FrameWork.Management.PublicTrans.RollBack();
                        //    errInfo = "更新费用标志失败" + this.myReport.Err;
                        //    return -1;
                        //}

                        if (chkMgr.ConformExeItem(SequenceNO, "0") == -1) //取消审核
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errInfo = "更新费用标志失败" + this.myReport.Err;
                            return -1;

                        }


                        //此处记录已更新的单号
                        applyCancelNOS = applyCancelNOS + "|" + applyNO;
                    }
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "未知的确认类型" + omg019Order.ORC.OrderControl.Value;
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

    }
}
