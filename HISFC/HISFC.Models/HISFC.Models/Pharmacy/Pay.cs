using System;

namespace FS.HISFC.Models.Pharmacy
{
	/// <summary>
	/// [��������: �����̸���ʵ��]<br></br>
	/// [�� �� ��: ������]<br></br>
	/// [����ʱ��: 2006-09]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	///  ID �������
	/// </summary>
    [Serializable]
    public class Pay : FS.FrameWork.Models.NeuObject
	{
		public Pay()
		{

		}


		#region ����

		/// <summary>
		/// ��Ʊ��
		/// </summary>
		string invoiceNo = "";								
		
		/// <summary>
		/// ��Ʊ����
		/// </summary>
		DateTime invoiceTime = System.DateTime.MinValue;	

		/// <summary>
		/// ���۽��
		/// </summary>
		decimal retailCost;									

		/// <summary>
		/// �������
		/// </summary>
		decimal wholesaleCost;								

		/// <summary>
		/// ������(��Ʊ���)
		/// </summary>
		decimal purchaseCost;								

		/// <summary>
		/// �Żݽ��
		/// </summary>
		decimal discountCost;								

		/// <summary>
		/// �˷�
		/// </summary>
		decimal deliveryCost;	
		
		/// <summary>
		/// ͬһ��Ʊ�ڸ������
		/// </summary>
		int sequenceNo;			
		
		/// <summary>
		/// �����־ 0δ����  1�Ѹ��� 2��ɸ���
		/// </summary>
		string payState;
		
		/// <summary>
		/// �������� �ֽ�/֧Ʊ
		/// </summary>
		string payType;										

		/// <summary>
		/// ������
		/// </summary>
		decimal payCost;
		
		/// <summary>
		/// δ�����
		/// </summary>
		decimal unPayCost;				
		
		/// <summary>
		/// ������Ϣ(��Ա ʱ��)
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment payOper = new FS.HISFC.Models.Base.OperEnvironment();	

		/// <summary>
		/// ������
		/// </summary>
		FS.FrameWork.Models.NeuObject stockDept = new FS.FrameWork.Models.NeuObject();		

		/// <summary>
		/// ������λ
		/// </summary>
		FS.HISFC.Models.Pharmacy.Company company = new Company();			
		
		/// <summary>
		/// ������Ϣ
		/// </summary>
		FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

		/// <summary>
		/// ��չ�ֶ�
		/// </summary>
		string extend;										

		/// <summary>
		/// ��չ�ֶ�2 
		/// </summary>
		string extend1;									

		/// <summary>
		/// ��չ�ֶ�2
		/// </summary>
		string extend2;									

		/// <summary>
		/// ��չ����
		/// </summary>
		DateTime extendTime = System.DateTime.MinValue;		

		/// <summary>
		/// ��չ����
		/// </summary>
		decimal extendQty;		
							
		#endregion

		/// <summary>
		/// ��ⵥ�ݺ�
		/// </summary>
		public string InListNO
		{
			get
			{
				return this.inListCode;
			}
			set
			{
				this.inListCode = value;
			}
		}


		/// <summary>
		/// ��Ʊ��
		/// </summary>
		public string InvoiceNO
		{
			get
			{
				return this.invoiceNo;
			}
			set
			{
				this.invoiceNo = value;
			}
		}


		/// <summary>
		/// ��Ʊ����
		/// </summary>
		public DateTime InvoiceTime
		{
			get
			{
				return this.invoiceTime;
			}
			set
			{
				this.invoiceTime = value;
			}
		}


		/// <summary>
		/// ���۽��
		/// </summary>
		public decimal RetailCost
		{
			get
			{
				return this.retailCost;
			}
			set
			{
				this.retailCost = value;
			}
		}


		/// <summary>
		/// �������
		/// </summary>
		public decimal WholeSaleCost
		{
			get
			{
				return this.wholesaleCost;
			}
			set
			{
				this.wholesaleCost = value;
			}
		}


		/// <summary>
		/// �������Ʊ��
		/// </summary>
		public decimal PurchaseCost
		{
			get
			{
				return this.purchaseCost;
			}
			set
			{
				this.purchaseCost = value;
			}
		}


		/// <summary>
		/// �Żݽ��
		/// </summary>
		public decimal DisCountCost
		{
			get
			{
				return this.discountCost;
			}
			set
			{
				this.discountCost = value;
			}
		}


		/// <summary>
		/// �˷�
		/// </summary>
		public decimal DeliveryCost
		{
			get
			{
				return this.deliveryCost;
			}
			set
			{
				this.deliveryCost = value;
			}
		}


