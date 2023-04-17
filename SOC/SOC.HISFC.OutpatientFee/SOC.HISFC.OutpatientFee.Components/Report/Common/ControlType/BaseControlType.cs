using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.UITypeEditor;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.TypeConterter;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Enum;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.ControlType
{
    [Serializable]
    [Editor(typeof(ControlUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
    [TypeConverter(typeof(ControlTypeConverter))]
    public class BaseControlType
    {
        private bool isLike = false;
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("B.基类控件")]
        [System.ComponentModel.Description("是否启用查询条件匹配模式，True启用，False不启用")]
        public bool IsLike
        {
            get
            {
                return isLike;
            }
            set
            {
                isLike = value;
            }
        }

        private string likeStr = "%{0}%";
        [System.ComponentModel.DefaultValue("%{0}%")]
        [System.ComponentModel.Category("B.基类控件")]
        [System.ComponentModel.Description("查询条件启用匹配模式时，使用匹配的方式，例：%{0}为结尾匹配；{0}%为起始匹配；%{0}%为全匹配")]
        public string LikeStr
        {
            get
            {
                return likeStr;
            }
            set
            {
                likeStr = value;
            }
        }

        private System.Drawing.Size size = new System.Drawing.Size(120, 20);
        [System.ComponentModel.Category("B.基类控件")]
        [System.ComponentModel.Description("控件的大小，包括标签")]
        public System.Drawing.Size Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

        private System.Drawing.Point location = new System.Drawing.Point();
        [System.ComponentModel.Category("B.基类控件")]
        [System.ComponentModel.Description("控件的位置")]
        public System.Drawing.Point Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

        //private Object defaultDataSource = null;
        //[System.ComponentModel.Category("B.基类控件")]
        //[System.ComponentModel.Description("当数据源为Custom模式时，默认数据集合的定义")]
        //public Object DefaultDataSource
        //{
        //    get
        //    {
        //        return defaultDataSource;
        //    }
        //    set
        //    {
        //        defaultDataSource = value;
        //    }
        //}

        private EnumDataSourceType dataSourceType = EnumDataSourceType.Dictionary;
        [System.ComponentModel.DefaultValue(EnumDataSourceType.Dictionary)]
        [System.ComponentModel.Category("B.基类控件")]
        [System.ComponentModel.Description("数据源的来源类型，Sql：使用Sql语句加载数据；Custom：自定义数据集；Dictionary：常数表加载；DepartmentType：科室类型；EmployeeType：人员类型")]
        public EnumDataSourceType QueryDataSource
        {
            get
            {
                return dataSourceType;
            }
            set
            {
                dataSourceType = value;
            }
        }

        private string dataSourceTypeName = string.Empty;
        [System.ComponentModel.DefaultValue("ALL")]
        [System.ComponentModel.Category("B.基类控件")]
        [System.ComponentModel.Description("数据源加载来源类型的子类型，例如：数据来源为EmployeeType时，可以用人员类型来加载数据，ALL则为全部子类型")]
        public string DataSourceTypeName
        {
            get
            {
                return dataSourceTypeName;
            }
            set
            {
                dataSourceTypeName = value;
            }
        }

        private bool enabled = true;
        [System.ComponentModel.DefaultValue(true)]
        [System.ComponentModel.Category("B.基类控件")]
        [System.ComponentModel.Description("是否可用；默认为true")]
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
            }
        }
    }
}
