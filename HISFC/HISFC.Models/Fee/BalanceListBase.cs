using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee 
{
	/// <summary>
	/// BalanceListBase<br></br>
	/// [功能描述: 费用结算明细抽象类 ID:发票号]<br></br>
	/// [创 建 者: 飞斯]<br></br>
	/// [创建时间: 2006-09-06]<br></br>
	/// <修改记录
	///		修改人=''
	///		修改时间='yyyy-mm-dd'
	///		修改目的=''
	///		修改描述=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public abstract class BalanceListBase : NeuObject
	{
		#region 变量

        /// <summary>
        /// 院区id
        /// </summary>
        private string hospital_id;

        /// <summary>
        /// 院区名
        /// </summary>
        private string hospital_name;

		/// <summary>
		/// 结算信息
		/// </summary>
		protected BalanceBase balanceBase;

		/// <summary>
		/// 统计大类
		/// </summary>
		private FeeCodeStat feeCodeStat = new FeeCodeStat();

		#endregion

		#region 属性

        /// <summary>
        ///院区id
        /// </summary>
        public string Hospital_id
        {
            get
            {
                return this.hospital_id;
            }
            set
            {
                this.hospital_id = value;
            }
        }


        /// <summary>
        /// 院区名
        /// </summary>
        public string Hospital_name
        {
            get
            {
                return this.hospital_name;
            }
            set
            {
                this.hospital_name = value;
            }
        }
		/// <summary>
		/// 结算信息
		/// </summary>
		public BalanceBase BalanceBase
		{
			get
			{
				return this.balanceBase;
			}
			set
			{
				this.balanceBase = value;
			}
		}

		/// <summary>
		/// 统计大类
		/// </summary>
		public FeeCodeStat FeeCodeStat
		{
			get
			{
				return this.feeCodeStat;
			}
			set
			{
				this.feeCodeStat = value;
			}
		}

		#endregion

		#region 方法

		#region 克隆
		
		/// <summary>
		/// 克隆
		/// </summary>
		/// <returns>返回当前对象的实例副本</returns>
		public new BalanceListBase Clone()
		{
			BalanceListBase balanceListBase = base.Clone() as BalanceListBase;

			balanceListBase.BalanceBase = this.BalanceBase.Clone();
			balanceListBase.FeeCodeStat = this.FeeCodeStat.Clone();

			return balanceListBase;
		}

		#endregion

		#endregion
	}
}
