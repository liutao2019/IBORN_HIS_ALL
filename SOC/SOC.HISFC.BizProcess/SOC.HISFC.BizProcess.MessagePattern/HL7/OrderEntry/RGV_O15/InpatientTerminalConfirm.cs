using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.RGV_O15
{
    public class InpatientTerminalConfirm
    {
        /// <summary>
        /// 发送住院终端确认消息（RGV^O15）
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alDrugInfo"></param>
        /// <returns></returns>
        public int ProcessMessage(FS.HISFC.Models.RADT.PatientInfo patientInfo, ArrayList alFeeItemList, bool isPositive, ref NHapi.Model.V24.Message.RGV_O15 RGV_O15, ref string errInfo)
        {
            if (alFeeItemList == null || alFeeItemList.Count == 0)
            {
                return 1;
            }

            RGV_O15 = new NHapi.Model.V24.Message.RGV_O15();
            //MSH
            #region MSH
            RGV_O15.MSH.MessageType.MessageType.Value = "RGV";
            RGV_O15.MSH.MessageType.TriggerEvent.Value = "O15";
            FS.HL7Message.V24.Function.GenerateMSH(RGV_O15.MSH);
            #endregion
            #region PID
            RGV_O15.PATIENT.PID.PatientID.ID.Value = patientInfo.PID.CardNO;
            //住院号
            NHapi.Model.V24.Datatype.CX patientList1 = RGV_O15.PATIENT.PID.GetPatientIdentifierList(0);
            patientList1.IdentifierTypeCode.Value = "PatientNO";
            patientList1.ID.Value = patientInfo.PID.PatientNO;
            //姓名
            NHapi.Model.V24.Datatype.XPN patientName = RGV_O15.PATIENT.PID.GetPatientName(0);
            patientName.FamilyName.Surname.Value = patientInfo.Name;
            //性别
            RGV_O15.PATIENT.PID.AdministrativeSex.Value = patientInfo.Sex.ID.ToString();
            //合同单位
            RGV_O15.PATIENT.PID.ProductionClassCode.Identifier.Value = patientInfo.Pact.ID;
            RGV_O15.PATIENT.PID.ProductionClassCode.Text.Value = patientInfo.Pact.Name;
            #endregion
            #region PV1
            RGV_O15.PATIENT.PATIENT_VISIT.PV1.SetIDPV1.Value = "1";
            RGV_O15.PATIENT.PATIENT_VISIT.PV1.PatientClass.Value = "I";//类型
            RGV_O15.PATIENT.PATIENT_VISIT.PV1.ReAdmissionIndicator.Value = patientInfo.InTimes.ToString();//住院次数
            RGV_O15.PATIENT.PATIENT_VISIT.PV1.VisitNumber.ID.Value = patientInfo.ID;//住院流水号
            #endregion
            #region Order

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in alFeeItemList)
            {
                NHapi.Model.V24.Group.RGV_O15_ORDER order = RGV_O15.GetORDER(RGV_O15.ORDERRepetitionsUsed);

                #region ORC
                order.ORC.OrderControl.Value = isPositive ? "OK" : "CR";//ORC-1控制段
                order.ORC.PlacerOrderNumber.EntityIdentifier.Value = feeItemList.SequenceNO.ToString();//收费细目编号 
                order.ORC.PlacerGroupNumber.EntityIdentifier.Value = feeItemList.Order.ID;//医嘱流水号
                #endregion
                #region RXO
                order.ORDER_DETAIL.RXO.RequestedGiveCode.Text.Value = feeItemList.Item.ID;//项目编号
                order.ORDER_DETAIL.RXO.RequestedGiveAmountMinimum.Value = feeItemList.Item.Qty.ToString();//数量
                order.ORDER_DETAIL.RXO.RequestedGiveUnits.Identifier.Value = feeItemList.Item.PriceUnit;//单位
                #endregion
            }

            #endregion

            //return FS.HL7Message.ProcessFactory.ProcessSender("RGV^O15(ORC-1=" + (isPositive ? "OK" : "CR") + ")", RGV_O15.MSH, RGV_O15);
            return 1;
        }
    }
}
