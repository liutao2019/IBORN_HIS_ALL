using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface.Attributes;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;
using FarPoint.Win.Spread;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.ItemCompare
{

    #region 枚举

    [Serializable]
    public enum EnumDataSourceType
    {
        Sql,
        Custom,
        Dictionary,
        DepartmentType,
        EmployeeType,
        CurrentOper,
        CurrentDept
    }

    public enum EnumCompareType
    {
        /// <summary>
        /// A+B=C
        /// </summary>
        AB_C,
        /// <summary>
        /// A=B+C
        /// </summary>
        A_BC,
        /// <summary>
        /// A=B
        /// </summary>
        A_B,
        /// <summary>
        /// 其他
        /// </summary>
        NONE
    }

    #endregion

    /// <summary>
    /// [功能描述: 项目信息对照管理]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// 说明：
    /// </summary>
    public partial class ucExecDeptManager : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange, FS.FrameWork.WinForms.Forms.IReport
    {
        public ucExecDeptManager()
        {
            InitializeComponent();
        }

        #region 变量

        private FS.FrameWork.WinForms.Forms.ToolBarService ToolBar = new FS.FrameWork.WinForms.Forms.ToolBarService();
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbTargetDataSource = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbTargetDataSource2 = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbOriginalDataSource = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();

        private FS.FrameWork.Public.ObjectHelper originalDataSourceHelper = new FS.FrameWork.Public.ObjectHelper();
        private FS.FrameWork.Public.ObjectHelper targetDataSourceHelper = new FS.FrameWork.Public.ObjectHelper();
        private FS.FrameWork.Public.ObjectHelper targetDataSource2Helper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 配置文件1
        /// </summary>
        private string settingFile1 = "";

        /// <summary>
        /// 配置文件2
        /// </summary>
        private string settingFile2 ="";

        /// <summary>
        /// 数据集
        /// </summary>
        private System.Data.DataTable dtItems = null;

        /// <summary>
        /// 对照信息
        /// </summary>
        private System.Data.DataTable dtCompares = null;
        private int DefaultIndex = 3;

        private FS.SOC.HISFC.Fee.BizProcess.DefaultExecDept defaultExecDeptMgr = new FS.SOC.HISFC.Fee.BizProcess.DefaultExecDept();
        #endregion

        #region 属性
        private EnumCompareType compareType = EnumCompareType.A_B;
        [FSSetting()]
        [Description("项目对照类型，A_B代表A对照B，AB_C代表A+B对照C，A_BC代表A对照B+C"), Category("设置"), Browsable(true)]
        public EnumCompareType CompareType
        {
            get
            {
                return compareType;
            }
            set
            {
                compareType = value;
            }
        }

        private string compareRelation = "FunctionClass";
        [FSSetting()]
        [Description("项目对照关系"), Category("设置"), Browsable(true)]
        public string CompareRelation
        {
            get
            {
                return compareRelation;
            }
            set
            {
                compareRelation = value;
            }
        }

        private string compareRelationName = "默认执行科室";
        [FSSetting()]
        [Description("项目对照关系名称"), Category("设置"), Browsable(true)]
        public string CompareRelationName
        {
            get
            {
                return compareRelationName;
            }
            set
            {
                compareRelationName = value;
            }
        }

        private string originalDataSourceName = "功能分类";
        [FSSetting()]
        [Description("对照项目的数据来源名称"), Category("设置"), Browsable(true)]
        public string OriginalDataSourceName
        {
            get
            {
                return originalDataSourceName;
            }
            set
            {

                if (value.Equals(this.targetDataSourceName) || value.Equals(this.targetDataSourceName2))
                {
                    return;
                }
                this.originalDataSourceName = value;
            }
        }

        private EnumDataSourceType originalDataSourceType= EnumDataSourceType.Dictionary;
        [FSSetting()]
        [Description("对照项目的数据来源类型"), Category("设置"), Browsable(true)]
        public EnumDataSourceType OriginalDataSourceType
        {
            get
            {
                return this.originalDataSourceType;
            }
            set
            {
                this.originalDataSourceType = value;
            }
        }

        private string originalDataSource = "ITEMPRICETYPE";
        [FSSetting()]
        [Description("对照项目的数据来源"), Category("设置"), Browsable(true)]
        public string OriginalDataSource
        {
            get
            {
                return this.originalDataSource;
            }
            set
            {
                this.originalDataSource = value;
            }
        }

        private string targetDataSourceName = "执行科室";
        [FSSetting()]
        [Description("目标对照项目的数据来源名称"), Category("设置"), Browsable(true)]
        public string TargetDataSourceName
        {
            get
            {
                return this.targetDataSourceName;
            }
            set
            {
                if (value.Equals(this.targetDataSourceName2) || value.Equals(this.originalDataSourceName))
                {
                    return;
                }
                this.targetDataSourceName = value;
            }
        }

        private EnumDataSourceType targetDataSourceType= EnumDataSourceType.DepartmentType;
        [FSSetting()]
        [Description("目标对照项目的数据来源类型"), Category("设置"), Browsable(true)]
        public EnumDataSourceType TargetDataSourceType
        {
            get
            {
                return this.targetDataSourceType;
            }
            set
            {
                this.targetDataSourceType = value;
            }
        }

        private string targetDataSource = "";
        [FSSetting()]
        [Description("目标对照项目的数据来源"), Category("设置"), Browsable(true)]
        public string TargetDataSource
        {
            get
            {
                return this.targetDataSource;
            }
            set
            {
                this.targetDataSource = value;
            }
        }

        private string targetDataSourceName2 = "执行科室2";
        [FSSetting()]
        [Description("目标对照项目的数据来源名称2"), Category("设置"), Browsable(true)]
        public string TargetDataSourceName2
        {
            get
            {
                return this.targetDataSourceName2;
            }
            set
            {
                if (value.Equals(this.targetDataSourceName) || value.Equals(this.originalDataSourceName))
                {
                    return;
                }

                this.targetDataSourceName2 = value;
            }
        }

        public EnumDataSourceType targetDataSourceType2 = EnumDataSourceType.DepartmentType;
        [FSSetting()]
        [Description("目标对照项目的数据来源类型2"), Category("设置"), Browsable(true)]
        public EnumDataSourceType TargetDataSourceType2
        {
            get
            {
                return this.targetDataSourceType2;
            }
            set
            {
                this.targetDataSourceType2 = value;
            }
        }

        public string targetDataSource2 = "";
        [FSSetting()]
        [Description("目标对照项目的数据来源类型2"), Category("设置"), Browsable(true)]
        public string TargetDataSource2
        {
            get
            {
                return this.targetDataSource2;
            }
            set
            {
                this.targetDataSource2 = value;
            }
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        private int Init()
        {
            try
            {
                this.InitDataTable();
                this.InitFarPoint();
                this.InitBaseData();
                this.InitEvents();
                this.InitPops();
            }
            catch (Exception ex)
            {
                CommonController.CreateInstance().MessageBox("初始化失败，请系统管理员报告错误：" + ex.Message, MessageBoxIcon.Information);
                return -1;
            }

            return 1;
        }

        private void InitFarPoint()
        {
            this.settingFile1=FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\" + compareRelation + "FPFeeItemCompareSetting1.xml";
            this.settingFile2=FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\" + compareRelation + "FPFeeItemCompareSetting2.xml";
            if (!this.neuSpread.ReadSchema(this.settingFile1))
            {
                this.sheetView1.ColumnHeader.Rows[0].Height = 30F;
            }

            if (!this.fpSpread1.ReadSchema(this.settingFile2))
            {
                this.sheetView2.ColumnHeader.Rows[0].Height = 30F;
            }

            //将列名称转换成DataTable名称
            for (int i = 0; i < this.sheetView1.ColumnCount; i++)
            {
                this.sheetView1.Columns[i].Label = this.dtItems.Columns[i].ColumnName;
            }

            for (int i = 0; i < this.sheetView2.ColumnCount; i++)
            {
                this.sheetView2.Columns[i].Label = this.dtCompares.Columns[i].ColumnName;
            }

            this.sheetView2.DataAutoSizeColumns = false;
            this.sheetView2.DataAutoHeadings = false;
            this.sheetView1.DataAutoSizeColumns = false;

            InputMap im;

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
        }

        private void InitBaseData()
        {
            if (this.dtItems == null)
            {
                this.InitDataTable();
            }

            //初始化原始数据
            ArrayList al = this.queryDataSource(this.originalDataSource, this.originalDataSourceType, null);
            if (al == null)
            {
                CommonController.CreateInstance().MessageBox("加载数据项失败，类别：" + this.originalDataSourceType.ToString() + "编码：" + this.originalDataSource, MessageBoxIcon.Error);
                return;
            }
            originalDataSourceHelper.ArrayObject = al;

            this.dtItems.Clear();
            DataRow drAll = this.dtItems.NewRow();
            drAll[this.OriginalDataSourceName + "编码"] = "All";
            drAll[this.OriginalDataSourceName + "自定义码"] = "All";
            drAll[this.OriginalDataSourceName + "名称"] = "全部";
            drAll["拼音码"] = "QB";
            drAll["五笔码"] = "WU";
            this.dtItems.Rows.Add(drAll);
            foreach (FS.HISFC.Models.Base.Spell spell in al)
            {
                DataRow dr=this.dtItems.NewRow();
                dr[this.OriginalDataSourceName + "编码"] = spell.ID;
                dr[this.OriginalDataSourceName + "自定义码"] = spell.UserCode;
                dr[this.OriginalDataSourceName + "名称"] = spell.Name;
                dr["拼音码"] = spell.SpellCode;
                dr["五笔码"] = spell.WBCode;

                this.dtItems.Rows.Add(dr);
            }

            this.dtItems.AcceptChanges();

            //初始化目标数据
            al = this.queryDataSource(this.targetDataSource, this.targetDataSourceType, null);
            if (al == null)
            {
                CommonController.CreateInstance().MessageBox("加载数据项失败，类别：" + this.targetDataSourceType.ToString() + "编码：" + this.targetDataSource, MessageBoxIcon.Error);
                return;
            }
            targetDataSourceHelper.ArrayObject = al;

            al = this.queryDataSource(this.targetDataSource2, this.targetDataSourceType2, null);
            if (al == null)
            {
                CommonController.CreateInstance().MessageBox("加载数据项失败，类别：" + this.targetDataSourceType2.ToString() + "编码：" + this.targetDataSource2, MessageBoxIcon.Error);
                return;
            }
            targetDataSource2Helper.ArrayObject = al;

            DataTable dt =  this.defaultExecDeptMgr.QueryForDataSet(this.compareRelation);
            if (dt == null)
            {
                CommonController.CreateInstance().MessageBox("加载数据项失败，原因："+this.defaultExecDeptMgr.Err, MessageBoxIcon.Error);
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                this.dtCompares.Rows.Add(dr.ItemArray);
            }
            this.dtCompares.AcceptChanges();
        }

        private void InitDataTable()
        {
            #region dtItems
            if (this.dtItems == null)
            {
                this.dtItems = new DataTable();
            }

            //定义类型
            this.dtItems.Columns.AddRange(new DataColumn[] { 
                                                   new DataColumn(this.OriginalDataSourceName+"编码", typeof(string)),
                                                   new DataColumn(this.OriginalDataSourceName+"自定义码", typeof(string)),
                                                   new DataColumn(this.OriginalDataSourceName+"名称", typeof(string)),
                                                   new DataColumn("拼音码", typeof(string)),
                                                   new DataColumn("五笔码", typeof(string))
            });

            foreach (DataColumn dc in this.dtItems.Columns)
            {
                dc.ReadOnly = true;
            }

            DataColumn[] keys = new DataColumn[1];
            keys[0] = this.dtItems.Columns[this.OriginalDataSourceName + "编码"];
            this.dtItems.PrimaryKey = keys;
            this.sheetView1.DataSource = this.dtItems.DefaultView;

            #endregion

            #region dtCompares
            if (this.dtCompares == null)
            {
                this.dtCompares = new DataTable();
            }

            //定义类型
            this.dtCompares.Columns.AddRange(new DataColumn[] { 
                                                   new DataColumn("主键", typeof(string)),
                                                   new DataColumn(this.CompareRelationName+"编码", typeof(string)),
                                                   new DataColumn(this.CompareRelationName+"名称", typeof(string)),
                                                   new DataColumn(this.OriginalDataSourceName+"编码", typeof(string)),
                                                   new DataColumn(this.OriginalDataSourceName+"自定义码", typeof(string)),
                                                   new DataColumn(this.OriginalDataSourceName+"名称", typeof(string)),
                                                   new DataColumn(this.TargetDataSourceName+"编码", typeof(string)),
                                                   new DataColumn(this.TargetDataSourceName+"自定义码", typeof(string)),
                                                   new DataColumn(this.TargetDataSourceName+"名称", typeof(string)),
                                                   new DataColumn(this.TargetDataSourceName+"拼音码", typeof(string)),
                                                   new DataColumn(this.TargetDataSourceName+"五笔码", typeof(string)),
                                                   new DataColumn(this.TargetDataSourceName+"扩展", typeof(string)),
                                                   new DataColumn(this.TargetDataSourceName+"扩展2", typeof(string)),
                                                   new DataColumn(this.TargetDataSourceName+"扩展3", typeof(string)),
                                                   new DataColumn(this.TargetDataSourceName2+"编码", typeof(string)),
                                                   new DataColumn(this.TargetDataSourceName2+"自定义码", typeof(string)),
                                                   new DataColumn(this.TargetDataSourceName2+"名称", typeof(string)),
                                                   new DataColumn(this.TargetDataSourceName2+"拼音码", typeof(string)),
                                                   new DataColumn(this.TargetDataSourceName2+"五笔码", typeof(string)),
                                                   new DataColumn(this.TargetDataSourceName2+"扩展", typeof(string)),
                                                   new DataColumn(this.TargetDataSourceName2+"扩展2", typeof(string)),
                                                   new DataColumn(this.TargetDataSourceName2+"扩展3", typeof(string))
            });

            this.dtCompares.PrimaryKey = new DataColumn[] { this.dtCompares.Columns["主键"] };
            this.sheetView2.DataSource = this.dtCompares.DefaultView;
            this.sheetView2.Columns[0, 2].Locked = true;

            #endregion
        }

        private void InitEvents()
        {
            this.txtOriginalFilter.TextChanged -= new EventHandler(txtOriginalFilter_TextChanged);
            this.txtOriginalFilter.TextChanged += new EventHandler(txtOriginalFilter_TextChanged);
            this.txtOriginalFilter.KeyDown -= new KeyEventHandler(txtOriginalFilter_KeyDown);
            this.txtOriginalFilter.KeyDown += new KeyEventHandler(txtOriginalFilter_KeyDown);

            this.txtCompareFilter.TextChanged -= new EventHandler(txtCompareFilter_TextChanged);
            this.txtCompareFilter.TextChanged += new EventHandler(txtCompareFilter_TextChanged);
            this.txtCompareFilter.KeyDown -= new KeyEventHandler(txtOriginalFilter_KeyDown);
            this.txtCompareFilter.KeyDown += new KeyEventHandler(txtOriginalFilter_KeyDown);
            this.neuSpread.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread_ColumnWidthChanged);
            this.neuSpread.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread_ColumnWidthChanged);
            this.neuSpread.KeyDown -= new KeyEventHandler(txtOriginalFilter_KeyDown);
            this.neuSpread.KeyDown += new KeyEventHandler(txtOriginalFilter_KeyDown);

            this.fpSpread1.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpSpread1_ColumnWidthChanged);
            this.fpSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpSpread1_ColumnWidthChanged);

            this.fpSpread1.EditModeOn -= new EventHandler(fpSpread1_EditModeOn);
            this.fpSpread1.EditModeOn += new EventHandler(fpSpread1_EditModeOn);

            this.fpSpread1.EditChange -= new FarPoint.Win.Spread.EditorNotifyEventHandler(fpSpread1_EditChange);
            this.fpSpread1.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpSpread1_EditChange);
            this.neuSpread.SelectionChanged -= new SelectionChangedEventHandler(neuSpread_SelectionChanged);
            this.neuSpread.SelectionChanged += new SelectionChangedEventHandler(neuSpread_SelectionChanged);
        }

        private void InitPops()
        {
            this.lbTargetDataSource.ItemSelected += new EventHandler(lbTargetDataSource_SelectItem);
            this.groupBox1.Controls.Add(this.lbTargetDataSource);
            this.lbTargetDataSource.BackColor = this.label1.BackColor;
            this.lbTargetDataSource.Font = new Font("宋体", 11F);
            this.lbTargetDataSource.BorderStyle = BorderStyle.None;
            this.lbTargetDataSource.Cursor = Cursors.Hand;
            this.lbTargetDataSource.Location = new Point(this.label1.Left + 1, this.label1.Top + 1);
            this.lbTargetDataSource.Size = new Size(this.label1.Width - 2, this.label1.Height - 2);
            this.lbTargetDataSource.AddItems(this.targetDataSourceHelper.ArrayObject);
            this.lbTargetDataSource.BringToFront();

            this.lbTargetDataSource2.ItemSelected += new EventHandler(lbTargetDataSource2_SelectItem);
            this.groupBox1.Controls.Add(this.lbTargetDataSource2);
            this.lbTargetDataSource2.BackColor = this.label1.BackColor;
            this.lbTargetDataSource2.Font = new Font("宋体", 11F);
            this.lbTargetDataSource2.BorderStyle = BorderStyle.None;
            this.lbTargetDataSource2.Cursor = Cursors.Hand;
            this.lbTargetDataSource2.Location = new Point(this.label1.Left + 1, this.label1.Top + 1);
            this.lbTargetDataSource2.Size = new Size(this.label1.Width - 2, this.label1.Height - 2);
            this.lbTargetDataSource2.AddItems(this.targetDataSource2Helper.ArrayObject);
            this.lbTargetDataSource2.BringToFront();

            this.lbOriginalDataSource.ItemSelected += new EventHandler(lbOriginalDataSource_SelectItem);
            this.groupBox1.Controls.Add(this.lbOriginalDataSource);
            this.lbOriginalDataSource.BackColor = this.label1.BackColor;
            this.lbOriginalDataSource.Font = new Font("宋体", 11F);
            this.lbOriginalDataSource.BorderStyle = BorderStyle.None;
            this.lbOriginalDataSource.Cursor = Cursors.Hand;
            this.lbOriginalDataSource.Location = new Point(this.label1.Left + 1, this.label1.Top + 1);
            this.lbOriginalDataSource.Size = new Size(this.label1.Width - 2, this.label1.Height - 2);
            this.lbOriginalDataSource.AddItems(this.originalDataSourceHelper.ArrayObject);
            this.lbOriginalDataSource.BringToFront();
        }
        #endregion

        #region 方法

        /// <summary>
        /// 查找数据集
        /// </summary>
        /// <param name="datasourceinfo"></param>
        /// <param name="datasoureType"></param>
        /// <param name="datasource"></param>
        /// <returns></returns>
        private ArrayList queryDataSource(string datasourceinfo, EnumDataSourceType datasoureType, List<FS.HISFC.Models.Base.Spell> datasource)
        {
            switch (datasoureType)
            {
                case EnumDataSourceType.Sql://自定义SQL
                    #region
                    FS.FrameWork.Management.DataBaseManger dbManager = new FS.FrameWork.Management.DataBaseManger();
                    if (dbManager.ExecQuery(datasourceinfo) == -1)
                    {
                        CommonController.CreateInstance().MessageBox(this, "执行自定义查询SQL出错！" + dbManager.Err, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                            obj.UserCode = dbManager.Reader[2].ToString();
                        }
                        if (dbManager.Reader.FieldCount > 3)
                        {
                            obj.SpellCode = dbManager.Reader[3].ToString();
                        }
                        if (dbManager.Reader.FieldCount > 4)
                        {
                            obj.WBCode = dbManager.Reader[4].ToString();
                        }
                        al.Add(obj);
                    }

                    return al;
                    break;
                    #endregion
                case EnumDataSourceType.DepartmentType://科室
                    #region
                    if (datasourceinfo == "ALL")
                    {
                        return CommonController.CreateInstance().QueryDepartment();
                    }
                    else
                    {
                        FS.HISFC.Models.Base.EnumDepartmentType deptType = FS.FrameWork.Function.NConvert.ToEnum<FS.HISFC.Models.Base.EnumDepartmentType>(datasourceinfo, FS.HISFC.Models.Base.EnumDepartmentType.C);
                        return CommonController.CreateInstance().QueryDepartment(deptType);
                    }
                    break;
                    #endregion
                case EnumDataSourceType.Dictionary://常数
                    return CommonController.CreateInstance().QueryConstant(datasourceinfo);
                    break;
                case EnumDataSourceType.EmployeeType://人员
                    #region
                    if (datasourceinfo == "ALL")
                    {
                        return CommonController.CreateInstance().QueryEmployee();
                    }
                    else
                    {
                        FS.HISFC.Models.Base.EnumEmployeeType emplType = FS.FrameWork.Function.NConvert.ToEnum<FS.HISFC.Models.Base.EnumEmployeeType>(datasourceinfo, FS.HISFC.Models.Base.EnumEmployeeType.D);
                        return CommonController.CreateInstance().QueryEmployee(emplType);
                    }
                    break;
                    #endregion
                case EnumDataSourceType.CurrentDept:
                    #region
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
                    #endregion
                case EnumDataSourceType.CurrentOper:
                    #region
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
                    #endregion
                case EnumDataSourceType.Custom:
                default:
                    if (datasource == null)
                    {
                        return null;
                    }

                    return new ArrayList(datasource);
                    break;
            }
        }

        private void FilterDtItems()
        {
            string filterText = " like '%" + this.txtOriginalFilter.Text + "%'";
            string filter = Function.GetFilterStr(this.dtItems.DefaultView, this.txtOriginalFilter.Text);

            if (this.dtCompares.Columns.Contains(this.OriginalDataSourceName + "编码"))
            {
                filter = Function.ConnectFilterStr(filter, this.OriginalDataSourceName + "编码" + filterText, "or");
            }
            if (this.dtCompares.Columns.Contains(this.OriginalDataSourceName + "自定义码"))
            {
                filter = Function.ConnectFilterStr(filter, this.OriginalDataSourceName + "自定义码" + filterText, "or");
            }
            if (this.dtCompares.Columns.Contains(this.OriginalDataSourceName + "名称"))
            {
                filter = Function.ConnectFilterStr(filter, this.OriginalDataSourceName + "名称" + filterText, "or");
            }

            this.dtItems.DefaultView.RowFilter = filter;

            this.neuSpread.ReadSchema(this.settingFile1);

            this.neuSpread_SelectionChanged(null, null);
        }

        private void FilterDtCompareItems()
        {
            string filterText = " like '%" + this.txtCompareFilter.Text + "%'";
            string filter = "";

            if (this.dtCompares.Columns.Contains(this.CompareRelationName + "编码"))
            {
                filter = Function.ConnectFilterStr(filter, this.CompareRelationName + "编码" + filterText, "or");
            }
            if (this.dtCompares.Columns.Contains(this.CompareRelationName + "名称"))
            {
                filter = Function.ConnectFilterStr(filter, this.CompareRelationName + "名称" + filterText, "or");
            }

            if (this.sheetView1.ActiveRowIndex< 0)
            {
                if (this.dtCompares.Columns.Contains(this.OriginalDataSourceName + "编码"))
                {
                    filter = Function.ConnectFilterStr(filter, this.OriginalDataSourceName + "编码" + filterText, "or");
                }
                if (this.dtCompares.Columns.Contains(this.OriginalDataSourceName + "名称"))
                {
                    filter = Function.ConnectFilterStr(filter, this.OriginalDataSourceName + "名称" + filterText, "or");
                }
                if (this.dtCompares.Columns.Contains(this.OriginalDataSourceName + "自定义码"))
                {
                    filter = Function.ConnectFilterStr(filter, this.OriginalDataSourceName + "自定义码" + filterText, "or");
                }
            }

            if (this.dtCompares.Columns.Contains(this.TargetDataSourceName + "编码"))
            {
                    filter = Function.ConnectFilterStr(filter, this.TargetDataSourceName + "编码" + filterText+" ", "or");
            }
            if (this.dtCompares.Columns.Contains(this.TargetDataSourceName + "自定义码"))
            {
                filter = Function.ConnectFilterStr(filter, this.TargetDataSourceName + "自定义码" + filterText, "or");
            }
            if (this.dtCompares.Columns.Contains(this.TargetDataSourceName + "名称"))
            {
                filter = Function.ConnectFilterStr(filter, this.TargetDataSourceName + "名称" + filterText, "or");
            }
            if (this.dtCompares.Columns.Contains(this.TargetDataSourceName + "拼音码"))
            {
                filter = Function.ConnectFilterStr(filter, this.TargetDataSourceName + "拼音码" + filterText, "or");
            }
            if (this.dtCompares.Columns.Contains(this.TargetDataSourceName + "五笔码"))
            {
                filter = Function.ConnectFilterStr(filter, this.TargetDataSourceName + "五笔码" + filterText, "or");
            }

            if (this.dtCompares.Columns.Contains(this.TargetDataSourceName2 + "编码"))
            {
                    filter = Function.ConnectFilterStr(filter, this.TargetDataSourceName2 + "编码" + filterText, "or");
            }
            if (this.dtCompares.Columns.Contains(this.TargetDataSourceName2 + "自定义码"))
            {
                filter = Function.ConnectFilterStr(filter, this.TargetDataSourceName2 + "自定义码" + filterText, "or");
            }
            if (this.dtCompares.Columns.Contains(this.TargetDataSourceName2 + "名称"))
            {
                filter = Function.ConnectFilterStr(filter, this.TargetDataSourceName2 + "名称" + filterText, "or");
            }
            if (this.dtCompares.Columns.Contains(this.TargetDataSourceName2 + "拼音码"))
            {
                filter = Function.ConnectFilterStr(filter, this.TargetDataSourceName2 + "拼音码" + filterText, "or");
            }
            if (this.dtCompares.Columns.Contains(this.TargetDataSourceName2 + "五笔码"))
            {
                filter = Function.ConnectFilterStr(filter, this.TargetDataSourceName2 + "五笔码" + filterText, "or ");
            }

            if (this.sheetView1.ActiveRowIndex >= 0)
            {
                string text = this.sheetView1.Cells[this.sheetView1.ActiveRowIndex, this.neuSpread.GetColumnIndex(0, this.originalDataSourceName + "编码")].Text;
                filter = Function.ConnectFilterStr("(" + filter + ")", "("+this.OriginalDataSourceName + "编码" + " = '" + text + "' or '" + text + "' = 'All')", "and");
            }

            this.dtCompares.DefaultView.RowFilter = filter;
            //this.dtCompares.DefaultView.Sort = this.originalDataSourceName + "编码," + this.targetDataSourceName + "编码," + this.targetDataSourceName2 + "编码";
        }

        private void AddNewCompare()
        {
            if (this.dtCompares == null)
            {
                this.InitDataTable();
            }
            this.txtCompareFilter.Text = "";
            this.dtCompares.DefaultView.Sort = "";
            DataRow row = this.dtCompares.NewRow();
            row["主键"] = this.defaultExecDeptMgr.GetID();
            row[this.compareRelationName + "编码"] = this.compareRelation;
            row[this.compareRelationName + "名称"] = this.compareRelationName;
            this.dtCompares.Rows.Add(row);
            this.fpSpread1.Focus();

            if (this.sheetView1.ActiveRowIndex >= 0 && this.sheetView1.Cells[this.sheetView1.ActiveRowIndex, this.neuSpread.GetColumnIndex(0, this.originalDataSourceName + "编码")].Text.Equals("All") == false)
            {
                row[this.originalDataSourceName + "编码"] = this.sheetView1.Cells[this.sheetView1.ActiveRowIndex, this.neuSpread.GetColumnIndex(0, this.originalDataSourceName + "编码")].Text;
                row[this.originalDataSourceName + "名称"] = this.sheetView1.Cells[this.sheetView1.ActiveRowIndex, this.neuSpread.GetColumnIndex(0, this.originalDataSourceName + "名称")].Text;
                row[this.originalDataSourceName + "自定义码"] = this.sheetView1.Cells[this.sheetView1.ActiveRowIndex, this.neuSpread.GetColumnIndex(0, this.originalDataSourceName + "自定义码")].Text;
                this.sheetView2.ActiveColumnIndex = this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "编码");
                DefaultIndex = this.sheetView2.ActiveColumnIndex;
                this.fpSpread1.ShowColumn(0, this.sheetView2.ActiveColumnIndex, HorizontalPosition.Center);
            }
            else
            {
                row[this.originalDataSourceName + "编码"] = "";
                row[this.originalDataSourceName + "名称"] = "";
                row[this.originalDataSourceName + "自定义码"] = "";
                this.sheetView2.ActiveColumnIndex = this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "编码");
                DefaultIndex = this.sheetView2.ActiveColumnIndex;
                this.fpSpread1.ShowColumn(0, this.sheetView2.ActiveColumnIndex, HorizontalPosition.Center);
            }

            int rowIndex = this.sheetView2.RowCount - 1;
            if (rowIndex >= 0)
            {
                this.sheetView2.ActiveRowIndex = rowIndex;
                this.fpSpread1.ShowRow(0, rowIndex, VerticalPosition.Center);
            }
            
        }

        private void DeleteCompare()
        {
            if (this.sheetView2.RowCount == 0)
            {
                return;
            }

            if (this.sheetView2.ActiveRowIndex < 0)
            {
                return;
            }
            if (this.sheetView2.Cells[this.sheetView2.ActiveRowIndex, this.fpSpread1.GetColumnIndex(0, "主键")].Text.Length > 0)
            {
                if (CommonController.CreateInstance().MessageBox("确认删除选中行？", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.sheetView2.Rows.Remove(this.sheetView2.ActiveRowIndex, 1);
                }
            }
            else
            {
                this.sheetView2.Rows.Remove(this.sheetView2.ActiveRowIndex, 1);
            }
        }

        private void Save()
        {
            this.fpSpread1.StopCellEditing();
            foreach(DataRow dr in this.dtCompares.Rows)
            {
                dr.EndEdit();
            }

            //取数据
            #region Delete

            List<FS.SOC.HISFC.Fee.Models.DefaultExecDept> listDelete = null;
            DataTable dt=this.dtCompares.GetChanges(DataRowState.Deleted);
            if (dt != null)
            {
                dt.RejectChanges();
                listDelete = new List<FS.SOC.HISFC.Fee.Models.DefaultExecDept>();
                foreach (DataRow row in dt.Rows)
                {
                    if (row.RowState == DataRowState.Added)
                    {
                        continue;
                    }
                    FS.SOC.HISFC.Fee.Models.DefaultExecDept execDept = new FS.SOC.HISFC.Fee.Models.DefaultExecDept();
                    execDept.ID = row["主键"].ToString() ;
                    if (string.IsNullOrEmpty(execDept.ID) == false)
                    {
                        listDelete.Add(execDept);
                    }
                }
            }
            #endregion

            #region Add
            List<FS.SOC.HISFC.Fee.Models.DefaultExecDept> listAdd = null;
             dt = this.dtCompares.GetChanges(DataRowState.Added);
            if (dt != null)
            {
                listAdd = new List<FS.SOC.HISFC.Fee.Models.DefaultExecDept>();
                foreach (DataRow row in dt.Rows)
                {
                    FS.SOC.HISFC.Fee.Models.DefaultExecDept execDept = new FS.SOC.HISFC.Fee.Models.DefaultExecDept();
                    execDept.ID = row["主键"].ToString();
                    execDept.Compare.ID = this.compareRelation;
                    execDept.Compare.Name = this.compareRelationName;
                    execDept.Original.ID = row[this.originalDataSourceName+"编码"].ToString();
                    if (execDept.Original.ID.Length == 0||originalDataSourceHelper.GetObjectFromID(execDept.Original.ID)==null)
                    {
                        continue;
                    }
                    execDept.Original.Name = row[this.originalDataSourceName + "名称"].ToString();
                    execDept.Original.UserCode = row[this.originalDataSourceName + "自定义码"].ToString();
                    execDept.Target.ID = row[this.targetDataSourceName + "编码"].ToString();
                    if (execDept.Target.ID.Length == 0 || this.targetDataSourceHelper.GetObjectFromID(execDept.Target.ID) == null)
                    {
                        continue;
                    }
                    execDept.Target.Name = row[this.targetDataSourceName + "名称"].ToString();
                    execDept.Target.UserCode = row[this.targetDataSourceName + "自定义码"].ToString();
                    execDept.Target.SpellCode = row[this.targetDataSourceName + "拼音码"].ToString();
                    execDept.Target.WBCode = row[this.targetDataSourceName + "五笔码"].ToString();
                    execDept.Target.User01 = row[this.targetDataSourceName + "扩展"].ToString();
                    execDept.Target.User02 = row[this.targetDataSourceName + "扩展2"].ToString();
                    execDept.Target.User03 = row[this.targetDataSourceName + "扩展3"].ToString();

                    execDept.Target2.ID = row[this.targetDataSourceName2 + "编码"].ToString();
                    if (this.compareType == EnumCompareType.A_BC || this.compareType == EnumCompareType.AB_C)
                    {
                        if (execDept.Target2.ID.Length == 0 || this.targetDataSource2Helper.GetObjectFromID(execDept.Target2.ID) == null)
                        {
                            continue;
                        }
                    }
                    execDept.Target2.Name = row[this.targetDataSourceName2 + "名称"].ToString();
                    execDept.Target2.UserCode = row[this.targetDataSourceName2 + "自定义码"].ToString();
                    execDept.Target2.SpellCode = row[this.targetDataSourceName2 + "拼音码"].ToString();
                    execDept.Target2.WBCode = row[this.targetDataSourceName2 + "五笔码"].ToString();
                    execDept.Target2.User01 = row[this.targetDataSourceName2 + "扩展"].ToString();
                    execDept.Target2.User02 = row[this.targetDataSourceName2 + "扩展2"].ToString();
                    execDept.Target2.User03 = row[this.targetDataSourceName2 + "扩展3"].ToString();
                    listAdd.Add(execDept);
                }
            }
            #endregion

            #region Modify
            List<FS.SOC.HISFC.Fee.Models.DefaultExecDept> listModify = null;
            dt = this.dtCompares.GetChanges(DataRowState.Modified);
            if (dt != null)
            {
                listModify = new List<FS.SOC.HISFC.Fee.Models.DefaultExecDept>();
                foreach (DataRow row in dt.Rows)
                {
                    FS.SOC.HISFC.Fee.Models.DefaultExecDept execDept = new FS.SOC.HISFC.Fee.Models.DefaultExecDept();
                    execDept.ID = row["主键"].ToString();
                    execDept.Compare.ID = this.compareRelation;
                    execDept.Compare.Name = this.compareRelationName;
                    execDept.Original.ID = row[this.originalDataSourceName + "编码"].ToString();
                    if (execDept.Original.ID.Length == 0 || originalDataSourceHelper.GetObjectFromID(execDept.Original.ID) == null)
                    {
                        continue;
                    }
                    execDept.Original.Name = row[this.originalDataSourceName + "名称"].ToString();
                    execDept.Original.UserCode = row[this.originalDataSourceName + "自定义码"].ToString();

                    execDept.Target.ID = row[this.targetDataSourceName + "编码"].ToString();
                    if (execDept.Target.ID.Length == 0 || this.targetDataSourceHelper.GetObjectFromID(execDept.Target.ID) == null)
                    {
                        continue;
                    }
                    execDept.Target.Name = row[this.targetDataSourceName + "名称"].ToString();
                    execDept.Target.UserCode = row[this.targetDataSourceName + "自定义码"].ToString();
                    execDept.Target.SpellCode = row[this.targetDataSourceName + "拼音码"].ToString();
                    execDept.Target.WBCode = row[this.targetDataSourceName + "五笔码"].ToString();
                    execDept.Target.User01 = row[this.targetDataSourceName + "扩展"].ToString();
                    execDept.Target.User02 = row[this.targetDataSourceName + "扩展2"].ToString();
                    execDept.Target.User03 = row[this.targetDataSourceName + "扩展3"].ToString();

                    execDept.Target2.ID = row[this.targetDataSourceName2 + "编码"].ToString();
                    if (this.compareType == EnumCompareType.A_BC || this.compareType == EnumCompareType.AB_C)
                    {
                        if (execDept.Target2.ID.Length == 0 || this.targetDataSource2Helper.GetObjectFromID(execDept.Target2.ID) == null)
                        {
                            continue;
                        }
                    }
                    execDept.Target2.Name = row[this.targetDataSourceName2 + "名称"].ToString();
                    execDept.Target2.UserCode = row[this.targetDataSourceName2 + "自定义码"].ToString();
                    execDept.Target2.SpellCode = row[this.targetDataSourceName2 + "拼音码"].ToString();
                    execDept.Target2.WBCode = row[this.targetDataSourceName2 + "五笔码"].ToString();
                    execDept.Target2.User01 = row[this.targetDataSourceName2 + "扩展"].ToString();
                    execDept.Target2.User02 = row[this.targetDataSourceName2 + "扩展2"].ToString();
                    execDept.Target2.User03 = row[this.targetDataSourceName2 + "扩展3"].ToString();
                    listModify.Add(execDept);
                }
            }
            #endregion

            if (this.defaultExecDeptMgr.Save(listAdd, listModify, listDelete) < 0)
            {
                CommonController.CreateInstance().MessageBox("保存失败，原因：" + this.defaultExecDeptMgr.Err, MessageBoxIcon.Warning);
                return;
            }
            else 
            {
                CommonController.CreateInstance().MessageBox("保存成功", MessageBoxIcon.Information);
            }
            this.dtCompares.AcceptChanges();
        }

        #region 弹出窗口

        /// <summary>
        /// 设置控件是否可见
        /// </summary>
        /// <param name="visible"></param>
        private void visible(bool visible)
        {
            if (visible == false)
            { this.groupBox1.Visible = false; }
            else
            {
                int col = this.sheetView2.ActiveColumnIndex;
                if (col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "编码")
                    || col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "名称")
                    || col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "自定义码"))
                {
                    this.lbTargetDataSource.BringToFront();
                    this.groupBox1.Visible = true;
                }
                else if (col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "编码")
                    || col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "名称")
                    || col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "自定义码"))
                {
                    this.lbTargetDataSource2.BringToFront();
                    this.groupBox1.Visible = true;
                }
                else if (col == this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "编码")
                    || col == this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "名称")
                    || col == this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "自定义码"))
                {
                    this.lbOriginalDataSource.BringToFront();
                    this.groupBox1.Visible = true;
                }
            }
        }

        /// <summary>
        /// 前一行
        /// </summary>
        private void nextRow()
        {
            int col = this.sheetView2.ActiveColumnIndex;
            if (col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "编码")
                || col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "名称")
                    || col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "自定义码"))
            {
                this.lbTargetDataSource.NextRow();
            }
            else if (col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "编码")
               || col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "名称")
                    || col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "自定义码"))
            {
                this.lbTargetDataSource2.NextRow();
            }
            else if (col == this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "编码")
               || col == this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "名称")
                    || col == this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "自定义码"))
            {
                this.lbOriginalDataSource.NextRow();
            }
        }

        /// <summary>
        /// 上一行
        /// </summary>
        private void priorRow()
        {
            int col = this.sheetView2.ActiveColumnIndex;
            if (col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "编码")
                || col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "名称")
                || col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "自定义码"))
            {
                this.lbTargetDataSource.PriorRow();
            }
            else if (col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "编码")
                || col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "名称")
                || col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "自定义码"))
            {
                this.lbTargetDataSource2.PriorRow();
            }
            else if (col == this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "编码")
                || col == this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "名称")
                || col == this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "自定义码"))
            {
                this.lbOriginalDataSource.PriorRow();
            }
        }

        /// <summary>
        /// 选择科室
        /// </summary>
        /// <returns></returns>
        private int selectOriginalDataSource()
        {
            int row = this.sheetView2.ActiveRowIndex;

            FS.HISFC.Models.Base.Spell obj = null;
            this.fpSpread1.StopCellEditing();
            string deptId = this.sheetView2.GetText(row, this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "编码")); string key = this.sheetView2.GetText(row, this.fpSpread1.GetColumnIndex(0, "主键"));
            string originalCode = this.sheetView2.GetText(row, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "编码"));
            bool isSuccess = false;

            if (this.groupBox1.Visible || this.originalDataSourceHelper.GetObjectFromID(deptId) != null)
            {
                if (this.groupBox1.Visible)
                {
                    obj = this.lbOriginalDataSource.GetSelectedItem() as FS.HISFC.Models.Base.Spell;
                }
                else
                {
                    obj = this.originalDataSourceHelper.GetObjectFromID(deptId) as FS.HISFC.Models.Base.Spell;
                }
                if (obj == null)
                {
                    return -1;
                }
                isSuccess = true;
                if (compareType == EnumCompareType.A_B)
                {
                    DataRow[] drs = this.dtCompares.Select(string.Format(this.originalDataSourceName + "编码 = '{0}' and 主键<>'{1}' and " + this.targetDataSourceName + "编码='{2}'", obj.ID, key, originalCode));
                    if (drs != null && drs.Length > 0)
                    {
                        CommonController.CreateInstance().MessageBox(this.targetDataSourceName + "编码为：" + obj.ID + "名称为：" + obj.Name + "已经维护，请重新选择", MessageBoxIcon.Warning);
                        isSuccess = false;
                    }
                }
                else if (compareType == EnumCompareType.A_BC || compareType == EnumCompareType.AB_C)
                {
                    string targetCode = this.sheetView2.Cells[row, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "编码")].Text;
                    DataRow[] drs = this.dtCompares.Select(string.Format(this.originalDataSourceName + "编码 = '{0}' and " + this.targetDataSourceName2 + "编码 ='{1}' and  主键<>'{2}' and " + this.originalDataSourceName + "编码='{3}'", obj.ID, targetCode, key, originalCode));

                    if (drs != null && drs.Length > 0)
                    {
                        CommonController.CreateInstance().MessageBox(this.targetDataSourceName + "编码为：" + obj.ID + "名称为：" + obj.Name + "已经维护，请重新选择", MessageBoxIcon.Warning);
                        isSuccess = false;
                    }
                }
            }

            if (isSuccess)
            {
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "编码"), obj.ID, false);
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "名称"), obj.Name, false);
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "自定义码"), obj.UserCode, false);
                this.visible(false);
            }
            else
            {
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, originalDataSourceName + "编码"), "", false);
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, originalDataSourceName + "名称"), "", false);
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "自定义码"), "", false);                   return -1;
            }

            return 0;
        }

        /// <summary>
        /// 选择科室
        /// </summary>
        /// <returns></returns>
        private int selectTargetDataSource()
        {
            int row = this.sheetView2.ActiveRowIndex;

            FS.HISFC.Models.Base.Spell obj=null;
            this.fpSpread1.StopCellEditing();
            bool isSuccess = false;
            string deptId = this.sheetView2.GetText(row, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "编码"));
            string key = this.sheetView2.GetText(row, this.fpSpread1.GetColumnIndex(0, "主键"));
            string originalCode = this.sheetView2.GetText(row, this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "编码"));
            if (this.groupBox1.Visible || this.targetDataSourceHelper.GetObjectFromID(deptId) != null)
            {
                if (this.groupBox1.Visible)
                {
                    obj = this.lbTargetDataSource.GetSelectedItem() as FS.HISFC.Models.Base.Spell;
                }
                else
                {
                    obj = this.targetDataSourceHelper.GetObjectFromID(deptId) as FS.HISFC.Models.Base.Spell;
                }
                if (obj == null)
                {
                    return -1;
                }

                isSuccess = true;

                if (compareType == EnumCompareType.A_B)
                {
                    DataRow[] drs = this.dtCompares.Select(string.Format(this.targetDataSourceName + "编码 = '{0}' and 主键<>'{1}' and " + this.originalDataSourceName + "编码='{2}'", obj.ID, key,originalCode));
                    if (drs != null && drs.Length > 0)
                    {
                        CommonController.CreateInstance().MessageBox(this.targetDataSourceName + "编码为：" + obj.ID + "名称为：" + obj.Name + "已经维护，请重新选择", MessageBoxIcon.Warning);
                        isSuccess = false;
                    }
                }
                else if (compareType == EnumCompareType.A_BC || compareType == EnumCompareType.AB_C)
                {
                    string targetCode = this.sheetView2.Cells[row, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "编码")].Text;
                    DataRow[] drs = this.dtCompares.Select(string.Format(this.targetDataSourceName + "编码 = '{0}' and " + this.targetDataSourceName2 + "编码 ='{1}' and  主键<>'{2}' and " + this.originalDataSourceName + "编码='{3}'", obj.ID, targetCode, key, originalCode));

                    if (drs != null && drs.Length > 0)
                    {
                        CommonController.CreateInstance().MessageBox(this.targetDataSourceName + "编码为：" + obj.ID + "名称为：" + obj.Name + "已经维护，请重新选择", MessageBoxIcon.Warning);
                        isSuccess = false;
                    }
                }

            }
            if(isSuccess)
            {

                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "编码"), obj.ID, false);
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "名称"), obj.Name, false);
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "自定义码"), obj.UserCode, false);
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "拼音码"), obj.SpellCode, false);
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "五笔码"), obj.WBCode, false);
                this.visible(false);
            }
            else
            {
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, targetDataSourceName + "编码"), "", false);
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, targetDataSourceName + "名称"), "", false);
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "自定义码"), "", false);
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "拼音码"), "", false);
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "五笔码"), "", false);
                return -1;
            }

            return 0;
        }

        private int selectTargetDataSource2()
        {
            int row = this.sheetView2.ActiveRowIndex;

            FS.HISFC.Models.Base.Spell obj=null;
            this.fpSpread1.StopCellEditing();
            string deptId = this.sheetView2.GetText(row, this.fpSpread1.GetColumnIndex(0, targetDataSourceName2 + "编码"));
            string key = this.sheetView2.GetText(row, this.fpSpread1.GetColumnIndex(0, "主键"));
            string originalCode = this.sheetView2.GetText(row, this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "编码"));
            bool isSuccess = false;
            if (this.groupBox1.Visible || this.targetDataSource2Helper.GetObjectFromID(deptId) != null)
            {
                if (this.groupBox1.Visible)
                {
                    obj = this.lbTargetDataSource2.GetSelectedItem() as FS.HISFC.Models.Base.Spell;
                }
                else
                {
                    obj = this.targetDataSource2Helper.GetObjectFromID(deptId) as FS.HISFC.Models.Base.Spell;
                }
                if (obj == null)
                {
                    return -1;
                }
                isSuccess = true;

                if (compareType == EnumCompareType.A_BC||compareType== EnumCompareType.AB_C)
                {
                    string targetCode = this.sheetView2.Cells[row, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "编码")].Text;
                    DataRow[] drs = this.dtCompares.Select(string.Format(this.targetDataSourceName + "编码 = '{0}' and " + this.targetDataSourceName2 + "编码 ='{1}' and  主键<>'{2}' and " + this.originalDataSourceName + "编码='{3}'", obj.ID, targetCode, key, originalCode));

                    if (drs != null && drs.Length > 0)
                    {
                        CommonController.CreateInstance().MessageBox(this.targetDataSourceName + "编码为：" + obj.ID + "名称为：" + obj.Name + "已经维护，请重新选择", MessageBoxIcon.Warning);
                        isSuccess = false;
                    }
                }
            }
            if (isSuccess)
            {
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "编码"), obj.ID, false);
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "名称"), obj.Name, false);
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "自定义码"), obj.UserCode, false);
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "拼音码"), obj.SpellCode, false);
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "五笔码"), obj.WBCode, false);
                this.visible(false);
            }
            else
            {
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, targetDataSourceName2 + "编码"), "", false);
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, targetDataSourceName2 + "名称"), "", false);
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "自定义码"), "", false);
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "拼音码"), "", false);
                this.sheetView2.SetValue(row, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "五笔码"), "", false);                       return -1;
            }

            return 0;
        }

        private void setLocation()
        {
            Control cell = fpSpread1.EditingControl;
            if (cell == null) return;

            int y = fpSpread1.Top + cell.Top + cell.Height + this.groupBox1.Height + 7;
            if (y <= this.Height)
            {
                if (fpSpread1.Left + cell.Left + this.groupBox1.Width + 20 <= this.Width)
                {
                    this.groupBox1.Location = new Point(fpSpread1.Left + cell.Left + 20, y - this.groupBox1.Height);
                }
                else
                {
                    this.groupBox1.Location = new Point(this.Width - this.groupBox1.Width - 10, y - this.groupBox1.Height);
                }
            }
            else
            {
                if (fpSpread1.Left + cell.Left + this.groupBox1.Width + 20 <= this.Width)
                {
                    this.groupBox1.Location = new Point(fpSpread1.Left + cell.Left + 20, fpSpread1.Top + cell.Top - this.groupBox1.Height - 7);
                }
                else
                {
                    this.groupBox1.Location = new Point(this.Width - this.groupBox1.Width - 10, fpSpread1.Top + cell.Top - this.groupBox1.Height - 7);
                }
            }
        }

        private void setNextColumn(int row,int defaultColumn,int column,int columnIndex)
        {
            if (row < this.sheetView2.RowCount)
            {
                if (column < columnIndex && this.sheetView2.ColumnCount > columnIndex)
                {
                    if (this.sheetView2.Columns[columnIndex].Width>5)
                    {
                        this.sheetView2.SetActiveCell(row, columnIndex, false);
                    }
                    else
                    {
                        this.setNextColumn(row, defaultColumn, columnIndex, columnIndex + 1);
                    }
                }
                else
                {
                    row++;
                    this.setNextColumn(row, defaultColumn, 0, defaultColumn );
                }
            }
            else
            {
                this.AddNewCompare();
                this.setNextColumn(row, defaultColumn, 0, defaultColumn );
            }
        }

        #endregion

        #endregion

        #region 重载

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.ToolBar.Clear();
            this.ToolBar.AddToolButton("添加", "添加对照信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T添加, true, false, null);
            this.ToolBar.AddToolButton("删除", "删除对照信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);

            return this.ToolBar;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text.Equals("添加"))
            {
                this.AddNewCompare();
            }
            else if (e.ClickedItem.Text.Equals("删除"))
            {
                this.DeleteCompare();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// 回车处理
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                #region enter
                if (this.fpSpread1.ContainsFocus)
                {
                    int col = this.sheetView2.ActiveColumnIndex;

                    if (col == this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "编码")
                       || col == this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "名称")
                       || col == this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "自定义码"))
                    {
                        if (this.selectOriginalDataSource() == -1)
                            return false;

                        if (this.sheetView2.Cells[this.sheetView2.ActiveRowIndex, col].Text.Length > 0)
                        {

                            this.setNextColumn(this.sheetView2.ActiveRowIndex, DefaultIndex, this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "编码"), this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "编码"));
                        }
                    }
                    else if (col == this.fpSpread1.GetColumnIndex(0,this.targetDataSourceName+"编码")
                        || col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "名称")
                        || col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "自定义码"))
                    {
                        if (this.selectTargetDataSource() == -1) 
                            return false;

                        if (this.sheetView2.Cells[this.sheetView2.ActiveRowIndex, col].Text.Length > 0)
                        {

                            this.setNextColumn(this.sheetView2.ActiveRowIndex, DefaultIndex, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "编码"), this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "扩展"));
                        }
                    }
                    else if (col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "编码")
                        || col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "名称")
                        || col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "自定义码"))
                    {
                        if (this.selectTargetDataSource2() == -1) return false;
                        if (this.sheetView2.Cells[this.sheetView2.ActiveRowIndex, col].Text.Length > 0)
                        {

                            this.setNextColumn(this.sheetView2.ActiveRowIndex, DefaultIndex, this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "编码"), this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "扩展"));
                        }
                    }
                    else
                    {
                        this.setNextColumn(this.sheetView2.ActiveRowIndex, DefaultIndex, this.sheetView2.ActiveColumnIndex, this.sheetView2.ActiveColumnIndex + 1);
                    }
               
                    return true;
                }
                #endregion

            }
            else if (keyData == Keys.Up)
            {
                #region up
                if (this.fpSpread1.ContainsFocus)
                {
                    if (this.groupBox1.Visible)
                    { this.priorRow(); }
                    else
                    {
                        int CurrentRow = this.sheetView2.ActiveRowIndex;
                        if (CurrentRow > 0)
                        {
                            sheetView2.ActiveRowIndex = CurrentRow - 1;
                            sheetView2.AddSelection(CurrentRow - 1, 0, 1, 0);
                        }
                    }
                    return true;
                }
                #endregion

            }
            else if (keyData == Keys.Down)
            {
                #region down
                if (this.fpSpread1.ContainsFocus)
                {
                    if (this.groupBox1.Visible)
                    { this.nextRow(); }
                    else
                    {
                        int CurrentRow = sheetView2.ActiveRowIndex;
                        if (CurrentRow >= 0 && CurrentRow <= sheetView2.RowCount - 2)
                        {
                            sheetView2.ActiveRowIndex = CurrentRow + 1;
                            sheetView2.AddSelection(CurrentRow + 1, 0, 1, 0);
                        }
                    }
                    return true;
                }
                #endregion

            }
            else if (keyData == Keys.Escape)
            {
                this.visible(false);
                return true;
            }
            else if (keyData == Keys.Back)
            {
                if (this.fpSpread1.ContainsFocus)
                {
                    int CurrentRow = sheetView2.ActiveRowIndex;
                    if (CurrentRow >= 0 )
                    {
                        sheetView2.Cells[CurrentRow, this.sheetView2.ActiveColumnIndex].Text = "";
                    }
                    return true;
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return base.OnSave(sender, neuObject);
        }
        #endregion

        #region 事件

        void txtOriginalFilter_TextChanged(object sender, EventArgs e)
        {
            this.FilterDtItems();
        }

        void txtOriginalFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.fpSpread1.Focus();
                this.fpSpread1.Select();
                if (this.sheetView2.RowCount > 0)
                {
                    this.sheetView2.ActiveRowIndex = 0;
                    this.sheetView2.ActiveColumnIndex = DefaultIndex;
                }
                else
                {
                    this.AddNewCompare();
                }
            }
        }

        void txtCompareFilter_TextChanged(object sender, EventArgs e)
        {
            this.FilterDtCompareItems();
        }

        void neuSpread_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.neuSpread.SaveSchema(this.settingFile1);
        }

        void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.fpSpread1.SaveSchema(this.settingFile2);
        }

        void fpSpread1_EditModeOn(object sender, EventArgs e)
        {
            this.visible(false);

            if (this.sheetView2.ActiveColumnIndex == this.fpSpread1.GetColumnIndex(0,this.originalDataSourceName+"编码")
                ||
                this.sheetView2.ActiveColumnIndex == this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "名称")
                ||
                this.sheetView2.ActiveColumnIndex == this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "自定义码")
                ||
                this.sheetView2.ActiveColumnIndex == this.fpSpread1.GetColumnIndex(0,this.targetDataSourceName+"编码")
                ||
                this.sheetView2.ActiveColumnIndex == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "名称")
                ||
                this.sheetView2.ActiveColumnIndex == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "自定义码")
                ||
                this.sheetView2.ActiveColumnIndex == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "编码")
                ||
                this.sheetView2.ActiveColumnIndex == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "名称")
                ||
                this.sheetView2.ActiveColumnIndex == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "自定义码"))
            {
                this.setLocation();
                this.visible(false);
            }
        }

        private void lbTargetDataSource_SelectItem(object sender, EventArgs e)
        {
            this.selectTargetDataSource();
            this.visible(false);

        }

        private void lbTargetDataSource2_SelectItem(object sender, EventArgs e)
        {
            this.selectTargetDataSource2();
            this.visible(false);
        }

        private void lbOriginalDataSource_SelectItem(object sender, EventArgs e)
        {
            this.selectOriginalDataSource();
            this.visible(false);
        }

        void fpSpread1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            int col = this.sheetView2.ActiveColumnIndex;
            int row = this.sheetView2.ActiveRowIndex;

            string text = this.sheetView2.ActiveCell.Text.Trim();
            text = FS.FrameWork.Public.String.TakeOffSpecialChar(text);
            if (col == this.fpSpread1.GetColumnIndex(0,this.targetDataSourceName+"编码")
                || col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "名称")
                || col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName + "自定义码"))
            {
                this.lbTargetDataSource.Filter(text);
                if (this.groupBox1.Visible == false) this.visible(true);
            }
            else if (col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "编码")
                || col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "名称")
                || col == this.fpSpread1.GetColumnIndex(0, this.targetDataSourceName2 + "自定义码"))
            {
                this.lbTargetDataSource2.Filter(text);
                if (this.groupBox1.Visible == false) this.visible(true);
            }
            else if (col == this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "编码")
                || col == this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "名称")
                || col == this.fpSpread1.GetColumnIndex(0, this.originalDataSourceName + "自定义码"))
            {
                this.lbOriginalDataSource.Filter(text);
                if (this.groupBox1.Visible == false) this.visible(true);
            }
        }

        void neuSpread_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.FilterDtCompareItems();
        }

        void cmbTargetDataSource_SelectedValueChanged(object sender, EventArgs e)
        {
            this.FilterDtCompareItems();
        }

        #endregion

        #region IPreArrange 成员

        public int PreArrange()
        {
            return 1;
        }

        #endregion

        #region IReport 成员

        public int Query()
        {
            this.InitBaseData();
            return 1;
        }

        public int SetParm(string parm, string reportID, string emplSql, string deptSql)
        {
            return 1;
        }

        public int SetParm(string parm, string reportID)
        {
            return 1;
        }

        #endregion

        #region IReportPrinter 成员

        public int Export()
        {
            return 1;
        }

        public int Print()
        {
            return 1;
        }

        public int PrintPreview()
        {
            return 1;
        }

        #endregion
    }

}
