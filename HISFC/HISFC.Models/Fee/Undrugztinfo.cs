using System;

namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// Undrugztinfo ��ҩƷ������ϸ�� ��ժҪ˵����
	/// </summary>
	public class Undrugztinfo :Neusoft.NFC.Object.NeuObject
	{
		public Undrugztinfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
//		PACKAGE_CODE VARCHAR2(12)                  ���ױ���         
//		ITEM_CODE    VARCHAR2(12)                  ��ҩƷ����       
//		SORT_ID      NUMBER       Y                ˳��� 
		//name ��������
		//id  ���ױ���
		public string itemCode ; //��ҩƷ����
		public string itemName ;//��ҩƷ����
		public string SpellCode; // ƴ��
		public string WbCode;    //��� 
		public string InputCode; //����
		public int    sortId ;//˳��� 
		public decimal Qty; //����
		public string ValidState;//��Ч�Ա�ʶ
	}
}
