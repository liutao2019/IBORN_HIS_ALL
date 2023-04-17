using System;

namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// ת���� ChangeCost ��ժҪ˵����
	/// </summary>
	public class ChangeCost
	{
		public ChangeCost()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
//		FEE_CODE        VARCHAR2(3)                   ��С���ô��� ���Ϊ all ��Ϊȫ������                          
//		CHANGE_TYPE     VARCHAR2(1)                   ת������,1 ����ת�룬2 סԺת�� 3 ��Ժת��                    
//		CLINIC_NO       VARCHAR2(14)                  ҽ����ˮ��                                                    
//		NAME            VARCHAR2(20)                  ����                                                          
//		PAYKIND_CODE    VARCHAR2(2)                   ������� 01-�Է�  02-���� 03-������ְ 04-�������� 05-���Ѹ߸� 
//		PACT_CODE       VARCHAR2(10)                  ��ͬ��λ                                                      
//		TOT_COST        NUMBER(10,2) Y                ���ý��                                                      
//		OWN_COST        NUMBER(10,2) Y                �Էѽ��                                                      
//		PAY_COST        NUMBER(10,2) Y                �Ը����                                                      
//		PUB_COST        NUMBER(10,2) Y                ���ѽ��                                                      
//		ECO_COST        NUMBER(10,2) Y                �Żݽ��                                                      
//		CHARGE_OPERCODE VARCHAR2(6)                   ת�����Ա                                                    
//		CHARGE_DATE     DATE                          ת������                                                      
//		BALANCE_NO      VARCHAR2(2)  Y                �������                                                      
//		BALANCE_STATE   VARCHAR2(1)                   �����־ 0:δ���㣻1:�ѽ��� 2:�ѽ�ת                          
//		OPER_CODE       VARCHAR2(6)                   ����Ա                                                        
//		OPER_DATE       DATE                          ��������    
		/// <summary>
		/// ת��ҽ�ƻ�������
		/// </summary>
		public string  ChangeCode ;
		/// <summary>
		/// ��С����
		/// </summary>
		public Neusoft.NFC.Object.NeuObject Fee = new Neusoft.NFC.Object.NeuObject();
		/// <summary>
		/// ת������
		/// </summary>
		public Neusoft.NFC.Object.NeuObject ChangeType = new Neusoft.NFC.Object.NeuObject();
		/// <summary>
		/// ҽ����ˮ��
		/// </summary>
		public string ClinicNo ;
		/// <summary>
		/// ����
		/// </summary>
		public string Name ;
		/// <summary>
		/// ��������
		/// </summary>
		public Neusoft.NFC.Object.NeuObject PayKind = new Neusoft.NFC.Object.NeuObject();
		/// <summary>
		/// ��ͬ��λ
		/// </summary>
		public Neusoft.NFC.Object.NeuObject Pact = new Neusoft.NFC.Object.NeuObject();
		/// <summary>
		/// ���ý��
		/// </summary>
		public decimal TotCost ;
		/// <summary>
		/// �Էѽ��
		/// </summary>
		public decimal OwnCost;
		/// <summary>
		/// �Ը����
		/// </summary>
	    public decimal PayCost;
		/// <summary>
		/// ���ѽ��
		/// </summary>
		public decimal pubCost;
		/// <summary>
		/// �Żݽ��
		/// </summary>
		public decimal EcoCost;
		public string BalanceNo;
		public string  BalanceState;  //����״̬

	}
}
