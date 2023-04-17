using System;

namespace FS.HISFC.Models.Pharmacy 
{
	/// <summary>
	/// [��������: ҩƷ���������Ϣ��]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2004-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��˹'
	///		�޸�ʱ��='2006-09-12'
	///		�޸�Ŀ��='ϵͳ�ع�'
	///		�޸�����='�����淶���� �̳���StorageBase����'
	///  />
	///  ID ���ⵥ��ˮ��
	/// </summary>
    [Serializable]
    public class Output : StorageBase 
	{
		public Output () 
		{
            //�˴�Ӧ�ô洢�û��Զ���ĳ������� ����Ӧ�ô洢0320
            //this.PrivType = "0320";	//����Ȩ�ޱ���

            this.Class2Type = "0320";
		}

		#region ����

		private string   myOutListCode;

		private string   myInBillCode = "0";

		private int      myInSerialNo;

		private string   myInListCode;

		private decimal  myApplyNum;

		private string   myApplyOperCode;

		private DateTime myApplyDate;

		private decimal  myExamNum;

		private string   myExamOperCode;

		private DateTime myExamDate;

		private string   myApproveOperCode;

		private DateTime myApproveDate;

		private decimal  myReturnNum;

		private string   myDrugedBillCode;

		private string   myMedID;

		private string   myRecipeNo;

		private int      mySequenceNo;

		private string   myGetPerson;

        private string   summary;

        /// <summary>
        /// ҩ��ۿ���ˮ��
        /// </summary>
        private string myChestOutNO;

        /// <summary>
        /// ����ʱ��   {F46D26C1-FBA7-44bc-9323-BEC9CD2115F9}
        /// </summary>
        private DateTime outDate;

        /// <summary>
        /// ����С��λ��
        /// </summary>
        private uint costDecimals = 2;

        #endregion

        /// <summary>
        /// ժҪ
        /// </summary>
        public string Summary
        {
            get { return summary; }
            set { summary = value; }
        }

        /// <summary>
        /// ����С��λ��
        /// </summary>
        public uint CostDecimals
        {
            get { return costDecimals; }
            set { costDecimals = value; }
        }


		/// <summary>
		/// ���ⵥ�ݺ�
		/// </summary>
		public string OutListNO 
		{
			get	
			{
				return  myOutListCode;
			}
			set	
			{
				myOutListCode = value;
			}
		}

		/// <summary>
		/// ��ⵥ��
		/// </summary>
		public string InBillNO 
		{
			get	
			{
				return  myInBillCode;
			}
			set	
			{
				myInBillCode = value;
			}
		}

		/// <summary>
		/// ��ⵥ�����
		/// </summary>
		public int InSerialNO 
		{
			get	
			{
				return  myInSerialNo;
			}
			set	
			{
				myInSerialNo = value; 
			}
		}

		/// <summary>
		/// ��ⵥ�ݺ�
		/// </summary>
		public string InListNO 
		{
			get	
			{
				return  myInListCode;
			}
			set	
			{
				myInListCode = value; 
			}
		}

		/// <summary>
		/// ��ҩ����
		/// </summary>
		public string DrugedBillNO 
		{
			get	
			{
				return  myDrugedBillCode;
			}
			set	
			{
				myDrugedBillCode = value; 
			}
		}

