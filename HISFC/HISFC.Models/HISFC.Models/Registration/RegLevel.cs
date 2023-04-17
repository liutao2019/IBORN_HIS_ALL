using System;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Registration
{
    /// <summary>
    /// <br>RegLevel</br>
    /// <br>[��������: �Һż���ʵ��]</br>
    /// <br>[�� �� ��: ��С��]</br>
    /// <br>[����ʱ��: 2007-2-1]</br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class RegLevel:FS.HISFC.Models.Base.Spell,IValid,ISort
	{
        /// <summary>
        /// �Һż��� ID���� NAME����
        /// </summary>
		public RegLevel() 
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
        }

        #region ����
        
        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        private bool isValid = true;

        /// <summary>
        /// �Ƿ�ר�Һ�
        /// </summary>
        private bool isExpert = false;

        /// <summary>
        /// �Ƿ�ר�ƺ�
        /// </summary>
        private bool isFaculty = false;

        /// <summary>
        /// �Ƿ������
        /// </summary>
        private bool isSpecial = false;

        /// <summary>
        /// �Ƿ�Ĭ�Ϻ�
        /// </summary>
        private bool isDefault = false;

        /// <summary>
        /// ��ʾ˳��
        /// </summary>
        private int sortID;

        /// <summary>
        /// �Ƿ���{156C449B-60A9-4536-B4FB-D00BC6F476A1}
        /// </summary>
        private bool isEmergency = false;


        /// <summary>
        /// ��������
        /// </summary>
        private Base.OperEnvironment oper = new OperEnvironment();

        #endregion

        #region ����

        /// <summary>
		/// �Ƿ�ר�Һ�
		/// </summary>
        public bool IsExpert
        {
            get
            {
                return this.isExpert;
            }
            set
            {
                this.isExpert = value;
            }
        }

		/// <summary>
		/// �Ƿ�ר�ƺ�
		/// </summary>
		public bool IsFaculty 
        {
            get
            {
                return this.isFaculty;
            }
            set
            {
                this.isFaculty = value;

            }
        }

		/// <summary>
		/// �Ƿ������
		/// </summary>
        public bool IsSpecial
        {
            get
            {
                return this.isSpecial;
            }
            set
            {
                this.isSpecial = value;
            }
        }

		/// <summary>
		/// �Ƿ�Ĭ�Ϻ�
		/// </summary>
        public bool IsDefault
        {
            get
            {
                return this.isDefault;
            }
            set
            {
                this.isDefault = value;
            }
        }

        /// <summary>
        /// ����������������Ա��ʱ��
        /// </summary>
        public Base.OperEnvironment Oper 
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
        /// �Ƿ���{156C449B-60A9-4536-B4FB-D00BC6F476A1}
        /// </summary>
        public bool IsEmergency
        {
            get { return isEmergency; }
            set { isEmergency = value; }
        }


        #endregion

        #region ����
        /// <summary>
		/// ICloneable
		/// </summary>
		/// <returns></returns>
		public new FS.HISFC.Models.Registration.RegLevel Clone ()
		{
            RegLevel regLevel = base.Clone() as RegLevel;

            regLevel.Oper = this.Oper.Clone();

            return regLevel;
        }
        #endregion

        #region �ӿ�ʵ��

        #region IValid ��Ա
        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
            set
            {
                this.isValid = value;
            }
        }
        #endregion
        #endregion

        #region ISort ��Ա
        /// <summary>
        /// ��ʾ˳��
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

        #region ����        

        /// <summary>
        /// ����Ա
        /// </summary>
        [Obsolete("����,�Ѿۺ���OperEnvironment������OperId", true)]
        public string OperID = "";

        /// <summary>
        /// ����ʱ��
        /// </summary>
        [Obsolete("����,�Ѿۺ���OperEnvironment������OperDate", true)]
        public DateTime OperDate = DateTime.MinValue;

        #endregion       
    }
}
