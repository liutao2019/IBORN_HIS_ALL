using System;

namespace neusoft.HISFC.Object
{
	/// <summary>
	/// PowerRole ��ժҪ˵����
	/// ��ɫ��Ϣ
	/// </summary>
	public class PowerRole: neusoft.neuFC.Object.neuObject
	{
		private System.String roleCode ;
		private System.String roleName ;
		private System.String roleMeanint ;
		private System.String mark ;

		private System.Collections.IList powers;

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


		public System.Collections.IList Powers
		{
			get
			{
				return this.powers;
			}

			set
			{
				this.powers = value;
			}
		}



	}
}
