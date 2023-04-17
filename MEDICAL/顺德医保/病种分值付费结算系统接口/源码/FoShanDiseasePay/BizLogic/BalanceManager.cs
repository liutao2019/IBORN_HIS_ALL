using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiseasePay.Models;

namespace DiseasePay.BizLogic
{
    public class BalanceManager : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 
        /// </summary>
        public BalanceManager()
        {

        }

        #region 私有方法

        /// <summary>
        /// 执行sql语句 获取结存头表信息数组
        /// </summary>
        /// <param name="strSql">欲执行的sql语句</param>
        /// <returns>成功返回pay数组 出错返回null 无记录返回空数组</returns>
        private List<DiseasePay.Models.BalanceHead> myGetpayHead(string strSql)
        {
            List<BalanceHead> payList = new List<BalanceHead>();
            BalanceHead pay = null;
            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "获得结存头表信息时，执行SQL语句出错！" + this.Err;
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    pay = new BalanceHead();
                    pay.PayHeadNo = this.Reader[0].ToString();//头表编码
                    pay.Company.ID = this.Reader[1].ToString();//供货单位编码
                    pay.InvoiceNo = this.Reader[2].ToString();//发票编码
                    pay.InputDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[3].ToString());//入库日期
                    pay.StockDept.ID = this.Reader[4].ToString();//仓库部门编码
                    pay.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5].ToString());//已付金额
                    pay.UnpayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6].ToString());//未付金额
                    pay.PayState = this.Reader[7].ToString();//付款标志 0未付款  1已付款 2完成付款
                    pay.PayDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8].ToString());//完成付款日期
                    pay.Memo = this.Reader[9].ToString();//备注
                    pay.DeliveryCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10].ToString());//运费
                    pay.InvoiceTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[11].ToString());//发票日期（发票上写的日期）
                    pay.Oper.ID = this.Reader[12].ToString();//操作员
                    pay.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[13].ToString());//操作日期

                    pay.DiscountCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[14].ToString());//优惠金额
                    pay.InListCode = this.Reader[15].ToString();//入库单据号
                    pay.RetailCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16].ToString());//零售金额
                    pay.WholesaleCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17].ToString());//批发金额
                    pay.PurchaseCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[18].ToString());//发票金额

                    payList.Add(pay);
                }
                return payList;
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
        /// 执行sql语句 获取结存头表信息数组
        /// </summary>
        private List<DiseasePay.Models.BalanceHead> myGetPayDetail(string strSql)
        {
            List<BalanceHead> payList = new List<BalanceHead>();
            BalanceHead pay = null;
            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "获得结存明细表信息时，执行SQL语句出错！" + this.Err;
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    pay = new BalanceHead();
                    pay.PayDetailNo = this.Reader[0].ToString();//明细表编码
                    pay.PayHeadNo = this.Reader[1].ToString();//头表编码
                    pay.SequenceNo = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());//同一发票内付款流水号
                    pay.Company.ID = this.Reader[3].ToString();//供货公司编码
                    pay.InvoiceNo = this.Reader[4].ToString();//发票号
                    pay.StockDept.ID = this.Reader[5].ToString();//库存部门编码
                    pay.PayType = this.Reader[6].ToString();//付款类型（现金，发票）
                    pay.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7].ToString());//已付金额
                    pay.UnpayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[8].ToString());//未付金额
                    pay.PayCredence = this.Reader[9].ToString();//付款凭证
                    pay.UnpayCredence = this.Reader[10].ToString();//未付款凭据
                    pay.UnpayCredenceTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[11].ToString());//付款日期
                    pay.DeliveryCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12].ToString());//运费
                    //pay.Company.OpenBank = this.Reader[13].ToString();//开户银行
                    //pay.Company.OpenAccounts = this.Reader[14].ToString();//银行帐号
                    pay.PayOper.ID = this.Reader[15].ToString();//付款人
                    pay.PayDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[16].ToString());//付款日期
                    pay.Memo = this.Reader[17].ToString();//备注
                    pay.Oper.ID = this.Reader[18].ToString();//操作员
                    pay.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[19].ToString());//操作时间
                    pay.ListNum = this.Reader[20].ToString();//流水单号

                    payList.Add(pay);
                }
                return payList;
            }
            catch (Exception ex)
            {
                this.Err = "获取结存明细表信息时 由Reader内读取信息发生异常 \n" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 获取结存头表编号
        /// </summary>
        /// {45FA88B5-A77C-4e2b-B6CE-8364424B0126}
        /// <returns></returns>
        private string GetPayHeadNo()
        {
            string payHeadNo = this.GetSequence("Mat.Seq.PayHeadHeadNo");
            return payHeadNo;
        }
        #endregion

        #region 保护方法

        /// <summary>
        /// 获取结存头表的Insert或Update参数数组
        /// </summary>
        /// <param name="pay">供货商结存实体</param>
        /// <returns>成功返回参数数组 失败返回null</returns>
        protected string[] myGetParmPayHead(DiseasePay.Models.BalanceHead pay)
        {
            try
            {
                string[] parm = {
                                    pay.PayHeadNo,                                                                      //付款序号
                                    pay.Company.ID,                                                                     //供货单位编码
                                    pay.InvoiceNo,                                                                         //发票编码
                                    pay.InputDate.ToString(),                                                         //入库日期
                                    pay.StockDept.ID,                                                                    //仓库部门编码
                                    pay.PayCost.ToString(),                                                            //已付金额
                                    pay.UnpayCost.ToString(),                                                        //未付金额
                                    pay.PayState,                                                                          //付款标志 0未付款  1已付款 2完成付款
                                    pay.PayDate.ToString(),                                            //完成付款日期
                                    pay.Memo,    
                                    pay.DeliveryCost.ToString(),                                                     //运费
                                    pay.InvoiceTime.ToString(),                                                     //发票日期（发票上写的日期）
                                    pay.Oper.ID,                                                                           //操作员
                                    pay.OperDate.ToString(),                                                                //操作日期
                                    pay.DiscountCost.ToString(),                                                    //优惠金额
                                    pay.InListCode,                                                                       //入库单据号
                                    pay.RetailCost.ToString(),                                                        //零售金额
                    　　　　  pay.WholesaleCost.ToString(),                                                 //批发金额
                                    pay.PurchaseCost.ToString()                                                    //发票金额
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
        /// 获取结存明细表的Insert或Update参数数组
        /// </summary>
        /// <param name="pay">供货商结存实体</param>
        /// <returns>成功返回参数数组 失败返回null</returns>
        protected string[] myGetParmPayDetail(DiseasePay.Models.BalanceHead pay)
        {
            try
            {
                string[] parm = {
                      pay.PayDetailNo,//明细表编码
                      pay.PayHeadNo,//头表编码
                      pay.SequenceNo.ToString(),//付款单内序号
                      pay.Company.ID,//供货单位编码
                      pay.InvoiceNo,//发票编码
                      pay.StockDept.ID,//仓库部门编码
                      pay.PayType,//付款类型（现金，发票）
                      pay.PayCost.ToString(),//付款金额
                      pay.UnpayCost.ToString(),//未付金额
                      pay.PayCredence,//付款凭证
                      pay.UnpayCredence,//未付款凭证
                      pay.UnpayCredenceTime.ToString(),//未付款凭证日期
                      pay.DeliveryCost.ToString(),//运费
                      "",//pay.Company.OpenBank,//开户银行
                      "",//pay.Company.OpenAccounts,//银行账号
                      pay.PayOper.ID,//付款人代码
                      pay.PayDate.ToString(),//付款日期
                      pay.Memo,//备注
                      pay.Oper.ID,//操作员
                      pay.OperDate.ToString(),//操作日期
                      pay.ListNum
								};
                return parm;
            }
            catch (Exception ex)
            {
                this.Err = "由实体获取参数数组时发生异常 \n" + ex.Message;
                return null;
            }
        }

        #endregion

        #region 公开方法

        /// <summary>
        /// 获取某入库科室某供货单位的所有发票列表
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="companyID">供货单位编码 </param>
        /// <param name="payFlag">付款标志 0未付款  1已付款 2完成付款 Sql语句采用In的方式 可同时查询多个状态</param>
        /// <param name="dtBegin">查询开始时间</param>
        /// <param name="dtEnd">查询结束时间</param>
        /// <returns>成功返回未结存发票列表 失败返回null</returns>
        public List<DiseasePay.Models.BalanceHead> QueryPayList(string deptID, string companyID, string payFlag, DateTime dtBegin, DateTime dtEnd)
        {
            string strSelect = string.Empty;
            string strWhere = string.Empty;
            if (this.Sql.GetSql("Sun.Procure.PayHead.Select", ref strSelect) == -1)
            {
                this.Err = "没有找到Sun.Procure.PayHead.Select字段!";
                return null;
            }
            if (this.Sql.GetSql("Sun.Procure.PayHead.Where.ByMulQuery", ref strWhere) == -1)
            {
                this.Err = "没有找到Sun.Procure.PayHead.Where.ByMulQuery字段!";
                return null;
            }
            try
            {
                strSelect = strSelect + strWhere;
                strSelect = string.Format(strSelect, deptID, companyID, payFlag, dtBegin.ToString(), dtEnd.ToString());
            }
            catch (Exception ex)
            {
                this.Err = "格式化Sql语句出错" + ex.Message;
                return null;
            }
            return this.myGetpayHead(strSelect);
        }

        /// <summary>
        /// 获取指定条件的列表
        /// </summary>
        /// {45FA88B5-A77C-4e2b-B6CE-8364424B0126}
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<DiseasePay.Models.BalanceHead> QueryPayList(string strWhere)
        {
            string strSelect = string.Empty;
            if (this.Sql.GetSql("Sun.Procure.PayHead.Select", ref strSelect) == -1)
            {
                this.Err = "没有找到Sun.Procure.PayHead.Select字段!";
                return null;
            }
            try
            {
                strSelect = strSelect + strWhere;
            }
            catch (Exception ex)
            {
                this.Err = "格式化Sql语句出错" + ex.Message;
                return null;
            }
            return this.myGetpayHead(strSelect);
        }

        /// <summary>
        /// 获取结存明细信息
        /// </summary>
        /// <param name="payNo">付款序号</param>
        /// <param name="invoiceNo">发票号</param>
        public List<DiseasePay.Models.BalanceHead> QueryPayDetail(string payNo, string invoiceNo)
        {
            string strSQL = string.Empty;
            if (this.Sql.GetSql("Sun.Procure.PayDetail.Select", ref strSQL) == -1)
            {
                this.Err = "没有找到Sun.Procure.PayDetail.Select字段!";
                return null;
            }
            string strWhere = string.Empty;
            if (this.Sql.GetSql("Sun.Procure.PayDetail.Where.ByPayHead", ref strWhere) == -1)
            {
                this.Err = "没有找到Sun.Procure.PayDetail.Where.ByPayHead字段!";
                return null;
            }

            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, payNo, invoiceNo);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Sun.Procure.PayDetail.Select:" + ex.Message;
                return null;
            }
            return this.myGetPayDetail(strSQL);

        }

        /// <summary>
        /// 获取结存明细信息
        /// </summary>
        ///<param name="payListNo"></param>
        public List<DiseasePay.Models.BalanceHead> QueryPayDetail(string payListNo)
        {
            string strSQL = string.Empty;
            if (this.Sql.GetSql("Sun.Procure.PayDetail.GetPay", ref strSQL) == -1)
            {
                this.Err = "没有找到Sun.Procure.PayDetail.Select字段!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, payListNo);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Sun.Procure.PayDetail.Select:" + ex.Message;
                return null;
            }
            return this.myGetPayDetail(strSQL);

        }


        /// <summary>
        /// 更新结存头表
        /// </summary>
        /// <param name="pay">供货商结存实体</param>
        /// <returns>成功返回更新条数 失败返回-1</returns>
        public int UpdateInsertPayHead(DiseasePay.Models.BalanceHead pay)
        {
            string strSQL = string.Empty;
            //取更新操作的SQL语句
            if (this.Sql.GetSql("Sun.Procure.PayHead.Update", ref strSQL) == -1)
            {
                this.Err = "没有找到Sun.Procure.PayHead.Update字段!";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetParmPayHead(pay);     //取参数列表
                strSQL = string.Format(strSQL, strParm);				   //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Sun.Procure.PayHead.Update:" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新结存头表 一次付款信息
        /// </summary>
        public int UpdateHeadPayInfo(DiseasePay.Models.BalanceHead pay)
        {
            string strSql = string.Empty;
            if (this.Sql.GetSql("Sun.Procure.PayHead.UpdateHeadPayInfo", ref strSql) == -1)
            {
                this.Err = "根据Sql索引Sun.Procure.PayHead.UpdateHeadPayInfo查找Sql出错 \n" + this.Err;
                return -1;
            }
            //格式化SQL语句
            try
            {
                strSql = string.Format(strSql, pay.PayHeadNo, pay.PayCost, pay.PayDate.ToString(), pay.PayOper.ID);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Sun.Procure.PayHead.UpdateHeadPayInfo" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// 获取同一发票内付款流水号
        /// </summary>
        public int GetInvoicePaySequence(string payNo, string invoiceNo, ref int sequenceNo)
        {
            string strSql = string.Empty;
            if (this.Sql.GetSql("Sun.Procure.PayDetail.Select.MaxSequence", ref strSql) == -1)
            {
                this.Err = "根据Sql索引Sun.Procure.PayDetail.Select.MaxSequence查找Sql出错 \n" + this.Err;
                return -1;
            }
            //格式化SQL语句
            try
            {
                strSql = string.Format(strSql, payNo, invoiceNo);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Sun.Procure.PayDetail.Select.MaxSequence" + ex.Message;
                return -1;
            }

            string strSequenceNo = this.ExecSqlReturnOne(strSql, "0");
            sequenceNo = FS.FrameWork.Function.NConvert.ToInt32(strSequenceNo) + 1;
            return 1;
        }
        /// <summary>
        /// 取结存单流水号
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public string GetPayListNum(string CompanyID)
        {
            string sql = string.Empty;
            if (this.Sql.GetSql("Sun.Procure.PayDetail.GetPayListNum", ref sql) == -1)
            {
                this.Err = "查询sql：错误";
            }
            try
            {
                sql = string.Format(sql, CompanyID);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Sun.Procure.PayDetail.Select.MaxSequence" + ex.Message;
            }
            return this.ExecSqlReturnOne(sql).ToString();
        }
        /// <summary>
        /// 插入结存明细表
        /// </summary>
        /// <param name="pay">供货商结存实体</param>
        /// <param name="tempPayListNum"></param>
        /// <returns>成功返回插入条数 失败返回-1</returns>
        public int InsertPayDetail(DiseasePay.Models.BalanceHead pay, string tempPayListNum)
        {
            if (string.IsNullOrEmpty(pay.PayDetailNo))
            {
                pay.PayDetailNo = this.GetSequence("Sun.Procure.Seq.PayHeadDetailNo");
                if (pay.PayDetailNo == "-1")
                {
                    pay.PayDetailNo = string.Empty;
                    this.Err = "获取明细表序列失败";
                    return -1;
                }
            }
            string strSQL = string.Empty;
            //取插入操作的SQL语句
            if (this.Sql.GetSql("Sun.Procure.PayDetail.Insert", ref strSQL) == -1)
            {
                this.Err = "没有找到Sun.Procure.PayDetail.Insert字段!";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetParmPayDetail(pay);     //取参数列表


                strParm[strParm.Length - 1] = tempPayListNum;
                strSQL = string.Format(strSQL, strParm);					//替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Sun.Procure.PayDetail.Insert:" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #endregion

        /// <summary>
        /// 根据类别查询供货公司或生产厂家
        /// </summary>
        /// <param name="companyType">公司类别</param>
        /// <param name="isOnlyValid">是否只包含有效记录</param>
        /// <returns>公司列表</returns>
        public List<FS.FrameWork.Models.NeuObject> QueryCompany(string type)
        {
            string sql = string.Empty;

            string sqlIndex = "";
            if (type == "1")
            {
                sqlIndex = "Sun.Procure.Seq.GetDrugCompany"; //药品供货商
            }
            else
            {
                sqlIndex = "Sun.Procure.Seq.GetMatCompany";
            }

            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "没有找到id为" + sqlIndex + "的sql语句!";
                return null;
            }

            List<FS.FrameWork.Models.NeuObject> companyList = new List<FS.FrameWork.Models.NeuObject>();
            FS.FrameWork.Models.NeuObject company;
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "获得物资公司信息时，执行SQL语句出错！" + this.Err;
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    company = new FS.FrameWork.Models.NeuObject();

                    company.ID = this.Reader[0].ToString(); //公司编码
                    company.Name = this.Reader[1].ToString(); //公司名称

                    companyList.Add(company);
                }
                return companyList;
            }
            catch (Exception ex)
            {
                this.Err = "获得物资公司信息时,由Reader内读取信息发生异常 \n" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }


    }
}
