using System;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee.Outpatient
{
	/// <summary>
	/// FeeItemList<br></br>
	/// [��������: ���������ϸ��]<br></br>
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
	public class FeeItemList : FeeItemBase
	{
		
		public FeeItemList()
		{
            this.Patient = new Registration.Register();
		}
		
		#region ����

		/// <summary>
		/// Ժ��ע�����
		/// </summary>
		private int injectCount; 
		
		/// <summary>
		/// ��ȷ��Ժע����
		/// </summary>
		private int confirmedInjectCount; 
		
		/// <summary>
		/// �Ƿ�Ӽ�
		/// </summary>
		private bool isUrgent;
		
		/// <summary>
		/// ��Ʊ��ţ�һ�ν���������ŷ�Ʊ��combNO
		/// </summary>
		private string invoiceCombNO;
		
		/// <summary>
		/// ��Ŀԭʼ����
		/// </summary>
		private decimal orgItemRate;
		
		/// <summary>
		/// �޸ĺ����Ŀ����
		/// </summary>
		private decimal newItemRate;
		
		/// <summary>
		/// �շ�����,ÿ��ҽ������,���ݴ������γɶ����շѴ���
		/// </summary>
		private string recipeSequence;
		
		/// <summary>
		/// ���ⵥ��,Ӧ�ù����޸���Ŀ����
		/// </summary>
		private decimal specialPrice;

        /// <summary>
        /// �����޸��Żݵ���ʱ����ԭʼ����
        /// </summary>
        private decimal orgPrice;

        /// <summary>
        /// ��Ŀ������־ 1 �Է� 2 ���� 3 ����
        /// </summary>
        private string itemRateFlag = string.Empty;

        /// <summary>
        /// ����־ 0�������/1�������/2������� 
        /// </summary>
        private string examineFlag = string.Empty;

        /// <summary>
        /// �Ƿ��Ѿ���ȡ�˻�
        /// </summary>
        private bool isAccounted = false;
        /// <summary>
        /// ҽ�������  {3AEB5613-1CB0-4158-89E6-F82F0B643388}
        /// </summary>
        private FS.FrameWork.Models.NeuObject medicalGroupCode = new FS.FrameWork.Models.NeuObject();
		#endregion
		
		#region ����

        /// <summary>
        /// �Ƿ��Ѿ���ȡ�˻�
        /// </summary>
        public bool IsAccounted 
        {
            get 
            {
                return this.isAccounted;
            }
            set 
            {
                this.isAccounted = value;
            }
        }
		

        /// <summary>
		/// ����־ 0�������/1�������/2������� 
		/// </summary>
		public string ExamineFlag
		{
			get
			{
				return this.examineFlag;
			}
			set
			{
				this.examineFlag = value;
			}
		}

        /// <summary>
        /// ��Ŀ������־ 1 �Է� 2 ���� 3 ����
        /// </summary>
        public string ItemRateFlag 
        {
            get 
            {
                return this.itemRateFlag;
            }
            set 
            {
                this.itemRateFlag = value;
            }
        }

        /// <summary>
        /// �����޸��Żݵ���ʱ����ԭʼ����
        /// </summary>
        public decimal OrgPrice 
        {
            get 
            {
                return this.orgPrice;
            }
            set 
            {
                this.orgPrice = value;
            }
        }
		
		/// <summary>
		/// Ժ��ע�����
		/// </summary>
		public int InjectCount
		{
			get
			{
				return this.injectCount;
			}
			set
			{
				this.injectCount = value;
			}
		}

		/// <summary>
		/// ��ȷ��Ժע����
		/// </summary>
		public int ConfirmedInjectCount
		{
			get
			{
				return this.confirmedInjectCount;
			}
			set
			{
				this.confirmedInjectCount = value;
			}
		}

		/// <summary>
		/// ��Ŀ�Ƿ�Ӽ� true �� false ����
		/// </summary>
		public bool IsUrgent
		{
			get
			{
				return this.isUrgent;
			}
			set
			{
				this.isUrgent = value;
			}
		}
		
		/// <summary>
		/// ��Ʊ��ţ�һ�ν���������ŷ�Ʊ��combNO
		/// </summary>
		public string InvoiceCombNO
		{
			get
			{
				return this.invoiceCombNO;
			}
			set
			{
				this.invoiceCombNO = value;
			}
		}

		/// <summary>
		/// ��Ŀԭʼ����
		/// </summary>
		public decimal OrgItemRate
		{
			get
			{
				return this.orgItemRate;
			}
			set
			{
				this.orgItemRate = value;
			}
		}

		/// <summary>
		/// �޸ĺ����Ŀ����
		/// </summary>
		public decimal NewItemRate
		{
			get
			{
				return this.newItemRate;
			}
			set
			{
				this.newItemRate = value;
			}
		}

		/// <summary>
		/// �շ�����,ÿ��ҽ������,���ݴ������γɶ����շѴ���
		/// </summary>
		public string RecipeSequence
		{
			get
			{
				return this.recipeSequence;
			}
			set
			{
				this.recipeSequence = value;
			}
		}

		/// <summary>
		/// ���ⵥ��,Ӧ�ù����޸���Ŀ����
		/// </summary>
		public decimal SpecialPrice
		{
			get
			{
				return this.specialPrice;
			}
			set
			{
				this.specialPrice = value;
			}
		}
        /// <summary>
        /// ҽ�������{3AEB5613-1CB0-4158-89E6-F82F0B643388}
        /// </summary>
        public FS.FrameWork.Models.NeuObject MedicalGroupCode
        {
            get { return medicalGroupCode; }
            set { medicalGroupCode = value; }
        }
		#endregion

		#region ����
		
		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ����ʵ��</returns>
		public new FeeItemList Clone()
		{
			return base.Clone() as FeeItemList;
		} 

		#endregion

		#endregion
		
		#region ���ñ�������

		/// <summary>
		/// ҩƷ������
		/// </summary>
		private decimal excessCost;
		
		/// <summary>
		/// �Է�ҩ���
		/// </summary>
		private decimal drugOwnCost;

		/// <summary>
		/// ҩƷ������
		/// </summary>
        [Obsolete("����,ʹ��FT.ExcessCost", true)]
		public decimal ExcessCost
		{
			set
			{
				this.excessCost = value;
			}
			get
			{
				return this.excessCost;
			}
		}

		/// <summary>
		/// �Է�ҩ���
		/// </summary>
        [Obsolete("����,FT.DrugOwnCost", true)]
		public decimal DrugOwnCost
		{
			set
			{
				drugOwnCost = value;
			}
			get
			{
				return drugOwnCost;
			}
		}

		private string costSource;//������Դ
		/// <summary>
		/// ������Դ0 �շ�Ա���� 1 ҽ�� 2 �ն� 3 ��� 
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��FTSource", true)]
		public string CostSource
		{
			set
			{
				costSource = value;
			}
			get
			{
				return costSource;
			}
		}
		private string subJobFlag;//���ı�־
		/// <summary>
		/// 1�Ǹ���0����
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��IsMaterial", true)]
		public string SubJobFlag
		{
			set
			{
				subJobFlag = value;
			}
			get
			{
				return subJobFlag;
			}
		}
		
		private string recipeSeq;//�շ�����
		/// <summary>
		/// �շ�����
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��RecipeSequence", true)]
		public string RecipeSeq
		{
			get
			{
				return recipeSeq;
			}
			set
			{
				recipeSeq = value;
			}
		}
		private decimal spPrice;//���ⵥ��
		/// <summary>
		/// ���ⵥ��
		/// </summary>
		[Obsolete("����,ʹ��SpecialPrice", true)]
		public decimal SpPrice
		{
			set
			{
				spPrice = value;
			}
			get
			{
				return spPrice;
			}
		}
		/// <summary>
		/// ������Ϣ
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��Compare", true)]
		public FS.HISFC.Models.SIInterface.Compare CompareItem = new FS.HISFC.Models.SIInterface.Compare();
		private string extFlag; //��չ��־,�����޸ı��������Ŀ��� 1 �Է� 2 ���� 3 ����
		/// <summary>
		/// ��չ��־,�����޸ı��������Ŀ��� 1 �Է� 2 ���� 3 ����
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��SpecialFlag", true)]
		public string ExtFlag
		{
			get
			{
				return extFlag;
			}
			set
			{
				extFlag = value;
			}
		}
		private string extFlag1;//��չ��־1
		/// <summary>
		/// ��չ��־1
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��SpecialFlag1", true)]
		public string ExtFlag1
		{
			get
			{
				return extFlag1;
			}
			set
			{
				extFlag1 = value;
			}
		}
		private string extFlag2;//��չ��־2
		/// <summary>
		/// ��չ��־2
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��SpecialFlag2", true)]
		public string ExtFlag2
		{
			get
			{
				return extFlag2;
			}
			set
			{
				extFlag2 = value;
			}
		}
		private string extFlag3;//��չ��־3
		/// <summary>
		/// ��չ��־3
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��SpecialFlag3", true)]
		public string ExtFlag3
		{
			get
			{
				return extFlag3;
			}
			set
			{
				extFlag3 = value;
			}
		}
        //
        //private FS.HISFC.Models.Fee.Item.UndrugComb package = new FS.HISFC.Models.Fee.Item.UndrugComb();
		/// <summary>
		/// ������Ŀ��Ϣ
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��UndrugComb", true)]
		public FS.HISFC.Models.Fee.Item.UndrugComb Package
		{
			get
			{
				return null;
			}
            //set
            //{
            //    package = value;
            //}
		}
		private decimal noBackNum;//��������
		/// <summary>
		/// ��������
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��NoBackQty", true)]
		public decimal NoBackNum
		{
			get
			{
				return noBackNum;
			}
			set
			{
				noBackNum = value;
			}
		}

		private decimal confirmNum;//ȷ������
		/// <summary>
		/// ȷ������
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��ConfirmedQty", true)]
		public decimal ConfirmNum
		{
			get
			{
				return confirmNum;
			}
			set
			{
				confirmNum = value;
			}
		}
		private string moOrder;//ҽ�����������Ŀ��ˮ��
		/// <summary>
		/// ҽ�����������Ŀ��ˮ��
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��Order", true)]
		public string MoOrder
		{
			get
			{
				return moOrder;
			}
			set
			{
				moOrder = value;
			}
		}

		private string invoiceSeq;//��Ʊ��ţ�һ�ν���������ŷ�Ʊ��combNo
		/// <summary>
		/// ��Ʊ��ţ�һ�ν���������ŷ�Ʊ��combNo
		/// </summary>
		[Obsolete("����,�Ѿ�ʹ��InvoiceCombNO", true)]
		public string InvoiceSeq
		{
			get
			{
				return invoiceSeq;
			}
			set
			{
				invoiceSeq = value;
			}
		}
		private string needBespeak;//��Ŀ�Ƿ���ҪԤԼ 1��Ҫ
		/// <summary>
		/// ��Ŀ�Ƿ���ҪԤԼ 1��Ҫ
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��IsNeedBespeak", true)]
		public string NeedBespeak
		{
			get
			{
				return needBespeak;
			}
			set
			{
				needBespeak = value;
			}
		}

		private int seqNo; //��Ŀ�ڴ�����
		/// <summary>
		/// ��Ŀ�ڴ�����
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��SequenceNO", true)]
		public int SeqNo
		{
			get
			{
				return seqNo;
			}
			set
			{
				seqNo = value;
			}
		}
		private string clinicCode;//������ˮ��
		/// <summary>
		/// ������ˮ��
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��ID", true)]
		public string ClinicCode
		{
			get
			{
				return clinicCode;
			}
			set
			{
				clinicCode = value;
			}
		}
		private string cardNo; //���￨��
		/// <summary>
		/// ���￨��
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��Patient.PID.CardNO", true)]
		public string CardNo
		{
			get
			{
				return cardNo;
			}
			set
			{
				cardNo = value;
			}
		}

		private DateTime regDate; ///�Һ�����
		[Obsolete("����,Register", true)]
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

		private FS.FrameWork.Models.NeuObject regDeptInfo = new FS.FrameWork.Models.NeuObject(); //�Һſ�����Ϣ
		/// <summary>
		/// �Һſ�����Ϣ id ���ұ��� Name ��������
		/// </summary>
		[Obsolete("����,Registerת��Ϊ", true)]
		public FS.FrameWork.Models.NeuObject RegDeptInfo
		{
			get
			{
				return regDeptInfo;
			}
			set
			{
				regDeptInfo = value;
			}
		}

		private FS.FrameWork.Models.NeuObject doctInfo = new FS.FrameWork.Models.NeuObject(); //����ҽʦ��Ϣ
		/// <summary>
		/// ����ҽʦ��Ϣ ID Ա������ Name Ա������
		/// </summary>
		[Obsolete("���� ʹ��RecipeOper.ID(Name)", true)]
		public FS.FrameWork.Models.NeuObject DoctInfo
		{
			get
			{
				return doctInfo;
			}
			set
			{
				doctInfo = value;
			}
		}
		
		private FS.FrameWork.Models.NeuObject doctDeptInfo = new FS.FrameWork.Models.NeuObject(); //����ҽʦ���ڿ�����Ϣ
		/// <summary>
		/// ����ҽʦ���ڿ�����Ϣ ID ���ұ��� Name ��������
		/// </summary>
		[Obsolete("����,ʹ��RecipeOper.Dept", true)]
		public FS.FrameWork.Models.NeuObject DoctDeptInfo
		{
			get
			{
				return doctDeptInfo;
			}
			set
			{
				doctDeptInfo = value;
			}
		}
		
		private bool isSelfMade;//������Ŀ��־
		/// <summary>
		/// ������Ŀ��־ true �� false ����
		/// </summary>
		[Obsolete("����,Itemת��ΪPharmarcy.Item��ȡ����", true)]
		public bool IsSelfMade
		{
			get
			{
				return isSelfMade;
			}
			set
			{
				isSelfMade = value;
			}
		}

		private FS.FrameWork.Models.NeuObject drugQualityInfo = new FS.FrameWork.Models.NeuObject(); //ҩƷ������Ϣ
		/// <summary>
		/// ҩƷ������Ϣ ID ����  Name ����
		/// </summary>
		[Obsolete("����", true)]
		public FS.FrameWork.Models.NeuObject DrugQualityInfo
		{
			get
			{
				return drugQualityInfo;
			}
			set
			{
				drugQualityInfo = value;
			}
		}

		private FS.FrameWork.Models.NeuObject doseInfo = new FS.FrameWork.Models.NeuObject(); //������Ϣ
		/// <summary>
		/// ������Ϣ ID ���ʹ��� Name ��������
		/// </summary>
		[Obsolete("����", true)]
		public FS.FrameWork.Models.NeuObject DoseInfo
		{
			get
			{	
				return doseInfo;
			}
			set
			{
				doseInfo = value;
			}
		}

		private FS.FrameWork.Models.NeuObject freqInfo = new FS.FrameWork.Models.NeuObject(); //Ƶ����Ϣ
		/// <summary>
		/// Ƶ����Ϣ ID Ƶ�δ��� Name Ƶ������
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��Order.Frenquence", true)]
		public FS.FrameWork.Models.NeuObject FreqInfo
		{
			get
			{
				return freqInfo;
			}
			set
			{
				freqInfo = value;
			}
		}
		
		private FS.FrameWork.Models.NeuObject usageInfo = new FS.FrameWork.Models.NeuObject(); //�÷���Ϣ
		/// <summary>
		/// �÷���Ϣ ID �÷����� Name �÷�����
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��Order.Usage", true)]
		public FS.FrameWork.Models.NeuObject UsageInfo
		{
			get
			{
				return usageInfo;
			}
			set
			{
				usageInfo = value;
			}
		}

		private FS.FrameWork.Models.NeuObject labTypeInfo = new FS.FrameWork.Models.NeuObject();//��������
		/// <summary>
		/// ����������Ϣ ID �������ʹ��� Name ������������
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��Order.Sample", true)]
		public FS.FrameWork.Models.NeuObject LabTypeInfo
		{
			get
			{
				return labTypeInfo;
			}
			set
			{
				labTypeInfo = value;
			}
		}

		private string checkBody; //����
		/// <summary>
		/// ����
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��Order.Sample", true)]
		public string CheckBody
		{
			get
			{
				return checkBody;
			}
			set
			{
				checkBody = value;
			}
		}

		private decimal doseOnce; //ÿ������
		/// <summary>
		/// ÿ������
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��Order.DoseOnce", true)]
		public decimal DoseOnce
		{
			get
			{
				return doseOnce;
			}
			set
			{
				doseOnce = value;
			}
		}
		
		private string doseUnit; //ÿ��������λ
		/// <summary>
		/// ÿ��������λ
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��Order.DoseUnit", true)]
		public string DoseUnit
		{
			get
			{
				return doseUnit;
			}
			set
			{
				doseUnit = value;
			}
		}

		private decimal baseDose; //��������
		/// <summary>
		/// ��������
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��Itemת��ΪPharmacy.Item��BaseDose����", true)]
		public decimal BaseDose
		{
			get
			{
				return baseDose;
			}
			set
			{
				baseDose = value;
			}
		}

		private FT cost = new FT(); //������Ϣ;
		/// <summary>
		/// ������Ϣ
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��FT", true)]
		public FT Cost
		{
			get
			{
				return cost;
			}
			set
			{
				cost = value;
			}
		}

		private FS.FrameWork.Models.NeuObject exeDeptInfo = new FS.FrameWork.Models.NeuObject();//ִ�п�����Ϣ
		/// <summary>
		/// ִ�п�����Ϣ ID ���Ҵ��� Name ��������
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��ExecOper.Dept", true)]
		public FS.FrameWork.Models.NeuObject ExeDeptInfo
		{
			get
			{
				return exeDeptInfo;
			}
			set
			{
				exeDeptInfo = value;
			}
		}

		private FS.HISFC.Models.SIInterface.Item centerInfo = new FS.HISFC.Models.SIInterface.Item();//ҽ��������Ϣ

		/// <summary>
		/// ҽ��������Ϣ �������ı��룬 ��Ŀ���(�ף��ң� �Է�)
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��Compare", true)]
		public FS.HISFC.Models.SIInterface.Item CenterInfo 
		{
			get
			{
				return centerInfo;
			}
			set
			{
				centerInfo = value;
			}
		}
		
		private bool isMainDrug; //�Ƿ���ҩ
		/// <summary>
		/// �Ƿ���ҩ true �� false ����
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��Order", true)]
		public bool IsMainDrug
		{
			get
			{
				return isMainDrug;
			}
			set
			{
				isMainDrug = value;
			}
		}

		private string combNo;//��Ϻ�
		/// <summary>
		/// ��Ϻ�
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��Order.Combo.ID", true)]
		public string CombNo
		{
			get
			{
				return combNo;
			}
			set
			{
				combNo = value;
			}
		}

		private FS.FrameWork.Models.NeuObject chargeOperInfo = new FS.FrameWork.Models.NeuObject();//��������Ϣ
		/// <summary>
		/// ��������Ϣ ID �����˴��� Name ����
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��ChargeOper.ID(Name)", true)]
		public FS.FrameWork.Models.NeuObject ChargeOperInfo
		{
			get
			{
				return chargeOperInfo;
			}
			set
			{
				chargeOperInfo = value;
			}
		}

		private DateTime chargeDate;//��������
		/// <summary>
		/// ��������
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��ChargeOper.OperTime", true)]
		public DateTime ChargeDate
		{
			get
			{
				return chargeDate;
			}
			set
			{
				chargeDate = value;
			}
		}


		private FS.FrameWork.Models.NeuObject feeOperInfo = new FS.FrameWork.Models.NeuObject();//�շ�����Ϣ
		/// <summary>
		/// �շ�����Ϣ ID �շ��˴��� Name ����
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��FeeOper.ID(Name)", true)]
		public FS.FrameWork.Models.NeuObject FeeOperInfo
		{
			get
			{
				return feeOperInfo;
			}
			set
			{
				feeOperInfo = value;
			}
		}

		private DateTime feeDate;//�շ�����
		/// <summary>
		/// �շ�����
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��FeeOper.OperTime", true)]
		public DateTime FeeDate
		{
			get
			{
				return feeDate;
			}
			set
			{
				feeDate = value;
			}
		}
		private string invoiceNo;//��Ʊ��
		/// <summary>
		/// ��Ʊ��
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��Invoice.ID", true)]
		public string InvoiceNo
		{
			get
			{
				return invoiceNo;
			}
			set
			{
				invoiceNo = value;
			}
		}

		private FS.FrameWork.Models.NeuObject invoiceTypeInfo = new FS.FrameWork.Models.NeuObject();//��Ʊ��Ŀ��Ϣ
		/// <summary>
		/// ��Ʊ��Ŀ��Ϣ  ID ��Ŀ���� Name ��Ŀ����
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��Invoice", true)]
		public FS.FrameWork.Models.NeuObject InvoiceTypeInfo
		{
			get
			{
				return invoiceTypeInfo;
			}
			set
			{
				invoiceTypeInfo = value;
			}
		}

		private int invoiceSeqNo;//��Ʊ����ˮ��
		/// <summary>
		/// ��Ʊ����ˮ��
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��Invoice", true)]
		public int InvoiceSeqNo
		{
			get
			{
				return invoiceSeqNo;
			}
			set
			{
				invoiceSeqNo = value;
			}
		}
		
		private bool isConfirm; //�Ƿ��ն�ȷ��
		/// <summary>
		/// �Ƿ��ն�ȷ�� true ȷ�� false û��ȷ��
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��IsConfirmed", true)]
		public bool IsConfirm
		{
			get
			{
				return isConfirm;
			}
			set
			{
				isConfirm = value;
			}
		}
		
		private FS.FrameWork.Models.NeuObject confirmOperInfo = new FS.FrameWork.Models.NeuObject();//ȷ������Ϣ
		/// <summary>
		/// ȷ������Ϣ ID ȷ���˴��� Name ����
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��ConfirmOper.ID(Name)", true)]
		public FS.FrameWork.Models.NeuObject ConfirmOperInfo
		{
			get
			{
				return confirmOperInfo;
			}
			set
			{
				confirmOperInfo = value;
			}
		}

		private FS.FrameWork.Models.NeuObject confirmDeptInfo = new FS.FrameWork.Models.NeuObject();//ȷ�Ͽ�����Ϣ
		/// <summary>
		/// ȷ�Ͽ�����Ϣ ID ȷ�Ͽ��Ҵ��� Name ��������
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��ConfirmOper.Dept", true)]
		public FS.FrameWork.Models.NeuObject ConfirmDeptInfo
		{
			get
			{
				return confirmDeptInfo;
			}
			set
			{
				confirmDeptInfo = value;
			}
		}

		private DateTime confirmDate;//ȷ������
		/// <summary>
		/// ȷ������
		/// </summary>
		[Obsolete("����,�Ѿ��̳�ʹ��ConfirmOper.OperTime", true)]
		public DateTime ConfirmDate
		{
			get
			{
				return confirmDate;
			}
			set
			{
				confirmDate = value;
			}
		}

		#endregion
		
	}
}
