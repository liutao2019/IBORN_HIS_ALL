using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee.Inpatient
{
	/// <summary>
	/// Balance<br></br>
	/// [��������: סԺ������ϸ��]<br></br>
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
	public class BalanceList : BalanceListBase
	{
		public BalanceList()
		{
			this.balanceBase = new Balance();
		}

		#region ����
		
		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ����ʵ������</returns>
		public new BalanceList Clone()
		{
			return base.Clone() as BalanceList;
		}

		#endregion

		#endregion
	}
}
