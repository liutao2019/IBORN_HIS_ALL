using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Pharmacy.Maintenance.Item
{
    public partial class ucUpdateItemReport : FS.SOC.HISFC.Components.PharmacyCommon.BaseReport
    {
        public ucUpdateItemReport()
        {
            InitializeComponent();


            
            this.MainTitle = "药品基本信息修改记录";
            this.RightAdditionTitle = "";
            this.MidAdditionTitle = "";            
            this.IsDeptAsCondition = false;            
            this.IsUseCustomType = true;            

            this.SQLIndexs = "Pharmacy.NewReport.BaseInfo.Log.Query";
            this.CustomTypeSQL = "select distinct d.old_data_code as id,d.new_data_code as name,'','','',''  from pha_com_shiftdata d";

            this.IsNeedDetailData = false;

            this.QueryDataWhenInit = false;
        }
    }
}
