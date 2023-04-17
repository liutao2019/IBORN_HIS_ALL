using System;

namespace neusoft.HISFC.Object.Power
{
	/// <summary>
	/// UserRoleDetail ��ժҪ˵����
	/// </summary>
	public class UserRoleDetail: neusoft.neuFC.Object.neuObject
	{
		private System.String pkID ;
		private System.String deptCode ;
		private System.String userCode ;
		private System.String roleCode ;
		private System.String grantDept ;
		private System.String grantEmpl ;
		private System.Boolean grantFlag ;    	
		private System.String mark ;

		private PowerRole role ;

		/// <summary>
		/// ���������
		/// </summary>
		public System.String PkID
		{
			get
			{
				return this.pkID;
			}
			set
			{
				this.pkID = value;
			}
		}

		/// <summary>
		/// Ȩ�޲���
		/// </summary>
		public System.String DeptCode
		{
			get
			{
				return this.deptCode;
			}
			set
			{
				this.deptCode = value;
			}
		}

		/// <summary>
		/// �û�����
		/// </summary>
		public System.String UserCode
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

		/// <summary>
		/// ��ɫ����
		/// </summary>
		public System.String RoleCode
		{
			get
			{
				return this.roleCode;
			}
			set
			{
				this.roleCode = value;
			}
		}

		/// <summary>
		/// ��Ȩ����
		/// </summary>
		public System.String GrantDept
		{
			get
			{
				return this.grantDept;
			}
			set
			{
				this.grantDept = value;
			}
		}

		/// <summary>
		/// ��Ȩ��
		/// </summary>
		public System.String GrantEmpl
		{
			get
			{
				return this.grantEmpl;
			}
			set
			{
				this.grantEmpl = value;
			}
		}

		/// <summary>
		/// �Ƿ��������Ȩ��0��1��
		/// </summary>
		public System.Boolean GrantFlag
		{
			get
			{
				return this.grantFlag;
			}
			set
			{
				this.grantFlag = value;
			}
		}

		
		/// <summary>
		/// ��ע
		/// </summary>
		public System.String Mark
		{
			get
			{
				return this.mark;
			}
			set
			{
				this.mark = value;
			}
		}


		/// <summary>
		/// ��ɫ
		/// </summary>
		public PowerRole Role
		{
			get
			{
				return role;
			}
			set
			{
				role = value;
			}
		}
	}
}
