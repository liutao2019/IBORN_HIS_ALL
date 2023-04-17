using System;
using FS.HISFC.Models.RADT;

namespace FS.HISFC.Models.Pharmacy
{
	/// <summary>
	/// Copyright (C) 2004 ��˹
	/// ��Ȩ����
	/// 
	/// �ļ�����DrugRecipe.cs
	/// �ļ����������������ҩ����(��������)ʵ��
	/// 
	/// 
	/// ������ʶ����˹ 2005-11
	/// ����˵����ID ��ҩҩ������ Name ��ҩҩ������
	/// 
	/// 
	/// �޸ı�ʶ����˹ 2006-04
	/// �޸��������޸�Sex����ΪSex��
	/// 
	/// 
	/// 
	/// �޸ı�ʶ����˹ 2006-09
	/// �޸���������������
	/// </summary>
    [Serializable]
    public class DrugRecipe : FS.FrameWork.Models.NeuObject,FS.HISFC.Models.Base.IValidState
	{
		public DrugRecipe()
		{
			
		}
		
		
		#region ����		

		/// <summary>
		/// ������
		/// </summary>
		private string recipeNo = "";

		/// <summary>
		/// �����������(Ȩ������ Class3_Menaing_Code) ���� M1 ��ҩ M2 �ˡ���ҩ
		/// </summary>
		private string systemType = "";

		/// <summary>
		/// �������� 1 ������ 2 ������
		/// </summary>
		private string transType = "";

		/// <summary>
		/// ����״̬: 0����,1��ӡ,2��ҩ,3��ҩ,4��ҩ(����δ����ҩƷ����)
		/// </summary>
		private string recipeState = "";

		/// <summary>
		/// �����
		/// </summary>
		private string clinicCode = "";

		/// <summary>
		/// ��������
		/// </summary>
		private string cardNo = "";

		/// <summary>
		/// ��������
		/// </summary>
		private string patientName = "";

		/// <summary>
		/// �Ա�
		/// </summary>
		FS.HISFC.Models.Base.SexEnumService sex = new FS.HISFC.Models.Base.SexEnumService();

		/// <summary>
		/// ����
		/// </summary>
		DateTime age = DateTime.MinValue;

		/// <summary>
		/// �Һ�����
		/// </summary>
		DateTime regDate = DateTime.MinValue;

		/// <summary>
		/// �������
		/// </summary>
		FS.FrameWork.Models.NeuObject payKind = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ���߿���
		/// </summary>
		FS.FrameWork.Models.NeuObject patientDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ����ҽ��
		/// </summary>
		FS.FrameWork.Models.NeuObject doct = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ����ҽ������
		/// </summary>
		FS.FrameWork.Models.NeuObject doctDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ��ҩ�ն�
		/// </summary>
		FS.FrameWork.Models.NeuObject drugTerminal = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ��ҩ�ն�
		/// </summary>
		FS.FrameWork.Models.NeuObject sendTerminal = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ��/��ҩ����
		/// </summary>
		private FS.FrameWork.Models.NeuObject drugDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// �շѲ�����Ϣ
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment feeOper = new FS.HISFC.Models.Base.OperEnvironment();

		/// <summary>
		/// ��Ʊ��
		/// </summary>
		string invoiceNo;

		/// <summary>
		/// �������
		/// </summary>
		decimal cost;

		/// <summary>
		/// ������ҩƷƷ������
		/// </summary>
		decimal recipeNum;

		/// <summary>
		/// ����ҩҩƷƷ������
		/// </summary>
		decimal drugedNum;

		/// <summary>
		/// ��ҩ������Ϣ
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment drugedOper = new FS.HISFC.Models.Base.OperEnvironment();
		
		/// <summary>
		/// ��ҩ������Ϣ
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment sendOper = new FS.HISFC.Models.Base.OperEnvironment();
		
		/// <summary>
		/// ��ҩ��
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment backOper = new FS.HISFC.Models.Base.OperEnvironment();

