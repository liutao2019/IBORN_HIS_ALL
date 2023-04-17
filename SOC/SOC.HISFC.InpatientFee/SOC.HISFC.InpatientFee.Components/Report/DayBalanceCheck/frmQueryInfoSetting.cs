using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.InpatientFee.Components.Report.DayBalanceCheck
{
    public partial class frmQueryInfoSetting : Form,ICommonReportController.ISettingReportForm
    {
        public frmQueryInfoSetting()
        {
            InitializeComponent();
            this.neuSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread1_SelectionChanged);
            this.neuSpread2.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread2_SelectionChanged);
            this.propertyGrid1.PropertyValueChanged += new PropertyValueChangedEventHandler(propertyGrid1_PropertyValueChanged);
            this.btnUp.Click += new EventHandler(btnUp_Click);
            this.btnDown.Click += new EventHandler(btnDown_Click);
            this.neuButton2.Click += new EventHandler(neuButton2_Click);
            this.neuButton1.Click += new EventHandler(neuButton1_Click);
            this.btnComfirm.Click += new EventHandler(btnComfirm_Click);

        }

        private string fileName = string.Empty;

        public void SetValue(ReportQueryInfo reportQueryInfo)
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread2_Sheet1.RowCount = 0;

            if (reportQueryInfo.List != null)
            {
                foreach (CommonReportQueryInfo common in reportQueryInfo.List)
                {
                    this.neuSpread1_Sheet1.RowCount++;
                    this.SetValue(this.neuSpread1_Sheet1.RowCount - 1, common);
                }
            }
            else
            {
                reportQueryInfo.List = new List<CommonReportQueryInfo>();
            }

            if (reportQueryInfo.DataSourceType != null)
            {
                foreach (DataSourceType common in reportQueryInfo.DataSourceType)
                {
                    this.neuSpread2_Sheet1.RowCount++;
                    this.SetDataSourceValue(this.neuSpread2_Sheet1.RowCount - 1, common);
                }
            }
            else
            {
                reportQueryInfo.DataSourceType = new List<DataSourceType>();
            }
        }

        private void SetValue(int i, CommonReportQueryInfo common)
        {
            if (i >= 0)
            {
                this.neuSpread1_Sheet1.Cells[i, 0].Text = common.Name;
                this.neuSpread1_Sheet1.Cells[i, 1].Text = common.Text;
                this.neuSpread1_Sheet1.Cells[i, 2].Text = common.ControlType.ToString();
                common.Index = i;
                this.neuSpread1_Sheet1.Rows[i].Tag = common;
            }
        }

        private void SetDataSourceValue(int i, DataSourceType common)
        {
            if (i >= 0)
            {
                this.neuSpread2_Sheet1.Cells[i, 0].Text = common.Name;
                this.neuSpread2_Sheet1.Cells[i, 1].Value = common.AddMapRow;
                this.neuSpread2_Sheet1.Rows[i].Tag = common;
            }
        }

        public ReportQueryInfo GetValue()
        {
            ReportQueryInfo a = new ReportQueryInfo();
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                a.List.Add(this.neuSpread1_Sheet1.Rows[i].Tag as CommonReportQueryInfo);
            }

            for (int i = 0; i < this.neuSpread2_Sheet1.RowCount; i++)
            {
                a.DataSourceType.Add(this.neuSpread2_Sheet1.Rows[i].Tag as DataSourceType);
            }

            return a;
        }

        private void neuSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag is CommonReportQueryInfo)
            {
                this.propertyGrid1.SelectedObject = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag;
            }
        }

        private void neuSpread2_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.neuSpread2_Sheet1.Rows[this.neuSpread2_Sheet1.ActiveRowIndex].Tag is DataSourceType)
            {
                DataSourceType dataSouceType = this.neuSpread2_Sheet1.Rows[this.neuSpread2_Sheet1.ActiveRowIndex].Tag as DataSourceType;
                this.txtName.Text = dataSouceType.Name;
                this.richTextBox1.Text = dataSouceType.Sql;
                this.ckAddMapRow.Checked = dataSouceType.AddMapRow;
                this.ckAddMapColumn.Checked = dataSouceType.AddMapColumn;
                this.ckAddMapData.Checked = dataSouceType.AddMapData;
                this.ckCross.Checked = dataSouceType.IsCross;
                this.txtCrossValues.Text = dataSouceType.CrossValues;
                this.txtCrossRows.Text = dataSouceType.CrossRows;
                this.txtCrossColumns.Text = dataSouceType.CrossColumns;
                this.txtSumColumn.Text = dataSouceType.CrossSumColumns;
                this.txtGroupColumn.Text = dataSouceType.CrossGroupColumns;
                this.txtCombinColumn.Text = dataSouceType.CrossCombinColumns;

            }
        }

        private void neuButton2_Click(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedTab == this.tabPage1)
            {
                if (this.neuSpread1_Sheet1.ActiveRowIndex >= 0 && this.neuSpread1_Sheet1.ActiveRowIndex <= this.neuSpread1_Sheet1.RowCount - 1)
                {
                    this.neuSpread1_Sheet1.RemoveRows(this.neuSpread1_Sheet1.ActiveRowIndex, 1);
                }
            }
            else
            {
                if (this.neuSpread2_Sheet1.ActiveRowIndex >= 0 && this.neuSpread2_Sheet1.ActiveRowIndex <= this.neuSpread2_Sheet1.RowCount - 1)
                {
                    this.neuSpread2_Sheet1.RemoveRows(this.neuSpread2_Sheet1.ActiveRowIndex, 1);
                }
            }
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedTab == this.tabPage1)
            {
                CommonReportQueryInfo commonReportQueryInfo = new CommonReportQueryInfo();
                this.neuSpread1_Sheet1.RowCount++;
                commonReportQueryInfo.Index = this.neuSpread1_Sheet1.RowCount - 1;
                this.SetValue(this.neuSpread1_Sheet1.RowCount - 1, commonReportQueryInfo);

                this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.RowCount - 1;
                this.neuSpread1_Sheet1.AddSelection(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 1);
                this.neuSpread1_SelectionChanged(null, null);
            }
            else
            {
                DataSourceType datasouceType = new DataSourceType();
                this.neuSpread2_Sheet1.RowCount++;
                this.SetDataSourceValue(this.neuSpread2_Sheet1.RowCount - 1, datasouceType);

                this.neuSpread2_Sheet1.ActiveRowIndex = this.neuSpread2_Sheet1.RowCount - 1;
                this.neuSpread2_Sheet1.AddSelection(this.neuSpread2_Sheet1.RowCount - 1, 0, 1, 1);
                this.neuSpread2_SelectionChanged(null, null);
            }
        }

        void btnDown_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1_Sheet1.ActiveRowIndex >= 0 && this.neuSpread1_Sheet1.ActiveRowIndex < this.neuSpread1_Sheet1.RowCount - 1)
            {
                CommonReportQueryInfo temp = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as CommonReportQueryInfo;

                this.SetValue(this.neuSpread1_Sheet1.ActiveRowIndex, this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex + 1].Tag as CommonReportQueryInfo);
                this.SetValue(this.neuSpread1_Sheet1.ActiveRowIndex + 1, temp);
                this.neuSpread1_Sheet1.ActiveRowIndex++;
                this.neuSpread1_Sheet1.AddSelection(this.neuSpread1_Sheet1.ActiveRowIndex, 0, 1, 1);
                this.neuSpread1_SelectionChanged(null, null);
            }
        }

        void btnUp_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1_Sheet1.ActiveRowIndex > 0)
            {
                CommonReportQueryInfo temp = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as CommonReportQueryInfo;

                this.SetValue(this.neuSpread1_Sheet1.ActiveRowIndex, this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex - 1].Tag as CommonReportQueryInfo);
                this.SetValue(this.neuSpread1_Sheet1.ActiveRowIndex - 1, temp);
                this.neuSpread1_Sheet1.ActiveRowIndex--;
                this.neuSpread1_Sheet1.AddSelection(this.neuSpread1_Sheet1.ActiveRowIndex, 0, 1, 1);
                this.neuSpread1_SelectionChanged(null, null);
            }
        }

        void btnComfirm_Click(object sender, EventArgs e)
        {
            DataSourceType datasouceType = new DataSourceType();
            datasouceType.AddMapRow = this.ckAddMapRow.Checked;
            datasouceType.Name = this.txtName.Text;
            datasouceType.Sql = this.richTextBox1.Text;
            datasouceType.IsCross = this.ckCross.Checked;
            datasouceType.CrossRows = this.txtCrossRows.Text;
            datasouceType.CrossColumns = this.txtCrossColumns.Text;
            datasouceType.CrossValues = this.txtCrossValues.Text;
            datasouceType.AddMapData = this.ckAddMapData.Checked;
            datasouceType.AddMapColumn = this.ckAddMapColumn.Checked;
            datasouceType.CrossCombinColumns = this.txtCombinColumn.Text;
            datasouceType.CrossGroupColumns = this.txtGroupColumn.Text;
            datasouceType.CrossSumColumns = this.txtSumColumn.Text;

            this.SetDataSourceValue(this.neuSpread2_Sheet1.ActiveRowIndex, datasouceType);
        }
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem != null && this.neuSpread1_Sheet1.ActiveRowIndex >= 0 && this.neuSpread1_Sheet1.ActiveRowIndex < this.neuSpread1_Sheet1.RowCount)
            {
                if (e.ChangedItem.Value.Equals(e.OldValue) == false)
                {
                    //重新加载数据
                    if (this.neuSpread1_Sheet1.ActiveRowIndex >= 0 && this.neuSpread1_Sheet1.ActiveRowIndex <= this.neuSpread1_Sheet1.RowCount - 1)
                    {
                        this.SetValue(this.neuSpread1_Sheet1.ActiveRowIndex, this.propertyGrid1.SelectedObject as CommonReportQueryInfo);
                    }
                }
            }
        }

        private void propertyGrid1_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            if (e.OldSelection != null)
            {
                if (e.OldSelection.Value is List<FS.FrameWork.Models.NeuObject>
                    || e.OldSelection.Value is ArrayList)
                {
                    //重新加载数据
                    if (this.neuSpread1_Sheet1.ActiveRowIndex >= 0 && this.neuSpread1_Sheet1.ActiveRowIndex <= this.neuSpread1_Sheet1.RowCount - 1)
                    {
                        this.SetValue(this.neuSpread1_Sheet1.ActiveRowIndex, this.propertyGrid1.SelectedObject as CommonReportQueryInfo);
                    }
                }
            }
        }
    }
}
