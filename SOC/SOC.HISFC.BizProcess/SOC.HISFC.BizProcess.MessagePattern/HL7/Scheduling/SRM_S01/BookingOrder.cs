using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Scheduling.SRM_S01
{
    /// <summary>
    /// 预约订单
    /// </summary>
    public class BookingOrder
    {
        public int processMessage(NHapi.Model.V24.Message.SRM_S01 o, ref NHapi.Model.V24.Message.SRR_S01 ackMessage, ref string errInfo)
        {
            string systemCode = o.MSH.SendingApplication.NamespaceID.Value;
            #region SRR_S01-返回信息

            if (ackMessage == null)
            {
                ackMessage = new NHapi.Model.V24.Message.SRR_S01();
            }

            #endregion

            FS.HISFC.Models.Base.Employee e = new FS.HISFC.Models.Base.Employee();
            e.ID = "T00001";
            e.Name = "自助终端";
            e.UserCode = "00";
            FS.FrameWork.Management.Connection.Operator = e;

            FS.HISFC.BizLogic.Registration.Schema schemaMgr = new FS.HISFC.BizLogic.Registration.Schema();
            FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
            FS.HISFC.BizLogic.Registration.Booking bookingMgr = new FS.HISFC.BizLogic.Registration.Booking();
            FS.SOC.HISFC.RADT.BizLogic.ComPatient comPatientMgr = new FS.SOC.HISFC.RADT.BizLogic.ComPatient();
            FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
            ItemCodeMapManager mapMgr = new ItemCodeMapManager();
            schemaMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            bookingMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            comPatientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            accountMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            mapMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //
            FS.HISFC.Models.Registration.Booking booking = new FS.HISFC.Models.Registration.Booking();

            //ARQ
            if (string.IsNullOrEmpty(o.ARQ.PlacerAppointmentID.EntityIdentifier.Value))
            {
                errInfo = "处理预约数据失败，预约流水号为空";
                return -1;
            }

            booking.ID = o.ARQ.FillerAppointmentID.EntityIdentifier.Value;//预约号
            if (string.IsNullOrEmpty(booking.ID))
            {
                errInfo = "处理预约数据失败，订单号为空，" + o.ARQ.PlacerAppointmentID.EntityIdentifier.Value;
                return -1;
            }


            //预约挂号对应的患者ID，我们存储到FIN_OPB_ACCOUNTCARD表中
            booking.Card.CardType.ID = "BookingCardNo";
            booking.Card.ID = booking.ID;

            if (string.IsNullOrEmpty(o.GetRESOURCES().GetPERSONNEL_RESOURCE().ZAI.TO_DATE.TimeOfAnEvent.Value) == false)
            {
                booking.DoctorInfo.SeeDate = DateTime.ParseExact(o.GetRESOURCES().GetPERSONNEL_RESOURCE().ZAI.TO_DATE.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);//出诊时间
                booking.Oper.OperTime = DateTime.ParseExact(o.GetRESOURCES().GetPERSONNEL_RESOURCE().ZAI.TO_DATE.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
            }
            booking.DoctorInfo.Templet.Noon.ID = o.GetRESOURCES().GetPERSONNEL_RESOURCE().ZAI.TIME_TYPE_DESC.Value;//午别
            booking.Name = o.GetPATIENT().PID.GetPatientName(0).FamilyName.Surname.Value;

            //卡号和卡类型
            for (int i = 0; i < o.GetPATIENT(0).PID.PatientIdentifierListRepetitionsUsed; i++)
            {
                if (o.GetPATIENT(0).PID.GetPatientIdentifierList(i).IdentifierTypeCode.Value.Equals("IdentifyNO"))
                {
                    booking.IDCardType.ID = "IdentifyNO";
                    booking.IDCard = o.GetPATIENT(0).PID.GetPatientIdentifierList(i).ID.Value;
                    string errText = "";
                    if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(booking.IDCard, ref errText) >= 0)
                    {
                        string[] reurnString = errText.Split(',');
                        if (reurnString.Length > 2)
                        {
                            booking.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(reurnString[1]);
                            booking.Sex.ID = reurnString[2].Equals("男") ? FS.HISFC.Models.Base.EnumSex.M : FS.HISFC.Models.Base.EnumSex.F;
                        }
                    }
                }
                else if (o.GetPATIENT(0).PID.GetPatientIdentifierList(i).IdentifierTypeCode.Value.Equals("SocialSecurityCardEncryption"))
                {
                    booking.SSN = o.GetPATIENT(0).PID.GetPatientIdentifierList(i).ID.Value;//社保卡加密卡号
                }
            }

            //性别
            string sexID = o.GetPATIENT(0).PID.AdministrativeSex.Value;
            if (Enum.GetNames(typeof(FS.HISFC.Models.Base.EnumSex)).Contains(sexID))
            {
                booking.Sex.ID = Enum.Parse(typeof(FS.HISFC.Models.Base.EnumSex), sexID);
            }
            else
            {
                booking.Sex.ID = FS.HISFC.Models.Base.EnumSex.M;
            }
            //出生日期
            if (string.IsNullOrEmpty(o.GetPATIENT(0).PID.DateTimeOfBirth.TimeOfAnEvent.Value) == false)
            {
                booking.Birthday = DateTime.ParseExact(o.GetPATIENT(0).PID.DateTimeOfBirth.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
            }
            booking.PhoneHome = o.GetPATIENT().PID.GetPhoneNumberHome(0).Get9999999X99999CAnyText.Value; //家庭电话
            booking.AddressHome = o.GetPATIENT().PID.GetPhoneNumberBusiness(0).Get9999999X99999CAnyText.Value;//工作单位电话号码

            //科室
            booking.DoctorInfo.Templet.Dept.ID = o.GetRESOURCES().GetLOCATION_RESOURCE().AIL.LocationResourceID.PointOfCare.Value;
            booking.DoctorInfo.Templet.Dept.Name = CommonController.CreateInstance().GetDepartmentName(booking.DoctorInfo.Templet.Dept.ID);
            if (string.IsNullOrEmpty(booking.DoctorInfo.Templet.Dept.Name))
            {
                errInfo = "处理预约数据失败，原因：没有找到相应的预约科室ID" + booking.DoctorInfo.Templet.Dept.ID;
                return -1;
            }

            //预约时间段
            if (string.IsNullOrEmpty(o.GetRESOURCES().GetPERSONNEL_RESOURCE().ZAI.BEGIN_TIME.Value) == false)
            {
                booking.DoctorInfo.Templet.Begin = DateTime.ParseExact(o.GetRESOURCES().GetPERSONNEL_RESOURCE().ZAI.BEGIN_TIME.Value, "HHmm", null);
                booking.DoctorInfo.Templet.Begin = new DateTime(booking.DoctorInfo.SeeDate.Year, booking.DoctorInfo.SeeDate.Month, booking.DoctorInfo.SeeDate.Day, booking.DoctorInfo.Templet.Begin.Hour, booking.DoctorInfo.Templet.Begin.Minute, 0);
            }
            else
            {
                errInfo = "处理预约数据失败，原因：开始时间为空";
                return -1;
            }
            if (string.IsNullOrEmpty(o.GetRESOURCES().GetPERSONNEL_RESOURCE().ZAI.END_TIME.Value) == false)
            {
                booking.DoctorInfo.Templet.End = DateTime.ParseExact(o.GetRESOURCES().GetPERSONNEL_RESOURCE().ZAI.END_TIME.Value, "HHmm", null);
                booking.DoctorInfo.Templet.End = new DateTime(booking.DoctorInfo.SeeDate.Year, booking.DoctorInfo.SeeDate.Month, booking.DoctorInfo.SeeDate.Day, booking.DoctorInfo.Templet.End.Hour, booking.DoctorInfo.Templet.End.Minute, 0);
            }
            else
            {
                errInfo = "处理预约数据失败，原因：结束时间为空";
                return -1;
            }

            //医生
            booking.DoctorInfo.Templet.Doct.ID = o.GetRESOURCES().GetPERSONNEL_RESOURCE().AIP.GetPersonnelResourceID(0).IDNumber.Value;//医生编码
            if (!string.IsNullOrEmpty(booking.DoctorInfo.Templet.Doct.ID))
            {
                booking.DoctorInfo.Templet.Doct.Name = o.GetRESOURCES().GetPERSONNEL_RESOURCE().AIP.GetPersonnelResourceID(0).FamilyName.Surname.Value;
                FS.HISFC.Models.Base.Employee doct = CommonInterface.CommonController.CreateInstance().GetEmployee(booking.DoctorInfo.Templet.Doct.ID);
                if (doct == null || string.IsNullOrEmpty(doct.ID))
                {
                    //没有医生默认为科室预约
                    booking.DoctorInfo.Templet.Doct.ID = string.Empty;
                    booking.DoctorInfo.Templet.Doct.Name = string.Empty;
                }
            }

            //排班号
            booking.DoctorInfo.Templet.ID = o.ARQ.ScheduleID.Identifier.Value;
            if (string.IsNullOrEmpty(booking.DoctorInfo.Templet.ID))
            {
                errInfo = "处理预约数据失败，原因：排班ID为空" + o.ARQ.PlacerAppointmentID.EntityIdentifier.Value;
                return -1;
            }

            //判断排班号是否存在并且医生或科室是否正确
            FS.HISFC.Models.Registration.Schema schemaInfo = schemaMgr.GetByID(booking.DoctorInfo.Templet.ID);
            if (schemaInfo == null || string.IsNullOrEmpty(schemaInfo.Templet.ID))
            {
                errInfo = "处理预约数据失败，原因：" + schemaMgr.Err + booking.DoctorInfo.Templet.ID;
                return -1;
            }

            //如果是专家排班，则判断医生和科室是否正确
            if (booking.DoctorInfo.Templet.Dept.ID.Equals(schemaInfo.Templet.Dept.ID) == false)
            {
                errInfo = "处理预约数据失败，原因：排班信息的科室与预约的科室不符合！" + schemaInfo.Templet.ID;
                return -1;
            }

            //如果是专家排班，则判断医生和科室是否正确
            if (string.IsNullOrEmpty(booking.DoctorInfo.Templet.Doct.ID) == false
                &&
                booking.DoctorInfo.Templet.Doct.ID.Equals(schemaInfo.Templet.Dept.ID) == false
                )
            {
                errInfo = "处理预约数据失败，原因：排班信息的医生与预约的医生不符合！" + schemaInfo.Templet.ID;
                return -1;
            }


            booking.IsSee = false;

            booking.Oper.ID = o.ARQ.GetEnteredByPerson(0).IDNumber.Value;//录入
            booking.Oper.Name = o.ARQ.GetEnteredByPerson(0).FamilyName.Surname.Value;//录入姓名

            //挂号级别
            booking.DoctorInfo.Templet.RegLevel.ID = o.GetRESOURCES().GetPERSONNEL_RESOURCE().AIP.ResourceRole.Identifier.Value;
            booking.DoctorInfo.Templet.RegLevel.Name = o.GetRESOURCES().GetPERSONNEL_RESOURCE().AIP.ResourceRole.Text.Value;

            #region 必须先开卡

            //取预约网站对应的患者CardNO
            bool isNewCardNO = false;
            //FS.FrameWork.Models.NeuObject obj = mapMgr.GetHISCode(EnumItemCodeMap.RegisterBookingCardNO, new FS.FrameWork.Models.NeuObject(o.GetPATIENT(0).PID.PatientID.ID.Value, o.GetPATIENT(0).PID.PatientID.ID.Value, ""),systemCode);

            //if (obj == null || string.IsNullOrEmpty(obj.ID))
            {
                //查询不到，只能在根据姓名、性别、身份证号在查询一遍
                ArrayList al= comPatientMgr.QueryComPatientByIdenInfoAndName(booking.IDCardType.ID, booking.IDCard, booking.Name);
                if (al != null && al.Count > 0)
                {
                    if (al.Count == 1)
                    {
                        isNewCardNO = false;
                        booking.PID.CardNO = (al[0] as FS.HISFC.Models.RADT.Patient).PID.CardNO;
                    }
                    else
                    {
                        isNewCardNO = false;
                        //性别，电话号码等匹配
                        foreach (FS.HISFC.Models.RADT.Patient patient in al)
                        {
                            if (patient.Sex.ID.ToString().Equals(booking.Sex.ID.ToString()) &&
                                patient.PhoneHome.Equals(booking.PhoneHome))
                            {
                                booking.PID.CardNO = patient.PID.CardNO;
                                break;
                            }
                        }

                        //如果没有找到，则取第一个
                        if (string.IsNullOrEmpty(booking.PID.CardNO))
                        {
                            booking.PID.CardNO = (al[0] as FS.HISFC.Models.RADT.Patient).PID.CardNO;
                        }
                    }
                }
                else
                {
                    isNewCardNO = true;//没有找到患者信息
                }
            }
            //else
            //{
            //    isNewCardNO = false;
            //    booking.PID.CardNO = obj.ID;
            //}

            if (isNewCardNO)
            {
                string cardNO = regMgr.AutoGetCardNO().ToString();
                if (string.IsNullOrEmpty(cardNO) || cardNO.Equals("-1"))
                {
                    errInfo = "获取病历号错误" + regMgr.Err;
                    return -1;
                }
                cardNO = cardNO.PadLeft(10, '0');
                booking.PID.CardNO = cardNO;

                ////插入对照信息
                //if (mapMgr.Insert(EnumItemCodeMap.RegisterBookingCardNO, new FS.FrameWork.Models.NeuObject(booking.PID.CardNO, booking.PID.CardNO, ""), new FS.FrameWork.Models.NeuObject(o.GetPATIENT(0).PID.PatientID.ID.Value, o.GetPATIENT(0).PID.PatientID.ID.Value, ""),systemCode) < 0)
                //{
                //    errInfo = "处理预约数据失败，原因：插入HL7对照表失败，" + mapMgr.Err + "订单编号：" + booking.ID + "预约患者ID：" + o.GetPATIENT(0).PID.PatientID.ID.Value;
                //    return -1;
                //}
            }
            else
            {
                #region 取患者信息
                FS.HISFC.Models.RADT.Patient patient = comPatientMgr.QueryComPatient(booking.PID.CardNO);
                if (patient != null)
                {
                    booking.Kin = patient.Kin;
                    if (booking.Birthday < DateTime.MinValue)
                    {
                        booking.Birthday = patient.Birthday;
                    }
                    booking.Pact.ID = patient.Pact.ID;
                    booking.Pact.PayKind.ID = patient.Pact.PayKind.ID;
                    booking.SSN = patient.SSN;
                    if (string.IsNullOrEmpty(booking.PhoneHome))
                    {
                        booking.PhoneHome = patient.PhoneHome;
                    }
                    booking.AddressHome = patient.AddressHome;
                    if (string.IsNullOrEmpty(booking.IDCard))
                    {
                        booking.IDCard = patient.IDCard;
                    }
                }
                #endregion
            }

            #region 开卡
            //开卡
            accountMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            comPatientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region 建卡

            //删除以前的预约订单号
            accountMgr.DeleteAccoutCard(booking.Card.ID, booking.Card.CardType.ID);

            FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            accountCard.MarkNO = booking.Card.ID;
            accountCard.MarkType = booking.Card.CardType;
            accountCard.MarkStatus = FS.HISFC.Models.Account.MarkOperateTypes.Begin;
            accountCard.ReFlag = "0";
            accountCard.CreateOper.ID = "T00001";
            accountCard.CreateOper.OperTime = CommonController.CreateInstance().GetSystemTime();
            accountCard.Patient.PID.CardNO = booking.PID.CardNO;
            accountCard.Patient.Name = booking.Name;
            accountCard.Patient.Sex.ID = booking.Sex.ID;
            accountCard.Patient.Birthday = booking.Birthday;
            accountCard.Patient.PhoneHome = booking.PhoneHome;
            accountCard.Patient.IDCard = booking.IDCard;
            accountCard.Patient.IDCardType.ID = booking.IDCardType.ID;

            if (accountMgr.InsertAccountCard(accountCard) == -1)
            {
                errInfo = "建卡失败，原因：" + accountMgr.Err;
                return -1;
            }
            #endregion

            #region 建卡记录

            FS.HISFC.Models.Account.AccountCardRecord accountCardRecord = new FS.HISFC.Models.Account.AccountCardRecord();
            //插入卡的操作记录
            accountCardRecord.MarkNO = accountCard.MarkNO;
            accountCardRecord.MarkType.ID = accountCard.MarkType.ID;
            accountCardRecord.CardNO = accountCard.Patient.PID.CardNO;
            accountCardRecord.OperateTypes.ID = (int)FS.HISFC.Models.Account.MarkOperateTypes.Begin;
            accountCardRecord.Oper.ID = accountCard.CreateOper.ID;
            //是否收取卡成本费
            accountCardRecord.CardMoney = 0;

            if (accountMgr.InsertAccountCardRecord(accountCardRecord) == -1)
            {
                errInfo = "建卡记录保存失败，原因：" + accountMgr.Err;
                return -1;
            }
            #endregion

            #region 患者信息
            
            if (comPatientMgr.InsertPatient(booking) <= 0)
            {
                if (comPatientMgr.UpdatePatient(booking) <= 0)
                {
                    errInfo = "保存患者信息失败，原因：" + comPatientMgr.Err;
                    return -1;
                }
            }

            #endregion


            #endregion

            #endregion

            #region 预约挂号

            if (bookingMgr.Delete(booking.ID) < 0)
            {
                errInfo = "处理预约数据失败，原因：" + bookingMgr.Err;
                return -1;
            }

            if (bookingMgr.Insert(booking) == -1)
            {
                errInfo = "处理预约数据失败，原因：" + bookingMgr.Err;
                return -1;
            }

            if (comPatientMgr.UpdatePatient(booking) <= 0)
            {
                errInfo = "处理预约数据失败，原因：" + comPatientMgr.Err;
                return -1;
            }

            //删除对照表
            if (mapMgr.Delete(EnumItemCodeMap.RegisterBooking, new FS.FrameWork.Models.NeuObject(o.ARQ.PlacerAppointmentID.EntityIdentifier.Value, o.GetPATIENT(0).PID.PatientID.ID.Value, ""),systemCode) < 0)
            {

                errInfo = "处理预约数据失败，原因：删除HL7对照表失败，" + mapMgr.Err + "订单编号：" + booking.ID + "预约ID：" + o.ARQ.PlacerAppointmentID.EntityIdentifier.Value;
                return -1;
            }

            //插入对照表
            if (mapMgr.Insert(EnumItemCodeMap.RegisterBooking, new FS.FrameWork.Models.NeuObject(booking.ID, booking.ID, ""), new FS.FrameWork.Models.NeuObject(o.ARQ.PlacerAppointmentID.EntityIdentifier.Value, o.GetPATIENT(0).PID.PatientID.ID.Value, ""),systemCode) < 0)
            {

                errInfo = "处理预约数据失败，原因：插入HL7对照表失败，" + mapMgr.Err + "订单编号：" + booking.ID + "预约ID：" + o.ARQ.PlacerAppointmentID.EntityIdentifier.Value;
                return -1;
            }


            #endregion
            //保存基本信息

            ackMessage.SCHEDULE.GetPATIENT(0).PID.PatientID.ID.Value = booking.PID.CardNO;
            ackMessage.SCHEDULE.GetPATIENT(0).PV1.PatientClass.Value = "O";
            ackMessage.SCHEDULE.GetPATIENT(0).PV1.VisitNumber.ID.Value = booking.ID;
            ackMessage.SCHEDULE.GetPATIENT(0).PV1.AlternateVisitID.ID.Value = booking.ID;

            return 1;
        }

    }
}
