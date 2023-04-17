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
    public partial class ucDrugRateContrast : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        /// <summary>
        /// 药品比例
        /// </summary>
        public ucDrugRateContrast()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.SunnyDrug.DrugRateContrast";
            this.MainTitle = "药品比例";
            this.PriveClassTwos = "0320";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8,9,10,11";
            this.CustomTypeShowType = "名次：";
            this.CustomTypeSQL = @"select '15' id,'15' name,'15' memo,'15' spell_code,'15','15' input_code from dual";
            this.cmbCustomType.Text = "15";
            this.IsDeptAsCondition = false;
            this.QueryDataWhenInit = false;
            this.MidAdditionTitle = string.Empty;
            this.RightAdditionTitle = string.Empty;
        }
    }
}
