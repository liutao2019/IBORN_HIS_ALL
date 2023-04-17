using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;

namespace FS.HISFC.Components.Common.Controls
{
    /// <summary>
    /// ���￨������ؼ�
    /// ����
    /// </summary>
    public partial class ucQuerySeeNoByCardNo : UserControl
    {
        public ucQuerySeeNoByCardNo()
        {
            InitializeComponent();
        }

        #region ˽�б���

        /// <summary>
        /// ����Һż�¼����ʾѡ���
        /// </summary>
        private System.Windows.Forms.Form listform;

        private System.Windows.Forms.ListBox lst;

        /// <summary>
        /// �Ƿ�����һ�����߶������ѡ��
        /// </summary>
        private bool isUserOnePersonMorePact = false;

        /// <summary>
        /// �Ƿ�����һ�����߶������ѡ��
        /// </summary>
        public bool IsUserOnePersonMorePact
        {
            get
            {
                return isUserOnePersonMorePact;
            }
            set
            {
                isUserOnePersonMorePact = value;
            }
        }

        /// <summary>
        /// �Һš��˺���Ч����
        /// ������ʾֻ��ѯ����ҺŻ���
        /// </summary>
        private decimal validDays = 1;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public event Controls.myEventDelegate myEvents;

        /// <summary>
        /// �Ƿ��տ���ҽ��������Ч�Һż�¼������ֻҪ��ͬһ�����ҵĶ���Ϊ��Ч
        /// </summary>
        private bool isValideRegByDoct = true;

        /// <summary>
        /// ��ϣ��洢��clinic_no/regObj
        /// </summary>
        private Hashtable hsReg = new Hashtable();

        //private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        //private FS.FrameWork.Public.ObjectHelper emplHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��Һſ���
        /// </summary>
        private Hashtable hsNoSupplyRegDept = new Hashtable();

        /// <summary>
        /// ��ѯ�Һ��б������ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Order.IAfterQueryRegList IAfterQueryRegList = null;

        #region ҵ���

        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Registration.Registration regInterMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// ��������ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant conManager = new FS.HISFC.BizLogic.Manager.Constant();

        //{BCF08E42-A911-42b5-946A-703B8AD81D7C}
        private FS.HISFC.BizLogic.Fee.Account account = new FS.HISFC.BizLogic.Fee.Account();

        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ���Ʋ�������
        /// </summary>
        FS.FrameWork.Management.ControlParam contrlManager = new FS.FrameWork.Management.ControlParam();

        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        private FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        #endregion

        #endregion

        #region ����

        /// <summary>
        /// �õ��������������Ϣ����
        /// </summary>
        private ArrayList alSeeNo = new ArrayList();

        /// <summary>
        /// �ÿؼ�ʹ�õĵط����Һš��쿨��ҽ��վ���շѴ�������
        /// </summary>
        private enumUseType useType = enumUseType.Other;

        /// <summary>
        /// �Ƿ���ʾ���Һ�
        /// </summary>
        //private bool isTipAddNewReg = true;

        /// <summary>
        /// �ÿؼ�ʹ�õĵط����Һš��쿨��ҽ��վ���շѴ�������
        /// </summary>
        public enumUseType UseType
        {
            get
            {
                return useType;
            }
            set
            {
                useType = value;
            }
        }
        private string mCardNo = "";// {DD27333B-4CBF-4bb2-845D-8D28D616937E}
        public string MCardNo
        {
            get
            {
                return mCardNo;
            }

            set
            {
                mCardNo = value;
                if (!string.IsNullOrEmpty(mCardNo))
                {
                    this.txtInputCode.Text = mCardNo;
                    this.QueryPatient();
                }
            }
        }


        /// <summary>
        /// �����Ƿ���ʾ���չҺŷ� 0 ����ʾ�������գ�1 ��ʾ�Ƿ��գ�2 ����ʾ������(HNMZ21)
        /// </summary>
        private int isAddRegFee_OtherDay = 1;

        /// <summary>
        /// ��ҽ���Ƿ���ʾ���չҺŷ� 0 ����ʾ�������գ�1 ��ʾ�Ƿ��գ�2 ����ʾ������ (HNMZ22)
        /// </summary>
        private int isAddRegFee_OtherDoct = 1;

        /// <summary>
        /// ��ǰ�����Ƿ����²��չҺŷѡ����
        /// </summary>
        //private bool isAddRegFee = false;

        /// <summary>
        /// �Ƿ�������cardNoֱ�ӿ���(���������˻�����£�����ҽ���ڻ��߲��ڵ������ �Լ������۷ѣ�
        /// </summary>
        private bool isAllowUserCardNoAdded = true;

        /// <summary>
        /// ���Һ��Ƿ�������
        /// houwb 2011-3-16 {3F83B7FE-39C7-4f13-87BB-B0B229F2949F}
        /// </summary>
        private bool isAllowNoRegSee = false;

        /// <summary>
        /// ���Һ��Ƿ�������
        /// </summary>
        public bool IsAllowNoRegSee
        {
            get
            {
                return isAllowNoRegSee;
            }
            set
            {
                isAllowNoRegSee = value;
            }
        }

        /// <summary>
        /// �Ƿ������Һ�
        /// </summary>
        private bool isAllowAddNewReg = false;

        /// <summary>
        /// �Ƿ������Һ�
        /// </summary>
        //public bool IsAllowAddNewReg
        //{
        //    get
        //    {
        //        //return this.isAllowAddNewReg;

        //        //�ȴ���Ϊ�������������Һţ��������ﲻ����
        //        if (((FS.HISFC.Models.Base.Employee)FrameWork.Management.Connection.Operator).Dept.Name.Contains("����"))
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        /// <summary>
        /// ����ҺŵĿ����б� ͬʱ��isAllowAddNewReg�й�
        /// ��������б�Ϊ�գ������п��Ҷ����Բ��Һ�
        /// </summary>
        private ArrayList alAllowAddRegDept = null;

        /// <summary>
        /// ���������ҺžͿ���Ŀ����б�Ϊ�գ����ʾ�����ƿ���
        /// </summary>
        private ArrayList alAllowNoRegSeeDept = null;

        /// <summary>
        /// ��ǰ����
        /// </summary>
        private FS.HISFC.Models.Registration.Register myRegister = new FS.HISFC.Models.Registration.Register();

        /// <summary>
        /// ��ǰ�Ǽ���Ϣ
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FS.HISFC.Models.Registration.Register Register
        {
            get
            {
                return this.myRegister;
            }
            set
            {
                this.myRegister = value;
            }
        }

        #endregion

        #region ����

        

        /// <summary>
        /// �Һż��������
        /// </summary>
        FS.FrameWork.Public.ObjectHelper regLevlHelper = null;

        /// <summary>
        /// ����Һż������
        /// </summary>
        string emergencyLevlCode = "";

