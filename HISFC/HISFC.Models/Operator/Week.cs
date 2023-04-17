using System;
using System.Collections;

namespace neusoft.HISFC.Object.Operator
{
	/// <summary>
	/// Week ��ժҪ˵����
	/// ���� ö����
	/// </summary>
	public class Week:neusoft.neuFC.Object.neuObject
	{
		public Week()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public enum enuWeek
		{
			/// <summary>
			///һ 
			/// </summary>
			һ=1,
			/// <summary>
			/// ��
			/// </summary>
			��=2,
			/// <summary>
			/// ��
			/// </summary>
			��=3,
			/// <summary>
			/// ��
			/// </summary>
			��=4,
			/// <summary>
			/// ��
			/// </summary>
			��=5,
			/// <summary>
			/// ��
			/// </summary>
			��=6,
			/// <summary>
			/// ��
			/// </summary>
			��=0

		};
		
		/// <summary>
		/// ����ID
		/// </summary>
		private enuWeek weekID;
		
		/// <summary>
		/// ABOѪ��
		/// </summary>
		public new System.Object ID
		{
			get
			{
				return this.weekID;
			}
			set
			{
				try
				{
					this.weekID=(this.GetIDFromName (value.ToString())); 
				}
				catch
				{}
				base.ID=this.weekID.ToString();
				string s=this.Name;
			}
		}
		
		public enuWeek GetIDFromName(string Name)
		{
			enuWeek c=new enuWeek();
			for(int i=0;i<100;i++)
			{
				c=(enuWeek)i;
				if(c.ToString()==Name) return c;
			}
			return (enuWeek)int.Parse(Name);
		}
		/// <summary>
		/// ��������
		/// </summary>
		public new string Name
		{
			get
			{				
				string strWeek = "";
				switch ((int)this.ID)
				{
					case 1:
						strWeek= "һ";
						break;
					case 2:
						strWeek="��";
						break;
					case 3:
						strWeek="��";
						break;
					case 4:
						strWeek="��";
						break;
					case 5:
						strWeek="��";
						break;
					case 6:
						strWeek="��";
						break;
					case 0:
						strWeek="��";
						break;
					default:
						strWeek="δ֪";
						break;

				}
				base.Name=strWeek;
				return	strWeek;
			}
		}
		/// <summary>
		/// ���ȫ���б�
		/// </summary>
		/// <returns>ArrayList(BloodType)</returns>
		public static ArrayList List()
		{
			Week aWeek;
			enuWeek e=new enuWeek();
			ArrayList alReturn=new ArrayList();
			int i;
			for(i=0;i<=System.Enum.GetValues(e.GetType()).GetUpperBound(0);i++)
			{
				aWeek=new Week();
				aWeek.ID=(enuWeek)i;
				aWeek.Memo=i.ToString();
				alReturn.Add(aWeek);
			}
			return alReturn;
		}
		public new Week Clone()
		{
			return base.Clone() as Week;
		}
	}
}
