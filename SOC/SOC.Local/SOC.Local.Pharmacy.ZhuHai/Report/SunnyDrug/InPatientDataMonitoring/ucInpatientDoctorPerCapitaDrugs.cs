using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Report.SunnyDrug.InPatientDataMonitoring
{
    public partial class ucInpatientDoctorPerCapitaDrugs : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        /// <summary>
        /// 住院医生人均药费排名
        /// </summary>
        public ucInpatientDoctorPerCapitaDrugs()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.SunnyDrug.ucInpatientDoctorPerCapitaDrugs.AvgTotCost";
            this.MainTitle = "住院医生人均药费排名";
            this.PriveClassTwos = "0320";
            this.SumColIndexs = "4,5";
            this.SortColIndexs = "0,1,2,3,4,5,6";
            this.IsUseCustomType = true;
            this.IsDeptAsCondition = false;
            this.CustomTypeShowType = "名次：";
            this.CustomTypeSQL = @"select '15' id,'15' name,'15' memo,'15' spell_code,'15','15' input_code from dual";
            this.cmbCustomType.Text = "15";
            this.MidAdditionTitle = string.Empty;
            this.RightAdditionTitle = string.Empty;
        }
    }
}
