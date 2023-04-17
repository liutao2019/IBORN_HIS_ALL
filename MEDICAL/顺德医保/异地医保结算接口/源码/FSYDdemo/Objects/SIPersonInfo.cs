using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoShanYDSI.Objects
{
    public class SIPersonInfo
    {
        /// <summary>
        /// 补助类型
        /// </summary>
        public string bzlx = "";

        /// <summary>
        /// 住院原因
        /// </summary>
        public string in_reason = "";

        /// <summary>
        /// 医生身份证信息
        /// </summary>
        public string docIdenno = "";

        /// <summary>
        /// 公务员补助支付费用
        /// </summary>
        public decimal GWYBZZF_cost = 0;
        /// <summary>
        /// 重大疾病或大病保险累计支付
        /// </summary>
        public decimal DBBXYLTCLJ = 0;
        /// <summary>
        /// 其他累计
        /// </summary>
        public decimal qtlj = 0;
        /// <summary>
        /// 主要费用结算方式
        /// </summary>
        public string ZYFYJSFS = "";
        /// <summary>
        /// 其他支付
        /// </summary>
        public decimal QTZF = 0;
        /// <summary>
        /// 个人自负金额
        /// </summary>
        public decimal GRZIFUJE = 0;
        /// <summary>
        ///社会保障卡号 
        /// </summary>
        public string MCardNo = "";//社会保障卡号

        /// <summary>
        /// 公民身份号码/【异地】证件号码用同一字段
        /// </summary>
        public string IdenNo = "";//公民身份号码

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name = "";//姓名

        /// <summary>
        /// 监护人1姓名
        /// </summary>
        public string Guard1Name = "";//监护人1姓名

        /// <summary>
        /// 监护人1身份号码
        /// </summary>
        public string Guard1IdenNo = "";//监护人1身份号码

        /// <summary>
        /// 监护人2姓名
        /// </summary>
        public string Guard2Name = "";//监护人2姓名

        /// <summary>
        /// 监护人2身份号码
        /// </summary>
        public string Guard2IdenNo = "";//监护人2身份号码

        /// <summary>
        /// 年度
        /// </summary>
        public string Year = "";//年度

        /// <summary>
        /// 参保险种
        /// </summary>
        public string SIType = "";//参保险种

        /// <summary>
        /// 签约主险种(仅门诊统筹返回)
        /// </summary>
        public string SIMainType = "";//签约主险种(仅门诊统筹返回)

        /// <summary>
        /// 人员类别
        /// 【异地】医疗人员类别
        /// </summary>
        public string PersonType = "";//人员类别

        /// <summary>
        /// 门诊病种
        /// </summary>
        public string OutDiseaseType = "";//门诊病种

        /// <summary>
        /// 封顶限额
        /// </summary>
        public string LimitCost = "0";//封顶限额

        /// <summary>
        /// 剩余限额
        /// </summary>
        public string RestLimitCost = "0";//剩余限额

        /// <summary>
        /// 基金支付比例
        /// </summary>
        public string PubRate = "";//基金支付比例

        /// <summary>
        /// 鉴定类型(仅工伤保险返回)
        /// </summary>
        public string EvaluateType = "";//鉴定类型(仅工伤保险返回)

        /// <summary>
        /// 受理编号(仅工伤保险返回)
        /// </summary>
        public string AcceptNo = "";//受理编号(仅工伤保险返回)

        /// <summary>
        /// 工伤事故时间(仅工伤保险返回)
        /// </summary>
        public string AccidentDate = "";//工伤事故时间(仅工伤保险返回)

        /// <summary>
        /// 工伤事故原因(仅工伤保险返回)
        /// </summary>
        public string AccidentReason = "";//工伤事故原因(仅工伤保险返回)

        /// <summary>
        /// 特定重大疾病病种
        /// </summary>
        public string OverDiseaseType = "";//特定重大疾病病种

        /// <summary>
        /// 特定重大疾病自费项目
        /// </summary>
        public string OverOwnCostItem = "";//特定重大疾病自费项目

        /// <summary>
        /// 特定重大疾病自费项目费用累计
        /// </summary>
        public string OverOwnCost = "0";//特定重大疾病自费项目费用累计

        /// <summary>
        /// 结算序号
        /// </summary>
        public string BalanceNo = "";

        /// <summary>
        /// 入院日期
        /// </summary>
        public string InDate = "";

        /// <summary>
        /// 入院科室编码
        /// </summary>
        public string InDeptCode = "";

        /// <summary>
        /// 入院科室名称
        /// </summary>
        public string InDeptName = "";

        /// <summary>
        /// 入院病区号
        /// </summary>
        public string InNurseDeptCode = "";

        /// <summary>
        /// 入院病区名称
        /// </summary>
        public string InNurseDeptName = "";

        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkMan = "";

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkPhone = "";

        /// <summary>
        /// 操作员
        /// </summary>
        public string OperName = "";

        /// <summary>
        /// 入院诊断
        /// </summary>
        public string InDiagn = "";

        #region 4.0修订

        /// <summary>
        /// 在院状态
        /// </summary>
        public string ZYZT = "";

        /// <summary>
        /// 救助对象类型
        /// </summary>
        public string JZDXLX = "";
        /// <summary>
        /// 本年度医疗救助累计金额
        /// </summary>
        public decimal BNDYLJZLJJE = 0;
        /// <summary>
        /// 本次医疗救助支付限额
        /// </summary>
        public decimal BCYLJZZFXE = 0;

        #endregion
        #region 住院使用
        /// <summary>
        /// 【住院】住院生育结算标识
        /// 【门诊】是否结算生育医疗
        /// </summary>
        public string SYFlag = "";

        /// <summary>
        /// 【住院】住院工伤结算标识
        /// </summary>
        public string GSFlag = "";

        /// <summary>
        /// 是否结算门诊统筹
        /// </summary>
        public string PTFlag = "";

        /// <summary>
        /// 【住院】连续缴费月数
        /// </summary>
        public string ContinuousMonths = "";

        /// <summary>
        /// 【住院】参保状态
        /// </summary>
        public string InsuredState = "";

        /// <summary>
        /// 【住院】住院未结算次数
        /// </summary>
        public string NoClearingTimes = "";

        /// <summary>
        /// 【住院】住院是否需审核
        /// </summary>
        public string NeedCheckFlag = "";

        /// <summary>
        /// 【住院】住院需审核原因
        /// </summary>
        public string NeedCheckReason = "";

        /// <summary>
        /// 【住院】备注
        /// </summary>
        public string Memo = "";

        /// <summary>
        /// 【住院】补充医保自付补偿累计
        /// </summary>
        public string SupplementPayCost = "0";

        /// <summary>
        /// 【住院】补充医保高额补偿累计
        /// </summary>
        public string SupplementHighPay = "0";

        /// <summary>
        /// 【住院】结算状态（1已结算，0未结算）
        /// </summary>
        public string BalanceFlag = "";

        /// <summary>
        /// 【住院】结算类型编码
        /// </summary>
        public string BalanceType = "";

        /// <summary>
        /// 【住院】结算类型名称
        /// </summary>
        public string BalanceTypeName = "";

        /// <summary>
        /// 【住院】结算项目编码
        /// </summary>
        public string BalanceItem = "";

        /// <summary>
        /// 【住院】结算项目名称
        /// </summary>
        public string BalanceItemName = "";

        /// <summary>
        /// 【住院】出院诊断1
        /// </summary>
        public string Diagn1 = "";

        /// <summary>
        /// 【住院】住院号
        /// </summary>
        public string PatientNO = "";

        /// <summary>
        /// 【住院】是否生育合并医疗
        /// </summary>
        public string SYHBFlag = "";

        /// <summary>
        /// 【住院】计生证号
        /// </summary>
        public string SyNo = "";

        /// <summary>
        /// 【住院】生育结算项目代码1编码
        /// </summary>
        public string BalanceItemSY = "";

        /// <summary>
        /// 【住院】生育结算项目代码1名称
        /// </summary>
        public string BalanceItemNameSY = "";

        #endregion

        #region 异地住院
        /// <summary>
        /// 【异地】参保地统筹区编码
        /// </summary>
        public string InsuredAreaCode = "";

        /// <summary>
        /// 【异地】参保地分中心编号
        /// </summary>
        public string InsuredCenterAreaCode = "";

        /// <summary>
        /// 【异地】就医地统筹区编码
        /// </summary>
        public string HospitalizeAreaCode = "";

        /// <summary>
        /// 【异地】就医地分中心编码
        /// </summary>
        public string HospitalizeCenterAreaCode = "";

        //医疗服务机构名称
        public string HospitalName = "";

        /// <summary>
        /// 医保编号
        /// </summary>
        public string SIcode = "";
        
        /// <summary>
        /// 结算业务号
        /// </summary>
        public string JSYWH = "";

        /// <summary>
        /// 【异地】医院编码
        /// </summary>
        public string HospitalCode = "";

        /// <summary>
        /// 【异地】性别
        /// </summary>
        public string Sex = "";

        /// <summary>
        /// 【异地】出生日期
        /// </summary>
        public string Birth = "";

        /// <summary>
        /// 【异地】人群类别
        /// </summary>
        public string RQtype = "";

        /// <summary>
        /// 【异地】公务员标识
        /// 0 非公务员;1 公务员
        /// </summary>
        public string GWYflag = "";

        /// <summary>
        /// 【异地】实际年龄
        /// </summary>
        public string Age = "";

        /// <summary>
        /// 【异地】缴费年限
        /// </summary>
        public string PayYears = "1";

        /// <summary>
        /// 【异地】帐户余额
        /// </summary>
        public string RestAccount = "0";

        /// <summary>
        /// 【异地】单位编码
        /// </summary>
        public string CompanyCode = "";

        /// <summary>
        /// 【异地】单位名称
        /// </summary>
        public string CompanyName = "";

        /// <summary>
        /// 【异地】单位类型
        /// </summary>
        public string CompanyType = "";


        /// <summary>
        /// 【异地】隶属关系
        /// </summary>
        public string LinkReal = "";

        /// <summary>
        /// 【异地】起付线累计
        /// </summary>
        public string SumCostLine = "0";

        /// <summary>
        /// 【异地】本次应付起付线
        /// </summary>
        public string CostLine = "0";

        /// <summary>
        /// 【异地】基本医疗本次支付限额
        /// </summary>
        public string LimitCostJBYL = "0";

        /// <summary>
        /// 【异地】大病医疗本次支付限额
        /// </summary>
        public string LimitCostDBYL = "0";

        /// <summary>
        /// 【异地】公务员本次支付限额
        /// </summary>
        public string LimitCostGWY = "0";

        /// <summary>
        /// 【异地】基本医疗统筹累计
        /// </summary>
        public decimal SumCostJBYL = 0;

        /// <summary>
        /// 【异地】大病医疗统筹累计
        /// </summary>
        public string SumCostDBYL = "0";

        /// <summary>
        /// 【异地】住院次数
        /// </summary>
        public string InTimes = "0";

        /// <summary>
        /// 【异地】二次返院审批标志
        /// 0：“不是二次返院”
        /// 1：“二次返院未审批”
        /// 2：“二次返院已审批”
        /// </summary>
        public string ReturnsFlag = "";

        /// <summary>
        /// 【异地】转院标志
        /// </summary>
        public string ChangeOutFlag = "";

        /// <summary>
        /// 【异地】转出就诊登记号
        /// </summary>
        public string ChangeOutClinicNo = "";

        /// <summary>
        /// 【异地】转出医院名称
        /// </summary>
        public string ChangeOutHosName = "";

        /// <summary>
        /// 【异地】登记转入医院名称
        /// </summary>
        public string ChangeInHosName = "";

        /// <summary>
        /// 【异地】就诊类别
        /// </summary>
        public string SeeDocType = "";


        /// <summary>
        /// 【异地】结算申报业务号
        /// </summary>
        public string JSSBYWH = "";


        /// <summary>
        /// 【异地】是否提交月度申报 1 是 0 否  
        /// </summary>
        public string IS_MONTH_BALANCE = "";


        /// <summary>
        /// 【异地】对账日期
        /// </summary>
        public string DZRQ_DATE = "";

        /// <summary>
        /// 【异地】转院类别
        /// </summary>
        public string ChangeType = "";

        /// <summary>
        ///【异地】转出日期
        /// </summary>
        public string ChangeDate = "";

        /// <summary>
        /// 【异地】转诊诊断
        /// </summary>
        public string ChangeDiagn = "";

        /// <summary>
        /// 【异地】转诊转院审批结果
        /// 1、未审批
        /// 2、已审批
        /// 9、审批无效 
        /// </summary>
        public string ChangePassFlag = "";

        /// <summary>
        /// 【异地】转诊原因   
        /// </summary>
        public string ChangeReason = "";

        /// <summary>
        /// 【异地】就诊登记号
        /// </summary>
        public string ClinicNo = "";

        /// <summary>
        /// 【异地】经济类型
        /// </summary>
        public string EconomicType = "";

        /// <summary>
        /// 【异地】住院状态
        /// </summary>
        public string InState = "";

        /// <summary>
        /// 【异地】费用总额
        /// </summary>
        public decimal tot_cost = 0;

        /// <summary>
        /// 【异地】经办人
        /// </summary>
        public string operater = "";

        /// <summary>
        /// 【异地】经办时间
        /// </summary>
        public string oper_date = "";

        /// <summary>
        /// 【异地】结算时间
        /// </summary>
        public string balance_date = "";

        /// <summary>
        /// 【异地】出院原因
        /// </summary>
        public string out_reason = "";

        /// <summary>
        /// 【异地】结算类型
        /// </summary>
        public string balance_type = "";

        /// <summary>
        /// 【异地】个人支付金额
        /// </summary>
        public decimal GRZF_cost = 0;

        /// <summary>
        /// 【异地】起付标准
        /// </summary>
        public decimal limit_cost = 0;

        /// <summary>
        /// 【异地】个人账户支付金额
        /// </summary>
        public decimal GZZF_cost = 0;

        /// <summary>
        /// 【异地】超报销限额自付金额
        /// </summary>
        public decimal CBXXEZF_cost = 0;

        /// <summary>
        /// 【异地】医保统筹支付
        /// </summary>
        public decimal YBTCZF_cost = 0;

        /// <summary>
        /// 【异地】统筹基金支付
        /// </summary>
        public decimal TCJJZF_cost = 0;

        /// <summary>
        /// 【异地】基本医疗统筹自付部分
        /// </summary>
        public decimal JBYLTCZF_cost = 0;

        /// <summary>
        /// 【异地】大病医疗统筹支付部分
        /// </summary>
        public decimal DBYLTCZHIF_cost = 0;

        /// <summary>
        /// 【异地】大病医疗统筹自付部分
        /// </summary>
        public decimal DBYLTCZIFU_cost = 0;

        /// <summary>
        /// 【异地】公务员补助
        /// </summary>
        public decimal GWYBZ_cost = 0;

        /// <summary>
        /// 【异地】公务员大病
        /// </summary>
        public decimal GWYDB_cost = 0;

        /// <summary>
        /// 【异地】医疗补助部分
        /// </summary>
        public decimal YLBZBF_cost = 0;

        /// <summary>
        /// 【异地】公务员超限补助部分
        /// </summary>
        public decimal GWYCXBZ_cost = 0;

        /// <summary>
        /// 【异地】其它补助支付
        /// </summary>
        public decimal QTBZZF_cost = 0;

        /// <summary>
        /// 【异地】结算床日
        /// </summary>
        public decimal balance_bed_day = 0;

        /// <summary>
        /// 补充医疗累计已支付
        /// </summary>
        public decimal BCYLLJYZF_cost = 0;

        /// <summary>
        /// 公务员补助累计已支付
        /// </summary>
        public decimal GWYBCLJYZF_cost = 0;

        /// <summary>
        /// 共付段个人支付
        /// </summary>
        public decimal GFDGRZF_cost = 0;

        /// <summary>
        /// 自费部分
        /// </summary>
        public decimal own_cost_part = 0;

        /// <summary>
        /// 自付部分
        /// </summary>
        public decimal pay_cost_part = 0;

        /// <summary>
        /// 允许报销部分
        /// </summary>
        public decimal pub_cost = 0;

        /// <summary>
        /// 大病医疗统筹支付部分
        /// </summary>
        public decimal DBYLTCZF_cost = 0;

        /// <summary>
        /// 证件类型
        /// </summary>
        public string ZJLX = "";

        /// <summary>
        /// 民族
        /// </summary>
        public string nation = "";

        /// <summary>
        /// 组织机构代码
        /// </summary>
        public string ZZJGDM = "";

        /// <summary>
        /// 异地就医备案号
        /// </summary>
        public string YDJYBAH = "";

        /// <summary>
        /// 医院结算业务序列号
        /// </summary>
        public string invoice_no = "";

        /// <summary>
        /// 医师执业证编码(这里是医生身份证号，考虑患者需要用到身份证号，未免混淆，使用这个)
        /// </summary>
        public string yszyzbm = "";

        /// <summary>
        /// 险种类型
        /// </summary>
        public string xzlx = "";

        /// <summary>
        /// 交易流水号
        /// </summary>
        public string jylsh = "";

        /// <summary>
        /// 出院诊断2
        /// </summary>
        public string Diagn2 = "";

        /// <summary>
        /// 出院诊断3
        /// </summary>
        public string Diagn3 = "";

        /// <summary>
        /// 出院诊断名称
        /// </summary>
        public string DiagnName = "";

        /// <summary>
        /// [佛山异地] 登记流水号
        /// </summary>
        public string regTransID = "";

        /// <summary>
        /// [佛山异地] 结算流水号
        /// </summary>
        public string balanceTransID = "";

        /// <summary>
        /// [佛山异地] 出院登记流水号
        /// </summary>
        public string Out_TransID = "";

        /// <summary>
        /// 住院流水号
        /// </summary>
        public string InPatient_No = "";



        //4.0修订

        /// <summary>
        /// *[佛山跨省异地]  统筹支付金额合计
        /// </summary>
        public decimal akb068 = 0;
        /// <summary>
        /// *[佛山跨省异地]  救助对象类型
        /// </summary>
        public string ykc751 = "";
        /// <summary>
        /// *[佛山跨省异地]  医疗救助金额
        /// </summary>
        public decimal ykc641 = 0;
        /// <summary>
        /// *[佛山跨省异地]  二次救助金额
        /// </summary>
        public decimal ykc642 = 0;

        /// <summary>
        /// *[佛山跨省异地]  一至六级残疾军人等医疗补助
        /// </summary>
        public decimal ykc752 = 0;

        /// <summary>
        /// *[佛山跨省异地]  本年度医疗救助累计金额

        /// </summary>
        public decimal ykc753 = 0;


        /// <summary>
        ///  操作类别
        /// </summary>
        public string oper_State = "";

        #endregion

    }

    public class SIFeeDetial
    {
        #region 异地医保费用实体
        /// <summary>
        /// HIS住院患者流水号
        /// </summary>
        public string inpatient_no = "";

        /// <summary>
        /// 处方号
        /// </summary>
        public string recipe_no = "";

        /// <summary>
        /// 明细序号
        /// </summary>
        public string balance_no = "";

        /// <summary>
        /// 大类代码（结算项目分类）
        /// </summary>
        public string class_code = "";

        /// <summary>
        /// 大类名称
        /// </summary>
        public string class_name = "";

        /// <summary>
        /// 就医地项目代码
        /// </summary>
        public string SI_item_code = "";

        /// <summary>
        /// 就医地项目名称
        /// </summary>
        public string SI_item_name = "";

        /// <summary>
        /// 药监局药品编码
        /// </summary>
        public string FDA_code = "";

        /// <summary>
        /// 限制用药标记
        /// </summary>
        public string limit_flag = "";

        /// <summary>
        /// 医用材料的注册证产品名称
        /// </summary>
        public string item_reg_name = "";

        /// <summary>
        /// 医用材料的食药监注册号
        /// </summary>
        public string item_reg_code = "";

        /// <summary>
        /// 就医地医院项目编码
        /// </summary>
        public string item_code = "";

        /// <summary>
        /// 就医地医院项目名称
        /// </summary>
        public string item_name = "";

        /// <summary>
        /// 数量
        /// </summary>
        public decimal qty = 0;

        /// <summary>
        /// 单价
        /// </summary>
        public decimal price = 0;

        /// <summary>
        /// 费用总额
        /// </summary>
        public decimal tot_cost = 0;

        /// <summary>
        /// 产地
        /// </summary>
        public string origin = "";

        /// <summary>
        /// 特项标志
        /// </summary>
        public string pecial_flag = "";

        /// <summary>
        /// 规格
        /// </summary>
        public string specs = "";

        /// <summary>
        /// 单位
        /// </summary>
        public string unit = "";

        /// <summary>
        /// 剂型
        /// </summary>
        public string drug_type = "";

        /// <summary>
        /// 处方医生姓名
        /// </summary>
        public string recipe_doc_name = "";

        /// <summary>
        /// 科室名称
        /// </summary>
        public string dept_name = "";

        /// <summary>
        /// 收费时间
        /// </summary>
        public string fee_date = "";

        /// <summary>
        /// 经办人
        /// </summary>
        public string operater = "";

        /// <summary>
        /// 经办时间
        /// </summary>
        public string oper_date = "";


        /// <summary>
        /// 自付比例
        /// </summary>
        public decimal pay_ray = 0;

        /// <summary>
        /// 自付金额
        /// </summary>
        public decimal own_cost = 0;

        /// <summary>
        /// 全自费部分
        /// </summary>
        public decimal own_cost_part = 0;

        /// <summary>
        /// 先自付部分
        /// </summary>
        public decimal first_pay_cost = 0;

        /// <summary>
        /// 允许报销部分
        /// </summary>
        public decimal pub_cost_part = 0;

        /// <summary>
        /// 收费项目等级
        /// </summary>
        public string fee_level = "";

        

        #endregion
    }
}
