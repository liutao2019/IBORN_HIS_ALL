using System;

namespace Neusoft.HISFC.Object.Pharmacy
{
	/// <summary>
	/// IFeeItem ��ժҪ˵����
	/// </summary>
	public interface IFeeItem1
	{
		/// <summary>
		/// ������ˮ��
		/// </summary>
	    string NoteNO
		{
			get;
			set;
		}

		/// <summary>
		/// ��������ˮ��
		/// </summary>
		int SequenceNo
		{
			get;
			set;
		}
	}
}
