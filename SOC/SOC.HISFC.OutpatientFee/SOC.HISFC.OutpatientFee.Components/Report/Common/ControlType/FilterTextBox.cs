using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Enum;
using System.Collections;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.ControlType
{
    [Serializable]
    public class FilterTextBox : BaseControlType
    {
        private string filterStr = "";
        [System.ComponentModel.Category("FT.过滤文本框")]
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

        private bool isEnterFilter = false;
        [System.ComponentModel.Category("FT.过滤文本框")]
        [System.ComponentModel.Description("是否回车后过滤，True:是，False:否")]
        public bool IsEnterFilter
        {
            get
            {
                return isEnterFilter;
            }
            set
            {
                isEnterFilter = value;
            }
        }


        public override string ToString()
        {
            return "文本过滤框";
        }
    }
}
