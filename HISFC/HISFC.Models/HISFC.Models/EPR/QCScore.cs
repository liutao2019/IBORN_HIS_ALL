
namespace FS.HISFC.Models.EPR
{
    /// <summary>
    /// QCScoreSet ��ժҪ˵����
    /// </summary>
    [System.Serializable]
    public class QCScore : FS.FrameWork.Models.NeuObject
    {
        public QCScore()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        private FS.HISFC.Models.RADT.PatientInfo myPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get
            {
                return this.myPatientInfo;
            }
            set
            {
                this.myPatientInfo = value;
            }
        }
        private string type;
        /// <summary>
        /// ��Ŀ���
        /// </summary>
        public string Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }

        private string totalScore;
        /// <summary>
        /// ��Ŀ����ܷ�ֵ
        /// </summary>
        public string TotalScore
        {
            get
            {
                return this.totalScore;
            }
            set
            {
                this.totalScore = value;
            }
        }
        private string miniScore;
        /// <summary>
        /// ��С��ֵ
        /// </summary>
        public string MiniScore
        {
            get
            {
                return this.miniScore;
            }
            set
            {
                this.miniScore = value;
            }
        }
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public QCScore Clone()
        {
            QCScore score = base.Clone() as QCScore;
            score.PatientInfo = this.PatientInfo.Clone();
            return score;
        }
    }
}