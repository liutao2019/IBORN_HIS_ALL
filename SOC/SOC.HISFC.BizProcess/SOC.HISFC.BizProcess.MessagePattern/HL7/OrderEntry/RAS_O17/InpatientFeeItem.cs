using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.RAS_O17
{
    public class InpatientFeeItem
    {
        /// <summary>
        /// 处理费用
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alFeeOrder"></param>
        /// <param name="alExecOrder"></param>
        /// <returns></returns>
        public int ProcessFeeOrder(FS.HISFC.Models.RADT.PatientInfo patientInfo, ArrayList alFeeItemList, bool isPositive, ref NHapi.Model.V24.Message.RAS_O17 rasO17, ref string errInfo)
        {
            rasO17 = new NHapi.Model.V24.Message.RAS_O17();

            #region MSH

            //MSH 消息头
            rasO17.MSH.MessageType.MessageType.Value = "RAS";
            rasO17.MSH.MessageType.TriggerEvent.Value = "O17";
            FS.HL7Message.V24.Function.GenerateMSH(rasO17.MSH);

            #endregion

            #region PID

            rasO17.PATIENT.PID.SetIDPID.Value = "1";
            rasO17.PATIENT.PID.PatientID.ID.Value = patientInfo.PID.CardNO;

            NHapi.Model.V24.Datatype.CX patientList3 = rasO17.PATIENT.PID.GetPatientIdentifierList(rasO17.PATIENT.PID.PatientIdentifierListRepetitionsUsed);
            patientList3.IdentifierTypeCode.Value = "PatientNO";
            patientList3.ID.Value = patientInfo.PID.PatientNO;

            #endregion

            #region PV1

            //SetIDPV1
            rasO17.PATIENT.PATIENT_VISIT.PV1.SetIDPV1.Value = "1";
            //PatientClass
            rasO17.PATIENT.PATIENT_VISIT.PV1.PatientClass.Value = "I";//类型 
            //ReAdmissionIndicator
            rasO17.PATIENT.PATIENT_VISIT.PV1.ReAdmissionIndicator.Value = patientInfo.InTimes.ToString();
            //PatientType
            rasO17.PATIENT.PATIENT_VISIT.PV1.PatientType.Value = patientInfo.Pact.PayKind.ID;
            //VisitNumber
            rasO17.PATIENT.PATIENT_VISIT.PV1.VisitNumber.ID.Value = patientInfo.ID;

            #endregion

            #region ORC
            
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in alFeeItemList)
            {
                NHapi.Model.V24.Group.RAS_O17_ORDER order = rasO17.GetORDER(rasO17.ORDERRepetitionsUsed);

                #region ORC

                //OrderControl
                order.ORC.OrderControl.Value = isPositive ? "OK" : "CR";//ORC-1控制段
                //PlacerOrderNumber
                order.ORC.PlacerOrderNumber.EntityIdentifier.Value = feeItemList.RecipeNO + feeItemList.SequenceNO + (int)feeItemList.TransType;
                //PlacerGroupNumber
                order.ORC.PlacerGroupNumber.EntityIdentifier.Value = feeItemList.Order.ID;//医嘱流水号
                //EnteredBy
                order.ORC.GetEnteredBy(0).IDNumber.Value = feeItemList.FeeOper.ID;//摆药人
                //DateTimeOfTransaction
                order.ORC.DateTimeOfTransaction.TimeOfAnEvent.Value =feeItemList.FeeOper.OperTime.ToString("yyyyMMddHHmmss");
                //EnteringOrganization
                order.ORC.EnteringOrganization.Identifier.Value = feeItemList.FeeOper.Dept.ID;
                //OrderingFacilityName
                order.ORC.GetOrderingFacilityName(0).OrganizationName.Value = patientInfo.PVisit.PatientLocation.Dept.ID;//患者所在科室
                #endregion

                #region RXO

                //RequestedGiveCode 

                //项目编号
                order.ORDER_DETAIL.RXO.RequestedGiveCode.Text.Value = feeItemList.Item.ID;
                //项目类型
                order.ORDER_DETAIL.RXO.RequestedDispenseCode.NameOfAlternateCodingSystem.Value = feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug ? "n" : "d";

                //RequestedGiveAmountMinimum
                order.ORDER_DETAIL.RXO.RequestedGiveAmountMinimum.Value = feeItemList.Item.Qty.ToString("F2");
                //RequestedGiveUnits
                order.ORDER_DETAIL.RXO.RequestedGiveUnits.Identifier.Value = feeItemList.Item.PriceUnit;

                #endregion

                #region RXA

                NHapi.Model.V24.Segment.RXA rxa = order.GetRXA(order.RXARepetitionsUsed);
                //ActionCodeRXA
                rxa.ActionCodeRXA.Value =  "A" ;
                //SystemEntryDateTime
                rxa.SystemEntryDateTime.TimeOfAnEvent.Value = feeItemList.ChargeOper.OperTime.ToString("yyyyMMddHHmmss");
                //AdministeredAtLocation
                rxa.AdministeredAtLocation.PointOfCare.Value = feeItemList.ExecOper.Dept.ID;

                //CompletionStatus
                rxa.CompletionStatus.Value = "2";//收费状态
                //DateTimeStartOfAdministration
                rxa.DateTimeStartOfAdministration.TimeOfAnEvent.Value = feeItemList.ChargeOper.OperTime.ToString("yyyyMMddHHmmss");
                //DateTimeEndOfAdministration
                rxa.DateTimeEndOfAdministration.TimeOfAnEvent.Value = feeItemList.ChargeOper.OperTime.ToString("yyyyMMddHHmmss");
                #endregion
            }
            #endregion


            return 1;
        }
    }
}
