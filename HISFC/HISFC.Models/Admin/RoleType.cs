using System;


namespace FS.HISFC.Models.Admin {


	/// <summary>
	/// RoleType ��ժҪ˵����
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
		/// ��ɫ�������
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
		/// ��ɫ��������
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
		/// ��ɫ����˵��
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

	}
}
