using System;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;
using FS.HISFC;
namespace FS.HISFC.Models.Order
{
	/// <summary>
	/// FS.HISFC.Models.Order.ExecOrder<br></br>
	/// [��������: ҽ��ִ������ʵ��]<br></br>
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
        private Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();

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
		private Base.OperEnvironment dcExecOper = new OperEnvironment();

		/// <summary>
		/// �Ƿ�ִ�У�1����ִ�� 0��δִ�� ��
		/// </summary>
		private bool isExec = false;

		/// <summary>
		/// ִ����
		/// </summary>
		private Base.OperEnvironment execOper = new OperEnvironment();

		/// <summary>
		/// �Ƿ��շѣ�1�����շ� 0��δ�շ� ��
		/// </summary>
		private bool isCharge = false;

		/// <summary>
		/// �շ���
		/// </summary>
		private Base.OperEnvironment chargeOper = new OperEnvironment();

		/// <summary>
		/// ��ҩʱ��
		/// </summary>
		private DateTime drugedTime;

        /// <summary>
        /// �Ƿ�ȷ��
        /// {DA77B01B-63DF-4559-B264-798E54F24ABB}
        /// </summary>
        private bool isConfirm = false;

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
		[Obsolete("������DCExecOper.Oper.ID",true)]
		public Base.Employee DcExecUser = new Employee();

		/// <summary>
		/// ����ʱ��
		/// </summary>
		[Obsolete("����ʱ��DCExecOper.Oper.OperTime",true)]
		public DateTime DcExecTime;

		/// <summary>
		/// ִ����
		/// </summary>
		[Obsolete("ExecOper.Oper.OperID",true)]
		public NeuObject ExecUser=new NeuObject();

		/// <summary>
		/// ִ��ʱ��
		/// </summary>
		[Obsolete("ExecOper.Oper.OperTime",true)]
		public DateTime ExecTime;

		/// <summary>
		/// �շ���
		/// </summary>
		[Obsolete("ChargeOper.Oper.Oper",true)]
		public NeuObject ChargeUser=new NeuObject();

		/// <summary>
		/// �շѿ���
		/// </summary>
		[Obsolete("ChargeOper.Oper.Dept",true)]
		public NeuObject ChargeDept=new NeuObject();

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
			
			obj.execOper = this.execOper.Clone();
			obj.dcExecOper = this.dcExecOper.Clone();
			obj.chargeOper = this.chargeOper.Clone();

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
