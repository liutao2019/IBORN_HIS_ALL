namespace FS.HISFC.Models.PhysicalExamination.Management 
{
	/// <summary>
	/// PEMeal <br></br>
	/// [��������: ����ײ�]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-11-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class PEMeal : FS.HISFC.Models.PhysicalExamination.Base.PE 
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
