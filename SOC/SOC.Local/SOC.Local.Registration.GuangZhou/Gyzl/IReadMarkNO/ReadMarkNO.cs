using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace FS.SOC.Local.Registration.GuangZhou.Gyzl.IReadMarkNO
{
    public class ReadMarkNO : FS.HISFC.BizLogic.Fee.IReadMarkNO
    {
        FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
        FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.FrameWork.Management.ControlParam ctlParam = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// 交叉管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager interManager = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 挂号管理
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// 患者卡类型列表
        /// </summary>
        private static ArrayList cardTypeList = null;

        /// <summary>
        /// 患者信息
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// 根据对照，获取病历号
        /// </summary>
        /// <param name="markNO">物理卡号</param>
        /// <param name="accountCard">账户实体（存卡类型）</param>
        /// <returns>0:病历号未找到；1 成功；-1 错误</returns>
        public int ReadMarkNOByRule(string markNO, ref FS.HISFC.Models.Account.AccountCard accountCard)
        {
            this.error = "";

            //判断返回值
            int returnValue = 0;

            FS.FrameWork.Models.NeuObject cardTypeObj = new FS.FrameWork.Models.NeuObject();

            string realCardNo = markNO;
            returnValue = this.GetCardType(markNO, ref cardTypeObj, ref this.error, ref realCardNo, accountCard.Memo);
            if (returnValue == -1)
            {
                return -1;
            }

            accountCard.MarkType = cardTypeObj;
            accountCard.MarkNO = realCardNo;

            FS.HISFC.Models.Account.AccountCard accountCardTemp = new FS.HISFC.Models.Account.AccountCard();
            accountCardTemp = accountManager.GetAccountCard(realCardNo, accountCard.MarkType.ID);
            FS.HISFC.Models.RADT.PatientInfo patient = null;
            //卡未发放
            if (accountCardTemp == null)
            {
                //员工号，如果不存在则提示
                if (realCardNo.StartsWith("*"))
                {
                    FS.HISFC.Models.Base.Employee empl = this.interManager.GetEmployeeInfo(realCardNo.Remove(0, 1).PadLeft(6, 'A'));
                    if (empl == null || string.IsNullOrEmpty(empl.ID))
                    {
                        //再根据0找一遍
                        empl = this.interManager.GetEmployeeInfo(realCardNo.Remove(0, 1).PadLeft(6, '0'));
                        if (empl == null || string.IsNullOrEmpty(empl.ID))
                        {
                            this.error = "没有找到该编号的职工！";
                            return -1;
                        }
                    }
                    //员工卡自动生成病历号
                    returnValue = this.regIntegrate.AutoGetCardNO();

                    if (returnValue == -1)
                    {
                        this.error = "未能成功自动产生卡号，请联系信息科！";
                        return -1;
                    }
                    realCardNo = returnValue.ToString().PadLeft(10, '0');

                    return ResetPatientInfo(empl, realCardNo, ref this.error, accountCard);
                }
                //如果卡类型为空，认为直接输入的病历号
                else if (string.IsNullOrEmpty(accountCard.MarkType.ID) || accountCard.MarkType.ID == "病历号")
                {
                    accountCard.MarkType.ID = "Card_No";
                    //找卡信息找不到时，直接按照病历号查找
                    patient = radtIntegrate.QueryComPatientInfo(realCardNo.PadLeft(10, '0'));
                    if (patient == null || string.IsNullOrEmpty(patient.PID.CardNO))
                    {
                        this.error = "输入卡号或病例号错误，请重新输入！";

                        //挂号的时候，新病历号不提示
                        if (accountCard.Memo == "1")
                        {
                            return 1;
                        }
                        //办卡的时候新卡不提示
                        else if (accountCard.Memo == "2")
                        {
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    accountCard.Patient = patient;
                    return 1;
                }
                else
                {
                    this.error = "该卡类型为：" + accountCard.MarkType.Name + "\n 该卡还未发放！";
                    //挂号的时候，新病历号不提示
                    if (accountCard.Memo == "1")
                    {
                        return 1;
                    }
                    //办卡的时候新卡不提示
                    else if (accountCard.Memo == "2")
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            else if (accountCardTemp.MarkStatus == FS.HISFC.Models.Account.MarkOperateTypes.Stop)
            {
                this.error = "该卡号已经停用！";
                return -1;
            }
            else if (accountCardTemp.MarkStatus == FS.HISFC.Models.Account.MarkOperateTypes.Cancel)
            {
                this.error = "该卡号已经回收";
                return -1;
            }
            else
            {
                accountCardTemp.MarkType = accountCard.MarkType;
                //accountCardTemp.MarkType.ID = accountCard.MarkType.ID;
                //accountCardTemp.MarkType.Name = accountCard.MarkType.Name;
                accountCard = accountCardTemp;

                if (string.IsNullOrEmpty(accountCard.Patient.PID.CardNO))
                {
                    return 0;
                }
                patient = radtIntegrate.QueryComPatientInfo(accountCard.Patient.PID.CardNO);
                if (patient == null)
                {
                    return 0;
                }
                accountCard.Patient = patient;
                return 1;
            }

        }

        /// <summary>
        /// 根据职工信息保存就诊卡信息和患者基本信息
        /// </summary>
        /// <param name="empl"></param>
        /// <param name="cardno"></param>
        /// <param name="errtxt"></param>
        /// <returns></returns>
        private int ResetPatientInfo(FS.HISFC.Models.Base.Employee empl, string cardno, ref string errtxt, FS.HISFC.Models.Account.AccountCard accountCardObj)
        {
            if (empl == null)
            {
                errtxt = "员工实体为空!";
                return -1;
            }

            if (string.IsNullOrEmpty(cardno))
            {
                errtxt = "员工卡获取病历号信息出错!";
                return -1;
            }

            patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

            patientInfo = ConvertEmplToPatient(empl);

            if (patientInfo == null)
            {
                errtxt = "员工信息转换为患者实体时出现异常";
                return -1;
            }
            patientInfo.PID.CardNO = cardno;

            accountCardObj.Patient = patientInfo;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (accountManager.InsertAccountCard(accountCardObj) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errtxt = "保存员工卡记录失败！" + accountManager.Err;
                return -1;
            }

            if (radtIntegrate.InsertPatientInfo(patientInfo) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errtxt = "保存员工就诊信息失败！" + accountManager.Err;
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        /// <summary>
        /// 员工信息转换为患者信息实体
        /// </summary>
        /// <param name="empl"></param>
        /// <returns></returns>
        private FS.HISFC.Models.RADT.PatientInfo ConvertEmplToPatient(FS.HISFC.Models.Base.Employee empl)
        {
            patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

            patientInfo.Name = empl.Name;
            patientInfo.Sex.ID = empl.Sex.ID;
            patientInfo.Birthday = empl.Birthday;
            patientInfo.IDCard = empl.IDCard;
            FS.HISFC.Models.Base.ISpell sp = this.interManager.Get(empl.Name);
            patientInfo.SpellCode = sp.SpellCode;
            patientInfo.WBCode = sp.WBCode;

            if (empl.User01.Equals("1"))//在职公费
            {
                patientInfo.Pact.PayKind.ID = "03";
                patientInfo.Pact.ID = "M55";
            }
            else if (empl.User01.Equals("2"))//本院医保
            {
                patientInfo.Pact.PayKind.ID = "02";
                patientInfo.Pact.ID = "3";
            }
            else//本院自费
            {
                patientInfo.Pact.PayKind.ID = "01";
                patientInfo.Pact.ID = "4";
            }

            return patientInfo;
        }

        /// <summary>
        /// 转换卡号
        /// 如果是全角输入的，改为半角显示
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        private string TranslateKeys(string cardNo)
        {
            try
            {
                string cardNoNew = "";
                char[] keys;
                keys = cardNo.ToCharArray();
                char key;
                for (int i = 0; i < keys.Length; i++)
                {
                    key = keys[i];
                    if (key == '０')
                    {
                        key = '0';
                    }
                    else if (key == '１')
                    {
                        key = '1';
                    }
                    else if (key == '２')
                    {
                        key = '2';
                    }
                    else if (key == '３')
                    {
                        key = '3';
                    }
                    else if (key == '４')
                    {
                        key = '4';
                    }
                    else if (key == '５')
                    {
                        key = '5';
                    }
                    else if (key == '６')
                    {
                        key = '6';
                    }
                    else if (key == '７')
                    {
                        key = '7';
                    }
                    else if (key == '８')
                    {
                        key = '8';
                    }
                    else if (key == '９')
                    {
                        key = '9';
                    }
                    cardNoNew += key;
                }
                return cardNoNew;
            }
            catch
            {
                return cardNo;
            }
        }

        /// <summary>
        /// 获取卡类型
        /// </summary>
        /// <param name="markNO">输入的卡信息</param>
        /// <param name="cardType">卡类型</param>
        /// <param name="errText">错误信息</param>
        /// <param name="realCardNo">真实的卡号，去掉了关键字</param>
        /// <param name="memo">增加是否是新办卡参数</param>
        /// <returns></returns>
        private int GetCardType(string markNO, ref FS.FrameWork.Models.NeuObject cardTypeObj, ref string errText, ref string realCardNo, string memo)
        {
            try
            {
                markNO = this.TranslateKeys(markNO);
                //卡类型常数
                if (cardTypeList == null)
                {
                    cardTypeList = this.interManager.GetConstantList("MarkType");
                    if (cardTypeList == null)
                    {
                        errText = "获取卡类型出错：" + interManager.Err;
                        return -1;
                    }
                }

                //比较输入内容的第一个字符
                string keyWord = markNO.Substring(0, 1);

                bool isMatch = false;
                foreach (FS.HISFC.Models.Base.Const conObj in cardTypeList)
                {
                    if (string.IsNullOrEmpty(conObj.UserCode) == false)
                    {
                        Regex regex = new Regex(@conObj.UserCode, RegexOptions.Multiline);
                        if (regex.IsMatch(markNO))
                        {
                            cardTypeObj = conObj.Clone();
                            cardTypeObj.ID = conObj.ID;
                            cardTypeObj.Name = conObj.Name;
                            isMatch = true;
                            break;
                        }
                    }
                }
                if (!isMatch && markNO.Length != 10) //暂时处理
                {
                    this.error = "卡号不符合该卡类型规则";
                    return -1;
                }

                //没有卡信息的重新根据病历号查找卡号
                if (string.IsNullOrEmpty(cardTypeObj.ID))
                {
                    string accountCardMarkNo = string.Empty;
                    FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
                    List<FS.HISFC.Models.Account.AccountCard> list = accountManager.GetAccountCardByNormalNo(markNO);//此时markNo有可能为病历号
                    if (list.Count > 0)
                    {
                        accountCardMarkNo = list[0].MarkNO;
                    }
                    if (string.IsNullOrEmpty(accountCardMarkNo))
                    {
                        cardTypeObj.ID = "病历号";
                        cardTypeObj.Name = "病历号";
                        realCardNo = markNO;
                    }
                    else
                    {
                        realCardNo = accountCardMarkNo;
                        GetCardType(realCardNo, ref cardTypeObj, ref errText, ref realCardNo, memo);//重新根据真实卡号获取
                    }
                }
              
            }
            catch (Exception ex)
            {
                errText = ex.Message;
                cardTypeObj = null;
                realCardNo = markNO;
                return -1;
            }

            //realCardNo = markNO;
            return 1;
        }

        private string error = string.Empty;
        public string Error
        {
            get
            {
                return error;
            }
            set
            {
                error = value;
            }
        }
    }
}
