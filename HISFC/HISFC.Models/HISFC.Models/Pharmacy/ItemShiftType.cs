using System;
using System.Collections;

namespace FS.HISFC.Models.Pharmacy 
{
	/// <summary>
	/// [��������: ҩƷ��Ϣ�䶯��]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2004-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���='������'
	///		�޸�ʱ��='2006-09-13'
	///		�޸�Ŀ��='ϵͳ�ع�'
	///		�޸�����='�����淶����'
	///  />
	///  ID �������
	/// </summary>
    [Serializable]
    public class ItemShiftType : FS.FrameWork.Models.NeuObject 
	{
		public ItemShiftType() 
		{
			
		}


		public enum enuQuality 
		{
			/// <summary>
			/// ����
			/// </summary>
			U = 0,
			/// <summary>
			/// ��ҩ
			/// </summary>
			N = 1,
			/// <summary>
			/// ͣ��
			/// </summary>
			S = 2,
			/// <summary>
			/// ����
			/// </summary>
			A = 3,
			/// <summary>
			/// �����޸�(���,���Ʊ䶯)
			/// </summary>
			M = 4,
			
		}	
		public new string Name 
		{
			get 
			{
				string strName;
				switch ((int)this.ID) 
				{
					case 0:
						strName = "����";
						break;
					case 1:
						strName = "��ҩ";
						break;
					case 2:
						strName = "ͣ��";
						break;
					case 3:
						strName = "����";
						break;
					case 4:
						strName = "�����޸�";
						break;
					default:
						strName = "����";
						break;
				}
				return	strName;
			}
		}


		/// <summary>
		/// ����ID
		/// </summary>
		private enuQuality myID;
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
				}
				base.ID=this.myID.ToString();
				base.Name = this.Name;
			}
		}


		public enuQuality GetIDFromName(string Name) 
		{
			enuQuality c = new enuQuality();
			for(int i=0;i<100;i++) 
			{
				c = (enuQuality)i;
				if(c.ToString()==Name) return c;
			}
			return (enuQuality)int.Parse(Name);
		}


		/// <summary>
		/// ��������  ���ȫ���б�
		/// </summary>
		/// <returns>ArrayList(Quality)</returns>
		public static ArrayList List() 
		{
            ItemShiftType o;
			enuQuality e=new enuQuality();
			ArrayList alReturn=new ArrayList();
			int i;
			for(i=0;i<=System.Enum.GetValues(e.GetType()).GetUpperBound(0);i++) 
			{
                o = new ItemShiftType();
				o.ID=(enuQuality)i;
				o.Memo=i.ToString();
				alReturn.Add(o);
			}
			return alReturn;
		}


		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>�ɹ����ص�ǰʵ���ĸ���</returns>
		public new ItemShiftType Clone() 
		{
			return base.Clone() as ItemShiftType;
		}
	}
}
