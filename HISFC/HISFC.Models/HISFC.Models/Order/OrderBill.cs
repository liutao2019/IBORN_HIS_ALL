using System;
//using FS.NFC;
using FS.HISFC;

namespace FS.HISFC.Models.Order
{
	/// <summary>
	/// FS.HISFC.Models.Order.OrderBill<br></br>
	/// [��������: ҽ������ʵ��]<br></br>
	/// [�� �� ��: ���Ʒ�]<br></br>
	/// [����ʱ��: 2006-09-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class OrderBill:FS.FrameWork.Models.NeuObject
	{
		public OrderBill()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����

		/// <summary>
		/// ����ҽ��ʵ��
		/// </summary>
		private FS.HISFC.Models.Order.Inpatient.Order order = new Inpatient.Order();

		/// <summary>
		/// ҽ����ҳ��
		/// </summary>
		private int pageNO;

		/// <summary>
		/// ҽ�����к�
		/// </summary>
		private int   lineno;

		/// <summary>
		/// ҽ��������ӡ�굥
		/// </summary>
		private string Prnflag;
		
		/// <summary>
		/// ��ӡ������
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment printOper = new FS.HISFC.Models.Base.OperEnvironment();

		/// <summary>
		/// ��ӡֹͣ���
		/// </summary>
		private string printDCFlag ;

		/// <summary>
		/// ҽ���������
		/// </summary>
		private int moTimes;

		/// <summary>
		/// ҽ������ʱ��
		/// </summary>
		private DateTime moTime;

		/// <summary>
		/// ����Ա������ʱ��
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ��ӡ���
        /// </summary>
        private int printSequence;

		#endregion

		#region ����
		///<summary>
		///�ۺϲ���ҽ��ʵ��
		///</summary>
		public FS.HISFC.Models.Order.Inpatient.Order Order 
		{
			get
			{
				return this.order;
			}
			set
			{
				this.order = value;
			}
		}

		/// <summary>
		/// ҽ����ҳ��
		/// </summary>
		public int   PageNO
		{
			get
			{
				return this.pageNO;
			}
			set
			{
				this.pageNO = value;
			}
		}

		/// <summary>
		/// ҽ�����к�
		/// </summary>
		public int LineNO
		{
			get
			{
				return this.lineno;
			}
			set
			{
				this.lineno = value;
			}
		}
		
		/// <summary>
		///  ҽ��������ӡ�굥
		/// </summary>
		public string PrintFlag
		{
			get
			{
				return this.Prnflag;
			}
			set
			{
				this.Prnflag = value;
			}
		}
	
		/// <summary>
		/// ��ӡ������
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment  PrintOper
		{
			get
			{
				return this.printOper;
			}
			set
			{
				this.printOper = value;
			}
		}

		/// <summary>
		/// ��ӡֹͣ���
		/// </summary>
		public string PrintDCFlag
		{
			get
			{
				return this.printDCFlag;
			}
			set
			{
				this.printDCFlag = value;
			}
		}

		/// <summary>
		/// ҽ���������
		/// </summary>
		public int MOTimes
		{
			get
			{
				return this.moTimes;
			}
			set
			{
				this.moTimes = value;
			}
		}

		/// <summary>
		/// ҽ������ʱ��
		/// </summary>
		public DateTime MOTime
		{
			get
			{
				return this.moTime;
			}
			set
			{
				this.moTime = value;
			}
		}

		/// <summary>
		/// ����Ա������ʱ��
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
        /// ��ӡ���
        /// </summary>
        public int PrintSequence
        {
            get 
            { 
                return this.printSequence; 
            }
            set
            {
                this.printSequence = value;
            }
        }

		#endregion

		#region ����
		/// <summary>
		/// ҽ������ʱ��
		/// </summary>
		[Obsolete("��MOTime����",true)]
		public DateTime MoDidt;
		/// <summary>
		/// ҽ���������
		/// </summary>
		[Obsolete("��MOTimes����",true)]
		public int      MoTimes;
		/// <summary>
		/// ��ӡ����
		/// </summary>
		[Obsolete("��PrintOper.Oper.OperTime����",true)]
		public DateTime  PrnDate;
                
		/// <summary>
		/// ֹͣҽ����ӡ��־
		/// </summary>
		[Obsolete("��PrintDCFlag����",true)]
		public string    PrnDcflag;
			/// <summary>
		/// ��ӡ��
		/// </summary>
		[Obsolete("��PrintOper.Oper.ID����",true)]
		public string    PrnUserCD;
		
		/// <summary>
		///��ȡ����Ա 
		/// </summary>
		[Obsolete("��Oper.Oper.ID����",true)]
		public string   OperCode;
		
		/// <summary>
		/// ��ȡʱ��
		/// </summary>
		[Obsolete("��Oper.OperTime����",true)]
		public DateTime OperDate;
	
		#endregion

		#region ����

		#region ��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new OrderBill Clone()
		{
			// TODO:  ��� Order.Clone ʵ��
			OrderBill obj=base.Clone() as OrderBill;
			obj.Order  = this.Order.Clone();
			obj.oper = this.oper.Clone();
			obj.printOper = this.printOper.Clone();
			return obj;
		}

		#endregion

		#endregion

   }
	
}
