using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace FS.SOC.Local.Account.GuangZhou.ZDLY.IReadMarkNO
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
            if (returnValue == -1) //出错的
            {
                return -1;
            }
            else if (returnValue == 2) //直接病历号的
            {
                FS.HISFC.Models.RADT.PatientInfo patient = null;
                accountCard.Patient.PID.CardNO = realCardNo.PadLeft(10,'0');
                if (string.IsNullOrEmpty(accountCard.Patient.PID.CardNO))
                {
                    //accountCard.Patient = null;
                    accountCard.Patient = new FS.HISFC.Models.RADT.PatientInfo();
                    return 0;
                }

                if (accountCard.Patient.PID.CardNO.StartsWith("9"))
                {
                    ArrayList al = this.regIntegrate.Query(accountCard.Patient.PID.CardNO,DateTime.Now.AddMonths(-1));
                    if (al != null && al.Count > 0)
                    {
                        FS.HISFC.Models.Registration.Register register = (FS.HISFC.Models.Registration.Register)al[0];
                        if (register != null)
                        {
                            patient = new FS.HISFC.Models.RADT.PatientInfo();
                            patient.Name = register.Name;
                            patient.PID.CardNO = register.PID.CardNO;
                            patient.Sex = register.Sex;
                            patient.Birthday = register.Birthday;
                            patient.AddressHome = register.AddressHome;
                            patient.PhoneHome = register.PhoneHome;
                            patient.IDCard = register.IDCard;
                            patient.Pact = register.Pact;
                        }
                    }
                    if (patient == null || string.IsNullOrEmpty(patient.PID.CardNO))
                    {
                        FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                        if (FS.SOC.Local.Account.GuangZhou.Zdly.Function.IsContainYKDept(employee.Dept.ID))
                        {
                            //DialogResult dResult = MessageBox.Show("未找到患者信息，是否关联住院号查询？", "提示",
                            //    MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                            //if (dResult == DialogResult.Yes)
                            //{
                            patient = radtIntegrate.QueryComPatientInfoByPatientNo(accountCard.Patient.PID.CardNO);
                            //}
                        }

                        if (patient == null || string.IsNullOrEmpty(patient.PID.CardNO))
                        {
                            this.error = "没有找到病历号的基本信息！";
                            accountCard.Patient = new FS.HISFC.Models.RADT.PatientInfo();
                            //accountCard.Patient = null;
                            return 0;
                        }
                    }
                }
                else
                {
                    patient = radtIntegrate.QueryComPatientInfo(accountCard.Patient.PID.CardNO);
                    if (patient == null || string.IsNullOrEmpty(patient.PID.CardNO))
                    {
                        FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                        if (FS.SOC.Local.Account.GuangZhou.Zdly.Function.IsContainYKDept(employee.Dept.ID))
                        {
                            //DialogResult dResult = MessageBox.Show("未找到患者信息，是否关联住院号查询？", "提示",
                            //    MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                            //if (dResult == DialogResult.Yes)
                            //{
                            patient = radtIntegrate.QueryComPatientInfoByPatientNo(accountCard.Patient.PID.CardNO);
                            //}
                        }

                        if (patient == null || string.IsNullOrEmpty(patient.PID.CardNO))
                        {
                            this.error = "没有找到病历号的基本信息！";
                            accountCard.Patient = new FS.HISFC.Models.RADT.PatientInfo();
                            //accountCard.Patient = null;
                            return 0;
                        }
                    }
                }
                accountCard.Patient = patient;
                return 1;

            }
            else //直接卡号的
            {
                accountCard.MarkType = cardTypeObj;
                accountCard.MarkNO = markNO;

                accountCard.Patient.PID.CardNO = "";

                FS.HISFC.Models.Account.AccountCard accountCardTemp = new FS.HISFC.Models.Account.AccountCard();
                accountCardTemp = accountManager.GetAccountCard(realCardNo, accountCard.MarkType.ID);
                FS.HISFC.Models.RADT.PatientInfo patient = null;
                //卡未发放
                if (accountCardTemp == null)
                {
                    this.error = "该卡类型为：" + accountCard.MarkType.Name + "\n 该卡还未发放！";
                    return -1;
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

                    accountCard = accountCardTemp;
                    if (string.IsNullOrEmpty(accountCard.Patient.PID.CardNO))
                    {
                        accountCardTemp.Patient = new FS.HISFC.Models.RADT.PatientInfo();
                        return 0;
                    }
                    patient = radtIntegrate.QueryComPatientInfo(accountCard.Patient.PID.CardNO);
                    if (patient == null || string.IsNullOrEmpty(patient.PID.CardNO))
                    {
                        this.error = "没有找到病历号的基本信息！";
                        accountCardTemp.Patient = new FS.HISFC.Models.RADT.PatientInfo();
                        return 0;
                    }
                    accountCard.Patient = patient;
                    return 1;
                }
 
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

        List<FS.HISFC.Models.Account.AccountCard> cardList = null;
        FS.FrameWork.Public.ObjectHelper cardTypeHelper = null;

        /// <summary>
        /// 获取卡类型
        /// </summary>
        /// <param name="markNO">输入的卡信息</param>
        /// <param name="cardType">卡类型</param>
        /// <param name="errText">错误信息</param>
        /// <param name="realCardNo">真实的卡号，去掉了关键字</param>
        /// <param name="memo">增加是否是新办卡参数</param>
        /// <returns></returns>
        private int GetCardType(string markNO, ref FS.FrameWork.Models.NeuObject cardTypeObj, ref string errText, ref string realCardNo,string memo)
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

                int resultd = -1;
                foreach (FS.HISFC.Models.Base.Const conObj in cardTypeList)
                {
                    markNO = markNO.ToUpper();
                    if (markNO.StartsWith(conObj.Memo) && markNO.Length==12)
                    {
                        cardTypeObj = conObj.Clone();
                        cardTypeObj.ID = conObj.ID;
                        cardTypeObj.Name = conObj.Name;
                        realCardNo = markNO;
                        return  1;
                     
                    }
                    else if (markNO.StartsWith(conObj.Memo)　&& !markNO.StartsWith("01"))
                    {
                        cardTypeObj = conObj.Clone();
                        cardTypeObj.ID = conObj.ID;
                        cardTypeObj.Name = conObj.Name;
                        realCardNo = markNO;
                        return  1;
                    }
                    else if (markNO.StartsWith("*"))//挂号全院序号
                    {
                        //按照序号查找
                       ArrayList alRegister= regIntegrate.QueryValidPatientsBySeeNO(markNO.Substring(1), accountManager.GetDateTimeFromSysDateTime().Date);
                       if (alRegister != null && alRegister.Count > 0)
                       {
                           markNO = (alRegister[0] as FS.HISFC.Models.Registration.Register).PID.CardNO;
                       }
                    }
                    
                }

                if (markNO.Length == 10)
                {
                    realCardNo = markNO;
                    return 2;
                }
                else
                {
                    realCardNo = markNO.PadLeft(10, '0');
                    return 2;
                }
            }
            catch (Exception ex)
            {
                errText = ex.Message;
                cardTypeObj = null;
                realCardNo = markNO;
                return -1;
            }
            this.error = "没有找到卡类型";
            return -1;
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
