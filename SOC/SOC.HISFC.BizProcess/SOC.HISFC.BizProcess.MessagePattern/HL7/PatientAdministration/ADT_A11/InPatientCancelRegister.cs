using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A11
{
    /// <summary>
    /// 取消住院登记（无费退院）
    /// </summary>
    public class InPatientCancelRegister
    {
        /// <summary>
        /// 发送取消登记消息
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int ProcessMessage(FS.HISFC.Models.RADT.PatientInfo patientInfo, ref NHapi.Model.V24.Message.ADT_A01 adtA11,ref string errInfo)
        {
            adtA11 = new NHapi.Model.V24.Message.ADT_A01();
            //MSH 消息头
            adtA11.MSH.MessageType.MessageType.Value = "ADT";
            adtA11.MSH.MessageType.TriggerEvent.Value = "A11";
            FS.HL7Message.V24.Function.GenerateMSH(adtA11.MSH);

            //EVN 消息事件
            adtA11.EVN.EventTypeCode.Value = "A11";
            FS.HL7Message.V24.Function.GenerateEVN(adtA11.EVN);

            //PID 患者基本信息
            adtA11.PID.SetIDPID.Value = "1";
            adtA11.PID.PatientID.ID.Value = patientInfo.PID.CardNO;

            NHapi.Model.V24.Datatype.CX patientList1 = adtA11.PID.GetPatientIdentifierList(adtA11.PID.PatientIdentifierListRepetitionsUsed);

            //其他厂商确定用这个IDCard
            patientList1.IdentifierTypeCode.Value = "IDCard";//register.Card.CardType.ID;
            patientList1.ID.Value = patientInfo.PID.CardNO;

            if (!string.IsNullOrEmpty(patientInfo.Card.CardType.ID) && !patientInfo.Card.CardType.ID.Equals("IDCard"))
            {
                NHapi.Model.V24.Datatype.CX patientList2 = adtA11.PID.GetPatientIdentifierList(adtA11.PID.PatientIdentifierListRepetitionsUsed);
                patientList2.IdentifierTypeCode.Value = patientInfo.Card.CardType.ID;
                patientList2.ID.Value = patientInfo.Card.ID;
            }

            if (!string.IsNullOrEmpty(patientInfo.IDCardType.ID))
            {
                NHapi.Model.V24.Datatype.CX patientList2 = adtA11.PID.GetPatientIdentifierList(adtA11.PID.PatientIdentifierListRepetitionsUsed);
                patientList2.IdentifierTypeCode.Value = patientInfo.IDCardType.ID;
                patientList2.ID.Value = patientInfo.IDCard;
            }

            //住院号
            NHapi.Model.V24.Datatype.CX patientList3 = adtA11.PID.GetPatientIdentifierList(adtA11.PID.PatientIdentifierListRepetitionsUsed);
            patientList3.IdentifierTypeCode.Value = "PatientNO";
            patientList3.ID.Value = patientInfo.PID.PatientNO;

            NHapi.Model.V24.Datatype.XPN patientName = adtA11.PID.GetPatientName(0);
            patientName.FamilyName.Surname.Value = patientInfo.Name;
            adtA11.PID.DateTimeOfBirth.TimeOfAnEvent.Value = patientInfo.Birthday.ToString("yyyyMMddHHmmss");
            adtA11.PID.AdministrativeSex.Value = patientInfo.Sex.ID.ToString();
            NHapi.Model.V24.Datatype.XAD homeAddress = adtA11.PID.GetPatientAddress(0);
            homeAddress.AddressType.Value = "H";
            homeAddress.StreetAddress.StreetOrMailingAddress.Value = patientInfo.AddressHome;
            homeAddress.ZipOrPostalCode.Value = patientInfo.HomeZip;

            NHapi.Model.V24.Datatype.XAD businessAddress = adtA11.PID.GetPatientAddress(1);
            businessAddress.AddressType.Value = "O";
            businessAddress.StreetAddress.StreetOrMailingAddress.Value = patientInfo.AddressBusiness;
            businessAddress.ZipOrPostalCode.Value = patientInfo.BusinessZip;

            NHapi.Model.V24.Datatype.XTN homePhone = adtA11.PID.GetPhoneNumberHome(0);
            homePhone.AnyText.Value = patientInfo.PhoneHome;
            NHapi.Model.V24.Datatype.XTN businessPhone = adtA11.PID.GetPhoneNumberBusiness(0);
            businessPhone.AnyText.Value = patientInfo.PhoneBusiness;
            adtA11.PID.MaritalStatus.Identifier.Value = patientInfo.MaritalStatus.ID.ToString();
            adtA11.PID.MaritalStatus.Text.Value = patientInfo.MaritalStatus.Name;
            adtA11.PID.SSNNumberPatient.Value = patientInfo.SSN;

            NHapi.Model.V24.Datatype.CX motherSID = adtA11.PID.GetMotherSIdentifier(0);
            motherSID.ID.Value = "";
            NHapi.Model.V24.Datatype.CE ethnicGroup = adtA11.PID.GetEthnicGroup(0);
            ethnicGroup.Identifier.Value = patientInfo.Nationality.ID;
            ethnicGroup.Text.Value = patientInfo.Nationality.Name;

            adtA11.PID.BirthPlace.Value = patientInfo.DIST;

            adtA11.PID.Nationality.Identifier.Value = patientInfo.Country.ID;
            adtA11.PID.Nationality.Text.Value = patientInfo.Country.Name;

            adtA11.PID.ProductionClassCode.Identifier.Value = patientInfo.Pact.ID;
            adtA11.PID.ProductionClassCode.Text.Value = patientInfo.Pact.Name;

            //NK1 患者联系人信息
            NHapi.Model.V24.Segment.NK1 NK1 = adtA11.GetNK1(0);
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
            adtA11.PV1.SetIDPV1.Value = "1";
            adtA11.PV1.PatientClass.Value = "I";//类型
            //科室编码
            adtA11.PV1.AssignedPatientLocation.Facility.NamespaceID.Value = patientInfo.PVisit.PatientLocation.Dept.ID;
            //床位编码
            adtA11.PV1.AssignedPatientLocation.Bed.Value = patientInfo.PVisit.PatientLocation.Bed.ID.Length > 4 ? patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) : "";
            //病区编码
            adtA11.PV1.AssignedPatientLocation.PointOfCare.Value = patientInfo.PVisit.PatientLocation.NurseCell.ID;
            //入院情况
            adtA11.PV1.AdmissionType.Value = patientInfo.PVisit.Circs.ID;
            //入院来源
            adtA11.PV1.AdmitSource.Value = patientInfo.PVisit.InSource.ID;
            //入院次数
            adtA11.PV1.ReAdmissionIndicator.Value = patientInfo.InTimes.ToString();
            adtA11.PV1.VIPIndicator.Value = FS.FrameWork.Function.NConvert.ToInt32(patientInfo.VipFlag).ToString(); //VIP标识符
            //入院医生
            NHapi.Model.V24.Datatype.XCN admittingDoctor = adtA11.PV1.GetAdmittingDoctor(0);
            admittingDoctor.IDNumber.Value = patientInfo.DoctorReceiver.ID;
            admittingDoctor.FamilyName.Surname.Value = patientInfo.DoctorReceiver.Name;
            //结算类别
            adtA11.PV1.PatientType.Value = patientInfo.Pact.PayKind.ID;
            adtA11.PV1.VisitNumber.ID.Value = patientInfo.ID;
            //入院时间
            adtA11.PV1.AdmitDateTime.TimeOfAnEvent.Value = patientInfo.PVisit.InTime.ToString("yyyyMMddHHmmss");

            //return ProcessFactory.ProcessSender("ADT^A11(PV1-2=I)", adtA11.MSH, adtA11);
            return 1;
        }
    }
}
