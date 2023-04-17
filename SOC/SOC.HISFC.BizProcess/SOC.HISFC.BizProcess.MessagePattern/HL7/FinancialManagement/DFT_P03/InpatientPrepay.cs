using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.FinancialManagement.DFT_P03
{
    /// <summary>
    /// 预交押金
    /// </summary>
    public class InpatientPrepay
    {
        /// <summary>
        /// 住院费用业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient feeInpatient = new FS.HISFC.BizLogic.Fee.InPatient();
        /// <summary>
        /// 发送预交押金消息
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alobj"></param>
        /// <param name="DFTP03"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public int ProcessMessage(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Prepay prepay, ref NHapi.Model.V24.Message.DFT_P03 DFTP03, ref string errInfo)
        {

            DFTP03 = new NHapi.Model.V24.Message.DFT_P03();
            //MSH 消息头
            DFTP03.MSH.MessageType.MessageType.Value = "DFT";
            DFTP03.MSH.MessageType.TriggerEvent.Value = "P03";
            FS.HL7Message.V24.Function.GenerateMSH(DFTP03.MSH);

            //EVN 消息事件
            DFTP03.EVN.EventTypeCode.Value = "P03";
            FS.HL7Message.V24.Function.GenerateEVN(DFTP03.EVN);

            //PID 患者基本信息
            DFTP03.PID.SetIDPID.Value = "1";
            DFTP03.PID.PatientID.ID.Value = patientInfo.PID.CardNO;

            NHapi.Model.V24.Datatype.CX patientList1 = DFTP03.PID.GetPatientIdentifierList(DFTP03.PID.PatientIdentifierListRepetitionsUsed);

            //其他厂商确定用这个IDCard
            patientList1.IdentifierTypeCode.Value = "IDCard";//register.Card.CardType.ID;
            patientList1.ID.Value = patientInfo.PID.CardNO;

            if (!string.IsNullOrEmpty(patientInfo.Card.CardType.ID) && !patientInfo.Card.CardType.ID.Equals("IDCard"))
            {
                NHapi.Model.V24.Datatype.CX patientList2 = DFTP03.PID.GetPatientIdentifierList(DFTP03.PID.PatientIdentifierListRepetitionsUsed);
                patientList2.IdentifierTypeCode.Value = patientInfo.Card.CardType.ID;
                patientList2.ID.Value = patientInfo.Card.ID;
            }

            if (!string.IsNullOrEmpty(patientInfo.IDCardType.ID))
            {
                NHapi.Model.V24.Datatype.CX patientList2 = DFTP03.PID.GetPatientIdentifierList(DFTP03.PID.PatientIdentifierListRepetitionsUsed);
                patientList2.IdentifierTypeCode.Value = patientInfo.IDCardType.ID;
                patientList2.ID.Value = patientInfo.IDCard;
            }
            //住院号
            NHapi.Model.V24.Datatype.CX patientList3 = DFTP03.PID.GetPatientIdentifierList(DFTP03.PID.PatientIdentifierListRepetitionsUsed);
            patientList3.IdentifierTypeCode.Value = "PatientNO";
            patientList3.ID.Value = patientInfo.PID.PatientNO;

            NHapi.Model.V24.Datatype.XPN patientName = DFTP03.PID.GetPatientName(0);
            patientName.FamilyName.Surname.Value = patientInfo.Name;
            if (patientInfo.Birthday > DateTime.MinValue)
            {
                DFTP03.PID.DateTimeOfBirth.TimeOfAnEvent.Value = patientInfo.Birthday.ToString("yyyyMMddHHmmss");
            }

            //PV1 患者就诊信息
            DFTP03.PV1.SetIDPV1.Value = "1";
            DFTP03.PV1.PatientClass.Value = "I";//类型
            //科室编码
            DFTP03.PV1.AssignedPatientLocation.Facility.NamespaceID.Value = patientInfo.PVisit.PatientLocation.Dept.ID;
            //床位编码
            DFTP03.PV1.AssignedPatientLocation.Bed.Value = patientInfo.PVisit.PatientLocation.Bed.ID.Length > 4 ? patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) : "";
            //病区编码
            DFTP03.PV1.AssignedPatientLocation.PointOfCare.Value = patientInfo.PVisit.PatientLocation.NurseCell.ID;
            //结算类别
            DFTP03.PV1.PatientType.Value = patientInfo.Pact.PayKind.ID;
            //住院流水号
            DFTP03.PV1.VisitNumber.ID.Value = patientInfo.ID;

            //入院次数
            DFTP03.PV1.ReAdmissionIndicator.Value = patientInfo.InTimes.ToString();


      
            if (prepay.ID == null|| prepay.ID =="")
            {
                prepay.ID = feeInpatient.QueryPrepayhappenNO(patientInfo.ID); //获取发生序号
            }
          //FT1
            DFTP03.GetFINANCIAL().FT1.SetIDFT1.Value = prepay.ID;
            DFTP03.GetFINANCIAL().FT1.TransactionID.Value = prepay.RecipeNO;
            DFTP03.GetFINANCIAL().FT1.TransactionDate.TimeOfAnEvent.Value = prepay.PrepayOper.OperTime.ToString("yyyyMMddHHmmss");
            DFTP03.GetFINANCIAL().FT1.TransactionCode.Identifier.Value = prepay.PayType.ID;
            DFTP03.GetFINANCIAL().FT1.TransactionCode.Text.Value = prepay.PayType.Name;
            DFTP03.GetFINANCIAL().FT1.TransactionAmountExtended.ToValue.Value = prepay.FT.PrepayCost.ToString();
            DFTP03.GetFINANCIAL().FT1.PatientType.Value = "I";
            DFTP03.GetFINANCIAL().FT1.ProcedureCode.Identifier.Value = prepay.PrepayState;//预交金状态0:收取；1:作废;2:补打,3结算召回作废
            DFTP03.GetFINANCIAL().FT1.GetProcedureCodeModifier(0).Identifier.Value = prepay.PrepayState;
         
        
           

            return 1;
          
        
        
        }
    }
}
