using System;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// PatientInfo <br></br>
	/// [��������: �����ۺ�ʵ��]<br></br>
	/// [�� �� ��: ���Ʒ�]<br></br>
	/// [����ʱ��: 2004-05]<br></br>
    /// User01 User02 User03 �ѱ�סԺԤԼ�Ǽ�ʹ��
	/// <�޸ļ�¼
    /// 
	///		�޸���='��һ��'
	///		�޸�ʱ��='2006-09-11'
	///		�޸�Ŀ��='�汾����'
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class PatientInfo : FS.HISFC.Models.RADT.Patient 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public PatientInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		
		}

		#region ����

		/// <summary>
		/// ���߷�����Ϣ
		/// </summary>
		private FT ft = new FT();

		/// <summary>
		/// ���߷�����
		/// </summary>
		private FS.HISFC.Models.RADT.PVisit pVisit = new FS.HISFC.Models.RADT.PVisit();

		/// <summary>
		/// ��������
		/// </summary>
		private FS.HISFC.Models.RADT.Caution caution = new FS.HISFC.Models.RADT.Caution();

		/// <summary>
		/// ������
		/// </summary>
		private FS.HISFC.Models.RADT.Kin kin = new Kin();

		/// <summary>
		/// �������  01-�Է�  02-���� 03-������ְ 04-�������� 05-���Ѹ߸�               
		/// </summary>
		private FS.HISFC.Models.Base.PayKind payKind = new FS.HISFC.Models.Base.PayKind();

		/// <summary>
		/// ����������Ϣ��
		/// </summary>
		private FS.HISFC.Models.RADT.PDisease disease = new PDisease();

		/// <summary>
		/// ���
		/// </summary>
		private System.Collections.ArrayList diagnoses = new System.Collections.ArrayList(); 

		/// <summary>
		/// ҽ�����߻�����Ϣ,������Ϣ
		/// </summary>
		private FS.HISFC.Models.SIInterface.SIMainInfo siMainInfo = new FS.HISFC.Models.SIInterface.SIMainInfo();

		/// <summary>
		/// ��չ���  Ŀǰ������ɽһԺ ��־��ҽ�����޶��Ƿ�ͬ�⣺0��ͬ�⣬1ͬ��
		/// </summary>
		private string extendFlag;

		/// <summary>
		/// ��չ���1
		/// </summary>
		private string extendFlag1;

		/// <summary>
		/// ��չ���2
		/// </summary>
		private string extendFlag2;

        /// <summary>
        /// ����סԺ������
        /// </summary>
        private EnumPatientNOType patientNOType;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime balanceDate = DateTime.MinValue;
        /// <summary>
        /// ����
        /// </summary>
        private FS.HISFC.Models.Fee.Surety surety = new FS.HISFC.Models.Fee.Surety();

        /// <summary>
        /// ���ʱ�־{2FA0D4CE-E2EB-4bc7-975A-3693B71C62CF}
        /// </summary>
        private bool isStopAcount = false;
		#endregion

		#region ����

		/// <summary>
		/// ���߷�����Ϣ
		/// </summary>
		public FT FT
		{
			get
			{
				return this.ft;
			}
			set
			{
				this.ft = value;
			}
		}

		/// <summary>
		/// ���߷�����
		/// </summary>
		public FS.HISFC.Models.RADT.PVisit PVisit
		{
			get
			{
				return this.pVisit;
			}
			set
			{
				this.pVisit = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
        [Obsolete("�Ѿ�����,�滻ΪSurety", true)]
		public FS.HISFC.Models.RADT.Caution Caution
		{
			get
			{
				return this.caution;
			}
			set
			{
				this.caution = value;
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public FS.HISFC.Models.RADT.Kin Kin
		{
			get
			{
				return this.kin;
			}
			set
			{
				this.kin = value;
			}
		}

		/// <summary>
		/// �������  01-�Է�  02-���� 03-������ְ 04-�������� 05-���Ѹ߸�
		/// </summary>
		[Obsolete("�Ѿ����ڣ���Patient�����е�Pact�Ѿ�����", true)]
		public FS.HISFC.Models.Base.PayKind PayKind
		{
			get
			{
				return this.payKind;
			}
			set
			{
				this.payKind = value;
			}
		}

		/// <summary>
		/// ����������Ϣ��
		/// </summary>
		public FS.HISFC.Models.RADT.PDisease Disease
		{
			get
			{
				return this.disease;
			}
			set
			{
				this.disease = value;
			}
		}

		/// <summary>
		/// ���
		/// </summary>
		public System.Collections.ArrayList Diagnoses
		{
			get
			{
				return this.diagnoses;
			}
			set
			{
				this.diagnoses = value;
			}
		}

		/// <summary>
		/// ҽ�����߻�����Ϣ,������Ϣ
		/// </summary>
		public FS.HISFC.Models.SIInterface.SIMainInfo SIMainInfo
		{
			get
			{
				return this.siMainInfo;
			}
			set
			{
				this.siMainInfo = value;
			}
		}

		/// <summary>
		/// ��չ���  Ŀǰ������ɽһԺ ��־��ҽ�����޶��Ƿ�ͬ�⣺0��ͬ�⣬1ͬ��
		/// </summary>
		public string ExtendFlag
		{
			get
			{
				return this.extendFlag;
			}
			set
			{
				this.extendFlag = value;
			}
		}

		/// <summary>
		/// ��չ���1
		/// </summary>
		public string ExtendFlag1
		{
			get
			{
				return this.extendFlag1;
			}
			set
			{
				this.extendFlag1 = value;
			}
		}

		/// <summary>
		/// ��չ���2
		/// </summary>
		public string ExtendFlag2
		{
			get
			{
				return this.extendFlag2;
			}
			set
			{
				this.extendFlag2 = value;
			}
		}

        /// <summary>
        /// ����סԺ������
        /// </summary>
        public EnumPatientNOType PatientNOType 
        {
            get 
            {
                return this.patientNOType;
            }
            set 
            {
                this.patientNOType = value;
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime BalanceDate
        {
            get
            {
                return this.balanceDate;
            }
            set
            {
                this.balanceDate = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public FS.HISFC.Models.Fee.Surety Surety
        {
            get
            {
                return surety;
            }
            set
            {
                surety = value;
            }
        }

        /// <summary>
        /// ���ʱ�־{2FA0D4CE-E2EB-4bc7-975A-3693B71C62CF}
        /// </summary>
        public bool IsStopAcount
        {
            get { return isStopAcount; }
            set { isStopAcount = value; }
        }
		#endregion

		#region ����

		/// <summary>
		/// ���߷�����Ϣ
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪFT", true)]
		public FT Fee=new FT();

		/// <summary>
		/// ���߻�����Ϣ
		/// </summary>
		private FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();

		/// <summary>
		/// ���߻�����Ϣ
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�����Ϊ�̳�",true)]
		public FS.HISFC.Models.RADT.Patient Patient
		{
			get
			{
				return this.patient;
			}
			set
			{
				this.patient = value;
			}
		}
		
		#endregion

		#region ����

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new PatientInfo Clone()
        {
            #region addby xuewj 2010-8-30 ǳ��¡�޷���¡�������� {7CBA264D-7659-4cfb-9329-10F60A0A753D}
            //PatientInfo patientInfo = base.MemberwiseClone() as PatientInfo;
            PatientInfo patientInfo = base.Clone() as PatientInfo;
            #endregion
            patientInfo.FT = this.FT.Clone();
			patientInfo.PVisit = this.PVisit.Clone();
			//patientInfo.Caution = this.Caution.Clone();
			patientInfo.Kin = this.Kin.Clone();
			patientInfo.Disease = this.Disease.Clone();
			patientInfo.Diagnoses = ( System.Collections.ArrayList)this.Diagnoses.Clone();
            patientInfo.Surety = this.Surety.Clone();
//			obj.SIMainInfo = this.SIMainInfo.Clone();
			return patientInfo;
		}

		#endregion
	}
}
