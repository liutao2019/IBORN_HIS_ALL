using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizLogic.Pharmacy
{
    public class Financial : DataBase
    {
        
        #region 财务核准
        /// <summary>
        /// 财务核准入库
        /// </summary>
        /// <param name="input">入库实体</param>
        /// <returns></returns>
        public int FinApproveInput(FS.HISFC.Models.Pharmacy.Input input)
        {
            string strSQL = "";
            if (this.GetSQL("SOC.Pharmacy.Item.FinApproveInput", ref strSQL) == -1)
            {
                strSQL = @"UPDATE  PHA_COM_INPUT SET
                                   EXAM_NUM = {1},  --核准数量
                                   APPROVE_OPERCODE = '{2}', --核准人 
                                   APPROVE_DATE = to_date('{3}','yyyy-mm-dd HH24:mi:ss') , --核准日期
                                   IN_STATE = '2',
                                   INVOICE_NO = '{4}',
                                   OPER_CODE = '{5}',
                                   OPER_DATE = sysdate,
                                   EXAM_OPERCODE = '{6}',
                                   EXAM_DATE = to_date('{7}','yyyy-mm-dd HH24:mi:ss') 
                          WHERE    IN_BILL_CODE = {0}  
                          ";

                this.CacheSQL("SOC.Pharmacy.Item.FinApproveInput", strSQL);
            }
            try
            {
                strSQL = string.Format(strSQL, input.ID,
                    input.Operation.ExamQty.ToString(),
                    input.Operation.ApproveOper.ID,
                    input.Operation.ApproveOper.OperTime.ToString(),
                    input.InvoiceNO,
                    input.Operation.Oper.ID,
                    input.Operation.ExamOper.ID,
                    input.Operation.ExamOper.OperTime.ToString());


            }
            catch (Exception ex)
            {
                this.Err = "参数化SOC.Pharmacy.Item.FinApproveInput出错：" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);

        }

        /// <summary>
        /// 财务入库记账
        /// </summary>
        /// <param name="input">入库</param>
        /// <param name="finBillNO">记账单</param>
        /// <returns></returns>
        public int SetFinBill(FS.HISFC.Models.Pharmacy.Input input, string finBillNO)
        {
            string strSQL = "";
            if (this.GetSQL("SOC.Pharmacy.Item.FinBill.Insert", ref strSQL) == -1)
            {
                strSQL = @"insert into PHA_COM_FINBILL
                                    (
                                      DRUG_DEPT_CODE,
                                      TYPE,
                                      LIST_CODE,
                                      FIN_CODE,
                                      OPER_CODE,
                                      OPER_DATE
                                    )
                                    values
                                    (
                                      '{0}',
                                      '{1}',
                                      '{2}',
                                      '{3}',
                                      '{4}',
                                      sysdate
                                    )
                          ";

                this.CacheSQL("SOC.Pharmacy.Item.FinBill.Insert", strSQL);
            }
            try
            {
                strSQL = string.Format(strSQL,
                    input.StockDept.ID,
                    "0",
                    input.InListNO,
                    finBillNO,
                    input.Operation.Oper.ID);


            }
            catch (Exception ex)
            {
                this.Err = "参数化SOC.Pharmacy.Item.FinBill.Insert出错：" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSQL);

        }

        /// <summary>
        /// 财务核准出库
        /// </summary>
        /// <param name="input">出库实体</param>
        /// <returns></returns>
        public int FinApproveOutput(FS.HISFC.Models.Pharmacy.Output output)
        {
            string strSQL = "";
            if (this.GetSQL("SOC.Pharmacy.Item.FinApproveOutput", ref strSQL) == -1)
            {
                strSQL = @"UPDATE  PHA_COM_OUTPUT SET
                                   FIN_FLAG = '1',
                                   OPER_CODE = '{1}',
                                   OPER_DATE = sysdate
                          WHERE    OUT_BILL_CODE = {0}  
                          ";

                this.CacheSQL("SOC.Pharmacy.Item.FinApproveOutput", strSQL);
            }
            try
            {
                strSQL = string.Format(strSQL, output.ID, output.Operation.Oper.ID);
            }
            catch (Exception ex)
            {
                this.Err = "参数化SOC.Pharmacy.Item.FinApproveOutput出错：" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);

        }

        /// <summary>
        /// 财务出库记账
        /// </summary>
        /// <param name="input">出库</param>
        /// <param name="finBillNO">记账单</param>
        /// <returns></returns>
        public int SetFinBill(FS.HISFC.Models.Pharmacy.Output output, string finBillNO)
        {
            string strSQL = "";
            if (this.GetSQL("SOC.Pharmacy.Item.FinBill.Insert", ref strSQL) == -1)
            {
                strSQL = @"insert into PHA_COM_FINBILL
                                    (
                                      DRUG_DEPT_CODE,
                                      TYPE,
                                      LIST_CODE,
                                      FIN_CODE,
                                      OPER_CODE,
                                      OPER_DATE
                                    )
                                    values
                                    (
                                      '{0}',
                                      '{1}',
                                      '{2}',
                                      '{3}',
                                      '{4}',
                                      sysdate
                                    )
                          ";

                this.CacheSQL("SOC.Pharmacy.Item.FinBill.Insert", strSQL);
            }
            try
            {
                strSQL = string.Format(strSQL,
                    output.StockDept.ID,
                    "1",
                    output.OutListNO,
                    finBillNO,
                    output.Operation.Oper.ID);


            }
            catch (Exception ex)
            {
                this.Err = "参数化SOC.Pharmacy.Item.FinBill.Insert出错：" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSQL);

        }
        #endregion

        #region 供货商结存\付款管理
        /// <summary>
        /// 取供货商结存付款序号
        /// </summary>
        /// <returns>成功返回新付款序号 失败返回null</returns>
        public string GetNewPayNO()
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.GetPayNO", ref strSQL) == -1) return null;
            string strReturn = this.ExecSqlReturnOne(strSQL);
            if (strReturn == "-1")
            {
                this.Err = "取供货商结存付款序号时出错！" + this.Err;
                return null;
            }
            return strReturn;
        }

        /// <summary>
        /// 未付款信息查询
        /// </summary>
        /// <param name="stockDeptNO">库存科室编码</param>
        /// <param name="companyNO">供货公司编码</param>
        /// <param name="invoiceNO">发票号</param>
        /// <param name="inBillNO">入库单号</param>
        /// <param name="payState">付款状态</param>
        /// <returns>FS.HISFC.Models.Pharmacy.Pay实体数组</returns>
        public ArrayList QueryUnpayFromInput(string stockDeptNO, string companyNO, string invoiceNO, string inBillNO,string payState)
        {
            string SQL = "";
            if (this.GetSQL("SOC.Pharmacy.Financial.GetUnpay", ref SQL) == -1)
            {
                SQL = @"
                        select  t.in_list_code,                            
                                t.invoice_no,  
                                t.company_code,                           
                                t.invoice_date,                           
                                t.purchase_cost,                          
                                t.wholesale_cost,
                                t.retail_cost,
                                t.apply_opercode,
                                t.in_date,
                                t.exam_opercode,
                                t.exam_date,
                                t.approve_opercode,
                                t.approve_date,
                                t.pay_state
                        from    pha_com_input t 
                        where   t.pay_state in ({4})
                        and     t.in_state = '2'
                        and     t.invoice_no is not null
                        and     (t.invoice_no = '{2}' or '{2}' = 'All')
                        and     (t.in_list_code = '{3}' or '{3}' = 'All')
                        and     t.drug_dept_code = '{0}'
                        and     t.company_code = '{1}'
                       ";

                this.CacheSQL("SOC.Pharmacy.Financial.GetUnpay", SQL);
            }

            try
            {
                SQL = string.Format(SQL, stockDeptNO, companyNO, invoiceNO, inBillNO, payState);
            }
            catch(Exception ex)
            {
                this.Err = "格式化SOC.Pharmacy.Financial.GetUnpay出错："+ex.Message;
                return null;
            }

            ArrayList al = new ArrayList();
            Hashtable hsPay = new Hashtable();

            if (this.ExecQuery(SQL) == -1)
            {
                this.Err = "获得结存头表信息时，执行SQL语句出错！" + this.Err;
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Pharmacy.Pay pay = new FS.HISFC.Models.Pharmacy.Pay();
                    pay.InListNO = this.Reader[0].ToString();
                    pay.InvoiceNO = this.Reader[1].ToString();
                    pay.Company.ID = this.Reader[2].ToString();
                    pay.InvoiceTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[3]);
                    pay.PurchaseCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]);
                    pay.WholeSaleCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                    pay.RetailCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);
                    pay.Oper.User01 = this.Reader[7].ToString();
                    pay.User01 = this.Reader[8].ToString();
                    pay.Oper.User02 = this.Reader[9].ToString();
                    pay.User02 = this.Reader[10].ToString();
                    pay.Oper.User03 = this.Reader[11].ToString();
                    pay.User03 = this.Reader[12].ToString();

                    pay.PayState = this.Reader[13].ToString();
                    if (string.IsNullOrEmpty(pay.PayState))
                    {
                        pay.PayState = "0";
                    }
                    pay.StockDept.ID = stockDeptNO;

                    if (hsPay.Contains(pay.InvoiceNO + pay.InListNO))
                    {
                        FS.HISFC.Models.Pharmacy.Pay payTot = hsPay[pay.InvoiceNO + pay.InListNO] as FS.HISFC.Models.Pharmacy.Pay;
                        payTot.PurchaseCost += pay.PurchaseCost;
                        payTot.WholeSaleCost += pay.WholeSaleCost;
                        payTot.RetailCost += pay.RetailCost;
                    }
                    else
                    {
                        al.Add(pay);
                        hsPay.Add(pay.InvoiceNO + pay.InListNO, pay);
                    }
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err = "获取结存头表信息时 由Reader内读取信息发生异常 \n" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }
        
        /// <summary>
        /// 未付款信息查询
        /// </summary>
        /// <param name="stockDeptNO">库存科室编码</param>
        /// <param name="companyNO">供货公司编码</param>
        /// <param name="invoiceNO">发票号</param>
        /// <param name="inBillNO">入库单号</param>
        /// <returns>FS.HISFC.Models.Pharmacy.Pay实体数组</returns>
        public ArrayList QueryUnpay(string stockDeptNO, string companyNO, string invoiceNO, string inBillNO)
        {
            return this.QueryUnpayFromInput(stockDeptNO, companyNO, invoiceNO, inBillNO, "'0','1'");
        }
        /// <summary>
        /// 未付款信息查询
        /// </summary>
        /// <param name="stockDeptNO">库存科室编码</param>
        /// <param name="companyNO">供货公司编码</param>
        /// <param name="invoiceNO">发票号</param>
        /// <param name="inBillNO">入库单号</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>FS.HISFC.Models.Pharmacy.Pay实体数组</returns>
        public ArrayList QueryPay(string stockDeptNO, string companyNO, string invoiceNO, string inBillNO, DateTime beginTime, DateTime endTime)
        {
            string SQL = "";
            if (this.GetSQL("SOC.Pharmacy.Financial.GetPay", ref SQL) == -1)
            {
                SQL = @"
                        select  p.pay_no,
                                p.in_list_code,                            
                                p.invoice_no,  
                                p.company_code,                           
                                p.invoice_date,                           
                                p.purchase_cost,                          
                                p.wholesale_cost,
                                p.retail_cost,
                                p.pay_flag,
                                p.ext_code,
                                p.ext_code1,
                                p.oper_code,
                                p.oper_date
                        from    pha_med_payhead p 
                        where   p.pay_flag <> '0'
                        and     (p.invoice_no = '{2}' or '{2}' = 'All')
                        and     (p.in_list_code = '{3}' or '{3}' = 'All')
                        and     p.drug_dept_code = '{0}'
                        and     p.company_code = '{1}'
                        and     p.oper_date >= to_date('{4}','yyyy-mm-dd hh24:mi:ss')
                        and     p.oper_date <  to_date('{5}','yyyy-mm-dd hh24:mi:ss')
                       ";

                this.CacheSQL("SOC.Pharmacy.Financial.GetPay", SQL);
            }

            try
            {
                if (invoiceNO == "All" && inBillNO == "All")
                {
                    beginTime = DateTime.MinValue;
                    endTime = DateTime.Now.Date.AddDays(1);
                }
                SQL = string.Format(SQL, stockDeptNO, companyNO, invoiceNO, inBillNO, beginTime.ToString(), endTime.ToString());
            }
            catch (Exception ex)
            {
                this.Err = "格式化SOC.Pharmacy.Financial.GetPay出错：" + ex.Message;
                return null;
            }

            ArrayList al = new ArrayList();

            if (this.ExecQuery(SQL) == -1)
            {
                this.Err = "获得结存头表信息时，执行SQL语句出错！" + this.Err;
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Pharmacy.Pay pay = new FS.HISFC.Models.Pharmacy.Pay();
                    pay.ID = this.Reader[0].ToString();
                    pay.InListNO = this.Reader[1].ToString();
                    pay.InvoiceNO = this.Reader[2].ToString();
                    pay.Company.ID = this.Reader[3].ToString();
                    pay.InvoiceTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4]);
                    pay.PurchaseCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                    pay.WholeSaleCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);
                    pay.RetailCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7]);
                    pay.PayState = this.Reader[8].ToString();
                    pay.Extend = this.Reader[9].ToString();
                    pay.Extend1 = this.Reader[10].ToString();
                    pay.Oper.ID = this.Reader[11].ToString();
                    pay.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[12].ToString());

                    if (string.IsNullOrEmpty(pay.PayState))
                    {
                        pay.PayState = "0";
                    }
                    pay.StockDept.ID = stockDeptNO;


                    al.Add(pay);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err = "获取结存头表信息时 由Reader内读取信息发生异常 \n" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 供货商付款
        /// </summary>
        /// <param name="pay">付款实体</param>
        /// <param name="isAddCost">金额是否累加</param>
        /// <returns></returns>
        public int FinCompanyPay(FS.HISFC.Models.Pharmacy.Pay pay, bool isAddCost)
        {
            int param = 0;
            if (string.IsNullOrEmpty(pay.ID))
            {
                pay.ID = this.GetNewPayNO();

                param = this.InsertPayHead(pay);

                if (param == -1)
                {
                    return -1;
                }

                pay.SequenceNO = 1;

                param = this.InsertPayDetail(pay);


                if (param == -1)
                {
                    return -1;
                }

            }
            else
            {
                if (isAddCost)
                {
                    param = this.UpdatePayHead(pay.ID, pay);
                    if (param < 1)
                    {
                        return -1;
                    }
                }
                else
                {
                    param = this.DelPayHead(pay.ID);
                    if (param < 1)
                    {
                        return -1;
                    }

                    param = this.InsertPayHead(pay);
                    if (param < 1)
                    {
                        return -1;
                    }
                }
                int sequenceNo = 0;

                param = this.GetInvoicePaySequence(pay.ID, pay.InvoiceNO, ref sequenceNo);

                pay.SequenceNO = sequenceNo;

                param = this.InsertPayDetail(pay);

                if (param == -1)
                {
                    return -1;
                }
            }

            string strSql = "";
            if (this.GetSQL("SOC.Pharmacy.Financial.CompanyPayInput", ref strSql) == -1)
            {
                strSql = @"
                            update pha_com_input t
                            set    t.pay_state = '{4}'
                            where  t.drug_dept_code = '{0}'
                            and    t.in_list_code = '{1}'
                            and    t.invoice_no = '{2}'
                            and    t.pay_state <> '2'
                            and    t.in_date = to_date('{3}','yyyy-mm-dd hh24:mi:ss')
                          ";

                this.CacheSQL("SOC.Pharmacy.Financial.CompanyPayInput", strSql);
            }
            try
            {
                strSql = string.Format(strSql, pay.StockDept.ID, pay.InListNO, pay.InvoiceNO, pay.User01, pay.PayState);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错SOC.Pharmacy.Financial.PayInput" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 财务发票补录
        /// </summary>
        /// <param name="deptNO">库房科室编码</param>
        /// <param name="inBillNO">入库单号</param>
        /// <param name="invoiceNO">发票号</param>
        /// <returns></returns>
        public int FinInvoiceInput(string deptNO, string inBillNO, string invoiceNO, string operCode, DateTime operTime)
        {
            string strSql = "";
            if (this.GetSQL("SOC.Pharmacy.Financial.FinInvoiceInput", ref strSql) == -1)
            {
                strSql = @"
                            update pha_com_input t
                            set    t.invoice_no = '{2}',
                                   t.in_state = '2',
                                   t.exam_num = t.in_num,
                                   t.exam_opercode = '{3}',
                                   t.exam_date = to_date('{4}','yyyy-mm-dd hh24:mi:ss')
                            where  t.drug_dept_code = '{0}'
                            and    t.in_list_code = '{1}'
                          ";

                this.CacheSQL("SOC.Pharmacy.Financial.FinInvoiceInput", strSql);
            }
            try
            {
                strSql = string.Format(strSql, deptNO, inBillNO, invoiceNO, operCode, operTime.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错SOC.Pharmacy.Financial.FinInvoiceInput" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 取消付款
        /// 删除汇总、明细、更新入库信息
        /// </summary>
        /// <param name="pay"></param>
        /// <returns></returns>
        public int FinCancelCompanyPay(FS.HISFC.Models.Pharmacy.Pay pay)
        {
            if (this.DelPayHead(pay.ID) < 1)
            {
                return -1;
            }

            if (this.DelPayDetail(pay.ID, pay.SequenceNO) < 1)
            {
                return -1;
            }

            string strSql = "";
            if (this.GetSQL("SOC.Pharmacy.Financial.CancelPayInput", ref strSql) == -1)
            {
                strSql = @"
                            update pha_com_input t
                            set    t.pay_state = '{3}'
                            where  t.drug_dept_code = '{0}'
                            and    t.in_list_code = '{1}'
                            and    t.invoice_no = '{2}'                            
                          ";

                this.CacheSQL("SOC.Pharmacy.Financial.CancelPayInput", strSql);
            }
            try
            {
                strSql = string.Format(strSql, pay.StockDept.ID, pay.InListNO, pay.InvoiceNO, "0");
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错SOC.Pharmacy.Financial.CancelPayInput" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新结存头表 一次付款信息
        /// </summary>
        /// <param name="payNo">付款序号</param>
        /// <param name="pay">本次付款信息</param>
        /// <returns>成功返回1 失败返回-1 无记录返回0</returns>
        public int UpdatePayHead(string payNo, FS.HISFC.Models.Pharmacy.Pay pay)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.Item.Pay.UpdatePayHeadInfo", ref strSql) == -1)
            {
                this.Err = "根据Sql索引Pharmacy.Item.Pay.UpdatePayHeadInfo查找Sql出错 \n" + this.Err;
                return -1;
            }
            //格式化SQL语句
            try
            {
                strSql = string.Format(strSql, payNo, pay.PayCost, pay.PayOper.OperTime.ToString(), pay.PayOper.ID);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.Pay.UpdatePayHeadInfo" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 获取同一发票内付款流水号
        /// </summary>
        /// <param name="payNo">付款序号</param>
        /// <param name="invoiceNo">发票号</param>
        /// <param name="sequenceNo">返回的当前最大的同一发票内付款流水号</param>
        /// <returns>成功返回1 失败返回-1</returns>
        public int GetInvoicePaySequence(string payNo, string invoiceNo, ref int sequenceNo)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.Item.Pay.GetInvoicePaySequence", ref strSql) == -1)
            {
                this.Err = "根据Sql索引Pharmacy.Item.Pay.GetInvoicePaySequence查找Sql出错 \n" + this.Err;
                return -1;
            }
            //格式化SQL语句
            try
            {
                strSql = string.Format(strSql, payNo, invoiceNo);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.Pay.GetInvoicePaySequence" + ex.Message;
                return -1;
            }

            string strSequenceNo = this.ExecSqlReturnOne(strSql, "0");
            sequenceNo = FS.FrameWork.Function.NConvert.ToInt32(strSequenceNo) + 1;
            return 1;
        }

        #region 基础增、删、改操作

        /// <summary>
        /// 获取结存头表的Insert或Update参数数组
        /// </summary>
        /// <param name="pay">供货商结存实体</param>
        /// <returns>成功返回参数数组 失败返回null</returns>
        protected string[] myGetParmPayHead(FS.HISFC.Models.Pharmacy.Pay pay)
        {
            try
            {
                string[] parm = {
									pay.ID,							//付款序号
									pay.InListNO,					//入库单据号
									pay.InvoiceNO,					//发票号
									pay.InvoiceTime.ToString(),		//发票日期
									pay.PayCost.ToString(),			//已付金额
									pay.UnPayCost.ToString(),		//未付金额
									pay.PayState,					//付款标志 0未付款  1已付款 2完成付款
									pay.PayOper.OperTime.ToString(),			//完成付款日期
									pay.DeliveryCost.ToString(),	//运费
									pay.RetailCost.ToString(),		//零售金额
									pay.WholeSaleCost.ToString(),	//批发金额
									pay.PurchaseCost.ToString(),	//购入金额（发票金额 ）
									pay.DisCountCost.ToString(),	//优惠金额
									pay.StockDept.ID,				//入库科室
									pay.Company.ID,					//供货单位编码
									pay.Company.Name,				//供货单位名称
									pay.Memo,						//备注
									pay.Oper.ID,					//操作员
									pay.Oper.OperTime.ToString(),		//操作日期
									pay.Extend,					//扩展字段
									pay.Extend1,					//扩展字段1
									pay.Extend2,					//扩展字段2
									pay.ExtendTime.ToString(),			//扩展日期
									pay.ExtendQty.ToString()		//扩展数量
								};
                return parm;
            }
            catch (Exception ex)
            {
                this.Err = "由实体获取参数数组时发生异常 \n" + ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 执行sql语句 获取结存头表信息数组
        /// </summary>
        /// <param name="strSql">欲执行的sql语句</param>
        /// <returns>成功返回pay数组 出错返回null 无记录返回空数组</returns>
        protected ArrayList myGetpayHead(string strSql)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Pharmacy.Pay pay;
            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "获得结存头表信息时，执行SQL语句出错！" + this.Err;
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    pay = new  FS.HISFC.Models.Pharmacy.Pay();
                    pay.ID = this.Reader[0].ToString();								//付款序号
                    pay.InListNO = this.Reader[1].ToString();						//入库单据号
                    pay.InvoiceNO = this.Reader[2].ToString();						//发票号
                    pay.InvoiceTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[3].ToString());	//发票日期
                    pay.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4].ToString());			//已付金额
                    pay.UnPayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5].ToString());		//未付金额
                    pay.PayState = this.Reader[6].ToString();						//付款标志 0未付款  1已付款 2完成付款
                    pay.PayOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7].ToString());		//付款完成日期
                    pay.DeliveryCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[8].ToString());	//运费
                    pay.RetailCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9].ToString());		//零售金额
                    pay.WholeSaleCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10].ToString());	//批发金额
                    pay.PurchaseCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[11].ToString());	//购入金额
                    pay.DisCountCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12].ToString());	//优惠金额
                    pay.StockDept.ID = this.Reader[13].ToString();			//入库科室
                    pay.Company.ID = this.Reader[14].ToString();			//供货单位编码
                    pay.Company.Name = this.Reader[15].ToString();			//供货单位名称
                    pay.Memo = this.Reader[16].ToString();					//备注
                    pay.Oper.ID = this.Reader[17].ToString();				//操作员
                    pay.PayOper.ID = this.Reader[17].ToString();
                    pay.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[18].ToString());		//操作日期
                    pay.Extend = this.Reader[19].ToString();
                    pay.Extend1 = this.Reader[20].ToString();
                    pay.Extend2 = this.Reader[21].ToString();
                    pay.ExtendTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[22].ToString());
                    pay.ExtendQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[23].ToString());


                    al.Add(pay);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err = "获取结存头表信息时 由Reader内读取信息发生异常 \n" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 获取结存明细表的Insert或Update参数数组
        /// </summary>
        /// <param name="pay">供货商结存实体</param>
        /// <returns>成功返回参数数组 失败返回null</returns>
        protected string[] myGetParmPayDetail(FS.HISFC.Models.Pharmacy.Pay pay)
        {
            try
            {
                string[] parm = {
									pay.ID,						//付款序号
									pay.InvoiceNO,				//发票号
									pay.SequenceNO.ToString(),	//同一发票内付款流水号
									pay.PayType,				//付款类型 现金/支票
									pay.Company.OpenBank,		//开户银行
									pay.Company.OpenAccounts,	//银行帐号
									pay.PayCost.ToString(),		//本次付款金额
									pay.UnPayCost.ToString(),	//本次剩余付款金额
									pay.PayOper.ID,				//付款人代码
									pay.PayOper.OperTime.ToString(),		//付款日期
									pay.DeliveryCost.ToString(),//运费
									pay.Oper.ID,				//操作员
									pay.Oper.OperTime.ToString(),	//操作日期
									pay.Memo,					//备注
									pay.Extend,				//扩展字段
									pay.Extend1,				//扩展字段1
									pay.Extend2,				//扩展字段2
									pay.ExtendTime.ToString(),		//扩展日期
									pay.ExtendQty.ToString()	//扩展数量
								};
                return parm;
            }
            catch (Exception ex)
            {
                this.Err = "由实体获取参数数组时发生异常 \n" + ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 执行sql语句 获取结存头表信息数组
        /// </summary>
        /// <param name="strSql">欲执行的sql语句</param>
        /// <returns>成功返回pay数组 出错返回null 无记录返回空数组</returns>
        protected ArrayList myGetPayDetail(string strSql)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Pharmacy.Pay pay;
            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "获得结存头表信息时，执行SQL语句出错！" + this.Err;
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    pay = new  FS.HISFC.Models.Pharmacy.Pay();

                    pay.ID = this.Reader[0].ToString();							//付款序号
                    pay.InvoiceNO = this.Reader[1].ToString();					//发票号
                    pay.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());	//同一发票内付款流水号
                    pay.PayType = this.Reader[3].ToString();					//付款类型（现金，发票）
                    pay.Company.OpenBank = this.Reader[4].ToString();			//开户银行
                    pay.Company.OpenAccounts = this.Reader[5].ToString();		//银行帐号
                    pay.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6].ToString());		//本次付款金额
                    pay.UnPayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7].ToString());	//本次剩余付款金额
                    pay.PayOper.ID = this.Reader[8].ToString();					//付款人
                    pay.PayOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[9].ToString());	//付款日期
                    pay.DeliveryCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10].ToString());//运费
                    pay.Oper.ID = this.Reader[11].ToString();
                    pay.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[12].ToString());
                    pay.Memo = this.Reader[13].ToString();
                    pay.Extend = this.Reader[14].ToString();
                    pay.Extend1 = this.Reader[15].ToString();
                    pay.Extend2 = this.Reader[16].ToString();
                    pay.ExtendTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[17].ToString());
                    pay.ExtendQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[18].ToString());

                    al.Add(pay);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err = "获取结存头表信息时 由Reader内读取信息发生异常 \n" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 插入结存头表
        /// </summary>
        /// <param name="pay">供货商结存实体</param>
        /// <returns>成功返回插入条数 失败返回-1</returns>
        public int InsertPayHead(FS.HISFC.Models.Pharmacy.Pay pay)
        {
            string strSQL = "";
            //取插入操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.Pay.InsertPayHead", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.Pay.InsertPayHead字段!";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetParmPayHead(pay);     //取参数列表
                strSQL = string.Format(strSQL, strParm);					//替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.Pay.InsertPayHead:" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新结存头表
        /// </summary>
        /// <param name="pay">供货商结存实体</param>
        /// <returns>成功返回更新条数 失败返回-1</returns>
        public int UpdateInsertPayHead(FS.HISFC.Models.Pharmacy.Pay pay)
        {
            string strSQL = "";
            //取更新操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.Pay.UpdatePayHead", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.Pay.UpdatePayHead字段!";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetParmPayHead(pay);     //取参数列表
                strSQL = string.Format(strSQL, strParm);				   //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.Pay.UpdatePayHead:" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 删除结存头表
        /// </summary>
        /// <param name="payNo">付款序号</param>
        /// <returns>成功返回删除条数 失败返回-1</returns>
        public int DelPayHead(string payNo)
        {
            string strSQL = "";
            //取删除操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.Pay.DeletePayHead", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.Pay.DeletePayHead字段!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, payNo);    //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.Pay.DeletePayHead:" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 插入结存明细表
        /// </summary>
        /// <param name="pay">供货商结存实体</param>
        /// <returns>成功返回插入条数 失败返回-1</returns>
        public int InsertPayDetail(FS.HISFC.Models.Pharmacy.Pay pay)
        {
            string strSQL = "";
            //取插入操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.Pay.InsertPayDetail", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.Pay.InsertPayDetail字段!";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetParmPayDetail(pay);     //取参数列表
                strSQL = string.Format(strSQL, strParm);					//替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.Pay.InsertPayDetail:" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新结存明细表
        /// </summary>
        /// <param name="pay">供货商结存实体</param>
        /// <returns>成功返回更新条数 失败返回-1</returns>
        public int UpdateInsertPayDetail(FS.HISFC.Models.Pharmacy.Pay pay)
        {
            string strSQL = "";
            //取更新操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.Pay.UpdatePayDetail", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.Pay.UpdatePayDetail字段!";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetParmPayDetail(pay);     //取参数列表
                strSQL = string.Format(strSQL, strParm);				   //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.Pay.UpdatePayDetail" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 删除结存结存表
        /// </summary>
        /// <param name="payNo">付款序号</param>
        /// <returns>成功返回删除条数 失败返回-1</returns>
        public int DelPayDetail(string payNo, int sequenceNo)
        {
            string strSQL = "";
            //取删除操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.Pay.DeletePayDetail", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.Pay.DeletePayDetail字段!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, payNo, sequenceNo);    //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.Pay.DeletePayDetail:" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #endregion      


        #endregion

        #region 财务结算
        /// <summary>
        /// 执行月结存储过程
        /// </summary>
        /// <param name="stockDeptNO">库存科室</param>
        /// <returns>成功执行返回1 失败返回-1</returns>
        public int ExecMonthStatic(string stockDeptNO)
        {
            string strSQL = "";
            if (this.GetSQL("SOC.Pharmacy.Procedure.MonthStatic", ref strSQL) == -1)
            {
                //this.Err = "找不到存储过程执行语句SOC.Pharmacy.Procedure.MonthStatic";
                //return -1;
                strSQL = @"pkg_pha.prc_monthstatic,p_oper_code,22,1,{0},p_drug_dept_code,22,1,{1},p_sql_code,13,2,{2},p_sql_err,22,2,{3}";
            }

            string sqlErr = "";
            int sqlCode = 0;
            try
            {
                strSQL = string.Format(strSQL, this.Operator.ID, stockDeptNO, sqlCode, sqlErr);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            string strReturn = "No Return";
            if (this.ExecEvent(strSQL, ref strReturn) == -1)
            {
                this.Err = strReturn + "执行存储过程出错!prc_monthstatic:" + this.Err;
                this.ErrCode = "prc_monthstatic";
                this.WriteErr();
                return -1;

            }

            if (strReturn != "")
            {
                string[] strParam = strReturn.Split(',');
                if (strParam.Length > 1)
                {
                    if (strParam[0] == "-1")
                    {
                        this.Err = this.Err + strParam[1];
                        return -1;
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// 执行日结存储过程
        /// </summary>
        /// <param name="stockDeptNO">库存科室</param>
        /// <returns>成功执行返回1 失败返回-1</returns>
        public int ExecDailyStatic(string stockDeptNO)
        {
            string strSQL = "";
            if (this.GetSQL("SOC.Pharmacy.Procedure.DailyStatic", ref strSQL) == -1)
            {
                //this.Err = "找不到存储过程执行语句SOC.Pharmacy.Procedure.MonthStatic";
                //return -1;
                strSQL = @"pkg_pha.prc_dailystatic,p_oper_code,22,1,{0},p_drug_dept_code,22,1,{1},p_sql_code,13,2,{2},p_sql_err,22,2,{3}";
            }

            string sqlErr = "";
            int sqlCode = 0;
            try
            {
                strSQL = string.Format(strSQL, this.Operator.ID, stockDeptNO, sqlCode, sqlErr);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            string strReturn = "No Return";
            if (this.ExecEvent(strSQL, ref strReturn) == -1)
            {
                this.Err = strReturn + "执行存储过程出错!prc_dailystatic:" + this.Err;
                this.ErrCode = "prc_dailystatic";
                this.WriteErr();
                return -1;

            }

            if (strReturn != "")
            {
                string[] strParam = strReturn.Split(',');
                if (strParam.Length > 1)
                {
                    if (strParam[0] == "-1")
                    {
                        this.Err = this.Err + strParam[1];
                        return -1;
                    }
                }
            }
            return 1;
        }

        #endregion
    }
}
