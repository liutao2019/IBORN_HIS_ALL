using System;

namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// PayKind ��ժҪ˵����
	/// �������-�������
	///  01-�Է�  02-���� 03-������ְ 04-�������� 05-���Ѹ߸�              
	/// ID ���� Name ����
	/// </summary>
	public class PayKind:Neusoft.NFC.Object.NeuObject
	{
		public PayKind()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ҩƷ׷�ӱ���
		/// </summary>
		public float MedicalRate=1;
		/// <summary>
		/// ��ҩƷ׷�ӱ���
		/// </summary>
		public float OtherRate=1;
		public new string ID
		{
			get
			{
				return base.ID;
			}
			set
			{
				base.ID=value;
			}
		}
		public new PayKind  Clone()
		{
			return base.Clone() as PayKind;
		}
	}
}
