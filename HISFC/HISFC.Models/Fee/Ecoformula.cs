using System;
using System.Collections;
namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// Ecoformula �Ż��ײͱ� ʵ����
	/// </summary>
	public class Ecoformula
	{
		public Ecoformula()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
//		PARENT_CODE    VARCHAR2(14)            'ROOT'  ����ҽ�ƻ�������                     
//		CURRENT_CODE   VARCHAR2(14)            'ROOT'  ����ҽ�ƻ�������                     
//		ECO_FLAG       VARCHAR2(1)                     �Ż�׼���־ 0 ���� 1 �Ծ����¼     
//		CLINIC_CODE    VARCHAR2(14)            'AAAA'  �����¼                             
//		CARD_NO        VARCHAR2(10)                    ���￨��                             
//		PACTCODE_FLAG  VARCHAR2(1)    Y                ��ͬ��λ                             
//		ICDCODE_FLAG   VARCHAR2(1)    Y                �����ֱ�־                           
//		DATE_FLAG      VARCHAR2(1)    Y                ʱ�α�־                             
//		ECOREAL_FLAG   VARCHAR2(1)    Y                �Ż�ԭ���ϵ 0 ȡ����Ż� 1 ȡ���Ż� 
//		SPECIL_FORMULA VARCHAR2(2000) Y                �������ʽ                         
//		OPER_CODE      VARCHAR2(6)                     ����Ա                               
//		OPER_DATE      DATE                            �������� 
		public string EcoFlag;//�Ż�׼���־ 0 ���� 1 �Ծ����¼   
		public string ClinicCode;//�����¼      
		public string CardNo;// ���￨��     
		public string PactcodeFlag;//��ͬ��λ 
		public string IcdcodeFlag;//�����ֱ�־   
		public string DateFlag; //ʱ�α�־    
		public string EcorealFlag;// �Ż�ԭ���ϵ 0 ȡ����Ż� 1 ȡ���Ż� 
		public string SpecilFormula;// �������ʽ         
	}
}
