using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.OutpatientFee.Data
{
    /// <summary>
    /// [功能描述: 门诊费用日结数据抽象类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public abstract class AbstractFeeDayReport : AbstractSql<AbstractFeeDayReport>
    {
        public abstract string SelectLastDate
        {
            get;
        }

        public abstract string SelectByMonth
        {
            get;
        }

        public abstract string SelectLastFeeDate
        {
            get;
        }

        public abstract string SelectDateByBalanceNO
        {
            get;
        }
    }
}
