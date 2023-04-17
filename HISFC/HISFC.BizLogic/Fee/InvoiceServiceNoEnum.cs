using System;
using System.Collections;
using FS.FrameWork.Function;
using FS.HISFC.Models.Fee;
using System.Data;
using System.Collections.Generic;

namespace FS.HISFC.BizLogic.Fee
{

    public class InvoiceServiceNoEnum:FS.FrameWork.Management.Database
    {

        #region 私有方法

        /// <summary>
        /// 检索发票信息

        /// </summary>
        /// <param name="sql">执行的查询SQL语句</param>
        /// <param name="args">SQL语句的参数</param>
        /// <returns>成功:发票信息数组 失败:null 没有查找到数据返回元素数为0的ArrayList</returns>
        private ArrayList QueryInvoicesBySql(string sql, params string[] args)
        {
            ArrayList invoices = new ArrayList();

            //执行SQL语句
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            try
            {
                //循环读取数据
                while (this.Reader.Read())
                {
                    Invoice invoice = new Invoice();

                    invoice.AcceptTime = NConvert.ToDateTime(this.Reader[0].ToString());
                    invoice.Type.ID = this.Reader[1].ToString();
                    invoice.AcceptOper.ID = this.Reader[2].ToString();
                    invoice.BeginNO = this.Reader[3].ToString();
                    invoice.EndNO = this.Reader[4].ToString();
                    invoice.UsedNO = this.Reader[5].ToString();
                    invoice.ValidState = this.Reader[6].ToString();
                    invoice.IsPublic = NConvert.ToBoolean(this.Reader[7].ToString());
                    invoice.AcceptOper.Name = this.Reader[8].ToString();

                    invoices.Add(invoice);
                }//循环结束

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }

            return invoices;
        }

        /// <summary>
        /// 获得update或者insert退费申请的传入参数数组
        /// </summary>
        /// <param name="invoice">发票实体类</param>
        /// <returns>参数数组</returns>
        private string[] GetInvoiceParams(Invoice invoice)
        {
            string[] args =
				{
					invoice.AcceptTime.ToString(),
					invoice.AcceptOper.ID,
					invoice.Type.ID.ToString(),
					invoice.Type.Name,
					invoice.BeginNO,
					invoice.EndNO,
					invoice.UsedNO,
					invoice.ValidState.ToString(),
					NConvert.ToInt32(invoice.IsPublic).ToString(),
					this.Operator.ID,
					this.GetSysDateTime()
				};

            return args;
        }

        /// <summary>
        /// 检索获得发票的人员信息
        /// </summary>
        /// <param name="sql">执行的查询SQL语句</param>
        /// <param name="args">SQL语句的参数</param>
        /// <returns>成功:人员信息数组 失败:null 没有查找到数据返回元素数为0的ArrayList</returns>
        private ArrayList QueryPersonsBySql(string sql, params string[] args)
        {
            ArrayList persons = new ArrayList();

            //执行SQL语句
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            try
            {
                //循环读取数据
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Base.Employee person = new FS.HISFC.Models.Base.Employee();

                    person.ID = this.Reader[0].ToString();
                    person.Name = this.Reader[1].ToString();
                    person.SpellCode = this.Reader[2].ToString();
                    person.WBCode = this.Reader[3].ToString();
                    person.Sex.ID = this.Reader[4].ToString();
                    person.Birthday = this.Reader.GetDateTime(5);
                    person.Duty.ID = this.Reader[6].ToString();
                    person.Level.ID = this.Reader[7].ToString();
                    person.GraduateSchool.ID = this.Reader[8].ToString();
                    person.IDCard = this.Reader[9].ToString();
                    person.Dept.ID = this.Reader[10].ToString();
                    person.Nurse.ID = this.Reader[11].ToString();
                    person.EmployeeType.ID = Reader[12].ToString();
                    person.IsExpert = NConvert.ToBoolean(Reader[13].ToString());
                    person.IsCanModify = NConvert.ToBoolean(Reader[14].ToString());
                    person.IsNoRegCanCharge = NConvert.ToBoolean(this.Reader[15].ToString());
                    person.ValidState = (HISFC.Models.Base.EnumValidState)NConvert.ToInt32(this.Reader[16].ToString());
                    person.SortID = NConvert.ToInt32(this.Reader[17].ToString());

                    persons.Add(person);
                }//循环结束

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }

            return persons;
        }

