using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.Data
{
    /// <summary>
    /// [功能描述: 非药品组套数据抽象类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public abstract class AbstractUndrugGroup : AbstractSql<AbstractUndrugGroup>
    {
        /// <summary>
        /// 根据ID获取组套明细信息
        /// where  package_code='{0}' 
        /// </summary>
        public abstract string WhereByID
        {
            get;
        }


        /// <summary>
        /// 根据ID获取组套信息
        /// where  item_code='{0}' 
        /// </summary>
        public abstract string WhereByDetailID
        {
            get;
        }
    }
}
