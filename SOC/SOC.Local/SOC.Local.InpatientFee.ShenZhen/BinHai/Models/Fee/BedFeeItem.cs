using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.SOC.Local.InpatientFee.ShenZhen.BinHai.Models.Fee
{
    [System.Serializable]
    [System.ComponentModel.DisplayName("床位固定费用")]
	public class BedFeeItem	: FS.HISFC.Models.Fee.BedFeeItem,ISort,IValidState
	{
		#region 变量

        /// <summary>
        /// 使用限制
        /// </summary>
        private string useLimit;
	
		#endregion

		#region 属性

        /// <summary>
        /// 使用限制
        /// </summary>
        public string UseLimit
        {
            get
            {
                return this.useLimit;
            }
            set
            {
                this.useLimit = value;
            }
        }

		#endregion

        #region 克隆

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns>返回当前类的实例副本</returns>
        public new BedFeeItem Clone()
        {
            return base.Clone() as BedFeeItem;
        }

        #endregion
	}
}
