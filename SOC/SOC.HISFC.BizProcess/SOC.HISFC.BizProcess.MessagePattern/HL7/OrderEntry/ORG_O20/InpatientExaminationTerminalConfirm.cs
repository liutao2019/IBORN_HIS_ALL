using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HL7Message;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORG_O20
{
    public class InpatientExaminationTerminalConfirm : TerminalConfirm
    {
        /// <summary>
        /// 住院检查确认
        /// </summary>
        /// <param name="orgO20"></param>
        /// <param name="ackMessage"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public int ProcessMessage(NHapi.Model.V24.Message.ORG_O20 orgO20, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            string inpatientNO = orgO20.RESPONSE.GetORDER(0).ORC.PlacerOrderNumber.UniversalID.Value;
            if (string.IsNullOrEmpty(inpatientNO))
            {
                errInfo = "患者流水号为空！";
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
            //申请单信息
            string applyNOS = "";

            DateTime operDate = this.orderManager.GetDateTimeFromSysDateTime();
            for (int i = 0; i < orgO20.RESPONSE.ORDERRepetitionsUsed; i++)
            {
                NHapi.Model.V24.Group.ORG_O20_ORDER orgO20Order = orgO20.RESPONSE.GetORDER(i);
                if (orgO20Order.ORC.OrderControl.Value.Equals("OR"))//检查登记
                {
                    if (!applyNOS.Contains(orgO20Order.ORC.PlacerOrderNumber.EntityIdentifier.Value))
                    {
                        string applyNO = orgO20Order.ORC.PlacerOrderNumber.EntityIdentifier.Value;
                        string confirmOperCode = orgO20Order.ORC.GetOrderingProvider(0).IDNumber.Value;
                        string confirmDeptCode = orgO20Order.ORC.EnteringOrganization.Identifier.Value;
                        //根据申请单号查找对应执行档

                        //确认费用
                        if (this.ComfirmFee(patientInfo, applyNO, confirmDeptCode, confirmOperCode, operDate,ref errInfo) == -1)
                        {
                            this.feeIntegrate.Rollback();
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }

                        //此处记录已更新的单号
                        applyNOS = applyNOS + "|" + applyNO;

                    }
                }
                else
                {
                    this.feeIntegrate.Rollback();
                    errInfo = "未知的确认类型" + orgO20Order.ORC.OrderControl.Value;
                    return -1;
                }
            }
            this.feeIntegrate.Commit();
            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }
    }
}
