using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoShanYDSI.Objects
{
    /// <summary>
    /// 结算项目实体【中心】
    /// </summary>
    public class SICenterBalanceItem
    {
        /// <summary>
        /// 参保险种
        /// </summary>
        public string SiType = "";

        /// <summary>
        /// 参保险种名称
        /// </summary>
        public string SiName = "";

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName = "";

        /// <summary>
        /// 项目代码
        /// </summary>
        public string ItemCode = "";

        /// <summary>
        /// 有效开始日期
        /// </summary>
        public string BgnTime = "";

        /// <summary>
        /// 有效结束日期
        /// </summary>
        public string EndTime = "";

        /// <summary>
        /// 结算类型
        /// </summary>
        public string BalanceType = "";

        /// <summary>
        /// 结算类型名称
        /// </summary>
        public string BalanceTypeName = "";

    }
}
