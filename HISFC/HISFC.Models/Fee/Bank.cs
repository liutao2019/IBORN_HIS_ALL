using System;

namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// Bank ����ʵ�� id ����id name ��������
	/// </summary>
	public class Bank:Neusoft.NFC.Object.NeuObject 
	{
		public Bank()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ����
		/// </summary>
		public FT fee=new FT();
		/// <summary>
		/// pos������ˮ�Ż�֧Ʊ��
		/// </summary>
		public string InvoiceNo;
		/// <summary>
		/// �ʺ�
		/// </summary>
		public string Account;
		/// <summary>
		/// ���ݵ�λ
		/// </summary>
		public string WorkName;
		public new Bank Clone()
		{
			Bank obj=base.Clone() as Bank;
			obj.fee=this.fee.Clone();
			return obj;
		}
	}
}
