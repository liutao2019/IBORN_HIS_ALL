using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.OutpatientFee.FuYou
{
    /// <summary>
    /// [功能描述: 妇幼辅材计算费用的关键属性集合 ]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-03]<br></br>
    /// 说明：
    /// 1、仅限各项目可通用函数
    /// </summary>>
    public class SubFeeSet
    {

        /// <summary>
        /// 用法
        /// </summary>
        private string usageNO = "";

        /// <summary>
        /// 用法
        /// </summary>
        public string UsageNO
        {
            get { return usageNO; }
            set { usageNO = value; }
        }

        /// <summary>
        /// 数量
        /// </summary>
        private decimal qty = 0;

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Qty
        {
            get { return qty; }
            set { qty = value; }
        }
    }
}
