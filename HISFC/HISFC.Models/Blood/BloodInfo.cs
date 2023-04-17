using System;

namespace Neusoft.HISFC.Models.Blood
{
	/// <summary>
	/// BloodInfo ��ժҪ˵����
	/// Ѫ��������Ϣ
	/// �̳�Neusoft.FrameWork.Models.NeuObject
	/// ID:Ѫ����
	/// Name:��Ѫ������
	/// </summary>
    /// 
    [System.Serializable]
	public class BloodInfo:Neusoft.FrameWork.Models.NeuObject
	{
		public BloodInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		private Neusoft.HISFC.Models.Base.RhDs RhDInfo; //Rhd��Ϣ Positive���� Negative����
		/// <summary>
		/// Rhd��Ϣ Positive���� Negative����
		/// </summary>
		public Neusoft.HISFC.Models.Base.RhDs RhD
		{
			get
			{
				return RhDInfo;
			}
			set
			{
				RhDInfo = value;
			}
		}

		private Neusoft.FrameWork.Models.NeuObject bloodType = new Neusoft.FrameWork.Models.NeuObject();
		/// <summary>
		/// Ѫ����Ϣ
		/// ID:Ѫ�ͱ���
		/// NAME:Ѫ������
		/// User01:����
		/// </summary>
		public Neusoft.FrameWork.Models.NeuObject BloodType
		{
			get
			{
				return bloodType;
			}
			set
			{
				bloodType = value;
			}
		}
		
		private decimal amount;//Ѫ��
		/// <summary>
		/// Ѫ��
		/// </summary>
		public decimal Amount
		{
			get
			{
				return amount;
			}
			set
			{
				amount = value;
			}
		}

		private DateTime gatherDate;//��Ѫʱ��
		/// <summary>
		/// ��Ѫʱ��
		/// </summary>
		public DateTime GatherDate
		{
			get
			{
				return gatherDate;
			}
			set
			{
				gatherDate = value;
			}
		}

