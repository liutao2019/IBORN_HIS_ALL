using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// [��������: ҽ���ֽ�ؼ���˵]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucOrderExecConfirm : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOrderExecConfirm()
        {
            InitializeComponent();
        }

        #region ����

        protected DateTime dt;

        /// <summary>
        /// ҩƷȫѡ���
        /// </summary>
        bool tab0AllSelect = false;

        /// <summary>
        /// ��ҩƷȫѡ���
        /// </summary>
        bool tab1AllSelect = false;

        bool bOnQuery = false;

        /// <summary>
        /// �÷���Ӧ�ķֽ�ʱ��
        /// </summary>
        private Hashtable hsUsageAndTime = new Hashtable();

        /// <summary>
        /// ҽ���ֽ�ӿ�
        /// </summary>
        private FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IConfirmExecOrder IConfirmExecOrder = null;

        /// <summary>
        /// Сʱ�Ʒ�ҽ����Ƶ�δ���
        /// </summary>
        string frequencyID = "";

        #endregion

        #region ����

        //protected ArrayList al = new ArrayList();

        ///// <summary>
        ///// ��Ա�б�
        ///// </summary>
        //private ArrayList alPatients
        //{
        //    get
        //    {
        //        if (al == null) al = new ArrayList();
        //        return al;
        //    }
        //    set
        //    {
        //        this.al = value;
        //    }
        //}

        /// <summary>
        /// ��Ա�б�
        /// </summary>
        private ArrayList alPatients = new ArrayList();

        /// <summary>
        /// Ĭ�Ϸֽ�ִ������
        /// </summary>
        protected int intDyas = 1;

        /// <summary>
        /// Ĭ�Ϸֽ�ִ������
        /// </summary>
        [Category("�ֽ�����"), Description("Ĭ�Ϸֽ�ִ������")]
        public int Days
        {
            set
            {
                this.intDyas = value;
                //this.txtDays.Value = (decimal)value;
                this.txtDays.Maximum = this.intDyas;
            }
            get
            {
                return this.intDyas;
            }
        }

        /// <summary>
        /// ��ʾ����Ϣ
        /// </summary>
        [Category("�ֽ�����"), Description("��ʾ���û�����ʾ��Ϣ��")]
        public string Tip
        {
            get
            {
                return this.neuLabel3.Text;
            }
            set
            {
                this.neuLabel3.Text = value;
            }
        }

        /// <summary>
        /// Ƿ���ж�ģʽ
        /// </summary>
        protected EnumLackFee lackfee = EnumLackFee.���ж�Ƿ��;

        /// <summary>
        /// Ƿ�Ѳ���
        /// </summary>
        [Category("�ֽ�����"), Description("Ƿ���ж�ģʽ")]
        public EnumLackFee Ƿ�Ѳ���
        {
            get
            {
                return this.lackfee;
            }
            set
            {
                this.lackfee = value;
            }
        }

        /// <summary>
        /// Ƿ�ѻ�����˱���ʱ�Ƿ�ͬʱ�Ʒ�
        /// </summary>
        private bool lackFeeDealModel = true;

        /// <summary>
        /// Ƿ�ѻ�����˱���ʱ�Ƿ�ͬʱ�Ʒ�
        /// </summary>
        [Category("�ֽ�����"), Description("Ƿ�ѻ�����˱���ʱ�Ƿ�ͬʱ�Ʒѣ�")]
        public bool LackFeeDealModel
        {
            get
            {
                return lackFeeDealModel;
            }
            set
            {
                lackFeeDealModel = value;
            }
        }

        /// <summary>
        /// ��ǰ������Ϊ�����治�ɹ�ʱ���Ƿ�������������������ߣ�
        /// </summary>
        protected bool isSaveErrContinue = true;

        /// <summary>
        /// ���������Ƿ���������������߱���
        /// </summary>
        [Category("�ֽ�����"), Description("��ǰ������Ϊ�����治�ɹ�ʱ���Ƿ�������������������ߣ�")]
        public bool IsSaveErrContinue
        {
            get
            {
                return this.isSaveErrContinue;
            }
            set
            {
                this.isSaveErrContinue = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ��ѡ����ҩƷ
        /// </summary>
        private bool isUseUnChkSpecial = false;

        /// <summary>
        /// �Ƿ���ʾ��ѡ����ҩƷ
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ���ʾ��ѡ����ҩƷ")]
        public bool IsUseUnChkSpecial
        {
            set { this.isUseUnChkSpecial = value; }
            get { return this.isUseUnChkSpecial; }
        }

        #endregion

        #region ����
        /// <summary>
        /// ��ʼ��FpSpread
        /// </summary>
        private void InitControl()
        {
            this.fpOrderExecBrowser1.IsShowRowHeader = false;
            this.fpOrderExecBrowser2.IsShowRowHeader = false;
            this.TabControl1.SelectedIndex = 1;
            this.TabControl1.SelectedIndex = 0;

            this.fpOrderExecBrowser1.fpSpread.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread_CellDoubleClick);
            this.fpOrderExecBrowser2.fpSpread.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread_CellDoubleClick);

            this.fpOrderExecBrowser1.fpSpread.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpSpread_ButtonClicked);
            this.fpOrderExecBrowser2.fpSpread.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpSpread_ButtonClicked);

            fpOrderExecBrowser1.fpSpread.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread_CellClick);
            fpOrderExecBrowser2.fpSpread.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread_CellClick);

            //ȡСʱ�շ� {97FA5C9D-F454-4aba-9C36-8AF81B7C9CCF}
            frequencyID = CacheManager.ContrlManager.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.MetConstant.Hours_Frequency_ID, true);

            FS.HISFC.BizLogic.Manager.Constant myCnst = new FS.HISFC.BizLogic.Manager.Constant();

            #region ������

            ArrayList alItems = myCnst.GetAllList("USAGEDIVTIME");

            ArrayList alItemsUsage = myCnst.GetAllList("USAGE");

            FS.HISFC.Models.Base.Const None = new FS.HISFC.Models.Base.Const();
            None.ID = "NONE";
            None.Name = "��ҩƷͨ��";
            None.UserCode = "NONE";
            alItemsUsage.Add(None);

            foreach (FS.HISFC.Models.Base.Const cnst in alItemsUsage)
            {
                if (!hsUsageAndTime.Contains(cnst.ID))
                {
                    cnst.Memo = "";

                    if (cnst.UserCode.Length > 0)
                    {
                        foreach (FS.HISFC.Models.Base.Const usage in alItems)
                        {
                            if (usage.ID == cnst.UserCode)
                            {
                                cnst.Memo = usage.Memo;
                            }
                        }
                    }

                    hsUsageAndTime.Add(cnst.ID, cnst);
                }
            }
            #endregion

            if (IConfirmExecOrder == null)
            {
                IConfirmExecOrder = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IConfirmExecOrder)) as FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IConfirmExecOrder;
            }
            if (IsUseUnChkSpecial)
            {
                this.chkUnSp.Visible = true;
            }

            if (FS.FrameWork.WinForms.Classes.Function.IsManager())
            {
                txtDays.Enabled = true;
            }
            else
            {
                txtDays.Enabled = false;
            }
        }

        FarPoint.Win.Spread.CellClickEventArgs cellClickEvent = null;
        int curRow = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fpSpread_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            cellClickEvent = e;

            curRow = e.Row;

            ContextMenu menu = new ContextMenu();

            //ȡ���޸�ȡҩ���ҹ�����
            MenuItem mnuChangeDept = new MenuItem("�޸�ȡҩ����");//�޸�ȡҩ����
            mnuChangeDept.Click += new EventHandler(mnuChangeStokDept_Click);
            menu.MenuItems.Add(mnuChangeDept);

            //ȡ���޸�ȡҩ���ҹ�����
            MenuItem mnuChangeDeptALL = new MenuItem("�޸�����ѡ����ȡҩ����");//�޸�ȡҩ����
            mnuChangeDeptALL.Click += new EventHandler(mnuChangeDeptALL_Click);
            menu.MenuItems.Add(mnuChangeDeptALL);

            //this.fpSpread.ContextMenu = menu;
            fpOrderExecBrowser1.fpSpread.ContextMenu = menu;
        }

        private void ChangeStokDept(bool isAll)
        {
            if (this.TabControl1.SelectedTab == this.tabPage1)
            {
                int errCol = fpOrderExecBrowser1.fpSpread.Sheets[0].ColumnCount - 1;

                bool isAllChange = isAll;

                //if (this.fpOrderExecBrowser1.fpSpread.Sheets[0].Columns[errCol].Visible)
                //{
                //    if (MessageBox.Show("�Ƿ��޸�����ѡ���У�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //    {
                //        isAllChange = true;
                //    }
                //}

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                try
                {
                    if (isAllChange)
                    {
                        bool isShow = true;

                        FS.FrameWork.Models.NeuObject drugDept = null;
                        for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].RowCount; i++)
                        {
                            if (this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser1.ColumnIndexSelection].Text.ToUpper() == "TRUE")
                            {
                                FS.HISFC.Models.Order.ExecOrder execOrder = this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;

                                if (execOrder != null)
                                {
                                    if (isShow)
                                    {
                                        drugDept = ucChangeStoreDept.ChangeStoreDept(execOrder.Order);
                                        if (drugDept == null)
                                        {
                                            return;
                                        }
                                        isShow = false;
                                    }

                                    execOrder.Order.StockDept = drugDept;

                                    if (localOrderMgr.UpdateExecOrderStockDept(execOrder) == -1)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        Classes.Function.ShowBalloonTip(3, "����", localOrderMgr.Err, ToolTipIcon.Error);
                                        return;
                                    }

                                    this.fpOrderExecBrowser1.fpSpread.Sheets[0].SetValue(i, 11, SOC.HISFC.BizProcess.Cache.Common.GetDeptName(execOrder.Order.StockDept.ID), false);//ȡҩ����
                                    fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].Tag = execOrder;
                                }
                            }
                        }
                    }
                    else
                    {
                        FS.HISFC.Models.Order.ExecOrder execOrder = null;

                        int rowIndex = this.fpOrderExecBrowser1.fpSpread.Sheets[0].ActiveRowIndex;
                        execOrder = this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[rowIndex].Tag as FS.HISFC.Models.Order.ExecOrder;

                        if (execOrder != null)
                        {
                            FS.FrameWork.Models.NeuObject dept = ucChangeStoreDept.ChangeStoreDept(execOrder.Order);
                            if (dept == null)
                            {
                                return;
                            }

                            int fromRowIndex = rowIndex - 20;
                            int endRowIndex = rowIndex + 20;
                            if (fromRowIndex < 0)
                            {
                                fromRowIndex = 0;
                            }
                            if (endRowIndex > fpOrderExecBrowser1.fpSpread.Sheets[0].RowCount)
                            {
                                endRowIndex = fpOrderExecBrowser1.fpSpread.Sheets[0].RowCount;
                            }

                            for (int i = fromRowIndex; i < endRowIndex; i++)
                            {
                                FS.HISFC.Models.Order.ExecOrder inExecOrder = this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;
                                if (inExecOrder.Order.Combo.ID == execOrder.Order.Combo.ID
                                    && inExecOrder.DateUse == execOrder.DateUse)
                                {
                                    inExecOrder.Order.StockDept = dept;

                                    if (localOrderMgr.UpdateExecOrderStockDept(execOrder) == -1)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        Classes.Function.ShowBalloonTip(3, "����", localOrderMgr.Err, ToolTipIcon.Error);
                                        return;
                                    }

                                    this.fpOrderExecBrowser1.fpSpread.Sheets[0].SetValue(i, 11, SOC.HISFC.BizProcess.Cache.Common.GetDeptName(inExecOrder.Order.StockDept.ID), false);//ȡҩ����
                                    fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].Tag = inExecOrder;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Classes.Function.ShowBalloonTip(3, "����", ex.Message, ToolTipIcon.Error);
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
        }

        /// <summary>
        /// �޸�ִ�п����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuChangeStokDept_Click(object sender, EventArgs e)
        {
            ChangeStokDept(false);
        }

        /// <summary>
        /// �޸�ִ�п����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuChangeDeptALL_Click(object sender, EventArgs e)
        {
            ChangeStokDept(true);
        }

        /// <summary>
        /// ��ѯִ�е�--�ֽ�ҽ��
        /// </summary>
        /// <returns></returns>
        public int RefreshExec()
        {
            string DeptCode = ""; //����

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            ArrayList alOrders = null;
            FS.HISFC.BizProcess.Integrate.RADT pManager = new FS.HISFC.BizProcess.Integrate.RADT();
            try
            {
                #region �ֽ���˹���ҽ��
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڷֽ⣬���Ժ�...");
                Application.DoEvents();

                CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                pManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);


                //��ȡȫ����Ƶ��,��ÿ��ҽ��ʹ�ã�����ÿ���������ݿ�
                ArrayList alFrequency = new ArrayList();
                alFrequency = SOC.HISFC.BizProcess.Cache.Order.QueryFrequency();
                FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper(alFrequency);

                //�ֽ�Ĳ���ʱ��,����ֽ�ʱÿ��ҽ������ѯϵͳʱ��
                DateTime dt = new DateTime();
                dt = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();
                FS.HISFC.Models.RADT.PatientInfo patientInfo = null;
                string errInfo = "";

                //��ÿ�����ߵ�ҽ���ֽ�
                for (int i = 0; i < this.alPatients.Count; i++)
                {
                    //������Ϣ
                    patientInfo = alPatients[i] as FS.HISFC.Models.RADT.PatientInfo;
                    if (patientInfo == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("��ȡ���߻�����Ϣ����������ϢΪ�գ�");
                        return -1;
                    }
                    else if (Classes.Function.CheckPatientState(patientInfo.ID, ref patientInfo, ref errInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show(errInfo);
                        return -1;
                    }

                    //��ҽ�������ڼ���ҽ��״̬Ϊ1 �� 2��ҽ��
                    alOrders = CacheManager.InOrderMgr.QueryValidOrderWithSubtbl(patientInfo.ID, FS.HISFC.Models.Order.EnumType.LONG);
                    if (alOrders == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show(CacheManager.InOrderMgr.Err);
                        return -1;
                    }

                    FS.HISFC.Models.Order.Inpatient.Order order = null;

                    #region �ֽ��������

                    CacheManager.InOrderMgr.AlAllOrders = alOrders;
                    CacheManager.InOrderMgr.HsUsageAndTime = this.hsUsageAndTime;

                    #endregion

                    //�ֽ⻼�ߵ�ҽ��
                    for (int j = 0; j < alOrders.Count; j++)
                    {
                        order = (FS.HISFC.Models.Order.Inpatient.Order)alOrders[j];

                        #region ���Ŀ���

                        DeptCode = order.ReciptDept.ID;//��������
                        //ҽ��ʵ���еĻ�����Ժ�������¸�ֵ
                        order.Patient.PVisit.PatientLocation.Dept.ID = patientInfo.PVisit.PatientLocation.Dept.ID;
                        order.Patient.PVisit.PatientLocation.NurseCell.ID = patientInfo.PVisit.PatientLocation.NurseCell.ID;

                        #endregion

                        #region ��ȡƵ��ʱ���

                        //���Ϊ����Ƶ�ξ͸�Ƶ�θ�ֵ������Ϊԭ����Ƶ��
                        FS.HISFC.Models.Order.Frequency f = (FS.HISFC.Models.Order.Frequency)helper.GetObjectFromID(order.Frequency.ID)
                            as FS.HISFC.Models.Order.Frequency;
                        if (order.Frequency.Times.Length == f.Times.Length && order.Frequency.Time != f.Time)
                        {
                            //����Ƶ��
                        }
                        else
                        {
                            order.Frequency = (FS.HISFC.Models.Order.Frequency)helper.GetObjectFromID(order.Frequency.ID).Clone();

                            if (order.Frequency == null)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                MessageBox.Show(CacheManager.InOrderMgr.Err);
                                return -1;
                            }
                        }

                        #endregion

                        //{97FA5C9D-F454-4aba-9C36-8AF81B7C9CCF}
                        //Сʱ�շ�ҽ�����ֽ�ʱ�䵽��ǰʱ��
                        if (order.Frequency.ID == frequencyID)
                        {
                            if (CacheManager.InOrderMgr.DecomposeOrderToNow(order, 0, false, dt) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                MessageBox.Show(CacheManager.InOrderMgr.Err);
                                return -1;
                            }
                        }
                        else
                        {
                            //��ҽ�����зֽ�
                            if (CacheManager.InOrderMgr.DecomposeOrder(order, this.intDyas, false, dt) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                MessageBox.Show(CacheManager.InOrderMgr.Err);
                                return -1;
                            }
                        }
                    }
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                #endregion
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                //MessageBox.Show("�ֽ����" + ex.Message + CacheManager.InOrderMgr.iNum.ToString());
                MessageBox.Show("�ֽ����" + ex.Message);
                return -1;
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            this.RefreshQuery();
            return 0;
        }

        /// <summary>
        /// ���²�ѯ��ʾ
        /// </summary>
        /// <returns></returns>
        protected int RefreshQuery()
        {
            if (bOnQuery)
            {
                return 0;
            }

            this.chkAll.Checked = false;

            if (this.chkUnSp.Visible)
            {
                this.chkUnSp.Checked = false;
            }

            bOnQuery = true;
            try
            {
                this.txtName.Items.Clear();
                ArrayList alOrders = null;

                #region ��ѯ��ʾ
                this.fpOrderExecBrowser1.Clear();
                this.fpOrderExecBrowser2.Clear();

                FS.FrameWork.Public.ObjectHelper orderNameHlpr = new FS.FrameWork.Public.ObjectHelper();
                FS.FrameWork.Public.ObjectHelper deptNameHlpr = new FS.FrameWork.Public.ObjectHelper();
                FS.FrameWork.Models.NeuObject objTmp = new FS.FrameWork.Models.NeuObject();

                for (int i = 0; i < this.alPatients.Count; i++)
                {
                    FS.HISFC.Models.RADT.PatientInfo p = this.alPatients[i] as FS.HISFC.Models.RADT.PatientInfo;
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ" + p.Name + "�����Ժ�...");

                    #region ��ѯ�ֽ����

                    alOrders = CacheManager.InOrderMgr.QueryExecOrderIsExec(p.ID, "1", false);//��ѯȷ�ϵ�ҩƷ 

                    if (IConfirmExecOrder != null)
                    {
                        if (alOrders.Count > 0)
                        {
                            if (IConfirmExecOrder.AfterRefreshExecOrder(ref alOrders) == -1)
                            {
                                FS.HISFC.Models.RADT.PatientInfo pInfo = ((FS.HISFC.Models.Order.Inpatient.Order)alOrders[0]).Patient;
                                MessageBox.Show(pInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� " + pInfo.Name + " ����ֽ�ӿ�AfterRefreshExecOrder����\r\n" + IConfirmExecOrder.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            }
                        }
                    }

                    for (int j = 0; j < alOrders.Count; j++)
                    {
                        FS.HISFC.Models.Order.ExecOrder order = alOrders[j] as FS.HISFC.Models.Order.ExecOrder;
                        order.Order.Patient = p;

                        string drugDept = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(order.Order.StockDept.ID);

                        if (order.Order.OrderType.IsDecompose)
                        {
                            this.fpOrderExecBrowser1.AddRow(order);
                            objTmp = new FS.FrameWork.Models.NeuObject(order.Order.Item.Name, order.Order.Item.Name, "");
                            if (orderNameHlpr.GetObjectFromID(objTmp.ID) == null)
                            {
                                orderNameHlpr.ArrayObject.Add(objTmp);
                            }

                            if (!string.IsNullOrEmpty(drugDept))
                            {
                                objTmp = new FS.FrameWork.Models.NeuObject(drugDept, drugDept, "");
                                if (deptNameHlpr.GetObjectFromID(objTmp.ID) == null)
                                {
                                    deptNameHlpr.ArrayObject.Add(objTmp);
                                }
                            }

                        }
                    }
                    //��ҩƷ
                    alOrders = CacheManager.InOrderMgr.QueryExecOrderIsExec(p.ID, "2", false);//��ѯδִ�еķ�ҩƷ
                    for (int j = 0; j < alOrders.Count; j++)
                    {
                        FS.HISFC.Models.Order.ExecOrder order = alOrders[j] as FS.HISFC.Models.Order.ExecOrder;
                        order.Order.Patient = p;
                        //��ʾ��Ҫ��ʿվȷ�ϵķ�ҩƷ
                        //if (//((FS.HISFC.Models.Fee.Item.Undrug)order.Order.Item).IsNeedConfirm == false ||
                        //    order.Order.ExeDept.ID == order.Order.ReciptDept.ID ||
                        //    order.Order.ExeDept.ID == NurseStation.ID) //��ʿվ�շѻ���ִ�п��ң�������  
                        //{
                            if (order.Order.OrderType.IsDecompose) //����ҽ��
                            {
                                this.fpOrderExecBrowser2.AddRow(order);
                                objTmp = new FS.FrameWork.Models.NeuObject(order.Order.Item.Name, order.Order.Item.Name, "");
                                if (orderNameHlpr.GetObjectFromID(objTmp.ID) == null)
                                {
                                    orderNameHlpr.ArrayObject.Add(objTmp);
                                }
                            }
                        //}
                    }

                    #endregion

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }
                #endregion

                objTmp = new FS.FrameWork.Models.NeuObject("ALL", "ȫ��", "");
                orderNameHlpr.ArrayObject.Insert(0, objTmp);

                txtName.SelectedIndexChanged -= new EventHandler(txtName_SelectedIndexChanged);
                this.txtName.AddItems(orderNameHlpr.ArrayObject);
                this.txtName.Tag = "ALL";
                txtName.SelectedIndexChanged += new EventHandler(txtName_SelectedIndexChanged);


                objTmp = new FS.FrameWork.Models.NeuObject("ALL", "ȫ��", "");
                deptNameHlpr.ArrayObject.Insert(0, objTmp);

                txtDrugDeptName.SelectedIndexChanged -= new EventHandler(txtDrugDeptName_SelectedIndexChanged);
                this.txtDrugDeptName.AddItems(deptNameHlpr.ArrayObject);
                this.txtDrugDeptName.Tag = "ALL";
                txtDrugDeptName.SelectedIndexChanged += new EventHandler(txtDrugDeptName_SelectedIndexChanged);

                this.fpOrderExecBrowser1.RefreshComboNo();
                this.fpOrderExecBrowser2.RefreshComboNo();

                this.fpOrderExecBrowser1.ShowOverdueOrder();
                this.fpOrderExecBrowser2.ShowOverdueOrder();

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                //��ʿ���¼���ʱ���ȼ���2�� Ȼ���ڼ���1���ʱ�� ɾ����������ݣ�������ɾ����
                this.fpOrderExecBrowser1.DeleteRow(this.intDyas);
                this.fpOrderExecBrowser2.DeleteRow(this.intDyas);
                this.tabPage1.Text = "ҩƷ" + "��" + this.fpOrderExecBrowser1.GetFpRowCount(0).ToString() + "����";
                this.tabPage2.Text = "��ҩƷ" + "��" + this.fpOrderExecBrowser2.GetFpRowCount(0).ToString() + "����";
            }
            catch
            {
            }
            bOnQuery = false;

            return 0;
        }

        /// <summary>
        /// ��ʿվ����
        /// </summary>
        protected FS.FrameWork.Models.NeuObject NurseStation
        {
            get
            {
                return ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.Clone();
            }
        }

        /// <summary>
        /// ���龯���ߣ������ó�����Ϊ��ҩƷ�ͷ�ҩƷ�ķ���һ����ʾ
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="messType"></param>
        /// <param name="isDrug">��ǰ�б��Ƿ�ΪҩƷҽ���б�</param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        private int CheckMoneyAlert(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.MessType messType, bool isDrug, ArrayList alOrders)
        {
            //�����б�Ϊ�˼���ѭ����ʱ


            try
            {
                FS.HISFC.Models.Order.ExecOrder order = null;

                if (isDrug)
                {
                    #region ��ҩƷ

                    for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].RowCount; i++)
                    {
                        if (this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser2.ColumnIndexSelection].Text.ToUpper() == "TRUE")
                        {
                            order = this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;
                            if (patientInfo.ID == order.Order.Patient.ID)
                            {
                                alOrders.Add(order);
                            }

                        }
                    }

                    #endregion
                }
                else
                {
                    #region ҩƷ

                    for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].RowCount; i++)
                    {
                        if (this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser1.ColumnIndexSelection].Text.ToUpper() == "TRUE")
                        {
                            order = this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;

                            if (patientInfo.ID == order.Order.Patient.ID)
                            {
                                alOrders.Add(order);
                            }

                        }
                    }

                    #endregion
                }

                if (Classes.Function.CheckMoneyAlert(patientInfo, alOrders, messType) == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return -1;
                }

                return 1;
            }
            catch (Exception ex)
            {
                Classes.Function.ShowBalloonTip(3, ex.Message, "����", ToolTipIcon.Info);
                return 1;
            }
        }

        Classes.LocalOrderManager localOrderMgr = new FS.HISFC.Components.Order.Classes.LocalOrderManager();


        /// <summary>
        /// �����ⲻ�ܱ������Ϻ�+ʹ��ʱ��
        /// </summary>
        private Dictionary<string, Dictionary<string, string>> dicStopCombNo = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// Ƿ����ʾ��Ϣ
        /// </summary>
        string lackFeeInfo = "";

        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <returns></returns>
        public int ComfirmExec()
        {
            if (FS.FrameWork.WinForms.Classes.Function.Msg("�Ƿ�ȷ��Ҫ���棿", 422) == DialogResult.No)
            {
                return -1;
            }
            this.btnSave.Enabled = false;
            FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();
            FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڱ������ݣ����Ժ�...");
            Application.DoEvents();
            dt = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();

            FS.HISFC.Models.Order.ExecOrder order = null;
            string inpatientNo = "";
            List<FS.HISFC.Models.Order.ExecOrder> alOrders = null;
            FS.HISFC.Models.RADT.PatientInfo patient = null;

            //{B2E4E2ED-08CF-41a8-BF68-B9DF7454F9BB} Ƿ���ж�
            FS.HISFC.Models.Base.MessType messType = FS.HISFC.Models.Base.MessType.M;
            switch (Ƿ�Ѳ���)
            {
                case EnumLackFee.���ж�Ƿ��:
                    messType = FS.HISFC.Models.Base.MessType.N;
                    break;
                case EnumLackFee.Ƿ�Ѳ�����ֽ�:
                    messType = FS.HISFC.Models.Base.MessType.Y;
                    break;
                case EnumLackFee.Ƿ����ʾ�ʲ�����ֽ�:
                    messType = FS.HISFC.Models.Base.MessType.M;
                    break;
            }

            orderIntegrate.MessageType = messType;

            lackFeeInfo = "";
            string errInfo = "";

            ///�Ƿ�Ƿ��
            bool isLackFee = false;

            //�����ⲻ�ܱ������Ϻ�+ʹ��ʱ��
            dicStopCombNo.Clear();

            Hashtable hsPatientID = new Hashtable();

            localOrderMgr.DicDrugQty.Clear();

            string stockDeptID = string.Empty;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            #region ҩƷ

            for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].RowCount; i++)
            {
                if (this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser1.ColumnIndexSelection].Text.ToUpper() == "TRUE")
                {
                    order = this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;
                    stockDeptID = order.Order.StockDept.ID;

                    string combNo = order.Order.Combo.ID + order.DateUse.ToString() + "ҩ";
                    if (inpatientNo != order.Order.Patient.ID)
                    {
                        if (patient != null) //��һ������
                        {
                            for (int index = alOrders.Count - 1; index >= 0; index--)
                            {
                                FS.HISFC.Models.Order.ExecOrder execOrder = alOrders[index] as FS.HISFC.Models.Order.ExecOrder;
                                string combNoTemp = execOrder.Order.Combo.ID + execOrder.DateUse.ToString() + "ҩ";
                                if (dicStopCombNo.ContainsKey(combNoTemp))
                                {
                                    alOrders.Remove(execOrder);
                                }
                            }

                            if (!hsPatientID.Contains(patient.ID))
                            {
                                if (CheckMoneyAlert(patient, messType, true, new ArrayList(alOrders)) == -1)
                                {
                                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                    return -1;
                                }
                                hsPatientID.Add(patient.ID, patient);
                            }

                            FS.FrameWork.Management.PublicTrans.BeginTransaction();
                            if (orderIntegrate.ComfirmExec(patient, alOrders, NurseStation.ID, dt, true, !isLackFee, false) == -1)
                            {
                                orderIntegrate.fee.Rollback();
                                this.btnSave.Enabled = true;

                                //FS.FrameWork.Management.PublicTrans.RollBack();
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                                //�����ʾ��ĳ�����߳����Ժ󣬿�ѡ���Ƿ�����������˷ֽ�
                                //MessageBox.Show(orderIntegrate.Err);
                                //return -1;
                                if (this.isSaveErrContinue)
                                {
                                    if (MessageBox.Show(FS.FrameWork.Management.Language.Msg(
                                        string.Format("{0}.�Ƿ����ִ�зֽ��������ߵĲ�����", orderIntegrate.Err)), "��ʾ", MessageBoxButtons.YesNo) == DialogResult.No)
                                    {
                                        return -1;
                                    }
                                    this.btnSave.Enabled = true;

                                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڱ������ݣ����Ժ�...");
                                    Application.DoEvents();
                                }
                                else
                                {
                                    MessageBox.Show(orderIntegrate.Err);
                                    return -1;
                                }
                            }
                            //}{B3173852-136F-4c4b-9FAC-E15EB879C619}����������Ū��}��֪��Ϊʲô����ôдҪ������
                            else
                            {
                                FS.FrameWork.Management.PublicTrans.Commit();
                            }
                        }

                        inpatientNo = order.Order.Patient.ID;
                        if (Classes.Function.CheckPatientState(inpatientNo, ref patient, ref errInfo) == -1)
                        {
                            MessageBox.Show(errInfo);
                            return -1;
                        }

                        isLackFee = false;
                        //Ƿ����ʾ
                        if (CacheManager.FeeIntegrate.IsPatientLackFee(patient))
                        {
                            if (!lackFeeInfo.Contains(patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "��  " + patient.Name))
                            {
                                lackFeeInfo += "\r\n" + patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "��  " + patient.Name;
                            }
                            if (!lackFeeDealModel)
                            {
                                isLackFee = true;
                            }
                        }

                        alOrders = new List<FS.HISFC.Models.Order.ExecOrder>();
                    }

                    if (localOrderMgr.CheckOrder(order.Order, true) == -1)
                    {
                        if (!dicStopCombNo.ContainsKey(combNo))
                        {
                            Dictionary<string, string> hs = new Dictionary<string, string>();
                            hs.Add(order.ID, localOrderMgr.Err);
                            dicStopCombNo.Add(combNo, hs);
                        }
                        else
                        {
                            dicStopCombNo[combNo].Add(order.ID, localOrderMgr.Err);
                        }
                        int errCol = fpOrderExecBrowser1.fpSpread.Sheets[0].ColumnCount - 1;
                        fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, errCol].Text = localOrderMgr.Err;
                        fpOrderExecBrowser1.fpSpread.Sheets[0].Columns[errCol].Visible = true;
                        fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, errCol].ForeColor = Color.Red;

                        continue;
                    }
                    if (dicStopCombNo.ContainsKey(combNo))
                    {
                        continue;
                    }

                    alOrders.Add(order);
                }
            }

            #region ���һ������
            if (patient != null)
            {
                for (int index = alOrders.Count - 1; index >= 0; index--)
                {
                    FS.HISFC.Models.Order.ExecOrder execOrder = alOrders[index] as FS.HISFC.Models.Order.ExecOrder;

                    string combNoTemp = execOrder.Order.Combo.ID + execOrder.DateUse.ToString() + "ҩ";
                    if (dicStopCombNo.ContainsKey(combNoTemp))
                    {
                        alOrders.Remove(execOrder);
                    }
                }

                if (!hsPatientID.Contains(patient.ID))
                {
                    if (CheckMoneyAlert(patient, messType, true, new ArrayList(alOrders)) == -1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        return -1;
                    }
                    hsPatientID.Add(patient.ID, patient);
                }


                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                if (orderIntegrate.ComfirmExec(patient, alOrders, NurseStation.ID, dt, true, !isLackFee, false) == -1)
                {
                    orderIntegrate.fee.Rollback();
                    this.btnSave.Enabled = true;
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show(orderIntegrate.Err);
                    return -1;
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
            }
            #endregion

            #endregion

            #region ��ҩƷ

            alOrders = new List<FS.HISFC.Models.Order.ExecOrder>();
            patient = null;
            inpatientNo = "";
            for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].RowCount; i++)
            {
                if (this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser2.ColumnIndexSelection].Text.ToUpper() == "TRUE")
                {
                    order = this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;
                    string combNo = order.Order.Combo.ID + order.DateUse.ToString() + "��ҩ";
                    if (inpatientNo != order.Order.Patient.ID)
                    {
                        if (patient != null) //��һ������
                        {
                            for (int index = alOrders.Count - 1; index >= 0; index--)
                            {
                                FS.HISFC.Models.Order.ExecOrder execOrder = alOrders[index] as FS.HISFC.Models.Order.ExecOrder;
                                string combNoTemp = execOrder.Order.Combo.ID + execOrder.DateUse.ToString() + "��ҩ";
                                if (dicStopCombNo.ContainsKey(combNoTemp))
                                {
                                    alOrders.Remove(execOrder);
                                }
                            }

                            if (!hsPatientID.Contains(patient.ID))
                            {
                                if (CheckMoneyAlert(patient, messType, false, new ArrayList(alOrders)) == -1)
                                {
                                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                    return -1;
                                }
                                hsPatientID.Add(patient.ID, patient);
                            }

                            FS.FrameWork.Management.PublicTrans.BeginTransaction();
                            if (orderIntegrate.ComfirmExec(patient, alOrders, NurseStation.ID, dt, false, !isLackFee, false) == -1)
                            {
                                orderIntegrate.fee.Rollback();
                                this.btnSave.Enabled = true;
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                                //�����ʾ��ĳ�����߳����Ժ󣬿�ѡ���Ƿ�����������˷ֽ�
                                //MessageBox.Show(orderIntegrate.Err);
                                //return -1;
                                if (this.isSaveErrContinue)
                                {
                                    if (MessageBox.Show(FS.FrameWork.Management.Language.Msg(
                                        string.Format("{0}.�Ƿ����ִ�зֽ��������ߵĲ�����", orderIntegrate.Err)), "��ʾ", MessageBoxButtons.YesNo) == DialogResult.No)
                                    {
                                        return -1;
                                    }
                                    this.btnSave.Enabled = false;
                                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڱ������ݣ����Ժ�...");
                                    Application.DoEvents();
                                }
                                else
                                {
                                    MessageBox.Show(orderIntegrate.Err);
                                    return -1;
                                }
                            }
                            else
                            {
                                FS.FrameWork.Management.PublicTrans.Commit();
                            }
                        }
                        inpatientNo = order.Order.Patient.ID;
                        //patient = radtIntegrate.GetPatientInfomation(inpatientNo);
                        if (Classes.Function.CheckPatientState(inpatientNo, ref patient, ref errInfo) == -1)
                        {
                            //FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show(errInfo);
                            return -1;
                        }

                        isLackFee = false;

                        //Ƿ����ʾ
                        if (CacheManager.FeeIntegrate.IsPatientLackFee(patient))
                        {
                            if (!lackFeeInfo.Contains(patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "��  " + patient.Name))
                            {
                                lackFeeInfo += "\r\n" + patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "��  " + patient.Name;
                            }

                            if (!lackFeeDealModel)
                            {
                                isLackFee = true;
                            }
                        }

                        alOrders = new List<FS.HISFC.Models.Order.ExecOrder>();

                    }

                    if (localOrderMgr.CheckOrder(order.Order, true) == -1)
                    {
                        if (!dicStopCombNo.ContainsKey(combNo))
                        {
                            Dictionary<string, string> hs = new Dictionary<string, string>();
                            hs.Add(order.ID, localOrderMgr.Err);
                            dicStopCombNo.Add(combNo, hs);
                        }
                        else
                        {
                            dicStopCombNo[combNo].Add(order.ID, localOrderMgr.Err);
                        }
                        int errCol = fpOrderExecBrowser2.fpSpread.Sheets[0].ColumnCount - 1;
                        fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, errCol].Text = localOrderMgr.Err;
                        fpOrderExecBrowser2.fpSpread.Sheets[0].Columns[errCol].Visible = true;
                        fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, errCol].ForeColor = Color.Red;

                        continue;
                    }
                    if (dicStopCombNo.ContainsKey(combNo))
                    {
                        continue;
                    }

                    alOrders.Add(order);
                }
            }
            if (patient != null) //��һ������
            {
                for (int index = alOrders.Count - 1; index >= 0; index--)
                {
                    FS.HISFC.Models.Order.ExecOrder execOrder = alOrders[index] as FS.HISFC.Models.Order.ExecOrder;
                    string combNoTemp = execOrder.Order.Combo.ID + execOrder.DateUse.ToString() + execOrder.ID + "��ҩ";
                    if (dicStopCombNo.ContainsKey(combNoTemp))
                    {
                        alOrders.Remove(execOrder);
                    }
                }

                if (!hsPatientID.Contains(patient.ID))
                {
                    if (CheckMoneyAlert(patient, messType, false, new ArrayList(alOrders)) == -1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        return -1;
                    }
                    hsPatientID.Add(patient.ID, patient);
                }


                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                if (orderIntegrate.ComfirmExec(patient, alOrders, NurseStation.ID, dt, false, !isLackFee, false) == -1)
                {
                    orderIntegrate.fee.Rollback();
                    this.btnSave.Enabled = true;
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    //FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(orderIntegrate.Err);
                    return -1;
                }
                else
                {
                    //orderIntegrate.fee.Commit();
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
            }

            #endregion

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            bOnQuery = false;

            //if (!string.IsNullOrEmpty(lackFeeInfo))
            //{
            //    if (messType == FS.HISFC.Models.Base.MessType.N && !lackFeeDealModel)
            //    {
            //        MessageBox.Show("���»��ߴ���Ƿ�������\r\n" + lackFeeInfo, "Ƿ����ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    else
            //    {                    
            //        Classes.Function.ShowBalloonTip(10, "Ƿ����ʾ", "���»��ߴ���Ƿ�������\r\n" + lackFeeInfo, System.Windows.Forms.ToolTipIcon.Info);
            //    }
            //}

            this.RefreshQuery();

            #region ҩ���̵���ʾ
            if (!string.IsNullOrEmpty(stockDeptID))
            {
                string strSql = @"select * from pha_com_checkstatic t where t.drug_dept_code = '{0}' and t.foper_time <= sysdate and t.check_state = '0'";
                strSql = string.Format(strSql,stockDeptID);
                if (FS.FrameWork.Function.NConvert.ToInt32(CacheManager.InOrderMgr.ExecSqlReturnOne(strSql)) > 0)
                {
                    MessageBox.Show("��ܰ��ʾ,ҩ���̵��ڼ䣬������ʹ��ҩƷ�����ݻ���ҩ��ȡҩ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion

            return 0;
        }

        /// <summary>
        /// ѡ�������Ŀ
        /// {ED1068B5-53FD-4bf4-A270-49AE1A70D225}
        /// </summary>
        private void CheckFilteredData()
        {
            for (int i = 0; i < this.CurrentBrowser.fpSpread.Sheets[0].Rows.Count; i++)
            {
                if (this.CurrentBrowser.fpSpread.Sheets[0].Rows[i].BackColor == Color.LightSkyBlue)
                {
                    FS.HISFC.Models.Order.ExecOrder order = new FS.HISFC.Models.Order.ExecOrder();
                    order = this.CurrentBrowser.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;
                    if (order.Order.Combo.ID != "" && order.Order.Combo.ID != "0")
                    {
                        //����Ƚ��������ͬ����Էֿ����˴���Ҫ
                        for (int j = this.CurrentBrowser.fpSpread.Sheets[0].RowCount - 1; j >= 0; j--)
                        {
                            FS.HISFC.Models.Order.ExecOrder objorder = (FS.HISFC.Models.Order.ExecOrder)this.CurrentBrowser.fpSpread.Sheets[0].Rows[j].Tag;
                            if (objorder.Order.Combo.ID == order.Order.Combo.ID && objorder.DateUse == order.DateUse)
                            {
                                this.CurrentBrowser.fpSpread.Sheets[0].Cells[j, 1].Value = true;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ������Ϲ��˺���
        /// {ED1068B5-53FD-4bf4-A270-49AE1A70D225}
        /// </summary>
        /// <param name="isMatchAll"></param>
        /// <param name="prmsDate">ִ�п�ʼ������ʱ��</param>
        /// <param name="prmsStr">���ơ�ҩ��</param>
        protected void SetFilteredFlag(bool isMatchAll, DateTime[] prmsDate, string[] prmsStr)
        {
            bool isHaveFilter = true;
            bool isAllOrderNames = (prmsStr[0] == "ȫ��");
            bool isAllDeptNames = (prmsStr[1] == "ȫ��");
            bool isAllTime = (prmsDate[0].ToString() == prmsDate[1].ToString());
            if (isAllOrderNames && isAllDeptNames && isAllTime)
            {
                isHaveFilter = false;
            }

            FS.HISFC.Models.Order.ExecOrder order = null;
            //��ʼ����ʾ��ҩƷ
            if (this.TabControl1.SelectedIndex == 0)
            {
                bool b = false;
                //�ָ�ԭ������ɫ
                //�����ɫ��ʾ
                //for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
                //{
                //    if (b)
                //    {
                //        this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.Linen;
                //    }
                //    else
                //    {
                //        this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.White;
                //    }
                //    b = !b;
                //}
                //ҩƷ����ȡҩ���ҷֿ���ʾ��ɫ
                for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    FS.HISFC.Models.Order.ExecOrder execOrder = this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;
                    string deptCode = execOrder.Order.StockDept.ID;
                    //if ("3002".Equals(deptCode))
                    //{
                    //    this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.BlanchedAlmond;
                    //}
                    //else if ("3004".Equals(deptCode))
                    //{
                    //    this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.White;
                    //}
                }
                if (isHaveFilter)
                {
                    for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
                    {
                        order = this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;
                        DateTime splitTime = FS.FrameWork.Function.NConvert.ToDateTime(this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 9].Text);
                        if (isMatchAll)
                        {
                            if ((isAllOrderNames || this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 2].Text == prmsStr[0])
                             && (isAllDeptNames || this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 11].Text == prmsStr[1])
                             && (isAllTime || (splitTime >= prmsDate[0] && splitTime <= prmsDate[1])))
                            {
                                this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                            }
                        }
                        else
                        {
                            if ((isAllOrderNames || this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 2].Text.Contains(prmsStr[0]))
                             && (isAllDeptNames || this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 11].Text.Contains(prmsStr[1]))
                             && (isAllTime || (splitTime >= prmsDate[0] && splitTime <= prmsDate[1])))
                            {
                                this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                            }
                        }
                    }
                }
                else
                {
                    this.fpOrderExecBrowser1.ShowOverdueOrder();
                }
            }
            //��ҩƷ
            else
            {
                //�ָ�ԭ������ɫ
                bool b = false;
                //�ָ�ԭ������ɫ
                //�����ɫ��ʾ
                for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    if (b)
                    {
                        this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.Linen;
                    }
                    else
                    {
                        this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.White;
                    }
                    b = !b;
                }
                if (isHaveFilter)
                {
                    for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows.Count; i++)
                    {
                        DateTime splitTime = FS.FrameWork.Function.NConvert.ToDateTime(this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, 9].Text);
                        if (isMatchAll)
                        {
                            if ((isAllOrderNames || this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, 2].Text == prmsStr[0])
                             && (isAllTime || (splitTime >= prmsDate[0] && splitTime <= prmsDate[1])))
                            {
                                this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                            }
                        }
                        else
                        {
                            if ((isAllOrderNames || this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, 2].Text.Contains(prmsStr[0]))
                             && (isAllTime || (splitTime >= prmsDate[0] && splitTime <= prmsDate[1])))
                            {
                                this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                            }
                        }
                    }
                }
                else
                {
                    this.fpOrderExecBrowser2.ShowOverdueOrder();
                }
            }
        }

        #endregion

        #region �¼�
        //���ֽ�ҽ��
        private void fpSpread_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.HISFC.Models.Order.ExecOrder order = null;
            order = this.CurrentBrowser.CurrentExecOrder;

            if (order == null) 
            {return;
            }
            string specs = "";
            if(order.Order.Item.ItemType==FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                specs = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Order.Item.ID).Specs;
            }
            else
            {
                specs = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Order.Item.ID).Specs;
            }

            if (MessageBox.Show("ȷ�ϲ��ֽ�" + order.DateUse.ToString() + "��ҽ�� " + order.Order.Item.Name + "[" + specs + "] ?", "��ʾ", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            //����ִ�е�ҽ��Ϊ�����
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(CacheManager.InOrderMgr.Connection);
            //t.BeginTransaction();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj = CacheManager.InOrderMgr.Operator;
            ArrayList alDel = new ArrayList();
            if (order.Order.Combo.ID == "" || order.Order.Combo.ID == "0")
            {
                if (CacheManager.InOrderMgr.DcExecImmediate((FS.HISFC.Models.Order.Order)order.Order, obj) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                    MessageBox.Show(CacheManager.InOrderMgr.Err);
                    return;
                }
                this.CurrentBrowser.fpSpread.Sheets[0].Rows.Remove(this.fpOrderExecBrowser1.fpSpread.Sheets[0].ActiveRowIndex, 1);
            }
            else //���ҽ����������Ϻ���ͬ��ʹ��ʱ����ͬ��ҽ��
            {
                for (int i = this.CurrentBrowser.fpSpread.Sheets[0].RowCount - 1; i >= 0; i--)
                {
                    FS.HISFC.Models.Order.ExecOrder objorder = (FS.HISFC.Models.Order.ExecOrder)this.CurrentBrowser.fpSpread.Sheets[0].Rows[i].Tag;
                    if (objorder.Order.Combo.ID == order.Order.Combo.ID && objorder.DateUse == order.DateUse)
                    {
                        if (CacheManager.InOrderMgr.DcExecImmediate(objorder, obj) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            MessageBox.Show(CacheManager.InOrderMgr.Err);
                            return;
                        }
                        //alDel.Add(i);
                        this.CurrentBrowser.fpSpread.Sheets[0].Rows.Remove(i, 1);
                    }
                }
            }


            FS.FrameWork.Management.PublicTrans.Commit();
            this.tabPage1.Text = "��" + this.fpOrderExecBrowser1.GetFpRowCount(0).ToString() + "����";
            this.tabPage2.Text = "��" + this.fpOrderExecBrowser2.GetFpRowCount(0).ToString() + "����";

        }
        /// <summary>
        /// ���ص�ǰ��FpSpreadҳ
        /// </summary>
        protected fpOrderExecBrowser CurrentBrowser
        {
            get
            {
                if (this.TabControl1.SelectedIndex == 0)
                {
                    return this.fpOrderExecBrowser1;
                }
                else
                {
                    return this.fpOrderExecBrowser2;
                }
            }
        }


        /// <summary>
        /// ���¼��㰴ť�¼� �Ѿ�����ʹ�� Ĭ��Ϊ1��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCaculate_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("ȷ��Ҫ���¼��㲢�ֽ�ҽ����?\n������Ҫһ��ʱ�䣡", "��ʾ", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.intDyas = (int)(this.txtDays.Value);
                RefreshExec();
            }
        }

        /// <summary>
        /// fpSpread_ButtonClicked�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (this.CurrentBrowser.fpSpread.Sheets[0].RowCount <= 0)
            {
                return;
            }
            if (e.Column == 1)
            {
                string checkValue = this.CurrentBrowser.fpSpread.Sheets[0].Cells[e.Row, e.Column].Text;

                FS.HISFC.Models.Order.ExecOrder order = new FS.HISFC.Models.Order.ExecOrder();
                order = this.CurrentBrowser.CurrentExecOrder;
                if (order.Order.Combo.ID != "" && order.Order.Combo.ID != "0")
                {
                    for (int i = this.CurrentBrowser.fpSpread.Sheets[0].RowCount - 1; i >= 0; i--)
                    {
                        FS.HISFC.Models.Order.ExecOrder objorder = (FS.HISFC.Models.Order.ExecOrder)this.CurrentBrowser.fpSpread.Sheets[0].Rows[i].Tag;
                        if (objorder.Order.Combo.ID == order.Order.Combo.ID && objorder.DateUse == order.DateUse)
                        {
                            this.CurrentBrowser.fpSpread.Sheets[0].Cells[i, 1].Text = checkValue;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// �����ѯ������,��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //�س�����
            if (e.KeyCode != Keys.Enter)
                return;
            string name = this.txtName.Text.Trim();
            if (name == "") return;

            this.SetDrugFlag(name, false);
        }

        /// <summary>
        /// ȫѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            bool b = false;
            if (this.chkAll.Checked)
            { //ȫѡ
                b = true;
                if (this.chkUnSp.Visible)
                {
                    this.chkUnSp.Checked = false;
                }
            }
            else
            {//ȡ��
                b = false;
            }

            //������©��Ĭ��ȫ��ѡ��

            for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
            {
                this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser1.ColumnIndexSelection].Value = b;
                tab0AllSelect = b;
            }

            for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows.Count; i++)
            {
                this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser2.ColumnIndexSelection].Value = b;
                tab1AllSelect = b;
            }
            /*
            if (this.TabControl1.SelectedIndex == 0)
            {
                for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser1.ColumnIndexSelection].Value = b;
                    tab0AllSelect = b;
                }
            }
            else
            {
                for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser2.ColumnIndexSelection].Value = b;
                    tab1AllSelect = b;
                }
            }
             * */
        }

        /// <summary>
        /// ��ѡ����ҩ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkUnSp_CheckedChanged(object sender, EventArgs e)
        {
            ArrayList alComb = new ArrayList();
            if (this.chkUnSp.Visible)
            {
                if (this.chkUnSp.Checked == true)
                {
                    for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
                    {
                        string temp = this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 12].Text;
                        if (!string.IsNullOrEmpty(temp))
                        {
                            string comb = this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 3].Text;
                            alComb.Add(comb);
                        }
                    }
                    for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
                    {
                        string tempComb = this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 3].Text;
                        if (alComb.Contains(tempComb))
                        {
                            this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser1.ColumnIndexSelection].Value = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ���¼���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnCalculate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(FS.FrameWork.Management.Language.Msg(string.Format("�Ƿ�ֽ�{0}���ҽ����Ϣ��", this.txtDays.Value.ToString())),
                "��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //{5617A654-9738-4db4-A006-5A80B44F0841} ���¼���ʱ��Ҫ�Ի����б�ֵ
                this.alPatients = this.GetSelectedTreeNodes();

                intDyas = (int)this.txtDays.Value;
                this.RefreshExec();
            }
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.TabControl1.SelectedIndex == 0)
            {
                this.chkAll.Checked = tab0AllSelect;
                if (IsUseUnChkSpecial)
                {
                    this.chkUnSp.Visible = true;
                }
            }
            else
            {
                this.chkAll.Checked = tab1AllSelect;
                this.chkUnSp.Visible = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveComfirm();
        }

        private void txtDays_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime[] prmsDate = new DateTime[] { this.dtpBeginTime.Value, this.dtpEndTime.Value };
            string[] prmsStr = new string[] { this.txtName.Text, this.txtDrugDeptName.Text };
            SetFilteredFlag(false, prmsDate, prmsStr);
        }


        private void txtDrugDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime[] prmsDate = new DateTime[] { this.dtpBeginTime.Value, this.dtpEndTime.Value };
            string[] prmsStr = new string[] { this.txtName.Text, this.txtDrugDeptName.Text };
            SetFilteredFlag(false, prmsDate, prmsStr);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            CheckFilteredData();
        }

        private void dtpBeginTime_ValueChanged(object sender, EventArgs e)
        {
            DateTime[] prmsDate = new DateTime[] { this.dtpBeginTime.Value, this.dtpEndTime.Value };
            string[] prmsStr = new string[] { this.txtName.Text, this.txtDrugDeptName.Text };
            SetFilteredFlag(false, prmsDate, prmsStr);
        }

        private void dtpEndTime_ValueChanged(object sender, EventArgs e)
        {
            DateTime[] prmsDate = new DateTime[] { this.dtpBeginTime.Value, this.dtpEndTime.Value };
            string[] prmsStr = new string[] { this.txtName.Text, this.txtDrugDeptName.Text };
            SetFilteredFlag(false, prmsDate, prmsStr);
        }
        #endregion

        #region ����

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.InitControl();
            TreeView tv = sender as TreeView;
            if (tv != null && this.tv.CheckBoxes == false)
                tv.CheckBoxes = true;
            return null;
        }
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (tv != null && tv.CheckBoxes == false)
                tv.CheckBoxes = true;
            return base.OnSetValue(neuObject, e);
        }

        protected override int OnSetValues(ArrayList alValues, object e)
        {
            //{5617A654-9738-4db4-A006-5A80B44F0841} ��ѯʱҲ��Ҫ��������ֵ
            intDyas = (int)this.txtDays.Value;

            this.alPatients = alValues;
            this.RefreshExec();
            return 0;
        }

        /// <summary>
        /// ִ�б���
        /// </summary>
        /// <returns></returns>
        private int SaveComfirm()
        {
            ComfirmExec();

            this.btnSave.Enabled = true;

            int errCol = fpOrderExecBrowser1.fpSpread.Sheets[0].ColumnCount - 1;
            fpOrderExecBrowser1.fpSpread.Sheets[0].Columns[errCol].Visible = true;

            //�����������ҽ��
            for (int row = fpOrderExecBrowser1.fpSpread.Sheets[0].RowCount - 1; row >= 0; row--)
            {
                //if (this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[row, this.fpOrderExecBrowser1.ColumnIndexSelection].Text.ToUpper() == "TRUE")
                //{
                    FS.HISFC.Models.Order.ExecOrder execOrder = fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[row].Tag as FS.HISFC.Models.Order.ExecOrder;

                    string combNo = execOrder.Order.Combo.ID + execOrder.DateUse.ToString() + "ҩ";
                    //string combNo = fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[row, 3].Text + "ҩ";

                    if (dicStopCombNo.ContainsKey(combNo))
                    {
                        Dictionary<string, string> hs = dicStopCombNo[combNo];
                        if (hs.ContainsKey(execOrder.ID))
                        {
                            fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[row, errCol].Text = hs[execOrder.ID];
                        }
                    }
                //}
            }

            errCol = fpOrderExecBrowser2.fpSpread.Sheets[0].ColumnCount - 1;
            fpOrderExecBrowser2.fpSpread.Sheets[0].Columns[errCol].Visible = true;

            //�����������ҽ��
            for (int row = fpOrderExecBrowser2.fpSpread.Sheets[0].RowCount - 1; row >= 0; row--)
            {
                //if (this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[row, this.fpOrderExecBrowser2.ColumnIndexSelection].Text.ToUpper() == "TRUE")
                //{
                    FS.HISFC.Models.Order.ExecOrder execOrder = fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[row].Tag as FS.HISFC.Models.Order.ExecOrder;

                    string combNo = execOrder.Order.Combo.ID + execOrder.DateUse.ToString() + "��ҩ";
                    //string combNo = fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[row, 3].Text + "��ҩ";

                    if (dicStopCombNo.ContainsKey(combNo))
                    {
                        Dictionary<string, string> hs = dicStopCombNo[combNo];
                        if (hs.ContainsKey(execOrder.ID))
                        {
                            fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[row, errCol].Text = hs[execOrder.ID];
                        }
                    }
                //}
            }

            this.tabPage1.Text = "ҩƷ" + "��" + this.fpOrderExecBrowser1.GetFpRowCount(0).ToString() + "����";
            this.tabPage2.Text = "��ҩƷ" + "��" + this.fpOrderExecBrowser2.GetFpRowCount(0).ToString() + "����";

            string ExecTip = "����δ�ֽⱣ����Ŀ��\r\n\r\n";
            bool show = false;
            if (fpOrderExecBrowser1.GetFpRowCount(0) > 0)
            {
                ExecTip += "\r\n\r\n" + tabPage1.Text;
                show = true;
            }
            if (fpOrderExecBrowser2.GetFpRowCount(0) > 0)
            {
                ExecTip += "\r\n\r\n" + tabPage2.Text;
                show = true;
            }
            if (show)
            {
                Classes.Function.ShowBalloonTip(3, "ע��", ExecTip + "\r\n", ToolTipIcon.Warning);
            }

            if (!string.IsNullOrEmpty(lackFeeInfo))
            {
                Classes.Function.ShowBalloonTip(3, "Ƿ����ʾ", "���»��ߴ���Ƿ�������\r\n" + lackFeeInfo, ToolTipIcon.Warning);
            }

            return 1;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            return SaveComfirm();
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Print(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.PrintPreview(this.TabControl1);
            return 0;
        }

        /// <summary>
        /// ���ô�ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int SetPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.ShowPageSetup();
            p.PrintPreview(this.TabControl1);
            return 0;
        }
        #endregion

        /// <summary>
        /// ���ƶ�ҽ����������ɫ��ʾ
        /// </summary>
        /// <param name="filterStr">����ƥ�������</param>
        /// <param name="isMatchingAll">�Ƿ���ȫ��ƥ��</param>
        protected void SetDrugFlag(string filterStr, bool isMatchingAll)
        {
            FS.HISFC.Models.Order.ExecOrder order = null;
            //��ʼ����ʾ��ҩƷ
            if (this.TabControl1.SelectedIndex == 0)
            {
                bool b = false;
                //�ָ�ԭ������ɫ
                //�����ɫ��ʾ
                for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    if (b)
                    {
                        this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.Linen;
                    }
                    else
                    {
                        this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.White;
                    }
                    b = !b;
                }
                for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    order = this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;
                    if (isMatchingAll)
                    {
                        if (this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 2].Text == filterStr)
                        {
                            this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                        }
                    }
                    else
                    {
                        if (this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 2].Text.IndexOf(filterStr) != -1)
                        {
                            this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                        }
                    }
                }
            }
            //��ҩƷ
            else
            {
                //�ָ�ԭ������ɫ
                bool b = false;
                //�ָ�ԭ������ɫ
                //�����ɫ��ʾ
                for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    if (b)
                    {
                        this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.Linen;
                    }
                    else
                    {
                        this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.White;
                    }
                    b = !b;
                }
                for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    if (isMatchingAll)
                    {
                        if (this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, 2].Text == filterStr)
                        {
                            this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                        }
                    }
                    else
                    {
                        if (this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, 2].Text.IndexOf(filterStr) != -1)
                        {
                            this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                        }
                    }
                }
            }
        }


    }

    /// <summary>
    /// 
    /// </summary>
    public enum EnumLackFee
    {
        ���ж�Ƿ��,
        Ƿ�Ѳ�����ֽ�,
        Ƿ����ʾ�ʲ�����ֽ�
    }
}
