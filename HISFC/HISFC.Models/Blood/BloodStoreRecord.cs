using System;
using System.Collections.Generic;
using System.Text;
using Neusoft.HISFC.Object.Base;
namespace Neusoft.HISFC.Object.Blood
{
    /// <summary>
    /// [��������: �洢����]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-4-18]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// 
    /// </summary>
    public class BloodStoreRecord : Spell, ISort, IValid
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public BloodStoreRecord()
        {
        }

        #region �ֶ�

        /// <summary>
        /// ISort
        /// </summary>
        private int iSort;

        /// <summary>
        /// IValid
        /// </summary>
        private bool iValid;

        /// <summary>
        /// ��Ѫ״̬
        /// </summary>
        private bool isMatched;

        /// <summary>
        /// ��Ѫ��
        /// </summary>
        private string bloodStoreId;

        /// <summary>
        /// �̳���BloodApply������
        /// </summary>
        private Neusoft.HISFC.Object.Blood.BloodApply bloodApply = new BloodApply();

        /// <summary>
        /// �̳���BloodOutputRecord������
        /// </summary>
        private Neusoft.HISFC.Object.Blood.BloodInputRecord bloodInputRecord = new BloodInputRecord();

        /// <summary>
        /// ����Ա������ʱ��
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment bloodStoreOperator = new OperEnvironment();

        #endregion

        #region ����

 

        /// <summary>
        /// ��Ѫ��
        /// </summary>
        public string BloodStoreId
        {
            get { return bloodStoreId; }
            set { bloodStoreId = value; }
        }

        /// <summary>
        /// ����Ա������ʱ��
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment BloodStoreOperator
        {
            get { return bloodStoreOperator; }
            set { bloodStoreOperator = value; }
        }

        /// <summary>
        /// �̳���BloodInputRecord������
        /// </summary>
        public Neusoft.HISFC.Object.Blood.BloodInputRecord BloodInputRecord
        {
            get { return bloodInputRecord; }
            set { bloodInputRecord = value; }
        }

        /// <summary>
        /// �̳���BloodApply������
        /// </summary>
        public Neusoft.HISFC.Object.Blood.BloodApply BloodApply
        {
            get { return bloodApply; }
            set { bloodApply = value; }
        }

        /// <summary>
        /// ��Ѫ״̬
        /// </summary>
        public bool IsMatched
        {
            get { return isMatched; }
            set { isMatched = value; }
        }

        #endregion

        #region ISort ��Ա

        public int SortID
        {
            get
            {
                return iSort;
            }
            set
            {
                this.iSort = value;
            }
        }

        #endregion

        #region IValid ��Ա

        public bool IsValid
        {
            get
            {
                return iValid;
            }
            set
            {
                this.iValid = value;
            }
        }

        #endregion

        #region ��¡
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new BloodStoreRecord Clone()
        {
            BloodStoreRecord bloodStoreRecord = base.Clone() as BloodStoreRecord;

            bloodStoreRecord.BloodInputRecord = this.BloodInputRecord.Clone();

            return bloodStoreRecord;
        }
        #endregion

    }
}