		/// <summary>
		/// ͬһ��Ʊ�ڸ������
		/// </summary>
		public int SequenceNO
		{
			get
			{
				return this.sequenceNo;
			}
			set
			{
				this.sequenceNo = value;
			}
		}


		/// <summary>
		/// �����־ 0δ����  1�Ѹ��� 2��ɸ���
		/// </summary>
		public string PayState
		{
			get
			{
				return this.payState;
			}
			set
			{
				this.payState = value;
			}
		}
	

		/// <summary>
		/// �������� �ֽ�/֧Ʊ
		/// </summary>
		public string PayType
		{
			get
			{
				return this.payType;
			}
			set
			{
				this.payType = value;
			}
		}


		/// <summary>
		/// ������
		/// </summary>
		public decimal PayCost
		{
			get
			{
				return this.payCost;
			}
			set
			{
				this.payCost = value;
			}
		}


		/// <summary>
		/// δ�����
		/// </summary>
		public decimal UnPayCost
		{
			get
			{
				return this.unPayCost;
			}
			set
			{
				this.unPayCost = value;
			}
		}


		/// <summary>
		/// ������Ϣ(������ ����ʱ��)
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment PayOper
		{
			get
			{
				return this.payOper;
			}
			set
			{
				this.payOper = value;
			}
		}


		/// <summary>
		/// ������
		/// </summary>
		public FS.FrameWork.Models.NeuObject StockDept
		{
			get
			{
				return this.stockDept;
			}
			set
			{
				this.stockDept = value;
			}
		}
		

		/// <summary>
		/// ������λ
		/// </summary>
		public Company Company
		{
			get
			{
				return this.company;
			}
			set
			{
				this.company = value;
			}
		}
		

		/// <summary>
		/// ������Ա��Ϣ
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment Oper
		{
			get
			{
				return this.oper;
			}
			set
			{
				this.oper = value;
			}
		}

		
		/// <summary>
		/// ��չ�ֶ�
		/// </summary>
		public string Extend
		{
			get
			{
				return this.extend;
			}
			set
			{
				this.extend = value;
			}
		}


		/// <summary>
		/// ��չ�ֶ�1
		/// </summary>
		public string Extend1
		{
			get
			{
				return this.extend1;
			}
			set
			{
				this.extend1 = value;
			}
		}


		/// <summary>
		/// ��չ�ֶ�2
		/// </summary>
		public string Extend2
		{
			get
			{
				return this.extend2;
			}
			set
			{
				this.extend2 = value;
			}
		}


		/// <summary>
		/// ��չ����
		/// </summary>
		public DateTime ExtendTime
		{
			get
			{
				return this.extendTime;
			}
			set
			{
				this.extendTime = value;
			}
		}


		/// <summary>
		/// ��չ����
		/// </summary>
		public decimal ExtendQty
		{
			get
			{
				return this.extendQty;
			}
			set
			{
				this.extendQty = value;
			}
		}


		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>���ص�ǰʵ������</returns>
		public new Pay Clone()
		{
			Pay pay = base.Clone() as Pay;

			pay.PayOper = this.PayOper.Clone();
			
			pay.StockDept = this.StockDept.Clone();

			pay.Company = this.Company.Clone();

			pay.Oper = this.Oper.Clone();

			return pay;
		}


		#endregion

		#region ��Ч����

		/// <summary>
		/// ��ⵥ�ݺ�
		/// </summary>
		string inListCode = "";	
		
		/// <summary>
		/// ��Ʊ����
		/// </summary>
		DateTime invoiceDate = System.DateTime.MinValue;	

		/// <summary>
		/// �����־ 0δ����  1�Ѹ��� 2��ɸ���
		/// </summary>
		string payFlag;					
		
		/// <summary>
		/// ��������
		/// </summary>
		DateTime payDate = System.DateTime.MinValue;	
		
		/// <summary>
		/// ������
		/// </summary>
		FS.FrameWork.Models.NeuObject payer = new FS.FrameWork.Models.NeuObject();	
	
		/// <summary>
		/// ������
		/// </summary>
		FS.FrameWork.Models.NeuObject drugDept = new FS.FrameWork.Models.NeuObject();		//������

		/// <summary>
		/// ����Ա
		/// </summary>
		string operCode;				
					
		/// <summary>
		/// ��������
		/// </summary>
		DateTime operDate;									//��������

		/// <summary>
		/// ��չ�ֶ�
		/// </summary>
		string extCode;										

		/// <summary>
		/// ��չ�ֶ�1 
		/// </summary>
		string extCode1;									

		/// <summary>
		/// ��չ�ֶ�2
		/// </summary>
		string extCode2;									

