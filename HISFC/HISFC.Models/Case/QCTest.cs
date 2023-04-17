using System;
namespace neusoft.HISFC.Object.Case 
{
	/// <summary>
	/// QCTest ��ժҪ˵���������ʿ���Ϣ�Ǽ� ���ʵ����
	/// </summary>
	public class QCTest :neusoft.neuFC.Object.neuObject 
	{
		public QCTest()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
//		PARENT_CODE  VARCHAR2(14)                  ����ҽ�ƻ�������        
//		CURRENT_CODE VARCHAR2(14)                  ����ҽ�ƻ�������        
//		SEQUENCE_NO  VARCHAR2(10)                  ������ˮ��              
//		TEST_DATE    DATE         Y                ��������                
//		EMPL_CODE    VARCHAR2(6)  Y                ��Ա���                
//		EMPL_NAME    VARCHAR2(10) Y                ��Ա����                
//		MARK         NUMBER(4,2)  Y                ����                    
//		LEVL_CODE    VARCHAR2(2)  Y                ҽʦ�������            
//		LEVL_NAME    VARCHAR2(50) Y                ҽʦ��������            
//		VALID_STATE  VARCHAR2(1)  Y                �Ƿ����� 0 ��Ч 1  ���� 
//		OPER_CODE    VARCHAR2(6)  Y                ¼�����Ա              
//		OPER_DATE    DATE         Y                ¼��ʱ�� 
		#region  ˽�б���

		//ID ������ˮ��
		//����ʱ��
		private string testDate ;
		//��Ա ID����  name ����
		private neusoft.neuFC.Object.neuObject emplInfo = new neusoft.neuFC.Object.neuObject();
		//����
		private float mark ;
		//ҽʦ ID ������� NAME ��������
		private neusoft.neuFC.Object.neuObject  levelInfo = new neusoft.neuFC.Object.neuObject();
		//��Ч�Ա�ʶ
		private bool validState ;
		//����Ա
		private neusoft.neuFC.Object.neuObject operInfo  = new neusoft.neuFC.Object.neuObject();
		//¼��ʱ��
		private DateTime  operDate ;
		#endregion

		#region ��������
		/// <summary>
		/// ����Ա��Ϣ
		/// </summary>
		public neusoft.neuFC.Object.neuObject OperInfo
		{
			get
			{
				return operInfo;
			}
			set
			{
				operInfo = value;
			}
		}
			/// <summary>
			/// ����ʱ��
			/// </summary>
			public DateTime OperDate 
		{
			get
			{
				return operDate ;
			}
			set
			{
				operDate = value;
			}
		}
		/// <summary>
		/// ��Ч
		/// </summary>
		public bool ValidState
		{
			get
			{
				return validState ;
			}
			set
			{
				validState = value;
			}
		}
		/// <summary>
		/// ҽʦ���� id ������� name ��������
		/// </summary>
		public neusoft.neuFC.Object.neuObject LevelInfo
		{
			get
			{
				return levelInfo;
			}
			set
			{
				levelInfo = value;
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public float Mark
		{
			get
			{
				return mark;
			}
			set
			{
				mark = value;
			}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public string  TestDate
		{
			get
			{
				return testDate;
			}
			set
			{
				testDate = value;
			}
		}
		/// <summary>
		/// �μӿ�����Ա��Ϣ id ��Ա���� name ��Ա���� 
		/// </summary>
		public neusoft.neuFC.Object.neuObject EmplInfo 
		{
			get
			{
				return emplInfo;
			}
			set
			{
				emplInfo = value;
			}
		}
		#endregion 
		public new  QCTest Clone()
		{
			QCTest qct = (QCTest)base.Clone();
			qct.emplInfo = this.emplInfo.Clone();
			qct.levelInfo = this.levelInfo.Clone();
			qct.operInfo = this.operInfo.Clone();
			return qct;

		}

		}
}
