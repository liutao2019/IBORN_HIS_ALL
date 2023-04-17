using System;


namespace FS.HISFC.Models.HealthRecord
{


    /// <summary>
    /// Nurse ��ժҪ˵��:����ȼ���Ϣ	ID ����Ա��� Name ����Ա����
    /// </summary>
    [Serializable]
    public class Nurse : FS.FrameWork.Models.NeuObject
    {
        public Nurse()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ˽�б���

        private FS.HISFC.Models.RADT.Patient myPatientInfo = new FS.HISFC.Models.RADT.Patient();
        private FS.FrameWork.Models.NeuObject myNurseInfo = new FS.FrameWork.Models.NeuObject();
        private int exeNumber;
        private string exeUnit;
        private DateTime operDate;

        #endregion

        #region ����
        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        public FS.HISFC.Models.RADT.Patient PatientInfo
        {
            get { return myPatientInfo; }
            set { myPatientInfo = value; }
        }
        /// <summary>
        /// ����ȼ���Ϣ ID �ȼ����� Name �ȼ�����
        /// </summary>
        public FS.FrameWork.Models.NeuObject NurseInfo
        {
            get { return myNurseInfo; }
            set { myNurseInfo = value; }
        }
        /// <summary>
        /// ִ������
        /// </summary>
        public int ExeNumber
        {
            get { return exeNumber; }
            set { exeNumber = value; }
        }
        /// <summary>
        /// ִ�е�λ
        /// </summary>
        public string ExeUnit
        {
            get { return exeUnit; }
            set { exeUnit = value; }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime OperDate
        {
            get { return operDate; }
            set { operDate = value; }
        }

        #endregion

        #region ���к���


        public new Nurse Clone()
        {
            Nurse NurseClone = base.MemberwiseClone() as Nurse;

            NurseClone.PatientInfo = this.PatientInfo.Clone();
            NurseClone.myNurseInfo = this.myNurseInfo.Clone();

            return NurseClone;
        }
        #endregion
    }
}
