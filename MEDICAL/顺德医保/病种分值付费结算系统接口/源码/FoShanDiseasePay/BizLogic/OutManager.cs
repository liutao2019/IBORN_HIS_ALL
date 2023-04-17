using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using FS.FrameWork.Function;
using FS.HISFC.Models;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Registration;
using FS.HISFC.Models.Fee.Outpatient;
using FS.HISFC.Models.Pharmacy;
using FS.HISFC.Models.Account;

namespace FoShanDiseasePay.BizLogic
{
    /// <summary>
    /// 门诊业务管理类
    /// </summary>
    public class OutManager : FS.HISFC.BizLogic.Fee.Outpatient
    {
        #region 费用明细

        /// <summary>
        /// 通过SQL语句获得费用明细信息
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">SQL参数</param>
        /// <returns>成功:费用明细集合 失败: null 没有查找到数据: 元素数为0的ArrayList</returns>
        private ArrayList QueryFeeDetailBySql(string sql, params string[] args)
        {
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            ArrayList feeItemLists = new ArrayList();//费用明细数组
            FeeItemList feeItemList = null;//费用明细实体

            try
            {
                //循环读取数据
                while (this.Reader.Read())
                {
                    feeItemList = new FeeItemList();

                    //feeItemList.Item.IsPharmacy = NConvert.ToBoolean(this.Reader[11].ToString());

                    feeItemList.Item.ItemType = (EnumItemType)NConvert.ToInt32(this.Reader[11]);

                    //if (feeItemList.Item.IsPharmacy)
                    if (feeItemList.Item.ItemType == EnumItemType.Drug)
                    {
                        feeItemList.Item = new FS.HISFC.Models.Pharmacy.Item();
                        feeItemList.Item.ItemType = EnumItemType.Drug;
                        //feeItemList.Item.IsPharmacy = true;
                    }
                    //{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                    else if (feeItemList.Item.ItemType == EnumItemType.UnDrug)
                    {
                        feeItemList.Item = new FS.HISFC.Models.Fee.Item.Undrug();
                        //feeItemList.Item.IsPharmacy = false;
                        feeItemList.Item.ItemType = EnumItemType.UnDrug;
                    }
                    //物资 {40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                    else
                    {
                        feeItemList.Item = new FS.HISFC.Models.FeeStuff.MaterialItem();
                        feeItemList.Item.ItemType = EnumItemType.MatItem;

                    }

                    feeItemList.RecipeNO = this.Reader[0].ToString();
                    feeItemList.SequenceNO = NConvert.ToInt32(this.Reader[1].ToString());
                    if (this.Reader[2].ToString() == "1")
                    {
                        feeItemList.TransType = TransTypes.Positive;
                    }
                    else
                    {
                        feeItemList.TransType = TransTypes.Negative;
                    }
                    feeItemList.Patient.ID = this.Reader[3].ToString();
                    feeItemList.Patient.PID.CardNO = this.Reader[4].ToString();
                    ((Register)feeItemList.Patient).DoctorInfo.SeeDate = NConvert.ToDateTime(this.Reader[5].ToString());
                    ((Register)feeItemList.Patient).DoctorInfo.Templet.Dept.ID = this.Reader[6].ToString();
                    feeItemList.RecipeOper.ID = this.Reader[7].ToString();
                    ((Register)feeItemList.Patient).DoctorInfo.Templet.Doct.ID = this.Reader[7].ToString();
                    feeItemList.RecipeOper.Dept.ID = this.Reader[8].ToString();
                    feeItemList.Item.ID = this.Reader[9].ToString();
                    feeItemList.Item.Name = this.Reader[10].ToString();
                    feeItemList.Item.Specs = this.Reader[12].ToString();

                    //if (feeItemList.Item.IsPharmacy)
                    if (feeItemList.Item.ItemType == EnumItemType.Drug)
                    {
                        ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).Product.IsSelfMade = NConvert.ToBoolean(this.Reader[13].ToString());
                        ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).Quality.ID = this.Reader[14].ToString();
                        ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).DosageForm.ID = this.Reader[15].ToString();
                    }
                    feeItemList.Item.MinFee.ID = this.Reader[16].ToString();
                    feeItemList.Item.SysClass.ID = this.Reader[17].ToString();
                    feeItemList.Item.Price = NConvert.ToDecimal(this.Reader[18].ToString());
                    feeItemList.Item.Qty = NConvert.ToDecimal(this.Reader[19].ToString());
                    feeItemList.Days = NConvert.ToDecimal(this.Reader[20].ToString());
                    feeItemList.Order.Frequency.ID = this.Reader[21].ToString();
                    feeItemList.Order.Usage.ID = this.Reader[22].ToString();
                    feeItemList.Order.Usage.Name = this.Reader[23].ToString();
                    feeItemList.InjectCount = NConvert.ToInt32(this.Reader[24].ToString());
                    feeItemList.IsUrgent = NConvert.ToBoolean(this.Reader[25].ToString());
                    feeItemList.Order.Sample.ID = this.Reader[26].ToString();
                    feeItemList.Order.CheckPartRecord = this.Reader[27].ToString();
                    feeItemList.Order.DoseOnce = NConvert.ToDecimal(this.Reader[28].ToString());
                    feeItemList.Order.DoseUnit = this.Reader[29].ToString();
                    //if (feeItemList.Item.IsPharmacy)
                    if (feeItemList.Item.ItemType == EnumItemType.Drug)
                    {
                        ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).BaseDose = NConvert.ToDecimal(this.Reader[30].ToString());
                    }
                    feeItemList.Item.PackQty = NConvert.ToDecimal(this.Reader[31].ToString());
                    feeItemList.Item.PriceUnit = this.Reader[32].ToString();
                    feeItemList.FT.PubCost = NConvert.ToDecimal(this.Reader[33].ToString());
                    feeItemList.FT.PayCost = NConvert.ToDecimal(this.Reader[34].ToString());
                    feeItemList.FT.OwnCost = NConvert.ToDecimal(this.Reader[35].ToString());
                    feeItemList.ExecOper.Dept.ID = this.Reader[36].ToString();
                    feeItemList.ExecOper.Dept.Name = this.Reader[37].ToString();
                    feeItemList.Compare.CenterItem.ID = this.Reader[38].ToString();
                    feeItemList.Compare.CenterItem.ItemGrade = this.Reader[39].ToString();
                    feeItemList.Order.Combo.IsMainDrug = NConvert.ToBoolean(this.Reader[40].ToString());
                    feeItemList.Order.Combo.ID = this.Reader[41].ToString();
                    feeItemList.ChargeOper.ID = this.Reader[42].ToString();
                    feeItemList.ChargeOper.OperTime = NConvert.ToDateTime(this.Reader[43].ToString());
                    feeItemList.PayType = (PayTypes)(NConvert.ToInt32(this.Reader[44].ToString()));
                    feeItemList.CancelType = (CancelTypes)(NConvert.ToInt32(this.Reader[45].ToString()));
                    feeItemList.FeeOper.ID = this.Reader[46].ToString();
                    feeItemList.FeeOper.OperTime = NConvert.ToDateTime(this.Reader[47].ToString());
                    feeItemList.Invoice.ID = this.Reader[48].ToString();
                    feeItemList.Invoice.Type.ID = this.Reader[49].ToString();
                    feeItemList.IsConfirmed = NConvert.ToBoolean(this.Reader[51].ToString());
                    feeItemList.ConfirmOper.ID = this.Reader[52].ToString();
                    feeItemList.ConfirmOper.Dept.ID = this.Reader[53].ToString();
                    feeItemList.ConfirmOper.OperTime = NConvert.ToDateTime(this.Reader[54].ToString());
                    feeItemList.InvoiceCombNO = this.Reader[55].ToString();
                    feeItemList.NewItemRate = NConvert.ToDecimal(this.Reader[56].ToString());
                    feeItemList.OrgItemRate = NConvert.ToDecimal(this.Reader[57].ToString());
                    feeItemList.ItemRateFlag = this.Reader[58].ToString();
                    feeItemList.Item.SpecialFlag1 = this.Reader[59].ToString();
                    feeItemList.Item.SpecialFlag2 = this.Reader[60].ToString();
                    feeItemList.FeePack = this.Reader[61].ToString();
                    feeItemList.UndrugComb.ID = this.Reader[62].ToString();
                    feeItemList.UndrugComb.Name = this.Reader[63].ToString();
                    feeItemList.NoBackQty = NConvert.ToDecimal(this.Reader[64].ToString());
                    feeItemList.ConfirmedQty = NConvert.ToDecimal(this.Reader[65].ToString());
                    feeItemList.ConfirmedInjectCount = NConvert.ToInt32(this.Reader[66].ToString());
                    feeItemList.Order.ID = this.Reader[67].ToString();
                    feeItemList.RecipeSequence = this.Reader[68].ToString();
                    feeItemList.FT.RebateCost = NConvert.ToDecimal(this.Reader[69].ToString());
                    feeItemList.SpecialPrice = NConvert.ToDecimal(this.Reader[70].ToString());
                    feeItemList.FT.ExcessCost = NConvert.ToDecimal(this.Reader[71].ToString());
                    feeItemList.FT.DrugOwnCost = NConvert.ToDecimal(this.Reader[72].ToString());
                    feeItemList.FTSource = this.Reader[73].ToString();
                    feeItemList.Item.IsMaterial = NConvert.ToBoolean(this.Reader[74].ToString());
                    feeItemList.IsAccounted = NConvert.ToBoolean(this.Reader[75].ToString());
                    //{143CA424-7AF9-493a-8601-2F7B1D635026}
                    //物资出库流水号
                    feeItemList.UpdateSequence = NConvert.ToInt32(this.Reader[76].ToString());

