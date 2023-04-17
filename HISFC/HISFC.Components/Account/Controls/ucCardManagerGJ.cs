using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Account;
using FS.FrameWork.Models;
using FS.HISFC.BizProcess.Interface.Account;
using FS.FrameWork.Management;
using System.Text.RegularExpressions;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.HISFC.BizProcess.Interface;

namespace FS.HISFC.Components.Account.Controls
{
    /// <summary>
    /// ���￨����
    /// </summary>
    public partial class ucCardManagerGJ : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucCardManagerGJ()
        {
            InitializeComponent();
            if (DesignMode)
            {
                return;
            }
        }

        #region ����

        /// <summary>
        /// HealthCardҵ���
        /// </summary>
        private FS.HISFC.BizLogic.HealthCard.HealthCardManager healthCardManager = new FS.HISFC.BizLogic.HealthCard.HealthCardManager();

        /// <summary>
        /// ������ʵ��
        /// </summary>
        private FS.HISFC.BizLogic.HealthCard.HealthCard healthCard = new FS.HISFC.BizLogic.HealthCard.HealthCard();

        /// <summary>
        /// Managerҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// �������ҵ����
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();
        
        /// <summary>
        /// Acountҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
        /// <summary>
        /// ���ù�����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeManage = new FS.HISFC.BizProcess.Integrate.Fee();
        
        /// <summary>
        /// ��ʵ��
        /// </summary>
        private FS.HISFC.Models.Account.AccountCard accountCard = null;

        /// <summary>
        /// ������ʵ��
        /// </summary>
        private FS.HISFC.Models.Account.AccountCardRecord accountCardRecord = null;
        /// <summary>
        /// ���תʵ��
        /// </summary>
        private HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        
        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBar = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// �����Ͱ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper markTypeHelp = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// �����¼ҵ���
        /// </summary>
        HISFC.BizProcess.Integrate.Function functionIntegrate = new FS.HISFC.BizProcess.Integrate.Function();

        private FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();
        
        /// <summary>
        /// ������������Ƿ�̬���һ�����Ϣ
        /// </summary>
        private bool isSelectPatientByEnter = false;
        /// <summary>
        /// �����Ƿ�ֻ�ڱ��ش��������������ķ���
        /// {BCE8D830-5FEA-4681-A08A-4BB48D172E20}
        /// </summary>
        private bool isLocalOperation = true;
        /// <summary>
        /// �쿨ʱ�Ƿ�ʵʱ�жϣ��Ƿ�������Ӧ�ĺ�ͬ��λ
        /// {C49AFFB1-D0EA-41bf-AD60-9F921D91E93D}
        /// </summary>
        private bool isJudgePact = false;
        /// <summary>
        /// �½����Ƿ���ȡ���ɱ��ѣ�0=����ȡ��1=��ȡ��2=���������ȡ
        /// </summary>
        private string IsAcceptCardFee = "0";
        /// <summary>
        /// �����Ƿ���ȡ���ɱ��� 0=����ȡ��1=��ȡ��2=���������ȡ
        /// </summary>
        private string IsAcceptChangeCardFee = "0";

        /// <summary>
        /// �Ƿ�����ֶ������Ա����
        /// </summary>
        private bool IsCanEditMakeNO = false;
        /// <summary>
        /// �˿�ʱ�˷� 0=���ˣ�1=�˷�
        /// </summary>
        private string ReturnCardReturnFee = "0";
        /// <summary>
        /// �Ƿ��Զ���ӡ
        /// </summary>
        private bool isAutoPrint = false;

        /// <summary>
        /// �Ƿ���ʾ������
        /// </summary>
        private bool isShowMarkNo = false;
        /// <summary>
        /// �Զ���ӡʱ��Щ�������Զ���ӡ���ԡ�;����β
        /// </summary>
        private string printCardType = "";

        private bool blnIDCardNoOnly = false;

        /// <summary>
        /// �Ƿ�������֤���ж��Ƿ�����
        /// </summary>
        private bool isJudgeByIDCard = false;
        /// <summary>
        /// �Ƿ�д��Card_No
        /// </summary>
        private bool isWriteCardNo = false;
        /// <summary>
        /// �Ƿ���֤�����Ƿ���Ͽ����͹���
        /// </summary>
        private bool isMatchCardTypeRole = true;
        /// <summary>
        /// �Ƿ�����֤���жϼ���������Ϣ
        /// </summary>
        private bool isQueryPatientInfoByReadIDCard = false;

        /// <summary>
        /// ������Ϊ�յı���Ŀ�����,�ԡ�;����β"
        /// </summary>
        private string allowNoCardSaveType = "";

        private NeuObject cardTypeObj = new NeuObject();

        /// <summary>
        /// �쿨��ǩ��ӡ�ӿ�
        /// </summary>
        IPrintLable iPrintLable = null;

        /// <summary>
        /// �����շ�ƾ֤��ӡ�ӿ�
        /// </summary>
        IPrintCardFee iPrint = null;

        /// <summary>
        /// ���Ʒ���ƾ֤��ӡ�ӿ�
        /// </summary>
        IPrintReturnCardFee iPrintReturn = null;

        private bool afterSaveClose = false;

        /// <summary>
        /// ����ɹ��Ƿ���ʾ����ɹ�
        /// </summary>
        private bool isShowTipsWhenSaveSucess = true;

        /// <summary>
        /// ����ɹ��Ƿ���ʾ����ɹ�
        /// </summary>
        public bool IsShowTipsWhenSaveSucess
        {
            get { return isShowTipsWhenSaveSucess; }
            set { isShowTipsWhenSaveSucess = value; }
        }

        #endregion

        #region ����

        /// <summary>
        /// �������Ƿ�����޸�
        /// </summary>
        private bool isCanChangeCardType = false;

        /// <summary>
        /// �Ƿ�������֤���ж��Ƿ�����
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�������֤���ж��Ƿ�����")]
        public bool IsJudgeByIDCard
        {
            get
            {
                return isJudgeByIDCard;
            }
            set
            {
                isJudgeByIDCard = value;
            }
        }

        /// <summary>
        /// �Ƿ�����֤���жϼ���������Ϣ
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�����֤���жϼ���������Ϣ")]
        public bool IsQueryPatientInfoByReadIDCard
        {
            get
            {
                return isQueryPatientInfoByReadIDCard;
            }
            set
            {
                isQueryPatientInfoByReadIDCard = value;
            }
        }

        [Category("�ؼ�����"), Description("�Ƿ���֤�����Ƿ���Ͽ����͹���")]
        public bool IsMatchCardTypeRole
        {
            get
            {
                return isMatchCardTypeRole;
            }
            set
            {
                isMatchCardTypeRole = value;
            }
        }

        private bool isJumpHomePhone = false;
        [Category("�ؼ�����"), Description("�Ƿ������ͥ��ַ��ֱ�������绰�ֶ�")]
        public bool IsJumpHomePhone
        {
            get { return this.ucRegPatientInfo1.IsJumpHomePhone; }
            set { this.ucRegPatientInfo1.IsJumpHomePhone = value; }
        }