        private void ucQuerySeeNoByCardNo_Load(object sender, System.EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }
            try
            {
                if (this.useType == enumUseType.Charge)
                {
                    //�շ�����ĹҺ���Ч����
                    this.validDays = FS.FrameWork.Function.NConvert.ToDecimal(contrlManager.QueryControlerInfo("MZ0014"));
                }
                else if (this.useType == enumUseType.Doct)
                {
                    //����ҽ������ĹҺ���Ч����
                    this.validDays = FS.FrameWork.Function.NConvert.ToDecimal(contrlManager.QueryControlerInfo("200022"));
                }
                else if (this.useType == enumUseType.CancelFee)
                {
                    //�˷�����ĹҺ���Ч���� ��ʱ���շ�һ��
                    this.validDays = FS.FrameWork.Function.NConvert.ToDecimal(contrlManager.QueryControlerInfo("MZ0014"));
                }
                else
                {
                    this.validDays = 99999;
                }

                this.isAllowAddNewReg = FS.FrameWork.Function.NConvert.ToBoolean(contrlManager.QueryControlerInfo("200030"));


                //houwb 2011-3-16 {3F83B7FE-39C7-4f13-87BB-B0B229F2949F}
                this.isAllowNoRegSee = ctrlMgr.GetControlParam<bool>("200060", false, false);
                //this.isTipAddNewReg = this.ctrlMgr.GetControlParam<bool>("MZ0091", false, true);

                isAllowUserCardNoAdded = ctrlMgr.GetControlParam<bool>("HNMZ20", true, true);

                isAddRegFee_OtherDay = ctrlMgr.GetControlParam<int>("HNMZ21", true, 1);
                isAddRegFee_OtherDoct = ctrlMgr.GetControlParam<int>("HNMZ22", true, 1);

                alAllowAddRegDept = conManager.GetList("AllowAddRegDept");

                alAllowNoRegSeeDept = conManager.GetList("AllowNoRegSeeDept");

                #region ��Һſ���

                ArrayList alNoSupplyRegDept = this.conManager.GetList("NoSupplyRegDept");
                if (alNoSupplyRegDept == null)
                {
                    MessageBox.Show("ucQuerySeeNoByCardNo_Load" + this.conManager.Err);
                    //return -1;
                }
                foreach (FS.HISFC.Models.Base.Const obj in alNoSupplyRegDept)
                {
                    if (!hsNoSupplyRegDept.Contains(obj.ID) && obj.IsValid)
                    {
                        hsNoSupplyRegDept.Add(obj.ID, obj);
                    }
                }
                #endregion

                #region ��ȡ���йҺż���

                FS.HISFC.Models.Registration.RegLevel emergRegLevl = SOC.HISFC.BizProcess.Cache.Fee.GetEmergRegLevl();
                if (emergRegLevl == null)
                {
                    MessageBox.Show("����Һż���û��ά�����ᵼ�²��չҺŷѴ���!\r\n����޼���Һż���������ά����ͣ�ü��ɣ�\r\n����ϵ��Ϣ������ά��" + regInterMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                emergencyLevlCode = emergRegLevl.ID;

                //if (regLevlHelper == null)
                //{
                //    regLevlHelper = new FS.FrameWork.Public.ObjectHelper();

                    //��ȡ���еĹҺż���
                    //ArrayList al = regInterMgr.QueryAllRegLevel();

                    //��Ч�ĹҺż���
                    //ArrayList alValidReglevl = new ArrayList();

                    //if (al == null || al.Count == 0)
                    //{
                    //    MessageBox.Show("��ѯ���йҺż�����󣡻ᵼ�²��չҺŷѴ���!\r\n����ϵ��Ϣ������ά��" + regInterMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                    //else
                    //{
                    //    bool isValidEmergency = true;
                    //    foreach (FS.HISFC.Models.Registration.RegLevel regLevl in al)
                    //    {
                    //        if (regLevl.IsValid)
                    //        {
                    //            alValidReglevl.Add(regLevl);

                    //            if (regLevl.IsEmergency)
                    //            {
                    //                emergencyLevlCode = regLevl.ID;
                    //                break;
                    //            }
                    //        }
                    //        else if (regLevl.IsEmergency)
                    //        {
                    //            isValidEmergency = false;
                    //        }
                    //    }

                    //    if (string.IsNullOrEmpty(emergencyLevlCode) && isValidEmergency)
                    //    {
                    //        MessageBox.Show("����Һż���û��ά�����ᵼ�²��չҺŷѴ���!\r\n����޼���Һż���������ά����ͣ�ü��ɣ�\r\n����ϵ��Ϣ������ά��" + regInterMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    }
                    //}
                //}
                #endregion

                //���ǵ������������󣬴˴������˽ӿڴ���
                if (IAfterQueryRegList == null)
                {
                    IAfterQueryRegList = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Order.IAfterQueryRegList)) as FS.HISFC.BizProcess.Interface.Order.IAfterQueryRegList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ucQuerySeeNoByCardNo_Load" + ex.Message);
            }
        }

        /// <summary>
        /// ���Һ��Ƿ�������
        /// </summary>
        /// <returns></returns>
        private bool AllowNoRegSee()
        {
            bool isAllowedDept = true;
            if (alAllowNoRegSeeDept != null
                && alAllowNoRegSeeDept.Count > 0)
            {
                isAllowedDept = false;
                foreach (FS.HISFC.Models.Base.Const conObj in alAllowAddRegDept)
                {
                    if (conObj.ID.Trim() == this.GetReciptDept().ID)
                    {
                        isAllowedDept = true;
                    }
                }
            }
            return isAllowedDept && isAllowNoRegSee;
        }

