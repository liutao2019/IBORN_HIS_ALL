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

namespace FS.SOC.HISFC.Components.Report.DataWindow
{
    /// <summary>
    /// 报表通用界面
    /// </summary>
    public partial class ucCommonDataWindow : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IReport
    {
        public ucCommonDataWindow()
        {
            InitializeComponent();
        }

        private string resourceID = "";
        private Dictionary<string, Object> allMap = null;

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
        /// 是否控制权限
        /// </summary>
        private bool isControlPriv = false;

        /// <summary>
        /// 是否控制权限
        /// </summary>
        [Category("F.权限设置"), Description("是否控制权限(SQL需要同步处理：全院权限为All，科室权限为当前登陆科室编码")]
        public bool IsControlPriv
        {
            get
            {
                return isControlPriv;
            }
            set
            {
                isControlPriv = value;
            }
        }

        /// <summary>
        /// 是否有全院查询权限
        /// </summary>
        private bool isHaveAllQuery = false;

        /// <summary>
        /// 当前登陆员
        /// </summary>
        private FS.HISFC.Models.Base.Employee oper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

        /// <summary>
        /// 是否有科室查询权限
        /// </summary>
        private bool isHaveDeptQuery = false;

        #region A.参数设置
        private EnumShowType showType = EnumShowType.Datawindow;
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

        /// <summary>
        /// 是否使用Datatable
        /// </summary>
        private bool isUseCustomSqlID = false;
        [Category("A.参数设置"), Description("查询时，是否使用自定义的SQLID进行查询")]
        public bool IsUseCustomSqlID
        {
            get
            {
                return this.isUseCustomSqlID;
            }
            set
            {
                this.isUseCustomSqlID = value;
            }
        }

        /// <summary>
        /// 当IsUseCustomSqlID=True 时，查询使用的SQLID
        /// </summary>
        private string sqlID = "";
        [Category("A.参数设置"), Description("查询时，当IsUseCustomSqlID=True 时，查询使用的SQLID")]
        public string SqlID
        {
            get
            {
                return this.sqlID;
            }
            set
            {
                this.sqlID = value;
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

        /// <summary>
        /// 是否使用Datatable
        /// </summary>
        private bool isDetailUseCustomSqlID = false;
        [Category("C.明细设置"), Description("查询时，明细报表是否使用自定义的SQLID进行查询")]
        public bool IsDetailUseCustomSqlID
        {
            get
            {
                return this.isDetailUseCustomSqlID;
            }
            set
            {
                this.isDetailUseCustomSqlID = value;
            }
        }

        /// <summary>
        /// 当IsDetailUseCustomSqlID=True 时，查询使用的SQLID
        /// </summary>
        private string sqlDetailID = "";
        [Category("C.明细设置"), Description("查询时，当IsDetailUseCustomSqlID=True 时，查询使用的SQLID")]
        public string SqlDetailID
        {
            get
            {
                return this.sqlDetailID;
            }
            set
            {
                this.sqlDetailID = value;
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
                if (detialUserMainReport&&this.isDetialDataObject)
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
                    if (string.IsNullOrEmpty(resourceID))
                    {
                        if (this.ParentForm != null && this.ParentForm is FS.FrameWork.WinForms.Forms.frmBaseForm)
                        {
                            resourceID = ((FS.FrameWork.WinForms.Forms.frmBaseForm)(this.ParentForm)).formID;
                        }
                    }

                    report.QueryFilePath = FS.FrameWork.WinForms.Classes.Function.SettingPath + "\\" + resourceID + ".xml";
                }

                if (string.IsNullOrEmpty(this.report.QueryFilePath) == false)
                {
                    //读取 
                    if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath+this.report.QueryFilePath) == false)
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
                    if (string.IsNullOrEmpty(resourceID))
                    {
                        if (this.ParentForm != null && this.ParentForm is FS.FrameWork.WinForms.Forms.frmBaseForm)
                        {
                            resourceID = ((FS.FrameWork.WinForms.Forms.frmBaseForm)(this.ParentForm)).formID;
                        }
                    }

                    this.mainDWDataObject = "Report\\" + resourceID + ".xml";
                }
            }
        }

