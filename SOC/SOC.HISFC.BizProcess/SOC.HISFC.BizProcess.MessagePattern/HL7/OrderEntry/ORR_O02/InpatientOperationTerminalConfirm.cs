using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORR_O02
{
    /// <summary>
    /// 住院手术确认
    /// </summary>
    public class InpatientOperationTerminalConfirm : TerminalConfirm
    {
        public int ProcessMessage(NHapi.Model.V24.Message.ORR_O02 orrO02, ref NHapi.Base.Model.IMessage ackMessage,ref string errInfo)
        {
            string inpatientNO = orrO02.RESPONSE.GetORDER(0).ORC.PlacerOrderNumber.UniversalID.Value;
            if (string.IsNullOrEmpty(inpatientNO))
            {
                errInfo= "患者流水号为空！";
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //获取患者信息
            FS.HISFC.Models.RADT.PatientInfo patientInfo = this.radtInpatient.QueryPatientInfoByInpatientNO(inpatientNO);
            if (patientInfo == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "获取" + inpatientNO + "患者信息失败，原因：" + this.radtInpatient.Err;
                return -1;
            }

            string applyNOS = "";
            string applyCancelNOS = "";

            DateTime operDate = this.orderManager.GetDateTimeFromSysDateTime();
            for (int i = 0; i < orrO02.RESPONSE.ORDERRepetitionsUsed; i++)
            {
                NHapi.Model.V24.Group.ORR_O02_ORDER orrO02Order = orrO02.RESPONSE.GetORDER(i);
                if (orrO02Order.ORC.OrderControl.Value.Equals("OR"))
                {
                    #region 手术确认
                    if (!applyNOS.Contains(orrO02Order.ORC.PlacerOrderNumber.EntityIdentifier.Value))
                    {
                        string applyNO = orrO02Order.ORC.PlacerOrderNumber.EntityIdentifier.Value;
                        string confirmOperCode = orrO02Order.ORC.GetOrderingProvider(0).IDNumber.Value;
                        string confirmDeptCode = orrO02Order.ORC.EnteringOrganization.Identifier.Value;

                        //确认费用
                        if (this.ComfirmFee(patientInfo, applyNO, confirmDeptCode, confirmOperCode, operDate,ref errInfo) == -1)
                        {
                            this.feeIntegrate.Rollback();
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }

                        //根据申请单号和类型更新确认人、确认数量等
                        applyNOS += "|" + applyNO;
                    }

                    #endregion
                }
                else if (orrO02Order.ORC.OrderControl.Value.Equals("CA"))//取消登记
                {
                    #region 手术取消确认
                    if (!applyCancelNOS.Contains(orrO02Order.ORC.PlacerOrderNumber.EntityIdentifier.Value))
                    {
                        string applyNO = orrO02Order.ORC.PlacerOrderNumber.EntityIdentifier.Value;
                        string confirmOperCode = orrO02Order.ORC.GetOrderingProvider(0).IDNumber.Value;
                        string confirmDeptCode = orrO02Order.ORC.EnteringOrganization.Identifier.Value;


                        if (this.CancelConfirmFee(patientInfo, applyNO, confirmDeptCode, confirmOperCode, operDate,ref errInfo) == -1)
                        {
                            this.feeIntegrate.Rollback();
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }


                        //此处记录已更新的单号
                        applyCancelNOS = applyCancelNOS + "|" + applyNO;
                    }
                    #endregion
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo= "未知的确认类型" + orrO02Order.ORC.OrderControl.Value;
                    return -1;
                }
            }
            this.feeIntegrate.Commit();
            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }
    }
}
