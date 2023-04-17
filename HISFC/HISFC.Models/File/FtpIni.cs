using System;

namespace FS.HISFC.Models.File
{
	/// <summary>
	/// Ftpini ��ժҪ˵����
	/// </summary>
    [System.Serializable]
    public class Ftpini : FS.FrameWork.Models.NeuObject, IFTP
	{
        public Ftpini()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		//		FTP_ADDRESS         VARCHAR2(20)                   FTP��ַ          
		//		FTP_USERNAME        VARCHAR2(20)  Y                FTP�û���        
		//		FTP_PASSWORD        VARCHAR2(20)  Y                FTP����          
		//		FTP_REMOTEDIRECTORY VARCHAR2(100) Y                FTPԶ��Ŀ¼      
		//		CLIENT_IPBEGIN      VARCHAR2(20)           'ALL'   �ͻ��˿�ʼIP��ַ 
		//		CLIENT_IPEND        VARCHAR2(20)           'ALL'   �ͻ��˽���IP��ַ 
		//		OPER_CODE           VARCHAR2(6)                    ����Ա           
		//		OPER_DATE           DATE                           ����ʱ��  

		public string FtpAddress;
		public string FtpUserName;
		public string FtpPassWord;
		public string FtpRemoteDirectory;
		public string FtpClientBegin;
		public string FtpClientEnd;
		public System.DateTime OperDate;
		#region IFTP ��Ա

		public string IP
		{
			get
			{
				// TODO:  ��� Ftpini.IP getter ʵ��
				return this.FtpAddress;
			}
			set
			{
				// TODO:  ��� Ftpini.IP setter ʵ��
			}
		}

		public string UserName
		{
			get
			{
				// TODO:  ��� Ftpini.UserName getter ʵ��
				return this.FtpUserName;
			}
			set
			{
				// TODO:  ��� Ftpini.UserName setter ʵ��
			}
		}

		public string PassWord
		{
			get
			{
				// TODO:  ��� Ftpini.PassWord getter ʵ��
				return this.FtpPassWord;
			}
			set
			{
				// TODO:  ��� Ftpini.PassWord setter ʵ��
			}
		}

		protected string folders;
		protected string filename;
		public string Folders
		{
			get
			{
				// TODO:  ��� Ftpini.Folders getter ʵ��
				return this.folders;
			}
			set
			{
				// TODO:  ��� Ftpini.Folders setter ʵ��
				this.folders = value;
			}
		}

		public string FileName
		{
			get
			{
				// TODO:  ��� Ftpini.FileName getter ʵ��
				return this.filename;
			}
			set
			{
				// TODO:  ��� Ftpini.FileName setter ʵ��
				this.filename = value;
			}
		}

		public string Root
		{
			get
			{
				// TODO:  ��� Ftpini.Root getter ʵ��
				return this.FtpRemoteDirectory;
			}
			set
			{
				// TODO:  ��� Ftpini.Root setter ʵ��
			}
		}

		#endregion
	}
}
