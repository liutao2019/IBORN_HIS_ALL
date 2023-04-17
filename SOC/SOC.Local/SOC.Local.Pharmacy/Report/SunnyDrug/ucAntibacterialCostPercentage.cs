using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Function;

namespace FS.SOC.Local.Pharmacy.Report.SunnyDrug
{
    public partial class ucAntibacterialCostPercentage : Base.BaseReport
    {
        public ucAntibacterialCostPercentage()
        {

            InitializeComponent();

            this.PriveClassTwos = "0310";
            this.MainTitle = "药品占总收入百分比";
            this.RightAdditionTitle = "";

            this.SQLIndexs = "SOC.Pharmacy.Report.SunnyDrug.AntibacterialDrug.CostPercentage";

            this.IsNeedDetailData = false;
            this.QueryDataWhenInit = false;
        }

        private void ucAntibacterialCostPercentage_Load(object sender, EventArgs e)
        {
            this.QueryEndHandler = new DelegateQueryEnd(QueryEnd);
        }

        private void QueryEnd()
        {
            if (!string.IsNullOrEmpty(this.SumColIndexs))
            {
                int lastRowIndex = this.fpSpread1_Sheet1.RowCount - 1;
                int lastColumnIndex = this.fpSpread1_Sheet1.ColumnCount - 1;

                decimal totDrugCost = NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[lastRowIndex, lastColumnIndex - 2].Value);
                decimal totAllCost = NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[lastRowIndex, lastColumnIndex - 1].Value);
                if (totDrugCost == 0)
                {
                    totDrugCost = 1;
                }
                this.fpSpread1_Sheet1.Cells[lastRowIndex, lastColumnIndex].Text = FrameWork.Public.String.FormatNumberReturnString(totDrugCost * 100 / totAllCost, 2);
                this.fpSpread1_Sheet1.Cells[lastRowIndex, 0].Text = "全院：";
            }
        }
    }
}
