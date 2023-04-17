using System;
using Neusoft.NFC.Object;

namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// BalancePay ��ժҪ˵����
	/// </summary>
	public class BalancePay
	{
		public BalancePay()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ��Ʊ��
		/// </summary>
		public string InvoiceNo;
		/// <summary>
		/// ��������
		/// </summary>
		public string TransType;
		/// <summary>
		/// ��������
		/// </summary>
		public string TransKind;
		/// <summary>
		/// ֧����ʽ
		/// </summary>
		public NeuObject PayType = new NeuObject();
		/// <summary>
		/// ���
		/// </summary>
		public decimal Cost= 0m;
		/// <summary>
		/// ����
		/// </summary>
		public int Qty;
		/// <summary>
		/// ����ʵ��
		/// </summary>
		public Bank Bank = new Bank();
		/// <summary>
		/// �������ձ�� 1����2����
		/// </summary>
		public string ReturnOrSupplyFlag;
		/// <summary>
		/// ������
		/// </summary>
		public NeuObject BalanceOper= new NeuObject();
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime DtBalance;
		/// <summary>
		/// �������
		/// </summary>
		public int BalanceNo;
		public new BalancePay Clone()
		{
			BalancePay obj = new BalancePay() as BalancePay;
			obj.BalanceOper=this.BalanceOper.Clone();
			obj.Bank=this.Bank.Clone();
			obj.PayType=this.PayType.Clone();
			return obj;
		}

	}
}
