namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// PrivInOutDept<br></br>
	/// [��������: �����Ȩ��ʵ��]<br></br>
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
    public class PrivInOutDept : FS.FrameWork.Models.NeuObject,  ISort
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public PrivInOutDept() 
		{
			
		}
        
		#region ����

		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;

		/// <summary>
		/// Ȩ��:0501-��⣬0502-����
		/// </summary>
		private PowerRole role = new PowerRole();
		
		/// <summary>
		/// ��ҩ��λ
		/// </summary>
		private FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ���
		/// </summary>
		private int sortID;

		#endregion

		#region ����

		/// <summary>
		/// 0501-��⣬0502-����
		/// </summary>
		public PowerRole Role 
		{
			get
			{ 
				return this.role; 
			}
			set
			{ 
				this.role = value; 
			}
		}

		/// <summary>
		/// ��ҩ����ҩ��λ��
		/// </summary>
		public FS.FrameWork.Models.NeuObject Dept 
		{
			get
			{ 
				return this.dept; 
			}
			set
			{ 
				this.dept = value; 
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

			if (this.dept != null)
			{
				this.dept.Dispose();
				this.dept = null;
			}

			base.Dispose (isDisposing);

			this.alreadyDisposed = true;
		}

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns></returns>
		public new PrivInOutDept Clone()
		{
			PrivInOutDept privInOutDept = base.Clone() as PrivInOutDept;

			privInOutDept.Dept = this.Dept.Clone();
			privInOutDept.Role = this.Role.Clone();

			return privInOutDept;
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
        
	}
		
}
