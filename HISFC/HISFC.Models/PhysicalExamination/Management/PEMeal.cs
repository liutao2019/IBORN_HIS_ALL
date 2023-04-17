namespace Neusoft.HISFC.Object.PhysicalExamination.Management 
{
	/// <summary>
	/// PEMeal <br></br>
	/// [��������: ����ײ�]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-11-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class PEMeal : Neusoft.HISFC.Object.PhysicalExamination.Base.PE 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public PEMeal()
		{

		}

		#region ˽�б���
		
		/// <summary>
		/// �۸�
		/// </summary>
		private Price price;

		#endregion

		#region ����

		/// <summary>
		/// �۸�
		/// </summary>
		public Price Price 
		{
			get 
			{
				return this.price;
			}
			set 
			{
				this.price = value;
			}
		}

		#endregion

		#region ����
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new PEMeal Clone()
		{
			PEMeal peMeal = base.Clone() as PEMeal;
			
			peMeal.Price = this.Price.Clone();

			return peMeal;
		}
		#endregion
	}
}
