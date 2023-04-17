using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee 
{
	/// <summary>
	/// BalanceListBase<br></br>
	/// [��������: ���ý�����ϸ������ ID:��Ʊ��]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-09-06]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public abstract class BalanceListBase : NeuObject
	{
		#region ����
		
		/// <summary>
		/// ������Ϣ
		/// </summary>
		protected BalanceBase balanceBase;

		/// <summary>
		/// ͳ�ƴ���
		/// </summary>
		private FeeCodeStat feeCodeStat = new FeeCodeStat();

		#endregion

		#region ����
		
		/// <summary>
		/// ������Ϣ
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
		/// ͳ�ƴ���
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

		#region ����

		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ�����ʵ������</returns>
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
