using System;
using neusoft.neuFC.Object;

namespace neusoft.HISFC.Object.Case
{
	/// <summary>
	/// Cases ��ժҪ˵����
	/// </summary>
	public class Case
	{
		public Case()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ������
		/// </summary>
		public string noMen = "" ;
		#region ���䶨��
		/// <summary>
		/// ����
		/// </summary>
		public int age
		{
			get
			{
				return age;
			}
			set
			{
				if(	this.ExLength( value, 3, "����" ) )
				{
					age = value;
				}
			}
		}
		#endregion
		#region ���䵥λ����
		/// <summary>
		/// ���䵥λ
		/// </summary>
		public string ageUnit
		{
			get
			{
				return ageUnit;
			}
			set
			{
				if( this.ExLength( value, 4,"���䵥λ" ) )
				{
					ageUnit = value;
				}
			}
		}
		#endregion
		/// <summary>
		/// ������Դ
		/// </summary>
		enum InSource
		{
			BQ = 1, //����
			BS = 2, //����
			WS = 3, //����
			OS = 4, //��ʡ
			GA = 5, //�۰�̨
			WG = 6  //���
		}
		#region ��ͥ�ʱ�
		/// <summary>
		/// ��ͥ�ʱ�
		/// </summary>
		
		public string homeZip
		{
			get
			{
				return homeZip;
			}
			set
			{
				if( this.ExLength( value, 6, "��ͥ�ʱ�" ) )
				{
					homeZip = value;
				}
			}
		}
		#endregion
		#region ��λ�ʱඨ��
		/// <summary>
		/// ��λ�ʱ�
		/// </summary>
		public string businessZip
		{
			get
			{
				return businessZip;
			}
			set
			{
				if( this.ExLength( value, 6, "��λ�ʱ�" ) )
				{
					businessZip = value;
				}
			}
		}
		#endregion
		#region ������ϴ��붨��
		/// <summary>
		/// ������ϴ���
		/// </summary>
		public string clinicICD
		{
			set
			{
				if( this.ExLength( value, 10, "������ϴ���" ) )
				{
					clinicICD = value;
				}
			}
		}
		#endregion
		#region ����������ƶ���
		/// <summary>
		/// �����������
		/// </summary>
		public string clinicICDName
		{
			set
			{
				if( this.ExLength( value, 50, "�����������" ) )
				{
					clinicICDName = value;
				}
			}
		}
		#endregion
		#region סԺ��������
		/// <summary>
		/// סԺ���� int
		/// </summary>
		public int inTimes
		{
			set
			{
				if( this.ExLength( value, 2, "סԺ����" ) )
				{
					inTimes = value;
				}
			}
		}
		#endregion
		#region ȷ�����ڶ���
		/// <summary>
		/// ȷ������
		/// </summary>
		public DateTime diagDate;
		#endregion
		#region �������ڶ���
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime deatDate;
		#endregion
		#region ����ԭ���� string
		/// <summary>
		/// ����ԭ�� string
		/// </summary>
		public string deadReason
		{
			set
			{
				if( this.ExLength( value, 50, "����ԭ��" ) )
				{
					deadReason = value;
				}
			}
		}
		#endregion
		#region ʬ�춨�� string
		/// <summary>
		/// ʬ�춨�� string
		/// </summary>
		public string bodyCheck
		{
			set
			{
				if( this.ExLength( value, 1, "ʬ����" ) )
				{
					bodyCheck = value;
				}
			}
		}
		#endregion
		#region ʬ����ʺŶ��� string Varchar(10)
		/// <summary>
		/// ʬ����ʺ� string Varchar(10)
		/// </summary>
		public string bodyAnotomize
		{
			set
			{
				if( this.ExLength( value, 10, "ʬ����ʺ�" ) )
				{
					bodyAnotomize = value;
				}
			}
		}
		#endregion
		#region ���ȴ������� int Number(2)
		/// <summary>
		/// ���ȴ��� int Number(2)
		/// </summary>
		public int salvTimes
		{
			set
			{
				if( this.ExLength( value, 2, "���ȴ���" ) )
				{
					salvTimes = value;
				}
			}
		}
		#endregion
		#region ���ȳɹ��������� int Number(2)
		/// <summary>
		/// ���ȳɹ����� int Number(2)
		/// </summary>
		public int succTimes
		{
			set
			{
				if( this.ExLength( value, 2, "���ȳɹ�����" ) )
				{
					succTimes = value;
				}
			}
		}
		#endregion
		#region ʾ�̿��ж��� string Varchar(1)
		/// <summary>
		/// ʾ�̿��� string Varchar(1)
		/// </summary>
		public string techSerc
		{
			set
			{
				if( this.ExLength( value, 1, "ʾ�̿���" ) )
				{
					techSerc = value;
				}
			}
		}
		#endregion
		#region �Ƿ����ﶨ�� string Varchar(1)
		/// <summary>
		/// �Ƿ����� string Varchar(1)
		/// </summary>
		public string visiStat
		{
			set
			{
				if( this.ExLength( value, 1, "�Ƿ�����" ) )
				{
					visiStat = value;
				}
			}
		}
		#endregion
		#region ������޶��� DateTime Date
		/// <summary>
		///  ������� DateTime Date
		/// </summary>
		public DateTime visiPeri;
		#endregion
		#region Ժ�ʻ���������� int Number(2)
		/// <summary>
		/// Ժ�ʻ������ int Number(2)
		/// </summary>
		public int inConTimes
		{
			set
			{
				if( this.ExLength( value, 2, "Ժ�ʻ������" ) )
				{
					inConTimes = value;
				}
			}
		}
		#endregion
		#region Զ�̻���������� int Number(2)
		/// <summary>
		/// Զ�̻������ int Number(2)
		/// </summary>
		public int outConTimes
		{
			set
			{
				if( this.ExLength( value, 2, "���ȳɹ�����" ) )
				{
					outConTimes = value;
				}
			}
		}
		#endregion
		#region ҩ�������־���� string Varchar(1)
		/// <summary>
		/// ҩ�������־ string Varchar(1)
		/// </summary>
		public string anaphyFlag
		{
			set
			{
				if( this.ExLength( value, 1, "ҩ�������־" ) )
				{
					anaphyFlag = value;
				}
			}
		}
		#endregion
		#region ����ҩ�����ƶ��� string Varchar(50)
		/// <summary>
		/// ����ҩ������ string Varchar(50)
		/// </summary>
		public int anaphyName
		{
			set
			{
				if( this.ExLength( value, 50, "����ҩ������" ) )
				{
					anaphyName = value;
				}
			}
		}
		#endregion
		#region �Ƿ������Ժ��־���� string Varchar(1)
		/// <summary>
		/// �Ƿ������Ժ��־ string Varchar(1)
		/// </summary>
		public string secoIn
		{
			set
			{
				if( this.ExLength( value, 1, "�Ƿ������Ժ��־" ) )
				{
					secoIn = value;
				}
			}
		}
		#endregion
		#region 167�������ඨ�� string Varchar(10)
		/// <summary>
		/// 167�������ඨ�� string Varchar(10)
		/// </summary>
		public string type167
		{
			set
			{
				if( this.ExLength( value, 10, "167�������ඨ��" ) )
				{
					type167 = value;
				}
			}
		}
		#endregion
		#region �����ж����ඨ�� string Varchar(10)
		/// <summary>
		/// �����ж����� string Varchar(10))
		/// </summary>
		public string type167E
		{
			set
			{
				if( this.ExLength( value, 10, "�����ж�����" ) )
				{
					type167E = value;
				}
			}
		}
		#endregion
		#region ȷ���������� int Number(5)
		/// <summary>
		///  ȷ���������� int Number(5)
		/// </summary>
		public int diagDays
		{
			set
			{
				if( this.ExLength( value, 5, "ȷ������" ) )
				{
					diagDays = value;
				}
			}
		}
		#endregion
		#region סԺ�������� int Number(5)
		/// <summary>
		/// סԺ�������� int Number(5)
		/// </summary>
		public string piDays
		{
			set
			{
				if( this.ExLength( value, 5, "סԺ����" ) )
				{
					piDays = value;
				}
			}
		}
		#endregion
		#region ��Ӧ���˶��� string Varchar2(1)
		/// <summary>
		/// ��Ӧ���� 1����Ӧ���˱��� 2������Ӧ���˱���  string Varchar2(1)
		/// </summary>
		public string diagToe
		{
			set
			{
				if( this.ExLength( value, 1, "��Ӧ����" ) )
				{
					diagToe = value;
				}
			}
		}
		#endregion
		#region ���ĺ��Ժ���ڶ��� DateTime Date
		/// <summary>
		///  ���ĺ��Ժ���� DateTime Date
		/// </summary>
		public DateTime coutDate;
		#endregion
		#region �Ƿ�ҽԺ��Ⱦ���� string Varchar(1)
		/// <summary>
		/// �Ƿ�ҽԺ��Ⱦ string Varchar(1)
		/// </summary>
		public string ynInfect
		{
			set
			{
				if( this.ExLength( value, 1, "�Ƿ�ҽԺ��Ⱦ" ) )
				{
					ynInfect = value;
				}
			}
		}
		#endregion
		#region �Ƿ���ض����� string Varchar(1)
		/// <summary>
		/// �Ƿ���ض� string Varchar(1)
		/// </summary>
		public string ynLowWeigh
		{
			set
			{
				if( this.ExLength( value, 1, "�Ƿ���ض�" ) )
				{
					ynLowWeigh = value;
				}
			}
		}
		#endregion
		#region ������ʽ���� string Varchar(2)
		/// <summary>
		/// ������ʽ string Varchar(2)
		/// </summary>
		public string birthMode
		{
			set
			{
				if( this.ExLength( value, 2, "������ʽ" ) )
				{
					birthMode = value;
				}
			}
		}
		#endregion
		#region �ѳ�������� string Varchar(2)
		/// <summary>
		/// �ѳ���� string Varchar(2)
		/// </summary>
		public string birthEnd
		{
			set
			{
				if( this.ExLength( value, 2, "�ѳ����" ) )
				{
					birthEnd = value;
				}
			}
		}
		#endregion
		#region �Ƿ�Ӥ������ string Varchar(1)
		/// <summary>
		///�Ƿ�Ӥ������ string Varchar(1)
		/// </summary>
		public string ynBaby
		{
			set
			{
				if( this.ExLength( value, 1, "�Ƿ�Ӥ��" ) )
				{
					ynBaby = value;
				}
			}
		}
		#endregion
		#region ������������ string Varchar(1)
		/// <summary>
		/// �������� string Varchar(1)
		/// </summary>
		public string mrQual
		{
			set
			{
				if( this.ExLength( value, 1, "��������" ) )
				{
					mrQual = value;
				}
			}
		}
		#endregion
		#region �ϸ񲡰����� string Varchar(1)
		/// <summary>
		/// �ϸ񲡰� string Varchar(1)
		/// </summary>
		public string mrElig
		{
			set
			{
				if( this.ExLength( value, 1, "�ϸ񲡰�" ) )
				{
					mrElig = value;
				}
			}
		}
		#endregion
		#region ���ʱ�䶨�� DateTime Date
		/// <summary>
		/// ���ʱ��DateTime Date
		/// </summary>
		public DateTime checkDate;
		#endregion
		#region ���������������ơ���顢���Ϊ��Ժ��һ����Ŀ���� string Varchar(1)
		/// <summary>
		/// ���������������ơ���顢���Ϊ��Ժ��һ����Ŀ string Varchar(1)
		/// </summary>
		public string ynFirst
		{
			set
			{
				if( this.ExLength( value, 1, "���������������ơ���顢���Ϊ��Ժ��һ����Ŀ" ) )
				{
					ynFirst = value;
				}
			}
		}
		#endregion
		#region ˵������ string Varchar(200)
		/// <summary>
		/// ˵�� string Varchar(200)
		/// </summary>
		public string remark
		{
			set
			{
				if( this.ExLength( value, 200, "˵��" ) )
				{
					remark = value;
				}
			}
		}
		#endregion
		#region �鵵����Ŷ��� string Varchar(16)
		/// <summary>
		/// �鵵����� string Varchar(16)
		/// </summary>
		public string barCode
		{
			set
			{
				if( this.ExLength( value, 16, "�鵵�����" ) )
				{
					barCode = value;
				}
			}
		}
		#endregion
		#region ��������״̬(O��� I�ڼ�)���� string Varchar(1)
		/// <summary>
		/// ��������״̬(O��� I�ڼ�) string Varchar(1)
		/// </summary>
		public string lendStus
		{
			set
			{
				if( this.ExLength( value, 1, "��������״̬" ) )
				{
					lendStus = value;
				}
			}
		}
		#endregion
		#region ����״̬����   string Varchar(1)
		/// <summary>
		/// ����״̬ 1�����ʼ�/2�ǼǱ���/3����/4�������ʼ� string Varchar(1)
		/// </summary>
		public string caseStus
		{
			set
			{
				if( this.ExLength( value, 1, "����״̬" ) )
				{
					caseStus = value;
				}
			}
		}
		#endregion
		#region ����Ա���� string Varchar(6)
		/// <summary>
		/// ����ԱID string Varchar(6)
		/// </summary>
		public string operCode
		{
			set
			{
				if( this.ExLength( value, 6, "����ԱID" ) )
				{
					operCode = value;
				}
			}
		}
		#endregion
		#region ����ʱ�䶨�� DateTime
		/// <summary>
		/// ����ʱ�� DateTime
		/// </summary>
		public DateTime operDate;
		#endregion

