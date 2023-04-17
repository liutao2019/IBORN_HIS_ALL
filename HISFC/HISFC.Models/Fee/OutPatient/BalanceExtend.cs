using System;

namespace Neusoft.HISFC.Models.Fee.Outpatient
{


	/// <summary>
	/// InvoiceExtend ��ժҪ˵����
	/// </summary>
    /// 
    [System.Serializable]
	public class BalanceExtend : Neusoft.FrameWork.Models.NeuObject {

		public BalanceExtend( ) {
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		private string invoiceSeq;
		/// <summary>
		/// ��Ʊ����
		/// </summary>
		public string InvoiceSeq
		{
			set
			{
				invoiceSeq = value;
			}
			get
			{
				return invoiceSeq;
			}
		}
		private string invoiceNo;//���Ժ�
		/// <summary>
		/// ���Ժ�
		/// </summary>
		public string InvoiceNo
		{
			set
			{
				invoiceNo = value;
			}
			get
			{
				return invoiceNo;
			}
		}
		private string realInvoiceNo;//ʵ�ʷ�Ʊ��
		/// <summary>
		/// ʵ�ʷ�Ʊ��,��Ʊ��ԭʼӡ�ĺ�
		/// </summary>
		public string RealInvoiceNo
		{
			set
			{
				realInvoiceNo = value;
			}
			get
			{
				return realInvoiceNo;
			}
		}
		private string windowsNo;//��ҩ����
		/// <summary>
		/// ��ҩ����
		/// </summary>
		public string WindowsNo
		{
			get
			{
				return windowsNo;
			}
			set
			{
				windowsNo = value;
			}
		}
		private string operCode;//����Ա
		/// <summary>
		/// ����Ա
		/// </summary>
		public string OperCode
		{
			set
			{
				operCode = value;
			}
			get
			{
				return operCode;
			}
		}
		private DateTime operDate;
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperDate
		{
			set
			{
				operDate = value;
			}
			get
			{
				return operDate;
			}
		}

		/// <summary>
		/// Clone
		/// </summary>
		/// <returns></returns>
		public new BalanceExtend Clone()
		{
			Neusoft.HISFC.Models.Fee.Outpatient.BalanceExtend obj = base.Clone() as Neusoft.HISFC.Models.Fee.Outpatient.BalanceExtend;

			return obj;
		}
	}
}
