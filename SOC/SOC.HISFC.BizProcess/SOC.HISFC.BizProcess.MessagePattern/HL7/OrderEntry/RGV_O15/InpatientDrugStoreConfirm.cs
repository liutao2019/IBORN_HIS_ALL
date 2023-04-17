using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.RGV_O15
{
    /// <summary>
    /// 发送发药确认消息
    /// </summary>
    public class InpatientDrugStoreConfirm
    {
        /// <summary>
        /// 发送住院发药确认消息（RGV^O15）
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alDrugInfo"></param>
        /// <returns></returns>
        public int ProcessMessage(FS.HISFC.Models.RADT.PatientInfo patientInfo, ArrayList alDrugInfo, bool isPositive, ref NHapi.Model.V24.Message.RGV_O15 RGV_O15, ref string errInfo)
        {
            if (alDrugInfo == null || alDrugInfo.Count == 0)
            {
                return 1;
            }
            ItemCodeMapManager mapMgr = new ItemCodeMapManager();
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

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alDrugInfo)
            {
                NHapi.Model.V24.Group.RGV_O15_ORDER order = RGV_O15.GetORDER(RGV_O15.ORDERRepetitionsUsed);

                #region ORC
                RGV_O15.PATIENT.PATIENT_VISIT.PV1.AssignedPatientLocation.PointOfCare.Value = applyOut.ApplyDept.ID;//病区
                order.ORC.OrderControl.Value = isPositive ? "OK" : "CR";//ORC-1控制段
                order.ORC.PlacerOrderNumber.EntityIdentifier.Value = mapMgr.GetHL7Code(EnumItemCodeMap.InpatientOrder, new FS.FrameWork.Models.NeuObject(applyOut.ExecNO.ToString(), applyOut.ExecNO.ToString(), "")).ID;  
                order.ORC.PlacerGroupNumber.EntityIdentifier.Value = applyOut.OrderNO;//医嘱流水号
                order.ORC.GetEnteredBy(0).IDNumber.Value =  applyOut.Operation.ApproveOper.ID;//摆药人
                order.ORC.DateTimeOfTransaction.TimeOfAnEvent.Value = applyOut.Operation.ApproveOper.OperTime.ToString("yyyyMMddHHmmss");
                #endregion
                #region RXO
                order.ORDER_DETAIL.RXO.RequestedGiveCode.Text.Value = applyOut.Item.ID;//项目编号
                order.ORDER_DETAIL.RXO.RequestedGiveAmountMinimum.Value = (applyOut.Operation.ApplyQty * applyOut.Days).ToString();//数量
                order.ORDER_DETAIL.RXO.RequestedGiveUnits.Identifier.Value = applyOut.Item.MinUnit;//单位
                order.ORDER_DETAIL.RXO.DeliverToLocation.Facility.UniversalID.Value = applyOut.StockDept.ID;//药房
                NHapi.Model.V24.Datatype.CE billInfo = order.ORDER_DETAIL.RXO.GetSupplementaryCode(order.ORDER_DETAIL.RXO.SupplementaryCodeRepetitionsUsed);
                if (isPositive)
                {
                    order.ORC.PlacerOrderNumber.EntityIdentifier.Value = mapMgr.GetHL7Code(EnumItemCodeMap.InpatientOrder, new FS.FrameWork.Models.NeuObject(applyOut.ExecNO.ToString(), applyOut.ExecNO.ToString(), "")).ID;
                }
                else
                {
                  FS.FrameWork.Models.NeuObject obj=  mapMgr.GetHL7Code(EnumItemCodeMap.InpatientOrderQuitFee, new FS.FrameWork.Models.NeuObject(applyOut.ExecNO.ToString(), applyOut.ExecNO.ToString(), ""));

                  order.ORC.AdvancedBeneficiaryNoticeCode.AlternateIdentifier.Value = obj.ID; //原来CIS执行单号流水
                  order.ORC.PlacerOrderNumber.EntityIdentifier.Value = obj.Name; //新CIS对执行单流水号
                }

                billInfo.Identifier.Value = applyOut.BillClassNO;
                billInfo.Text.Value = applyOut.DrugNO;
                //else
                //{
                //    billInfo.Identifier.Value = applyOut.BillClassNO;//摆药单类型
                //    billInfo.Text.Value = applyOut.BillNO;//申请单号
                //}
                #endregion
            }

            #endregion

            //return FS.HL7Message.ProcessFactory.ProcessSender("RGV^O15(ORC-1=" + (isPositive ? "OK" : "CR") + ")", RGV_O15.MSH, RGV_O15);
            return 1;
        }
    }
}
