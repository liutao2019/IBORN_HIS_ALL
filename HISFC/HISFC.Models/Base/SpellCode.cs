using System;

using Neusoft.NFC.Object;
namespace Neusoft.HISFC.Object.Base
{
    /*----------------------------------------------------------------
    // Copyright (C) 2004 ����ɷ����޹�˾
    // ��Ȩ���С� 
    //
    // �ļ�����SpellCode.cs
    // �ļ�����������ƴ����ʵ��
    //
    // 
    // ������ʶ:���Ʒ� 20050614
    //
    // �޸ı�ʶ����ѩ�� 20060420
    // �޸�����������һ�´���,���ڴ������˵��»����ˣ����ǲ��Ҹ�
    //
    // �޸ı�ʶ��
    // �޸�������
    //----------------------------------------------------------------*/
	public class SpellCode:Neusoft.NFC.Object.NeuObject,ISpellCode
	{	
		/// <summary>
		/// ���������
		/// </summary>
		public SpellCode()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ƴ����
		/// </summary>
		protected string sSpell_Code;
		/// <summary>
		/// �����
		/// </summary>
		protected string sWB_Code;
		/// <summary>
		/// �Զ�����
		/// </summary>
		protected string sUser_Code;

		public new SpellCode Clone()
		{
			return this.MemberwiseClone() as SpellCode;
		}
		#region ISpellCode ��Ա

		public string Spell_Code
		{
			get
			{
				// TODO:  ��� SpellCode.Spell_Code getter ʵ��
				return this.sSpell_Code ;
			}
			set
			{
				// TODO:  ��� SpellCode.Spell_Code setter ʵ��
				this.sSpell_Code=value;
			}
		}

		public string WB_Code
		{
			get
			{
				// TODO:  ��� SpellCode.WB_Code getter ʵ��
				return this.sWB_Code ;
			}
			set
			{
				// TODO:  ��� SpellCode.WB_Code setter ʵ��
				this.sWB_Code=value;
			}
		}

		public string User_Code
		{
			get
			{
				// TODO:  ��� SpellCode.User_Code getter ʵ��
				return this.sUser_Code ;
			}
			set
			{
				// TODO:  ��� SpellCode.User_Code setter ʵ��
				this.sUser_Code =value;
			}
		}

		#endregion
	}
}
