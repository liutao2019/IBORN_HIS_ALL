using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Enum;
using System.Collections;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.ControlType
{


    [Serializable]
    public class TextBoxType : BaseControlType
    {
        private bool isPadLeft = false;
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("T.一般输入框")]
        [System.ComponentModel.Description("是否启用左边补齐字符模式，True启用，False不启用")]
        public bool IsPadLeft
        {
            get
            {
                return isPadLeft;
            }
            set
            {
                isPadLeft = value;
            }
        }

        private int length = 10;
        [System.ComponentModel.DefaultValue(10)]
        [System.ComponentModel.Category("T.一般输入框")]
        [System.ComponentModel.Description("启用左边补齐字符模式时，补齐的位数；默认为10")]
        public int Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
            }
        }

        private string padLeftName = "0";
        [System.ComponentModel.DefaultValue("0")]
        [System.ComponentModel.Category("T.一般输入框")]
        [System.ComponentModel.Description("启用左边补齐字符模式时，补齐的字符；默认为0")]
        public string PadLeftName
        {
            get
            {
                return padLeftName;
            }
            set
            {
                padLeftName = value;
            }
        }

        private string datasource = string.Empty;
        internal string DataSource
        {
            get
            {
                return datasource;
            }
            set
            {
                datasource = value;
            }
        }

        private string defaultDataSource = string.Empty;
        [System.ComponentModel.Category("T.一般输入框")]
        [System.ComponentModel.Description("当数据源为Custom模式时，默认数据集合的定义")]
        public  string DefaultDataSource
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

        private bool isEnterFilter = true;
        [System.ComponentModel.Category("T.一般输入框")]
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
            return "文本框";
        }
    }
}
