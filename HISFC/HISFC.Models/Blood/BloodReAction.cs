using System;
using System.Collections.Generic;
using System.Text;
using Neusoft.HISFC.Object.Base;

namespace Neusoft.HISFC.Object.Blood
{
    /// <summary>
    /// [��������: ��Ѫ��ӳ]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-4-19]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// 
    /// </summary>
    public class BloodReAction : Spell, ISort, IValid
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public BloodReAction()
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
        /// ����ţ�Ѫ����
        /// </summary> 
        private Neusoft.HISFC.Object.Blood.BloodMatchRecord bloodMatch = new BloodMatchRecord();

        /// <summary>
        /// �ҽ�����ʱ��
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment reportDoctor = new OperEnvironment();

        /// <summary>
        /// ���
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment reportPerson = new OperEnvironment();

        /// <summary>
        /// ������
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment reportCheckPerson = new OperEnvironment();

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

        /// <summary>
        /// ����ţ�Ѫ����
        /// </summary>
        public Neusoft.HISFC.Object.Blood.BloodMatchRecord BloodMatch
        {
            get { return bloodMatch; }
            set { bloodMatch = value; }
        }

        /// <summary>
        /// �ҽ�����ʱ��
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment ReportDoctor
        {
            get { return reportDoctor; }
            set { reportDoctor = value; }
        }

        /// <summary>
        /// ���
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment ReportPerson
        {
            get { return reportPerson; }
            set { reportPerson = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment ReportCheckPerson
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
        public new BloodReAction Clone()
        {
            BloodReAction bloodReAction = base.Clone() as BloodReAction;

            bloodReAction.ReportCheckPerson = this.ReportCheckPerson.Clone();
            bloodReAction.ReportDoctor = this.ReportDoctor.Clone();
            bloodReAction.ReportCheckPerson = this.ReportCheckPerson.Clone();
            bloodReAction.BloodMatch = this.BloodMatch.Clone();

            return bloodReAction;
        }
        #endregion
    }
}
