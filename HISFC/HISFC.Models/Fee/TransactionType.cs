using System;

namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// TransactionType ��ժҪ˵����
	/// ��������
	/// <br>Values 	 Description</br>
	///	<br>CG	Charge</br>
	///	<br>CD	Credit</br>
	///	<br>PY	Payment</br>
	///	<br>AJ	Adjustment</br>
	/// </summary>
	public class TransactionType:Neusoft.NFC.Object.NeuObject 
	{
		public TransactionType()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public enum enuTransactionType
		{
			/// <summary>
			/// cash �ֽ�
			/// </summary>
			CA=0,
			/// <summary>
			/// cheque ֧Ʊ
			/// </summary>
			CH=1,
			
			/// <summary>
			/// ���ÿ�
			/// </summary>
			CD=2,
			/// <summary>
			/// debit��ǿ�
			/// </summary>
			DB=3,
//			/// <summary>
//			/// תѺ��
//			/// </summary>
//			AJ=4,
			/// <summary>
			/// ��Ʊ
			/// </summary>
			PO=4,
			/// <summary>
			/// �����ʻ�
			/// </summary>
			PS=5,
//			/// <summary>
//			/// ���
//			/// </summary>
//			FR=9
		};
		
		/// <summary>
		/// ����ID
		/// </summary>
		private enuTransactionType myID;
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
		public enuTransactionType GetIDFromName(string Name)
		{
			enuTransactionType c=new enuTransactionType();
			for(int i=0;i<100;i++)
			{
				c=(enuTransactionType)i;
				if(c.ToString()==Name) return c;
			}
			return (enuTransactionType)int.Parse(Name);
		}
		public new string Name
		{
			get
			{
				string strTransactionType;
				switch ((int)this.ID)
				{
					case 0:
						strTransactionType= "�ֽ�";
						break;
					case 1:
						strTransactionType= "֧Ʊ";
						break;

					case 2:
						strTransactionType="���ÿ�";
						break;
					case 3:
						strTransactionType="��ǿ�";
						break;
					case 5:
						strTransactionType="�����ʻ�";
						break;
//					case 4:
//						strTransactionType="תѺ��";
//						break;
					case 4:
						strTransactionType="��Ʊ";
						break;
//					case 8:
//						strTransactionType="�۱����ʻ�";
//						break;
//					case 9:
//						strTransactionType="���";
//						break;
					default:
						strTransactionType="�ֽ�";
						break;
				}
				base.Name=strTransactionType;
				return	strTransactionType;
			}
		}
		/// <summary>
		/// ���ȫ���б�
		/// </summary>
		/// <returns>ArrayList(TransactionType)</returns>
		public static System.Collections.ArrayList List()
		{
			TransactionType aTransactionType;
			System.Collections.ArrayList alReturn=new System.Collections.ArrayList();
			int i;
			for(i=0;i<=5;i++)
			{
				aTransactionType=new TransactionType();
				aTransactionType.ID=(enuTransactionType)i;
				aTransactionType.Memo=i.ToString();
				alReturn.Add(aTransactionType);
			}
			return alReturn;
		}
	}
}
