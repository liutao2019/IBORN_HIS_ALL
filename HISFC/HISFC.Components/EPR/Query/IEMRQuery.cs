using System;
using System.Data;
namespace FS.HISFC.Components.EPR.Query
{
	/// <summary>
	/// IEMRQuery ��ժҪ˵����
	/// </summary>
	public interface IEMRQuery
	{
		/// <summary>
		/// ��ǰ����
		/// </summary>
		string  InpatientNo{get;set;}
		/// <summary>
		/// ��ѯ���
		/// </summary>
		/// <returns></returns>
		DataSet Query();
	}
}
