using System;
using System.Collections;
using FS.HISFC.Models.RADT;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// Employee <br></br>
	/// [��������: ��Աʵ��]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-09-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class Employee :  Spell,  ISort, IValidState
	{

		/// <summary>
		/// ���캯��
		/// </summary>
		public Employee()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����

		/// <summary>
		/// ����
		/// </summary>
        protected FS.FrameWork.Models.NeuObject nurse = new NeuObject();

		/// <summary>
		/// ����
		/// </summary>
        protected FS.FrameWork.Models.NeuObject dept = new NeuObject();

		/// <summary>
		/// ����
		/// </summary>
		private string password;

		/// <summary>
		/// ְ��
		/// </summary>
		private NeuObject duty = new NeuObject();

		/// <summary>
		/// �Ƿ�ר��
		/// </summary>
		private bool isExpert;

		/// <summary>
		/// ר��
		/// </summary>
		private NeuObject expert = new NeuObject();

		/// <summary>
		/// ְ��
		/// </summary>
		private NeuObject level = new NeuObject();

		/// <summary>
		/// �Ƿ���Կ���ҩ
		/// </summary>
		private bool isPermissionAnesthetic;

		/// <summary>
		/// Ȩ��������
		/// </summary>
		private ArrayList permissionGroup = new ArrayList();

		/// <summary>
		/// Ȩ��
		/// </summary>
		private ArrayList permission = new ArrayList();

		/// <summary>
		/// ��ǰѡ�����
		/// </summary>
		private NeuObject currentGroup = new NeuObject();

		/// <summary>
		/// ��ǰѡ���Ȩ��
		/// </summary>
		private string currentPermission;

		/// <summary>
		/// �˵�
		/// </summary>
		private string menu;

		/// <summary>
		/// �Ƿ����Ա
		/// </summary>
		private bool isManager;

		/// <summary>
		/// �Ա�
		/// </summary>
		private SexEnumService sex = new SexEnumService();

		/// <summary>
		/// ��������
		/// </summary>
		private DateTime birthDay;  

		/// <summary>
		/// ���֤
		/// </summary>
		private string idCard;

		/// <summary>
		/// ��Ա���
		/// </summary>
		private EmployeeTypeEnumService employeeType = new EmployeeTypeEnumService();

		/// <summary>
		/// �������
		/// </summary>
		private int  sortID;

		/// <summary>
		/// Ա��״̬
		/// </summary>
		private Base.EnumValidState validState;

		/// <summary>
		/// �Ƿ����޸�Ʊ��Ȩ�� 1���� 0������
		/// </summary>
		private bool isCanModify;

		/// <summary>
		/// ��ҵԺУ
		/// </summary>
		private NeuObject graduateSchool = new NeuObject();

		/// <summary>
		/// ���Һž��շ�Ȩ�� 0 ������ 1����
		/// </summary>
		private bool isNoRegCanCharge;

		#endregion

		#region ����

		/// <summary>
		/// ��ҵԺУ
		/// </summary>
		public NeuObject GraduateSchool
		{
			get
			{
				return this.graduateSchool;
			}
			set
			{
				this.graduateSchool = value;
			}
		}

		/// <summary>
		/// ���Һž��շ�Ȩ�� 0 ������ 1����
		/// </summary>
		public bool IsNoRegCanCharge
		{
			get
			{
				return this.isNoRegCanCharge;
			}
			set
			{
				this.isNoRegCanCharge = value;
			}
		}

		/// <summary>
		/// �Ƿ����޸�Ʊ��Ȩ�� 1���� 0������
		/// </summary>
		public bool IsCanModify
		{
			get
			{
				return this.isCanModify;
			}
			set
			{
				this.isCanModify = value;
			}
		}

		/// <summary>
		/// Ա��״̬
		/// </summary>
        public Base.EnumValidState ValidState
		{
			get
			{
				return this.validState;
			}
			set
			{
				this.validState = value;
			}
		}

		/// <summary>
		/// ��Ա���
		/// </summary>
		public EmployeeTypeEnumService EmployeeType
		{
			get
			{
				return this.employeeType;
			}
			set
			{
				this.employeeType = value;
			}
		}

		/// <summary>
		/// ���֤
		/// </summary>
		public string IDCard
		{
			get
			{
				return this.idCard;
			}
			set
			{
				this.idCard = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public DateTime Birthday
		{
			get
			{
				return this.birthDay;
			}
			set
			{
				this.birthDay = value;
			}
		}

		/// <summary>
		/// �Ա�
		/// </summary>
		public SexEnumService Sex
		{
			get
			{
				return this.sex;
			}
			set
			{
				this.sex = value;
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		public FS.FrameWork.Models.NeuObject Nurse
		{
			get
			{
				
				return nurse;
			}
			set
			{
                this.nurse = value;
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		public FS.FrameWork.Models.NeuObject Dept
		{
			get
			{
				
				return dept;
			}
			set
			{
                this.dept = value;
			}
		}

		/// <summary>
		/// �Ƿ����Ա
		/// </summary>
		public bool IsManager
		{
			get
			{
				return this.isManager;
			}
			set
			{
				this.isManager = value;
			}
		}

		/// <summary>
		/// �˵�
		/// </summary>
		public string Menu
		{
			get
			{
				return this.menu;
			}
			set
			{
				this.menu = value;
			}
		}

		/// <summary>
		/// ��ǰѡ���Ȩ��
		/// </summary>
		public string CurrentPermission
		{
			get
			{
				return this.currentPermission;
			}
			set
			{
				this.currentPermission = value;
			}
		}

		/// <summary>
		/// ��ǰѡ�����
		/// </summary>
		public NeuObject CurrentGroup
		{
			get
			{
				return this.currentGroup;
			}
			set
			{
				this.currentGroup = value;
			}
		}

		/// <summary>
		/// Ȩ��
		/// </summary>
		public ArrayList Permission
		{
			get
			{
				return this.permission;
			}
			set
			{
				this.permission = value;
			}
		}

		/// <summary>
		/// Ȩ��������
		/// </summary>
		public ArrayList PermissionGroup
		{
			get
			{
				return this.permissionGroup;
			}
			set
			{
				this.permissionGroup = value;
			}
		}

		/// <summary>
		/// �Ƿ�������ҩ
		/// </summary>
		public bool IsPermissionAnesthetic
		{
			get
			{
				return this.isPermissionAnesthetic;
			}
			set
			{
				this.isPermissionAnesthetic = value;
			}
		}

		/// <summary>
		/// ְ��
		/// </summary>
		public NeuObject Level
		{
			get
			{
				return this.level;
			}
			set
			{
				this.level = value;
			}
		}

		/// <summary>
		/// ר��
		/// </summary>
		public NeuObject Expert
		{
			get
			{
				return this.expert;
			}
			set
			{
				this.expert = value;
			}
		}

		/// <summary>
		/// �Ƿ�ר��
		/// </summary>
		public bool IsExpert
		{
			get
			{
				return this.isExpert;
			}
			set
			{
				this.isExpert = value;
			}
		}

		/// <summary>
		/// ְ��
		/// </summary>
		public NeuObject Duty
		{
			get
			{
				return this.duty;
			}
			set
			{
				this.duty = value;
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}
		#endregion

		#region ����

		/// <summary>
		/// ��ǰѡ���Ȩ��
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪCurrentPermission")]
		public string curPermission;

		/// <summary>
		/// ��ǰѡ�����
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪCurrentGroup")]
		public FS.FrameWork.Models.NeuObject curGroup=new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ����
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪPassword")]
		public string PassWord;

		/// <summary>
		/// �Ƿ���Կ���ҩ
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪIsPermissionAnesthetic")]
		public bool drugPermission;

		/// <summary>
		/// ���֤��
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪIDCard")]
		public string IdenCode;

		/// <summary>
		/// ���Һž��շ�Ȩ�� 0 ������ 1����
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪIsNoRegCanCharge")]
		public bool CanNoRegFee;

		/// <summary>
		/// ��ҵԺУ
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����Ϊ����GraduateSchool")]
		public string EducationCode;
		
		/// <summary>
		/// ��Ա���
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����Ϊ����EmployeeType")]
		public string PersonType;


		/// <summary>
		/// �Ƿ���Ը���
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����Ϊ����IsModify")]
		public string Modify;
		#endregion

		#region ISort ��Ա

		/// <summary>
		/// �������
		/// </summary>
		public int SortID
		{
			get
			{
				return this.sortID;
			}
			set
			{
				this.sortID = value;
			}
		}

		#endregion

		#region ����

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new Employee Clone()
		{
			Employee employee = base.Clone() as Employee;

			employee.Dept = this.Dept.Clone();
			employee.Nurse = this.Nurse.Clone();
			employee.GraduateSchool = this.GraduateSchool.Clone();
			employee.CurrentGroup = this.CurrentGroup.Clone();
			employee.Level = this.Level.Clone();
			employee.Expert = this.Expert.Clone();
			employee.Duty = this.Duty.Clone();

			return employee;
		}

		#endregion
		
	}
}
