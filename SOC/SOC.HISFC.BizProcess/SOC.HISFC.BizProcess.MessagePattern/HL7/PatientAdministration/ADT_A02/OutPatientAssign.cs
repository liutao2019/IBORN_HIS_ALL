﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A02
{
    /// <summary>
    /// 患者分诊
    /// </summary>
   public  class OutPatientAssign
    {
       public int ProcessMessage(FS.SOC.HISFC.Assign.Models.Assign assign, ref NHapi.Model.V24.Message.ADT_A02 adtA02,ref string errInfo)
       {
           adtA02 = new NHapi.Model.V24.Message.ADT_A02();
           //MSH 消息头
           adtA02.MSH.MessageType.MessageType.Value = "ADT";
           adtA02.MSH.MessageType.TriggerEvent.Value = "A02";
           FS.HL7Message.V24.Function.GenerateMSH(adtA02.MSH);

           //EVN 消息事件
           adtA02.EVN.EventTypeCode.Value = "A02";
           FS.HL7Message.V24.Function.GenerateEVN(adtA02.EVN);


           //PID 患者基本信息
           adtA02.PID.SetIDPID.Value = "1";
           adtA02.PID.PatientID.ID.Value = assign.Register.PID.CardNO;

           //就诊卡
           NHapi.Model.V24.Datatype.CX patientList1 = adtA02.PID.GetPatientIdentifierList(0);
           //其他厂商确定用这个IDCard
           patientList1.IdentifierTypeCode.Value = "IDCard";//register.Card.CardType.ID;
           patientList1.ID.Value = assign.Register.PID.CardNO;

           if (!string.IsNullOrEmpty(assign.Register.Card.CardType.ID) && !assign.Register.Card.CardType.ID.Equals("IDCard"))
           {
               NHapi.Model.V24.Datatype.CX patientList2 = adtA02.PID.GetPatientIdentifierList(adtA02.PID.PatientIdentifierListRepetitionsUsed);
               patientList2.IdentifierTypeCode.Value = assign.Register.Card.CardType.ID;
               patientList2.ID.Value = assign.Register.Card.ID;
           }

           //身份证号
           if (!string.IsNullOrEmpty(assign.Register.IDCardType.ID))
           {
               NHapi.Model.V24.Datatype.CX patientList2 = adtA02.PID.GetPatientIdentifierList(adtA02.PID.PatientIdentifierListRepetitionsUsed);
               patientList2.IdentifierTypeCode.Value = assign.Register.IDCardType.ID;
               patientList2.ID.Value = assign.Register.IDCard;
           }
           NHapi.Model.V24.Datatype.XPN patientName = adtA02.PID.GetPatientName(0);
           patientName.FamilyName.Surname.Value = assign.Register.Name;

           //PV1 患者就诊信息
           adtA02.PV1.PatientClass.Value = "O";//类型
           adtA02.PV1.AssignedPatientLocation.Facility.NamespaceID.Value = assign.Queue.AssignDept.ID;
           adtA02.PV1.AssignedPatientLocation.LocationStatus.Value = assign.Queue.SRoom.ID;
           adtA02.PV1.AssignedPatientLocation.PersonLocationType.Value = assign.Queue.Console.ID;

           adtA02.PV1.AssignedPatientLocation.Floor.Value = assign.Queue.ID;
           adtA02.PV1.AssignedPatientLocation.LocationDescription.Value = assign.Queue.Order.ToString();

           NHapi.Model.V24.Datatype.XCN attendingDoctor1 = adtA02.PV1.GetAttendingDoctor(0);

           attendingDoctor1.IDNumber.Value = assign.Queue.Doctor.ID;
           attendingDoctor1.FamilyName.Surname.Value = assign.Queue.Doctor.Name;

           adtA02.PV1.ReAdmissionIndicator.Value = assign.Register.InTimes.ToString();//入院次数
           adtA02.PV1.VisitNumber.ID.Value = assign.Register.ID;
           adtA02.PV1.AssignedPatientLocation.Floor.Value = assign.Queue.ID.ToString(); //队列号
           adtA02.PV1.AssignedPatientLocation.LocationDescription.Value = assign.SeeNO.ToString(); //看诊序号

           //return ProcessFactory.ProcessSender("ADT^A02(PV1-2=O)", adtA02.MSH, adtA02);
           return 1;
        }
    }
}
