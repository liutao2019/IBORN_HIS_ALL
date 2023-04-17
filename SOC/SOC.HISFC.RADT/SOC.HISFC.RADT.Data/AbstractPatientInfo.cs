using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.RADT.Data
{
    /// <summary>
    /// [功能描述: 住院主表信息数据抽象类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public abstract class AbstractPatientInfo : AbstractSql<AbstractPatientInfo>
    {
        /// <summary>
        /// 更新住院主表的合同单位信息（包括公费信息）
        /// </summary>
        public abstract string UpdatePactInfo
        {
            get;
        }
    }
}