		/// <summary>
		/// ��Ժ״̬
		/// </summary>
		public neuObject InCircs = new neuObject();
		/// <summary>
		/// ��Ժ���
		/// </summary>
		public neuObject OutICD = new neuObject();
		/// <summary>
		/// ��Ժ������Ϣ
		/// </summary>
		public neuObject OutDept = new neuObject();
		/// <summary>
		/// ��������
		/// </summary>
		public neuObject DeadKind = new neuObject();
		/// <summary>
		/// ĸ����Ϣ ID ĸ��סԺ�� Name ĸ������
		/// </summary>
		public neuObject MotherInfo = new neuObject();
		/// <summary>
		/// ����Ա��Ϣ  ID ����Ա��� Name ����Ա����
		/// </summary>
		public neuObject CodingInfo = new neuObject();
		/// <summary>
		/// �ʿ�ҽʦ��Ϣ ID �ʿ�ҽʦ��� Name �ʿ�ҽʦ����
		/// </summary>
		public neuObject QCDocInfo = new neuObject();
		/// <summary>
		/// �ʿػ�ʿ ID �ʿػ�ʿ��� Name �ʿػ�ʿ����
		/// </summary>
		public neuObject QCNurInfo = new neuObject();
		/// <summary>
		/// ���߼�黯����������
		/// </summary>
		public Quanlity QuanlityControl = new Quanlity();
		/// <summary>
		/// ������黯����������Ӧ����
		/// </summary>
		public CheckInfo CheckInfoPatient = new CheckInfo();
		/// <summary>
		/// ��Ա������Ϣ
		/// </summary>
		public neusoft.HISFC.Object.RADT.PatientInfo PatientInfo = new neusoft.HISFC.Object.RADT.PatientInfo();

