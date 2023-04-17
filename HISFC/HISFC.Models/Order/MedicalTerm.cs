using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Order
{
    /// <summary>
    /// Neusoft.HISFC.Object.Order.IatricalTerm<br></br>
    /// [��������: ҽ������ʵ��]<br></br>
    /// [�� �� ��: Sunm]<br></br>
    /// [����ʱ��: 2008-06-19]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class MedicalTerm : Neusoft.HISFC.Object.Fee.Item.Undrug
    {
        /// <summary>
		/// ���캯��
		/// </summary>
        public MedicalTerm()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
            this.ItemType = Neusoft.HISFC.Object.Base.EnumItemType.Term;
		}

        #region ����
        /// <summary>
        /// �Ƿ���ҽ��
        /// </summary>
        private bool isParent = false;
        /// <summary>
        /// �㷨������ҽ���㷨��
        /// </summary>
        private string arithmetic = string.Empty;

        /// <summary>
        /// ����
        /// </summary>
        private string mutex = string.Empty;

        /// <summary>
        /// ���Ｖ��
        /// </summary>
        private string termLevel = string.Empty;

        /// <summary>
        /// Լ��
        /// </summary>
        private string constraints = string.Empty;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private int reportTimeLimit = 0;

        /// <summary>
        /// �Ƿ���ҪҩƷ
        /// </summary>
        private bool isNeedDrug = false;

        #endregion

        #region ����
        /// <summary>
        /// �Ƿ���ҽ��
        /// </summary>
        public bool IsParent
        {
            get
            {
                return this.isParent;
            }
            set
            {
                this.isParent = value;
            }
        }

        /// <summary>
        /// �㷨������ҽ���㷨��
        /// </summary>
        public string Arithmetic
        {
            get 
            {
                return this.arithmetic;
            }
            set 
            {
                this.arithmetic = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Mutex
        {
            get 
            {
                return this.mutex;
            }
            set 
            {
                this.mutex = value;
            }
        }

        /// <summary>
        /// ���Ｖ��
        /// </summary>
        public string TermLevel
        {
            get 
            {
                return this.termLevel;
            }
            set
            {
                this.termLevel = value;
            }
        }
        
        /// <summary>
        /// Լ��
        /// </summary>
        public string Constraints
        {
            get 
            {
                return this.constraints;
            }
            set 
            {
                this.constraints = value;
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public int ReportTimeLimit
        {
            get { return reportTimeLimit; }
            set { reportTimeLimit = value; }
        }


        /// <summary>
        /// �Ƿ���ҪҩƷ
        /// </summary>
        public bool IsNeedDrug
        {
            get { return isNeedDrug; }
            set { isNeedDrug = value; }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>��ǰ��ʵ���ĸ���</returns>
        public new MedicalTerm Clone()
        {
            MedicalTerm obj = base.Clone() as MedicalTerm;
            
            return obj;
        }

        #endregion
    }
}
