using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A12
{
    /// <summary>
    /// 患者取消分诊
    /// </summary>
    public class OutPatientCancelAssign
    {
        public int ProcessMessage(FS.HISFC.Models.Nurse.Assign assign, ref NHapi.Model.V24.Message.ADT_A09 adtA12,ref string errInfo)
        {
            adtA12 = new NHapi.Model.V24.Message.ADT_A09();
            //MSH 消息头
            adtA12.MSH.MessageType.MessageType.Value = "ADT";
            adtA12.MSH.MessageType.TriggerEvent.Value = "A12";
            FS.HL7Message.V24.Function.GenerateMSH(adtA12.MSH);

            //EVN 消息事件
            adtA12.EVN.EventTypeCode.Value = "A12";
            FS.HL7Message.V24.Function.GenerateEVN(adtA12.EVN);


            //PID 患者基本信息
            adtA12.PID.SetIDPID.Value = "1";
            adtA12.PID.PatientID.ID.Value = assign.Register.ID;

            //就诊卡
            NHapi.Model.V24.Datatype.CX patientList1 = adtA12.PID.GetPatientIdentifierList(0);
            //其他厂商确定用这个IDCard
            patientList1.IdentifierTypeCode.Value = "IDCard";//register.Card.CardType.ID;
            patientList1.ID.Value = assign.Register.PID.CardNO;

            if (!string.IsNullOrEmpty(assign.Register.Card.CardType.ID) && !assign.Register.Card.CardType.ID.Equals("IDCard"))
            {
                NHapi.Model.V24.Datatype.CX patientList2 = adtA12.PID.GetPatientIdentifierList(adtA12.PID.PatientIdentifierListRepetitionsUsed);
                patientList2.IdentifierTypeCode.Value = assign.Register.Card.CardType.ID;
                patientList2.ID.Value = assign.Register.Card.ID;
            }
            //身份证号
            if (!string.IsNullOrEmpty(assign.Register.IDCardType.ID))
            {
                NHapi.Model.V24.Datatype.CX patientList2 = adtA12.PID.GetPatientIdentifierList(adtA12.PID.PatientIdentifierListRepetitionsUsed);
                patientList2.IdentifierTypeCode.Value = assign.Register.IDCardType.ID;
                patientList2.ID.Value = assign.Register.IDCard;
            }
            NHapi.Model.V24.Datatype.XPN patientName = adtA12.PID.GetPatientName(0);
            patientName.FamilyName.Surname.Value = assign.Register.Name;

            //PV1 患者就诊信息
            adtA12.PV1.PatientClass.Value = "O";//类型
            adtA12.PV1.AssignedPatientLocation.Facility.NamespaceID.Value = assign.Queue.AssignDept.ID;
            adtA12.PV1.AssignedPatientLocation.LocationStatus.Value = assign.Queue.SRoom.ID;
            adtA12.PV1.AssignedPatientLocation.PersonLocationType.Value = assign.Queue.Console.ID;

            adtA12.PV1.AssignedPatientLocation.Floor.Value = assign.Queue.ID;
            adtA12.PV1.AssignedPatientLocation.LocationDescription.Value = assign.Queue.Order.ToString();


            NHapi.Model.V24.Datatype.XCN attendingDoctor1 = adtA12.PV1.GetAttendingDoctor(0);

            attendingDoctor1.IDNumber.Value = assign.Register.SeeDoct.ID;
            attendingDoctor1.FamilyName.Surname.Value = assign.Register.SeeDoct.Name;

            adtA12.PV1.ReAdmissionIndicator.Value = assign.Register.InTimes.ToString();//入院次数
            adtA12.PV1.VisitNumber.ID.Value = assign.Register.ID;

            //return ProcessFactory.ProcessSender("ADT^A12(PV1-2=O)", adtA12.MSH, adtA12);

            return 1;
        }
    }
}
