using System;

namespace FS.HISFC.Models.Pharmacy
{
	/// <summary>
	/// [��������: ҩƷ�̵���]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2004-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���='������'
	///		�޸�ʱ��='2006-09-12'
	///		�޸�Ŀ��='ϵͳ�ع�'
	///		�޸�����='�����淶���� �̳���StorageBase����'
	///  />
	///  ID			�̵���ˮ��
	///  GroupNO	����
	///  State		�̵�״̬:0����,1���,2ȡ��
	/// </summary>
    [Serializable]
    public class Check : StorageBase
	{
		public Check()
		{
			this.PrivType = "0306";	//�̵���Ȩ�ޱ���

            this.Class2Type = "0306";
		}


		#region ����

		private string myCheckCode;		//�̵㵥��
        private string myCheckName;     //�̵㵥����
		private decimal	myFStoreNum;	//��������
		private decimal myAdjustNum;	//�̵�����
		private decimal myCStoreNum;	//�������
		private decimal myMinNum;		//��С����
		private	decimal myPackNum;		//��װ����
		private decimal myProfitLossNum;//ӯ������
		private string myProfitStatic;	//ӯ����� 0 �̿�  1 ��ӯ 2 ��ӯ��
		private string myQualityFlag;	//������� 0 ��	   1 ����
		private bool isAdd;			//����ҩƷ���  0 ������  1 ����
		private string myDisposeWay;	//����ʽ
		private string myCheckState;	//�̵�״̬:0����,1���,2ȡ��

		/// <summary>
		/// ���ʲ�����Ϣ
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment fOper = new FS.HISFC.Models.Base.OperEnvironment();

		/// <summary>
		/// ��������Ϣ
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment cOper = new FS.HISFC.Models.Base.OperEnvironment();

		#endregion
		
		/// <summary>
		/// �̵㵥��
		/// </summary>
		public string CheckNO
		{
			get 
			{
				return myCheckCode;
			}
			set 
			{
				myCheckCode = value;
			}
		}

        /// <summary>
        ///  �̵㵥����
        /// </summary>
        public string CheckName
        {
            get
            {
                return myCheckName;
            }
            set
            {
                myCheckName = value;
                base.Name = value;
            }
        }


		/// <summary>
		/// ��������
		/// </summary>
		public decimal FStoreQty
		{
			get 
			{
				return myFStoreNum;
			}
			set 
			{
				myFStoreNum = value;
			}
		}


		/// <summary>
		/// �̵�����
		/// </summary>
		public decimal AdjustQty 
		{
			get 
			{
				return myAdjustNum;
			}
			set 
			{
				myAdjustNum = value;
			}
		}


		/// <summary>
		/// �������
		/// </summary>
		public decimal CStoreQty
		{
			get 
			{
				return myCStoreNum;
			}
			set 
			{
				myCStoreNum = value;
			}
		}


		/// <summary>
		/// ��С����
		/// </summary>
		public decimal MinQty
		{
			get 
			{
				return myMinNum;
			}
			set 
			{
				myMinNum = value;
			}
		}


		/// <summary>
		/// ��װ����
		/// </summary>
		public decimal PackQty 
		{
			get 
			{
				return myPackNum;
			}
			set 
			{
				myPackNum = value;
			}
		}


		/// <summary>
		/// ӯ�����
		/// </summary>
		public decimal ProfitLossQty
		{
			get 
			{
				return myProfitLossNum;
			}
			set 
			{
				myProfitLossNum = value;
			}
		}


		/// <summary>
		/// ӯ����� 0 �̿� 1 ��ӯ 2 ��ӯ��
		/// </summary>
		public string ProfitStatic
		{
			get 
			{
				return myProfitStatic;
			}
			set 
			{
				myProfitStatic = value;
			}
		}


		/// <summary>
		/// �������
		/// </summary>
		public string QualityFlag
		{
			get 
			{
				return myQualityFlag;
			}
			set 
			{
				myQualityFlag = value;
			}
		}


		/// <summary>
		/// �Ƿ񸽼�ҩƷ
		/// </summary>
		public bool IsAdd
		{
			get 
			{
				return isAdd;
			}
			set 
			{
				isAdd = value;
			}
		}


