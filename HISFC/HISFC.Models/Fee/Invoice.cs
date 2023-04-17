using System;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;
using FS.HISFC.Models.RADT;

namespace FS.HISFC.Models.Fee
{
	/// <summary>
	/// Invoice<br></br>
	/// [��������: ��Ʊ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-01]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public class Invoice : NeuObject
	{

		#region ����
		
		/// <summary>
		/// ��Ʊ����
		/// </summary>
        private NeuObject invoiceType;
		
		/// <summary>
		/// ��Ч״̬
		/// </summary>
		private string validState;
		
		/// <summary>
		/// ��÷�Ʊ�Ĳ���Ա��Ϣ
		/// </summary>
        private Employee acceptOper;
		
		/// <summary>
		/// ��ȡʱ��
		/// </summary>
		private DateTime acceptTime;
		
		/// <summary>
		/// ��Ʊ��ʼ��
		/// </summary>
		private string beginNO;
		
		/// <summary>
		/// ��Ʊ��ֹ��
		/// </summary>
		private string endNO;
		
		/// <summary>
		/// ��ǰʹ�ú�
		/// </summary>
		private string usedNO;
	
		/// <summary>
		/// ��Ʊ��Ŀ
		/// </summary>
		private int qty;

		/// <summary>
		/// �Ƿ���
		/// </summary>
		private bool isPublic;

		#endregion

		#region ����
		
		/// <summary>
		/// ��Ʊ����
		/// </summary>
        public NeuObject Type
        {
            set
            {
                this.invoiceType = value;
            }
            get
            {
                if (invoiceType == null)
                {
                    invoiceType = new NeuObject();
                }
                return this.invoiceType;
            }
        }

		/// <summary>
		/// ��Ч״̬
		/// </summary>
		public string ValidState
		{
			get
			{
				return this.validState;
			}
			set
			{
				this.validState = value;
			}
		}
		
		/// <summary>
		/// ��÷�Ʊ�Ĳ���Ա
		/// </summary>
		public Employee AcceptOper
		{
			get
			{
                if (acceptOper == null)
                {
                    acceptOper = new Employee();
                }
				return this.acceptOper;
			}
			set
			{
				this.acceptOper = value;
			}
		}
		
		/// <summary>
		/// ��ȡʱ��
		/// </summary>
		public DateTime AcceptTime
		{
			get
			{
				return this.acceptTime;
			}
			set
			{
				this.acceptTime = value;
			}
		}
		
		/// <summary>
		/// ��Ʊ��ʼ��
		/// </summary>
		public string BeginNO
		{
			get
			{
				return this.beginNO;
			}
			set
			{
				this.beginNO = value.PadLeft(10, '0');
			}
		
		}
		
		/// <summary>
		/// ��Ʊ��ֹ��
		/// </summary>
		public string EndNO
		{
			get
			{
				return this.endNO;
			}
			set
			{
				this.endNO = value.PadLeft(10, '0');
			}
		
		}

		/// <summary>
		/// ��ǰʹ�ú�
		/// </summary>
		public string UsedNO
		{
			get
			{
				return this.usedNO;
			}
			set
			{
				this.usedNO = value.PadLeft(10, '0');
			}
		}

		/// <summary>
		/// ��Ʊ��Ŀ
		/// </summary>
		public int Qty
		{
			get
			{
				return this.qty;
			}
			set
			{
				this.qty = value;
			}
		}

		/// <summary>
		/// �Ƿ���
		/// </summary>
		public bool IsPublic
		{
			get
			{
				return this.isPublic;
			}
			set
			{
				this.isPublic = value;
			}
		}

		#endregion
		
		#region ����

		#region ��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>��ǰ�����ʵ������</returns>
		public new Invoice Clone()
		{
			Invoice invoice = base.Clone() as Invoice;

			invoice.AcceptOper = this.AcceptOper.Clone();
			invoice.Type = this.Type.Clone();
            
			return invoice;
		}

		#endregion

		#endregion
	}
}
