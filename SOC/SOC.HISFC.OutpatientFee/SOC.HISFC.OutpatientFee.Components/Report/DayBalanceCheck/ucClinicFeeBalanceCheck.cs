﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Interface;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Enum;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.ControlType;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.UITypeEditor;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Implement;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.DayBalanceCheck
{
    /// <summary>
    /// 门诊收费日结审核
    /// </summary>
    public partial class ucClinicFeeBalanceCheck : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucClinicFeeBalanceCheck()
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
        private string class2Code = "";

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

        #endregion

        #region 接口类

        private IMainReportForm IMainReportForm = null;

        private IMainReportForm IDetailReportForm = null;

        private ILeftQueryCondition ILeftQueryCondition = null;

        private ITopQueryCondition ITopQueryCondition = null;
        #endregion

        #region  私有方法

        private void loadReportFile()
        {
            if (this.report != null)
            {
                if (string.IsNullOrEmpty(this.report.QueryFilePath))
                {
                    report.QueryFilePath = "\\Report\\门诊收费报表\\门诊收费日结查询设置.xml";
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
                        this.report.QueryDataSource = r.QueryDataSource;
                        sr.Close();
                    }
                }
            }

            if (this.showType == EnumShowType.FarPoint)
            {
                if (string.IsNullOrEmpty(this.mainDWDataObject))
                {
                    this.mainDWDataObject = "\\Report\\门诊收费报表\\门诊收费日结.xml";
                }
            }
        }

        private int initInterface()
        {
            this.IMainReportForm = ICommonReportController.CreateFactory().CreateInferface<IMainReportForm>(this.GetType(), null);
            if (this.IMainReportForm == null)
            {
                this.IMainReportForm = new ucMainFpReport();
            }
            IDetailReportForm = ICommonReportController.CreateFactory().CreateInferface<IMainReportForm>(this.GetType(), null);
            if (this.IDetailReportForm == null)
            {
                this.IDetailReportForm = new ucMainFpReport();
            }
            ILeftQueryCondition = ICommonReportController.CreateFactory().CreateInferface<ILeftQueryCondition>(this.GetType(), null);
            if (this.ILeftQueryCondition == null)
            {
                this.ILeftQueryCondition = new ucLeftQueryCondition();
            }
            ITopQueryCondition = ICommonReportController.CreateFactory().CreateInferface<ITopQueryCondition>(this.GetType(), null);
            if (this.ITopQueryCondition == null)
            {
                this.ITopQueryCondition = new ucTopQueryCondition();
            }

            this.IMainReportForm.DataWindowObject = mainDWDataObject;
            this.IMainReportForm.Init(this.report);
            if (this.IMainReportForm is Control)
            {
                this.gbData.Controls.Clear();
                ((Control)this.IMainReportForm).Dock = DockStyle.Fill;
                this.gbData.Controls.Add(this.IMainReportForm as Control);
            }

            List<QueryControl> list = new List<QueryControl>();
            List<QueryControl> listView = new List<QueryControl>();
            if (this.report != null && this.report.List != null)
            {
                //初始化数据
                foreach (QueryControl common in this.report.List)
                {
                    if (common.ControlType is ComboBoxType)
                    {
                        ComboBoxType combo = common.ControlType as ComboBoxType;
                        combo.DataSource = this.queryDataByControlType(combo.DataSourceTypeName, combo.QueryDataSource, combo.DefaultDataSource);
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
                        combo.DataSource = this.queryDataByControlType(combo.DataSourceTypeName, combo.QueryDataSource, combo.DefaultDataSource);
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
                case EnumDataSourceType.Custom:
                default:
                    return new ArrayList(datasource);
                    break;
            }
        }

        private int query()
        {
            //获取参数
            List<Object> list = new List<Object>();
            Dictionary<string, Object> map = new Dictionary<string, object>();
            map["balanceState"] = "0";
            if (this.report != null)
            {
                map = this.report.GetDefualtSetting();
                if (this.report.List != null)
                {
                    foreach (QueryControl common in this.report.List)
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
                if (this.report.QueryDataSource != null)
                {
                    string sql = string.Empty;
                    DataSet ds = new DataSet();
                    foreach (QueryDataSource common in this.report.QueryDataSource)
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

        private int check()
        {
            if (allMap == null || allMap.ContainsKey("balanceNO") == false || string.IsNullOrEmpty(allMap["balanceNO"].ToString()))
            {
                return -1;
            }
            if (CommonController.CreateInstance().MessageBox("确认审核?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //if (Function.GetDayReportBizProcess().ClinicFeeCheck(FS.FrameWork.Management.Connection.Operator, allMap["balanceNO"].ToString()) < 0)
                //{
                CommonController.CreateInstance().MessageBox("门诊收费日结审核失败，原因：" + Function.GetDayReportBizProcess().Err, MessageBoxIcon.Error);
                //    return 1;
                //}
            }

            CommonController.CreateInstance().MessageBox("审核成功！", MessageBoxIcon.Asterisk);

            this.ITopQueryCondition.Init();
            return this.ILeftQueryCondition.Init();
        }

        private int cancelCheck()
        {
            if (allMap == null || allMap.ContainsKey("balanceNO") == false || string.IsNullOrEmpty(allMap["balanceNO"].ToString()))
            {
                return -1;
            }
            if (CommonController.CreateInstance().MessageBox("确认取消审核?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //if (Function.GetDayReportBizProcess().ClinicFeeCancelCheck(FS.FrameWork.Management.Connection.Operator, allMap["balanceNO"].ToString()) < 0)
                //{
                CommonController.CreateInstance().MessageBox("取消门诊收费日结审核失败，原因：" + Function.GetDayReportBizProcess().Err, MessageBoxIcon.Error);
                //    return 1;
                //}
            }

            CommonController.CreateInstance().MessageBox("取消审核成功！", MessageBoxIcon.Asterisk);

            this.ITopQueryCondition.Init();
            return this.ILeftQueryCondition.Init();
        }

        #endregion

        #region 工具栏

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("审核", "日结本次数据", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            this.toolBarService.AddToolButton("取消审核", "取消上次日结数据", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);

            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "审核":
                    this.check();
                    break;
                case "取消审核":
                    this.cancelCheck();
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
            return this.Export();
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            return this.Print();
        }

        protected override int OnPrintPreview(object sender, object neuObject)
        {
            return this.PrintPreview();
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
            if (Function.GetDayReportBizProcess().GetClinicFeeBalanceDate(filterStr, ref dtBeginTime, ref dtEndTime, ref operCode) < 0)
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
            CommonController.CreateInstance().MessageBox("暂不支持导出", MessageBoxIcon.Warning);
            return 1;
        }

        public int Print()
        {
            CommonController.CreateInstance().MessageBox("暂不支持打印", MessageBoxIcon.Warning);
            return 1;
        }

        public int PrintPreview()
        {
            CommonController.CreateInstance().MessageBox("暂不支持打印预览", MessageBoxIcon.Warning);
            return 1;
        }

        #endregion

        #region IPreArrange 成员

        public int PreArrange()
        {
            if (this.DesignMode)
            {
                return 0;
            }
            if (string.IsNullOrEmpty(class2Code) == true)
            {
                class2Code = "0820+26";
            }
            if (class2Code.Split('+').Length < 2)
            {
                class2Code = class2Code + "26";
            }
            string[] prives = class2Code.Split('+');
            string userCode = oper.ID;
            string deptCode = oper.Dept.ID;

            FS.HISFC.BizLogic.Manager.UserPowerDetailManager userPowerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            if (userPowerDetailManager.JudgeUserPriv(userCode, deptCode, prives[0], prives[1]) == false)
            {
                CommonController.CreateInstance().MessageBox("您没有收费日结审核权限!", MessageBoxIcon.Warning);
                return -1;
            }
            return 1;
        }

        #endregion
    }
}