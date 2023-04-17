namespace Neusoft.HISFC.Object.PhysicalExamination.Management 
{
	/// <summary>
	/// Price <br></br>
	/// [��������: ���۸�ʵ��]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-11-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class Price : Neusoft.HISFC.Object.PhysicalExamination.Base.PE
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Price()
		{

		}

		#region ˽�б���

		/// <summary>
		/// �����շѼ۸񣬸������Ӧ����ȡ�ļ۸�
		/// </summary>
		private decimal individualPrice;

		/// <summary>
		/// ��˾���۸񣬹�˾�������Ӧ����ȡ�ļ۸�
		/// </summary>
		private decimal companyPrice;
		
		#endregion

		#region ����
		
		/// <summary>
		/// �����շѼ۸񣬸������Ӧ����ȡ�ļ۸�
		/// </summary>
		public decimal IndividualPrice 
		{
			get 
			{
				return this.individualPrice;
			}
			set 
			{
				this.individualPrice = value;
			}
		}

		/// <summary>
		/// ��˾���۸񣬹�˾�������Ӧ����ȡ�ļ۸�
		/// </summary>
		public decimal CompanyPrice 
		{
			get 
			{
				return this.companyPrice;
			}
			set 
			{
				this.companyPrice = value;
			}
		}

		#endregion

		#region ����
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���۸���</returns>
		public new Price Clone()
		{
			return base.Clone() as Price;
		}
		
		#endregion
	}
}
