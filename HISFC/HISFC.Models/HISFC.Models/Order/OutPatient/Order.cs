using System;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;
namespace FS.HISFC.Models.Order.OutPatient
{
	/// <summary>
	/// FS.HISFC.Models.Order.OutPatient.Order<br></br>
	/// [��������: ����ҽ������ʵ��]<br></br>
	/// [�� �� ��: ���Ʒ�]<br></br>
	/// [����ʱ��: 2006-09-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///		/>
	/// </summary>
    [Serializable]
    public class Order:FS.HISFC.Models.Order.Order
	{
		public Order()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����

		#region ˽��

		/// <summary>
		/// �������
		/// </summary>
		private string seeno;

		/// <summary>
		/// �շ�����
		/// </summary>
		private string recipeSeq;

		/// <summary>
		/// �Һ�����
		/// </summary>
		private DateTime regDate;

		/// <summary>
		/// ��Ŀ������Ϣ
		/// </summary>
		private FT ft = new FT();

		/// <summary>
		/// �շ���Ա
		/// </summary>
		private Base.OperEnvironment chargeOper = new OperEnvironment();

		/// <summary>
		/// ȷ����
		/// </summary>
		private Base.OperEnvironment confirmOper = new OperEnvironment();

		/// <summary>
		/// �Ƿ��Ѿ��շ�
		/// </summary>
		protected bool isHaveCharged = false;

		/// <summary>
		/// �Ƿ���Ҫ�ն�ȷ��
		/// </summary>
		private bool isNeedConfirm = false;

        /// <summary>
        /// ��ҩ����
        /// </summary>
        private int useDays;
		#endregion

		#endregion

		#region ����

		/// <summary>
		/// �������
		/// </summary>
		public string SeeNO
		{
			get
			{
				return this.seeno;
			}
			set
			{
				this.seeno = value;
			}
		}

		/// <summary>
		/// �շ�����
		/// </summary>
		public  string ReciptSequence
		{
			get
			{
				return this.recipeSeq;
			}
			set
			{
				this.recipeSeq = value;
			}
		}

		/// <summary>
		/// �Һ�����
		/// </summary>
		public DateTime RegTime
		{
			get
			{
				return this.regDate;
			}
			set
			{
				this.regDate = value;
			}
		}

		/// <summary>
		/// ��Ŀ������Ϣ
		/// </summary>
		public FT FT
		{
			get
			{
				return ft;
			}
			set
			{
				this.ft = value;
			}
		}

		/// <summary>
		/// �շ���Ա
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
		/// ȷ����
		/// </summary>
		public Base.OperEnvironment ConfirmOper
		{
			get
			{
				return this.confirmOper;		
			}
			set
			{
				this.confirmOper = value;
			}
		}

		/// <summary>
		/// �Ƿ��Ѿ��շ�
		/// </summary>
		public bool IsHaveCharged
		{
			get
			{
				return isHaveCharged ;
			}
			set
			{
				isHaveCharged = value;
			}
		}

		/// <summary>
		/// �Ƿ���Ҫ�ն�ȷ��
		/// </summary>
		public bool IsNeedConfirm
		{
			get
			{
				return this.isNeedConfirm;
			}
			set
			{
				this.isNeedConfirm = value;
			}
		}

        /// <summary>
        /// ��ҩ����
        /// </summary>
        public int UseDays
        {
            get { return useDays; }
            set { useDays = value; }
        }
		#endregion

		#region ���ϵ�

		/// <summary>
		/// �������
		/// </summary>
		[Obsolete("��SeeNO",true)]
		public string SeeNo 
		{
			get
			{
				return seeno;
			}
			set
			{
				seeno = value;
			}
		}

		/// <summary>
		/// ��Ŀ����ˮ��
		/// </summary>
		[Obsolete("��SequenceNO",true)]
		public int SeqNo
		{
			get
			{
				return int.Parse(base.ID);
			}
			set
			{
				base.ID  = value.ToString();
			}
		}

		/// <summary>
		/// �շ�����
		/// </summary>
		[Obsolete("��ReciptSequence",true)]
		public  string RecipeSeq
		{
			get
			{
				return this.recipeSeq;
			}
			set
			{
				this.recipeSeq = value;
			}
		}

		/// <summary>
		/// �Һ�����
		/// </summary>
		[Obsolete("��RegTime",true)]
		public DateTime RegDate
		{
			get
			{
				return regDate;
			}
			set
			{
				regDate = value;
			}
		}

		/// <summary>
		/// �շ���
		/// </summary>
		[Obsolete("��ChargeOper.Oper����",true)]
		public NeuObject ChargeUser=new NeuObject();
		/// <summary>
		/// �շѿ���
		/// </summary>
		[Obsolete("��ChargeOper.Dept����",true)]
		public NeuObject ChargeDept=new NeuObject();
		/// <summary>
		/// �շ�ʱ��
		/// </summary>
		[Obsolete("��ChargeOper.OperTime����",true)]
		public DateTime ChargeTime;

		/// <summary>
		/// ȷ�Ͽ���
		/// </summary>
		[Obsolete("��ConfirmOper.Dept����",true)]
		public NeuObject ComfirmDept = new NeuObject();
		/// <summary>
		/// ȷ����
		/// </summary>
		[Obsolete("��ConfirmOper.Oper����",true)]
		public NeuObject User_Comfirm = new NeuObject();
		
		#endregion

		#region ����

		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new Order Clone()
		{
			// TODO:  ��� Order.Clone ʵ��
			Order obj = base.Clone() as Order;
			obj.chargeOper = this.chargeOper.Clone();
			obj.confirmOper = this.confirmOper.Clone();
			obj.ft = this.ft.Clone();
			return obj;
		}

		#endregion

		#endregion
	
	}
}
