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
        /// �������ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager interManager = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// �ҺŹ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// ���߿������б�
        /// </summary>
        private static ArrayList cardTypeList = null;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// ���ݶ��գ���ȡ������
        /// </summary>
        /// <param name="markNO">������</param>
        /// <param name="accountCard">�˻�ʵ�壨�濨���ͣ�</param>
        /// <returns>0:������δ�ҵ���1 �ɹ���-1 ����</returns>
        public int ReadMarkNOByRule(string markNO, ref FS.HISFC.Models.Account.AccountCard accountCard)
        {
            this.error = "";

            //�жϷ���ֵ
            int returnValue = 0;

            FS.FrameWork.Models.NeuObject cardTypeObj = new FS.FrameWork.Models.NeuObject();

            string realCardNo = markNO;
            returnValue = this.GetCardType(markNO, ref cardTypeObj, ref this.error, ref realCardNo, accountCard.Memo);
            if (returnValue == -1) //�����
            {
                return -1;
            }
            else if (returnValue == 2) //ֱ�Ӳ����ŵ�
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
                            //DialogResult dResult = MessageBox.Show("δ�ҵ�������Ϣ���Ƿ����סԺ�Ų�ѯ��", "��ʾ",
                            //    MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                            //if (dResult == DialogResult.Yes)
                            //{
                            patient = radtIntegrate.QueryComPatientInfoByPatientNo(accountCard.Patient.PID.CardNO);
                            //}
                        }

                        if (patient == null || string.IsNullOrEmpty(patient.PID.CardNO))
                        {
                            this.error = "û���ҵ������ŵĻ�����Ϣ��";
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
                            //DialogResult dResult = MessageBox.Show("δ�ҵ�������Ϣ���Ƿ����סԺ�Ų�ѯ��", "��ʾ",
                            //    MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                            //if (dResult == DialogResult.Yes)
                            //{
                            patient = radtIntegrate.QueryComPatientInfoByPatientNo(accountCard.Patient.PID.CardNO);
                            //}
                        }

                        if (patient == null || string.IsNullOrEmpty(patient.PID.CardNO))
                        {
                            this.error = "û���ҵ������ŵĻ�����Ϣ��";
                            accountCard.Patient = new FS.HISFC.Models.RADT.PatientInfo();
                            //accountCard.Patient = null;
                            return 0;
                        }
                    }
                }
                accountCard.Patient = patient;
                return 1;

            }
            else //ֱ�ӿ��ŵ�
            {
                accountCard.MarkType = cardTypeObj;
                accountCard.MarkNO = markNO;

                accountCard.Patient.PID.CardNO = "";

                FS.HISFC.Models.Account.AccountCard accountCardTemp = new FS.HISFC.Models.Account.AccountCard();
                accountCardTemp = accountManager.GetAccountCard(realCardNo, accountCard.MarkType.ID);
                FS.HISFC.Models.RADT.PatientInfo patient = null;
                //��δ����
                if (accountCardTemp == null)
                {
                    this.error = "�ÿ�����Ϊ��" + accountCard.MarkType.Name + "\n �ÿ���δ���ţ�";
                    return -1;
                }
                else if (accountCardTemp.MarkStatus == FS.HISFC.Models.Account.MarkOperateTypes.Stop)
                {
                    this.error = "�ÿ����Ѿ�ͣ�ã�";
                    return -1;
                }
                else if (accountCardTemp.MarkStatus == FS.HISFC.Models.Account.MarkOperateTypes.Cancel)
                {
                    this.error = "�ÿ����Ѿ�����";
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
                        this.error = "û���ҵ������ŵĻ�����Ϣ��";
                        accountCardTemp.Patient = new FS.HISFC.Models.RADT.PatientInfo();
                        return 0;
                    }
                    accountCard.Patient = patient;
                    return 1;
                }
 
            }

        }

        /// <summary>
        /// ����ְ����Ϣ������￨��Ϣ�ͻ��߻�����Ϣ
        /// </summary>
        /// <param name="empl"></param>
        /// <param name="cardno"></param>
        /// <param name="errtxt"></param>
        /// <returns></returns>
        private int ResetPatientInfo(FS.HISFC.Models.Base.Employee empl, string cardno, ref string errtxt, FS.HISFC.Models.Account.AccountCard accountCardObj)
        {
            if (empl == null)
            {
                errtxt = "Ա��ʵ��Ϊ��!";
                return -1;
            }

            if (string.IsNullOrEmpty(cardno))
            {
                errtxt = "Ա������ȡ��������Ϣ����!";
                return -1;
            }

            patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

            patientInfo = ConvertEmplToPatient(empl);

            if (patientInfo == null)
            {
                errtxt = "Ա����Ϣת��Ϊ����ʵ��ʱ�����쳣";
                return -1;
            }
            patientInfo.PID.CardNO = cardno;

            accountCardObj.Patient = patientInfo;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (accountManager.InsertAccountCard(accountCardObj) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errtxt = "����Ա������¼ʧ�ܣ�" + accountManager.Err;
                return -1;
            }

            if (radtIntegrate.InsertPatientInfo(patientInfo) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errtxt = "����Ա��������Ϣʧ�ܣ�" + accountManager.Err;
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        /// <summary>
        /// Ա����Ϣת��Ϊ������Ϣʵ��
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
        /// ת������
        /// �����ȫ������ģ���Ϊ�����ʾ
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
                    if (key == '��')
                    {
                        key = '0';
                    }
                    else if (key == '��')
                    {
                        key = '1';
                    }
                    else if (key == '��')
                    {
                        key = '2';
                    }
                    else if (key == '��')
                    {
                        key = '3';
                    }
                    else if (key == '��')
                    {
                        key = '4';
                    }
                    else if (key == '��')
                    {
                        key = '5';
                    }
                    else if (key == '��')
                    {
                        key = '6';
                    }
                    else if (key == '��')
                    {
                        key = '7';
                    }
                    else if (key == '��')
                    {
                        key = '8';
                    }
                    else if (key == '��')
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
        /// ��ȡ������
        /// </summary>
        /// <param name="markNO">����Ŀ���Ϣ</param>
        /// <param name="cardType">������</param>
        /// <param name="errText">������Ϣ</param>
        /// <param name="realCardNo">��ʵ�Ŀ��ţ�ȥ���˹ؼ���</param>
        /// <param name="memo">�����Ƿ����°쿨����</param>
        /// <returns></returns>
        private int GetCardType(string markNO, ref FS.FrameWork.Models.NeuObject cardTypeObj, ref string errText, ref string realCardNo,string memo)
        {
            try
            {
                markNO = this.TranslateKeys(markNO);
                //�����ͳ���
                if (cardTypeList == null)
                {
                    cardTypeList = this.interManager.GetConstantList("MarkType");
                    if (cardTypeList == null)
                    {
                        errText = "��ȡ�����ͳ���" + interManager.Err;
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
                    else if (markNO.StartsWith(conObj.Memo)��&& !markNO.StartsWith("01"))
                    {
                        cardTypeObj = conObj.Clone();
                        cardTypeObj.ID = conObj.ID;
                        cardTypeObj.Name = conObj.Name;
                        realCardNo = markNO;
                        return  1;
                    }
                    else if (markNO.StartsWith("*"))//�Һ�ȫԺ���
                    {
                        //������Ų���
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
            this.error = "û���ҵ�������";
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
