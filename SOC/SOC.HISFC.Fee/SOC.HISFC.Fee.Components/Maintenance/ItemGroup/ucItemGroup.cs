using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.Fee.Interface.Components;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.SOC.HISFC.Fee.Components.Maintenance.Item;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.ItemGroup
{
    /// <summary>
    /// [功能描述: 物价组套基本信息查询界面]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// 说明：
    /// </summary>
     partial class ucItemGroup : FS.FrameWork.WinForms.Controls.ucBaseControl,IItemGroupQuery
    {
        public ucItemGroup()
        {
            InitializeComponent();
        }

        #region 域变量

        private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Fee.Models.Undrug> selectedItemGroupChange;
        private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.FilterTextChangeHander filterTextChanged;

        private FS.SOC.HISFC.Fee.BizLogic.Undrug itemManager = new FS.SOC.HISFC.Fee.BizLogic.Undrug();

        /// <summary>
        /// 药品基本信息的数据集
        /// </summary>
        private System.Data.DataTable dtItems = new DataTable();
        /// <summary>
        /// 存储变量
        /// </summary>
        private System.Collections.Hashtable hsItem = new System.Collections.Hashtable();

        /// <summary>
        /// Fp设置文件
        /// </summary>
        private string settingFile = "";

        private string filter = "1=1";

        private ucItem ucItem = null;
        private Form frmItem = null;
        #endregion

        #region 初始化

        private void initFarPoint()
        {
            if (!this.neuSpread.ReadSchema(this.settingFile))
            {
                this.fpItemGroup.ColumnHeader.Rows[0].Height = 34f;
            }
            this.fpItemGroup.DataAutoSizeColumns = false;
        }

        private void initBaseData()
        {
            //最小费用
            this.ncmbFeeType.AddItems(CommonController.CreateInstance().QueryConstant(FS.HISFC.Models.Base.EnumConstant.MINFEE));

            //系统类别
            this.ncmbSystemType.AddItems(FS.HISFC.Models.Base.SysClassEnumService.List());

            //复合项目
            List<FS.SOC.HISFC.Fee.Models.Undrug> lstUndrug = this.itemManager.QueryAllValidItemGroup();

            if (lstUndrug==null)
            {
                throw new Exception(this.itemManager.Err);
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载数据...");
            Application.DoEvents();
            int progress = 1;
            foreach (FS.SOC.HISFC.Fee.Models.Undrug item in lstUndrug)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(progress++, lstUndrug.Count);
                if (this.addItemObjectToDataTable(item,false) != 1)
                {
                    continue;
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            this.dtItems.AcceptChanges();
            this.fpItemGroup.DataSource = this.dtItems.DefaultView;
        }

        private void initDataTable()
        {
            if (this.dtItems == null)
            {
                this.dtItems = new DataTable();
            }

            //定义类型
            this.dtItems.Columns.AddRange(new DataColumn[] { 
                                                   new DataColumn("自定义码", typeof(string)),
                                                   new DataColumn("项目编号", typeof(string)),
                                                   new DataColumn("项目名称", typeof(string)),
                                                   new DataColumn("系统类别", typeof(string)),
                                                   new DataColumn("费用代码", typeof(string)),
                                                   new DataColumn("拼音码", typeof(string)),
                                                   new DataColumn("五笔码", typeof(string)),
                                                   new DataColumn("国家编码", typeof(string)),
                                                   new DataColumn("国际编码", typeof(string)),
                                                   new DataColumn("默认价", typeof(string)),
                                                   new DataColumn("规格", typeof(string)),
                                                   new DataColumn("执行科室", typeof(string)),
                                                   new DataColumn("适用范围",typeof(string)),
                                                   new DataColumn("物价费用类别",typeof(string))
            });


            this.setReadOnly(true);

            DataColumn[] keys = new DataColumn[1];

            keys[0] = this.dtItems.Columns["项目编号"];

            this.dtItems.PrimaryKey = keys;
            this.fpItemGroup.DataAutoSizeColumns = false;
            this.fpItemGroup.DataSource = this.dtItems.DefaultView;

        }

        private void initEvents()
        {
            this.nTxtCustomCode.TextChanged -= new EventHandler(nTxtCustomCode_TextChanged);
            this.nTxtCustomCode.TextChanged += new EventHandler(nTxtCustomCode_TextChanged);

            this.nTxtCustomCode.KeyDown -= new KeyEventHandler(nTxtCustomCode_KeyDown);
            this.nTxtCustomCode.KeyDown += new KeyEventHandler(nTxtCustomCode_KeyDown);

            this.ncmbSystemType.TextChanged -= new EventHandler(nTxtCustomCode_TextChanged);
            this.ncmbSystemType.TextChanged += new EventHandler(nTxtCustomCode_TextChanged);

            this.ncmbFeeType.SelectedIndexChanged -= new EventHandler(nTxtCustomCode_TextChanged);
            this.ncmbFeeType.SelectedIndexChanged += new EventHandler(nTxtCustomCode_TextChanged);

            this.neuSpread.SelectionChanged -= new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread_SelectionChanged);
            this.neuSpread.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread_SelectionChanged);

            this.ncbMutiQuery.CheckedChanged -= new EventHandler(ncbMutiQuery_CheckedChanged);
            this.ncbMutiQuery.CheckedChanged += new EventHandler(ncbMutiQuery_CheckedChanged);

            this.neuSpread.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread_ColumnWidthChanged);
            this.neuSpread.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread_ColumnWidthChanged);
        }

        /// <summary>
        /// 设置药品维护窗口
        /// </summary>
        private void initMaintenanceForm()
        {
            if (this.ucItem == null)
            {
                this.ucItem = new ucItem();
                this.ucItem.Dock = DockStyle.Fill;
                this.ucItem.Init();
                this.ucItem.EndSave -= new ucItem.SaveItemHandler(ucItem_EndSave);
                this.ucItem.EndSave += new ucItem.SaveItemHandler(ucItem_EndSave);
            }
            if (this.frmItem == null)
            {
                this.frmItem = new Form();
                this.frmItem.Width = this.ucItem.Width + 10;
                this.frmItem.Height = this.ucItem.Height + 25;
                this.frmItem.Text = "物价详细信息维护";
                this.frmItem.StartPosition = FormStartPosition.CenterScreen;
                this.frmItem.ShowInTaskbar = false;
                this.frmItem.HelpButton = false;
                this.frmItem.MaximizeBox = false;
                this.frmItem.MinimizeBox = false;
                this.frmItem.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.frmItem.Controls.Add(this.ucItem);
            }

        }

        #endregion

        #region 数据显示

        private int addGroupItem()
        {
            this.initMaintenanceForm();
            this.ucItem.Clear();
            FS.SOC.HISFC.Fee.Models.Undrug undrug = new FS.SOC.HISFC.Fee.Models.Undrug();
            undrug.UnitFlag = "1";
            undrug.ValidState = "1";
            this.ucItem.SetItem(undrug, true);
            this.ucItem.IsGroup = true;
            this.frmItem.ShowDialog();

            return 1;
        }

        private int modifyGroupItem()
        {
            this.initMaintenanceForm();
            this.ucItem.IsGroup = true;

            string undrugNO = this.neuSpread.GetCellText(0, this.fpItemGroup.ActiveRowIndex, "项目编号");
            if (this.hsItem.Contains(undrugNO))
            {
                this.ucItem.SetItem(hsItem[undrugNO] as FS.SOC.HISFC.Fee.Models.Undrug, false);
                this.frmItem.ShowDialog();
            }

            return 1;
        }

        private int addItemObjectToDataTable(FS.SOC.HISFC.Fee.Models.Undrug item,bool isModify)
        {
            if (item == null)
            {
                CommonController.CreateInstance().MessageBox("向DataTable中添加物价复合项目基本信息失败：物价基本信息为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
            if (this.dtItems == null)
            {
                CommonController.CreateInstance().MessageBox("向DataTable中添加物价复合项目基本信息失败：DataTable为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }

            DataRow row = null;


            if (this.hsItem.Contains(item.ID))
            {
                if (isModify)
                {
                    row = this.dtItems.Rows.Find(item.ID);
                }
                else
                {
                    CommonController.CreateInstance().MessageBox("编码：" + item.ID + " 名称：" + item.Name + " 已经重复！", System.Windows.Forms.MessageBoxIcon.Information);
                    return -1;
                }
            }
            else
            {
                this.hsItem.Add(item.ID, item);
                row = this.dtItems.NewRow();
            }
            this.setReadOnly(false);
            this.setDataRowValue(item, row);
            this.setReadOnly(true);
            if (row.RowState == DataRowState.Detached)
            {
                this.dtItems.Rows.Add(row);
            }
            return 1;
        }

        private int setDataRowValue(FS.SOC.HISFC.Fee.Models.Undrug item, DataRow row)
        {
            row["项目编号"] = item.ID;
            row["项目名称"] = item.Name;
            row["系统类别"] = item.SysClass.Name;
            row["执行科室"] = CommonController.CreateInstance().GetDepartmentName(item.ExecDept);
            row["费用代码"] = CommonController.CreateInstance().GetConstantName(FS.HISFC.Models.Base.EnumConstant.MINFEE, item.MinFee.ID);//
            row["自定义码"] = item.UserCode;
            row["拼音码"] = item.SpellCode;
            row["五笔码"] = item.WBCode;
            row["国家编码"] = item.GBCode;
            row["国际编码"] = item.NationCode;
            row["默认价"] = item.Price.ToString("F4");
            row["规格"] = item.Specs;
            row["适用范围"] = CommonController.CreateInstance().GetConstantName("APPLICABILITYAREA", item.ApplicabilityArea);
            row["物价费用类别"] = item.ItemPriceType;

            return 1;
        }
        private void setReadOnly(bool readOnly)
        {

            foreach (DataColumn dc in this.dtItems.Columns)
            {
                dc.ReadOnly = readOnly;
            }
        }

        private void selectItemChange()
        {
            string drugNO = this.neuSpread.GetCellText(0, this.fpItemGroup.ActiveRowIndex, "项目编号");
            if (this.hsItem.Contains(drugNO))
            {
                if (this.selectedItemGroupChange != null)
                {
                    this.selectedItemGroupChange(this.hsItem[drugNO] as FS.SOC.HISFC.Fee.Models.Undrug);
                }
            }
        }

        #endregion

        #region 过滤

        private void filterItem()
        {
            this.filter = Function.GetFilterStr(this.dtItems.DefaultView, "%" + this.nTxtCustomCode.Text.Trim() + "%");
            filter = "(" + filter + ")";
            //增加系统类别，费用类别，有效性，组套
            if (this.dtItems.Columns.Contains("系统类别"))
            {
                filter += string.Format(" and 系统类别 like '%{0}%'", this.ncmbSystemType.Text.Trim());
            }

            if (this.dtItems.Columns.Contains("费用代码"))
            {
                filter += string.Format(" and 费用代码 like '%{0}%'", this.ncmbFeeType.Text.Trim());
            }

            this.dtItems.DefaultView.RowFilter = filter;
            this.neuSpread.ReadSchema(this.settingFile);

            if (this.filterTextChanged != null)
            {
                this.filterTextChanged();
            }

        }

        #endregion

        #region 事件触发

        void nTxtCustomCode_TextChanged(object sender, EventArgs e)
        {
            this.filterItem();
        }

        void nTxtCustomCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.fpItemGroup.RowCount > 0 && this.fpItemGroup.ActiveRowIndex >= 0)
                {
                    this.selectItemChange();
                }
            }
        }

        void neuSpread_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (e.View.Sheets[0].ActiveRowIndex >= 0)
            {
                this.selectItemChange();
            }
        }

        void ncbMutiQuery_CheckedChanged(object sender, EventArgs e)
        {
            this.ngbQuerySet.Visible = this.ncbMutiQuery.Checked;
        }

        void neuSpread_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.neuSpread.SaveSchema(this.settingFile);
        }

        void ucItem_EndSave(FS.SOC.HISFC.Fee.Models.Undrug item)
        {
            if (this.hsItem.ContainsKey(item.ID))
            {
                this.addItemObjectToDataTable(item, true);
            }
            else
            {
                this.addItemObjectToDataTable(item, true);
                this.neuSpread.ShowRow(0, this.fpItemGroup.RowCount - 1, FarPoint.Win.Spread.VerticalPosition.Top);
                this.fpItemGroup.AddSelection(this.fpItemGroup.RowCount - 1, 0, 1, 1);
            }

        }

        #endregion

        #region IItemGroupQuery 成员

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Fee.Models.Undrug> SelectedItemGroupChange
        {
            get
            {
                return selectedItemGroupChange;
            }
            set
            {
                selectedItemGroupChange = value;
            }
        }

        #endregion

        #region IDataDetail 成员

        public int Clear()
        {
            this.nTxtCustomCode.Text = "";
            this.ncmbFeeType.Tag = "";
            this.ncmbSystemType.Tag = "";
            return 1;
        }

        public string Filter
        {
            set { this.filter = value; }
        }

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.FilterTextChangeHander FilterTextChanged
        {
            get
            {
                return filterTextChanged;
            }
            set
            {
                filterTextChanged = value;
            }
        }

        public FS.SOC.Windows.Forms.FpSpread FpSpread
        {
            get { return this.neuSpread; }
        }

        public string Info
        {
            set { }
        }

        public int Init()
        {
            try
            {
                this.initEvents();
                this.initDataTable();
                this.initBaseData();
                this.initFarPoint();
            }
            catch (Exception ex)
            {
                CommonController.CreateInstance().MessageBox("初始化失败，请系统管理员报告错误：" + ex.Message, MessageBoxIcon.Information);
                return -1;
            }

            return 1;
        }

        public bool IsContainsFocus
        {
            get { return this.neuSpread.Focused; }
        }

        public int SetFocus()
        {
            //更新当前价格
            if (this.fpItemGroup.ActiveRowIndex >= 0)
            {
                string drugNO = this.neuSpread.GetCellText(0, this.fpItemGroup.ActiveRowIndex, "项目编号");
                if (this.hsItem.Contains(drugNO))
                {
                    FS.SOC.HISFC.Fee.Models.Undrug undrug = this.hsItem[drugNO] as FS.SOC.HISFC.Fee.Models.Undrug;
                    this.dtItems.Columns["默认价"].ReadOnly = false;
                    this.neuSpread.SetCellValue(0, this.fpItemGroup.ActiveRowIndex, "默认价", undrug.Price.ToString("F4"));
                    this.dtItems.Columns["默认价"].ReadOnly = true;
                    if (this.selectedItemGroupChange != null)
                    {
                        this.selectedItemGroupChange(undrug);
                    }
                }
            }

            return 1;
        }

        public int SetFocusToFilter()
        {
            this.nTxtCustomCode.Select();
            this.nTxtCustomCode.Focus();
            return 1;
        }

        public string SettingFileName
        {
            set { this.settingFile = value; }
        }

        #endregion

        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            itemManager.Dispose();
            neuSpread.Dispose();
            dtItems.Dispose();
            hsItem.Clear();
            this.nTxtCustomCode.TextChanged -= new EventHandler(nTxtCustomCode_TextChanged);
            this.ncmbFeeType.SelectedIndexChanged -= new EventHandler(nTxtCustomCode_TextChanged);
            this.ncmbSystemType.SelectedIndexChanged -= new EventHandler(nTxtCustomCode_TextChanged);
        }

        #endregion


        #region IItemGroupQuery 成员


        public int AddGroupItem()
        {
            return this.addGroupItem();
        }

        public int ModifyGroupItem()
        {
            return this.modifyGroupItem();
        }

        #endregion
    }
}
