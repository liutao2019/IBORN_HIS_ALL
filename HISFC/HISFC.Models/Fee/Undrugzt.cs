using System;

namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// Undrugzt  ��ҩƷ���ױ� ��ժҪ˵����
	/// </summary>
	public class Undrugzt:Neusoft.NFC.Object.NeuObject
	{
		public Undrugzt()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		//		PACKAGE_CODE VARCHAR2(12)                   ���ױ���                         
		//		PACKAGE_NAME VARCHAR2(50)  Y                ��������                         
		//		SYS_CLASS    VARCHAR2(3)   Y                ϵͳ���                         
		//		SPELL_CODE   VARCHAR2(8)   Y                ƴ����                           
		//		WB_CODE      VARCHAR2(8)   Y                ���                             
		//		INPUT_CODE   VARCHAR2(8)   Y                ������                           
		//		DEPT_CODE    VARCHAR2(200) Y                ִ�п��ұ��� �� | �ָ�           
		//		SORT_ID      NUMBER        Y                ˳���                           
		//		CONFIRM_FLAG VARCHAR2(1)   Y                ȷ�ϱ�־ 1 ��Ҫȷ�� 0 ����Ҫȷ�� 
		//		VALID_STATE  VARCHAR2(1)            '0'     ��Ч�Ա�־ 0 ���� 1 ͣ�� 2 ����  
		//		EXT_FLAG     VARCHAR2(1)   Y                ��չ��־                         
		//		EXT1_FLAG    VARCHAR2(1)   Y                ��չ��־1
		//name  ��������
		//id  ���ױ���
		/// <summary>
		/// ϵͳ���  
		/// </summary>
		public string  sysClass; //ϵͳ���  
		public string MinFee ;//��С����
		/// <summary>
		/// ƴ����  
		/// </summary>
		public string  spellCode ; //  ƴ����  
		/// <summary>
		/// ���
		/// </summary>
		public string wbCode  ; //���
		/// <summary>
		/// ������
		/// </summary>
		public string inputCode  ;//������
		/// <summary>
		/// ִ�п��ұ���
		/// </summary>
		public string deptCode ;//ִ�п��ұ���
		/// <summary>
		/// ˳���
		/// </summary>
		public int  sortId  ; //˳���
		/// <summary>
		/// ȷ�ϱ�־ 
		/// </summary>
		public string confirmFlag ;//ȷ�ϱ�־ 
		/// <summary>
		/// ��Ч�Ա�־
		/// </summary>
		public string  validState ;//��Ч�Ա�־
		/// <summary>
		/// ��չ��־
		/// </summary>
		public string  ExtFlag; //��չ��־
		/// <summary>
		/// ��չ��־1
		/// </summary>
		public string Ext1Flag ;// ��չ��־1
		/// <summary>
		/// ��ʷ�����(����������뵥ʱʹ��) 
		/// </summary>
		public string Mark1;//��ʷ�����(����������뵥ʱʹ��) 
		/// <summary>
		/// ���Ҫ��(����������뵥ʱʹ��)  
		/// </summary>
		public string Mark2;//���Ҫ��(����������뵥ʱʹ��)  
		/// <summary>
		/// ע������(����������뵥ʱʹ��)
		/// </summary>
		public string Mark3;//ע������(����������뵥ʱʹ��)
		/// <summary>
		/// //������뵥����   
		/// </summary>
		public string Mark4;
		/// <summary>
		/// �Ƿ���ҪԤԼ
		/// </summary>
		public string NeedBespeak; 
	}
}
