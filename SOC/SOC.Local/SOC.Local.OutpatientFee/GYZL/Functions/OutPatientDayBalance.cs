
using System;
using System.Collections;
using System.Data;
using FS.FrameWork.Models;
using FS.FrameWork.Function;
using System.Collections.Generic;

namespace FS.SOC.Local.OutpatientFee.GYZL.Functions
{
    /// <summary>
    /// 门诊收款员日结
    /// </summary>
    ///*----------------------------------------------------------------
    // Copyright (C) 沈阳东软软件股份有限公司
    // 版权所有。 
    //
    // 文件名：			OutPatientDayBalance.cs
    // 文件功能描述：	门诊收款日结方法类
    //
    // 
    // 创建标识：		2006-3-22
    //
    // 修改标识：
    // 修改描述：
    //
    // 修改标识：
    // 修改描述：
    //----------------------------------------------------------------*/
    public class OutPatientDayBalance : FS.FrameWork.Management.Database
    {
        public OutPatientDayBalance()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        //
        // 变量
        //
        #region 变量
        /// <summary>
        ///  返回值
        /// </summary>
        int intReturn = 0;

        /// <summary>
        /// 执行查询的SQL语句
        /// </summary>
        string SQL = "";

        /// <summary>
        /// 查询语句
        /// </summary>
        string stringSelect = "";

        /// <summary>
        /// 条件语句
        /// </summary>
        string stringWhere = "";

        /// <summary>
        /// 分组语句
        /// </summary>
        string stringGroup = "";

        /// <summary>
        /// 排序语句
        /// </summary>
        string stringOrder = "";

        /// <summary>
        /// 构造SQL语句的参数
        /// </summary>
        string[] parms = new string[26];
        #endregion

        //
        // 公共方法
        //
        #region 初始化变量
        /// <summary>
        /// 初始化变量
        /// </summary>
        private void InitVar()
        {
            this.intReturn = 0;
            this.SQL = "";
            this.stringSelect = "";
            this.stringWhere = "";
            this.stringGroup = "";
            this.stringOrder = "";
        }
        #endregion

        #region 构造SQL语句
        /// <summary>
        /// 构造SQL语句
        /// </summary>
        private void CreateSQL()
        {
            this.SQL = this.stringSelect + " " + this.stringWhere + " " + this.stringGroup + " " + this.stringOrder;
        }
        #endregion

        #region 清空参数数组
        /// <summary>
        /// 清空参数数组
        /// </summary>
        public void ClearParms()
        {
            for (int i = 0; i < parms.Length; i++)
            {
                parms[i] = "";
            }
        }
        #endregion

        #region 构造SQL语句匹配参数
        /// <summary>
        /// 构造SQL语句匹配参数
        /// </summary>
        /// <param name="clinicDayBalance">日结实体类</param>
        /// <param name="insert">是否插入</param>
        public void FillParms(Models.OutPatientDayBalance clinicDayBalance, bool insert)
        {
            // 清空参数数组
            this.ClearParms();

            // 日结序号
            parms[0] = clinicDayBalance.BalanceSequence;
            // 日结数据开始时间
            parms[1] = clinicDayBalance.BeginDate.ToString();
            // 日结数据截止时间
            parms[2] = clinicDayBalance.EndDate.ToString();
            // 总收入
            parms[3] = clinicDayBalance.Cost.TotCost.ToString();
            // 收款员代码
            parms[4] = clinicDayBalance.BalanceOperator.ID;
            // 收款员姓名
            parms[5] = clinicDayBalance.BalanceOperator.Name;
            // 日结操作时间
            parms[6] = clinicDayBalance.BalanceDate.ToString();
            // 备注金额1
            parms[7] = clinicDayBalance.UnValidNumber.ToString();
            // 备注金额2
            parms[8] = clinicDayBalance.BKNumber.ToString();
            // 备注金额3
            parms[9] = clinicDayBalance.Memo;
            // 财务审核标志
            parms[10] = clinicDayBalance.CheckFlag;
            // 财务审核人
            parms[11] = clinicDayBalance.CheckOperator.ID;
            // 财务审核时间
            parms[12] = clinicDayBalance.CheckDate.ToString();
            // 日结项目
            if (clinicDayBalance.BalanceItem == FS.HISFC.Models.Base.CancelTypes.Valid)
            {
                // 正常
                parms[13] = "0";
            }
            else if (clinicDayBalance.BalanceItem == FS.HISFC.Models.Base.CancelTypes.Canceled)
            {
                // 退费
                parms[13] = "1";
            }
            else if (clinicDayBalance.BalanceItem == FS.HISFC.Models.Base.CancelTypes.Reprint)
            {
                // 重打
                parms[13] = "2";
            }
            else if (clinicDayBalance.BalanceItem == FS.HISFC.Models.Base.CancelTypes.LogOut)
            {
                // 注销
                parms[13] = "3";
            }
            // 日结项目对应发票号
            parms[14] = clinicDayBalance.InvoiceNo;
            // 实收金额
            parms[15] = clinicDayBalance.Cost.OwnCost.ToString();
            // 记帐单数量
            parms[16] = clinicDayBalance.AccountNumber.ToString();
            // 扩展字段
            parms[17] = clinicDayBalance.ExtendField;
            // 记帐金额
            parms[18] = clinicDayBalance.Cost.LeftCost.ToString();
            //刷卡数量
            parms[19] = clinicDayBalance.CDNumber.ToString();
            //现金
            parms[20] = clinicDayBalance.BackCost1.ToString();
            //刷卡
            parms[21] = clinicDayBalance.BackCost2.ToString();
            //支票
            parms[22] = clinicDayBalance.BackCost3.ToString();
            //退费发票明细
            parms[23] = clinicDayBalance.BKInvoiceNo;
            //作废发票明细
            parms[24] = clinicDayBalance.UnValidInvoiceNo;
            //发票区间
            parms[25] = clinicDayBalance.InvoiceBand;


        }
        #endregion

