using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A04
{
    class HealthCheckupRegister
    {
        /// <summary>
        /// 发送体检患者登记信息
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int ProcessMessage(FS.HISFC.HealthCheckup.Object.ChkRegister register, ref NHapi.Model.V24.Message.ADT_A01 adtA01, ref string errInfo)
        {
            adtA01 = new NHapi.Model.V24.Message.ADT_A01();
            //MSH 消息头
            adtA01.MSH.MessageType.MessageType.Value = "ADT";
            adtA01.MSH.MessageType.TriggerEvent.Value = "A04";
            FS.HL7Message.V24.Function.GenerateMSH(adtA01.MSH);
            adtA01.MSH.SendingApplication.NamespaceID.Value = "PEIS";
   
            //EVN 消息事件
            adtA01.EVN.EventTypeCode.Value = "A04";
            FS.HL7Message.V24.Function.GenerateEVN(adtA01.EVN);

            //PID 患者基本信息
            adtA01.PID.SetIDPID.Value = "1";
            adtA01.PID.PatientID.ID.Value = register.PatientInfo.PID.CardNO;

            NHapi.Model.V24.Datatype.CX patientList1 = adtA01.PID.GetPatientIdentifierList(0);

            //其他厂商确定用这个IDCard
            patientList1.IdentifierTypeCode.Value = "IDCard";//register.Card.CardType.ID;
            patientList1.ID.Value = register.PatientInfo.PID.ID;

            if (!string.IsNullOrEmpty(register.Card.CardType.ID) && !register.Card.CardType.ID.Equals("IDCard"))
            {
                NHapi.Model.V24.Datatype.CX patientList2 = adtA01.PID.GetPatientIdentifierList(adtA01.PID.PatientIdentifierListRepetitionsUsed);
                patientList2.IdentifierTypeCode.Value = register.Card.CardType.ID;
                patientList2.ID.Value = register.Card.ID;
            }

            if (!string.IsNullOrEmpty(register.IDCardType.ID))
            {
                NHapi.Model.V24.Datatype.CX patientList3 = adtA01.PID.GetPatientIdentifierList(adtA01.PID.PatientIdentifierListRepetitionsUsed);
                patientList3.IdentifierTypeCode.Value = register.IDCardType.ID;
                patientList3.ID.Value = register.IDCard;
            }

            NHapi.Model.V24.Datatype.XPN patientName = adtA01.PID.GetPatientName(0);
            patientName.FamilyName.Surname.Value = register.PatientInfo.Name;
            if (register.PatientInfo.Birthday > DateTime.MinValue)
            {
                adtA01.PID.DateTimeOfBirth.TimeOfAnEvent.Value = register.PatientInfo.Birthday.ToString("yyyyMMddHHmmss");
            }
            adtA01.PID.AdministrativeSex.Value = register.PatientInfo.Sex.ID.ToString();
            NHapi.Model.V24.Datatype.XAD homeAddress = adtA01.PID.GetPatientAddress(0);
            homeAddress.AddressType.Value = "H";
            homeAddress.StreetAddress.StreetOrMailingAddress.Value = register.PatientInfo.AddressHome;
            homeAddress.ZipOrPostalCode.Value = register.PatientInfo.HomeZip;

            NHapi.Model.V24.Datatype.XAD businessAddress = adtA01.PID.GetPatientAddress(1);
            businessAddress.AddressType.Value = "O";
            businessAddress.StreetAddress.StreetOrMailingAddress.Value = register.PatientInfo.AddressBusiness;
            businessAddress.ZipOrPostalCode.Value = register.PatientInfo.BusinessZip;

            NHapi.Model.V24.Datatype.XTN homePhone = adtA01.PID.GetPhoneNumberHome(0);
            homePhone.AnyText.Value = register.PatientInfo.PhoneHome;
            NHapi.Model.V24.Datatype.XTN businessPhone = adtA01.PID.GetPhoneNumberBusiness(0);
            businessPhone.AnyText.Value = register.PatientInfo.PhoneBusiness;
            adtA01.PID.MaritalStatus.Identifier.Value = register.PatientInfo.MaritalStatus.ID.ToString();
            adtA01.PID.MaritalStatus.Text.Value = register.PatientInfo.MaritalStatus.Name;
            adtA01.PID.SSNNumberPatient.Value = register.PatientInfo.SSN;

            NHapi.Model.V24.Datatype.CX motherSID = adtA01.PID.GetMotherSIdentifier(0);
            motherSID.ID.Value = "";
            NHapi.Model.V24.Datatype.CE ethnicGroup = adtA01.PID.GetEthnicGroup(0);
            ethnicGroup.Identifier.Value = register.PatientInfo.Nationality.ID;
            ethnicGroup.Text.Value = register.PatientInfo.Nationality.Name;

            adtA01.PID.BirthPlace.Value = register.PatientInfo.DIST;

            adtA01.PID.Nationality.Identifier.Value = register.PatientInfo.Country.ID;
            adtA01.PID.Nationality.Text.Value = register.PatientInfo.Country.Name;

            adtA01.PID.ProductionClassCode.Identifier.Value = register.PatientInfo.Pact.ID;
            adtA01.PID.ProductionClassCode.Text.Value = register.PatientInfo.Pact.Name;

            //NK1 患者联系人信息
            NHapi.Model.V24.Segment.NK1 NK1 = adtA01.GetNK1(0);
            NK1.SetIDNK1.Value = "1";
            NHapi.Model.V24.Datatype.XPN linkName = NK1.GetName(0);
            linkName.FamilyName.Surname.Value = register.PatientInfo.Kin.Name;
            NK1.Relationship.Identifier.Value = register.PatientInfo.Kin.Relation.ID;
            NK1.Relationship.Text.Value = register.PatientInfo.Kin.Relation.Name;
            NHapi.Model.V24.Datatype.XAD linkAddress = NK1.GetAddress(0);
            linkAddress.StreetAddress.StreetOrMailingAddress.Value = register.PatientInfo.Kin.RelationAddress;
            NHapi.Model.V24.Datatype.XTN linkPhone = NK1.GetPhoneNumber(0);
            linkPhone.AnyText.Value = register.PatientInfo.Kin.RelationPhone;

            //PV1 患者就诊信息
            adtA01.PV1.SetIDPV1.Value = "1";
            if (register.CHKKind == "1") //个人体检
                adtA01.PV1.PatientClass.Value = "T";//类型
            else  //集体体检
                adtA01.PV1.PatientClass.Value = "J";//病人类型
            //adtA01.PV1.AssignedPatientLocation.Facility.NamespaceID.Value = register.DoctorInfo.Templet.Dept.ID;//挂号科室编码
            //adtA01.PV1.AdmissionType.Value = register.DoctorInfo.Templet.RegLevel.ID;//挂号级别
            adtA01.PV1.ReAdmissionIndicator.Value = register.PatientInfo.InTimes.ToString();//入院次数
            adtA01.PV1.VIPIndicator.Value = FS.FrameWork.Function.NConvert.ToInt32(register.VipFlag).ToString(); //VIP标识符
            NHapi.Model.V24.Datatype.XCN admittingDoctor = adtA01.PV1.GetAdmittingDoctor(0);
            //admittingDoctor.IDNumber.Value = register.DoctorInfo.Templet.Doct.ID;
            //admittingDoctor.FamilyName.Surname.Value = register.DoctorInfo.Templet.Doct.Name;
            adtA01.PV1.PatientType.Value = register.PatientInfo.Pact.PayKind.ID;
            adtA01.PV1.VisitNumber.ID.Value = register.ChkClinicNo;
            //adtA01.PV1.AdmitDateTime.TimeOfAnEvent.Value = register.DoctorInfo.SeeDate.ToString("yyyyMMddHHmmss");

            //return ProcessFactory.ProcessSender("ADT^A04", adtA01.MSH, adtA01);

            return 1;
        }

     
    }
}
