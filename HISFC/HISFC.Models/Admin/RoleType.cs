using System;


namespace FS.HISFC.Models.Admin {


	/// <summary>
	/// RoleType 的摘要说明。
	/// </summary>
    /// 
    [System.Serializable]
	public class RoleType: FS.FrameWork.Models.NeuObject
	{
		private System.String typeCode ;
		private System.String typeName ;
		private System.String typeMeanint ;
		private System.String mark ;

		/// <summary>
		/// 角色分类代码
		/// </summary>
		public System.String TypeCode
		{
			get
			{
				return this.typeCode;
			}
			set
			{
				this.typeCode = value;
			}
		}

		/// <summary>
		/// 角色分类名称
		/// </summary>
		public System.String TypeName
		{
			get
			{
				return this.typeName;
			}
			set
			{
				this.typeName = value;
			}
		}

		/// <summary>
		/// 角色分类说明
		/// </summary>
		public System.String TypeMeanint
		{
			get
			{
				return this.typeMeanint;
			}
			set
			{
				this.typeMeanint = value;
			}
		}

		/// <summary>
		/// 备注
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

	}
}
