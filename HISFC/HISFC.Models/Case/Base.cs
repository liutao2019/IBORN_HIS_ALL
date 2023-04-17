using System;
using neusoft.neuFC.Object;

namespace neusoft.HISFC.Object.Case
{
    /*----------------------------------------------------------------
    // Copyright (C) 2004 ����ɷ����޹�˾
    // ��Ȩ���С� 
    //
    // �ļ�����Base.cs
    // �ļ�������������������ʵ��
    //
    // 
    // ������ʶ:
    //
    // �޸ı�ʶ����ѩ�� 20060420
    // �޸�����������һ�´���,���ʵ��ȽϿֲ���1600���У��������������д��ԭ����Ӧ����������ϡ�������Ϣ����Ⱦ����������ʵ��ľۺϡ�
    //           ��������ɶ�Ƕ����ˣ���������Ȼ��д�Ķԣ�������������ˣ������������������Ӳ���װ������������װ������д��ʵ��
    //
    // �޸ı�ʶ��
    // �޸�������
    //----------------------------------------------------------------*/
    
	public class Base:neusoft.neuFC.Object.neuObject
	{
		public Base()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ˽�б���
		private System.String nomen ;
		private System.String nationCode ;
		private System.Int32 age ;
		private System.String ageUnit ;
		private System.String linkmanTel ;
		private System.String linkmanAdd ;
		private System.String clinicDocd ;
		private System.String clinicDonm ;
		private System.String comeFrom ;
		private System.String inAvenue ;
		private System.String inCircs ;
		private System.DateTime diagDate ;
		private System.DateTime operationDate ;
		private System.Int32 diagDays ;
		private System.Int32 piDays ;
		private System.DateTime deadDate  ;
		private System.String deadReason ;
		private System.String bodyCheck ;
		private System.String deadKind ;
		private System.String bodyAnotomize ;
		private System.String hbsag ;
		private System.String hcvAb ;
		private System.String hivAb ;
		private System.String cePi ;
		private System.String piPo ;
		private System.String opbOpa ;
		private System.String clX ;
		private System.String clCt ;
		private System.String clMri ;
		private System.String clPa ;
		private System.String fsBl ;
		private System.Int32 salvTimes ;
		private System.Int32 succTimes ;
		private System.String techSerc ;
		private System.String visiStat ;
		private System.DateTime visiPeri ;
		private System.Int32 inconNum ;
		private System.Int32 outconNum ;
		private System.String anaphyFlag ;
		private System.String anaphyName1 ;
		private System.String anaphyName2 ;
		private System.DateTime coutDate ;
		private System.String refresherDocd ;
		private System.String refresherDonm ;
		private System.String graDocCode ;
		private System.String graDocName ;
		private System.String praDocCode ;
		private System.String praDocName ;
		private System.String codingCode ;
		private System.String codingName ;
		private System.String mrQual ;
		private System.String mrElig ;
		private System.String qcDocd ;
		private System.String qcDonm ;
		private System.String qcNucd ;
		private System.String qcNunm ;
		private System.DateTime checkDate ;
		private System.String ynFirst ;
		private System.String rhBlood ;
		private System.String reactionBlood ;
		private System.String bloodRed ;
		private System.String bloodPlatelet ;
		private System.String bloodPlasma ;
		private System.String bloodWhole ;
		private System.String bloodOther ;
		private System.String xNumb ;
		private System.String ctNumb ;
		private System.String mriNumb ;
		private System.String pathNumb ;
		private System.String dsaNumb ;
		private System.String petNumb ;
		private System.String ectNumb ;
		private System.Int32 xTimes ;
		private System.Int32 ctTimes ;
		private System.Int32 mrTimes ;
		private System.Int32 dsaTimes ;
		private System.Int32 petTimes ;
		private System.Int32 ectTimes ;
		private System.String barCode ;
		private System.String lendStus ;
		private System.String caseStus ;
		private System.String operCode ;
		private System.DateTime operDate;
		private string VISIPERIWEEK;
		private string VISIPERIMONTH;
		private string VISIPERIYEAR ;
		private decimal  INUS  ;//  I������ʱ��(��)                                     
		private decimal  IINUS ;//  II������ʱ��(��)                                    
		private decimal IIINUS ;// III������ʱ��(��)                                   
		private decimal STRICTNESSNUS;// ��֢�໤ʱ��( Сʱ)                                 
		private decimal SUPERNUS;// �ؼ�����ʱ��(Сʱ)   
		private decimal SPECALNUS;//  ���⻤��(��)   
		private neusoft.neuFC.Object.neuObject  packupCode = new neuObject();// ����Ա 
		private string disease30 ;
		private string isHandCraft; //�ֹ�¼����
        private neusoft.HISFC.Object.RADT.PatientInfo patientInfo = new neusoft.HISFC.Object.RADT.PatientInfo();
		

