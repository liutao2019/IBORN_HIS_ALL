using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOC.Local.Operation
{
    /// <summary>
    /// add-by cao-lin
    /// 手术室当日患者状态一览
    /// 1、等待手术
    /// 2、术中
    /// 3、术毕回ICU
    /// 4、术毕回科室
    /// </summary>
    public enum EnumOperationState
    { 
        未选择,
        等待手术,
        手术中,
        术毕回病房,
        术毕回麻恢室,
        术毕回ICU
    }
}
