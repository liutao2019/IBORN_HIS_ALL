using System;


namespace FS.HISFC.Models.Admin {


	/// <summary>
	/// SysModel ��ժҪ˵����
	/// </summary>
    /// 
    [System.Serializable]
	public class SysModel: FS.FrameWork.Models.NeuObject
	{
		private System.String sysCode ;
		private System.String sysName ;
		private System.Int32 sortId ;

		/// <summary>
		/// ģ�����
		/// </summary>
		public System.String SysCode
		{
			get
			{
				return this.sysCode;
			}
			set
			{
				this.sysCode = value;
				this.ID = value;
			}
		}

		/// <summary>
		/// ģ������
		/// </summary>
		public System.String SysName
		{
			get
			{
				return this.sysName;
			}
			set
			{
				this.sysName = value;
				this.Name = value;
			}
		}

		/// <summary>
		/// ˳���
		/// </summary>
		public System.Int32 SortId
		{
			get
			{
				return this.sortId;
			}
			set
			{
				this.sortId = value;
			}
		}

	}
}
