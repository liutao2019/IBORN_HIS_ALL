using System;

namespace FS.HISFC.Models.File
{
	/// <summary>
	/// �����ļ������� ��ժҪ˵����
	/// ID = param��
	/// Name = ���ݱ���
	/// </summary>
    [System.Serializable]
    public class DataFileParam : FS.FrameWork.Models.NeuObject, IFTP
	{
		public DataFileParam()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Type;
		/// <summary>
		/// �Ƿ�����ݿ�
		/// </summary>
		public bool IsInDB=false;
		/// <summary>
		/// ģ���ļ���
		/// </summary>
		public string ModualFolders;
		/// <summary>
		/// ͷ
		/// </summary>
		public string Http;
		#region IFTP ��Ա
		protected string ip;
		/// <summary>
		/// ip
		/// </summary>
		public string IP
		{
			get
			{
				// TODO:  ��� DataFileParam.IP getter ʵ��
				return this.ip;
			}
			set
			{
				// TODO:  ��� DataFileParam.IP setter ʵ��
				this.ip=value;
			}
		}
		protected string username;
		/// <summary>
		/// �û���
		/// </summary>
		public string UserName
		{
			get
			{
				// TODO:  ��� DataFileParam.UserName getter ʵ��
				return this.username ;
			}
			set
			{
				// TODO:  ��� DataFileParam.UserName setter ʵ��
				this.username=value;
			}
		}
		protected string password;
		/// <summary>
		/// ����
		/// </summary>
		public string PassWord
		{
			get
			{
				// TODO:  ��� DataFileParam.PassWord getter ʵ��
				return this.password;
			}
			set
			{
				// TODO:  ��� DataFileParam.PassWord setter ʵ��
				this.password=value;
			}
		}
		protected string folders;
		/// <summary>
		/// Զ���ļ���
		/// </summary>
		public string Folders
		{
			get
			{
				// TODO:  ��� DataFileParam.Folders getter ʵ��
				return this.folders;
			}
			set
			{
				// TODO:  ��� DataFileParam.Folders setter ʵ��
				this.folders=value;
			}
		}
		protected string filename;
		/// <summary>
		/// �ļ���
		/// </summary>
		public string FileName
		{
			get
			{
				// TODO:  ��� DataFileParam.FileName getter ʵ��
				return this.filename;
			}
			set
			{
				// TODO:  ��� DataFileParam.FileName setter ʵ��
				this.filename=value;
			}
		}
		protected string root;
		/// <summary>
		/// ��
		/// </summary>
		public string Root
		{
			get
			{
				// TODO:  ��� DataFileParam.FileName getter ʵ��
				return this.root;
			}
			set
			{
				// TODO:  ��� DataFileParam.FileName setter ʵ��
				this.root = value;
			}
		}
		#endregion
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new DataFileParam Clone()
		{
			DataFileParam obj = new DataFileParam();
			obj = base.Clone() as DataFileParam;
			return obj;
		}
	}
}
