using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Order;
using System.Collections.Generic;
using FS.HISFC.Models.RADT;

namespace FS.HISFC.Models.Fee.Inpatient
{


	/// <summary>
	///������Ŀ��Ϣ��
	///IDסԺ��ˮ��
	///name ��������
	/// </summary>
    /// 

    [System.Serializable]
	public class FeeItemList : FeeItemBase, IBaby
	{
		/// <summary>
		/// FeeItemBase<br></br>
		/// [��������: סԺ������ϸ��]<br></br>
		/// [�� �� ��: ��˹]<br></br>
		/// [����ʱ��: 2006-09-13]<br></br>
		/// <�޸ļ�¼ 
		///		�޸���='' 
		///		�޸�ʱ��='yyyy-mm-dd' 
		///		�޸�Ŀ��=''
		///		�޸�����=''
		///  />
		/// </summary>
		public FeeItemList()
		{
            this.Patient = new FS.HISFC.Models.RADT.PatientInfo();
            this.Order = new FS.HISFC.Models.Order.Inpatient.Order();
		}
		
		#region ����

        /// <summary>
        /// Ժ��id
        /// </summary>
        private string hospital_id;

        /// <summary>
        /// Ժ����
        /// </summary>
        private string hospital_name;


		/// <summary>
		/// �������
		/// </summary>
		private int balanceNO;
		
		/// <summary>
		/// ����״̬
		/// </summary>
        private string balanceState;

		/// <summary>
		/// Ӥ�����
		/// </summary>
        private string babyNO;
		
		/// <summary>
		/// �Ƿ�Ӥ��
		/// </summary>
		private bool isBaby;
		
		/// <summary>
		/// ���ⵥ��
		/// </summary>
		private int sendSequence;


		/// <summary>
		/// �豸���
		/// </summary>
        private string machineNO;
		
		/// <summary>
		/// ���ñ���(����Ϊ������������)
		/// </summary>
		private FTRate ftRate;

		/// <summary>
		/// ������
		/// </summary>
		private string auditingNO;//string.Empty;
		
		/// <summary>
		/// �����������(�����ˣ�����ʱ�䣬���������ڿ��ң�
		/// </summary>
		private OperEnvironment balanceOper;// new OperEnvironment();
		
		/// <summary>
		/// ҽ��ִ�е�
		/// </summary>
		private ExecOrder execOrder;// new ExecOrder();
		
		/// <summary>
		/// ��չ��־
		/// </summary>
        private string extFlag;

        /// <summary>
        /// ��չ��־1
        /// </summary>
        private string extFlag1;// string.Empty;

        /// <summary>
        /// ��չ��־2
        /// </summary>
        private string extFlag2;// string.Empty;

        /// <summary>
        /// ��չ����
        /// </summary>
        private string extCode;// string.Empty;

        /// <summary>
        /// ��չ��������(��չ��,��չʱ��,��չ�����ڿ���)
        /// </summary>
        private OperEnvironment extOper;

		/// <summary>
		/// ������
		/// </summary>
		private string approveNO;// string.Empty;

        /// <summary>
        /// �Ƿ���Ҫ���¿������� true ��Ҫ false ����Ҫ
        /// </summary>
        private bool isNeedUpdateNoBackQty;

        //{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E} ����ҽ���鴦��
        /// <summary>
        /// ҽ����
        /// </summary>
        private FS.FrameWork.Models.NeuObject medicalTeam;

        /// <summary>
        /// ��������{0604764A-3F55-428f-9064-FB4C53FD8136}
        /// </summary>
        private string operationNO ;// string.Empty;

        /// <summary>
        /// ������Դ����д��
        /// </summary>
        private FTSource ftSource ;

        /// <summary>
        /// ��ֱ��
        /// </summary>
        private bool splitFlag = false;
        /// <summary>
        /// ���ID
        /// </summary>
        private string splitID = string.Empty;

        /// <summary>
        /// ��ֺ� ���շѱ��
        /// </summary>
        private bool splitFeeFlag = false;

        //{18AD984D-78C6-4c64-8066-6F31E3BDF7DA}
        /// <summary>
        /// �ײͱ��
        /// </summary>
        private string packageFlag = "0";

        /// <summary>
        /// �Ƿ��ۿ�{c89524f7-3f9e-4a41-a2a6-7cccbf476404}
        /// </summary>
        private bool isDiscount = false;

		#endregion

		#region ����

        /// <summary>
        ///Ժ��id
        /// </summary>
        public string Hospital_id
        {
            get
            {
                return this.hospital_id;
            }
            set
            {
                this.hospital_id = value;
            }
        }


        /// <summary>
        /// Ժ����
        /// </summary>
        public string Hospital_name
        {
            get
            {
                return this.hospital_name;
            }
            set
            {
                this.hospital_name = value;
            }
        }

