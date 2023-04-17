using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Enum;
using System.Collections;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.TypeConterter;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.ControlType
{

    [Serializable]
    public class ComboBoxType : BaseControlType
    {
        private bool isAddAll = true;
        [System.ComponentModel.DefaultValue(true)]
        [System.ComponentModel.Category("C.一般下拉框")]
        [System.ComponentModel.Description("是否增加'全部'选项，True增加，False不增加")]
        public bool IsAddAll
        {
            get
            {
                return isAddAll;
            }
            set
            {
                this.isAddAll = value;
            }
        }

        private FS.FrameWork.Models.NeuObject allValue = new FS.FrameWork.Models.NeuObject("ALL", "全部", "");
        [System.ComponentModel.Category("C.一般下拉框")]
        [System.ComponentModel.Description("增加'全部'选项时，全部选项的编码和名称")]
        [System.ComponentModel.TypeConverter(typeof(ControlTypeConverter))]
        public FS.FrameWork.Models.NeuObject AllValue
        {
            get
            {
                return this.allValue;
            }
            set
            {
                this.allValue = value;
            }
        }

        private ArrayList datasource = new ArrayList();
        public ArrayList DataSource
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

        private List<FS.FrameWork.Models.NeuObject> defaultDataSource = new List<FS.FrameWork.Models.NeuObject>();
        [System.ComponentModel.Category("C.一般下拉框")]
        [System.ComponentModel.Description("当数据源为Custom模式时，默认数据集合的定义")]
        public  List<FS.FrameWork.Models.NeuObject> DefaultDataSource
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

        public void SetGenericTypeValue(Object mainObj, Object genericValue, System.Reflection.PropertyInfo p)
        {
            if (p.Name == "DefaultDataSource" && genericValue != null)
            {
                List<FS.FrameWork.Models.NeuObject> list = new List<FS.FrameWork.Models.NeuObject>();
                foreach (FS.FrameWork.Models.NeuObject step in (System.Collections.ICollection)genericValue)
                {
                    list.Add(step);
                }

                p.SetValue(mainObj, list, null);
            }
        }

        public override string ToString()
        {
            return "下拉框";
        }
    }
}
