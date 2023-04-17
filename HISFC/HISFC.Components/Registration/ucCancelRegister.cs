using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
//{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
using FS.HISFC.Models.Registration;
using FS.HISFC.Models.Account;
using System.Collections.Generic;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Registration
{
    /// <summary>
    /// �˺�/ע��
    /// </summary>
    public partial class ucCancelRegister : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucCancelRegister()
        {
            InitializeComponent();

            this.fpSpread1.KeyDown  += new KeyEventHandler(fpSpread1_KeyDown);
            this.txtInvoice.KeyDown += new KeyEventHandler(txtInvoice_KeyDown);
            this.txtIDCard.KeyDown += new KeyEventHandler(txtIDCard_KeyDown);
            this.txtCardNo.KeyDown  += new System.Windows.Forms.KeyEventHandler(this.txtCardNo_KeyDown);
            this.txtName.KeyDown += new KeyEventHandler(txtName_KeyDown);
            this.fpSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(fpSpread1_SelectionChanged);

            this.fpSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpSpread1_ButtonClicked);
            this.neuSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread1_ButtonClicked);
            this.neuSpread2.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread2_ButtonClicked);

            this.Init();
        }

        #region ��
        private AppointmentService NetService = new AppointmentService();
        /// <summary>
        /// ԤԼ����������
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Appointment appointmentMgr = new FS.HISFC.BizLogic.Registration.Appointment();
        /// <summary>
        /// �ҺŹ�����
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
        /// <summary>
        /// �ʻ�����
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accMgr = new FS.HISFC.BizLogic.Fee.Account();
        /// <summary>
        /// �������
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Assign assignMgr = new FS.HISFC.BizLogic.Nurse.Assign();
        /// <summary>
        /// ���ƹ�����
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();
        /// <summary>
        /// ϵͳ��������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParma = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        /// <summary>
        /// �Ű������
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Schema schMgr = new FS.HISFC.BizLogic.Registration.Schema();
        /// <summary>
        /// ����������
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constantManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// ����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();
        private FS.HISFC.BizProcess.Integrate.RADT radtMgr = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// ֧����ʽ
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper payWayHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ���˺�����
        /// </summary>
        private int PermitDays = 0;

        //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        private bool isQuitAccount = false;

        /// <summary>
        /// �Ƿ��ӡ�˺�Ʊ
        /// {B700292D-50A6-4cdf-8B03-F556F990BB9B}
        /// </summary>
        private bool isPrintBackBill = false;


        private bool isCallYBInterface = true;


        private bool isatm=false;

      
        #endregion

        //{B700292D-50A6-4cdf-8B03-F556F990BB9B}
        #region ����

        private bool isCardFeeCanReturn = false;

        /// <summary>
        /// �Ƿ��ӡ�˺�Ʊ
        /// </summary>
        [Category("�ؼ�����"), Description("�������Ƿ�����˷�,Ĭ��false"), DefaultValue(false)]
        public bool IsCardFeeCanReturn
        {
            set
            {
                this.isCardFeeCanReturn = value;
            }
            get
            {
                return this.isCardFeeCanReturn;
            }
        }

        /// <summary>
        /// �Ƿ��ӡ�˺�Ʊ
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ��ӡ�˺�Ʊ(δʵ��)"), DefaultValue(false)]
        public bool IsPrintBackBill
        {
            set
            {
                this.isPrintBackBill = value;
            }
            get
            {
                return this.isPrintBackBill;
            }
        }
        /// <summary>
        /// �Ƿ�������ԤԼ�˺�
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�������ԤԼ�˺�"), DefaultValue(false)]
        public bool IsNetCancle { get; set; }

        /// <summary>
        /// �Ƿ��ж�Ȩ��
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ��ж�Ȩ��"), DefaultValue(true)]
        public bool IsJudePrivileged { get { return isJudePrivileged; } set { this.isJudePrivileged = value; } }

        private bool isJudePrivileged = true;
        /// <summary>
        /// �Ƿ�������ԤԼ�˺�
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�������ԤԼ�Һŷ�"), DefaultValue(false)]
        public bool IsReturnNetFee { get; set; }
        /// <summary>
        /// �Ƿ��ӡ�˺�Ʊ
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ��ⷢƱ�Ƿ�����˷�/�Ϻ�"), DefaultValue(false)]
        public bool IsCheckInvoice
        {
            set
            {
                this.isCheckInvoic = value;
            }
            get
            {
                return this.isCheckInvoic;
            }
        }
        private bool isCheckInvoic = true;
        /// <summary>
        /// //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�ר���˺�"), DefaultValue(false)]
        public bool IsATM
        {
            get { return isatm; }
            set { isatm = value; }
        }

        /// <summary>
        /// //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        /// </summary>
        [Category("�ؼ�����"), Description("�ʻ������Ƿ����ʻ�"), DefaultValue(false)]
        public bool IsQuitAccount
        {
            get { return isQuitAccount; }
            set { isQuitAccount = value; }
        }

        //{182DA62D-6BCE-4c4c-956F-6F2A363138A0}
        private bool isSeeedCanCancelRegInfo = false;

        //{182DA62D-6BCE-4c4c-956F-6F2A363138A0}
        [Category("�ؼ�����"), Description("�ѿ���Һż�¼�Ƿ����˺ţ�"), DefaultValue(false)]
        public bool IsSeeedCanCancelRegInfo
        {
            get { return isSeeedCanCancelRegInfo; }
            set { isSeeedCanCancelRegInfo = value; }
        }

        private bool isMustAllReturnFee = true;
        //{182DA62D-6BCE-4c4c-956F-6F2A363138A0}
        [Category("�ؼ�����"), Description("�˺��Ƿ����ȫ�����з��ã�true:����ȫ�ˣ�"), DefaultValue(true)]
        public bool IsMustAllReturnFee
        {
            get { return isMustAllReturnFee; }
            set { isMustAllReturnFee = value; }
        }

        /// <summary>
        /// �Ƿ����ϲ���
        /// </summary>
        private bool isUseLogout = false;

        [Category("�ؼ�����"), Description("�˺��Ƿ��������Ϲ��ܣ�True = �ǣ�False = ��"), DefaultValue(false)]
        public bool IsUseLogout
        {
            get
            {
                return isUseLogout;
            }
            set
            {
                isUseLogout = value;
            }
        }
        /// <summary>
        /// �Һ�״̬ö�������ݿⶨ���Ƿ�һ��
        /// </summary>
        private bool isEnumEqualDataBase = true;
        /// <summary>
        /// �Һ�״̬ö�������ݿⶨ���Ƿ�һ��
        /// </summary>
        [Category("�ؼ�����"), Description("�Һ�״̬ö�ٶ��������ݿⶨ���Ƿ�һ�£�Ĭ��true"), DefaultValue(true)]
        public bool IsEnumEqualDataBase
        {
            get
            {
                return this.isEnumEqualDataBase;
            }
            set
            {
                this.isEnumEqualDataBase = value;
            }
        }
        #endregion

        #region ҽ���ӿ�

        /// <summary>
        /// ҽ���ӿڴ��������
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        /// <summary>
        /// ����Һţ��Һŷ���otherfee������ 0:����(��ҽר��) 1���������� 2��������
        /// </summary>
        string otherFeeType = string.Empty;

        #endregion

        #region ����
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            //֧����ʽ
            ArrayList al = constantManager.GetList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            if (al == null)
            {
                MessageBox.Show("��ȡ֧����ʽʧ��!");
            }
            else
            {
                payWayHelper.ArrayObject = al;
            }

            //�����˺ţ������˺�����
            string Days = this.ctlMgr.QueryControlerInfo("400006");

            if (Days == null || Days == "" || Days == "-1")
            {
                this.PermitDays = 1;
            }
            else
            {
                this.PermitDays = int.Parse(Days);
            }

            //����Һţ��Һŷ���otherfee������ 0:����(��ҽר��) 1���������� 2��������
            //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
            Days = this.ctlMgr.QueryControlerInfo("400027");

            if (string.IsNullOrEmpty(Days))
            {
                Days = "2"; //Ĭ��������
            }

            this.isCallYBInterface = this.controlParma.GetControlParam<bool>("MZ9931",false,true);

            this.otherFeeType = Days;

            this.txtCardNo.Focus();

            return 0;
        }

        /// <summary>
        /// ���ý���
        /// </summary>
        private void SetNameFocus()
        {
            this.txtName.SelectAll();
            this.txtName.Focus();
        }

        /// <summary>
        /// ���ý���
        /// </summary>
        private void SetCardNoFocus()
        {
            this.txtCardNo.SelectAll();
            this.txtCardNo.Focus();
        }
        /// <summary>
        /// ���ý���
        /// </summary>
        private void SetInvoiceFocus()
        {
            this.txtInvoice.SelectAll();
            this.txtInvoice.Focus();
        }
        /// <summary>
        /// �����ʾ��Ϣ
        /// </summary>
        private void Clear()
        {
            this.fpSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread2_Sheet1.RowCount = 0;

            this.lbTot.Text = "";
            this.lbReturn.Text = "";
        }

        /// <summary>
        /// ����
        /// </summary>
        private void ClearAll()
        {
            Clear();
            this.txtName.Text = "";
            this.txtCardNo.Text = "";
            this.txtInvoice.Text = "";
            this.txtIDCard.Text = "";
            this.txtCardNo.Focus();
        }
        /// <summary>
        /// ���ݲ����Ų�ѯ����Ϣ
        /// </summary>
        /// <param name="IDCard"></param>
        private void QueryRegisterByIDCard(string IDCard)
        {
            this.Clear();

            //����������Ч��
            ArrayList tarlRegInfo = this.regMgr.GetByIDCard(IDCard);
            if (tarlRegInfo == null || tarlRegInfo.Count < 1)
            {
                MessageBox.Show("δ�����������֤����Ч�Һ���Ϣ", "��ʾ", MessageBoxButtons.OK);
                return;
            }

            ArrayList arlRegInfo = new ArrayList();
            if (isatm)
            {
                foreach (Register tinfo in tarlRegInfo)
                {
                    if (tinfo.IsAccount)
                    {
                        arlRegInfo.Add(tinfo);
                    }
                }
            }
            else if (IsNetCancle)
            {
                foreach (Register tinfo in tarlRegInfo)
                {
                    if (isNetInvoice(tinfo.InvoiceNO))
                    {
                        arlRegInfo.Add(tinfo);
                    }
                }
            } else
            {
                foreach (Register tinfo in tarlRegInfo)
                {
                    if (!tinfo.IsAccount && !isNetInvoice(tinfo.InvoiceNO))
                    {
                        arlRegInfo.Add(tinfo);
                    }
                }
            }

            if (arlRegInfo == null || arlRegInfo.Count == 0)
            {
                MessageBox.Show("δ��������ػ��߹Һ���Ϣ" + this.regMgr.Err, "��ʾ");
                return;
            }

            foreach (FS.HISFC.Models.Registration.Register r in arlRegInfo)
            {
                List<AccountCardFee> lstCardFee = null;

                FS.HISFC.Models.Account.AccountCard accountObj = new FS.HISFC.Models.Account.AccountCard();
                if (this.feeMgr.ValidMarkNO(r.PID.CardNO, ref accountObj) == -1)
                {
                    MessageBox.Show(feeMgr.Err);
                    SetCardNoFocus();
                    return;
                }

                int iRes = this.accMgr.QueryAccCardFeeDirectory(r.PID.CardNO, out lstCardFee);

                if (lstCardFee != null && lstCardFee.Count > 0)
                {
                    for (int idx = 0; idx < lstCardFee.Count; idx++)
                    {
                        lstCardFee[idx].Patient = accountObj.Patient;
                    }
                    AddCardFeeNoRegister(lstCardFee);
                }
            }



            if (arlRegInfo != null && arlRegInfo.Count > 0)
            {

                //ֻ�ҵ�һ���Һż�¼
                if (arlRegInfo.Count == 1)
                {
                    this.addRegister(arlRegInfo);
                }
                else
                {
                    //�����Һż�¼�����շ�Ա�Լ�ȥѡ��
                    ucShowRegisterInfo ucShow = new ucShowRegisterInfo();
                    ucShow.SelectedRegister += new ucShowRegisterInfo.GetRegister(ucShow_SelectedRegister);
                    ucShow.SetRegisterInfo(arlRegInfo);
                    Form fpShow = new Form();
                    fpShow.Width = 600;
                    fpShow.Height = 300;
                    fpShow.Controls.Add(ucShow);
                    fpShow.ShowDialog();
                }
            }
        }

        /// <summary>
        /// ����������ѯ�Һż�¼
        /// </summary>
        /// <param name="name"></param>
        private void QueryRegisterByName(string name)
        {
            this.Clear();
            FS.HISFC.Models.Account.AccountCard accountObj = new FS.HISFC.Models.Account.AccountCard();


            DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-this.PermitDays);

            //����������Ч��
            ArrayList tarlRegInfo = this.regMgr.QueryName(name, permitDate);
            ArrayList arlRegInfo = new ArrayList();
            if (isatm)
            {
                foreach (Register tinfo in tarlRegInfo)
                {
                    if (tinfo.IsAccount)
                    {
                        arlRegInfo.Add(tinfo);
                    }
                }
            }
            else if (IsNetCancle)
            {
                foreach (Register tinfo in tarlRegInfo)
                {
                    if (isNetInvoice(tinfo.InvoiceNO))
                    {
                        arlRegInfo.Add(tinfo);
                    }
                }
            }
            else
            {
                foreach (Register tinfo in tarlRegInfo)
                {
                    if (!tinfo.IsAccount && !isNetInvoice(tinfo.InvoiceNO))
                    {
                        arlRegInfo.Add(tinfo);
                    }
                }
            }

            if (arlRegInfo == null || arlRegInfo.Count == 0)
            {
                MessageBox.Show("δ��������ػ��߹Һ���Ϣ" + this.regMgr.Err, "��ʾ");
                return;
            }
            //List<AccountCardFee> lstCardFee = null;
            //int iRes = this.accMgr.QueryAccCardFeeDirectory(cardNo, out lstCardFee);

            //if (lstCardFee != null && lstCardFee.Count > 0)
            //{
            //    for (int idx = 0; idx < lstCardFee.Count; idx++)
            //    {
            //        lstCardFee[idx].Patient = accountObj.Patient;
            //    }
            //    AddCardFeeNoRegister(lstCardFee);
            //}

            if (arlRegInfo != null && arlRegInfo.Count > 0)
            {

                //ֻ�ҵ�һ���Һż�¼
                if (arlRegInfo.Count == 1)
                {
                    this.addRegister(arlRegInfo);
                }
                else
                {
                    //�����Һż�¼�����շ�Ա�Լ�ȥѡ��
                    ucShowRegisterInfo ucShow = new ucShowRegisterInfo();
                    ucShow.SelectedRegister += new ucShowRegisterInfo.GetRegister(ucShow_SelectedRegister);
                    ucShow.SetRegisterInfo(arlRegInfo);
                    Form fpShow = new Form();
                    fpShow.Width = 600;
                    fpShow.Height = 300;
                    fpShow.Controls.Add(ucShow);
                    fpShow.ShowDialog();
                }
            }
        }

        /// <summary>
        /// ���ݲ����Ų�ѯ����Ϣ
        /// </summary>
        /// <param name="cardNo"></param>
        private void QueryRegisterByCardNO(string cardNo)
        {
            this.Clear();
            FS.HISFC.Models.Account.AccountCard accountObj = new FS.HISFC.Models.Account.AccountCard();
            if (this.feeMgr.ValidMarkNO(cardNo, ref accountObj) == -1)
            {
                MessageBox.Show(feeMgr.Err);
                SetCardNoFocus();
                return;
            }

            if (string.IsNullOrEmpty(accountObj.Patient.PID.CardNO))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("������ľ��￨�Ų�����"), "��ʾ");
                SetCardNoFocus();
                return;
            }

            DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-this.PermitDays);
            cardNo = accountObj.Patient.PID.CardNO;

            //����������Ч��
            ArrayList tarlRegInfo = this.regMgr.Query(cardNo, permitDate);
             ArrayList arlRegInfo =new ArrayList();
            if(isatm)
            {
                foreach(Register tinfo in tarlRegInfo)
                {
                   if(tinfo.IsAccount)
                   {
                       arlRegInfo.Add(tinfo);
                   }
                }
            }
            else if (IsNetCancle)
            {
                foreach (Register tinfo in tarlRegInfo)
                {
                    if (isNetInvoice(tinfo.InvoiceNO))
                    {
                        arlRegInfo.Add(tinfo);
                    }
                }
            }
            else
            {
                foreach(Register tinfo in tarlRegInfo)
                {
                   if(!tinfo.IsAccount&&!isNetInvoice(tinfo.InvoiceNO))
                   {
                       arlRegInfo.Add(tinfo);
                   }
                }               
            }

            //if (arlRegInfo == null || arlRegInfo.Count == 0)
            //{
            //    MessageBox.Show("δ��������ػ��߹Һ���Ϣ" + this.regMgr.Err, "��ʾ");
            //    return;
            //}
            List<AccountCardFee> lstCardFee = null;
            int iRes = this.accMgr.QueryAccCardFeeDirectory(cardNo, out lstCardFee);

            if (lstCardFee != null && lstCardFee.Count > 0)
            {
                for (int idx = 0; idx < lstCardFee.Count; idx++)
                {
                    lstCardFee[idx].Patient = accountObj.Patient;
                }
                AddCardFeeNoRegister(lstCardFee);
            }

            if (arlRegInfo != null && arlRegInfo.Count > 0)
            {
               
                //ֻ�ҵ�һ���Һż�¼
                if (arlRegInfo.Count == 1)
                {
                    this.addRegister(arlRegInfo);
                }
                else
                {
                    //�����Һż�¼�����շ�Ա�Լ�ȥѡ��
                    ucShowRegisterInfo ucShow = new ucShowRegisterInfo();
                    ucShow.SelectedRegister += new ucShowRegisterInfo.GetRegister(ucShow_SelectedRegister);
                    ucShow.SetRegisterInfo(arlRegInfo);
                    Form fpShow = new Form();
                    fpShow.Width = 600;
                    fpShow.Height = 300;
                    fpShow.Controls.Add(ucShow);
                    fpShow.ShowDialog();
                }
            }
        }

        /// <summary>
        /// ���ݷ�Ʊ�Ų�ѯ����Ϣ
        /// </summary>
        /// <param name="invoiceNO"></param>
        private void QueryRegisterByInvoiceNO(string invoiceNo)
        {
            this.Clear();
            invoiceNo = invoiceNo.PadLeft(12, '0');
            txtInvoice.Text = invoiceNo;
            DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-this.PermitDays);

            //�ȸ��ݷ�Ʊ�Ų��ҵ�Clinic_Code
            //Ȼ��ͨ��Clinic_Code�ҵ���Ӧ�ļ�¼��Ϣ
            List<AccountCardFee> lstCardFee = null;
            int iRes = this.accMgr.QueryAccountCardFeeByInvoiceNO(invoiceNo, out lstCardFee);
            if (lstCardFee == null || lstCardFee.Count == 0)
            {
                MessageBox.Show("δ������������ط�Ʊ��Ϣ" + this.accMgr.Err, "��ʾ");
                return;
            }

            string clinicCode = lstCardFee[0].ClinicNO;

            //����������Ч��
            if (string.IsNullOrEmpty(clinicCode)==false)
            {
                Register arlRegInfo = this.regMgr.GetByClinic(clinicCode);
                this.addRegister(arlRegInfo);
            }
            else
            {
                //���һ�����Ϣ
                string cardNO = lstCardFee[0].CardNo;
                FS.HISFC.Models.RADT.PatientInfo p= radtMgr.QueryComPatientInfo(cardNO);
                for (int idx = 0; idx < lstCardFee.Count; idx++)
                {
                    lstCardFee[idx].Patient = p;
                }
                AddCardFeeNoRegister(lstCardFee);
                //��ת���շѽ���
                this.neuTabControl1.SelectedTab = this.tabPageDir;
            }

        }

        /// <summary>
        /// ��ӻ��߹Һ���ϸ
        /// </summary>
        /// <param name="registers"></param>
        private void addRegister(ArrayList registers)
        {
            this.fpSpread1.SelectionChanged -= new FarPoint.Win.Spread.SelectionChangedEventHandler(fpSpread1_SelectionChanged);

            try
            {
                if (this.fpSpread1_Sheet1.RowCount > 0)
                    this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

                FS.HISFC.Models.Registration.Register obj;

                for (int i = registers.Count - 1; i >= 0; i--)
                {
                    obj = (FS.HISFC.Models.Registration.Register)registers[i];
                    this.addRegister(obj);
                }
            }
            catch (Exception objEx)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg(objEx.Message), "��ʾ");
            }
            finally
            {
                this.fpSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(fpSpread1_SelectionChanged);
            }
            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                this.fpSpread1_Sheet1.SetActiveCell(0, 1);
                this.fpSpread1_Sheet1.Cells[0, 0].Value = true;
                this.fpSpread1_SelectionChanged(null, null);

                SetReturnCost(this.neuSpread1_Sheet1);
            }

            //�������ʺ��п�
            for (int i = 0; i < this.fpSpread1_Sheet1.Columns.Count; i++)
            {
                this.fpSpread1_Sheet1.Columns[i].Width = this.fpSpread1_Sheet1.Columns[i].GetPreferredWidth();
            }
            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                this.neuSpread1_Sheet1.Columns[i].Width = this.neuSpread1_Sheet1.Columns[i].GetPreferredWidth();
            }
            for (int i = 0; i < this.neuSpread2_Sheet1.Columns.Count; i++)
            {
                this.neuSpread2_Sheet1.Columns[i].Width = this.neuSpread2_Sheet1.Columns[i].GetPreferredWidth();
            }
        }
        /// <summary>
        /// ������ʹ��ֱ���շ����ɵĺ��ٽ��йҺ�
        /// </summary>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        private int ValidCardNO(string CardNO)
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParams = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            string cardRule = controlParams.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.NO_REG_CARD_RULES, false, "9");
            if (CardNO != "" && CardNO != string.Empty)
            {
                if (CardNO.Substring(0, 1) == cardRule)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�˺Ŷ�Ϊֱ���շ�ʹ�ã��������˺�"), FS.FrameWork.Management.Language.Msg("��ʾ"));
                    return -1;
                }
            }
            return 1;
        }
        /// <summary>
        /// ��ʾ�Һ���Ϣ
        /// </summary>
        /// <param name="reg"></param>
        private void addRegister(FS.HISFC.Models.Registration.Register reg)
        {
            this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);

            int cnt = this.fpSpread1_Sheet1.RowCount - 1;

            this.fpSpread1_Sheet1.SetValue(cnt, 1, reg.Name, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 2, reg.Sex.Name, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 3, reg.DoctorInfo.SeeDate.ToString(), false);
            this.fpSpread1_Sheet1.SetValue(cnt, 4, reg.DoctorInfo.Templet.Dept.Name, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 5, reg.DoctorInfo.Templet.RegLevel.Name, false);
            //������ǣ��Ƿ��ѿ���
            this.fpSpread1_Sheet1.SetValue(cnt, 6, reg.IsSee, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 7, reg.DoctorInfo.Templet.Doct.Name, false);
            
            this.fpSpread1_Sheet1.SetValue(cnt, 8, reg.RegLvlFee.RegFee + reg.RegLvlFee.OwnDigFee + reg.RegLvlFee.ChkFee + reg.RegLvlFee.OthFee, false);
            this.fpSpread1_Sheet1.Rows[cnt].Tag = reg;

            if (reg.IsSee)
            {
                this.fpSpread1_Sheet1.Rows[cnt].BackColor = Color.LightCyan;
            }
            if (reg.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back||
                reg.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
            {
                this.fpSpread1_Sheet1.Rows[cnt].BackColor = Color.MistyRose;
            }


            this.fpSpread1_Sheet1.SetActiveCell(cnt, 1);
            this.fpSpread1_Sheet1.Cells[cnt, 0].Value = true;
            this.fpSpread1_SelectionChanged(null, null);
        }
        /// <summary>
        /// ����������ԤԼ�˺�
        /// </summary>
        /// <returns></returns>
        private int ReturnNetRegister()
        {

            // ��ѡ�Һ���Ϣ
            FS.HISFC.Models.Registration.Register regSelect = null;

            #region ��ȡ�˷�����
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                if (this.fpSpread1_Sheet1.Rows.Count >= 2)
                {
                    MessageBox.Show("����ͬʱ�������������ϵĹҺż�¼!");
                    return -1;
                }
                if (fpSpread1_Sheet1.ActiveRow == null)
                {
                    MessageBox.Show("û���ҵ���Ҫ�˷���Ϣ!");
                    return -1;
                }
                regSelect = this.fpSpread1_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Registration.Register;
                bool blnCheck = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 0].Value);
                if (blnCheck)
                {
                    if (regSelect.IsSee && !this.isSeeedCanCancelRegInfo)
                    {
                        MessageBox.Show("�ú��Ѿ�����������˺ţ�", "��ʾ");
                        return -1;
                    }
                }

            }
            #endregion
            if (!isNetInvoice(regSelect.InvoiceNO))
            {
                MessageBox.Show("�˹���ֻ��Ϊ����ԤԼ�˺�,��û��Ȩ�����������˺�/�Ϻ�");
                return -1;
            }

            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();

            #region ����������ԤԼ��Ϣ

            FS.HISFC.Models.Registration.AppointmentOrder app = appointmentMgr.QueryAppointmentOrderByClinicNO(regSelect.ID);
            if (app == null)
            {
                MessageBox.Show("û���ҵ���ԤԼ��Ϣ");
                return -1;
            }
            if (app != null && (app.OrderState == "2" || app.OrderState == "5"))
            {
                MessageBox.Show("�ú����˷�,��������!");
                return -1;
            }

            if (MessageBox.Show("�Ƿ�ȡ��[" + regSelect.Name + "]ԤԼ��ר��[" + regSelect.DoctorInfo.Templet.Doct.Name +
                "]��[" + regSelect.DoctorInfo.Templet.Begin.ToString("yyyy-MM-dd") + "]�ĹҺ���Ϣ?", "��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
                return -1;

            try
            {
                FS.HISFC.Components.Registration.AppointmentService.InvokeResult result =
                    NetService.Invoke_Sync(AppointmentService.funs.refundPay,
                                 app.OrderID,
                                 "2",
                                 current.ToString(),
                                 (app.RegFee + app.TreatFee).ToString()
                                 );

                if (result.ResultCode == "0")
                    MessageBox.Show("֪ͨ������ȡ���Һųɹ�,�Һŷ����˻�����,����Ҫ���ֽ�", "��ʾ");
                else
                {
                    MessageBox.Show("֪ͨ������ȡ���Һ�ʧ��,ԭ��: " + result.ResultDesc, "����");
                    return -1;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("֪ͨ������ȡ���Һ�ʧ��,ԭ��: " + ex.Message, "����");
                return -1;
            }
            #endregion
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("�˺ųɹ�"), "��ʾ");


            this.ClearAll();

            return 0;

        }

        private int DualAccountCardFee(ref List<AccountCardFee> lstAccFee)
        {
            FS.HISFC.Components.Registration.Forms.frmAccountCardFeePayTypeInput frmPayType = new FS.HISFC.Components.Registration.Forms.frmAccountCardFeePayTypeInput();
            frmPayType.AccountCardFeeList = lstAccFee;
            DialogResult dr = frmPayType.ShowDialog();
            if (dr == DialogResult.OK)
            {
                lstAccFee = frmPayType.AccountCardFeeList;
                return 1;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        private int save()
        {
           
            // true=ͨ���ҺŲ������ã�false=ֱ���յ�����
            bool blnReturnRegFee = true;
            // ��ѡ�Һ���Ϣ
            FS.HISFC.Models.Registration.Register regSelect = null;

            // �˺���Ϣ, true=�˺�
            bool blnRegReturn = false;

            decimal returndecimal=0;


            // �˷���Ϣ
            List<AccountCardFee> lstReturnFee = new List<AccountCardFee>();
            // ���˷���Ϣ
            List<AccountCardFee> lstUnRetFee = new List<AccountCardFee>();

            #region ��ȡ�˷�����
            AccountCardFee cardFee = null;
            bool blnCheck = false;
            if (this.neuTabControl1.SelectedIndex == 0)
            {

                //�ж��Ƿ��ܹ��˺�
                //if (this.fpSpread1_Sheet1.Rows.Count <= 0)
                //{
                //    MessageBox.Show("û�п����˷ѵļ�¼!");
                //    return -1;
                //}

                if (this.fpSpread1_Sheet1.Rows.Count >= 2)
                {
                    MessageBox.Show("����ͬʱ�������������ϵĹҺż�¼!");
                    return -1;
                }

                if (this.fpSpread1_Sheet1.Rows.Count == 0)
                {
                    MessageBox.Show("û��ѡ���˷�����!");
                    return -1;
                }

                blnReturnRegFee = true;
                regSelect = this.fpSpread1_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Registration.Register;
                blnCheck = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 0].Value);
                if (blnCheck)
                {
                    FS.HISFC.Models.Base.Employee myemployee = this.regMgr.Operator as FS.HISFC.Models.Base.Employee;

                    if (myemployee.ID.Equals(regSelect.InputOper.ID) == false)
                    {
                        //�ж�Ȩ��,�Ƿ����������Һ�Ա������Ȩ��
                        if (!CommonController.CreateInstance().JugePrive("0811", "02"))
                        {
                            CommonController.CreateInstance().MessageBox("û������������Ա�Һż�¼��Ȩ�ޣ�����Ա��" + CommonController.CreateInstance().GetEmployeeName(regSelect.InputOper.ID), MessageBoxIcon.Warning);
                            return -1;
                        }
                    }



                    if (regSelect.IsSee && !this.isSeeedCanCancelRegInfo)
                    {
                        MessageBox.Show("�ú��Ѿ�����������˺ţ�", "��ʾ");
                        return -1;
                    }
                    else if (regSelect.PVisit.InState.ID.ToString() == "I" || regSelect.PVisit.InState.ID.ToString() == "R")
                    {
                        MessageBox.Show("���ۻ��߲������˺�", "��ʾ");
                        return -1;
                    }

                    blnRegReturn = true;
                }

                for (int idx = 0; idx < this.neuSpread1_Sheet1.RowCount; idx++)
                {
                    cardFee = this.neuSpread1_Sheet1.Rows[idx].Tag as AccountCardFee;
                    blnCheck = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[idx, 0].Value);
                    if (blnCheck)
                    {
                        lstReturnFee.Add(cardFee);
                    }
                    else
                    {
                        lstUnRetFee.Add(cardFee);
                    }
                }

                if (blnRegReturn && IsMustAllReturnFee)
                {
                    if (lstUnRetFee.Count > 0)
                    {
                        MessageBox.Show("�˺ŵ�����£�����ȫ��!");
                        return -1;
                    }
                }
            }
            else
            {
                // ��ֱ����ȡ���� -- ֻ��һ��һ���ˣ�����ȡʱҲ��ֻ��һ��һ�����ȡ
                // ��ʱ�Һ���ϢΪ��

                blnReturnRegFee = false;
                for (int idx = 0; idx < this.neuSpread2_Sheet1.RowCount; idx++)
                {
                    cardFee = this.neuSpread2_Sheet1.Rows[idx].Tag as AccountCardFee;
                    blnCheck = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread2_Sheet1.Cells[idx, 0].Value);
                    if (blnCheck)
                    {
                        lstReturnFee.Add(cardFee);
                    }
                }
            }

            #endregion

            if (!IsNetCancle && regSelect != null && isNetInvoice(regSelect.InvoiceNO))
            {
                MessageBox.Show("�����Һż�¼Ϊ����ԤԼ,��û��Ȩ�������˺�/�Ϻ�");
                return -1;
            }
            if (IsNetCancle && !isNetInvoice(regSelect.InvoiceNO))
            {
                MessageBox.Show("�˴���ֻ��Ϊ����ԤԼ�˺�,��û��Ȩ�����������˺�/�Ϻ�");
                return -1;
            }

            if (blnReturnRegFee)
            {
                if (lstReturnFee == null || lstReturnFee.Count <= 0)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѡ����Ҫ�˷ѵļ�¼��"), "��ʾ");
                    return -1;
                }
            }
            else
            {
                if (lstReturnFee == null || lstReturnFee.Count != 1)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѡ����Ҫ�˷ѵļ�¼����ֻ�ܵ����˷ѣ�"), "��ʾ");
                    return -1;
                }
            }

            //if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("�Ƿ�ȷ�ϴ˲���") + "?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            //{
            //    return -1;
            //}
            #region ѡ��֧����ʽ�˷�

            if (this.DualAccountCardFee(ref lstReturnFee) < 0)
            {
                return -1;
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.schMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.appointmentMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            int rtn;
            FS.HISFC.BizLogic.Registration.EnumUpdateStatus flag = FS.HISFC.BizLogic.Registration.EnumUpdateStatus.Return;
            FS.HISFC.Models.Base.EnumRegisterStatus registerStatus = FS.HISFC.Models.Base.EnumRegisterStatus.Back;

            if (this.neuTabControl1.SelectedIndex == 0)
            {
                if (this.isUseLogout && regSelect.InputOper.ID.Equals(regMgr.Operator.ID) && regSelect.BalanceOperStat.IsCheck == false)
                {
                    flag = FS.HISFC.BizLogic.Registration.EnumUpdateStatus.Bad;
                    registerStatus = FS.HISFC.Models.Base.EnumRegisterStatus.Bad;
                }
                if (IsATM)
                {
                    flag = FS.HISFC.BizLogic.Registration.EnumUpdateStatus.Bad;
                    registerStatus = FS.HISFC.Models.Base.EnumRegisterStatus.Bad;
                }
            }
            else
            {
                if (this.isUseLogout && lstReturnFee.Count > 0 && lstReturnFee[0].Oper.ID.Equals(regMgr.Operator.ID) && lstReturnFee[0].IsBalance==false)
                {
                    flag = FS.HISFC.BizLogic.Registration.EnumUpdateStatus.Bad;
                    registerStatus = FS.HISFC.Models.Base.EnumRegisterStatus.Bad;
                }
            }

            try
            {
                DateTime current = this.regMgr.GetDateTimeFromSysDateTime();

                if (blnReturnRegFee)
                {
                    //���»�ȡ����ʵ��,��ֹ����
                    regSelect = this.regMgr.GetByClinic(regSelect.ID);
                    if (this.ValidCardNO(regSelect.PID.CardNO) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                    //����
                    if (regSelect == null || string.IsNullOrEmpty(regSelect.ID))
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.regMgr.Err, "��ʾ");
                        return -1;
                    }
                    //ҽ�Ʊ����ѽ����ѵģ����ܽ����˺� 
                    if (regSelect.IsFee && feeMgr.QueryFeeItemListsByClinicNO(regSelect.ID).Count > 0)
                    {
                        if (regSelect.Pact.ID == "2")
                        {
                           
                                MessageBox.Show("�ú�Ϊҽ�Ʊ��պ��Ѿ��н��Ѽ�¼,�����˷�!");
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                return -1;
                        }
                        DialogResult dir = MessageBox.Show("�ú��Ѿ��н��Ѽ�¼,�Ƿ�����˺�!", "��ʾ", MessageBoxButtons.YesNo);
                        if (dir == DialogResult.No)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }
                    }

                    //ʹ��,��������
                    if (regSelect.IsSee && !this.isSeeedCanCancelRegInfo)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("�ú��Ѿ�����,��������"), "��ʾ");
                        return -1;
                    }

                    //�Ƿ��Ѿ��˺�
                    if (regSelect.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("�ùҺż�¼�Ѿ��˺ţ������ٴ��˺�"), "��ʾ");
                        return -1;
                    }

                    //�Ƿ��Ѿ�����
                    if (regSelect.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel||regSelect.Status== FS.HISFC.Models.Base.EnumRegisterStatus.Bad)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("�ùҺż�¼�Ѿ����ϣ����ܽ����˺�"), "��ʾ");
                        return -1;
                    }
                }

                FS.HISFC.Models.Base.Employee employee = this.regMgr.Operator as FS.HISFC.Models.Base.Employee;

                // �Ƿ���Ҫ��ȡ��Ʊ��
                bool blnGetInvoice = false;
                if (lstUnRetFee != null && lstUnRetFee.Count > 0)
                {
                    blnGetInvoice = true;
                }

                string strNewInvoiceNo = string.Empty;
                string strNewPrintInvoiceNo = string.Empty;
                int iRes = 0;
                string strErrText = "";

                string strInvoiceType = "R";
                if (blnGetInvoice)
                {
                    iRes = this.feeMgr.GetInvoiceNO(employee, strInvoiceType, ref strNewInvoiceNo, ref strNewPrintInvoiceNo, ref strErrText);

                    if (iRes == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(strErrText);
                        return -1;
                    }
                }

                #region ����ԭ��¼״̬

                string oldInvoiceNo = "";
                if (blnReturnRegFee)
                {
                    oldInvoiceNo = regSelect.InvoiceNO;
                }
                else
                {
                    oldInvoiceNo = lstReturnFee[0].InvoiceNo;
                }

                if (string.IsNullOrEmpty(oldInvoiceNo) == false)
                {
                    //���ݿ��¼��״̬������ö�ٶ����Ƿ�һ��

                    //�ҺŲ���ö�٣�FS.HISFC.BizLogic.Registration.EnumUpdateStatus
                    //        Return = 0,
                    //        ChangeDept = 1,
                    //        Cancel = 2,
                    //        PatientInfo = 3,
                    //        Uncancel = 4,
                    //        Bad = 5,		
                    //fin_opr_register VALID_FLAG��0�˷�,1��Ч,2����

                    //�Һ�״̬ö�٣�FS.HISFC.Models.Base.EnumRegisterStatus
                    //        �˷�
                    //        Back = 0,
                    //        ��Ч
                    //        Valid = 1,
                    //        ����
                    //        Cancel = 2,
                    //        Bad = 3,
                    //fin_opb_accountcardfee CANCEL_FLAG����0�� ��Ч ��1�� ��Ч,2�˷�
					//�˷Ѷ��岻һ��
                    if (!this.IsEnumEqualDataBase&&registerStatus==FS.HISFC.Models.Base.EnumRegisterStatus.Back)
                    {
                        iRes = this.accMgr.CancelAccountCardFeeByInvoice(oldInvoiceNo, 2);
                    }
                    else
                    {
                        iRes = this.accMgr.CancelAccountCardFeeByInvoice(oldInvoiceNo, (int)registerStatus);
                    }
                    if (iRes <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ʧ�ܣ�" + this.accMgr.Err), "��ʾ");
                        return -1;
                    }
                }

                #endregion

                #region ���븺��¼

                cardFee = null;
                if (lstReturnFee != null && lstReturnFee.Count > 0)
                {
                    for (int idx = 0; idx < lstReturnFee.Count; idx++)
                    {
                        cardFee = lstReturnFee[idx].Clone();
                        cardFee.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                        cardFee.Oper.ID = employee.ID;
                        cardFee.Oper.Name = employee.Name;
                        cardFee.Oper.OperTime = current;
                        cardFee.Tot_cost = -cardFee.Tot_cost;
                        cardFee.Own_cost = -cardFee.Own_cost;
                        cardFee.Pub_cost = -cardFee.Pub_cost;
                        cardFee.Pay_cost = -cardFee.Pay_cost;
                        returndecimal=returndecimal+cardFee.Tot_cost;
                        //�˷Ѷ��岻һ��
                        if (!this.IsEnumEqualDataBase && registerStatus == FS.HISFC.Models.Base.EnumRegisterStatus.Back)
                        {
                            cardFee.IStatus = 2;
                        }
                        else
                        {
                            cardFee.IStatus = (int)registerStatus;
                        }

                        iRes = this.accMgr.InsertAccountCardFee(cardFee);
                        if (iRes <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ʧ�ܣ�" + this.accMgr.Err), "��ʾ");
                            return -1;
                        }
                    }
                }

                cardFee = null;
                if (lstUnRetFee != null && lstUnRetFee.Count > 0)
                {
                    for (int idx = 0; idx < lstUnRetFee.Count; idx++)
                    {
                        cardFee = lstUnRetFee[idx].Clone();
                        cardFee.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                        cardFee.Oper.ID = employee.ID;
                        cardFee.Oper.Name = employee.Name;
                        cardFee.Oper.OperTime = current;
                        cardFee.Tot_cost = -cardFee.Tot_cost;
                        cardFee.Own_cost = -cardFee.Own_cost;
                        cardFee.Pub_cost = -cardFee.Pub_cost;
                        cardFee.Pay_cost = -cardFee.Pay_cost;

                        cardFee.IStatus = (int)registerStatus;

                        iRes = this.accMgr.InsertAccountCardFee(cardFee);
                        if (iRes <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ʧ�ܣ�" + this.accMgr.Err), "��ʾ");
                            return -1;
                        }
                    }
                }


                #endregion

                #region ��������¼

                cardFee = null;
                if (lstUnRetFee != null && lstUnRetFee.Count > 0)
                {
                    for (int idx = 0; idx < lstUnRetFee.Count; idx++)
                    {
                       
                        cardFee = lstUnRetFee[idx];

                        if (blnRegReturn) cardFee.ClinicNO = "";

                        cardFee.InvoiceNo = strNewInvoiceNo;
                        cardFee.Print_InvoiceNo = strNewPrintInvoiceNo;

                        cardFee.TransType = FS.HISFC.Models.Base.TransTypes.Positive;

                        cardFee.FeeOper.ID = employee.ID;
                        cardFee.FeeOper.Name = employee.Name;
                        cardFee.FeeOper.OperTime = current;
                        cardFee.Oper.ID = employee.ID;
                        cardFee.Oper.Name = employee.Name;
                        cardFee.Oper.OperTime = current;

                        cardFee.IStatus = (int)(FS.HISFC.Models.Base.EnumRegisterStatus.Valid);

                        iRes = this.accMgr.InsertAccountCardFee(cardFee);
                        if (iRes <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ʧ�ܣ�" + this.accMgr.Err), "��ʾ");
                            return -1;
                        }

                        lstUnRetFee[idx] = cardFee;
                    }
                }

                #endregion

                #region ����Һż�¼

                if (blnReturnRegFee)
                {
                    decimal decRegFee = 0;
                    decimal decChkFee = 0;
                    decimal decDigFee = 0;
                    decimal decOthFee = 0;
                    decimal ownCost = 0;
                    decimal pubCost = 0;
                    decimal payCost = 0;

                    if (lstUnRetFee != null && lstUnRetFee.Count > 0)
                    {
                        for (int idx = 0; idx < lstUnRetFee.Count; idx++)
                        {
                            cardFee = lstUnRetFee[idx];
                            switch (cardFee.FeeType)
                            {
                                case AccCardFeeType.RegFee:
                                    decRegFee += cardFee.Tot_cost;
                                    break;
                                case AccCardFeeType.ChkFee:
                                    decChkFee += cardFee.Tot_cost;
                                    break;
                                case AccCardFeeType.DiaFee:
                                    decDigFee += cardFee.Tot_cost;
                                    break;
                                case AccCardFeeType.OthFee:
                                case AccCardFeeType.CaseFee:
                                case AccCardFeeType.CardFee:
                                case AccCardFeeType.AirConFee:
                                    decOthFee += cardFee.Tot_cost;
                                    break;
                            }

                            ownCost += cardFee.Own_cost;
                            pubCost += cardFee.Pub_cost;
                            payCost += cardFee.Pay_cost;
                        }
                    }

                    FS.HISFC.Models.Registration.Register objReturn = regSelect.Clone();

                    if (!blnRegReturn)
                    {
                        // ֻ�˷ѣ����¹Һż�¼
                        if (blnGetInvoice)
                        {
                            objReturn.InvoiceNO = strNewInvoiceNo;
                        }
                        objReturn.RegLvlFee.ChkFee = decChkFee;
                        objReturn.RegLvlFee.OwnDigFee = decDigFee;
                        objReturn.RegLvlFee.OthFee = decOthFee;
                        objReturn.RegLvlFee.RegFee = decRegFee;

                        objReturn.OwnCost = ownCost;
                        objReturn.PubCost = pubCost;
                        objReturn.PayCost = payCost;

                        iRes = this.regMgr.UpdateRegFeeCost(objReturn);
                        if (iRes <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("�ùҺ���Ϣ״̬�Ѿ����,�����¼�������"), "��ʾ");
                            return -1;
                        }
                    }
                    else
                    {
                        #region ���ϹҺ���Ϣ

                        //��Ʊ��ȡ�ؾɵķ�Ʊ��
                        //if (blnGetInvoice)
                        //{
                        //    objReturn.InvoiceNO = strNewInvoiceNo;
                        //}

                        objReturn.RegLvlFee.ChkFee = -objReturn.RegLvlFee.ChkFee;// +decChkFee;
                        objReturn.RegLvlFee.OwnDigFee = -objReturn.RegLvlFee.OwnDigFee;// +decDigFee;
                        objReturn.RegLvlFee.OthFee = -objReturn.RegLvlFee.OthFee;// +decOthFee;
                        objReturn.RegLvlFee.RegFee = -objReturn.RegLvlFee.RegFee;// +decRegFee;

                        objReturn.OwnCost = -objReturn.OwnCost;// +ownCost;
                        objReturn.PubCost = -objReturn.PubCost;// +pubCost;
                        objReturn.PayCost = -objReturn.PayCost;// +payCost;

                        objReturn.BalanceOperStat.IsCheck = false;
                        objReturn.BalanceOperStat.ID = "";
                        objReturn.BalanceOperStat.Oper.ID = "";
                        objReturn.Status = registerStatus;
                        
                        objReturn.InputOper.OperTime = current;//����ʱ��
                        objReturn.InputOper.ID = employee.ID;//������
                        objReturn.CancelOper.ID = employee.ID;//�˺���
                        objReturn.CancelOper.OperTime = current;//�˺�ʱ��

                        objReturn.TranType = FS.HISFC.Models.Base.TransTypes.Negative;

                        if (this.regMgr.Insert(objReturn) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.regMgr.Err, "��ʾ");
                            return -1;
                        }

                        #endregion

                        #region ����ԭ����ĿΪ����

                        regSelect.CancelOper.ID = regMgr.Operator.ID;
                        regSelect.CancelOper.OperTime = current;

                        //����ԭ����ĿΪ����
                        rtn = this.regMgr.Update(flag, regSelect);
                        if (rtn == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.regMgr.Err, "��ʾ");
                            return -1;
                        }
                        if (rtn == 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("�ùҺ���Ϣ״̬�Ѿ����,�����¼�������"), "��ʾ");
                            return -1;
                        }

                        #endregion

                        #region �ָ��޶�
                        //�ָ�ԭ���Ű��޶�
                        //���ԭ�������޶�,��ô�ָ��޶�
                        if (regSelect.DoctorInfo.Templet.ID != null && regSelect.DoctorInfo.Templet.ID != "")
                        {
                            //�ֳ��š�ԤԼ�š������

                            bool IsReged = false, IsTeled = false, IsSped = false, IsTeling = false;

                            if (regSelect.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)
                            {
                                IsTeled = true; //ԤԼ��
                                IsTeling = true;
                            }
                            else if (regSelect.RegType == FS.HISFC.Models.Base.EnumRegType.Reg)
                            {
                                IsReged = true;//�ֳ���
                                if (!regSelect.DoctorInfo.SeeDate.ToString("yyyy-MM-dd").Equals(DateTime.Now.ToString("yyyy-MM-dd")))
                                {
                                    IsTeled = true; //��ǰ�Һ�
                                    IsTeling = true;

                                    IsReged = false;
                                }
                            }
                            else
                            {
                                IsSped = true;//�����
                            }

                            rtn = this.schMgr.Reduce(regSelect.DoctorInfo.Templet.ID, IsReged, IsTeling, IsTeled, IsSped);
                            if (rtn == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(this.schMgr.Err, "��ʾ");
                                return -1;
                            }

                            if (rtn == 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����Ű���Ϣ,�޷��ָ��޶�"), "��ʾ");
                                return -1;
                            }
                        }
                        #endregion

                        //ҽ������
                        if (isCallYBInterface)
                        {
                            #region ҽ������

                            if (regSelect.Pact.PayKind.ID == "02" && DialogResult.Yes == MessageBox.Show("�Ƿ�ѡ��ҽ���Ǽǻ��ߣ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                            {
                                long returnValue = 0;
                                FS.HISFC.Models.Registration.Register myYBregObject = regSelect.Clone();
                                this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                this.medcareInterfaceProxy.SetPactCode(regSelect.Pact.ID);
                                //��ʼ��ҽ��dll
                                returnValue = this.medcareInterfaceProxy.Connect();
                                if (returnValue == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    this.medcareInterfaceProxy.Rollback();
                                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ʼ��ʧ��") + this.medcareInterfaceProxy.ErrMsg);
                                    return -1;
                                }
                                //����ȡ������Ϣ
                                returnValue = this.medcareInterfaceProxy.GetRegInfoOutpatient(myYBregObject);
                                if (returnValue == -1)
                                {

                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    this.medcareInterfaceProxy.Rollback();
                                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ������Ϣʧ��") + this.medcareInterfaceProxy.ErrMsg);
                                    return -1;
                                }
                                //ҽ����Ϣ��ֵ
                                regSelect.SIMainInfo = myYBregObject.SIMainInfo;
                                //�˺�
                                regSelect.User01 = "-1";//�˺Ž���
                                //����ĵ����˹Һŷ���{719DEE22-E3E3-4d3c-8711-829391BEA73C} by GengXiaoLei
                                //returnValue = this.medcareInterfaceProxy.UploadRegInfoOutpatient(reg);
                                regSelect.TranType = FS.HISFC.Models.Base.TransTypes.Negative;

                                returnValue = this.medcareInterfaceProxy.CancelRegInfoOutpatient(regSelect);
                                if (returnValue == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    this.medcareInterfaceProxy.Rollback();
                                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����˺�ʧ��") + this.medcareInterfaceProxy.ErrMsg);
                                    return -1;
                                }
                                returnValue = this.medcareInterfaceProxy.Commit();
                                if (returnValue == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    this.medcareInterfaceProxy.Rollback();
                                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����˺��ύʧ��") + this.medcareInterfaceProxy.ErrMsg);
                                    return -1;
                                }
                                this.medcareInterfaceProxy.Disconnect();
                            }
                            #endregion
                        }
                    }

                }
                #endregion

                #region ��Ʊ�ߺ�

                if (blnGetInvoice)
                {
                    string invoiceStytle = controlParma.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, false, "0");
                    if (this.feeMgr.UseInvoiceNO(employee, invoiceStytle, strInvoiceType, 1, ref strNewInvoiceNo, ref strNewPrintInvoiceNo, ref strErrText) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.feeMgr.Err);
                        return -1;
                    }

                    if (this.feeMgr.InsertInvoiceExtend(strNewInvoiceNo, strInvoiceType, strNewPrintInvoiceNo, "00") < 1)
                    {
                        // ��Ʊͷ��ʱ�ȱ���00
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.feeMgr.Err);
                        return -1;
                    }
                }

                #endregion

                #region �˺Ųŷ�����Ϣ

                //�˺Ųŷ�����Ϣ
                if (blnRegReturn)
                {
                    if (InterfaceManager.GetIADT() != null)
                    {
                        if (InterfaceManager.GetIADT().Register(regSelect, false) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this, "�˺�ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + InterfaceManager.GetIADT().Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return -1;

                        }
                    }
                }
                
               
                #endregion

                #region ���·���״̬
                if (blnRegReturn)
                {
                    int updateAssignResult = assignMgr.UpdateAssignFlag(regSelect.ID, employee.ID, FS.HISFC.Models.Nurse.EnuTriageStatus.Cancel);
                    if (updateAssignResult < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ʧ�ܣ�" + this.assignMgr.Err), "��ʾ");
                        return -1;
                    }
                }
                #endregion

                if (isatm && regSelect.IsAccount)
                {
                    if (returndecimal > 0)
                    {
                        returndecimal = returndecimal * -1;
                    }
                    if (!checkatm(regSelect.InvoiceNO))
                    {
                        //MessageBox.Show(FS.FrameWork.Management.Language.Msg("��Ʊ�Ų���Ϊ��"), "��ʾ");
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.txtInvoice.Focus();
                        return -1;
                    }
                   
                  if (feeMgr.AccountCancelPay(regSelect, returndecimal, regSelect.InvoiceNO, (feeMgr.Operator as Employee).Dept.ID, "R") < 0)
                  {
                       FS.FrameWork.Management.PublicTrans.RollBack();
                       MessageBox.Show("�˻��˷��뻧ʧ�ܣ��˺�ʧ�ܣ�" + feeMgr.Err);
                       return -1;
                   }
                  else
                  {
                       FS.FrameWork.Management.PublicTrans.Commit();
                       MessageBox.Show("���˷�������˻��У�����Ҫ���ֽ�" + feeMgr.Err);
                  }
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("�˺ų���!" + e.Message, "��ʾ");
                return -1;
            }

            //����Ѿ���ӡ��Ʊ,��ʾ�ջط�Ʊ
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("�˺ųɹ�"), "��ʾ");


            #region ��ӡ����

            if (lstUnRetFee != null && lstUnRetFee.Count > 0)
            {
                cardFee = lstUnRetFee[0];
                if (regSelect != null)
                {
                    string name = regSelect.Name;
                    if (string.IsNullOrEmpty(cardFee.Patient.Name))
                    {
                        cardFee.Patient.Name = name;
                    }
                    if (string.IsNullOrEmpty(cardFee.Patient.Sex.Name))
                    {
                        cardFee.Patient.Sex = regSelect.Sex;
                    }
                    if (DateTime.Compare(cardFee.Patient.Birthday, new DateTime(1900, 1, 1)) <= 0)
                    {
                        cardFee.Patient.Birthday = regSelect.Birthday;
                    }
                    regSelect = new Register();
                    regSelect.PID.CardNO = cardFee.Patient.PID.CardNO;
                    
                    regSelect.Name = cardFee.Patient.Name;
                    regSelect.Sex = cardFee.Patient.Sex;
                    regSelect.Pact = cardFee.Patient.Pact;
                    regSelect.Birthday = cardFee.Patient.Birthday;

                    //������йҺż�¼������£�ȥ��ѯ�Һż�¼��
                    if (!string.IsNullOrEmpty(cardFee.ClinicNO))
                    {
                        ArrayList listTemp = this.regMgr.QueryPatient(cardFee.ClinicNO);

                        if (listTemp != null && listTemp.Count == 1)
                        {
                            Register regTemp = listTemp[0] as FS.HISFC.Models.Registration.Register;

                            if (regTemp != null && !string.IsNullOrEmpty(regTemp.ID))
                            {
                                regSelect = regTemp;
                            }
                        }
                        
                    }

                    regSelect.InvoiceNO = cardFee.InvoiceNo;//�������÷�Ʊ��
                }

                regSelect.LstCardFee = lstUnRetFee;

                //��ӡ�˺�Ʊ
                this.Print(regSelect);
            }
            #endregion

            this.ClearAll();

            return 0;
        }

        //{B700292D-50A6-4cdf-8B03-F556F990BB9B}
        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="regObj"></param>
        private void Print(FS.HISFC.Models.Registration.Register regObj)
        {

            FS.HISFC.BizProcess.Interface.Registration.IRegPrint regprint = null;
            regprint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Registration.IRegPrint)) as FS.HISFC.BizProcess.Interface.Registration.IRegPrint;

            if (regprint == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ӡƱ��ʧ��,���ڱ���ά����ά���˺�Ʊ"));
            }
            else
            {

                if (regObj.IsEncrypt)
                {
                    regObj.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(regObj.NormalName);
                }

                regprint.SetPrintValue(regObj);
                regprint.Print();
            }



        }

        #endregion

        #region �¼�
        /// <summary>
        /// �����ݼ�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            //if (keyData == Keys.F12)
            //{
            //    this.save();

            //    return true;
            //}
            //else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.X.GetHashCode())
            //{
            //    this.FindForm().Close();

            //    return true;
            //}
            //else if (keyData == Keys.Escape)
            //{
            //    this.FindForm().Close();

            //    return true;
            //}
            //else if (keyData == Keys.F8)
            //{
            //    this.Clear();

            //    return true;
            //}

            if (keyData == Keys.F1)
            {
                this.txtName.Focus();
            }
            if (keyData == Keys.F2)
            {
                this.txtCardNo.Focus();
            }

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// fp�س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    this.save();
            //}
        }

        private void fpSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.fpSpread1_Sheet1.ActiveRowIndex < 0 || this.fpSpread1_Sheet1.ActiveRowIndex > this.fpSpread1_Sheet1.RowCount)
                return;

            FS.HISFC.Models.Registration.Register obj = fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Registration.Register;
            if (obj == null)
                return;

            if (this.regMgr.Operator.ID.Equals(obj.InputOper.ID) == false && IsJudePrivileged)
            {
                //�ж�Ȩ��,�Ƿ����������Һ�Ա������Ȩ��
                if (!CommonController.CreateInstance().JugePrive("0811", "02"))
                {
                    CommonController.CreateInstance().MessageBox("û������������Ա�Һż�¼��Ȩ�ޣ�����Ա��" + CommonController.CreateInstance().GetEmployeeName(obj.InputOper.ID), MessageBoxIcon.Warning);
                    return;
                }
            }

            List<AccountCardFee> lstCardFee = null;
            int iRes = this.accMgr.QueryAccCardFeeByClinic(obj.PID.CardNO, obj.ID, out lstCardFee);
            if (iRes < 0 )
            {
                MessageBox.Show("δ�������Һ���ط�����Ϣ!" + this.accMgr.Err, "��ʾ");
                return;
            }
            if (lstCardFee != null && lstCardFee.Count > 0)
            {
                AddCardFeeByRegister(lstCardFee, FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 0].Value));
            }

            //this.SetReturnFee(e.Range.Row);
        }

        void fpSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == 0)
            {
                bool bln = FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[e.Row, e.Column].Value);

                SetClinicFeeChecked(bln);
            }
        }

        private void SetClinicFeeChecked(bool blnChecked)
        {
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                return;
            }

            int value = 0;
            if (blnChecked)
            {
                value = 1;
            }

            AccountCardFee cardFee = null;
            for (int idx = 0; idx < this.neuSpread1_Sheet1.RowCount; idx++)
            {
                cardFee = this.neuSpread1_Sheet1.Rows[idx].Tag as AccountCardFee;
                if (cardFee == null)
                    continue;

                switch (cardFee.FeeType)
                {
                    case AccCardFeeType.RegFee:
                    case AccCardFeeType.DiaFee:
                    case AccCardFeeType.ChkFee:
                        this.neuSpread1_Sheet1.Cells[idx, 0].Value = value;
                        break;
                }
            }
        }

        void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column != 0)
            {
                return;
            }

            bool boolChecked = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[e.Row,e.Column].Value);
            int n = 0;
            if(boolChecked)
            {
                n = 1;
            }

            // ѡ����𼰹Һŷ��Զ�ѡ�ϹҺż�¼
            AccountCardFee cardFee = null;
            cardFee = this.neuSpread1_Sheet1.Rows[e.Row].Tag as AccountCardFee;
            switch (cardFee.FeeType)
            {
                case AccCardFeeType.RegFee:
                case AccCardFeeType.DiaFee:
                case AccCardFeeType.ChkFee:
                    this.fpSpread1_Sheet1.Cells[0, 0].Value = n;
                    break;
            }

            SetReturnCost(this.neuSpread1_Sheet1);
        }

        void neuSpread2_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column != 0)
            {
                return;
            }

            SetReturnCost(this.neuSpread2_Sheet1);
        }
        /// <summary>
        /// ���㲢���ý��
        /// </summary>
        /// <param name="sheet"></param>
        private void SetReturnCost(FarPoint.Win.Spread.SheetView sheet)
        {
            this.lbTot.Text = "";
            this.lbReturn.Text = "";

            if (sheet == null)
                return;

            decimal totCost = 0;
            decimal retCost = 0;

            AccountCardFee cardFee = null;
            for (int idx = 0; idx < sheet.RowCount; idx++)
            {
                if (sheet.Rows[idx].Tag == null)
                    continue;

                cardFee = sheet.Rows[idx].Tag as AccountCardFee;
                if (cardFee == null)
                    continue;

                totCost += cardFee.Tot_cost;

                bool bln = FS.FrameWork.Function.NConvert.ToBoolean(sheet.Cells[idx, 0].Value);
                if (bln)
                {
                    retCost += cardFee.Own_cost + cardFee.Pay_cost;
                }
            }

            this.lbTot.Text = totCost.ToString();
            this.lbReturn.Text = retCost.ToString();
        }

        /// <summary>
        /// ���������������߿���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Clear();

                string name = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtName.Text.Trim(), "'", "[", "]");

                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("���￨�Ų���Ϊ��"), "��ʾ");
                    SetNameFocus();
                    return;
                }

                this.QueryRegisterByName(name);
            }
        }

        /// <summary>
        /// ���ݲ����ż������߹Һ���Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cardNo = this.txtCardNo.Text.Trim();

                Clear();

                if (string.IsNullOrEmpty(cardNo))
                {
                    SetCardNoFocus();
                    return;
                }

                cardNo = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtCardNo.Text.Trim(), "'", "[", "]");
                if(string.IsNullOrEmpty(cardNo))
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("���￨�Ų���Ϊ��"), "��ʾ");
                    SetCardNoFocus();
                    return;
                }

                this.QueryRegisterByCardNO(cardNo);

            }
        }
        /// <summary>
        /// �������֤�����Һ���Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtIDCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //{4661623D-235A-4380-A7E0-476C977650CD}
                string IDCard = this.txtIDCard.Text.Trim();
                if (string.IsNullOrEmpty(IDCard))
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("���֤����Ϊ��"), "��ʾ");
                    this.txtIDCard.Focus();
                    return;
                }

                this.QueryRegisterByIDCard(IDCard);
            }
        }
        /// <summary>
        /// �Һ���Ϣѡ���¼�
        /// </summary>
        /// <param name="reg"></param>
        public void ucShow_SelectedRegister(FS.HISFC.Models.Registration.Register reg)
        {
            ArrayList listReg = new ArrayList();
            listReg.Add(reg);
            this.addRegister(listReg);
        }

        private void AddCardFeeNoRegister(List<AccountCardFee> lstCardFee)
        {
            this.neuSpread2_Sheet1.RowCount = 0;
            if (lstCardFee == null || lstCardFee.Count <= 0)
                return;

            int idx = 0;
            string strTypeName = "";
           // decimal decTotCost = 0;
            foreach (AccountCardFee cardFee in lstCardFee)
            {
                if (cardFee.IStatus != 1)
                {
                    continue;
                }
                if (isCardFeeCanReturn == false && cardFee.FeeType == AccCardFeeType.CardFee)
                {
                    continue;
                }
                this.neuSpread2_Sheet1.Rows.Add(this.neuSpread2_Sheet1.RowCount, 1);
                idx = this.neuSpread2_Sheet1.RowCount - 1;

                this.neuSpread2_Sheet1.SetValue(idx, 0, true, false);
                this.neuSpread2_Sheet1.SetValue(idx, 1, cardFee.Patient.Name, false);
                this.neuSpread2_Sheet1.SetValue(idx, 2, cardFee.Patient.Sex.Name, false);
                this.neuSpread2_Sheet1.SetValue(idx, 3, cardFee.Print_InvoiceNo, false);

                switch (cardFee.FeeType)
                {
                    case AccCardFeeType.AirConFee:
                        strTypeName = "�յ���";
                        break;
                    case AccCardFeeType.CardFee:
                        strTypeName = "������";
                        break;
                    case AccCardFeeType.CaseFee:
                        strTypeName = "��������";
                        break;

                    case AccCardFeeType.ChkFee:
                        strTypeName = "����";
                        break;

                    case AccCardFeeType.DiaFee:
                        strTypeName = "���";
                        break;

                    case AccCardFeeType.OthFee:
                        strTypeName = "��������";
                        break;
                    case AccCardFeeType.RegFee:
                        strTypeName = "�Һŷ�";
                        break;

                    default:
                        strTypeName = "��������";
                        break;
                }

                this.neuSpread2_Sheet1.SetValue(idx, 4, strTypeName, false);


                this.neuSpread2_Sheet1.SetValue(idx, 5, cardFee.Own_cost.ToString(), false);
                this.neuSpread2_Sheet1.SetValue(idx, 6, cardFee.Pub_cost.ToString(), false);
                this.neuSpread2_Sheet1.SetValue(idx, 7, cardFee.Pay_cost.ToString(), false);

                this.neuSpread2_Sheet1.SetValue(idx, 8, cardFee.FeeOper.Name, false);
                this.neuSpread2_Sheet1.SetValue(idx, 9, cardFee.FeeOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss"), false);

                this.neuSpread2_Sheet1.SetValue(idx, 10, cardFee.MarkNO, false);
                this.neuSpread2_Sheet1.SetValue(idx, 11, cardFee.MarkType.Name, false);

                this.neuSpread2_Sheet1.Rows[idx].Tag = cardFee;
                //decTotCost +=cardFee.Own_cost + cardFee.Pub_cost + cardFee.Pay_cost;
            }
           // this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.Rows.Count - 1, 8, decTotCost.ToString(), false);
        }

        private void AddCardFeeByRegister(List<AccountCardFee> lstCardFee, bool isSelect)
        {
            decimal decTotCost = 0;
            this.neuSpread1_Sheet1.RowCount = 0;
            if (lstCardFee == null || lstCardFee.Count <= 0)
                return;

            int idx = 0;
            string strTypeName = "";
            foreach (AccountCardFee cardFee in lstCardFee)
            {
                if (cardFee.IStatus != 1)
                    continue;

                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                idx = this.neuSpread1_Sheet1.RowCount - 1;

                switch (cardFee.FeeType)
                {
                    case AccCardFeeType.AirConFee:
                        strTypeName = "�յ���";
                        break;
                    case AccCardFeeType.CardFee:
                        strTypeName = "������";
                        break;
                    case AccCardFeeType.CaseFee:
                        strTypeName = "��������";
                        break;

                    case AccCardFeeType.ChkFee:
                        strTypeName = "����";
                        break;

                    case AccCardFeeType.DiaFee:
                        strTypeName = "���";
                        break;

                    case AccCardFeeType.OthFee:
                        strTypeName = "��������";
                        break;
                    case AccCardFeeType.RegFee:
                        strTypeName = "�Һŷ�";
                        break;

                    default:
                        strTypeName = "��������";
                        break;
                }

                this.neuSpread1_Sheet1.SetValue(idx, 0, isSelect, false);
                this.neuSpread1_Sheet1.SetValue(idx, 1, strTypeName, false);
                this.neuSpread1_Sheet1.SetValue(idx, 2, cardFee.Own_cost.ToString(), false);
                this.neuSpread1_Sheet1.SetValue(idx, 3, cardFee.Pub_cost.ToString(), false);
                this.neuSpread1_Sheet1.SetValue(idx, 4, cardFee.Pay_cost.ToString(), false);
                this.neuSpread1_Sheet1.SetValue(idx, 5, this.payWayHelper.GetName(cardFee.PayType.ID), false);
                this.neuSpread1_Sheet1.SetValue(idx, 6, cardFee.FeeOper.Name, false);
                this.neuSpread1_Sheet1.SetValue(idx, 7, cardFee.FeeOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss"), false);
                this.neuSpread1_Sheet1.SetValue(idx, 8, cardFee.MarkNO, false);
                this.neuSpread1_Sheet1.SetValue(idx, 9, cardFee.MarkType.Name, false);

                this.neuSpread1_Sheet1.Rows[idx].Tag = cardFee;

                decTotCost += cardFee.Own_cost + cardFee.Pub_cost + cardFee.Pay_cost;
            }
            this.fpSpread1_Sheet1.SetValue(this.fpSpread1_Sheet1.Rows.Count - 1, 8, decTotCost.ToString(), false);
        }
        private bool isNetInvoice(string invoiceno)
        {
            if (invoiceno.StartsWith("W"))
                return true;

            return false;
        }
        private bool checkatm(string invoiceno)
        {
            if (invoiceno.StartsWith("R"))
           {
               MessageBox.Show("����ȥר����ӡ��Ʊ��");
               return false;
           }
           FS.HISFC.Models.Registration.Register tregObj=null;
           try
           {
               if(this.fpSpread1_Sheet1.Rows.Count<0)
               {
                  MessageBox.Show("û���ҵ��Һż�¼");
                  return false;   
               }
               tregObj=(FS.HISFC.Models.Registration.Register)this.fpSpread1_Sheet1.Rows[0].Tag;
               if(tregObj==null)
               {
                   MessageBox.Show("û���ҵ��Һż�¼");
                   return false;   
               }
           
           }
           catch
           {
               MessageBox.Show("û���ҵ��Һż�¼");
               return false;   
           }
           if(isatm)
           {
               if(!tregObj.IsAccount)
               {
                     MessageBox.Show("���������Һ����ݣ���ȥ��ͨ���ڰ���");
                     return false;
               }
           }
           else
           {
                if(tregObj.IsAccount)
               {
                     MessageBox.Show("�������Һ����ݣ���ȥר������");
                     return false;
               }
           }
           return true; 
        }

        /// <summary>
        /// ���ݴ����ż����Һ���Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //{4661623D-235A-4380-A7E0-476C977650CD}
                string invoiceNo = this.txtInvoice.Text.Trim();

                if (invoiceNo == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��Ʊ�Ų���Ϊ��"), "��ʾ");
                    this.txtInvoice.Focus();
                    return;
                }
                if (IsNetCancle && !isNetInvoice(invoiceNo))
                {
                    MessageBox.Show("�˷�Ʊ����������ԤԼ��Ʊ,�������˷�", "��ʾ");
                    return;
                }
                if (!IsNetCancle && isNetInvoice(invoiceNo))
                {
                    MessageBox.Show("�˷�ƱΪ������ԤԼ��Ʊ,�������˷�", "��ʾ");
                    return;
                }
                this.QueryRegisterByInvoiceNO(invoiceNo);
                if (!checkatm(invoiceNo))
                {
                    //MessageBox.Show(FS.FrameWork.Management.Language.Msg("��Ʊ�Ų���Ϊ��"), "��ʾ");
                    this.Clear();
                    this.txtInvoice.Focus();
                    return;
                }
            }
        }

        #endregion

        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("�˷�", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            toolbarService.AddToolButton("�Ϻ�", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            toolbarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            toolbarService.AddToolButton("ˢ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            toolbarService.AddToolButton("���֤", "���֤", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Mģ��, true, false, null);
            toolbarService.AddToolButton("��������", "����������ԤԼ�����Ƿ�ͨ��", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S����, true, false, null);
            toolbarService.AddToolButton("�����˺�", "����ԤԼ���˺�/�˷�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            return toolbarService;
        }        

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "��������":
                    break; 
                case "�����˺�":
                    ReturnNetRegister();
                    break; 
                case "�˷�":
                    //if (txtCardNo.Text == null || txtCardNo.Text.Trim() == "")
                    //{
                    //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("���벡����"), FS.FrameWork.Management.Language.Msg("��ʾ"));
                    //    return;
                    //}
                    e.ClickedItem.Enabled = false;
                    if (this.save() == -1)
                    {
                        e.ClickedItem.Enabled = true;

                        return;
                    }
                    e.ClickedItem.Enabled = true;

                    break;
                case "�Ϻ�":
                    e.ClickedItem.Enabled = false;
                    if (this.save() == -1)
                    {
                        e.ClickedItem.Enabled = true;
                        return;
                    }
                    e.ClickedItem.Enabled = true;

                    break;
                case "���֤":
                    {
                        if (InterfaceManager.GetIReadIDCard() != null)
                        {
                            string code = "", name = "", sex = "", nation = "", agent = "", add = "", message = "";
                            DateTime birth = DateTime.MinValue, bigen = DateTime.MinValue, end = DateTime.MinValue;
                            string photoFileName = "";
                            int rtn = InterfaceManager.GetIReadIDCard().GetIDInfo(ref code, ref name, ref sex, ref birth, ref nation, ref add, ref agent, ref bigen, ref end, ref photoFileName, ref message);
                            if (rtn == -1)
                            {
                                CommonController.Instance.MessageBox(this, "����ʧ�ܣ�" + message, MessageBoxIcon.Asterisk);
                            }
                            else if (rtn == 0)
                            {
                                CommonController.Instance.MessageBox(this, "����ʧ�ܣ�" + message, MessageBoxIcon.Asterisk);
                            }
                            else if (!string.IsNullOrEmpty(code))
                            {
                                this.txtIDCard.Text = code;
                                this.txtIDCard_KeyDown(txtIDCard, new KeyEventArgs(Keys.Enter));
                            }
                        }
                        break;
                    }
                case "ˢ��":
                    {
                        string cardNo = "";
                        string error = "";
                        if (Function.OperMCard(ref cardNo, ref error) == -1)
                        {
                            CommonController.Instance.MessageBox(error, MessageBoxIcon.Error);
                            return;
                        }

                        txtCardNo.Text = cardNo;
                        txtCardNo.Focus();
                        this.txtCardNo_KeyDown(null, new KeyEventArgs(Keys.Enter));

                        break;
                    }
                case "����":

                    this.ClearAll();

                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        #region IInterfaceContainer ��Ա
        //{B700292D-50A6-4cdf-8B03-F556F990BB9B}
        public Type[] InterfaceTypes
        {

            get
            {
                Type[] type = new Type[1];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.Registration.IRegPrint);

                return type;
            }
        }

        #endregion
        //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
        private void chbQuitFeeBookFee_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                SetReturnCost(this.neuSpread1_Sheet1);
            }
            else
            {
                SetReturnCost(this.neuSpread2_Sheet1);
            }
        }

        #region IPreArrange ��Ա

        public int PreArrange()
        {
            //�ж��˺�Ȩ��
            if (!CommonController.CreateInstance().JugePrive("0811", "01"))
            {
                CommonController.CreateInstance().MessageBox("û����Ա��Ȩ�ޣ�����Ա��" + CommonController.CreateInstance().GetEmployeeName(regMgr.Operator.ID), MessageBoxIcon.Warning);
                return -1;
            }
            return 1;
        }

        #endregion
    }
}
