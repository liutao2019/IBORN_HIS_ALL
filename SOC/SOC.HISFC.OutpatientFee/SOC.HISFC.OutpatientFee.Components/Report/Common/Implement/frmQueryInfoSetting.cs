using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Interface;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Implement
{
    public partial class frmQueryInfoSetting : Form,ISettingReportForm
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
            this.btnComfirmColumnGroup.Click += new EventHandler(btnComfirm_Click);
            this.btnComfirmColumnSum.Click += new EventHandler(btnComfirm_Click);
            this.btnComfirmCross.Click += new EventHandler(btnComfirm_Click);
            this.btnComfirmRowGroup.Click += new EventHandler(btnComfirm_Click);
            this.btnComfirmRowSum.Click += new EventHandler(btnComfirm_Click);

        }

        private string fileName = string.Empty;
        System.Drawing.FontConverter fc = new System.Drawing.FontConverter();

        public void SetValue(ReportQueryInfo reportQueryInfo)
        {
            //初始化数据
            this.cmbSqlType.AddItems(FS.FrameWork.Public.EnumHelper.Current.EnumArrayList<Enum.EnumSqlType>());
            this.cmbRowGroupLocation.AddItems(FS.FrameWork.Public.EnumHelper.Current.EnumArrayList<Enum.EnumGroupShowLocation>());
            this.cmbRowGroupShowInfo.AddItems(FS.FrameWork.Public.EnumHelper.Current.EnumArrayList<Enum.EnumGroupShowInfoType>());

            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread2_Sheet1.RowCount = 0;

            if (reportQueryInfo.List != null)
            {
                foreach (QueryControl common in reportQueryInfo.List)
                {
                    this.neuSpread1_Sheet1.RowCount++;
                    this.SetValue(this.neuSpread1_Sheet1.RowCount - 1, common);
                }
            }
            else
            {
                reportQueryInfo.List = new List<QueryControl>();
            }

            if (reportQueryInfo.QueryDataSource != null)
            {
                foreach (QueryDataSource common in reportQueryInfo.QueryDataSource)
                {
                    this.neuSpread2_Sheet1.RowCount++;
                    this.SetDataSourceValue(this.neuSpread2_Sheet1.RowCount - 1, common);
                }
            }
            else
            {
                reportQueryInfo.QueryDataSource = new List<QueryDataSource>();
            }

            if (reportQueryInfo.TableGroup != null)
            {
                this.txtTableGroupColumn.Text = reportQueryInfo.TableGroup.GroupCondition;
                this.ckGroupByPage.Checked = reportQueryInfo.TableGroup.GroupByPage;
                if (reportQueryInfo.TableGroup.QueryDataSource != null)
                {
                    this.txtTableGroupDataSource.Text = reportQueryInfo.TableGroup.QueryDataSource.Name;
                }
            }

            if (reportQueryInfo.PrintInfo != null&&reportQueryInfo.PrintInfo.PaperSize!=null&&string.IsNullOrEmpty(reportQueryInfo.PrintInfo.PaperSize.PaperName)==false)
            {
                this.txtPaperSizeName.Text = reportQueryInfo.PrintInfo.PaperSize.PaperName;
                this.txtPaperSizeWidth.Text = reportQueryInfo.PrintInfo.PaperSize.Width.ToString();
                this.txtPaperSizeHeight.Text = reportQueryInfo.PrintInfo.PaperSize.Height.ToString();
            }
            else
            {
                this.txtPaperSizeName.Text = "A4";
                this.txtPaperSizeWidth.Text = "850";
                this.txtPaperSizeHeight.Text = "1169";
            }
        }

        private void SetValue(int i, QueryControl common)
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

        private void SetDataSourceValue(int i, QueryDataSource common)
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
                a.List.Add(this.neuSpread1_Sheet1.Rows[i].Tag as QueryControl);
            }
            QueryDataSource tableGroupDataSource = null;
            for (int i = 0; i < this.neuSpread2_Sheet1.RowCount; i++)
            {
                a.QueryDataSource.Add(this.neuSpread2_Sheet1.Rows[i].Tag as QueryDataSource);

                if (a.QueryDataSource[a.QueryDataSource.Count - 1].Name == this.txtTableGroupDataSource.Text)
                {
                    tableGroupDataSource = a.QueryDataSource[a.QueryDataSource.Count - 1];
                }
            }

            if (string.IsNullOrEmpty(this.txtTableGroupColumn.Text)==false)
            {
                TableGroupInfo tableGroup = new TableGroupInfo();
                tableGroup.GroupCondition = this.txtTableGroupColumn.Text;
                tableGroup.GroupByPage = this.ckGroupByPage.Checked;
                tableGroup.QueryDataSource = tableGroupDataSource;
                a.TableGroup = tableGroup;
            }

            a.PrintInfo = new PrintInfo();
            a.PrintInfo.PaperSize.PaperName = this.txtPaperSizeName.Text;
            a.PrintInfo.PaperSize.Width = FS.FrameWork.Function.NConvert.ToInt32(this.txtPaperSizeWidth.Text);
            a.PrintInfo.PaperSize.Height = FS.FrameWork.Function.NConvert.ToInt32(this.txtPaperSizeHeight.Text);

            return a;
        }

        private void neuSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag is QueryControl)
            {
                this.propertyGrid1.SelectedObject = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag;
            }
        }

        private void neuSpread2_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.neuSpread2_Sheet1.Rows[this.neuSpread2_Sheet1.ActiveRowIndex].Tag is QueryDataSource)
            {
                QueryDataSource dataSouceType = this.neuSpread2_Sheet1.Rows[this.neuSpread2_Sheet1.ActiveRowIndex].Tag as QueryDataSource;
                this.txtName.Text = dataSouceType.Name;
                this.richTextBox1.Text = dataSouceType.Sql;
                this.ckAddMapRow.Checked = dataSouceType.AddMapRow;
                this.ckAddMapColumn.Checked = dataSouceType.AddMapColumn;
                this.ckAddMapData.Checked = dataSouceType.AddMapData;
                this.ckAddMapSourceData.Checked = dataSouceType.AddMapSourceData;
                this.ckCross.Checked = dataSouceType.IsCross;
                this.txtCrossValues.Text = dataSouceType.CrossValues;
                this.txtCrossRows.Text = dataSouceType.CrossRows;
                this.txtCrossColumns.Text = dataSouceType.CrossColumns;
                this.txtSumColumn.Text = dataSouceType.SumColumns;
                this.txtRowGroupColumn.Text = dataSouceType.CrossGroupColumns;
                this.txtCombinColumn.Text = dataSouceType.CrossCombinColumns;
                this.cmbSqlType.Tag = dataSouceType.SqlType.ToString();
                this.txtSumRow.Text = dataSouceType.SumRows;
                this.ckRowSum.Checked = dataSouceType.IsSumRow;

                if (dataSouceType.RowGroup != null && dataSouceType.RowGroup.Count > 0)
                {
                    this.txtRowGroupColumn.Text = dataSouceType.RowGroup[0].GroupConditionStr;
                    this.txtRowGroupCustomInfo.Text = dataSouceType.RowGroup[0].CustomShowInfo;
                    this.cmbRowGroupDepend.Text = dataSouceType.RowGroup[0].GroupDependColumn;
                    this.cmbRowGroupLocation.Tag = dataSouceType.RowGroup[0].GroupShowLocation.ToString();
                    this.cmbRowGroupShowInfo.Tag = dataSouceType.RowGroup[0].ShowInfoType.ToString();
                    this.ckRowGroupLine.Checked = dataSouceType.RowGroup[0].IsUseTableLine;
                    this.txtRowGroupFont.Text = fc.ConvertToInvariantString(dataSouceType.RowGroup[0].GroupFont);
                }
                else
                {
                    this.txtRowGroupColumn.Text = null;
                    this.txtRowGroupCustomInfo.Text = null;
                    this.cmbRowGroupDepend.Text = null;
                    this.cmbRowGroupLocation.Tag = null;
                    this.cmbRowGroupShowInfo.Tag = null;
                    this.ckRowGroupLine.Checked = false;
                    this.txtRowGroupFont.Text = string.Empty;
                }
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
                QueryControl commonReportQueryInfo = new QueryControl();
                this.neuSpread1_Sheet1.RowCount++;
                commonReportQueryInfo.Index = this.neuSpread1_Sheet1.RowCount - 1;
                this.SetValue(this.neuSpread1_Sheet1.RowCount - 1, commonReportQueryInfo);

                this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.RowCount - 1;
                this.neuSpread1_Sheet1.AddSelection(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 1);
                this.neuSpread1_SelectionChanged(null, null);
            }
            else
            {
                QueryDataSource datasouceType = new QueryDataSource();
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
                QueryControl temp = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as QueryControl;

                this.SetValue(this.neuSpread1_Sheet1.ActiveRowIndex, this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex + 1].Tag as QueryControl);
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
                QueryControl temp = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as QueryControl;

                this.SetValue(this.neuSpread1_Sheet1.ActiveRowIndex, this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex - 1].Tag as QueryControl);
                this.SetValue(this.neuSpread1_Sheet1.ActiveRowIndex - 1, temp);
                this.neuSpread1_Sheet1.ActiveRowIndex--;
                this.neuSpread1_Sheet1.AddSelection(this.neuSpread1_Sheet1.ActiveRowIndex, 0, 1, 1);
                this.neuSpread1_SelectionChanged(null, null);
            }
        }

        void btnComfirm_Click(object sender, EventArgs e)
        {
            QueryDataSource datasouceType = new QueryDataSource();
            datasouceType.AddMapRow = this.ckAddMapRow.Checked;
            datasouceType.Name = this.txtName.Text;
            datasouceType.Sql = this.richTextBox1.Text;
            datasouceType.IsCross = this.ckCross.Checked;
            datasouceType.CrossRows = this.txtCrossRows.Text;
            datasouceType.CrossColumns = this.txtCrossColumns.Text;
            datasouceType.CrossValues = this.txtCrossValues.Text;
            datasouceType.AddMapData = this.ckAddMapData.Checked;
            datasouceType.AddMapColumn = this.ckAddMapColumn.Checked;
            datasouceType.AddMapSourceData = this.ckAddMapSourceData.Checked;
            datasouceType.CrossCombinColumns = this.txtCombinColumn.Text;
            datasouceType.CrossGroupColumns = this.txtRowGroupColumn.Text;
            datasouceType.SumColumns = this.txtSumColumn.Text;
            datasouceType.SumRows = this.txtSumRow.Text;
            datasouceType.IsSumRow = this.ckRowSum.Checked;
            if (this.cmbSqlType.Tag != null&&string.IsNullOrEmpty(this.cmbSqlType.Tag.ToString())==false)
            {
                datasouceType.SqlType = FS.FrameWork.Public.EnumHelper.Current.GetEnum<Enum.EnumSqlType>(this.cmbSqlType.Tag.ToString());
            }

            if (string.IsNullOrEmpty(this.txtRowGroupColumn.Text)==false)
            {
                RowGroupInfo rowgroup = new RowGroupInfo();
                rowgroup.GroupConditionStr = this.txtRowGroupColumn.Text;
                rowgroup.IsUseTableLine = this.ckRowGroupLine.Checked;
                rowgroup.GroupDependColumn = this.cmbRowGroupDepend.Text;
                rowgroup.GroupShowLocation = FS.FrameWork.Public.EnumHelper.Current.GetEnum<Enum.EnumGroupShowLocation>(this.cmbRowGroupLocation.Tag.ToString());
                rowgroup.ShowInfoType = FS.FrameWork.Public.EnumHelper.Current.GetEnum<Enum.EnumGroupShowInfoType>(this.cmbRowGroupShowInfo.Tag.ToString());
                rowgroup.CustomShowInfo = this.txtRowGroupCustomInfo.Text;
                if (string.IsNullOrEmpty(this.txtRowGroupFont.Text) == false)
                {
                    rowgroup.GroupFont = (System.Drawing.Font)fc.ConvertFromString(this.txtRowGroupFont.Text);
                }
                datasouceType.RowGroup=new List<RowGroupInfo>();
                datasouceType.RowGroup.Add(rowgroup);
            }

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
                        this.SetValue(this.neuSpread1_Sheet1.ActiveRowIndex, this.propertyGrid1.SelectedObject as QueryControl);
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
                        this.SetValue(this.neuSpread1_Sheet1.ActiveRowIndex, this.propertyGrid1.SelectedObject as QueryControl);
                    }
                }
            }
        }

        private void neuButton3_Click(object sender, EventArgs e)
        {
            FontDialog fDialog = new FontDialog();
            if (string.IsNullOrEmpty(this.txtRowGroupFont.Text)==false)
            {
                fDialog.Font = (System.Drawing.Font)fc.ConvertFromString(this.txtRowGroupFont.Text);
            }
            fDialog.ShowDialog(this);
           this.txtRowGroupFont.Text=  fc.ConvertToInvariantString(fDialog.Font);
        }
    }
}
