using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.InpatientFee.Data
{
    /// <summary>
    /// [功能描述: 规则收费数据抽象类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public abstract class AbstractFeeRegular : AbstractSql<AbstractFeeRegular>
    {
        /// <summary>
        /// 根据ItemCode获取规则收费数据
        /// WHERE ITEM_CODE = '{0}'
        /// </summary>
        public abstract string WhereByItemCode
        {
            get;
        }
    }
}
