
namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// ConstriantDictionary<br></br>
	/// [��������: ����Լ��ʵ��]<br></br>
	/// [�� �� ��: ����ȫ]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class ConstriantDictionary: FS.FrameWork.Models.NeuObject
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public ConstriantDictionary()
		{
		}


		#region ����

		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;

		/// <summary>
		/// �ֵ�����
		/// </summary>
		private string dictType ;

		/// <summary>
		/// �ֵ�ID
		/// </summary>
		private string dictID ;

		/// <summary>
		/// SQL״̬ UPDATE DELETE
		/// </summary>
		private string sqlType ;

		/// <summary>
		/// Լ������
		/// </summary>
		private string constraintSql ;

		/// <summary>
		/// ������Ϣ
		/// </summary>				
		private OperEnvironment operEnvironment = new OperEnvironment();	

		#endregion

		#region ����

		/// <summary>
		/// �ֵ�����
		/// </summary>
		public string Type
		{
			get
			{
				return this.dictType;
			}
			set
			{
				this.dictType = value;
			}
		}


		/// <summary>
		/// �ֵ�ID
		/// </summary>
		public string Id
		{
			get
			{
				return this.dictID;
			}
			set
			{
				this.dictID = value;
			}
		}


		/// <summary>
		/// SQL״̬ UPDATE DELETE
		/// </summary>
		public string SqlType
		{
			get
			{
				return this.sqlType;
			}
			set
			{
				this.sqlType = value;
			}
		}


		/// <summary>
		/// Լ������
		/// </summary>
		public string ConstraintSql
		{
			get
			{
				return this.constraintSql;
			}
			set
			{
				this.constraintSql = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public OperEnvironment OperEnvironment
		{
			get
			{
				return this.operEnvironment;
			}
			set
			{
				this.operEnvironment = value ;
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

			if (this.operEnvironment != null)
			{
				this.operEnvironment.Dispose();
				this.operEnvironment = null;
			}
		
			base.Dispose (isDisposing);

			this.alreadyDisposed = true;
		}

		#endregion

		#region ��¡

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>ConstriantDictionary��ʵ��</returns>
		public new ConstriantDictionary Clone()
		{
			return this.MemberwiseClone() as ConstriantDictionary;
		}

		#endregion

		#endregion

		

	}
}