        /// <summary>
        /// �������Ƿ�����޸�
        /// </summary>
        [Category("�ؼ�����"), Description("�������Ƿ�����޸�")]
        public bool IsCanChangeCardType
        {
            get
            {
                return isCanChangeCardType;
            }
            set
            {
                isCanChangeCardType = value;
                this.cmbMarkType.Enabled = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ������
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ���ʾ������")]
        public bool IsShowMarkNo
        {
            get
            {
                return isShowMarkNo;
            }
            set
            {
                isShowMarkNo = value;
            }
        }

        #region �����������
        [Category("��������"), Description("�����Ƿ�������룡")]
        public bool IsInputName
        {
            get
            {
                return this.ucRegPatientInfo1.IsInputName;
            }
            set
            {
                this.ucRegPatientInfo1.IsInputName = value;
            }
        }

        [Category("�޸Ŀ���"), Description("�Ƿ��жϱ�Ժְ��")]
        public string IsValidHospitalStaff
        {
            get
            {
                return this.ucRegPatientInfo1.IsValidHospitalStaff;
            }
            set
            {
                this.ucRegPatientInfo1.IsValidHospitalStaff = value;
            }
        }

        [Category("��������"), Description("�Ա��Ƿ�������룡")]
        public bool IsInputSex
        {
            get
            {
                return this.ucRegPatientInfo1.IsInputSex;
            }
            set
            {
                this.ucRegPatientInfo1.IsInputSex = value;
            }
        }

        [Category("��������"), Description("��������Ƿ�������룡")]
        public bool IsInputPact
        {
            get
            {
                return this.ucRegPatientInfo1.IsInputPact;
            }
            set
            {
                this.ucRegPatientInfo1.IsInputPact = value;
            }
        }

        [Category("��������"), Description("ҽ��֤���Ƿ�������룡")]
        public bool IsInputSiNo
        {
            get 
            {
                return this.ucRegPatientInfo1.IsInputSiNo; 
            }
            set 
            {
                this.ucRegPatientInfo1.IsInputSiNo = value;
            }
        }

        [Category("��������"), Description("���������Ƿ�������룡")]
        public bool IsInputBirthDay
        {
            get 
            {
                return this.ucRegPatientInfo1.IsInputBirthDay; 
            }
            set
            {
                this.ucRegPatientInfo1.IsInputBirthDay = value;
            }
        }

        [Category("��������"), Description("֤�������Ƿ�������룡")]
        public bool IsInputIDEType
        {
            get 
            { 
                return this.ucRegPatientInfo1.IsInputIDEType; 
            }
            set
            {
                this.ucRegPatientInfo1.IsInputIDEType = value;
            }
        }

        [Category("��������"), Description("֤�����Ƿ�������룡")]
        public bool IsInputIDENO
        {
            get 
            {
                return this.ucRegPatientInfo1.IsInputIDENO; 
            }
            set
            {
                this.ucRegPatientInfo1.IsInputIDENO = value;
            }
        }

        [Category("��������"), Description("�绰�Ƿ�������룡")]
        public bool IsInputPHONE
        {
            get
            {
                return this.ucRegPatientInfo1.IsInputPHONE;
            }
            set
            {
                this.ucRegPatientInfo1.IsInputPHONE = value;
            }
        }

        [Category("��������"), Description("�Ƿ�����->��ַ->�绰->�������룡")]
        public bool IsInSequentialOrder
        {
            get
            {
                return this.ucRegPatientInfo1.IsInSequentialOrder;
            }
            set
            {
                this.ucRegPatientInfo1.IsInSequentialOrder = value;
            }
        }

        /// <summary>
        /// ����ͬʱΪ����Ŀ  0 = ������ 1 = ���� ���֤��绰����
        /// </summary>
        [Category("�޸Ŀ���"), Description("����ͬʱΪ����Ŀ  0 = ������ 1 = ���� ���֤��绰����")]
        public int IMustInpubByOne
        {
            get { return this.ucRegPatientInfo1.IMustInpubByOne; }
            set { this.ucRegPatientInfo1.IMustInpubByOne = value; }

        }

        #endregion

        [Category("�ؼ�����"), Description("�Ƿ��ձ�¼����ת���뽹�� True:�� False:��")]
        public bool IsMustInputTabIndex
        {
            get
            {
                return this.ucRegPatientInfo1.IsMustInputTabIndex;
            }
            set
            {
                this.ucRegPatientInfo1.IsMustInputTabIndex = value;
            }
        }

        [Category("�ؼ�����"),Description("������������Ƿ�̬���һ�����Ϣ True:�� False:��")]
        public bool IsSelectPatientByEnter
        {
            get 
            {
                isSelectPatientByEnter = this.ucRegPatientInfo1.IsSelectPatientByNameIDCardByEnter;
                return isSelectPatientByEnter;
            }
            set 
            { 
                isSelectPatientByEnter = value;
                this.ucRegPatientInfo1.IsSelectPatientByNameIDCardByEnter = isSelectPatientByEnter;
            }
        }

        /// <summary>
        /// �����Ƿ�ֻ�ڱ��ش��������������ķ���
        /// {BCE8D830-5FEA-4681-A08A-4BB48D172E20}
        /// </summary>
        [Category("�ؼ�����"), Description("�����Ƿ�ֻ�ڱ��ش��������������ķ��� True:�� False:��")]
        public bool IsLocalOperation
        {
            get
            {
                return isLocalOperation;
            }
            set
            {
                isLocalOperation = value;
            }
        }

        [Category("�ؼ�����"), Description("false:���￨���������� true:���￨���������Ų�ͬ")]
        public bool CardWay
        {
            get
            {
                return this.ucRegPatientInfo1.CardWay;
            }
            set
            {
                this.ucRegPatientInfo1.CardWay = value;
            }
        }
        /// <summary>
        /// ֤����Ψһ�Ե���ʾ
        /// </summary>
        [Category("�ؼ�����"), Description("֤����Ψһ�Ե���ʾ")]
        public bool BlnIDCardNoOnly
        {
            get { return blnIDCardNoOnly; }
            set { blnIDCardNoOnly = value; }
        }

        [Category("��������"), Description("һ�����ͬ��λ")]
        public bool IsMutilPactInfo
        {
            get
            {
                return this.ucRegPatientInfo1.IsMutilPactInfo;
            }
            set
            {
                this.ucRegPatientInfo1.IsMutilPactInfo = value;
            }
        }

        [Description("�Ƿ��Զ���ӡ��ǩ��"), Category("����")]
        public bool IsAutoPrint
        {
            get
            {
                return isAutoPrint;
            }
            set
            {
                this.isAutoPrint = value;
            }
        }

        [Description("�Զ���ӡʱ��Щ�������Զ���ӡ���ԡ�;����β"), Category("����")]
        public string PrintCardType
        {
            get
            {
                return printCardType;
            }
            set
            {
                this.printCardType = value;
            }
        }

        /// <summary>
        /// ������Ϊ�յı���Ŀ����ͣ��ԡ�;����β
        /// </summary>
        [Description("������Ϊ�յı���Ŀ����ͣ��ԡ�;����β"), Category("����")]
        public string AllowNoCardSaveType
        {
            get
            {
                return allowNoCardSaveType;
            }
            set
            {
                this.allowNoCardSaveType = value;
            }
        }

        [Description("�Ƿ���ʾ���ߵ��հ쿨�б�"), Category("����")]
        public bool IsShowPatientIndaySheet
        {
            get
            {
                return this.spPatientInDay.Visible;
            }
            set
            {
                this.spPatientInDay.Visible = value;
            }
        }

        private bool isNewAccount = false;
        [Description("�Ƿ��ڰ쿨��ͬʱ������Ա�˺�"), Category("����")]
        public bool IsNewAccount
        {
            get
            {
                return isNewAccount;
            }
            set
            {
                isNewAccount = value;
            }
        }


        private string memberCardType = string.Empty;
        [Description("��Ա�������ͣ��ԡ�;����β"), Category("����")]
        public string MemberCardType
        {
            get
            {
                return this.memberCardType;
            }
            set
            {
                this.memberCardType = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʾ��ʾ��Ϣ
        /// </summary>
        /// <param name="consList">��ʾ��Ϣ����</param>
        private void DealConstantList(ArrayList consList)
        {
            if (consList == null || consList.Count <= 0)
            {
                return;
            }

            this.spInfo.RowCount = 0;
            this.spInfo.RowCount = (consList.Count / 3) + (consList.Count % 3 == 0 ? 0 : 1);

            int row = 0;
            int col = 0;

            foreach (FS.FrameWork.Models.NeuObject obj in consList)
            {
                if (col >= 5)
                {
                    col = 0;
                    row++;
                }

                this.spInfo.SetValue(row, col, obj.ID);
                this.spInfo.SetValue(row, col + 1, obj.Name);

                col = col + 2;
            }
        }

        /// <summary>
        /// ��֤����
        /// </summary>
        /// <returns>true:�ɹ���falseʧ��</returns>
        private bool Valid()
        {
            FrameWork.Models.NeuObject MarkType = this.cmbMarkType.SelectedItem as FrameWork.Models.NeuObject;
            //�������û�п��ű���Ͳ��ж��Ƿ��п���
            if (!AllowNoCardSaveType.Contains(MarkType.ID))
            {
                if (this.txtMarkNo.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("�����뿨�ţ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtMarkNo.Focus();
                    return false;
                }
            }

            if (string.IsNullOrEmpty(MarkType.ID))
            {
                MessageBox.Show("��ѡ�����ͣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cmbMarkType.Focus();
                return false;
            }

            if (this.cmbMarkType.Tag == null || this.cmbMarkType.Tag.ToString() == string.Empty)
            {
                MessageBox.Show("�����뿨�ź�س�ȷ�ϣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtMarkNo.Focus();
                this.txtMarkNo.SelectAll();
                return false;
            }

            this.IsInputIDEType = false;
            this.IsInputIDENO = false;

            if ( string.IsNullOrEmpty(markTypeHelp.GetName(this.cmbMarkType.Tag.ToString())) )
            {
                MessageBox.Show("�����뿨�ź�س�ȷ�ϣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtMarkNo.Focus();
                this.txtMarkNo.SelectAll();
                return false;
            }

            if (markTypeHelp.GetName(this.cmbMarkType.Tag.ToString()).Contains("����"))
            {
                this.IsInputIDEType = true;
                this.IsInputIDENO = true;
            }

            if (!FS.FrameWork.Public.String.ValidMaxLengh(txtMarkNo.Text.Trim(), 30))
            {
                MessageBox.Show("���￨�Ź�����������������￨�ţ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtMarkNo.Focus();
                this.txtMarkNo.SelectAll();
                return false;
            }

            if (isMatchCardTypeRole)
            {
                //����֤���ŵĹ���
                ArrayList cardTypeList = constManager.GetList("MarkType");
                FS.HISFC.Models.Base.Const cardTypeRole = null;
                foreach (FS.HISFC.Models.Base.Const markType in cardTypeList)
                {
                    if (MarkType.ID.Equals(markType.ID))
                    {
                        cardTypeRole = markType;
                        break;
                    }
                }
                Regex regex = new Regex(@cardTypeRole.UserCode, RegexOptions.Multiline);
                if (!regex.IsMatch(txtMarkNo.Text))
                {
                    MessageBox.Show("���Ų����ϸÿ����͹������޸Ŀ���!\n" + cardTypeRole.Name + "����Ϊ��" + cardTypeRole.Memo, "����");
                    this.txtMarkNo.Text = "";
                    this.txtMarkNo.Focus();
                    this.txtMarkNo.SelectAll();
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(this.txtMarkNo.Text) && (string.IsNullOrEmpty(this.cmbMemCardType.Tag.ToString()) || string.IsNullOrEmpty(this.cmbMemCardType.Text)))
            {// {AE74D7CC-B573-48a7-9EAD-60340E3F01C9}
                MessageBox.Show("��ѡ���Ա���ȼ���");
                this.cmbMemCardType.Focus();
                this.cmbMemCardType.SelectAll();
                return false; ;
            }
            AccountCard card = this.accountManager.GetAccountCard(txtMarkNo.Text.Trim(), this.cmbMarkType.Tag.ToString());

            //accountCard = new FS.HISFC.Models.Account.AccountCard();
            //int resultValue = feeManage.ValidMarkNO(medicalCardNo, ref accountCard);

            if (card != null)
            {
                MessageBox.Show("�ÿ��ѱ���������ʹ�ã��뻻����", "����");
                this.txtMarkNo.Focus();
                this.txtMarkNo.SelectAll();
                return false;
            }
            if (isJudgeByIDCard)
            {
                FS.HISFC.Models.RADT.PatientInfo patient = this.ucRegPatientInfo1.GetPatientInfomation();
                if (patient.IDCard !="" && patient.IDCard!=null && patient.PID.CardNO == "")
                {
                    FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                    int i = this.accountManager.GetPatientInfoByIDCard(patient.IDCard);
                    if (i >0)
                    {
                        DialogResult digRreslut = MessageBox.Show("�û������֤���Ѱ�������Ƿ����?", "��ʾ", MessageBoxButtons.YesNo,MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        {
                            if (digRreslut == DialogResult.No)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// ��������ʹ�õľ��￨
        /// </summary>
        /// <returns>1���ϳɹ���0���������ϲ�����-1ʧ��</returns>
        private int StopPatientCard()
        {
            string tempCardNO = this.ucRegPatientInfo1.CardNO;
            //��tempCardNOΪ�յ�ʱ����������������������Ϊ�յ�ʱ���ǲ����¿�
            //����������������ʱ����½���CardNO�γ��µĻ�����Ϣ
            //�ڲ�����ʱ��ֻ���»�����Ϣ
            if (string.IsNullOrEmpty(tempCardNO)) return 0;
            //���һ�������ʹ�õĿ��ļ���
            //List<AccountCard> list = accountManager.GetMarkList(tempCardNO, this.cardTypeObj.ID, "1");

            //ԭ��������ѯ�����������⣬��ʱ����ȫ��������
            List<AccountCard> list = accountManager.GetMarkList(tempCardNO, "Account_CARD", "1");
            if (list.Count == 0) return 0;
            DialogResult digRreslut = MessageBox.Show("�Ƿ�ͣ������ʹ�õľ��￨��", "��ʾ", MessageBoxButtons.OKCancel);
            if (digRreslut == DialogResult.Cancel) return 0;   
            ucCancelMark uc = new ucCancelMark(list);

            uc.IsAcceptCardFee = this.IsAcceptCardFee;
            uc.IsAcceptChangeCardFee = this.IsAcceptChangeCardFee;
            uc.ReturnCardReturnFee = this.ReturnCardReturnFee;

            uc.StopCardEvent+=new ucCancelMark.EventStopCard(uc_StopCardEvent);
            FS.FrameWork.WinForms.Classes.Function.ShowControl(uc);
            if (uc.FindForm().DialogResult == DialogResult.No) return 0;
            if (uc.FindForm().DialogResult == DialogResult.Cancel) return -1;
            return 1;
            
        }

        /// <summary>
        /// ͣ�þ��￨
        /// </summary>
        /// <param name="markList">������</param>
        /// <param name="lstCardFee">������</param>
        /// <returns></returns>
        private bool uc_StopCardEvent(List<AccountCard> markList)
        {
            int resultValue = 0;

            AccountCardRecord tempCardRecord = null;
            
            foreach (AccountCard tempAccountCard in markList)
            {
                //�޸Ŀ�״̬
                if (tempAccountCard.MarkStatus == MarkOperateTypes.Stop)
                {
                    resultValue = accountManager.StopAccountCard(tempAccountCard);
                }
                else if (tempAccountCard.MarkStatus == MarkOperateTypes.Cancel)
                {
                    resultValue = accountManager.BackAccountCard(tempAccountCard);
                }
                else
                {
                    resultValue = accountManager.UpdateAccountCardState(tempAccountCard.MarkNO, tempAccountCard.MarkType, false);
                }
                if (resultValue < 0)
                {
                    MessageBox.Show("���ϻ��߾��￨ʧ�ܣ�" + accountManager.Err, "��ʾ");
                    return false;
                }

                #region �γɿ�������¼
                tempCardRecord = new AccountCardRecord();
                tempCardRecord.CardNO = tempAccountCard.Patient.PID.CardNO;//���￨��
                tempCardRecord.MarkNO = tempAccountCard.MarkNO;//���￨��
                tempCardRecord.MarkType = tempAccountCard.MarkType; //������
                tempCardRecord.OperateTypes.ID = (int)MarkOperateTypes.Cancel; //��������
                tempCardRecord.Oper.ID = accountManager.Operator.ID; //������
                #endregion
                //�γɲ�����¼
                resultValue = accountManager.InsertAccountCardRecord(tempCardRecord);
                if (resultValue < 0)
                {
                    MessageBox.Show("���뿨������¼ʧ�ܣ�" + accountManager.Err);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// ��������
        /// </summary>
        protected virtual void Save()
        {
            if (!this.Valid())
            {
                //this.ClearData();
                return;
            }

            //��ͨ���߾��￨����
            if (!this.ucRegPatientInfo1.IsTreatment)
            {
                if (!this.ucRegPatientInfo1.InputValid())
                {
                    //this.ClearData();
                    return;
                }

            }

            this.ucRegPatientInfo1.IsAutoBuildCardNo = false;
            //�������û�п��ű������ȡCard_No,��card_no��Ϊmark_no
            
            FrameWork.Models.NeuObject MarkType = this.cmbMarkType.SelectedItem as FrameWork.Models.NeuObject;
            if (AllowNoCardSaveType.Contains(MarkType.ID))
            {
                string cardNo = "";
                accountCard = new AccountCard();
                string markNO = this.txtMarkNo.Text.Trim();

                if (this.ucRegPatientInfo1.CardNO != null && this.ucRegPatientInfo1.CardNO != "")
                {
                    cardNo = this.ucRegPatientInfo1.CardNO;
                }
                else
                {
                    cardNo = outpatientManager.GetAutoCardNO();
                }

                cardNo = cardNo.PadLeft(10, '0');
                if (string.IsNullOrEmpty(markNO))
                {
                    accountCard.MarkNO = cardNo;
                    this.txtMarkNo.Text = cardNo;
                }
                else
                {
                    accountCard.MarkNO = markNO;
                }
                this.ucRegPatientInfo1.IsAutoBuildCardNo = true;
                this.ucRegPatientInfo1.AutoCardNo = cardNo;

                accountCard.MarkType.ID = MarkType.ID;
                accountCard.Memo = "2";
                this.txtMarkNo.Text = accountCard.MarkNO.ToString();

                this.cmbMarkType.Tag = MarkType.ID;


                if (string.IsNullOrEmpty(this.cmbMarkType.Tag.ToString()))
                {
                    this.cmbMarkType.SelectedIndex = 0;
                    MessageBox.Show("�����ʹ������������뿨�ţ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                AccountCard card = this.accountManager.GetAccountCard(txtMarkNo.Text.Trim(), this.cmbMarkType.Tag.ToString());
                if (card != null)
                {
                    MessageBox.Show("�û�������Ժ�ڿ������򼴿ɣ�", "��ʾ");
                    this.txtMarkNo.Focus();
                    this.txtMarkNo.SelectAll();
                    return;
                }
            }

            string strMsg = string.Empty;

            if (blnIDCardNoOnly)
            {
                string strIDCardType = string.Empty;
                string strIDCardNo = string.Empty;
                string strCardNo = string.Empty;
                this.ucRegPatientInfo1.GetIdCardInfo(out strIDCardType, out strIDCardNo, out strCardNo);

                if (!string.IsNullOrEmpty(strIDCardType) && !string.IsNullOrEmpty(strIDCardNo))
                {
                    //���һ�����Ϣ
                    List<AccountCard> list = accountManager.GetAccountCard("", "", "", "", strIDCardType, strIDCardNo, "");

                    if (list != null && !string.IsNullOrEmpty(strCardNo))
                    {
                        for (int idx = 0; idx < list.Count; idx++)
                        {
                            if (list[idx].Patient.PID.CardNO == strCardNo)
                            {
                                list.RemoveAt(idx);
                                idx--;
                            }
                        }
                    }

                    if (list != null && list.Count > 0)
                    {
                        strMsg = Language.Msg("������ͬ ��֤���š� ��¼�Ƿ������");
                        if (MessageBox.Show(strMsg, "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
            }


            // ��ʷ����¼
            List<AccountCard> lstHistoryCard = null;

            // ����ͣ���˿��б�
            List<AccountCard> lstStopBackCard = null;

            // ��¼������
            List<AccountCardFee> lstCardFee = null;

            // ��ǰ����Ա
            FS.HISFC.Models.Base.Employee currentOperator = accountManager.Operator as FS.HISFC.Models.Base.Employee;
            DateTime nowTime = FS.FrameWork.Function.NConvert.ToDateTime(accountManager.GetSysDateTime());

            #region ����ͣ���˿�����
            string tempCardNO = this.ucRegPatientInfo1.CardNO;
            //��tempCardNOΪ�յ�ʱ����������������������Ϊ�յ�ʱ���ǲ����¿�
            //����������������ʱ����½���CardNO�γ��µĻ�����Ϣ
            //�ڲ�����ʱ��ֻ���»�����Ϣ
            if (!string.IsNullOrEmpty(tempCardNO))
            {
                //ԭ��������ѯ�����������⣬��ʱ����ȫ��������
                lstHistoryCard = accountManager.GetMarkList(tempCardNO, "Account_CARD", "1");
                if (lstHistoryCard != null && lstHistoryCard.Count > 0)
                {
                    DialogResult digRreslut = MessageBox.Show("�Ƿ�ͣ������ʹ�õľ��￨��", "��ʾ", MessageBoxButtons.YesNoCancel);
                    if (digRreslut == DialogResult.Yes)
                    {
                        ucCancelMark uc = new ucCancelMark(lstHistoryCard);
                        FS.FrameWork.WinForms.Classes.Function.ShowControl(uc);
                        if (uc.FindForm().DialogResult == DialogResult.OK)
                        {
                            lstStopBackCard = uc.lstCard;
                        }
                        else if (uc.FindForm().DialogResult == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                    else if (digRreslut == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }

            #endregion

            #region ����ʵ�崦��

            accountCard = new FS.HISFC.Models.Account.AccountCard();
            accountCard.MarkNO = this.txtMarkNo.Text.Trim();
            accountCard.AccountLevel.ID = this.cmbMemCardType.Tag.ToString();// {AE74D7CC-B573-48a7-9EAD-60340E3F01C9}
            accountCard.MarkType = this.cmbMarkType.SelectedItem as FrameWork.Models.NeuObject;
            accountCard.MarkStatus = MarkOperateTypes.Begin;

            if (lstHistoryCard != null && lstHistoryCard.Count > 0)
            {
                accountCard.ReFlag = "1";

                if (this.IsAcceptCardFee == "2")
                {
                    // �ȵ����·������ҵ���ͬ���͵Ŀ���Ϊ������
                    accountCard.ReFlag = "0";
                    foreach (AccountCard temp in lstHistoryCard)
                    {
                        if (temp.MarkType.ID == accountCard.MarkType.ID)
                        {
                            accountCard.ReFlag = "1";
                            break;
                        }
                    }
                }
            }
            else
            {
                accountCard.ReFlag = "0";
            }
            accountCard.CreateOper.ID = currentOperator.ID;
            accountCard.CreateOper.OperTime = nowTime;

            #endregion

            lstCardFee = new List<AccountCardFee>();

            #region �����Ʒ�ʵ�崦��

            if ((accountCard.ReFlag == "0" && this.IsAcceptCardFee != "0") || (accountCard.ReFlag == "1" && this.IsAcceptChangeCardFee != "0"))
            {
                AccountCardFee cardFee = new AccountCardFee();
                cardFee.InvoiceNo = "";
                cardFee.Print_InvoiceNo = "";
                cardFee.CardNo = "";
                cardFee.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                cardFee.MarkNO = accountCard.MarkNO;
                cardFee.MarkType = accountCard.MarkType;

                FS.HISFC.Models.Base.Const obj = cardFee.MarkType as FS.HISFC.Models.Base.Const;
                if (obj != null)
                {
                    //UserCode�����ڿ�������֤,����SortID
                    cardFee.Tot_cost = FS.FrameWork.Function.NConvert.ToDecimal(obj.SortID);
                }

                cardFee.Own_cost = cardFee.Tot_cost;

                cardFee.FeeOper.ID = currentOperator.ID;
                cardFee.FeeOper.OperTime = nowTime;

                cardFee.Oper.ID = currentOperator.ID;
                cardFee.Oper.OperTime = nowTime;

                cardFee.IsBalance = false;
                cardFee.BalanceNo = "";
                cardFee.BalnaceOper.ID = "";

                cardFee.FeeType = AccCardFeeType.CardFee;

                cardFee.IStatus = 1;

                if (cardFee.Tot_cost > 0)
                {
                    lstCardFee.Add(cardFee);
                }
            }
            #endregion

            strMsg = string.Empty;
            decimal decMoney = 0;
            decimal decTotalMoney = 0;
            int iRes = 0;

            #region �����˿��˷�����

            if (ReturnCardReturnFee == "1" && lstStopBackCard != null && lstStopBackCard.Count > 0)
            {
                List<AccountCardFee> lstTempCardFee = null;
                AccountCardFee tempCardFee = null;
                foreach (AccountCard backCard in lstStopBackCard)
                {
                    tempCardFee = null;
                    decMoney = 0;

                    if (backCard.MarkStatus == MarkOperateTypes.Cancel)
                    {
                        strMsg += "\t��" + backCard.MarkType.Name + "��" + backCard.MarkNO + "��\r\n";

                        iRes = accountManager.QueryAccountCardFee(backCard.Patient.PID.CardNO, backCard.MarkNO, backCard.MarkType.ID, out lstTempCardFee);
                        if (iRes > 0 && lstTempCardFee != null && lstTempCardFee.Count > 0)
                        {
                            for (int idx = 0; idx < lstTempCardFee.Count; idx++)
                            {
                                if (lstTempCardFee[idx].IStatus != 1)
                                    continue;

                                decMoney = lstTempCardFee[idx].Tot_cost;
                                if (decMoney <= 0)
                                    continue;

                                tempCardFee = new AccountCardFee();
                                tempCardFee.InvoiceNo = lstTempCardFee[idx].InvoiceNo;
                                tempCardFee.Print_InvoiceNo = lstTempCardFee[idx].Print_InvoiceNo;
                                tempCardFee.Patient = backCard.Patient;

                                tempCardFee.ClinicNO = lstTempCardFee[idx].ClinicNO;

                                tempCardFee.MarkNO = backCard.MarkNO;
                                tempCardFee.MarkType = backCard.MarkType.Clone();
                                tempCardFee.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                                tempCardFee.Tot_cost = -decMoney;

                                tempCardFee.Own_cost = -lstTempCardFee[idx].Own_cost;
                                tempCardFee.Pay_cost = -lstTempCardFee[idx].Pay_cost;
                                tempCardFee.Pub_cost = -lstTempCardFee[idx].Pub_cost;


                                tempCardFee.FeeOper = lstTempCardFee[idx].FeeOper;

                                tempCardFee.Oper.ID = currentOperator.ID;
                                tempCardFee.Oper.Name = currentOperator.Name;
                                tempCardFee.Oper.OperTime = nowTime;

                                tempCardFee.IsBalance = false;
                                tempCardFee.BalanceNo = "";
                                tempCardFee.IStatus = 1;

                                tempCardFee.FeeType = AccCardFeeType.CardFee;

                                lstCardFee.Add(tempCardFee);
                            }
                        }
                    }

                    decTotalMoney += decMoney;
                }
            }

            if (!string.IsNullOrEmpty(strMsg))
            {
                if (MessageBox.Show("���ջ����¿���\r\n" + strMsg, "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    return;
                }
            }

            #endregion

            #region ��������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            #endregion

            #region  ���滼����Ϣ
            int resultValue = 0;
            this.ucRegPatientInfo1.McardNO = txtMarkNo.Text;
            resultValue = this.ucRegPatientInfo1.Save();
            if (resultValue <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return;
            }
            #endregion

            //#region ��������ʹ�õľ��￨
            //if (StopPatientCard() < 0)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    return;
            //}
            //#endregion

            #region ����

            #region ��ʵ�岹��

            accountCard.Patient = this.ucRegPatientInfo1.GetPatientInfomation();

            #endregion
            //����������
            string error = string.Empty;
            resultValue = this.BulidCard(accountCard);
            if (resultValue == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(error, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            #region ͣ���˿�����

            if (lstStopBackCard != null && lstStopBackCard.Count > 0)
            {
                for (int idx = 0; idx < lstStopBackCard.Count; idx++)
                {
                    if (lstStopBackCard[idx].MarkStatus == MarkOperateTypes.Stop)
                    {
                        lstStopBackCard[idx].StopOper.ID = currentOperator.ID;
                        lstStopBackCard[idx].StopOper.Name = currentOperator.Name;
                        lstStopBackCard[idx].StopOper.OperTime = nowTime;

                        resultValue = accountManager.StopBackAccountCard(lstStopBackCard[idx]);
                    }
                    else if (lstStopBackCard[idx].MarkStatus == MarkOperateTypes.Cancel)
                    {
                        lstStopBackCard[idx].BackOper.ID = currentOperator.ID;
                        lstStopBackCard[idx].BackOper.Name = currentOperator.Name;
                        lstStopBackCard[idx].BackOper.OperTime = nowTime;

                        resultValue = accountManager.StopBackAccountCard(lstStopBackCard[idx]);
                    }

                    if (resultValue == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(accountManager.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            #endregion

            #region �����ò���

            decimal totalCardFee = 0m;

            if (lstCardFee != null && lstCardFee.Count > 0)
            {
                AccountCardFee cardFee = null;

                for (int idx = 0; idx < lstCardFee.Count; idx++)
                {
                    cardFee = lstCardFee[idx];

                    cardFee.Patient = accountCard.Patient;

                    resultValue = this.feeManage.SaveAccountCardFee(ref cardFee, false);

                    if (resultValue == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(feeManage.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        totalCardFee += cardFee.Tot_cost;
                    }

                    lstCardFee[idx] = cardFee;
                }
            }

            #endregion

            #region ����������
            if (accountCard.MarkType.Name.Contains("����"))
            {
                //����Ǳ�Ժ���ŵĽ�����
                if (accountCard.MarkNO.StartsWith(healthCard.UnitCode))
                {

                    healthCard.DepartmentCode = currentOperator.Dept.ID;
                    healthCard.CardNumber = accountCard.MarkNO;
                    healthCard.Name = accountCard.Patient.Name;
                    if (accountCard.Patient.Sex.ID.ToString() == "M")
                    {
                        healthCard.Sex = "1";
                    }
                    else if (accountCard.Patient.Sex.ID.ToString() == "F")
                    {
                        healthCard.Sex = "2";
                    }
                    else
                    {
                        healthCard.Sex = "9";
                    }
                    FS.HISFC.Models.Base.Const cons;
                    if (accountCard.Patient.IDCardType != null)
                    {
                        cons = this.constManager.GetConstant("IDCard", accountCard.Patient.IDCardType.ID) as FS.HISFC.Models.Base.Const;
                        healthCard.IdType = cons.UserCode;
                    }

                    healthCard.Id = accountCard.Patient.IDCard;
                    healthCard.Birthday = accountCard.Patient.Birthday.ToString("yyyyMMdd");
                    healthCard.Phone = accountCard.Patient.PhoneHome;
                    healthCard.Mphone = accountCard.Patient.Kin.RelationPhone;
                    //healthCard.Province = accountCard.Patient.DIST;
                    //healthCard.City = accountCard.Patient.DIST;
                    //healthCard.Section = accountCard.Patient.AreaCode;
                    healthCard.Address = accountCard.Patient.AddressHome;
                    healthCard.IdAddress = accountCard.Patient.AddressHome;
                    if (accountCard.Patient.Profession != null)
                    {
                        cons = this.constManager.GetConstant("PROFESSION", accountCard.Patient.Profession.ID) as FS.HISFC.Models.Base.Const;
                        healthCard.Profession = cons.UserCode;
                    }
                    byte[] b = null;
                    if (this.accountManager.GetIdenInfoPhoto(accountCard.Patient.PID.CardNO, accountCard.Patient.IDCardType.ID, ref b) > 0 && b != null)
                    {
                        healthCard.Photo = b;
                    }
                    //�Ѿ����ڸ����֤�Ŀ��ţ�������콡������ֻ�ܰ�Ժ�ڿ�
                    if (healthCardManager.HadCard(healthCard) == 4)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ƽ̨�Ѵ��ڸû��ߵİ쿨��Ϣ��������콡�����������Ժ�ھ��￨", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.txtMarkNo.Focus();
                        this.txtMarkNo.SelectAll();
                        this.txtMarkNo.Text = "";
                        this.cmbMarkType.SelectedIndex = 0;
                        return;
                    }

                    bool uploadSuccess = false;

                    //ֱ�Ӱ����¿����ϴ��ɹ�
                    if (healthCardManager.CreateCard(healthCard) == 3)
                    {
                        uploadSuccess = true;
                    }

                    //�ϴ�����ƽ̨���ɹ��Ļ������¿��ϴ����Ϊ2(����ƽ̨�ϴ����ɹ�)��
                    if (!uploadSuccess)
                    {
                        //accountManager.UpdateAccountCardState(accountCard.MarkNO, accountCard.MarkType.ID, "2");
                        accountManager.UpdateAccountCardUploadFlag(accountCard.Patient.PID.CardNO, accountCard.MarkNO, accountCard.MarkType.ID, "2");
                    }
                }
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();
            //д�벡���� lfhm// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            if (this.cmbMarkType.Tag.ToString() != "Card_No")
            {
                if (Function.WriterCardNo(this.ucRegPatientInfo1.CardNO, ref error) == -1)
                {
                    //CommonController.Instance.MessageBox(error, MessageBoxIcon.Error);
                    MessageBox.Show("д�뿨��ʧ��" + error);

                    return;
                }
                //isWriteCardNo = false;
            }

            // ��ӡ������ƾ֤
            PrintCardFee(lstCardFee);

           
            #region ��ʾ�ɹ�

            string register = this.controlParma.GetControlParam<string>("MZ9951", false, "0");
            if (register == "0")
            {
                if (totalCardFee > 0 && accountCard.ReFlag == "1" && this.IsAcceptChangeCardFee != "0")
                {
                    MessageBox.Show("��ȡ�������ã�" + FS.FrameWork.Public.String.FormatNumberReturnString(totalCardFee, 2) + "(Ԫ)");
                }

                if (this.IsShowTipsWhenSaveSucess)
                {
                    //{DA6D31A6-CD0D-4b70-87EC-9F5EB5923512}
                    //{9043E842-7A83-4213-AB6D-E7E04B2EF76E} lfhm
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�������ݳɹ���\r\n" + "���߲�����Ϊ:\"" + this.ucRegPatientInfo1.CardNO + "\""), FS.FrameWork.Management.Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //{DF60E480-46A6-4dac-B3CC-BDD96E77CE52}
                    //��ӡ��ǩ
                    if (this.isAutoPrint && printCardType.Contains(accountCard.MarkType.ID))
                    {
                        if (MessageBox.Show("�Ƿ��ӡ���룡", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            PrintLable();
                        }
                    }
                }
            }
            else
            {
                this.save();
            }

            #endregion

            #region ��Ա�˻�����

            if (this.isNewAccount && MarkType.ID == this.memberCardType)
            {
                if (this.NewAccount() == 0)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("������Ա�˻��ɹ���"), FS.FrameWork.Management.Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //isWriteCardNo = true;
            }

            #endregion

            if (afterSaveClose)
            {
                this.Tag = txtMarkNo.Text;
                this.FindForm().DialogResult = DialogResult.OK;
            }
            else
            {
                this.ClearData();
            }
        }
        /// <summary>
        /// �޸Ļ�����Ϣ
        /// </summary>
        protected virtual void ModifyPatientInfo()
        {
            //{9043E842-7A83-4213-AB6D-E7E04B2EF76E} lfhm

            HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            patientInfo = managerIntegrate.QueryComPatientInfo(this.ucRegPatientInfo1.CardNO);
            if (string.IsNullOrEmpty(patientInfo.PID.CardNO))
            {
                MessageBox.Show("û�пɸ��µĻ�����Ϣ�����Ȱ�������Ϣ�Ǽǣ�");
                return;
            }
            this.ucRegPatientInfo1.IsEditMode = true;
            if (!this.ucRegPatientInfo1.InputValid()) return;

            HISFC.Models.RADT.PatientInfo patient = this.ucRegPatientInfo1.GetPatientInfomation();
            FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            #region ���»��߻�����Ϣ
            int resultValue = radtIntegrate.RegisterComPatient(patient);
            if (resultValue <= 0)
            {
                FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("���»�����Ϣʧ�ܣ�" + accountManager.Err);
                return;
            }
            resultValue = functionIntegrate.SaveChange<HISFC.Models.RADT.Patient>(false, false, patient.PID.CardNO, patientInfo, patient);
            if (resultValue < 0)
            {
                FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("���ɱ����¼ʧ�ܣ�");
                return;
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();

            string register = this.controlParma.GetControlParam<string>("MZ9951", false, "0");

            if (register == "0")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("���»�����Ϣ�ɹ���"), FS.FrameWork.Management.Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.ClearData();
        }
        /// <summary>
        /// ��ӡ��ǩ
        /// </summary>
        private void PrintLable()
        {
            if (iPrintLable != null)
            {
                if (accountCard == null)
                {
                    return;
                }
                iPrintLable.PrintLable(accountCard);
            }
        }

        /// <summary>
        /// ��ӡ������ƾ֤
        /// </summary>
        /// <param name="lstCardFee"></param>
        private void PrintCardFee(List<AccountCardFee> lstCardFee)
        {
            if (lstCardFee == null || lstCardFee.Count <= 0)
                return;

            foreach (AccountCardFee cardFee in lstCardFee)
            {
                if (cardFee.TransType == FS.HISFC.Models.Base.TransTypes.Positive)
                {
                    if (iPrint == null)
                        continue;

                    iPrint.SetValue(cardFee);
                    iPrint.Print();
                }
                else
                {
                    if (iPrintReturn == null)
                        continue;

                    iPrintReturn.SetValue(cardFee);
                    iPrintReturn.Print();
                }
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        private void ClearData()
        {
            this.ucRegPatientInfo1.Clear(true);
            this.txtMarkNo.Text = string.Empty;
            this.txtMedicalCardNo.Text = string.Empty;
            this.cmbMarkType.SelectedIndex = 0;
            this.ucRegPatientInfo1.Focus();
            accountCard = null;
            this.ckIsTreatment.Checked = false;
            this.spPatient.RowCount = 0;//{9043E842-7A83-4213-AB6D-E7E04B2EF76E} lfhm
            
            //�������ʾ{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
            if (Screen.AllScreens.Length > 1)
            {
                //this.ucRegPatientInfo1.iMultiScreen.ListInfo = null;
                FS.HISFC.Models.Base.Employee currentOperator = accountManager.Operator as FS.HISFC.Models.Base.Employee;
                //��ʾ��ʼ������                    
                System.Collections.Generic.List<Object> lo = new System.Collections.Generic.List<object>();
                lo.Add("");//������Ϣ
                lo.Add("");//����
                lo.Add(currentOperator.ID);//�շ�Ա����
                lo.Add(currentOperator.Name);//�շ�Ա����
                this.ucRegPatientInfo1.iMultiScreen.ListInfo = lo;
            }
            this.txtCardNo.Text = "";//{9043E842-7A83-4213-AB6D-E7E04B2EF76E} lfhm
            this.txtCardNo.Visible = false;
        }
               
        /// <summary>
        /// �����µĲ�����
        /// </summary>
        private int BulidCard(AccountCard tempAccountCard)
        {
            try
            {
                //if (accountManager.InsertAccountCard(tempAccountCard) == -1)
                if (accountManager.InsertAccountCardEX(tempAccountCard) == -1)// {AE74D7CC-B573-48a7-9EAD-60340E3F01C9}
                {
                    MessageBox.Show("���濨��¼ʧ�ܣ�" + accountManager.Err, "����");
                    return -1;
                }
                accountCardRecord = new FS.HISFC.Models.Account.AccountCardRecord();
                //���뿨�Ĳ�����¼
                accountCardRecord.MarkNO = tempAccountCard.MarkNO;
                accountCardRecord.MarkType.ID = tempAccountCard.MarkType.ID;
                accountCardRecord.CardNO = tempAccountCard.Patient.PID.CardNO;
                accountCardRecord.OperateTypes.ID = (int)FS.HISFC.Models.Account.MarkOperateTypes.Begin;
                accountCardRecord.Oper.ID = (this.accountManager.Operator as FS.HISFC.Models.Base.Employee).ID;
                //�Ƿ���ȡ���ɱ���
                accountCardRecord.CardMoney = 0;

                if (accountManager.InsertAccountCardRecord(accountCardRecord) == -1)
                {
                    MessageBox.Show("���濨������¼ʧ�ܣ�"+ accountManager.Err);
                    return -1;
                }
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("����ʧ�ܣ�" + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        protected virtual int QueryPatientInfo()
        {
            FS.HISFC.Models.RADT.PatientInfo patient = this.ucRegPatientInfo1.GetPatientInfomation();

            if (string.IsNullOrEmpty(patient.Name) == true)
            {
                DialogResult dr = MessageBox.Show("��ǰ��ѯ����Ϊ�գ�\n�����ĺܳ�ʱ�䣬�Ƿ������", "��ʾ", MessageBoxButtons.YesNo);
                if (dr != DialogResult.Yes)
                {
                    return -1;
                }
            }

            // �Ա����ͬ��λ����Ϊ��ѯ����
            return this.QueryPatientInfo(patient.Name,
                                         patient.PhoneHome,
                                         patient.Mobile,
                                         patient.IDCardType.ID,
                                         patient.IDCard,
                                         patient.SSN);
        }

        /// <summary>
        /// ��ѯһ����ĳ���߰쿨��Ϣ
        /// </summary>
        /// <returns></returns>
        protected virtual int QueryPatientInfoInDay()
        {
            string operCode = this.accountManager.Operator.ID.ToString();
            string days = "1";
            if (!string.IsNullOrEmpty(operCode) && !string.IsNullOrEmpty(days))
            {
                return this.QueryPatientInfoInDay(operCode, days);
            }
            return -1;
        }

        protected virtual int QueryPatientInfoInDay(string operCode, string days)
        {
            if (string.IsNullOrEmpty(operCode) || string.IsNullOrEmpty(days))
            {
                return -1;
            }
            List<AccountCard> list = accountManager.GetAccountCardInDays(operCode, days);
            if (list == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(accountManager.Err);
                return -1;  
            }
            try
            {
                if (this.spPatientInDay.RowCount > 0)
                {
                    this.spPatientInDay.Rows.Remove(0, this.spPatientInDay.RowCount);
                }

                this.spPatientInDay.Rows.Count = list.Count;
                int count = 0, beginIndex = 0, rangCount = 1;
                count = list.Count;

                for (int i = 0; i < count; i++)
                {
                    AccountCard tempCard = list[i];
                    this.spPatientInDay.Cells[i, 0].Text = tempCard.Patient.Name;
                    //�Ա�
                    this.spPatientInDay.Cells[i, 1].Text = tempCard.Patient.Sex.Name;
                    //����
                    this.spPatientInDay.Cells[i, 2].Text = this.accountManager.GetAge(tempCard.Patient.Birthday);
                    //����
                    this.spPatientInDay.Cells[i, 3].Text = this.ucRegPatientInfo1.GetName(tempCard.Patient.Nationality.ID, 0);
                    //��ͬ��λ
                    this.spPatientInDay.Cells[i, 4].Text = tempCard.Patient.Pact.Name;
                    //֤������
                    this.spPatientInDay.Cells[i, 5].Text = this.ucRegPatientInfo1.GetName(tempCard.Patient.IDCardType.ID, 1);
                    //֤����
                    this.spPatientInDay.Cells[i, 6].Text = tempCard.Patient.IDCard;
                    string telephone = "";
                    if (tempCard.Patient.PhoneHome != null && tempCard.Patient.PhoneHome != "")
                    {
                        telephone = tempCard.Patient.PhoneHome;
                    }
                    else if (tempCard.Patient.Kin.RelationPhone != null && tempCard.Patient.Kin.RelationPhone != "")
                    {
                        telephone = tempCard.Patient.Kin.RelationPhone;
                    }
                    else
                    {
                        telephone = "";
                    }
                    this.spPatientInDay.Cells[i, 7].Text = telephone;
                    this.spPatientInDay.Cells[i, 8].Text = tempCard.Patient.AddressHome;
                    this.spPatientInDay.Cells[i, 9].Text = markTypeHelp.GetName(tempCard.MarkType.ID);
                    this.spPatientInDay.Cells[i, 10].Text = tempCard.MarkNO;
                    this.spPatientInDay.Columns.Get(10).Visible = true;//{9043E842-7A83-4213-AB6D-E7E04B2EF76E} lfhm
                    //if (isShowMarkNo)
                    //{
                    //}
                    this.spPatientInDay.Rows[i].Tag = tempCard;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return -1;
            }
            return 1;
        }

        protected int QueryPatientInfoByEnter()
        {
            FS.HISFC.Models.RADT.PatientInfo patientTemp = this.ucRegPatientInfo1.patientInfo;
            FS.HISFC.Models.RADT.PatientInfo patient = this.ucRegPatientInfo1.GetPatientInfomation();
            this.ucRegPatientInfo1.patientInfo = patientTemp;

            // �Ա����ͬ��λ����Ϊ��ѯ����
            return this.QueryPatientInfo(patient.Name,
                                         patient.PhoneHome,
                                         patient.Mobile,
                                         patient.IDCardType.ID,
                                         patient.IDCard,
                                         patient.SSN);
        }

        protected virtual int QueryPatientInfo(string patientName,string homePhone,string mobile, string idCardType, string idCard, string SSN)
        {
            if (string.IsNullOrEmpty(patientName)
             && string.IsNullOrEmpty(idCard)
             && string.IsNullOrEmpty(homePhone)
             && string.IsNullOrEmpty(mobile))
            {
                return -1;
            }

            //if (!IsSelectPatientByNameIDCardByEnter)
            //{
            //    if (string.IsNullOrEmpty(idCardType) && string.IsNullOrEmpty(idCard))
            //    {
            //        return -1;
            //    }
            //}

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ��һ�����Ϣ�����Ժ�...");
            Application.DoEvents();
            //���һ�����Ϣ

            //���֤������Ϊ�գ�֤������Ϊ��
            if (string.IsNullOrEmpty(idCard))
            {
                idCardType = "";
            }
            List<AccountCard> list = accountManager.GetAccountCard(patientName,homePhone,mobile,idCardType, idCard, SSN);
                                                                    
            if (list == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(accountManager.Err);
                return -1;
            }
            try
            {
                if (this.spPatient.Rows.Count > 0)
                {
                    this.spPatient.Rows.Remove(0, this.spPatient.Rows.Count);
                }
                this.spPatient.Rows.Count = list.Count;
                int count = 0, beginIndex = 0, rangCount = 1;
                count = list.Count;
                for (int i = 0; i < count; i++)
                {
                    AccountCard tempCard = list[i];
                    //����
                    this.spPatient.Cells[i, 0].Text = tempCard.Patient.Name;
                    //�Ա�
                    this.spPatient.Cells[i, 1].Text = tempCard.Patient.Sex.Name;
                    //����
                    this.spPatient.Cells[i, 2].Text = this.accountManager.GetAge(tempCard.Patient.Birthday);
                    //����
                    this.spPatient.Cells[i, 3].Text = this.ucRegPatientInfo1.GetName(tempCard.Patient.Nationality.ID, 0);
                    //��ͬ��λ
                    this.spPatient.Cells[i, 4].Text = tempCard.Patient.Pact.Name;
                    //֤������
                    this.spPatient.Cells[i, 5].Text = this.ucRegPatientInfo1.GetName(tempCard.Patient.IDCardType.ID, 1);
                    //֤����
                    this.spPatient.Cells[i, 6].Text = tempCard.Patient.IDCard;
                    string telephone="";
                    if (tempCard.Patient.PhoneHome != null && tempCard.Patient.PhoneHome != "")
                    {
                        telephone = tempCard.Patient.PhoneHome;
                    }
                    else if (tempCard.Patient.Kin.RelationPhone != null && tempCard.Patient.Kin.RelationPhone != "")
                    {
                        telephone = tempCard.Patient.Kin.RelationPhone;
                    }
                    else
                    {
                        telephone = "";
                    }
                    this.spPatient.Cells[i, 7].Text = telephone;
                    this.spPatient.Cells[i, 8].Text = tempCard.Patient.AddressHome;
                    this.spPatient.Cells[i, 9].Text = tempCard.Patient.PID.CardNO;
                    this.spPatient.Cells[i, 10].Text = markTypeHelp.GetName(tempCard.MarkType.ID);
                    this.spPatient.Cells[i, 11].Text = tempCard.MarkNO;
                    this.spPatient.Columns.Get(11).Visible = true;
                    //{9043E842-7A83-4213-AB6D-E7E04B2EF76E} lfhm
                    //if (isShowMarkNo)
                    //{
                    //}
                    this.spPatient.Rows[i].Tag = tempCard;
                    //����ϲ���Ԫ��
                    if (i < count - 1)
                    {
                        if (tempCard.Patient.PID.CardNO == list[i + 1].Patient.PID.CardNO)
                        {
                            rangCount += 1;
                            if (i == count - 2)
                            {
                                if (rangCount > 1)
                                {
                                    RangFpCell(beginIndex, rangCount);
                                }
                            }
                        }
                        else
                        {
                            if (rangCount > 1)
                            {
                                RangFpCell(beginIndex, rangCount);
                            }
                            beginIndex = i + 1;
                            rangCount = 1;
                        }
                    }

                }
                this.neuSpread1.ActiveSheet = spPatient;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return -1;
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
        }

        /// <summary>
        /// �ϲ���Ԫ��
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="count"></param>
        private void RangFpCell(int begin, int count)
        {
            for (int col = 0; col < this.spPatient.Columns.Count - 2; col++)
            {
                this.spPatient.Models.Span.Add(begin, col, count, 1);
            }
        }

        /// <summary>
        /// ��ת����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucRegPatientInfo1_OnFoucsOver(object sender, EventArgs e)
        {
            this.neuSpread1.ActiveSheet = this.spPatient;
            this.txtMarkNo.Focus();
        }

        /// <summary>
        /// ���һ�����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucRegPatientInfo1_OnEnterSelectPatient(object sender, EventArgs e)
        {
            if (this.IsSelectPatientByEnter)
            {
                this.QueryPatientInfoByEnter();
            }
        }

        /// <summary>
        /// ucRegPatientInfo�ؼ�cmb�õ�����ʱ�������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucRegPatientInfo1_CmbFoucs(object sender, EventArgs e)
        {
            if (sender is FS.FrameWork.WinForms.Controls.NeuComboBox)
            {
                FrameWork.WinForms.Controls.NeuComboBox cmb = sender as FrameWork.WinForms.Controls.NeuComboBox;
                ArrayList al = cmb.alItems;
                DealConstantList(al);
                this.neuSpread1.ActiveSheet = this.spInfo;
            }
            else
            {
                this.neuSpread1.ActiveSheet = this.spPatient;
            }
        }

        /// <summary>
        /// �½��˻�
        /// </summary>
        /// <returns></returns>
        private int NewAccount()
        {
            if (accountCard == null)
            {
                return 0;
            }
            FS.HISFC.Models.Account.Account account = this.accountManager.GetAccountByCardNoEX(accountCard.Patient.PID.CardNO);
            if (account == null || string.IsNullOrEmpty(account.ID))//�˻�Ϊ�գ�����
            {
                string  JudgeCredentialCreating = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.AccountConstant.JudgeCredentialCreating, false);
                if ("0".Equals(JudgeCredentialCreating))
                {
                    //�ж�֤�����Ƿ�����˻�
                    ArrayList accountList = accountManager.GetAccountByIdNO(accountCard.Patient.IDCard, accountCard.Patient.IDCardType.ID);
                    if (accountList == null)
                    {
                        MessageBox.Show("�����˻�ʧ�ܣ����һ����˻���Ϣʧ�ܣ�");
                        return -1;
                    }
                    //����֤���Ų��һ����˻���Ϣ
                    if (accountList.Count > 0)
                    {
                        return 0;
                    }
                }

                //�˻���Ϣ
                account = new FS.HISFC.Models.Account.Account();
                account.ID = accountManager.GetAccountNO();
                account.AccountCard = accountCard;
                //�Ƿ�ȡĬ�����룬ϵͳ���ã����߳���һ��Ĭ�����롣
                //�˻�����
                bool IsDefaultPassword= controlParamIntegrate.GetControlParam<bool>("S00033", false);
                if (!IsDefaultPassword)
                {
                    ucEditPassWord uc = new ucEditPassWord(false);
                    FS.FrameWork.WinForms.Classes.Function.ShowControl(uc);
                    if (uc.FindForm().DialogResult != DialogResult.OK) return -1;
                    //��������
                    account.PassWord = uc.PwStr;
                }
                else
                {
                    account.PassWord = "000000";
                }

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //�����˻���
                if (accountManager.InsertAccount(account) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�����˻�ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                //�����˻���ˮ��Ϣ
                FS.HISFC.Models.Account.AccountRecord accountRecord =  new FS.HISFC.Models.Account.AccountRecord();
                accountRecord.AccountNO = account.ID;//�ʺ�
                accountRecord.Patient = accountCard.Patient;//���￨��
                accountRecord.FeeDept.ID = (accountManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;//���ұ���
                accountRecord.Oper.ID = accountManager.Operator.ID;//����Ա
                accountRecord.OperTime = accountManager.GetDateTimeFromSysDateTime();//����ʱ��
                accountRecord.IsValid = true;//�Ƿ���Ч
                if (accountRecord != null)
                {
                    accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.NewAccount;
                    if (accountManager.InsertAccountRecord(accountRecord) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�����˻�ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�����˻�ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("���沢�����˻��ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                return 1;
            }
            return 0;
        }
        #endregion

        #region �¼�

        private void ucCardManager_Load(object sender, EventArgs e)
        {
            //����
            //if (!string.IsNullOrEmpty(this.Tag.ToString()))
            //{
            //    this.afterSaveClose = true;
            //    btnSave.Visible = true;
            //    this.txtMarkNo.Text = this.Tag.ToString();
            //}

            ArrayList al = managerIntegrate.GetConstantList("MarkType");
            if (al == null)
            {
                MessageBox.Show("���Ҿ��￨����ʧ��");
                return;
            }
            this.cmbMarkType.AddItems(al);

            ArrayList al1 = managerIntegrate.GetConstantList("MemCardType");// {AE74D7CC-B573-48a7-9EAD-60340E3F01C9}
            if (al1 == null)
            {
                MessageBox.Show("���һ�Ա���ȼ�ʧ��");
                return;
            }
            this.cmbMemCardType.AddItems(al1);// {AE74D7CC-B573-48a7-9EAD-60340E3F01C9}
            this.cmbMarkType.SelectedIndex = 0;
            this.txtMarkNo.Enabled = false;
            markTypeHelp.ArrayObject = al;
            foreach (NeuObject conObj in al)
            {
                if (conObj.Name.Contains("����"))
                {
                    this.cardTypeObj = conObj;
                    break;
                }
            }
            this.QueryPatientInfoInDay();

            this.IsAcceptCardFee = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.AccountConstant.IsAcceptCardFee, false);
            this.IsAcceptChangeCardFee = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.AccountConstant.IsAcceptChangeCardFee, false);
            this.ReturnCardReturnFee = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.AccountConstant.ReturnCardReturnFee, false);

            this.ucRegPatientInfo1.CmbFoucs += new HandledEventHandler(ucRegPatientInfo1_CmbFoucs);
            this.ucRegPatientInfo1.OnFoucsOver += new HandledEventHandler(ucRegPatientInfo1_OnFoucsOver);
            this.ucRegPatientInfo1.OnEnterSelectPatient += new HandledEventHandler(ucRegPatientInfo1_OnEnterSelectPatient);
            this.ucRegPatientInfo1.IsLocalOperation = this.isLocalOperation;

            this.cmbMarkType.SelectedIndexChanged += new EventHandler(cmbMarkType_SelectedIndexChanged);

            iPrintLable = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(IPrintLable)) as IPrintLable;

            iPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(IPrintCardFee)) as IPrintCardFee;

            iPrintReturn = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(IPrintReturnCardFee)) as IPrintReturnCardFee;

            // {C49AFFB1-D0EA-41bf-AD60-9F921D91E93D}
            this.isJudgePact = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.AccountConstant.BuildCardIsJudgePact, false);
            this.ucRegPatientInfo1.IsJudgePact = this.isJudgePact;
            this.ucRegPatientInfo1.Focus();
            //�������ʾ{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
            if (Screen.AllScreens.Length > 1)
            {
                this.FindForm().Deactivate += new EventHandler(ucCardManager_Deactivate);
                this.FindForm().Activated += new EventHandler(ucCardManager_Activated);
            }

            this.Enter += new EventHandler(ucCardManager_Enter);

            this.Leave += new EventHandler(ucCardManager_Leave);

            //�ж��Ƿ�����Һ�
            string register = this.controlParma.GetControlParam<string>("MZ9951", false, "0");

            IsCanEditMakeNO = this.controlParma.GetControlParam<bool>("MZDJ01", false, false);
            if (register.Equals("1"))
            {
                System.Windows.Forms.MenuItem menuItem4 = new MenuItem("��ӡ�Һ�Ʊ");
                this.menu.MenuItems.Add(menuItem4);
                menuItem4.Click += new EventHandler(menuItem4_Click);
            }
        }

        void cmbMarkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtMarkNo.Text = "";

            if (this.cmbMarkType.SelectedItem != null)
            {
                FrameWork.Models.NeuObject MarkType = this.cmbMarkType.SelectedItem as FrameWork.Models.NeuObject;
                if (AllowNoCardSaveType.Contains(MarkType.ID))
                {
                    this.txtMarkNo.Enabled = false;
                    this.cmbMemCardType.Enabled = false;//// {AE74D7CC-B573-48a7-9EAD-60340E3F01C9}
                }
                else
                {
                    this.txtMarkNo.Enabled = IsCanEditMakeNO;
                    this.cmbMemCardType.Enabled = true;//// {AE74D7CC-B573-48a7-9EAD-60340E3F01C9}
                }
            }
        }


        #region ��������{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
        public void ucCardManager_Activated(object sender, EventArgs e)
        {
            this.ucRegPatientInfo1.iMultiScreen.ShowScreen();
        }

        public void ucCardManager_Deactivate(object sender, EventArgs e)
        {
            this.ucRegPatientInfo1.iMultiScreen.CloseScreen();
        }
          #endregion
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBar.AddToolButton("������Ϣ��ѯ", "������Ϣ��ѯ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ, true, false, null);
            toolBar.AddToolButton("����", "����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            toolBar.AddToolButton("���֤", "���֤", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Mģ��, true, false, null);
            toolBar.AddToolButton("�޸�", "�޸�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            toolBar.AddToolButton("ˢ��", "ˢ��", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            return toolBar;
        }
        
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "������Ϣ��ѯ":
                    {
                        QueryPatientInfo();
                        break;
                    }
                case "����":
                    {
                        this.ClearData();
                        break;
                    }
                case "�޸�":
                    {
                        this.ModifyPatientInfo();
                        //{9043E842-7A83-4213-AB6D-E7E04B2EF76E} lfhm

                        break;
                    }
                case "���֤"://��ȡ�������֤
                    {
                        this.ucRegPatientInfo1.Clear(true);
                        int i= this.ucRegPatientInfo1.ReadIDCard();
                        if (i == 1)
                        {
                            this.ucRegPatientInfo1_OnEnterSelectPatient(null, null);

                            if (this.isQueryPatientInfoByReadIDCard)
                            {
                                this.QueryPatientInfo();
                            }
                            if (this.ucRegPatientInfo1.IsJudgePactByIdno)
                            {
                                this.ucRegPatientInfo1.JudgePactByIdno();
                            }
                        }
                        break;
                    }
                case "ˢ��":
                    {
                        string cardNo = "";
                        string mCardNo = "";
                        string error = "";

                        if (this.txtMedicalCardNo.Focused)
                        {
                            if (Function.OperCard(ref cardNo, ref error) == -1)
                            {
                                CommonController.Instance.MessageBox(error, MessageBoxIcon.Error);
                                return;
                            }
                            txtMedicalCardNo.Text = cardNo;
                            txtMedicalCardNo.Focus();
                            this.txtMedicalCardNo_KeyDown(null, new KeyEventArgs(Keys.Enter));

                        }
                        else if (this.cmbMarkType.Tag.ToString() == "Account_CARD" || this.cmbMarkType.Text.ToString() == "��Ա��")// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}(this.txtMarkNo.Focused)
                        {
                            if (Function.OperMCard(ref mCardNo, ref error) == -1)
                            {
                                CommonController.Instance.MessageBox(error, MessageBoxIcon.Error);
                                return;
                            }
                            this.txtMarkNo.Text = mCardNo;
                            this.txtMarkNo.Focus();
                            this.txtMarkNo.SelectAll();

                        }
                        
                        break;
                    }
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        
        //{B062ABDC-7545-4e5d-A9F5-DCBF217052F9}
        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryPatientInfo();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return base.OnSave(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.PrintLable();
            this.ClearData();
            return base.OnPrint(sender, neuObject);
        }

        private void txtMarkNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                //�������ʾ{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
                if (Screen.AllScreens.Length > 1)
                {
                    
                    System.Collections.Generic.List<Object> lo = new System.Collections.Generic.List<object>();
                    lo.Add(this.ucRegPatientInfo1 .GetPatientInfomation());
                    lo.Add(this.txtMarkNo.Text .ToString ());
                    lo.Add("");
                    lo.Add("");
                    this.ucRegPatientInfo1.iMultiScreen.ListInfo = lo;
                }
                string strMarkNo = this.txtMarkNo.Text.Trim();
                FrameWork.Models.NeuObject MarkType = this.cmbMarkType.SelectedItem as FrameWork.Models.NeuObject;

                if (MarkType == null)
                {
                    MessageBox.Show("��ǰδѡ�����ͣ�");
                    this.cmbMarkType.Focus();
                    return;
                }

                if (AllowNoCardSaveType.Contains(MarkType.ID) && string.IsNullOrEmpty(strMarkNo))
                {
                }
                else
                {
                    accountCard = new AccountCard();
                    //��ʶ�ǰ쿨
                    accountCard.Memo = "2";

                    int resultValue = accountManager.GetCardByRule(this.txtMarkNo.Text.Trim(), ref accountCard);
                    if (resultValue <= 0)
                    {
                        if (accountCard.MarkType.Name.Contains("����"))
                        {
                            //��Ժ���ŵĽ�������������ƽ̨�ϻ�ȡ���߻�����Ϣ������
                            if (!accountCard.MarkNO.StartsWith(healthCard.UnitCode))
                            {
                                healthCard.DepartmentCode = (accountManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;
                                healthCard.CardNumber = accountCard.MarkNO;
                                int getResult;
                                getResult = healthCardManager.GetPatientInfo(healthCard);
                                if (getResult == 3)
                                {
                                    MessageBox.Show("�ÿ���������ҽԺ�쿨ʹ�ã����ڻ��߻�����Ϣ�޸Ľ����ȡ������Ϣ��");
                                    this.txtMarkNo.Focus();
                                    this.txtMarkNo.SelectAll();
                                    //this.cmbMarkType.Tag = string.Empty;
                                    this.cmbMarkType.SelectedIndex = 0;
                                    return;
                                }
                            }
                        }
                        else if (resultValue < 0)
                        {
                            MessageBox.Show(accountManager.Err);
                            this.txtMarkNo.Focus();
                            this.txtMarkNo.SelectAll();
                            //this.cmbMarkType.Tag = string.Empty;
                            this.cmbMarkType.SelectedIndex = 0;
                            return;
                        }
                    }

                    if (resultValue == 1)
                    {
                        MessageBox.Show("�ÿ��ѱ�ʹ�ã��뻻����");
                        this.txtMarkNo.Focus();
                        this.txtMarkNo.SelectAll();
                        //this.cmbMarkType.Tag = string.Empty;

                        this.cmbMarkType.SelectedIndex = 0;
                        return;
                    }
                    if (!string.IsNullOrEmpty(accountCard.MarkNO))
                    {
                        this.txtMarkNo.Text = accountCard.MarkNO;
                    }
                    if (!string.IsNullOrEmpty(accountCard.MarkType.ID))
                    {
                        this.cmbMarkType.Tag = accountCard.MarkType.ID;
                    }     
                }
                if (MessageBox.Show("�Ƿ񱣴����ݣ�", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Save();
                }
            }
        }



        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            if (this.neuSpread1.ActiveSheet != spPatient && this.neuSpread1.ActiveSheet != spPatientInDay)
            {
                return;
            }
            if (this.neuSpread1.ActiveSheet == spPatient && this.spPatient.ActiveRow == null)
            {
                return;
            }
            if (this.neuSpread1.ActiveSheet == spPatient && this.spPatient.ActiveRow.Tag == null)
            {
                return;
            }
            if (this.neuSpread1.ActiveSheet == spPatientInDay && this.spPatientInDay.ActiveRow == null)
            {
                return;
            }
            if (this.neuSpread1.ActiveSheet == spPatientInDay && this.spPatientInDay.ActiveRow.Tag == null)
            {
                return;
            }
            if (this.neuSpread1.ActiveSheet != spPatient)
            {
                this.menuItem1.Enabled = false;
            }

            AccountCard tempAccountCard = new AccountCard();
            if (this.neuSpread1.ActiveSheet == spPatient)
            {
                tempAccountCard = this.spPatient.ActiveRow.Tag as AccountCard;
            }
            else
            {
                tempAccountCard = this.spPatientInDay.ActiveRow.Tag as AccountCard;
            }
            if (printCardType.Contains(tempAccountCard.MarkType.ID))
            {
                this.menuItem2.Enabled = true;
            }
            else
            {
                this.menuItem2.Enabled = false;
            }

            this.menu.Show(neuSpread1 as Control, new Point(e.X, e.Y));
        }

        /// <summary>
        /// ��ʾ���߻�����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem1_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1.ActiveSheet != this.spPatient) return;
            if (this.spPatient.ActiveRow.Tag == null) return;
            AccountCard tempCard = this.spPatient.ActiveRow.Tag as AccountCard;
            if (tempCard.Patient == null)
            {
                MessageBox.Show("��ѯ������Ϣʧ�ܣ�");
                return;
            }
            if (!string.IsNullOrEmpty(tempCard.Patient.PID.CardNO))
            {
                this.txtCardNo.Visible = true;
                this.txtCardNo.Text = "�����ţ�" + tempCard.Patient.PID.CardNO;//{9043E842-7A83-4213-AB6D-E7E04B2EF76E} lfhm
            }
            this.ucRegPatientInfo1.CardNO = tempCard.Patient.PID.CardNO;
            this.txtMarkNo.Focus();
        }
        /// <summary>
        /// ͣ�û�Ա��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem3_Click(object sender, EventArgs e)
        {
            //{9043E842-7A83-4213-AB6D-E7E04B2EF76E} lfhm
            if (this.neuSpread1.ActiveSheet != this.spPatient) return;
            if (this.spPatient.ActiveRow.Tag == null) return;
            if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("�Ƿ�ͣ�øû�Ա����"), "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }
            AccountCard tempCard = this.spPatient.ActiveRow.Tag as AccountCard;
            if (tempCard.Patient == null)
            {
                MessageBox.Show("��ѡ����Ҫͣ�ÿ��ţ�");
                return;
            }
            if (tempCard.MarkNO == tempCard.Patient.PID.CardNO && tempCard.MarkType.ID.ToString() == "Card_No")
            {
                MessageBox.Show("����ֹͣ�����ţ�Ժ�ںţ���������ѡ����Ҫͣ�õĻ�Ա���ţ�");
                return;
            }
            if (accountManager.QueryCardAountByCardNo(tempCard.Patient.PID.CardNO) == "1")
            {
                MessageBox.Show("ֻ������һ�Ż�Ա����������»�Ա����Ժ�ڿ���ͣ�ã�");
                return;
            }
            if (!string.IsNullOrEmpty(tempCard.MarkType.ID) && !string.IsNullOrEmpty(tempCard.Patient.PID.CardNO) && !string.IsNullOrEmpty(tempCard.MarkNO))
            {
                tempCard.MarkStatus = MarkOperateTypes.Stop;
                tempCard.StopOper.ID = accountManager.Operator.ID;
                tempCard.StopOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(accountManager.GetSysDateTime());

                if (accountManager.StopAccountCard(tempCard) < 0)
                {
                    MessageBox.Show("ͣ��ʧ�ܣ�����ϵ��Ϣ�ƣ�");
                    return;
                }
                MessageBox.Show("ͣ�óɹ���");

                FS.HISFC.Models.RADT.PatientInfo patient = this.ucRegPatientInfo1.GetPatientInfomation();
                // �Ա����ͬ��λ����Ϊ��ѯ����
                this.QueryPatientInfo(patient.Name,
                                             patient.PhoneHome,
                                             patient.Mobile,
                                             patient.IDCardType.ID,
                                             patient.IDCard,
                                             patient.SSN);
            }
            else
            {
                MessageBox.Show("����ʧ�ܣ�����ϵ��Ϣ�ƣ�");
                return;
            }
        }

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem2_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1.ActiveSheet != this.spPatient && this.neuSpread1.ActiveSheet != spPatientInDay) 
                return;
            if (this.neuSpread1.ActiveSheet == spPatient && this.spPatient.ActiveRow == null)
            {
                return;
            }
            if (this.neuSpread1.ActiveSheet == spPatient && this.spPatient.ActiveRow.Tag == null)
            {
                return;
            }
            if (this.neuSpread1.ActiveSheet == spPatientInDay && this.spPatientInDay.ActiveRow == null)
            {
                return;
            }
            if (this.neuSpread1.ActiveSheet == spPatientInDay && this.spPatientInDay.ActiveRow.Tag == null)
            {
                return;
            }
            //if (this.spPatient.ActiveRow.Tag == null && this.spPatientInDay.ActiveRow.Tag == null) 
            //    return;

            AccountCard tempaccontCard = new AccountCard();
            //���ýӿڴ����ӡ��ǩ
            if (this.neuSpread1.ActiveSheet == this.spPatient)
            {
                tempaccontCard = this.spPatient.ActiveRow.Tag as AccountCard;
            }
            else
            {
                tempaccontCard = this.spPatientInDay.ActiveRow.Tag as AccountCard;
            }

            if (iPrintLable != null)
            {
                iPrintLable.PrintLable(tempaccontCard);
            }
            else
            {
                MessageBox.Show("δ�ܴ�ӡ������ϵ��Ϣ��ά����ǩ��ӡ�ӿ�[IPrintLable]��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void menuItem4_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1.ActiveSheet != this.spPatient) return;
            if (this.spPatient.ActiveRow.Tag == null) return;
            AccountCard tempCard = this.spPatient.ActiveRow.Tag as AccountCard;
            if (tempCard.Patient == null)
            {
                MessageBox.Show("��ѯ������Ϣʧ�ܣ�");
                return;
            }
            this.ucRegPatientInfo1.CardNO = tempCard.Patient.PID.CardNO;
            this.save();
        }

        private void ckIsTreatment_CheckedChanged(object sender, EventArgs e)
        {
            bool bl = ckIsTreatment.Checked;
            this.ucRegPatientInfo1.IsTreatment = bl;
            if (bl)
            {
                this.ucRegPatientInfo1.Clear(true);
                this.txtMarkNo.Focus();
            }
            else
            {
                this.ucRegPatientInfo1.Focus();
            }

        }

        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {

            get
            {
                Type[] vtype = new Type[2];
                vtype[0] = typeof(IPrintLable);
               
                return vtype;
            }
        }

        #endregion

        private void txtMarkNo_Enter(object sender, EventArgs e)
        {
            try
            {
                foreach (InputLanguage input in InputLanguage.InstalledInputLanguages)
                {
                    if (input.LayoutName == "��ʽ����" || input.LayoutName == "����(����) - ��ʽ����")
                    {
                        InputLanguage.CurrentInputLanguage = input;
                    }
                }
            }
            catch
            { }
        }

        private void txtMedicalCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.spPatient.RowCount = 0;
                QueryPatientInfoByCardNO();
            }
            //�������ʾ{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
            if (Screen.AllScreens.Length > 1)
            { 
                FS.HISFC.Models.RADT.PatientInfo showPatientInfo = this.ucRegPatientInfo1 .GetPatientInfomation ();
                    System.Collections.Generic.List<Object> lo = new System.Collections.Generic.List<object>();
                    lo.Add(showPatientInfo);//������Ϣ
                    lo.Add(this.txtMedicalCardNo .Text );//����
                    lo.Add("");//�շ�Աid
                    lo.Add("");//�շ�Ա����
                    this.ucRegPatientInfo1 .iMultiScreen .ListInfo =lo;                 
            }
        }

        /// <summary>
        /// ���һ�����Ϣ
        /// </summary>
        protected virtual void QueryPatientInfoByCardNO()
        {

            string medicalCardNo = this.txtMedicalCardNo.Text.Trim();
            if (medicalCardNo == string.Empty)
            {
                return;
            }

            //{B062ABDC-7545-4e5d-A9F5-DCBF217052F9}
            //accountCard = new FS.HISFC.Models.Account.AccountCard();
            //int resultValue = accountManager.GetCardByRule(medicalCardNo, ref accountCard);
            accountCard = new FS.HISFC.Models.Account.AccountCard();
            int resultValue = feeManage.ValidMarkNO(medicalCardNo, ref accountCard);

            if (resultValue <= 0)
            {
                MessageBox.Show("û�в�ѯ��������Ϣ��" + feeManage.Err);
                this.txtMedicalCardNo.Focus();
                this.txtMedicalCardNo.SelectAll();
                return;
            }

            this.txtMedicalCardNo.Text = accountCard.MarkNO;
            this.ucRegPatientInfo1.CardNO = this.accountCard.Patient.PID.CardNO;            
            if (!string.IsNullOrEmpty(this.accountCard.Patient.PID.CardNO))
            {
                this.txtCardNo.Visible = true;
                this.txtCardNo.Text = "�����ţ�" + this.accountCard.Patient.PID.CardNO;//{9043E842-7A83-4213-AB6D-E7E04B2EF76E} lfhm
            }
        }

        /// <summary>
        /// �ҺŹ�����
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

        private int GetRecipeType = 2;
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        private int save()
        {
            //if (this.valid() == -1)
            //    return 2;

            // ������ϸ
            List<AccountCardFee> lstAccFee = null;

            if (this.getValue(out lstAccFee) == -1)
                return 2;

            //if (this.ValidCardNO(this.regObj.PID.CardNO) < 0)
            //{
            //    return -1;
            //}

            //if (this.IsPrompt)
            //{
            //    //ȷ����ʾ
            //    if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȷ��¼�������Ƿ���ȷ"), "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            //        MessageBoxDefaultButton.Button1) == DialogResult.No)
            //    {
            //        this.cmbRegLevel.Focus();
            //        return -1;
            //    }
            //}


            int rtn;
            string Err = "";
            ////�ӿ�ʵ��{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
            //if (this.iProcessRegiter != null)
            //{
            //    rtn = this.iProcessRegiter.SaveBegin(ref regObj, ref Err);

            //    if (rtn < 0)
            //    {
            //        MessageBox.Show(Err);
            //        return -1;
            //    }
            //}

            //this.MedcareInterfaceProxy.SetPactCode(this.regObj.Pact.ID);

            #region �˻�����
            //bool isAccountFee = false;
            //decimal vacancy = 0;
            //int result = this.feeMgr.GetAccountVacancy(this.regObj.PID.CardNO, ref vacancy);
            //if (result > 0)
            //{
            //    if (!feeMgr.CheckAccountPassWord(this.regObj))
            //        return -1;
            //    if (vacancy > 0)
            //    {
            //        isAccountFee = true;
            //    }

            //}
            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.regMgr.con);
            //t.BeginTransaction();

            //this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //this.bookingMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //this.SchemaMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //this.patientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //this.feeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //this.SIMgr.SetTrans(t);
            //this.InterfaceMgr.SetTrans(t.Trans);


            //����ʼ
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();
            // ������
            // {23BA226E-A1E5-4a0b-A1D5-92FA97AF3E85}
            AccountCardFee cardFee = null;

            #region ���������⴦��

            //if (chbCardFee.Visible && chbCardFee.Checked)
            //{
            //    AccountCard accountCard = this.txtCardNo.Tag as AccountCard;
            //    if (accountCard != null)
            //    {
            //        cardFee = new AccountCardFee();
            //        cardFee.FeeType = AccCardFeeType.CardFee;
            //        cardFee.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
            //        cardFee.MarkNO = accountCard.MarkNO;
            //        cardFee.MarkType = accountCard.MarkType;

            //        FS.HISFC.Models.Base.Const obj = cardFee.MarkType as FS.HISFC.Models.Base.Const;
            //        if (obj != null)
            //        {
            //            cardFee.Tot_cost = FS.FrameWork.Function.NConvert.ToDecimal(obj.UserCode);
            //        }
            //        cardFee.Own_cost = cardFee.Tot_cost;

            //        cardFee.IsBalance = false;
            //        cardFee.BalanceNo = "";
            //        cardFee.BalnaceOper.ID = "";
            //        cardFee.IStatus = 1;

            //    }
            //}

            if (lstAccFee == null)
            {
                lstAccFee = new List<AccountCardFee>();
            }
            if (cardFee != null)
            {
                lstAccFee.Add(cardFee);

                // ����Һż�¼�� �����ù鵽�Һű�����������
                this.regObj.RegLvlFee.OthFee += cardFee.Tot_cost;
                this.regObj.OwnCost += cardFee.Own_cost;
                this.regObj.PubCost += cardFee.Pub_cost;
                this.regObj.PayCost += cardFee.Pay_cost;
            }

            //�����ӡ��Ʊ��lstAccFee.count > 0������ӡ��ƱlstAccFee.count = 0
            //���Ҫ��ӡ��Ʊ����������ñ���Ҫ�йҺŷ���Ϣ�������ùҺ�
            if (lstAccFee.Count > 0)
            {
                bool isExistRegFee = false;
                foreach (FS.HISFC.Models.Account.AccountCardFee tempCardFee in lstAccFee)
                {
                    if (tempCardFee.FeeType == AccCardFeeType.RegFee)
                    {
                        isExistRegFee = true;
                        break;
                    }
                }

                if (!isExistRegFee)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    //this.MedcareInterfaceProxy.Rollback();
                    MessageBox.Show("�Һű���Ҫ�йҺŷ���Ϣ!", "����");
                    return -1;
                }

            }

            #endregion

            #region ȡ��Ʊ��

            string strInvioceNO = "";
            string strRealInvoiceNO = "";
            string strErrText = "";
            int iRes = 0;
            string strInvoiceType = "R";

            FS.HISFC.Models.Base.Employee employee = this.regMgr.Operator as FS.HISFC.Models.Base.Employee;

            //�з�����Ϣ��ʱ��Ŵ�Ʊ
            if (lstAccFee.Count > 0)
            {

                if (this.GetRecipeType == 1)
                {
                    strInvioceNO = this.regObj.RecipeNO.ToString().PadLeft(12, '0');
                    strRealInvoiceNO = "";
                }
                else
                {
                    if (this.GetRecipeType == 2)
                    {
                        strInvoiceType = "R";
                    }
                    else if (this.GetRecipeType == 3)
                    {
                        // ȡ�����վ�
                        strInvoiceType = "C";
                    }

                    iRes = this.feeMgr.GetInvoiceNO(employee, strInvoiceType, ref strInvioceNO, ref strRealInvoiceNO, ref strErrText);

                    if (iRes == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(strErrText);
                        return -1;
                    }
                }
            }

            this.regObj.InvoiceNO = strInvioceNO;

            #endregion



            #region ���������ϸ��Ϣ

            //�з�����Ϣ��ʱ��Ŵ���
            if (lstAccFee.Count > 0)
            {

                foreach (AccountCardFee accFee in lstAccFee)
                {
                    accFee.InvoiceNo = strInvioceNO;
                    accFee.Print_InvoiceNo = strRealInvoiceNO;
                    accFee.ClinicNO = this.regObj.ID;

                    accFee.Patient.PID.CardNO = this.regObj.PID.CardNO;
                    accFee.Patient.Name = this.regObj.Name;

                    accFee.IStatus = 1;

                    accFee.FeeOper.ID = employee.ID;
                    accFee.FeeOper.Name = employee.Name;
                    accFee.FeeOper.OperTime = current;

                    accFee.Oper.ID = employee.ID;
                    accFee.Oper.Name = employee.Name;
                    accFee.Oper.OperTime = current;

                    accFee.IsBalance = false;
                    accFee.BalanceNo = "";

                }

            }

            #endregion

            decimal OwnCostTot = this.regObj.OwnCost;

            #region �˻�����
            //if (isAccountFee)
            //{
            //    decimal cost = 0m;

            //    if (vacancy < OwnCostTot)
            //    {
            //        cost = vacancy;
            //        this.regObj.PayCost = vacancy;
            //        this.regObj.OwnCost = this.regObj.OwnCost - vacancy;
            //    }
            //    else
            //    {
            //        cost = OwnCostTot;
            //        this.regObj.PayCost = this.regObj.OwnCost;
            //        this.regObj.OwnCost = 0;
            //    }
            //    if (this.feeMgr.AccountPay(this.regObj, cost, this.regObj.InvoiceNO, this.regObj.DoctorInfo.Templet.Dept.ID, "R") < 0)
            //    {
            //        FS.FrameWork.Management.PublicTrans.RollBack();
            //        MessageBox.Show(this.feeMgr.Err);
            //        return -1;
            //    }
            //    this.regObj.IsAccount = true;
            //}
            #endregion

            try
            {
                #region ���¿������
                int orderNo = 0;

                //2�������		
                if (this.UpdateSeeID(this.regObj.DoctorInfo.Templet.Dept.ID, this.regObj.DoctorInfo.Templet.Doct.ID,
                    this.regObj.DoctorInfo.Templet.Noon.ID, this.regObj.DoctorInfo.SeeDate, ref orderNo,
                    ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "��ʾ");
                    return -1;
                }

                regObj.DoctorInfo.SeeNO = orderNo;

                //ר�ҡ�ר�ơ����ԤԼ�Ÿ����Ű��޶�
                #region schema
                //if (this.UpdateSchema(this.SchemaMgr, this.regObj.RegType, ref orderNo, ref Err) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    if (Err != "")
                //        MessageBox.Show(Err, "��ʾ");
                //    return -1;
                //}

                //regObj.DoctorInfo.SeeNO = orderNo;
                #endregion

                //1ȫԺ��ˮ��			
                if (this.Update(this.regMgr, current, ref orderNo, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "��ʾ");
                    return -1;
                }

                regObj.OrderNO = orderNo;
                #endregion

                //ԤԼ�Ÿ����ѿ����־
                #region booking
                //if (this.regObj.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)
                //{
                //    //���¿����޶�
                //    rtn = this.bookingMgr.Update((this.txtOrder.Tag as FS.HISFC.Models.Registration.Booking).ID,
                //                true, regMgr.Operator.ID, current);
                //    if (rtn == -1)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show("����ԤԼ������Ϣ����!" + this.bookingMgr.Err, "��ʾ");
                //        return -1;
                //    }
                //    if (rtn == 0)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show(FS.FrameWork.Management.Language.Msg("ԤԼ�Һ���Ϣ״̬�Ѿ����,�����¼���"), "��ʾ");
                //        return -1;
                //    }
                //}
                #endregion

                #region �����ӿ�ʵ��
                //this.MedcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //this.MedcareInterfaceProxy.Connect();

                //this.MedcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //this.MedcareInterfaceProxy.BeginTranscation();

                //this.regObj.SIMainInfo.InvoiceNo = this.regObj.InvoiceNO;
                //int returnValue = this.MedcareInterfaceProxy.UploadRegInfoOutpatient(this.regObj);
                //if (returnValue == -1)
                //{
                //    this.MedcareInterfaceProxy.Rollback();
                //    FS.FrameWork.Management.PublicTrans.RollBack()
                //        ;
                //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�ϴ��Һ���Ϣʧ��!") + this.MedcareInterfaceProxy.ErrMsg);

                //    return -1;
                //}
                //////ҽ�����ߵǼ�ҽ����Ϣ
                ////if (this.regObj.Pact.PayKind.ID == "02")
                ////{
                //this.regObj.OwnCost = this.regObj.SIMainInfo.OwnCost;  //�Էѽ��
                //this.regObj.PubCost = this.regObj.SIMainInfo.PubCost;  //ͳ����
                //this.regObj.PayCost = this.regObj.SIMainInfo.PayCost;  //�ʻ����
                ////}
                #endregion

                #region addby xuewj 2010-3-15

                //if (this.adt == null)
                //{
                //    this.adt = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IHE.IADT)) as FS.HISFC.BizProcess.Interface.IHE.IADT;
                //}
                //if (this.adt != null)
                //{
                //    this.adt.RegOutPatient(this.regObj);
                //}

                #endregion

                //�ǼǹҺ���Ϣ
                if (this.regMgr.Insert(this.regObj) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    //this.MedcareInterfaceProxy.Rollback();
                    MessageBox.Show(this.regMgr.Err, "��ʾ");
                    return -1;
                }

                #region ���������ϸ��Ϣ

                if (lstAccFee != null && lstAccFee.Count > 0)
                {
                    if (this.feeMgr.SaveAccountCardFee(lstAccFee) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        //this.MedcareInterfaceProxy.Rollback();
                        MessageBox.Show(this.feeMgr.Err, "��ʾ");
                        return -1;
                    }
                }

                #endregion


                ////���»��߻�����Ϣ
                //if (this.UpdatePatientinfo(this.regObj, this.patientMgr, this.regMgr, ref Err) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    this.MedcareInterfaceProxy.Rollback();
                //    MessageBox.Show(Err, "��ʾ");
                //    return -1;
                //}
                ////�ӿ�ʵ��{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
                //if (this.iProcessRegiter != null)
                //{
                //    //{4F5BD7B2-27AF-490b-9F09-9DB107EA7AA0}
                //    //rtn = this.iProcessRegiter.SaveBegin(ref regObj, ref Err);

                //    rtn = this.iProcessRegiter.SaveEnd(ref regObj, ref Err);
                //    if (rtn < 0)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        this.MedcareInterfaceProxy.Rollback();
                //        MessageBox.Show(Err);
                //        return -1;
                //    }
                //}

                //����ҽ������
                //this.MedcareInterfaceProxy.UploadRegInfoOutpatient

                #region ��Ʊ�ߺ�

                //�з�����Ϣ��ʱ�򣬲Ŵ���Ʊ
                if (lstAccFee.Count > 0)
                {

                    if (this.GetRecipeType == 2 || this.GetRecipeType == 3)
                    {
                        string invoiceStytle = controlParma.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, false, "0");
                        if (this.feeMgr.UseInvoiceNO(employee, invoiceStytle, strInvoiceType, 1, ref strInvioceNO, ref strRealInvoiceNO, ref strErrText) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.feeMgr.Err);
                            return -1;
                        }

                        if (this.feeMgr.InsertInvoiceExtend(strInvioceNO, strInvoiceType, strRealInvoiceNO, "00") < 1)
                        {
                            // ��Ʊͷ��ʱ�ȱ���00
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.feeMgr.Err);
                            return -1;
                        }
                    }

                }

                #endregion

                FS.FrameWork.Management.PublicTrans.Commit();
                //this.MedcareInterfaceProxy.Commit();
                //this.MedcareInterfaceProxy.Disconnect();

                //�����´�����,�� 1,��ֹ��;��������
                //this.UpdateRecipeNo(1);

                //this.QueryRegLevl();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return -1;
            }

            #region ������ڷ�Ʊ��ӡ���桾������

            ////����{F0661633-4754-4758-B683-CB0DC983922B}
            //if (this.isShowChangeCostForm)
            //{
            //    rtn = this.ShowChangeForm(this.regObj);
            //    {
            //        if (rtn < 0)
            //        {
            //            return -1;
            //        }
            //    }
            //}

            #endregion

            // ��¼��Ʊ���õĴ�ӡ��Ϣ
            this.regObj.LstCardFee = lstAccFee;

            //�з�����Ϣ��ʱ�򣬲Ŵ�ӡ��Ʊ
            if (lstAccFee.Count > 0)
            {

                //if (this.isAutoPrint)
                //{
                    this.PrintReg(this.regObj, this.regMgr);
                //}
                //else
                //{
                //    DialogResult rs = MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѡ���Ƿ��ӡ�Һ�Ʊ"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                //    if (rs == DialogResult.Yes)
                //    {
                //        this.Print(this.regObj, this.regMgr);
                //    }
                //}

            }
            else if (lstAccFee.Count == 0)
            {
                MessageBox.Show("�Һųɹ�! ����ӡ��Ʊ!", "��ʾ");
            }


            //this.addRegister(this.regObj);

            //this.clear();
            //ChangeInvoiceNOMessage();
            return 0;


        }

        /// <summary>
        /// ���ݲ����Ż�û��߹Һ���Ϣ
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Register getRegInfo(string CardNo)
        {
            if (string.IsNullOrEmpty(CardNo))
            {
                return null;
            }

            FS.HISFC.Models.Registration.Register ObjReg = new FS.HISFC.Models.Registration.Register();
            FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();
            FS.HISFC.Models.RADT.PatientInfo p;
            int regCount = this.regMgr.QueryRegiterByCardNO(CardNo);

            if (regCount == 1)
            {
                ObjReg.IsFirst = false;
            }
            else
            {
                if (regCount == 0)
                {
                    ObjReg.IsFirst = true;

                }
                else
                {
                    return null;
                }
            }
            //�ȼ������߻�����Ϣ��,���Ƿ���ڸû�����Ϣ
            p = radt.QueryComPatientInfo(CardNo);

            if (p == null || p.Name == "")
            {
                //�����ڻ�����Ϣ
                ObjReg.PID.CardNO = CardNo;
                //ObjReg.IsFirst = true;
                ObjReg.Sex.ID = "M";
                //ObjReg.Pact.ID = this.DefaultPactID;
            }
            else
            {
                //���ڻ��߻�����Ϣ,ȡ������Ϣ

                ObjReg.PID.CardNO = CardNo;
                ObjReg.Name = p.Name;
                ObjReg.Sex.ID = p.Sex.ID;
                ObjReg.Birthday = p.Birthday;
                ObjReg.Pact.ID = p.Pact.ID;
                ObjReg.Pact.PayKind.ID = p.Pact.PayKind.ID;
                ObjReg.SSN = p.SSN;
                ObjReg.PhoneHome = p.PhoneHome;
                ObjReg.AddressHome = p.AddressHome;
                ObjReg.IDCard = p.IDCard;
                ObjReg.NormalName = p.NormalName;
                ObjReg.IsEncrypt = p.IsEncrypt;
                //{6B6167F7-3A9B-4f6c-9326-C5CD6AA3AC98}
                ObjReg.IDCard = p.IDCard;

                if (p.IsEncrypt == true)
                {
                    ObjReg.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(p.NormalName);
                }
                //this.chbEncrpt.Checked = p.IsEncrypt;
                ////ObjReg.IsFirst = false;

                //if (this.validCardType(p.IDCardType.ID))//����Memo�洢֤�����
                //{
                //    ObjReg.CardType.ID = p.IDCardType.ID;

                //}
            }

            return ObjReg;
        }

        /// <summary>
        /// ����������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ��ȡ�Һ���Ϣ
        /// </summary>
        /// <returns></returns>
        private int getValue(out List<AccountCardFee> lstAccFee)
        {
          
            FS.HISFC.Models.RADT.PatientInfo patient = this.ucRegPatientInfo1.patientInfo;
            regObj = getRegInfo(patient.PID.CardNO);
            lstAccFee = null;
            //�����
            this.regObj.ID = this.regMgr.GetSequence("Registration.Register.ClinicID");
            this.regObj.TranType = FS.HISFC.Models.Base.TransTypes.Positive;//������

            this.regObj.DoctorInfo.Templet.RegLevel.ID = RegisterLevel;//this.cmbRegLevel.Tag.ToString();
            //this.regObj.DoctorInfo.Templet.RegLevel.Name = this.cmbRegLevel.Text;
            //{156C449B-60A9-4536-B4FB-D00BC6F476A1}
            this.regObj.DoctorInfo.Templet.RegLevel.IsEmergency = false;

            this.regObj.DoctorInfo.Templet.Dept.ID = registerDept;
            //this.regObj.DoctorInfo.Templet.Dept.Name = this.cmbDept.Text;

            //this.regObj.DoctorInfo.Templet.Doct.ID = this.cmbDoctor.Tag.ToString();
            //this.regObj.DoctorInfo.Templet.Doct.Name = this.cmbDoctor.Text;

            //{0BA561B1-376F-4412-AAD0-F19A0C532A03}
            this.regObj.Name = patient.Name;//FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtName.Text.Trim(), "'");//��������
            this.regObj.Sex.ID = patient.Sex.ID;// this.cmbSex.Tag.ToString();//�Ա�

            this.regObj.Birthday = patient.Birthday;//this.dtBirthday.Value;//��������			

            //FS.HISFC.Models.Registration.RegLevel level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
            //this.regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Reg;
            ////��Ϊ��˵����ԤԼ��
            //if (this.txtOrder.Tag != null)
            //{
            //    this.regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Pre;
            //}
            //else if (level.IsSpecial)
            //{
            //    this.regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Spe;
            //}

            //FS.HISFC.Models.Registration.Schema schema = null;

            ////ֻ��ר�ҡ�ר�ơ�������Ҫ���뿴��ʱ��Ρ������޶�
            //if (this.regObj.RegType != FS.HISFC.Models.Base.EnumRegType.Pre
            //            && (level.IsSpecial || level.IsFaculty || level.IsExpert))
            //{
            //    schema = this.GetValidSchema(level);
            //    if (schema == null)
            //    {
            //        MessageBox.Show("ԤԼʱ��ָ������,û�з����������Ű���Ϣ!", "��ʾ");
            //        this.dtBookingDate.Focus();
            //        return -1;
            //    }
            //    this.SetBookingTag(schema);
            //}


            //if (level.IsExpert && this.regObj.RegType != FS.HISFC.Models.Base.EnumRegType.Pre)
            //{
            //    if (this.VerifyIsProfessor(level, schema) == false)
            //    {
            //        this.cmbRegLevel.Focus();
            //        return -1;
            //    }
            //}


            #region �������
            this.regObj.Pact.ID = patient.Pact.ID;//this.cmbPayKind.Tag.ToString();//��ͬ��λ
            //this.regObj.Pact.Name = this.cmbPayKind.Text;

            FS.HISFC.Models.Base.PactInfo pact = conMgr.GetPactUnitInfoByPactCode(this.regObj.Pact.ID);
            if (pact == null || pact.ID == "")
            {
                MessageBox.Show("��ȡ����Ϊ:" + this.regObj.Pact.ID + "�ĺ�ͬ��λ��Ϣ����!" + this.conMgr.Err, "��ʾ");
                return -1;
            }
            this.regObj.Pact.Name = pact.Name;
            this.regObj.Pact.PayKind.Name = pact.PayKind.Name;
            this.regObj.Pact.PayKind.ID = pact.PayKind.ID;
            //this.regObj.SSN = this.txtMcardNo.Text.Trim();//ҽ��֤��

            //if (pact.IsNeedMCard && this.regObj.SSN == "")
            //{
            //    MessageBox.Show("��Ҫ����ҽ��֤��!", "��ʾ");
            //    this.txtMcardNo.Focus();
            //    return -1;
            //}
            ////��Ա�������ж�
            //if (this.validMcardNo(this.regObj.Pact.ID, this.regObj.SSN) == -1)
            //    return -1;

            #endregion

            this.regObj.PhoneHome = patient.PhoneHome;//FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtPhone.Text.Trim(), "'");//��ϵ�绰
            this.regObj.AddressHome = patient.AddressHome;//FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtAddress.Text.Trim(), "'");//��ϵ��ַ
            //this.regObj.CardType.ID = this.cmbCardType.Tag.ToString();

            #region ԤԼʱ���
            //if (this.regObj.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)//ԤԼ�ſ��Ű��޶�
            //{
            //    this.regObj.IDCard = (this.txtOrder.Tag as FS.HISFC.Models.Registration.Booking).IDCard;
            //    this.regObj.DoctorInfo.Templet.Noon.ID = (this.txtOrder.Tag as FS.HISFC.Models.Registration.Booking).DoctorInfo.Templet.Noon.ID;
            //    this.regObj.DoctorInfo.Templet.IsAppend = (this.txtOrder.Tag as FS.HISFC.Models.Registration.Booking).DoctorInfo.Templet.IsAppend;
            //    this.regObj.DoctorInfo.SeeDate = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
            //        this.dtBegin.Value.ToString("HH:mm:ss"));//�Һ�ʱ��
            //    this.regObj.DoctorInfo.Templet.Begin = this.regObj.DoctorInfo.SeeDate;
            //    this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
            //        this.dtEnd.Value.ToString("HH:mm:ss"));//����ʱ��
            //    this.regObj.DoctorInfo.Templet.ID = (this.txtOrder.Tag as FS.HISFC.Models.Registration.Booking).DoctorInfo.Templet.ID;
            //}
            //else if (level.IsSpecial || level.IsExpert || level.IsFaculty)//ר�ҡ�ר�ơ�����ſ��Ű��޶�
            //{
            //    this.regObj.DoctorInfo.Templet.Noon.ID = (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.Noon.ID;
            //    this.regObj.DoctorInfo.Templet.IsAppend = (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.IsAppend;
            //    this.regObj.DoctorInfo.SeeDate = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
            //        this.dtBegin.Value.ToString("HH:mm:ss"));//�Һ�ʱ��
            //    this.regObj.DoctorInfo.Templet.Begin = this.regObj.DoctorInfo.SeeDate;
            //    this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
            //        this.dtEnd.Value.ToString("HH:mm:ss"));//����ʱ��
            //    this.regObj.DoctorInfo.Templet.ID = (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.ID;

            //}
            //else//�����Ų����޶�
            //{
            //    this.regObj.DoctorInfo.SeeDate = this.regMgr.GetDateTimeFromSysDateTime();
            //    this.regObj.DoctorInfo.Templet.Begin = DateTime.Parse(this.regObj.DoctorInfo.SeeDate.Date.ToString("yyyy-MM-dd") + " " +
            //            this.dtBegin.Value.ToString("HH:mm:ss"));
            //    this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.regObj.DoctorInfo.SeeDate.Date.ToString("yyyy-MM-dd") + " " +
            //            this.dtEnd.Value.ToString("HH:mm:ss"));

            //    ///����Һ����ڴ��ڽ���,ΪԤԼ�����յĺ�,���¹Һ�ʱ��
            //    ///
            //    if (this.regObj.DoctorInfo.SeeDate.Date < this.dtBookingDate.Value.Date)
            //    {
            //        this.regObj.DoctorInfo.SeeDate = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
            //            this.dtBegin.Value.ToString("HH:mm:ss"));//�Һ�ʱ��
            //        this.regObj.DoctorInfo.Templet.Begin = this.regObj.DoctorInfo.SeeDate;
            //        this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
            //            this.dtEnd.Value.ToString("HH:mm:ss"));//����ʱ��

            //        this.regObj.DoctorInfo.Templet.Noon.ID = this.getNoon(this.regObj.DoctorInfo.Templet.Begin);
            //    }
            //    else
            //    {
            //        this.regObj.DoctorInfo.Templet.Noon.ID = this.getNoon(this.regObj.DoctorInfo.SeeDate);
            //    }


            //    if (this.regObj.DoctorInfo.Templet.Noon.ID == "")
            //    {
            //        MessageBox.Show("δά�������Ϣ,����ά��!", "��ʾ");
            //        return -1;
            //    }
            //    this.regObj.DoctorInfo.Templet.ID = "";
            //}
            #endregion

            //if (this.regObj.Pact.PayKind.ID == "03")//���������ж�
            //{
            //    if (this.IsAllowPubReg(this.regObj.PID.CardNO, this.regObj.DoctorInfo.SeeDate) == -1)
            //        return -1;
            //}

            regObj.DoctorInfo.Templet.Noon.ID = "1";
            #region �Һŷ�
            int rtn = ConvertRegFeeToObject(regObj);
            if (rtn == -1)
            {
                MessageBox.Show("��ȡ�Һŷѳ���!" + this.regFeeMgr.Err, "��ʾ");
                //this.cmbRegLevel.Focus();
                return -1;
            }
            if (rtn == 1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�ùҺż���δά���Һŷ�,����ά���Һŷ�"), "��ʾ");
                //this.cmbRegLevel.Focus();
                return -1;
            }

            //��û���Ӧ�ա�����
            ConvertCostToObject(regObj, out lstAccFee);

            #endregion

            //������
            //  this.regObj.InvoiceNO = this.txtRecipeNo.Text.Trim();
            this.regObj.RecipeNO = this.conMgr.GetConstansObj("RegRecipeNo", regMgr.Operator.ID).Name;//this.txtRecipeNo.Text.Trim();


            this.regObj.IsFee = false;
            this.regObj.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Valid;
            this.regObj.IsSee = false;
            this.regObj.InputOper.ID = this.regMgr.Operator.ID;
            this.regObj.InputOper.OperTime = this.regMgr.GetDateTimeFromSysDateTime();
            //add by niuxinyuan
            this.regObj.DoctorInfo.SeeDate = this.regObj.InputOper.OperTime;
            //this.regObj.DoctorInfo.Templet.Noon.Name = this.QeryNoonName(this.regObj.DoctorInfo.Templet.Noon.ID);
            // add by niuxinyuan
            this.regObj.CancelOper.ID = "";
            this.regObj.CancelOper.OperTime = DateTime.MinValue;
            ArrayList al = new ArrayList();

            //if (this.chbEncrpt.Checked)
            //{
            //    this.regObj.IsEncrypt = true;
            //    this.regObj.NormalName = FS.FrameWork.WinForms.Classes.Function.Encrypt3DES(this.regObj.Name);
            //    this.regObj.Name = "******";
            //}

            //this.regObj.IDCard = this.txtIdNO.Text;
            this.regObj.IsFee = true;
            return 0;
        }

        /// <summary>
        /// �Һ�ʵ��
        /// </summary>
        private FS.HISFC.Models.Registration.Register regObj;

        /// <summary>
        /// ��ͬ��λ������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ����ҽ������ҵĿ������
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="doctID"></param>
        /// <param name="noonID"></param>
        /// <param name="regDate"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int UpdateSeeID(string deptID, string doctID, string noonID, DateTime regDate,
            ref int seeNo, ref string Err)
        {
            string Type = "", Subject = "";

            #region ""

            if (doctID != null && doctID != "")
            {
                Type = "1";//ҽ��
                Subject = doctID;
            }
            else
            {
                Type = "2";//����
                Subject = deptID;
            }

            #endregion

            //���¿������
            if (this.regMgr.UpdateSeeNo(Type, regDate, Subject, noonID) == -1)
            {
                Err = this.regMgr.Err;
                return -1;
            }

            //��ȡ�������		
            if (this.regMgr.GetSeeNo(Type, regDate, Subject, noonID, ref seeNo) == -1)
            {
                Err = this.regMgr.Err;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// ����ȫԺ�������
        /// </summary>
        /// <param name="rMgr"></param>
        /// <param name="current"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int Update(FS.HISFC.BizLogic.Registration.Register rMgr, DateTime current, ref int seeNo,
            ref string Err)
        {
            //���¿������
            //ȫԺ��ȫ����������������Ч��Ĭ�� 1
            if (rMgr.UpdateSeeNo("4", current, "ALL", "1") == -1)
            {
                Err = rMgr.Err;
                return -1;
            }

            //��ȡȫԺ�������
            if (rMgr.GetSeeNo("4", current, "ALL", "1", ref seeNo) == -1)
            {
                Err = rMgr.Err;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// ϵͳ��������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParma = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// ��Ӧ�ɽ��תΪ�Һ�ʵ��,
        /// ���Բ�����Ϊref�������� TNND
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private int ConvertRegFeeToObject(FS.HISFC.Models.Registration.Register obj)
        {
            decimal regFee = 0, chkFee = 0, digFee = 0, othFee = 0;

            int rtn = this.GetRegFee(obj.Pact.ID, obj.DoctorInfo.Templet.RegLevel.ID,
                          ref regFee, ref chkFee, ref digFee, ref othFee);

            obj.RegLvlFee.RegFee = regFee;
            obj.RegLvlFee.ChkFee = chkFee;
            obj.RegLvlFee.OwnDigFee = digFee;
            obj.RegLvlFee.OthFee = othFee;

            return rtn;
        }

        private int iFeeDiagReg = 3;

        /// <summary>
        /// ��ȡ�Һŷ�
        /// </summary>
        /// <param name="pactID"></param>
        /// <param name="regLvlID"></param>
        /// <param name="regFee"></param>
        /// <param name="chkFee"></param>
        /// <param name="digFee"></param>
        /// <param name="othFee"></param>
        /// <returns></returns>
        private int GetRegFee(string pactID, string regLvlID, ref decimal regFee, ref decimal chkFee, ref decimal digFee, ref decimal othFee)
        {
            FS.HISFC.Models.Registration.RegLvlFee p = this.regFeeMgr.Get(pactID, regLvlID);
            if (p == null)//����
            {
                return -1;
            }
            if (p.ID == null || p.ID == "")//û��ά���Һŷ�
            {
                return 1;
            }

            regFee = p.RegFee;
            chkFee = p.ChkFee;
            digFee = p.OwnDigFee;
            othFee = p.OthFee;

            //�ж��Ƿ�ֻ��ȡ�Һŷ�
            switch (this.iFeeDiagReg)
            {
                case 1:
                    // ��ȡ�Һŷ�
                    chkFee = 0;
                    digFee = 0;
                    break;
                case 2:
                    // ��ȡ���
                    regFee = 0;
                    break;
                case 3:
                    // ����ȡ��𡢹Һ�
                    regFee = 0;
                    chkFee = 0;
                    digFee = 0;
                    break;

                default:
                    // Ĭ�϶���ȡ
                    break;

            }

            return 0;
        }

        /// <summary>
        /// �Һŷѹ�����
        /// </summary>
        private FS.HISFC.BizLogic.Registration.RegLvlFee regFeeMgr = new FS.HISFC.BizLogic.Registration.RegLvlFee();

        /// <summary>
        /// ��Ӧ�ɽ��תΪ�Һ�ʵ��,
        /// ���Բ�����Ϊref�������� TNND
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="lstCardFee"></param>
        private void ConvertCostToObject(FS.HISFC.Models.Registration.Register obj, out List<AccountCardFee> lstCardFee)
        {
            lstCardFee = null;
            decimal othFee = 0, ownCost = 0, pubCost = 0;
            othFee = obj.RegLvlFee.OthFee; //add by niux
            //lstCardFee = this.getCost(obj.RegLvlFee.RegFee, obj.RegLvlFee.ChkFee, obj.RegLvlFee.OwnDigFee,
            //        ref othFee, ref ownCost, ref pubCost, this.regObj.PID.CardNO);
            lstCardFee = new List<AccountCardFee>();

            AccountCardFee cardFee = new AccountCardFee();

            cardFee.FeeType = AccCardFeeType.RegFee;
            cardFee.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
            cardFee.IStatus = 1;
            cardFee.Own_cost = ownCost;
            cardFee.Pub_cost = pubCost;
            cardFee.Tot_cost = ownCost + pubCost;
            lstCardFee.Add(cardFee);

            obj.RegLvlFee.OthFee = othFee;
            obj.OwnCost = ownCost;
            obj.PubCost = pubCost;

        }

        string registerLevel = "1";

        /// <summary>
        /// Ĭ�ϹҺż���
        /// </summary>
        public string RegisterLevel
        {
            get
            {
                return registerLevel;
            }
            set
            {
                registerLevel = value;
            }
        }

        string registerDept = "2001";

        /// <summary>
        /// Ĭ�ϹҺſ���
        /// </summary>
        public string RegisterDept
        {
            get
            {
                return registerDept;
            }
            set
            {
                registerDept = value;
            }
        }

        /// <summary>
        /// ��ӡ�Һŷ�Ʊ
        /// </summary>
        /// <param name="regObj"></param>
        private void PrintReg(FS.HISFC.Models.Registration.Register regObj, FS.HISFC.BizLogic.Registration.Register regmr)
        {
            #region ����
            /*if( this.PrintWhat == "Invoice")//��ӡ��Ʊ
            {
                this.ucInvoice.Registeration = regObj ;
			
                System.Drawing.Printing.PaperSize size ;

                if( PrintCnt % 2 == 0)
                {
                    size = new System.Drawing.Printing.PaperSize("RegInvoice1", 425 ,288);
                }
                else
                {
                    size = new System.Drawing.Printing.PaperSize("RegInvoice2",425,280) ;
                }

                PrintCnt ++ ;

                printer.SetPageSize(size);
                printer.PrintPage(0,0,ucInvoice) ;
            }
            else//��ӡ����
            {
                //fuck
                FS.neuFC.Object.neuObject obj = this.conMgr.Get("PrintRecipe",regObj.RegDept.ID) ;

                //�������ģ�����ӡ
                if( obj == null || obj.ID == "")
                {
                    this.ucBill.Register = regObj ;
					
                    System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("Recipe", 670 ,1120);
                    printer.SetPageSize(size);
                    printer.PrintPage(0,0,this.ucBill) ;
                }
            }*/
            #endregion
            #region by niuxy
            /*
            try
            {
                if (IRegPrint != null)
                {
                    this.IRegPrint.RegInfo = regObj;
                    this.IRegPrint.Print();
                }
            }
            catch { }
             */
            #endregion
            FS.HISFC.BizProcess.Interface.Registration.IRegPrint regprint = null;
            
            regprint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Registration.IRegPrint)) as FS.HISFC.BizProcess.Interface.Registration.IRegPrint;
            if (regprint == null)
            {
                MessageBox.Show(FS.FrameWork.WinForms.Classes.UtilInterface.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //regprint.SetPrintValue(regObj,regmr);
            if (regObj.IsEncrypt)
            {
                regObj.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(this.regObj.NormalName);
            }

            regprint.SetPrintValue(regObj);
            int i = regprint.Print();
            //regprint.PrintView();

        }

        #region ���֤�Զ���ȡ

        /// <summary>
        /// 
        /// </summary>
        private bool isReaderIDCard = false;

        /// <summary>
        /// 
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ��Զ���ȡ���֤"), DefaultValue(false)]
        public bool IsReaderIDCard
        {
            get
            {
                return isReaderIDCard;
            }
            set
            {
                isReaderIDCard = value;

                this.timer1.Enabled = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.ucRegPatientInfo1.Clear(true);
            int i = this.ucRegPatientInfo1.AutoReadIDCard();

            if (i == -2)
            {
                this.timer1.Enabled = false;

                return;
            }
            if (i == 1)
            {
                this.ucRegPatientInfo1_OnEnterSelectPatient(null, null);
                if (this.isQueryPatientInfoByReadIDCard)
                {
                    this.QueryPatientInfo();
                }
                if (this.ucRegPatientInfo1.IsJudgePactByIdno)
                {
                    this.ucRegPatientInfo1.JudgePactByIdno();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucCardManager_Leave(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucCardManager_Enter(object sender, EventArgs e)
        {
            if (IsReaderIDCard)
            {
                this.timer1.Enabled = true;
            }
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                //�Ƿ�ʹ�ÿ�ݼ��˳�����
                this.FindForm().Close();

            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
        
}
