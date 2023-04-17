using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee
{
	/// <summary>
	/// BedFeeItemInfo<br></br>
	/// [��������: ��λ������ ID:��Ŀ���� Name:��Ŀ����]<br></br>
	/// [�� �� ��: ��˹]<br></br>
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
    [System.ComponentModel.DisplayName("��λ�̶�����")]
	public class BedFeeItem	: NeuObject, ISort,IValidState
	{
		#region ����

        /// <summary>
        /// ������
        /// </summary>
        private string primaryKey;

		/// <summary>
		/// ���õȼ�����
		/// </summary>
		private string feeGradeCode;
		
		/// <summary>
		/// ����
		/// </summary>
		private decimal qty;
		
		/// <summary>
		/// �Ʒѿ�ʼʱ��
		/// </summary>
		private DateTime beginTime;
		
		/// <summary>
		/// �Ʒѽ���ʱ��
		/// </summary>
		private DateTime endTime;
		
		/// <summary>
		/// �Ƿ���Ӥ���й�
		/// </summary>
		private bool isBabyRelation;
		
		/// <summary>
		/// �Ƿ���ʱ���й�
		/// </summary>
		private bool isTimeRelation;

		/// <summary>
		/// ��Ч�Ա�ʶ 0 ���� 1 ͣ�� 2 ����
		/// </summary>
		private Base.EnumValidState validState;
		
		/// <summary>
		/// ��չ���(����Ժ�����Ƿ�Ʒ�0���Ʒ�,1�Ʒ�---�������,�Ҵ�)
		/// </summary>
		private string extendFlag;
		
		/// <summary>
		/// �����
		/// </summary>
		private int sortID;

        private bool isOutFeeFlag = true;

        private decimal price;
        private decimal oldPrice;
        private string patientID = string.Empty;
        private string bedNO = string.Empty;

		#endregion

		#region ����

        /// <summary>
        /// ������
        /// </summary>
        public string PrimaryKey
        {
            get { return primaryKey; }
            set { primaryKey = value; }
        }

		
		/// <summary>
		/// ���õȼ�����
		/// </summary>
		public string FeeGradeCode
		{
			get
			{
				return this.feeGradeCode;	
			}
			set
			{
				this.feeGradeCode = value;
			}
		}
		
		/// <summary>
		/// ����
		/// </summary>
        [System.ComponentModel.DisplayName("����")]
        [System.ComponentModel.Description("��λ��������")]
		public decimal Qty
		{
			get
			{
				return this.qty;
			}
			set
			{
				this.qty = value;
			}
		}
		
		/// <summary>
		/// �Ʒѿ�ʼʱ��
		/// </summary>
        [System.ComponentModel.DisplayName("�Ʒѿ�ʼʱ��")]
        [System.ComponentModel.Description("�Ʒѿ�ʼʱ��")]
		public DateTime BeginTime
		{	
			get
            {
                if (beginTime != null)
                {
                    return this.beginTime;
                }
                else
                {
                    return DateTime.MinValue;
                }

			}
			set
			{
				this.beginTime = value;
			}
		}

		/// <summary>
		/// �Ʒѽ���ʱ��
		/// </summary>
        [System.ComponentModel.DisplayName("�Ʒѽ���ʱ��")]
        [System.ComponentModel.Description("�Ʒѽ���ʱ��")]
		public DateTime EndTime
		{
			get
			{
                if (endTime != null)
                {
                    return this.endTime;
                }
                else
                {
                    return DateTime.MinValue;
                }
				
			}
			set
			{
				this.endTime = value;
			}
		}
		
		/// <summary>
		/// �Ƿ���Ӥ���й�
		/// </summary>
		public bool IsBabyRelation
		{
			get
			{
				return this.isBabyRelation;
			}
			set
			{
				this.isBabyRelation = value;
			}
		}

		/// <summary>
		/// �Ƿ���ʱ���й�
		/// </summary>
		public bool IsTimeRelation
		{
			get
			{
				return this.isTimeRelation;
			}
			set
			{
				this.isTimeRelation = value;
			}
		}

		/// <summary>
		/// ��Ч�Ա�ʶ 0 ���� 1 ͣ�� 2 ����
		/// </summary>
        [System.ComponentModel.DisplayName("��Ч�Ա�ʶ")]
        [System.ComponentModel.Description("��Ч�Ա�ʶ")]
		public EnumValidState ValidState
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
		/// ��չ���(����Ժ�����Ƿ�Ʒ�0���Ʒ�,1�Ʒ�---�������,�Ҵ�)
		/// </summary>
		public string ExtendFlag
		{
			get
			{
				return this.extendFlag;
			}
			set
			{
				this.extendFlag = value;
			}
		}

        [System.ComponentModel.DisplayName("��Ժ�Ƿ���ȡ����")]
        [System.ComponentModel.Description("��Ժ�Ƿ���ȡ����")]
        public bool IsOutFeeFlag
        {
            get
            {
                return this.isOutFeeFlag;
            }
            set
            {
                this.isOutFeeFlag = value;
            }
        }

        /// <summary>
        /// �۸�
        /// </summary>
        [System.ComponentModel.DisplayName("�۸�")]
        [System.ComponentModel.Description("�۸�")]
        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }

        /// <summary>
        /// �۸�
        /// </summary>
        [System.ComponentModel.DisplayName("�۸�")]
        [System.ComponentModel.Description("�۸�")]
        public decimal OldPrice
        {
            get
            {
                return oldPrice;
            }
            set
            {
                oldPrice = value;
            }
        }

        /// <summary>
        /// ����ID
        /// </summary>
        [System.ComponentModel.DisplayName("����ID")]
        [System.ComponentModel.Description("����ID")]
        public string PatientID
        {
            get
            {
                return patientID;
            }
            set
            {
                patientID = value;
            }
        }

        /// <summary>
        /// ����ID
        /// </summary>
        [System.ComponentModel.DisplayName("���ߴ�λID")]
        [System.ComponentModel.Description("���ߴ�λID")]
        public string BedNO
        {
            get
            {
                return bedNO;
            }
            set
            {
                bedNO = value;
            }
        }

		#endregion

		#region ����
		/// <summary>
		/// ��Ŀ����
		/// </summary>
		[Obsolete("�Ѿ�����,ʹ�û����ID", true)]
		public string ItemCode;

		/// <summary>
		/// ��Ŀ����
		/// </summary>
		[Obsolete("�Ѿ�����,ʹ�û����Name", true)]
		public string ItemName;

		/// <summary>
		/// ����
		/// </summary>
		[Obsolete("�Ѿ�����,ʹ��Qty����", true)]
		public decimal Number;

		/// <summary>
		/// �Ʒѿ�ʼʱ��
		/// </summary>
		[Obsolete("�Ѿ�����,ʹ��BeginTime����", true)]
		public DateTime StartTime;

		/// <summary>
		/// ������ȡ�Ƿ��Ӥ���й�
		/// </summary>
		[Obsolete("�Ѿ�����,ʹ��IsBabyRelation����", true)]
		public bool HasRelationToBaby;

		/// <summary>
		/// ������ȡ�Ƿ��ʱ���й�
		/// </summary>
		[Obsolete("�Ѿ�����,ʹ��IsTimeRelation����", true)]
		public bool HasRelationToTime;

		/// <summary>
		/// ��չ���(����Ժ�����Ƿ�Ʒ�0���Ʒ�,1�Ʒ�---�������,�Ҵ�)
		/// </summary>
		[Obsolete("�Ѿ�����,ʹ��ExtendFlag����", true)]
		public string ExtFlag;	
	
		#endregion

		#region ����

		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ���ʵ������</returns>
		public new BedFeeItem Clone()
		{
			return base.Clone() as BedFeeItem;
		}
		
		#endregion

		#endregion

		#region �ӿ�ʵ��

		#region ISort ��Ա

		/// <summary>
		/// �����
		/// </summary>
		public int SortID
		{
			get
			{
				return this.sortID;
			}
			set
			{
				this.sortID = value;
			}
		}

		#endregion

		#endregion
	}
}
