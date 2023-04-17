using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace FS.SOC.Local.ZDLY.PubReport.BizLogic
{
    public class Fee : FS.FrameWork.Management.Database
    {
        public Fee()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region 调整费用，用于公费托收单调整自负进位引起的差额，结算指定金额插入数据调整费用
        /// <summary>
        /// 调整公费小数部分，插入差额
        /// </summary>
        /// <param name="pInfo"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public int AdjustPubFeeTial(FS.HISFC.Models.RADT.PatientInfo pInfo, string begin, string end)
        {
            if (pInfo.Pact.PayKind.ID != "03")
            {
                return 0;
            }
            return this.ExecProcedure(pInfo.ID, begin, end, "2", 0);
        }
        /// <summary>
        /// 调整按一定金额结算时差额
        /// </summary>
        /// <param name="pInfo"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="Odd"></param>
        /// <returns></returns>
        public int AdjustOddForBalance(FS.HISFC.Models.RADT.PatientInfo pInfo, string begin, string end, decimal Odd)
        {
            if (pInfo.Pact.PayKind.ID != "01")
            {
                this.Err = "非自费患者,不能进行结算拆分";
                return -1;
            }
            return this.ExecProcedure(pInfo.ID, begin, end, "1", Odd);
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="InpatientNo"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="type"></param>
        /// <param name="Odd"></param>
        /// <returns></returns>
        public int ExecProcedure(string InpatientNo, string begin, string end, string type, decimal Odd)
        {
            string strReturn = "";
            string strSql = "";
            string strOdd = Odd.ToString();
            strSql = "PRC_IPB_ADJODDMENT,Par_InpatientNO,22,1,{0}," +
                "Par_Begin,22,1,{1}," +
                "Par_End,22,1,{2}," +
                "Par_Type,22,1,{3}," +
                "Par_OddFee,30,1,{4}," +
                "Par_ErrText,22,2,OK," +
                "Par_NErrCode,13,2,1";
            try
            {
                strSql = string.Format(strSql, InpatientNo, begin, end, type, strOdd);
                if (this.ExecEvent(strSql, ref strReturn) == -1)
                {
                    this.Err = "执行存储过程出错！" + this.Err;
                    return -1;
                }
                string[] str = strReturn.Split(',');
                if (FS.FrameWork.Function.NConvert.ToInt32(str[1]) == -1)
                {
                    this.Err = str[0];
                    return -1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                this.Err = this.Err + ex.Message;
                return -1;
            }
        }

        #endregion


        /// <summary>
        /// 预交金月报
        /// </summary>
        /// <param name="dept">科室:住院处</param>
        /// <param name="begin">上月时间</param>
        /// <param name="end">本月时间</param>
        /// <param name="Last">期初预收</param>
        /// <param name="Current">期末预收</param>
        /// <param name="Income">本期预收</param>
        /// <param name="Balance">本期退预收</param>
        /// <param name="ChangeIN">转入预收</param>
        /// <returns></returns>
        public int GetUnBanlancePrepay(string dept, string begin, string end, out string Last,
            out string Current, out string Income, out string Balance, out string ChangeIN)
        {
            string strReturn = "";
            string strSql = "";
            Last = "";
            Income = "";
            Balance = "";
            ChangeIN = "";
            Current = "";
            strSql = "prc_ipb_getunbalanceprepay,begin_date,22,1,{0}," +
                "end_date,22,1,{1}," +
                "par_dept,22,1,{2}," +
                "last_prepay,30,2,1," +
                "income_prepay,30,2,1," +
                "balance_prepay,30,2,1," +
                "change_in,30,2,1," +
                "rest_prapay,30,2,1";
            try
            {
                strSql = string.Format(strSql, begin, end, dept);
                if (this.ExecEvent(strSql, ref strReturn) == -1)
                {
                    this.Err = "执行存储过程出错！" + this.Err;
                    return -1;
                }
                string[] s = strReturn.Split(',');
                Last = s[0];
                Income = s[1];
                Balance = s[2];
                ChangeIN = s[3];
                Current = s[4];

                return 0;
            }
            catch (Exception ex)
            {
                this.Err = this.Err + ex.Message;
                return -1;
            }
        }

        #region 出院未清账病人
        /// <summary>
        /// 出院日期大于指定时间出院登记未结帐的患者
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <returns></returns>
        public DataSet GetOutPatient(string dtBegin)
        {
            string strSql = "select patient_no 住院号,name 姓名,pact_name 合同单位,dept_name 病区, " +
                " prepay_cost 预交金,own_cost+pay_cost 应缴金额,free_cost 剩余金额,out_date 出院日期 " +
                " from fin_ipr_inmaininfo i where i.in_state='B'" +
                " and i.out_date>to_date('{0}','yyyy-mm-dd hh24:mi:ss') order by dept_code";

            try
            {
                strSql = string.Format(strSql, dtBegin);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            DataSet ds = new DataSet();

            this.ExecQuery(strSql, ref ds);

            return ds;
        }
        /// <summary>
        /// 当天作出院登记但未结帐的患者
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DataSet GetOutBeforeBalance(string begin, string end)
        {
            string strSql = "select patient_no 住院号,name 姓名,pact_name 合同单位,dept_name 病区," +
                "  prepay_cost 预交金,own_cost+pay_cost 应缴金额,free_cost 剩余金额, " +
                " max(s.oper_date) 登记日期,  out_date 出院日期 	 from fin_ipr_inmaininfo i,com_shiftdata s " +
                " where i.inpatient_no = s.clinic_no   and s.shift_type='O' " +
                " and s.oper_date>=to_date('{0}','yyyy-mm-dd hh24:mi:ss') " +
                " and s.oper_date<=to_date('{1}','yyyy-mm-dd hh24:mi:ss') and i.in_state='B' " +
                "  group by patient_no,name,pact_name,dept_name,prepay_cost,own_cost,pay_cost,free_cost,out_Date,dept_code " +
                " order by dept_code ";

            try
            {
                strSql = string.Format(strSql, begin, end);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            DataSet ds = new DataSet();

            this.ExecQuery(strSql, ref ds);

            return ds;
        }
        /// <summary>
        /// 应收应付报表
        /// </summary>
        /// <param name="begin">统计开始日期</param>
        /// <param name="end">统计截止日期</param>		
        /// <param name="flag">1 应收2应付</param>
        /// <returns></returns>
        public DataSet GetOutNoBalance(string begin, string end, string flag)
        {
            string strSql;
            if (flag == "1")
            {
                strSql = "select patient_no 住院号,name 姓名,pact_name 合同单位,dept_name 病区, " +
                    " prepay_cost 预交金,own_cost+pay_cost 应缴金额,free_cost 剩余金额,out_date 出院日期 " +
                    " from fin_ipr_inmaininfo i where i.in_state in('B','C') " +
                    " and i.out_date>=to_date('{0}','yyyy-mm-dd hh24:mi:ss') " +
                    " and i.out_date<=to_date('{1}','yyyy-mm-dd hh24:mi:ss') and i.free_cost<0 " +
                    " order by  dept_code, out_date";
            }
            else
            {

                strSql = "select patient_no 住院号,name 姓名,pact_name 合同单位,dept_name 病区, " +
                    " prepay_cost 预交金,own_cost+pay_cost 应缴金额,free_cost 剩余金额,out_date 出院日期 " +
                    " from fin_ipr_inmaininfo i where i.in_state in('B','C') " +
                    " and i.out_date>=to_date('{0}','yyyy-mm-dd hh24:mi:ss') " +
                    " and i.out_date<=to_date('{1}','yyyy-mm-dd hh24:mi:ss') and i.free_cost>=0 " +
                    " order by  dept_code,out_date";
            }

            try
            {
                strSql = string.Format(strSql, begin, end);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            DataSet ds = new DataSet();

            this.ExecQuery(strSql, ref ds);

            return ds;
        }
        #endregion
        #region 更改支付方式相关 -预交金和实收
        /// <summary>
        /// 获取支付信息
        /// </summary>
        /// <param name="BillNo">收据号</param>
        /// <param name="TransType">交易类型 1正交易，2反交易</param>
        /// <param name="TransKind">交易种类 0预交款1结算款</param>
        /// <returns></returns>
        public DataSet GetPayWayInfoByNo(string BillNo, string TransType, string TransKind)
        {
            /*  new DataColumn("收据号",str),
                                                              new DataColumn("交易类型",str),
                                                              new DataColumn("交易序号",Int),
                                                              new DataColumn("金额",dec),
                                                              new DataColumn("支付方式",str),
                                                              new DataColumn("操作员",str),
                                                              new DataColumn("操作日期",str),
                                                              new DataColumn("标志",str)})
                                                              */
            string strSql;
            if (TransKind == "0")//预交款
            {
                strSql = " select i.receipt_no 收据号,i.prepay_state 交易类型,i.happen_no 交易序号,i.prepay_cost 金额, " +
                    " i.pay_way 支付方式,i.oper_code 操作员,i.oper_date 操作日期,'' 标志 " +
                    " from fin_ipb_inprepay i where i.receipt_no='{0}' " +
                    " and i.prepay_state='{1}'";
            }
            else
            {
                strSql = " select p.invoice_no 收据号,p.trans_type 交易类型,p.balance_no 交易序号,p.cost 金额, " +
                         " p.pay_way 支付方式,p.balance_opercode 操作员,p.balance_date 操作日期,p.reutrnorsupply_flag 标志 " +
                         " from fin_ipb_balancepay p where p.invoice_no='{0}' " +
                         " and p.trans_type='{1}' and p.trans_kind='1'";
            }

            try
            {
                strSql = string.Format(strSql, BillNo, TransType);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            DataSet ds = new DataSet();

            this.ExecQuery(strSql, ref ds);

            return ds;
        }

        /// <summary>
        /// 更改支付方式
        /// </summary>
        /// <param name="BillNo">收据号</param>
        /// <param name="Transtype">交易类型 1正交易 2 反交易</param>
        /// <param name="TransKind">交易种类 0预交款1结算款</param>
        /// <param name="No">序号</param>
        /// <param name="rsFlag">收取返还标志</param>
        /// <param name="OldPayWay">原支付方式</param>
        /// <param name="NewPayWay">新支付方式</param>
        /// <returns>1成功 其他失败</returns>
        public int UpdatePayWay(string BillNo, string TransType, string TransKind, int No, string rsFlag, string OldPayWay, string NewPayWay)
        {
            string strSql = "";
            if (TransKind == "0")
            {
                strSql = " update fin_ipb_inprepay i set i.pay_way='{4}' " +
                       " where i.receipt_no='{0}' and i.happen_no={1} " +
                       " and i.prepay_state='{2}' and (i.pay_way is null or i.pay_way='{3}')";
                strSql = string.Format(strSql, BillNo, No.ToString(), TransType, OldPayWay, NewPayWay);
            }
            else
            {
                strSql = " update fin_ipb_balancepay p set p.pay_way='{5}' " +
                       " where p.invoice_no='{0}' and p.balance_no={1} " +
                       " and p.trans_type={2} and p.trans_kind='1' " +
                       " and p.reutrnorsupply_flag='{3}' and p.pay_way='{4}'";
                strSql = string.Format(strSql, BillNo, No.ToString(), TransType, rsFlag, OldPayWay, NewPayWay);
            }
            if (this.ExecNoQuery(strSql) != 1)
            {
                this.Err = "更新数据出错，更新行数不不是1，" + this.Err;
                return -1;
            }
            return 1;
        }

        #endregion
        #region  生育保险患者
        /// <summary>
        /// 查询生育保险门诊费用记账金额
        /// </summary>
        /// <param name="DNH">生育保险电脑号</param>
        /// <param name="IDno">身份证号码</param>
        /// <param name="MzCost">门诊记账费用</param>
        /// <returns></returns>
        public int GetSYBXMzCost(string DNH, string IDno, out decimal MzCost)
        {
            string strSql = "";
            MzCost = 0;
            string rtnString = "";
            if (this.Sql.GetSql("Local.Fee.GetSYBXMzCost", ref strSql) == -1)
            {
                this.Err = "Can't find the Sql Local.Fee.GetSYBXMzCost";
                return -1;
            }
            strSql = string.Format(strSql, DNH, IDno);
            rtnString = this.ExecSqlReturnOne(strSql);
            MzCost = FS.FrameWork.Function.NConvert.ToDecimal(rtnString);
            return 1;
        }
        /// <summary>
        /// 统计门诊患者费用
        /// </summary>
        /// <param name="IDno">身份证号</param>
        /// <param name="DNH">生育保险 电脑号</param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.FT GetSYBXPatientFee(string IDno, string DNH)
        {
            string strSql = "";
            FS.HISFC.Models.Base.FT ft = new FS.HISFC.Models.Base.FT();
            if (this.Sql.GetSql("Local.Fee.GetSYBXPatientFee", ref strSql) == -1)
            {
                this.Err = "Can't find the Sql Local.Fee.GetSYBXPatientFee";
                return null;
            }
            strSql = string.Format(strSql, IDno, DNH);
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }
            while (this.Reader.Read())
            {
                ft.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
                ft.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[1]);
                ft.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
                ft.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3]);
            }
            return ft;
        }
        #endregion


        #region 费用管理 Edit by hgx
        /// <summary>
        /// 获取检索fin_com_itemlist的全部数据的sql
        /// </summary>
        /// <returns></returns>
        private string GetSqlForSelectAllFeeItems()
        {
            string strSql = "";
            if (this.Sql.GetSql("Fee.SelectAllFromFeeItem.1", ref strSql) == -1)
                return null;
            return strSql;
        }
        /// <summary>
        /// 获取检索fin_com_medicinelist的全部数据的sql
        /// </summary>
        /// <returns></returns>
        public string GetSqlForSelectAllMedItems()
        {
            string strSql = "";
            if (this.Sql.GetSql("Fee.SelectAllFromMedItem.2", ref strSql) == -1)
                return null;
            return strSql;
        }

        /// <summary>
        /// 获得药品费用项目信息 执行此函数使用sql必须依照下面注释顺序取本类中的GetSqlForSelectAllMedItems()函数可以取此顺序的全表检索sql
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public ArrayList GetMedItems(string strSql)
        {
            ArrayList al = new ArrayList();

            this.ExecQuery(strSql);
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList ItemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                FS.HISFC.Models.Pharmacy.Item pharmacyItem = new FS.HISFC.Models.Pharmacy.Item();
                ItemList.Order.Item = pharmacyItem;
                //0 处方号1处方内项目流水号2交易类型,1正交易，2反交易3住院流水号4姓名5结算类别6合同单位7更新库存的流水号(物资)8在院科室代码
                //9护士站代码10开立科室代码11执行科室代码12扣库科室代码13开立医师代码14项目代码15最小费用代码16中心代码17项目名称18单价19数量
                //20当前单位21包装数22付数23费用金额24自费金额25自付金额26公费金额27优惠金额28出库单序列号29发放状态30是否婴儿用31急诊抢
                //救标志32出院带疗标记33结算发票号34结算序号35审批号36划价人37划价日期38自制标识39药品性质40发药人代码41发药日期42计费人43计费日
                //期45审核序号46医嘱流水号47医嘱执行单流水号48规格49药品类别50可退数量51结算状态52收费比例 53收费员科室
                try
                {
                    ItemList.RecipeNO = this.Reader[0].ToString();
                    ItemList.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[1].ToString());
                    ItemList.TransType = (FS.HISFC.Models.Base.TransTypes)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());
                    ItemList.ID = this.Reader[3].ToString();
                    ItemList.Name = this.Reader[4].ToString();
                    ItemList.Patient.Pact.PayKind.ID = this.Reader[5].ToString();
                    ItemList.Patient.Pact.ID = this.Reader[6].ToString();
                    ItemList.UpdateSequence = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[7].ToString());
                    ItemList.Order.InDept.ID = this.Reader[8].ToString();
                    ItemList.Order.NurseStation.ID = this.Reader[9].ToString();
                    ItemList.Order.ReciptDept.ID = this.Reader[10].ToString();
                    ItemList.Order.ExeDept.ID = this.Reader[11].ToString();
                    ItemList.Order.StockDept.ID = this.Reader[12].ToString();
                    ItemList.Order.ReciptDoctor.ID = this.Reader[13].ToString();
                    ItemList.Item.ID = this.Reader[14].ToString();
                    ItemList.Item.MinFee.ID = this.Reader[15].ToString();

                    ItemList.Item.Name = this.Reader[17].ToString();
                    ItemList.Item.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[18].ToString());
                    ItemList.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[19].ToString());
                    ItemList.Item.PriceUnit = this.Reader[20].ToString();
                    ItemList.Item.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[21].ToString());
                    ItemList.Order.HerbalQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[22].ToString());
                    ItemList.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[23].ToString());
                    ItemList.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[24].ToString());
                    ItemList.FT.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[25].ToString());
                    ItemList.FT.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26].ToString());
                    ItemList.FT.RebateCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[27].ToString());
                    ItemList.SendSequence = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[28].ToString());
                    ItemList.Order.Status = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[29].ToString());
                    ItemList.IsBaby = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[30].ToString());

                    ItemList.Order.OrderType.Memo = this.Reader[32].ToString();
                    ItemList.BalanceNO = FS.FrameWork.Function.NConvert.ToInt32( this.Reader[33].ToString());
                    ItemList.SendSequence = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[34].ToString());

                    ItemList.ChargeOper.ID = this.Reader[36].ToString();
                    //ft,把划价日期漏掉，狂晕 by Maokb 06-03-22
                    ItemList.ChargeOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[37].ToString());
                    pharmacyItem.IsAllergy = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[38].ToString());
                    pharmacyItem.Quality.ID = this.Reader[39].ToString();


                    ItemList.ExecOper.ID = this.Reader[40].ToString();
                    ItemList.ExecOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[41].ToString());
                    ItemList.FeeOper.ID = this.Reader[42].ToString();
                    ItemList.FeeOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[43].ToString());
                    ItemList.AuditingNO =  this.Reader[45].ToString();
                    ItemList.Order.ID = this.Reader[46].ToString();
                    ItemList.Order.ReciptNO = this.Reader[47].ToString();
                    pharmacyItem.Specs = this.Reader[48].ToString();
                    pharmacyItem.Type.ID = this.Reader[49].ToString();
                    //pharmacyItem.IsPharmacy = true;
                    pharmacyItem.ItemType = FS.HISFC.Models.Base.EnumItemType.Drug;
                    ItemList.NoBackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[50].ToString());
                    ItemList.BalanceState = this.Reader[51].ToString();
                    ItemList.FTRate.PayRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[52].ToString());
                    ItemList.FeeOper.Dept.ID = this.Reader[53].ToString();	//收费员科室
                    ItemList.ExtFlag = this.Reader[54].ToString();		//扩展标记
                    ItemList.ExtFlag1 = this.Reader[55].ToString();		//扩展标记1
                    ItemList.ExtFlag2 = this.Reader[56].ToString();		//扩展标记2
                    ItemList.ExtCode = this.Reader[57].ToString();		//扩展编码
                    ItemList.ExecOper.ID = this.Reader[58].ToString();	//扩展操作员
                    ItemList.ExecOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[59].ToString());	//扩展日期
                }
                catch (Exception ex)
                {
                    this.Err = "查询患者非药品费用明细赋值错误" + ex.Message;
                    this.ErrCode = ex.Message;
                    this.WriteErr();
                    return null;
                }
                al.Add(ItemList);
            }
            this.Reader.Close();
            return al;
        }
        /// <summary>
        /// 根据发放状态提取一段时间范围内未结算的供可供退费的药品项目 Edit by liangjz,Modify by hgx
        /// </summary>
        /// <param name="InpatientNo"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="SendDrugState"></param>
        /// <returns></returns>
        public ArrayList GetForQuitDrug(string InpatientNo, DateTime dtBegin, DateTime dtEnd, string SendDrugState)
        {
            return this.GetForQuitDrug(InpatientNo, dtBegin, dtEnd, SendDrugState, false);
        }

        /// <summary>
        /// 根据是否结算、是否发放提取一段时间范围内可供退费的药品项目 Edit by liangjz Modify by hgx
        /// </summary>
        /// <param name="InpatientNo">住院流水号</param>
        /// <param name="dtBegin">起始时间</param>
        /// <param name="dtEnd">终止时间</param>
        /// <param name="SendDrugState">是否发药</param>
        /// <param name="isBalance">是否结算</param>
        /// <returns>返回动态数组，失败返回null</returns>
        public ArrayList GetForQuitDrug(string InpatientNo, DateTime dtBegin, DateTime dtEnd, string SendDrugState, bool isBalance)
        {
            string strSql1 = "";
            string strSql2 = "";
            string balanceState;
            if (isBalance)	//已结算
                balanceState = "1";
            else	//未结算
                balanceState = "0";
            strSql1 = this.GetSqlForSelectAllMedItems();
            if (this.Sql.GetSql("Fee.GetForQuitDrug.5", ref strSql2) == -1)
                return null;
            strSql1 = strSql1 + strSql2;
            try
            {
                strSql1 = string.Format(strSql1, InpatientNo, dtBegin, dtEnd, SendDrugState, balanceState);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }
            return this.GetMedItems(strSql1);
        }
        /// <summary>
        /// 根据是否结算、是否发放提取一段时间范围内可供退费的药品项目 Edit by liangjz,Modify by hgx
        /// </summary>
        /// <param name="InpatientNo">住院流水号</param>
        /// <param name="dtBegin">起始时间</param>
        /// <param name="dtEnd">终止时间</param>
        /// <param name="SendDrugState">是否发药</param>
        /// <param name="isBalance">是否结算</param>
        /// <param name="minFeeID">最小费用代码</param>
        /// <returns>返回动态数组，失败返回null</returns>
        public ArrayList GetForQuitDrug(string InpatientNo, DateTime dtBegin, DateTime dtEnd, string SendDrugState, bool isBalance, string minFeeID)
        {
            string strSql1 = "";
            string strSql2 = "";
            string balanceState;
            if (isBalance)	//已结算
                balanceState = "1";
            else	//未结算
                balanceState = "0";
            strSql1 = this.GetSqlForSelectAllMedItems();
            if (this.Sql.GetSql("Fee.GetForQuitDrug.5", ref strSql2) == -1)
                return null;
            strSql1 = strSql1 + strSql2;
            try
            {
                strSql1 = string.Format(strSql1, InpatientNo, dtBegin, dtEnd, SendDrugState, balanceState, minFeeID);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }
            return this.GetMedItems(strSql1);
        }


        /// <summary>
        /// 检索药品和非药品明细单条记录---通过主键
        /// </summary>
        /// <param name="NoteNo"></param>
        /// <param name="SequenceNo"></param>
        /// <param name="IsPharmacy"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Fee.Inpatient.FeeItemList GetFeeItemListByNoteNoAndNoteSequence(string NoteNo, int SequenceNo, bool IsPharmacy)
        {

            //0 处方号1处方内流水号
            string strSql1 = "";
            string strSql2 = "";
            ArrayList alItem = new ArrayList();
            FS.HISFC.Models.Fee.Inpatient.FeeItemList ItemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
            if (IsPharmacy)
            {
                strSql1 = this.GetSqlForSelectAllMedItems();
                if (this.Sql.GetSql("Fee.GetFeeItemListByNoteNoAndNoteSequence.1", ref strSql2) == -1)
                    return null;
                strSql2 = string.Format(strSql2, NoteNo, SequenceNo);
            }
            else
            {

                strSql1 = this.GetSqlForSelectAllFeeItems();
                if (this.Sql.GetSql("Fee.GetFeeItemListByNoteNoAndNoteSequence.1", ref strSql2) == -1)
                    return null;
                strSql2 = string.Format(strSql2, NoteNo, SequenceNo);
            }
            strSql1 = strSql1 + strSql2;
            if (IsPharmacy)
            {

                alItem = this.GetMedItems(strSql1);

            }
            else
            {
                alItem = this.GetFeeItems(strSql1);
            }
            if (alItem == null)
                return null;
            if (alItem.Count == 0)
            {
                this.Err = "没有找到该项目";
                return null;
            }
            ItemList = (FS.HISFC.Models.Fee.Inpatient.FeeItemList)alItem[0];
            return ItemList;
        }

        /// <summary>
        /// 获得费用项目信息 执行此函数使用sql必须依照下面注释顺序取本类中的GetSqlForSelectAllFeeItems()函数可以取此顺序的全表检索sql
        /// </summary>
        /// <returns></returns>
        private ArrayList GetFeeItems(string strSql)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Fee.Inpatient.FeeItemList ItemList;
            this.ExecQuery(strSql);
            while (this.Reader.Read())
            {

                //0 处方号1处方内项目流水号2交易类型,1正交易，2反交易3住院流水号4姓名5结算类别6合同单位7更新库存的流水号(物资)8在院科室代码
                //9护士站代码10开立科室代码11执行科室代码12扣库科室代码13开立医师代码14项目代码15最小费用代码16中心代码17项目名称18单价19数量
                //20当前单位21组套代码22组套名称23费用金额24自费金额25自付金额26公费金额27优惠金额28出库单序列号29发放状态30是否婴儿用31急诊抢

                //救标志32出院带疗标记33结算发票号34结算序号35审批号36划价人37划价日期38已确认数39设备号40执行人代码41执行日期42计费人43计费日

                //期45审核序号46医嘱流水号47医嘱执行单流水号48可退数量49结算状态50收费比例51收费员科室
                ItemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                try
                {
                    ItemList.RecipeNO = this.Reader[0].ToString();
                    ItemList.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[1].ToString());
                    ItemList.TransType = (FS.HISFC.Models.Base.TransTypes)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());
                    ItemList.ID = this.Reader[3].ToString();
                    ItemList.Name = this.Reader[4].ToString();
                    ItemList.Patient.Pact.PayKind.ID = this.Reader[5].ToString();
                    ItemList.Patient.Pact.ID = this.Reader[6].ToString();
                    ItemList.UpdateSequence = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[7].ToString());
                    ItemList.Order.InDept.ID = this.Reader[8].ToString();
                    ItemList.Order.NurseStation.ID = this.Reader[9].ToString();
                    ItemList.Order.ReciptDept.ID = this.Reader[10].ToString();
                    ItemList.Order.ExeDept.ID = this.Reader[11].ToString();
                    ItemList.Order.StockDept.ID = this.Reader[12].ToString();
                    ItemList.Order.ReciptDoctor.ID = this.Reader[13].ToString();
                    ItemList.Item.ID = this.Reader[14].ToString();
                    ItemList.Item.MinFee.ID = this.Reader[15].ToString();

                    ItemList.Item.Name = this.Reader[17].ToString();
                    ItemList.Item.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[18].ToString());
                    ItemList.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[19].ToString());
                    ItemList.Item.PriceUnit = this.Reader[20].ToString();
                    ItemList.Item.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[21].ToString());
                    ItemList.Order.HerbalQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[22].ToString());
                    ItemList.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[23].ToString());
                    ItemList.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[24].ToString());
                    ItemList.FT.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[25].ToString());
                    ItemList.FT.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26].ToString());
                    ItemList.FT.RebateCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[27].ToString());
                    ItemList.SendSequence = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[28].ToString());
                    ItemList.Order.Status = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[29].ToString());
                    ItemList.IsBaby = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[30].ToString());

                    ItemList.Order.OrderType.Memo = this.Reader[32].ToString();
                    ItemList.BalanceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[33].ToString());
                    ItemList.SendSequence = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[34].ToString());

                    ItemList.ChargeOper.ID = this.Reader[36].ToString();
                    //ft,把划价日期漏掉，狂晕 by Maokb 06-03-22
                    ItemList.ChargeOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[37].ToString());
                    ItemList.ConfirmedQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[38].ToString());
                    ItemList.MachineNO = this.Reader[39].ToString();


                    ItemList.ExecOper.ID = this.Reader[40].ToString();
                    ItemList.ExecOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[41].ToString());
                    ItemList.FeeOper.ID = this.Reader[42].ToString();
                    ItemList.FeeOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[43].ToString());
                    ItemList.AuditingNO = this.Reader[45].ToString();
                    ItemList.Order.ID = this.Reader[46].ToString();
                    ItemList.Order.ReciptNO = this.Reader[47].ToString();
                    ItemList.Item.Specs = this.Reader[48].ToString();
                    ItemList.BalanceState = this.Reader[49].ToString();
                    //ItemList.Item.IsPharmacy = true;
                    ItemList.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.Drug;
                    ItemList.NoBackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[50].ToString());
                    ItemList.BalanceState = this.Reader[51].ToString();
                    ItemList.Patient.Pact.Rate.PayRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[52].ToString());
                    ItemList.FeeOper.Dept.ID = this.Reader[53].ToString();	//收费员科室
                    ItemList.ExtFlag = this.Reader[54].ToString();		//扩展标记
                    ItemList.ExtFlag1 = this.Reader[55].ToString();		//扩展标记1
                    ItemList.ExtFlag2 = this.Reader[56].ToString();		//扩展标记2
                    ItemList.ExtCode = this.Reader[57].ToString();		//扩展编码
                    ItemList.ExecOper.ID = this.Reader[58].ToString();	//扩展操作员
                    ItemList.ExecOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[59].ToString());	//扩展日期
                }

                catch (Exception ex)
                {
                    this.Err = "查询患者非药品费用明细赋值错误" + ex.Message;
                    this.ErrCode = ex.Message;
                    this.WriteErr();
                    return null;
                }
                al.Add(ItemList);
            }
            this.Reader.Close();
            return al;
        }

        /// <summary>
        /// 查找自费项目（公费使用）
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public ArrayList QueryOwnFeeItems(string inpatientNO)
        {
            ArrayList al = new ArrayList();
            FS.FrameWork.Models.NeuObject obj = null;
            string sql = @"select distinct t.item_code,t.item_name from fin_ipb_itemlist t where 
                                         t.inpatient_no ='{0}'
                                        and t.ext_flag='1' and t.ext_flag4 is null and t.noback_num>0
                                        and t.split_fee_flag='0'
                                        union all
                                        select distinct t.drug_code,t.drug_name from fin_ipb_medicinelist t where 
                                         t.inpatient_no ='{0}'
                                        and t.ext_flag='1' and t.ext_flag4 is null and t.noback_num>0";
            this.ExecQuery(string.Format(sql, inpatientNO));
            try
            {
                while (this.Reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();

                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString();

                    al.Add(obj);
                }
            }

            catch (Exception ex)
            {
                this.Err = "查询患者非药品费用明细赋值错误" + ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                if (this.Reader != null && this.Reader.IsClosed == false)
                {
                    this.Reader.Close();
                }
            }
            return al;
        }

        #endregion
    }
}