                    //判断77（结算类别）是否存在
                    if (this.Reader.FieldCount > 77)
                    {
                        feeItemList.Order.Patient.Pact.PayKind.ID = this.Reader[77].ToString();
                    }
                    //判断78（合同单位）是否存在
                    if (this.Reader.FieldCount > 78)
                    {
                        feeItemList.Order.Patient.Pact.ID = this.Reader[78].ToString();
                    }
                    //基药标志
                    if (this.Reader.FieldCount > 79)
                    {
                        if (feeItemList.Item.ItemType == EnumItemType.Drug)
                        {
                            (feeItemList.Item as FS.HISFC.Models.Pharmacy.Item).ExtendData2 = this.Reader[79].ToString();

                            //feeItemList.Item.User01 = this.Reader[79].ToString();
                        }
                    }

                    feeItemLists.Add(feeItemList);
                }//循环结束

                this.Reader.Close();

                return feeItemLists;
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
        }

        /// <summary>
        /// 获得处方明细的sql语句
        /// </summary>
        /// <returns>返回查询费用明细SQL语句</returns>
        private string GetSqlFeeDetail()
        {
            string sql = string.Empty;//查询SQL语句的SELECT部分

            if (this.Sql.GetSql("Fee.Item.GetFeeItem", ref sql) == -1)
            {
                this.Err = "没有找到索引为Fee.Item.GetFeeItem的SQL语句";

                return null;
            }

            return sql;
        }

        /// <summary>
        /// 根据发票号查询费用明细信息
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="invoiceNO"></param>
        /// <returns>成功:费用明细 失败:null 没有数据:返回元素数为0的ArrayList</returns>
        public ArrayList QueryFeeItemByInvoiceNO(string clinicCode, string invoiceNO)
        {
            //SELECT语句
            string sql = this.GetSqlFeeDetail();
            if (string.IsNullOrEmpty(sql))
            {
                return null;
            }

            //WHERE语句
            string where = @"
                            where CLINIC_CODE = '{0}' AND INVOICE_NO = '{1}' order by mo_order";

            return this.QueryFeeDetailBySql(sql + " " + where, clinicCode, invoiceNO);
        }

        #endregion

        #region 发票信息

        /// <summary>
        /// 通过SQL语句获得结算信息数组
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns>成功:结算信息信息数组 失败:null 没有查找到数据返回元素数为0的ArrayList</returns>
        private ArrayList QueryBalancesBySql(string sql, params string[] args)
        {
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            ArrayList balances = new ArrayList();//结算信息实体数组
            Balance balance = null;//结算信息实体

            try
            {
                //循环读取数据
                while (this.Reader.Read())
                {
                    balance = new Balance();

                    balance.Invoice.ID = this.Reader[0].ToString();//0发票号
                    balance.TransType = (TransTypes)NConvert.ToInt32(this.Reader[1].ToString());//交易类型,1正交易，2反交易
                    balance.Patient.PID.CardNO = this.Reader[2].ToString();//2病历卡号
                    ((Register)balance.Patient).DoctorInfo.SeeDate = NConvert.ToDateTime(this.Reader[3].ToString());//3挂号日期
                    balance.Patient.Name = this.Reader[4].ToString();//	4患者姓名
                    balance.Patient.Pact.PayKind.ID = this.Reader[5].ToString();//5结算类别代码
                    balance.Patient.Pact.ID = this.Reader[6].ToString();//6合同单位代码
                    balance.Patient.Pact.Name = this.Reader[7].ToString();//7合同单位名称
                    balance.Patient.SSN = this.Reader[8].ToString();//8个人编号
                    balance.FT.TotCost = NConvert.ToDecimal(this.Reader[10].ToString());//10总额
                    balance.FT.PubCost = NConvert.ToDecimal(this.Reader[11].ToString());//11可报效金额
                    balance.FT.OwnCost = NConvert.ToDecimal(this.Reader[12].ToString());//12不可报效金额
                    balance.FT.PayCost = NConvert.ToDecimal(this.Reader[13].ToString());//13自付金额
                    balance.User01 = this.Reader[14].ToString();//14预留1
                    balance.FT.RebateCost = NConvert.ToDecimal(this.Reader[14]);
                    balance.User02 = this.Reader[15].ToString();//15预留2
                    balance.User03 = this.Reader[16].ToString();//16预留3
                    balance.FT.BalancedCost = NConvert.ToDecimal(this.Reader[17].ToString());//17实付金额
                    balance.BalanceOper.ID = this.Reader[18].ToString();//18结算人
                    balance.BalanceOper.OperTime = NConvert.ToDateTime(this.Reader[19].ToString());//19结算时间
                    balance.ExamineFlag = this.Reader[20].ToString();//0不是体检/1个人体检/2团体体检 
                    balance.CancelType = (CancelTypes)NConvert.ToInt32(this.Reader[21].ToString());
                    balance.CanceledInvoiceNO = this.Reader[22].ToString();//22作废票据号
                    balance.CancelOper.ID = this.Reader[23].ToString();//23作废操作员
                    balance.CancelOper.OperTime = NConvert.ToDateTime(this.Reader[24].ToString());//24作废时间
                    balance.IsAuditing = NConvert.ToBoolean(this.Reader[25].ToString());//是否核查
                    balance.AuditingOper.ID = this.Reader[26].ToString();//		26核查人
                    balance.AuditingOper.OperTime = NConvert.ToDateTime(this.Reader[27].ToString());//27核查时间
                    balance.IsDayBalanced = NConvert.ToBoolean(this.Reader[28].ToString());//28是否日结
                    balance.BalanceID = this.Reader[29].ToString();//29	日结标识号
                    balance.DayBalanceOper.ID = this.Reader[30].ToString();//30日结人
                    balance.DayBalanceOper.OperTime = NConvert.ToDateTime(this.Reader[31].ToString());//31日结时间0
                    balance.CombNO = this.Reader[32].ToString();
                    balance.InvoiceType.ID = this.Reader[33].ToString();
                    balance.Patient.ID = this.Reader[34].ToString();
                    balance.PrintedInvoiceNO = this.Reader[35].ToString();
                    balance.DrugWindowsNO = this.Reader[36].ToString();
                    //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                    balance.IsAccount = NConvert.ToBoolean(this.Reader[37]);

                    if (this.Reader.FieldCount > 38)
                    {
                        balance.Invoice.User03 = this.Reader[38].ToString();  //38 同一发票号 {0384001D-318D-4b9a-BF99-005808D331C3}
                    }

                    balances.Add(balance);
                }//循环结束

                this.Reader.Close();

                return balances;
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
        }

        /// <summary>
        /// 根据发票号查询门诊结算信息
        /// </summary>
        /// <param name="invoiceNO"></param>
        /// <returns>成功:结算信息 失败:null 没有数据:返回元素数为0的ArrayList</returns>
        public ArrayList QueryBalances(string invoiceNO)
        {
            //SELECT语句
            string sql = this.GetBalanceSelectSql();
            if (string.IsNullOrEmpty(sql))
            {
                return null;
            }

            //WHERE语句
            string where = @"
                            Where   INVOICE_NO='{0}'";

            return this.QueryBalancesBySql(sql + " " + where, invoiceNO);
        }

        #endregion

        #region 社保结算信息

        /// <summary>
        /// 获取社保信息 
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="patientType">结算分类1-门诊2-住院</param>
        /// <returns></returns>
        public string GetSiInfo(string clinicCode, string invoiceNO,string patientType)
        {
            string strSql = @"SELECT t.REMARK FROM FIN_IPR_SIINMAININFO t 
                                WHERE t.INPATIENT_NO = '{0}' 
                                AND t.INVOICE_NO like '%{1}%' 
                                AND t.TYPE_CODE = '{2}'
                                and t.valid_flag = '1'";
            strSql = string.Format(strSql, clinicCode, invoiceNO, patientType);
            return this.ExecSqlReturnOne(strSql);
        }
        /// <summary>
        /// 获取结算时间// {E1865220-C459-45ca-966E-F111E0A6B560}
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="patientType"></param>
        /// <returns></returns>
        public void GetFeeInfo(string clinicCode, string invoiceNO, string patientType,
            ref string dateTime, ref string totCost, ref string pubCost, ref string ownCost, ref string jydjh)
        {
            string strSql = @"SELECT to_char(t.oper_date, 'yyyy-mm-dd') dateTime,
                                   t.tot_cost,
                                   t.pub_cost,
                                   t.own_cost,
                                   t.reg_no
                              FROM FIN_IPR_SIINMAININFO t
                             WHERE t.INPATIENT_NO = '{0}'
                               AND t.INVOICE_NO like '%{1}%'
                               AND t.TYPE_CODE = '{2}'
                               and t.valid_flag = '1'
                            ";
            strSql = string.Format(strSql, clinicCode, invoiceNO, patientType);
            System.Data.DataSet dsResult = null;
            if (this.ExecQuery(strSql, ref dsResult) == -1)
            {
                return;
            }

            dateTime = "";
            totCost = "";
            pubCost = "";
            ownCost = "";
            jydjh = "";
            DataTable dtResult = dsResult.Tables[0];
            foreach (DataRow dr in dtResult.Rows)
            {
                dateTime = dr["dateTime"].ToString();
                totCost = dr["tot_cost"].ToString();
                pubCost = dr["pub_cost"].ToString();
                ownCost = dr["own_cost"].ToString();
                jydjh = dr["reg_no"].ToString();
            }
        }
        /// <summary>
        /// 获取上传标志// {E1865220-C459-45ca-966E-F111E0A6B560}
        /// </summary>
        /// <param name="upType"></param>
        /// <param name="clinicCode"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="patientType"></param>
        /// <returns></returns>
        public string GetUpCount(string clinicCode, string invoiceNO, string patientType)
        {
            string strSql = @"select upload_type from FS_UPLOAD_DISEASEPAY y 
                                where y.patient_type = '{0}' 
                                and y.clinic_code = '{1}' 
                                and y.invoice_no = '{2}'";
            strSql = string.Format(strSql, patientType, clinicCode,  invoiceNO);
            return this.ExecSqlReturnOne(strSql);
        }
        /// <summary>
        /// 获取项目对照信息
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public DataTable GetItemInfo(string itemCode)
        {
            ArrayList al = new ArrayList();
            string strSql = @"SELECT a.item_code 项目编码,
                                   a.input_code 自定义码,
                                   a.gb_code 国家编码,
                                   t.CENTER_CODE 社保项目编码,
                                   a.item_name 项目名称,
                                   a.specs 规格,
                                   decode(substr(to_char(a.unit_price,'99999999.9999'),9,1),' ',lpad('0'|| substr(to_char(a.unit_price,'99999999.9999'),10),14,' '),to_char(a.unit_price,'99999999.9999')) 基准价格,
                                   a.STOCK_UNIT 单位,
                                   '' 批准文号,
                                   '' 生产厂家
                              FROM FIN_COM_COMPARE t, fin_com_undruginfo a
                             WHERE t.PACT_CODE = '99'
                               and t.his_code = a.input_code
                               and (a.item_code = '{0}' or '{0}' = 'ALL')

                            union all

                            SELECT b.drug_code 项目编码,
                                   b.custom_code 自定义码,
                                   b.gb_code 国家编码,
                                   t.CENTER_CODE 社保项目编码,
                                   b.trade_name 项目名称,
                                   b.specs 规格,
                                   decode(substr(to_char(b.retail_price,'99999999.9999'),9,1),' ',lpad('0'|| substr(to_char(b.retail_price,'99999999.9999'),10),14,' '),to_char(b.retail_price,'99999999.9999')) 基准价格,
                                   b.pack_unit 单位,
                                   b.APPROVE_INFO 批准文号,
                                   (select y.fac_name
                                      from pha_com_company y
                                     where y.fac_code = b.PRODUCER_CODE) 生产厂家
                              FROM FIN_COM_COMPARE t, pha_com_baseinfo b
                             WHERE t.PACT_CODE = '99'
                               and t.his_code = b.custom_code
                               and (b.drug_code = '{0}' or '{0}' = 'ALL')
                            ";
            strSql = string.Format(strSql,itemCode);

            DataSet dsSet = null;
            if (this.ExecQuery(strSql, ref dsSet) == -1)
            {
                return null;
            }
            return dsSet.Tables[0];

        }
        /// <summary>
        /// 获取医保结算金额
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="patientType">结算分类1-门诊2-住院</param>
        /// <param name="totCost"></param>
        /// <param name="pubCost"></param>
        /// <param name="ownCost"></param>
        /// <returns></returns>
        public int GetSiCost(string clinicCode, string invoiceNO, string patientType, out decimal totCost, out decimal pubCost, out decimal ownCost)
        {
            totCost = 0;
            pubCost = 0;
            ownCost = 0;
            string strSql = @"SELECT t.TOT_COST,t.PUB_COST,t.OWN_COST
                                FROM FIN_IPR_SIINMAININFO t
                                WHERE t.INPATIENT_NO = '{0}'
                                AND t.INVOICE_NO = '{1}' 
                                AND t.TYPE_CODE = '{2}'
                                and t.valid_flag = '1'";
            strSql = string.Format(strSql, clinicCode, invoiceNO, patientType);
            DataSet dsSet = null;
            if (this.ExecQuery(strSql, ref dsSet) == -1)
            {
                return -1;
            }
            if (dsSet == null)
            {
                return -1;
            }
            if (dsSet.Tables.Count > 0 && dsSet.Tables[0] != null && dsSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dRow in dsSet.Tables[0].Rows)
                {
                    totCost = NConvert.ToDecimal(dRow[0].ToString());
                    pubCost = NConvert.ToDecimal(dRow[1].ToString());
                    ownCost = NConvert.ToDecimal(dRow[2].ToString());
                }
            }

            return 1;
        }
        /// <summary>
        /// 门诊重打发票更新发票流水号
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="newInvoiceNo"></param>
        /// <returns></returns>
        public int UpdateSiInvoice(string clinicCode,string newInvoiceNo)
        {
            string sql = @"update FIN_IPR_SIINMAININFO t
                            set invoice_no = '{1}'
                            where TYPE_CODE = '1'
                            --and valid_flag = '1'
                            and t.inpatient_no = '{0}'";
            sql = string.Format(sql, clinicCode, newInvoiceNo);

            return this.ExecNoQuery(sql);
        }
        #endregion

        #region 挂号信息 【张槎、朝阳、澜石不适用】

        /// <summary>
        /// 查询卡费用信息
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="lstCardFee"></param>
        /// <returns></returns>
        private int QueryAccountCardFeeSQL(string sql, out List<AccountCardFee> lstCardFee)
        {
            lstCardFee = null;
            try
            {
                if (this.ExecQuery(sql) == -1)
                {
                    return -1;
                }

                lstCardFee = new List<AccountCardFee>();
                AccountCardFee cardFee = null;
                while (this.Reader.Read())
                {
                    cardFee = new AccountCardFee();
                    cardFee.InvoiceNo = this.Reader[0].ToString().Trim();
                    cardFee.TransType = this.Reader[1].ToString().Trim() == "1" ? TransTypes.Positive : TransTypes.Negative;
                    cardFee.MarkNO = this.Reader[2].ToString().Trim();
                    cardFee.MarkType.ID = this.Reader[3].ToString().Trim();
                    cardFee.Tot_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4].ToString());
                    cardFee.FeeOper.Oper.ID = this.Reader[5].ToString().Trim();
                    cardFee.FeeOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6]);
                    cardFee.Oper.Oper.ID = this.Reader[7].ToString().Trim();
                    cardFee.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8]);
                    cardFee.IsBalance = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[9].ToString());
                    cardFee.BalanceNo = this.Reader[10].ToString().Trim();
                    cardFee.BalnaceOper.Oper.ID = this.Reader[11].ToString().Trim();
                    cardFee.BalnaceOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[12]);
                    cardFee.IStatus = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[13]);
                    cardFee.CardNo = this.Reader[14].ToString().Trim();

                    cardFee.Print_InvoiceNo = this.Reader[15].ToString().Trim();
                    switch (this.Reader[16].ToString().Trim())
                    {
                        case "1":
                            cardFee.FeeType = AccCardFeeType.CardFee;
                            break;
                        case "2":
                            cardFee.FeeType = AccCardFeeType.CaseFee;
                            break;
                        case "3":
                            cardFee.FeeType = AccCardFeeType.RegFee;
                            break;
                        case "4":
                            cardFee.FeeType = AccCardFeeType.DiaFee;
                            break;
                        case "5":
                            cardFee.FeeType = AccCardFeeType.ChkFee;
                            break;
                        case "6":
                            cardFee.FeeType = AccCardFeeType.AirConFee;
                            break;
                        case "7":
                            cardFee.FeeType = AccCardFeeType.OthFee;
                            break;
                    }
                    cardFee.ClinicNO = this.Reader[17].ToString().Trim();
                    cardFee.Remark = this.Reader[18].ToString().Trim();

                    cardFee.Own_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20].ToString());
                    cardFee.Pub_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[21].ToString());
                    cardFee.Pay_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[22].ToString());

                    cardFee.Oper.Oper.Name = this.Reader[23].ToString().Trim();
                    cardFee.MarkType.Name = this.Reader[24].ToString().Trim();

                    // {6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造
                    if (this.Reader.FieldCount > 25)
                    {
                        cardFee.ItemCode = this.Reader[25].ToString();
                        cardFee.ItemName = this.Reader[26].ToString();
                        cardFee.ItemQty = NConvert.ToInt32(this.Reader[27].ToString());
                        cardFee.ItemUnit = this.Reader[28].ToString();
                        cardFee.ItemPrice = NConvert.ToDecimal(this.Reader[29].ToString());
                        cardFee.SiFlag = this.Reader[30].ToString();
                        cardFee.SiBalanceDate = NConvert.ToDateTime(this.Reader[31].ToString());
                        cardFee.SiBalanceNO = this.Reader[32].ToString();
                        cardFee.Eco_cost = NConvert.ToDecimal(this.Reader[33].ToString());
                    }

                    lstCardFee.Add(cardFee);
                }

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 查询卡费用信息 -- 指定挂号记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="clinicNo"></param>
        /// <param name="lstCardFee"></param>
        /// <returns></returns>
        public int QueryAccCardFeeByClinic(string cardNo, string clinicNo, string mzInvoiceNo, out List<AccountCardFee> lstCardFee)
        {
            lstCardFee = null;
            if (string.IsNullOrEmpty(cardNo) || string.IsNullOrEmpty(clinicNo) || string.IsNullOrEmpty(mzInvoiceNo))
            {
                this.Err = "参数不对！";
                return -1;
            }

            #region SQL语句

            string strSql = @"select a.invoice_no,
                                a.trans_type,
                                a.markno,
                                a.type,
                                a.tot_cost,
                                a.fee_oper,
                                a.fee_date,
                                a.oper_code,
                                a.oper_date,
                                a.balance_flag,
                                a.balance_no,
                                a.balance_opcd,
                                a.balance_date,
                                a.cancel_flag,
                                a.card_no,
                                a.print_invoiceno,
                                a.fee_type,
                                a.clinic_no,
                                a.remark,
                                a.pay_type,
                                a.own_cost,
                                a.pub_cost,
                                a.pay_cost,
                                b.empl_name,
                                (select c.name
                                  from com_dictionary c
                                 where c.type = 'MarkType'
                                   and c.code = a.type) cardtypename,
                                a.ITEM_CODE,
                                a.ITEM_NAME,
                                a.QTY,
                                a.UNIT,
                                a.PRICE,
                                a.SI_FLAG,
                                a.SI_BALANCEDATE,
                                a.SI_BALANCENO,
                                a.ECO_COST
                                from fin_opb_accountcardfee a, com_employee b

                                where a.cancel_flag = 1
                                and a.oper_code = b.empl_code
                                and a.card_no = '{0}'
                                and a.clinic_no = '{1}'
                                and a.tot_cost > 0
                                --AND a.SI_BALANCENO = '{2}'
                                order by a.invoice_no";

            #endregion

            int iRes = 0;
            try
            {
                strSql = string.Format(strSql, cardNo, clinicNo, mzInvoiceNo);

                iRes = this.QueryAccountCardFeeSQL(strSql, out lstCardFee);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return iRes;
        }

        #endregion

        /// <summary>
        /// 查询待上传的门诊患者
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public DataTable QueryNeedUploadDetail(string dtBegin, string dtEnd, string patientNo)
        {
            string sql = @"SELECT '0' 选择,
                                   '1' 类别,
                                   r.CARD_NO 病历号,
                                   r.NAME 姓名,
                                   r.IDENNO 证件号,
                                   decode(r.SEX_CODE, 'F', '女', '男') 性别,
                                   t.PACT_NAME 结算类型,
                                   t.INVOICE_NO 发票电脑号,
                                   t.PRINT_INVOICENO 发票印刷号,
                                   t.TOT_COST 总金额,
                                   t.PUB_COST 统筹金额,
                                   (t.OWN_COST + t.PAY_COST) 自费金额,
                                   FUN_GET_EMPLOYEE_NAME(t.OPER_CODE) 结算员,
                                   t.OPER_DATE 结算日期,
                                   t.CLINIC_CODE,
                                   t.PACT_CODE,
                                    (SELECT f.upload_type
                                      FROM FS_UPLOAD_DISEASEPAY f
                                     WHERE f.PATIENT_TYPE = '1'
                                       AND f.INVOICE_NO = t.INVOICE_NO
                                       AND f.CLINIC_CODE = t.CLINIC_CODE
                                       and rownum = 1) upload_type,
                                   (select g.err
                                      from upload_err_log g
                                     where g.inpatient_no = r.clinic_code
                                       and g.invoice_no = t.invoice_no
                                       and g.oper_date =
                                           (select max(a.oper_date)
                                              from upload_err_log a
                                             where a.inpatient_no = g.inpatient_no
                                               and a.invoice_no = g.invoice_no)
                                    and rownum = 1) err
                              FROM FIN_OPB_INVOICEINFO t, FIN_OPR_REGISTER r
                             WHERE t.CLINIC_CODE = r.CLINIC_CODE
                               AND t.CANCEL_FLAG = '1'
                               AND t.PACT_CODE IN (SELECT c.PACT_CODE
                                                     FROM FIN_COM_PACTUNITINFO c
                                                    WHERE c.DLL_NAME = '{2}')
                               AND t.OPER_DATE >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                               AND t.OPER_DATE <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                               AND NOT EXISTS (SELECT 1
                                      FROM FS_UPLOAD_DISEASEPAY f
                                     WHERE f.PATIENT_TYPE = '1'
                                       AND f.INVOICE_NO = t.INVOICE_NO
                                       AND f.CLINIC_CODE = t.CLINIC_CODE
                                       and f.upload_type = '3')
                               and (r.CARD_NO = '{3}' or '{3}' = 'ALL')
                            ";

            //佛山医保合同单位
            string strDllName = "FoShanSI.dll";
            if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
            {
                strDllName = "FoSiFoShanSI.dll";
            }

            sql = string.Format(sql, dtBegin, dtEnd, strDllName, patientNo);
            DataSet dsSet = null;
            if (this.ExecQuery(sql, ref dsSet) == -1)
            {
                return null;
            }
            return dsSet.Tables[0];
        }

        /// <summary>
        /// 查询已上传的门诊患者
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public DataTable QueryHaveUploadedDetail(string dtBegin, string dtEnd,string patientNo)
        {
            string sql = @"SELECT  '1' 选择,
                                   '1' 类别,
                                   r.CARD_NO 病历号,
                                   r.NAME 姓名,
                                   r.IDENNO 证件号,
                                   decode(r.SEX_CODE,'F','女','男') 性别,
                                   t.PACT_NAME 结算类型,
                                   t.INVOICE_NO 发票电脑号,
                                   t.PRINT_INVOICENO 发票印刷号,
                                   t.TOT_COST 总金额,
                                   t.PUB_COST 统筹金额,
                                   (t.OWN_COST+t.PAY_COST) 自费金额,
                                   FUN_GET_EMPLOYEE_NAME(t.OPER_CODE) 结算员,
                                   t.OPER_DATE 结算日期,
                                   t.CLINIC_CODE,
                                   t.PACT_CODE,
                                   FUN_GET_EMPLOYEE_NAME(fs.OPER_CODE) ,
                                   fs.OPER_DATE,
                                   fs.SFDJH,
                                   fs.WYLSH,
                                   fs.upload_type  
                            FROM FIN_OPB_INVOICEINFO t,FIN_OPR_REGISTER r,FS_UPLOAD_DISEASEPAY fs
                            WHERE t.CLINIC_CODE = r.CLINIC_CODE 
                            AND t.CANCEL_FLAG = '1'
                            AND t.PACT_CODE IN (SELECT c.PACT_CODE FROM FIN_COM_PACTUNITINFO c WHERE c.DLL_NAME =  '{2}')
                            AND t.oper_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                            AND t.oper_date <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                            AND fs.PATIENT_TYPE = '1'
                            AND t.INVOICE_NO = fs.INVOICE_NO
                            AND EXISTS(
                                SELECT 1 FROM FS_UPLOAD_DISEASEPAY f
                                WHERE f.PATIENT_TYPE = '1'
                                AND f.INVOICE_NO = t.INVOICE_NO
                                AND f.CLINIC_CODE = t.CLINIC_CODE
                                )
                            and (r.CARD_NO = '{3}'or '{3}' = 'ALL')
                            ";

            //佛山医保合同单位
            string strDllName = "FoShanSI.dll";
            if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
            {
                strDllName = "FoSiFoShanSI.dll";
            }

            sql = string.Format(sql, dtBegin, dtEnd, strDllName, patientNo);
            DataSet dsSet = null;
            if (this.ExecQuery(sql, ref dsSet) == -1)
            {
                return null;
            }
            return dsSet.Tables[0];
        }

        /// <summary>
        /// 获取项目信息
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryItemInfo()
        {
            ArrayList al = new ArrayList();
            string sqlStr = @"select a.item_code, a.item_name, '1', a.spell_code, a.wb_code, a.input_code
                          from fin_com_undruginfo a
                        union all
                        select b.drug_code,
                               b.trade_name,
                               '2',
                               b.spell_code,
                               b.wb_code,
                               b.custom_code
                          from pha_com_baseinfo b";
            try
            {
                this.ExecQuery(sqlStr);
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Base.Const Obj = new FS.HISFC.Models.Base.Const();
                    Obj.ID = Reader[0].ToString();
                    Obj.Name = Reader[1].ToString();
                    Obj.Memo = Reader[2].ToString();
                    Obj.SpellCode = Reader[3].ToString();
                    Obj.WBCode = Reader[4].ToString();
                    Obj.UserCode = Reader[5].ToString();
                    al.Add(Obj);
                }
                return al;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
