using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Report.SunnyDrug.SingleSpeciesMonitoring
{
    public partial class ucAntibioticsUseRankings : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        /// <summary>
        /// 抗菌药物使用率排名
        /// </summary>
        public ucAntibioticsUseRankings()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.SunnyDrug.AntibioticsUseRankings.Out";
            this.MainTitle = "抗菌药物使用率排名";
            this.PriveClassTwos = "0320";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8,9,10,11";
            this.IsUseCustomType = true;
            this.CustomTypeShowType = "名 次：";
            this.CustomTypeSQL = @"select '15' id,'15' name,'15' memo,'15' spell_code,'15','15' input_code from dual";
            this.cmbCustomType.Text = "15";
            this.IsDeptAsCondition = false;
            this.QueryDataWhenInit = false;
            this.MidAdditionTitle = string.Empty;
            this.RightAdditionTitle = string.Empty;

            this.label2.Location = new Point(4, this.label2.Location.Y);

            this.cbInPatient.CheckedChanged += new EventHandler(cbInPatient_CheckedChanged);
            this.cbOutPatient.CheckedChanged += new EventHandler(cbOutPatient_CheckedChanged);
            this.cbInPatient.Visible = true;
            this.cbOutPatient.Visible = true;
        }

        void cbInPatient_CheckedChanged(object sender, EventArgs e)
        {
            this.cbOutPatient.Checked = !this.cbInPatient.Checked;
        }

        void cbOutPatient_CheckedChanged(object sender, EventArgs e)
        {
            this.cbInPatient.Checked = !this.cbOutPatient.Checked;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.cbInPatient.Checked)
            {
                this.SQLIndexs = @"Pharmacy.NewReport.SunnyDrug.AntibioticsUseRankings.In";
            }
            else
            {
                this.SQLIndexs = @"Pharmacy.NewReport.SunnyDrug.AntibioticsUseRankings.Out";
            }
            return base.OnQuery(sender, neuObject);
        }
    }
}
