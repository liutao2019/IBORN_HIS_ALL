using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.ControlType;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting
{
    [Serializable]
    public class QueryControl
    {
        private int index = 0;
        [System.ComponentModel.DefaultValue(0)]
        [System.ComponentModel.Category("A.基本信息")]
        [System.ComponentModel.Description("控件的Index值")]
        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
            }
        }

        private string name = string.Empty;
        [System.ComponentModel.DefaultValue("")]
        [System.ComponentModel.Category("A.基本信息")]
        [System.ComponentModel.Description("控件的内置名称，不允许与其他控件相同")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        private string text = string.Empty;
        [System.ComponentModel.DefaultValue("")]
        [System.ComponentModel.Category("A.基本信息")]
        [System.ComponentModel.Description("控件的显示名称")]
        public string Text
        {

            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }


        private bool isAddText = true;
        [System.ComponentModel.DefaultValue(true)]
        [System.ComponentModel.Category("A.基本信息")]
        [System.ComponentModel.Description("是否添加显示名称标签")]
        public bool IsAddText
        {
            get
            {
                return isAddText;
            }
            set
            {
                isAddText = value;
            }
        }

        private BaseControlType controlType = new TextBoxType();
        [System.ComponentModel.Category("B.控件信息")]
        [System.ComponentModel.Description("控件设置")]
        public BaseControlType ControlType
        {
            get
            {
                return controlType;
            }
            set
            {
                controlType = value;
            }
        }
    }
}
