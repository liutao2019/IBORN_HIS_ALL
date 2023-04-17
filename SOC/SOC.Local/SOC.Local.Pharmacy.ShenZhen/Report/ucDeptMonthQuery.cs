using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Pharmacy.ShenZhen.Report
{
    public partial class ucDeptMonthQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDeptMonthQuery()
        {
            InitializeComponent();
        }

        private bool tab1InitEnd = false;
        private bool tab2InitEnd = false;
        private bool tab3InitEnd = false;
        protected override void OnLoad(EventArgs e)
        {
            this.ucPIMonstoreINQuery1.LoadInit();
            this.tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
            base.OnLoad(e);
        }

        void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 1 && !tab1InitEnd)
            {
                this.ucPIMonstoreOutQuery1.LoadInit();
                tab1InitEnd = true;
            }
            else if (this.tabControl1.SelectedIndex == 2 && !tab2InitEnd)
            {
                this.ucMonstoreAdjustStatic1.LoadInit();
                tab2InitEnd = true;
            }
            else if (this.tabControl1.SelectedIndex == 3 && !tab3InitEnd)
            {
                this.ucMonstoreCheckStatic1.LoadInit();
                tab3InitEnd = true;
            }
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.tabPage1 == this.tabControl1.SelectedTab)
            {
                this.ucPIMonstoreINQuery1.fpSpread1_Sheet1.Rows.Count = 0;
                this.ucPIMonstoreINQuery1.fpSpread1_Sheet2.Rows.Count = 0;
                this.ucPIMonstoreINQuery1.QueryData();//(sender, neuObject);
                this.ucPIMonstoreINQuery1.fpSpread1_Sheet1.Rows.Add(this.ucPIMonstoreINQuery1.fpSpread1_Sheet1.RowCount, 1);
                this.ucPIMonstoreINQuery1.fpSpread1_Sheet1.Models.Span.Add(this.ucPIMonstoreINQuery1.fpSpread1_Sheet1.RowCount - 1, 0, 1, this.ucPIMonstoreINQuery1.fpSpread1_Sheet1.ColumnCount);
                this.ucPIMonstoreINQuery1.fpSpread1_Sheet1.Cells[this.ucPIMonstoreINQuery1.fpSpread1_Sheet1.RowCount - 1, 0].Text = "制表：             核对：           主任签名：";
                this.ucPIMonstoreINQuery1.fpSpread1_Sheet2.Rows.Add(this.ucPIMonstoreINQuery1.fpSpread1_Sheet2.RowCount, 1);
                this.ucPIMonstoreINQuery1.fpSpread1_Sheet2.Models.Span.Add(this.ucPIMonstoreINQuery1.fpSpread1_Sheet2.RowCount - 1, 0, 1, this.ucPIMonstoreINQuery1.fpSpread1_Sheet2.ColumnCount);
                this.ucPIMonstoreINQuery1.fpSpread1_Sheet2.Cells[this.ucPIMonstoreINQuery1.fpSpread1_Sheet2.RowCount - 1, 0].Text = "制表：             核对：           主任签名：";
            }
            else if (this.tabPage2 == this.tabControl1.SelectedTab)
            {
                this.ucPIMonstoreOutQuery1.fpSpread1_Sheet1.Rows.Count = 0;
                this.ucPIMonstoreOutQuery1.fpSpread1_Sheet2.Rows.Count = 0;
                this.ucPIMonstoreOutQuery1.QueryData();
                this.ucPIMonstoreOutQuery1.fpSpread1_Sheet1.Rows.Add(this.ucPIMonstoreOutQuery1.fpSpread1_Sheet1.RowCount, 1);
                this.ucPIMonstoreOutQuery1.fpSpread1_Sheet1.Models.Span.Add(this.ucPIMonstoreOutQuery1.fpSpread1_Sheet1.RowCount - 1, 0, 1, this.ucPIMonstoreOutQuery1.fpSpread1_Sheet1.ColumnCount);
                this.ucPIMonstoreOutQuery1.fpSpread1_Sheet1.Cells[this.ucPIMonstoreOutQuery1.fpSpread1_Sheet1.RowCount - 1, 0].Text = "制表：             核对：           主任签名：";
                this.ucPIMonstoreOutQuery1.fpSpread1_Sheet2.Rows.Add(this.ucPIMonstoreOutQuery1.fpSpread1_Sheet2.RowCount, 1);
                this.ucPIMonstoreOutQuery1.fpSpread1_Sheet2.Models.Span.Add(this.ucPIMonstoreOutQuery1.fpSpread1_Sheet2.RowCount - 1, 0, 1, this.ucPIMonstoreOutQuery1.fpSpread1_Sheet2.ColumnCount);
                this.ucPIMonstoreOutQuery1.fpSpread1_Sheet2.Cells[this.ucPIMonstoreOutQuery1.fpSpread1_Sheet2.RowCount - 1, 0].Text = "制表：             核对：           主任签名：";
            }
            else if (this.tabPage3 == this.tabControl1.SelectedTab)
            {
                this.ucMonstoreAdjustStatic1.fpSpread1_Sheet1.Rows.Count = 0;
                this.ucMonstoreAdjustStatic1.fpSpread1_Sheet2.Rows.Count = 0;
                this.ucMonstoreAdjustStatic1.QueryData();
                this.ucMonstoreAdjustStatic1.fpSpread1_Sheet1.Rows.Add(this.ucMonstoreAdjustStatic1.fpSpread1_Sheet1.RowCount, 1);
                this.ucMonstoreAdjustStatic1.fpSpread1_Sheet1.Models.Span.Add(this.ucMonstoreAdjustStatic1.fpSpread1_Sheet1.RowCount - 1, 0, 1, this.ucMonstoreAdjustStatic1.fpSpread1_Sheet1.ColumnCount);
                this.ucMonstoreAdjustStatic1.fpSpread1_Sheet1.Cells[this.ucMonstoreAdjustStatic1.fpSpread1_Sheet1.RowCount - 1, 0].Text = "制表：             核对：           主任签名：";
            }
            else if (this.tabPage4 == this.tabControl1.SelectedTab)
            {
                this.ucMonstoreCheckStatic1.fpSpread1_Sheet1.Rows.Count = 0;
                this.ucMonstoreCheckStatic1.fpSpread1_Sheet2.Rows.Count = 0;
                this.ucMonstoreCheckStatic1.QueryData();
                this.ucMonstoreCheckStatic1.fpSpread1_Sheet1.Rows.Add(this.ucMonstoreCheckStatic1.fpSpread1_Sheet1.RowCount, 1);
                this.ucMonstoreCheckStatic1.fpSpread1_Sheet1.Models.Span.Add(this.ucMonstoreCheckStatic1.fpSpread1_Sheet1.RowCount - 1, 0, 1, this.ucMonstoreCheckStatic1.fpSpread1_Sheet1.ColumnCount);
                this.ucMonstoreCheckStatic1.fpSpread1_Sheet1.Cells[this.ucMonstoreCheckStatic1.fpSpread1_Sheet1.RowCount - 1, 0].Text = "制表：             核对：           主任签名：";
            }
            return base.OnQuery(sender, neuObject);
        }
        public override int Export(object sender, object neuObject)
        {
            if (this.tabPage1 == this.tabControl1.SelectedTab)
            {
                this.ucPIMonstoreINQuery1.ExportData();//(sender, neuObject);
            }
            else if (this.tabPage2 == this.tabControl1.SelectedTab)
            {
                this.ucPIMonstoreOutQuery1.ExportData();
            }
            else if (this.tabPage3 == this.tabControl1.SelectedTab)
            {
                this.ucMonstoreAdjustStatic1.ExportData();
            }
            else if (this.tabPage4 == this.tabControl1.SelectedTab)
            {
                this.ucMonstoreCheckStatic1.ExportData();
            }
            return base.Export(sender, neuObject);
        }
        protected override int OnPrint(object sender, object neuObject)
        {
            if (this.tabPage1 == this.tabControl1.SelectedTab)
            {
                this.ucPIMonstoreINQuery1.PrintData();//(sender, neuObject);
            }
            else if (this.tabPage2 == this.tabControl1.SelectedTab)
            {
                this.ucPIMonstoreOutQuery1.PrintData();
            }
            else if (this.tabPage3 == this.tabControl1.SelectedTab)
            {
                this.ucMonstoreAdjustStatic1.PrintData();
            }
            else if (this.tabPage4 == this.tabControl1.SelectedTab)
            {
                this.ucMonstoreCheckStatic1.PrintData();
            }
            return base.OnPrint(sender, neuObject);
        }
    }
}
