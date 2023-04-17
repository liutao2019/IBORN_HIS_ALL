namespace FS.HISFC.Models.Base
{  
	/// <summary>
	/// PageSize<br></br>
	/// [��������: ��ӡֽ�Ŵ�С�� ID ����(��ˮ��) Name ֽ������(���Զ���),Ĭ��A4�Ĵ�С]<br></br>
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
    public class PageSize : FS.FrameWork.Models.NeuObject
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public PageSize()
		{

		}

		/// <summary>
		/// �ļ�������������ӡֽ�Ŵ�С�� ID ����(��ˮ��) Name ֽ������(���Զ���)
		/// </summary>
		/// <param name="pageName">ֽ������</param>
		/// <param name="width">��</param>
		/// <param name="height">��</param>
		public PageSize(string pageName, int width, int height)
		{
			this.Name = pageName;
			this.Width = width;
			this.Height = height;
		}

		#region ����

		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;
	
		/// <summary>
		/// ���ظ߶�
		/// </summary>
		protected int height = 1145;

		/// <summary>
		/// ���ؿ��
		/// </summary>
		protected int width = 901;

		/// <summary>
		/// ���׿��
		/// </summary>
		protected float widthMM = 0f;

		/// <summary>
		/// ���׸߶�
		/// </summary>
		protected float heightMM = 0f;

		/// <summary>
		/// �ϱ߾�
		/// </summary>
		private int top = 0;

		/// <summary>
		/// ��߾�
		/// </summary>
		private int left = 0;

		/// <summary>
		/// ��ӡ������
		/// </summary>
		private string printer = "";

		/// <summary>
		/// ��������
		/// </summary>
		private OperEnvironment operEnvironment = new OperEnvironment();

		/// <summary>
		/// �Ƿ��Զ�ת��
		/// </summary>
		private bool isAutoConvert = true;
 
		/// <summary>
		/// ����-ȫ��All
		/// </summary>
		protected FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();

		#endregion

		#region ����

		/// <summary>
		/// ֽ�Ÿ߶�
		/// </summary>
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				if (value <= 0)
				{
					return;
				}
				
				if (this.isAutoConvert)
				{
					this.heightMM = this.ConvertPixelToMM(value);
				}
				
				this.height = value;
			}
		}

		/// <summary>
		/// ֽ�ſ��
		/// </summary>
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				if (value <= 0)
				{
					return;
				}
				
				if (isAutoConvert)
				{
					this.widthMM = this.ConvertPixelToMM(value);
				}
				
				this.width = value;
			}

		}

		/// <summary>
		/// ֽ�ź��׿��
		/// </summary>
		public float WidthMM 
		{
			get
			{
				return this.widthMM;
			}
			set
			{
				if (value <= 0f)
				{
					return;
				}
				
				if (isAutoConvert)
				{
					this.width = this.ConvertMMToPixel(value);
				}
				
				this.widthMM = value;
			}
		}

		/// <summary>
		/// ֽ�ź��׸߶�
		/// </summary>
		public float HeightMM
		{
			get
			{
				return this.heightMM;
			}
			set
			{
				if (value <= 0f)
				{
					return;
				}
				
				if (isAutoConvert)
				{
					this.height = this.ConvertMMToPixel(value);
				}
				
				this.heightMM = value;
			}
		}

		/// <summary>
		/// ����-ȫ��All
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
				
				if (value.ID == "ALL" && this.dept.Name == "")
				{
					this.dept.Name = "ȫ��";
				}
			}
		}

		/// <summary>
		/// ��ӡ��
		/// </summary>
		public string Printer 
		{
			get
			{
				return this.printer;
			}
			set
			{
				this.printer = value;
			}
		}

		/// <summary>
		/// �ϱ߾�
		/// </summary>
		public int Top
		{
			get
			{
				return this.top;
			}
			set
			{
				this.top = value;
			}
		}

		/// <summary>
		/// ��߾�
		/// </summary>
		public int Left 
		{
			get
			{
				return this.left;
			}
			set
			{
				this.left = value;
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
				this.operEnvironment = value;
			}
		}

		/// <summary>
		/// �Ƿ��Զ�ת��
		/// </summary>
		public bool IsAutoConvert
		{
			get
			{
				return this.isAutoConvert;
			}
			set
			{
				this.isAutoConvert = value;
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
			if (this.dept != null)
			{
				this.dept.Dispose();
				this.dept = null;
			}

			base.Dispose(isDisposing);

			this.alreadyDisposed = true;
		}

		#endregion

		#region ��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>��ǰ���ʵ���ĸ���</returns>
		public new PageSize Clone()
		{
			PageSize pageSize = base.Clone() as PageSize;

			pageSize.Dept = this.Dept.Clone();
			pageSize.OperEnvironment = this.OperEnvironment.Clone();

			return pageSize;
		}

		#endregion

        #region ���з���

		/// <summary>
		/// ת�����ص�����
		/// </summary>
		/// <param name="pixel">����ֵ</param>
		/// <returns>���׳���</returns>
		public float ConvertPixelToMM(int pixel)
		{
			if (pixel > 0)
			{
				return (float)(pixel / 3.78);	
			}
			else
			{
				return 0f;
			}
		}

		/// <summary>
		/// ת�����׵�����
		/// </summary>
		/// <param name="mm">���׳���</param>
		/// <returns>����ֵ</returns>
		public int ConvertMMToPixel(float mm)
		{
			if (mm > 0)
			{
				return (int)(mm * 3.78);	
			}
			else
			{
				return 0;
			}
		}

		#endregion

		#endregion
	}
}
