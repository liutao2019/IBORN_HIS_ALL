using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A04
{
    /// <summary>
    /// 门诊患者登记
    /// </summary>
    public class OutPatientRegister
    {
         /// <summary>
        /// 发送门诊患者登记信息
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int ProcessMessage(FS.HISFC.Models.Registration.Register register, ref NHapi.Model.V24.Message.ADT_A01 adtA01,ref string errInfo)
        {

            //增加护士站排队处理
            //找科室对应的护士站，如果没有就用科室
            FS.HISFC.BizLogic.Manager.DepartmentStatManager departStat = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            ArrayList alNurse= departStat.LoadByChildren("14", register.DoctorInfo.Templet.Dept.ID);
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

            string isChangeOrderRule = (new FS.FrameWork.Management.ControlParam()).QueryControlerInfo("LOC001");
            if (isChangeOrderRule == "1" && register.DoctorInfo.Templet.Dept.ID.Equals("3110"))
            {
                #region 规则变更
                int rtn = this.GetSeeNo(register, nurseID, out errInfo);
                if (rtn == -1)
                {
                    return -1;
                }
                register.DoctorInfo.SeeNO = rtn;
                #endregion
            }
            else
            {
                string seeNO = string.Empty;
                if (register.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)
                {
                    if (this.getNursePreSeeNO(register.RegLvlFee.RegLevel.IsExpert, "2", register.DoctorInfo.Templet.Dept.ID + "|" + register.DoctorInfo.Templet.Begin.ToString("HHmm"), ref seeNO, out errInfo) < 0)
                    {
                        return -1;
                    }
                }
                else
                {
                    if (new SOC.HISFC.Assign.BizProcess.Assign().GetNurseSeeNO(register.RegLvlFee.RegLevel.IsExpert, null, register.RegLvlFee.RegLevel.IsExpert ? register.DoctorInfo.Templet.Doct.ID : nurseID, ref seeNO, out errInfo) < 0)
                    {
                        return -1;
                    }
                }
                register.DoctorInfo.SeeNO = FS.FrameWork.Function.NConvert.ToInt32(seeNO);
            }
            
            if (register.DoctorInfo.Templet.Dept.ID.Equals("3110") && register.DoctorInfo.Templet.RegLevel.ID.Equals("5"))
            {

            }
            else
            {
                register.Memo = nurseID;
            }
            //更新挂号表
            if (this.updateRegisterSeeNO(register,out errInfo) < 0)
            {
                return -1;
            }

            #region 消息转换
            adtA01 = new NHapi.Model.V24.Message.ADT_A01();
            //MSH 消息头
            adtA01.MSH.MessageType.MessageType.Value = "ADT";
            adtA01.MSH.MessageType.TriggerEvent.Value = "A04";
            FS.HL7Message.V24.Function.GenerateMSH(adtA01.MSH);

            //EVN 消息事件
            adtA01.EVN.EventTypeCode.Value = "A04";
            FS.HL7Message.V24.Function.GenerateEVN(adtA01.EVN);

            //PID 患者基本信息
            adtA01.PID.SetIDPID.Value = "1";
            adtA01.PID.PatientID.ID.Value = register.PID.CardNO;

            NHapi.Model.V24.Datatype.CX patientList1 = adtA01.PID.GetPatientIdentifierList(0);

            //其他厂商确定用这个IDCard
            patientList1.IdentifierTypeCode.Value = "IDCard";//register.Card.CardType.ID;
            patientList1.ID.Value = register.PID.CardNO;

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
           
            //身份证
            NHapi.Model.V24.Datatype.CX patientList4 = adtA01.PID.GetPatientIdentifierList(adtA01.PID.PatientIdentifierListRepetitionsUsed);
            patientList4.IdentifierTypeCode.Value = "IdentifyNO";
            patientList4.ID.Value = register.IDCard;
       

            NHapi.Model.V24.Datatype.XPN patientName = adtA01.PID.GetPatientName(0);
            patientName.FamilyName.Surname.Value = register.Name;
            if (register.Birthday  > DateTime.MinValue)
            {
                adtA01.PID.DateTimeOfBirth.TimeOfAnEvent.Value = register.Birthday.ToString("yyyyMMddHHmmss");
            }
            adtA01.PID.AdministrativeSex.Value = register.Sex.ID.ToString();
            NHapi.Model.V24.Datatype.XAD homeAddress = adtA01.PID.GetPatientAddress(0);
            homeAddress.AddressType.Value = "H";
            homeAddress.StreetAddress.StreetOrMailingAddress.Value = register.AddressHome;
            homeAddress.ZipOrPostalCode.Value = register.HomeZip;

            NHapi.Model.V24.Datatype.XAD businessAddress = adtA01.PID.GetPatientAddress(1);
            businessAddress.AddressType.Value = "O";
            businessAddress.StreetAddress.StreetOrMailingAddress.Value = register.AddressBusiness;
            businessAddress.ZipOrPostalCode.Value = register.BusinessZip;

            NHapi.Model.V24.Datatype.XTN homePhone = adtA01.PID.GetPhoneNumberHome(0);
            homePhone.AnyText.Value = register.PhoneHome;
            NHapi.Model.V24.Datatype.XTN businessPhone = adtA01.PID.GetPhoneNumberBusiness(0);
            businessPhone.AnyText.Value = register.PhoneBusiness;
            adtA01.PID.MaritalStatus.Identifier.Value = register.MaritalStatus.ID.ToString();
            adtA01.PID.MaritalStatus.Text.Value = register.MaritalStatus.Name;
            adtA01.PID.SSNNumberPatient.Value = register.SSN;

            NHapi.Model.V24.Datatype.CX motherSID = adtA01.PID.GetMotherSIdentifier(0);
            motherSID.ID.Value = "";
            NHapi.Model.V24.Datatype.CE ethnicGroup = adtA01.PID.GetEthnicGroup(0);
            ethnicGroup.Identifier.Value = register.Nationality.ID;
            ethnicGroup.Text.Value = register.Nationality.Name;

            adtA01.PID.BirthPlace.Value = register.DIST;

            adtA01.PID.Nationality.Identifier.Value = register.Country.ID;
            adtA01.PID.Nationality.Text.Value = register.Country.Name;

            adtA01.PID.ProductionClassCode.Identifier.Value = register.Pact.ID;
            adtA01.PID.ProductionClassCode.Text.Value = register.Pact.Name;

            //NK1 患者联系人信息
            NHapi.Model.V24.Segment.NK1 NK1 = adtA01.GetNK1(0);
            NK1.SetIDNK1.Value = "1";
            NHapi.Model.V24.Datatype.XPN linkName = NK1.GetName(0);
            linkName.FamilyName.Surname.Value = register.Kin.Name;
            NK1.Relationship.Identifier.Value = register.Kin.Relation.ID;
            NK1.Relationship.Text.Value = register.Kin.Relation.Name;
            NHapi.Model.V24.Datatype.XAD linkAddress = NK1.GetAddress(0);
            linkAddress.StreetAddress.StreetOrMailingAddress.Value = register.Kin.RelationAddress;
            NHapi.Model.V24.Datatype.XTN linkPhone = NK1.GetPhoneNumber(0);
            linkPhone.AnyText.Value = register.Kin.RelationPhone;

            //PV1 患者就诊信息
            adtA01.PV1.SetIDPV1.Value = "1";
            adtA01.PV1.PatientClass.Value = "O";//类型
            adtA01.PV1.AssignedPatientLocation.Facility.NamespaceID.Value = register.DoctorInfo.Templet.Dept.ID;//挂号科室编码
            adtA01.PV1.AdmissionType.Value = register.DoctorInfo.Templet.RegLevel.ID;//挂号级别
            adtA01.PV1.ReAdmissionIndicator.Value = register.InTimes.ToString();//入院次数
            adtA01.PV1.VIPIndicator.Value = FS.FrameWork.Function.NConvert.ToInt32(register.VipFlag).ToString(); //VIP标识符
            NHapi.Model.V24.Datatype.XCN admittingDoctor = adtA01.PV1.GetAdmittingDoctor(0);
            admittingDoctor.IDNumber.Value = register.DoctorInfo.Templet.Doct.ID;
            admittingDoctor.FamilyName.Surname.Value = register.DoctorInfo.Templet.Doct.Name;
            adtA01.PV1.PatientType.Value = register.Pact.PayKind.ID;
            adtA01.PV1.VisitNumber.ID.Value = register.ID;
            adtA01.PV1.AdmitDateTime.TimeOfAnEvent.Value = register.DoctorInfo.SeeDate.ToString("yyyyMMddHHmmss");

            //return ProcessFactory.ProcessSender("ADT^A04", adtA01.MSH, adtA01);
            #endregion
            return 1;
        }

        private int updateRegisterSeeNO(FS.HISFC.Models.Registration.Register register,out string errorInfo)
        {
            string sql = "update fin_opr_register set SEENO={1},mark1='{2}'  where clinic_code='{0}'";
            FS.FrameWork.Management.DataBaseManger dgMgr = new FS.FrameWork.Management.DataBaseManger();
            int i = dgMgr.ExecNoQuery(sql,register.ID, register.DoctorInfo.SeeNO.ToString(),register.Memo);
            errorInfo = dgMgr.Err;
            return i;
        }

        /// <summary>
        /// 预约取号规则
        /// </summary>
        /// <param name="isExpert"></param>
        /// <param name="seeType"></param>
        /// <param name="subjectID"></param>
        /// <param name="seeNO"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private int getNursePreSeeNO(bool isExpert, string seeType, string subjectID, ref string seeNO, out string error)
        {
            error = "";
            if (string.IsNullOrEmpty(seeType))
            {
                if (isExpert)
                {
                    seeType = "1";//医生
                }
                else
                {
                    seeType = "3";//护士站
                }
            }
            FS.SOC.HISFC.Assign.BizLogic.Assign assignMgr = new FS.SOC.HISFC.Assign.BizLogic.Assign();
            //assignMgr.SetTrans(new FS.SOC.HISFC.BizProcess.CommonInterface()));
            //全天走一个号
            DateTime dtNow = assignMgr.GetDateTimeFromSysDateTime();
            string noonID = "A";// CommonController.CreateInstance().GetNoonID(dtNow);
            string preTime = string.Empty;
            string dept = string.Empty;
            string[] str = subjectID.Split('|');
            dept = str[0];
            if (str.Length > 1)
            {
                preTime = str[1];
            }
            //先判断是不是先来
            //int intNowTime = dtNow.Hour + dtNow.Minute;
            //if (intNowTime > NConvert.ToDecimal(preTime))//现在时间大于预约时间，说明已经过了预约时间点
            //{

            //}
            //else//如果提前过来的，或者正好那个时间过来的
            //{
            //    //先查询预约表，该预约时间段，有多少个预约
            //    //再查询挂号表该预约段有无号码已经挂号
            //}
            
            int i = assignMgr.UpdateSeeNO(seeType, dtNow, subjectID, noonID);
            if (i < 0)
            {
                error = "更新看诊序号失败，原因：" + assignMgr.Err;
                return -1;
            }
            else if (i == 0)
            {
                //取预约时间段人数号，查询该时段多少人预留号码
                int numPre = 0;
                if (assignMgr.GetSeeNoPre(dtNow, dept, preTime, ref numPre) <= 0)
                {
                    error = "获取预约时间段人数失败，原因：" + assignMgr.Err;
                    return -1;
                }

                if (assignMgr.InsertSeeNOPre(seeType, dtNow, subjectID, numPre.ToString(), noonID) <= 0)
                {
                    error = "插入看诊序号失败，原因：" + assignMgr.Err;
                    return -1;
                }
            }

            //取看诊序号
            int num = 0;
            if (assignMgr.GetSeeNO(seeType, dtNow, subjectID, noonID, ref num) <= 0)
            {
                error = "获取看诊序号失败，原因：" + assignMgr.Err;
                return -1;
            }

            //看诊类别+看诊序号
            seeNO = seeType + num.ToString().PadLeft(3, '0');

            return 1;
        }

        /// <summary>
        /// 门诊患者看诊排号规则
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="nurseID"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private int GetSeeNo(FS.HISFC.Models.Registration.Register regObj, string nurseID, out string error)
        {
            /* 2012/09/27 规则变更 By Huangd
             * 
             * 1.按预约时段开始时间顺序排号[30分钟为一段]
             *   类别 + 时段顺序号 + 顺序号(暂定2位)
             *   101001  8:00-8:30
             *   102001  8:30-9:00
             *   103001  9:00-9:30
             *   ……
             *   
             * 2.预约迟到取当前挂号时段在预约号基础上往后加号排队
             *   预约8:00-8:30未到
             *   9:00-9:30才到现场挂号             
             *   那么预约放号10个, 则该患者排号为13011或者更后面
             *   
             * 3.预约患者现场挂号往后推一个时间段排号
             *   若患者8:00-8:30来现场挂号
             *   则取8:30-9:00这段时间的号  
             *   
             * 4.上班时间以外的按原取号规则走
             *   
             * */

            try
            {
                string rtn = "-1";
                //if (regObj.RegType != FS.HISFC.Models.Base.EnumRegType.Pre)  //非预约号
                //{
                //    if (new SOC.HISFC.Assign.BizProcess.Assign().GetNurseSeeNO(regObj.RegLvlFee.RegLevel.IsExpert, null, regObj.RegLvlFee.RegLevel.IsExpert ? regObj.DoctorInfo.Templet.Doct.ID : nurseID, ref rtn, out error) < 0)
                //    {
                //        return -1;
                //    }
                //    return NConvert.ToInt32(rtn);
                //}
                //else
                //{
                    FS.SOC.HISFC.Assign.BizLogic.Assign assignMgr = new FS.SOC.HISFC.Assign.BizLogic.Assign();
                    DateTime dtNow = assignMgr.GetDateTimeFromSysDateTime();

                    //1.类别
                    string seeType = "2";
                    //if (regObj.RegType != FS.HISFC.Models.Base.EnumRegType.Pre)
                    //{
                    //    switch (regObj.RegLvlFee.RegLevel.IsExpert)
                    //    {
                    //        case true:
                    //            seeType = "1";  //医生站
                    //            break;
                    //        case false:
                    //            seeType = "3";  //护士站
                    //            break;
                    //        default:
                    //            seeType = "2";  //预约
                    //            break;
                    //    }
                    //}

                    //2.时段顺序号
                    bool isLate = false;  //是否迟到
                    string timeOrder = string.Empty;
                    DateTime dtBegin = dtNow;
                    DateTime dtEnd = dtNow;

                    if (regObj.DoctorInfo.SeeDate <= regObj.DoctorInfo.Templet.End && regObj.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)  //未迟到  按正常预约时间段取号
                    {
                        dtBegin = regObj.DoctorInfo.Templet.Begin;
                        dtEnd = regObj.DoctorInfo.Templet.End;
                        isLate = false;
                    }
                    else   //已迟到  在挂号时间基础上往前推一时间段取号
                    {
                        if (regObj.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)
                        {
                            if (regObj.DoctorInfo.SeeDate.Minute <= 30)  //上半段
                            {
                                dtBegin = new DateTime(regObj.DoctorInfo.SeeDate.Year, regObj.DoctorInfo.SeeDate.Month, regObj.DoctorInfo.SeeDate.Day, regObj.DoctorInfo.SeeDate.Hour, 0, 0).AddMinutes(30);
                                dtEnd = new DateTime(regObj.DoctorInfo.SeeDate.Year, regObj.DoctorInfo.SeeDate.Month, regObj.DoctorInfo.SeeDate.Day, regObj.DoctorInfo.SeeDate.Hour, 0, 0).AddMinutes(60);
                            }
                            else  //下半段
                            {
                                dtBegin = new DateTime(regObj.DoctorInfo.SeeDate.Year, regObj.DoctorInfo.SeeDate.Month, regObj.DoctorInfo.SeeDate.Day, regObj.DoctorInfo.SeeDate.Hour, 30, 0).AddMinutes(30);
                                dtEnd = new DateTime(regObj.DoctorInfo.SeeDate.Year, regObj.DoctorInfo.SeeDate.Month, regObj.DoctorInfo.SeeDate.Day, regObj.DoctorInfo.SeeDate.Hour, 30, 0).AddMinutes(60);
                            }
                        }
                        else
                        {
                            if (regObj.DoctorInfo.SeeDate.Minute <= 30)  //上半段
                            {
                                dtBegin = new DateTime(regObj.DoctorInfo.SeeDate.Year, regObj.DoctorInfo.SeeDate.Month, regObj.DoctorInfo.SeeDate.Day, regObj.DoctorInfo.SeeDate.Hour, 0, 0).AddMinutes(30);
                                dtEnd = new DateTime(regObj.DoctorInfo.SeeDate.Year, regObj.DoctorInfo.SeeDate.Month, regObj.DoctorInfo.SeeDate.Day, regObj.DoctorInfo.SeeDate.Hour, 0, 0).AddMinutes(60);
                            }
                            else  //下半段
                            {
                                dtBegin = new DateTime(regObj.DoctorInfo.SeeDate.Year, regObj.DoctorInfo.SeeDate.Month, regObj.DoctorInfo.SeeDate.Day, regObj.DoctorInfo.SeeDate.Hour, 30, 0).AddMinutes(30);
                                dtEnd = new DateTime(regObj.DoctorInfo.SeeDate.Year, regObj.DoctorInfo.SeeDate.Month, regObj.DoctorInfo.SeeDate.Day, regObj.DoctorInfo.SeeDate.Hour, 30, 0).AddMinutes(60);
                            }
                        
                        }
                        
                        isLate = true;
                    }

                    DateTime dtAmBeginDuty = dtNow;
                    DateTime dtAmEndDuty = dtNow;
                    DateTime dtPmBeginDuty = dtNow;
                    int restTime = 0;
                    ArrayList alDutyTime = (new FS.HISFC.BizLogic.Manager.Constant()).GetAllList("DUTYTIME"); //上班时间
                    foreach (FS.FrameWork.Models.NeuObject dutyTime in alDutyTime)   //预先获取上午上班结束时间 及 中午休息时间
                    {
                        string[] times = dutyTime.Name.Split('-');
                        if (dutyTime.ID == "RestTime")
                        {
                            restTime = NConvert.ToInt32(dutyTime.Name);
                        }
                        else if (dutyTime.ID == "1")
                        {
                            dtAmBeginDuty = NConvert.ToDateTime(dtNow.ToString("yyyy-MM-dd") + " " + times[0]);  
                            dtAmEndDuty = NConvert.ToDateTime(dtNow.ToString("yyyy-MM-dd") + " " + times[1]);  
                        }
                        else if (dutyTime.ID == "2")
                        {
                            dtPmBeginDuty = NConvert.ToDateTime(dtNow.ToString("yyyy-MM-dd") + " " + times[0]);
                        }
                    }

                    //判断时间段是否在中午休息时间段内
                    if (dtBegin >= dtAmEndDuty && dtEnd <= dtPmBeginDuty)
                    {
                        dtBegin = dtPmBeginDuty;
                        dtEnd = dtPmBeginDuty.AddMinutes(30);
                    }

                    foreach (FS.FrameWork.Models.NeuObject dutyTime in alDutyTime)
                    {
                        string[] times = dutyTime.Name.Split('-');
                        DateTime dtBeginTmp = NConvert.ToDateTime(dtNow.ToString("yyyy-MM-dd") + " " + times[0]);
                        DateTime dtEndTmp = NConvert.ToDateTime(dtNow.ToString("yyyy-MM-dd") + " " + times[1]);
                        if (dtBegin >= dtBeginTmp && dtEnd <= dtEndTmp)
                        {
                            System.TimeSpan ts = dtBegin.Subtract(dtAmBeginDuty);
                            int temp = 0;
                            if (dutyTime.ID == "1")  //上午
                                temp = NConvert.ToInt32((NConvert.ToInt32(ts.TotalMinutes)) / 30) + 1;
                            else if (dutyTime.ID == "2")  //下午
                                temp = NConvert.ToInt32((NConvert.ToInt32(ts.TotalMinutes) - restTime) / 30) + 1;

                            timeOrder = temp.ToString().PadLeft(2, '0');
                            break;
                        }
                    }
                    if (string.IsNullOrEmpty(timeOrder) || timeOrder == "00")
                    {
                        error = "获取看诊序号失败, 原因：可能是上班时间常数维护有误!";
                        return -1;
                    }


                    //3.顺序号
                    string deptCode = regObj.DoctorInfo.Templet.Dept.ID;
                    string beginTime = dtBegin.ToString("HHmm");
                    int bookNum = 0;
                    int seeNum = 0;
                    int orderNo = 0;
                    if (isLate)   //迟到取该段内预约号 +　排号
                    {
                        int i = assignMgr.UpdateSeeNO(seeType, dtBegin, deptCode + "|" + beginTime, "A"); //午别按全天走, 按时段开始时间分组取号
                        if (i < 0)
                        {
                            error = "更新看诊序号失败, 原因: " + assignMgr.Err;
                            return -1;
                        }
                        else if (i == 0)
                        {
                            //取预约时间段人数号及已挂人数  
                            if (assignMgr.GetBookNums(deptCode, dtBegin, ref bookNum, ref seeNum) <= 0)
                            {
                                error = "获取预约时间段人数失败, 原因: " + assignMgr.Err;
                                return -1;
                            }
                            int bookNumTmp = bookNum + 1;
                            if (assignMgr.InsertSeeNOPre(seeType, dtBegin, deptCode + "|" + beginTime, bookNumTmp.ToString(), "A") <= 0)
                            {
                                error = "插入看诊序号失败, 原因: " + assignMgr.Err;
                                return -1;
                            }
                            orderNo = bookNumTmp;
                        }
                        else
                        {
                            //取看诊序号
                            if (assignMgr.GetSeeNO(seeType, dtBegin, deptCode + "|" + beginTime, "A", ref orderNo) <= 0)
                            {
                                error = "获取看诊序号失败, 原因: " + assignMgr.Err;
                                return -1;
                            }
                        }
                    }
                    else   //正常取该段内预约排号
                    {
                        //取预约时间段人数号及已挂人数            
                        if (assignMgr.GetBookNums(deptCode,dtBegin, ref bookNum, ref seeNum) <= 0)
                        {
                            error = "获取预约时间段人数失败, 原因: " + assignMgr.Err;
                            return -1;
                        }
                        orderNo = seeNum;  //当前患者SEE_FLAG已被更新, 故不需再加 1
                    }

                    error = "取号成功!";
                    rtn = seeType + timeOrder + orderNo.ToString().PadLeft(3, '0');    
                   // rtn =  timeOrder + orderNo.ToString().PadLeft(3, '0');                    
                    return NConvert.ToInt32(rtn);
                }
            //}
            catch (Exception ex)
            {
                error = "获取看诊序号失败，原因：" + ex.Message;
                return -1;
            }

        }
    }
}
