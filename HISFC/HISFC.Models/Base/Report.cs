namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// Record<br></br>
	/// [��������: ����ʵ��]<br></br>
	/// [�� �� ��: ����ΰ]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class Report : FS.FrameWork.Models.NeuObject,  IValid,  ISort
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Report()
		{

		}

		#region ����

		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;
		
		/// <summary>
		/// ����������
		/// </summary>
		private string parentCode;
		
		/// <summary>
		/// ������
		/// </summary>
		private string currentCode;
		
		/// <summary>
		/// �ؼ�����
		/// </summary>
		private string ctrlName;
		
		/// <summary>
		/// ���ò���
		/// </summary>
		private string parm;
		
		/// <summary>
		/// �Ƿ���Ч
		/// </summary>
		private bool isValid;
		
		/// <summary>
		/// ���
		/// </summary>
		private int sortID;
		
		/// <summary>
		/// ������
		/// </summary>
		private string specialFlag;

		#endregion

		#region ����

		/// <summary>
		/// ����������
		/// </summary>
		public string ParentCode 
		{
			get
			{
				return this.parentCode;
			}
			set
			{
				this.parentCode = value;
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public string CurrentCode
		{
			get
			{
				return this.currentCode;
			}
			set
			{
				this.currentCode = value;
			}
		}

		/// <summary>
		/// �ؼ�����
		/// </summary>
		public string CtrlName
		{
			get
			{
				return this.ctrlName;
			}
			set
			{
				this.ctrlName = value;
			}
		}

		/// <summary>
		/// ���ò���
		/// </summary>
		public string Parm
		{
			get
			{
				return this.parm;
			}
			set
			{
				this.parm = value;
			}
		}
 
		/// <summary>
		/// ������
		/// </summary>
		public string SpecialFlag
		{
			get
			{
				return this.specialFlag;
			}
			set
			{
				this.specialFlag = value;
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

			base.Dispose(isDisposing);

			this.alreadyDisposed = true;
		}

		#endregion

		#region ��¡
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new Report Clone()
		{
			return base.Clone() as Report;
		}

		#endregion

		#endregion

		#region �ӿ�ʵ��
		
		#region IValid ��Ա
		
		/// <summary>
		/// �Ƿ���Ч
		/// </summary>
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value ;
			}
		}

		#endregion
		
		#region ISort ��Ա
		
		/// <summary>
		/// ���
		/// </summary>
		public int SortID
		{
			get
			{
				return this.sortID;
			}
			set
			{
				this.sortID = value;
			}
		}

		#endregion

		#endregion

	}
}
