using System;

namespace FS.HISFC.Models.Insurance
{
	/// <summary>
	/// IInsurence ��ժҪ˵����
	/// </summary>
    //[Serializable]
    public interface IInsurence
	{
		/// <summary>
		/// ҽ�����ı���
		/// </summary>
		string CenterCode{get;set;}
		/// <summary>
		/// ҽ����������
		/// </summary>
		string CenterName{get;set;}
		/// <summary>
		/// ҽ��������
		/// </summary>
		string ApprNo{get;set;}
		/// <summary>
		/// ҽ����Ŀ����
		/// </summary>
		string ItemCode{get;set;}
		/// <summary>
		/// ҽ����Ŀ����
		/// </summary>
		string ItemName{get;set;}
		/// <summary>
		/// �Ƿ񼱾�
		/// </summary>
		bool IsEmergency{get;set;}
		/// <summary>
		/// ��Ŀ��𣬼ף���...
		/// </summary>
		FS.FrameWork.Models.NeuObject Type{get;set;}
	}
}
