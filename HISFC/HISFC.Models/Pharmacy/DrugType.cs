using System;
using System.Collections;

namespace neusoft.HISFC.Object.Pharmacy
{
	/// <summary>
	/// DrugAttribute ��ժҪ˵����
	/// </summary>
	public class DrugAttribute: neusoft.neuFC.Object.neuObject 
	{
		public DrugAttribute()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public enum enuDrugAttribute
		{
			/// <summary>
			/// һ���ҩ
			/// </summary>
			G = 0,
			/// <summary>
			/// ����ҩƷ��ҩ
			/// </summary>
			S = 1,
			/// <summary>
			/// ��Ժ��ҩ��ҩ
			/// </summary>
			O = 2		
		}	
		public new string Name
		{
			get
			{
				string strName;
				switch ((int)this.ID)
				{
					case 1:
						strName = "����ҩƷ��ҩ";
						break;
					case 2:
						strName = "��Ժ��ҩ��ҩ";
						break;
					default:
						strName = "һ���ҩ";
						break;
				}
				return	strName;
			}
		}

		/// <summary>
		/// ����ID
		/// </summary>
		private enuDrugAttribute myID;
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
				{
					string err="�޷�ת��"+this.GetType().ToString()+"���룡";
				}
				base.ID=this.myID.ToString();
				base.Name = this.Name;
			}
		}
		public enuDrugAttribute GetIDFromName(string Name)
		{
			enuDrugAttribute c = new enuDrugAttribute();
			for(int i=0;i<100;i++)
			{
				c = (enuDrugAttribute)i;
				if(c.ToString()==Name) return c;
			}
			return (enuDrugAttribute)int.Parse(Name);
		}
		/// <summary>
		/// ��������
		/// </summary>
		/// <summary>
		/// ���ȫ���б�
		/// </summary>
		/// <returns>ArrayList(DrugAttribute)</returns>
		public static ArrayList List()
		{
			DrugAttribute o;
			enuDrugAttribute e=new enuDrugAttribute();
			ArrayList alReturn=new ArrayList();
			int i;
			for(i=0;i<=System.Enum.GetValues(e.GetType()).GetUpperBound(0);i++)
			{
				o=new DrugAttribute();
				o.ID=(enuDrugAttribute)i;
				o.Memo=i.ToString();
				alReturn.Add(o);
			}
			return alReturn;
		}
		public new DrugAttribute Clone()
		{
			return base.Clone() as DrugAttribute;
		}
	}
}
