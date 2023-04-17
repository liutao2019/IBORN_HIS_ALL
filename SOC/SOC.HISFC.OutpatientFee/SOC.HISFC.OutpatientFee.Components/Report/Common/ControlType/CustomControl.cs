using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Enum;
using System.Collections;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.ControlType
{
    [Serializable]
    public class CustomControl : BaseControlType
    {
        private string dllName = "HISFC.Components.Common";
        [System.ComponentModel.Category("CC.自定义控件")]
        [System.ComponentModel.Description("需要查找的自定义控件dll名称")]
        public string DllName
        {
            get
            {
                return this.dllName;
            }
            set
            {
                this.dllName = value;
            }
        }

        private string typeName = "";
        [System.ComponentModel.Category("CC.自定义控件")]
        [System.ComponentModel.Description("需要查找的自定义控件type名称")]
        public string TypeName
        {
            get
            {
                return this.typeName;
            }
            set
            {
                this.typeName = value;
            }
        }

        private string valueProperty = "";
        [System.ComponentModel.Category("CC.自定义控件")]
        [System.ComponentModel.Description("需要从自动控件的哪个属性取值")]
        public string ValueProperty
        {
            get
            {
                return this.valueProperty;
            }
            set
            {
                this.valueProperty = value;
            }
        }

        private string eventProperty = "";
        [System.ComponentModel.Category("CC.自定义控件")]
        [System.ComponentModel.Description("回车后触发事件")]
        public string EventProperty
        {
            get
            {
                return this.eventProperty;
            }
            set
            {
                this.eventProperty = value;
            }
        }

        private string delegateDllName = "HISFC.Components.Common";
        [System.ComponentModel.Category("CC.自定义控件")]
        [System.ComponentModel.Description("需要查找的自定义事件dll名称")]
        public string DelegateDllName
        {
            get
            {
                return this.delegateDllName;
            }
            set
            {
                this.delegateDllName = value;
            }
        }


        private string delegateTypeName = "";
        [System.ComponentModel.Category("CC.自定义控件")]
        [System.ComponentModel.Description("需要查找的自定义事件type名称")]
        public string DelegateTypeName
        {
            get
            {
                return this.delegateTypeName;
            }
            set
            {
                this.delegateTypeName = value;
            }
        }

        public override string ToString()
        {
            return "自定义控件";
        }
    }
}
