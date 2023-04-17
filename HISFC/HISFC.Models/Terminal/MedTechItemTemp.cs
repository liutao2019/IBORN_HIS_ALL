using System;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Terminal
{
	/// <summary>
	/// MedTechItemTemp <br></br>
	/// [��������: ҽ��ԤԼ�Ű���Ϣ]<br></br>
	/// [�� �� ��: sunxh]<br></br>
	/// [����ʱ��: 2005-3-3]<br></br>
	/// <˵��>
    ///     1��  {F8383442-78B0-40c2-B906-50BA52ADB139}  ����ʵ������ ��ʼʱ�䡢����ʱ�䡢ִ���豸
    /// </˵��>
	/// </summary>
    [Serializable]
    public class MedTechItemTemp : FS.FrameWork.Models.NeuObject
	{
		public MedTechItemTemp()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ������
		
		/// <summary>
		/// ����
		/// </summary>
		FS.HISFC.Models.Base.Department dept = new Department();
		
		/// <summary>
		/// ����
		/// </summary>
		string week;
		
		/// <summary>
		/// ���
		/// </summary>
		string noonCode;
		
		/// <summary>
		/// ԤԼ�޶�
		/// </summary>
		decimal bookLmt;
		
		/// <summary>
		/// ����ԤԼ�޶�
		/// </summary>
		decimal specialBookLmt;
		
		/// <summary>
		/// �ն�ȷ����
		/// </summary>
		private int conformNum;

        /// <summary>
        /// ��ʶλ
        /// </summary>
        private string tmpFlag;
       
		/// <summary>
		/// ҽ��ԤԼ��Ŀ��Ϣ
		/// </summary>
		private MedTechItem medTechItem = new MedTechItem();

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private string startTime = new DateTime().ToLongTimeString();

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private string endTime = new DateTime().ToLongTimeString();

        /// <summary>
        /// ִ���豸
        /// </summary>
        private FS.FrameWork.Models.NeuObject machine = new FS.FrameWork.Models.NeuObject();

		#endregion

		#region ����

		/// <summary>
		/// ҽ��ԤԼ��Ŀ��Ϣ
		/// </summary>
		public MedTechItem MedTechItem
		{
			get
			{
				return this.medTechItem;
			}
			set
			{
				this.medTechItem = value;
			}
		}

		/// <summary>
		/// �ն�ȷ����
		/// </summary>
		public int ConformNum
		{
			get
			{
				return conformNum;
			}
			set
			{
				conformNum = value;
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		public FS.HISFC.Models.Base.Department Dept
		{
			get
			{
				return this.dept;
			}
			set
			{
				this.dept = value;
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		public string Week
		{
			get
			{
				return week;
			}
			set
			{
				week = value;
			}
		}

		/// <summary>
		/// ���
		/// </summary>
		public string NoonCode
		{
			get
			{
				return noonCode;
			}
			set
			{
				noonCode = value;
			}
		}

		/// <summary>
		/// ԤԼ�޶�
		/// </summary>
		public decimal BookLmt
		{
			get
			{
				return bookLmt;
			}
			set
			{
				bookLmt = value;
			}
		}

		/// <summary>
		/// ����ԤԼ�޶�
		/// </summary>
		public decimal SpecialBookLmt
		{
			get
			{
				return specialBookLmt;
			}
			set
			{
				specialBookLmt = value;
			}
		}
        /// <summary>
        /// ��ʶλ
        /// </summary>
        public string TmpFlag
        {
            get
            {
                return tmpFlag;
            }
            set
            {
                tmpFlag = value;
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public string EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public string StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
            }
        }

        /// <summary>
        /// ִ���豸
        /// </summary>
        public FS.FrameWork.Models.NeuObject Machine
        {
            get
            {
                return machine;
            }
            set
            {
                machine = value;
            }
        }
		#endregion

		#region ��ʱ

		/// <summary>
		/// ��������
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪDept", true)]
		string deptName;

		/// <summary>
		/// ��������
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪDept", true)]
		public string DeptName
		{
			get
			{
				return this.dept.Name;
			}
			set
			{
				this.dept.Name = value;
			}
		}
		
		#endregion

		#region ����

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>ҽ��ԤԼ�Ű���Ϣ</returns>
		public new MedTechItemTemp Clone()
		{
			MedTechItemTemp medTechItemTemp = base.Clone() as MedTechItemTemp;

			medTechItemTemp.MedTechItem = this.MedTechItem.Clone();
			medTechItemTemp.Dept = this.Dept.Clone();
            medTechItemTemp.Machine = this.Machine.Clone();

			return medTechItemTemp;
		}

		#endregion
	}
}
