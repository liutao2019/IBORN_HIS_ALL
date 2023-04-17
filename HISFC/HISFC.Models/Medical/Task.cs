using System;

namespace Neusoft.HISFC.Models.Medical

{
	#region ����ʵ��
	/// <summary>
	/// [��������: ����ʵ��]<br></br>
	/// [�� �� ��: ����ΰ]<br></br>
	/// [����ʱ��: 2006-09-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
	public class Task
	{
		public Task()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ��
		/*
			PRIMARY_ID     VARCHAR2(20)                   ������                
			CURRENT_CODE   VARCHAR2(14)  Y                ����ҽ�ƻ�������      
			TASK_ID        VARCHAR2(20)  Y                ������              
			TASK_FILEINFO  VARCHAR2(50)  Y                ����                  
			PROJECT_ID     VARCHAR2(20)  Y                ��Ŀ���              
			PROJECT_NAME   VARCHAR2(100) Y                ��Ŀ����              
			PROJECT_TYPE   VARCHAR2(20)  Y                ��Ŀ���              
			PROJECT_KIND   VARCHAR2(20)  Y                ��������              
			TASK_LEVEL     VARCHAR2(20)  Y                ���⼶��              
			TASK_PRINCIPAL VARCHAR2(6)   Y                ��Ŀ������            
			BEGIN_DATE     DATE          Y                ��ʼʱ��              
			END_DATE       DATE          Y                ����ʱ��              
			CONFIRM_COST   NUMBER(12,2)  Y                ��׼����              
			MARK           VARCHAR2(500) Y                ��ע                  
			VALID_STATE    VARCHAR2(1)   Y                ��Ч��״̬  0 �� 1 �� 
			OPER_CODE      VARCHAR2(6)   Y                ����Ա                
			OPER_DATE      DATE          Y                �������� 
			confirm_Date
		*/
		#endregion

		#region ��
		private System.String primaryId = "";
		private System.String taskId = "";
		private System.String taskFileInfo = "";
		private System.String kind = "";
		private System.String type = "";
		private System.String level = "";
		private System.String principal = "";
		private System.DateTime beginTime = System.DateTime.MinValue;
		private System.DateTime endTime = System.DateTime.MinValue;
		private System.Decimal cost = 0;
		private System.String mark = "";
		private System.String valid = "";
		private System.String operCode = "";
		private System.DateTime operDate = System.DateTime.MinValue;
		private Neusoft.FrameWork.Models.NeuObject project = new Neusoft.FrameWork.Models.NeuObject();
		private System.DateTime confirmDate = System.DateTime.MinValue;
		private System.String oneDept = "";
		private System.String twoDept = "";
		#endregion

		/// <summary>
		/// ������
		/// </summary>
		public string PrimaryId
		{
			get
			{
				return this.primaryId;
			}
			set
			{
				this.primaryId = value;
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string TaskId
		{
			get
			{
				return this.taskId;
			}
			set
			{
				this.taskId = value;
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string TaskFileInfo
		{
			get
			{
				return this.taskFileInfo;
			}
			set
			{
				this.taskFileInfo = value;
			}
		}
		/// <summary>
		/// ��Ŀ
		/// </summary>
		public Neusoft.FrameWork.Models.NeuObject Project
		{
			get
			{

				return this.project;
			}
			set
			{
				this.project = value;
			}
		}
		/// <summary>
		/// ��Ŀ���
		/// </summary>
		public string Kind
		{
			get
			{
				return this.kind;
			}
			set
			{
				this.kind = value;
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}
		/// <summary>
		/// ���⼶��
		/// </summary>
		public string Level
		{
			get
			{
				return this.level;
			}
			set
			{
				this.level = value;
			}
		}
		/// <summary>
		/// ��Ŀ������
		/// </summary>
		public string Principal
		{
			get
			{
				return this.principal;
			}
			set
			{
				this.principal = value;
			}
		}
		/// <summary>
		/// ��ʼʱ��
		/// </summary>
		public DateTime BeginTime
		{
			get
			{
				return this.beginTime;
			}
			set
			{
				this.beginTime = value;
			}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime EndTime
		{
			get
			{
				return this.endTime;
			}
			set
			{
				this.endTime = value;
			}
		}
		/// <summary>
		/// ��׼����
		/// </summary>
		public decimal Cost
		{
			get
			{
				return this.cost;
			}
			set
			{
				this.cost = value;
			}
		}
		/// <summary>
		/// ��ע
		/// </summary>
		public string Mark
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
		/// <summary>
		/// ��Ч״̬
		/// </summary>
		public string Valid
		{
			get
			{
				return this.valid;
			}
			set
			{
				this.valid = value;
			}
		}
		/// <summary>
		/// ����Ա
		/// </summary>
		public string OperCode
		{
			get
			{
				return this.operCode;
			}
			set
			{
				this.operCode = value;
			}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperDate
		{
			get
			{
				return this.operDate;
			}
			set
			{
				this.operDate = value;
			}
		}
		/// <summary>
		/// ȷ��ʱ��
		/// </summary>
		public DateTime ConfirmDate
		{
			get
			{
				return this.confirmDate;
			}
			set
			{
				this.confirmDate = value;
			}
		}
		/// <summary>
		/// һ����λ
		/// </summary>
		public string OneDept
		{
			get
			{
				return this.oneDept;
			}
			set
			{
				this.oneDept = value;
			}
		}
		/// <summary>
		/// ������λ
		/// </summary>
		public string TwoDept
		{
			get
			{
				return this.twoDept;
			}
			set
			{
				this.twoDept = value;
			}
		}
//		/// <summary>
//		/// ��������
//		/// </summary>
//		public ArrayList AlPertainDept
//		{
//			get
//			{
//				return this.alpertainDept;
//			}
//			set
//			{
//				this.alpertainDept = value;
//			}
//		}
//		/// <summary>
//		/// ������Դ	
//		/// </summary>
//		public ArrayList AlSource
//		{
//			get
//			{
//				return this.alsource;
//			}
//			set
//			{
//				this.alsource = value;
//			}
//		}
//		/// <summary>
//		/// �μӵ�λ
//		/// </summary>
//		public ArrayList AlJoinDept
//		{
//			get
//			{
//				return this.aljoinDept;
//			}
//			set
//			{
//				this.aljoinDept = value;
//			}
//		}
//		/// <summary>
//		/// ����
//		/// </summary>
//		public ArrayList AlDept
//		{
//			get
//			{
//				return this.aldept;
//			}
//			set
//			{
//				this.aldept = value;
//			}
//		}
//		/// <summary>
//		/// ר��
//		/// </summary>
//		public ArrayList AlSpecialDept
//		{
//			get
//			{
//				return this.alspecialDept;
//			}
//			set
//			{
//				this.alspecialDept = value;
//			}
//		}
//		/// <summary>
//		/// С���Ա
//		/// </summary>
//		public ArrayList AlLeaguer
//		{
//			get
//			{
//				return this.alleaguer;
//			}
//			set
//			{
//				this.alleaguer = value;
//			}
//		}
//		/// <summary>
//		/// �����ֶ�
//		/// </summary>
//		public ArrayList AlExtent
//		{
//			get
//			{
//				return this.alsxtent;
//			}
//			set
//			{
//				this.alextent = value;
//			}
//		}
//		

	}

	#endregion
}
