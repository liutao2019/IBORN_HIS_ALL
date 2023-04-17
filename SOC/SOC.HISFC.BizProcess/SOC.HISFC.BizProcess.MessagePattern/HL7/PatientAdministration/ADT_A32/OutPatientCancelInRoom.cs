using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A32
{
    /// <summary>
    /// 患者取消进诊
    /// </summary>
    public class OutPatientCancelInRoom
    {
        public int ProcessMessage(FS.HISFC.Models.Nurse.Assign assign, ref NHapi.Model.V24.Message.ADT_A32 adtA32,ref string errInfo)
        {
            adtA32 = new NHapi.Model.V24.Message.ADT_A32();
            //MSH 消息头
            adtA32.MSH.MessageType.MessageType.Value = "ADT";
            adtA32.MSH.MessageType.TriggerEvent.Value = "A32";
            FS.HL7Message.V24.Function.GenerateMSH(adtA32.MSH);

            //EVN 消息事件
            adtA32.EVN.EventTypeCode.Value = "A32";
            FS.HL7Message.V24.Function.GenerateEVN(adtA32.EVN);


            //PID 患者基本信息
            adtA32.PID.SetIDPID.Value = "1";
            adtA32.PID.PatientID.ID.Value = assign.Register.ID;

            //就诊卡
            NHapi.Model.V24.Datatype.CX patientList1 = adtA32.PID.GetPatientIdentifierList(0);
            //其他厂商确定用这个IDCard
            patientList1.IdentifierTypeCode.Value = "IDCard";//register.Card.CardType.ID;
            patientList1.ID.Value = assign.Register.PID.CardNO;

            if (!string.IsNullOrEmpty(assign.Register.Card.CardType.ID) && !assign.Register.Card.CardType.ID.Equals("IDCard"))
            {
                NHapi.Model.V24.Datatype.CX patientList2 = adtA32.PID.GetPatientIdentifierList(adtA32.PID.PatientIdentifierListRepetitionsUsed);
                patientList2.IdentifierTypeCode.Value = assign.Register.Card.CardType.ID;
                patientList2.ID.Value = assign.Register.Card.ID;
            }
            //身份证号
            if (!string.IsNullOrEmpty(assign.Register.IDCardType.ID))
            {
                NHapi.Model.V24.Datatype.CX patientList2 = adtA32.PID.GetPatientIdentifierList(adtA32.PID.PatientIdentifierListRepetitionsUsed);
                patientList2.IdentifierTypeCode.Value = assign.Register.IDCardType.ID;
                patientList2.ID.Value = assign.Register.IDCard;
            }
            NHapi.Model.V24.Datatype.XPN patientName = adtA32.PID.GetPatientName(0);
            patientName.FamilyName.Surname.Value = assign.Register.Name;

            //PV1 患者就诊信息
            adtA32.PV1.PatientClass.Value = "O";//类型

            adtA32.PV1.ReAdmissionIndicator.Value = assign.Register.InTimes.ToString();//入院次数
            adtA32.PV1.VisitNumber.ID.Value = assign.Register.ID;

            //return ProcessFactory.ProcessSender("ADT^A32(PV1-2=O)", adtA32.MSH, adtA32);
            return 1;
        }
    }
}
