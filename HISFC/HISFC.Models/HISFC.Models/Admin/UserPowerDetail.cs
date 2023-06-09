using System;
using System.Collections;


namespace FS.HISFC.Models.Admin {


	/// <summary>
	/// 人员权限明细
	/// </summary>
    /// 
    [System.Serializable]
	public class UserPowerDetail: FS.FrameWork.Models.NeuObject {
		private System.String pkID ;
		private FS.FrameWork.Models.NeuObject dept =new FS.FrameWork.Models.NeuObject();
		private FS.FrameWork.Models.NeuObject user =new FS.FrameWork.Models.NeuObject();
		private System.String class1Code ;
		private System.String class2Code ;
		//private System.String class3Code ;
		private System.String grantDept ;
		private System.String grantEmpl ;
		private System.Boolean grantFlag ;
		private System.String roleCode;
		//private ArrayList Privileges;

		public PowerLevelClass3 PowerLevelClass = new PowerLevelClass3();

		/// <summary>
		/// 重写ID＝用户编码
		/// </summary>
		public new string ID {
			get{ return this.user.ID; }
			set{ this.user.ID = value; }
		}

		/// <summary>
		/// 重写Name＝用户姓名
		/// </summary>
		public new string Name {
			get{ return this.user.Name; }
			set{ this.user.Name = value; }
		}

		/// <summary>
		/// 关联表序号
		/// </summary>
		public System.String PkID {
			get {
				return this.pkID;
			}
			set {
				this.pkID = value;
				this.ID = value;
			}
		}

		/// <summary>
		/// 权限部门
		/// </summary>
		public FS.FrameWork.Models.NeuObject Dept {
			get {
				return this.dept;
			}
			set {
				this.dept = value;
			}
		}

		/// <summary>
		/// 用户编码
		/// </summary>
		public FS.FrameWork.Models.NeuObject User {
			get {
				return this.user;
			}
			set {
				this.user = value;
			}
		}

		/// <summary>
		/// 一级权限分类码，权限类型
		/// </summary>
		public System.String Class1Code {
			get {
				return this.class1Code;
			}
			set {
				this.class1Code = value;
			}
		}

		/// <summary>
		/// 二级权限分类码
		/// </summary>
		public System.String Class2Code {
			get {
				return this.class2Code;
			}
			set {
				this.class2Code = value;
				this.PowerLevelClass.Class2Code = value;
			}
		}

		//		/// <summary>
		//		/// 三级权限分类码
		//		/// </summary>
		//		public System.String Class3Code
		//		{
		//			get
		//			{
		//				return this.class3Code;
		//			}
		//			set
		//			{
		//				this.class3Code = value;
		//			}
		//		}

		/// <summary>
		/// 授权科室
		/// </summary>
		public System.String GrantDept {
			get {
				return this.grantDept;
			}
			set {
				this.grantDept = value;
			}
		}

		/// <summary>
		/// 授权人
		/// </summary>
		public System.String GrantEmpl {
			get {
				return this.grantEmpl;
			}
			set {
				this.grantEmpl = value;
			}
		}

		/// <summary>
		/// 是否可以再授权：0否1是
		/// </summary>
		public System.Boolean GrantFlag {
			get {
				return this.grantFlag;
			}
			set {
				this.grantFlag = value;
			}
		}

		/// <summary>
		/// 角色
		/// </summary>
		public System.String RoleCode {
			get {
				return this.roleCode;
			}
			set {
				this.roleCode = value;
			}
		}
	}
}