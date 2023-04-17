using System;
using neusoft.neuFC;
using neusoft.HISFC;
using System.Collections;

namespace neusoft.HISFC.Object.Operator
{
	/// <summary>
	/// �������뵥�� Written By liling
	/// </summary>
	public class OpsApplication:neusoft.neuFC.Object.neuObject,
		neusoft.HISFC.Object.Base.IDept
	{
		public OpsApplication()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		
		/// <summary>
		/// ������Ա�Ľ�ɫ��������
		/// �����д��Ԫ��Ϊneusoft.HISFC.Object.Operator.ArrangeRole���Ͷ���
		/// ��ʵ���涨��� ����ҽ����ָ��ҽ��������ҽ�������Զ����Ժϲ�����������е�
		/// ֻ��Ϊ���ر�ǿ�������벿�ֵ���Ϣ�����ص����ඨ�岢��ֵ��Щ����
		/// </summary>		
		public ArrayList RoleAl = new ArrayList();

		#region ��������ʱ��Ҫ��д������
		//---------------------------------------------------------------
		#region ��ϵͳ��ȡ
		//---------------------------------------
		///<summary>
		///�������к�
		///</summary>
		public string OperationNo = "";
		///<summary>
		///������Ϣ
		///</summary>
		public neusoft.HISFC.Object.RADT.PatientInfo PatientInfo = new neusoft.HISFC.Object.RADT.PatientInfo();
		//---------------------------------------
		#endregion
		///<summary>
		///�������
		///(�п���һ��������Ŷ�Ӧ�����ǰ��ϣ������һ����̬�������洢��Ϣ)
		///(����Ԫ��Ϊneusoft.HISFC.Object.RADT.Diagnose Diagnose����)
		///</summary>
		public ArrayList DiagnoseAl = new ArrayList();
		///<summary>
		///1��������/2סԺ����
		///</summary>
		public string Pasource = "";

		///<summary>
		///����ԤԼʱ��
		///</summary>
		public DateTime Pre_Date = DateTime.MinValue;

		///<summary>
		///����Ԥ����ʱ
		///</summary>
		public decimal Duration = 0;

		///<summary>
		///������Ϣ
		///���п���һ��������Ŷ�Ӧ������������ˣ���һ����̬�������洢��Ϣ������Ԫ��ΪOperateInfo���ͣ�
		///</summary>
		public ArrayList OperateInfoAl = new ArrayList();
		///<summary>
		///��������
		///</summary>
		public neusoft.neuFC.Object.neuObject Anes_type=new neusoft.neuFC.Object.neuObject();

		///<summary>
		///��������(�����Ⱦ����ͨ)
		///</summary>
		public string OperateKind = "1";
		///<summary>
		///������ģ
		///</summary>
		public neusoft.neuFC.Object.neuObject OperateType = new neusoft.neuFC.Object.neuObject();
		
		///<summary>
		///�п�����
		///</summary>
		public neusoft.neuFC.Object.neuObject InciType = new neusoft.neuFC.Object.neuObject();

		///<summary>
		///������λ
		///</summary>
		public neusoft.neuFC.Object.neuObject OpePos = new neusoft.neuFC.Object.neuObject();
		///<summary>
		///����ҽ��
		///</summary>
		public neusoft.HISFC.Object.RADT.Person Ops_docd = new neusoft.HISFC.Object.RADT.Person();

		///<summary>
		///ָ��ҽ��
		///</summary>
		public neusoft.HISFC.Object.RADT.Person Gui_docd = new neusoft.HISFC.Object.RADT.Person();

		///<summary>
		///����ҽ��
		///���п����Ƕ��ˣ���ˣ���һ����̬�������洢��Ϣ������Ԫ��Ϊneusoft.HISFC.Object.RADT.Person���ͣ�
		///</summary>
		public ArrayList HelperAl = new ArrayList();

		///<summary>
		///����ҽ��
		///</summary>
		public neusoft.HISFC.Object.RADT.Person Apply_Doct = new neusoft.HISFC.Object.RADT.Person();

		///<summary>
		///����ʱ��
		///</summary>
		public DateTime Apply_Date = DateTime.MinValue;

		///<summary>
		///���뱸ע
		///</summary>
		public string ApplyNote = "";

		#region ��Ѫ���
		///<summary>
		///ѪҺ�ɷ�(ȫѪ��Ѫ����Ѫ���)
		///</summary>
		public neusoft.neuFC.Object.neuObject BloodType = new neusoft.neuFC.Object.neuObject();
		///<summary>
		///Ѫ��
		///</summary>
		public decimal BloodNum = 0;
		///<summary>
		///��Ѫ��λ
		///</summary>
		public string BloodUnit = "ml";
		#endregion

		///<summary>
		///����ע������
		///</summary>
		public string OpsNote = "";
		///<summary>
		///����ע������
		///</summary>
		public string AneNote = "";
		///<summary>
		///0��̨/1��̨/2��̨
		///</summary>
		public string TableType = "";
		///<summary>
		///����ҽ��
		///</summary>
		public neusoft.HISFC.Object.RADT.Person ApprDocd = new neusoft.HISFC.Object.RADT.Person();
		///<summary>
		///����ʱ��
		///</summary>
		public DateTime ApprDate = DateTime.MinValue;
		///<summary>
		///������ע
		///</summary>
		public string ApprNote = "";
		/// <summary>
		///����Ա
		/// </summary>
		public neusoft.HISFC.Object.RADT.Person User = new neusoft.HISFC.Object.RADT.Person();
		/// <summary>
		/// ǩ�ּ���
		/// </summary>
		public string Folk = "";
		/// <summary>
		/// ������ϵ
		/// </summary>
		public neusoft.neuFC.Object.neuObject RelaCode = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// �������
		/// </summary>
		public string FolkComment = "";
		/// <summary>
		/// ִ�п���
		/// </summary>
		private neusoft.HISFC.Object.Base.Department ExecDept = new neusoft.HISFC.Object.Base.Department();
		//---------------------------------------------------------------
		#endregion

		#region ��������ʱ��Ҫ��д������
		//---------------------------------------------------------------
		///<summary>
		///������
		///</summary>
		public neusoft.HISFC.Object.Base.Department OperateRoom = new neusoft.HISFC.Object.Base.Department(); 
		///<summary>
		///�����
		///</summary>
		public string RoomID = "";

		///<summary>
		///����̨
		///</summary>
		public neusoft.HISFC.Object.Operator.OpsTable OpsTable = new OpsTable();
		/// <summary>
		/// �������ϰ��ż�¼����
		/// Ԫ��Ϊneusoft.HISFC.Object.Operator.OpsApparatusRec����
		/// </summary>		
		public ArrayList AppaRecAl = new ArrayList();
		/// <summary>
		/// 1 �о� 0�޾�
		/// </summary>
		private string YNGerm = ""; 
		public bool bGerm
		{
			get
			{
				if(YNGerm == "1")
					return true;
				else
					return false;
			}
			set
			{
				if(value == true)
					YNGerm = "1";
				else
					YNGerm = "0";
			}
		}
		/// <summary>
		/// 1 �ڲ��鿴���� 2 ҽ���鿴���� 
		/// </summary>
		public string ScreenUp = "";
		//---------------------------------------------------------------
        /// <summary>
        /// ҽ���Ƿ���Բ鿴�������Ž��(1 ��  2����)
        /// </summary>
        public string DocCanSee;

		#endregion

		#region ��־

		///<summary>
		///���뵥״̬(1�������� 2 �������� 3�������� 4�������)
		///</summary>
		public string ExecStatus = "1";

		///<summary>
		///0δ������/1��������
		///</summary>
		private string YNFinished = "0";
		public bool bFinished
		{
			get
			{
				if(YNFinished == "1")
					return true;
				else
					return false;
			}
			set
			{
				if(value == true)
					YNFinished = "1";
				else
					YNFinished = "0";
			}
		}

		///<summary>
		///0δ����/1������
		///</summary>
		private string YNAnesth = "0";
		public bool bAnesth
		{
			get
			{
				if(YNAnesth == "1")
					return true;
				else
					return false;
			}
			set
			{
				if(value == true)
					YNAnesth = "1";
				else
					YNAnesth = "0";
			}
		}
		///<summary>
		///�Ӽ����� 1��/0��
		///</summary>
		private string YNUrgent = "0";
		public bool bUrgent
		{
			get
			{
				if(YNUrgent == "1")
					return true;
				else
					return false;
			}
			set
			{
				if(value == true)
					YNUrgent = "1";
				else 
					YNUrgent = "0";
			}
		}
		///<summary>
		///��Σ 1��/0��
		///</summary>
		private string YNChange = "0";
		public bool bChange
		{
			get
			{
				if(YNChange == "1")
					return true;
				else 
					return false;
			}
			set
			{
				if(value == true)
					YNChange ="1";
				else
					YNChange ="0";
			}
		}

		///<summary>
		///��֢ 1��/0��
		///</summary>
		private string YNHeavy = "0";
		public bool bHeavy
		{
			get
			{
				if(YNHeavy == "1")
					return true;
				else
					return false;
			}
			set
			{
				if(value == true)
					YNHeavy = "1";
				else
					YNHeavy = "0";
			}
		}
		///<summary>
		///�������� 1��0��
		///</summary>
		private string YNSpecial = "0";
		public bool bSpecial
		{
			get
			{
				if(YNSpecial == "1")
					return true;
				else
					return false;
			}
			set
			{
				if(value == true)
					YNSpecial = "1";
				else
					YNSpecial = "0";
			}
		}
		///<summary>
		///1��Ч/0��Ч
		///</summary>
		private string YNValid = "1";
		public bool bValid
		{
			get
			{
				if(YNValid =="1")
					return true;
				else
					return false;
			}
			set
			{
				if(value ==true)
					YNValid = "1";
				else
					YNValid = "0";
			}
		}
		///<summary>
		///1�ϲ�/0��
		///</summary>
		private string YNUnite = "0";
		public bool bUnite
		{
			get
			{
				if(YNUnite == "1")
					return true;
				else 
					return false;
			}
			set
			{
				if(value == true)
					YNUnite = "1";
				else
					YNUnite = "0";
			}
		}
		/// <summary>
		/// ������ʱָ�����Ƿ���Ҫ��̨��ʿ
		/// </summary>
		private string YNAccoNur;
		public bool bAccoNur
		{
			get
			{
				if(YNAccoNur == "1")
					return true;
				else 
					return false;
			}
			set
			{
				if(value == true)
					YNAccoNur = "1";
				else
					YNAccoNur = "0";
			}
		}
		/// <summary>
		/// ������ʱָ�����Ƿ���ҪѲ�ػ�ʿ
		/// </summary>
		private string YNPrepNur;
		public bool bPrepNur
		{
			get
			{
				if(YNPrepNur == "1")
					return true;
				else 
					return false;
			}
			set
			{
				if(value == true)
					YNPrepNur = "1";
				else
					YNPrepNur = "0";
			}
		}
		#endregion

		#region IDept ��Ա (�ӿڼ̳�)

		public neusoft.neuFC.Object.neuObject InDept
		{
			get
			{
				// TODO:  ��� OpsApplication.InDept getter ʵ��
				return null;
			}
			set
			{
				// TODO:  ��� OpsApplication.InDept setter ʵ��
			}
		}

		public neusoft.neuFC.Object.neuObject ExeDept
		{
			get
			{
				// TODO:  ��� OpsApplication.ExeDept getter ʵ��
				return this.ExecDept;
			}
			set
			{
				ExecDept = (neusoft.HISFC.Object.Base.Department)value;
			}
		}		

		public neusoft.neuFC.Object.neuObject ReciptDept
		{
			get
			{
				// TODO:  ��� OpsApplication.ReciptDept getter ʵ��
				return null;
			}
			set
			{
				// TODO:  ��� OpsApplication.ReciptDept setter ʵ��
			}
		}

		public neusoft.neuFC.Object.neuObject NurseStation
		{
			get
			{
				// TODO:  ��� OpsApplication.NurseStation getter ʵ��
				return null;
			}
			set
			{
				// TODO:  ��� OpsApplication.NurseStation setter ʵ��
			}
		}

		public neusoft.neuFC.Object.neuObject StockDept
		{
			get
			{
				// TODO:  ��� OpsApplication.StockDept getter ʵ��
				return null;
			}
			set
			{
				// TODO:  ��� OpsApplication.StockDept setter ʵ��
			}
		}

		public neusoft.neuFC.Object.neuObject ReciptDoct
		{
			get
			{
				// TODO:  ��� OpsApplication.ReciptDoct getter ʵ��
				return null;
			}
			set
			{
				// TODO:  ��� OpsApplication.ReciptDoct setter ʵ��
			}
		}

		#endregion

		public new OpsApplication Clone()
		{
			return base.Clone() as OpsApplication;
		}

	}
}
