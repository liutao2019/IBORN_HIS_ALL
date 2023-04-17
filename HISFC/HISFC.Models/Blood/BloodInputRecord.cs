using System;
using System.Collections.Generic;
using System.Text;
using Neusoft.HISFC.Object.Base;

namespace Neusoft.HISFC.Object.Blood
{
    /// <summary>
    /// [��������: ������]<br></br>
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
    /// 
    public class BloodInputRecord : Spell, ISort, IValid
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public BloodInputRecord()
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
        /// Ѫ�ͣ�Hr�ȼ̳���BloodComponents
        /// </summary>
        private Neusoft.HISFC.Object.Blood.BloodApply bloodApply = new BloodApply();

        /// <summary>
        /// ѪҺ����,��λ�ȼ̳���BloodComponents
        /// </summary>
        private Neusoft.HISFC.Object.Blood.BloodComponents bloodComponents = new BloodComponents();

        /// <summary>
        /// ��Ѫ��,��Ѫ����
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment adoptBloodPerson = new OperEnvironment();

        /// <summary>
        /// ������Ա�����ʱ��
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment bloodIntputPerson = new OperEnvironment();

        /// <summary>
        /// ѪԴ
        /// </summary>
        private string bloodResource;

        /// <summary>
        /// ��Ѫ��
        /// </summary>
        private string bloodDonor;

        /// <summary>
        /// ��Ѫ��
        /// </summary>
        private string bloodQuality;

        /// <summary>
        /// Ѫ����
        /// </summary>
        private string bloodBagNo;

        /// <summary>
        /// Ѫ������
        /// </summary>
        private string bloodBagID;

        /// <summary>
        /// ��ⵥ��
        /// </summary>
        private string inputNo;

        /// <summary>
        /// �������
        /// </summary>
        private string isInputType;

        /// <summary>
        /// �����
        /// </summary>
        private decimal bloodBuyPrice;

        /// <summary>
        /// ���ۼ�
        /// </summary>
        private decimal bloodSalePrice;

        /// <summary>
        /// ���
        /// </summary>
        private decimal bloodPrice;

        /// <summary>
        /// ʧЧ����
        /// </summary>
        private DateTime invalidDate;

   #endregion

        #region ����

        /// <summary>
        /// Ѫ�ͣ�Hr�ȼ̳���BloodComponents
        /// </summary>
        public Neusoft.HISFC.Object.Blood.BloodApply BloodApply
        {
            get { return bloodApply; }
            set { bloodApply = value; }
        }

        /// <summary>
        /// ѪҺ����,��λ�ȼ̳���BloodComponents
        /// </summary>
        public Neusoft.HISFC.Object.Blood.BloodComponents BloodComponents
        {
            get { return bloodComponents; }
            set { bloodComponents = value; }
        }

        /// <summary>
        /// ������Ա�����ʱ��
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment BloodIntputPerson
        {
            get { return bloodIntputPerson; }
            set { bloodIntputPerson = value; }
        }

        /// <summary>
        /// ��Ѫ��,��Ѫ����
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment AdoptBloodPerson
        {
            get { return adoptBloodPerson; }
            set { adoptBloodPerson = value; }
        }

        /// <summary>
        /// ��Ѫ��
        /// </summary>
        public string BloodDonor
        {
            get { return bloodDonor; }
            set { bloodDonor = value; }
        }

        /// <summary>
        /// ��Ѫ��
        /// </summary>
        public string BloodQuality
        {
            get { return bloodQuality; }
            set { bloodQuality = value; }
        }

        /// <summary>
        /// ѪԴ
        /// </summary>
        public string BloodResource
        {
            get { return bloodResource; }
            set { bloodResource = value; }
        }

        /// <summary>
        /// Ѫ����
        /// </summary>
        public string BloodBagNo
        {
            get { return bloodBagNo; }
            set { bloodBagNo = value; }
        }

        /// <summary>
        /// Ѫ������
        /// </summary>
        public string BloodBagID
        {
            get { return bloodBagID; }
            set { bloodBagID = value; }
        }

        /// <summary>
        /// ��ⵥ��
        /// </summary>
        public string InputNo
        {
            get { return inputNo; }
            set { inputNo = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public string IsInputType
        {
            get { return isInputType; }
            set { isInputType = value; }
        }

        /// <summary>
        /// �����
        /// </summary>
        public decimal BloodBuyPrice
        {
            get { return bloodBuyPrice; }
            set { bloodBuyPrice = value; }
        }

        /// <summary>
        /// ���ۼ�
        /// </summary>
        public decimal BloodSalePrice
        {
            get { return bloodSalePrice; }
            set { bloodSalePrice = value; }
        }

        /// <summary>
        /// ���
        /// </summary>
        public decimal BloodPrice
        {
            get { return bloodPrice; }
            set { bloodPrice = value; }
        }

        /// <summary>
        /// ʧЧ����
        /// </summary>
        public DateTime InvalidDate
        {
            get { return invalidDate; }
            set { invalidDate = value; }
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
        public new BloodInputRecord Clone()
        {
            BloodInputRecord bloodInputRecord = base.Clone() as BloodInputRecord;

            bloodInputRecord.BloodComponents = this.BloodComponents.Clone();
            bloodInputRecord.BloodIntputPerson = this.BloodIntputPerson.Clone();
            bloodInputRecord.AdoptBloodPerson = this.AdoptBloodPerson.Clone();
            bloodInputRecord.BloodApply = this.BloodApply.Clone();
            return bloodInputRecord;
        }
        #endregion
    }
}
