using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORL_O22
{
    public class InpatientInspectionTerminalConfirm : TerminalConfirm
    {
        /// <summary>
        /// 住院检验确认
        /// </summary>
        /// <param name="orlO22"></param>
        /// <param name="ackMessage"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public int ProcessMessage(NHapi.Model.V24.Message.ORL_O22 orlO22, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            string inpatientNO = orlO22.RESPONSE.PATIENT.GetGENERAL_ORDER(0).GetORDER(0).ORC.PlacerOrderNumber.UniversalID.Value; 
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
            for (int i = 0; i < orlO22.RESPONSE.PATIENT.GetGENERAL_ORDER().ORDERRepetitionsUsed; i++)
            {
                NHapi.Model.V24.Group.ORL_O22_ORDER orlO22Order = orlO22.RESPONSE.PATIENT.GetGENERAL_ORDER().GetORDER(i);
                if (orlO22Order.ORC.OrderControl.Value.Equals("OR") ||orlO22Order.ORC.OrderControl.Value.Equals("OK"))
                {
                    #region 检验确认
                    if (!applyNOS.Contains(orlO22Order.ORC.PlacerOrderNumber.EntityIdentifier.Value))
                    {
                        string applyNO = orlO22Order.ORC.PlacerOrderNumber.EntityIdentifier.Value;
                        string confirmOperCode = orlO22Order.ORC.GetOrderingProvider(0).IDNumber.Value;
                        string confirmDeptCode = orlO22Order.ORC.EnteringOrganization.Identifier.Value;

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
                else if (orlO22Order.ORC.OrderControl.Value.Equals("CA") || orlO22Order.ORC.OrderControl.Value.Equals("OC"))//取消登记
                {
                    #region 检验取消确认
                    if (!applyCancelNOS.Contains(orlO22Order.ORC.PlacerOrderNumber.EntityIdentifier.Value))
                    {
                        string applyNO = orlO22Order.ORC.PlacerOrderNumber.EntityIdentifier.Value;
                        string confirmOperCode = orlO22Order.ORC.GetOrderingProvider(0).IDNumber.Value;
                        string confirmDeptCode = orlO22Order.ORC.EnteringOrganization.Identifier.Value;


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
                    errInfo= "未知的确认类型" + orlO22Order.ORC.OrderControl.Value;
                    return -1;
                }
            }
            this.feeIntegrate.Commit();
            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }
    }
}