        /// <summary>
		/// ��Ժ����
		/// </summary>
		public neusoft.neuFC.Object.neuObject deptIN = new neuObject();
		
        /// <summary>
		/// ��Ժ����
		/// </summary>
		public neusoft.neuFC.Object.neuObject deptOut = new neuObject();
		
        /// <summary>
		/// �������
		/// </summary>
 		public neusoft.neuFC.Object.neuObject ClinicDiag = new neuObject();

        /// <summary>
		/// ��Ժ���
		/// </summary>
		public neusoft.neuFC.Object.neuObject InhosDiag = new neuObject();
		
        /// <summary>
		/// ��Ժ���
		/// </summary>
		public neusoft.neuFC.Object.neuObject OutDiag = new neuObject();
		
        /// <summary>
		/// ��һ����
		/// </summary>
 		public neusoft.neuFC.Object.neuObject FirstOperation = new neuObject();
        
        /// <summary>
        /// ����ҽʦ
        /// </summary>
        public neusoft.neuFC.Object.neuObject FirstOperationDoc = new neuObject();
		
        /// <summary>
		/// Ժ�ڸ�Ⱦ����
		/// </summary>
		public int InfectionNum ;

		/// <summary>
		/// �Ƿ��в���֢
		/// </summary>
		public string SyndromeFlag ;
		
