using System;

namespace Neusoft.HISFC.Object.Permission
{
	/// <summary>
	/// �����Է���Ŀ֪����Ϣʵ��
	/// </summary>
	public class Permission:Neusoft.NFC.Object.NeuObject
	{
		public Permission()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// סԺ��ˮ��
		/// </summary>
		public string InpatientNo="";
		/// <summary>
		/// ��Ŀ
		/// </summary>
		public Neusoft.NFC.Object.NeuObject Item=new Neusoft.NFC.Object.NeuObject();
		/// <summary>
		/// ��Ŀ���0��Ŀ/1��С���ô���/2ͳ�ƴ���
		/// </summary>
		public enuItemType ItemType=enuItemType.StatFee;
		/// <summary>
		/// �Ƿ�ͬ��
		/// </summary>
		public bool IsAgree=true;
		/// <summary>
		/// ����Ա����
		/// </summary>
		public string OperCode="";
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperDate;
	}
	public enum enuItemType
	{
		/// <summary>
		/// ������Ŀ
		/// </summary>
		Item,
		/// <summary>
		/// ��С����
		/// </summary>
		MiniFee,
		/// <summary>
		/// ͳ�Ʒ���
		/// </summary>
		StatFee,
		/// <summary>
		/// ҽ������
		/// </summary>
		SI
	}

}