		/*
		private void look()
		{
			Pi.Patient.PID.PatientNo = "";//סԺ��
			Pi.Patient.PID.CardNo = "";//������
			Pi.Patient.Name = "";//��������
			//Pv.Date_In = null;//��Ժ����
			Pi.Patient.Sex.ID = "";//�Ա����
			//Pi.Patient.Birthday = null;//����
			Pi.Patient.Country.ID = null;//���ұ���
			Pi.Patient.Nationality.ID =null;//�������;
			Pi.Patient.Profession.ID = null;//ְҵ����
			Pi.Patient.BloodType.ID = null;//Ѫ�ͱ���
			Pi.Patient.MaritalStatus.ID = null;//�Ƿ��
			Pi.Patient.IDNo = "";//���֤��
			Pi.PayKind.ID = "";//�������
			Pi.Patient.Pact.ID = "";//��ͬ����
			Pi.Patient.SSN = ""; //ҽ�����
			Pi.Patient.AddressHome ="";//��ͥסַ
			Pi.Patient.PhoneHome = "";//��ͥ�绰
			Pi.Patient.AddressBusiness = "";//��λ��ַ
			Pi.Patient.PhoneBusiness = "" ;//��λ�绰
			Pi.PVisit.PatientLocation.Dept.ID ="";//��Ժ���Ҵ���
			Pi.PVisit.PatientLocation.Dept.Name = "";//��Ժ��������
		}*/
		/// <summary>
		/// �ж����Ƿ񳬹������й涨���ȣ�������������׳��쳣
		/// </summary>
		/// <param name="obj">������</param>
		/// <param name="length">��Ӧ�������ƶ�����</param>
		/// <param name="exMessage">�쳣������Ϣ</param>
		/// <returns>����ֵ True�������� ����ֱ���׳��쳣</returns>
		private bool ExLength( System.Object Obj, int length, string exMessage )
		{
			if( Obj.ToString().Length > length )
			{
				Exception ExLength = new Exception( exMessage + " ����" + length.ToString() + "λ��" );
				ExLength.Source = Obj.ToString();
				throw ExLength;
			}
			else
			{
				return true;
			}
		}

		public new Case Clone()
		{
			Case CaseClone = base.MemberwiseClone() as Case;

			CaseClone.CodingInfo = this.CodingInfo.Clone();
			CaseClone.DeadKind = this.DeadKind.Clone();
			CaseClone.InCircs = this.InCircs.Clone();
			CaseClone.MotherInfo = this.MotherInfo.Clone();
			CaseClone.OutDept = this.OutDept.Clone();
			CaseClone.OutICD = this.OutICD.Clone();
			CaseClone.PatientInfo = this.PatientInfo.Clone();
			CaseClone.QCDocInfo = this.QCDocInfo.Clone();
			CaseClone.QCNurInfo = this.QCNurInfo.Clone();
			CaseClone.QuanlityControl = this.QuanlityControl.Clone();
			CaseClone.CheckInfoPatient = this.CheckInfoPatient.Clone();

			return CaseClone;
		}
		
	}
}
