using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.FrameWork.Function;
using FS.HISFC.Models.Pharmacy;

namespace FS.SOC.HISFC.BizLogic.Pharmacy
{
    /// <summary>
    /// [功能描述: 药品管理类]<br></br>
    /// [创 建 者: Cube]<br></br>
    /// [创建时间: 2012-12]<br></br>
    /// 说明
    /// 1、零售价调价功能，正常的调价单是不分科室的，这种属于全院调价，获取调价单列表的函数不需要科室
    /// 2、调价表的索引应该是oper_date，inure_time是期望执行时间，实际上执行时间是明细表的oper_date，所
    ///    以对于统计查询inure_time意义不大        
    /// 3、存在单科调价的功能，这个是必须分科室的
    /// </summary>
    public class Adjust : Item
    {
        /// <summary>
        /// 药品调价 存储过程调用
        /// </summary>
        /// <returns> -1 失败 1 成功</returns>
        public int ExecProcedureChangPrice()
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Procedure.pkg_pha.prc_change_price", ref strSQL) == -1)
            {
                this.Err = "找不到存储过程执行语句Pharmacy.Procedure.pkg_pha.prc_change_price";
                return -1;
            }

            string strReturn = "No Return";
            if (this.ExecEvent(strSQL, ref strReturn) == -1)
            {
                this.Err = strReturn + "执行存储过程出错!prc_change_price:" + this.Err;
                this.ErrCode = "PRC_GET_INVOICE";
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
        /// 获取调价单号
        /// </summary>
        /// <returns></returns>
        public string GetAdjustPriceBillNO()
        {
            return this.GetSequence("Pharmacy.Item.GetNewAdjustPriceID");
        }
        
        /// <summary>
        /// 取某一药房中某一张调价单中的数据
        /// </summary>
        /// <param name="billCode">调价单号</param>
        /// <returns>调价信息记录数组，出错返回null</returns>
        public ArrayList QueryAdjustPriceInfoList(string billCode)
        {
            string strSQL = "";
            //string strWhere = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetAdjustPriceInfoist", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetAdjustPriceInfoist字段!";
                return null;
            }

            //格式化SQL语句
            strSQL = string.Format(strSQL, billCode);

            //取调价数据
            return this.myGetAdjustPriceInfo(strSQL);
        }

