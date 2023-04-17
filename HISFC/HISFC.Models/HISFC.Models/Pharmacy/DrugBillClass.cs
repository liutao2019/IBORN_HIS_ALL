using System;

namespace FS.HISFC.Models.Pharmacy
{
	/// <summary>
	/// [��������: ��ҩ������ʵ��]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2004-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���='������'
	///		�޸�ʱ��='2006-09-12'
	///		�޸�Ŀ��='ϵͳ�ع�'
	///		�޸�����='�����淶����'
	///  />
	///  ID		��ҩ���������
	///  Name   ��ҩ����������
	/// </summary>
    [Serializable]
    public class DrugBillClass: FS.FrameWork.Models.NeuObject,FS.HISFC.Models.Base.IValid
	{
		public DrugBillClass()
		{
			
		}
		

		#region ����

        /// <summary>
        /// �������(���뷢�Ϳ���)
        /// </summary>
        private FS.FrameWork.Models.NeuObject applyDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ��ӡ����
		/// </summary>
		private BillPrintType myPrintType = new BillPrintType();

		/// <summary>
		/// ��ҩ����
		/// </summary>
		private DrugAttribute myDrugAttribute = new DrugAttribute();

		/// <summary>
		/// ��Ч��
		/// </summary>
		private System.Boolean  myIsValid;

		/// <summary>
		/// ����������Ϣ
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ��ҩ����
        /// </summary>
        private string drugBillNO;

        /// <summary>
        /// ����״̬
        /// </summary>
        private string applyState;

		#endregion

        /// <summary>
        /// �������(���뷢�Ϳ���)  ���������ݿ� ��������ʹ��
        /// </summary>
        public FS.FrameWork.Models.NeuObject ApplyDept
        {
            get
            {
                return this.applyDept;
            }
            set
            {
                this.applyDept = value;
            }
        }

        /// <summary>
        /// ��ҩ���� ���������ݿ� ��������ʹ��
        /// </summary>
        public string DrugBillNO
        {
            get
            {
                return this.drugBillNO;
            }
            set
            {
                this.drugBillNO = value;
            }
        }

        /// <summary>
        /// ����״̬  ���������ݿ� ��������ʹ��
        /// </summary>
        public string ApplyState
        {
            get
            {
                return this.applyState;
            }
            set
            {
                this.applyState = value;
            }
        }

		/// <summary>
		/// ��ӡ����1����2��ϸ3��ҩ4�󴦷�
		/// </summary>
		public BillPrintType PrintType
		{
			get
			{
				return this.myPrintType;
			}
			set
			{ 
				this.myPrintType = value;
			}
		}

		/// <summary>
		/// ��ҩ����
		/// </summary>
		public DrugAttribute DrugAttribute
		{
			get
			{
				return this.myDrugAttribute; 
			}
			set
			{
				this.myDrugAttribute = value; 
			}
		}

		/// <summary>
		/// �Ƿ���Ч1-��Ч0����Ч
		/// </summary>
		public bool IsValid
		{
			get
			{ 
				return this.myIsValid; 
			}
			set
			{ 
				this.myIsValid = value; 
			}
		}

		/// <summary>
		/// ������Ϣ
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment Oper
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
		/// ��¡����
		/// </summary>
		/// <returns>���ص�ǰʵ���ĸ���</returns>
		public new DrugBillClass Clone()
		{
			DrugBillClass drugBillClass = base.Clone() as DrugBillClass;

            drugBillClass.ApplyDept = this.ApplyDept.Clone();

			drugBillClass.PrintType = this.PrintType.Clone();

			drugBillClass.DrugAttribute = this.DrugAttribute.Clone();

			drugBillClass.Oper = this.Oper.Clone();

			return drugBillClass;
		}

		#region ��Ч����

		private System.String   myOperCode ;

		private System.DateTime myOperDate ;

		/// <summary>
		/// ����Ա
		/// </summary>
		[System.Obsolete("�������� ����ΪOper����",true)]
		public System.String OperCode
		{
			get{ return this.myOperCode; }
			set{ this.myOperCode = value; }
		}


		/// <summary>
		/// ����ʱ��
		/// </summary>
		[System.Obsolete("�������� ����ΪOper����",true)]
		public System.DateTime OperDate
		{
			get{ return this.myOperDate; }
			set{ this.myOperDate = value; }
		}


		#endregion

	}
}
