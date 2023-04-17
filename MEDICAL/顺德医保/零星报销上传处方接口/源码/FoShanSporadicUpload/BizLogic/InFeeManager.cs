using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.HISFC.Models;
using FS.FrameWork.Models;
using FS.HISFC.Models.Fee.Inpatient;
using FS.HISFC.Models.Fee;
using FS.HISFC.Models.RADT;
using FS.FrameWork.Function;
using FS.HISFC.Models.Base;

namespace FoShanSporadicUpload.BizLogic
{
    /// <summary>
    /// 住院业务管理类
    /// </summary>
    public class InFeeManager : FS.HISFC.BizLogic.Fee.InPatient
    {
        #region 非药品

        /// <summary>
        /// 获得费用项目信息
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns>成功: 获得费用项目信息 失败: null</returns>
        private ArrayList QueryFeeItemListsBySql(string sql, params string[] args)
        {
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            ArrayList feeItemLists = new ArrayList();//费用明细信息集合
            FeeItemList itemList = null;//费用明细实体

            try
            {
                while (this.Reader.Read())
                {
                    itemList = new FeeItemList();

                    itemList.RecipeNO = this.Reader[0].ToString();//0 处方号
                    itemList.SequenceNO = NConvert.ToInt32(this.Reader[1].ToString());//1处方内项目流水号
                    itemList.TransType = (TransTypes)NConvert.ToInt32(Reader[2].ToString());//2交易类型,1正交易，2反交易
                    itemList.ID = this.Reader[3].ToString();//3住院流水号
                    itemList.Patient.ID = this.Reader[3].ToString();//3住院流水号
                    itemList.Name = this.Reader[4].ToString();//4姓名
                    itemList.Patient.Name = this.Reader[4].ToString();//4姓名
                    itemList.Patient.Pact.PayKind.ID = this.Reader[5].ToString();//5结算类别
                    itemList.Patient.Pact.ID = this.Reader[6].ToString();//6合同单位
                    itemList.UpdateSequence = NConvert.ToInt32(this.Reader[7].ToString());//7更新库存的流水号(物资)
                    ((PatientInfo)itemList.Patient).PVisit.PatientLocation.Dept.ID = this.Reader[8].ToString();//8在院科室代码
                    itemList.Order.Patient.PVisit.PatientLocation.Dept.ID = this.Reader[8].ToString();
                    ((PatientInfo)itemList.Patient).PVisit.PatientLocation.NurseCell.ID = this.Reader[9].ToString();//9护士站代码
                    itemList.Order.Patient.PVisit.PatientLocation.NurseCell.ID = this.Reader[9].ToString();

                    itemList.RecipeOper.Dept.ID = this.Reader[10].ToString();//10开立科室代码
                    itemList.ExecOper.Dept.ID = this.Reader[11].ToString();//11执行科室代码
                    itemList.StockOper.Dept.ID = this.Reader[12].ToString();//12扣库科室代码
                    itemList.RecipeOper.ID = this.Reader[13].ToString();//13开立医师代码
                    itemList.Item.ID = this.Reader[14].ToString();//14项目代码
                    itemList.Item.MinFee.ID = this.Reader[15].ToString();//15最小费用代码
                    itemList.Compare.CenterItem.ID = this.Reader[16].ToString();//16中心代码
                    itemList.Item.Name = this.Reader[17].ToString();//17项目名称
                    itemList.Item.Price = NConvert.ToDecimal(this.Reader[18].ToString());//18单价
                    itemList.Item.Qty = NConvert.ToDecimal(this.Reader[19].ToString());//19数量
                    itemList.Item.PriceUnit = this.Reader[20].ToString();//20当前单位
                    itemList.UndrugComb.ID = this.Reader[21].ToString();//21组套代码
                    itemList.UndrugComb.Name = this.Reader[22].ToString();//22组套名称
                    itemList.FT.TotCost = NConvert.ToDecimal(this.Reader[23].ToString());//23费用金额
                    itemList.FT.OwnCost = NConvert.ToDecimal(this.Reader[24].ToString());//24自费金额
                    itemList.FT.PayCost = NConvert.ToDecimal(this.Reader[25].ToString());//25自付金额
                    itemList.FT.PubCost = NConvert.ToDecimal(this.Reader[26].ToString());//26公费金额
                    itemList.FT.RebateCost = NConvert.ToDecimal(this.Reader[27].ToString());//27优惠金额
                    itemList.SendSequence = NConvert.ToInt32(this.Reader[28].ToString());//28出库单序列号
                    itemList.PayType = (PayTypes)NConvert.ToInt32(this.Reader[29].ToString());//29收费状态
                    itemList.IsBaby = NConvert.ToBoolean(this.Reader[30].ToString());//30是否婴儿用
                    ((FS.HISFC.Models.Order.Inpatient.Order)itemList.Order).OrderType.ID = this.Reader[32].ToString();
                    itemList.Invoice.ID = this.Reader[33].ToString();//33结算发票号
                    itemList.BalanceNO = NConvert.ToInt32(this.Reader[34].ToString());//34结算序号
                    itemList.ChargeOper.ID = this.Reader[36].ToString();//36划价人
                    itemList.ChargeOper.OperTime = NConvert.ToDateTime(this.Reader[37].ToString());//37划价日期
                    itemList.MachineNO = this.Reader[39].ToString();//39设备号
                    itemList.ExecOper.ID = this.Reader[40].ToString();//40执行人代码
                    itemList.ExecOper.OperTime = NConvert.ToDateTime(this.Reader[41].ToString());//41执行日期
                    itemList.FeeOper.ID = this.Reader[42].ToString();//42计费人
                    itemList.FeeOper.OperTime = NConvert.ToDateTime(this.Reader[43].ToString());//43计费日期
                    itemList.AuditingNO = this.Reader[45].ToString();//45审核序号
                    itemList.Order.ID = this.Reader[46].ToString();//46医嘱流水号
                    itemList.ExecOrder.ID = this.Reader[47].ToString();//47医嘱执行单流水号
                    //itemList.Item.IsPharmacy = false;
                    //itemList.Item.ItemType = //HISFC.Models.Base.EnumItemType.UnDrug;
                    itemList.NoBackQty = NConvert.ToDecimal(this.Reader[48].ToString());//48可退数量
                    itemList.BalanceState = this.Reader[49].ToString();//49结算状态
                    itemList.FTRate.ItemRate = NConvert.ToDecimal(this.Reader[50].ToString());//50收费比例
                    itemList.FeeOper.Dept.ID = this.Reader[51].ToString();//51收费员科室
                    itemList.FTSource = new FTSource(this.Reader[54].ToString());
                    if (itemList.Item.PackQty == 0)
                    {
                        itemList.Item.PackQty = 1;
                    }
                    itemList.Item.ItemType = (FS.HISFC.Models.Base.EnumItemType)(NConvert.ToInt32(this.Reader[58]));
                    //{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E} 增加医疗组处理
                    itemList.MedicalTeam.ID = this.Reader[59].ToString();
                    // 手术编码{0604764A-3F55-428f-9064-FB4C53FD8136}
                    itemList.OperationNO = this.Reader[60].ToString();
                    feeItemLists.Add(itemList);
                }

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
        /// 获取检索fin_com_itemlist的全部数据的sql
        /// </summary>
        /// <returns>成功:检索fin_com_itemlist的全部数据的sql 失败:null</returns>
        private string GetFeeItemsSelectSql()
        {
            string sql = string.Empty;//SQly语句

            if (this.Sql.GetSql("Fee.SelectAllFromFeeItem.1", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.SelectAllFromFeeItem.1的SQL语句";

                return null;
            }

            return sql;
        }
        
        /// <summary>
        /// 根据发票号获取非药品费用项目信息
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="invoiceNO"></param>
        /// <returns>成功: 获得费用项目信息 失败: null</returns>
        public ArrayList QueryFeeItemListsByInvoiceNO(string inpatientNO, string invoiceNO)
        {
            //SELECT语句
            string sql = this.GetFeeItemsSelectSql();
            if (string.IsNullOrEmpty(sql))
            {
                return null;
            }

            //WHERE语句
            string where = @"
                            WHERE INPATIENT_NO = '{0}' AND INVOICE_NO = '{1}' ORDER BY CHARGE_DATE  ";


            return this.QueryFeeItemListsBySql(sql + " " + where, inpatientNO, invoiceNO);
        }

        #endregion

        #region 药品

        /// <summary>
        /// 根据发票号获取药品费用项目信息
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="invoiceNO"></param>
        /// <returns>成功: 获得费用项目信息 失败: null</returns>
        public ArrayList QueryMedItemListsByInvoiceNO(string inpatientNO, string invoiceNO)
        {
            //SELECT语句
            string sql = this.GetMedItemListSelectSql();
            if (string.IsNullOrEmpty(sql))
            {
                return null;
            }

            //WHERE语句
            string where = @"
                            WHERE INPATIENT_NO = '{0}' AND INVOICE_NO = '{1}' ORDER BY CHARGE_DATE  ";

            return this.QueryMedItemListsBySql(sql + " " + where, inpatientNO, invoiceNO);
        }

        #endregion

        #region 住院结算信息

        /// <summary>
        /// 获得结算信息
        /// 
        /// {F6C3D16B-8F52-4449-814C-5262F90C583B}
        /// </summary>
        /// <param name="sql">查询的SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns>成功:结算头表信息 失败:null 没有查找到数据ArrayList.Count = 0</returns>
        private ArrayList QueryBalancesBySql(string sql, params string[] args)
        {
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            ArrayList balances = new ArrayList();//结算头实体集合
            Balance balance = null;//结算头实体

            try
            {
                //开始循环读取数据
                while (this.Reader.Read())
                {
                    balance = new Balance();

                    balance.Invoice.ID = this.Reader[0].ToString();//0发票号码
                    balance.TransType = (TransTypes)NConvert.ToInt32(Reader[1].ToString());//1交易类型
                    balance.Patient.ID = this.Reader[2].ToString();//2住院流水号
                    balance.ID = this.Reader[3].ToString();//3结算序号
                    balance.Patient.Pact.PayKind.ID = this.Reader[4].ToString();//4结算类别
                    balance.Patient.Pact.ID = this.Reader[5].ToString();//5合同代码
                    balance.FT.PrepayCost = NConvert.ToDecimal(this.Reader[6].ToString());//6预交金额
                    balance.FT.TransferPrepayCost = NConvert.ToDecimal(this.Reader[7].ToString());//7转入预交金额
                    balance.FT.TotCost = NConvert.ToDecimal(this.Reader[8].ToString());//8费用金额
                    balance.FT.OwnCost = NConvert.ToDecimal(this.Reader[9].ToString());//9自费金额
                    balance.FT.PayCost = NConvert.ToDecimal(this.Reader[10].ToString());//10自付金额
                    balance.FT.PubCost = NConvert.ToDecimal(this.Reader[11].ToString());//11公费金额
                    balance.FT.RebateCost = NConvert.ToDecimal(this.Reader[12].ToString());//12优惠金额
                    balance.FT.DerateCost = NConvert.ToDecimal(this.Reader[13].ToString());//13减免金额
                    balance.FT.TransferTotCost = NConvert.ToDecimal(this.Reader[14].ToString());//14转入费用金额
                    balance.FT.SupplyCost = NConvert.ToDecimal(this.Reader[15].ToString());//15补收金额
                    balance.FT.ReturnCost = NConvert.ToDecimal(this.Reader[16].ToString());//16返还金额
                    balance.FT.TransferPrepayCost = NConvert.ToDecimal(this.Reader[17].ToString());//17转押金
                    balance.BeginTime = NConvert.ToDateTime(this.Reader[18].ToString());//18起始日期
                    balance.EndTime = NConvert.ToDateTime(this.Reader[19].ToString());//19终止日期
                    balance.BalanceType.ID = this.Reader[20].ToString();//20结算类型
                    balance.BalanceOper.ID = this.Reader[21].ToString();//21结算人代码
                    balance.BalanceOper.OperTime = NConvert.ToDateTime(this.Reader[22].ToString());//22结算时间
                    balance.FinanceGroup.ID = this.Reader[23].ToString();//23财务组代码
                    balance.PrintTimes = NConvert.ToInt32(this.Reader[24].ToString());//24打印次数
                    balance.AuditingNO = this.Reader[25].ToString();//25审核序号
                    balance.CancelType = (CancelTypes)NConvert.ToInt32(Reader[26].ToString());//26作废标志
                    balance.IsMainInvoice = NConvert.ToBoolean(this.Reader[27].ToString());//27主发票标记
                    balance.IsLastBalance = NConvert.ToBoolean(this.Reader[28].ToString());//28生育保险最后结算标记
                    balance.Name = this.Reader[29].ToString();//29 患者姓名
                    balance.Patient.Name = this.Reader[29].ToString();//29 患者姓名
                    balance.BalanceOper.Dept.ID = this.Reader[30].ToString();//30结算员科室
                    balance.FT.AdjustOvertopCost = NConvert.ToDecimal(this.Reader[31].ToString());//31结算调整公费日限额超标金额
                    balance.CancelOper.ID = this.Reader[32].ToString();//32作废操作员
                    balance.CancelOper.OperTime = NConvert.ToDateTime(this.Reader[33].ToString());//33作废时间
                    //balance.User01 = this.Reader[34].ToString();
                    balance.IsTempInvoice = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[34].ToString());
                    balance.User02 = this.Reader[35].ToString();
                    balance.BalanceSaveType = this.Reader[36].ToString();
                    balance.BalanceSaveOper.ID = this.Reader[37].ToString();
                    balance.BalanceSaveOper.OperTime = NConvert.ToDateTime(this.Reader[38]);

                    // {F6C3D16B-8F52-4449-814C-5262F90C583B}
                    balance.PrintedInvoiceNO = this.Reader[39].ToString().Trim();
                    balance.FT.ArrearCost = NConvert.ToDecimal(this.Reader[40]);

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
        /// 获取检索fin_ipb_balanceHead的全部数据的sql
        /// </summary>
        /// <returns>成功:检索SQL语句 失败:null</returns>
        private string GetSqlForSelectAllInfoFromBalanceHead()
        {
            string sql = string.Empty;//SQL语句

            if (this.Sql.GetSql("Fee.GetSqlForSelectAllInfoFromBalanceHead.1", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.GetSqlForSelectAllInfoFromBalanceHead.1的SQL语句";

                return null;
            }

            return sql;
        }

        /// <summary>
        /// 通过发票号查询住院结算信息
        /// </summary>
        /// <param name="invoiceNO"></param>
        /// <returns></returns>
        public ArrayList QueryBalances(string invoiceNO)
        {
            ////SELECT语句
            string sql = this.GetSqlForSelectAllInfoFromBalanceHead();
            if (string.IsNullOrEmpty(sql))
            {
                return null;
            }

            string printInvoiceNO = this.ConvertToPrintInvoiceNO(invoiceNO); 

            //WHERE语句
            string where = @"
                            AND (INVOICE_NO='{0}' or PRINT_INVOICENO='{1}'or PRINT_INVOICENO='{2}') ";

            return this.QueryBalancesBySql(sql + " " + where, invoiceNO, printInvoiceNO, invoiceNO.PadLeft(12,'0'));
        }

        #endregion

        #region Common

        /// <summary>
        /// 转换成HIS对应的发票打印号
        /// </summary>
        /// <param name="invoiceNO"></param>
        /// <returns></returns>
        private string ConvertToPrintInvoiceNO(string invoiceNO)
        {
            if (invoiceNO.Length <= 2)
            {
                return invoiceNO;
            }
            if (invoiceNO.Length <= 8)
            {
                invoiceNO = invoiceNO.PadLeft(12,'0');
            }
            else
            {
                invoiceNO = "0000" + invoiceNO.Substring(2);
            }
            //string printInvoiceNO = "0000" + invoiceNO;
            //string printInvoiceNO = invoiceNO;
            return invoiceNO;
        }

        #endregion
    }
}
