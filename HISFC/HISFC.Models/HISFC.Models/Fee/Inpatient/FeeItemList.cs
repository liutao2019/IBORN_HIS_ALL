using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Order;
using System.Collections.Generic;

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
		/// [�� �� ��: ����]<br></br>
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
		/// �������
		/// </summary>
		private int balanceNO;
		
		/// <summary>
		/// ����״̬
		/// </summary>
		private string balanceState = string.Empty;

		/// <summary>
		/// Ӥ�����
		/// </summary>
		private string babyNO = string.Empty;
		
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
		private string machineNO = string.Empty;
		
		/// <summary>
		/// ���ñ���(����Ϊ������������)
		/// </summary>
		private FTRate ftRate = new FTRate();

		/// <summary>
		/// ������
		/// </summary>
		private string auditingNO = string.Empty;
		
		/// <summary>
		/// �����������(�����ˣ�����ʱ�䣬���������ڿ��ң�
		/// </summary>
		private OperEnvironment balanceOper = new OperEnvironment();
		
		/// <summary>
		/// ҽ��ִ�е�
		/// </summary>
		private ExecOrder execOrder = new ExecOrder();
		
		/// <summary>
		/// ��չ��־
		/// </summary>
		private string extFlag = string.Empty;

        /// <summary>
        /// ��չ��־1
        /// </summary>
        private string extFlag1 = string.Empty;

        /// <summary>
        /// ��չ��־2
        /// </summary>
        private string extFlag2 = string.Empty;

        /// <summary>
        /// ��չ����
        /// </summary>
        private string extCode = string.Empty;

        /// <summary>
        /// ��չ��������(��չ��,��չʱ��,��չ�����ڿ���)
        /// </summary>
        private OperEnvironment extOper = new OperEnvironment();

		/// <summary>
		/// ������
		/// </summary>
		private string approveNO = string.Empty;

        /// <summary>
        /// �Ƿ���Ҫ���¿������� true ��Ҫ false ����Ҫ
        /// </summary>
        private bool isNeedUpdateNoBackQty;

        //{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E} ����ҽ���鴦��
        /// <summary>
        /// ҽ����
        /// </summary>
        private FS.FrameWork.Models.NeuObject medicalTeam = new NeuObject();

        /// <summary>
        /// ��������{0604764A-3F55-428f-9064-FB4C53FD8136}
        /// </summary>
        private string operationNO = string.Empty;

        

        
		#endregion

		#region ����

        /// <summary>
        /// ҽ����Ϣ
        /// </summary>
        public new Order.Inpatient.Order Order
        {
            get
            {
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

        

        //{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E} ����ҽ���鴦��
        /// <summary>
        /// ҽ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject MedicalTeam
        {
            get { return medicalTeam; }
            set { medicalTeam = value; }
        }

        /// <summary>
        /// ��������{0604764A-3F55-428f-9064-FB4C53FD8136}
        /// </summary>
        public string OperationNO
        {
            get { return operationNO; }
            set { operationNO = value; }
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
			feeItemList.BalanceOper = this.BalanceOper.Clone();
			feeItemList.ExecOrder = this.ExecOrder.Clone();
            //{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E} ����ҽ���鴦��
            feeItemList.MedicalTeam = this.MedicalTeam.Clone();
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
		public NeuObject ItemGroup = new NeuObject()   ;
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
}
