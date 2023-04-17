using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.HISFC.Models.Account;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Scheduling.SRM_S01
{
    class PreRegister
    {
        private int processMessage(NHapi.Model.V24.Message.SRM_S01 o, ref NHapi.Model.V24.Message.SRR_S01 ackMessage,ref string errInfo)
        {

            FS.HISFC.Models.Base.Employee e = new FS.HISFC.Models.Base.Employee();
            e.ID = "T00001";
            e.Name = "自助终端机";
            e.UserCode = "99";
            FS.FrameWork.Management.Connection.Operator = e;

            FS.HISFC.BizLogic.Registration.Schema schemaMgr = new FS.HISFC.BizLogic.Registration.Schema();
            FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
            FS.SOC.HISFC.RADT.BizLogic.ComPatient comPatientMgr = new FS.SOC.HISFC.RADT.BizLogic.ComPatient();
            FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
            FS.HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();
            FS.HISFC.BizLogic.Registration.RegLvlFee regLevelFeeMgr = new FS.HISFC.BizLogic.Registration.RegLvlFee();

            #region PID-挂号患者信息

            //姓名
            register.Name = o.GetPATIENT(0).PID.GetPatientName(0).FamilyName.Surname.Value;

            //卡号和卡类型
            for (int i = 0; i < o.GetPATIENT(0).PID.PatientIdentifierListRepetitionsUsed; i++)
            {
                if (o.GetPATIENT(0).PID.GetPatientIdentifierList(i).IdentifierTypeCode.Value.Equals("IdentifyNO"))
                {
                    register.IDCardType.ID = "IdentifyNO";
                    register.IDCard = o.GetPATIENT(0).PID.GetPatientIdentifierList(i).ID.Value;
                    string errText = "";
                    if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(register.IDCard, ref errText) >= 0)
                    {
                        string[] reurnString = errText.Split(',');
                        register.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(reurnString[1]);
                        register.Sex.ID = reurnString[2].Equals("男") ? FS.HISFC.Models.Base.EnumSex.M : FS.HISFC.Models.Base.EnumSex.F;
                    }
                }
                else if (o.GetPATIENT(0).PID.GetPatientIdentifierList(i).IdentifierTypeCode.Value.Equals("SocialSecurityCardEncryption"))
                {
                    register.Memo = o.GetPATIENT(0).PID.GetPatientIdentifierList(i).ID.Value;//社保卡加密卡号
                }
                else
                {
                    register.Card.CardType.ID = o.GetPATIENT(0).PID.GetPatientIdentifierList(i).IdentifierTypeCode.Value;//卡类型
                    register.Card.ID = o.GetPATIENT(0).PID.GetPatientIdentifierList(i).ID.Value;//卡号
                }
            }
            //性别
            string sexID = o.GetPATIENT(0).PID.AdministrativeSex.Value;
            if (Enum.GetNames(typeof(FS.HISFC.Models.Base.EnumSex)).Contains(sexID))
            {
                register.Sex.ID = Enum.Parse(typeof(FS.HISFC.Models.Base.EnumSex), sexID);
            }
            else
            {
                register.Sex.ID = FS.HISFC.Models.Base.EnumSex.M;
            }
            //出生日期
            if (string.IsNullOrEmpty(o.GetPATIENT(0).PID.DateTimeOfBirth.TimeOfAnEvent.Value) == false)
            {
                register.Birthday = DateTime.ParseExact(o.GetPATIENT(0).PID.DateTimeOfBirth.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
            }

            //家庭电话
            register.PhoneHome = o.GetPATIENT(0).PID.GetPhoneNumberHome(0).AnyText.Value;
            #endregion

            #region 必须先开卡

            string cardNO = "";
            if (accountMgr.GetCardNoByMarkNo(register.Card.ID, register.Card.CardType, ref cardNO) == false || string.IsNullOrEmpty(cardNO))
            {
                //自动生成
                cardNO = regMgr.AutoGetCardNO().ToString();
                if (string.IsNullOrEmpty(cardNO) || cardNO.Equals("-1"))
                {
                    errInfo = "获取病历号错误" + regMgr.Err;
                    return -1;
                }
                cardNO = cardNO.PadLeft(10, '0');
                register.PID.CardNO = cardNO;

                #region 开卡
                accountMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                comPatientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                #region 建卡

                FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                accountCard.MarkNO = register.Card.ID;
                accountCard.MarkType = register.Card.CardType;
                accountCard.MarkStatus = MarkOperateTypes.Begin;
                accountCard.ReFlag = "0";
                accountCard.CreateOper.ID = "T00001";
                accountCard.CreateOper.OperTime = CommonController.CreateInstance().GetSystemTime();
                accountCard.Patient.PID.CardNO = register.PID.CardNO;
                accountCard.Patient.Name = register.Name;
                accountCard.Patient.Sex.ID = register.Sex.ID;
                accountCard.Patient.Birthday = register.Birthday;
                accountCard.Patient.PhoneHome = register.PhoneHome;
                accountCard.Patient.IDCard = register.IDCard;
                accountCard.Patient.IDCardType.ID = register.IDCardType.ID;

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
                if (comPatientMgr.InsertPatient(register) <= 0)
                {
                    errInfo = "保存患者信息失败，原因：" + comPatientMgr.Err;
                    return -1;
                }

                #endregion

                #endregion
            }
            else
            {
                //根据卡号和卡类型查找对应的病历号
                register.PID.CardNO = cardNO;

                #region 取患者信息
                FS.HISFC.Models.RADT.Patient patient = comPatientMgr.QueryComPatient(cardNO);
                if (patient == null)
                {
                    errInfo = "卡号：" + register.Card.ID + "，卡类型：" + register.Card.CardType.ID + "，不能查找到相应的患者信息，请到人工窗口开卡" + comPatientMgr.Err;
                    return -1;
                }

                register.Kin = patient.Kin;
                register.Birthday = patient.Birthday;
                register.Pact.ID = patient.Pact.ID;
                register.Pact.PayKind.ID = patient.Pact.PayKind.ID;
                register.SSN = patient.SSN;
                if (string.IsNullOrEmpty(register.PhoneHome))
                {
                    register.PhoneHome = patient.PhoneHome;
                }
                register.AddressHome = patient.AddressHome;
                register.IDCard = patient.IDCard;
                register.NormalName = patient.NormalName;
                register.IsEncrypt = patient.IsEncrypt;
                register.IDCard = patient.IDCard;

                #endregion
            }

            #endregion

            #region AIL-挂号科室

            //科室编码
            register.DoctorInfo.Templet.Dept.ID = o.GetRESOURCES(0).GetLOCATION_RESOURCE(0).AIL.LocationResourceID.PointOfCare.Value;
            if (string.IsNullOrEmpty(register.DoctorInfo.Templet.Dept.ID))
            {
                errInfo = "科室编码为空，SRM_S01-AIL-3.1";
                return -1;
            }

            if (CommonController.CreateInstance().GetDepartment(register.DoctorInfo.Templet.Dept.ID) == null)
            {
                errInfo = "传入的科室编码无法在系统中查找到，请确认是否正确，SRM_S01-AIL-3.1=" + register.DoctorInfo.Templet.Dept.ID;
                return -1;
            }

            register.DoctorInfo.Templet.Dept.Name = CommonController.CreateInstance().GetDepartmentName(register.DoctorInfo.Templet.Dept.ID);

            #endregion

            #region AIP-挂号医生或级别

            //医生编码
            register.DoctorInfo.Templet.Doct.ID = o.GetRESOURCES(0).GetPERSONNEL_RESOURCE(0).AIP.GetPersonnelResourceID(0).IDNumber.Value;

            //挂号级别
            register.DoctorInfo.Templet.RegLevel.ID = o.GetRESOURCES(0).GetPERSONNEL_RESOURCE(0).AIP.ResourceRole.Identifier.Value;

            if (string.IsNullOrEmpty(register.DoctorInfo.Templet.RegLevel.ID) && string.IsNullOrEmpty(register.DoctorInfo.Templet.Doct.ID))
            {
                errInfo = "挂号级别和医生必须有一个不为空，SRM_S01-AIP-3和AIP-4";
                return -1;
            }

            //如果是医生不为空，则查询挂号级别
            if (string.IsNullOrEmpty(register.DoctorInfo.Templet.Doct.ID) == false)
            {
                if (CommonController.CreateInstance().GetEmployee(register.DoctorInfo.Templet.Doct.ID) == null)
                {
                    errInfo = "传入的医生编码无法在系统中查找到，请确认是否正确，SRM_S01-AIP-3=" + register.DoctorInfo.Templet.Doct.ID;
                    return -1;
                }
                register.DoctorInfo.Templet.Doct.Name = CommonController.CreateInstance().GetEmployeeName(register.DoctorInfo.Templet.Doct.ID);

                //查找挂号级别

                FS.HISFC.Models.Registration.Schema doctSchema = schemaMgr.GetSchema(register.DoctorInfo.Templet.Doct.ID, CommonController.CreateInstance().GetSystemTime().Date);
                if (doctSchema == null)
                {
                    errInfo = "当前医生无法在系统中查找到对应的排班记录，原因：" + schemaMgr.Err;
                    return -1;
                }

                register.DoctorInfo.Templet.ID = doctSchema.Templet.ID;
                register.DoctorInfo.Templet.RegLevel.ID = doctSchema.Templet.RegLevel.ID;
            }

            if (FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetRegLevel(register.DoctorInfo.Templet.RegLevel.ID) == null)
            {
                errInfo = "传入的挂号级别无法在系统中查找到，请确认是否正确，SRM_S01-AIP-4=" + register.DoctorInfo.Templet.RegLevel.ID;
                return -1;
            }
            register.DoctorInfo.Templet.RegLevel = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetRegLevel(register.DoctorInfo.Templet.RegLevel.ID);

            //查找挂号费，合同单位默认为现金
            if (string.IsNullOrEmpty(register.Memo))
            {
                register.Pact.ID = "1";
                register.Pact.Name = "现金";
            }
            else
            {
                register.Pact.ID = "2";
                register.Pact.Name = "医保";
            }

            register.Pact.PayKind = CommonController.CreateInstance().GetPayKind(register.Pact.ID);

            FS.HISFC.Models.Registration.RegLvlFee regLvlFee = regLevelFeeMgr.Get(register.Pact.ID, register.DoctorInfo.Templet.RegLevel.ID);
            if (regLvlFee == null)
            {
                errInfo = "获取挂号费失败，原因：" + regLevelFeeMgr.Err + "（PACT=" + register.Pact.ID + "，RegLevel=" + register.DoctorInfo.Templet.RegLevel.ID + "）";
                return -1;
            }

            register.RegLvlFee = regLvlFee;

            #endregion

            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            FS.HISFC.BizLogic.Manager.Sequence sequenceMgr = new FS.HISFC.BizLogic.Manager.Sequence();
            FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            #region 补充

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            comPatientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            sequenceMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            accountMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            schemaMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            conMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region 专家判断限额
            //专家 判断限额
            if (register.RegLvlFee.RegLevel.IsExpert)//专家、专科,扣挂号限额
            {
                if (schemaMgr.AddLimit(register.DoctorInfo.Templet.ID, true, false, false, false) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "更新排班限额失败，原因：" + schemaMgr.Err;
                    return -1;
                }

                if (schemaMgr.Reduce(register.DoctorInfo.Templet.ID, true, false, false, false) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "更新排班限额失败，原因：" + schemaMgr.Err;
                    return -1;
                }

                //判断限额是否允许挂号
                FS.HISFC.Models.Registration.Schema schema = schemaMgr.GetByID(register.DoctorInfo.Templet.ID);
                if (schema == null || schema.Templet.ID == "")
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "获取排班限额失败，原因：" + schemaMgr.Err;
                    return -1;
                }

                if (schema.Templet.RegQuota - schema.RegedQTY <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "当前排班[" + CommonController.CreateInstance().GetEmployeeName(schema.Templet.Doct.ID) + "]已经超出限额，请重新选择！";
                    return -1;
                }

            }
            #endregion

            #region 补充Register

            //补充ClinicCode
            register.ID = regMgr.GetSequence("Registration.Register.ClinicID");
            register.RegType = FS.HISFC.Models.Base.EnumRegType.Reg;
            register.IsFee = false;
            register.IsSee = false;
            register.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Valid;
            //挂号日期
            register.DoctorInfo.SeeDate = CommonController.CreateInstance().GetSystemTime();
            register.DoctorInfo.Templet.Noon = CommonController.CreateInstance().GetNoon(register.DoctorInfo.SeeDate);
            register.InputOper.ID = "T00001";
            register.InputOper.Name = "自助挂号终端机";
            register.InputOper.OperTime = register.DoctorInfo.SeeDate;

           FS.FrameWork.Models.NeuObject obj=   conMgr.GetConstansObj("RegRecipeNo", register.InputOper.ID);
            if(obj==null)
            {
                obj=new FS.FrameWork.Models.NeuObject();
                obj.ID=register.InputOper.ID;
                obj.Name="0";
            }
            //自动获取
            register.RecipeNO = obj.Name;

            #endregion

            #region 发票

            //形成发票信息
            string strInvioceNO = "";
            string strRealInvoiceNO = "";
            string strInvoiceType = "R";

            //获取临时挂号发票号
            strInvioceNO = sequenceMgr.GetNewGHInvoiceNO();

            if (string.IsNullOrEmpty(strInvioceNO) || strInvioceNO.Equals("-1"))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "获取发票信息失败，原因：" + sequenceMgr.Err;
                return -1;
            }
            strInvioceNO = strInvioceNO.PadLeft(12, '0');
            strRealInvoiceNO = strInvioceNO;

            register.InvoiceNO = strInvioceNO;

            #endregion

            #region 费用

            List<AccountCardFee> lstCardFee = new List<AccountCardFee>();
            decimal othFee = 0, ownCost = 0, pubCost = 0;

            AccountCardFee cardFee = null;

            if (register.RegLvlFee.RegFee > 0)
            {
                cardFee = this.buildAccountCardFee(AccCardFeeType.RegFee, register.RegLvlFee.RegFee, 0);
                lstCardFee.Add(cardFee);
            }

            if (register.RegLvlFee.OwnDigFee > 0)
            {
                cardFee = this.buildAccountCardFee(AccCardFeeType.DiaFee, register.RegLvlFee.OwnDigFee, 0);
                lstCardFee.Add(cardFee);
            }

            if (register.RegLvlFee.ChkFee > 0)
            {
                cardFee = buildAccountCardFee(AccCardFeeType.ChkFee, register.RegLvlFee.ChkFee, 0);
                lstCardFee.Add(cardFee);
            }

            //诊金费是否记账
            bool IsPubDiagFee = controlParamMgr.GetControlParam<bool>("400008");

            if (IsPubDiagFee)
            {
                ownCost = register.RegLvlFee.RegFee + register.RegLvlFee.ChkFee;
                pubCost = register.RegLvlFee.OwnDigFee;
            }
            else
            {
                ownCost = register.RegLvlFee.ChkFee + register.RegLvlFee.RegFee + register.RegLvlFee.OwnDigFee;
                pubCost = 0;
            }

            register.OwnCost = ownCost;
            register.PubCost = pubCost;

            //有费用信息的时候才处理
            if (lstCardFee.Count > 0)
            {
                foreach (AccountCardFee accFee in lstCardFee)
                {
                    accFee.InvoiceNo = strInvioceNO;
                    accFee.Print_InvoiceNo = strRealInvoiceNO;
                    accFee.ClinicNO = register.ID;
                    accFee.Patient.PID.CardNO = register.PID.CardNO;
                    accFee.Patient.Name = register.Name;
                    accFee.IStatus = 1;
                    accFee.FeeOper.ID = register.InputOper.ID;
                    accFee.FeeOper.Oper.Name = register.InputOper.Name;
                    accFee.FeeOper.OperTime = register.DoctorInfo.SeeDate;
                    accFee.Oper.ID = accFee.FeeOper.ID;
                    accFee.Oper.Oper.Name = accFee.FeeOper.Oper.Name;
                    accFee.Oper.OperTime = accFee.FeeOper.OperTime;
                    accFee.IsBalance = false;
                    accFee.BalanceNo = "";
                }
            }

            #endregion

            #region 挂号及患者信息

            //插入挂号记录
             if (regMgr.Insert(register) <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "插入预挂号信息失败，原因：" + regMgr.Err;
                return -1;
            }

            if (comPatientMgr.UpdatePatient(register) <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "保存患者信息失败，原因：" + comPatientMgr.Err;
                return -1;
            }

            #endregion

            #region 保存费用明细信息

            if (lstCardFee != null && lstCardFee.Count > 0)
            {
                foreach (AccountCardFee accountCardFee in lstCardFee)
                {
                    if (accountMgr.InsertAccountCardFee(accountCardFee) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errInfo = "保存费用信息失败，原因：" + comPatientMgr.Err;
                        return -1;
                    }
                }
            }

            #endregion

            //临时发票号不需要走号

            #region 处方号走号
            FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
            con.ID = obj.ID;
            con.Name =( FS.FrameWork.Function.NConvert.ToInt32(obj.Name) + 1).ToString();//处方号
            con.IsValid = true;

            int rtn = conMgr.UpdateConstant("RegRecipeNo", con);
            if (rtn == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "更新处方号失败，原因：" + conMgr.Err;
                return -1;
            }
            if (rtn == 0)//更新没有数据、插入
            {
                if (conMgr.InsertConstant("RegRecipeNo", con) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "插入处方号失败，原因：" + conMgr.Err;
                    return -1;
                }
            }

            #endregion

            #region 专家号走限额

            //专家号限额增加
            if (register.RegLvlFee.RegLevel.IsExpert)//专家、专科,扣挂号限额
            {
                if (schemaMgr.AddLimit(register.DoctorInfo.Templet.ID, true, false, false, false) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "更新排班限额失败，原因：" + schemaMgr.Err;
                    return -1;
                }
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();

            #endregion

            #region SRR_S01-返回信息

            if (ackMessage == null)
            {
                ackMessage = new NHapi.Model.V24.Message.SRR_S01();
            }

            FS.HL7Message.V24.Function.GenerateMSH(ackMessage.MSH, o.MSH);
            FS.HL7Message.V24.Function.GenerateSuccessMSA(o.MSH, ackMessage.MSA);
            ackMessage.MSA.ExpectedSequenceNumber.Value = "100";

            ackMessage.SCHEDULE.GetPATIENT(0).PID.PatientID.ID.Value = register.PID.CardNO;
            ackMessage.SCHEDULE.GetPATIENT(0).PV1.PatientClass.Value = "O";
            ackMessage.SCHEDULE.GetPATIENT(0).PV1.VisitNumber.ID.Value = register.ID;
            ackMessage.SCHEDULE.GetPATIENT(0).PV1.AlternateVisitID.ID.Value = register.ID;

            #endregion

            return 1;
        }

        private AccountCardFee buildAccountCardFee(AccCardFeeType feeType, decimal ownCost, decimal pubCost)
        {
            AccountCardFee cardFee = new AccountCardFee();
            cardFee.FeeType = feeType;
            cardFee.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
            cardFee.IStatus = 1;
            cardFee.Own_cost = ownCost;
            cardFee.Pub_cost = pubCost;
            cardFee.Tot_cost = ownCost + pubCost;

            return cardFee;
        }

        public int ProcessMessage(NHapi.Model.V24.Message.SRM_S01 o, ref NHapi.Model.V24.Message.SRR_S01 SRRS01, ref string errInfo)
        {
            SRRS01 = new NHapi.Model.V24.Message.SRR_S01();
            FS.HL7Message.V24.Function.GenerateMSH(SRRS01.MSH, o.MSH);
            if (this.processMessage(o, ref SRRS01, ref errInfo) <= 0)
            {
                FS.HL7Message.V24.Function.GenerateErrorMSA(o.MSH, SRRS01.MSA, errInfo);
                SRRS01.MSA.TextMessage.Value = errInfo;
                SRRS01.MSA.ExpectedSequenceNumber.Value = "200";
                return -1;
            }
            else
            {
                FS.HL7Message.V24.Function.GenerateSuccessMSA(o.MSH, SRRS01.MSA);
                SRRS01.MSA.ExpectedSequenceNumber.Value = "100";
                return 1;
            }
        }

    }
}
