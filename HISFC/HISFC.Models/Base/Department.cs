using System.Collections;
namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// Department<br></br>
	/// [��������: ����ʵ��]<br></br>
	/// [�� �� ��: ����ȫ]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class Department : Spell, ISort,IValidState
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Department()
		{
		}


		#region ����

	

		/// <summary>
		/// ��������
		/// </summary>
        private Base.DepartmentTypeEnumService deptType;
				
		/// <summary>
		/// Ӣ������
		/// </summary>
        private string englishName;

		/// <summary>
		/// ������ұ��
		/// </summary>
	    private string specialFlag;

		/// <summary>
		/// �����    
		/// </summary>
        private int sortID;

		/// <summary>
		/// ��Ч��״̬
		/// </summary>
        private Base.EnumValidState validState;

		/// <summary>
		/// �Ƿ�ͳ�ƿ���
		/// </summary>
        private bool isStatDept;

		/// <summary>
		/// �Ƿ�Һſ���
		/// </summary>
        private bool isRegDept;

		/// <summary>
		/// ���Ҽ��
		/// </summary>
        private string shortName;

        //{335827AE-42F6-4cbd-A73F-1B900B070E74}
        /// <summary>
        /// Ժ��ID
        /// </summary>
        private string hospitalID;


        //{335827AE-42F6-4cbd-A73F-1B900B070E74}
        /// <summary>
        /// Ժ������
        /// </summary>
        private string hospitalName;

		#endregion

		#region ����

		/// <summary>
		/// ���Ҽ��
		/// </summary>
        public string ShortName {
            get
            {
                return shortName;
            }
            set
            {
                shortName = value;
            }
        }

		/// <summary>
		/// Ӣ������
		/// </summary>
		public string EnglishName
		{
			get
			{
				return this.englishName;
			}
			set
			{
				this.englishName = value;
			}
		}
        
        /// <summary>
        /// ��������
        /// </summary>
        public DepartmentTypeEnumService DeptType
        {
            get
            {
                if (deptType == null)
                {
                    deptType = new DepartmentTypeEnumService();
                }
                return deptType; 
            }
            set
            { 
                deptType = value;
            }
        }
		
        /// <summary>
        /// �Ƿ�Һſ���
        /// </summary>
        public bool IsRegDept 
        {
            get
            { 
                return isRegDept; 
            }
            set
            { 
                isRegDept = value;
            }
        }

		/// <summary>
		/// �Ƿ�ͳ�ƿ���
		/// </summary>
        public bool IsStatDept 
		{
            get
            { 
                return isStatDept;
            }
            set
            { 
                isStatDept = value;
            }
        }

        /// <summary>
        /// ������ұ�ʶ
        /// </summary>
        public string SpecialFlag
        {
            get
            { 
                return this.specialFlag; 
            }
            set
            { 
                this.specialFlag = value; 
            }
        }

        [System.Obsolete("�Ѹ���ΪSpecialFlag", true)]
        public string DeptPro
        {
            get
            {
                return null;
            }
        }
		/// <summary>
		/// ��Ч��״̬
		/// </summary>
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

        //{335827AE-42F6-4cbd-A73F-1B900B070E74}
        public string HospitalID
        {
            get
            {
                return this.hospitalID;
            }
            set
            {
                this.hospitalID = value;
            }
        }

        //{335827AE-42F6-4cbd-A73F-1B900B070E74}
        public string HospitalName
        {
            get
            {
                return this.hospitalName;
            }
            set
            {
                this.hospitalName = value;
            }
        }

		#endregion
		
		#region ����

		#region �˽�
       
		/// <summary>
        /// ��¡����
        /// </summary>
        /// <returns>���ص�ǰʵ���ĸ���</returns>
        public new Department Clone()
        {
            Department department = base.Clone() as Department;

            return department; 
        }

		#endregion

		#endregion

		#region ISort ��Ա

		/// <summary>
		/// ����ID
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
    }
}
