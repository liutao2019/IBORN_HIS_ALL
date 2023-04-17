using System;
using System.Collections;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;
using System.Collections.Generic;

namespace FS.HISFC.Models.Fee 
{
	/// <summary>
	/// ReturnApply<br></br>
	/// [��������: �˷�������]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-09-06]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public class ReturnApply : Inpatient.FeeItemList, IBaby
	{
		
		#region ����
		
		/// <summary>
		/// ���뵥�ݺ�
		/// </summary>
		private string applyBillNO = string.Empty;

        /// <summary>
        /// ȷ�ϵ���
        /// </summary>
        private string confirmBillNO = string.Empty;
		
		/// <summary>
		/// �Ƿ�Ӥ��
		/// </summary>
		private bool isBaby;
		
		/// <summary>
		/// Ӥ�����
		/// </summary>
		private string babyNO = string.Empty;
		
		/// <summary>
		/// �������ڿ���
		/// </summary>
		private NeuObject dept = new NeuObject();
		
		/// <summary>
		/// �������ڻ�ʿվ
		/// </summary>
		private NeuObject nurseStation = new NeuObject();
		
		/// <summary>
		/// ��������(����Ա,��������,����ʱ��)
		/// </summary>
		private OperEnvironment oper = new OperEnvironment();

        /// <summary>
        /// �Ƿ��հ�װ��λ�˷�
        /// </summary>
        private bool isPackUnit = false;

        private System.Collections.Generic.List<ReturnApplyMet> applyMateList = new System.Collections.Generic.List<ReturnApplyMet>();

		#endregion
		
		#region ����
		
		/// <summary>
		/// ���뵥�ݺ�
		/// </summary>
		public string ApplyBillNO
		{
			get
			{
				return this.applyBillNO;
			}
			set
			{
				this.applyBillNO = value;
			}
		}

        /// <summary>
        /// ȷ�ϵ���
        /// </summary>
        public string ConfirmBillNO 
        {
            get 
            {
                return this.confirmBillNO;
            }
            set 
            {
                this.confirmBillNO = value;
            }
        }
		
		
		
		/// <summary>
		/// ��������(����Ա,��������,����ʱ��)
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
        /// �Ƿ��հ�װ��λ�˷�
        /// </summary>
        public bool IsPackUnit 
        {
            get 
            {
                return this.isPackUnit;
            }
            set 
            {
                this.isPackUnit = value;
            }
        }

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        //{25934862-5C82-409c-A0D7-7BE5A24FFC75}
        public System.Collections.Generic.List<ReturnApplyMet> ApplyMateList
        {
            get
            {
                return applyMateList;
            }
            set
            {
                applyMateList = value;
            }
        }

        
		#endregion

		#region ����
		
		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ����ʵ������</returns>
		public new ReturnApply Clone()
		{
			ReturnApply returnApply = base.Clone() as ReturnApply;

            //returnApply.Dept = this.Dept.Clone();
            //returnApply.NurseStation = this.NurseStation.Clone();
            returnApply.Oper = this.Oper.Clone();
            List<ReturnApplyMet> list= new List<ReturnApplyMet>();
            foreach (ReturnApplyMet item in this.ApplyMateList)
            {
                list.Add(item.Clone());
            }
            returnApply.ApplyMateList = list;
			return returnApply;
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

		#region ���ñ��� ����


		private System.String myBillCode ;
		private System.String myInpatientNo ;
		
		private FS.FrameWork.Models.NeuObject myDept = new FS.FrameWork.Models.NeuObject();
		private FS.FrameWork.Models.NeuObject myNurseCellCode = new FS.FrameWork.Models.NeuObject();
		private System.String myDrugFlag ;
		private FS.HISFC.Models.Base.Item item = new FS.HISFC.Models.Base.Item();
		
		
		private System.String myExecDpcd ;
		private System.String myOperCode ;
		private System.DateTime myOperDate ;
		private System.String myOperDpcd ;
		private System.String myRecipeNo ;
		private System.Int32 mySequenceNo ;
		private string myBillNo;
		private System.String myConfirmFlag ;
		private System.String myConfirmDpcd ;
		private System.String myConfirmCode ;
		private System.DateTime myConfirmDate ;
		private System.String myChargeFlag ;
		private System.String myChargeCode ;
		private System.DateTime myChargeDate ;
		private string extFlag3 ; //  1 ��װ ��λ 0, ��С��λ

		/// <summary>
		/// ���뵥�ݺ�
		/// </summary>
		[Obsolete("����,������BillNO����", true)]
		public System.String BillCode 
		{
			get{ return this.myBillCode; }
			set{ this.myBillCode = value; }
		}


		/// <summary>
		/// סԺ��ˮ��
		/// </summary>
		[Obsolete("����,������Base.ID����", true)]
		public System.String InpatientNo 
		{
			get{ return this.myInpatientNo; }
			set{ this.myInpatientNo = value; }
		}

		/// <summary>
		/// ���ڲ���
		/// </summary>
		[Obsolete("����,������NurseStation����", true)]
		public FS.FrameWork.Models.NeuObject NurseCellCode 
		{
			get{ return this.myNurseCellCode; }
			set{ this.myNurseCellCode = value; }
		}


		/// <summary>
		/// ҩƷ��־,1ҩƷ/2��ҩ
		/// </summary>
		[Obsolete("����,��IsFharmacy����", true)]
		public System.String DrugFlag 
		{
			get{ return this.myDrugFlag; }
			set{ this.myDrugFlag = value; }
		}

		/// <summary>
		/// ִ�п���
		/// </summary>
		[Obsolete("����,������ExecOper.Dept����", true)]
		public System.String ExecDpcd 
		{
			get{ return this.myExecDpcd; }
			set{ this.myExecDpcd = value; }
		}


		/// <summary>
		/// ����Ա����
		/// </summary>
		[Obsolete("����,������ExecOper.Employee����", true)]
		public System.String OperCode 
		{
			get{ return this.myOperCode; }
			set{ this.myOperCode = value; }
		}


		/// <summary>
		/// ����ʱ��
		/// </summary>
		[Obsolete("����,������ExecOper.OperTime����", true)]
		public System.DateTime OperDate 
		{
			get{ return this.myOperDate; }
			set{ this.myOperDate = value; }
		}


		/// <summary>
		/// ����Ա���ڿ���
		/// </summary>
		[Obsolete("����,������Oper.Dept����", true)]
		public System.String OperDpcd 
		{
			get{ return this.myOperDpcd; }
			set{ this.myOperDpcd = value; }
		}


		/// <summary>
		/// ��Ӧ�շ���ϸ������
		/// </summary>
		[Obsolete("����,������RecipeNO����", true)]
		public System.String RecipeNo 
		{
			get{ return this.myRecipeNo; }
			set{ this.myRecipeNo = value; }
		}


		/// <summary>
		/// ��Ӧ��������ˮ��
		/// </summary>
		[Obsolete("����,������SequenceNO����", true)]
		public System.Int32 SequenceNo 
		{
			get{ return this.mySequenceNo; }
			set{ this.mySequenceNo = value; }
		}

		
		/// <summary>
		/// �˷ѵ��ݺ� ����ϵͳ�б����˷ѵķ�Ʊ��
		/// </summary>
		[Obsolete("����,������Invoice.ID����", true)]
		public string BillNo
		{
			get
			{
				return this.myBillNo;
			}
			set
			{
				this.myBillNo = value;
			}
		}

		/// <summary>
		/// ��ҩȷ�ϱ�ʶ 0δȷ��/1ȷ��
		/// </summary>
		[Obsolete("����,������IsComfrimed����", true)]
		public System.String ConfirmFlag 
		{
			get{ return this.myConfirmFlag; }
			set{ this.myConfirmFlag = value; }
		}


		/// <summary>
		/// ȷ�Ͽ��Ҵ���
		/// </summary>
		[Obsolete("����,������ConfirmOper.Dept����", true)]
		public System.String ConfirmDpcd 
		{
			get{ return this.myConfirmDpcd; }
			set{ this.myConfirmDpcd = value; }
		}


		/// <summary>
		/// ȷ���˱���
		/// </summary>
		[Obsolete("����,������ConfirmOper.Employee����", true)]
		public System.String ConfirmCode 
		{
			get{ return this.myConfirmCode; }
			set{ this.myConfirmCode = value; }
		}


		/// <summary>
		/// ȷ��ʱ��
		/// </summary>
		[Obsolete("����,������ConfirmOper.OperTime����", true)]
		public System.DateTime ConfirmDate 
		{
			get{ return this.myConfirmDate; }
			set{ this.myConfirmDate = value; }
		}


		/// <summary>
		/// �˷ѱ�ʶ 0δ�˷�/1�˷�
		/// </summary>
		[Obsolete("����,������CancelType����", true)]
		public System.String ChargeFlag 
		{
			get{ return this.myChargeFlag; }
			set{ this.myChargeFlag = value; }
		}


		/// <summary>
		/// �˷�ȷ����
		/// </summary>
		[Obsolete("����,������CancelOper.Employee����", true)]
		public System.String ChargeCode 
		{
			get{ return this.myChargeCode; }
			set{ this.myChargeCode = value; }
		}


		/// <summary>
		/// �˷�ȷ��ʱ��
		/// </summary>
		[Obsolete("����,������CancelOper.OperTime����", true)]
		public System.DateTime ChargeDate 
		{
			get{ return this.myChargeDate; }
			set{ this.myChargeDate = value; }
		}

		/// <summary>
		/// ��װ ��λ 0, ��С��λ
		/// </summary>
		[Obsolete("����,������FeePack����", true)]
		public string ExtFlage3
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

		#endregion
	}
    /// <summary>
    /// ReturnApply<br></br>
    /// [��������: �˷�����������Ϣ��]<br></br>
    /// [�� �� ��: ·־��]<br></br>
    /// [����ʱ��: 2008-04-24]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    //{25934862-5C82-409c-A0D7-7BE5A24FFC75}
    public class ReturnApplyMet : NeuObject
    {
        #region ����
        /// <summary>
        /// ������ˮ��
        /// </summary>
        private string applyNo = string.Empty;
        /// <summary>
        /// ���ⵥ��ˮ��
        /// </summary>
        private string outNo = string.Empty;
        /// <summary>
        /// ������
        /// </summary>
        private string stockNo = string.Empty;
        /// <summary>
        /// ������
        /// </summary>
        private string recipeNo = string.Empty;
        /// <summary>
        /// ��������Ŀ��ˮ��
        /// </summary>
        private string sequenceNo = string.Empty;
        /// <summary>
        /// �˷ѱ�ʶ
        /// </summary>
        HISFC.Models.Base.CancelTypes applyFlag = CancelTypes.Canceled;
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private HISFC.Models.FeeStuff.MaterialItem item = new FS.HISFC.Models.FeeStuff.MaterialItem();
        #endregion

        #region ����
        /// <summary>
        /// ������ˮ��
        /// </summary>
        public string ApplyNo
        {
            get
            {
                return applyNo;
            }
            set
            {
                applyNo = value;
            }
        }

        /// <summary>
        /// ���ⵥ��ˮ��
        /// </summary>
        public string OutNo
        {
            get
            {
                return outNo;
            }
            set
            {
                outNo = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string StockNo
        {
            get
            {
                return stockNo;
            }
            set
            {
                stockNo = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string RecipeNo
        {
            get
            {
                return recipeNo;
            }
            set
            {
                recipeNo = value;
            }
        }

        /// <summary>
        /// ��������Ŀ��ˮ��
        /// </summary>
        public string SequenceNo
        {
            get
            {
                return sequenceNo;
            }
            set
            {
                sequenceNo = value;
            }
        }
        /// <summary>
        /// �˷ѱ�ʶ
        /// </summary>
        public CancelTypes ApplyFlag
        {
            get
            {
                return applyFlag;
            }
            set
            {
                applyFlag = value;
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public HISFC.Models.FeeStuff.MaterialItem Item
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new ReturnApplyMet Clone()
        {
            ReturnApplyMet returnApplyMet = base.Clone() as ReturnApplyMet;
            returnApplyMet.Item = this.Item.Clone();
            return returnApplyMet;
        }
        #endregion
    }
}