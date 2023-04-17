using System;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// Person <br></br>
	/// [��������: ��Աʵ��]<br></br>
	/// [�� �� ��: ���Ʒ�]<br></br>
	/// [����ʱ��: 2004-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��һ��'
	///		�޸�ʱ��='2006-09-11'
	///		�޸�Ŀ��='�汾����'
	///		�޸�����=''
	///  />
	/// </summary>
	[Obsolete("�Ѿ����ڣ�����ΪFS.HISFC.Models.Base.Employee",true)]
	public class Person : FS.HISFC.Models.Base.Spell 
	{

		/// <summary>
		/// ���캯��
		/// </summary>
		public Person()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ����
		/// </summary>
		public FS.FrameWork.Models.NeuObject Dept
		{
			get
			{
				if(myDept==null)myDept=new FS.FrameWork.Models.NeuObject();
				return myDept;
			}
			set
			{
				if(value==null)value=new FS.FrameWork.Models.NeuObject();
				this.myDept=value;
				this.Memo=this.myDept.ID;
				this.User01=this.myDept.Name;
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public FS.FrameWork.Models.NeuObject Nurse
		{
			get
			{
				if(myNurse==null)myNurse=new FS.FrameWork.Models.NeuObject();
				return myNurse;
			}
			set
			{
				if(value==null)value=new FS.FrameWork.Models.NeuObject();
				this.myNurse=value;
				this.User02=this.myNurse.ID;
				this.User03=this.myNurse.Name;
			}
		}	
	
		#region ����

		/// <summary>
		/// ����
		/// </summary>
		public string PassWord;

		#endregion

		/// <summary>
		/// ְ��
		/// </summary>
		public FS.FrameWork.Models.NeuObject Duty=new FS.FrameWork.Models.NeuObject();
		
		/// <summary>
		/// �Ƿ�ר��
		/// </summary>
		public bool IsExpert=false;
		/// <summary>
		/// ר��
		/// </summary>
		public FS.FrameWork.Models.NeuObject Expert=new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// ְ��
		/// </summary>
		public FS.FrameWork.Models.NeuObject Level=new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// �Ƿ���Կ���ҩ
		/// </summary>
		public bool drugPermission=false;
		/// <summary>
		/// Ȩ��������
		/// </summary>
		public ArrayList PermissionGroup=new ArrayList();
		/// <summary>
		/// Ȩ��
		/// </summary>
		public ArrayList Permission=new ArrayList();
		/// <summary>
		/// ��ǰѡ�����
		/// </summary>
		public FS.FrameWork.Models.NeuObject curGroup=new FS.FrameWork.Models.NeuObject();
		
		/// <summary>
		/// ��ǰѡ���Ȩ��
		/// </summary>
		public string curPermission;
		/// <summary>
		/// �˵�
		/// </summary>
		public string Menu;
		/// <summary>
		/// �Ƿ����Ա
		/// </summary>
		public bool isManager=false;
		protected FS.FrameWork.Models.NeuObject myDept;
		protected FS.FrameWork.Models.NeuObject myNurse;


		
		
		public EnumSex Sex = new EnumSex();
		
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime BirthDay;  		
//		public string LevelCode;
		public string EducationCode;

		/// <summary>
		/// ���֤��
		/// </summary>
		public string IdenCode; 	

		public PersonType PersonType = new PersonType();
		public bool CanModify;
		public bool CanNoRegFee;
		public int ValidState;
		public int  SortID;

		public new Person Clone()
		{
			Person obj=base.Clone() as Person;
			obj.Dept=this.Dept.Clone();
			obj.Nurse=this.Nurse.Clone();
			return obj;
		}
		

	}
}