        /// <summary>
        /// ҽ����Ϣ
        /// </summary>
        public new Order.Inpatient.Order Order
        {
            get
            {
                if (base.Order == null)
                {
                    base.Order = new Order.Inpatient.Order();
                }
                return base.Order as Order.Inpatient.Order;
            }
            set
            {
                base.Order = value;
            }
        }

		/// <summary>
		/// �������
		/// </summary>
		public int BalanceNO
		{
			get
			{
				return this.balanceNO;
			}
			set
			{
				this.balanceNO = value;
			}
		}
		
		/// <summary>
		/// ����״̬
		/// </summary>
		public string BalanceState
		{
			get
			{
                if (balanceState == null)
                {
                    balanceState = string.Empty;
                }
				return this.balanceState;
			}
			set
			{
				this.balanceState = value;
			}
		}

		/// <summary>
		/// ���ⵥ��
		/// </summary>
		public int SendSequence
		{
			get
			{
				return this.sendSequence;
			}
			set
			{
				this.sendSequence = value;
			}
		}

		/// <summary>
		/// �豸���
		/// </summary>
		public string MachineNO
		{
			get
			{
                if (machineNO == null)
                {
                    machineNO = string.Empty;
                }
				return this.machineNO;
			}
			set
			{
				this.machineNO = value;
			}
		}

