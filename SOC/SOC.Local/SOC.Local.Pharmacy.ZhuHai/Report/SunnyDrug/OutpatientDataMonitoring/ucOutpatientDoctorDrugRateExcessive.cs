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
    public partial class ucOutpatientDoctorDrugRateExcessive:FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        /// <summary>
        /// 门诊医生药品比例超标
        /// </summary>
        public ucOutpatientDoctorDrugRateExcessive()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.SunnyDrug.ucOutpatientDoctorDrugRateExcessive";
            this.MainTitle = "门诊医生药品比例超标";
            this.PriveClassTwos = "0320";
            this.SumColIndexs = "5,6";
            this.SortColIndexs = "0,1,2,3,4,5，6,7";
            this.IsUseCustomType = false;
            this.IsDeptAsCondition = false;
            this.MidAdditionTitle = string.Empty;
            this.RightAdditionTitle = string.Empty;
        }
    }
}
