using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

namespace FS.HISFC.Components.Common.Controls
{
    public delegate void selectedEventDelegate();

    public partial class txtClinicInput : FS.FrameWork.WinForms.Controls.NeuTextBox
    {
        public txtClinicInput()
        {
            InitializeComponent();
        }

        public txtClinicInput(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        

        #region ˽�б���

        private ArrayList alInfo = new ArrayList();
        private ArrayList alRegInfo = new ArrayList();
        //�����
        private string clinicCode = "";
        //���￨��
        private string cardNO = "";
        //�Ƿ�ֻ��ʾ��������Ϣ
        private bool isShowOwnDept = false;
        //�Ƿ����ڲ�ѯ
        private bool isExecQuery = true;

        private DateTime dtNow = DateTime.Now;
        private string strFormatHeader = "";
        
        private int intLength = 10;
        private int validDays = 1;//�Һ���Ч����

        //�Һ���Ϣ
        private FS.HISFC.Models.Registration.Register regInfo = new FS.HISFC.Models.Registration.Register();
        //��ѯ����
        private string queryType ="0";
        private System.Windows.Forms.Form listform;
        private System.Windows.Forms.ListBox lst;
        private bool isShowMarkNO = false;
        private string markNO = string.Empty;
        //�ж��Ƿ�ˢ�������Ƿ���ʾ��������
        private bool isMarkNo = false;
        #region ҵ���
        //ҽ��ҵ���
        protected FS.HISFC.BizLogic.Order.OutPatient.Order orderManagement = new FS.HISFC.BizLogic.Order.OutPatient.Order();
        //����ҵ���
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlManagement = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        //�Һ�ҵ���
        protected FS.HISFC.BizProcess.Integrate.Registration.Registration regManagement = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        /// <summary>
        /// �ʻ�ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        #endregion

        #endregion

        #region �������ԡ�����

        [Category("����"), Description("ֻ����ˢ����ʱ��������Բ������ã��Ƿ���ʾ������")]
        public bool IsShowMarkNO
        {
            get
            {
                return isShowMarkNO;
            }
            set
            {
                isShowMarkNO = value;
            }
        }

        /// <summary>
        /// �����
        /// </summary>
        public string ClinicCode
        {
            get { return this.clinicCode; }
        }
        /// <summary>
        /// ���￨��
        /// </summary>
        public string CardNO
        {
            get { return this.cardNO; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string MarkNO
        {
            get
            {
                return markNO;
            }
        }

        [Category("����"),Description("�Ƿ����ڲ�ѯ"),Browsable(true),DefaultValue(true)]
        public bool IsExecQuery
        {
            get { return this.isExecQuery; }
            set { this.isExecQuery = value; }
        }
        
        [Category("����"), Description("�Ƿ�ֻ��ʾ��������Ϣ"), Browsable(true), DefaultValue(false)]
        public bool IsShowOwnDept
        {
            get { return this.isShowOwnDept; }
            set { this.isShowOwnDept = value; }
        }

        /// <summary>
        /// �Һ���Ϣ
        /// </summary>
        public FS.HISFC.Models.Registration.Register Register
        {
            get 
            {
                if (this.clinicCode != "")
                {
                    this.regInfo = this.regManagement.GetByClinic(this.clinicCode);
                }
                return this.regInfo; 
            }
        }
        /// <summary>
        /// ���￨�Ÿ�ʽ����������������ͷ�ַ�������ų��ȣ�
        /// </summary>
        /// <param name="Header"></param>
        /// <param name="Length"></param>
        public void SetCardNOFormat(string Header, int Length)
        {
            this.intLength = Length;
            this.strFormatHeader = Header;
        }
        /// <summary>
        /// ������Ϣ�¼�
        /// </summary>
        public event Controls.selectedEventDelegate selectedEvents;

        #endregion

        #region ����

        /// <summary>
        /// ��ʽ��������ַ��������￨�ţ�
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        private string CardNOFormat(string Text)
        {
            
            string strText = Text;
            try
            {
                for (int i = 0; i < this.intLength - strText.Length; i++)
                {
                    Text = "0" + Text;
                }

                if (this.strFormatHeader != "")
                {
                    Text = this.strFormatHeader + Text.Substring(this.strFormatHeader.Length);
                }
            }
            catch { }
            
            return Text;
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        public void Query()
        {
            string txtInput = this.CardNOFormat(this.Text.Trim());
            if (txtInput == string.Empty)
            {
                MessageBox.Show("���������ݣ�");
                this.Focus();
                return;
            }
            this.alInfo = new ArrayList();
            this.QueryByCardNO(txtInput);
            this.QueryByMarkNO();
            //��ˢ��
            if (isMarkNo)
            {
                //��ʾ����
                if (isShowMarkNO)
                    this.Text = markNO;
                else
                    this.Text = cardNO;
            }
            else
            {
                this.Text = cardNO;
            }
            isMarkNo = false;
            if (this.alInfo.Count == 1)
            {
                this.clinicCode = ((FS.FrameWork.Models.NeuObject)this.alInfo[0]).User03;
            }
            else if (this.alInfo.Count <= 0)
            {
                this.NoInfo();
                MessageBox.Show("δ��ѯ����Ϣ","��ʾ");
            }
            else
            {
                this.SelectPatient();
                return;
            }
            try
            {
                if (this.listform != null)
                {
                    this.listform.Close();
                }
            }
            catch { }
            try
            {
                this.selectedEvents();
            }
            catch { }
        }

        /// <summary>
        /// ���ݿ��Ų�ѯ
        /// </summary>
        private void QueryByCardNO(string txtInput)
        {
            try
            {
                ArrayList alReg = new ArrayList();
                this.queryType = "0";
                this.cardNO = txtInput;
                this.validDays = this.ctrlManagement.GetControlParam<int>("MZ0014", false, 1);
                dtNow = this.orderManagement.GetDateTimeFromSysDateTime();
                alReg = this.regManagement.Query(txtInput, dtNow.AddDays(-this.validDays));
                if (alReg.Count > 0)
                {
                    for (int i = 0; i < alReg.Count; i++)
                    {
                        FS.HISFC.Models.Registration.Register obj = alReg[i] as FS.HISFC.Models.Registration.Register;
                        this.regInfo = obj;//�ڿؼ��������»�ùҺ���Ϣ
                        FS.FrameWork.Models.NeuObject o = new FS.FrameWork.Models.NeuObject();
                        if (obj.DoctorInfo.SeeDate.Date == dtNow.Date)
                        {
                            o.ID = "��";
                            o.Memo = "����          ";
                        }
                        else
                        {
                            o.ID = "  ";
                            o.Memo = obj.DoctorInfo.SeeDate.ToString("yyyy��MM��dd��");
                        }
                        o.Name = obj.Name + "(" + obj.RecipeNO + ")";  //����˴�����,����ҽ��ȷ��
                        o.User02 = obj.DoctorInfo.Templet.Dept.Name;
                        o.User03 = obj.ID;//������ˮ��
                        
                        o.User01 = ((FS.HISFC.Models.Base.Employee)this.orderManagement.Operator).Dept.ID;
                        //��ʾ������ţ��������������ڣ�ȥ��ʱ�䣩,�������
                        try
                        {
                            o.Name = o.ID + "  " + o.Name + "  " + o.User02 + "  " + o.Memo + "  " + o.User03;
                        }
                        catch
                        {
                            
                        }
                        o.ID = this.queryType;
                        this.alInfo.Insert(0, o);
                    }
                }
            }
            catch { }
        }
        /// <summary>
        /// ���������Ų������￨��
        /// </summary>
        protected virtual void QueryByMarkNO()
        {
            string txtinput = this.Text.Trim();
            this.markNO = string.Empty;
            //���ݿ��Ź����ж��Ƿ��ǿ�����ȡ������
            //{E24EF7EC-94EE-45b2-B717-E722A2D10068}
            FS.HISFC.Models.Account.AccountCard accountCard  = new FS.HISFC.Models.Account.AccountCard ();
            //if (feeIntegrate.ValidMarkNO(txtinput, ref markNO) < 0) return;
            if (feeIntegrate.ValidMarkNO(txtinput, ref accountCard ) <0) return;
            markNO = accountCard.MarkNO;
            cardNO = string.Empty;
            //���ҿ�������Ӧ��
            //bool bl = feeIntegrate.GetCardNoByMarkNo(markNO, FS.HISFC.Models.Account.MarkTypes.Magcard, ref cardNO);
            bool bl = feeIntegrate.GetCardNoByMarkNo(markNO, ref cardNO);
            if (!bl)
            {
                MessageBox.Show(this.feeIntegrate.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            isMarkNo = true;
            this.QueryByCardNO(cardNO);
        }

        /// <summary>
        /// ��ȡѡ�����Ϣ
        /// </summary>
        private void GetInfo()
        {
            try
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                                
                obj = lst.Items[lst.SelectedIndex] as FS.FrameWork.Models.NeuObject;
                if (obj.ID == "0")
                {
                    this.clinicCode = obj.User03;
                    if (this.clinicCode != "")
                    {
                        this.regInfo = this.regManagement.GetByClinic(this.clinicCode);
                    }
                    this.cardNO = this.regInfo.PID.CardNO;
                    try
                    {
                        this.listform.Hide();
                    }
                    catch
                    {

                    }
                    try
                    {
                        this.selectedEvents();
                    }
                    catch { }
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.ToString()); 
                this.NoInfo(); 
            }
        }

        /// <summary>
        /// ѡ����
        /// </summary>
        private void SelectPatient()
        {
            lst = new System.Windows.Forms.ListBox();
            lst.Dock = System.Windows.Forms.DockStyle.Fill;
            lst.Items.Clear();
            this.listform = new System.Windows.Forms.Form();
            //�ô�����ʾ			
            
            listform.Size = new Size(300, 200);
            listform.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            FS.HISFC.Models.Base.Employee user = this.orderManagement.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.BizLogic.Manager.Department managerDept = new FS.HISFC.BizLogic.Manager.Department();
            for (int i = 0; i < this.alInfo.Count; i++)
            {
                FS.FrameWork.Models.NeuObject obj;
                obj = (FS.FrameWork.Models.NeuObject)this.alInfo[i];
                bool b = false;
                if (this.isShowOwnDept)//���˲���������
                {
                    b = false;
                    if (user.EmployeeType.ID.ToString() == "N")//��ʿվ
                    {
                        FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();
                        ArrayList alDept = managerDept.GetDeptFromNurseStation(user.Nurse);
                        if (alDept == null)
                        {

                        }
                        else
                        {
                            for (int k = 0; k < alDept.Count; i++)
                            {
                                dept = alDept[k] as FS.FrameWork.Models.NeuObject;
                                if (dept.ID == obj.User01)
                                {
                                    b = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (user.Dept.ID == obj.User01)//���Ҷ�Ӧ��
                        {
                            b = true;
                        }
                    }
                }
                else
                {
                    b = true;
                }
                if (b)
                {

                    try
                    {
                        lst.Items.Add(obj);
                    }
                    catch { }
                                        
                }
            }
            if (lst.Items.Count == 1)
            {
                try
                {
                    this.listform.Close();

                }
                catch { }
                try
                {
                    
                    this.selectedEvents();
                }
                catch { }
                return;
            }

            if (lst.Items.Count <= 0)
            {
                this.NoInfo();
                this.selectedEvents();
                return;
            }

            lst.Visible = true;
            lst.DoubleClick += new EventHandler(lst_DoubleClick);
            lst.KeyDown += new KeyEventHandler(lst_KeyDown);
            lst.Show();

            listform.Controls.Add(lst);
            listform.TopMost = true;
            listform.Owner = this.FindForm();
            listform.Show();

            #region ������ʾλ��
            Point tp = new Point(0, this.Height);
            Point p = this.PointToScreen(tp);
            Screen[] screens = Screen.AllScreens;
            int width = screens[0].Bounds.Width;
            int height = screens[0].Bounds.Height;

            if (width - p.X < listform.Width)
            {
                tp = new Point(tp.X-listform.Width+this.Width, tp.Y);
            }
            if (height - p.Y < listform.Height)
            {
                tp = new Point(tp.X, tp.Y - this.Height-listform.Height-4);
            }
            listform.Location = this.PointToScreen(tp);
            #endregion

            try
            {
                lst.SelectedIndex = 0;
                lst.Focus();
                lst.LostFocus += new EventHandler(lst_LostFocus);
            }
            catch { }
            return;
        }

        /// <summary>
        /// ����Ϣ
        /// </summary>
        private void NoInfo()
        {
            this.clinicCode = "";
            this.cardNO = "";
            this.regInfo = new FS.HISFC.Models.Registration.Register();
        }

        #endregion

        #region ���������¼�

        private void lst_LostFocus(object sender, EventArgs e)
        {
            this.listform.Hide();
        }

        private void lst_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.GetInfo();
            }
            catch { }
        }

        private void lst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.GetInfo();
            }
        }

        #endregion

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (isExecQuery)
                {
                    this.Query();
                }               
            }
            base.OnKeyDown(e);
        }

        //protected override void OnKeyPress(KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == "/r")
        //    {
        //        if (isExecQuery)
        //        {
        //            e.Handled = true;
        //            this.Query();

        //        }
        //    }

        //    base.OnKeyPress(e);
        //}

        
    }
}