        /// <summary>
        /// ���ݿ������һ�ȡ�Ƿ������Һ�
        /// </summary>
        /// <returns></returns>
        private bool AllowAddNewReg()
        {
            if (isAllowAddNewReg)
            {
                if (alAllowAddRegDept != null && alAllowAddRegDept.Count > 0)
                {
                    foreach (FS.HISFC.Models.Base.Const conObj in alAllowAddRegDept)
                    {
                        if (conObj.ID.Trim() == this.GetReciptDept().ID)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public void AddNewReg()
        {
            #region ҽ�����Һ�

            string name = string.Empty;
            if (txtInputCode.Text.Trim() != "���뿨�Ż�����"
                && !string.IsNullOrEmpty(txtInputCode.Text)
                && (txtInputCode.Text.StartsWith("/") || txtInputCode.Text.StartsWith("+")))
            {
                name = this.txtInputCode.Text.Remove(0, 1);
            }

            Forms.frmRegistrationByDoctor frmDoctRegistration = new Forms.frmRegistrationByDoctor(name);
            frmDoctRegistration.EmergencyLevlCode = this.emergencyLevlCode;
            frmDoctRegistration.IAfterQueryRegList = this.IAfterQueryRegList;

            frmDoctRegistration.ShowDialog();
            if (frmDoctRegistration.DialogResult == DialogResult.Cancel)
            {
                return;
            }

            this.myRegister = frmDoctRegistration.PatientInfo;
            if (this.myRegister.ID == "" || this.myRegister.ID == null)
            {
                this.ClearInfo();
            }
            else
            {
                this.txtInputCode.Text = myRegister.PID.CardNO;

                if (isUserOnePersonMorePact)
                {
                    if (account.GetPatientPactInfo(myRegister) == -1)
                    {
                        MessageBox.Show("��ȡ���ߺ�ͬ��λ��Ϣʧ�ܣ�" + account.Err);
                        return;
                    }

                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    if (myRegister.MutiPactInfo.Count > 0)
                    {
                        if (FrameWork.WinForms.Classes.Function.ChooseItem(new ArrayList(myRegister.MutiPactInfo), new string[] { "��ͬ��λ����", "��ͬ��λ", "��Ч��" }, new bool[] { }, new int[] { 0, 100, 70 }, ref obj) != 1)
                        {
                            return;
                        }
                    }
                    myRegister.Pact = obj as FS.HISFC.Models.Base.PactInfo;
                }
                this.myEvents();
            }

            #endregion
        }
        private void QueryPatient()
        {
            //�Ȱ�ȫ��ת��Ϊ���,����ѷ�������Ϊ���� houwb 2011-3-11 {E0AA533A-F09B-47ed-B6EB-C9ADC591F333}
            this.txtInputCode.Text = FS.FrameWork.Function.NConvert.ToDBC(this.txtInputCode.Text.Trim());

            string cardNO = this.txtInputCode.Text;
            if (AllowAddNewReg()
                && this.useType == enumUseType.Doct
                && (cardNO.StartsWith("/") || cardNO.StartsWith("+")))
            {
                DialogResult r = MessageBox.Show("�Ƿ���ٲ��Һţ�\r\n", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (r == DialogResult.Yes)
                {
                    AddNewReg();
                }
                else
                {
                    this.ClearInfo();
                }
            }
            else
            {
                if (Regex.IsMatch(cardNO, @"^\d*$"))
                {
                    FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();

                    int flag = feeIntegrate.ValidMarkNO(cardNO, ref accountCard);

                    if (flag > 0)
                    {
                        cardNO = accountCard.Patient.PID.CardNO;
                    }
                    //���ش�����
                    else
                    {
                        MessageBox.Show(feeIntegrate.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    this.txtInputCode.Text = cardNO;
                }
                else
                {
                    #region ���ֿ�ͷĬ���ǲ�ѯ����

                    Components.Common.Forms.frmQueryPatientByName frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByName();
                    frmQuery.QueryByName(this.txtInputCode.Text.Trim());
                    frmQuery.SelectedPatient += new FS.HISFC.Components.Common.Forms.frmQueryPatientByName.GetPatient(frmQuery_SelectedPatient);
                    frmQuery.ShowDialog(this);

                    #endregion
                }

                int rev = this.Query();

                if (rev == -1)
                {
                    return;
                }
            }
        }
        /// <summary>
        /// �����Żس����ж��Ƿ�¼�Һ���Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInputCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //�Ȱ�ȫ��ת��Ϊ���,����ѷ�������Ϊ���� houwb 2011-3-11 {E0AA533A-F09B-47ed-B6EB-C9ADC591F333}
                this.txtInputCode.Text = FS.FrameWork.Function.NConvert.ToDBC(this.txtInputCode.Text.Trim());

                string cardNO = this.txtInputCode.Text;
                if (AllowAddNewReg()
                    && this.useType == enumUseType.Doct
                    && (cardNO.StartsWith("/") || cardNO.StartsWith("+")))
                {
                    DialogResult r = MessageBox.Show("�Ƿ���ٲ��Һţ�\r\n", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (r == DialogResult.Yes)
                    {
                        AddNewReg();
                    }
                    else
                    {
                        this.ClearInfo();
                    }
                }
                else
                {
                    if (Regex.IsMatch(cardNO, @"^\d*$"))
                    {
                        FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();

                        int flag = feeIntegrate.ValidMarkNO(cardNO, ref accountCard);

                        if (flag > 0)
                        {
                            cardNO = accountCard.Patient.PID.CardNO;
                        }
                        //���ش�����
                        else
                        {
                            MessageBox.Show(feeIntegrate.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        this.txtInputCode.Text = cardNO;
                    }
                    else
                    {
                        #region ���ֿ�ͷĬ���ǲ�ѯ����

                        Components.Common.Forms.frmQueryPatientByName frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByName();
                        frmQuery.QueryByName(this.txtInputCode.Text.Trim());
                        frmQuery.SelectedPatient += new FS.HISFC.Components.Common.Forms.frmQueryPatientByName.GetPatient(frmQuery_SelectedPatient);
                        frmQuery.ShowDialog(this);

                        #region �ɵ�����

                        //FS.HISFC.BizLogic.RADT.InPatient myInpatient = new FS.HISFC.BizLogic.RADT.InPatient();

                        //ArrayList alReg = myInpatient.QueryPatientByName(this.txtInputCode.Text.Trim());

                        //if (alReg == null || alReg.Count <= 0)
                        //{
                        //    MessageBox.Show("û���ҵ�����Ϊ��[" + this.txtInputCode.Text + "]�Ļ���!", "��ʾ");
                        //    return;
                        //}

                        //FS.FrameWork.WinForms.Controls.NeuListView lvAllReg = new FS.FrameWork.WinForms.Controls.NeuListView();

                        //System.Windows.Forms.ColumnHeader colCardID1 = new ColumnHeader();
                        //System.Windows.Forms.ColumnHeader colName1 = new ColumnHeader();
                        //System.Windows.Forms.ColumnHeader colSex1 = new ColumnHeader();
                        //System.Windows.Forms.ColumnHeader colOrder1 = new ColumnHeader();
                        //System.Windows.Forms.ColumnHeader colDate1 = new ColumnHeader();
                        //System.Windows.Forms.ColumnHeader colRegDept1 = new ColumnHeader();

                        //colCardID1.Text = "������";
                        //colCardID1.Width = 114;
                        //colName1.Text = "����";
                        //colName1.Width = 70;
                        //colSex1.Text = "�Ա�";
                        //colSex1.Width = 50;
                        //colDate1.Text = "�绰";
                        //colDate1.Width = 150;
                        //colRegDept1.Text = "��ַ";
                        //colRegDept1.Width = 100;

                        //lvAllReg.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                        //                        colCardID1,
                        //                        colName1,
                        //                        colSex1,
                        //                        colDate1,
                        //                        colRegDept1});

                        //lvAllReg.Dock = System.Windows.Forms.DockStyle.Fill;
                        //lvAllReg.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                        //lvAllReg.FullRowSelect = true;
                        //lvAllReg.GridLines = true;
                        //lvAllReg.Location = new System.Drawing.Point(0, 0);
                        //lvAllReg.Name = "lvAllReg";
                        //lvAllReg.Size = new System.Drawing.Size(500, 250);
                        //lvAllReg.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
                        //lvAllReg.TabIndex = 1;
                        //lvAllReg.UseCompatibleStateImageBehavior = false;
                        //lvAllReg.View = System.Windows.Forms.View.Details;

                        //foreach (FS.HISFC.Models.RADT.PatientInfo regObj in alReg)
                        //{
                        //    ListViewItem item = new ListViewItem();
                        //    item.Text = regObj.PID.CardNO;
                        //    item.Tag = regObj;
                        //    item.SubItems.Add(regObj.Name);
                        //    item.SubItems.Add(regObj.Sex.Name);
                        //    item.SubItems.Add(regObj.PhoneHome);
                        //    item.SubItems.Add(regObj.AddressHome);

                        //    lvAllReg.Items.Add(item);
                        //}

                        //lvAllReg.DoubleClick += new EventHandler(lvAllReg_DoubleClick);

                        //FS.FrameWork.WinForms.Classes.Function.PopShowControl(lvAllReg, FormBorderStyle.None);
                        #endregion
                        #endregion
                    }

                    int rev = this.Query();

                    if (rev == -1)
                    {
                        return;
                    }
                }
            }
        }

        void frmQuery_SelectedPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.txtInputCode.Text = patientInfo.PID.CardNO;
            //Query(patientInfo.PID.CardNO);
        }

        public void Focus()
        {
            this.txtInputCode.Focus();
        }

        private void ucQuerySeeNoByCardNo_Leave(object sender, EventArgs e)
        {
            this.Text = "���뿨�Ż�����";
        }

        #region ����Һż�¼ʱ������ѡ��


        /// <summary>
        /// ����ѡ��Ļ����б�
        /// </summary>
        ArrayList alPatientList = null;

        /// <summary>
        /// ѡ��Ļ���
        /// </summary>
        FS.HISFC.Models.Base.Spell patientObj = null;

        FS.HISFC.Models.Registration.Register regObj;

        /// <summary>
        /// �����Ա��Ϣ
        /// </summary>
        private Hashtable hsEmpl = new Hashtable();

        /// <summary>
        /// ��ſ�����Ϣ
        /// </summary>
        private Hashtable hsDept = new Hashtable();

        /// <summary>
        /// �ж���Һż�¼ʱ��ѡ����
        /// </summary>
        private void SelectPatient()
        {
            #region ֮ǰ��

            //lst = new ListBox();
            //lst.Dock = System.Windows.Forms.DockStyle.Fill;
            //lst.Items.Clear();
            //this.listform = new System.Windows.Forms.Form();
            //this.listform.Text = "ѡ��Һż�¼";

            //listform.Size = new Size(300, 200);
            //listform.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;

            //FS.HISFC.Models.Registration.Register regObj;
            //for (int i = 0; i < this.alSeeNo.Count; i++)
            //{
            //    regObj = this.alSeeNo[i] as FS.HISFC.Models.Registration.Register;

            //    //��ʾ������������ҡ��������ڣ�ȥ��ʱ�䣩���������
            //    lst.Items.Add(regObj.ID + "  " + regObj.Name + "  " + regObj.DoctorInfo.Templet.Dept.Name + "  " + regObj.DoctorInfo.SeeDate.Date.ToString("yyyy��MM��dd��"));
            //}

            //if (lst.Items.Count == 1)
            //{
            //    try
            //    {
            //        if (this.CheckRegInfo(this.Register) == -1)
            //        {
            //            return;
            //        }

            //        this.listform.Close();
            //    }
            //    catch { }
            //    try
            //    {
            //        //this.txtInputCode.Text = this.strSeeNo.Substring(4, 10);
            //        this.myEvents();
            //    }
            //    catch { }
            //    return;
            //}

            //if (lst.Items.Count <= 0)
            //{
            //    if (this.CheckRegInfo(this.Register) == -1)
            //    {
            //        return;
            //    }
            //    //this.strSeeNo = "";
            //    this.myEvents();
            //    return;
            //}

            //lst.Visible = true;
            //lst.DoubleClick += new EventHandler(lst_DoubleClick);
            //lst.KeyDown += new KeyEventHandler(lst_KeyDown);
            //lst.Show();

            //listform.Controls.Add(lst);

            //listform.TopMost = true;

            //listform.Show();
            //listform.Location = this.txtInputCode.PointToScreen(new Point(this.txtInputCode.Width / 2 + this.txtInputCode.Left, this.txtInputCode.Height + this.txtInputCode.Top));
            //try
            //{
            //    lst.SelectedIndex = 0;
            //    lst.Focus();
            //    lst.LostFocus += new EventHandler(lst_LostFocus);
            //}
            //catch { }

            #endregion

            alPatientList = new ArrayList();

            FS.HISFC.Models.Base.Employee doctObj=null;
            FS.HISFC.Models.Base.Department seeDcpt=null;
            for (int i = 0; i < this.alSeeNo.Count; i++)
            {
                regObj = this.alSeeNo[i] as FS.HISFC.Models.Registration.Register;

                patientObj = new FS.HISFC.Models.Base.Spell();
                patientObj.ID = regObj.ID;
                patientObj.Name = regObj.Name;
                patientObj.Memo = regObj.Pact.Name;

                if (!string.IsNullOrEmpty(regObj.DoctorInfo.Templet.Dept.ID))
                {
                    patientObj.SpellCode = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(regObj.DoctorInfo.Templet.Dept.ID);
                }
                //if (!string.IsNullOrEmpty(regObj.SeeDoct.Dept.ID))
                //{
                //    if (hsDept.Contains(regObj.SeeDoct.Dept.ID))
                //    {
                //        seeDcpt = hsDept[regObj.SeeDoct.Dept.ID] as FS.HISFC.Models.Base.Department;
                //    }
                //    else
                //    {
                //        seeDcpt = this.managerIntegrate.GetDepartment(regObj.SeeDoct.Dept.ID);
                //        if (seeDcpt == null)
                //        {
                //            MessageBox.Show("��ѯ������Ϣ����" + managerIntegrate.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        }
                //        hsDept.Add(seeDcpt.ID, seeDcpt);
                //    }
                //    patientObj.SpellCode = seeDcpt.Name;
                //}


                if (!string.IsNullOrEmpty(regObj.DoctorInfo.Templet.Doct.ID))
                {
                    patientObj.WBCode = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(regObj.DoctorInfo.Templet.Doct.ID);
                }

                //if (!string.IsNullOrEmpty(regObj.SeeDoct.ID))
                //{
                //    if (hsEmpl.Contains(regObj.SeeDoct.ID))
                //    {
                //        doctObj = hsEmpl[regObj.SeeDoct.ID] as FS.HISFC.Models.Base.Employee;
                //    }
                //    else
                //    {
                //        doctObj = this.managerIntegrate.GetEmployeeInfo(regObj.SeeDoct.ID);
                //        if (doctObj == null)
                //        {
                //            MessageBox.Show("��ѯ����ҽ����Ϣ����" + managerIntegrate.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        }
                //        hsEmpl.Add(doctObj.ID, doctObj);
                //    }
                //    patientObj.WBCode = doctObj.Name;
                //}
                //patientObj.UserCode= regObj.DoctorInfo.SeeDate.Date.ToString("yyyy��MM��dd��");
                patientObj.UserCode = regObj.DoctorInfo.SeeDate.ToString();
                alPatientList.Add(patientObj);
            }

            if (alPatientList.Count <= 1)
            {
                try
                {
                    if (this.CheckRegInfo(ref regObj) == -1)
                    {
                        return;
                    }
                    this.Register = regObj;

                    this.myEvents();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SelectPatient" + ex.Message);
                }
                return;
            }

            FS.FrameWork.Models.NeuObject selectPatient = null;
            if (alPatientList.Count > 0)
            {
                if (FrameWork.WinForms.Classes.Function.ChooseItem(alPatientList, new string[] { "������ˮ��", "����", "��ͬ��λ", "�Һſ���", "�Һ�ҽ��", "�Һ�ʱ��" }, new bool[] { false, true, true, true, true, true }, new int[] { 50, 50, 70, 100, 70, 160 }, ref selectPatient) != 1)
                {
                    return;
                }
            }

            try
            {
                regObj = this.hsReg[selectPatient.ID] as FS.HISFC.Models.Registration.Register;

                if (this.CheckRegInfo(ref regObj) == -1)
                {
                    return;
                }
                Register = regObj;

                this.myEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("SelectPatient" + ex.Message);
            }

            return;
        }

        private void lst_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GetInfo();
            }
            catch { }
        }

        private void lst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetInfo();
            }
        }

        private void lst_LostFocus(object sender, EventArgs e)
        {
            this.listform.Hide();
            //if (this.strSeeNo == "") ClearInfo();
        }

        /// <summary>
        /// ��û��߹Һ���Ϣ
        /// </summary>
        private void GetInfo()
        {
            try
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

                obj.ID = lst.Items[lst.SelectedIndex].ToString();

                string clinicCode = obj.ID.Substring(0, obj.ID.IndexOf(" "));
                regObj = this.hsReg[clinicCode] as FS.HISFC.Models.Registration.Register;

                if (this.CheckRegInfo(ref regObj) == -1)
                {
                    return;
                }
                this.Register = regObj;

                this.listform.Hide();
                this.myEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetInfo" + ex.ToString()); 
                ClearInfo();
            }
        }

        #endregion

        /// <summary>
        /// �����Ϣ
        /// </summary>
        private void ClearInfo()
        {
            this.txtInputCode.Text = "";
            this.txtInputCode.Focus();
        }

        /// <summary>
        /// ��Ч�Һ���Ϣ
        /// </summary>
        ArrayList alReg = null;

        /// <summary>
        /// ��ȡ�Һ���Чʱ��
        /// </summary>
        /// <returns></returns>
        public decimal GetRegValideDate(bool isEmergency)
        {
            //��ͨ���ﵱ����Ч������24Сʱ��Ч,��Ӧ���Ʋ���Ϊ200022

            //Ĭ��24Сʱ��Ч
            decimal valideDate = 24;

            valideDate = ctrlMgr.GetControlParam<decimal>("200022", false, 24);

            if (isEmergency)
            {
                valideDate = Math.Floor(valideDate / 24) * 24;
            }

            return valideDate;
        }

        /// <summary>
        /// ��ѯ���߹Һ���Ϣ
        /// </summary>
        /// <returns>1:��ѯ����Ч�Һż�¼ 0:û����Ч�Һż�¼ -1:����</returns>
        protected int Query()
        {
            this.hsReg.Clear();
            this.alSeeNo.Clear();

            DateTime dtQueryBegin = this.contrlManager.GetDateTimeFromSysDateTime();
            if (validDays <= 0)
            {
                dtQueryBegin = dtQueryBegin.Date;
            }
            else
            {
                dtQueryBegin = dtQueryBegin.AddDays(0 - (double)this.validDays);
            }

            //���ﵱ����Ч������24Сʱ��Ч
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.Name.Contains("��"))
            {
                validDays = Math.Ceiling(validDays) * 24;
                if (validDays == 0)
                {
                    validDays = 24;
                }
            }

            //dtQueryBegin = dtQueryBegin.AddDays(0 - (double)GetRegValideDate();

            try
            {
                //���ݻ����������ӿڻ�ȡ���ﲡ����
                FS.HISFC.Models.Account.AccountCard accountCardObj = new FS.HISFC.Models.Account.AccountCard();

                //���ڰ쿨�͹Һţ�ֻ��ѯ���žͿ�����
                if (this.useType == enumUseType.Register || this.useType == enumUseType.TransactCard)
                {
                    accountCardObj.Memo = this.useType == enumUseType.Register ? "1" : "2";

                    if (this.feeIntegrate.ValidMarkNO(this.txtInputCode.Text, ref accountCardObj) <= 0)
                    {
                        MessageBox.Show("Query" + this.feeIntegrate.Err);
                        return -1;
                    }
                    this.txtInputCode.Text = accountCardObj.Patient.PID.CardNO;
                    return 1;
                }
                else
                {
                    if (this.feeIntegrate.ValidMarkNO(this.txtInputCode.Text, ref accountCardObj) <= 0)
                    {
                        MessageBox.Show("Query" + this.feeIntegrate.Err);
                        return -1;
                    }

                    if (!isAllowUserCardNoAdded)
                    {
                        if (this.txtInputCode.Text.PadLeft(10, '0') == accountCardObj.Patient.PID.CardNO)
                        {
                            MessageBox.Show("ֻ��ͨ��ˢ��������������������ϵ��Ϣ�ƣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return 1;
                        }
                    }

                    this.txtInputCode.Text = accountCardObj.Patient.PID.CardNO;

                    #region ��ѯ��Ч�ĹҺż�¼

                    if (this.cbxNewReg.Checked)
                    {
                        alReg = new ArrayList();
                    }
                    else
                    {
                        //��ѯ��Ч����ʱ�����������Ч�ĹҺż�¼
                        alReg = regInterMgr.Query(accountCardObj.Patient.PID.CardNO, dtQueryBegin);
                    }

                    //���ǵ������������󣬴˴������˽ӿڴ���
                    if (IAfterQueryRegList != null)
                    {
                        if (IAfterQueryRegList.OnAfterQueryRegList(alReg, this.reciptDept) == -1)
                        {
                            MessageBox.Show(IAfterQueryRegList.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return -1;
                        }
                    }

                    //1���Һ���Ч���ڣ�δ����
                    //2���Һ���Ч���ڣ��ѿ������ҽ���ǵ�ǰҽ��
                    if (alReg == null || alReg.Count <= 0)
                    {
                        #region ҽ��վ���Һ�
                        if (this.useType == enumUseType.Doct)
                        {
                            #region �鲻���Һż�¼ʱ�������Զ��Һ�
                            //houwb 2011-3-16 {3F83B7FE-39C7-4f13-87BB-B0B229F2949F}
                            if (this.AllowNoRegSee()
                                && AllowAddNewReg())
                            {
                                FS.HISFC.Models.Registration.Register regObj = this.GetRegInfoFromPatientInfo(accountCardObj.Patient.PID.CardNO);
                                if (regObj == null)
                                {
                                    return -1;
                                }

                                if (!this.hsReg.ContainsKey(regObj.ID))
                                {
                                    this.hsReg.Add(regObj.ID, regObj);
                                }

                                this.alSeeNo.Add(regObj);
                                this.Register = regObj;
                                //return 1;
                            }
                            else
                            {
                                MessageBox.Show("û�в��ҵ��û�������Чʱ���ڵĹҺ���Ϣ!", "����");
                                this.txtInputCode.Focus();
                                txtInputCode.SelectAll();
                                return -1;
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        FS.HISFC.Models.Registration.Register regObj = null;
                        for (int i = 0; i < alReg.Count; i++)
                        {
                            regObj = alReg[i] as FS.HISFC.Models.Registration.Register;

                            //�ж��Ƿ񱾿������۵Ļ���
                            if (regObj.PVisit.InState.ID.ToString() != "N")
                            {
                                if (regObj.SeeDoct.Dept.ID == this.GetReciptDept().ID)
                                {
                                    MessageBox.Show("�û������ڱ��������ۣ�");
                                    return 1;
                                }
                                else
                                {
                                    continue;
                                }
                            }

                            if (!this.hsReg.ContainsKey(regObj.ID))
                            {
                                this.hsReg.Add(regObj.ID, regObj);
                            }

                            #region ����ҽ��Ҫ�жϿ�����ҺͿ���ҽ��

                            if (this.useType == enumUseType.Doct)
                            {
                                //�ѿ������ҽ����һ��
                                if (regObj.IsSee
                                    && regObj.SeeDoct.ID != this.contrlManager.Operator.ID)
                                {
                                    if (this.isAddRegFee_OtherDoct == 0)
                                    {
                                        continue;
                                    }
                                }

                                //�ѿ���������ڲ�һ��
                                else if (regObj.IsSee
                                    && (regObj.SeeDoct.ID == this.contrlManager.Operator.ID)
                                    && regObj.DoctorInfo.SeeDate.Date != this.conManager.GetDateTimeFromSysDateTime().Date)
                                {
                                    if (isAddRegFee_OtherDay == 0)
                                    {
                                        continue;
                                    }
                                }
                            }
                            #endregion

                            //{4A5DA3D2-5278-46e1-AD2A-DD60A466BE17}
                            HISFC.BizLogic.Manager.Department deptManager = new HISFC.BizLogic.Manager.Department();
                            FS.HISFC.Models.Base.Department dept = deptManager.GetDeptmentById(regObj.DoctorInfo.Templet.Dept.ID);
                            FS.HISFC.Models.Base.Employee empl = FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                            FS.HISFC.Models.Base.Department deptempl = empl.Dept as FS.HISFC.Models.Base.Department;

                            if (dept.HospitalID.Equals(deptempl.HospitalID))
                            {
                                this.alSeeNo.Insert(0, regObj);
                            }

                            //this.alSeeNo.Insert(0, regObj);
                            //this.Register = regObj;//�ڿؼ��������»�ùҺ���Ϣ
                        }
                    }

                    //δ�ҵ���Ч�Һż�¼��ϵͳ���Һ�
                    if (this.alSeeNo == null || this.alSeeNo.Count <= 0)
                    {
                        //houwb 2011-3-16 {3F83B7FE-39C7-4f13-87BB-B0B229F2949F}
                        if (this.useType == enumUseType.Doct
                            && this.AllowNoRegSee()
                            && AllowAddNewReg())
                        {
                            FS.HISFC.Models.Registration.Register regObj = this.GetRegInfoFromPatientInfo(accountCardObj.Patient.PID.CardNO);
                            if (regObj == null)
                            {
                                return -1;
                            }
                            if (!this.hsReg.ContainsKey(regObj.ID))
                            {
                                this.hsReg.Add(regObj.ID, regObj);
                            }

                            //this.alSeeNo.Add(regObj);
                            //this.Register = ((FS.HISFC.Models.Registration.Register)this.alSeeNo[0]);
                            this.Register = regObj;
                        }
                        else
                        {
                            MessageBox.Show("û�в��ҵ��û�������Чʱ���ڵĹҺ���Ϣ!", "����");
                            this.txtInputCode.Focus();
                            txtInputCode.SelectAll();
                            return -1;
                        }
                    }
                    else if (this.alSeeNo.Count == 1)
                    {
                        regObj = ((FS.HISFC.Models.Registration.Register)this.alSeeNo[0]);
                        if (this.CheckRegInfo(ref regObj) == -1)
                        {
                            return -1;
                        }
                        this.Register = regObj;
                    }
                    else
                    {
                        this.SelectPatient();
                        return 1;
                    }
                    #endregion


                    if (this.listform != null)
                    {
                        this.listform.Close();
                    }
                    this.myEvents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Query" + ex.Message);
                ClearInfo();
                return -1;
            }

            return 1;
        }


        /// <summary>
        /// �Ƿ������
        /// </summary>
        /// <param name="index">0 ��ҽ����1 ����</param>
        /// <param name="index">0 ����ʾ��������(��Ч�Һţ���1 ��ʾ�Ƿ��գ�2 ����ʾ������</param>
        /// <param name="regObj"></param>
        /// <param name="isAddRegFee"></param>
        /// <returns></returns>
        private int CheckRegInfo(int index, int checkType, ref FS.HISFC.Models.Registration.Register regObj, ref bool isAddRegFee)
        {
            if (!regObj.IsSee)
            {
                return 1;
            }

            if (index == 0)
            {
                //���ҡ�ҽ����һ�� ��ʾ�Ƿ��չҺŷ�
                if (!(string.IsNullOrEmpty(regObj.SeeDoct.ID) && string.IsNullOrEmpty(regObj.SeeDoct.Dept.ID))
                    && (regObj.SeeDoct.ID != this.contrlManager.Operator.ID)
                    )
                {
                    //��Һſ��Ҳ�����ʾ���չҺŷ�
                    if (hsNoSupplyRegDept != null
                        && hsNoSupplyRegDept.Contains(((FS.HISFC.Models.Base.Employee)this.contrlManager.Operator).Dept.ID))
                    {
                        if (checkType == 0)
                        {
                            isAddRegFee = true;
                            return -1;
                        }
                        else if (checkType == 1)
                        {
                            DialogResult diag = MessageBox.Show("�û����Ѿ�����������Ϊ[" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(regObj.SeeDoct.Dept.ID) + "],\r\n\r\n����ҽ��Ϊ[" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(regObj.SeeDoct.ID) + "]\r\n\r\n����ʱ��Ϊ��" + regObj.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm:ss") + "\n\r\n�����Ƿ��չҺŷѣ�", "ѯ��", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (diag == DialogResult.Yes)
                            {
                                if (AllowAddNewReg())
                                {
                                    //������չҺŷѵĻ�����Ҫ�޸Ļ���״̬����Ϣ
                                    regObj = this.GetRegInfoFromPatientInfo(regObj.PID.CardNO);
                                    if (regObj == null)
                                    {
                                        return -1;
                                    }
                                    regObj.Memo = "������";
                                    isAddRegFee = false;
                                }
                                else
                                {
                                    MessageBox.Show("�뻼�ߵ��ҺŴ����Һź��������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else if (diag == DialogResult.Cancel)
                            {
                                isAddRegFee = false;
                                return -1;
                            }
                            else
                            {
                                regObj.Memo = "������";
                                isAddRegFee = false;
                            }
                        }
                        else
                        {
                            if (AllowAddNewReg())
                            {
                                //������չҺŷѵĻ�����Ҫ�޸Ļ���״̬����Ϣ
                                regObj = this.GetRegInfoFromPatientInfo(regObj.PID.CardNO);
                                if (regObj == null)
                                {
                                    return -1;
                                }
                                regObj.Memo = "������";
                                isAddRegFee = false;
                            }
                            else
                            {
                                MessageBox.Show("�뻼�ߵ��ҺŴ����Һź��������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        if (checkType == 0)
                        {
                            isAddRegFee = true;
                            return -1;
                        }
                        else if (checkType == 1)
                        {
                            DialogResult diag = MessageBox.Show("�û����Ѿ�����������Ϊ[" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(regObj.SeeDoct.Dept.ID) + "],\r\n\r\n����ҽ��Ϊ[" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(regObj.SeeDoct.ID) + "]\r\n\r\n����ʱ��Ϊ��" + regObj.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm:ss") + "\n\r\n�����Ƿ��չҺŷѣ�", "ѯ��", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (diag == DialogResult.Yes)
                            {
                                if (AllowAddNewReg())
                                {
                                    //������չҺŷѵĻ�����Ҫ�޸Ļ���״̬����Ϣ
                                    regObj = this.GetRegInfoFromPatientInfo(regObj.PID.CardNO);
                                    if (regObj == null)
                                    {
                                        return -1;
                                    }
                                    isAddRegFee = true;
                                }
                                else
                                {
                                    MessageBox.Show("�뻼�ߵ��ҺŴ����Һź��������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else if (diag == DialogResult.Cancel)
                            {
                                isAddRegFee = false;
                                return -1;
                            }
                            else
                            {
                                regObj.Memo = "������";
                                isAddRegFee = false;
                            }
                        }
                        else
                        {
                            if (AllowAddNewReg())
                            {
                                //������չҺŷѵĻ�����Ҫ�޸Ļ���״̬����Ϣ
                                regObj = this.GetRegInfoFromPatientInfo(regObj.PID.CardNO);
                                if (regObj == null)
                                {
                                    return -1;
                                }
                                isAddRegFee = true;
                            }
                            else
                            {
                                MessageBox.Show("�뻼�ߵ��ҺŴ����Һź��������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            else if (index == 1)
            {
                //�������ڲ�һ����ʾ�Ƿ��չҺŷ�
                if ((regObj.SeeDoct.ID == this.contrlManager.Operator.ID)
                    && regObj.DoctorInfo.SeeDate.Date != this.conManager.GetDateTimeFromSysDateTime().Date)
                {
                    //��Һſ��Ҳ�����ʾ���չҺŷ�
                    if (hsNoSupplyRegDept != null
                        && hsNoSupplyRegDept.Contains(((FS.HISFC.Models.Base.Employee)this.contrlManager.Operator).Dept.ID))
                    {
                        if (checkType == 0)
                        {
                            isAddRegFee = true;
                            return -1;
                        }
                        else if (checkType == 1)
                        {
                            DialogResult diag = MessageBox.Show("�û����Ѿ�����ϴο���ҽ��Ϊ[" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(regObj.SeeDoct.ID) + "],\r\n\r\n����ʱ��Ϊ[" + regObj.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm:ss") + "]\n\r\n�Ƿ����¹Һţ�(������ȡ�Һŷѣ�)", "ѯ��", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (diag == DialogResult.Yes)
                            {
                                if (AllowAddNewReg())
                                {
                                    //������չҺŷѵĻ�����Ҫ�޸Ļ���״̬����Ϣ
                                    regObj = this.GetRegInfoFromPatientInfo(regObj.PID.CardNO);
                                    if (regObj == null)
                                    {
                                        return -1;
                                    }
                                    regObj.Memo = "������";
                                    isAddRegFee = false;
                                }
                                else
                                {
                                    MessageBox.Show("�뻼�ߵ��ҺŴ����Һź��������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else if (diag == DialogResult.Cancel)
                            {
                                isAddRegFee = false;
                                return -1;
                            }
                            else
                            {
                                regObj.Memo = "������";
                                isAddRegFee = false;
                            }
                        }
                        else
                        {
                            if (AllowAddNewReg())
                            {
                                //������չҺŷѵĻ�����Ҫ�޸Ļ���״̬����Ϣ
                                regObj = this.GetRegInfoFromPatientInfo(regObj.PID.CardNO);
                                if (regObj == null)
                                {
                                    return -1;
                                }
                                regObj.Memo = "������";
                                isAddRegFee = false;
                            }
                            else
                            {
                                MessageBox.Show("�뻼�ߵ��ҺŴ����Һź��������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        if (checkType == 0)
                        {
                            isAddRegFee = true;
                            return -1;
                        }
                        else if (checkType == 1)
                        {
                            DialogResult diag = MessageBox.Show("�û����Ѿ�����ϴο���ҽ��Ϊ[" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(regObj.SeeDoct.ID) + "],\r\n\r\n����ʱ��Ϊ[" + regObj.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm:ss") + "]\n\r\n�Ƿ����¹Һţ�(������ȡ�Һŷѣ�)", "ѯ��", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (diag == DialogResult.Yes)
                            {
                                if (AllowAddNewReg())
                                {
                                    //������չҺŷѵĻ�����Ҫ�޸Ļ���״̬����Ϣ
                                    regObj = this.GetRegInfoFromPatientInfo(regObj.PID.CardNO);
                                    if (regObj == null)
                                    {
                                        return -1;
                                    }
                                    isAddRegFee = true;
                                }
                                else
                                {
                                    MessageBox.Show("�뻼�ߵ��ҺŴ����Һź��������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else if (diag == DialogResult.Cancel)
                            {
                                isAddRegFee = false;
                                return -1;
                            }
                            else
                            {
                                regObj.Memo = "������";
                                isAddRegFee = false;
                            }
                        }
                        else
                        {
                            if (AllowAddNewReg())
                            {
                                //������չҺŷѵĻ�����Ҫ�޸Ļ���״̬����Ϣ
                                regObj = this.GetRegInfoFromPatientInfo(regObj.PID.CardNO);
                                if (regObj == null)
                                {
                                    return -1;
                                }
                                isAddRegFee = true;
                            }
                            else
                            {
                                MessageBox.Show("�뻼�ߵ��ҺŴ����Һź��������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// �жϿ�����Ϣ
        /// </summary>
        /// <param name="regObj"></param>
        /// <returns>0 �����գ�-1 ����</returns>
        private int CheckRegInfo(ref FS.HISFC.Models.Registration.Register regObj)
        {
            //if (!isTipAddNewReg)
            //{
            //    return 1;
            //}

            if (regObj == null)
            {
                return -1;
            }

            //��ǰ�����Ƿ����²��չҺŷѡ����
            bool isAddRegFee = false;

            if (regObj.IsSee)
            {
                if (CheckRegInfo(0, isAddRegFee_OtherDoct, ref regObj, ref isAddRegFee) == -1)
                {
                    return -1;
                }

                if (!isAddRegFee)
                {
                    if (CheckRegInfo(1, isAddRegFee_OtherDay, ref regObj, ref isAddRegFee) == -1)
                    {
                        return -1;
                    }
                }
            }

            return 1;
        }

        /// <summary>
        /// �Ƿ�������ң�������ҹҺż���ʼ��������
        /// </summary>
        bool isOrdinaryRegDept = false;

        /// <summary>
        /// ���ݻ�����Ϣ��ȡ�Һ���Ϣ
        /// </summary>
        /// <param name="cardNO">���߿���</param>
        /// <returns>�Һ�ʵ��</returns>
        private FS.HISFC.Models.Registration.Register GetRegInfoFromPatientInfo(string cardNO)
        {
            #region ��ȡ���߻�����Ϣ

            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

            FS.HISFC.Models.RADT.PatientInfo patientInfo = manager.QueryComPatientInfo(cardNO);
            if (patientInfo == null
                ||string.IsNullOrEmpty(patientInfo.PID.CardNO))
            {
                MessageBox.Show("��ѯ���߻�����Ϣ�����޷����Һţ�\r\n\r\n��������ﻼ�ߣ����Ⱦ����ҺŴ��Һţ�\r\n" + manager.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return null;
            }

            #endregion

            FS.HISFC.Models.Registration.Register regObj = new FS.HISFC.Models.Registration.Register();

            FS.HISFC.Models.Base.Employee oper = this.managerIntegrate.GetEmployeeInfo(this.contrlManager.Operator.ID);
            try
            {
                //ϵͳ���ҺŻ��ߣ���ˮ��Ϊ�º�
                //����regObj.IsFee�ж��Ƿ��ǲ��Һ�
                regObj.ID = this.contrlManager.GetSequence("Registration.Register.ClinicID");
                regObj.TranType = FS.HISFC.Models.Base.TransTypes.Positive;//������
                regObj.PID = patientInfo.PID;

                //����ʱ����ж��Ƿ���
                //regObj.DoctorInfo.Templet.RegLevel.IsEmergency = (this.cmbRegLevel.SelectedItem as FS.HISFC.Models.Registration.RegLevel).IsEmergency;

                regObj.DoctorInfo.Templet.Dept.ID = ((FS.HISFC.Models.Base.Employee)this.contrlManager.Operator).Dept.ID;
                regObj.DoctorInfo.Templet.Dept.Name = ((FS.HISFC.Models.Base.Employee)this.contrlManager.Operator).Dept.Name;
                regObj.DoctorInfo.Templet.Doct.ID = this.contrlManager.Operator.ID;
                regObj.DoctorInfo.Templet.Doct.Name = this.contrlManager.Operator.Name;

                regObj.Name = patientInfo.Name;//��������
                regObj.Sex = patientInfo.Sex;//�Ա�
                regObj.Birthday = patientInfo.Birthday;//��������			

                regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Reg;

                regObj.InputOper.ID = this.contrlManager.Operator.ID;
                regObj.InputOper.OperTime = this.contrlManager.GetDateTimeFromSysDateTime();
                regObj.DoctorInfo.SeeDate = this.contrlManager.GetDateTimeFromSysDateTime();
                regObj.SeeDoct.ID = this.conManager.Operator.ID;
                regObj.SeeDoct.Dept.ID = ((FS.HISFC.Models.Base.Employee)this.contrlManager.Operator).Dept.ID;
                regObj.DoctorInfo.Templet.Begin = this.contrlManager.GetDateTimeFromSysDateTime();
                regObj.DoctorInfo.Templet.End = this.contrlManager.GetDateTimeFromSysDateTime();

                #region ���
                if (regObj.DoctorInfo.SeeDate.Hour < 12 && regObj.DoctorInfo.SeeDate.Hour > 6)
                {
                    //����
                    regObj.DoctorInfo.Templet.Noon.ID = "1";
                }
                else if (regObj.DoctorInfo.SeeDate.Hour > 12 && regObj.DoctorInfo.SeeDate.Hour < 18)
                {
                    //����
                    regObj.DoctorInfo.Templet.Noon.ID = "2";
                }
                else
                {
                    //����
                    regObj.DoctorInfo.Templet.Noon.ID = "3";
                }
                #endregion

                //����ר�ҿ��޶� �Ȳ�����


                //��ͬ��λ���ݰ쿨��¼��ȡ���������ȡ����
                regObj.Pact = patientInfo.Pact;
                if (string.IsNullOrEmpty(regObj.Pact.ID))
                {
                    regObj.Pact.ID = "1";
                    regObj.Pact.Name = "��ͨ";
                    regObj.Pact.PayKind.ID = "01";
                    regObj.Pact.PayKind.Name = "�Է�";
                }

                #region ȫ���ԷѴ���

                ArrayList alOwnFeeRegDept = this.conManager.GetList("OwnFeeRegDept");
                if (alOwnFeeRegDept == null)
                {
                    MessageBox.Show("��ȡ�ԷѹҺſ���ʧ�ܣ�" + conManager.Err);
                    return null;
                }

                foreach (FS.HISFC.Models.Base.Const constObj in alOwnFeeRegDept)
                {
                    if (constObj.IsValid && constObj.ID.Trim() == this.GetReciptDept().ID)
                    {
                        ArrayList alOwnFeeRegLevl = this.conManager.GetList("OwnFeeRegLevl");
                        if (alOwnFeeRegLevl == null || alOwnFeeRegLevl.Count == 0)
                        {
                            MessageBox.Show("��ȡ�ԷѹҺż���ʧ�ܣ�" + conManager.Err);
                            return null;
                        }

                        foreach (FS.HISFC.Models.Base.Const obj in alOwnFeeRegLevl)
                        {
                            if (obj.IsValid)
                            {
                                regObj.Pact.ID = obj.ID;
                                regObj.Pact.Name = "��ͨ";
                                regObj.Pact.PayKind.ID = "01";
                                regObj.Pact.PayKind.Name = "�Է�";
                                break;
                            }
                        }

                        break;
                    }
                }
                #endregion

                #region �Һż���

                string regLevl = "";

                isOrdinaryRegDept = false;

                #region ����Һſ���
                ArrayList alOrdinaryRegDept = this.conManager.GetList("OrdinaryRegLevlDept");
                if (alOrdinaryRegDept == null)
                {
                    MessageBox.Show("��ȡ����Һſ���ʧ�ܣ�" + conManager.Err);
                    return null;
                }

                foreach (FS.HISFC.Models.Base.Const constObj in alOrdinaryRegDept)
                {
                    if (constObj.IsValid && constObj.ID.Trim() == this.GetReciptDept().ID)
                    {
                        isOrdinaryRegDept = true;
                        break;
                    }
                }

                #endregion

                //����
                if (isOrdinaryRegDept)
                {
                    ArrayList alOrdinaryLevl = this.conManager.GetList("OrdinaryRegLevel");
                    if (alOrdinaryLevl == null || alOrdinaryLevl.Count == 0)
                    {
                        MessageBox.Show("��ȡ��ͨ�����Ӧ�ĹҺż������" + conManager.Err);
                        return null;
                    }

                    foreach (FS.HISFC.Models.Base.Const constObj in alOrdinaryLevl)
                    {
                        if (constObj.IsValid)
                        {
                            regLevl = constObj.ID.Trim();
                            break;
                        }
                    }
                }
                else
                {
                    //�Ƿ���
                    bool isEmerg = this.regInterMgr.IsEmergency(this.GetReciptDept().ID);

                    string diagItemCode = "";
                    if (isEmerg)
                    {
                        regObj.DoctorInfo.Templet.RegLevel.IsEmergency = true;

                        regLevl = emergencyLevlCode;

                        if (string.IsNullOrEmpty(regLevl))
                        {
                            MessageBox.Show("��ȡ�Һż������\r\nԭ��:����Һż���û��ά����\r\n������������ϵ��Ϣ�ƣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        //MessageBox.Show("�Ǽ�����ұ����ȹҺţ�");
                        //return null;
                        ///*
                        if (this.regInterMgr.GetSupplyRegInfo(oper.ID, oper.Level.ID.ToString(), regObj.DoctorInfo.Templet.Dept.ID, ref regLevl, ref diagItemCode) == -1)
                        {
                            MessageBox.Show("GetRegInfoFromPatientInfo" + regInterMgr.Err);
                            return null;
                        }
                        //*/ 
                    }
                }

                FS.HISFC.Models.Registration.RegLevel regLevlObj = null;

                if (regLevlHelper != null && regLevlHelper.ArrayObject.Count != 0)
                {
                    regLevlObj = regLevlHelper.GetObjectFromID(regLevl) as FS.HISFC.Models.Registration.RegLevel;
                }

                if (regLevlObj == null)
                {
                    regLevlObj = this.regInterMgr.QueryRegLevelByCode(regLevl);
                    if (regLevlObj == null)
                    {
                        MessageBox.Show("��ѯ�Һż�����󣬱���[" + regLevl + "]������ϵ��Ϣ������ά��" + regInterMgr.Err);
                        return null;
                    }
                }

                regObj.DoctorInfo.Templet.RegLevel = regLevlObj;
                #endregion

                regObj.SSN = patientInfo.SSN;//ҽ��֤��

                regObj.PhoneHome = patientInfo.PhoneHome;//��ϵ�绰
                regObj.AddressHome = patientInfo.AddressHome;//��ϵ��ַ
                regObj.CardType = patientInfo.IDCardType; //֤������

                regObj.IsFee = false;
                regObj.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Valid;
                //֮ǰΪʲô��Ϊtrue�أ���
                regObj.IsSee = false;
                regObj.CancelOper.ID = "";
                regObj.CancelOper.OperTime = DateTime.MinValue;
                regObj.IDCard = patientInfo.IDCard;

                regObj.PVisit.InState.ID = "N";
                regObj.DoctorInfo.SeeNO = -1;

                //���ܴ���
                if (patientInfo.IsEncrypt)
                {
                    regObj.IsEncrypt = true;
                    regObj.NormalName = FS.FrameWork.WinForms.Classes.Function.Encrypt3DES(patientInfo.Name);
                    regObj.Name = "******";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetRegInfoFromPatientInfo" + ex.Message);
                return null;
            }

            if (isUserOnePersonMorePact)
            {
                if (account.GetPatientPactInfo(regObj) == -1)
                {
                    MessageBox.Show("��ȡ���ߺ�ͬ��λ��Ϣʧ�ܣ�" + account.Err);
                    return null;
                }

                if (regObj.MutiPactInfo.Count > 1)
                {
                    FS.FrameWork.Models.NeuObject pactObj = new FS.FrameWork.Models.NeuObject();
                    if (regObj.MutiPactInfo.Count > 0)
                    {
                        if (FrameWork.WinForms.Classes.Function.ChooseItem(new ArrayList(regObj.MutiPactInfo), new string[] { "��ͬ��λ����", "��ͬ��λ", "��Ч��" }, new bool[] { false, true, true, false, false, false }, new int[] { 50, 100, 70 }, ref pactObj) != 1)
                        {
                            return null;
                        }
                    }

                    if (pactObj != null && !string.IsNullOrEmpty(pactObj.ID))
                    {
                        regObj.Pact = pactObj as FS.HISFC.Models.Base.PactInfo;
                    }
                }

                if (this.cbxNewReg.Checked)
                {
                    if (MessageBox.Show("�û��߽���[" + regObj.Pact.Name + "]��������²��Һţ��Ƿ����²��չҺŷѣ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    {
                        regObj.Memo = "������";
                    }
                }

                this.cbxNewReg.Checked = false;
            }

            return regObj;
        }

        /// <summary>
        /// ��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject reciptDept = null;

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetReciptDept()
        {
            try
            {
                if (this.reciptDept == null)
                {
                    FS.HISFC.Models.Registration.Schema schema = this.regInterMgr.GetSchema(this.contrlManager.Operator.ID, this.contrlManager.GetDateTimeFromSysDateTime());
                    if (schema != null && schema.Templet.Dept.ID != "")
                    {
                        this.reciptDept = schema.Templet.Dept.Clone();
                    }
                    //û���Ű�ȡ��½������Ϊ��������
                    else
                    {
                        this.reciptDept = ((FS.HISFC.Models.Base.Employee)this.contrlManager.Operator).Dept.Clone(); //��������
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetReciptDept" + ex.Message);
                return null;
            }
            return this.reciptDept;
        }

        #endregion

        #region ����

        /// <summary>
        /// �Զ��ҺŵĲ����ſ�ͷ�ַ�
        /// </summary>
        //private string strFormatHeader = "";

        ///// <summary>
        ///// �Զ������������ɲ����ţ���������
        ///// </summary>
        //private int intDateType = 0;

        //protected ToolTip tooltip = new ToolTip();

        ///// <summary>
        ///// �����ų���
        ///// </summary>
        //private int intLength = 10;





        #region ������ͷ������ ���䲡����

        /// <summary>
        /// ¼��������ı���ʽ�������㣨����������ų��ȣ�
        /// </summary>
        /// <param name="Length"></param>
        public void SetFormat(int Length)
        {
            this.SetFormat("", 0, Length);
        }

        /// <summary>
        /// ¼��������ı���ʽ��������ͷ����������ͷ�ַ�������ų��ȣ�
        /// </summary>
        /// <param name="Header"></param>
        /// <param name="Length"></param>
        public void SetFormat(string Header, int Length)
        {
            this.SetFormat(Header, 0, Length);
        }

        /// <summary>
        /// ¼��������ı���ʽ��������ͷ������ڣ���������ͷ�ַ���ʱ�䣻����ų��ȣ�
        /// </summary>
        /// <param name="Header"></param>
        /// <param name="DateType"></param>
        /// <param name="Length"></param>
        public void SetFormat(string Header, int DateType, int Length)
        {
            //this.intLength = Length;
            //this.strFormatHeader = Header;
            //this.intDateType = DateType;
        }

        /// <summary>
        /// ������ͷ������ ���䲡����
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        private string formatInputCode(string Text)
        {
            return null;
            //string strText = Text;
            //try
            //{
            //    for (int i = 0; i < this.intLength - strText.Length; i++)
            //    {
            //        Text = "0" + Text;
            //    }
            //    string strDateTime = "";
            //    try
            //    {
            //        strDateTime = this.contrlManager.GetSysDateNoBar();
            //    }
            //    catch { }
            //    switch (this.intDateType)
            //    {
            //        case 1:
            //            strDateTime = strDateTime.Substring(2);
            //            Text = strDateTime + Text.Substring(strDateTime.Length);
            //            break;
            //        case 2:
            //            Text = strDateTime + Text.Substring(strDateTime.Length);
            //            break;
            //    }
            //    if (this.strFormatHeader != "") Text = this.strFormatHeader + Text.Substring(this.strFormatHeader.Length);
            //}
            //catch { }
            ////����   
            //return Text;
        }

        #endregion

        /// <summary>
        /// ˫�������Ļ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lvAllReg_DoubleClick(object sender, EventArgs e)
        {
            if ((sender as FS.FrameWork.WinForms.Controls.NeuListView).SelectedItems.Count > 0)
            {
                ListViewItem listItem = (sender as FS.FrameWork.WinForms.Controls.NeuListView).SelectedItems[0];

                if (listItem != null)
                {
                    this.txtInputCode.Text = listItem.SubItems[0].Text;
                }
            }

            ((sender as ListView).Parent as Form).Close();
        }

        private void txtInputCode_Enter(object sender, EventArgs e)
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

                if (this.txtInputCode.Text.Length >= 2 && System.Text.Encoding.Default.GetBytes(this.txtInputCode.Text.Substring(0, 1)).Length > 1)
                {
                    this.txtInputCode.Text = "";
                }
            }
            catch
            { }
        }

        #endregion
    }

    /// <summary>
    /// �˿ؼ�ʹ�õĵط�
    /// ���ط�����һ��
    /// </summary>
    public enum enumUseType
    {
        /// <summary>
        /// �Һ�
        /// </summary>
        Register,

        /// <summary>
        /// �쿨
        /// </summary>
        TransactCard,

        /// <summary>
        /// �շѴ�
        /// </summary>
        Charge,

        /// <summary>
        /// �˺�
        /// </summary>
        CancelFee,

        /// <summary>
        /// ����ҽ��
        /// </summary>
        Doct,

        /// <summary>
        /// �����ط�
        /// </summary>
        Other
    }
}
