using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A13
{
    /// <summary>
    /// 患者取消结算
    /// </summary>
    public class PatientCancelBalance
    {
        /// <summary>
        /// 发送取消结算信息
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int ProcessMessage(FS.HISFC.Models.RADT.PatientInfo patientInfo,ref NHapi.Model.V24.Message.ADT_A01 adtA13,ref string errInfo)
        {
            adtA13 = new NHapi.Model.V24.Message.ADT_A01();
            //MSH 消息头
            adtA13.MSH.MessageType.MessageType.Value = "ADT";
            adtA13.MSH.MessageType.TriggerEvent.Value = "A13";
            FS.HL7Message.V24.Function.GenerateMSH(adtA13.MSH);

            //EVN 消息事件
            adtA13.EVN.EventTypeCode.Value = "A13";
            FS.HL7Message.V24.Function.GenerateEVN(adtA13.EVN);

            //PID 患者基本信息
            adtA13.PID.SetIDPID.Value = "1";
            adtA13.PID.PatientID.ID.Value = patientInfo.PID.CardNO;

            NHapi.Model.V24.Datatype.CX patientList1 = adtA13.PID.GetPatientIdentifierList(adtA13.PID.PatientIdentifierListRepetitionsUsed);

            //其他厂商确定用这个IDCard
            patientList1.IdentifierTypeCode.Value = "IDCard";//register.Card.CardType.ID;
            patientList1.ID.Value = patientInfo.PID.CardNO;

            if (!string.IsNullOrEmpty(patientInfo.Card.CardType.ID) && !patientInfo.Card.CardType.ID.Equals("IDCard"))
            {
                NHapi.Model.V24.Datatype.CX patientList2 = adtA13.PID.GetPatientIdentifierList(adtA13.PID.PatientIdentifierListRepetitionsUsed);
                patientList2.IdentifierTypeCode.Value = patientInfo.Card.CardType.ID;
                patientList2.ID.Value = patientInfo.Card.ID;
            }

            if (!string.IsNullOrEmpty(patientInfo.IDCardType.ID))
            {
                NHapi.Model.V24.Datatype.CX patientList2 = adtA13.PID.GetPatientIdentifierList(adtA13.PID.PatientIdentifierListRepetitionsUsed);
                patientList2.IdentifierTypeCode.Value = patientInfo.IDCardType.ID;
                patientList2.ID.Value = patientInfo.IDCard;
            }

            //住院号
            NHapi.Model.V24.Datatype.CX patientList3 = adtA13.PID.GetPatientIdentifierList(adtA13.PID.PatientIdentifierListRepetitionsUsed);
            patientList3.IdentifierTypeCode.Value = "PatientNO";
            patientList3.ID.Value = patientInfo.PID.PatientNO;

            NHapi.Model.V24.Datatype.XPN patientName = adtA13.PID.GetPatientName(0);
            patientName.FamilyName.Surname.Value = patientInfo.Name;

            adtA13.PID.ProductionClassCode.Identifier.Value = patientInfo.Pact.ID;
            adtA13.PID.ProductionClassCode.Text.Value = patientInfo.Pact.Name;

            //PV1 患者就诊信息
            adtA13.PV1.SetIDPV1.Value = "1";
            adtA13.PV1.PatientClass.Value = "I";//类型

            //入院次数
            adtA13.PV1.ReAdmissionIndicator.Value = patientInfo.InTimes.ToString();
            adtA13.PV1.VisitNumber.ID.Value = patientInfo.ID;


            //return ProcessFactory.ProcessSender("ADT^A13", adtA13.MSH, adtA13);
            return 1;
        }
    }
}
