using System;
using FS.FrameWork.Models;


namespace FS.HISFC.Models.HealthRecord
{
    /// <summary>
    /// ICDCompare�������<br></br>
    /// [��������: ICDCompare]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-04-2]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class ICDCompare : NeuObject, FS.HISFC.Models.Base.IValid
    {
        public ICDCompare()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region  ˽�б���
        private ICD icd10 = new ICD(); //����ICD10��Ϣ
        private ICD icd9 = new ICD();  //����ICD9��Ϣ
        private FS.HISFC.Models.Base.OperEnvironment operInfo = new FS.HISFC.Models.Base.OperEnvironment();  //����Ա��Ϣ, ID ���� Name ����
        private bool isValid; //��Ч�Ա�ʶ

        #endregion

        #region  ��������


        public ICD ICD10
        {
            //����ICD10��Ϣ
            get
            {
                return icd10;
            }
            set
            {
                icd10 = value;
            }
        }
        public ICD ICD9
        {
            //����ICD9��Ϣ
            get
            {
                return icd9;
            }
            set
            {
                icd9 = value;
            }
        }
        public FS.HISFC.Models.Base.OperEnvironment OperInfo
        {
            //������ ��Ϣ
            get
            {
                return operInfo;
            }
            set
            {
                operInfo = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new ICDCompare Clone()
        {
            //��¡����
            ICDCompare icdCompare = base.Clone() as ICDCompare; // ��¡����
            icdCompare.operInfo = operInfo.Clone(); //��¡
            icdCompare.icd10 = icd10.Clone(); //��¡CD10��Ϣ ����Ա��Ϣ
            icdCompare.icd9 = icd9.Clone(); //��¡CD9��Ϣ
            return icdCompare;
        }
        #endregion

        #region IValid ��Ա

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

        #region ����
        [Obsolete("���� ��OperInfo.OperTime����")]
        private DateTime operDate;                   //¼��ʱ��
        [Obsolete("���� ��OperInfo.OperTime����", true)]
        public DateTime OperDate
        {
            //����ʱ��
            get
            {
                return operDate;
            }
            set
            {
                operDate = value;
            }
        }
        #endregion
    }
}
