using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;


namespace FS.HISFC.Models.HealthRecord
{


    /// <summary>
    /// ICD<br></br>
    /// [��������: ICD]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-04-3]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class ICD : Spell, IValid
    {

        public ICD()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ˽�б���
        /// <summary>
        /// //���
        /// </summary>
        private string seqNO;
        /// <summary>
        /// //ҽ�����Ĵ���
        /// </summary>
        private string siCode;
        /// <summary>
        /// //����ԭ��
        /// </summary>
        private string deadReason;
        /// <summary>
        /// //����������
        /// </summary>
        private string diseaseCode;
        /// <summary>
        /// //��׼סԺ��
        /// </summary>
        private int standardDays;
        /// <summary>
        /// //סԺ�ȼ�
        /// </summary>
        private string inpGrade;
        /// <summary>
        /// //�Ƿ�30�ּ��� True �� False ����
        /// </summary>
        private string is30Illness;
        /// <summary>
        /// //�Ƿ�Ⱦ�� True �� False ����
        /// </summary>
        private string isInfection;
        /// <summary>
        /// //�Ƿ�������� True �� False ����
        /// </summary>
        private string isTumour;
        /// <summary>
        /// //�Ƿ���Ч  True ��Ч False ����
        /// </summary>
        private bool isValid;
        /// <summary>
        /// //����,�������ô�����
        /// </summary>
        private string keyCode;
        private bool traditionalDiag;
        /// <summary>
        /// 	//����Ա��Ϣ ID ���� Name ����
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment operInfo = new OperEnvironment();
        /// <summary>
        /// //�����Ա� 
        /// </summary>
        private FS.FrameWork.Models.NeuObject sexType = new NeuObject();

        #endregion

        #region ����
        /// <summary>
        /// ��ҽ���
        /// </summary>
        public bool TraditionalDiag
        {
            get
            {
                return traditionalDiag;
            }
            set 
            {
                traditionalDiag = value;
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string SeqNo
        {
            get
            {
                return seqNO;
            }
            set
            {
                seqNO = value;
            }
        }

        /// <summary>
        /// ҽ�����Ĵ���
        /// </summary>
        public string SICode
        {
            get
            {
                return siCode;
            }
            set
            {
                siCode = value;
            }
        }

        /// <summary>
        /// ����ԭ��
        /// </summary>
        public string DeadReason
        {
            get
            {
                return deadReason;
            }
            set
            {
                deadReason = value;
            }
        }

        /// <summary>
        /// ���༲����
        /// </summary>
        public string DiseaseCode
        {
            get
            {
                return diseaseCode;
            }
            set
            {
                diseaseCode = value;
            }
        }

        /// <summary>
        /// ��׼סԺ��
        /// </summary>
        public int StandardDays
        {
            get
            {
                return standardDays;
            }
            set
            {
                standardDays = value;
            }
        }

        /// <summary>
        /// סԺ�ȼ�
        /// </summary>
        public string InpGrade
        {
            get
            {
                return inpGrade;
            }
            set
            {
                inpGrade = value;
            }
        }

        /// <summary>
        /// �Ƿ�30�ּ��� 
        /// </summary>
        public string Is30Illness
        {
            get
            {
                return is30Illness;
            }
            set
            {
                is30Illness = value;
            }
        }
        /// <summary>
        /// �Ƿ�Ⱦ�� 
        /// </summary>
        public string IsInfection
        {
            get
            {
                return isInfection;
            }
            set
            {
                isInfection = value;
            }
        }

        /// <summary>
        /// �Ƿ��������
        /// </summary>
        public string IsTumour
        {
            get
            {
                return isTumour;
            }
            set
            {
                isTumour = value;
            }
        }

        /// <summary>
        /// �������ô�����
        /// </summary>
        public string KeyCode
        {
            get
            {
                return keyCode;
            }
            set
            {
                keyCode = value;
            }
        }

        /// <summary>
        /// ����Ա��Ϣ ID ���� Name ����
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment OperInfo
        {
            get
            {
                return operInfo;
            }
            set
            {
                operInfo = value;
            }
        }

        /// <summary>
        /// �����Ա�
        /// </summary>
        public FS.FrameWork.Models.NeuObject SexType
        {
            get
            {
                return sexType;
            }
            set
            {
                sexType = value; ;
            }
        }

        #endregion

        #region ��¡����


        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new ICD Clone()
        {
            ICD icd = base.Clone() as ICD; //��¡����
            icd.operInfo = this.operInfo.Clone(); //��¡����Ա
            icd.sexType = this.sexType.Clone();

            return icd;
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

        #region ����ʱ��
        /// <summary>
        ///����ʱ��
        /// </summary>
        [Obsolete("���� ��OperInfo.OperTime����", true)]
        public DateTime OperDate
        {
            get
            {
                return System.DateTime.Now;
            }
        }
        #endregion
    }
}
