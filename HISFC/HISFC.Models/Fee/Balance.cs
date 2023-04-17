using System;
using System.Collections;
using Neusoft.NFC.Object;
namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// Balance ��ժҪ˵����
	/// id �������
	/// ������Ϣ��
	/// </summary>
	public class Balance:Neusoft.NFC.Object.NeuObject 
	{
		public Balance()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ���ϱ��
		/// </summary>
		public string WasteFlag;
		/// <summary>
		/// ����
		/// </summary>
		public FT Fee=new FT();
		/// <summary>
		/// ��ʼʱ��
		/// </summary>
		public DateTime DtBegin;
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime DtEnd;
       /// <summary>
       /// ����ʱ��
       /// </summary>
        public DateTime DtBalance;  
		/// <summary>
		/// ��������
		///  0 ��Ժ���� I
		/// 1ת�ƽ��� R
		/// 2 ��Ժ���� O
		/// 3 �ؽ��� M
		/// 4 ��ת S
		/// </summary>
		public BalanceType BalanceType=new BalanceType();
		/// <summary>
		/// ��Ʊ
		/// </summary>
		public Invoice Invoice=new Invoice();
		/// <summary>
		/// ��������
		/// </summary>
		public string TransType;
        /// <summary>
		/// �������Ա
		/// </summary>
		public NeuObject BalanceOper = new NeuObject();
		/// <summary>
		/// ��ӡ����(��������Ʋ�����ͬ��Ʊ��ʹ��)
		/// </summary>
		public int PrintTimes;
		/// <summary>
		/// ���������
		/// </summary>
		public NeuObject FinGrp = new NeuObject();
		/// <summary>
		/// ������
		/// </summary>
		public string CheckNo;
		/// <summary>
		/// ��ͬ��λ
		/// </summary>
		public NeuObject Pact = new NeuObject();
		/// <summary>
		/// �������
		/// </summary>
		public NeuObject PayKind = new NeuObject();
		/// <summary>
		/// �Ƿ�����Ʊ
		/// </summary>
		public bool IsMainInvoice;
		/// <summary>
		/// �Ƿ�Ϊ��������������
		/// </summary>
		public bool IsLastFlag;
		/// <summary>
		/// ����Ա����
		/// </summary>
		public NeuObject BalanceOperDept = new NeuObject();
		/// <summary>
		/// ���ϲ���Ա
		/// </summary>
		public NeuObject WasteOper = new NeuObject();
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime  DtWaste;

	

		public new Balance Clone()
		{
			Balance obj = base.Clone() as Balance;
			obj.BalanceOper= this.BalanceOper.Clone();
			obj.Invoice=this.Invoice.Clone();//(Neusoft.HISFC.Object.Fee.Invoice)Invoice.Clone();

			obj.FinGrp=this.FinGrp.Clone();
			obj.BalanceType=this.BalanceType.Clone();
			obj.Fee=this.Fee.Clone();
			obj.Pact=this.Pact.Clone();
			obj.BalanceOperDept=this.BalanceOperDept.Clone();

			return obj;
		}

	}
}
