namespace Neusoft.HISFC.Object.Base
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
	public class Bank : Neusoft.NFC.Object.NeuObject 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Bank()
		{
		}


		#region ����

		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;

		/// <summary>
		/// ����
		/// </summary>
		private FT fee = new FT();

		/// <summary>
		/// pos������ˮ�Ż�֧Ʊ��
		/// </summary>
		private string invoiceNO;

		/// <summary>
		/// �ʺ�
		/// </summary>
		private string account;

		/// <summary>
		/// ���ݵ�λ
		/// </summary>
		private string workName;

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

		#region �ͷ���Դ
		/// <summary>
		/// �ͷ�
		/// </summary>
		/// <param name="isDisposing">�Ƿ��Լ��ͷ�</param>
		protected override void Dispose(bool isDisposing)
		{
			if (this.alreadyDisposed)
			{
				return;
			}
			
			if (fee != null)
			{
				myoldData.Dispose();
			}
			
			base.Dispose( isDisposing );
			this.alreadyDisposed = true;
		}

		#endregion

		#region ��¡
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new Bank Clone()
		{
			Bank bank = new Bank();
			bank = base.Clone();
			obj.fee = this.fee.Clone();
			return obj;
		}
		#endregion

		#endregion

	}
}

