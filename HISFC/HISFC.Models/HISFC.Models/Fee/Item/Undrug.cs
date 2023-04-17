using System;
using System.Data;
using System.Collections;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee.Item
{
	/// <summary>
	/// Undrug<br></br>
	/// [��������: ��ҩƷ��Ϣ��]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-08-30]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public class Undrug : Base.Item 
	{
		public Undrug() 
		{
			this.IsNeedConfirm = false;
		}
		
		#region ����
		
		/// <summary>
		/// ִ�п���(�ַ�����ʽ)
		/// </summary>
		private string execDept;
		
		/// <summary>
		/// ִ�п���(������ʽ)
		/// </summary>
		private ArrayList execDepts = new ArrayList();
		
		/// <summary>
		/// ������Ϣ
		/// </summary>
		private FTRate ftRate = new FTRate();
		
		/// <summary>
		/// �Ƿ�ƻ����������Ŀ
		/// </summary>
		private bool isFamilyPlanning;
		
		/// <summary>
		/// �������(�ַ�����ʽ)
		/// </summary>
		private string machineNO;
		
		/// <summary>
		/// �������(������ʽ)
		/// </summary>
		private ArrayList machineNOs = new ArrayList();
		
		/// <summary>
		/// Ĭ�ϼ�鲿λ
		/// </summary>
		private string checkBody;
		
		/// <summary>
		/// �Ƿ������ʶ���
		/// </summary>
		private bool isCompareToMaterial;
		
		/// <summary>
		/// ��������(����Ա,����ʱ��,��������)
		/// </summary>
		private OperEnvironment oper = new OperEnvironment();
		
		/// <summary>
		/// ������Ϣ
		/// </summary>
		private NeuObject operationInfo = new NeuObject();
		
		/// <summary>
		/// ��������
		/// </summary>
		private NeuObject operationType = new NeuObject();
		
		/// <summary>
		/// ������ģ
		/// </summary>
		private NeuObject operationScale = new NeuObject();
		
		/// <summary>
		/// ��������
		/// </summary>
		private NeuObject diseaseType = new NeuObject();
		
		/// <summary>
		/// ר����Ϣ
		/// </summary>
		private Department specialDept = new Department();
		
		/// <summary>
		/// �Ƿ�֪��ͬ��
		/// </summary>
		private bool isConsent;

		/// <summary>
		/// ��ʷ�����
		/// </summary>
		private string medicalRecord;

		/// <summary>
		/// ���Ҫ��  
		/// </summary>
		private string checkRequest;
	
		/// <summary>
		/// ע������           
		/// </summary>
		private string notice;

		/// <summary>
		/// ������뵥����  
		/// </summary>
		private string checkApplyDept;
		
		/// <summary>
		/// ��Ŀ��Χ
		/// </summary>
		private string itemArea = "";
		
		/// <summary>
		/// ��Ŀ����
		/// </summary>
		private string itemException;

        /// <summary>
        /// ��λ��ʶ(0,��ϸ; 1,����)
        /// </summary>
        private string unitFlag="";

        /// <summary>
        /// ���÷�Χ
        /// </summary>
        private string applicabilityArea = "";

        /// <summary>
        /// ִ��״̬(0δִ�У�1��ִ��)(����453�汾��ֲ)
        /// </summary>
        private string executeFlag;

        /// <summary>
        /// ע��֤���(����453�汾��ֲ)
        /// </summary>
        private string registerNo;

        //{2A5608D8-26AD-47d7-82CC-81375722FF72}
        /// <summary>
        /// ����������Ŀ�Ŀ���
        /// </summary>
        private string deptList;
        //{55CFCB36-B084-4a56-95AD-2CDED962ADC4}
        /// <summary>
        /// ��۷������
        /// </summary>
        private string itemPriceType;
        /// <summary>
        /// �Ƿ��ӡҽ����
        /// </summary>
        private string isOrderPrint;
		#endregion

		#region ����
        /// <summary>
        /// ���÷�Χ
        /// </summary>
        public string ApplicabilityArea
        {
            get
            {
                return applicabilityArea;
            }
            set
            {
                applicabilityArea = value;
            }
        }
        /// <summary>
        /// ��λ��ʶ(0,��ϸ; 1,����)
        /// </summary>
        public string UnitFlag
        {
            get
            {
                return this.unitFlag;
            }
            set
            {
                this.unitFlag = value;
            }
        }

		/// <summary>
		/// ִ�п���(�ַ���)
		/// </summary>
		public string ExecDept
		{
			get
			{
				return execDept;
			}
			set
			{
				execDept = value;

				string[] s = value.Split('|');

				this.execDepts.Clear();
				
				this.execDepts.CopyTo(s, 0);
			}
		}
		
		/// <summary>
		/// ִ�п���(������ʽ)
		/// </summary>
		public ArrayList ExecDepts
		{
			get
			{
				return this.execDepts;
			}
			set
			{
				this.execDepts = value;
			}
		}
		
		/// <summary>
		/// ������Ϣ
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
		/// �Ƿ�ƻ����������Ŀ
		/// </summary>
		public bool IsFamilyPlanning
		{
			get
			{
				return this.isFamilyPlanning;
			}
			set
			{
				this.isFamilyPlanning = value;
			}
		}
		
		/// <summary>
		/// �豸���(�ַ���)
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
				
				string[] s = value.Split('|');

				this.machineNOs.Clear();

				this.machineNOs.CopyTo(s, 0);
			}
		}

		///<summary>
		///�豸���(����)
		///</summary>
		public ArrayList MachineNOs
		{
			get
			{
				return this.machineNOs;
			}
			set
			{
				this.machineNOs = value;
			}
		}

		/// <summary>
		/// Ĭ�ϼ�鲿λ
		/// </summary>
		public string CheckBody
		{
			get
			{
				return this.checkBody;
			}
			set
			{
				this.checkBody = value;
			}
		}

		/// <summary>
		/// �Ƿ������ʶ���
		/// </summary>
		public bool IsCompareToMaterial
		{
			get
			{
				return this.isCompareToMaterial;
			}
			set
			{
				this.isCompareToMaterial = value;
			}
		}

		/// <summary>
		/// ��������(����Ա,����ʱ��,��������)
		/// </summary>
		public OperEnvironment Oper
		{
			get
			{
				return this.oper;
			}
			set
			{
				this.oper = value;
			}
		}

		/// <summary>
		/// ������Ϣ
		/// </summary>
		public NeuObject OperationInfo
		{
			get
			{
				return this.operationInfo;
			}
			set
			{
				this.operationInfo = value;
			}
		}
		
		/// <summary>
		/// ��������
		/// </summary>
		public NeuObject OperationType
		{
			get
			{
				return this.operationType;
			}
			set
			{
				this.operationType = value;
			}
		}
		
		/// <summary>
		/// ������ģ
		/// </summary>
		public NeuObject OperationScale
		{
			get
			{
				return this.operationScale;
			}
			set
			{
				this.operationScale = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public NeuObject DiseaseType
		{
			get
			{
				return this.diseaseType;
			}
			set
			{
				this.diseaseType = value;
			}
		}
		
		/// <summary>
		/// ר����Ϣ
		/// </summary>
		public Department SpecialDept
		{
			get
			{
				return this.specialDept;
			}
			set
			{
				this.specialDept = value;
			}
		}
		
		/// <summary>
		/// �Ƿ�֪��ͬ��
		/// </summary>
		public bool IsConsent
		{
			get
			{
				return this.isConsent;
			}
			set
			{
				this.isConsent = value;
			}
		}

		/// <summary>
		/// ��ʷ�����
		/// </summary>
		public string MedicalRecord
		{
			get
			{
				return this.medicalRecord;
			}
			set
			{
				this.medicalRecord = value;
			}
		}

		/// <summary>
		/// ���Ҫ��  
		/// </summary>
		public string CheckRequest
		{
			get
			{
				return this.checkRequest;
			}
			set
			{
				this.checkRequest = value;
			}
		}
	
		/// <summary>
		/// ע������           
		/// </summary>
		public string Notice
		{
			get
			{
				return this.notice;
			}
			set
			{
				this.notice = value;
			}
		}

		/// <summary>
		/// ������뵥����  
		/// </summary>
		public string CheckApplyDept
		{
			get
			{
				return this.checkApplyDept;
			}
			set
			{
				this.checkApplyDept = value;
			}
		}

		/// <summary>
		/// ��Ŀ��Χ
		/// </summary>
		public string ItemArea
		{
			get
			{
				return this.itemArea;
			}
			set
			{
				this.itemArea = value;
			}
		}
		
		/// <summary>
		/// ��Ŀ����
		/// </summary>
		public string ItemException
		{
			get
			{
				return this.itemException;
			}
			set
			{
				this.itemException = value;
			}
		}

        //{4E5ADF40-F21E-403d-AC37-07795BBC3071}
        /// <summary>
        /// ִ��״̬(0δִ�У�1��ִ��)(����453�汾��ֲ)
        /// </summary>
        public string ExecuteFlag
        {
            get
            {
                return executeFlag;
            }
            set
            {
                executeFlag = value;
            }
        }

        //{4E5ADF40-F21E-403d-AC37-07795BBC3071}
        /// <summary>
        /// ע��֤���(����453�汾��ֲ)
        /// </summary>
        public string RegisterNo
        {
            get
            {
                return registerNo;
            }
            set
            {
                registerNo = value;
            }
        }
        //{2A5608D8-26AD-47d7-82CC-81375722FF72}
        /// <summary>
        /// ����������Ŀ�Ŀ���
        /// </summary>
        public string DeptList
        {
            get
            {
                return deptList;
            }
            set
            {
                deptList = value;
            }
        }
        /// <summary>
        /// ��۷������
        /// </summary>
        public string ItemPriceType
        {
            get
            {
                return this.itemPriceType;
            }
            set
            {
                this.itemPriceType = value;
            }
        }
        /// <summary>
        /// ҽ������ӡ����
        /// </summary>
        public string IsOrderPrint
        {
            get
            {
                return this.isOrderPrint;
            }
            set
            {
                this.isOrderPrint = value;
            }
        }
		#endregion

		#region ����
		
		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ����ʵ������</returns>
		public new Undrug Clone()
		{
			Undrug undrug = base.Clone() as Undrug;

			undrug.FTRate = this.FTRate.Clone();
			undrug.Oper = this.Oper.Clone();
			undrug.OperationInfo = this.OperationInfo.Clone();
			undrug.OperationScale = this.OperationScale.Clone();
			undrug.OperationType = this.OperationType.Clone();
			undrug.DiseaseType = this.DiseaseType.Clone();
			undrug.SpecialDept = this.SpecialDept.Clone();
			
			return undrug;
		}

		#endregion

		#endregion
   }
}