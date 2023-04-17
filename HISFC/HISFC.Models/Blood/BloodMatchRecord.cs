using System;
using System.Collections.Generic;
using System.Text;
using Neusoft.HISFC.Object.Base;

namespace Neusoft.HISFC.Object.Blood
{
    /// <summary>
    /// [��������: ��Ѫ����]<br></br>
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
    
    public class BloodMatchRecord : Spell,ISort,IValid
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public BloodMatchRecord()
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
        /// ���뵥�ŵȼ̳���BloodApply
        /// </summary>
        private Neusoft.HISFC.Object.Blood.BloodApply bloodApply = new BloodApply();

        /// <summary>
        /// Ѫ������,��Ѫ�˵ȼ̳���BloodInputRecord
        /// </summary>
        private Neusoft.HISFC.Object.Blood.BloodInputRecord bloodInputRecord = new BloodInputRecord();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.RADT.PatientInfo patientInfo = new Neusoft.HISFC.Object.RADT.PatientInfo();
    
        /// <summary>
        /// ��Ѫ����
        /// </summary>
        private string bloodMatchNo;

        /// <summary>
        /// ���Ѫ����
        /// </summary>
        private string bloodBagNo;

        /// <summary>
        /// ��Ѫ����
        /// </summary>
        private string bloodInType;

        /// <summary>
        /// ��Ѫʱ��
        /// </summary>
        private string bloodInTime;

        /// <summary>
        /// ��Ѫ��Ѫ��
        /// </summary>
        private string bloodInPersonType;

        /// <summary>
        /// ����Ѫ��
        /// </summary>
        private string bloodCheckType;

        /// <summary>
        /// ��Ѫ����
        /// </summary>
        private DateTime adoptBloodDate;

        /// <summary>
        /// ��Ч����
        /// </summary>
        private DateTime validDate;

        /// <summary>
        /// ��λ
        /// </summary>
        private string stockUnit;

        /// <summary>
        /// ״̬
        /// </summary>
        private string bloodStatic;

        /// <summary>
        /// ѪԴ
        /// </summary>
        private string bloodSource;

        /// <summary>
        /// ������Ѫ
        /// </summary>
        private bool isCrossMatchBlood;

        /// <summary>
        /// ��������ɸѡ
        /// </summary>
        private bool isantifilterFlag;
 

        /// <summary>
        /// ��Ѫ��,��Ѫʱ��
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment matchBloodOperator = new OperEnvironment();

        /// <summary>
        /// ������
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment matchBloodOperator2 = new OperEnvironment();

        /// <summary>
        /// ��Ѫ�ˣ���Ѫʱ��
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment bloodOutputPerson = new OperEnvironment();

        /// <summary>
        /// ��������
        /// </summary>
        private Object.Base.Item item = new Neusoft.HISFC.Object.Base.Item();

        #endregion

        #region ����

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public Neusoft.HISFC.Object.RADT.PatientInfo PatientInfo
        {
            get { return patientInfo; }
            set { patientInfo = value; }
        } 

        /// <summary>
        /// ���뵥��
        /// </summary>
        public Neusoft.HISFC.Object.Blood.BloodApply BloodApply
        {
            get { return bloodApply; }
            set { bloodApply = value; }
        }

        /// <summary>
        /// Ѫ������,��Ѫ�˵ȼ̳���BloodMatchRecord
        /// </summary>
        public Neusoft.HISFC.Object.Blood.BloodInputRecord BloodInputRecord
        {
            get { return bloodInputRecord; }
            set { bloodInputRecord = value; }
        }

        /// <summary>
        /// ��Ѫ��,��Ѫʱ��
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment MatchBloodOperator
        {
            get { return matchBloodOperator; }
            set { matchBloodOperator = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment MatchBloodOperator2
        {
            get { return matchBloodOperator2; }
            set { matchBloodOperator2 = value; }
        }

        /// <summary>
        /// ��Ѫ�ˣ���Ѫʱ��
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment BloodOutputPerson
        {
            get { return bloodOutputPerson; }
            set { bloodOutputPerson = value; }
        }

        /// <summary>
        /// ��Ѫ����
        /// </summary>
        public string BloodMatchNo
        {
            get { return bloodMatchNo; }
            set { bloodMatchNo = value; }
        }

        /// <summary>
        /// ���Ѫ����
        /// </summary>
        public string BloodBagNo
        {
            get { return bloodBagNo; }
            set { bloodBagNo = value; }
        }

        /// <summary>
        /// ��Ѫ��Ѫ��
        /// </summary>
        public string BloodInPersonType
        {
            get { return bloodInPersonType; }
            set { bloodInPersonType = value; }
        }

        /// <summary>
        /// ����Ѫ��
        /// </summary>
        public string BloodCheckType
        {
            get { return bloodCheckType; }
            set { bloodCheckType = value; }
        }

        /// <summary>
        /// ��Ѫʱ��
        /// </summary>
        public string BloodInTime
        {
            get { return bloodInTime; }
            set { bloodInTime = value; }
        }

        /// <summary>
        /// ��Ѫ����
        /// </summary>
        public string BloodInType
        {
            get { return bloodInType; }
            set { bloodInType = value; }
        }

        /// <summary>
        /// ��Ѫ����
        /// </summary>
        public DateTime AdoptBloodDate
        {
            get { return adoptBloodDate; }
            set { adoptBloodDate = value; }
        }

        /// <summary>
        /// ��λ
        /// </summary>
        public string StockUnit
        {
            get { return stockUnit; }
            set { stockUnit = value; }
        }

        /// <summary>
        /// ״̬
        /// </summary>
        public string BloodStatic
        {
            get { return bloodStatic; }
            set { bloodStatic = value; }
        }

        /// <summary>
        /// ѪԴ
        /// </summary>
        public string BloodSource
        {
            get { return bloodSource; }
            set { bloodSource = value; }
        }

        /// <summary>
        /// ��Ч����
        /// </summary>
        public DateTime ValidDate
        {
            get { return validDate; }
            set { validDate = value; }
        }

        /// <summary>
        /// ������Ѫ
        /// </summary>
        public bool IsCrossMatchBlood
        {
            get { return isCrossMatchBlood; }
            set { isCrossMatchBlood = value; }
        }

        /// <summary>
        /// ��������ɸѡ 
        /// </summary>
        public bool IsantifilterFlag
        {
            get { return isantifilterFlag; }
            set { isantifilterFlag = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public Object.Base.Item Item
        {
            get { return item; }
            set { item = value; }
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
        public new BloodMatchRecord Clone()
        {
            BloodMatchRecord bloodMatchRecord = base.Clone() as BloodMatchRecord;

            bloodMatchRecord.PatientInfo = this.PatientInfo.Clone();
            bloodMatchRecord.BloodApply = this.BloodApply.Clone();
            bloodMatchRecord.MatchBloodOperator = this.MatchBloodOperator.Clone();
            bloodMatchRecord.MatchBloodOperator2 = this.MatchBloodOperator2.Clone();
            bloodMatchRecord.BloodOutputPerson = this.BloodOutputPerson.Clone();
            bloodMatchRecord.BloodInputRecord = this.BloodInputRecord.Clone();
            bloodMatchRecord.Item = this.Item.Clone();
            return bloodMatchRecord;
        }
        #endregion
    }
}
