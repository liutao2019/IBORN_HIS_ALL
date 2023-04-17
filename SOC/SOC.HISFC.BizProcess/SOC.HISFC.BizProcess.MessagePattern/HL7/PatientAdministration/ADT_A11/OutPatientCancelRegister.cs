using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A11
{
    /// <summary>
    /// 患者取消登记
    /// </summary>
    public class OutPatientCancelRegister
    {
        public int ProcessMessage(FS.HISFC.Models.Registration.Register register, ref NHapi.Model.V24.Message.ADT_A01 adtA01, ref string errInfo)
        {
           adtA01 = new NHapi.Model.V24.Message.ADT_A01();
            //MSH 消息头
            adtA01.MSH.MessageType.MessageType.Value = "ADT";
            adtA01.MSH.MessageType.TriggerEvent.Value = "A11";
            FS.HL7Message.V24.Function.GenerateMSH(adtA01.MSH);

            //EVN 消息事件
            adtA01.EVN.EventTypeCode.Value = "A11";
            FS.HL7Message.V24.Function.GenerateEVN(adtA01.EVN);

            //PID 患者基本信息
            adtA01.PID.SetIDPID.Value = "1";
            adtA01.PID.PatientID.ID.Value = register.PID.CardNO;

            NHapi.Model.V24.Datatype.CX patientList1 = adtA01.PID.GetPatientIdentifierList(0);
            //其他厂商确定用这个IDCard
            patientList1.IdentifierTypeCode.Value = "IDCard";//register.Card.CardType.ID;
            patientList1.ID.Value = register.PID.CardNO;

            if (!string.IsNullOrEmpty(register.Card.CardType.ID) && !register.Card.CardType.ID.Equals("IDCard"))
            {
                NHapi.Model.V24.Datatype.CX patientList2 = adtA01.PID.GetPatientIdentifierList(adtA01.PID.PatientIdentifierListRepetitionsUsed);
                patientList2.IdentifierTypeCode.Value = register.Card.CardType.ID;
                patientList2.ID.Value = register.Card.ID;
            }

            if (!string.IsNullOrEmpty(register.IDCardType.ID))
            {
                NHapi.Model.V24.Datatype.CX patientList2 = adtA01.PID.GetPatientIdentifierList(adtA01.PID.PatientIdentifierListRepetitionsUsed);
                patientList2.IdentifierTypeCode.Value = register.IDCardType.ID;
                patientList2.ID.Value = register.IDCard;
            }

            NHapi.Model.V24.Datatype.XPN patientName = adtA01.PID.GetPatientName(0);
            patientName.FamilyName.Surname.Value = register.Name;

            //PV1 患者就诊信息
            adtA01.PV1.PatientClass.Value = "O";//类型
            adtA01.PV1.ReAdmissionIndicator.Value = register.InTimes.ToString();//入院次数
            adtA01.PV1.VisitNumber.ID.Value = register.ID;

            //return ProcessFactory.ProcessSender("ADT^A11(PV1-2=O)", adtA01.MSH, adtA01);
            return 1;
        }
    }
}
