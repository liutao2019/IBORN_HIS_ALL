using System;

namespace neusoft.HISFC.Object.RADT
{
	/// <summary>
	/// Cdfqspecial 的摘要说明。
	/// </summary>
	public class Cdfqspecial
	{
		public Cdfqspecial()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public string moOrder; //医嘱流水号
		public string combNo;  //医嘱组合号
		public string drqFreqtype; //频次类型
		public string drqPoint; //频次点
		public string dosePoint; //  频次点用量
		public string OperID; // 操作员
		public System.DateTime operDate; //操作时间
	}
}