		private DateTime invalidationDate;//ʧЧʱ��
		/// <summary>
		/// ʧЧʱ��
		/// </summary>
		public DateTime InvalidationDate
		{
			get
			{
				return invalidationDate;
			}
			set
			{
				invalidationDate = value;
			}
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

        ///<summary>
        ///���뵥��
        ///</summary> 
        private string applyNum;

        /// <summary>
        /// Ѫ����
        /// </summary> 
        private Neusoft.HISFC.Models.Blood.BloodBags bloodBags = new BloodBags();

        /// <summary>
        /// �ҽ�����ʱ��
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment reportDoctor = new Neusoft.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ���
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment reportPerson = new Neusoft.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ������
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment reportCheckPerson = new Neusoft.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ��Ѫ��������ӳʱ��
        /// </summary>
        private string bloodToReActionTime;

        /// <summary>
        /// ����
        /// </summary>
        private string bloodPulse;

        /// <summary>
        /// Ѫѹ
        /// </summary>
        private string bloodPress;

        /// <summary>
        /// ������
        /// </summary>
        private decimal bloodInputQty;

        /// <summary>
        /// �Ƿ���
        /// </summary>
        private bool ibloodFever;

        /// <summary>
        /// �Ƿ�ͷ��
        /// </summary>
        private bool ibloodSwirl;

        /// <summary>
        /// �Ƿ��ļ�
        /// </summary>
        private bool ibloodHeart;

        /// <summary>
        /// �Ƿ��˿���Ѫ
        /// </summary>
        private bool ibloodWound;

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        private bool ibloodBreath;

        /// <summary>
        /// �Ƿ���ɫ�԰�
        /// </summary>
        private bool ibloodFaceWhilt;

        /// <summary>
        /// �Ƿ����
        /// </summary>
        private bool ibloodIcterus;

        /// <summary>
        /// �Ƿ����
        /// </summary>
        private bool ibloodPerspire;

        /// <summary>
        /// �Ƿ�Ƥ��
        /// </summary>
        private bool ibloodTetter;

        /// <summary>
        /// �Ƿ��沿���졢���
        /// </summary>
        private bool ibloodFaceRed;

        /// <summary>
        /// �Ƿ�Ѫ�쵰����
        /// </summary>
        private bool ibloodStalered;

        /// <summary>
        /// �Ƿ���ġ�Ż��
        /// </summary>
        private bool ibloodSurfeit;

        /// <summary>
        /// �Ƿ����
        /// </summary>
        private bool ibloodPurple;

        /// <summary>
        /// �Ƿ����
        /// </summary>
        private bool ibloodComa;

        /// <summary>
        /// �Ƿ����ᱳʹ
        /// </summary>
        private bool ibloodLumbago;

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        private bool ibloodHives;

        /// <summary>
        /// �Ƿ���Ѫ��ʹ������
        /// </summary>
        private bool ibloodTranspain;

        /// <summary>
        /// �Ƿ��Ѫ
        /// </summary>
        private bool ibloodBleed;

        /// <summary>
        /// �Ƿ��������
        /// </summary>
        private bool ibloodStaleLittle;

        /// <summary>
        /// ��Ѫ�����
        /// </summary>
        private string bloodClinicSuggestion;

        /// <summary>
        /// Ѫվ���
        /// </summary>
        private string bloodStationSuggestion;

        /// <summary>
        /// �������
        /// </summary>
        private string bloodOtherThings;

        #endregion

        #region ����

        ///<summary>
        ///���뵥��
        ///</summary> 
        public string ApplyNum
        {
            get { return applyNum; }
            set { applyNum = value; }
        } 

        /// <summary>
        /// Ѫ����
        /// </summary>
        public Neusoft.HISFC.Models.Blood.BloodBags BloodBags
        {
            get { return bloodBags; }
            set { bloodBags = value; }
        }

        /// <summary>
        /// �ҽ�����ʱ��
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment ReportDoctor
        {
            get { return reportDoctor; }
            set { reportDoctor = value; }
        }

        /// <summary>
        /// ���
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment ReportPerson
        {
            get { return reportPerson; }
            set { reportPerson = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment ReportCheckPerson
        {
            get { return reportCheckPerson; }
            set { reportCheckPerson = value; }
        }

        /// <summary>
        /// ��Ѫ��������ӳʱ��
        /// </summary>
        public string BloodToReActionTime1
        {
            get { return bloodToReActionTime; }
            set { bloodToReActionTime = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string BloodPulse
        {
            get { return bloodPulse; }
            set { bloodPulse = value; }
        }

        /// <summary>
        /// Ѫѹ
        /// </summary>
        public string BloodPress
        {
            get { return bloodPress; }
            set { bloodPress = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public decimal BloodInputQty
        {
            get { return bloodInputQty; }
            set { bloodInputQty = value; }
        }

        /// <summary>
        /// �Ƿ���
        /// </summary>
        public bool BloodFever
        {
            get { return ibloodFever; }
            set { ibloodFever = value; }
        }

        /// <summary>
        /// �Ƿ�ͷ��
        /// </summary>
        public bool BloodSwirl
        {
            get { return ibloodSwirl; }
            set { ibloodSwirl = value; }
        }

        /// <summary>
        /// �Ƿ��ļ�
        /// </summary>
        public bool BloodHeart
        {
            get { return ibloodHeart; }
            set { ibloodHeart = value; }
        }

        /// <summary>
        /// �Ƿ��˿���Ѫ
        /// </summary>
        public bool BloodWound
        {
            get { return ibloodWound; }
            set { ibloodWound = value; }
        }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool BloodBreath
        {
            get { return ibloodBreath; }
            set { ibloodBreath = value; }
        }

        /// <summary>
        /// �Ƿ���ɫ�԰�
        /// </summary>
        public bool BloodFaceWhilt
        {
            get { return ibloodFaceWhilt; }
            set { ibloodFaceWhilt = value; }
        }

        /// <summary>
        /// �Ƿ����
        /// </summary>
        public bool BloodIcterus
        {
            get { return ibloodIcterus; }
            set { ibloodIcterus = value; }
        }

        /// <summary>
        /// �Ƿ����
        /// </summary>
        public bool BloodPerspire
        {
            get { return ibloodPerspire; }
            set { ibloodPerspire = value; }
        }

        /// <summary>
        /// �Ƿ�Ƥ��
        /// </summary>
        public bool BloodTetter
        {
            get { return ibloodTetter; }
            set { ibloodTetter = value; }
        }

        /// <summary>
        /// �Ƿ��沿���졢���
        /// </summary>
        public bool BloodFaceRed
        {
            get { return ibloodFaceRed; }
            set { ibloodFaceRed = value; }
        }

        /// <summary>
        /// �Ƿ�Ѫ�쵰����
        /// </summary>
        public bool BloodStalered
        {
            get { return ibloodStalered; }
            set { ibloodStalered = value; }
        }

        /// <summary>
        /// �Ƿ���ġ�Ż��
        /// </summary>
        public bool BloodSurfeit
        {
            get { return ibloodSurfeit; }
            set { ibloodSurfeit = value; }
        }

        /// <summary>
        /// �Ƿ����
        /// </summary>
        public bool BloodPurple
        {
            get { return ibloodPurple; }
            set { ibloodPurple = value; }
        }

        /// <summary>
        /// �Ƿ����
        /// </summary>
        public bool BloodComa
        {
            get { return ibloodComa; }
            set { ibloodComa = value; }
        }

        /// <summary>
        /// �Ƿ����ᱳʹ
        /// </summary>
        public bool BloodLumbago
        {
            get { return ibloodLumbago; }
            set { ibloodLumbago = value; }
        }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool BloodHives
        {
            get { return ibloodHives; }
            set { ibloodHives = value; }
        }

        /// <summary>
        /// �Ƿ���Ѫ��ʹ������
        /// </summary>
        public bool BloodTranspain
        {
            get { return ibloodTranspain; }
            set { ibloodTranspain = value; }
        }

        /// <summary>
        /// �Ƿ��Ѫ
        /// </summary>
        public bool BloodBleed
        {
            get { return ibloodBleed; }
            set { ibloodBleed = value; }
        }

        /// <summary>
        /// �Ƿ��������
        /// </summary>
        public bool BloodStaleLittle
        {
            get { return ibloodStaleLittle; }
            set { ibloodStaleLittle = value; }
        }

        /// <summary>
        /// ��Ѫ�����
        /// </summary>
        public string BloodClinicSuggestion
        {
            get { return bloodClinicSuggestion; }
            set { bloodClinicSuggestion = value; }
        }

        /// <summary>
        /// Ѫվ���
        /// </summary>
        public string BloodStationSuggestion
        {
            get { return bloodStationSuggestion; }
            set { bloodStationSuggestion = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public string BloodOtherThings
        {
            get { return bloodOtherThings; }
            set { bloodOtherThings = value; }
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
        public new BloodInfo Clone()
        {
            BloodInfo bloodInfo = base.Clone() as BloodInfo;

            bloodInfo.ReportCheckPerson = this.ReportCheckPerson.Clone();
            bloodInfo.ReportDoctor = this.ReportDoctor.Clone();
            bloodInfo.ReportCheckPerson = this.ReportCheckPerson.Clone();
            bloodInfo.BloodBags = this.BloodBags.Clone();

            return bloodInfo;
        }
        #endregion
    }
	}
