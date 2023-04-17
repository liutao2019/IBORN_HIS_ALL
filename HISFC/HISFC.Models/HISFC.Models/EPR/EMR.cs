using System;
using System.Collections;
namespace FS.HISFC.Models.EPR
{
	/// <summary>
	/// EMR 的摘要说明。
	/// 电子病历实体
	/// id inpatientNo,name 病历名称
	/// </summary>
    [Serializable]
	public class EMR:FS.FrameWork.Models.NeuObject 
	{
		public EMR()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		/// <summary>
		/// 病历类型
		/// </summary>
		public FS.FrameWork.Models.NeuObject Type=new FS.FrameWork.Models.NeuObject();
		public string FullFileName;
		public ArrayList Folders;

		public string strFolders
		{
			get
			{
				return strFolder;
			}
			set
			{
				strFolder=value;
				try
				{
					string[] s=strFolder.Split('\\');
					this.Folders=new ArrayList();
					for(int i=0;i<s.GetUpperBound(0);i++)
					{
						this.Folders.Add(s[i]);
					}
				}
				catch{}
			}
		}

		public string FileName;
		public string HostIP="127.0.0.1";
		private string strFolder;
	}
}
