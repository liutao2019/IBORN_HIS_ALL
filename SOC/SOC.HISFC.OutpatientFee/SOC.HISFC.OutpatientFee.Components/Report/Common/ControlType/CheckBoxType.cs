using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.ControlType
{
    [Serializable]
    class CheckBoxType : BaseControlType
    {
        private bool defaultDataSource = false;
        [System.ComponentModel.Category("T.单选择框")]
        [System.ComponentModel.Description("默认值")]
        public bool DefaultDataSource
        {
            get
            {
                return defaultDataSource;
            }
            set
            {
                defaultDataSource = value;
            }
        }

        public override string ToString()
        {
            return "单选择框";
        }
    }
}
