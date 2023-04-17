using System;


namespace FS.HISFC.Models.HealthRecord
{

    /// <summary>
    /// ICD10�������<br></br>
    /// [��������: ICD10]<br></br>
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
    public class ICD10 : FS.FrameWork.Models.NeuObject
    {
        public ICD10()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //.
        }
        #region  ˽�б���
        /// <summary>
        /// ����������
        /// </summary>
        private string diseaseCode;
        /// <summary>
        /// �������
        /// </summary>
        private string sICD10;
        /// <summary>
        /// ����ԭ��
        /// </summary>
        private string deadReason;
        /// <summary>
        /// ��׼סԺ��
        /// </summary>
        private int inDays;
        /// <summary>
        /// ����Ա��
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment operInfo = new FS.HISFC.Models.Base.OperEnvironment();
        private FS.FrameWork.Models.NeuObject diagnoseType = new FS.FrameWork.Models.NeuObject();
        #endregion

        #region ����
        /// <summary>
        /// ����Ա��
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
        /// ����������
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
        /// �������
        /// </summary>
        public string SICD10
        {
            get
            {
                return sICD10;
            }
            set
            {
                sICD10 = value;
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
        /// ��׼סԺ��
        /// </summary>
        public int InDays
        {
            get
            {
                return inDays;
            }
            set
            {
                inDays = value;
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public FS.FrameWork.Models.NeuObject DiagnoseType
        {
            get
            {
                return diagnoseType;
            }
            set
            {
                diagnoseType = value;
            }
        }

        public FS.HISFC.Models.Base.Spell SpellCode = new FS.HISFC.Models.Base.Spell();
        #endregion

        #region ����
        public new ICD10 Clone()
        {
            ICD10 obj = base.Clone() as ICD10;
            obj.DiagnoseType = this.DiagnoseType.Clone();
            obj.SpellCode = this.SpellCode.Clone();
            return obj;
        }
        #endregion

        #region ����
        /// <summary>
        /// ����ʱ��
        /// </summary>
        [Obsolete("���� �� OperInfo.OperTime����", true)]
        public DateTime OperDate;
        #endregion
    }
}
