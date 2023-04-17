using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.UITypeEditor;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.TypeConverter;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.ControlType;
using System.Drawing;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting
{
    [Serializable]
    [Editor(typeof(SetInfoUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
    [TypeConverter(typeof(ListTypeConverter))]
    public class QueryCondition
    {
        private List<QueryControl> list = new List<QueryControl>();
        public List<QueryControl> List
        {
            get
            {
                return list;
            }
            set
            {
                list = value;
            }
        }

        private List<QueryDataSource> dataSourceType = new List<QueryDataSource>();
        public List<QueryDataSource> QueryDataSource
        {
            get
            {
                return this.dataSourceType;
            }
            set
            {
                this.dataSourceType = value;
            }
        }

        public void SetGenericTypeValue(Object mainObj, Object genericValue, System.Reflection.PropertyInfo p)
        {
            if (p.Name == "List" && genericValue != null)
            {
                List<QueryControl> list = new List<QueryControl>();
                foreach (QueryControl step in (System.Collections.ICollection)genericValue)
                {
                    list.Add(step);
                }

                p.SetValue(mainObj, list, null);
            }
            else if (p.Name == "QueryDataSource" && genericValue != null)
            {
                List<QueryDataSource> dataSourceType = new List<QueryDataSource>();
                foreach (QueryDataSource step in (System.Collections.ICollection)genericValue)
                {
                    dataSourceType.Add(step);
                }

                p.SetValue(mainObj, dataSourceType, null);
            }
        }

        private string filePath = "";
        /// <summary>
        /// 查询条件的配置文件
        /// </summary>
        public string QueryFilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
            }
        }

        public List<QueryControl> GetDefaults()
        {
            List<QueryControl> listDefault = new List<QueryControl>();
            QueryControl commonDefault = new QueryControl();
            commonDefault.Index = 0;
            commonDefault.Name = "dtBeginTime";
            commonDefault.Text = "开始时间：";
            DateTimeType dt = new DateTimeType();
            dt.AddDays = -1;
            dt.Location = new Point(10, 2);
            commonDefault.ControlType = dt;
            listDefault.Add(commonDefault);

            commonDefault = new QueryControl();
            commonDefault.Index = 0;
            commonDefault.Name = "dtEndTime";
            commonDefault.Text = "结束时间：";
            dt = new DateTimeType();
            dt.Location = new Point(220, 2);
            commonDefault.ControlType = dt;
            listDefault.Add(commonDefault);

            return listDefault;
        }

        public Dictionary<string, Object> GetDefualtSetting()
        {
            Dictionary<string, Object> map = new Dictionary<string, object>();
            map.Add("CurrentOperID", FS.FrameWork.Management.Connection.Operator.ID);
            map.Add("CurrentOperName", FS.FrameWork.Management.Connection.Operator.Name);
            map.Add("HospitalID", FS.FrameWork.Management.Connection.Hospital.ID);
            map.Add("HospitalName", FS.FrameWork.Management.Connection.Hospital.Name);
            if (FS.FrameWork.Management.Connection.Operator is FS.HISFC.Models.Base.Employee)
            {
                FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

                map.Add("CurrentDeptID", employee.Dept.ID);
                map.Add("CurrentDeptName", SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetDepartmentName(employee.Dept.Name));
            }

            return map;
        }

        public override string ToString()
        {
            return filePath;
        }
    }
}
