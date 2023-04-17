using System;

namespace Neusoft.HISFC.Object.Order
{
    /// <summary>
    /// Neusoft.HISFC.Object.Order.IatricalTerm<br></br>
    /// [��������: ҽ������ʵ��]<br></br>
    /// [�� �� ��: Sunm]<br></br>
    /// [����ʱ��: 2008-06-24]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class TermMutex : Neusoft.NFC.Object.NeuObject
    {

        /// <summary>
		/// ���캯��
		/// </summary>
        public TermMutex()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

        #region ����

        /// <summary>
        /// ҽ���������
        /// </summary>
        private string termID = string.Empty;

        /// <summary>
        /// ҽ����������
        /// </summary>
        private string termName = string.Empty;

      
        /// <summary>
        /// ҽ���������
        /// </summary>
        private Neusoft.HISFC.Object.Base.SysClassEnumService termSysClass = new Neusoft.HISFC.Object.Base.SysClassEnumService();

        /// <summary>
        /// ����ҽ���������
        /// </summary>
        private string mutexTermID = string.Empty;

        /// <summary>
        /// ����ҽ����������
        /// </summary>
        private string mutexTermName = string.Empty;

        /// <summary>
        /// ����ҽ���������
        /// </summary>
        private Neusoft.HISFC.Object.Base.SysClassEnumService mutexSysClass = new Neusoft.HISFC.Object.Base.SysClassEnumService();

        /// <summary>
        /// ����Ա
        /// </summary>
        private Neusoft.NFC.Object.NeuObject oper = new Neusoft.NFC.Object.NeuObject();

        /// <summary>
        /// ��������
        /// </summary>
        private EnumMutex mutexType = EnumMutex.None;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime operDate = DateTime.MinValue;

        #endregion

        #region ����

        /// <summary>
        /// ҽ���������
        /// </summary>
        public string TermID 
        {
            get
            {
                return this.termID;
            }
            set 
            {
                this.termID = value;
            }
        }

        /// <summary>
        /// ҽ���������
        /// </summary>
        public Neusoft.HISFC.Object.Base.SysClassEnumService TermSysClass
        {
            get
            {
                return this.termSysClass;
            }
            set
            {
                this.termSysClass = value;
            }
        }

        /// <summary>
        /// ����ҽ���������
        /// </summary>
        public string MutexTermID
        {
            get
            {
                return this.mutexTermID;
            }
            set
            {
                this.mutexTermID = value;
            }
        }

        /// <summary>
        /// ����ҽ���������
        /// </summary>
        public Neusoft.HISFC.Object.Base.SysClassEnumService MutexSysClass
        {
            get
            {
                return this.mutexSysClass;
            }
            set
            {
                this.mutexSysClass = value;
            }
        }

        /// <summary>
        /// ����Ա
        /// </summary>
        public Neusoft.NFC.Object.NeuObject Oper
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

        public EnumMutex MutexType
        {
            get
            {
                return this.mutexType;
            }
            set
            {
                this.mutexType = value;
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime OperDate
        {
            get
            {
                return this.operDate;
            }
            set
            {
                this.operDate = value;
            }
        }

        /// <summary>
        /// ҽ����������
        /// </summary>
        public string TermName
        {
            get { return termName; }
            set { termName = value; }
        }


        /// <summary>
        /// ����ҽ����������
        /// </summary>
        public string MutexTermName
        {
            get { return mutexTermName; }
            set { mutexTermName = value; }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>��ǰ��ʵ���ĸ���</returns>
        public new TermMutex Clone()
        {
            TermMutex obj = base.Clone() as TermMutex;

            obj.MutexSysClass = this.mutexSysClass.Clone();
            obj.TermSysClass = this.termSysClass.Clone();
            obj.Oper = this.oper.Clone();

            return obj;
        }

        #endregion

    }
}
