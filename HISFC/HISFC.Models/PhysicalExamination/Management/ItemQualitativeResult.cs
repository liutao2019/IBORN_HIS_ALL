namespace Neusoft.HISFC.Object.PhysicalExamination.Management 
{
	/// <summary>
	/// ItemQualitativeResult <br></br>
	/// [��������: �����Ŀ���Խ��]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-11-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class ItemQualitativeResult : Neusoft.HISFC.Object.PhysicalExamination.Base.PE
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public ItemQualitativeResult()
		{

		}

		#region ˽�б���
		
		/// <summary>
		/// �����Ŀ
		/// </summary>
		private Item item;

		/// <summary>
		/// ���Խ��
		/// </summary>
		private string qualitativeResult;

		#endregion

		#region ����

		/// <summary>
		/// �����Ŀ
		/// </summary>
		public Item Item 
		{
			get 
			{
				return this.item;
			}
			set 
			{
				this.item = value;
			}
		}

		/// <summary>
		/// ���Խ��
		/// </summary>
		public string QualitativeResult
		{
			get
			{
				return this.qualitativeResult;
			}
			set
			{
				this.qualitativeResult = value;
			}
		}

		#endregion

		#region ����
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>��Ŀ���Խ��</returns>
		public new ItemQualitativeResult Clone()
		{
			ItemQualitativeResult itemQualitativeResult = base.Clone() as ItemQualitativeResult;

			itemQualitativeResult.Item = this.Item.Clone();

			return itemQualitativeResult;
		}

		#endregion
	}
}
