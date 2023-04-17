
namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// Logo<br></br>
	/// [��������: ������־ʵ��]<br></br>
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
    public class Logo : FS.FrameWork.Models.NeuObject
	{
		public Logo()
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
		/// ���ݿ����
		/// </summary>
        private string dbCode = "";

		/// <summary>
		/// sqlcode
		/// </summary>
        private string sqlCode = "";

		/// <summary>
		/// ������Ϣ
		/// </summary>
        private string sqlError = "";

		/// <summary>
		/// ģ��
		/// </summary>
		/// 
        private string modual = "";

		/// <summary>
		/// ����
		/// </summary>
        private string codeDescription;
        
		/// <summary>
		/// ��������
		/// </summary>
        private int debugType = 0;

		#endregion

		#region ����

		/// <summary>
		/// ���ݿ������
		/// </summary>
        public string DBCode
        {
            get
            {
                return this.dbCode;
            }
            set
            {
                this.dbCode = value;
            }
        }

		/// <summary>
		/// ִ�е�sql
		/// </summary>
        public string SqlCode
        {
            get
            {
                return this.sqlCode;
            }
            set
            {
                this.sqlCode = value;
            }
        }
        
		/// <summary>
		/// ִ�е�sql error
		/// </summary>
        public string SqlError
        {
            get
            {
                return this.sqlError;
            }
            set
            {
                this.sqlError = value;
            }

        }
        
		/// <summary>
		/// ���е�modual
		/// </summary>
        public string Modual
        {
            get
            {
                return this.modual;
            }
            set
            {
                this.modual = value;
            }
        }
        
        
		/// <summary>
		/// 0 debug 1 error
		/// </summary>
        public int DebugType
        {
            get
            {
                return this.debugType;
            }
            set
            {
                this.debugType = value;
            }
        }

		/// <summary>
		/// ��������
		/// </summary>
		public new string CodeDescription
		{
			get
			{
				return this.codeDescription;
			}
			set
			{
				this.codeDescription = value;
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

			base.Dispose (isDisposing);

			this.alreadyDisposed = true;
		}

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>Logo</returns>
		public new Logo Clone()
		{
			Logo logo = base.Clone() as Logo;

			return logo;
		}

		#endregion

	}
}
