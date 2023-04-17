using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace API.GZSI
{
    public class LocalManager : FS.FrameWork.Management.Database
    {

        /// <summary>
        /// 医院编码
        /// </summary>
        public const string HosCode = "006181";

        #region 共用函数

        /// <summary>
        /// 获取最大结算序号
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string getBalanceNo(string inpatientNo, string type)
        {
            string strSql = @"select max(to_number(balance_no))
                                from fin_ipr_siinmaininfo
                               where inpatient_no = '{0}'
                                 and type_code = '{1}' ";
            strSql = string.Format(strSql, inpatientNo, type);

            try
            {
                return this.ExecSqlReturnOne(strSql);
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 返回下一个结算序号
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public int GetNextBalanceNo(string inpatientNo, string type)
        {
            string currentBalanceNo = this.getBalanceNo(inpatientNo, type);
            if (string.IsNullOrEmpty(currentBalanceNo))
            {
                currentBalanceNo = "0";
            }
            int nextBalanceNO = int.Parse(currentBalanceNo) + 1;

            return nextBalanceNO;
        }

        /// <summary>
        /// 得到医保患者基本信息
        /// </summary>
        /// <param name="patient">门诊/住院实体</param>
        /// <returns></returns>
        public bool GetSIInfo(ref FS.HISFC.Models.RADT.Patient patient)
        {
            string inpatientNo = patient.ID;
            string typeCode = "1";

            FS.HISFC.Models.Registration.Register registerInfo = patient as FS.HISFC.Models.Registration.Register;
            FS.HISFC.Models.RADT.PatientInfo patientInfo = patient as FS.HISFC.Models.RADT.PatientInfo;
            //判断实体类型
            if (patient is FS.HISFC.Models.Registration.Register)
            {
                typeCode = "1";
            }
            else
            {
                typeCode = "2";
            }

            #region 查询语句

            string strSql = @"select inpatient_no, -- 0住院流水号
                                     reg_no, -- 1就医登记号
                                     fee_times, -- 2费用批次
                                     balance_no, -- 3 结算序号
                                     invoice_no, -- 4 主发票号
                                     medical_type, -- 5 医疗类别
                                     patient_no, -- 6 住院号
                                     card_no, -- 7 卡号
                                     mcard_no, -- 8 医疗证号
                                     app_no, -- 9 审批号
                                     procreate_pcno, -- 10 生育保险患者电脑号
                                     si_begindate, -- 11 参保日期
                                     si_state, -- 12 参保状态
                                     name, -- 13 姓名
                                     sex_code, -- 14 性别
                                     idenno, -- 15 身份证号码
                                     spell_code, -- 16 拼音码
                                     birthday, -- 17 生日
                                     empl_type, -- 18 人员类型
                                     work_name, -- 19 工作单位
                                     clinic_diagnose, -- 20 门诊诊断
                                     dept_code, -- 21 科室编码
                                     dept_name, -- 22 科室名称
                                     paykind_code, -- 23 结算类别
                                     pact_code, --  24 合同代码
                                     pact_name, -- 25 合同单位名称
                                     bed_no, -- 26 床号
                                     in_date, -- 27 入院日期
                                     in_diagnosedate, --28 入院诊断日期
                                     in_diagnose, -- 29 入院诊断编码
                                     in_diagnosename, -- 30 入院诊断名称
                                     out_date, -- 31 出院时间
                                     out_diagnose, -- 32 出院诊断编码
                                     out_diagnosename, -- 33 出院诊断名称
                                     balance_date, -- 34 结算日期
                                     tot_cost, -- 35 总金额
                                     pay_cost, -- 36 帐户支付
                                     pub_cost, -- 37 社保支付金额
                                     item_paycost, -- 38 部分项目自付金额
                                     base_cost, -- 39 个人起付金额
                                     item_paycost2, -- 40 个人自费项目金额
                                     item_ylcost, -- 41 个人自付金额（乙类自付部分）
                                     own_cost, -- 42 个人自负金额
                                     overtake_owncost,-- 43 超统筹支付限额个人自付金额
                                     hos_cost, -- 44 医药机构分担金额(中山医保民政统筹金额)
                                     own_cause, -- 45 自费原因
                                     oper_code, -- 46 操作员编码
                                     oper_date, -- 47 操作时间
                                     year_cost, -- 48 本年度可用定额
                                     valid_flag, -- 49 是否有效
                                     balance_state, -- 50 是否结算
                                     individualbalance, -- 51 个人账户余额
                                     freezemessage, -- 52 冻结信息
                                     applysequence, -- 53 申请序号
                                     applytypeid, -- 54 申请类型编号
                                     applytypename, -- 55 申请类型
                                     fundid, -- 56 基金编码
                                     fundname, -- 57 基金名称
                                     businesssequence, -- 58 业务序列号
                                     invoice_seq, -- 59 发票序号
                                     over_cost, -- 60 医保大额补助
                                     official_cost, -- 61 医保公务员补助
                                     remark, -- 62 医保信息（PersonAccountInfo）
                                     type_code, -- 63 人员类型
                                     trans_type, -- 64 交易类型
                                     person_type, -- 65 人员类型
                                     diagnose_oper_code, -- 66 操作员
                                     operatecode1, --67
                                     operatecode2, -- 68
                                     operatecode3, -- 69
                                     primarydiagnosename, -- 70
                                     primarydiagnosecode, -- 71
                                     ybmedno, -- 72 居民医保结算单号,保存以做为 注销居民门诊费用结算 参数之一
                                     trans_no, -- 73 上传号                                 --东莞医保
                                     internal_fee, -- 74 普通医保内费用                  --东莞医保
                                     external_fee, --75 external_fee 75 普通医保外费用                   --东莞医保
                                     official_own_cost, -- 76 大额/公务员自付金额              --东莞医保
                                     over_inte_fee, -- 77 本次交易统筹封顶后医保内金额    --东莞医保
                                     own_count_fee, -- 78  个人应付总金额(个人帐户支付+现金)    --东莞医保
                                     own_second_count_fee, -- 79 个人自付二金额    --东莞医保
                                     si_diagnose, -- 80 医保诊断代码    --东莞医保
                                     si_diagnosename, -- 81 医保诊断代码名称     --东莞医保
                                     pub_fee_cost, -- 82 统筹支付金额     --东莞医保
                                     ext_flag, -- 83 深圳医保的执行状态 登记： R 上传 ：S 结算：B 支付：J
                                     ext_flag1, -- 84 异地医保的参保地址
                                     zhuhaisitype, -- 85 参保险种(珠海医保)
                                     gzsiupload, -- 86 广州医保上传状态 未上传：0， 已上传 1
                                     mdtrt_id, -- 87 就诊ID
                                     setl_id, -- 88 结算ID
                                     psn_no, -- 89 人员编号
                                     psn_name, -- 90 人员姓名
                                     psn_cert_type, -- 91 人员证件类型
                                     certno, -- 92 证件号码
                                     gend, -- 93 性别
                                     naty, -- 94 民族
                                     brdy, -- 95 生日
                                     age, -- 96 年龄
                                     insutype, -- 97 险种类型
                                     psn_type, -- 98 人员类别
                                     cvlserv_flag, -- 99 公务员标识
                                     setl_time, -- 100 结算时间
                                     psn_setlway, -- 101 个人结算方式
                                     mdtrt_cert_type, -- 102 就诊凭证类型
                                     med_type, -- 103 医疗类别
                                     dise_code, -- 104 病种编码
                                     dise_name, -- 105 病种名称
                                     medfee_sumamt, -- 106 医疗费总额
                                     ownpay_amt, -- 107 全自费金额
                                     overlmt_selfpay, -- 108 超限价自费费用
                                     preselfpay_amt, -- 109 先行自付金额
                                     inscp_scp_amt, -- 110 符合范围金额
                                     med_sumfee, -- 111 医保认可费用总额
                                     act_pay_dedc, -- 112 实际支付起付线
                                     hifp_pay, -- 113 基本医疗保险统筹基金支出
                                     pool_prop_selfpay, -- 114 基本医疗保险统筹基金支付比例
                                     cvlserv_pay, -- 115 公务员医疗补助资金支出
                                     hifes_pay, -- 116 企业补充医疗保险基金支出
                                     hifmi_pay, -- 117 居民大病保险资金支出
                                     hifob_pay, -- 118 职工大额医疗费用补助基金支出
                                     hifdm_pay, -- 119 伤残人员医疗保障基金支出
                                     maf_pay, -- 120 医疗救助基金支出
                                     oth_pay, -- 121 其他基金支出
                                     fund_pay_sumamt, -- 122 基金支付总额
                                     hosp_part_amt, -- 123 医院负担金额
                                     psn_part_am, -- 124 个人负担总金额
                                     acct_pay, -- 125 个人账户支出
                                     cash_payamt, -- 126 现金支付金额
                                     acct_mulaid_pay, -- 127 账户共济支付金额
                                     balc, -- 128 个人账户支出后余额
                                     clr_optins, -- 129 清算经办机构
                                     clr_way, --130 清算方式
                                     clr_type, -- 131 清算类别
                                     medins_setl_id, -- 132 医药机构结算ID
                                     vola_type, -- 133 违规类型
                                     vola_dscr -- 134 违规说明
                                from fin_ipr_siinmaininfo
                               WHERE inpatient_no = '{0}'
                                 and valid_flag = '1'
                                 and type_code = '{1}' ";

            #endregion

            try
            {
                strSql = string.Format(strSql, inpatientNo, typeCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return false;
            }

            this.ExecQuery(strSql);

            try
            {
                while (Reader.Read())
                {
                    var obj = patient as FS.HISFC.Models.Registration.Register;

                    if (typeCode == "1")
                    {
                        this.setRegiseterInfo(ref registerInfo);
                        return true;
                    }
                    else
                    {
                        this.setInpatientInfo(ref patientInfo);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                Reader.Close();
                return false;
            }
        }

        /// <summary>
        /// 设置门诊实例
        /// </summary>
        /// <param name="obj"></param>
        private void setRegiseterInfo(ref FS.HISFC.Models.Registration.Register obj)
        {
            obj.ID = Reader[0].ToString(); //住院流水号
            obj.SIMainInfo.RegNo = Reader[1].ToString(); //就医登记号
            //obj.SIMainInfo.FeeTimes = Int32.Parse(Reader[2].ToString()); //费用批次
            obj.SIMainInfo.BalNo = Reader[3].ToString(); //结算序号
            obj.SIMainInfo.InvoiceNo = Reader[4].ToString(); //主发票号
            obj.SIMainInfo.MedicalType.ID = Reader[5].ToString(); //医疗类别
            obj.PID.PatientNO = Reader[6].ToString(); //住院号
            obj.PID.CardNO = Reader[7].ToString(); //卡号
            obj.SSN = Reader[8].ToString(); //医疗证号
            //obj.SIMainInfo.AppNo = Int32.Parse(Reader[9].ToString()); //审批号
            obj.SIMainInfo.ProceatePcNo = Reader[10].ToString(); //生育保险患者电脑号

            obj.SIMainInfo.SiBegionDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[11].ToString()); //参保日期
            obj.SIMainInfo.SiState = Reader[12].ToString(); //参保状态
            obj.Name = Reader[13].ToString(); //姓名
            obj.Sex.ID = Reader[14].ToString(); //性别
            obj.IDCard = Reader[15].ToString(); //身份证号码
            obj.SpellCode = Reader[16].ToString(); //拼音码
            if (!Reader.IsDBNull(17))
                obj.Birthday = DateTime.Parse(Reader[17].ToString()); //生日
            obj.SIMainInfo.EmplType = Reader[18].ToString(); //人员类型
            obj.CompanyName = Reader[19].ToString(); //工作单位
            obj.ClinicDiagnose = Reader[20].ToString(); //门诊诊断

            obj.PVisit.PatientLocation.Dept.ID = Reader[21].ToString(); //科室编码
            obj.PVisit.PatientLocation.Dept.Name = Reader[22].ToString(); //科室名称
            obj.Pact.PayKind.ID = Reader[23].ToString(); //结算类别
            obj.Pact.ID = Reader[24].ToString(); //合同代码
            obj.Pact.Name = Reader[25].ToString(); //合同单位名称
            obj.PVisit.PatientLocation.Bed.ID = Reader[26].ToString(); //床号 
            obj.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[27].ToString()); //入院日期
            obj.SIMainInfo.InDiagnoseDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[28].ToString()); //入院诊断日期
            obj.SIMainInfo.InDiagnose.ID = Reader[29].ToString(); //入院诊断编码
            obj.SIMainInfo.InDiagnose.Name = Reader[30].ToString(); //入院诊断名称

            if (!Reader.IsDBNull(31))
                obj.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[31].ToString()); //出院时间
            obj.SIMainInfo.OutDiagnose.ID = Reader[32].ToString(); //出院诊断编码
            obj.SIMainInfo.OutDiagnose.Name = Reader[33].ToString(); //出院诊断名称
            if (!Reader.IsDBNull(34))
                obj.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[34].ToString()); //结算日期
            obj.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[35].ToString()); //总金额
            obj.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[36].ToString()); //账户支付
            obj.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[37].ToString()); //社保支付金额
            obj.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[38].ToString()); //部分项目自付金额
            obj.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[39].ToString()); //个人起付金额
            obj.SIMainInfo.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[40].ToString()); //个人自费项目金额

            obj.SIMainInfo.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[41].ToString()); //个人自付金额（乙类自付部分）
            obj.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[42].ToString()); //个人自负金额
            obj.SIMainInfo.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[43].ToString()); //超统筹支付限额个人自付金额
            obj.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[44].ToString()); //医药机构分担金额(中山医保民政统筹金额)
            obj.SIMainInfo.Memo = Reader[45].ToString(); //自费原因
            obj.SIMainInfo.OperInfo.ID = Reader[46].ToString(); //操作员编码
            obj.SIMainInfo.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[47].ToString()); //操作时间
            obj.SIMainInfo.YearCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[48].ToString()); //本年度可用定额
            obj.SIMainInfo.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[49].ToString()); //是否有效
            obj.SIMainInfo.BalanceState = Reader[50].ToString(); //是否结算

            obj.SIMainInfo.IndividualBalance = FS.FrameWork.Function.NConvert.ToDecimal(Reader[51].ToString()); //个人账户余额
            obj.SIMainInfo.FreezeMessage = Reader[52].ToString(); //冻结信息
            obj.SIMainInfo.ApplySequence = Reader[53].ToString(); //申请序号
            obj.SIMainInfo.ApplyType.ID = Reader[54].ToString(); //申请类型编号
            obj.SIMainInfo.ApplyType.Name = Reader[55].ToString(); //申请类型名称
            obj.SIMainInfo.Fund.ID = Reader[56].ToString(); //基金编码
            obj.SIMainInfo.Fund.Name = Reader[57].ToString(); //基金名称
            obj.SIMainInfo.BusinessSequence = Reader[58].ToString(); //业务序列号
            //invoice_seq 发票序号 59
            obj.SIMainInfo.OverCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[60].ToString()); //医保大额补助

            obj.SIMainInfo.OfficalCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[61].ToString()); //医保公务员补助
            obj.SIMainInfo.Memo = Reader[62].ToString(); //医保信息（PersonAccountInfo）
            obj.SIMainInfo.TypeCode = Reader[63].ToString(); //人员类型
            obj.SIMainInfo.TransType = Reader[64].ToString(); //交易类型
            obj.SIMainInfo.PersonType.ID = Reader[65].ToString(); //人员类型
            //diagnose_oper_code 66 操作员
            //operatecode1 67
            //operatecode2 68
            //operatecode3 69
            //primarydiagnosename 70

            //primarydiagnosecode 71
            //ybmedno 72 居民医保结算单号,保存以做为 注销居民门诊费用结算 参数之一
            //---------------东莞医保--------------------
            //trans_no 73 上传号                                 --东莞医保
            //internal_fee 74 普通医保内费用                  --东莞医保
            //external_fee 75 普通医保外费用                   --东莞医保
            //official_own_cost 76 大额/公务员自付金额              --东莞医保
            //over_inte_fee 77 本次交易统筹封顶后医保内金额    --东莞医保
            //own_count_fee 78 个人应付总金额(个人帐户支付+现金)    --东莞医保
            //own_second_count_fee 79 个人自付二金额    --东莞医保
            //si_diagnose 80 医保诊断代码    --东莞医保

            //si_diagnosename 81医保诊断代码名称     --东莞医保
            //pub_fee_cost 82 统筹支付金额     --东莞医保
            //ext_flag 83 深圳医保的执行状态 登记： R 上传 ：S 结算：B 支付：J
            //ext_flag1 84 异地医保的参保地址
            //zhuhaisitype 85 参保险种(珠海医保)

            //gzsiupload 86 广州医保上传状态 未上传：0， 已上传 1
            obj.SIMainInfo.IsSIUploaded = FS.FrameWork.Function.NConvert.ToBoolean(Reader[86].ToString());

            //---------------广州医保API新增接口--------------------
            obj.SIMainInfo.Mdtrt_id = Reader[87].ToString(); //就诊ID
            obj.SIMainInfo.Setl_id = Reader[88].ToString(); //结算ID
            obj.SIMainInfo.Psn_no = Reader[89].ToString(); //人员编号
            obj.SIMainInfo.Psn_name = Reader[90].ToString(); // 人员姓名

            obj.SIMainInfo.Psn_cert_type = Reader[91].ToString(); //人员证件类型
            obj.SIMainInfo.Certno = Reader[92].ToString(); //证件号码
            obj.SIMainInfo.Gend = Reader[93].ToString(); //性别
            obj.SIMainInfo.Naty = Reader[94].ToString(); //民族
            obj.SIMainInfo.Brdy = FS.FrameWork.Function.NConvert.ToDateTime(Reader[95].ToString()); //生日
            obj.SIMainInfo.Age = Reader[96].ToString(); //年龄
            obj.SIMainInfo.Insutype = Reader[97].ToString(); //险种类型
            obj.SIMainInfo.Psn_type = Reader[98].ToString(); //人员类别
            obj.SIMainInfo.Cvlserv_flag = Reader[99].ToString(); //公务员标识
            obj.SIMainInfo.Setl_time = FS.FrameWork.Function.NConvert.ToDateTime(Reader[100].ToString()); //结算时间

            obj.SIMainInfo.Psn_setlway = Reader[101].ToString(); //个人结算方式
            obj.SIMainInfo.Mdtrt_cert_type = Reader[102].ToString(); //就诊凭证类型
            obj.SIMainInfo.Med_type = Reader[103].ToString(); //医疗类别
            obj.SIMainInfo.Dise_code = Reader[104].ToString(); //病种编码
            obj.SIMainInfo.Dise_name = Reader[105].ToString(); //病种名称
            obj.SIMainInfo.Medfee_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[106].ToString()); //医疗费总额
            obj.SIMainInfo.Ownpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[107].ToString()); //全自费金额
            obj.SIMainInfo.Overlmt_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[108].ToString()); //超限价自费费用
            obj.SIMainInfo.Preselfpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[109].ToString()); //先行自付金额
            obj.SIMainInfo.Inscp_scp_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[110].ToString()); //符合范围金额
            obj.SIMainInfo.Med_sumfee = FS.FrameWork.Function.NConvert.ToDecimal(Reader[111].ToString()); //医保认可费用总额
            obj.SIMainInfo.Act_pay_dedc = FS.FrameWork.Function.NConvert.ToDecimal(Reader[112].ToString()); //实际支付起付线

            obj.SIMainInfo.Hifp_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[113].ToString()); //基本医疗保险统筹基金支出
            obj.SIMainInfo.Pool_prop_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[114].ToString()); //基本医疗保险统筹基金支付比例
            obj.SIMainInfo.Cvlserv_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[115].ToString()); //公务员医疗补助资金支出
            obj.SIMainInfo.Hifes_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[116].ToString()); //企业补充医疗保险基金支出
            obj.SIMainInfo.Hifmi_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[117].ToString()); //居民大病保险资金支出
            obj.SIMainInfo.Hifob_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[118].ToString()); //职工大额医疗费用补助基金支出
            obj.SIMainInfo.Hifdm_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[119].ToString()); //伤残人员医疗保障基金支出
            obj.SIMainInfo.Maf_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[120].ToString()); //医疗救助基金支出
            obj.SIMainInfo.Oth_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[121].ToString()); //其他基金支出
            obj.SIMainInfo.Fund_pay_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[122].ToString()); //基金支付总额

            obj.SIMainInfo.Hosp_part_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[1231].ToString()); //医院负担金额
            obj.SIMainInfo.Psn_part_am = FS.FrameWork.Function.NConvert.ToDecimal(Reader[124].ToString()); //个人负担总金额
            obj.SIMainInfo.Acct_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[125].ToString()); //个人账户支出
            obj.SIMainInfo.Cash_payamt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[126].ToString()); //现金支付金额
            obj.SIMainInfo.Acct_mulaid_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[127].ToString()); //账户共济支付金额
            obj.SIMainInfo.Balc = FS.FrameWork.Function.NConvert.ToDecimal(Reader[128].ToString()); //个人账户支出后余额
            obj.SIMainInfo.Clr_optins = Reader[129].ToString(); //清算经办机构
            obj.SIMainInfo.Clr_way = Reader[130].ToString(); //清算方式
            obj.SIMainInfo.Clr_type = Reader[131].ToString(); //清算类别
            obj.SIMainInfo.Medins_setl_id = Reader[132].ToString(); //医药机构结算ID

            obj.SIMainInfo.Vola_type = Reader[133].ToString(); //违规类型
            obj.SIMainInfo.Vola_dscr = Reader[134].ToString(); //违规说明
        }

        /// <summary>
        /// 设置住院实例
        /// </summary>
        /// <param name="obj"></param>
        private void setInpatientInfo(ref FS.HISFC.Models.RADT.PatientInfo obj)
        {
            obj.ID = Reader[0].ToString(); //住院流水号
            obj.SIMainInfo.RegNo = Reader[1].ToString(); //就医登记号
            //obj.SIMainInfo.FeeTimes = Int32.Parse(Reader[2].ToString()); //费用批次
            obj.SIMainInfo.BalNo = Reader[3].ToString(); //结算序号
            obj.SIMainInfo.InvoiceNo = Reader[4].ToString(); //主发票号
            obj.SIMainInfo.MedicalType.ID = Reader[5].ToString(); //医疗类别
            obj.PID.PatientNO = Reader[6].ToString(); //住院号
            obj.PID.CardNO = Reader[7].ToString(); //卡号
            obj.SSN = Reader[8].ToString(); //医疗证号
            //obj.SIMainInfo.AppNo = Int32.Parse(Reader[9].ToString()); //审批号
            obj.SIMainInfo.ProceatePcNo = Reader[10].ToString(); //生育保险患者电脑号

            obj.SIMainInfo.SiBegionDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[11].ToString()); //参保日期
            obj.SIMainInfo.SiState = Reader[12].ToString(); //参保状态
            obj.Name = Reader[13].ToString(); //姓名
            obj.Sex.ID = Reader[14].ToString(); //性别
            obj.IDCard = Reader[15].ToString(); //身份证号码
            obj.SpellCode = Reader[16].ToString(); //拼音码
            if (!Reader.IsDBNull(17))
                obj.Birthday = DateTime.Parse(Reader[17].ToString()); //生日
            obj.SIMainInfo.EmplType = Reader[18].ToString(); //人员类型
            obj.CompanyName = Reader[19].ToString(); //工作单位
            obj.ClinicDiagnose = Reader[20].ToString(); //门诊诊断

            obj.PVisit.PatientLocation.Dept.ID = Reader[21].ToString(); //科室编码
            obj.PVisit.PatientLocation.Dept.Name = Reader[22].ToString(); //科室名称
            obj.Pact.PayKind.ID = Reader[23].ToString(); //结算类别
            obj.Pact.ID = Reader[24].ToString(); //合同代码
            obj.Pact.Name = Reader[25].ToString(); //合同单位名称
            obj.PVisit.PatientLocation.Bed.ID = Reader[26].ToString(); //床号 
            obj.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[27].ToString()); //入院日期
            obj.SIMainInfo.InDiagnoseDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[28].ToString()); //入院诊断日期
            obj.SIMainInfo.InDiagnose.ID = Reader[29].ToString(); //入院诊断编码
            obj.SIMainInfo.InDiagnose.Name = Reader[30].ToString(); //入院诊断名称

            if (!Reader.IsDBNull(31))
                obj.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[31].ToString()); //出院时间
            obj.SIMainInfo.OutDiagnose.ID = Reader[32].ToString(); //出院诊断编码
            obj.SIMainInfo.OutDiagnose.Name = Reader[33].ToString(); //出院诊断名称
            if (!Reader.IsDBNull(34))
                obj.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[34].ToString()); //结算日期
            obj.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[35].ToString()); //总金额
            obj.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[36].ToString()); //账户支付
            obj.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[37].ToString()); //社保支付金额
            obj.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[38].ToString()); //部分项目自付金额
            obj.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[39].ToString()); //个人起付金额
            obj.SIMainInfo.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[40].ToString()); //个人自费项目金额

            obj.SIMainInfo.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[41].ToString()); //个人自付金额（乙类自付部分）
            obj.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[42].ToString()); //个人自负金额
            obj.SIMainInfo.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[43].ToString()); //超统筹支付限额个人自付金额
            obj.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[44].ToString()); //医药机构分担金额(中山医保民政统筹金额)
            obj.SIMainInfo.Memo = Reader[45].ToString(); //自费原因
            obj.SIMainInfo.OperInfo.ID = Reader[46].ToString(); //操作员编码
            obj.SIMainInfo.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[47].ToString()); //操作时间
            obj.SIMainInfo.YearCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[48].ToString()); //本年度可用定额
            obj.SIMainInfo.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[49].ToString()); //是否有效
            obj.SIMainInfo.BalanceState = Reader[50].ToString(); //是否结算

            obj.SIMainInfo.IndividualBalance = FS.FrameWork.Function.NConvert.ToDecimal(Reader[51].ToString()); //个人账户余额
            obj.SIMainInfo.FreezeMessage = Reader[52].ToString(); //冻结信息
            obj.SIMainInfo.ApplySequence = Reader[53].ToString(); //申请序号
            obj.SIMainInfo.ApplyType.ID = Reader[54].ToString(); //申请类型编号
            obj.SIMainInfo.ApplyType.Name = Reader[55].ToString(); //申请类型名称
            obj.SIMainInfo.Fund.ID = Reader[56].ToString(); //基金编码
            obj.SIMainInfo.Fund.Name = Reader[57].ToString(); //基金名称
            obj.SIMainInfo.BusinessSequence = Reader[58].ToString(); //业务序列号
            //invoice_seq 发票序号 59
            obj.SIMainInfo.OverCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[60].ToString()); //医保大额补助

            obj.SIMainInfo.OfficalCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[61].ToString()); //医保公务员补助
            obj.SIMainInfo.Memo = Reader[62].ToString(); //医保信息（PersonAccountInfo）
            obj.SIMainInfo.TypeCode = Reader[63].ToString(); //人员类型
            obj.SIMainInfo.TransType = Reader[64].ToString(); //交易类型
            obj.SIMainInfo.PersonType.ID = Reader[65].ToString(); //人员类型
            //diagnose_oper_code 66 操作员
            //operatecode1 67
            //operatecode2 68
            //operatecode3 69
            //primarydiagnosename 70

            //primarydiagnosecode 71
            //ybmedno 72 居民医保结算单号,保存以做为 注销居民门诊费用结算 参数之一
            //---------------东莞医保--------------------
            //trans_no 73 上传号                                 --东莞医保
            //internal_fee 74 普通医保内费用                  --东莞医保
            //external_fee 75 普通医保外费用                   --东莞医保
            //official_own_cost 76 大额/公务员自付金额              --东莞医保
            //over_inte_fee 77 本次交易统筹封顶后医保内金额    --东莞医保
            //own_count_fee 78 个人应付总金额(个人帐户支付+现金)    --东莞医保
            //own_second_count_fee 79 个人自付二金额    --东莞医保
            //si_diagnose 80 医保诊断代码    --东莞医保

            //si_diagnosename 81医保诊断代码名称     --东莞医保
            //pub_fee_cost 82 统筹支付金额     --东莞医保
            //ext_flag 83 深圳医保的执行状态 登记： R 上传 ：S 结算：B 支付：J
            //ext_flag1 84 异地医保的参保地址
            //zhuhaisitype 85 参保险种(珠海医保)

            //gzsiupload 86 广州医保上传状态 未上传：0， 已上传 1
            obj.SIMainInfo.IsSIUploaded = FS.FrameWork.Function.NConvert.ToBoolean(Reader[86].ToString());

            //---------------广州医保API新增接口--------------------
            obj.SIMainInfo.Mdtrt_id = Reader[87].ToString(); //就诊ID
            obj.SIMainInfo.Setl_id = Reader[88].ToString(); //结算ID
            obj.SIMainInfo.Psn_no = Reader[89].ToString(); //人员编号
            obj.SIMainInfo.Psn_name = Reader[90].ToString(); // 人员姓名

            obj.SIMainInfo.Psn_cert_type = Reader[91].ToString(); //人员证件类型
            obj.SIMainInfo.Certno = Reader[92].ToString(); //证件号码
            obj.SIMainInfo.Gend = Reader[93].ToString(); //性别
            obj.SIMainInfo.Naty = Reader[94].ToString(); //民族
            obj.SIMainInfo.Brdy = FS.FrameWork.Function.NConvert.ToDateTime(Reader[95].ToString()); //生日
            obj.SIMainInfo.Age = Reader[96].ToString(); //年龄
            obj.SIMainInfo.Insutype = Reader[97].ToString(); //险种类型
            obj.SIMainInfo.Psn_type = Reader[98].ToString(); //人员类别
            obj.SIMainInfo.Cvlserv_flag = Reader[99].ToString(); //公务员标识
            obj.SIMainInfo.Setl_time = FS.FrameWork.Function.NConvert.ToDateTime(Reader[100].ToString()); //结算时间

            obj.SIMainInfo.Psn_setlway = Reader[101].ToString(); //个人结算方式
            obj.SIMainInfo.Mdtrt_cert_type = Reader[102].ToString(); //就诊凭证类型
            obj.SIMainInfo.Med_type = Reader[103].ToString(); //医疗类别
            obj.SIMainInfo.Dise_code = Reader[104].ToString(); //病种编码
            obj.SIMainInfo.Dise_name = Reader[105].ToString(); //病种名称
            obj.SIMainInfo.Medfee_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[106].ToString()); //医疗费总额
            obj.SIMainInfo.Ownpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[107].ToString()); //全自费金额
            obj.SIMainInfo.Overlmt_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[108].ToString()); //超限价自费费用
            obj.SIMainInfo.Preselfpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[109].ToString()); //先行自付金额
            obj.SIMainInfo.Inscp_scp_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[110].ToString()); //符合范围金额
            obj.SIMainInfo.Med_sumfee = FS.FrameWork.Function.NConvert.ToDecimal(Reader[111].ToString()); //医保认可费用总额
            obj.SIMainInfo.Act_pay_dedc = FS.FrameWork.Function.NConvert.ToDecimal(Reader[112].ToString()); //实际支付起付线

            obj.SIMainInfo.Hifp_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[113].ToString()); //基本医疗保险统筹基金支出
            obj.SIMainInfo.Pool_prop_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[114].ToString()); //基本医疗保险统筹基金支付比例
            obj.SIMainInfo.Cvlserv_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[115].ToString()); //公务员医疗补助资金支出
            obj.SIMainInfo.Hifes_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[116].ToString()); //企业补充医疗保险基金支出
            obj.SIMainInfo.Hifmi_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[117].ToString()); //居民大病保险资金支出
            obj.SIMainInfo.Hifob_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[118].ToString()); //职工大额医疗费用补助基金支出
            obj.SIMainInfo.Hifdm_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[119].ToString()); //伤残人员医疗保障基金支出
            obj.SIMainInfo.Maf_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[120].ToString()); //医疗救助基金支出
            obj.SIMainInfo.Oth_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[121].ToString()); //其他基金支出
            obj.SIMainInfo.Fund_pay_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[122].ToString()); //基金支付总额

            obj.SIMainInfo.Hosp_part_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[123].ToString()); //医院负担金额
            obj.SIMainInfo.Psn_part_am = FS.FrameWork.Function.NConvert.ToDecimal(Reader[124].ToString()); //个人负担总金额
            obj.SIMainInfo.Acct_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[125].ToString()); //个人账户支出
            obj.SIMainInfo.Cash_payamt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[126].ToString()); //现金支付金额
            obj.SIMainInfo.Acct_mulaid_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[127].ToString()); //账户共济支付金额
            obj.SIMainInfo.Balc = FS.FrameWork.Function.NConvert.ToDecimal(Reader[128].ToString()); //个人账户支出后余额
            obj.SIMainInfo.Clr_optins = Reader[129].ToString(); //清算经办机构
            obj.SIMainInfo.Clr_way = Reader[130].ToString(); //清算方式
            obj.SIMainInfo.Clr_type = Reader[131].ToString(); //清算类别
            obj.SIMainInfo.Medins_setl_id = Reader[132].ToString(); //医药机构结算ID

            obj.SIMainInfo.Vola_type = Reader[133].ToString(); //违规类型
            obj.SIMainInfo.Vola_dscr = Reader[134].ToString(); //违规说明
            if (this.Reader.FieldCount > 135)
            {
                obj.Sellmanager.Memo = Reader[135].ToString(); //借用, 发票代码
            }

            if (this.Reader.FieldCount > 136)
            {
                obj.ServiceInfo.Memo = Reader[136].ToString();//借用, 发票号
            }
           

           
        }

        /// <summary>
        /// 转换罗马数字(由Ⅰ,ⅡⅢ... 变换成 1,2,3...)
        /// </summary>
        /// <param name="oldstr"></param>
        /// <param name="newstr"></param>
        /// <returns>0成功 ,-1 失败</returns>
        public static string ConvertDeptRomanNum(string romanStr)
        {
            try
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"[Ⅰ,ⅡⅢ,Ⅳ,Ⅴ,Ⅵ,Ⅶ,Ⅷ,Ⅸ,Ⅹ]");
                string currntstr = romanStr;
                foreach (System.Text.RegularExpressions.Match match in regex.Matches(romanStr))
                {
                    int index = match.Index;
                    string matchvalue = match.Value;
                    string numstr = "";
                    if (matchvalue == "Ⅰ") numstr = "1";
                    if (matchvalue == "Ⅱ") numstr = "2";
                    if (matchvalue == "Ⅲ") numstr = "3";
                    if (matchvalue == "Ⅳ") numstr = "4";
                    if (matchvalue == "Ⅴ") numstr = "5";
                    if (matchvalue == "Ⅵ") numstr = "6";
                    if (matchvalue == "Ⅶ") numstr = "7";
                    if (matchvalue == "Ⅷ") numstr = "8";
                    if (matchvalue == "Ⅸ") numstr = "9";
                    if (string.IsNullOrEmpty(numstr))
                    {
                        continue;
                    }
                    else
                    {
                        currntstr = currntstr.Remove(index, 1);
                        currntstr = currntstr.Insert(index, numstr);
                        numstr = "";
                    }
                }
                currntstr = currntstr.Replace("＆", "");
                return currntstr;
            }
            catch (Exception e)
            {

            }
            return romanStr;

        }

        #endregion

        #region 控费方面
        /// <summary>
        /// 获取门诊医保账户控费项目明细
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="itemCode"></param>
        /// <param name="limitAccountFeeItem"></param>
        /// <returns></returns>
        public int GetAccountLimitFeeItem(string cardno, string itemCode, ref FS.HISFC.Models.Fee.Item.LimitAccountFeeItem limitAccountFeeItem)
        {
            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.GetAccountLimitFeeItem.Select.1", ref strSql) == -1)
                return -1;

            try
            {
                strSql = string.Format(strSql, cardno, itemCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    FS.HISFC.Models.Fee.Item.LimitAccountFeeItem obj = new FS.HISFC.Models.Fee.Item.LimitAccountFeeItem();

                    obj.CardNo = Reader[0].ToString();
                    obj.Name = Reader[1].ToString();
                    obj.XMBH = Reader[2].ToString();
                    obj.XMMC = Reader[3].ToString();
                    obj.CreateTime = Convert.ToDateTime(Reader[4].ToString());
                    obj.ClinicCode = Reader[5].ToString();
                    obj.JE = Convert.ToDouble(Reader[6]);
                    obj.QTY = Convert.ToInt32(Reader[7]);

                    al.Add(obj);
                }

                Reader.Close();

                if (al.Count > 0)
                {
                    limitAccountFeeItem = (FS.HISFC.Models.Fee.Item.LimitAccountFeeItem)al[0];
                    return 0;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 插入门诊医保账户控费项目
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int InsertLimitAccountFeeItem(FS.HISFC.Models.Fee.Outpatient.FeeItemList f, string itemid, string itemname, decimal qty, string clincid, FS.HISFC.Models.Registration.Register r)
        {
            string sqlStr = @"INSERT INTO GZSI_HIS_ACCOUNTDETAIL 
                                          (CARD_NO,  --卡号
                                           NAME,  --姓名
                                           XMBH, --项目编号
                                           XMMC, --项目名称
                                           CREATETIME, --创建时间
                                           CLINIC_CODE, --分诊号
                                           JE, --金额
                                           QTY, --次数
                                           ID  ) 
                                    VALUES ('{0}',
                                           '{1}',
                                           '{2}',
                                           '{3}',
                                           to_date('{4}','yyyy-MM-dd hh24:mi:ss'),
                                           '{5}',
                                           '{6}',
                                           '{7}','{8}')";

            try
            {
                string sql = string.Format(sqlStr, f.Patient.PID.CardNO,  //就诊ID
                                                  r.Name, //结算ID
                                                  itemid, //结算ID
                                                  itemname, //结算ID
                                                  DateTime.Now, //结算ID
                                                  r.ID, //结算ID
                                                  "", //结算ID
                                                  qty, //结算ID
                                                  clincid
                                                  );

                int ret = this.ExecNoQuery(sql);

                if (ret < 1)
                {
                    this.ErrCode = "1";
                    this.Err = "插入门诊医保账户控费项目出错";
                    return -1;
                }

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }


        /// <summary>
        /// 修改次数
        /// </summary>
        /// <param name="r"></param>
        /// <param name="itemmedical"></param>
        /// <param name="qty"></param>
        /// <returns></returns>
        public int UpdateExpItemDetailQty(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Account.ExpItemMedical itemmedical, decimal qty)
        {

            string sqlStr = @"update exp_itemmedical set RTN_QTY=RTN_QTY - {4} , CONFIRM_QTY=CONFIRM_QTY + {4} ,OPER_CODE ='{3}',OPER_DATE = to_date('{2}','yyyy-MM-dd hh24:mi:ss') where clinic_code ='{1}' and card_no ='{0}' ";

            try
            {
                string sql = string.Format(sqlStr, r.PID.CardNO,  //就诊号
                                                  itemmedical.ClinicCode, //执行编码
                                                  DateTime.Now, //时间
                                                  FS.FrameWork.Management.Connection.Operator.ID, //操作人编码
                                                  qty
                                                  );
                int ret = this.ExecNoQuery(sql);

                if (ret < 1)
                {
                    this.ErrCode = "1";
                    this.Err = "修改医保控费剩余次失败";
                    return -1;
                }

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;


        }

        #endregion
        #region 门诊医保

        /// <summary>
        /// 获取门诊医保结算信息
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int GetRegInfoOutpatient(ref FS.HISFC.Models.Registration.Register register)
        {
            string strSql = @"SELECT INPATIENT_NO,
                                     REG_NO,
                                     BALANCE_NO,
                                     INVOICE_NO,
                                     MDTRT_ID,
                                     SETL_ID,
                                     PSN_NO,
                                     PSN_NAME,
                                     PSN_CERT_TYPE,
                                     CERTNO,
                                     GEND,
                                     NATY,
                                     BRDY,
                                     AGE,
                                     INSUTYPE,
                                     PSN_TYPE,
                                     CVLSERV_FLAG,
                                     SETL_TIME,
                                     PSN_SETLWAY,
                                     MDTRT_CERT_TYPE,
                                     MED_TYPE,
                                     MEDFEE_SUMAMT,
                                     OWNPAY_AMT,
                                     OVERLMT_SELFPAY,
                                     PRESELFPAY_AMT,
                                     INSCP_SCP_AMT,
                                     MED_SUMFEE,
                                     ACT_PAY_DEDC,
                                     HIFP_PAY,
                                     POOL_PROP_SELFPAY,
                                     CVLSERV_PAY,
                                     HIFES_PAY,
                                     HIFMI_PAY,
                                     HIFOB_PAY,
                                     HIFDM_PAY,
                                     MAF_PAY,
                                     OTH_PAY,
                                     FUND_PAY_SUMAMT,
                                     HOSP_PART_AMT,
                                     PSN_PART_AM,
                                     ACCT_PAY,
                                     CASH_PAYAMT,
                                     ACCT_MULAID_PAY,
                                     BALC,
                                     CLR_OPTINS,
                                     CLR_WAY,
                                     CLR_TYPE,
                                     MEDINS_SETL_ID,
                                     VOLA_TYPE,
                                     VOLA_DSCR
                                FROM FIN_IPR_SIINMAININFO
                               WHERE INPATIENT_NO = '{0}' 
                                 AND INVOICE_NO = '{1}'
                                 AND TYPE_CODE = '{2}' ";

            try
            {
                strSql = string.Format(strSql, register.ID, register.SIMainInfo.InvoiceNo, "1");
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                this.ExecQuery(strSql);

                while (Reader.Read())
                {
                    register.SIMainInfo.RegNo = Reader[1].ToString();//结算发票号
                    register.SIMainInfo.BalNo = Reader[2].ToString();//结算发票号
                    register.SIMainInfo.InvoiceNo = Reader[3].ToString();//结算发票号
                    register.SIMainInfo.Mdtrt_id = Reader[4].ToString();  //就诊ID
                    register.SIMainInfo.Setl_id = Reader[5].ToString(); //结算id
                    register.SIMainInfo.Psn_no = Reader[6].ToString();//人员编号
                    register.SIMainInfo.Psn_name = Reader[7].ToString();//人员姓名
                    register.SIMainInfo.Psn_cert_type = Reader[8].ToString();//人员证件类型
                    register.SIMainInfo.Certno = Reader[9].ToString();//证件号码
                    register.SIMainInfo.Gend = Reader[10].ToString();//性别
                    register.SIMainInfo.Naty = Reader[11].ToString();//民族
                    register.SIMainInfo.Brdy = DateTime.Parse(Reader[12].ToString());//出生日期
                    register.SIMainInfo.Age = Reader[13].ToString();//年龄
                    register.SIMainInfo.Insutype = Reader[14].ToString();//险种类型
                    register.SIMainInfo.Psn_type = Reader[15].ToString();//人员类别
                    register.SIMainInfo.Cvlserv_flag = Reader[16].ToString();//公务员标志
                    register.SIMainInfo.Setl_time = DateTime.Parse(Reader[17].ToString());//结算时间
                    register.SIMainInfo.Psn_setlway = Reader[18].ToString();//个人结算方式
                    register.SIMainInfo.Mdtrt_cert_type = Reader[19].ToString();//就诊凭证类型
                    register.SIMainInfo.Med_type = Reader[20].ToString();//医疗类别
                    register.SIMainInfo.Medfee_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[21].ToString());//医疗费总额
                    register.SIMainInfo.Ownpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[22].ToString());//全自费金额Ownpay_amt
                    register.SIMainInfo.Overlmt_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[23].ToString());//超限价自费费用
                    register.SIMainInfo.Preselfpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[24].ToString());//先行自付金额
                    register.SIMainInfo.Inscp_scp_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[25].ToString());//符合范围金额
                    register.SIMainInfo.Med_sumfee = FS.FrameWork.Function.NConvert.ToDecimal(Reader[26].ToString());//医保认可费用总额
                    register.SIMainInfo.Act_pay_dedc = FS.FrameWork.Function.NConvert.ToDecimal(Reader[27].ToString());//实际支付起付线
                    register.SIMainInfo.Hifp_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[28].ToString());//基本医疗保险统筹基金支出
                    register.SIMainInfo.Pool_prop_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[29].ToString());//基本医疗保险统筹基金支付比例
                    register.SIMainInfo.Cvlserv_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[30].ToString());//公务员医疗补助资金支出
                    register.SIMainInfo.Hifes_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[31].ToString());//企业补充医疗保险基金支出
                    register.SIMainInfo.Hifmi_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[32].ToString());//居民大病保险资金支出
                    register.SIMainInfo.Hifob_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[33].ToString());//职工大额医疗费用补助基金支出
                    register.SIMainInfo.Hifdm_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[34].ToString());//伤残人员医疗保障基金支出
                    register.SIMainInfo.Maf_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[35].ToString());//医疗救助基金支出
                    register.SIMainInfo.Oth_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[36].ToString());//其他基金支出
                    register.SIMainInfo.Fund_pay_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[37].ToString());//基金支付总额
                    register.SIMainInfo.Hosp_part_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[38].ToString());//医院负担金额
                    register.SIMainInfo.Psn_part_am = FS.FrameWork.Function.NConvert.ToDecimal(Reader[39].ToString());//个人负担总金额
                    register.SIMainInfo.Acct_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[40].ToString());//个人账户支出
                    register.SIMainInfo.Cash_payamt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[41].ToString());//现金支付金额
                    register.SIMainInfo.Acct_mulaid_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[42].ToString());//账户共济支付金额
                    register.SIMainInfo.Balc = FS.FrameWork.Function.NConvert.ToDecimal(Reader[43].ToString());//个人账户支出后余额
                    register.SIMainInfo.Clr_optins = Reader[44].ToString();//清算经办机构
                    register.SIMainInfo.Clr_way = Reader[45].ToString();//清算方式
                    register.SIMainInfo.Clr_type = Reader[46].ToString();//清算类别
                    register.SIMainInfo.Medins_setl_id = Reader[47].ToString();//医药机构结算id
                    register.SIMainInfo.Vola_type = Reader[48].ToString();//违规类型
                    register.SIMainInfo.Vola_dscr = Reader[49].ToString();//违规说明
                }

                Reader.Close();
                return 1;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                Reader.Close();
                return -1;
            }
        }

        /// <summary>
        /// 获得医保患者基本信息
        /// [未使用]
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="typeCode">0-挂号， 1-门诊， 2-住院</param>
        /// <returns></returns>
        public FS.HISFC.Models.Registration.Register GetOutPatientBalanceInfo(FS.HISFC.Models.Registration.Register register, string typeCode)
        {
            #region Region
            FS.HISFC.Models.Registration.Register obj = new FS.HISFC.Models.Registration.Register();
            obj = register.Clone();
            string strSqlWhere = "";
            string strSql = @"SELECT inpatient_no,   --住院流水号
                                     reg_no,       --就医登记号
                                     balance_no,   --结算序号
                                     invoice_no,   --发票号
                                     medical_type,   --医疗类别
                                     patient_no,   --住院号
                                     card_no,   --就诊卡号
                                     mcard_no,   --医疗证号
                                     name,   --姓名
                                     sex_code,   --性别
                                     idenno,   --身份证号
                                     birthday,   --生日
                                     empl_type,   --人员类别 1 在职 2 退休
                                     work_name,   --工作单位
                                     clinic_diagnose,   --门诊诊断
                                     dept_code,   --科室代码
                                     dept_name,   --科室名称
                                     paykind_code,   --结算类别 1-自费  2-保险 3-公费在职 4-公费退休 5-公费高干
                                     pact_code,   --合同代码
                                     pact_name,   --合同单位名称
                                     bed_no,   --床号
                                     in_date,   --入院日期
                                     in_diagnosedate,--入院诊断日期
                                     in_diagnose,   --入院诊断代码
                                     in_diagnosename,   --入院诊断名称
                                     out_date,   --出院日期
                                     out_diagnose,   --出院诊断代码
                                     out_diagnosename,   --出院诊断名称
                                     balance_date,   --结算日期(上次)
                                     tot_cost,   --费用金额(未结)(住院总金额)
                                     pay_cost,   --帐户支付
                                     pub_cost,   --公费金额(未结)(社保支付金额)
                                     item_paycost,   --部分项目自付金额
                                     base_cost,   --个人起付金额
                                     item_paycost2,   --个人自费项目金额
                                     item_ylcost,   --个人自付金额（乙类自付部分）
                                     own_cost,   --个人自负金额
                                     overtake_owncost,   --超统筹支付限额个人自付金额
                                     own_cause,   --自费原因
                                     oper_code,   --操作员
                                     oper_date,    --操作日期
                                     fee_times,
                                     hos_cost,
                                     year_cost,
                                     VALID_FLAG,
                                     BALANCE_STATE,
                                     remark,
                                     type_code,
                                     over_cost,
                                     person_type,
                                     bka911,
                                     bka912,
                                     bka913,
                                     bka914,
                                     bka915,
                                     bka916,
                                     bka917,
                                     bka042,
                                     aaz267,
                                     bka825,
                                     bka826,
                                     aka151,
                                     bka838,
                                     akb067,
                                     akb066,
                                     bka821,
                                     bka839,
                                     ake039,
                                     ake035,
                                     ake026,
                                     ake029,
                                     bka841,
                                     bka842,
                                     bka840,
                                     aaa027,
                                     bka006,
                                     ic_reg_permit,
                                     Bka438
                                FROM fin_ipr_siinmaininfo_gd   --广东省统一医保信息住院主表
                            ";

            if (register.SIMainInfo.OperInfo.User02 == "ZZSB")
            {
                strSqlWhere = @" WHERE inpatient_no = '{0}'
                                   and balance_no = '{1}'
                                   and pact_code = '{2}'
                                   and valid_flag = '1'
                                   and type_code = '{3}'";
                try
                {
                    strSqlWhere = string.Format(strSqlWhere,
                        register.ID,
                        register.SIMainInfo.BalNo,
                        register.Pact.ID,
                        typeCode);
                }
                catch (Exception ex)
                {
                    this.ErrCode = ex.Message;
                    this.Err = ex.Message;
                    return null;
                }
            }
            else
            {
                strSqlWhere = @" WHERE inpatient_no = '{0}'
                                   and invoice_no = '{1}'
                                   and pact_code = '{2}'
                                   and valid_flag = '1'
                                   and type_code = '{3}'";
                try
                {
                    strSqlWhere = string.Format(strSqlWhere,
                        register.ID,
                        register.InvoiceNO,
                        register.Pact.ID,
                        typeCode);
                }
                catch (Exception ex)
                {
                    this.ErrCode = ex.Message;
                    this.Err = ex.Message;
                    return null;
                }
            }

            this.ExecQuery(strSql + strSqlWhere);
            try
            {
                while (Reader.Read())
                {
                    obj.ID = Reader[0].ToString();
                    obj.SIMainInfo.RegNo = Reader[1].ToString();
                    obj.SIMainInfo.BalNo = Reader[2].ToString();
                    obj.SIMainInfo.InvoiceNo = Reader[3].ToString();
                    obj.SIMainInfo.MedicalType.ID = Reader[4].ToString();
                    obj.PID.PatientNO = Reader[5].ToString();
                    obj.PID.CardNO = Reader[6].ToString();
                    obj.SSN = Reader[7].ToString();
                    obj.Name = Reader[8].ToString();
                    obj.Sex.ID = Reader[9].ToString();
                    obj.IDCard = Reader[10].ToString();
                    obj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[11].ToString());
                    obj.SIMainInfo.EmplType = Reader[12].ToString();
                    obj.CompanyName = Reader[13].ToString();
                    obj.ClinicDiagnose = Reader[14].ToString();
                    obj.PVisit.PatientLocation.Dept.ID = Reader[15].ToString();
                    obj.PVisit.PatientLocation.Dept.Name = Reader[26].ToString();
                    obj.Pact.PayKind.ID = Reader[17].ToString();
                    obj.Pact.ID = Reader[18].ToString();
                    obj.Pact.Name = Reader[19].ToString();
                    obj.PVisit.PatientLocation.Bed.ID = Reader[20].ToString();
                    obj.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[21].ToString());
                    obj.SIMainInfo.InDiagnoseDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[22].ToString());
                    obj.SIMainInfo.InDiagnose.ID = Reader[23].ToString();
                    obj.SIMainInfo.InDiagnose.Name = Reader[24].ToString();
                    if (!Reader.IsDBNull(25))
                        obj.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[25].ToString());
                    obj.SIMainInfo.OutDiagnose.ID = Reader[26].ToString();
                    obj.SIMainInfo.OutDiagnose.Name = Reader[27].ToString();
                    if (!Reader.IsDBNull(28))
                        obj.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[28].ToString());

                    obj.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[29].ToString());
                    obj.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[30].ToString());
                    obj.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[31].ToString());
                    obj.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[32].ToString());
                    obj.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[33].ToString());
                    obj.SIMainInfo.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[34].ToString());
                    obj.SIMainInfo.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[35].ToString());
                    obj.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[36].ToString());
                    obj.SIMainInfo.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[37].ToString());
                    obj.SIMainInfo.Memo = Reader[38].ToString();
                    obj.SIMainInfo.OperInfo.ID = Reader[39].ToString();
                    obj.SIMainInfo.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[40].ToString());
                    obj.SIMainInfo.FeeTimes = FS.FrameWork.Function.NConvert.ToInt32(Reader[41].ToString());
                    obj.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[42].ToString());
                    obj.SIMainInfo.YearCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[43].ToString());
                    obj.SIMainInfo.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[44].ToString());
                    obj.SIMainInfo.IsBalanced = FS.FrameWork.Function.NConvert.ToBoolean(Reader[45].ToString());
                    obj.SIMainInfo.Memo = Reader[46].ToString();
                    obj.SIMainInfo.TypeCode = Reader[47].ToString();
                    obj.SIMainInfo.OverCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[48].ToString());
                    obj.SIMainInfo.PersonType.ID = Reader[49].ToString();
                    if (!Reader.IsDBNull(50))
                        obj.SIMainInfo.Bka911 = FS.FrameWork.Function.NConvert.ToDateTime(Reader[50].ToString());
                    obj.SIMainInfo.Bka912 = Reader[51].ToString();
                    obj.SIMainInfo.Bka913 = FS.FrameWork.Function.NConvert.ToInt32(Reader[52].ToString());
                    obj.SIMainInfo.Bka914 = Reader[53].ToString();
                    if (!Reader.IsDBNull(54))
                        obj.SIMainInfo.Bka915 = FS.FrameWork.Function.NConvert.ToDateTime(Reader[54].ToString());
                    obj.SIMainInfo.Bka916 = Reader[55].ToString();
                    if (!Reader.IsDBNull(56))
                        obj.SIMainInfo.Bka917 = FS.FrameWork.Function.NConvert.ToDateTime(Reader[56].ToString());
                    obj.SIMainInfo.Bka042 = Reader[57].ToString();
                    obj.SIMainInfo.Aaz267 = Reader[58].ToString();
                    obj.SIMainInfo.Bka825 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[59].ToString());
                    obj.SIMainInfo.Bka826 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[60].ToString());
                    obj.SIMainInfo.Aka151 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[61].ToString());
                    obj.SIMainInfo.Bka838 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[62].ToString());
                    obj.SIMainInfo.Akb067 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[63].ToString());
                    obj.SIMainInfo.Akb066 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[64].ToString());
                    obj.SIMainInfo.Bka821 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[65].ToString());
                    obj.SIMainInfo.Bka839 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[66].ToString());
                    obj.SIMainInfo.Ake039 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[67].ToString());
                    obj.SIMainInfo.Ake035 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[68].ToString());
                    obj.SIMainInfo.Ake026 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[69].ToString());
                    obj.SIMainInfo.Ake029 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[70].ToString());
                    obj.SIMainInfo.Bka841 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[71].ToString());
                    obj.SIMainInfo.Bka842 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[72].ToString());
                    obj.SIMainInfo.Bka840 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[73].ToString());
                    obj.SIMainInfo.Aaa027 = Reader[74].ToString();
                    obj.SIMainInfo.Bka006 = Reader[75].ToString();
                    obj.SIMainInfo.TacCode = Reader[76].ToString();
                    obj.SIMainInfo.Bka438 = Reader[77].ToString();
                }
                Reader.Close();
                return obj;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                Reader.Close();
                return null;
            }
            #endregion
        }

        /// <summary>
        /// 插入医保挂号信息
        /// </summary>
        /// <param name="registerPatientInfo"></param>
        /// <returns></returns>
        public int InsertOutPatientRegInfo(FS.HISFC.Models.Registration.Register patientInfo)
        {
            //获取结算序号
            int nextBalanceNo = this.GetNextBalanceNo(patientInfo.ID, "1");
            string nxtBalanceNoStr = nextBalanceNo.ToString();
            patientInfo.SIMainInfo.BalNo = nxtBalanceNoStr;

            #region sql

            string strSql = @"INSERT INTO FIN_IPR_SIINMAININFO
                                          (INPATIENT_NO, --住院流水号 0
                                           REG_NO, --就医登记号 1
                                           BALANCE_NO, --结算序号 2
                                           CARD_NO, --卡号  3
                                           MCARD_NO, --医疗证号 4
                                           NAME, --姓名 5
                                           SEX_CODE, --性别 6
                                           IDENNO, --身份证号 7
                                           BIRTHDAY, --生日 8
                                           DEPT_CODE, --科室代码 9
                                           DEPT_NAME, --科室名称 10
                                           PAYKIND_CODE, --结算类别 1-自费  2-保险 3-公费在职 4-公费退休 5-公费高干 11
                                           PACT_CODE, --合同代码 12
                                           PACT_NAME, --合同单位名称 13
                                           MDTRT_ID, --就诊ID 14
                                           PSN_NO, --人员编号 15
                                           PSN_NAME, --人员姓名 16
                                           PSN_CERT_TYPE, --人员证件类型 17
                                           CERTNO, --证件号码 18
                                           GEND, --性别 19
                                           BRDY, --生日 20
                                           INSUTYPE, --险种类型 21
                                           PSN_TYPE, --人员类别 22
                                           MDTRT_CERT_TYPE, --就诊凭证类型 23
                                           MED_TYPE, --就诊类型 24
                                           DISE_CODE, --病种编码 25
                                           DISE_NAME, --病种名称 26
                                           VOLA_TYPE, --违规类型 27
                                           VOLA_DSCR, --违规说明 28
                                           OPER_CODE, --操作员 29
                                           OPER_DATE, --操作日期 30
                                           TYPE_CODE -- 1 门诊/2 住院 31 
                                           )
                                  VALUES ('{0}', -- 0 住院流水号
                                          '{1}', -- 1 就医登记号
                                          '{2}', -- 2 结算序号
                                          '{3}', -- 3 卡号 
                                          '{4}', -- 4 医疗证号
                                          '{5}', -- 5 姓名
                                          '{6}', -- 6 性别
                                          '{7}', -- 7 身份证号
                                          to_date('{8}', 'YYYY-MM-DD hh24:mi:ss'),-- 8 生日
                                          '{9}', -- 9 科室代码
                                          '{10}', -- 10 科室名称
                                          '{11}',  -- 11 结算类别 1-自费  2-保险 3-公费在职 4-公费退休 5-公费高干
                                          '{12}', -- 12 合同代码
                                          '{13}', -- 13 合同单位名称
                                          '{14}', -- 14 就诊ID
                                          '{15}', -- 15 人员编号
                                          '{16}', -- 16 人员姓名
                                          '{17}', -- 17 人员证件类型
                                          '{18}', -- 18 证件号码
                                          '{19}', -- 19 性别
                                          to_date('{20}', 'YYYY-MM-DD hh24:mi:ss'),-- 20 生日
                                          '{21}', -- 21 险种类型
                                          '{22}', -- 22 人员类别
                                          '{23}', -- 23 就诊凭证类型
                                          '{24}', -- 24 就诊类型
                                          '{25}', -- 25 病种编码
                                          '{26}', -- 26 病种名称
                                          '{27}', -- 27 违规类型
                                          '{28}', -- 28 违规说明
                                          '{29}', -- 29 操作员
                                          to_date('{30}', 'YYYY-MM-DD hh24:mi:ss'),-- 17 操作日期
                                          '1'  ) -- 31 患者类型 ";
            #endregion

            try
            {
                strSql = string.Format(strSql,
                    patientInfo.ID,//住院流水号/门诊流水号
                    patientInfo.SIMainInfo.Mdtrt_id,//就医登记号
                    patientInfo.SIMainInfo.BalNo, //结算序号
                    patientInfo.PID.CardNO,//卡号
                    patientInfo.SIMainInfo.Psn_no,//医疗证号
                    patientInfo.Name,//姓名
                    patientInfo.Sex.ID,//性别
                    patientInfo.IDCard, //身份证号\
                    patientInfo.SIMainInfo.Brdy.ToString(), //生日
                    patientInfo.DoctorInfo.Templet.Dept.ID,//科室代码
                    patientInfo.DoctorInfo.Templet.Dept.Name,//科室名称
                    patientInfo.Pact.PayKind.ID,//结算类别 1-自费  2-保险 3-公费在职 4-公费退休 5-公费高干
                    patientInfo.Pact.ID,//合同代码
                    patientInfo.Pact.Name,//合同单位名称
                    patientInfo.SIMainInfo.Mdtrt_id,//就诊ID
                    patientInfo.SIMainInfo.Psn_no,//人员编号
                    patientInfo.SIMainInfo.Psn_name, //人员姓名
                    patientInfo.SIMainInfo.Psn_cert_type, //人员证件类型
                    patientInfo.SIMainInfo.Certno, //证件号码
                    patientInfo.SIMainInfo.Gend, //性别
                    patientInfo.SIMainInfo.Brdy.ToString("yyyy-MM-dd HH:mm:ss"), //生日
                    patientInfo.SIMainInfo.Insutype, //险种类型
                    patientInfo.SIMainInfo.Psn_type, //人员类别
                    patientInfo.SIMainInfo.Mdtrt_cert_type, //就诊凭证类型
                    patientInfo.SIMainInfo.Med_type, //就诊类型
                    patientInfo.SIMainInfo.Dise_code, //病种编码
                    patientInfo.SIMainInfo.Dise_name, //病种名称
                    patientInfo.SIMainInfo.Vola_type,//违规类型
                    patientInfo.SIMainInfo.Vola_dscr,//违规说明
                    this.Operator.ID,//操作员
                    this.GetDateTimeFromSysDateTime().ToString()//操作日期
                    );
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 作废医保挂号信息
        /// </summary>
        /// <param name="regNo">医保就诊登记号</param>
        /// <returns></returns>
        public int CancelOutPatientRegInfo(string regNo)
        {
            string strSql = "";

            strSql = @"UPDATE FIN_IPR_SIINMAININFO
                          SET VALID_FLAG = '0',
                              BALANCE_STATE = '0',
                              OPER_CODE = '{0}',
                              OPER_DATE = to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                        WHERE REG_NO = '{2}'
                          AND TYPE_CODE = '1'";
            try
            {
                strSql = string.Format(strSql,
                    this.Operator.ID,
                    this.GetDateTimeFromSysDateTime(),
                    regNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新医保结算信息
        /// </summary>
        /// <returns></returns>
        public int UpdateSiMainInfoOutBalanceInfo(FS.HISFC.Models.Registration.Register patientInfo)
        {
            #region sql
            string strSql = @"
                            UPDATE FIN_IPR_SIINMAININFO f
                               SET f.MDTRT_ID  = '{1}', --就诊ID
                                   f.SETL_ID     =  '{2}', --结算ID
                                   f.PSN_NO     =  '{3}', --人员编号
                                   f.PSN_NAME =  '{4}', --人员姓名
                                   f.PSN_CERT_TYPE = '{5}', --人员证件类型
                                   f.CERTNO =  '{6}', --证件号码
                                   f.GEND =  '{7}', --性别
                                   f.NATY =  '{8}', --民族
                                   f.BRDY =  to_date('{9}', 'YYYY-MM-DD hh24:mi:ss'), --出生日期
                                   f.AGE =  '{10}', --年龄
                                   f.INSUTYPE =  '{11}', --险种类型
                                   f.PSN_TYPE = '{12}', --人员类别
                                   f.CVLSERV_FLAG =  '{13}', --公务员标志
                                   f.SETL_TIME = to_date('{14}', 'YYYY-MM-DD hh24:mi:ss'), --结算时间
                                   f.PSN_SETLWAY =  '{15}', --个人结算方式
                                   f.MDTRT_CERT_TYPE =  '{16}', --就诊凭证类型
                                   f.MED_TYPE =  '{17}', --医疗类别
                                   f.MEDFEE_SUMAMT =  '{18}', --医疗费总额
                                   f.OWNPAY_AMT =  {19}, --全自费金额
                                   f.OVERLMT_SELFPAY =  {20}, --超限价自费费用
                                   f.PRESELFPAY_AMT =  {21}, --先行自付金额
                                   f.INSCP_SCP_AMT = {22}, --符合范围金额
                                   f.MED_SUMFEE =  {23}, --医保认可费用总额
                                   f.ACT_PAY_DEDC =  {24}, --实际支付起付线
                                   f.HIFP_PAY = {25}, --基本医疗保险统筹基金支出
                                   f.POOL_PROP_SELFPAY = {26}, --基本医疗保险统筹基金支付比例
                                   f.CVLSERV_PAY = {27}, --公务员医疗补助资金支出
                                   f.HIFES_PAY = {28}, --企业补充医疗保险基金支出
                                   f.HIFMI_PAY = {29}, --居民大病保险资金支出
                                   f.HIFOB_PAY = {30}, --职工大额医疗费用补助基金支出
                                   f.HIFDM_PAY =  {31}, --伤残人员医疗保障基金支出
                                   f.MAF_PAY = {32}, --医疗救助基金支出
                                   f.OTH_PAY = {33}, --其他基金支出
                                   f.FUND_PAY_SUMAMT = {34}, --基金支付总额
                                   f.HOSP_PART_AMT =  {35}, --医院负担金额
                                   f.PSN_PART_AM = {36}, --个人负担总金额
                                   f.ACCT_PAY = {37}, --个人账户支出
                                   f.CASH_PAYAMT = {38}, --现金支付金额
                                   f.ACCT_MULAID_PAY = {39}, --账户共济支付金额
                                   f.BALC = {40}, --个人账户支出后余额
                                   f.CLR_OPTINS =  '{41}', --清算经办机构
                                   f.CLR_WAY =  '{42}', --清算方式
                                   f.CLR_TYPE =  '{43}', --清算类别
                                   f.MEDINS_SETL_ID =  '{44}', --医药机构结算ID
                                   f.VOLA_TYPE = '{45}', --违规类型
                                   f.VOLA_DSCR = '{46}', --违规说明
                                   f.VALID_FLAG = '1',   --是否有效（0 无效 1有效）
                                   f.BALANCE_STATE = '1', --是否结算（0 未结算 1 结算） 
                                   f.INVOICE_NO = '{47}', --发票号
                                   f.BALANCE_DATE = to_date('{48}', 'YYYY-MM-DD hh24:mi:ss') --结算时间
                             where f.INPATIENT_NO = '{0}'
                               and f.REG_NO = '{1}' ";
            #endregion

            try
            {
                strSql = string.Format(strSql, patientInfo.ID, //患者挂号流水号
                    patientInfo.SIMainInfo.Mdtrt_id,//就诊id
                    patientInfo.SIMainInfo.Setl_id,//结算id
                    patientInfo.SIMainInfo.Psn_no,//人员编号
                    patientInfo.SIMainInfo.Psn_name,//人员姓名
                    patientInfo.SIMainInfo.Psn_cert_type,//人员证件类型
                    patientInfo.SIMainInfo.Certno,//证件号码
                    patientInfo.SIMainInfo.Gend,//性别
                    patientInfo.SIMainInfo.Naty,//民族
                    patientInfo.SIMainInfo.Brdy,//出生日期
                    patientInfo.SIMainInfo.Age,//年龄
                    patientInfo.SIMainInfo.Insutype,//险种类型
                    patientInfo.SIMainInfo.Psn_type,//人员类别
                    patientInfo.SIMainInfo.Cvlserv_flag,//公务员标志
                    patientInfo.SIMainInfo.Setl_time,//结算时间
                    patientInfo.SIMainInfo.Psn_setlway,//个人结算方式
                    patientInfo.SIMainInfo.Mdtrt_cert_type,//就诊凭证类型
                    patientInfo.SIMainInfo.Med_type,//医疗类别
                    patientInfo.SIMainInfo.Medfee_sumamt,//医疗费总额
                    patientInfo.SIMainInfo.Ownpay_amt,//全自费金额Ownpay_amt
                    patientInfo.SIMainInfo.Overlmt_selfpay,//超限价自费费用
                    patientInfo.SIMainInfo.Preselfpay_amt,//先行自付金额
                    patientInfo.SIMainInfo.Inscp_scp_amt,//符合范围金额
                    patientInfo.SIMainInfo.Med_sumfee,//医保认可费用总额
                    patientInfo.SIMainInfo.Act_pay_dedc,//实际支付起付线
                    patientInfo.SIMainInfo.Hifp_pay,//基本医疗保险统筹基金支出
                    patientInfo.SIMainInfo.Pool_prop_selfpay,//基本医疗保险统筹基金支付比例
                    patientInfo.SIMainInfo.Cvlserv_pay,//公务员医疗补助资金支出
                    patientInfo.SIMainInfo.Hifes_pay,//企业补充医疗保险基金支出
                    patientInfo.SIMainInfo.Hifmi_pay,//居民大病保险资金支出
                    patientInfo.SIMainInfo.Hifob_pay,//职工大额医疗费用补助基金支出
                    patientInfo.SIMainInfo.Hifdm_pay,//伤残人员医疗保障基金支出
                    patientInfo.SIMainInfo.Maf_pay,//医疗救助基金支出
                    patientInfo.SIMainInfo.Oth_pay,//其他基金支出
                    patientInfo.SIMainInfo.Fund_pay_sumamt,//基金支付总额
                    patientInfo.SIMainInfo.Hosp_part_amt,//医院负担金额
                    patientInfo.SIMainInfo.Psn_part_am,//个人负担总金额
                    patientInfo.SIMainInfo.Acct_pay,//个人账户支出
                    patientInfo.SIMainInfo.Cash_payamt,//现金支付金额
                    patientInfo.SIMainInfo.Acct_mulaid_pay,//账户共济支付金额
                    patientInfo.SIMainInfo.Balc,//个人账户支出后余额
                    patientInfo.SIMainInfo.Clr_optins,//清算经办机构
                    patientInfo.SIMainInfo.Clr_way,//清算方式
                    patientInfo.SIMainInfo.Clr_type,//清算类别
                    patientInfo.SIMainInfo.Medins_setl_id,//医药机构结算id
                    patientInfo.SIMainInfo.Vola_type,//违规类型
                    patientInfo.SIMainInfo.Vola_dscr,//违规说明
                    patientInfo.SIMainInfo.InvoiceNo, //结算发票号
                    patientInfo.SIMainInfo.Setl_time //结算时间
                );

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新医保返回的费用明细结果
        /// </summary>
        /// <returns></returns>
        public int UpdateOutBalanceSIFeeDetail(FS.HISFC.Models.Registration.Register patientInfo)
        {
            #region sql
            string strSql = @"update gzsi_feedetail t
                                 set t.setl_id = '{1}'
                               where t.mdtrt_id = '{0}'
                                 and t.type_code = '1'";
            #endregion

            try
            {
                strSql = string.Format(strSql,
                    patientInfo.SIMainInfo.Mdtrt_id,
                    patientInfo.SIMainInfo.Setl_id
                );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 插入医保返回的费用明细结果
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="feeDetailList"></param>
        /// <returns></returns>
        public int InsertOutBalanceSIFeeDetail(FS.HISFC.Models.Registration.Register patientInfo, List<API.GZSI.Models.Response.ResponseGzsiModel2204.Result> feeDetailList)
        {
            int rtn = 1;
            string sql = @"insert into gzsi_feedetail 
                                       (mdtrt_id,
                                       setl_id,
                                       feedetl_sn,
                                       det_item_fee_sumamt,
                                       cnt,
                                       pric,
                                       pric_uplmt_amt,
                                       selfpay_prop,
                                       defray_type,
                                       fulamt_ownpay_amt,
                                       overlmt_amt,
                                       preselfpay_amt,
                                       inscp_scp_amt,
                                       chrgitm_lv,
                                       med_chrgitm_type,
                                       bas_medn_flag,
                                       hi_nego_drug_flag,
                                       chld_medc_flag,
                                       list_sp_item_flag,
                                       lmt_used_flag,
                                       drt_reim_flag,
                                       memo,
                                       type_code)
                                      values
                                      ('{0}',
                                       '{1}',
                                       '{2}',
                                       {3},
                                       {4},
                                       {5},
                                       {6},
                                       {7},
                                       '{8}',
                                       {9},
                                       {10},
                                       {11},
                                       {12},
                                       '{13}',
                                       '{14}',
                                       '{15}',
                                       '{16}',
                                       '{17}',
                                       '{18}',
                                       '{19}',
                                       '{20}',
                                       '{21}',
                                       '{22}')";

            foreach (API.GZSI.Models.Response.ResponseGzsiModel2204.Result result in feeDetailList)
            {
                try
                {
                    string strSQL = string.Format(sql, patientInfo.SIMainInfo.Mdtrt_id,
                        "",
                        result.feedetl_sn,
                        FS.FrameWork.Function.NConvert.ToDecimal(result.det_item_fee_sumamt).ToString(),
                        FS.FrameWork.Function.NConvert.ToDecimal(result.cnt).ToString(),
                        FS.FrameWork.Function.NConvert.ToDecimal(result.pric).ToString(),
                        FS.FrameWork.Function.NConvert.ToDecimal(result.pric_uplmt_amt).ToString(),
                        FS.FrameWork.Function.NConvert.ToDecimal(result.selfpay_prop).ToString(),
                        "",
                        FS.FrameWork.Function.NConvert.ToDecimal(result.fulamt_ownpay_amt).ToString(),
                        FS.FrameWork.Function.NConvert.ToDecimal(result.overlmt_amt).ToString(),
                        FS.FrameWork.Function.NConvert.ToDecimal(result.preselfpay_amt).ToString(),
                        FS.FrameWork.Function.NConvert.ToDecimal(result.inscp_scp_amt).ToString(),
                        result.chrgitm_lv,
                        result.med_chrgitm_type,
                        result.bas_medn_flag,
                        result.hi_nego_drug_flag,
                        result.chld_medc_flag,
                        result.list_sp_item_flag,
                        result.lmt_used_flag,
                        result.drt_reim_flag,
                        result.memo,
                        "1");

                    rtn = this.ExecNoQuery(strSQL);

                    if (rtn <= 0)
                    {
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 删除医保返回的费用明细结果
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int DeleteOutBalanceSIFeeDetail(FS.HISFC.Models.Registration.Register patientInfo)
        {
            try
            {
                string sql = @" delete from gzsi_feedetail where mdtrt_id = '{0}' and type_code = '1'";
                sql = string.Format(sql, patientInfo.SIMainInfo.Mdtrt_id);
                return this.ExecNoQuery(sql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 插入门诊医保收费明细
        /// </summary>
        /// <param name="feeListForUpload"></param>
        /// <returns></returns>
        public int InsertOutBalanceFeeDetail(FS.HISFC.Models.Registration.Register r, ArrayList feeListForUpload)
        {
            int ret = 0;
            DateTime dateNow = this.GetDateTimeFromSysDateTime();
            ret = this.InsertMZXMData(r, feeListForUpload, dateNow);

            if (ret < 0)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 插入多条费用明细
        /// </summary>
        /// <param name="r">挂号信息，包括医保患者基本信息</param>
        /// <param name="feeDetails">费用明细</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int InsertMZXMData(FS.HISFC.Models.Registration.Register r, ArrayList feeDetails, DateTime operDate)
        {
            int iReturn = 0;
            int uploadRows = 0;

            try
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
                {
                    iReturn = DeleteMZXMData(r, f);
                    if (iReturn < 0)
                    {
                        this.Err += "删除历史费用明细失败!";
                        return -1;
                    }

                    iReturn = InsertMZXMData(r, f, operDate);
                    if (iReturn <= 0)
                    {
                        this.Err += "插入明细失败!";
                        return -1;
                    }

                    uploadRows += iReturn;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return uploadRows;
        }

        /// <summary>
        /// 插入单条费用明细
        /// </summary>
        /// <param name="r">挂号信息，包括医保患者基本信息</param>
        /// <param name="f">费用明细</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int InsertMZXMData(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f, DateTime operDate)
        {
            if (string.IsNullOrEmpty(r.SIMainInfo.HosNo))
            {
                r.SIMainInfo.HosNo = string.IsNullOrEmpty(HosCode) ? r.SIMainInfo.RegNo.Substring(0, 6) : HosCode;
            }

            #region 插入语句

            string strSQL = @"INSERT INTO GZSI_HIS_MZXM     --广州医保费用明细信息表
                                      ( JYDJH,   --就医登记号
                                        YYBH,   --医院编号
                                        GMSFHM,   --公民身份证号
                                        ZYH,   --住院号/门诊号
                                        RYRQ,   --挂号/入院时间
                                        FYRQ,   --收费时间
                                        XMXH,   --项目序号
                                        XMBH,   --医院的项目编号
                                        XMMC,   --医院的项目名称
                                        FLDM,   --分类代码
                                        YPGG,   --规格
                                        YPJX,   --剂型
                                        JG,   --单价
                                        MCYL,   --数量
                                        JE,   --金额
                                        BZ1,   --备注1，存放记录产生时间
                                        BZ2,   --备注2
                                        BZ3,   --备注3,存放费用明细读入时有效性检查的处理结果代码
                                        DRBZ,   --读入标志
                                        YPLY,   --1-国产 2-进口 3-合资
                                        CLINIC_CODE,   --门诊就诊流水号
                                        CARD_NO,   --门诊号
                                        OPER_CODE,   --操作员
                                        OPER_DATE,   --操作时间
                                        INVOICE_NO,   --发票号
                                        FYPC,   --费用批次
                                        fee_code,  --his费用类别
                                        JSID, --结算编码
                                        ITEM_CODE, --HIS编码,
                                        RECIPE_NO, --处方号,
                                        SEQUENCE_NO --处方内流水号
                                        ) 
                                        VALUES
                                        (
                                        '{0}',   --就医登记号
                                        '{1}',   --医院编号
                                        '{2}',   --公民身份证号
                                        '{3}',   --住院号/门诊号
                                        TO_DATE('{4}','YYYY-MM-DD HH24:MI:SS'),   --挂号/入院时间
                                        TO_DATE('{5}','YYYY-MM-DD HH24:MI:SS'),   --收费时间
                                        '{6}',   --项目序号
                                        '{7}',   --医院的项目编号
                                        '{8}',   --医院的项目名称
                                        '{9}',   --分类代码
                                        '{10}',   --规格
                                        '{11}',   --剂型
                                        '{12}',   --单价
                                        '{13}',   --数量
                                        '{14}',   --金额
                                        '{15}',   --备注1，存放记录产生时间
                                        '{16}',   --备注2
                                        '{17}',   --备注3,存放费用明细读入时有效性检查的处理结果代码
                                        '{18}',   --读入标志
                                        '{19}',   --1-国产   2-进口3-合资
                                        '{20}',   --门诊就诊流水号
                                        '{21}',   --门诊号
                                        '{22}',   --操作员
                                        sysdate,   --操作时间
                                        '{23}',   --发票号
                                        '{24}',   --费用批次
                                        '{25}',  --HIS费用类别
                                        '{26}',  --结算序号
                                        '{27}',  --HIS编码
                                        '{28}',  --HIS处方号
                                        {29})  --处方内流水号 ";
            #endregion

            string name = "";
            name = f.Item.Name;
            if (name.Length > 20)
            {
                name = name.Substring(0, 20);
            }
            try
            {
                decimal price = f.SIPrice; //this.GetPrice(f.Item);
                decimal totCost = f.SIft.OwnCost + f.SIft.PubCost + f.SIft.PayCost;

                strSQL = string.Format(strSQL,
                    r.SIMainInfo.Mdtrt_id,
                    r.SIMainInfo.HosNo,
                    r.IDCard,
                    r.PID.CardNO,
                    r.DoctorInfo.SeeDate.ToString(),
                    operDate.ToString(),
                    f.RecipeNO + f.SequenceNO,//f.Order.SequenceNO.ToString(),
                    f.Item.UserCode,
                    name,
                    "0",
                    f.Item.Specs,
                    r.MainDiagnose, //诊断
                    FS.FrameWork.Public.String.FormatNumber(price / f.Item.PackQty, 4),
                    f.Item.Qty,                 //总量Amount
                    totCost,//(f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost).ToString(),
                    operDate.ToString(),
                    "",
                    "",
                    "0",
                    "",
                    r.ID,
                    r.PID.CardNO,
                    this.Operator.ID,
                    r.SIMainInfo.InvoiceNo,
                    r.SIMainInfo.BalNo,
                    f.Item.MinFee.ID,
                    r.ID + r.SIMainInfo.BalNo,
                    f.Item.ID,
                    f.RecipeNO,
                    f.SequenceNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 删除单条费用明细
        /// </summary>
        /// <param name="r">挂号信息，包括医保患者基本信息</param>
        /// <param name="f">费用明细</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int DeleteMZXMData(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            if (string.IsNullOrEmpty(r.SIMainInfo.HosNo))
            {
                r.SIMainInfo.HosNo = string.IsNullOrEmpty(HosCode) ? r.SIMainInfo.RegNo.Substring(0, 6) : HosCode;
            }

            string strSQL = "delete from gzsi_his_mzxm t where t.clinic_code='{0}' and t.jydjh='{1}' and t.xmbh='{2}' and t.xmxh='{3}' ";
            try
            {
                strSQL = string.Format(strSQL,
                    r.ID,
                    r.SIMainInfo.RegNo,
                    f.Item.UserCode,
                    f.RecipeNO + f.SequenceNO
                    );
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 插入门诊医保结算基金分项
        /// </summary>
        /// <param name="r"></param>
        /// <param name="setlDetailList"></param>
        /// <returns></returns>
        public int InsertOutBalanceSetldetail(FS.HISFC.Models.Registration.Register r, List<Models.Setldetail> setlDetailList)
        {
            string sqlStr = @"INSERT INTO GZSI_SETLDETAIL 
                                          (MDTRT_ID,  --就诊ID
                                           SETL_ID,  --结算ID
                                           FUND_PAY_TYPE, --基金支付类型
                                           FUN_PAY_TYPE_NAME, --基金支付名称
                                           INSCP_SCP_AMT, --符合范围金额
                                           CRT_PAYB_LMT_AMT, --本次可支付限额金额
                                           FUND_PAYAMT, --基金支付金额
                                           SETL_PROC_INFO, --结算过程信息
                                           TYPE_CODE) --就诊类型
                                    VALUES ('{0}',
                                           '{1}',
                                           '{2}',
                                           '{3}',
                                           {4},
                                           {5},
                                           {6},
                                           '{7}',
                                           '1')";

            try
            {
                foreach (Models.Setldetail setl in setlDetailList)
                {
                    string sql = string.Format(sqlStr, r.SIMainInfo.Mdtrt_id,  //就诊ID
                                                      r.SIMainInfo.Setl_id, //结算ID
                                                      setl.fund_pay_type, //基金支付类型
                                                      setl.fund_pay_type_name, //基金支付名称
                                                      setl.inscp_scp_amt,//符合范围金额
                                                      setl.crt_payb_lmt_amt, //本次可支付限额金额
                                                      setl.fund_payamt, //基金支付金额
                                                      setl.setl_proc_info); //结算过程信息

                    int ret = this.ExecNoQuery(sql);

                    if (ret < 1)
                    {
                        this.ErrCode = "1";
                        this.Err = "插入结算基金分项出错";
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 插入门诊医保结算违规费用明细
        /// </summary>
        /// <param name="r"></param>
        /// <param name="setlDetailList"></param>
        /// <returns></returns>
        public int InsertOutBalanceVoladetail(FS.HISFC.Models.Registration.Register r, List<Models.Voladetail> volaDetailList)
        {
            string sqlStr = @"INSERT INTO GZSI_VOLADETAIL
                                          (MDTRT_ID,  --就诊ID
                                           SETL_ID, --结算ID
                                           MEDINS_LIST_CODG, --医疗机构目录编码
                                           MEDINS_LIST_NAME, --医疗机构目录名称
                                           MED_LIST_CODG, --医疗目录编码
                                           VOLA_TYPE, --违规类型
                                           VOLA_DSCR, --违规说明
                                           TYPE_CODE) --就诊类型
                                   VALUES ('{0}',
                                           '{1}',
                                           '{2}',
                                           '{3}',
                                           '{4}',
                                           '{5}',
                                           '{6}',
                                           '1') ";

            try
            {
                foreach (Models.Voladetail vola in volaDetailList)
                {
                    string sql = string.Format(sqlStr, r.SIMainInfo.Mdtrt_id,  //就诊ID
                                                      r.SIMainInfo.Setl_id, //结算ID
                                                      vola.medins_list_codg, //医疗机构目录编码
                                                      vola.medins_list_name, //医疗机构目录名称
                                                      vola.med_list_codg,//医疗目录编码
                                                      vola.vola_type, //违规类型
                                                      vola.vola_dscr); //违规说明

                    int ret = this.ExecNoQuery(sql);

                    if (ret < 1)
                    {
                        this.ErrCode = "1";
                        this.Err = "插入结算违规费用明细出错";
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 获取门诊诊断信息
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public List<Models.Request.RequestGzsiModel2203.Diseinfo> DiagnoseList(string inpatientNo)
        {
            List<Models.Request.RequestGzsiModel2203.Diseinfo> obj = new List<Models.Request.RequestGzsiModel2203.Diseinfo>();
            //// {5ABD2A6D-AABB-49C5-9ADC-CE57C646B606}
            string strSql = @"  select t.diag_kind diag_type,
                                      t.happen_no diag_srt_no,
                                     (select distinct a.old_code  from MET_COM_ICDCOMPARE10 a where a.new_code   =t.icd_code and a.valid_state = '1' )  diag_code,
                                     (select distinct a.old_name  from MET_COM_ICDCOMPARE10 a where a.new_code   =t.icd_code and a.valid_state = '1' )  diag_name,
                                      r.DEPT_NAME diag_dept,
                                       --t.doct_code dise_dor_no,
                                      (select nvl(e.user_code,e.empl_code) from com_employee e where e.empl_code =r.doct_code) as dise_dor_no,
                                      t.doct_name dise_dor_name,
                                      t.diag_date diag_time,
                                      t.valid_flag vali_flag,
                                      decode(t.diag_kind,'1','1','0') maindiag_flag
                                 from met_cas_diagnose t 
                                   left join fin_opr_register r on t.inpatient_no=r.CLINIC_CODE
                                where t.inpatient_no = '{0}'
                                  and t.valid_flag = '1'
                                  and t.icd_code != 'MS999' --排除描述诊断";// {AF420A61-59D9-4123-97AB-21E93B137744}
            strSql = string.Format(strSql, inpatientNo);

            bool existMain = false;

            try
            {
                if (ExecQuery(strSql) == -1)
                {
                    return null;
                }
                while (Reader.Read())
                {
                    Models.Request.RequestGzsiModel2203.Diseinfo diseinfo = new Models.Request.RequestGzsiModel2203.Diseinfo();
                    diseinfo.diag_type = Reader[0].ToString();//诊断类别 
                    diseinfo.diag_srt_no = Reader[1].ToString();//诊断排序号 
                    diseinfo.diag_code = Reader[2].ToString();//诊断代码 
                    diseinfo.diag_name = Reader[3].ToString();//诊断名称 
                    diseinfo.diag_dept = Reader[4].ToString();//诊断科室 
                    diseinfo.dise_dor_no = Reader[5].ToString();//诊断医生编码 
                    diseinfo.dise_dor_name = Reader[6].ToString();//诊断医生姓名 
                    diseinfo.diag_time = Reader[7].ToString();//诊断时间 
                    diseinfo.vali_flag = Reader[8].ToString();//有效标志 

                    string mainFlag = Reader[9].ToString();//主诊断标志

                    //只能存在1个主诊断
                    if (mainFlag == "1" && !existMain)
                    {
                        diseinfo.maindiag_flag = mainFlag;
                        existMain = true;
                    }
                    else
                    {
                        diseinfo.maindiag_flag = "0";
                    }

                    obj.Add(diseinfo);
                }
                Reader.Close();
            } //抛出错误
            catch (Exception ex)
            {
                Err = "取诊断信息出错！" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
            return obj;
        }

        /// <summary>
        /// 获得单条对照信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="objCompare"></param>
        /// <returns></returns>
        public int GetCompareSingleItem(string pactCode, string itemCode, ref FS.HISFC.Models.SIInterface.Compare objCompare)
        {
            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.GetCompareSingleItem.Select.1", ref strSql) == -1)
                return -1;

            try
            {
                strSql = string.Format(strSql, pactCode, itemCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    FS.HISFC.Models.SIInterface.Compare obj = new FS.HISFC.Models.SIInterface.Compare();

                    obj.CenterItem.PactCode = Reader[0].ToString();
                    obj.HisCode = Reader[1].ToString();
                    obj.CenterItem.ID = Reader[2].ToString();
                    obj.CenterItem.SysClass = Reader[3].ToString();
                    obj.CenterItem.Name = Reader[4].ToString();
                    obj.CenterItem.EnglishName = Reader[5].ToString();
                    obj.CenterItem.Specs = Reader[6].ToString();
                    obj.CenterItem.DoseCode = Reader[7].ToString();
                    obj.CenterItem.SpellCode = Reader[8].ToString();
                    obj.CenterItem.FeeCode = Reader[9].ToString();
                    obj.CenterItem.ItemType = Reader[10].ToString();
                    obj.CenterItem.ItemGrade = Reader[11].ToString();
                    obj.CenterItem.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());
                    obj.CenterItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[13].ToString());
                    obj.CenterItem.Memo = Reader[14].ToString();
                    obj.SpellCode.SpellCode = Reader[15].ToString();
                    obj.SpellCode.WBCode = Reader[16].ToString();
                    obj.SpellCode.UserCode = Reader[17].ToString();
                    obj.Specs = Reader[18].ToString();
                    obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[19].ToString());
                    obj.DoseCode = Reader[20].ToString();
                    obj.CenterItem.OperCode = Reader[21].ToString();
                    obj.CenterItem.OperDate = Convert.ToDateTime(Reader[22].ToString());
                    obj.Name = Reader[23].ToString();
                    obj.RegularName = Reader[24].ToString();

                    al.Add(obj);
                }

                Reader.Close();

                if (al.Count > 0)
                {
                    objCompare = (FS.HISFC.Models.SIInterface.Compare)al[0];
                    return 0;
                }
                else
                {
                    return -2;
                }
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 查询ICD10
        /// </summary>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject QueryICDByCode(string id)
        {
            string sqlStr = "select icd_code, icd_name from met_com_icd10 p where p.icd_code='{0}'";
            sqlStr = string.Format(sqlStr, id);
            try
            {
                this.ExecQuery(sqlStr);
                FS.FrameWork.Models.NeuObject Obj = new FS.FrameWork.Models.NeuObject();
                while (this.Reader.Read())
                {
                    Obj.ID = Reader[0].ToString();
                    Obj.Name = Reader[1].ToString();
                    break;
                }
                return Obj;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #region 屏蔽
        ///// <summary>
        ///// 插入门诊收费信息
        ///// </summary>
        ///// <param name="registerPatientInfo"></param>
        ///// <returns></returns>
        //public int InsertOutPatientBalance(FS.HISFC.Models.Registration.Register patientInfo)
        //{
        //    int nextBalanceNo = this.GetNextBalanceNo(patientInfo.ID, "1");
        //    string nxtBalanceNoStr = nextBalanceNo.ToString();
        //    patientInfo.SIMainInfo.BalNo = nxtBalanceNoStr;
        //    string TacCode = "0";
        //    if (string.IsNullOrEmpty(patientInfo.SIMainInfo.TacCode))
        //    {
        //        TacCode = "0";
        //    }
        //    else
        //    {
        //        TacCode = patientInfo.SIMainInfo.TacCode;
        //    }
        //    #region sql
        //    string strSql = @"insert into FIN_IPR_SIINMAININFO
        //                              (MDTRT_ID, --就诊ID
        //                               SETL_ID, --结算ID
        //                               PSN_NO, --人员编号
        //                               PSN_NAME, --人员姓名
        //                               PSN_CERT_TYPE, --人员证件类型
        //                               CERTNO, --证件号码
        //                               GEND, --性别
        //                               NATY, --民族
        //                               BRDY, --出生日期
        //                               AGE, --年龄
        //                               INSUTYPE, --险种类型
        //                               PSN_TYPE, --人员类别
        //                               CVLSERV_FLAG, --公务员标志
        //                               SETL_TIME, --结算时间
        //                               PSN_SETLWAY, --个人结算方式
        //                               MDTRT_CERT_TYPE, --就诊凭证类型
        //                               MED_TYPE, --医疗类别
        //                               MEDFEE_SUMAMT, --医疗费总额
        //                               OWNPAY_AMT, --全自费金额
        //                               OVERLMT_SELFPAY, --超限价自费费用
        //                               PRESELFPAY_AMT, --先行自付金额
        //                               INSCP_SCP_AMT, --符合范围金额
        //                               MED_SUMFEE, --医保认可费用总额
        //                               ACT_PAY_DEDC, --实际支付起付线
        //                               HIFP_PAY, --基本医疗保险统筹基金支出
        //                               POOL_PROP_SELFPAY, --基本医疗保险统筹基金支付比例
        //                               CVLSERV_PAY, --公务员医疗补助资金支出
        //                               HIFES_PAY, --企业补充医疗保险基金支出
        //                               HIFMI_PAY, --居民大病保险资金支出
        //                               HIFOB_PAY, --职工大额医疗费用补助基金支出
        //                               HIFDM_PAY, --伤残人员医疗保障基金支出
        //                               MAF_PAY, --医疗救助基金支出
        //                               OTH_PAY, --其他基金支出
        //                               FUND_PAY_SUMAMT, --基金支付总额
        //                               HOSP_PART_AMT, --医院负担金额
        //                               PSN_PART_AM, --个人负担总金额
        //                               ACCT_PAY, --个人账户支出
        //                               CASH_PAYAMT, --现金支付金额
        //                               ACCT_MULAID_PAY, --账户共济支付金额
        //                               BALC, --个人账户支出后余额
        //                               CLR_OPTINS, --清算经办机构
        //                               CLR_WAY, --清算方式
        //                               CLR_TYPE, --清算类别
        //                               MEDINS_SETL_ID, --医药机构结算ID
        //                               VOLA_TYPE, --违规类型
        //                               VOLA_DSCR,--违规说明
        //                               VALID_FLAG--是否有效（0 无效 1有效）
        //                               )
        //                            VALUES
        //                              ('{0}', --就诊ID
        //                               '{1}', --结算ID
        //                               '{2}', --人员编号
        //                               '{3}', --人员姓名
        //                               '{4}', --人员证件类型
        //                               '{5}', --证件号码
        //                               '{6}', --性别
        //                               '{7}', --民族
        //                               to_date('{8}', 'YYYY-MM-DD hh24:mi:ss'), --出生日期
        //                               '{9}', --年龄
        //                               '{10}', --险种类型
        //                               '{11}', --人员类别
        //                               '{12}', --公务员标志
        //                               to_date('{13}', 'YYYY-MM-DD hh24:mi:ss'), --结算时间
        //                               '{14}', --个人结算方式
        //                               '{15}', --就诊凭证类型
        //                               '{16}', --医疗类别
        //                               {17}, --医疗费总额
        //                               {18}, --全自费金额
        //                               {19}, --超限价自费费用
        //                               {20}, --先行自付金额
        //                               {21}, --符合范围金额
        //                               {22}, --医保认可费用总额
        //                               {23}, --实际支付起付线
        //                               {24}, --基本医疗保险统筹基金支出
        //                               {25}, --基本医疗保险统筹基金支付比例
        //                               {26}, --公务员医疗补助资金支出
        //                               {27}, --企业补充医疗保险基金支出
        //                               {28}, --居民大病保险资金支出
        //                               {29}, --职工大额医疗费用补助基金支出
        //                               {30}, --伤残人员医疗保障基金支出
        //                               {31}, --医疗救助基金支出
        //                               {32}, --其他基金支出
        //                               {33}, --基金支付总额
        //                               {34}, --医院负担金额
        //                               {35}, --个人负担总金额
        //                               {36}, --个人账户支出
        //                               {37}, --现金支付金额
        //                               {38}, --账户共济支付金额
        //                               {39}, --个人账户支出后余额
        //                               '{40}', --清算经办机构
        //                               '{41}', --清算方式
        //                               '{42}', --清算类别
        //                               '{43}', --医药机构结算ID
        //                               '{44}', --违规类型
        //                               '{45}', --违规说明
        //                               '1'   --是否有效（0 无效 1有效） 
        //                               )";
        //    strSql = string.Format(strSql,
        //        patientInfo.SIMainInfo.Mdtrt_id,//就诊id
        //        patientInfo.SIMainInfo.Setl_id,//结算id
        //        patientInfo.SIMainInfo.Psn_no,//人员编号
        //        patientInfo.SIMainInfo.Psn_name,//人员姓名
        //        patientInfo.SIMainInfo.Psn_cert_type,//人员证件类型
        //        patientInfo.SIMainInfo.Certno,//证件号码
        //        patientInfo.SIMainInfo.Gend,//性别
        //        patientInfo.SIMainInfo.Naty,//民族
        //        patientInfo.SIMainInfo.Brdy,//出生日期
        //        patientInfo.SIMainInfo.Age,//年龄
        //        patientInfo.SIMainInfo.Insutype,//险种类型
        //        patientInfo.SIMainInfo.Psn_type,//人员类别
        //        patientInfo.SIMainInfo.Cvlserv_flag,//公务员标志
        //        patientInfo.SIMainInfo.Setl_time,//结算时间
        //        patientInfo.SIMainInfo.Psn_setlway,//个人结算方式
        //        patientInfo.SIMainInfo.Mdtrt_cert_type,//就诊凭证类型
        //        patientInfo.SIMainInfo.Med_type,//医疗类别
        //        patientInfo.SIMainInfo.Medfee_sumamt,//医疗费总额
        //        patientInfo.SIMainInfo.Ownpay_amt,//全自费金额Ownpay_amt
        //        patientInfo.SIMainInfo.Overlmt_selfpay,//超限价自费费用
        //        patientInfo.SIMainInfo.Preselfpay_amt,//先行自付金额
        //        patientInfo.SIMainInfo.Inscp_scp_amt,//符合范围金额
        //        patientInfo.SIMainInfo.Med_sumfee,//医保认可费用总额
        //        patientInfo.SIMainInfo.Act_pay_dedc,//实际支付起付线
        //        patientInfo.SIMainInfo.Hifp_pay,//基本医疗保险统筹基金支出
        //        patientInfo.SIMainInfo.Pool_prop_selfpay,//基本医疗保险统筹基金支付比例
        //        patientInfo.SIMainInfo.Cvlserv_pay,//公务员医疗补助资金支出
        //        patientInfo.SIMainInfo.Hifes_pay,//企业补充医疗保险基金支出
        //        patientInfo.SIMainInfo.Hifmi_pay,//居民大病保险资金支出
        //        patientInfo.SIMainInfo.Hifob_pay,//职工大额医疗费用补助基金支出
        //        patientInfo.SIMainInfo.Hifdm_pay,//伤残人员医疗保障基金支出
        //        patientInfo.SIMainInfo.Maf_pay,//医疗救助基金支出
        //        patientInfo.SIMainInfo.Oth_pay,//其他基金支出
        //        patientInfo.SIMainInfo.Fund_pay_sumamt,//基金支付总额
        //        patientInfo.SIMainInfo.Hosp_part_amt,//医院负担金额
        //        patientInfo.SIMainInfo.Psn_part_am,//个人负担总金额
        //        patientInfo.SIMainInfo.Acct_pay,//个人账户支出
        //        patientInfo.SIMainInfo.Cash_payamt,//现金支付金额
        //        patientInfo.SIMainInfo.Acct_mulaid_pay,//账户共济支付金额
        //        patientInfo.SIMainInfo.Balc,//个人账户支出后余额
        //        patientInfo.SIMainInfo.Clr_optins,//清算经办机构
        //        patientInfo.SIMainInfo.Clr_way,//清算方式
        //        patientInfo.SIMainInfo.Clr_type,//清算类别
        //        patientInfo.SIMainInfo.Medins_setl_id,//医药机构结算id
        //        patientInfo.SIMainInfo.Vola_type,//违规类型
        //        patientInfo.SIMainInfo.Vola_dscr//违规说明
        //        );

        //    #endregion

        //    try
        //    {
        //        if (this.ExecNoQuery(strSql) < 0)
        //        {
        //            return -1;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        this.Err = e.Message;
        //        return -1;
        //    }

        //    return 1;
        //}
        #endregion

        /// <summary>
        /// 取消门诊结算
        /// </summary>
        /// <param name="patientInfo">挂号患者基本信息类</param>
        /// <returns></returns>
        public int CancelOutPatientBalance(FS.HISFC.Models.Registration.Register patientInfo)
        {
            string strSql = "";

            strSql = @"UPDATE FIN_IPR_SIINMAININFO
                          SET VALID_FLAG = '0',
                              BALANCE_STATE = '0',
                              OPER_CODE = '{0}',
                              OPER_DATE = to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                        WHERE INPATIENT_NO = '{2}'
                          AND REG_NO = '{3}'
                          AND INVOICE_NO = '{4}'";
            try
            {
                strSql = string.Format(strSql,
                    this.Operator.ID,
                    this.GetDateTimeFromSysDateTime(),
                    patientInfo.ID,
                    patientInfo.SIMainInfo.RegNo,
                    patientInfo.SIMainInfo.InvoiceNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 诊断实体赋值
        /// [未使用]
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        private ArrayList queryPatientDiagnose(string strSQL)
        {
            if (ExecQuery(strSQL) == -1)
            {
                return null;
            }
            ArrayList obj = new ArrayList();
            try
            {
                while (Reader.Read())
                {
                    FS.HISFC.Models.RADT.Diagnose diagTemp = new FS.HISFC.Models.RADT.Diagnose();
                    diagTemp.ID = Reader[0].ToString();
                    diagTemp.Name = Reader[1].ToString();
                    diagTemp.SpellCode = Reader[2].ToString();
                    diagTemp.WBCode = Reader[3].ToString();
                    diagTemp.Memo = Reader[4].ToString();
                    diagTemp.HappenNO = long.Parse(Reader[5].ToString());
                    obj.Add(diagTemp);
                }
                Reader.Close();
            } //抛出错误
            catch (Exception ex)
            {
                Err = "取诊断信息出错！" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }

            return obj;
        }

        /// <summary> 
        /// 门诊医生事前提醒时保存不判断规则原因
        /// [未使用]
        /// </summary>
        /// <param name="clinic_code"></param>
        /// <param name="rulechake"></param>
        /// <returns></returns>
        public int OutRegister(string clinic_code, string rulechake)
        {
            string strSql = "";

            strSql = @"UPDATE fin_opr_register
                          SET rulechake = '{1}'
                        WHERE clinic_code = '{0}'";
            try
            {
                strSql = string.Format(strSql, clinic_code, rulechake);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 获取对照的医院自定义码
        /// [未使用]
        /// </summary>
        /// <param name="HisCode"></param>
        /// <returns></returns>
        public string GetHisCode(string HisCode)
        {
            string strSql = @"select his_user_code
                                from fin_com_compare
                               where pacT_code='SZ04'
                                 AND HIS_code ='{0}'";
            strSql = string.Format(strSql, HisCode);

            try
            {
                return this.ExecSqlReturnOne(strSql);
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        #endregion

        #region 住院医保

        /// <summary>
        /// 得到住院医保患者基本信息;
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo GetSIPersonInfo(string inpatientNo, FS.HISFC.Models.RADT.PatientInfo obj)
        {
            #region 屏蔽旧代码
            //FS.HISFC.Models.RADT.PatientInfo obj = new FS.HISFC.Models.RADT.PatientInfo();
            //string strSql1 = @"SELECT inpatient_no,   --住院流水号
            //                         reg_no,       --就医登记号
            //                         balance_no,   --结算序号
            //                         invoice_no,   --发票号
            //                         medical_type,   --医疗类别
            //                         patient_no,   --住院号
            //                         card_no,   --就诊卡号
            //                         mcard_no,   --医疗证号
            //                         name,   --姓名
            //                         sex_code,   --性别
            //                         idenno,   --身份证号
            //                         birthday,   --生日
            //                         empl_type,   --人员类别 1 在职 2 退休
            //                         work_name,   --工作单位
            //                         clinic_diagnose,   --门诊诊断
            //                         dept_code,   --科室代码
            //                         dept_name,   --科室名称
            //                         paykind_code,   --结算类别 1-自费  2-保险 3-公费在职 4-公费退休 5-公费高干
            //                         pact_code,   --合同代码
            //                         pact_name,   --合同单位名称
            //                         bed_no,   --床号
            //                         in_date,   --入院日期
            //                         in_diagnosedate,--入院诊断日期
            //                         in_diagnose,   --入院诊断代码
            //                         in_diagnosename,   --入院诊断名称
            //                         out_date,   --出院日期
            //                         out_diagnose,   --出院诊断代码
            //                         out_diagnosename,   --出院诊断名称
            //                         balance_date,   --结算日期(上次)
            //                         tot_cost,   --费用金额(未结)(住院总金额)
            //                         pay_cost,   --帐户支付
            //                         pub_cost,   --公费金额(未结)(社保支付金额)
            //                         item_paycost,   --部分项目自付金额
            //                         base_cost,   --个人起付金额
            //                         item_paycost2,   --个人自费项目金额
            //                         item_ylcost,   --个人自付金额（乙类自付部分）
            //                         own_cost,   --个人自负金额
            //                         overtake_owncost,   --超统筹支付限额个人自付金额
            //                         own_cause,   --自费原因
            //                         oper_code,   --操作员
            //                         oper_date,    --操作日期
            //                         fee_times,
            //                         hos_cost,
            //                         year_cost,
            //                         VALID_FLAG,
            //                         BALANCE_STATE,
            //                         remark,
            //                         type_code,
            //                         over_cost,
            //                         person_type,
            //                         bka911,
            //                         bka912,
            //                         bka913,
            //                         bka914,
            //                         bka915,
            //                         bka916,
            //                         bka917,
            //                         bka042,
            //                         aaz267,
            //                         bka825,
            //                         bka826,
            //                         aka151,
            //                         bka838,
            //                         akb067,
            //                         akb066,
            //                         bka821,
            //                         bka839,
            //                         ake039,
            //                         ake035,
            //                         ake026,
            //                         ake029,
            //                         bka841,
            //                         bka842,
            //                         bka840,
            //                         aaa027,
            //                         bka438,
            //                         AAB301,
            //                         AAE140
            //                    FROM fin_ipr_siinmaininfo_gd   --广东省统一医保信息住院主表
            //                   WHERE inpatient_no = '{0}'
            //                     and valid_flag = '1'
            //                     and type_code = '2' ";
            //try
            //{
            //    while (Reader.Read())
            //    {
            //        obj.ID = Reader[0].ToString();
            //        obj.SIMainInfo.RegNo = Reader[1].ToString();
            //        obj.SIMainInfo.BalNo = Reader[2].ToString();
            //        //obj.SIMainInfo.InvoiceNo = Reader[3].ToString();
            //        obj.SIMainInfo.MedicalType.ID = Reader[4].ToString();
            //        obj.PID.PatientNO = Reader[5].ToString();
            //        obj.PID.CardNO = Reader[6].ToString();
            //        obj.SSN = Reader[7].ToString();
            //        obj.Name = Reader[8].ToString();
            //        obj.Sex.ID = Reader[9].ToString();
            //        obj.IDCard = Reader[10].ToString();
            //        //obj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[11].ToString());
            //        obj.SIMainInfo.EmplType = Reader[12].ToString();
            //        obj.CompanyName = Reader[13].ToString();
            //        obj.ClinicDiagnose = Reader[14].ToString();
            //        obj.PVisit.PatientLocation.Dept.ID = Reader[15].ToString();
            //        //obj.PVisit.PatientLocation.Dept.Name = Reader[26].ToString();
            //        obj.Pact.PayKind.ID = Reader[17].ToString();
            //        obj.Pact.ID = Reader[18].ToString();
            //        obj.Pact.Name = Reader[19].ToString();
            //        obj.PVisit.PatientLocation.Bed.ID = Reader[20].ToString();
            //        obj.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[21].ToString());
            //        obj.SIMainInfo.InDiagnoseDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[22].ToString());
            //        obj.SIMainInfo.InDiagnose.ID = Reader[23].ToString();
            //        obj.SIMainInfo.InDiagnose.Name = Reader[24].ToString();
            //        //if (!Reader.IsDBNull(25))
            //        //    obj.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[25].ToString());
            //        obj.SIMainInfo.OutDiagnose.ID = Reader[26].ToString();
            //        obj.SIMainInfo.OutDiagnose.Name = Reader[27].ToString();
            //        if (!Reader.IsDBNull(28))
            //            obj.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[28].ToString());

            //        obj.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[29].ToString());
            //        obj.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[30].ToString());
            //        obj.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[31].ToString());
            //        obj.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[32].ToString());
            //        obj.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[33].ToString());
            //        obj.SIMainInfo.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[34].ToString());
            //        obj.SIMainInfo.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[35].ToString());
            //        obj.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[36].ToString());
            //        obj.SIMainInfo.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[37].ToString());
            //        obj.SIMainInfo.Memo = Reader[38].ToString();
            //        obj.SIMainInfo.OperInfo.ID = Reader[39].ToString();
            //        obj.SIMainInfo.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[40].ToString());
            //        obj.SIMainInfo.FeeTimes = FS.FrameWork.Function.NConvert.ToInt32(Reader[41].ToString());
            //        obj.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[42].ToString());
            //        obj.SIMainInfo.YearCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[43].ToString());
            //        obj.SIMainInfo.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[44].ToString());
            //        obj.SIMainInfo.IsBalanced = FS.FrameWork.Function.NConvert.ToBoolean(Reader[45].ToString());
            //        obj.SIMainInfo.Memo = Reader[46].ToString();
            //        obj.SIMainInfo.TypeCode = Reader[47].ToString();
            //        obj.SIMainInfo.OverCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[48].ToString());
            //        obj.SIMainInfo.PersonType.ID = Reader[49].ToString();
            //        if (!Reader.IsDBNull(50))
            //            obj.SIMainInfo.Bka911 = FS.FrameWork.Function.NConvert.ToDateTime(Reader[50].ToString());
            //        obj.SIMainInfo.Bka912 = Reader[51].ToString();
            //        obj.SIMainInfo.Bka913 = FS.FrameWork.Function.NConvert.ToInt32(Reader[52].ToString());
            //        obj.SIMainInfo.Bka914 = Reader[53].ToString();
            //        if (!Reader.IsDBNull(54))
            //            obj.SIMainInfo.Bka915 = FS.FrameWork.Function.NConvert.ToDateTime(Reader[54].ToString());
            //        obj.SIMainInfo.Bka916 = Reader[55].ToString();
            //        if (!Reader.IsDBNull(56))
            //            obj.SIMainInfo.Bka917 = FS.FrameWork.Function.NConvert.ToDateTime(Reader[56].ToString());
            //        obj.SIMainInfo.Bka042 = Reader[57].ToString();
            //        obj.SIMainInfo.Aaz267 = Reader[58].ToString();
            //        obj.SIMainInfo.Bka825 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[59].ToString());
            //        obj.SIMainInfo.Bka826 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[60].ToString());
            //        obj.SIMainInfo.Aka151 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[61].ToString());
            //        obj.SIMainInfo.Bka838 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[62].ToString());
            //        obj.SIMainInfo.Akb067 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[63].ToString());
            //        obj.SIMainInfo.Akb066 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[64].ToString());
            //        obj.SIMainInfo.Bka821 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[65].ToString());
            //        obj.SIMainInfo.Bka839 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[66].ToString());
            //        obj.SIMainInfo.Ake039 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[67].ToString());
            //        obj.SIMainInfo.Ake035 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[68].ToString());
            //        obj.SIMainInfo.Ake026 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[69].ToString());
            //        obj.SIMainInfo.Ake029 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[70].ToString());
            //        obj.SIMainInfo.Bka841 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[71].ToString());
            //        obj.SIMainInfo.Bka842 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[72].ToString());
            //        obj.SIMainInfo.Bka840 = FS.FrameWork.Function.NConvert.ToDecimal(Reader[73].ToString());
            //        obj.SIMainInfo.Aaa027 = Reader[74].ToString();
            //        obj.SIMainInfo.Bka438 = Reader[75].ToString();
            //        obj.SIMainInfo.Aab301 = Reader[76].ToString();
            //        obj.SIMainInfo.Aae140 = Reader[77].ToString();
            //    }
            //    Reader.Close();
            //    return obj;
            //}
            //catch (Exception ex)
            //{
            //    this.ErrCode = ex.Message;
            //    this.Err = ex.Message;
            //    Reader.Close();
            //    return null;
            //}
            #endregion

            string strSql = @"select inpatient_no, -- 0住院流水号
                                     reg_no, -- 1就医登记号
                                     fee_times, -- 2费用批次
                                     balance_no, -- 3 结算序号
                                     invoice_no, -- 4 主发票号
                                     medical_type, -- 5 医疗类别
                                     patient_no, -- 6 住院号
                                     card_no, -- 7 卡号
                                     mcard_no, -- 8 医疗证号
                                     app_no, -- 9 审批号
                                     procreate_pcno, -- 10 生育保险患者电脑号
                                     si_begindate, -- 11 参保日期
                                     si_state, -- 12 参保状态
                                     name, -- 13 姓名
                                     sex_code, -- 14 性别
                                     idenno, -- 15 身份证号码
                                     spell_code, -- 16 拼音码
                                     birthday, -- 17 生日
                                     empl_type, -- 18 人员类型
                                     work_name, -- 19 工作单位
                                     clinic_diagnose, -- 20 门诊诊断
                                     dept_code, -- 21 科室编码
                                     dept_name, -- 22 科室名称
                                     paykind_code, -- 23 结算类别
                                     pact_code, --  24 合同代码
                                     pact_name, -- 25 合同单位名称
                                     bed_no, -- 26 床号
                                     in_date, -- 27 入院日期
                                     in_diagnosedate, --28 入院诊断日期
                                     in_diagnose, -- 29 入院诊断编码
                                     in_diagnosename, -- 30 入院诊断名称
                                     out_date, -- 31 出院时间
                                     out_diagnose, -- 32 出院诊断编码
                                     out_diagnosename, -- 33 出院诊断名称
                                     balance_date, -- 34 结算日期
                                     tot_cost, -- 35 总金额
                                     pay_cost, -- 36 帐户支付
                                     pub_cost, -- 37 社保支付金额
                                     item_paycost, -- 38 部分项目自付金额
                                     base_cost, -- 39 个人起付金额
                                     item_paycost2, -- 40 个人自费项目金额
                                     item_ylcost, -- 41 个人自付金额（乙类自付部分）
                                     own_cost, -- 42 个人自负金额
                                     overtake_owncost,-- 43 超统筹支付限额个人自付金额
                                     hos_cost, -- 44 医药机构分担金额(中山医保民政统筹金额)
                                     own_cause, -- 45 自费原因
                                     oper_code, -- 46 操作员编码
                                     oper_date, -- 47 操作时间
                                     year_cost, -- 48 本年度可用定额
                                     valid_flag, -- 49 是否有效
                                     balance_state, -- 50 是否结算
                                     individualbalance, -- 51 个人账户余额
                                     freezemessage, -- 52 冻结信息
                                     applysequence, -- 53 申请序号
                                     applytypeid, -- 54 申请类型编号
                                     applytypename, -- 55 申请类型
                                     fundid, -- 56 基金编码
                                     fundname, -- 57 基金名称
                                     businesssequence, -- 58 业务序列号
                                     invoice_seq, -- 59 发票序号
                                     over_cost, -- 60 医保大额补助
                                     official_cost, -- 61 医保公务员补助
                                     remark, -- 62 医保信息（PersonAccountInfo）
                                     type_code, -- 63 人员类型
                                     trans_type, -- 64 交易类型
                                     person_type, -- 65 人员类型
                                     diagnose_oper_code, -- 66 操作员
                                     operatecode1, --67
                                     operatecode2, -- 68
                                     operatecode3, -- 69
                                     primarydiagnosename, -- 70
                                     primarydiagnosecode, -- 71
                                     ybmedno, -- 72 居民医保结算单号,保存以做为 注销居民门诊费用结算 参数之一
                                     trans_no, -- 73 上传号                                 --东莞医保
                                     internal_fee, -- 74 普通医保内费用                  --东莞医保
                                     external_fee, --75 external_fee 75 普通医保外费用                   --东莞医保
                                     official_own_cost, -- 76 大额/公务员自付金额              --东莞医保
                                     over_inte_fee, -- 77 本次交易统筹封顶后医保内金额    --东莞医保
                                     own_count_fee, -- 78  个人应付总金额(个人帐户支付+现金)    --东莞医保
                                     own_second_count_fee, -- 79 个人自付二金额    --东莞医保
                                     si_diagnose, -- 80 医保诊断代码    --东莞医保
                                     si_diagnosename, -- 81 医保诊断代码名称     --东莞医保
                                     pub_fee_cost, -- 82 统筹支付金额     --东莞医保
                                     ext_flag, -- 83 深圳医保的执行状态 登记： R 上传 ：S 结算：B 支付：J
                                     ext_flag1, -- 84 异地医保的参保地址
                                     zhuhaisitype, -- 85 参保险种(珠海医保)
                                     gzsiupload, -- 86 广州医保上传状态 未上传：0， 已上传 1
                                     mdtrt_id, -- 87 就诊ID
                                     setl_id, -- 88 结算ID
                                     psn_no, -- 89 人员编号
                                     psn_name, -- 90 人员姓名
                                     psn_cert_type, -- 91 人员证件类型
                                     certno, -- 92 证件号码
                                     gend, -- 93 性别
                                     naty, -- 94 民族
                                     brdy, -- 95 生日
                                     age, -- 96 年龄
                                     insutype, -- 97 险种类型
                                     psn_type, -- 98 人员类别
                                     cvlserv_flag, -- 99 公务员标识
                                     setl_time, -- 100 结算时间
                                     psn_setlway, -- 101 个人结算方式
                                     mdtrt_cert_type, -- 102 就诊凭证类型
                                     med_type, -- 103 医疗类别
                                     medfee_sumamt, -- 104 医疗费总额
                                     ownpay_amt, -- 105 全自费金额
                                     overlmt_selfpay, -- 106 超限价自费费用
                                     preselfpay_amt, -- 107 先行自付金额
                                     inscp_scp_amt, -- 108 符合范围金额
                                     med_sumfee, -- 109 医保认可费用总额
                                     act_pay_dedc, -- 110 实际支付起付线
                                     hifp_pay, -- 111 基本医疗保险统筹基金支出
                                     pool_prop_selfpay, -- 112 基本医疗保险统筹基金支付比例
                                     cvlserv_pay, -- 113 公务员医疗补助资金支出
                                     hifes_pay, -- 114 企业补充医疗保险基金支出
                                     hifmi_pay, -- 115 居民大病保险资金支出
                                     hifob_pay, -- 116 职工大额医疗费用补助基金支出
                                     hifdm_pay, -- 117 伤残人员医疗保障基金支出
                                     maf_pay, -- 118 医疗救助基金支出
                                     oth_pay, -- 119 其他基金支出
                                     fund_pay_sumamt, -- 120 基金支付总额
                                     hosp_part_amt, -- 121 医院负担金额
                                     psn_part_am, -- 122 个人负担总金额
                                     acct_pay, -- 123 个人账户支出
                                     cash_payamt, -- 124 现金支付金额
                                     acct_mulaid_pay, -- 125 账户共济支付金额
                                     balc, -- 126 个人账户支出后余额
                                     clr_optins, -- 127 清算经办机构
                                     clr_way, --128 清算方式
                                     clr_type, -- 129 清算类别
                                     medins_setl_id, -- 130 医药机构结算ID
                                     vola_type, -- 131 违规类型
                                     vola_dscr -- 132 违规说明
                                from fin_ipr_siinmaininfo
                               WHERE inpatient_no = '{0}'
                                 and valid_flag = '1'
                                 and type_code = '2' ";

            try
            {
                strSql = string.Format(strSql, inpatientNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            this.ExecQuery(strSql);

            try
            {
                while (Reader.Read())
                {
                    obj.ID = Reader[0].ToString(); //住院流水号
                    obj.SIMainInfo.RegNo = Reader[1].ToString(); //就医登记号
                    //obj.SIMainInfo.FeeTimes = Int32.Parse(Reader[2].ToString()); //费用批次
                    obj.SIMainInfo.BalNo = Reader[3].ToString(); //结算序号
                    obj.SIMainInfo.InvoiceNo = Reader[4].ToString(); //主发票号
                    obj.SIMainInfo.MedicalType.ID = Reader[5].ToString(); //医疗类别
                    obj.PID.PatientNO = Reader[6].ToString(); //住院号
                    obj.PID.CardNO = Reader[7].ToString(); //卡号
                    obj.SSN = Reader[8].ToString(); //医疗证号
                    //obj.SIMainInfo.AppNo = Int32.Parse(Reader[9].ToString()); //审批号
                    obj.SIMainInfo.ProceatePcNo = Reader[10].ToString(); //生育保险患者电脑号

                    obj.SIMainInfo.SiBegionDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[11].ToString()); //参保日期
                    obj.SIMainInfo.SiState = Reader[12].ToString(); //参保状态
                    obj.Name = Reader[13].ToString(); //姓名
                    obj.Sex.ID = Reader[14].ToString(); //性别
                    obj.IDCard = Reader[15].ToString(); //身份证号码
                    obj.SpellCode = Reader[16].ToString(); //拼音码
                    if (!Reader.IsDBNull(17))
                        obj.Birthday = DateTime.Parse(Reader[17].ToString()); //生日
                    obj.SIMainInfo.EmplType = Reader[18].ToString(); //人员类型
                    obj.CompanyName = Reader[19].ToString(); //工作单位
                    obj.ClinicDiagnose = Reader[20].ToString(); //门诊诊断

                    obj.PVisit.PatientLocation.Dept.ID = Reader[21].ToString(); //科室编码
                    obj.PVisit.PatientLocation.Dept.Name = Reader[22].ToString(); //科室名称
                    obj.Pact.PayKind.ID = Reader[23].ToString(); //结算类别
                    obj.Pact.ID = Reader[24].ToString(); //合同代码
                    obj.Pact.Name = Reader[25].ToString(); //合同单位名称
                    obj.PVisit.PatientLocation.Bed.ID = Reader[26].ToString(); //床号 
                    obj.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[27].ToString()); //入院日期
                    obj.SIMainInfo.InDiagnoseDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[28].ToString()); //入院诊断日期
                    obj.SIMainInfo.InDiagnose.ID = Reader[29].ToString(); //入院诊断编码
                    obj.SIMainInfo.InDiagnose.Name = Reader[30].ToString(); //入院诊断名称

                    if (!Reader.IsDBNull(31))
                        obj.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[31].ToString()); //出院时间
                    obj.SIMainInfo.OutDiagnose.ID = Reader[32].ToString(); //出院诊断编码
                    obj.SIMainInfo.OutDiagnose.Name = Reader[33].ToString(); //出院诊断名称
                    if (!Reader.IsDBNull(34))
                        obj.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[34].ToString()); //结算日期
                    obj.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[35].ToString()); //总金额
                    obj.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[36].ToString()); //账户支付
                    obj.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[37].ToString()); //社保支付金额
                    obj.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[38].ToString()); //部分项目自付金额
                    obj.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[39].ToString()); //个人起付金额
                    obj.SIMainInfo.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[40].ToString()); //个人自费项目金额

                    obj.SIMainInfo.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[41].ToString()); //个人自付金额（乙类自付部分）
                    obj.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[42].ToString()); //个人自负金额
                    obj.SIMainInfo.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[43].ToString()); //超统筹支付限额个人自付金额
                    obj.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[44].ToString()); //医药机构分担金额(中山医保民政统筹金额)
                    obj.SIMainInfo.Memo = Reader[45].ToString(); //自费原因
                    obj.SIMainInfo.OperInfo.ID = Reader[46].ToString(); //操作员编码
                    obj.SIMainInfo.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[47].ToString()); //操作时间
                    obj.SIMainInfo.YearCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[48].ToString()); //本年度可用定额
                    obj.SIMainInfo.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[49].ToString()); //是否有效
                    obj.SIMainInfo.BalanceState = Reader[50].ToString(); //是否结算

                    obj.SIMainInfo.IndividualBalance = FS.FrameWork.Function.NConvert.ToDecimal(Reader[51].ToString()); //个人账户余额
                    obj.SIMainInfo.FreezeMessage = Reader[52].ToString(); //冻结信息
                    obj.SIMainInfo.ApplySequence = Reader[53].ToString(); //申请序号
                    obj.SIMainInfo.ApplyType.ID = Reader[54].ToString(); //申请类型编号
                    obj.SIMainInfo.ApplyType.Name = Reader[55].ToString(); //申请类型名称
                    obj.SIMainInfo.Fund.ID = Reader[56].ToString(); //基金编码
                    obj.SIMainInfo.Fund.Name = Reader[57].ToString(); //基金名称
                    obj.SIMainInfo.BusinessSequence = Reader[58].ToString(); //业务序列号
                    //invoice_seq 发票序号 59
                    obj.SIMainInfo.OverCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[60].ToString()); //医保大额补助

                    obj.SIMainInfo.OfficalCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[61].ToString()); //医保公务员补助
                    obj.SIMainInfo.Memo = Reader[62].ToString(); //医保信息（PersonAccountInfo）
                    obj.SIMainInfo.TypeCode = Reader[63].ToString(); //人员类型
                    obj.SIMainInfo.TransType = Reader[64].ToString(); //交易类型
                    obj.SIMainInfo.PersonType.ID = Reader[65].ToString(); //人员类型
                    //diagnose_oper_code 66 操作员
                    //operatecode1 67
                    //operatecode2 68
                    //operatecode3 69
                    //primarydiagnosename 70

                    //primarydiagnosecode 71
                    //ybmedno 72 居民医保结算单号,保存以做为 注销居民门诊费用结算 参数之一
                    //---------------东莞医保--------------------
                    //trans_no 73 上传号                                 --东莞医保
                    //internal_fee 74 普通医保内费用                  --东莞医保
                    //external_fee 75 普通医保外费用                   --东莞医保
                    //official_own_cost 76 大额/公务员自付金额              --东莞医保
                    //over_inte_fee 77 本次交易统筹封顶后医保内金额    --东莞医保
                    //own_count_fee 78 个人应付总金额(个人帐户支付+现金)    --东莞医保
                    //own_second_count_fee 79 个人自付二金额    --东莞医保
                    //si_diagnose 80 医保诊断代码    --东莞医保

                    //si_diagnosename 81医保诊断代码名称     --东莞医保
                    //pub_fee_cost 82 统筹支付金额     --东莞医保
                    //ext_flag 83 深圳医保的执行状态 登记： R 上传 ：S 结算：B 支付：J
                    //ext_flag1 84 异地医保的参保地址
                    //zhuhaisitype 85 参保险种(珠海医保)

                    // 86 广州医保上传状态 未上传：0， 已上传 1
                    obj.SIMainInfo.IsSIUploaded = FS.FrameWork.Function.NConvert.ToBoolean(Reader[86].ToString());

                    //---------------广州医保API新增接口--------------------
                    obj.SIMainInfo.Mdtrt_id = Reader[87].ToString(); //就诊ID
                    obj.SIMainInfo.Setl_id = Reader[88].ToString(); //结算ID
                    obj.SIMainInfo.Psn_no = Reader[89].ToString(); //人员编号
                    obj.SIMainInfo.Psn_name = Reader[90].ToString(); // 人员姓名

                    obj.SIMainInfo.Psn_cert_type = Reader[91].ToString(); //人员证件类型
                    obj.SIMainInfo.Certno = Reader[92].ToString(); //证件号码
                    obj.SIMainInfo.Gend = Reader[93].ToString(); //性别
                    obj.SIMainInfo.Naty = Reader[94].ToString(); //民族
                    obj.SIMainInfo.Brdy = FS.FrameWork.Function.NConvert.ToDateTime(Reader[95].ToString()); //生日
                    obj.SIMainInfo.Age = Reader[96].ToString(); //年龄
                    obj.SIMainInfo.Insutype = Reader[97].ToString(); //险种类型
                    obj.SIMainInfo.Psn_type = Reader[98].ToString(); //人员类别
                    obj.SIMainInfo.Cvlserv_flag = Reader[99].ToString(); //公务员标识
                    obj.SIMainInfo.Setl_time = FS.FrameWork.Function.NConvert.ToDateTime(Reader[100].ToString()); //结算时间

                    obj.SIMainInfo.Psn_setlway = Reader[101].ToString(); //个人结算方式
                    obj.SIMainInfo.Mdtrt_cert_type = Reader[102].ToString(); //就诊凭证类型
                    obj.SIMainInfo.Med_type = Reader[103].ToString(); //医疗类别
                    obj.SIMainInfo.Medfee_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[104].ToString()); //医疗费总额
                    obj.SIMainInfo.Ownpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[105].ToString()); //全自费金额
                    obj.SIMainInfo.Overlmt_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[106].ToString()); //超限价自费费用
                    obj.SIMainInfo.Preselfpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[107].ToString()); //先行自付金额
                    obj.SIMainInfo.Inscp_scp_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[108].ToString()); //符合范围金额
                    obj.SIMainInfo.Med_sumfee = FS.FrameWork.Function.NConvert.ToDecimal(Reader[109].ToString()); //医保认可费用总额
                    obj.SIMainInfo.Act_pay_dedc = FS.FrameWork.Function.NConvert.ToDecimal(Reader[110].ToString()); //实际支付起付线

                    obj.SIMainInfo.Hifp_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[111].ToString()); //基本医疗保险统筹基金支出
                    obj.SIMainInfo.Pool_prop_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[112].ToString()); //基本医疗保险统筹基金支付比例
                    obj.SIMainInfo.Cvlserv_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[113].ToString()); //公务员医疗补助资金支出
                    obj.SIMainInfo.Hifes_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[114].ToString()); //企业补充医疗保险基金支出
                    obj.SIMainInfo.Hifmi_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[115].ToString()); //居民大病保险资金支出
                    obj.SIMainInfo.Hifob_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[116].ToString()); //职工大额医疗费用补助基金支出
                    obj.SIMainInfo.Hifdm_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[117].ToString()); //伤残人员医疗保障基金支出
                    obj.SIMainInfo.Maf_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[118].ToString()); //医疗救助基金支出
                    obj.SIMainInfo.Oth_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[119].ToString()); //其他基金支出
                    obj.SIMainInfo.Fund_pay_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[120].ToString()); //基金支付总额

                    obj.SIMainInfo.Hosp_part_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[121].ToString()); //医院负担金额
                    obj.SIMainInfo.Psn_part_am = FS.FrameWork.Function.NConvert.ToDecimal(Reader[122].ToString()); //个人负担总金额
                    obj.SIMainInfo.Acct_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[123].ToString()); //个人账户支出
                    obj.SIMainInfo.Cash_payamt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[124].ToString()); //现金支付金额
                    obj.SIMainInfo.Acct_mulaid_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[125].ToString()); //账户共济支付金额
                    obj.SIMainInfo.Balc = FS.FrameWork.Function.NConvert.ToDecimal(Reader[126].ToString()); //个人账户支出后余额
                    obj.SIMainInfo.Clr_optins = Reader[127].ToString(); //清算经办机构
                    obj.SIMainInfo.Clr_way = Reader[128].ToString(); //清算方式
                    obj.SIMainInfo.Clr_type = Reader[129].ToString(); //清算类别
                    obj.SIMainInfo.Medins_setl_id = Reader[130].ToString(); //医药机构结算ID

                    obj.SIMainInfo.Vola_type = Reader[131].ToString(); //违规类型
                    obj.SIMainInfo.Vola_dscr = Reader[132].ToString(); //违规说明
                }
                Reader.Close();
                return obj;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                Reader.Close();
                return null;
            }

        }

        #region 住院诊断// {1624908D-0C01-4E1C-A7FB-445DE755E85E}
        /// <summary>
        /// 获取住院出院诊断信息
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public ArrayList InpatientClinicDiagnoseList(string inpatientNo)
        {
            ArrayList obj = new ArrayList();
            string strSql = @" select  (select distinct a.old_code  from MET_COM_ICDCOMPARE10 a where a.new_code   =t.ext_flag2 and a.valid_state = '1' )   diag_code ,
                                                    t.clinic_diagnose
                                        from fin_ipr_inmaininfo t
                                        where t.inpatient_no = '{0}'
                                        and t.ext_flag2 != 'MS999' --过滤描述诊断
                                        and trim(t.clinic_diagnose) != '正常妊娠监督' --过滤指定诊断
                                ";
            strSql = string.Format(strSql, inpatientNo);

            try
            {
                if (ExecQuery(strSql) == -1)
                {
                    return null;
                }
                while (Reader.Read())
                {
                    if (string.IsNullOrEmpty(Reader[0].ToString()))
                    {
                        continue;
                    }

                    FS.HISFC.Models.RADT.Diagnose diag = new FS.HISFC.Models.RADT.Diagnose();
                    diag.ID = Reader[0].ToString();//诊断编码
                    diag.Name = Reader[1].ToString();//诊断名称
                    diag.ICD10.ID = Reader[0].ToString();//诊断编码
                    diag.ICD10.Name = Reader[1].ToString();//诊断名称
                    obj.Add(diag);
                }
                Reader.Close();
            } //抛出错误
            catch (Exception ex)
            {
                Err = "取诊断信息出错！" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
            return obj;
        }

        /// <summary>
        /// 获取住院电子病历诊断信息
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public ArrayList InpatientBaseDiagnoseList(string inpatientNo)
        {
            ArrayList obj = new ArrayList();
            string strSql = @"select 
              (select distinct a.old_code  from MET_COM_ICDCOMPARE10 a where a.new_code   =t3.FICDM and a.valid_state = '1' )   diag_code, --诊断代码
             (select distinct a.old_name  from MET_COM_ICDCOMPARE10 a where a.new_code   =t3.FICDM and a.valid_state = '1' )    diag_name --诊断名称
            from  fin_ipr_inmaininfo t2,
                      tDiagnose@proemr_dblink t3,-- left join met_com_icdcompare10 a on t3.FICDM = a.new_code and a.valid_state = '1',
                      tpatientvisit@proemr_dblink t4
            where t2.in_state <> 'N'
            and ltrim(t2.patient_no,'0') = ltrim(t3.FPRN,'0')
            and t2.in_times = t3.FTIMES
            and t3.fprn = t4.fprn
            and t3.ftimes = t4.ftimes
            and (regexp_substr(t3.FJBNAME,'[0-9]+') is null or t3.FJBNAME like '%24%')
            and t2.inpatient_no = '{0}'
            order by t3.FZDLX";//获取电子病病历的诊断// {3DC7C1C0-A521-4841-A329-1A5B5BBD6A8B}
            strSql = string.Format(strSql, inpatientNo);

            try
            {
                if (ExecQuery(strSql) == -1)
                {
                    return null;
                }
                while (Reader.Read())
                {
                    if (string.IsNullOrEmpty(Reader[0].ToString()))
                    {
                        continue;
                    }

                    FS.HISFC.Models.RADT.Diagnose diag = new FS.HISFC.Models.RADT.Diagnose();
                    diag.ID = Reader[0].ToString();//诊断编码
                    diag.Name = Reader[1].ToString();//诊断名称
                    diag.ICD10.ID = Reader[0].ToString();//诊断编码
                    diag.ICD10.Name = Reader[1].ToString();//诊断名称
                    obj.Add(diag);
                }
                Reader.Close();
            } //抛出错误
            catch (Exception ex)
            {
                Err = "取诊断信息出错！" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
            return obj;
        }

        /// <summary>
        /// 获取住院电子病历诊断信息// {39FB648E-CC9A-4B52-B278-649A3B08E69D}
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public ArrayList InpatientEMRDiagnoseList(string inpatientNo, string diagType)
        {
            ArrayList obj = new ArrayList();

            //{EBF974BE-9D1E-492C-81EA-49BE9CC1289C}
            string strSql = @"select  nvl(nvl((select distinct a.old_code  from MET_COM_ICDCOMPARE10 a where a.new_code   = d.diag_code and a.valid_state = '1' ),
                    (select distinct a.old_code  from MET_COM_ICDCOMPARE10 a where a.new_name   = d.diag_name and a.valid_state = '1' )) , d.diag_code) diag_code,
                                                   nvl( (select distinct a.old_name  from MET_COM_ICDCOMPARE10 a where a.new_code   =d.diag_code and a.valid_state = '1' ) ,d.diag_name)  diag_name，
                                                    d.MAINDIAG_FLAG,
                                                    d.inout_diag_type,
                                                     d.diag_code
                                         from view_emr_disease d 
                                        where d.inpatient_no = '{0}' and d.maindiag_flag!='1'
union all 
select  nvl(nvl((select distinct a.old_code  from MET_COM_ICDCOMPARE10 a where a.new_code   = d.diag_code and a.valid_state = '1' ),
                    (select distinct a.old_code  from MET_COM_ICDCOMPARE10 a where a.new_name   = d.diag_name and a.valid_state = '1' )) , d.diag_code) diag_code,
                                                   nvl( (select distinct a.old_name  from MET_COM_ICDCOMPARE10 a where a.new_code   =d.diag_code and a.valid_state = '1' ) ,d.diag_name)  diag_name，
                                                    d.MAINDIAG_FLAG,
                                                    d.inout_diag_type,
                                                     d.diag_code
                                         from view_emr_disease d 
                                        where d.inpatient_no = '{0}' and d.maindiag_flag='1' and d.inout_diag_type='1' and rownum=1
union all 
select  nvl(nvl((select distinct a.old_code  from MET_COM_ICDCOMPARE10 a where a.new_code   = d.diag_code and a.valid_state = '1' ),
                    (select distinct a.old_code  from MET_COM_ICDCOMPARE10 a where a.new_name   = d.diag_name and a.valid_state = '1' )) , d.diag_code) diag_code,
                                                   nvl( (select distinct a.old_name  from MET_COM_ICDCOMPARE10 a where a.new_code   =d.diag_code and a.valid_state = '1' ) ,d.diag_name)  diag_name，
                                                    d.MAINDIAG_FLAG,
                                                    d.inout_diag_type,
                                                     d.diag_code
                                         from view_emr_disease d 
                                        where d.inpatient_no = '{0}' and d.maindiag_flag='1' and d.inout_diag_type='2' and rownum=1";

            strSql = string.Format(strSql, inpatientNo);
            if (!string.IsNullOrEmpty(diagType))
            {
                strSql += string.Format("and d.inout_diag_type = '{0}'", diagType);
            }

            try
            {
                if (ExecQuery(strSql) == -1)
                {
                    return null;
                }
                while (Reader.Read())
                {
                    if (string.IsNullOrEmpty(Reader[0].ToString()))
                    {
                        continue;
                    }

                    FS.HISFC.Models.RADT.Diagnose diag = new FS.HISFC.Models.RADT.Diagnose();
                    diag.ID = Reader[0].ToString();//诊断编码
                    diag.Name = Reader[1].ToString();//诊断名称
                    diag.ICD10.ID = Reader[0].ToString();//诊断编码
                    diag.ICD10.Name = Reader[1].ToString();//诊断名称
                    diag.IsMain = Reader[2].ToString() == "1" ? true : false;//主诊断标志
                    diag.Type.ID = Reader[3].ToString();//出入院诊断标志
                    obj.Add(diag);
                }
                Reader.Close();
            } //抛出错误
            catch (Exception ex)
            {
                Err = "取诊断信息出错！" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
            return obj;
        }

        /// <summary>
        /// 获取医保住院登记表的入院诊断、出院诊断信息
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public ArrayList InpatientSIDiagnoseList(string inpatientNo)
        {
            ArrayList obj = new ArrayList();
            string strSql = @" select s.in_diagnose,s.in_diagnosename,
                                                    s.out_diagnose,s.out_diagnosename
                                             from fin_ipr_siinmaininfo s 
                                             where s.in_diagnosename is not null 
                                             and s.out_diagnosename is not null
                                             and s.in_diagnose != 'MS999'
                                             and s.inpatient_no  = '{0}'";
            strSql = string.Format(strSql, inpatientNo);

            try
            {
                if (ExecQuery(strSql) == -1)
                {
                    return null;
                }
                while (Reader.Read())
                {
                    if (string.IsNullOrEmpty(Reader[0].ToString()))
                    {
                        continue;
                    }

                    FS.HISFC.Models.RADT.Diagnose diag = new FS.HISFC.Models.RADT.Diagnose();
                    diag.ID = Reader[0].ToString();//诊断编码
                    diag.Name = Reader[1].ToString();//诊断名称
                    diag.ICD10.ID = Reader[2].ToString();//诊断编码
                    diag.ICD10.Name = Reader[3].ToString();//诊断名称
                    obj.Add(diag);
                }
                Reader.Close();
            } //抛出错误
            catch (Exception ex)
            {
                Err = "取诊断信息出错！" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
            return obj;
        }

        /// <summary>
        /// 获取ICD10诊断信息
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public ArrayList getDiagList()
        {
            ArrayList obj = new ArrayList();
            string strSql = @"select 
                                        icd_code as code,
                                        icd_name as name
                                        from MET_COM_ICD10 t";

            try
            {
                if (ExecQuery(strSql) == -1)
                {
                    return null;
                }
                while (Reader.Read())
                {
                    if (string.IsNullOrEmpty(Reader[0].ToString()))
                    {
                        continue;
                    }

                    FS.HISFC.Models.RADT.Diagnose diag = new FS.HISFC.Models.RADT.Diagnose();
                    diag.ID = Reader[0].ToString();//诊断编码
                    diag.Name = Reader[1].ToString();//诊断名称
                    diag.ICD10.ID = Reader[0].ToString();//诊断编码
                    diag.ICD10.Name = Reader[1].ToString();//诊断名称
                    obj.Add(diag);
                }
                Reader.Close();
            } //抛出错误
            catch (Exception ex)
            {
                Err = "取诊断信息出错！" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
            return obj;
        }

        #endregion

        /// <summary>
        /// 插入住院登记信息
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int InsertInPatientReg(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            //医保基本信息保存到表FIN_IPR_SIINMAININFO_NEW
            int nextBalanceNo = this.GetNextBalanceNo(patientInfo.ID, "2");
            string nextBalanceNoStr = nextBalanceNo.ToString();

            patientInfo.SIMainInfo.BalNo = nextBalanceNoStr;

            #region sql

            string strSql = @"INSERT INTO FIN_IPR_SIINMAININFO
                                          (INPATIENT_NO, --住院流水号 0
                                           REG_NO, --就医登记号 1
                                           BALANCE_NO, --结算序号 2
                                           PATIENT_NO, --住院号 3
                                           CARD_NO, --卡号 4
                                           MCARD_NO, --医疗证号 5
                                           NAME, --姓名 6
                                           SEX_CODE, --性别 7
                                           IDENNO, --身份证号 8
                                           BIRTHDAY, --生日 9
                                           CLINIC_DIAGNOSE, --门诊诊断 10
                                           IN_DATE, --入院日期 11
                                           IN_DIAGNOSEDATE, --入院诊断日期 12
                                           IN_DIAGNOSE, -- 入院诊断编码 13
                                           IN_DIAGNOSENAME, -- 入院诊断名称 14
                                           DEPT_CODE, --科室代码 15
                                           DEPT_NAME, --科室名称 16
                                           PAYKIND_CODE, --结算类别 1-自费  2-保险 3-公费在职 4-公费退休 5-公费高干 17
                                           PACT_CODE, --合同代码 18
                                           PACT_NAME, --合同单位名称 19
                                           MDTRT_ID, --就诊ID 20
                                           PSN_NO, --人员编号 21
                                           PSN_NAME, --人员姓名 22
                                           PSN_CERT_TYPE, --人员证件类型 23
                                           CERTNO, --证件号码 24
                                           GEND, --性别 25
                                           BRDY, --生日 26
                                           INSUTYPE, --险种类型 27
                                           PSN_TYPE, --人员类别 28
                                           MDTRT_CERT_TYPE, --就诊凭证类型 29
                                           MED_TYPE, --医疗类别 30
                                           DISE_CODE, --病种编码 31
                                           DISE_NAME, --病种名称 32
                                           VOLA_TYPE, --违规类型 33
                                           VOLA_DSCR, --违规说明 34
                                           OPER_CODE, --操作员 35
                                           OPER_DATE, --操作日期 36
                                           TYPE_CODE --1 门诊/2 住院 37
                                           )
                                  VALUES ('{0}', --住院流水号 0
                                          '{1}', --就医登记号 1
                                          '{2}', --结算序号 2
                                          '{3}', --住院号 3
                                          '{4}', --卡号  4
                                          '{5}', --医疗证号 5
                                          '{6}', --姓名 6
                                          '{7}', --性别  7
                                          '{8}', --身份证号 8
                                          to_date('{9}','YYYY-MM-DD hh24:mi:ss'), --生日 9
                                          '{10}', --门诊诊断  10
                                          to_date('{11}','YYYY-MM-DD hh24:mi:ss'), --入院时间 11
                                          to_date('{12}','YYYY-MM-DD hh24:mi:ss'), --入院诊断时间 12
                                          '{13}', --入院诊断编码 13
                                          '{14}', --入院诊断 14
                                          '{15}', --科室代码 15
                                          '{16}', --科室名称 16
                                          '{17}',  --结算类别 1-自费  2-保险 3-公费在职 4-公费退休 5-公费高干 17
                                          '{18}', --合同代码 18
                                          '{19}', --合同单位名称 19
                                          '{20}', --就诊ID 20
                                          '{21}', --人员编号 21
                                          '{22}', --人员姓名 22
                                          '{23}', --人员证件类型 23
                                          '{24}', --证件号码 24
                                          '{25}', --性别 25
                                          to_date('{26}','YYYY-MM-DD hh24:mi:ss'), --生日 26
                                          '{27}', --险种类型 27
                                          '{28}', --人员类别 28
                                          '{29}', --就诊凭证类型 29
                                          '{30}', --医疗类别 30
                                          '{31}', --病种编码 31
                                          '{32}', --病种名称 32
                                          '{33}', --违规类型 33
                                          '{34}', --违规说明 34
                                          '{35}', --操作员 35
                                          to_date('{36}', 'YYYY-MM-DD hh24:mi:ss'),--操作日期 36
                                          '2'  )";
            #endregion

            try
            {
                FS.HISFC.Models.RADT.Diagnose diagInfo = patientInfo.Diagnoses[0] as FS.HISFC.Models.RADT.Diagnose;
                if (diagInfo == null)
                {
                    diagInfo = new FS.HISFC.Models.RADT.Diagnose();
                }

                strSql = string.Format(strSql,
                    patientInfo.ID,//住院流水号/门诊流水号
                    patientInfo.SIMainInfo.Mdtrt_id,//就医登记号
                    patientInfo.SIMainInfo.BalNo, //结算序号
                    patientInfo.PID.PatientNO, //住院号
                    patientInfo.PID.CardNO,//卡号
                    patientInfo.SIMainInfo.Psn_no,//医疗证号
                    patientInfo.Name,//姓名
                    patientInfo.Sex.ID.ToString(),//性别
                    patientInfo.IDCard, //身份证
                    patientInfo.SIMainInfo.Brdy.ToString("yyyy-MM-dd HH:mm:ss"), //生日
                    patientInfo.ClinicDiagnose, //门诊诊断
                    patientInfo.PVisit.InTime.ToString("yyyy-MM-dd HH:mm:ss"),//入院日期
                    this.GetDateTimeFromSysDateTime().ToString(), //诊断时间
                    diagInfo.ID, //诊断编码
                    diagInfo.Name, //诊断名称
                    patientInfo.PVisit.PatientLocation.Dept.ID,//科室代码
                    patientInfo.PVisit.PatientLocation.Dept.Name,//科室名称
                    patientInfo.Pact.PayKind.ID,//结算类别 1-自费  2-保险 3-公费在职 4-公费退休 5-公费高干
                    patientInfo.Pact.ID,//合同代码
                    patientInfo.Pact.Name,//合同单位名称
                    patientInfo.SIMainInfo.Mdtrt_id,//就诊ID
                    patientInfo.SIMainInfo.Psn_no,//人员编号
                    patientInfo.SIMainInfo.Psn_name, //人员姓名
                    patientInfo.SIMainInfo.Psn_cert_type, //人员证件类型
                    patientInfo.SIMainInfo.Certno, //证件号码
                    patientInfo.SIMainInfo.Gend, //性别
                    patientInfo.SIMainInfo.Brdy.ToString("yyyy-MM-dd HH:mm:ss"), //生日
                    patientInfo.SIMainInfo.Insutype, //险种类型
                    patientInfo.SIMainInfo.Psn_type, //人员类别
                    patientInfo.SIMainInfo.Mdtrt_cert_type, //就诊凭证类型
                    patientInfo.SIMainInfo.Med_type, //医疗类别
                    patientInfo.SIMainInfo.Dise_code, //病种编码
                    patientInfo.SIMainInfo.Dise_name, //病种名称
                    patientInfo.SIMainInfo.Vola_type,//违规类型
                    patientInfo.SIMainInfo.Vola_dscr,//违规说明
                    this.Operator.ID,//操作员
                    this.GetDateTimeFromSysDateTime().ToString()//操作日期
                    );
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }

            return 1;

            #region 屏蔽
            //#region sql
            //string strSql = @"INSERT INTO fin_ipr_siinmaininfo_NEW f
            //                              (
            //                              inpatient_no,
            //                              reg_no,
            //                              balance_no,
            //                              patient_no,
            //                              card_no,
            //                              mcard_no,
            //                              name,
            //                              idenno,
            //                              clinic_diagnose,
            //                              paykind_code,
            //                              pact_code,
            //                              pact_name,
            //                              oper_code,
            //                              oper_date,
            //                              TOT_COST,
            //                              PUB_COST,
            //                              OWN_COST,
            //                              VALID_FLAG,
            //                              FEE_TIMES,
            //                              SEX_CODE,
            //                              DEPT_CODE,
            //                              IN_DATE,
            //                              BALANCE_DATE,
            //                              TYPE_CODE,
            //                              BKA438,
            //                              AAA027,
            //                              empl_type,
            //                              in_diagnose,
            //                              AAB301,
            //                              Aae140,
            //                              aka130,
            //                              bka006,
            //                              person_type                          
            //                              )
            //                              Values
            //                              (
            //                              '{0}',
            //                              '{1}',
            //                              '{2}',
            //                              '{3}',
            //                              '{4}',
            //                              '{5}',
            //                              '{6}',
            //                              '{7}',
            //                              '{8}',
            //                              '{9}',
            //                              '{10}',
            //                              '{11}',
            //                              '{12}',
            //                              to_date('{13}','YYYY-MM-DD hh24:mi:ss'),
            //                              '{14}',
            //                              '{15}',
            //                              '{16}',
            //                              '{17}',
            //                              0,
            //                              '{18}',
            //                              '{19}',
            //                              to_date('{20}','YYYY-MM-DD hh24:mi:ss'),
            //                              to_date('{21}','YYYY-MM-DD hh24:mi:ss'),
            //                              '{22}',
            //                              '{23}',
            //                              '{24}',
            //                              '{25}',
            //                              '{26}',
            //                              '{27}',
            //                              '{28}',
            //                              '{29}',
            //                              '{30}',
            //                              '{31}'  
            //                              )";
            //strSql = string.Format(strSql,
            //    patientInfo.ID,
            //    patientInfo.SIMainInfo.RegNo,
            //    nextBalanceNoStr,
            //    patientInfo.PID.PatientNO,
            //    patientInfo.PID.CardNO,
            //    patientInfo.SSN,//personInfo.MCardNo,
            //    patientInfo.Name,//personInfo.Name,
            //    patientInfo.IDCard,//personInfo.IdenNo,
            //    patientInfo.ClinicDiagnose,
            //    patientInfo.Pact.PayKind.ID,
            //    patientInfo.Pact.ID,
            //    patientInfo.Pact.Name,
            //    this.Operator.ID,
            //    patientInfo.PVisit.InTime.ToString("yyyy-MM-dd HH:mm:ss"),
            //    patientInfo.SIMainInfo.TotCost,
            //    patientInfo.SIMainInfo.PubCost,
            //    patientInfo.SIMainInfo.OwnCost,
            //    "1",
            //    patientInfo.Sex.ID,
            //    patientInfo.PVisit.PatientLocation.Dept.ID,
            //    patientInfo.PVisit.InTime.ToString("yyyy-MM-dd HH:mm:ss"),
            //    patientInfo.SIMainInfo.BalanceDate.ToString("yyyy-MM-dd HH:mm:ss"),
            //    "2",
            //    patientInfo.SIMainInfo.Bka438,
            //    patientInfo.SIMainInfo.Aaa027,
            //    //patientInfo.SIMainInfo.Disease.Memo,
            //    //patientInfo.SIMainInfo.Disease.ID,
            //    patientInfo.SIMainInfo.EmplType,
            //    patientInfo.SIMainInfo.InDiagnose.ID,
            //    patientInfo.SIMainInfo.Aab301,
            //    patientInfo.SIMainInfo.Aae140,
            //    patientInfo.SIMainInfo.Aka130,
            //    patientInfo.SIMainInfo.Bka006,
            //    patientInfo.SIMainInfo.PersonType.ID
            //    );

            //#endregion

            //try
            //{
            //    if (this.ExecNoQuery(strSql) < 0)
            //    {
            //        return -1;
            //    }
            //}
            //catch (Exception e)
            //{
            //    this.Err = e.Message;
            //    return -1;
            //}

            //return 1;

            #endregion
        }

        /// <summary>
        /// 取消住院登记
        /// </summary>
        /// <returns></returns>
        public int CancelInPatientReg(string inpatientNo, string balanceNo)
        {
            string strSql = @"update fin_ipr_siinmaininfo f 
                                 set f.valid_flag = '0', --不作废记录
                                     f.balance_state='0',
                                     f.oper_code = '{2}',
                                     f.oper_date = sysdate
                               where f.inpatient_no = '{0}' 
                                 and f.balance_no = '{1}'";

            try
            {
                strSql = string.Format(strSql, inpatientNo, balanceNo, this.Operator.ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新医保结算主表信息
        /// </summary>
        /// <returns></returns>
        public int UpdateSiMainInfoBalanceInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            #region 屏蔽代码
            //#region sql
            //            string strSql = @"UPDATE FIN_IPR_SIINMAININFO_GD_TT f
            //                                    SET BALANCE_DATE = sysdate,
            //                                      OPER_CODE = '{2}',
            //                                      OPER_DATE = sysdate,
            //                                      BALANCE_STATE = '{3}',
            //                                      TOT_COST = {4},
            //                                      PAY_COST = {5},
            //                                      PUB_COST = {6},
            //                                      OWN_COST = {7},
            //                                      BKA825 = {8},
            //                                      BKA826 = {9},
            //                                      AKA151 = {10},
            //                                      BKA838 = {11},
            //                                      AKB067 = {12},
            //                                      AKB066 = {13},
            //                                      BKA821 = {14},
            //                                      BKA839 = {15},
            //                                      AKE039 = {16},
            //                                      AKE035 = {17},
            //                                      AKE026 = {18},
            //                                      AKE029 = {19},
            //                                      BKA841 = {20},
            //                                      BKA842 = {21},
            //                                      BKA840 = {22},
            //                                      BKA438 = '2'
            //                                      where f.inpatient_no = '{0}'
            //                                      and f.balance_no = '{1}'";

            //string strSql = @"
            //                UPDATE fin_ipr_siinmaininfo_gd f
            //                   SET f.BALANCE_DATE  = sysdate,
            //                       f.OPER_CODE     = '{2}',
            //                       f.OPER_DATE     = sysdate,
            //                       f.BALANCE_STATE = '{3}',
            //                       f.TOT_COST      = {4},
            //                       f.PAY_COST      = {5},
            //                       f.PUB_COST      = {6},
            //                       f.OWN_COST      = {7},
            //                       f.BKA825        = {8},
            //                       f.BKA826        = {9},
            //                       f.AKA151        = {10},
            //                       f.BKA838        = {11},
            //                       f.AKB067        = {12},
            //                       f.AKB066        = {13},
            //                       f.BKA821        = {14},
            //                       f.BKA839        = {15},
            //                       f.AKE039        = {16},
            //                       f.AKE035        = {17},
            //                       f.AKE026        = {18},
            //                       f.AKE029        = {19},
            //                       f.BKA841        = {20},
            //                       f.BKA842        = {21},
            //                       f.BKA840        = {22},
            //                       f.BKA438        = '2',
            //                       f.invoice_no    ='{23}',
            //                       f.birthday =to_date('{24}','yyyy-mm-dd hh24:mi:ss'),
            //                       f.dept_name ='{25}',
            //                       f.bed_no='{26}',
            //                       f.out_date=to_date('{27}','yyyy-mm-dd hh24:mi:ss'),
            //                       f.out_diagnose='{28}',
            //                       f.out_diagnosename='{29}',
            //                       f.aae140='{30}',
            //                       f.bka812 = {31},
            //                       f.bka811 = {32}
            //                 where f.inpatient_no = '{0}'
            //                   and f.balance_no = '{1}'
            //                ";
            //#endregion

            //try
            //{
            //    strSql = string.Format(strSql, patientInfo.ID, patientInfo.SIMainInfo.BalNo,
            //                                    this.Operator.ID, "1",
            //                                    patientInfo.SIMainInfo.TotCost,
            //                                    patientInfo.SIMainInfo.PayCost,
            //                                    patientInfo.SIMainInfo.PubCost,
            //                                    patientInfo.SIMainInfo.OwnCost,
            //                                    patientInfo.SIMainInfo.Bka825,
            //                                    patientInfo.SIMainInfo.Bka826,
            //                                    patientInfo.SIMainInfo.Aka151,
            //                                    patientInfo.SIMainInfo.Bka838,
            //                                    patientInfo.SIMainInfo.Akb067,
            //                                    patientInfo.SIMainInfo.Akb066,
            //                                    patientInfo.SIMainInfo.Bka821,
            //                                    patientInfo.SIMainInfo.Bka839,
            //                                    patientInfo.SIMainInfo.Ake039,
            //                                    patientInfo.SIMainInfo.Ake035,
            //                                    patientInfo.SIMainInfo.Ake026,
            //                                    patientInfo.SIMainInfo.Ake029,
            //                                    patientInfo.SIMainInfo.Bka841,
            //                                    patientInfo.SIMainInfo.Bka842,
            //                                    patientInfo.SIMainInfo.Bka840,
            //                                    patientInfo.SIMainInfo.InvoiceNo,
            //                                    patientInfo.Birthday.ToString(),
            //                                    patientInfo.PVisit.PatientLocation.Dept.Name,
            //                                    patientInfo.PVisit.PatientLocation.Bed.Name,
            //                                    patientInfo.PVisit.OutTime.ToString(),
            //                                    patientInfo.SIMainInfo.OutDiagnose.Name,
            //                                    patientInfo.SIMainInfo.OutDiagnose.ID,
            //                                    patientInfo.SIMainInfo.Aae140,
            //                                    patientInfo.SIMainInfo.Bka812,
            //                                    patientInfo.SIMainInfo.Bka811);
            //}
            //catch (Exception ex)
            //{
            //    this.ErrCode = ex.Message;
            //    this.Err = ex.Message;
            //    return -1;
            //}

            //return this.ExecNoQuery(strSql);
            #endregion

            #region sql
            string strSql = @"
                            UPDATE FIN_IPR_SIINMAININFO f
                               SET f.MDTRT_ID  = '{1}', --就诊ID
                                   f.SETL_ID     =  '{2}', --结算ID
                                   f.PSN_NO     =  '{3}', --人员编号
                                   f.PSN_NAME =  '{4}', --人员姓名
                                   f.PSN_CERT_TYPE = '{5}', --人员证件类型
                                   f.CERTNO =  '{6}', --证件号码
                                   f.GEND =  '{7}', --性别
                                   f.NATY =  '{8}', --民族
                                   f.BRDY =  to_date('{9}', 'YYYY-MM-DD hh24:mi:ss'), --出生日期
                                   f.AGE =  '{10}', --年龄
                                   f.INSUTYPE =  '{11}', --险种类型
                                   f.PSN_TYPE = '{12}', --人员类别
                                   f.CVLSERV_FLAG =  '{13}', --公务员标志
                                   f.SETL_TIME = to_date('{14}', 'YYYY-MM-DD hh24:mi:ss'), --结算时间
                                   f.PSN_SETLWAY =  '{15}', --个人结算方式
                                   f.MDTRT_CERT_TYPE =  '{16}', --就诊凭证类型
                                   f.MED_TYPE =  '{17}', --医疗类别
                                   f.DISE_CODE =  '{18}', --病种编码
                                   f.DISE_NAME =  '{19}', --病种名称
                                   f.MEDFEE_SUMAMT =  '{20}', --医疗费总额
                                   f.OWNPAY_AMT =  {21}, --全自费金额
                                   f.OVERLMT_SELFPAY =  {22}, --超限价自费费用
                                   f.PRESELFPAY_AMT =  {23}, --先行自付金额
                                   f.INSCP_SCP_AMT = {24}, --符合范围金额
                                   f.MED_SUMFEE =  {25}, --医保认可费用总额
                                   f.ACT_PAY_DEDC =  {26}, --实际支付起付线
                                   f.HIFP_PAY = {27}, --基本医疗保险统筹基金支出
                                   f.POOL_PROP_SELFPAY = {28}, --基本医疗保险统筹基金支付比例
                                   f.CVLSERV_PAY = {29}, --公务员医疗补助资金支出
                                   f.HIFES_PAY = {30}, --企业补充医疗保险基金支出
                                   f.HIFMI_PAY = {31}, --居民大病保险资金支出
                                   f.HIFOB_PAY = {32}, --职工大额医疗费用补助基金支出
                                   f.HIFDM_PAY =  {33}, --伤残人员医疗保障基金支出
                                   f.MAF_PAY = {34}, --医疗救助基金支出
                                   f.OTH_PAY = {35}, --其他基金支出
                                   f.FUND_PAY_SUMAMT = {36}, --基金支付总额
                                   f.HOSP_PART_AMT =  {37}, --医院负担金额
                                   f.PSN_PART_AM = {38}, --个人负担总金额
                                   f.ACCT_PAY = {39}, --个人账户支出
                                   f.CASH_PAYAMT = {40}, --现金支付金额
                                   f.ACCT_MULAID_PAY = {41}, --账户共济支付金额
                                   f.BALC = {42}, --个人账户支出后余额
                                   f.CLR_OPTINS =  '{43}', --清算经办机构
                                   f.CLR_WAY =  '{44}', --清算方式
                                   f.CLR_TYPE =  '{45}', --清算类别
                                   f.MEDINS_SETL_ID =  '{46}', --医药机构结算ID
                                   f.VOLA_TYPE = '{47}', --违规类型
                                   f.VOLA_DSCR = '{48}', --违规说明
                                   f.VALID_FLAG = '1',   --是否有效（0 无效 1有效）
                                   f.BALANCE_STATE = '1', --是否结算（0 未结算 1 结算） 
                                   f.INVOICE_NO = '{49}', --发票号
                                   f.BALANCE_DATE = to_date('{50}', 'YYYY-MM-DD hh24:mi:ss') --结算时间
                             where f.INPATIENT_NO = '{0}'
                               and f.REG_NO = '{1}' ";
            #endregion

            try
            {
                strSql = string.Format(strSql, patientInfo.ID, //患者挂号流水号
                    patientInfo.SIMainInfo.Mdtrt_id,//就诊id
                    patientInfo.SIMainInfo.Setl_id,//结算id
                    patientInfo.SIMainInfo.Psn_no,//人员编号
                    patientInfo.SIMainInfo.Psn_name,//人员姓名
                    patientInfo.SIMainInfo.Psn_cert_type,//人员证件类型
                    patientInfo.SIMainInfo.Certno,//证件号码
                    patientInfo.SIMainInfo.Gend,//性别
                    patientInfo.SIMainInfo.Naty,//民族
                    patientInfo.SIMainInfo.Brdy,//出生日期
                    patientInfo.SIMainInfo.Age,//年龄
                    patientInfo.SIMainInfo.Insutype,//险种类型
                    patientInfo.SIMainInfo.Psn_type,//人员类别
                    patientInfo.SIMainInfo.Cvlserv_flag,//公务员标志
                    patientInfo.SIMainInfo.Setl_time,//结算时间
                    patientInfo.SIMainInfo.Psn_setlway,//个人结算方式
                    patientInfo.SIMainInfo.Mdtrt_cert_type,//就诊凭证类型
                    patientInfo.SIMainInfo.Med_type,//医疗类别
                    patientInfo.SIMainInfo.Dise_code,//病种编码
                    patientInfo.SIMainInfo.Dise_name,//病种名称
                    patientInfo.SIMainInfo.Medfee_sumamt,//医疗费总额
                    patientInfo.SIMainInfo.Ownpay_amt,//全自费金额Ownpay_amt
                    patientInfo.SIMainInfo.Overlmt_selfpay,//超限价自费费用
                    patientInfo.SIMainInfo.Preselfpay_amt,//先行自付金额
                    patientInfo.SIMainInfo.Inscp_scp_amt,//符合范围金额
                    patientInfo.SIMainInfo.Med_sumfee,//医保认可费用总额
                    patientInfo.SIMainInfo.Act_pay_dedc,//实际支付起付线
                    patientInfo.SIMainInfo.Hifp_pay,//基本医疗保险统筹基金支出
                    patientInfo.SIMainInfo.Pool_prop_selfpay,//基本医疗保险统筹基金支付比例
                    patientInfo.SIMainInfo.Cvlserv_pay,//公务员医疗补助资金支出
                    patientInfo.SIMainInfo.Hifes_pay,//企业补充医疗保险基金支出
                    patientInfo.SIMainInfo.Hifmi_pay,//居民大病保险资金支出
                    patientInfo.SIMainInfo.Hifob_pay,//职工大额医疗费用补助基金支出
                    patientInfo.SIMainInfo.Hifdm_pay,//伤残人员医疗保障基金支出
                    patientInfo.SIMainInfo.Maf_pay,//医疗救助基金支出
                    patientInfo.SIMainInfo.Oth_pay,//其他基金支出
                    patientInfo.SIMainInfo.Fund_pay_sumamt,//基金支付总额
                    patientInfo.SIMainInfo.Hosp_part_amt,//医院负担金额
                    patientInfo.SIMainInfo.Psn_part_am,//个人负担总金额
                    patientInfo.SIMainInfo.Acct_pay,//个人账户支出
                    patientInfo.SIMainInfo.Cash_payamt,//现金支付金额
                    patientInfo.SIMainInfo.Acct_mulaid_pay,//账户共济支付金额
                    patientInfo.SIMainInfo.Balc,//个人账户支出后余额
                    patientInfo.SIMainInfo.Clr_optins,//清算经办机构
                    patientInfo.SIMainInfo.Clr_way,//清算方式
                    patientInfo.SIMainInfo.Clr_type,//清算类别
                    patientInfo.SIMainInfo.Medins_setl_id,//医药机构结算id
                    patientInfo.SIMainInfo.Vola_type,//违规类型
                    patientInfo.SIMainInfo.Vola_dscr,//违规说明
                    patientInfo.SIMainInfo.InvoiceNo, //结算发票号
                    patientInfo.SIMainInfo.Setl_time  //结算时间
                );

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新医保返回的费用明细结果
        /// </summary>
        /// <returns></returns>
        public int UpdateInBalanceSIFeeDetail(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            #region sql
            string strSql = @"update gzsi_feedetail t
                                 set t.setl_id = '{1}'
                               where t.mdtrt_id = '{0}'
                                 and t.type_code = '2'";
            #endregion

            try
            {
                strSql = string.Format(strSql,
                    patientInfo.SIMainInfo.Mdtrt_id,
                    patientInfo.SIMainInfo.Setl_id
                );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 插入医保返回的费用明细结果
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="feeDetailList"></param>
        /// <returns></returns>
        public int InsertInBalanceSIFeeDetail(FS.HISFC.Models.RADT.PatientInfo patientInfo, List<API.GZSI.Models.Response.ResponseGzsiModel2301.Result> feeDetailList)
        {
            int rtn = 1;
            string sql = @"insert into gzsi_feedetail 
                                       (mdtrt_id,
                                       setl_id,
                                       feedetl_sn,
                                       det_item_fee_sumamt,
                                       cnt,
                                       pric,
                                       pric_uplmt_amt,
                                       selfpay_prop,
                                       defray_type,
                                       fulamt_ownpay_amt,
                                       overlmt_amt,
                                       preselfpay_amt,
                                       inscp_scp_amt,
                                       chrgitm_lv,
                                       med_chrgitm_type,
                                       bas_medn_flag,
                                       hi_nego_drug_flag,
                                       chld_medc_flag,
                                       list_sp_item_flag,
                                       lmt_used_flag,
                                       drt_reim_flag,
                                       memo,
                                       type_code)
                                      values
                                      ('{0}',
                                       '{1}',
                                       '{2}',
                                       {3},
                                       {4},
                                       {5},
                                       {6},
                                       {7},
                                       '{8}',
                                       {9},
                                       {10},
                                       {11},
                                       {12},
                                       '{13}',
                                       '{14}',
                                       '{15}',
                                       '{16}',
                                       '{17}',
                                       '{18}',
                                       '{19}',
                                       '{20}',
                                       '{21}',
                                       '{22}')";

            foreach (API.GZSI.Models.Response.ResponseGzsiModel2301.Result result in feeDetailList)
            {
                try
                {
                    string strSQL = string.Format(sql, patientInfo.SIMainInfo.Mdtrt_id,
                        "",
                        result.feedetl_sn,
                        FS.FrameWork.Function.NConvert.ToDecimal(result.det_item_fee_sumamt).ToString(),
                        FS.FrameWork.Function.NConvert.ToDecimal(result.cnt).ToString(),
                        FS.FrameWork.Function.NConvert.ToDecimal(result.pric).ToString(),
                        FS.FrameWork.Function.NConvert.ToDecimal(result.pric_uplmt_amt).ToString(),
                        FS.FrameWork.Function.NConvert.ToDecimal(result.selfpay_prop).ToString(),
                        result.defray_type,
                        FS.FrameWork.Function.NConvert.ToDecimal(result.fulamt_ownpay_amt).ToString(),
                        FS.FrameWork.Function.NConvert.ToDecimal(result.overlmt_amt).ToString(),
                        FS.FrameWork.Function.NConvert.ToDecimal(result.preselfpay_amt).ToString(),
                        FS.FrameWork.Function.NConvert.ToDecimal(result.inscp_scp_amt).ToString(),
                        result.chrgitm_lv,
                        result.med_chrgitm_type,
                        result.bas_medn_flag,
                        result.hi_nego_drug_flag,
                        result.chld_medc_flag,
                        result.list_sp_item_flag,
                        result.lmt_used_flag,
                        result.drt_reim_flag,
                        result.memo,
                        "2");

                    rtn = this.ExecNoQuery(strSQL);

                    if (rtn <= 0)
                    {
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 删除医保返回的费用明细结果
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int DeleteInBalanceSIFeeDetail(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            try
            {
                string sql = @" delete from gzsi_feedetail where mdtrt_id = '{0}' and type_code = '2'";
                sql = string.Format(sql, patientInfo.SIMainInfo.Mdtrt_id);
                return this.ExecNoQuery(sql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 插入住院医保收费明细
        /// </summary>
        /// <param name="feeListForUpload"></param>
        /// <returns></returns>
        public int InsertInBalanceFeeDetail(FS.HISFC.Models.RADT.PatientInfo patientInfo, ArrayList feeListForUpload)
        {
            int ret = 0;
            DateTime dateNow = this.GetDateTimeFromSysDateTime();
            ret = this.InsertZYXMData(patientInfo, feeListForUpload, dateNow);

            if (ret < 0)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 插入多条费用明细
        /// </summary>
        /// <param name="r">住院信息，包括医保患者基本信息</param>
        /// <param name="feeDetails">费用明细</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int InsertZYXMData(FS.HISFC.Models.RADT.PatientInfo patientInfo, ArrayList feeDetails, DateTime operDate)
        {
            int iReturn = 0;
            int uploadRows = 0;

            try
            {
                iReturn = DeleteZYXMData(patientInfo);
                if (iReturn < 0)
                {
                    this.Err += "删除历史费用明细失败!";
                    return -1;
                }

                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in feeDetails)
                {

                    iReturn = InsertZYXMData(patientInfo, f, operDate);
                    if (iReturn <= 0)
                    {
                        this.Err += "插入明细失败!";
                        return -1;
                    }

                    uploadRows += iReturn;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return uploadRows;
        }

        /// <summary>
        /// 插入单条费用明细
        /// </summary>
        /// <param name="r">住院信息，包括医保患者基本信息</param>
        /// <param name="f">费用明细</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int InsertZYXMData(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.FeeItemList f, DateTime operDate)
        {
            if (string.IsNullOrEmpty(patientInfo.SIMainInfo.HosNo))
            {
                patientInfo.SIMainInfo.HosNo = string.IsNullOrEmpty(HosCode) ? patientInfo.SIMainInfo.RegNo.Substring(0, 6) : HosCode;
            }

            #region 插入语句

            string strSQL = @"INSERT INTO GZSI_HIS_CFXM(
                                                JYDJH,   --就医登记号
                                                YYBH,   --医院编号
                                                GMSFHM,   --公民身份证号
                                                ZYH,   --住院号/门诊号
                                                RYRQ,   --挂号/入院时间
                                                FYRQ,   --收费时间
                                                XMXH,   --项目序号
                                                XMBH,   --医院的项目编号
                                                XMMC,   --医院的项目名称
                                                FLDM,   --分类代码
                                                YPGG,   --规格
                                                YPJX,   --剂型
                                                JG,   --单价
                                                MCYL,   --数量
                                                JE,   --金额
                                                BZ1,   --备注1，存放记录产生时间
                                                BZ2,   --备注2
                                                BZ3,   --备注3,存放费用明细读入时有效性检查的处理结果代码
                                                DRBZ,   --读入标志
                                                YPLY,   --1-国产 2-进口 3-合资
                                                PATIENT_NO,  --住院号
                                                INPATIENT_NO,  --住院流水号
                                                INVOICE_NO,     --结算发票号
                                                OPER_CODE,
                                                OPER_DATE,
                                                RECIPE_NO,
                                                SEQUENCE_NO,
                                                ITEM_CODE,
                                                FEE_CODE,
                                                jsid
                                                ) 
                                                VALUES
                                                (
                                                '{0}',   --就医登记号
                                                '{1}',   --医院编号
                                                '{2}',   --公民身份证号
                                                '{3}',   --住院号/门诊号
                                                TO_DATE('{4}','YYYY-MM-DD HH24:MI:SS'),   --挂号/入院时间
                                                TO_DATE('{5}','YYYY-MM-DD HH24:MI:SS'),   --收费时间
                                                '{6}',   --项目序号
                                                '{7}',   --医院的项目编号
                                                '{8}',   --医院的项目名称
                                                '{9}',   --分类代码
                                                '{10}',   --规格
                                                '{11}',   --剂型
                                                '{12}',   --单价
                                                '{13}',   --数量
                                                '{14}',   --金额
                                                '{15}',   --备注1，存放记录产生时间
                                                '{16}',   --备注2
                                                '{17}',   --备注3,存放费用明细读入时有效性检查的处理结果代码
                                                '{18}',   --读入标志
                                                '{19}',   --1-国产   2-进口3-合资
                                                '{20}',   --住院号
                                                '{21}',   --住院流水号
                                                '{22}',   --结算发票号
                                                '{23}',   --操作员
                                                sysdate,  --操作时间
                                                '{24}',   --处方号
                                                {25},     --处方内明细
                                                '{26}',   --HIS编码
                                                '{27}',   --HIS费用类别
                                                '{28}'    --结算序号
                                                ) ";
            #endregion

            string name = "";
            name = f.Item.Name;
            if (name.Length > 20)
            {
                name = name.Substring(0, 20);
            }
            try
            {
                decimal price = f.Item.Price;
                decimal totCost = f.SIft.OwnCost + f.SIft.PubCost + f.SIft.PayCost;

                strSQL = string.Format(strSQL,
                    patientInfo.SIMainInfo.RegNo,
                    patientInfo.SIMainInfo.HosNo,
                    patientInfo.IDCard,
                    patientInfo.PID.PatientNO,
                    patientInfo.PVisit.InTime.ToString(),
                    f.FeeOper.OperTime.ToString(),
                    f.RecipeNO + f.SequenceNO.ToString().PadLeft(3, '0'),
                    f.Item.UserCode,
                    name,
                    "0",
                    f.Item.Specs,
                    "",
                    FS.FrameWork.Public.String.FormatNumber(price / f.Item.PackQty, 4),
                    f.Item.Qty,                 //总量Amount
                    totCost,
                    operDate.ToString(),
                    "",
                    "",
                    "0",
                    "",
                    patientInfo.PID.PatientNO,
                    patientInfo.ID,
                    patientInfo.SIMainInfo.InvoiceNo,
                    this.Operator.ID,
                    f.RecipeNO,
                    f.SequenceNO.ToString(),
                    f.Item.ID,
                    f.Item.MinFee.ID,
                    patientInfo.ID + patientInfo.SIMainInfo.BalNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 删除费用明细
        /// </summary>
        /// <param name="jsid">结算序号</param>
        /// <returns>-1错误 0 1正确</returns>
        public int DeleteZYXMData(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            string jsid = patientInfo.ID + patientInfo.SIMainInfo.BalNo;
            if (string.IsNullOrEmpty(jsid))
            {
                this.Err = "DeleteZYXMData方法参数错误。";
                return -1;
            }
            string strSQL = string.Format("delete from GZSI_HIS_CFXM t where t.jsid='{0}'", jsid);

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 插入门诊医保结算基金分项
        /// </summary>
        /// <param name="r"></param>
        /// <param name="setlDetailList"></param>
        /// <returns></returns>
        public int InsertInBalanceSetldetail(FS.HISFC.Models.RADT.PatientInfo patientInfo, List<Models.Setldetail> setlDetailList)
        {
            string sqlStr = @"INSERT INTO GZSI_SETLDETAIL 
                                          (MDTRT_ID,  --就诊ID
                                           SETL_ID,  --结算ID
                                           FUND_PAY_TYPE, --基金支付类型
                                           FUN_PAY_TYPE_NAME, --基金支付名称
                                           INSCP_SCP_AMT, --符合范围金额
                                           CRT_PAYB_LMT_AMT, --本次可支付限额金额
                                           FUND_PAYAMT, --基金支付金额
                                           SETL_PROC_INFO, --结算过程信息
                                           TYPE_CODE) --就诊类型
                                    VALUES ('{0}',
                                           '{1}',
                                           '{2}',
                                           '{3}',
                                           {4},
                                           {5},
                                           {6},
                                           '{7}',
                                           '2')";

            try
            {
                foreach (Models.Setldetail setl in setlDetailList)
                {
                    string sql = string.Format(sqlStr, patientInfo.SIMainInfo.Mdtrt_id,  //就诊ID
                                                      patientInfo.SIMainInfo.Setl_id, //结算ID
                                                      setl.fund_pay_type, //基金支付类型
                                                      setl.fund_pay_type_name, //基金支付名称
                                                      setl.inscp_scp_amt,//符合范围金额
                                                      setl.crt_payb_lmt_amt, //本次可支付限额金额
                                                      setl.fund_payamt, //基金支付金额
                                                      setl.setl_proc_info); //结算过程信息

                    int ret = this.ExecNoQuery(sql);

                    if (ret < 1)
                    {
                        this.ErrCode = "1";
                        this.Err = "插入结算基金分项出错";
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 插入门诊医保结算违规费用明细
        /// </summary>
        /// <param name="r"></param>
        /// <param name="setlDetailList"></param>
        /// <returns></returns>
        public int InsertInBalanceVoladetail(FS.HISFC.Models.RADT.PatientInfo patientInfo, List<Models.Voladetail> volaDetailList)
        {
            string sqlStr = @"INSERT INTO GZSI_VOLADETAIL
                                          (MDTRT_ID,  --就诊ID
                                           SETL_ID, --结算ID
                                           MEDINS_LIST_CODG, --医疗机构目录编码
                                           MEDINS_LIST_NAME, --医疗机构目录名称
                                           MED_LIST_CODG, --医疗目录编码
                                           VOLA_TYPE, --违规类型
                                           VOLA_DSCR, --违规说明
                                           TYPE_CODE) --就诊类型
                                   VALUES ('{0}',
                                           '{1}',
                                           '{2}',
                                           '{3}',
                                           '{4}',
                                           '{5}',
                                           '{6}',
                                           '2') ";

            try
            {
                foreach (Models.Voladetail vola in volaDetailList)
                {
                    string sql = string.Format(sqlStr, patientInfo.SIMainInfo.Mdtrt_id,  //就诊ID
                                                      patientInfo.SIMainInfo.Setl_id, //结算ID
                                                      vola.medins_list_codg, //医疗机构目录编码
                                                      vola.medins_list_name, //医疗机构目录名称
                                                      vola.med_list_codg,//医疗目录编码
                                                      vola.vola_type, //违规类型
                                                      vola.vola_dscr); //违规说明

                    int ret = this.ExecNoQuery(sql);

                    if (ret < 1)
                    {
                        this.ErrCode = "1";
                        this.Err = "插入结算违规费用明细出错";
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 取消结算、更新医保结算主表信息
        /// </summary>
        /// <returns></returns>
        public int CancelInPatientBalance(string inpatientNo, string balanceNo)
        {
            string strSql = @"update fin_ipr_siinmaininfo f 
                                 set --f.valid_flag = '0', --不作废记录
                                     f.balance_state='0',
                                     f.oper_code = '{2}',
                                     f.oper_date = sysdate
                               where f.inpatient_no = '{0}' 
                                 and f.balance_no = '{1}'";

            try
            {
                strSql = string.Format(strSql, inpatientNo, balanceNo, this.Operator.ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }


        /// <summary>
        /// 获取住院出院诊断信息
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public ArrayList InpatientDiagnoseList(string inpatientNo)
        {
            ArrayList obj = new ArrayList();
            string strSql = @" select t.入院主要诊断编码 diag_code,
                                      t.入院主要诊断 diag_name
                                 from disease_API@Emr1_Dblink t
                                where t.HIS内部标识 = '{0}'";
            strSql = string.Format(strSql, inpatientNo);

            try
            {
                if (ExecQuery(strSql) == -1)
                {
                    return null;
                }
                while (Reader.Read())
                {
                    if (string.IsNullOrEmpty(Reader[0].ToString()))
                    {
                        continue;
                    }

                    FS.HISFC.Models.RADT.Diagnose diag = new FS.HISFC.Models.RADT.Diagnose();
                    diag.ID = Reader[0].ToString();//诊断编码
                    diag.Name = Reader[1].ToString();//诊断名称
                    diag.ICD10.ID = Reader[0].ToString();//诊断编码
                    diag.ICD10.Name = Reader[1].ToString();//诊断名称
                    obj.Add(diag);
                }
                Reader.Close();
            } //抛出错误
            catch (Exception ex)
            {
                Err = "取诊断信息出错！" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
            return obj;
        }

        #region 未使用

        /// <summary>
        /// 查询患者费用明细(按执行时间)
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int QueryfeeDetailsForUseTime(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            feeDetails.Clear();
            int result = 0;

            #region 查询赋值

            #region 修改收费时间为执行时间按数量汇总
            string strSql = @"
                            SELECT TAB.SEQ_NO,
                                   TAB.ITEM_CODE,
                                   TAB.ITEM_NAME,
                                   TAB.UNIT_PRICE,
                                   sum(TAB.QTY) QTY,
                                   sum(TAB.TOT_COST) TOT_COST,
                                   TAB.EMPL_NAME,
                                   TAB.IDENNO,
                                   TAB.EMPL_CODE,
                                   TAB.charge_date,
                                   TAB.extend3,
                                   (select u.input_code from fin_com_undruginfo u where u.item_code=TAB.ITEM_CODE ) center_code,
                                   (select u.item_name from fin_com_undruginfo u where u.item_code=TAB.ITEM_CODE ) center_name,--f.center_name
                                    RangeFlag
                              FROM (SELECT I.Recipe_No||I.Sequence_No SEQ_NO,
                                           I.INPATIENT_NO,
                                           I.ITEM_CODE,
                                           I.ITEM_NAME,
                                           I.UNIT_PRICE,
                                           I.QTY,
                                           I.TOT_COST,
                                           decode(E.EMPL_TYPE,
                                                  'D',
                                                  E.EMPL_NAME,
                                                  (SELECT c.EMPL_NAME
                                                     FROM COM_EMPLOYEE c
                                                    WHERE c.EMPL_CODE = r.HOUSE_DOC_CODE)) EMPL_NAME,
                                           decode(E.EMPL_TYPE,
                                                  'D',
                                                  E.IDENNO,
                                                  (SELECT c.IDENNO
                                                     FROM COM_EMPLOYEE c
                                                    WHERE c.EMPL_CODE = r.HOUSE_DOC_CODE)) IDENNO,
                                           decode(E.EMPL_TYPE, 'D', E.EMPL_CODE, r.HOUSE_DOC_CODE) EMPL_CODE,
                                           trunc(nvl((select (case u.undrug_code when 'F00000032101'then u.charge_date
                                                                                 when 'F00000026381' then u.charge_date
                                                                                   else u.use_time end) usetime--药袋要用费用发生日期
                                                       from met_ipm_execundrug u
                                                      where u.inpatient_no = i.inpatient_no
                                                        and u.mo_order = i.mo_order
                                                        and u.exec_sqn = i.mo_exec_sqn),
                                                     i.charge_date)) charge_date,
                                           '0' as extend3,
                                           r.HOUSE_DOC_CODE HOUSE_DOC_CODE,'' RangeFlag
                                      FROM FIN_IPB_ITEMLIST I, COM_EMPLOYEE E, FIN_IPR_INMAININFO R
                                     WHERE E.EMPL_CODE = I.RECIPE_DOCCODE
                                       AND I.INPATIENT_NO = R.INPATIENT_NO
                                       AND I.BALANCE_STATE = '0'
                                       AND R.INPATIENT_NO = '{0}'
                                       AND I.SPLIT_FEE_FLAG <> '1'
                                       AND NOT EXISTS (SELECT NULL
                                              FROM FIN_IPB_ITEMLIST O
                                             WHERE O.EXT_CODE = I.RECIPE_NO
                                               AND O.SEQUENCE_NO = I.SEQUENCE_NO
                                               AND O.TRANS_TYPE = '2'
                                               AND O.INPATIENT_NO = I.INPATIENT_NO)
                                       AND I.TRANS_TYPE = '1') TAB
                            
                             GROUP BY TAB.INPATIENT_NO,
                                      TAB.ITEM_CODE,
                                      TAB.ITEM_NAME,
                                      TAB.UNIT_PRICE,
                                      TAB.HOUSE_DOC_CODE,
                                      TAB.EMPL_CODE,
                                      TAB.EMPL_NAME,
                                      TAB.IDENNO,
                                      TAB.SEQ_NO,
                                      TAB.charge_date,
                                      TAB.extend3,RangeFlag
                            HAVING sum(TAB.TOT_COST) != 0 
                            UNION ALL
                            SELECT M.Recipe_No||M.Sequence_No AS SEQ_NO,
                                   M.DRUG_CODE,
                                   M.DRUG_NAME,
                                   ROUND(M.UNIT_PRICE / M.PACK_QTY, 2) AS UNIT_PRICE,
                                   sum(M.QTY) QTY,
                                   sum(M.TOT_COST) TOT_COST,
                                   decode(EE.EMPL_TYPE,
                                          'D',
                                          EE.EMPL_NAME,
                                          (SELECT c.EMPL_NAME
                                             FROM COM_EMPLOYEE c
                                            WHERE c.EMPL_CODE = RR.HOUSE_DOC_CODE)) EMPL_NAME,
                                   decode(EE.EMPL_TYPE,
                                          'D',
                                          EE.IDENNO,
                                          (SELECT c.IDENNO
                                             FROM COM_EMPLOYEE c
                                            WHERE c.EMPL_CODE = RR.HOUSE_DOC_CODE)) IDENNO,
                                   decode(EE.EMPL_TYPE, 'D', EE.EMPL_CODE, RR.HOUSE_DOC_CODE) EMPL_CODE,
                                   trunc(m.charge_date) fee_date,
                                   nvl(oet.extend3,'0'),
                                   (select p.custom_code from pha_com_baseinfo p where p.drug_code=m.drug_code) center_code,
                                   (select p.trade_name from pha_com_baseinfo p where p.drug_code=m.drug_code) center_name,
                                   m.RangeFlag
                              FROM COM_EMPLOYEE EE, FIN_IPR_INMAININFO RR,FIN_IPB_MEDICINELIST M
                              left join (select distinct oe.extend3,
                                                         oe.inpatient_no,
                                                         oe.mo_order,
                                                         b.custom_code
                                           from met_ipm_orderextend oe,
                                                --met_ipm_order       oi,
                                                fin_ipb_medicinelist oi,
                                                pha_com_baseinfo     b,
                                                com_dictionary       dc
                                          where oe.mo_order = oi.mo_order
                                            and b.drug_code = oi.drug_code
                                            and dc.type = 'ZHUHAILIMITDRUG'
                                            and dc.code = b.custom_code) oet
                                on (oet.inpatient_no = m.inpatient_no and oet.mo_order = m.mo_order)
                             WHERE EE.EMPL_CODE = M.RECIPE_DOCCODE
                               AND M.INPATIENT_NO = RR.INPATIENT_NO
                               AND M.BALANCE_STATE = '0'
                               AND M.Noback_Num<>0
                               AND RR.INPATIENT_NO = '{0}'
                             GROUP BY M.INPATIENT_NO,
                                      m.DRUG_CODE,
                                      m.DRUG_NAME,
                                      m.UNIT_PRICE,
                                      m.PACK_QTY,
                                      rr.HOUSE_DOC_CODE,
                                      ee.EMPL_CODE,
                                      ee.EMPL_NAME,
                                      ee.IDENNO,
                                      ee.EMPL_TYPE,
                                      m.charge_date,
                                      M.Recipe_No,
                                      M.Sequence_No,
                                      oet.extend3,
                                      m.RangeFlag
                            HAVING sum(M.TOT_COST) != 0 
                            ";
            #endregion

            strSql = string.Format(strSql, patient.ID);

            try
            {
                int intFlag = this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo feeItem = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();

                    feeItem.RecipeNO = this.Reader[0].ToString();
                    feeItem.Item.ID = this.Reader[1].ToString();
                    feeItem.Item.Name = this.Reader[2].ToString();
                    feeItem.Item.Price = Convert.ToDecimal(this.Reader[3].ToString());
                    feeItem.Item.Qty = Convert.ToDecimal(this.Reader[4].ToString());
                    feeItem.FT.TotCost = Convert.ToDecimal(this.Reader[5].ToString());

                    //医生信息
                    feeItem.RecipeOper.Name = this.Reader[6].ToString();     //医生姓名
                    feeItem.RecipeOper.Memo = this.Reader[7].ToString();     //医生身份证号
                    feeItem.RecipeOper.ID = this.Reader[8].ToString();       //医生工号
                    feeItem.ExecOrder.ExecOper.OperTime = DateTime.Parse(this.Reader[9].ToString());
                    feeItem.Compare.SpellCode.UserCode = this.Reader[11].ToString();
                    feeItem.RangeFlag = this.Reader[13].ToString();
                    feeDetails.Add(feeItem);
                    result = 1;
                }

            }
            catch (Exception e)
            {
                result = -1;
            }
            #endregion

            return result;
        }

        /// <summary>
        /// 更新fin_ipb_feeinfo的owmCost
        /// </summary>
        /// <returns></returns>
        public int UpdateFeeinfoOwnCost(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            string strSql = @"update fin_ipb_feeinfo t 
                                 set t.own_cost = t.tot_cost 
                               where t.inpatient_no='{0}'
                                 and t.tot_cost<>t.own_cost ";

            try
            {
                strSql = string.Format(strSql, patientInfo.ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新fin_ipb_itemlist的ownCost
        /// </summary>
        /// <returns></returns>
        public int UpdateItemlistOwnCost(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            string strSql = @"update fin_ipb_itemlist t 
                                 set t.own_cost = t.tot_cost 
                               where t.inpatient_no='{0}'
                                 and t.tot_cost<>t.own_cost ";

            try
            {
                strSql = string.Format(strSql, patientInfo.ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新fin_ipb_medicinelist的owmCost
        /// </summary>
        /// <returns></returns>
        public int UpdateMedicinelistOwnCost(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            string strSql = @"update fin_ipb_medicinelist t 
                                 set t.own_cost = t.tot_cost 
                               where t.inpatient_no='{0}'
                                 and t.tot_cost<>t.own_cost ";

            try
            {
                strSql = string.Format(strSql, patientInfo.ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        #endregion

        #region 病案首页信息

        public List<FS.HISFC.Models.RADT.PatientInfo> GetPatientForUpload(DateTime beginDate, DateTime endDate, string patientNO, string name)
        {
            #region 查询语句

            string strSql = @"select t1.inpatient_no, -- 0住院流水号
                                     t1.reg_no, -- 1就医登记号
                                     t1.fee_times, -- 2费用批次
                                     t1.balance_no, -- 3 结算序号
                                     t1.invoice_no, -- 4 主发票号
                                     t1.medical_type, -- 5 医疗类别
                                     t1.patient_no, -- 6 住院号
                                     t1.card_no, -- 7 卡号
                                     t1.mcard_no, -- 8 医疗证号
                                     t1.app_no, -- 9 审批号
                                     t1.procreate_pcno, -- 10 生育保险患者电脑号
                                     t1.si_begindate, -- 11 参保日期
                                     t1.si_state, -- 12 参保状态
                                     t1.name, -- 13 姓名
                                     t1.sex_code, -- 14 性别
                                     t1.idenno, -- 15 身份证号码
                                     t1.spell_code, -- 16 拼音码
                                     t1.birthday, -- 17 生日
                                     t1.empl_type, -- 18 人员类型
                                     t1.work_name, -- 19 工作单位
                                     t1.clinic_diagnose, -- 20 门诊诊断
                                     t1.dept_code, -- 21 科室编码
                                     t1.dept_name, -- 22 科室名称
                                     t1.paykind_code, -- 23 结算类别
                                     t1.pact_code, --  24 合同代码
                                     t1.pact_name, -- 25 合同单位名称
                                     t1.bed_no, -- 26 床号
                                     t1.in_date, -- 27 入院日期
                                     t1.in_diagnosedate, --28 入院诊断日期
                                     t1.in_diagnose, -- 29 入院诊断编码
                                     t1.in_diagnosename, -- 30 入院诊断名称
                                     t2.out_date, -- 31 出院时间
                                     t1.out_diagnose, -- 32 出院诊断编码
                                     t1.out_diagnosename, -- 33 出院诊断名称
                                     t1.balance_date, -- 34 结算日期
                                     t1.tot_cost, -- 35 总金额
                                     t1.pay_cost, -- 36 帐户支付
                                     t1.pub_cost, -- 37 社保支付金额
                                     t1.item_paycost, -- 38 部分项目自付金额
                                     t1.base_cost, -- 39 个人起付金额
                                     t1.item_paycost2, -- 40 个人自费项目金额
                                     t1.item_ylcost, -- 41 个人自付金额（乙类自付部分）
                                     t1.own_cost, -- 42 个人自负金额
                                     t1.overtake_owncost,-- 43 超统筹支付限额个人自付金额
                                     t1.hos_cost, -- 44 医药机构分担金额(中山医保民政统筹金额)
                                     t1.own_cause, -- 45 自费原因
                                     t1.oper_code, -- 46 操作员编码
                                     t1.oper_date, -- 47 操作时间
                                     t1.year_cost, -- 48 本年度可用定额
                                     t1.valid_flag, -- 49 是否有效
                                     t1.balance_state, -- 50 是否结算
                                     t1.individualbalance, -- 51 个人账户余额
                                     t1.freezemessage, -- 52 冻结信息
                                     t1.applysequence, -- 53 申请序号
                                     t1.applytypeid, -- 54 申请类型编号
                                     t1.applytypename, -- 55 申请类型
                                     t1.fundid, -- 56 基金编码
                                     t1.fundname, -- 57 基金名称
                                     t1.businesssequence, -- 58 业务序列号
                                     t1.invoice_seq, -- 59 发票序号
                                     t1.over_cost, -- 60 医保大额补助
                                     t1.official_cost, -- 61 医保公务员补助
                                     t1.remark, -- 62 医保信息（PersonAccountInfo）
                                     t1.type_code, -- 63 人员类型
                                     t1.trans_type, -- 64 交易类型
                                     t1.person_type, -- 65 人员类型
                                     t1.diagnose_oper_code, -- 66 操作员
                                     t1.operatecode1, --67
                                     t1.operatecode2, -- 68
                                     t1.operatecode3, -- 69
                                     t1.primarydiagnosename, -- 70
                                     t1.primarydiagnosecode, -- 71
                                     t1.ybmedno, -- 72 居民医保结算单号,保存以做为 注销居民门诊费用结算 参数之一
                                     t1.trans_no, -- 73 上传号                                 --东莞医保
                                     t1.internal_fee, -- 74 普通医保内费用                  --东莞医保
                                     t1.external_fee, --75 external_fee 75 普通医保外费用                   --东莞医保
                                     t1.official_own_cost, -- 76 大额/公务员自付金额              --东莞医保
                                     t1.over_inte_fee, -- 77 本次交易统筹封顶后医保内金额    --东莞医保
                                     t1.own_count_fee, -- 78  个人应付总金额(个人帐户支付+现金)    --东莞医保
                                     t1.own_second_count_fee, -- 79 个人自付二金额    --东莞医保
                                     t1.si_diagnose, -- 80 医保诊断代码    --东莞医保
                                     t1.si_diagnosename, -- 81 医保诊断代码名称     --东莞医保
                                     t1.pub_fee_cost, -- 82 统筹支付金额     --东莞医保
                                     t1.ext_flag, -- 83 深圳医保的执行状态 登记： R 上传 ：S 结算：B 支付：J
                                     t1.ext_flag1, -- 84 异地医保的参保地址
                                     t1.zhuhaisitype, -- 85 参保险种(珠海医保)
                                     t1.gzsiupload, -- 86 广州医保上传状态 未上传：0， 已上传 1
                                     t1.mdtrt_id, -- 87 就诊ID
                                     t1.setl_id, -- 88 结算ID
                                     t1.psn_no, -- 89 人员编号
                                     t1.psn_name, -- 90 人员姓名
                                     t1.psn_cert_type, -- 91 人员证件类型
                                     t1.certno, -- 92 证件号码
                                     t1.gend, -- 93 性别
                                     t1.naty, -- 94 民族
                                     t1.brdy, -- 95 生日
                                     t1.age, -- 96 年龄
                                     t1.insutype, -- 97 险种类型
                                     t1.psn_type, -- 98 人员类别
                                     t1.cvlserv_flag, -- 99 公务员标识
                                     t1.setl_time, -- 100 结算时间
                                     t1.psn_setlway, -- 101 个人结算方式
                                     t1.mdtrt_cert_type, -- 102 就诊凭证类型
                                     t1.med_type, -- 103 医疗类别
                                     t1.dise_code, -- 104 病种编码
                                     t1.dise_name, -- 105 病种名称
                                     t1.medfee_sumamt, -- 106 医疗费总额
                                     t1.ownpay_amt, -- 107 全自费金额
                                     t1.overlmt_selfpay, -- 108 超限价自费费用
                                     t1.preselfpay_amt, -- 109 先行自付金额
                                     t1.inscp_scp_amt, -- 110 符合范围金额
                                     t1.med_sumfee, -- 111 医保认可费用总额
                                     t1.act_pay_dedc, -- 112 实际支付起付线
                                     t1.hifp_pay, -- 113 基本医疗保险统筹基金支出
                                     t1.pool_prop_selfpay, -- 114 基本医疗保险统筹基金支付比例
                                     t1.cvlserv_pay, -- 115 公务员医疗补助资金支出
                                     t1.hifes_pay, -- 116 企业补充医疗保险基金支出
                                     t1.hifmi_pay, -- 117 居民大病保险资金支出
                                     t1.hifob_pay, -- 118 职工大额医疗费用补助基金支出
                                     t1.hifdm_pay, -- 119 伤残人员医疗保障基金支出
                                     t1.maf_pay, -- 120 医疗救助基金支出
                                     t1.oth_pay, -- 121 其他基金支出
                                     t1.fund_pay_sumamt, -- 122 基金支付总额
                                     t1.hosp_part_amt, -- 123 医院负担金额
                                     t1.psn_part_am, -- 124 个人负担总金额
                                     t1.acct_pay, -- 125 个人账户支出
                                     t1.cash_payamt, -- 126 现金支付金额
                                     t1.acct_mulaid_pay, -- 127 账户共济支付金额
                                     t1.balc, -- 128 个人账户支出后余额
                                     t1.clr_optins, -- 129 清算经办机构
                                     t1.clr_way, --130 清算方式
                                     t1.clr_type, -- 131 清算类别
                                     t1.medins_setl_id, -- 132 医药机构结算ID
                                     t1.vola_type, -- 133 违规类型
                                     t1.vola_dscr, -- 134 违规说明
                                     (select t.invoicecode from com_opb_eleinvoiceopeninfo t where t.status=2 and t1.inpatient_no= t.INPATIENTNOORINVOICENO and t1.medfee_sumamt=t.EXTAXAMOUNT and rownum=1) invoicecode , --发票代码
                                     (select t.invoiceno from com_opb_eleinvoiceopeninfo t where t.status=2 and t1.inpatient_no=t.INPATIENTNOORINVOICENO and t1.medfee_sumamt=t.EXTAXAMOUNT and rownum=1) invoiceno   --发票号码
                                from fin_ipr_siinmaininfo t1, fin_ipr_inmaininfo t2
                               where (t1.patient_no = '{0}' or 'ALL' = 'ALL')
                                 and (t1.name = '{1}' or 'ALL' = 'ALL')
                                 and t1.valid_flag = '1'
                                 and t1.type_code = '2'
                                 and t1.inpatient_no = t2.inpatient_no
                                 and t2.out_date > to_date('{2}', 'YYYY-MM-DD')
                                 and t2.out_date < to_date('{3}', 'YYYY-MM-DD')
                                 and t2.in_state <> 'N'
                                 and t1.balance_state = '1'
                               order by t2.out_date, t1.name";

            #endregion

            string sql = string.Format(strSql, patientNO, name, beginDate.ToShortDateString(), endDate.ToShortDateString());

            this.ExecQuery(sql);

            try
            {
                List<FS.HISFC.Models.RADT.PatientInfo> patientList = new List<FS.HISFC.Models.RADT.PatientInfo>();

                while (Reader.Read())
                {
                    FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
                    this.setInpatientInfo(ref patient);
                    patientList.Add(patient);
                }

                return patientList;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                Reader.Close();
                return null;
            }
        }

        /// <summary>
        /// 获取病案首页基本信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetBasyBaseInfo(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select mdtrt_sn,
                                   mdtrt_id,
                                   psn_no,
                                   patn_ipt_cnt,
                                   ipt_otp_no,
                                   medcasno,
                                   psn_name,
                                   gend,
                                   brdy,
                                   ntly,
                                   ntly_name,
                                   nwb_bir_wt,
                                   nwb_adm_wt,
                                   birplc,
                                   napl,
                                   naty,
                                   naty_name,
                                   certno,
                                   prfs,
                                   mrg_stas,
                                   mrg_name,
                                   curr_addr_poscode,
                                   curr_addr,
                                   psn_tel,
                                   resd_addr_prov,
                                   resd_addr_city,
                                   resd_addr_coty,
                                   resd_addr_subd,
                                   resd_addr_vil,
                                   resd_addr_housnum,
                                   resd_addr_poscode,
                                   resd_addr,
                                   empr_tel,
                                   empr_poscode,
                                   empr_addr,
                                   coner_tel,
                                   coner_name,
                                   coner_addr,
                                   coner_rlts_code,
                                   adm_way_code,
                                   adm_way_name,
                                   trt_type,
                                   trt_type_name,
                                   adm_caty,
                                   adm_ward,
                                   adm_date,
                                   dscg_date,
                                   dscg_caty,
                                   refldept_caty_name,
                                   dscg_ward,
                                   ipt_days,
                                   drug_dicm_flag,
                                   dicm_drug_name,
                                   die_autp_flag,
                                   abo_code,
                                   abo_name,
                                   rh_code,
                                   rh_name,
                                   die_code,
                                   deptdrt_name,
                                   chfdr_name,
                                   atddr_name,
                                   chfpdr_name,
                                   ipt_dr_name,
                                   resp_nurs_name,
                                   train_dr_name,
                                   intn_dr_name,
                                   codr_name,
                                   qltctrl_dr_name,
                                   qltctrl_nurs_name,
                                   medcas_qlt_name,
                                   medcas_qlt_code,
                                   medcas_qlt_date,
                                   dscg_way_name,
                                   dscg_way,
                                   acp_medins_code,
                                   acp_medins_name,
                                   dscg_31days_rinp_flag,
                                   dscg_31days_rinp_pup,
                                   damg_intx_ext_rea,
                                   damg_intx_ext_rea_disecode,
                                   brn_damg_bfadm_coma_dura,
                                   brn_damg_afadm_coma_dura,
                                   vent_used_time,
                                   cnfm_date,
                                   patn_dise_diag_crsp,
                                   patn_dise_diag_crsp_code,
                                   ipt_patn_diag_inscp,
                                   ipt_patn_diag_inscp_code,
                                   dscg_trt_rslt,
                                   dscg_trt_rslt_code,
                                   medins_orgcode,
                                   age,
                                   aise,
                                   pote_intn_dr_name,
                                   hbsag,
                                   hcv_ab,
                                   hiv_ab,
                                   resc_cnt,
                                   resc_succ_cnt,
                                   hosp_dise_fsttime,
                                   hif_pay_way_name,
                                   hif_pay_way_code,
                                   med_fee_paymtd_name,
                                   medfee_paymtd_code,
                                   selfpay_amt,
                                   medfee_sumamt,
                                   ordn_med_servfee,
                                   ordn_trt_oprt_fee,
                                   nurs_fee,
                                   com_med_serv_oth_fee,
                                   palg_diag_fee,
                                   lab_diag_fee,
                                   rdhy_diag_fee,
                                   clnc_dise_fee,
                                   nsrgtrt_item_fee,
                                   clnc_phys_trt_fee,
                                   rgtrt_trt_fee,
                                   anst_fee,
                                   rgtrt_fee,
                                   rhab_fee,
                                   tcm_trt_fee,
                                   wm_fee,
                                   abtl_medn_fee,
                                   tcmpat_fee,
                                   tcmherb_fee,
                                   blo_fee,
                                   albu_fee,
                                   glon_fee,
                                   clotfac_fee,
                                   cyki_fee,
                                   exam_dspo_matl_fee,
                                   trt_dspo_matl_fee,
                                   oprn_dspo_matl_fee,
                                   oth_fee,
                                   vali_flag
                              from v_basy_baseinfo
                             where mdtrt_id = '{0}'";
            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            System.Data.DataSet ds = new System.Data.DataSet();

            try
            {
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 病案首页诊断信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetBasyDiseInfo(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select reg_no,
                                   palg_no,
                                   ipt_patn_disediag_type_code,
                                   disediag_type,
                                   maindiag_flag,
                                   diag_code,
                                   diag_name,
                                   inhosp_diag_code,
                                   inhosp_diag_name,
                                   adm_dise_cond_name,
                                   adm_dise_cond_code,
                                   adm_cond,
                                   adm_cond_code,
                                   high_diag_evid,
                                   bkup_deg,
                                   bkup_deg_code,
                                   vali_flag
                              from v_basy_diseinfo
                              where reg_no = '{0}'";
            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 病案首页手术信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetBasyOprnInfo(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select reg_no,
                                   oprn_oprt_date,
                                   oprn_oprt_name,
                                   oprn_oprt_code,
                                   oprn_oprt_sn,
                                   oprn_lv_code,
                                   oprn_lv_name,
                                   oper_name,
                                   asit_1_name,
                                   asit_name2,
                                   sinc_heal_lv,
                                   sinc_heal_lv_code,
                                   anst_mtd_name,
                                   anst_mtd_code,
                                   anst_dr_name,
                                   oprn_oper_part,
                                   oprn_oper_part_code,
                                   oprn_con_time,
                                   anst_lv_name,
                                   anst_lv_code,
                                   oprn_patn_type,
                                   oprn_patn_type_code,
                                   main_oprn_flag,
                                   anst_asa_lv_code,
                                   anst_asa_lv_name,
                                   anst_medn_code,
                                   anst_medn_name,
                                   anst_medn_dos,
                                   unt,
                                   anst_begntime,
                                   anst_endtime,
                                   anst_copn_code,
                                   anst_copn_name,
                                   anst_copn_dscr,
                                   pacu_begntime,
                                   pacu_endtime,
                                   canc_oprn_flag,
                                   vali_flag
                              from v_basy_oprninfo
                             where reg_no = '{0}'";
            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 病案首页ICU信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetBasyICUInfo(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql

            string strSql = @"select reg_no,
                                   icu_codeid,
                                   inpool_icu_time,
                                   out_icu_time,
                                   medins_orgcode,
                                   nurscare_lv_code,
                                   nurscare_lv_name,
                                   nurscare_days,
                                   back_icu,
                                   vali_flag
                              from v_basy_icuinfo
                             where reg_no = '{0}'";

            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        #endregion

        #region 医嘱信息

        public int GetOrerInfo(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql

            string strSql = @"select mdtrt_sn,
                                   mdtrt_id,
                                   psn_no,
                                   ipt_bedno,
                                   drord_no,
                                   isu_dept_code,
                                   drord_isu_no,
                                   exe_dept_code,
                                   exedept_name,
                                   drord_chker_name,
                                   drord_ptr_name,
                                   drord_grpno,
                                   drord_type,
                                   drord_item_type,
                                   drord_item_name,
                                   drord_detl_code,
                                   drord_detl_name,
                                   medn_type_code,
                                   medn_type_name,
                                   drug_dosform,
                                   drug_dosform_name,
                                   drug_spec,
                                   dismed_cnt,
                                   dismed_cnt_unt,
                                   medn_use_frqu,
                                   medn_used_dosunt,
                                   drug_used_sdose,
                                   drug_used_idose,
                                   drug_used_way_code,
                                   drug_used_way,
                                   medc_days,
                                   medc_begntime,
                                   medc_endtime,
                                   skintst_dicm,
                                   tcmherb_foote,
                                   drord_endtime,
                                   ipt_dept_code,
                                   medins_orgcode,
                                   unif_purc_drug_flag,
                                   drug_mgt_plaf_code,
                                   drug_purc_code,
                                   bas_medn_flag,
                                   vali_flag,
                                   medcas_drord_detl_id
                            from v_gzsi_orderinfo
                            where mdtrt_id = '{0}'";

            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        #endregion

        #region 病历信息

        /// <summary>
        /// 入院记录
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetAdminfo(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql

            string strSql = @"select mdtrt_id,
                                   psn_no,
                                   mdtrt_sn,
                                   mdtrtsn,
                                   name,
                                   gend,
                                   age,
                                   adm_rec_no,
                                   psn_adm_no,
                                   wardarea_name,
                                   dept_code,
                                   dept_name,
                                   bedno,
                                   adm_time,
                                   illhis_stte_name,
                                   illhis_stte_rltl,
                                   stte_rele,
                                   chfcomp,
                                   dise_now,
                                   hlcon,
                                   dise_his,
                                   ifet,
                                   ifet_his,
                                   prev_vcnt,
                                   oprn_his,
                                   bld_his,
                                   algs_his,
                                   psn_his,
                                   mrg_his,
                                   mena_his,
                                   fmhis,
                                   physexm_tprt,
                                   physexm_pule,
                                   physexm_vent_frqu,
                                   physexm_systolic_pre,
                                   physexm_dstl_pre,
                                   physexm_height,
                                   physexm_wt,
                                   physexm_ordn_stas,
                                   physexm_skin_musl,
                                   physexm_spef_lymph,
                                   physexm_head,
                                   physexm_neck,
                                   physexm_chst,
                                   physexm_abd,
                                   physexm_finger_exam,
                                   physexm_genital_area,
                                   physexm_spin,
                                   physexm_all_fors,
                                   nersys,
                                   spcy_info,
                                   asst_exam_rslt,
                                   tcm4d_rslt,
                                   syddclft,
                                   syddclft_name,
                                   prnp_trt,
                                   rec_doc_code,
                                   rec_doc_name,
                                   ipdr_code,
                                   ipdr_name,
                                   chfdr_code,
                                   chfdr_name,
                                   chfpdr_code,
                                   chfpdr_name,
                                   atddr_code,
                                   atddr_name,
                                   main_symp,
                                   adm_rea,
                                   adm_way,
                                   apgr,
                                   diet_info,
                                   growth_deg,
                                   mtl_stas_norm,
                                   slep_info,
                                   sp_info,
                                   mind_info,
                                   nurt,
                                   self_ablt,
                                   nurscare_obsv_item_name,
                                   nurscare_obsv_rslt,
                                   smoke,
                                   stop_smok_days,
                                   smok_info,
                                   smok_day,
                                   drnk,
                                   drnk_frqu,
                                   drnk_day,
                                   eval_time,
                                   resp_nurs_name,
                                   vali_flag
                              from v_gzsi_adminfo
                            where mdtrt_id = '{0}' ";

            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 诊断记录
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetDiseinfo(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql

            string strSql = @"select mdtrt_id,
                                   psn_no,
                                   inout_diag_type,
                                   maindiag_flag,
                                   diag_seq,
                                   diag_time,
                                   wm_diag_code,
                                   wm_diag_name,
                                   tcm_dise_code,
                                   tcm_dise_name,
                                   tcmsymp_code,
                                   tcmsymp,
                                   vali_flag
                              from v_gzsi_diseinfo
                            where mdtrt_id = '{0}'";

            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 病程记录
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetCoursinfo(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql

            string strSql = @"select mdtrt_id,
                                   psn_no,
                                   dept_code,
                                   dept_name,
                                   wardarea_name,
                                   bedno,
                                   rcd_time,
                                   chfcomp,
                                   cas_ftur,
                                   tcm4d_rslt,
                                   dise_evid,
                                   prel_wm_diag_code,
                                   prel_wm_dise_name,
                                   prel_tcm_diag_code,
                                   prel_tcm_dise_name,
                                   prel_tcmsymp_code,
                                   prel_tcmsymp,
                                   finl_wm_diag_code,
                                   finl_wm_diag_name,
                                   finl_tcm_dise_code,
                                   finl_tcm_dise_name,
                                   finl_tcmsymp_code,
                                   finl_tcmsymp,
                                   dise_plan,
                                   prnp_trt,
                                   ipdr_code,
                                   ipdr_name,
                                   ipt_dr_code,
                                   ipt_dr_name,
                                   prnt_doc_name,
                                   vali_flag
                              from v_gzsi_coursrinfo
                            where mdtrt_id = '{0}' ";

            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 手术记录
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetOprninfo(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql

            string strSql = @"select mdtrt_id,
                                   psn_no,
                                   oprn_appy_id,
                                   oprn_seq,
                                   blotype_abobfpn_inhosp_ifet,
                                   oprn_time,
                                   oprn_type_code,
                                   oprn_type_name,
                                   bfpn_diag_code,
                                   bfpn_oprn_diag_name,
                                   bfpn_inhosp_ifet,
                                   afpn_diag_code,
                                   afpn_diag_name,
                                   sinc_heal_lv_code,
                                   sinc_heal_lv,
                                   back_oprn,
                                   selv,
                                   prev_abtl_mednme,
                                   prev_abtl_medn,
                                   abtl_medn_days,
                                   oprn_oprt_name,
                                   oprn_oprt_code,
                                   oprn_lv_code,
                                   oprn_lv_name,
                                   anst_mtd_code,
                                   anst_mtd_name,
                                   anst_lv_code,
                                   anst_lv_name,
                                   exe_anst_dept_code,
                                   exe_anst_dept_name,
                                   anst_efft,
                                   oprn_begntime,
                                   oprn_endtime,
                                   oprn_asps,
                                   oprn_asps_ifet,
                                   afpn_info,
                                   oprn_merg,
                                   oprn_conc,
                                   oprn_anst_dept_code,
                                   oprn_anst_dept_name,
                                   palg_dise,
                                   oth_med_dspo,
                                   out_std_oprn_time,
                                   oprn_oper_name,
                                   oprn_asit_name1,
                                   oprn_asit_name2,
                                   anst_dr_name,
                                   anst_asa_lv_code,
                                   anst_asa_lv_name,
                                   anst_medn_code,
                                   anst_medn_name,
                                   anst_medn_dos,
                                   anst_dosunt,
                                   anst_begntime,
                                   anst_endtime,
                                   anst_merg_symp_code,
                                   anst_merg_symp,
                                   anst_merg_symp_dscr,
                                   pacu_begntime,
                                   pacu_endtime,
                                   oprn_selv,
                                   canc_oprn,
                                   vali_flag
                              from v_gzsi_oprninfo
                             where mdtrt_id = '{0}'";

            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 抢救记录
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetRescinfo(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql

            string strSql = @"select dept,
                                   dept_name,
                                   bedno,
                                   diag_name,
                                   diag_code,
                                   wardarea_name,
                                   cond_chg,
                                   resc_mes,
                                   oprn_oprt_code,
                                   oprn_oprt_name,
                                   oprn_oper_part,
                                   itvt_name,
                                   oprt_mtd,
                                   oprt_cnt,
                                   resc_begntime,
                                   resc_endtime,
                                   dise_item_name,
                                   dise_ccls,
                                   dise_ccls_qunt,
                                   dise_ccls_code,
                                   mnan,
                                   resc_psn_list,
                                   proftechttl_code,
                                   doc_code,
                                   dr_name,
                                   vali_flag
                              from v_gzsi_rescinfo
                             where mdtrt_id = '{0}'";

            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 死亡记录
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetDieinfo(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql

            string strSql = @"select dept,
                                   dept_name,
                                   bedno,
                                   adm_time,
                                   adm_dise,
                                   adm_info,
                                   trt_proc_dscr,
                                   die_time,
                                   die_drt_rea,
                                   die_drt_rea_code,
                                   die_dise_name,
                                   die_dise_code,
                                   agre_corp_dset,
                                   ipdr_name,
                                   atddr_code,
                                   atddr_name,
                                   chfpdr_code,
                                   chfpdr_name,
                                   chfdr_name,
                                   sign_time,
                                   vali_flag
                              from v_gzsi_dieinfo
                             where mdtrt_id = '{0}'";

            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 出院小结
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetDscginfo(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql

            string strSql = @"select dscg_date,
                                   adm_dise_dscr,
                                   dscg_dise_dscr,
                                   adm_info,
                                   trt_proc_rslt_dscr,
                                   dscg_info,
                                   dscg_drord,
                                   dept,
                                   rec_doc,
                                   main_drug_name,
                                   oth_imp_info,
                                   vali_flag,
                                   caty
                              from v_gzsi_dscginfo
                             where mdtrt_id = '{0}'";

            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        #endregion

        #region 屏蔽
        ///// <summary>
        ///// 插入上传失败的费用明细
        ///// </summary>
        ///// <param name="registerPatientInfo"></param>
        ///// <returns></returns>
        //public int InsertGDSZFeeinfo(List<API.GZSI.Models.FeeInfoRow> feeinfos)
        //{

        //    #region sql
        //    string strSql = @"INSERT INTO fin_ipr_siinmaininfo_gd f
        //                        (
        //                        INPATIENT_NO,
        //                        REG_NO,
        //                        BALANCE_NO,
        //                        INVOICE_NO,
        //                        CARD_NO,
        //                        MCARD_NO, --5
        //                        NAME,
        //                        IDENNO,
        //                        CLINIC_DIAGNOSE,
        //                        PAYKIND_CODE,
        //                        PACT_CODE,--10
        //                        PACT_NAME,
        //                        OPER_CODE,
        //                        OPER_DATE,
        //                        TOT_COST,
        //                        PUB_COST,--15
        //                        OWN_COST,
        //                        VALID_FLAG,--17
        //                        FEE_TIMES,
        //                        SEX_CODE,--18
        //                        DEPT_CODE,
        //                        IN_DATE,--20
        //                        BALANCE_DATE,
        //                        TYPE_CODE,
        //                        BKA825,
        //                        BKA826,
        //                        AKA151,
        //                        BKA838,
        //                        AKB067,--27
        //                        AKB066,
        //                        BKA821,
        //                        BKA839,
        //                        AKE039,
        //                        AKE035,--32
        //                        AKE026,
        //                        AKE029,
        //                        BKA841,
        //                        BKA842,
        //                        BKA840,    --37        
        //                        PATIENT_NO,
        //                        AAA027,
        //                        AAZ267,
        //                        AAB301,
        //                        AAE140,
        //                        BKA006,
        //                        AKA130,
        //                        ic_reg_permit,
        //                        empl_type          
        //                         )
        //                        Values
        //                        (
        //                        '{0}',
        //                        '{1}',
        //                        '{2}',
        //                        '{3}',
        //                        '{4}',
        //                        '{5}',
        //                        '{6}',
        //                        '{7}',
        //                        '{8}',
        //                        '{9}',
        //                        '{10}',
        //                        '{11}',
        //                        '{12}',
        //                        to_date('{13}','YYYY-MM-DD hh24:mi:ss'),
        //                        '{14}',
        //                        '{15}',
        //                        '{16}',
        //                        '{17}',
        //                        0,
        //                        '{18}',
        //                        '{19}',
        //                        to_date('{20}','YYYY-MM-DD hh24:mi:ss'),
        //                        to_date('{21}','YYYY-MM-DD hh24:mi:ss'),
        //                        '{22}',
        //                        {23},
        //                        {24},
        //                        {25},
        //                        {26},
        //                        {27},
        //                        {28},
        //                        {29},
        //                        {30},
        //                        {31},
        //                        {32},
        //                        {33},
        //                        {34},
        //                        {35},
        //                        {36},
        //                        {37},
        //                        '{38}',
        //                        '{39}',
        //                        '{40}',
        //                        '{41}',
        //                        '{42}',
        //                        '{43}',
        //                         {44},
        //                         '{45}',
        //                         '{46}'
        //                        )";
        //    strSql = string.Format(strSql,
        //        feeinfos.ID,
        //        patientInfo.SIMainInfo.RegNo,
        //        balanceNo,
        //        patientInfo.SIMainInfo.InvoiceNo,
        //        patientInfo.PID.CardNO,
        //        patientInfo.SSN,//personInfo.MCardNo,
        //        patientInfo.Name,//personInfo.Name,
        //        patientInfo.IDCard,//personInfo.IdenNo,
        //        patientInfo.ClinicDiagnose,
        //        patientInfo.Pact.PayKind.ID,
        //        patientInfo.Pact.ID,
        //        patientInfo.Pact.Name,
        //        this.Operator.ID,
        //        this.GetDateTimeFromSysDateTime().ToString(),
        //        patientInfo.SIMainInfo.TotCost,
        //        patientInfo.SIMainInfo.PubCost,
        //        patientInfo.SIMainInfo.OwnCost,
        //        "1",
        //        patientInfo.Sex.ID,
        //        patientInfo.DoctorInfo.Templet.Dept.ID,//SeeDoct.Dept.ID,//PVisit.PatientLocation.Dept.ID,
        //        patientInfo.PVisit.InTime.ToString("yyyy-MM-dd HH:mm:ss"),
        //        patientInfo.SIMainInfo.BalanceDate.ToString("yyyy-MM-dd HH:mm:ss"),
        //        "1",
        //        patientInfo.SIMainInfo.Bka825,
        //        patientInfo.SIMainInfo.Bka826,
        //        patientInfo.SIMainInfo.Aka151,
        //        patientInfo.SIMainInfo.Bka838,
        //        patientInfo.SIMainInfo.Akb067,
        //        patientInfo.SIMainInfo.Akb066,
        //        patientInfo.SIMainInfo.Bka821,
        //        patientInfo.SIMainInfo.Bka839,
        //        patientInfo.SIMainInfo.Ake039,
        //        patientInfo.SIMainInfo.Ake035,
        //        patientInfo.SIMainInfo.Ake026,
        //        patientInfo.SIMainInfo.Ake029,
        //        patientInfo.SIMainInfo.Bka841,
        //        patientInfo.SIMainInfo.Bka842,
        //        patientInfo.SIMainInfo.Bka840,
        //        patientInfo.PID.PatientNO,
        //        patientInfo.SIMainInfo.Aaa027,
        //        patientInfo.SIMainInfo.Aaz267,
        //        patientInfo.SIMainInfo.Aab301,
        //        patientInfo.SIMainInfo.Aae140,
        //        patientInfo.SIMainInfo.Bka006,
        //        patientInfo.SIMainInfo.Aka130,
        //        TacCode,
        //        patientInfo.SIMainInfo.Bka004
        //        );

        //    #endregion

        //    try
        //    {
        //        if (this.ExecNoQuery(strSql) < 0)
        //        {
        //            return -1;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        this.Err = e.Message;
        //        return -1;
        //    }

        //    return 1;
        //}
        #endregion

        #endregion

        #region 医疗保障基金结算清单

        /// <summary>
        /// 1、结算信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetJsqdSetlInfo(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select t.mdtrt_id,
                                   t.setl_id,
                                   t.fixmedins_name,
                                   t.fixmedins_code,
                                   t.hi_setl_lv,
                                   t.hi_no,
                                   t.medcasno,
                                   t.dcla_time,
                                   t.psn_name,
                                   t.gend,
                                   t.brdy,
                                   t.age,
                                   t.ntly,
                                   t.nwb_age,
                                   t.naty,
                                   t.patn_cert_type,
                                   t.certno,
                                   t.prfs,
                                   t.curr_addr,
                                   t.emp_name,
                                   t.empr_addr,
                                   t.empr_tel,
                                   t.curr_addr_poscode,
                                   t.coner_name,
                                   t.patn_rlts,
                                   t.coner_addr,
                                   t.coner_tel,
                                   t.hi_type,
                                   t.insuplc,
                                   t.sp_psn_type,
                                   t.nwb_adm_type,
                                   t.nwb_bir_wt,
                                   t.nwb_adm_wt,
                                   t.opsp_diag_caty,
                                   t.opsp_mdtrt_date,
                                   t.ipt_med_type,
                                   t.adm_way,
                                   t.trt_type,
                                   t.adm_time,
                                   t.adm_caty,
                                   t.refldept_dept,
                                   t.dscg_time,
                                   t.dscg_caty,
                                   t.act_ipt_days,
                                   t.adm_ward,
                                   t.otp_wm_dise,
                                   t.wm_dise_code,
                                   t.otp_tcm_dise,
                                   t.tcm_dise_code,
                                   t.oprn_oprt_code_cnt,
                                   t.vent_used_dura,
                                   t.pwcry_bfadm_coma_dura,
                                   t.pwcry_afadm_coma_dura,
                                   t.bld_cat,
                                   t.bld_amt,
                                   t.bld_unt,
                                   t.spga_nurscare_days,
                                   t.lv1_nurscare_days,
                                   t.scd_nurscare_days,
                                   t.lv3_nurscare_days,
                                   t.dscg_way,
                                   t.acp_medins_name,
                                   t.acp_optins_code,
                                   t.bill_code,
                                   t.bill_no,
                                   t.biz_sn,
                                   t.days_rinp_flag_31,
                                   t.days_rinp_pup_31,
                                   t.chfpdr_name,
                                   t.chfpdr_code,
                                   t.setl_begn_date,
                                   t.setl_end_date,
                                   t.psn_selfpay,
                                   t.psn_ownpay,
                                   t.acct_pay,
                                   t.psn_cashpay,
                                   t.hi_paymtd,
                                   t.hsorg,
                                   t.hsorg_opter,
                                   t.medins_fill_dept,
                                   t.medins_fill_psn,
                                   t.prfs_name,
                                   t.psn_no,
                                   t.mul_nwb_bir_wt,
                                   t.mul_nwb_adm_wt,
                                   t.resp_nurs_code,
                                   t.stas_type
                              from v_jsqd_setlinfo t
                             where t.mdtrt_id = '{0}' ";
            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 2、基金支付信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetJsqdPayInfo(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select t.fund_pay_type,
                                     t.fund_payamt
                                from v_jsqd_payinfo t
                               where t.mdtrt_id = '{0}' ";
            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 3、门诊慢特病诊断信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetJsqdOpspDiseinfo(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select  t.diag_name,
                                      t.diag_code,
                                      t.oprn_oprt_name,
                                      t.oprn_oprt_code
                                 from v_jsqd_opspdiseinfo t
                                where t.mdtrt_id = '{0}' ";
            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 4、住院诊断信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetJsqdDiseInfo(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select t.diag_type,
                                     t.maindiag_flag,
                                     t.diag_code,
                                     t.diag_name,
                                     t.adm_cond_type
                               from v_jsqd_diseinfo t
                              where t.mdtrt_id = '{0}'";
            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 5、收费项目信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetJsqdIteminfo(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select t.med_chrgitm,
                                     t.amt,
                                     t.claa_sumfee,
                                     t.clab_amt,
                                     t.fulamt_ownpay_amt,
                                     t.oth_amt
                                from v_jsqd_iteminfo t
                               where t.mdtrt_id = '{0}' ";
            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 6、手术信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetJsqdOprnInfo(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select t.oprn_oprt_type,
                                     t.oprn_oprt_name,
                                     t.oprn_oprt_code,
                                     t.oprn_oprt_date,
                                     t.anst_way,
                                     t.oper_dr_name,
                                     t.oper_dr_code,
                                     t.anst_dr_name,
                                     t.anst_dr_code,
                                     t.main_oprn_flag,
                                     t.oprn_oprt_begntime,
                                     t.oprn_oprt_endtime,
                                     t.anst_begntime,
                                     t.anst_endtime
                                from v_jsqd_oprninfo t
                               where t.mdtrt_id = '{0}' ";
            #endregion
            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 7、重症监护信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetJsqdIcuInfo(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select t.scs_cutd_ward_type,
                                     t.scs_cutd_inpool_time,
                                     t.scs_cutd_exit_time,
                                     t.scs_cutd_sum_dura
                                from v_jsqd_icuinfo t
                               where t.mdtrt_id = '{0}' ";
            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 8、输血信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetJsqdBldinfo(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select t.bld_cat,
                                     t.bld_amt,
                                     t.bld_unt
                                from v_jsqd_bldinfo t
                               where t.mdtrt_id = '{0}' ";
            #endregion



            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 通过id获取医保信息
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo GetPatientForJSQD(string inPatientNO)
        {
            #region 查询语句

            string strSql = @"select t1.inpatient_no, -- 0住院流水号
                                     t1.reg_no, -- 1就医登记号
                                     t1.fee_times, -- 2费用批次
                                     t1.balance_no, -- 3 结算序号
                                     t1.invoice_no, -- 4 主发票号
                                     t1.medical_type, -- 5 医疗类别
                                     t1.patient_no, -- 6 住院号
                                     t1.card_no, -- 7 卡号
                                     t1.mcard_no, -- 8 医疗证号
                                     t1.app_no, -- 9 审批号
                                     t1.procreate_pcno, -- 10 生育保险患者电脑号
                                     t1.si_begindate, -- 11 参保日期
                                     t1.si_state, -- 12 参保状态
                                     t1.name, -- 13 姓名
                                     t1.sex_code, -- 14 性别
                                     t1.idenno, -- 15 身份证号码
                                     t1.spell_code, -- 16 拼音码
                                     t1.birthday, -- 17 生日
                                     t1.empl_type, -- 18 人员类型
                                     t1.work_name, -- 19 工作单位
                                     t1.clinic_diagnose, -- 20 门诊诊断
                                     t1.dept_code, -- 21 科室编码
                                     t1.dept_name, -- 22 科室名称
                                     t1.paykind_code, -- 23 结算类别
                                     t1.pact_code, --  24 合同代码
                                     t1.pact_name, -- 25 合同单位名称
                                     t1.bed_no, -- 26 床号
                                     t1.in_date, -- 27 入院日期
                                     t1.in_diagnosedate, --28 入院诊断日期
                                     t1.in_diagnose, -- 29 入院诊断编码
                                     t1.in_diagnosename, -- 30 入院诊断名称
                                     t2.out_date, -- 31 出院时间
                                     t1.out_diagnose, -- 32 出院诊断编码
                                     t1.out_diagnosename, -- 33 出院诊断名称
                                     t1.balance_date, -- 34 结算日期
                                     t1.tot_cost, -- 35 总金额
                                     t1.pay_cost, -- 36 帐户支付
                                     t1.pub_cost, -- 37 社保支付金额
                                     t1.item_paycost, -- 38 部分项目自付金额
                                     t1.base_cost, -- 39 个人起付金额
                                     t1.item_paycost2, -- 40 个人自费项目金额
                                     t1.item_ylcost, -- 41 个人自付金额（乙类自付部分）
                                     t1.own_cost, -- 42 个人自负金额
                                     t1.overtake_owncost,-- 43 超统筹支付限额个人自付金额
                                     t1.hos_cost, -- 44 医药机构分担金额(中山医保民政统筹金额)
                                     t1.own_cause, -- 45 自费原因
                                     t1.oper_code, -- 46 操作员编码
                                     t1.oper_date, -- 47 操作时间
                                     t1.year_cost, -- 48 本年度可用定额
                                     t1.valid_flag, -- 49 是否有效
                                     t1.balance_state, -- 50 是否结算
                                     t1.individualbalance, -- 51 个人账户余额
                                     t1.freezemessage, -- 52 冻结信息
                                     t1.applysequence, -- 53 申请序号
                                     t1.applytypeid, -- 54 申请类型编号
                                     t1.applytypename, -- 55 申请类型
                                     t1.fundid, -- 56 基金编码
                                     t1.fundname, -- 57 基金名称
                                     t1.businesssequence, -- 58 业务序列号
                                     t1.invoice_seq, -- 59 发票序号
                                     t1.over_cost, -- 60 医保大额补助
                                     t1.official_cost, -- 61 医保公务员补助
                                     t1.remark, -- 62 医保信息（PersonAccountInfo）
                                     t1.type_code, -- 63 人员类型
                                     t1.trans_type, -- 64 交易类型
                                     t1.person_type, -- 65 人员类型
                                     t1.diagnose_oper_code, -- 66 操作员
                                     t1.operatecode1, --67
                                     t1.operatecode2, -- 68
                                     t1.operatecode3, -- 69
                                     t1.primarydiagnosename, -- 70
                                     t1.primarydiagnosecode, -- 71
                                     t1.ybmedno, -- 72 居民医保结算单号,保存以做为 注销居民门诊费用结算 参数之一
                                     t1.trans_no, -- 73 上传号                                 --东莞医保
                                     t1.internal_fee, -- 74 普通医保内费用                  --东莞医保
                                     t1.external_fee, --75 external_fee 75 普通医保外费用                   --东莞医保
                                     t1.official_own_cost, -- 76 大额/公务员自付金额              --东莞医保
                                     t1.over_inte_fee, -- 77 本次交易统筹封顶后医保内金额    --东莞医保
                                     t1.own_count_fee, -- 78  个人应付总金额(个人帐户支付+现金)    --东莞医保
                                     t1.own_second_count_fee, -- 79 个人自付二金额    --东莞医保
                                     t1.si_diagnose, -- 80 医保诊断代码    --东莞医保
                                     t1.si_diagnosename, -- 81 医保诊断代码名称     --东莞医保
                                     t1.pub_fee_cost, -- 82 统筹支付金额     --东莞医保
                                     t1.ext_flag, -- 83 深圳医保的执行状态 登记： R 上传 ：S 结算：B 支付：J
                                     t1.ext_flag1, -- 84 异地医保的参保地址
                                     t1.zhuhaisitype, -- 85 参保险种(珠海医保)
                                     t1.gzsiupload, -- 86 广州医保上传状态 未上传：0， 已上传 1
                                     t1.mdtrt_id, -- 87 就诊ID
                                     t1.setl_id, -- 88 结算ID
                                     t1.psn_no, -- 89 人员编号
                                     t1.psn_name, -- 90 人员姓名
                                     t1.psn_cert_type, -- 91 人员证件类型
                                     t1.certno, -- 92 证件号码
                                     t1.gend, -- 93 性别
                                     t1.naty, -- 94 民族
                                     t1.brdy, -- 95 生日
                                     t1.age, -- 96 年龄
                                     t1.insutype, -- 97 险种类型
                                     t1.psn_type, -- 98 人员类别
                                     t1.cvlserv_flag, -- 99 公务员标识
                                     t1.setl_time, -- 100 结算时间
                                     t1.psn_setlway, -- 101 个人结算方式
                                     t1.mdtrt_cert_type, -- 102 就诊凭证类型
                                     t1.med_type, -- 103 医疗类别
                                     t1.dise_code, -- 104 病种编码
                                     t1.dise_name, -- 105 病种名称
                                     t1.medfee_sumamt, -- 106 医疗费总额
                                     t1.ownpay_amt, -- 107 全自费金额
                                     t1.overlmt_selfpay, -- 108 超限价自费费用
                                     t1.preselfpay_amt, -- 109 先行自付金额
                                     t1.inscp_scp_amt, -- 110 符合范围金额
                                     t1.med_sumfee, -- 111 医保认可费用总额
                                     t1.act_pay_dedc, -- 112 实际支付起付线
                                     t1.hifp_pay, -- 113 基本医疗保险统筹基金支出
                                     t1.pool_prop_selfpay, -- 114 基本医疗保险统筹基金支付比例
                                     t1.cvlserv_pay, -- 115 公务员医疗补助资金支出
                                     t1.hifes_pay, -- 116 企业补充医疗保险基金支出
                                     t1.hifmi_pay, -- 117 居民大病保险资金支出
                                     t1.hifob_pay, -- 118 职工大额医疗费用补助基金支出
                                     t1.hifdm_pay, -- 119 伤残人员医疗保障基金支出
                                     t1.maf_pay, -- 120 医疗救助基金支出
                                     t1.oth_pay, -- 121 其他基金支出
                                     t1.fund_pay_sumamt, -- 122 基金支付总额
                                     t1.hosp_part_amt, -- 123 医院负担金额
                                     t1.psn_part_am, -- 124 个人负担总金额
                                     t1.acct_pay, -- 125 个人账户支出
                                     t1.cash_payamt, -- 126 现金支付金额
                                     t1.acct_mulaid_pay, -- 127 账户共济支付金额
                                     t1.balc, -- 128 个人账户支出后余额
                                     t1.clr_optins, -- 129 清算经办机构
                                     t1.clr_way, --130 清算方式
                                     t1.clr_type, -- 131 清算类别
                                     t1.medins_setl_id, -- 132 医药机构结算ID
                                     t1.vola_type, -- 133 违规类型
                                     t1.vola_dscr -- 134 违规说明
                                from fin_ipr_siinmaininfo t1, fin_ipr_inmaininfo t2
                               where t1.inpatient_no = '{0}'
                                 and t1.valid_flag = '1'
                                 and t1.type_code = '2'
                                 and t1.inpatient_no = t2.inpatient_no
                                 and t1.balance_state = '1'";

            #endregion

            string sql = string.Format(strSql, inPatientNO);

            this.ExecQuery(sql);

            try
            {
                FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();

                while (Reader.Read())
                {
                    this.setInpatientInfo(ref patient);
                    break;
                }

                return patient;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                Reader.Close();
                return null;
            }
        }

        /// <summary>
        /// 通过id获取医保信息
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <returns></returns>
        public int UpdateUploadDRG(string mdtrt)
        {
            #region sql
            string strSql = @"UPDATE FIN_IPR_SIINMAININFO
                          SET gzsiupload_jsqd = '1'
                        WHERE REG_NO = '{0}'";
            #endregion
            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        public DataTable QueryInvoiceInfoList(string inpatientno)
        {
            string strSql = @"select t.createtime 创建时间,t.orderno 订单号,t.notifyemail 邮箱地址,t.invoicecode 发票代码,t.invoiceno 发票号码,
                    t.payername 发票抬头,t.extaxamount 含税金额,t.statusmsg 开票状态,t.serialno 序列号,reqxml from com_opb_eleinvoiceopeninfo t where inpatientnoorinvoiceno='{0}' or PAYERNAME ='{0}'  order by t.createtime desc ";

           

            strSql = string.Format(strSql, inpatientno);
            try
            {
                DataSet ds = new DataSet();
                this.ExecQuery(strSql, ref ds);
                return ds.Tables[0];
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DataTable QueryInvoiceInfo(string inpatientno,decimal money)
        {
            string strSql = @"select t.invoicecode,t.invoiceno from com_opb_eleinvoiceopeninfo t 
                                 where t.inpatientnoorinvoiceno='{0}' and t.EXTAXAMOUNT='{1}' order by t.createtime desc";
            strSql = string.Format(strSql, inpatientno, money);

            try
            {
                DataSet ds = new DataSet();
                this.ExecQuery(strSql, ref ds);
                return ds.Tables[0];
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 保存发票号// {16D3E1C0-0903-4A6B-8A7B-BDC272F2481C}
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <returns></returns>
        public int SaveInvoiceInfo(string mdtrt, string invoiceID, string invoiceNO)
        {
            #region sql
            string strSql = @"UPDATE FIN_IPR_SIINMAININFO
                          SET invoice_id_ps = '{1}',
                                 invoice_no_ps = '{2}'
                        WHERE REG_NO = '{0}'";
            #endregion
            try
            {
                strSql = string.Format(strSql, mdtrt, invoiceID, invoiceNO);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        #endregion

        #region  零星报销
        //结算明细
        public string HasLXInfo(string mdtrt_id, string setl_id)
        {
            string strSql = @"select 1 from FIN_IPR_SIINMAININFO_LX s 
                                        where 1=1
                                        and s.mdtrt_id = '{0}' 
                                        and s.setl_id  = '{1}'";
            strSql = string.Format(strSql, mdtrt_id, setl_id);
            return this.ExecSqlReturnOne(strSql);
        }

        //获取零星表中的流水号
        public string getLXInpatientNoBySetlInfo(string mdtrt_id, string setl_id)
        {
            string strSql = @"select inpatient_no from fin_ipr_siinmaininfo_lx s 
                                        where 1=1
                                        and s.mdtrt_id = '{0}' 
                                        and s.setl_id  = '{1}'";
            strSql = string.Format(strSql, mdtrt_id, setl_id);
            return this.ExecSqlReturnOne(strSql);
        }

        public int getLXInpatientNoBySetlInfo(string mdtrt_id, string setl_id, ref Models.Response.ResponseGzsiModel5261.Output.Setlinfo data)
        {
            string strSql = @"select inpatient_no, gzsiupload  from fin_ipr_siinmaininfo s 
                                        where 1=1
                                        and s.mdtrt_id = '{0}' 
                                        and s.setl_id  = '{1}'";
            strSql = string.Format(strSql, mdtrt_id, setl_id);

            try
            {
                if (ExecQuery(strSql) == -1)
                {
                    return -1;
                }
                while (Reader.Read())
                {
                    data.inpatientNo = Reader[0].ToString();//流水号
                    data.updateState = Reader[1].ToString();//上传状态
                }
                Reader.Close();
            } //抛出错误
            catch (Exception ex)
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return -1;
            }
            return 1;
        }

        //插入零星数据
        public int InsertLXInfo(Models.Response.ResponseGzsiModel5261.Output.Setlinfo setlinfo)
        {
            try
            {
                string strSql = @"insert into FIN_IPR_SIINMAININFO_LX
                                            ( setl_id	,--	受理号  0
                                                reim_type	,--	零报类型 1
                                                manl_reim_rea	,--	零星报销原因 2
                                                mdtrt_id	,--	就诊ID 3
                                                psn_name	,--	人员姓名 4
                                                psn_cert_type	,--	人员证件类型 5
                                                certno	,--	证件号码 6
                                                gend	,--	性别 7
                                                naty	,--	民族 8
                                                brdy	,--	出生日期 9
                                                age	,--	年龄 10 
                                                insutype	,--	险种类型 11
                                                psn_type	,--	人员类别 12
                                                insu_optins	,--	参保机构医保区划 13
                                                emp_name	,--	单位名称 14
                                                med_type	,--	医疗类别 15
                                                ipt_op_no	,--	住院/门诊号 16
                                                dise_no	,--	病种编码 17
                                                dise_name	,--	病种名称 18
                                                invono	,--	发票号 19
                                                oprn_oprt_code	,--	手术操作代码 20
                                                oprn_oprt_name	,--	手术操作名称 21
                                                main_cond_desc	,--	主要病情描述 22
                                                dise_info	,--	诊断信息 23
                                                adm_dept_code	,--	科室编码 24
                                                adm_dept_name	,--	科室名称 25
                                                adm_bed	,--	入院床位 26
                                                dscg_dise_code	,--	主诊断代码 27
                                                dscg_dise_name	--	主诊断名称 28

                                            )
                                            values
                                            ('{0}',
                                                '{1}',
                                                '{2}',
                                                '{3}',
                                                '{4}',
                                                '{5}',
                                                '{6}',
                                                '{7}',
                                                '{8}',
                                                 to_date('{9}','yyyy-MM-dd'),
                                                '{10}',
                                                '{11}',
                                                '{12}',
                                                '{13}',
                                                '{14}',
                                                '{15}',
                                                '{16}',
                                                '{17}',
                                                '{18}',
                                                '{19}',
                                                '{20}',
                                                '{21}',
                                                '{22}',
                                                '{23}',
                                                '{24}',
                                                '{25}',
                                                '{26}',
                                                '{27}',
                                                '{28}'
                                                )";
                strSql = string.Format(strSql,
                  setlinfo.setl_id,//	受理号
                    setlinfo.reim_type,//	零报类型
                    setlinfo.manl_reim_rea,//	零星报销原因
                    setlinfo.mdtrt_id,//	就诊ID
                    setlinfo.psn_name,//	人员姓名
                    setlinfo.psn_cert_type,//	人员证件类型
                    setlinfo.certno,//	证件号码
                    setlinfo.gend,//	性别
                    setlinfo.naty,//	民族
                    setlinfo.brdy,//	出生日期
                    setlinfo.age,//	年龄
                    setlinfo.insutype,//	险种类型
                    setlinfo.psn_type,//	人员类别
                    setlinfo.insu_optins,//	参保机构医保区划
                    setlinfo.emp_name,//	单位名称
                    setlinfo.med_type,//	医疗类别
                    setlinfo.ipt_op_no,//	住院/门诊号
                    setlinfo.dise_no,//	病种编码
                    setlinfo.dise_name,//	病种名称
                    setlinfo.invono,//	发票号
                    setlinfo.oprn_oprt_code,//	手术操作代码
                    setlinfo.oprn_oprt_name,//	手术操作名称
                    setlinfo.main_cond_desc,//	主要病情描述
                    setlinfo.dise_info,//	诊断信息
                    setlinfo.adm_dept_code,//	科室编码
                    setlinfo.adm_dept_name,//	科室名称
                    setlinfo.adm_bed,//	入院床位
                    setlinfo.dscg_dise_code,//	主诊断代码
                    setlinfo.dscg_dise_name//	主诊断名称
                    );
                return this.ExecNoQuery(strSql);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        //更新零星流水号
        public int UpdateInpatientNoToLX(string mdtrt_id, string setl_id, string inpatientNO)
        {
            try
            {
                //更新零星流水号
                string strSql = @" update fin_ipr_siinmaininfo_lx x
                                        set x.inpatient_no = '{2}'
                                        where x.mdtrt_id = '{0}' and x.setl_id = '{1}'
                               ";
                strSql = string.Format(strSql, mdtrt_id, setl_id, inpatientNO);
                int res = this.ExecNoQuery(strSql);

                //无效原来的医保信息
                string deleteSISql = @" update fin_ipr_siinmaininfo x
                                        set x.valid_flag = '0'
                                        where x.mdtrt_id = '{0}' and x.setl_id = '{1}'
                                        and x.valid_flag = '1'";
                deleteSISql = string.Format(deleteSISql, mdtrt_id, setl_id);
                res = this.ExecNoQuery(deleteSISql);

                //插入新的记录
                string insertSISql = @"insert into fin_ipr_siinmaininfo   
                                                    select * from v_fin_ipr_inmaininfo_lx x 
                                                    where x.mdtrt_id = '{0}' and x.setl_id = '{1}'
                                                    ";
                insertSISql = string.Format(insertSISql, mdtrt_id, setl_id);
                res = this.ExecNoQuery(insertSISql);
                return res;

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        //判断是否有合适的流水号
        public int HasLXInpatientNoByCertno(string Certno)
        {
            string strSql = @"select count(*) from fin_ipr_inmaininfo i
                                        where i.idenno = '{0}'
                                        and i.inpatient_no not in (select nvl(x.inpatient_no,'-1') from fin_ipr_siinmaininfo_lx x)
                                        and i.in_state = 'O'";
            strSql = string.Format(strSql, Certno);
            return Convert.ToInt32(this.ExecSqlReturnOne(strSql));
        }

        public int HasLXInpatientNoByPatientNO(string patientNO)
        {
            string strSql = @"select count(*) from fin_ipr_inmaininfo i
                                        where i.patient_no = '{0}'
                                        and i.inpatient_no not in (select nvl(x.inpatient_no,'-1') from fin_ipr_siinmaininfo_lx x)
                                        and i.in_state = 'O'";
            strSql = string.Format(strSql, patientNO);
            return Convert.ToInt32(this.ExecSqlReturnOne(strSql));
        }

        //根据身份证获取流水号
        public string getLXInpatientNoByCertno(string Certno)
        {
            string strSql = @"select i.inpatient_no from fin_ipr_inmaininfo i
                                        where i.idenno = '{0}'
                                        and i.inpatient_no not in (select nvl(x.inpatient_no,'-1') from fin_ipr_siinmaininfo_lx x)
                                        and i.in_state = 'O'";
            strSql = string.Format(strSql, Certno);
            return this.ExecSqlReturnOne(strSql);
        }

        public string getLXInpatientNoByPatientNO(string patientNO)
        {
            string strSql = @"select i.inpatient_no from fin_ipr_inmaininfo i
                                        where i.patient_no = '{0}'
                                        and i.inpatient_no not in (select nvl(x.inpatient_no,'-1') from fin_ipr_siinmaininfo_lx x)
                                        and i.in_state = 'O'";
            strSql = string.Format(strSql, patientNO);
            return this.ExecSqlReturnOne(strSql);
        }

        /// <summary>
        /// 根据流水号获取患者信息
        /// </summary>
        /// <param name="InpatientNO"></param>
        /// <returns></returns>
        public DataTable getPatientInfoByInPatientNO(string InpatientNO)
        {
            string strSql = @"select i.name,i.idenno,s.invoice_id_ps,s.invoice_no_ps from fin_ipr_inmaininfo i
                                            left join fin_ipr_siinmaininfo s on i.inpatient_no = s.inpatient_no and s.valid_flag = '1'
                                            where i.inpatient_no = '{0}'";
            strSql = string.Format(strSql, InpatientNO);

            try
            {
                DataSet ds = new DataSet();
                this.ExecQuery(strSql, ref ds);
                return ds.Tables[0];
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #region 4201
        /// <summary>
        /// 1、明细信息feedetail
        /// </summary>
        /// <param name="inptiantNO"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int Get4201Feedetail(string inptiantNO, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select *
                              from v_4201_feedetail t
                             where t.inpatient_no = '{0}' ";

            #endregion

            try
            {
                strSql = string.Format(strSql, inptiantNO);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 1、就诊信息mdtrtinfo
        /// </summary>
        /// <param name="inptiantNO"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int Get4201Mdtrtinfo(string inptiantNO, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select *
                              from v_4201_mdtrtinfo t
                             where t.inpatient_no = '{0}' ";

            #endregion

            try
            {
                strSql = string.Format(strSql, inptiantNO);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 1、诊断diseinfo
        /// </summary>
        /// <param name="inptiantNO"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int Get4201Diseinfo(string inptiantNO, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select *
                              from v_4201_diseinfo t
                             where t.inpatient_no = '{0}' ";

            #endregion

            try
            {
                strSql = string.Format(strSql, inptiantNO);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }
        #endregion

        #region 病案首页信息4101

        /// <summary>
        /// 获取病案首页基本信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetBasyBaseInfoLX(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select mdtrt_sn,
                                   mdtrt_id,
                                   psn_no,
                                   patn_ipt_cnt,
                                   ipt_otp_no,
                                   medcasno,
                                   psn_name,
                                   gend,
                                   brdy,
                                   ntly,
                                   ntly_name,
                                   nwb_bir_wt,
                                   nwb_adm_wt,
                                   birplc,
                                   napl,
                                   naty,
                                   naty_name,
                                   certno,
                                   prfs,
                                   mrg_stas,
                                   mrg_name,
                                   curr_addr_poscode,
                                   curr_addr,
                                   psn_tel,
                                   resd_addr_prov,
                                   resd_addr_city,
                                   resd_addr_coty,
                                   resd_addr_subd,
                                   resd_addr_vil,
                                   resd_addr_housnum,
                                   resd_addr_poscode,
                                   resd_addr,
                                   empr_tel,
                                   empr_poscode,
                                   empr_addr,
                                   coner_tel,
                                   coner_name,
                                   coner_addr,
                                   coner_rlts_code,
                                   adm_way_code,
                                   adm_way_name,
                                   trt_type,
                                   trt_type_name,
                                   adm_caty,
                                   adm_ward,
                                   adm_date,
                                   dscg_date,
                                   dscg_caty,
                                   refldept_caty_name,
                                   dscg_ward,
                                   ipt_days,
                                   drug_dicm_flag,
                                   dicm_drug_name,
                                   die_autp_flag,
                                   abo_code,
                                   abo_name,
                                   rh_code,
                                   rh_name,
                                   die_code,
                                   deptdrt_name,
                                   chfdr_name,
                                   atddr_name,
                                   chfpdr_name,
                                   ipt_dr_name,
                                   resp_nurs_name,
                                   train_dr_name,
                                   intn_dr_name,
                                   codr_name,
                                   qltctrl_dr_name,
                                   qltctrl_nurs_name,
                                   medcas_qlt_name,
                                   medcas_qlt_code,
                                   medcas_qlt_date,
                                   dscg_way_name,
                                   dscg_way,
                                   acp_medins_code,
                                   acp_medins_name,
                                   dscg_31days_rinp_flag,
                                   dscg_31days_rinp_pup,
                                   damg_intx_ext_rea,
                                   damg_intx_ext_rea_disecode,
                                   brn_damg_bfadm_coma_dura,
                                   brn_damg_afadm_coma_dura,
                                   vent_used_time,
                                   cnfm_date,
                                   patn_dise_diag_crsp,
                                   patn_dise_diag_crsp_code,
                                   ipt_patn_diag_inscp,
                                   ipt_patn_diag_inscp_code,
                                   dscg_trt_rslt,
                                   dscg_trt_rslt_code,
                                   medins_orgcode,
                                   age,
                                   aise,
                                   pote_intn_dr_name,
                                   hbsag,
                                   hcv_ab,
                                   hiv_ab,
                                   resc_cnt,
                                   resc_succ_cnt,
                                   hosp_dise_fsttime,
                                   hif_pay_way_name,
                                   hif_pay_way_code,
                                   med_fee_paymtd_name,
                                   medfee_paymtd_code,
                                   selfpay_amt,
                                   medfee_sumamt,
                                   ordn_med_servfee,
                                   ordn_trt_oprt_fee,
                                   nurs_fee,
                                   com_med_serv_oth_fee,
                                   palg_diag_fee,
                                   lab_diag_fee,
                                   rdhy_diag_fee,
                                   clnc_dise_fee,
                                   nsrgtrt_item_fee,
                                   clnc_phys_trt_fee,
                                   rgtrt_trt_fee,
                                   anst_fee,
                                   rgtrt_fee,
                                   rhab_fee,
                                   tcm_trt_fee,
                                   wm_fee,
                                   abtl_medn_fee,
                                   tcmpat_fee,
                                   tcmherb_fee,
                                   blo_fee,
                                   albu_fee,
                                   glon_fee,
                                   clotfac_fee,
                                   cyki_fee,
                                   exam_dspo_matl_fee,
                                   trt_dspo_matl_fee,
                                   oprn_dspo_matl_fee,
                                   oth_fee,
                                   vali_flag
                              from v_basy_baseinfo_LX
                             where mdtrt_id = '{0}'";
            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            System.Data.DataSet ds = new System.Data.DataSet();

            try
            {
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 病案首页诊断信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetBasyDiseInfoLX(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select reg_no,
                                   palg_no,
                                   ipt_patn_disediag_type_code,
                                   disediag_type,
                                   maindiag_flag,
                                   diag_code,
                                   diag_name,
                                   inhosp_diag_code,
                                   inhosp_diag_name,
                                   adm_dise_cond_name,
                                   adm_dise_cond_code,
                                   adm_cond,
                                   adm_cond_code,
                                   high_diag_evid,
                                   bkup_deg,
                                   bkup_deg_code,
                                   vali_flag
                              from v_basy_diseinfo_LX
                              where reg_no = '{0}'";
            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 病案首页手术信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetBasyOprnInfoLX(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select reg_no,
                                   oprn_oprt_date,
                                   oprn_oprt_name,
                                   oprn_oprt_code,
                                   oprn_oprt_sn,
                                   oprn_lv_code,
                                   oprn_lv_name,
                                   oper_name,
                                   asit_1_name,
                                   asit_name2,
                                   sinc_heal_lv,
                                   sinc_heal_lv_code,
                                   anst_mtd_name,
                                   anst_mtd_code,
                                   anst_dr_name,
                                   oprn_oper_part,
                                   oprn_oper_part_code,
                                   oprn_con_time,
                                   anst_lv_name,
                                   anst_lv_code,
                                   oprn_patn_type,
                                   oprn_patn_type_code,
                                   main_oprn_flag,
                                   anst_asa_lv_code,
                                   anst_asa_lv_name,
                                   anst_medn_code,
                                   anst_medn_name,
                                   anst_medn_dos,
                                   unt,
                                   anst_begntime,
                                   anst_endtime,
                                   anst_copn_code,
                                   anst_copn_name,
                                   anst_copn_dscr,
                                   pacu_begntime,
                                   pacu_endtime,
                                   canc_oprn_flag,
                                   vali_flag
                              from v_basy_oprninfo_LX
                             where reg_no = '{0}'";
            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 病案首页ICU信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetBasyICUInfoLX(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql

            string strSql = @"select reg_no,
                                   icu_codeid,
                                   inpool_icu_time,
                                   out_icu_time,
                                   medins_orgcode,
                                   nurscare_lv_code,
                                   nurscare_lv_name,
                                   nurscare_days,
                                   back_icu,
                                   vali_flag
                              from v_basy_icuinfo_LX
                             where reg_no = '{0}'";

            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        #endregion

        #region 病历信息4701

        /// <summary>
        /// 入院记录
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetAdminfoLX(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql

            string strSql = @"select mdtrt_id,
                                   psn_no,
                                   mdtrt_sn,
                                   mdtrtsn,
                                   name,
                                   gend,
                                   age,
                                   adm_rec_no,
                                   psn_adm_no,
                                   wardarea_name,
                                   dept_code,
                                   dept_name,
                                   bedno,
                                   adm_time,
                                   illhis_stte_name,
                                   illhis_stte_rltl,
                                   stte_rele,
                                   chfcomp,
                                   dise_now,
                                   hlcon,
                                   dise_his,
                                   ifet,
                                   ifet_his,
                                   prev_vcnt,
                                   oprn_his,
                                   bld_his,
                                   algs_his,
                                   psn_his,
                                   mrg_his,
                                   mena_his,
                                   fmhis,
                                   physexm_tprt,
                                   physexm_pule,
                                   physexm_vent_frqu,
                                   physexm_systolic_pre,
                                   physexm_dstl_pre,
                                   physexm_height,
                                   physexm_wt,
                                   physexm_ordn_stas,
                                   physexm_skin_musl,
                                   physexm_spef_lymph,
                                   physexm_head,
                                   physexm_neck,
                                   physexm_chst,
                                   physexm_abd,
                                   physexm_finger_exam,
                                   physexm_genital_area,
                                   physexm_spin,
                                   physexm_all_fors,
                                   nersys,
                                   spcy_info,
                                   asst_exam_rslt,
                                   tcm4d_rslt,
                                   syddclft,
                                   syddclft_name,
                                   prnp_trt,
                                   rec_doc_code,
                                   rec_doc_name,
                                   ipdr_code,
                                   ipdr_name,
                                   chfdr_code,
                                   chfdr_name,
                                   chfpdr_code,
                                   chfpdr_name,
                                   atddr_code,
                                   atddr_name,
                                   main_symp,
                                   adm_rea,
                                   adm_way,
                                   apgr,
                                   diet_info,
                                   growth_deg,
                                   mtl_stas_norm,
                                   slep_info,
                                   sp_info,
                                   mind_info,
                                   nurt,
                                   self_ablt,
                                   nurscare_obsv_item_name,
                                   nurscare_obsv_rslt,
                                   smoke,
                                   stop_smok_days,
                                   smok_info,
                                   smok_day,
                                   drnk,
                                   drnk_frqu,
                                   drnk_day,
                                   eval_time,
                                   resp_nurs_name,
                                   vali_flag
                              from v_gzsi_adminfo_LX
                            where mdtrt_id = '{0}' ";

            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 诊断记录
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetDiseinfoLX(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql

            string strSql = @"select mdtrt_id,
                                   psn_no,
                                   inout_diag_type,
                                   maindiag_flag,
                                   diag_seq,
                                   diag_time,
                                   wm_diag_code,
                                   wm_diag_name,
                                   tcm_dise_code,
                                   tcm_dise_name,
                                   tcmsymp_code,
                                   tcmsymp,
                                   vali_flag
                              from v_gzsi_diseinfo_LX
                            where mdtrt_id = '{0}'";

            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 病程记录
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetCoursinfoLX(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql

            string strSql = @"select mdtrt_id,
                                   psn_no,
                                   dept_code,
                                   dept_name,
                                   wardarea_name,
                                   bedno,
                                   rcd_time,
                                   chfcomp,
                                   cas_ftur,
                                   tcm4d_rslt,
                                   dise_evid,
                                   prel_wm_diag_code,
                                   prel_wm_dise_name,
                                   prel_tcm_diag_code,
                                   prel_tcm_dise_name,
                                   prel_tcmsymp_code,
                                   prel_tcmsymp,
                                   finl_wm_diag_code,
                                   finl_wm_diag_name,
                                   finl_tcm_dise_code,
                                   finl_tcm_dise_name,
                                   finl_tcmsymp_code,
                                   finl_tcmsymp,
                                   dise_plan,
                                   prnp_trt,
                                   ipdr_code,
                                   ipdr_name,
                                   ipt_dr_code,
                                   ipt_dr_name,
                                   prnt_doc_name,
                                   vali_flag
                              from v_gzsi_coursrinfo_LX
                            where mdtrt_id = '{0}' ";

            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 手术记录
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetOprninfoLX(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql

            string strSql = @"select mdtrt_id,
                                   psn_no,
                                   oprn_appy_id,
                                   oprn_seq,
                                   blotype_abobfpn_inhosp_ifet,
                                   oprn_time,
                                   oprn_type_code,
                                   oprn_type_name,
                                   bfpn_diag_code,
                                   bfpn_oprn_diag_name,
                                   bfpn_inhosp_ifet,
                                   afpn_diag_code,
                                   afpn_diag_name,
                                   sinc_heal_lv_code,
                                   sinc_heal_lv,
                                   back_oprn,
                                   selv,
                                   prev_abtl_mednme,
                                   prev_abtl_medn,
                                   abtl_medn_days,
                                   oprn_oprt_name,
                                   oprn_oprt_code,
                                   oprn_lv_code,
                                   oprn_lv_name,
                                   anst_mtd_code,
                                   anst_mtd_name,
                                   anst_lv_code,
                                   anst_lv_name,
                                   exe_anst_dept_code,
                                   exe_anst_dept_name,
                                   anst_efft,
                                   oprn_begntime,
                                   oprn_endtime,
                                   oprn_asps,
                                   oprn_asps_ifet,
                                   afpn_info,
                                   oprn_merg,
                                   oprn_conc,
                                   oprn_anst_dept_code,
                                   oprn_anst_dept_name,
                                   palg_dise,
                                   oth_med_dspo,
                                   out_std_oprn_time,
                                   oprn_oper_name,
                                   oprn_asit_name1,
                                   oprn_asit_name2,
                                   anst_dr_name,
                                   anst_asa_lv_code,
                                   anst_asa_lv_name,
                                   anst_medn_code,
                                   anst_medn_name,
                                   anst_medn_dos,
                                   anst_dosunt,
                                   anst_begntime,
                                   anst_endtime,
                                   anst_merg_symp_code,
                                   anst_merg_symp,
                                   anst_merg_symp_dscr,
                                   pacu_begntime,
                                   pacu_endtime,
                                   oprn_selv,
                                   canc_oprn,
                                   vali_flag
                              from v_gzsi_oprninfo_LX
                             where mdtrt_id = '{0}'";

            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 抢救记录
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetRescinfoLX(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql

            string strSql = @"select dept,
                                   dept_name,
                                   bedno,
                                   diag_name,
                                   diag_code,
                                   wardarea_name,
                                   cond_chg,
                                   resc_mes,
                                   oprn_oprt_code,
                                   oprn_oprt_name,
                                   oprn_oper_part,
                                   itvt_name,
                                   oprt_mtd,
                                   oprt_cnt,
                                   resc_begntime,
                                   resc_endtime,
                                   dise_item_name,
                                   dise_ccls,
                                   dise_ccls_qunt,
                                   dise_ccls_code,
                                   mnan,
                                   resc_psn_list,
                                   proftechttl_code,
                                   doc_code,
                                   dr_name,
                                   vali_flag
                              from v_gzsi_rescinfo_LX
                             where mdtrt_id = '{0}'";

            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 死亡记录
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetDieinfoLX(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql

            string strSql = @"select dept,
                                   dept_name,
                                   bedno,
                                   adm_time,
                                   adm_dise,
                                   adm_info,
                                   trt_proc_dscr,
                                   die_time,
                                   die_drt_rea,
                                   die_drt_rea_code,
                                   die_dise_name,
                                   die_dise_code,
                                   agre_corp_dset,
                                   ipdr_name,
                                   atddr_code,
                                   atddr_name,
                                   chfpdr_code,
                                   chfpdr_name,
                                   chfdr_name,
                                   sign_time,
                                   vali_flag
                              from v_gzsi_dieinfo_LX
                             where mdtrt_id = '{0}'";

            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 出院小结
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetDscginfoLX(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql

            string strSql = @"select dscg_date,
                                   adm_dise_dscr,
                                   dscg_dise_dscr,
                                   adm_info,
                                   trt_proc_rslt_dscr,
                                   dscg_info,
                                   dscg_drord,
                                   dept,
                                   rec_doc,
                                   main_drug_name,
                                   oth_imp_info,
                                   vali_flag,
                                   caty
                              from v_gzsi_dscginfo_LX
                             where mdtrt_id = '{0}'";

            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        #endregion

        #region 医疗保障基金结算清单4401

        /// <summary>
        /// 1、结算信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetJsqdSetlInfoLX(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select t.mdtrt_id,
                                   t.setl_id,
                                   t.fixmedins_name,
                                   t.fixmedins_code,
                                   t.hi_setl_lv,
                                   t.hi_no,
                                   t.medcasno,
                                   t.dcla_time,
                                   t.psn_name,
                                   t.gend,
                                   t.brdy,
                                   t.age,
                                   t.ntly,
                                   t.nwb_age,
                                   t.naty,
                                   t.patn_cert_type,
                                   t.certno,
                                   t.prfs,
                                   t.curr_addr,
                                   t.emp_name,
                                   t.empr_addr,
                                   t.empr_tel,
                                   t.curr_addr_poscode,
                                   t.coner_name,
                                   t.patn_rlts,
                                   t.coner_addr,
                                   t.coner_tel,
                                   t.hi_type,
                                   t.insuplc,
                                   t.sp_psn_type,
                                   t.nwb_adm_type,
                                   t.nwb_bir_wt,
                                   t.nwb_adm_wt,
                                   t.opsp_diag_caty,
                                   t.opsp_mdtrt_date,
                                   t.ipt_med_type,
                                   t.adm_way,
                                   t.trt_type,
                                   t.adm_time,
                                   t.adm_caty,
                                   t.refldept_dept,
                                   t.dscg_time,
                                   t.dscg_caty,
                                   t.act_ipt_days,
                                   t.adm_ward,
                                   t.otp_wm_dise,
                                   t.wm_dise_code,
                                   t.otp_tcm_dise,
                                   t.tcm_dise_code,
                                   t.oprn_oprt_code_cnt,
                                   t.vent_used_dura,
                                   t.pwcry_bfadm_coma_dura,
                                   t.pwcry_afadm_coma_dura,
                                   t.bld_cat,
                                   t.bld_amt,
                                   t.bld_unt,
                                   t.spga_nurscare_days,
                                   t.lv1_nurscare_days,
                                   t.scd_nurscare_days,
                                   t.lv3_nurscare_days,
                                   t.dscg_way,
                                   t.acp_medins_name,
                                   t.acp_optins_code,
                                   t.bill_code,
                                   t.bill_no,
                                   t.biz_sn,
                                   t.days_rinp_flag_31,
                                   t.days_rinp_pup_31,
                                   t.chfpdr_name,
                                   t.chfpdr_code,
                                   t.setl_begn_date,
                                   t.setl_end_date,
                                   t.psn_selfpay,
                                   t.psn_ownpay,
                                   t.acct_pay,
                                   t.psn_cashpay,
                                   t.hi_paymtd,
                                   t.hsorg,
                                   t.hsorg_opter,
                                   t.medins_fill_dept,
                                   t.medins_fill_psn,
                                   t.prfs_name
                              from v_jsqd_setlinfo_LX t
                             where t.mdtrt_id = '{0}' ";

            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 2、基金支付信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetJsqdPayInfoLX(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select t.fund_pay_type,
                                     t.fund_payamt
                                from v_jsqd_payinfo_LX t
                               where t.mdtrt_id = '{0}' ";
            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 3、门诊慢特病诊断信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetJsqdOpspDiseinfoLX(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select  t.diag_name,
                                      t.diag_code,
                                      t.oprn_oprt_name,
                                      t.oprn_oprt_code
                                 from v_jsqd_opspdiseinfo_LX t
                                where t.mdtrt_id = '{0}' ";
            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 4、住院诊断信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetJsqdDiseInfoLX(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select t.diag_type,
                                     t.maindiag_flag,
                                     t.diag_code,
                                     t.diag_name,
                                     t.adm_cond_type
                               from v_jsqd_diseinfo_LX t
                              where t.mdtrt_id = '{0}'";
            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 5、收费项目信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetJsqdIteminfoLX(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select t.med_chrgitm,
                                     t.amt,
                                     t.claa_sumfee,
                                     t.clab_amt,
                                     t.fulamt_ownpay_amt,
                                     t.oth_amt
                                from v_jsqd_iteminfo_LX t
                               where t.mdtrt_id = '{0}' ";
            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 6、手术信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetJsqdOprnInfoLX(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select t.oprn_oprt_type,
                                     t.oprn_oprt_name,
                                     t.oprn_oprt_code,
                                     t.oprn_oprt_date,
                                     t.anst_way,
                                     t.oper_dr_name,
                                     t.oper_dr_code,
                                     t.anst_dr_name,
                                     t.anst_dr_code,
                                     t.main_oprn_flag
                                from v_jsqd_oprninfo_LX t
                               where t.mdtrt_id = '{0}' ";
            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 7、重症监护信息
        /// </summary>
        /// <param name="mdtrt"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetJsqdIcuInfoLX(string mdtrt, ref System.Data.DataTable dt)
        {
            #region sql
            string strSql = @"select t.scs_cutd_ward_type,
                                     t.scs_cutd_inpool_time,
                                     t.scs_cutd_exit_time,
                                     t.scs_cutd_sum_dura
                                from v_jsqd_icuinfo_LX t
                               where t.mdtrt_id = '{0}' ";
            #endregion

            try
            {
                strSql = string.Format(strSql, mdtrt);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                int ret = this.ExecQuery(strSql, ref ds);

                if (ret == -1)
                {
                    return -1;
                }

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        #endregion

        #region 更新上传

        /// <summary>
        /// 1、诊断diseinfo
        /// </summary>
        /// <param name="inptiantNO"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int setSiUploadFlagLX(string inptiantNO, string uploadFlag)
        {
            #region sql
            string strSql = "";

            #endregion

            try
            {
                strSql = string.Format(strSql, inptiantNO, uploadFlag);
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }
        #endregion

        #endregion

        #region 新医保
        #region 1301 sql
        public int QueryYB1301()
        {
            ArrayList al = new ArrayList();
            string sqlStr = "select count(1) from fin_yb_1301";
            try
            {
                string rs = this.ExecSqlReturnOne(sqlStr, "0");

                return int.Parse(rs);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int DeleteYB1301()
        {
            Class.Log log = new Class.Log();
            string strSql = "";
            #region sql
            strSql = @"delete  from  fin_yb_1301";
            #endregion
            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    log.WriteLog("sql " + strSql);
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                log.WriteLog("sql " + strSql);
                return -1;
            }
            return 1;
        }

        public int InsertYB1301(FS.HISFC.Models.SIInterface.YB_1301 yb1301)
        {
            #region sql
            string strSql = @"INSERT INTO fin_yb_1301 --西药中成药目录
                                  (med_list_codg, --医疗目录编码
                                    drug_prodname, --药品商品名
                                    genname_codg, --通用名编号
                                    drug_genname, --药品通用名
                                    chemname, --化学名称
                                    alis, --别名
                                    eng_name, --英文名称
                                    reg_name, --注册名称
                                    drugadm_strdcod, --药监本位码
                                    drug_dosform, --药品剂型
                                    drug_dosform_name, --药品剂型名称
                                    drug_type, --药品类别
                                    drug_type_name, --药品类别名称
                                    drug_spec, --药品规格
                                    drug_spec_code, --药品规格代码
                                    reg_dosform, --注册剂型
                                    reg_spec, --注册规格
                                    reg_spec_code, --注册规格代码
                                    each_dos, --每次用量
                                    used_frqu, --使用频次
                                    acdbas, --酸根盐基
                                    nat_drug_no, --国家药品编号
                                    used_mtd, --用法
                                    tcmpat_flag, --中成药标志
                                    prodplac_type, --生产地类别
                                    prodplac_type_name, --生产地类别名称
                                    prcunt_type, --计价单位类型
                                    otc_flag, --非处方药标志
                                    otc_flag_name, --非处方药标志名称
                                    pacmatl, --包装材质
                                    pacmatl_name, --包装材质名称
                                    pacspec, --包装规格
                                    pac_cnt, --包装数量
                                    efcc_atd, --功能主治
                                    rute, --给药途径
                                    manl, --说明书
                                    begndate, --开始日期
                                    enddate, --结束日期
                                    min_useunt, --最小使用单位
                                    min_salunt, --最小销售单位
                                    min_unt, --最小计量单位
                                    min_pac_cnt, --最小包装数量
                                    min_pacunt, --最小包装单位
                                    min_prepunt, --最小制剂单位
                                    min_pacunt_name, --最小包装单位名称
                                    min_prepunt_name, --最小制剂单位名称
                                    convrat, --转换比
                                    drug_expy, --药品有效期
                                    min_prcunt, --最小计价单位
                                    wubi, --五笔助记码
                                    pinyin, --拼音助记码
                                    subpck_fcty, --分包装厂家
                                    prodentp_code, --生产企业编号
                                    prodentp_name, --生产企业名称
                                    sp_lmtpric_drug_flag, --特殊限价药品标志
                                    sp_drug_flag, --特殊药品标志
                                    lmt_usescp, --限制使用范围
                                    lmt_used_flag, --限制使用标志
                                    drug_regcertno, --药品注册证号
                                    drug_regcert_begndate, --药品注册证号开始日期
                                    drug_regcert_enddate, --药品注册证号结束日期
                                    aprvno, --批准文号
                                    aprvno_begndate, --批准文号开始日期
                                    aprvno_enddate, --批准文号结束日期
                                    market_condition_code, --市场状态
                                    market_condition_name, --市场状态名称
                                    regdrug_elcword, --药品注册批件电子档案
                                    regdrug_add_elcword, --药品补充申请批件电子档案
                                    yb_drug_memo, --国家医保药品目录备注
                                    drugbase_flagname, --基本药物标志名称
                                    drugbase_flag, --基本药物标志
                                    vat__adjust_drugflag, --增值税调整药品标志
                                    vat__adjust_drugname, --增值税调整药品名称
                                    drugslist_onmarket, --上市药品目录集药品
                                    yb_negotiatedrug_flag, --医保谈判药品标志
                                    yb_negotiatedrug_name, --医保谈判药品名称
                                    nhc_drugcode, --卫健委药品编码
                                    memo, --备注
                                    vali_flag, --有效标志
                                    record_num, --唯一记录号
                                    create_time, --数据创建时间
                                    update_time, --数据更新时间
                                    ver_num, --版本号
                                    ver_name, --版本名称
                                    child_drug, --儿童用药
                                    company, --公司名称
                                    fda_about, --仿制药一致性评价药品
                                    distribution_enterprise, --经销企业
                                    de_linkname, --经销企业联系人
                                    elefile_authorization_de, --经销企业授权书电子档案
                                    yb_drug_catalogue, --国家医保药品目录剂型
                                    yb_drug_abtype--国家医保药品目录甲乙类标识

                                   
                                   )
                                VALUES
                                  (
                                    '{0}', --医疗目录编码
                                    '{1}', --药品商品名
                                    '{2}', --通用名编号
                                    '{3}', --药品通用名
                                    '{4}', --化学名称
                                    '{5}', --别名
                                    '{6}', --英文名称
                                    '{7}', --注册名称
                                    '{8}', --药监本位码
                                    '{9}', --药品剂型
                                    '{10}', --药品剂型名称
                                    '{11}', --药品类别
                                    '{12}', --药品类别名称
                                    '{13}', --药品规格
                                    '{14}', --药品规格代码
                                    '{15}', --注册剂型
                                    '{16}', --注册规格
                                    '{17}', --注册规格代码
                                    '{18}', --每次用量
                                    '{19}', --使用频次
                                    '{20}', --酸根盐基
                                    '{21}', --国家药品编号
                                    '{22}', --用法
                                    '{23}', --中成药标志
                                    '{24}', --生产地类别
                                    '{25}', --生产地类别名称
                                    '{26}', --计价单位类型
                                    '{27}', --非处方药标志
                                    '{28}', --非处方药标志名称
                                    '{29}', --包装材质
                                    '{30}', --包装材质名称
                                    '{31}', --包装规格
                                    '{32}', --包装数量
                                    '{33}', --功能主治
                                    '{34}', --给药途径
                                    '{35}', --说明书
                                     to_date('{36}','yyyy-mm-dd HH24:mi:ss'), --开始日期
                                     to_date('{37}','yyyy-mm-dd HH24:mi:ss'), --结束日期
                                    '{38}', --最小使用单位
                                    '{39}', --最小销售单位
                                    '{40}', --最小计量单位
                                    '{41}', --最小包装数量
                                    '{42}', --最小包装单位
                                    '{43}', --最小制剂单位
                                    '{44}', --最小包装单位名称
                                    '{45}', --最小制剂单位名称
                                    '{46}', --转换比
                                    to_date('{47}','yyyy-mm-dd HH24:mi:ss'), --药品有效期
                                    '{48}', --最小计价单位
                                    '{49}', --五笔助记码
                                    '{50}', --拼音助记码
                                    '{51}', --分包装厂家
                                    '{52}', --生产企业编号
                                    '{53}', --生产企业名称
                                    '{54}', --特殊限价药品标志
                                    '{55}', --特殊药品标志
                                    '{56}', --限制使用范围
                                    '{57}', --限制使用标志
                                    '{58}', --药品注册证号
                                    to_date('{59}','yyyy-mm-dd HH24:mi:ss'), --药品注册证号开始日期
                                    to_date('{60}','yyyy-mm-dd HH24:mi:ss'), --药品注册证号结束日期
                                    '{61}', --批准文号
                                    to_date('{62}','yyyy-mm-dd HH24:mi:ss'), --批准文号开始日期
                                    to_date('{63}','yyyy-mm-dd HH24:mi:ss'), --批准文号结束日期
                                    '{64}', --市场状态
                                    '{65}', --市场状态名称
                                    '{66}', --药品注册批件电子档案
                                    '{67}', --药品补充申请批件电子档案
                                    '{68}', --国家医保药品目录备注
                                    '{69}', --基本药物标志名称
                                    '{70}', --基本药物标志
                                    '{71}', --增值税调整药品标志
                                    '{72}', --增值税调整药品名称
                                    '{73}', --上市药品目录集药品
                                    '{74}', --医保谈判药品标志
                                    '{75}', --医保谈判药品名称
                                    '{76}', --卫健委药品编码
                                    '{77}', --备注
                                    '{78}', --有效标志
                                    '{79}', --唯一记录号
                                    to_date('{80}','yyyy-mm-dd HH24:mi:ss'), --数据创建时间
                                    to_date('{81}','yyyy-mm-dd HH24:mi:ss'), --数据更新时间
                                    '{82}', --版本号
                                    '{83}', --版本名称
                                    '{84}', --儿童用药
                                    '{85}', --公司名称
                                    '{86}', --仿制药一致性评价药品
                                    '{87}', --经销企业
                                    '{88}', --经销企业联系人
                                    '{89}', --经销企业授权书电子档案
                                    '{90}', --国家医保药品目录剂型
                                    '{91}' --国家医保药品目录甲乙类标识
                                   )
                                ";
            #endregion

            #region 赋值
            strSql = string.Format(strSql, yb1301.med_list_codg, //医疗目录编码
                                            yb1301.drug_prodname, //药品商品名
                                            yb1301.genname_codg, //通用名编号
                                            yb1301.drug_genname, //药品通用名
                                            yb1301.chemname, //化学名称
                                            yb1301.alis, //别名
                                            yb1301.eng_name, //英文名称
                                            yb1301.reg_name, //注册名称
                                            yb1301.drugadm_strdcod, //药监本位码
                                            yb1301.drug_dosform, //药品剂型
                                            yb1301.drug_dosform_name, //药品剂型名称
                                            yb1301.drug_type, //药品类别
                                            yb1301.drug_type_name, //药品类别名称
                                            yb1301.drug_spec, //药品规格
                                            yb1301.drug_spec_code, //药品规格代码
                                            yb1301.reg_dosform, //注册剂型
                                            yb1301.reg_spec, //注册规格
                                            yb1301.reg_spec_code, //注册规格代码
                                            yb1301.each_dos, //每次用量
                                            yb1301.used_frqu, //使用频次
                                            yb1301.acdbas, //酸根盐基
                                            yb1301.nat_drug_no, //国家药品编号
                                            yb1301.used_mtd, //用法
                                            yb1301.tcmpat_flag, //中成药标志
                                            yb1301.prodplac_type, //生产地类别
                                            yb1301.prodplac_type_name, //生产地类别名称
                                            yb1301.prcunt_type, //计价单位类型
                                            yb1301.otc_flag, //非处方药标志
                                            yb1301.otc_flag_name, //非处方药标志名称
                                            yb1301.pacmatl, //包装材质
                                            yb1301.pacmatl_name, //包装材质名称
                                            yb1301.pacspec, //包装规格
                                            yb1301.pac_cnt, //包装数量
                                            yb1301.efcc_atd, //功能主治
                                            yb1301.rute, //给药途径
                                            yb1301.manl, //说明书
                                            yb1301.begndate, //开始日期
                                            yb1301.enddate, //结束日期
                                            yb1301.min_useunt, //最小使用单位
                                            yb1301.min_salunt, //最小销售单位
                                            yb1301.min_unt, //最小计量单位
                                            yb1301.min_pac_cnt, //最小包装数量
                                            yb1301.min_pacunt, //最小包装单位
                                            yb1301.min_prepunt, //最小制剂单位
                                            yb1301.min_pacunt_name, //最小包装单位名称
                                            yb1301.min_prepunt_name, //最小制剂单位名称
                                            yb1301.convrat, //转换比
                                            yb1301.drug_expy, //药品有效期
                                            yb1301.min_prcunt, //最小计价单位
                                            yb1301.wubi, //五笔助记码
                                            yb1301.pinyin, //拼音助记码
                                            yb1301.subpck_fcty, //分包装厂家
                                            yb1301.prodentp_code, //生产企业编号
                                            yb1301.prodentp_name, //生产企业名称
                                            yb1301.sp_lmtpric_drug_flag, //特殊限价药品标志
                                            yb1301.sp_drug_flag, //特殊药品标志
                                            yb1301.lmt_usescp, //限制使用范围
                                            yb1301.lmt_used_flag, //限制使用标志
                                            yb1301.drug_regcertno, //药品注册证号
                                            yb1301.drug_regcert_begndate, //药品注册证号开始日期
                                            yb1301.drug_regcert_enddate, //药品注册证号结束日期
                                            yb1301.aprvno, //批准文号
                                            yb1301.aprvno_begndate, //批准文号开始日期
                                            yb1301.aprvno_enddate, //批准文号结束日期
                                            yb1301.market_condition_code, //市场状态
                                            yb1301.market_condition_name, //市场状态名称
                                            yb1301.regdrug_elcword, //药品注册批件电子档案
                                            yb1301.regdrug_add_elcword, //药品补充申请批件电子档案
                                            yb1301.yb_drug_memo, //国家医保药品目录备注
                                            yb1301.drugbase_flagname, //基本药物标志名称
                                            yb1301.drugbase_flag, //基本药物标志
                                            yb1301.vat__adjust_drugflag, //增值税调整药品标志
                                            yb1301.vat__adjust_drugname, //增值税调整药品名称
                                            yb1301.drugslist_onmarket, //上市药品目录集药品
                                            yb1301.yb_negotiatedrug_flag, //医保谈判药品标志
                                            yb1301.yb_negotiatedrug_name, //医保谈判药品名称
                                            yb1301.nhc_drugcode, //卫健委药品编码
                                            yb1301.memo, //备注
                                            yb1301.vali_flag, //有效标志
                                            yb1301.record_num, //唯一记录号
                                            yb1301.create_time, //数据创建时间
                                            yb1301.update_time, //数据更新时间
                                            yb1301.ver_num, //版本号
                                            yb1301.ver_name, //版本名称
                                            yb1301.child_drug, //儿童用药
                                            yb1301.company, //公司名称
                                            yb1301.fda_about, //仿制药一致性评价药品
                                            yb1301.distribution_enterprise, //经销企业
                                            yb1301.de_linkname, //经销企业联系人
                                            yb1301.elefile_authorization_de, //经销企业授权书电子档案
                                            yb1301.yb_drug_catalogue, //国家医保药品目录剂型
                                            yb1301.yb_drug_abtype//国家医保药品目录甲乙类标识
                                           );

            #endregion

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }

            return 1;
        }

        #endregion

        #region 1302 sql

        public int InsertYB1302(FS.HISFC.Models.SIInterface.YB_1302 yb1302)
        {
            #region sql
            string strSql = @"INSERT INTO fin_yb_1302 --医保信息住院主表
                                  ( med_list_codg, --医疗目录编码
                                    sindrug_name, --单味药名称
                                    cpnd_flag, --单复方标志
                                    qlt_lv, --质量等级
                                    tcmdrug_year, --中草药年份
                                    med_part, --药用部位
                                    safe_mtr, --安全计量
                                    cnvl_used, --常规用法
                                    natfla, --性味
                                    mertpm, --归经
                                    cat, --品种
                                    begndate, --开始日期
                                    enddate, --结束日期
                                    vali_flag, --有效标志
                                    record_num, --唯一记录号
                                    create_time, --数据创建时间
                                    update_time, --数据更新时间
                                    ver_num, --版本号
                                    ver_name, --版本名称
                                    drug_name, --药材名称
                                    efcc_atd, --功能主治
                                    processing_method, --炮制方法
                                    functional_classification, --功效分类
                                    source, --药材种来源
                                    yb_gj_paypolicy, --国家医保支付政策
                                    yb_sj_paypolicy, --省级医保支付政策
                                    standard_name, --标准名称
                                    standard_page, --标准页码
                                    standard_elcword--标准电子档案
                                   )
                                    VALUES
                                  (
                                    '{0}', --医疗目录编码
                                    '{1}', --单味药名称
                                    '{2}', --单复方标志
                                    '{3}', --质量等级
                                    '{4}', --中草药年份
                                    '{5}', --药用部位
                                    '{6}', --安全计量
                                    '{7}', --常规用法
                                    '{8}', --性味
                                    '{9}', --归经
                                    '{10}', --品种
                                    to_date('{11}','yyyy-mm-dd HH24:mi:ss'), --开始日期
                                    to_date('{12}','yyyy-mm-dd HH24:mi:ss'), --结束日期
                                    '{13}', --有效标志
                                    '{14}', --唯一记录号
                                    to_date('{15}','yyyy-mm-dd HH24:mi:ss'), --数据创建时间
                                    to_date('{16}','yyyy-mm-dd HH24:mi:ss'), --数据更新时间
                                    '{17}', --版本号
                                    '{18}', --版本名称
                                    '{19}', --药材名称
                                    '{20}', --功能主治
                                    '{21}', --炮制方法
                                    '{22}', --功效分类
                                    '{23}', --药材种来源
                                    '{24}', --国家医保支付政策
                                    '{25}', --省级医保支付政策
                                    '{26}', --标准名称
                                    '{27}', --标准页码
                                    '{28}' --标准电子档案
                                    )";
            #endregion

            #region 赋值
            strSql = string.Format(strSql, yb1302.med_list_codg, //医疗目录编码
                                            yb1302.sindrug_name, //单味药名称
                                            yb1302.cpnd_flag, //单复方标志
                                            yb1302.qlt_lv, //质量等级
                                            yb1302.tcmdrug_year, //中草药年份
                                            yb1302.med_part, //药用部位
                                            yb1302.safe_mtr, //安全计量
                                            yb1302.cnvl_used, //常规用法
                                            yb1302.natfla, //性味
                                            yb1302.mertpm, //归经
                                            yb1302.cat, //品种
                                            yb1302.begndate, //开始日期
                                            yb1302.enddate, //结束日期
                                            yb1302.vali_flag, //有效标志
                                            yb1302.record_num, //唯一记录号
                                            yb1302.create_time, //数据创建时间
                                            yb1302.update_time, //数据更新时间
                                            yb1302.ver_num, //版本号
                                            yb1302.ver_name, //版本名称
                                            yb1302.drug_name, //药材名称
                                            yb1302.efcc_atd, //功能主治
                                            yb1302.processing_method, //炮制方法
                                            yb1302.functional_classification, //功效分类
                                            yb1302.source, //药材种来源
                                            yb1302.yb_gj_paypolicy, //国家医保支付政策
                                            yb1302.yb_sj_paypolicy, //省级医保支付政策
                                            yb1302.standard_name, //标准名称
                                            yb1302.standard_page, //标准页码
                                            yb1302.standard_elcword//标准电子档案

                                           );

            #endregion

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }

            return 1;
        }

        public int QueryYB1302()
        {
            ArrayList al = new ArrayList();
            string sqlStr = "select count(1) from fin_yb_1302";
            try
            {
                string rs = this.ExecSqlReturnOne(sqlStr, "0");

                return int.Parse(rs);
            }
            catch (Exception e)
            {
                return 0;
            }
        }


        public int DeleteYB1302()
        {
            Class.Log log = new Class.Log();
            string strSql = "";
            #region sql
            strSql = @"delete  from  fin_yb_1302";
            #endregion
            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    log.WriteLog("sql " + strSql);
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                log.WriteLog("sql " + strSql);
                return -1;
            }
            return 1;
        }
        #endregion

        #region 1303 sql

        public int InsertYB1303(FS.HISFC.Models.SIInterface.YB_1303 yb1303)
        {
            #region sql
            string strSql = @"INSERT INTO fin_yb_1303 --医疗机构制剂目录
                                  ( med_list_codg, --医疗目录编码
                                    drug_prodname, --药品商品名
                                    alis, --别名
                                    eng_name, --英文名称
                                    dosform, --剂型
                                    dosform_name, --剂型名称
                                    reg_dosform, --注册剂型
                                    ing, --成分
                                    efcc_atd, --功能主治
                                    chrt, --性状
                                    drug_spec, --药品规格
                                    drug_spec_code, --药品规格代码
                                    reg_spec, --注册规格
                                    reg_spec_code, --注册规格代码
                                    rute, --给药途径
                                    stog, --贮藏
                                    used_frqu_dscr, --使用频次
                                    each_dos, --每次用量
                                    drug_tye, --药品类别
                                    drug_typename, --药品类别名称
                                    otc_flag, --非处方药标志
                                    otc_flag_name, --非处方药标志名称
                                    pacmatl, --包装材质
                                    pacmatl_name, --包装材质名称
                                    pacspec, --包装规格
                                    manl, --说明书
                                    pac_cnt, --包装数量
                                    min_useunt, --最小使用单位
                                    min_salunt, --最小销售单位
                                    min_unt,-- 最小计量单位
                                    min_pac_cnt,-- 最小包装数量
                                    min_pacunt,-- 最小包装单位
                                    min_prepunt,-- 最小制剂单位
                                    min_prepunt_name,-- 最小制剂单位名称
                                    drug_expy,-- 药品有效期
                                    min_prcunt,-- 最小计价单位
                                    defs,-- 不良反应
                                    mnan,-- 注意事项
                                    tabo,-- 禁忌
                                    manufacturer_num,-- 生产企业编号
                                    manufacturer_name,-- 生产企业名称
                                    manufacturer_add,-- 生产企业地址
                                    sp_lmtpric_drug_flag,-- 特殊限价药品标志
                                    aprvno,-- 批准文号
                                    aprvno_begndate,-- 批准文号开始日期
                                    aprvno_enddate,-- 批准文号结束日期
                                    drug_regcertno,-- 药品注册证号
                                    drug_regcert_begndate,-- 药品注册证号开始日期
                                    drug_regcert_enddate,-- 药品注册证号结束日期
                                    convrat,-- 转换比
                                    lmt_usescp,-- 限制使用范围
                                    minpackageuint_name,-- 最小包装单位名称
                                    reg_name,-- 注册名称
                                    subpck_fcty,-- 分包装厂家
                                    market_condition,-- 市场状态
                                    regdrug_elcword,-- 药品注册批件电子档案
                                    regdrug_add_elcword,-- 药品补充申请批件电子档案
                                    yb_drug_code,-- 国家医保药品目录编号
                                    yb_drug_memo,-- 国家医保药品目录备注
                                    vat_adjust_drugflag,-- 增值税调整药品标志
                                    vat_adjust_drugname,-- 增值税调整药品名称
                                    drugslist_onmarket,-- 上市药品目录集药品
                                    nhc_drugcode,-- 卫健委药品编码
                                    memo,-- 备注
                                    vali_flag,-- 有效标志
                                    begin_time,-- 开始时间
                                    end_time,-- 结束时间
                                    record_num,-- 唯一记录号
                                    create_time,-- 数据创建时间
                                    update_time,-- 数据更新时间
                                    ver_num,-- 版本号
                                    ver_name,-- 版本名称
                                    homebrew_license_no,-- 自制剂许可证号
                                    child_drug,-- 儿童用药
                                    gpatient_drug,-- 老年患者用药
                                    institution_linkname,-- 医疗机构联系人姓名
                                    institution_linktel,-- 医疗机构联系人电话
                                    homebrew_license_file --自制剂许可证电子档案

                                   )
                                    VALUES
                                  (
                                    '{0}', --医疗目录编码
                                    '{1}', --药品商品名
                                    '{2}', --别名
                                    '{3}', --英文名称
                                    '{4}', --剂型
                                    '{5}', --剂型名称
                                    '{6}', --注册剂型
                                    '{7}', --成分
                                    '{8}', --功能主治
                                    '{9}', --性状
                                    '{10}', --药品规格
                                    '{11}', --药品规格代码
                                    '{12}', --注册规格
                                    '{13}', --注册规格代码
                                    '{14}', --给药途径
                                    '{15}', --贮藏
                                    '{16}', --使用频次
                                    '{17}', --每次用量
                                    '{18}', --药品类别
                                    '{19}', --药品类别名称
                                    '{20}', --非处方药标志
                                    '{21}', --非处方药标志名称
                                    '{22}', --包装材质
                                    '{23}', --包装材质名称
                                    '{24}', --包装规格
                                    '{25}', --说明书
                                    '{26}', --包装数量
                                    '{27}', --最小使用单位
                                    '{28}', --最小销售单位
                                    '{29}', --最小计量单位
                                    '{30}', --最小包装数量
                                    '{31}', --最小包装单位
                                    '{32}', --最小制剂单位
                                    '{33}', --最小制剂单位名称
                                    to_date('{34}','yyyy-mm-dd HH24:mi:ss'), --药品有效期
                                    '{35}', --最小计价单位
                                    '{36}', --不良反应
                                    '{37}', --注意事项
                                    '{38}', --禁忌
                                    '{39}', --生产企业编号
                                    '{40}', --生产企业名称
                                    '{41}', --生产企业地址
                                    '{42}', --特殊限价药品标志
                                    '{43}', --批准文号
                                    to_date('{44}','yyyy-mm-dd HH24:mi:ss'), --批准文号开始日期
                                    to_date('{45}','yyyy-mm-dd HH24:mi:ss'), --批准文号结束日期
                                    '{46}', --药品注册证号
                                    to_date('{47}','yyyy-mm-dd HH24:mi:ss'), --药品注册证号开始日期
                                    to_date('{48}','yyyy-mm-dd HH24:mi:ss'), --药品注册证号结束日期
                                    '{49}', --转换比
                                    '{50}', --限制使用范围
                                    '{51}', --最小包装单位名称
                                    '{52}', --注册名称
                                    '{53}', --分包装厂家
                                    '{54}', --市场状态
                                    '{55}', --药品注册批件电子档案
                                    '{56}', --药品补充申请批件电子档案
                                    '{57}', --国家医保药品目录编号
                                    '{58}', --国家医保药品目录备注
                                    '{59}', --增值税调整药品标志
                                    '{60}', --增值税调整药品名称
                                    '{61}', --上市药品目录集药品
                                    '{62}', --卫健委药品编码
                                    '{63}', --备注
                                    '{64}', --有效标志
                                    to_date('{65}','yyyy-mm-dd HH24:mi:ss'), --开始时间
                                    to_date('{66}','yyyy-mm-dd HH24:mi:ss'), --结束时间
                                    '{67}', --唯一记录号
                                    to_date('{68}','yyyy-mm-dd HH24:mi:ss'), --数据创建时间
                                    to_date('{69}','yyyy-mm-dd HH24:mi:ss'), --数据更新时间
                                    '{70}', --版本号
                                    '{71}', --版本名称
                                    '{72}', --自制剂许可证号
                                    '{73}', --儿童用药
                                    '{74}', --老年患者用药
                                    '{75}', --医疗机构联系人姓名
                                    '{76}', --医疗机构联系人电话
                                    '{77}' --自制剂许可证电子档案
                                    )";
            #endregion

            #region 赋值
            strSql = string.Format(strSql, yb1303.med_list_codg, //医疗目录编码
                                            yb1303.drug_prodname, //药品商品名
                                            yb1303.alis, //别名
                                            yb1303.eng_name, //英文名称
                                            yb1303.dosform, //剂型
                                            yb1303.dosform_name, //剂型名称
                                            yb1303.reg_dosform, //注册剂型
                                            yb1303.ing, //成分
                                            yb1303.efcc_atd, //功能主治
                                            yb1303.chrt, //性状
                                            yb1303.drug_spec, //药品规格
                                            yb1303.drug_spec_code, //药品规格代码
                                            yb1303.reg_spec, //注册规格
                                            yb1303.reg_spec_code, //注册规格代码
                                            yb1303.rute, //给药途径
                                            yb1303.stog, //贮藏
                                            yb1303.used_frqu_dscr, //使用频次
                                            yb1303.each_dos, //每次用量
                                            yb1303.drug_tye, //药品类别
                                            yb1303.drug_typename, //药品类别名称
                                            yb1303.otc_flag, //非处方药标志
                                            yb1303.otc_flag_name, //非处方药标志名称
                                            yb1303.pacmatl, //包装材质
                                            yb1303.pacmatl_name, //包装材质名称
                                            yb1303.pacspec, //包装规格
                                            yb1303.manl, //说明书
                                            yb1303.pac_cnt, //包装数量
                                            yb1303.min_useunt, //最小使用单位
                                            yb1303.min_salunt, //最小销售单位
                                            yb1303.min_unt, //最小计量单位
                                            yb1303.min_pac_cnt, //最小包装数量
                                            yb1303.min_pacunt, //最小包装单位
                                            yb1303.min_prepunt, //最小制剂单位
                                            yb1303.min_prepunt_name, //最小制剂单位名称
                                            yb1303.drug_expy, //药品有效期
                                            yb1303.min_prcunt, //最小计价单位
                                            yb1303.defs, //不良反应
                                            yb1303.mnan, //注意事项
                                            yb1303.tabo, //禁忌
                                            yb1303.manufacturer_num, //生产企业编号
                                            yb1303.manufacturer_name, //生产企业名称
                                            yb1303.manufacturer_add, //生产企业地址
                                            yb1303.sp_lmtpric_drug_flag, //特殊限价药品标志
                                            yb1303.aprvno, //批准文号
                                            yb1303.aprvno_begndate, //批准文号开始日期
                                            yb1303.aprvno_enddate, //批准文号结束日期
                                            yb1303.drug_regcertno, //药品注册证号
                                            yb1303.drug_regcert_begndate, //药品注册证号开始日期
                                            yb1303.drug_regcert_enddate, //药品注册证号结束日期
                                            yb1303.convrat, //转换比
                                            yb1303.lmt_usescp, //限制使用范围
                                            yb1303.minpackageuint_name, //最小包装单位名称
                                            yb1303.reg_name, //注册名称
                                            yb1303.subpck_fcty, //分包装厂家
                                            yb1303.market_condition, //市场状态
                                            yb1303.regdrug_elcword, //药品注册批件电子档案
                                            yb1303.regdrug_add_elcword, //药品补充申请批件电子档案
                                            yb1303.yb_drug_code, //国家医保药品目录编号
                                            yb1303.yb_drug_memo, //国家医保药品目录备注
                                            yb1303.vat_adjust_drugflag, //增值税调整药品标志
                                            yb1303.vat_adjust_drugname, //增值税调整药品名称
                                            yb1303.drugslist_onmarket, //上市药品目录集药品
                                            yb1303.nhc_drugcode, //卫健委药品编码
                                            yb1303.memo, //备注
                                            yb1303.vali_flag, //有效标志
                                            yb1303.begin_time, //开始时间
                                            yb1303.end_time, //结束时间
                                            yb1303.record_num, //唯一记录号
                                            yb1303.create_time, //数据创建时间
                                            yb1303.update_time, //数据更新时间
                                            yb1303.ver_num, //版本号
                                            yb1303.ver_name, //版本名称
                                            yb1303.homebrew_license_no, //自制剂许可证号
                                            yb1303.child_drug, //儿童用药
                                            yb1303.gpatient_drug, //老年患者用药
                                            yb1303.institution_linkname, //医疗机构联系人姓名
                                            yb1303.institution_linktel, //医疗机构联系人电话
                                            yb1303.homebrew_license_file//自制剂许可证电子档案
                                           );

            #endregion

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }

            return 1;
        }

        public int QueryYB1303()
        {
            ArrayList al = new ArrayList();
            string sqlStr = "select count(1) from fin_yb_1303";
            try
            {
                string rs = this.ExecSqlReturnOne(sqlStr, "0");

                return int.Parse(rs);
            }
            catch (Exception e)
            {
                return 0;
            }
        }


        public int DeleteYB1303()
        {
            Class.Log log = new Class.Log();
            string strSql = "";
            #region sql
            strSql = @"delete  from  fin_yb_1303";
            #endregion
            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    log.WriteLog("sql " + strSql);
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                log.WriteLog("sql " + strSql);
                return -1;
            }
            return 1;
        }
        #endregion

        #region 1305 sql

        public int InsertYB1305(FS.HISFC.Models.SIInterface.YB_1305 yb1305)
        {
            #region sql
            string strSql = @"INSERT INTO fin_yb_1305 --医保信息住院主表
                                  ( med_list_codg, --医疗目录编码
                                    prcunt, --计价单位
                                    prcunt_name, --计价单位名称
                                    trt_item_dscr, --诊疗项目说明
                                    trt_exct_cont, --诊疗除外内容
                                    trt_item_cont, --诊疗项目内涵
                                    vali_flag, --有效标志
                                    memo, --备注
                                    servitem_type, --服务项目类别
                                    servitem_name, --医疗服务项目名称
                                    item_name, --项目说明
                                    begin_time, --开始日期
                                    end_time, --结束日期
                                    record_num, --唯一记录号
                                    ver_num, --版本号
                                    ver_name--版本名称

                                   )
                                    VALUES
                                  (
                                    '{0}', --医疗目录编码
                                    '{1}', --计价单位
                                    '{2}', --计价单位名称
                                    '{3}', --诊疗项目说明
                                    '{4}', --诊疗除外内容
                                    '{5}', --诊疗项目内涵
                                    '{6}', --有效标志
                                    '{7}', --备注
                                    '{8}', --服务项目类别
                                    '{9}', --医疗服务项目名称
                                    '{10}', --项目说明
                                    to_date('{11}','yyyy-mm-dd HH24:mi:ss'), --开始日期
                                    to_date('{12}','yyyy-mm-dd HH24:mi:ss'), --结束日期
                                    '{13}', --唯一记录号
                                    '{14}', --版本号
                                    '{15}' --版本名
                                    )";
            #endregion

            #region 赋值
            strSql = string.Format(strSql, yb1305.med_list_codg, //医疗目录编码
                                            yb1305.prcunt, //计价单位
                                            yb1305.prcunt_name, //计价单位名称
                                            yb1305.trt_item_dscr, //诊疗项目说明
                                            yb1305.trt_exct_cont, //诊疗除外内容
                                            yb1305.trt_item_cont, //诊疗项目内涵
                                            yb1305.vali_flag, //有效标志
                                            yb1305.memo, //备注
                                            yb1305.servitem_type, //服务项目类别
                                            yb1305.servitem_name, //医疗服务项目名称
                                            yb1305.item_name, //项目说明
                                            yb1305.begin_time, //开始日期
                                            yb1305.end_time, //结束日期
                                            yb1305.record_num, //唯一记录号
                                            yb1305.ver_num, //版本号
                                            yb1305.ver_name//版本名称


                                           );

            #endregion

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }

            return 1;
        }

        public int QueryYB1305()
        {
            ArrayList al = new ArrayList();
            string sqlStr = "select count(1) from fin_yb_1305";
            try
            {
                string rs = this.ExecSqlReturnOne(sqlStr, "0");

                return int.Parse(rs);
            }
            catch (Exception e)
            {
                return 0;
            }
        }


        public int DeleteYB1305()
        {
            Class.Log log = new Class.Log();
            string strSql = "";
            #region sql
            strSql = @"delete  from  fin_yb_1305";
            #endregion
            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    log.WriteLog("sql " + strSql);
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                log.WriteLog("sql " + strSql);
                return -1;
            }
            return 1;
        }
        #endregion

        #region 1306 sql

        public int InsertYB1306(FS.HISFC.Models.SIInterface.YB_1306 yb1306)
        {
            #region sql
            string strSql = @"INSERT INTO fin_yb_1306 --医用耗材目录
                                  ( wm_dise_id, --医疗目录编码
                                    cpr, --耗材名称
                                    cpr_code_scp, --医疗器械唯一标识码
                                    cpr_name, --医保通用名代码
                                    sec_code_scp, --医保通用名
                                    prod_mol, --产品型号
                                    spec_code, --规格代码
                                    spec, --规格
                                    mcs_type, --耗材分类
                                    spec_mol, --规格型号
                                    dise_code, --材质代码
                                    mcs_matl, --耗材材质
                                    pacspec, --包装规格
                                    pac_cnt, --包装数量
                                    prod_pacmatl, --产品包装材质
                                    pacunt, --包装单位
                                    prod_convrat, --产品转换比
                                    min_useunt, --最小使用单位
                                    prodplac_type, --生产地类别
                                    prodplac_name, --生产地类别名称
                                    cpbz, --产品标准
                                    prodexpy, --产品有效期
                                    xnjgyzc, --性能结构与组成
                                    syfw, --适用范围
                                    cpsyff, --产品使用方法
                                    cptpbh, --产品图片编号
                                    cpzlbz, --产品质量标准
                                    manl, --说明书
                                    qtzmcl, --其他证明材料
                                    zjzybz, --专机专用标志
                                    zj_name, --专机名称
                                    zt_name, --组套名称
                                    zt_flag, --机套标志
                                    lmt_used_flag, --限制使用标志
                                    lmt_usescp, --医保限用范围
                                    min_salunt, --最小销售单位
                                    highval_mcs_flag, --高值耗材标志
                                    yyclfl_code, --医用材料分类代码
                                    impt_matl_hmorgn_flag, --植入材料和人体器官标志
                                    mj_flag, --灭菌标志
                                    mj_name, --灭菌标志名称
                                    impt_itvt_clss_flag, --植入或介入类标志
                                    impt_itvt_clss_name, --植入或介入类名称
                                    dspo_used_flag, --一次性使用标志
                                    dspo_used_name, --一次性使用标志名称
                                    rzcbar_name, --注册备案人名称
                                    begndate, --开始日期
                                    enddate, --结束日期
                                    ylqxgllb_flag, --医疗器械管理类别
                                    ylqxgllb_name, --医疗器械管理类别名称
                                    reg_fil_no, --注册备案号
                                    reg_fil_name, --注册备案产品名称
                                    jgjzc, --结构及组成
                                    qtnr, --其他内容
                                    aprv_date, --批准日期
                                    zcbar_addr, --注册备案人住所
                                    zcz_begndate, --注册证有效期开始时间
                                    zcz_enddate, --注册证有效期结束时间
                                    scqy_code, --生产企业编号
                                    scqy_name, --生产企业名称
                                    sc_addr, --生产地址
                                    dlrqy, --代理人企业
                                    dlrqy_addr, --代理人企业地址
                                    scghdq, --生产国或地区
                                    shfwjg, --售后服务机构
                                    zchbazdzda, --注册或备案证电子档案
                                    cpyx, --产品影像
                                    valid_state, --有效标志
                                    wyjlh, --唯一记录号
                                    ver, --版本号
                                    ver_name--版本名称
                                   )
                                    VALUES
                                  (
                                      '{0}', --医疗目录编码
                                      '{1}', --耗材名称
                                      '{2}', --医疗器械唯一标识码
                                      '{3}', --医保通用名代码
                                      '{4}', --医保通用名
                                      '{5}', --产品型号
                                      '{6}', --规格代码
                                      '{7}', --规格
                                      '{8}', --耗材分类
                                      '{9}', --规格型号
                                      '{10}', --材质代码
                                      '{11}', --耗材材质
                                      '{12}', --包装规格
                                      '{13}', --包装数量
                                      '{14}', --产品包装材质
                                      '{15}', --包装单位
                                      '{16}', --产品转换比
                                      '{17}', --最小使用单位
                                      '{18}', --生产地类别
                                      '{19}', --生产地类别名称
                                      '{20}', --产品标准
                                      to_date('{21}','yyyy-mm-dd HH24:mi:ss'), --产品有效期
                                      '{22}', --性能结构与组成
                                      '{23}', --适用范围
                                      '{24}', --产品使用方法
                                      '{25}', --产品图片编号
                                      '{26}', --产品质量标准
                                      '{27}', --说明书
                                      '{28}', --其他证明材料
                                      '{29}', --专机专用标志
                                      '{30}', --专机名称
                                      '{31}', --组套名称
                                      '{32}', --机套标志
                                      '{33}', --限制使用标志
                                      '{34}', --医保限用范围
                                      '{35}', --最小销售单位
                                      '{36}', --高值耗材标志
                                      '{37}', --医用材料分类代码
                                      '{38}', --植入材料和人体器官标志
                                      '{39}', --灭菌标志
                                      '{40}', --灭菌标志名称
                                      '{41}', --植入或介入类标志
                                      '{42}', --植入或介入类名称
                                      '{43}', --一次性使用标志
                                      '{44}', --一次性使用标志名称
                                      '{45}', --注册备案人名称
                                      to_date('{46}','yyyy-mm-dd HH24:mi:ss'), --开始日期
                                      to_date('{47}','yyyy-mm-dd HH24:mi:ss'), --结束日期
                                      '{48}', --医疗器械管理类别
                                      '{49}', --医疗器械管理类别名称
                                      '{50}', --注册备案号
                                      '{51}', --注册备案产品名称
                                      '{52}', --结构及组成
                                      '{53}', --其他内容
                                       to_date('{54}','yyyy-mm-dd HH24:mi:ss'), --批准日期
                                      '{55}', --注册备案人住所
                                      to_date('{56}','yyyy-mm-dd HH24:mi:ss'), --注册证有效期开始时间
                                      to_date('{57}','yyyy-mm-dd HH24:mi:ss'), --注册证有效期结束时间
                                      '{58}', --生产企业编号
                                      '{59}', --生产企业名称
                                      '{60}', --生产地址
                                      '{61}', --代理人企业
                                      '{62}', --代理人企业地址
                                      '{63}', --生产国或地区
                                      '{64}', --售后服务机构
                                      '{65}', --注册或备案证电子档案
                                      '{66}', --产品影像
                                      '{67}', --有效标志
                                      '{68}', --唯一记录号
                                      '{69}', --版本号
                                      '{70}' --版本名称

                                    )";
            #endregion

            #region 赋值
            strSql = string.Format(strSql, yb1306.wm_dise_id, //医疗目录编码
                                            yb1306.cpr, //耗材名称
                                            yb1306.cpr_code_scp, //医疗器械唯一标识码
                                            yb1306.cpr_name, //医保通用名代码
                                            yb1306.sec_code_scp, //医保通用名
                                            yb1306.prod_mol, //产品型号
                                            yb1306.spec_code, //规格代码
                                            yb1306.spec, //规格
                                            yb1306.mcs_type, //耗材分类
                                            yb1306.spec_mol, //规格型号
                                            yb1306.dise_code, //材质代码
                                            yb1306.mcs_matl, //耗材材质
                                            yb1306.pacspec, //包装规格
                                            yb1306.pac_cnt, //包装数量
                                            yb1306.prod_pacmatl, //产品包装材质
                                            yb1306.pacunt, //包装单位
                                            yb1306.prod_convrat, //产品转换比
                                            yb1306.min_useunt, //最小使用单位
                                            yb1306.prodplac_type, //生产地类别
                                            yb1306.prodplac_name, //生产地类别名称
                                            yb1306.cpbz, //产品标准
                                            yb1306.prodexpy, //产品有效期
                                            yb1306.xnjgyzc, //性能结构与组成
                                            yb1306.syfw, //适用范围
                                            yb1306.cpsyff, //产品使用方法
                                            yb1306.cptpbh, //产品图片编号
                                            yb1306.cpzlbz, //产品质量标准
                                            yb1306.manl, //说明书
                                            yb1306.qtzmcl, //其他证明材料
                                            yb1306.zjzybz, //专机专用标志
                                            yb1306.zj_name, //专机名称
                                            yb1306.zt_name, //组套名称
                                            yb1306.zt_flag, //机套标志
                                            yb1306.lmt_used_flag, //限制使用标志
                                            yb1306.lmt_usescp, //医保限用范围
                                            yb1306.min_salunt, //最小销售单位
                                            yb1306.highval_mcs_flag, //高值耗材标志
                                            yb1306.yyclfl_code, //医用材料分类代码
                                            yb1306.impt_matl_hmorgn_flag, //植入材料和人体器官标志
                                            yb1306.mj_flag, //灭菌标志
                                            yb1306.mj_name, //灭菌标志名称
                                            yb1306.impt_itvt_clss_flag, //植入或介入类标志
                                            yb1306.impt_itvt_clss_name, //植入或介入类名称
                                            yb1306.dspo_used_flag, //一次性使用标志
                                            yb1306.dspo_used_name, //一次性使用标志名称
                                            yb1306.rzcbar_name, //注册备案人名称
                                            yb1306.begndate, //开始日期
                                            yb1306.enddate, //结束日期
                                            yb1306.ylqxgllb_flag, //医疗器械管理类别
                                            yb1306.ylqxgllb_name, //医疗器械管理类别名称
                                            yb1306.reg_fil_no, //注册备案号
                                            yb1306.reg_fil_name, //注册备案产品名称
                                            yb1306.jgjzc, //结构及组成
                                            yb1306.qtnr, //其他内容
                                            yb1306.aprv_date, //批准日期
                                            yb1306.zcbar_addr, //注册备案人住所
                                            yb1306.zcz_begndate, //注册证有效期开始时间
                                            yb1306.zcz_enddate, //注册证有效期结束时间
                                            yb1306.scqy_code, //生产企业编号
                                            yb1306.scqy_name, //生产企业名称
                                            yb1306.sc_addr, //生产地址
                                            yb1306.dlrqy, //代理人企业
                                            yb1306.dlrqy_addr, //代理人企业地址
                                            yb1306.scghdq, //生产国或地区
                                            yb1306.shfwjg, //售后服务机构
                                            yb1306.zchbazdzda, //注册或备案证电子档案
                                            yb1306.cpyx, //产品影像
                                            yb1306.valid_state, //有效标志
                                            yb1306.wyjlh, //唯一记录号
                                            yb1306.ver, //版本号
                                            yb1306.ver_name//版本名称

                                           );

            #endregion

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }

            return 1;
        }

        public int QueryYB1306()
        {
            ArrayList al = new ArrayList();
            string sqlStr = "select count(1) from fin_yb_1306";
            try
            {
                string rs = this.ExecSqlReturnOne(sqlStr, "0");

                return int.Parse(rs);
            }
            catch (Exception e)
            {
                return 0;
            }
        }


        public int DeleteYB1306()
        {
            Class.Log log = new Class.Log();
            string strSql = "";
            #region sql
            strSql = @"delete  from  fin_yb_1306";
            #endregion
            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    log.WriteLog("sql " + strSql);
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                log.WriteLog("sql " + strSql);
                return -1;
            }
            return 1;
        }
        #endregion

        #region 1307 sql

        public int InsertYB1307(FS.HISFC.Models.SIInterface.YB_1307 yb1307)
        {
            #region sql
            string strSql = @"INSERT INTO fin_yb_1307 --医用耗材目录
                                  ( wm_dise_id,--西医疾病诊断id
                                    cpr,--章
                                    cpr_code_scp,--章代码范围
                                    cpr_name,--章名称
                                    sec_code_scp,--节代码范围
                                    sec_name,--节名称
                                    cgy_code,--类目代码
                                    cgy_name,--类目名称
                                    sor_code,--亚目代码
                                    sor_name,--亚目名称
                                    dise_code,--诊断代码
                                    dise_name,--诊断名称
                                    used_std,--使用标记
                                    gb_dise_code,--国标版诊断代码
                                    gb_dise_name,--国标版诊断名称
                                    lc_dise_code,--临床版诊断代码
                                    lc_dise_name,--临床版诊断名称
                                    mark,--备注
                                    valid_state,--有效标志
                                    wyjlh,--唯一记录号
                                    oper_date,--数据创建时间
                                    update_date,--数据更新时间
                                    ver,--版本号
                                    ver_name--版本名称

                                   )
                                    VALUES
                                  (
                                          '{0}', --西医疾病诊断id
                                          '{1}', --章
                                          '{2}', --章代码范围
                                          '{3}', --章名称
                                          '{4}', --节代码范围
                                          '{5}', --节名称
                                          '{6}', --类目代码
                                          '{7}', --类目名称
                                          '{8}', --亚目代码
                                          '{9}', --亚目名称
                                          '{10}', --诊断代码
                                          '{11}', --诊断名称
                                          '{12}', --使用标记
                                          '{13}', --国标版诊断代码
                                          '{14}', --国标版诊断名称
                                          '{15}', --临床版诊断代码
                                          '{16}', --临床版诊断名称
                                          '{17}', --备注
                                          '{18}', --有效标志
                                          '{19}', --唯一记录号
                                          to_date('{20}','yyyy-mm-dd HH24:mi:ss'), --数据创建时间
                                          to_date('{21}','yyyy-mm-dd HH24:mi:ss'), --数据更新时间
                                          '{22}', --版本号
                                          '{23}' --版本名称

                                    )";
            #endregion

            #region 赋值
            strSql = string.Format(strSql, yb1307.wm_dise_id,//西医疾病诊断id
                                            yb1307.cpr,//章
                                            yb1307.cpr_code_scp,//章代码范围
                                            yb1307.cpr_name,//章名称
                                            yb1307.sec_code_scp,//节代码范围
                                            yb1307.sec_name,//节名称
                                            yb1307.cgy_code,//类目代码
                                            yb1307.cgy_name,//类目名称
                                            yb1307.sor_code,//亚目代码
                                            yb1307.sor_name,//亚目名称
                                            yb1307.dise_code,//诊断代码
                                            yb1307.dise_name,//诊断名称
                                            yb1307.used_std,//使用标记
                                            yb1307.gb_dise_code,//国标版诊断代码
                                            yb1307.gb_dise_name,//国标版诊断名称
                                            yb1307.lc_dise_code,//临床版诊断代码
                                            yb1307.lc_dise_name,//临床版诊断名称
                                            yb1307.mark,//备注
                                            yb1307.valid_state,//有效标志
                                            yb1307.wyjlh,//唯一记录号
                                            yb1307.oper_date,//数据创建时间
                                            yb1307.update_date,//数据更新时间
                                            yb1307.ver,//版本号
                                            yb1307.ver_name//版本名称
                                           );

            #endregion

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }

            return 1;
        }

        public int QueryYB1307()
        {
            ArrayList al = new ArrayList();
            string sqlStr = "select count(1) from fin_yb_1307";
            try
            {
                string rs = this.ExecSqlReturnOne(sqlStr, "0");

                return int.Parse(rs);
            }
            catch (Exception e)
            {
                return 0;
            }
        }


        public int DeleteYB1307()
        {
            Class.Log log = new Class.Log();
            string strSql = "";
            #region sql
            strSql = @"delete  from  fin_yb_1307";
            #endregion
            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    log.WriteLog("sql " + strSql);
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                log.WriteLog("sql " + strSql);
                return -1;
            }
            return 1;
        }
        #endregion

        #region 1308 sql

        public int InsertYB1308(FS.HISFC.Models.SIInterface.YB_1308 yb1308)
        {
            #region sql
            string strSql = @"INSERT INTO fin_yb_1308 --医用耗材目录
                                  ( ssbzml_id, --手术标准目录id
                                    cpr, --章
                                    cpr_code_scp, --章代码范围
                                    cpr_name, --章名称
                                    cgy_code, --类目代码
                                    cgy_name, --类目名称
                                    sor_code, --亚目代码
                                    sor_name, --亚目名称
                                    xm_code, --细目代码
                                    xm_name, --细目名称
                                    oprn_oprt_code, --手术操作代码
                                    oprn_oprt_name, --手术操作名称
                                    used_std, --使用标记
                                    tb_oprn_oprt_code, --团标版手术操作代码
                                    tb_oprn_oprt_name, --团标版手术操作名称
                                    lc_oprn_oprt_code, --临床版手术操作代码
                                    lc_oprn_oprt_name, --临床版手术操作名称
                                    mark, --备注
                                    valid_state, --有效标志
                                    wyjlh, --唯一记录号
                                    oper_date, --数据创建时间
                                    update_date, --数据更新时间
                                    ver, --版本号
                                    ver_name--版本名称


                                   )
                                    VALUES
                                  (
                                      '{0}', --手术标准目录id
                                      '{1}', --章
                                      '{2}', --章代码范围
                                      '{3}', --章名称
                                      '{4}', --类目代码
                                      '{5}', --类目名称
                                      '{6}', --亚目代码
                                      '{7}', --亚目名称
                                      '{8}', --细目代码
                                      '{9}', --细目名称
                                      '{10}', --手术操作代码
                                      '{11}', --手术操作名称
                                      '{12}', --使用标记
                                      '{13}', --团标版手术操作代码
                                      '{14}', --团标版手术操作名称
                                      '{15}', --临床版手术操作代码
                                      '{16}', --临床版手术操作名称
                                      '{17}', --备注
                                      '{18}', --有效标志
                                      '{19}', --唯一记录号
                                      to_date('{20}','yyyy-mm-dd HH24:mi:ss'), --数据创建时间
                                      to_date('{21}','yyyy-mm-dd HH24:mi:ss'), --数据更新时间
                                      '{22}', --版本号
                                      '{23}' --版本名称


                                    )";
            #endregion

            #region 赋值
            strSql = string.Format(strSql, yb1308.ssbzml_id, //手术标准目录id
                                            yb1308.cpr, //章
                                            yb1308.cpr_code_scp, //章代码范围
                                            yb1308.cpr_name, //章名称
                                            yb1308.cgy_code, //类目代码
                                            yb1308.cgy_name, //类目名称
                                            yb1308.sor_code, //亚目代码
                                            yb1308.sor_name, //亚目名称
                                            yb1308.xm_code, //细目代码
                                            yb1308.xm_name, //细目名称
                                            yb1308.oprn_oprt_code, //手术操作代码
                                            yb1308.oprn_oprt_name, //手术操作名称
                                            yb1308.used_std, //使用标记
                                            yb1308.tb_oprn_oprt_code, //团标版手术操作代码
                                            yb1308.tb_oprn_oprt_name, //团标版手术操作名称
                                            yb1308.lc_oprn_oprt_code, //临床版手术操作代码
                                            yb1308.lc_oprn_oprt_name, //临床版手术操作名称
                                            yb1308.mark, //备注
                                            yb1308.valid_state, //有效标志
                                            yb1308.wyjlh, //唯一记录号
                                            yb1308.oper_date, //数据创建时间
                                            yb1308.update_date, //数据更新时间
                                            yb1308.ver, //版本号
                                            yb1308.ver_name//版本名称

                                           );

            #endregion

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }

            return 1;
        }

        public int QueryYB1308()
        {
            ArrayList al = new ArrayList();
            string sqlStr = "select count(1) from fin_yb_1308";
            try
            {
                string rs = this.ExecSqlReturnOne(sqlStr, "0");

                return int.Parse(rs);
            }
            catch (Exception e)
            {
                return 0;
            }
        }


        public int DeleteYB1308()
        {
            Class.Log log = new Class.Log();
            string strSql = "";
            #region sql
            strSql = @"delete  from  fin_yb_1308";
            #endregion
            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    log.WriteLog("sql " + strSql);
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                log.WriteLog("sql " + strSql);
                return -1;
            }
            return 1;
        }
        #endregion

        #region 1309 sql

        public int InsertYB1309(FS.HISFC.Models.SIInterface.YB_1309 yb1309)
        {
            #region sql
            string strSql = @"INSERT INTO fin_yb_1309 --医用耗材目录
                                  ( mmmtbzml_code, --门慢门特病种目录代码
                                    mmmtbzdl_name, --门慢门特病种大类名称
                                    mmmtbzxfl_code, --门慢门特病种细分类名称
                                    ybqf, --医保区划
                                    mark, --备注
                                    valid_state, --有效标志
                                    wyjlh, --唯一记录号
                                    oper_date, --数据创建时间
                                    update_date, --数据更新时间
                                    ver, --版本号
                                    bznh, --病种内涵
                                    ver_name, --版本名称
                                    zlznym, --诊疗指南页码
                                    zlzndzda, --诊疗指南电子档案
                                    mmmtbz_name, --门慢门特病种名称
                                    mmmtbzdl_code--门慢门特病种大类代码



                                   )
                                    VALUES
                                  (
                                      '{0}', --门慢门特病种目录代码
                                      '{1}', --门慢门特病种大类名称
                                      '{2}', --门慢门特病种细分类名称
                                      '{3}', --医保区划
                                      '{4}', --备注
                                      '{5}', --有效标志
                                      '{6}', --唯一记录号
                                      to_date('{7}','yyyy-mm-dd HH24:mi:ss'), --数据创建时间
                                      to_date('{8}','yyyy-mm-dd HH24:mi:ss'), --数据更新时间
                                      '{9}', --版本号
                                      '{10}', --病种内涵
                                      '{11}', --版本名称
                                      '{12}', --诊疗指南页码
                                      '{13}', --诊疗指南电子档案
                                      '{14}', --门慢门特病种名称
                                      '{15}' --门慢门特病种大类代码



                                    )";
            #endregion

            #region 赋值
            strSql = string.Format(strSql, yb1309.mmmtbzml_code, //门慢门特病种目录代码
                                            yb1309.mmmtbzdl_name, //门慢门特病种大类名称
                                            yb1309.mmmtbzxfl_code, //门慢门特病种细分类名称
                                            yb1309.ybqf, //医保区划
                                            yb1309.mark, //备注
                                            yb1309.valid_state, //有效标志
                                            yb1309.wyjlh, //唯一记录号
                                            yb1309.oper_date, //数据创建时间
                                            yb1309.update_date, //数据更新时间
                                            yb1309.ver, //版本号
                                            yb1309.bznh, //病种内涵
                                            yb1309.ver_name, //版本名称
                                            yb1309.zlznym, //诊疗指南页码
                                            yb1309.zlzndzda, //诊疗指南电子档案
                                            yb1309.mmmtbz_name, //门慢门特病种名称
                                            yb1309.mmmtbzdl_code//门慢门特病种大类代码

                                           );

            #endregion

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }

            return 1;
        }

        public int QueryYB1309()
        {
            ArrayList al = new ArrayList();
            string sqlStr = "select count(1) from fin_yb_1309";
            try
            {
                string rs = this.ExecSqlReturnOne(sqlStr, "0");

                return int.Parse(rs);
            }
            catch (Exception e)
            {
                return 0;
            }
        }


        public int DeleteYB1309()
        {
            Class.Log log = new Class.Log();
            string strSql = "";
            #region sql
            strSql = @"delete  from  fin_yb_1309";
            #endregion
            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    log.WriteLog("sql " + strSql);
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                log.WriteLog("sql " + strSql);
                return -1;
            }
            return 1;
        }
        #endregion

        #region 1310 sql

        public int InsertYB1310(FS.HISFC.Models.SIInterface.YB_1310 yb1310)
        {
            #region sql
            string strSql = @"INSERT INTO fin_yb_1310 --按病种付费病种目录
                                  ( bzjsml_id, --病种结算目录id
                                    bzjsml_code, --按病种结算病种目录代码
                                    bzjsml_name, --按病种结算病种名称
                                    ydsscz_code, --限定手术操作代码
                                    ydsscz_name, --限定手术操作名称
                                    valid_state, --有效标志
                                    wyjlh, --唯一记录号
                                    oper_date, --数据创建时间
                                    update_date, --数据更新时间
                                    ver, --版本号
                                    bznh, --病种内涵
                                    mark, --备注
                                    ver_name, --版本名称
                                    zlznym, --诊疗指南页码
                                    zlzndzda--诊疗指南电子档案




                                   )
                                    VALUES
                                  (
                                          '{0}', --病种结算目录id
                                          '{1}', --按病种结算病种目录代码
                                          '{2}', --按病种结算病种名称
                                          '{3}', --限定手术操作代码
                                          '{4}', --限定手术操作名称
                                          '{5}', --有效标志
                                          '{6}', --唯一记录号
                                          to_date('{7}','yyyy-mm-dd HH24:mi:ss'), --数据创建时间
                                          to_date('{8}','yyyy-mm-dd HH24:mi:ss'), --数据更新时间
                                          '{9}', --版本号
                                          '{10}', --病种内涵
                                          '{11}', --备注
                                          '{12}', --版本名称
                                          '{13}', --诊疗指南页码
                                          '{14}' --诊疗指南电子档案

                                    )";
            #endregion

            #region 赋值
            strSql = string.Format(strSql, yb1310.bzjsml_id, //病种结算目录id
                                            yb1310.bzjsml_code, //按病种结算病种目录代码
                                            yb1310.bzjsml_name, //按病种结算病种名称
                                            yb1310.ydsscz_code, //限定手术操作代码
                                            yb1310.ydsscz_name, //限定手术操作名称
                                            yb1310.valid_state, //有效标志
                                            yb1310.wyjlh, //唯一记录号
                                            yb1310.oper_date, //数据创建时间
                                            yb1310.update_date, //数据更新时间
                                            yb1310.ver, //版本号
                                            yb1310.bznh, //病种内涵
                                            yb1310.mark, //备注
                                            yb1310.ver_name, //版本名称
                                            yb1310.zlznym, //诊疗指南页码
                                            yb1310.zlzndzda//诊疗指南电子档案


                                           );

            #endregion

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }

            return 1;
        }

        public int QueryYB1310()
        {
            ArrayList al = new ArrayList();
            string sqlStr = "select count(1) from fin_yb_1310";
            try
            {
                string rs = this.ExecSqlReturnOne(sqlStr, "0");

                return int.Parse(rs);
            }
            catch (Exception e)
            {
                return 0;
            }
        }


        public int DeleteYB1310()
        {
            Class.Log log = new Class.Log();
            string strSql = "";
            #region sql
            strSql = @"delete  from  fin_yb_1310";
            #endregion
            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    log.WriteLog("sql " + strSql);
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                log.WriteLog("sql " + strSql);
                return -1;
            }
            return 1;
        }
        #endregion

        #region 1311 sql

        public int InsertYB1311(FS.HISFC.Models.SIInterface.YB_1311 yb1311)
        {
            #region sql
            string strSql = @"INSERT INTO fin_yb_1311 --日间手术治疗病种目录
                                  ( rjsszlml_id, --日间手术治疗目录id
                                    rjssbzml_code, --日间手术病种目录代码
                                    rjssbzml_name, --日间手术病种名称
                                    valid_state, --有效标志
                                    wyjlh, --唯一记录号
                                    oper_date, --数据创建时间
                                    update_date, --数据更新时间
                                    ver, --版本号
                                    ver_name, --版本名称
                                    bznh, --病种内涵
                                    mark, --备注
                                    zlznym, --诊疗指南页码
                                    zlzndzda, --诊疗指南电子档案
                                    sscz_name, --手术操作名称
                                    sscz_code--手术操作代码

                                   )
                                    VALUES
                                  (
                                      '{0}', --日间手术治疗目录id
                                      '{1}', --日间手术病种目录代码
                                      '{2}', --日间手术病种名称
                                      '{3}', --有效标志
                                      '{4}', --唯一记录号
                                      to_date('{5}','yyyy-mm-dd HH24:mi:ss'), --数据创建时间
                                      to_date('{6}','yyyy-mm-dd HH24:mi:ss'), --数据更新时间
                                      '{7}', --版本号
                                      '{8}', --版本名称
                                      '{9}', --病种内涵
                                      '{10}', --备注
                                      '{11}', --诊疗指南页码
                                      '{12}', --诊疗指南电子档案
                                      '{13}', --手术操作名称
                                      '{14}' --手术操作代码

                                    )";
            #endregion

            #region 赋值
            strSql = string.Format(strSql, yb1311.rjsszlml_id, //日间手术治疗目录id
                                            yb1311.rjssbzml_code, //日间手术病种目录代码
                                            yb1311.rjssbzml_name, //日间手术病种名称
                                            yb1311.valid_state, //有效标志
                                            yb1311.wyjlh, //唯一记录号
                                            yb1311.oper_date, //数据创建时间
                                            yb1311.update_date, //数据更新时间
                                            yb1311.ver, //版本号
                                            yb1311.ver_name, //版本名称
                                            yb1311.bznh, //病种内涵
                                            yb1311.mark, //备注
                                            yb1311.zlznym, //诊疗指南页码
                                            yb1311.zlzndzda, //诊疗指南电子档案
                                            yb1311.sscz_name, //手术操作名称
                                            yb1311.sscz_code//手术操作代码



                                           );

            #endregion

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }

            return 1;
        }

        public int QueryYB1311()
        {
            ArrayList al = new ArrayList();
            string sqlStr = "select count(1) from fin_yb_1311";
            try
            {
                string rs = this.ExecSqlReturnOne(sqlStr, "0");

                return int.Parse(rs);
            }
            catch (Exception e)
            {
                return 0;
            }
        }


        public int DeleteYB1311()
        {
            Class.Log log = new Class.Log();
            string strSql = "";
            #region sql
            strSql = @"delete  from  fin_yb_1311";
            #endregion
            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    log.WriteLog("sql " + strSql);
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                log.WriteLog("sql " + strSql);
                return -1;
            }
            return 1;
        }
        #endregion

        #region 1313 sql

        public int InsertYB1313(FS.HISFC.Models.SIInterface.YB_1313 yb1313)
        {
            #region sql
            string strSql = @"INSERT INTO fin_yb_1313 --肿瘤形态学目录
                                  ( zlxtx_id, --肿瘤形态学id
                                    zllx_code, --肿瘤/细胞类型代码
                                    zllx_name, --肿瘤/细胞类型
                                    xtxfl_code, --形态学分类代码
                                    xtxfl_name, --形态学分类
                                    valid_state, --有效标志
                                    wyjlh, --唯一记录号
                                    oper_date, --数据创建时间
                                    update_date, --数据更新时间
                                    ver, --版本号
                                    ver_name--版本名称

                                   )
                                    VALUES
                                  (
                                      '{0}', --肿瘤形态学id
                                      '{1}', --肿瘤/细胞类型代码
                                      '{2}', --肿瘤/细胞类型
                                      '{3}', --形态学分类代码
                                      '{4}', --形态学分类
                                      '{5}', --有效标志
                                      '{6}', --唯一记录号
                                      to_date('{7}','yyyy-mm-dd HH24:mi:ss'), --数据创建时间
                                      to_date('{8}','yyyy-mm-dd HH24:mi:ss'), --数据更新时间
                                      '{9}', --版本号
                                      '{10}' --版本名称

                                    )";
            #endregion

            #region 赋值
            strSql = string.Format(strSql, yb1313.zlxtx_id, //肿瘤形态学id
                                            yb1313.zllx_code, //肿瘤/细胞类型代码
                                            yb1313.zllx_name, //肿瘤/细胞类型
                                            yb1313.xtxfl_code, //形态学分类代码
                                            yb1313.xtxfl_name, //形态学分类
                                            yb1313.valid_state, //有效标志
                                            yb1313.wyjlh, //唯一记录号
                                            yb1313.oper_date, //数据创建时间
                                            yb1313.update_date, //数据更新时间
                                            yb1313.ver, //版本号
                                            yb1313.ver_name//版本名称



                                           );

            #endregion

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }

            return 1;
        }

        public int QueryYB1313()
        {
            ArrayList al = new ArrayList();
            string sqlStr = "select count(1) from fin_yb_1313";
            try
            {
                string rs = this.ExecSqlReturnOne(sqlStr, "0");

                return int.Parse(rs);
            }
            catch (Exception e)
            {
                return 0;
            }
        }


        public int DeleteYB1313()
        {
            Class.Log log = new Class.Log();
            string strSql = "";
            #region sql
            strSql = @"delete  from  fin_yb_1313";
            #endregion
            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    log.WriteLog("sql " + strSql);
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                log.WriteLog("sql " + strSql);
                return -1;
            }
            return 1;
        }
        #endregion

        #region 1314 sql

        public int InsertYB1314(FS.HISFC.Models.SIInterface.YB_1314 yb1314)
        {
            #region sql
            string strSql = @"INSERT INTO fin_yb_1314 --中医疾病目录
                                  ( zyjbzd_id, --中医疾病诊断id
                                    kblm_code, --科别类目代码
                                    kblm_name, --科别类目名称
                                    zkxtfl_code, --专科系统分类目代码
                                    zkxtfl_name, --专科系统分类目名称
                                    jbfl_code, --疾病分类代码
                                    jbfl_name, --疾病分类名称
                                    mark, --备注
                                    valid_state, --有效标志
                                    wyjlh, --唯一记录号
                                    oper_date, --数据创建时间
                                    update_date, --数据更新时间
                                    ver, --版本号
                                    ver_name--版本名称
                                   )
                                    VALUES
                                  (
                                      '{0}', --中医疾病诊断id
                                      '{1}', --科别类目代码
                                      '{2}', --科别类目名称
                                      '{3}', --专科系统分类目代码
                                      '{4}', --专科系统分类目名称
                                      '{5}', --疾病分类代码
                                      '{6}', --疾病分类名称
                                      '{7}', --备注
                                      '{8}', --有效标志
                                      '{9}', --唯一记录号
                                      to_date('{10}','yyyy-mm-dd HH24:mi:ss'), --数据创建时间
                                      to_date('{11}','yyyy-mm-dd HH24:mi:ss'), --数据更新时间
                                      '{12}', --版本号
                                      '{13}' --版本名称


                                    )";
            #endregion

            #region 赋值
            strSql = string.Format(strSql, yb1314.zyjbzd_id, //中医疾病诊断id
                                            yb1314.kblm_code, //科别类目代码
                                            yb1314.kblm_name, //科别类目名称
                                            yb1314.zkxtfl_code, //专科系统分类目代码
                                            yb1314.zkxtfl_name, //专科系统分类目名称
                                            yb1314.jbfl_code, //疾病分类代码
                                            yb1314.jbfl_name, //疾病分类名称
                                            yb1314.mark, //备注
                                            yb1314.valid_state, //有效标志
                                            yb1314.wyjlh, //唯一记录号
                                            yb1314.oper_date, //数据创建时间
                                            yb1314.update_date, //数据更新时间
                                            yb1314.ver, //版本号
                                            yb1314.ver_name//版本名称



                                           );

            #endregion

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }

            return 1;
        }

        public int QueryYB1314()
        {
            ArrayList al = new ArrayList();
            string sqlStr = "select count(1) from fin_yb_1314";
            try
            {
                string rs = this.ExecSqlReturnOne(sqlStr, "0");

                return int.Parse(rs);
            }
            catch (Exception e)
            {
                return 0;
            }
        }


        public int DeleteYB1314()
        {
            Class.Log log = new Class.Log();
            string strSql = "";
            #region sql
            strSql = @"delete  from  fin_yb_1314";
            #endregion
            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    log.WriteLog("sql " + strSql);
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                log.WriteLog("sql " + strSql);
                return -1;
            }
            return 1;
        }
        #endregion

        #region 1315 sql

        public int InsertYB1315(FS.HISFC.Models.SIInterface.YB_1315 yb1315)
        {
            #region sql
            string strSql = @"INSERT INTO fin_yb_1315 --中医证候目录
                                  ( zyzh_id, --中医证候id
                                    zhlm_code, --证候类目代码
                                    zhlm_name, --证候类目名称
                                    zhsx_code, --证候属性代码
                                    zhsx_name, --证候属性
                                    zhfl_code, --证候分类代码
                                    zhfl_name, --证候分类名称
                                    mark, --备注
                                    valid_state, --有效标志
                                    wyjlh, --唯一记录号
                                    oper_date, --数据创建时间
                                    update_date, --数据更新时间
                                    ver, --版本号
                                    ver_name--版本名称
                                   )
                                    VALUES
                                  (
                                    '{0}', --中医证候id
                                    '{1}', --证候类目代码
                                    '{2}', --证候类目名称
                                    '{3}', --证候属性代码
                                    '{4}', --证候属性
                                    '{5}', --证候分类代码
                                    '{6}', --证候分类名称
                                    '{7}', --备注
                                    '{8}', --有效标志
                                    '{9}', --唯一记录号
                                     to_date('{10}','yyyy-mm-dd HH24:mi:ss'), --数据创建时间
                                     to_date('{11}','yyyy-mm-dd HH24:mi:ss'), --数据更新时间
                                    '{12}', --版本号
                                    '{13}' --版本名称


                                    )";
            #endregion

            #region 赋值
            strSql = string.Format(strSql, yb1315.zyzh_id, //中医证候id
                                            yb1315.zhlm_code, //证候类目代码
                                            yb1315.zhlm_name, //证候类目名称
                                            yb1315.zhsx_code, //证候属性代码
                                            yb1315.zhsx_name, //证候属性
                                            yb1315.zhfl_code, //证候分类代码
                                            yb1315.zhfl_name, //证候分类名称
                                            yb1315.mark, //备注
                                            yb1315.valid_state, //有效标志
                                            yb1315.wyjlh, //唯一记录号
                                            yb1315.oper_date, //数据创建时间
                                            yb1315.update_date, //数据更新时间
                                            yb1315.ver, //版本号
                                            yb1315.ver_name//版本名称



                                           );

            #endregion

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }

            return 1;
        }

        public int QueryYB1315()
        {
            ArrayList al = new ArrayList();
            string sqlStr = "select count(1) from fin_yb_1315";
            try
            {
                string rs = this.ExecSqlReturnOne(sqlStr, "0");

                return int.Parse(rs);
            }
            catch (Exception e)
            {
                return 0;
            }
        }


        public int DeleteYB1315()
        {
            Class.Log log = new Class.Log();
            string strSql = "";
            #region sql
            strSql = @"delete  from  fin_yb_1315";
            #endregion
            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    log.WriteLog("sql " + strSql);
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                log.WriteLog("sql " + strSql);
                return -1;
            }
            return 1;
        }
        #endregion

        #region  结算清单信息setlinfo
        public Models.Request.RequestGzsiModel4101.Setlinfo GetSetlinfo4101(string inpatientNo)
        {
            Models.Request.RequestGzsiModel4101.Setlinfo setlinfo4101 = new Models.Request.RequestGzsiModel4101.Setlinfo();

            #region sql
            string strSql = @"SELECT   s.mdtrt_id, --就诊ID
                                       s.setl_id, --结算ID
                                       s.patient_no, --住院号
                                       s.name, --姓名
                                       s.sex_code, --性别
                                       to_char(s.birthday, 'yyyy-MM-dd'), --生日
                                       s.age, --年龄
                                       i.coun_code, --国籍
                                       (select t.name
                                          from com_dictionary t
                                         where t.type = 'COUNTRY'
                                           and t.code = i.coun_code), --国籍名称
                                       s.gend, --民族
                                       s.psn_cert_type, --证件类别
                                       s.idenno, --nvl(s.psn_no,s.idenno),--证件号
                                       i.prof_code, --职业
                                       i.linkman_name, --联系人
                                       nvl(i.rela_code, '-'), --联系人关系
                                       nvl(i.linkman_add, '-'), --联系人地址
                                       nvl(i.linkman_tel, '-'), --联系人电话
                                       s.insutype, --医保类型
                                       nvl(s.insuplc_admdvs, '-'), --参保地
                                       '1', -- s.med_type,--住院医疗类型
                                       to_char(s.in_date， 'yyyy-MM-dd' ）, --入院时间
                                       s.dept_code, --入院科别
                                       to_char(s.out_date, 'yyyy-MM-dd'), --出院时间
                                       s.dept_code, --出院科别
                                       to_char(s.setl_time, 'yyyy-MM-dd'), --结算开始日期
                                       s.preselfpay_amt, --个人自付
                                       s.ownpay_amt, --个人自费
                                       s.acct_pay, --个人账户支出
                                       s.cash_payamt, --个人现金支付
                                       s.psn_no, --医保个人编号  29
                                       i.home, --现住址（家庭住址）30
                                       i.work_name, --单位名称 31
                                       i.work_tel, --单位电话 32
                                       i.work_zip --邮编33
                                       FROM fin_ipr_siinmaininfo s, fin_ipr_inmaininfo i --医保信息住院主表
                                       WHERE s.inpatient_no = i.inpatient_no 
                                       and s.inpatient_no = '{0}' 
                                       and s.valid_flag = '1' 
                                       and s.TYPE_CODE = '2' ";
            #endregion

            #region sql
            //            string strSql = @"SELECT s.mdtrt_id, --就诊ID
            //                                     s.setl_id, --结算ID
            //                                     s.patient_no, --住院号
            //                                     s.name, --姓名
            //                                     s.sex_code, --性别
            //                                     to_char(s.birthday,'yyyy-MM-dd') , --生日
            //                                     s.age,--年龄
            //                                     i.coun_code,--国籍
            //                                     (select t.name from com_dictionary t where t.type='COUNTRY' and t.code=i.coun_code),--国籍名称
            //                                     s.gend,--民族
            //                                     s.psn_cert_type,--证件类别
            //                                     s.idenno, --nvl(s.psn_no,s.idenno),--证件号
            //                                     nvl((select OCCUPATION_CODE from met_mrs_base where inpatient_no=s.inpatient_no),'70'),--职业
            //                                     i.linkman_name,--联系人
            //                                     nvl(i.rela_code,'-'),--联系人关系
            //                                     nvl(i.linkman_add,'-'),--联系人地址
            //                                     nvl(i.linkman_tel,'-'),--联系人电话
            //                                     s.insutype,--医保类型
            //                                     nvl(s.insuplc_admdvs,'-'),--参保地
            //                                     '1',-- s.med_type,--住院医疗类型
            //                                     to_char(s.in_date，'yyyy-MM-dd'）,--入院时间
            //                                     s.dept_code,--入院科别
            //                                     to_char(s.out_date,'yyyy-MM-dd'),--出院时间
            //                                     s.dept_code,--出院科别
            //                                     to_char(s.setl_time,'yyyy-MM-dd'),--结算开始日期
            //                                     s.preselfpay_amt,--个人自付
            //                                     s.ownpay_amt,--个人自费
            //                                    s.acct_pay,--个人账户支出
            //                                    s.cash_payamt, --个人现金支付
            //                                    s.psn_no ,  --医保个人编号  29
            //                                    i.home,--现住址（家庭住址）30
            //                                    i.WORK_NAME,--单位名称 31
            //                                    i.work_tel,--单位电话 32
            //                                    i.WORK_ZIP --邮编33
            //                                FROM fin_ipr_siinmaininfo s,fin_ipr_inmaininfo i --医保信息住院主表
            //                               WHERE s.inpatient_no=i.inpatient_no
            //                                 and s.inpatient_no = '{0}'
            //                                 and s.valid_flag = '1'
            //                                 and s.TYPE_CODE='2'
            //                            ";
            #endregion
            strSql = string.Format(strSql, inpatientNo);

            try
            {
                if (ExecQuery(strSql) == -1)
                {
                    return null;
                }
                while (Reader.Read())
                {
                    #region 赋值
                    setlinfo4101.mdtrt_id = Reader[0].ToString();
                    setlinfo4101.setl_id = Reader[1].ToString();
                    setlinfo4101.fixmedins_code = Models.UserInfo.Instance.fixmedins_code;
                    setlinfo4101.fixmedins_name = Models.UserInfo.Instance.fixmedins_code;//ningjx
                    setlinfo4101.hi_setl_lv = "";
                    setlinfo4101.hi_no = Reader[29].ToString(); ;
                    setlinfo4101.medcasno = Reader[2].ToString();
                    setlinfo4101.dcla_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    setlinfo4101.psn_name = Reader[3].ToString();
                    setlinfo4101.gend = Reader[4].ToString();
                    setlinfo4101.brdy = Convert.ToDateTime(Reader[5].ToString()).ToString(Common.Constants.FORMAT_DATETIME1);
                    setlinfo4101.age = Reader[6].ToString();
                    setlinfo4101.ntly = Reader[7].ToString();
                    setlinfo4101.nwb_age = "";
                    setlinfo4101.naty = Reader[9].ToString();
                    setlinfo4101.patn_cert_type = Reader[10].ToString();
                    setlinfo4101.certno = Reader[11].ToString();
                    setlinfo4101.prfs = Reader[12].ToString();
                    setlinfo4101.curr_addr = Reader[30].ToString();
                    setlinfo4101.emp_name = Reader[31].ToString();
                    setlinfo4101.emp_addr = "";
                    setlinfo4101.emp_tel = Reader[32].ToString();
                    setlinfo4101.poscode = Reader[33].ToString();
                    setlinfo4101.coner_name = Reader[13].ToString();
                    setlinfo4101.patn_rlts = Reader[14].ToString();
                    setlinfo4101.coner_addr = Reader[15].ToString();
                    setlinfo4101.coner_tel = Reader[16].ToString();
                    setlinfo4101.hi_type = Reader[17].ToString();
                    setlinfo4101.insuplc = Reader[18].ToString();
                    setlinfo4101.sp_psn_type = "";
                    setlinfo4101.nwb_adm_type = "";
                    setlinfo4101.nwb_bir_wt = "";
                    setlinfo4101.nwb_adm_wt = "";
                    setlinfo4101.opsp_diag_caty = "";
                    setlinfo4101.opsp_mdtrt_date = "";
                    setlinfo4101.ipt_med_type = Reader[19].ToString();
                    setlinfo4101.adm_way = "";
                    setlinfo4101.trt_type = "";
                    setlinfo4101.adm_time = Convert.ToDateTime(Reader[20].ToString()).ToString(Common.Constants.FORMAT_DATETIME1);
                    setlinfo4101.adm_caty = Reader[21].ToString();
                    setlinfo4101.refldept_dept = "";
                    setlinfo4101.dscg_time = Convert.ToDateTime(Reader[22].ToString()).ToString(Common.Constants.FORMAT_DATETIME1);
                    setlinfo4101.dscg_caty = Reader[23].ToString();
                    setlinfo4101.act_ipt_days = "";
                    setlinfo4101.otp_wm_dise = "";
                    setlinfo4101.wm_dise_code = "";
                    setlinfo4101.otp_tcm_dise = "";
                    setlinfo4101.tcm_dise_code = "";
                    setlinfo4101.diag_code_cnt = "";
                    setlinfo4101.oprn_oprt_code_cnt = "";
                    setlinfo4101.vent_used_dura = "";
                    setlinfo4101.pwcry_bfadm_coma_dura = "";
                    setlinfo4101.pwcry_afadm_coma_dura = "";
                    setlinfo4101.bld_cat = "";
                    setlinfo4101.bld_amt = "";
                    setlinfo4101.bld_unt = "";
                    setlinfo4101.spga_nurscare_days = "";
                    setlinfo4101.lv1_nurscare_days = "";
                    setlinfo4101.scd_nurscare_days = "";
                    setlinfo4101.lv3_nurscare_days = "";
                    setlinfo4101.dscg_way = "";
                    setlinfo4101.acp_medins_name = "";
                    setlinfo4101.acp_optins_code = "";
                    setlinfo4101.bill_code = "-";
                    setlinfo4101.bill_no = "-";
                    setlinfo4101.biz_sn = "-";
                    setlinfo4101.days_rinp_flag_31 = "";
                    setlinfo4101.days_rinp_pup_31 = "";
                    setlinfo4101.chfpdr_name = "";
                    setlinfo4101.chfpdr_code = "";
                    setlinfo4101.setl_begn_date = Convert.ToDateTime(Reader[24].ToString()).ToString("yyyy-MM-dd");
                    setlinfo4101.setl_end_date = Convert.ToDateTime(Reader[24].ToString()).ToString("yyyy-MM-dd");
                    setlinfo4101.psn_selfpay = Reader[25].ToString();
                    setlinfo4101.psn_ownpay = Reader[26].ToString();
                    setlinfo4101.acct_pay = Reader[27].ToString();
                    setlinfo4101.psn_cashpay = Reader[28].ToString();
                    setlinfo4101.hi_paymtd = "1";
                    setlinfo4101.hsorg = Models.UserInfo.Instance.fixmedins_code;//ningjx
                    setlinfo4101.hsorg_opter = "-";
                    setlinfo4101.medins_fill_dept = "-";
                    setlinfo4101.medins_fill_psn = "-";

                    #endregion
                }
                Reader.Close();
            }
            catch (Exception e)
            {
                return null;
            }
            return setlinfo4101;
        }

        #endregion

        #region 获取医生对照医保编码
        //获取医生对照医保编码
        public string GetDocYBNO(string DocCode)
        {
            string strSql = @"select  c.interface_code  from   com_employee  c
                              where   c.empl_code ='{0}'
                              and     rownum = 1";
            strSql = string.Format(strSql, DocCode);

            try
            {
                return this.ExecSqlReturnOne(strSql);
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        #endregion

        #endregion

        #region 贯标项目对照
        public DataTable GetItemInfo(string HisCode)
        {
            string strSql = @"select 
                                i.his_code, 
                                i.gdCode,
                                i.his_name,
                                fun_get_dictionary_name('MINFEE', i.fee_code)  fee_code,
                                i.specs ,
                                fun_get_company_name(i.producer_code),
                                i.approve_info,
                                i.gjbm,
                                e.center_code,
                                e.center_memo,
                                e.center_name
                                from
                                (select  
                                                (case when (select t.control_value from com_controlargument t where t.control_code = 'FSMZ16'
                                                )='0' then b.gb_code else b.custom_code end) gdCode,
                                                b.drug_code his_code,
                                                b.trade_name his_name,
                                                b.class_code class_code,
                                                b.fee_code  fee_code,
                                                b.item_grade item_grade, 
                                                b.specs specs,
                                                b.producer_code  producer_code,
                                                b.approve_info approve_info,
                                                b.gb_code gjbm
                                            from pha_com_baseinfo b
                                            union all
                                            select 
                                                (case when (select t.control_value from com_controlargument t where t.control_code = 'FSMZ16'
                                                )='0' then u.gb_code else u.input_code end) gdCode,
                                                u.item_code his_code,
                                                u.item_name his_name,
                                                u.sys_class class_code,
                                                u.fee_code fee_code,
                                                u.item_grade item_grade, 
                                                u.specs specs,
                                                '' producer_code,
                                                '' approve_info,
                                                u.gb_code gjbm
                                            from  fin_com_undruginfo u) i
                                left join fin_com_compare e
                                on e.his_user_code = i.gdCode and e.pact_code = '4'
                                where i.his_code ='{0}'";
            strSql = string.Format(strSql, HisCode);

            try
            {
                DataSet ds = new DataSet();
                this.ExecQuery(strSql, ref ds);
                return ds.Tables[0];
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 查询未对照的项目
        /// </summary>
        /// <returns></returns>
        public ArrayList GetGDItemInfo()
        {
            string strSql = @"select * from (
                                                    select b.drug_code gdCode,b.trade_name
                                                    from pha_com_baseinfo b
                                                    where b.valid_state = '1'
                                                    union all
                                                    select u.item_code gdCode,u.item_name
                                                    from fin_com_undruginfo u
                                                    where u.valid_state = '1') aa
                                                    /*where (select count(*) from  fin_com_compare e
                                                    where  e.pact_code = '4'
                                                    and e.his_code = aa.gdCode) <= 0*/";
            ArrayList al = new ArrayList();
            try
            {

                if (ExecQuery(strSql) == -1)
                {
                    return null;
                }
                while (Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = Reader[0].ToString();// 
                    obj.Name = Reader[1].ToString();// 
                    //obj.Memo = Reader[2].ToString();
                    al.Add(obj);
                }

                return al;
            } //抛出错误
            catch (Exception ex)
            {
                Err = "取诊断信息出错！" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
        }

        /// <summary>
        /// 查询未对照的项目
        /// </summary>
        /// <returns></returns>
        public ArrayList GetItemLevel()
        {
            string strSql = @"select code,name from com_dictionary t where  t.type='SIGRADE'";
            ArrayList al = new ArrayList();
            try
            {

                if (ExecQuery(strSql) == -1)
                {
                    return null;
                }
                while (Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = Reader[0].ToString();// 
                    obj.Name = Reader[1].ToString();// 

                    al.Add(obj);
                }

                return al;
            } //抛出错误
            catch (Exception ex)
            {
                Err = "取等级信息出错！" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
        }
        #endregion

        #region 自费患者结算清单
        #region 查询
        public List<FS.HISFC.Models.RADT.PatientInfo> GetOwnPayPatientByUpload(string beginTime, string endTime,
    string isUpload, string personType, string cardNo, string invoice_state)
        {
            if (string.IsNullOrEmpty(cardNo))
            {
                cardNo = "ALL";
            }
            if (string.IsNullOrEmpty(personType))
            {
                personType = "ALL";
            }

            List<FS.HISFC.Models.RADT.PatientInfo> tempList = new List<FS.HISFC.Models.RADT.PatientInfo>();

            #region sql
            //            string strSql = @"select '2' 患者类型,
            //       i.inpatient_no 就诊流水号,
            //       d.invoice_no 发票流水号,
            //       i.patient_no 住院号,
            //       i.card_no 病历号,
            //       i.name 姓名,
            //       i.sex_code 性别,
            //       i.idenno 证件号,
            //       trunc(i.birthday) 出生日期,
            //       i.in_date 入院时间,
            //       i.out_date 出院时间,
            //       d.balance_date 结算时间,
            //       fun_get_dept_name(i.dept_code) 就诊科室,
            //       nvl(d.pact_code, i.pact_code) 合同单位编码,
            //       (select p.pact_name
            //          from fin_com_pactunitinfo p
            //         where p.pact_code = nvl(d.pact_code, i.pact_code)) 合同单位名称,
            //       nvl(i.UPLOAD_FLAGOwnPay,'0') 上传标志,
            //       i.UPLOAD_DATE 上传时间,
            //       i.UPLOAD_ERR 报错信息
            //  from fin_ipr_inmaininfo i
            //  left join fin_ipb_balancehead d
            //   on d.inpatient_no = i.inpatient_no
            //   and d.trans_type = '1' 
            //   and d.waste_flag = '1'
            //   and d.invoice_no not like '9%'
            // where 1=1
            //   and i.in_state = 'O'
            //   and i.paykind_code = '01' 
            //   and d.balance_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
            //   and d.balance_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
            //   and nvl(i.UPLOAD_FLAGOwnPay,'0') = '{2}'
            //   and ('2' = '{3}' or 'ALL' = '{3}')
            //   and (i.patient_no like '%{4}' or 'ALL' = '{4}')
            //   
            //   
            //union all
            //
            //select '1' 患者类型,
            //       r.clinic_code 就诊流水号,
            //       b.invoice_no 发票流水号,
            //       '' 住院号,
            //       r.card_no 病历号,
            //       r.name 姓名,
            //       r.sex_code 性别,
            //       r.idenno 证件号,
            //       trunc(r.birthday) 出生日期,
            //       r.reg_date 入院时间,
            //       r.reg_date 出院时间,
            //       b.oper_date 结算时间,
            //       fun_get_dept_name(r.see_dpcd) 就诊科室,
            //       nvl(b.pact_code, r.pact_code) 合同单位编码,
            //       (select p.pact_name
            //          from fin_com_pactunitinfo p
            //         where p.pact_code = nvl(b.pact_code, r.pact_code)) 合同单位名称,
            //       nvl(b.UPLOAD_FLAGOwnPay,'0') 上传标志,
            //       b.UPLOAD_DATE 上传时间,
            //       b.UPLOAD_ERR 报错信息
            //  from fin_opr_register r, fin_opb_invoiceinfo b
            // where 1=1
            //   and r.valid_flag = '1'
            //   and r.paykind_code = '01' 
            //   and r.clinic_code = b.clinic_code
            //   and b.cancel_flag = '1' 
            //   and b.oper_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
            //   and b.oper_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
            //   and nvl(b.UPLOAD_FLAGOwnPay,'0') = '{2}'
            //   and ('1' = '{3}' or 'ALL' = '{3}')
            //   and (r.card_no like '%{4}' or 'ALL' = '{4}')
            //   ";
            #endregion

            #region sql
            string strSql = @"select '2' 患者类型,
       i.inpatient_no 就诊流水号,
       d.invoice_no 发票流水号,
       i.patient_no 住院号,
       i.card_no 病历号,
       i.name 姓名,
       i.sex_code 性别,
       i.idenno 证件号,
       trunc(i.birthday) 出生日期,
       i.in_date 入院时间,
       i.out_date 出院时间,
       d.balance_date 结算时间,
       i.dept_code 就诊科室编码,
       fun_get_dept_name(i.dept_code) 就诊科室,
       nvl(d.pact_code, i.pact_code) 合同单位编码,
       (select p.pact_name
          from fin_com_pactunitinfo p
         where p.pact_code = nvl(d.pact_code, i.pact_code)) 合同单位名称,
       nvl(l.state,'0') 上传标志,
       l.oper_date 上传时间,
       l.errinfo 报错信息,
       d.waste_flag 发票状态
  from fin_ipr_inmaininfo i, fin_ipb_balancehead d
  left join FIN_CAS_WONPAY_LOG l on d.inpatient_no = l.inpatient_no and d.invoice_no = l.invoice_no

 where 1=1
   and i.in_state = 'O'
   and d.inpatient_no = i.inpatient_no
   and i.patient_type in('P','F') 
   --and i.dept_code in (select d.code from com_dictionary d where  d.type = 'GZSI_OwnLoadDept' and d.valid_state = '1') 
   --and i.nurse_cell_code in (select d.code from com_dictionary d where  d.type = 'GZSI_OwnLoadDept' and d.valid_state = '1')  --分娩、新生儿
   and i.dept_code in('1001','1002') and (i.NURSE_CELL_NAME like '%妇产科%' or i.NURSE_CELL_NAME like '%新生儿科%' or i.NURSE_CELL_NAME  like '%产房%') --分娩、新生儿
   and ((d.trans_type = '1'  and  d.waste_flag = '1') or (d.trans_type = '2' and d.waste_flag = '0'))
   and (d.waste_flag = '{5}' or  'ALL' =  '{5}')
   --and d.invoice_no not like '9%'
   and i.paykind_code = '01' 
   and d.balance_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
   and d.balance_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
   and (nvl(l.state,'0') = '{2}' or ('{2}' = '0'  and l.state != '1'))
   and ('2' = '{3}' or 'ALL' = '{3}')
   and (i.patient_no like '%{4}' or 'ALL' = '{4}')
   
   
union all

select '1' 患者类型,
       r.clinic_code 就诊流水号,
       b.invoice_no 发票流水号,
       '' 住院号,
       r.card_no 病历号,
       r.name 姓名,
       r.sex_code 性别,
       r.idenno 证件号,
       trunc(r.birthday) 出生日期,
       r.reg_date 入院时间,
       r.reg_date 出院时间,
       b.oper_date 结算时间,
       r.see_dpcd 就诊科室编码,
       fun_get_dept_name(r.see_dpcd) 就诊科室,
       nvl(b.pact_code, r.pact_code) 合同单位编码,
       (select p.pact_name
          from fin_com_pactunitinfo p
         where p.pact_code = nvl(b.pact_code, r.pact_code)) 合同单位名称,
       nvl(l.state,'0') 上传标志,
       l.oper_date 上传时间,
       l.errinfo 报错信息,
       b.cancel_flag  发票状态
  from fin_opr_register r, fin_opb_invoiceinfo b
  left join FIN_CAS_WONPAY_LOG l on b.clinic_code = l.inpatient_no and b.invoice_no = l.invoice_no
 where 1=1
   and r.valid_flag = '1' 
   and r.clinic_code = b.clinic_code
   and b.paykind_code = '01'
   and ((b.trans_type = '1'  and  b.cancel_flag = '1') or (b.trans_type = '2' and b.cancel_flag = '0'))
   and (b.cancel_flag = '{5}'  or 'ALL' = '{5}')
   and b.oper_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
   and b.oper_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
   and (nvl(l.state,'0') = '{2}' or ('{2}' = '0'  and l.state != '1'))
   and ('1' = '{3}' or 'ALL' = '{3}')
   and (r.card_no like '%{4}' or 'ALL' = '{4}')
   ";
            #endregion

            strSql = string.Format(strSql, beginTime, endTime, isUpload, personType, cardNo, invoice_state);

            try
            {
                DataSet dsSet = null;
                if (this.ExecQuery(strSql, ref dsSet) == -1)
                {
                    return null;
                }
                DataTable al = new DataTable();
                if (dsSet == null || dsSet.Tables.Count <= 0)
                {
                    return null;
                }
                al = dsSet.Tables[0];
                if (al == null || al.Rows.Count <= 0)
                {
                    return null;
                }


                if (al != null && al.Rows.Count > 0)
                {
                    foreach (DataRow dRow in al.Rows)
                    {
                        FS.HISFC.Models.RADT.PatientInfo Obj = new FS.HISFC.Models.RADT.PatientInfo();
                        #region 赋值
                        Obj.ID = dRow["就诊流水号"].ToString();
                        Obj.SIMainInfo.InvoiceNo = dRow["发票流水号"].ToString();
                        Obj.Name = dRow["姓名"].ToString();
                        Obj.PID.PatientNO = dRow["住院号"].ToString();
                        Obj.PID.CardNO = dRow["病历号"].ToString();
                        Obj.SIMainInfo.TypeCode = dRow["患者类型"].ToString();
                        Obj.Sex.ID = dRow["性别"].ToString();
                        Obj.IDCard = dRow["证件号"].ToString();
                        Obj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(dRow["出生日期"].ToString());
                        Obj.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(dRow["入院时间"].ToString());
                        Obj.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(dRow["出院时间"].ToString());
                        Obj.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(dRow["结算时间"].ToString());
                        Obj.PVisit.PatientLocation.Dept.Name = dRow["就诊科室"].ToString();
                        Obj.Pact.ID = dRow["合同单位编码"].ToString();
                        Obj.Pact.Name = dRow["合同单位名称"].ToString();
                        Obj.SIMainInfo.User02 = dRow["上传标志"].ToString();
                        Obj.SIMainInfo.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(dRow["上传时间"].ToString());
                        Obj.SIMainInfo.User01 = dRow["报错信息"].ToString();
                        Obj.SIMainInfo.BalanceState = dRow["发票状态"].ToString();
                        #endregion
                        tempList.Add(Obj);
                    }
                }

                return tempList;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return null;
            }
        }
        #endregion

        #region 住院
        #region  【4201A】明细信息(节点标识：fsiOwnpayPatnFeeListDDTO)
        public List<Models.Request.RequestGzsiModel4201A.FsiOwnpayPatnFeeListDDTO> Get4201AFsiOwnpayPatnFeeListDDTO(string InpatientNo, string InvoiceNo)
        {
            List<Models.Request.RequestGzsiModel4201A.FsiOwnpayPatnFeeListDDTO> list = new List<Models.Request.RequestGzsiModel4201A.FsiOwnpayPatnFeeListDDTO>();
            #region sql
            string strSql = @"select * from view_4201A_DDTO where INPATIENT_NO = '{0}' and invoice_no = '{1}'";
            strSql = string.Format(strSql, InpatientNo, InvoiceNo);
            #endregion


            try
            {
                DataSet dsSet = null;
                if (this.ExecQuery(strSql, ref dsSet) == -1)
                {
                    return list;
                }
                DataTable al = new DataTable();
                if (dsSet == null || dsSet.Tables.Count <= 0)
                {
                    return list;
                }
                al = dsSet.Tables[0];
                if (al == null || al.Rows.Count <= 0)
                {
                    return list;
                }


                if (al != null && al.Rows.Count > 0)
                {
                    foreach (DataRow dRow in al.Rows)
                    {
                        Models.Request.RequestGzsiModel4201A.FsiOwnpayPatnFeeListDDTO data = new Models.Request.RequestGzsiModel4201A.FsiOwnpayPatnFeeListDDTO();
                        data.memo = new Models.Request.RequestGzsiModel4201A.Memo();

                        data.fixmedins_mdtrt_id = dRow["fixmedins_mdtrt_id"].ToString(); //医药机构就诊ID
                        data.med_type = dRow["med_type"].ToString(); //医疗类别
                        data.bkkp_sn = dRow["bkkp_sn"].ToString(); //记账流水号
                        data.fee_ocur_time = dRow["fee_ocur_time"].ToString(); //费用发生时间
                        data.fixmedins_code = dRow["fixmedins_code"].ToString(); //定点医药机构编号
                        data.fixmedins_name = dRow["fixmedins_name"].ToString(); //定点医药机构名称
                        data.cnt = dRow["cnt"].ToString(); //数量
                        data.pric = dRow["pric"].ToString(); //单价
                        data.det_item_fee_sumamt = dRow["det_item_fee_sumamt"].ToString(); //明细项目费用总额
                        data.med_list_codg = dRow["med_list_codg"].ToString(); //医疗目录编码
                        data.medins_list_codg = dRow["medins_list_codg"].ToString(); //医药机构目录编码
                        data.medins_list_name = dRow["medins_list_name"].ToString(); //医药机构目录名称
                        data.med_chrgitm_type = dRow["med_chrgitm_type"].ToString(); //医疗收费项目类别
                        data.prodname = dRow["prodname"].ToString(); //商品名
                        data.bilg_dept_codg = dRow["bilg_dept_codg"].ToString(); //开单科室编码
                        data.bilg_dept_name = dRow["bilg_dept_name"].ToString(); //开单科室名称
                        data.bilg_dr_code = dRow["bilg_dr_code"].ToString(); //开单医生编码
                        data.bilg_dr_name = dRow["bilg_dr_name"].ToString(); //开单医师姓名
                        data.acord_dept_codg = dRow["acord_dept_codg"].ToString(); //受单科室编码
                        data.acord_dept_name = dRow["acord_dept_name"].ToString(); //受单科室名称
                        data.acord_dr_code = dRow["acord_dr_code"].ToString(); //受单医生编码
                        data.acord_dr_name = dRow["acord_dr_name"].ToString(); //受单医生姓名
                        data.tcmdrug_used_way = dRow["tcmdrug_used_way"].ToString(); //中药使用方式
                        data.etip_flag = dRow["etip_flag"].ToString(); //外检标志
                        data.etip_hosp_code = dRow["etip_hosp_code"].ToString(); //外检医院编码
                        data.dscg_tkdrug_flag = dRow["dscg_tkdrug_flag"].ToString(); //出院带药标志
                        data.sin_dos_dscr = dRow["sin_dos_dscr"].ToString(); //单次剂量描述
                        data.used_frqu_dscr = dRow["used_frqu_dscr"].ToString(); //使用频次描述
                        data.prd_days = dRow["prd_days"].ToString(); //周期天数
                        data.medc_way_dscr = dRow["medc_way_dscr"].ToString(); //用药途径描述
                        data.memo.hosp_appr_flag = dRow["hosp_appr_flag"].ToString(); //医院审核标志
                        data.memo.invoice_no = dRow["hosp_appr_flag"].ToString(); //发票号
                        data.memo.memo = dRow["memo"].ToString(); //备注
                        data.fulamt_ownpay_amt = dRow["fulamt_ownpay_amt"].ToString(); //全自费金额
                        data.overlmt_selfpay = dRow["overlmt_selfpay"].ToString(); //超限价自费金额
                        data.preselfpay_amt = dRow["preselfpay_amt"].ToString(); //先行自付金额
                        data.inscp_amt = dRow["inscp_amt"].ToString(); //符合政策范围金额
                        list.Add(data);
                    }
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return null;
            }
            return list;
        }
        #endregion

        #region  【4202】自费病人就诊信息(节点标识：ownPayPatnMdtrtD)
        public Models.Request.RequestGzsiModel4202.OwnPayPatnMdtrtD Get4202OwnPayPatnMdtrtD(string InpatientNo, string InvoiceNo)
        {
            Models.Request.RequestGzsiModel4202.OwnPayPatnMdtrtD data = new Models.Request.RequestGzsiModel4202.OwnPayPatnMdtrtD();
            #region sql
            string strSql = @"select * from view_4202_OwnPayPatnMdtrtD where INPATIENT_NO = '{0}' and invoice_no = '{1}'";
            strSql = string.Format(strSql, InpatientNo, InvoiceNo);
            #endregion


            try
            {
                DataSet dsSet = null;
                if (this.ExecQuery(strSql, ref dsSet) == -1)
                {
                    return data;
                }
                DataTable al = new DataTable();
                if (dsSet == null || dsSet.Tables.Count <= 0)
                {
                    return data;
                }
                al = dsSet.Tables[0];
                if (al == null || al.Rows.Count <= 0)
                {
                    return data;
                }


                if (al != null && al.Rows.Count > 0)
                {
                    foreach (DataRow dRow in al.Rows)
                    {
                        data.fixmedins_mdtrt_id = dRow["fixmedins_mdtrt_id"].ToString(); //医药机构就诊ID
                        data.fixmedins_code = dRow["fixmedins_code"].ToString(); //定点医药机构编号
                        data.fixmedins_name = dRow["fixmedins_name"].ToString(); //定点医药机构名称
                        data.psn_cert_type = dRow["psn_cert_type"].ToString(); //人员证件类型
                        data.certno = dRow["certno"].ToString(); //证件号码
                        data.psn_name = dRow["psn_name"].ToString(); //人员姓名
                        data.gend = dRow["gend"].ToString(); //性别
                        data.naty = dRow["naty"].ToString(); //民族
                        data.brdy = dRow["brdy"].ToString(); //出生日期
                        data.age = dRow["age"].ToString(); //年龄
                        data.coner_name = dRow["coner_name"].ToString(); //联系人姓名
                        data.tel = dRow["tel"].ToString(); //联系电话
                        data.addr = dRow["addr"].ToString(); //联系地址
                        data.begntime = dRow["begntime"].ToString(); //开始时间
                        data.endtime = dRow["endtime"].ToString(); //结束时间
                        data.med_type = dRow["med_type"].ToString(); //医疗类别
                        data.ipt_otp_no = dRow["ipt_otp_no"].ToString(); //住院/门诊号
                        data.medrcdno = dRow["medrcdno"].ToString(); //病历号
                        data.chfpdr_code = dRow["chfpdr_code"].ToString(); //主诊医师代码
                        data.adm_diag_dscr = dRow["adm_diag_dscr"].ToString(); //入院诊断描述
                        data.adm_dept_codg = dRow["adm_dept_codg"].ToString(); //入院科室编码
                        data.adm_dept_name = dRow["adm_dept_name"].ToString(); //入院科室名称
                        data.adm_bed = dRow["adm_bed"].ToString(); //入院床位
                        data.wardarea_bed = dRow["wardarea_bed"].ToString(); //病区床位
                        data.traf_dept_flag = dRow["traf_dept_flag"].ToString(); //转科室标志
                        data.dscg_maindiag_code = dRow["dscg_maindiag_code"].ToString(); //出院主诊断代码
                        data.dscg_dept_codg = dRow["dscg_dept_codg"].ToString(); //出院科室编码
                        data.dscg_dept_name = dRow["dscg_dept_name"].ToString(); //出院科室名称
                        data.dscg_bed = dRow["dscg_bed"].ToString(); //出院床位
                        data.dscg_way = dRow["dscg_way"].ToString(); //离院方式
                        data.main_cond_dscr = dRow["main_cond_dscr"].ToString(); //主要病情描述
                        data.dise_no = dRow["dise_no"].ToString(); //病种编号
                        data.dise_name = dRow["dise_name"].ToString(); //病种名称
                        data.oprn_oprt_code = dRow["oprn_oprt_code"].ToString(); //手术操作代码
                        data.oprn_oprt_name = dRow["oprn_oprt_name"].ToString(); //手术操作名称
                        data.otp_diag_info = dRow["otp_diag_info"].ToString(); //门诊诊断信息
                        data.inhosp_stas = dRow["inhosp_stas"].ToString(); //在院状态
                        data.die_date = dRow["die_date"].ToString(); //死亡日期
                        data.ipt_days = dRow["ipt_days"].ToString(); //住院天数
                        data.fpsc_no = dRow["fpsc_no"].ToString(); //计划生育服务证号
                        data.matn_type = dRow["matn_type"].ToString(); //生育类别
                        data.birctrl_type = dRow["birctrl_type"].ToString(); //计划生育手 术类别
                        data.latechb_flag = dRow["latechb_flag"].ToString(); //晚育标志
                        data.geso_val = dRow["geso_val"].ToString(); //孕周数
                        data.fetts = dRow["fetts"].ToString(); //胎次
                        data.fetus_cnt = dRow["fetus_cnt"].ToString(); //胎儿数
                        data.pret_flag = dRow["pret_flag"].ToString(); //早产标志
                        data.prey_time = dRow["prey_time"].ToString(); //妊娠时间
                        data.birctrl_matn_date = dRow["birctrl_matn_date"].ToString(); //计划生育手术或生育日期
                        data.cop_flag = dRow["cop_flag"].ToString(); //伴有并发症标志
                        data.vali_flag = dRow["vali_flag"].ToString(); //有效标志
                        data.memo = dRow["memo"].ToString(); //备注
                        data.opter_id = dRow["opter_id"].ToString(); //经办人ID
                        data.opter_name = dRow["opter_name"].ToString(); //经办人姓名
                        data.opt_time = dRow["opt_time"].ToString(); //经办时间
                        data.chfpdr_name = dRow["chfpdr_name"].ToString(); //主诊医师姓名
                        data.dscg_maindiag_name = dRow["dscg_maindiag_name"].ToString(); //住院主诊断名称
                        data.medfee_sumamt = dRow["medfee_sumamt"].ToString(); //医疗总费用
                        data.elec_bill_code = dRow["elec_bill_code"].ToString(); //电子票据代码
                        data.elec_billno_code = dRow["elec_billno_code"].ToString(); //电子票据号码
                        data.elec_bil_chkcode = dRow["elec_bil_chkcode"].ToString(); //电子票据校验码
                        data.exp_content = dRow["exp_content"].ToString(); //扩展字段
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return null;
            }
            return data;
        }
        #endregion

        #region  【4202】自费病人诊断信息(节点标识：ownPayPatnDiagListD)
        public List<Models.Request.RequestGzsiModel4202.OwnPayPatnDiagListD> Get4202OwnPayPatnDiagListD(string InpatientNo)
        {
            List<Models.Request.RequestGzsiModel4202.OwnPayPatnDiagListD> list = new List<Models.Request.RequestGzsiModel4202.OwnPayPatnDiagListD>();

            #region sql
            string strSql = @"select * from view_4202_OwnPayPatnDiagListD where INPATIENT_NO = '{0}'";
            strSql = string.Format(strSql, InpatientNo);
            #endregion


            try
            {
                DataSet dsSet = null;
                if (this.ExecQuery(strSql, ref dsSet) == -1)
                {
                    return list;
                }
                DataTable al = new DataTable();
                if (dsSet == null || dsSet.Tables.Count <= 0)
                {
                    return list;
                }
                al = dsSet.Tables[0];
                if (al == null || al.Rows.Count <= 0)
                {
                    return list;
                }


                if (al != null && al.Rows.Count > 0)
                {
                    foreach (DataRow dRow in al.Rows)
                    {
                        Models.Request.RequestGzsiModel4202.OwnPayPatnDiagListD data = new Models.Request.RequestGzsiModel4202.OwnPayPatnDiagListD();
                        data.inout_diag_type = dRow["inout_diag_type"].ToString(); //出入院诊断类别
                        data.diag_type = dRow["diag_type"].ToString(); //诊断类别
                        data.maindiag_flag = dRow["maindiag_flag"].ToString(); //主诊断标志
                        data.diag_srt_no = dRow["diag_srt_no"].ToString(); //诊断排序号
                        data.diag_code = dRow["diag_code"].ToString(); //诊断代码
                        data.diag_name = dRow["diag_name"].ToString(); //诊断名称
                        data.diag_dept = dRow["diag_dept"].ToString(); //诊断科室
                        data.diag_dr_code = dRow["diag_dr_code"].ToString(); //诊断医师代码
                        data.diag_dr_name = dRow["diag_dr_name"].ToString(); //诊断医师姓名
                        data.diag_time = dRow["diag_time"].ToString(); //诊断时间
                        data.vali_flag = dRow["vali_flag"].ToString(); //有效标志
                        list.Add(data);
                    }
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return null;
            }
            return list;
        }
        #endregion

        ///// <summary>
        ///// 更新结算清单信息上传标识
        ///// </summary>
        ///// <returns></returns>
        //public int UpdateInOwnPay(string inpatient_no, string InvoiceNo)
        //{
        //    string strSql = String.Empty;

        //    strSql = @"update fin_ipr_inmaininfo i
        //                            set i.upload_flagownpay = '1',
        //                                i.upload_err = '',
        //                                i.upload_date = sysdate
        //                            where i.inpatient_no = '{0}'";


        //    try
        //    {
        //        strSql = string.Format(strSql, inpatient_no);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ErrCode = ex.Message;
        //        this.Err = ex.Message;
        //        return -1;
        //    }
        //    return this.ExecNoQuery(strSql);
        //}

        ///// <summary>
        ///// 更新结算清单信息上传标识
        ///// </summary>
        ///// <returns></returns>
        //public int UpdateInOwnPayERR(string inpatient_no, string InvoiceNo, string uploadErr)
        //{
        //    string strSql = String.Empty;

        //    strSql = @"update fin_ipr_inmaininfo i
        //                            set i.upload_flagownpay = '0',
        //                                i.upload_err = '{1}',
        //                                i.upload_date = sysdate
        //                            where i.inpatient_no = '{0}'";


        //    try
        //    {
        //        strSql = string.Format(strSql, inpatient_no, uploadErr);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ErrCode = ex.Message;
        //        this.Err = ex.Message;
        //        return -1;
        //    }
        //    return this.ExecNoQuery(strSql);
        //}
        #endregion

        #region 门诊
        #region  【4205】自费病人门诊就诊信息(节点标识：mdtrtinfo)
        public Models.Request.RequestGzsiModel4205.Mdtrtinfo Get4205Mdtrtinfo(string InpatientNo, string InvoiceNo)
        {
            Models.Request.RequestGzsiModel4205.Mdtrtinfo data = new Models.Request.RequestGzsiModel4205.Mdtrtinfo();
            #region sql
            string strSql = @"select * from view_4205_Mdtrtinfo where INPATIENT_NO = '{0}'  and invoice_no = '{1}'";
            strSql = string.Format(strSql, InpatientNo, InvoiceNo);
            #endregion


            try
            {
                DataSet dsSet = null;
                if (this.ExecQuery(strSql, ref dsSet) == -1)
                {
                    return data;
                }
                DataTable al = new DataTable();
                if (dsSet == null || dsSet.Tables.Count <= 0)
                {
                    return data;
                }
                al = dsSet.Tables[0];
                if (al == null || al.Rows.Count <= 0)
                {
                    return data;
                }


                if (al != null && al.Rows.Count > 0)
                {
                    foreach (DataRow dRow in al.Rows)
                    {
                        data.fixmedins_mdtrt_id = dRow["fixmedins_mdtrt_id"].ToString(); //医药机构就诊ID
                        data.fixmedins_code = dRow["fixmedins_code"].ToString(); //定点医药机构编号
                        data.fixmedins_name = dRow["fixmedins_name"].ToString(); //定点医药机构名称
                        data.psn_cert_type = dRow["psn_cert_type"].ToString(); //人员证件类型
                        data.certno = dRow["certno"].ToString(); //证件号码
                        data.psn_name = dRow["psn_name"].ToString(); //人员姓名
                        data.gend = dRow["gend"].ToString(); //性别
                        data.naty = dRow["naty"].ToString(); //民族
                        data.brdy = dRow["brdy"].ToString(); //出生日期
                        data.age = dRow["age"].ToString(); //年龄
                        data.coner_name = dRow["coner_name"].ToString(); //联系人姓名
                        data.tel = dRow["tel"].ToString(); //联系电话
                        data.addr = dRow["addr"].ToString(); //联系地址
                        data.begntime = dRow["begntime"].ToString(); //开始时间
                        data.endtime = dRow["endtime"].ToString(); //结束时间
                        data.med_type = dRow["med_type"].ToString(); //医疗类别
                        data.main_cond_dscr = dRow["main_cond_dscr"].ToString(); //主要病情描述
                        data.dise_codg = dRow["dise_codg"].ToString(); //病种编码
                        data.dise_name = dRow["dise_name"].ToString(); //病种名称
                        data.birctrl_type = dRow["birctrl_type"].ToString(); //计划生育手 术类别
                        data.birctrl_matn_date = dRow["birctrl_matn_date"].ToString(); //计划生育手术或生育日期
                        data.matn_type = dRow["matn_type"].ToString(); //生育类别
                        data.geso_val = dRow["geso_val"].ToString(); //孕周数
                        data.elec_bill_code = dRow["elec_bill_code"].ToString(); //电子票据代码
                        data.elec_billno_code = dRow["elec_billno_code"].ToString(); //电子票据号码
                        data.elec_bill_chkcode = dRow["elec_bill_chkcode"].ToString(); //电子票据校验码
                        data.exp_content = dRow["exp_content"].ToString(); //字段扩展
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return null;
            }
            return data;
        }
        #endregion

        #region  【4205】自费病人门诊诊断信息(节点标识：diseinfo)
        public List<Models.Request.RequestGzsiModel4205.Diseinfo> Get4205Diseinfo(string InpatientNo)
        {
            List<Models.Request.RequestGzsiModel4205.Diseinfo> list = new List<Models.Request.RequestGzsiModel4205.Diseinfo>();

            #region sql
            string strSql = @"select * from view_4205_Diseinfo where INPATIENT_NO = '{0}'";
            strSql = string.Format(strSql, InpatientNo);
            #endregion


            try
            {
                DataSet dsSet = null;
                if (this.ExecQuery(strSql, ref dsSet) == -1)
                {
                    return list;
                }
                DataTable al = new DataTable();
                if (dsSet == null || dsSet.Tables.Count <= 0)
                {
                    return list;
                }
                al = dsSet.Tables[0];
                if (al == null || al.Rows.Count <= 0)
                {
                    return list;
                }


                if (al != null && al.Rows.Count > 0)
                {
                    foreach (DataRow dRow in al.Rows)
                    {
                        Models.Request.RequestGzsiModel4205.Diseinfo data = new Models.Request.RequestGzsiModel4205.Diseinfo();
                        data.diag_type = dRow["diag_type"].ToString(); //诊断类别
                        data.diag_srt_no = dRow["diag_srt_no"].ToString(); //诊断排序号
                        data.diag_code = dRow["diag_code"].ToString(); //诊断代码
                        data.diag_name = dRow["diag_name"].ToString(); //诊断名称
                        data.diag_dept = dRow["diag_dept"].ToString(); //诊断科室
                        data.diag_dr_code = dRow["diag_dr_code"].ToString(); //诊断医生编码
                        data.diag_dr_name = dRow["diag_dr_name"].ToString(); //诊断医生姓名
                        data.diag_time = dRow["diag_time"].ToString(); //诊断时间
                        data.vali_flag = dRow["vali_flag"].ToString(); //有效标志
                        list.Add(data);
                    }
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return null;
            }
            return list;
        }
        #endregion

        #region  【4205】自费病人门诊费用明细信息(节点标识：feedetail)
        public List<Models.Request.RequestGzsiModel4205.Feedetail> Get4205Feedetail(string InpatientNo, string InvoiceNo)
        {
            List<Models.Request.RequestGzsiModel4205.Feedetail> list = new List<Models.Request.RequestGzsiModel4205.Feedetail>();

            #region sql
            string strSql = @"select * from view_4205_Feedetail where INPATIENT_NO = '{0}' and invoice_no = '{1}'";
            strSql = string.Format(strSql, InpatientNo, InvoiceNo);
            #endregion


            try
            {
                DataSet dsSet = null;
                if (this.ExecQuery(strSql, ref dsSet) == -1)
                {
                    return list;
                }
                DataTable al = new DataTable();
                if (dsSet == null || dsSet.Tables.Count <= 0)
                {
                    return list;
                }
                al = dsSet.Tables[0];
                if (al == null || al.Rows.Count <= 0)
                {
                    return list;
                }


                if (al != null && al.Rows.Count > 0)
                {
                    foreach (DataRow dRow in al.Rows)
                    {
                        Models.Request.RequestGzsiModel4205.Feedetail data = new Models.Request.RequestGzsiModel4205.Feedetail();
                        data.memo = new Models.Request.RequestGzsiModel4205.Memo();
                        data.fixmedins_mdtrt_id = dRow["fixmedins_mdtrt_id"].ToString(); //医药机构就诊ID
                        data.med_type = dRow["med_type"].ToString(); //医疗类别
                        data.bkkp_sn = dRow["bkkp_sn"].ToString(); //记账流水号
                        data.fee_ocur_time = dRow["fee_ocur_time"].ToString(); //费用发生时间
                        data.fixmedins_code = dRow["fixmedins_code"].ToString(); //定点医药机构编号
                        data.fixmedins_name = dRow["fixmedins_name"].ToString(); //定点医药机构名称
                        data.cnt = dRow["cnt"].ToString(); //数量
                        data.pric = dRow["pric"].ToString(); //单价
                        data.det_item_fee_sumamt = dRow["det_item_fee_sumamt"].ToString(); //明细项目费用总额
                        data.med_list_codg = dRow["med_list_codg"].ToString(); //医疗目录编码
                        data.medins_list_codg = dRow["medins_list_codg"].ToString(); //医药机构目 录编码
                        data.medins_list_name = dRow["medins_list_name"].ToString(); //医药机构目录名称
                        data.med_chrgitm_type = dRow["med_chrgitm_type"].ToString(); //医疗收费项目类别
                        data.prodname = dRow["prodname"].ToString(); //商品名
                        data.bilg_dept_codg = dRow["bilg_dept_codg"].ToString(); //开单科室编码
                        data.bilg_dept_name = dRow["bilg_dept_name"].ToString(); //开单科室名称
                        data.bilg_dr_code = dRow["bilg_dr_code"].ToString(); //开单医生编码
                        data.bilg_dr_name = dRow["bilg_dr_name"].ToString(); //开单医师姓名
                        data.acord_dept_codg = dRow["acord_dept_codg"].ToString(); //受单科室编码
                        data.acord_dept_name = dRow["acord_dept_name"].ToString(); //受单科室名称
                        data.acord_dr_code = dRow["acord_dr_code"].ToString(); //受单医生编码
                        data.acord_dr_name = dRow["acord_dr_name"].ToString(); //受单医生姓名
                        data.tcmdrug_used_way = dRow["tcmdrug_used_way"].ToString(); //中药使用方式
                        data.etip_flag = dRow["etip_flag"].ToString(); //外检标志
                        data.etip_hosp_code = dRow["etip_hosp_code"].ToString(); //外检医院编码
                        data.dscg_tkdrug_flag = dRow["dscg_tkdrug_flag"].ToString(); //出院带药标志
                        data.sin_dos_dscr = dRow["sin_dos_dscr"].ToString(); //单次剂量描 述
                        data.used_frqu_dscr = dRow["used_frqu_dscr"].ToString(); //使用频次描述
                        data.prd_days = dRow["prd_days"].ToString(); //周期天数
                        data.medc_way_dscr = dRow["medc_way_dscr"].ToString(); //用药途径描述
                        data.memo.hosp_appr_flag = dRow["hosp_appr_flag"].ToString(); //医院审核标志
                        data.memo.invoice_no = dRow["hosp_appr_flag"].ToString(); //发票号
                        data.memo.memo = dRow["memo"].ToString(); //备注
                        data.fulamt_ownpay_amt = dRow["fulamt_ownpay_amt"].ToString(); //全自费金额
                        data.overlmt_selfpay = dRow["overlmt_selfpay"].ToString(); //超限价金额
                        data.preselfpay_amt = dRow["preselfpay_amt"].ToString(); //先行自付金额
                        data.inscp_amt = dRow["inscp_amt"].ToString(); //符合政策范围金额
                        data.rxno = dRow["rxno"].ToString(); //处方号
                        list.Add(data);
                    }
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return null;
            }
            return list;
        }
        #endregion

        ///// <summary>
        ///// 更新结算清单信息上传标识
        ///// </summary>
        ///// <returns></returns>
        //public int UpdateOutOwnPay(string inpatient_no, string InvoiceNo)
        //{
        //    string strSql = String.Empty;

        //    strSql = @"update fin_opb_invoiceinfo i
        //                        set i.upload_flagownpay = '1',
        //                            i.upload_err = '',
        //                            i.upload_date = sysdate
        //                        where i.clinic_code = '{0}' and  i.invoice_no = '{1}'";


        //    try
        //    {
        //        strSql = string.Format(strSql, inpatient_no, InvoiceNo);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ErrCode = ex.Message;
        //        this.Err = ex.Message;
        //        return -1;
        //    }
        //    return this.ExecNoQuery(strSql);
        //}

        ///// <summary>
        ///// 更新结算清单信息上传标识
        ///// </summary>
        ///// <returns></returns>
        //public int UpdateOutOwnPayERR(string inpatient_no, string InvoiceNo, string uploadErr)
        //{
        //    string strSql = String.Empty;

        //    strSql = @"update fin_opb_invoiceinfo i
        //                        set i.upload_flagownpay = '0',
        //                            i.upload_err = '{2}',
        //                            i.upload_date = sysdate
        //                        where i.clinic_code = '{0}' and  i.invoice_no = '{1}'";


        //    try
        //    {
        //        strSql = string.Format(strSql, inpatient_no, InvoiceNo, uploadErr);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ErrCode = ex.Message;
        //        this.Err = ex.Message;
        //        return -1;
        //    }
        //    return this.ExecNoQuery(strSql);
        //}
        #endregion

        #region 日志
        /// <summary>
        /// 更新结算清单信息上传标识
        /// </summary>
        /// <returns></returns>
        public int InsertOwnPayLog(string inpatient_no, string InvoiceNo, string state, string uploadErr)
        {
            string strSql = String.Empty;

            strSql = @"insert into FIN_CAS_WONPAY_LOG(
                            inpatient_no, invoice_no, state, errinfo, oper_code, oper_date    
                        )
                        values ('{0}','{1}','{2}','{3}','{4}',sysdate)";

            try
            {
                strSql = string.Format(strSql, inpatient_no, InvoiceNo, state, uploadErr, this.Operator.ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新结算清单信息上传标识
        /// </summary>
        /// <returns></returns>
        public int UpdateOwnPayLog(string inpatient_no, string InvoiceNo, string state, string uploadErr)
        {
            string strSql = String.Empty;

            strSql = @"update FIN_CAS_WONPAY_LOG l
                        set l.state = '{2}',
                            l.errinfo = '{3}',
                            l.oper_code = '{4}',
                            l.oper_date = sysdate
                        where l.inpatient_no = '{0}' and l.invoice_no = '{1}'";

            try
            {
                strSql = string.Format(strSql, inpatient_no, InvoiceNo, state, uploadErr, this.Operator.ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        #endregion
        #endregion
    }
}
