using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HL7Message;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A08
{
    /// <summary>
    /// 患者修改信息
    /// </summary>
    public class OutPatientChangeInfo
    {
        public int ProcessMessage(FS.HISFC.Models.Registration.Register register, FS.HISFC.Models.RADT.Patient patient, ref NHapi.Model.V24.Message.ADT_A01 adtA08, ref string errInfo)
        {
            if (!string.IsNullOrEmpty(register.DoctorInfo.Templet.Dept.Memo) && (register.DoctorInfo.Templet.Dept.Memo != register.DoctorInfo.Templet.Dept.ID))//修改科室，增加护士站排队处理
            {
                //找科室对应的护士站，如果没有就用科室
                FS.HISFC.BizLogic.Manager.DepartmentStatManager departStat = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
                ArrayList alNurse = departStat.LoadByChildren("14", register.DoctorInfo.Templet.Dept.ID);
                if (alNurse == null)
                {
                    errInfo = "获取科室对应的护理站失败，原因：" + departStat.Err;
                    return -1;
                }
                string nurseID = string.Empty;
                if (alNurse.Count == 0)
                {
                    nurseID = register.DoctorInfo.Templet.Dept.ID;
                }
                else
                {
                    nurseID = (alNurse[0] as FS.HISFC.Models.Base.DepartmentStat).PardepCode;
                }
                string seeNO = string.Empty;
                if (new SOC.HISFC.Assign.BizProcess.Assign().GetNurseSeeNO(register.RegLvlFee.RegLevel.IsExpert, null, register.RegLvlFee.RegLevel.IsExpert ? register.DoctorInfo.Templet.Doct.ID : nurseID, ref seeNO, out errInfo) < 0)
                {
                    return -1;
                }
                register.DoctorInfo.SeeNO = FS.FrameWork.Function.NConvert.ToInt32(seeNO);
                register.Memo = nurseID;
                //更新挂号表
                if (this.updateRegisterSeeNO(register, out errInfo) < 0)
                {
                    return -1;
                }
            }

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
            adtA08.PID.PatientID.ID.Value = patient.PID.CardNO;

            NHapi.Model.V24.Datatype.CX patientList1 = adtA08.PID.GetPatientIdentifierList(0);
            //其他厂商确定用这个IDCard
            patientList1.IdentifierTypeCode.Value = "IDCard";//register.Card.CardType.ID;
            patientList1.ID.Value = patient.PID.CardNO;
            if (!string.IsNullOrEmpty(patient.Card.CardType.ID) && !patient.Card.CardType.ID.Equals("IDCard"))
            {
                NHapi.Model.V24.Datatype.CX patientList2 = adtA08.PID.GetPatientIdentifierList(adtA08.PID.PatientIdentifierListRepetitionsUsed);
                patientList2.IdentifierTypeCode.Value = patient.Card.CardType.ID;
                patientList2.ID.Value = patient.Card.ID;
            }

            if (!string.IsNullOrEmpty(patient.IDCardType.ID))
            {
                NHapi.Model.V24.Datatype.CX patientList3 = adtA08.PID.GetPatientIdentifierList(adtA08.PID.PatientIdentifierListRepetitionsUsed);
                patientList3.IdentifierTypeCode.Value = patient.IDCardType.ID;
                patientList3.ID.Value = patient.IDCard;
            }

            //身份证
            NHapi.Model.V24.Datatype.CX patientList4 = adtA08.PID.GetPatientIdentifierList(adtA08.PID.PatientIdentifierListRepetitionsUsed);
            patientList4.IdentifierTypeCode.Value = "IdentifyNO";
            patientList4.ID.Value = register.IDCard;

            NHapi.Model.V24.Datatype.XPN patientName = adtA08.PID.GetPatientName(0);
            patientName.FamilyName.Surname.Value = patient.Name;
            if (patient.Birthday > DateTime.MinValue)
            {
                adtA08.PID.DateTimeOfBirth.TimeOfAnEvent.Value = patient.Birthday.ToString("yyyyMMddHHmmss");
            }
            adtA08.PID.AdministrativeSex.Value = patient.Sex.ID.ToString();
            NHapi.Model.V24.Datatype.XAD homeAddress = adtA08.PID.GetPatientAddress(0);
            homeAddress.AddressType.Value = "H";
            homeAddress.StreetAddress.StreetOrMailingAddress.Value = patient.AddressHome;
            homeAddress.ZipOrPostalCode.Value = patient.HomeZip;
            NHapi.Model.V24.Datatype.XAD businessAddress = adtA08.PID.GetPatientAddress(1);
            businessAddress.AddressType.Value = "O";
            businessAddress.StreetAddress.StreetOrMailingAddress.Value = patient.AddressBusiness;
            businessAddress.ZipOrPostalCode.Value = patient.BusinessZip;
            NHapi.Model.V24.Datatype.XTN homePhone = adtA08.PID.GetPhoneNumberHome(0);
            homePhone.AnyText.Value = patient.PhoneHome;
            NHapi.Model.V24.Datatype.XTN businessPhone = adtA08.PID.GetPhoneNumberBusiness(0);
            businessPhone.AnyText.Value = patient.PhoneBusiness;
            adtA08.PID.MaritalStatus.Identifier.Value = patient.MaritalStatus.ID.ToString();
            adtA08.PID.MaritalStatus.Text.Value = patient.MaritalStatus.Name;
            adtA08.PID.SSNNumberPatient.Value = patient.SSN;
            NHapi.Model.V24.Datatype.CX motherSID = adtA08.PID.GetMotherSIdentifier(0);
            motherSID.ID.Value = "";
            NHapi.Model.V24.Datatype.CE ethnicGroup = adtA08.PID.GetEthnicGroup(0);
            ethnicGroup.Identifier.Value = patient.Nationality.ID;
            ethnicGroup.Text.Value = patient.Nationality.Name;
            adtA08.PID.BirthPlace.Value = patient.DIST;
            adtA08.PID.Nationality.Identifier.Value = patient.Country.ID;
            adtA08.PID.Nationality.Text.Value = patient.Country.Name;
            adtA08.PID.ProductionClassCode.Identifier.Value = patient.Pact.ID;
            adtA08.PID.ProductionClassCode.Text.Value = patient.Pact.Name;

            //NK1 患者联系人信息
            NHapi.Model.V24.Segment.NK1 NK1 = adtA08.GetNK1(0);
            NK1.SetIDNK1.Value = "";
            NHapi.Model.V24.Datatype.XPN linkName = NK1.GetName(0);
            linkName.FamilyName.Surname.Value = patient.Kin.Name;
            NK1.Relationship.Identifier.Value = patient.Kin.Relation.ID;
            NK1.Relationship.Text.Value = patient.Kin.Relation.Name;
            NHapi.Model.V24.Datatype.XAD linkAddress = NK1.GetAddress(0);
            linkAddress.StreetAddress.StreetOrMailingAddress.Value = patient.Kin.RelationAddress;
            NHapi.Model.V24.Datatype.XTN linkPhone = NK1.GetPhoneNumber(0);
            linkPhone.AnyText.Value = patient.Kin.RelationPhone;

            //PV1 患者就诊信息
            adtA08.PV1.SetIDPV1.Value = "1";
            adtA08.PV1.PatientClass.Value = "O";//类型
            adtA08.PV1.AssignedPatientLocation.Facility.NamespaceID.Value = register.DoctorInfo.Templet.Dept.ID;//挂号科室编码
            adtA08.PV1.AdmissionType.Value = register.DoctorInfo.Templet.RegLevel.ID;//挂号级别
            adtA08.PV1.ReAdmissionIndicator.Value = register.InTimes.ToString();//入院次数
            adtA08.PV1.VIPIndicator.Value = FS.FrameWork.Function.NConvert.ToInt32(register.VipFlag).ToString(); //VIP标识符
            NHapi.Model.V24.Datatype.XCN admittingDoctor = adtA08.PV1.GetAdmittingDoctor(0);
            admittingDoctor.IDNumber.Value = register.DoctorInfo.Templet.Doct.ID;
            admittingDoctor.FamilyName.Surname.Value = register.DoctorInfo.Templet.Doct.Name;
            adtA08.PV1.PatientType.Value = register.Pact.PayKind.ID;
            adtA08.PV1.VisitNumber.ID.Value = register.ID;
            adtA08.PV1.AdmitDateTime.TimeOfAnEvent.Value = register.DoctorInfo.SeeDate.ToString("yyyyMMddHHmmss");

            //return ProcessFactory.ProcessSender("ADT^A08(PV1-2=O)", adtA08.MSH, adtA08);
            return 1;
        }

        private int updateRegisterSeeNO(FS.HISFC.Models.Registration.Register register, out string errorInfo)
        {
            string sql = "update fin_opr_register set SEENO={1},mark1='{2}'  where clinic_code='{0}'";
            FS.FrameWork.Management.DataBaseManger dgMgr = new FS.FrameWork.Management.DataBaseManger();
            int i = dgMgr.ExecNoQuery(sql, register.ID, register.DoctorInfo.SeeNO.ToString(), register.Memo);
            errorInfo = dgMgr.Err;
            return i;
        }
    }
}
