using System;

namespace Neusoft.HISFC.Object
{
	/// <summary>
	/// Cpactsat ��ժҪ˵����
	/// </summary>
	public class Cpactsat :Neusoft.NFC.Object.NeuObject 
	{
		public Cpactsat()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		// id �������
		// name ��������
		// ��ͬ����
		public string PactId;
		//��ͬ����
		public string PactName;
		//ƴ����
		public string SpellCode ;
		//���
		public string WBCode;
		// ������
		public string InputCode;
		// ˳��� 
		public int SortId;

		//��Ч�Ա�ʶ
		public string ValidState;

		//�����˴���
		public string Opercode;
	}
}
