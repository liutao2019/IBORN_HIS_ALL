using System;


namespace FS.HISFC.Models.Operation {


	/// <summary>
	/// Item ��ժҪ˵����
	/// </summary>
    [Serializable]
    public class Item
	{
		public Item()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		///<summary>
		///��������
		///</summary>
		public string  OperateId;
		///<summary>
		///��������
		///</summary>
		public string  OperateKind;
		///<summary>
		///������ģ
		///</summary>
		public string  OperateType;
	}
}
