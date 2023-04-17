using System;
using System.Collections;
namespace Neusoft.HISFC.Object.RADT
{
	/// <summary>
	/// PersonType ��ժҪ˵����
	///D ҽ�� ,N ��ʿ,F�տ�Ա,P ҩʦ,T ��ʦ,C��ʦ(Cooker),O ����
	/// </summary>
	[Obsolete("�Ѿ����ڣ�����ΪHISFC.Object.Base.EmployeeTypeEnumService")]
	public class PersonType:Neusoft.NFC.Object.NeuObject 
	{
		public PersonType()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ��Ա���
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪHISFC.Object.Base.EnumEmployeeType")]
		public enum enuPersonType
		{
			/// <summary>
			///ҽ�� 
			/// </summary>
			D=1,
			/// <summary>
			///��ʿ
			/// </summary>
			N=2,
			/// <summary>
			///�տ�Ա
			/// </summary>
			F=3,
			/// <summary>
			///ҩʦ
			/// </summary>
			P=4,
			/// <summary>
			///��ʦ
			/// </summary>
			T=5,
			/// <summary>
			///��ʦ
			/// </summary>
			C=6,
			/// <summary>
			///����
			/// </summary>
			O=0
		};
		
		/// <summary>
		/// ����ID
		/// </summary>
		private enuPersonType myID;
	
	
		public new System.Object ID
		{
			get
			{
				return this.myID;
			}
			set
			{
				try
				{
					this.myID=(this.GetIDFromName (value.ToString())); 
				}
				catch
				{}
				base.ID=this.myID.ToString();
				string s=this.Name;
			}
		}
		
		public enuPersonType GetIDFromName(string Name)
		{
			enuPersonType c=new enuPersonType();
			for(int i=0;i<100;i++)
			{
				c=(enuPersonType)i;
				if(c.ToString()==Name) return c;
			}
			return (Neusoft.HISFC.Object.RADT.PersonType.enuPersonType)int.Parse(Name);
		}
		/// <summary>
		/// ��������
		/// </summary>
		public new string Name
		{
			get
			{
				string strPersonType;
				

				switch ((int)this.ID)
				{
					case 0:
						strPersonType="����";
						break;
					case 1:
						strPersonType= "ҽ��";
						break;
					case 2:
						strPersonType="��ʿ";
						break;
					case 3:
						strPersonType="�տ�Ա";
						break;
					case 4:
						strPersonType="ҩʦ";
						break;
					case 5:
						strPersonType="��ʦ";
						break;
					case 6:
						strPersonType="��ʦ";
						break;
					default:
						strPersonType="����";
						break;

				}
				base.Name=strPersonType;
				return	strPersonType;
			}
		}
		/// <summary>
		/// ���ȫ���б�
		/// </summary>
		/// <returns>ArrayList(PersonType)</returns>
		public static ArrayList List()
		{
			PersonType aPersonType;
			enuPersonType e=new enuPersonType();
			ArrayList alReturn=new ArrayList();
			int i;
			for(i=0;i<=System.Enum.GetValues(e.GetType()).GetUpperBound(0);i++)
			{
				aPersonType=new PersonType();
				aPersonType.ID=(enuPersonType)i;
				aPersonType.Memo=i.ToString();
				alReturn.Add(aPersonType);
			}
			return alReturn;
		}
	}
}
