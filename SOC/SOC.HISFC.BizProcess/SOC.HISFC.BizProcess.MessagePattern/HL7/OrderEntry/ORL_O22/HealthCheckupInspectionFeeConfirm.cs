using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORL_O22
{
    class HealthCheckupInspectionFeeConfirm
    {
        /// <summary>
        /// 体检检验收费确认消息发送
        /// </summary>
        /// <param name="register"></param>
        /// <param name="alObj">收费信息</param>
        /// <param name="isPositive"></param>
        /// <returns></returns>
        public int ProcessMessage(FS.HISFC.HealthCheckup.Object.ChkRegister register, ArrayList alObj, bool isPositive, ref NHapi.Model.V24.Message.ORL_O22 ORL_O22, ref string errInfo)
        {
            if (alObj == null || alObj.Count == 0)
            {
                return 1;
            }

            ORL_O22 = new NHapi.Model.V24.Message.ORL_O22();
            //MSH
            ORL_O22.MSH.MessageType.MessageType.Value = "ORL";
            ORL_O22.MSH.MessageType.TriggerEvent.Value = "O22";
            FS.HL7Message.V24.Function.GenerateMSH(ORL_O22.MSH);
            ORL_O22.MSH.SendingApplication.NamespaceID.Value = "PEIS";
            //MSA
            ORL_O22.MSA.AcknowledgementCode.Value = "AA";
            ORL_O22.MSA.DelayedAcknowledgmentType.Value = "F";
            //PID
            ORL_O22.RESPONSE.PATIENT.PID.PatientID.ID.Value = register.PatientInfo.PID.CardNO;

            NHapi.Model.V24.Datatype.CX patientList1 = ORL_O22.RESPONSE.PATIENT.PID.GetPatientIdentifierList(0);
            patientList1.IdentifierTypeCode.Value = "IDCard";
            patientList1.ID.Value = register.PatientInfo.PID.CardNO;

            //身份证号
            NHapi.Model.V24.Datatype.CX patientList2 = ORL_O22.RESPONSE.PATIENT.PID.GetPatientIdentifierList(1);
            patientList2.IdentifierTypeCode.Value = register.PatientInfo.IDCardType.ID;
            patientList2.ID.Value = register.PatientInfo.Patient.ID;
            //姓名
            NHapi.Model.V24.Datatype.XPN patientName = ORL_O22.RESPONSE.PATIENT.PID.GetPatientName(0);
            patientName.FamilyName.Surname.Value = register.PatientInfo.Name;
            //性别
            ORL_O22.RESPONSE.PATIENT.PID.AdministrativeSex.Value = register.PatientInfo.Sex.ID.ToString();
            //合同单位
            ORL_O22.RESPONSE.PATIENT.PID.ProductionClassCode.Identifier.Value = register.PatientInfo.Pact.ID;
            ORL_O22.RESPONSE.PATIENT.PID.ProductionClassCode.Text.Value = register.PatientInfo.Pact.Name;

            //ORC
            Dictionary<string, NHapi.Model.V24.Group.ORL_O22_ORDER> ApplyNOs = new Dictionary<string, NHapi.Model.V24.Group.ORL_O22_ORDER>();
            foreach (FS.HISFC.HealthCheckup.Object.CHKFeeItem feeItemList in alObj)
            {
                string itemCode = (string.IsNullOrEmpty(feeItemList.UndrugComb.Package.ID) ? feeItemList.Item.ID : feeItemList.UndrugComb.Package.ID);
                string itemName = (string.IsNullOrEmpty(feeItemList.UndrugComb.Package.Name) ? feeItemList.Item.Name : feeItemList.UndrugComb.Package.Name);
                string applyno = feeItemList.Order.ID + feeItemList.UndrugComb.ID + itemCode;
                if (ApplyNOs.ContainsKey(applyno))
                {
                    ApplyNOs[applyno].OBSERVATION_REQUEST.OBR.ChargeToPractice.DollarAmount.Quantity.Value = (FS.FrameWork.Function.NConvert.ToDecimal(ApplyNOs[applyno].OBSERVATION_REQUEST.OBR.ChargeToPractice.DollarAmount.Quantity.Value) + feeItemList.FT.TotCost).ToString("F2");
                }
                else
                {
                    NHapi.Model.V24.Group.ORL_O22_GENERAL_ORDER GeneralOrder = ORL_O22.RESPONSE.PATIENT.GetGENERAL_ORDER();
                    NHapi.Model.V24.Group.ORL_O22_ORDER order = GeneralOrder.GetORDER(GeneralOrder.ORDERRepetitionsUsed);
                    order.ORC.OrderControl.Value = isPositive ? "OK" : "CR";
                    order.ORC.PlacerOrderNumber.UniversalID.Value = register.ID;
                    order.ORC.PlacerOrderNumber.UniversalIDType.Value = "O";
                    order.ORC.FillerOrderNumber.EntityIdentifier.Value = feeItemList.RecipeNO;
                    order.ORC.PlacerOrderNumber.EntityIdentifier.Value = feeItemList.Order.ID;
                    order.ORC.PlacerGroupNumber.EntityIdentifier.Value = feeItemList.RecipeNO;

                    order.OBSERVATION_REQUEST.OBR.ChargeToPractice.DollarAmount.Quantity.Value = feeItemList.FT.TotCost.ToString("F2");
                    order.OBSERVATION_REQUEST.OBR.UniversalServiceIdentifier.Identifier.Value = itemCode;
                    order.OBSERVATION_REQUEST.OBR.UniversalServiceIdentifier.Text.Value = itemName;

                    ApplyNOs[applyno] = order;
                }
            }
            //return FS.HL7Message.ProcessFactory.ProcessSender("ORL^O22(ORC-1=" + (isPositive ? "OK" : "CR") + ")", ORL_O22.MSH, ORL_O22);
            return 1;
        }
    }
}