		/// <summary>
		/// ȡ����
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment cancelOper = new FS.HISFC.Models.Base.OperEnvironment();

		/// <summary>
		/// �Ƿ��˸�ҩ
		/// </summary>
		private bool isModify;

        /// <summary>
        /// ��Ч״̬  0 ��Ч 1 ��Ч  2 ��ҩ����ҩ
        /// �ĺ�״̬ 0 ��Ч 1 ��Ч 2 ��ҩ����ҩ
        /// </summary>
        FS.HISFC.Models.Base.EnumValidState validState = FS.HISFC.Models.Base.EnumValidState.Valid;

        /// <summary>
        /// ������ҩƷ�����ϼ�
        /// </summary>
        decimal sumDays;
		#endregion

		/// <summary>
		/// ������
		/// </summary>
		public string RecipeNO
		{
			get
			{
				return this.recipeNo;
			}
			set
			{
				this.recipeNo = value;
			}
		}


		/// <summary>
		/// �����������(Ȩ������ Class3_Menaing_Code) ���� M1 ��ҩ M2 �ˡ���ҩ
		/// </summary>
		public string SystemType
		{
			get
			{
				return this.systemType;
			}
			set
			{
				this.systemType = value;
			}
		}


		/// <summary>
		/// �������� 1 ������ 2 ������
		/// </summary>
		public string TransType
		{
			get
			{
				return this.transType;
			}
			set
			{
				this.transType = value;
			}
		}


		/// <summary>
		/// ����״̬: 0����,1��ӡ,2��ҩ,3��ҩ,4��ҩ(����δ����ҩƷ����)
		/// </summary>
		public string RecipeState
		{
			get
			{
				return this.recipeState;
			}
			set
			{
				this.recipeState = value;
			}
		}


		/// <summary>
		/// �����
		/// </summary>
		public string ClinicNO
		{
			get
			{
				return this.clinicCode;
			}
			set
			{
				this.clinicCode = value;
			}
		}


		/// <summary>
		/// ��������
		/// </summary>
		public string CardNO
		{
			get
			{
				return this.cardNo;
			}
			set
			{
				this.cardNo = value;
			}
		}


		/// <summary>
		/// ��������
		/// </summary>
		public string PatientName
		{
			get
			{
				return this.patientName;
			}
			set
			{
				this.patientName = value;
			}
		}


		/// <summary>
		/// �Ա�
		/// </summary>
		public FS.HISFC.Models.Base.SexEnumService Sex
		{
			get
			{
				return this.sex;
			}
			set
			{
				this.sex = value;
			}
		}


		/// <summary>
		/// ����
		/// </summary>
		public DateTime Age
		{
			get
			{
				return this.age;
			}
			set
			{
				this.age = value;
			}
		}


		/// <summary>
		/// �������
		/// </summary>
		public FS.FrameWork.Models.NeuObject PayKind
		{
			get
			{
				return this.payKind;
			}
			set
			{
				this.payKind = value;
			}
		}


