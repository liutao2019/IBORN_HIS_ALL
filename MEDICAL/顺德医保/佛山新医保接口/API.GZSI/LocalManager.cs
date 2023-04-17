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
            string currentBalanceNo = this.getBalanceNo(inpatientNo,type);
            if (string.IsNullOrEmpty(currentBalanceNo))
            {
                currentBalanceNo = "0";
            }
            int nextBalanceNO = int.Parse(currentBalanceNo) + 1;

            return nextBalanceNO;
        }

        /// <summary>
        /// 获取医保信息
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="typeCode"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Base.Const> GetPatientInfo(string cardNO, string typeCode, string beginTime, string endTime)
        {

            List<FS.HISFC.Models.Base.Const> al = new List<FS.HISFC.Models.Base.Const>();
            string strSql = @"select inpatient_no,
                                     card_no,
                                     name,
                                     in_date
                                from fin_ipr_siinmaininfo
                               WHERE card_no = '{0}'
                                 and valid_flag = '1'
                                 and type_code = '{1}'
                                 and oper_date between to_date('{2}','YYYY-MM-DD hh24:mi:ss')
                                                   and to_date('{3}','YYYY-MM-DD hh24:mi:ss')";

            try
            {
                strSql = string.Format(strSql, cardNO, typeCode, beginTime, endTime);
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
                    FS.HISFC.Models.Base.Const obj = new FS.HISFC.Models.Base.Const();


                    obj.ID = Reader[0].ToString(); //住院流水号
                    obj.Name = Reader[1].ToString(); //卡号
                    obj.SpellCode = Reader[2].ToString(); //姓名
                    obj.WBCode = Reader[2].ToString(); //姓名

                    al.Add(obj);

                }
                Reader.Close();
                return al;
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
        /// 获取医保信息
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="typeCode"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int GetPatientInfo(ref FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {

            // {0E33B442-43B6-41C9-B509-9E0DBC719551}
            List<FS.HISFC.Models.Base.Const> al = new List<FS.HISFC.Models.Base.Const>();
            string strSql = @"select t.setl_id,
                                     t.type_code,
                                     t.dise_code
                                from fin_ipr_siinmaininfo t
                               where 1=1
                                 --and t.patient_no = '{0}'
                                 and t.mdtrt_id = '{1}'
                                 and t.psn_no = '{2}'
                                 and t.valid_flag = '1'";

            try
            {
                strSql = string.Format(strSql, patientInfo.ID, patientInfo.SIMainInfo.Mdtrt_id, patientInfo.SIMainInfo.Psn_no);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            this.ExecQuery(strSql);
            try
            {
                while (Reader.Read())
                {
                    patientInfo.SIMainInfo.Setl_id = Reader[0].ToString();   //结算序号
                    patientInfo.SIMainInfo.TypeCode = Reader[1].ToString();  //实体类型
                    patientInfo.SIMainInfo.Dise_code = Reader[2].ToString(); //病种编码
                }

                Reader.Close();
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                Reader.Close();
                return -1;
            }

            return 1;
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
        /// 获取医保信息
        /// </summary>
        public string GetSIPsnNo(string IDCard)
        {

            string strSql = @"select s.psn_no  from fin_ipr_siinmaininfo s where s.idenno = '{0}'";
            try
            {
                strSql = string.Format(strSql, IDCard);
                return this.ExecSqlReturnOne(strSql);
           }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
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

        /// <summary>
        /// 根据科室编码获取科室名称
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public string getDeptName(string deptCode)
        {
            string strSql = @"select dept_name from com_department where dept_code = '{0}'";

            try
            {
                strSql = string.Format(strSql, deptCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }

            try
            {
                this.ExecQuery(strSql);

                string deptName = "";

                while (Reader.Read())
                {
                    deptName = Reader[0].ToString();
                }

                Reader.Close();

                return deptName;
            }
            catch(Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
        }

        /// <summary>
        /// 根据科室编码获取科室医保对照吗
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public string getDeptYBCode(string deptCode)
        {
            string strSql = @"select bz_dept_code from com_department where dept_code = '{0}'";

            strSql = string.Format(strSql, deptCode);

            try
            {
                this.ExecQuery(strSql);

                string deptYBCode = deptCode;

                while (Reader.Read())
                {
                    deptYBCode = Reader[0].ToString();
                }

                Reader.Close();

                return deptYBCode;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return deptCode;
            }
        }

        /// <summary>
        /// 根据员工编码获取员工名称
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public string getEmplName(string deptCode)
        {
            string strSql = @"select empl_name from com_employee where empl_code = '{0}'";

            try
            {
                strSql = string.Format(strSql, deptCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }

            try
            {
                this.ExecQuery(strSql);

                string deptName = "";

                while (Reader.Read())
                {
                    deptName = Reader[0].ToString();
                }

                Reader.Close();

                return deptName;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
        }

        /// <summary>
        /// 获取医保结算信息
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int GetBalanceInfo(ref FS.HISFC.Models.Registration.Register register, string typeCode)
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
                                     VOLA_DSCR,
                                     VALID_FLAG,
                                     TYPE_CODE
                                FROM FIN_IPR_SIINMAININFO
                               WHERE INPATIENT_NO = '{0}' 
                                 AND INVOICE_NO = '{1}'
                                 AND TYPE_CODE = '{2}'";

            try
            {
                strSql = string.Format(strSql, register.ID, register.SIMainInfo.InvoiceNo, typeCode);
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
                    register.SIMainInfo.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[50].ToString());//是否有效
                    register.SIMainInfo.TypeCode = Reader[51].ToString();//就诊类型 1-门诊，2-住院
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
        /// 住院物理治疗只报销消费最高的两个项目
        /// </summary>
        /// <param name="mdtrt_id"></param>
        /// <param name="setl_id"></param>
        /// <param name="type_code"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetPhysicalSort(string inpatientno,out string errorMsg)
        {
            errorMsg = string.Empty;

            Dictionary<string, string> PhysicalDictionary = new Dictionary<string, string>();
            System.Data.DataSet ds = new System.Data.DataSet();

            string sql = @"select rownum,a.item_code,a.item_name
                            from (
                            select t2.item_code,t2.item_name,sum(t.qty*decode(t2.unit_price2,0,t2.unit_price,t2.unit_price2)) tot_cost
                              from fin_ipb_itemlist t left join fin_com_undruginfo t2 on t.item_code = t2.item_code
                             where t.inpatient_no = '{0}'
                               and t2.fee_code = '068'
                               and t.upload_flag != '2'
                             group by t2.item_code,t2.item_name
                             order by sum(t.qty*decode(t2.unit_price2,0,t2.unit_price,t2.unit_price2)) desc) a ";
            try
            {
                sql = string.Format(sql, inpatientno);

                if (this.ExecQuery(sql, ref ds) < 0)
                {
                    errorMsg = "";
                    return null;
                }

                foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                {
                    PhysicalDictionary.Add(dr[1].ToString(), dr[0].ToString());
                }
            }
            catch(Exception e)
            {
                errorMsg += e.Message;
                return null;
            }
            return PhysicalDictionary;
        }

        /// <summary>
        /// 查询总金额发票明细
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        public ArrayList GetFeeTypeDetail(string mdtrt_id, string setl_id, string type_code)
        {
            ArrayList feeTypeList = new ArrayList();
            System.Data.DataSet ds = new System.Data.DataSet();

//            string sql = @"select nvl(t3.fee_stat_name,'其他费') fee_stat_name,
//                                  sum(t.det_item_fee_sumamt) feetype
//                             from gzsi_feedetail t  
//                             left join gzsi_his_mzxm t2 on t.feedetl_sn = t2.xmxh and t.mdtrt_id = t2.jydjh
//                             left join fin_com_feecodestat t3 on t2.fee_code = t3.fee_code and t3.report_code = 'SIMZ01'
//                            where t.mdtrt_id = '{0}' and t.setl_id = '{1}'
//                            group by t3.fee_stat_name";

//            //住院与门诊查询的明细表不同
//            if (type_code == "2")
//            {
//                sql = @"select nvl(t3.fee_stat_name,'其他费') fee_stat_name,
//                               sum(t.det_item_fee_sumamt) feetype
//                          from gzsi_feedetail t  
//                          left join gzsi_his_cfxm t2 on t.feedetl_sn = t2.xmxh and t.mdtrt_id = t2.jydjh
//                          left join fin_com_feecodestat t3 on t2.fee_code = t3.fee_code
//                                                          and t3.report_code = 'SIZY01'
//                         where t.mdtrt_id = '{0}' and t.setl_id = '{1}'
//                         group by t3.fee_stat_name";
//            }


            string sql = @" select t2.name fee_stat_name, 
                                   sum(t.det_item_fee_sumamt) feetype
                              from gzsi_feedetail t
                              left join com_dictionary t2
                                on t2.type = 'GZSI_med_chrgitm_type'
                               and nvl(t.med_chrgitm_type,'14') = t2.code
                             where t.mdtrt_id = '{0}'
                               and t.setl_id = '{1}'
                             group by t2.name";

            try
            {
                sql = string.Format(sql, mdtrt_id, setl_id);

                if (this.ExecQuery(sql, ref ds) < 0)
                {
                    return null;
                }

                foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                {
                    FS.HISFC.Models.Base.Const feetype = new FS.HISFC.Models.Base.Const();
                    feetype.ID = dr[0].ToString();
                    feetype.Name = dr[0].ToString();
                    feetype.Memo = dr[1].ToString();
                    feeTypeList.Add(feetype);
                }
            }
            catch
            {
                return null;
            }

            return feeTypeList;
        }

        /// <summary>
        /// 查询全自费发票明细
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        public ArrayList GetOverlmtFeeTypeDetail(string mdtrt_id, string setl_id, string type_code)
        {
            ArrayList feeTypeList = new ArrayList();
            System.Data.DataSet ds = new System.Data.DataSet();

//            string sql = @"select nvl(t3.fee_stat_name,'其他费') fee_stat_name,
//                                  sum(t.fulamt_ownpay_amt) feetype
//                             from gzsi_feedetail t  
//                             left join gzsi_his_mzxm t2 on t.feedetl_sn = t2.xmxh and t.mdtrt_id = t2.jydjh
//                             left join fin_com_feecodestat t3 on t2.fee_code = t3.fee_code and t3.report_code = 'SIMZ01'
//                            where t.mdtrt_id = '{0}' and t.setl_id = '{1}'
//                            group by t3.fee_stat_name";

//            //住院与门诊查询的明细表不同
//            if (type_code == "2")
//            {
//                sql = @"select nvl(t3.fee_stat_name,'其他费') fee_stat_name,
//                               sum(t.fulamt_ownpay_amt) feetype
//                          from gzsi_feedetail t  
//                          left join gzsi_his_cfxm t2 on t.feedetl_sn = t2.xmxh and t.mdtrt_id = t2.jydjh
//                          left join fin_com_feecodestat t3 on t2.fee_code = t3.fee_code
//                                                          and t3.report_code = 'SIZY01'
//                         where t.mdtrt_id = '{0}' and t.setl_id = '{1}'
//                         group by t3.fee_stat_name";
//            }

            string sql = @" select t2.name fee_stat_name, 
                                   sum(t.fulamt_ownpay_amt) feetype
                              from gzsi_feedetail t
                              left join com_dictionary t2
                                on t2.type = 'GZSI_med_chrgitm_type'
                               and nvl(t.med_chrgitm_type,'14') = t2.code
                             where t.mdtrt_id = '{0}'
                               and t.setl_id = '{1}'
                             group by t2.name";

            try
            {
                sql = string.Format(sql, mdtrt_id, setl_id);

                if (this.ExecQuery(sql, ref ds) < 0)
                {
                    return null;
                }

                foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                {
                    FS.HISFC.Models.Base.Const feetype = new FS.HISFC.Models.Base.Const();
                    feetype.ID = dr[0].ToString();
                    feetype.Name = dr[0].ToString();
                    feetype.Memo = dr[1].ToString();
                    feeTypeList.Add(feetype);
                }
            }
            catch
            {
                return null;
            }

            return feeTypeList;
        }

        /// <summary>
        /// 查询部分自费发票明细
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        public ArrayList GetPreselfPayFeeTypeDetail(string mdtrt_id, string setl_id, string type_code)
        {
            ArrayList feeTypeList = new ArrayList();
            System.Data.DataSet ds = new System.Data.DataSet();

//            string sql = @"select nvl(t3.fee_stat_name,'其他费') fee_stat_name,
//                                  sum(t.overlmt_amt) feetype
//                             from gzsi_feedetail t  
//                             left join gzsi_his_mzxm t2 on t.feedetl_sn = t2.xmxh and t.mdtrt_id = t2.jydjh
//                             left join fin_com_feecodestat t3 on t2.fee_code = t3.fee_code and t3.report_code = 'SIMZ01'
//                            where t.mdtrt_id = '{0}' and t.setl_id = '{1}'
//                            group by t3.fee_stat_name";

//            //住院与门诊查询的明细表不同
//            if (type_code == "2")
//            {
//                sql = @"select nvl(t3.fee_stat_name,'其他费') fee_stat_name,
//                               sum(t.overlmt_amt) feetype
//                          from gzsi_feedetail t  
//                          left join gzsi_his_cfxm t2 on t.feedetl_sn = t2.xmxh and t.mdtrt_id = t2.jydjh
//                          left join fin_com_feecodestat t3 on t2.fee_code = t3.fee_code
//                                                          and t3.report_code = 'SIZY01'
//                         where t.mdtrt_id = '{0}' and t.setl_id = '{1}'
//                         group by t3.fee_stat_name";
//            }

            string sql = @" select t2.name fee_stat_name, 
                                   sum(t.overlmt_amt) feetype
                              from gzsi_feedetail t
                              left join com_dictionary t2
                                on t2.type = 'GZSI_med_chrgitm_type'
                               and nvl(t.med_chrgitm_type,'14') = t2.code
                             where t.mdtrt_id = '{0}'
                               and t.setl_id = '{1}'
                             group by t2.name";

            try
            {
                sql = string.Format(sql, mdtrt_id, setl_id);

                if (this.ExecQuery(sql, ref ds) < 0)
                {
                    return null;
                }

                foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                {
                    FS.HISFC.Models.Base.Const feetype = new FS.HISFC.Models.Base.Const();
                    feetype.ID = dr[0].ToString();
                    feetype.Name = dr[0].ToString();
                    feetype.Memo = dr[1].ToString();
                    feeTypeList.Add(feetype);
                }
            }
            catch
            {
                return null;
            }

            return feeTypeList;
        }

        /// <summary>
        /// 更新费用结算明细表
        /// </summary>
        /// <returns></returns>
        public int UpdateSIfeeDetail(string mdtrt_id, string setl_id, string feedetl_sn, string type_code, string det_item_fee_sumamt, string overlmt_amt, string preselfpay_amt, string fulamt_ownpay_amt)
        {
            string strSql = @"update gzsi_feedetail t
                                 set t.det_item_fee_sumamt = '{4}',
                                     t.overlmt_amt = '{5}',
                                     t.preselfpay_amt = '{6}',
                                     t.fulamt_ownpay_amt = '{7}'
                               where t.mdtrt_id = '{0}'
                                 and t.setl_id = '{1}'
                                 and t.feedetl_sn = '{2}'
                                 and t.type_code = '{3}'";

            try
            {
                strSql = string.Format(strSql, mdtrt_id, setl_id, feedetl_sn, type_code, det_item_fee_sumamt, overlmt_amt, preselfpay_amt, fulamt_ownpay_amt);
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
                                     VOLA_DSCR,
                                     VALID_FLAG
                                FROM FIN_IPR_SIINMAININFO
                               WHERE INPATIENT_NO = '{0}' 
                                 AND INVOICE_NO = '{1}'
                                 AND TYPE_CODE = '{2}'
                                 AND VALID_FLAG = '1'";

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
                    register.SIMainInfo.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[50].ToString());//是否有效
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
        /// 根据挂号记录查询结算记录
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public ArrayList GetInvoiceList(FS.HISFC.Models.Registration.Register register,string typeCode)
        {
            ArrayList invoiceList = new ArrayList();
            System.Data.DataSet ds = new System.Data.DataSet();

            string sql = @"select t.reg_no,
                                  t.invoice_no,
                                  t.balance_no,
                                  t.setl_time
                         from fin_ipr_siinmaininfo t
                        where t.inpatient_no = '{0}'
                           and t.type_code = '{1}'
                           and t.balance_state = '1'
                           and t.valid_flag = '1'";

            try
            {
                sql = string.Format(sql, register.ID, typeCode);

                if (this.ExecQuery(sql, ref ds) < 0)
                {
                    return null;
                }

                foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                {
                    FS.HISFC.Models.Base.Const invoice = new FS.HISFC.Models.Base.Const();
                    invoice.ID = dr[0].ToString();
                    invoice.Name = dr[1].ToString();
                    invoice.Memo = dr[2].ToString();
                    invoice.SpellCode = dr[3].ToString();
                    invoiceList.Add(invoice);
                }
            }
            catch
            {
                return null;
            }

            return invoiceList;
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
            int nextBalanceNo = this.GetNextBalanceNo(patientInfo.ID,"1");
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
                                           '{4}',
                                           '{5}',
                                           '{6}',
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
        public int InsertLimitAccountFeeItem(FS.HISFC.Models.Fee.Outpatient.FeeItemList f,string itemid,string itemname,decimal qty ,string clincid, FS.HISFC.Models.Registration.Register r)
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
        public int UpdateExpItemDetailQty(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Account.ExpItemMedical itemmedical,decimal qty)
        {

            string sqlStr = @"update exp_itemmedical set RTN_QTY=RTN_QTY - {4} , CONFIRM_QTY=CONFIRM_QTY + {4} ,OPER_CODE ='{3}',OPER_DATE = to_date('{2}','yyyy-MM-dd hh24:mi:ss') where clinic_code ='{1}' and card_no ='{0}' ";

            try
            {
                string sql = string.Format(sqlStr,r.PID.CardNO,  //就诊号
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
            string strSql = @" select t.diag_kind diag_type,
                                      t.happen_no diag_srt_no,
                                      --t.icd_code diag_code,
                                      nvl((select a.yb_code from met_com_icdcompare_yb a where a.icd10_code =t.icd_code and a.kind_code = 'ZD'),t.icd_code) diag_code,
                                      t.diag_name diag_name,
                                      (select  nvl((select b.bz_dept_code from com_department b where b.dept_code =a.dept_code ),a.dept_code)
                                         from fin_opr_register a 
                                        where a.clinic_code = t.inpatient_no 
                                          and a.trans_type = '1') diag_dept,
                                       --t.doct_code dise_dor_no,
                                      (select nvl(e.user_code,e.empl_code) from com_employee e where e.empl_code =t.doct_code) as dise_dor_no,
                                      t.doct_name dise_dor_name,
                                      t.diag_date diag_time,
                                      t.valid_flag vali_flag,
                                      decode(t.diag_kind,'1','1','0') maindiag_flag
                                 from met_cas_diagnose t
                                where t.inpatient_no = '{0}'
                                  and t.valid_flag = '1'
                                 and t.icd_code != 'MS999'--过滤描述诊断"; // {F9409746-2582-45A6-9673-E3F459BA474E}
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
                    diseinfo.diag_dept =  Reader[4].ToString();//诊断科室 
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

        #region 结算单

        /// <summary>
        /// 查询门诊结算单
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Base.Const> QueryAllFeeReg(string cardNo, string typeCode)
        {
            List<FS.HISFC.Models.Base.Const> alList = new List<FS.HISFC.Models.Base.Const>();

            string strSql = "";
            if (typeCode == "1")
            {
                strSql = @"select o.SETL_ID,o.mdtrt_id,r.name,r.reg_date,fun_get_employee_name(nvl(r.see_docd,r.doct_code)),o.insuplc_admdvs
                             ,o.invoice_no
                            from fin_opr_register r,fin_ipr_siinmaininfo o
                            where r.clinic_code = o.inpatient_no
                            and o.mdtrt_id is not null
                            and o.SETL_ID is not null
                            and o.valid_flag = '1'
                            and o.TYPE_CODE = '1'
                            and r.card_no = '{0}'
                            and r.reg_date > sysdate - 60
                            order by r.reg_date ";
            }
            else if (typeCode == "2")
            {
                strSql = @"select sii.setl_id,sii.mdtrt_id, i.name, i.in_date, i.out_date,sii.insuplc_admdvs
                           ,sii.invoice_no
                          from fin_ipr_inmaininfo i, fin_ipr_siinmaininfo sii
                         where i.inpatient_no = sii.inpatient_no
                           and i.patient_no = '{0}'
                           and sii.mdtrt_id is not null
                           and sii.setl_id is not null
                           and sii.valid_flag = '1'
                           order by i.in_date, sii.setl_id
                            ";
            }
            else if (typeCode == "3")
            {
                strSql = @"select sii.setl_id, sii.mdtrt_id, sii.name, sii.oper_date, '',sii.insuplc_admdvs,sii.invoice_no
                      from fin_ipr_siinmaininfo sii
                     where sii.mdtrt_id = '{0}'
                       and sii.valid_flag = '1'
                       and sii.setl_id is not null
                       order by sii.oper_date
                            ";
            }

            strSql = string.Format(strSql, cardNo);

            try
            {
                if (ExecQuery(strSql) == -1)
                {
                    return null;
                }
                while (Reader.Read())
                {
                    FS.HISFC.Models.Base.Const obj = new FS.HISFC.Models.Base.Const();
                    obj.ID = Reader[0].ToString();
                    obj.Name = Reader[1].ToString();
                    obj.Memo = Reader[2].ToString();
                    obj.SpellCode = Reader[3].ToString();
                    obj.WBCode = Reader[4].ToString();
                    obj.UserCode = Reader[5].ToString();
                    obj.OperEnvironment.ID = Reader[6].ToString();
                    alList.Add(obj);
                }
                Reader.Close();
            }
            catch (Exception e)
            {
                return null;
            }
            return alList;
        }

        /// <summary>
        /// 获取患者基本信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="typeCode"></param>
        public void GetPatientInfoByFeeInfo(string MDTRT_ID, string seilID, string cardNo, string typeCode, ref FS.HISFC.Models.RADT.PatientInfo p)
        {
            string inpatientNo = "";
            if (p == null)
            {
                p = new FS.HISFC.Models.RADT.PatientInfo();
            }
            try
            {
                #region 获取结算信息
                if (!string.IsNullOrEmpty(MDTRT_ID))
                {
                    string sqlStrFee = @" select sii.mdtrt_id,
                                  sii.setl_id,
                                  sii.psn_no,
                                  sii.patient_no,
                                  sii.card_no,
                                  sii.type_code,
                                  sii.inpatient_no,
                                  sii.insuplc_admdvs
                             from fin_ipr_siinmaininfo sii
                            where sii.MDTRT_ID = '{0}'
                              and sii.setl_id = '{1}'
                              and sii.valid_flag = '1'
                              and sii.balance_state = '1'";
                    sqlStrFee = string.Format(sqlStrFee, MDTRT_ID, seilID);

                    this.ExecQuery(sqlStrFee);
                    while (Reader.Read())
                    {
                        p.SIMainInfo.Mdtrt_id = Reader[0].ToString();//
                        p.SIMainInfo.Setl_id = Reader[1].ToString();//
                        p.SIMainInfo.Psn_no = Reader[2].ToString();//
                        p.PID.PatientNO = Reader[3].ToString();//
                        p.PID.CardNO = Reader[4].ToString();//
                        p.SIMainInfo.TypeCode = Reader[5].ToString();//
                        p.SIMainInfo.IsBalanced = true;
                        inpatientNo = Reader[6].ToString();//
                        p.SIMainInfo.Insuplc_admdvs = Reader[7].ToString();//
                    }

                    Reader.Close();
                    typeCode = p.SIMainInfo.TypeCode;
                    if (typeCode == "1")
                    {
                        cardNo = p.PID.CardNO;
                    }
                    else if (typeCode == "2")
                    {
                        cardNo = p.PID.PatientNO;
                    }

                }

                #endregion
                #region 获取诊断和科室
                if (!string.IsNullOrEmpty(inpatientNo))
                {
                    string sqlStrQueryBaseInfo = "";
                    #region SQL
                    if (typeCode == "1")
                    {
                        sqlStrQueryBaseInfo = @"select fun_get_dept_name(nvl(r.see_dpcd, r.doct_code)),
                               nvl((select e.icd_code
                                     from met_cas_diagnose e
                                    where e.inpatient_no = r.clinic_code
                                      and e.persson_type = '0'
                                      and e.valid_flag = '1'
                                      and e.diag_kind = '1'
                                      and rownum = 1),
                                   'Z00.800')
                          from fin_opr_register r
                         where r.clinic_code = '{0}'
                        ";
                    }
                    else
                    {
                        sqlStrQueryBaseInfo = @"select fun_get_dept_name(i.dept_code),
                                       (select e.icd_code
                                             from met_cas_diagnose e
                                            where e.inpatient_no = '{0}'
                                              and e.persson_type = '1'
                                              and e.valid_flag = '1'
                                              and e.diag_kind = '1'
                                              and rownum = 1)
                                  from fin_ipr_inmaininfo i
                                 where i.inpatient_no = '{0}'
                        ";
                    }
                    #endregion
                    sqlStrQueryBaseInfo = string.Format(sqlStrQueryBaseInfo, inpatientNo);
                    this.ExecQuery(sqlStrQueryBaseInfo);

                    while (Reader.Read())
                    {
                        p.PVisit.PatientLocation.Dept.Name = Reader[0].ToString();//
                        if (string.IsNullOrEmpty(p.SIMainInfo.InDiagnose.ID))
                        {
                            p.SIMainInfo.InDiagnose.ID = Reader[1].ToString();//
                        }
                    }

                    Reader.Close();

                    if (string.IsNullOrEmpty(p.SIMainInfo.InDiagnose.ID))
                    {
                        #region 三院诊断获取         // {D2498BBA-6F11-4af8-835B-0A55A777036F}
                        if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第三人民医院" && typeCode == "2")
                        {
                            sqlStrQueryBaseInfo = @"select fun_get_dept_name(i.dept_code),
                                   (select e.icd10_code
                                      from DIAG_DIAGNOSE             diagnose0_,
                                           DIAG_TRADITIONAL_DIAGNOSE diagnose0_1_,
                                           DIAG_WESTERN_DIAGNOSE     diagnose0_2_,
                                           DIAG_SI_DIAGNOSE          diagnose0_3_,
                                           pt_inpatient_cure         b,
                                           diag_creation             c,
                                           DXT_WESTERN_DISEASE       e,
                                           fin_ipr_inmaininfo        ii
                                     where diagnose0_.ID = diagnose0_1_.ID(+)
                                       and diagnose0_.ID = diagnose0_2_.ID(+)
                                       and diagnose0_.ID = diagnose0_3_.ID(+)
                                       and diagnose0_.VALID_STATE = 1
                                       and b.id = diagnose0_.VISIT_ID
                                       and b.patient_id = diagnose0_.PATIENT_ID
                                       and b.patient_id = c.patient_id
                                       and diagnose0_.DIAG_CREATION_ID = c.id
                                       and diagnose0_2_.disease_id = e.id
                                       and c.diagnose_type_code = '5502'
                                       and b.inpatient_code = ii.inpatient_no
                                       and b.inpatient_code = '{0}'
                                       and ii.in_state <> 'N'
                                       and diagnose0_.is_main_diagnose = '1'
                                       and rownum = 1)
                              from fin_ipr_inmaininfo i
                             where i.inpatient_no = '{0}'
                            ";
                            sqlStrQueryBaseInfo = string.Format(sqlStrQueryBaseInfo, inpatientNo);
                            this.ExecQuery(sqlStrQueryBaseInfo);

                            while (Reader.Read())
                            {
                                p.PVisit.PatientLocation.Dept.Name = Reader[0].ToString();//
                                if (string.IsNullOrEmpty(p.SIMainInfo.InDiagnose.ID))
                                {
                                    p.SIMainInfo.InDiagnose.ID = Reader[1].ToString();//
                                }
                            }

                            Reader.Close();
                        }
                        #endregion
                    }

                }
                #endregion

                #region 获取基本信息
                if (!string.IsNullOrEmpty(cardNo) && !string.IsNullOrEmpty(typeCode))
                {
                    string sqlStrQueryInfo = @"
select i.patient_no,
       i.name,
       i.card_no,
       i.bed_no,
       i.home,
       i.home_tel,
       i.linkman_name,
       i.linkman_tel,
       i.linkman_add,
       i.work_name,
       i.work_tel,
       i.work_zip
  from fin_ipr_inmaininfo i
 where i.patient_no = '{1}'
   and i.in_date = (select max(ii.in_date)
                      from fin_ipr_inmaininfo ii
                     where ii.patient_no = i.patient_no)
   and '2' = '{0}'
                             union all
                             select '',
                                    p.name,
                                    p.card_no,
                                    '',
                                    p.home,
                                    p.home_tel,
                                    p.linkman_name,
                                    p.linkman_tel,
                                    p.linkman_add,
                                    p.work_home,
                                    p.work_tel,
                                    p.work_zip
                                   from com_patientinfo p
                              where p.card_no = '{1}'
                                and '1' = '{0}'";

                    sqlStrQueryInfo = string.Format(sqlStrQueryInfo, typeCode, cardNo, inpatientNo);

                    this.ExecQuery(sqlStrQueryInfo);
                    while (Reader.Read())
                    {
                        p.PID.PatientNO = Reader[0].ToString();//
                        p.Name = Reader[1].ToString();//
                        p.PID.CardNO = Reader[2].ToString();//
                        p.PVisit.PatientLocation.Bed.ID = Reader[3].ToString();//
                        p.AddressHome = Reader[4].ToString();//
                        p.PhoneHome = Reader[5].ToString();//
                        p.Kin.Name = Reader[6].ToString();//
                        p.Kin.RelationPhone = Reader[7].ToString();//
                        p.Kin.RelationAddress = Reader[8].ToString();//
                        p.CompanyName = Reader[9].ToString();//
                        p.ExtendFlag = Reader[10].ToString();//
                        p.BusinessZip = Reader[11].ToString();//
                        //p.PVisit.PatientLocation.Dept.Name = Reader[12].ToString();//
                        //if (string.IsNullOrEmpty(p.SIMainInfo.InDiagnose.ID))
                        //{
                        //    p.SIMainInfo.InDiagnose.ID = Reader[13].ToString();//
                        //}
                    }

                    Reader.Close();
                }
                #endregion

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return;
            }
        }

        /// <summary>
        /// 获取结算明细
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <param name="isOut"></param>
        /// <returns></returns>
        public ArrayList GetInvoiceDetail(string invoiceNo, bool isOut)
        {
            string strSql = "";
            if (isOut)
            {
                strSql = @"select l.invo_code,l.invo_name,(l.pub_cost+l.own_cost+l.pay_cost) totCost from fin_opb_invoicedetail l
                        where l.invoice_no = '{0}'and l.cancel_flag = '1'";
            }
            else
            {
                strSql = @"select t.stat_code,t.stat_name,t.tot_cost from fin_ipb_balancelist t where t.invoice_no = '{0}' and t.trans_type = '1'";
            }
            ArrayList al = new ArrayList();
            strSql = string.Format(strSql, invoiceNo);
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
                    obj.Memo = Reader[2].ToString();// 
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
        /// 得到住院医保患者基本信息;
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <returns></returns>
        public bool GetSIPersonInfo(ref FS.HISFC.Models.RADT.Patient patient)
        {
            string inpatientNo = patient.ID;

            FS.HISFC.Models.RADT.PatientInfo inp = patient as FS.HISFC.Models.RADT.PatientInfo;

            FS.HISFC.Models.Registration.Register reg = patient as FS.HISFC.Models.Registration.Register;
            #region sql
            string strSql = @"SELECT MDTRT_ID,--就诊ID
                                     SETL_ID,--结算ID
                                     PSN_NO,--人员编号
                                     PSN_NAME,--人员姓名
                                     PSN_CERT_TYPE,--人员证件类型
                                     CERTNO,--证件号码
                                     GEND,--性别
                                     NATY,--民族
                                     BRDY,--出生日期
                                     AGE,--年龄
                                     INSUTYPE,--险种类型
                                     PSN_TYPE,--人员类别
                                     CVLSERV_FLAG,--公务员标志
                                     SETL_TIME,--结算时间
                                     PSN_SETLWAY,--个人结算方式
                                     MDTRT_CERT_TYPE,--就诊凭证类型
                                     MED_TYPE,--医疗类别
                                     MEDFEE_SUMAMT,--医疗费总额
                                     OWNPAY_AMT,--全自费金额
                                     OVERLMT_SELFPAY,--超限价自费费用
                                     PRESELFPAY_AMT,--先行自付金额
                                     INSCP_SCP_AMT,--符合范围金额
                                     MED_SUMFEE,--医保认可费用总额
                                     ACT_PAY_DEDC,--实际支付起付线
                                     HIFP_PAY,--基本医疗保险统筹基金支出
                                     POOL_PROP_SELFPAY,--基本医疗保险统筹基金支付比例
                                     CVLSERV_PAY,--公务员医疗补助资金支出
                                     HIFES_PAY,--企业补充医疗保险基金支出
                                     HIFMI_PAY,--居民大病保险资金支出
                                     HIFOB_PAY,--职工大额医疗费用补助基金支出
                                     HIFDM_PAY,--伤残人员医疗保障基金支出
                                     MAF_PAY,--医疗救助基金支出
                                     OTH_PAY,--其他基金支出
                                     FUND_PAY_SUMAMT,--基金支付总额
                                     HOSP_PART_AMT,--医院负担金额
                                     PSN_PART_AM,--个人负担总金额
                                     ACCT_PAY,--个人账户支出
                                     CASH_PAYAMT,--现金支付金额
                                     ACCT_MULAID_PAY,--账户共济支付金额
                                     BALC,--个人账户支出后余额
                                     CLR_OPTINS,--清算经办机构
                                     CLR_WAY,--清算方式
                                     CLR_TYPE,--清算类别
                                     MEDINS_SETL_ID,--医药机构结算ID
                                     VOLA_TYPE,--违规类型
                                     VOLA_DSCR,--违规说明
                                     in_diagnose,
                                     in_diagnosename,
                                     insuplc_admdvs,
                                     balance_state
                                FROM fin_ipr_siinmaininfo   --广东省统一医保信息住院主表
                               WHERE inpatient_no = '{0}'
                                 and valid_flag = '1'
                                 and mdtrt_id is not null
                                 and type_code = '2'";

            try
            {
                strSql = string.Format(strSql, inpatientNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return false;
            }
            this.ExecQuery(strSql);

            #endregion sql

            try
            {
                while (Reader.Read())
                {
                    this.SetSIInPatient(ref inp);
                    return true;
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
        /// 得到住院医保患者基本信息;
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <returns></returns>
        public bool GetSIPersonInfo(string Mdtrt_id, string Setl_id, ref FS.HISFC.Models.RADT.PatientInfo obj)
        {
            #region sql
            string strSql = @"SELECT MDTRT_ID,--就诊ID
                                     SETL_ID,--结算ID
                                     PSN_NO,--人员编号
                                     PSN_NAME,--人员姓名
                                     PSN_CERT_TYPE,--人员证件类型
                                     CERTNO,--证件号码
                                     GEND,--性别
                                     NATY,--民族
                                     BRDY,--出生日期
                                     AGE,--年龄
                                     INSUTYPE,--险种类型
                                     PSN_TYPE,--人员类别
                                     CVLSERV_FLAG,--公务员标志
                                     SETL_TIME,--结算时间
                                     PSN_SETLWAY,--个人结算方式
                                     MDTRT_CERT_TYPE,--就诊凭证类型
                                     MED_TYPE,--医疗类别
                                     MEDFEE_SUMAMT,--医疗费总额
                                     OWNPAY_AMT,--全自费金额
                                     OVERLMT_SELFPAY,--超限价自费费用
                                     PRESELFPAY_AMT,--先行自付金额
                                     INSCP_SCP_AMT,--符合范围金额
                                     MED_SUMFEE,--医保认可费用总额
                                     ACT_PAY_DEDC,--实际支付起付线
                                     HIFP_PAY,--基本医疗保险统筹基金支出
                                     POOL_PROP_SELFPAY,--基本医疗保险统筹基金支付比例
                                     CVLSERV_PAY,--公务员医疗补助资金支出
                                     HIFES_PAY,--企业补充医疗保险基金支出
                                     HIFMI_PAY,--居民大病保险资金支出
                                     HIFOB_PAY,--职工大额医疗费用补助基金支出
                                     HIFDM_PAY,--伤残人员医疗保障基金支出
                                     MAF_PAY,--医疗救助基金支出
                                     OTH_PAY,--其他基金支出
                                     FUND_PAY_SUMAMT,--基金支付总额
                                     HOSP_PART_AMT,--医院负担金额
                                     PSN_PART_AM,--个人负担总金额
                                     ACCT_PAY,--个人账户支出
                                     CASH_PAYAMT,--现金支付金额
                                     ACCT_MULAID_PAY,--账户共济支付金额
                                     BALC,--个人账户支出后余额
                                     CLR_OPTINS,--清算经办机构
                                     CLR_WAY,--清算方式
                                     CLR_TYPE,--清算类别
                                     MEDINS_SETL_ID,--医药机构结算ID
                                     VOLA_TYPE,--违规类型
                                     VOLA_DSCR,--违规说明
                                     in_diagnose,
                                     in_diagnosename,
                                     insuplc_admdvs,
                                     invoice_no,
                                     inpatient_no,
                                     type_code,
                                     case when TYPE_CODE = '1'
                                       then (select max(o.oper_date) from fin_opb_invoiceinfo o
                                         where o.invoice_no = INVOICE_NO)
                                         else (select max(d.balance_date) from fin_ipb_balancehead d
                                         where d.invoice_no = INVOICE_NO) end
                                FROM fin_ipr_siinmaininfo   --广东省统一医保信息住院主表
                               WHERE mdtrt_id = '{0}'
                                 and setl_id = '{1}'
                                 and valid_flag = '1'";

            try
            {
                strSql = string.Format(strSql, Mdtrt_id, Setl_id);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return false;
            }
            this.ExecQuery(strSql);

            #endregion sql

            try
            {
                while (Reader.Read())
                {
                    obj.SIMainInfo.Mdtrt_id = Reader[0].ToString();//就诊ID
                    obj.SIMainInfo.Setl_id = Reader[1].ToString();//结算ID
                    obj.SIMainInfo.Psn_no = Reader[2].ToString();//人员编号
                    obj.SIMainInfo.Psn_name = Reader[3].ToString();//人员姓名
                    obj.SIMainInfo.Psn_cert_type = Reader[4].ToString();//人员证件类型
                    obj.SIMainInfo.Certno = Reader[5].ToString();//证件号码
                    obj.SIMainInfo.Gend = Reader[6].ToString();//性别
                    obj.SIMainInfo.Naty = Reader[7].ToString();//民族
                    obj.SIMainInfo.Brdy = FS.FrameWork.Function.NConvert.ToDateTime(Reader[8].ToString());//出生日期
                    obj.SIMainInfo.Age = Reader[9].ToString();//年龄
                    obj.SIMainInfo.Insutype = Reader[10].ToString();//险种类型
                    obj.SIMainInfo.Psn_type = Reader[11].ToString();//人员类别
                    obj.SIMainInfo.Cvlserv_flag = Reader[12].ToString();//公务员标志
                    obj.SIMainInfo.Setl_time = FS.FrameWork.Function.NConvert.ToDateTime(Reader[13].ToString());//结算时间
                    obj.SIMainInfo.Psn_setlway = Reader[14].ToString();//个人结算方式
                    obj.SIMainInfo.Mdtrt_cert_type = Reader[15].ToString();//就诊凭证类型
                    obj.SIMainInfo.Med_type = Reader[16].ToString();//医疗类别
                    obj.SIMainInfo.Medfee_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[17].ToString());//医疗费总额
                    obj.SIMainInfo.Ownpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[18].ToString());//全自费金额
                    obj.SIMainInfo.Overlmt_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[19].ToString());//超限价自费费用
                    obj.SIMainInfo.Preselfpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[20].ToString());//先行自付金额
                    obj.SIMainInfo.Inscp_scp_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[21].ToString());//符合范围金额
                    obj.SIMainInfo.Med_sumfee = FS.FrameWork.Function.NConvert.ToDecimal(Reader[22].ToString());//医保认可费用总额
                    obj.SIMainInfo.Act_pay_dedc = FS.FrameWork.Function.NConvert.ToDecimal(Reader[23].ToString());//实际支付起付线
                    obj.SIMainInfo.Hifp_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[24].ToString());//基本医疗保险统筹基金支出
                    obj.SIMainInfo.Pool_prop_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[25].ToString());//基本医疗保险统筹基金支付比例
                    obj.SIMainInfo.Cvlserv_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[26].ToString());//公务员医疗补助资金支出
                    obj.SIMainInfo.Hifes_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[27].ToString());//企业补充医疗保险基金支出
                    obj.SIMainInfo.Hifmi_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[28].ToString());//居民大病保险资金支出
                    obj.SIMainInfo.Hifob_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[29].ToString());//职工大额医疗费用补助基金支出
                    obj.SIMainInfo.Hifdm_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[30].ToString());//伤残人员医疗保障基金支出
                    obj.SIMainInfo.Maf_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[31].ToString());//医疗救助基金支出
                    obj.SIMainInfo.Oth_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[32].ToString());//其他基金支出
                    obj.SIMainInfo.Fund_pay_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[33].ToString());//基金支付总额
                    obj.SIMainInfo.Hosp_part_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[34].ToString());//医院负担金额
                    obj.SIMainInfo.Psn_part_am = FS.FrameWork.Function.NConvert.ToDecimal(Reader[35].ToString());//个人负担总金额
                    obj.SIMainInfo.Acct_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[36].ToString());//个人账户支出
                    obj.SIMainInfo.Cash_payamt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[37].ToString());//现金支付金额
                    obj.SIMainInfo.Acct_mulaid_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[38].ToString());//账户共济支付金额
                    obj.SIMainInfo.Balc = FS.FrameWork.Function.NConvert.ToDecimal(Reader[39].ToString());//个人账户支出后余额
                    obj.SIMainInfo.Clr_optins = Reader[40].ToString();//清算经办机构
                    obj.SIMainInfo.Clr_way = Reader[41].ToString();//清算方式
                    obj.SIMainInfo.Clr_type = Reader[42].ToString();//清算类别
                    obj.SIMainInfo.Medins_setl_id = Reader[43].ToString();//医药机构结算ID
                    obj.SIMainInfo.Vola_type = Reader[44].ToString();//违规类型
                    obj.SIMainInfo.Vola_dscr = Reader[45].ToString();//违规说明
                    obj.SIMainInfo.Disease.ID = Reader[46].ToString();//
                    obj.SIMainInfo.Disease.Name = Reader[47].ToString();//
                    obj.SIMainInfo.Insuplc_admdvs = Reader[48].ToString();//
                    obj.SIMainInfo.InvoiceNo = Reader[49].ToString();
                    obj.ID = Reader[50].ToString();
                    obj.SIMainInfo.TypeCode = Reader[51].ToString();
                    obj.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[52].ToString());
                    return true;
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
        private void SetSIInPatient(ref FS.HISFC.Models.RADT.PatientInfo obj)
        {
            obj.SIMainInfo.Mdtrt_id = Reader[0].ToString();//就诊ID
            obj.SIMainInfo.Setl_id = Reader[1].ToString();//结算ID
            obj.SIMainInfo.Psn_no = Reader[2].ToString();//人员编号
            obj.SIMainInfo.Psn_name = Reader[3].ToString();//人员姓名
            obj.SIMainInfo.Psn_cert_type = Reader[4].ToString();//人员证件类型
            obj.SIMainInfo.Certno = Reader[5].ToString();//证件号码
            obj.SIMainInfo.Gend = Reader[6].ToString();//性别
            obj.SIMainInfo.Naty = Reader[7].ToString();//民族
            obj.SIMainInfo.Brdy = FS.FrameWork.Function.NConvert.ToDateTime(Reader[8].ToString());//出生日期
            obj.SIMainInfo.Age = Reader[9].ToString();//年龄
            obj.SIMainInfo.Insutype = Reader[10].ToString();//险种类型
            obj.SIMainInfo.Psn_type = Reader[11].ToString();//人员类别
            obj.SIMainInfo.Cvlserv_flag = Reader[12].ToString();//公务员标志
            obj.SIMainInfo.Setl_time = FS.FrameWork.Function.NConvert.ToDateTime(Reader[13].ToString());//结算时间
            obj.SIMainInfo.Psn_setlway = Reader[14].ToString();//个人结算方式
            obj.SIMainInfo.Mdtrt_cert_type = Reader[15].ToString();//就诊凭证类型
            obj.SIMainInfo.Med_type = Reader[16].ToString();//医疗类别
            obj.SIMainInfo.Medfee_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[17].ToString());//医疗费总额
            obj.SIMainInfo.Ownpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[18].ToString());//全自费金额
            obj.SIMainInfo.Overlmt_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[19].ToString());//超限价自费费用
            obj.SIMainInfo.Preselfpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[20].ToString());//先行自付金额
            obj.SIMainInfo.Inscp_scp_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[21].ToString());//符合范围金额
            obj.SIMainInfo.Med_sumfee = FS.FrameWork.Function.NConvert.ToDecimal(Reader[22].ToString());//医保认可费用总额
            obj.SIMainInfo.Act_pay_dedc = FS.FrameWork.Function.NConvert.ToDecimal(Reader[23].ToString());//实际支付起付线
            obj.SIMainInfo.Hifp_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[24].ToString());//基本医疗保险统筹基金支出
            obj.SIMainInfo.Pool_prop_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[25].ToString());//基本医疗保险统筹基金支付比例
            obj.SIMainInfo.Cvlserv_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[26].ToString());//公务员医疗补助资金支出
            obj.SIMainInfo.Hifes_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[27].ToString());//企业补充医疗保险基金支出
            obj.SIMainInfo.Hifmi_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[28].ToString());//居民大病保险资金支出
            obj.SIMainInfo.Hifob_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[29].ToString());//职工大额医疗费用补助基金支出
            obj.SIMainInfo.Hifdm_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[30].ToString());//伤残人员医疗保障基金支出
            obj.SIMainInfo.Maf_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[31].ToString());//医疗救助基金支出
            obj.SIMainInfo.Oth_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[32].ToString());//其他基金支出
            obj.SIMainInfo.Fund_pay_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[33].ToString());//基金支付总额
            obj.SIMainInfo.Hosp_part_amt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[34].ToString());//医院负担金额
            obj.SIMainInfo.Psn_part_am = FS.FrameWork.Function.NConvert.ToDecimal(Reader[35].ToString());//个人负担总金额
            obj.SIMainInfo.Acct_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[36].ToString());//个人账户支出
            obj.SIMainInfo.Cash_payamt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[37].ToString());//现金支付金额
            obj.SIMainInfo.Acct_mulaid_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[38].ToString());//账户共济支付金额
            obj.SIMainInfo.Balc = FS.FrameWork.Function.NConvert.ToDecimal(Reader[39].ToString());//个人账户支出后余额
            obj.SIMainInfo.Clr_optins = Reader[40].ToString();//清算经办机构
            obj.SIMainInfo.Clr_way = Reader[41].ToString();//清算方式
            obj.SIMainInfo.Clr_type = Reader[42].ToString();//清算类别
            obj.SIMainInfo.Medins_setl_id = Reader[43].ToString();//医药机构结算ID
            obj.SIMainInfo.Vola_type = Reader[44].ToString();//违规类型
            obj.SIMainInfo.Vola_dscr = Reader[45].ToString();//违规说明
            obj.SIMainInfo.Disease.ID = Reader[46].ToString();//
            obj.SIMainInfo.Disease.Name = Reader[47].ToString();//
            obj.SIMainInfo.Insuplc_admdvs = Reader[48].ToString();//
            obj.SIMainInfo.IsBalanced = FS.FrameWork.Function.NConvert.ToBoolean(Reader[49].ToString());
        }
        #endregion
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
                                 and type_code = '2' 
                                 ";

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

        /// <summary>
        /// 得到住院医保患者基本信息;
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo GetSIPersonInfo(string inpatientNo, FS.HISFC.Models.RADT.PatientInfo obj , string type_code)
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
                                 ";

            try
            {
                strSql = string.Format(strSql, inpatientNo, type_code);
                if (!string.IsNullOrEmpty(type_code)) {
                    strSql += string.Format("and type_code = '{0}' ", type_code);
                }
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

        /// <summary>
        /// 插入住院登记信息
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int InsertInPatientReg(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            //医保基本信息保存到表FIN_IPR_SIINMAININFO_NEW
            int nextBalanceNo = this.GetNextBalanceNo(patientInfo.ID,"2");
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
                FS.HISFC.Models.RADT.Diagnose diagInfo = new FS.HISFC.Models.RADT.Diagnose();
                if (patientInfo.Diagnoses != null && patientInfo.Diagnoses.Count > 0) {
                    diagInfo = patientInfo.Diagnoses[0] as FS.HISFC.Models.RADT.Diagnose;
                }

                strSql = string.Format(strSql,
                    patientInfo.ID,//住院流水号/门诊流水号0
                    patientInfo.SIMainInfo.Mdtrt_id,//就医登记号1
                    patientInfo.SIMainInfo.BalNo, //结算序号2
                    patientInfo.PID.PatientNO, //住院号3
                    patientInfo.PID.CardNO,//卡号4
                    patientInfo.SIMainInfo.Psn_no,//医疗证号5
                    patientInfo.Name,//姓名6
                    patientInfo.Sex.ID.ToString(),//性别7
                    patientInfo.IDCard, //身份证8
                    patientInfo.SIMainInfo.Brdy.ToString("yyyy-MM-dd HH:mm:ss"), //生日9
                    patientInfo.ClinicDiagnose, //门诊诊断10
                    patientInfo.PVisit.InTime.ToString("yyyy-MM-dd HH:mm:ss"),//入院日期11
                    this.GetDateTimeFromSysDateTime().ToString(), //诊断时间12
                    diagInfo.ID, //诊断编码13
                    diagInfo.Name, //诊断名称14
                    patientInfo.PVisit.PatientLocation.Dept.ID,//科室代码15
                    patientInfo.PVisit.PatientLocation.Dept.Name,//科室名称16
                    patientInfo.Pact.PayKind.ID,//结算类别 1-自费  2-保险 3-公费在职 4-公费退休 5-公费高干17
                    patientInfo.Pact.ID,//合同代码18
                    patientInfo.Pact.Name,//合同单位名称19
                    patientInfo.SIMainInfo.Mdtrt_id,//就诊ID20
                    patientInfo.SIMainInfo.Psn_no,//人员编号21
                    patientInfo.SIMainInfo.Psn_name, //人员姓名22
                    patientInfo.SIMainInfo.Psn_cert_type, //人员证件类型23
                    patientInfo.SIMainInfo.Certno, //证件号码24
                    patientInfo.SIMainInfo.Gend, //性别25
                    patientInfo.SIMainInfo.Brdy.ToString("yyyy-MM-dd HH:mm:ss"), //生日26
                    patientInfo.SIMainInfo.Insutype, //险种类型27
                    patientInfo.SIMainInfo.Psn_type, //人员类别28
                    patientInfo.SIMainInfo.Mdtrt_cert_type, //就诊凭证类型29
                    patientInfo.SIMainInfo.Med_type, //医疗类别30
                    patientInfo.SIMainInfo.Dise_code, //病种编码31
                    patientInfo.SIMainInfo.Dise_name, //病种名称32
                    patientInfo.SIMainInfo.Vola_type,//违规类型33
                    patientInfo.SIMainInfo.Vola_dscr,//违规说明34
                    this.Operator.ID,//操作员35
                    this.GetDateTimeFromSysDateTime().ToString()//操作日期36
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
                    string strSQL = string.Format(sql,patientInfo.SIMainInfo.Mdtrt_id,
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
                    f.RecipeNO + f.SequenceNO.ToString().PadLeft(3,'0'),
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
                                           '{4}',
                                           '{5}',
                                           '{6}',
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

        #region 住院诊断// {1624908D-0C01-4E1C-A7FB-445DE755E85E}
        /// <summary>
        /// 获取住院出院诊断信息
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public ArrayList InpatientClinicDiagnoseList(string inpatientNo)
        {
            ArrayList obj = new ArrayList();
            string strSql = @" select t.ext_flag2,
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
        /// 获取住院病案首页诊断信息
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public ArrayList InpatientBaseDiagnoseList(string inpatientNo)
        {
            ArrayList obj = new ArrayList();
            string strSql = @"select 
            t3.FICDM diag_code, --诊断代码
            t3.FJBNAME diag_name ,--诊断名称
            (case when (select a.user_code from met_com_icdcompare_yb a
                 where a.icd10_code = t3.FICDM and a.kind_code = 'ZD')=1 then '0'--灰码不能作为主诊断
            when t3.FZDLX ='1' then '1'
            when (select (select a.user_code from met_com_icdcompare_yb a
                       where a.icd10_code = t.FICDM and a.kind_code = 'ZD')
                from tDiagnose@proemr_dblink t
                where t.FPRN = t3.FPRN and t.ftimes = t3.ftimes
                      and t.FPX = '1' and t.fzdlx = '1')=1 and t3.FPX = '2' then '1'
            else '0' end) maindiag_flag  --是否主要诊断
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
            order by t3.FZDLX";//获取病案首页的诊断// {3DC7C1C0-A521-4841-A329-1A5B5BBD6A8B}
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
                    diag.IsMain = Reader[2].ToString() == "1" ? true : false;//主诊断标志
                    diag.Type.ID = "2";//出人院诊断类型
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
        public ArrayList InpatientEMRDiagnoseList(string inpatientNo ,string diagType)
        {
            ArrayList obj = new ArrayList();
            string strSql = @"select  d.diag_code,
                                                    d.diag_name，
                                                    d.MAINDIAG_FLAG,
                                                    d.inout_diag_type
                                         from view_emr_disease d 
                                        where d.inpatient_no = '{0}'";

            strSql = string.Format(strSql, inpatientNo);
            if (!string.IsNullOrEmpty(diagType)) {
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
                    diag.IsMain = Reader[2].ToString() == "1"?true:false;//主诊断标志
                    diag.Type.ID = Reader[3].ToString() ;//出入院诊断标志
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

        /// <summary>
        /// 更新医保结算主表信息(取消入院登记)
        /// </summary>
        /// <returns></returns>
        public int UpdateSiMainInfoValidFlag(string inpatientNo, string balanceNo)
        {
            string strSql = @"update fin_ipr_siinmaininfo f 
                                 set f.valid_flag = '0',
                                     f.oper_code = '{1}',
                                     f.oper_date = sysdate,
                                     f.balance_state='0'
                               where f.inpatient_no = '{0}'
                               and f.mdtrt_id = '{2}'";

            try
            {
                strSql = string.Format(strSql, inpatientNo, this.Operator.ID, balanceNo);
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
                                     --t1.invoice_no, -- 4 主发票号
                                     nvl(t1.invoice_no_ps,(select e.serialno from com_opb_eleinvoiceopeninfo   e 
                                                                        where e.status = '2' 
                                                                        and (e.inpatientnoorinvoiceno = t1.invoice_no or e.inpatientnoorinvoiceno = t1.inpatient_no ) 
                                                                        and rownum =1)) invoice_no, --票据号码 
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
                               where (t1.patient_no = '{0}' or 'ALL' = '{0}')
                                 and (t1.name like '%{1}%' or 'ALL' = '{1}')
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
                                    t.resp_nurs_code,
                                    t.stas_type,
                                    t.mul_nwb_bir_wt,
                                    t.mul_nwb_adm_wt
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
        public int GetJsqdbldInfo(string mdtrt, ref System.Data.DataTable dt)
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

        #region 对账 // {ACB49EE2-E85B-4AA0-96CB-2EE7912AA5E8}
        #region 获取总账对账数据
        /// <summary>
        /// 获取总账对账数据
        /// </summary>
        /// <param name="insutype">险种</param>
        /// <param name="clr_type">清算类别</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public int getTotBalanceInfo(string IfSY, string insutype, string clr_type, string beginTime, string endTime, ref FS.HISFC.Models.RADT.PatientInfo objInfo)
        {
            // {40666427-FACA-496e-A861-3D33305C4114}
            string strSql = @"select decode(tt.ifsy,'1','440699','440606') clr,
                                               sum(tt.medfee_sumamt),
                                               sum(tt.fund_pay_sumamt),
                                               0,
                                               count(1) 
                                            from (
                                                  select 
                                                   t.medfee_sumamt ,
                                                   t.fund_pay_sumamt,
                                                   t.acct_pay,
                                                   (case when t.clr_type in ('9902','9903') then '0' else decode(t.med_type,'51','1','52','1','0') end) ifsy
                                                  from fin_ipr_siinmaininfo t
                                                  where t.valid_flag = '1'
                                                    and  t.MDTRT_ID is not null
                                                    and t.setl_id is not null
                                                    and t.balance_state = '1'
                                                    and t.paykind_code = '02'
                                                    and t.insutype = '{0}'
                                                    and t.clr_type = '{1}'
                                                    --and decode(t.med_type,'51','1','52','1','0')  = '{4}'
                                                    and (decode(t.med_type,'51','1','52','1','0')  = '{4}' or  t.clr_type in ('9902','9903'))
                                                        --住院
                                                    and (((t.type_code = '2' or t.own_cause = '对账补充')  and t.balance_date between
                                                        to_date('{2}', 'yyyy-mm-dd HH24:mi:ss') and
                                                        to_date('{3}', 'yyyy-mm-dd HH24:mi:ss')   

                                                        )
                                                        --门诊
                                                        or
                                                        (t.type_code = '1' and (t.own_cause != '对账补充' or t.own_cause is null) and t.oper_date between
                                                        to_date('{2}', 'yyyy-mm-dd HH24:mi:ss') and
                                                        to_date('{3}', 'yyyy-mm-dd HH24:mi:ss'))

                                                    or
                                                        (t.setl_time between
                                                        to_date('{2}', 'yyyy-mm-dd HH24:mi:ss') and
                                                        to_date('{3}', 'yyyy-mm-dd HH24:mi:ss'))
                                                        )  
                                             
                                                   union all

                                                  select
                                                   b.medfee_sumamt,
                                                   b.fund_pay_sumamt,
                                                   b.acct_pay,
                                                   '0' ifsy
                                                  from fin_syb_hsbalanceinfo b 
                                                  where  b.valid_flag = '1'
                                                   and b.MDTRT_ID is not null
                                                   and b.setl_id is not null
                                                   and b.insutype = '{0}'
                                                   and b.clr_type = '{1}'
                                                   and '0' = '{4}'
                                                   and b.setl_time between
                                                   to_date('{2}', 'yyyy-mm-dd HH24:mi:ss') and
                                                   to_date('{3}', 'yyyy-mm-dd HH24:mi:ss')
                                              ) tt
                                              group by tt.ifsy
                            ";
            strSql = string.Format(strSql, insutype, clr_type, beginTime, endTime,IfSY);

            try
            {
                this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    objInfo.SIMainInfo.Clr_optins = Reader[0].ToString();
                    objInfo.SIMainInfo.Medfee_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[1].ToString());
                    objInfo.SIMainInfo.Fund_pay_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(Reader[2].ToString());
                    objInfo.SIMainInfo.Acct_pay = FS.FrameWork.Function.NConvert.ToDecimal(Reader[3].ToString());
                    objInfo.SIMainInfo.Memo = Reader[4].ToString();
                }
                return 1;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }
        }
        #endregion

        #region 获取总账对账明细数据
        /// <summary>
        /// 获取本地总账对账明细数据
        /// </summary>
        /// <param name="insutype">险种</param>
        /// <param name="clr_type">清算类别</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="BalanceDetails">结算明细集合</param>
        /// <returns></returns>
        public int getBalanceDetail(string IfSY, string insutype, string clr_type, string beginTime, string endTime, ref ArrayList BalanceDetails)
        {
            BalanceDetails.Clear();
            // {40666427-FACA-496e-A861-3D33305C4114}
            string strSql = @"
                                  SELECT t.card_no 门诊号,--门诊号  0
                                             t.patient_no 住院号,--住院号 1
                                             t.name 姓名,--姓名 2
                                             t.MDTRT_ID 就诊ID, --就诊ID 3
                                             t.setl_id 结算ID, --结算ID 4
                                             t.PSN_NO 人员编号, --人员编号 5
                                             (select aa.name from com_dictionary aa where aa.type = 'GZSI_insutype' and aa.code = t.insutype) 险种,--险种 6
                                             (select aa.name from com_dictionary aa where aa.type = 'GZSI_med_type' and aa.code = t.med_type)  医疗类别, --医疗类别 7
                                             (select aa.name from com_dictionary aa where aa.type = 'GZSI_clr_type' and aa.code = t.clr_type) 清算类别, --8
                                             t.certno 证件号码, --证件号码 9
                                             t.dept_name 入院科室,--入院科室 10
                                             decode(t.type_code ,'1',t.oper_date, t.in_date) 入院日期,--入院日期 11
                                             decode(t.type_code ,'1',t.oper_date,t.out_date) 出院日期,--出院日期 12
                                             decode(t.type_code ,'1',t.oper_date,t.balance_date) 结算时间,--结算时间 13
                                             t.medfee_sumamt 总金额,--总金额 14
                                             t.fund_pay_sumamt  基金支付,--基金支付 15
                                             0 个人账户支付,--个人支付 16
                                             --对账结果
                                             t.stmt_rslt 对账结果 , --17
                                             t.setl_optins 结算经办机构 , --18
                                             t.stmt_rslt_dscr 对账结果说明 , --19
                                             t.stmt_oper 对账操作人 , --20
                                             t.stmt_time 对账时间 , --21
                                             t.stmt_state 对账状态 --22
                                        FROM fin_ipr_siinmaininfo t 
                                        where t.valid_flag = '1'
                                         and  t.MDTRT_ID is not null
                                         and t.setl_id is not null
                                         and t.balance_state = '1'
                                         and t.paykind_code = '02'
                                         and t.insutype = '{0}'
                                         and t.clr_type = '{1}'
                                         --and decode(t.med_type,'51','1','52','1','0')  = '{4}'
                                          and (decode(t.med_type,'51','1','52','1','0')  = '{4}' or  t.clr_type in ('9902','9903'))
                                             --住院
                                         and (((t.type_code = '2' or t.own_cause = '对账补充')  and t.balance_date between
                                             to_date('{2}', 'yyyy-mm-dd HH24:mi:ss') and
                                             to_date('{3}', 'yyyy-mm-dd HH24:mi:ss')   

                                             )
                                             --门诊
                                             or
                                             (t.type_code = '1' and (t.own_cause != '对账补充' or t.own_cause is null) and t.oper_date between
                                             to_date('{2}', 'yyyy-mm-dd HH24:mi:ss') and
                                             to_date('{3}', 'yyyy-mm-dd HH24:mi:ss'))

                                            or
                                             (t.setl_time between
                                             to_date('{2}', 'yyyy-mm-dd HH24:mi:ss') and
                                             to_date('{3}', 'yyyy-mm-dd HH24:mi:ss'))
                                             )     --
                                        union all                      
                                          select h.card_no 门诊号,--门诊号  0
                                                 '' 住院号,--住院号 1
                                                 b.psn_name 姓名,--姓名 2
                                                 b.MDTRT_ID 就诊ID, --就诊ID 3
                                                 b.setl_id 结算ID, --结算ID 4
                                                 b.PSN_NO 人员编号, --人员编号 5
                                                 (select aa.name from com_dictionary aa where aa.type = 'GZSI_insutype' and aa.code = b.insutype) 险种,--险种 6
                                                 (select aa.name from com_dictionary aa where aa.type = 'GZSI_med_type' and aa.code = b.med_type) || '核酸'  医疗类别, --医疗类别 7
                                                 (select aa.name from com_dictionary aa where aa.type = 'GZSI_clr_type' and aa.code = b.clr_type) 清算类别, --8
                                                 b.certno 证件号码, --证件号码 9
                                                 '' 入院科室,--入院科室 10
                                                 h.see_date 入院日期,--入院日期 11
                                                 h.see_date 出院日期,--出院日期 12
                                                 b.setl_time 结算时间,--结算时间 13
                                                 b.medfee_sumamt 总金额,--总金额 14
                                                 b.fund_pay_sumamt  基金支付,--基金支付 15
                                                 0 个人账户支付,--个人支付 16
                                                 --对账结果
                                                 b.stmt_rslt 对账结果 , --17
                                                 b.setl_optins 结算经办机构 , --18
                                                 b.stmt_rslt_dscr 对账结果说明 , --19
                                                 b.stmt_oper 对账操作人 , --20
                                                 b.stmt_time 对账时间 , --21
                                                 b.stmt_state 对账状态 --22
                                          from fin_syb_hsbalanceinfo b ,fin_syb_hsbaseinfo h
                                          where b.id = h.id 
                                               and b.valid_flag = '1'
                                               and b.MDTRT_ID is not null
                                               and b.setl_id is not null
                                               and b.insutype = '{0}'
                                               and b.clr_type = '{1}'
                                               and '0' = '{4}'
                                               and b.setl_time between
                                               to_date('{2}', 'yyyy-mm-dd HH24:mi:ss') and
                                               to_date('{3}', 'yyyy-mm-dd HH24:mi:ss')
                                ";
            strSql = string.Format(strSql, insutype, clr_type, beginTime, endTime,IfSY);

            try
            {
                this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.RADT.PatientInfo objInfo = new FS.HISFC.Models.RADT.PatientInfo();
                    objInfo.PID.CardNO = Reader[0].ToString();
                    objInfo.PID.PatientNO = Reader[1].ToString();
                    objInfo.Name = Reader[2].ToString();
                    objInfo.SIMainInfo.Mdtrt_id = Reader[3].ToString();
                    objInfo.SIMainInfo.Setl_id = Reader[4].ToString();
                    objInfo.SIMainInfo.Psn_no = Reader[5].ToString();
                    objInfo.SIMainInfo.Insutype = Reader[6].ToString();
                    objInfo.SIMainInfo.Med_type = Reader[7].ToString();
                    objInfo.SIMainInfo.Clr_type = Reader[8].ToString();
                    objInfo.SIMainInfo.Certno = Reader[9].ToString();
                    objInfo.PVisit.PatientLocation.Dept.Name = Reader[10].ToString();
                    objInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[11].ToString());
                    objInfo.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[12].ToString());
                    objInfo.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[13].ToString());
                    objInfo.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[14].ToString());
                    objInfo.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[15].ToString());
                    objInfo.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[16].ToString());
                    objInfo.SIMainInfo.Stmt_relt = Reader[17].ToString();
                    objInfo.SIMainInfo.Setl_options = Reader[18].ToString();
                    objInfo.SIMainInfo.Stmt_rslt_dscr = Reader[19].ToString();
                    objInfo.SIMainInfo.OperInfo.ID = Reader[20].ToString();
                    objInfo.SIMainInfo.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[21].ToString());
                    objInfo.SIMainInfo.Stmt_state = Reader[22].ToString();
                    BalanceDetails.Add(objInfo);
                }
                return 1;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// 保存医保中心对账明细数据
        /// </summary>
        /// <param name="insutype">险种</param>
        /// <param name="clr_type">清算类别</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public int updateStmtInfo(string IfSY,string insutype, string clr_type, string beginTime, string endTime, string setl_optins, string stmt_rslt, string stmt_rslt_dscr, string stmt_state)
        {
            string strSql = @" update fin_ipr_siinmaininfo s
                                        set s.setl_optins = '{4}', 
                                            s.stmt_rslt = '{5}', 
                                            s.stmt_rslt_dscr = '{6}',
                                            s.stmt_state = '{7}',
                                            s.stmt_oper = '{8}',
                                            s.stmt_time =  sysdate
                                         where s.setl_id in (SELECT  t.setl_id 结算ID--结算ID 4
                                                          FROM fin_ipr_siinmaininfo t 
                                                         where t.valid_flag = '1'
                                                           and  t.MDTRT_ID is not null
                                                           and t.setl_id is not null
                                                           and t.balance_state = '1'
                                                           and t.paykind_code = '02'
                                                           and t.insutype = '{0}'
                                                           and t.clr_type = '{1}'
                                                           --and decode(t.med_type,'51','1','52','1','0')  = '{9}'
                                                           and (decode(t.med_type,'51','1','52','1','0')  = '{9}' or  t.clr_type in ('9902','9903'))
                                                               --住院
                                                           and ((t.type_code = '2' and t.balance_date between
                                                               to_date('{2}', 'yyyy-mm-dd HH24:mi:ss') and
                                                               to_date('{3}', 'yyyy-mm-dd HH24:mi:ss')   
                                                               )
                                                               --门诊
                                                               or
                                                               (t.type_code = '1' and t.oper_date between
                                                               to_date('{2}', 'yyyy-mm-dd HH24:mi:ss') and
                                                               to_date('{3}', 'yyyy-mm-dd HH24:mi:ss')
                                                               )))
                                ";
            try
            {
                strSql = string.Format(strSql, insutype,
                                                             clr_type,
                                                             beginTime,
                                                             endTime,
                                                             setl_optins,
                                                             stmt_rslt,
                                                             stmt_rslt_dscr,
                                                             stmt_state,
                                                             this.Operator.ID,
                                                             IfSY
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
        /// 保存医保中心对账明细数据--核酸
        /// </summary>
        /// <param name="insutype">险种</param>
        /// <param name="clr_type">清算类别</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public int updateStmtInfoHS(string insutype, string clr_type, string beginTime, string endTime, string setl_optins, string stmt_rslt, string stmt_rslt_dscr, string stmt_state)
        {
            string strSql = @" update fin_syb_hsbalanceinfo s
                                        set s.setl_optins = '{4}', 
                                            s.stmt_rslt = '{5}', 
                                            s.stmt_rslt_dscr = '{6}',
                                            s.stmt_state = '{7}',
                                            s.stmt_oper = '{8}',
                                            s.stmt_time =  sysdate
                                         where s.setl_id in (SELECT  b.setl_id 结算ID--结算ID 4
                                                          from fin_syb_hsbalanceinfo b ,fin_syb_hsbaseinfo h
                                                          where b.id = h.id 
                                                               and b.valid_flag = '1'
                                                               and b.MDTRT_ID is not null
                                                               and b.setl_id is not null
                                                               and b.insutype = '{0}'
                                                               and b.clr_type = '{1}'
                                                               and b.setl_time between
                                                               to_date('{2}', 'yyyy-mm-dd HH24:mi:ss') and
                                                               to_date('{3}', 'yyyy-mm-dd HH24:mi:ss')）
                                ";
            try
            {
                strSql = string.Format(strSql, insutype,
                                                             clr_type,
                                                             beginTime,
                                                             endTime,
                                                             setl_optins,
                                                             stmt_rslt,
                                                             stmt_rslt_dscr,
                                                             stmt_state,
                                                             this.Operator.ID
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

        #endregion

        #region 查询本地是否有相关记录
        public string QuerySIInfo(string mdtrt_id, string setl_id)
        {
            string strSql = @"select 1 from fin_ipr_siinmaininfo s 
                                        where 1=1
                                        and s.mdtrt_id = '{0}' 
                                        or s.setl_id  = '{1}'
                                        union all 
                                        select 1 from fin_syb_hsbalanceinfo b
                                        where 1=1
                                        and b.mdtrt_id = '{0}' 
                                        or b.setl_id  = '{1}'";
            strSql = string.Format(strSql, mdtrt_id, setl_id);
            return this.ExecSqlReturnOne(strSql);
        }

        #endregion

        #region 90502
        //结算明细
        public string ExisteLogarithmSetlinfo(string mdtrt_id, string setl_id)
        {
            string strSql = @"select 1 from GDSI_DZ_SETLINFO s 
                                        where 1=1
                                        and s.mdtrt_id = '{0}' 
                                        and s.setl_id  = '{1}'";
            strSql = string.Format(strSql, mdtrt_id, setl_id);
            return this.ExecSqlReturnOne(strSql);
        }

        public int insertLogarithmSetlinfo(Models.Response.ResponseGzsiModel90502.Output.Setlinfo setlInfo)
        {
            try
            {
                string strSql = @"insert into GDSI_DZ_SETLINFO
                                            (SEQNO	,--	顺序号	0
                                            SETL_ID	,--	结算ID	1
                                            MDTRT_ID	,--	就诊ID	2
                                            PSN_NO	,--	人员编号	3
                                            PSN_NAME	,--	人员姓名	4
                                            PSN_CERT_TYPE	,--	人员证件类型	5
                                            CERTNO	,--	证件号码	6
                                            GEND	,--	性别	7
                                            NATY	,--	民族	8
                                            BRDY	,--	出生日期	9
                                            AGE	,--	年龄	10
                                            INSUTYPE	,--	险种类型	11
                                            PSN_TYPE	,--	人员类别	12
                                            CVLSERV_FLAG	,--	公务员标志	13
                                            FLXEMPE_FLAG	,--	灵活就业标志	14
                                            NWB_FLAG	,--	新生儿标志	15
                                            INSU_OPTINS	,--	参保机构医保区划	16
                                            EMP_NAME	,--	单位名称	17
                                            FIXMEDINS_CODE	,--	定点医药机构编号	18
                                            FIXMEDINS_NAME	,--	定点医药机构名称	19
                                            HOSP_LV	,--	医院等级	20
                                            FIX_BLNG_ADMDVS	,--	定点归属机构	21
                                            BEGNDATE	,--	开始日期	22
                                            ENDDATE	,--	结束日期	23
                                            SETL_TIME	,--	结算时间	24
                                            MED_TYPE	,--	医疗类别	25
                                            CLR_TYPE	,--	清算类别	26
                                            CLR_WAY	,--	清算方式	27
                                            CLR_OPTINS	,--	清算经办机构	28
                                            CLR_TYPE_LV2	,--	二级清算类别	29
                                            MEDFEE_SUMAMT	,--	医疗费总额	30
                                            FULAMT_OWNPAY_AMT	,--	全自费金额	31
                                            OVERLMT_SELFPAY	,--	超限价自费费用	32
                                            PRESELFPAY_AMT	,--	先行自付金额	33
                                            INSCP_AMT	,--	符合政策范围金额	34
                                            ACT_PAY_DEDC	,--	实际支付起付线	35
                                            FUND_PAY_SUMAMT	,--	基金支付总额	36
                                            PSN_PAY	,--	个人支付金额	37
                                            ACCT_PAY	,--	个人账户支出	38
                                            CASH_PAYAMT	,--	现金支付金额	39
                                            BALC	,--	余额	40
                                            ACCT_MULAID_PAY	,--	个人账户共济支付金额	41
                                            YEAR	,--	年度	42
                                            DISE_NO	,--	病种编码	43
                                            DISE_NAME	,--	病种名称	44
                                            MEDINS_STMT_FLAG	,--	医疗机构对账标志	45
                                            REFD_SETL_FLAG	,--	退费结算标志	46
                                            CLR_FLAG	,--	清算标志	47
                                            IDE_ADMDVS	,--	身份认定行政区划	48
                                            PAY_LOC	,--	支付地点类别 1零报数据	49
                                            INVONO,	--	发票号	50
                                            hifpPay	,--	统筹基金支出 51
                                            cvlservPay	,--	公务员医疗补助资金支出 52
                                            hifesPay	,--	补充医疗保险基金支出 53
                                            hifmiPay	,--	大病补充医疗保险基金支出 54
                                            hifobPay	,--	大额医疗补助基金支出 55
                                            hifdmPay	,--	伤残人员医疗保障基金支出 56
                                            mafPay	,--	医疗救助基金支出 57
                                            othfundPay,	--	其它基金支付 58
                                            matnEnddate, -- 生育备案结束时间 
                                            birHospChangFlag,  --产检医院变更标志	 
                                            birctrl_type --生育类型
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
                                                to_date('{22}','yyyy-MM-dd hh24:mi:ss'),
                                                to_date('{23}','yyyy-MM-dd hh24:mi:ss'),
                                                to_date('{24}','yyyy-MM-dd hh24:mi:ss'),
                                                '{25}',
                                                '{26}',
                                                '{27}',
                                                '{28}',
                                                '{29}',
                                                '{30}',
                                                '{31}',
                                                '{32}',
                                                '{33}',
                                                '{34}',
                                                '{35}',
                                                '{36}',
                                                '{37}',
                                                '{38}',
                                                '{39}',
                                                '{40}',
                                                '{41}',
                                                '{42}',
                                                '{43}',
                                                '{44}',
                                                '{45}',
                                                '{46}',
                                                '{47}',
                                                '{48}',
                                                '{49}',
                                                '{50}',
                                                '{51}',
                                                '{52}',
                                                '{53}',
                                                '{54}',
                                                '{55}',
                                                '{56}',
                                                '{57}',
                                                '{58}',
                                                to_date('{59}','yyyy-MM-dd hh24:mi:ss'),
                                                '{60}','{61}'
                                                )";
                strSql = string.Format(strSql,
                   setlInfo.seqno,//顺序号0
                    setlInfo.setl_id,//结算ID1
                    setlInfo.mdtrt_id,//就诊ID2
                    setlInfo.psn_no,//人员编号3
                    setlInfo.psn_name,//人员姓名4
                    setlInfo.psn_cert_type,//人员证件类型5
                    setlInfo.certno,//证件号码6
                    setlInfo.gend,//性别7
                    setlInfo.naty,//民族8
                    setlInfo.brdy,//出生日期9
                    setlInfo.age,//年龄10
                    setlInfo.insutype,//险种类型11
                    setlInfo.psn_type,//人员类别12
                    setlInfo.cvlserv_flag,//公务员标志13
                    setlInfo.flxempe_flag,//灵活就业标志14
                    setlInfo.nwb_flag,//新生儿标志15
                    setlInfo.insu_optins,//参保机构医保区划16
                    setlInfo.emp_name,//单位名称17
                    setlInfo.fixmedins_code,//定点医药机构编号18
                    setlInfo.fixmedins_name,//定点医药机构名称19
                    setlInfo.hosp_lv,//医院等级20
                    setlInfo.fix_blng_admdvs,//定点归属机构21
                    setlInfo.begndate,//开始日期22
                    setlInfo.enddate,//结束日期23
                    setlInfo.setl_time,//结算时间24
                    setlInfo.med_type,//医疗类别25
                    setlInfo.clr_type,//清算类别26
                    setlInfo.clr_way,//清算方式27
                    setlInfo.clr_optins,//清算经办机构28
                    setlInfo.clr_type_lv2,//二级清算类别29
                    setlInfo.medfee_sumamt,//医疗费总额30
                    setlInfo.fulamt_ownpay_amt,//全自费金额31
                    setlInfo.overlmt_selfpay,//超限价自费费用32
                    setlInfo.preselfpay_amt,//先行自付金额33
                    setlInfo.inscp_amt,//符合政策范围金额34
                    setlInfo.act_pay_dedc,//实际支付起付线35
                    setlInfo.fund_pay_sumamt,//基金支付总额36
                    setlInfo.psn_pay,//个人支付金额37
                    setlInfo.acct_pay,//个人账户支出38
                    setlInfo.cash_payamt,//现金支付金额39
                    setlInfo.balc,//余额40
                    setlInfo.acct_mulaid_pay,//个人账户共济支付金额41
                    setlInfo.year,//年度42
                    setlInfo.dise_no,//病种编码43
                    setlInfo.dise_name,//病种名称44
                    setlInfo.medins_stmt_flag,//医疗机构对账标志45
                    setlInfo.refd_setl_flag,//退费结算标志46
                    setlInfo.clr_flag,//清算标志47
                    setlInfo.ide_admdvs,//身份认定行政区划48
                    setlInfo.pay_loc,//支付地点类别49
                    setlInfo.invono,//发票号50
                    setlInfo.hifp_pay,//	统筹基金支出
                    setlInfo.cvlserv_pay,//	公务员医疗补助资金支出
                    setlInfo.hifes_pay,//	补充医疗保险基金支出
                    setlInfo.hifmi_pay,//	大病补充医疗保险基金支出
                    setlInfo.hifob_pay,//	大额医疗补助基金支出
                    setlInfo.hifdm_pay,//	伤残人员医疗保障基金支出
                    setlInfo.maf_pay,//	医疗救助基金支出
                    setlInfo.othfund_pay,//	其它基金支付
                    setlInfo.matnEnddate,  //  生育备案结束时间 
                    setlInfo.birHospChangFlag,  //产检医院变更标志	     
                    setlInfo.birctrl_type//生育类型
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

        //基金支付
        public string ExisteLogarithmSetldetail(string mdtrt_id, string setl_id, string fund_pay_type)
        {
            string strSql = @"select 1 from GDSI_DZ_SETLDETAIL s 
                                        where 1=1
                                        and s.mdtrt_id = '{0}' 
                                        and s.setl_id  = '{1}'
                                        and s.FUND_PAY_TYPE = '{2}'";
            strSql = string.Format(strSql, mdtrt_id, setl_id, fund_pay_type);
            return this.ExecSqlReturnOne(strSql);
        }

        public int insertLogarithmSetldetail(Models.Response.ResponseGzsiModel90502.Output.Setldetail sdetail)
        {
            try
            {
                string strSql = @"insert into GDSI_DZ_SETLDETAIL
                                            ( SETL_ID	,--	结算ID	0
                                                MDTRT_ID	,--	就诊ID	1
                                                FUND_PAY_TYPE	,--	基金支付类型	2
                                                POOLAREA_FUND_PAY_TYPE	,--	统筹区基金支付类型	3
                                                FUND_PAYAMT--	基金支付金额	4
                                            )
                                            values
                                            ('{0}',
                                                '{1}',
                                                '{2}',
                                                '{3}',
                                                '{4}'
                                                )";
                strSql = string.Format(strSql,
                    sdetail.setl_id,
                    sdetail.mdtrt_id,
                    sdetail.fund_pay_type,
                    sdetail.poolarea_fund_pay_type,
                    sdetail.fund_payamt
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

        //清空明细、基金支付
        public int deleteLogarithm()
        {
            try
            {
                string strSql = @"delete from GDSI_DZ_SETLINFO";
                this.ExecNoQuery(strSql);

                strSql = @"delete from GDSI_DZ_SETLDETAIL";
                this.ExecNoQuery(strSql);

                return 1;

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        public int deleteLogarithm(string mdtrt_id, string selt_id)
        {
            try
            {
                string strSql = @"delete from GDSI_DZ_SETLINFO  s
                                        where 1=1
                                        and s.mdtrt_id = '{0}' 
                                        and s.setl_id  = '{1}'";
                this.ExecNoQuery(strSql, mdtrt_id, selt_id);

                strSql = @"delete from GDSI_DZ_SETLDETAIL s
                                        where 1=1
                                        and s.mdtrt_id = '{0}' 
                                        and s.setl_id  = '{1}'";
                this.ExecNoQuery(strSql, mdtrt_id, selt_id);

                return 1;

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        public System.Data.DataTable getLogarithmInfo(string insutype, string clr_type, string clr_type_lv2, string begintime, string endtime)
        {
            string sql = getLogarithmSQL(insutype, clr_type, clr_type_lv2);
            if (string.IsNullOrEmpty(sql)) return null;

            sql = string.Format(sql, begintime, endtime);
            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sql, ref ds);
            return ds.Tables[0];
        }

        public string getLogarithmSQL(string insutype, string clr_type, string clr_type_lv2)
        {
            if (insutype == "390" && clr_type == "11" && clr_type_lv2 != "XG")
            {
                return this.sql_390_11;
            }
            else if (insutype == "310" && clr_type == "11" && clr_type_lv2 != "XG")
            {
                return this.sql_310_11;
            }
            else if (insutype == "390" && clr_type == "21")
            {
                return this.sql_390_21_DRG;
            }
            else if (insutype == "310" && clr_type == "21")
            {
                return this.sql_310_21_DRG;
            }
            else if (insutype == "310" && clr_type == "11" && clr_type_lv2 == "XG")
            {
                return this.sql_310_11_XG;
            }
            else if (insutype == "390" && clr_type == "11" && clr_type_lv2 == "XG")
            {
                return this.sql_390_11_XG;
            }
            else if (insutype == "310" && clr_type_lv2 == "SY")
            {
                return this.sql_310_SY;
            }
            else if (insutype == "310" && clr_type_lv2 == "SYMZ")
            {
                return this.sql_310_SYMZ;
            }
            else if (insutype == "310" && clr_type_lv2 == "SYZY")
            {
                return this.sql_310_SYZY;
            }
            else if (insutype == "340")
            {
                return this.sql_340_LX;
            }

            return null;
        }

        public System.Data.DataTable getLogarithmInfo_MX(string insutype, string clr_type, string clr_type_lv2, string begintime, string endtime)
        {
            string sql = getLogarithmSQL_MX(insutype, clr_type, clr_type_lv2);
            if (string.IsNullOrEmpty(sql)) return null;

            sql = string.Format(sql, begintime, endtime);
            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sql, ref ds);
            return ds.Tables[0];
        }

        public string getLogarithmSQL_MX(string insutype, string clr_type, string clr_type_lv2)
        {
            if (insutype == "390" && clr_type == "11" && clr_type_lv2 != "XG")
            {
                return this.sql_390_11_MX;
            }
            else if (insutype == "310" && clr_type == "11" && clr_type_lv2 != "XG")
            {
                return this.sql_310_11_MX;
            }
            else if (insutype == "390" && clr_type == "21")
            {
                return this.sql_390_21_DRG_MX;
            }
            else if (insutype == "310" && clr_type == "21")
            {
                return this.sql_310_21_DRG_MX;
            }
            else if (insutype == "310" && clr_type == "11" && clr_type_lv2 == "XG")
            {
                return this.sql_310_11_XG_MX;
            }
            else if (insutype == "390" && clr_type == "11" && clr_type_lv2 == "XG")
            {
                return this.sql_390_11_XG_MX;
            }
            else if (insutype == "310" && clr_type_lv2 == "SY")
            {
                return this.sql_310_SY_MX;
            }
            else if (insutype == "310" && clr_type_lv2 == "SYMZ")
            {
                return this.sql_310_SYMZ_MX;
            }
            else if (insutype == "310" && clr_type_lv2 == "SYZY")
            {
                return this.sql_310_SYZY_MX;
            }
            else if (insutype == "340")
            {
                return this.sql_340_LX_MX;
            }

            return null;
        }
        #endregion

        #region 5203
        //结算明细
        public string ExisteSetlinfo5203(string mdtrt_id, string setl_id)
        {
            string strSql = @"select count(*) from fin_gd_balance5203 s 
                                        where 1=1
                                        and s.mdtrt_id = '{0}' 
                                        and s.setl_id  = '{1}'";
            strSql = string.Format(strSql, mdtrt_id, setl_id);
            return this.ExecSqlReturnOne(strSql);
        }

        public int insertSetlinfo5203(Models.Response.ResponseGzsiModel5203.Output.SetlInfo setlInfo)
        {

            string strSql = @"insert into fin_gd_balance5203(
                       setl_id	,--		结算ID
                        mdtrt_id	,--		就诊ID
                        psn_no	,--		人员编号
                        psn_name	,--		人员姓名
                        psn_cert_type	,--		人员证件类型
                        certno	,--		证件号码
                        gend	,--		性别
                        naty	,--		民族
                        brdy	,--		出生日期
                        age	,--		年龄
                        insutype	,--		险种类型
                        psn_type	,--		人员类别
                        cvlserv_flag	,--		公务员标志
                        flxempe_flag	,--		灵活就业标志
                        nwb_flag	,--		新生儿标志
                        insu_optins	,--		参保机构医保区划
                        emp_name	,--		单位名称
                        pay_loc	,--		支付地点类别
                        fixmedins_code	,--		定点医药机构编号
                        fixmedins_name	,--		定点医药机构名称
                        hosp_lv	,--		医院等级
                        fixmedins_poolarea	,--		定点归属机构
                        lmtpric_hosp_lv	,--		限价医院等级
                        dedc_hosp_lv	,--		起付线医院等级
                        begndate	,--		开始日期
                        enddate	,--		结束日期
                        setl_time	,--		结算时间
                        mdtrt_cert_type	,--		就诊凭证类型
                        med_type	,--		医疗类别
                        clr_type	,--		清算类别
                        clr_way	,--		清算方式
                        clr_optins	,--		清算经办机构
                        medfee_sumamt	,--		医疗费总额
                        fulamt_ownpay_amt	,--		全自费金额
                        overlmt_selfpay	,--		超限价自费费用
                        preselfpay_amt	,--		先行自付金额
                        inscp_scp_amt	,--		符合政策范围金额
                        act_pay_dedc	,--		实际支付起付线
                        hifp_pay	,--		基本医疗保险统筹基金支出
                        pool_prop_selfpay	,--		基本医疗保险统筹基金支付比例
                        cvlserv_pay	,--		公务员医疗补助资金支出
                        hifes_pay	,--		企业补充医疗保险基金支出
                        hifmi_pay	,--		居民大病保险资金支出
                        hifob_pay	,--		职工大额医疗费用补助基金支出
                        maf_pay	,--		医疗救助基金支出
                        oth_pay	,--		其他支出
                        fund_pay_sumamt	,--		基金支付总额
                        psn_pay	,--		个人支付金额
                        acct_pay	,--		个人账户支出
                        cash_payamt	,--		现金支付金额
                        balc	,--		余额
                        acct_mulaid_pay	,--		个人账户共济支付金额
                        medins_setl_id	,--		医药机构结算ID
                        refd_setl_flag	,--		退费结算标志
                        year	,--		年度
                        dise_codg	,--		病种编码
                        dise_name	,--		病种名称
                        invono	,--		发票号
                        opter_id	,--		经办人ID
                        opter_name	,--		经办人姓名
                        opt_time	--		经办时间
            )
             values(
                   '{0}',
                    '{1}',
                    '{2}',
                    '{3}',
                    '{4}',
                    '{5}',
                    '{6}',
                    '{7}',
                     to_date('{8}','yyyy-MM-dd'),
                    '{9}',
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
                     to_date('{24}','yyyy-MM-dd HH24:mi:ss'),
                     to_date('{25}','yyyy-MM-dd HH24:mi:ss'),
                    to_date('{26}','yyyy-MM-dd HH24:mi:ss'),
                    '{27}',
                    '{28}',
                    '{29}',
                    '{30}',
                    '{31}',
                    '{32}',
                    '{33}',
                    '{34}',
                    '{35}',
                    '{36}',
                    '{37}',
                    '{38}',
                    '{39}',
                    '{40}',
                    '{41}',
                    '{42}',
                    '{43}',
                    '{44}',
                    '{45}',
                    '{46}',
                    '{47}',
                    '{48}',
                    '{49}',
                    '{50}',
                    '{51}',
                    '{52}',
                    '{53}',
                    '{54}',
                    '{55}',
                    '{56}',
                    '{57}',
                    '{58}',
                    '{59}',
                      to_date('{60}','yyyy-MM-dd HH24:mi:ss')

            )";
            strSql = string.Format(strSql,
                setlInfo.setl_id,//结算ID
                setlInfo.mdtrt_id,//就诊ID
                setlInfo.psn_no,//人员编号
                setlInfo.psn_name,//人员姓名
                setlInfo.psn_cert_type,//人员证件类型
                setlInfo.certno,//证件号码
                setlInfo.gend,//性别
                setlInfo.naty,//民族
                setlInfo.brdy,//出生日期
                setlInfo.age,//年龄
                setlInfo.insutype,//险种类型
                setlInfo.psn_type,//人员类别
                setlInfo.cvlserv_flag,//公务员标志
                setlInfo.flxempe_flag,//灵活就业标志
                setlInfo.nwb_flag,//新生儿标志
                setlInfo.insu_optins,//参保机构医保区划
                setlInfo.emp_name,//单位名称
                setlInfo.pay_loc,//支付地点类别
                setlInfo.fixmedins_code,//定点医药机构编号
                setlInfo.fixmedins_name,//定点医药机构名称
                setlInfo.hosp_lv,//医院等级
                setlInfo.fixmedins_poolarea,//定点归属机构
                setlInfo.lmtpric_hosp_lv,//限价医院等级
                setlInfo.dedc_hosp_lv,//起付线医院等级
                setlInfo.begndate,//开始日期
                setlInfo.enddate,//结束日期
                setlInfo.setl_time,//结算时间
                setlInfo.mdtrt_cert_type,//就诊凭证类型
                setlInfo.med_type,//医疗类别
                setlInfo.clr_type,//清算类别
                setlInfo.clr_way,//清算方式
                setlInfo.clr_optins,//清算经办机构
                setlInfo.medfee_sumamt,//医疗费总额
                setlInfo.fulamt_ownpay_amt,//全自费金额
                setlInfo.overlmt_selfpay,//超限价自费费用
                setlInfo.preselfpay_amt,//先行自付金额
                setlInfo.inscp_scp_amt,//符合政策范围金额
                setlInfo.act_pay_dedc,//实际支付起付线
                setlInfo.hifp_pay,//基本医疗保险统筹基金支出
                setlInfo.pool_prop_selfpay,//基本医疗保险统筹基金支付比例
                setlInfo.cvlserv_pay,//公务员医疗补助资金支出
                setlInfo.hifes_pay,//企业补充医疗保险基金支出
                setlInfo.hifmi_pay,//居民大病保险资金支出
                setlInfo.hifob_pay,//职工大额医疗费用补助基金支出
                setlInfo.maf_pay,//医疗救助基金支出
                setlInfo.oth_pay,//其他支出
                setlInfo.fund_pay_sumamt,//基金支付总额
                setlInfo.psn_pay,//个人支付金额
                setlInfo.acct_pay,//个人账户支出
                setlInfo.cash_payamt,//现金支付金额
                setlInfo.balc,//余额
                setlInfo.acct_mulaid_pay,//个人账户共济支付金额
                setlInfo.medins_setl_id,//医药机构结算ID
                setlInfo.refd_setl_flag,//退费结算标志
                setlInfo.year,//年度
                setlInfo.dise_codg,//病种编码
                setlInfo.dise_name,//病种名称
                setlInfo.invono,//发票号
                setlInfo.opter_id,//经办人ID
                setlInfo.opter_name,//经办人姓名
                setlInfo.opt_time//经办时间
                );

            return this.ExecNoQuery(strSql);


        }
        #endregion

        #region 报表SQL
        #region sql_310_11
        private string sql_310_11 = @"--1佛山市基本医疗保险医疗费用现场结算月结统计表(职工门诊)      
                                                    Select clr_way 清算方式,
                                                    clr_type 清算类别,
                                                    clr_type_lv2 二级清算类别,
                                                    count(distinct a.psn_no) 人数,
                                                    count(distinct a.setl_id) 实际支付人次, 
                                                     (select nvl(sum(c.medfee_sumamt),0)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='310'
                                                    And clr_type='11' And med_type in('11','14')
                                                    and c.clr_way = a.clr_way
                                                    And (c.clr_type_lv2 = a.clr_type_lv2 or (a.clr_type_lv2 is null and  c.clr_type_lv2 is null and c.dise_no not in ('XGJC002',' XGCT001','XGJC003')))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                     ) 医疗费总额,
                                                    sum(case when FUND_PAY_TYPE='310100' then fund_payamt else 0 end) 基本医疗保险统筹基金支付,
                                                    sum(case when FUND_PAY_TYPE='310100' and fund_payamt>10000 then fund_payamt-10000 else 0 end) 基本医疗超1万以上金额,
                                                    sum(case when FUND_PAY_TYPE='310300' then fund_payamt else 0 end) 大病基金支付,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440699' then fund_payamt else 0 end) 公务员基金支付_市直,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440604' then fund_payamt else 0 end) 公务员基金支付_禅城区,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440605' then fund_payamt else 0 end) 公务员基金支付_南海区,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440607' then fund_payamt else 0 end) 公务员基金支付_三水区,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440608' then fund_payamt else 0 end) 公务员基金支付_高明区,
                                                    0 离休医疗保障_市直,
                                                    0 离休医疗保障_禅城区,
                                                    0 离休医疗保障_南海区,
                                                    0 离休医疗保障_顺德区,
                                                    0 离休医疗保障_三水区,
                                                    0 离休医疗保障_高明区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440699' then fund_payamt else 0 end) 医疗救助基金支付_市直,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440604' then fund_payamt else 0 end) 医疗救助基金支付_禅城区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440605' then fund_payamt else 0 end) 医疗救助基金支付_南海区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440606' then fund_payamt else 0 end) 医疗救助基金支付_顺德区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440607' then fund_payamt else 0 end) 医疗救助基金支付_三水区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440608' then fund_payamt else 0 end) 医疗救助基金支付_高明区,
                                                    sum(case when FUND_PAY_TYPE='610200' then fund_payamt else 0 end) 优抚医疗补助,
                                                     (select nvl(sum(c.psn_pay),0)+nvl(sum(c.hifesPay),0)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='310'
                                                    And clr_type='11' And med_type in('11','14')
                                                    and c.clr_way = a.clr_way
                                                    And (c.clr_type_lv2 = a.clr_type_lv2 or (a.clr_type_lv2 is null and  c.clr_type_lv2 is null and c.dise_no not in ('XGJC002',' XGCT001','XGJC003')))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                     ) + sum(case when FUND_PAY_TYPE in ('620101','620100') then fund_payamt else 0 end)  个人支付,
                                                    nvl( sum(fund_payamt),0)  基金支付总额
                                                    From GDSI_DZ_SETLINFO a
                                                    left join GDSI_DZ_SETLDETAIL b on a.mdtrt_id = b.mdtrt_id and a.setl_id = b.setl_id
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='310'
                                                    And clr_type='11' And med_type in('11','14')
                                                    And (clr_type_lv2 in('3102','3103','999950') or  (clr_type_lv2 is null and dise_no not in ('XGJC002',' XGCT001','XGJC003')))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                    group by clr_way,clr_type,clr_type_lv2";//1
        #endregion
        #region sql_390_11
        private string sql_390_11 = @"--2佛山市基本医疗保险医疗费用现场结算月结统计表(居民门诊)     
                                                    Select clr_way 清算方式,
                                                    clr_type 清算类别,
                                                    clr_type_lv2 二级清算类别,
                                                    count(distinct a.psn_no) 人数,
                                                    count(distinct a.setl_id) 实际支付人次, 
                                                     (select nvl(sum(c.medfee_sumamt),0)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='390'
                                                    and c.clr_way = a.clr_way
                                                    And clr_type='11' And med_type in('11','14')
                                                    And (c.clr_type_lv2 = a.clr_type_lv2 or  (a.clr_type_lv2 is null and  c.clr_type_lv2 is null And c.dise_no not in ('XGJC002',' XGCT001','XGJC003')))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                     ) 医疗费总额,
                                                    sum(case when FUND_PAY_TYPE='390100' then fund_payamt else 0 end) 基本医疗保险统筹基金支付,
                                                    sum(case when FUND_PAY_TYPE='390100' and fund_payamt>10000 then fund_payamt-10000 else 0 end) 基本医疗超1万以上金额,
                                                    sum(case when FUND_PAY_TYPE='390200' then fund_payamt else 0 end) 大病基金支付,
                                                    0 公务员基金支付_市直,
                                                    0 公务员基金支付_禅城区,
                                                    0 公务员基金支付_南海区,
                                                    0 公务员基金支付_三水区,
                                                    0 公务员基金支付_高明区,
                                                    0 离休医疗保障_市直,
                                                    0 离休医疗保障_禅城区,
                                                    0 离休医疗保障_南海区,
                                                    0 离休医疗保障_顺德区,
                                                    0 离休医疗保障_三水区,
                                                    0 离休医疗保障_高明区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440699' then fund_payamt else 0 end) 医疗救助基金支付_市直,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440604' then fund_payamt else 0 end) 医疗救助基金支付_禅城区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440605' then fund_payamt else 0 end) 医疗救助基金支付_南海区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440606' then fund_payamt else 0 end) 医疗救助基金支付_顺德区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440607' then fund_payamt else 0 end) 医疗救助基金支付_三水区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440608' then fund_payamt else 0 end) 医疗救助基金支付_高明区,
                                                    sum(case when FUND_PAY_TYPE='610200' then fund_payamt else 0 end) 优抚医疗补助,
                                                     (select nvl(sum(c.psn_pay),0)+nvl(sum(c.hifesPay),0)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='390'
                                                    And clr_type='11' And med_type in('11','14')
                                                    and c.clr_way = a.clr_way
                                                    And (c.clr_type_lv2 = a.clr_type_lv2 or (a.clr_type_lv2 is null and  c.clr_type_lv2 is null And c.dise_no not in ('XGJC002',' XGCT001','XGJC003')))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                     ) + sum(case when FUND_PAY_TYPE in ('620101','620100') then fund_payamt else 0 end)   个人支付,
                                                    nvl( sum(fund_payamt),0)  基金支付总额
                                                    From GDSI_DZ_SETLINFO a left join GDSI_DZ_SETLDETAIL b on a.mdtrt_id = b.mdtrt_id and a.setl_id = b.setl_id
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='390'
                                                    And clr_type='11' And med_type in('11','14')
                                                    And (clr_type_lv2 in('3901','3902','999951') or  (clr_type_lv2 is null And dise_no not in ('XGJC002',' XGCT001','XGJC003')))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                    group by clr_way,clr_type,clr_type_lv2";//2
        #endregion
        #region sql_310_11_XG
        private string sql_310_11_XG = @"--7佛山市基本医疗保险医疗费用现场结算月结统计表(职工新冠)  
                                                    Select clr_way 清算方式,
                                                    clr_type 清算类别,
                                                    clr_type_lv2 二级清算类别,
                                                    count(distinct a.psn_no) 人数,
                                                    count(distinct a.setl_id) 实际支付人次, 
                                                     (select nvl(sum(c.medfee_sumamt),0)  From GDSI_DZ_SETLINFO c 
                                                     Where REFD_SETL_FLAG='0'
                                                    And insutype='310'
                                                    And clr_type='11'
                                                    and c.clr_way = a.clr_way
                                                    And (c.clr_type_lv2 = a.clr_type_lv2  or (a.clr_type_lv2 is null and  c.clr_type_lv2 is null And c.dise_no  in ('XGJC002',' XGCT001','XGJC003')))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                     ) 医疗费总额,
                                                    sum(case when FUND_PAY_TYPE='310100' then fund_payamt else 0 end) 基本医疗保险统筹基金支付,
                                                    sum(case when FUND_PAY_TYPE='310100' and fund_payamt>10000 then fund_payamt-10000 else 0 end) 基本医疗超1万以上金额,
                                                    sum(case when FUND_PAY_TYPE='310300' then fund_payamt else 0 end) 大病基金支付,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440699' then fund_payamt else 0 end) 公务员基金支付_市直,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440604' then fund_payamt else 0 end) 公务员基金支付_禅城区,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440605' then fund_payamt else 0 end) 公务员基金支付_南海区,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440607' then fund_payamt else 0 end) 公务员基金支付_三水区,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440608' then fund_payamt else 0 end) 公务员基金支付_高明区,
                                                    0 离休医疗保障_市直,
                                                    0 离休医疗保障_禅城区,
                                                    0 离休医疗保障_南海区,
                                                    0 离休医疗保障_顺德区,
                                                    0 离休医疗保障_三水区,
                                                    0 离休医疗保障_高明区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440699' then fund_payamt else 0 end) 医疗救助基金支付_市直,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440604' then fund_payamt else 0 end) 医疗救助基金支付_禅城区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440605' then fund_payamt else 0 end) 医疗救助基金支付_南海区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440606' then fund_payamt else 0 end) 医疗救助基金支付_顺德区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440607' then fund_payamt else 0 end) 医疗救助基金支付_三水区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440608' then fund_payamt else 0 end) 医疗救助基金支付_高明区,
                                                    sum(case when FUND_PAY_TYPE='610200' then fund_payamt else 0 end) 优抚医疗补助,
                                                    (select nvl(sum(c.psn_pay),0)  From GDSI_DZ_SETLINFO c 
                                                     Where REFD_SETL_FLAG='0'
                                                    And insutype='310'
                                                    And clr_type='11'
                                                    and c.clr_way = a.clr_way
                                                    And (c.clr_type_lv2 = a.clr_type_lv2  or (a.clr_type_lv2 is null and  c.clr_type_lv2 is null And c.dise_no  in ('XGJC002',' XGCT001','XGJC003')))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                     ) + sum(case when FUND_PAY_TYPE in ('620101','620100') then fund_payamt else 0 end)  个人支付,
                                                    nvl( sum(fund_payamt),0)  基金支付总额
                                                    From GDSI_DZ_SETLINFO a left join GDSI_DZ_SETLDETAIL b on a.mdtrt_id = b.mdtrt_id and a.setl_id = b.setl_id
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='310'
                                                    And clr_type='11'
                                                    And (clr_type_lv2 in('999903','999905','999948') or  (clr_type_lv2 is null And dise_no  in ('XGJC002',' XGCT001','XGJC003')))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                    group by clr_way,clr_type,clr_type_lv2";//7
        #endregion
        #region sql_390_11_XG
        private string sql_390_11_XG = @"--8佛山市基本医疗保险医疗费用现场结算月结统计表(居民新冠)    
                                                    Select clr_way 清算方式,
                                                    clr_type 清算类别,
                                                    clr_type_lv2 二级清算类别,
                                                    count(distinct a.psn_no) 人数,
                                                    count(distinct a.setl_id) 实际支付人次, 
                                                     (select nvl(sum(c.medfee_sumamt),0)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='390'
                                                    And clr_type='11'
                                                    and c.clr_way = a.clr_way
                                                    And (c.clr_type_lv2 = a.clr_type_lv2  or (a.clr_type_lv2 is null and  c.clr_type_lv2 is null And c.dise_no  in ('XGJC002',' XGCT001','XGJC003')))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                     ) 医疗费总额,
                                                    sum(case when FUND_PAY_TYPE='390100' then fund_payamt else 0 end) 基本医疗保险统筹基金支付,
                                                    sum(case when FUND_PAY_TYPE='390100' and fund_payamt>10000 then fund_payamt-10000 else 0 end) 基本医疗超1万以上金额,
                                                    sum(case when FUND_PAY_TYPE='390200' then fund_payamt else 0 end) 大病基金支付,
                                                    0 公务员基金支付_市直,
                                                    0 公务员基金支付_禅城区,
                                                    0 公务员基金支付_南海区,
                                                    0 公务员基金支付_三水区,
                                                    0 公务员基金支付_高明区,
                                                    0 离休医疗保障_市直,
                                                    0 离休医疗保障_禅城区,
                                                    0 离休医疗保障_南海区,
                                                    0 离休医疗保障_顺德区,
                                                    0 离休医疗保障_三水区,
                                                    0 离休医疗保障_高明区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440699' then fund_payamt else 0 end) 医疗救助基金支付_市直,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440604' then fund_payamt else 0 end) 医疗救助基金支付_禅城区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440605' then fund_payamt else 0 end) 医疗救助基金支付_南海区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440606' then fund_payamt else 0 end) 医疗救助基金支付_顺德区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440607' then fund_payamt else 0 end) 医疗救助基金支付_三水区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440608' then fund_payamt else 0 end) 医疗救助基金支付_高明区,
                                                    sum(case when FUND_PAY_TYPE='610200' then fund_payamt else 0 end) 优抚医疗补助,
                                                     (select nvl(sum(c.psn_pay),0)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='390'
                                                    And clr_type='11'
                                                    and c.clr_way = a.clr_way
                                                    And (c.clr_type_lv2 = a.clr_type_lv2  or (a.clr_type_lv2 is null and  c.clr_type_lv2 is null And c.dise_no  in ('XGJC002',' XGCT001','XGJC003')))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                     ) + sum(case when FUND_PAY_TYPE in ('620101','620100') then fund_payamt else 0 end)  个人支付,
                                                    nvl(sum(fund_payamt),0)  基金支付总额
                                                    From GDSI_DZ_SETLINFO a left join GDSI_DZ_SETLDETAIL b on a.mdtrt_id = b.mdtrt_id and a.setl_id = b.setl_id
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='390'
                                                    And clr_type='11'
                                                    And (clr_type_lv2 in('999904','999906','999949') or  (clr_type_lv2 is null And dise_no  in ('XGJC002',' XGCT001','XGJC003')))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                    group by clr_way,clr_type,clr_type_lv2";//8
        #endregion

        #region sql_310_21_DRG
        private string sql_310_21_DRG = @"--3佛山市基本医疗保险医疗费用现场结算月结统计表(职工DRG住院)  
                                                    Select clr_way 清算方式,
                                                    clr_type 清算类别,
                                                    clr_type_lv2 二级清算类别,
                                                    count(distinct a.psn_no) 人数,
                                                    count(distinct a.setl_id) 实际支付人次, 
                                                     (select nvl(sum(c.medfee_sumamt),0)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    and med_type='21'
                                                    And insutype='310'
                                                    And clr_type='21'
                                                    and c.clr_way = a.clr_way
                                                    And (c.clr_type_lv2 = a.clr_type_lv2 or (a.clr_type_lv2 is null and  c.clr_type_lv2 is null))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                     ) 医疗费总额,
                                                    sum(case when FUND_PAY_TYPE='310100' then fund_payamt else 0 end) 基本医疗保险统筹基金支付,
                                                    sum(case when FUND_PAY_TYPE='310100' and fund_payamt>10000 then fund_payamt-10000 else 0 end) 基本医疗超1万以上金额,
                                                    sum(case when FUND_PAY_TYPE='310300' then fund_payamt else 0 end) 大病基金支付,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440699' then fund_payamt else 0 end) 公务员基金支付_市直,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440604' then fund_payamt else 0 end) 公务员基金支付_禅城区,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440605' then fund_payamt else 0 end) 公务员基金支付_南海区,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440607' then fund_payamt else 0 end) 公务员基金支付_三水区,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440608' then fund_payamt else 0 end) 公务员基金支付_高明区,
                                                    0 离休医疗保障_市直,
                                                    0 离休医疗保障_禅城区,
                                                    0 离休医疗保障_南海区,
                                                    0 离休医疗保障_顺德区,
                                                    0 离休医疗保障_三水区,
                                                    0 离休医疗保障_高明区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440699' then fund_payamt else 0 end) 医疗救助基金支付_市直,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440604' then fund_payamt else 0 end) 医疗救助基金支付_禅城区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440605' then fund_payamt else 0 end) 医疗救助基金支付_南海区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440606' then fund_payamt else 0 end) 医疗救助基金支付_顺德区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440607' then fund_payamt else 0 end) 医疗救助基金支付_三水区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440608' then fund_payamt else 0 end) 医疗救助基金支付_高明区,
                                                    sum(case when FUND_PAY_TYPE='610200' then fund_payamt else 0 end) 优抚医疗补助,
                                                     (select nvl(sum(c.psn_pay),0) + nvl(sum(c.hifesPay),0) From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    and med_type='21'
                                                    And insutype='310'
                                                    And clr_type='21'
                                                    and c.clr_way = a.clr_way
                                                    And (c.clr_type_lv2 = a.clr_type_lv2 or (a.clr_type_lv2 is null and  c.clr_type_lv2 is null))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                     ) + sum(case when FUND_PAY_TYPE in ('620101','620100') then fund_payamt else 0 end)  个人支付,
                                                    nvl(sum(fund_payamt),0)  基金支付总额
                                                    From GDSI_DZ_SETLINFO a left join GDSI_DZ_SETLDETAIL b on a.mdtrt_id = b.mdtrt_id and a.setl_id = b.setl_id
                                                    Where REFD_SETL_FLAG='0'
                                                    and med_type='21'
                                                    And insutype='310'
                                                    And clr_type='21'
                                                    And (clr_type_lv2='3104' or  clr_type_lv2 is null)
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                    group by clr_way,clr_type,clr_type_lv2";//3
        #endregion
        #region sql_390_21_DRG
        private string sql_390_21_DRG = @"--4佛山市基本医疗保险医疗费用现场结算月结统计表(居民DRG住院)  
                                                    Select clr_way 清算方式,
                                                    clr_type 清算类别,
                                                    clr_type_lv2 二级清算类别,
                                                    count(distinct a.psn_no) 人数,
                                                    count(distinct a.setl_id) 实际支付人次, 
                                                     (select nvl(sum(c.medfee_sumamt),0)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    and med_type='21'
                                                    And insutype='390'
                                                    And clr_type='21'
                                                    and c.clr_way = a.clr_way
                                                    And (c.clr_type_lv2 = a.clr_type_lv2 or (a.clr_type_lv2 is null and  c.clr_type_lv2 is null))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                     ) 医疗费总额,
                                                    sum(case when FUND_PAY_TYPE='390100' then fund_payamt else 0 end) 基本医疗保险统筹基金支付,
                                                    sum(case when FUND_PAY_TYPE='390100' and fund_payamt>10000 then fund_payamt-10000 else 0 end) 基本医疗超1万以上金额,
                                                    sum(case when FUND_PAY_TYPE='390200' then fund_payamt else 0 end) 大病基金支付,
                                                    0 公务员基金支付_市直,
                                                    0 公务员基金支付_禅城区,
                                                    0 公务员基金支付_南海区,
                                                    0 公务员基金支付_三水区,
                                                    0 公务员基金支付_高明区,
                                                    0 离休医疗保障_市直,
                                                    0 离休医疗保障_禅城区,
                                                    0 离休医疗保障_南海区,
                                                    0 离休医疗保障_顺德区,
                                                    0 离休医疗保障_三水区,
                                                    0 离休医疗保障_高明区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440699' then fund_payamt else 0 end) 医疗救助基金支付_市直,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440604' then fund_payamt else 0 end) 医疗救助基金支付_禅城区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440605' then fund_payamt else 0 end) 医疗救助基金支付_南海区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440606' then fund_payamt else 0 end) 医疗救助基金支付_顺德区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440607' then fund_payamt else 0 end) 医疗救助基金支付_三水区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440608' then fund_payamt else 0 end) 医疗救助基金支付_高明区,
                                                    sum(case when FUND_PAY_TYPE='610200' then fund_payamt else 0 end) 优抚医疗补助,
                                                     (select nvl(sum(c.psn_pay),0)+nvl(sum(c.hifesPay),0)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    and med_type='21'
                                                    And insutype='390'
                                                    And clr_type='21'
                                                    and c.clr_way = a.clr_way
                                                    And (c.clr_type_lv2 = a.clr_type_lv2 or (a.clr_type_lv2 is null and  c.clr_type_lv2 is null))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                     ) + sum(case when FUND_PAY_TYPE in ('620101','620100') then fund_payamt else 0 end)  个人支付,
                                                    nvl(sum(fund_payamt) ,0) 基金支付总额
                                                    From GDSI_DZ_SETLINFO a left join GDSI_DZ_SETLDETAIL b on a.mdtrt_id = b.mdtrt_id and a.setl_id = b.setl_id
                                                    Where REFD_SETL_FLAG='0'
                                                    and med_type='21'
                                                    And insutype='390'
                                                    And clr_type='21'
                                                    And (clr_type_lv2='3903' or  clr_type_lv2 is null)
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                    group by clr_way,clr_type,clr_type_lv2";//4
        #endregion
        #region sql_310_21_FDRG 佛山以DRG的形式上传
        private string sql_310_21_FDRG = @"--5佛山市基本医疗保险医疗费用现场结算月结统计表(职工非DRG住院)  
                                                    Select clr_way 清算方式,
                                                    clr_type 清算类别,
                                                    clr_type_lv2 二级清算类别,
                                                    count(distinct a.psn_no) 人数,
                                                    count(distinct a.setl_id) 实际支付人次, 
                                                     (select nvl(sum(c.medfee_sumamt),0)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='310'
                                                    And clr_type='21'
                                                    and med_type !='21'
                                                    and c.clr_way = a.clr_way
                                                    And (c.clr_type_lv2 = a.clr_type_lv2 or (a.clr_type_lv2 is null and  c.clr_type_lv2 is null))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                     ) 医疗费总额,
                                                    sum(case when FUND_PAY_TYPE='310100' then fund_payamt else 0 end) 基本医疗保险统筹基金支付,
                                                    sum(case when FUND_PAY_TYPE='310100' and fund_payamt>10000 then fund_payamt-10000 else 0 end) 基本医疗超1万以上金额,
                                                    sum(case when FUND_PAY_TYPE='310300' then fund_payamt else 0 end) 大病基金支付,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440699' then fund_payamt else 0 end) 公务员基金支付_市直,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440604' then fund_payamt else 0 end) 公务员基金支付_禅城区,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440605' then fund_payamt else 0 end) 公务员基金支付_南海区,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440607' then fund_payamt else 0 end) 公务员基金支付_三水区,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440608' then fund_payamt else 0 end) 公务员基金支付_高明区,
                                                    0 离休医疗保障_市直,
                                                    0 离休医疗保障_禅城区,
                                                    0 离休医疗保障_南海区,
                                                    0 离休医疗保障_顺德区,
                                                    0 离休医疗保障_三水区,
                                                    0 离休医疗保障_高明区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440699' then fund_payamt else 0 end) 医疗救助基金支付_市直,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440604' then fund_payamt else 0 end) 医疗救助基金支付_禅城区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440605' then fund_payamt else 0 end) 医疗救助基金支付_南海区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440606' then fund_payamt else 0 end) 医疗救助基金支付_顺德区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440607' then fund_payamt else 0 end) 医疗救助基金支付_三水区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440608' then fund_payamt else 0 end) 医疗救助基金支付_高明区,
                                                    sum(case when FUND_PAY_TYPE='610200' then fund_payamt else 0 end) 优抚医疗补助,
                                                    (select nvl(sum(c.psn_pay),0)+nvl(sum(c.hifesPay),0)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='310'
                                                    And clr_type='21'
                                                    and med_type !='21'
                                                    and c.clr_way = a.clr_way
                                                    And (c.clr_type_lv2 = a.clr_type_lv2 or (a.clr_type_lv2 is null and  c.clr_type_lv2 is null))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                     ) + sum(case when FUND_PAY_TYPE in ('620101','620100') then fund_payamt else 0 end)  个人支付,
                                                    nvl(sum(fund_payamt),0)  基金支付总额
                                                    From GDSI_DZ_SETLINFO a left join GDSI_DZ_SETLDETAIL b on a.mdtrt_id = b.mdtrt_id and a.setl_id = b.setl_id
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='310'
                                                    And clr_type='21'
                                                    And (clr_type_lv2 in('999932','999907') or  clr_type_lv2 is null)
                                                    and med_type in ('21','23')
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                    group by clr_way,clr_type,clr_type_lv2";//5
        #endregion
        #region sql_390_21_FDRG 佛山以DRG的形式上传
        private string sql_390_21_FDRG = @"--6佛山市基本医疗保险医疗费用现场结算月结统计表(居民非DRG住院)  
                                                    Select clr_way 清算方式,
                                                    clr_type 清算类别,
                                                    clr_type_lv2 二级清算类别,
                                                    count(distinct a.psn_no) 人数,
                                                    count(distinct a.setl_id) 实际支付人次, 
                                                     (select nvl(sum(c.medfee_sumamt),0)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='390'
                                                    And clr_type='21'
                                                    and med_type != '21'
                                                    and c.clr_way = a.clr_way
                                                    And (c.clr_type_lv2 = a.clr_type_lv2 or (a.clr_type_lv2 is null and  c.clr_type_lv2 is null))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                     ) 医疗费总额,
                                                    sum(case when FUND_PAY_TYPE='390100' then fund_payamt else 0 end) 基本医疗保险统筹基金支付,
                                                    sum(case when FUND_PAY_TYPE='390100' and fund_payamt>10000 then fund_payamt-10000 else 0 end) 基本医疗超1万以上金额,
                                                    sum(case when FUND_PAY_TYPE='390200' then fund_payamt else 0 end) 大病基金支付,
                                                    0 公务员基金支付_市直,
                                                    0 公务员基金支付_禅城区,
                                                    0 公务员基金支付_南海区,
                                                    0 公务员基金支付_三水区,
                                                    0 公务员基金支付_高明区,
                                                    0 离休医疗保障_市直,
                                                    0 离休医疗保障_禅城区,
                                                    0 离休医疗保障_南海区,
                                                    0 离休医疗保障_顺德区,
                                                    0 离休医疗保障_三水区,
                                                    0 离休医疗保障_高明区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440699' then fund_payamt else 0 end) 医疗救助基金支付_市直,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440604' then fund_payamt else 0 end) 医疗救助基金支付_禅城区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440605' then fund_payamt else 0 end) 医疗救助基金支付_南海区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440606' then fund_payamt else 0 end) 医疗救助基金支付_顺德区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440607' then fund_payamt else 0 end) 医疗救助基金支付_三水区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440608' then fund_payamt else 0 end) 医疗救助基金支付_高明区,
                                                    sum(case when FUND_PAY_TYPE='610200' then fund_payamt else 0 end) 优抚医疗补助,
                                                     (select nvl(sum(c.psn_pay),0)+nvl(sum(c.hifesPay),0)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='390'
                                                    And clr_type='21'
                                                    and med_type != '21'
                                                    and c.clr_way = a.clr_way
                                                    And (c.clr_type_lv2 = a.clr_type_lv2 or (a.clr_type_lv2 is null and  c.clr_type_lv2 is null))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                     ) + sum(case when FUND_PAY_TYPE in ('620101','620100') then fund_payamt else 0 end)   个人支付,
                                                    nvl(sum(fund_payamt),0)  基金支付总额
                                                    From GDSI_DZ_SETLINFO a left join GDSI_DZ_SETLDETAIL b on a.mdtrt_id = b.mdtrt_id and a.setl_id = b.setl_id
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='390'
                                                    And clr_type='21'
                                                    And (clr_type_lv2 in('999933','999908') or  clr_type_lv2 is null)
                                                    and med_type in ('21','23')
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                    group by clr_way,clr_type,clr_type_lv2";//6
        #endregion

        #region sql_310_SY
        private string sql_310_SY = @"--9佛山市基本医疗保险医疗费用现场结算月结统计表(生育清算)    
                                                    Select clr_way 清算方式,
                                                    clr_type 清算类别,
                                                    clr_type_lv2 二级清算类别,
                                                    count(distinct a.psn_no) 人数,
                                                    count(distinct a.setl_id) 实际支付人次, 
                                                    (select nvl(sum(c.medfee_sumamt),0)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='310'
                                                    And med_type in('51','52')
                                                    and c.clr_way = a.clr_way
                                                    And (c.clr_type_lv2 = a.clr_type_lv2 )
                                                   And pay_loc='2'                                                     
                                                   And ((matnEnddate >=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                            and matnEnddate <to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                            and med_type ='51')
                                                            or
                                                            (setl_time >=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                            and setl_time <to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                            and med_type ='52')
                                                            )
                                                     ) 医疗费总额,
                                                    sum(case when FUND_PAY_TYPE='510100' then fund_payamt else 0 end) 基本医疗保险统筹基金支付,
                                                    sum(case when FUND_PAY_TYPE='510100' and fund_payamt>10000 then fund_payamt-10000 else 0 end) 基本医疗超1万以上金额,
                                                    0 大病基金支付,
                                                    0 公务员基金支付_市直,
                                                    0 公务员基金支付_禅城区,
                                                    0 公务员基金支付_南海区,
                                                    0 公务员基金支付_三水区,
                                                    0 公务员基金支付_高明区,
                                                    0 离休医疗保障_市直,
                                                    0 离休医疗保障_禅城区,
                                                    0 离休医疗保障_南海区,
                                                    0 离休医疗保障_顺德区,
                                                    0 离休医疗保障_三水区,
                                                    0 离休医疗保障_高明区,
                                                    0 医疗救助基金支付_市直,
                                                    0 医疗救助基金支付_禅城区,
                                                    0 医疗救助基金支付_南海区,
                                                    0 医疗救助基金支付_顺德区,
                                                    0 医疗救助基金支付_三水区,
                                                    0 医疗救助基金支付_高明区,
                                                    0 优抚医疗补助,
                                                    (select nvl(sum(c.psn_pay),0)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='310'
                                                    And med_type in('51','52')
                                                    and c.clr_way = a.clr_way
                                                    and (c.clr_type_lv2 = a.clr_type_lv2 )
                                                   And pay_loc='2' 
                                                   And ((matnEnddate >=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                            and matnEnddate <to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                            and med_type ='51')
                                                            or
                                                            (setl_time >=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                            and setl_time <to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                            and med_type ='52')
                                                            )
                                                     ) + sum(case when FUND_PAY_TYPE in ('620101','620100') then fund_payamt else 0 end)   个人支付，
                                                     nvl(sum(fund_payamt),0)  基金支付总额
                                                    From GDSI_DZ_SETLINFO a left join GDSI_DZ_SETLDETAIL b on a.mdtrt_id = b.mdtrt_id and a.setl_id = b.setl_id
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='310'
                                                    And (med_type in('51','52'))
                                                   And pay_loc='2' 
                                                    And ((matnEnddate >=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                            and matnEnddate <to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                            and med_type ='51')
                                                            or
                                                            (setl_time >=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                            and setl_time <to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                            and med_type ='52')
                                                            )
                                                    group by clr_way,clr_type,clr_type_lv2";//9
        #endregion
        #region sql_310_SYMZ
        private string sql_310_SYMZ = @"--9佛山市基本医疗保险医疗费用现场结算月结统计表(生育清算)    
                                                    Select clr_way 清算方式,
                                                    clr_type 清算类别,
                                                    clr_type_lv2 二级清算类别,
                                                    count(distinct a.psn_no) 人数,
                                                    count(distinct a.setl_id) 实际支付人次, 
                                                    (select nvl(sum(c.medfee_sumamt),0)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='310'
                                                    And med_type = '51'
                                                    and c.clr_way = a.clr_way
                                                    And (c.clr_type_lv2 = a.clr_type_lv2 )
                                                   And pay_loc='2'                                                     
                                                   And ((matnEnddate >=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                            and matnEnddate <to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                            and med_type ='51')
                                                            )
                                                     ) 医疗费总额,
                                                    sum(case when FUND_PAY_TYPE='510100' then fund_payamt else 0 end) 基本医疗保险统筹基金支付,
                                                    sum(case when FUND_PAY_TYPE='510100' and fund_payamt>10000 then fund_payamt-10000 else 0 end) 基本医疗超1万以上金额,
                                                    0 大病基金支付,
                                                    0 公务员基金支付_市直,
                                                    0 公务员基金支付_禅城区,
                                                    0 公务员基金支付_南海区,
                                                    0 公务员基金支付_三水区,
                                                    0 公务员基金支付_高明区,
                                                    0 离休医疗保障_市直,
                                                    0 离休医疗保障_禅城区,
                                                    0 离休医疗保障_南海区,
                                                    0 离休医疗保障_顺德区,
                                                    0 离休医疗保障_三水区,
                                                    0 离休医疗保障_高明区,
                                                    0 医疗救助基金支付_市直,
                                                    0 医疗救助基金支付_禅城区,
                                                    0 医疗救助基金支付_南海区,
                                                    0 医疗救助基金支付_顺德区,
                                                    0 医疗救助基金支付_三水区,
                                                    0 医疗救助基金支付_高明区,
                                                    0 优抚医疗补助,
                                                    (select nvl(sum(c.psn_pay),0)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='310'
                                                    And med_type ='51'
                                                    and c.clr_way = a.clr_way
                                                    and (c.clr_type_lv2 = a.clr_type_lv2 )
                                                   And pay_loc='2' 
                                                   And (matnEnddate >=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                            and matnEnddate <to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                            and med_type ='51')
                                                     ) + sum(case when FUND_PAY_TYPE in ('620101','620100') then fund_payamt else 0 end)   个人支付，
                                                     nvl(sum(fund_payamt),0)  基金支付总额
                                                    From GDSI_DZ_SETLINFO a left join GDSI_DZ_SETLDETAIL b on a.mdtrt_id = b.mdtrt_id and a.setl_id = b.setl_id
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='310'
                                                    And (med_type = '51')
                                                   And pay_loc='2' 
                                                    And ((matnEnddate >=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                            and matnEnddate <to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                            and med_type ='51')
                                                            )
                                                    group by clr_way,clr_type,clr_type_lv2";//9
        #endregion
        #region sql_310_SYZY
        private string sql_310_SYZY = @"--9佛山市基本医疗保险医疗费用现场结算月结统计表(生育清算)    
                                                    Select clr_way 清算方式,
                                                    clr_type 清算类别,
                                                    clr_type_lv2 二级清算类别,
                                                    count(distinct a.psn_no) 人数,
                                                    count(distinct a.setl_id) 实际支付人次, 
                                                    (select nvl(sum(c.medfee_sumamt),0)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='310'
                                                    And med_type = '52'
                                                    and c.clr_way = a.clr_way
                                                    And (c.clr_type_lv2 = a.clr_type_lv2 )
                                                   And pay_loc='2'                                                     
                                                   And (
                                                            (setl_time >=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                            and setl_time <to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                            and med_type ='52')
                                                            )
                                                     ) 医疗费总额,
                                                    sum(case when FUND_PAY_TYPE='510100' then fund_payamt else 0 end) 基本医疗保险统筹基金支付,
                                                    sum(case when FUND_PAY_TYPE='510100' and fund_payamt>10000 then fund_payamt-10000 else 0 end) 基本医疗超1万以上金额,
                                                    0 大病基金支付,
                                                    0 公务员基金支付_市直,
                                                    0 公务员基金支付_禅城区,
                                                    0 公务员基金支付_南海区,
                                                    0 公务员基金支付_三水区,
                                                    0 公务员基金支付_高明区,
                                                    0 离休医疗保障_市直,
                                                    0 离休医疗保障_禅城区,
                                                    0 离休医疗保障_南海区,
                                                    0 离休医疗保障_顺德区,
                                                    0 离休医疗保障_三水区,
                                                    0 离休医疗保障_高明区,
                                                    0 医疗救助基金支付_市直,
                                                    0 医疗救助基金支付_禅城区,
                                                    0 医疗救助基金支付_南海区,
                                                    0 医疗救助基金支付_顺德区,
                                                    0 医疗救助基金支付_三水区,
                                                    0 医疗救助基金支付_高明区,
                                                    0 优抚医疗补助,
                                                    (select nvl(sum(c.psn_pay),0)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='310'
                                                    And med_type ='52'
                                                    and c.clr_way = a.clr_way
                                                    and (c.clr_type_lv2 = a.clr_type_lv2 )
                                                   And pay_loc='2' 
                                                   And (
                                                            (setl_time >=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                            and setl_time <to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                            and med_type ='52')
                                                            )
                                                     ) + sum(case when FUND_PAY_TYPE in ('620101','620100') then fund_payamt else 0 end)   个人支付，
                                                     nvl(sum(fund_payamt),0)  基金支付总额
                                                    From GDSI_DZ_SETLINFO a left join GDSI_DZ_SETLDETAIL b on a.mdtrt_id = b.mdtrt_id and a.setl_id = b.setl_id
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='310'
                                                    And (med_type ='52')
                                                   And pay_loc='2' 
                                                    And (
                                                            (setl_time >=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                            and setl_time <to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                            and med_type ='52')
                                                            )
                                                    group by clr_way,clr_type,clr_type_lv2";//9
        #endregion

        #region sql_340_LX
        private string sql_340_LX = @"--10佛山市基本医疗保险医疗费用现场结算月结统计表(离休)   
                                                    Select clr_way 清算方式,
                                                    clr_type 清算类别,
                                                    clr_type_lv2 二级清算类别,
                                                    count(distinct a.psn_no) 人数,
                                                    count(distinct a.setl_id) 实际支付人次, 
                                                    (select sum(c.medfee_sumamt)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='340'
                                                    and c.clr_way = a.clr_way
                                                    And (c.clr_type_lv2 = a.clr_type_lv2 or (a.clr_type_lv2 is null and  c.clr_type_lv2 is null))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                     ) 医疗费总额,
                                                    0 基本医疗保险统筹基金支付,
                                                    0 基本医疗超1万以上金额,
                                                    0 大病基金支付,
                                                    0 公务员基金支付_市直,
                                                    0 公务员基金支付_禅城区,
                                                    0 公务员基金支付_南海区,
                                                    0 公务员基金支付_三水区,
                                                    0 公务员基金支付_高明区,
                                                    sum(case when FUND_PAY_TYPE='340100' and insu_optins='440699' then fund_payamt else 0 end) 离休医疗保障_市直,
                                                    sum(case when FUND_PAY_TYPE='340100' and insu_optins='440604' then fund_payamt else 0 end) 离休医疗保障_禅城区,
                                                    sum(case when FUND_PAY_TYPE='340100' and insu_optins='440605' then fund_payamt else 0 end) 离休医疗保障_南海区,
                                                    sum(case when FUND_PAY_TYPE='340100' and insu_optins='440606' then fund_payamt else 0 end) 离休医疗保障_顺德区,
                                                    sum(case when FUND_PAY_TYPE='340100' and insu_optins='440607' then fund_payamt else 0 end) 离休医疗保障_三水区,
                                                    sum(case when FUND_PAY_TYPE='340100' and insu_optins='440608' then fund_payamt else 0 end) 离休医疗保障_高明区,
                                                    0 医疗救助基金支付_市直,
                                                    0 医疗救助基金支付_禅城区,
                                                    0 医疗救助基金支付_南海区,
                                                    0 医疗救助基金支付_顺德区,
                                                    0 医疗救助基金支付_三水区,
                                                    0 医疗救助基金支付_高明区,
                                                    0 优抚医疗补助,
                                                    (select sum(c.psn_pay)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='340'
                                                    and c.clr_way = a.clr_way
                                                    And (c.clr_type_lv2 = a.clr_type_lv2 or (a.clr_type_lv2 is null and  c.clr_type_lv2 is null))
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                     ) + sum(case when FUND_PAY_TYPE in ('620101','620100') then fund_payamt else 0 end)   个人支付,
                                                    sum(fund_payamt)  基金支付总额
                                                    From GDSI_DZ_SETLINFO a left join GDSI_DZ_SETLDETAIL b on a.mdtrt_id = b.mdtrt_id and a.setl_id = b.setl_id
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='340'
                                                   And pay_loc='2' And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                    group by clr_way,clr_type,clr_type_lv2";//10
        #endregion

        #region sql_ALL
        private string sql_ALL = @"--10佛山市基本医疗保险医疗费用现场结算月结统计表(汇总)   
                                                    Select clr_way 清算方式,
                                                    clr_type 清算类别,
                                                    clr_type_lv2 二级清算类别,
                                                    count(distinct a.psn_no) 人数,
                                                    count(distinct a.setl_id) 实际支付人次, 
                                                    (select nvl(sum(c.medfee_sumamt),0)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    And c.clr_way = a.clr_way
                                                    And c.clr_type = a.clr_type
                                                    And ((c.clr_type_lv2 = a.clr_type_lv2) or (c.clr_type_lv2 is null and a.clr_type_lv2 is null))
                                                    And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                     ) 医疗费总额,
                                                    sum(case when FUND_PAY_TYPE in ('310100','390100','510100') then fund_payamt else 0 end) 基本医疗保险统筹基金支付,
                                                    sum(case when FUND_PAY_TYPE in ('310100','390100','510100') and fund_payamt>10000 then fund_payamt-10000 else 0 end) 基本医疗超1万以上金额,
                                                    sum(case when FUND_PAY_TYPE in ('310200','390200','390300','310300') then fund_payamt else 0 end) 大病基金支付,

                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440699' and insutype = '310' then fund_payamt else 0 end) 公务员基金支付_市直,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440604' and insutype = '310' then fund_payamt else 0 end) 公务员基金支付_禅城区,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440605' and insutype = '310' then fund_payamt else 0 end) 公务员基金支付_南海区,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440607' and insutype = '310' then fund_payamt else 0 end) 公务员基金支付_三水区,
                                                    sum(case when FUND_PAY_TYPE='320100' and insu_optins='440608' and insutype = '310' then fund_payamt else 0 end) 公务员基金支付_高明区,

                                                    sum(case when FUND_PAY_TYPE='340100' and insu_optins='440699' and insutype = '340' then fund_payamt else 0 end) 离休医疗保障_市直,
                                                    sum(case when FUND_PAY_TYPE='340100' and insu_optins='440604' and insutype = '340' then fund_payamt else 0 end) 离休医疗保障_禅城区,
                                                    sum(case when FUND_PAY_TYPE='340100' and insu_optins='440605' and insutype = '340' then fund_payamt else 0 end) 离休医疗保障_南海区,
                                                    sum(case when FUND_PAY_TYPE='340100' and insu_optins='440606' and insutype = '340' then fund_payamt else 0 end) 离休医疗保障_顺德区,
                                                    sum(case when FUND_PAY_TYPE='340100' and insu_optins='440607' and insutype = '340' then fund_payamt else 0 end) 离休医疗保障_三水区,
                                                    sum(case when FUND_PAY_TYPE='340100' and insu_optins='440608' and insutype = '340' then fund_payamt else 0 end) 离休医疗保障_高明区,

                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440699' then fund_payamt else 0 end) 医疗救助基金支付_市直,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440604' then fund_payamt else 0 end) 医疗救助基金支付_禅城区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440605' then fund_payamt else 0 end) 医疗救助基金支付_南海区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440606' then fund_payamt else 0 end) 医疗救助基金支付_顺德区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440607' then fund_payamt else 0 end) 医疗救助基金支付_三水区,
                                                    sum(case when FUND_PAY_TYPE='610100' and insu_optins='440608' then fund_payamt else 0 end) 医疗救助基金支付_高明区,
                                                    sum(case when FUND_PAY_TYPE='610200' then fund_payamt else 0 end) 优抚医疗补助,
                                                    (select nvl(sum(c.psn_pay),0)  From GDSI_DZ_SETLINFO c 
                                                    Where REFD_SETL_FLAG='0'
                                                    And c.clr_way = a.clr_way
                                                    And c.clr_type = a.clr_type
                                                    And ((c.clr_type_lv2 = a.clr_type_lv2) or (c.clr_type_lv2 is null and a.clr_type_lv2 is null))
                                                    And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                     ) + sum(case when FUND_PAY_TYPE in ('620101','620100') then fund_payamt else 0 end)   个人支付,
                                                    --职工的离休不能算
                                                    nvl(sum((case when FUND_PAY_TYPE='340100'  and insutype != '340' then 0 else fund_payamt end)),0)  基金支付总额
                                                    From GDSI_DZ_SETLINFO a left join GDSI_DZ_SETLDETAIL b on a.mdtrt_id = b.mdtrt_id and a.setl_id = b.setl_id
                                                    Where REFD_SETL_FLAG='0'
                                                    And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                    group by clr_way,clr_type,clr_type_lv2
                                                    ";//10
        #endregion
        #endregion

        #region 报表明细SQL
        #region sql_310_11_MX
        private string sql_310_11_MX = @"--1佛山市基本医疗保险医疗费用现场结算月结统计表(职工门诊)      
                                                    Select insu_optins ,--参保人所在区
                                                        CERTNO,--身份证
                                                        PSN_NAME,--姓名
                                                        mdtrt_id,--就诊id
                                                        setl_id,--结算id
                                                        SETL_TIME,--就诊开始时间
                                                        BEGNDATE,--就诊结束时间
                                                        ENDDATE,--结算时间（经办时间）
                                                        MED_TYPE,--业务类别
                                                        CLR_WAY,--清算方式
                                                        CLR_TYPE,--清算类别
                                                        CLR_TYPE_LV2,--二级清算类别
                                                        birctrl_type,--生育类型
                                                        MEDFEE_SUMAMT,--医疗总费用
                                                        (select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                        and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='310100')  HIFP_PAY,--基本医疗保险统筹基金支付
                                                        (select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                        and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('310300','390200')) HIFMI_PAY,--大病基金支付
                                                        (select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                        and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='320100') CVLSERV_PAY,--公务员基金支付
                                                        (select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                        and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '340100' ) OTHFUND_PAY_LX,--离休医疗保障
                                                        (select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                        and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610100' ) MAF_PAY,--医疗救助基金支付
                                                        (select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                        and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610200' ) OTHFUND_PAY_YF,--优抚医疗补助
                                                        (select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                        and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')) Hifes_pay,--企业基金
                                                        FUND_PAY_SUMAMT - nvl((select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                        and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')),0) ,--基金支付总额
                                                        medfee_sumamt-fund_pay_sumamt,--个人支付
                                                        '' as MAF_PAY_optins --医疗救助所在区
                                                        From GDSI_DZ_SETLINFO a
                                                        Where REFD_SETL_FLAG='0'
                                                        And insutype='310'
                                                        And clr_type='11'
                                                        And clr_type_lv2 in('3102','3103','999950')
                                                        And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                        and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')";//1
        #endregion
        #region sql_390_11_MX
        private string sql_390_11_MX = @"--2佛山市基本医疗保险医疗费用现场结算月结统计表(居民门诊)     
                                                Select insu_optins ,--参保人所在区
													CERTNO,--身份证
													PSN_NAME,--姓名
													mdtrt_id,--就诊id
													setl_id,--结算id
													SETL_TIME,--就诊开始时间
													BEGNDATE,--就诊结束时间
													ENDDATE,--结算时间（经办时间）
													MED_TYPE,--业务类别
													CLR_WAY,--清算方式
													CLR_TYPE,--清算类别
													CLR_TYPE_LV2,--二级清算类别
                                                        birctrl_type,--生育类型
													MEDFEE_SUMAMT,--医疗总费用
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='390100')  HIFP_PAY,--基本医疗保险统筹基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('310300','390200')) HIFMI_PAY,--大病基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='320100') CVLSERV_PAY,--公务员基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '340100' ) OTHFUND_PAY_LX,--离休医疗保障
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610100' ) MAF_PAY,--医疗救助基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610200' ) OTHFUND_PAY_YF,--优抚医疗补助
                                                    (select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')) Hifes_pay,--企业基金
                                                    FUND_PAY_SUMAMT - nvl((select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')),0) ,--基金支付总额
													medfee_sumamt-fund_pay_sumamt,--个人支付
													'' as MAF_PAY_optins --医疗救助所在区
                                                    From GDSI_DZ_SETLINFO a
                                                    Where REFD_SETL_FLAG='0'
                                                    And insutype='390'
                                                    And clr_type='11'
                                                    And clr_type_lv2 in('3901','3902','999951')
                                                    And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                    ";//2
        #endregion
        #region sql_310_11_XG_MX
        private string sql_310_11_XG_MX = @"--7佛山市基本医疗保险医疗费用现场结算月结统计表(职工新冠)  
                                                Select insu_optins ,--参保人所在区
													CERTNO,--身份证
													PSN_NAME,--姓名
													mdtrt_id,--就诊id
													setl_id,--结算id
													SETL_TIME,--就诊开始时间
													BEGNDATE,--就诊结束时间
													ENDDATE,--结算时间（经办时间）
													MED_TYPE,--业务类别
													CLR_WAY,--清算方式
													CLR_TYPE,--清算类别
													CLR_TYPE_LV2,--二级清算类别
                                                        birctrl_type,--生育类型
													MEDFEE_SUMAMT,--医疗总费用
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='310100')  HIFP_PAY,--基本医疗保险统筹基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('310300','390200')) HIFMI_PAY,--大病基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='320100') CVLSERV_PAY,--公务员基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '340100' ) OTHFUND_PAY_LX,--离休医疗保障
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610100' ) MAF_PAY,--医疗救助基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610200' ) OTHFUND_PAY_YF,--优抚医疗补助
                                                    (select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')) Hifes_pay,--企业基金
                                                    FUND_PAY_SUMAMT - nvl((select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')),0) ,--基金支付总额
													medfee_sumamt-fund_pay_sumamt,--个人支付
													'' as MAF_PAY_optins --医疗救助所在区
                                                    From GDSI_DZ_SETLINFO a
                                                    Where REFD_SETL_FLAG='0'
                                                    
                                                    And insutype='310'
                                                    And clr_type='11'
                                                    And clr_type_lv2 in('999903','999905','999948')
                                                    And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                    ";//7
        #endregion
        #region sql_390_11_XG_MX
        private string sql_390_11_XG_MX = @"--8佛山市基本医疗保险医疗费用现场结算月结统计表(居民新冠)    
                                                Select insu_optins ,--参保人所在区
													CERTNO,--身份证
													PSN_NAME,--姓名
													mdtrt_id,--就诊id
													setl_id,--结算id
													SETL_TIME,--就诊开始时间
													BEGNDATE,--就诊结束时间
													ENDDATE,--结算时间（经办时间）
													MED_TYPE,--业务类别
													CLR_WAY,--清算方式
													CLR_TYPE,--清算类别
													CLR_TYPE_LV2,--二级清算类别
                                                        birctrl_type,--生育类型
													MEDFEE_SUMAMT,--医疗总费用
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='390100')  HIFP_PAY,--基本医疗保险统筹基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('310300','390200')) HIFMI_PAY,--大病基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='320100') CVLSERV_PAY,--公务员基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '340100' ) OTHFUND_PAY_LX,--离休医疗保障
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610100' ) MAF_PAY,--医疗救助基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610200' ) OTHFUND_PAY_YF,--优抚医疗补助
                                                    (select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')) Hifes_pay,--企业基金
                                                    FUND_PAY_SUMAMT - nvl((select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')),0) ,--基金支付总额
													medfee_sumamt-fund_pay_sumamt,--个人支付
													'' as MAF_PAY_optins --医疗救助所在区
                                                    From GDSI_DZ_SETLINFO a
                                                    Where REFD_SETL_FLAG='0'
                                                    
                                                    And insutype='390'
                                                    And clr_type='11'
                                                    And clr_type_lv2 in('999904','999906','999949')
                                                    And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                    ";//8
        #endregion

        #region sql_310_21_DRG_MX
        private string sql_310_21_DRG_MX = @"--3佛山市基本医疗保险医疗费用现场结算月结统计表(职工DRG住院)  
                                                Select insu_optins ,--参保人所在区
													CERTNO,--身份证
													PSN_NAME,--姓名
													mdtrt_id,--就诊id
													setl_id,--结算id
													SETL_TIME,--就诊开始时间
													BEGNDATE,--就诊结束时间
													ENDDATE,--结算时间（经办时间）
													MED_TYPE,--业务类别
													CLR_WAY,--清算方式
													CLR_TYPE,--清算类别
													CLR_TYPE_LV2,--二级清算类别
                                                        birctrl_type,--生育类型
													MEDFEE_SUMAMT,--医疗总费用
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='310100')  HIFP_PAY,--基本医疗保险统筹基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('310300','390200')) HIFMI_PAY,--大病基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='320100') CVLSERV_PAY,--公务员基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '340100' ) OTHFUND_PAY_LX,--离休医疗保障
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610100' ) MAF_PAY,--医疗救助基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610200' ) OTHFUND_PAY_YF,--优抚医疗补助
                                                    (select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')) Hifes_pay,--企业基金
                                                    FUND_PAY_SUMAMT - nvl((select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')),0) ,--基金支付总额
													medfee_sumamt-fund_pay_sumamt,--个人支付
													'' as MAF_PAY_optins --医疗救助所在区
                                                    From GDSI_DZ_SETLINFO a
                                                    Where REFD_SETL_FLAG='0'
                                                    
                                                    And insutype='310'
                                                    And clr_type='21'
                                                    And clr_type_lv2='3104'
                                                    And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                    ";//3
        #endregion
        #region sql_390_21_DRG_MX
        private string sql_390_21_DRG_MX = @"--4佛山市基本医疗保险医疗费用现场结算月结统计表(居民DRG住院)  
                                                Select insu_optins ,--参保人所在区
													CERTNO,--身份证
													PSN_NAME,--姓名
													mdtrt_id,--就诊id
													setl_id,--结算id
													SETL_TIME,--就诊开始时间
													BEGNDATE,--就诊结束时间
													ENDDATE,--结算时间（经办时间）
													MED_TYPE,--业务类别
													CLR_WAY,--清算方式
													CLR_TYPE,--清算类别
													CLR_TYPE_LV2,--二级清算类别
                                                        birctrl_type,--生育类型
													MEDFEE_SUMAMT,--医疗总费用
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='390100')  HIFP_PAY,--基本医疗保险统筹基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('310300','390200')) HIFMI_PAY,--大病基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='320100') CVLSERV_PAY,--公务员基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '340100' ) OTHFUND_PAY_LX,--离休医疗保障
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610100' ) MAF_PAY,--医疗救助基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610200' ) OTHFUND_PAY_YF,--优抚医疗补助
                                                    (select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')) Hifes_pay,--企业基金
                                                    FUND_PAY_SUMAMT - nvl((select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')),0) ,--基金支付总额
													medfee_sumamt-fund_pay_sumamt,--个人支付
													'' as MAF_PAY_optins --医疗救助所在区
                                                    From GDSI_DZ_SETLINFO a
                                                    Where REFD_SETL_FLAG='0'
                                                    
                                                    And insutype='390'
                                                    And clr_type='21'
                                                    And clr_type_lv2='3903'
                                                    And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                    ";//4
        #endregion
        #region sql_310_21_FDRG_MX  佛山以DRG的形式上传
        private string sql_310_21_FDRG_MX = @"--5佛山市基本医疗保险医疗费用现场结算月结统计表(职工非DRG住院)  
                                                Select insu_optins ,--参保人所在区
													CERTNO,--身份证
													PSN_NAME,--姓名
													mdtrt_id,--就诊id
													setl_id,--结算id
													SETL_TIME,--就诊开始时间
													BEGNDATE,--就诊结束时间
													ENDDATE,--结算时间（经办时间）
													MED_TYPE,--业务类别
													CLR_WAY,--清算方式
													CLR_TYPE,--清算类别
													CLR_TYPE_LV2,--二级清算类别
                                                        birctrl_type,--生育类型
													MEDFEE_SUMAMT,--医疗总费用
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='310100')  HIFP_PAY,--基本医疗保险统筹基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('310300','390200')) HIFMI_PAY,--大病基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='320100') CVLSERV_PAY,--公务员基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '340100' ) OTHFUND_PAY_LX,--离休医疗保障
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610100' ) MAF_PAY,--医疗救助基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610200' ) OTHFUND_PAY_YF,--优抚医疗补助
                                                    (select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')) Hifes_pay,--企业基金
                                                    FUND_PAY_SUMAMT - nvl((select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')),0) ,--基金支付总额
													medfee_sumamt-fund_pay_sumamt,--个人支付
													'' as MAF_PAY_optins --医疗救助所在区
                                                    From GDSI_DZ_SETLINFO a
                                                    Where REFD_SETL_FLAG='0'
                                                    
                                                    And insutype='310'
                                                    And clr_type='21'
                                                    And clr_type_lv2 in('999932','999907')
                                                    And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                    ";//5
        #endregion
        #region sql_390_21_FDRG_MX  佛山以DRG的形式上传
        private string sql_390_21_FDRG_MX = @"--6佛山市基本医疗保险医疗费用现场结算月结统计表(居民非DRG住院)  
                                                Select insu_optins ,--参保人所在区
													CERTNO,--身份证
													PSN_NAME,--姓名
													mdtrt_id,--就诊id
													setl_id,--结算id
													SETL_TIME,--就诊开始时间
													BEGNDATE,--就诊结束时间
													ENDDATE,--结算时间（经办时间）
													MED_TYPE,--业务类别
													CLR_WAY,--清算方式
													CLR_TYPE,--清算类别
													CLR_TYPE_LV2,--二级清算类别
                                                        birctrl_type,--生育类型
													MEDFEE_SUMAMT,--医疗总费用
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='390100')  HIFP_PAY,--基本医疗保险统筹基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('310300','390200')) HIFMI_PAY,--大病基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='320100') CVLSERV_PAY,--公务员基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '340100' ) OTHFUND_PAY_LX,--离休医疗保障
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610100' ) MAF_PAY,--医疗救助基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610200' ) OTHFUND_PAY_YF,--优抚医疗补助
                                                    (select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')) Hifes_pay,--企业基金
                                                    FUND_PAY_SUMAMT - nvl((select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')),0) ,--基金支付总额
													medfee_sumamt-fund_pay_sumamt,--个人支付
													'' as MAF_PAY_optins --医疗救助所在区
                                                    From GDSI_DZ_SETLINFO a
                                                    Where REFD_SETL_FLAG='0'
                                                    
                                                    And insutype='390'
                                                    And clr_type='21'
                                                    And clr_type_lv2 in('999933','999908')
                                                    And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                    ";//6
        #endregion

        #region sql_310_SY_MX
        private string sql_310_SY_MX = @"--9佛山市基本医疗保险医疗费用现场结算月结统计表(生育清算)    
                                                Select insu_optins ,--参保人所在区
													CERTNO,--身份证
													PSN_NAME,--姓名
													mdtrt_id,--就诊id
													setl_id,--结算id
													SETL_TIME,--就诊开始时间
													BEGNDATE,--就诊结束时间
													ENDDATE,--结算时间（经办时间）
													MED_TYPE,--业务类别
													CLR_WAY,--清算方式
													CLR_TYPE,--清算类别
													CLR_TYPE_LV2,--二级清算类别
                                                        birctrl_type,--生育类型
													MEDFEE_SUMAMT,--医疗总费用
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='510100')  HIFP_PAY,--基本医疗保险统筹基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('310300','390200')) HIFMI_PAY,--大病基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='320100') CVLSERV_PAY,--公务员基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '340100' ) OTHFUND_PAY_LX,--离休医疗保障
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610100' ) MAF_PAY,--医疗救助基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610200' ) OTHFUND_PAY_YF,--优抚医疗补助
                                                    (select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')) Hifes_pay,--企业基金
                                                    FUND_PAY_SUMAMT - nvl((select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')),0) ,--基金支付总额
													medfee_sumamt-fund_pay_sumamt,--个人支付
													'' as MAF_PAY_optins --医疗救助所在区
                                                    From GDSI_DZ_SETLINFO a
                                                    Where REFD_SETL_FLAG='0'
                                                    
                                                    And insutype='310'
                                                    And med_type in('51','52')
                                                    And ((matnEnddate >=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                            and matnEnddate <to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                            and med_type ='51')
                                                            or
                                                            (setl_time >=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                            and setl_time <to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                            and med_type ='52')
                                                            )
                                                    ";//9
        #endregion
        #region sql_310_SYMZ_MX
        private string sql_310_SYMZ_MX = @"--9佛山市基本医疗保险医疗费用现场结算月结统计表(生育清算)    
                                                Select insu_optins ,--参保人所在区
													CERTNO,--身份证
													PSN_NAME,--姓名
													mdtrt_id,--就诊id
													setl_id,--结算id
													SETL_TIME,--就诊开始时间
													BEGNDATE,--就诊结束时间
													ENDDATE,--结算时间（经办时间）
													MED_TYPE,--业务类别
													CLR_WAY,--清算方式
													CLR_TYPE,--清算类别
													CLR_TYPE_LV2,--二级清算类别
                                                        birctrl_type,--生育类型
													MEDFEE_SUMAMT,--医疗总费用
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='510100')  HIFP_PAY,--基本医疗保险统筹基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('310300','390200')) HIFMI_PAY,--大病基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='320100') CVLSERV_PAY,--公务员基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '340100' ) OTHFUND_PAY_LX,--离休医疗保障
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610100' ) MAF_PAY,--医疗救助基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610200' ) OTHFUND_PAY_YF,--优抚医疗补助
                                                    (select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')) Hifes_pay,--企业基金
                                                    FUND_PAY_SUMAMT - nvl((select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')),0) ,--基金支付总额
													medfee_sumamt-fund_pay_sumamt,--个人支付
													'' as MAF_PAY_optins --医疗救助所在区
                                                    From GDSI_DZ_SETLINFO a
                                                    Where REFD_SETL_FLAG='0'
                                                    
                                                    And insutype='310'
                                                    And med_type = '51'
                                                    And ((matnEnddate >=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                            and matnEnddate <to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                            and med_type ='51')
                                                            )
                                                    ";//9
        #endregion
        #region sql_310_SYZY_MX
        private string sql_310_SYZY_MX = @"--9佛山市基本医疗保险医疗费用现场结算月结统计表(生育清算)    
                                                Select insu_optins ,--参保人所在区
													CERTNO,--身份证
													PSN_NAME,--姓名
													mdtrt_id,--就诊id
													setl_id,--结算id
													SETL_TIME,--就诊开始时间
													BEGNDATE,--就诊结束时间
													ENDDATE,--结算时间（经办时间）
													MED_TYPE,--业务类别
													CLR_WAY,--清算方式
													CLR_TYPE,--清算类别
													CLR_TYPE_LV2,--二级清算类别
                                                        birctrl_type,--生育类型
													MEDFEE_SUMAMT,--医疗总费用
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='510100')  HIFP_PAY,--基本医疗保险统筹基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('310300','390200')) HIFMI_PAY,--大病基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='320100') CVLSERV_PAY,--公务员基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '340100' ) OTHFUND_PAY_LX,--离休医疗保障
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610100' ) MAF_PAY,--医疗救助基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610200' ) OTHFUND_PAY_YF,--优抚医疗补助
                                                    (select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')) Hifes_pay,--企业基金
                                                    FUND_PAY_SUMAMT - nvl((select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')),0) ,--基金支付总额
													medfee_sumamt-fund_pay_sumamt,--个人支付
													'' as MAF_PAY_optins --医疗救助所在区
                                                    From GDSI_DZ_SETLINFO a
                                                    Where REFD_SETL_FLAG='0'
                                                    
                                                    And insutype='310'
                                                    And med_type = '52'
                                                    And (
                                                            (setl_time >=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                            and setl_time <to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                            and med_type ='52')
                                                            )
                                                    ";//9
        #endregion

        #region sql_340_LX_MX
        private string sql_340_LX_MX = @"--10佛山市基本医疗保险医疗费用现场结算月结统计表(离休)   
                                                Select insu_optins ,--参保人所在区
													CERTNO,--身份证
													PSN_NAME,--姓名
													mdtrt_id,--就诊id
													setl_id,--结算id
													SETL_TIME,--就诊开始时间
													BEGNDATE,--就诊结束时间
													ENDDATE,--结算时间（经办时间）
													MED_TYPE,--业务类别
													CLR_WAY,--清算方式
													CLR_TYPE,--清算类别
													CLR_TYPE_LV2,--二级清算类别
                                                        birctrl_type,--生育类型
													MEDFEE_SUMAMT,--医疗总费用
													0 HIFP_PAY,--基本医疗保险统筹基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('310300','390200')) HIFMI_PAY,--大病基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE ='320100') CVLSERV_PAY,--公务员基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '340100' ) OTHFUND_PAY_LX,--离休医疗保障
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610100' ) MAF_PAY,--医疗救助基金支付
													(select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
													and a.setl_id = b.setl_id and b.FUND_PAY_TYPE = '610200' ) OTHFUND_PAY_YF,--优抚医疗补助
                                                    (select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')) Hifes_pay,--企业基金
                                                    FUND_PAY_SUMAMT - nvl((select sum(b.fund_payamt) from GDSI_DZ_SETLDETAIL b where a.mdtrt_id = b.mdtrt_id 
                                                    and a.setl_id = b.setl_id and b.FUND_PAY_TYPE in ('620101','620100')),0) ,--基金支付总额
													medfee_sumamt-fund_pay_sumamt,--个人支付
													'' as MAF_PAY_optins --医疗救助所在区
                                                    From GDSI_DZ_SETLINFO a
                                                    Where REFD_SETL_FLAG='0'
                                                    
                                                    And insutype='340'
                                                    And setl_time>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                                                    and setl_time<to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                                                    ";//10
        #endregion
        #endregion

        #region 查询本地结算信息
        public System.Data.DataTable QuerySIdetail(string mdtrt_id, string setl_id, string certno, string setl_time)
        {
            string sql = @"select 
                                decode(s.mdtrt_id,'{0}','就诊ID相同','就诊ID不同')|| '|' 
                                ||decode(s.setl_id,'{1}','结算ID相同','结算ID不同')备注,
                                s.mdtrt_id ,--就诊id 
                                s.setl_id ,--结算id 
                                s.psn_no ,--人员编号 
                                s.valid_flag ,--1 有效 0 作废 
                                s.balance_state ,--1 结算 0 未结算 
                                s.inpatient_no,--住院流水号 
                                s.invoice_no ,--发票号 
                                s.patient_no ,--住院号 
                                s.card_no ,--就诊卡号 
                                s.name ,--姓名 
                                s.sex_code ,--性别 
                                s.idenno ,--身份证号 
                                s.type_code ,--结算分类1-门诊2-住院 
                                decode(s.type_code,'1',s.oper_date,s.balance_date) ,--结算时间 
                                s.med_type ,--医疗类别 
                                s.medfee_sumamt ,--医疗费总额 
                                s.fund_pay_sumamt  ,--基金支付总额 
                                s.psn_part_am --个人负担总金额 
                                from fin_ipr_siinmaininfo s 
                                where 1=1 and s.mdtrt_id is not null and s.setl_id is not null
                                and s.mdtrt_id = '{0}' 

                                union  --用or太慢了

                                select 
                                decode(s.mdtrt_id,'{0}','就诊ID相同','就诊ID不同')|| '|' 
                                ||decode(s.setl_id,'{1}','结算ID相同','结算ID不同')备注,
                                s.mdtrt_id ,--就诊id 
                                s.setl_id ,--结算id 
                                s.psn_no ,--人员编号 
                                s.valid_flag ,--1 有效 0 作废 
                                s.balance_state ,--1 结算 0 未结算 
                                s.inpatient_no,--住院流水号 
                                s.invoice_no ,--发票号 
                                s.patient_no ,--住院号 
                                s.card_no ,--就诊卡号 
                                s.name ,--姓名 
                                s.sex_code ,--性别 
                                s.idenno ,--身份证号 
                                s.type_code ,--结算分类1-门诊2-住院 
                                decode(s.type_code,'1',s.oper_date,s.balance_date) ,--结算时间 
                                s.med_type ,--医疗类别 
                                s.medfee_sumamt ,--医疗费总额 
                                s.fund_pay_sumamt  ,--基金支付总额 
                                s.psn_part_am --个人负担总金额 
                                from fin_ipr_siinmaininfo s 
                                where 1=1 and s.mdtrt_id is not null and s.setl_id is not null
                                and  s.setl_id  = '{1}'";
            sql = string.Format(sql, mdtrt_id, setl_id);
            if (!string.IsNullOrEmpty(certno) && !string.IsNullOrEmpty(setl_time))
            {
                sql += string.Format(@"  union  --用or太慢了
                                select 
                                decode(s.mdtrt_id,'{0}','就诊ID相同','就诊ID不同')|| '|' 
                                ||decode(s.setl_id,'{1}','结算ID相同','结算ID不同')备注,
                                s.mdtrt_id ,--就诊id 
                                s.setl_id ,--结算id 
                                s.psn_no ,--人员编号 
                                s.valid_flag ,--1 有效 0 作废 
                                s.balance_state ,--1 结算 0 未结算 
                                s.inpatient_no,--住院流水号 
                                s.invoice_no ,--发票号 
                                s.patient_no ,--住院号 
                                s.card_no ,--就诊卡号 
                                s.name ,--姓名 
                                s.sex_code ,--性别 
                                s.idenno ,--身份证号 
                                s.type_code ,--结算分类1-门诊2-住院 
                                decode(s.type_code,'1',s.oper_date,s.balance_date) ,--结算时间 
                                s.med_type ,--医疗类别 
                                s.medfee_sumamt ,--医疗费总额 
                                s.fund_pay_sumamt  ,--基金支付总额 
                                s.psn_part_am --个人负担总金额 
                                from fin_ipr_siinmaininfo s 
                                where 1=1  and s.mdtrt_id is not null and s.setl_id is not null
                                and (s.psn_no = '{0}' 
                                and decode(s.type_code,'1',s.oper_date,s.balance_date) >= to_date('{1}','yyyy-MM-dd hh24:mi:ss')-1
                                and decode(s.type_code,'1',s.oper_date,s.balance_date) < to_date('{1}','yyyy-MM-dd hh24:mi:ss')+1)"
                                                   , certno, setl_time);
            }

            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sql, ref ds);
            return ds.Tables[0];
        }

        public System.Data.DataTable QueryFPdetail(ArrayList inPatientNOs, ArrayList invoiceNos, string certno, string setl_time, string clrType,string patientNo)
        {
            string sql = @"select 
                                    invoice_no ,--发票号 
                                    card_no ,--病历卡号 
                                    clinic_code ,--挂号流水号 
                                    name ,--患者姓名 
                                    paykind_code ,--合同单位代码 
                                    pact_name ,--合同单位名称 
                                    reg_date ,--挂号日期 
                                    tot_cost ,--总额 
                                    pub_cost ,--可报效金额 
                                    own_cost ,--不可报效金额 
                                    pay_cost ,--自付金额 
                                    oper_code ,--结算人 
                                    oper_date ,--结算时间 
                                    cancel_flag --0 退费 1 有效 2 重打 3 注销 
                                    from fin_opb_invoiceinfo 
                                    where  clinic_code in ('{0}') 
    
                                    union 

                                    select 
                                    invoice_no ,--发票号 
                                    card_no ,--病历卡号 
                                    clinic_code ,--挂号流水号 
                                    name ,--患者姓名 
                                    paykind_code ,--合同单位代码 
                                    pact_name ,--合同单位名称 
                                    reg_date ,--挂号日期 
                                    tot_cost ,--总额 
                                    pub_cost ,--可报效金额 
                                    own_cost ,--不可报效金额 
                                    pay_cost ,--自付金额 
                                    oper_code ,--结算人 
                                    oper_date ,--结算时间 
                                    cancel_flag --0 退费 1 有效 2 重打 3 注销 
                                    from fin_opb_invoiceinfo 
                                    where  invoice_no in ('{1}') 
                                    ";

            if (clrType == "2")
            {
                sql = @"select 
                              b.invoice_no ,--发票号 
                              i.card_no ,--病历卡号 
                              b.inpatient_no  ,--挂号流水号 
                              i.name ,--患者姓名 
                              b.paykind_code ,--合同单位代码 
                              b.pact_code ,--合同单位名称 
                              i.in_date ,--挂号日期 
                              b.tot_cost ,--总额 
                              b.pub_cost ,--可报效金额 
                              b.own_cost ,--不可报效金额 
                              b.pay_cost ,--自付金额 
                              b.balance_opercode ,--结算人 
                              b.balance_date ,--结算时间 
                              b.waste_flag --0 退费 1 有效 2 重打 3 注销 
                              from fin_ipb_balancehead b
                              left join fin_ipr_inmaininfo i on b.inpatient_no = i.inpatient_no
                              where  b.inpatient_no  in ('{0}') 

                            union 
                            select 
                              b.invoice_no ,--发票号 
                              i.card_no ,--病历卡号 
                              b.inpatient_no  ,--挂号流水号 
                              i.name ,--患者姓名 
                              b.paykind_code ,--合同单位代码 
                              b.pact_code ,--合同单位名称 
                              i.in_date ,--挂号日期 
                              b.tot_cost ,--总额 
                              b.pub_cost ,--可报效金额 
                              b.own_cost ,--不可报效金额 
                              b.pay_cost ,--自付金额 
                              b.balance_opercode ,--结算人 
                              b.balance_date ,--结算时间 
                              b.waste_flag --0 退费 1 有效 2 重打 3 注销 
                              from fin_ipb_balancehead b
                              left join fin_ipr_inmaininfo i on b.inpatient_no = i.inpatient_no
                              where  b.invoice_no in ('{1}') 
                                    ";
            }

            string inPatientNOList = string.Empty;
            if (inPatientNOs != null && inPatientNOs.Count > 0)
            {
                foreach (string inpatient in inPatientNOs)
                {
                    inPatientNOList += "'" + inpatient + "',";
                }
                inPatientNOList = inPatientNOList.Substring(1, inPatientNOList.Length - 3);
            }

            string invoiceNoList = string.Empty;
            if (invoiceNos != null && invoiceNos.Count > 0)
            {
                foreach (string invoiceNo in invoiceNos)
                {
                    invoiceNoList += "'" + invoiceNo + "',";
                }
                invoiceNoList = invoiceNoList.Substring(1, invoiceNoList.Length - 3);
            }

            sql = string.Format(sql, inPatientNOList, invoiceNoList);
            if ((!string.IsNullOrEmpty(certno) || !string.IsNullOrEmpty(patientNo) ) && !string.IsNullOrEmpty(setl_time))
            {
                if (clrType == "1")
                {
                    sql += string.Format(@" union
                                    select 
                                    invoice_no ,--发票号 
                                    card_no ,--病历卡号 
                                    clinic_code ,--挂号流水号 
                                    name ,--患者姓名 
                                    paykind_code ,--合同单位代码 
                                    pact_name ,--合同单位名称 
                                    reg_date ,--挂号日期 
                                    tot_cost ,--总额 
                                    pub_cost ,--可报效金额 
                                    own_cost ,--不可报效金额 
                                    pay_cost ,--自付金额 
                                    oper_code ,--结算人 
                                    oper_date ,--结算时间 
                                    cancel_flag --0 退费 1 有效 2 重打 3 注销 
                                    from fin_opb_invoiceinfo 
                                    where   (  card_no in (select p.card_no from com_patientinfo p where p.idenno = '{0}')
                                                                      and oper_date >= to_date('{1}','yyyy-MM-dd hh24:mi:ss') -1
                                                                      and oper_date < to_date('{1}','yyyy-MM-dd hh24:mi:ss')+1)"
                                                      , certno, setl_time);
                }
                else
                {
                    sql += string.Format(@"  union 
                                select 
                              b.invoice_no ,--发票号 
                              i.card_no ,--病历卡号 
                              b.inpatient_no  ,--挂号流水号 
                              i.name ,--患者姓名 
                              b.paykind_code ,--合同单位代码 
                              b.pact_code ,--合同单位名称 
                              i.in_date ,--挂号日期 
                              b.tot_cost ,--总额 
                              b.pub_cost ,--可报效金额 
                              b.own_cost ,--不可报效金额 
                              b.pay_cost ,--自付金额 
                              b.balance_opercode ,--结算人 
                              b.balance_date ,--结算时间 
                              b.waste_flag --0 退费 1 有效 2 重打 3 注销 
                              from fin_ipb_balancehead b
                              left join fin_ipr_inmaininfo i on b.inpatient_no = i.inpatient_no
                              where (  i.card_no in (select p.card_no from com_patientinfo p where p.idenno = '{0}')
                                            or i.patient_no = '{2}'
                                                                      --and b.balance_date >= to_date('{1}','yyyy-MM-dd hh24:mi:ss') -180
                                                                      --and b.balance_date < to_date('{1}','yyyy-MM-dd hh24:mi:ss')+30
                                                                      )"
                                                         , certno, setl_time,patientNo);
                }

            }
            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sql, ref ds);
            return ds.Tables[0];
        }

        /// <summary>
        /// 查找患者卡号
        /// </summary>
        /// <returns></returns>
        public string GetCardNOByInpatientID(string inpatientID)
        {
            string sql = "select i.card_no from fin_ipr_inmaininfo i where i.inpatient_no = '{0}'";
            sql = string.Format(sql, inpatientID);
            string res = this.ExecSqlReturnOne(sql);
            return res;
        }

        /// <summary>
        /// 查询合同单位名称
        /// </summary>
        /// <returns></returns>
        public string GetPactName(string pactID)
        {
            string sql = "select p.pact_name from fin_com_pactunitinfo p where p.pact_code = '{0}'";
            sql = string.Format(sql, pactID);
            string res = this.ExecSqlReturnOne(sql);
            return res;
        }
        #endregion

        #region 异常处理
        //作废医保记录
        public int cancelSIinfo(string mdtrt_id, string setl_id)
        {
            string strSql = @" update fin_ipr_siinmaininfo s
                                            set s.valid_flag = '0',s.own_cause = '对账无效'
                                            where s.mdtrt_id = '{0}' and s.setl_id = '{1}'
                                ";
            try
            {
                strSql = string.Format(strSql, mdtrt_id, setl_id);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        public int updateSIinfo(string mdtrt_id, string setl_id)
        {
            string strSql = @" update fin_ipr_siinmaininfo s
                                            set s.valid_flag = '1' , s.balance_state = '1',s.own_cause = '对账有效'
                                            where s.mdtrt_id = '{0}' and s.setl_id = '{1}'
                                ";
            try
            {
                strSql = string.Format(strSql, mdtrt_id, setl_id);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        public int updateSIinfoForCBD(string mdtrt_id, string setl_id, string insuplc_admdvs)
        {
            string strSql = @"  update fin_ipr_siinmaininfo s
                                          set s.insuplc_admdvs = '{2}',s.own_cause = '对账参保地调整'
                                          where s.mdtrt_id = '{0}' and s.setl_id = '{1}'
                                ";
            try
            {
                strSql = string.Format(strSql, mdtrt_id, setl_id, insuplc_admdvs);
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
        /// 检查是否保存成功
        /// </summary>
        /// <param name="Mdtrt_id"></param>
        /// <param name="Setl_id"></param>
        /// <returns></returns>
        public bool QueryIsSave(string Mdtrt_id, string Setl_id)
        {
            string strSql = @"select count(*) from fin_ipr_siinmaininfo sii
                        where sii.mdtrt_id = '{0}'
                        and sii.setl_id = '{1}'";

            strSql = string.Format(strSql, Mdtrt_id, Setl_id);

            string res = this.ExecSqlReturnOne(strSql);
            if (res == "-1" || string.IsNullOrEmpty(res))
            {
                return false;
            }
            if (int.Parse(res) > 0)
            {
                return true;
            }

            return false;


        }
        #endregion

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
                                   vali_flag
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
                                   mdtrtsn,
                                   name,
                                   gend,
                                   age,
                                   adm_rec_no,
                                   psn_adm_no,
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
                                   inout_dise_type,
                                   maindise_flag,
                                   dise_seq,
                                   dise_time,
                                   wm_dise_code,
                                   wm_dise_name,
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
                                   bedno,
                                   rcd_time,
                                   hfcomp,
                                   cas_ftur,
                                   tcm4d_rslt,
                                   dise_evid,
                                   prel_wm_dise_code,
                                   prel_wm_dise_name,
                                   prel_tcm_dise_code,
                                   prel_tcm_dise_name,
                                   prel_tcmsymp_code,
                                   prel_tcmsymp,
                                   finl_wm_dise_code,
                                   finl_wm_dise_name,
                                   finl_tcm_dise_code,
                                   finl_tcm_dise_name,
                                   finl_tcmsymp_code,
                                   finl_tcmsymp,
                                   dise_plan,
                                   prnp_trt,
                                   ipdr_code,
                                   ipdr_name,
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
                                   bfpn_inhosp_ifet,
                                   afpn_dise_code,
                                   afpn_dise_name,
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
                                   dise_name,
                                   dise_code,
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
                                   doc_name,
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
                                   vali_flag
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

        #region 核酸检测

        /// <summary>
        /// 结算核酸项目
        /// </summary>
        /// <returns></returns>
        public ArrayList GetNucleicItem()
        {
            string strSql = @"select y.code,y.name,y.mark from com_dictionary y where y.type = 'NucleicItem' and y.valid_state = '1'";
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
                    obj.Memo = Reader[2].ToString();
                    al.Add(obj);
                }

                return al;
            } //抛出错误
            catch (Exception ex)
            {
                Err = "获取结算核酸项目信息出错！" + ex.Message;
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
        /// 获取全部待结算明细
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public System.Data.DataTable QueryALLNeedBalanceBaseInfo(string beginTime, string endTime)
        {
            string sql = @"select * from fin_syb_hsbaseinfo o 
                            where o.state = '0' 
                            and o.oper_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
                            and o.oper_date <= to_date('{1}','yyyy-mm-dd hh24:mi:ss') order by to_number(ID) desc";
            sql = string.Format(sql, beginTime, endTime);
            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sql, ref ds);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取本地结算明细
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public System.Data.DataTable QueryALLBaseInfoLoacl(string beginTime, string endTime)
        {
            string sql = "";
            sql = string.Format(sql, beginTime, endTime);
            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sql, ref ds);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取全部已结算明细
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public System.Data.DataTable QueryALLBalanceInfo(string beginTime, string endTime)
        {
            string sql = @"
select i.ID,
       i.BALANCE_NO,
       i.FEE_TIMES,
       i.VALID_FLAG,
       i.MDTRT_ID,
       i.SETL_ID,
       i.PSN_NO,
       i.PSN_NAME,
       i.PSN_CERT_TYPE,
       i.CERTNO,
       i.GEND,
       i.NATY,
       i.BRDY,
       i.AGE,
       i.INSUTYPE,
       i.PSN_TYPE,
       i.CVLSERV_FLAG,
       i.SETL_TIME,
       i.MDTRT_CERT_TYPE,
       i.MED_TYPE,
       i.MEDFEE_SUMAMT,
       i.FULAMT_OWNPAY_AMT,
       i.OVERLMT_SELFPAY,
       i.PRESELFPAY_AMT,
       i.INSCP_SCP_AMT,
       i.ACT_PAY_DEDC,
       i.HIFP_PAY,
       i.POOL_PROP_SELFPAY,
       i.CVLSERV_PAY,
       i.HIFES_PAY,
       i.HIFMI_PAY,
       i.HIFOB_PAY,
       i.MAF_PAY,
       i.OTH_PAY,
       i.FUND_PAY_SUMAMT,
       i.PSN_PART_AMT,
       i.ACCT_PAY,
       i.PSN_CASH_PAY,
       i.HOSP_PART_AMT,
       i.BALC,
       i.ACCT_MULAID_PAY,
       i.MEDINS_SETL_ID,
       i.CLR_OPTINS,
       i.CLR_WAY,
       i.CLR_TYPE,
       i.OPER_CODE,
       i.OPER_DATE,
       o.CARD_TYPE,
       o.IDNO,
       o.NAME,
       o.SEE_DATE,
       o.CHECK_NUM,
       o.PRICE,
       o.TOT_COST,
       o.PATIENT_TYPE,
       o.STATE,
       o.CARD_NO,
       o.ERR,
       o.CLINIC_NO

  from fin_syb_hsbalanceinfo i, fin_syb_hsbaseinfo o
 where i.ID = o.id
   and i.VALID_FLAG = '1'
   and o.oper_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
   and o.oper_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
   and o.state = '1'order by to_number(i.ID) desc
";
            sql = string.Format(sql, beginTime, endTime);
            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sql, ref ds);
            if (ds == null || ds.Tables.Count <= 0)
            {
                return null;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 更新核酸状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateHSBalanceState(string id, string state, string err)
        {
            try
            {
                string strSql = @"update fin_syb_hsbaseinfo o
                                   set o.state = '{1}', o.err = '{2}'
                                 where o.id = '{0}'
                               ";
                strSql = string.Format(strSql, id, state, err);
                return this.ExecNoQuery(strSql);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// 删除核酸基本信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int DeleteHSBaseInfo(string id)
        {
            try
            {
                string strSql = @"delete from FIN_SYB_HSBASEINFO where id = '{0}'
                               ";
                strSql = string.Format(strSql, id);
                return this.ExecNoQuery(strSql);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// 删除核酸结算
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int DeleteHSBalanceinfo(string id)
        {
            try
            {
                string strSql = @"delete from fin_syb_hsbalanceinfo where id = '{0}'
                               ";
                strSql = string.Format(strSql, id);
                return this.ExecNoQuery(strSql);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// 判断是否已经结算
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool GetHSIsFee(string id)
        {
            try
            {
                string strSql = @"select count(*) from  fin_syb_hsbalanceinfo where id = '1' and VALID_FLAG = '1''
                               ";
                strSql = string.Format(strSql, id);
                string res = this.ExecSqlReturnOne(strSql);
                if (!string.IsNullOrEmpty(res) && int.Parse(res) > 0)
                {
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 更新核酸结算状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int UpdateHSBalanceValidFlag(string id, string mdtrt_id, string valid_flag)
        {
            try
            {
                string strSql = @"update fin_syb_hsbalanceinfo o
                                set o.valid_flag = '{2}'
                                where o.id = '{0}'
                                and o.mdtrt_id = '{1}'
                               ";
                strSql = string.Format(strSql, id, mdtrt_id, valid_flag);
                return this.ExecNoQuery(strSql);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }


        /// <summary>
        /// 插入核酸基本信息
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int InsertHSBaseInfo(FS.HISFC.Models.Registration.Register r)
        {
            try
            {
                string balSql = "select nvl(max(to_number(o.id)),0) + 1 from fin_syb_hsbaseinfo o";
                string id = this.ExecSqlReturnOne(balSql);
                if (id == "-1" || id == "0")
                {
                    id = "1";
                }

                string strSql = @"
insert into fin_syb_hsbaseinfo o
  (ID, --0 唯一编号
   CARD_TYPE, --1 证件类型
   IDNO, --2  证件号
   NAME, --3  姓名
   SEE_DATE, --4  就诊日期
   CHECK_NUM, --5 检测次数
   PRICE, --6 单价
   TOT_COST, --7  总金额
   PATIENT_TYPE, --8  核酸检测患者类型
   STATE, --9 标志：0导入，1结算（已上传）
   CARD_NO, --10  就诊卡号，体检的没有则为空
   ERR, --11  报错信息
   OPER_CODE, --12  操作人
   OPER_DATE, --13 操作时间
   CLINIC_NO,
   ITEMINFO,
   PATIENTY_TYPE,
   SEE_DOCT_CODE
   )
values
  ('{0}', --  唯一编号
   '{1}', -- 证件类型
   '{2}', -- 证件号
   '{3}', -- 姓名
   to_date('{4}', 'yyyy-mm-dd hh24:mi:ss'), --  就诊日期
   '{5}', -- 检测次数
   '{6}', -- 单价
   '{7}', -- 总金额
   '{8}', -- 核酸检测患者类型
   '{9}', -- 标志：0导入，1结算（已上传）
   '{10}', --  就诊卡号，体检的没有则为空
   '{11}', --  报错信息
   '{12}', --  操作人
   to_date('{13}', 'yyyy-mm-dd hh24:mi:ss'), -- 操作时间
    '{14}',
    '{15}',
    '{16}',
    '{17}'
   )

                               ";
                strSql = string.Format(strSql, id, r.IDCardType.ID, r.IDCard, r.Name, r.DoctorInfo.SeeDate.ToString(),//4
                    r.InTimes.ToString(), r.OwnCost.ToString(), r.PubCost.ToString(), r.PatientType, "0", r.PID.CardNO,//10
                    "", this.Operator.ID, this.GetSysDateTime(), r.ID, r.Insurance.Name, r.Insurance.Memo, r.DoctorInfo.Templet.Doct.ID//
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
        /// <summary>
        /// 插入结算信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InsertHSBalanceInfo(FS.HISFC.Models.Registration.Register r, Models.Response.ResponseGzsiModel2207 responseGdsiModel2207)
        {
            try
            {
                string balSql = "select nvl(max(to_number(o.balance_no)),0) + 1 from fin_syb_hsbalanceinfo o where o.id = '{0}'";
                balSql = string.Format(balSql, r.ID);
                string balNo = this.ExecSqlReturnOne(balSql);
                if (balNo == "-1" || balNo == "0")
                {
                    balNo = "1";
                }
                string strSql = @"
insert into fin_syb_hsbalanceinfo
  (ID, --0    核酸基本信息表的ID
   BALANCE_NO, --1    结算次数
   FEE_TIMES, --2    费用批次
   VALID_FLAG, --3    1 有效 0 作废
   MDTRT_ID, --4    就诊ID
   SETL_ID, --5    结算ID
   PSN_NO, --6    人员编号
   PSN_NAME, --7    人员姓名
   PSN_CERT_TYPE, --8    人员证件类型
   CERTNO, --9    证件号码
   GEND, --10    性别
   NATY, --11    民族
   BRDY, --12    出生日期
   AGE, --13    年龄
   INSUTYPE, --14    险种类型
   PSN_TYPE, --15    人员类别
   CVLSERV_FLAG, --16    公务员标志
   SETL_TIME, --17    结算时间
   MDTRT_CERT_TYPE, --18    就诊凭证类型
   MED_TYPE, --19    医疗类别
   MEDFEE_SUMAMT, --20    医疗费总额
   FULAMT_OWNPAY_AMT, --21    全自费金额
   OVERLMT_SELFPAY, --22    超限价自费费用
   PRESELFPAY_AMT, --23    先行自付金额
   INSCP_SCP_AMT, --24    符合政策范围金额
   ACT_PAY_DEDC, --25    实际支付起付线
   HIFP_PAY, --26    基本医疗保险统筹基金支出
   POOL_PROP_SELFPAY, --27    基本医疗保险统筹基金支付比例
   CVLSERV_PAY, --28    公务员医疗补助资金支出
   HIFES_PAY, --29    企业补充医疗保险基金支出
   HIFMI_PAY, --30    居民大病保险资金支出
   HIFOB_PAY, --31    职工大额医疗费用补助基金支出
   MAF_PAY, --32    医疗救助基金支出
   OTH_PAY, --33    其他支出
   FUND_PAY_SUMAMT, --34    基金支付总额
   PSN_PART_AMT, --35    个人负担总金额
   ACCT_PAY, --36    个人账户支出
   PSN_CASH_PAY, --37    个人现金支出
   HOSP_PART_AMT, --38    医院负担金额
   BALC, --39    余额
   ACCT_MULAID_PAY, --40    个人账户共济支付金额
   MEDINS_SETL_ID, --41    医药机构结算ID
   CLR_OPTINS, --42    清算经办机构
   CLR_WAY, --43   清算方式
   CLR_TYPE, --44   清算类别
   OPER_CODE, --45    操作人
   OPER_DATE --46    操作时间
   )
values
  ('{0}', --  核酸基本信息表的ID
   '{1}', -- 结算次数
   '{2}', -- 费用批次
   '{3}', -- 1 有效 0 作废
   '{4}', -- 就诊ID
   '{5}', -- 结算ID
   '{6}', -- 人员编号
   '{7}', -- 人员姓名
   '{8}', -- 人员证件类型
   '{9}', -- 证件号码
   '{10}', --  性别
   '{11}', --  民族
   to_date('{12}', 'yyyy-mm-dd hh24:mi:ss'), --  出生日期
   '{13}', --  年龄
   '{14}', --  险种类型
   '{15}', --  人员类别
   '{16}', --  公务员标志
   to_date('{17}', 'yyyy-mm-dd hh24:mi:ss'), --  结算时间
   '{18}', --  就诊凭证类型
   '{19}', --  医疗类别
   '{20}', --  医疗费总额
   '{21}', --  全自费金额
   '{22}', --  超限价自费费用
   '{23}', --  先行自付金额
   '{24}', --  符合政策范围金额
   '{25}', --  实际支付起付线
   '{26}', --  基本医疗保险统筹基金支出
   '{27}', --  基本医疗保险统筹基金支付比例
   '{28}', --  公务员医疗补助资金支出
   '{29}', --  企业补充医疗保险基金支出
   '{30}', --  居民大病保险资金支出
   '{31}', --  职工大额医疗费用补助基金支出
   '{32}', --  医疗救助基金支出
   '{33}', --  其他支出
   '{34}', --  基金支付总额
   '{35}', --  个人负担总金额
   '{36}', --  个人账户支出
   '{37}', --  个人现金支出
   '{38}', --  医院负担金额
   '{39}', --  余额
   '{40}', --  个人账户共济支付金额
   '{41}', --  医药机构结算ID
   '{42}', --  清算经办机构
   '{43}', --  清算方式
   '{44}', --  清算类别
   '{45}', --  操作人
   to_date('{46}', 'yyyy-mm-dd hh24:mi:ss') -- 操作时间
   )


                               ";
                strSql = string.Format(strSql, r.ID,//0   	核酸基本信息表的ID
                       balNo,//1   	结算次数
                        r.SIMainInfo.Mdtrt_id,//2   	费用批次
                        "1",//3   	1 有效 0 作废
                        responseGdsiModel2207.output.setlinfo.mdtrt_id,//4   	就诊ID
                        responseGdsiModel2207.output.setlinfo.setl_id,//5   	结算ID
                        r.SIMainInfo.Psn_no,//6   	人员编号
                        r.Name,//7   	人员姓名
                        responseGdsiModel2207.output.setlinfo.psn_cert_type,//8   	人员证件类型
                        responseGdsiModel2207.output.setlinfo.certno,//9   	证件号码
                        responseGdsiModel2207.output.setlinfo.gend,	//	GEND	//10   	性别
                        responseGdsiModel2207.output.setlinfo.naty,	//	NATY	//11   	民族
                        string.IsNullOrEmpty(responseGdsiModel2207.output.setlinfo.brdy) ? "" : Convert.ToDateTime(responseGdsiModel2207.output.setlinfo.brdy).ToString(),	//	BRDY	//12   	出生日期
                        responseGdsiModel2207.output.setlinfo.age,	//	AGE	//13   	年龄
                        responseGdsiModel2207.output.setlinfo.insutype,	//	INSUTYPE	//14   	险种类型
                        responseGdsiModel2207.output.setlinfo.psn_type,	//	PSN_TYPE	//15   	人员类别
                        responseGdsiModel2207.output.setlinfo.cvlserv_flag,	//	CVLSERV_FLAG	//16   	公务员标志
                        string.IsNullOrEmpty(responseGdsiModel2207.output.setlinfo.setl_time) ? DateTime.Now.ToString() : Convert.ToDateTime(responseGdsiModel2207.output.setlinfo.setl_time).ToString(),  //	SETL_TIME	//17   	结算时间
                        responseGdsiModel2207.output.setlinfo.mdtrt_cert_type,	//	MDTRT_CERT_TYPE	//18   	就诊凭证类型
                        responseGdsiModel2207.output.setlinfo.med_type,	//	MED_TYPE	//19   	医疗类别
                        responseGdsiModel2207.output.setlinfo.medfee_sumamt,	//	MEDFEE_SUMAMT	//20   	医疗费总额
                        responseGdsiModel2207.output.setlinfo.fulamt_ownpay_amt,	//	FULAMT_OWNPAY_AMT	//21   	全自费金额
                        responseGdsiModel2207.output.setlinfo.overlmt_selfpay,	//	OVERLMT_SELFPAY	//22   	超限价自费费用
                        responseGdsiModel2207.output.setlinfo.preselfpay_amt,	//	PRESELFPAY_AMT	//23   	先行自付金额
                        responseGdsiModel2207.output.setlinfo.inscp_scp_amt,	//	INSCP_SCP_AMT	//24   	符合政策范围金额
                        responseGdsiModel2207.output.setlinfo.act_pay_dedc,	//	ACT_PAY_DEDC	//25   	实际支付起付线
                        responseGdsiModel2207.output.setlinfo.hifp_pay,	//	HIFP_PAY	//26   	基本医疗保险统筹基金支出
                        responseGdsiModel2207.output.setlinfo.pool_prop_selfpay,	//	POOL_PROP_SELFPAY	//27   	基本医疗保险统筹基金支付比例
                        responseGdsiModel2207.output.setlinfo.cvlserv_pay,	//	CVLSERV_PAY	//28   	公务员医疗补助资金支出
                        responseGdsiModel2207.output.setlinfo.hifes_pay,	//	HIFES_PAY	//29   	企业补充医疗保险基金支出
                        responseGdsiModel2207.output.setlinfo.hifmi_pay,	//	HIFMI_PAY	//30   	居民大病保险资金支出
                        responseGdsiModel2207.output.setlinfo.hifob_pay,	//	HIFOB_PAY	//31   	职工大额医疗费用补助基金支出
                        responseGdsiModel2207.output.setlinfo.maf_pay,	//	MAF_PAY	//32   	医疗救助基金支出
                        responseGdsiModel2207.output.setlinfo.oth_pay,	//	OTH_PAY	//33   	其他支出
                        responseGdsiModel2207.output.setlinfo.fund_pay_sumamt,	//	FUND_PAY_SUMAMT	//34   	基金支付总额
                        responseGdsiModel2207.output.setlinfo.psn_part_amt,	//	PSN_PART_AMT	//35   	个人负担总金额
                        responseGdsiModel2207.output.setlinfo.acct_pay,	//	ACCT_PAY	//36   	个人账户支出
                        responseGdsiModel2207.output.setlinfo.psn_cash_pay,	//	PSN_CASH_PAY	//37   	个人现金支出
                        responseGdsiModel2207.output.setlinfo.hosp_part_amt,	//	HOSP_PART_AMT	//38   	医院负担金额
                        responseGdsiModel2207.output.setlinfo.balc,	//	BALC	//39   	余额
                        responseGdsiModel2207.output.setlinfo.acct_mulaid_pay,	//	ACCT_MULAID_PAY	//40   	个人账户共济支付金额
                        responseGdsiModel2207.output.setlinfo.medins_setl_id,	//	MEDINS_SETL_ID	//41   	医药机构结算ID
                        responseGdsiModel2207.output.setlinfo.clr_optins,	//	CLR_OPTINS	//42   	清算经办机构
                        responseGdsiModel2207.output.setlinfo.clr_way,	//	CLR_WAY	//43   	清算方式
                        responseGdsiModel2207.output.setlinfo.clr_type,	//	CLR_TYPE	//44   	清算类别
                        this.Operator.ID,	//	OPER_CODE	//45   	操作人
                        this.GetSysDateTime()	//	OPER_DATE	//46   	操作时间

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

        /// <summary>
        /// 获取对照的医保自定义码
        /// </summary>
        /// <param name="HisCode"></param>
        /// <param name="idno"></param>
        /// <param name="birthday"></param>
        /// <returns></returns>
        public string GetCenterCode(string HisCode, string idno, DateTime birthday)
        {
            int age = 7;
            if (!string.IsNullOrEmpty(idno) && idno.Length == 18)
            {
                int year = int.Parse(idno.Substring(6, 4));
                int month = int.Parse(idno.Substring(10, 2));
                int day = int.Parse(idno.Substring(12, 2));
                birthday = new DateTime(year, month, day);
            }
            if (!(birthday >= DateTime.MaxValue || birthday <= DateTime.MinValue))
            {
                DateTime nowDate = this.GetDateTimeFromSysDateTime();
                age = (int)((new TimeSpan(nowDate.Ticks - birthday.Ticks)).TotalDays / 365);
                if ((new TimeSpan(DateTime.Now.AddYears(-6).Ticks - birthday.Ticks)).TotalDays > 1)
                {
                    age += 1;
                }
            }

            string strSql = "";
            if (age <= 6)//暂时不考虑单价了，低于6岁的都取儿童项目编码
            {
                strSql = @"select nvl(CENTER_CODE_CHILD,center_code)
                                from fin_com_compare
                               where pacT_code='4'
                                 AND HIS_User_code ='{0}'";

            }
            else
            {
                strSql = @"select center_code
                                from fin_com_compare
                               where pacT_code='4'
                                 AND HIS_User_code ='{0}'";
            }

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

        public string GetCardNoByIDNO(string idno)
        {
            string sql = "select p.card_no from com_patientinfo p where p.idenno = '{0}'";
            sql = string.Format(sql, idno);

            string res = this.ExecSqlReturnOne(sql);

            if (res == "-1" || string.IsNullOrEmpty(res))
            {
                return "";//其他
            }
            return res;
        }

        /// <summary>
        /// 查找医护国家编码// {B9DC5FED-C702-451e-9A28-2A9E89A89127}
        /// </summary>
        /// <returns></returns>
        public string GetGDDoct(string doctID)
        {
            string sql = "select nvl(e.user_code,e.empl_code) from com_employee e where e.empl_code = '{0}'";
            sql = string.Format(sql, doctID);
            string res = this.ExecSqlReturnOne(sql);
            return res;
        }

        public string GetGDDept(string deptHIS)
        {
            string sql = "select t.bz_dept_code from com_department t where t.dept_code = '{0}'";
            sql = string.Format(sql, deptHIS);

            string res = this.ExecSqlReturnOne(sql);

            if (res == "-1" || string.IsNullOrEmpty(res))
            {
                return "A50.18";//其他
            }
            return res;
        }


        //获取医保2.0的icd编码// {C5F0A966-A7A4-4f64-B5E5-A8F414982D7A}
        public string GetICDYB2(string icdCode)
        {
            string strSql = @"select a.yb_code from met_com_icdcompare_yb a where a.icd10_code = '{0}' and a.kind_code = 'ZD'";
            strSql = string.Format(strSql, icdCode);

            try
            {
                string icd = this.ExecSqlReturnOne(strSql);
                if (string.IsNullOrEmpty(icd) || icd == "-1")
                {
                    return icdCode;
                }
                return icd;
            }
            catch (Exception e)
            {
                return icdCode;
            }
        }


        /// <summary>
        /// 查询门诊患者挂号信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.Const QueryIDNO(string cardNO)
        {
            List<FS.HISFC.Models.Base.Const> alList = new List<FS.HISFC.Models.Base.Const>();

            string strSql = "";

            strSql = @"select o.card_no,o.name,o.idenno from com_patientinfo o where o.card_no = '{0}'";



            strSql = string.Format(strSql, cardNO);

            try
            {
                if (ExecQuery(strSql) == -1)
                {
                    return null;
                }
                while (Reader.Read())
                {
                    FS.HISFC.Models.Base.Const obj = new FS.HISFC.Models.Base.Const();
                    obj.ID = Reader[0].ToString();
                    obj.Name = Reader[1].ToString();
                    obj.Memo = Reader[2].ToString();
                    //obj.SpellCode = Reader[3].ToString();
                    //obj.WBCode = Reader[4].ToString();
                    //obj.UserCode = Reader[5].ToString();
                    alList.Add(obj);
                }
                Reader.Close();
            }
            catch (Exception e)
            {
                return null;
            }
            if (alList != null && alList.Count > 0)
            {
                return alList[0];
            }
            else
            {
                return null;
            }
        }
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

        /// <summary>
        /// 是否是限制用药
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string IfRangeItem(string userCode)
        {
            string strSql = @"select 1 from com_dictionary c where c.type = 'IndicationsDrug' and c.code = '{0}'";
            strSql = string.Format(strSql, userCode);
            return this.ExecSqlReturnOne(strSql);
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
