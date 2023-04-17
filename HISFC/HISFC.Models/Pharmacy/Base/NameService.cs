using System;

namespace FS.HISFC.Models.Pharmacy.Base
{
	/// <summary>
	/// [��������: ��Ŀ���ƻ�����Ϣ]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-11]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class NameService : FS.HISFC.Models.Base.Spell
	{

		public NameService()
		{
		}


		#region ����
		/// <summary>
		/// ͨ����
		/// </summary>
		private string regularName;

		/// <summary>
		/// ͨ����ƴ����
		/// </summary>
		private FS.HISFC.Models.Base.Spell regularSpell = new FS.HISFC.Models.Base.Spell();

		/// <summary>
		/// ѧ��
		/// </summary>
		private string formalName;

		/// <summary>
		/// ѧ��ƴ����
		/// </summary>
		private FS.HISFC.Models.Base.Spell formalSpell = new FS.HISFC.Models.Base.Spell();

		/// <summary>
		/// ����
		/// </summary>
		private string otherName;

		/// <summary>
		/// ѧ��ƴ����
		/// </summary>
		private FS.HISFC.Models.Base.Spell otherSpell = new FS.HISFC.Models.Base.Spell();

		/// <summary>
		/// Ӣ����
		/// </summary>
		private string englishName;

		/// <summary>
		/// Ӣ��ͨ����
		/// </summary>
		private string englishRegularName;

		/// <summary>
		/// Ӣ�ı���
		/// </summary>
		private string englishOtherName;

		/// <summary>
		/// ���ʱ���
		/// </summary>
		private string internationalCode;

		/// <summary>
		/// ���ұ���
		/// </summary>
		private string gbCode;

		#endregion

		/// <summary>
		/// ͨ����
		/// </summary>		
		public string RegularName
		{
			get
			{
				return this.regularName;
			}
			set
			{
				this.regularName = value;
			}
		}


		/// <summary>
		/// ͨ����������
		/// </summary>
		public FS.HISFC.Models.Base.Spell RegularSpell
		{
			get
			{
				return this.regularSpell;
			}
			set
			{
				this.regularSpell = value;
			}
		}
		

		/// <summary>
		/// ѧ��
		/// </summary>
		public string FormalName
		{
			get
			{
				return this.formalName;
			}
			set
			{
				this.formalName = value;
			}
		}


		/// <summary>
		/// ѧ��������
		/// </summary>
		public FS.HISFC.Models.Base.Spell FormalSpell
		{
			get
			{
				return this.formalSpell;
			}
			set
			{
				this.formalSpell = value;
			}
		}
		

		/// <summary>
		/// ����
		/// </summary>
		public string OtherName
		{
			get
			{
				return this.otherName;
			}
			set
			{
				this.otherName = value;
			}
		}
		

		/// <summary>
		/// ����������
		/// </summary>
		public FS.HISFC.Models.Base.Spell OtherSpell
		{
			get
			{
				return this.otherSpell;
			}
			set
			{
				this.otherSpell = value;
			}
		}
		

		/// <summary>
		/// Ӣ����
		/// </summary>
		public string EnglishName
		{
			get
			{
				return this.englishName;
			}
			set
			{
				this.englishName = value;
			}
		}
		

		/// <summary>
		/// Ӣ��ͨ����
		/// </summary>
		public string EnglishRegularName
		{
			get
			{
				return this.englishRegularName;
			}
			set
			{
				this.englishRegularName = value;
			}
		}


		/// <summary>
		/// Ӣ�ı���
		/// </summary>
		public string EnglishOtherName
		{
			get
			{
				return this.englishOtherName;
			}
			set
			{
				this.englishOtherName = value;
			}
		}
		

		/// <summary>
		/// ���ʱ���
		/// </summary>
		public string InternationalCode
		{
			get
			{
				return this.internationalCode;
			}
			set
			{
				this.internationalCode = value;
			}
		}
		

		/// <summary>
		/// ���ұ���
		/// </summary>
		public string GbCode
		{
			get
			{
				return this.gbCode;
			}
			set
			{
				this.gbCode = value;
			}
		}


		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns></returns>
		public new NameService Clone()
		{
			NameService nameS = base.Clone() as NameService;

			nameS.RegularSpell = this.RegularSpell.Clone();
			nameS.FormalSpell = this.FormalSpell.Clone();
			nameS.OtherSpell = this.OtherSpell.Clone();

			return nameS;
		}
	}
}
