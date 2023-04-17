using System;

namespace Neusoft.HISFC.Object.PhysicalExamination.Register
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
	public class Register : Neusoft.HISFC.Object.PhysicalExamination.Base.PE
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
		private Neusoft.HISFC.Object.PhysicalExamination.Base.PE invoice;

		/// <summary>
		/// ��ͬ��λ
		/// </summary>
		private Neusoft.NFC.Object.NeuObject pact;

		/// <summary>
		/// ��������
		/// </summary>
		private Neusoft.HISFC.Object.PhysicalExamination.HealthArchieve.HealthArchieve archieve;

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
		public Neusoft.HISFC.Object.PhysicalExamination.Base.PE Invoice
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
		public Neusoft.NFC.Object.NeuObject Pact
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
		public Neusoft.HISFC.Object.PhysicalExamination.HealthArchieve.HealthArchieve Archieve
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
