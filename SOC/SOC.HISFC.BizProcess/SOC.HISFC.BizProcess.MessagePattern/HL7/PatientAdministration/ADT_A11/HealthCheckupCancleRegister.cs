using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A11
{
      /// <summary>
    /// 体检取消登记
    /// </summary>
    public class HealthCheckupCancleRegister
    {
        public int ProcessMessage(FS.HISFC.HealthCheckup.Object.ChkRegister register, ref NHapi.Model.V24.Message.ADT_A01 adtA01, ref string errInfo)
        {
           adtA01 = new NHapi.Model.V24.Message.ADT_A01();
            //MSH 消息头
            adtA01.MSH.MessageType.MessageType.Value = "ADT";
            adtA01.MSH.MessageType.TriggerEvent.Value = "A11";

            FS.HL7Message.V24.Function.GenerateMSH(adtA01.MSH);
            adtA01.MSH.SendingApplication.NamespaceID.Value ="PEIS"; //体检系统发送

            //EVN 消息事件
            adtA01.EVN.EventTypeCode.Value = "A11";
            FS.HL7Message.V24.Function.GenerateEVN(adtA01.EVN);

            //PID 患者基本信息
            adtA01.PID.SetIDPID.Value = "1";
            adtA01.PID.PatientID.ID.Value = register.PatientInfo.PID.CardNO;

            NHapi.Model.V24.Datatype.CX patientList1 = adtA01.PID.GetPatientIdentifierList(0);
            //其他厂商确定用这个IDCard
            patientList1.IdentifierTypeCode.Value = "IDCard";//register.Card.CardType.ID;
            patientList1.ID.Value = register.PatientInfo.PID.ID;

            if (!string.IsNullOrEmpty(register.PatientInfo.Card.CardType.ID) && !register.PatientInfo.Card.CardType.ID.Equals("IDCard"))
            {
                NHapi.Model.V24.Datatype.CX patientList2 = adtA01.PID.GetPatientIdentifierList(adtA01.PID.PatientIdentifierListRepetitionsUsed);
                patientList2.IdentifierTypeCode.Value = register.PatientInfo.Card.CardType.ID;
                patientList2.ID.Value = register.PatientInfo.Card.ID;
            }

            if (!string.IsNullOrEmpty(register.IDCardType.ID))
            {
                NHapi.Model.V24.Datatype.CX patientList2 = adtA01.PID.GetPatientIdentifierList(adtA01.PID.PatientIdentifierListRepetitionsUsed);
                patientList2.IdentifierTypeCode.Value = register.PatientInfo.IDCardType.ID;
                patientList2.ID.Value = register.PatientInfo.IDCard;
            }

            NHapi.Model.V24.Datatype.XPN patientName = adtA01.PID.GetPatientName(0);
            patientName.FamilyName.Surname.Value = register.PatientInfo.Name;

            //PV1 患者就诊信息
            if (register.CHKKind == "1")
                adtA01.PV1.PatientClass.Value = "T";//类型
            else
                adtA01.PV1.PatientClass.Value = "J";//集体体检
            adtA01.PV1.ReAdmissionIndicator.Value = register.PatientInfo.InTimes.ToString();//入院次数
            adtA01.PV1.VisitNumber.ID.Value = register.ChkClinicNo;

            //return ProcessFactory.ProcessSender("ADT^A11(PV1-2=O)", adtA01.MSH, adtA01);
            return 1;
        }
    }
}
