using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Pharmacy.Report
{
    public partial class ucOutputStaticByType : Base.BaseReport
    {
        public ucOutputStaticByType()
        {
            InitializeComponent();

            this.PriveClassTwos = "0320";
            this.MainTitle = "药库调拨统计表";
            this.RightAdditionTitle = "";
            this.SQLIndexs = "SOC.Pharmacy.Report.Output.StaticByType";


        }

        protected override void OnLoad(EventArgs e)
        {
            this.QueryEndHandler += new DelegateQueryEnd(QueryEnd);            
            
            base.OnLoad(e);
            QueryEnd();
        }

        /// <summary>
        /// 设置列头
        /// </summary>
        private void QueryEnd()
        {
            //// 
            //// fpSpread1
            //// 
            //this.fpSpread1.AccessibleDescription = "fpSpread1, 汇总, Row 0, Column 0, ";
            //// 
            //// fpSpread1_Sheet1
            //// 
            //this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "汇总";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 16;
            this.fpSpread1_Sheet1.ColumnHeader.RowCount = 2;
            this.fpSpread1_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Default;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).RowSpan = 2;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "部门";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).ColumnSpan = 3;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "西药";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).ColumnSpan = 3;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "中成药";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).ColumnSpan = 3;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "中草药";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).ColumnSpan = 3;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "疫苗";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).ColumnSpan = 3;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "输液";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 1).Value = "购入金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 2).Value = "零售金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 3).Value = "差额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 4).Value = "购入金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 5).Value = "零售金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 6).Value = "差额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 7).Value = "购入金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 8).Value = "零售金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 9).Value = "差额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 10).Value = "购入金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 11).Value = "零售金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 12).Value = "差额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 13).Value = "购入金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 14).Value = "零售金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(1, 15).Value = "差额";
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine();
            this.fpSpread1_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine();
            this.fpSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 40F;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.RowHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine();
            this.fpSpread1_Sheet1.RowHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine();
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Auto;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
        }
    }
}
