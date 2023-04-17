using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.HISFC.Models;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Registration;
using FS.HISFC.Models.Fee.Outpatient;
using FS.HISFC.Models.Pharmacy;
using FS.FrameWork.Function;

namespace FoShanSporadicUpload.BizLogic
{
    /// <summary>
    /// 门诊业务管理类
    /// </summary>
    public class OutFeeManager : FS.HISFC.BizLogic.Fee.Outpatient
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
                    balance.Invoice.User03 = this.Reader[38].ToString();  //38 同一发票号 {0384001D-318D-4b9a-BF99-005808D331C3}
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

            string printInvoiceNO = this.ConvertToPrintInvoiceNO(invoiceNO); 

            //WHERE语句
            string where = @"
                            Where   INVOICE_NO='{0}' or PRINT_INVOICENO='{1}' or PRINT_INVOICENO='{2}' ";

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
            string printInvoiceNO = "";
            if (invoiceNO.Length <= 8)
            {
                printInvoiceNO = invoiceNO.PadLeft(12,'0');
            }
            else
            {
                printInvoiceNO = "0000" + invoiceNO.Substring(2);
            }
            //string printInvoiceNO = "0000" + invoiceNO;
            //string printInvoiceNO = invoiceNO;

            return printInvoiceNO;
        }

        #endregion

    }
}
