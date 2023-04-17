using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FSDataWindow.Controls
{
    public partial class ucQueryBaseForDataWindowExtend : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQueryBaseForDataWindowExtend()
        {
            InitializeComponent();
        }

        #region  //{2C89BBBC-10FB-4f7e-B080-712A6C228719}

        private string sqlid = string.Empty;

        [Category("控件设置"), Description("SQL语句ID维护")]
        public string SqlID
        {
            get
            {
                return sqlid;
            }
            set
            {
                sqlid = value;
            }
        }

        private string sql = string.Empty;


        #endregion

        #region 变量

        /// <summary>
        /// 左侧控件初始宽度
        /// </summary>
        protected const int LEFT_CONTROL_WIDTH = 200;

        /// <summary>
        /// 细节显示部分高度
        /// </summary>
        protected const int DETAIL_CONTROL_HEIGHT = 300;

        /// <summary>
        /// 左侧显示树控件是否可见
        /// </summary>
        protected bool isLeftVisible = true;

        /// <summary>
        /// 是否显示细节部分
        /// </summary>
        protected bool isShowDetail = false;

        /// <summary>
        /// 左侧控件,默认为其他控件
        /// </summary>
        protected QueryControls leftControl = QueryControls.Other;

        /// <summary>
        /// 左侧树控件
        /// </summary>
        protected TreeView tvLeft = null;

        /// <summary>
        /// 是否选择树节点后,调用Retrieve();
        /// </summary>
        protected bool isAfterSelectRetrieve = false;

        /// <summary>
        /// 主数据窗pbl路径
        /// </summary>
        protected string mainDWLabrary = string.Empty;

        /// <summary>
        /// 主数据窗DataObject
        /// </summary>
        protected string mainDWDataObject = string.Empty;

        /// <summary>
        /// 主查询默认控件
        /// </summary>
        protected QueryControls mainQueryControl = QueryControls.DataWindow;

        /// <summary>
        /// 是否查询条件只有开始时间,结束时间
        /// </summary>
        protected bool isRetrieveArgsOnlyTime = false;

        /// <summary>
        /// 开始时间
        /// </summary>
        protected DateTime beginTime = DateTime.MinValue;

        /// <summary>
        /// 结束时间
        /// </summary>
        protected DateTime endTime = DateTime.MinValue;

        /// <summary>
        /// 
        /// </summary>
        protected FS.FrameWork.Management.DataBaseManger dataBaseManager = new FS.FrameWork.Management.DataBaseManger();

        /// <summary>
        /// 登录人员信息
        /// </summary>
        protected FS.HISFC.Models.Base.Employee employee = null;

    
    



        #endregion

        #region 属性



        /// <summary>
        /// 数据窗报表Title
        /// </summary>
        protected string reportTitle = string.Empty;


        /// <summary>
        /// 数据窗报表Title
        /// </summary>
        [Category("控件设置"), Description("数据窗报表Title")]
        public string ReportTitle 
        {
            get 
            {
                return this.reportTitle;
            }
            set 
            {
                this.reportTitle = value;
            }
        }

        /// <summary>
        /// 主查询默认控件
        /// </summary>
        [Category("控件设置"), Description("主数据窗pbl路径")]
        public QueryControls MainQueryControl 
        {
            get 
            {
                return this.mainQueryControl;
            }
            set 
            {
                this.mainQueryControl = value;

                this.SetMainQueryControl();
            }
        }

        /// <summary>
        /// 主数据窗pbl路径
        /// </summary>
        [Category("控件设置"), Description("主数据窗pbl路径")]
        public string MainDWLabrary 
        {
            get 
            {
                return this.mainDWLabrary;
            }
            set 
            {
                this.mainDWLabrary = value;
            }
        }

        /// <summary>
        /// 主数据窗DataObject
        /// </summary>
        [Category("控件设置"), Description("主数据窗DataObject")]
        public string MainDWDataObject 
        {
            get 
            {
                return this.mainDWDataObject;
            }
            set 
            {
                this.mainDWDataObject = value;
            }
        }

        /// <summary>
        /// 左侧显示树控件是否可见
        /// </summary>
        [Category("控件设置"), Description("左侧容器是否可见(树)")]
        public bool IsLeftVisible 
        {
            get 
            {
                return this.isLeftVisible;
            }
            set 
            {
                this.isLeftVisible = value;

                //设置左侧控件是否可见
                this.SetLeftControlVisible();
            }
        }

        /// <summary>
        /// 是否选择树节点后,调用Retrieve();
        /// </summary>
        [Category("控件设置"), Description("是否选择树节点后,调用Retrieve")]
        public bool IsAfterSelectRetrieve 
        {
            get 
            {
                return this.isAfterSelectRetrieve;
            }
            set 
            {
                this.isAfterSelectRetrieve = value;
            }
        }

        /// <summary>
        /// 左侧控件,默认为其他控件
        /// </summary>
        [Category("控件设置"), Description("左侧控件,默认为其他控件")]
        public QueryControls LeftControl 
        {
            get 
            {
                return this.leftControl;
            }
            set 
            {
                this.leftControl = value;

                this.SetLeftControl();
            }
        }

        /// <summary>
        /// 是否显示细节部分
        /// </summary>
        [Category("控件设置"), Description("是否显示细节部分")]
        public bool IsShowDetail 
        {
            get 
            {
                return this.isShowDetail;
            }
            set 
            {
                this.isShowDetail = value;

                this.SetDetailVisible();
            }
        }

        /// <summary>
        /// 是否查询条件只有开始时间,结束时间
        /// </summary>
        [Category("控件设置"), Description("是否查询条件只有开始时间,结束时间")]
        public bool IsRetrieveArgsOnlyTime
        {
            get
            {
                return this.isRetrieveArgsOnlyTime;
            }
            set
            {
                this.isRetrieveArgsOnlyTime = value;
            }
        }

        #region 东莞加的

        /// <summary>
        /// 数据窗报表Title的Text控件名称
        /// </summary>
        protected string reportTitleObjectName = "t_title";

        /// <summary>
        /// 数据窗报表Title的Text控件名称
        /// </summary>
        [Category("报表格式设置"), Description("数据窗报表Title的Text控件名称")]
        public string ReportTitleObjectName
        {
            get
            {
                return this.reportTitleObjectName;
            }
            set
            {
                this.reportTitleObjectName = value;
            }
        }





        /// <summary>
        /// 类别
        /// </summary>
        string reportType = "0";

        [Category("报表格式设置"), Description("报表类别，０门诊１住院　不写枚举了")]
        public string ReportType
        {
            get
            {
                return reportType;
            }
            set
            {
                reportType = value;
            }
        }

      


        /// <summary>
        /// 显示操作员的控件名称
        /// </summary>
        string operObjectName = "t_oper";

        [Category("报表格式设置"), Description("显示操作员的控件名称")]
        public string OperObjectName
        {
            get
            {
                return operObjectName;
            }
            set
            {
                operObjectName = value;
            }
        }

        /// <summary>
        ///  显示操作员
        /// </summary>
        protected virtual void SetOper()
        {
            if (this.dwMain != null)
            {
                if (this.operObjectName.Trim() != string.Empty)
                {
                    try
                    {
                        this.dwMain.Modify(this.operObjectName + ".Text = " + "'制单人：" + FS.FrameWork.Management.Connection.Operator.ID + "(" + FS.FrameWork.Management.Connection.Operator.Name + ")'");
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// 显示操作时间的控件名称
        /// </summary>
        string operDateObjectName = "t_date";

        [Category("报表格式设置"), Description("显示操作时间的控件名称")]
        public string OperDateObjectName
        {
            get
            {
                return operDateObjectName;
            }
            set
            {
                operDateObjectName = value;
            }
        }

        /// <summary>
        ///  显示操作员
        /// </summary>
        protected virtual void SetOperDate()
        {
            if (this.dwMain != null)
            {
                if (this.operDateObjectName.Trim() != string.Empty)
                {
                    try
                    {
                        this.dwMain.Modify(this.operDateObjectName + ".Text = " + "'打印时间：" +　this.dataBaseManager.GetDateTimeFromSysDateTime().ToShortDateString() + "'");
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// 显示查询时间段的控件名称
        /// </summary>
        string queryDateObjectName = "t_query";

        [Category("报表格式设置"), Description("显示查询时间段的控件名称")]
        public string QueryDateObjectName
        {
            get
            {
                return queryDateObjectName;
            }
            set
            {
                queryDateObjectName = value;
            }
        }

        /// <summary>
        ///  显示统计时间段
        /// </summary>
        protected virtual void SetOperQueryDate(DateTime beginTime, DateTime endTime)
        {
            if (this.dwMain != null)
            {
                if (this.queryDateObjectName.Trim() != string.Empty)
                {
                    try
                    {
                        this.dwMain.Modify(this.queryDateObjectName + ".Text = " + "'时间：" + beginTime.ToShortDateString() + " 至 " + endTime.ToShortDateString() + "'");
                    }
                    catch
                    {
                    }
                }
            }
        }
        /// <summary>
        /// 交叉报表最后显示一行的控件
        /// </summary>
        string queryFooterName = "t_footer";

        [Category("报表格式设置"), Description("交叉报表最后显示一行的控件名称")]
        public string QueryFooterName
        {
            get
            {
                return queryFooterName;
            }
            set
            {
                queryFooterName = value;
            }
        }

        /// <summary>
        ///  显示统计时间段
        /// </summary>
        protected virtual void SetQueryFooter()
        {
            if (this.dwMain != null)
            {
                if (this.queryFooterName.Trim() != string.Empty)
                {
                    try
                    {
                        this.dwMain.Modify(this.QueryFooterName + ".Text = " + "    '操作员：" + FS.FrameWork.Management.Connection.Operator.ID + "(" + FS.FrameWork.Management.Connection.Operator.Name + ")                          操作科室："  +              (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Dept.Name+"'");   // +                   "                           打印时间：" + this.dataBaseManager.GetDateTimeFromSysDateTime().ToShortDateString() + "'");
                    }
                    catch
                    {
                    }
                }
            }
        }
        #endregion

        #endregion

        #region 方法

        /// <summary>
        /// 设置数据窗口Title名称
        /// </summary>
        protected virtual void SetTitle() 
        {
            if (this.dwMain != null) 
            {
                if (this.reportTitleObjectName != string.Empty)
                {
                    try
                    {
                        this.dwMain.Modify(this.reportTitleObjectName + ".Text = " + "'" + this.reportTitle + "'");
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// TreeView节点查询
        /// </summary>
        /// <returns></returns>
        protected virtual int OnQueryTree() 
        {
            if (this.tvLeft == null) 
            {
                return -1;
            }

            string queryText = this.cmbQuery.Text;

            if (queryText == string.Empty) 
            {
                return -1;
            }

            if (this.tvLeft.Nodes.Count <= 0) 
            {
                return -1;
            }

            TreeNode queryNode = this.tvLeft.Nodes[0];

            this.QueryTree(queryNode, queryText);

            return 1;
        }

        private void QueryTree(TreeNode nowNode, string queryText) 
        {
            if (nowNode == null) 
            {
                return;
            }
       
            if (nowNode.Tag != null && nowNode.Tag.ToString() == queryText)
            {
                this.tvLeft.Select();
                this.tvLeft.SelectedNode = nowNode;

                if (this.cmbQuery.Items.IndexOf(queryText) < 0)
                {
                    this.cmbQuery.Items.Add(queryText);
                }
                
                return;
            }
            if (nowNode.Text == queryText) 
            {
                this.tvLeft.Select();
                this.tvLeft.SelectedNode = nowNode;

                if (this.cmbQuery.Items.IndexOf(queryText) < 0)
                {
                    this.cmbQuery.Items.Add(queryText);
                }

                return;
            }
     
            foreach (TreeNode node in nowNode.Nodes) 
            {
                QueryTree(node, queryText);
            }

            return;
        }


        /// <summary>
        /// 画树方法.
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int OnDrawTree() 
        {
            if (this.tvLeft == null) 
            {
                return -1;
            }

            this.tvLeft.ImageList = new ImageList();
            this.tvLeft.ImageList.Images.Add(FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.A安排));
            this.tvLeft.ImageList.Images.Add(FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.L浏览));

            return 1;
        }

        /// <summary>
        /// 获得查询时间
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int GetQueryTime() 
        {
            /*
            if (this.dtpEndTime.Value < this.dtpBeginTime.Value) 
            {
                MessageBox.Show("结束时间不能小于开始时间");

                return -1;
            }
            */ 

            this.beginTime = this.dtpBeginTime.Value;
            this.endTime = this.dtpEndTime.Value;

            return 1;
        }

        protected void ClearSql()
        {
            this.sql = string.Empty;
        }

        /// <summary>
        /// 设置主查询控件类型
        /// </summary>
        protected virtual void SetMainQueryControl() 
        {
            this.plRightTop.Controls.Clear();

            switch (this.mainQueryControl) 
            {
                case QueryControls.DataWindow:

                    this.dwMain = new FSDataWindow.Controls.FSDataWindowExtend();

                    this.plRightTop.Controls.Add(this.dwMain);

                    this.dwMain.LiveScroll = true;
                    this.dwMain.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
                    this.dwMain.Dock = DockStyle.Fill;
                    this.dwMain.BringToFront();

                    break;
            }
        }

        /// <summary>
        /// 设置细节部分是否可见
        /// </summary>
        protected virtual void SetDetailVisible() 
        {
            this.plRightBottom.Visible = this.isShowDetail;

            this.plRightBottom.Height = this.isShowDetail ? DETAIL_CONTROL_HEIGHT : 0;

            this.slTop.Enabled = this.isShowDetail;

            this.slTop.Visible = this.isShowDetail;


        }

        /// <summary>
        /// 设置左侧控件
        /// </summary>
        protected virtual void SetLeftControl() 
        {
            //如果左侧控件已经不可见,以下代码不发生作用.
            if (!this.isLeftVisible) 
            {
                return;
            }

            //清除左侧控件容器已经加载的控件
            this.plLeftControl.Controls.Clear();

            switch (this.leftControl) 
            {
                case QueryControls.Tree:

                    this.tvLeft = new TreeView();
                    
                    this.plLeftControl.Controls.Add(tvLeft);

                    this.tvLeft.Dock = DockStyle.Fill;

                    this.tvLeft.AfterSelect += new TreeViewEventHandler(tvLeft_AfterSelect);

                    break;
            }
        }

        /// <summary>
        /// 如果有树的话,树的AfterSelect事件触发后,执行方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnTreeViewAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!this.isAfterSelectRetrieve) 
            {
                return;
            }
        }

        void tvLeft_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.tv == null) 
            {
                return;
            }

            this.OnTreeViewAfterSelect(sender, e);
        }

        /// <summary>
        /// 设置左侧控件是否可见
        /// </summary>
        protected virtual void SetLeftControlVisible()
        {
            this.plLeft.Visible = this.isLeftVisible;

            this.plLeft.Width = this.isLeftVisible ? LEFT_CONTROL_WIDTH : 0;

            this.slLeft.Enabled = this.isLeftVisible;

            this.slLeft.Visible = this.isLeftVisible;
        }

        /// <summary>
        /// 按照开始时间和结束时间查询dw
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int RetrieveMainOnlyByTime() 
        {
            if (this.dwMain == null) 
            {
                return -1;
            }

            if (this.GetQueryTime() == -1) 
            {
                return -1;
            }

            //{2C89BBBC-10FB-4f7e-B080-712A6C228719}
            //if (FS.FrameWork.Management.Connection.Instance.GetType().ToString().IndexOf("IBM") >= 0)
            if (this.sqlid.Trim() != string.Empty)
            {
                //if (this.sqlid == string.Empty)
                //{
                //    MessageBox.Show("没有维护SqlID！");
                //    return -1;
                //}
                if (this.sql == string.Empty)
                {
                    if (this.dataBaseManager.Sql.GetSql(this.sqlid, ref this.sql) < 0)
                    {
                        MessageBox.Show("找不到SQL语句！");
                        return -1;
                    }
                }

                try
                {
                    string exeSql = string.Format(this.sql, beginTime.ToString(), endTime.ToString());

                    DataSet ds = new DataSet();

                    //设置超时时间(默认的超时时间为30S)

                    if (this.dataBaseManager.ExecQuery(exeSql, ref ds) < 0)
                    {
                        MessageBox.Show("执行SQL语句错误！");
                        return -1;
                    }

                    if (ds == null)
                    {
                        MessageBox.Show("执行SQL语句错误！");
                        return -1;
                    }

                    
                    if (dwMain.RetrieveDataTable( ds.Tables[0]) < 0)
                    {
                        MessageBox.Show(dwMain.Error);

                        return -1;
                    }

                    return 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return -1;
                }
            }

            if (FS.FrameWork.Management.Connection.Instance.GetType().ToString().IndexOf("IBM") >= 0)
            {
                if (this.dwMain.Retrieve(beginTime.ToString(), endTime.ToString()) < 0)
                {
                    MessageBox.Show(dwMain.Error);

                    return -1;
                }
            }
            else
            {
                if (this.dwMain.Retrieve(beginTime, endTime) < 0)
                {
                    MessageBox.Show(dwMain.Error);

                    return -1;
                }
            }

            return 1;
        }
        
        /// <summary>
        /// 主数据窗Retrieve方法
        /// </summary>
        /// <param name="args">Retrieve参数列表</param>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int RetrieveMain(params object[] args) 
        {
            if (this.dwMain != null) 
            {
                try
                {
                    return this.dwMain.Retrieve(args);
                }
                catch { }
            }
            
            return 1;
        }
        /// <summary>
        /// 是否支持排序
        /// </summary>
        protected bool isSort = true;

        protected string sortColumn = string.Empty;

        /// <summary>
        /// 升序排序
        /// </summary>
        protected string sortType = "A";

        /// <summary>
        /// 排序 成功 1 失败 -1
        /// </summary>
        /// <returns></returns>
        protected int OnSort() 
        {
            try
            {
                if (this.isSort)
                {
                    string ls_CurObj = "";

                    int ll_CurRowNumber = 0;
                    ls_CurObj = this.dwMain.ObjectUnderMouse.Gob.Name; //得出objectName
                    ll_CurRowNumber = this.dwMain.ObjectUnderMouse.RowNumber; //得出当前Row

                    if (this.dwMain.Describe(ls_CurObj + ".Band") == "header")
                    {
                        if (ll_CurRowNumber == 0 & this.dwMain.Describe(ls_CurObj + ".Text") != "!")
                        {
                            sortColumn = ls_CurObj.Substring(0, ls_CurObj.Length - 2);

                            if (sortType == "A")
                            {
                                DataWindowSort(this.dwMain, sortColumn, sortType);
                                sortType = "D";
                            }
                            else
                            {
                                DataWindowSort(this.dwMain, sortColumn, sortType);
                                sortType = "A";
                            }
                        }
                    }
                }
            }
            catch
            {
                return -1;
            }

            finally
            {

            }

            return 1;
        }

        /// <summary>
        /// 取消掉其他列的排序符号
        /// </summary>
        /// <param name="dwControl">当前数据窗口</param>
        protected void DeleleSortFlag(Sybase.DataWindow.DataWindowControl dwControl) 
        {
            string columnName = string.Empty;

            try
            {
                for (int i = 1; i < dwControl.ColumnCount + 1; i++)
                {
                    columnName = dwControl.Describe('#' + i.ToString() + ".name") + "_t";

                    dwControl.Modify(columnName + ".text = '" + this.dwMain.Describe(columnName + ".text").Replace("↑", string.Empty) + "'");
                    dwControl.Modify(columnName + ".text = '" + this.dwMain.Describe(columnName + ".text").Replace("↓", string.Empty) + "'");
                }
            }
            catch { }
        }

        /// <summary>
        /// 排序的方法
        /// </summary>
        /// <param name="dwControl">当前数据窗</param>
        /// <param name="currColumn">当前列</param>
        /// <param name="sortType">排序类型</param>
        /// <returns>成功 true 失败 false</returns>
        private bool DataWindowSort(Sybase.DataWindow.DataWindowControl dwControl, string currColumn, string sortType) 
        {
            try
            {
                //排序  
                dwControl.SetSort(currColumn + " " + sortType);
                dwControl.Sort();

                //创建升序的箭头图形

                DeleleSortFlag(dwControl);

                switch (sortType)
                {
                    case "A":

                        dwControl.Modify(currColumn + "_t" + ".text = '" + this.dwMain.Describe(currColumn + "_t" + ".text") + "↑'");

                        break;
                    case "D":
                        dwControl.Modify(currColumn + "_t" + ".text = '" + this.dwMain.Describe(currColumn + "_t" + ".text") + "↓'");

                        break;
                }

                return true;
            }
            catch
            {
                return false;
            }

            finally
            {

            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected int Init() 
        {
            if (this.dwMain != null)
            {
                this.dwMain.LibraryList = FS.FrameWork.WinForms.Classes.Function.CurrentPath + this.mainDWLabrary;

                this.dwMain.DataWindowObject = this.mainDWDataObject;
            }
            DateTime dtSysdate = new DateTime();
            dtSysdate = this.dataBaseManager.GetDateTimeFromSysDateTime();

            this.dtpBeginTime.Value = new DateTime(dtSysdate.Year,dtSysdate.Month,dtSysdate.Day,0,0,0);
            this.dtpEndTime.Value = new DateTime(this.dataBaseManager.GetDateTimeFromSysDateTime().Year,this.dataBaseManager.GetDateTimeFromSysDateTime().Month,this.dataBaseManager.GetDateTimeFromSysDateTime().Day,23,59,59);

            this.OnDrawTree();

            if (this.tvLeft != null) 
            {
                if (this.tvLeft.Nodes.Count > 0) 
                {
                    this.tvLeft.Select();
                    this.tvLeft.SelectedNode = this.tvLeft.Nodes[0];
                }
            }
            //by zengft
            //this.dwMain.PrintProperties.Preview = true;
            try
            {
                this.dwMain.PrintProperties.PrinterName = this.PrinterName;
                this.dwMain.PrintProperties.PaperSize = 1;
            }
            catch
            {
            }

            this.SetTitle();

            this.SetOper();
            this.SetOperDate();

            this.SetQueryFooter();

            return 1;
        }

        /// <summary>
        /// Load事件
        /// </summary>
        protected virtual void OnLoad() 
        {
            this.employee = (FS.HISFC.Models.Base.Employee)this.dataBaseManager.Operator;
        }

        /// <summary>
        /// 自行设计查询条件的查询,继承用
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int OnRetrieve(params object[] objects) 
        {
            //{2C89BBBC-10FB-4f7e-B080-712A6C228719}
            //if (FS.FrameWork.Management.Connection.Instance.GetType().ToString().IndexOf("IBM") >= 0)
            if (this.sqlid.Trim() != string.Empty)
            {
                //if (this.sqlid == string.Empty)
                //{
                //    MessageBox.Show("没有维护SqlID！");
                //    return -1;
                //}
                if (this.sql == string.Empty)
                {
                    if (this.dataBaseManager.Sql.GetSql(this.sqlid, ref this.sql) < 0)
                    {
                        MessageBox.Show("找不到SQL语句！");
                        return -1;
                    }
                }

                try
                {
                    string exeSql = string.Format(this.sql, objects);

                    DataSet ds = new DataSet();

                    //设置超时时间(默认的超时时间为30S)

                    if (this.dataBaseManager.ExecQuery(exeSql, ref ds) < 0)
                    {
                        MessageBox.Show("执行SQL语句错误！");
                        return -1;
                    }


                    if (ds == null)
                    {
                        MessageBox.Show("执行SQL语句错误！");
                        return -1;
                    }
                    //by zengft
                    //this.dwMain.PrintProperties.Preview = true;
                    this.dwMain.PrintProperties.PrinterName = this.PrinterName;
                    this.dwMain.PrintProperties.PaperSize = 1;
                    if (dwMain.RetrieveDataTable(ds.Tables[0]) < 0)
                    {
                        MessageBox.Show(dwMain.Error);

                        return -1;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return -1;
                }

                return 1;
            }

            if (dwMain != null)
            {
                if (dwMain.Retrieve(objects) < 0)
                {
                    MessageBox.Show(dwMain.Error);

                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            OnExport();
            return base.Export(sender, neuObject);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int OnExport()
        {
            if (dwMain == null)
            {
                return -1;
            }    

            this.DeleleSortFlag(dwMain);

            System.Windows.Forms.SaveFileDialog dd = new SaveFileDialog();
            dd.Filter = "txt files (*.xls)|*.xls";
            if (dd.ShowDialog() == DialogResult.Cancel)
            {
                return 1;
            }
            dwMain.SaveAs(dd.FileName, Sybase.DataWindow.FileSaveAsType.Excel, true);

            return 1;
        }

    

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            if (this.dwMain != null) 
            {
                try
                {
                    this.DeleleSortFlag(dwMain);
                    this.dwMain.PrintProperties.Preview = false;
                    //this.dwMain.Print(true, true);
                    //if (this.printerName.Trim() != "")
                    //{
                    //    dwMain.SetProperty("DataWindow.Printer", printerName);
                    //}
                    //by zengft
                    //this.dwMain.PrintProperties.Preview = true;
                    this.dwMain.PrintProperties.PrinterName = this.PrinterName;
                    this.dwMain.PrintProperties.PaperSize = this.PrinterPaper;

                    if (this.PageHeight != 0 && this.PageWidth != 0)
                    {
                        dwMain.Modify("DataWindow.Print.Paper.Size=256");
                        dwMain.Modify("DataWindow.Print.CustomPage.Length=" + PageHeight.ToString());
                        dwMain.Modify("DataWindow.Print.CustomPage.Width=" + PageWidth.ToString());
                    }

                    this.dwMain.Print(ShowCancelDiag, ShowPrinterDiag);
                }
                catch { }
            }
            
            return base.OnPrint(sender, neuObject);
        }


        /// <summary>
        /// 有些控件没有实现需要屏蔽
        /// </summary>
        /// <param name="isVisible"></param>
        public void SetControlVisible(bool isVisible)
        {
           
        }
             

        #endregion

        #region 枚举

        /// <summary>
        /// 左侧容器装载控件
        /// </summary>
        public enum QueryControls 
        {
            /// <summary>
            /// TreeView控件
            /// </summary>
            Tree = 0,

            /// <summary>
            /// DataWindow控件
            /// </summary>
            DataWindow,

            /// <summary>
            /// 文本控件
            /// </summary>
            Text,

            /// <summary>
            /// 其他控件
            /// </summary>
            Other
        }

        #endregion

        #region 事件

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            Cursor = Cursors.WaitCursor;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询数据,请等待....");

            Application.DoEvents();

            this.GetQueryTime();
            if (this.isRetrieveArgsOnlyTime)
            {
                this.RetrieveMainOnlyByTime();
            }
            else 
            {
                this.OnRetrieve();
            }

            //this.SetOperQueryDate(this.beginTime, this.endTime);

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            Cursor = Cursors.Arrow;

            return 1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.IsShowDetail = false;
        }

        private void ucQueryBaseForDataWindow_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            this.OnLoad();

            this.Init();
        }

        private void cmbQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.OnQueryTree();
            }
        }

        private void btnQueryTree_Click(object sender, EventArgs e)
        {
            this.OnQueryTree();
        }

        private void dwMain_Click(object sender, EventArgs e)
        {
            if (this.dwMain != null)
            {
                this.OnSort();
            }
        }

        #endregion

        #region 打印设置

        int printerPaper =1;


        [Category("打印设置") ,Description("打印纸张类型设置")]
        public int PrinterPaper 
        {
            get { return this.printerPaper; }
            set { this.printerPaper = value; }
        }



        /// <summary>
        /// 打印机名
        /// </summary>
        string printerName = "";

        /// <summary>
        /// 打印机名
        /// </summary>
        [Category("打印设置"), Description("打印机名")]
        public string PrinterName
        {
            get
            {
                return printerName;
            }
            set
            {
                printerName = value;
            }
        }

        /// <summary>
        /// 纸张宽度
        /// </summary>
        int pageWidth = 0;

        /// <summary>
        /// 纸张宽度
        /// </summary>
        [Category("打印设置"), Description("纸张宽度")]
        public int PageWidth
        {
            get
            {
                return pageWidth;
            }
            set
            {
                pageWidth = value;
            }
        }

        /// <summary>
        /// 纸张高度
        /// </summary>
        int pageHeight = 0;

        /// <summary>
        /// 纸张高度
        /// </summary>
        [Category("打印设置"), Description("纸张高度")]
        public int PageHeight
        {
            get
            {
                return pageHeight;
            }
            set
            {
                pageHeight = value;
            }
        }

        /// <summary>
        /// 打印时是否显示打印设置对话框
        /// </summary>
        bool showPrinterDiag = false;

        /// <summary>
        /// 打印时是否显示打印设置对话框
        /// </summary>
        [Category("打印设置"), Description("打印时是否显示打印设置对话框")]
        public bool ShowPrinterDiag
        {
            get
            {
                return showPrinterDiag;
            }
            set
            {
                showPrinterDiag = value;
            }
        }

        /// <summary>
        /// 打印时是否显示取消打印对话框
        /// </summary>
        bool showCancelDiag = false;

        /// <summary>
        /// 打印时是否显示取消打印对话框
        /// </summary>
        [Category("打印设置"), Description("打印时是否显示取消打印对话框")]
        public bool ShowCancelDiag
        {
            get
            {
                return showCancelDiag;
            }
            set
            {
                showCancelDiag = value;
            }
        }

        #endregion

    }
}
