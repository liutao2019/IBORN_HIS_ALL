using System;
using System.Collections;
 
using FS.FrameWork.Models;
using FS.HISFC.Models;
namespace FS.HISFC.Models.Insurance
{
	/// <summary>
	/// InsuranceDeal 医保待遇维护。
	/// </summary>
//	PACT_CODE	VARCHAR2(4)	N		合同单位
//	INPRO_STAUS	VARCHAR2(14)	N		在职状态 0 在职 1 离退休
//	PART_ID	NUMBER(1)	N		分段序号
//	STANDARD_COST	NUMBER(10)	N		起付线
//	SERVANT_SUBSIDY	NUMBER(10,2)	Y		公务员补助
//	SPECIAL_SUBSIDY	NUMBER(10,2)	Y		老红军补助
//	EXCEED_SUBSIDY	NUMBER(10,2)	Y		大额补助
//	BEGIN_COST	NUMBER(10,2)	Y		区间开始
//	END_COST	NUMBER(10,2)	Y		区间结束
//	OPER_CODE	VARCHAR2(6)	N		操作员
//	OPER_DATE	DATE	N		操作日期
    [Serializable]
	public class InsuranceDeal:FS.FrameWork.Models.NeuObject
	{
		public InsuranceDeal()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		/// <summary>
		/// 合同代码
		/// </summary>
		public NeuObject PactCode = new NeuObject();
		/// <summary>
		/// 在职状态
		/// </summary>
		public NeuObject InproStaus = new NeuObject();
		/// <summary>
		/// 分段序号
		/// </summary>
		public decimal PartID;
		/// <summary>
		/// 起付线
		/// </summary>
		public decimal StandardCost;
		/// <summary>
		/// 公务员补助
		/// </summary>
		public decimal ServantSubsidy;
		/// <summary>
		/// 老红军补助
		/// </summary>
		public decimal SpecialSubsidy;
		/// <summary>
		/// 大额补助
		/// </summary>
		public decimal ExceedSubsidy;
		/// <summary>
		/// 区间开始
		/// </summary>
		public decimal BeginCost;
		/// <summary>
		/// 区间结束
		/// </summary>
		public decimal EndCost;
	}
}
