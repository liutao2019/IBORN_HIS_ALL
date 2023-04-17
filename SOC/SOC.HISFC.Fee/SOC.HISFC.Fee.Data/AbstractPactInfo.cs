using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.Data
{
    /// <summary>
    /// [功能描述: 合同单位数据抽象类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public abstract class AbstractPactInfo:AbstractSql<AbstractPactInfo>
    {
        /// <summary>
        /// 根据SortID排序
        /// </summary>
        public abstract string OrderBySortID
        {
            get;
        }

        /// <summary>
        /// 条件为使用系统
        /// </summary>
        public abstract string WhereBySystemType
        {
            get;
        }
    }
}
