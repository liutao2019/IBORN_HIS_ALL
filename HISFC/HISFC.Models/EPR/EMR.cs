using System;
using System.Collections;
namespace FS.HISFC.Models.EPR
{
	/// <summary>
	/// EMR ��ժҪ˵����
	/// ���Ӳ���ʵ��
	/// id inpatientNo,name ��������
	/// </summary>
    [Serializable]
	public class EMR:FS.FrameWork.Models.NeuObject 
	{
		public EMR()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ��������
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
