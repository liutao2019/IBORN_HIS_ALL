using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Report.SunnyDrug.OutpatientDataMonitoring
{
    public partial class ucDoctorAmountOfMedicineRank:FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        /// <summary>
        /// 门诊医生用药金额数量排名
        /// </summary>
        public ucDoctorAmountOfMedicineRank()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.SunnyDrug.DoctorAmountOfMedicineRank.Amount";
            this.MainTitle = "门诊医生用药金额数量排名";
            this.PriveClassTwos = "0320";
            this.SumColIndexs = "3,4";
            this.SortColIndexs = "0,1,2,3,4";
            this.IsUseCustomType = true;
            this.IsDeptAsCondition = false;
            this.CustomTypeShowType = "名次：";
            this.CustomTypeSQL = @"select '15' id,'15' name,'15' memo,'15' spell_code,'15','15' input_code from dual";
            this.cmbCustomType.Text = "15";
            this.cbAmount.CheckedChanged += new EventHandler(cbAmount_CheckedChanged);
            this.cbTotCost.CheckedChanged += new EventHandler(cbTotCost_CheckedChanged);
            this.cbTotCost.Visible = true;
            this.cbAmount.Visible = true;
            this.MidAdditionTitle = string.Empty;
            this.RightAdditionTitle = string.Empty;
            this.cbAmount.Location = new Point(this.dtEnd.Location.X + this.dtEnd.Width + 300, this.dtEnd.Location.Y);
            this.cbTotCost.Location = new Point(this.cbAmount.Location.X + this.cbAmount.Width + 50, this.dtEnd.Location.Y);
        }

        void cbTotCost_CheckedChanged(object sender, EventArgs e)
        {
            this.cbAmount.Checked = !this.cbTotCost.Checked;
        }

        void cbAmount_CheckedChanged(object sender, EventArgs e)
        {
            this.cbTotCost.Checked = !this.cbAmount.Checked;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            if(this.cbTotCost.Checked)
            {
                this.SQLIndexs = "Pharmacy.NewReport.SunnyDrug.DoctorAmountOfMedicineRank.TotCost";
            }
            else
            {
                this.SQLIndexs = "Pharmacy.NewReport.SunnyDrug.DoctorAmountOfMedicineRank.Amount";
            }
            
            return base.OnQuery(sender, neuObject);
        }
    }
}
