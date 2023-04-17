using System;
using System.Collections;
namespace neusoft.HISFC.Object.Operator
{
	/// <summary>
	/// RoleOperKind ��ժҪ˵����
	/// ������Ա��ɫ״̬��Ŀǰֻ������������ã�
	/// </summary>
	public class RoleOperKind : neusoft.neuFC.Object.neuObject
	{
		public RoleOperKind()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public enum enuRoleOperKind
		{
			/// <summary>
			///����
			/// </summary>
			ZC=1,
			/// <summary>
			///ֱ��
			/// </summary>
			ZL=2,
			/// <summary>
			///�Ӱ�
			/// </summary>
			JB=3
		};
		/// <summary>
		/// ����ID
		/// </summary>
		private enuRoleOperKind RoleID;

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
		
		public enuRoleOperKind GetIDFromName(string Name)
		{
			enuRoleOperKind c=new enuRoleOperKind();
			for(int i=0;i<100;i++)
			{
				c=(enuRoleOperKind)i;
				if(c.ToString()==Name) return c;
			}
			return (enuRoleOperKind)int.Parse(Name);
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
						strRole= "����";
						break;
					case 2:
						strRole="ֱ��";
						break;
					case 3:
						strRole="�Ӱ�";
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
			RoleOperKind aRoleOperKind;
			enuRoleOperKind e=new enuRoleOperKind();
			ArrayList alReturn=new ArrayList();
			int i;
			for(i=0;i<=System.Enum.GetValues(e.GetType()).GetUpperBound(0);i++)
			{
				aRoleOperKind=new RoleOperKind();
				aRoleOperKind.ID=(enuRoleOperKind)i;
				aRoleOperKind.Memo=i.ToString();
				alReturn.Add(aRoleOperKind);
			}
			return alReturn;
		}

		public new RoleOperKind Clone()
		{
			return base.Clone() as RoleOperKind;
		}
	}
}
