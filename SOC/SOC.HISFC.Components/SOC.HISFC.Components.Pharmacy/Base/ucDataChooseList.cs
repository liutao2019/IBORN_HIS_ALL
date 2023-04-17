using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Pharmacy.Base
{
    /// <summary>
    /// [功能描述: FarPoint数据选择]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-3]<br></br>
    /// </summary>
    public partial class ucDataChooseList : UserControl, SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataChooseList
    {
        public ucDataChooseList()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 获取数据集的SQL，药品类别变化时提供可Format的SQL查询待选数据集
        /// </summary>
        private string curSQL = "";

        /// <summary>
        /// 是否显示药品类别
        /// </summary>
        private bool isShowDrugType = false;

        /// <summary>
        /// 当前列的CellType,保留格式便于过滤或刷新数据后重新Format
        /// </summary>
        private FarPoint.Win.Spread.CellType.BaseCellType[] curCellTypes = null;

        /// <summary>
        /// 当前列的Label,保留格式便于过滤或刷新数据后重新Format
        /// </summary>
        private string[] curColumnLabels = null;

        /// <summary>
        /// 当前列的With,保留格式便于过滤或刷新数据后重新Format
        /// </summary>
        private float[] curColumnWiths = null;

        /// <summary>
        /// 录入信息完成事件（函数指针定义）
        /// 目前外部实现：FS.SOC.HISFC.Components.Pharmacy.Common.Input.CommonInput
        /// </summary>
        private FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander chooseCompletedEvent;

        /// <summary>
        /// 过滤字符串
        /// </summary>
        private string curFilter = "";

        /// <summary>
        /// 配置文件
        /// </summary>
        private string curSettingFileName = "";

        #endregion       

        #region IDataChooseList的成员

        
        public FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.ChooseCompletedHander ChooseCompletedEvent
        {
            get
            {
                return chooseCompletedEvent;
            }
            set
            {
                chooseCompletedEvent = value;
            }
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        public void Clear()
        {
            this.neuDataListSpread_Sheet1.RowCount = 0;
            this.neuDataListSpread_Sheet1.ColumnCount = 0;
            this.neuDataListSpread_Sheet1.DataSource = null;

            this.ntxtFilter.Text = "";

            this.nlbDrugType.Visible = false;
            this.ncmbDrugType.Visible = false;
        }

        /// <summary>
        /// 清空数据选择项
        /// </summary>
        public void ClearFilter()
        {
            this.ntxtFilter.Text = "";
        }

        /// <summary>
        /// 初始化设置
        /// </summary>
        public void Init()
        {
            this.ntxtFilter.KeyDown -= new KeyEventHandler(ntxtFilter_KeyDown);
            this.ntxtFilter.KeyDown += new KeyEventHandler(ntxtFilter_KeyDown);

            this.ntxtFilter.KeyPress -= new KeyPressEventHandler(ntxtFilter_KeyPress);
            this.ntxtFilter.KeyPress += new KeyPressEventHandler(ntxtFilter_KeyPress);

            this.ntxtFilter.TextChanged -= new EventHandler(ntxtFilter_TextChanged);
            this.ntxtFilter.TextChanged += new EventHandler(ntxtFilter_TextChanged);

            this.neuDataListSpread_Sheet1.ColumnHeader.Rows[0].Height = 26F;
            this.neuDataListSpread_Sheet1.DefaultStyle.Locked = true;
            this.neuDataListSpread_Sheet1.Rows.Default.Height = 24F;
            this.neuDataListSpread.CellDoubleClick -= new FarPoint.Win.Spread.CellClickEventHandler(neuDataListSpread_CellDoubleClick);
            this.neuDataListSpread.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(neuDataListSpread_CellDoubleClick);
            this.neuDataListSpread.KeyUp -= new KeyEventHandler(neuDataListSpread_KeyUp);
            this.neuDataListSpread.KeyUp += new KeyEventHandler(neuDataListSpread_KeyUp);
            this.nllReset.LinkClicked -= new LinkLabelLinkClickedEventHandler(nllReset_LinkClicked);
            this.nllReset.LinkClicked += new LinkLabelLinkClickedEventHandler(nllReset_LinkClicked);

            SOC.HISFC.BizProcess.Cache.Common.InitDrugType();
            System.Collections.ArrayList alDrugType = new System.Collections.ArrayList();
            FS.HISFC.Models.Base.Const drugTypeConst = new FS.HISFC.Models.Base.Const();
            drugTypeConst.ID = "All";
            drugTypeConst.Name = "全部";
            drugTypeConst.SpellCode = "QB";
            drugTypeConst.WBCode = "WU";
            alDrugType.Add(drugTypeConst);
            alDrugType.AddRange(SOC.HISFC.BizProcess.Cache.Common.drugTypeHelper.ArrayObject);

            this.ncmbDrugType.AddItems(alDrugType);
            this.ncmbDrugType.SelectedIndexChanged += new EventHandler(ncmbDrugType_SelectedIndexChanged);
            this.ncmbDrugType.KeyPress -= new KeyPressEventHandler(ncmbDrugType_KeyPress);
            this.ncmbDrugType.KeyPress += new KeyPressEventHandler(ncmbDrugType_KeyPress);

            this.neuDataListSpread.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuDataListSpread_ColumnWidthChanged);
            this.neuDataListSpread.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuDataListSpread_ColumnWidthChanged);

            this.Dock = DockStyle.Fill;
        }

        void nllReset_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (System.IO.File.Exists(this.curSettingFileName))
            {
                System.IO.File.Delete(this.curSettingFileName);
            }

            string filter = this.ntxtFilter.Text;
            string r = (new Random()).Next().ToString();
            this.ntxtFilter.Text = r;
            this.ntxtFilter.Text = filter;
            this.ReSetFormat();
        }


        /// <summary>
        /// 显示数据
        /// </summary>
        /// <param name="title">TabPages[0]标题</param>
        /// <param name="tabPageText">供用户选择的数据集,这个数据集不需要区(药品)分类别</param>
        /// <returns>-1 发生错误</returns>
        public int ShowChooseList(string title, DataSet dataSet, string filter)
        {
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                Function.ShowMessage("显示选择列表发生错误：没有数据集。请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }

            if (string.IsNullOrEmpty(title))
            {
                this.ngbChooseList.Text = "选择列表";
            }
            else 
            {
                this.ngbChooseList.Text = title;
            }

            this.curFilter = filter;
            this.neuDataListSpread_Sheet1.DataSource = dataSet.Tables[0].DefaultView;

            return 1;
        }

        public string DeptID { set; get; }

        /// <summary>
        /// 显示数据
        /// </summary>
        /// <param name="title">TabPages[0]标题</param>
        /// <param name="SQL">获取供用户选择的数据集的SQL</param>
        /// <param name="isFormatDrugType">是否Format药品类别</param>
        /// <returns></returns>
        public int ShowChooseList(string title, string SQL, bool isFormatDrugType, string filter)
        {
            if (string.IsNullOrEmpty(SQL))
            {
                Function.ShowMessage("显示选择列表发生错误：获取数据的SQL为空。请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }

            this.curSQL = SQL;
            this.curFilter = filter;
            this.isShowDrugType = isFormatDrugType;
            this.nlbDrugType.Visible = isFormatDrugType;
            this.ncmbDrugType.Visible = isFormatDrugType;

            string drugType = "All";
            if (isFormatDrugType && this.ncmbDrugType.Tag != null && !string.IsNullOrEmpty(this.ncmbDrugType.Tag.ToString()))
            {
                drugType = this.ncmbDrugType.Tag.ToString();               
            }
            //SQL不提供药品类别参数也可以执行
            SQL = string.Format(SQL, drugType);

            FS.SOC.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();
            DataSet ds = new DataSet();
            if (itemMgr.ExecQuery(SQL, ref ds) == -1)
            {
                Function.ShowMessage("显示选择列表发生错误：" + itemMgr.Err, MessageBoxIcon.Error);
                return -1;
            }
            return this.ShowChooseList(this.ngbChooseList.Text, ds, filter);
        }


        /// <summary>
        /// 显示数据  //{5DDC1B83-0693-4949-93A0-98FAC0630510}添加科室过滤取左侧药品信息
        /// </summary>
        /// <param name="title">TabPages[0]标题</param>
        /// <param name="SQL">获取供用户选择的数据集的SQL</param>
        /// <param name="isFormatDrugType">是否Format药品类别</param>
        /// <returns></returns>
        public int ShowChooseList1(string title, string SQL, bool isFormatDrugType, string filter)
        {
            if (string.IsNullOrEmpty(SQL))
            {
                Function.ShowMessage("显示选择列表发生错误：获取数据的SQL为空。请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }

            this.curSQL = SQL;
            this.curFilter = filter;
            this.isShowDrugType = isFormatDrugType;
            this.nlbDrugType.Visible = isFormatDrugType;
            this.ncmbDrugType.Visible = isFormatDrugType;

            string drugType = "All";
            if (isFormatDrugType && this.ncmbDrugType.Tag != null && !string.IsNullOrEmpty(this.ncmbDrugType.Tag.ToString()))
            {
                drugType = this.ncmbDrugType.Tag.ToString();
            }
            //SQL不提供药品类别参数也可以执行
            SQL = string.Format(SQL, drugType, DeptID);

            FS.SOC.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();
            DataSet ds = new DataSet();
            if (itemMgr.ExecQuery(SQL, ref ds) == -1)
            {
                Function.ShowMessage("显示选择列表发生错误：" + itemMgr.Err, MessageBoxIcon.Error);
                return -1;
            }
            return this.ShowChooseList(this.ngbChooseList.Text, ds, filter);
        }

        /// <summary>
        /// 根据上次使用的SQL重新获取数据集，用于用户改变药品类别后或者需要刷新数据时
        /// </summary>
        /// <returns></returns>
        public int ReShowChooseList()
        {
            return this.ShowChooseList(this.ngbChooseList.Text, this.curSQL, this.isShowDrugType, this.curFilter);
        }

        /// <summary>
        /// 设置显示格式(格式化FarPoint)
        /// </summary>
        /// <param name="cellTypes">列的单元格显示类型</param>
        /// <param name="columnLabels">列的标题</param>
        /// <param name="columnWiths">列的宽度</param>
        /// <returns>-1 发生错误</returns>
        public int SetFormat(FarPoint.Win.Spread.CellType.BaseCellType[] cellTypes, string[] columnLabels, float[] columnWiths)
        {
            if (System.IO.File.Exists(curSettingFileName))
            {
                this.neuDataListSpread.ReadSchema(curSettingFileName);
                return 1;
            }

            int columnIndex = 0;
            if (cellTypes != null)
            {
                this.curCellTypes = cellTypes;
                foreach (FarPoint.Win.Spread.CellType.BaseCellType cellType in cellTypes)
                {
                    if (columnIndex > this.neuDataListSpread_Sheet1.ColumnCount - 1)
                    {
                        break;
                    }
                    this.neuDataListSpread_Sheet1.Columns[columnIndex].CellType = cellType;
                    columnIndex++;
                }

            }
            if (columnLabels != null)
            {
                this.curColumnLabels = columnLabels;
                columnIndex = 0;
                foreach (string label in columnLabels)
                {
                    if (columnIndex > this.neuDataListSpread_Sheet1.ColumnCount - 1)
                    {
                        break;
                    }
                    this.neuDataListSpread_Sheet1.Columns[columnIndex].Label = label;
                    columnIndex++;
                }

            }
            if (columnWiths != null)
            {
                this.curColumnWiths = columnWiths;
                columnIndex = 0;
                foreach (float with in columnWiths)
                {
                    if (columnIndex > this.neuDataListSpread_Sheet1.ColumnCount - 1)
                    {
                        break;
                    }
                    this.neuDataListSpread_Sheet1.Columns[columnIndex].Width = with;
                    columnIndex++;
                }
            }
            return 1;
        }

        /// <summary>
        /// 根据上次使用的CellType,Label,With集合重新设置FarPoint，用于用户改变药品类别后或者需要刷新数据时
        /// </summary>
        /// <returns></returns>
        public int ReSetFormat()
        {           
            return this.SetFormat(this.curCellTypes, this.curColumnLabels, this.curColumnWiths);
        }

        /// <summary>
        /// 获取选择的数据
        /// </summary>
        /// <param name="columns">需要获取数据的列索引</param>
        /// <returns></returns>
        public string[] GetChooseData(int[] columns)
        {
            string[] values =new string[columns.Length];
            int rowIndex = this.neuDataListSpread_Sheet1.ActiveRowIndex;

            for (int index = 0; index < columns.Length; index++)
            {
                int colIndex = columns[index];
                values[index] = this.neuDataListSpread_Sheet1.Cells[rowIndex, colIndex].Text;
            }
            return values;
        }

        /// <summary>
        /// 将焦点定位到过滤框
        /// </summary>
        public void SetFocusToFilter()
        {
            this.ntxtFilter.Select();
            this.ntxtFilter.SelectAll();
            this.ntxtFilter.Focus();
            this.ntxtFilter.Text = string.Empty;
        }

        #endregion       
        
        #region IDataChooseList 成员 配置文件

        public string SettingFileName
        {
            get
            {
                return this.curSettingFileName;
            }
            set
            {
                this.curSettingFileName = value;
            }
        }

      //  public string DeptID { set; get; }

        #endregion

        #region 事件


        void ntxtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(this.ntxtFilter.Text))
                {
                    this.SetFocusToFilter();
                    return;
                }
                if (this.ChooseCompletedEvent != null)
                {
                    this.ChooseCompletedEvent();
                }
            }
        }  

        void ntxtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (this.neuDataListSpread_Sheet1.ActiveRowIndex < this.neuDataListSpread_Sheet1.RowCount - 1)
                {
                    this.neuDataListSpread_Sheet1.ActiveRowIndex++;
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (this.neuDataListSpread_Sheet1.ActiveRowIndex > 0)
                {
                    this.neuDataListSpread_Sheet1.ActiveRowIndex--;
                }
            }
        }


        void ntxtFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.neuDataListSpread_Sheet1.DataSource is DataView && !string.IsNullOrEmpty(this.curFilter))
                {
                    string f = string.Format(this.curFilter, this.ntxtFilter.Text);
                    ((DataView)this.neuDataListSpread_Sheet1.DataSource).RowFilter = f;
                    this.ReSetFormat();
                }
            }
            catch (Exception ex)
            {
                Function.ShowMessage("数据选择过滤发生错误：" + ex.Message, MessageBoxIcon.Error);
            }
        }

        void ncmbDrugType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ReShowChooseList();
            this.ReSetFormat();
        }

        void neuDataListSpread_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.ChooseCompletedEvent != null)
            {
                if (this.neuDataListSpread_Sheet1.ActiveRowIndex != e.Row)
                {
                    this.neuDataListSpread_Sheet1.ActiveRowIndex = e.Row; 
                }
                this.ChooseCompletedEvent();
            }
        }

        void neuDataListSpread_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ChooseCompletedEvent != null)
                {
                    this.ChooseCompletedEvent();
                }
            }
        }

        void ncmbDrugType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.ntxtFilter.Select();
                this.ntxtFilter.SelectAll();
                this.ntxtFilter.Focus();
            }
        }


        void neuDataListSpread_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.neuDataListSpread.SaveSchema(this.curSettingFileName);
        }
           
        #endregion

    }
}
