using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.SIInterface;
using FS.HISFC.Models.RADT;
using System.Collections.Generic;

namespace FS.HISFC.Models.Fee 
{
	/// <summary>
	/// FeeItemBase<br></br>
	/// [��������: ������ϸ����]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-13]<br></br>
	/// <�޸ļ�¼ 
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public abstract class FeeItemBase : NeuObject
	{
		#region ����
        #region {46381C92-851C-42b6-BCD2-7559A375240F}
        /// <summary>
        /// ҽ�����߻�����Ϣ,������Ϣ
        /// </summary>
        private SIFeeItemBase siFeeItemBase;
        #endregion
   
		/// <summary>
		/// �������� TransTypes.Positive ������(1), TransTypes.Negative ������(2)
		/// </summary>
		private TransTypes transType;
		
		/// <summary>
		///  ������Ϣ ��Ч:Valid(0),�˷�����:Canceled(1) �ش�:Reprint(2) ע��:LogOut(3)
		/// </summary>
		private CancelTypes cancelType;
		
		/// <summary>
		/// ���߻�����Ϣ
		/// </summary>
        private Patient patient ;
		
		/// <summary>
		/// ������Ŀ��Ϣ
		/// </summary>
		private Base.Item item ;// new FS.HISFC.Models.Base.Item();
		
		/// <summary>
		/// �Ƿ�����
		/// </summary>
		private bool isGroup;
		
		/// <summary>
		/// ��Ʊ��Ϣ
		/// </summary>
		private Invoice invoice ;// new Invoice();

		/// <summary>
		/// ������
		/// </summary>
        private string recipeNO;
		
		/// <summary>
		/// ��������ˮ��
		/// </summary>
		private int sequenceNO;
		
		/// <summary>
		/// �շ�״̬��Ϣ
		/// </summary>
		private PayTypes payType;
		
		/// <summary>
		/// ҽ����Ϣ
		/// </summary>
		private Order.Order order;//new FS.HISFC.Models.Order.Order();

		/// <summary>
		/// ������Ϣ
		/// </summary>
        private FT ft;

        /// <summary>
        /// {6CBD45BC-F6C7-4ae0-A338-2E251423B418}
        /// ҽ��������Ϣ
        /// </summary>
        private FT sift;

        /// <summary>
        /// {DC67634A-696F-4e03-8C63-447C4265817E}
        /// ҽ��������ҩ��ʶ 0 Ĭ��/1�ɱ�/2���ɱ�
        /// </summary>
        private string rangeflag = null;
		
		/// <summary>
		/// ����
		/// </summary>
		private decimal days;
		
		/// <summary>
		/// �շ�ʱ�ĵ�λ 1 ��װ��λ 0 ��С��λ
		/// </summary>
        private string feePack;

		/// <summary>
		/// ��������
		/// </summary>
		private decimal noBackQty;
		
		/// <summary>
		/// ������Դ �շ�Ա����(0) ҽ��(1) �ն�(2) ���(3) 
		/// </summary>
        private string ftSource;
		
		/// <summary>
		/// �����Ŀ��Ϣ
		/// </summary> 
        private Item.UndrugComb undrugComb;//new Item.UndrugComb(); 
		
		/// <summary>
		/// ������������(����ҽ��,����ҽ�����ڿ���,ҽ������ʱ��)
		/// </summary>
		private OperEnvironment recipeOper;// new OperEnvironment();
		
		/// <summary>
		/// ���۲�������(������,���ۿ���,����ʱ��)
		/// </summary>
		private OperEnvironment chargeOper;// new OperEnvironment();
		
		/// <summary>
		/// �շѲ�������(�շ���,�շѿ���,�շ�ʱ��)
		/// </summary>
		private OperEnvironment feeOper;// new OperEnvironment();
		
		/// <summary>
		/// �˷Ѳ�������(�˷���,�˷������ڿ���,�˷�ʱ��)
		/// </summary>
		private OperEnvironment cancelOper ;// new OperEnvironment();
		
		/// <summary>
		/// ִ�в�������(ִ����,ִ�п���, ִ��ʱ��)
		/// </summary>
		private OperEnvironment execOper;// new OperEnvironment();
		
		/// <summary>
		/// �ۿ���������(�ۿ����,�ۿ����,�ۿ�ʱ��)
		/// </summary>
		private OperEnvironment stockOper ;// new OperEnvironment();
		
		/// <summary>
		/// �Ƿ��Ѿ��ն�ȷ��
		/// </summary>
		private bool isConfirmed;
		
		/// <summary>
		/// �ն�ȷ�ϵ�����
		/// </summary>
		private decimal confirmedQty;

		/// <summary>
		/// ȷ�ϲ�������(ȷ����,ȷ�Ͽ���,ȷ��ʱ��)
		/// </summary>
		private OperEnvironment confirmOper ;// new OperEnvironment();
		
		/// <summary>
		/// ҽ��������Ϣ
		/// </summary>
		private Compare compare;// new Compare();
		
		/// <summary>
		/// �����־
		/// </summary>
		private bool isEmergency;

        /// <summary>
        /// ԭ�������ţ�����ǰ��
        /// </summary>
        private string cancelRecipeNO;

        /// <summary>
        /// ԭ��������ˮ�ţ�����ǰ��
        /// </summary>
        private int cancelSequenceNO;

        /// <summary>
        /// �ۿ���ˮ��
        /// </summary>
        ////{25934862-5C82-409c-A0D7-7BE5A24FFC75}
        private int updateSequence;
        /// <summary>
        /// �շ��з�ҩƷ����Ӧ������
        /// </summary>
        //{25934862-5C82-409c-A0D7-7BE5A24FFC75}
        //private List<HISFC.Object.Material.Output> mateList = new List<FS.HISFC.Models.FeeStuff.Output>();
        private List<FS.HISFC.Models.FeeStuff.Output> mateList;// new List<FS.HISFC.Models.FeeStuff.Output>();

        /// <summary>
        /// �Ƿ�Э������
        /// </summary>
        private bool isNostrum = false;


        /// <summary>
        /// �Ƿ��ϴ�
        /// </summary>
        private bool isUpload = false;

        /// <summary>
        /// ͳ�ƴ���
        /// </summary>
        private FeeCodeStat feeCodeStat;

		#endregion

		#region ����
        #region {46381C92-851C-42b6-BCD2-7559A375240F}
        /// <summary>
        /// ҽ�����߻�����Ϣ,������Ϣ
        /// </summary>
        public SIFeeItemBase SIFeeItemBase
        {
            get {
                if (siFeeItemBase == null)
                {
                    siFeeItemBase = new SIFeeItemBase();
                }
                return siFeeItemBase; }
            set { siFeeItemBase = value; }
        } 
        #endregion
		/// <summary>
		/// �������� TransTypes.Positive ������(1), TransTypes.Negative ������(2)
		/// </summary>
		public TransTypes TransType
		{
			get
			{
				return transType;
			}
			set
			{
				transType = value;
			}
		}
		
		/// <summary>
		/// ������Ϣ ��Ч:Valid(0),�˷�����:Canceled(1) �ش�:Reprint(2) ע��:LogOut(3)
		/// </summary>
		public CancelTypes CancelType
		{
			get
			{
				return this.cancelType;
			}
			set
			{
				this.cancelType = value;
			}
		}
		
		/// <summary>
		/// ���߻�����Ϣ
		/// </summary>
		public Patient Patient
		{
			get
			{
                if (this.patient == null)
                {
                    this.patient = new Patient();
                }
				return this.patient;
			}
			set
			{
				this.patient = value;
			}
		}

		/// <summary>
		/// ������Ŀ��Ϣ
		/// </summary>
		public Base.Item Item
		{
			get
			{
                if (this.item == null)
                {
                    this.item = new FS.HISFC.Models.Base.Item();
                }
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}

		/// <summary>
		/// �Ƿ����� ture �� false ����
		/// </summary>
		public bool IsGroup
		{
			get
			{
				return isGroup;
			}
			set
			{
				isGroup = value;
			}
		}
		
		/// <summary>
		/// ��Ʊ��Ϣ
		/// </summary>
		public Invoice Invoice
		{
			get
			{
                if (invoice == null)
                {
                    invoice = new Invoice();
                }
				return this.invoice;
			}
			set
			{
				this.invoice = value;
			}
		}

        /// <summary>
        /// ͳ�ƴ���
        /// </summary>
        public FeeCodeStat FeeCodeStat
        {
            get
            {
                if (feeCodeStat == null)
                {
                    feeCodeStat = new FeeCodeStat();
                }

                return this.feeCodeStat;
            }
            set
            {
                this.feeCodeStat = value;
            }
        }
		
		/// <summary>
		/// ������
		/// </summary>
		public string RecipeNO
		{
			get
			{
                if (recipeNO == null)
                {
                    recipeNO = string.Empty;
                }
				return this.recipeNO;
			}
			set
			{
				this.recipeNO = value;
			}
		}
		
		/// <summary>
		/// ��������ˮ��
		/// </summary>
		public int SequenceNO
		{
			get
			{
				return this.sequenceNO;
			}
			set
			{
				this.sequenceNO = value;
			}
		}

		/// <summary>
		/// �շ�״̬��Ϣ Charged ���� Balanced �շ�
		/// </summary>
		public PayTypes PayType
		{
			get
			{
				return payType;
			}
			set
			{
				payType = value;
			}
		}	
		
		/// <summary>
		/// ������Ϣ
		/// </summary>
		public FT FT
		{
			get
			{
                if (ft == null)
                {
                    ft = new FT();
                }
				return this.ft;
			}
			set
			{
				this.ft = value;
			}
		}

        /// <summary>
        /// {DC67634A-696F-4e03-8C63-447C4265817E}
        /// ҽ��������ҩ��ʶ
        /// </summary>
        public string RangeFlag
        {
            get
            {
                if (rangeflag == null)
                {
                    rangeflag = string.Empty;
                }
                return this.rangeflag;
            }
            set
            {
                this.rangeflag = value;
            }
        }

        /// <summary>
        /// {6CBD45BC-F6C7-4ae0-A338-2E251423B418}
        /// ҽ��������Ϣ
        /// </summary>
        public FT SIft
        {
            get 
            {
                if (sift == null)
                {
                    sift = new FT();
                }
                return this.sift;
            }
            set 
            { 
                sift = value; 
            }
        }
		
		/// <summary>
		/// ҽ����Ϣ
		/// </summary>
		public Order.Order Order
		{
			get
			{
                if (this.order == null)
                {
                    this.order = new FS.HISFC.Models.Order.Order();
                }
				return this.order;
			}
			set
			{
				this.order = value;
			}
		}
		
		/// <summary>
		/// ����
		/// </summary>
		public decimal Days
		{
			get
			{
				return this.days;
			}
			set
			{
				this.days = value;
			}
		}
		
		/// <summary>
		/// ��������
		/// </summary>
		public decimal NoBackQty
		{
			get
			{
				return this.noBackQty;
			}
			set
			{
				this.noBackQty = value;
			}
		}
		
		/// <summary>
		/// �շ�ʱ�ĵ�λ 1 ��װ��λ 0 ��С��λ
		/// </summary>
		public string FeePack
		{
			get
			{
                if (feePack == null)
                {
                    feePack = string.Empty;
                }
				return this.feePack;
			}
			set
			{
				this.feePack = value;
			}
		}

		/// <summary>
		/// ������Դ �շ�Ա����(0) ҽ��(1) �ն�(2) ���(3) 
		/// </summary>
		public string FTSource
		{
			get
			{
                if (ftSource == null)
                {
                    ftSource = string.Empty;
                }
				return this.ftSource;
			}
			set
			{
				this.ftSource = value;
			}
		}

		/// <summary>
		/// �����Ŀ��Ϣ
		/// </summary>  
		public Item.UndrugComb UndrugComb
		{ 
			get
			{
                if (this.undrugComb == null)
                {
                    this.undrugComb = new FS.HISFC.Models.Fee.Item.UndrugComb();
                }
                return this.undrugComb;
			}
            set
            {
                this.undrugComb = value;
            }
		}

		/// <summary>
		/// ������������(����ҽ��,����ҽ�����ڿ���,ҽ������ʱ��)
		/// </summary>
		public OperEnvironment RecipeOper
		{
			get
			{
                if (recipeOper == null)
                {
                    recipeOper = new OperEnvironment();
                }
				return this.recipeOper;
			}	
			set
			{
				this.recipeOper = value;
			}
		}
		
		/// <summary>
		/// ���۲�������(������,���ۿ���,����ʱ��)
		/// </summary>
		public OperEnvironment ChargeOper
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
		/// ִ�в�������(ִ����,ִ�п���, ִ��ʱ��)
		/// </summary>
		public OperEnvironment FeeOper
		{
			get
			{
                if (feeOper == null)
                {
                    feeOper = new OperEnvironment();
                }
				return this.feeOper;
			}
			set
			{
				this.feeOper = value;
			}
		}
		
		/// <summary>
		/// �˷Ѳ�������(�˷���,�˷������ڿ���,�˷�ʱ��)
		/// </summary>
		public OperEnvironment CancelOper
		{
			get
			{
                if (cancelOper == null)
                {
                    cancelOper = new OperEnvironment();
                }
				return this.cancelOper;
			}
			set
			{
				this.cancelOper = value;
			}
		}

		/// <summary>
		///  ִ�в�������(ִ����,ִ�п���, ִ��ʱ��)
		/// </summary>
		public OperEnvironment ExecOper
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
		/// �Ƿ��Ѿ��ն�ȷ��
		/// </summary>
		public bool IsConfirmed
		{
			get
			{
				return this.isConfirmed;
			}
			set
			{
				this.isConfirmed = value;
			}
		}
		
		/// <summary>
		/// �ն�ȷ�ϵ�����
		/// </summary>
		public decimal ConfirmedQty
		{
			get
			{
				return this.confirmedQty;
			}
			set
			{
				this.confirmedQty = value;
			}
		}
		
		/// <summary>
		/// ȷ�ϲ�������(ȷ����,ȷ�Ͽ���,ȷ��ʱ��)
		/// </summary>
		public OperEnvironment ConfirmOper
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
		/// �ۿ���������(�ۿ����,�ۿ����,�ۿ�ʱ��)
		/// </summary>
		public OperEnvironment StockOper
		{
			get
			{
                if (stockOper == null)
                {
                    stockOper = new OperEnvironment();
                }
				return this.stockOper;
			}
			set
			{
				this.stockOper = value;
			}
		}

		/// <summary>
		/// ҽ��������Ϣ
		/// </summary>
        public Compare Compare = new Compare();

		/// <summary>
		/// �����־
		/// </summary>
		public bool IsEmergency
		{
			get
			{
				return this.isEmergency;
			}
			set
			{
				this.isEmergency = value;
			}
		}

        /// <summary>
        /// ԭ������
        /// </summary>
        public string CancelRecipeNO
        {
            get
            {
                if (cancelRecipeNO == null)
                {
                    cancelRecipeNO = string.Empty;
                }
                return this.cancelRecipeNO;
            }
            set
            {
                this.cancelRecipeNO = value;
            }
        }

        /// <summary>
        /// ��������ˮ��
        /// </summary>
        public int CancelSequenceNO
        {
            get
            {
                return this.cancelSequenceNO;
            }
            set
            {
                this.cancelSequenceNO = value;
            }
        }

        /// <summary>
        /// �ۿ���ˮ��
        /// </summary>
        //{25934862-5C82-409c-A0D7-7BE5A24FFC75}
        public int UpdateSequence
        {
            get
            {
                return this.updateSequence;
            }
            set
            {
                this.updateSequence = value;
            }
        }

        /// <summary>
        /// ��ҩƷ����Ӧ������
        /// </summary>
        //{25934862-5C82-409c-A0D7-7BE5A24FFC75}
        public List<HISFC.Models.FeeStuff.Output> MateList
        {
            get
            {
                if (mateList == null)
                {
                    mateList = new List<FS.HISFC.Models.FeeStuff.Output>();
                }
                return mateList;
            }
            set
            {
                mateList = value;
            }
        }

        /// <summary>
        /// �Ƿ�Э������
        /// </summary>
        public bool IsNostrum
        {
            get
            {
                return isNostrum;
            }
            set
            {
                isNostrum = value;
            }
        }


        /// <summary>
        /// �Ƿ��ϴ�
        /// </summary>
        public bool IsUpload
        {
            get { return isUpload; }
            set { isUpload = value; }
        }

		#endregion

		#region ����
		
		#region ��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ�����ʵ������</returns>
		public new FeeItemBase Clone()
		{
			FeeItemBase feeItemBase = base.Clone() as FeeItemBase;
			
			feeItemBase.ChargeOper = this.ChargeOper.Clone();
			feeItemBase.ExecOper = this.ExecOper.Clone();
			feeItemBase.FeeOper = this.FeeOper.Clone();
			feeItemBase.FT = this.FT.Clone();
			feeItemBase.Invoice = this.Invoice.Clone();
			feeItemBase.Item = this.Item.Clone();
			feeItemBase.Order = this.Order.Clone();
			feeItemBase.Patient = this.Patient.Clone();
			feeItemBase.RecipeOper = this.RecipeOper.Clone();
			feeItemBase.StockOper = this.StockOper.Clone();
            feeItemBase.UndrugComb = this.UndrugComb.Clone();
			feeItemBase.Compare = this.Compare.Clone();
            if (this.feeCodeStat!=null)
            {
                feeItemBase.FeeCodeStat = this.FeeCodeStat.Clone();
            }
            //{25934862-5C82-409c-A0D7-7BE5A24FFC75}
            List<FS.HISFC.Models.FeeStuff.Output> list = new List<FS.HISFC.Models.FeeStuff.Output>();
            foreach (FS.HISFC.Models.FeeStuff.Output item in this.MateList)
            {
                list.Add(item.Clone());
            }
            feeItemBase.MateList = list;
			return feeItemBase;
		}

		#endregion

		#endregion
	}
}