        private int initInterface()
        {
            this.IMainReportForm = ICommonReportController.CreateFactory().CreateInferface<ICommonReportController.IMainReportForm>(typeof(ucCommonDataWindow), null);
            if (this.IMainReportForm == null)
            {
                if (this.showType == EnumShowType.Datawindow)
                {
                    this.IMainReportForm = new ucMainReportForm();
                }
                else
                {
                    this.IMainReportForm = new ucMainFpReport();
                }
            }
            IDetailReportForm = ICommonReportController.CreateFactory().CreateInferface<ICommonReportController.IMainReportForm>(typeof(ucCommonDataWindow),null);
            if (this.IDetailReportForm == null)
            {
                if (this.showType == EnumShowType.Datawindow)
                {
                    this.IDetailReportForm = new ucMainReportForm();
                }
                else
                {
                    this.IDetailReportForm = new ucMainFpReport();
                }
            }
            ILeftQueryCondition = ICommonReportController.CreateFactory().CreateInferface<ICommonReportController.ILeftQueryCondition>(typeof(ucCommonDataWindow), null);
            if (this.ILeftQueryCondition == null)
            {
                this.ILeftQueryCondition = new ucLeftQueryCondition();
            }
            ITopQueryCondition = ICommonReportController.CreateFactory().CreateInferface<ICommonReportController.ITopQueryCondition>(typeof(ucCommonDataWindow), null);
            if (this.ITopQueryCondition == null)
            {
                this.ITopQueryCondition = new ucTopQueryCondition();
            }

            this.IMainReportForm.Init();
            this.IMainReportForm.DataWindowObject = mainDWDataObject;
            this.IMainReportForm.LibraryList = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\" + mainDWLabrary;
            this.IMainReportForm.OnSelectRowHandler += new ICommonReportController.SelectRowHanlder(IMainReportForm_OnSelectRowHandler);
            if (this.IMainReportForm is Control)
            {
                this.gbData.Controls.Clear();
                ((Control)this.IMainReportForm).Dock = DockStyle.Fill;
                this.gbData.Controls.Add(this.IMainReportForm as Control);
            }

            if (this.isDetialDataObject)
            {
                this.IDetailReportForm.Init();
                this.IDetailReportForm.DataWindowObject = this.detailDWDataObject;
                this.IDetailReportForm.LibraryList = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\" + this.detailDWLabrary;
                if (this.IDetailReportForm is Control)
                {
                    this.gbDataDetail.Controls.Clear();
                    ((Control)this.IDetailReportForm).Dock = DockStyle.Fill;
                    this.gbDataDetail.Controls.Add(this.IDetailReportForm as Control);
                }
            }

            List<CommonReportQueryInfo> list = new List<CommonReportQueryInfo>();
            List<CommonReportQueryInfo> listView = new List<CommonReportQueryInfo>();
            if (this.report != null&&this.report.List!=null)
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
                        ||common.ControlType is FilterTextBox
                        ||common.ControlType is CustomControl)
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
                        if (dbManager.Reader.FieldCount >2)
                        {
                            obj.Memo = dbManager.Reader[2].ToString();
                        }
                        if (dbManager.Reader.FieldCount > 3)
                        {
                            obj.SpellCode = dbManager.Reader[3].ToString();
                        }
                        if (dbManager.Reader.FieldCount >4)
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
                        FS.HISFC.Models.Base.EnumEmployeeType emplType=FS.FrameWork.Function.NConvert.ToEnum<FS.HISFC.Models.Base.EnumEmployeeType>(type, FS.HISFC.Models.Base.EnumEmployeeType.D);
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
            this.GetPriv();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询，请稍后...");
            Application.DoEvents();
            //获取参数
            List<Object> list = new List<Object>();
            Dictionary<string, Object> map = this.report.GetDefualtSetting();
            FS.FrameWork.Management.DataBaseManger dbManager = new FS.FrameWork.Management.DataBaseManger();
            if (this.report != null )
            {
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

                if (this.report.DataSourceType != null)
                {
                    string sql = string.Empty;
                    DataSet ds=new DataSet();
                    foreach (DataSourceType common in this.report.DataSourceType)
                    {
                        sql = TemplateValueReplacer.ReplaceValues(common.Sql, map);

                        if (TemplateValueReplacer.HasSelect(sql))
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
                                dt = this.queryCross(ds.Tables[0], common, map);
                            }

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



            if (this.isControlPriv)
            {
                list.Add(this.isHaveAllQuery ? "All" : this.isHaveDeptQuery ? oper.Dept.ID : oper.ID);
            }

            int ret = 0;

            if (isUseCustomSqlID)
            {
                DataSet dsResult = new DataSet();
                //if (dbManager.ExecQuery(string.Format(this.sqlID, list.ToArray()), ref dsResult) == -1 || dsResult == null || dsResult.Tables.Count == 0) 
                string[] parms = new string[list.Count];
                for (int j = 0; j < list.Count; j++)
                {
                    parms[j] = list[j].ToString();
                }

                if (dbManager.ExecQuery(this.sqlID, ref dsResult, parms) == -1 || dsResult == null || dsResult.Tables.Count == 0)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show(this, "执行自定义查询SQL出错！" + dbManager.Err, "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                ret = this.IMainReportForm.Retrieve(dsResult.Tables[0]);
            }
            else
            {
                ret = this.IMainReportForm.Retrieve(list.ToArray());
                ret = this.IMainReportForm.Retrieve(map);
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            if (this.isDetialDataObject )
            {
                if (this.detailConditionType == EnumDetailCondition.UseMainCondition)
                {
                     this.queryDetail(list.ToArray());
                     return this.queryDetail(map);
                }
                else if (this.detailConditionType == EnumDetailCondition.UseMainData)
                {
                    if (this.IMainReportForm.RowCount > 0)
                    {
                        this.IMainReportForm_OnSelectRowHandler(1);
                    }
                    else
                    {
                        this.IDetailReportForm.Reset();
                    }
                }
                else
                {
                    this.IDetailReportForm.Reset();
                }
                return ret;
            }
            else
            {
                return ret;
            }
        }

        /// <summary>
        /// 获取查询权限
        /// </summary>
        private void GetPriv()
        {
            //权限管理类
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager privManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            List<FS.FrameWork.Models.NeuObject> al = null;

            //取操作员拥有权限的科室
            al = privManager.QueryUserPriv(privManager.Operator.ID, class2Code, "01");

            if (al == null || al.Count == 0)
                this.isHaveAllQuery = false;
            else
                this.isHaveAllQuery = true;

            //取操作员拥有权限的科室
            al = privManager.QueryUserPriv(privManager.Operator.ID, class2Code, "02");

            if (al == null || al.Count == 0)
                this.isHaveDeptQuery = false;
            else
                this.isHaveDeptQuery = true;
        }

        private int queryDetail(params Object[] param)
        {
            if (this.isDetailUseCustomSqlID)
            {
                FS.FrameWork.Management.DataBaseManger dbManager = new FS.FrameWork.Management.DataBaseManger();
                DataSet dsResult = new DataSet();
                if (dbManager.ExecQuery(string.Format(this.sqlDetailID,param), ref dsResult) == -1 || dsResult == null || dsResult.Tables.Count == 0)
                {
                    MessageBox.Show(this, "执行自定义查询SQL出错！" + dbManager.Err, "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                return this.IDetailReportForm.Retrieve(dsResult.Tables[0]);
            }
            else
            {
                return this.IDetailReportForm.Retrieve(param);
            }

        }

        private int queryDetail(Dictionary<string, Object> map)
        {
            return this.IDetailReportForm.Retrieve(map);
        }

        /// <summary>
        /// 处理交叉报表
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        private DataTable queryCross(DataTable dt, DataSourceType common, Dictionary<string, Object> map)
        {

            DataSetHelper dsh = new DataSetHelper();
            //先取交叉报表的行
            string[] crossRows = common.CrossRows.Split('|');
            ///生成行数据集，不包括合计行
            DataTable dtCrossRows = dsh.SelectDistinctByIndexs("dtCrossRows", dt, crossRows);

            string[] crossColumns = common.CrossColumns.Split('|');
            ///生成列数据集，不包括合计列
            DataTable dtCrossColumns = dsh.SelectDistinctByIndexs("dtCrossColumns", dt, crossColumns);

            //形成新的数据列表
            string[] crossValues = common.CrossValues.Split('|');
            DataTable dtCrossValues = new DataTable();
            dtCrossValues = new DataTable();

            //合并列
            string[] crossCombinColumns = common.CrossCombinColumns.Split('|');
            string[] crossGroupColumns = common.CrossGroupColumns.Split('|');

            #region 向值数据集添加列

            foreach (DataColumn dc in dtCrossRows.Columns)
            {
                dtCrossValues.Columns.Add(dc.ColumnName, dc.DataType);
            }

            foreach (DataRow dr in dtCrossColumns.Rows)
            {
                foreach (DataColumn dc in dtCrossColumns.Columns)
                {
                    if (crossValues.Length == 1)
                    {
                        //值数据集，添加一列
                        dtCrossValues.Columns.Add(
                            new DataColumn(dr[dc].ToString(),
                                                            dt.Columns[crossValues[0]].DataType));
                    }
                    else
                    {
                        foreach (string s in crossValues)
                        {
                            //值数据集，添加一列
                            dtCrossValues.Columns.Add(
                                new DataColumn(dr[dc].ToString() + s,
                                                                dt.Columns[s].DataType));

                        }
                    }
                }
            }

            dtCrossValues.Columns.Add("合计", typeof(System.Decimal));

            #endregion

            //形成新的数据内容
            StringBuilder currExp = null;
            StringBuilder currColumnExp = null;
            string sampleValues = string.Empty;
            string selectSampleValues = "1=1";
            foreach (DataRow drRow in dtCrossRows.Rows)
            {
                
                //值数据集，添加一行
                DataRow drValues = dtCrossValues.NewRow();
                foreach (DataColumn dcCrossRow in dtCrossRows.Columns)
                {
                    drValues[dcCrossRow.ColumnName] = drRow[dcCrossRow.ColumnName];
                }

                ///当前单元格的数据值表达式
                currExp = new StringBuilder("1=1");
                #region 遍历行集，计算当前单元格值的行表达式部分

                foreach (string sDataCrossRows in crossRows)
                {
                    //根据原始数据集对应值列的类型计算表达式
                    switch (dt.Columns[sDataCrossRows].DataType.ToString())
                    {
                        case "System.Decimal":
                            {
                                currExp = currExp.Append(" AND ").Append(dt.Columns[sDataCrossRows].Caption).Append(" = ").Append(drRow[sDataCrossRows].ToString());
                                break;
                            }
                        case "System.DateTime":
                            {
                                currExp = currExp.Append(" AND ").Append(dt.Columns[sDataCrossRows].Caption).Append(" = #").Append(drRow[sDataCrossRows].ToString()).Append("# ");
                                break;
                            }
                        case "System.String":
                        default:
                            {
                                currExp = currExp.Append(" AND ").Append(dt.Columns[sDataCrossRows].Caption).Append(" = '").Append(drRow[sDataCrossRows].ToString()).Append("'");
                                break;
                            }
                    }
                }
                #endregion

                foreach (DataRow drColumn in dtCrossColumns.Rows)
                {
                    currColumnExp = new StringBuilder(currExp.ToString());
                    #region 遍历列集，计算当前单元格的,数据值的，列表达式部分
                    foreach (DataColumn dcCrossColumn in dtCrossColumns.Columns)
                    {
                        //根据原始数据集对应值列的类型计算表达式
                        switch (dt.Columns[dcCrossColumn.ColumnName].DataType.ToString())
                        {
                            case "System.Decimal":
                                {
                                    currColumnExp.Append(" AND ").Append(dt.Columns[dcCrossColumn.ColumnName].Caption).Append(" = ").Append(drColumn[dcCrossColumn].ToString());
                                    break;
                                }
                            case "System.DateTime":
                                {
                                    currColumnExp.Append(" AND ").Append(dt.Columns[dcCrossColumn.ColumnName].Caption).Append(" = #").Append(drColumn[dcCrossColumn]).Append("#");
                                    break;
                                }
                            case "System.String":
                            default:
                                {
                                    currColumnExp.Append(" AND ").Append(dt.Columns[dcCrossColumn.ColumnName].Caption).Append(" = '").Append(drColumn[dcCrossColumn]).Append("'");
                                    break;
                                }
                        }


                        #region 遍历值数据集每一行

                        DataRow[] arryDr = dt.Select(currColumnExp.ToString());
                        //当前单元格的数据值
                        string[] currVal = new string[crossValues.Length];
                        int currValIdx = 0;

                        foreach (string s in crossValues)
                        {
                            currValIdx = Array.IndexOf(crossValues, s);
                            currVal[currValIdx] = "0";
                            #region 遍历取查询出来的行，计算对应位置的值
                            if (arryDr.Length > 0)
                            {
                                //有可能有多行，遍历取值
                                foreach (DataRow drCurrExp in arryDr)
                                {
                                    switch (drCurrExp.Table.Columns[s].DataType.ToString())
                                    {
                                        case "System.Decimal":
                                            {
                                                currVal[currValIdx] = (decimal.Parse(currVal[currValIdx]) + decimal.Parse(drCurrExp[s].ToString())).ToString();
                                                break;
                                            }
                                        case "System.DateTime":
                                            {
                                                currVal[currValIdx] = DateTime.Parse(drCurrExp[s].ToString()).ToString();
                                                break;
                                            }
                                        case "System.String":
                                            {
                                                currVal[currValIdx] = drCurrExp[s].ToString();
                                                break;
                                            }
                                        default:
                                            {
                                                currVal[currValIdx] = (int.Parse(currVal[currValIdx]) + int.Parse(drCurrExp[s].ToString())).ToString();
                                                break;
                                            }
                                    }
                                }
                            }
                            #endregion
                            //添加合计
                            if (crossValues.Length == 1)
                            {

                                drValues[drColumn[dcCrossColumn].ToString()] = currVal[currValIdx];
                                if (dtCrossValues.Columns[drColumn[dcCrossColumn].ToString()].DataType.IsValueType)
                                {
                                    drValues["合计"] = FS.FrameWork.Function.NConvert.ToDecimal(drValues["合计"]) + FS.FrameWork.Function.NConvert.ToDecimal(currVal[currValIdx]);
                                }
                            }
                            else
                            {

                                drValues[drColumn[dcCrossColumn].ToString() + s] = currVal[currValIdx];
                                if (dtCrossValues.Columns[drColumn[dcCrossColumn].ToString() + s].DataType.IsValueType)
                                {
                                    drValues["合计"] = FS.FrameWork.Function.NConvert.ToDecimal(drValues["合计"]) + FS.FrameWork.Function.NConvert.ToDecimal(currVal[currValIdx]);
                                }
                            }
                        }
                        #endregion
                    }

                    #endregion

                }


                if (crossGroupColumns.Length > 0)
                {
                    string currentSampleValues = string.Empty;
                    string currentSelectSampleValues = "1=1";
                    foreach (string crossGroupColumn in crossGroupColumns)
                    {
                        currentSampleValues += drValues[crossGroupColumn].ToString();
                        currentSelectSampleValues += " AND " + crossGroupColumn + " = '" + drValues[crossGroupColumn].ToString() + "'";
                    }
                    if (string.Empty.Equals(sampleValues))
                    {
                        sampleValues = currentSampleValues;
                    }

                    //添加小计
                    //判断是否与上一个相等
                    if (!sampleValues.Equals(currentSampleValues))
                    {
                        //添加合计
                        dtCrossValues.AcceptChanges();
                        DataRow drSumRow = dtCrossValues.NewRow();
                        drSumRow[crossGroupColumns[0]] = "小计：";

                        foreach (DataColumn dc in dtCrossValues.Columns)
                        {
                            if (dc.DataType.IsValueType)
                            {
                                drSumRow[dc.ColumnName] = dtCrossValues.Compute("sum(" + dc.ColumnName + ")", selectSampleValues);
                            }
                        }
                        dtCrossValues.Rows.Add(drSumRow);
                        //添加空行
                        dtCrossValues.Rows.Add(dtCrossValues.NewRow());
                        sampleValues = currentSampleValues;
                        selectSampleValues = currentSelectSampleValues;
                    }
                }
                dtCrossValues.Rows.Add(drValues);

            }

            if (dtCrossValues.Rows.Count > 0)
            {
                DataRow drValues = dtCrossValues.Rows[dtCrossValues.Rows.Count - 1];
                if (crossGroupColumns.Length > 0)
                {
                    //添加小计
                    //判断是否与上一个相等
                    
                        //添加合计
                        dtCrossValues.AcceptChanges();
                        DataRow drSumRow = dtCrossValues.NewRow();
                        drSumRow[crossGroupColumns[0]] = "小计：";

                        foreach (DataColumn dc in dtCrossValues.Columns)
                        {
                            if (dc.DataType.IsValueType)
                            {
                                drSumRow[dc.ColumnName] = dtCrossValues.Compute("sum(" + dc.ColumnName + ")", selectSampleValues);
                            }
                        }
                        dtCrossValues.Rows.Add(drSumRow);
                        //添加空行
                        dtCrossValues.Rows.Add(dtCrossValues.NewRow());
                }
            }

            //添加列合计
            DataRow drNewRow = dtCrossValues.NewRow();
            drNewRow[0] = "合计：";
            foreach (DataColumn dc in dtCrossValues.Columns)
            {
                if (dc.DataType.IsValueType)
                {
                    if (crossGroupColumns.Length > 0)
                    {
                        drNewRow[dc.ColumnName] = dtCrossValues.Compute("sum(" + dc.ColumnName + ")", crossGroupColumns[0]+ "= '小计：'");
                    }
                    else
                    {
                        drNewRow[dc.ColumnName] = dtCrossValues.Compute("sum(" + dc.ColumnName + ")", "");
                    }
                }
            }
            dtCrossValues.Rows.Add(drNewRow);

            return dtCrossValues;
        }

        #endregion

        #region 工具栏

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("明细导出", "导出明细信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D导出, true, false, null);
            this.toolBarService.AddToolButton("明细打印", "打印明细信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("明细打印预览", "预览明细信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Y预览, true, false, null);

            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "明细打印":
                    if (this.isDetialDataObject)
                    {
                        this.IDetailReportForm.Print();
                    }
                    break;
                case "明细导出":
                    if (this.isDetialDataObject)
                    {
                        this.IDetailReportForm.Export();
                    }
                    break;
                case "明细打印预览":
                    if (this.isDetialDataObject)
                    {
                        this.IDetailReportForm.PrintPreview(true);
                    }
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
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            return this.query();
        }

        public override int Export(object sender, object neuObject)
        {
             this.IMainReportForm.Export();
            if (this.isDetialDataObject)
            {
                this.IDetailReportForm.Export();
            }
            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.IMainReportForm.Print();

            if (this.isDetialDataObject)
            {
                this.IDetailReportForm.Print();
            }
            return 1;
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

        private void IMainReportForm_OnSelectRowHandler(int row)
        {
            if (this.isDetialDataObject)
            {
                if (this.detailConditionType == EnumDetailCondition.UseMainData)
                {
                    if (row > 0)
                    {
                        string[] detailInfo = this.useMainDataColName.Split('|');
                        if (detailInfo != null)
                        {
                            string[] param = new string[detailInfo.Length];
                            Dictionary<string, Object> map = allMap;
                            if (map == null)
                            {
                                map = this.report.GetDefualtSetting();
                            }

                            for (int i = 0; i < detailInfo.Length; i++)
                            {
                                param[i] = this.IMainReportForm.GetItemString(row, detailInfo[i]);
                                if (map.ContainsKey(detailInfo[i]))
                                {
                                    map.Remove(detailInfo[i]);
                                }
                                map.Add(detailInfo[i], param[i]);
                            }
                            
                            this.queryDetail(param);
                            this.queryDetail(map);
                        }
                    }
                    else
                    {
                        this.IDetailReportForm.Reset();
                    }
                }
            }
        }

        private void ILeftQueryCondition_OnFilterHandler(string filterStr)
        {
            this.IMainReportForm.OnFilter(filterStr);
        }

        #endregion

        #region IReport 成员

        public int Query()
        {
            return this.query();
        }

        public int SetParm(string parm, string reportID)
        {
            resourceID = reportID;
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
             this.IMainReportForm.Export();
            if (this.isDetialDataObject)
            {
                this.IDetailReportForm.Export();
            }
            return 1;
        }

        public int Print()
        {
             this.IMainReportForm.Print();

            if (this.isDetialDataObject)
            {
                this.IDetailReportForm.Print();
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

        public  void SetGenericTypeValue(Object mainObj, Object genericValue, System.Reflection.PropertyInfo p)
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
            DateTimeType  dt= new DateTimeType();
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
                        ReportQueryInfo  r= ICommonReportController.XmlDeSerialization<ReportQueryInfo>(xml);
                        ((ReportQueryInfo)value).List = r.List;
                        ((ReportQueryInfo)value).DataSourceType = r.DataSourceType; ; 
                        sr.Close();
                    }

                    ReportQueryInfo a = ((ReportQueryInfo)value);

                    ICommonReportController.ISettingReportForm ISettingReportForm =
                         ICommonReportController.CreateFactory().CreateInferface<ICommonReportController.ISettingReportForm>(typeof(ucCommonDataWindow), null);
                    if (ISettingReportForm == null)
                    {
                        ISettingReportForm = new frmQueryInfoSetting();
                    }
                    ISettingReportForm.SetValue(a);
                    if (ISettingReportForm is Form)
                    {
                        editorService.ShowDialog(ISettingReportForm as  Form);
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
