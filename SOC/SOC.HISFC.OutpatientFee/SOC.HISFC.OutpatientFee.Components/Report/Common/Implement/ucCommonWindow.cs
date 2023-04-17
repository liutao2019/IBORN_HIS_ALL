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
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Interface;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Enum;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.UITypeEditor;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.ControlType;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Helper;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Implement
{
    /// <summary>
    /// 报表通用界面
    /// </summary>
    public partial class ucCommonWindow : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IReport
    {
        public ucCommonWindow()
        {
            InitializeComponent();
        }

        private string resourceID = "";
        private Dictionary<string, Object> allMap = null;
        FS.FrameWork.Management.DataBaseManger dbManager = new FS.FrameWork.Management.DataBaseManger();
        private FS.FrameWork.WinForms.Controls.NeuTabControl tabControl = new FS.FrameWork.WinForms.Controls.NeuTabControl();
        private Forms.frmShowInfo frmShowInfo = new FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Implement.Forms.frmShowInfo();

        #region 属性

        #region A.参数设置

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

        private EnumDetailShowType detailShowType = EnumDetailShowType.ShowDialog;
        [Category("C.明细设置"), Description("查询时，明细显示方式；ShowControl：下方控件显示，ShowDialog：弹出窗口显示，ShowTabControl：Tab页显示")]
        public EnumDetailShowType DetailShowType
        {
            get
            {
                return detailShowType;
            }
            set
            {
                detailShowType = value;
            }
        }

        private EnumDetailQueryType detailQueryType = EnumDetailQueryType.MouseRightSelect;
        [Category("C.明细设置"), Description("查询时，查看明细触发方式；MouseClick：鼠标左键，MouseDoubleClick：鼠标左键双击，MouseRightSelect：鼠标右键选择")]
        public EnumDetailQueryType DetailQueryType
        {
            get
            {
                return detailQueryType;
            }
            set
            {
                detailQueryType = value;
            }
        }


        private EnumDetailQueryMode detailQueryMode = EnumDetailQueryMode.RowMode;
        [Category("C.明细设置"), Description("查询时，查看明细触发方式；RowMode：行触发，CellMode：单元格触发，RangeMode：区域触发")]
        public EnumDetailQueryMode DetailQueryMode
        {
            get
            {
                return detailQueryMode;
            }
            set
            {
                detailQueryMode = value;
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
                        this.report = ICommonReportController.XmlDeSerialization<ReportQueryInfo>(xml);
                        
                        sr.Close();
                    }
                }
            }

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

        private int initInterface()
        {
            this.IMainReportForm = ICommonReportController.CreateFactory().CreateInferface<IMainReportForm>(typeof(ucCommonWindow), null);
            if (this.IMainReportForm == null)
            {
                this.IMainReportForm = new ucMainFpReport();
            }
            IDetailReportForm = ICommonReportController.CreateFactory().CreateInferface<IMainReportForm>(typeof(ucCommonWindow), null);
            if (this.IDetailReportForm == null)
            {
                this.IDetailReportForm = new ucMainFpReport();
            }
            ILeftQueryCondition = ICommonReportController.CreateFactory().CreateInferface<ILeftQueryCondition>(typeof(ucCommonWindow), null);
            if (this.ILeftQueryCondition == null)
            {
                this.ILeftQueryCondition = new ucLeftQueryCondition();
            }
            ITopQueryCondition = ICommonReportController.CreateFactory().CreateInferface<ITopQueryCondition>(typeof(ucCommonWindow), null);
            if (this.ITopQueryCondition == null)
            {
                this.ITopQueryCondition = new ucTopQueryCondition();
            }

            this.IMainReportForm.DataWindowObject = mainDWDataObject;
            this.IMainReportForm.Init(this.report);

            if (this.detailQueryMode == EnumDetailQueryMode.RowMode)
            {
                this.IMainReportForm.OnSelectRowHandler += new ICommonReportController.SelectRowHanlder(IMainReportForm_OnSelectRowHandler);
            }
            else if (this.detailQueryMode == EnumDetailQueryMode.CellMode)
            {
                if (this.detailQueryType == EnumDetailQueryType.MouseClick)
                {
                    this.IMainReportForm.OnCellClickHandler += new ICommonReportController.SelectCellHanlder(IMainReportForm_OnCellClickHandler);
                }
                else if (this.detailQueryType == EnumDetailQueryType.MouseDoubleClick)
                {
                    this.IMainReportForm.OnDoubleCellClickHandler += new ICommonReportController.SelectCellHanlder(IMainReportForm_OnDoubleCellClickHandler);
                }
            }

            if (this.IMainReportForm is Control)
            {
                this.gbData.Controls.Clear();
                ((Control)this.IMainReportForm).Dock = DockStyle.Fill;
                this.gbData.Controls.Add(this.IMainReportForm as Control);
            }

            if (this.isDetialDataObject)
            {
                this.IDetailReportForm.DataWindowObject = this.detailDWDataObject;
                this.IDetailReportForm.Init(this.report);
                if (this.IDetailReportForm is Control)
                {
                    this.gbDataDetail.Controls.Clear();
                    ((Control)this.IDetailReportForm).Dock = DockStyle.Fill;
                    this.gbDataDetail.Controls.Add(this.IDetailReportForm as Control);
                }
            }

            List<QueryControl> list = new List<QueryControl>();
            List<QueryControl> listView = new List<QueryControl>();
            if (this.report != null && this.report.List != null)
            {
                Dictionary<string, Object> map = this.report.GetDefualtSetting();

                //初始化数据
                foreach (QueryControl common in this.report.List)
                {
                    if (common.ControlType is ComboBoxType)
                    {
                        ComboBoxType combo = common.ControlType as ComboBoxType;

                        object c = this.queryDataByControlType(combo.DataSourceTypeName, combo.QueryDataSource, combo.DefaultDataSource, map);
                        if (c is ArrayList)
                        {
                            combo.DataSource = c as ArrayList;
                        }
                        else if (c is List<FS.FrameWork.Models.NeuObject>)
                        {
                            combo.DataSource = new ArrayList(c as List<FS.FrameWork.Models.NeuObject>);
                        }

                        if (combo.IsAddAll)
                        {
                            combo.DataSource.Insert(0, combo.AllValue);
                        }
                        common.ControlType = combo;
                        list.Add(common);
                    }
                    else if (common.ControlType is TextBoxType)
                    {
                        TextBoxType txt = common.ControlType as TextBoxType;
                        object objDateSoruce = this.queryDataByControlType(txt.DataSourceTypeName, txt.QueryDataSource, txt.DefaultDataSource, map);
                        if (objDateSoruce is ArrayList)
                        {
                            ArrayList listDateSoruce = objDateSoruce as ArrayList;
                            if (listDateSoruce.Count > 0)
                            {
                                txt.DataSource = ((FS.HISFC.Models.Base.Spell)listDateSoruce[0]).ID;
                            }
                        }
                        else
                        {
                            txt.DataSource = objDateSoruce as string;
                        }
                        common.ControlType = txt;
                        list.Add(common);
                    }
                    else if (common.ControlType is TextBoxType
                        || common.ControlType is DateTimeType
                        || common.ControlType is FilterTextBox)
                    {
                        DateTimeType txt = common.ControlType as DateTimeType;
                        txt.DataSource = this.queryDataByControlType(txt.DataSourceTypeName, txt.QueryDataSource, txt.DefaultDataSource, map) as string;
                        common.ControlType = txt;
                        list.Add(common);
                    }
                    else if (common.ControlType is CustomControl
                        || common.ControlType is FilterTextBox)
                    {
                        list.Add(common);
                    }
                    else if (common.ControlType is TreeViewType)
                    {
                        TreeViewType combo = common.ControlType as TreeViewType;
                        combo.DataSource = this.queryDataByControlType(combo.DataSourceTypeName, combo.QueryDataSource,new ArrayList( combo.DefaultDataSource), map) as ArrayList;
                        common.ControlType = combo;
                        listView.Add(common);
                    }
                    else if (common.ControlType is GroupTreeViewType)
                    {
                        GroupTreeViewType combo = common.ControlType as GroupTreeViewType;
                        combo.DataSource = this.queryDataByControlType(combo.DataSourceTypeName, combo.QueryDataSource, null, map) as Dictionary<FS.FrameWork.Models.NeuObject, ArrayList>;
                        common.ControlType = combo;
                        listView.Add(common);
                    }
                    else
                    {
                        list.Add(common);
                    }
                }

            }

            this.ITopQueryCondition.Init();
            this.ITopQueryCondition.OnFilterHandler += new ICommonReportController.FilterHandler(ITopQueryCondition_OnFilterHandler);
            this.ITopQueryCondition.AddControls(list);
            if (this.ITopQueryCondition is Control)
            {
                this.gbQueryInfo.Controls.Clear();
                ((Control)this.ITopQueryCondition).Dock = DockStyle.Fill;
                this.gbQueryInfo.Controls.Add(this.ITopQueryCondition as Control);
            }

            this.ILeftQueryCondition.Init();
            this.ILeftQueryCondition.OnFilterHandler += new ICommonReportController.FilterHandler(ILeftQueryCondition_OnFilterHandler);
            this.ILeftQueryCondition.AddControls(listView);
            if (this.ILeftQueryCondition is Control)
            {
                this.gbTreeView.Controls.Clear();
                ((Control)this.ILeftQueryCondition).Dock = DockStyle.Fill;
                this.gbTreeView.Controls.Add(this.ILeftQueryCondition as Control);
            }
            return 1;
        }

        private Object queryDataByControlType(string type, EnumDataSourceType datasoureType, object datasource, Dictionary<string, Object> map)
        {
            ArrayList al = new ArrayList();
            switch (datasoureType)
            {
                case EnumDataSourceType.Sql://自定义SQL
                    #region Sql
                    type = Function.ReplaceValues(type, map);
                    if (dbManager.ExecQuery(type) == -1)
                    {
                        MessageBox.Show(this, "执行自定义查询SQL出错！" + dbManager.Err, "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    while (dbManager.Reader.Read())
                    {
                        FS.HISFC.Models.Base.Spell obj = new FS.HISFC.Models.Base.Spell();
                        obj.ID = dbManager.Reader[0].ToString();
                        if (dbManager.Reader.FieldCount > 1)
                        {
                            obj.Name = dbManager.Reader[1].ToString();
                        }
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
                    #endregion
                case EnumDataSourceType.DepartmentType://科室
                    #region Department
                    if (type == "ALL")
                    {
                        return CommonController.CreateInstance().QueryDepartment();
                    }
                    else
                    {
                        FS.HISFC.Models.Base.EnumDepartmentType deptType = FS.FrameWork.Function.NConvert.ToEnum<FS.HISFC.Models.Base.EnumDepartmentType>(type, FS.HISFC.Models.Base.EnumDepartmentType.C);
                        return CommonController.CreateInstance().QueryDepartment(deptType);
                    }
                    #endregion
                case EnumDataSourceType.Dictionary://常数
                    return CommonController.CreateInstance().QueryConstant(type);
                case EnumDataSourceType.EmployeeType://人员
                    #region Employee
                    if (type == "ALL")
                    {
                        return CommonController.CreateInstance().QueryEmployee();
                    }
                    else
                    {
                        FS.HISFC.Models.Base.EnumEmployeeType emplType = FS.FrameWork.Function.NConvert.ToEnum<FS.HISFC.Models.Base.EnumEmployeeType>(type, FS.HISFC.Models.Base.EnumEmployeeType.D);
                        return CommonController.CreateInstance().QueryEmployee(emplType);
                    }
                    #endregion
                case EnumDataSourceType.DataSource:
                    #region DataSource
                    this.queryDataTable(EnumSqlType.ConditionUsing, ref map);
                    if (Function.HasReplaceableValues(type))
                    {
                        return Function.ReplaceValues(type, map);
                    }
                    else if (map.ContainsKey(type))
                    {
                        if (map[type] is KeyValuePair<DataTable, QueryDataSource>)
                        {
                            foreach (DataRow dr in ((KeyValuePair<DataTable, QueryDataSource>)map[type]).Key.Rows)
                            {
                                FS.HISFC.Models.Base.Spell obj = new FS.HISFC.Models.Base.Spell();
                                obj.ID = dr[0].ToString();
                                obj.Name = dr[1].ToString();
                                if (dr.ItemArray.Length > 2)
                                {
                                    obj.Memo = dr[2].ToString();
                                }
                                if (dr.ItemArray.Length > 3)
                                {
                                    obj.SpellCode = dr[3].ToString();
                                }
                                if (dr.ItemArray.Length > 4)
                                {
                                    obj.WBCode = dr[4].ToString();
                                }
                                if (dr.ItemArray.Length > 5)
                                {
                                    obj.UserCode = dr[5].ToString();
                                }
                                al.Add(obj);
                            }
                        }
                        return al;
                    }


                    #endregion
                    break;
                case EnumDataSourceType.DepartStat://科室结构
                    return getDepartStatDictionary(type);
                    break;
                case EnumDataSourceType.Custom:
                default:
                    return datasource;
            }

            return null;
        }

        private int query()
        {
            this.setDetailShowType();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询，请稍后...");
            Application.DoEvents();

                //获取参数
                Dictionary<string, Object> map = null;
                this.queryDataTable(EnumSqlType.TableGroupUsing, ref map);
                //报表分组
                if (this.report != null && this.report.TableGroup != null && this.report.TableGroup.QueryDataSource != null && map.ContainsKey(this.report.TableGroup.QueryDataSource.Name))
                {
                    List<Dictionary<string, Object>> list = new List<Dictionary<string, object>>();
                    this.queryDataTable(EnumSqlType.MainReportUsing, ref map);
                    allMap = new Dictionary<string, object>(map);
                    KeyValuePair<DataTable, QueryDataSource> group = (KeyValuePair<DataTable, QueryDataSource>)map[this.report.TableGroup.QueryDataSource.Name];
                    foreach (DataRow drRowGroup in group.Key.Rows)
                    {
                        map = new Dictionary<string, object>(allMap);
                        foreach (KeyValuePair<string, object> keyValue in allMap)
                        {
                            if (keyValue.Value is KeyValuePair<DataTable, QueryDataSource>)//对DataTable进行转换
                            {
                                KeyValuePair<DataTable, QueryDataSource> keyDt = (KeyValuePair<DataTable, QueryDataSource>)keyValue.Value;
                                keyDt.Key.DefaultView.RowFilter = string.Format(this.report.TableGroup.GroupCondition, drRowGroup[0].ToString());

                                QueryDataSource common = keyDt.Value;
                                DataTable dt = keyDt.Key.DefaultView.ToTable();
                                Object[,] crossSource = null;
                                #region AddMapData
                                if (common.AddMapData)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        //添加数据
                                        for (int j = 0; j < dt.Columns.Count; j++)
                                        {
                                            map[common.Name + ".Rows[" + i + "][" + j + "]"] = dt.Rows[i][j];
                                            map[common.Name + ".Rows[" + i + "][" + dt.Columns[j].ColumnName + "]"] = dt.Rows[i][j];
                                        }
                                    }
                                }
                                #endregion
                                #region AddMapRow
                                if (common.AddMapRow)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        map[common.Name + ".Rows[" + i + "]"] = dt.Rows[i];
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
                                    DataRow drColumnGroup = dtColumns.NewRow();
                                    DataRow dr = dtColumns.NewRow();
                                    for (int i = 0; i < dt.Columns.Count; i++)
                                    {
                                        drColumnGroup[i] = dt.Columns[i].Caption ?? dt.Columns[i].ColumnName;
                                        dr[i] = dt.Columns[i].ColumnName;
                                        map[common.Name + ".Columns[" + i + "]"] = dt.DefaultView.ToTable(false, dt.Columns[i].ColumnName);
                                    }
                                    if (string.IsNullOrEmpty(common.CrossCombinColumns) == false)
                                    {
                                        dtColumns.Rows.Add(drColumnGroup);
                                    }
                                    dtColumns.Rows.Add(dr);
                                    map[common.Name + ".Columns"] = dtColumns;
                                }
                                #endregion
                                #region AddMapSourceData
                                if (common.AddMapSourceData)
                                {
                                    if (crossSource == null)
                                    {
                                        crossSource = new object[dt.Rows.Count, dt.Columns.Count];
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            //添加数据
                                            for (int j = 0; j < dt.Columns.Count; j++)
                                            {
                                                crossSource[i, j] = new DataRow[] { dt.Rows[i] };
                                            }
                                        }
                                    }

                                    map[common.Name + "AddMapSourceData"] = crossSource;
                                }
                                #endregion
                                map[keyValue.Key] = new KeyValuePair<DataTable, QueryDataSource>(dt, keyDt.Value);
                            }
                        }
                        list.Add(map);
                    }
                    this.IMainReportForm.Retrieve(list.ToArray());
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }
                else
                {
                    this.queryDataTable(EnumSqlType.MainReportUsing, ref map);
                    allMap = map;
                    int ret = this.IMainReportForm.Retrieve(map);
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    if (this.isDetialDataObject && this.detailQueryMode == EnumDetailQueryMode.RowMode)
                    {
                        if (this.detailConditionType == EnumDetailCondition.UseMainCondition)
                        {
                            return this.queryDetail(map);
                        }
                        else if (this.detailConditionType == EnumDetailCondition.UseMainData)
                        {
                            this.IMainReportForm_OnSelectRowHandler(1);
                        }
                    }
                }

            return 1;
        }

        private int queryDetail(Dictionary<string, Object> map)
        {
            this.queryDataTable(EnumSqlType.DetailReportUsing, ref map);
            this.IDetailReportForm.Retrieve(map);
            //显示明细
            this.showDetail();
            return 1;
        }

        /// <summary>
        /// 获取数据内容
        /// </summary>
        /// <param name="sqlType"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        private int queryDataTable(EnumSqlType sqlType, ref Dictionary<string, Object> map)
        {
            if (map == null || map.Count == 0)
            {
                map = this.report.GetDefualtSetting();
            }

            List<Dictionary<string, Object>> list = new List<Dictionary<string, Object>>();

            if (this.report != null)
            {
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
                        else
                        {
                            if (common.ControlType is TreeViewType || common.ControlType is FilterTreeView || common.ControlType is GroupTreeViewType)
                            {

                                object text = this.ILeftQueryCondition.GetTexts(common);
                                map[common.Name + ".Texts"] = text;
                                object value = this.ILeftQueryCondition.GetValues(common);
                                map[common.Name + ".Values"] = value;

                                 value = this.ILeftQueryCondition.GetValue(common);
                                if (common.ControlType.IsLike)
                                {
                                    value = string.Format(common.ControlType.LikeStr, value);
                                }

                                 text = this.ILeftQueryCondition.GetText(common);
                                map[common.Name + ".Text"] = text;
                                map[common.Name] = value;
                                map[common.Name + ".Value"] = value;

                            }
                            else
                            {
                                object value = this.ITopQueryCondition.GetValue(common);
                                if (common.ControlType.IsLike)
                                {
                                    value = string.Format(common.ControlType.LikeStr, value);
                                }

                                object text = this.ITopQueryCondition.GetText(common);

                                map[common.Name + ".Text"] = text;
                                map[common.Name] = value;
                                map[common.Name + ".Value"] = value;
                            }
                        }
                    }
                }

                if (this.report.QueryDataSource != null)
                {
                    string sql = string.Empty;
                    DataSet ds = new DataSet();

                    foreach (QueryDataSource common in this.report.QueryDataSource)
                    {
                        //不是主报表的不需要
                        if (common.SqlType != sqlType)
                        {
                            continue;
                        }

                        sql = Function.ReplaceValues(common.Sql, map);

                        if (Function.HasSelect(sql))
                        {
                            //使用参数
                            if (dbManager.ExecQuery(sql, ref ds) == -1 || ds == null || ds.Tables.Count == 0)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                MessageBox.Show(this, "执行数据源" + common.Name + "的sql出错！" + dbManager.Err, "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return -1;
                            }
                            DataTable dt = ds.Tables[0];

                            Object[,] crossSource = null;
                            //如果是交叉报表
                            if (common.IsCross)
                            {
                                dt = this.queryCross(ds.Tables[0], common, map, ref crossSource);
                            }
                            else
                            {
                                //列分组
                                this.columnGroup(dt, common);
                                //行分组
                                //this.rowGroup(dt, common);
                            }

                            #region AddMapData
                            if (common.AddMapData)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    //添加数据
                                    for (int j = 0; j < dt.Columns.Count; j++)
                                    {
                                        map[common.Name + ".Rows[" + i + "][" + j + "]"] = dt.Rows[i][j];
                                        map[common.Name + ".Rows[" + i + "][" + dt.Columns[j].ColumnName + "]"] = dt.Rows[i][j];
                                    }
                                }
                            }
                            #endregion
                            #region AddMapRow
                            if (common.AddMapRow)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    map[common.Name + ".Rows[" + i + "]"] = dt.Rows[i];
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
                                DataRow drGroup = dtColumns.NewRow();
                                DataRow dr = dtColumns.NewRow();
                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    drGroup[i] = dt.Columns[i].Caption ?? dt.Columns[i].ColumnName;
                                    dr[i] = dt.Columns[i].ColumnName;
                                    map[common.Name + ".Columns[" + i + "]"] = dt.DefaultView.ToTable(false, dt.Columns[i].ColumnName);
                                }
                                if (string.IsNullOrEmpty(common.CrossCombinColumns) == false)
                                {
                                    dtColumns.Rows.Add(drGroup);
                                }
                                dtColumns.Rows.Add(dr);
                                map[common.Name + ".Columns"] = dtColumns;
                            }
                            #endregion
                            #region AddMapSourceData
                            if (common.AddMapSourceData)
                            {
                                if (crossSource == null)
                                {
                                    crossSource = new object[dt.Rows.Count, dt.Columns.Count];
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        //添加数据
                                        for (int j = 0; j < dt.Columns.Count; j++)
                                        {
                                            crossSource[i, j] = new DataRow[] { dt.Rows[i] };
                                        }
                                    }
                                }

                                map[common.Name + "AddMapSourceData"] = crossSource;
                            }
                            #endregion

                            //传递参数变化
                            KeyValuePair<DataTable, QueryDataSource> keyValue = new KeyValuePair<DataTable, QueryDataSource>(dt, common);
                            map[common.Name] = keyValue;

                        }
                        else
                        {
                            map[common.Name] = sql;
                        }
                    }
                }
            }

            return 1;
        }

        /// <summary>
        /// 处理交叉报表
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        private DataTable queryCross(DataTable dt, QueryDataSource common, Dictionary<string, Object> map, ref Object[,] crossSource)
        {

            DataSetHelper dsh = new DataSetHelper();
            //先取交叉报表的行
            string[] crossRows = common.CrossRows.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            if (crossRows.Length == 0)
            {
                throw new Exception("交叉报表没有设置交叉行，请设置！");
            }

            ///生成行数据集，不包括合计行
            DataTable dtCrossRows = dsh.SelectDistinctByIndexs("dtCrossRows", dt, crossRows);

            string[] crossColumns = common.CrossColumns.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (crossColumns.Length == 0)
            {
                throw new Exception("交叉报表没有设置交叉列，请设置！");
            }

            ///生成列数据集，不包括合计列
            DataTable dtCrossColumns = dsh.SelectDistinctByIndexs("dtCrossColumns", dt, crossColumns);

            //形成新的数据列表
            string[] crossValues = common.CrossValues.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (crossValues.Length == 0)
            {
                throw new Exception("交叉报表没有设置交叉值，请设置！");
            }
            DataTable dtCrossValues = new DataTable();
            dtCrossValues = new DataTable();

            //合并列
            string[] crossCombinColumns = common.CrossCombinColumns.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            string[] crossGroupColumns = common.CrossGroupColumns.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

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

                            //{AB4140A5-54B5-4dae-9FE4-E70B4A50A27D}
                            #region 增加caption的赋值
                            //dtCrossValues.Columns.Add(new DataColumn(s,dt.Columns[s].DataType));
                            dtCrossValues.Columns[dr[dc].ToString() + s].Caption = dr[dc].ToString();
                            #endregion

                        }
                    }
                }
            }

            #endregion

            //列分组
            this.columnGroup(dtCrossValues, common);

            //记录源数据内容
            crossSource = new object[1000, dtCrossValues.Columns.Count];

            //形成新的数据内容
            StringBuilder currExp = null;
            StringBuilder currColumnExp = null;
            //string sampleValues = string.Empty;
            //string selectSampleValues = "1=1";
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

                if (crossGroupColumns.Length > 0)
                {
                    //string currentSampleValues = string.Empty;
                    //string currentSelectSampleValues = "1=1";
                    //foreach (string crossGroupColumn in crossGroupColumns)
                    //{
                    //    currentSampleValues += drValues[crossGroupColumn].ToString();
                    //    currentSelectSampleValues += " AND " + crossGroupColumn + " = '" + drValues[crossGroupColumn].ToString() + "'";
                    //}
                    //if (string.Empty.Equals(sampleValues))
                    //{
                    //    sampleValues = currentSampleValues;
                    //}

                    //添加小计
                    //判断是否与上一个相等
                    //if (!sampleValues.Equals(currentSampleValues))
                    //{
                    //    //添加合计
                    //    dtCrossValues.AcceptChanges();
                    //    DataRow drSumRow = dtCrossValues.NewRow();
                    //    drSumRow[crossGroupColumns[0]] = "小计：";

                    //    foreach (DataColumn dc in dtCrossValues.Columns)
                    //    {
                    //        if (dc.DataType.IsValueType)
                    //        {
                    //            string columnName = "[" + dc.ColumnName + "]";
                    //            drSumRow[dc.ColumnName] = dtCrossValues.Compute("sum(" + columnName + ")", selectSampleValues);
                    //        }
                    //    }
                    //    dtCrossValues.Rows.Add(drSumRow);
                    //    //添加空行
                    //    dtCrossValues.Rows.Add(dtCrossValues.NewRow());
                    //    sampleValues = currentSampleValues;
                    //    selectSampleValues = currentSelectSampleValues;
                    //}
                }

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
                                //添加元数据
                                crossSource[dtCrossValues.Rows.Count, dtCrossValues.Columns[drColumn[dcCrossColumn].ToString()].Ordinal] = arryDr;
                            }
                            else
                            {

                                drValues[drColumn[dcCrossColumn].ToString() + s] = currVal[currValIdx];
                                //添加元数据
                                crossSource[dtCrossValues.Rows.Count, dtCrossValues.Columns[drColumn[dcCrossColumn].ToString() + s].Ordinal] = arryDr;
                            }
                        }
                        #endregion
                    }

                    #endregion

                }


                

                dtCrossValues.Rows.Add(drValues);

            }

            if (dtCrossValues.Rows.Count > 0)
            {
                //DataRow drValues = dtCrossValues.Rows[dtCrossValues.Rows.Count - 1];
                //if (crossGroupColumns.Length > 0)
                //{
                //    //添加小计
                //    //判断是否与上一个相等

                //    //添加合计
                //    dtCrossValues.AcceptChanges();
                //    DataRow drSumRow = dtCrossValues.NewRow();
                //    drSumRow[crossGroupColumns[0]] = "小计：";

                //    foreach (DataColumn dc in dtCrossValues.Columns)
                //    {
                //        if (dc.DataType.IsValueType && dc.DataType == typeof(decimal))
                //        {
                //            string columnName = "[" + dc.ColumnName + "]";
                //            drSumRow[dc.ColumnName] = dtCrossValues.Compute("sum(" + columnName + ")", selectSampleValues);
                //        }
                //    }
                //    dtCrossValues.Rows.Add(drSumRow);
                //    //添加空行
                //    dtCrossValues.Rows.Add(dtCrossValues.NewRow());
                //}
            }

            if (common.IsSumRow)
            {
                //添加列合计
                //DataRow drNewRow = dtCrossValues.NewRow();
                //drNewRow[0] = "合计：";
                //foreach (DataColumn dc in dtCrossValues.Columns)
                //{
                //    if (dc.DataType.IsValueType)
                //    {
                //        if (crossGroupColumns.Length > 0)
                //        {
                //            string columnName = "[" + dc.ColumnName + "]";
                //            drNewRow[dc.ColumnName] = dtCrossValues.Compute("sum(" + columnName + ")", crossGroupColumns[0] + "= '小计：'");
                //        }
                //        else
                //        {
                //            string columnName = "[" + dc.ColumnName + "]";
                //            drNewRow[dc.ColumnName] = dtCrossValues.Compute("sum(" + columnName + ")", "");
                //        }
                //    }
                //}
                //dtCrossValues.Rows.Add(drNewRow);
            }

            return dtCrossValues;
        }

        /// <summary>
        /// 列分组+行合计
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="common"></param>
        private void columnGroup(DataTable dt, QueryDataSource common)
        {
            //添加“列分组”
            #region 列分组
            if (string.IsNullOrEmpty(common.CrossCombinColumns) == false)
            {
                string[] columnGroups = common.CrossCombinColumns.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string columnGroup in columnGroups)
                {
                    string[] groupInfo = columnGroup.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    //groupInfo[0]为合并后名称
                    //groupInfo[1]为需要合并的列

                    if (groupInfo.Length > 1)
                    {
                        string expression = "0";
                        //合并列
                        foreach (string groupColumn in groupInfo[1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            if (dt.Columns.Contains(groupColumn))
                            {
                                //调整位置
                                dt.Columns[groupColumn].SetOrdinal(dt.Columns.Count - 1);
                                dt.Columns[groupColumn].Caption = groupInfo[0];
                                expression += "+([" + groupColumn + "])";
                            }
                        }

                        //添加行合计
                        if (common.SumRows.Contains(groupInfo[0]))
                        {
                            DataColumn dc = new DataColumn(groupInfo[0] + "合计");
                            dc.Caption = groupInfo[0];
                            dc.DataType = dt.Columns[dt.Columns.Count - 1].DataType;
                            //合计数据
                            dc.Expression = expression;
                            dt.Columns.Add(dc);
                        }
                    }
                }
            }
            #endregion

            #region 列合计
            //添加列合计
            if (string.IsNullOrEmpty(common.SumRows) == false)
            {
                string[] columnSums = common.SumRows.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string columnGroup in columnSums)
                {
                    string[] sumInfo = columnGroup.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    //groupInfo[0]为合并后名称
                    //groupInfo[1]为需要合并的列

                    if (sumInfo.Length > 1)
                    {
                        string expression = "0";
                        Type type = typeof(Decimal);
                        //合并列
                        foreach (string sumColumn in sumInfo[1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            if (dt.Columns.Contains(sumColumn))
                            {
                                type = dt.Columns[sumColumn].DataType;
                                expression += "+(" + sumColumn + ")";
                            }
                        }

                        DataColumn dc = new DataColumn(sumInfo[0]);
                        dc.Caption = sumInfo[0];
                        dc.DataType = type;
                        //合计数据
                        dc.Expression = expression;
                        dt.Columns.Add(dc);
                    }
                }
            }
            #endregion

        }

        private void setDetailShowType()
        {
            //去掉所有的控件
            if (isDetialDataObject == false)
            {
                return;
            }

            if (this.detailShowType == EnumDetailShowType.ShowDialog)
            {
                if (!frmShowInfo.Controls.Contains(this.gbDataDetail))
                {
                    this.gbDataDetail.Dock = DockStyle.Fill;
                    frmShowInfo.Controls.Add(this.gbDataDetail);
                }
                this.gbDataDetail.Visible = true;
            }
            else if (this.detailShowType == EnumDetailShowType.ShowControl)
            {
                if (this.plReport.Controls.ContainsKey(this.gbDataDetail.Name) == false)
                {
                    this.gbDataDetail.Dock = DockStyle.Bottom;
                    this.plReport.Controls.Add(this.gbDataDetail);
                }
                this.gbDataDetail.Visible = true;

                if (this.plReport.Controls.ContainsKey(this.gbData.Name) == false)
                {
                    this.plReport.Controls.Add(this.gbData);
                }
            }
            else if (this.detailShowType == EnumDetailShowType.ShowTabControl)
            {
                if (!this.plReport.Controls.ContainsKey(this.tabControl.Name))
                {
                    this.tabControl.Dock = DockStyle.Fill;
                    this.tabControl.TabPages.Clear();
                    TabPage pMain = new TabPage("主报表");
                    pMain.Controls.Add(this.gbData);

                    this.tabControl.TabPages.Add(pMain);
                    TabPage pDetail = new TabPage("明细报表");
                    this.gbDataDetail.Dock = DockStyle.Fill;
                    pDetail.Controls.Add(this.gbDataDetail);

                    this.tabControl.TabPages.Add(pDetail);
                    this.plReport.Controls.Add(this.tabControl);
                    this.tabControl.SelectedIndex = 0;
                }
                this.gbDataDetail.Visible = true;
            }
            else if (this.detailShowType == EnumDetailShowType.LeftAndRight)
            {
                this.gbDataDetail.Visible = true;
                this.gbTreeView.Visible = true;
                this.plReport.Controls.Remove(this.gbData);
                this.pLeft.Controls.Remove(this.gbTreeView);
                this.plReport.Controls.Remove(this.neuSplitter2);
                this.gbDataDetail.Dock = DockStyle.Fill;
                this.pLeft.Controls.Add(this.gbData);
            }
        }

        private void showDetail()
        {
            if (this.detailShowType == EnumDetailShowType.ShowDialog)
            {
                if (frmShowInfo.Visible == false)
                {
                    frmShowInfo.Focus();
                    frmShowInfo.Show(this);
                }
            }
            else if (this.detailShowType == EnumDetailShowType.ShowControl)
            {
                this.gbDataDetail.Visible = true;
            }
            else if (this.detailShowType == EnumDetailShowType.ShowTabControl)
            {
                this.tabControl.SelectedIndex = 1;
            }
        }

        private Dictionary<FS.FrameWork.Models.NeuObject, ArrayList> getDepartStatDictionary(string type)
        {
            string secondPrivCode = string.Empty;
            string threePrivCode=string.Empty;
            if (type.Contains("+"))
            {
                secondPrivCode = type.Split(new char[]{'+'}, StringSplitOptions.RemoveEmptyEntries)[0];
                threePrivCode = type.Split(new char[]{'+'}, StringSplitOptions.RemoveEmptyEntries)[1];
            }
            else
            {
                secondPrivCode = type;
            }
            string sql = @"select e.empl_code,e.empl_name,e.spell_code,e.wb_code,
                                                   e.user_code,e.empl_type,e.valid_state,d.dept_code,d.dept_name
                                    from com_employee e,com_priv_user u,com_department d
                                    where  e.dept_code=d.dept_code
                                    and u.user_code='{0}'
                                    and u.class2_code='{1}'
                                    and  decode(u.class3_code,'{2}',u.grant_dept,e.empl_code) in (e.dept_code)
                                    union
                                    select e.empl_code,e.empl_name,e.spell_code,e.wb_code,e.user_code,e.empl_type,e.valid_state,d.dept_code,d.dept_name
                                     from com_employee e,com_department d
                                    where e.dept_code=d.dept_code
                                    and e.empl_code='{0}'";

            if (dbManager.ExecQuery(sql, dbManager.Operator.ID,secondPrivCode,threePrivCode) == -1)
            {
                MessageBox.Show(this, "执行权限查询SQL出错！" + dbManager.Err, "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            Dictionary<FS.FrameWork.Models.NeuObject, ArrayList> list = new Dictionary<FS.FrameWork.Models.NeuObject, ArrayList>();
            Dictionary<string, FS.FrameWork.Models.NeuObject> listKey = new Dictionary<string, FS.FrameWork.Models.NeuObject>();
            Dictionary<string, ArrayList> listValue = new Dictionary<string, ArrayList>();

            try
            {
                while (dbManager.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = dbManager.Reader["DEPT_CODE"].ToString();
                    obj.Name = dbManager.Reader["DEPT_NAME"].ToString();

                    FS.HISFC.Models.Base.Spell s = new FS.HISFC.Models.Base.Spell();
                    s.ID = dbManager.Reader["EMPL_CODE"].ToString();
                    s.Name = dbManager.Reader["EMPL_NAME"].ToString();
                    s.SpellCode = dbManager.Reader["SPELL_CODE"].ToString();
                    s.WBCode = dbManager.Reader["WB_CODE"].ToString();
                    s.UserCode = dbManager.Reader["USER_CODE"].ToString();

                    if (listKey.ContainsKey(obj.ID))
                    {
                        listValue[obj.ID].Add(s);
                    }
                    else
                    {
                        listKey[obj.ID] = obj;

                        listValue[obj.ID] = new ArrayList();
                        listValue[obj.ID].Add(s);
                    }
                }

                foreach (KeyValuePair<string, FS.FrameWork.Models.NeuObject> keyValue in listKey)
                {
                    list[keyValue.Value] = listValue[keyValue.Key];
                }

                return list;
            }
            catch (Exception e)
            {
                MessageBox.Show(this, "执行权限查询SQL出错！" + e.Message, "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            finally
            {
                if (dbManager.Reader != null && dbManager.Reader.IsClosed == false)
                {
                    dbManager.Reader.Close();
                }
            }
        }

        #endregion

        #region 工具栏

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();


        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            if (this.toolBarService.GetToolButton("明细导出") == null)
            {
                this.toolBarService.AddToolButton("全部打印", "打印所有页", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
                this.toolBarService.AddToolButton("选页打印", "打印指定页", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
                this.toolBarService.AddToolButton("明细导出", "导出明细信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D导出, true, false, null);
                this.toolBarService.AddToolButton("明细打印", "打印明细信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
                this.toolBarService.AddToolButton("明细打印预览", "预览明细信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Y预览, true, false, null);
            }

            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "明细打印":
                    if (this.isDetialDataObject)
                    {
                        this.IDetailReportForm.Print(this.report.PrintInfo);
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
                        this.IDetailReportForm.PrintPreview(true, this.report.PrintInfo);
                    }
                    break;
                case "全部打印":
                    if (CommonController.Instance.MessageBox(this, "确认全部打印？", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.IMainReportForm.PrintAll(this.report.PrintInfo);
                    }
                    break;
                case "选页打印":
                    PrintInfo p = new PrintInfo();
                    p.SelectPage = true;
                    p.PaperSize = this.report.PrintInfo.PaperSize;

                    this.IMainReportForm.Print(p);

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

            //明细设置
            this.setDetailShowType();
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            return this.Query();
        }

        public override int Export(object sender, object neuObject)
        {
            this.Export();
            //if (this.isDetialDataObject)
            //{
            //    this.IDetailReportForm.Export();
            //}
            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();
            //if (this.isDetialDataObject)
            //{
            //    this.IDetailReportForm.Print();
            //}
            return 1;
        }

        protected override int OnPrintPreview(object sender, object neuObject)
        {
            this.PrintPreview();
            //if (this.isDetialDataObject)
            //{
            //    this.IDetailReportForm.PrintPreview(true);
            //}
            return 1;
        }

        private void ITopQueryCondition_OnFilterHandler(string filterStr)
        {
            //没有过滤了
            //直接查询
            this.query();

            //this.IMainReportForm.OnFilter(filterStr);
        }

        private void IMainReportForm_OnSelectRowHandler(int row)
        {
            if (this.isDetialDataObject)
            {
                if (this.detailConditionType == EnumDetailCondition.UseMainData)
                {
                    if (row > 0)
                    {
                        string[] detailInfo = this.useMainDataColName.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
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
                                map[detailInfo[i]] = param[i];
                            }

                            this.queryDetail(map);
                        }
                    }
                }
            }
        }

        void IMainReportForm_OnDoubleCellClickHandler(Object values)
        {
            if (isDetialDataObject && this.detailQueryType == EnumDetailQueryType.MouseDoubleClick)
            {
                if (this.detailConditionType == EnumDetailCondition.UseMainData)
                {
                    if (values is DataRow[])
                    {
                        DataRow[] dataRows = values as DataRow[];
                        string[] detailInfo = this.useMainDataColName.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                        if (detailInfo != null)
                        {
                            string[] param = new string[detailInfo.Length * dataRows.Length];
                            Dictionary<string, Object> map = allMap;
                            if (map == null)
                            {
                                map = this.report.GetDefualtSetting();
                            }
                            string key = string.Empty;
                            for (int row = 0; row < dataRows.Length; row++)
                            {
                                DataRow dr = dataRows[row];

                                for (int i = 0; i < detailInfo.Length; i++)
                                {
                                    if (dr.Table.Columns.Contains(detailInfo[i].ToString()) == false)
                                    {
                                        continue;
                                    }

                                    param[i + row * detailInfo.Length] = dr[detailInfo[i]].ToString();
                                    if (row == 0)
                                    {
                                        key = detailInfo[i];
                                    }
                                    else
                                    {
                                        key = detailInfo[i] + row.ToString();
                                    }
                                    if (map.ContainsKey(key))
                                    {
                                        map.Remove(key);
                                    }
                                    map[key] = dr[detailInfo[i]];
                                }
                            }
                            this.queryDetail(map);
                        }
                    }
                }
            }
        }

        void IMainReportForm_OnCellClickHandler(Object values)
        {
            if (isDetialDataObject && this.detailQueryType == EnumDetailQueryType.MouseClick)
            {
                if (this.detailConditionType == EnumDetailCondition.UseMainData)
                {
                    if (values is DataRow[])
                    {
                        DataRow[] dataRows = values as DataRow[];
                        string[] detailInfo = this.useMainDataColName.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                        if (detailInfo != null)
                        {
                            string[] param = new string[detailInfo.Length * dataRows.Length];
                            Dictionary<string, Object> map = allMap;
                            if (map == null)
                            {
                                map = this.report.GetDefualtSetting();
                            }
                            string key = string.Empty;
                            for (int row = 0; row < dataRows.Length; row++)
                            {
                                DataRow dr = dataRows[row];

                                for (int i = 0; i < detailInfo.Length; i++)
                                {
                                    param[i + row * detailInfo.Length] = dr[detailInfo[i]].ToString();
                                    if (row == 0)
                                    {
                                        key = detailInfo[i];
                                    }
                                    else
                                    {
                                        key = detailInfo[i] + row.ToString();
                                    }
                                    if (map.ContainsKey(key))
                                    {
                                        map.Remove(key);
                                    }
                                    map[key] = dr[detailInfo[i]];
                                }
                            }
                            this.queryDetail(map);
                        }
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
            //if (this.isDetialDataObject)
            //{
            //    this.IDetailReportForm.Export();
            //}
            return 1;
        }

        public int Print()
        {
            this.IMainReportForm.Print(this.report.PrintInfo);

            //if (this.isDetialDataObject)
            //{
            //    this.IDetailReportForm.Print();
            //}

            return 1;
        }

        public int PrintPreview()
        {
            this.IMainReportForm.PrintPreview(true, this.report.PrintInfo);
            //if (this.isDetialDataObject)
            //{
            //    this.IDetailReportForm.PrintPreview(true);
            //}
            return 1;
        }

        #endregion
    }
}