        #region 转换返回的Reader到实体类
        /// <summary>
        /// 转换返回的Reader到实体类
        /// </summary>
        /// <param name="clinicDayBalance">返回的日结实体</param>
        public void ChangeReaderToClass(ref Models.OutPatientDayBalance clinicDayBalance)
        {
            // 如果Reader为空，那么返回空
            if (this.Reader == null)
            {
                clinicDayBalance = null;
            }

            // 转换
            if (this.Reader.Read())
            {
                // 日结序号
                clinicDayBalance.BalanceSequence = this.Reader[2].ToString();
                // 日结数据开始时间
                try
                {
                    clinicDayBalance.BeginDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[3]);
                }
                catch
                {
                    clinicDayBalance.BeginDate = DateTime.MinValue;
                }
                // 日结数据截止时间
                try
                {
                    clinicDayBalance.EndDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4]);
                }
                catch
                {
                    clinicDayBalance.EndDate = DateTime.MinValue;
                }
                // 总收入
                clinicDayBalance.Cost.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                // 收款员代码
                clinicDayBalance.BalanceOperator.ID = this.Reader[6].ToString();
                // 收款员姓名
                clinicDayBalance.BalanceOperator.Name = this.Reader[7].ToString();
                // 日结操作时间
                try
                {
                    clinicDayBalance.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8]);
                }
                catch
                {
                    clinicDayBalance.BalanceDate = DateTime.MinValue;
                }
                // 备注金额1
                clinicDayBalance.UnValidNumber = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[9]);
                // 备注金额2
                clinicDayBalance.BKNumber = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[10]);
                // 备注金额3
                clinicDayBalance.User03 = this.Reader[11].ToString();
                // 财务审核标志
                clinicDayBalance.CheckFlag = this.Reader[12].ToString();
                // 财务审核人
                clinicDayBalance.CheckOperator.ID = this.Reader[13].ToString();
                // 财务审核时间
                clinicDayBalance.CheckDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[14]);
                // 日结项目
                if (this.Reader[15].ToString() == "0")
                {
                    clinicDayBalance.BalanceItem = FS.HISFC.Models.Base.CancelTypes.Valid;
                }
                else if (this.Reader[15].ToString() == "1")
                {
                    clinicDayBalance.BalanceItem = FS.HISFC.Models.Base.CancelTypes.Canceled;
                }
                else if (this.Reader[15].ToString() == "2")
                {
                    clinicDayBalance.BalanceItem = FS.HISFC.Models.Base.CancelTypes.Reprint;
                }
                else if (this.Reader[15].ToString() == "3")
                {
                    clinicDayBalance.BalanceItem = FS.HISFC.Models.Base.CancelTypes.LogOut;
                }
                // 日结项目对应发票号
                clinicDayBalance.InvoiceNo = this.Reader[16].ToString();
                // 实收金额
                clinicDayBalance.Cost.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17].ToString());
                // 记帐单数量
                clinicDayBalance.AccountNumber = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[18].ToString());
                // 扩展字段
                clinicDayBalance.ExtendField = this.Reader[19].ToString();
                // 记帐金额
                clinicDayBalance.Cost.LeftCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20].ToString());
                //刷卡数量
                if (this.Reader[21] != null)
                {
                    clinicDayBalance.CDNumber = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[21].ToString());
                }
                //刷卡
                if (this.Reader[22] != null)
                {
                    clinicDayBalance.BackCost2 = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[22].ToString());
                }
                //现金
                if (this.Reader[23] != null)
                {
                    clinicDayBalance.BackCost1 = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[23].ToString());
                }
                //支票
                if (this.Reader[24] != null)
                {
                    clinicDayBalance.BackCost3 = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[24].ToString());
                }
                //票据区间
                clinicDayBalance.RecipeBand = this.Reader[25].ToString();
                //发票区间
                clinicDayBalance.InvoiceBand = this.Reader[26].ToString();
                //作废发票明细
                clinicDayBalance.UnValidInvoiceNo = this.Reader[27].ToString();
                //退费发票明细
                clinicDayBalance.BKInvoiceNo = this.Reader[28].ToString();
            }
        }
        #endregion

        #region 转换返回的Reader到实体类数组
        /// <summary>
        /// 转换返回的Reader到实体类数组
        /// </summary>
        /// <param name="alClinicDayBalance">返回的日结实体数组</param>
        public void ChangeReaderToClass(ref ArrayList alClinicDayBalance)
        {


            // 如果Reader为空，那么返回空
            if (this.Reader == null)
            {
                return;
            }

            // 转换
            while (this.Reader.Read())
            {
                Models.OutPatientDayBalance clinicDayBalance = new Models.OutPatientDayBalance();
                // 日结序号
                clinicDayBalance.BalanceSequence = this.Reader[2].ToString();
                // 日结数据开始时间
                try
                {
                    clinicDayBalance.BeginDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[3]);
                }
                catch
                {
                    clinicDayBalance.BeginDate = DateTime.MinValue;
                }
                // 日结数据截止时间
                try
                {
                    clinicDayBalance.EndDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4]);
                }
                catch
                {
                    clinicDayBalance.EndDate = DateTime.MinValue;
                }
                // 总收入
                clinicDayBalance.Cost.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                // 收款员代码
                clinicDayBalance.BalanceOperator.ID = this.Reader[6].ToString();
                // 收款员姓名
                clinicDayBalance.BalanceOperator.Name = this.Reader[7].ToString();
                // 日结操作时间
                try
                {
                    clinicDayBalance.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8]);
                }
                catch
                {
                    clinicDayBalance.BalanceDate = DateTime.MinValue;
                }
                // 备注金额1
                clinicDayBalance.UnValidNumber = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[9]);
                // 备注金额2
                clinicDayBalance.BKNumber = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[10]);
                // 备注金额3
                clinicDayBalance.User03 = this.Reader[11].ToString();
                // 财务审核标志
                clinicDayBalance.CheckFlag = this.Reader[12].ToString();
                // 财务审核人
                clinicDayBalance.CheckOperator.ID = this.Reader[13].ToString();
                // 财务审核时间
                clinicDayBalance.CheckDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[14]);
                // 日结项目
                if (this.Reader[15].ToString() == "0")
                {
                    clinicDayBalance.BalanceItem = FS.HISFC.Models.Base.CancelTypes.Valid;
                }
                else if (this.Reader[15].ToString() == "1")
                {
                    clinicDayBalance.BalanceItem = FS.HISFC.Models.Base.CancelTypes.Canceled;
                }
                else if (this.Reader[15].ToString() == "2")
                {
                    clinicDayBalance.BalanceItem = FS.HISFC.Models.Base.CancelTypes.Reprint;
                }
                else if (this.Reader[15].ToString() == "3")
                {
                    clinicDayBalance.BalanceItem = FS.HISFC.Models.Base.CancelTypes.LogOut;
                }
                // 日结项目对应发票号
                clinicDayBalance.InvoiceNo = this.Reader[16].ToString();
                // 实收金额
                clinicDayBalance.Cost.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17].ToString());
                // 记帐单数量
                clinicDayBalance.AccountNumber = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[18].ToString());
                // 扩展字段
                clinicDayBalance.ExtendField = this.Reader[19].ToString();
                // 记帐金额
                clinicDayBalance.Cost.LeftCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20].ToString());
                //刷卡数量
                if (this.Reader[21] != null)
                {
                    clinicDayBalance.CDNumber = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[21].ToString());
                }
                //刷卡
                if (this.Reader[22] != null)
                {
                    clinicDayBalance.BackCost2 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[22].ToString());
                }
                //现金
                if (this.Reader[23] != null)
                {
                    clinicDayBalance.BackCost1 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[23].ToString());
                }
                //支票
                if (this.Reader[24] != null)
                {
                    clinicDayBalance.BackCost3 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[24].ToString());
                }
                //票据区间
                clinicDayBalance.RecipeBand = this.Reader[25].ToString();
                //发票区间
                clinicDayBalance.InvoiceBand = this.Reader[16].ToString();
                //作废发票明细
                clinicDayBalance.UnValidInvoiceNo = this.Reader[27].ToString();
                //退费发票明细
                clinicDayBalance.BKInvoiceNo = this.Reader[28].ToString();
                // 添加实体
                alClinicDayBalance.Add(clinicDayBalance);
            }
        }
        #endregion

        #region 计算发票张数
        /// <summary>
        /// 计算发票张数
        /// </summary>
        /// <param name="invoiceCode">发票号或发票区间</param>
        /// <returns>发票张数</returns>
        public int GetInvoiceCount(string invoiceCode)
        {
            // 变量定义
            int intCount = 0;
            int intLeft = 0;
            int intRight = 0;
            int intLength = 0;
            string stringSub = "";

            // 获取长度和分割符位置
            intLength = invoiceCode.Length;
            intCount = invoiceCode.IndexOf("～");

            // 如果没有分割符，那么发票张数为1
            if (intCount == -1)
            {
                intCount = 1;
            }
            else
            {
                intLeft = int.Parse(invoiceCode.Substring(0, intCount));
                stringSub = invoiceCode.Substring(intCount + 1, intLength - intCount - 1);
                intRight = int.Parse(stringSub);
                intCount = intRight - intLeft + 1;

            }

            return intCount;
        }
        #endregion

        //
        // 查询获取
        //
        #region 根据收款员工号获取上次日结时间(1：成功/0：没有作过日结/-1：失败)
       
        /// <summary>
        /// 得到日结数据
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="dtBegin"></param>
        /// <param name="?"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int GetZSYDayBalanceData(string employeeID, string dtBegin, string dtEnd, ref DataSet ds)
        {
            string strSql = "";
            string strWhere = "";
            int intReturn = -1;
            if (this.Sql.GetSql("Local.Clinic.GetZSYDayBalanceData.Select", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql:Local.Clinic.GetZSYDayBalanceData.Select";
                return -1;
            }
            if (this.Sql.GetSql("Local.Clinic.GetZSYDayBalanceData.Where1", ref strWhere) == -1)
            {
                this.Err = "Can't Find Sql:Local.Clinic.GetZSYDayBalanceData.Where1";
                return -1;
            }
            strSql = strSql + strWhere;
            strSql = System.String.Format(strSql, employeeID, dtBegin, dtEnd);
            intReturn = this.ExecQuery(strSql, ref ds);
            if (intReturn == -1)
            {
                this.Err = "执行SQL语句失败" + this.Err;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 得到退费数据
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="dtBegin"></param>
        /// <param name="?"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int GetZSYDayBalanceReturnData(string employeeID, string dtBegin, string dtEnd, ref DataSet ds)
        {
            string strSql = "";
            string strWhere = "";
            int intReturn = -1;
            if (this.Sql.GetSql("Local.Clinic.GetZSYDayBalanceData.Select", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql:Local.Clinic.GetZSYDayBalanceData.Select";
                return -1;
            }
            if (this.Sql.GetSql("Local.Clinic.GetZSYDayBalanceData.Where2", ref strWhere) == -1)
            {
                this.Err = "Can't Find Sql:Local.Clinic.GetZSYDayBalanceData.Where1";
                return -1;
            }
            strSql = strSql + strWhere;
            strSql = System.String.Format(strSql, employeeID, dtBegin, dtEnd);
            intReturn = this.ExecQuery(strSql, ref ds);
            if (intReturn == -1)
            {
                this.Err = "执行SQL语句失败" + this.Err;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 得到无效数据
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="dtBegin"></param>
        /// <param name="?"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int GetZSYDayBalanceUnvalidData(string employeeID, string dtBegin, string dtEnd, ref DataSet ds)
        {
            string strSql = "";
            string strWhere = "";
            int intReturn = -1;
            if (this.Sql.GetSql("Local.Clinic.GetZSYDayBalanceData.Select", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql:Local.Clinic.GetZSYDayBalanceData.Select";
                return -1;
            }
            if (this.Sql.GetSql("Local.Clinic.GetZSYDayBalanceData.Where3", ref strWhere) == -1)
            {
                this.Err = "Can't Find Sql:Local.Clinic.GetZSYDayBalanceData.Where1";
                return -1;
            }
            strSql = strSql + strWhere;
            strSql = System.String.Format(strSql, employeeID, dtBegin, dtEnd);
            intReturn = this.ExecQuery(strSql, ref ds);
            if (intReturn == -1)
            {
                this.Err = "执行SQL语句失败" + this.Err;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 获得最大日结号
        /// </summary>
        /// <param name="operCode"></param>
        /// <returns></returns>
        public string GetMaxBalanceNoByOper(string operCode)
        {
            string strSql = "";
            if (this.Sql.GetSql("Local.Clinic.Function.GetMaxBalanceNo.Select", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql:Local.Clinic.Function.GetLastBalanceDate.Select";
                return "";
            }
            strSql = System.String.Format(strSql, operCode);
            return this.ExecSqlReturnOne(strSql);
        }
        #endregion

        #region 获取门诊收款员的日结数据(1：成功/-1：失败)
        /// <summary>
        /// 获取门诊收款员的日结数据(1：成功/-1：失败)
        /// </summary>
        /// <param name="employeeID">门诊收款员编号</param>
        /// <param name="dateBegin">日结起始时间</param>
        /// <param name="dateEnd">日结截止时间</param>
        /// <returns>1：成功/-1：失败</returns>
        public int GetDayBalanceData(string employeeID, string dateBegin,
                                        string dateEnd, ref System.Data.DataSet dsResult)
        {
            //
            // 初始化变量
            //
            this.InitVar();

            //
            // 获取SQL语句
            //

            // 获取查询语句
            intReturn = this.Sql.GetSql("Local.Clinic.GetDayBalanceData.Select", ref stringSelect);
            if (intReturn == -1)
            {
                this.Err = "获取SQL语句失败" + this.Err;
                return -1;
            }
            // 获取条件语句
            intReturn = this.Sql.GetSql("Local.Clinic.GetDayBalanceData.Where", ref stringWhere);
            if (intReturn == -1)
            {
                this.Err = "获取SQL语句失败" + this.Err;
                return -1;
            }
            // 获取分组语句
            intReturn = this.Sql.GetSql("Local.Clinic.GetDayBalanceData.Group", ref stringGroup);
            if (intReturn == -1)
            {
                this.Err = "获取SQL语句失败" + this.Err;
                return -1;
            }
            // 获取排序语句
            intReturn = this.Sql.GetSql("Local.Clinic.GetDayBalanceData.Order", ref stringOrder);
            if (intReturn == -1)
            {
                this.Err = "获取SQL语句失败" + this.Err;
                return -1;
            }
            // 构造SQL语句
            this.CreateSQL();

            //
            // 格式化SQL语句
            //
            try
            {
                this.SQL = string.Format(this.SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception e)
            {
                this.InitVar();
                this.Err = "格式化SQL语句失败(" + this.Err + ")(" + e.Message + ")";
                return -1;
            }

            //
            // 执行SQL语句
            //
            intReturn = this.ExecQuery(this.SQL, ref dsResult);
            if (intReturn == -1)
            {
                this.Err = "执行SQL语句失败" + this.Err;
                return -1;
            }

            return 1;
        }
        #endregion

        #region 根据时间范围获取相应的日结记录（非明细）
        /// <summary>
        /// 根据时间范围获取相应的日结记录（非明细）
        /// </summary>
        /// <param name="employee">操作员信息</param>
        /// <param name="dtFrom">起始时间</param>
        /// <param name="dtTo">截止时间</param>
        /// <param name="clinicDayBalance">返回的日结记录数组</param>
        /// <returns>1：成功/-1：失败</returns>
        public int GetBalanceRecord(FS.FrameWork.Models.NeuObject employee, DateTime dtFrom, DateTime dtTo,
                                    ref ArrayList clinicDayBalance)
        {
            //
            // 初始化变量
            //
            this.InitVar();
            // 日结记录
            FS.FrameWork.Models.NeuObject balanceRecord = new NeuObject();

            //
            // 获取SQL语句
            //

            // 获取查询语句
            intReturn = this.Sql.GetSql("Local.Clinic.GetBalanceRecord.Select", ref stringSelect);
            if (intReturn == -1)
            {
                this.Err = "获取SQL语句失败" + this.Err;
                return -1;
            }
            // 获取条件语句
            intReturn = this.Sql.GetSql("Local.Clinic.GetBalanceRecord.Where", ref stringWhere);
            if (intReturn == -1)
            {
                this.Err = "获取SQL语句失败" + this.Err;
                return -1;
            }
            // 构造SQL语句
            this.CreateSQL();

            //
            // 格式化SQL语句
            //
            try
            {
                this.SQL = string.Format(this.SQL, employee.ID, dtFrom, dtTo);
            }
            catch (Exception e)
            {
                this.InitVar();
                this.Err = "格式化SQL语句失败(" + this.Err + ")(" + e.Message + ")";
                return -1;
            }

            //
            // 执行SQL语句
            //
            intReturn = this.ExecQuery(this.SQL);
            if (intReturn == -1)
            {
                this.Err = "执行SQL语句失败" + this.Err;
                return -1;
            }

            //
            // 赋值
            //
            while (this.Reader.Read())
            {
                balanceRecord = new NeuObject();
                balanceRecord.ID = this.Reader[0].ToString();
                balanceRecord.Name = this.Reader[1].ToString();
                balanceRecord.Memo = this.Reader[2].ToString();
                balanceRecord.User01 = this.Reader[3].ToString();
                clinicDayBalance.Add(balanceRecord);
            }

            return 1;
        }
        /// <summary>
        /// 根据时间范围获取相应的日结记录（非明细）
        /// </summary>
        /// <param name="employee">操作员信息</param>
        /// <param name="empDept">操作员科室</param>
        /// <param name="dtFrom">起始时间</param>
        /// <param name="dtTo">截止时间</param>
        /// <param name="clinicDayBalance">返回的日结记录数组</param>
        /// <returns>1：成功/-1：失败</returns>
        public int GetBalanceRecord(FS.FrameWork.Models.NeuObject employee, FS.FrameWork.Models.NeuObject empDept, DateTime dtFrom, DateTime dtTo,
            ref ArrayList clinicDayBalance)
        {
            //
            // 初始化变量
            //
            this.InitVar();
            // 日结记录
            FS.FrameWork.Models.NeuObject balanceRecord = new NeuObject();

            //
            // 获取SQL语句
            //

            // 获取查询语句
            intReturn = this.Sql.GetSql("Local.Clinic.GetBalanceRecord.Select", ref stringSelect);
            if (intReturn == -1)
            {
                this.Err = "获取SQL语句失败" + this.Err;
                return -1;
            }
            // 获取条件语句
            intReturn = this.Sql.GetSql("Local.Clinic.GetBalanceRecord.ByOperDeptAndOperCode", ref stringWhere);
            if (intReturn == -1)
            {
                this.Err = "获取SQL语句失败" + this.Err;
                return -1;
            }
            // 构造SQL语句
            this.CreateSQL();

            //
            // 格式化SQL语句
            //
            try
            {
                this.SQL = string.Format(this.SQL, employee.ID, dtFrom, dtTo, empDept.ID);
            }
            catch (Exception e)
            {
                this.InitVar();
                this.Err = "格式化SQL语句失败(" + this.Err + ")(" + e.Message + ")";
                return -1;
            }

            //
            // 执行SQL语句
            //
            intReturn = this.ExecQuery(this.SQL);
            if (intReturn == -1)
            {
                this.Err = "执行SQL语句失败" + this.Err;
                return -1;
            }

            //
            // 赋值
            //
            while (this.Reader.Read())
            {
                balanceRecord = new NeuObject();
                balanceRecord.ID = this.Reader[0].ToString();
                balanceRecord.Name = this.Reader[1].ToString();
                balanceRecord.Memo = this.Reader[2].ToString();
                balanceRecord.User01 = this.Reader[3].ToString();
                clinicDayBalance.Add(balanceRecord);
            }

            return 1;
        }
        #endregion

        #region 根据日结流水号获取日结明细(1：成功/-1：失败)
        /// <summary>
        /// 根据日结流水号获取日结明细(1：成功/-1：失败)
        /// </summary>
        /// <param name="stringSequece">日结流水号</param>
        /// <param name="balanceDetail">返回的日结明细</param>
        /// <returns>1：成功/-1：失败</returns>
        public int GetDayBalanceDetail(string stringSequece, ref ArrayList balanceDetail)
        {
            //
            // 初始化变量
            //
            this.InitVar();

            //
            // 获取SQL语句
            //

            // 获取查询语句
            intReturn = this.Sql.GetSql("Local.Clinic.GetDayBalanceDetail.Select", ref stringSelect);
            if (intReturn == -1)
            {
                this.Err = "获取SQL语句失败" + this.Err;
                return -1;
            }
            // 获取条件语句
            intReturn = this.Sql.GetSql("Local.Clinic.GetDayBalanceDetail.Where", ref stringWhere);
            if (intReturn == -1)
            {
                this.Err = "获取SQL语句失败" + this.Err;
                return -1;
            }
            // 构造SQL语句
            this.CreateSQL();

            //
            // 格式化SQL语句
            //
            try
            {
                this.SQL = string.Format(this.SQL, stringSequece);
            }
            catch (Exception e)
            {
                this.InitVar();
                this.Err = "格式化SQL语句失败(" + this.Err + ")(" + e.Message + ")";
                return -1;
            }

            //
            // 执行SQL语句
            //
            intReturn = this.ExecQuery(this.SQL);
            if (intReturn == -1)
            {
                this.Err = "执行SQL语句失败" + this.Err;
                return -1;
            }

            //
            // 赋值
            //
            this.ChangeReaderToClass(ref balanceDetail);

            return 1;
        }
        #endregion

        #region 获取日结序号(1：成功/-1：失败)
        /// <summary>
        /// 获取日结序号(1：成功/-1：失败)
        /// </summary>
        /// <param name="sequence">返回日结序列号</param>
        /// <returns>1：成功/-1：失败</returns>
        public int GetBalanceSequence(ref string sequence)
        {
            // 获取日结序号
            sequence = this.GetSequence("Local.Clinic.Function.CreateClinicDayBalance.GetInsertSequence");
            if (sequence == null)
            {
                this.Err = "获取流水号失败！" + this.Err;
                return -1;
            }
            return 1;
        }
        #endregion

        #region 根据发票号获得对应的支付方式和金额
        /// <summary>
        /// 获得发票对应支付方式
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public ArrayList GetPayModeByInvoiceNo(string invoiceNo, string invoice_seq, string transType)
        {
            string strSql = "";//sql 语句

            ArrayList al = new ArrayList();//返回数组
            //
            //找不到sql
            //
            if (this.Sql.GetSql("Local.Clinic.GetPayModeByInvoiceNo", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql:Local.Clinic.GetPayModeByInvoiceNo";
                return null;
            }
            strSql = System.String.Format(strSql, invoiceNo, invoice_seq, transType);
            //
            //执行出错
            //
            if (this.ExecQuery(strSql) < 0)
            {
                this.Err = "Execute Sql Err";
                return null;
            }
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject obj = new NeuObject();
                obj.ID = this.Reader[0].ToString();//发票号
                obj.Name = this.Reader[1].ToString();//支付方式
                obj.Memo = this.Reader[2].ToString();//金额
                al.Add(obj);
            }
            this.Reader.Close();
            return al;
        }
        #endregion

        #region  根据日结流水号更新日结表的票据范围等字段
        /// <summary>
        /// 根据日结流水号更新日结表的票据范围等字段
        /// </summary>
        /// <param name="balanceID"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateOtherByBalanceID(string balanceID, FS.FrameWork.Models.NeuObject obj)
        {
            string strSql = "";
            if (this.Sql.GetSql("Local.Clinic.UpdateOtherByBalanceID", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql:Local.Clinic.UpdateOtherByBalanceID";
                return -1;
            }
            strSql = System.String.Format(strSql, obj.ID, obj.Name, obj.Memo, obj.User01, obj.User02, obj.User03, balanceID);
            if (this.ExecNoQuery(strSql) < 0)
            {
                this.Err = "Execute Err";
                return -1;
            }
            return 1;
        }
        #endregion

        //
        // 数据操作
        //
        #region 插入日结表(-1：失败/1：成功)
        /// <summary>
        /// 插入日结表(-1：失败/1：成功)
        /// </summary>
        /// <param name="clinicDayBalance">输入的日结实体</param>
        /// <returns>-1：失败/1</returns>
        public int CreateClinicDayBalance(Models.OutPatientDayBalance clinicDayBalance)
        {
            // 初始化变量
            this.InitVar();


            // 获取SQL语句
            this.intReturn = this.Sql.GetSql("Local.Clinic.Function.CreateClinicDayBalance", ref this.stringSelect);
            if (intReturn == -1)
            {
                this.Err = "获取SQL语句失败" + this.Err;
                return -1;
            }
            this.CreateSQL();

            // 匹配参数
            this.FillParms(clinicDayBalance, true);

            // 格式化语句
            try
            {
                this.SQL = string.Format(this.SQL, this.parms);
            }
            catch (Exception e)
            {
                this.Err = "格式化SQL语句失败" + e.Message;
                return -1;
            }

            // 执行SQL语句
            intReturn = this.ExecNoQuery(this.SQL);
            if (intReturn <= 0)
            {
                this.Err = "执行SQL语句失败" + this.Err;
                return -1;
            }

            return 1;
        }
        #endregion

        #region 新日结
        /// <summary>
        /// 获取日结项目数据
        /// </summary>
        /// <param name="employeeID">收款员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="dsResult">返回数据集</param>
        /// <returns>1：成功/-1：失败</returns>
        public int GetDayBalanceDataNew(string employeeID, string dateBegin,
                                       string dateEnd, ref DataSet dsResult)
        {
            if (this.Sql.GetSql("Local.Clinic.GetDayBalanceDataNew.Select", ref SQL) == -1)
            {
                this.Err = "查找Sql语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;

            }
            if (this.ExecQuery(SQL, ref dsResult) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }

        #region 门诊日结 luoff

        public int GetDayBalanceDataMZRJ(string employeeID, string dateBegin,
                                         string dateEnd, ref DataSet dsResult)
        {
            if (this.Sql.GetSql("GYZL.Local.Clinic.GetDayBalanceDataMZRJ.Select", ref SQL) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref dsResult) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;

        }
        #endregion

        public int GetDayBalanceDataMZRJReprint(string employeeID, string dateBegin,
                                 string dateEnd, ref DataSet dsResult)
        {
            if (this.Sql.GetSql("GYZL.Local.Clinic.GetDayBalanceDataMZRJReprint.Select", ref SQL) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref dsResult) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;

        }
        /// <summary>
        /// 获取日结发票数据
        /// </summary>
        /// <param name="employeeID">收款员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="dsResult">返回数据集</param>
        /// <returns>1：成功/-1：失败</returns>
        public int GetDayInvoiceDataNew(string employeeID, string dateBegin,
                                       string dateEnd, ref DataSet dsResult)
        {
            if (this.Sql.GetSql("Local.Clinic.GetDayInvoiceDataNew.Select", ref SQL) == -1)
            {
                this.Err = "查找Sql语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;

            }
            if (this.ExecQuery(SQL, ref dsResult) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取日结发票数据
        /// </summary>
        /// <param name="employeeID">收款员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="dsResult">返回数据集</param>
        /// <returns>1：成功/-1：失败</returns>
        public int GetDayInvoiceDataNewReprint(string employeeID, string dateBegin,
                                       string dateEnd, ref DataSet dsResult)
        {
            if (this.Sql.GetSql("Local.Clinic.GetDayInvoiceDataNewReprint.Select", ref SQL) == -1)
            {
                this.Err = "查找Sql语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;

            }
            if (this.ExecQuery(SQL, ref dsResult) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 生成结算数据
        /// </summary>
        /// <param name="clicicDayBlanceNew">结算数据实体</param>
        /// <returns>1成功-1失败</returns>
        public int InsertClinicDayBalance(Models.ClinicDayBalanceNew clicicDayBlanceNew)
        {
            if (this.Sql.GetSql("Local.Clinic.InsertDayBalanceDataNew", ref SQL) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL,
                                  clicicDayBlanceNew.BlanceNO,
                                  clicicDayBlanceNew.BeginTime.ToString(),
                                  clicicDayBlanceNew.EndTime.ToString(),
                                  clicicDayBlanceNew.TotCost,
                                  clicicDayBlanceNew.Oper.ID,
                                  clicicDayBlanceNew.Oper.Name,
                                  clicicDayBlanceNew.Oper.OperTime.ToString(),
                                  clicicDayBlanceNew.InvoiceNO.ID,
                                  clicicDayBlanceNew.InvoiceNO.Name,
                                  clicicDayBlanceNew.BegionInvoiceNO,
                                  clicicDayBlanceNew.EndInvoiceNo,
                                  clicicDayBlanceNew.FalseInvoiceNo,
                                  clicicDayBlanceNew.CancelInvoiceNo,
                                  clicicDayBlanceNew.TypeStr,
                                  clicicDayBlanceNew.SortID);

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(SQL);
        }

        /// <summary>
        /// 查找日结数据
        /// </summary>
        /// <param name="employeeID">收款员</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">终止时间</param>
        /// <returns></returns>
        public int GetDayBalanceRecord(string strSequence, ref DataSet dsResult)
        {
            if (this.Sql.GetSql("Local.Clinic.SelectDayBalanceRecord", ref SQL) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, strSequence);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref dsResult) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 获取日结退费金额
        /// </summary>
        /// <param name="employeeID">操作员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">终止时间</param>
        /// <param name="cancelMoney">返回的退费金额</param>
        /// <returns>1成功-1失败</returns>
        public int GetDayBalanceCancelMoney(string employeeID, string dateBegin, string dateEnd,ref decimal cancelMoney)
        {
            if (this.Sql.GetSql("Local.Clinic.GetDayInvoiceCancelMoney", ref SQL) == -1)
            {
                this.Err = "查找Sql语句失败！";
                return -1;
            }
            try
            {
                this.SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
                cancelMoney = NConvert.ToDecimal(this.ExecSqlReturnOne(SQL));
                return 1;
            }
            catch(Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 获取日结退费金额
        /// </summary>
        /// <param name="employeeID">操作员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">终止时间</param>
        /// <param name="cancelMoney">返回的退费金额</param>
        /// <returns>1成功-1失败</returns>
        public int GetDayBalanceCancelMoneyReprint(string employeeID, string dateBegin, string dateEnd, ref decimal cancelMoney)
        {
            if (this.Sql.GetSql("Local.Clinic.GetDayBalanceCancelMoneyReprint", ref SQL) == -1)
            {
                this.Err = "查找Sql语句失败！";
                return -1;
            }
            try
            {
                this.SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
                cancelMoney = NConvert.ToDecimal(this.ExecSqlReturnOne(SQL));
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 获取日结作废金额
        /// </summary>
        /// <param name="employeeID">操作员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">终止时间</param>
        /// <param name="falseMoney">返回的作废金额</param>
        /// <returns>1成功-1失败</returns>
        public int GetDayBalanceFalseMoney(string employeeID, string dateBegin, string dateEnd, ref decimal falseMoney)
        {
            if (this.Sql.GetSql("Local.Clinic.GetDayInvoiceFalseMoney.Select", ref SQL) == -1)
            {
                this.Err = "查找Sql语句失败！";
                return -1;
            }
            try
            {
                this.SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
                falseMoney = NConvert.ToDecimal(this.ExecSqlReturnOne(SQL));
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 获取日结作废金额
        /// </summary>
        /// <param name="employeeID">操作员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">终止时间</param>
        /// <param name="falseMoney">返回的作废金额</param>
        /// <returns>1成功-1失败</returns>
        public int GetDayBalanceFalseMoneyReprint(string employeeID, string dateBegin, string dateEnd, ref decimal falseMoney)
        {
            if (this.Sql.GetSql("Local.Clinic.GetDayInvoiceFalseMoneyReprint.Select", ref SQL) == -1)
            {
                this.Err = "查找Sql语句失败！";
                return -1;
            }
            try
            {
                this.SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
                falseMoney = NConvert.ToDecimal(this.ExecSqlReturnOne(SQL));
                return 1;
            }
            catch(Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// 获取日结四舍五入金额
        /// </summary>
        /// <param name="employeeID">操作员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">终止时间</param>
        /// <param name="modeMoney">返回四舍五入金额</param>
        /// <returns>1成功-1失败</returns>
        public int GetDayBalanceModeMoney(string employeeID, string dateBegin, string dateEnd, ref decimal modeMoney)
        { 
            if (this.Sql.GetSql("Local.Clinic.GetDayModeMoney", ref SQL) == -1)
            {
                this.Err = "查找Sql语句失败！";
                return -1;
            }
            try
            {
                this.SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
                modeMoney = NConvert.ToDecimal(this.ExecSqlReturnOne(SQL));
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// 获取公费、省、市医护金额
        /// </summary>
        /// <param name="employeeID">操作员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">终止时间</param>
        /// <param name="ds">dataSet</param>
        /// <returns>1成功 -1失败</returns>
        public int GetDayBalanceProtectMoney(string employeeID, string dateBegin, string dateEnd, ref DataSet ds)
        {
            if (this.Sql.GetSql("Local.Clinic.GetProtectMoney", ref SQL) == -1)
            {
                this.Err = "查找Sql语句失败！";
                return -1;
            }
            try
            {
                //1是门诊数据　2是住院数据
                this.SQL = string.Format(SQL, employeeID, dateBegin, dateEnd,"1");
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref ds) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取公费医护金额
        /// </summary>
        /// <param name="employeeID">操作员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">终止时间</param>
        /// <param name="pactCode">合同单位</param>
        /// <param name="ds">dataSet</param>
        /// <returns>1成功 -1失败</returns>
        public int GetDayBalancePublicMoney(string employeeID, string dateBegin, string dateEnd,string pactCode, ref DataSet ds)
        {
            if (this.Sql.GetSql("Local.Clinic.GetPublicMoney", ref SQL) == -1)
            {
                this.Err = "查找Sql语句失败！";
                return -1;
            }
            try
            {
                //1是门诊数据　2是住院数据
                this.SQL = string.Format(SQL, employeeID, dateBegin, dateEnd,pactCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref ds) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }
        //{745DF4AC-4A2D-47e8-A4D1-8D8A80D6C2B8}
        /// <summary>
        /// 获取减免金额
        /// </summary>
        /// <param name="employeeID">操作员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">终止时间</param>
        /// <param name="pactCode">合同单位</param>
        /// <param name="ds">dataSet</param>
        /// <returns>1成功 -1失败</returns>
        public int GetDayBalanceRebateMoney(string employeeID, string dateBegin, string dateEnd, ref DataSet ds)
        {
            if (this.Sql.GetSql("Local.Clinic.GetRebateMoney", ref SQL) == -1)
            {
                this.Err = "查找Sql语句失败！";
                return -1;
            }
            try
            {
                //1是门诊数据　2是住院数据
                this.SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref ds) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }

        //{C6FD30B5-7F6F-4991-86F6-49473E94C669}
        /// <summary>
        /// 获取减免与医保金额
        /// </summary>
        /// <param name="employeeID">操作员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">终止时间</param>
        /// <param name="pactCode">合同单位</param>
        /// <param name="ds">dataSet</param>
        /// <returns>1成功 -1失败</returns>
        public int GetDayBalanceDerateAndProtectMoney(string employeeID, string dateBegin, string dateEnd, ref DataSet ds)
        {
            if (this.Sql.GetSql("Local.Clinic.GetRebateAndProtectMoney", ref SQL) == -1)
            {
                this.Err = "查找Sql语句失败！";
                return -1;
            }
            try
            {
                //1是门诊数据　2是住院数据
                this.SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref ds) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }


        /// <summary>
        /// 按支付方式查找金额
        /// </summary>
        /// <param name="employeeID">操作员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">终止时间</param>
        /// <param name="ds">dataSet</param>
        /// <returns>1成功 -1失败</returns>
        public int GetDayBalancePayTypeMoney(string employeeID, string dateBegin, string dateEnd, ref DataSet ds)
        {
            if (this.Sql.GetSql("Local.Clinic.GetPayTypeMoney", ref SQL) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref ds) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 按支付方式查找金额
        /// </summary>
        /// <param name="employeeID">操作员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">终止时间</param>
        /// <param name="ds">dataSet</param>
        /// <returns>1成功 -1失败</returns>
        public int GetDayBalancePayTypeMoneyReprint(string employeeID, string dateBegin, string dateEnd, ref DataSet ds)
        {
            if (this.Sql.GetSql("Local.Clinic.GetPayTypeMoneyReprint", ref SQL) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref ds) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取合同单位报销金额
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QueryDayBalancePactPubMoney(string employeeID, string dateBegin, string dateEnd, ref DataSet ds)
        {
            if (this.Sql.GetSql("Local.Clinic.QueryDayBalancePactPubMoney", ref SQL) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref ds) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取合同单位报销金额
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QueryDayBalancePactPubMoneyReprint(string employeeID, string dateBegin, string dateEnd, ref DataSet ds)
        {
            if (this.Sql.GetSql("Local.Clinic.QueryDayBalancePactPubMoneyReprint", ref SQL) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref ds) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 查找汇总数据信息
        /// </summary>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public int GetCollectDayBalanceInfo(string dateBegin, string dateEnd, ref List<Models.ClinicDayBalanceNew> list)
        {
            if (this.Sql.GetSql("Local.Clinic.GetCollectDayBalanceInfo", ref SQL) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, dateBegin, dateEnd);
                if (this.ExecQuery(SQL) == -1)
                {
                    this.Err = "查找数据失败！";
                    return -1;
                }
                Models.ClinicDayBalanceNew obj = null;
                while (this.Reader.Read())
                {
                    obj = new Models.ClinicDayBalanceNew();
                    obj.BlanceNO = Reader[0].ToString();
                    obj.Oper.Name = Reader[1].ToString();
                    obj.BeginTime = NConvert.ToDateTime(Reader[2]);
                    obj.EndTime = NConvert.ToDateTime(Reader[3]);
                    obj.BegionInvoiceNO = Reader[4].ToString();
                    obj.EndInvoiceNo = Reader[5].ToString();
                    list.Add(obj);
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句失败！" + ex.Message;
                return -1;
            }

        }   /// <summary>
        #region {A233C411-4B52-4831-AF89-8D7C2CE8D09E} 日结汇总加补打功能
        /// 查找汇总数据信息
        /// </summary>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public int GetCheckedCollectDayBalanceInfo(string dateBegin, string dateEnd, ref List<Models.ClinicDayBalanceNew> list)
        {
            if (this.Sql.GetSql("Local.Clinic.GetCheckCollectDayBalanceInfo", ref SQL) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, dateBegin, dateEnd);
                if (this.ExecQuery(SQL) == -1)
                {
                    this.Err = "查找数据失败！";
                    return -1;
                }
                Models.ClinicDayBalanceNew obj = null;
                while (this.Reader.Read())
                {
                    obj = new Models.ClinicDayBalanceNew();
                    obj.BlanceNO = Reader[0].ToString();
                    obj.Oper.Name = Reader[1].ToString();
                    obj.BeginTime = NConvert.ToDateTime(Reader[2]);
                    obj.EndTime = NConvert.ToDateTime(Reader[3]);
                    obj.BegionInvoiceNO = Reader[4].ToString();
                    obj.EndInvoiceNo = Reader[5].ToString();
                    obj.Memo = Reader[6].ToString();
                    list.Add(obj);
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句失败！" + ex.Message;
                return -1;
            }

        } 
        #endregion

        /// <summary>
        /// 查找日结汇总数据
        /// </summary>
        ///<param name="balanceNos">日结算序号</param>
        /// <param name="ds">DataSet</param>
        /// <returns>1：成功-1失败</returns>
        public int GetCollectDayBalanceData(string balanceNos, ref DataSet ds)
        {
            if (this.Sql.GetSql("Local.Clinic.SelecCollectDayBalanceData", ref SQL) == -1)
            {
                this.Err = "查找Sql语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, balanceNos);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句失败！" + ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref ds) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 保存汇总数据
        /// </summary>
        /// <param name="operID">审核人编码</param>
        /// <param name="operTime">审核时间</param>
        /// <param name="balanceNos">结算序号</param>
        /// <returns>1成功 -1失败</returns>
        public int SaveCollectData(string operID, DateTime operTime, string balanceNos)
        {
            if (this.Sql.GetSql("Local.Clinic.SaveDayBalanceCollectData", ref SQL) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, operID, operTime.ToString(), balanceNos);
            }
            catch
            { 
                this.Err="格式化SQL语句失败！";
                return -1;
            }
            return this.ExecNoQuery(SQL);
        }


        #region 门诊收费取消日结

        /// <summary>
        /// 获取最近一次日结数据
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="balance"></param>
        /// <returns></returns>
        public int QueryLastBalanceRecord(string operID, out FS.FrameWork.Models.NeuObject balance)
        {
            balance = null;
            string strSql = string.Empty;

            if (this.Sql.GetSql("Fee.Daybalance.Outpatient.DayBalance.GetLastBalance", ref strSql) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Outpatient.DayBalance.GetLastBalance 的SQL语句";

                return -1;
            }

            try
            {
                strSql = string.Format(strSql, operID, operID);

                int intReturn = this.ExecQuery(strSql);
                if (intReturn == -1)
                {
                    this.Err = "执行SQL语句失败 " + this.Err;
                    return -1;
                }

                balance = new FS.FrameWork.Models.NeuObject();
                while (this.Reader.Read())
                {
                    balance.ID = this.Reader[0].ToString();
                    balance.Name = this.Reader[1].ToString();
                    balance.Memo = this.Reader[2].ToString();
                    balance.User01 = this.Reader[3].ToString();
                    balance.User02 = this.Reader[4].ToString();
                }

                this.Reader.Close();
            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                this.WriteErr();
                return -1;
            }

            return 1;
        }


        /// <summary>
        /// 取消日结
        /// </summary>
        /// <param name="balanceNo">日结号</param>
        /// <param name="beginDate">日结开始时间</param>
        /// <param name="endDate">日结结束时间</param>
        /// <returns></returns>
        public int UnDoOperDayBalance(string balanceNo, string beginDate, string endDate)
        {
            string strSql = string.Empty;

            #region 删除收入中间表
            if (this.Sql.GetSql("Fee.Daybalance.Outpatient.UnDoDayBalance.DeleteIncom", ref strSql) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Outpatient.UnDoDayBalance.DeleteIncom 的SQL语句";

                return -1;
            }

            try
            {
                strSql = string.Format(strSql, balanceNo);
                if (this.ExecNoQuery(strSql) == -1)
                {
                    return -1;
                }
            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                this.WriteErr();
                return -1;
            }
            #endregion

            #region 删除挂号统计中间表

            if (this.Sql.GetSql("Fee.Daybalance.Outpatient.UnDoDayBalance.DeleteRegPeople", ref strSql) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Outpatient.UnDoDayBalance.DeleteRegPeople 的SQL语句";

                return -1;
            }

            try
            {
                strSql = string.Format(strSql, balanceNo);
                if (this.ExecNoQuery(strSql) == -1)
                {
                    return -1;
                }
            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                this.WriteErr();
                return -1;
            }

            #endregion

            #region 更新业务表

            // 更新挂号表
            if (this.Sql.GetSql("Fee.Daybalance.Outpatient.UnDoDayBalance.UpdateRegister", ref strSql) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Outpatient.UnDoDayBalance.UpdateRegister 的SQL语句";

                return -1;
            }

            try
            {
                strSql = string.Format(strSql, balanceNo);
                if (this.ExecNoQuery(strSql) == -1)
                {
                    return -1;
                }
            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                this.WriteErr();
                return -1;
            }

            // 更新发票表
            if (this.Sql.GetSql("Fee.Daybalance.Outpatient.UnDoDayBalance.UpdateInvoiceInfo", ref strSql) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Outpatient.UnDoDayBalance.UpdateInvoiceInfo 的SQL语句";

                return -1;
            }

            try
            {
                strSql = string.Format(strSql, balanceNo, beginDate, endDate);
                if (this.ExecNoQuery(strSql) == -1)
                {
                    return -1;
                }
            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                this.WriteErr();
                return -1;
            }

            // 更新支付表
            if (this.Sql.GetSql("Fee.Daybalance.Outpatient.UnDoDayBalance.UpdatePaymode", ref strSql) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Outpatient.UnDoDayBalance.UpdatePaymode 的SQL语句";

                return -1;
            }

            try
            {
                strSql = string.Format(strSql, balanceNo, beginDate, endDate);
                if (this.ExecNoQuery(strSql) == -1)
                {
                    return -1;
                }
            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                this.WriteErr();
                return -1;
            }

            // 更新费用明细表
            if (this.Sql.GetSql("Fee.Daybalance.Outpatient.UnDoDayBalance.UpdateFeeDetial", ref strSql) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Outpatient.UnDoDayBalance.UpdateFeeDetial 的SQL语句";

                return -1;
            }

            try
            {
                strSql = string.Format(strSql, balanceNo, beginDate, endDate);
                if (this.ExecNoQuery(strSql) == -1)
                {
                    return -1;
                }
            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                this.WriteErr();
                return -1;
            }
            #endregion

            #region 删除日结数据
            if (this.Sql.GetSql("Fee.Daybalance.Outpatient.UnDoDayBalance.DeleteDayBalance", ref strSql) == -1)
            {
                this.Err = "没有找到索引为: Fee.Daybalance.Outpatient.UnDoDayBalance.DeleteDayBalance 的SQL语句";

                return -1;
            }

            try
            {
                strSql = string.Format(strSql, balanceNo);
                if (this.ExecNoQuery(strSql) == -1)
                {
                    return -1;
                }
            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                this.WriteErr();
                return -1;
            }
            #endregion
            return 1;
        }

        #endregion 

        /// <summary>
        /// 获取日结发票数据
        /// </summary>
        /// <param name="employeeID">收款员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="dsResult">返回数据集</param>
        /// <returns>1：成功/-1：失败</returns>
        public int GetDayInvoiceDataGYZL(string employeeID, string dateBegin,
                                       string dateEnd, ref DataSet dsResult)
        {
            if (this.Sql.GetSql("GYZL.Local.Clinic.GetDayInvoiceDataNew.Select", ref SQL) == -1)
            {
                this.Err = "查找Sql语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;

            }
            if (this.ExecQuery(SQL, ref dsResult) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取日结发票数据
        /// </summary>
        /// <param name="employeeID">收款员编码</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="dsResult">返回数据集</param>
        /// <returns>1：成功/-1：失败</returns>
        public int GetDayInvoiceYJDataGYZL(string employeeID, string dateBegin,
                                       string dateEnd, ref DataSet dsResult)
        {
            if (this.Sql.GetSql("GYZL.Local.Clinic.GetDayInvoiceYJDataNew.Select", ref SQL) == -1)
            {
                this.Err = "查找Sql语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;

            }
            if (this.ExecQuery(SQL, ref dsResult) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取门诊发票统计名称
        /// </summary>
        /// <param name="dsResult">门诊发票统计名称集合</param>
        /// <returns>1：成功/-1：失败</returns>
        public int GetDayFeeStatName(ref DataSet dsResult)
        {
            if (this.Sql.GetSql("GYZL.Local.Clinic.GetDayFeeStatName.Select", ref SQL) == -1)
            {
                this.Err = "查找Sql语句失败！";
                return -1;
            }
            if (this.ExecQuery(SQL, ref dsResult) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取门诊发票统计名称
        /// </summary>
        /// <returns>门诊发票统计名称集合</returns>
        public DataSet GetFeeStatNameByReportCode(string report_code)
        {
            System.Data.DataSet ds = new DataSet();
            try
            {
                string strSql = "";
                if (this.Sql.GetSql("GYZL.Report.GetfsFeeStatNameByReportCode", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return null;
                }
                else
                {
                    strSql = string.Format(strSql, report_code);
                }
                this.ExecQuery(strSql, ref ds);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 根据SQLID查询数据集
        /// </summary>
        /// <param name="employeeID">收费员代码</param>
        /// <param name="statClass">t</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">截止时间</param>
        /// <param name="dsResult">返回数据集</param>
        /// <returns>成功：1；失败：-1</returns>
        public int GetDataSetBySqlID(string sqlID, string employeeID, string dateBegin,
                                       string dateEnd, string report_code, ref DataSet dsResult)
        {
            if (dsResult == null)
            {
                dsResult = new DataSet();
            }
            string sqlStr = string.Empty;

            if (this.Sql.GetSql(sqlID, ref sqlStr) == -1)
            {
                this.Err = "查找索引为" + sqlID + "的SQL语句失败!";
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr, employeeID, dateBegin, dateEnd, report_code);
                if (this.ExecQuery(sqlStr, ref dsResult) == -1)
                {
                    this.Err = "执行" + sqlID + "的SQL语句失败!";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = "执行SQL语句失败!" + ex.Message;
                return -1;
            }

            return 1;
        }


        #endregion

        #region  账户日结

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="LastBalanceDate"></param>
        /// <param name="CurDate"></param>
        /// <returns></returns>
        public int GetAccBalanceDate(string operCode, ref DateTime LastBalanceDate, ref DateTime CurDate)
        {
            string sql = "";
            if (this.Sql.GetSql("ACC.Daybalance.Getoperdate", ref sql) == -1)
            {
                return -1;
            }
            this.ExecQuery(sql, operCode);
            while (this.Reader.Read())
            {
                LastBalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[0]);
                CurDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[1]);
            }
            this.Reader.Close();
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="LastBalanceDate"></param>
        /// <param name="CurDate"></param>
        /// <returns></returns>
        public int GetAccBalanceInvoice(string operCode, string LastBalanceDate, string  CurDate,string balanceFlag,ref int invoiceCount, ref string beginInvoice, ref string endInvoice)
        {
            string sql = "";
            if (this.Sql.GetSql("ACC.Daybalance.GetAccBalanceInvoice", ref sql) == -1)
            {
                return -1;
            }
            this.ExecQuery(sql, operCode, LastBalanceDate, CurDate, balanceFlag);
            while (this.Reader.Read())
            {
                invoiceCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0]);
                beginInvoice = this.Reader[1].ToString();
                endInvoice = this.Reader[2].ToString();
            }
            this.Reader.Close();
            return 1;
        }

        /// <summary>
        /// 通过结算序号获取结算开始时间和结束时间
        /// </summary>
        /// <param name="balanceNO">结算序号</param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int GetAccBalanceBeginAndEndTime(string balanceNO, ref string beginTime, ref string endTime)
        {
            string sql = "";
            if (this.Sql.GetSql("ACC.Daybalance.GetAccBalanceBeginAndEndTime", ref sql) == -1)
            {
                return -1;
            }
            this.ExecQuery(sql, balanceNO);
            while (this.Reader.Read())
            {
                beginTime = this.Reader[0].ToString();
                endTime = this.Reader[1].ToString();
            }
            this.Reader.Close();
            return 1;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="lastDate"></param>
        /// <param name="curDate"></param>
        /// <param name="totCost"></param>
        /// <param name="cash"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int GetAccBalanceCost(string operCode,string lastDate,string curDate,string balanceFlag,out decimal totCost,out  decimal cash,out decimal pos)
        {
            string sql = "";
            totCost = 0;
            cash = 0;
            pos = 0;
            if (this.Sql.GetSql("ACC.Daybalance.GetAccBalanceCost", ref sql) == -1)
            {
                return -1;
            }
            this.ExecQuery(sql, operCode, lastDate, curDate, balanceFlag);
            while (this.Reader.Read())
            {
                totCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);              
                cash = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[1]);
                pos = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
            }
            this.Reader.Close();
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="lastDate"></param>
        /// <param name="curDate"></param>
        /// <param name="quitCost"></param>
        /// <returns></returns>
        public int GetAccQuitCost(string operCode, string lastDate, string curDate, out decimal quitCost)
        {
            string sql = "";
            quitCost = 0;
            if (this.Sql.GetSql("ACC.Daybalance.GetAccQuitCost", ref sql) == -1)
            {
                return -1;
            }
            this.ExecQuery(sql, operCode,lastDate,curDate);
            while (this.Reader.Read())
            {
                quitCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);             
            }
            this.Reader.Close();
            return 1;
        }

        /// <summary>
        /// 获取历时日结记录
        /// </summary>
        /// <param name="operCode">收费员工号</param>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>成功返回1，不成功返回-1</returns>
        public int GetAccBalanceHistory(string operCode, string begin, string end, ref System.Data.DataSet dsResult)
        {
            int intReturn = 0;
            string sql = "";
            intReturn = this.Sql.GetSql("ACC.Daybalance.GetAccBalanceHistory", ref sql);
            try
            {
                sql = string.Format(sql, operCode,begin,end);
            }
            catch (Exception e)
            {
                this.Err = "格式化SQL语句失败(" + this.Err + ")(" + e.Message + ")";
                return -1;
            }

            //
            // 执行SQL语句
            //
            intReturn = this.ExecQuery(sql, ref dsResult);
            if (intReturn == -1)
            {
                this.Err = "执行SQL语句失败" + this.Err;
                return -1;
            }

            return 1;
        }



       /// <summary>
      /// 
      /// </summary>
      /// <param name="balanceNO"></param>
      /// <param name="operCode"></param>
      /// <param name="lastDate"></param>
      /// <param name="curDate"></param>
      /// <param name="leftCost"></param>
      /// <param name="totCost"></param>
      /// <param name="quitCost"></param>
      /// <param name="ca"></param>
      /// <param name="pos"></param>
      /// <param name="checkFlag"></param>
      /// <param name="checkOpcd"></param>
      /// <param name="checkDate"></param>
      /// <returns></returns>
        public int SaveBalance(string balanceNO, string operCode, string lastDate, string curDate, decimal leftCost, decimal totCost, decimal quitCost,decimal ca,decimal pos,string checkFlag,string checkOpcd,string checkDate,string beginInvoice,string endInvoice)
        {
            return this.UpdateSingleTable("ACC.Daybalance.SaveBalance", balanceNO, lastDate, curDate, operCode, leftCost.ToString(), totCost.ToString(), quitCost.ToString(), ca.ToString(), pos.ToString(), checkFlag, checkOpcd, checkDate, beginInvoice, endInvoice);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opercode"></param>
        /// <param name="lastDate"></param>
        /// <param name="curDate"></param>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int UpdateBalanceFlag(string opercode, string lastDate, string curDate, string balanceNO)
        {
            return this.UpdateSingleTable("ACC.Daybalance.UpdateBalanceFlag", opercode, lastDate, curDate, balanceNO);
        }
        #endregion

        #region 日结明细数据
        /// <summary>
        /// 收款员缴款报表日结明细数据
        /// </summary>
        /// <param name="employeeID">收款员</param>
        /// <param name="dateBegin">上次日结时间</param>
        /// <param name="dateEnd">本次日结时间</param>
        /// <param name="deptCode">科室</param>
        /// <param name="statCode">统计大类</param>
        /// <param name="dsResult">返回程序集</param>
        /// <returns></returns>
        public int GetDayBalanceDataMZRJDetail(string employeeID, string dateBegin,
                                       string dateEnd,string deptCode,string statCode, ref DataSet dsResult)
        {
            if (this.Sql.GetSql("GYZL.Local.Clinic.GetDayBalanceDataMZRJ.DetailSelect", ref SQL) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd,deptCode,statCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref dsResult) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;

        }

        /// <summary>
        /// 收款员缴款报表日结明细数据
        /// </summary>
        /// <param name="employeeID">收款员</param>
        /// <param name="dateBegin">上次日结时间</param>
        /// <param name="dateEnd">本次日结时间</param>
        /// <param name="PatientCardNO">患者门诊号</param>
        /// <param name="deptCode">科室</param>
        /// <param name="statCode">统计大类</param>
        /// <param name="dsResult">返回程序集</param>
        /// <returns></returns>
        public int GetDayBalanceDataMZRJDetailFeeBack(string employeeID, string dateBegin,
                                       string dateEnd, string PatientCardNO,string deptCode, string statCode, ref DataSet dsResult)
        {
            if (this.Sql.GetSql("GYZL.Local.Clinic.GetDayBalanceDataMZRJ.FeeBackDetailSelect", ref SQL) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd, PatientCardNO,deptCode, statCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref dsResult) == -1)
            {
                this.Err = "执行SQL语句失败！";
                return -1;
            }
            return 1;

        }
        #endregion

    }
}
