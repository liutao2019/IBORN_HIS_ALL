using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.FrameWork.Function;


namespace FS.SOC.Local.Pharmacy.ZhuHai.Extend.ZDWY
{
    /// <summary>
    /// [功能描述: 单据补打]<br></br>
    /// [创 建 者: 梁俊泽]<br></br>
    /// [创建时间: 2006-12]<br></br>
    /// </summary>
    public partial class ucBillRePrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBillRePrint()
        {
            InitializeComponent();
            this.txtFilter.Click += new EventHandler(txtFilter_Click);
        }

        void txtFilter_Click(object sender, EventArgs e)
        {
            this.txtFilter.SelectAll();
        }        

        #region 域变量

        /// <summary>
        /// 权限科室
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 单据类型
        /// </summary>
        private string billtype = "NO";

        /// <summary>
        /// 单据状态
        /// </summary>
        private string billstate = "A";

        /// <summary>
        /// 是否打印
        /// </summary>
        private bool isprint = false;

        /// <summary>
        /// 四舍五入精度
        /// </summary>
        private int decimals = 4; 
       
        /// <summary>
        /// 药品业务管理类
        /// </summary>
        //FS.SOC.HISFC.BizLogic.Pharmacy.InOut InOutManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 药品出入库业务管理类
        /// </summary>
        FS.SOC.HISFC.BizLogic.Pharmacy.InOut inOutMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();

        /// <summary>
        /// 药品库存业务管理类
        /// </summary>
        SOC.HISFC.BizLogic.Pharmacy.Storage storageMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Storage();

        /// <summary>
        /// 入库数据视图
        /// </summary>
        private DataView dvInput = null;

        /// <summary>
        /// 出库数据视图
        /// </summary>
        private DataView dvOutput = null;

        /// <summary>
        /// 入库单打印实例
        /// </summary>
        protected object inputInstance;

        /// <summary>
        /// 出库单打印实例
        /// </summary>
        protected object outputInstance;

        /// <summary>
        /// 显示结果是否与打印内容一致
        /// </summary>
        private bool isShowAsPrint = true;

        #endregion 

    

        #region 属性

        /// <summary>
        /// 单据类型[I入库单 O出库单 D调拨单]
        /// </summary>
        [Browsable(false)]
        public string BillType
        {
            get
            {
                return this.billtype;
            }
            set
            {
                if (value != this.billtype)
                {
                    this.billtype = value;

                    this.txtFilter.Text = "";

                    this.lbStatus.Visible = false;
                    this.cmbStatus.Visible = false;

                    if (value == "I")
                    {
                        this.SetFpForInput();
                    }
                    else if (value == "O")
                    {
                        this.SetFpForOutput();

                    }
                }
            }
        }
        
        /// <summary>
        /// 单据状态
        /// </summary>
        [Browsable(false)]
        public string BillState
        {
            get
            {
                if (this.billstate == null || this.billstate == "")
                {

                    return "A";
                }
                else
                {

                    return this.billstate;
                }
            }
            set
            {
                this.billstate = value;
            }
        }

        /// <summary>
        /// 活动表索引
        /// </summary>
        [Browsable(false)]
        public int ActiveSheet
        {
            get
            {
                return this.neuSpread1.ActiveSheetIndex;
            }
            set
            {
                this.neuSpread1.ActiveSheetIndex = value;
            }
        }
        
        /// <summary>
        /// 是否打印
        /// </summary>
        [Browsable(false)]
        public new bool IsPrint
        {
            get
            {
                return this.isprint;
            }
            set
            {
                this.isprint = value;
            }
        }

        /// <summary>
        /// 数据库精度
        /// </summary>
        [Description("购入价数据显示精度"), Category("设置"), DefaultValue(4)]
        public int Decimals
        {
            get
            {
                return decimals;
            }
            set
            {
                decimals = value;
            }
        }

        #region 
        ///// <summary>
        ///// 是否显示调拨单列表
        ///// </summary>
        //public bool IsShowAttempBill
        //{
        //    get
        //    {
        //        return this.isShowAttempBill;
        //    }
        //    set
        //    {
        //        this.isShowAttempBill = value;
        //    }
        //}
        #endregion 


        /// <summary>
        /// 盘点单显示结果是否与打印内容一致
        /// </summary>
        [Description("盘点单是否分药品类别进行统计"),Category("设置"),Browsable(true)]
        public bool IsShowAsPrint
        {
            get 
            {
               return isShowAsPrint ; 
            }
            set
            { 
                isShowAsPrint = value; 
            }
        }
        #endregion

        #region 工具栏

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("全不选", "清除选择项", FS.FrameWork.WinForms.Classes.EnumImageList.Q全不选, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "全不选")
            {
                this.SelectAll(false);
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();

            return base.OnQuery(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();

            return 1;
        }
     
        #endregion      

        #region 方法

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            this.InitFp();
            this.neuSpread1.ActiveSheetIndex = 0;

            DateTime dt = this.inOutMgr.GetDateTimeFromSysDateTime();
            this.dtEnd.Value = dt;
            dt = dt.AddDays(-3);
            this.dtBegin.Value = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);

