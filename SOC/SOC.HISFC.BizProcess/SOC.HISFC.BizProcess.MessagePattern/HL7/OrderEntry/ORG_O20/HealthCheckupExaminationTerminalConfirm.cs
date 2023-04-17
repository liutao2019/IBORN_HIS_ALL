using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORG_O20
{
    /// <summary>
    /// 检查执行确认
    /// </summary>
    class HealthCheckupExaminationTerminalConfirm
    {
         FS.HISFC.HealthCheckup.BizLogic.CHKFee chkMgr = new FS.HISFC.HealthCheckup.BizLogic.CHKFee();
        FS.HISFC.HealthCheckup.BizLogic.ReportRef myReport = new FS.HISFC.HealthCheckup.BizLogic.ReportRef();
        public int ProcessMessage(NHapi.Model.V24.Message.ORG_O20 orgO20, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            string clinicCode = orgO20.RESPONSE.GetORDER().ORC.PlacerOrderNumber.UniversalID.Value;
            if (string.IsNullOrEmpty(clinicCode))
            {
                errInfo = "患者流水号为空！";
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            string applyNOS = "";
            string applyCancelNOS = "";

            DateTime operDate = this.chkMgr.GetDateTimeFromSysDateTime();
            for (int i = 0; i < orgO20.RESPONSE.ORDERRepetitionsUsed; i++)
            {
                NHapi.Model.V24.Group.ORG_O20_ORDER orgO20Order = orgO20.RESPONSE.GetORDER(i);
                if (orgO20Order.ORC.OrderControl.Value.Equals("OR"))
                {
                    if (!applyNOS.Contains(orgO20Order.ORC.PlacerOrderNumber.EntityIdentifier.Value))
                    {
                        string applyNO = orgO20Order.ORC.PlacerOrderNumber.EntityIdentifier.Value;
                        string confirmOperCode = orgO20Order.ORC.GetEnteredBy(0).IDNumber.Value;
                        string confirmDeptCode = orgO20Order.ORC.EnteringOrganization.Identifier.Value;
                        string SequenceNO = orgO20Order.ORC.PlacerOrderNumber.EntityIdentifier.Value;
                        //根据申请单号和类型更新确认人、确认数量等

                        //if (myReport.UpdateFeeItemConfirmFlag(SequenceNO,"1","0") == -1)
                        //{
                        //    FS.FrameWork.Management.PublicTrans.RollBack();
                        //    errInfo = "更新费用标志失败"+ this.myReport.Err;
                        //    return -1;
                        //}

                        if (chkMgr.ConformExeItem(SequenceNO, "1") == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errInfo = "更新费用标志失败" + this.myReport.Err;
                            return -1;
                        
                        }
                        //根据申请单号和类型更新确认人、确认数量等
                        applyNOS += "|" + applyNO;
                    }
                }
                else if (orgO20Order.ORC.OrderControl.Value.Equals("OC"))//取消登记
                {
                    if (!applyCancelNOS.Contains(orgO20Order.ORC.PlacerOrderNumber.EntityIdentifier.Value))
                    {
                        string applyNO = orgO20Order.ORC.PlacerOrderNumber.EntityIdentifier.Value;
                        string confirmOperCode = orgO20Order.ORC.GetOrderingProvider(0).IDNumber.Value;
                        string confirmDeptCode = orgO20Order.ORC.EnteringOrganization.Identifier.Value;
                        string SequenceNO = orgO20Order.ORC.PlacerOrderNumber.EntityIdentifier.Value;

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
                    errInfo = "未知的确认类型" + orgO20Order.ORC.OrderControl.Value;
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

    
    }
}
