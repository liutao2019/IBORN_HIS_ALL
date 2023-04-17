using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.InpatientFee.Data
{
    /// <summary>
    /// [功能描述: 非药品费用明细数据抽象类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public abstract  class AbstractItemList:AbstractSql<AbstractItemList>
    {
        #region Select
        
        /// <summary>
        /// 获取所有最新的费用信息
        /// </summary>
        public abstract string SelectAllNewFee
        {
            get;
        }

        /// <summary>
        /// 获取未拆分的费用信息
        /// {0} 住院号
        /// {1} 开始时间
        /// {2} 结束时间
        /// </summary>
        public abstract string SelectNoSplitFeeItemListCount
        {
            get;
        }

        #endregion

        #region Insert

        /// <summary>
        /// 插入负的非药品费用明细
        /// </summary>
        public abstract string InsertOldFee
        {
            get;
        }

        /// <summary>
        /// 插入新的非药品费用明细
        /// </summary>
        public abstract string InsertNewFee
        {
            get;
        }
        
        #endregion

        #region Update

        /// <summary>
        /// 更新可退数量（更新患者当前所有有效费用的可退数量）
        /// </summary>
        public abstract string UpdateNoBackNumByInPatientNO
        {
            get;
        }

        /// <summary>
        /// 更新患者所有费用的费用来源
        /// </summary>
        public abstract string UpdateFTSource
        {
            get;
        }

        /// <summary>
        /// 更新非药品费用的金额信息
        /// </summary>
        public abstract string UpdateFTCost
        {
            get;
        }

        #endregion
    }
}
