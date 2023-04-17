using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Enum;
using System.Collections;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.ControlType
{

    [Serializable]
    public class FilterTreeView : TreeViewType
    {
        private string filterStr = "";
        [System.ComponentModel.Category("FT.过滤树")]
        [System.ComponentModel.Description("设置过滤时使用的语句，例如patient_no like '%{0}%'")]
        public string FilterStr
        {
            get
            {
                return filterStr;
            }
            set
            {
                filterStr = value;
            }
        }

        public override string ToString()
        {
            return "树过滤框";
        }
    }
}
