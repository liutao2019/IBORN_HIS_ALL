namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// PayKind<br></br>
	/// [��������: �������<br></br>
	/// 01-�Է�,<br></br>
	/// 02-����,<br></br>
	/// 03-������ְ,<br></br>
	/// 04-��������,<br></br>
	/// 05-���Ѹ߸�]<br></br>
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
    public class PayKind : FS.FrameWork.Models.NeuObject
	{
		public PayKind()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����

		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;

		/// <summary>
		/// ҩƷ׷�ӱ���
		/// </summary>
		private float drugAdditionalRate = 1;

		/// <summary>
		/// ��ҩƷ׷�ӱ���
		/// </summary>
		private float undrugAdditionalRate = 1;

		#endregion

		#region ����
		/// <summary>
		/// ҩƷ׷�ӱ���
		/// </summary>
		public float DrugAdditionalRate
		{
			get
			{
				return this.drugAdditionalRate;
			}
			set
			{
				this.drugAdditionalRate = value;
			}
		}

		/// <summary>
		/// ��ҩƷ׷�ӱ���
		/// </summary>
		public float UndrugAdditionalRate
		{
			get
			{
				return this.undrugAdditionalRate;
			}
			set
			{
				this.undrugAdditionalRate = value;
			}
		}
		#endregion

		#region ����
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

			base.Dispose(isDisposing);

			this.alreadyDisposed = true;
		}

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>PayKind</returns>
		public new PayKind  Clone()
		{
			return base.Clone() as PayKind;
		}

		#endregion

	}
}
