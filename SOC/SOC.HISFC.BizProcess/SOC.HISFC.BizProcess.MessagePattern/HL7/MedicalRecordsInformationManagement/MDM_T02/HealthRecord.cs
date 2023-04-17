using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Neusoft.SOC.HISFC.BizProcess.MessagePatternInterface;
using Neusoft.HL7Message;
using System.Collections;

namespace Neusoft.SOC.HISFC.BizProcess.MessagePattern.HL7.MedicalRecordsInformationManagement.MDM_T02
{
    public class HealthRecord
    {
        //病案首页
        public int ProcessMessage(NHapi.Model.V24.Message.MDM_T02 mdmt02, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            if (mdmt02 == null)
            {
                return -1;
            }

            Neusoft.HISFC.Models.HealthRecord.Base healthRecord = new Neusoft.HISFC.Models.HealthRecord.Base();
            Neusoft.HISFC.BizLogic.Manager.Department deptMgr = new Neusoft.HISFC.BizLogic.Manager.Department();
            Neusoft.HISFC.BizLogic.HealthRecord.Diagnose diagNoseMgr = new Neusoft.HISFC.BizLogic.HealthRecord.Diagnose();

            ArrayList alDept = deptMgr.GetRegDepartment();
            healthRecord.PatientInfo.ID = mdmt02.PV1.VisitNumber.ID.Value;//住院流水号
            healthRecord.PatientInfo.PID.CardNO = mdmt02.PID.PatientID.ID.Value;//住院病历号
 
             //住院号
            NHapi.Model.V24.Datatype.CX patientList = mdmt02.PID.GetPatientIdentifierList(mdmt02.PID.PatientIdentifierListRepetitionsUsed);
            if (patientList.IdentifierTypeCode.Value == "PatientNO")
               healthRecord.PatientInfo.PID.PatientNO = patientList.ID.Value; //住院号
            healthRecord.PatientInfo.Name = mdmt02.PID.GetPatientName(0).FamilyName.Surname.Value; //姓名
            healthRecord.Nomen = mdmt02.PID.GetPatientAlias(0).FamilyName.Surname.Value;//曾用名
              //性别
            string sexID =  mdmt02.PID.AdministrativeSex.Value;
            if (Enum.GetNames(typeof(Neusoft.HISFC.Models.Base.EnumSex)).Contains(sexID))
            {
                healthRecord.PatientInfo.Sex.ID = Enum.Parse(typeof(Neusoft.HISFC.Models.Base.EnumSex), sexID);
            }
            else
            {
                healthRecord.PatientInfo.Sex.ID = Neusoft.HISFC.Models.Base.EnumSex.M;
            }
            //出生日期
            if (string.IsNullOrEmpty(mdmt02.PID.DateTimeOfBirth.TimeOfAnEvent.Value) == false)
            {
                 healthRecord.PatientInfo.Birthday = DateTime.ParseExact(mdmt02.PID.DateTimeOfBirth.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
            }
            healthRecord.PatientInfo.Country.ID = mdmt02.PID.CountyCode.Value;//国家
            healthRecord.PatientInfo.Nationality.ID = mdmt02.PID.Nationality.Identifier.Value;////民族
            healthRecord.PatientInfo.Nationality.Name = mdmt02.PID.Nationality.Text.Value;
            //healthRecord.PatientInfo.Profession.ID  =mdmt02.PID.g              9
           //  healthRecord.PatientInfo.BloodType.ID =                           10
            healthRecord.PatientInfo.MaritalStatus.ID = mdmt02.PID.MaritalStatus.Identifier.Value; //婚否
            healthRecord.PatientInfo.Age = Neusoft.SOC.Public.Function.GetAge(healthRecord.PatientInfo.Birthday,System.DateTime.Now);
            //healthRecord.AgeUnit   = 

                //住院号
            NHapi.Model.V24.Datatype.CX patientList1 = mdmt02.PID.GetPatientIdentifierList(mdmt02.PID.PatientIdentifierListRepetitionsUsed);
            if (patientList1.IdentifierTypeCode.Value == "IdentifyNO")
               healthRecord.PatientInfo.PID.PatientNO = patientList1.ID.Value; //身份证号
            healthRecord.PatientInfo.PVisit.InSource.ID = mdmt02.PV1.AdmitSource.Value;//地区来源
            healthRecord.PatientInfo.Pact.PayKind.ID = mdmt02.PV1.PatientType.Value;
            healthRecord.PatientInfo.Pact.ID = mdmt02.PID.ProductionClassCode.Identifier.Value;
            healthRecord.PatientInfo.Pact.Name = mdmt02.PID.ProductionClassCode.Text.Value; //
            healthRecord.PatientInfo.SSN = mdmt02.PID.SSNNumberPatient.Value;//社保号
            healthRecord.PatientInfo.DIST = mdmt02.PID.Nationality.Text.Value;//籍贯
            healthRecord.PatientInfo.AreaCode = mdmt02.PID.BirthPlace.Value;// 出生地
            if(mdmt02.PID.GetPatientAddress(0).AddressType.Value.Equals("O"))
            {
                healthRecord.PatientInfo.AddressHome = mdmt02.PID.GetPatientAddress(0).StreetAddress.StreetOrMailingAddress.Value;//家庭住址
              
                healthRecord.PatientInfo.HomeZip  = mdmt02.PID.GetPatientAddress(0).ZipOrPostalCode.Value ;//住址邮编
            }
            healthRecord.PatientInfo.PhoneHome = mdmt02.PID.GetPhoneNumberHome(0).Get9999999X99999CAnyText.Value;////家庭电话
            if(mdmt02.PID.GetPatientAddress(1).AddressType.Value.Equals("H")) //单位地址
            {
               healthRecord.PatientInfo.AddressBusiness = mdmt02.PID.GetPatientAddress(1).StreetAddress.StreetOrMailingAddress.Value;
               healthRecord.PatientInfo.BusinessZip =mdmt02.PID.GetPatientAddress(1).ZipOrPostalCode.Value;
               
            }
            healthRecord.PatientInfo.PhoneBusiness = mdmt02.PID.GetPhoneNumberBusiness(0).Get9999999X99999CAnyText.Value;//单位电话
            healthRecord.PatientInfo.Kin.Name = mdmt02.GetNK1().GetName(0).FamilyName.Surname.Value;//联系人
            //healthRecord.PatientInfo.Kin.RelationLink = mdmt02.GetNK1(0).Relationship.Identifier.Value;//与患者关系
            healthRecord.PatientInfo.Kin.RelationLink = mdmt02.GetNK1().Relationship.Text.Value;//
            healthRecord.PatientInfo.Kin.RelationPhone = mdmt02.GetNK1().GetPhoneNumber(0).Get9999999X99999CAnyText.Value;
            healthRecord.PatientInfo.Kin.RelationAddress = mdmt02.GetNK1().GetAddress(0).StreetAddress.StreetOrMailingAddress.Value;//联系地址
            healthRecord.ClinicDoc.ID = mdmt02.PV1.GetAttendingDoctor(0).IDNumber.Value; ////门诊诊断医生
            healthRecord.ClinicDoc.Name = mdmt02.PV1.GetAttendingDoctor(0).FamilyName.Surname.Value;//门诊诊断医生姓名
            // healthRecord.ComeFrom = mdmt02.PV1.g    //转来医院
            healthRecord.PatientInfo.PVisit.InTime = DateTime.ParseExact(mdmt02.PV1.AdmitDateTime.TimeOfAnEvent.Value, "yyyy-MM-dd hh24:mi:ss", null); //入院时间
            healthRecord.PatientInfo.InTimes = Convert.ToInt32(mdmt02.PV1.ReAdmissionIndicator.Value);//住院次数
            healthRecord.InDept.ID = mdmt02.PV1.AssignedPatientLocation.Facility.NamespaceID.Value;
            healthRecord.InDept.Name = deptMgr.GetDeptmentById(mdmt02.PV1.AssignedPatientLocation.Facility.NamespaceID.Value).Name;// 入院科室名称
            healthRecord.PatientInfo.PVisit.InSource.ID = mdmt02.PV1.AdmitSource.Value; //入院来源
            healthRecord.PatientInfo.PVisit.Circs.ID = mdmt02.PV1.AdmissionType.Value;// 入院情况  39
            //确诊日期  healthRecord.DiagDate   40
            // 手术日期 healthRecord.OperationDate 41
            healthRecord.PatientInfo.PVisit.OutTime = DateTime.ParseExact(mdmt02.PV1.GetDischargeDateTime(0).TimeOfAnEvent.Value,"yyyy-MM-dd hh:mi:ss",null);
            //healthRecord.OutDept.ID  =mdmt0


            #region 诊断
            //诊断
             List<Neusoft.HISFC.Models.HealthRecord.Diagnose> diagList = new List<Neusoft.HISFC.Models.HealthRecord.Diagnose>();
             Neusoft.HISFC.Models.HealthRecord.Diagnose Diagnose = new Neusoft.HISFC.Models.HealthRecord.Diagnose();
            for(int i=0; i< mdmt02.GetDG1().DiagnosingClinicianRepetitionsUsed;i++)
            {
                Diagnose.DiagInfo.Patient.ID = mdmt02.PV1.VisitNumber.ID.Value;
                Diagnose.DiagInfo.Patient.PID.PatientNO = mdmt02.PID.PatientID.ID.Value;
                Diagnose.DiagInfo.DiagType.ID = mdmt02.GetDG1().DiagnosisPriority.Value; //将主诊断设置成  为1
                if (Diagnose.DiagInfo.DiagType.ID == "1") //将主诊断设置成 
                {
                    Diagnose.DiagInfo.IsMain = true;
                }
                else
                {
                    Diagnose.DiagInfo.IsMain = false;
                }
                Diagnose.DiagInfo.ICD10.ID = mdmt02.GetDG1().DiagnosisCodeDG1.Identifier.Value;// 诊断代码 
                Diagnose.DiagInfo.ICD10.Name = mdmt02.GetDG1().DiagnosisDescription.Value;//诊断名称
                Diagnose.DiagOutState = mdmt02.GetDG1().DRGApprovalIndicator.Value;//入院情况
                diagList.Add(Diagnose);
              
            }
            if (diagList != null && diagList.Count > 0)
            {

                diagNoseMgr.DeleteDiagnoseAll(mdmt02.PV1.VisitNumber.ID.Value, Neusoft.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, Neusoft.HISFC.Models.Base.ServiceTypes.I);
                foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose obj in diagList)
                {
                    if (diagNoseMgr.InsertDiagnose(obj) < 1)
                    {
                        errInfo = "接收病案诊断插入失败" + diagNoseMgr.Err;   
                         return -1;
                    }
                }
            }
            #endregion





            return 1;







        }
    }
}
