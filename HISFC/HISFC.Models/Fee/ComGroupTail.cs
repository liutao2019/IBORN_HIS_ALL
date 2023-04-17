using System;

namespace  FS.HISFC.Models.Fee
{
	/// <summary>
	/// ComGroupTail ��ժҪ˵����
	/// </summary>
    /// 
    [System.Serializable]
	public class ComGroupTail : FS.FrameWork.Models.NeuObject
	{
		public ComGroupTail()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
//		GROUP_ID     VARCHAR2(10)                   ����ID                        
//		SEQUENCE_NO  NUMBER(4)                      ��������Ŀ��ˮ��              
//		ITEM_CODE    VARCHAR2(12)                   ��Ŀ����                      
//		DRUG_FLAG    VARCHAR2(1)   Y                �Ƿ�ҩƷ,1��/2��              
//		EXEC_DPCD    VARCHAR2(4)   Y                ִ�п���                      
//		QTY          NUMBER(10,4)  Y                ��������                      
//		UNIT_FLAG    VARCHAR2(1)   Y                ������λ��1��С��λ/2��װ��λ 
//		COMB_NO      VARCHAR2(14)  Y                ��ϴ���                      
//		REMARK       VARCHAR2(150) Y                ��ע                          
//		OPER_CODE    VARCHAR2(6)   Y                ����Ա                        
//		OPER_DATE    DATE          Y                ����ʱ��  
		//ID  // ����ID  
		public int sequenceNo ;//��������Ŀ��ˮ��
		public string itemCode;//��Ŀ����  
		public string itemName; //��Ŀ����
		public string drugFlag;//�Ƿ�ҩƷ,1��/2��
		public string deptCode ;// ִ�п���
		public string deptName; //������
		public decimal qty;// ��������
		public string unitFlag;//������λ��1��С��λ/2��װ��λ 
		public string combNo;//��ϴ���
		public string reMark;// ��ע 
		public string operCode;// ����Ա
		public string operName ;//����Ա����
		public System.DateTime OperDate;// ����ʱ�� 
		public int SortNum; //��� 
	}
}
