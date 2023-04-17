using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.Data
{
    /// <summary>
    /// [功能描述: 非药品数据抽象类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public abstract  class AbstractUndrug:AbstractSql<AbstractUndrug>
    {
        /// <summary>
        /// 根据组套标记获取非药品（简洁）
        /// WHERE   valid_state='1' and unitflag='0' ORDER BY INPUT_CODE
        /// </summary>
        public abstract string SelectValidBriefByUnitFlg
        {
            get;
        }

        /// <summary>
        /// 根据编码获取有效的
        /// WHERE   (item_code ='{0}'  Or 'All'='{0}' ) AND (valid_state ='{1}' Or 'All'='{1}') ORDER BY m.INPUT_CODE
        /// </summary>
        public abstract string WhereValidByItemCode
        {
            get;
        }

        /// <summary>
        /// 获取所有有效的组套信息
        /// WHERE   m.valid_state='1' AND (m.unitflag='1') ORDER BY m.INPUT_CODE
        /// </summary>
        public abstract string WhereValidByUnitFlag
        {
            get;
        }

        /// <summary>
        /// 获取所有有效的组套信息
        /// WHERE   m.valid_state='1' AND (m.unitflag='1') ORDER BY m.INPUT_CODE
        /// </summary>
        public abstract string WhereExistsByUserCode
        {
            get;
        }
    }
}
