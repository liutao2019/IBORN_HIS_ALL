
namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// Spell<br></br>
	/// [��������: ������ʵ��]<br></br>
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
    public class Spell : FS.FrameWork.Models.NeuObject, FS.HISFC.Models.Base.ISpell 
	{

		/// <summary>
		/// ���캯��
		/// </summary>
		public Spell( ) 
		{

		}

		#region ����
		
		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;
		
		/// <summary>
		/// ƴ����
		/// </summary>
		protected string spellCode;
		
		/// <summary>
		/// �����
		/// </summary>
		protected string wubiCode;
		
		/// <summary>
		/// �Զ�����
		/// </summary>
		protected string userCode;
		
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
		/// <returns>��ǰ��ʵ���ĸ���</returns>
		public new Spell Clone()
		{
			return base.Clone() as Spell;
		}

		#endregion

		#endregion

		#region �ӿ�ʵ��

		#region ISpellCode ��Ա

		/// <summary>
		/// ƴ����
		/// </summary>
		public string SpellCode
		{
			get
			{				
				return this.spellCode ;
			}
			set
			{
				this.spellCode = value;
			}
		}

		/// <summary>
		/// �����
		/// </summary>
		public string WBCode
		{
			get
			{
				return this.wubiCode ;
			}
			set
			{
				this.wubiCode = value;
			}
		}

		/// <summary>
		/// �Զ�����
		/// </summary>
		public string UserCode
		{
			get
			{
				return this.userCode ;
			}
			set
			{
				this.userCode = value;
			}
		}

		#endregion

		#endregion		

		

	}
}
