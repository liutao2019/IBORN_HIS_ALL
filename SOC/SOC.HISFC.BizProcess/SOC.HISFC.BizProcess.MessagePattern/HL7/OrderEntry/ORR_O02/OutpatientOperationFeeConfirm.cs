using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORR_O02
{
    /// <summary>
    /// 门诊手术收费确认
    /// </summary>
    public class OutpatientOperationFeeConfirm
    {
        /// <summary>
        /// 门诊手术收费确认消息发送
        /// </summary>
        /// <param name="register"></param>
        /// <param name="alObj">收费信息</param>
        /// <param name="isPositive"></param>
        /// <returns></returns>
        public int ProcessMessage(FS.HISFC.Models.Registration.Register register, ArrayList alObj, bool isPositive, ref NHapi.Model.V24.Message.ORR_O02 ORR_O02, ref string errInfo)
        {
            if (alObj == null || alObj.Count == 0)
            {
                return 1;
            }

            ORR_O02 = new NHapi.Model.V24.Message.ORR_O02();
            //MSH
            ORR_O02.MSH.MessageType.MessageType.Value = "ORR";
            ORR_O02.MSH.MessageType.TriggerEvent.Value = "O02";
            FS.HL7Message.V24.Function.GenerateMSH(ORR_O02.MSH);
            //MSA
            ORR_O02.MSA.AcknowledgementCode.Value = "AA";
            ORR_O02.MSA.DelayedAcknowledgmentType.Value = "F";
            //PID
            ORR_O02.RESPONSE.PATIENT.PID.PatientID.ID.Value = register.PID.CardNO;

            //就诊卡
            NHapi.Model.V24.Datatype.CX patientList1 = ORR_O02.RESPONSE.PATIENT.PID.GetPatientIdentifierList(0);
            patientList1.IdentifierTypeCode.Value = "IDCard";
            patientList1.ID.Value = register.Card.ID;

            //身份证号
            NHapi.Model.V24.Datatype.CX patientList2 = ORR_O02.RESPONSE.PATIENT.PID.GetPatientIdentifierList(1);
            patientList2.IdentifierTypeCode.Value = register.IDCardType.ID;
            patientList2.ID.Value = register.IDCard;
            //姓名
            NHapi.Model.V24.Datatype.XPN patientName = ORR_O02.RESPONSE.PATIENT.PID.GetPatientName(0);
            patientName.FamilyName.Surname.Value = register.Name;
            //性别
            ORR_O02.RESPONSE.PATIENT.PID.AdministrativeSex.Value = register.Sex.ID.ToString();
            //合同单位
            ORR_O02.RESPONSE.PATIENT.PID.ProductionClassCode.Identifier.Value = register.Pact.ID;
            ORR_O02.RESPONSE.PATIENT.PID.ProductionClassCode.Text.Value = register.Pact.Name;


            //ORC

            Dictionary<string, NHapi.Model.V24.Group.ORR_O02_ORDER> ApplyNOs = new Dictionary<string, NHapi.Model.V24.Group.ORR_O02_ORDER>();
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in alObj)
            {
                string itemCode = (string.IsNullOrEmpty(feeItemList.Order.Package.ID) ? feeItemList.Order.Item.ID : feeItemList.Order.Package.ID);
                string applyno = feeItemList.Order.ApplyNo + feeItemList.Order.Combo.ID + itemCode;
                if (ApplyNOs.ContainsKey(applyno))
                {
                    ApplyNOs[applyno].OBR.ChargeToPractice.DollarAmount.Quantity.Value = (FS.FrameWork.Function.NConvert.ToDecimal(ApplyNOs[applyno].OBR.ChargeToPractice.DollarAmount.Quantity.Value) + feeItemList.FT.TotCost).ToString("F2");
                }
                else
                {
                    NHapi.Model.V24.Group.ORR_O02_ORDER order = ORR_O02.RESPONSE.GetORDER(ORR_O02.RESPONSE.ORDERRepetitionsUsed);
                    order.ORC.OrderControl.Value = isPositive ? "OK" : "CR";
                    order.ORC.PlacerOrderNumber.UniversalID.Value = register.ID;
                    order.ORC.PlacerOrderNumber.UniversalIDType.Value = "O";
                    order.ORC.PlacerOrderNumber.EntityIdentifier.Value = feeItemList.Order.ApplyNo;
                    order.ORC.PlacerGroupNumber.EntityIdentifier.Value = feeItemList.Order.ID;
                    order.OBR.ChargeToPractice.DollarAmount.Quantity.Value = feeItemList.FT.TotCost.ToString("F2");
                    order.OBR.UniversalServiceIdentifier.Identifier.Value = itemCode;

                    ApplyNOs[applyno] = order;
                }
            }

            //return FS.HL7Message.ProcessFactory.ProcessSender("ORR^O02(ORC-1=" + (isPositive ? "OK" : "CR") + ")", ORR_O02.MSH, ORR_O02);
            return 1;
        }
    }
}
