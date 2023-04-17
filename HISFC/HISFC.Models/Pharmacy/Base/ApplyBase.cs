using System;

namespace Neusoft.HISFC.Object.Pharmacy.Base
{
	/// <summary>
	/// [��������: ҩƷ�����ʹ���������Ϣ����]<br></br>
	/// [�� �� ��: ������]<br></br>
	/// [����ʱ��: 2006-09-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	///  ID �������
	/// </summary>
	public abstract class ApplyBase : Neusoft.NFC.Object.NeuObject
	{
		public ApplyBase()
		{

		}


		#region ����

		/// <summary>
		/// �������
		/// </summary>
		private Neusoft.NFC.Object.NeuObject applyDept;

		/// <summary>
		/// ������
		/// </summary>
		private Neusoft.NFC.Object.NeuObject stockDept;

		/// <summary>
		/// ���뵥��
		/// </summary>
		private string billCode;

		/// <summary>
		/// ���������Ϣ
		/// </summary>
		private Neusoft.HISFC.Object.Base.OperEnvironment applyOper = new Neusoft.HISFC.Object.Base.OperEnvironment();

		/// <summary>
		/// ״̬
		/// </summary>
		private string state;

		/// <summary>
		/// ��������
		/// </summary>
		private decimal applyQty;

		/// <summary>
		/// ������Ϣ
		/// </summary>
		private Neusoft.HISFC.Object.Base.OperEnvironment examOper = new Neusoft.HISFC.Object.Base.OperEnvironment();

		/// <summary>
		/// ��׼����
		/// </summary>
		private decimal approveQty;

		/// <summary>
		/// ��׼������Ϣ
		/// </summary>
		private Neusoft.HISFC.Object.Base.OperEnvironment approveOper = new Neusoft.HISFC.Object.Base.OperEnvironment();

		/// <summary>
		/// ������Ϣ
		/// </summary>
		private Neusoft.HISFC.Object.Base.OperEnvironment oper = new Neusoft.HISFC.Object.Base.OperEnvironment();

		#endregion

		/// <summary>
		/// ������Ŀ
		/// </summary>
		public abstract object Item
		{
			get;
			set;
		}


		/// <summary>
		/// �������
		/// </summary>
		public Neusoft.NFC.Object.NeuObject ApplyDept
		{
			get
			{
				return this.applyDept;
			}
			set
			{
				this.applyDept = value;
			}
		}


		/// <summary>
		/// ������
		/// </summary>
		public Neusoft.NFC.Object.NeuObject StockDept
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
		/// ���뵥��
		/// </summary>
		public string BillNO
		{
			get
			{
				return this.billCode;
			}
			set
			{
				this.billCode = value;
			}
		}


		/// <summary>
		/// ���������Ϣ
		/// </summary>
		public Neusoft.HISFC.Object.Base.OperEnvironment ApplyOper
		{
			get
			{
				return this.applyOper;
			}
			set
			{
				this.applyOper = value;
			}
		}


		/// <summary>
		/// ״̬
		/// </summary>
		public string State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}


		/// <summary>
		/// ��������
		/// </summary>
		public decimal ApplyQty
		{
			get
			{
				return this.applyQty;
			}
			set
			{
				this.applyQty = value;
			}
		}


		/// <summary>
		/// ����������Ϣ
		/// </summary>
		public Neusoft.HISFC.Object.Base.OperEnvironment ExamOper 
		{
			get
			{
				return this.examOper;
			}
			set
			{
				this.examOper = value;
			}
		}


		/// <summary>
		/// ��׼����
		/// </summary>
		public decimal ApproveQty
		{
			get
			{
				return this.approveQty;
			}
			set
			{
				this.approveQty = value;
			}
		}


		/// <summary>
		/// ��׼������Ϣ
		/// </summary>
		public Neusoft.HISFC.Object.Base.OperEnvironment ApproveOper
		{
			get
			{
				return this.approveOper;
			}
			set
			{
				this.approveOper = value;
			}
		}


		/// <summary>
		/// ������Ϣ
		/// </summary>
		public Neusoft.HISFC.Object.Base.OperEnvironment Oper
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


		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>���ص�ǰʵ���ĸ���</returns>
		public new ApplyBase Clone()
		{
			ApplyBase applyBase = base.Clone() as ApplyBase;
			
			applyBase.ApplyDept = this.ApplyDept.Clone();
			applyBase.StockDept = this.StockDept.Clone();
			applyBase.ApplyOper = this.ApplyOper.Clone();
			applyBase.ExamOper = this.ExamOper.Clone();
			applyBase.ApproveOper = this.ApproveOper.Clone();
			applyBase.Oper = this.Oper.Clone();

			return applyBase;

		}


		#endregion
	}
}
