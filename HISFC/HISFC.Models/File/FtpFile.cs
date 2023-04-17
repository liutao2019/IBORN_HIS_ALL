using System;

namespace FS.HISFC.Models.File
{
	/// <summary>
	/// FtpFile ��ժҪ˵����
	/// </summary>
    [System.Serializable]
    public class FtpFile : FS.FrameWork.Models.NeuObject
	{
		public FtpFile()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		//		FILE_NAME       VARCHAR2(40)                   �ļ���                   
		//		UPDATE_FLAG     VARCHAR2(1)   Y                ����״̬ 1 ���� 0 ������ 
		//		FILE_VERSION    VARCHAR2(20)  Y                �汾��                   
		//		LOCAL_DIRECTORY VARCHAR2(100) Y                �ͻ������Ŀ¼           
		//		OPER_CODE       VARCHAR2(6)                    ����Ա                   
		//		OPER_DATE       DATE                           ��������        
		//name �ļ���
		public string UpdateFlag;
		public string FileVersion;
		public string LocalDirectory;	
		public System.DateTime UpdateDate ;
	}
}
