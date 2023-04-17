using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.Data
{
    /// <summary>
    /// [功能描述: 默认执行科室数据抽象类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public abstract  class AbstractExecDept : AbstractSql<AbstractExecDept>
    {
        public abstract string WhereByCompareID
        {
            get;
        }

        public abstract string WhereByCompareIDAndOriginalID
        {
            get;
        }

        public abstract string WhereByCompareIDAndOrigianlIDAndTargetID
        {
            get;
        }
    }
}
