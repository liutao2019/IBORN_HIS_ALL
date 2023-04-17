using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HL7Message;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORG_O20
{
    /// <summary>
    /// 体检检查收费确认消息发送
    /// </summary>
    class HealthCheckupExaminationFeeConfirm
    {
        /// <summary>
        /// 体检检查收费确认消息发送
        /// </summary>
        /// <param name="register"></param>
        /// <param name="alObj">收费信息</param>
        /// <param name="isPositive"></param>
        /// <returns></returns>
        public int ProcessMessage(FS.HISFC.HealthCheckup.Object.ChkRegister register, ArrayList alObj, bool isPositive, ref NHapi.Model.V24.Message.ORG_O20[] ORG_O20S, ref string errInfo)
        {
            if (alObj == null || alObj.Count == 0)
            {
                return 1;
            }

            FS.HISFC.BizLogic.Fee.Item ItemMgr = new FS.HISFC.BizLogic.Fee.Item();
            Dictionary<string, NHapi.Model.V24.Message.ORG_O20> applyMessage = new Dictionary<string, NHapi.Model.V24.Message.ORG_O20>();
            Dictionary<string, NHapi.Model.V24.Group.ORG_O20_ORDER> ApplyNOs = new Dictionary<string, NHapi.Model.V24.Group.ORG_O20_ORDER>();
            NHapi.Model.V24.Message.ORG_O20 ORG_O20 = null;

            //按照申请单分开
            foreach (FS.HISFC.HealthCheckup.Object.CHKFeeItem feeItemList in alObj)
            {
                if (applyMessage.ContainsKey(feeItemList.Order.ID))
                {
                    ORG_O20 = applyMessage[feeItemList.Order.ID];
                }
                else
                {
                    ORG_O20 = new NHapi.Model.V24.Message.ORG_O20();
                    //MSH
                    ORG_O20.MSH.MessageType.MessageType.Value = "ORG";
                    ORG_O20.MSH.MessageType.TriggerEvent.Value = "O20";
                    FS.HL7Message.V24.Function.GenerateMSH(ORG_O20.MSH);
                    ORG_O20.MSH.SendingApplication.NamespaceID.Value = "PEIS";
                    //MSA
                    ORG_O20.MSA.AcknowledgementCode.Value = "AA";
                    ORG_O20.MSA.DelayedAcknowledgmentType.Value = "F";
                    //PID
                    ORG_O20.RESPONSE.PATIENT.PID.PatientID.ID.Value = register.PatientInfo.PID.CardNO;

                    NHapi.Model.V24.Datatype.CX patientList1 = ORG_O20.RESPONSE.PATIENT.PID.GetPatientIdentifierList(0);
                    patientList1.IdentifierTypeCode.Value = "IDCard";
                    patientList1.ID.Value = register.PatientInfo.Patient.ID;

                    //身份证号
                    NHapi.Model.V24.Datatype.CX patientList2 = ORG_O20.RESPONSE.PATIENT.PID.GetPatientIdentifierList(1);
                    patientList2.IdentifierTypeCode.Value = register.PatientInfo.IDCardType.ID;
                    patientList2.ID.Value = register.PatientInfo.IDCard;
                    //姓名
                    NHapi.Model.V24.Datatype.XPN patientName = ORG_O20.RESPONSE.PATIENT.PID.GetPatientName(0);
                    patientName.FamilyName.Surname.Value = register.PatientInfo.Name;
                    //性别
                    ORG_O20.RESPONSE.PATIENT.PID.AdministrativeSex.Value = register.PatientInfo.Sex.ID.ToString();
                    //合同单位
                    ORG_O20.RESPONSE.PATIENT.PID.ProductionClassCode.Identifier.Value = register.PatientInfo.Pact.ID;
                    ORG_O20.RESPONSE.PATIENT.PID.ProductionClassCode.Text.Value = register.PatientInfo.Pact.Name;

                    //ORC

                    applyMessage.Add(feeItemList.Order.ID, ORG_O20);
                }

                string itemCode = (string.IsNullOrEmpty(feeItemList.UndrugComb.Package.ID) ? feeItemList.Item.ID : feeItemList.UndrugComb.Package.ID);
                string itemName = (string.IsNullOrEmpty(feeItemList.UndrugComb.Package.Name) ? feeItemList.Item.Name : feeItemList.UndrugComb.Package.Name);
                string applyno = feeItemList.Order.ID + itemCode;
                if (ApplyNOs.ContainsKey(applyno))
                {
                    ApplyNOs[applyno].OBR.ChargeToPractice.DollarAmount.Quantity.Value = (FS.FrameWork.Function.NConvert.ToDecimal(ApplyNOs[applyno].OBR.ChargeToPractice.DollarAmount.Quantity.Value) + (feeItemList.Item.Qty * feeItemList.Item.Price)).ToString("F2");
                }
                else
                {
                    NHapi.Model.V24.Group.ORG_O20_ORDER order = ORG_O20.RESPONSE.GetORDER(ORG_O20.RESPONSE.ORDERRepetitionsUsed);
                    order.ORC.OrderControl.Value = isPositive ? "OK" : "CR";
                    order.ORC.PlacerOrderNumber.UniversalID.Value = register.ID;
                    order.ORC.FillerOrderNumber.EntityIdentifier.Value = feeItemList.RecipeNO;
                    order.ORC.PlacerOrderNumber.UniversalIDType.Value = "O";
                    order.ORC.PlacerOrderNumber.EntityIdentifier.Value = feeItemList.Order.ID;
                    order.ORC.PlacerGroupNumber.EntityIdentifier.Value = feeItemList.RecipeNO;
                    order.OBR.ChargeToPractice.DollarAmount.Quantity.Value = (feeItemList.Item.Qty * feeItemList.Item.Price).ToString("F2");
                    //order.OBR.UniversalServiceIdentifier.Identifier.Value = itemCode;
                    //order.OBR.UniversalServiceIdentifier.Text.Value = itemName;
                  // order.OBR.UniversalServiceIdentifier.NameOfCodingSystem.Value = feeItemList.Order.Sample.ID;
 
                    order.OBR.SpecimenSource.SpecimenSourceNameOrCode.Identifier.Value = SOC.HISFC.BizProcess.Cache.Fee.GetItem(feeItemList.Item.ID).CheckBody; ;//检查部位
                    order.OBR.UniversalServiceIdentifier.NameOfCodingSystem.Value = SOC.HISFC.BizProcess.Cache.Fee.GetItem(feeItemList.Item.ID).CheckApplyDept;
                    ApplyNOs[applyno] = order;
                }

            }
            ORG_O20S = applyMessage.Values.ToArray<NHapi.Model.V24.Message.ORG_O20>();
            //return FS.HL7Message.ProcessFactory.ProcessSender("ORG^O20(ORC-1=" + (isPositive ? "OK" : "CR") + ")", ORG_O20.MSH, applyMessage.Values.ToArray<NHapi.Model.V24.Message.ORG_O20>());

            return 1;
        }
    }
}