		/// <summary>
		/// ����ʽ
		/// </summary>
		public string DisposeWay
		{
			get 
			{
				return myDisposeWay;
			}
			set 
			{
				myDisposeWay = value;
			}
		}


		/// <summary>
		/// ���ʲ�����Ϣ
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment FOper
		{
			get
			{
				return this.fOper;
			}
			set
			{
				this.fOper = value;
			}
		}


		/// <summary>
		/// ��������Ϣ
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment COper
		{
			get
			{
				return this.cOper;
			}
			set
			{
				this.cOper = value;
			}
		}


		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>�ɹ����ص�ǰʵ���ĸ���</returns>
		public new Check Clone()
		{
			Check check = base.Clone() as Check;

			check.FOper = this.FOper.Clone();
			check.COper = this.COper.Clone();

			return check;
		}


		#endregion

		#region ��Ч����

		
		private string myFOperCode;		//������
		private DateTime myFOperDate;	//����ʱ��
		private string myCOperCode;		//�����
		private DateTime myCOperDate;	//���ʱ��

		/// <summary>
		/// �̵㵥��
		/// </summary>
		[System.Obsolete("�������� ����ΪCheckNO����",true)]
		public string CheckCode 
		{
			get {return myCheckCode;}
			set {myCheckCode = value;}
		}


		/// <summary>
		/// �̵�״̬:0����,1���,2ȡ��
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�����ڵ�State����",true)]
		public string CheckState 
		{
			get {return myCheckState;}
			set {myCheckState = value;}
		}


		/// <summary>
		/// ��������
		/// </summary>
		[System.Obsolete("�������� ����ΪFStoreQty����",true)]
		public decimal FStoreNum
		{
			get {return myFStoreNum;}
			set {myFStoreNum = value;}
		}


		/// <summary>
		/// �̵�����
		/// </summary>
		[System.Obsolete("�������� ����ΪAdjustQty����",true)]
		public decimal AdjustNum 
		{
			get {return myAdjustNum;}
			set {myAdjustNum = value;}
		}


		/// <summary>
		/// �������
		/// </summary>
		[System.Obsolete("�������� ����ΪCStoreQty����",true)]
		public decimal CStoreNum
		{
			get {return myCStoreNum;}
			set {myCStoreNum = value;}
		}


		/// <summary>
		/// ��С����
		/// </summary>
		[System.Obsolete("�������� ����ΪMinQty����",true)]
		public decimal MinNum
		{
			get {return myMinNum;}
			set {myMinNum = value;}
		}


		/// <summary>
		/// ��װ����
		/// </summary>
		[System.Obsolete("�������� ����ΪPackQty����",true)]
		public decimal PackNum 
		{
			get {return myPackNum;}
			set {myPackNum = value;}
		}


		/// <summary>
		/// ӯ�����
		/// </summary>
		[System.Obsolete("�������� ����ΪProfitLossQty����",true)]
		public decimal ProfitLossNum
		{
			get {return myProfitLossNum;}
			set {myProfitLossNum = value;}
		}


		/// <summary>
		/// ������
		/// </summary>
		[System.Obsolete("�������� ����ΪFOper����",true)]
		public string FOperCode
		{
			get {return myFOperCode;}
			set {myFOperCode = value;}
		}


		/// <summary>
		/// ����ʱ��
		/// </summary>
		[System.Obsolete("�������� ����ΪFOper����",true)]
		public DateTime FOperDate
		{
			get {return myFOperDate;}
			set {myFOperDate = value;}
		}


		/// <summary>
		/// �����(�����)
		/// </summary>
		[System.Obsolete("�������� ����COper����",true)]
		public string COperCode
		{
			get {return myCOperCode;}
			set {myCOperCode = value;}
		}


		/// <summary>
		/// ���ʱ��(���ʱ��)
		/// </summary>
		[System.Obsolete("�������� ����COper����",true)]
		public DateTime COperDate
		{
			get {return myCOperDate;}
			set {myCOperDate = value;}
		}


		#endregion
	}
}
