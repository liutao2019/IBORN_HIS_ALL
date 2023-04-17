using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Enum;
using System.Collections;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.ControlType
{
    [Serializable]
    public class DateTimeType : BaseControlType
    {
        private int addDays = 0;
        [System.ComponentModel.DefaultValue(0)]
        [System.ComponentModel.Category("D.时间选择框")]
        [System.ComponentModel.Description("当前时间的基础上默认增加/减少几天")]
        public int AddDays
        {
            get
            {
                return addDays;
            }
            set
            {
                addDays = value;
            }
        }

        private int addMonths = 0;
        [System.ComponentModel.DefaultValue(0)]
        [System.ComponentModel.Category("D.时间选择框")]
        [System.ComponentModel.Description("当前时间的基础上默认增加/减少几个月")]
        public int AddMonths
        {
            get
            {
                return addMonths;
            }
            set
            {
                addMonths = value;
            }
        }

        private string customFormat = "yyyy-MM-dd";
        [System.ComponentModel.DefaultValue(0)]
        [System.ComponentModel.Category("D.时间选择框")]
        [System.ComponentModel.Description("自定义日期格式 默认：yyyy-MM-dd")]
        public string CustomFormat
        {
            get
            {
                return customFormat;
            }
            set
            {
                customFormat = value;
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
        [System.ComponentModel.Category("D.时间选择框")]
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

        private System.Windows.Forms.DateTimePickerFormat format = System.Windows.Forms.DateTimePickerFormat.Custom;
        [System.ComponentModel.DefaultValue(System.Windows.Forms.DateTimePickerFormat.Custom)]
        [System.ComponentModel.Category("D.时间选择框")]
        [System.ComponentModel.Description("使用日期格式的模式")]
        public System.Windows.Forms.DateTimePickerFormat Format
        {
            get
            {
                return format;
            }
            set
            {
                format = value;
            }
        }

        public override string ToString()
        {
            return "日期控件";
        }
    }
}
