using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neusoft.SOC.HISFC.OutpatientFee.Data
{
    /// <summary>
    /// [功能描述: 门诊支付方式数据抽象类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public abstract class AbstractPayMode : AbstractSql<AbstractPayMode>
    {

        /// <summary>
        /// 更新（日结）
        /// </summary>
        public abstract string UpdateForDayBalance
        {
            get;
        }

        /// <summary>
        /// 更新（取消日结）
        /// </summary>
        public abstract string UpdateForCancelDayBalance
        {
            get;
        }

        public override string SelectAll
        {
            get { throw new NotImplementedException(); }
        }

        public override string Insert
        {
            get { throw new NotImplementedException(); }
        }
    }
}
