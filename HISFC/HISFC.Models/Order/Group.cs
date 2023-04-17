using System;
namespace Neusoft.HISFC.Object.Base
{


	/// <summary>
	/// ����
	/// </summary>
	public class Group: Neusoft.NFC.Object.NeuObject,Neusoft.HISFC.Object.Base.ISpell {

		private System.String mySpell_Code ;
		private System.String myUser_Code ;
		private Neusoft.HISFC.Object.Base.enuType myUserType =Neusoft.HISFC.Object.Base.enuType.I;
		private enuGroupKind myKind =enuGroupKind.Doctor ;
		//private enuGroupType myType =enuGroupType.Doctor;
		private Neusoft.NFC.Object.NeuObject myDept = new Neusoft.NFC.Object.NeuObject();
		private Neusoft.NFC.Object.NeuObject myDoctor = new Neusoft.NFC.Object.NeuObject();
		private System.Boolean myIsShared ;
	
		public Group() 
		{
			// TODO: �ڴ˴���ӹ��캯���߼�
		}
	
		/// <summary>
		/// 1����/2סԺ
		/// </summary>
		public Neusoft.HISFC.Object.Base.enuType UserType
		{
			get{ return this.myUserType; }
			set{ this.myUserType = value; }
		}


		/// <summary>
		/// ��������,1.ҽʦ���ף�2.��������
		/// </summary>
		public enuGroupKind Kind
		{
			get{ return this.myKind; }
			set{ this.myKind = value; }
		}
//		/// <summary>
//		/// ��������,1.ҽʦ���ף�2.����
//		/// </summary>
//		public enuGroupType Type
//		{
//			get{ return this.myType; }
//			set{ this.myType = value; }
//		}

		/// <summary>
		/// ���Ҵ���
		/// </summary>
		public Neusoft.NFC.Object.NeuObject Dept
		{
			get{ return this.myDept; }
			set{ this.myDept = value; }
		}


		/// <summary>
		/// ����ҽʦ
		/// </summary>
		public Neusoft.NFC.Object.NeuObject Doctor
		{
			get{ return this.myDoctor; }
			set{ this.myDoctor = value; }
		}


		/// <summary>
		/// �Ƿ���1�ǣ�0��
		/// </summary>
		public System.Boolean IsShared
		{
			get{ return this.myIsShared; }
			set{ this.myIsShared = value; }
		}
		#region ISpellCode ��Ա

		public string Spell_Code
		{
			get
			{
				// TODO:  ��� Group.Neusoft.HISFC.Object.Base.ISpellCode.Spell_Code getter ʵ��
				return this.mySpell_Code;
			}
			set
			{
				// TODO:  ��� Group.Neusoft.HISFC.Object.Base.ISpellCode.Spell_Code setter ʵ��
				this.mySpell_Code = value;
			}
		}

		public string WB_Code
		{
			get
			{
				// TODO:  ��� Group.WB_Code getter ʵ��
				return null;
			}
			set
			{
				// TODO:  ��� Group.WB_Code setter ʵ��
			}
		}

		public string User_Code
		{
			get
			{
				// TODO:  ��� Group.Neusoft.HISFC.Object.Base.ISpellCode.User_Code getter ʵ��
				return this.myUser_Code;
			}
			set
			{
				// TODO:  ��� Group.Neusoft.HISFC.Object.Base.ISpellCode.User_Code setter ʵ��
				this.myUser_Code = value;
			}
		}

		#endregion	

	}
	/// <summary>
	/// ��������
	/// </summary>
	public enum enuGroupKind
	{
		/// <summary>
		/// ҽ��
		/// </summary>
		Doctor = 1,
		/// <summary>
		/// ����
		/// </summary>
		Dept = 2,
		/// <summary>
		/// ȫԺ
		/// </summary>
		All = 3
	}
	/// <summary>
	/// ��������
	/// </summary>
	public enum enuGroupType
	{
		/// <summary>
		/// ҽ������
		/// </summary>
		Doctor = 1,
		/// <summary>
		/// ��������
		/// </summary>
		Fee = 2
	}
}