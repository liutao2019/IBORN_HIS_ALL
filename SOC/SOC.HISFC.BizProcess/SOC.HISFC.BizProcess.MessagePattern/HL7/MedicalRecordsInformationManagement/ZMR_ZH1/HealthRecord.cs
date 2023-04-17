using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MedicalRecordsInformationManagement.ZMR_ZH1
{
    public class HealthRecord
    {
        //病案首页
        public int ProcessMessage(NHapi.Model.V24.Message.ZMR_ZH1 zmrzh1 , ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            if (zmrzh1 == null)
            {
                return -1;
            }
             //病案基本信息操作类
            FS.HISFC.BizLogic.HealthRecord.Base baseMgr = new FS.HISFC.BizLogic.HealthRecord.Base();
            FS.HISFC.Models.HealthRecord.Base healthRecord = new FS.HISFC.Models.HealthRecord.Base();
            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            FS.HISFC.BizLogic.HealthRecord.Diagnose diagNoseMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            FS.HISFC.BizLogic.HealthRecord.Fee healthRecordFee = new FS.HISFC.BizLogic.HealthRecord.Fee();
            #region 病人基本信息
            ArrayList alDept = deptMgr.GetRegDepartment();
            healthRecord.PatientInfo.ID = zmrzh1.PV1.VisitNumber.ID.Value;//住院流水号
            healthRecord.PatientInfo.PID.CardNO = zmrzh1.PID.PatientID.ID.Value;//住院病历号
            for (int i = 0; i < zmrzh1.PID.PatientIdentifierListRepetitionsUsed; i++)
            {
                if (zmrzh1.PID.GetPatientIdentifierList(i).IdentifierTypeCode.Value.Equals("PatientNO")) //住院号
                {
                    healthRecord.PatientInfo.PID.PatientNO = zmrzh1.PID.GetPatientIdentifierList(i).ID.Value;
                }
                if(zmrzh1.PID.GetPatientIdentifierList(i).IdentifierTypeCode.Value.Equals("IdentifyNO"))
                {
                    healthRecord.PatientInfo.IDCard = zmrzh1.PID.GetPatientIdentifierList(i).ID.Value;
                }
            }

             //住院号
            //NHapi.Model.V24.Datatype.CX patientList = zmrzh1.PID.GetPatientIdentifierList(zmrzh1.PID.PatientIdentifierListRepetitionsUsed);
            //if (patientList.IdentifierTypeCode.Value == "PatientNO")
            //   healthRecord.PatientInfo.PID.PatientNO = patientList.ID.Value; //住院号
            healthRecord.PatientInfo.Name = zmrzh1.PID.GetPatientName(0).FamilyName.Surname.Value; //姓名
            healthRecord.Nomen = zmrzh1.PID.GetPatientAlias(0).FamilyName.Surname.Value;//曾用名
              //性别
            string sexID =  zmrzh1.PID.AdministrativeSex.Value;
            if (Enum.GetNames(typeof(FS.HISFC.Models.Base.EnumSex)).Contains(sexID))
            {
                healthRecord.PatientInfo.Sex.ID = Enum.Parse(typeof(FS.HISFC.Models.Base.EnumSex), sexID);
            }
            else
            {
                healthRecord.PatientInfo.Sex.ID = FS.HISFC.Models.Base.EnumSex.M;
            }
            //出生日期
            if (string.IsNullOrEmpty(zmrzh1.PID.DateTimeOfBirth.TimeOfAnEvent.Value) == false)
            {
                healthRecord.PatientInfo.Birthday = DateTime.ParseExact(zmrzh1.PID.DateTimeOfBirth.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
                //healthRecord.PatientInfo.Age = FS.SOC.Public.Function.GetAge(healthRecord.PatientInfo.Birthday, System.DateTime.Now);
                
            }
        
            healthRecord.PatientInfo.Age = "0"; //年龄
           
            healthRecord.PatientInfo.Country.ID = zmrzh1.PID.CountyCode.Value;//国家
            healthRecord.PatientInfo.Nationality.ID = zmrzh1.PID.Nationality.Identifier.Value;////民族
            healthRecord.PatientInfo.Nationality.Name = zmrzh1.PID.Nationality.Text.Value;
            //healthRecord.PatientInfo.Profession.ID  =zmrzh1.PID.g              9
           //  healthRecord.PatientInfo.BloodType.ID =                           10
            healthRecord.PatientInfo.MaritalStatus.ID = zmrzh1.PID.MaritalStatus.Identifier.Value; //婚否
           //healthRecord.AgeUnit   = 

                //住院号
            NHapi.Model.V24.Datatype.CX patientList1 = zmrzh1.PID.GetPatientIdentifierList(zmrzh1.PID.PatientIdentifierListRepetitionsUsed);
            if (patientList1.IdentifierTypeCode.Value == "IdentifyNO")
               healthRecord.PatientInfo.PID.PatientNO = patientList1.ID.Value; //身份证号
            healthRecord.PatientInfo.PVisit.InSource.ID = zmrzh1.PV1.AdmitSource.Value;//地区来源
            healthRecord.PatientInfo.Pact.PayKind.ID = zmrzh1.PV1.PatientType.Value;
            healthRecord.PatientInfo.Pact.ID = zmrzh1.PID.ProductionClassCode.Identifier.Value;
            healthRecord.PatientInfo.Pact.Name = zmrzh1.PID.ProductionClassCode.Text.Value; //
            healthRecord.PatientInfo.SSN = zmrzh1.PID.SSNNumberPatient.Value;//社保号
            healthRecord.PatientInfo.DIST = zmrzh1.PID.Nationality.Text.Value;//籍贯
            healthRecord.PatientInfo.AreaCode = zmrzh1.PID.BirthPlace.Value;// 出生地
            if(zmrzh1.PID.GetPatientAddress(0).AddressType.Value.Equals("O"))
            {
                healthRecord.PatientInfo.AddressHome = zmrzh1.PID.GetPatientAddress(0).StreetAddress.StreetOrMailingAddress.Value;//家庭住址
              
                healthRecord.PatientInfo.HomeZip  = zmrzh1.PID.GetPatientAddress(0).ZipOrPostalCode.Value ;//住址邮编
            }
            healthRecord.PatientInfo.PhoneHome = zmrzh1.PID.GetPhoneNumberHome(0).Get9999999X99999CAnyText.Value;////家庭电话
            if(zmrzh1.PID.GetPatientAddress(1).AddressType.Value.Equals("H")) //单位地址
            {
               healthRecord.PatientInfo.AddressBusiness = zmrzh1.PID.GetPatientAddress(1).StreetAddress.StreetOrMailingAddress.Value;
               healthRecord.PatientInfo.BusinessZip =zmrzh1.PID.GetPatientAddress(1).ZipOrPostalCode.Value;
               
            }
            healthRecord.PatientInfo.PhoneBusiness = zmrzh1.PID.GetPhoneNumberBusiness(0).Get9999999X99999CAnyText.Value;//单位电话
            healthRecord.PatientInfo.Kin.Name = zmrzh1.GetNK1().GetName(0).FamilyName.Surname.Value;//联系人
            //healthRecord.PatientInfo.Kin.RelationLink = zmrzh1.GetNK1(0).Relationship.Identifier.Value;//与患者关系
            healthRecord.PatientInfo.Kin.RelationLink = zmrzh1.GetNK1().Relationship.Text.Value;//
            healthRecord.PatientInfo.Kin.RelationPhone = zmrzh1.GetNK1().GetPhoneNumber(0).Get9999999X99999CAnyText.Value;
            healthRecord.PatientInfo.Kin.RelationAddress = zmrzh1.GetNK1().GetAddress(0).StreetAddress.StreetOrMailingAddress.Value;//联系地址
            healthRecord.ClinicDoc.ID = zmrzh1.PV1.GetAttendingDoctor(0).IDNumber.Value; ////门诊诊断医生
            healthRecord.ClinicDoc.Name = zmrzh1.PV1.GetAttendingDoctor(0).FamilyName.Surname.Value;//门诊诊断医生姓名
            // healthRecord.ComeFrom = zmrzh1.PV1.g    //转来医院
            healthRecord.PatientInfo.PVisit.InTime = DateTime.ParseExact(zmrzh1.PV1.AdmitDateTime.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null); //入院时间
            healthRecord.PatientInfo.InTimes = Convert.ToInt32(zmrzh1.PV1.ReAdmissionIndicator.Value);//住院次数
            healthRecord.InDept.ID = zmrzh1.PV1.AssignedPatientLocation.Facility.NamespaceID.Value;
            healthRecord.InDept.Name = deptMgr.GetDeptmentById(zmrzh1.PV1.AssignedPatientLocation.Facility.NamespaceID.Value).Name;// 入院科室名称
            healthRecord.PatientInfo.PVisit.InSource.ID = zmrzh1.PV1.AdmitSource.Value; //入院来源
            healthRecord.PatientInfo.PVisit.Circs.ID = zmrzh1.PV1.AdmissionType.Value;// 入院情况  39
            //确诊日期  healthRecord.DiagDate   40
            // 手术日期 healthRecord.OperationDate 41
            if (zmrzh1.PV1.GetDischargeDateTime(0).TimeOfAnEvent.Value != null)
            {
                healthRecord.PatientInfo.PVisit.OutTime = DateTime.ParseExact(zmrzh1.PV1.GetDischargeDateTime(0).TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
            }
            healthRecord.OutDept.ID = zmrzh1.GetZMI().DischargeDept.Facility.NamespaceID.Value;//  出院科室
            healthRecord.OutDept.Name = zmrzh1.GetZMI().DischargeDept.Facility.Description;//出院科室名称

            healthRecord.OutRoom = zmrzh1.GetZMI().DischargeWard.Facility.NamespaceID.Value;
            healthRecord.InHospitalDays = FS.FrameWork.Function.NConvert.ToInt32(zmrzh1.GetZMI().LengthofInpatientStay.Value); //住院天数
            healthRecord.ComeBackInMonth = zmrzh1.GetZMI().ReadmissionIndicator.Value;//是否有出院31天内再住院计划 1,2
            healthRecord.ComeBackPurpose = zmrzh1.GetZMI().TransferReason.Text.Value;//转移原因
            healthRecord.InHospitalDays = FS.FrameWork.Function.NConvert.ToInt32(zmrzh1.GetZMI().ActualLengthofInpatientStay.Value);//实际住院日长度
            healthRecord.PatientInfo.PVisit.ReferringDoctor.ID = zmrzh1.GetZMI().DeptDirector.IDNumber.Value;
            healthRecord.PatientInfo.PVisit.ReferringDoctor.Name = zmrzh1.GetZMI().DeptDirector.FamilyName.Surname.Value;//科主任医生
            healthRecord.PatientInfo.PVisit.ConsultingDoctor.ID = zmrzh1.GetZMI().ChiefDoctor.IDNumber.Value;
            healthRecord.PatientInfo.PVisit.ConsultingDoctor.Name = zmrzh1.GetZMI().ChiefDoctor.FamilyName.Surname.Value;//主任(副主任)
            healthRecord.PatientInfo.PVisit.AttendingDoctor.ID = zmrzh1.GetZMI().DoctorinChargeofACase.IDNumber.Value;
            healthRecord.PatientInfo.PVisit.AttendingDoctor.Name = zmrzh1.GetZMI().DoctorinChargeofACase.FamilyName.Surname.Value;//主治医生
            healthRecord.PatientInfo.PVisit.AdmittingDoctor.ID = zmrzh1.GetZMI().ReferPhysicianDoctor.IDNumber.Value;
            healthRecord.PatientInfo.PVisit.AdmittingDoctor.Name = zmrzh1.GetZMI().ReferPhysicianDoctor.FamilyName.Surname.Value;//住院医师
            healthRecord.DutyNurse.ID = zmrzh1.GetZMI().PrimaryNurse.IDNumber.Value;
            healthRecord.DutyNurse.Name = zmrzh1.GetZMI().PrimaryNurse.FamilyName.Surname.Value;//责任护士
            healthRecord.RefresherDoc.ID = zmrzh1.GetZMI().LearnDoctor.IDNumber.Value;
            healthRecord.RefresherDoc.Name = zmrzh1.GetZMI().LearnDoctor.FamilyName.Surname.Value; //进修医师
            healthRecord.PatientInfo.PVisit.TempDoctor.ID = zmrzh1.GetZMI().PracticeDoctor.IDNumber.Value;
            healthRecord.PatientInfo.PVisit.TempDoctor.Name = zmrzh1.GetZMI().PracticeDoctor.FamilyName.Surname.Value;//实习医师
            healthRecord.CodingOper.ID = zmrzh1.GetZMI().EncoderDoctor.IDNumber.Value;
            healthRecord.CodingOper.Name = zmrzh1.GetZMI().EncoderDoctor.FamilyName.Surname.Value;//编码员
            healthRecord.QcDoc.ID = zmrzh1.GetZMI().QualityControlDoctor.IDNumber.Value;
            healthRecord.QcDoc.Name = zmrzh1.GetZMI().QualityControlDoctor.FamilyName.Surname.Value;//质控医师
            healthRecord.QcNurse.ID = zmrzh1.GetZMI().qualitycontrolNurse.IDNumber.Value;
            healthRecord.QcNurse.Name = zmrzh1.GetZMI().qualitycontrolNurse.FamilyName.Surname.Value;//质控护士
            healthRecord.PathologicalDiagCode = zmrzh1.GetZMI().PathologyDiagnosisCode.Identifier.Value;//病理诊断编码
            healthRecord.PathologicalDiagName = zmrzh1.GetZMI().PathologyDiagnosisDescription.Value;//病理诊断名称
            healthRecord.PathNum = zmrzh1.GetZMI().PathologyNumber.ID.Value;//病理号
            healthRecord.InjuryOrPoisoningCauseCode = zmrzh1.GetZMI().ScathingDiagnosisCode.Identifier.Value;
            healthRecord.InjuryOrPoisoningCause = zmrzh1.GetZMI().ScathingPathologyDiagnosisDescription.Value;
            healthRecord.PatientInfo.SSN = zmrzh1.GetZMI().SSNNumberPatient.Value;
            healthRecord.PatientInfo.BloodType.ID = zmrzh1.GetZMI().BloodType.Value;
            healthRecord.RhBlood = zmrzh1.GetZMI().RHType.Value;//RH型
            healthRecord.MrQuality = zmrzh1.GetZMI().MedicalRecordsManagement.Value;
            healthRecord.InPath = zmrzh1.GetZMI().AdmissRoad.Value;//入院途径
            healthRecord.OutComeDay = FS.FrameWork.Function.NConvert.ToInt32(zmrzh1.GetZMI().BeforeAdmissStuporDays.Value);
            healthRecord.OutComeHour =FS.FrameWork.Function.NConvert.ToInt32( zmrzh1.GetZMI().BeforeAdmissStuporHours.Value);
            healthRecord.OutComeMin = FS.FrameWork.Function.NConvert.ToInt32(zmrzh1.GetZMI().BeforeAdmissStuporMinutes.Value);
            healthRecord.InComeDay = FS.FrameWork.Function.NConvert.ToInt32(zmrzh1.GetZMI().AfterAdmissStuporDays.Value);
            healthRecord.InComeHour = FS.FrameWork.Function.NConvert.ToInt32(zmrzh1.GetZMI().AfterAdmissStuporHours.Value);
            healthRecord.InComeMin = FS.FrameWork.Function.NConvert.ToInt32(zmrzh1.GetZMI().AfterAdmissStuporMinutes.Value);
            healthRecord.PatientInfo.Profession.ID = zmrzh1.GetZMI().Profession.Value;//职业


          
            #endregion

            #region 诊断
            //诊断
             List<FS.HISFC.Models.HealthRecord.Diagnose> diagList = new List<FS.HISFC.Models.HealthRecord.Diagnose>();

             for (int i = 0; i < zmrzh1.GetDETAIL().DG1RepetitionsUsed; i++)
            {
                FS.HISFC.Models.HealthRecord.Diagnose Diagnose = new FS.HISFC.Models.HealthRecord.Diagnose();
                Diagnose.DiagInfo.Patient.ID = zmrzh1.PV1.VisitNumber.ID.Value;
                Diagnose.DiagInfo.Patient.PID.PatientNO = zmrzh1.PID.PatientID.ID.Value;
                Diagnose.Pvisit.InTime = DateTime.ParseExact(zmrzh1.PV1.AdmitDateTime.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
                Diagnose.DiagInfo.DiagType.ID = zmrzh1.GetDETAIL().GetDG1().DiagnosisPriority.Value; //将主诊断设置成  为1
                if (Diagnose.DiagInfo.DiagType.ID == "1") //将主诊断设置成 
                {
                    Diagnose.DiagInfo.IsMain = true;
                }
                else
                {
                    Diagnose.DiagInfo.IsMain = false;
                }
                if (zmrzh1.GetDETAIL().GetDG1().DiagnosisType.Value == "04") //为门诊诊断
                {
                    healthRecord.ClinicDiag.ID = zmrzh1.GetDETAIL().GetDG1().DiagnosisCodeDG1.Identifier.Value;
                    healthRecord.ClinicDiag.Name = zmrzh1.GetDETAIL().GetDG1().DiagnosisDescription.Value;
                    continue;
                }
                Diagnose.DiagInfo.ICD10.ID = zmrzh1.GetDETAIL().GetDG1().DiagnosisCodeDG1.Identifier.Value;// 诊断代码 
                Diagnose.DiagInfo.ICD10.Name = zmrzh1.GetDETAIL().GetDG1().DiagnosisDescription.Value;//诊断名称
                Diagnose.DiagOutState = zmrzh1.GetDETAIL().GetDG1().DRGApprovalIndicator.Value;//入院情况
                Diagnose.OperType = "1";
                Diagnose.DiagInfo.Doctor.ID = zmrzh1.GetZMI().DoctorinChargeofACase.IDNumber.Value;
                Diagnose.DiagInfo.Doctor.Name = zmrzh1.GetZMI().DoctorinChargeofACase.FamilyName.Surname.Value;

                diagList.Add(Diagnose);
              
            }


            if (diagList != null && diagList.Count > 0)
            {

                diagNoseMgr.DeleteDiagnoseAll(zmrzh1.PV1.VisitNumber.ID.Value, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, FS.HISFC.Models.Base.ServiceTypes.I);
                foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in diagList)
                {
                    if (diagNoseMgr.InsertDiagnose(obj) < 1)
                    {
                        errInfo = "接收病案诊断插入失败" + diagNoseMgr.Err;   
                         return -1;
                    }
                }
            }
            #endregion


            //先执行更新操作 
            if (baseMgr.UpdateBaseInfo(healthRecord) < 1)
            {

                //更新失败 则执行插入操作 
                if (baseMgr.InsertBaseInfo(healthRecord) < 1)
                {
                    //回退
                    errInfo = "病案插入失败" + baseMgr.Err;
                    return -1;
                }
            }
            #region  手术
            FS.HISFC.BizLogic.HealthRecord.Operation operation = new FS.HISFC.BizLogic.HealthRecord.Operation();
            ArrayList operationList = new ArrayList();
            for (int j = 0; j < zmrzh1.GetDETAIL().ZMSRepetitionsUsed; j++)
            {
                FS.HISFC.Models.HealthRecord.OperationDetail OperationDetail = new FS.HISFC.Models.HealthRecord.OperationDetail();
                OperationDetail.OperType = "1";
                OperationDetail.InpatientNO = zmrzh1.PV1.VisitNumber.ID.Value;//住院流水号
                OperationDetail.OperationInfo.ID = zmrzh1.GetDETAIL().GetZMS().ProcedureCode.Identifier.Value;//手术编码
                OperationDetail.OperationInfo.Name = zmrzh1.GetDETAIL().GetZMS().ProcedureDescription.Value;//手术名称
                if (zmrzh1.GetDETAIL().GetZMS().ProcedureDateTime.TimeOfAnEvent.Value != null)
                {
                    OperationDetail.OperationDate = DateTime.ParseExact(zmrzh1.GetDETAIL().GetZMS().ProcedureDateTime.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
                }
                OperationDetail.NarcDoctInfo.ID = zmrzh1.GetDETAIL().GetZMS().GetAnesthesiologist(0).IDNumber.Value;//麻醉医生
                OperationDetail.NarcDoctInfo.Name = zmrzh1.GetDETAIL().GetZMS().GetAnesthesiologist(0).FamilyName.Surname.Value;//
                OperationDetail.MarcKind = zmrzh1.GetDETAIL().GetZMS().AnesthesiaCode.Identifier.Value;//麻醉方法
                OperationDetail.FirDoctInfo.ID = zmrzh1.GetDETAIL().GetZMS().GetProcedurePractitioner(0).IDNumber.Value;
                OperationDetail.FirDoctInfo.Name = zmrzh1.GetDETAIL().GetZMS().GetProcedurePractitioner(0).FamilyName.Surname.Value;
                OperationDetail.SecDoctInfo.ID = zmrzh1.GetDETAIL().GetZMS().FirstAsistant.Identifier.Value; //一助
                OperationDetail.ThrDoctInfo.ID = zmrzh1.GetDETAIL().GetZMS().SecondAsistant.Identifier.Value; //二助
                OperationDetail.FourDoctInfo.Name = zmrzh1.GetDETAIL().GetZMS().OperationCode.Identifier.Value;//手术级别
                OperationDetail.OperationKind = zmrzh1.GetDETAIL().GetZMS().EmergencyIndicator.Identifier.Value;//择期手术
                OperationDetail.NickKind = zmrzh1.GetDETAIL().GetZMS().CutHealGrade.Value;//切口愈合等级
                OperationDetail.OperationNO = zmrzh1.GetDETAIL().GetZMS().SetIDZMS.Value;
                operationList.Add(OperationDetail);
            }

            if (operationList != null && operationList.Count > 0)
            {
                operation.deleteAll(zmrzh1.PV1.VisitNumber.ID.Value);

                foreach (FS.HISFC.Models.HealthRecord.OperationDetail obj in operationList)
                {
                    if (operation.Insert(FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, obj) < 1)
                    {
                       // FS.FrameWork.Management.PublicTrans.RollBack();
                        errInfo = "保存手术诊断信息失败" + operation.Err;
                        return -1;
                    }
                }
            }

            #endregion

            #region  费用信息

            FS.HISFC.BizLogic.HealthRecord.Fee ba = new FS.HISFC.BizLogic.HealthRecord.Fee();


            ArrayList feeList = ba.QueryFeeInfoState(zmrzh1.PV1.VisitNumber.ID.Value);

            if (feeList != null)
            {
                foreach (FS.HISFC.Models.RADT.Patient obj in feeList)
                {
                    obj.ID = zmrzh1.PV1.VisitNumber.ID.Value; //住院流水号
                   // obj.User01 = DateTime.ParseExact(zmrzh1.PV1.GetDischargeDateTime(0).TimeOfAnEvent.Value,"yyyy-mm-dd hh:mi:ss",null); //出院日期
                    if (healthRecordFee.UpdateFeeInfo(obj) < 1)
                    {
                        errInfo = "更新费用失败" + healthRecordFee.Err;
                        return -1;
                    }
                }
            }
            #endregion

            #region 妇婴信息
            FS.HISFC.BizLogic.HealthRecord.Baby babyMgr = new FS.HISFC.BizLogic.HealthRecord.Baby();
            FS.HISFC.Models.HealthRecord.Baby  babyobj = new FS.HISFC.Models.HealthRecord.Baby();
            ArrayList alBaby = new ArrayList();
            for(int m =0;m<zmrzh1.ZORRepetitionsUsed;m++ )
            {
                babyobj.InpatientNo = zmrzh1.PV1.VisitNumber.ID.Value;
                babyobj.HappenNum = FS.FrameWork.Function.NConvert.ToInt32(zmrzh1.GetZOR().NewbornCode.Identifier.Value);//新生儿代码 
                //性别
                string BabysexID = zmrzh1.PID.AdministrativeSex.Value;
                if (Enum.GetNames(typeof(FS.HISFC.Models.Base.EnumSex)).Contains(BabysexID))
                {
                    babyobj.SexCode = Enum.Parse(typeof(FS.HISFC.Models.Base.EnumSex), BabysexID).ToString();
                }
                else
                {
                    babyobj.SexCode = FS.HISFC.Models.Base.EnumSex.M.ToString();
                }
                babyobj.BirthEnd = zmrzh1.GetZOR().StillbornIndicator.Value;
                if (string.IsNullOrEmpty(zmrzh1.GetZOR().NewbornWeight.Value) == false)
                {
                  babyobj.Weight = float.Parse(zmrzh1.GetZOR().NewbornWeight.Value);
                }
                babyobj.BabyState = zmrzh1.GetZOR().DischargeDisposition.Value;
                babyobj.Breath = zmrzh1.GetZOR().Breath.Value;//呼吸
                babyobj.SalvNum = FS.FrameWork.Function.NConvert.ToInt32(zmrzh1.GetZOR().CriticalNooftimes.Value);//抢救次数
                babyobj.SuccNum = FS.FrameWork.Function.NConvert.ToInt32( zmrzh1.GetZOR().Rescuesuccessnumber.Value);//抢救成功次数
                babyobj.OperInfo.ID = "9999";
                babyobj.OperInfo.Name = "";
                alBaby.Add(babyobj);

            }

            foreach (FS.HISFC.Models.HealthRecord.Baby obj in alBaby)
            {
                babyMgr.Delete(obj);
              
            }

            foreach (FS.HISFC.Models.HealthRecord.Baby baby in alBaby)
            {

                if (babyMgr.Insert(baby) < 1)
                {
                    errInfo = "保存妇婴信息失败" + baseMgr.Err;
                    return -1;
                }
            }
            #endregion

            #region 肿瘤信息

            FS.HISFC.BizLogic.HealthRecord.Tumour tumour = new FS.HISFC.BizLogic.HealthRecord.Tumour();
            FS.HISFC.Models.HealthRecord.Tumour TumInfo = new FS.HISFC.Models.HealthRecord.Tumour();
            TumInfo.InpatientNo = zmrzh1.PV1.VisitNumber.ID.Value; 

            //肿瘤分期类型
            TumInfo.Tumour_Type = zmrzh1.GetZCR().Tumourstagetype.Value;
            //TumInfo.Tumour_T = zmrzh1.GetZCR().
            TumInfo.Tumour_Stage = zmrzh1.GetZCR().Tumourstage.Value;
            TumInfo.Rmodeid = zmrzh1.GetZCR().Radiationway.Value;//放疗方式
            TumInfo.Rprocessid = zmrzh1.GetZCR().Radiationtherapyprograms.Value;//放疗程式
            TumInfo.Rdeviceid = zmrzh1.GetZCR().Radiationdevice.Value;//放疗装置
            TumInfo.Gy1 = FS.FrameWork.Function.NConvert.ToInt32(zmrzh1.GetZCR().Originaltumordose.Value);//原发灶计量
           // TumInfo.Time1 =zmrzh1.GetZCR().Originaltumordose
            if (string.IsNullOrEmpty(zmrzh1.GetZCR().Originaltumorstarttime.Value) == false)
            {
                TumInfo.BeginDate1 = DateTime.ParseExact(zmrzh1.GetZCR().Originaltumorstarttime.Value, "yyyyMMdd", null);
                TumInfo.EndDate1 = DateTime.ParseExact(zmrzh1.GetZCR().Originaltumorstoptime.Value, "yyyyMMdd", null);
            }
            TumInfo.Gy2 = FS.FrameWork.Function.NConvert.ToInt32(zmrzh1.GetZCR().regionallymphnodedose.Value);
            if (string.IsNullOrEmpty(zmrzh1.GetZCR().regionallymphnodestarttime.Value) == false)
            {
                TumInfo.BeginDate2 = DateTime.ParseExact(zmrzh1.GetZCR().regionallymphnodestarttime.Value, "yyyyMMdd", null);
                TumInfo.EndDate2 = DateTime.ParseExact(zmrzh1.GetZCR().regionallymphnodestoptime.Value, "yyyyMMdd", null);
            }
            TumInfo.Gy3 = FS.FrameWork.Function.NConvert.ToInt32(zmrzh1.GetZCR().Metastasisdose.Value);////转移灶计量
            if( string.IsNullOrEmpty(zmrzh1.GetZCR().Metastasisstarttime.Value) == false)
            {
                TumInfo.BeginDate3 = DateTime.ParseExact(zmrzh1.GetZCR().Metastasisstarttime.Value, "yyyyMMdd", null);
                TumInfo.EndDate3 = DateTime.ParseExact(zmrzh1.GetZCR().Metastasisstoptime.Value, "yyyyMMdd", null);
            }
            TumInfo.Cmodeid = zmrzh1.GetZCR().Chemotherapyway.Value;//化疗方式
            TumInfo.Cmethod = zmrzh1.GetZCR().Chemotherapymethod.Value;//化疗方法
            //药物治疗方法

            if (tumour.UpdateTumour(TumInfo) < 1)
            {
                if (tumour.InsertTumour(TumInfo) < 1)
                {
                    errInfo = "保存妇婴信息失败" + tumour.Err;
                    return -3;
                }
            }

            #endregion


            return 1;







        }
    }
}
