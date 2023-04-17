using System;
using System.Collections;
namespace Neusoft.HISFC.Object.Examination
{
    /// <summary>
    /// IBaby<br></br>
    /// [��������: ���Ǽ���]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12-08]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
	public class ChkRegister : Neusoft.NFC.Object.NeuObject ,Neusoft.HISFC.Object.Base.ISpell
	{
        /// <summary>
        /// ���캯��
        /// </summary>
		public ChkRegister()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
        }
        #region ˽�б���

        /// <summary>
        ///��쵵���� 
        /// </summary>
		private string Chk_Id=string.Empty;  
        /// <summary>
        ///�����ˮ��
        /// </summary>
		private string chkClinicNo=string.Empty;
        /// <summary>
        /// ����ʷ
        /// </summary>
        private string anaphy_flag = string.Empty;
        /// <summary>
        /// ƴ����
        /// </summary>
        private string spellCode = string.Empty;
        /// <summary>
        /// �����
        /// </summary>
        private string wbCode = string.Empty;
        /// <summary>
        /// �û���
        /// </summary>
        private string userCode = string.Empty;
        private string chk_level = string.Empty;
        /// <summary>
        ///Ѫѹ���ֵ
        /// </summary>
        private string bloodPressTop = string.Empty;
        /// <summary>
        ///Ѫѹ���ֵ
        /// </summary>
        private string bloodPressDown = string.Empty;
        /// <summary>
        ///���
        /// </summary>
		private decimal ownCost=0.0m; 
        /// <summary>
        /// ������λ�ʿ 
        /// </summary>
		private Neusoft.NFC.Object.NeuObject dutyNuse = null;
        /// <summary>
        /// ��쵥λ
        /// </summary>
		private Neusoft.NFC.Object.NeuObject  chkCompany  = null;
        /// <summary>
        /// �����ۺ�ʵ��
        /// </summary>
		private Neusoft.HISFC.Object.RADT.PatientInfo patientInfo = null;
        /// <summary>
        /// �Һſ���
        /// </summary>
		private Neusoft.NFC.Object.NeuObject regDept = null;
        /// <summary>
        /// ��쵥λ����ʵ��
        /// </summary>
        private CHKCompanyGroup chkCompanyGroup =new CHKCompanyGroup();
		/// <summary>
		/// ��������ʵ��
		/// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment oper = new Neusoft.HISFC.Object.Base.OperEnvironment();
        /// <summary>
        /// ��������
        /// </summary>
        private string collectivityCode = string.Empty;
        /// <summary>
        /// �������Ǽ�����
        /// </summary>
		private System.DateTime collectivityDate=DateTime.MinValue;
        /// <summary>
        /// ��쵥λ���ű���
        /// </summary>
        private string comDeptCode = string.Empty; //��쵥λ���ű���
        /// <summary>
        /// ��쵥λ��������
        /// </summary>
        private string comDeptName=string.Empty;
        /// <summary>
        /// �����������
        /// </summary>
        private Neusoft.NFC.Object.NeuObject specalChkType;
        /// <summary>
        /// �Ǽ����Ԥ�Ǽǻ�����ʽ�Ǽ�)
        /// </summary>
        private int registerType = 0;
        /// <summary>
        /// ��Ƭ
        /// </summary>
        private byte[] image = null;//��Ƭ
        /// <summary>
        /// ����1 ���� 2  ����
        /// </summary>
        private string cHKKind=string.Empty;
        /// <summary>
        /// //��ʷ
        /// </summary>c
        private string caseHospital=string.Empty;
        /// <summary>
        ///  //��ͥ��ʷ
        /// </summary>
        private string homeCase=string.Empty;
        /// <summary>
        /// ����ʷ
        /// </summary>
        private string anamnesis = string.Empty;
        /// <summary>
        /// �������
        /// </summary>
        private System.DateTime checkDate=DateTime.MinValue;
        /// <summary>
        /// Ԥ���ʱ��
        /// </summary>
        private DateTime preCheckdate=DateTime.MinValue;
        /// <summary>
        /// ������ 
        /// </summary>
        private int cHKSortNo=0;
        /// <summary>
        /// �������ǼǴ���
        /// </summary>
        private int collectRegNum = 0;
        /// <summary>
        /// ��������
        /// </summary>
        private string transType=string.Empty;//��������
        /// <summary>
        ///�����Ŀ
        /// </summary>
        public Neusoft.HISFC.Object.Base.Item item = new Neusoft.HISFC.Object.Base.Item();
        private ArrayList chkItemList = new ArrayList();
        ///// <summary>
        ///// �Һſ���
        ///// </summary>
        //private Neusoft.NFC.Object.NeuObject operDept = null; //����Ա����
        /// <summary>
        /// ������־
        /// </summary>
        private bool finishFlag = false;
        

