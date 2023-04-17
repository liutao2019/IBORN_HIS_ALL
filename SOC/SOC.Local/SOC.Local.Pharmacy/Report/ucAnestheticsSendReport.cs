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
    public partial class ucAnestheticsSendReport : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        public ucAnestheticsSendReport()
        {
            InitializeComponent();

            this.PriveClassTwos = "0310,0320";
            this.MainTitle = "精神类药品发退量汇总统计表";
            this.RightAdditionTitle = "";

            this.SQLIndexs = "SOC.Pharmacy.Report.Out.AnestheticsSendReport";

            this.IsNeedDetailData = false;

            this.QueryDataWhenInit = false;

            this.CustomTypeSQL = "select code id,name,mark,spell_code,wb_code,input_code from com_dictionary where type = 'DRUGQUALITY' and code in ('P','P1','P2','S1','SS')";
        }
    }
}
