using System;


namespace FS.HISFC.Models.HealthRecord
{


    /// <summary>
    /// DiagnoseBase<br></br>
    /// [��������: ���������]<br></br>
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
    public class DiagnoseBase : FS.HISFC.Models.Base.Spell, FS.HISFC.Models.Base.IValid
    {
        public DiagnoseBase()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //.
        }
        #region ˽�б���
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();
        /// <summary>
        /// �������(10λ����)
        /// </summary>
        private int happenNo;
        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.Models.NeuObject diagType = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// ICD10
        /// </summary>
        private ICD icd10 = new ICD();
        /// <summary>
        /// ���������
        /// </summary>
        //private ICD icdf10 = new ICD();
        /// <summary>
        /// ���ʱ��
        /// </summary>
        private DateTime diagDate;
        /// <summary>
        /// ���ҽ��
        /// </summary>
        private FS.FrameWork.Models.NeuObject doctor = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// ��Ͽ���
        /// </summary>
        private FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// �������
        /// </summary>
        private string operationNo = "";
        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        private bool isValid;
        /// <summary>
        /// �Ƿ������
        /// </summary>
        private bool isMain;
        #endregion

        #region ����
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public FS.HISFC.Models.RADT.Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
            }
        }
        /// <summary>
        /// �������(10λ����)
        /// </summary>
        public int HappenNo
        {
            get
            {
                return happenNo;
            }
            set
            {
                happenNo = value;
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public FS.FrameWork.Models.NeuObject DiagType
        {
            get
            {
                return diagType;
            }
            set
            {
                diagType = value;
            }
        }
        /// <summary>
        /// ICD10
        /// </summary>
        public ICD ICD10
        {
            get
            {
                return icd10;
            }
            set
            {
                icd10 = value;
            }
        }
        /// <summary>
        /// ���������
        /// </summary>
        //public ICD ICDF10
        //{
        //    get
        //    {
        //        return icdf10;
        //    }
        //    set
        //    {
        //        icdf10 = value;
        //    }
        //}
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public DateTime DiagDate
        {
            get
            {
                return diagDate;
            }
            set
            {
                diagDate = value;
            }
        }
        /// <summary>
        /// ���ҽ��
        /// </summary>
        public FS.FrameWork.Models.NeuObject Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                doctor = value;
            }
        }
        /// <summary>
        /// ��Ͽ���
        /// </summary>
        public FS.FrameWork.Models.NeuObject Dept
        {
            get
            {
                return dept;
            }
            set
            {
                dept = value;
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string OperationNo
        {
            get
            {
                return operationNo;
            }
            set
            {
                operationNo = value;
            }
        }
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
        /// <summary>
        /// �Ƿ������
        /// </summary>
        public bool IsMain
        {
            get
            {
                return isMain;
            }
            set
            {
                isMain = value;
            }
        }

        #endregion

        #region ����
        public new DiagnoseBase Clone()
        {
            DiagnoseBase obj = base.Clone() as DiagnoseBase;
            obj.patient = patient.Clone();
            obj.DiagType = DiagType.Clone();
            obj.ICD10 = ICD10.Clone();
            obj.Dept = Dept.Clone();
            obj.Doctor = Doctor.Clone();
            return obj;
        }
        #endregion

        #region ����
        ///// <summary>
        ///// ��������
        ///// </summary>
        //[Obsolete("���� �ü̳д���",true)]
        //public FS.HISFC.Models.Base.Spell SpellCode = new FS.HISFC.Models.Base.Spell();
        #endregion

        #region IValid ��Ա

        bool FS.HISFC.Models.Base.IValid.IsValid
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
