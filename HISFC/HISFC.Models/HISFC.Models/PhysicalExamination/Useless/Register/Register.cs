using System;

namespace FS.HISFC.Models.PhysicalExamination.Useless.Register
{
	/// <summary>
	/// Register <br></br>
	/// <br> ID - ������ </br>
	/// <br> Name - �˿����� </br>
	/// [��������: ���Ǽ�]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-11-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class Register : FS.HISFC.Models.PhysicalExamination.Base.PE
	{

		/// <summary>
		/// ���캯��
		/// </summary>
		public Register()
		{
		}

		#region ����


		/// <summary>
		/// �����ܽ��
		/// </summary>
		private SumCost sumCost;

		/// <summary>
		/// ����շѵķ�Ʊ��Ϣ
		/// </summary>
		private FS.HISFC.Models.PhysicalExamination.Base.PE invoice;

		/// <summary>
		/// ��ͬ��λ
		/// </summary>
		private FS.FrameWork.Models.NeuObject pact;

		/// <summary>
		/// ��������
		/// </summary>
		private FS.HISFC.Models.PhysicalExamination.HealthArchieve.HealthArchieve archieve;

		#endregion

		#region ����


		/// <summary>
		/// �����ܽ��
		/// </summary>
		public SumCost SumCost
		{
			get
			{
				return this.sumCost;
			}
			set
			{
				this.sumCost = value;
			}
		}

		/// <summary>
		/// ����շѵķ�Ʊ��Ϣ
		/// </summary>
		public FS.HISFC.Models.PhysicalExamination.Base.PE Invoice
		{
			get
			{
				return this.invoice;
			}
			set
			{
				this.invoice = value;
			}
		}

		/// <summary>
		/// ��ͬ��λ
		/// </summary>
		public FS.FrameWork.Models.NeuObject Pact
		{
			get
			{
				return this.pact;
			}
			set
			{
				this.pact = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public FS.HISFC.Models.PhysicalExamination.HealthArchieve.HealthArchieve Archieve
		{
			get
			{
				return this.archieve;
			}
			set
			{
				this.archieve = value;
			}
		}

		#endregion

		#region ����


		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���Ǽ�</returns>
		public new Register Clone()
		{
			Register register = base.Clone() as Register;

			register.Invoice = this.Invoice;
			register.SumCost = this.SumCost.Clone();
			register.Pact = this.Pact.Clone();
			register.Archieve = this.Archieve.Clone();

			return register;
		}
		#endregion

	}
}
