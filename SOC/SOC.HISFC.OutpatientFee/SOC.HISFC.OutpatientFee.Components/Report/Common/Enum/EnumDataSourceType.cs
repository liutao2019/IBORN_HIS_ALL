using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Enum
{
    [Serializable]
    public enum EnumDataSourceType
    {
        [FS.FrameWork.Public.Description("Sql语句")]
        Sql,
        [FS.FrameWork.Public.Description("自定义")]
        Custom,
        [FS.FrameWork.Public.Description("字典")]
        Dictionary,
        [FS.FrameWork.Public.Description("科室")]
        DepartmentType,
        [FS.FrameWork.Public.Description("人员")]
        EmployeeType,
        [FS.FrameWork.Public.Description("数据源")]
        DataSource,
        [FS.FrameWork.Public.Description("科室结构")]
        DepartStat
    }

}
