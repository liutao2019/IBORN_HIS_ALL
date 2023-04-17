using System;
 
using Neusoft.NFC.Object;
namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// Ԥ���������Ϣ��
	/// ID ������� ,Name ��������
	/// 
	/// </summary>
	public class Prepay:Neusoft.NFC.Object.NeuObject
	{
		/// <summary>
		///  Ԥ������
		/// </summary>
		public Prepay()
		{
		}
		/// <summary>
		///  Ԥ�����
		/// </summary>
		public decimal Pre_Cost;
		/// <summary>
		/// ���ѷ�ʽ
		/// 
		/// </summary>
		public NeuObject PayType=new NeuObject();
		/// <summary>
		/// ���߿���
		/// </summary>
		public NeuObject Dept=new NeuObject();
		
		/// <summary>
		/// ���㷢Ʊ��
		/// </summary>
		public string InvoiceNo;
		
		/// <summary>
		/// ͳ������
		/// </summary>
		public DateTime StatisticDate;
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime DtBalanceDate;
		/// <summary>
		/// ����Ա����
		/// </summary>
		public NeuObject BalanceOper = new NeuObject();
		/// <summary>
		/// ����״̬
		/// </summary>
		public string BalanceStatus;
		/// <summary>
		/// �������
		/// </summary>
		public int BalanceSequece;
		/// <summary>
		/// ���ر�� ����״̬
		/// </summary>
		public string PrepayState;
		/// <summary>
		/// ��������
		/// </summary>
		public Bank AccountBank=new Bank();
		/// <summary>
		/// �Ƿ��Ͻ�
		/// </summary>
		public bool IsReport;
		/// <summary>
		/// ���������
		/// </summary>
		public NeuObject FinGrpCode = new NeuObject();
		/// <summary>
		/// תѺ��״̬ 0��תѺ��1תѺ��2תѺ���Ѵ�ӡ
		/// </summary>
		public string  TransPrepayState;
		/// <summary>
		/// תѺ��ʱ��
		/// </summary>
		public DateTime DtTransPrepay;
		/// <summary>
		/// Ԥ����Ʊ��
		/// </summary>
		public string ReceiptNo;
		/// <summary>
		/// ԭ��Ʊ��
		/// </summary>
		public string OldReceiptNo;
        /// <summary>
		/// ����������
		/// </summary>
		public string CheckNo;
		/// <summary>
		/// תѺ�����Ա
		/// </summary>
		public NeuObject TransPrepayOper = new NeuObject();
		/// <summary>
		/// Ԥ�������Ա
		/// </summary>
		public NeuObject PrepayOper = new NeuObject();
		/// <summary>
		/// ����Ա����
		/// </summary>
		public NeuObject OperDept = new NeuObject();
		/// <summary>
		/// Ԥ�������ʱ��
		/// </summary>
		public DateTime DtOperate;
		/// <summary>
		/// תѺ��ʱ������� 
		/// </summary>
		public int ChangeBalanceNo;
		
		public new Prepay Clone()
		{
			Prepay obj = base.Clone() as Prepay;
			obj.PrepayOper=this.PrepayOper.Clone();
			obj.TransPrepayOper=this.TransPrepayOper.Clone();
			obj.FinGrpCode=this.FinGrpCode.Clone();
			obj.BalanceOper=this.BalanceOper.Clone();
			obj.AccountBank=this.AccountBank.Clone();
			obj.Dept=this.Dept.Clone();
			obj.PayType=this.PayType.Clone();
			obj.OperDept=this.OperDept.Clone();
			return obj;
		}
		
	}
}