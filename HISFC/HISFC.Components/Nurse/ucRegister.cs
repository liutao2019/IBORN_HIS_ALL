using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;

namespace FS.HISFC.Components.Nurse
{
    /// <summary>
    /// ����ע�����������
    /// </summary>
    public partial class ucRegister : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucRegister()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                this.Load += new EventHandler(ucRegister_Load);
            }
        }

        /**  ��������
         *  1�����԰���ϵͳ����÷�����Ŀ��ӡ
         *  2�����Դ�ӡ1�Ρ�1�졢����Ժע����
         *  3�����԰���������������ȫ�� ���ô�ӡ����
         *  4��
         * 
         * */

        #region ����

        #region ҵ������

        /// <summary>
        /// ������ù�����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// Ժע������
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Inject InjMgr = new FS.HISFC.BizLogic.Nurse.Inject();

        /// <summary>
        /// ҩƷ����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// �ҺŹ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// �ۺ�ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager inteMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ҽ������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Order orderMgr = new FS.HISFC.BizProcess.Integrate.Order();

        #endregion

        /// <summary>
        /// ��ǰ����
        /// </summary>
        private FS.HISFC.Models.Registration.Register myRegInfo = null;

        /// <summary>
        /// ���Ƶ�������
        /// </summary>
        private ArrayList alPrint = null;


        /// ���ע��˳���
        /// </summary>
        private int maxInjectOrder = 0;

        /// <summary>
        /// ע�䵥������
        /// </summary>
        private ArrayList alInject = null;

        /// <summary>
        /// ҽ����Ա������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper doctHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// Ƶ�ΰ�����
        /// </summary>
        FS.FrameWork.Public.ObjectHelper freqHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��ſ�����Ϣ
        /// </summary>
        FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        Hashtable hsInfos = new Hashtable();

        /// <summary>
        /// ���ע��˳���
        /// </summary>
        //private int maxInjectOrder = 0;

        /// <summary>
        /// ��ȡע��� �ӿ�ģʽ
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Nurse.IGetInjectOrderNo IGetOrderNo = null;


        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// �����ļ�
        /// </summary>
        private string injectRegisterXml = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @".\\Profile\\injectRegister.xml";

        FS.HISFC.Models.Pharmacy.Item drug = null;

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ���ʾ���ߵ���ɵǼǵ�ȫ������
        /// </summary>
        private bool isShowAllInject = false;

        /// <summary>
        /// �Ƿ���ʾ���ߵ���ɵǼǵ�ȫ������
        /// </summary>
        [Description("�Ƿ���ʾ���ߵ���ɵǼǵ�ȫ������"), Category("����"), DefaultValue("false")]
        public bool IsShowAllInject
        {
            get
            {
                return isShowAllInject;
            }
            set
            {
                isShowAllInject = value;
            }
        }

        /// <summary>
        /// ��ӡ��Ѳ�ӿ��ϵ��÷�
        /// </summary>
        private string usage = "iv.drip(�ţ�;";

        /// <summary>
        /// �÷��Ƿ��ӡ��Ѳ�ӿ���
        /// {EE46827D-D081-4aa5-8653-1EF9D176A5DC}
        /// </summary>
        [Description("��ӡ��Ѳ�ӿ����÷�(����ע���÷�����Һ�÷�)"), Category("����")]
        public string Usage
        {
            get
            {
                return this.usage;
            }
            set
            {
                this.usage = value;
            }
        }

        /// <summary>
        /// �Ƿ���special��������Ժע,���������½����ù���
        /// </summary>
        private bool isUseSpecialConstant = true;

        /// <summary>
        /// �Ƿ���special��������Ժע,���������½����ù���
        /// </summary>
        [Description("�Ƿ���special��������Ժע,���������½����ù���"), Category("����")]
        public bool IsUseSpecialConstant
        {
            get
            {
                return this.isUseSpecialConstant;
            }
            set
            {
                this.isUseSpecialConstant = value;
            }
        }

        /// <summary>
        /// ע���÷�����������Һ��Ŀ��
        /// </summary>
        private string injectUsage = "";

        /// <summary>
        /// ע���÷�����������Һ��Ŀ��
        /// </summary>
        [Description("ע���÷�ά������������Һ�÷���"), Category("����")]
        public string InjectUsage
        {
            get
            {
                return this.injectUsage;
            }
            set
            {
                this.injectUsage = value;
            }
        }

        /// <summary>
        /// �Ƿ�һ�δ�ӡ����Ժע����,������ÿ�δ�ӡһ��
        /// </summary>
        private bool isSaveAllInjectCount = true;

        /// <summary>
        /// �Ƿ�һ�δ�ӡ����Ժע����,������ÿ�δ�ӡһ��
        /// </summary>
        [Description("�Ƿ�һ�δ�ӡ����Ժע����,������ÿ�δ�ӡһ��"), Category("����")]
        public bool IsSaveAllInjectCount
        {
            get
            {
                return isSaveAllInjectCount;
            }
            set
            {
                isSaveAllInjectCount = value;
            }
        }

        private bool isSaveDayInjectCount = false;
        [Description("�Ƿ�һ�δ�ӡ����Ժע����,������ÿ�δ�ӡһ��"), Category("����")]
        public bool IsSaveDayInjectCount
        {
            get
            {
                return isSaveDayInjectCount;
            }
            set
            {
                isSaveDayInjectCount = value;
            }
        }

        /// <summary>
        /// �Ƿ��Զ���ӡѲ�ӿ�
        /// </summary>
        private bool isAutoPrint = true;

        /// <summary>
        /// �Ƿ��Զ���ӡѲ�ӿ�
        /// </summary>
        [Description("�Ƿ��Զ���ӡѲ�ӿ�"), Category("����")]
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

        /// <summary>
        /// �Ƿ��Զ���������
        /// </summary>
        private bool isAutoSave = true;

        /// <summary>
        /// �Ƿ��Զ���������
        /// </summary>
        [Description("�Ƿ��Զ���������"), Category("����")]
        public bool IsAutoSave
        {
            get
            {
                return isAutoSave;
            }
            set
            {
                this.isAutoSave = value;
            }
        }

        /// <summary>
        /// �Ƿ�ʹ��ע��˳���
        /// </summary>
        private bool isUserOrderNumber = false;

        /// <summary>
        /// �Ƿ�ʹ��ע��˳���
        /// </summary>
        [Description("�Ƿ�ʹ��ע��˳���"), Category("����")]
        public bool IsUserOrderNumber
        {
            get
            {
                return isUserOrderNumber;
            }
            set
            {
                this.isUserOrderNumber = value;
            }
        }

        /// <summary>
        /// �Զ���ӡʱ�����Ƿ���ʾ
        /// </summary>
        private bool isMessageOnSave = true;

        /// <summary>
        /// �Զ���ӡʱ�����Ƿ���ʾ
        /// </summary>
        [Description("�Զ���ӡʱ�����Ƿ���ʾ"), Category("����")]
        public bool IsMessageOnSave
        {
            get
            {
                return isMessageOnSave;
            }
            set
            {
                this.isMessageOnSave = value;
            }
        }

        /// <summary>
        /// ��ѯ�ĹҺŵ����ڼ��
        /// </summary>
        private int queryRegDays = 2;

        /// <summary>
        /// ��ѯ�ĹҺŵ����ڼ��
        /// </summary>
        [Category("��ѯ����"), Description("��ѯ�Һ���Ϣ�����ڼ�����ӽ����ϵĿ�ʼʱ����ǰ��ѯ���죩")]
        public int QueryRegDays
        {
            get
            {
                return queryRegDays;
            }
            set
            {
                queryRegDays = value;
            }
        }

        /// <summary>
        /// ��ʼʱ������ڼ��(�����ļ������)
        /// </summary>
        private int beginDateIntervalDays = 0;

        /// <summary>
        /// ��ʼʱ������ڼ��(�����ļ������)
        /// </summary>
        [Category("��ѯ����"), Description("��ʼʱ������ڼ��(�����ļ������)")]
        public int BeginDateIntervalDays
        {
            get
            {
                return beginDateIntervalDays;
            }
            set
            {
                beginDateIntervalDays = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ����
        /// </summary>
        private bool isShowSubjob = false;

        /// <summary>
        /// �Ƿ���ʾ����
        /// </summary>
        [Description("�Ƿ���ʾ����"), Category("����")]
        public bool IsShowSubjob
        {
            get
            {
                return this.isShowSubjob;
            }
            set
            {
                this.isShowSubjob = value;
            }
        }
        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucRegister_Load(object sender, EventArgs e)
        {
            this.dtpStart.Value = this.InjMgr.GetDateTimeFromSysDateTime().Date.AddDays(0 - this.beginDateIntervalDays);
            this.dtpEnd.Value = this.InjMgr.GetDateTimeFromSysDateTime().Date.AddDays(1).AddSeconds(-1);

            this.InitData();
            this.SetFP();

            this.InitOrder();
            //ƿǩ����
            this.ucCureReprint1.Init();

            this.Clear();
        }

        /// <summary>
        /// ��ʼ��ҽ��
        /// </summary>
        private void InitData()
        {
            ArrayList al = this.inteMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);

            if (al == null)
            {
                MessageBox.Show("��ѯҽ���б����\r\n" + inteMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            doctHelper.ArrayObject = al;

            //Ƶ��
            ArrayList alFrequency = this.inteMgr.QuereyFrequencyList();
            if (al == null)
            {
                MessageBox.Show("��ѯƵ���б����\r\n" + inteMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            freqHelper.ArrayObject = alFrequency;

            al = new ArrayList();
            al = this.inteMgr.GetDepartment();
            if (al == null)
            {
                MessageBox.Show("��ѯ�����б����\r\n" + inteMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.deptHelper.ArrayObject = al;
        }

        /// <summary>
        /// ���ø�ʽ
        /// </summary>
        private void SetFP()
        {
            FarPoint.Win.Spread.CellType.TextCellType txtOnly = new FarPoint.Win.Spread.CellType.TextCellType();
            txtOnly.ReadOnly = true;

            FarPoint.Win.Spread.CellType.NumberCellType numType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numType.DecimalPlaces = 0;

            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.Ժע����].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.ȷ�ϴ���].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.����ҽ��].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.�Ʊ�].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.ҽ��].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.���].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.ÿ������].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.Ƶ��].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.�÷�].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.Ƥ��].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.ע��ʱ��].CellType = txtOnly;


            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.���].CellType = numType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.Ժע����].CellType = numType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.ȷ�ϴ���].CellType = numType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.ע�����].CellType = numType;

            if (System.IO.File.Exists(injectRegisterXml))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuSpread1_Sheet1, injectRegisterXml);
            }
            else
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuSpread1_Sheet1, injectRegisterXml);
            }

            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
        }

        /// <summary>
        /// ��ʼ��ע��˳���
        /// </summary>
        private void InitOrder()
        {
            //��ȡ�Ƿ��Զ�����ע��˳��
            try
            {
                //��Ҫ����
                //�޸�Ϊ���½�����

                bool isAutoInjectOrder = false;

                //���ﻤʿ���Ƿ�����ע��˳��
                isAutoInjectOrder = FS.FrameWork.Function.NConvert.ToBoolean(this.inteMgr.QueryControlerInfo("900005"));
                if (isAutoInjectOrder)
                {
                    this.chkIsOrder.Checked = true;
                    this.SetOrder();
                    this.lbLastOrder.Text = "�������һ��ע���:" +
                        (FS.FrameWork.Function.NConvert.ToInt32(this.txtOrder.Text.Trim()) - 1).ToString();
                }
                else
                {
                    this.chkIsOrder.Checked = false;
                    this.lbLastOrder.Text = "�������Զ�����ע��˳���!";
                    this.txtOrder.Text = "0";
                }
            }
            catch //�������ļ�
            {
                this.chkIsOrder.Checked = false;
                this.lbLastOrder.Text = "�������Զ�����ע��˳���!";
                this.txtOrder.Text = "0";
            }


        }

        #region ������

        /// <summary>
        /// ���幤����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("ȫѡ", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȫѡ, true, false, null);
            this.toolBarService.AddToolButton("ȡ��ȫѡ", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            this.toolBarService.AddToolButton("��ӡƿǩ", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton("��ӡǩ����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton("��ӡע�䵥", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton("��ӡѲ�ӵ�", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton("��ӡ���߿�", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton("��ӡ������", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton("�޸�Ƥ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            this.toolBarService.AddToolButton("��ӡLIS����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// �˵��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "ȫѡ":
                    this.SelectAll(true);
                    break;
                case "ȡ��ȫѡ":
                    this.SelectAll(false);
                    break;
                case "��ӡƿǩ":
                    this.PrintCure();
                    break;
                case "��ӡǩ����":
                    this.PrintItinerate();
                    break;
                case "��ӡע�䵥":
                    this.PrintInject();
                    break;
                case "��ӡѲ�ӵ�":
                    this.PrintInjectScoutCard();
                    break;
                case "��ӡ���߿�":
                    this.PrintPatient();
                    break;
                case "��ӡ������":
                    this.PrintNumber();
                    break;
                case "�޸�Ƥ��":
                    this.ModifyHytest();
                    break;
                case "��ӡLIS����":
                    this.PrintLisBarCode();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #endregion

        #region ����

        /// <summary>
        /// ������ɫ(��������ʾ���һ��clinicҽ��)
        /// </summary>
        /// <returns></returns>
        private int ShowColor()
        {
            //ȡ�����clinic_code
            int maxClinic = 0;
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                return -1;
            }
            FS.HISFC.Models.Fee.Outpatient.FeeItemList item = null;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                item = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                if (FS.FrameWork.Function.NConvert.ToInt32(item.ID) > maxClinic)
                {
                    maxClinic = FS.FrameWork.Function.NConvert.ToInt32(item.ID);
                }
            }

            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                item = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                if (item.ID == maxClinic.ToString())
                {
                    this.neuSpread1_Sheet1.Rows[i].ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    this.neuSpread1_Sheet1.SetValue(i, 0, false);
                }
            }
            return 0;
        }

        /// <summary>
        /// ��õ�ǰ���������ע��˳��
        /// </summary>
        /// <returns></returns>
        private int GetMaxInjectOrder()
        {
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                return 0;
            }

            this.neuSpread1.StopCellEditing();

            maxInjectOrder = 0;

            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetText(i, (int)EnumColSet.ѡ��).ToUpper() == "FALSE" ||
                    this.neuSpread1_Sheet1.GetText(i, (int)EnumColSet.ѡ��) == "")
                {
                    continue;
                }
                if (FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.���].Text) > maxInjectOrder)
                {
                    maxInjectOrder = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.���].Text);
                }
            }
            return maxInjectOrder;
        }

        /// <summary>
        /// ���ò�����Ϣ
        /// </summary>
        /// <param name="reg"></param>
        private void SetPatient(FS.HISFC.Models.Registration.Register reg)
        {
            if (reg == null || reg.ID == "")
            {
                return;
            }
            else
            {
                this.txtName.Text = reg.Name;
                this.txtSex.Text = reg.Sex.Name;
                this.txtBirthday.Text = reg.Birthday.ToString("yyyy-MM-dd");
                this.txtAge.Text = this.InjMgr.GetAge(reg.Birthday);
                this.txtCardNo.Text = reg.PID.CardNO;

                #region ���

                ArrayList alDiagnoses = new ArrayList();
                alDiagnoses = (new FS.HISFC.BizLogic.HealthRecord.Diagnose()).QueryDiagnoseNoOps(reg.ID);
                this.txtDiagnoses.Text = "";

                if (alDiagnoses != null)
                {
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose dg in alDiagnoses)
                    {
                        if (dg.Memo == reg.ID || !dg.IsValid)
                        {
                            //�ѷǱ��ιҺŵ���Ϲ��˵�������Ժ���Ϻܶ࣬ͨ�����ַ�ʽ�����Ļ���Ҫ��дһ��ҵ���
                            continue;
                        }
                        else
                        {
                            //�ѱ��ιҺŵ�������������ŵ�lblDiagnose��
                            this.txtDiagnoses.Text += dg.DiagInfo.ICD10.Name + " ";
                        }
                    }
                }
                else
                {
                    this.txtDiagnoses.Text = "";
                }
                #endregion
            }
        }

        /// <summary>
        /// ȫѡ
        /// </summary>
        private void SelectAll(bool isSelected)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (!this.neuSpread1_Sheet1.Rows[i].Locked)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.ѡ��].Value = isSelected;
                }
            }
        }

        #endregion

        #region  ��ӡ

        /// <summary>
        /// ��ӡ���߿�
        /// </summary>
        private void PrintPatient()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint patientPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint;
            if (patientPrint == null)
            {
                patientPrint = new Nurse.Print.ucPrintPatient() as FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint;
            }
            patientPrint.Init(al);
        }

        /// <summary>
        /// ��ӡƿǩ
        /// </summary>
        private void PrintCure()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint curePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint;
            if (curePrint == null)
            {
                curePrint = new Nurse.Print.ucPrintCure() as FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint;
            }
            curePrint.Init(al);
        }

        /// <summary>
        /// ��ӡע�䵥.
        /// </summary>
        private void PrintInject()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint injectPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint;
            if (injectPrint == null)
            {
                injectPrint = new Nurse.Print.ucPrintInject() as FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint;
            }
            injectPrint.Init(al);
        }

        /// <summary>
        /// ��ӡǩ���� ���Ŵ�ӡ��
        /// </summary>
        private void PrintItinerate()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }

            FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            if (itineratePrint == null)
            {
                itineratePrint = new Nurse.Print.ucPrintItinerate() as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            }
            itineratePrint.IsReprint = false;
            itineratePrint.Init(al);
        }

        /// <summary>
        /// ��ӡǩ����  ����
        /// </summary>
        private void PrintItinerateLarge()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            if (itineratePrint == null)
            {
                itineratePrint = new Nurse.Print.ucPrintItinerateLarge() as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            }
            itineratePrint.IsReprint = false;
            itineratePrint.Init(al);
        }

        /// <summary>
        /// ��ӡ������ҺѲ�ӿ�
        /// </summary>
        private void PrintInjectScoutCard()
        {
            int intReturn = this.GetAllPrintInjectList();
            if (intReturn == -1)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            foreach (ArrayList al in hsInfos.Values)
            {
                FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
                if (itineratePrint == null)
                {
                    return;
                }
                //itineratePrint.Init(al);
                if (string.IsNullOrEmpty(injectUsage))
                {
                    itineratePrint.IsReprint = false;
                    itineratePrint.Init(al);
                }
                else
                {
                    ArrayList alZS = new ArrayList();
                    ArrayList alSY = new ArrayList();
                    for (int i = 0; i < al.Count; i++)
                    {
                        FS.HISFC.Models.Nurse.Inject info = al[i] as FS.HISFC.Models.Nurse.Inject;
                        if (injectUsage.Contains(info.Item.Order.Usage.Name.ToString()))
                        {
                            alZS.Add(info);
                        }
                        else
                        {
                            alSY.Add(info);
                        }
                    }
                    if (alZS.Count > 0)
                    {
                        itineratePrint.IsReprint = false;
                        itineratePrint.Init(alZS);
                    }
                    if (alSY.Count > 0)
                    {
                        itineratePrint.IsReprint = false;
                        itineratePrint.Init(alSY);
                    }
                }
            }

        }

        /// <summary>
        /// ��ӡע�������
        /// </summary>
        private void PrintNumber()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count == 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint numberPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint;
            if (numberPrint == null)
            {
                numberPrint = new Nurse.Print.ucPrintNumber() as FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint;
            }
            numberPrint.Init(al);
        }

        /// <summary>
        /// ��ӡLis����
        /// </summary>
        private void PrintLisBarCode()
        {
            //if (myRegInfo == null)
            //{
            //    MessageBox.Show("û�л�����Ϣ");
            //    return;
            //}
            FS.HISFC.BizProcess.Interface.Registration.IPrintBar lisBarPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Registration.IPrintBar)) as FS.HISFC.BizProcess.Interface.Registration.IPrintBar;
            if (lisBarPrint == null)
            {
                return;
            }
            string err = "";
            lisBarPrint.printBar(this.myRegInfo, ref err);
        }


        /// <summary>
        /// ��ȡҪ��ӡ������
        /// </summary>
        /// <returns></returns>
        private ArrayList GetPrintInjectList()
        {
            ArrayList al = new ArrayList();
            ArrayList alJiePing = new ArrayList();
            this.neuSpread1.StopCellEditing();

            FS.HISFC.Models.Fee.Outpatient.FeeItemList detail = null;
            FS.HISFC.Models.Order.OutPatient.Order orderinfo = null;
            FS.HISFC.Models.Nurse.Inject info = null;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE")
                {
                    continue;
                }

                detail = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                orderinfo = (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.Ƥ��].Tag;
                info = new FS.HISFC.Models.Nurse.Inject();

                info.Patient = myRegInfo;
                //info.Patient.Name = reg.Name;
                //info.Patient.Sex.ID = reg.Sex.ID;
                //info.Patient.Birthday = reg.Birthday;

                info.Item = detail;
                info.Item.InjectCount = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.Ժע����].Text);
                info.OrderNO = this.txtOrder.Text.ToString();
                info.Item.Order.Combo.ID = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.���].Tag.ToString();

                info.Item.Order.ReciptDoctor.Name = doctHelper.GetName(detail.RecipeOper.ID);
                info.Item.Order.ReciptDoctor.ID = detail.RecipeOper.ID;
                info.Item.Name = detail.Item.Name;
                string strOrder = "";
                if (this.neuSpread1_Sheet1.GetValue(i, 1) == null || this.neuSpread1_Sheet1.GetValue(i, 1).ToString() == "")
                {
                    strOrder = "";
                }
                else
                {
                    strOrder = this.neuSpread1_Sheet1.GetValue(i, 1).ToString();
                }
                info.InjectOrder = strOrder;


                if (orderinfo != null)
                {
                    //��עӦ����Memo+Ƥ��
                    info.Memo = orderinfo.Memo;
                    info.Hypotest = orderinfo.HypoTest;
                }
                info.PrintNo = detail.User02;

                al.Add(info);

                #region ��ƿ����û��
                ////�жϽ�ƿ,���������ӵ�alJiePing��
                //if (orderinfo.ExtendFlag1 == null || orderinfo.ExtendFlag1.Length < 1)
                //    orderinfo.ExtendFlag1 = "1|";
                ////				string[] str = orderinfo.Mark1.Split('|');
                //int inum = FS.FrameWork.Function.NConvert.ToInt32(orderinfo.ExtendFlag1.Substring(0, 1));
                //info.Memo = inum.ToString();
                ////FS.HISFC.Components.Function.NConvert.ToInt32(str[0]);
                ////				if(inum > 1)
                ////				{
                ////						FS.HISFC.Models.Nurse.Inject inj = new FS.HISFC.Models.Nurse.Inject();
                ////						inj = info.Clone();
                ////						inj.InjectOrder = (this.GetMaxInjectOrder() + 1).ToString();
                ////						maxInjectOrder++;
                ////						alJiePing.Add(inj);
                ////					}
                ////				}
                #endregion

                //{EB016FFE-0980-479c-879E-225462ECA6D0}

            }
            return al;
        }

        /// <summary>
        /// ��ȡҪ��ӡ�����ݣ���ά���÷���
        /// </summary>
        /// <returns></returns>
        private int GetAllPrintInjectList()
        {
            this.SelectAll(true);
            this.neuSpread1.StopCellEditing();
            hsInfos.Clear();
            FS.HISFC.Models.Fee.Outpatient.FeeItemList detail = null;
            FS.HISFC.Models.Order.OutPatient.Order orderinfo = null;
            FS.HISFC.Models.Nurse.Inject info = null;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE")
                    continue;
                detail = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                orderinfo = (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.Ƥ��].Tag;
                info = new FS.HISFC.Models.Nurse.Inject();
                if (!usage.Contains(detail.Order.Usage.Name))
                {
                    continue;
                }

                info.Patient.ID = detail.Patient.ID;
                info.Patient.Name = myRegInfo.Name;
                info.Patient.Sex.ID = myRegInfo.Sex.ID;
                info.Patient.Birthday = myRegInfo.Birthday;
                info.Patient.Card.ID = this.txtCardNo.Text.Trim().PadLeft(10, '0');

                info.Item = detail;
                info.Item.InjectCount = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.Ժע����].Text);
                info.OrderNO = this.txtOrder.Text.ToString();
                info.Item.Order.Combo.ID = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.���].Tag.ToString();

                info.Item.Order.ReciptDoctor.Name = doctHelper.GetName(detail.RecipeOper.ID);
                info.Item.Order.ReciptDoctor.ID = detail.RecipeOper.ID;

                info.Item.Name = detail.Item.Name;
                string strOrder = "";
                if (this.neuSpread1_Sheet1.GetValue(i, 1) == null || this.neuSpread1_Sheet1.GetValue(i, 1).ToString() == "")
                {
                    strOrder = "";
                }
                else
                {
                    strOrder = this.neuSpread1_Sheet1.GetValue(i, 1).ToString();
                }
                info.Item.Days = detail.Days;
                //���ߵ���ע�䴦��ʱ��
                info.InjectOrder = strOrder;

                info.User03 = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.ע��ʱ��].Text;

                string hypoTest = string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.Ƥ��].Text) ? "" : "(" + this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.Ƥ��].Text + ")";

                if (orderinfo != null)
                {
                    //��עӦ����Memo+Ƥ��
                    //info.Memo = orderinfo.ExtendFlag1;
                    info.Memo = orderinfo.Memo;
                    info.Hypotest = orderinfo.HypoTest;
                }

                if (!hsInfos.ContainsKey(info.Item.Order.ReciptDoctor.ID))
                {
                    ArrayList al = new ArrayList();
                    al.Add(info);
                    hsInfos.Add(info.Item.Order.ReciptDoctor.ID, al);
                }
                else
                {
                    ((ArrayList)hsInfos[info.Item.Order.ReciptDoctor.ID]).Add(info);
                }
            }
            return 1;
        }

        /// <summary>
        /// ��ȡ�����ȵ�ʹ�÷���
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Base.Const GetFirstUsage()
        {
            FS.HISFC.Models.Fee.Outpatient.FeeItemList info = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
            if (this.neuSpread1_Sheet1.RowCount <= 0) return new FS.HISFC.Models.Base.Const();

            int FirstCodeNum = 10000;
            FS.HISFC.Models.Base.Const retobj = new FS.HISFC.Models.Base.Const();
            try
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    info = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                    FS.FrameWork.Models.NeuObject obj = this.inteMgr.GetConstant("SPECIAL", info.Order.Usage.ID);
                    FS.HISFC.Models.Base.Const conobj = (FS.HISFC.Models.Base.Const)obj;

                    if (conobj.SortID < FirstCodeNum)
                    {
                        FirstCodeNum = conobj.SortID;
                        retobj = conobj;
                    }
                }
            }
            catch
            {
                return retobj;
            }

            return retobj;
        }

        #endregion

        #region ע��˳��ŵĴ���

        /// <summary>
        /// ����Ĭ��ע��˳��
        /// </summary>
        private void SetInject()
        {
            #region  û�����ݾͲ�����,ֱ�ӷ���
            if (this.neuSpread1_Sheet1.RowCount <= 0) return;
            #endregion

            #region ���û��߽����ע��˳���
            if (this.chkIsOrder.Checked)
            {
                this.SetOrder();
            }
            else
            {
                this.txtOrder.Text = "0";
                //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B}
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.ע�����].Text = this.txtOrder.Text;
                }
            }
            #endregion

            #region ����ÿ����Ŀ��ע��˳��
            int InjectOrder = 1;
            this.neuSpread1_Sheet1.SetValue(0, 1, 1, false);
            for (int i = 1; i < this.neuSpread1_Sheet1.RowCount; i++)
            {

                if (this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.���].Text == null || this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.���].Text.Trim() == "")
                {
                    InjectOrder++;
                    this.neuSpread1_Sheet1.SetValue(i, 1, InjectOrder, false);
                }
                else if (this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.���].Text != null && this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.���].Text.Trim() != ""
                    //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
                    && this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.���].Tag.ToString() + this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.ע��ʱ��].Text == this.neuSpread1_Sheet1.Cells[i - 1, (int)EnumColSet.���].Tag.ToString() + this.neuSpread1_Sheet1.Cells[i - 1, (int)EnumColSet.ע��ʱ��].Text)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 1, InjectOrder, false);
                }
                else
                {
                    InjectOrder++;
                    this.neuSpread1_Sheet1.SetValue(i, 1, InjectOrder, false);
                }
            }
            #endregion
        }

        /// <summary>
        /// ����ע���
        /// </summary>
        private void SetOrder()
        {
            if (!this.chkIsOrder.Checked)
            {
                this.txtOrder.Text = "0";
                this.lbLastOrder.Text = "���ڱ���û�������Զ��������!";
                return;
            }
            //����Զ�����,���õ�һ�����,����ֵthis.currentOrder
            //��Ϊͨ���ӿ�ʵ�֣����û��������ԭ�����������

            this.CreateInterface();

            if (IGetOrderNo != null)
            {
                string orderNo = IGetOrderNo.GetOrderNo(this.myRegInfo);

                this.txtOrder.Text = orderNo;
                if (this.neuSpread1_Sheet1.Rows.Count == 0)
                {
                    return;
                }

                string comboAndInjectTime = this.neuSpread1_Sheet1.Cells[0, (int)EnumColSet.���].Tag.ToString() + this.neuSpread1_Sheet1.Cells[0, (int)EnumColSet.ע��ʱ��].Text;

                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    string rowComboAndInjectTime = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.���].Tag.ToString() + this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.ע��ʱ��].Text;
                    if (comboAndInjectTime != rowComboAndInjectTime)
                    {
                        comboAndInjectTime = rowComboAndInjectTime;
                        orderNo = IGetOrderNo.GetSamePatientNextOrderNo(orderNo);
                    }
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.ע�����].Text = orderNo;
                }
                return;
            }
            else
            {
                FS.HISFC.Models.Nurse.Inject info = this.InjMgr.QueryLast();
                if (info != null && info.Booker.OperTime != System.DateTime.MinValue)
                {
                    if (info.Booker.OperTime.Date == this.InjMgr.GetDateTimeFromSysDateTime().Date)
                    {
                        this.txtOrder.Text = (FS.FrameWork.Function.NConvert.ToInt32(info.OrderNO) + 1).ToString();
                    }
                    else
                    {
                        this.txtOrder.Text = "1";
                    }
                }
                else
                {
                    this.txtOrder.Text = "1";
                }

                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.ע�����].Text = this.txtOrder.Text;
                }
            }
        }

        /// <summary>
        /// �����ӿ�
        /// </summary>
        private void CreateInterface()
        {
            if (this.IGetOrderNo == null)
            {
                this.IGetOrderNo = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IGetInjectOrderNo)) as FS.HISFC.BizProcess.Interface.Nurse.IGetInjectOrderNo;
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            return this.Save();
        }

        /// <summary>
        /// ȷ�ϱ���( 1.met_nuo_inject�����¼  2.fin_ipb_feeitemlist������ȷ��Ժע������ȷ�ϱ�־)
        /// </summary>
        private int Save()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("û��Ҫ���������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }

            this.neuSpread1.StopCellEditing();

            int selectNum = 0;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE"
                    || this.neuSpread1_Sheet1.GetValue(i, 0).ToString() == "")
                {
                    selectNum++;
                }
            }
            if (selectNum >= this.neuSpread1_Sheet1.RowCount)
            {
                MessageBox.Show("��ѡ��Ҫ���������", "��ʾ");
                return -1;
            }

            alInject = new ArrayList();
            alPrint = new ArrayList();

            if (this.isUserOrderNumber)
            {
                #region �ж�������кŵ���Ч��
                if (this.txtOrder.Text == null || this.txtOrder.Text.Trim().ToString() == "")
                {
                    MessageBox.Show("û���������˳���!");
                    this.txtOrder.Focus();
                    return -1;
                }
                else if (this.InjMgr.QueryInjectOrder(this.txtOrder.Text.Trim().ToString()).Count > 0)
                {
                    if (MessageBox.Show("�����к��Ѿ�ʹ��,�Ƿ����!", "��ʾ", System.Windows.Forms.MessageBoxButtons.YesNo,
                        System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        this.txtOrder.Focus();
                        return -1;
                    }
                }
                #endregion


                #region ���ע��˳��ŵ���Ч�ԣ������ͬ�ģ�ע��˳���Ҳ������ͬ��
                for (int i = 1; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    if (this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.���].Tag != null
                        && this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.���].Tag.ToString() != "" &&
                        //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
                        this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.���].Tag.ToString() + this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.ע��ʱ��].Text == this.neuSpread1_Sheet1.Cells[i - 1, (int)EnumColSet.���].Tag.ToString() + this.neuSpread1_Sheet1.Cells[i - 1, (int)EnumColSet.ע��ʱ��].Text
                        && this.neuSpread1_Sheet1.GetValue(i, 1).ToString() != this.neuSpread1_Sheet1.GetValue(i - 1, 1).ToString()
                        )
                    {
                        MessageBox.Show("��ͬ��ŵ�ע��˳��ű�����ͬ!", "��" + (i + 1).ToString() + "��");
                        return -1;
                    }
                }
                #endregion
            }

            #region ���Ժע��������Ч�ԣ������ͬ�ģ�ע��˳���Ҳ������ͬ��
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                string strnum = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.Ժע����].Text;
                if (strnum == null || strnum == "")
                {
                    MessageBox.Show("Ժע��������Ϊ��!", "��" + (i + 1).ToString() + "��");
                    return -1;
                }
                string completenum = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.ȷ�ϴ���].Text;
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "TRUE")
                {
                    if (FS.FrameWork.Function.NConvert.ToInt32(strnum) == 0)
                    {
                        continue;
                    }

                    if (FS.FrameWork.Function.NConvert.ToInt32(strnum) <= FS.FrameWork.Function.NConvert.ToInt32(completenum))
                    {
                        MessageBox.Show("Ժע��������!", "��" + (i + 1).ToString() + "��");
                        return -1;
                    }
                }
                //if (this.fpSpread1_Sheet1.Cells[i, (int)EnumColSet.���].Tag != null && this.fpSpread1_Sheet1.Cells[i, (int)EnumColSet.���].Tag.ToString() != "" &&
                //    this.fpSpread1_Sheet1.Cells[i, (int)EnumColSet.���].Tag.ToString() == this.fpSpread1_Sheet1.Cells[i - 1, (int)EnumColSet.���].Tag.ToString()
                //    && this.fpSpread1_Sheet1.GetValue(i, 2).ToString() != this.fpSpread1_Sheet1.GetValue(i - 1, 2).ToString()
                //    )
                //{
                //    MessageBox.Show("��ͬ��ŵ�Ժע����������ͬ!", "��" + (i + 1).ToString() + "��");
                //    return -1;
                //}
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            try
            {
                this.InjMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.phaIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.inteMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.inteMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                DateTime confirmDate = this.InjMgr.GetDateTimeFromSysDateTime();

                FS.HISFC.Models.Fee.Outpatient.FeeItemList detail = null;
                FS.HISFC.Models.Nurse.Inject info = null;

                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    if (this.neuSpread1_Sheet1.GetText(i, 0).ToUpper() == "FALSE" ||
                        this.neuSpread1_Sheet1.GetText(i, 0) == "")
                    {
                        continue;
                    }
                    detail = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;

                    //					#region �ж��Ƿ���Ҫ��ӡע�䵥
                    //					if(detail.ConfirmedInject == 0)
                    //					{
                    //						IsFirstTime = true;
                    //						countInject = detail.InjectCount;
                    //					}
                    //					#endregion

                    info = new FS.HISFC.Models.Nurse.Inject();

                    #region ʵ��ת����������Ŀ�շ���ϸʵ��FeeItemList��->ע��ʵ��Inject��

                    info.Patient = myRegInfo;
                    info.Patient.ID = detail.Patient.ID;
                    info.Patient.Name = myRegInfo.Name;
                    info.Patient.Sex.ID = myRegInfo.Sex.ID;
                    info.Patient.Birthday = myRegInfo.Birthday;
                    info.Patient.PID.CardNO = myRegInfo.PID.CardNO;

                    info.Item = detail;
                    info.Item.ID = detail.Item.ID;
                    info.Item.Name = detail.Item.Name;

                    info.Item.InjectCount = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.Ժע����].Text);

                    info.Item.Order.DoctorDept.Name = deptHelper.GetName(detail.RecipeOper.Dept.ID);
                    info.Item.Order.DoctorDept.ID = detail.RecipeOper.Dept.ID;

                    info.Item.Order.ReciptDoctor.Name = doctHelper.GetName(detail.RecipeOper.ID);
                    info.Item.Order.ReciptDoctor.ID = detail.RecipeOper.ID;
                    //�Ƿ�Ƥ��
                    if (this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.Ƥ��].Tag.ToString().ToUpper() == "TRUE")
                    {
                        info.Hypotest = FS.HISFC.Models.Order.EnumHypoTest.NeedHypoTest;
                    }
                    else
                    {
                        info.Hypotest = FS.HISFC.Models.Order.EnumHypoTest.FreeHypoTest;
                    }
                    #endregion

                    info.ID = this.InjMgr.GetSequence("Nurse.Inject.GetSeq");

                    info.OrderNO = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.ע�����].Text;
                    //{24A47206-F111-4817-A7B4-353C21FC7724}
                    info.PrintNo = detail.User02;
                    info.Item.Order.Combo.ID = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.���].Tag.ToString();
                    info.Booker.ID = FS.FrameWork.Management.Connection.Operator.ID;
                    info.Booker.OperTime = confirmDate;
                    info.Item.ExecOper.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                    string strOrder = "";
                    if (this.neuSpread1_Sheet1.GetValue(i, 1) == null || this.neuSpread1_Sheet1.GetValue(i, 1).ToString() == "")
                    {
                        strOrder = "";
                    }
                    else
                    {
                        strOrder = this.neuSpread1_Sheet1.GetValue(i, 1).ToString();
                    }
                    info.InjectOrder = strOrder;
                    info.Item.Days = detail.Days;
                    string hypoTest = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.Ƥ��].Text;

                    //��ע--(ȡҽ����ע)
                    FS.HISFC.Models.Order.OutPatient.Order orderinfo =
                        (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.Ƥ��].Tag;
                    if (orderinfo != null)
                    {
                        //��עӦ����Memo+Ƥ��
                        //info.Memo = orderinfo.ExtendFlag1;
                        info.Memo = orderinfo.Memo;
                        info.Hypotest = orderinfo.HypoTest;
                    }

                    #region ��met_nuo_inject�У������¼
                    if (this.InjMgr.Insert(info) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.InjMgr.Err, "��ʾ");
                        return -1;
                    }
                    #endregion

                    #region ��fin_ipb_feeitemlist�У���������

                    //���µ�Ժע����
                    int updateInjectCount = 1;
                    if (isSaveAllInjectCount)
                    {
                        if (isSaveDayInjectCount)
                        {
                            //����һ���Ժע
                            updateInjectCount = detail.InjectCount / detail.Order.Frequency.Times.Length;
                        }
                        else
                        {
                            //����ȫ��Ժע
                            updateInjectCount = detail.InjectCount;
                        } 
                    }

                    if (this.feeIntegrate.UpdateConfirmInject(detail.Order.ID, detail.RecipeNO, detail.SequenceNO.ToString(), updateInjectCount) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.feeIntegrate.Err, "��ʾ");
                        return -1;
                    }
                    #endregion
                    info.Item.InjectCount = info.Item.InjectCount;
                    //���ƿ�ĲŴ�ӡ���Ƶ�---��д��-------------�˶γ�����,��Ϊ�ɲ���Աѡ���Ƿ��ӡ
                    if (info.Item.Order.Usage.ID == "03" ||
                        info.Item.Order.Usage.ID == "04")
                    {
                        alPrint.Add(info);
                    }
                    alInject.Add(info);
                    this.lbLastOrder.Text = "�������һ��ע���:" + info.OrderNO;

                }
                FS.FrameWork.Management.PublicTrans.Commit();

            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return -1;
            }

            if (this.isMessageOnSave)
            {
                MessageBox.Show("����ɹ�!", "��ʾ");
            }
            this.Clear();
            this.lblWarning.Text = "";

            this.txtCardNo.SelectAll();
            this.txtRecipe.Text = "";
            this.txtCardNo.Text = "";
            this.txtCardNo.Focus();
            return 0;
        }

        /// <summary>
        /// ���
        /// </summary>
        private void Clear()
        {
            neuSpread1_Sheet1.RowCount = 0;

            this.txtOrder.Text = "";
            this.txtRecipe.Text = "";
            this.lblWarning.Text = "";
            this.txtCardNo.Text = "";
            this.myRegInfo = null;

            this.txtCardNo.Focus();
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return 1;
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        private void Query()
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
            }
            string cardNo = this.txtCardNo.Text.Trim().PadLeft(10, '0');

            //��ȡҽ�������Ĵ�����Ϣ��û��ȫ��ִ����ģ�
            DateTime dtFrom = this.dtpStart.Value.Date;
            DateTime dtTo = this.dtpEnd.Value.Date.AddDays(1);
            ArrayList al = new ArrayList();

            if (/*this.txtRecipe.Text == null ||*/ this.txtRecipe.Text.Trim() == "")
            {
                if (isShowSubjob)
                {
                    al = this.feeIntegrate.QueryOutpatientFeeItemListsZsSubjob(cardNo, dtFrom, dtTo);
                }
                else
                {
                    al = this.feeIntegrate.QueryOutpatientFeeItemListsZs(cardNo, dtFrom, dtTo);
                }
            }
            else
            {
                al = this.feeIntegrate.QueryFeeDetailFromRecipeNO(this.txtRecipe.Text.Trim());

                if (al == null)
                {
                    return;
                }

                if (al.Count > 0)
                {
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList item = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)al[0];
                    myRegInfo = this.regMgr.GetByClinic(item.Patient.ID);
                    if (myRegInfo == null || myRegInfo.ID == "")
                    {
                        MessageBox.Show("û�в�����Ϊ:" + item.Patient.ID + "�Ļ���!", "��ʾ");

                        this.txtCardNo.Focus();
                        return;
                    }

                    this.txtCardNo.Text = item.Patient.PID.CardNO;
                    this.SetPatient(myRegInfo);
                    this.txtRecipe.Text = "";
                    this.Query();
                    return;
                }

            }

            this.Query(al);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        private void Query(ArrayList al)
        {
            if (al == null || al.Count == 0)
            {
                MessageBox.Show("�û���û����Ҫȷ�ϵ�ҽ����Ϣ!", "��ʾ");
                this.txtCardNo.Focus();
                return;
            }

            this.AddDetail(al);

            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                MessageBox.Show("��ʱ�����û�иû�����Ϣ!", "��ʾ");
                this.txtCardNo.Focus();
                return;
            }

            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                int confirmNum = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.ȷ�ϴ���].Text);
                if (confirmNum == 0)
                {
                    this.lblWarning.Text = "�״�Ժע,��˶�Ժע����!";
                    this.lblWarning.ForeColor = System.Drawing.Color.Magenta;
                }
            }

            this.SelectAll(true);
            this.SetComb();
            this.ShowColor();
            this.txtOrder.Focus();
            if (this.isUserOrderNumber)
            {
                this.SetInject();
            }

            //��ӡѲ�ӿ�
            if (this.isAutoPrint)
            {
                this.PrintInjectScoutCard();
            }

            if (this.isAutoSave)
            {
                this.SelectAll(true);
                this.Save();
            }
        }

        /// <summary>
        /// ��������ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// �����Ŀ��ϸ
        /// </summary>
        /// <param name="detail"></param>
        private void AddDetail(ArrayList details)
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
            }

            //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
            List<FS.HISFC.Models.Fee.Outpatient.FeeItemList> tmpFeeList = new List<FS.HISFC.Models.Fee.Outpatient.FeeItemList>();
            if (details != null)
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList detail in details)
                {
                    #region  ��ҩƷ����ʾ
                    //��ҩƷ����ʾ
                    //if (detail.Item.IsPharmacy == false) continue;
                    if (detail.Item.ItemType != HISFC.Models.Base.EnumItemType.Drug)
                    {
                        if (!isShowSubjob)
                        {
                            continue;
                        }
                        else
                        {
                            tmpFeeList.Add(detail);
                            continue;
                        }
                    }
                    #endregion

                    #region ����Ժע�Ĳ���ʾ
                    if (IsUseSpecialConstant)
                    {
                        //����Ժע�Ĳ���ʾ-------Ȩ��֮��
                        FS.HISFC.BizProcess.Integrate.Manager con = new FS.HISFC.BizProcess.Integrate.Manager();
                        FS.FrameWork.Models.NeuObject obj = con.GetConstant("SPECIAL", detail.Order.Usage.ID);
                        if (obj == null || obj.ID == null || obj.ID == "")
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (!usage.Contains(detail.Order.Usage.Name))
                        {
                            continue;
                        }
                    }
                    #endregion

                    #region  Ժע���� <= ��ȷ��Ժע���� �Ĳ���ʾ

                    //Ժע���� <= ��ȷ��Ժע���� �Ĳ���ʾ
                    if (detail.InjectCount != 0 && detail.InjectCount <= detail.ConfirmedInjectCount && !this.cbFinish.Checked)
                    {
                        continue;
                    }
                    #endregion

                    #region �Ƿ���ʾ0������
                    if (!chkNullNum.Checked && detail.InjectCount == 0)
                    {
                        continue;
                    }
                    #endregion

                    #region �����Ѿ��Ǽǵ�QD����ʾ��ע�����ε�BID����ʾ����ǰ���ע��һ�ε�BID������ʾ��(���ݽ���ĵǼ�ʱ��)
                    DateTime dt = FS.FrameWork.Function.NConvert.ToDateTime(
                        this.InjMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd 00:00:00"));
                    //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
                    ArrayList alTodayInject = this.InjMgr.Query(detail.Patient.PID.CardNO, detail.RecipeNO, detail.SequenceNO.ToString(), dt);

                    FS.HISFC.Models.Order.Frequency frequence = this.freqHelper.GetObjectFromID(detail.Order.Frequency.ID) as FS.HISFC.Models.Order.Frequency;

                    string[] injectTime = frequence.Time.Split('-');
                    //������Ѿ�ȫ��ע����Ϻ�����
                    if (alTodayInject.Count >= injectTime.Length)
                    {
                        continue;
                    }
                    if (this.isShowAllInject)
                    {
                        for (int i = alTodayInject.Count; i < injectTime.Length; i++)
                        {
                            FS.HISFC.Models.Fee.Outpatient.FeeItemList newDetail = detail.Clone();
                            newDetail.User03 = injectTime[i];
                            tmpFeeList.Add(newDetail);
                        }
                    }
                    else
                    {
                        //δ���ϴ�ע��ʱ��Ļ��������ٴεǼ�
                        if (alTodayInject.Count > 0)
                        {
                            DateTime lastInjectTime = FrameWork.Function.NConvert.ToDateTime(dt.ToString("yyyy-MM-dd ") + injectTime[alTodayInject.Count - 1] + ":00");
                            if (this.InjMgr.GetDateTimeFromSysDateTime() < lastInjectTime)
                            {
                                continue;
                            }
                        }
                        detail.User03 = injectTime[alTodayInject.Count];
                        tmpFeeList.Add(detail);
                    }
                    //if (detail.Order.Frequency.ID == "QD")
                    //{
                    //    ArrayList alTemp = this.InjMgr.Query(detail.Patient.PID.CardNO, detail.RecipeNO,
                    //        detail.SequenceNO.ToString(), dt);
                    //    if (alTemp != null)
                    //    {
                    //        if (alTemp.Count > 0) continue;
                    //    }
                    //}
                    //if (detail.Order.Frequency.ID == "BID")
                    //{
                    //    ArrayList alTemp = this.InjMgr.Query(detail.Patient.PID.CardNO, detail.RecipeNO,
                    //        detail.SequenceNO.ToString(), dt);
                    //    if (alTemp != null && alTemp.Count > 0)
                    //    {
                    //        if (alTemp.Count >= 2) continue;
                    //        //��ǰ���ע��һ�ε�BID������ʾ
                    //        FS.HISFC.Models.Nurse.Inject item = (FS.HISFC.Models.Nurse.Inject)alTemp[0];
                    //        bool bl1 = true;
                    //        bool bl2 = true;
                    //        if (FS.FrameWork.Function.NConvert.ToInt32(item.Booker.OperTime.ToString("HH")) > 12) bl1 = false;
                    //        if (FS.FrameWork.Function.NConvert.ToInt32(this.InjMgr.GetDateTimeFromSysDateTime().ToString("HH")) > 12) bl2 = false;
                    //        if (bl1 == bl2) continue;
                    //    }
                    //}
                    #endregion
                    //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
                    //this.AddDetail(detail);
                }

                //����
                tmpFeeList.Sort(new FeeItemListSort());
                //��ȡ��ӡ���
                this.CreateInterface();
                if (this.IGetOrderNo != null)
                {
                    this.IGetOrderNo.SetPrintNo(new ArrayList(tmpFeeList.ToArray()));
                }
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in tmpFeeList)
                {
                    this.AddDetail(feeItem);
                }
            }
        }

        FarPoint.Win.Spread.CellType.TextCellType tcell = new FarPoint.Win.Spread.CellType.TextCellType();

        /// <summary>
        /// �����ϸ
        /// </summary>
        /// <param name="detail"></param>
        private void AddDetail(FS.HISFC.Models.Fee.Outpatient.FeeItemList info)
        {
            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
            int row = this.neuSpread1_Sheet1.RowCount - 1;
            this.neuSpread1_Sheet1.Rows[row].Tag = info;

            #region "���ڸ�ֵ"

            #region ��ȡƤ����Ϣ

            string strTest = "��";

            if (info.Item.ID != "999" && info.Item.ItemType == HISFC.Models.Base.EnumItemType.Drug)
            {
                //��ȡƤ����Ϣ
                drug = this.phaIntegrate.GetItem(info.Item.ID);
                if (drug == null)
                {
                    MessageBox.Show("��ȡҩƷƤ����Ϣʧ��!\r\n" + phaIntegrate.Err, "����", MessageBoxButtons.OK);
                    this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                    return;
                }
                if (drug.IsAllergy)
                {
                    strTest = "��";
                }
            }
            else
            {
                drug = null;
            }
            //
            #endregion

            info.Order.DoctorDept.Name = deptHelper.GetName(info.RecipeOper.Dept.ID);

            this.neuSpread1_Sheet1.SetValue(row, 1, "", false);//ע��˳���

            if (info.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                //this.neuSpread1_Sheet1.Cells[row, 0].CellType = tcell;
                //this.neuSpread1_Sheet1.Cells[row, 0].Text = " ";
                //this.neuSpread1_Sheet1.Cells[row, 0].Value = " ";
                this.neuSpread1_Sheet1.Rows[row].BackColor = Color.Silver;
                this.neuSpread1_Sheet1.SetValue(row, 0, false);
                this.neuSpread1_Sheet1.Rows[row].Locked = true;
            }

            this.neuSpread1_Sheet1.SetValue(row, 2, info.InjectCount.ToString(), false);//Ժע����
            this.neuSpread1_Sheet1.SetValue(row, 3, info.ConfirmedInjectCount.ToString(), false);//�Ѿ�ȷ�ϵ�Ժע����
            this.neuSpread1_Sheet1.SetValue(row, 4, doctHelper.GetName(info.RecipeOper.ID), false);//����ҽ��
            this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.����ҽ��].Tag = info.Order.ReciptDoctor.ID;
            this.neuSpread1_Sheet1.SetValue(row, 5, info.Order.DoctorDept.Name, false);//�Ʊ�
            this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.�Ʊ�].Tag = info.Order.DoctorDept.ID;
            this.neuSpread1_Sheet1.SetValue(row, 6, info.Item.Name, false);//ҩƷ����
            this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.���].Tag = info.Order.Combo.ID;//��Ϻ�
            this.neuSpread1_Sheet1.SetValue(row, 8, info.Order.DoseOnce.ToString() + info.Order.DoseUnit, false);//ÿ����
            this.neuSpread1_Sheet1.SetValue(row, 9, info.Order.Frequency.ID, false);//Ƶ��
            this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.Ƶ��].Tag = info.Order.Frequency.ID.ToString();
            this.neuSpread1_Sheet1.SetValue(row, 10, info.Order.Usage.Name, false);//�÷�
            this.neuSpread1_Sheet1.SetValue(row, 11, strTest, false);//Ƥ�ԣ�
            if (drug != null)
            {
                this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.Ƥ��].Tag = drug.IsAllergy.ToString().ToUpper();
            }
            else
            {
                this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.Ƥ��].Tag = "false".ToUpper();
                this.neuSpread1_Sheet1.SetText(row, 12, "��" + info.Item.Qty * info.Days + info.Item.PriceUnit);
            }

            FS.HISFC.Models.Order.OutPatient.Order orderinfo = new FS.HISFC.Models.Order.OutPatient.Order();

            orderinfo = orderMgr.GetOneOrder(info.Patient.ID, info.Order.ID);
            if (orderinfo != null)
            {
                this.neuSpread1_Sheet1.SetText(row, 12, string.IsNullOrEmpty(orderinfo.Memo) ? " " : orderinfo.Memo);

                //if (orderinfo.HypoTest == 1)
                //{
                //    if (drug != null && !drug.IsAllergy)
                //    {
                //        orderinfo.HypoTest = 0;//���Ե�ֵΪ��
                //    }
                //    else
                //    {
                //        orderinfo.HypoTest = 0;
                //    }
                //}
                this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.Ƥ��].Text = this.orderMgr.TransHypotest(orderinfo.HypoTest);
                this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.Ƥ��].Tag = orderinfo;
            }
            else
            {
                orderinfo = new FS.HISFC.Models.Order.OutPatient.Order();
                if (drug != null && drug.IsAllergy)
                {
                    orderinfo.Item = drug;
                    orderinfo.HypoTest = FS.HISFC.Models.Order.EnumHypoTest.NoHypoTest;
                }
                else
                {
                    orderinfo.HypoTest = 0;
                }

                this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.Ƥ��].Text = this.orderMgr.TransHypotest(orderinfo.HypoTest);
                this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.Ƥ��].Tag = orderinfo;

            }
            //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
            this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.ע��ʱ��].Text = info.User03;


            //��ʾ��ҩ����Ϣ
            if (!string.IsNullOrEmpty(info.ConfirmOper.ID))
            {
                //applyOutObj = drugMgr.GetApplyOut(info.RecipeNO, info.SequenceNO);
                //if (applyOutObj == null)
                //{
                //    MessageBox.Show("��ȡ��ҩ����Ϣʧ��:" + drugMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}

                //if (hsEmpl.Contains(applyOutObj.Operation.ApproveOper.ID))
                //{
                //    empl = hsEmpl[applyOutObj.Operation.ApproveOper.ID] as FS.HISFC.Models.Base.Employee;
                //}
                //{
                //    empl = this.inteMgr.GetEmployeeInfo(applyOutObj.Operation.ApproveOper.ID);
                //    if (empl == null)
                //    {
                //        MessageBox.Show("��ȡ��ҩ����Ϣʧ��:" + inteMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    }
                //    else
                //    {
                //        this.neuSpread1_Sheet1.Cells[row, 17].Text = empl.Name;
                //    }
                //}
            }

            #endregion
        }

        /// <summary>
        /// ������Ϻ�
        /// </summary>
        private void SetComb()
        {
            int myCount = this.neuSpread1_Sheet1.RowCount;
            int i;
            //��һ��
            this.neuSpread1_Sheet1.SetValue(0, 7, "��");
            //�����
            this.neuSpread1_Sheet1.SetValue(myCount - 1, 7, "��");
            //�м���
            for (i = 1; i < myCount - 1; i++)
            {
                int prior = i - 1;
                int next = i + 1;
                string currentRowCombNo = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.���].Tag.ToString();
                string priorRowCombNo = this.neuSpread1_Sheet1.Cells[prior, (int)EnumColSet.���].Tag.ToString();
                string nextRowCombNo = this.neuSpread1_Sheet1.Cells[next, (int)EnumColSet.���].Tag.ToString();

                //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
                string currentRowInjectTime = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.ע��ʱ��].Text.ToString();
                string priorRowInjectTime = this.neuSpread1_Sheet1.Cells[prior, (int)EnumColSet.ע��ʱ��].Text.ToString();
                string nextRowInjectTime = this.neuSpread1_Sheet1.Cells[next, (int)EnumColSet.ע��ʱ��].Text.ToString();

                #region """""
                bool bl1 = true;
                bool bl2 = true;
                //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
                if (currentRowCombNo + currentRowInjectTime != priorRowCombNo + priorRowInjectTime)
                    bl1 = false;
                if (currentRowCombNo + currentRowInjectTime != nextRowCombNo + nextRowInjectTime)
                    bl2 = false;
                //  ��
                if (bl1 && bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 7, "��");
                }
                //  ��
                if (bl1 && !bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 7, "��");
                }
                //  ��
                if (!bl1 && bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 7, "��");
                }
                //  ""
                if (!bl1 && !bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 7, "");
                }
                #endregion
            }
            //��û����ŵ�ȥ��
            for (i = 0; i < myCount; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.���].Tag.ToString() == "")
                {
                    this.neuSpread1_Sheet1.SetValue(i, 7, "");
                }
            }
            //�ж���ĩ�� ����ţ���ֻ���Լ�һ�����ݵ����
            if (myCount == 1)
            {
                this.neuSpread1_Sheet1.SetValue(0, 7, "");
            }
            //ֻ����ĩ���У���ô��Ҫ�ж���Ű�
            if (myCount == 2)
            {
                if (this.neuSpread1_Sheet1.Cells[0, (int)EnumColSet.���].Tag.ToString().Trim() != this.neuSpread1_Sheet1.Cells[1, (int)EnumColSet.���].Tag.ToString().Trim())
                {
                    this.neuSpread1_Sheet1.SetValue(0, 7, "");
                    this.neuSpread1_Sheet1.SetValue(1, 7, "");
                }
                //��ֹһ��ҩbid�����һ��
                if (this.neuSpread1_Sheet1.Cells[0, (int)EnumColSet.ע��ʱ��].Text.ToString().Trim() != this.neuSpread1_Sheet1.Cells[1, (int)EnumColSet.ע��ʱ��].Text.ToString().Trim())
                {
                    this.neuSpread1_Sheet1.SetValue(0, 7, "");
                    this.neuSpread1_Sheet1.SetValue(1, 7, "");
                }
            }
            if (myCount > 2)
            {
                if (this.neuSpread1_Sheet1.GetValue(1, 7).ToString() != "��"
                    && this.neuSpread1_Sheet1.GetValue(1, 7).ToString() != "��")
                {
                    this.neuSpread1_Sheet1.SetValue(0, 7, "");
                }
                if (this.neuSpread1_Sheet1.GetValue(myCount - 2, 7).ToString() != "��"
                    && this.neuSpread1_Sheet1.GetValue(myCount - 2, 7).ToString() != "��")
                {
                    this.neuSpread1_Sheet1.SetValue(myCount - 1, 7, "");
                }
            }

        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        private void Print()
        {
            if (this.alPrint == null || this.alPrint.Count <= 0)
            {
                MessageBox.Show("û����Ҫ��ӡ������!");
                return;
            }

            //��ӡ��ƾ��
            Nurse.Print.ucPrintCure uc = new Nurse.Print.ucPrintCure();
            uc.Init(alPrint);

            //��ӡע�䵥
            Nurse.Print.ucPrintInject uc2 = new Nurse.Print.ucPrintInject();
            uc2.Init(alInject);

            alPrint.Clear();
            alInject.Clear();
        }

        private void SelectedComb(bool isSelect)
        {
            int row = this.neuSpread1_Sheet1.ActiveRowIndex;
            string combID = this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.���].Tag.ToString();
            //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
            string injectTime = this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.ע��ʱ��].Text;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
                if (this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.���].Tag.ToString() == combID && this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.ע��ʱ��].Text == injectTime)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.ѡ��].Value = isSelect;
                }
            }

        }

        /// <summary>
        /// �޸�Ƥ����Ϣ
        /// </summary>
        private void ModifyHytest()
        {
            ArrayList al = new ArrayList();
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                bool isSelected = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.ѡ��].Value);
                if (isSelected)
                {
                    FS.HISFC.Models.Order.OutPatient.Order orderinfo = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.Ƥ��].Tag as FS.HISFC.Models.Order.OutPatient.Order;
                    if (orderinfo.HypoTest == FS.HISFC.Models.Order.EnumHypoTest.FreeHypoTest)
                    {
                        continue;
                    }
                    al.Add(orderinfo);
                }

            }

            if (al.Count == 0)
            {
                return;
            }
            Forms.frmHypoTest frmHypoTest = new FS.HISFC.Components.Nurse.Forms.frmHypoTest();
            frmHypoTest.AlOrderList = al;
            DialogResult d = frmHypoTest.ShowDialog();
            if (d == DialogResult.OK)
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    bool isSelected = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.ѡ��].Value);
                    if (!isSelected)
                    {
                        continue;
                    }
                    FS.HISFC.Models.Order.OutPatient.Order orderinfo = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.Ƥ��].Tag as FS.HISFC.Models.Order.OutPatient.Order;
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.Ƥ��].Text = this.orderMgr.TransHypotest(orderinfo.HypoTest);

                }
            }
        }

        #endregion

        #region �¼�

        /// <summary>
        /// �����Ų�ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtRecipe.Text = string.Empty;
                if (this.txtCardNo.Text.Trim() == "")
                {
                    MessageBox.Show("�����벡����!", "��ʾ");
                    this.txtCardNo.Focus();
                    return;
                }

                HISFC.Models.Account.AccountCard objCard = new FS.HISFC.Models.Account.AccountCard();
                string strCardNO = this.txtCardNo.Text.Trim();
                
                int iTemp = this.feeIntegrate.ValidMarkNO(strCardNO, ref objCard);
                if (iTemp <= 0 || objCard == null)
                {
                    MessageBox.Show("��Ч���ţ�����ϵ����Ա��");
                    return;
                }
                this.Clear();

                string cardNo = objCard.Patient.PID.CardNO;
                ArrayList alRegs = this.regMgr.Query(cardNo, this.dtpStart.Value.AddDays(0 - queryRegDays));
                if (alRegs == null || alRegs.Count == 0)
                {
                    MessageBox.Show("û�в�����Ϊ:" + cardNo + "�Ļ���!", "��ʾ");
                    this.txtCardNo.Focus();
                    return;
                }
                myRegInfo = alRegs[0] as FS.HISFC.Models.Registration.Register;
                if (myRegInfo == null || myRegInfo.ID == "")
                {
                    MessageBox.Show("û�в�����Ϊ:" + cardNo + "�Ļ���!", "��ʾ");

                    this.txtCardNo.Focus();
                    return;
                }

                this.txtCardNo.Text = cardNo;
                this.SetPatient(myRegInfo);

                //�ֽ�ע����Ŀ
                //if (al != null)
                //{
                this.Query();
                //}
                this.txtCardNo.Focus();
                this.txtRecipe.SelectAll();
            }
        }

        /// <summary>
        /// �������Ų�ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRecipe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCardNo.Text = string.Empty;
                if (this.txtRecipe.Text.Trim() == "")
                {
                    this.txtRecipe.Focus();
                    return;
                }
                this.Query();
                if (this.isUserOrderNumber)
                {
                    this.SetInject();
                }
                this.txtRecipe.Focus();
                this.txtRecipe.SelectAll();
            }
        }

        /// <summary>
        /// ��ӡ�����ת
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtCardNo.Focus();
            }
        }

        #region ������

        ///// <summary>
        ///// ��ݼ�����
        ///// </summary>
        ///// <param name="keyData"></param>
        ///// <returns></returns>
        //protected override bool ProcessDialogKey(Keys keyData)
        //{
        //    int altKey = Keys.Alt.GetHashCode();

        //    if (keyData == Keys.F1)
        //    {
        //        this.SelectAll(true);
        //        return true;
        //    }
        //    if (keyData == Keys.F2)
        //    {
        //        this.SelectAll(false);
        //        return true;
        //    }
        //    if (keyData.GetHashCode() == altKey + Keys.S.GetHashCode())
        //    {
        //        if (this.Save() == 0)
        //        {
        //            this.Print();
        //        }
        //        return true;
        //    }
        //    if (keyData.GetHashCode() == altKey + Keys.Q.GetHashCode())
        //    {
        //        this.Query();
        //        return true;
        //    }
        //    if (keyData.GetHashCode() == altKey + Keys.P.GetHashCode())
        //    {
        //        //
        //        return true;
        //    }
        //    if (keyData.GetHashCode() == altKey + Keys.X.GetHashCode())
        //    {
        //        this.FindForm().Close();
        //        return true;
        //    }
        //    return base.ProcessDialogKey(keyData);
        //}
        #endregion

        private void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            int row = this.neuSpread1_Sheet1.ActiveRowIndex;
            bool isSelect = Convert.ToBoolean(this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.ѡ��].Value);
            this.SelectedComb(isSelect);
        }

        /// <summary>
        /// ������ʾ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuSpread1_Sheet1, injectRegisterXml);
        }

        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {

            get
            {
                Type[] types = new Type[1];
                types[0] = typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint);
                return types;
            }
        }

        #endregion

        /// <summary>
        /// ������ö��
        /// </summary>
        enum EnumColSet
        {
            ѡ��,
            ���,
            Ժע����,
            ȷ�ϴ���,
            ����ҽ��,
            �Ʊ�,
            ҽ��,
            ���,
            ÿ������,
            Ƶ��,
            �÷�,
            Ƥ��,
            ҽ����ע,
            ע��ʱ��,
            ע�����,
            ��ҩ��
        }
    }

    /// <summary>
    /// ����
    /// ���߿��ԵǼ�ȫ������ע�䴦��
    /// </summary>
    public class FeeItemListSort : IComparer<FS.HISFC.Models.Fee.Outpatient.FeeItemList>
    {
        public int Compare(FS.HISFC.Models.Fee.Outpatient.FeeItemList x, FS.HISFC.Models.Fee.Outpatient.FeeItemList y)
        {
            //�Ȱ��մ�������
            if (x.RecipeNO != y.RecipeNO)
            {
                return -y.RecipeNO.CompareTo(x.RecipeNO);
            }
            //��ע��ʱ������
            if (x.User03 != y.User03)
            {
                string a = x.User03;
                string b = y.User03;
                if (a.Length < "12:00".Length)
                {
                    a = a.PadLeft("12:00".Length, '0');
                }
                if (b.Length < "12:00".Length)
                {
                    b = b.PadLeft("12:00".Length, '0');
                }
                return -b.CompareTo(a);
            }

            //����Ϻ�����
            if (x.Order.Combo.ID != y.Order.Combo.ID)
            {
                return -y.Order.Combo.ID.CompareTo(x.Order.Combo.ID);
            }
            //���������
            if (x.SequenceNO != y.SequenceNO)
            {
                return -y.SequenceNO.CompareTo(x.SequenceNO);
            }
            //ҩƷ����
            if (x.Item.ID != y.Item.ID)
            {
                return -y.Item.ID.CompareTo(x.Item.ID);
            }
            return 0;
        }
    }
}
