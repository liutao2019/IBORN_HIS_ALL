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
    public partial class ucOutpatientDeptPerCapitaDrugs:FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        /// <summary>
        /// 门诊科室人均药费排名
        /// </summary>
        public ucOutpatientDeptPerCapitaDrugs()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.SunnyDrug.OutpatientDeptPerCapitaDrugs.AvgTotCost";
            this.MainTitle = "门诊科室人均药费排名";
            this.PriveClassTwos = "0320";
            this.SumColIndexs = "5";
            this.SortColIndexs = "0,1,2,3,4,5";
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
