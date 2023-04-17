using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Common.Controls
{

    public delegate void myEventDelegate();

    /// <summary>
    /// txtQueryInpatientNo ��ժҪ˵����
    /// ��ѯסԺ��ˮ�ſؼ�
    /// �����InpatientNos
    ///		  InpatientNo
    ///	��������Ҫ������̳�baseForm���ࡣ	  
    /// </summary>
    public partial class ucQueryInpatientNo : UserControl
    {
        public ucQueryInpatientNo()
        {
            InitializeComponent();
            Inpatient = new FS.HISFC.BizLogic.RADT.InPatient();
            this.txtInputCode.GotFocus += new EventHandler(txtInputCode_GotFocus);
            this.txtInputCode.Click += new EventHandler(txtInputCode_Click);
        }

        void txtInputCode_Click(object sender, EventArgs e)
        {
            this.txtInputCode.Focus();
            this.txtInputCode.Select(0, this.txtInputCode.Text.Length);
        }

        void txtInputCode_GotFocus(object sender, EventArgs e)
        {
            this.txtInputCode.Focus();
            this.txtInputCode.Select(0, this.txtInputCode.Text.Length);
        }

        #region ˽�б���
        private ArrayList alInpatientNos;
        private string strInpatientNo;
        private FS.HISFC.BizLogic.RADT.InPatient Inpatient = null;
        private System.Windows.Forms.Form listform;
        private System.Windows.Forms.ListBox lst;

        private string strFormatHeader = "";
        private int intDateType = 0;
        private int intLength = 10;
        #endregion

        #region �ɿ��ƹ������ԡ�����

        /// <summary>
        /// ��ǰ�������� 
        /// 0 סԺ��
        /// 1 סԺ��ˮ��
        /// 2 ����
        /// 3 �������պ�
        /// 4 ҽ��֤��
        /// 5 ����
        /// </summary>
        protected int inputtype = 0;

        /// <summary>
        /// ��������
        /// 0 סԺ��
        /// 1 סԺ��ˮ��
        /// 2 ����
        /// 3 �������պ�
        /// 4 ҽ��֤��
        /// 5 ����
        /// </summary>
        public int InputType
        {
            get
            {
                return this.inputtype;
            }
            set
            {
                if (value >= 5) value = 0;
                this.inputtype = value;
                switch (inputtype)
                {
                    //סԺ��
                    case 0:
                        this.txtInputCode.BackColor = Color.White;
                        this.label1.Text = "סԺ��:";
                        this.tooltip.SetToolTip(txtInputCode, "��ǰ����סԺ�Ų�ѯ��\n��F2�л���ѯ��ʽ��");
                        break;
                    //ֱ�Ӳ�סԺ��ˮ��
                    case 1:
                        this.label1.Text = "סԺ��ˮ��:";
                        this.tooltip.SetToolTip(txtInputCode, "��ǰ����סԺ��ˮ�Ų�ѯ��\n��F2�л���ѯ��ʽ��");
                        break;
                    //����
                    case 2:
                        this.label1.Text = "����:";
                        this.txtInputCode.BackColor = Color.FromArgb(255, 190, 190);
                        this.tooltip.SetToolTip(txtInputCode, "��ǰ����������ѯ��\n��F2�л���ѯ��ʽ��");
                        break;
                    //�������պ�
                    case 3:
                        this.label1.Text = "�������պ�:";
                        //						this.txtInputCode.BackColor =Color.FromArgb(255,150,150);
                        this.tooltip.SetToolTip(txtInputCode, "��ǰ����������ѯ��\n��F2�л���ѯ��ʽ��");
                        break;
                    case 4:
                        this.label1.Text = "ҽ��֤��:";
                        //						this.txtInputCode.BackColor =Color.FromArgb(255,100,100);
                        this.tooltip.SetToolTip(txtInputCode, "��ǰ����������ѯ��\n��F2�л���ѯ��ʽ��");
                        break;
                    //����
                    case 5:
                        this.label1.Text = "����:";
                        this.txtInputCode.BackColor = Color.FromArgb(255, 220, 220); ;
                        this.tooltip.SetToolTip(txtInputCode, "��ǰ���벡���Ų�ѯ��\n��F2�л���ѯ��ʽ��");
                        break;
                    default:
                        this.label1.Text = "סԺ��:";
                        this.txtInputCode.BackColor = Color.White;
                        this.tooltip.SetToolTip(txtInputCode, "��ǰ����סԺ�Ų�ѯ��\n��F2�л���ѯ��ʽ��");
                        break;
                }
                this.tooltip.Active = true;
            }
        }

        private int defaultInputType = 0;//Ĭ����������
        public int DefaultInputType
        {
            get
            {
                return defaultInputType;
            }
            set
            {
                defaultInputType = value;
                inputtype = value;
                switch (defaultInputType)
                {
                    //סԺ��
                    case 0:
                        this.txtInputCode.BackColor = Color.White;
                        this.label1.Text = "סԺ��:";
                        this.tooltip.SetToolTip(txtInputCode, "��ǰ����סԺ�Ų�ѯ��\n��F2�л���ѯ��ʽ��");
                        break;
                    //סԺ��ˮ��
                    case 1:
                        this.label1.Text = "סԺ��ˮ��:";
                        this.tooltip.SetToolTip(txtInputCode, "��ǰ����סԺ��ˮ�Ų�ѯ��\n��F2�л���ѯ��ʽ��");
                        break;
                    //����
                    case 2:
                        this.label1.Text = "����:";
                        this.txtInputCode.BackColor = Color.FromArgb(255, 190, 190);
                        this.tooltip.SetToolTip(txtInputCode, "��ǰ����������ѯ��\n��F2�л���ѯ��ʽ��");
                        break;
                    case 3:
                        this.label1.Text = "�������պ�:";
                        //this.txtInputCode.BackColor =Color.FromArgb(255,150,150);
                        this.tooltip.SetToolTip(txtInputCode, "��ǰ����������ѯ��\n��F2�л���ѯ��ʽ��");
                        break;
                    case 4:
                        this.label1.Text = "ҽ��֤��:";
                        //this.txtInputCode.BackColor =Color.FromArgb(255,100,100);
                        this.tooltip.SetToolTip(txtInputCode, "��ǰ����������ѯ��\n��F2�л���ѯ��ʽ��");
                        break;
                    //����
                    case 5:
                        this.label1.Text = "����:";
                        this.txtInputCode.BackColor = Color.FromArgb(255, 220, 220); ;
                        this.tooltip.SetToolTip(txtInputCode, "��ǰ���벡���Ų�ѯ��\n��F2�л���ѯ��ʽ��");
                        break;
                    default:
                        this.label1.Text = "סԺ��:";
                        this.txtInputCode.BackColor = Color.White;
                        this.tooltip.SetToolTip(txtInputCode, "��ǰ����סԺ�Ų�ѯ��\n��F2�л���ѯ��ʽ��");
                        break;
                }
                this.tooltip.Active = true;
            }
        }

        /// <summary>
        /// ����״̬
        /// </summary>
        private string patientInState = "ALL";
        public string PatientInState
        {
            get
            {
                return patientInState;
            }
            set
            {
                patientInState = value;
            }
        }

        /// <summary>
        /// �Ƿ�ֻ��ѯ��½������Ϣ
        /// </summary>
        private bool isDeptOnly = true;

        /// <summary>
        /// �Ƿ�ֻ��ѯ��½������Ϣ
        /// </summary>
        public bool IsDeptOnly
        {
            get
            {
                return isDeptOnly;
            }
            set
            {
                isDeptOnly = value;
            }
        }

        protected ToolTip tooltip = new ToolTip();
        /// <summary>
        /// ����
        /// </summary>
        protected bool isRestrictOwnDept = false;

        /// <summary>
        /// �Ƿ����Ʊ����һ���
        /// </summary>
        public bool IsRestrictOwnDept
        {
            set
            {
                this.isRestrictOwnDept = value;
            }
        }
       
        /// <summary>
        /// ¼��סԺ���ı���ʽ�������㣨������סԺ�ų��ȣ�
        /// </summary>
        /// <param name="Length"></param>
        public void SetFormat(int Length)
        {
            this.SetFormat("", 0, Length);
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string Err;
        /// <summary>
        /// ������Ϣ�¼�
        /// </summary>
        public event myEventDelegate myEvent;
        /// <summary>
        /// �õ�����סԺ��ˮ����Ϣ����
        /// </summary>
        public ArrayList InpatientNos
        {
            get
            {
                return this.alInpatientNos;
            }
        }

        protected enuShowState myShowState = enuShowState.All;

        /// <summary>
        /// ��ʾ����״̬
        /// </summary>
        public enuShowState ShowState
        {
            get
            {
                return this.myShowState;
            }
            set
            {
                this.myShowState = value;
            }
        }
        /// <summary>
        /// �õ�һ��סԺ��ˮ����Ϣ
        /// </summary>
        public string InpatientNo
        {
            get
            {
                //���²�һ�飬�����ˮ��Ϊ�գ�����סԺ�Ų�Ϊ�գ����ѯһ��
                if (string.IsNullOrEmpty(this.strInpatientNo)&&string.IsNullOrEmpty(this.Text)==false)
                {
                    this.query();
                }
                //�����ˮ�Ų�Ϊ�գ�����סԺ��Ϊ�գ���ô���
                else if (string.IsNullOrEmpty(this.strInpatientNo) == false && string.IsNullOrEmpty(this.Text))
                {
                    this.strInpatientNo = string.Empty;
                    this.alInpatientNos.Clear();
                }

                return this.strInpatientNo;
            }
        }

        /// <summary>
        /// סԺ���ı�¼������
        /// </summary>
        public new string Text
        {
            get
            {
                return this.txtInputCode.Text.ToUpper();
            }
            set
            {
                this.txtInputCode.Text = value;
            }
        }

        /// <summary>
        /// ��ǰ������ı��ؼ�
        /// </summary>
        public TextBox TextBox
        {
            get
            {
                return this.txtInputCode;
            }
           
        }
        /// <summary>
        /// ��ǰlabel�ؼ�
        /// </summary>
        public Label Label
        {
            get { return this.label1; }
           
        }

        private bool isCanChangeInputType = true;
        /// <summary>
        /// �Ƿ�����F2�任���뷽ʽ
        /// </summary>
        public bool IsCanChangeInputType
        {
            set
            {
                this.isCanChangeInputType = value;
            }
        }
        /// <summary>
        /// ǰ�հף�������Label������
        /// </summary>
        public int LabelMarginLeft
        {
            set
            {
                this.label1.Left = value;
            }
        }

        /// <summary>
        /// ¼��סԺ���ı���ʽ��������ͷ����������ͷ�ַ���סԺ�ų��ȣ�
        /// </summary>
        /// <param name="Header"></param>
        /// <param name="Length"></param>
        public void SetFormat(string Header, int Length)
        {
            this.SetFormat(Header, 0, Length);
        }
        /// <summary>
        /// ¼��סԺ���ı���ʽ��������ͷ������ڣ���������ͷ�ַ���ʱ�䣻סԺ�ų��ȣ�
        /// </summary>
        /// <param name="Header"></param>
        /// <param name="DateType"></param>
        /// <param name="Length"></param>
        public void SetFormat(string Header, int DateType, int Length)
        {
            this.intLength = Length;
            this.strFormatHeader = Header;
            this.intDateType = DateType;
        }
        /// <summary>
        /// 
        /// </summary>
        public new void Focus()
        {
            this.txtInputCode.SelectAll();
            this.txtInputCode.Focus();
        }
        #endregion

        /// <summary>
        /// Label ������ɫ
        /// </summary>
        public System.Drawing.Color LabelColor
        {
            set
            {
                this.label1.ForeColor = value;
            }
        }

        #region ���ɿ���˽�����ԡ�����

        private void txtInputCode_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void txtInputCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    query();
                }
                else if (e.KeyCode == Keys.F2)
                {
                    if(isCanChangeInputType)
                        this.InputType++;
                }
                else if (e.KeyCode == Keys.Space)
                {
                    query();
                }
            }
            catch { }
        }
        private void SelectPatient()
        {
            lst = new ListBox();
            lst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listform = new System.Windows.Forms.Form();

            listform.Size = new Size(500, 150);
            listform.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;

            lst.HorizontalScrollbar = true; 

            FS.HISFC.Models.Base.Employee user = new FS.HISFC.Models.Base.Employee();
            FS.HISFC.BizLogic.Manager.Department managerDept = new FS.HISFC.BizLogic.Manager.Department();
            for (int i = 0; i < this.alInpatientNos.Count; i++)
            {
                FS.FrameWork.Models.NeuObject obj;
                obj = (FS.FrameWork.Models.NeuObject)this.alInpatientNos[i];
                FS.HISFC.Models.RADT.InStateEnumService VisitStatus = new FS.HISFC.Models.RADT.InStateEnumService();
                VisitStatus.ID = obj.Memo;
                bool b = false;
                switch (this.myShowState)//���˻���״̬
                {
                    case enuShowState.InHos:
                        if (obj.Memo == "I") 
                            b = true;
                        break;
                    case enuShowState.OutHos:
                        if (obj.Memo == "B" || obj.Memo == "O" || obj.Memo == "P" || obj.Memo == "N") 
                            b = true;
                        break;
                    case enuShowState.BeforeArrived:
                        if (obj.Memo == "R") b = true;
                        break;
                    case enuShowState.AfterArrived:
                        if (obj.Memo != "R") b = true;
                        break;
                    case enuShowState.InhosBeforBalanced:
                        if (obj.Memo == "B" || obj.Memo == "I" || obj.Memo == "P" || obj.Memo == "R") 
                            b = true;
                        break;
                    case enuShowState.InhosAfterBalanced:
                        if (obj.Memo == "O") 
                            b = true;
                        break;
                    case enuShowState.InBalanced:
                        if (obj.Memo == "B") 
                            b = true;
                        break;
                    default:
                        b = true;
                        break;
                }
                if (b && this.isRestrictOwnDept)//���˲���������
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
                if (b)
                {
                    //��ʾסԺ��ˮ�ţ���������Ժ״̬
                    //���Ӵ�����ʾ
                    string bedNo = "";

                    try
                    {
                        if (obj.GetType() == typeof(FS.HISFC.Models.Base.Spell))
                        {
                            FS.HISFC.Models.Base.Spell spTemp = (FS.HISFC.Models.Base.Spell)obj;
                            if (!string.IsNullOrEmpty(spTemp.SpellCode))
                            {
                                bedNo = "   " + spTemp.SpellCode + "��";
                            }
                        }
                    }
                    catch
                    { }

                    try
                    {
                        lst.Items.Add((obj.ID.Length > 10 ? obj.ID.Substring(0, 5) : obj.ID) + "  " + obj.Name.PadRight(6, ' ').Replace(" ", "  ") + "  " + VisitStatus.Name.PadRight(12, ' ').Replace(" ", "  ") + "  " + obj.User02 + "  " + obj.User03.ToString().Substring(0, 10) + bedNo);
                    }
                    catch
                    {
                        lst.Items.Add((obj.ID.Length > 10 ? obj.ID.Substring(0, 5) : obj.ID) + "  " + obj.Name.PadRight(6, ' ').Replace(" ", "  ") + "  " + obj.Memo.PadRight(12, ' ').Replace(" ", "  ") + "  " + obj.User02 + "  " + obj.User03.ToString().Substring(0, 10) + bedNo);

                    }

                    this.strInpatientNo = obj.ID;
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
                    // {A31F656E-B2E4-494a-8142-65D17517C24A}
                    // ���߶��סԺʱ�ᱨ��
                    //this.Text = this.strInpatientNo.Substring(4, 10);
                    this.myEvent();
                }
                catch { }
                return;
            }

            //			if(lst.Items.Count <=0) return;
            if (lst.Items.Count <= 0)
            {
                this.strInpatientNo = "";
                NoInfo();
                this.myEvent();
                return;
            }

            lst.Visible = true;
            lst.DoubleClick += new EventHandler(lst_DoubleClick);
            lst.KeyDown += new KeyEventHandler(lst_KeyDown);
            lst.Show();

            listform.Controls.Add(lst);

            listform.TopMost = true;

            listform.Show();
            listform.Location = this.txtInputCode.PointToScreen(new Point(this.txtInputCode.Width / 2 + this.txtInputCode.Left, this.txtInputCode.Height + this.txtInputCode.Top));
            try
            {
                lst.SelectedIndex = 0;
                lst.Focus();
                lst.LostFocus += new EventHandler(lst_LostFocus);
            }
            catch { }
            return;
        }
        private string formatInputCode(string Text)
        {

            string strText = Text.PadLeft(this.intLength, '0');
            try
            {

                string strDateTime = "";
                try
                {
                    strDateTime = this.Inpatient.GetSysDateNoBar();
                }
                catch { }
                switch (this.intDateType)
                {
                    case 1:
                        strDateTime = strDateTime.Substring(2);
                        strText = strDateTime + strText.Substring(strDateTime.Length);
                        break;
                    case 2:
                        strText = strDateTime + strText.Substring(strDateTime.Length);
                        break;
                }
                if (this.strFormatHeader != "") strText = this.strFormatHeader + strText.Substring(this.strFormatHeader.Length);
            }
            catch { }
            //����   
            return strText;
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
        private void GetInfo()
        {
            try
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                //				obj=(FS.FrameWork.Models.NeuObject)this.alInpatientNos[lst.SelectedIndex];
                obj.ID = lst.Items[lst.SelectedIndex].ToString();

                string[] strArr = obj.ID.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                this.strInpatientNo = strArr[0];
                if (this.InputType != 3 && this.InputType != 4)
                {
                    //this.Text = strArr[1];
                }
                try
                {
                    this.listform.Hide();
                }
                catch
                {

                }
                try
                {
                    this.myEvent();
                }
                catch { }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); NoInfo(); }
        }
        private void NoInfo()
        {
            this.txtInputCode.Text = "";
            this.txtInputCode.Focus();
        }

        private void txtQueryInpatientNo_Load(object sender, System.EventArgs e)
        {
            //			InputType =0;	

        }


        private void lst_LostFocus(object sender, EventArgs e)
        {
            this.listform.Hide();
            if (this.strInpatientNo == "") NoInfo();
        }

        #endregion

        #region ��ѯ

        /// <summary>
        /// ����
        /// </summary>
        private ArrayList Filter(ArrayList alPatient)
        {
            if (alPatient == null || alPatient.Count == 0)
            {
                return alPatient;
            }

            ArrayList alTemp = new ArrayList();
            
            //����״̬����

            try
            {
                //���տ��ҹ���
                foreach (FS.FrameWork.Models.NeuObject obj in alPatient)
                {
                    if (this.isDeptOnly)
                    {
                        if (obj.GetType() == typeof(FS.HISFC.Models.Base.Spell))
                        {
                            FS.HISFC.Models.Base.Spell sp = obj as FS.HISFC.Models.Base.Spell;
                            //obj.User01����Ժ������Ϣ
                            if (sp.User01.Trim() == ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID ||
                                sp.UserCode.Trim() == ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID)
                            {
                                alTemp.Add(obj);
                            }
                        }
                        else
                        {
                            //obj.User01����Ժ������Ϣ
                            if (obj.User01.Trim() == ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID)
                            {
                                alTemp.Add(obj);
                            }
                        }
                    }
                    else
                    {
                        alTemp.Add(obj);
                    }
                }
                alPatient = alTemp;

                return alPatient;
            }
            catch
            {
                return alPatient;
            }
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        public void query()
        {
            this.Err = "";

            #region סԺ�Ų�
            if (this.inputtype == 0)
            {
                string patientNO = this.formatInputCode(this.Text).Trim();
                try
                {
                    this.alInpatientNos = this.Inpatient.QueryInpatientNOByPatientNO(patientNO, true);

                    if (this.alInpatientNos == null)
                    {
                        this.Err = "δ���ҵ���סԺ�ţ�";
                        return;
                    }
                    if (this.alInpatientNos.Count == 1)
                    {
                        bool b = false;
                        FS.FrameWork.Models.NeuObject obj = alInpatientNos[0] as FS.FrameWork.Models.NeuObject;
                        switch (this.myShowState)//���˻���״̬
                        {
                            case enuShowState.InHos:
                                if (obj.Memo == "I") 
                                    b = true;
                                break;
                            case enuShowState.OutHos:
                                if (obj.Memo == "B" || obj.Memo == "O" || 
                                    obj.Memo == "P" || obj.Memo == "N") 
                                    b = true;
                                break;
                            case enuShowState.BeforeArrived:
                                if (obj.Memo == "R") 
                                    b = true;
                                break;
                            case enuShowState.AfterArrived:
                                if (obj.Memo != "R") 
                                    b = true;
                                break;
                            case enuShowState.InhosBeforBalanced:
                                if (obj.Memo == "B" || obj.Memo == "I" || 
                                    obj.Memo == "P" || obj.Memo == "R") 
                                    b = true;
                                break;
                            case enuShowState.InhosAfterBalanced:
                                if (obj.Memo == "O") 
                                    b = true;
                                break;
                            case enuShowState.InBalanced:
                                if (obj.Memo == "B")
                                    b = true;
                                break;
                            default:
                                b = true;
                                break;
                        }
                        if (b)
                        {
                            this.strInpatientNo = ((FS.FrameWork.Models.NeuObject)this.alInpatientNos[0]).ID;
                        }
                        else
                        {
                            this.Err = "δ���ҵ���סԺ�ţ�";
                            this.strInpatientNo = "";
                            NoInfo();
                        }
                    }
                    else if (this.alInpatientNos.Count <= 0)
                    {
                        this.Err = "δ���ҵ���סԺ�ţ�";
                        this.strInpatientNo = "";
                        NoInfo();
                    }
                    else
                    {
                        this.strInpatientNo = ((FS.FrameWork.Models.NeuObject)this.alInpatientNos[0]).ID;
                        this.SelectPatient();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    NoInfo();
                }
                //try
                //{
                //    this.listform.Close();{3D2F489C-A1A7-483f-A66B-5D4DCA0347DC}

                //}
                //catch { }
                try
                {
                    if (this.myEvent != null)
                        this.myEvent();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
            #endregion

            #region סԺ��ˮ�Ų�

            if (this.inputtype == 1)
            {
                string temp = this.Inpatient.GetInStateByInpatientNO(this.txtInputCode.Text);
                if ("-1".Equals(temp) == false)
                {
                    this.strInpatientNo = this.txtInputCode.Text;
                }
                else
                {
                    this.Err = "δ���ҵ���סԺ��ˮ�ţ�";
                    NoInfo();
                }
                this.myEvent();
                return;
            }

            #endregion

            #region �����Ų�
            if (this.inputtype==5)
            {
                try
                {
                    this.alInpatientNos = this.Inpatient.QueryInpatientNOByBedNO(this.Text, patientInState);

                    alInpatientNos = this.Filter(alInpatientNos);
                    if (this.alInpatientNos == null)
                    {
                        this.Err = "δ���ҵ��ò����ţ�";
                        return;
                    }
                    if (this.alInpatientNos.Count == 1)
                    {
                        this.strInpatientNo = ((FS.FrameWork.Models.NeuObject)this.alInpatientNos[0]).ID;
                    }
                    else if (this.alInpatientNos.Count <= 0)
                    {
                        this.Err = "δ���ҵ��ò����ţ�";
                        this.strInpatientNo = "";
                        NoInfo();
                    }
                    else
                    {
                        this.strInpatientNo = ((FS.FrameWork.Models.NeuObject)this.alInpatientNos[0]).ID;
                        this.SelectPatient();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    NoInfo();
                }
                try
                {
                    this.listform.Close();

                }
                catch { }
                try
                {
                    if (this.myEvent != null)
                    {
                        this.myEvent();
                    }
                }
                catch { }
            }
            #endregion

            #region ������
            if (this.inputtype == 2)
            {
                try
                {
                    this.alInpatientNos = this.Inpatient.QueryInpatientNOByName(this.Text);

                    alInpatientNos = this.Filter(alInpatientNos);
                    if (this.alInpatientNos == null)
                    {
                        this.Err = "δ���ҵ��ò����ţ�";
                        return;
                    }
                    if (this.alInpatientNos.Count == 1)
                    {
                        this.strInpatientNo = ((FS.FrameWork.Models.NeuObject)this.alInpatientNos[0]).ID;
                    }
                    else if (this.alInpatientNos.Count <= 0)
                    {
                        this.Err = "δ���ҵ��ò����ţ�";
                        this.strInpatientNo = "";
                        NoInfo();
                    }
                    else
                    {
                        this.strInpatientNo = ((FS.FrameWork.Models.NeuObject)this.alInpatientNos[0]).ID;
                        this.SelectPatient();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    NoInfo();
                }
                try
                {
                    this.listform.Close();

                }
                catch { }
                try
                {
                    if (this.myEvent != null)
                        this.myEvent();
                }
                catch { }
            }
            #endregion

            #region ���������պŲ�
            if (this.inputtype == 3)
            {
                try
                {
                    this.alInpatientNos = this.Inpatient.PatientQueryByPcNoRetArray("", this.Text);
                    if (this.alInpatientNos == null)
                    {
                        this.Err = "δ���ҵ��ñ��պţ�";
                        return;
                    }
                    if (this.alInpatientNos.Count == 1)
                    {
                        bool b = false;
                        FS.FrameWork.Models.NeuObject obj = alInpatientNos[0] as FS.FrameWork.Models.NeuObject;
                        switch (this.myShowState)//���˻���״̬
                        {
                            case enuShowState.InHos:
                                if (obj.Memo == "I") b = true;
                                break;
                            case enuShowState.OutHos:
                                if (obj.Memo == "B" || obj.Memo == "O" || obj.Memo == "P" || obj.Memo == "N") b = true;
                                break;
                            case enuShowState.BeforeArrived:
                                if (obj.Memo == "R") b = true;
                                break;
                            case enuShowState.AfterArrived:
                                if (obj.Memo != "R") b = true;
                                break;
                            case enuShowState.InhosBeforBalanced:
                                if (obj.Memo == "B" || obj.Memo == "I" || obj.Memo == "P" || obj.Memo == "R") b = true;
                                break;
                            case enuShowState.InhosAfterBalanced:
                                if (obj.Memo == "O") b = true;
                                break;
                            case enuShowState.InBalanced:
                                if (obj.Memo == "B") b = true;
                                break;
                            default:
                                b = true;
                                break;
                        }
                        if (b)
                        {
                            this.strInpatientNo = ((FS.FrameWork.Models.NeuObject)this.alInpatientNos[0]).ID;
                        }
                        else
                        {
                            this.Err = "δ���ҵ���סԺ�ţ�";
                            this.strInpatientNo = "";
                            NoInfo();
                        }
                    }
                    else if (this.alInpatientNos.Count <= 0)
                    {
                        this.Err = "δ���ҵ��ñ��պţ�";
                        this.strInpatientNo = "";
                        NoInfo();
                    }
                    else
                    {
                        this.strInpatientNo = ((FS.FrameWork.Models.NeuObject)this.alInpatientNos[0]).ID;
                        this.SelectPatient();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    NoInfo();
                }
                try
                {
                    this.listform.Close();

                }
                catch { }
                try
                {
                    if (this.myEvent != null)
                        this.myEvent();
                }
                catch { }
            }
            #endregion

            #region �����ԺŲ�

            if (this.inputtype == 4)
            {
                try
                {
                    this.alInpatientNos = this.Inpatient.PatientQueryByPcNoRetArray(this.Text, "");
                    if (this.alInpatientNos == null)
                    {
                        this.Err = "δ���ҵ��õ��Ժţ�";
                        return;
                    }
                    if (this.alInpatientNos.Count == 1)
                    {
                        bool b = false;
                        FS.FrameWork.Models.NeuObject obj = alInpatientNos[0] as FS.FrameWork.Models.NeuObject;
                        switch (this.myShowState)//���˻���״̬
                        {
                            case enuShowState.InHos:
                                if (obj.Memo == "I") b = true;
                                break;
                            case enuShowState.OutHos:
                                if (obj.Memo == "B" || obj.Memo == "O" || 
                                    obj.Memo == "P" || obj.Memo == "N") 
                                    b = true;
                                break;
                            case enuShowState.BeforeArrived:
                                if (obj.Memo == "R")
                                    b = true;
                                break;
                            case enuShowState.AfterArrived:
                                if (obj.Memo != "R")
                                    b = true;
                                break;
                            case enuShowState.InhosBeforBalanced:
                                if (obj.Memo == "B" || obj.Memo == "I" || 
                                    obj.Memo == "P" || obj.Memo == "R") 
                                    b = true;
                                break;
                            case enuShowState.InhosAfterBalanced:
                                if (obj.Memo == "O") 
                                    b = true;
                                break;
                            case enuShowState.InBalanced:
                                if (obj.Memo == "B") 
                                    b = true;
                                break;
                            default:
                                b = true;
                                break;
                        }
                        if (b)
                        {
                            this.strInpatientNo = ((FS.FrameWork.Models.NeuObject)this.alInpatientNos[0]).ID;
                        }
                        else
                        {
                            this.Err = "δ���ҵ���סԺ�ţ�";
                            this.strInpatientNo = "";
                            NoInfo();
                        }
                    }
                    else if (this.alInpatientNos.Count <= 0)
                    {
                        this.Err = "δ���ҵ��õ��Ժţ�";
                        this.strInpatientNo = "";
                        NoInfo();
                    }
                    else
                    {
                        this.strInpatientNo = ((FS.FrameWork.Models.NeuObject)this.alInpatientNos[0]).ID;
                        this.SelectPatient();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    NoInfo();
                }
                try
                {
                    this.listform.Close();

                }
                catch { }
                try
                {
                    if(this.myEvent!=null)
                        this.myEvent();
                }
                catch { }
            }
            #endregion

        }
        #endregion

        private void label1_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is FS.HISFC.Components.Common.Forms.frmPatientView)
            {
                return;
            }
            FS.HISFC.Components.Common.Forms.frmPatientView frmPob = new FS.HISFC.Components.Common.Forms.frmPatientView();

            frmPob.SelectedPatientinfo += new FS.HISFC.Components.Common.Forms.frmPatientView.SelectPatientInfoDelagate(frmPob_SelectedPatientinfo);
            frmPob.ShowDialog();

            if (!string.IsNullOrEmpty(this.strInpatientNo))
            {
                if (this.myEvent != null)
                    this.myEvent();
            }

        }

        void frmPob_SelectedPatientinfo(object sender)
        {
            this.txtInputCode.Text = (sender as FS.HISFC.Models.RADT.PatientInfo).PID.PatientNO;
            this.strInpatientNo = (sender as FS.HISFC.Models.RADT.PatientInfo).ID;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum enuShowState
    {
        /// <summary>
        /// ȫ������
        /// </summary>
        All,
        /// <summary>
        /// ��Ժ���� �����-��Ժǰ
        /// </summary>
        InHos,
        /// <summary>
        /// ��Ժ�ǼǺ�
        /// </summary>
        OutHos,
        /// <summary>
        /// �����
        /// </summary>
        AfterArrived,
        /// <summary>
        /// ����ǰ
        /// </summary>
        BeforeArrived,
        /// <summary>
        /// ��Ժ�����ǰ
        /// </summary>
        InhosBeforBalanced,
        /// <summary>
        /// ��Ժ������
        /// </summary>
        InhosAfterBalanced,
        /// <summary>
        /// ������״̬
        /// </summary>
        InBalanced
    }
}
