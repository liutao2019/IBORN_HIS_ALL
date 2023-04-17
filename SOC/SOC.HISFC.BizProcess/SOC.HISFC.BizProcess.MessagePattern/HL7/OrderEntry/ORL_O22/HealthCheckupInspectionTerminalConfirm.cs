using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORL_O22
{
    /// <summary>
    /// 检验体检执行与取消
    /// </summary>
    public class HealthCheckupInspectionTerminalConfirm
    {
        FS.HISFC.HealthCheckup.BizLogic.CHKFee chkMgr = new FS.HISFC.HealthCheckup.BizLogic.CHKFee();
        FS.HISFC.HealthCheckup.BizLogic.ReportRef myReport = new FS.HISFC.HealthCheckup.BizLogic.ReportRef();
        public int ProcessMessage(NHapi.Model.V24.Message.ORL_O22 orlO22, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            string clinicCode = orlO22.RESPONSE.PATIENT.GetGENERAL_ORDER(0).GetORDER(0).ORC.PlacerOrderNumber.UniversalID.Value;
            if (string.IsNullOrEmpty(clinicCode))
            {
                errInfo = "患者流水号为空！";
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            string applyNOS = "";
            string applyCancelNOS = "";

            DateTime operDate = this.chkMgr.GetDateTimeFromSysDateTime();
            for (int i = 0; i < orlO22.RESPONSE.PATIENT.GetGENERAL_ORDER().ORDERRepetitionsUsed; i++)
            {
                NHapi.Model.V24.Group.ORL_O22_ORDER orlO22Order = orlO22.RESPONSE.PATIENT.GetGENERAL_ORDER().GetORDER(i);
                if (orlO22Order.ORC.OrderControl.Value.Equals("OR"))
                {
                    if (!applyNOS.Contains(orlO22Order.ORC.PlacerOrderNumber.EntityIdentifier.Value))
                    {
                        string applyNO = orlO22Order.ORC.PlacerOrderNumber.EntityIdentifier.Value;
                        string confirmOperCode = orlO22Order.ORC.GetEnteredBy(0).IDNumber.Value;
                        string confirmDeptCode = orlO22Order.ORC.EnteringOrganization.Identifier.Value;
                        string SequenceNO = orlO22Order.ORC.PlacerOrderNumber.EntityIdentifier.Value;
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
                else if (orlO22Order.ORC.OrderControl.Value.Equals("OC"))//取消登记
                {
                    if (!applyCancelNOS.Contains(orlO22Order.ORC.PlacerOrderNumber.EntityIdentifier.Value))
                    {
                        string applyNO = orlO22Order.ORC.PlacerOrderNumber.EntityIdentifier.Value;
                        string confirmOperCode = orlO22Order.ORC.GetOrderingProvider(0).IDNumber.Value;
                        string confirmDeptCode = orlO22Order.ORC.EnteringOrganization.Identifier.Value;
                        string SequenceNO = orlO22Order.ORC.PlacerOrderNumber.EntityIdentifier.Value;

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
                    errInfo = "未知的确认类型" + orlO22Order.ORC.OrderControl.Value;
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

    }

}