		/// <summary>
		/// ���ñ���(����Ϊ������������)
		/// </summary>
		public FTRate FTRate
		{
			get
			{
                if (ftRate == null)
                {
                    ftRate = new FTRate();
                }
				return this.ftRate;
			}
			set
			{
				this.ftRate = value;
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public string AuditingNO
		{
			get
			{
                if (auditingNO == null)
                {
                    auditingNO = string.Empty;
                }
				return this.auditingNO;
			}
			set
			{
				this.auditingNO = value;
			}
		}

		/// <summary>
		/// �����������(�����ˣ�����ʱ�䣬���������ڿ��ң�
		/// </summary>
		public OperEnvironment BalanceOper
		{
			get
			{
                if (this.balanceOper == null)
                {
                    this.balanceOper = new OperEnvironment();
                }
				return this.balanceOper;
			}
			set
			{
				this.balanceOper = value;
			}
		}

		/// <summary>
		/// ��չ��־
		/// </summary>
		public string ExtFlag
		{
			get
			{
                if (extFlag == null)
                {
                    extFlag = string.Empty;
                }
				return this.extFlag;	
			}
			set
			{
				this.extFlag = value;
			}
		}

        /// <summary>
        /// ��չ��־1
        /// </summary>
        public string ExtFlag1
        {
            get
            {
                if (extFlag1 == null)
                {
                    extFlag1 = string.Empty;
                }
                return this.extFlag1;
            }
            set
            {
                this.extFlag1 = value;
            }
        }

        /// <summary>
        /// ��չ��־2
        /// </summary>
        public string ExtFlag2
        {
            get
            {
                if (extFlag2 == null)
                {
                    extFlag2 = string.Empty;
                }
                return this.extFlag2;
            }
            set
            {
                this.extFlag2 = value;
            }
        }

        /// <summary>
        /// ��չ����
        /// </summary>
        public string ExtCode
        {
            get
            {
                if (extCode == null)
                {
                    extCode = string.Empty;
                }
                return this.extCode;
            }
            set
            {
                this.extCode = value;
            }
        }

        /// <summary>
        ///  ��չ��������(��չ��,��չʱ��,��չ�����ڿ���)
        /// </summary>
        public OperEnvironment ExtOper
        {
            get
            {
                if (this.extOper == null)
                {
                    this.extOper = new OperEnvironment();
                }
                return this.extOper;
            }
            set
            {
                this.extOper = value;
            }
        }

		/// <summary>
		/// ҽ��ִ�е�
		/// </summary>
		public ExecOrder ExecOrder 
		{
			get
			{
                if (execOrder == null)
                {
                    execOrder = new ExecOrder();
                }
				return this.execOrder;
			}
			set
			{
				this.execOrder = value;
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public string ApproveNO
		{
			get
			{
                if (approveNO == null)
                {
                    approveNO = string.Empty;
                }
				return this.approveNO;
			}
			set
			{
				this.approveNO = value;
			}
		}

        /// <summary>
        /// �Ƿ���Ҫ���¿������� true ��Ҫ false ����Ҫ
        /// </summary>
        public bool IsNeedUpdateNoBackQty 
        {
            get 
            {
                return this.isNeedUpdateNoBackQty;
            }
            set 
            {
                this.isNeedUpdateNoBackQty = value;
            }
        }

        /// <summary>
        /// ������Դ
        /// </summary>
        public new FTSource FTSource
        {
            get
            {
                if (ftSource == null)
                {
                    ftSource = new FTSource();
                }
                return ftSource;
            }
            set
            {
                ftSource = value;
            }
        }


        //{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E} ����ҽ���鴦��
        /// <summary>
        /// ҽ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject MedicalTeam
        {
            get {
                if (medicalTeam == null)
                {
                    medicalTeam = new NeuObject();
                }
                return medicalTeam; }
            set { medicalTeam = value; }
        }

        /// <summary>
        /// ��������{0604764A-3F55-428f-9064-FB4C53FD8136}
        /// </summary>
        public string OperationNO
        {
            get {
                if (operationNO == null)
                {
                    operationNO = string.Empty;
                }
                return operationNO; }
            set { operationNO = value; }
        }

        /// <summary>
        /// ��ֱ��
        /// </summary>
        public bool SplitFlag
        {
            get
            {
                return splitFlag;
            }
            set
            {
                splitFlag = value;
            }
        }

        /// <summary>
        /// ���ID
        /// </summary>
        public string SplitID
        {
            get
            {
                return splitID;
            }
            set
            {
                splitID = value;
            }
        }

        /// <summary>
        /// ��ֺ�  ���շѱ��
        /// </summary>
        public bool SplitFeeFlag
        {
            get
            {
                return splitFeeFlag;
            }
            set
            {
                splitFeeFlag = value;
            }
        }


         /// <summary>
        /// �Ƿ��ۿ۱��{c89524f7-3f9e-4a41-a2a6-7cccbf476404}
        /// </summary>
        public bool IsDiscount
        {
            get
            {
                return isDiscount;
            }
            set
            {
                isDiscount = value;
            }
        }


        /// <summary>
        /// �ײͱ��
        /// //{18AD984D-78C6-4c64-8066-6F31E3BDF7DA}
        /// </summary>
        public string PackageFlag
        {
            get
            {
                return packageFlag;
            }
            set
            {
                packageFlag = value;
            }
        }

		#endregion

		#region ����

		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ����ʵ������</returns>
		public new FeeItemList Clone()
		{
			FeeItemList feeItemList = base.Clone() as FeeItemList;

			feeItemList.FTRate = this.FTRate.Clone();
            if (this.balanceOper != null)
            {
                feeItemList.balanceOper = this.BalanceOper.Clone();
            }
			feeItemList.ExecOrder = this.ExecOrder.Clone();
            //{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E} ����ҽ���鴦��
            feeItemList.MedicalTeam = this.MedicalTeam.Clone();
            feeItemList.ftSource = this.FTSource.Clone();
			return feeItemList;
		}

		#endregion

		#endregion 

		#region �ӿ�ʵ��
		
		#region IBaby ��Ա
		
		/// <summary>
		/// Ӥ�����
		/// </summary>
		public string BabyNO
		{
			get
			{
                if (babyNO == null)
                {
                    babyNO = string.Empty;
                }
				return this.babyNO;
			}
			set
			{
				this.babyNO = value;
			}
		}
		
		/// <summary>
		/// �Ƿ�Ӥ��
		/// </summary>
		public bool IsBaby
		{
			get
			{
				return this.isBaby;
			}
			set
			{
				this.isBaby = value;
			}
		}

		#endregion

		#endregion

		#region ���÷�������

		//[Obsolete("����", true)]
		//public FeeInfo FeeInfo=new FeeInfo();

		/// <summary>
		/// ҽ����ˮ��
		/// </summary>
		[Obsolete("����,����Order.ID����", true)]
		public string MoOrder
		{
			get
			{	
				return "";
			}
			set
			{
				
			}
		}
		
		/// <summary>
		/// �����������Ϣ
		/// </summary>
        [Obsolete("����,����UndrugComb����", true)]
        public NeuObject ItemGroup
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
		/// 0����1�շ�2ִ�з���
		/// </summary>
		[Obsolete("����,����PayType����", true)]
		public string SendState;

		/// <summary>
		/// �Ƿ��Ժ����(��Ϊҽ������)
		/// </summary>
		[Obsolete("����,����Order����", true)]
		public string IsBrought;
		
		[Obsolete("����,����NoBackQty����", true)]
		public decimal NoBackNum=0m;
		/// <summary>
		/// �շѱ���(������)
		/// </summary>
		[Obsolete("����,FTRate����", true)]
		public decimal Rate=0m;

		#endregion
	}

    /// <summary>
    /// סԺ������ϸ���������ֶΡ�������Դ����Ĭ��ֵ����0
    ///����Ϊ3��ÿһλ������ͬ���壬����չ
    ///��һλ�������շѵ����0 �ֹ��շ� ��1ҽ���շѣ�2�Զ��շѣ�
    ///�ڶ�λ�������շ����������շѵص���շ�ʱ�䣨������ֵ��
    ///����λ�������˷����յ����0������1��ݱ����2�����ٻأ�
    ///000-��ʿվ�ֹ��շ�
    ///010-סԺ���ֹ��շ�
    ///020-�������ֹ��շ�
    ///001-��ʿվ�ֹ��շѣ���ݱ����
    ///011-סԺ���ֹ��շѣ���ݱ����
    ///021-�������ֹ��շѣ���ݱ����
    ///002-��ʿվ�ֹ��շѣ������ٻأ�
    ///012-סԺ���ֹ��շѣ������ٻأ�
    ///022-�������ֹ��շѣ������ٻأ�
    ///100-���ҽ���շ�
    ///110-�ֽ�ҽ���շ�
    ///120-��ӡʱ�շ�
    ///130-ִ��ʱ�շ�
    ///140-��ҩʱ�շ�
    ///100-���ҽ���շ�
    ///110-�ֽ�ҽ���շ�
    ///120-��ӡʱ�շ�
    ///130-ִ��ʱ�շ�
    ///140-��ҩʱ�շ�
    ///150-�ն��շ�
    ///101-���ҽ���շѣ���ݱ����
    ///111-�ֽ�ҽ���շѣ���ݱ����
    ///121-��ӡʱ�շѣ���ݱ����
    ///131-ִ��ʱ�շѣ���ݱ����
    ///141-��ҩʱ�շѣ���ݱ����
    ///151-�ն��շѣ���ݱ����
    ///102-���ҽ���շѣ������ٻأ�
    ///112-�ֽ�ҽ���շѣ������ٻأ�
    ///122-��ӡʱ�շѣ������ٻأ�
    ///132-ִ��ʱ�շѣ������ٻأ�
    ///142-��ҩʱ�շѣ������ٻأ�
    ///152-�ն��շѣ������ٻأ�
    ///200-�Զ��շѣ����϶�ʱ��
    ///210-�Զ��շѣ���Ժ���գ�����գ�
    ///220-�Զ��շѣ���Ժ���գ�������գ�
    ///201-�Զ��շѣ����϶�ʱ������ݱ����
    ///211-�Զ��շѣ���Ժ���գ�����գ�����ݱ����
    ///221-�Զ��շѣ���Ժ���գ�������գ�����ݱ����
    ///202-�Զ��շѣ����϶�ʱ���������ٻأ�
    ///212-�Զ��շѣ���Ժ���գ�����գ��������ٻأ�
    ///222-�Զ��շѣ���Ժ���գ�������գ��������ٻأ�
    ///����������дFTSource��
    ///�������ֶα�ʾ
    ///SourceType1
    ///SourceType2
    ///SourceType3
    ///��дToString()���������������ֵ
    /// </summary>
    public class FTSource
    {
        public FTSource()
        {
        }

        /// <summary>
        /// ������Դ
        /// </summary>
        /// <param name="sourceCost"></param>
        public FTSource(string sourceCost)
        {
            if (sourceCost == null)
            {
            }
            else if (sourceCost.Length==1)
            {
                sourceType1 = sourceCost;
            }
            else if (sourceCost.Length == 2)
            {
                sourceType1 = sourceCost[0].ToString();
                sourceType2 = sourceCost[1].ToString();
            }
            else if (sourceCost.Length == 3)
            {
                sourceType1 = sourceCost[0].ToString();
                sourceType2 = sourceCost[1].ToString();
                sourceType3 = sourceCost[2].ToString();
            }
        }

        private string sourceType1;
        private string sourceType2;
        private string sourceType3;

        /// <summary>
        ///������Դ��һλ�������շѵ����0 �ֹ��շ� ��1ҽ���շѣ�2�Զ��շѣ�
        /// </summary>
        public string SourceType1
        {
            get
            {
                if (sourceType1 == null)
                {
                    sourceType1 = "0";
                }
                return this.sourceType1;
            }
            set
            {
                this.sourceType1=value;
            }
        }
        /// <summary>
        /// ������Դ�ڶ�λ�������շ����������շѵص���շ�ʱ�䣨������ֵ��
        /// </summary>
        public string SourceType2
        {
            get
            {
                if (sourceType2 == null)
                {
                    sourceType2 = "0";
                }
                return this.sourceType2;
            }
            set
            {
                this.sourceType2=value;
            }
        }
        /// <summary>
        /// ������Դ����λ�������˷����յ����0������1��ݱ����2�����ٻأ�
        /// </summary>
        public string SourceType3
        {
            get
            {
                if (sourceType3 == null)
                {
                    sourceType3 = "0";
                }
                return this.sourceType3;
            }
            set
            {
                this.sourceType3=value;
            }
        }

        public new FTSource Clone()
        {
            FTSource s = base.MemberwiseClone() as FTSource;
            return s;
        }

        public override string ToString()
        {
            return sourceType1 + sourceType2+sourceType3;
        }
    }
}
