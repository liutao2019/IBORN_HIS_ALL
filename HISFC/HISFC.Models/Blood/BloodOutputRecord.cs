using System;
using System.Collections.Generic;
using System.Text;
using Neusoft.HISFC.Object.Base;

namespace Neusoft.HISFC.Object.Blood
{
    /// <summary>
    /// [��������: �������]<br></br>
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
    public class BloodOutputRecord : Spell, ISort, IValid
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public BloodOutputRecord()
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
        /// Ѫ�ͣ�ѪҺ�ɷֵȼ̳���BloodApply
        /// </summary>
        private Neusoft.HISFC.Object.Blood.BloodApply bloodAppy = new Neusoft.HISFC.Object.Blood.BloodApply();

        /// <summary>
        /// סԺ�ţ�����ŵȼ̳��Բ�����Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.RADT.PatientInfo patientInfo = new Neusoft.HISFC.Object.RADT.PatientInfo();
        
        /// <summary>
        /// ��Ѫ���ŵȼ̳���BloodMatchRecord
        /// </summary>
        private Neusoft.HISFC.Object.Blood.BloodMatchRecord bloodMatchRecord = new BloodMatchRecord();

        /// <summary>
        /// Ѫ���ŵȼ̳���BloodInputRecord
        /// </summary>
        private Neusoft.HISFC.Object.Blood.BloodInputRecord bloodInputRecord = new BloodInputRecord();

        /// <summary>
        /// ���ұ���
        /// </summary>
        private Neusoft.HISFC.Object.Base.Employee clinicNo = new Employee();

        /// <summary>
        /// �������Ա������ʱ��
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment bloodOutputPerson = new OperEnvironment();

        /// <summary>
        /// ���ⵥ��
        /// </summary>
        private string outputNo;

        /// <summary>
        /// ��������
        /// </summary>
        private string bloodOutputType;

        /// <summary>
        /// ���ⷽ��
        /// </summary>
        private string bloodOutputDirect;

        /// <summary>
        /// ������
        /// </summary>
        private string bloodOutputPrice;

        #endregion

        #region ����

        /// <summary>
        /// Ѫ�ͣ�ѪҺ�ɷֵȼ̳���BloodApply
        /// </summary>
        public Neusoft.HISFC.Object.Blood.BloodApply BloodAppy
        {
            get { return bloodAppy; }
            set { bloodAppy = value; }
        }
   
        /// <summary>
        /// סԺ�ţ�����ŵȼ̳��Բ�����Ϣ
        /// </summary>
        public Neusoft.HISFC.Object.RADT.PatientInfo PatientInfo
        {
            get { return patientInfo; }
            set { patientInfo = value; }
        }

        /// <summary>
        /// ��Ѫ���ŵȼ̳���BloodMatchRecord
        /// </summary>
        public Neusoft.HISFC.Object.Blood.BloodMatchRecord BloodMatchRecord
        {
            get { return bloodMatchRecord; }
            set { bloodMatchRecord = value; }
        }

        /// <summary>
        /// Ѫ���ŵȼ̳���BloodInputRecord
        /// </summary>
        public Neusoft.HISFC.Object.Blood.BloodInputRecord BloodInputRecord
        {
            get { return bloodInputRecord; }
            set { bloodInputRecord = value; }
        }

        /// <summary>
        /// ���ұ���
        /// </summary>
        public Neusoft.HISFC.Object.Base.Employee ClinicNo
        {
            get { return clinicNo; }
            set { clinicNo = value; }
        }

        /// <summary>
        /// �������Ա������ʱ��
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment BloodOutputPerson
        {
            get { return bloodOutputPerson; }
            set { bloodOutputPerson = value; }
        }

        /// <summary>
        /// ���ⵥ��
        /// </summary>
        public string OutputNo
        {
            get { return outputNo; }
            set { outputNo = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string BloodOutputType
        {
            get { return bloodOutputType; }
            set { bloodOutputType = value; }
        }

        /// <summary>
        /// ���ⷽ��
        /// </summary>
        public string BloodOutputDirect
        {
            get { return bloodOutputDirect; }
            set { bloodOutputDirect = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string BloodOutputPrice
        {
            get { return bloodOutputPrice; }
            set { bloodOutputPrice = value; }
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
        public new BloodOutputRecord Clone()
        {
            BloodOutputRecord bloodOutputRecord = base.Clone() as BloodOutputRecord;

            bloodOutputRecord.BloodInputRecord = this.BloodInputRecord.Clone();
            bloodOutputRecord.BloodMatchRecord = this.BloodMatchRecord.Clone();
            bloodOutputRecord.BloodAppy = this.BloodAppy.Clone();
            bloodOutputRecord.BloodOutputPerson = this.BloodOutputPerson.Clone();
            bloodOutputRecord.ClinicNo = this.ClinicNo.Clone();
            bloodOutputRecord.PatientInfo = this.PatientInfo.Clone();
            return bloodOutputRecord;
        }
        #endregion

    }
}
