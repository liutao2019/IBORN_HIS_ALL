using System;

namespace Neusoft.HISFC.Object.Fee.OutPatient
{
	/// <summary>
	/// InvoiceExtend ��ժҪ˵����
	/// </summary>
	public class InvoiceExtend : Neusoft.NFC.Object.NeuObject 
	{
		public InvoiceExtend()
		{
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
		public new Neusoft.HISFC.Object.Fee.OutPatient.InvoiceExtend Clone()
		{
			Neusoft.HISFC.Object.Fee.OutPatient.InvoiceExtend obj = base.Clone() as Neusoft.HISFC.Object.Fee.OutPatient.InvoiceExtend;

			return obj;
		}
	}
}
