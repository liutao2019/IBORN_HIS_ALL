using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A08
{
    /// <summary>
    /// 患者信息变更（身份、基本信息、住院号等信息）
    /// </summary>
    public class InPatientChangeInfo
    {
        /// <summary>
        /// 发送出院结算信息
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int ProcessMessage(FS.HISFC.Models.RADT.PatientInfo patientInfo,ref NHapi.Model.V24.Message.ADT_A01 adtA08,ref string errInfo)
        {
            adtA08 = new NHapi.Model.V24.Message.ADT_A01();
            //MSH 消息头
            adtA08.MSH.MessageType.MessageType.Value = "ADT";
            adtA08.MSH.MessageType.TriggerEvent.Value = "A08";
            FS.HL7Message.V24.Function.GenerateMSH(adtA08.MSH);

            //EVN 消息事件
            adtA08.EVN.EventTypeCode.Value = "A08";
            FS.HL7Message.V24.Function.GenerateEVN(adtA08.EVN);

            //PID 患者基本信息
            adtA08.PID.SetIDPID.Value = "1";
            adtA08.PID.PatientID.ID.Value = patientInfo.PID.CardNO;

            NHapi.Model.V24.Datatype.CX patientList1 = adtA08.PID.GetPatientIdentifierList(adtA08.PID.PatientIdentifierListRepetitionsUsed);

            //其他厂商确定用这个IDCard
            patientList1.IdentifierTypeCode.Value = "IDCard";//register.Card.CardType.ID;
            patientList1.ID.Value = patientInfo.PID.CardNO;

            if (!string.IsNullOrEmpty(patientInfo.Card.CardType.ID) && !patientInfo.Card.CardType.ID.Equals("IDCard"))
            {
                NHapi.Model.V24.Datatype.CX patientList2 = adtA08.PID.GetPatientIdentifierList(adtA08.PID.PatientIdentifierListRepetitionsUsed);
                patientList2.IdentifierTypeCode.Value = patientInfo.Card.CardType.ID;
                patientList2.ID.Value = patientInfo.Card.ID;
            }

            if (!string.IsNullOrEmpty(patientInfo.IDCardType.ID))
            {
                NHapi.Model.V24.Datatype.CX patientList2 = adtA08.PID.GetPatientIdentifierList(adtA08.PID.PatientIdentifierListRepetitionsUsed);
                patientList2.IdentifierTypeCode.Value = patientInfo.IDCardType.ID;
                patientList2.ID.Value = patientInfo.IDCard;
            }

            //住院号
            NHapi.Model.V24.Datatype.CX patientList3 = adtA08.PID.GetPatientIdentifierList(adtA08.PID.PatientIdentifierListRepetitionsUsed);
            patientList3.IdentifierTypeCode.Value = "PatientNO";
            patientList3.ID.Value = patientInfo.PID.PatientNO;
             
            //身份证
            NHapi.Model.V24.Datatype.CX patientList4 = adtA08.PID.GetPatientIdentifierList(adtA08.PID.PatientIdentifierListRepetitionsUsed);
            patientList4.IdentifierTypeCode.Value = "IdentifyNO";
            patientList4.ID.Value = patientInfo.IDCard;

            NHapi.Model.V24.Datatype.XPN patientName = adtA08.PID.GetPatientName(0);
            patientName.FamilyName.Surname.Value = patientInfo.Name;
            if (patientInfo.Birthday > DateTime.MinValue)
            {
                adtA08.PID.DateTimeOfBirth.TimeOfAnEvent.Value = patientInfo.Birthday.ToString("yyyyMMddHHmmss");
            }
            adtA08.PID.AdministrativeSex.Value = patientInfo.Sex.ID.ToString();
            NHapi.Model.V24.Datatype.XAD homeAddress = adtA08.PID.GetPatientAddress(0);
            homeAddress.AddressType.Value = "H";
            homeAddress.StreetAddress.StreetOrMailingAddress.Value = patientInfo.AddressHome;
            homeAddress.ZipOrPostalCode.Value = patientInfo.HomeZip;

            NHapi.Model.V24.Datatype.XAD businessAddress = adtA08.PID.GetPatientAddress(1);
            businessAddress.AddressType.Value = "O";
            businessAddress.StreetAddress.StreetOrMailingAddress.Value = patientInfo.AddressBusiness;
            businessAddress.ZipOrPostalCode.Value = patientInfo.BusinessZip;

            NHapi.Model.V24.Datatype.XTN homePhone = adtA08.PID.GetPhoneNumberHome(0);
            homePhone.AnyText.Value = patientInfo.PhoneHome;
            NHapi.Model.V24.Datatype.XTN businessPhone = adtA08.PID.GetPhoneNumberBusiness(0);
            businessPhone.AnyText.Value = patientInfo.PhoneBusiness;
            adtA08.PID.MaritalStatus.Identifier.Value = patientInfo.MaritalStatus.ID.ToString();
            adtA08.PID.MaritalStatus.Text.Value = patientInfo.MaritalStatus.Name;
            adtA08.PID.SSNNumberPatient.Value = patientInfo.SSN;

            NHapi.Model.V24.Datatype.CX motherSID = adtA08.PID.GetMotherSIdentifier(0);
            motherSID.ID.Value = "";
            NHapi.Model.V24.Datatype.CE ethnicGroup = adtA08.PID.GetEthnicGroup(0);
            ethnicGroup.Identifier.Value = patientInfo.Nationality.ID;
            ethnicGroup.Text.Value = patientInfo.Nationality.Name;

            adtA08.PID.BirthPlace.Value = patientInfo.DIST;

            adtA08.PID.Nationality.Identifier.Value = patientInfo.Country.ID;
            adtA08.PID.Nationality.Text.Value = patientInfo.Country.Name;

            adtA08.PID.ProductionClassCode.Identifier.Value = patientInfo.Pact.ID;
            adtA08.PID.ProductionClassCode.Text.Value = patientInfo.Pact.Name;

            //NK1 患者联系人信息
            NHapi.Model.V24.Segment.NK1 NK1 = adtA08.GetNK1(0);
            NK1.SetIDNK1.Value = "1";
            NHapi.Model.V24.Datatype.XPN linkName = NK1.GetName(0);
            linkName.FamilyName.Surname.Value = patientInfo.Kin.Name;
            NK1.Relationship.Identifier.Value = patientInfo.Kin.Relation.ID;
            NK1.Relationship.Text.Value = patientInfo.Kin.Relation.Name;
            NHapi.Model.V24.Datatype.XAD linkAddress = NK1.GetAddress(0);
            linkAddress.StreetAddress.StreetOrMailingAddress.Value = patientInfo.Kin.RelationAddress;
            NHapi.Model.V24.Datatype.XTN linkPhone = NK1.GetPhoneNumber(0);
            linkPhone.AnyText.Value = patientInfo.Kin.RelationPhone;

            //PV1 患者就诊信息
            adtA08.PV1.SetIDPV1.Value = "1";
            adtA08.PV1.PatientClass.Value = "I";//类型
            //科室编码
            adtA08.PV1.AssignedPatientLocation.Facility.NamespaceID.Value = patientInfo.PVisit.PatientLocation.Dept.ID;
            //床位编码
            adtA08.PV1.AssignedPatientLocation.Bed.Value = patientInfo.PVisit.PatientLocation.Bed.ID.Length > 4 ? patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) : "";
            //病区编码
            adtA08.PV1.AssignedPatientLocation.PointOfCare.Value = patientInfo.PVisit.PatientLocation.NurseCell.ID;
            //入院情况
            adtA08.PV1.AdmissionType.Value = patientInfo.PVisit.Circs.ID;
            //入院来源
            adtA08.PV1.AdmitSource.Value = patientInfo.PVisit.InSource.ID;
            //入院次数
            adtA08.PV1.ReAdmissionIndicator.Value = patientInfo.InTimes.ToString();
            adtA08.PV1.VIPIndicator.Value = FS.FrameWork.Function.NConvert.ToInt32(patientInfo.VipFlag).ToString(); //VIP标识符
            //入院医生
            NHapi.Model.V24.Datatype.XCN admittingDoctor = adtA08.PV1.GetAdmittingDoctor(0);
            admittingDoctor.IDNumber.Value = patientInfo.DoctorReceiver.ID;
            admittingDoctor.FamilyName.Surname.Value = patientInfo.DoctorReceiver.Name;
            //结算类别
            adtA08.PV1.PatientType.Value = patientInfo.Pact.PayKind.ID;
            adtA08.PV1.VisitNumber.ID.Value = patientInfo.ID;
            //入院时间
            adtA08.PV1.AdmitDateTime.TimeOfAnEvent.Value = patientInfo.PVisit.InTime.ToString("yyyyMMddHHmmss");


            //DG1入院诊断
            NHapi.Model.V24.Segment.DG1 dg1 = adtA08.GetDG1(0);
            dg1.DiagnosisCodeDG1.Identifier.Value = "";
            dg1.DiagnosisCodeDG1.Text.Value = patientInfo.ClinicDiagnose;
            dg1.DiagnosisDescription.Value = patientInfo.ClinicDiagnose;
            dg1.DiagnosisType.Value = "11";
            NHapi.Model.V24.Datatype.XCN xcn = dg1.GetDiagnosingClinician(0);
            xcn.IDNumber.Value = patientInfo.DoctorReceiver.ID;
            xcn.FamilyName.Surname.Value = patientInfo.DoctorReceiver.Name;

            //return ProcessFactory.ProcessSender("ADT^A08(PV1-2=I)", adtA08.MSH, adtA08);
            return 1;
        }
    }
}
