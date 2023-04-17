using System;
using System.Collections;

namespace FS.HISFC.Models.Pharmacy
{
	/// <summary>
	/// [��������: ��ҩ����ӡ����]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2004-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���='������' 
	///		�޸�ʱ��='2006-09-12'
	///		�޸�Ŀ��='ϵͳ�ع�'
	///		�޸�����='�����淶���� '
	///  />
	/// </summary>
    [Serializable]
    public class BillPrintType: FS.FrameWork.Models.NeuObject 
	{
		public BillPrintType()
		{

		}


		public enum enuBillPrintType
		{
			/// <summary>
			/// ���ܴ�ӡTotal
			/// </summary>
			T = 0,
			/// <summary>
			/// ��ϸ��ӡDetail
			/// </summary>
			D = 1,
			/// <summary>
			/// ��ҩ��ӡHerbal
			/// </summary>
			H = 2,
			/// <summary>
			/// ������
			/// </summary>
			R = 3,
            /// <summary>
            /// ��չ����
            /// </summary>
            O = 4
		}	

		public new string Name
		{
			get
			{
				string strName;
				switch ((int)this.ID)
				{
					case 0:
						strName = "���ܴ�ӡ";
						break;
					case 1:
						strName = "��ϸ��ӡ";
						break;
					case 2:
						strName = "��ҩ��ӡ";
						break;
                    case 3:
                        strName = "������ӡ";
                        break;
                    case 4:
                        strName = "��չ��ӡ";
                        break;
                    default:
                        strName = "������ӡ";
                        break;
				}
				return	strName;
			}
		}


		/// <summary>
		/// ����ID
		/// </summary>
		private enuBillPrintType myID;
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


		public enuBillPrintType GetIDFromName(string Name)
		{
			enuBillPrintType c = new enuBillPrintType();
			for(int i=0;i<100;i++)
			{
				c = (enuBillPrintType)i;
				if(c.ToString()==Name) return c;
			}
			return (enuBillPrintType)int.Parse(Name);
		}

		/// <summary>
		/// ��������
		/// </summary>
		/// <summary>
		/// ���ȫ���б�
		/// </summary>
		/// <returns>ArrayList(BillPrintType)</returns>
		public static ArrayList List()
		{
			BillPrintType o;
			enuBillPrintType e=new enuBillPrintType();
			ArrayList alReturn=new ArrayList();
			int i;
			for(i=0;i<=System.Enum.GetValues(e.GetType()).GetUpperBound(0);i++)
			{
				o=new BillPrintType();
				o.ID=(enuBillPrintType)i;
				o.Memo=i.ToString();
				alReturn.Add(o);
			}
			return alReturn;
		}


		public new BillPrintType Clone()
		{
			return base.Clone() as BillPrintType;
		}
	}
}
