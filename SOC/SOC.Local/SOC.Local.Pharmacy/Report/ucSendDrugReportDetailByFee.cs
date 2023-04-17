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
    public partial class ucSendDrugReportDetailByFee : FS.SOC.Local.Pharmacy.Base.ucPrivePowerReport
    {
        public ucSendDrugReportDetailByFee()
        {
            InitializeComponent();
            this.SQLIndexs = "SOC.Pharmacy.Out.SendDrugRepot.Detail.ByFee";
            this.PriveClassTwos = "0320";
            this.MainTitle = "药房消耗明细查询（根据收费查询）";
            this.RightAdditionTitle = "";
            this.IsUseCustomType = true;
            this.CustomTypeSQL = @"select '0' id,
       '门诊' name,
       '' memo,
       'mz' spell_code,
       'MZ',
       ''
  from dual
union
select '1' id,
       '住院' name,
       '' memo,
       'zy' spell_code,
       'ZY',
       ''
  from dual";
        }

        private void ucSendDrugReport_Load(object sender, EventArgs e)
        {           

            
            //this.Init();
        }
    }
}
