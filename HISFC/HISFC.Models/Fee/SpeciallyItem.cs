using System;
using System.Data;
using System.Collections;
namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// SpeciallyItem ��ժҪ˵����
	/// </summary>
	public class SpeciallyItem:Neusoft.NFC.Object.NeuObject
	{
		public SpeciallyItem()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public enum enuSpeciallyItem {
			
			/// <summary>
			/// ��鼰����
			/// </summary>
			LJZF=0,
			/// <summary>
			/// CT����
			/// </summary>
			CTLF=1,
			/// <summary>
			/// Ƭ��
			/// </summary>
			PF = 2,
			/// <summary>
			/// ע������
			/// </summary>
			ZSQF = 3,
			/// <summary>
			/// ��Ӱ����
			/// </summary>
			XYJF = 4
		}
		/// <summary>
		/// ����ID
		/// </summary>
		private enuSpeciallyItem myID;
		//public new System.Object ID 
		/// <summary>
		/// 
		/// </summary>
		public new System.Object ID {
			get {
				return this.myID;
			}
			set {
				try {
					this.myID=this.GetIDFromName (value.ToString()); 
				}
				catch {
					string err="�޷�ת��"+this.GetType().ToString()+"���룡";
				}
				base.ID=this.myID.ToString();
				string s=this.Name;
			}
		}
		public enuSpeciallyItem GetIDFromName(string Name) {
			enuSpeciallyItem c=new enuSpeciallyItem();
			for(int i=0;i<100;i++) {
				c=(enuSpeciallyItem)i;
				if(c.ToString()==Name) return c;
			}
			return (Neusoft.HISFC.Object.Fee.SpeciallyItem.enuSpeciallyItem)int.Parse(Name);
		}
		/// <summary>
		/// ��������
		/// </summary>
		public new string Name {
			get {
				string str = "";
				switch ((int)this.ID) {
					case 0:
						str= "��鼰����";
						break;
					case 1:
						str="CT����";
						break;
					case 2:
						str="Ƭ��";
						break;
					case 3:
						str="ע������";
						break;
					case 4:
						str="��Ӱ����";
						break;
				}
				base.Name=str;
				return	str;
			}
		}
		/// <summary>
		/// ���ȫ���б�
		/// </summary>
		/// <returns>ArrayList()</returns>
		public static ArrayList List() {
			SpeciallyItem o;
			
			ArrayList alReturn=new ArrayList();
			int i;
			for(i=0;i<=System.Enum.GetValues(typeof(enuSpeciallyItem)).GetUpperBound(0);i++) {
				o=new SpeciallyItem();
				o.ID=(enuSpeciallyItem)i;
				o.Memo=i.ToString();
				alReturn.Add(o);
			}
			return alReturn;
		}
		public new SpeciallyItem Clone() {
			return this.MemberwiseClone() as SpeciallyItem;
		}


	}
}