        /// <summary>
        /// 获得发票变更参数数组
        /// </summary>
        /// <param name="invoiceChange"></param>
        /// <returns></returns>
        private string[] GetInvoiceChangeParms(InvoiceChange invoiceChange)
        {
            string[] args =
				{
					invoiceChange.HappenNO.ToString(),
					invoiceChange.GetOper.ID,
					invoiceChange.InvoiceType.ID.ToString(),
                    invoiceChange.InvoiceType.Name,
					
					invoiceChange.BeginNO,
					invoiceChange.EndNO,
					invoiceChange.ShiftType,
					invoiceChange.Oper.ID,
                    invoiceChange.Memo.ToString()
				};

            return args;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 通过发票类型,查询发票信息
        /// </summary>
        /// <param name="invoiceType">发票类别</param>
        /// <returns>成功:发票信息数组 失败:null 没有查找到数据返回元素数为0的ArrayList</returns>
        public ArrayList QueryInvoices(string invoiceType)
        {
            string sql = string.Empty; //查询SQL语句

            if (this.Sql.GetCommonSql("Fee.InvoiceService.SelectInvoices.2", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.InvoiceService.SelectInvoices.2的SQL语句";

                return null;
            }

            return this.QueryInvoicesBySql(sql, invoiceType.ToString());
        }

        /// <summary>
        ///  根据是否是Groupy以及发票类别获得所有使用状态的发票信息。

        /// </summary>
        /// <param name="invoiceType">发票类别</param>
        /// <param name="isGroup">是否发票组</param>
        /// <returns>成功:发票信息数组 失败:null 没有查找到数据返回元素数为0的ArrayList</returns>
        public ArrayList QueryInvoices(string invoiceType, bool isGroup)
        {

            string sql = string.Empty; //查询SQL语句

            if (this.Sql.GetCommonSql("Fee.InvoiceService.SelectInvoices.ByTypeIsGroup", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.InvoiceService.SelectInvoices.ByTypeIsGroup的SQL语句";

                return null;
            }

            return this.QueryInvoicesBySql(sql, invoiceType.ToString(), NConvert.ToInt32(isGroup).ToString());
        }

        /// <summary>
        /// 获得领用过发票类型为InvoiceType的所有人员信息 /
        /// </summary>
        /// <param name="invoiceType">发票类别</param>
        /// <returns>成功:人员信息数组 失败:null 没有查找到数据返回元素数为0的ArrayList</returns>
        public ArrayList QueryPersonsByInvoiceType(string invoiceType)
        {

            string sql = string.Empty; //查询SQL语句

            if (this.Sql.GetCommonSql("Fee.InvoiceService.GetPersonByInvoiceType", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.InvoiceService.GetPersonByInvoiceType的SQL语句";

                return null;
            }

            return this.QueryPersonsBySql(sql, invoiceType.ToString());
        }

        /// <summary>
        /// 通过人员编号,和发票类别查询该人员的发票信息

        /// </summary>
        /// <param name="personID">人员编号</param>
        /// <param name="invoiceType">发票类别</param>
        /// <returns>成功:发票信息数组 失败:null 没有查找到数据返回元素数为0的ArrayList</returns>
        public ArrayList QueryInvoices(string personID, string invoiceType)
        {
            string sql = string.Empty; //查询SQL语句

            if (this.Sql.GetCommonSql("Fee.InvoiceService.SelectInvoices.1", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.InvoiceService.SelectInvoices.1的SQL语句";

                return null;
            }

            return this.QueryInvoicesBySql(sql, personID, invoiceType.ToString());
        }

        /// <summary>
        /// 通过人员编号,和发票类别查询和是否财务组该人员的发票信息

        /// </summary>
        /// <param name="personID">人员编号</param>
        /// <param name="invoiceType">发票类别</param>
        /// <param name="isGroup">是否财务组</param>
        /// <returns>成功:发票信息数组 失败:null 没有查找到数据返回元素数为0的ArrayList</returns>
        public ArrayList QueryInvoices(string personID, string invoiceType, bool isGroup)
        {
            string sql = string.Empty; //查询SQL语句

            if (this.Sql.GetCommonSql("Fee.InvoiceService.SelectInvoices.ByIdTypeIsGroup", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.InvoiceService.SelectInvoices.ByIdTypeIsGroup的SQL语句";

                return null;
            }

            return this.QueryInvoicesBySql(sql, personID, invoiceType.ToString(), NConvert.ToInt32(isGroup).ToString().ToString());
        }

        /// <summary>
        /// 根据发票类型得到当前可用的起始号(默认)
        /// </summary>
        /// <param name="invoiceType">发票类型</param>
        /// <returns>可用起始号</returns>
        public string GetDefaultStartCode(string invoiceType)
        {
            string sql = string.Empty;//查询SQL语句
            string startNO = string.Empty;//起始号


            if (this.Sql.GetCommonSql("Fee.InvoiceService.GetDefaultStartCode.1", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.InvoiceService.GetDefaultStartCode.1的SQL语句";

                return null;
            }

            try
            {
                sql = string.Format(sql, invoiceType);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                return null;
            }

            startNO = this.ExecSqlReturnOne(sql);

            //如果起始号为空,那么默认为"000000000001"
            if (startNO == null || startNO == string.Empty)
            {
                startNO = "000000000001";
            }
            else//否则,为当前号+1
            {
                startNO = FS.FrameWork.Public.String.AddNumber(startNO, 1);// (Convert.ToInt64(startNO) + 1).ToString().PadLeft(12, '0');
            }

            return startNO;
        }

        /// <summary>
        /// 检测所给的起始号和发票数量是否有效：

        /// </summary>
        /// <param name="startNO">起始号</param>
        /// <param name="endNO">发票数量</param>
        /// <param name="invoiceType">发票类型</param>
        /// <returns>有效true, 无效 false</returns>
        public bool InvoicesIsValid(long startNO, long endNO, string invoiceType)
        {

            if (endNO < startNO)
            {
                this.Err = "输入的终止号大于起始号!";

                return false;
            }

            string sql = string.Empty;

            ArrayList invoices = new ArrayList();

            if (this.Sql.GetCommonSql("Fee.InvoiceService.SelectInvoices.2", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.InvoiceService.SelectInvoices.2的SQL语句";

                return false;
            }

            invoices = QueryInvoicesBySql(sql, invoiceType.ToString());

            //如果没有符合条件的发票,说明可以生成
            if (invoices == null)
            {
                return true;
            }

            for (int i = 0; i < invoices.Count; i++)
            {
                Invoice invoice = invoices[i] as Invoice;

                if (FS.FrameWork.Public.String.GetNumber(invoice.BeginNO) <= startNO && startNO <= FS.FrameWork.Public.String.GetNumber(invoice.EndNO))
                {
                    return false;
                }
                if (FS.FrameWork.Public.String.GetNumber(invoice.BeginNO) <= endNO && endNO <= FS.FrameWork.Public.String.GetNumber(invoice.EndNO))
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// 检测所给的起始号和发票数量是否有效：
        /// </summary>
        /// <param name="startNO">起始号</param>
        /// <param name="endNO">发票数量</param>
        /// <param name="invoiceType">发票类型</param>
        /// <returns>有效true, 无效 false</returns>
        public bool InvoicesIsValid(string startNO, string endNO, string invoiceType)
        {

            if (endNO.CompareTo( startNO)<0)
            {
                this.Err = "输入的终止号大于起始号!";

                return false;
            }

            string sql = string.Empty;

            ArrayList invoices = new ArrayList();

            //if (this.Sql.GetCommonSql("Fee.InvoiceService.SelectInvoices.ByInvoiceNO", ref sql) == -1)
            {
                sql = @"	     SELECT b.get_dtime,   --时间     
                                       b.invoice_kind,   --发票种类
                                       b.get_person_code,  --领取人
                                       b.start_no,   --发票开始号
                                       b.end_no,   --发票终止号
                                       b.used_no,   --发票已用号
                                       b.used_state,   --1：使用，0：未用，-1：已用
                                       b.is_pub ,  --公用标志 
                                       (select  a.empl_name from com_employee a where a.empl_code =b.get_person_code  )  
                                  FROM fin_com_invoice b    --发票领取记录
                                 WHERE  b.invoice_kind = '{0}' 
                                 and ((b.start_no <= '{1}' and b.end_no>='{1}') or (b.start_no<='{2}' and b.end_no>='{2}'))";
            }

            invoices = QueryInvoicesBySql(sql, invoiceType.ToString(),startNO,endNO);

            //如果没有符合条件的发票,说明可以生成
            if (invoices == null||invoices.Count==0)
            {
                return true;
            }


            return false;
        }

        /// <summary>
        /// 插入一条发票信息.
        /// </summary>
        /// <param name="invoice">发票信息类</param>
        /// <returns> 成功: 1 失败: -1 没有插入记录: 0</returns>
        public int InsertInvoice(Invoice invoice)
        {
            string sql = string.Empty;//插入SQL语句

            if (this.Sql.GetCommonSql("Fee.InvoiceService.InsertInvoice.1", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.InvoiceService.InsertInvoice.1的SQL语句";

                return -1;
            }

            return this.ExecNoQuery(sql, this.GetInvoiceParams(invoice));
        }

        /// <summary>
        /// 更新一条发票信息.发票回收专用
        /// </summary>
        /// <param name="invoice">发票信息类</param>
        /// <returns> 成功: 1 失败: -1 没有更新记录: 0</returns>
        public int UpdateInvoice(Invoice invoice)
        {
            string sql = string.Empty;//插入SQL语句

            if (this.Sql.GetCommonSql("Fee.InvocieService.UpdateInvoice.1", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.InvocieService.UpdateInvoice.1的SQL语句";

                return -1;
            }

            return this.ExecNoQuery(sql, this.GetInvoiceParams(invoice));
        }

        /// <summary>
        /// 删除一条记录

        /// </summary>
        /// <param name="invoice">发票信息类</param>
        /// <returns>成功: 删除的条目 失败: -1 没有删除记录: 0</returns>
        public int Delete(Invoice invoice)
        {
            string sql = string.Empty;//插入SQL语句

            if (this.Sql.GetCommonSql("Fee.InvocieService.DeleteInvoice.1", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.InvocieService.DeleteInvoice.1的SQL语句";

                return -1;
            }

            return this.ExecNoQuery(sql, invoice.AcceptTime.ToString(), invoice.AcceptOper.ID);
        }

        /// <summary>
        /// 更具操作员或财务组查询发票类型 //{BF01254E-3C73-43d4-A644-4B258438294E}
        /// </summary>
        /// <param name="operID"></param>
        /// <param name="finGroupID"></param>
        /// <returns></returns>
        public ArrayList GetInvoiceTypeByOperIDORFinGroupID(string operID, string finGroupID)
        {
            string sql = string.Empty;//插入SQL语句

            if (this.Sql.GetCommonSql("Fee.InvocieService.GetInvoice.ByOperIDORFinGroupID", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.InvocieService.GetInvoice.ByOperIDORFinGroupID的SQL语句";

                return null;
            }

            try
            {
                sql = string.Format(sql, operID, finGroupID);
            }
            catch (Exception ex)
            {

                this.Err = ex.Message;
            }

            int returnValue = this.ExecQuery(sql);

            if (returnValue == -1)
            {
                this.Err = Err;
                return null;
            }

            ArrayList al = new ArrayList();

            FS.FrameWork.Models.NeuObject neuObj = null;
            try
            {
                while (this.Reader.Read())
                {
                    neuObj = new FS.FrameWork.Models.NeuObject();
                    neuObj.ID = this.Reader[0].ToString();
                    neuObj.Name = this.Reader[1].ToString();
                    al.Add(neuObj);
                }
            }
            catch (Exception ex)
            {

                this.Err = ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        /// <summary>
        /// 更新发票段是否为默认号段 //{BF01254E-3C73-43d4-A644-4B258438294E}
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public int UpdateInvoiceDefaltState(Invoice invoice)
        {
            string sql = string.Empty;//插入SQL语句

            if (this.Sql.GetCommonSql("Fee.InvocieService.UpdateInvoice.2", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.InvocieService.UpdateInvoice.2的SQL语句";

                return -1;
            }

            return this.ExecNoQuery(sql, this.GetInvoiceParams(invoice));

        }

        /// <summary>
        /// 更新当前使用号 //{BF01254E-3C73-43d4-A644-4B258438294E}
        /// </summary>
        /// <param name="acceptCode"></param>
        /// <param name="invoiceTypeID"></param>
        /// <returns></returns>
        public int UpdateUsedNO(string usedNO, string acceptCode, string invoiceTypeID)
        {
            string StrSql = string.Empty;

            int returnValue = this.Sql.GetCommonSql("Fee.Invoice.update.usedNO", ref StrSql);

            if (returnValue < 0)
            {
                this.Err = "没有找到索引为Fee.Invoice.update.usedNO的SQL语句";
                return -1;
            }

            StrSql = string.Format(StrSql, usedNO, acceptCode, invoiceTypeID);

            return this.ExecNoQuery(StrSql);

        }

        //{A6DB46E9-EDD4-47d3-B2E5-1D8966DBBA43}
        /// <summary>
        /// 根据发票类别领用人查询当前已使用的发票号
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="invoiceType"></param>
        /// <returns></returns>
        public string QueryUsedInvoiceNO(string personID,string invoiceType)
        {
            string sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.InvoiceService.SelectInvoices.3", ref sql) == -1)
            {
                this.Err = "查询索引为Fee.InvoiceService.SelectInvoices.3的SQL语句失败！";
                return "-1";
            }
            sql = string.Format(sql, personID, invoiceType);
            return this.ExecSqlReturnOne(sql,string.Empty);
           
        }

        //{A6DB46E9-EDD4-47d3-B2E5-1D8966DBBA43}
        /// <summary>
        /// 根据发票类别领用人查询未使用的发票号
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="invoiceType"></param>
        /// <returns></returns>
        public string QueryNoUsedInvoiceNO(string personID, string invoiceType)
        {
            string sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.InvoiceService.SelectInvoices.4", ref sql) == -1)
            {
                this.Err = "查询索引为Fee.InvoiceService.SelectInvoices.3的SQL语句失败！";
                return "-1";
            }
            sql = string.Format(sql, personID, invoiceType);
            return this.ExecSqlReturnOne(sql,string.Empty);
        }

        #region 发票变更

        /// <summary>
        /// 插入发票变更记录
        /// </summary>
        /// <param name="invoiceChange"></param>
        /// <returns></returns>
        public int InsertInvoiceChange(InvoiceChange invoiceChange)
        {
            string sql = string.Empty;//插入SQL语句

            if (this.Sql.GetCommonSql("Fee.InvoiceService.InsertInvoiceChange.1", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.InvoiceService.InsertInvoiceChange.1的SQL语句";

                return -1;
            }

            return this.ExecNoQuery(sql, this.GetInvoiceChangeParms(invoiceChange));
        }

        /// <summary>
        /// 按发票领取人取得发票变更序号
        /// </summary>
        /// <param name="getPersonID"></param>
        /// <returns></returns>
        public int GetInvoiceChangeHappenNO(string getPersonID)
        {
            string sql = string.Empty;
            string happenNO = string.Empty;

            if (this.Sql.GetCommonSql("Fee.InvoiceService.GetInvoiceChangeHappenNO.1", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.InvoiceService.GetInvoiceChangeHappenNO.1的SQL语句";

                return -1;
            }

            try
            {
                sql = string.Format(sql, getPersonID);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                return -1;
            }
            happenNO = this.ExecSqlReturnOne(sql);

            if (happenNO == null || happenNO == string.Empty)
            {
                return 1;
            }
            else//否则,为当前号+1
            {
                return (Convert.ToInt32(happenNO) + 1);
            }
        }

        /// <summary>
        /// 更新发票已用号码（发票跳号用）
        /// </summary>
        /// <param name="usedNO"></param>
        /// <param name="getPersonID"></param>
        /// <param name="getDate"></param>
        /// <returns></returns>
        public int UpdateInvoiceUsedNO(string usedNO, string getPersonID, DateTime getDate)
        {
            string sql = string.Empty;//插入SQL语句

            if (this.Sql.GetCommonSql("Fee.InvoiceService.UpdateInvoice.2", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.InvoiceService.UpdateInvoice.2的SQL语句";

                return -1;
            }

            return this.ExecNoQuery(sql, getDate.ToString("yyyy-MM-dd HH:mm:ss"), getPersonID, usedNO);
        }

        #endregion

        #region 发票核销 
       // public int GetOutpatientFeeRegInvoice(string begin, string end, string casher, ref List<FS.FrameWork.Models.NeuObject> list)
        public int GetOutpatientFeeRegInvoice(string begin, string end, string casher, ref List<FS.HISFC.Models.Fee.Invoice> list)
        {
            string sqlStr = string.Empty;
            #region 原来的代码,新增修改的
            //if (this.Sql.GetCommonSql("Fee.CheckInvoice.GetOutpatientRegInvoice", ref sqlStr) == -1)
            //{
            //    this.Err = "查找SQL语句Fee.CheckInvoice.GetOutpatientRegInvoice失败！";
            //    return -1;
            //}
            //try
            //{
            //    sqlStr = string.Format(sqlStr, begin, end, casher);
            //    if (this.ExecQuery(sqlStr) == -1)
            //    {
            //        this.Err = "查找门诊发票数据失败！";
            //        return -1;
            //    }
            //    FS.FrameWork.Models.NeuObject obj = null;
            //    while (this.Reader.Read())
            //    {
            //        obj = new FS.FrameWork.Models.NeuObject();
            //        obj.ID = this.Reader[0].ToString();
            //        obj.Name = this.Reader[1].ToString();
            //        obj.User01 = this.Reader[2].ToString();
            //        obj.User02 = this.Reader[3].ToString();
            //        obj.User03 = this.Reader[4].ToString();
            //        obj.Memo = this.Reader[5].ToString();
            //        //防止其它医院数据库SQL未更新报错
            //        if (this.Reader.FieldCount > 6)
            //        {
            //            obj.Name += "|" + this.Reader[6].ToString();

            //        }
            //        //发票印刷号
            //        if (this.Reader.FieldCount >7)
            //        {
            //            obj.User03+="|" + this.Reader[7].ToString();
            //        }
            //        list.Add(obj);
            //    }
            //    return 1;
            //}
            //catch (Exception ex)
            //{
            //    this.Err = ex.Message;
            //    return -1;
            //}
#endregion 
            #region 默认sql,新增的//cancel_flag,
            sqlStr = @"--挂号收据
 select INVOICE_NO,PRINT_INVOICENO, state,sum(tot_cost) tot_cost, 
        balance_opcd,fun_get_employee_name(balance_opcd),balance_date,fee_date from (

select a.INVOICE_NO,a.PRINT_INVOICENO,decode(a.cancel_flag,'0','退费','1','有效','2','重打','3','注销') as state,
       a.trans_type,a.cancel_flag,a.tot_cost,balance_opcd,balance_date,fee_date 
from(
      select invoice_no,PRINT_INVOICENO,trans_type,cancel_flag,0 tot_cost,
      balance_opcd,balance_date,fee_date 
      from fin_opb_accountcardfee  
      where cancel_flag='0' 
      and balance_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
      and balance_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
      and balance_flag='1'  
      and (BALANCE_OPCD='{2}' or 'ALL'='{2}')
      union all
      select invoice_no,PRINT_INVOICENO,trans_type,cancel_flag,tot_cost,
      balance_opcd,balance_date,fee_date 
      from fin_opb_accountcardfee  
      where cancel_flag='1' and trans_type='1'
      and balance_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
      and balance_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
      and balance_flag='1'  
      and (BALANCE_OPCD='{2}' or 'ALL'='{2}')
      union all
      select invoice_no,PRINT_INVOICENO,trans_type,cancel_flag ,0 tot_cost,
      balance_opcd,balance_date,fee_date  
      from fin_opb_accountcardfee 
      where cancel_flag='2' and trans_type='2'
      and balance_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
      and balance_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
      and balance_flag='1'  
      and (BALANCE_OPCD='{2}' or 'ALL'='{2}')
      union all
      select invoice_no,PRINT_INVOICENO,trans_type,cancel_flag,0 as tot_cost,
      balance_opcd,balance_date,fee_date  
      from fin_opb_accountcardfee  
      where cancel_flag='3' and trans_type='1'
      and balance_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
      and balance_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
      and balance_flag='1'  
      and (BALANCE_OPCD='{2}' or 'ALL'='{2}')
) a
order by a.invoice_no
) group by INVOICE_NO,PRINT_INVOICENO,state,cancel_flag,balance_opcd,balance_date,fee_date
order by INVOICE_NO";

            #endregion
            try
            {
                sqlStr = string.Format(sqlStr, begin, end, casher);
                if (this.ExecQuery(sqlStr) == -1)
                {
                    this.Err = "查找挂号收据数据失败";
                    return -1;
                }
            //    FS.FrameWork.Models.NeuObject obj = null;
                FS.HISFC.Models.Fee.Invoice obj = null;
                while (this.Reader.Read())
                {
                  //  obj = new FS.FrameWork.Models.NeuObject();
                    obj = new FS.HISFC.Models.Fee.Invoice();
                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString();
                    obj.User01 = this.Reader[2].ToString();
                    obj.Memo = this.Reader[3].ToString();
                    if (this.Reader.FieldCount > 4)
                    {
                        obj.User02 = this.Reader[4].ToString();
                    }
                    obj.User03 = this.Reader[5].ToString();
                    obj.BeginNO = this.Reader[6].ToString();
                    obj.EndNO = this.Reader[7].ToString();
                    //if (this.Reader.FieldCount > 5)
                    //{
                    //    obj.User03 = this.Reader[5].ToString();
                    //}
                    //if (this.Reader.FieldCount > 6)
                    //{
                    //    obj.User04 = this.Reader[6].ToString();
                    //}
                    list.Add(obj);
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// 获取门诊发票
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="casher">收款员</param>
        ///<param name="list"></param>
        /// <returns></returns>
    //    public int GetOutpatientFeeInvoice(string begin, string end, string casher, ref List<FS.FrameWork.Models.NeuObject> list)
          public int GetOutpatientFeeInvoice(string begin, string end, string casher, ref List<FS.HISFC.Models.Fee.Invoice> list)
        {
            string sqlStr = string.Empty;
            #region 原来的代码,新增修改的
            //if (this.Sql.GetCommonSql("Fee.CheckInvoice.GetOutpatientInvoice", ref sqlStr) == -1)
            //{
            //    this.Err = "查找SQL语句Fee.CheckInvoice.GetOutpatientInvoice失败！";
            //    return -1;
            //}
            //try
            //{
            //    sqlStr = string.Format(sqlStr, begin, end, casher);
            //    if (this.ExecQuery(sqlStr) == -1)
            //    {
            //        this.Err = "查找门诊发票数据失败！";
            //        return -1;
            //    }
            //    FS.FrameWork.Models.NeuObject obj = null;
            //    while (this.Reader.Read())
            //    {
            //        obj = new FS.FrameWork.Models.NeuObject();
            //        obj.ID = this.Reader[0].ToString();
            //        obj.Name = this.Reader[1].ToString();
            //        obj.User01 = this.Reader[2].ToString();
            //        obj.User02 = this.Reader[3].ToString();
            //        obj.User03 = this.Reader[4].ToString();
            //        obj.Memo = this.Reader[5].ToString();
            //        //防止其它医院数据库SQL未更新报错
            //        if (this.Reader.FieldCount > 6) 
            //        {
            //            obj.Name += "|"+this.Reader[6].ToString();
                        
            //        }
            //        //发票印刷号
            //        if (this.Reader.FieldCount > 7)
            //        {
            //            obj.User03 += "|" + this.Reader[7].ToString();

            //        }

            //        list.Add(obj);
            //    }
            //    return 1;
            //}
            //catch (Exception ex)
            //{
            //    this.Err = ex.Message;
            //    return -1;
            //}
            #endregion
            #region 默认sql
            sqlStr = @"select a.invoice_no,a.PRINT_INVOICENO,decode(a.cancel_flag,'0','退费','1','有效','2','重打','3','注销') as state,
       a.tot_cost,oper_code,fun_get_employee_name(oper_code),balance_date,oper_date
from(
      select invoice_no,PRINT_INVOICENO,trans_type,invoice_seq,cancel_flag,0 tot_cost,
      oper_code,oper_date,balance_date
      from fin_opb_invoiceinfo  
      where cancel_flag='0' and trans_type='2' 
      and balance_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
      and balance_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
      and balance_flag='1' and check_flag='0' 
      and (BALANCE_OPCD='{2}' or 'ALL'='{2}')
      union all
      select invoice_no,PRINT_INVOICENO,trans_type,invoice_seq,cancel_flag,tot_cost,
      oper_code,oper_date,balance_date
      from fin_opb_invoiceinfo  
      where cancel_flag='1' and trans_type='1'
      and balance_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
      and balance_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
      and balance_flag='1' and check_flag='0'
      and (BALANCE_OPCD='{2}' or 'ALL'='{2}')
      union all
      select invoice_no,PRINT_INVOICENO,trans_type,invoice_seq,cancel_flag ,0 tot_cost,
      oper_code,oper_date,balance_date 
      from fin_opb_invoiceinfo 
      where cancel_flag='2' and trans_type='2'
      and balance_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
      and balance_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
      and balance_flag='1' and check_flag='0'
      and (BALANCE_OPCD='{2}' or 'ALL'='{2}')
      union all
      select invoice_no,PRINT_INVOICENO,trans_type,invoice_seq,cancel_flag,0 as tot_cost,
      oper_code,oper_date,balance_date 
      from fin_opb_invoiceinfo  
      where cancel_flag='3' and trans_type='1'
      and balance_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
      and balance_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
      and balance_flag='1' and check_flag='0' 
      and (BALANCE_OPCD='{2}' or 'ALL'='{2}')
) a
order by a.invoice_no";
            #endregion
            try
            {
                sqlStr = string.Format(sqlStr,begin,end,casher);
                if (this.ExecQuery(sqlStr) == -1)
                { 
                   this.Err="查找门诊收据数据失败";
                    return -1;
                }
              //  FS.FrameWork.Models.NeuObject obj=null;
                FS.HISFC.Models.Fee.Invoice obj =null;
                while(this.Reader.Read())
                {
                   //obj=new FS.FrameWork.Models.NeuObject();
                   obj=new FS.HISFC.Models.Fee.Invoice();
                   obj.ID=this.Reader[0].ToString();
                   obj.Name=this.Reader[1].ToString();
                   obj.User01=this.Reader[2].ToString();
                   obj.Memo=this.Reader[3].ToString();
                   //防止其它医院数据库SQL未更新报错
                   if (this.Reader.FieldCount > 4)
                   {
                       obj.User02 = this.Reader[4].ToString();//结算人
                   }
                   //if (this.Reader.FieldCount > 5)
                   //{
                   //    obj.User03 = this.Reader[5].ToString();//印刷号
                   //}
                   //if (this.Reader.FieldCount > 6)
                   //{
                   //    obj.User04 = this.Reader[6].ToString();
                   //}
                   //if (this.Reader.FieldCount > 6)
                   //{
                   //    obj.BeginNO = this.Reader[6].ToString();
                   //}
                   obj.User03 = this.Reader[5].ToString();
                   obj.BeginNO = this.Reader[6].ToString();
                   if (this.Reader.FieldCount > 7)
                   {
                       obj.EndNO = this.Reader[7].ToString();
                   }
                   
                //   obj.EndNO = this.Reader[7].ToString();
                   list.Add(obj);
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

        }

        /// <summary>
        /// 获取住院预交金发票
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="casher"></param>
        /// <param name="list"></param>
        /// <returns></returns>
       // public int GetInpatientPrepayInvoice(string begin, string end, string casher, ref List<FS.FrameWork.Models.NeuObject> list)
          public int GetInpatientPrepayInvoice(string begin, string end, string casher, ref List<FS.HISFC.Models.Fee.Invoice> list)
        {
            string sqlStr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.CheckInvoice.GetInpatientPrepayInvoice", ref sqlStr) == -1)
            {
                #region 默认sql a.prepay_state,
                sqlStr = @"select invoice_no,receipt_no,decode(a.prepay_state,'0','有效','1','作废','2','补打','3','结算召回') 发票状态,
       sum(a.prepay_cost),balance_opercode,fun_get_employee_name(balance_opercode),balance_date,oper_date  from fin_ipb_inprepay a
where a.balance_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
and a.balance_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
and a.balance_state='1'
and (balance_opercode='{2}' or 'ALL'='{2}')
group by a.invoice_no,a.receipt_no,a.balance_opercode,prepay_state,balance_date,oper_date
order by a.invoice_no";

                #endregion
            }
            try
            {
                sqlStr = string.Format(sqlStr, begin, end, casher);
                if (this.ExecQuery(sqlStr) == -1)
                {
                    this.Err = "查找住院发票数据失败！";
                    return -1;
                }
            //    FS.FrameWork.Models.NeuObject obj = null;
                FS.HISFC.Models.Fee.Invoice obj = null;
                while (this.Reader.Read())
                {
                 //   obj = new FS.FrameWork.Models.NeuObject();
                    obj = new FS.HISFC.Models.Fee.Invoice();
                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString();
                    obj.User01 = this.Reader[2].ToString();
                    obj.Memo = this.Reader[3].ToString();
                    //防止其它医院数据库SQL未更新报错
                    if (this.Reader.FieldCount > 4)
                    {
                        obj.User02 = this.Reader[4].ToString();//结算人

                    }
                    obj.User03 = this.Reader[5].ToString();//结算人姓名
                    obj.BeginNO = this.Reader[6].ToString();
                    obj.EndNO = this.Reader[7].ToString();
                    //if (this.Reader.FieldCount > 5)
                    //{
                    //    obj.User03 = this.Reader[5].ToString();//印刷号

                    //}
                    //if (this.Reader.FieldCount > 6)
                    //{
                    //    obj.User04 = this.Reader[6].ToString();
                    //}
                    //if (this.Reader.FieldCount > 7)
                    //{
                    //    obj.User05 = this.Reader[7].ToString();
                    //}
                    list.Add(obj);
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

        }

        /// <summary>
        /// 获取住院发票数据
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">终止时间</param>
        /// <param name="casher">收款员</param>
        /// <param name="list"></param>
        /// <returns></returns>
     //   public int GetInpatientFeeInvoice(string begin, string end, string casher, ref List<FS.FrameWork.Models.NeuObject> list)
        public int GetInpatientFeeInvoice(string begin, string end, string casher, ref List<FS.HISFC.Models.Fee.Invoice> list)
        {
            string sqlStr = string.Empty;
            #region 原来的代码，新增修改的
            //if (this.Sql.GetCommonSql("Fee.CheckInvoice.GetInpatientInvoice", ref sqlStr) == -1)
            //{
            //    this.Err = "查找SQL语句Fee.CheckInvoice.GetInpatientInvoice失败！";
            //    return -1;
            //}
            //try
            //{
            //    sqlStr = string.Format(sqlStr, begin, end, casher);
            //    if (this.ExecQuery(sqlStr) == -1)
            //    {
            //        this.Err = "查找住院发票数据失败！";
            //        return -1;
            //    }
            //    FS.FrameWork.Models.NeuObject obj = null;
            //    while (this.Reader.Read())
            //    {
            //        obj = new FS.FrameWork.Models.NeuObject();
            //        obj.ID = this.Reader[0].ToString();
            //        obj.Name = this.Reader[1].ToString();
            //        obj.User01 = this.Reader[2].ToString();
            //        obj.Memo = this.Reader[3].ToString();
            //        //防止其它医院数据库SQL未更新报错
            //        if (this.Reader.FieldCount > 4)
            //        {
            //            obj.User02 = this.Reader[4].ToString();//结算人
                        
            //        }
            //        if (this.Reader.FieldCount > 5)
            //        {
            //            obj.User03 = this.Reader[5].ToString();//印刷号

            //        }    
            //        list.Add(obj);
            //    }
            //    return 1;
            //}
            //catch (Exception ex)
            //{
            //    this.Err = ex.Message;
            //    return -1;
            //}
            #endregion
            #region 默认sql  max(a.trans_type),
            sqlStr = @"--住院收据
select invoice_no,print_invoiceno, decode(max(a.trans_type),'1','有效','2','结算召回'),
      sum(tot_cost),balance_opercode,fun_get_employee_name(balance_opercode),daybalance_date,balance_date  
       from fin_ipb_balancehead a
where a.balance_date>=to_date('{0}','yyyy-MM-dd hh24:mi:ss')
and a.balance_date<=to_date('{1}','yyyy-MM-dd hh24:mi:ss')
and a.daybalance_flag='1' --and  a.check_flag='0' 
and (DAYBALANCE_OPCD='{2}' or 'ALL'='{2}')
group by invoice_no,print_invoiceno,balance_opercode,daybalance_date,balance_date";
            #endregion
            try
            {
                sqlStr = string.Format(sqlStr, begin, end, casher);
                if (this.ExecQuery(sqlStr) == -1)
                {
                    this.Err = "查找住院收据数据失败";
                    return -1;
                }
              //  FS.FrameWork.Models.NeuObject obj = null;
                FS.HISFC.Models.Fee.Invoice obj=null;
                while (this.Reader.Read())
                {
                 //   obj = new FS.FrameWork.Models.NeuObject();
                    obj=new FS.HISFC.Models.Fee.Invoice();
                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString();
                    obj.User01 = this.Reader[2].ToString();
                    obj.Memo = this.Reader[3].ToString();
                    if (this.Reader.FieldCount > 4)
                    {
                        obj.User02 = this.Reader[4].ToString();
                    }
                    obj.User03 = this.Reader[5].ToString();
                    //if (this.Reader.FieldCount > 5)
                    //{
                    //    obj.User03 = this.Reader[5].ToString();
                    //}
                    obj.BeginNO = this.Reader[6].ToString();
                    obj.EndNO = this.Reader[7].ToString();
                  
                    list.Add(obj);
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

        }

        /// <summary>
        /// 核销门诊发票数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="operTime">操作时间</param>
        /// <param name="oper">操作人</param>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns></returns>
        public int SaveCheckOutPatientFeeInvoice(FS.FrameWork.Models.NeuObject obj, string operTime, string oper, string begin, string end)
        {
            string sqlStr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.CheckInvoice.SaveOutpatientInvoice", ref sqlStr) == -1)
            {
                this.Err = "查找SQL语句Fee.CheckInvoice.SaveOutpatientInvoice失败！";
                return -1;
            }
            try
            {
                sqlStr = string.Format(sqlStr, obj.ID,//发票号

                                            obj.User02,//seq
                                            oper,//操作人

                                            operTime,//操作时间
                                            begin,//开始时间

                                            end);//结束时间

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(sqlStr);
        }

        /// <summary>
        /// 核销住院发票数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="operTime">操作时间</param>
        /// <param name="oper">操作人</param>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns></returns>
        public int SaveCheckInpatientFeeInvoice(FS.FrameWork.Models.NeuObject obj, string operTime, string oper, string begin, string end)
        {
            string sqlStr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.CheckInvoice.SaveInpatientInvoice", ref sqlStr) == -1)
            {
                this.Err = "查找SQL语句Fee.CheckInvoice.SaveOutpatientInvoice失败！";
                return -1;
            }
            try
            {
                sqlStr = string.Format(sqlStr, obj.ID,//发票号

                                            oper,//操作人

                                            operTime,//操作时间
                                            begin,//开始时间

                                            end);//结束时间
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(sqlStr);
        }
        #endregion

        #endregion


        #region 发票扩展表


        /// <summary>
        /// 获得发票扩展信息
        /// </summary>
        /// <param name="strSQL">查询SQL语句</param>
        /// <returns></returns>
        private List<FS.HISFC.Models.Fee.InvoiceExtend> GetInvoiceExtend(string strSQL)
        {
            List<FS.HISFC.Models.Fee.InvoiceExtend> al = new List<InvoiceExtend>();
            FS.HISFC.Models.Fee.InvoiceExtend invoiceExtend = null;
            this.ExecQuery(strSQL);
            while (this.Reader.Read())
            {
                invoiceExtend = new FS.HISFC.Models.Fee.InvoiceExtend();
                try
                {
                    invoiceExtend.ID = this.Reader[0].ToString();
                    invoiceExtend.InvoiceType = this.Reader[1].ToString();
                    invoiceExtend.RealInvoiceNo = this.Reader[2].ToString();
                    invoiceExtend.InvvoiceHead = this.Reader[3].ToString();
                    invoiceExtend.InvoiceState = this.Reader[4].ToString();
                    invoiceExtend.WasteOper.ID = this.Reader[5].ToString();
                    invoiceExtend.WasteOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6]);
                    invoiceExtend.Oper.ID = this.Reader[7].ToString();
                    invoiceExtend.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8]);
                }

                catch (Exception ex)
                {
                    this.Err = "查询发票扩展赋值错误" + ex.Message;
                    this.ErrCode = ex.Message;
                    this.WriteErr();
                    if (!this.Reader.IsClosed)
                    {
                        this.Reader.Close();
                    }
                    return null;
                }

                al.Add(invoiceExtend);
            }
            this.Reader.Close();
            return al;
        }

        /// <summary>
        /// 获得发票扩展参数
        /// </summary>
        /// <param name="obj">发票扩展信息</param>
        /// <returns>null错误 </returns>
        protected string[] GetParmInvoiceExtend(FS.HISFC.Models.Fee.InvoiceExtend obj)
        {
            string[] strParm = { obj.ID, //发票流水号
                                   obj.InvoiceType,//发票类型
                                    obj.RealInvoiceNo,//实际发票号
                                   obj.InvvoiceHead,//发票号字母头
                                   "1", //插入时都是有效发票
                                   this.Operator.ID
							   };

            return strParm;

        }
        
        /// <summary>
        /// 插入扩展发票信息
        /// </summary>
        /// <param name="obj">发票扩展信息</param>
        /// <returns></returns>
        public int InsertInvoiceExtend(FS.HISFC.Models.Fee.InvoiceExtend obj)
        {
            return this.UpdateSingleTable("Fee.OutPatient.InsertInvoiceExtend.Insert", this.GetParmInvoiceExtend(obj));           
        }

        /// <summary>
        /// 查询发票扩展信息
        /// </summary>
        /// <param name="invoiceNo">发票号</param>
        /// <param name="invoiceType">发票类型</param>
        /// <returns> 发票扩展信息集合</returns>
        public FS.HISFC.Models.Fee.InvoiceExtend GetInvoiceExtend(string invoiceNo, string invoiceType)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Fee.OutPatient.QueryInvoiceExtend.Select.1", ref strSql) == -1)
            {
                this.Err += "获得SQL语句出错" + "索引: Fee.OutPatient.QueryInvoiceExtend.Select.1";
                return null;
            }
            try
            {
                strSql = string.Format(strSql, invoiceNo, invoiceType);
            }
            catch (Exception ex)
            {
                this.Err += "参数付值出错!" + ex.Message;
                return null;
            }

            return GetInvoiceExtend(strSql)[0];
        }

        /// <summary>
        /// 更新发票状态
        /// </summary>
        /// <param name="invoiceNO">发票号</param>
        /// <param name="invoiceType">发票类型</param>
        /// <param name="wasteFlag">作废标记，0退费，2注销</param>
        /// <returns></returns>
        public int UpdateInvoiceExtendWaste(string invoiceNO, string invoiceType, string wasteFlag)
        {
            return UpdateSingleTable("Fee.InvoiceExtend.Update.1", invoiceNO, invoiceType, wasteFlag, this.Operator.ID);
            
        }

        #endregion

        #region 更新电脑发票号 常数类型的

        /// <summary>
        /// 更新电脑发票号 常数类型(调用前判断发票是否已使用)
        /// </summary>
        /// <param name="emplCode"></param>
        /// <param name="invoiceType"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public int UpdateNextInvoliceNo(string emplCode, string invoiceType, string invoiceNo)
        {
            if (string.IsNullOrEmpty(emplCode) || string.IsNullOrEmpty(invoiceType) || string.IsNullOrEmpty(invoiceNo))
            {
                this.Err = "参数错误！";
                return -1;
            }

            string strSql = "";
            if (this.Sql.GetCommonSql("Fee.OutPatient.UpdateNextInvoiceNo", ref strSql) == -1)
            {
                this.Err += "获得SQL语句出错" + "索引: Fee.OutPatient.UpdateNextInvoiceNo";
                return -1;
            }

            int rev = this.ExecNoQuery(strSql, emplCode, invoiceType, invoiceNo);
            if (rev == 0)
            {
                this.Err = "更新发票号失败，未找到对应的发票信息！";
                return -1;
            }
            else if (rev == -1)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 获取电脑发票号最近的更新时间（常数类型）
        /// </summary>
        /// <param name="emplCode"></param>
        /// <param name="invoiceType"></param>
        /// <param name="recentUpdate">最近更新时间</param>
        /// <returns></returns>
        public int GetRecentUpdateInvoiceTime(string emplCode, string invoiceType, ref DateTime recentUpdate)
        {
            if (string.IsNullOrEmpty(emplCode) || string.IsNullOrEmpty(invoiceType))
            {
                this.Err = "参数错误！";
                return -1;
            }

            try
            {
                string strSql = "";
                if (this.Sql.GetCommonSql("Fee.OutPatient.GetInvoiceNo.UpdateTime", ref strSql) == -1)
                {
                    this.Err += "获得SQL语句出错" + "索引: Fee.OutPatient.GetInvoiceNo.UpdateTime";
                    return -1;
                }

                strSql = string.Format(strSql, emplCode, invoiceType);

                string date = this.ExecSqlReturnOne(strSql, "");
                if (string.IsNullOrEmpty(date))
                {
                    return -1;
                }
                recentUpdate = FS.FrameWork.Function.NConvert.ToDateTime(date);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return -1;
            }

            return 1;
        }

        #endregion

        #region 新增实际发票号处理 {9B4EC21B-4318-4bb7-B15C-AABF3201655F}
        /// <summary>
        /// 更新实际发票号
        /// </summary>
        /// <param name="strEmpCode"></param>
        /// <param name="invoiceType">发票类型</param>
        /// <param name="realInvoiceNo">下一发票号</param>
        /// <returns></returns>
        public int UpdateRealInvoiceNo(string strEmpCode, string invoiceType, string realInvoiceNo)
        {
            if (string.IsNullOrEmpty(strEmpCode) || string.IsNullOrEmpty(invoiceType) || string.IsNullOrEmpty(realInvoiceNo))
            {
                this.Err = "参数错误！";
                return -1;
            }

            ArrayList arlInvoice = QueryInvoiceByRealInvoiceNo(strEmpCode, invoiceType, realInvoiceNo);
            if (arlInvoice == null || arlInvoice.Count <= 0)
            {
                this.Err = "指定下一发票号，不在领用发票号段内！";
                return 0;
            }

            Invoice invoice = arlInvoice[0] as Invoice;
            if (invoice == null)
            {
                this.Err = "指定下一发票号，不在领用发票号段内！";
                return 0;
            }

            long lngRealInvoice = 0;
            string strRealInvoice = FS.FrameWork.Public.String.AddNumber(realInvoiceNo, -1);
            //long.TryParse(realInvoiceNo, out lngRealInvoice);
            lngRealInvoice = lngRealInvoice - 1;
            if (lngRealInvoice < 0)
            {
                lngRealInvoice = 0;
            }
            //string strUseNo = strRealInvoice.PadLeft(12, '0');
            string strUseNo = strRealInvoice;
            return this.UpdateInvoiceUsedNO(strUseNo, invoice.AcceptOper.ID, invoice.AcceptTime);
        }
        /// <summary>
        /// 查询实际发票号所在发票段
        /// </summary>
        /// <param name="strEmpCode"></param>
        /// <param name="invoiceType"></param>
        /// <param name="realInvoiceNo"></param>
        /// <returns></returns>
        public ArrayList QueryInvoiceByRealInvoiceNo(string strEmpCode, string invoiceType, string realInvoiceNo)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Fee.OutPatient.QueryInvoiceByRealInvoiceNo", ref strSql) == -1)
            {
                this.Err += "获得SQL语句出错" + "索引: Fee.OutPatient.QueryInvoiceByRealInvoiceNo";
                return null;
            }

            return QueryInvoicesBySql(strSql, invoiceType, strEmpCode, realInvoiceNo);
        }

        #endregion
    }
}
