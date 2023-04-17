using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Common.Base
{
    public partial class ucDataDetail : UserControl, SOC.HISFC.BizProcess.CommonInterface.IDataDetail
    {
        public ucDataDetail()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 当前FarPoint使用的配置文件
        /// </summary>
        private string curSettingFileName = "";

        /// <summary>
        /// 当前使用的过滤串
        /// </summary>
        private string curFilter = "";

        FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.FilterTextChangeHander curFilterTextChanged;

        #region IDataDetail 成员

        public string Filter
        {
            set
            {
                this.curFilter = value;
            }
        }

        [Browsable(false)]
        public SOC.Windows.Forms.FpSpread FpSpread
        {
            get
            {
                return this.neuDataListSpread;
            }
        }

        public string SettingFileName
        {
            set
            {
                curSettingFileName = value;
            }
        }

        public string Info
        {
            set
            {
                this.nlbInfo.Text = value;
            }
        }

        public int Init() 
        {
            this.neuDataListSpread_Sheet1.Rows.Default.Height = 24F;
            this.neuDataListSpread_Sheet1.ColumnHeader.Rows.Default.Height = 28F;

            this.Dock = DockStyle.Fill;
            this.ntxtFilter.TextChanged -= new EventHandler(ntxtFilter_TextChanged);
            this.ntxtFilter.TextChanged += new EventHandler(ntxtFilter_TextChanged);
            this.neuDataListSpread.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread_ColumnWidthChanged);
            this.neuDataListSpread.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread_ColumnWidthChanged);

            //屏蔽回车键
            FarPoint.Win.Spread.InputMap im;
            im = this.neuDataListSpread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            
            return 1;
        }

        public int Clear()
        {
            this.nlbInfo.Text = "";
            this.ntxtFilter.Text = "";
            this.neuDataListSpread_Sheet1.RowCount = 0;
            this.neuDataListSpread_Sheet1.ColumnCount = 0;

            return 1;
        }

        public int SetFocus()
        {
            if (!this.neuDataListSpread.ContainsFocus)
            {
                this.neuDataListSpread.Select();
                this.neuDataListSpread.Focus();
            }
            return this.SetFocusToNextWriteColumn();
        }

        public bool IsContainsFocus
        {
            get { return this.ContainsFocus; }
        }

        public int SetFocusToFilter()
        {
            this.ntxtFilter.Select();
            this.ntxtFilter.SelectAll();
            this.ntxtFilter.Focus();
            return 1;
        }


        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.FilterTextChangeHander FilterTextChanged
        {
            get { return curFilterTextChanged; }
            set { curFilterTextChanged = value; }
        }

        #endregion


        private bool CheckWrite(int columnIndex)
        {
            if (this.neuDataListSpread_Sheet1.DataSource is DataView)
            {
                DataView dv = this.neuDataListSpread_Sheet1.DataSource as DataView;
                if (dv.Table.Columns[columnIndex].ReadOnly)
                {
                    return false;
                }
            }
            if (this.neuDataListSpread_Sheet1.Columns[columnIndex].CellType is FarPoint.Win.Spread.CellType.TextCellType)
            {
                if (!((FarPoint.Win.Spread.CellType.TextCellType)this.neuDataListSpread_Sheet1.Columns[columnIndex].CellType).ReadOnly)
                {
                    return true;
                }

            }
            else if (this.neuDataListSpread_Sheet1.Columns[columnIndex].CellType is FarPoint.Win.Spread.CellType.NumberCellType)
            {
                if (!((FarPoint.Win.Spread.CellType.NumberCellType)this.neuDataListSpread_Sheet1.Columns[columnIndex].CellType).ReadOnly)
                {
                    return true;
                }
            }
            else if (this.neuDataListSpread_Sheet1.Columns[columnIndex].CellType is FarPoint.Win.Spread.CellType.DateTimeCellType)
            {
                if (!((FarPoint.Win.Spread.CellType.DateTimeCellType)this.neuDataListSpread_Sheet1.Columns[columnIndex].CellType).ReadOnly)
                {
                    return true;
                }
            }

            return false;
        }

        private int FindFirstWriteColumn()
        {
            for (int columnIndex = 0; columnIndex < this.neuDataListSpread_Sheet1.ColumnCount; columnIndex++)
            {
                if (this.CheckWrite(columnIndex) && this.neuDataListSpread_Sheet1.Columns[columnIndex].Width > 1)
                {
                    return columnIndex;
                }
            }
            return -1;
        }

        private int FindLastWriteColumn()
        {
            for (int columnIndex = this.neuDataListSpread_Sheet1.ColumnCount-1; columnIndex > -1; columnIndex--)
            {
                if (this.CheckWrite(columnIndex) && this.neuDataListSpread_Sheet1.Columns[columnIndex].Width > 1)
                {
                    return columnIndex;
                }
            }
            return -1;
        }

        private int SetFocusToNextWriteColumn()
        {
            int activeRow = this.neuDataListSpread_Sheet1.ActiveRowIndex;
            int activeColumn = this.neuDataListSpread_Sheet1.ActiveColumnIndex;

            if (activeRow < 0)
            {
                activeRow = 0;
                this.neuDataListSpread_Sheet1.SetActiveCell(activeRow, this.FindFirstWriteColumn());
                return 1;
            }

            if (activeColumn == this.neuDataListSpread_Sheet1.ColumnCount || activeColumn == this.FindLastWriteColumn())
            {
                if (activeRow + 1 > this.neuDataListSpread_Sheet1.RowCount - 1)
                {
                    return 0;
                }
                this.neuDataListSpread_Sheet1.SetActiveCell(activeRow + 1, this.FindFirstWriteColumn());
                return 1;
            }

            for (int columnIndex = activeColumn + 1; columnIndex < this.neuDataListSpread_Sheet1.ColumnCount; columnIndex++)
            {
                if (this.CheckWrite(columnIndex) && this.neuDataListSpread_Sheet1.Columns[columnIndex].Width > 1)
                {
                    this.neuDataListSpread_Sheet1.SetActiveCell(activeRow, columnIndex);
                    break;
                }
            }
            return 1;
        }

        void ntxtFilter_TextChanged(object sender, EventArgs e)
        {
            if (this.neuDataListSpread_Sheet1.DataSource is DataView && !string.IsNullOrEmpty(this.curFilter))
            {
                try
                {
                    ((DataView)this.neuDataListSpread_Sheet1.DataSource).RowFilter = string.Format(this.curFilter, this.ntxtFilter.Text);
                    this.neuDataListSpread.ReadSchema(this.curSettingFileName);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("过滤明细数据发生错误：" + ex.Message);
                }
            }
            if (this.curFilterTextChanged != null)
            {
                this.curFilterTextChanged();
            }
        }

        void neuSpread_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.neuDataListSpread.SaveSchema(this.curSettingFileName);
        }

    }
}