		/// <summary>
		/// ��չ����
		/// </summary>
		DateTime extDate = System.DateTime.MinValue;		

		/// <summary>
		/// ��չ����
		/// </summary>
		decimal extNumber;									

		/// <summary>
		/// ��ⵥ�ݺ�
		/// </summary>
		[System.Obsolete("�����ع� ����ΪInListNO����",true)]
		public string InListCode
		{
			get
			{
				return this.inListCode;
			}
			set
			{
				this.inListCode = value;
			}
		}


		/// <summary>
		/// ��Ʊ��
		/// </summary>
		[System.Obsolete("�����ع� ����ΪInvoiceNO����",true)]
		public string InvoiceNo
		{
			get
			{
				return this.invoiceNo;
			}
			set
			{
				this.invoiceNo = value;
			}
		}


		/// <summary>
		/// ��Ʊ����
		/// </summary>
		[System.Obsolete("�����ع� ����ΪInvoiceTime����",true)]
		public DateTime InvoiceDate
		{
			get
			{
				return this.invoiceDate;
			}
			set
			{
				this.invoiceDate = value;
			}
		}


		/// <summary>
		/// ͬһ��Ʊ�ڸ������
		/// </summary>
		[System.Obsolete("�����ع� ����ΪSequenceNO����",true)]
		public int SequenceNo
		{
			get
			{
				return this.sequenceNo;
			}
			set
			{
				this.sequenceNo = value;
			}
		}


		/// <summary>
		/// �����־ 0δ����  1�Ѹ��� 2��ɸ���
		/// </summary>
		[System.Obsolete("�����ع� ����ΪPayState����",true)]
		public string PayFlag
		{
			get
			{
				return this.payFlag;
			}
			set
			{
				this.payFlag = value;
			}
		}

		
		/// <summary>
		/// ��������
		/// </summary>
		[System.Obsolete("�����ع� ����ΪPayOper����",true)]
		public DateTime PayDate
		{
			get
			{
				return this.payDate;
			}
			set
			{
				this.payDate = value;
			}
		}


		/// <summary>
		/// ������
		/// </summary>
		[System.Obsolete("�����ع� ����ΪPayOper����",true)]
		public FS.FrameWork.Models.NeuObject Payer
		{
			get
			{
				return this.payer;
			}
			set
			{
				this.payer = value;
			}
		}


		/// <summary>
		/// ������
		/// </summary>
		[System.Obsolete("�����ع� ����ΪStockDept����",true)]
		public FS.FrameWork.Models.NeuObject DrugDept
		{
			get
			{
				return this.drugDept;
			}
			set
			{
				this.drugDept = value;
			}
		}


		/// <summary>
		/// ����Ա
		/// </summary>
		[System.Obsolete("�����ع� ����ΪOper����",true)]
		public string OperCode
		{
			get
			{
				return this.operCode;
			}
			set
			{
				this.operCode = value;
			}
		}


		/// <summary>
		/// ��������
		/// </summary>
		[System.Obsolete("�����ع� ����ΪOper����",true)]
		public DateTime OperDate
		{
			get
			{
				return this.operDate;
			}
			set
			{
				this.operDate = value;
			}
		}


		/// <summary>
		/// ��չ�ֶ�
		/// </summary>
		[System.Obsolete("�����ع� ����ΪExtend����",true)]
		public string ExtCode
		{
			get
			{
				return this.extCode;
			}
			set
			{
				this.extCode = value;
			}
		}


		/// <summary>
		/// ��չ�ֶ�1
		/// </summary>
		[System.Obsolete("�����ع� ����ΪExtend1����",true)]
		public string ExtCode1
		{
			get
			{
				return this.extCode1;
			}
			set
			{
				this.extCode1 = value;
			}
		}

		
		/// <summary>
		/// ��չ�ֶ�2
		/// </summary>
		[System.Obsolete("�����ع� ����ΪExtend2����",true)]
		public string ExtCode2
		{
			get
			{
				return this.extCode2;
			}
			set
			{
				this.extCode2 = value;
			}
		}


		/// <summary>
		/// ��չ����
		/// </summary>
		[System.Obsolete("�����ع� ����ΪExtendTime����",true)]
		public DateTime ExtDate
		{
			get
			{
				return this.extDate;
			}
			set
			{
				this.extDate = value;
			}
		}

		/// <summary>
		/// ��չ����
		/// </summary>
		[System.Obsolete("�����ع� ����ΪExtendQty����",true)]
		public decimal ExtNumber
		{
			get
			{
				return this.extNumber;
			}
			set
			{
				this.extNumber = value;
			}
		}

		#endregion
	}
}
