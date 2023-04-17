using System;
using System.Collections;

namespace FS.HISFC.Models.Pharmacy
{
	/// <summary>
	/// [��������: ��ҩ����]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2004-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��˹'
	///		�޸�ʱ��='2006-09-12'
	///		�޸�Ŀ��='ϵͳ�ع�' 
	///		�޸�����='�����淶����'
	///  />
	/// </summary>
    [Serializable]
    public class DrugAttribute: FS.FrameWork.Models.NeuObject 
	{
		public DrugAttribute()
		{
			
		}


		public enum enuDrugAttribute
		{
			/// <summary>
			/// ��ʱ��ҩ
			/// </summary>
			T = 0,
			/// <summary>
			/// ���а�ҩ
			/// </summary>
			A = 1,
			/// <summary>
			/// ��ҽ����ҩ(�����Ұ�ҩ)
			/// </summary>
			P = 2,
			/// <summary>
			/// ��Ժ��ҩ��ҩ
			/// </summary>
			O = 3,
			/// <summary>
			/// ����ҩƷ��ҩ
			/// </summary>
			S = 4,
			/// <summary>
			/// ��ҩ
			/// </summary>
			R = 5,
            /// <summary>
            /// ȫ��
            /// </summary>
	        Q = 6,
		}	

		public new string Name
		{
			get
			{
				string strName;
				switch ((int)this.ID)
				{
					case 1:
						strName = "���а�ҩ";
						break;
					case 2:
						strName = "��ҽ����ҩ(�����Ұ�ҩ)";
						break;
					case 3:
						strName = "��Ժ��ҩ��ҩ";
						break;
					case 4:
						strName = "����ҩƷ��ҩ";
						break;
					case 5:
						strName = "��ҩ";
						break;
                    case 6:
                        strName = "ȫ��";
                        break;
					default:
						strName = "��ʱ��ҩ";
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
