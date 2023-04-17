using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.HISFC.Models.Pharmacy;
using FS.FrameWork.Function;

namespace FS.SOC.HISFC.BizLogic.Pharmacy
{
    public class Plan:Storage
    {
        #region 入库/采购计划操作

        #region 入库计划基础增、删、改操作

        ///<summary>
        ///获得update或者insert入库计划明细信息传入参数数组
        ///</summary>
        ///<param name="inPlan">入库计划信息实体</param>
        ///<returns>字符串数组 失败返回null</returns>
        private string[] myGetParmInPlan(FS.HISFC.Models.Pharmacy.InPlan inPlan)
        {
            string[] strParam = {
									inPlan.ID,                                              // 入库计划单流水号
									inPlan.BillNO,                                          // 采购单号
									inPlan.State,                                           // 状态 0计划单，1采购单，2审核，3已入库 4 作废计划单
									inPlan.PlanType,                                        // 类型 0手工计划，1警戒线，2消耗，3时间，4日消耗
									inPlan.Dept.ID,                                         // 科室编码
									inPlan.Item.ID,                                         // 药品编码
									inPlan.Item.Name,                                       // 药品名称
									inPlan.Item.Specs,                                      // 药品规格
									inPlan.Item.PriceCollection.RetailPrice.ToString(),     // 药品零售价
									inPlan.Item.PriceCollection.WholeSalePrice.ToString(),  // 药品批发价
									inPlan.Item.PriceCollection.PurchasePrice.ToString(),   // 药品购入价
									inPlan.Item.PackUnit,                                   // 药品包装单位
									inPlan.Item.PackQty.ToString(),	                        // 药品包装数量
									inPlan.Item.MinUnit,	                                // 药品最小单位
									inPlan.Item.Product.Producer.ID,                        // 药品生产厂家编码
									inPlan.Item.Product.Producer.Name,                      // 药品生产厂家名称
									inPlan.StoreQty.ToString(),                             // 本科室库存数量
									inPlan.StoreTotQty.ToString(),                          // 全院库存数量
									inPlan.OutputQty.ToString(),	                        // 全院出库总量
									inPlan.PlanQty.ToString(),		                        // 计划入库量
									inPlan.PlanOper.ID,		                                // 计划人
									inPlan.PlanOper.OperTime.ToString(),	                // 计划日期
									inPlan.StockOper.ID,		                            // 采购人
									inPlan.StockOper.OperTime.ToString(),	                // 采购日期
                                    inPlan.StockNO,                                         //采购流水号
                                    inPlan.ReplacePlanNO,                                   //作废、替代流水号
									inPlan.Memo,		                                    // 备注
									inPlan.Oper.ID,		                                    // 操作员
									inPlan.Oper.OperTime.ToString(),
                                    inPlan.Extend,                                           //扩展字段
                                    //by cube 2011-04-28 新增字段
                                    inPlan.Company.ID,
                                    inPlan.InQty.ToString(),
                                    inPlan.InOper.ID,
                                    inPlan.InOper.OperTime.ToString(),
                                    inPlan.StencilName,
                                    inPlan.Formula,
                                    inPlan.StockQty.ToString(),
                                    //end by
                                    //inPlan.SortNO.ToString()                                //顺序号
								};

            return strParam;
        }

        /// <summary>
        /// 取入库计划信息列表，可能是一条或者多条
        /// 私有方法，在其他方法中调用
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <returns>入库计划信息数组</returns>
        private List<FS.HISFC.Models.Pharmacy.InPlan> myGetInPlan(string sqlStr)
        {
            List<FS.HISFC.Models.Pharmacy.InPlan> al = new List<InPlan>();
            FS.HISFC.Models.Pharmacy.InPlan inPlan; //入库计划明细信息实体

            //执行查询语句
            if (this.ExecQuery(sqlStr) == -1)
            {
                this.Err = "获得入库计划明细信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    //取查询结果中的记录
                    inPlan = new FS.HISFC.Models.Pharmacy.InPlan();
                    inPlan.ID = this.Reader[0].ToString();                                  // 入库计划单流水号
                    inPlan.BillNO = this.Reader[1].ToString();                              // 采购单号
                    inPlan.State = this.Reader[2].ToString();                               // 状态0计划单，1采购单，2审核，3已入库 4 作废计划单
                    inPlan.PlanType = this.Reader[3].ToString();                            // 采购类型0手工计划，1警戒线，2消耗，3时间，4日消耗
                    inPlan.Dept.ID = this.Reader[4].ToString();                             // 科室编码 
                    inPlan.Item.ID = this.Reader[5].ToString();                             // 药品编码
                    inPlan.Item.Name = this.Reader[6].ToString();                           // 药品名称
                    inPlan.Item.Specs = this.Reader[7].ToString();                          // 药品规格
                    inPlan.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[8].ToString());        // 药品零售价
                    inPlan.Item.PriceCollection.WholeSalePrice = NConvert.ToDecimal(this.Reader[9].ToString());     // 药品批发价
                    inPlan.Item.PriceCollection.PurchasePrice = NConvert.ToDecimal(this.Reader[10].ToString());     // 药品购入价(最新购入价)
                    inPlan.Item.PackUnit = this.Reader[11].ToString();		                // 药品包装单位
                    inPlan.Item.PackQty = NConvert.ToDecimal(this.Reader[12].ToString());	// 药品包装数量
                    inPlan.Item.MinUnit = this.Reader[13].ToString();	                    // 药品最小单位
                    inPlan.Item.Product.Producer.ID = this.Reader[14].ToString();           // 药品生产厂家编码
                    inPlan.Item.Product.Producer.Name = this.Reader[15].ToString();         // 药品生产厂家名称
                    inPlan.StoreQty = NConvert.ToDecimal(this.Reader[16].ToString());       // 本科室库存数量
                    inPlan.StoreTotQty = NConvert.ToDecimal(this.Reader[17].ToString());    // 全院库存数量
                    inPlan.OutputQty = NConvert.ToDecimal(this.Reader[18].ToString());		// 全院出库总量
                    inPlan.PlanQty = NConvert.ToDecimal(this.Reader[19].ToString());		// 计划入库量
                    inPlan.PlanOper.ID = this.Reader[20].ToString();			            // 计划人
                    inPlan.PlanOper.OperTime = NConvert.ToDateTime(this.Reader[21].ToString());		// 计划日期
                    inPlan.StockOper.ID = this.Reader[22].ToString();			            // 采购人
                    inPlan.StockOper.OperTime = NConvert.ToDateTime(this.Reader[23].ToString());	// 采购日期
                    inPlan.StockNO = this.Reader[24].ToString();                            //采购流水号
                    inPlan.ReplacePlanNO = this.Reader[25].ToString();                      //作废、替代流水号
                    inPlan.Memo = this.Reader[26].ToString();			                    // 备注
                    inPlan.Oper.ID = this.Reader[27].ToString();		                    // 操作员
                    inPlan.Oper.OperTime = NConvert.ToDateTime(this.Reader[28].ToString()); // 操作时间
                    inPlan.Extend = this.Reader[29].ToString();
                    //inPlan.SortNO = NConvert.ToDecimal(this.Reader[30].ToString());        //顺序号
                    //by cube 2011-04-28 新增字段
                    inPlan.Company.ID = this.Reader[30].ToString();
                    inPlan.InQty = NConvert.ToDecimal(this.Reader[31].ToString());
                    inPlan.InOper.ID = this.Reader[32].ToString();
                    inPlan.InOper.OperTime = NConvert.ToDateTime(this.Reader[33]);
                    inPlan.StencilName = this.Reader[34].ToString();
                    inPlan.Formula = this.Reader[35].ToString();
                    inPlan.StockQty = NConvert.ToDecimal(this.Reader[36]);
                    //end by
                    al.Add(inPlan);
                }
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得入库计划明细信息信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// 向采购计划表内插入一条记录
        /// </summary>
        /// <param name="inPlan">入库计划实体</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int InsertInPlan(FS.HISFC.Models.Pharmacy.InPlan inPlan)
        {
            string strSQL = "";
            //取插入操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.InsertInPlan", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.InsertInPlan字段!";
                return -1;
            }
            try
            {
                //取流水号
                inPlan.ID = this.GetSequence("Pharmacy.Item.GetStockPlanID");
                if (inPlan.ID == null)
                    return -1;

                string[] strParm = this.myGetParmInPlan(inPlan);     //取参数列表

                strSQL = string.Format(strSQL, strParm);                     //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.InsertInPlan:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新入库计划表中一条记录，根据流水号更新 只能对状态不为2、3、4的更新
        /// </summary>
        /// <param name="inPlan">入库计划类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int UpdateInPlan(FS.HISFC.Models.Pharmacy.InPlan inPlan)
        {
            string strSQL = "";
            //取更新操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.UpdateInPlan", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateInPlan字段!";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetParmInPlan(inPlan);     //取参数列表

                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.UpdateInPlan:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 删除采购计划表中一条记录
        /// </summary>
        /// <param name="inPlanNO"></param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int DeleteInPlan(string inPlanNO)
        {
            string strSQL = "";
            //取删除操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.DeleteInPlan.PlanNO", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.DeleteInPlan.PlanNO字段!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, inPlanNO);    //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.DeleteInPlan.PlanNO:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 对入库计划单进行整单删除
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="billNO">入库计划单号</param>
        /// <returns></returns>
        public int DeleteInPlan(string deptCode, string billNO, string oldState)
        {
            string strSQL = "";
            //取删除操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.DeleteInPlan.Bill", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.DeleteInPlan.Bill字段!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, deptCode, billNO, oldState);    //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.DeleteInPlan.Bill:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #endregion

        #region 入库计划方法

        /// <summary>
        /// 根据入库计划单据号检索入库计划信息
        /// </summary>
        /// <param name="deptNO">科室编码</param>
        /// <param name="billNO">单据号</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Pharmacy.InPlan> QueryInPlanDetail(string deptNO, string billNO)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.QueryInPlanDetail", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryInPlanDetail字段!";
                return null;
            }

            string strWhere = "";
            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.QueryInPlanDetail.BillNO", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryInPlanDetail.BillNO字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, deptNO, billNO);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.QueryInPlanDetail:" + ex.Message;
                return null;
            }

            return this.myGetInPlan(strSQL);
        }

        /// <summary>
        /// 根据多个入库计划单号检索入库计划信息
        /// </summary>
        /// <param name="deptNO">科室编码</param>
        /// <param name="isSortByBill">是否按单据号对计划信息进行排序 True 按单据号 False 按药品项目</param>
        /// <param name="billNO">入库计划单号</param>
        /// <returns>成功返回入库计划单明细信息</returns>
        public List<FS.HISFC.Models.Pharmacy.InPlan> QueryInPlanDetail(string deptNO, bool isSortByBill, params string[] billNO)
        {
            if (billNO.Length == 1)
                return QueryInPlanDetail(deptNO, billNO[0]);

            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.QueryInPlanDetail", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryInPlanDetail字段!";
                return null;
            }

            string multiBillNO = "";
            foreach (string strBillNO in billNO)
            {
                if (strBillNO == null || strBillNO == "")
                    continue;

                if (multiBillNO == "")
                    multiBillNO = strBillNO;
                else
                    multiBillNO = multiBillNO + "','" + strBillNO;
            }

            string strWhere = "";
            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.QueryInPlanDetail.MultiBillNO", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryInPlanDetail.MultiBillNO字段!";
                return null;
            }
            string strSort = "";
            //数据排序
            if (isSortByBill)           //按单据号排序
            {
                if (this.GetSQL("Pharmacy.Item.QueryInPlanDetail.SortBill", ref strSort) == -1)
                {
                    this.Err = "没有找到Pharmacy.Item.QueryInPlanDetail.SortBill字段!";
                    return null;
                }
            }
            else                        //按药品项目排序
            {
                if (this.GetSQL("Pharmacy.Item.QueryInPlanDetail.SortItem", ref strSort) == -1)
                {
                    this.Err = "没有找到Pharmacy.Item.QueryInPlanDetail.SortItem字段!";
                    return null;
                }
            }

