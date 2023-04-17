using System;
namespace FS.HISFC.Models.Base
{	
	/// <summary>
	/// Bank<br></br>
	/// [��������: ����ʵ��]<br></br>
	/// [�� �� ��: ����ȫ]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class Bank : FS.FrameWork.Models.NeuObject 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Bank()
		{
		}


		#region ����

	

		/// <summary>
		/// ����
		/// </summary>
		private FT fee = new FT();

		/// <summary>
		/// pos������ˮ�Ż�֧Ʊ��
		/// </summary>
		private string invoiceNO = string.Empty;

		/// <summary>
		/// �ʺ�
		/// </summary>
		private string account = string.Empty;

		/// <summary>
		/// ���ݵ�λ
		/// </summary>
		private string workName = string.Empty;

		#endregion

		#region ����

		/// <summary>
		/// ����
		/// </summary>
		public FT Fee
		{
			get
			{
				return this.fee;
			}
			set
			{
				this.fee = value;
			}
		}


		/// <summary>
		/// pos������ˮ�Ż�֧Ʊ��
		/// </summary>
		public string InvoiceNO
		{
			get
			{
				return this.invoiceNO;
			}
			set
			{
				this.invoiceNO = value;
			}
		}


		/// <summary>
		/// �ʺ�
		/// </summary>
		public string Account
		{
			get
			{
				return this.account;
			}
			set
			{
				this.account = value;
			}
		}


		/// <summary>
		/// ���ݵ�λ
		/// </summary>
		public string WorkName
		{
			get
			{
				return this.workName;
			}
			set
			{
				this.workName = value;
			}
		}

		#endregion

		#region ����

		

		#region ��¡
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new Bank Clone()
		{
			Bank bank ;
			bank = base.Clone() as Bank;
			bank.fee = this.fee.Clone();
			return bank;
		}
		#endregion

		#endregion

		

	}
}
