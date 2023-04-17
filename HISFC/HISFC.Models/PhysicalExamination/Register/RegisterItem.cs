namespace Neusoft.HISFC.Object.PhysicalExamination.Register 
{
	/// <summary>
	/// RegisterItem <br></br>
	/// [��������: ���Ǽǿ���������շ���Ŀ��һ������������ײ�]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-11-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class RegisterItem : Neusoft.HISFC.Object.PhysicalExamination.Management.Item
	{
		#region ˽�б���
		
		/// <summary>
		/// ���Ǽ���Ϣ
		/// </summary>
		private Register register;

		/// <summary>
		/// ��������
		/// </summary>
		private decimal count;

		/// <summary>
		/// ��Ŀ�Ķ��Խ��
		/// </summary>
		private Neusoft.HISFC.Object.PhysicalExamination.Management.ItemQualitativeResult qualitativeResult;

		/// <summary>
		/// ��Ŀ��ִ����Ϣ
		/// </summary>
		private Neusoft.HISFC.Object.PhysicalExamination.Confirm.Confirm confirm;

		#endregion

		#region ����

		/// <summary>
		/// ���Ǽ���Ϣ
		/// </summary>
		public Register Register 
		{
			get 
			{
				return this.register;
			}
			set 
			{
				this.register = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public decimal Count
		{
			get
			{
				return this.count;
			}
			set
			{
				this.count = value;
			}
		}

		/// <summary>
		/// ��Ŀ�Ķ��Խ��
		/// </summary>
		public Neusoft.HISFC.Object.PhysicalExamination.Management.ItemQualitativeResult QualitativeResult
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

		/// <summary>
		/// ��Ŀ��ִ����Ϣ
		/// </summary>
		public Neusoft.HISFC.Object.PhysicalExamination.Confirm.Confirm Confirm
		{
			get
			{
				return this.confirm;
			}
			set
			{
				this.confirm = value;
			}
		}

		#endregion

		#region ����

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���Ǽǿ���������շ���Ŀ</returns>
		public new RegisterItem Clone()
		{
			RegisterItem registerItem = base.Clone() as RegisterItem;

			registerItem.Register = this.Register.Clone();
			registerItem.QualitativeResult = this.QualitativeResult.Clone();
			registerItem.Confirm = this.Confirm.Clone();
			
			return registerItem;
		}
		#endregion
	}
}
