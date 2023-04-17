using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.WinForms.DrugStore
{
    /// <summary>
    /// <br></br>
    /// [��������: ҩ����ҩ������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11]<br></br>
    /// </summary>
    public partial class frmInpatientDrug : FS.FrameWork.WinForms.Forms.BaseStatusBar,FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public frmInpatientDrug()
        {
            InitializeComponent();

            this.ProgressRun(true);

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {                
                this.InitControlParm();

                this.tvDrugMessage1.Init();
            }
        }

        #region �����

        /// <summary>
        /// ��ҩ�����ӿ�
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.Pharmacy.IInpatientDrug IDrugManager;
	
        /// <summary>
		/// ���β����İ�ҩ̨
		/// </summary>
		protected FS.HISFC.Models.Pharmacy.DrugControl drugControl = new FS.HISFC.Models.Pharmacy.DrugControl();
		
        /// <summary>
		/// ��ǰ��ҩ֪ͨ
		/// </summary>
		protected FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = new FS.HISFC.Models.Pharmacy.DrugMessage();
		
        /// <summary>
		/// ҩ��������
		/// </summary>
		protected FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

        
        /// <summary>
		/// ��ǰ���ҵ�ȫ����ҩ̨
		/// </summary>
		private ArrayList drugControlGather = null;

        /// <summary>
        /// �Զ���ӡ�Ƿ���ö��߳�Timer��ʽ
        /// </summary>
        private bool isAutoByThreadTimer = true;

        /// <summary>
        /// ��׼��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject ApproveOperDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �Ƿ�ҩ�����
        /// </summary>
        private bool isArkDept = false;

        /// <summary>
        /// ��ӡ�ӿ�ʵ����
        /// </summary>
        private object printInterfaceInstance = null;

        #endregion

        #region ����

		/// <summary>
		/// ��ǰ���ҵ�ȫ����ҩ̨
		/// </summary>
		public ArrayList DrugControlGather
		{
			set
			{
				this.drugControlGather = value;
				this.InitMenu();
			}
		}

        /// <summary>
        /// �Ƿ���ʾ��ϸ
        /// </summary>
        public bool IsShowDetail
        {
            get
            {
                return this.ucDrugDetail1.Visible;
            }
            set
            {
                this.SuspendLayout();

                this.ucDrugDetail1.Visible = value;
                this.ucDrugMessage1.Visible = !value;

                this.ResumeLayout();

                //������ʾ�Ŀؼ������ýӿڵ�ʵ��
                if (value)
                    this.IDrugManager = this.ucDrugDetail1 as FS.HISFC.BizProcess.Interface.Pharmacy.IInpatientDrug;
                else
                    this.IDrugManager = this.ucDrugMessage1 as FS.HISFC.BizProcess.Interface.Pharmacy.IInpatientDrug;
            }
        }

        /// <summary>
        /// �Ƿ��ӡ��ǩ
        /// </summary>
        private bool IsOuterPrintLabel
        {
            set
            {
                this.ucDrugDetail1.IsPrintLabel = value;
                this.ucDrugMessage1.IsPrintLabel = value;
                this.tvDrugMessage1.IsPrintLabel = value;
            }
        }

        /// <summary>
        /// �Ƿ��Զ���ӡ
        /// </summary>
        private bool IsAutoPrint
        {
            set
            {
                this.tvDrugMessage1.IsAutoPrint = value;
                this.ucDrugMessage1.IsAutoPrint = value;
                this.ucDrugDetail1.IsAutoPrint = value;
            }
        }

        #endregion

        /// <summary>
        /// ��ȡ���Ʋ���
        /// </summary>
        private void InitControlParm()
        {
            FS.FrameWork.Management.ControlParam ctrlManager = new ControlParam();
            string ctrlAutoByMessage = ctrlManager.QueryControlerInfo("51007"); //�Ƿ��б��Զ�ˢ��ʱ������Ϣ������ʽ
            if (ctrlAutoByMessage == "1")
            {
                this.isAutoByThreadTimer = false;
            }
            else
            {
                this.isAutoByThreadTimer = true;
            }

            this.ApproveOperDept = ((FS.HISFC.Models.Base.Employee)ctrlManager.Operator).Dept.Clone();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڼ��ذ�ҩ���ؼ�..."));
            Application.DoEvents();

            #region �����ȡ��ǩ��ʽ

            if (this.printInterfaceInstance == null)
            {
                this.printInterfaceInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint)) as FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint;

            }

            if (this.printInterfaceInstance != null)
            {
                this.ucDrugDetail1.AddDrugBill(this.printInterfaceInstance, false);
            }
            else
            {
                MessageBox.Show("δ����סԺ��ҩ����ʵ��,�޷����а�ҩ����ӡ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            #endregion

            FS.HISFC.Components.DrugStore.Function.IsApproveInitPrintInterface = false;

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        #region ��ҩ̨���÷���

        /// <summary>
        /// ��ȡ��֪��ҩ̨����ҩ������ҩ̨����
        /// </summary>
        /// <param name="drugControl">��֪��ҩ̨</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private int GetDrugControlGather(FS.HISFC.Models.Pharmacy.DrugControl drugControl)
        {
            //ȡ������ȫ����ҩ̨�б�
            ArrayList al = this.drugStoreManager.QueryDrugControlList(drugControl.Dept.ID);
            if (al == null)
            {
                MessageBox.Show(Language.Msg("��ȡȫ����ҩ̨�б�������!") + drugStoreManager.Err);
                return -1;
            }
            this.DrugControlGather = al;

            return 1;
        }

        /// <summary>
		/// ���ݰ�ҩ����ȡ�����ҵİ�ҩ̨
		/// </summary>
		/// <returns>�ɹ�����1ʧ�ܷ���-1</returns>
        public virtual int SetDrugControl()
        {         
            if (this.Tag != null && this.Tag.ToString() != "")
            {
                if (this.drugControl.ID != "")
                    return 1;

                this.drugControl.Dept = this.ApproveOperDept;

                FS.FrameWork.Models.NeuObject tempDept = drugControl.Dept.Clone();

                this.SetArkDept(ref tempDept);

                drugControl.Dept = tempDept;

                #region ���ݴ��ڲ��� ������ҩ̨

                this.drugControl.DrugAttribute.ID = this.Tag.ToString();
               
                //�ж��Ƿ������Ч�İ�ҩ̨��
                this.drugControl = this.drugStoreManager.GetDrugControl(this.drugControl.Dept.ID, (FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute)this.drugControl.DrugAttribute.ID);
                if (this.drugControl == null)
                {
                    MessageBox.Show(Language.Msg("ȡ��ҩ̨ʱ����") + this.drugStoreManager.Err);
                    return -1;
                }
                if (this.drugControl.ID == "")
                {
                    MessageBox.Show(Language.Msg("�����ñ������С�" + this.drugControl.DrugAttribute.Name + "���İ�ҩ̨��"));
                    return -1;
                }

                this.SetDrugControl(this.drugControl);

                #endregion

                if (this.GetDrugControlGather(this.drugControl) == -1)
                    return -1;
            }         

            return 1;
        }

		/// <summary>
		/// ���ݴ���İ�ҩ̨���ñ����ҵİ�ҩ̨
		/// </summary>
		/// <param name="drugControl">��ҩ̨</param>
		/// <returns></returns>
        public virtual int SetDrugControl(FS.HISFC.Models.Pharmacy.DrugControl drugControl)
        {
            FS.FrameWork.Models.NeuObject tempDept = drugControl.Dept;

            this.SetArkDept(ref tempDept);

            drugControl.Dept = tempDept;

            if (this.drugControl.Dept.ID != drugControl.Dept.ID)
            {
                this.GetDrugControlGather(drugControl);
            }

            this.drugControl = drugControl;
            try
            {
                this.SetPropertyByDrugControl();
                if (this.IDrugManager != null)
                    this.IDrugManager.Clear();

                this.ShowList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("���ݴ����ҩ̨���ñ����ҵİ�ҩ̨ʧ��!") + ex.Message);
            }
            return 1;
        }

		/// <summary>
		/// ������ҩ̨������ʾ
		/// </summary>
        protected void SetPropertyByDrugControl()
        {
            //�ɸ�����Ҫ��������
            this.ucDrugDetail1.IsShowBillPreview = false;           //�Ƿ���ʾ��ҩ��Ԥ��Tab

            //{22E638FE-2821-4bdf-A8A9-5BD25D51742E}  ͨ�����Ʋ�����������
            //this.ucDrugDetail1.IsShowPatientTot = false;            //�Ƿ���ʾ����ҩƷ����
            //this.ucDrugDetail1.IsShowDeptTot = true;                //�Ƿ���ʱ�����ҩƷ����

            #region ���ݲ�ͬ��ҩ̨���ÿؼ�����

            if (this.drugControl.DrugAttribute.ID.ToString() == "S" || this.drugControl.DrugAttribute.ID.ToString() == "T")		//ֻ��������ҩ̨��ʾ
                this.ucDrugDetail1.IsAutoCheck = true;
            else
                this.ucDrugDetail1.IsAutoCheck = false;

            if (this.drugControl.DrugAttribute.ID.ToString() == "R")		//��ҩ̨
                this.ucDrugDetail1.IsFilterBillCode = true;
            else
                this.ucDrugDetail1.IsFilterBillCode = false;

            //{ECF8E92A-9116-4a4e-9487-2BFC73F8DBE1}
            //������������������Ϣȡ��  --- �����ͨ���ӿ�ʵ�ֽ���������� û��Ҫͨ���˴���������
            this.IsOuterPrintLabel = false;
            this.ucDrugDetail1.IsNeedPreview = false;
            this.ucDrugMessage1.IsNeedPreview = false;
            this.IsAutoPrint = false;

            //if (this.drugControl.DrugAttribute.ID.ToString() == "O")      //��Ժ��ҩ
            //    this.IsOuterPrintLabel = this.drugControl.IsPrintLabel;
            //else
            //    this.IsOuterPrintLabel = this.drugControl.IsPrintLabel;

            ////�Ƿ���Ҫ��ҩ��Ԥ�� ������Ҫ��������
            //this.ucDrugDetail1.IsNeedPreview = this.drugControl.IsBillPreview;
            //this.ucDrugMessage1.IsNeedPreview = this.drugControl.IsBillPreview;

            ////�Ƿ��Զ���ӡ
            //this.IsAutoPrint = this.drugControl.IsAutoPrint;

            //{ECF8E92A-9116-4a4e-9487-2BFC73F8DBE1}

            //�б���ʾʱ�Ƿ��տ������ȷ�ʽ��ʾ ������Ҫ��������
            //{22E638FE-2821-4bdf-A8A9-5BD25D51742E}  ͨ�����Ʋ�����������
            //this.ucDrugDetail1.IsDeptFirst = true;
            //this.ucDrugMessage1.IsDeptFirst = true;
            //this.tvDrugMessage1.IsDeptFirst = true;
            
            #endregion
           
            //��ʾ������
            this.Text = "סԺ��ҩ - " + this.drugControl.Name;
            this.tsLabel.Text = "��ǰ��ҩ̨:" + this.drugControl.Name;

            //���ð�ҩ��ϸ��ʾ����
            if (this.drugControl.ShowLevel == 0)
            {
                this.IsShowDetail = false;
            }
            else
            {
                this.IsShowDetail = true;
            }

            this.tvDrugMessage1.OperDrugControl = this.drugControl;

            this.SetToolBarVisible();

            this.SetDefaultValue();

            this.SetStatusBarText();

            this.RefreshInit();
        }

        #endregion

        #region ���������˵� ��������ť��ʾ ����

        /// <summary>
        /// ��ʼ���˵���ʾ
        /// </summary>
        protected void InitMenu()
        {
            #region ������ҩ̨�б���ʾ

            if (this.drugControlGather == null)
                return;

            this.tsbControlList.DropDownItems.Clear();

            foreach (FS.HISFC.Models.Pharmacy.DrugControl info in this.drugControlGather)
            {
                System.Windows.Forms.ToolStripMenuItem menuItem = new ToolStripMenuItem(info.Name);

                if (this.drugControl != null && info.ID == this.drugControl.ID)
                    this.SetCheckMenuItem(menuItem, true);
                else
                    this.SetCheckMenuItem(menuItem, false);

                menuItem.Tag = info;
                menuItem.Click += new EventHandler(menuItem_Click);

                this.tsbControlList.DropDownItems.Add(menuItem);
            }

            #endregion

            #region ˢ��/��ӡ��ʽ����

            System.Windows.Forms.ToolStripMenuItem autoRefreshMenuItem = new ToolStripMenuItem("�Զ�ˢ��");
            autoRefreshMenuItem.Tag = "Auto";
            autoRefreshMenuItem.Click += new EventHandler(RefreshMenuItem_Click);
            this.tsbRefreshWay.DropDownItems.Add(autoRefreshMenuItem);

            System.Windows.Forms.ToolStripMenuItem handworkRefreshMenuItem = new ToolStripMenuItem("�ֹ�ˢ��");
            handworkRefreshMenuItem.Tag = "Handwork";
            handworkRefreshMenuItem.Click += new EventHandler(RefreshMenuItem_Click);
            this.tsbRefreshWay.DropDownItems.Add(handworkRefreshMenuItem);

            System.Windows.Forms.ToolStripMenuItem autoPrintMenuItem = new ToolStripMenuItem("�Զ���ӡ");
            autoPrintMenuItem.Tag = "Auto";
            autoPrintMenuItem.Click += new EventHandler(PrintMenuItem_Click);
            this.tsbAutoPrint.DropDownItems.Add(autoPrintMenuItem);

            System.Windows.Forms.ToolStripMenuItem handworkPrintMenuItem = new ToolStripMenuItem("�ֹ���ӡ");
            handworkPrintMenuItem.Tag = "Handwork";
            handworkPrintMenuItem.Click += new EventHandler(PrintMenuItem_Click);
            this.tsbAutoPrint.DropDownItems.Add(handworkPrintMenuItem);            

            #endregion

            #region ��ӡ����

            System.Windows.Forms.ToolStripMenuItem pausePrintMenuItem = new ToolStripMenuItem("��ͣ��ӡ");
            pausePrintMenuItem.Tag = "Pause";
            pausePrintMenuItem.Click += new EventHandler(PrinterSetMenuItem_Click);
            this.tsbPrinterSetList.DropDownItems.Add(pausePrintMenuItem);

            System.Windows.Forms.ToolStripMenuItem continueMenuItem = new ToolStripMenuItem("������ӡ");
            continueMenuItem.Tag = "Continue";
            continueMenuItem.Click += new EventHandler(PrinterSetMenuItem_Click);
            this.tsbPrinterSetList.DropDownItems.Add(continueMenuItem);

            #endregion
        }

        /// <summary>
        /// ���������˵�������ʾ
        /// </summary>
        /// <param name="menuItem">����Ĳ˵���</param>
        /// <param name="isCheck">�Ƿ�ѡ��</param>
        private void SetCheckMenuItem(ToolStripMenuItem menuItem, bool isCheck)
        {
            if (isCheck)
                menuItem.BackColor = System.Drawing.Color.Moccasin;
            else
                menuItem.BackColor = Color.FromName(System.Drawing.KnownColor.Control.ToString());
        }

        /// <summary>
        /// ��ȡ��ǰѡ�еĲ˵���
        /// </summary>
        /// <param name="splitButton">���жϵ�������ť</param>
        /// <returns>���ص�ǰѡ�еĲ˵��� ��ѡ�з���null</returns>
        private ToolStripMenuItem GetCheckMenuItem(ToolStripSplitButton splitButton)
        {
            foreach(ToolStripMenuItem checkMenu in splitButton.DropDownItems)
            {
                if (checkMenu.BackColor == System.Drawing.Color.Moccasin)
                    return checkMenu;
            }
            return null;
        }

        /// <summary>
        /// ���ù�������ť��ʾ
        /// </summary>
        private void SetToolBarVisible()
        {
            this.tsbRefreshWay.Visible = this.drugControl.IsAutoPrint;
            this.tsbAutoPrint.Visible = this.drugControl.IsAutoPrint;
            this.tsbPrint.Visible = this.drugControl.IsAutoPrint;
            this.tsbPrinterSetList.Visible = this.drugControl.IsAutoPrint;
        }

        #endregion

        #region ҩ����

        /// <summary>
        /// ҩ����
        /// </summary>
        private void SetArkDept(ref FS.FrameWork.Models.NeuObject operDept)
        {
            FS.HISFC.BizLogic.Pharmacy.Constant phaConsManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
            FS.HISFC.Models.Pharmacy.DeptConstant deptCons = phaConsManager.QueryDeptConstant(operDept.ID);
            if (deptCons == null)
            {
                MessageBox.Show(Language.Msg("���ݿ��ұ����ȡ���ҳ�����Ϣ��������") + phaConsManager.Err);
                return;
            }

            operDept.Memo = FS.FrameWork.Function.NConvert.ToInt32(deptCons.IsArk).ToString();
            if (this.tvDrugMessage1.StockMarkDept == null || (this.tvDrugMessage1.StockMarkDept.ID != operDept.ID))
            {
                this.tvDrugMessage1.StockMarkDept = operDept.Clone();
            }

            if (deptCons.IsArk)         //��ҩ�������ҽ������´���
            {
                #region ҩ����

                FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
                ArrayList al = managerIntegrate.LoadPhaParentByChildren(operDept.ID);
                if (al == null || al.Count == 0)
                {
                    MessageBox.Show(Language.Msg("��ȡ���ҽṹ��Ϣ��������") + managerIntegrate.Err);
                    return;
                }

                FS.HISFC.Models.Base.DepartmentStat deptStat = al[0] as FS.HISFC.Models.Base.DepartmentStat;
                if (deptStat.PardepCode.Substring(0, 1) == "S")     //�ϼ��ڵ�Ϊ������� �����д���
                {
                    return;
                }
                else
                {
                    this.ucDrugDetail1.ArkDept = operDept.Clone();
                    this.ucDrugMessage1.ArkDept = operDept.Clone();                    

                    //��׼�ۿ����Ϊҩ������ҩ��
                    this.ucDrugDetail1.ApproveDept = new FS.FrameWork.Models.NeuObject();
                    this.ucDrugDetail1.ApproveDept.ID = deptStat.PardepCode;
                    this.ucDrugDetail1.ApproveDept.Name = deptStat.PardepName;

                    this.ucDrugMessage1.ApproveDept = new FS.FrameWork.Models.NeuObject();
                    this.ucDrugMessage1.ApproveDept.ID = deptStat.PardepCode;
                    this.ucDrugMessage1.ApproveDept.Name = deptStat.PardepName;

                    operDept.ID = deptStat.PardepCode;
                    operDept.Name = deptStat.PardepName;                   
                }

                #endregion

                this.isArkDept = true;
            }                
        }

        #endregion

        /// <summary>
		/// ��ȡ��ҩ֪ͨ���ݣ�����ʾ��ҩ֪ͨ�����б�
		/// </summary>
        public virtual void ShowList()
        {
            //��ճ�������������ʾ
            this.IDrugManager.Clear();         

            this.tvDrugMessage1.ShowList(this.drugControl);

            this.tvDrugMessage1.SetExpand(0, false);
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Save()
        {
            //FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڱ���" + this.drugMessage.StockDept.Name + "��ҩ��"));
            Application.DoEvents();
            if (this.IDrugManager.Save(this.drugMessage) > 0)
            {
                this.ShowList();             //ˢ�°�ҩ֪ͨ�б�
            }
            //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        public void Print()
        {
            if (this.IDrugManager != null)
                this.IDrugManager.Print();
        }

        /// <summary>
        /// �޸�״̬����ʾ��Ϣ
        /// </summary>
        private void SetStatusBarText()
        {
            if (this.drugControl.IsAutoPrint)
            {
                ToolStripMenuItem refreshWayItem = this.GetCheckMenuItem(this.tsbRefreshWay);
                ToolStripMenuItem autoPrintItem = this.GetCheckMenuItem(this.tsbAutoPrint);
                if (refreshWayItem == null)
                    refreshWayItem = new ToolStripMenuItem("�Զ�ˢ��");
                if (autoPrintItem == null)
                    autoPrintItem = new ToolStripMenuItem("�Զ���ӡ");

                this.statusBar1.Panels[3].Text = string.Format("����: {0}  ��ҩ̨:{1}  ˢ�·�ʽ: {2}  ��ӡ��ʽ: {3}", this.ApproveOperDept.Name, this.drugControl.Name,refreshWayItem.Text,autoPrintItem.Text);
            }
            else
            {
                this.statusBar1.Panels[3].Text = string.Format( "����: {0}  ��ҩ̨:{1} ", this.ApproveOperDept.Name, this.drugControl.Name );
            }
        }

        #region �Զ�ˢ�´���

        #region ����ˢ�µ������

        private System.Threading.Timer refreshTimer;
        private System.Threading.TimerCallback refreshTimerCallBack;
        private delegate void ShowListDelegate();

        /// <summary>
        /// �Ƿ����ڴ���ˢ��
        /// </summary>
        private bool isBusy = false;
        /// <summary>
        /// ˢ�¼��
        /// </summary>
        private int refreshInterval = 180;
        /// <summary>
        /// ��ʼˢ���߳�ִ���ӳ�
        /// </summary>
        private int startDelay = 10;

        #endregion

        /// <summary>
        /// ˢ�³�ʼ��
        /// </summary>
        private void RefreshInit()
        {
            if (!this.drugControl.IsAutoPrint)
                this.RefreshSwitch(true);
            else
                this.RefreshSwitch(false);
        }

        /// <summary>
        /// �Զ�ˢ���л�
        /// </summary>
        /// <param name="isStop">�Ƿ�ֹͣ�Զ�ˢ��</param>
        private void RefreshSwitch(bool isStop)
        {
            this.IsAutoPrint = !isStop;

            if (this.isAutoByThreadTimer)
            {
                if (isStop)
                {
                    if (this.refreshTimer != null)
                        this.refreshTimer.Dispose();
                }
                else
                {
                    this.refreshTimerCallBack = new System.Threading.TimerCallback(this.AutoRefresh);
                    this.refreshTimer = new System.Threading.Timer(this.refreshTimerCallBack, null, this.startDelay * 1000, this.refreshInterval * 1000);
                }
            }
        }

        /// <summary>
        /// ˢ��ִ��
        /// </summary>
        /// <param name="param">����</param>
        private void AutoRefresh(object param)
        {
            if (this.isBusy)
                return;

            this.isBusy = true;

            ShowListDelegate showList = new ShowListDelegate(this.ShowList);

            try
            {
                this.Invoke(showList, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.isBusy = false;
        }

        #endregion

        /// <summary>
        /// ���ô���Ĭ��ֵ��ʾ
        /// </summary>
        private void SetDefaultValue()
        {
            foreach (ToolStripMenuItem controlItem in this.tsbControlList.DropDownItems)
            {
                if ((controlItem.Tag as FS.HISFC.Models.Pharmacy.DrugControl).ID == this.drugControl.ID)
                    this.SetCheckMenuItem(controlItem, true);
                else
                    this.SetCheckMenuItem(controlItem, false);
            }
            if (this.drugControl.IsAutoPrint)
            {
                foreach (ToolStripMenuItem printItem in this.tsbAutoPrint.DropDownItems)
                {
                    if (printItem.Text == "�Զ���ӡ")
                        this.SetCheckMenuItem(printItem, true);
                    else
                        this.SetCheckMenuItem(printItem, false);
                }

                foreach (ToolStripMenuItem refreshItem in this.tsbRefreshWay.DropDownItems)
                {
                    if (refreshItem.Text == "�Զ�ˢ��")
                        this.SetCheckMenuItem(refreshItem, true);
                    else
                        this.SetCheckMenuItem(refreshItem, false);
                }
            }            
        }

        /// <summary>
        /// ��ҩ��׼������ʾ
        /// </summary>
        private void ShowApprove()
        {
            frmDrugBillApprove frmApprove = new frmDrugBillApprove();
            frmApprove.ShowDialog();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);            

            this.WindowState = FormWindowState.Maximized;

            try
            {                
                this.SetDrugControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F12:
                    //F4��ִ�а�ҩ������
                    break;
                case Keys.F5:
                    //ˢ��
                    this.ShowList();
                    break;
                case Keys.F8:
                    //����
                    if (this.IDrugManager.Save(this.drugMessage) > 0)
                    {
                        //ˢ�°�ҩ֪ͨ�б�
                        this.ShowList();
                    }
                    break;
                case Keys.F10:
                    //F10���˳�
                    this.Close();
                    break;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void menuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            this.drugControl = menuItem.Tag as FS.HISFC.Models.Pharmacy.DrugControl;

            System.Windows.Forms.ToolStripMenuItem tempMenu = this.GetCheckMenuItem(this.tsbControlList);
            if (tempMenu != null)
            {
                if ((tempMenu.Tag as FS.HISFC.Models.Pharmacy.DrugControl).ID == this.drugControl.ID)
                {
                    return;
                }
            }

            //ȡ������ѡ�б��
            foreach (ToolStripMenuItem info in this.tsbControlList.DropDownItems)
            {
                this.SetCheckMenuItem(info, false);
            }
            //����ѡ�б��
            this.SetCheckMenuItem(menuItem, true);
            //�������ð�ҩ̨����ʾ
            this.SetDrugControl(this.drugControl);
        }

        private void PrintMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem printMenu = sender as ToolStripMenuItem;
            ToolStripMenuItem tempMenu = this.GetCheckMenuItem(this.tsbAutoPrint);
            if (tempMenu != null && tempMenu.Tag.ToString() == printMenu.Tag.ToString())
                return;

            if (printMenu.Tag.ToString() == "Auto")
            {
                this.IsAutoPrint = true;
            }
            else
            {
                this.IsAutoPrint = false;
            }

            foreach (ToolStripMenuItem tempItem in this.tsbAutoPrint.DropDownItems)
            {
                this.SetCheckMenuItem(tempItem, false);
            }

            this.SetCheckMenuItem(printMenu, true);

            this.SetStatusBarText();

        }

        private void PrinterSetMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem printerSetMenu = sender as ToolStripMenuItem;
            if (printerSetMenu.Tag.ToString() == "Pause")
            {
                FS.FrameWork.WinForms.Classes.Print.PausePrintJob(0);
            }
            else
            {
                FS.FrameWork.WinForms.Classes.Print.ResumePrintJob(0);
            }

            foreach (ToolStripMenuItem tempItem in this.tsbPrinterSetList.DropDownItems)
            {
                this.SetCheckMenuItem(tempItem, false);
            }

            this.SetCheckMenuItem(printerSetMenu, true);
        }

        private void RefreshMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem refreshMenu = sender as ToolStripMenuItem;
            ToolStripMenuItem tempMenuItem = this.GetCheckMenuItem(this.tsbRefreshWay);
            if (tempMenuItem != null && refreshMenu.Tag.ToString() == tempMenuItem.Tag.ToString())
                return;

            if (refreshMenu.Tag.ToString() == "Auto")
                this.RefreshSwitch(false);
            else
                this.RefreshSwitch(true);

            foreach (ToolStripMenuItem tempItem in this.tsbRefreshWay.DropDownItems)
            {
                this.SetCheckMenuItem(tempItem, false);
            }

            this.SetCheckMenuItem(refreshMenu, true);

            this.SetStatusBarText();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == this.tsbCheckAll)      //ȫѡ
            {
                this.IDrugManager.CheckAll();
                return;
            }
            if (e.ClickedItem == this.tsbCheckNone)     //ȡ��Ȩ��
            {
                this.IDrugManager.CheckNone();
                return;
            }
            if (e.ClickedItem == this.tsbRefresh)       //ˢ��
            {
                this.ShowList();
                return;
            }
            if (e.ClickedItem == this.tsbSave)          //����
            {
                //�ò��ַ�ֹ�򿪰�ҩ��׼������ٽ��д�ӡʱ�޷���ȷ��ӡ��
                if (FS.HISFC.Components.DrugStore.Function.IsApproveInitPrintInterface)
                {
                    this.InitControlParm();
                }

                this.Save();
                return;
            }
            if (e.ClickedItem == this.tsbDrugBill)      //��ҩ������ ��ҩ��׼
            {
                this.ShowApprove();
                return;
            }
            if (e.ClickedItem == this.tsbPrint)         //��ӡ
            {
                //�ò��ַ�ֹ�򿪰�ҩ��׼������ٽ��д�ӡʱ�޷���ȷ��ӡ��
                if (FS.HISFC.Components.DrugStore.Function.IsApproveInitPrintInterface)
                {
                    this.InitControlParm();
                }

                this.Print();
                return;
            }
            if (e.ClickedItem == this.tsbExit)          //�˳�
            {
                this.Close();
                return;
            }
        }

        private void tvDrugMessage1_SelectDataEvent(FS.HISFC.Models.Pharmacy.DrugMessage drugMessage,ArrayList alData, bool isShowDetail)
        {
            if (this.IDrugManager != null)
            {
                this.drugMessage = drugMessage;

                this.IsShowDetail = isShowDetail;       //���ýӿ� �ؼ���ʾ

                this.IDrugManager.ShowData(alData);
            }
        }

        private void ucQueryInpatientNo1_myEvent()
        {
            if (this.ucQueryInpatientNo1.InpatientNo != null)
            {
                this.tvDrugMessage1.FindPatient(this.ucQueryInpatientNo1.InpatientNo);
            }
        }

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] printType = new Type[1];
                printType[0] = typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint);

                return printType;
            }
        }

        #endregion
      }
}