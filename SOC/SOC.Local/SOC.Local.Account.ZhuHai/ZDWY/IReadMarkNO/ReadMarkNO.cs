using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace FS.SOC.Local.Account.ZhuHai.ZDWY.IReadMarkNO
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
            if (returnValue == -1)
            {
                return -1;
            }

            accountCard.MarkType = cardTypeObj;
            accountCard.MarkNO = realCardNo;
            //accountCard.Patient.PID.CardNO = realCardNo.PadLeft(10, '0');
            accountCard.Patient.PID.CardNO = realCardNo;

            FS.HISFC.Models.Account.AccountCard accountCardTemp = new FS.HISFC.Models.Account.AccountCard();
            accountCardTemp = accountManager.GetAccountCard(realCardNo, accountCard.MarkType.ID);
            FS.HISFC.Models.RADT.PatientInfo patient = null;
            //��δ����
            if (accountCardTemp == null)
            {
                //���������Ϊ�գ���Ϊֱ������Ĳ�����
                if (string.IsNullOrEmpty(accountCard.MarkType.ID) || accountCard.MarkType.ID == "������")
                {
                    accountCard.MarkType.ID = "Card_No";
                    //�ҿ���Ϣ�Ҳ���ʱ��ֱ�Ӱ��ղ����Ų���
                    patient = radtIntegrate.QueryComPatientInfo(realCardNo.PadLeft(10, '0'));
                    if (patient == null || string.IsNullOrEmpty(patient.PID.CardNO))
                    {
                        this.error = "���뿨�Ż����Ŵ������������룡";

                        //�Һŵ�ʱ���²����Ų���ʾ
                        if (accountCard.Memo == "1")
                        {
                            return 1;
                        }
                        //�쿨��ʱ���¿�����ʾ
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
                else if (accountCard.MarkType.Name.Contains("��Ժְ��"))
                {
                    this.error = "�ÿ�����Ϊ��" + accountCard.MarkType.Name + "\n �����ݻ�û��ά�� \n ����ϵ��Ϣ�ƣ�";

                    //�쿨��ʱ���¿�����ʾ
                    if (accountCard.Memo == "2")
                    {
                        return 0;
                    }
                    return -1;


                }
                //Ա������Զ����� ������
                else if (accountCard.MarkType.Name.Contains("Ա��"))
                {
                    FS.HISFC.Models.Base.Employee empl = this.interManager.GetEmployeeInfo(realCardNo.PadLeft(6, '0'));
                    if (empl == null || string.IsNullOrEmpty(empl.ID))
                    {
                        this.error = "û���ҵ��ñ�ŵ�ְ����";
                        return -1;
                    }
                    //Ա�����Զ����ɲ�����
                    returnValue = this.regIntegrate.AutoGetCardNO();

                    if (returnValue == -1)
                    {
                        this.error = "δ�ܳɹ��Զ��������ţ�����ϵ��Ϣ�ƣ�";
                        return -1;
                    }
                    realCardNo = returnValue.ToString().PadLeft(10, '0');

                    return ResetPatientInfo(empl, realCardNo, ref this.error, accountCard);
                }
                else
                {
                    this.error = "�ÿ�����Ϊ��" + accountCard.MarkType.Name + "\n �ÿ���δ���ţ�";

                    //�Һŵ�ʱ���²����Ų���ʾ
                    if (accountCard.Memo == "1")
                    {
                        return 1;
                    }
                    //�쿨��ʱ���¿�����ʾ
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
        private int GetCardType(string markNO, ref FS.FrameWork.Models.NeuObject cardTypeObj, ref string errText, ref string realCardNo, string memo)
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

                //�Ƚ��������ݵĵ�һ���ַ�
                string keyWord = markNO.Substring(0, 1);

                foreach (FS.HISFC.Models.Base.Const conObj in cardTypeList)
                {
                    if (conObj.Memo.Contains(keyWord))
                    {
                        cardTypeObj = conObj.Clone();

                        cardTypeObj.ID = conObj.ID;
                        cardTypeObj.Name = conObj.Name;

                        if (cardTypeObj.Name.Contains("Ա��"))
                        {
                            realCardNo = realCardNo.PadLeft(6, '0');
                        }

                        if (cardTypeObj.Name.Contains("��Ժְ��"))
                        {
                            realCardNo = realCardNo.Substring(1, realCardNo.Length - 1);
                        }

                        if (markNO.Length < 20 && conObj.Name.Contains("����"))
                        {
                            if (markNO.Length > 10)
                            {
                                realCardNo = markNO.Substring(1, 10);
                            }
                            else
                            {
                                realCardNo = markNO.Substring(1);
                            }

                            break;
                        }

                        if (markNO.Length >= 20 && conObj.Name.Contains("����"))
                        {
                            realCardNo = markNO.Substring(1, 19);
                            break;
                        }
                    }
                    //jing.zhao ʹ��������ʽ������������ж�
                    else if (string.IsNullOrEmpty(conObj.UserCode) == false)
                    {
                        Regex regex = new Regex(@conObj.UserCode, RegexOptions.Multiline);
                        if (regex.IsMatch(markNO))
                        {
                            cardTypeObj = conObj.Clone();
                            cardTypeObj.ID = conObj.ID;
                            cardTypeObj.Name = conObj.Name;
                            break;
                        }

                    }

                }

                //û�п���Ϣ����ʾ������
                if (string.IsNullOrEmpty(cardTypeObj.ID))
                {
                    realCardNo = markNO;
                }

            }
            catch (Exception ex)
            {
                errText = ex.Message;
                cardTypeObj = null;
                realCardNo = markNO;
                return -1;
            }

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