		/// <summary>
		/// �Ƽ����
		/// </summary>
		public string MedNO 
		{
			get	
			{
				return  myMedID;
			}
			set	
			{
				myMedID = value; 
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public string RecipeNO 
		{
			get	
			{
				return  myRecipeNo;
			}
			set	
			{
				myRecipeNo = value; 
			}
		}

		/// <summary>
		/// ���������
		/// </summary>
		public int SequenceNO 
		{
			get	
			{
				return  mySequenceNo;
			}
			set	
			{
				mySequenceNo = value;
			}
		}

		/// <summary>
		/// ȡҩ�ˣ��������￨�š�סԺ��ˮ�ţ�
		/// </summary>
		public string GetPerson 
		{
			get	
			{
				return  myGetPerson;
			}
			set	
			{
				myGetPerson = value; 
			}
		}

        /// <summary>
        /// ҩ��ۿ���ˮ��
        /// </summary>
        public string ArkOutNO
        {
            get
            {
                return this.myChestOutNO;
            }
            set
            {
                this.myChestOutNO = value;
            }
        }

        /// <summary>
        /// ����ʱ��  {F46D26C1-FBA7-44bc-9323-BEC9CD2115F9}
        /// </summary>
        public DateTime OutDate
        {
            get
            {
                return this.outDate;
            }
            set
            {
                this.outDate = value;
            }
        }

		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>�ɹ����ص�ǰʵ���ĸ���</returns>
		public new Output Clone()
		{
			return base.Clone() as Output;
		}


		#endregion

		#region ��Ч����

		/// <summary>
		/// ���ⵥ�ݺ�
		/// </summary>
		[System.Obsolete("�������� ����ΪOutListNO����",true)]
		public string OutListCode 
		{
			get	{ return  myOutListCode;}
			set	{  myOutListCode = value; }
		}


		/// <summary>
		/// ��ⵥ��
		/// </summary>
		[System.Obsolete("�������� ����ΪInBillNo����",true)]
		public string InBillCode 
		{
			get	{ return  myInBillCode;}
			set	{  myInBillCode = value; }
		}


		/// <summary>
		/// ��ⵥ�����
		/// </summary>
		[System.Obsolete("�������� ����ΪInSerialNO����",true)]
		public int InSerialNo 
		{
			get	{ return  myInSerialNo;}
			set	{  myInSerialNo = value; }
		}


		/// <summary>
		/// ��ⵥ�ݺ�
		/// </summary>
		[System.Obsolete("�������� ����ΪInListNO����",true)]
		public string InListCode 
		{
			get	{ return  myInListCode;}
			set	{  myInListCode = value; }
		}


		/// <summary>
		/// ��ҩ����
		/// </summary>
		[System.Obsolete("�������� ����ΪDrugBillNO����",true)]
		public string DrugedBillCode 
		{
			get	{ return  myDrugedBillCode;}
			set	{  myDrugedBillCode = value; }
		}


		/// <summary>
		/// �Ƽ����
		/// </summary>
		[System.Obsolete("�������� ����ΪMedNO����",true)]
		public string MedID 
		{
			get	{ return  myMedID;}
			set	{  myMedID = value; }
		}


		/// <summary>
		/// ������
		/// </summary>
		[System.Obsolete("�������� ����ΪRecipeNO����",true)]
		public string RecipeNo 
		{
			get	{ return  myRecipeNo;}
			set	{  myRecipeNo = value; }
		}


		/// <summary>
		/// ���������
		/// </summary>
		[System.Obsolete("�������� ����ΪSequenceNO����",true)]
		public int SequenceNo 
		{
			get	{ return  mySequenceNo;}
			set	{  mySequenceNo = value; }
		}



		/// <summary>
		/// �����������
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�ɻ�����Operation���Ի�ȡ",true)]
		public decimal ApplyNum 
		{
			get	{ return  myApplyNum;}
			set	{  myApplyNum = value; }
		}


		/// <summary>
		/// ��������˱���
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�ɻ�����Operation���Ի�ȡ",true)]
		public string ApplyOperCode 
		{
			get	{ return  myApplyOperCode;}
			set	{  myApplyOperCode = value; }
		}


		/// <summary>
		/// �����������
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�ɻ�����Operation���Ի�ȡ",true)]
		public DateTime ApplyDate 
		{
			get	{ return  myApplyDate;}
			set	{  myApplyDate = value; }
		}


		/// <summary>
		/// ������������ҩ��
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�ɻ�����Operation���Ի�ȡ",true)]
		public decimal ExamNum 
		{
			get	{ return  myExamNum;}
			set	{  myExamNum = value; }
		}


		/// <summary>
		/// �����˱��루��ҩ��
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�ɻ�����Operation���Ի�ȡ",true)]
		public string ExamOperCode 
		{
			get	{ return  myExamOperCode;}
			set	{  myExamOperCode = value; }
		}


		/// <summary>
		/// �������ڣ���ҩ��
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�ɻ�����Operation���Ի�ȡ",true)]
		public DateTime ExamDate 
		{
			get	{ return  myExamDate;}
			set	{  myExamDate = value; }
		}


		/// <summary>
		/// ��׼�˱���
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�ɻ�����Operation���Ի�ȡ",true)]
		public string ApproveOperCode 
		{
			get	{ return  myApproveOperCode;}
			set	{  myApproveOperCode = value; }
		}


		/// <summary>
		/// ��׼����
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�ɻ�����Operation���Ի�ȡ",true)]
		public DateTime ApproveDate 
		{
			get	{ return  myApproveDate;}
			set	{  myApproveDate = value; }
		}


		/// <summary>
		/// �˿�����
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�ɻ�����Operation���Ի�ȡ",true)]
		public decimal ReturnNum 
		{
			get	{ return  myReturnNum;}
			set	{  myReturnNum = value; }
		}


		#endregion

	}
}
