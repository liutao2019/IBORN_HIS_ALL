using System;

namespace FS.HISFC.Models.AdminQuery
{
	
	/*----------------------------------------------------------------
	// Copyright (C) 2004 
	// ��Ȩ���С� 
	//
	// �ļ�����FileTree.cs
	// �ļ�����������ID �ļ��������� NAME �ļ��������� ��ժҪ˵����
	//----------------------------------------------------------------*/

	public class FileTree:FS.FrameWork.Models.NeuObject
	{
		public FileTree()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

        private FS.FrameWork.Models.NeuObject upObj = new FS.FrameWork.Models.NeuObject();   //����ʵ��                        
		private string strURL = "";															   //���ӵ�ַ
		private string strTarget = "";														   //Ŀ���ַ
		private bool isValid = false;														   //�Ƿ���Ч
		private string strLevel = "";														   //�㼶
		private string strActorID = "";														   //��ɫ
	    private string strLoginID = "";														   //��¼ID

		/// <summary>
		/// �ϼ�����
		/// </summary>
		public FS.FrameWork.Models.NeuObject UpObj
		{
			get
			{
				return this.upObj;
			}
			set
			{
				this.upObj = value;
			}
		}
	
		/// <summary>
		/// ���ӵ�ַ
		/// </summary>
		public string URL
		{
			get
			{
				return this.strURL;
			}
			set
			{
				this.strURL = value;
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		public string Target
		{
			get
			{
				return this.strTarget;
			}
			set
			{
				this.strTarget = value;
			}
		}

		/// <summary>
		/// �Ƿ���Ч
		/// </summary>
		public bool Valid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
		}

		/// <summary>
		/// �㼶
		/// </summary>
		public string Level
		{
			get
			{
				return this.strLevel;
			}
			set
			{
				this.strLevel = value;
			}
		}
		
		/// <summary>
		/// ��ɫ��ʶ
		/// </summary>
		public string ActorID
		{
			get
			{
				return this.strActorID;
			}
			set
			{
				this.strActorID = value;
			}
		}
		
		/// <summary>
		/// ��½��ʶ
		/// </summary>
		public string LoginID
		{
			get
			{
				return this.strLoginID;
			}
			set
			{
				this.strLoginID = value;
			}
		}

		/*
		��ѩ�� 20060411
		���ӿ�¡����
		*/

		/// <summary>
		/// ��¡һ��ʵ��
		/// </summary>
		/// <returns></returns>
		public new FileTree Clone()
		{
			FileTree obj=base.MemberwiseClone() as FileTree;
			obj.upObj   =this.upObj.Clone();
			return obj;
		}

	}
}
