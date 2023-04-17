using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProvinceAcrossSI.Objects
{
    /// <summary>
    /// 结算返回结果【门诊、住院】
    /// </summary>
    public class OutBalanceInfo
    {
        /// <summary>
        /// 生育报销项目【生育门诊结算】
        /// </summary>
        public string BalanceItemSY = "";

        /// <summary>
        /// 是否减免手术费【生育门诊结算】
        /// </summary>
        public string JMSSflag = "";


        /// <summary>
        /// 开户银行(仅生育保险返回)
        /// </summary>
        public string Bank = "";//开户银行(仅生育保险返回)

        /// <summary>
        /// 银行帐号(仅生育保险返回)
        /// </summary>
        public string Acount = "";//银行帐号(仅生育保险返回)

        /// <summary>
        /// 结算流水号
        /// </summary>
        public string BalanceSeq = "";//结算流水号

        /// <summary>
        /// 操作时间(YYYMMDDHHMMSS)
        /// </summary>
        public string OperDate = "";//操作时间(YYYMMDDHHMMSS)

        /// <summary>
        /// 操作员编码
        /// </summary>
        public string OperCode = "";

        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string OperName = "";

        /// <summary>
        /// 明细行数
        /// </summary>
        public string DetailRowCount = "0";//明细行数

        /// <summary>
        /// 费用总额
        /// </summary>
        public string TotCost = "0";//费用总额

        /// <summary>
        /// 核准金额
        /// </summary>
        public string ApprovalCost = "0";//核准金额

        /// <summary>
        /// 基金支付比例 
        /// </summary>
        public string PubRate = "0";//基金支付比例

        /// <summary>
        /// 基金支付金额
        /// 基金支付金额【工伤住院】
        /// </summary>
        public string PubCost = "0";//基金支付金额

        /// <summary>
        /// 剩余限额
        /// </summary>
        public string RestLimitCost = "0";//剩余限额

        /// <summary>
        /// 结算单号
        /// </summary>
        public string BillNo = "";//结算单号

        /// <summary>
        /// 民政救助金额
        /// </summary>
        public string Subsidy = "0";//民政救助金额

        /// <summary>
        /// 补充医保自费项目补偿
        /// </summary>
        public string OwnItemSupply = "0";//补充医保自费项目补偿

        /// <summary>
        /// 基金支付个人金额(仅20131101开始的生育保险使用)
        /// 基金支付个人【住院】
        /// </summary>
        public string FundPayCost = "0";//基金支付个人金额(仅20131101开始的生育保险使用)

        /// <summary>
        /// 个人自付
        /// 个人自付金额【工伤住院】
        /// </summary>
        public string PayCost = "0";//个人自付(仅20131101开始的生育保险使用)

        /// <summary>
        /// 按比例自付(仅20131101开始的生育保险使用)
        /// </summary>
        public string PayRate = "0";//按比例自付(仅20131101开始的生育保险使用)

        /// <summary>
        /// 津贴(仅20131101开始的生育保险使用)
        /// </summary>
        public string Allowance = "0";//津贴(仅20131101开始的生育保险使用)

        /// <summary>
        /// 起付标准【住院】
        /// </summary>
        public string DeductibleLine = "0";

        /// <summary>
        /// 起付报销【住院】
        /// </summary>
        public string DeductiblePay = "0";

        /// <summary>
        /// 住院按比例自付【住院】
        /// </summary>
        public string PayRateInPatient = "0";

        /// <summary>
        /// 公务员材料费报销【住院】
        /// </summary>
        public string PayMaterialsGWY = "0";

        /// <summary>
        /// 基金支付医院金额【住院】
        /// </summary>
        public string FundPayHospitalCost = "0";

        /// <summary>
        ///超封顶限额费用【住院】
        /// </summary>
        public string OverLimitCost = "0";

        /// <summary>
        /// 补充医保自付补偿【住院】
        /// </summary>
        public string OwnPaySupply = "0";

        /// <summary>
        /// 补充医保高额补偿【住院】
        /// </summary>
        public string HighPaySupply = "0";

        /// <summary>
        /// 补充医保支付合计【住院】
        /// </summary>
        public string SumSupply = "0";

        /// <summary>
        /// 生育合并医疗支付金额【住院】
        /// </summary>
        public string SYHBPay = "0";

        /// <summary>
        /// 是否工伤基金支付【工伤住院】
        /// </summary>
        public string GsFundPayFlag = "";

        /// <summary>
        /// 药费【工伤住院】
        /// </summary>
        public string DrugTotCost = "0";

        /// <summary>
        /// 自费药【工伤住院】
        /// </summary>
        public string DrugOwnCost = "0";

        /// <summary>
        /// 床位费【工伤住院】
        /// </summary>
        public string BedCost = "0";

        /// <summary>
        /// 超床位费【工伤住院】
        /// </summary>
        public string BedOverCost = "0";

        /// <summary>
        /// 治疗费【工伤住院】
        /// </summary>
        public string TreatmentCost = "0";

        /// <summary>
        /// 检查费【工伤住院】
        /// </summary>
        public string LibCost = "0";

        /// <summary>
        /// 手术费【工伤住院】
        /// </summary>
        public string OperationCost = "0";

        /// <summary>
        /// 普及型材料费【工伤住院】
        /// </summary>
        public string MaterialsPGCost = "0";

        /// <summary>
        /// 自选材料费【工伤住院】
        /// </summary>
        public string MaterialsZXCost = "0";

        /// <summary>
        /// 材料费自费【工伤住院】
        /// </summary>
        public string MaterialsOwnCost = "0";

        /// <summary>
        /// 其它费用【工伤住院】
        /// </summary>
        public string OtherCost = "0";

        /// <summary>
        /// 其它自费【工伤住院】
        /// </summary>
        public string OtherOwnCost = "0";

        /// <summary>
        /// 单位名称【工伤住院】
        /// </summary>
        public string CompanyName = "";

        /// <summary>
        /// 单位联系电话【工伤住院】
        /// </summary>
        public string CompanyTel = "";

        /// <summary>
        /// 震波碎石次数【门诊放化疗碎石它院检查】
        /// </summary>
        public string WaveTimes = "";

        /// <summary>
        /// 输尿管结石大小【门诊放化疗碎石它院检查】
        /// </summary>
        public string SizeSNG = "";

        /// <summary>
        /// 左肾结石大小【门诊放化疗碎石它院检查】
        /// </summary>
        public string SizeZS = "";

        /// <summary>
        /// 右肾结石大小【门诊放化疗碎石它院检查】
        /// </summary>
        public string SizeYS = "";

        /// <summary>
        /// 膀胱结石大小【门诊放化疗碎石它院检查】
        /// </summary>
        public string SizePG = "";
    }
}
