using System;

namespace Neusoft.HISFC.Object.Fee 
{
	/// <summary>
	/// Rate ��ժҪ˵����
	/// </summary>
	public class Rate 
	{
		public Rate()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
//		CLINIC_NO        VARCHAR2(14)                  ҽ����ˮ��                            
//		HAPPEN_NO        NUMBER(5)                     �������                              
//		DERATE_KIND      VARCHAR2(1)  Y                �������� 0 �ܶ� 1 ��С���� 2 ��Ŀ���� 
//		RECIPE_NO        VARCHAR2(14) Y                ������                                
//		SEQUENCE_NO      NUMBER(3)    Y                ��������Ŀ��ˮ��                      
//		DERATE_TYPE      VARCHAR2(1)  Y                ��������                              
//		DERATE_COST      NUMBER(10,2) Y                ������                              
//		DERATE_CAUSE     VARCHAR2(50) Y                ����ԭ��                              
//		CONFIRM_OPERCODE VARCHAR2(6)  Y                ��׼��Ա������                        
//		CONFIRM_NAME     VARCHAR2(10) Y                ��׼������                            
//		DEPT_CODE        VARCHAR2(4)                   ���Ҵ���                              
//		BALANCE_NO       VARCHAR2(2)  Y                �������                              
//		BALANCE_STATE    VARCHAR2(1)                   ����״̬ 0:δ���㣻1:�ѽ���           
//		INVOICE_NO       VARCHAR2(12) Y                ��Ʊ��                                
//		CANCEL_CODE      VARCHAR2(6)  Y                �����˴���                            
//		CANCEL_DATE      DATE         Y                ����ʱ��  
		
		public string clinicNo;		// ҽ����ˮ��   
		public int    happenNo;		//�������    
		public string derateKind ;	//�������� 
		public string recipeNo;		// ������
		public int sequenceNo;	//��������Ŀ��ˮ��
		public string Item ;		//��Ŀ
		public string derateType;	// ��������
		public string FeeCode;      //��С����
		public string feeName;      //��С���ô���
		public decimal derate_Cost;  // ������
		public string derate_cause; //����ԭ��
		public string confirmOpercode ;// ��׼��Ա������  
		public string confirmName;     //��׼������
		public string deptCode;   //���Ҵ��� 
		public string deptName;  // ��������
		public int  BalanceNo;  //�������
		public string balanceState; // ����״̬ 0:δ���㣻1:�ѽ���  
		public string invoiceNo;  // ��Ʊ�� 
		public string opercode;  // �����˴���  
		public System.DateTime operdate; // ����ʱ��


	}
}
