using System;

namespace FS.SOC.Local.InpatientFee.FuYou.Models
{
    /// <summary>
    /// PrepayStat 的摘要说明。
    /// TODO:
    /// 需要进一步讨论
    /// </summary>
    /// 
    [System.Serializable]
    public class PrepayDayBalance : FS.FrameWork.Models.NeuObject
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public string BeginDate;
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndDate;

        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal RealCost;

        /// <summary>
        /// 退费金额
        /// </summary>
        public decimal QuitCost;

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotCost;

        /// <summary>
        ///  预交现金
        /// </summary>
        public decimal CACost;
        /// <summary>
        /// POS机金额（刷卡）
        /// </summary>
        public decimal POSCost;
        /// <summary>
        /// 支票汇票
        /// </summary>
        public decimal CHCost;
        /// <summary>
        /// 预交汇票
        /// </summary>
        public decimal ORCost;
        /// <summary>
        /// 转押金
        /// </summary>
        public decimal FGCost;
        /// <summary>
        /// 票据区间
        /// </summary>
        public string BeginInvoice;
        /// <summary>
        /// 预交张数
        /// </summary>
        public int PrepayNum;

        /// <summary>
        /// 
        /// </summary>
        public int QuitNum;
        /// <summary>
        /// 预交作废票子号
        /// </summary>
        public string EndInvoice;

        /// <summary>
        /// 审核标志 0未审核 1已审核
        /// </summary>
        public string CheckFlag;

        /// <summary>
        /// 审核人信息
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment CheckOper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// 日结人信息
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment BalancOper = new FS.HISFC.Models.Base.OperEnvironment();

    }
}