            //格式化SQL语句
            try
            {
                strSQL += " " + strWhere + strSort;
                strSQL = string.Format(strSQL, deptNO, multiBillNO);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.QueryInPlanDetail:" + ex.Message;
                return null;
            }

            return this.myGetInPlan(strSQL);
        }

        /// <summary>
        /// 根据入库计划单流水号检索入库计划信息
        /// </summary>
        /// <param name="planNO">入库计划单流水号</param>
        /// <returns>成功返回入库计划明细信息，失败返回null</returns>
        public List<FS.HISFC.Models.Pharmacy.InPlan> QueryInPlanDetail(string planNO)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.QueryInPlanDetail", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryInPlanDetail字段!";
                return null;
            }
            string strWhere = "";
            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.QueryInPlanDetail.PlanNO", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryInPlanDetail.PlanNO字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, planNO);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.QueryInPlanDetail:" + ex.Message;
                return null;
            }

            return this.myGetInPlan(strSQL);
        }

        /// <summary>
        /// 根据入库单状态获得入库单号、供货公司列表
        /// </summary>
        /// <param name="state">入库计划单状态</param>
        /// <param name="deptNO">库房编码</param>
        /// <returns></returns>
        public ArrayList QueryInPLanList(string deptNO, string state)
        {
            string strSQL = "";
            //取查找记录的SQL语句
            if (this.GetSQL("Pharmacy.Item.QueryInPlanList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryInPlanList字段!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, deptNO, state);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.QueryInPlanList:" + ex.Message;
                this.WriteErr();
                return null;
            }
            ArrayList al = new ArrayList();

            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得采购计划信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    //此语句不能加到循环外面，否则会在al数组内加入相同的数据（最后一条数据）
                    FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
                    info.ID = this.Reader[0].ToString();            //入库单号
                    info.Name = this.Reader[1].ToString();          //计划人
                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得采购计划信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        /// <summary>
        /// 根据入库单状态获得入库单号、供货公司列表
        /// </summary>
        /// <param name="state">入库计划单状态</param>
        /// <param name="deptNO">库房编码</param>
        /// <param name="beginTime">计划制定时间</param>
        /// <param name="endTime">计划制定时间</param>
        /// <returns></returns>
        public ArrayList QueryInPLanList(string deptNO, string state, DateTime beginTime, DateTime endTime)
        {
            string strSQL = "";
            //取查找记录的SQL语句
            if (this.GetSQL("SOC.Pharmacy.Common.InPlanPrive.QueryList.WhereByTime", ref strSQL) == -1)
            {
                this.Err = "没有找到SOC.Pharmacy.Common.InPlanPrive.QueryList.WhereByTime字段!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, deptNO, state, beginTime.ToString(), endTime.ToString());
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错SOC.Pharmacy.Common.InPlanPrive.QueryList.WhereByTime:" + ex.Message;
                this.WriteErr();
                return null;
            }
            ArrayList al = new ArrayList();

            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得采购计划信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    //此语句不能加到循环外面，否则会在al数组内加入相同的数据（最后一条数据）
                    FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
                    info.ID = this.Reader[0].ToString();            //入库单号
                    info.Name = this.Reader[1].ToString();          //计划人
                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得采购计划信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        ///<summary>
        ///根据日消耗获得入库计划
        ///</summary>
        ///<param name="deptNO">库房编码</param>
        ///<returns>成功返回数组，否则返回null</returns>
        public ArrayList InPLanByConsume(string deptNO)
        {
            string strSQL = "";
            //取药品出库总量的SQL语句
            if (this.GetSQL("Pharmacy.Item.OutPutByConsume", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.OutPutByConsume字段!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, deptNO);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.OutPutByConsume:" + ex.Message;
                this.WriteErr();
                return null;
            }
            ArrayList al = new ArrayList();
            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得药品出库总量信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    //此语句不能加到循环外面，否则会在al数组内加入相同的数据（最后一条数据）
                    FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
                    info.ID = this.Reader[0].ToString();     //药品编码
                    info.Name = this.Reader[1].ToString();   //出库总量
                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得药品出库总量信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return null;
        }

        ///<summary>
        ///取入库计划单号
        ///</summary>
        ///<returns>成功返回调价单号：年月日＋四位流水号，失败返回null</returns>
        public string GetPlanBillNO(string deptNO)
        {
            string strSQL = "";
            string temp1, temp2;
            string newBillCode;
            //系统时间 yymmdd
            temp1 = this.GetSysDateNoBar().Substring(2, 6);
            //取最大入库计划单号
            if (this.GetSQL("Pharmacy.Item.GetMaxInPlanBillCode", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetMaxInPlanBillCode字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL = string.Format(strSQL, deptNO);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetMaxInPlanBillCode:" + ex.Message;
                return null;
            }

            temp2 = this.ExecSqlReturnOne(strSQL);
            if (temp2.ToString() == "-1" || temp2.ToString() == "")
            {
                temp2 = "0001";
            }
            else
            {
                decimal i = NConvert.ToDecimal(temp2.Substring(6, 4)) + 1;
                temp2 = i.ToString().PadLeft(4, '0');
            }
            newBillCode = temp1 + temp2;

            return newBillCode;
        }

        /// <summary>
        /// 合并计划单  作废原计划单
        /// </summary>
        /// <param name="newPlanNO">合并后计划单流水号</param>
        /// <param name="cancelPlanNO">被合并的(作废计划单)</param>
        /// <returns>成功返回1 失败返回-1</returns>
        public int CancelInPlan(string newPlanNO, params string[] cancelPlanNO)
        {
            string strSQL = "";
            //取更新操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.CancelInPlan", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.CancelInPlan字段!";
                return -1;
            }
            try
            {
                string cancelParm = "";
                foreach (string strPlanNO in cancelPlanNO)
                {
                    if (cancelParm == "")
                    {
                        cancelParm = strPlanNO;
                    }
                    else
                    {
                        cancelParm = cancelParm + "','" + strPlanNO;
                    }
                }

                strSQL = string.Format(strSQL, newPlanNO, cancelParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.CancelInPlan:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 合并计划单 返回合并后的新计划单实体
        /// </summary>
        /// <param name="inPlanListNO">合并计划单号</param>
        /// <returns>成功返回合并后的新计划单信息 失败返回null</returns>
        public List<FS.HISFC.Models.Pharmacy.InPlan> MergeInPlan(string deptNO, params string[] inPlanListNO)
        {
            List<FS.HISFC.Models.Pharmacy.InPlan> alOriginalInPlanDetail = this.QueryInPlanDetail(deptNO, false, inPlanListNO);
            if (alOriginalInPlanDetail == null)
            {
                this.Err = "根据多个单据号获取入库计划明细发生错误" + this.Err;
                return null;
            }

            if (inPlanListNO.Length == 1)
            {
                return alOriginalInPlanDetail;
            }

            DateTime sysTime = this.GetDateTimeFromSysDateTime();

            string privDrugNO = "";
            List<FS.HISFC.Models.Pharmacy.InPlan> alAlterInPlan = new List<InPlan>();
            FS.HISFC.Models.Pharmacy.InPlan alterInPlan = null;
            foreach (FS.HISFC.Models.Pharmacy.InPlan info in alOriginalInPlanDetail)
            {
                if (privDrugNO == "")               //初始 处理第一条
                {
                    alterInPlan = info.Clone();

                    alterInPlan.ID = "";                                    //流水号
                    alterInPlan.BillNO = "";                                //单据号

                    alterInPlan.Oper.ID = this.Operator.ID;                 //操作人
                    alterInPlan.Oper.OperTime = sysTime;                    //操作时间
                    alterInPlan.PlanOper = alterInPlan.Oper;                //计划人

                    alterInPlan.ReplacePlanNO = info.ID;                    //原单据流水号

                    privDrugNO = info.Item.ID;                              //药品编码

                    continue;
                }
                if (privDrugNO == info.Item.ID)     //处理相同药品
                {
                    alterInPlan.PlanQty = alterInPlan.PlanQty + info.PlanQty;
                    alterInPlan.ReplacePlanNO = alterInPlan.ReplacePlanNO + "|" + info.ID;
                }
                else
                {
                    alAlterInPlan.Add(alterInPlan); //将上一条入库计划信息加入List

                    alterInPlan = info.Clone();

                    alterInPlan.ID = "";                                    //流水号
                    alterInPlan.BillNO = "";                                //单据号

                    alterInPlan.Oper.ID = this.Operator.ID;                 //操作人
                    alterInPlan.Oper.OperTime = sysTime;                    //操作时间
                    alterInPlan.PlanOper = alterInPlan.Oper;                //计划人

                    alterInPlan.ReplacePlanNO = info.ID;                    //原单据流水号

                    privDrugNO = info.Item.ID;
                }
            }

            if (alterInPlan != null)
            {
                alAlterInPlan.Add(alterInPlan);
            }

            return alAlterInPlan;
        }

        /// <summary>
        /// 采购计划制定后更新入库计划信息
        /// </summary>
        /// <param name="planNO"></param>
        /// <param name="stockNO"></param>
        /// <param name="stockOper"></param>
        /// <returns></returns>
        public int UpdateInPlanForStock(string planNO, string stockNO, FS.HISFC.Models.Base.OperEnvironment stockOper)
        {
            string strSQL = "";
            //取更新操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.UpdateInPlanForStock", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateInPlanForStock字段!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, planNO, stockNO, stockOper.ID, stockOper.OperTime.ToString());            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.UpdateInPlanForStock:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }


        /// <summary>
        /// 封存计划
        /// </summary>
        /// <param name="billNO">单号</param>
        /// <param name="stockDeptNO">科室</param>
        /// <returns></returns>
        public int FInPlan(string billNO, string stockDeptNO)
        {
            string strSQL = "";
            //取更新操作的SQL语句
            if (this.GetSQL("SOC.Pharmacy.Common.InPlanPrive.FPlan", ref strSQL) == -1)
            {
                this.Err = "没有找到SOC.Pharmacy.Common.InPlanPrive.FPlan字段!";
                return -1;
            }
            try
            {
                DateTime sysTime = this.GetDateTimeFromSysDateTime();
                strSQL = string.Format(strSQL, billNO, stockDeptNO, this.Operator.ID, sysTime.ToString());            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL:SOC.Pharmacy.Common.InPlanPrive.FPlan语句时出错:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        #endregion

        #region 采购计划基础增、删、改操作

        ///<summary>
        ///获得update或者insert采购计划明细信息传入参数数组
        ///</summary>
        ///<param name="stockPlan">入库计划信息实体</param>
        ///<returns>字符串数组 失败返回null</returns>
        private string[] myGetParmStockPlan(FS.HISFC.Models.Pharmacy.StockPlan stockPlan)
        {
            string[] strParam = {
									stockPlan.ID,                                       // 采购计划单流水号
									stockPlan.BillNO,                                   // 采购单号
									stockPlan.State,                                    // 状态0计划单，1采购单，2审核，3已入库								
									stockPlan.Dept.ID,                                  // 科室编码
									stockPlan.Company.ID,                               // 供药公司编码
									stockPlan.Company.Name,                             // 供货公司名称
									stockPlan.Item.ID,                                  // 药品编码
									stockPlan.Item.Name,                                // 药品名称
									stockPlan.Item.Specs,                               // 药品规格
									stockPlan.Item.PriceCollection.RetailPrice.ToString(),      // 药品零售价
									stockPlan.Item.PriceCollection.WholeSalePrice.ToString(),   // 药品批发价
									stockPlan.Item.PriceCollection.PurchasePrice.ToString(),    // 药品购入价
									stockPlan.Item.PackUnit,                                    // 药品包装单位
									stockPlan.Item.PackQty.ToString(),	                        // 药品包装数量
									stockPlan.Item.MinUnit,	                                    // 药品最小单位
									stockPlan.Item.Product.Producer.ID,                         // 药品生产厂家编码
									stockPlan.Item.Product.Producer.Name,                       // 药品生产厂家名称
                                    NConvert.ToInt32(stockPlan.Item.TenderOffer.IsTenderOffer).ToString(), // 是否招标用药
									stockPlan.StoreQty.ToString(),                              // 本科室库存数量
									stockPlan.StoreTotQty.ToString(),                           // 全院库存数量
									stockPlan.OutputQty.ToString(),	                            // 全院出库总量
									stockPlan.PlanQty.ToString(),		                        // 计划入库量
									stockPlan.PlanOper.ID,		                                // 计划人
									stockPlan.PlanOper.OperTime.ToString(),	                    // 计划日期
                                    stockPlan.PlanNO,                                           // 计划单号									
									stockPlan.StockOper.ID,		                                // 采购人
									stockPlan.StockOper.OperTime.ToString(),	                // 采购日期
									stockPlan.StockApproveQty.ToString(),	                    // 采购数量
                                    stockPlan.StockPrice.ToString(),	                        // 计划购入价
									stockPlan.ApproveOper.ID,	                                // 审批人
									stockPlan.ApproveOper.OperTime.ToString(),	                // 审批时间
									stockPlan.InQty.ToString(),	                                // 实际入库数量
									stockPlan.InOper.ID,	                                    // 入库操作人
									stockPlan.InOper.OperTime.ToString(),		                // 入库时间
									stockPlan.InListNO,		                                    // 入库单据号
									stockPlan.Memo,		                                        // 备注
									stockPlan.Oper.ID,		                                    // 操作员
									stockPlan.Oper.OperTime.ToString(),
                                    stockPlan.Extend,                                           // 扩展操作员
								};

            return strParam;
        }

        /// <summary>
        /// 取采购计划信息列表，可能是一条或者多条
        /// 私有方法，在其他方法中调用
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>入库计划信息数组</returns>
        private ArrayList myGetStockPlan(string SQLString)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Pharmacy.StockPlan stockPlan; //入库计划明细信息实体

            //执行查询语句
            if (this.ExecQuery(SQLString) == -1)
            {
                this.Err = "获得采购计划明细信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    //取查询结果中的记录
                    stockPlan = new FS.HISFC.Models.Pharmacy.StockPlan();

                    stockPlan.ID = this.Reader[0].ToString();                             // 入库计划单流水号
                    stockPlan.BillNO = this.Reader[1].ToString();                         // 采购单号
                    stockPlan.State = this.Reader[2].ToString();                          // 状态0计划单，1采购单，2审核，3已入库
                    stockPlan.Dept.ID = this.Reader[3].ToString();                        // 科室编码 
                    stockPlan.Company.ID = this.Reader[4].ToString();                     // 供药公司编码
                    stockPlan.Company.Name = this.Reader[5].ToString();                   // 供货公司名称
                    stockPlan.Item.ID = this.Reader[6].ToString();                        // 药品编码
                    stockPlan.Item.Name = this.Reader[7].ToString();                      // 药品名称
                    stockPlan.Item.Specs = this.Reader[8].ToString();                     // 药品规格
                    stockPlan.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[9].ToString());       // 药品零售价
                    stockPlan.Item.PriceCollection.WholeSalePrice = NConvert.ToDecimal(this.Reader[10].ToString());   // 药品批发价
                    stockPlan.Item.PriceCollection.PurchasePrice = NConvert.ToDecimal(this.Reader[11].ToString());    // 药品购入价(最新购入价)
                    stockPlan.Item.PackUnit = this.Reader[12].ToString();		                    // 药品包装单位
                    stockPlan.Item.PackQty = NConvert.ToDecimal(this.Reader[13].ToString());	    // 药品包装数量
                    stockPlan.Item.MinUnit = this.Reader[14].ToString();	                        // 药品最小单位
                    stockPlan.Item.Product.Producer.ID = this.Reader[15].ToString();                // 药品生产厂家编码
                    stockPlan.Item.Product.Producer.Name = this.Reader[16].ToString();              // 药品生产厂家名称
                    stockPlan.Item.TenderOffer.IsTenderOffer = NConvert.ToBoolean(this.Reader[17]); // 是否招标用药
                    stockPlan.StoreQty = NConvert.ToDecimal(this.Reader[18].ToString());            // 本科室库存数量
                    stockPlan.StoreTotQty = NConvert.ToDecimal(this.Reader[19].ToString());         // 全院库存数量
                    stockPlan.OutputQty = NConvert.ToDecimal(this.Reader[20].ToString());		    // 全院出库总量
                    stockPlan.PlanQty = NConvert.ToDecimal(this.Reader[21].ToString());		        // 计划入库量
                    stockPlan.PlanOper.ID = this.Reader[22].ToString();			                    // 计划人
                    stockPlan.PlanOper.OperTime = NConvert.ToDateTime(this.Reader[23].ToString());	// 计划日期
                    stockPlan.PlanNO = this.Reader[24].ToString();                                  // 计划流水号
                    stockPlan.StockOper.ID = this.Reader[25].ToString();			                // 采购人
                    stockPlan.StockOper.OperTime = NConvert.ToDateTime(this.Reader[26].ToString());	// 采购日期
                    stockPlan.StockApproveQty = NConvert.ToDecimal(this.Reader[27].ToString());     // 采购数量
                    stockPlan.StockPrice = NConvert.ToDecimal(this.Reader[28].ToString());	        // 计划购入价                   
                    stockPlan.ApproveOper.ID = this.Reader[29].ToString();	                        // 审批人
                    stockPlan.ApproveOper.OperTime = NConvert.ToDateTime(this.Reader[30].ToString());	// 审批时间
                    stockPlan.InQty = NConvert.ToDecimal(this.Reader[31].ToString());		        //  实际入库数量
                    stockPlan.InOper.ID = this.Reader[32].ToString();	                            // 入库操作人
                    stockPlan.InOper.OperTime = NConvert.ToDateTime(this.Reader[33].ToString());    // 入库时间
                    stockPlan.InListNO = this.Reader[34].ToString();		                        // 入库单据号
                    stockPlan.Memo = this.Reader[35].ToString();			                        // 备注
                    stockPlan.Oper.ID = this.Reader[36].ToString();		                            // 操作员
                    stockPlan.Oper.OperTime = NConvert.ToDateTime(this.Reader[37].ToString());      // 操作时间
                    stockPlan.Extend = this.Reader[38].ToString();

                    al.Add(stockPlan);
                }
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得入库计划明细信息信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// 向采购计划表内插入一条记录
        /// </summary>
        /// <param name="stockPlan">入库计划类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int InsertStockPlan(FS.HISFC.Models.Pharmacy.StockPlan stockPlan)
        {
            string strSQL = "";
            //取插入操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.InsertStockPlan", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.InsertStockPlan字段!";
                return -1;
            }
            try
            {
                //取流水号
                stockPlan.ID = this.GetSequence("Pharmacy.Item.GetStockPlanID");
                if (stockPlan.ID == null)
                    return -1;

                string[] strParm = this.myGetParmStockPlan(stockPlan);     //取参数列表

                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.InsertStockPlan:" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新采购计划表中一条记录，只能对所有状态 不等于2 的记录进行更新
        /// </summary>
        /// <param name="stockPlan">采购计划类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int UpdateStockPlan(FS.HISFC.Models.Pharmacy.StockPlan stockPlan)
        {
            string strSQL = "";
            //取更新操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.UpdateStockPlan", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateStockPlan字段!";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetParmStockPlan(stockPlan);     //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.UpdateStockPlan:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新采购计划表中一条记录
        /// </summary>
        /// <param name="stockNO">采购计划号</param>
        /// <param name="inBillNO">入库单据号</param>
        /// <param name="inQty">实际入库量</param>
        /// <param name="inOper">入库人</param>
        /// <param name="state">状态</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int UpdateStockPlanForIn(string stockNO, decimal inQty, string inBillNO, FS.HISFC.Models.Base.OperEnvironment inOper, string state)
        {
            string strSQL = "";
            //取更新操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.UpdateStockPlanForIn", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateStockPlanForIn字段!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, stockNO, inQty.ToString(), inBillNO, inOper.ID, inOper.OperTime.ToString(), state);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.UpdateStockPlanForIn:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 删除采购计划表中一条记录
        /// </summary>
        /// <param name="deptNO">科室编码</param>
        /// <param name="billNO">计划单号</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int DeleteStockPlan(string deptNO, string billNO)
        {
            string strSQL = "";
            //取删除操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.DeleteStockPlan.BillNo", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.DeleteStockPlan.BillNo字段!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, deptNO, billNO);    //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.DeleteStockPlan.BillNo:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 对入库计划单进行整单删除
        /// </summary>
        /// <param name="stockNO">采购流水号</param>
        /// <returns></returns>
        public int DeleteStockPlan(string stockNO)
        {
            string strSQL = "";
            //取删除操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.DeleteStockPlan.StockNO", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.DeleteStockPlan.StockNO字段!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, stockNO);    //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.DeleteStockPlan.StockNO:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #endregion

        #region 采购计划方法

        ///<summary>
        ///根据入库计划单号检索入库计划明细信息
        ///</summary>
        ///<param name="deptNO">库房编码</param>
        ///<param name="billNO">入库计划单号</param>
        ///<returns>入库计划信息数组 失败返回null</returns>
        public ArrayList QueryStockPlanDetail(string deptNO, string billNO)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStockPlan", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockPlanRecord字段!";
                return null;
            }

            string strWhere = "";
            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetNoStockPlan.BillNo", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetNoStockPlan.BillNo字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, deptNO, billNO);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetNoStockPlanRecord:" + ex.Message;
                return null;
            }

            return this.myGetStockPlan(strSQL);

        }

        /// <summary>
        /// 根据入库计划单流水号检索入库计划信息
        /// </summary>
        /// <param name="planNO">入库计划单流水号</param>
        /// <returns>成功返回入库计划明细信息，失败返回null</returns>
        public ArrayList QueryStockPlanDetail(string planNO)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStockPlan", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockPlan字段!";
                return null;
            }
            string strWhere = "";
            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetStockPlan.StockNo", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockPlan.StockNo字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, planNO);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetStockPlanByPlanNo:" + ex.Message;
                return null;
            }

            return this.myGetStockPlan(strSQL);
        }

        /// <summary>
        /// 获取药品的历史采购记录
        /// </summary>
        /// <param name="deptNO">库房编码</param>
        /// <param name="drugNO">药品编码</param>
        /// <returns>成功返回入库计划信息，失败返回null</returns>
        public ArrayList QueryHistoryStockPlan(string deptNO, string drugNO)
        {
            string strSQLWhere = "";
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStockPlan", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockPlan字段!";
                return null;
            }
            //取查找记录的SQL语句
            if (this.GetSQL("Pharmacy.Item.QueryHistoryStockPlan", ref strSQLWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryHistoryStockPlan字段!";
                return null;
            }
            try
            {
                strSQL = strSQL + strSQLWhere;
                strSQL = string.Format(strSQL, deptNO, drugNO);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.QueryHistoryStockPlan:" + ex.Message;
                this.WriteErr();
                return null;
            }

            //取入库计划单明细信息数据
            return this.myGetStockPlan(strSQL);
        }

        /// <summary>
        /// 根据采购单状态获得采购单号、供货公司列表
        /// </summary>
        /// <param name="state">采购计划单状态</param>
        /// <param name="deptNO">库房编码</param>
        /// <returns></returns>
        public ArrayList QueryStockPLanCompanayList(string deptNO, string state)
        {
            string strSQL = "";
            //取查找记录的SQL语句
            if (this.GetSQL("Pharmacy.Item.QueryStockPLanCompanayList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryStockPLanCompanayList字段!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, deptNO, state);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.QueryStockPLanCompanayList:" + ex.Message;
                this.WriteErr();
                return null;
            }
            ArrayList al = new ArrayList();

            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得采购计划信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    //此语句不能加到循环外面，否则会在al数组内加入相同的数据（最后一条数据）
                    FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
                    info.ID = this.Reader[0].ToString();            //采购单号
                    info.User01 = this.Reader[1].ToString();          //供货公司
                    info.Name = this.Reader[2].ToString();       //供货公司编码
                    info.User02 = this.Reader[3].ToString();        //科室编码
                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得采购计划信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// 根据采购单状态和时间获得采购单号、供货公司列表
        /// </summary>
        /// <param name="state">采购计划单状态</param>
        /// <param name="deptNO">库房编码</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        public ArrayList QueryStockPLanCompanayList(string deptNO, string state, string beginDate, string endDate)
        {
            string strSQL = "";
            //取查找记录的SQL语句
            if (this.GetSQL("Pharmacy.Item.QueryStockPLanCompanayList.ByDate", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryStockPLanCompanayList.ByDate字段!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, deptNO, state, beginDate, endDate);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.QueryStockPLanCompanayList.ByDate:" + ex.Message;
                this.WriteErr();
                return null;
            }
            ArrayList al = new ArrayList();

            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得采购计划信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    //此语句不能加到循环外面，否则会在al数组内加入相同的数据（最后一条数据）
                    FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
                    info.ID = this.Reader[0].ToString();            //采购单号
                    info.User01 = this.Reader[1].ToString();          //供货公司
                    info.Name = this.Reader[2].ToString();       //供货公司编码
                    info.User02 = this.Reader[3].ToString();        //科室编码
                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得采购计划信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        ///<summary>
        ///根据科室编码、采购计划单号、供货公司检索采购计划单明细信息
        ///</summary>
        ///<param name="deptNO">库房编码</param>
        ///<param name="billNO">入库计划单号</param>
        ///<param name="companyNO">供货公司编码</param>
        ///<returns>成功返回数组，失败返回null</returns>
        public ArrayList QueryStockPlanByCompany(string deptNO, string billNO, string companyNO)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStockPlan", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockPlan字段!";
                return null;
            }

            string strWhere = "";
            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetStockPlan.Company", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockPlan.Company字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, deptNO, billNO, companyNO);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetStockPlan.Company:" + ex.Message;
                return null;
            }

            //取入库计划单明细信息数据
            return this.myGetStockPlan(strSQL);
        }

        ///<summary>
        ///取采购计划单号
        ///</summary>
        ///<returns>成功返回调价单号：年月日＋四位流水号，失败返回null</returns>
        public string GetStockBillCode(string deptcode)
        {
            string strSQL = "";
            string temp1, temp2;
            string newBillCode;
            //系统时间 yymmdd
            temp1 = this.GetSysDateNoBar().Substring(2, 6);
            //取最大采购计划单号
            if (this.GetSQL("Pharmacy.Item.GetMaxStockBillCode", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetMaxStockBillCode字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL = string.Format(strSQL, deptcode);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetMaxStockBillCode:" + ex.Message;
                return null;
            }

            temp2 = this.ExecSqlReturnOne(strSQL);
            if (temp2.ToString() == "-1" || temp2.ToString() == "")
            {
                temp2 = "0001";
            }
            else
            {
                decimal i = NConvert.ToDecimal(temp2.Substring(6, 4)) + 1;
                temp2 = i.ToString().PadLeft(4, '0');
            }
            newBillCode = temp1 + temp2;

            return newBillCode;
        }

        /// <summary>
        /// 采购计划设置
        /// </summary>
        /// <param name="stockPlan">采购计划信息</param>
        /// <returns>成功返回1 失败返回-1 无数据返回0</returns>
        public int SetStockPlan(FS.HISFC.Models.Pharmacy.StockPlan stockPlan)
        {
            if (stockPlan.ID == "")
            {
                return this.InsertStockPlan(stockPlan);
            }

            int param = this.UpdateStockPlan(stockPlan);
            if (param == 0)
            {
                param = this.InsertStockPlan(stockPlan);
            }

            return param;
        }

        #endregion

        #region 采购计划保存 更新入库计划信息

        /// <summary>
        /// 采购计划保存
        /// </summary>
        /// <param name="stockPlan">采购计划信息</param>
        /// <returns>成功返回1 失败返回-1 无数据返回0</returns>
        public int SaveStockPlan(FS.HISFC.Models.Pharmacy.StockPlan stockPlan)
        {
            int param = this.SetStockPlan(stockPlan);
            if (param != 1)
            {
                return param;
            }
            ////采购计划状态为零 不对入库计划进行处理 尚未进入采购状态
            //if (stockPlan.State == "0")
            //{
            //    return 1;
            //}

            //if (stockPlan.PlanNO == null || stockPlan.PlanNO == "")
            //{
            //    return 1;
            //}

            //if (stockPlan.PlanNO.IndexOf("|") == -1)        //只有一个对应对应的入库单流水号
            //{
            //    param = this.UpdateInPlanForStock(stockPlan.PlanNO, stockPlan.ID, stockPlan.StockOper);
            //}
            //else
            //{
            //    string[] inPlanNOCollection = stockPlan.PlanNO.Split('|');
            //    foreach (string planNO in inPlanNOCollection)
            //    {
            //        param = this.UpdateInPlanForStock(planNO, stockPlan.ID, stockPlan.StockOper);
            //        if (param != 1)
            //        {
            //            this.Err = "更新入库计划流水号 " + planNO + " 入库计划信息未成功";
            //            return -1;
            //        }
            //    }                
            //}

            return param;
        }

        /// <summary>
        /// 采购计划保存
        /// </summary>
        /// <param name="stockPlanCollection">需保存的采购计划信息</param>
        /// <returns>成功返回1 失败返回-1 无操作数据返回0</returns>
        public int SaveStockPlan(List<FS.HISFC.Models.Pharmacy.StockPlan> stockPlanCollection)
        {
            //保存采购计划中相关的入库计划信息 对入库计划进行更新
            System.Collections.Hashtable hsInPlanInfo = new Hashtable();

            FS.HISFC.Models.Base.OperEnvironment stockOper = new FS.HISFC.Models.Base.OperEnvironment();

            foreach (FS.HISFC.Models.Pharmacy.StockPlan info in stockPlanCollection)
            {
                int parma = this.SaveStockPlan(info);
                if (parma == -1)
                {
                    return -1;
                }
                else if (parma == 0)
                {
                    return 0;
                }
                //仍为计划单 则不处理以下信息
                if (info.State == "0")
                {
                    continue;
                }
                //保存采购人员信息
                stockOper = info.StockOper;
                //保存入库计划信息
                if (info.PlanNO.IndexOf("|") == -1)         //只有一个流水号
                {
                    this.AddPlanNOToHs(hsInPlanInfo, info.PlanNO, info.ID);
                }
                else                                        //多个流水号
                {
                    string[] planNOList = info.PlanNO.Split('|');
                    foreach (string planNO in planNOList)
                    {
                        this.AddPlanNOToHs(hsInPlanInfo, planNO, info.ID);
                    }
                }
            }

            #region 处理入库记录 更新入库计划信息内的采购记录

            foreach (string strPlanNO in hsInPlanInfo.Keys)
            {
                int parma = this.UpdateInPlanForStock(strPlanNO, hsInPlanInfo[strPlanNO] as string, stockOper);
                if (parma == -1)
                {
                    return -1;
                }
                else if (parma == 0)
                {
                    this.Err = "原入库计划单信息可能已修改 请重新选择计划单";
                    return 0;
                }
            }

            #endregion

            return 1;
        }

        private void AddPlanNOToHs(System.Collections.Hashtable hsInPlan, string inPlanNO, string stockPlanNO)
        {
            if (hsInPlan.ContainsKey(inPlanNO))         //已包含该入库计划流水号
            {
                //采购计划流水号累加
                hsInPlan[inPlanNO] = (hsInPlan[inPlanNO] as string) + "|" + stockPlanNO;
            }
            else
            {
                //增加计划流水号
                hsInPlan.Add(inPlanNO, stockPlanNO);
            }
        }

        #endregion

        #region 基础增、删、改操作

        /*

        ///<summary>
        ///获得update或者insert入库计划明细信息传入参数数组
        ///</summary>
        ///<param name="stockPlanRecord">入库计划信息实体</param>
        ///<returns>字符串数组 失败返回null</returns>
        private string[] myGetParmStockPlanRecord(FS.HISFC.Models.Pharmacy.StockPlan stockPlanRecord)
        {
            switch (stockPlanRecord.State)
            {
                case "0":
                    stockPlanRecord.PlanOper.ID = this.Operator.ID;
                    stockPlanRecord.PlanOper.OperTime = this.GetDateTimeFromSysDateTime();
                    break;
                case "1":
                    stockPlanRecord.StockOper.ID = this.Operator.ID;
                    stockPlanRecord.StockOper.OperTime = this.GetDateTimeFromSysDateTime();
                    break;
                case "2":
                    stockPlanRecord.ApproveOper.ID = this.Operator.ID;
                    stockPlanRecord.ApproveOper.OperTime = this.GetDateTimeFromSysDateTime();
                    break;
                default:
                    stockPlanRecord.InOper.ID = this.Operator.ID;
                    stockPlanRecord.InOper.OperTime = this.GetDateTimeFromSysDateTime();
                    break;
            }

            string[] strParam = {
									stockPlanRecord.ID, //0 入库计划单流水号
									stockPlanRecord.BillNO, //1 采购单号
									stockPlanRecord.State, //2 单据状态0计划单，1采购单，2审核，3已入库
									stockPlanRecord.PlanType, //3 采购类型0手工计划，1警戒线，2消耗，3时间，4日消耗
									stockPlanRecord.Dept.ID,//4 科室编码
									stockPlanRecord.Company.ID, //5 供药公司编码
									stockPlanRecord.Company.Name, //6 供货公司名称
									stockPlanRecord.Item.ID, //7 药品编码
									stockPlanRecord.Item.Name,//8 药品名称
									stockPlanRecord.Item.Specs, //9 药品规格
									stockPlanRecord.Item.PriceCollection.RetailPrice.ToString(), //10 药品零售价
									stockPlanRecord.Item.PriceCollection.WholeSalePrice.ToString(), //11 药品批发价
									stockPlanRecord.Item.PriceCollection.PurchasePrice.ToString(), //12 药品购入价
									stockPlanRecord.Item.PackUnit, //13 药品包装单位
									stockPlanRecord.Item.PackQty.ToString(),	//14 药品包装数量
									stockPlanRecord.Item.MinUnit,	//15 药品最小单位
									stockPlanRecord.Item.Product.Producer.ID, //16 药品生产厂家编码
									stockPlanRecord.Item.Product.Producer.Name, //17 药品生产厂家名称
									stockPlanRecord.StoreQty.ToString(), //18 本科室库存数量
									stockPlanRecord.StoreTotQty.ToString(), //19 全院库存数量
									stockPlanRecord.OutputQty.ToString(),	//20 全院出库总量
									stockPlanRecord.PlanQty.ToString(),		//21 计划入库量
									stockPlanRecord.PlanOper.ID,		//22 计划人
									stockPlanRecord.PlanOper.OperTime.ToString(),	//23 计划日期
									stockPlanRecord.StockPrice.ToString(),	//24 计划购入价
									stockPlanRecord.StockOper.ID,		//25 采购人
									stockPlanRecord.StockOper.OperTime.ToString(),	//26 采购日期
									stockPlanRecord.ApproveQty.ToString(),	//27 审批数量
									stockPlanRecord.ApproveOper.ID,	//28 审批人
									stockPlanRecord.ApproveOper.OperTime.ToString(),	//29 审批时间
									stockPlanRecord.InQty.ToString(),	//30  实际入库数量
									stockPlanRecord.InOper.ID,	//31 入库操作人
									stockPlanRecord.InOper.OperTime.ToString(),		//32 入库时间
									stockPlanRecord.InListNO,		//33 入库单据号
									stockPlanRecord.Memo,		//34 备注
									stockPlanRecord.Oper.ID,		//35 操作员
									stockPlanRecord.Oper.OperTime.ToString()
								};

            return strParam;
        }

        /// <summary>
        /// 取入库计划信息列表，可能是一条或者多条
        /// 私有方法，在其他方法中调用
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>入库计划信息数组</returns>
        private ArrayList myGetStockPlanRecord(string SQLString)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Pharmacy.StockPlan stockPlanRecord; //入库计划明细信息实体
            this.ProgressBarText = "正在检索人员属性变动信息...";
            this.ProgressBarValue = 0;

            //执行查询语句
            if (this.ExecQuery(SQLString) == -1)
            {
                this.Err = "获得入库计划明细信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    //取查询结果中的记录
                    stockPlanRecord = new FS.HISFC.Models.Pharmacy.StockPlan();
                    stockPlanRecord.ID = this.Reader[0].ToString(); //0 入库计划单流水号
                    stockPlanRecord.BillNO = this.Reader[1].ToString(); //1 采购单号
                    stockPlanRecord.State = this.Reader[2].ToString(); //2 单据状态0计划单，1采购单，2审核，3已入库
                    stockPlanRecord.PlanType = this.Reader[3].ToString(); //3 采购类型0手工计划，1警戒线，2消耗，3时间，4日消耗
                    stockPlanRecord.Dept.ID = this.Reader[4].ToString(); //4 科室编码 
                    stockPlanRecord.Company.ID = this.Reader[5].ToString(); //5 供药公司编码
                    stockPlanRecord.Company.Name = this.Reader[6].ToString(); //6 供货公司名称
                    stockPlanRecord.Item.ID = this.Reader[7].ToString(); //7 药品编码
                    stockPlanRecord.Item.Name = this.Reader[8].ToString(); //8 药品名称
                    stockPlanRecord.Item.Specs = this.Reader[9].ToString(); //9 药品规格
                    stockPlanRecord.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[10].ToString()); //10 药品零售价
                    stockPlanRecord.Item.PriceCollection.WholeSalePrice = NConvert.ToDecimal(this.Reader[11].ToString()); //11 药品批发价
                    stockPlanRecord.Item.PriceCollection.PurchasePrice = NConvert.ToDecimal(this.Reader[12].ToString()); //12 药品购入价(最新购入价)
                    stockPlanRecord.Item.PackUnit = this.Reader[13].ToString();		//13 药品包装单位
                    stockPlanRecord.Item.PackQty = NConvert.ToDecimal(this.Reader[14].ToString());	//14 药品包装数量
                    stockPlanRecord.Item.MinUnit = this.Reader[15].ToString();	//15 药品最小单位
                    stockPlanRecord.Item.Product.Producer.ID = this.Reader[16].ToString(); //16 药品生产厂家编码
                    stockPlanRecord.Item.Product.Producer.Name = this.Reader[17].ToString(); //17 药品生产厂家名称
                    stockPlanRecord.StoreQty = NConvert.ToDecimal(this.Reader[18].ToString()); //18 本科室库存数量
                    stockPlanRecord.StoreTotQty = NConvert.ToDecimal(this.Reader[19].ToString()); //19 全院库存数量
                    stockPlanRecord.OutputQty = NConvert.ToDecimal(this.Reader[20].ToString());		//20 全院出库总量
                    stockPlanRecord.PlanQty = NConvert.ToDecimal(this.Reader[21].ToString());		//21 计划入库量
                    stockPlanRecord.PlanOper.ID = this.Reader[22].ToString();			//22 计划人
                    stockPlanRecord.PlanOper.OperTime = NConvert.ToDateTime(this.Reader[23].ToString());		//23 计划日期
                    stockPlanRecord.StockPrice = NConvert.ToDecimal(this.Reader[24].ToString());	//24 计划购入价
                    stockPlanRecord.StockOper.ID = this.Reader[25].ToString();			//25 采购人
                    stockPlanRecord.StockOper.OperTime = NConvert.ToDateTime(this.Reader[26].ToString());	//26 采购日期
                    stockPlanRecord.ApproveQty = NConvert.ToDecimal(this.Reader[27].ToString());	//27 审批数量
                    stockPlanRecord.ApproveOper.ID = this.Reader[28].ToString();	//28 审批人
                    stockPlanRecord.ApproveOper.OperTime = NConvert.ToDateTime(this.Reader[29].ToString());	//29 审批时间
                    stockPlanRecord.InQty = NConvert.ToDecimal(this.Reader[30].ToString());		//30  实际入库数量
                    stockPlanRecord.InOper.ID = this.Reader[31].ToString();	//31 入库操作人
                    stockPlanRecord.InOper.OperTime = NConvert.ToDateTime(this.Reader[32].ToString());		//32 入库时间
                    stockPlanRecord.InListNO = this.Reader[33].ToString();		//33 入库单据号
                    stockPlanRecord.Memo = this.Reader[34].ToString();			//34 备注
                    stockPlanRecord.Oper.ID = this.Reader[35].ToString();		//35 操作员
                    stockPlanRecord.Oper.OperTime = NConvert.ToDateTime(this.Reader[36].ToString());		//36 操作时间

                    this.ProgressBarValue++;
                    al.Add(stockPlanRecord);
                }
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得入库计划明细信息信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            this.ProgressBarValue = -1;
            return al;
        }

        /// <summary>
        /// 向采购计划表内插入一条记录
        /// </summary>
        /// <param name="stockPlanRecord">入库计划类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int InsertStockPlanRecord(FS.HISFC.Models.Pharmacy.StockPlan stockPlanRecord)
        {
            string strSQL = "";
            //取插入操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.InsertStockPlanRecord", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.InsertStockPlanRecord字段!";
                return -1;
            }
            try
            {
                //取流水号
                stockPlanRecord.ID = this.GetSequence("Pharmacy.Item.GetStockPlanID");
                //if (employeeRecord.ID == null) return -1;
                string[] strParm = myGetParmStockPlanRecord(stockPlanRecord);     //取参数列表

                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.InsertStockPlanRecord:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新采购计划表中一条记录，只能对所有状态 不等于2 的记录进行更新
        /// </summary>
        /// <param name="stockPlanRecord">入库计划类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int UpdateStockPlanRecord(FS.HISFC.Models.Pharmacy.StockPlan stockPlanRecord)
        {
            string strSQL = "";
            //取更新操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.UpdateStockPlanRecord", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateStockPlanRecord字段!";
                return -1;
            }
            try
            {
                string[] strParm = myGetParmStockPlanRecord(stockPlanRecord);     //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.UpdateStockPlanRecord:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新采购计划表中一条记录
        /// </summary>
        /// <param name="stockPlanRecord">入库计划类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int UpdateStockPlanRecordForIn(FS.HISFC.Models.Pharmacy.StockPlan stockPlanRecord)
        {
            string strSQL = "";
            //取更新操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.UpdateStockPlanRecordForIn", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateStockPlanRecord字段!";
                return -1;
            }
            try
            {
                string[] strParm = myGetParmStockPlanRecord(stockPlanRecord);     //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.UpdateStockPlanRecord:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 删除采购计划表中一条记录
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <param name="billNum">计划单号</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int DeleteStockPlanRecord(string deptCode, string billNum)
        {
            string strSQL = "";
            //取删除操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.DeleteStockPlanRecord", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.DeleteStockPlanRecord字段!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, deptCode, billNum);    //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.DeleteStockPlanRecord:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 对入库计划单进行整单删除
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="billCode">入库计划单号</param>
        /// <returns></returns>
        public int DeleteStockPlanByBill(string deptCode, string billCode)
        {
            string strSQL = "";
            //取删除操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.DeleteStockPlanByBill", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.DeleteStockPlanByBill字段!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, deptCode, billCode);    //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.DeleteStockPlanByBill:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        */

        #endregion

        #region 内部使用

        /*

        ///<summary>
        ///取某段时间内某库房的入库计划信息
        ///</summary>
        ///<param name="deptcode">库房编码</param>
        ///<param name="beginDate">计划起始时间</param>
        ///<param name="endDate">计划结束时间</param>
        ///<returns>入库计划明细信息数组，出错返回null</returns>
        public ArrayList QueryDeptStockPlanRecord(string deptcode, DateTime beginDate, DateTime endDate)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStockPlanRecord", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockPlanRecord字段!";
                return null;
            }

            string strWhere = "";
            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetStockPlanRecordList", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockPlanRecordList字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, deptcode, beginDate.ToString(), endDate.ToString());
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetStockPlanRecordList:" + ex.Message;
                return null;
            }

            //取人员属性变动信息数据
            return this.myGetStockPlanRecord(strSQL);
        }

        ///<summary>
        ///根据入库计划单号检索入库计划明细信息
        ///</summary>
        ///<param name="deptCode">库房编码</param>
        ///<param name="billCode">入库计划单号</param>
        ///<returns>入库计划信息数组 失败返回null</returns>
        public ArrayList QueryNoStockPlanRecord(string deptCode, string billCode)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStockPlanRecord", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockPlanRecord字段!";
                return null;
            }

            string strWhere = "";
            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetNoStockPlanRecord", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetNoStockPlanRecord字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, deptCode, billCode);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetNoStockPlanRecord:" + ex.Message;
                return null;
            }

            return this.myGetStockPlanRecord(strSQL);

        }

        /// <summary>
        /// 根据入库计划单流水号检索入库计划信息
        /// </summary>
        /// <param name="planNo">入库计划单流水号</param>
        /// <returns>成功返回入库计划明细信息，失败返回null</returns>
        public ArrayList QueryStockPlanByPlanNo(string planNo)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStockPlanRecord", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockPlanRecord字段!";
                return null;
            }
            string strWhere = "";
            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetStockPlanByPlanNo", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockPlanByPlanNo字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, planNo);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetStockPlanByPlanNo:" + ex.Message;
                return null;
            }

            return this.myGetStockPlanRecord(strSQL);
        }

        /// <summary>
        /// 获取药品的历史采购记录
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="drugCode">药品编码</param>
        /// <param name="state">获取的采购历史记录的状态 2 审核 3 入库</param>
        /// <returns>成功返回入库计划信息，失败返回null</returns>
        public ArrayList QueryHistoryStockPlan(string deptCode, string drugCode, string state)
        {
            string strSQLWhere = "";
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStockPlanRecord", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockPlanRecord字段!";
                return null;
            }
            //取查找记录的SQL语句
            if (this.GetSQL("Pharmacy.Item.GetHistoryStockPlan", ref strSQLWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetHistoryStockPlan字段!";
                return null;
            }
            try
            {
                strSQL = strSQL + strSQLWhere;
                strSQL = string.Format(strSQL, deptCode, drugCode, state);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetHistoryStockPlan:" + ex.Message;
                this.WriteErr();
                return null;
            }

            //取入库计划单明细信息数据
            return this.myGetStockPlanRecord(strSQL);
        }

        /// <summary>
        /// 根据入库单状态获得入库单号、供货公司列表
        /// </summary>
        /// <param name="state">入库计划单状态</param>
        /// <param name="deptcode">库房编码</param>
        /// <returns></returns>
        public ArrayList QueryStockPLanCompanayList(string deptcode, string state)
        {
            string strSQL = "";
            //取查找记录的SQL语句
            if (this.GetSQL("Pharmacy.Item.GetStockPLanCompanayList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockPLanCompanayList字段!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, deptcode, state);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetStockPLanCompanayList:" + ex.Message;
                this.WriteErr();
                return null;
            }
            ArrayList al = new ArrayList();

            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得采购计划信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    //此语句不能加到循环外面，否则会在al数组内加入相同的数据（最后一条数据）
                    FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
                    info.ID = this.Reader[0].ToString();            //入库单号
                    info.Name = this.Reader[1].ToString();          //供货公司
                    info.User01 = this.Reader[2].ToString();       //供货公司编码
                    info.User02 = this.Reader[3].ToString();        //科室编码
                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得采购计划信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        ///<summary>
        ///根据入库计划单科室、状态、时间检索入库计划单信息
        ///</summary>
        ///<param name="deptCode">库房编码</param>
        ///<param name="state">入库计划单状态</param>
        ///<param name="beginDate">起始时间</param>
        ///<param name="endDate">终止时间</param>
        ///<returns>入库计划明细信息数组，出错返回null</returns>
        public ArrayList QueryStateStockPlanRecord(string deptCode, string state, DateTime beginDate, DateTime endDate)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStockPlanRecord", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockPlanRecord字段!";
                return null;
            }
            //根据入库计划单状态确定采用何种时间查询
            string strInfo = "";
            switch (state)
            {
                case "0":			//计划单，计划时间
                    strInfo = "Pharmacy.Item.GetStockPlanRecordByPlanTime";
                    break;
                case "1":			//采购单，采购时间
                    strInfo = "Pharmacy.Item.GetStockPlanRecordByStockTime";
                    break;
                case "2":			//审核单，审核时间
                    strInfo = "Pharmacy.Item.GetStockPlanRecordByApproveTime";
                    break;
                default:			//已入库状态，入库时间
                    strInfo = "Pharmacy.Item.GetStockPlanRecordByInTime";
                    break;
            }

            string strWhere = "";
            //取WHERE语句
            if (this.GetSQL(strInfo, ref strWhere) == -1)
            {
                this.Err = "没有找到" + strInfo + "字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, deptCode, state, beginDate.ToString(), endDate.ToString());
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错" + strInfo + ex.Message;
                return null;
            }

            return this.myGetStockPlanRecord(strSQL);
        }

        ///<summary>
        ///根据科室编码、入库计划单号、供货公司检索入库计划单明细信息
        ///</summary>
        ///<param name="deptCode">库房编码</param>
        ///<param name="billCode">入库计划单号</param>
        ///<param name="companyId">供货公司编码</param>
        ///<returns>成功返回数组，失败返回null</returns>
        public ArrayList QueryStockPlanByCompany(string deptCode, string billCode, string companyId)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStockPlanRecord", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockPlanRecord字段!";
                return null;
            }

            string strWhere = "";
            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetStockPlanByCompany", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockPlanByCompany字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, deptCode, billCode, companyId);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetStockPlanByCompany:" + ex.Message;
                return null;
            }

            //取入库计划单明细信息数据
            return this.myGetStockPlanRecord(strSQL);
        }

        ///<summary>
        ///根据日消耗获得入库计划
        ///</summary>
        ///<param name="deptCode">库房编码</param>
        ///<returns>成功返回数组，否则返回null</returns>
        public ArrayList StockPLanByConsume(string deptCode)
        {
            string strSQL = "";
            //取药品出库总量的SQL语句
            if (this.GetSQL("Pharmacy.Item.OutPutByConsume", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.OutPutByConsume字段!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, deptCode);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.OutPutByConsume:" + ex.Message;
                this.WriteErr();
                return null;
            }
            ArrayList al = new ArrayList();
            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得药品出库总量信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    //此语句不能加到循环外面，否则会在al数组内加入相同的数据（最后一条数据）
                    FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
                    info.ID = this.Reader[0].ToString();     //药品编码
                    info.Name = this.Reader[1].ToString();   //出库总量
                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得药品出库总量信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return null;
        }

        ///<summary>
        ///取入库计划单号
        ///</summary>
        ///<returns>成功返回调价单号：年月日＋四位流水号，失败返回null</returns>
        public string GetBillCode(string deptcode)
        {
            string strSQL = "";
            string temp1, temp2;
            string newBillCode;
            //系统时间 yymmdd
            temp1 = this.GetSysDateNoBar().Substring(2, 6);
            //取最大入库计划单号
            if (this.GetSQL("Pharmacy.Item.GetMaxBillCode", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetMaxBillCode字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL = string.Format(strSQL, deptcode);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetMaxBillCode:" + ex.Message;
                return null;
            }

            temp2 = this.ExecSqlReturnOne(strSQL);
            if (temp2.ToString() == "-1" || temp2.ToString() == "")
            {
                temp2 = "0001";
            }
            else
            {
                decimal i = NConvert.ToDecimal(temp2.Substring(6, 4)) + 1;
                temp2 = i.ToString().PadLeft(4, '0');
            }
            newBillCode = temp1 + temp2;

            return newBillCode;
        }

        */

        #endregion

        #endregion

        #region 公式计算计划
        /// <summary>
        /// 日消耗获取计划
        /// </summary>
        /// <param name="deptNO">计划科室编码</param>
        /// <param name="consumeBeginTime">消耗开始时间</param>
        /// <param name="consumeEndTime">消耗结束时间</param>
        /// <param name="lowDays">下限天数</param>
        /// <param name="upDays">上限天数</param>
        /// <param name="drugType">药品类别</param>
        /// <param name="stencilNO">模板编码</param>
        /// <returns></returns>
        public ArrayList GetPlan(string deptNO, DateTime consumeBeginTime, DateTime consumeEndTime, int lowDays, int upDays, string drugType, string stencilNO)
        {
            if (consumeBeginTime >= consumeEndTime)
            {
                return new ArrayList();
            }
            if (string.IsNullOrEmpty(drugType))
            {
                drugType = "All";
            }
            if (string.IsNullOrEmpty(stencilNO))
            {
                stencilNO = "All";
            }

            //天数
            int spanDays = (consumeEndTime - consumeBeginTime).Days;

            string SQL = @"select  s.drug_code,
                                   u.day_consume * {4} - s.store_sum,
                                   consume_qty,
                                   s.store_sum,
                                   (select sum(ss.store_sum) from pha_com_stockinfo ss where ss.drug_code=s.drug_code group by s.drug_code)
                            from 
                            (
                                select o.drug_dept_code,
                                       o.drug_code,
                                       sum(o.out_num) consume_qty,
                                       round(sum(o.out_num)/{5},2) day_consume
                                from   pha_com_output o
                                where  o.drug_dept_code = '{0}'
                                and    o.out_date > to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                and    o.out_date < to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                                group  by o.drug_dept_code,o.drug_code
                            ) u,pha_com_stockinfo s,pha_com_baseinfo b
                            where  s.drug_code = b.drug_code
                            and    b.valid_state = '1'
                            and    s.valid_state = '1'
                            and    s.drug_dept_code = u.drug_dept_code
                            and    s.drug_code = u.drug_code
                            and    u.day_consume * {3} > s.store_sum
                            and    (s.drug_type = '{6}' or '{6}' = 'All')
                            and    ('{7}' = 'All' or s.drug_code in (
                                    select drug_code from pha_com_drugopen where  stencil_code = '{7}'))";

            SQL = string.Format(SQL,
                deptNO,
                consumeBeginTime.ToString(),
                consumeEndTime.ToString(),
                lowDays.ToString(),
                upDays.ToString(),
                spanDays.ToString(),
                drugType,
                stencilNO);

            if (this.ExecQuery(SQL) == -1)
            {
                return null;
            }

            ArrayList alInPlan = new ArrayList();
            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Pharmacy.InPlan inPlan = new FS.HISFC.Models.Pharmacy.InPlan();
                    inPlan.Item.ID = this.Reader[0].ToString();
                    inPlan.Extend = this.Reader[1].ToString();
                    inPlan.OutputQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
                    inPlan.StoreQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3]);
                    inPlan.StoreTotQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]);
                    inPlan.Formula = "日消耗";

                    alInPlan.Add(inPlan);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                this.Reader.Close();
            }

            return alInPlan;
        }

        /// <summary>
        /// 警戒线获取计划
        /// </summary>
        /// <param name="deptNO">计划科室编码</param>
        /// <param name="drugType">药品类别</param>
        /// <param name="stencilNO">模板编码</param>
        /// <returns></returns>
        public ArrayList GetPlan(string deptNO, string drugType, string stencilNO)
        {
            if (string.IsNullOrEmpty(drugType))
            {
                drugType = "All";
            }
            if (string.IsNullOrEmpty(stencilNO))
            {
                stencilNO = "All";
            }

            string SQL = @"select  s.drug_code,
                                   s.top_sum-s.store_sum,
                                   s.store_sum 
                            from   pha_com_stockinfo s,pha_com_baseinfo b
                            where  s.drug_code = b.drug_code
                            and    b.valid_state = '1'
                            and    s.valid_state = '1'
                            and    s.store_sum < s.low_sum
                            and    s.drug_dept_code = '{0}'
                            and    (s.drug_type = '{1}' or '{1}' = 'All')
                            and    ('{2}' = 'All' or s.drug_code in (
                            select drug_code from pha_com_drugopen where  stencil_code = '{2}'))";

            SQL = string.Format(SQL, deptNO, drugType, stencilNO);

            if (this.ExecQuery(SQL) == -1)
            {
                return null;
            }
            ArrayList alInPlan = new ArrayList();
            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Pharmacy.InPlan inPlan = new FS.HISFC.Models.Pharmacy.InPlan();
                    inPlan.Item.ID = this.Reader[0].ToString();
                    inPlan.Extend = this.Reader[1].ToString();
                    inPlan.StoreQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
                    inPlan.Formula = "警戒线";
                    alInPlan.Add(inPlan);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
            }
            finally
            {
                this.Reader.Close();
            }

            return alInPlan;
        }
        #endregion
    }
}
