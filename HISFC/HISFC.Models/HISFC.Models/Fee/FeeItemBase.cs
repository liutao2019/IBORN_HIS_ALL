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
	/// [�� �� ��: ����]<br></br>
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
        private SIFeeItemBase siFeeItemBase = new SIInterface.SIFeeItemBase(); 
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
		private Patient patient = new Patient();
		
		/// <summary>
		/// ������Ŀ��Ϣ
		/// </summary>
		private Base.Item item = new FS.HISFC.Models.Base.Item();
		
		/// <summary>
		/// �Ƿ�����
		/// </summary>
		private bool isGroup;
		
		/// <summary>
		/// ��Ʊ��Ϣ
		/// </summary>
		private Invoice invoice = new Invoice();

		/// <summary>
		/// ������
		/// </summary>
        private string recipeNO = string.Empty;
		
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
		private Order.Order order = new FS.HISFC.Models.Order.Order();

		/// <summary>
		/// ������Ϣ
		/// </summary>
		private FT ft = new FT();
		
		/// <summary>
		/// ����
		/// </summary>
		private decimal days;
		
		/// <summary>
		/// �շ�ʱ�ĵ�λ 1 ��װ��λ 0 ��С��λ
		/// </summary>
        private string feePack = string.Empty;

		/// <summary>
		/// ��������
		/// </summary>
		private decimal noBackQty;
		
		/// <summary>
		/// ������Դ �շ�Ա����(0) ҽ��(1) �ն�(2) ���(3) 
		/// </summary>
        private string ftSource = string.Empty;
		
		/// <summary>
		/// �����Ŀ��Ϣ
		/// </summary> 
        private Item.UndrugComb undrugComb = new Item.UndrugComb(); 
		
		/// <summary>
		/// ������������(����ҽ��,����ҽ�����ڿ���,ҽ������ʱ��)
		/// </summary>
		private OperEnvironment recipeOper = new OperEnvironment();
		
		/// <summary>
		/// ���۲�������(������,���ۿ���,����ʱ��)
		/// </summary>
		private OperEnvironment chargeOper = new OperEnvironment();
		
		/// <summary>
		/// �շѲ�������(�շ���,�շѿ���,�շ�ʱ��)
		/// </summary>
		private OperEnvironment feeOper = new OperEnvironment();
		
		/// <summary>
		/// �˷Ѳ�������(�˷���,�˷������ڿ���,�˷�ʱ��)
		/// </summary>
		private OperEnvironment cancelOper = new OperEnvironment();
		
		/// <summary>
		/// ִ�в�������(ִ����,ִ�п���, ִ��ʱ��)
		/// </summary>
		private OperEnvironment execOper = new OperEnvironment();
		
		/// <summary>
		/// �ۿ���������(�ۿ����,�ۿ����,�ۿ�ʱ��)
		/// </summary>
		private OperEnvironment stockOper = new OperEnvironment();
		
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
		private OperEnvironment confirmOper = new OperEnvironment();
		
		/// <summary>
		/// ҽ��������Ϣ
		/// </summary>
		private Compare compare = new Compare();
		
		/// <summary>
		/// �����־
		/// </summary>
		private bool isEmergency;

        /// <summary>
        /// ԭ�������ţ�����ǰ��
        /// </summary>
        private string cancelRecipeNO = string.Empty;

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
        private List<FS.HISFC.Models.FeeStuff.Output> mateList = new List<FS.HISFC.Models.FeeStuff.Output>();

        /// <summary>
        /// �Ƿ�Э������
        /// </summary>
        private bool isNostrum = false;


		#endregion

		#region ����
        #region {46381C92-851C-42b6-BCD2-7559A375240F}
        /// <summary>
        /// ҽ�����߻�����Ϣ,������Ϣ
        /// </summary>
        public SIFeeItemBase SIFeeItemBase
        {
            get { return siFeeItemBase; }
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
				return this.invoice;
			}
			set
			{
				this.invoice = value;
			}
		}
		
		/// <summary>
		/// ������
		/// </summary>
		public string RecipeNO
		{
			get
			{
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
				return this.ft;
			}
			set
			{
				this.ft = value;
			}
		}
		
		/// <summary>
		/// ҽ����Ϣ
		/// </summary>
		public Order.Order Order
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
            //{25934862-5C82-409c-A0D7-7BE5A24FFC75}
            List<FS.HISFC.Models.FeeStuff.Output> list = new List<FS.HISFC.Models.FeeStuff.Output>();
            foreach (FS.HISFC.Models.FeeStuff.Output item in this.mateList)
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
