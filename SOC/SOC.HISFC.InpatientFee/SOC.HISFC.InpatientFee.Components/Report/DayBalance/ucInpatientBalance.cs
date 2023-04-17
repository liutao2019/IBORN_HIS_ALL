using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.InpatientFee.Components.Report.DayBalance
{
    /// <summary>
    /// 门诊收费日结
    /// </summary>
    public partial class ucInpatientBalance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucInpatientBalance()
        {
            InitializeComponent();
        }
        private Dictionary<string, Object> allMap = null;
        private bool isOperation = false;

        #region 枚举

        /// <summary>
        /// 显示数据的类型
        /// </summary>
        public enum EnumShowType
        {
            FarPoint,
            Datawindow
        }

        public enum EnumDetailCondition
        {
            UseMainCondition,
            UseMainData,
            Custom
        }

        #endregion

        #region 属性

        /// <summary>
        /// 二级权限代码
        /// </summary>
        private string class2Code = "8801";

        /// <summary>
        /// 二级权限代码
        /// </summary>
        [Category("F.权限设置"), Description("判断查询权限的二级代码")]
        public string Class2Code
        {
            get
            {
                return class2Code;
            }
            set
            {
                class2Code = value;
            }
        }

        /// <summary>
        /// 当前登陆员
        /// </summary>
        private FS.HISFC.Models.Base.Employee oper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

        #region A.参数设置
        private EnumShowType showType = EnumShowType.FarPoint;
        [Category("A.参数设置"), Description("数据窗口显示的类型")]
        public EnumShowType ShowType
        {
            get
            {
                return showType;
            }
            set
            {
                showType = value;
            }
        }

        private ReportQueryInfo report = new ReportQueryInfo();
        [Category("A.参数设置"), Description("设置查询参数")]
        public ReportQueryInfo Report
        {
            get
            {
                return report;
            }
            set
            {
                report = value;
            }
        }

        private string mainDWLabrary = string.Empty;
        [Category("A.参数设置"), Description("设置主窗口的PBD/PBL的路径")]
        public string MainDWLabrary
        {
            get
            {
                return mainDWLabrary;
            }
            set
            {
                mainDWLabrary = value;
            }
        }

        /// <summary>
        /// 主数据窗DataObject
        /// </summary>
        protected string mainDWDataObject = string.Empty;
        [Category("A.参数设置"), Description("设置主数据窗DataObject")]
        [Editor(typeof(FarPointUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
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

        #endregion

        #region B.位置设置
        [Category("B.位置设置"), Description("设置查询树的Top高度")]
        public int TreeTopHeight
        {
            get
            {
                return this.gbTreeTop.Height;
            }
            set
            {
                this.gbTreeTop.Height = value;
            }
        }

        [Category("B.位置设置"), Description("设置查询信息的Top高度")]
        public int QueryTopHeight
        {
            get
            {
                return this.gbQueryInfo.Height;
            }
            set
            {
                this.gbQueryInfo.Height = value;
            }
        }

        [Category("B.位置设置"), Description("设置查询树的Buttom高度")]
        public int TreeButtomHeight
        {
            get
            {
                return this.gbTreeButtom.Height;
            }
            set
            {
                this.gbTreeButtom.Height = value;
            }
        }

        [Category("B.位置设置"), Description("设置查询信息的Buttom高度")]
        public int QueryButtomHeight
        {
            get
            {
                return this.gbQueryButtom.Height;
            }
            set
            {
                this.gbQueryButtom.Height = value;
            }
        }

        [Category("B.位置设置"), Description("设置查询树的可见性")]
        public bool TreeLeftVisible
        {
            get
            {
                return this.pLeft.Visible;
            }
            set
            {
                this.pLeft.Visible = value;
            }
        }

        #endregion

        #region C.明细设置
        private bool isDetialDataObject = false;
        [Category("C.明细设置"), Description("是否加载明细报表")]
        public bool IsDetialDataObject
        {
            get
            {
                return isDetialDataObject;
            }
            set
            {
                isDetialDataObject = value;
                this.gbDataDetail.Visible = value;
            }
        }

        [Category("C.明细设置"), Description("明细报表显示高度")]
        public int DetailHeight
        {
            get
            {
                return this.gbDataDetail.Height;
            }
            set
            {
                this.gbDataDetail.Height = value;
            }
        }

        private string detailDWLabrary = string.Empty;
        [Category("C.明细设置"), Description("设置明细的PBD/PBL的路径")]
        public string DetailDWLabrary
        {
            get
            {
                return detailDWLabrary;
            }
            set
            {
                detailDWLabrary = value;
            }
        }

        /// <summary>
        /// 主数据窗DataObject
        /// </summary>
        protected string detailDWDataObject = string.Empty;
        [Category("C.明细设置"), Description("设置明细数据窗DataObject")]
        [Editor(typeof(FarPointUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string DetailDWDataObject
        {
            get
            {
                return this.detailDWDataObject;
            }
            set
            {
                this.detailDWDataObject = value;
            }
        }

        private EnumDetailCondition detailConditionType = EnumDetailCondition.UseMainData;
        [Category("C.明细设置"), Description("查询时，明细条件的来源；UseMainCondition：同主报表，UseMainData：使用主报表的数据，Custom：自定义")]
        public EnumDetailCondition DetailConditionType
        {
            get
            {
                return detailConditionType;
            }
            set
            {
                detailConditionType = value;
            }
        }

        private string useMainDataColName = "";
        [Category("C.明细设置"), Description("当DetailConditionType=UseMainData时，使用的数据列，用“|”隔开")]
        public string UseMainDataColName
        {
            get
            {
                return useMainDataColName;
            }
            set
            {
                useMainDataColName = value;
            }
        }

        #endregion

        #region D.打印设置
        [Category("D.打印设置"), Description("设置报表的打印纸张，255代表自定义英制（像素），256代表自定义公制（厘米）")]
        public int PaperSize
        {
            get
            {
                return this.IMainReportForm.PaperSize;
            }
            set
            {
                this.IMainReportForm.PaperSize = value;
            }
        }

        [Category("D.打印设置"), Description("设置自定义纸张的长度")]
        public int CustomPageLength
        {
            get
            {
                return this.IMainReportForm.CustomPageLength;
            }
            set
            {
                this.IMainReportForm.CustomPageLength = value;
            }
        }

        [Category("D.打印设置"), Description("设置自定义纸张的长度")]
        public int CustomPageWidth
        {
            get
            {
                return this.IMainReportForm.CustomPageWidth;
            }
            set
            {
                this.IMainReportForm.CustomPageWidth = value;
            }
        }

        [Category("D.打印设置"), Description("设置打印机名称")]
        public string PrintName
        {
            get
            {
                return this.IMainReportForm.PrintName;
            }
            set
            {
                this.IMainReportForm.PrintName = value;
            }
        }

        [Category("D.打印设置"), Description("左边距")]
        public int MarginLeft
        {
            get
            {
                return this.IMainReportForm.MarginLeft;
            }
            set
            {
                this.IMainReportForm.MarginLeft = value;
            }
        }

        [Category("D.打印设置"), Description("右边距")]
        public int MarginRight
        {
            get
            {
                return this.IMainReportForm.MarginRight;
            }
            set
            {
                this.IMainReportForm.MarginRight = value;
            }
        }

        [Category("D.打印设置"), Description("上边距")]
        public int MarginTop
        {
            get
            {
                return this.IMainReportForm.MarginTop;
            }
            set
            {
                this.IMainReportForm.MarginTop = value;
            }
        }

        [Category("D.打印设置"), Description("下边距")]
        public int MarginBottom
        {
            get
            {
                return this.IMainReportForm.MarginBottom;
            }
            set
            {
                this.IMainReportForm.MarginBottom = value;
            }
        }

        #endregion

        #region E.明细打印设置
        [Category("E.明细打印设置"), Description("设置报表的打印纸张，255代表自定义英制（像素），256代表自定义公制（厘米）")]
        public int DetailPaperSize
        {
            get
            {
                return this.IDetailReportForm.PaperSize;
            }
            set
            {
                this.IDetailReportForm.PaperSize = value;
            }
        }

        [Category("E.明细打印设置"), Description("设置自定义纸张的长度")]
        public int DetailCustomPageLength
        {
            get
            {
                return this.IDetailReportForm.CustomPageLength;
            }
            set
            {
                this.IDetailReportForm.CustomPageLength = value;
            }
        }

        [Category("E.明细打印设置"), Description("设置自定义纸张的长度")]
        public int DetailCustomPageWidth
        {
            get
            {
                return this.IDetailReportForm.CustomPageWidth;
            }
            set
            {
                this.IDetailReportForm.CustomPageWidth = value;
            }
        }

        [Category("E.明细打印设置"), Description("设置打印机名称")]
        public string DetailPrintName
        {
            get
            {
                return this.IDetailReportForm.PrintName;
            }
            set
            {
                this.IDetailReportForm.PrintName = value;
            }
        }

        [Category("E.明细打印设置"), Description("左边距")]
        public int DetailMarginLeft
        {
            get
            {
                return this.IDetailReportForm.MarginLeft;
            }
            set
            {
                this.IDetailReportForm.MarginLeft = value;
            }
        }

        [Category("E.明细打印设置"), Description("右边距")]
        public int DetailMarginRight
        {
            get
            {
                return this.IDetailReportForm.MarginRight;
            }
            set
            {
                this.IDetailReportForm.MarginRight = value;
            }
        }

        [Category("E.明细打印设置"), Description("上边距")]
        public int DetailMarginTop
        {
            get
            {
                return this.IDetailReportForm.MarginTop;
            }
            set
            {
                this.IDetailReportForm.MarginTop = value;
            }
        }

        [Category("E.明细打印设置"), Description("下边距")]
        public int DetailMarginBottom
        {
            get
            {
                return this.IDetailReportForm.MarginBottom;
            }
            set
            {
                this.IDetailReportForm.MarginBottom = value;
            }
        }

        private bool detialUserMainReport = false;
        [Category("E.明细打印设置"), Description("明细打印设置是否使用主报表的设置，仅用来将明细的打印设置修改同主报表一样，还可以另行修改")]
        public bool DetialUserMainReport
        {
            get
            {
                return this.detialUserMainReport;
            }
            set
            {
                detialUserMainReport = value;
                if (detialUserMainReport && this.isDetialDataObject)
                {
                    this.IDetailReportForm.PaperSize = this.IMainReportForm.PaperSize;
                    this.IDetailReportForm.CustomPageLength = this.IMainReportForm.CustomPageLength;
                    this.IDetailReportForm.CustomPageWidth = this.IMainReportForm.CustomPageWidth;
                    this.IDetailReportForm.PrintName = this.IMainReportForm.PrintName;
                    this.IDetailReportForm.MarginRight = this.IMainReportForm.MarginRight;
                    this.IDetailReportForm.MarginLeft = this.IMainReportForm.MarginLeft;
                    this.IDetailReportForm.MarginTop = this.IMainReportForm.MarginTop;
                    this.IDetailReportForm.MarginBottom = this.IMainReportForm.MarginBottom;
                }
            }
        }
        #endregion

        #endregion

        #region 接口类

        private ICommonReportController.IMainReportForm IMainReportForm = null;

        private ICommonReportController.IMainReportForm IDetailReportForm = null;

        private ICommonReportController.ILeftQueryCondition ILeftQueryCondition = null;

        private ICommonReportController.ITopQueryCondition ITopQueryCondition = null;
        #endregion

        #region  私有方法

        private void loadReportFile()
        {
            if (this.report != null)
            {
                if (string.IsNullOrEmpty(this.report.QueryFilePath))
                {
                    report.QueryFilePath = "\\Report\\住院收费报表\\住院收费日结查询设置.xml";
                }

                if (string.IsNullOrEmpty(this.report.QueryFilePath) == false)
                {
                    //读取 
                    if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + this.report.QueryFilePath) == false)
                    {
                        System.IO.File.Create(FS.FrameWork.WinForms.Classes.Function.CurrentPath + this.report.QueryFilePath).Close();

                        //保存
                        System.IO.StreamWriter swa = new System.IO.StreamWriter(FS.FrameWork.WinForms.Classes.Function.CurrentPath + this.report.QueryFilePath);
                        ReportQueryInfo reportQueryInfo = new ReportQueryInfo();
                        this.report.List = reportQueryInfo.GetDefaults();
                        //取默认值
                        swa.Write(ICommonReportController.XmlSerialization(this.report));
                        swa.Close();
                    }

                    else
                    {
                        System.IO.StreamReader sr = new System.IO.StreamReader(FS.FrameWork.WinForms.Classes.Function.CurrentPath + this.report.QueryFilePath);
                        string xml = sr.ReadToEnd();
                        ReportQueryInfo r = ICommonReportController.XmlDeSerialization<ReportQueryInfo>(xml);
                        this.report.List = r.List;
                        this.report.DataSourceType = r.DataSourceType;
                        sr.Close();
                    }
                }
            }

            if (this.showType == EnumShowType.FarPoint)
            {
                if (string.IsNullOrEmpty(this.mainDWDataObject))
                {
                    this.mainDWDataObject = "\\Report\\住院收费报表\\住院收费日结.xml";
                }
            }
        }

        private int initInterface()
        {
            this.IMainReportForm = ICommonReportController.CreateFactory().CreateInferface<ICommonReportController.IMainReportForm>(this.GetType(), null);
            if (this.IMainReportForm == null)
            {
                this.IMainReportForm = new ucMainFpReport();
            }
            IDetailReportForm = ICommonReportController.CreateFactory().CreateInferface<ICommonReportController.IMainReportForm>(this.GetType(), null);
            if (this.IDetailReportForm == null)
            {
                this.IDetailReportForm = new ucMainFpReport();
            }
            ILeftQueryCondition = ICommonReportController.CreateFactory().CreateInferface<ICommonReportController.ILeftQueryCondition>(this.GetType(), null);
            if (this.ILeftQueryCondition == null)
            {
                this.ILeftQueryCondition = new ucLeftQueryCondition();
            }
            ITopQueryCondition = ICommonReportController.CreateFactory().CreateInferface<ICommonReportController.ITopQueryCondition>(this.GetType(), null);
            if (this.ITopQueryCondition == null)
            {
                this.ITopQueryCondition = new ucTopQueryCondition();
            }

            this.IMainReportForm.Init();
            this.IMainReportForm.DataWindowObject = mainDWDataObject;
            this.IMainReportForm.LibraryList = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\" + mainDWLabrary;
            if (this.IMainReportForm is Control)
            {
                this.gbData.Controls.Clear();
                ((Control)this.IMainReportForm).Dock = DockStyle.Fill;
                this.gbData.Controls.Add(this.IMainReportForm as Control);
            }

            List<CommonReportQueryInfo> list = new List<CommonReportQueryInfo>();
            List<CommonReportQueryInfo> listView = new List<CommonReportQueryInfo>();
            if (this.report != null && this.report.List != null)
            {
                //初始化数据
                foreach (CommonReportQueryInfo common in this.report.List)
                {
                    if (common.ControlType is ComboBoxType)
                    {
                        ComboBoxType combo = common.ControlType as ComboBoxType;
                        combo.DataSource = this.queryDataByControlType(combo.DataSourceTypeName, combo.DataSourceType, combo.DefaultDataSource);
                        if (combo.IsAddAll)
                        {
                            combo.DataSource.Insert(0, combo.AllValue);
                        }
                        common.ControlType = combo;
                        list.Add(common);
                    }
                    else if (common.ControlType is TextBoxType
                        || common.ControlType is DateTimeType
                        || common.ControlType is FilterTextBox
                        || common.ControlType is CustomControl)
                    {
                        list.Add(common);
                    }
                    else if (common.ControlType is TreeViewType)
                    {
                        TreeViewType combo = common.ControlType as TreeViewType;
                        combo.DataSource = this.queryDataByControlType(combo.DataSourceTypeName, combo.DataSourceType, combo.DefaultDataSource);
                        common.ControlType = combo;
                        listView.Add(common);
                    }
                }

            }

            this.ITopQueryCondition.Init();
            this.ITopQueryCondition.AddControls(list);
            if (this.ITopQueryCondition is Control)
            {
                this.gbQueryInfo.Controls.Clear();
                ((Control)this.ITopQueryCondition).Dock = DockStyle.Fill;
                this.gbQueryInfo.Controls.Add(this.ITopQueryCondition as Control);
            }
            this.ITopQueryCondition.OnFilterHandler += new ICommonReportController.FilterHandler(ITopQueryCondition_OnFilterHandler);

            this.ILeftQueryCondition.Init();
            this.ILeftQueryCondition.AddControls(listView);
            if (this.ILeftQueryCondition is Control)
            {
                this.gbTreeView.Controls.Clear();
                ((Control)this.ILeftQueryCondition).Dock = DockStyle.Fill;
                this.gbTreeView.Controls.Add(this.ILeftQueryCondition as Control);
            }
            this.ILeftQueryCondition.OnFilterHandler += new ICommonReportController.FilterHandler(ILeftQueryCondition_OnFilterHandler);
            return 1;
        }

        private ArrayList queryDataByControlType(string type, EnumDataSourceType datasoureType, List<FS.FrameWork.Models.NeuObject> datasource)
        {
            switch (datasoureType)
            {
                case EnumDataSourceType.Sql://自定义SQL
                    FS.FrameWork.Management.DataBaseManger dbManager = new FS.FrameWork.Management.DataBaseManger();
                    if (dbManager.ExecQuery(type) == -1)
                    {
                        MessageBox.Show(this, "执行自定义查询SQL出错！" + dbManager.Err, "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    ArrayList al = new ArrayList();
                    while (dbManager.Reader.Read())
                    {
                        FS.HISFC.Models.Base.Spell obj = new FS.HISFC.Models.Base.Spell();
                        obj.ID = dbManager.Reader[0].ToString();
                        obj.Name = dbManager.Reader[1].ToString();
                        if (dbManager.Reader.FieldCount > 2)
                        {
                            obj.Memo = dbManager.Reader[2].ToString();
                        }
                        if (dbManager.Reader.FieldCount > 3)
                        {
                            obj.SpellCode = dbManager.Reader[3].ToString();
                        }
                        if (dbManager.Reader.FieldCount > 4)
                        {
                            obj.WBCode = dbManager.Reader[4].ToString();
                        }
                        if (dbManager.Reader.FieldCount > 5)
                        {
                            obj.UserCode = dbManager.Reader[5].ToString();
                        }
                        al.Add(obj);
                    }

                    return al;
                    break;
                case EnumDataSourceType.DepartmentType://科室
                    if (type == "ALL")
                    {
                        return CommonController.CreateInstance().QueryDepartment();
                    }
                    else
                    {
                        FS.HISFC.Models.Base.EnumDepartmentType deptType = FS.FrameWork.Function.NConvert.ToEnum<FS.HISFC.Models.Base.EnumDepartmentType>(type, FS.HISFC.Models.Base.EnumDepartmentType.C);
                        return CommonController.CreateInstance().QueryDepartment(deptType);
                    }
                    break;
                case EnumDataSourceType.Dictionary://常数
                    return CommonController.CreateInstance().QueryConstant(type);
                    break;
                case EnumDataSourceType.EmployeeType://人员
                    if (type == "ALL")
                    {
                        return CommonController.CreateInstance().QueryEmployee();
                    }
                    else
                    {
                        FS.HISFC.Models.Base.EnumEmployeeType emplType = FS.FrameWork.Function.NConvert.ToEnum<FS.HISFC.Models.Base.EnumEmployeeType>(type, FS.HISFC.Models.Base.EnumEmployeeType.D);
                        return CommonController.CreateInstance().QueryEmployee(emplType);
                    }
                    break;
                case EnumDataSourceType.CurrentDept:
                    if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                    {
                        return CommonController.CreateInstance().QueryDepartment();
                    }
                    else
                    {
                        ArrayList alDept = new ArrayList();
                        alDept.Add(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept);
                        return alDept;
                    }
                    break;
                case EnumDataSourceType.CurrentOper:
                    if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                    {
                        return CommonController.CreateInstance().QueryEmployee();
                    }
                    else
                    {
                        ArrayList alEmployee = new ArrayList();
                        alEmployee.Add(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator));
                        return alEmployee;
                    }
                    break;
                case EnumDataSourceType.Custom:
                default:
                    return new ArrayList(datasource);
                    break;
            }
        }

        private int query()
        {
            //清空历史日结日期【设置为空值】
            this.dtHistoryDateBegin = string.Empty;
            this.dtHistoryDateEnd = string.Empty;


            //获取参数
            List<Object> list = new List<Object>();
            Dictionary<string, Object> map = new Dictionary<string, object>();
            map["balanceState"] = "0";
            if (this.report != null)
            {
                map = this.report.GetDefualtSetting();
                if (this.report.List != null)
                {
                    foreach (CommonReportQueryInfo common in this.report.List)
                    {
                        //过滤窗口不需要
                        if (common.ControlType is FilterTextBox
                        || common.ControlType is FilterComboBox)
                        {
                            continue;
                        }

                        object value = this.ITopQueryCondition.GetValue(common);
                        if (common.ControlType.IsLike)
                        {
                            value = string.Format(common.ControlType.LikeStr, value);
                        }
                        list.Add(value);
                        map.Add(common.Name, value);
                    }
                }
            }

            return this.query(map);
        }

        private int query(Dictionary<string, Object> map)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询，请稍后...");
            Application.DoEvents();

            FS.FrameWork.Management.DataBaseManger dbManager = new FS.FrameWork.Management.DataBaseManger();
            if (this.report != null)
            {
                if (this.report.DataSourceType != null)
                {
                    string sql = string.Empty;
                    DataSet ds = new DataSet();
                    foreach (DataSourceType common in this.report.DataSourceType)
                    {
                        sql = Function.ReplaceValues(common.Sql, map);

                        if (Function.HasSelect(sql))
                        {
                            //使用参数
                            dbManager.ExecQuery(sql, ref ds);
                            if (dbManager.ExecQuery(sql, ref ds) == -1 || ds == null || ds.Tables.Count == 0)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                MessageBox.Show(this, "执行数据源" + common.Name + "的sql出错！" + dbManager.Err, "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return -1;
                            }
                            DataTable dt = ds.Tables[0];
                            //如果是交叉报表
                            if (common.IsCross)
                            {
                                //dt = this.queryCross(ds.Tables[0], common, map);
                            }

                            #region AddMapData
                            if (common.AddMapData)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    //添加数据
                                    for (int j = 0; j < dt.Columns.Count; j++)
                                    {
                                        map.Add(common.Name + ".Rows[" + i + "][" + j + "]", dt.Rows[i][j]);
                                        map.Add(common.Name + ".Rows[" + i + "][" + dt.Columns[j].ColumnName + "]", dt.Rows[i][j]);
                                    }
                                }
                            }
                            #endregion

                            #region AddMapRow
                            if (common.AddMapRow)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {

                                    map.Add(common.Name + ".Rows[" + i + "]", dt.Rows[i]);
                                }
                            }
                            #endregion

                            #region AddMapColumn
                            if (common.AddMapColumn)
                            {
                                DataTable dtColumns = new DataTable();

                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    dtColumns.Columns.Add(dt.Columns[i].ColumnName);
                                }
                                DataRow dr = dtColumns.NewRow();
                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    dr[i] = dt.Columns[i].ColumnName;
                                    map.Add(common.Name + ".Columns[" + i + "]", dt.Columns[i].ColumnName);
                                }
                                dtColumns.Rows.Add(dr);
                                map.Add(common.Name + ".Columns", dr);
                            }
                            #endregion

                            map.Add(common.Name, dt);
                        }
                        else
                        {
                            map.Add(common.Name, sql);
                        }
                    }
                }
            }

            allMap = map;

            int ret = this.IMainReportForm.Retrieve(map);

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();


            return ret;
        }

        private int balance()
        {
            FS.SOC.HISFC.InpatientFee.BizProcess.DayReport dayReportProcess = Function.GetDayReportBizProcess();
            if (allMap == null)//没有查询
            {
                return -1;
            }
            string beginTimeStr = string.Empty;
            if (allMap.ContainsKey("dtBeginTime"))
            {
                beginTimeStr = allMap["dtBeginTime"].ToString();
            }
            else
            {
                return -1;
            }
            string endTimeStr = string.Empty;
            if (allMap.ContainsKey("dtEndTime"))
            {
                endTimeStr = allMap["dtEndTime"].ToString();
            }
            else
            {
                return -1;
            }

            //开始时间
            DateTime dtBeginTime = DateTime.MinValue;
            string balanceNO =string.Empty;
            string error=string.Empty;
            int i = dayReportProcess.GetLastBalanceDateAndNO(FS.FrameWork.Management.Connection.Operator.ID, ref dtBeginTime, ref balanceNO);
            if (i < 0)
            {
                CommonController.CreateInstance().MessageBox("获取日结时间失败，原因：" + dayReportProcess.Err, MessageBoxIcon.Error);
                return -1;
            }

            if (dtBeginTime >new DateTime(1753,1,1))//说明日结过
            {
                if (!dtBeginTime.AddSeconds(1).Equals(FS.FrameWork.Function.NConvert.ToDateTime(beginTimeStr)))//如果开始时间不相同了
                {
                    CommonController.CreateInstance().MessageBox("本次已经日结过，请刷新界面", MessageBoxIcon.Asterisk);
                    return 1;
                }
            }
            //结束时间
            DateTime dtEndTime = FS.FrameWork.Function.NConvert.ToDateTime(endTimeStr);
            if (dtEndTime <= dtBeginTime)
            {
                CommonController.CreateInstance().MessageBox("没有日结的数据，请刷新界面", MessageBoxIcon.Asterisk);
                return 1;
            }

            //开始日结
            if (dayReportProcess.InpatientInvoice(FS.FrameWork.Management.Connection.Operator, dtBeginTime, dtEndTime) < 0)
            {
                CommonController.CreateInstance().MessageBox("日结失败，原因：" + dayReportProcess.Err, MessageBoxIcon.Error);
                return 1;
            }

            CommonController.CreateInstance().MessageBox("日结成功！" , MessageBoxIcon.Asterisk);

            allMap["balanceState"] = "1";
            //刷新界面
            this.ITopQueryCondition.Init();
            return this.ILeftQueryCondition.Init();
        }

        private int cancelBalance()
        {
            FS.SOC.HISFC.InpatientFee.BizProcess.DayReport dayReportProcess = Function.GetDayReportBizProcess();
            if (CommonController.CreateInstance().MessageBox("确认取消上一次日结？", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DateTime dtBeginTime = DateTime.MinValue;
                string balanceNO = string.Empty;
                string error = string.Empty;
                int i = dayReportProcess.GetLastBalanceDateAndNO(FS.FrameWork.Management.Connection.Operator.ID, ref dtBeginTime, ref balanceNO);
                if (i < 0)
                {
                    CommonController.CreateInstance().MessageBox("获取日结时间失败，原因：" + dayReportProcess.Err, MessageBoxIcon.Error);
                    return -1;
                }

                if (string.IsNullOrEmpty(balanceNO) == false)
                {
                    //开始取消日结
                    if (dayReportProcess.CancelInpatientInvoice(FS.FrameWork.Management.Connection.Operator, balanceNO) < 0)
                    {
                        CommonController.CreateInstance().MessageBox("取消日结失败，原因：" + dayReportProcess.Err, MessageBoxIcon.Error);
                        return 1;
                    }
                }

                CommonController.CreateInstance().MessageBox("取消日结成功！", MessageBoxIcon.Asterisk);

                //刷新界面
                this.ITopQueryCondition.Init();
                return this.ILeftQueryCondition.Init();
            }

            return 1;
        }

        /// <summary>
        /// 是否已日结
        /// </summary>
        /// <returns></returns>
        private bool isBalance()
        {
            if (this.allMap == null || this.allMap.ContainsKey("balanceState") == false || this.allMap["balanceState"] == null || this.allMap["balanceState"].ToString().Equals("0"))//说明没有日结过
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 工具栏

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("日结", "日结本次数据", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            this.toolBarService.AddToolButton("取消日结", "取消上次日结数据", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);

            //日结明细
            this.toolBarService.AddToolButton("预收明细", "收款员预收明细表", (int)FS.FrameWork.WinForms.Classes.EnumImageList.M明细, true, false, null);
            this.toolBarService.AddToolButton("发票明细", "收款员发票明细表", (int)FS.FrameWork.WinForms.Classes.EnumImageList.M明细, true, false, null);
            this.toolBarService.AddToolButton("发票退费明细", "收款员发票退费明细表", (int)FS.FrameWork.WinForms.Classes.EnumImageList.M明细, true, false, null);

            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "日结":
                    this.balance();
                    break;
                case "取消日结":
                    this.cancelBalance();
                    break;
                case "预收明细":
                    this.ShowInpatientDayBalancePrepayDetail();
                    break;
                case "发票明细":
                    this.ShowInpatientDayBalanceInvoicePositive();
                    break;
                case "发票退费明细":
                    this.ShowInpatientDayBalanceInvoiceNegative();
                    break;
                default:
                    base.ToolStrip_ItemClicked(sender, e);
                    break;
            }
        }

        #endregion

        #region 重载

        protected override void OnLoad(EventArgs e)
        {
            this.loadReportFile();
            this.initInterface();
            this.query();
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            return this.query();
        }

        public override int Export(object sender, object neuObject)
        {
            return this.Export();
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            return this.Print();
        }

        protected override int OnPrintPreview(object sender, object neuObject)
        {
            this.IMainReportForm.PrintPreview(true);
            if (this.isDetialDataObject)
            {
                this.IDetailReportForm.PrintPreview(true);
            }
            return 1;
        }

        private void ITopQueryCondition_OnFilterHandler(string filterStr)
        {
            this.IMainReportForm.OnFilter(filterStr);
        }

        private void ILeftQueryCondition_OnFilterHandler(string filterStr)
        {
            if (string.IsNullOrEmpty(filterStr))
            {
                return;
            }
            DateTime dtBeginTime = DateTime.MinValue;
            DateTime dtEndTime = DateTime.MinValue;
            string operCode = string.Empty;
            string error = string.Empty;
            if (Function.GetDayReportBizProcess().GetBalanceDate(filterStr, ref dtBeginTime, ref dtEndTime, ref operCode) < 0)
            {
                CommonController.CreateInstance().MessageBox("查询日结数据失败，原因：" + Function.GetDayReportBizProcess().Err, MessageBoxIcon.Error);
                return;
            }
            Dictionary<string, Object> map = this.report.GetDefualtSetting();
            map["dtBeginTime"] = dtBeginTime;
            map["dtEndTime"] = dtEndTime;
            map["CurrentOperID"] = operCode;
            map["balanceState"] = "1";
            map["balanceNO"] = filterStr;

            //历史记录的开始日期和结束日期
            this.dtHistoryDateBegin = dtBeginTime.ToString();
            this.dtHistoryDateEnd = dtEndTime.ToString();

            this.query(map);
        }

        #endregion

        #region IReport 成员

        public int Query()
        {
            return this.query();
        }

        public int SetParm(string parm, string reportID)
        {
            this.loadReportFile();
            return 1;
        }

        public int SetParm(string parm, string reportID, string emplSql, string deptSql)
        {
            return 1;
        }
        #endregion

        #region IReportPrinter 成员

        public int Export()
        {
            if (!this.isBalance())
            {
                CommonController.CreateInstance().MessageBox("您还没有日结，请先日结后在操作！", MessageBoxIcon.Asterisk);
                return -1;
            }
            this.IMainReportForm.Export();
            if (this.isDetialDataObject)
            {
                this.IDetailReportForm.Export();
            }
            return 1;
        }

        public int Print()
        {
            if (!this.isBalance())
            {
                CommonController.CreateInstance().MessageBox("您还没有日结，请先日结后在操作！", MessageBoxIcon.Asterisk);
                return -1;
            }
            try
            {
                if (!isOperation)
                {
                    isOperation = true; 
                    this.IMainReportForm.Print();

                    if (this.isDetialDataObject)
                    {
                        this.IDetailReportForm.Print();
                    }
                }
               
            }
            finally
            {
                isOperation = false; 
            }

            return 1;
        }

        public int PrintPreview()
        {
            this.IMainReportForm.PrintPreview(true);
            if (this.isDetialDataObject)
            {
                this.IDetailReportForm.PrintPreview(true);
            }
            return 1;
        }

        #endregion

        #region 住院日结明细

        /// <summary>
        /// 历史记录的开始日期
        /// </summary>
        private string dtHistoryDateBegin = string.Empty;

        /// <summary>
        /// 历史记录的结束日期
        /// </summary>
        private string dtHistoryDateEnd = string.Empty;

        /// <summary>
        /// 住院收款员预收明细表
        /// </summary>
        private void ShowInpatientDayBalancePrepayDetail()
        {
            if (string.IsNullOrEmpty(this.dtHistoryDateBegin) || string.IsNullOrEmpty(this.dtHistoryDateEnd))
            {
                MessageBox.Show("请选择左边的历史日结数据双击!");
                return;
            }
            ucDayBalanceDetail ucDetail = new ucDayBalanceDetail();
            ucDetail.Report.QueryFilePath = "\\Report\\住院收费报表\\收款员预收明细表(日结查询).xml";
            ucDetail.MainDWDataObject = "\\Report\\住院收费报表\\收款员预收明细表(日结报表).xml";
            ucDetail.dtBegin = this.dtHistoryDateBegin;  //开始日期
            ucDetail.dtEnd = this.dtHistoryDateEnd;    //结算日期

            System.Windows.Forms.Form frm = new System.Windows.Forms.Form();
            frm.Controls.Add(ucDetail);
            ucDetail.Dock = DockStyle.Fill;
            frm.Width = 1230;
            frm.Height = 600;
            frm.Text = "收款员预收明细表";
            frm.Show();
        }

        /// <summary>
        /// 住院收款员发票明细表
        /// </summary>
        private void ShowInpatientDayBalanceInvoicePositive()
        {
            if (string.IsNullOrEmpty(this.dtHistoryDateBegin) || string.IsNullOrEmpty(this.dtHistoryDateEnd))
            {
                MessageBox.Show("请选择左边的历史日结数据双击!");
                return;
            }
            ucDayBalanceDetail ucDetail = new ucDayBalanceDetail();
            ucDetail.Report.QueryFilePath = "\\Report\\住院收费报表\\收款员发票明细表(日结查询).xml";
            ucDetail.MainDWDataObject = "\\Report\\住院收费报表\\收款员发票明细表(日结报表).xml";
            ucDetail.dtBegin = this.dtHistoryDateBegin;  //开始日期
            ucDetail.dtEnd = this.dtHistoryDateEnd;    //结算日期

            System.Windows.Forms.Form frm = new System.Windows.Forms.Form();
            frm.Controls.Add(ucDetail);
            ucDetail.Dock = DockStyle.Fill;
            frm.Width = 1230;
            frm.Height = 600;
            frm.Text = "收款员发票明细表";
            frm.Show();

        }

        /// <summary>
        /// 收款员发票退费明细表
        /// </summary>
        private void ShowInpatientDayBalanceInvoiceNegative()
        {
            if (string.IsNullOrEmpty(this.dtHistoryDateBegin) || string.IsNullOrEmpty(this.dtHistoryDateEnd))
            {
                MessageBox.Show("请选择左边的历史日结数据双击!");
                return;
            }
            ucDayBalanceDetail ucDetail = new ucDayBalanceDetail();
            ucDetail.Report.QueryFilePath = "\\Report\\住院收费报表\\收款员发票退费明细表(日结查询).xml";
            ucDetail.MainDWDataObject = "\\Report\\住院收费报表\\收款员发票退费明细表(日结报表).xml";
            ucDetail.dtBegin = this.dtHistoryDateBegin;  //开始日期
            ucDetail.dtEnd = this.dtHistoryDateEnd;    //结算日期

            System.Windows.Forms.Form frm = new System.Windows.Forms.Form();
            frm.Controls.Add(ucDetail);
            ucDetail.Dock = DockStyle.Fill;
            frm.Width = 1230;
            frm.Height = 600;
            frm.Text = "收款员发票退费明细表";
            frm.Show();
        }

        #endregion

    }

    [Serializable]
    [Editor(typeof(SetInfoUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
    [TypeConverter(typeof(ListTypeConverter))]
    public class ReportQueryInfo
    {
        private List<CommonReportQueryInfo> list = new List<CommonReportQueryInfo>();

        public List<CommonReportQueryInfo> List
        {
            get
            {
                return list;
            }
            set
            {
                list = value;
            }
        }

        private List<DataSourceType> dataSourceType = new List<DataSourceType>();
        public List<DataSourceType> DataSourceType
        {
            get
            {
                return this.dataSourceType;
            }
            set
            {
                this.dataSourceType = value;
            }
        }

        public void SetGenericTypeValue(Object mainObj, Object genericValue, System.Reflection.PropertyInfo p)
        {
            if (p.Name == "List" && genericValue != null)
            {
                List<CommonReportQueryInfo> list = new List<CommonReportQueryInfo>();
                foreach (CommonReportQueryInfo step in (System.Collections.ICollection)genericValue)
                {
                    list.Add(step);
                }

                p.SetValue(mainObj, list, null);
            }
            else if (p.Name == "DataSourceType" && genericValue != null)
            {
                List<DataSourceType> dataSourceType = new List<DataSourceType>();
                foreach (DataSourceType step in (System.Collections.ICollection)genericValue)
                {
                    dataSourceType.Add(step);
                }

                p.SetValue(mainObj, dataSourceType, null);
            }
        }

        private string filePath = "";
        /// <summary>
        /// 查询条件的配置文件
        /// </summary>
        public string QueryFilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
            }
        }

        public List<CommonReportQueryInfo> GetDefaults()
        {
            List<CommonReportQueryInfo> listDefault = new List<CommonReportQueryInfo>();
            CommonReportQueryInfo commonDefault = new CommonReportQueryInfo();
            commonDefault.Index = 0;
            commonDefault.Name = "dtBeginTime";
            commonDefault.Text = "开始时间：";
            DateTimeType dt = new DateTimeType();
            dt.AddDays = -1;
            dt.Location = new Point(10, 2);
            commonDefault.ControlType = dt;
            listDefault.Add(commonDefault);

            commonDefault = new CommonReportQueryInfo();
            commonDefault.Index = 0;
            commonDefault.Name = "dtEndTime";
            commonDefault.Text = "结束时间：";
            dt = new DateTimeType();
            dt.Location = new Point(220, 2);
            commonDefault.ControlType = dt;
            listDefault.Add(commonDefault);

            return listDefault;
        }

        public Dictionary<string, Object> GetDefualtSetting()
        {
            Dictionary<string, Object> map = new Dictionary<string, object>();
            map.Add("CurrentOperID", FS.FrameWork.Management.Connection.Operator.ID);
            map.Add("CurrentOperName", FS.FrameWork.Management.Connection.Operator.Name);
            map.Add("HospitalID", FS.FrameWork.Management.Connection.Hospital.ID);
            map.Add("HospitalName", FS.FrameWork.Management.Connection.Hospital.Name);
            if (FS.FrameWork.Management.Connection.Operator is FS.HISFC.Models.Base.Employee)
            {
                FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

                map.Add("CurrentDeptID", employee.Dept.ID);
                map.Add("CurrentDeptName", SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetDepartmentName(employee.Dept.Name));
            }
            DateTime sysdate = CommonController.Instance.GetSystemTime();
            map.Add("CurrentSysTime", sysdate.ToString());

            return map;
        }

        public override string ToString()
        {
            return filePath;
        }
    }
    public class ListTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return true;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is String)
            {
                ReportQueryInfo a = new ReportQueryInfo();
                a.QueryFilePath = value.ToString();

                return a;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value == null)
            {
                return "";
            }

            //序列化
            if (value is ReportQueryInfo)
            {
                return ((ReportQueryInfo)value).ToString();
            }
            return "";
        }
    }
    public class SetInfoUITypeEditor : System.Drawing.Design.UITypeEditor
    {
        System.Windows.Forms.Design.IWindowsFormsEditorService editorService = null;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context != null && context.Instance != null && provider != null)
            {
                editorService = (System.Windows.Forms.Design.IWindowsFormsEditorService)provider.GetService(typeof(System.Windows.Forms.Design.IWindowsFormsEditorService));
                if (editorService != null)
                {
                    if (string.IsNullOrEmpty(((ReportQueryInfo)value).QueryFilePath))
                    {
                        ((ReportQueryInfo)value).QueryFilePath = FS.FrameWork.WinForms.Classes.Function.SettingPath + "\\" + Guid.NewGuid().ToString() + ".xml";
                    }

                    if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + ((ReportQueryInfo)value).QueryFilePath) == false)
                    {
                        System.IO.File.Create(FS.FrameWork.WinForms.Classes.Function.CurrentPath + ((ReportQueryInfo)value).QueryFilePath).Close();

                        //保存
                        System.IO.StreamWriter swa = new System.IO.StreamWriter(FS.FrameWork.WinForms.Classes.Function.CurrentPath + ((ReportQueryInfo)value).QueryFilePath);
                        ReportQueryInfo reportQueryInfo = new ReportQueryInfo();
                        ((ReportQueryInfo)value).List = reportQueryInfo.GetDefaults();
                        //取默认值
                        swa.Write(ICommonReportController.XmlSerialization(((ReportQueryInfo)value)));
                        swa.Close();
                    }
                    else
                    {
                        //读取
                        System.IO.StreamReader sr = new System.IO.StreamReader(FS.FrameWork.WinForms.Classes.Function.CurrentPath + ((ReportQueryInfo)value).QueryFilePath);
                        string xml = sr.ReadToEnd();
                        ReportQueryInfo r = ICommonReportController.XmlDeSerialization<ReportQueryInfo>(xml);
                        ((ReportQueryInfo)value).List = r.List;
                        ((ReportQueryInfo)value).DataSourceType = r.DataSourceType; ;
                        sr.Close();
                    }

                    ReportQueryInfo a = ((ReportQueryInfo)value);

                    ICommonReportController.ISettingReportForm ISettingReportForm =
                         ICommonReportController.CreateFactory().CreateInferface<ICommonReportController.ISettingReportForm>(typeof(ucInpatientBalance), null);
                    if (ISettingReportForm == null)
                    {
                        ISettingReportForm = new frmQueryInfoSetting();
                    }
                    ISettingReportForm.SetValue(a);
                    if (ISettingReportForm is Form)
                    {
                        editorService.ShowDialog(ISettingReportForm as Form);
                    }

                    a = ISettingReportForm.GetValue();
                    a.QueryFilePath = ((ReportQueryInfo)value).QueryFilePath;
                    //保存
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(FS.FrameWork.WinForms.Classes.Function.CurrentPath + a.QueryFilePath);
                    sw.Write(ICommonReportController.XmlSerialization(a));
                    sw.Close();
                    return base.EditValue(context, provider, a);
                }
            }

            return base.EditValue(context, provider, value);

        }

        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }
    }
    public class FarPointUITypeEditor : System.Drawing.Design.UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (value is string)
            {
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    value = "Report\\" + Guid.NewGuid().ToString() + ".xml";
                }

                FarPoint.Win.Spread.Design.DesignerMain c = new FarPoint.Win.Spread.Design.DesignerMain();


                if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + value.ToString()) == false)
                {
                    System.IO.FileStream fs = System.IO.File.Create(FS.FrameWork.WinForms.Classes.Function.CurrentPath + value.ToString());
                    c.Spread.Save(fs, false);
                    fs.Close();
                }

                c.Spread.Open(FS.FrameWork.WinForms.Classes.Function.CurrentPath + value.ToString());
                c.Tag = FS.FrameWork.WinForms.Classes.Function.CurrentPath + value.ToString();

                c.FormClosing += new FormClosingEventHandler(c_FormClosing);
                c.ShowDialog();


                return base.EditValue(context, provider, value);
            }

            return base.EditValue(context, provider, value);
        }

        void c_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((FarPoint.Win.Spread.Design.DesignerMain)sender).SaveFile(((FarPoint.Win.Spread.Design.DesignerMain)sender).Tag.ToString());
        }

        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }
    }
}
