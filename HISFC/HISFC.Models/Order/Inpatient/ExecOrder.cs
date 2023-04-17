using System;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;
using FS.HISFC;
namespace FS.HISFC.Models.Order
{
	/// <summary>
	/// FS.HISFC.Models.Order.ExecOrder<br></br>
	/// [��������: ҽ��ִ������ʵ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class ExecOrder:FS.FrameWork.Models.NeuObject,FS.HISFC.Models.Base.IValid
	{
		public ExecOrder()
		{
			
		}

		#region ����

		#region ˽��

		/// <summary>
		/// ҽ����Ϣ
		/// </summary>
        private Models.Order.Inpatient.Order order;
		/// <summary>
		/// ��ʼʹ��ʱ��
		/// </summary>
		private DateTime dateUse;

		/// <summary>
		/// �ֽ�ʱ��
		/// </summary>
		private DateTime dateDeco;

		/// <summary>
		/// 0���跢��/1���з���/2��ɢ����/3����ҩ
		/// </summary>
		private int drugFlag = 0;

		/// <summary>
		/// �Ƿ���Ч��1����Ч 0������ ��
		/// </summary>
		private bool isValid = true;

		/// <summary>
		/// ������
		/// </summary>
        private Base.OperEnvironment dcExecOper;

		/// <summary>
		/// �Ƿ�ִ�У�1����ִ�� 0��δִ�� ��
		/// </summary>
		private bool isExec = false;

		/// <summary>
		/// ִ����
		/// </summary>
        private Base.OperEnvironment execOper;

		/// <summary>
		/// �Ƿ��շѣ�1�����շ� 0��δ�շ� ��
		/// </summary>
		private bool isCharge = false;

		/// <summary>
		/// �շ���
		/// </summary>
        private Base.OperEnvironment chargeOper;

		/// <summary>
		/// ��ҩʱ��
		/// </summary>
		private DateTime drugedTime;

        /// <summary>
        /// �Ƿ�ȷ��
        /// {DA77B01B-63DF-4559-B264-798E54F24ABB}
        /// </summary>
        private bool isConfirm = false;

        /// <summary>
        /// ���
        /// </summary>
        private int subCombNO;

        /// <summary>
        /// ���
        /// </summary>
        public int SubCombNO
        {
            get
            {
                return subCombNO;
            }
            set
            {
                subCombNO = value;
            }
        }

        /// <summary>
        /// ҽ���������
        /// </summary>
        private int sortID;

        /// <summary>
        /// ҽ���������
        /// </summary>
        protected int SortID
        {
            get
            {
                return sortID;
            }
            set
            {
                sortID = value;
            }
        }

		#endregion

		#endregion ����

		#region ����
		/// <summary>
		/// ҽ����Ϣ
		/// </summary>
        public Models.Order.Inpatient.Order Order
		{
			get
			{
                if (order == null)
                {
                    order = new FS.HISFC.Models.Order.Inpatient.Order();
                }

				return this.order;
			}
			set
			{
				this.order = value;
			}
		}

		/// <summary>
		/// ��ʼʹ��ʱ��
		/// </summary>
		public DateTime DateUse
		{
			get
			{
				return this.dateUse;
			}
			set
			{
				this.dateUse = value;
			}
		}

		/// <summary>
		/// �ֽ�ʱ��
		/// </summary>
		public DateTime DateDeco
		{
			get
			{
				return this.dateDeco;
			}
			set
			{
				this.dateDeco = value;
			}
		}

		/// <summary>
		/// 0���跢��/1���з���/2��ɢ����/3����ҩ
		/// </summary>
		public int DrugFlag
		{
			get
			{
				return this.drugFlag;
			}
			set
			{
				this.drugFlag = value;
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public Base.OperEnvironment DCExecOper
		{
			get
			{
                if (dcExecOper == null)
                {
                    dcExecOper = new OperEnvironment();
                }
				return this.dcExecOper;
			}
			set
			{
				this.dcExecOper = value;
			}
		}

		/// <summary>
		/// �Ƿ�ִ�У�1����ִ�� 0��δִ�� ��
		/// </summary>
		public bool IsExec
		{
			get
			{
				return this.isExec;
			}
			set
			{
				this.isExec = value;
			}
		}

		/// <summary>
		/// ִ����
		/// </summary>
		public Base.OperEnvironment ExecOper
		{
			get
			{
                if (execOper == null)
                {
                    execOper = new OperEnvironment();
                }

				return this.execOper;
			}
			set
			{
				this.execOper = value;
			}
		}

		/// <summary>
		/// �Ƿ��շѣ�1�����շ� 0��δ�շ� ��
		/// </summary>
		public bool IsCharge 
		{
			get
			{
				return this.isCharge;
			}
			set
			{
				this.isCharge = value;
			}
		}

		/// <summary>
		/// �շ���
		/// </summary>
		public Base.OperEnvironment ChargeOper
		{
			get
			{
                if (chargeOper == null)
                {
                    chargeOper = new OperEnvironment();
                }

				return this.chargeOper;
			}
			set
			{
				this.chargeOper = value;
			}
		}

		/// <summary>
		/// ��ҩʱ��
		/// </summary>
		public DateTime DrugedTime
		{
			get
			{
				return this.drugedTime;
			}set
			 {
				 this.drugedTime = value;
			 }
		}

        /// <summary>
        /// �Ƿ�ȷ��
        /// {DA77B01B-63DF-4559-B264-798E54F24ABB}
        /// </summary>
        public bool IsConfirm
        {
            get { return isConfirm; }
            set { isConfirm = value; }
        }

		#endregion ����

		#region ���ϵ�
		/// <summary>
		/// ������
		/// </summary>
        [Obsolete("������DCExecOper.Oper.ID", true)]
        public Base.Employee DcExecUser
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

		/// <summary>
		/// ����ʱ��
		/// </summary>
		[Obsolete("����ʱ��DCExecOper.Oper.OperTime",true)]
		public DateTime DcExecTime;

		/// <summary>
		/// ִ����
		/// </summary>
        [Obsolete("ExecOper.Oper.OperID", true)]
        public NeuObject ExecUser
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

		/// <summary>
		/// ִ��ʱ��
		/// </summary>
		[Obsolete("ExecOper.Oper.OperTime",true)]
		public DateTime ExecTime;

		/// <summary>
		/// �շ���
		/// </summary>
        [Obsolete("ChargeOper.Oper.Oper", true)]
        public NeuObject ChargeUser
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

		/// <summary>
		/// �շѿ���
		/// </summary>
        [Obsolete("ChargeOper.Oper.Dept", true)]
        public NeuObject ChargeDept
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

		/// <summary>
		/// �շ�ʱ��
		/// </summary>
		[Obsolete("ChargeOper.Oper.OperTime",true)]
		public DateTime ChargeTime;

		#endregion ���ϵ�

		#region ����

		#region ��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new ExecOrder Clone()
		{
			// TODO:  ��� Order.Clone ʵ��
			ExecOrder obj=base.Clone() as ExecOrder;

			try{obj.Order =this.Order.Clone();}
			catch{};
			
			obj.execOper = this.ExecOper.Clone();
			obj.dcExecOper = this.DCExecOper.Clone();
			obj.chargeOper = this.ChargeOper.Clone();

			return obj;
		}

		#endregion

		#endregion

		#region �ӿ�ʵ��

		#region IValid �ӿ�ʵ��

		/// <summary>
		/// �Ƿ���Ч��1����Ч 0������ ��
		/// </summary>
		public bool IsValid
		{
			get
			{
				// TODO:  ��� ExecOrder.IsInvalid getter ʵ��
				return this.isValid;
			}
			set
			{
				// TODO:  ��� ExecOrder.IsInvalid setter ʵ��
				this.isValid =value;
			}
		}

		#endregion

		#endregion
	}
}
