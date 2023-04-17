using System;

namespace neusoft.HISFC.Object.Power
{
	/// <summary>
	/// Role ��ժҪ˵����
	/// </summary>
	public class Role: neusoft.neuFC.Object.neuObject
	{
		private System.String roleCode ;
		private System.String roleName ;
		private System.String roleMeanint ;
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


		private System.Collections.Hashtable rolePowerDetails = null;
		public System.Collections.Hashtable RolePowerDetails
		{
			get
			{
				if(rolePowerDetails == null)
					rolePowerDetails = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());

				return rolePowerDetails ;
			}
			set
			{
				rolePowerDetails = value;
			}
		}
	}
}
