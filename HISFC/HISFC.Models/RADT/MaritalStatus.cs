using System;
using Neusoft.NFC.Object;

namespace Neusoft.HISFC.Object.RADT
{
	/// <summary>
	/// [��������: ����״̬ʵ��]<br></br>
	/// [�� �� ��: ���Ʒ�]<br></br>
	/// [����ʱ��: 2006-09-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary> 
	public class MaritalStatus:NeuObject
	{
		/// <summary>
		/// ������
		/// </summary>
		public MaritalStatus()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		
		}
		/// <summary>
		/// ����״̬
		/// </summary>
		[Obsolete("����Ϊ Base.EnumMaritalStatus",true)]
		public enum enuMaritalStatus {
			/// <summary>
			/// Single
			/// </summary>
			S=1,
			/// <summary>
			/// Married
			/// </summary>
			M,
			/// <summary>
			/// Divorced
			/// </summary>
			D,
			/// <summary>
			/// remarriage
			/// </summary>
			R,
			/// <summary>
			/// Separated
			/// </summary>
			A,
			/// <summary>
			/// Widowed
			/// </summary>
			W
		};
		
		/// <summary>
		/// ����ID
		/// </summary>
//		private enuMaritalStatus myID;
//		public new System.Object ID
//		{
//			get
//			{
//				return this.myID;
//			}
//			set
//			{
//				try
//				{
//					this.myID=(this.GetIDFromName (value.ToString())); 
//				}
//				catch
//				{}
//				base.ID=this.myID.ToString();
//				string s=this.Name;
//			}
//		}
//		public enuMaritalStatus GetIDFromName(string Name)
//		{
//			enuMaritalStatus c=new enuMaritalStatus();
//			for(int i=0;i<100;i++)
//			{
//				c=(enuMaritalStatus)i;
//				if(c.ToString()==Name) return c;
//			}
//			return (enuMaritalStatus)int.Parse(Name);
//		}
		/// <summary>
		/// ��ʾ�Ļ�������
		/// </summary>
//		public new string Name
//		{
//			get
//			{
//				string strMaritalStatus;
//				switch ((int)this.ID)
//				{
//					case 1:
//						strMaritalStatus= "δ��";
//						break;
//					case 2:
//						strMaritalStatus="�ѻ�";
//						break;
//					case 3:
//						strMaritalStatus="ʧ��";
//						break;
//					case 4:
//						strMaritalStatus="�ٻ�";
//						break;
//					case 5:
//						strMaritalStatus="�־�";
//						break;
//					case 6:
//						strMaritalStatus="ɥż";
//						break;
//					default:
//						strMaritalStatus="δ��";
//						break;
//
//				}
//					base.Name=strMaritalStatus;
//				return	strMaritalStatus;
//			}
//		}
		/// <summary>
		/// ��û���ȫ���б�
		/// </summary>
		/// <returns>ArrayList(MaritalStatus)</returns>
//		public static ArrayList List()
//		{
//			MaritalStatus aMaritalStatus;
//			ArrayList alReturn=new ArrayList();
//			int i;
//			for(i=1;i<=4;i++)
//			{
//				aMaritalStatus=new MaritalStatus();
//				aMaritalStatus.ID=(enuMaritalStatus)i;
//				aMaritalStatus.Memo=i.ToString();
//				alReturn.Add(aMaritalStatus);
//			}
//			return alReturn;
//		}
//		public new MaritalStatus Clone()
//		{
//			return this.MemberwiseClone() as MaritalStatus;
//		}
	}
}