        /// <summary>
        /// 获取所有调价明细
        /// </summary>
        /// <param name="billCode"></param>
        /// <returns></returns>
        public ArrayList QueryAdjustPriceInfoDetailList(string billCode)
        {
            string strSQL = string.Empty;

            if (this.GetSQL("Pharmacy.Item.GetAdjustPriceInfoist.Detail", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetAdjustPriceInfoist.Detail字段";
                return null;
            }

            strSQL = string.Format(strSQL, billCode);

            return this.myGetAdjustPriceDetailInfo(strSQL);
        }

        /// <summary>
        /// 根据购入价获取调价明细汇总
        /// </summary>
        /// <param name="billCode"></param>
        /// <returns></returns>
        public ArrayList QueryAdjustPriceInfoDetailListByPurhancePrice(string billCode)
        {
            string strSQL = string.Empty;
            if (this.GetSQL("Pharmacy.Item.GetAdjustPriceInfoist.BatchDetail", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetAdjustPriceInfoist.BatchDetail字段";
                return null;
            }
            strSQL = string.Format(strSQL, billCode);
            return this.myGetAdjustPriceDetailInfo(strSQL);
        }




        /// <summary>
        /// 某一段时间的调价单列表
        /// </summary>
        /// <param name="beginTime">制单起始时间</param>
        /// <param name="endTime">制单终止时间</param>
        /// <param name="singleFlag">标记：0全院调价，1单科调价</param>
        /// <param name="detpNO">科室代码</param>
        /// <returns>调价信息记录数组，出错返回null</returns>
        private ArrayList QueryBillList(DateTime beginTime, DateTime endTime, string singleFlag, string detpNO)
        {
            ArrayList al = new ArrayList();
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("SOC.Pharmacy.AdjustPrice.GetBillList.ByOperTime", ref strSQL) == -1)
            {
                strSQL = @"select   distinct 
                                    adjust_number, --调价单序号
                                    inure_time, --调价执行时间
                                    current_state, --调价单状态：0、未调价；1、已调价；2、无效
                                    oper_code, --操作员编码
                                    oper_name, --操作员名称
                                    oper_date, --操作时间
                                    dd_adjust_mark,
                                    ds_adjust_mark,
                                    adjustpricetype
                              from  pha_com_adjustpriceinfo
                             where  oper_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') 
                               and  oper_date <  to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                               and  nvl(dd_adjust_mark, '0') = '{2}'
                               and  (drug_dept_code = '{3}' or 'All' = '{3}')                             
                             order  by current_state, adjust_number desc
                            ";

                this.CacheSQL("SOC.Pharmacy.AdjustPrice.GetBillList.ByOperTime", strSQL);
            }

            //格式化SQL语句
            string[] parm = { beginTime.ToString(), endTime.ToString(), singleFlag, detpNO };
            strSQL = string.Format(strSQL, parm);

            try
            {
                //执行SQL语句，取调价单数据
                if (this.ExecQuery(strSQL) == -1)
                {
                    this.Err = "取调价单列表时出错：" + this.Err;
                    return null;
                }

                AdjustPrice info;  //药品调价信息
                while (this.Reader.Read())
                {
                    info = new AdjustPrice();
                    try
                    {
                        info.ID = this.Reader[0].ToString();                            //调价单号
                        info.InureTime = NConvert.ToDateTime(this.Reader[1].ToString());//生效时间              
                        info.State = this.Reader[2].ToString();                         //调价单状态：0、未调价；1、已调价；2、无效
                        info.Operation.ID = this.Reader[3].ToString();                      //操作员编码
                        info.Operation.Name = this.Reader[4].ToString();                      //操作员名称
                        info.Operation.Oper.OperTime = NConvert.ToDateTime(this.Reader[5].ToString()); //操作时间
                        info.IsDDAdjust = NConvert.ToBoolean(this.Reader[6]);
                        info.IsDSAdjust = NConvert.ToBoolean(this.Reader[7]);
                        info.AdjustPriceType = this.Reader[8].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得调价单列表时出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    al.Add(info);
                }

                return al;
            }
            catch (Exception ex)
            {
                this.Err = "执行Sql语句获取调价信息发生错误 " + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

        }



        /// <summary>
        /// 某一段时间的调价单列表{00FFBF33-AA43-45d0-8644-A5E49ECB54D9}
        /// </summary>
        /// <param name="beginTime">制单起始时间</param>
        /// <param name="endTime">制单终止时间</param>
        /// <param name="singleFlag">标记：0全院调价，1单科调价</param>
        /// <param name="detpNO">科室代码</param>
        /// <returns>调价信息记录数组，出错返回null</returns>
        private ArrayList QueryBillListAndItem(DateTime beginTime, DateTime endTime, string singleFlag, string detpNO,string item)
        {
            ArrayList al = new ArrayList();
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("SOC.Pharmacy.AdjustPrice.GetBillList.ByOperTimeAndItem", ref strSQL) == -1)
            {
                strSQL = @"select   distinct 
                                    adjust_number, --调价单序号
                                    inure_time, --调价执行时间
                                    current_state, --调价单状态：0、未调价；1、已调价；2、无效
                                    oper_code, --操作员编码
                                    oper_name, --操作员名称
                                    oper_date, --操作时间
                                    dd_adjust_mark,
                                    ds_adjust_mark,
                                    adjustpricetype
                              from  pha_com_adjustpriceinfo
                             where  
                                
                               EXISTS(
                                       SELECT o.adjust_number FROM PHA_COM_ADJUSTPRICEDETAIL o WHERE o.adjust_number=pha_com_adjustpriceinfo.adjust_number and o.trade_name like '%{4}%'
                                     )  
                               and  oper_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') 
                               and  oper_date <  to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                               and  nvl(dd_adjust_mark, '0') = '{2}'
                               and  (drug_dept_code = '{3}' or 'All' = '{3}')                            
                             order  by current_state, adjust_number desc
                            ";

                this.CacheSQL("SOC.Pharmacy.AdjustPrice.GetBillList.ByOperTimeAndItem", strSQL);
            }

            //格式化SQL语句
            string[] parm = { beginTime.ToString(), endTime.ToString(), singleFlag, detpNO, item };
            strSQL = string.Format(strSQL, parm);

            try
            {
                //执行SQL语句，取调价单数据
                if (this.ExecQuery(strSQL) == -1)
                {
                    this.Err = "取调价单列表时出错：" + this.Err;
                    return null;
                }

                AdjustPrice info;  //药品调价信息
                while (this.Reader.Read())
                {
                    info = new AdjustPrice();
                    try
                    {
                        info.ID = this.Reader[0].ToString();                            //调价单号
                        info.InureTime = NConvert.ToDateTime(this.Reader[1].ToString());//生效时间              
                        info.State = this.Reader[2].ToString();                         //调价单状态：0、未调价；1、已调价；2、无效
                        info.Operation.ID = this.Reader[3].ToString();                      //操作员编码
                        info.Operation.Name = this.Reader[4].ToString();                      //操作员名称
                        info.Operation.Oper.OperTime = NConvert.ToDateTime(this.Reader[5].ToString()); //操作时间
                        info.IsDDAdjust = NConvert.ToBoolean(this.Reader[6]);
                        info.IsDSAdjust = NConvert.ToBoolean(this.Reader[7]);
                        info.AdjustPriceType = this.Reader[8].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得调价单列表时出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    al.Add(info);
                }

                return al;
            }
            catch (Exception ex)
            {
                this.Err = "执行Sql语句获取调价信息发生错误 " + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

        }
 

        /// <summary>
        /// 某一段时间的调价单列表{00FFBF33-AA43-45d0-8644-A5E49ECB54D9}
        /// </summary>
        /// <param name="beginTime">制单起始时间</param>
        /// <param name="endTime">制单终止时间</param>
        /// <returns>调价信息记录数组，出错返回null</returns>
        public ArrayList QueryGlobalBillList(DateTime beginTime, DateTime endTime)
        {
            return this.QueryBillList(beginTime, endTime, "0", "All");
        }

        /// <summary>
        /// 某一段时间的调价单列表
        /// </summary>
        /// <param name="beginTime">制单起始时间</param>
        /// <param name="endTime">制单终止时间</param>
        /// <returns>调价信息记录数组，出错返回null</returns>
        public ArrayList QueryGlobalBillListAndItem(DateTime beginTime, DateTime endTime,string item)
        {
            return this.QueryBillListAndItem(beginTime, endTime, "0", "All",item);
        }

        /// <summary>
        /// 某一段时间的单科调价单列表{00FFBF33-AA43-45d0-8644-A5E49ECB54D9}
        /// </summary>
        /// <param name="deptNO">科室编码</param>
        /// <param name="beginTime">制单起始时间</param>
        /// <param name="endTime">制单终止时间</param>
        /// <returns>调价信息记录数组，出错返回null</returns>
        public ArrayList QueryBillList(string deptNO, DateTime beginTime, DateTime endTime)
        {
            return this.QueryBillList(beginTime, endTime, "1", deptNO);
        }

        /// <summary>
        /// 某一段时间的单科调价单列表{00FFBF33-AA43-45d0-8644-A5E49ECB54D9}
        /// </summary>
        /// <param name="deptNO">科室编码</param>
        /// <param name="beginTime">制单起始时间</param>
        /// <param name="endTime">制单终止时间</param>
        /// <returns>调价信息记录数组，出错返回null</returns>
        public ArrayList QueryBillListAndItem(string deptNO, DateTime beginTime, DateTime endTime,string item)
        {
            return this.QueryBillListAndItem(beginTime, endTime, "1", deptNO,item);
        }

        /// <summary>
        /// 根据药品编码获取未执行的调价单列表
        /// </summary>
        /// <param name="drugNO">药品编码</param>
        /// <param name="singleFlag">标记：0全院调价，1单科调价</param>
        /// <param name="detpNO">科室代码</param>
        /// <returns></returns>
        private ArrayList QueryUnexcutedBillList(string drugNO, string singleFlag, string deptNO)
        {
            ArrayList al = new ArrayList();
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("SOC.Pharmacy.AdjustPrice.GetBillList.ByDrugNO", ref strSQL) == -1)
            {
                strSQL = @"select   distinct 
                                    adjust_number, --调价单序号
                                    inure_time, --调价执行时间
                                    current_state, --调价单状态：0、未调价；1、已调价；2、无效
                                    oper_code, --操作员编码
                                    oper_name, --操作员名称
                                    oper_date, --操作时间
                                    dd_adjust_mark,
                                    ds_adjust_mark
                              from  pha_com_adjustpriceinfo
                             where  current_state = '0'
                               and  drug_code = '{0}'
                               and  nvl(dd_adjust_mark, '0') = '{1}'
                               and  (drug_dept_code = '{2}' or 'All' = '{2}')                             
                             order  by current_state, adjust_number desc
                            ";
                this.CacheSQL("SOC.Pharmacy.AdjustPrice.GetBillList.ByDrugNO", strSQL);
            }

            //格式化SQL语句
            string[] parm = { drugNO, singleFlag,deptNO };
            strSQL = string.Format(strSQL, parm);

            try
            {
                //执行SQL语句，取调价单数据
                if (this.ExecQuery(strSQL) == -1)
                {
                    this.Err = "取调价单列表时出错：" + this.Err;
                    return null;
                }

                AdjustPrice info;  //药品调价信息
                while (this.Reader.Read())
                {
                    info = new AdjustPrice();
                    try
                    {
                        info.ID = this.Reader[0].ToString();                            //调价单号
                        info.InureTime = NConvert.ToDateTime(this.Reader[1].ToString());//生效时间              
                        info.State = this.Reader[2].ToString();                         //调价单状态：0、未调价；1、已调价；2、无效
                        info.Operation.ID = this.Reader[3].ToString();                      //操作员编码
                        info.Operation.Name = this.Reader[4].ToString();                      //操作员名称
                        info.Operation.Oper.OperTime = NConvert.ToDateTime(this.Reader[5].ToString()); //操作时间
                        info.IsDDAdjust = NConvert.ToBoolean(this.Reader[6]);
                        info.IsDSAdjust = NConvert.ToBoolean(this.Reader[7]);

                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得调价单列表时出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    al.Add(info);
                }

                return al;
            }
            catch (Exception ex)
            {
                this.Err = "执行Sql语句获取调价信息发生错误 " + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

        }

        /// <summary>
        /// 根据药品编码获取未执行的调价单列表
        /// </summary>
        /// <param name="drugNO">药品编码</param>
        /// <returns></returns>
        public ArrayList QueryGlobalUnexcutedBillList(string drugNO)
        {
            return this.QueryUnexcutedBillList(drugNO, "0", "All");
        }

        ///<summary>
        /// 根据药品编码获取未执行的调价单列表
        /// </summary>
        /// <param name="detpNO">科室代码</param>
        /// <param name="drugNO">药品编码</param>
        /// <returns></returns>
        public ArrayList QueryUnexcutedBillList(string deptNO, string drugNO)
        {
            return this.QueryUnexcutedBillList(drugNO, "1", deptNO);
        }


       
        
        /// <summary>
        /// 向调价汇总表中插入一条记录
        /// </summary>
        /// <param name="adjustPrice">库存记录类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int InsertAdjustPriceInfo(FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.InsertAdjustPriceInfo", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.InsertAdjustPriceInfo字段!";
                return -1;
            }
            try
            {
                string[] strParm = myGetParmAdjustPriceInfo(adjustPrice);     //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "插入调价信息SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 向调价明细表中插入一条记录
        /// </summary>
        /// <param name="adjustPrice">库存记录类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int InsertAdjustPriceDetail(FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.InsertAdjustPriceDetail", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.InsertAdjustPriceDetail字段!";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetParmAdjustPriceDetail(adjustPrice);     //取参数列表
                strSQL = string.Format(strSQL, strParm);									//替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "插入调价明细信息SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }


      
        
        /// <summary>
        /// 整单删除未执行的数据
        /// </summary>
        /// <param name="adjustPriceID">调价单号</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int DeleteAdjustPriceInfo(string adjustPriceID)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.DeleteAdjustPriceInfo", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.DeleteAdjustPriceInfo字段!";
                return -1;
            }
            try
            {
                //如果是新增加的调价单，则直接返回
                if (adjustPriceID == "") return 1;
                strSQL = string.Format(strSQL, adjustPriceID);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "删除调价信息SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 删除某条未生效的药品调价信息 by Sunjh 2010-8-31 {B56F6FDF-E7D0-4afd-953A-3006AFE257C1}
        /// </summary>
        /// <param name="adjustPriceID">调价单号</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int DeleteAdjustPriceInfo(string adjustPriceID, string drugCode)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.DeleteAdjustPriceInfo.ByDrugCode", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.DeleteAdjustPriceInfo.ByDrugCode字段!";
                return -1;
            }
            try
            {
                //如果是新增加的调价单，则直接返回
                if (adjustPriceID == "") return 1;
                strSQL = string.Format(strSQL, adjustPriceID, drugCode);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "删除调价信息SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 取调价明细列表
        /// </summary>
        /// <param name="SQLString"></param>
        /// <returns></returns>
        private ArrayList myGetAdjustPriceDetailInfo(string SQLString)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Pharmacy.AdjustPrice adjustPriceDetail;//调价明细实体
            if (this.ExecQuery(SQLString) == -1)
            {
                this.Err = "获得库存信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    adjustPriceDetail = new AdjustPrice();
                    adjustPriceDetail.ID = this.Reader[0].ToString();                                                       
                    //0 调价单号
                    adjustPriceDetail.SerialNO = NConvert.ToInt32(this.Reader[1].ToString());                               
                    //1 调价单内序号
                    adjustPriceDetail.StockDept.ID = this.Reader[2].ToString();                                             
                    //2 库房编码  
                    adjustPriceDetail.Item.ID = this.Reader[3].ToString();                                                  
                    //3 药品编码
                    adjustPriceDetail.Item.Type.ID = this.Reader[4].ToString();                                             
                    //4 药品类别
                    adjustPriceDetail.Item.Quality.ID = this.Reader[5].ToString();                                          
                    //5 药品性质
                    adjustPriceDetail.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[6].ToString());     
                    //6 调价前零售价格
                    adjustPriceDetail.Item.PriceCollection.PurchasePrice = NConvert.ToDecimal(this.Reader[7].ToString());   
                    //7 调价前购入价格
                    adjustPriceDetail.Item.PriceCollection.WholeSalePrice = NConvert.ToDecimal(this.Reader[8].ToString());  
                    //8 调价前加成价格
                    adjustPriceDetail.AfterRetailPrice = NConvert.ToDecimal(this.Reader[9].ToString());                     
                    //9 调价后零售价格
                    adjustPriceDetail.AfterPurhancePrice = NConvert.ToDecimal(this.Reader[10].ToString());                  
                    //10 调价后购入价格
                    adjustPriceDetail.AfterWholesalePrice = NConvert.ToDecimal(this.Reader[11].ToString());                 
                    //11 调价后批发价格
                    adjustPriceDetail.ProfitFlag = this.Reader[12].ToString();                                              
                    //12盈亏标记1-盈，0-亏
                    adjustPriceDetail.InureTime = NConvert.ToDateTime(this.Reader[13].ToString());                          
                    //13调价执行时间
                    adjustPriceDetail.Item.Name = this.Reader[14].ToString();                                               
                    //14药品名
                    adjustPriceDetail.Item.Specs = this.Reader[15].ToString();                                              
                    //15规格
                    adjustPriceDetail.Item.Product.Producer.ID = this.Reader[16].ToString();                                
                    //16生产厂家
                    adjustPriceDetail.Item.PackUnit = this.Reader[17].ToString();                                           
                    //17包装单位
                    adjustPriceDetail.Item.PackQty = NConvert.ToDecimal(this.Reader[18].ToString());                        
                    //18包装数
                    adjustPriceDetail.Item.MinUnit = this.Reader[19].ToString();                                            
                    //19最小单位
                    adjustPriceDetail.State = this.Reader[20].ToString();                                                   
                    //20调价单状态：0、未调价；1、已调价；2、无效
                    adjustPriceDetail.StoreQty = NConvert.ToDecimal(this.Reader[21]);                                       
                    //21库存数量
                    adjustPriceDetail.FileNO = this.Reader[22].ToString(); //22调价文号
                    adjustPriceDetail.Memo = this.Reader[23].ToString();                                                    
                    //23备注
                    adjustPriceDetail.Operation.Oper.ID = this.Reader[24].ToString();                                       
                    //24操作员编码
                    adjustPriceDetail.Operation.Oper.Name = this.Reader[25].ToString();                                     
                    //25操作员名称
                    adjustPriceDetail.Operation.Oper.OperTime = NConvert.ToDateTime(this.Reader[26].ToString());            
                    //26操作时间
                    adjustPriceDetail.IsDDAdjust = NConvert.ToBoolean(this.Reader[27].ToString());
                    adjustPriceDetail.IsDSAdjust = NConvert.ToBoolean(this.Reader[28]);

                    //29调价前零差价
                    if (this.Reader.FieldCount > 29)
                    {
                        adjustPriceDetail.Item.RetailPrice2 = NConvert.ToDecimal(this.Reader[29].ToString());
                    }
                    //30调价后零差价
                    if (this.Reader.FieldCount > 30)
                    {
                        adjustPriceDetail.AfterRetailPrice2 = NConvert.ToDecimal(this.Reader[30].ToString());
                    }
                    al.Add(adjustPriceDetail);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得调价信息时出错！" + ex.Message;
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
        /// 取调价信息列表，可能是一条或者多条库存记录
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>调价信息对象数组</returns>
        private ArrayList myGetAdjustPriceInfo(string SQLString)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice; //调价信息实体

            //执行查询语句
            if (this.ExecQuery(SQLString) == -1)
            {
                this.Err = "获得库存信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    //取查询结果中的记录
                    adjustPrice = new  AdjustPrice();
                    adjustPrice.ID = this.Reader[0].ToString();                                    //0 调价单号
                    adjustPrice.SerialNO = NConvert.ToInt32(this.Reader[1].ToString());            //1 调价单内序号
                    adjustPrice.StockDept.ID = this.Reader[2].ToString();                               //2 库房编码  
                    adjustPrice.Item.ID = this.Reader[3].ToString();                               //3 药品编码
                    adjustPrice.Item.Type.ID = this.Reader[4].ToString();                          //4 药品类别
                    adjustPrice.Item.Quality.ID = this.Reader[5].ToString();                       //5 药品性质
                    adjustPrice.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[6].ToString());  //6 调价前零售价格
                    adjustPrice.Item.PriceCollection.WholeSalePrice = NConvert.ToDecimal(this.Reader[7].ToString()); //7 调价前批发价格
                    adjustPrice.AfterRetailPrice = NConvert.ToDecimal(this.Reader[8].ToString());   //8 调价后零售价格
                    adjustPrice.AfterWholesalePrice = NConvert.ToDecimal(this.Reader[9].ToString()); //9 调价后批发价格
                    adjustPrice.ProfitFlag = this.Reader[10].ToString();                           //10盈亏标记1-盈，0-亏
                    adjustPrice.InureTime = NConvert.ToDateTime(this.Reader[11].ToString());       //11调价执行时间
                    adjustPrice.Item.Name = this.Reader[12].ToString();                             //12药品商品名
                    adjustPrice.Item.Specs = this.Reader[13].ToString();                            //13规格
                    adjustPrice.Item.Product.Producer.ID = this.Reader[14].ToString();                      //14生产厂家
                    adjustPrice.Item.PackUnit = this.Reader[15].ToString();                         //15包装单位
                    adjustPrice.Item.PackQty = NConvert.ToDecimal(this.Reader[16].ToString());      //16包装数
                    adjustPrice.Item.MinUnit = this.Reader[17].ToString();                          //17最小单位
                    adjustPrice.State = this.Reader[18].ToString();                                //18调价单状态：0、未调价；1、已调价；2、无效
                    adjustPrice.FileNO = this.Reader[19].ToString();                                //19招标文件号
                    adjustPrice.Memo = this.Reader[20].ToString();                                 //20备注
                    adjustPrice.Operation.Oper.ID = this.Reader[21].ToString();                              //21操作员编码
                    adjustPrice.Operation.Oper.Name = this.Reader[22].ToString();                             //22操作员名称
                    adjustPrice.Operation.Oper.OperTime = NConvert.ToDateTime(this.Reader[23].ToString());        //23操作时间
                    adjustPrice.IsDDAdjust = NConvert.ToBoolean(this.Reader[24].ToString());

                    adjustPrice.IsDSAdjust = NConvert.ToBoolean(this.Reader[25]);

                    adjustPrice.Item.RetailPrice2 = NConvert.ToDecimal(this.Reader[26]);//调价前零差价

                    adjustPrice.AfterRetailPrice2 = NConvert.ToDecimal(this.Reader[27]);//调价后零差价

                    al.Add(adjustPrice);
                }
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得调价信息时出错！" + ex.Message;
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
        /// 获得update或者insert库存表的传入参数数组
        /// </summary>
        /// <param name="adjustPrice">库存类</param>
        /// <returns>字符串数组 失败返回null</returns>
        private string[] myGetParmAdjustPriceInfo(FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice)
        {

            string[] strParm ={   
								 adjustPrice.ID,                        //0 调价单号
								 adjustPrice.SerialNO.ToString(),           //1 调价单内序号
								 adjustPrice.StockDept.ID,                       //2 库房编码  
								 adjustPrice.Item.ID,                       //3 药品编码
								 adjustPrice.Item.Type.ID,                  //4 药品类别
								 adjustPrice.Item.Quality.ID.ToString(),    //5 药品性质
								 adjustPrice.Item.PriceCollection.RetailPrice.ToString(),   //6 调价前零售价格
								 adjustPrice.Item.PriceCollection.WholeSalePrice.ToString(),//7 调价前批发价格
								 adjustPrice.AfterRetailPrice.ToString(),   //8 调价后零售价格
								 adjustPrice.AfterWholesalePrice.ToString(),//9 调价后批发价格
								 adjustPrice.ProfitFlag,                    //10盈亏标记1-盈，0-亏
								 adjustPrice.InureTime.ToString() ,         //11调价执行时间
								 adjustPrice.Item.Name,                     //12药品商品名
								 adjustPrice.Item.Specs,                    //13规格
								 adjustPrice.Item.Product.Producer.ID,              //14生产厂家
								 adjustPrice.Item.PackUnit,                 //15包装单位
								 adjustPrice.Item.PackQty.ToString(),       //16包装数
								 adjustPrice.Item.MinUnit,                  //17最小单位
								 adjustPrice.State,                         //18调价单状态：0、未调价；1、已调价；2、无效
								 adjustPrice.FileNO,                        //19招标文件号
								 adjustPrice.Memo ,                         //20备注
								 adjustPrice.Operation.Oper.ID,                      //21操作员编码
								 adjustPrice.Operation.Oper.Name,                      //22操作员名称
								 adjustPrice.Operation.Oper.OperTime.ToString(),            //23操作时间
                                 NConvert.ToInt32(adjustPrice.IsDDAdjust).ToString(),
                                 NConvert.ToInt32(adjustPrice.IsDSAdjust).ToString(),
                                 adjustPrice.AdjustPriceType,                //26调价类型
                                 adjustPrice.Item.RetailPrice2.ToString(),   //27调价前零差价
                                 adjustPrice.AfterRetailPrice2.ToString()    //28调价后零差价
							 };
            return strParm;
        }

        /// <summary>
        /// 获得update或者insert库存表的传入参数数组 操作调价明细表
        /// </summary>
        /// <param name="adjustPrice">调价实体</param>
        /// <returns>字符串数组 失败返回null</returns>
        private string[] myGetParmAdjustPriceDetail(FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice)
        {
            string[] strParm ={   
								 adjustPrice.ID,							//0 调价单号
								 adjustPrice.SerialNO.ToString(),           //1 调价单内序号
								 adjustPrice.StockDept.ID,                       //2 库房编码  
								 adjustPrice.Item.ID,                       //3 药品编码
								 adjustPrice.Item.Name,                     //4 药品商品名
								 adjustPrice.Item.Type.ID,                  //5 药品类别
								 adjustPrice.Item.Quality.ID.ToString(),    //6 药品性质
								 adjustPrice.Item.Specs,                    //7 规格
								 adjustPrice.Item.Product.Producer.ID,              //8 生产厂家
								 adjustPrice.Item.PackUnit,                 //9 包装单位
								 adjustPrice.Item.PackQty.ToString(),       //10包装数
								 adjustPrice.Item.MinUnit,                  //11最小单位
								 adjustPrice.Item.PriceCollection.RetailPrice.ToString(),   //12调价前零售价格
								 adjustPrice.Item.PriceCollection.WholeSalePrice.ToString(),//13调价前批发价格
								 adjustPrice.AfterRetailPrice.ToString(),   //14调价后零售价格
								 adjustPrice.AfterWholesalePrice.ToString(),//15调价后批发价格
								 adjustPrice.StoreQty.ToString(),			//16调价时库存量
								 adjustPrice.ProfitFlag,                    //17盈亏标记1-盈，0-亏
								 adjustPrice.InureTime.ToString() ,         //18调价执行时间								 
								 adjustPrice.State,                         //19调价单状态：0、未调价；1、已调价；2、无效
								 adjustPrice.Operation.Oper.ID,                      //20操作员编码
								 adjustPrice.Operation.Oper.OperTime.ToString(),           //21操作时间
								 adjustPrice.Memo							//22备注
							 };
            return strParm;
        }

        /// <summary>
        /// 获得update或者insert库存表的传入参数数组 操作调价明细表
        /// </summary>
        /// <param name="adjustPrice">调价实体</param>
        /// <returns>字符串数组 失败返回null</returns>
        private string[] myGetParmAdjustPurcahsePriceInfo(FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice)
        {
            string[] strParm ={   
								 adjustPrice.ID,							//0 调价单号
								 adjustPrice.SerialNO.ToString(),           //1 调价单内序号
								 adjustPrice.StockDept.ID,                       //2 库房编码  
								 adjustPrice.Item.ID,                       //3 药品编码
								 adjustPrice.Item.Name,                     //4 药品商品名
                                 adjustPrice.GroupNO.ToString(),                        //5批次号
								 adjustPrice.Item.Type.ID,                  //6 药品类别
								 adjustPrice.Item.Quality.ID.ToString(),    //7 药品性质
								 adjustPrice.Item.Specs,                    //8 规格
								 adjustPrice.Item.Product.Producer.ID,       //9 生产厂家
								 adjustPrice.Item.PackUnit,                 //10 包装单位
								 adjustPrice.Item.PackQty.ToString(),       //11包装数
								 adjustPrice.Item.MinUnit,                  //12最小单位
								 adjustPrice.PriceCollection.WholeSalePrice.ToString(),   //13调价前购入价格							
								 adjustPrice.AfterWholesalePrice.ToString(),//14调价后购入价格
								 adjustPrice.StoreQty.ToString(),			//15调价时库存量
								 adjustPrice.ProfitFlag,                    //16盈亏标记1-盈，0-亏
								 adjustPrice.InureTime.ToString() ,         //17调价执行时间								 
								 adjustPrice.State,                         //18调价单状态：0、未调价；1、已调价；2、无效
								 adjustPrice.Operation.Oper.ID,                      //19操作员编码
								 adjustPrice.Operation.Oper.OperTime.ToString(),           //20操作时间
								 adjustPrice.Memo,							//21备注
                                 adjustPrice.FileNO                         //22入库单号
							 };
            return strParm;
        }

        /// <summary>
        /// 单科室调价更新库存零售价
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <param name="drugCode">药品编码</param>
        /// <param name="retailPrice">新零售价格</param>
        /// <returns>成功返回1 失败返回-1</returns>
        public int UpdateStoragePrice(string deptCode, string drugCode, decimal retailPrice)
        {
            string strSQL = "";
            //取SQL语句。
            if (this.GetSQL("Pharmacy.Item.UpdateStoragePrice", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateStoragePrice字段!";
                return -1;
            }
            try
            {
                //取参数列表
                string[] strParm = {
									   deptCode,                       //库存科室编码
									   drugCode,                       //药品编码
									   retailPrice.ToString(),         //预扣变化数量
									   this.Operator.ID                //操作人
								   };

                strSQL = string.Format(strSQL, strParm);        //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "更新库存汇总表中的零售价时出错！Pharmacy.Item.UpdateStoragePrice" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

       
        
        
        
        
        /// <summary>
        /// 获得零售价的调价公式参数数组
        /// </summary>
        /// <param name="formula">公式实体</param>
        /// <returns>字符串数组 失败返回null</returns>
        private string[] myGetParmAdjustPriceFormula(FS.SOC.HISFC.Models.Pharmacy.Adjust.RetailPriceFormula formula)
        {

            string[] strParm ={   
								 formula.ID,                        //编号
								 formula.DrugType.ID,
                                 formula.DrugType.Name,
                                 formula.PriceType,
                                 formula.PriceLower.ToString(),
                                 formula.PriceUpper.ToString(),
                                 formula.Name,
                                 formula.ValidState,
                                 formula.OperID,
                                 formula.OperTime.ToString(),
                                 formula.FomulaType
							 };
            return strParm;
        }

        /// <summary>
        /// 插入零售价的调价公式
        /// </summary>
        /// <param name="formula">公式实体</param>
        /// <returns></returns>
        public int InsertAdjustPriceFormula(FS.SOC.HISFC.Models.Pharmacy.Adjust.RetailPriceFormula formula)
        {
            string strSQL = "";
            if (this.GetSQL("SOC.Pharmacy.Adjust.Formula.Insert", ref strSQL) == -1)
            {
                strSQL = @"insert into pha_soc_adjust_formula
                              (
                                id,
                                drug_type,
                                drug_type_name,
                                price_type,
                                limit_lower,
                                limit_upper,
                                formula,
                                valid_state,
                                oper_code,
                                oper_date,
                                isbasedrug
                              )
                              values
                              (
                                '{0}',
                                '{1}',
                                '{2}',
                                '{3}',
                                {4},
                                {5},
                                '{6}',
                                '{7}',
                                '{8}',
                                to_date('{9}','yyyy-mm-dd hh24:mi:ss'),
                                '{10}'
                              )";
                this.CacheSQL("SOC.Pharmacy.Adjust.Formula.Insert", strSQL);
            }
            try
            {
                string[] strParm = myGetParmAdjustPriceFormula(formula);     //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "插入调价公式信息SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 插入零售价的调价公式
        /// </summary>
        /// <param name="ID">公式编号</param>
        /// <returns></returns>
        public int DeleteAdjustPriceFormula(string ID)
        {
            string strSQL = "";
            if (this.GetSQL("SOC.Pharmacy.Adjust.Formula.Delete", ref strSQL) == -1)
            {
                strSQL = @"delete from pha_soc_adjust_formula where id = '{0}'";
            }
            try
            {
                strSQL = string.Format(strSQL, ID);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "删除调价公式时SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 零售价的调价公式
        /// </summary>
        /// <returns>零售价的调价公式实体数组</returns>
        public ArrayList QueryAdjustPriceFormula()
        {
            string strSQL = "";
            if (this.GetSQL("SOC.Pharmacy.Adjust.Formula.Query", ref strSQL) == -1)
            {
                strSQL = @"select id,
                           drug_type,
                           drug_type_name,
                           price_type,
                           limit_lower,
                           limit_upper,
                           formula,
                           valid_state,
                           oper_code,
                           oper_date,
                           isbasedrug
                      from pha_soc_adjust_formula";

                this.CacheSQL("SOC.Pharmacy.Adjust.Formula.Query", strSQL);
            }

            ArrayList al = new ArrayList();

            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获取调价公式信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    FS.SOC.HISFC.Models.Pharmacy.Adjust.RetailPriceFormula formula = new FS.SOC.HISFC.Models.Pharmacy.Adjust.RetailPriceFormula();
                    
                    formula.ID = this.Reader[0].ToString();
                    formula.DrugType.ID = this.Reader[1].ToString();
                    formula.DrugType.Name = this.Reader[2].ToString();
                    formula.PriceType = this.Reader[3].ToString();
                    formula.PriceLower = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]);
                    formula.PriceUpper = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                    formula.Name = this.Reader[6].ToString();
                    formula.ValidState = this.Reader[7].ToString();
                    formula.OperID = this.Reader[8].ToString();
                    formula.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[9]);
                    formula.FomulaType = this.Reader[10].ToString();

                    al.Add(formula);
                }
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得调价公式信息时出错！" + ex.Message;
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
        /// 获得特殊药品零售价的调价公式参数数组
        /// </summary>
        /// <param name="formula">公式实体</param>
        /// <returns>字符串数组 失败返回null</returns>
        private string[] myGetParmAdjustPriceSpeFormula(FS.SOC.HISFC.Models.Pharmacy.Adjust.SpecialDrugFormula formula)
        {

            string[] strParm ={   
								 formula.DrugNO,                        //编号
								 formula.DrugName,
                                 formula.DrugSpecs,
                                 formula.Formula,
                                 formula.ValidState,
                                 formula.OperID,
                                 formula.OperTime.ToString()
							 };
            return strParm;
        }

        /// <summary>
        /// 插入零售价的调价公式
        /// </summary>
        /// <param name="formula">公式实体</param>
        /// <returns></returns>
        public int InsertAdjustPriceSpeFormula(FS.SOC.HISFC.Models.Pharmacy.Adjust.SpecialDrugFormula formula)
        {
            string strSQL = "";
            if (this.GetSQL("SOC.Pharmacy.Adjust.SpeFormula.Insert", ref strSQL) == -1)
            {
                strSQL = @"insert into pha_soc_adjust_speformula
                              (
                                   drug_code,
                                   trade_name,
                                   specs,
                                   formula,
                                   valid_state,
                                   oper_code,
                                   oper_date
                              )
                              values
                              (
                                '{0}',
                                '{1}',
                                '{2}',
                                '{3}',                             
                                '{4}',
                                '{5}',
                                to_date('{6}','yyyy-mm-dd hh24:mi:ss')
                              )";

                this.CacheSQL("SOC.Pharmacy.Adjust.SpeFormula.Insert", strSQL);
            }
            try
            {
                string[] strParm = this.myGetParmAdjustPriceSpeFormula(formula);     //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "插入调价公式信息SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 插入零售价的调价公式
        /// </summary>
        /// <param name="ID">公式编号</param>
        /// <returns></returns>
        public int DeleteAdjustPriceSpeFormula(string ID)
        {
            string strSQL = "";
            if (this.GetSQL("SOC.Pharmacy.Adjust.SpeFormula.Delete", ref strSQL) == -1)
            {
                strSQL = @"delete from pha_soc_adjust_speformula where drug_code = '{0}'";
            }
            try
            {
                strSQL = string.Format(strSQL, ID);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "删除调价公式信息SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        private ArrayList myQueryAdjustPriceSpeFormula(string strSQL)
        {
            ArrayList al = new ArrayList();
            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获取调价公式时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    FS.SOC.HISFC.Models.Pharmacy.Adjust.SpecialDrugFormula formula = new FS.SOC.HISFC.Models.Pharmacy.Adjust.SpecialDrugFormula();

                    formula.ID = this.Reader[0].ToString();
                    formula.DrugNO = this.Reader[0].ToString();
                    formula.DrugName = this.Reader[1].ToString();
                    formula.DrugSpecs = this.Reader[2].ToString();
                    formula.Formula = this.Reader[3].ToString();
                    formula.ValidState = this.Reader[4].ToString();
                    formula.OperID = this.Reader[5].ToString();
                    formula.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6]);

                    al.Add(formula);
                }
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获取调价公式时发生错误！" + ex.Message;
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
        /// 零售价的调价公式
        /// </summary>
        /// <returns>零售价的调价公式实体数组</returns>
        public ArrayList QueryAdjustPriceSpeFormula()
        {
            string strSQL = "";
            if (this.GetSQL("SOC.Pharmacy.Adjust.SpeFormula.Query", ref strSQL) == -1)
            {
                strSQL = @"select  t.drug_code,
                                   t.trade_name,
                                   t.specs,
                                   t.formula,
                                   t.valid_state,
                                   t.oper_code,
                                   t.oper_date
                              from pha_soc_adjust_speformula t";

                this.CacheSQL("SOC.Pharmacy.Adjust.SpeFormula.Query", strSQL);
            }

            return this.myQueryAdjustPriceSpeFormula(strSQL);
            
        }

        /// <summary>
        /// 零售价的调价公式
        /// </summary>
        /// <param name="drugNO">药品编码</param>
        /// <returns>零售价的调价公式实体</returns>
        public FS.SOC.HISFC.Models.Pharmacy.Adjust.SpecialDrugFormula GetAdjustPriceSpeFormula(string drugNO)
        {
            string strSQL = "";
            if (this.GetSQL("SOC.Pharmacy.Adjust.Formula.QueryByItem", ref strSQL) == -1)
            {
                strSQL = @"select  t.drug_code,
                                   t.trade_name,
                                   t.specs,
                                   t.formula,
                                   t.valid_state,
                                   t.oper_code,
                                   t.oper_date
                              from pha_soc_adjust_speformula t
                             where t.drug_code = '{0}'";

                this.CacheSQL("SOC.Pharmacy.Adjust.Formula.QueryByItem", strSQL);
            }


            strSQL = string.Format(strSQL, drugNO);
            ArrayList al = this.myQueryAdjustPriceSpeFormula(strSQL);
            if (al != null)
            {
                if (al.Count > 0)
                {
                    return al[0] as FS.SOC.HISFC.Models.Pharmacy.Adjust.SpecialDrugFormula;
                }
                else
                {
                    return new FS.SOC.HISFC.Models.Pharmacy.Adjust.SpecialDrugFormula();
                }
            }

            return null;
        }

        public int InsertAdjustPurchasePriceInfo(FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.InsertAdjustPurchasePriceInfo", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.InsertAdjustPurchasePriceInfo字段!";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetParmAdjustPurcahsePriceInfo(adjustPrice);     //取参数列表
                strSQL = string.Format(strSQL, strParm);									//替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "插入调价明细信息SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #region 调价申请相关
        /// <summary>
        /// 获取未生效的调价申请
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public ArrayList QueryUnExecBillListApply(string drugNO)
        {
            ArrayList al = new ArrayList();
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("SOC.Pharmacy.AdjustPrice.GetBillListApply.ByDrugNO", ref strSQL) == -1)
            {
                strSQL = @"select   distinct 
                                    adjust_number, --调价单序号
                                    inure_time, --调价执行时间
                                    current_state, --调价单状态：0、未调价；1、已调价；2、无效
                                    oper_code, --操作员编码
                                    oper_name, --操作员名称
                                    oper_date, --操作时间
                                    dd_adjust_mark,
                                    ds_adjust_mark
                              from  pha_com_adjustpriceinfoapply
                             where  current_state = '0'
                               and  drug_code = '{0}'                 
                             order  by current_state, adjust_number desc
                            ";
                this.CacheSQL("SOC.Pharmacy.AdjustPrice.GetBillListApply.ByDrugNO", strSQL);
            }

            //格式化SQL语句
            string[] parm = { drugNO};
            strSQL = string.Format(strSQL, parm);

            try
            {
                //执行SQL语句，取调价单数据
                if (this.ExecQuery(strSQL) == -1)
                {
                    this.Err = "取调价单列表时出错：" + this.Err;
                    return null;
                }

                AdjustPrice info;  //药品调价信息
                while (this.Reader.Read())
                {
                    info = new AdjustPrice();
                    try
                    {
                        info.ID = this.Reader[0].ToString();                            //调价单号
                        info.InureTime = NConvert.ToDateTime(this.Reader[1].ToString());//生效时间              
                        info.State = this.Reader[2].ToString();                         //调价单状态：0、未调价；1、已调价；2、无效
                        info.Operation.ID = this.Reader[3].ToString();                      //操作员编码
                        info.Operation.Name = this.Reader[4].ToString();                      //操作员名称
                        info.Operation.Oper.OperTime = NConvert.ToDateTime(this.Reader[5].ToString()); //操作时间
                        info.IsDDAdjust = NConvert.ToBoolean(this.Reader[6]);
                        info.IsDSAdjust = NConvert.ToBoolean(this.Reader[7]);

                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得调价单列表时出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    al.Add(info);
                }

                return al;
            }
            catch (Exception ex)
            {
                this.Err = "执行Sql语句获取调价信息发生错误 " + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

        }

        /// <summary>
        /// 获取调价申请
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ArrayList QueryBillApplyList(DateTime beginTime, DateTime endTime)
        {
            ArrayList al = new ArrayList();
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("SOC.Pharmacy.AdjustPrice.GetApplyBillList.ByOperTime", ref strSQL) == -1)
            {
                strSQL = @"select   distinct 
                                    adjust_number, --调价单序号
                                    inure_time, --调价执行时间
                                    current_state, --调价单状态：0、未调价；1、已调价；2、无效
                                    oper_code, --操作员编码
                                    oper_name, --操作员名称
                                    oper_date, --操作时间
                                    dd_adjust_mark,
                                    ds_adjust_mark
                              from  PHA_COM_ADJUSTPRICEINFOAPPLY
                             where  oper_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') 
                               and  oper_date <  to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')                       
                             order  by current_state, adjust_number desc
                            ";

                this.CacheSQL("SOC.Pharmacy.AdjustPrice.GetApplyBillList.ByOperTime", strSQL);
            }

            //格式化SQL语句
            string[] parm = { beginTime.ToString(), endTime.ToString() };
            strSQL = string.Format(strSQL, parm);

            try
            {
                //执行SQL语句，取调价单数据
                if (this.ExecQuery(strSQL) == -1)
                {
                    this.Err = "取调价单列表时出错：" + this.Err;
                    return null;
                }

                AdjustPrice info;  //药品调价信息
                while (this.Reader.Read())
                {
                    info = new AdjustPrice();
                    try
                    {
                        info.ID = this.Reader[0].ToString();                            //调价单号
                        info.InureTime = NConvert.ToDateTime(this.Reader[1].ToString());//生效时间              
                        info.State = this.Reader[2].ToString();                         //调价单状态：0、未调价；1、已调价；2、无效
                        info.Operation.ID = this.Reader[3].ToString();                      //操作员编码
                        info.Operation.Name = this.Reader[4].ToString();                      //操作员名称
                        info.Operation.Oper.OperTime = NConvert.ToDateTime(this.Reader[5].ToString()); //操作时间
                        info.IsDDAdjust = NConvert.ToBoolean(this.Reader[6]);
                        info.IsDSAdjust = NConvert.ToBoolean(this.Reader[7]);

                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得调价单列表时出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    al.Add(info);
                }

                return al;
            }
            catch (Exception ex)
            {
                this.Err = "执行Sql语句获取调价信息发生错误 " + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 向调价汇总表中插入一条记录
        /// </summary>
        /// <param name="adjustPrice">库存记录类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int InsertAdjustPriceInfoApply(FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.InsertAdjustPriceInfoApply", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.InsertAdjustPriceInfoApply字段!";
                return -1;
            }
            try
            {
                string[] strParm = myGetParmAdjustApplyPriceInfo(adjustPrice);     //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "插入调价信息SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 获取调价单申请号
        /// </summary>
        /// <returns></returns>
        public string GetAdjustPriceBillNOApply()
        {
            return this.GetSequence("Pharmacy.Item.GetNewAdjustPriceApplyID");
        }

           /// <summary>
        /// 取某一药房中某一张调价单中的数据
        /// </summary>
        /// <param name="billCode">调价单号</param>
        /// <param name="state">调价状态</param>
        /// <returns>调价信息记录数组，出错返回null</returns>
        public ArrayList QueryAdjustPriceApplyInfoList(string billCode,string state)
        {
            string strSQL = "";
            //string strWhere = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetAdjustPriceApplyInfoist", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetAdjustPriceApplyInfoist字段!";
                return null;
            }

            //格式化SQL语句
            strSQL = string.Format(strSQL, billCode,state);

            //取调价数据
            return this.myGetAdjustPriceApplyInfo(strSQL);
        }

        /// <summary>
        /// 删除某条未生效的药品调价信息
        /// </summary>
        /// <param name="adjustPriceID">调价单号</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int DeleteAdjustPriceApplyInfo(string adjustPriceID)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.DeleteAdjustPriceApplyInfo", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.DeleteAdjustPriceApplyInfo字段!";
                return -1;
            }
            try
            {
                //如果是新增加的调价单，则直接返回
                if (adjustPriceID == "") return 1;
                strSQL = string.Format(strSQL, adjustPriceID);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "删除调价信息SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 删除调价监控表中的记录
        /// </summary>
        /// <param name="adjustPriceID"></param>
        /// <returns></returns>
        public int DeleteAdjustMonitorQuery(string adjustPriceID,string drugCode)
        {
            string strSQL = string.Empty;
            if (this.GetSQL("Pharmacy.Item.DeleteAdjustMonitorRecord", ref strSQL) == -1)
            {
                this.Err = "Pharmacy.Item.DeleteAdjustMonitorRecord";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, adjustPriceID, drugCode);
            }
            catch (Exception ex)
            {
                this.Err = "删除调价监控表SQL参数赋值时错误" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 插入调价监控表中的记录
        /// </summary>
        /// <param name="adjustPriceID"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="drugCode"></param>
        /// <returns></returns>
        public int InsertAdjustMonitorQuery(string adjustPriceID, DateTime beginTime, DateTime endTime, string drugCode)
        {
            string strSQL = string.Empty;
            if (this.GetSQL("Pharmacy.Item.InsertAdjustMonitorRecord", ref strSQL) == -1)
            {
                this.Err = "Pharmacy.Item.InsertAdjustMonitorRecord";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, adjustPriceID, beginTime,endTime,drugCode,this.Operator.ID);
            }
            catch (Exception ex)
            {
                this.Err = "删除调价监控表SQL参数赋值时错误" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 删除某条未生效的药品调价信息
        /// </summary>
        /// <param name="adjustPriceID"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public int DeleteAdjustApplyInfoByDrugCode(string adjustPriceID, string itemCode)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.DeleteAdjustPriceApplyInfoByDrugCode", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.DeleteAdjustPriceApplyInfoByDrugCode字段!";
                return -1;
            }
            try
            {
                //如果是新增加的调价单，则直接返回
                if (adjustPriceID == "") return 1;
                strSQL = string.Format(strSQL, adjustPriceID,itemCode);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "删除调价信息SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 按申请单更新调价申请单状态
        /// </summary>
        /// <param name="adjustPriceID"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int UpdateAdjustPriceApplyInfo(string adjustPriceID, string state)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.UpdateAdjustPriceApplyInfo", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateAdjustPriceApplyInfo字段!";
                return -1;
            }
            try
            {
                //如果是新增加的调价单，则直接返回
                if (adjustPriceID == "") return 1;
                strSQL = string.Format(strSQL, adjustPriceID,state);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "删除调价信息SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 取调价信息列表，可能是一条或者多条库存记录
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>调价信息对象数组</returns>
        private ArrayList myGetAdjustPriceApplyInfo(string SQLString)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice; //调价信息实体

            //执行查询语句
            if (this.ExecQuery(SQLString) == -1)
            {
                this.Err = "获得库存信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    //取查询结果中的记录
                    adjustPrice = new AdjustPrice();
                    adjustPrice.ID = this.Reader[0].ToString();                                    //0 调价单号
                    adjustPrice.SerialNO = NConvert.ToInt32(this.Reader[1].ToString());            //1 调价单内序号
                    adjustPrice.StockDept.ID = this.Reader[2].ToString();                               //2 库房编码  
                    adjustPrice.Item.ID = this.Reader[3].ToString();                               //3 药品编码
                    adjustPrice.Item.Type.ID = this.Reader[4].ToString();                          //4 药品类别
                    adjustPrice.Item.Quality.ID = this.Reader[5].ToString();                       //5 药品性质
                    adjustPrice.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[6].ToString());  //6 调价前零售价格
                    adjustPrice.Item.PriceCollection.WholeSalePrice = NConvert.ToDecimal(this.Reader[7].ToString()); //7 调价前批发价格
                    adjustPrice.AfterRetailPrice = NConvert.ToDecimal(this.Reader[8].ToString());   //8 调价后零售价格
                    adjustPrice.AfterWholesalePrice = NConvert.ToDecimal(this.Reader[9].ToString()); //9 调价后批发价格
                    adjustPrice.ProfitFlag = this.Reader[10].ToString();                           //10盈亏标记1-盈，0-亏
                    adjustPrice.InureTime = NConvert.ToDateTime(this.Reader[11].ToString());       //11调价执行时间
                    adjustPrice.Item.Name = this.Reader[12].ToString();                             //12药品商品名
                    adjustPrice.Item.Specs = this.Reader[13].ToString();                            //13规格
                    adjustPrice.Item.Product.Producer.ID = this.Reader[14].ToString();                      //14生产厂家
                    adjustPrice.Item.PackUnit = this.Reader[15].ToString();                         //15包装单位
                    adjustPrice.Item.PackQty = NConvert.ToDecimal(this.Reader[16].ToString());      //16包装数
                    adjustPrice.Item.MinUnit = this.Reader[17].ToString();                          //17最小单位
                    adjustPrice.State = this.Reader[18].ToString();                                //18调价单状态：0、未调价；1、已调价；2、无效
                    adjustPrice.Memo = this.Reader[19].ToString();                                 //19备注
                    adjustPrice.Operation.Oper.ID = this.Reader[20].ToString();                              //20操作员编码
                    adjustPrice.Operation.Oper.Name = this.Reader[21].ToString();                             //21操作员名称
                    adjustPrice.Operation.Oper.OperTime = NConvert.ToDateTime(this.Reader[22].ToString());        //22操作时间
                    adjustPrice.IsDDAdjust = NConvert.ToBoolean(this.Reader[23].ToString());
                    adjustPrice.IsDSAdjust = NConvert.ToBoolean(this.Reader[24]);

                    al.Add(adjustPrice);
                }
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得调价信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }


        private string[] myGetParmAdjustApplyPriceInfo(FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice)
        {

            string[] strParm ={   
								 adjustPrice.ID,                        //0 调价单号
								 adjustPrice.SerialNO.ToString(),           //1 调价单内序号
								 adjustPrice.StockDept.ID,                       //2 库房编码  
								 adjustPrice.Item.ID,                       //3 药品编码
								 adjustPrice.Item.Type.ID,                  //4 药品类别
								 adjustPrice.Item.Quality.ID.ToString(),    //5 药品性质
								 adjustPrice.Item.PriceCollection.RetailPrice.ToString(),   //6 调价前零售价格
								 adjustPrice.Item.PriceCollection.WholeSalePrice.ToString(),//7 调价前批发价格
								 adjustPrice.AfterRetailPrice.ToString(),   //8 调价后零售价格
								 adjustPrice.AfterWholesalePrice.ToString(),//9 调价后批发价格
								 adjustPrice.ProfitFlag,                    //10盈亏标记1-盈，0-亏
								 adjustPrice.InureTime.ToString() ,         //11调价执行时间
								 adjustPrice.Item.Name,                     //12药品商品名
								 adjustPrice.Item.Specs,                    //13规格
								 adjustPrice.Item.Product.Producer.ID,              //14生产厂家
								 adjustPrice.Item.PackUnit,                 //15包装单位
								 adjustPrice.Item.PackQty.ToString(),       //16包装数
								 adjustPrice.Item.MinUnit,                  //17最小单位
								 adjustPrice.State,                         //18调价单状态：0、未调价；1、已调价；2、无效
								 adjustPrice.Memo ,                         //21备注
								 adjustPrice.Operation.Oper.ID,                      //22操作员编码
								 adjustPrice.Operation.Oper.Name,                      //23操作员名称
								 adjustPrice.Operation.Oper.OperTime.ToString(),            //24操作时间
                                 NConvert.ToInt32(adjustPrice.IsDDAdjust).ToString(),
                                 NConvert.ToInt32(adjustPrice.IsDSAdjust).ToString(),
                                 adjustPrice.AdjustPriceType                //26调价类型
							 };
            return strParm;
        }
        #endregion
    }
}
