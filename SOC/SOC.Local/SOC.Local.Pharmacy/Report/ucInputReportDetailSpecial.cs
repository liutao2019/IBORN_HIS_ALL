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
    public partial class ucInputReportDetailSpecial : FS.SOC.Local.Pharmacy.Base.ucPrivePowerReport
    {
        public ucInputReportDetailSpecial()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.InSpecial.Detail";
            this.PriveClassTwos = "0310";
            this.MainTitle = "入库明细查询(自增加入库)";
            this.RightAdditionTitle = "";
            this.ShowTypeName = myDeptType.单位;
            this.IsUseCustomType = true;
            this.CustomTypeSQL = @"select c.class3_code,c.class3_name,c.class3_meaning_code,c.class3_code,'','' from com_priv_class3 c where c.class2_code = '0320'";
            this.SumColIndexs = "12,13,14";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20";
        }
    }
}
