using FS.FrameWork.Models;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// PactFeeCodeRate<br></br>
	/// [��������: ��ͬ��λ��С���ñ���ʵ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class PactFeeCodeRate :NeuObject 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public PactFeeCodeRate( ) 
		{
			
		}

		#region ����

		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;

		/// <summary>
		/// ��ͬ��λ
		/// </summary>
		private FS.HISFC.Models.Base.Pact pact = new Pact();

		/// <summary>
		/// ��С����
		/// </summary>
		private FS.FrameWork.Models.NeuObject minFee = new NeuObject();

		/// <summary>
		/// ���ֱ���
		/// </summary>
		private FS.HISFC.Models.Base.FTRate rate = new FTRate();

		#endregion

		#region ����

		/// <summary>
		/// ��ͬ��λ
		/// </summary>
		public FS.HISFC.Models.Base.Pact Pact
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
		/// ��С����
		/// </summary>
		public FS.FrameWork.Models.NeuObject MinFee 
		{
			get
			{
				return this.minFee;
			}
			set
			{
				this.minFee = value;
			}
		}

		/// <summary>
		/// ���ֱ���
		/// </summary>
		public FS.HISFC.Models.Base.FTRate Rate
		{
			get
			{
				return this.rate;
			}
			set
			{
				this.rate = value;
			}
		}

		#endregion

		#region ����
		
		#region �ͷ���Դ
		
		/// <summary>
		/// �ͷ���Դ
		/// </summary>
		/// <param name="isDisposing"></param>
		protected override void Dispose(bool isDisposing)
		{
			if (this.alreadyDisposed)
			{
				return;
			}

			if (this.pact != null)
			{
				this.pact.Dispose();
				this.pact = null;
			}
			if (this.minFee != null)
			{
				this.minFee.Dispose();
				this.minFee = null;
			}
			if (this.rate != null)
			{
				this.rate.Dispose();
				this.rate = null;
			}

			base.Dispose(isDisposing);

			this.alreadyDisposed = true;
		}

		#endregion

		#region ��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>��ǰ�����ʵ���ĸ���</returns>
		public new PactFeeCodeRate Clone()
		{
			PactFeeCodeRate pactFeeCodeRate = base.Clone() as PactFeeCodeRate;

			pactFeeCodeRate.MinFee = this.MinFee.Clone();
			pactFeeCodeRate.Pact = this.Pact.Clone();
			pactFeeCodeRate.Rate = this.Rate.Clone();

			return pactFeeCodeRate;
		}

		#endregion

		#endregion
	}
}
