using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A08
{
    /// <summary>
    /// 体检信息修改
    /// </summary>
    class HealthCheckupChangeInfo
    {
        public int ProcessMessage(FS.HISFC.HealthCheckup.Object.ChkRegister register, ref NHapi.Model.V24.Message.ADT_A01 adtA08, ref string errInfo)
        {
            adtA08 = new NHapi.Model.V24.Message.ADT_A01();
            //MSH 消息头
            adtA08.MSH.MessageType.MessageType.Value = "ADT";
            adtA08.MSH.MessageType.TriggerEvent.Value = "A08";
            FS.HL7Message.V24.Function.GenerateMSH(adtA08.MSH);
            adtA08.MSH.SendingApplication.NamespaceID.Value = "PEIS";
            //EVN 消息事件
            adtA08.EVN.EventTypeCode.Value = "A08";
            FS.HL7Message.V24.Function.GenerateEVN(adtA08.EVN);

            //PID 患者基本信息
            adtA08.PID.SetIDPID.Value = "1";
            adtA08.PID.PatientID.ID.Value = register.PatientInfo.PID.CardNO;

            NHapi.Model.V24.Datatype.CX patientList1 = adtA08.PID.GetPatientIdentifierList(0);
            //其他厂商确定用这个IDCard
            patientList1.IdentifierTypeCode.Value = "IDCard";//register.Card.CardType.ID;
            patientList1.ID.Value = register.PatientInfo.PID.CardNO;
            if (!string.IsNullOrEmpty(register.PatientInfo.Card.CardType.ID) && !register.PatientInfo.Card.CardType.ID.Equals("IDCard"))
            {
                NHapi.Model.V24.Datatype.CX patientList2 = adtA08.PID.GetPatientIdentifierList(adtA08.PID.PatientIdentifierListRepetitionsUsed);
                patientList2.IdentifierTypeCode.Value = register.PatientInfo.Card.CardType.ID;
                patientList2.ID.Value = register.PatientInfo.Card.ID;
            }

            if (!string.IsNullOrEmpty(register.PatientInfo.IDCardType.ID))
            {
                NHapi.Model.V24.Datatype.CX patientList3 = adtA08.PID.GetPatientIdentifierList(adtA08.PID.PatientIdentifierListRepetitionsUsed);
                patientList3.IdentifierTypeCode.Value = register.PatientInfo.IDCardType.ID;
                patientList3.ID.Value = register.PatientInfo.IDCard;
            }

            NHapi.Model.V24.Datatype.XPN patientName = adtA08.PID.GetPatientName(0);
            patientName.FamilyName.Surname.Value = register.PatientInfo.Name;
            if (register.PatientInfo.Birthday > DateTime.MinValue)
            {
                adtA08.PID.DateTimeOfBirth.TimeOfAnEvent.Value = register.PatientInfo.Birthday.ToString("yyyyMMddHHmmss");
            }
            adtA08.PID.AdministrativeSex.Value = register.PatientInfo.Sex.ID.ToString();
            NHapi.Model.V24.Datatype.XAD homeAddress = adtA08.PID.GetPatientAddress(0);
            homeAddress.AddressType.Value = "H";
            homeAddress.StreetAddress.StreetOrMailingAddress.Value = register.PatientInfo.AddressHome;
            homeAddress.ZipOrPostalCode.Value = register.PatientInfo.HomeZip;
            NHapi.Model.V24.Datatype.XAD businessAddress = adtA08.PID.GetPatientAddress(1);
            businessAddress.AddressType.Value = "O";
            businessAddress.StreetAddress.StreetOrMailingAddress.Value = register.PatientInfo.AddressBusiness;
            businessAddress.ZipOrPostalCode.Value = register.PatientInfo.BusinessZip;
            NHapi.Model.V24.Datatype.XTN homePhone = adtA08.PID.GetPhoneNumberHome(0);
            homePhone.AnyText.Value = register.PatientInfo.PhoneHome;
            NHapi.Model.V24.Datatype.XTN businessPhone = adtA08.PID.GetPhoneNumberBusiness(0);
            businessPhone.AnyText.Value = register.PatientInfo.PhoneBusiness;
            adtA08.PID.MaritalStatus.Identifier.Value = register.PatientInfo.MaritalStatus.ID.ToString();
            adtA08.PID.MaritalStatus.Text.Value = register.PatientInfo.MaritalStatus.Name;
            adtA08.PID.SSNNumberPatient.Value = register.PatientInfo.SSN;
            NHapi.Model.V24.Datatype.CX motherSID = adtA08.PID.GetMotherSIdentifier(0);
            motherSID.ID.Value = "";
            NHapi.Model.V24.Datatype.CE ethnicGroup = adtA08.PID.GetEthnicGroup(0);
            ethnicGroup.Identifier.Value = register.PatientInfo.Nationality.ID;
            ethnicGroup.Text.Value = register.PatientInfo.Nationality.Name;
            adtA08.PID.BirthPlace.Value = register.PatientInfo.DIST;
            adtA08.PID.Nationality.Identifier.Value = register.PatientInfo.Country.ID;
            adtA08.PID.Nationality.Text.Value = register.PatientInfo.Country.Name;
            adtA08.PID.ProductionClassCode.Identifier.Value = register.PatientInfo.Pact.ID;
            adtA08.PID.ProductionClassCode.Text.Value = register.PatientInfo.Pact.Name;

            //NK1 患者联系人信息
            NHapi.Model.V24.Segment.NK1 NK1 = adtA08.GetNK1(0);
            NK1.SetIDNK1.Value = "";
            NHapi.Model.V24.Datatype.XPN linkName = NK1.GetName(0);
            linkName.FamilyName.Surname.Value = register.PatientInfo.Kin.Name;
            NK1.Relationship.Identifier.Value = register.PatientInfo.Kin.Relation.ID;
            NK1.Relationship.Text.Value = register.PatientInfo.Kin.Relation.Name;
            NHapi.Model.V24.Datatype.XAD linkAddress = NK1.GetAddress(0);
            linkAddress.StreetAddress.StreetOrMailingAddress.Value = register.PatientInfo.Kin.RelationAddress;
            NHapi.Model.V24.Datatype.XTN linkPhone = NK1.GetPhoneNumber(0);
            linkPhone.AnyText.Value = register.PatientInfo.Kin.RelationPhone;

            //PV1 患者就诊信息
            adtA08.PV1.SetIDPV1.Value = "1";
            if (register.CHKKind == "1")
                adtA08.PV1.PatientClass.Value = "T";//类型
            else
                adtA08.PV1.PatientClass.Value = "J";//集体体检
            //adtA08.PV1.AssignedPatientLocation.Facility.NamespaceID.Value = register.DoctorInfo.Templet.Dept.ID;//挂号科室编码
            //adtA08.PV1.AdmissionType.Value = register.DoctorInfo.Templet.RegLevel.ID;//挂号级别
            adtA08.PV1.ReAdmissionIndicator.Value = register.PatientInfo.InTimes.ToString();//入院次数
            adtA08.PV1.VIPIndicator.Value = FS.FrameWork.Function.NConvert.ToInt32(register.PatientInfo.VipFlag).ToString(); //VIP标识符
            NHapi.Model.V24.Datatype.XCN admittingDoctor = adtA08.PV1.GetAdmittingDoctor(0);
            //admittingDoctor.IDNumber.Value = register.DoctorInfo.Templet.Doct.ID;
            //admittingDoctor.FamilyName.Surname.Value = register.DoctorInfo.Templet.Doct.Name;
            adtA08.PV1.PatientType.Value = register.PatientInfo.Pact.PayKind.ID;
            adtA08.PV1.VisitNumber.ID.Value = register.ChkClinicNo;
            //adtA08.PV1.AdmitDateTime.TimeOfAnEvent.Value = register.DoctorInfo.SeeDate.ToString("yyyyMMddHHmmss");

            //return ProcessFactory.ProcessSender("ADT^A08(PV1-2=O)", adtA08.MSH, adtA08);
            return 1;
        }
    }
}
