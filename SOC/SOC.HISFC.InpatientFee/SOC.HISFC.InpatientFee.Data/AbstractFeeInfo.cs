using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.InpatientFee.Data
{
    /// <summary>
    /// [功能描述: 费用汇总数据抽象类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public abstract class AbstractFeeInfo:AbstractSql<AbstractFeeInfo>
    {
        #region Insert

        /// <summary>
        /// 插入单个人所有的费用汇总信息（不在汇总表的，为身份变更用）
        /// </summary>
        public abstract string InsertAllDetailNotInFeeInfo
        {
            get;
        }

        #endregion

        #region Select

        /// <summary>
        /// 查询汇总费用（不在汇总表的所有总和）
        /// </summary>
        public abstract string SelectSumFeeByNotInFeeInfo
        {
            get;
        }

        #endregion

        #region Update

        /// <summary>
        /// 更新患者主表费用信息
        /// </summary>
        public abstract string UpdateMainFee
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

        #endregion

        #region Procedue

        /// <summary>
        /// 存储过程拆分费用
        /// </summary>
        public abstract string ProcedueSplitFee
        {
            get;
        }
        /// <summary>
        /// 存储过程合并费用
        /// </summary>
        public abstract string ProcedueCombineFee
        {
            get;
        }

        /// <summary>
        /// 存储过程处理身份变更的费用
        /// </summary>
        public abstract string ProcedueChangePactFee
        {
            get;
        }
        #endregion
    }
}
