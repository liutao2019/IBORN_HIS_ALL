using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FarPoint.Win.Spread;
using FS.FrameWork.Function;
using FS.HISFC.Components.Common.Controls;
using FS.HISFC.Models.Base;
using FS.FrameWork.Management;
namespace FS.HISFC.Components.Terminal.Confirm
{
    public partial class ucInpatientConfirm : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucInpatientConfirm()
        {
            InitializeComponent(); 
            this.fpExecOrder.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpExecOrder_ColumnWidthChanged);

            this.ckCancel.CheckedChanged += new EventHandler(ckComplete_CheckedChanged);
            this.ckNone.CheckedChanged += new EventHandler(ckComplete_CheckedChanged);
            this.ckComplete.CheckedChanged += new EventHandler(ckComplete_CheckedChanged);

            this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInpatientNo1_myEvent);

            this.fpExecOrder.ComboSelChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpExecOrder_ComboSelChange);
            this.fpExecOrder.ButtonClicked += new EditorNotifyEventHandler(fpExecOrder_ButtonClicked);

            this.cmbDept.SelectedIndexChanged += new EventHandler(cmbDept_SelectedIndexChanged);
        }

        #region ˽�б���
        public bool seeAll = false;
        public bool clearConfirm = false;
        FS.HISFC.Models.Base.MessType messType =  FS.HISFC.Models.Base.MessType.N;
        private bool addFirstRow = false;
        FS.HISFC.BizLogic.Terminal.TerminalConfirm terminalMgr = new FS.HISFC.BizLogic.Terminal.TerminalConfirm();
        FS.HISFC.Components.Common.Controls.EnumShowItemType itemType = FS.HISFC.Components.Common.Controls.EnumShowItemType.DeptItem;
        //FS.ApplyInterface.HisInterface PACSApplyInterface = null;
        FS.HISFC.BizProcess.Integrate.Common.ControlParam cpMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        #region {5E5299D8-95A2-4498-B2F1-52D00E4FB11A}
        //FS.HISFC.Components.PacsApply.HisInterface PACSApplyInterfaceNew = null;
        #endregion

        /// <summary>
        ///  ȷ�����Ƿ���� {130607A7-01C7-4bdf-931C-D7CE7AC8111B}
        /// </summary>
        private bool isNeedOper = false;

        private Dictionary<string, int> hsRows = new Dictionary<string, int>();

        /// <summary>
        /// Ĭ�Ϲ�ѡ��ȷ��״̬
        /// </summary>
        private EnumCheckState defaultConfirmState = EnumCheckState.ComfirmComplete;

        /// <summary>
        /// Ĭ�Ϲ�ѡ��ȷ��״̬
        /// </summary>
        [Category("�ؼ�����"), Description("Ĭ�Ϲ�ѡ��ȷ��״̬��ȷ���շѡ�ȡ���շѡ��ݲ������")]
        public EnumCheckState DefaultConfirmState
        {
            get
            {
                return defaultConfirmState;
            }
            set
            {
                defaultConfirmState = value;
            }
        }

        /// <summary>
        /// �Ƿ�ʹ��WebEMR����
        /// </summary>
        private bool isShowWebEMR = false;

        /// <summary>
        /// �Ƿ�ʹ��WebEMR����
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�ʹ��WebEMR����")]
        public bool IsShowWebEMR
        {
            get
            {
                return isShowWebEMR;
            }
            set
            {
                isShowWebEMR = value;
            }
        }

        /// <summary>
        /// WebEMR��ѯ��ַ���贫��סԺ��ˮ��
        /// </summary>
        private string webEMRUrl = "http://172.16.61.119:8080/emrweb/Default.aspx?hisInpatientID={0}&Out=0";

        /// <summary>
        /// WebEMR��ѯ��ַ���贫��סԺ��ˮ��
        /// </summary>
        [Category("�ؼ�����"), Description("WebEMR��ѯ��ַ���贫��סԺ��ˮ��")]
        public string WebEMRUrl
        {
            get
            {
                return webEMRUrl;
            }
            set
            {
                webEMRUrl = value;
            }
        }

        #endregion

        #region  ����
        /// <summary>
        /// ����Ƿ������
        /// </summary>
        [Category("�ؼ�����"), Description("Ƿ���ж���ʾ����")]
        public FS.HISFC.Models.Base.MessType MessType
        {
            get
            {
                return messType;
            }
            set
            {
                messType = value;
            }
        }
        /// <summary>
        /// ��ʾ����Ŀ���
        /// </summary>
        [Category("�ؼ�����"), Description("��ʾ����Ŀ���")]
        public FS.HISFC.Components.Common.Controls.EnumShowItemType ItemType
        {
            get
            {
                return itemType;
            }
            set
            {
                itemType = value;
            }
        }
        /// <summary>
        /// �鿴���п����ն�ȷ����Ŀ
        /// </summary>
        [Category("�ؼ�����"), Description("�鿴���п����ն�ȷ����Ŀ")]
        public bool SeeAll
        {
            get
            {
                return seeAll;
            }
            set
            {
                seeAll = value;
            }
        }
        /// <summary>
        /// ���������Ƿ�ɾ����ȷ����Ŀ
        /// </summary>
        [Category("�ؼ�����"), Description("���������Ƿ�ɾ����ȷ����Ŀ")]
        public bool ClearConfirm
        {
            get
            {
                return clearConfirm;
            }
            set
            {
                clearConfirm = value;
            }
        }

        /// <summary>
        /// �������Ƿ��ڵ�һ��
        /// </summary>
        [Category("�ؼ�����"), Description("�������Ƿ��ڵ�һ��")]
        public bool AddFirstRow
        {
            get
            {
                return addFirstRow;
            }
            set
            {
                addFirstRow = value;
            }
        }

        /// <summary>
        /// ȷ�����Ƿ���� {130607A7-01C7-4bdf-931C-D7CE7AC8111B}
        /// </summary>
        [Category("�ؼ�����"), Description("ȷ�����Ƿ���� true ��;false ��")]
        public bool IsNeedOper
        {
            get
            {
                return isNeedOper;
            }
            set
            {
                isNeedOper = value;
            }
        }

        private int showdays = 999;

        [Category("�ؼ�����"), Description("��ʾȷ����Ŀ��������0Ϊ���졢1Ϊǰһ�졢�Դ�����")]
        public int ShowDays
        {
            get
            {
                return showdays;
            }
            set
            {
                this.showdays = value;
            }
        }

        /// <summary>
        /// ���������ж�ҩƷ���ʱ�����Ҵ���ȡֵ
        /// </summary>
        EnumDrugStorageDept drugStorageDept = EnumDrugStorageDept.CurrentLoginDept;

        [Category("�ؼ�����"), Description("���������ж�ҩƷ���ʱ�����Ҵ���ȡֵ")]
        public EnumDrugStorageDept DrugStorageDept
        {
            get
            {
                return this.drugStorageDept;
            }
            set
            {
                this.drugStorageDept = value;
            }
        }
        #endregion

        #region ����


        private enum Cols
        {
            IsExec, //0
            ExecDate,
            ItemCode,//1
            ItemName,//2
            ItemQty,//3
            ItemAlreadConfirmQty,//4
            ItemConfirmQty,//5
            Unit,//6
            Price,//7
            TotCost,//8
            OrderType,//9
            //by yuyun 08-7-7{810581A3-6DF5-49af-8A5F-D7F843CBEA89}
            ItemStatus,//10��Ŀ״̬��δȷ�ϡ�����ȷ�ϡ�ȫ��ȷ�ϣ�ͨ�������������ۺϱȽ�
            Machine,//11��Ŀʹ���豸����ҽ���������в���
            Operator//12��ʦ��Ĭ���ǵ�ǰ����Ա�������޸�
            //by yuyun 08-7-7{810581A3-6DF5-49af-8A5F-D7F843CBEA89}
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo myPatient = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// ����Ա
        /// </summary>
        private FS.HISFC.Models.Base.Employee oper = new FS.HISFC.Models.Base.Employee();

        /// <summary>
        /// ҽ��ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Order orderManager = new FS.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// ����ҵ��
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeManager = new FS.HISFC.BizProcess.Integrate.Fee();
        private FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyManager = new FS.HISFC.BizProcess.Integrate.Pharmacy();
        /// <summary>
        /// �ն�ҵ��
        /// </summary>
        private FS.HISFC.BizLogic.Terminal.TerminalConfirm terminalManager = new FS.HISFC.BizLogic.Terminal.TerminalConfirm();

        /// <summary>
        /// �ն�ҵ��
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Terminal.Confirm confirmIntergrate = new FS.HISFC.BizProcess.Integrate.Terminal.Confirm();
        //by yuyun 08-7-7{810581A3-6DF5-49af-8A5F-D7F843CBEA89}
        /// <summary>
        /// ҽ������ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Terminal.TerminalCarrier terminalCarrier = new FS.HISFC.BizLogic.Terminal.TerminalCarrier();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        private ArrayList alExecOrder = new ArrayList();

        private DataTable dtExecOrder = new DataTable();

        private DataView dvExecOrder = new DataView();

        private string filePath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @"\Profile\TecExecOrder.xml";

        /// <summary>
        /// ToolBarService
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// ��Ŀѡ��ؼ�
        /// </summary>
        public FS.HISFC.Components.Common.Controls.ucItemList ucItemList;

        private int currentRow = 0;
        private int currentColumn = 0;
        private DateTime dtNow = DateTime.Now;

        //�Ƿ�ʹ�õ������뵥
        private string isUseDL = "0";
        private Hashtable depts = new Hashtable();

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ����շ���Ŀ��Ϣ...");
            Application.DoEvents();

            oper = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Clone();

            //this.ucItemList = new ucItemList(FS.HISFC.Components.Common.Controls.EnumShowItemType.Undrug);
            //this.ucItemList = new FS.HISFC.Components.Common.Controls.ucItemList(itemType);
            //{A79AF523-6D9D-4d57-A8A8-277252334AF7}
            this.ucItemList = new FS.HISFC.Components.Common.Controls.ucItemList();

            //ucItemList.enuShowItemType = itemType;{270CF1A4-2A6B-4137-8358-BEB5179E1800}
            // �����Ŀ�б�
            this.Controls.Add(this.ucItemList);

            ucItemList.enuShowItemType = itemType;//Ų������ͺ���{270CF1A4-2A6B-4137-8358-BEB5179E1800}
            ucItemList.Init(string.Empty);

            // ������Ŀ�б��ɼ�

            this.ucItemList.Visible = false;
            // ʹ��Ŀ�б���ǰ

            this.ucItemList.BringToFront();
            // �շ���Ŀ�б�ѡ����Ŀ�¼�
            this.ucItemList.SelectItem += new FS.HISFC.Components.Common.Controls.ucItemList.MyDelegate(ucItemList_SelectItem);

            this.InitFp();

            this.fpExecOrder.KeyEnter += new FS.FrameWork.WinForms.Controls.NeuFpEnter.keyDown(fpExecOrder_KeyEnter);

            isUseDL = cpMgr.GetControlParam<string>("PACSZY");

            //���Ҳ��������Ŀ���
            depts.Clear();
            ArrayList alDept = managerIntegrate.QueryDepartment((this.terminalCarrier.Operator as FS.HISFC.Models.Base.Employee).Nurse.ID);
            if (alDept != null)
            {
                foreach (FS.FrameWork.Models.NeuObject obj in alDept)
                {
                    if (depts.ContainsKey(obj.ID) == false)
                    {
                        depts.Add(obj.ID, obj.Name);
                    }
                }
            }

            if (depts.ContainsKey(this.oper.Dept.ID) == false)
            {
                depts.Add(this.oper.Dept.ID, this.oper.Dept.Name);
            }


            if (!isShowWebEMR)
            {
                this.tabControl1.TabPages.Remove(tpWebEMR);
            }

        }

        void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((tvInpatientConfirm)this.tv).FindDefaultDept(this.cmbDept.Tag.ToString());
        }

        /// <summary>
        /// ��ʼ�����
        /// </summary>
        private void InitFp()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(FS.FrameWork.Management.Language.Msg("���ڳ�ʼ��������Ժ�....."));
            try
            {
                if (System.IO.File.Exists(this.filePath))
                {

                    FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(this.filePath, this.dtExecOrder, ref this.dvExecOrder, this.fpExecOrder_Sheet1);

                    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpExecOrder_Sheet1, this.filePath);
                }
                else
                {
                    this.dtExecOrder.Columns.AddRange(new DataColumn[]
                    {
                        new DataColumn("ִ�б��",typeof(bool)),
                        new DataColumn("��������",typeof(string)),
                        new DataColumn("��Ŀ����",typeof(string)),
                        new DataColumn("��Ŀ����",typeof(string)),
                        new DataColumn("������",typeof(decimal)),
                        new DataColumn("��ȷ������",typeof(decimal)),
                        new DataColumn("ȷ������",typeof(decimal)),
                        new DataColumn("��λ",typeof(string)),
                        new DataColumn("����",typeof(decimal)),
                        new DataColumn("�ܶ�",typeof(decimal)),
                        new DataColumn("SOURCE",typeof(string)),
                        //by yuyun 08-7-7{810581A3-6DF5-49af-8A5F-D7F843CBEA89}
                        new DataColumn("״̬",typeof(string)),
                        new DataColumn("��Ӧ�豸",typeof(string)),
                        new DataColumn("��ʦ",typeof(string))
                        //by yuyun 08-7-7
                    });

                    this.dvExecOrder = new DataView(this.dtExecOrder);

                    this.fpExecOrder.DataSource = this.dvExecOrder;

                    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpExecOrder_Sheet1, this.filePath);
                }
            }
            catch
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            this.fpExecOrder_Sheet1.DefaultStyle.Locked = true;

            this.fpExecOrder_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpExecOrder_Sheet1.Columns[(int)Cols.IsExec].Locked = false;
            this.fpExecOrder_Sheet1.Columns[(int)Cols.ExecDate].Locked = true;
            this.fpExecOrder_Sheet1.Columns[(int)Cols.ItemName].Locked = false;
            this.fpExecOrder_Sheet1.Columns[(int)Cols.ItemQty].Locked = true;
            //by yuyun 08-7-7{810581A3-6DF5-49af-8A5F-D7F843CBEA89}
            //this.fpExecOrder_Sheet1.Columns[(int)Cols.ItemQty].Visible = false;
            //by yuyun 08-7-7
            #region {DEF0DE5C-96BF-4f48-80DC-297B9B16315A}
            this.fpExecOrder_Sheet1.Columns[(int)Cols.ItemCode].Locked = true;
            #endregion
            this.fpExecOrder_Sheet1.Columns[(int)Cols.ItemAlreadConfirmQty].Locked = true;
            this.fpExecOrder_Sheet1.Columns[(int)Cols.ItemConfirmQty].Locked = false;
            this.fpExecOrder_Sheet1.Columns[(int)Cols.Unit].Locked = true;
            this.fpExecOrder_Sheet1.Columns[(int)Cols.Price].Locked = true;
            this.fpExecOrder_Sheet1.Columns[(int)Cols.TotCost].Locked = true;
            this.fpExecOrder_Sheet1.Columns[(int)Cols.OrderType].Locked = true;
            //by yuyun 08-7-8{52355595-B401-4db9-82BC-A3650F11D2CC}
            this.fpExecOrder_Sheet1.Columns[(int)Cols.ItemStatus].Locked = true;
            this.fpExecOrder_Sheet1.Columns[(int)Cols.Machine].Locked = false;
            this.fpExecOrder_Sheet1.Columns[(int)Cols.Operator].Locked = false;
            //�ڼ�ʦ���м�����Ա�б�ѡ��
            ArrayList al = new ArrayList();
            FS.HISFC.BizProcess.Integrate.Manager ztManager = new FS.HISFC.BizProcess.Integrate.Manager();
            al = ztManager.QueryEmployeeAll();
            //.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.T);
            this.fpExecOrder.SetColumnList(this.fpExecOrder_Sheet1, (int)Cols.Operator, al);
            //�豸�м���ҽ���豸���ݣ�ֻ��ʾ��ǰ����Ա���ڿ��ҵ�ҽ���豸
            al = this.terminalCarrier.GetDesigns(this.oper.Dept.ID);
            if (al.Count > 0)
            {
                ArrayList alTer = new ArrayList();
                foreach (HISFC.Models.Terminal.TerminalCarrier terObj in al)
                {
                    alTer.Add(new FS.FrameWork.Models.NeuObject(terObj.CarrierCode, terObj.CarrierName, ""));
                }
                this.fpExecOrder.SetColumnList(this.fpExecOrder_Sheet1, (int)Cols.Machine, alTer);
            }
            this.fpExecOrder.SetItem += new FS.FrameWork.WinForms.Controls.NeuFpEnter.setItem(fpExecOrder_SetItem);
            //by yuyun 08-7-8
            FarPoint.Win.Spread.CellType.MultiOptionCellType checkBoxCellType = new FarPoint.Win.Spread.CellType.MultiOptionCellType();

            ArrayList alArrNames = FS.FrameWork.Public.EnumHelper.Current.EnumArrayList<EnumCheckState>();
            string[] arr = new string[alArrNames.Count];
            for(int i=0;i<arr.Length;i++)
            {
                arr[i] = (alArrNames[i] as FS.FrameWork.Models.NeuObject).Name;
            }
            checkBoxCellType.Items = arr;
            checkBoxCellType.Orientation = FarPoint.Win.RadioOrientation.Horizontal;
            checkBoxCellType.ItemData = arr;
            this.fpExecOrder_Sheet1.Columns[(int)Cols.IsExec].CellType = checkBoxCellType;
            //this.fpExecOrder_Sheet1.Columns[(int)Cols.IsExec].Width = 220F;
           
            FarPoint.Win.Spread.InputMap im = this.fpExecOrder.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Escape, Keys.None), Keys.None);

        }


        /// <summary>
        /// ���ҽ�����ݵ����
        /// </summary>
        /// <param name="alExecOrder"></param>
        /// <returns></returns>
        protected virtual int AddExecOrderToFp(ArrayList alExecOrder)
        {
            try
            {
                decimal totCost = 0;
                foreach (FS.HISFC.Models.Order.ExecOrder order in alExecOrder)
                {
                    #region {5197289A-AB55-410b-81EE-FC7C1B7CB5D7}
                    //�Ѿ��ж�
                    //���������ն�ȷ��ҽ������ʿ�ֽ⣬��û�д�û�б��������Ҳ��ʾ����������
                    //if (order.Order.OrderType.IsDecompose)
                    //{
                    //    if (!this.orderManager.CheckLongUndrugIsConfirm(order.ID))
                    //    {
                    //        continue;
                    //    }
                    //}
                    #endregion

                    int row = 0;

                    if (hsRows.ContainsKey(order.ID))
                    {
                        row = hsRows[order.ID];
                    }
                    else
                    {
                        this.fpExecOrder.Sheets[0].Rows.Add(this.fpExecOrder.Sheets[0].RowCount, 1);
                        row = this.fpExecOrder.Sheets[0].RowCount - 1;
                        hsRows.Add(order.ID, row);
                    }

                    this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.IsExec].Value = string.IsNullOrEmpty(this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.IsExec].Text) ? (int)defaultConfirmState : this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.IsExec].Value;

                    if (defaultConfirmState == EnumCheckState.ComfirmCancel)
                    {
                        this.fpExecOrder_Sheet1.Cells[row, (int)Cols.IsExec].ForeColor = Color.Red;
                    }
                    this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.ExecDate].Value = order.ExecOper.OperTime.ToShortDateString();
                    this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.ItemCode].Text = order.Order.Item.ID;
                    this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.ItemName].Text = order.Order.Item.Name;
                    this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.ItemName].Locked = true;
                    this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.ItemQty].Text = order.Order.Qty.ToString();
                    //�޸�ȷ�Ͻ�����ʾȷ�������������� {47531763-0983-44dc-BC92-59A5588AF2F8} 2014-12-12 by lixuelong
                    decimal packQty = terminalMgr.GetPackageItemPackQty(order.Order.ID, order.ID);
                    decimal AlreadyQty = terminalMgr.GetAlreadConfirmNum(order.Order.ID, order.ID) / packQty;
                    if (AlreadyQty < 0)
                    {
                        MessageBox.Show("��ȡ��ȷ����Ŀ����ʧ��" + terminalMgr.Err);
                        return -1;
                    }
                    decimal LeaveQty = order.Order.Qty - AlreadyQty;
                    this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.ItemAlreadConfirmQty].Text = AlreadyQty.ToString();
                    if (string.IsNullOrEmpty(this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.ItemConfirmQty].Text) || LeaveQty <= FS.FrameWork.Function.NConvert.ToDecimal(this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.ItemConfirmQty].Value))
                    {
                        this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.ItemConfirmQty].Text = LeaveQty.ToString();
                    }
                    this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.Unit].Text = order.Order.Unit;
                    this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.Price].Text = order.Order.Item.Price.ToString();
                    if (order.Order.Item.Price == 0 && order.Order.Unit != "[������]")//by zhouxs 2007-10-28
                    {
                        this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.Price].Locked = false;
                    }//end zhouxs
                    this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.TotCost].Text = Convert.ToString(order.Order.Item.Price * order.Order.Qty);
                    totCost += order.Order.Item.Price * order.Order.Qty;

                    this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.OrderType].Text = "ORDER";
                    //by yuyun 08-7-8{07D1BACB-8E4F-4ac8-8254-81763D0F0699}
                    if (AlreadyQty > 0 && AlreadyQty < order.Order.Qty)
                    {
                        this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.ItemStatus].Text = "����ȷ��";
                        this.fpExecOrder.Sheets[0].Rows[row].BackColor = Color.LightBlue;//������ȷ�ϵ���Ŀ����ɫ����
                    }
                    else
                    {
                        this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.ItemStatus].Text = "δȷ��";
                    }
                    //ҽ���豸
                    this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.Machine].Text = "";
                    ////////////////////
                    //��ʦ
                    this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.Operator].Text = this.oper.Name;
                    this.fpExecOrder.Sheets[0].Cells[row, (int)Cols.Operator].Tag = this.oper.ID;
                    //by yuyun 08-7-7{810581A3-6DF5-49af-8A5F-D7F843CBEA89}

                    this.fpExecOrder.Sheets[0].Rows[row].Tag = order;
                }

                //lblTotCost.Text = "��" + totCost.ToString();
                this.ShowTotFee();
            }
            catch (Exception ex)
            {
                MessageBox.Show("���ر���ʽ���������TecExecOrder.xml�ļ�" + ex.Message);

                return -1;
            }
            return 0;
        }

      
        #region {53061BA2-33FC-469d-9773-9A9454023612}
        /// <summary>
        /// ɾ������Ŀ�¼�

        /// </summary>

        void DeleteItemEvent()
        {
            this.MakeItemListDisappear();
            this.DeleteNew();
        }

        /// <summary>
        /// ɾ��һ��(��ǰ��)����Ŀ

        /// </summary>
        public void DeleteNew()
        {
            // ���û�м�¼���򷵻�
            if (this.fpExecOrder_Sheet1.RowCount == 0)
            {
                return;
            }
            // ֻ��ɾ������Ŀ

            if (this.GetItem(this.fpExecOrder_Sheet1.ActiveRowIndex, (int)Cols.OrderType) != "NEW")
            {
                MessageBox.Show("����Ŀ������ɾ��", "ҽ���ն�ȷ��");
                //this.Focus();
                //this.CellFocus(this.fpExecOrder_Sheet1.ActiveRowIndex, (int)Cols.OrderType);
                return;
            }
            // ɾ��
            this.DeleteRow(this.fpExecOrder_Sheet1.ActiveRowIndex, true);
        }
        /// <summary>
        /// ��ָ�����к�ɾ��һ��

        /// [����1: int row - Ҫɾ�����к�]
        /// [����2: bool confirm - �Ƿ���Ҫ�û�ȷ��]
        /// </summary>
        /// <param name="row">Ҫɾ�����к�</param>
        /// <param name="confirm">�Ƿ���Ҫȷ��</param>
        public void DeleteRow(int row, bool confirm)
        {
            // ����ǿռ�¼����ֱ��ɾ����������Ҫȷ��ɾ��

            if (confirm && (!this.IsNull(row)))
            {
                // ���ȡ��ɾ������ô����

                if (MessageBox.Show("�Ƿ�ɾ����ǰ�У�", "ҽ���ն�ȷ��",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    this.Focus();
                    return;
                }
            }
            //{8C0239CE-4272-4f30-B547-9C3C27D694E4} ֹͣ�༭ģʽ������ɾ��ʱ�����˵�ǰѡ���ı�
            this.fpExecOrder.EditMode = false;
            // ɾ��ָ������
            this.fpExecOrder_Sheet1.RemoveRows(row, 1);
            // ���ý��㵽��һ��

            //this.fpExecOrder_Sheet1.Focus();
            //if (row - 1 >= 0)
            //{
            //    this.CurrentRow = row - 1;
            //    // ���ý��㵽��Ŀ����

            //    this.CurrentColumn = (int)DisplayField.ItemName;
            //}
        }
        /// <summary>
        /// �ж�ָ�����Ƿ�Ϊ�գ������Ŀ���14��Ϊ�վ���Ϊ�ǿռ�¼��

        /// [����: int row - �к�]
        /// [����: bool,true - ��, false - �ǿ�]
        /// </summary>
        /// <param name="row">ָ�����к�</param>
        /// <returns>true��Ϊ��/false����Ϊ��</returns>
        public bool IsNull(int row)
        {
            // �����Ŀ����Ϊ�գ�������ĿΪ��

            if (this.fpExecOrder_Sheet1.Cells[row, (int)Cols.ItemCode].Text.Equals(""))
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// ��ȡһ��CELL��ֵ

        /// [����1: int row - �к�]
        /// [����2: int column - �к�]
        /// [����: string, �ı�ֵ]
        /// </summary>
        /// <param name="row">��</param>
        /// <param name="column">��</param>
        /// <returns>CELL������ı�</returns>
        public string GetItem(int row, int column)
        {
            return this.fpExecOrder_Sheet1.Cells[row, column].Text;
        }
        /// <summary>
        /// ���շ���Ŀѡ��ؼ����ɼ�

        /// </summary>
        private void MakeItemListDisappear()
        {
            if (this.ucItemList.Visible == true)
            {
                this.ucItemList.Visible = false;
            }
        }
        #endregion
        private void Clear()
        {
            this.myPatient = new FS.HISFC.Models.RADT.PatientInfo();
            //this.ucQueryInpatientNo1.Text = string.Empty;
            lblName.Text = "����������";
            lblPatientNO.Text = "סԺ�ţ�";
            lblDept.Text = "���߿��ң�";
            lblFreeCost.Text = "������";
            this.fpExecOrder.Sheets[0].RowCount = 0;
            this.hsRows.Clear();
        }

        /// <summary>
        /// ��ѡ��
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {

            FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            if (neuObject != null)
            {
                if (neuObject.GetType() == patientInfo.GetType())
                {
                    patientInfo = neuObject as FS.HISFC.Models.RADT.PatientInfo;
                    if (this.myPatient != patientInfo || this.myPatient == null || patientInfo == null || !this.myPatient.ID.Equals(patientInfo.ID))
                    {
                        this.myPatient = patientInfo;
                        this.fpExecOrder.Sheets[0].RowCount = 0;
                        this.hsRows.Clear();
                    }

                    lblName.Text = "����������" + this.myPatient.Name;
                    lblPatientNO.Text = "סԺ�ţ�" + this.myPatient.PID.PatientNO;
                    lblDept.Text = "���߿��ң�" + this.myPatient.PVisit.PatientLocation.Dept.Name;
                    lblFreeCost.Text = "������" + this.myPatient.FT.LeftCost.ToString();
                    //1����������ʾ�Ļ��߻�����Ϣ��Ҫ��ʾ������סԺ���ڿ��ҡ�����������
                    lbNurseCellName.Text = "������" + this.myPatient.PVisit.PatientLocation.NurseCell.Name;
                    lbBedName.Text = "���ţ�" + this.myPatient.PVisit.PatientLocation.Bed.Name;
                    neuLabel1.Text = "���䣺" + this.myPatient.Age;
                    neuLabel3.Text = "�Ա�" + this.myPatient.Sex.Name;
                    neuLabel2.Text = "��ͬ��λ��" + this.myPatient.Pact.Name;
                    this.ucQueryInpatientNo1.Text = this.myPatient.PID.PatientNO;
                    //this.ucQueryInpatientNo1_myEvent();
                    if (this.seeAll)
                    {
                        alExecOrder = this.orderManager.QueryExecOrderByDept(patientInfo.ID, "2", false, "all");
                    }
                    else
                    {
                        alExecOrder = new ArrayList();
                        //�������е�
                        foreach (DictionaryEntry de in depts)
                        {
                            ArrayList al = this.orderManager.QueryExecOrderByDept(patientInfo.ID, "2", false, de.Key.ToString());
                            if (al != null)
                            {
                                alExecOrder.AddRange(al);
                            }
                        }
                        //alExecOrder = this.orderManager.QueryExecOrderByDept(patientInfo.ID, "2", false, oper.Dept.ID);
                    }
                    if (alExecOrder == null || alExecOrder.Count == 0)
                    {
                        MessageBox.Show("�û���û����Ҫȷ�ϵ���Ŀ");
                        //this.Clear();
                    }
                    else
                    {
                        this.AddExecOrderToFp(alExecOrder);
                    }

                    this.tabControl1.SelectedTab = this.tpItemInfo;

                    //��ѯWebEMR
                    string URL = webEMRUrl;
                    URL = string.Format(URL, patientInfo.ID);
                    this.webBrowser1.Refresh(WebBrowserRefreshOption.Normal);
                    this.webBrowser1.Navigate(URL);
                }
            }
            else
            {
                this.Clear();
                this.Refresh();
                this.ucQueryInpatientNo1.Text = string.Empty;
            }

            return base.OnSetValue(neuObject, e);
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.Init();
                base.OnLoad(e);
                InputMap im;
                im = fpExecOrder.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                im = fpExecOrder.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                im = fpExecOrder.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            }
            catch (Exception ex)
            {
                MessageBox.Show("��ʼ������:" + ex.Message + "   ��ɾ��" + this.filePath + "�����ԣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        /// <summary>
        /// ����»�������
        /// </summary>
        private void AddNewRow()
        {
            if (this.myPatient == null || this.myPatient.ID == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��û��ѡ���ߣ�"));
                return;
            }
            int rowcount = this.fpExecOrder.Sheets[0].RowCount;
            if (AddFirstRow)
            {
                this.fpExecOrder.Sheets[0].Rows.Add(0, 1);
                this.fpExecOrder.Sheets[0].Cells[0, (int)Cols.OrderType].Text = "NEW";
                #region {15181E09-0842-4e3f-A91C-A25F3930CA28}
                this.fpExecOrder.Sheets[0].SetActiveCell(0, (int)Cols.ItemName, true);
                #endregion

            }
            else
            {
                this.fpExecOrder.Sheets[0].Rows.Add(rowcount, 1);
                this.fpExecOrder.Sheets[0].Cells[rowcount, (int)Cols.OrderType].Text = "NEW";
                #region {15181E09-0842-4e3f-A91C-A25F3930CA28}
                this.fpExecOrder.Sheets[0].SetActiveCell(rowcount, (int)Cols.ItemName, true);
                #endregion
            }
            #region {15181E09-0842-4e3f-A91C-A25F3930CA28}
            this.fpExecOrder.EditMode = true;
            //this.fpExecOrder.Focus();
            #endregion
            this.currentRow = rowcount;
            this.ShowTotFee();
        }

        /// <summary>
        /// ����»������ݵ����
        /// </summary>
        private int  InsertItem()
        {
            FS.HISFC.Models.Base.Item item = null;
            FS.HISFC.Models.Fee.Item.Undrug undrug = null;
            FS.HISFC.Models.Pharmacy.Item pharmacy = null;
            int intReturn = this.ucItemList.GetSelectItem(out item);

            if (item == null || item.ID == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("û���ҵ���Ŀ"));
                return -1;
            }
            if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
            {
                undrug = feeManager.GetUndrugByCode(item.ID);

                if (intReturn > 0)
                {
                    this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.IsExec].Value = (int)EnumCheckState.ComfirmComplete;
                    this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.ItemCode].Text = undrug.ID;
                    this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.ItemName].Text = undrug.Name;
                    this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.Unit].Text = undrug.PriceUnit;
                    this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.ItemConfirmQty].Value = 1;
                    if (undrug.UnitFlag == "0")
                    {
                        this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.Price].Text = undrug.Price.ToString();
                    }
                    else if (undrug.UnitFlag == "1")
                    {
                        this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.Price].Text = undrug.Price.ToString();
                    }
                    this.UnDisplayUcItemList();
                    this.fpExecOrder.Sheets[0].ActiveRow.Tag = item;
                }
            }
            else//ҩƷ¼��
            {
                pharmacy = pharmacyManager.GetItem(item.ID);

                if (intReturn > 0)
                {
                    this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.IsExec].Value = (int)EnumCheckState.ComfirmComplete;
                    this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.ItemCode].Text = pharmacy.ID;
                    if (pharmacy.Specs != null && pharmacy.Specs != string.Empty)
                    {
                        this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.ItemName].Text = pharmacy.Name + "{" + pharmacy.Specs + "}";
                    }

                    //�۸񡢵�λ
                    FarPoint.Win.Spread.CellType.ComboBoxCellType comboType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                    comboType.Editable = true;
                    comboType.Items = (new string[]{pharmacy.MinUnit,
                                                pharmacy.PackUnit});
                    this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.Unit].CellType = comboType;
                    this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.Unit].Locked = false;
                    decimal price = 0.00M;
                    if (item.SpecialFlag4 == "2") //��װ��λ
                    {
                        this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.Unit].Text = pharmacy.PackUnit;
                        pharmacy.PriceUnit = pharmacy.PackUnit;
                        price = FS.FrameWork.Public.String.FormatNumber(pharmacy.Price, 4);
                    }
                    else
                    {
                        this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.Unit].Text = pharmacy.MinUnit;
                        pharmacy.PriceUnit = pharmacy.MinUnit;
                        price = FS.FrameWork.Public.String.FormatNumber(pharmacy.Price / pharmacy.PackQty, 4);
                    }
                    //����
                    pharmacy.Qty = 1;
                    this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.ItemConfirmQty].Value = pharmacy.Qty;
                    //�۸�
                    this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.Price].Text = price.ToString();
                    this.UnDisplayUcItemList();
                    this.fpExecOrder.Sheets[0].ActiveRow.Tag = pharmacy;
                }
            }

            return 1;
        }

        /// <summary>
        /// ��Ŀѡ���б�����
        /// </summary>
        public void UnDisplayUcItemList()
        {
            if (this.ucItemList.Visible)
            {
                this.ucItemList.Visible = false;
            }
        }

        /// <summary>
        /// �������ת����order
        /// </summary>
        /// <returns></returns>
        private ArrayList GetFeeOrder()
        {
            int rowCount = this.fpExecOrder.Sheets[0].RowCount;
            ArrayList alFeeOrder = new ArrayList();
            for (int i = 0; i < rowCount; i++)
            {
                FS.HISFC.Models.Order.Inpatient.Order obj = new FS.HISFC.Models.Order.Inpatient.Order();
                if (FS.FrameWork.Function.NConvert.ToInt32(this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.IsExec].Value)==(int)EnumCheckState.ComfirmComplete)
                {
                    FS.HISFC.Models.Base.OperEnvironment o = new FS.HISFC.Models.Base.OperEnvironment();
                    o.ID = oper.ID;
                    o.Name = oper.Name;
                    o.Dept = oper.Dept;
                    o.OperTime = this.dtNow;
                    if (this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.OrderType].Text == "ORDER")
                    {
                        FS.HISFC.Models.Order.ExecOrder order = new FS.HISFC.Models.Order.ExecOrder(); ;
                        order = (FS.HISFC.Models.Order.ExecOrder)this.fpExecOrder.Sheets[0].Rows[i].Tag;
                        FS.HISFC.Models.Fee.Item.Undrug undrug = new FS.HISFC.Models.Fee.Item.Undrug();
                        undrug = this.feeManager.GetUndrugByCode(order.Order.Item.ID);
                        decimal AlreadyQty = terminalMgr.GetAlreadConfirmNum(order.Order.ID, order.ID);
                        if (AlreadyQty < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("��ȡ��ȷ����Ŀ����ʧ��" + terminalMgr.Err);
                            return null;
                        }
                        decimal LeaveQty = order.Order.Qty - AlreadyQty;
                        order.Order.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.ItemConfirmQty].Value);
                        if (order.Order.Item.Qty > LeaveQty)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("��������δȷ�ϵ���������ˢ�£�");
                            return null;
                        }

                        if (order.Order.Item.Qty <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("������������㣬���������룡");
                            return null;
                        }

                        obj = order.Order;
                        obj.Item.MinFee = undrug.MinFee;
                        obj.Item.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.Price].Value);
                        obj.ExecOper = o;
                        obj.ExecOper.Dept = oper.Dept;//order.Order.ExeDept.Clone();
                        obj.Oper = o;
                        obj.User03 = order.ID;
                        //ִ���豸
                        //by yuyun 08-7-7{810581A3-6DF5-49af-8A5F-D7F843CBEA89}
                        if (this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.Machine].Value != null)
                        {
                            obj.Item.User01 = this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.Machine].Value.ToString();
                        }
                        //ִ�м�ʦ
                        if (this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.Operator].Value != null)
                        {
                            obj.Item.User02 = this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.Operator].Value.ToString();
                        }
                        alFeeOrder.Add(obj);
                    }

                }
            }

            return alFeeOrder;
        }

        /// <summary>
        /// ����е������շ�����ת����FeeItemList
        /// </summary>
        /// <returns></returns>
        private ArrayList GetNewFeeItemList()
        {
            int rowCount = this.fpExecOrder.Sheets[0].RowCount;
            ArrayList alFeeItemList = new ArrayList();
            for (int i = 0; i < rowCount; i++)
            {
                //{A22E7A8E-DE5E-40fc-8273-F774B286B7C8}����������Ŀ�����۴���
                if (FS.FrameWork.Function.NConvert.ToInt32(this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.IsExec].Value)==(int)EnumCheckState.ComfirmComplete)
                {
                    FS.HISFC.Models.Base.OperEnvironment o = new FS.HISFC.Models.Base.OperEnvironment();
                    o.ID = oper.ID;
                    o.Name = oper.Name;
                    o.Dept = oper.Dept;
                    o.OperTime = this.dtNow;
                    //------------------------
                    if (this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.OrderType].Text == "NEW")
                    {
                        FS.HISFC.Models.Base.Item item = new FS.HISFC.Models.Base.Item(); ;
                        FS.HISFC.Models.Fee.Item.Undrug undrug = new FS.HISFC.Models.Fee.Item.Undrug();
                        item = this.fpExecOrder.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Base.Item;
    
                        if (item == null)
                        {
                            continue;
                        }

                        if (item is FS.HISFC.Models.Fee.Item.Undrug)
                        {
                            #region ��ҩƷ
                            undrug = this.feeManager.GetUndrugByCode(item.ID);
                            //{F6A2DCD5-10B9-4fac-8ACB-D4B6FE9D684F}��סԺ�����˵���Ŀ��unitflagΪ��ʱ���÷���û���ϵ���ʾȷ�ϳɹ���
                            if (string.IsNullOrEmpty(undrug.UnitFlag))
                            {
                                MessageBox.Show(undrug.Name + "����Ŀ�Ȳ�����ϸ��Ŀ��Ҳ����������Ŀ�����ܶ�����мƷѣ�������ά������Ŀ��Ϣ��");
                                return null;
                            }
                            //----------------------------------------------
                            if (undrug.UnitFlag == "0")
                            {
                                //{A22E7A8E-DE5E-40fc-8273-F774B286B7C8}����������Ŀ�����۴���
                                FS.HISFC.Models.Order.Inpatient.Order obj = new FS.HISFC.Models.Order.Inpatient.Order();

                                obj.Item = item;
                                obj.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.ItemConfirmQty].Text);

                                if (obj.Qty <= 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("������������㣬���������룡");
                                    return null;
                                }

                                obj.ReciptDept = oper.Dept;
                                obj.ReciptDoctor.ID = oper.ID;
                                obj.ExecOper = o;
                                obj.Unit = item.PriceUnit;
                                obj.Oper = o;
                                obj.ExeDept = oper.Dept;//by yuyun 08-8-12 ����ȷ�Ͽ���
                                //{810581A3-6DF5-49af-8A5F-D7F843CBEA89}
                                obj.Item.User01 = this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.Machine].Text;//ִ���豸
                                obj.Item.User02 = this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.Operator].Text;//ִ�м�ʦ
                                alFeeItemList.Add(obj);
                            }
                            else if (undrug.UnitFlag == "1")
                            {
                                FS.HISFC.BizProcess.Integrate.Manager ztManager = new FS.HISFC.BizProcess.Integrate.Manager();
                                ztManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                ArrayList alZtDetail = ztManager.QueryUndrugPackageDetailByCode(undrug.ID);
                                foreach (FS.HISFC.Models.Fee.Item.Undrug undrugitem in alZtDetail)
                                {
                                    //{A22E7A8E-DE5E-40fc-8273-F774B286B7C8}����������Ŀ�����۴���
                                    FS.HISFC.Models.Order.Inpatient.Order obj = new FS.HISFC.Models.Order.Inpatient.Order();

                                    FS.HISFC.Models.Fee.Item.Undrug myUndrug = this.feeManager.GetUndrugByCode(undrugitem.ID);
                                    obj.Item = myUndrug as FS.HISFC.Models.Base.Item;
                                    obj.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.ItemConfirmQty].Text);
                                    if (obj.Qty <= 0)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show("������������㣬���������룡");
                                        return null;
                                    }
                                    //{6EFEC5EC-2258-4d3e-877B-179215E2F783} ���¼�����ϸ����
                                    obj.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.ItemConfirmQty].Text) * undrugitem.Qty;
                                    if (obj.Item.Qty <= 0)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show("������������㣬���������룡");
                                        return null;
                                    }
                                    obj.ReciptDept = oper.Dept;
                                    obj.ReciptDoctor.ID = oper.ID;
                                    obj.Unit = undrugitem.PriceUnit;
                                    obj.ExecOper = o;
                                    obj.ExeDept = oper.Dept;
                                    obj.Oper = o;
                                    //{810581A3-6DF5-49af-8A5F-D7F843CBEA89}
                                    obj.Item.User01 = this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.Machine].Text;//ִ���豸
                                    obj.Item.User02 = this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.Operator].Text;//ִ�м�ʦ
                                    alFeeItemList.Add(obj);
                                }
                            }
                            #endregion

                        }
                        else if (item is FS.HISFC.Models.Pharmacy.Item)
                        {
                            #region ҩƷ

                            FS.HISFC.Models.Pharmacy.Item pharmacy = pharmacyManager.GetItem(item.ID);

                            FS.HISFC.Models.Order.Inpatient.Order obj = new FS.HISFC.Models.Order.Inpatient.Order();
                            obj.Item = pharmacy;
                            obj.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.ItemConfirmQty].Text);

                            if (obj.Qty <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(string.Format("{0}��������������㣬���������룡", item.Name));
                                return null;
                            }
                            string priceUnit = this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.Unit].Text;
                            decimal price = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Function.NConvert.ToDecimal(this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.Price].Text));
                            obj.ReciptDept = oper.Dept;
                            obj.ReciptDoctor.ID = oper.ID;
                            obj.ExecOper = o;

                            obj.Item.Qty = obj.Qty;
                            obj.Unit = priceUnit;
                            obj.Item.PriceUnit = priceUnit;

                            obj.Oper = o;


                            //����ҩƷid��ȡҩƷʵ��
                            FS.HISFC.Models.Pharmacy.Storage drugStorate = null;

                            if (this.drugStorageDept == EnumDrugStorageDept.CurrentLoginDept)
                            {
                                //ִ�п���
                                obj.ExeDept = oper.Dept;
                            }
                            else if (this.drugStorageDept == EnumDrugStorageDept.PatientInDept)
                            {
                                obj.ExeDept = this.myPatient.PVisit.PatientLocation.Dept;
                            }


                            obj.Item.User01 = this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.Machine].Text;//ִ���豸
                            obj.Item.User02 = this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.Operator].Text;//ִ�м�ʦ
                            alFeeItemList.Add(obj);
                            #endregion
                        }

                    }
                }
            }

            return alFeeItemList;
        }

        /// <summary>
        /// �������ת����execorder
        /// </summary>
        /// <returns></returns>
        private ArrayList GetExecOrder()
        {
            int rowCount = this.fpExecOrder.Sheets[0].RowCount;
            ArrayList alNeedExecOrder = new ArrayList();
            for (int i = 0; i < rowCount; i++)
            {

                if (FS.FrameWork.Function.NConvert.ToInt32(this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.IsExec].Value)==(int)EnumCheckState.ComfirmComplete)
                {
                    FS.HISFC.Models.Base.OperEnvironment o = new FS.HISFC.Models.Base.OperEnvironment();
                    o.ID = oper.ID;
                    o.Name = oper.Name;
                    o.OperTime = this.dtNow;
                    o.Dept = oper.Dept;
                    if (this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.OrderType].Text == "ORDER")
                    {
                        FS.HISFC.Models.Order.ExecOrder order = new FS.HISFC.Models.Order.ExecOrder(); ;
                        order = (FS.HISFC.Models.Order.ExecOrder)this.fpExecOrder.Sheets[0].Rows[i].Tag;

                        order.Order.ExecOper = o;
                        order.ExecOper = o;
                        order.ExecOper.Dept = order.Order.ExeDept.Clone();
                        order.IsExec = order.IsExec;
                        order.ChargeOper = o;
                        order.IsCharge = order.IsCharge;
                        order.Order.User03 = order.ID;
                        alNeedExecOrder.Add(order);
                    }

                }
            }

            return alNeedExecOrder;
        }

        /// <summary>
        /// ��ȡȡ���շѵ�ִ�е�
        /// </summary>
        /// <returns></returns>
        private ArrayList GetCancelComfirmExecOrder()
        {
            int rowCount = this.fpExecOrder.Sheets[0].RowCount;
            ArrayList alNeedExecOrder = new ArrayList();
            for (int i = 0; i < rowCount; i++)
            {

                if (FS.FrameWork.Function.NConvert.ToInt32(this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.IsExec].Value) == (int)EnumCheckState.ComfirmCancel)
                {
                    FS.HISFC.Models.Base.OperEnvironment o = new FS.HISFC.Models.Base.OperEnvironment();
                    o.ID = oper.ID;
                    o.Name = oper.Name;
                    o.OperTime = this.dtNow;
                    o.Dept = oper.Dept;
                    if (this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.OrderType].Text == "ORDER")
                    {
                        FS.HISFC.Models.Order.ExecOrder order= (FS.HISFC.Models.Order.ExecOrder)this.fpExecOrder.Sheets[0].Rows[i].Tag;

                        order.Order.ExecOper = o;
                        order.ExecOper = o;
                        order.ExecOper.Dept = order.Order.ExeDept.Clone();
                        order.IsExec = order.IsExec;
                        order.ChargeOper = o;
                        order.Order.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.ItemConfirmQty].Value);

                        if (order.Order.Item.Qty <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("������������㣬���������룡");
                            return null;
                        }
                        //���շ�
                        order.IsCharge = order.IsCharge;
                        order.Order.User03 = order.ID;
                        alNeedExecOrder.Add(order);
                    }

                }
            }

            return alNeedExecOrder;
        }

        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        /// <returns></returns>
        private bool ValidState()
        {
            for (int i = 0; i < this.fpExecOrder_Sheet1.RowCount; i++)
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.IsExec].Value))
                {
                    if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpExecOrder_Sheet1.Cells[i, (int)Cols.ItemConfirmQty].Text) <= 0)
                    {
                        MessageBox.Show("��������Ҫȷ�ϵ�����");
                        return false;
                    }

                    //{130607A7-01C7-4bdf-931C-D7CE7AC8111B}
                    if (isNeedOper) 
                    {
                        if (string.IsNullOrEmpty(this.fpExecOrder_Sheet1.Cells[i, (int)Cols.Operator].Text))
                        {
                            MessageBox.Show("������ȷ����.");

                            return false;
                        }
                    }

                    if (this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.OrderType].Text == "ORDER")
                    {
                        decimal totQty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpExecOrder_Sheet1.Cells[i, (int)Cols.ItemQty].Text);
                        decimal alreadQty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpExecOrder_Sheet1.Cells[i, (int)Cols.ItemAlreadConfirmQty].Text);
                        decimal confirmQty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpExecOrder_Sheet1.Cells[i, (int)Cols.ItemConfirmQty].Text);
                        if (totQty < alreadQty + confirmQty)
                        {
                            MessageBox.Show("��ȷ��������������ȷ���������ܴ���" + (totQty - alreadQty).ToString());
                            return false;
                        }
                    }
                    if (this.fpExecOrder_Sheet1.Cells[i, (int)Cols.OrderType].Text == "NEW")
                    {
                        #region {B56C131D-2600-421c-9D51-12A1C214CA1E}
                        FS.HISFC.Models.Base.Item item = new FS.HISFC.Models.Base.Item(); ;
                        item = this.fpExecOrder.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Base.Item;
                        if (item == null)
                        {
                            MessageBox.Show("��������Ҫȷ�ϵ���Ŀ��");
                            return false;
                        }
                        #endregion
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// ����ת����feeitemlist
        /// </summary>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        private ArrayList ChangeOrderToFeeItemList(ArrayList alOrder)
        {
            ArrayList alFeeItemList = new ArrayList();
            foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrder)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                feeItemList.Item = order.Item.Clone();
                feeItemList.Item.Qty = order.Qty;
                feeItemList.Item.PriceUnit = order.Unit;//��λ���¸�
                feeItemList.RecipeOper.Dept = order.ReciptDept.Clone();
                feeItemList.RecipeOper.ID = order.ReciptDoctor.ID;
                feeItemList.RecipeOper.Name = order.ReciptDoctor.Name;
                feeItemList.ExecOper = order.ExecOper.Clone();
                feeItemList.ExecOper.Dept = order.ExeDept.Clone();
                feeItemList.StockOper.Dept = order.StockDept.Clone();
                if (feeItemList.Item.PackQty == 0)
                {
                    feeItemList.Item.PackQty = 1;
                }

                if (feeItemList.Item.ItemType == EnumItemType.Drug)
                {
                    if (feeItemList.Item.PriceUnit.Equals(((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).PackUnit))
                    {
                        feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.Price * feeItemList.Item.Qty), 2);
                        feeItemList.FeePack = "1";
                    }
                    else
                    {
                        feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty), 2);
                        feeItemList.FeePack = "0";
                    }
                }
                else
                {
                    feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty), 2);
                }

                feeItemList.FT.OwnCost = feeItemList.FT.TotCost;
                feeItemList.IsBaby = order.IsBaby;
                feeItemList.IsEmergency = order.IsEmergency;
                feeItemList.Order = order.Clone();
                feeItemList.ExecOrder.ID = order.User03;
                feeItemList.NoBackQty = feeItemList.Item.Qty;
                feeItemList.FTRate.OwnRate = 1;
                feeItemList.BalanceState = "0";
                feeItemList.ChargeOper = order.Oper.Clone();
                feeItemList.FeeOper = order.Oper.Clone();
                feeItemList.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                feeItemList.Item.User01 = order.Item.User01;
                feeItemList.Item.User02 = order.Item.User02;
                alFeeItemList.Add(feeItemList);
            }
            return alFeeItemList;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            int iReturn = 0;
            //FS.HISFC.BizProcess.Integrate.Terminal.Result result = FS.HISFC.BizProcess.Integrate.Terminal.Result.None;
            if (!ValidState())
            {
                return -1;
            }

            #region ������Trans
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.feeManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.confirmIntergrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.orderManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            terminalMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeManager.MessageType = messType;

            //{58B76F7C-A35D-4cbb-8948-8163EA3C5191}
            this.dtNow = this.terminalManager.GetDateTimeFromSysDateTime();
            ArrayList alOrder = new ArrayList();
            ArrayList alFeeItemList = new ArrayList();
            ArrayList alNeedExecOrder = new ArrayList();
            ArrayList alCancelOrder = new ArrayList();
            ArrayList alSendInfo = new ArrayList();

            #endregion

            #region ȡ����Ҫ����������ArrayList

            alOrder = this.GetFeeOrder();
            if (alOrder == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            alFeeItemList = this.GetNewFeeItemList();
            if (alFeeItemList == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            alFeeItemList = this.ChangeOrderToFeeItemList(alFeeItemList);
            alNeedExecOrder = this.GetExecOrder();
            if (alNeedExecOrder == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            //��ȡȡ���շѵ�����
            alCancelOrder = this.GetCancelComfirmExecOrder();
            if (alCancelOrder == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            #endregion

            #region ��ָ�����Ŀ{856164A9-000A-482f-B9F4-2A2FF44F96B3}

            FS.HISFC.BizProcess.Integrate.Manager managerPack = new FS.HISFC.BizProcess.Integrate.Manager();
            FS.HISFC.BizProcess.Integrate.Fee tempManagerFee = new FS.HISFC.BizProcess.Integrate.Fee();

            ArrayList alOrderTemp = new ArrayList();

            for (int i = 0; i < alOrder.Count; i++)
            {
                FS.HISFC.Models.Order.Inpatient.Order oldOrder = alOrder[i] as FS.HISFC.Models.Order.Inpatient.Order;
                FS.HISFC.Models.Fee.Item.Undrug ug = tempManagerFee.GetItem((oldOrder as FS.HISFC.Models.Order.Inpatient.Order).Item.ID);
                if (ug.UnitFlag == "1")
                {
                    ArrayList al = managerPack.QueryUndrugPackageDetailByCode(oldOrder.Item.ID);
                    foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in al)
                    {
                        FS.HISFC.Models.Fee.Item.Undrug tmpUndrug = tempManagerFee.GetItem(undrug.ID);
                        FS.HISFC.Models.Order.Inpatient.Order myorder = null;
                        decimal qty = oldOrder.Qty;
                        myorder = oldOrder.Clone();
                        myorder.Item = tmpUndrug.Clone();
                        myorder.Item.User01 = oldOrder.Item.User01;
                        myorder.Item.User02 = oldOrder.Item.User02;
                        myorder.Qty = qty * undrug.Qty;//����==������Ŀ����*С��Ŀ����
                        myorder.Item.Qty = qty * undrug.Qty;//����==С��Ŀ����
                        myorder.Package.ID = oldOrder.Item.ID;//������Ŀ����
                        myorder.Package.Name = oldOrder.Item.Name; //������Ŀ����
                        myorder.Package.Qty = qty;
                        alOrderTemp.Add(myorder);
                    }
                }
                else
                {
                    alOrderTemp.Add(alOrder[i]);
                }
            }
            alOrder = alOrderTemp;

            #endregion

            #region �ж�

            if ((alOrder == null || alOrder.Count == 0) && (alFeeItemList == null || alFeeItemList.Count == 0) && (alCancelOrder == null || alCancelOrder.Count == 0))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("û��ѡ�񱣴��ҽ�����ݣ�"));
                return 100;
            }

            #endregion

            #region �����շѣ������¿�������

            if (alOrder != null && alOrder.Count > 0)
            {
                iReturn = this.feeManager.FeeItem(this.myPatient, ref alOrder);
                if (iReturn < 0)
                {
                    //FS.FrameWork.Management.PublicTrans.RollBack();
                    //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}
                    feeManager.Rollback();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ���߷���ʧ�ܣ�" + this.feeManager.Err));
                    return iReturn;
                }

                alSendInfo.AddRange(alOrder);

                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in alOrder)
                {
                    //{07D1BACB-8E4F-4ac8-8254-81763D0F0699}
                    iReturn = this.feeManager.UpdateConfirmNumForUndrug(feeItem.RecipeNO, feeItem.SequenceNO, feeItem.Item.Qty, feeItem.BalanceState);
                    if (iReturn < 0)
                    {
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}
                        feeManager.Rollback();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("���·�����ϸ��������ʧ�ܣ�" + this.feeManager.Err));
                        return -1;
                    }
                    iReturn = this.feeManager.UpdateExtFlagForUndrug(feeItem.RecipeNO, feeItem.SequenceNO, "150", feeItem.BalanceState);
                    if (iReturn < 0)
                    {
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}
                        feeManager.Rollback();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("���·�����ϸ��չ���ʧ�ܣ�" + this.feeManager.Err));
                        return -1;
                    }
                }

            }
            if (alFeeItemList != null && alFeeItemList.Count > 0)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in alFeeItemList)
                {
                    if (feeItem.Item.ItemType == EnumItemType.Drug)
                    {
                        //ȡ���
                        if (string.IsNullOrEmpty(feeItem.StockOper.Dept.ID))
                        {
                            FS.HISFC.Models.Pharmacy.Storage storage = this.pharmacyManager.GetItemForInpatient(feeItem.ExecOper.Dept.ID, feeItem.Item.ID);
                            if (storage == null || storage.Item == null || string.IsNullOrEmpty(storage.Item.ID))
                            {
                                feeManager.Rollback();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(string.Format("{0}�ڿ�����Ŀ{1}ʱ����Ӧҩ�������ڴ�ҩƷ��", feeItem.ExecOper.Dept.Name, feeItem.Item.Name) + this.pharmacyManager.Err);
                                return -1;
                            }
                            feeItem.StockOper.Dept.ID = storage.StockDept.ID;
                        }
                    }

                }

                iReturn = this.feeManager.FeeItem(this.myPatient, ref alFeeItemList);
                if (iReturn < 0)
                {
                    //FS.FrameWork.Management.PublicTrans.RollBack();
                    //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}
                    feeManager.Rollback();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ���߷���ʧ�ܣ�" + this.feeManager.Err));
                    return iReturn;
                }

                alSendInfo.AddRange(alFeeItemList);

                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in alFeeItemList)
                {
                    if (feeItem.Item.ItemType == EnumItemType.UnDrug)
                    {
                        //{07D1BACB-8E4F-4ac8-8254-81763D0F0699}
                        iReturn = this.feeManager.UpdateConfirmNumForUndrug(feeItem.RecipeNO, feeItem.SequenceNO, feeItem.Item.Qty, feeItem.BalanceState);
                        if (iReturn < 0)
                        {
                            //FS.FrameWork.Management.PublicTrans.RollBack();
                            //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}
                            feeManager.Rollback();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("���·�����ϸ��������ʧ�ܣ�" + this.feeManager.Err));
                            return -1;
                        }

                        iReturn = this.feeManager.UpdateExtFlagForUndrug(feeItem.RecipeNO, feeItem.SequenceNO, "150", feeItem.BalanceState);
                        if (iReturn < 0)
                        {
                            //FS.FrameWork.Management.PublicTrans.RollBack();
                            //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}
                            feeManager.Rollback();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("���·�����ϸ��չ���ʧ�ܣ�" + this.feeManager.Err));
                            return -1;
                        }
                    }
                    else if (feeItem.Item.ItemType == EnumItemType.Drug)
                    {
                        if (this.pharmacyManager.ApplyOut(this.myPatient, feeItem, feeItem.FeeOper.OperTime, false) == -1)
                        {
                            feeManager.Rollback();
                            MessageBox.Show( this.pharmacyManager.Err);
                            return -1;
                        }
                    }
                }

            }

            #endregion

            #region �����ն�

            #region ȷ����ɵ�����
            //Ŀǰ�������նˣ���Ҫʱ�޸ı����ִ���
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in alOrder)
            {
                FS.HISFC.Models.Terminal.TerminalConfirmDetail detail = new FS.HISFC.Models.Terminal.TerminalConfirmDetail();

                #region ����ȷ����ϸ
                string applySequence = "";
                int sReturn = terminalMgr.GetNextSequence(ref applySequence);
                if (sReturn == -1)
                {
                    feeManager.Rollback();
                    MessageBox.Show("��ȡȷ����ˮ��ʧ��");
                    return -1;
                }
                detail.MoOrder = feeItem.Order.ID;//ҽ����ˮ�� 0
                detail.ExecMoOrder = feeItem.ExecOrder.ID;//ҽ����ִ�е���ˮ��1 
                detail.Sequence = applySequence;//2
                detail.Apply.Item.ID = feeItem.Item.ID;//3
                detail.Apply.Item.Name = feeItem.Item.Name;//4
                detail.Apply.Item.ConfirmedQty = feeItem.Item.Qty;//5
                detail.Apply.ConfirmOperEnvironment.ID = this.oper.ID;//6
                detail.Apply.ConfirmOperEnvironment.Dept.ID = this.oper.Dept.ID;//7
                detail.Apply.ConfirmOperEnvironment.OperTime = this.dtNow;//8
                detail.Status.ID = "0";//9 0-������1-ȡ����2-�˷�
                detail.Apply.Patient.ID = feeItem.Patient.ID;
                detail.Apply.Item.RecipeNO = feeItem.RecipeNO;
                detail.Apply.Item.SequenceNO = feeItem.SequenceNO;
                detail.ExecDevice = feeItem.Item.User01;
                detail.Oper.ID = feeItem.Item.User02;

                #endregion
                if (terminalMgr.InsertInpatientConfirmDetail(detail) == -1)
                {
                    feeManager.Rollback();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ն�ȷ����ϸʧ�ܣ�" + this.terminalMgr.Err));
                    return -1;
                }
            }
            #endregion

            #region ���շѵ�����

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in alFeeItemList)
            {
                FS.HISFC.Models.Terminal.TerminalConfirmDetail detail = new FS.HISFC.Models.Terminal.TerminalConfirmDetail();

                #region ����ȷ����ϸ
                string applySequence = "";
                int sReturn = terminalMgr.GetNextSequence(ref applySequence);
                if (sReturn == -1)
                {
                    feeManager.Rollback();
                    MessageBox.Show("��ȡȷ����ˮ��ʧ��");
                    return -1;
                }
                detail.MoOrder = feeItem.Order.ID;//ҽ����ˮ�� 0
                detail.ExecMoOrder = feeItem.ExecOrder.ID;//ҽ����ִ�е���ˮ��1 
                detail.Sequence = applySequence;//2
                detail.Apply.Item.ID = feeItem.Item.ID;//3
                detail.Apply.Item.Name = feeItem.Item.Name;//4
                detail.Apply.Item.ConfirmedQty = feeItem.Item.Qty;//5
                detail.Apply.ConfirmOperEnvironment.ID = this.oper.ID;//6
                detail.Apply.ConfirmOperEnvironment.Dept.ID = this.oper.Dept.ID;//7
                detail.Apply.ConfirmOperEnvironment.OperTime = this.dtNow;//8
                //---------------------------------------------------------------------------------
                detail.Status.ID = "0";//9 0-������1-ȡ����2-�˷�
                detail.Apply.Patient.ID = feeItem.Patient.ID;
                detail.Apply.Item.RecipeNO = feeItem.RecipeNO;
                detail.Apply.Item.SequenceNO = feeItem.SequenceNO;
                detail.ExecDevice = feeItem.Item.User01;
                detail.Oper.ID = feeItem.Item.User02;
                #endregion
                if (terminalMgr.InsertInpatientConfirmDetail(detail) == -1)
                {
                    feeManager.Rollback();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ն�ȷ����ϸʧ�ܣ�" + this.terminalMgr.Err));
                    return -1;
                }
            }

            #endregion

            #region ȷ��ȡ����ҽ��

            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in alCancelOrder)
            {
                FS.HISFC.Models.Terminal.TerminalConfirmDetail detail = new FS.HISFC.Models.Terminal.TerminalConfirmDetail();

                #region ����ȷ����ϸ
                string applySequence = "";
                int sReturn = terminalMgr.GetNextSequence(ref applySequence);
                if (sReturn == -1)
                {
                    feeManager.Rollback();
                    MessageBox.Show("��ȡȷ����ˮ��ʧ��");
                    return -1;
                }
                detail.MoOrder = execOrder.Order.ID;//ҽ����ˮ�� 0
                detail.ExecMoOrder = execOrder.ID;//ҽ����ִ�е���ˮ��1 
                detail.Sequence = applySequence;//2
                detail.Apply.Item.ID = execOrder.Order.Item.ID;//3
                detail.Apply.Item.Name = execOrder.Order.Item.Name;//4
                detail.Apply.Item.ConfirmedQty = execOrder.Order.Item.Qty;//5
                detail.Apply.ConfirmOperEnvironment.ID = this.oper.ID;//6
                detail.Apply.ConfirmOperEnvironment.Dept.ID = this.oper.Dept.ID;//7
                detail.Apply.ConfirmOperEnvironment.OperTime = this.dtNow;//8
                //---------------------------------------------------------------------------------
                detail.Status.ID = "1";//9 0-������1-ȡ����2-�˷�
                detail.Apply.Patient.ID = execOrder.Order.Patient.ID;
                detail.Apply.Item.RecipeNO = "";
                detail.Apply.Item.SequenceNO = 0;
                detail.ExecDevice = execOrder.Order.Item.User01;
                detail.Oper.ID = execOrder.Order.Item.User02;
                #endregion
                if (terminalMgr.InsertInpatientConfirmDetail(detail) == -1)
                {
                    feeManager.Rollback();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ն�ȷ����ϸʧ�ܣ�" + this.terminalMgr.Err));
                    return -1;
                }
            }

            #endregion

            #endregion

            #region ����ҽ��ȷ�Ϻ��շ�

            #region ȷ����ɵ�ҽ��

            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in alNeedExecOrder)
            {
                execOrder.ExecOper.OperTime = dtNow;
                execOrder.ChargeOper.OperTime = dtNow;

                if (!execOrder.IsExec)
                {
                    execOrder.IsExec = true;
                    execOrder.IsConfirm = true;
                    iReturn = this.orderManager.UpdateRecordExec(execOrder);
                    if (iReturn < 0)
                    {
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}
                        feeManager.Rollback();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҽ��ȷ����Ϣʧ�ܣ�" + this.orderManager.Err));
                        return iReturn;
                    }
                    else if (iReturn == 0)
                    {
                        feeManager.Rollback();
                        MessageBox.Show(this, "����ҽ��ȷ����Ϣʧ�ܣ���" + execOrder.Order.Name + "���Ѿ�ȷ�ϣ���ˢ�£�", "��ʾ>>", MessageBoxButtons.OK);
                        return iReturn;
                    }
                    iReturn = this.orderManager.UpdateOrderStatus(execOrder.Order.ID, 2);
                    if (iReturn < 0)
                    {
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}
                        feeManager.Rollback();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҽ������ִ����Ϣʧ�ܣ�" + this.orderManager.Err));
                        return iReturn;
                    }
                }

                if (!execOrder.IsCharge)
                {
                    execOrder.IsCharge = true;
                    execOrder.IsValid = true;
                    iReturn = this.orderManager.UpdateChargeExec(execOrder);
                    if (iReturn <= 0)
                    {
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}
                        feeManager.Rollback();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҽ���շ���Ϣʧ�ܣ�" + this.orderManager.Err));
                        return iReturn;
                    }
                }
              
            }
            #endregion

            #region ȷ��ȡ����ҽ��

            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in alCancelOrder)
            {
                execOrder.ExecOper.OperTime = dtNow;
                execOrder.ChargeOper.OperTime = dtNow;

                if (!execOrder.IsExec)
                {
                    execOrder.IsExec = true;
                    execOrder.IsConfirm = true;
                    iReturn = this.orderManager.UpdateRecordExec(execOrder);
                    if (iReturn < 0)
                    {
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}
                        feeManager.Rollback();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҽ��ȷ����Ϣʧ�ܣ�" + this.orderManager.Err));
                        return iReturn;
                    }
                    else if (iReturn == 0)
                    {
                        feeManager.Rollback();
                        MessageBox.Show(this, "����ҽ��ȷ����Ϣʧ�ܣ���" + execOrder.Order.Name + "���Ѿ�ȷ�ϣ���ˢ�£�", "��ʾ>>", MessageBoxButtons.OK);
                        return iReturn;
                    }

                    iReturn = this.orderManager.UpdateOrderStatus(execOrder.Order.ID, 2);
                    if (iReturn < 0)
                    {
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}
                        feeManager.Rollback();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҽ������ִ����Ϣʧ�ܣ�" + this.orderManager.Err));
                        return iReturn;
                    }
                }

                if (!execOrder.IsCharge)
                {
                    execOrder.IsValid = false;
                    iReturn = this.orderManager.UpdateChargeExec(execOrder);
                    if (iReturn <= 0)
                    {
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}
                        feeManager.Rollback();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҽ���շ���Ϣʧ�ܣ�" + this.orderManager.Err));
                        return iReturn;
                    }
                }
                
            }

            #endregion

            #endregion

            //������Ϣ
            #region HL7��Ϣ����
            object curInterfaceImplement = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Fee), typeof(FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder));
            if (curInterfaceImplement is FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder)
            {
                FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder curIOrderControl = curInterfaceImplement as FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder;

                int param = curIOrderControl.SendDrugInfo(myPatient, alSendInfo, true);
                if (param == -1)
                {
                    feeManager.Rollback();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("������Ϣʧ�ܣ�" +  curIOrderControl.Err));
                    return -1;
                }
            }

            #endregion

            feeManager.Commit();
           // FS.FrameWork.Management.PublicTrans.Commit();
           

            MessageBox.Show(this,"ȷ�ϳɹ���","��ʾ>>", MessageBoxButtons.OK);

            this.fpExecOrder.Sheets[0].RowCount = 0;
            this.hsRows.Clear();
            ((tvInpatientConfirm)this.tv).Refresh();
            this.Clear();
            this.ucQueryInpatientNo1.Text = string.Empty;
            this.ucQueryInpatientNo1.Focus();
            this.ucQueryInpatientNo1.Select();
            ((tvInpatientConfirm)this.tv).SelectedNode = null; 
          
            //this.OnSetValue(this.myPatient, null);
            //for (int i = this.fpExecOrder.Sheets[0].RowCount - 1; i >= 0; i--)
            //{

            //    if (FS.FrameWork.Function.NConvert.ToInt32(this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.IsExec].Value)==(int)EnumCheckState.ComfirmComplete)
            //    {
            //        if (clearConfirm)
            //        {
            //            this.fpExecOrder.Sheets[0].Rows.Remove(i, 1);
            //        }
            //        else
            //        {
            //            this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.IsExec].Value = (int)EnumCheckState.ComfirmComplete;
            //            this.fpExecOrder.Sheets[0].Cells[i, (int)Cols.IsExec].Locked = true;
            //            this.fpExecOrder_Sheet1.Rows[i].BackColor = System.Drawing.Color.Azure;
            //        }
            //    }
            //}

            #region �������뵥 {6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} ����������뵥 yangw 20100504

            if (!string.IsNullOrEmpty(isUseDL) && isUseDL == "1")
            {
                //if (PACSApplyInterface == null)
                //{
                //    PACSApplyInterface = new FS.ApplyInterface.HisInterface();
                //}
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in alOrder)
                {
                    if (f.Item.SysClass.ID.ToString() == "UC" && f.Order.ID != null)
                    {
                        try
                        {
                            string applyNo = string.Empty;
                            terminalMgr.GetApplyNoByOrderNo(f.Order.ID, ref applyNo);
                            FS.HISFC.Models.Order.Inpatient.Order order = orderManager.QueryOneOrder(f.Order.ID);
                            applyNo = order.ApplyNo;
                            //int a = PACSApplyInterface.Charge(applyNo, "1");
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("���µ������뵥�շѱ�־ʱ����\n" + e.Message);
                        }
                    }
                }
            }
            #endregion
            #region  ��ӡ
            if (this.IsPrint)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList obj in alOrder)
                {
                    if (OnPrint(new object(), obj) == -1)
                    {
                        return -1;
                    }
                }
            }
            #endregion
            return 0;
        }

        /// <summary>
        /// ��ʾ������뵥
        /// </summary>
        private void ShowPacsApply(int rowIndex)
        {
            if (!string.IsNullOrEmpty(isUseDL) && isUseDL == "1")
            {
                #region {5E5299D8-95A2-4498-B2F1-52D00E4FB11A} UpdateApply��Ҫʹ��FS.HISFC.Components.PacsApply.HisInterface,�Ժ���Ҫ�������뵥�ع���FS.ApplyInterface.HisInterface��
                //if (PACSApplyInterface == null)
                //{
                //    PACSApplyInterface = new FS.ApplyInterface.HisInterface();
                //}
                //int rowIndex = this.fpExecOrder.Sheets[0].ActiveRowIndex;
                if (rowIndex == -1)
                {
                    return;
                }
                //if (PACSApplyInterfaceNew == null)
                //{
                //    PACSApplyInterfaceNew = new FS.HISFC.Components.PacsApply.HisInterface(FS.FrameWork.Management.Connection.Operator.ID, (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Dept.ID);
                //}                
                #endregion
                
                if (this.fpExecOrder.Sheets[0].Cells[rowIndex, (int)Cols.OrderType].Text == "ORDER")
                {
                    FS.HISFC.Models.Order.ExecOrder exeOrder = new FS.HISFC.Models.Order.ExecOrder(); ;
                    exeOrder = (FS.HISFC.Models.Order.ExecOrder)this.fpExecOrder.Sheets[0].Rows[rowIndex].Tag;
                    FS.HISFC.Models.Order.Inpatient.Order order = orderManager.QueryOneOrder(exeOrder.Order.ID);
                    if (order == null || order.Item.SysClass.ID.ToString() != "UC")
                        return;
                    if (!string.IsNullOrEmpty(order.ApplyNo))
                    {
                        #region {5E5299D8-95A2-4498-B2F1-52D00E4FB11A}
                        //if (PACSApplyInterface.UpdateApply(order.ApplyNo) < 0)
                        //if (PACSApplyInterfaceNew.UpdateApply(order.ApplyNo) < 0)
                        #endregion
                        {
                            MessageBox.Show("��ѯ�������뵥ʧ�ܣ�");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ����ϼƽ��
        /// </summary>
        private void ShowTotFee()
        {
            decimal totFee = 0;
            for (int i = 0; i < this.fpExecOrder_Sheet1.RowCount; i++)
            {
                if (this.fpExecOrder_Sheet1.Cells[i, (int)Cols.IsExec].Value !=null && this.fpExecOrder_Sheet1.Cells[i, (int)Cols.IsExec].Value.ToString() == "0")
                {
                    if (this.fpExecOrder_Sheet1.Cells[i, (int)Cols.TotCost].Value != null)
                    {
                        totFee = decimal.Parse(this.fpExecOrder_Sheet1.Cells[i, (int)Cols.TotCost].Value.ToString()) + totFee;
                    }
                }
            }
            this.lblTotCost.Text = "��" + totFee.ToString();
        }

        public override void Refresh()
        {
            //FS.HISFC.Models.RADT.PatientInfo temp = this.myPatient.Clone();

            DateTime now = this.terminalMgr.GetDateTimeFromSysDateTime();
            DateTime beginTime = DateTime.MinValue;
            if ((now - DateTime.MinValue).Days > this.showdays)
            {
                beginTime = now.AddDays(-this.showdays);
            }
            ((tvInpatientConfirm)this.tv).Init(beginTime, now);
            RefleshDeptList();
            this.ShowTotFee();
            //this.OnSetValue(temp, null);
            Application.DoEvents();
            base.Refresh();
        }
        #endregion

        #region �¼�


        void ucQueryInpatientNo1_myEvent()
        {
            //���
            this.Clear();

            //�ж��Ƿ��иû���
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                if (this.ucQueryInpatientNo1.Err == "")
                {
                    ucQueryInpatientNo1.Err = "�˻��߲���Ժ!";
                }
                FS.FrameWork.WinForms.Classes.Function.Msg(this.ucQueryInpatientNo1.Err, 211);

                this.ucQueryInpatientNo1.Focus();
                return;
            }
            //��ȡסԺ�Ÿ�ֵ��ʵ��
            FS.HISFC.Models.RADT.PatientInfo patientInfo  = this.radtIntegrate .GetPatientInfomation(this.ucQueryInpatientNo1.InpatientNo);

            if (patientInfo == null) 
            {
                MessageBox.Show(this.radtIntegrate.Err);
                return;
            };

            ((tvInpatientConfirm)this.tv).SelectedNode = ((tvInpatientConfirm)this.tv).FindDefaultNode(patientInfo);

            this.OnSetValue(patientInfo, null);
        }

        void ckComplete_CheckedChanged(object sender, EventArgs e)
        {
            if (this.fpExecOrder_Sheet1.RowCount > 0)
            {
                if (this.ckComplete.Checked)
                {
                    this.fpExecOrder_Sheet1.Cells[0, 0, this.fpExecOrder_Sheet1.RowCount - 1, 0].Value = (int)EnumCheckState.ComfirmComplete;
                }
                else if (this.ckCancel.Checked)
                {
                    this.fpExecOrder_Sheet1.Cells[0, 0, this.fpExecOrder_Sheet1.RowCount - 1, 0].Value = (int)EnumCheckState.ComfirmCancel;
                }
                else if (this.ckNone.Checked)
                {
                    this.fpExecOrder_Sheet1.Cells[0, 0, this.fpExecOrder_Sheet1.RowCount - 1, 0].Value = (int)EnumCheckState.None;
                }
            }
        }

        /// <summary>
        /// ѡ����Ŀ�б�����
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int ucItemList_SelectItem(Keys key)
        {
            return this.InsertItem();

        }

        /// <summary>
        /// �˵�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public override FS.FrameWork.WinForms.Forms.ToolBarService Init(object sender, object neuObject, object param)
        {
            base.Init(sender, neuObject, param);

            this.toolBarService.AddToolButton("����", "������Ŀ�Ļ�����Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            this.toolBarService.AddToolButton("ˢ��", "ˢ�»�����Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.Sˢ��, true, false, null);
            #region {53061BA2-33FC-469d-9773-9A9454023612}
            this.toolBarService.AddToolButton("ɾ��", "ɾ�������Ĳ�����Ŀ�Ļ�����Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            #endregion
            //by yuyun 08-7-8{C46CFCDE-132B-47c0-ADAC-4AD73DA2FD90}
            //this.toolBarService.AddToolButton("��������", "��������", FS.FrameWork.WinForms.Classes.EnumImageList.Dҽ��, true, false, null);
            //by yuyun 08-7-8
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    if (this.seeAll)
                    {
                        MessageBox.Show("��ѯȫԺȷ����Ϣʱ ���������շ���Ŀ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    this.AddNewRow();
                    break;
                #region {53061BA2-33FC-469d-9773-9A9454023612}
                case "ɾ��":
                    DeleteItemEvent();
                    break;
                #endregion
                case "ˢ��":
                    this.Refresh();
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        void fpExecOrder_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            if (e.Column == (int)Cols.IsExec)
            {
                if (e.EditingControl is FarPoint.Win.FpMultiOption)
                {
                    switch (((FarPoint.Win.FpMultiOption)e.EditingControl).Value)
                    {
                        case (int)EnumCheckState.ComfirmComplete:
                            ((FarPoint.Win.FpMultiOption)e.EditingControl).ForeColor = Color.Blue;
                            this.fpExecOrder_Sheet1.Cells[e.Row, e.Column].ForeColor = Color.Blue;
                            this.ShowTotFee();
                            break;
                        case (int)EnumCheckState.ComfirmCancel:
                            ((FarPoint.Win.FpMultiOption)e.EditingControl).ForeColor = Color.Red;
                            this.fpExecOrder_Sheet1.Cells[e.Row, e.Column].ForeColor = Color.Red;
                            this.ShowTotFee();
                            break;
                        case (int)EnumCheckState.None:
                            ((FarPoint.Win.FpMultiOption)e.EditingControl).ForeColor = Color.Black;
                            this.fpExecOrder_Sheet1.Cells[e.Row, e.Column].ForeColor = Color.Black;
                            this.ShowTotFee();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        //by yuyun 08-7-7{810581A3-6DF5-49af-8A5F-D7F843CBEA89}
        private int fpExecOrder_SetItem(FS.FrameWork.Models.NeuObject obj)
        {
            this.fpExecOrder_Sheet1.Cells[this.fpExecOrder_Sheet1.ActiveRowIndex, this.fpExecOrder_Sheet1.ActiveColumnIndex].Text = obj.Name;
            this.fpExecOrder_Sheet1.Cells[this.fpExecOrder_Sheet1.ActiveRowIndex, this.fpExecOrder_Sheet1.ActiveColumnIndex].Tag = obj;
            return 0;
        }

        /// <summary>
        /// ���EditChange
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpExecOrder_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            string source = this.fpExecOrder.Sheets[0].Cells[e.Row, (int)Cols.OrderType].Text.ToString();
            if (source == "NEW")//e.Row == this.currentRow &&
            {
                if (e.Column == (int)Cols.ItemName||e.Column==(int)Cols.ItemCode)
                {
                    System.Windows.Forms.Control cellControl = this.fpExecOrder.EditingControl;
                    //����λ��
                    this.ucItemList.Location = new System.Drawing.Point(cellControl.Location.X, cellControl.Location.Y + cellControl.Height + 80);
                    ucItemList.BringToFront();
                    // ������Ŀ
                    this.ucItemList.Filter(this.fpExecOrder_Sheet1.ActiveCell.Text);
                    this.ucItemList.Visible = true;
                    // ���浱ǰ�У����ڱ�֤�ƶ����¼�ͷ���ı䵱ǰ��¼
                    this.fpExecOrder_Sheet1.ActiveRowIndex = e.Row;
                    this.currentRow = e.Row;
                }
                else
                {
                    this.UnDisplayUcItemList();
                }
            }

           
        }

        /// <summary>
        /// ���س�
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int fpExecOrder_KeyEnter(Keys key)
        {
            if (key == Keys.Up)
            {
                if (this.ucItemList.Visible)
                {

                    this.ucItemList.PriorRow();
                    this.fpExecOrder_Sheet1.ActiveRowIndex = this.currentRow;
                }
                return 0;
            }
            if (key == Keys.Escape)
            {
                if (this.ucItemList.Visible)
                {

                    this.ucItemList.Visible = false;
                }
                return 0;
            }
            if (key == Keys.Down)
            {
                if (this.ucItemList.Visible)
                {
                    this.ucItemList.NextRow();
                    this.fpExecOrder_Sheet1.ActiveRowIndex = this.currentRow;
                }
                return 0;
            }

            if (key == Keys.Enter)
            {
                if (this.fpExecOrder.Sheets[0].ActiveColumnIndex == (int)Cols.ItemName)
                {
                    if (this.InsertItem() > 0)
                    {
                        this.fpExecOrder_Sheet1.ActiveColumnIndex = (int)Cols.ItemConfirmQty;
                        return 0;
                    }
                }
                #region ����ȷ������
                if (this.fpExecOrder.Sheets[0].ActiveColumnIndex == (int)Cols.ItemConfirmQty)
                {
                    decimal price = FS.FrameWork.Function.NConvert.ToDecimal(this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.Price].Text);
                    decimal qty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.ItemConfirmQty].Text);
                    this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.TotCost].Text = Convert.ToString(price * qty);

                    if (price == 0)//by zhouxs 2007-10-28
                    {
                        this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder_Sheet1.ActiveRowIndex, (int)Cols.Price].Locked = false;
                        this.fpExecOrder.Sheets[0].SetActiveCell(this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.Price);
                        return 0;//end zhouxs
                    }

                    if (this.fpExecOrder_Sheet1.ActiveRowIndex == this.fpExecOrder_Sheet1.Rows.Count - 1)
                    {
                        fpExecOrder_Sheet1.SetActiveCell(this.fpExecOrder_Sheet1.ActiveRowIndex, (int)Cols.ItemConfirmQty);
                        if (qty > 0)
                        {
                            if (DialogResult.Yes.Equals(MessageBox.Show("�Ƿ��������շ���Ŀ?", "ҽ���ն�ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question)))
                            {
                                this.AddNewRow();
                            }
                        }
                    }
                    else
                    {
                        fpExecOrder_Sheet1.SetActiveCell(this.fpExecOrder_Sheet1.ActiveRowIndex + 1, (int)Cols.ItemName);
                    }
                    return 0;
                }
                #endregion
                if (this.fpExecOrder.Sheets[0].ActiveColumnIndex == (int)Cols.Price)
                {
                    decimal price = FS.FrameWork.Function.NConvert.ToDecimal(this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.Price].Text);
                    decimal qty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.ItemConfirmQty].Text);
                    this.fpExecOrder.Sheets[0].Cells[this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.TotCost].Text = Convert.ToString(price * qty);
                    this.fpExecOrder.Sheets[0].SetActiveCell(this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.ItemConfirmQty);
                    return 0;
                }
                //by yuyun 08-7-8{810581A3-6DF5-49af-8A5F-D7F843CBEA89}
                if (this.fpExecOrder.Sheets[0].ActiveColumnIndex == (int)Cols.Machine)
                {
                    this.fpExecOrder.Sheets[0].SetActiveCell(this.fpExecOrder.Sheets[0].ActiveRowIndex, (int)Cols.Operator);
                }

            }
            this.ShowTotFee();
            return 0;
        }

        private void fpExecOrder_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            if (!System.IO.File.Exists(this.filePath))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(this.filePath, this.dtExecOrder, ref this.dvExecOrder, this.fpExecOrder_Sheet1);
            }

            else
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpExecOrder_Sheet1, this.filePath);
            }
        }

        /// <summary>
        /// ѡ���װ��λ����С��λʱ�򴥷�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpExecOrder_ComboSelChange(object sender, EditorNotifyEventArgs e)
        {
            if (e.Column == (int)Cols.Unit)
            {
                FarPoint.Win.Spread.CellType.ComboBoxCellType comboType = (FarPoint.Win.Spread.CellType.ComboBoxCellType)this.fpExecOrder_Sheet1.Cells[e.Row, e.Column].CellType;

                string text = e.EditingControl.Text;
                if (((FarPoint.Win.FpCombo)e.EditingControl).SelectedIndex == 0)
                {
                    //����С��λ�շ�
                    object obj = this.fpExecOrder_Sheet1.Rows[e.Row].Tag;
                    if (obj == null)
                    {
                        return;
                    }
                    decimal price = FS.FrameWork.Public.String.FormatNumber(
                        (obj as FS.HISFC.Models.Pharmacy.Item).Price /
                        (obj as FS.HISFC.Models.Pharmacy.Item).PackQty, 4);

                    this.fpExecOrder_Sheet1.SetValue(e.Row, (int)Cols.Price, price, false);
                    //�����ܶ�
                    text = this.fpExecOrder_Sheet1.GetText(e.Row, (int)Cols.ItemConfirmQty);//����
                    if (text == string.Empty)
                    {
                        text = "0";
                    }
                    decimal qty = NConvert.ToDecimal(text);


                    this.fpExecOrder_Sheet1.SetValue(e.Row, (int)Cols.TotCost, price * qty, false);
                }
                else if (((FarPoint.Win.FpCombo)e.EditingControl).SelectedIndex == 1)
                {
                    //����װ��λ�շ�
                    object obj = this.fpExecOrder_Sheet1.Rows[e.Row].Tag;
                    if (obj == null)
                    {
                        return;
                    }
                    decimal price = (obj as FS.HISFC.Models.Pharmacy.Item).Price;
                    this.fpExecOrder_Sheet1.SetValue(e.Row, (int)Cols.Price, price, false);
                    //�����ܶ�
                    text = this.fpExecOrder_Sheet1.GetText(e.Row, (int)Cols.ItemConfirmQty);//����
                    if (text == string.Empty)
                    {
                        text = "0";
                    }
                    decimal qty = NConvert.ToDecimal(text);


                    this.fpExecOrder_Sheet1.SetValue(e.Row, (int)Cols.TotCost, price * qty, false);
                }
            }
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return base.OnSave(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            #region {019B78E5-8076-4d17-8CEE-4F2FC66AD0D3}
            return base.OnPrint(sender, neuObject);
            #endregion
            if (this.terminalInterface == null)
            {
                this.terminalInterface = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(TerminalInterface)) as TerminalInterface;
                if (this.terminalInterface == null)
                {
                    MessageBox.Show("��ýӿ�TerminalInterface����\n������û��ά����صĴ�ӡ�ؼ����ӡ�ؼ�û��ʵ�ֽӿ�TerminalInterface\n����ϵͳ����Ա��ϵ��");
                    return -11;
                }
            }
            if (this.terminalInterface.Reset() == -1)
            {
                return -1;
            }
            //for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            //{
            if (this.terminalInterface.ControlValue(neuObject) == -1)
            {
                return -1;
            }
            if (this.terminalInterface.Print() == -1)
            {
                return -1;
            }
            return base.OnPrint(sender, neuObject);
        }

        private void ucInpatientConfirm_Load(object sender, EventArgs e)
        {
            if (this.tv != null)
            {
                try
                {
                    tvInpatientConfirm t = (tvInpatientConfirm)tv;
                    t.IsExpand = false;
                    if (this.seeAll)
                    {
                        t.OperDept = "all";
                        //t.Init();  
                    }
                    DateTime now = this.terminalMgr.GetDateTimeFromSysDateTime();
                    DateTime beginTime = DateTime.MinValue;
                    if ((now - DateTime.MinValue).Days > this.showdays)
                    {
                        beginTime = now.AddDays(-this.showdays);
                    }
                    t.Init(beginTime, now);

                    RefleshDeptList();

                    this.BeginAutoRefresh();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void fpExecOrder_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            ShowPacsApply(e.Row);
        }

        public override int SetPrint(object sender, object neuObject)
        {
            DateTime now = this.terminalMgr.GetDateTimeFromSysDateTime();
            DateTime beginTime = DateTime.MinValue;
            if ((now - DateTime.MinValue).Days > this.showdays)
            {
                beginTime = now.AddDays(-this.showdays);
            }
            DateTime endTime = DateTime.MinValue;
            int returnValues = FS.FrameWork.WinForms.Classes.Function.ChooseDate(ref beginTime, ref endTime);
            if (returnValues < 0)
            {
                return -1;
            }
            tvInpatientConfirm t = (tvInpatientConfirm)tv;
            if (t != null)
            {
                t.Init(beginTime, endTime);

                RefleshDeptList();
            }
            return 1;
        }

        private void RefleshDeptList()
        { 
            tvInpatientConfirm t = (tvInpatientConfirm)tv;
            ArrayList allDept = new ArrayList();
            if (t.AllCurDept != null && t.AllCurDept.Count > 0)
            {
                this.cmbDept.Items.Clear();
                foreach (FS.FrameWork.Models.NeuObject s in t.AllCurDept)
                {
                    allDept.Add(SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(s.ID));
                }
                this.cmbDept.AddItems(allDept);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            if (this.autoRefreshCallBack != null)
            {
                this.autoRefreshCallBack = null;
            }

            if (this.autoRefreshTimer != null)
            {
                this.autoRefreshTimer.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion

        #region �Զ�ˢ��

        /// <summary>
        /// �ն����õ�ˢ�¼��
        /// </summary>
        private int refreshInterval = 10;

        [Category("�ؼ�����"), Description("ˢ�¼��ʱ�䣬Ĭ��Ϊ10��")]
        public int RefreshInterval
        {
            get
            {
                return refreshInterval;
            }
            set
            {
                this.refreshInterval = value;
            }
        }

        private System.Threading.Timer autoRefreshTimer = null;
        private System.Threading.TimerCallback autoRefreshCallBack = null;
        private delegate void autoRefreshHandler();
        private autoRefreshHandler autoRefreshEven;
        private bool isRefresh = false;

        private void BeginAutoRefresh()
        {
            if (this.autoRefreshCallBack == null)
            {
                this.autoRefreshCallBack = new System.Threading.TimerCallback(this.AutoRefreshTimerCallback);
            }
            this.autoRefreshTimer = new System.Threading.Timer(this.autoRefreshCallBack, null, refreshInterval * 1000, this.refreshInterval * 1000);
        }

        /// <summary>
        /// ˢ�´����б�
        /// </summary>
        /// <param name="param">������û��ʹ�ã�</param>
        /// <returns></returns>
        private void AutoRefreshTimerCallback(object param)
        {
            if (this.autoRefreshEven == null)
            {
                autoRefreshEven = new autoRefreshHandler(this.AutoRefresh);
            }

            if (this.ParentForm != null)
            {
                this.ParentForm.BeginInvoke(this.autoRefreshEven);
            }

        }

        /// <summary>
        /// �Զ�ˢ��
        /// </summary>
        public void AutoRefresh()
        {
            if (isRefresh)
            {
                return;
            }
            isRefresh = true;
            try
            {
                this.Refresh();
            }
            finally
            {
                isRefresh = false;
            }
        }

        #endregion

        #region IInterfaceContainer ��Ա
        TerminalInterface terminalInterface = null;
        public Type[] InterfaceTypes
        {
            get { return new Type[] { typeof(TerminalInterface) }; }
        }

        #endregion

        private void fpExecOrder_CellClick(object sender, CellClickEventArgs e)
        {
            FS.HISFC.Models.Order.ExecOrder inOrder = fpExecOrder.Sheets[0].Rows[e.Row].Tag as FS.HISFC.Models.Order.ExecOrder;
            if (inOrder != null)
            {
                try
                {
                    this.txtFeeDetail.ReadOnly = true;

                    #region ��ʾ��Ŀ��Ϣ

                    string showInfo = "";

                    //ҽ����ע
                    showInfo += "ҽ����ע��" + inOrder.Order.Memo + "\r\n\r\n";

                    //��Ŀ��Ϣ
                    if (inOrder.Order.Item.ID == "999")
                    {
                        showInfo += inOrder.Order.Item.Name + " �����" + inOrder.Order.Item.Specs + " �����ۡ�" + inOrder.Order.Item.Price.ToString() + "Ԫ/" + inOrder.Order.Item.PriceUnit;
                    }
                    else
                    {
                        if (inOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            showInfo += SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Order.Item.ID).UserCode + " " + inOrder.Order.Item.Name + " �����" + inOrder.Order.Item.Specs + " �����ۡ�" + inOrder.Order.Item.Price.ToString() + "Ԫ/" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Order.Item.ID).PackUnit;
                        }
                        else
                        {
                            showInfo += SOC.HISFC.BizProcess.Cache.Fee.GetItem(inOrder.Order.Item.ID).UserCode + " " + inOrder.Order.Item.Name + " �����" + inOrder.Order.Item.Specs + " �����ۡ�" + inOrder.Order.Item.Price.ToString() + "Ԫ/" + inOrder.Order.Item.PriceUnit;
                        }
                    }
                    if (inOrder.Order.Item.ID != "999")
                    {
                        //�ײ���ϸ
                        if (inOrder.Order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            FS.HISFC.Models.Fee.Item.Undrug undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(inOrder.Order.Item.ID);
                            if (undrug.UnitFlag == "1")
                            {
                                showInfo += "\r\n���ײͰ�������";

                                ArrayList alZt = managerIntegrate.QueryUndrugPackageDetailByCode(inOrder.Order.Item.ID);
                                foreach (FS.HISFC.Models.Fee.Item.UndrugComb comb in alZt)
                                {
                                    FS.HISFC.Models.Fee.Item.Undrug combUndrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(comb.ID);
                                    showInfo += "\r\n" + combUndrug.Name + (string.IsNullOrEmpty(combUndrug.Specs) ? "" : "[" + combUndrug.Specs + "]") + "   ������" + comb.Qty + combUndrug.PriceUnit + "   �۸�" + combUndrug.Price.ToString() + "   ��" + (comb.Qty * combUndrug.Price).ToString();
                                }
                            }
                        }
                    }

                    this.txtFeeDetail.Text = showInfo;

                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }      
    }

    public enum EnumCheckState
    {
        /// <summary>
        /// ȷ���շ�
        /// </summary>
        [FS.FrameWork.Public.Description("�շ�")]
        ComfirmComplete,

        /// <summary>
        /// ȡ���շ�
        /// </summary>
        [FS.FrameWork.Public.Description("���շ�")]
        ComfirmCancel,

        /// <summary>
        /// �ݲ�����
        /// </summary>
        [FS.FrameWork.Public.Description("�ݲ�����")]
        None
    }
}

