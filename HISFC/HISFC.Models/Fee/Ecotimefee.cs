using System;

namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// Ecotimefee �Ż�ʱ�α�
	/// </summary>
	public class Ecotimefee
	{
		public Ecotimefee()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
//		SEQUENCE_NO  VARCHAR2(4)                     ���к�                      
//		PACT_CODE    VARCHAR2(3)    Y                ��ͬ��λ����                
//		FEE_CODE     VARCHAR2(12)                    ��С���û���Ŀ����          
//		ECO_RATIO    NUMBER(10,2)                    �Żݱ���                    
//		BEGIN_DATE   DATE           Y                ��ʼ����                    
//		END_DATE     DATE           Y                ��������                    
//		ENU_DAY      VARCHAR2(2000) Y                ö���� Ϊ|�ָ� YYYY/MM/DD   
//		WEEK_DAY     VARCHAR2(50)   Y                ���ڼ� ��|���ָ�            
//		VALID_STATE  VARCHAR2(1)                     ��Ч�� 0 ��Ч 1 ��Ч 2 ���� 
//		SORT_ID      NUMBER                          ˳��� 
		public string SequenceNo ;// ���к�       
		public string PactCode;//��ͬ��λ���� 
		public string FeeCode;// ��С���û���Ŀ����  
		public string EcoRatio;// �Żݱ��� 
		public string BeginDate;//��ʼ���� 
		public string EndDate;//  ��������  
		public string EnuDaty;//  ö���� Ϊ|�ָ� YYYY/MM/DD 
		public string WeekDay;//���ڼ� ��|���ָ� 
		public string ValidState;//��Ч�� 0 ��Ч 1 ��Ч 2 ���� 
		public string SortId;// ˳��� 
	}
}
