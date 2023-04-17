using System;

namespace neusoft.HISFC.Object.Power
{
	/// <summary>
	/// Role ��ժҪ˵����
	/// </summary>
	public class PowerRole: neusoft.neuFC.Object.neuObject
	{
		private System.String roleCode ;
		private System.String roleName ;
		private System.String roleMeanint ;
		private System.String roleType;
		private System.String mark ;

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
		/// ��ɫ����
		/// </summary>
		public System.String RoleName
		{
			get
			{
				return this.roleName;
			}
			set
			{
				this.roleName = value;
			}
		}

		/// <summary>
		/// ��ɫ����
		/// </summary>
		public System.String RoleType
		{
			get
			{
				return this.roleType;
			}
			set
			{
				this.roleType = value;
			}
		}

		/// <summary>
		/// ��ɫ˵��
		/// </summary>
		public System.String RoleMeanint
		{
			get
			{
				return this.roleMeanint;
			}
			set
			{
				this.roleMeanint = value;
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


		private System.Collections.ArrayList rolePowerDetails = null;
		public System.Collections.ArrayList RolePowerDetails
		{
			get
			{
				 
				return rolePowerDetails ;
			}
			set
			{
				rolePowerDetails = value;
			}
		}
	}
}
