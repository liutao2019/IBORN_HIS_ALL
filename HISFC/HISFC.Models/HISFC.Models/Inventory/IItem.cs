using System;

namespace FS.HISFC.Models.Insurance
{
	/// <summary>
	/// IItem ��ժҪ˵����
	/// </summary>
    //[Serializable]
	public interface IItem
	{
		/// <summary>
		/// ҽ����Ŀ����
		/// </summary>
		string ItemCode{get;set;}
		/// <summary>
		/// ҽ����Ŀ����
		/// </summary>
		string ItemName{get;set;}
		bool IsEmergency{get;set;}
		//		/// <summary>
		//		/// ����ӳɱ���
		//		/// </summary>
		//		public decimal EmcRate;
		//		/// <summary>
		//		/// �ƻ��������
		//		/// </summary>
		//		public bool Family;
		//		///<summary>
		//		///�ض�������Ŀ
		//		///</summary>
		//		public string  Special;
		//		///<summary>
		//		///������
		//		///</summary>
		//		public string  ItemGrade;
		/// <summary>
		/// ��Ŀ��𣬼ף���...
		/// </summary>
		FS.FrameWork.Models.NeuObject Type{get;set;}
	}
}