        /// <summary>
		/// ��������Ա 
		/// </summary>
		public string OperationCoding ;
		#endregion
		#region ����
		/// <summary>
		/// �ֹ�¼�벡����־
		/// </summary>
		public string IsHandCraft
		{
			get
			{
				if(isHandCraft == null)
				{
					isHandCraft = "";
				}
				return isHandCraft;
			}
			set
			{
				isHandCraft = value;
			}
		}
		/// <summary>
		/// 30�ּ���
		/// </summary>
		public string Disease30
		{
			get
			{
				if(disease30 == "")
				{
					disease30 = "";
				}
				return disease30;
			}
			set
			{
				disease30 = value;
			}
		}
		/// <summary>
		/// ����Ա 
		/// </summary>
		public neusoft.neuFC.Object.neuObject PackupMan
		{
			get
			{
				return packupCode;
			}
			set
			{
				packupCode = value;
			}
		}
		/// <summary>
		/// ���⻤��
		/// </summary>
		public decimal SPecalNus
		{
			get
			{
				return SPECALNUS;
			}
			set
			{
				SPECALNUS = value;
			}
		}
		/// <summary>
		/// �ؼ�����ʱ��
		/// </summary>
		public decimal SuperNus
		{
			get
			{
				return SUPERNUS;
			}
			set
			{
				SUPERNUS = value;
			}
		}
		/// <summary>
		/// ��֢�໤ʱ��
		/// </summary>
		public decimal StrictNuss
		{
			get
			{
				return STRICTNESSNUS;
			}
			set
			{
				STRICTNESSNUS = value;
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public decimal IIINus
		{
			get
			{
				return IIINUS;
			}
			set
			{
				IIINUS = value;
			}
		}
		/// <summary>
		/// ��������ʱ��
		/// </summary>
		public decimal IINus
		{
			get
			{
				return IINUS;
			}
			set
			{
				IINUS = value;
			}
		}
		/// <summary>
		/// һ������ʱ��
		/// </summary>
		public decimal INus
		{
			get
			{
				return INUS;
			}
			set
			{
				INUS = value;
			}
		}
		/// <summary>
		/// s������� ��������
		/// </summary>
		public string VisiPeriYear
		{
			get
			{
				if(VISIPERIYEAR == null)
				{
					VISIPERIYEAR = "";
				}
				return VISIPERIYEAR;
			}
			set
			{
				VISIPERIYEAR = value;
			}
		}
		/// <summary>
		/// s������� ��������
		/// </summary>
		public string VisiPeriMonth
		{
			get
			{
				if(VISIPERIMONTH == null)
				{
					VISIPERIMONTH = "";
				}
				return VISIPERIMONTH;
			}
			set
			{
				VISIPERIMONTH = value;
			}
		}
		/// <summary>
		/// s������� ��������
		/// </summary>
		public string VisiPeriWeek
		{
			get
			{
				if(VISIPERIWEEK == null)
				{
					VISIPERIWEEK = "";
				}
				return VISIPERIWEEK;
			}
			set
			{
				VISIPERIWEEK = value;
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		public System.String Nomen
		{
			get
			{
				return this.nomen;
			}
			set
			{
				this.nomen = value;
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		public System.String NationCode
		{
			get
			{
				return this.nationCode;
			}
			set
			{
				this.nationCode = value;
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		public System.Int32 Age
		{
			get
			{
				return this.age;
			}
			set
			{
				this.age = value;
			}
		}

		/// <summary>
		/// ���䵥λ
		/// </summary>
		public System.String AgeUnit
		{
			get
			{
				return this.ageUnit;
			}
			set
			{
				this.ageUnit = value;
			}
		}

		/// <summary>
		/// ��ϵ�绰
		/// </summary>
		public System.String LinkmanTel
		{
			get
			{
				return this.linkmanTel;
			}
			set
			{
				this.linkmanTel = value;
			}
		}

		/// <summary>
		/// ��ϵ��ַ
		/// </summary>
		public System.String LinkmanAdd
		{
			get
			{
				return this.linkmanAdd;
			}
			set
			{
				this.linkmanAdd = value;
			}
		}

		/// <summary>
		/// �������ҽ��
		/// </summary>
		public System.String ClinicDocd
		{
			get
			{
				return this.clinicDocd;
			}
			set
			{
				this.clinicDocd = value;
			}
		}

		/// <summary>
		/// �������ҽ������
		/// </summary>
		public System.String ClinicDonm
		{
			get
			{
				return this.clinicDonm;
			}
			set
			{
				this.clinicDonm = value;
			}
		}

		/// <summary>
		/// ת��ҽԺ
		/// </summary>
		public System.String ComeFrom
		{
			get
			{
				return this.comeFrom;
			}
			set
			{
				this.comeFrom = value;
			}
		}

		/// <summary>
		/// ��Ժ��Դ
		/// </summary>
		public System.String InAvenue
		{
			get
			{
				return this.inAvenue;
			}
			set
			{
				this.inAvenue = value;
			}
		}

		/// <summary>
		/// ��Ժ״̬
		/// </summary>
		public System.String InCircs
		{
			get
			{
				return this.inCircs;
			}
			set
			{
				this.inCircs = value;
			}
		}

		/// <summary>
		/// ȷ������
		/// </summary>
		public System.DateTime DiagDate
		{
			get
			{
				return this.diagDate;
			}
			set
			{
				this.diagDate = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public System.DateTime OperationDate
		{
			get
			{
				return this.operationDate;
			}
			set
			{
				this.operationDate = value;
			}
		}

		/// <summary>
		/// ȷ������
		/// </summary>
		public System.Int32 DiagDays
		{
			get
			{
				return this.diagDays;
			}
			set
			{
				this.diagDays = value;
			}
		}

		/// <summary>
		/// סԺ����
		/// </summary>
		public System.Int32 PiDays
		{
			get
			{
				return this.piDays;
			}
			set
			{
				this.piDays = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public System.DateTime DeadDate
		{
			get
			{
				return this.deadDate;
			}
			set
			{
				this.deadDate = value;
			}
		}

		/// <summary>
		/// ����ԭ��
		/// </summary>
		public System.String DeadReason
		{
			get
			{
				return this.deadReason;
			}
			set
			{
				this.deadReason = value;
			}
		}

		/// <summary>
		/// ʬ��
		/// </summary>
		public System.String BodyCheck
		{
			get
			{
				return this.bodyCheck;
			}
			set
			{
				this.bodyCheck = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public System.String DeadKind
		{
			get
			{
				return this.deadKind;
			}
			set
			{
				this.deadKind = value;
			}
		}

		/// <summary>
		/// ʬ����ʺ�
		/// </summary>
		public System.String BodyAnotomize
		{
			get
			{
				return this.bodyAnotomize;
			}
			set
			{
				this.bodyAnotomize = value;
			}
		}

		/// <summary>
		/// �Ҹα��濹ԭ�����ԡ����ԡ�δ����
		/// </summary>
		public System.String Hbsag
		{
			get
			{
				return this.hbsag;
			}
			set
			{
				this.hbsag = value;
			}
		}

		/// <summary>
		/// ���β������壨���ԡ����ԡ�δ����
		/// </summary>
		public System.String HcvAb
		{
			get
			{
				return this.hcvAb;
			}
			set
			{
				this.hcvAb = value;
			}
		}

		/// <summary>
		/// �������������ȱ�ݲ������壨���ԡ����ԡ�δ����
		/// </summary>
		public System.String HivAb
		{
			get
			{
				return this.hivAb;
			}
			set
			{
				this.hivAb = value;
			}
		}

		/// <summary>
		/// �ż���Ժ����
		/// </summary>
		public System.String CePi
		{
			get
			{
				return this.cePi;
			}
			set
			{
				this.cePi = value;
			}
		}

		/// <summary>
		/// ���Ժ����
		/// </summary>
		public System.String PiPo
		{
			get
			{
				return this.piPo;
			}
			set
			{
				this.piPo = value;
			}
		}

		/// <summary>
		/// ��ǰ�����
		/// </summary>
		public System.String OpbOpa
		{
			get
			{
				return this.opbOpa;
			}
			set
			{
				this.opbOpa = value;
			}
		}

		/// <summary>
		/// �ٴ�X�����
		/// </summary>
		public System.String ClX
		{
			get
			{
				return this.clX;
			}
			set
			{
				this.clX = value;
			}
		}

		/// <summary>
		/// �ٴ�CT����
		/// </summary>
		public System.String ClCt
		{
			get
			{
				return this.clCt;
			}
			set
			{
				this.clCt = value;
			}
		}

		/// <summary>
		/// �ٴ�MRI����
		/// </summary>
		public System.String ClMri
		{
			get
			{
				return this.clMri;
			}
			set
			{
				this.clMri = value;
			}
		}

		/// <summary>
		/// �ٴ��������
		/// </summary>
		public System.String ClPa
		{
			get
			{
				return this.clPa;
			}
			set
			{
				this.clPa = value;
			}
		}

		/// <summary>
		/// ���䲡�����
		/// </summary>
		public System.String FsBl
		{
			get
			{
				return this.fsBl;
			}
			set
			{
				this.fsBl = value;
			}
		}

		/// <summary>
		/// ���ȴ���
		/// </summary>
		public System.Int32 SalvTimes
		{
			get
			{
				return this.salvTimes;
			}
			set
			{
				this.salvTimes = value;
			}
		}

		/// <summary>
		/// �ɹ�����
		/// </summary>
		public System.Int32 SuccTimes
		{
			get
			{
				return this.succTimes;
			}
			set
			{
				this.succTimes = value;
			}
		}

		/// <summary>
		/// ʾ�̿���
		/// </summary>
		public System.String TechSerc
		{
			get
			{
				return this.techSerc;
			}
			set
			{
				this.techSerc = value;
			}
		}

		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public System.String VisiStat
		{
			get
			{
				return this.visiStat;
			}
			set
			{
				this.visiStat = value;
			}
		}

		/// <summary>
		/// �������
		/// </summary>
		public System.DateTime VisiPeri
		{
			get
			{
				return this.visiPeri;
			}
			set
			{
				this.visiPeri = value;
			}
		}

		/// <summary>
		/// Ժ�ʻ������
		/// </summary>
		public System.Int32 InconNum
		{
			get
			{
				return this.inconNum;
			}
			set
			{
				this.inconNum = value;
			}
		}

		/// <summary>
		/// Զ�̻������
		/// </summary>
		public System.Int32 OutconNum
		{
			get
			{
				return this.outconNum;
			}
			set
			{
				this.outconNum = value;
			}
		}

		/// <summary>
		/// ҩ�����
		/// </summary>
		public System.String AnaphyFlag
		{
			get
			{
				return this.anaphyFlag;
			}
			set
			{
				this.anaphyFlag = value;
			}
		}

		/// <summary>
		/// ����ҩ������
		/// </summary>
		public System.String AnaphyName1
		{
			get
			{
				return this.anaphyName1;
			}
			set
			{
				this.anaphyName1 = value;
			}
		}

		/// <summary>
		/// ����ҩ������
		/// </summary>
		public System.String AnaphyName2
		{
			get
			{
				return this.anaphyName2;
			}
			set
			{
				this.anaphyName2 = value;
			}
		}

		/// <summary>
		/// ���ĺ��Ժ����
		/// </summary>
		public System.DateTime CoutDate
		{
			get
			{
				return this.coutDate;
			}
			set
			{
				this.coutDate = value;
			}
		}

		/// <summary>
		/// ����ҽʦ����
		/// </summary>
		public System.String RefresherDocd
		{
			get
			{
				return this.refresherDocd;
			}
			set
			{
				this.refresherDocd = value;
			}
		}

		/// <summary>
		/// ����ҽ������
		/// </summary>
		public System.String RefresherDonm
		{
			get
			{
				return this.refresherDonm;
			}
			set
			{
				this.refresherDonm = value;
			}
		}

		/// <summary>
		/// �о���ʵϰҽʦ����
		/// </summary>
		public System.String GraDocCode
		{
			get
			{
				return this.graDocCode;
			}
			set
			{
				this.graDocCode = value;
			}
		}

		/// <summary>
		/// �о���ʵϰҽʦ����
		/// </summary>
		public System.String GraDocName
		{
			get
			{
				return this.graDocName;
			}
			set
			{
				this.graDocName = value;
			}
		}

		/// <summary>
		/// ʵϰҽʦ����
		/// </summary>
		public System.String PraDocCode
		{
			get
			{
				return this.praDocCode;
			}
			set
			{
				this.praDocCode = value;
			}
		}

		/// <summary>
		/// ʵϰҽʦ����
		/// </summary>
		public System.String PraDocName
		{
			get
			{
				return this.praDocName;
			}
			set
			{
				this.praDocName = value;
			}
		}

		/// <summary>
		/// ����Ա����
		/// </summary>
		public System.String CodingCode
		{
			get
			{
				return this.codingCode;
			}
			set
			{
				this.codingCode = value;
			}
		}

		/// <summary>
		/// ����Ա����
		/// </summary>
		public System.String CodingName
		{
			get
			{
				return this.codingName;
			}
			set
			{
				this.codingName = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public System.String MrQual
		{
			get
			{
				return this.mrQual;
			}
			set
			{
				this.mrQual = value;
			}
		}

		/// <summary>
		/// �ϸ񲡰�
		/// </summary>
		public System.String MrElig
		{
			get
			{
				return this.mrElig;
			}
			set
			{
				this.mrElig = value;
			}
		}

		/// <summary>
		/// �ʿ�ҽʦ����
		/// </summary>
		public System.String QcDocd
		{
			get
			{
				return this.qcDocd;
			}
			set
			{
				this.qcDocd = value;
			}
		}

		/// <summary>
		/// �ʿ�ҽʦ����
		/// </summary>
		public System.String QcDonm
		{
			get
			{
				return this.qcDonm;
			}
			set
			{
				this.qcDonm = value;
			}
		}

		/// <summary>
		/// �ʿػ�ʿ����
		/// </summary>
		public System.String QcNucd
		{
			get
			{
				return this.qcNucd;
			}
			set
			{
				this.qcNucd = value;
			}
		}

		/// <summary>
		/// �ʿػ�ʿ����
		/// </summary>
		public System.String QcNunm
		{
			get
			{
				return this.qcNunm;
			}
			set
			{
				this.qcNunm = value;
			}
		}

		/// <summary>
		/// ���ʱ��
		/// </summary>
		public System.DateTime CheckDate
		{
			get
			{
				return this.checkDate;
			}
			set
			{
				this.checkDate = value;
			}
		}

		/// <summary>
		/// ���������������ơ���顢���Ϊ��Ժ��һ����Ŀ
		/// </summary>
		public System.String YnFirst
		{
			get
			{
				return this.ynFirst;
			}
			set
			{
				this.ynFirst = value;
			}
		}

		/// <summary>
		/// RhѪ��(������)
		/// </summary>
		public System.String RhBlood
		{
			get
			{
				return this.rhBlood;
			}
			set
			{
				this.rhBlood = value;
			}
		}

		/// <summary>
		/// ��Ѫ��Ӧ���С��ޣ�
		/// </summary>
		public System.String ReactionBlood
		{
			get
			{
				return this.reactionBlood;
			}
			set
			{
				this.reactionBlood = value;
			}
		}

		/// <summary>
		/// ��ϸ����
		/// </summary>
		public System.String BloodRed
		{
			get
			{
				return this.bloodRed;
			}
			set
			{
				this.bloodRed = value;
			}
		}

		/// <summary>
		/// ѪС����
		/// </summary>
		public System.String BloodPlatelet
		{
			get
			{
				return this.bloodPlatelet;
			}
			set
			{
				this.bloodPlatelet = value;
			}
		}

		/// <summary>
		/// Ѫ����
		/// </summary>
		public System.String BloodPlasma
		{
			get
			{
				return this.bloodPlasma;
			}
			set
			{
				this.bloodPlasma = value;
			}
		}

		/// <summary>
		/// ȫѪ��
		/// </summary>
		public System.String BloodWhole
		{
			get
			{
				return this.bloodWhole;
			}
			set
			{
				this.bloodWhole = value;
			}
		}

		/// <summary>
		/// ������Ѫ��
		/// </summary>
		public System.String BloodOther
		{
			get
			{
				return this.bloodOther;
			}
			set
			{
				this.bloodOther = value;
			}
		}

		/// <summary>
		/// X���
		/// </summary>
		public System.String XNumb
		{
			get
			{
				return this.xNumb;
			}
			set
			{
				this.xNumb = value;
			}
		}

		/// <summary>
		/// CT��
		/// </summary>
		public System.String CtNumb
		{
			get
			{
				return this.ctNumb;
			}
			set
			{
				this.ctNumb = value;
			}
		}

		/// <summary>
		/// MRI��
		/// </summary>
		public System.String MriNumb
		{
			get
			{
				return this.mriNumb;
			}
			set
			{
				this.mriNumb = value;
			}
		}

		/// <summary>
		/// �����
		/// </summary>
		public System.String PathNumb
		{
			get
			{
				return this.pathNumb;
			}
			set
			{
				this.pathNumb = value;
			}
		}

		/// <summary>
		/// DSA��
		/// </summary>
		public System.String DsaNumb
		{
			get
			{
				return this.dsaNumb;
			}
			set
			{
				this.dsaNumb = value;
			}
		}

		/// <summary>
		/// PET��
		/// </summary>
		public System.String PetNumb
		{
			get
			{
				return this.petNumb;
			}
			set
			{
				this.petNumb = value;
			}
		}

		/// <summary>
		/// ECT��
		/// </summary>
		public System.String EctNumb
		{
			get
			{
				return this.ectNumb;
			}
			set
			{
				this.ectNumb = value;
			}
		}

		/// <summary>
		/// X�ߴ���
		/// </summary>
		public System.Int32 XTimes
		{
			get
			{
				return this.xTimes;
			}
			set
			{
				this.xTimes = value;
			}
		}

		/// <summary>
		/// CT����
		/// </summary>
		public System.Int32 CtTimes
		{
			get
			{
				return this.ctTimes;
			}
			set
			{
				this.ctTimes = value;
			}
		}

		/// <summary>
		/// MR����
		/// </summary>
		public System.Int32 MrTimes
		{
			get
			{
				return this.mrTimes;
			}
			set
			{
				this.mrTimes = value;
			}
		}

		/// <summary>
		/// DSA����
		/// </summary>
		public System.Int32 DsaTimes
		{
			get
			{
				return this.dsaTimes;
			}
			set
			{
				this.dsaTimes = value;
			}
		}

		/// <summary>
		/// PET����
		/// </summary>
		public System.Int32 PetTimes
		{
			get
			{
				return this.petTimes;
			}
			set
			{
				this.petTimes = value;
			}
		}

		/// <summary>
		/// ECT����
		/// </summary>
		public System.Int32 EctTimes
		{
			get
			{
				return this.ectTimes;
			}
			set
			{
				this.ectTimes = value;
			}
		}

		/// <summary>
		/// �鵵�����
		/// </summary>
		public System.String BarCode
		{
			get
			{
				return this.barCode;
			}
			set
			{
				this.barCode = value;
			}
		}

		/// <summary>
		/// ��������״̬(O��� I�ڼ�)
		/// </summary>
		public System.String LendStus
		{
			get
			{
				return this.lendStus;
			}
			set
			{
				this.lendStus = value;
			}
		}

		/// <summary>
		/// ����״̬1�����ʼ�/2�ǼǱ���/3����/4�������ʼ�/5��Ч
		/// </summary>
		public System.String CaseStus
		{
			get
			{
				return this.caseStus;
			}
			set
			{
				this.caseStus = value;
			}
		}

		/// <summary>
		/// ����Ա
		/// </summary>
		public System.String OperCode
		{
			get
			{
				return this.operCode;
			}
			set
			{
				this.operCode = value;
			}
		}

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public System.DateTime OperDate
		{
			get
			{
				return this.operDate;
			}
			set
			{
				this.operDate = value;
			}
		}
		/// <summary>
		/// ���߻�������
		/// </summary>
		public neusoft.HISFC.Object.RADT.PatientInfo PatientInfo
		{
			get
			{
				return this.patientInfo;
			}
			set
			{
				this.patientInfo = value;
			}
		}
		#endregion
		#region ����
		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns></returns>
		public new Base Clone()
		{
			Base b = base.MemberwiseClone() as Base;
			
			b.patientInfo = this.patientInfo.Clone();
			b.packupCode = this.packupCode.Clone();
			b.deptIN = this.deptIN.Clone();
			b.deptOut = this.deptOut.Clone();

			return b;
		}
		#endregion
	}
}