		/// <summary>
		/// ���߿���
		/// </summary>
		public FS.FrameWork.Models.NeuObject PatientDept
		{
			get
			{
				return this.patientDept;
			}
			set
			{
				this.patientDept = value;
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
		/// ����ҽ��
		/// </summary>
		public FS.FrameWork.Models.NeuObject Doct
		{
			get
			{
				return this.doct;
			}
			set
			{
				this.doct = value;
			}
		}


		/// <summary>
		/// ����ҽ������
		/// </summary>
		public FS.FrameWork.Models.NeuObject DoctDept
		{
			get
			{
				return this.doctDept;
			}
			set
			{
				this.doctDept = value;
			}
		}


		/// <summary>
		/// ��ҩ�ն�
		/// </summary>
		public FS.FrameWork.Models.NeuObject DrugTerminal
		{
			get
			{
				return this.drugTerminal;
			}
			set
			{
				this.drugTerminal = value;
			}
		}


		/// <summary>
		/// ��ҩ�ն�
		/// </summary>
		public FS.FrameWork.Models.NeuObject SendTerminal
		{
			get
			{
				return this.sendTerminal;
			}
			set
			{
				this.sendTerminal = value;
			}
		}


		/// <summary>
		/// ����Ա�շ���Ϣ
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment FeeOper
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
		/// ��Ʊ��
		/// </summary>
		public string InvoiceNO
		{
			get
			{
				return this.invoiceNo;
			}
			set
			{
				this.invoiceNo = value;
			}
		}


		/// <summary>
		/// �������
		/// </summary>
		public decimal Cost
		{
			get
			{
				return this.cost;
			}
			set
			{
				this.cost = value;
			}
		}


		/// <summary>
		/// ������ҩƷƷ������
		/// </summary>
		public decimal RecipeQty
		{
			get
			{
				return this.recipeNum;
			}
			set
			{
				this.recipeNum = value;
			}
		}


		/// <summary>
		/// ����ҩҩƷƷ������
		/// </summary>
		public decimal DrugedQty
		{
			get
			{
				return this.drugedNum;
			}
			set
			{
				this.drugedNum = value;
			}
		}


		/// <summary>
		/// ��ҩ��
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment DrugedOper
		{
			get
			{
				return this.drugedOper;
			}
			set
			{
				this.drugedOper = value;
			}
		}


		/// <summary>
		/// ��ҩ��
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment SendOper
		{
			get
			{
				return this.sendOper;
			}
			set
			{
				this.sendOper = value;
			}
		}
		

		/// <summary>
		/// ��/��ҩ����
		/// </summary>
		public FS.FrameWork.Models.NeuObject StockDept
		{
			get
			{
				return this.drugDept;
			}
			set
			{
				this.drugDept = value;
				this.ID = value.ID;
				this.Name = value.Name;
			}
		}


		/// <summary>
		/// ��/��ҩ״̬ 0 �� 1 ��
		/// </summary>
		public bool IsModify
		{
			get
			{
				return this.isModify;
			}
			set
			{
				this.isModify = value;
			}
		}


		/// <summary>
		/// ��ҩ��
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment BackOper
		{
			get
			{
				return this.backOper;
			}
			set
			{
				this.backOper = value;
			}
		}


		/// <summary>
		/// ȡ����
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment CancelOper
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
        /// ��Ч״̬  0 ��Ч 1 ��Ч  2 ��ҩ����ҩ
        /// ��Ч״̬  0 ��Ч 1 ��Ч  2 ��ҩ����ҩ
        /// </summary>
        public FS.HISFC.Models.Base.EnumValidState ValidState
        {
            get
            {
                return this.validState;
            }
            set
            {
                this.validState = value;
            }
        }

        /// <summary>
        /// ������ҩƷ�����ϼ�
        /// </summary>
        public decimal SumDays
        {
            get
            {
                return this.sumDays;
            }
            set
            {
                this.sumDays = value;
            }
        }

		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>�ɹ����ص�ǰʵ���ĸ���</returns>
		public new DrugRecipe Clone()
		{
			DrugRecipe drugRecipe = base.Clone() as DrugRecipe;
			
			drugRecipe.Sex = this.Sex.Clone();
			drugRecipe.PayKind = this.PayKind.Clone();
			drugRecipe.PatientDept = this.PatientDept.Clone();
			drugRecipe.Doct = this.Doct.Clone();
			drugRecipe.DoctDept = this.DoctDept.Clone();
			drugRecipe.DrugTerminal = this.DrugTerminal.Clone();
			drugRecipe.SendTerminal = this.SendTerminal.Clone();
			drugRecipe.FeeOper = this.FeeOper.Clone();
			drugRecipe.DrugedOper = this.DrugedOper.Clone();
			drugRecipe.SendOper = this.SendOper.Clone();
			drugRecipe.StockDept = this.StockDept.Clone();
			drugRecipe.BackOper = this.BackOper.Clone();
			drugRecipe.CancelOper = this.CancelOper.Clone();

			return drugRecipe;
			
		}


		#endregion

		#region ��Ч����

		/// <summary>
		/// �շ�ʱ��
		/// </summary>
		DateTime feeDate;

		/// <summary>
		/// ��ҩ����
		/// </summary>
		DateTime drugedDate;

		/// <summary>
		/// ��ҩ����
		/// </summary>
		DateTime sendDate;

		


		/// <summary>
		/// ������
		/// </summary>
		[System.Obsolete("�������� ����ΪRecipeNO����")]
		public string RecipeNo
		{
			get
			{
				return this.recipeNo;
			}
			set
			{
				this.recipeNo = value;
			}
		}


		/// <summary>
		/// �����
		/// </summary>
		[System.Obsolete("�������� ����ΪClinicNO����",true)]
		public string ClinicCode
		{
			get
			{
				return this.clinicCode;
			}
			set
			{
				this.clinicCode = value;
			}
		}


		/// <summary>
		/// ��������
		/// </summary>
		[System.Obsolete("�������� ����ΪCardNO����",true)]
		public string CardNo
		{
			get
			{
				return this.cardNo;
			}
			set
			{
				this.cardNo = value;
			}
		}


		/// <summary>
		/// �Һ�����
		/// </summary>
		[System.Obsolete("�������� ����ΪRegTime����",true)]
		public DateTime RegDate
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
		/// �շ�ʱ��
		/// </summary>
		[System.Obsolete("�������� ����ΪOperEnvironment���͵�FeeOper����",true)]
		public DateTime FeeDate
		{
			get
			{
				return this.feeDate;
			}
			set
			{
				this.feeDate = value;
			}
		}


		/// <summary>
		/// ��Ʊ��
		/// </summary>
		[System.Obsolete("�������� ����ΪInvoiceNO����",true)]
		public string InvoiceNo
		{
			get
			{
				return this.invoiceNo;
			}
			set
			{
				this.invoiceNo = value;
			}
		}


		/// <summary>
		/// ������ҩƷƷ������
		/// </summary>
		[System.Obsolete("�������� ����ΪRecipeQty����",true)]
		public decimal RecipeNum
		{
			get
			{
				return this.recipeNum;
			}
			set
			{
				this.recipeNum = value;
			}
		}


		/// <summary>
		/// ����ҩҩƷƷ������
		/// </summary>
		[System.Obsolete("�������� ����ΪDrugedQty����")]
		public decimal DrugedNum
		{
			get
			{
				return this.drugedNum;
			}
			set
			{
				this.drugedNum = value;
			}
		}


		/// <summary>
		/// ��ҩ����
		/// </summary>
		[System.Obsolete("�������� ����ΪDrugedOper����",true)]
		public DateTime DrugedDate
		{
			get
			{
				return this.drugedDate;
			}
			set
			{
				this.drugedDate = value;
			}
		}


		/// <summary>
		/// ��ҩ����
		/// </summary>
		[System.Obsolete("�������� ����ΪSendOper����")]
		public DateTime SendDate
		{
			get
			{
				return this.sendDate;
			}
			set
			{
				this.sendDate = value;
			}
		}


		/// <summary>
		/// ��/��ҩ����
		/// </summary>
		[System.Obsolete("�������� ����ΪStockDept����",true)]
		public FS.FrameWork.Models.NeuObject DrugDept
		{
			get
			{
				return this.drugDept;
			}
			set
			{
				this.drugDept = value;
				this.ID = value.ID;
				this.Name = value.Name;
			}
		}
		

		/// <summary>
		/// ��ҩʱ��
		/// </summary>
		[System.Obsolete("�������� ����ΪBackOper����",true)]
		public DateTime BackDate;

		/// <summary>
		/// ȡ��ʱ��
		/// </summary>
		[System.Obsolete("�������� ����ΪCancelOper����")]
		public DateTime CancelDate;

		/// <summary>
		/// ��/��ҩ״̬ 0 �� 1 ��
		/// </summary>
		[System.Obsolete("�������� ����ΪBool���͵�IsModify����")]
		public string ModifyState;

		#endregion

		
	}
}