        #endregion

        #region ����
        /// <summary>
        /// ����ʷ
        /// </summary>
        public string Anamnesis
        {
            get
            {
                return anamnesis;
            }
            set
            {
                anamnesis = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string TransType
        {
            get
            {
                return transType;
            }
            set
            {
                transType = value;
            }
        }
        /// <summary>
        /// Ԥ���ʱ��
        /// </summary>
        public DateTime PreCheckdate
        {
            get
            {
                return preCheckdate;
            }
            set
            {
                preCheckdate = value;
            }
        }
        /// <summary>
        /// ������ 
        /// </summary>
        public int CHKSortNo
        {
            get
            {
                return cHKSortNo;
            }
            set
            {
                cHKSortNo = value;
            }
        }
        /// <summary>
        ///  //��ͥ��ʷ
        /// </summary>
        public string HomeCase
        {
            get
            {
                return homeCase;
            }
            set
            {
                homeCase = value;
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public System.DateTime CheckDate
        {
            get
            {
                return checkDate;
            }
            set
            {
                checkDate = value;
            }
        }
        /// <summary>
        /// ����1 ���� 2  ����
        /// </summary>
        public string CHKKind
        {
            get
            {
                return cHKKind;
            }
            set
            {
                cHKKind = value;
            }
        }
        /// <summary>
        /// //��ʷ
        /// </summary>c
        public string CaseHospital
        {
            get
            {
                return caseHospital;
            }
            set
            {
                caseHospital = value;
            }
        }
        /// <summary>
        /// ��쵥λ����ʵ��
        /// </summary>
        public CHKCompanyGroup ChkCompanyGroup
        {
            get
            {
                return chkCompanyGroup;
            }
            set
            {
                chkCompanyGroup = value;
            }
        }
        /// <summary>
        /// ��쵥λ��������
        /// </summary>
        public string ComDeptName
        {
            get
            {
                return comDeptName;
            }
            set
            {
                comDeptName = value;
            }
        }
        
        /// <summary>
        /// ��쵥�Ų��ű���
        /// </summary>
        public string ComDeptCode
        {
            get
            {
                return comDeptCode;
            }
            set
            {
                comDeptCode = value;
            }
        }

        /// <summary>
		/// ��Ƭ
		/// </summary>
		public byte[] Image
		{
			get
			{
				return this.image;
			}
			set
			{
				this.image = value;
			}
		}
		
		/// <summary>
		/// �������Ǽ�����
		/// </summary>
		public System.DateTime CollectivityDate
		{
			get
			{
				return collectivityDate;
			}
			set
			{
				collectivityDate = value;
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string CollectivityCode
		{
			get
			{
				return collectivityCode;
			}
			set
			{
				collectivityCode = value;
			}
		}
        
		/// <summary>
		/// �Һſ���
		/// </summary>
		public Neusoft.NFC.Object.NeuObject RegDept 
		{
			get
			{
				if(regDept == null)
				{
					regDept = new Neusoft.NFC.Object.NeuObject();
				}
				return  regDept;
			}
			set
			{
				regDept = value;
			}
		}
		
        ///// <summary>
        ///// ����Ա����
        ///// </summary>
        //public Neusoft.NFC.Object.NeuObject OperDept 
        //{
        //    get
        //    {
        //        if(operDept == null)
        //        {
        //            operDept = new Neusoft.NFC.Object.NeuObject();
        //        }
        //        return operDept;
        //    }
        //    set
        //    {
        //        operDept = value;
        //    }
        //}
		/// <summary>
		/// ���λ�ʿ
		/// </summary>
		public Neusoft.NFC.Object.NeuObject DutyNuse
		{
			get
			{
				if(dutyNuse == null)
				{
					dutyNuse = new Neusoft.NFC.Object.NeuObject();
				}
				return dutyNuse;
			}
			set
			{
				dutyNuse = value;
			}
		}
		/// <summary>
		/// ���
		/// </summary>
		public decimal OwnCost
		{
			get
			{
				return ownCost;
			}
			set
			{
				ownCost = value;
			}
		}
		/// <summary>
		/// Ѫѹ���ֵ
		/// </summary>
		public string BloodPressDown
		{
			get
			{
				return bloodPressDown;
			}
			set
			{
				bloodPressDown = value;
			}
		}
		/// <summary>
		/// Ѫѹ���ֵ
		/// </summary>
		public string BloodPressTop 
		{
			get
			{
				return bloodPressTop;
			}
			set
			{
				bloodPressTop =  value;
			}
		}
		 
		public string ChkLevel
		{
			get
			{
				return chk_level;
			}
			set
			{
				chk_level = value;
			}
		}
		/// <summary>
		/// ��쵥λ 
		/// </summary>
		public  Neusoft.NFC.Object.NeuObject  ChkCompany
		{
			get
			{
				if(chkCompany == null)
				{
					chkCompany = new Neusoft.NFC.Object.NeuObject();
				}
				return chkCompany;
			}
			set
			{
				chkCompany = value;
			}
		}
		/// <summary>
		/// ҩ����� 
		/// </summary>
		public string AnaphyFlag
		{
			get
			{
				return anaphy_flag;
			}
			set
			{
				anaphy_flag = value;
			}
		}
		//����ʵ����
		public Neusoft.HISFC.Object.RADT.PatientInfo PatientInfo
		{
			get
			{
				if(patientInfo == null)
				{
					patientInfo = new Neusoft.HISFC.Object.RADT.PatientInfo();
				}
				return patientInfo;
			}
			set
			{
				patientInfo = value;
			}
		}
		/// <summary>
		/// ����������
		/// </summary>
		public string CHKID
		{
			get
			{
				return Chk_Id;
			}
			set
			{
				Chk_Id = value;
			}
		}
		/// <summary>
		/// ����������� �� �й���죬�������� 
		/// </summary>
		public Neusoft.NFC.Object.NeuObject SpecalChkType
		{
			get
			{
				if(specalChkType == null)
				{
					specalChkType = new Neusoft.NFC.Object.NeuObject();
				}
				return specalChkType;
			}
			set
			{
				specalChkType = value;
			}
		}
		/// <summary>
		/// �����ˮ��
		/// </summary>
		public string ChkClinicNo
		{
			get
			{
				return chkClinicNo;
			}
			set 
			{
				chkClinicNo = value;
			}
		}

		/// <summary>
		/// ����Ա��Ϣ
		/// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment Operator 
		{
			get
			{
				if(oper == null)
				{
                    oper = new Neusoft.HISFC.Object.Base.OperEnvironment();
				}
				return oper;
			}
			set
			{
				oper = value;
			}
		}
        /// <summary>
        /// �Ǽ����Ԥ�Ǽǻ�����ʽ�Ǽ�)
        /// </summary>
        public int RegisterType
        {
            get
            {
                return registerType;
            }
            set
            {
                registerType = value;
            }
        }
        /// <summary>
        /// �������ǼǴ���
        /// </summary>
        public int CollectRegNum
        {
            get
            {
                return collectRegNum;
            }
            set
            {
                collectRegNum = value;
            }
        }

        /// <summary>
        /// ������־
        /// </summary>
        public bool FinishFlag
        {
            get
            {
                return finishFlag;
            }
            set
            {
                finishFlag = value;
            }
        }

        #endregion

        #region ʵ�ֽӿ�
        /// <summary>
        /// ƴ����
        /// </summary>
        public string SpellCode
		{
			get
			{
				return this.spellCode;
			}
			set
			{
				this.spellCode=value;
			}
		}
       /// <summary>
       /// �����
       /// </summary>
		public string WBCode
		{
			get
			{
				return this.wbCode;
			}
			set
			{
				this.wbCode = value;
			}
		}
        /// <summary>
        /// �û���
        /// </summary>
		public string UserCode
		{
			get
			{
				return this.userCode;
			}
			set
			{
				this.userCode = value;
			}
        }
        #endregion

        #region ������¡
        /// <summary>
		/// ��¡����
		/// </summary>
		/// <returns></returns>
		public new ChkRegister Clone()
		{
            ChkRegister obj = base.Clone() as ChkRegister;            
			obj.item= this.item.Clone();
			obj.ChkCompany=this.chkCompany.Clone();//(Neusoft.HISFC.Object.Fee.Invoice)Invoice.Clone();
			obj.PatientInfo=this.PatientInfo.Clone();
			obj.regDept = this.regDept.Clone();
			obj.Operator = this.Operator.Clone();
			obj.dutyNuse= DutyNuse.Clone();
            //obj.operDept = this.operDept.Clone();
			return obj;
        }
        #endregion
    }
}
