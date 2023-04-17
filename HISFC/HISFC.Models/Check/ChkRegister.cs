using System;
using System.Collections;
namespace neusoft.HISFC.Object.Check
{
	/// <summary>
	/// ���Ǽ��� ID ,
	/// </summary>
	public class ChkRegister : neusoft.neuFC.Object.neuObject ,neusoft.HISFC.Object.Base.ISpellCode
	{
		public ChkRegister()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		//���������� t����
		#region  ˽�б��� 
		private string Chk_Id;  //��쵵���� 
		private string chkClinicNo; //�����ˮ�� 
		private string anaphy_flag ;
		private string SpellCode;
		private string wbCode;
		private string userCode;
		private string chk_level;
		private string bloodPressTop;//Ѫѹ���ֵ
		private string bloodPressDown;//Ѫѹ���ֵ
		private decimal ownCost; //���
		private neusoft.neuFC.Object.neuObject dutyNuse = null;
		private neusoft.neuFC.Object.neuObject  chkCompany  = null;
		private neusoft.HISFC.Object.RADT.PatientInfo patientInfo = null;
		private string extCha = ""; //��չ���1
		private System.DateTime extDate ; //��չ���2
		private int extNum ; //��չ���3 
		private string extCha1 ;//��չ���4 
		private System.DateTime extDate1 ; //��չ��� 5
		private neusoft.neuFC.Object.neuObject regDept = null;
		//����Ա��Ϣ
		private neusoft.HISFC.Object.RADT.Person oper = new neusoft.HISFC.Object.RADT.Person();
		private int extNum1 ; // ��չ��� 6
		private string collectivityCode; //��������
		private System.DateTime collectivityDate;//�������Ǽ�����
		private string deptName ; //��쵥λ ����
		private string deptSeq = ""; //��������
		#endregion
		/// <summary>
		/// ��������
		/// </summary>
		public string DeptSeq
		{
			get
			{
				return deptSeq;
			}
			set
			{
				deptSeq = value;
			}
		}
		/// <summary>
		/// ��쵥λ����
		/// </summary>
		public string DeptName
		{
			get
			{
				return deptName;
			}
			set
			{
				deptName = value;
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
		///  //��չ���5
		/// </summary>
		public  System.DateTime ExtDate1
		{
			get
			{
				return extDate1;
			}
			set
			{
				extDate1 = value;
			}
		}
		/// <summary>
		///  //��չ���2
		/// </summary>
		public  System.DateTime ExtDate
		{
			get
			{
				return extDate;
			}
			set
			{
				extDate = value;
			}
		}
		/// <summary>
		/// ��չ���3 
		/// </summary>
		public int ExtNum1
		{
			get
			{
				return extNum1;
			}
			set
			{
				extNum1 = value;
			}
		}
		/// <summary>
		/// ��չ���3 
		/// </summary>
		public int ExtNum
		{
			get
			{
				return extNum;
			}
			set
			{
				extNum = value;
			}
		}
		/// <summary>
		/// ��չ���
		/// </summary>
		public string ExtCha1
		{
			get
			{
				return extCha1;
			}
			set
			{
				extCha1 = value;
			}
		}
		/// <summary>
		/// ��չ���
		/// </summary>
		public string ExtCha
		{
			get
			{
				return extCha;
			}
			set
			{
				extCha = value;
			}
		}
		/// <summary>
		/// �Һſ���
		/// </summary>
		public neusoft.neuFC.Object.neuObject RegDept 
		{
			get
			{
				if(regDept == null)
				{
					regDept = new neusoft.neuFC.Object.neuObject();
				}
				return  regDept;
			}
			set
			{
				regDept = value;
			}
		}
		private neusoft.neuFC.Object.neuObject operDept = null; //����Ա����
		/// <summary>
		/// ����Ա����
		/// </summary>
		public neusoft.neuFC.Object.neuObject OperDept 
		{
			get
			{
				if(operDept == null)
				{
					operDept = new neusoft.neuFC.Object.neuObject();
				}
				return operDept;
			}
			set
			{
				operDept = value;
			}
		}
		/// <summary>
		/// ���λ�ʿ
		/// </summary>
		public neusoft.neuFC.Object.neuObject DutyNuse
		{
			get
			{
				if(dutyNuse == null)
				{
					dutyNuse = new neusoft.neuFC.Object.neuObject();
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
		/// <summary>
		/// ����1 ���� 2  ����
		/// </summary>
		public string CHKKind ;
		/// <summary>
		/// //��ʷ
		/// </summary>
		public string CaseHospital; 
		/// <summary>
		///  //��ͥ��ʷ
		/// </summary>
		public string HomeCase;
		/// <summary>
		/// �������
		/// </summary>
		public System.DateTime CheckDate ;//
		/// <summary>
		/// ������ 
		/// </summary>
		public int CHKSortNo ;//������ 
		/// <summary>
		/// ��������
		/// </summary>
		public string TransType ;//��������
		/// <summary>
		///�����Ŀ
		/// </summary>
		public neusoft.HISFC.Object.Base.Item item = new neusoft.HISFC.Object.Base.Item(); 
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
		public  neusoft.neuFC.Object.neuObject  ChkCompany
		{
			get
			{
				if(chkCompany == null)
				{
					chkCompany = new neusoft.neuFC.Object.neuObject();
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
		public neusoft.HISFC.Object.RADT.PatientInfo PatientInfo
		{
			get
			{
				if(patientInfo == null)
				{
					patientInfo = new neusoft.HISFC.Object.RADT.PatientInfo();
				}
				return patientInfo;
			}
			set
			{
				patientInfo = value;
			}
		}

		public ArrayList chkItemList = new  ArrayList();
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
		public neusoft.HISFC.Object.RADT.Person Operator 
		{
			get
			{
				if(oper == null)
				{
					oper = new neusoft.HISFC.Object.RADT.Person();
				}
				return oper;
			}
			set
			{
				oper = value;
			}
		}
		#region ISpellCode ��Ա
		public string Spell_Code
		{
			get
			{
				// TODO:  ��� Const.Spell_Code getter ʵ��
				return this.SpellCode;
			}
			set
			{
				// TODO:  ��� Const.Spell_Code setter ʵ��
				this.SpellCode=value;
			}
		}

		public string WB_Code
		{
			get
			{
				// TODO:  ��� Const.WB_Code getter ʵ��
				return this.wbCode;
			}
			set
			{
				// TODO:  ��� Const.WB_Code setter ʵ��
				this.wbCode = value;
			}
		}

		public string User_Code
		{
			get
			{
				// TODO:  ��� Const.User_Code getter ʵ��
				return this.userCode;
			}
			set
			{
				// TODO:  ��� Const.User_Code setter ʵ��
				this.userCode = value;
			}
		}

		#endregion
		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns></returns>
		public new ChkRegister Clone()
		{
			ChkRegister obj = base.Clone() as ChkRegister;
			obj.item= this.item.Clone();
			obj.ChkCompany=this.ChkCompany.Clone();//(neusoft.HISFC.Object.Fee.Invoice)Invoice.Clone();
			obj.PatientInfo=this.PatientInfo.Clone();
			return obj;
		}

	}
}
