
namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// UserText<br></br>
	/// [��������: �û��ı�ʵ��]<br></br>
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
    public class UserText : Spell
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public UserText()
		{

		}

		#region ����

		/// <summary>
		/// �ı�
		/// </summary>
		private string text;
		
		/// <summary>
		/// ����
		/// </summary>
		private string code;
		
		/// <summary>
		/// �����ı�
		/// </summary>
		private string richText;
		
		/// <summary>
		/// ����
		/// </summary>
		private string userTextType;
		
		/// <summary>
		/// �ͷű�־
		/// </summary>
		private bool alreadyDisposed = false;
		
		/// <summary>
		/// ������
		/// </summary>
		private FS.FrameWork.Models.NeuObject group = new FS.FrameWork.Models.NeuObject();
		
		#endregion
	
        #region ����
		
		/// <summary>
		/// ������
		/// </summary>
		public FS.FrameWork.Models.NeuObject Group
		{
			get
			{
				return this.group;
			}
			set
			{
				this.group = value;
			}
		}

		/// <summary>
		/// �ı�
		/// </summary>
		public string Text
	    {
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
        }

		/// <summary>
		/// ����
		/// </summary>
		public string Code
		{
			get
			{
				return this.code;
			}
			set
			{
				this.code = value ;
			}
		}

		/// <summary>
		/// ���ı�
		/// </summary>
		public string RichText
		{
			get
			{
				return this.richText;
			}
			set
			{
				this.richText = value;
			}
		}

		/// <summary>
		/// ����  --0 �û� 1 ���� 2 ROOT
		/// </summary>
		public string Type 
		{
			get
			{
				return this.userTextType;
			}
			set
			{
				this.userTextType = value;
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
			
			if (this.group != null)
			{
				this.group.Dispose();
				this.group = null;
			}

			base.Dispose(isDisposing);

			this.alreadyDisposed = true;
		}

		#endregion

		#region ��¡
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>��ǰ��ʵ���ĸ���</returns>
		public new UserText Clone()
		{
			UserText userText = base.Clone() as UserText;

			userText.Group = this.Group.Clone();

			return userText ;
		}

		#endregion

		#endregion

	}
}
