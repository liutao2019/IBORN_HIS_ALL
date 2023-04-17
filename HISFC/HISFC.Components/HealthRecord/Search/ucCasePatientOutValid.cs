using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.Search
{
    public partial class ucCasePatientOutValid : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCasePatientOutValid()
        {
            InitializeComponent();
        }
        FS.HISFC.BizLogic.HealthRecord.SearchManager searchManager = new FS.HISFC.BizLogic.HealthRecord.SearchManager();
        protected override int OnQuery(object sender, object neuObject)
        {
            this.neuSpread1.ActiveSheetIndex = 0;
            DataSet ds = new DataSet();
            if (searchManager.QueryDeptCaseInfo(dtBegin.Value, dtEnd.Value, ref ds) == -1)
            {
                MessageBox.Show("查询病案有效性信息出错 "+searchManager.Err);
                return -1;
            }
            if (ds.Tables == null || ds.Tables.Count == 0)
            {
                MessageBox.Show("查询病案有效性信息出错");
                return -1;
            }
            this.neuSpread1_Sheet1.DataSource = ds.Tables[0].DefaultView;
            this.LockFp();
            return base.OnQuery(sender, neuObject);
        }
        private void LockFp()
        {
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numberCellType.DecimalPlaces = 0;
            this.neuSpread1_Sheet1.Columns[0].Width = 100;//出院时间
            this.neuSpread1_Sheet1.Columns[1].Width = 0;//科室编码
            this.neuSpread1_Sheet1.Columns[2].Width = 100;//科室名称
            this.neuSpread1_Sheet1.Columns[3].Width = 100;//出院人数
            this.neuSpread1_Sheet1.Columns[3].CellType = numberCellType;
            this.neuSpread1_Sheet1.Columns[4].Width = 100;//病案录入数量
            this.neuSpread1_Sheet1.Columns[4].CellType = numberCellType;
        }
        private void LockFpDetail()
        {
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numberCellType.DecimalPlaces = 0;
            this.neuSpread1_Sheet2.Columns[0].Width = 100;//住院号
            this.neuSpread1_Sheet2.Columns[1].Width = 100;//姓名
            this.neuSpread1_Sheet2.Columns[2].Width = 100;//入院日期
            this.neuSpread1_Sheet2.Columns[3].Width = 100;//出院日期
            this.neuSpread1_Sheet2.Columns[4].Width = 100;//病案录入数量
            this.neuSpread1_Sheet2.Columns[4].CellType = numberCellType;
            this.neuSpread1_Sheet2.Columns[5].Width = 100;//病案录入数量

        }
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.neuSpread1_Sheet1.RowCount == 0 || this.neuSpread1.ActiveSheetIndex == 1)
            {
                return;
            }
            int index = this.neuSpread1_Sheet1.ActiveRowIndex;

            if (this.neuSpread1_Sheet1.Cells[index, 3].Text == this.neuSpread1_Sheet1.Cells[index, 4].Text)
            {
                MessageBox.Show("没有未录入病案的患者信息" + searchManager.Err);
                return;
            }
            this.neuSpread1.ActiveSheetIndex = 1;
            DataSet ds = new DataSet();
            string dept=this.neuSpread1_Sheet1.Cells[index,1].Text;
            DateTime dtOut = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[index, 0].Text);
            
            if (searchManager.QueryDeptCaseInfoDetail(dtOut,dept,ref ds)== -1)
            {
                MessageBox.Show("查询科室没有病案录入信息错处" + searchManager.Err);
                return;
            }
            if (ds.Tables == null || ds.Tables.Count == 0)
            {
                MessageBox.Show("查询科室没有病案录入信息错处");
                return;
            }
            this.neuSpread1_Sheet2.DataSource = ds.Tables[0].DefaultView;
            this.LockFpDetail();
        }
    }
}
