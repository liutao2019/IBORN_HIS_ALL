using System;

namespace FS.HISFC.Models.Pharmacy 
{
	/// <summary>
	/// [��������: ҩƷ����������Ϣ��]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2004-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��˹'
	///		�޸�ʱ��='2006-09-13'
	///		�޸�Ŀ��='ϵͳ�ع�'
	///		�޸�����='�����淶���� �̳���StorageBase����'
	///  />
	///  ID ���۵���ˮ��
	/// </summary>
    [Serializable]
    public class AdjustPrice : StorageBase 
	{

		public AdjustPrice () 
		{
			this.PrivType = "0304";	//����Ȩ�ޱ���

            this.Class2Type = "0304";
		}


		#region ����

		private System.String myFileNo ;	

		private System.Decimal myAfterRetailPrice ;

        private System.Decimal myAfterPurhancePrice;

		private System.Decimal myAfterWholesalePrice ;

		private System.String myProfitFlag ;

		private System.DateTime myInureTime ;

		private System.String myOperName;

        private System.String myAdjustPriceType;

        /// <summary>
        /// ���ۺ�����
        /// </summary>
        private System.Decimal myAfterRetailPrice2;

        /// <summary>
        /// �Ƿ�ҩ�ⵥ�Ƶ���
        /// </summary>
        private bool isDDAdjust = false;

        /// <summary>
        /// �Ƿ�ҩ�����Ƶ���
        /// </summary>
        private bool isDSAdjust = false;

		#endregion

		/// <summary>
		/// �������ݺ�
		/// </summary>
		public string FileNO
		{
			get	
			{
				return  myFileNo;
			}
			set	
			{  
				myFileNo = value;
			}
		}

		/// <summary>
		/// ���ۺ����ۼ�
		/// </summary>
		public Decimal AfterRetailPrice 
		{
			get	{
				return  myAfterRetailPrice;
			}
			set	{
				myAfterRetailPrice = value; 
			}
		}

        /// <summary>
        /// ���ۺ����
        /// </summary>
        public Decimal AfterPurhancePrice
        {
            get
            {
                return myAfterPurhancePrice;
            }
            set
            {
                myAfterPurhancePrice = value;
            }
        }

		/// <summary>
		/// ���ۺ�������
		/// </summary>
		public Decimal AfterWholesalePrice 
		{
			get	
			{
				return  myAfterWholesalePrice;
			}
			set	
			{
				myAfterWholesalePrice = value;
			}
		}

		/// <summary>
		/// ӯ�����1-ӯ��0-��
		/// </summary>
		public string ProfitFlag 
		{
			get	
			{
				return  myProfitFlag;
			}
			set	
			{
				myProfitFlag = value; 
			}
		}

        /// <summary>
        /// �������ͣ�0Ĭ�� 1���ۼ۵��� 2���ε���
        /// </summary>
        public string AdjustPriceType
        {
            get
            { 
                return myAdjustPriceType; 
            }
            set
            {
                myAdjustPriceType = value;
            }
        }

        /// <summary>
        /// ���ۺ�����
        /// </summary>
        public Decimal AfterRetailPrice2
        {
            get
            {
                return myAfterRetailPrice2;
            }
            set
            {
                myAfterRetailPrice2 = value;
            }
        }

		/// <summary>
		/// ������Чʱ��
		/// </summary>
		public DateTime InureTime 
		{
			get	
			{
				return  myInureTime;
			}
			set	
			{
				myInureTime = value; 
			}
		}

        /// <summary>
        /// �Ƿ�ҩ�ⵥ�Ƶ���
        /// </summary>
        public bool IsDDAdjust
        {
            get
            {
                return this.isDDAdjust;
            }
            set            
            {
                this.isDDAdjust = value;
            }
        }

        /// <summary>
        /// �Ƿ�ҩ�����Ƶ���
        /// </summary>
        public bool IsDSAdjust
        {
            get
            {
                return this.isDSAdjust;
            }
            set
            {
                this.isDSAdjust = value;
            }
        }

		#region ��Ч����
		
		/// <summary>
		/// ����������
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�����Operation����",true)]
		public string OperName 
		{
			get	{ return  myOperName;}
			set	{  myOperName = value; }
		}


		/// <summary>
		/// �������ݺ�
		/// </summary>
		[System.Obsolete("�����ع� ����ΪFileNO����",true)]
		public string FileNo 
		{
			get	
			{
				return  myFileNo;
			}
			set	{  myFileNo = value; }
		}


		#endregion
	}
}
