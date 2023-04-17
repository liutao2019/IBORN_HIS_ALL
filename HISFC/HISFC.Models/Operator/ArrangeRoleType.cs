using System;
using System.Collections;

namespace neusoft.HISFC.Object.Operator
{
	/// <summary>
	/// ����������Ա������Ա��ɫö����
	/// </summary>
	public class ArrangeRoleType:neusoft.neuFC.Object.neuObject,neusoft.HISFC.Object.Base.ISpellCode
	{
		public ArrangeRoleType()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public enum enuArrangeRole
		{
			/// <summary>
			///����ҽʦ
			/// </summary>
			OPS=1,
			/// <summary>
			///ָ��ҽʦ
			/// </summary>
			GUI=2,
			/// <summary>
			///һ��
			/// </summary>
			HP1=3,
			/// <summary>
			///����
			/// </summary>
			HP2=4,
			/// <summary>
			///����
			/// </summary>
			HP3=5,
			/// <summary>
			///����ҽ��
			/// </summary>
			ANA=6,
			/// <summary>
			///��������
			/// </summary>
			AHP=7,
			/// <summary>
			///ϴ�ֻ�ʿ
			/// </summary>
			WNR=8,
			/// <summary>
			///Ѳ�ػ�ʿ
			/// </summary>
			INR=9,
			/// <summary>
			///��̨��ʿ
			/// </summary>
			FNR=10,
			/// <summary>
			///����
			/// </summary>
			OTH=11
		};

		/// <summary>
		/// ����ID
		/// </summary>
		private enuArrangeRole RoleID;

		/// <summary>
		/// ID
		/// </summary>
		public new System.Object ID
		{
			get
			{
				return this.RoleID;
			}
			set
			{
				try
				{
					this.RoleID=(this.GetIDFromName (value.ToString())); 
				}
				catch
				{}
				base.ID=this.RoleID.ToString();
				string s=this.Name;
			}
		}
		
		public enuArrangeRole GetIDFromName(string Name)
		{
			enuArrangeRole c=new enuArrangeRole();
			for(int i=0;i<100;i++)
			{
				c=(enuArrangeRole)i;
				if(c.ToString()==Name) return c;
			}
			return (enuArrangeRole)int.Parse(Name);
		}

		/// <summary>
		/// ��������
		/// </summary>
		public new string Name
		{
			get
			{				
				string strRole = "";
				switch ((int)this.ID)
				{
					case 1:
						strRole= "����ҽʦ";
						break;
					case 2:
						strRole="ָ��ҽʦ";
						break;
					case 3:
						strRole="һ��";
						break;
					case 4:
						strRole="����";
						break;
					case 5:
						strRole="����";
						break;
					case 6:
						strRole="����ҽʦ";
						break;
					case 7:
						strRole="��������";
						break;
					case 8:
						strRole="ϴ�ֻ�ʿ";
						break;
					case 9:
						strRole="Ѳ�ػ�ʿ";
						break;
					case 10:
						strRole="��̨��ʿ";
						break;
					case 11:
						strRole="����";
						break;
					default:
						strRole="δ֪";
						break;
				}
				base.Name=strRole;
				return	strRole;
			}
		}
		/// <summary>
		/// ���ȫ���б�
		/// </summary>
		/// <returns>ArrayList(BloodType)</returns>
		public static ArrayList List()
		{
			ArrangeRoleType aArrangeRoleType;
			enuArrangeRole e=new enuArrangeRole();
			ArrayList alReturn=new ArrayList();
			int i;
			for(i=0;i<=System.Enum.GetValues(e.GetType()).GetUpperBound(0);i++)
			{
				aArrangeRoleType=new ArrangeRoleType();
				aArrangeRoleType.ID=(enuArrangeRole)i;
				aArrangeRoleType.Memo=i.ToString();
				alReturn.Add(aArrangeRoleType);
			}
			return alReturn;
		}

		public new ArrangeRoleType Clone()
		{
			return base.Clone() as ArrangeRoleType;
		}
		#region ISpellCode ��Ա

		public string Spell_Code
		{
			get
			{
				// TODO:  ��� ArrangeRoleType.Spell_Code getter ʵ��
				return null;
			}
			set
			{
				// TODO:  ��� ArrangeRoleType.Spell_Code setter ʵ��
			}
		}

		public string WB_Code
		{
			get
			{
				// TODO:  ��� ArrangeRoleType.WB_Code getter ʵ��
				return null;
			}
			set
			{
				// TODO:  ��� ArrangeRoleType.WB_Code setter ʵ��
			}
		}

		public string User_Code
		{
			get
			{
				// TODO:  ��� ArrangeRoleType.User_Code getter ʵ��
				return null;
			}
			set
			{
				// TODO:  ��� ArrangeRoleType.User_Code setter ʵ��
			}
		}

		#endregion
	}
}
