using FS.FrameWork.Models;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// PactItemRate<br></br>
	/// [��������: ��ͬ��λ��Ŀ����]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class PactItemRate : Pact
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public PactItemRate( ) 
		{
	
		}

		#region ����

		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;

		/// <summary>
		/// ��ͬ��λ��Ӧ����Ŀ����С����
		/// </summary>
		private FS.FrameWork.Models.NeuObject pactItem = new NeuObject();

		/// <summary>
		/// ��Ŀ���: 0-��С����,1-ҩƷ,2-��ҩƷ
		/// </summary>
		private string itemType;

		/// <summary>
		/// ��ͬ��λ��Ӧ����Ŀ�ĸ��ֱ���
		/// </summary>
		private FS.HISFC.Models.Base.FTRate rate = new FTRate();

		/// <summary>
		/// ��������
		/// </summary>
		private OperEnvironment operEnvironment = new OperEnvironment();

		#endregion

		#region ����
		/// <summary>
		/// ��ͬ��λ��Ӧ����Ŀ����С����
		/// </summary>
		public FS.FrameWork.Models.NeuObject PactItem
		{
			get
			{
				return this.pactItem;
			}
			set
			{
				this.pactItem = value;
			}
		}

		/// <summary>
		/// ��Ŀ���: 0-��С����,1-ҩƷ,2-��ҩƷ
		/// </summary>
		public string ItemType
		{
			get
			{
				return this.itemType;
			}
			set
			{
				this.itemType = value;
			}
		}

		/// <summary>
		/// ��ͬ��λ��Ӧ����Ŀ�ĸ��ֱ���
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

		/// <summary>
		/// ��������
		/// </summary>
		public new OperEnvironment OperEnvironment
		{
			get
			{
				return this.operEnvironment;
			}
			set
			{
				this.operEnvironment = value;
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

			if (this.pactItem != null)
			{
				this.pactItem.Dispose();
				this.pactItem = null;
			}
			if (this.rate != null)
			{
				this.rate.Dispose();
				this.rate = null;
			}
			if (this.operEnvironment != null)
			{
				this.operEnvironment.Dispose();
				this.operEnvironment = null;
			}

			base.Dispose(isDisposing);

			this.alreadyDisposed = true;
		}

		#endregion

		#region ��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>��ǰ����ʵ���ĸ���</returns>
		public new PactItemRate  Clone()
		{
			PactItemRate pactItemRate = base.Clone() as PactItemRate;

			pactItemRate.OperEnvironment = this.OperEnvironment.Clone();
			pactItemRate.PactItem = this.PactItem.Clone();
			pactItemRate.Rate = this.Rate.Clone();

			return pactItemRate;
		}

		#endregion

		#endregion
	}
}
