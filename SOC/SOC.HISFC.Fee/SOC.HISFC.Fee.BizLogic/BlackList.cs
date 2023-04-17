using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace FS.SOC.HISFC.Fee.BizLogic
{
    /// <summary>
    /// [功能描述: 黑名单SOC业务层，从核心版本独立出来，只对SOC层有效]<br></br>
    /// [创 建 者: zhaorong]<br></br>
    /// [创建时间: 2013-07]<br></br>
    /// </summary>
    public class BlackList : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 表头信息
        /// </summary>
        private Hashtable columnsTable = new Hashtable();
        /// <summary>
        /// 初始黑名单列明值
        /// </summary>
        /// <param name="columnsStr">key代表数据库表列名，value为空代表不显示在页面table中</param>
        public void InitColumnsHead4Table(Hashtable columnsTable)
        {
            columnsTable.Add("BLACK_ID", "编号");//序号
            columnsTable.Add("NAME", "姓名");//姓名
            columnsTable.Add("PACT_CODE", "合同单位编码");//合同单位编码
            columnsTable.Add("PACT_NAME", "合同单位名称");//合同单位名称
            columnsTable.Add("MCARD_NO", "医疗证号");//医疗证号
            columnsTable.Add("KIND", "种类编码");//种类 0 单位 1 个人
            columnsTable.Add("KINDNAME", "种类");//种类名称 0 单位 1 个人
            columnsTable.Add("IDDNO", "身份证号");//身份证号
            columnsTable.Add("SEX_CODE", "性别编码");//性别
            columnsTable.Add("SEXNAME", "性别");//性别名称
            columnsTable.Add("BIRTHDAY", "出生日期");//出生日期
            columnsTable.Add("DDYY1", "定点医院1");//定点医院1
            columnsTable.Add("D1NAME", "定点医院1名称");//定点医院1名称
            columnsTable.Add("DDYY2", "定点医院2");//定点医院2
            columnsTable.Add("D2NAME", "定点医院2名称");//定点医院2名称
            columnsTable.Add("DDYY3", "定点医院3");//定点医院3
            columnsTable.Add("D3NAME", "定点医院3名称");//定点医院3名称
            columnsTable.Add("CLINIC_RATE", "门诊比例");//门诊比例
            columnsTable.Add("INPATIENT_RATE", "住院比例");//住院比例
            columnsTable.Add("BEGIN_DATE", "起始日期（有效期）");//起始日期（有效期）
            columnsTable.Add("END_DATE", "结束日期（有效期）");//结束日期（有效期）
            columnsTable.Add("DAY_LIMIT", "日限额");//日限额
            columnsTable.Add("MONTH_LIMIT", "月限额");//月限额
            columnsTable.Add("YEAR_LIMIT", "年限额");//年限额
            columnsTable.Add("ONCE_LIMIT", "一次限额");//一次限额
            columnsTable.Add("BED_LIMIT", "床位上限");//床位上限
            columnsTable.Add("AIR_LIMIT", "空调上限");//空调上限
            columnsTable.Add("OPER_CODE", "操作员ID");//操作员名称
            columnsTable.Add("OPER_NAME", "操作员");//操作员名称
            columnsTable.Add("OPER_DATE", "操作时间");//操作时间
            columnsTable.Add("CLINIC_FLAG", "门诊启用标记");//门诊启用标记
            columnsTable.Add("INPATIENT_FLAG", "住院启用标记");//住院启用标记
            columnsTable.Add("UNIT_CODE", "单位编码");//单位编码
        }
        /// <summary>
        /// 获取所有的黑名单
        /// </summary>
        /// <returns></returns>
        public DataTable QueryAllBlackList()
        {
            string sqlStr = "";
            //取select语句
            if (this.Sql.GetCommonSql("SOC.HISFC.Fee.Components.Maintenance.Pact.GetAllBlackList", ref sqlStr) == -1)
            {
                this.Err = "没有找到SOC.HISFC.Fee.Components.Maintenance.Pact.GetAllBlackList字段!";
                return null;
            }
            //根据sql语句返回所有黑名单信息
            return this.Query(sqlStr);
        }
        /// <summary>
        /// 根据sql获取黑名单信息
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public DataTable Query(string sqlStr)
        {
            DataTable blackListsDt = new DataTable();
            //执行当前sql语句
            if (this.ExecQuery(sqlStr) == -1)
            {
                this.Err = this.Sql.Err;
                this.WriteErr();
                return null;
            }
            if (this.Reader == null)
            {
                this.Err = this.Sql.Err;
                this.WriteErr();
                return null;
            }
            //初始化列表表头信息
            columnsTable.Clear();
            InitColumnsHead4Table(columnsTable);
            //表头信息
            for (int i = 0; i < this.Reader.FieldCount; i++)
            {
                DataColumn dc = new DataColumn();
                string columnName = this.Reader.GetName(i);
                dc.DataType = this.Reader.GetFieldType(i);
                try
                {
                    dc.ColumnName = columnsTable[columnName].ToString();
                }
                catch
                {
                    dc.ColumnName = columnName;
                }
                blackListsDt.Columns.Add(dc);
            }
            try
            {
                while (this.Reader.Read())
                {
                    DataRow blackListDr = blackListsDt.NewRow();
                    for (int i = 0; i < this.Reader.FieldCount; i++)
                    {
                        string columnName = this.Reader.GetName(i);
                        try
                        {
                            blackListDr[columnsTable[columnName].ToString()] = this.Reader[i].ToString();
                        }
                        catch (Exception ex)
                        {
                            //类型：String、Decimal、DateTime。
                            //说明：Decimal、DateTime类型的值为空时调用ToString()会出现异常
                        }
                    }
                    blackListsDt.Rows.Add(blackListDr);
                }
                // 过滤不显示的列
                for (int i = 0; i < blackListsDt.Columns.Count; i++)
                {
                    DataColumn column = blackListsDt.Columns[i];
                    if (column.ColumnName.Contains("合同单位编码")
                        || column.ColumnName.Contains("种类编码")
                        || column.ColumnName.Contains("性别编码")
                        || column.ColumnName.Contains("操作员ID"))
                    {
                        blackListsDt.Columns.RemoveAt(i);
                    }
                }
                return blackListsDt;
            }
            catch (System.Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                //如果还没有关闭Reader,关闭之
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }
        /// <summary>
        /// 根据编号取某一黑名单人员信息
        /// </summary>
        /// <param name="blackId">黑名单编号</param>
        /// <returns>返回指定黑名单人员信息 失败返回-1</returns>
        public FS.SOC.HISFC.Fee.Models.BlackList GetBlackListById(string blackId)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.Sql.GetCommonSql("BlackList.Item.GetBlackListRowNum.ByBlackId", ref strSQL) == -1)
            {
                this.Err = "没有找到BlackList.Item.GetBlackListRowNum.ByBlackId字段!";
                return null;
            }
            //格式化SQL语句
            string[] parm = { blackId };
            strSQL = string.Format(strSQL, parm);

            //执行当前sql语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = this.Sql.Err;
                this.WriteErr();
                return null;
            }
            if (this.Reader == null)
            {
                this.Err = this.Sql.Err;
                this.WriteErr();
                return null;
            }
            try
            {
                FS.SOC.HISFC.Fee.Models.BlackList blackList = new FS.SOC.HISFC.Fee.Models.BlackList();
                while (this.Reader.Read())
                {
                    blackList.BlackId = Reader["BLACK_ID"].ToString();//编号
                    blackList.Name = Reader["NAME"].ToString();//名称
                    blackList.PactCode = Reader["PACT_CODE"].ToString();//合同单位编码
                    blackList.PactName = Reader["PACT_NAME"].ToString();//合同单位名称
                    blackList.McordNo = Reader["MCARD_NO"].ToString();//医疗证号
                    blackList.Kind = Reader["KIND"].ToString();//种类编号 0 单位 1 个人
                    blackList.KindName = Reader["KINDNAME"].ToString();//种类名称 0 单位 1 个人
                    blackList.IdDno = Reader["IDDNO"].ToString();//身份证号
                    blackList.SexCode = Reader["SEX_CODE"].ToString();//性别编号
                    blackList.SexName = Reader["SEXNAME"].ToString();//性别名称
                    blackList.Birthday = Convert.ToDateTime(Reader["BIRTHDAY"].ToString());//出生日期
                    blackList.Ddyy1 = Reader["DDYY1"].ToString();//定点医院1
                    //blackList.D1Name = Reader["D1NAME"].ToString();//定点医院1名称
                    blackList.Ddyy2 = Reader["DDYY2"].ToString();//定点医院2
                    //blackList.D2Name = Reader["D2NAME"].ToString();//定点医院2名称
                    blackList.Ddyy3 = Reader["DDYY3"].ToString();//定点医院3
                    //blackList.D3Name = Reader["D3NAME"].ToString();//定点医院3名称
                    blackList.ClinicRate = Convert.ToDecimal(Reader["CLINIC_RATE"].ToString());//门诊比例
                    blackList.InpatientRate = Convert.ToDecimal(Reader["INPATIENT_RATE"].ToString());//住院比例
                    //起始日期（有效期）
                    string beginDate = Reader["BEGIN_DATE"].ToString();
                    if (!string.IsNullOrEmpty(beginDate))
                    {
                        blackList.BeginDate = Convert.ToDateTime(beginDate);
                    }
                    //结束日期（有效期）
                    string endDate = Reader["END_DATE"].ToString();
                    if (!string.IsNullOrEmpty(endDate))
                    {
                        blackList.EndDate = Convert.ToDateTime(endDate);
                    }
                    blackList.DayLimit = Convert.ToDecimal(string.IsNullOrEmpty(Reader["DAY_LIMIT"].ToString()) ? "0" : Reader["DAY_LIMIT"].ToString());//日限额
                    blackList.MonthLimit = Convert.ToDecimal(string.IsNullOrEmpty(Reader["MONTH_LIMIT"].ToString()) ? "0" :Reader["MONTH_LIMIT"].ToString());//月限额
                    blackList.YearLimit = Convert.ToDecimal(string.IsNullOrEmpty(Reader["YEAR_LIMIT"].ToString()) ? "0" :Reader["YEAR_LIMIT"].ToString());//年限额
                    blackList.OnceLimit = Convert.ToDecimal(string.IsNullOrEmpty(Reader["ONCE_LIMIT"].ToString()) ? "0" :Reader["ONCE_LIMIT"].ToString());//一次限额
                    blackList.BedLimit = Convert.ToDecimal(string.IsNullOrEmpty(Reader["BED_LIMIT"].ToString()) ? "0" :Reader["BED_LIMIT"].ToString());//床位上限
                    blackList.AirLimit = Convert.ToDecimal(string.IsNullOrEmpty(Reader["AIR_LIMIT"].ToString()) ? "0" : Reader["AIR_LIMIT"].ToString());//空调上限
                    blackList.OperCode = Reader["OPER_CODE"].ToString();//操作员
                    blackList.OperName = Reader["OPER_NAME"].ToString();//操作员名称
                    //操作时间
                    string operDate = Reader["OPER_DATE"].ToString();
                    if (!string.IsNullOrEmpty(operDate))
                    {
                        blackList.OperDate = Convert.ToDateTime(operDate);
                    }
                    blackList.ClinicFlag = Reader["CLINIC_FLAG"].ToString();//门诊启用标记
                    blackList.InpatientFlag = Reader["INPATIENT_FLAG"].ToString();//住院启用标记
                    blackList.UnitCode = Reader["UNIT_CODE"].ToString();//单位编码
                }
                return blackList;
            }
            catch (System.Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                //如果还没有关闭Reader,关闭之
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }
        /// <summary>
        /// 向黑名单信息表中插入一条记录，黑名单编号采用oracle中的序列号
        /// </summary>
        /// <param name="blackList">黑名单基本信息</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int InsertItem(FS.SOC.HISFC.Fee.Models.BlackList blackList)
        {
            string strSQL = "";
            if (this.Sql.GetCommonSql("BlackList.item.InsertItem", ref strSQL) == -1) return -1;
            string[] strParm;
            try
            {
                //取黑名单ID
                blackList.BlackId = this.GetSequence("BlackList.Item.GetNewBlackId");
                if (blackList.BlackId == null) return -1;

                strParm = myGetParmItem(blackList);  //取参数列表
            }
            catch (Exception ex)
            {
                this.Err = "付数值时候出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSQL, strParm);
        }
        /// <summary>
        /// 更新黑名单信息，以黑名单ID为主键
        /// </summary>
        /// <param name="blackList">黑名单基本信息</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int UpdateItem(FS.SOC.HISFC.Fee.Models.BlackList blackList)
        {
            string strSQL = "";
            if (this.Sql.GetCommonSql("BlackList.Item.UpdateItem", ref strSQL) == -1) return -1;
            string[] strParm;
            try
            {
                strParm = myGetParmItem(blackList);  //取参数列表
            }
            catch (Exception ex)
            {
                this.Err = "付数值时候出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL, strParm);
        }
        /// <summary>
        /// 获得update或者insert黑名单表的传入参数数组
        /// </summary>
        /// <param name="Item">黑名单基本信息</param>
        /// <returns>成功返回参数数组 失败返回null</returns>
        private string[] myGetParmItem(FS.SOC.HISFC.Fee.Models.BlackList blackList)
        {
            #region "接口说明"
            //[0]序号
            //[1]名称
            //[2]合同单位编码
            //[3]医疗证号
            //[4]种类 0 单位 1 个人
            //[5]身份证号
            //[6]性别
            //[7]出生日期
            //[8]定点医院1
            //[9]定点医院2
            //[10]定点医院3
            //[11]门诊比例
            //[12]住院比例
            //[13]起始日期（有效期）
            //[14]结束日期（有效期）
            //[15]日限额
            //[16]月限额
            //[17]年限额
            //[18]一次限额
            //[19]床位上限
            //[20]空调上限
            //[21]操作员
            //[22]操作时间
            //[23]门诊启用标记
            //[24]住院启用标记
            //[25]单位编码    
            #endregion

            string[] strParm ={   blackList.BlackId,
                                  blackList.Name,
                                  blackList.PactCode,
								  blackList.McordNo,           
                                  blackList.Kind,
                                  blackList.IdDno,     
                                  blackList.SexCode,       
                                  blackList.Birthday.ToString(),
	                              blackList.Ddyy1,
	                              blackList.Ddyy2,
	                              blackList.Ddyy3,
	                              blackList.ClinicRate.ToString(),
	                              blackList.InpatientRate.ToString(),
	                              blackList.BeginDate.ToString(),
	                              blackList.EndDate.ToString(),
	                              blackList.DayLimit.ToString(),
	                              blackList.MonthLimit.ToString(),
	                              blackList.YearLimit.ToString(),
	                              blackList.OnceLimit.ToString(),
	                              blackList.BedLimit.ToString(),
	                              blackList.AirLimit.ToString(),
	                              blackList.OperCode,
	                              blackList.OperDate.ToString(),
	                              blackList.ClinicFlag,
	                              blackList.InpatientFlag,
	                              blackList.UnitCode,
							 };

            return strParm;
        }
        /// <summary>
        /// 删除黑名单信息
        /// </summary>
        /// <param name="blackId">黑名单编号</param>
        /// <returns>0没有删除 1成功 -1失败</returns>
        public int DeleteItem(string blackId)
        {
            string strSQL = ""; //根据黑名单编号删除某一黑名单信息的DELETE语句
            if (this.Sql.GetCommonSql("BlackList.Item.DeleteItem", ref strSQL) == -1) return -1;
            try
            {
                strSQL = string.Format(strSQL, blackId);
            }
            catch
            {
                this.Err = "传入参数不对！BlackList.Item.DeleteItem";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
    }
}
