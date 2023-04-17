using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORM_O01
{
    /// <summary>
    /// 住院手术取消申请单
    /// </summary>
    public class InpatientOperationTerminalCancelConfirm : TerminalConfirm
    {
        /// <summary>
        /// 住院检查取消确认
        /// </summary>
        /// <param name="omgO19"></param>
        /// <returns></returns>
        public  int ProcessMessage(NHapi.Model.V24.Message.ORM_O01 ormO01, ref NHapi.Base.Model.IMessage ackMessage,ref string errInfo)
        {
            //获取患者信息
            string inpatientNO = ormO01.PATIENT.PATIENT_VISIT.PV1.VisitNumber.ID.Value;
            if (string.IsNullOrEmpty(inpatientNO))
            {
                errInfo = "患者流水号为空！";
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.HISFC.Models.RADT.PatientInfo patientInfo = this.radtInpatient.QueryPatientInfoByInpatientNO(inpatientNO);
            if (patientInfo == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "获取" + inpatientNO + "患者信息失败，原因：" + this.radtInpatient.Err;
                return -1;
            }
            string applyCancelNOS = "";
            DateTime operDate = this.orderManager.GetDateTimeFromSysDateTime();
            for (int i = 0; i < ormO01.ORDERRepetitionsUsed; i++)
            {
                NHapi.Model.V24.Group.ORM_O01_ORDER omgOrder = ormO01.GetORDER(i);
                if (omgOrder.ORC.OrderControl.Value == "CA")
                {
                    //取申请单号
                    if (!applyCancelNOS.Contains(omgOrder.ORC.PlacerOrderNumber.EntityIdentifier.Value))
                    {
                        string applyNO = omgOrder.ORC.PlacerOrderNumber.EntityIdentifier.Value;
                        string confirmOperCode = omgOrder.ORC.GetOrderingProvider(0).IDNumber.Value;
                        string confirmDeptCode = omgOrder.ORC.EnteringOrganization.Identifier.Value;

                        if (this.CancelConfirmFee(patientInfo, applyNO, confirmDeptCode, confirmOperCode, operDate,ref errInfo) == -1)
                        {
                            this.feeIntegrate.Rollback();
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }

                        //此处记录已更新的单号
                        applyCancelNOS = applyCancelNOS + "|" + applyNO;
                    }
                }

            }

            this.feeIntegrate.Commit();
            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }

    }
}
