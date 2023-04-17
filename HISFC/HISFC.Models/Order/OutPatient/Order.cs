using System;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;
namespace FS.HISFC.Models.Order.OutPatient
{
	/// <summary>
	/// FS.HISFC.Models.Order.OutPatient.Order<br></br>
	/// [��������: ����ҽ������ʵ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///		/>
	/// </summary>
    [Serializable]
    public class Order : FS.HISFC.Models.Order.Order
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
        private FT ft;

		/// <summary>
		/// �շ���Ա
		/// </summary>
        private Base.OperEnvironment chargeOper;

		/// <summary>
		/// ȷ����
		/// </summary>
        private Base.OperEnvironment confirmOper;

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

        /// <summary>
        ///  ��������
        ///  ������ҽ��������ʽ
        ///  add by liuww 2011-3-8
        /// </summary>
        private NeuObject recipeType;

        /// <summary>
        /// �Ƿ񳬹�
        /// </summary>
        private bool isExceeded;
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
                if (seeno == null)
                {
                    seeno = string.Empty;
                }
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
                if (recipeSeq == null)
                {
                    recipeSeq = string.Empty;
                }
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
                if (ft == null)
                {
                    ft = new FT();
                }
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
		/// ȷ����
		/// </summary>
		public Base.OperEnvironment ConfirmOper
		{
			get
			{
                if (confirmOper == null)
                {
                    confirmOper = new OperEnvironment();
                }
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
            get
            {
                return useDays;
            }
            set
            {
                useDays = value;
            }
        }

        /// <summary>
        ///  ��������
        ///  ������ҽ��������ʽ
        ///  add by liuww 2011-3-8
        /// </summary>
        public NeuObject RecipeType
        {
            get
            {
                if (recipeType == null)
                {
                    recipeType = new NeuObject();
                }
                return this.recipeType;
            }
            set
            {
                this.recipeType = value;
            }
        }

        public bool IsExceeded
        {
            get
            {
                return this.isExceeded;
            }
            set
            {
                this.isExceeded = value;
            }
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
        [Obsolete("��ChargeOper.Oper����", true)]
        public NeuObject ChargeUser;

		/// <summary>
		/// �շѿ���
		/// </summary>
        [Obsolete("��ChargeOper.Dept����", true)]
        public NeuObject ChargeDept;

		/// <summary>
		/// �շ�ʱ��
		/// </summary>
		[Obsolete("��ChargeOper.OperTime����",true)]
		public DateTime ChargeTime;

		/// <summary>
		/// ȷ�Ͽ���
		/// </summary>
        [Obsolete("��ConfirmOper.Dept����", true)]
        public NeuObject ComfirmDept;
		/// <summary>
		/// ȷ����
		/// </summary>
        [Obsolete("��ConfirmOper.Oper����", true)]
        public NeuObject User_Comfirm;
		
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
			obj.chargeOper = this.ChargeOper.Clone();
			obj.confirmOper = this.ConfirmOper.Clone();
			obj.ft = this.FT.Clone();
			return obj;
		}

		#endregion

		#endregion
	}


    public enum EnumOutPatientBill
    {
        /// <summary>
        /// 0 ȫ��
        /// </summary>
        [FS.FrameWork.Public.Description("ȫ��")]
        AllBill = 0,

        /// <summary>
        /// 1 ������
        /// </summary>
        [FS.FrameWork.Public.Description("������")]
        RecipeBill,

        /// <summary>
        /// 2 ע�䵥
        /// </summary>
        [FS.FrameWork.Public.Description("ע�䵥")]
        InjectBill,

        /// <summary>
        /// 3 ��鵥
        /// </summary>
        [FS.FrameWork.Public.Description("��鵥")]
        PacsBill,

        /// <summary>
        /// 4 ���鵥
        /// </summary>
        [FS.FrameWork.Public.Description("���鵥")]
        LisBill,

        /// <summary>
        /// 5 ���Ƶ�
        /// </summary>
        [FS.FrameWork.Public.Description("���Ƶ�")]
        TreatmentBill,

        /// <summary>
        /// 6 ���Ƶ�
        /// </summary>
        [FS.FrameWork.Public.Description("���Ƶ�")]
        ClinicsBill,

        /// <summary>
        /// 7 ָ�����������¼����
        /// </summary>
        [FS.FrameWork.Public.Description("�����¼")]
        GuideBill,

        /// <summary>
        /// 8 ��ϵ�
        /// </summary>
        [FS.FrameWork.Public.Description("���֤��")]
        DiagNoseBill,

        /// <summary>
        /// 9 ���ô�����
        /// </summary>
        [FS.FrameWork.Public.Description("���ô�����")]
        OutRecipeBill,

        
        /// <summary>
        /// 10 ���ϵ�// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
        /// </summary>
        [FS.FrameWork.Public.Description("���ϵ�")]
        MaterialBill,

        /// <summary>
        /// 11 ���鵥��ִ
        /// </summary>
        [FS.FrameWork.Public.Description("���鵥��ִ")]
        LisReceiptBill,
        /// <summary>
        /// 12 ����
        /// </summary>
        [FS.FrameWork.Public.Description("����")]
        MedicalReportBill,
    }
}
