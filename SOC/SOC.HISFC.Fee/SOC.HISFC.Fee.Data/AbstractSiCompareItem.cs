using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.Data
{
    /// <summary>
    /// [功能描述: 合同单位明细对照数据抽象类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public abstract class AbstractSiCompareItem:AbstractSql<AbstractSiCompareItem>
    {
        ///// <summary>
        ///// 获取项目的类型 1：本地姓名 2：医保中心项目
        ///// </summary>
        //public abstract int ItemType
        //{
        //    get;
        //    set;
        //}
        /// <summary>
        /// 获取所有项目（本地项目 ）
        /// </summary>
        public abstract String QueryLocalItems
        {
            get;
        }
        /// <summary>
        /// 获取所有项目（医保中心项目）
        /// </summary>
        public abstract String QueryCenterItems
        {
            get;
        }


        public abstract string QueryItemsByType
        {
            get;
        }
        public abstract string QueryCenterItemsByType
        {
            get;
        }

        /// <summary>
        /// 获取已对照下项目 (针对同一项目的不同合同单位的已对照项目)
        /// </summary>
        public abstract String QueryComparedItems
        {
            get;
        }

        ///// <summary>
        ///// 医保类型
        ///// </summary>
        //public abstract string CenterType
        //{
        //    get;
        //    set;
        //}
        /// <summary>
        /// 根据医保类型获取合同单位列表
        /// </summary>
        public abstract string QueryPacts
        {
            get;
        }
        public abstract string QueryCenterTypes
        {
            get;
        }
    }
}
