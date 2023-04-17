using System;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// Const<br></br>
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
    public class Const : Spell, Base.ISort, Base.IValid
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public Const()
        {
        }


        #region ����

        /// <summary>
        /// ����
        /// </summary>
        protected int sortID;

        /// <summary>
        /// ������Ϣ
        /// </summary>				
        private OperEnvironment operEnvironment;

        /// <summary>
        /// ��Ч��
        /// </summary>
        private bool isValid;								//��Ч��


        #endregion

        #region ����

        /// <summary>
        /// ��������
        /// </summary>
        public OperEnvironment OperEnvironment
        {
            get
            {
                if (operEnvironment == null)
                {
                    operEnvironment = new OperEnvironment();
                }
                return this.operEnvironment;
            }
            set
            {
                this.operEnvironment = value;
            }
        }



        #endregion

        #region ����

        #region ��¡
        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns>Const��ʵ��</returns>
        public new Const Clone()
        {
            return this.MemberwiseClone() as Const;
        }
        #endregion

        #endregion

        #region ISort ��Ա

        /// <summary>
        /// ����
        /// </summary>
        public int SortID
        {
            get
            {
                return this.sortID;
            }
            set
            {
                sortID = value;
            }
        }

        #endregion

        #region IValid ��Ա
        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                isValid = value;
            }
        }

        #endregion
    }


	
}