            //this.InitStatus();
        }

        /// <summary>
        /// 初始化(入库)状态
        /// </summary>
        private void InitStatus()
        {
            try
            {
                //显示名称，取ID值
                FS.FrameWork.Models.NeuObject ob;
                ArrayList al = new ArrayList();
                this.cmbStatus.Text = "请选择";

                ob = new FS.FrameWork.Models.NeuObject();
                ob.ID = "A";
                ob.Name = "全部";
                al.Add(ob);

                ob = new FS.FrameWork.Models.NeuObject();
                ob.ID = "2";
                ob.Name = "核准";
                al.Add(ob);

                ob = new FS.FrameWork.Models.NeuObject();
                ob.ID = "1";
                ob.Name = "发票[审核]";
                al.Add(ob);

                ob = new FS.FrameWork.Models.NeuObject();
                ob.ID = "0";
                ob.Name = "申请";
                al.Add(ob);

                //将数据源初始化
                this.cmbStatus.DataSource = al;
                this.cmbStatus.DisplayMember = "Name";
                this.cmbStatus.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("初始化查询状态失败>>" + ex.Message));
            }
        }


        /// <summary>
        /// 初始化fp
        /// </summary>
        private void InitFp()
        {
            #region 汇总
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.ColumnCount = 2;
            this.neuSpread1_Sheet1.RowCount = 30;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.AppWorkspace, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.White, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.White, true, true, true, true, true);
            this.neuSpread1_Sheet1.Cells.Get(0, 1).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 1).Text = "操作说明";
            this.neuSpread1_Sheet1.Cells.Get(1, 1).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells.Get(1, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells.Get(1, 1).Text = "一、阅读说明";
            this.neuSpread1_Sheet1.Cells.Get(2, 1).Text = "    1、当您看到此说明时您还未进行任何操作，如果您不明确如何操作，请认真阅读";
            this.neuSpread1_Sheet1.Cells.Get(3, 1).Text = "    2、您阅读时请务必一次记住此说明内容，我们无法让您一边阅读一边操作";
            this.neuSpread1_Sheet1.Cells.Get(4, 1).Text = "    3、当您再需要查看此说明时请选择[帮助]按钮";
            this.neuSpread1_Sheet1.Cells.Get(5, 1).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells.Get(5, 1).Text = "二、条件选择";
            this.neuSpread1_Sheet1.Cells.Get(6, 1).Text = "    1、在左侧单据类型树型列表中选择好单据类型、科室，底色变蓝后有效[适用于所有单据]";
            this.neuSpread1_Sheet1.Cells.Get(7, 1).Font = new System.Drawing.Font("宋体", 9F);
            this.neuSpread1_Sheet1.Cells.Get(7, 1).Text = "    2、选择开始和结束时间，否则默认为4天";
            this.neuSpread1_Sheet1.Cells.Get(8, 1).Text = "    3、如果是入库单请选择状态，否则默认为全部";
            this.neuSpread1_Sheet1.Cells.Get(9, 1).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells.Get(9, 1).Text = "三、如何查询";
            this.neuSpread1_Sheet1.Cells.Get(10, 1).Text = "    1、确保第二步完成后按下[查询]按钮，显示选择条件内单据列表";
            this.neuSpread1_Sheet1.Cells.Get(11, 1).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells.Get(11, 1).Text = "四、为何刷新";
            this.neuSpread1_Sheet1.Cells.Get(12, 1).Text = "    1、在您进入界面时间过长后，可能有其它操作引起数据变动，这时请[刷新]";
            this.neuSpread1_Sheet1.Cells.Get(13, 1).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells.Get(13, 1).Text = "五、查看明细";
            this.neuSpread1_Sheet1.Cells.Get(14, 1).Text = "    1、查询完成后双击列表[汇总表]条目则显示明细";
            this.neuSpread1_Sheet1.Cells.Get(15, 1).Text = "    2、入库单应先选择[选中]栏，打钩后有效[单击选中或取消]，否则默认正单据数据";
            this.neuSpread1_Sheet1.Cells.Get(16, 1).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells.Get(16, 1).Text = "六、如何打印";
            this.neuSpread1_Sheet1.Cells.Get(17, 1).Text = "    1、在汇总表中单击选择要打印的单据";
            this.neuSpread1_Sheet1.Cells.Get(18, 1).Text = "    2、选择[打印]按钮即可打印";
            this.neuSpread1_Sheet1.Cells.Get(19, 1).Text = "    3、入库单可以按发票打印，应先选择[选中]栏，打钩后有效[单击选中或取消]，否则默认正单据数据";          
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Text = "操作说明";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(1).ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "操作说明";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 732F;
            this.neuSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 0F;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.neuSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SheetName = "汇总";
            #endregion

            #region 详细
            this.neuSpread1_Sheet2.Reset();
            this.neuSpread1_Sheet2.ColumnCount = 15;
            this.neuSpread1_Sheet2.RowCount = 50;
            this.neuSpread1_Sheet2.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.AppWorkspace, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.White, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.FromArgb(((System.Byte)(206)), ((System.Byte)(93)), ((System.Byte)(90))), System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.White, true, true, true, true, true);
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet2.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet2.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.SheetName = "详细";
            #endregion
        }

        #endregion

        #region 查询分类

        /// <summary>
        /// 查询 调用时按单据类型、状态、事件查询
        /// </summary>
        public void Query()
        {
            this.neuSpread1.ActiveSheetIndex = 0;
            this.txtFilter.Text = "";
            if (this.BillType == "NO")
            {
                MessageBox.Show(this, Language.Msg("请选择单据类型"), "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return;
            }
            else if (BillType == "I")
            {
                this.QueryInputData();
            }
            else if (BillType == "O")
            {
                this.QueryOutputData();
            }
           
            else
            {
                MessageBox.Show(this, Language.Msg("无法识别的单据类型") + this.BillType, "错误>>", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
           
        }

        #endregion

        #region 打印分类

        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            if (this.neuSpread1_Sheet1.RowCount == 0)
            {
                MessageBox.Show(Language.Msg("没有可打印的数据"));
                return;
            }
            if (this.neuSpread1_Sheet1.ActiveRowIndex < 0)
            {
                MessageBox.Show(Language.Msg("没有选择要打印的单据"));
                return;
            }
            //this.txtFilter.Text = "";
            this.neuSpread1_Sheet2.RowCount = 0;
            this.isprint = true;
            //入库
            if (this.BillType == "I")
            {
                this.PrintInputBill(this.neuSpread1_Sheet1.ActiveRowIndex);
            }
            //出库
            if (this.BillType == "O")
            {
                this.PrintOutputBill(this.neuSpread1_Sheet1.ActiveRowIndex);
            }
          
            this.neuSpread1.ActiveSheetIndex = 1;
        }

        #endregion

        #region 入库单

        /// <summary>
        /// 获取入库单列表
        /// </summary>
        public void QueryInputData()
        {
            DataSet ds = new DataSet();
            if (this.inOutMgr.ExecQuery("Pharmacy.Report.GetInputList", ref ds, this.privDept.ID, this.BillState, this.dtBegin.Value.ToString(), this.dtEnd.Value.ToString()) == -1)
            {
                MessageBox.Show(Language.Msg("获取入库单列表出错"));
                return;
            }
            if (ds.Tables == null || ds.Tables.Count == 0)
            {
                MessageBox.Show(Language.Msg("无入库单数据"));
                return;
            }

            this.neuSpread1_Sheet1.DataSource = ds.Tables[0].DefaultView;

            this.dvInput = ds.Tables[0].DefaultView;

            this.SetFpForInputTot();
        }

        /// <summary>
        /// 获取入库详细信息
        /// </summary>
        /// <param name="billno">入库单号</param>
        /// <returns>input实体数组</returns>
        private ArrayList QueryInputDetail(string billNO)
        {
            if (billNO == null || billNO == "")
            {
                return null;
            }

            ArrayList al = this.inOutMgr.QueryInputInfoByListID(this.privDept.ID, billNO, "AAAA", this.BillState == "A" ? this.BillState.PadLeft(4, 'A') : this.BillState);
            if (al == null)
            {
                MessageBox.Show(Language.Msg("根据单据号获取入库信息失败"));
                return null;
            }
            return al;
        }

        /// <summary>
        /// 根据发票获取入库详细信息
        /// </summary>
        /// <param name="alInputBillNO">入库单号</param>
        /// <param name="alInvoice">发票号</param>
        /// <returns>input实体数组</returns>
        private ArrayList QueryInputDetail(ArrayList alInputBillNO, ArrayList alInvoice)
        {
            if (alInputBillNO.Count == 0)
            {
                return null;
            }
            try
            {
                ArrayList alAllInput = new ArrayList();     //所有查询的入库详细数据    
                //获取所有要查询的单据的所有信息				
                foreach (string inputBillNO in alInputBillNO)
                {
                    ArrayList al = this.QueryInputDetail(inputBillNO);
                    if (al == null)
                    {
                        MessageBox.Show(Language.Msg("根据单据号获取单据列表失败"));
                    }
                    if (al.Count > 0)
                    {
                        alAllInput.AddRange(al);
                    }
                }
                if (alInvoice.Count == 0)
                {
                    return alAllInput;
                }

                //需要显示的入库详细数据
                ArrayList alNeedShowInput = new ArrayList();
                //将发票号＋单据号做为主键加入HashTable
                System.Collections.Hashtable hsInvoiceList = new Hashtable();

                //取得所有发票的入库信息 根据过滤条件加入
                foreach (FS.FrameWork.Models.NeuObject invoice in alInvoice)
                {
                    if (!hsInvoiceList.ContainsKey(invoice.ID + invoice.Name))
                    {
                        hsInvoiceList.Add(invoice.ID + invoice.Name,null);
                    }
                }

                foreach (FS.HISFC.Models.Pharmacy.Input input in alAllInput)
                {
                    if (hsInvoiceList.ContainsKey(input.InvoiceNO + input.InListNO))
                    {
                        alNeedShowInput.Add(input);
                    }
                }

               
                return alNeedShowInput;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("根据发票获取入库详细信息出错>>" + ex.Message));
                return null;
            }

        }

        /// <summary>
        /// 打印、显示入库详细信息 
        /// </summary>
        /// <param name="activerow">单据所在汇总fp的行号</param>
        private void PrintInputBill(int activeRowIndex)
        {
            /*2007-1-20 by zengft
             * 如果用户选则了“选中”，则认为按发票打印入库单，所以显示所有被选中发票的入库数据
             * 如果用户没有选择任何“选中”，则认为全单打印，所以显示整张单据数据(在事件中体现)
             * 如果用户选择了多张单据中的数据，只显示当前活动行的单据中选中的数据
             * 活动行认为是用户在汇总表选中的那一行，并且汇总表显示的数据是按单据号和发票号分开的
             */           

            ArrayList alInvoice = new ArrayList();          //选中打印的发票号
            ArrayList alSelectListNO = new ArrayList();     //选中的入库单号

            bool isMergePrint = false;                      //是否合并显示和打印

            //活动行的单据号
            string activeInputBillNO = this.neuSpread1_Sheet1.Cells[activeRowIndex, (int)ColIndex.ListNOIndex].Text.Trim();
            //当前单据号
            string tempInputBillNO = activeInputBillNO;


            //获取数据
            ArrayList alInputDetail = this.QueryInputDetail(activeInputBillNO);
            if (alInputDetail == null || alInputDetail.Count == 0)
            {
                MessageBox.Show(Language.Msg("没有获得入库详细数据"));

                return;
            }

            //显示数据
            this.AddInputDataToFp(alInputDetail);

            //打印
            if (this.IsPrint)
            {
                Function.PrintBill("0310", ((FS.HISFC.Models.Pharmacy.Input)alInputDetail[0]).PrivType, alInputDetail);

                this.IsPrint = false;
            }
        }

        /// <summary>
        /// 入库详细数据赋值
        /// </summary>
        /// <param name="al"></param>
        private void AddInputDataToFp(ArrayList al)
        {
            FS.SOC.HISFC.BizLogic.Pharmacy.Constant constant = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();

            //将要查明细的单据号显示出来，考虑有多张单据的可能
            string tempBillNO = ((FS.HISFC.Models.Pharmacy.Input)al[0]).InListNO;
            //this.txtFilter.Text = "" + tempBillNO;

            //添加行
            if (this.neuSpread1_Sheet2.RowCount > 0)
            {
                this.neuSpread1_Sheet2.RowCount = 0;
            }
            this.neuSpread1_Sheet2.Rows.Add(0, al.Count + 1);

            //分行赋值
            if (this.neuSpread1_Sheet2.ColumnCount < 11)
            {
                this.neuSpread1_Sheet2.ColumnCount = 11;
            }
            decimal PurchaseTot = 0;
            decimal RetailTot = 0;
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.Input input = al[i] as FS.HISFC.Models.Pharmacy.Input;

                //多单据号
                if (tempBillNO != input.InListNO)
                {
                    tempBillNO = input.InListNO;
                    this.txtFilter.Text += "+" + tempBillNO;
                }

                this.neuSpread1_Sheet2.Cells[i, 0].Text = input.InvoiceNO;
                try
                {
                    if (input.Company.Name == null || input.Company.Name == "")
                    {
                        input.Company.Name = (constant.QueryCompanyByCompanyID(input.Company.ID)).Name;
                    }
                }
                catch
                {
                    try
                    {
                        FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
                        input.Company.Name = (deptMgr.GetDeptmentById(input.Company.ID)).Name;

                    }
                    catch
                    { }
                }
                this.neuSpread1_Sheet2.Cells[i, 1].Text = input.Company.Name;
                this.neuSpread1_Sheet2.Cells[i, 2].Text = input.BatchNO;
                this.neuSpread1_Sheet2.Cells[i, 3].Text = input.Item.Name;

                //单位紧跟数量后显示在同一列
                this.neuSpread1_Sheet2.Cells[i, 4].Text = " " + input.Item.Specs;

                //包装单位作被除数，不能为0
                if (input.Item.PackQty == 0) input.Item.PackQty = 1;
                decimal count = 0;
                count = input.Quantity;

                if (input.ShowState == "0")
                {
                    this.neuSpread1_Sheet2.Cells[i, 5].Text = (count).ToString() + input.Item.MinUnit;
                }
                else
                {
                    this.neuSpread1_Sheet2.Cells[i, 5].Text = (count / input.Item.PackQty).ToString() + input.Item.PackUnit;
                }
                this.neuSpread1_Sheet2.Cells[i, 6].Value = System.Math.Round(input.Item.PriceCollection.PurchasePrice, decimals);
                this.neuSpread1_Sheet2.Cells[i, 7].Value = System.Math.Round(input.Item.PriceCollection.PurchasePrice, decimals) * count / input.Item.PackQty;
                this.neuSpread1_Sheet2.Cells[i, 8].Value = System.Math.Round(input.Item.PriceCollection.RetailPrice, decimals);
                this.neuSpread1_Sheet2.Cells[i, 9].Value = System.Math.Round(input.Item.PriceCollection.RetailPrice, decimals) * count / input.Item.PackQty;
                this.neuSpread1_Sheet2.Cells[i, 10].Value = System.Math.Round(input.Item.PriceCollection.RetailPrice, decimals) * count / input.Item.PackQty
                    - System.Math.Round(input.Item.PriceCollection.PurchasePrice, decimals) * count / input.Item.PackQty;
                PurchaseTot += System.Math.Round(input.Item.PriceCollection.PurchasePrice, decimals) * count / input.Item.PackQty;
                RetailTot += System.Math.Round(input.Item.PriceCollection.RetailPrice, decimals) * count / input.Item.PackQty;
            }
            this.neuSpread1_Sheet2.Cells[al.Count, 3].Text = "合  计：";
            this.neuSpread1_Sheet2.Cells[al.Count, 7].Text = PurchaseTot.ToString("F2");
            this.neuSpread1_Sheet2.Cells[al.Count, 9].Text = RetailTot.ToString("F2");
        }

        /// <summary>
        /// 入库单汇总设置fp
        /// </summary>
        private void SetFpForInputTot()
        {
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            if (this.neuSpread1_Sheet1.RowCount <= 0) return;
            if (this.neuSpread1_Sheet1.ColumnCount > 5)
            {
                this.neuSpread1_Sheet1.Columns.Get(0).Width = 120F;
                this.neuSpread1_Sheet1.Columns.Get((int)ColIndex.PrintIndex).Visible = false;
                this.neuSpread1_Sheet1.Columns.Get((int)ColIndex.PrintIndex).CellType = checkBoxCellType1;
                this.neuSpread1_Sheet1.Columns.Get((int)ColIndex.PrintIndex).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Columns.Get(1).Width = 42F;
                this.neuSpread1_Sheet1.Columns.Get(2).Width = 109F;
                this.neuSpread1_Sheet1.Columns.Get(3).Width = 211F;
                this.neuSpread1_Sheet1.Columns.Get(4).Width = 97F;
                this.neuSpread1_Sheet1.Columns.Get(5).Width = 112F;
            }
        }

        /// <summary>
        /// 按入库单初始化fp
        /// </summary>
        public void SetFpForInput()
        {
            #region 汇总信息
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.ColumnCount = 7;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Classic2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Text = "单据号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Text = "选中";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Text = "发票号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Text = "供货公司";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Text = "状态";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Text = "方式";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "单据号";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 120F;
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).CellType = checkBoxCellType1;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "选中";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 42F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "发票号";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 109F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "供货公司";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 211F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "状态";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 97F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "方式";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 112F;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "查询号";
            this.neuSpread1_Sheet1.Columns.Get(6).Visible = false;
            this.neuSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.neuSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SheetName = "入库单汇总";
            this.neuSpread1_Sheet1.Columns.Get(0).MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;                

            #endregion

            #region 详细信息
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType5 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuSpread1_Sheet2.Reset();
            this.neuSpread1_Sheet2.ColumnCount = 11;
            this.neuSpread1_Sheet2.RowCount = 0;
            this.neuSpread1_Sheet2.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Classic2;
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 0).Text = "发票号";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 1).Text = "药品来源";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 2).Text = "批号";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 3).Text = "药品名称";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 4).Text = "规格";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 5).Text = "数量";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 6).Text = "购入价";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 7).Text = "购入金额";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 8).Text = "零售价";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 9).Text = "零售金额";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 10).Text = "差价";
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.Columns.Get(0).Label = "发票号";
            this.neuSpread1_Sheet2.Columns.Get(0).CellType = textCellType1;
            this.neuSpread1_Sheet2.Columns.Get(0).Width = 71F;
            this.neuSpread1_Sheet2.Columns.Get(1).Label = "药品来源";
            this.neuSpread1_Sheet2.Columns.Get(1).Width = 105F;
            this.neuSpread1_Sheet2.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet2.Columns.Get(3).Label = "药品名称";
            this.neuSpread1_Sheet2.Columns.Get(3).Width = 86F;
            this.neuSpread1_Sheet2.Columns.Get(4).Label = "规格";
            this.neuSpread1_Sheet2.Columns.Get(4).Width = 87F;
            this.neuSpread1_Sheet2.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            numberCellType1.DecimalPlaces = 4;
            this.neuSpread1_Sheet2.Columns.Get(6).CellType = numberCellType1;
            this.neuSpread1_Sheet2.Columns.Get(6).Label = "购入价";
            this.neuSpread1_Sheet2.Columns.Get(6).Width = 70F;
            numberCellType2.DecimalPlaces = 2;
            this.neuSpread1_Sheet2.Columns.Get(7).CellType = numberCellType2;
            this.neuSpread1_Sheet2.Columns.Get(7).Label = "购入金额";
            this.neuSpread1_Sheet2.Columns.Get(7).Width = 80F;
            numberCellType3.DecimalPlaces = 4;
            this.neuSpread1_Sheet2.Columns.Get(8).CellType = numberCellType3;
            this.neuSpread1_Sheet2.Columns.Get(8).Label = "零售价";
            this.neuSpread1_Sheet2.Columns.Get(8).Width = 70F;
            numberCellType4.DecimalPlaces = 2;
            this.neuSpread1_Sheet2.Columns.Get(9).CellType = numberCellType4;
            this.neuSpread1_Sheet2.Columns.Get(9).Label = "零售金额";
            this.neuSpread1_Sheet2.Columns.Get(9).Width = 80F;
            numberCellType5.DecimalPlaces = 2;
            this.neuSpread1_Sheet2.Columns.Get(10).CellType = numberCellType5;
            this.neuSpread1_Sheet2.Columns.Get(10).Label = "差价";
            this.neuSpread1_Sheet2.Columns.Get(10).Width = 80F;
            this.neuSpread1_Sheet2.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet2.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet2.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet2.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.SheetName = "入库详细";
            #endregion
        }

        #endregion

        #region 出库单

        /// <summary>
        /// 获取出库单列表
        /// </summary>
        public void QueryOutputData()
        {
            DataSet ds = new DataSet();
            if (this.inOutMgr.ExecQuery("Pharmacy.Report.GetOutputList", ref ds, this.privDept.ID, this.BillState, this.dtBegin.Value.ToString(), this.dtEnd.Value.ToString()) == -1)
            {
                MessageBox.Show("获取出库单列表出错");
                return;
            }

            if (ds.Tables == null || ds.Tables.Count == 0)
            {
                MessageBox.Show(Language.Msg("无出库单数据"));
                return;
            }

            this.neuSpread1_Sheet1.DataSource = ds.Tables[0].DefaultView;

            this.dvOutput = ds.Tables[0].DefaultView;

            this.SetFpForOutputTot();
        }

        /// <summary>
        /// 出库单汇总设置fp
        /// </summary>
        private void SetFpForOutputTot()
        {
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 120F;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 68F;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 65F;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 211F;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 97F;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 112F;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 114F;
        }

        /// <summary>
        /// 获取出库单明细
        /// </summary>
        /// <param name="billno">单据号</param>
        /// <returns>output实体数组</returns>
        private ArrayList QueryOutputDetail(string billNO)
        {
            if (billNO == null || billNO == "")
            {
                return null;
            }

            //ArrayList al = this.itemManager.QueryOutputInfo(this.privDept.ID, billNO, this.BillState);
            ArrayList al = this.inOutMgr.QueryOutputInfo(this.privDept.ID, billNO, this.BillState);
            if (al.Count == 0)
            {
                return null;
            }
            try
            {

                //Sort noSort = new Sort();
                //al.Sort(noSort);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("出库数据排序错误>>" + ex.Message));
            }
            return al;
        }

        /// <summary>
        /// 打印、显示出库明细
        /// </summary>
        /// <param name="activerow">活动行索引</param>
        private void PrintOutputBill(int activerow)
        {
            //获得单据号
            string billNO = this.neuSpread1_Sheet1.Cells[activerow, (int)ColIndex.BillNOIndex].Text.Trim();
            if (billNO == null || billNO == "")
            {
                return;
            }
            ArrayList al = new ArrayList();
            al = this.QueryOutputDetail(billNO);
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show(Language.Msg("没有获得出库详细数据"));
                return;
            }
            this.AddOutputDataToFp(al);

            //打印
            if (this.IsPrint)
            {
                //接口打印处理
                Function.PrintBill("0320", ((FS.HISFC.Models.Pharmacy.Output)al[0]).PrivType, al);
                this.IsPrint = false;
            }
        }

        /// <summary>
        /// 按出库单初始化fp
        /// </summary>
        private void SetFpForOutput()
        {
            #region 汇总
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.ColumnCount = 7;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Classic2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Text = "出库单号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Text = "方式";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Text = "领用单位";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Text = "领用单位";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Text = "领用人";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Text = "状态";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Text = "入库单号";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "出库单号";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 120F;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "方式";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 68F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "领用单位";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 65F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "领用单位";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 211F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "领用人";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 97F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "状态";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 112F;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "入库单号";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 114F;
            this.neuSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.neuSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SheetName = "出库汇总";
            #endregion

            #region 详细
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.neuSpread1_Sheet2.Reset();
            this.neuSpread1_Sheet2.ColumnCount = 10;
            this.neuSpread1_Sheet2.RowCount = 0;
            this.neuSpread1_Sheet2.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Classic2;
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 0).Text = "批号";
            this.neuSpread1_Sheet2.Columns.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;            
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 1).Text = "药名";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 2).Text = "规格";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 3).Text = "单位";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 4).Text = "出库数量";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 5).Text = "当前库存";            
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 6).Text = "购入价";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 7).Text = "购入金额";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 8).Text = "零售价";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 9).Text = "零售金额";
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.Columns.Get(0).Label = "批号";
            this.neuSpread1_Sheet2.Columns.Get(0).Width = 71F;
            this.neuSpread1_Sheet2.Columns.Get(1).Label = "药名";
            this.neuSpread1_Sheet2.Columns.Get(1).Width = 115F;
            this.neuSpread1_Sheet2.Columns.Get(2).Label = "规格";
            this.neuSpread1_Sheet2.Columns.Get(2).Width = 138F;
            this.neuSpread1_Sheet2.Columns.Get(3).Label = "单位";
            this.neuSpread1_Sheet2.Columns.Get(3).Width = 45F;
            this.neuSpread1_Sheet2.Columns.Get(4).Label = "出库数量";            
            this.neuSpread1_Sheet2.Columns.Get(4).Width = 87F;
            this.neuSpread1_Sheet2.Columns.Get(5).Label = "当前库存";
            this.neuSpread1_Sheet2.Columns.Get(5).Width = 87F;
            this.neuSpread1_Sheet2.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            numberCellType1.DecimalPlaces = 4;
            this.neuSpread1_Sheet2.Columns.Get(6).CellType = numberCellType1;
            this.neuSpread1_Sheet2.Columns.Get(6).Label = "购入价";
            this.neuSpread1_Sheet2.Columns.Get(6).Width = 70F;
            //调拨单隐藏购入价
            if (this.BillType == "D")
            {
                this.neuSpread1_Sheet2.Columns.Get(6).Visible = false;
            }
            numberCellType2.DecimalPlaces = 2;
            this.neuSpread1_Sheet2.Columns.Get(7).CellType = numberCellType2;
            this.neuSpread1_Sheet2.Columns.Get(7).Label = "购入金额";
            this.neuSpread1_Sheet2.Columns.Get(7).Width = 80F;
            if (this.BillType == "D")
            {
                this.neuSpread1_Sheet2.Columns.Get(7).Visible = false;
            }
            numberCellType3.DecimalPlaces = 4;
            this.neuSpread1_Sheet2.Columns.Get(8).CellType = numberCellType3;
            this.neuSpread1_Sheet2.Columns.Get(8).Label = "零售价";
            this.neuSpread1_Sheet2.Columns.Get(8).Width = 70F;
            numberCellType4.DecimalPlaces = 2;
            this.neuSpread1_Sheet2.Columns.Get(9).CellType = numberCellType4;
            this.neuSpread1_Sheet2.Columns.Get(9).Label = "零售金额";
            this.neuSpread1_Sheet2.Columns.Get(9).Width = 80F;
            this.neuSpread1_Sheet2.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet2.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet2.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet2.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.SheetName = "出库详细";
            #endregion
        }

        /// <summary>
        /// fp出库详细数据赋值
        /// </summary>
        /// <param name="al"></param>
        private void AddOutputDataToFp(ArrayList al)
        {
            //显示单据号
            string tempbillno = ((FS.HISFC.Models.Pharmacy.Output)al[0]).OutListNO;
            //this.txtFilter.Text = "" + tempbillno;

            //清空fp
            if (this.neuSpread1_Sheet2.RowCount > 0)
            {
                this.neuSpread1_Sheet2.RowCount = 0;
            }

            //给fp添加行
            this.neuSpread1_Sheet2.Rows.Add(0, al.Count + 1);

            //fp分行赋值
            if (this.neuSpread1_Sheet2.ColumnCount < 10)
            {
                this.neuSpread1_Sheet2.ColumnCount = 10;
            }
            decimal PurchaseTot = 0;
            decimal RetailTot = 0;
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.Output output = al[i] as FS.HISFC.Models.Pharmacy.Output;
                this.neuSpread1_Sheet2.Cells[i, 0].Text = output.BatchNO;
                this.neuSpread1_Sheet2.Cells[i, 1].Text = output.Item.Name;
                this.neuSpread1_Sheet2.Cells[i, 2].Text = output.Item.Specs;
                if (output.ShowState == "0")
                {
                    this.neuSpread1_Sheet2.Cells[i, 3].Text = output.Item.MinUnit;
                }
                else
                {
                    this.neuSpread1_Sheet2.Cells[i, 3].Text = output.Item.PackUnit;
                }
                //包装数量作被除数
                if (output.Item.PackQty == 0)
                {
                    output.Item.PackQty = 1;
                }
                decimal count = 0;
                count = output.Quantity;

                
                decimal curStoreSum = 0m;
                int returnVal = storageMgr.GetStorageNum(output.StockDept.ID, output.Item.ID, out curStoreSum);


                if (output.ShowState == "0")
                {
                    this.neuSpread1_Sheet2.Cells[i, 4].Text = System.Convert.ToString(count);
                    this.neuSpread1_Sheet2.Cells[i, 5].Value = curStoreSum;
                }
                else
                {
                    this.neuSpread1_Sheet2.Cells[i, 4].Text = System.Convert.ToString(count / output.Item.PackQty);

                    this.neuSpread1_Sheet2.Cells[i, 5].Value = System.Convert.ToString(curStoreSum / output.Item.PackQty); 
                }
                this.neuSpread1_Sheet2.Cells[i, 6].Value = System.Math.Round(output.Item.PriceCollection.PurchasePrice, decimals);
                this.neuSpread1_Sheet2.Cells[i, 7].Value = System.Math.Round(output.Item.PriceCollection.PurchasePrice, decimals) * count / output.Item.PackQty;
                this.neuSpread1_Sheet2.Cells[i, 8].Value = System.Math.Round(output.Item.PriceCollection.RetailPrice, decimals);
                this.neuSpread1_Sheet2.Cells[i, 9].Value = System.Math.Round(output.Item.PriceCollection.RetailPrice, decimals) * count / output.Item.PackQty;
                PurchaseTot += System.Math.Round(output.Item.PriceCollection.PurchasePrice, decimals) * count / output.Item.PackQty;
                RetailTot += System.Math.Round(output.Item.PriceCollection.RetailPrice, decimals) * count / output.Item.PackQty;
            }
            this.neuSpread1_Sheet2.Cells[al.Count, 1].Text = "合  计：";
            this.neuSpread1_Sheet2.Cells[al.Count, 7].Text = PurchaseTot.ToString("F2");
            this.neuSpread1_Sheet2.Cells[al.Count, 9].Text = RetailTot.ToString("F2");
        }

        #endregion

        #region 清空选中

        /// <summary>
        /// 清选
        /// </summary>
        /// <param name="isSelect">是否选中</param>
        public void SelectAll(bool isSelect)
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                if (this.neuSpread1_Sheet1.Cells.Get(0, 1).Text == "操作说明")
                {
                    return;
                }

                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)ColIndex.PrintIndex].Value = isSelect;
                }
            }
        }

        #endregion

        #endregion

        #region 树操作

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            //父结点科室，子结点单据
            if (e.Tag != null && e.Parent != null && e.Parent.Tag != null)
            {
                this.BillType = (e.Tag as FS.FrameWork.Models.NeuObject).ID;
                this.privDept = e.Parent.Tag as FS.FrameWork.Models.NeuObject;
                if (this.neuSpread1.ActiveSheetIndex != 0)
                {
                    this.neuSpread1.ActiveSheetIndex = 0;
                }
            }
            return base.OnSetValue(neuObject, e);
        }        

        #endregion

        #region 事件

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();

                tvPrivTree tvPriv = this.tv as tvPrivTree;
                
            }
            base.OnLoad(e);
        }

        private void cmbStatus_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            if (this.cmbStatus.Text != null && this.cmbStatus.Text != "")
            {
                this.BillState = this.cmbStatus.SelectedValue.ToString();
            }

        }

        private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.neuSpread1_Sheet1.RowCount == 0 || this.neuSpread1.ActiveSheetIndex == 1 || this.BillType != "I")
            {
                return;
            }
            if (e.Column == (int)ColIndex.PrintIndex)
            {
                if (this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Text == "True")
                {
                    this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Text = "False";
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Text = "True";
                }
            }
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.neuSpread1_Sheet1.RowCount == 0 || this.neuSpread1.ActiveSheetIndex == 1)
            {
                return;
            }
            this.IsPrint = false;
            this.neuSpread1_Sheet2.RowCount = 0;
            this.neuSpread1_Sheet1.ActiveRowIndex = e.Row;
            //入库
            if (this.BillType == "I")
            {
                this.PrintInputBill(e.Row);
            }
            //出库
            if (this.BillType == "O")
            {
                this.PrintOutputBill(e.Row);
            }
            if (this.BillType != "NO")
            {
                this.neuSpread1.ActiveSheetIndex = 1;
            }
         
        }

        private void txtFilter_TextChanged(object sender, System.EventArgs e)
        {
            if (this.neuSpread1.ActiveSheetIndex == 1 && this.dvInput == null && this.dvOutput == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(this.txtFilter.Text.Trim())==false)
            {
                if (this.txtFilter.Text.Trim().Substring(0, 1) == "单")
                {
                    return;
                }
            }
            string tempfilter = this.txtFilter.Text.Trim();
            try
            {
                if (this.BillType=="I")
                {
                    if (this.dvInput != null)
                    {
                        this.dvInput.RowFilter = "(单据号 LIKE '%" + tempfilter + "%')"
                            + "OR (发票号 LIKE '%" + tempfilter + "%')"
                            + "OR (状态 LIKE '%" + tempfilter + "%')"
                            + "OR (方式 LIKE '%" + tempfilter + "%')"
                            + "OR (发票号 LIKE '%" + tempfilter + "%')"
                            + "OR (供货公司 LIKE '%" + tempfilter + "%')";

                        this.neuSpread1_Sheet1.DataSource = this.dvInput;

                        this.SetFpForInputTot();
                    }
                }
                else if (this.BillType == "O")
                {
                    if (this.dvOutput != null)
                    {
                        this.dvOutput.RowFilter = "(出库单号 LIKE '%" + tempfilter + "%')"
                            + "OR (领用单位 LIKE '%" + tempfilter + "%')"
                            + "OR (状态 LIKE '%" + tempfilter + "%')"
                            + "OR (方式 LIKE '%" + tempfilter + "%')"
                            + "OR (入库单号 LIKE '%" + tempfilter + "%')"
                            + "OR (领用人 LIKE '%" + tempfilter + "%')";

                        this.neuSpread1_Sheet1.DataSource = this.dvOutput;

                        this.SetFpForOutputTot();
                    }
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("获取过滤条件出错>>" + ex.Message));
            }
        }
       
        #endregion

        #region 列设置

        private enum ColIndex
        {
            /// <summary>
            /// 单据号列
            /// </summary>
            BillNOIndex = 0,
            /// <summary>
            /// 打印选中列
            /// </summary>
            PrintIndex = 1,
            /// <summary>
            /// 发票号列
            /// </summary>
            InvoiceIndex = 2,
            /// <summary>
            /// 入库单据号列
            /// </summary>
            ListNOIndex = 6
        }

        #endregion        
    }

    internal class Sort : System.Collections.IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            FS.HISFC.Models.Pharmacy.Output o1 = x as FS.HISFC.Models.Pharmacy.Output;

            FS.HISFC.Models.Pharmacy.Output o2 = y as FS.HISFC.Models.Pharmacy.Output;

            return NConvert.ToInt32(o1.ID) - NConvert.ToInt32(o2.ID);
        }

        #endregion
    }
    /// <summary>
    /// 盘点单按药品类别排序
    /// </summary>
    internal class SortCheck : System.Collections.IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            FS.HISFC.Models.Pharmacy.Check ch1 = x as FS.HISFC.Models.Pharmacy.Check;

            FS.HISFC.Models.Pharmacy.Check ch2 = y as FS.HISFC.Models.Pharmacy.Check;
            string drugType1 = GetDrugCode(ch1.Item.Type.ID) + ch1.PlaceNO;
            string drugType2 = GetDrugCode(ch2.Item.Type.ID) + ch2.PlaceNO;
            return string.Compare(drugType1, drugType2);
        }
        private string GetDrugCode(string drugtype)
        {
            string drugType = drugtype;
            if (drugtype == "Z" || drugtype == "P" || drugtype == "C")
            {
                return "L";
            }
            return drugType;
        }
        #endregion 
    }
}
