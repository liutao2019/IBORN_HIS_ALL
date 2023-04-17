using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Pharmacy.Base
{
    public partial class frmBillChooseList : Form
    {
        public frmBillChooseList()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 获取数据集的SQL，药品类别变化时提供可Format的SQL查询待选数据集
        /// </summary>
        private string curSQL = "";

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
        /// 配置文件
        /// </summary>
        private string curSettingFileName = "";

        ArrayList curStates = null;

        #endregion


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
        private void Clear()
        {
            this.neuDataListSpread_Sheet1.RowCount = 0;
            this.neuDataListSpread_Sheet1.ColumnCount = 0;
            this.neuDataListSpread_Sheet1.DataSource = null;

        }

        /// <summary>
        /// 初始化设置
        /// </summary>
        public void Init()
        {
           
            this.neuDataListSpread_Sheet1.ColumnHeader.Rows[0].Height = 26F;
            this.neuDataListSpread_Sheet1.DefaultStyle.Locked = true;
            this.neuDataListSpread_Sheet1.Rows.Default.Height = 24F;
            this.neuDataListSpread.CellDoubleClick -= new FarPoint.Win.Spread.CellClickEventHandler(neuDataListSpread_CellDoubleClick);
            this.neuDataListSpread.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(neuDataListSpread_CellDoubleClick);
            this.neuDataListSpread.KeyUp -= new KeyEventHandler(neuDataListSpread_KeyUp);
            this.neuDataListSpread.KeyUp += new KeyEventHandler(neuDataListSpread_KeyUp);

            this.neuDataListSpread.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuDataListSpread_ColumnWidthChanged);
            this.neuDataListSpread.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuDataListSpread_ColumnWidthChanged);

            this.nbtQuery.Click -= new EventHandler(nbtQuery_Click);
            this.nbtQuery.Click += new EventHandler(nbtQuery_Click);

            this.nbtOK.Click -= new EventHandler(nbtOK_Click);
            this.nbtOK.Click += new EventHandler(nbtOK_Click);

            this.nbtCancel.Click -= new EventHandler(nbtCancel_Click);
            this.nbtCancel.Click += new EventHandler(nbtCancel_Click);

            FS.FrameWork.Management.ControlParam controler = new FS.FrameWork.Management.ControlParam();            
            string controlCode = controler.QueryControlerInfo("P02000");
            if (string.IsNullOrEmpty(controlCode) || (controlCode == "-1"))
            {
                this.neuDateTimePicker1.Value = DateTime.Now.Date.AddDays(-30);
                this.neuDateTimePicker2.Value = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            }
            else
            {
                this.neuDateTimePicker1.Value = FS.FrameWork.Function.NConvert.ToDateTime(controlCode);
                this.neuDateTimePicker2.Value = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            }

            this.Clear();
        }

        void nbtCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void nbtOK_Click(object sender, EventArgs e)
        {
            if (this.neuDataListSpread_Sheet1.ActiveRowIndex > -1)
            {
                if (this.ChooseCompletedEvent != null)
                {
                    this.ChooseCompletedEvent();
                    this.Close();
                }
            }
        }

        void nbtQuery_Click(object sender, EventArgs e)
        {
            this.Query();
        }

        /// <summary>
        /// 显示数据
        /// </summary>
        /// <param name="title">TabPages[0]标题</param>
        /// <param name="tabPageText">供用户选择的数据集,这个数据集不需要区(药品)分类别</param>
        /// <returns>-1 发生错误</returns>
        private int Query()
        {
            this.Clear();

            FS.SOC.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();
            DataSet dataSet = new DataSet();

            string state = "All";
            if (this.ncmbState.Tag != null && !string.IsNullOrEmpty(this.ncmbState.Tag.ToString()))
            {
                state = this.ncmbState.Tag.ToString();
            }
            string SQL = string.Format(this.curSQL, this.neuDateTimePicker1.Value.ToString(), this.neuDateTimePicker2.Value.ToString(),state);
            
            if (itemMgr.ExecQuery(SQL, ref dataSet) == -1)
            {
                Function.ShowMessage("显示选择列表发生错误：" + itemMgr.Err, MessageBoxIcon.Error);
                return -1;
            }
           
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                Function.ShowMessage("显示选择列表发生错误：没有数据集。请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }


            this.neuDataListSpread_Sheet1.DataSource = dataSet.Tables[0].DefaultView;

            this.SetFormat();

            return 1;
        }

        /// <summary>
        /// 设置显示格式(格式化FarPoint)
        /// </summary>
        /// <param name="cellTypes">列的单元格显示类型</param>
        /// <param name="columnLabels">列的标题</param>
        /// <param name="columnWiths">列的宽度</param>
        /// <returns>-1 发生错误</returns>
        public int Set(string title, string SQL, ArrayList alState, string settingFileName, FarPoint.Win.Spread.CellType.BaseCellType[] cellTypes, string[] columnLabels, float[] columnWiths)
        {
            if (string.IsNullOrEmpty(SQL))
            {
                Function.ShowMessage("显示选择列表发生错误：获取数据的SQL为空。请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }

            this.curSQL = SQL;


            if (!string.IsNullOrEmpty(title))
            {
                this.Text = title;
            }

            if (alState == null || alState.Count == 0)
            {
                this.ncmbState.Enabled = false;
            }
            else
            {
                this.ncmbState.AddItems(alState);
                this.ncmbState.Enabled = true;
                this.ncmbState.SelectedIndex = 0;
            }
            if (columnLabels != null)
            {
                this.neuDataListSpread_Sheet1.ColumnCount = columnLabels.Length;
            }
            this.curCellTypes = cellTypes;
            this.curColumnLabels = columnLabels;
            this.curColumnWiths = columnWiths;
            this.curSettingFileName = settingFileName;

            this.SetFormat();

            return 1;
        }

        /// <summary>
        /// 根据上次使用的CellType,Label,With集合重新设置FarPoint，用于用户改变药品类别后或者需要刷新数据时
        /// </summary>
        /// <returns></returns>
        private int SetFormat()
        {
            if (System.IO.File.Exists(curSettingFileName))
            {
                this.neuDataListSpread.ReadSchema(curSettingFileName);
                return 1;
            }

            int columnIndex = 0;

            if (curColumnLabels != null)
            {
                columnIndex = 0;
                foreach (string label in curColumnLabels)
                {
                    if (columnIndex > this.neuDataListSpread_Sheet1.ColumnCount - 1)
                    {
                        break;
                    }
                    this.neuDataListSpread_Sheet1.Columns[columnIndex].Label = label;
                    columnIndex++;
                }

            }
            if (curColumnWiths != null)
            {
                columnIndex = 0;
                foreach (float with in curColumnWiths)
                {
                    if (columnIndex > this.neuDataListSpread_Sheet1.ColumnCount - 1)
                    {
                        break;
                    }
                    this.neuDataListSpread_Sheet1.Columns[columnIndex].Width = with;
                    columnIndex++;
                }
            }
            if (curCellTypes != null)
            {
                foreach (FarPoint.Win.Spread.CellType.BaseCellType cellType in curCellTypes)
                {
                    if (columnIndex > this.neuDataListSpread_Sheet1.ColumnCount - 1)
                    {
                        break;
                    }
                    this.neuDataListSpread_Sheet1.Columns[columnIndex].CellType = cellType;
                    columnIndex++;
                }

            }

            return 1;
        }

        /// <summary>
        /// 获取选择的数据
        /// </summary>
        /// <param name="columns">需要获取数据的列索引</param>
        /// <returns></returns>
        public string[] GetChooseData(int[] columns)
        {
            string[] values = new string[columns.Length];
            int rowIndex = this.neuDataListSpread_Sheet1.ActiveRowIndex;

            for (int index = 0; index < columns.Length; index++)
            {
                int colIndex = columns[index];
                values[index] = this.neuDataListSpread_Sheet1.Cells[rowIndex, colIndex].Text;
            }
            return values;
        }      

        #region 事件

        void neuDataListSpread_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.ChooseCompletedEvent != null)
            {
                if (this.neuDataListSpread_Sheet1.ActiveRowIndex != e.Row)
                {
                    this.neuDataListSpread_Sheet1.ActiveRowIndex = e.Row;
                }
                this.ChooseCompletedEvent();
                this.Close();
            }
        }

        void neuDataListSpread_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ChooseCompletedEvent != null)
                {
                    this.ChooseCompletedEvent();
                    this.Close();
                }
            }
        }

        void neuDataListSpread_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.neuDataListSpread.SaveSchema(this.curSettingFileName);
        }

        #endregion
    }
}
