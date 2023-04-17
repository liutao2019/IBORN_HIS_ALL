using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.SIInterface;
using FS.HISFC.Models.RADT;
using System.Collections.Generic;
using FS.HISFC.Models.Fee.Item;
using FS.HISFC.Models.Fee;
//using FS.HISFC.Models.Fee;

namespace FS.HISFC.Models.SIInterface 
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
    public  class SIFeeItemBase 
	{
		#region ����
		
        /// <summary>
        /// �������� TransTypes.Positive ������(1), TransTypes.Negative ������(2)
        /// </summary>
        private TransTypes transType;

        /// <summary>
        /// ������ˮ��
        /// </summary>
        private string transSequenceNO;
      

   
        /// <summary>
        ///  ������Ϣ ��Ч:Valid(0),�˷�����:Canceled(1) �ش�:Reprint(2) ע��:LogOut(3)
        /// </summary>
        private CancelTypes cancelType;
		
        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        private Patient patient;
		
        /// <summary>
        /// ������Ŀ��Ϣ
        /// </summary>
        private Base.Item item;


        /// <summary>
        /// �Ƿ��ϴ�
        /// </summary>
        private bool isUpload;
		
        /// <summary>
        /// ��Ʊ��Ϣ
        /// </summary>
        private FS.HISFC.Models.Fee.Invoice invoice;

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
        private Order.Order order;

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
        private UndrugComb undrugComb;
		
        /// <summary>
        /// ������������(����ҽ��,����ҽ�����ڿ���,ҽ������ʱ��)
        /// </summary>
        private OperEnvironment recipeOper;
		
        /// <summary>
        /// ���۲�������(������,���ۿ���,����ʱ��)
        /// </summary>
        private OperEnvironment chargeOper;
		
        /// <summary>
        /// �շѲ�������(�շ���,�շѿ���,�շ�ʱ��)
        /// </summary>
        private OperEnvironment feeOper;
		
        /// <summary>
        /// �˷Ѳ�������(�˷���,�˷������ڿ���,�˷�ʱ��)
        /// </summary>
        private OperEnvironment cancelOper;

         /// <summary>
         /// ִ�в�������(ִ����,ִ�п���, ִ��ʱ��)
         /// </summary>       
        private OperEnvironment execOper;
		
        /// <summary>
        /// �ۿ���������(�ۿ����,�ۿ����,�ۿ�ʱ��)
        /// </summary>
        private OperEnvironment stockOper;
		
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
        private OperEnvironment confirmOper;
		
        /// <summary>
        /// ҽ��������Ϣ
        /// </summary>
        private Compare compare;
		
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
        private List<FS.HISFC.Models.FeeStuff.Output> mateList;
        /// <summary>
        /// �Ƿ�Э������
        /// </summary>
        private bool isNostrum = false;


		#endregion

		#region ����
        /// <summary>
        /// ������ˮ��
        /// </summary>
        public string  TransSequenceNO
        {
            get { return transSequenceNO; }
            set { transSequenceNO = value; }
        }
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
                if (patient == null)
                {
                    patient = new Patient();
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
                    item = new FS.HISFC.Models.Base.Item();
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
         public bool IsUpload
        {
            get
            {
                return isUpload;
            }
            set
            {
                isUpload = value;
            }
        }
		
        /// <summary>
        /// ��Ʊ��Ϣ
        /// </summary>
        public FS.HISFC.Models.Fee.Invoice Invoice
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
                if (order == null)
                {
                    order = new FS.HISFC.Models.Order.Order();
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
        public UndrugComb UndrugComb
        { 
            get
            {
                if (undrugComb == null)
                {
                    undrugComb = new UndrugComb();
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
         /// ִ�в�������(ִ����,ִ�п���, ִ��ʱ��)
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

		#endregion

		#region ����
		
		#region ��¡
        
		 ///<summary>
		 ///��¡
		 ///</summary>
		 ///<returns>���ص�ǰ�����ʵ������</returns>
        public new SIFeeItemBase Clone()
		{
            SIFeeItemBase sifeeItemBase = new SIFeeItemBase();

            sifeeItemBase.ChargeOper = this.ChargeOper.Clone();
            sifeeItemBase.ExecOper = this.ExecOper.Clone();
            sifeeItemBase.FeeOper = this.FeeOper.Clone();
            sifeeItemBase.FT = this.FT.Clone();
            sifeeItemBase.Invoice = this.Invoice.Clone();
            sifeeItemBase.Item = this.Item.Clone();
            sifeeItemBase.Order = this.Order.Clone();
            sifeeItemBase.Patient = this.Patient.Clone();
            sifeeItemBase.RecipeOper = this.RecipeOper.Clone();
            sifeeItemBase.StockOper = this.StockOper.Clone();
            sifeeItemBase.UndrugComb = this.UndrugComb.Clone();
            sifeeItemBase.Compare = this.Compare.Clone();
            //{25934862-5C82-409c-A0D7-7BE5A24FFC75}
            List<FS.HISFC.Models.FeeStuff.Output> list = new List<FS.HISFC.Models.FeeStuff.Output>();
            foreach (FS.HISFC.Models.FeeStuff.Output item in this.MateList)
            {
                list.Add(item.Clone());
            }
            sifeeItemBase.MateList = list;
            return sifeeItemBase;
		}

		#endregion

		#endregion
	}
}
