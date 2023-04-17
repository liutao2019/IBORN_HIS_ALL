namespace Neusoft.HISFC.Object.PhysicalExamination.Register 
{
	/// <summary>
	/// SumCost <br></br>
	/// [��������: �ܽ��]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-11-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class SumCost : Neusoft.HISFC.Object.PhysicalExamination.Base.PE
	{
		#region ˽�б���

		/// <summary>
		/// ���Ǽǵ��ܽ��
		/// </summary>
		private decimal registerCost;

		/// <summary>
		/// ����շѵ�ʵ���ܽ��
		/// </summary>
		private decimal chargeCost;

		#endregion

		#region ����

		/// <summary>
		/// ���Ǽǵ��ܽ��
		/// </summary>
		public decimal RegisterCost 
		{
			get 
			{
				return this.registerCost;
			}
			set 
			{
				this.registerCost = value;
			}
		}

		/// <summary>
		/// ����շѵ�ʵ���ܽ��
		/// </summary>
		public decimal ChargeCost
		{
			get
			{
				return this.chargeCost;
			}
			set
			{
				this.chargeCost = value;
			}
		}

		#endregion

		#region ����
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>�ܽ��</returns>
		public new SumCost Clone()
		{
			return base.Clone() as SumCost;
		}
		#endregion
	}
}
