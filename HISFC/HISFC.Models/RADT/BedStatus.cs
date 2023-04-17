using System;
 
namespace Neusoft.HISFC.Object.RADT
{
	/// <summary>
	/// [��������: ����״̬ʵ��]<br></br>
	/// [�� �� ��: ����ΰ]<br></br>
	/// [����ʱ��: 2006-09-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary> 
	[System.Obsolete("BedStatus ö�ٸ�Ϊ EmnuBedStatus ",true)]
	public class BedStatus:Neusoft.NFC.Object.NeuObject
	{
		/// <summary>
		/// ����״̬
		/// </summary>
		public BedStatus()
		{
		}
		/// <summary>
		/// ����״̬
		/// </summary>
		public enum enuBedStatus
		{
			/// <summary>
			/// Closed
			/// </summary>
			C,
			/// <summary>
			/// Unoccupied
			/// </summary>
			U,
			/// <summary>
			/// Contaminated��Ⱦ��
			/// </summary>
			K,
			/// <summary>
			/// �����
			/// </summary>
			I,
			/// <summary>
			/// Occupied
			/// </summary>
			O,
			/// <summary>
			/// �ٴ�  user define
			/// </summary>
			R,
			/// <summary>
			/// ���� user define
			/// </summary>
			W,
			/// <summary>
			/// �Ҵ�
			/// </summary>
			H

		};
		
		/// <summary>
		/// ����ID
		/// </summary>
		private enuBedStatus myID;
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
		public enuBedStatus GetIDFromName(string Name)
		{
			enuBedStatus c=new enuBedStatus();
			for(int i=0;i<100;i++)
			{
				c=(enuBedStatus)i;
				if(c.ToString()==Name) return c;
			}
			return (enuBedStatus)int.Parse(Name);
		}
		/// <summary>
		/// ���ش�״̬����
		/// </summary>
		public new string Name
		{
			get
			{
				string strBedStatus;
				switch ((int)this.ID)
				{
					case 0:
						strBedStatus= "�ر�";
						break;
					case 1:
						strBedStatus="�մ�";
						break;
					case 2:
						strBedStatus="��Ⱦ";
						break;
					case 3:
						strBedStatus="����";
						break;
					case 4:
						strBedStatus="ռ��";
						break;
					case 5:
						strBedStatus="�ٴ�";
						break;
					case 6:
						strBedStatus="����";
						break;
					case 7:
						strBedStatus="�Ҵ�";
						break;
					
					default:
						strBedStatus="�մ�";
						break;
				}
					base.Name=strBedStatus;
				return	strBedStatus;
			}
		}
		/// <summary>
		/// ��ò���״̬ȫ���б�
		/// </summary>
		/// <returns>ArrayList(BedStatus)</returns>
		public static System.Collections.ArrayList List()
		{
			BedStatus aBedStatus;
			System.Collections.ArrayList alReturn=new System.Collections.ArrayList();
			int i;
			for(i=0;i<=7;i++)
			{
				aBedStatus=new BedStatus();
				aBedStatus.ID=(enuBedStatus)i;
				aBedStatus.Memo=i.ToString();
				alReturn.Add(aBedStatus);
			}
			return alReturn;
		}
		/// <summary>
		/// ��ò���״̬ռ�á��ż�4��5
		/// </summary>
		/// <returns>ArrayList(BedStatus)</returns>
		public static System.Collections.ArrayList OccupiedList()
		{
			BedStatus aBedStatus;
			System.Collections.ArrayList alReturn=new System.Collections.ArrayList();
			int i;
			for(i=4;i<=5;i++)
			{
				aBedStatus=new BedStatus();
				aBedStatus.ID=(enuBedStatus)i;
				aBedStatus.Memo=i.ToString();
				alReturn.Add(aBedStatus);
			}
			return alReturn;
		}
		/// <summary>
		/// ��ò���״̬��ռ��0-3��
		/// </summary>
		/// <returns>ArrayList(BedStatus)</returns>
		public static System.Collections.ArrayList UnoccupiedList()
		{
			BedStatus aBedStatus;
			System.Collections.ArrayList alReturn=new System.Collections.ArrayList();
			int i;
			for(i=0;i<=3;i++)
			{
				aBedStatus=new BedStatus();
				aBedStatus.ID=(enuBedStatus)i;
				aBedStatus.Memo=i.ToString();
				alReturn.Add(aBedStatus);
			}
			return alReturn;
		}
		public new BedStatus Clone()
		{
			return base.Clone() as BedStatus;
		}
	}
}
