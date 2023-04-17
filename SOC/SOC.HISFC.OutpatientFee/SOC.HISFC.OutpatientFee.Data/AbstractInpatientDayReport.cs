using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.OutpatientFee.Data
{
    /// <summary>
    /// [功能描述: 住院费用日结汇总数据抽象类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public abstract class AbstractInaptientDayReport : AbstractSql<AbstractInaptientDayReport>
    {
        public abstract string SelectLastDate
        {
            get;
        }

        public abstract string SelectByMonth
        {
            get;
        }

        public abstract string SelectDateByBalanceNO
        {
            get;
        }
    }
}
