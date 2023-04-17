using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using FS.HISFC.Models;
using FS.FrameWork.Models;
using FS.HISFC.Models.Fee.Inpatient;
using FS.HISFC.Models.Fee;
using FS.HISFC.Models.RADT;
using FS.FrameWork.Function;
using FS.HISFC.Models.Base;

namespace FoShanDiseasePay.BizLogic
{
    /// <summary>
    /// 住院业务管理类
    /// </summary>
    public class InManager : FS.HISFC.BizLogic.Fee.InPatient
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
                    itemList.ConfirmedQty = NConvert.ToDecimal(this.Reader[38].ToString());//38确认数量
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
                    //修改医技退费更新取消确认数量bug {47531763-0983-44dc-BC92-59A5588AF2F8} 2014-12-11 by lixuelong
                    itemList.ExtCode = this.Reader[55].ToString();//52退费原记录的处方号
                    itemList.FTSource = new FTSource(this.Reader[54].ToString());
                    if (itemList.Item.PackQty == 0)
                    {
                        itemList.Item.PackQty = 1;
                    }
                    itemList.Item.ItemType = (FS.HISFC.Models.Base.EnumItemType)(NConvert.ToInt32(this.Reader[58]));
                    //增加医疗组处理
                    itemList.MedicalTeam.ID = this.Reader[59].ToString();
                    // 手术编码
                    itemList.OperationNO = this.Reader[60].ToString();
                    //医保上传标记
                    itemList.User03 = this.Reader[61].ToString();
                    //特批标记
                    itemList.Item.SpecialFlag4 = this.Reader[62].ToString();
                    itemList.Item.DefPrice = NConvert.ToDecimal(this.Reader[63].ToString());
                    itemList.UndrugComb.Qty = NConvert.ToDecimal(this.Reader[64].ToString());
                    itemList.SplitID = this.Reader[65].ToString();
                    itemList.SplitFlag = NConvert.ToBoolean(this.Reader[66].ToString());
                    itemList.SplitFeeFlag = NConvert.ToBoolean(this.Reader[67].ToString());
                    if (this.Reader.FieldCount > 68)
                    {
                        itemList.ExecOrder.DateUse = NConvert.ToDateTime(this.Reader[68].ToString());
                        if (this.Reader[69] != DBNull.Value) itemList.FT.DonateCost = Decimal.Parse(this.Reader[69].ToString());
                        else
                        {
                            itemList.FT.DonateCost = 0.0m;
                        }
                        if (this.Reader[70] != DBNull.Value) itemList.PackageFlag = this.Reader[70].ToString();
                        else
                        {
                            itemList.PackageFlag = "0";
                        }
                    }

                    if (this.Reader.FieldCount > 71)
                    {
                        itemList.Item.ChildPrice = NConvert.ToDecimal(this.Reader[71].ToString());//儿童价
                        itemList.Item.SpecialPrice = NConvert.ToDecimal(this.Reader[72].ToString());//特诊价
                    }
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
                this.Err = "没有找到索引为:【Fee.SelectAllFromFeeItem.SI】的SQL语句";

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
            WHERE INPATIENT_NO = '{0}' AND INVOICE_NO = '{1}'and upload_flag !='2'  ORDER BY CHARGE_DATE  ";


            return this.QueryFeeItemListsBySql(sql + " " + where, inpatientNO, invoiceNO);
        }

        /// <summary>
        /// 获取所有上传社保的费用明细// {E1865220-C459-45ca-966E-F111E0A6B560}
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="invoiceNO"></param>
        /// <returns></returns>
        public ArrayList QueryAllFeeItemListsByInvoiceNO(string invoiceNO, string inpatientNo, string jydjh)
        {
            string sql = @"select m.ryrq,
                              m.xmbh,
                              m.xmmc,
                              m.fldm,
                              m.ypgg,
                              m.jg,
                              m.mcyl,
                              m.je,
                              d.min_unit,
                              decode(d.trade_name,null,'0','1'),
                              m.fyrq
                              from GZSI_HIS_CFXM m
                              left join pha_com_baseinfo d on m.xmbh = d.custom_code  
                             where m.invoice_no = '{0}'
                             and m.inpatient_no = '{1}'
                             and m.jydjh = '{2}'
                            ";
            //sql = string.Format(sql, invoiceNO, jsid);

            if (this.ExecQuery(sql, invoiceNO, inpatientNo,jydjh) == -1)
            {
                return null;
            }
            ArrayList feeItemLists = new ArrayList();//费用明细信息集合
            FS.HISFC.Models.Fee.Inpatient.FeeItemList itemList = null;//费用明细实体
            
            try
            {
                while (this.Reader.Read())
                {
                    itemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();

                    itemList.ChargeOper.OperTime = NConvert.ToDateTime(this.Reader[0].ToString());//0 
                    itemList.Item.ID = this.Reader[1].ToString();//0
                    itemList.Item.GBCode = this.Reader[1].ToString();//0
                    itemList.Item.UserCode = this.Reader[1].ToString();//0
                    itemList.Item.Name = this.Reader[2].ToString();//0
                    itemList.Item.MinFee.ID = this.Reader[3].ToString();//0
                    itemList.Item.Specs = this.Reader[4].ToString();//0
                    itemList.Item.Price = NConvert.ToDecimal(this.Reader[5].ToString());//0
                    itemList.Item.SpecialPrice = NConvert.ToDecimal(this.Reader[5].ToString());//0
                    itemList.Item.Qty = NConvert.ToDecimal(this.Reader[6].ToString());//0
                    itemList.FT.TotCost = NConvert.ToDecimal(this.Reader[7].ToString());//0
                    itemList.FT.OwnCost = NConvert.ToDecimal(this.Reader[7].ToString());//0
                    itemList.Item.PriceUnit = this.Reader[8].ToString();//0
                    if (this.Reader[9].ToString() == "1")
                    {
                        itemList.Item.ItemType = EnumItemType.Drug;
                    }
                    else
                    {
                        itemList.Item.ItemType = EnumItemType.UnDrug;
                    }
                    itemList.ChargeOper.OperTime = NConvert.ToDateTime(this.Reader[10].ToString());

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
        /// 获取医嘱信息// {E1865220-C459-45ca-966E-F111E0A6B560}
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="inputCode"></param>
        public void GetOrderInfo(string inpatientNo, string inputCode,
            ref string dose_once, ref string frequency, ref string usage, ref string comb_no, ref string type_code, ref string recipe_no)
        {
            string sql = @"
                        select nvl((select o.dose_once
                                  From met_ipm_order o
                                 where o.mo_order = r.mo_order),0) dose_once,
                               (select o.frequency_code
                                  From met_ipm_order o
                                 where o.mo_order = r.mo_order) frequency,
                               (select o.usage_code
                                  From met_ipm_order o
                                 where o.mo_order = r.mo_order) usage,
                               (select o.comb_no From met_ipm_order o where o.mo_order = r.mo_order) comb_no,
                               (select o.type_code
                                  From met_ipm_order o
                                 where o.mo_order = r.mo_order) type_code,
                             r.recipe_no
                          from fin_ipb_itemlist r
                         where r.item_code in (select d.drug_code
                                                 from pha_com_baseinfo d
                                                where d.custom_code = '{1}'
                                               union all
                                               select u.item_code
                                                 from fin_com_undruginfo u
                                                where u.input_code = '{1}')
                           and r.inpatient_no = '{0}'
                        union all

                        select nvl((select o.dose_once
                                  From met_ipm_order o
                                 where o.mo_order = r.mo_order),0) dose_once,
                               (select o.frequency_code
                                  From met_ipm_order o
                                 where o.mo_order = r.mo_order) frequency,
                               (select o.usage_code
                                  From met_ipm_order o
                                 where o.mo_order = r.mo_order) usage,
                               (select o.comb_no From met_ipm_order o where o.mo_order = r.mo_order) comb_no,
                               (select o.type_code
                                  From met_ipm_order o
                                 where o.mo_order = r.mo_order) type_code,
                             r.recipe_no
                          from fin_ipb_medicinelist r
                         where r.drug_code in (select d.drug_code
                                                 from pha_com_baseinfo d
                                                where d.custom_code = '{1}'
                                               union all
                                               select u.item_code
                                                 from fin_com_undruginfo u
                                                where u.input_code = '{1}')
                           and r.inpatient_no = '{0}'
                        ";

            sql = string.Format(sql, inpatientNo, inputCode);

            System.Data.DataSet dsResult = null;
            if (this.ExecQuery(sql, ref dsResult) == -1)
            {
                return;
            }

            dose_once = "";
            frequency = "";
            usage = "";
            comb_no = "";
            type_code = "";
            recipe_no = "";
            DataTable dtResult = dsResult.Tables[0];
            foreach (DataRow dr in dtResult.Rows)
            {
                dose_once = dr["dose_once"].ToString();
                frequency = dr["frequency"].ToString();
                usage = dr["usage"].ToString();
                type_code = dr["type_code"].ToString();
                comb_no = dr["comb_no"].ToString();
                recipe_no = dr["recipe_no"].ToString();
            }
        }

        #endregion

        #region 药品

        /// <summary>
        /// 获取检索fin_com_medicinelist的全部数据的sql
        /// </summary>
        /// <returns>成功: 获取检索fin_com_medicinelist的全部数据的sql 失败:null</returns>
        public new string GetMedItemListSelectSql()
        {
            string sql = string.Empty;

            if (this.Sql.GetSql("Fee.SelectAllFromMedItem.1", ref sql) == -1)
            {
                this.Err = "没有找到索引为:【Fee.SelectAllFromMedItem.SI】的SQL语句";

                return null;
            }

            return sql;
        }

        /// <summary>
        /// 获得药品费用项目信息
        /// </summary>
        /// <param name="sql">SQl语句</param>
        /// <param name="args">参数</param>
        /// <returns>成功:获得药品费用项目信息 失败: null</returns>
        public new ArrayList QueryMedItemListsBySql(string sql, params string[] args)
        {
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            ArrayList medItemLists = new ArrayList();//药品明细集合
            FeeItemList itemList = null;//药品明细实体

            try
            {
                while (this.Reader.Read())
                {
                    itemList = new FeeItemList();

                    FS.HISFC.Models.Pharmacy.Item pharmacyItem = new FS.HISFC.Models.Pharmacy.Item();
                    itemList.Item = pharmacyItem;

                    itemList.RecipeNO = this.Reader[0].ToString();//0 处方号
                    itemList.SequenceNO = NConvert.ToInt32(this.Reader[1].ToString());//1处方内项目流水号
                    itemList.TransType = (TransTypes)NConvert.ToInt32(this.Reader[2].ToString());//2交易类型,1正交易，2反交易
                    itemList.ID = this.Reader[3].ToString();//3住院流水号
                    itemList.Patient.ID = this.Reader[3].ToString();//3住院流水号
                    itemList.Name = this.Reader[4].ToString();//4姓名
                    itemList.Patient.Name = this.Reader[4].ToString();//4姓名
                    itemList.Patient.Pact.PayKind.ID = this.Reader[5].ToString();//5结算类别
                    itemList.Patient.Pact.ID = this.Reader[6].ToString();//6合同单位
                    itemList.UpdateSequence = NConvert.ToInt32(this.Reader[7].ToString());//7更新库存的流水号(物资)
                    ((PatientInfo)itemList.Patient).PVisit.PatientLocation.Dept.ID = this.Reader[8].ToString();//8在院科室代码
                    ((PatientInfo)itemList.Patient).PVisit.PatientLocation.NurseCell.ID = this.Reader[9].ToString();//9护士站代码
                    itemList.Order.Patient.PVisit.PatientLocation.Dept.ID = this.Reader[8].ToString();
                    itemList.Order.Patient.PVisit.PatientLocation.NurseCell.ID = this.Reader[9].ToString();

                    itemList.RecipeOper.Dept.ID = this.Reader[10].ToString();//10开立科室代码
                    itemList.ExecOper.Dept.ID = this.Reader[11].ToString();//11执行科室代码
                    itemList.StockOper.Dept.ID = this.Reader[12].ToString();//12扣库科室代码
                    itemList.RecipeOper.ID = this.Reader[13].ToString();//13开立医师代码
                    itemList.Item.ID = this.Reader[14].ToString();//14项目代码
                    itemList.Item.MinFee.ID = this.Reader[15].ToString();//15最小费用代码
                    itemList.Compare.CenterItem.ID = this.Reader[16].ToString();//16中心代码
                    itemList.Item.Name = this.Reader[17].ToString();//17项目名称
                    itemList.Item.Price = NConvert.ToDecimal(this.Reader[18].ToString());//18单价1
                    itemList.Item.Qty = NConvert.ToDecimal(this.Reader[19].ToString());//9数量
                    itemList.Item.PriceUnit = this.Reader[20].ToString();//20当前单位
                    itemList.Item.PackQty = NConvert.ToDecimal(this.Reader[21].ToString());//21包装数量
                    itemList.Days = NConvert.ToDecimal(this.Reader[22].ToString());//22付数
                    itemList.FT.TotCost = NConvert.ToDecimal(this.Reader[23].ToString());//23费用金额
                    itemList.FT.OwnCost = NConvert.ToDecimal(this.Reader[24].ToString());//24自费金额
                    itemList.FT.PayCost = NConvert.ToDecimal(this.Reader[25].ToString());//25自付金额
                    itemList.FT.PubCost = NConvert.ToDecimal(this.Reader[26].ToString());//26公费金额
                    itemList.FT.RebateCost = NConvert.ToDecimal(this.Reader[27].ToString());//27优惠金额
                    itemList.SendSequence = NConvert.ToInt32(this.Reader[28].ToString());//28出库单序列号
                    itemList.PayType = (PayTypes)NConvert.ToInt32(this.Reader[29].ToString());//29收费状态
                    itemList.IsBaby = NConvert.ToBoolean(this.Reader[30].ToString());//30是否婴儿用
                    ((FS.HISFC.Models.Order.Inpatient.Order)itemList.Order).OrderType.ID = this.Reader[32].ToString();//32出院带疗标记
                    itemList.Invoice.ID = this.Reader[33].ToString();//33结算发票号
                    itemList.BalanceNO = NConvert.ToInt32(this.Reader[34].ToString());//34结算序号
                    itemList.ChargeOper.ID = this.Reader[36].ToString();//36划价人
                    itemList.ChargeOper.OperTime = NConvert.ToDateTime(this.Reader[37].ToString());//37划价日期
                    pharmacyItem.Product.IsSelfMade = NConvert.ToBoolean(this.Reader[38].ToString());//38自制标识
                    pharmacyItem.Quality.ID = this.Reader[39].ToString();//39药品性质
                    itemList.ExecOper.ID = this.Reader[40].ToString();//40发药人代码
                    itemList.ExecOper.OperTime = NConvert.ToDateTime(this.Reader[41].ToString());//41发药日期
                    itemList.FeeOper.ID = this.Reader[42].ToString();//42计费人
                    itemList.FeeOper.OperTime = NConvert.ToDateTime(this.Reader[43].ToString());//43计费日期
                    itemList.AuditingNO = this.Reader[45].ToString();//45审核序号
                    itemList.Order.ID = this.Reader[46].ToString();//46医嘱流水号
                    itemList.ExecOrder.ID = this.Reader[47].ToString();//47医嘱执行单流水号
                    pharmacyItem.Specs = this.Reader[48].ToString();//规格
                    pharmacyItem.Type.ID = this.Reader[49].ToString();//49药品类别
                    //pharmacyItem.IsPharmacy = true;
                    pharmacyItem.ItemType = FS.HISFC.Models.Base.EnumItemType.Drug;
                    itemList.NoBackQty = NConvert.ToDecimal(this.Reader[50].ToString());//50可退数量
                    itemList.BalanceState = this.Reader[51].ToString();//51结算状态
                    itemList.FTRate.ItemRate = NConvert.ToDecimal(this.Reader[52].ToString());//52收费比例
                    itemList.FTRate.OwnRate = itemList.FTRate.ItemRate;

                    itemList.FeeOper.Dept.ID = this.Reader[53].ToString();//53收费员科室
                    //itemList.Item.IsPharmacy = true;
                    itemList.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.Drug;
                    itemList.FTSource = new FTSource(this.Reader[56].ToString());
                    //{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E} 增加医疗组处理
                    itemList.MedicalTeam.ID = this.Reader[60].ToString();
                    // 手术编码{0604764A-3F55-428f-9064-FB4C53FD8136}
                    itemList.OperationNO = this.Reader[61].ToString();

                    if (this.Reader.FieldCount >= 63 && !string.IsNullOrEmpty(this.Reader[62].ToString()))
                    {
                        // 默认价格总金额
                        itemList.Item.DefPrice = NConvert.ToDecimal(this.Reader[62].ToString());
                        itemList.FT.DefTotCost = NConvert.ToDecimal(this.Reader[62].ToString());
                    }
                    if (this.Reader.FieldCount >= 64 && !string.IsNullOrEmpty(this.Reader[63].ToString()))
                    {
                        //医保上传标记
                        itemList.User03 = this.Reader[63].ToString();
                    }
                    itemList.ExecOrder.DateUse = NConvert.ToDateTime(this.Reader[64].ToString());
                    //if (this.Reader.FieldCount >= 66 && !string.IsNullOrEmpty(this.Reader[65].ToString()))
                    //{
                    //    itemList.Item.SpecialFlag4 = this.Reader[65].ToString(); //特批标记
                    //}
                    itemList.Order.DoseOnce = NConvert.ToDecimal(this.Reader[65].ToString());
                    itemList.Order.Frequency.ID = this.Reader[66].ToString();
                    itemList.Order.Usage.ID = this.Reader[67].ToString();
                    itemList.Order.Combo.ID = this.Reader[68].ToString();
                    itemList.Order.OrderType.ID = this.Reader[69].ToString();

                    if (this.Reader[70] != DBNull.Value) itemList.FT.DonateCost = Decimal.Parse(this.Reader[70].ToString()); //{18AD984D-78C6-4c64-8066-6F31E3BDF7DA}
                    else
                    {
                        itemList.FT.DonateCost = 0;
                    }

                    if (this.Reader[71] != DBNull.Value)
                        itemList.PackageFlag = this.Reader[71].ToString(); //{18AD984D-78C6-4c64-8066-6F31E3BDF7DA}
                    else
                    {
                        itemList.PackageFlag = "0";
                    }

                    if (this.Reader.FieldCount > 72)
                    {
                        itemList.Item.ChildPrice = NConvert.ToDecimal(this.Reader[72].ToString());//儿童价
                        itemList.Item.SpecialPrice = NConvert.ToDecimal(this.Reader[73].ToString());//特诊价
                    }
                    medItemLists.Add(itemList);
                }

                this.Reader.Close();

                return medItemLists;
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
            WHERE INPATIENT_NO = '{0}' AND INVOICE_NO = '{1}'and upload_flag !='2' ORDER BY CHARGE_DATE  ";

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

            //WHERE语句
            string where = @" 
                            AND INVOICE_NO like'%{0}%'";

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
        public string GetSiInfo(string clinicCode, string invoiceNO, string patientType)
        {
            string strSql = "SELECT t.REMARK FROM FIN_IPR_SIINMAININFO t WHERE t.INPATIENT_NO = '{0}' AND t.INVOICE_NO like '%{1}%' AND t.TYPE_CODE = '{2}'";
            strSql = string.Format(strSql, clinicCode, invoiceNO, patientType);
            return this.ExecSqlReturnOne(strSql);
        }

        #endregion


        #region 住院医嘱信息

        /// <summary>
        /// 查询患者医嘱信息的select语句（无where条件）
        /// </summary>
        /// <returns></returns>
        private string OrderQuerySelect()
        {
            #region 接口说明
            //Order.Order.QueryOrder.Select.1
            //传入：0
            //传出：sql.select
            #endregion
            string sql = "";
            if (this.Sql.GetSql("Order.Order.QueryOrder.Select.1", ref sql) == -1)
            {
                this.Err = "没有找到Order.Order.QueryOrder.Select.1字段!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            return sql;
        }

        /// <summary>
        /// 私有函数，查询医嘱信息
        /// </summary>
        /// <param name="SQLPatient"></param>
        /// <returns></returns>
        private ArrayList MyOrderQuery(string SQLPatient)
        {
            ArrayList al = new ArrayList();

            if (this.ExecQuery(SQLPatient) == -1) return null;
            try
            {
                while (this.Reader.Read())
                {
                    //目前医嘱字段取值有 99 gumzh 2013-11-13
                    FS.HISFC.Models.Order.Inpatient.Order objOrder = new FS.HISFC.Models.Order.Inpatient.Order();
                    #region "患者信息"
                    //患者信息——  
                    //			1 住院流水号   2住院病历号     3患者科室id      4患者护理id
                    try
                    {
                        objOrder.Patient.ID = this.Reader[1].ToString();
                        objOrder.Patient.PID.PatientNO = this.Reader[2].ToString();
                        objOrder.Patient.PVisit.PatientLocation.Dept.ID = this.Reader[3].ToString();
                        objOrder.InDept.ID = this.Reader[3].ToString();
                        objOrder.Patient.PVisit.PatientLocation.NurseCell.ID = this.Reader[4].ToString();

                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得患者基本信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    #endregion

                    #region "项目信息"
                    //医嘱辅助信息
                    // ——项目信息
                    //	       5项目类别      6项目编码       7项目名称      8项目输入码,    9项目拼音码 
                    //	       10项目类别代码 11项目类别名称  12药品规格     13药品基本剂量  14剂量单位       
                    //         15最小单位     16包装数量,     17剂型代码     18药品类别  ,   19药品性质
                    //         20零售价       21用法代码      22用法名称     23用法英文缩写  24频次代码  
                    //         25频次名称     26每次剂量      27项目总量     28计价单位      29使用天数			  
                    // 判断药品/非药品
                    //         25频次名称     26每次剂量      27项目总量     28计价单位      29使用天数			  
                    // 73 样本类型 名称
                    if (this.Reader[5].ToString() == "1")//药品
                    {
                        FS.HISFC.Models.Pharmacy.Item objPharmacy = new FS.HISFC.Models.Pharmacy.Item();
                        try
                        {
                            objPharmacy.ID = this.Reader[6].ToString();
                            objPharmacy.Name = this.Reader[7].ToString();
                            objPharmacy.UserCode = this.Reader[8].ToString();
                            objPharmacy.SpellCode = this.Reader[9].ToString();
                            objPharmacy.SysClass.ID = this.Reader[10].ToString();
                            //objPharmacy.SysClass.Name = this.Reader[11].ToString();
                            objPharmacy.Specs = this.Reader[12].ToString();
                            //							try
                            //							{
                            if (!this.Reader.IsDBNull(13)) objPharmacy.BaseDose = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[13].ToString());
                            //}
                            //							catch{}
                            objPharmacy.DoseUnit = this.Reader[14].ToString();
                            objOrder.DoseUnit = this.Reader[14].ToString();
                            objPharmacy.MinUnit = this.Reader[15].ToString();
                            //try
                            //{
                            if (!this.Reader.IsDBNull(16)) objPharmacy.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16].ToString());
                            //}
                            //catch{}
                            objPharmacy.DosageForm.ID = this.Reader[17].ToString();
                            objPharmacy.Type.ID = this.Reader[18].ToString();
                            objPharmacy.Quality.ID = this.Reader[19].ToString();
                            // 计价单位
                            objPharmacy.PriceUnit = this.Reader[28].ToString();

                            //try
                            //{
                            if (!this.Reader.IsDBNull(20)) objPharmacy.PriceCollection.RetailPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20].ToString());
                            //}
                            //catch{}					
                            objOrder.Usage.ID = this.Reader[21].ToString();
                            objOrder.Usage.Name = this.Reader[22].ToString();
                            objOrder.Usage.Memo = this.Reader[23].ToString();


                            if (!this.Reader.IsDBNull(87))
                            {
                                objOrder.DoseOnceDisplay = this.Reader[87].ToString();
                            }
                            if (!this.Reader.IsDBNull(88))
                            {
                                objOrder.DoseUnitDisplay = this.Reader[88].ToString();
                            }

                            //药品基药属性 gumzh 2013-11-13
                            if (this.Reader.FieldCount >= 99)
                            {
                                objPharmacy.ExtendData2 = this.Reader[98].ToString();
                            }


                        }
                        catch (Exception ex)
                        {
                            this.Err = "获得医嘱项目信息出错！" + ex.Message;
                            this.WriteErr();
                            return null;
                        }
                        objOrder.Item = objPharmacy;
                    }
                    else if (this.Reader[5].ToString() == "2")//非药品
                    {
                        FS.HISFC.Models.Fee.Item.Undrug objAssets = new FS.HISFC.Models.Fee.Item.Undrug();
                        try
                        {
                            objAssets.ID = this.Reader[6].ToString();
                            objAssets.Name = this.Reader[7].ToString();
                            objAssets.UserCode = this.Reader[8].ToString();
                            objAssets.SpellCode = this.Reader[9].ToString();
                            objAssets.SysClass.ID = this.Reader[10].ToString();
                            //objAssets.SysClass.Name = this.Reader[11].ToString();
                            objAssets.Specs = this.Reader[12].ToString();
                            objOrder.Usage.ID = this.Reader[21].ToString();
                            objOrder.Usage.Name = this.Reader[22].ToString();
                            objOrder.Usage.Memo = this.Reader[23].ToString();
                            //							try
                            //							{
                            if (!this.Reader.IsDBNull(20)) objAssets.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20].ToString());
                            //}
                            //							catch{}	
                            objAssets.PriceUnit = this.Reader[28].ToString();
                            //样本类型名称
                            objOrder.Sample.Name = this.Reader[73].ToString();
                        }
                        catch (Exception ex)
                        {
                            this.Err = "获得医嘱项目信息出错！" + ex.Message;
                            this.WriteErr();
                            return null;
                        }
                        objOrder.Item = objAssets;
                    }


                    objOrder.Frequency.ID = this.Reader[24].ToString();
                    objOrder.Frequency.Name = this.Reader[25].ToString();
                    //try
                    //{
                    if (!this.Reader.IsDBNull(26)) objOrder.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26].ToString());//}
                    //catch{}
                    //try
                    //{
                    if (!this.Reader.IsDBNull(27)) objOrder.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[27].ToString());//}
                    //catch{}
                    objOrder.Unit = this.Reader[28].ToString();
                    //try
                    //{
                    if (!this.Reader.IsDBNull(29)) objOrder.HerbalQty = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[29].ToString());//}
                    //catch{}

                    #endregion

                    #region "医嘱属性"
                    // ——医嘱属性
                    //		   30医嘱类别代码 31医嘱类别名称  32医嘱是否分解:1长期/2临时     33是否计费 
                    //		   34药房是否配药 35打印执行单    36是否需要确认   
                    try
                    {
                        objOrder.ID = this.Reader[0].ToString();
                        objOrder.ExtendFlag1 = this.Reader[78].ToString();//临时医嘱执行时间－－自定义
                        objOrder.OrderType.ID = this.Reader[30].ToString();
                        objOrder.OrderType.Name = this.Reader[31].ToString();
                        //try
                        //{
                        if (!this.Reader.IsDBNull(32)) objOrder.OrderType.IsDecompose = System.Convert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[32].ToString()));//}
                        //catch{}
                        //try
                        //{
                        if (!this.Reader.IsDBNull(33)) objOrder.OrderType.IsCharge = System.Convert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[33].ToString()));//}
                        //catch{}
                        //try
                        //{
                        if (!this.Reader.IsDBNull(34)) objOrder.OrderType.IsNeedPharmacy = System.Convert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[34].ToString()));//}
                        //catch{}
                        //try
                        //{
                        if (!this.Reader.IsDBNull(35)) objOrder.OrderType.IsPrint = System.Convert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[35].ToString()));//}
                        //catch{}
                        //try
                        //{
                        if (!this.Reader.IsDBNull(36)) objOrder.OrderType.IsConfirm = System.Convert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[36].ToString()));//}
                        //catch{}
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得医嘱属性信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    #endregion

                    #region "执行情况"
                    // ——执行情况
                    //		   37开立医师Id   38开立医师name  39开始时间      40结束时间     41开立科室
                    //		   42开立时间     43录入人员代码  44录入人员姓名  45审核人ID     46审核时间       
                    //		   47DC原因代码   48DC原因名称    49DC医师代码    50DC医师姓名   51Dc时间
                    //         52执行人员代码 53执行时间      54执行科室代码  55执行科室名称 
                    //		   56本次分解时间 57下次分解时间
                    try
                    {
                        objOrder.ReciptDoctor.ID = this.Reader[37].ToString();
                        objOrder.ReciptDoctor.Name = this.Reader[38].ToString();
                        //try{
                        if (!this.Reader.IsDBNull(39)) objOrder.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[39].ToString());
                        //}
                        //catch{}
                        //try{
                        if (!this.Reader.IsDBNull(40)) objOrder.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[40].ToString());
                        //}
                        //catch{}
                        objOrder.ReciptDept.ID = this.Reader[41].ToString();//InDept.ID
                        //try{
                        if (!this.Reader.IsDBNull(42)) objOrder.MOTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[42].ToString());
                        //}
                        //catch{}
                        objOrder.Oper.ID = this.Reader[43].ToString();
                        objOrder.Oper.Name = this.Reader[44].ToString();
                        objOrder.Nurse.ID = this.Reader[45].ToString();
                        //try{
                        if (!this.Reader.IsDBNull(46)) objOrder.ConfirmTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[46].ToString());
                        //}
                        //catch{}
                        objOrder.DcReason.ID = this.Reader[47].ToString();
                        objOrder.DcReason.Name = this.Reader[48].ToString();
                        objOrder.DCOper.ID = this.Reader[49].ToString();
                        objOrder.DCOper.Name = this.Reader[50].ToString();
                        //try{
                        if (!this.Reader.IsDBNull(51)) objOrder.DCOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[51].ToString());
                        //}
                        //catch{}
                        objOrder.ExecOper.ID = this.Reader[52].ToString();
                        //try{
                        if (!this.Reader.IsDBNull(53)) objOrder.ExecOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[53].ToString());

                        objOrder.ExeDept.ID = this.Reader[54].ToString();
                        objOrder.ExeDept.Name = this.Reader[55].ToString();

                        objOrder.ExecOper.Dept.ID = this.Reader[54].ToString();
                        objOrder.ExecOper.Dept.Name = this.Reader[55].ToString();

                        if (!this.Reader.IsDBNull(56)) objOrder.CurMOTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[56].ToString());

                        if (!this.Reader.IsDBNull(57)) objOrder.NextMOTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[57].ToString());

                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得医嘱执行情况信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    #endregion

                    #region "医嘱类型"
                    // ——医嘱类型
                    //		   58是否婴儿（1是/2否）          59发生序号  	  60组合序号     61主药标记 
                    //		   62是否附材'1'  63是否包含附材  64医嘱状态      65扣库标记     66执行标记1未执行/2已执行/3DC执行 
                    //		   67医嘱说明                     68加急标记:1普通/2加急         69排列序号
                    //         70 批注       ,       71检查部位备注    ,72 整档标记,74 取药药房编码 81 是否皮试 
                    //         84 申请单号
                    try
                    {

                        if (!this.Reader.IsDBNull(58)) objOrder.IsBaby = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[58].ToString()));

                        if (!this.Reader.IsDBNull(59)) objOrder.BabyNO = this.Reader[59].ToString();

                        objOrder.Combo.ID = this.Reader[60].ToString();

                        if (!this.Reader.IsDBNull(61)) objOrder.Combo.IsMainDrug = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[61].ToString()));

                        if (!this.Reader.IsDBNull(62)) objOrder.IsSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[62].ToString()));

                        if (!this.Reader.IsDBNull(63)) objOrder.IsHaveSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[63].ToString()));

                        if (!this.Reader.IsDBNull(64)) objOrder.Status = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[64].ToString());

                        if (!this.Reader.IsDBNull(65)) objOrder.IsStock = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[65].ToString()));


                        if (!this.Reader.IsDBNull(66)) objOrder.ExecStatus = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[66].ToString());

                        objOrder.Note = this.Reader[67].ToString();

                        if (!this.Reader.IsDBNull(68)) objOrder.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[68].ToString()));

                        if (!this.Reader.IsDBNull(69)) objOrder.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[69]);

                        objOrder.Memo = this.Reader[70].ToString();
                        objOrder.CheckPartRecord = this.Reader[71].ToString();
                        objOrder.Reorder = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[72].ToString());
                        objOrder.StockDept.ID = this.Reader[74].ToString();//取药药房编码
                        try
                        {
                            if (!this.Reader.IsDBNull(75)) objOrder.IsPermission = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[75]);//患者用药知情
                        }
                        catch { }
                        objOrder.Package.ID = this.Reader[76].ToString();
                        objOrder.Package.Name = this.Reader[77].ToString();
                        objOrder.ExtendFlag2 = this.Reader[79].ToString();
                        objOrder.ReTidyInfo = this.Reader[80].ToString();
                        try
                        {
                            if (!this.Reader.IsDBNull(81))
                            {
                                objOrder.HypoTest = (FS.HISFC.Models.Order.EnumHypoTest)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[81].ToString());
                            }
                        }
                        catch
                        {
                            objOrder.HypoTest = 0;
                        }

                        objOrder.Frequency.Time = this.Reader[82].ToString(); //执行时间
                        objOrder.ExecDose = this.Reader[83].ToString();//特殊频次剂量
                        if (!this.Reader.IsDBNull(84)) objOrder.ApplyNo = this.Reader[84].ToString(); //申请单号

                        #region {16EE9720-A7C1-4f07-8262-2F0A1567C78F}
                        if (!this.Reader.IsDBNull(85))
                        {
                            objOrder.DCNurse.ID = this.Reader[85].ToString();
                        }
                        if (!this.Reader.IsDBNull(86))
                        {
                            objOrder.DCNurse.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[86].ToString());
                        }
                        #region {0D81AD2A-4F10-4fe5-9F79-5C6393384318}

                        if (!this.Reader.IsDBNull(89))
                        {
                            objOrder.FirstUseNum = this.Reader[89].ToString();
                        }

                        if (objOrder.ExtendFlag6.Length <= 0)
                            objOrder.FirstUseNum = "0";


                        #endregion
                        if (!this.Reader.IsDBNull(90))
                        {
                            objOrder.Patient.PVisit.PatientLocation.Bed.ID = this.Reader[90].ToString();
                        }
                        if (!this.Reader.IsDBNull(91))
                        {
                            objOrder.PageNo = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[91].ToString());
                        }
                        if (!this.Reader.IsDBNull(92))
                        {
                            objOrder.RowNo = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[92].ToString());
                        }

                        if (this.Reader[5].ToString() == "1")
                        {
                            if (objOrder.DoseOnceDisplay.Length <= 0)
                                objOrder.DoseOnceDisplay = objOrder.DoseOnce.ToString();

                            if (objOrder.DoseUnitDisplay.Length <= 0)
                                objOrder.DoseUnitDisplay = (objOrder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit.ToString();
                        }
                        // {C903923D-C0F7-4665-83C4-6CB8CC529155}
                        if (this.Reader.FieldCount >= 94)
                        {
                            objOrder.GetFlag = this.Reader[93].ToString();
                        }

                        // {C903923D-C0F7-4665-83C4-6CB8CC529155}
                        if (this.Reader.FieldCount >= 95)
                        {
                            objOrder.SpeOrderType = this.Reader[94].ToString();
                        }

                        #endregion

                        //组号 2011-7-3 houwb
                        if (this.Reader.FieldCount >= 96)
                        {
                            objOrder.SubCombNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[95]);
                        }

                        ////{FAA26B6C-E483-4497-B17E-74D1C6BC8FD1}。医嘱类型：1 医嘱；0 护嘱 gumzh 
                        //if (this.Reader.FieldCount >= 97)
                        //{
                        //    objOrder.OrderFormType = this.Reader[96].ToString();
                        //}

                        //最小单位标记‘1’最小单位‘0’剂量单位'
                        if (this.Reader.FieldCount >= 98)
                        {
                            objOrder.MinunitFlag = this.Reader[97].ToString();//开立时的单位
                        }

                        //药品基药属性 gumzh 2013-11-13,在第99位上是药品的"基药属性"，请参照“项目信息”

                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得医嘱类型信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    #endregion

                    al.Add(objOrder);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得医嘱信息出错！" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            this.Reader.Close();
            return al;
        }

        /// <summary>
        /// 根据住院流水号和发票号查询所有医嘱
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="invoiceNO"></param>
        /// <returns></returns>
        public ArrayList QueryOrderByInvoiceNo(string inpatientNO, string invoiceNO)
        {

            string sql = this.OrderQuerySelect();
            if (string.IsNullOrEmpty(sql))
            {
                return null;
            }
            //WHERE语句
            string sqlWhere = @"
                                WHERE INPATIENT_NO =  '{0}'
                                AND MO_ORDER IN (

                                SELECT f.MO_ORDER FROM FIN_IPB_ITEMLIST f
                                WHERE f.INPATIENT_NO = '{0}'
                                AND f.INVOICE_NO = '{1}'
                                GROUP BY f.MO_ORDER 
                                HAVING sum(f.TOT_COST) > 0 

                                UNION ALL 

                                SELECT r.MO_ORDER FROM FIN_IPB_MEDICINELIST r
                                WHERE r.INPATIENT_NO = '{0}'
                                AND r.INVOICE_NO = '{1}'
                                GROUP BY r.MO_ORDER 
                                HAVING sum(r.TOT_COST) > 0
                                )
                                ";

            sql = sql + " " + string.Format(sqlWhere, inpatientNO, invoiceNO);

            return this.MyOrderQuery(sql);
        }

        #endregion

        #region 住院医嘱执行信息

        /// <summary>
        /// 私有函数，查询医嘱执行信息
        /// </summary>
        /// <param name="SQLPatient"></param>
        /// <returns></returns>
        private ArrayList myExecOrderQuery(string SQLPatient)
        {

            ArrayList al = new ArrayList();

            if (this.ExecQuery(SQLPatient) == -1) return null;
            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Order.ExecOrder objOrder = new FS.HISFC.Models.Order.ExecOrder();

                    #region "患者信息"
                    //患者信息——  
                    //			1 住院流水号   2住院病历号     3患者科室id      4患者护理id
                    try
                    {
                        objOrder.Order.Patient.ID = this.Reader[1].ToString();
                        objOrder.Order.Patient.PID.PatientNO = this.Reader[2].ToString();
                        objOrder.Order.Patient.PVisit.PatientLocation.Dept.ID = this.Reader[3].ToString();
                        objOrder.Order.Patient.PVisit.PatientLocation.NurseCell.ID = this.Reader[4].ToString();
                        objOrder.Order.NurseStation.ID = this.Reader[4].ToString();
                        objOrder.Order.InDept.ID = this.Reader[3].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得患者基本信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    #endregion

                    if (this.Reader[5].ToString() == "1")//药品
                    {
                        FS.HISFC.Models.Pharmacy.Item objPharmacy = new FS.HISFC.Models.Pharmacy.Item();
                        try
                        {
                            #region "项目信息"
                            //医嘱辅助信息
                            // ——项目信息
                            //	       5项目类别      6项目编码       7项目名称      8项目输入码,    9项目拼音码 
                            //	       10项目类别代码 11项目类别名称  12药品规格     13药品基本剂量  14剂量单位       
                            //         15最小单位     16包装数量,     17剂型代码     18药品类别  ,   19药品性质
                            //         20零售价       21用法代码      22用法名称     23用法英文缩写  24频次代码  
                            //         25频次名称     26每次剂量      27项目总量     28计价单位      29使用天数			
                            objPharmacy.ID = this.Reader[6].ToString();
                            objPharmacy.Name = this.Reader[7].ToString();
                            objPharmacy.UserCode = this.Reader[8].ToString();
                            objPharmacy.SpellCode = this.Reader[9].ToString();
                            objPharmacy.SysClass.ID = this.Reader[10].ToString();
                            //objPharmacy.SysClass.Name = this.Reader[11].ToString();
                            objPharmacy.Specs = this.Reader[12].ToString();
                            //try
                            //{
                            objPharmacy.BaseDose = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[13]);
                            //}
                            //catch{}
                            objPharmacy.DoseUnit = this.Reader[14].ToString();
                            objPharmacy.MinUnit = this.Reader[15].ToString();
                            //try
                            //{
                            objPharmacy.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16]);
                            //}
                            //catch{}
                            objPharmacy.DosageForm.ID = this.Reader[17].ToString();
                            objPharmacy.Type.ID = this.Reader[18].ToString();
                            objPharmacy.Quality.ID = this.Reader[19].ToString();
                            //try
                            //{
                            objPharmacy.PriceCollection.RetailPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20]);
                            //}
                            //catch{}	
                            objOrder.Order.Item = objPharmacy;
                            #endregion

                            objOrder.Order.Usage.ID = this.Reader[21].ToString();
                            objOrder.Order.Usage.Name = this.Reader[22].ToString();
                            objOrder.Order.Usage.Memo = this.Reader[23].ToString();
                            objOrder.Order.Frequency.ID = this.Reader[24].ToString();
                            objOrder.Order.Frequency.Name = this.Reader[25].ToString();
                            //try
                            //{
                            objOrder.Order.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26]);
                            //}
                            //catch{}
                            objOrder.Order.DoseUnit = objPharmacy.DoseUnit;
                            //try
                            //{
                            objOrder.Order.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[27]);
                            //}
                            //catch{}
                            objOrder.Order.Unit = this.Reader[28].ToString();
                            //保存包装单位，药房那里要用到 houwb 2011-3-11{237771BE-195A-48ed-9BB9-AF66776A56C3}
                            //医嘱的单位不一定是包装单位
                            //objPharmacy.PackUnit = objOrder.Order.Unit;
                            //try
                            //{
                            objOrder.Order.HerbalQty = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[29]);
                            //}
                            //catch{}
                        }
                        catch (Exception ex)
                        {
                            this.Err = "获得医嘱项目信息出错！" + ex.Message;
                            this.WriteErr();
                            return null;
                        }
                        //objOrder.Order.Item = objPharmacy;

                        #region "医嘱属性"
                        // ——医嘱属性
                        //		  30医嘱流水号 , 31医嘱类别代码  32医嘱是否分解:1长期/2临时     33是否计费 
                        //		   34药房是否配药 35打印执行单    36是否需要确认  
                        try
                        {
                            objOrder.ID = this.Reader[0].ToString();
                            objOrder.Order.ID = this.Reader[30].ToString();
                            objOrder.Order.OrderType.ID = this.Reader[31].ToString();
                            //try
                            //{
                            objOrder.Order.OrderType.IsDecompose = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[32]);
                            //}
                            //catch{}
                            //try
                            //{
                            objOrder.Order.OrderType.IsCharge = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[33]);
                            //}
                            //catch{}
                            //try
                            //{
                            objOrder.Order.OrderType.IsNeedPharmacy = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[34]);
                            //}
                            //catch{}
                            //try
                            //{
                            objOrder.Order.OrderType.IsPrint = FS.FrameWork.Function.NConvert.ToBoolean(Reader[35]);
                            //}
                            //catch{}
                            //try
                            //{
                            objOrder.Order.OrderType.IsConfirm = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[36]);
                            //}
                            //catch{}
                        }
                        catch (Exception ex)
                        {
                            this.Err = "获得医嘱属性信息出错！" + ex.Message;
                            this.WriteErr();
                            return null;
                        }
                        #endregion
                        #region "执行情况"
                        // ——执行情况
                        //		   37开立医师Id   38开立医师name  39要求执行时间  40作废时间     41开立科室
                        //		   42开立时间     43作废人代码    44记账人代码    45记账科室代码 46记账时间       
                        //		   47取药药房     48执行科室      49执行护士代码  50执行科室代码 51执行时间
                        //         52分解时间
                        try
                        {
                            objOrder.Order.ReciptDoctor.ID = this.Reader[37].ToString();
                            objOrder.Order.ReciptDoctor.Name = this.Reader[38].ToString();
                            //try{
                            objOrder.DateUse = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[39]);
                            //}
                            //catch{}
                            //try{
                            objOrder.DCExecOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[40]);
                            //}
                            //catch{}
                            objOrder.Order.ReciptDept.ID = this.Reader[41].ToString();
                            //try{
                            objOrder.Order.MOTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[42]);
                            //}
                            //catch{}
                            objOrder.DCExecOper.ID = this.Reader[43].ToString();
                            objOrder.ChargeOper.ID = this.Reader[44].ToString();
                            objOrder.ChargeOper.Dept.ID = this.Reader[45].ToString();
                            //try{
                            objOrder.ChargeOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[46]);
                            //}
                            //catch{}
                            objOrder.Order.StockDept.ID = this.Reader[47].ToString();
                            objOrder.Order.ExeDept.ID = this.Reader[48].ToString();
                            objOrder.ExecOper.ID = this.Reader[49].ToString();
                            //try
                            //{
                            if (this.Reader[50].ToString() != "")
                                objOrder.Order.ExeDept.ID = this.Reader[50].ToString();
                            //}
                            //catch{}
                            //try{
                            objOrder.ExecOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[51]);
                            //}
                            //catch{}
                            objOrder.DateDeco = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[52]);

                            if (!Reader.IsDBNull(68))
                            {
                                objOrder.DrugedTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[68].ToString());
                            }

                        }
                        catch (Exception ex)
                        {
                            this.Err = "获得医嘱执行情况信息出错！" + ex.Message;
                            this.WriteErr();
                            return null;
                        }
                        #endregion
                        #region "医嘱类型"
                        // ——医嘱类型
                        //		   64是否婴儿（1是/2否）          53发生序号  	  54组合序号     55主药标记 
                        //		   56是否包含附材                 57是否有效      58扣库标记     59是否执行 
                        //		   60配药标记                     61收费标记      62医嘱说明     63备注
                        //         65处方号                       66处方序号
                        try
                        {
                            //try{
                            objOrder.Order.IsBaby = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[53]);
                            //}
                            //catch{}
                            //try{
                            objOrder.Order.BabyNO = this.Reader[54].ToString();
                            //}
                            //catch{}
                            objOrder.Order.Combo.ID = this.Reader[55].ToString();
                            //try{
                            objOrder.Order.Combo.IsMainDrug = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[56]);
                            //}
                            //catch{}
                            //try{
                            objOrder.Order.IsHaveSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[57]);
                            //}
                            //catch{}
                            //try{
                            objOrder.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[58]);
                            //}
                            //catch{}
                            //try{
                            objOrder.Order.IsStock = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[59]);
                            //}
                            //catch{}
                            //try{
                            objOrder.IsExec = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[60]);
                            //}
                            //catch{}
                            //try{
                            objOrder.DrugFlag = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[61]);
                            //}
                            //catch{}
                            //try{
                            objOrder.IsCharge = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[62]);
                            //}
                            //catch{}
                            objOrder.Order.Note = this.Reader[63].ToString();
                            objOrder.Order.Memo = this.Reader[64].ToString();
                            objOrder.Order.ReciptNO = this.Reader[65].ToString();
                            //try{
                            objOrder.Order.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[66]);
                            //}
                            //catch{}
                            if (this.Reader.FieldCount >= 70)
                            {
                                objOrder.Order.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[69].ToString());
                            }
                            if (this.Reader.FieldCount >= 71)
                            {
                                objOrder.Order.SpeOrderType = this.Reader[70].ToString();
                            }
                            if (this.Reader.FieldCount >= 72)
                            {
                                objOrder.Order.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[71].ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            this.Err = "获得医嘱类型信息出错！" + ex.Message;
                            this.WriteErr();
                            return null;
                        }
                        #endregion
                    }
                    else if (this.Reader[5].ToString() == "2")//非药品
                    {
                        FS.HISFC.Models.Fee.Item.Undrug objAssets = new FS.HISFC.Models.Fee.Item.Undrug();
                        try
                        {
                            #region "项目信息"
                            // ——项目信息
                            //	       5项目类别      6项目编码       7项目名称      8项目输入码,    9项目拼音码 
                            //	       10项目类别代码 11项目类别名称  12规格         13零售价        14用法代码   
                            //         15用法名称     16用法英文缩写  17频次代码     18频次名称      19每次用量
                            //         20项目总量     21计价单位      22使用次数	
                            objAssets.ID = this.Reader[6].ToString();
                            objAssets.Name = this.Reader[7].ToString();
                            objAssets.UserCode = this.Reader[8].ToString();
                            objAssets.SpellCode = this.Reader[9].ToString();
                            objAssets.SysClass.ID = this.Reader[10].ToString();
                            //objAssets.SysClass.Name = this.Reader[11].ToString();
                            objAssets.Specs = this.Reader[12].ToString();
                            //try
                            //{
                            objAssets.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[13].ToString());
                            //}
                            //catch{}	
                            objAssets.PriceUnit = this.Reader[21].ToString();
                            objOrder.Order.Item = objAssets;
                            #endregion

                            objOrder.Order.Usage.ID = this.Reader[14].ToString();
                            objOrder.Order.Usage.Name = this.Reader[15].ToString();
                            objOrder.Order.Usage.Memo = this.Reader[16].ToString();
                            objOrder.Order.Frequency.ID = this.Reader[17].ToString();
                            objOrder.Order.Frequency.Name = this.Reader[18].ToString();
                            //try
                            //{
                            objOrder.Order.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[19]);
                            //}
                            //catch{}
                            //try
                            //{
                            objOrder.Order.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20]);
                            //}
                            //catch{}
                            objOrder.Order.Unit = this.Reader[21].ToString();
                            objOrder.Order.DoseUnit = objOrder.Order.Unit;
                            //try
                            //{
                            objOrder.Order.HerbalQty = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[22]);
                            //}
                            //catch{}
                        }
                        catch (Exception ex)
                        {
                            this.Err = "获得医嘱项目信息出错！" + ex.Message;
                            this.WriteErr();
                            return null;
                        }

                        #region "医嘱属性"
                        // ——医嘱属性
                        //		   23医嘱类别代码 24医嘱流水号    25医嘱是否分解:1长期/2临时     26是否计费 
                        //		   27药房是否配药 28打印执行单    29是否需要确认    
                        try
                        {
                            objOrder.ID = this.Reader[0].ToString();
                            objOrder.Order.OrderType.ID = this.Reader[23].ToString();
                            objOrder.Order.ID = this.Reader[24].ToString();
                            //							try
                            //							{
                            objOrder.Order.OrderType.IsDecompose = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[25]);
                            //}
                            //catch{}
                            //try
                            //{
                            objOrder.Order.OrderType.IsCharge = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[26]);
                            //}
                            //catch{}
                            //try
                            //{
                            objOrder.Order.OrderType.IsNeedPharmacy = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[27]);
                            //}
                            //catch{}
                            //try
                            //{
                            objOrder.Order.OrderType.IsPrint = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[28]);
                            //}
                            //catch{}
                            //try
                            //{
                            objOrder.Order.OrderType.IsConfirm = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[29]);
                            //}
                            //catch{}
                        }
                        catch (Exception ex)
                        {
                            this.Err = "获得医嘱属性信息出错！" + ex.Message;
                            this.WriteErr();
                            return null;
                        }
                        #endregion
                        #region "执行情况"
                        // ——执行情况
                        //		   30开立医师Id   31开立医师name  32要求执行时间  33作废时间     34开立科室
                        //		   35开立时间     36作废人代码    37记账人代码    38记账科室代码 39记账时间       
                        //		   40取药药房     41执行科室      42执行护士代码  43执行科室代码 44执行时间
                        //         45分解时间     46执行科室名称
                        try
                        {
                            objOrder.Order.ReciptDoctor.ID = this.Reader[30].ToString();
                            objOrder.Order.ReciptDoctor.Name = this.Reader[31].ToString();
                            //try{
                            objOrder.DateUse = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[32]);
                            //}
                            //catch{}
                            //try{
                            objOrder.DCExecOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[33]);
                            //}
                            //catch{}
                            objOrder.Order.ReciptDept.ID = this.Reader[34].ToString();
                            //try{
                            objOrder.Order.MOTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[35]);
                            //}
                            //catch{}
                            objOrder.DCExecOper.ID = this.Reader[36].ToString();
                            objOrder.ChargeOper.ID = this.Reader[37].ToString();
                            objOrder.ChargeOper.Dept.ID = this.Reader[38].ToString();
                            //try{
                            objOrder.ChargeOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[39]);
                            //}
                            //catch{}
                            objOrder.Order.StockDept.ID = this.Reader[40].ToString();
                            objOrder.Order.ExeDept.ID = this.Reader[41].ToString();//执行科室用项目执行科室
                            objOrder.ExecOper.ID = this.Reader[42].ToString();
                            //objOrder.ExeDept.ID = this.Reader[43].ToString();//这个字段就是没用的
                            //try{
                            objOrder.ExecOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[44]);
                            //}
                            //catch{}
                            objOrder.DateDeco = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[45]);
                            objOrder.Order.ExeDept.Name = this.Reader[46].ToString();

                            if (this.Reader.FieldCount >= 66)
                            {
                                //65、医嘱开始时间
                                objOrder.Order.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[65].ToString());
                            }
                            if (this.Reader.FieldCount >= 67)
                            {
                                //66、特殊医嘱标识 手术医嘱等
                                objOrder.Order.SpeOrderType = this.Reader[66].ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            this.Err = "获得医嘱执行情况信息出错！" + ex.Message;
                            this.WriteErr();
                            return null;
                        }
                        #endregion
                        #region "医嘱类型"
                        // ——医嘱类型
                        //		   47是否婴儿（1是/2否）          48发生序号  	  49组合序号     50主项标记 
                        //		   51是否附材                     52是否包含附材  53是否有效     54是否执行 
                        //		   55收费标记     56加急标记      57检查部位检体  58医嘱说明     59备注 
                        //         60处方号                       61处方序号      62配药科室ID
                        try
                        {
                            //try{
                            objOrder.Order.IsBaby = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[47]);
                            //}
                            //catch{}
                            //try{
                            objOrder.Order.BabyNO = this.Reader[48].ToString();
                            //}
                            //catch{}
                            objOrder.Order.Combo.ID = this.Reader[49].ToString();
                            //try{
                            objOrder.Order.Combo.IsMainDrug = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[50]);
                            //}
                            //catch{}
                            //try{
                            objOrder.Order.IsSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[51]);
                            //}
                            //catch{}
                            //try{
                            objOrder.Order.IsHaveSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[52]);
                            //}
                            //catch{}
                            //try{
                            objOrder.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[53]);
                            //}
                            //catch{}
                            //try{
                            objOrder.IsExec = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[54]);
                            //}
                            //catch{}
                            //try{
                            objOrder.IsCharge = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[55]);
                            //}
                            //catch{}
                            //try{
                            objOrder.Order.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[56]);
                            //}
                            //catch{}
                            objOrder.Order.CheckPartRecord = this.Reader[57].ToString();

                            objOrder.Order.Note = this.Reader[58].ToString();
                            objOrder.Order.Memo = this.Reader[59].ToString();
                            objOrder.Order.ReciptNO = this.Reader[60].ToString();
                            //try{
                            objOrder.Order.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[61]);
                            //}
                            //catch{}
                            objOrder.Order.StockDept.ID = this.Reader[62].ToString();

                            try
                            {
                                objOrder.Order.Sample.Name = this.Reader[63].ToString();			//样本类型
                                objOrder.Order.Sample.Memo = this.Reader[64].ToString();			//检验条码号
                            }
                            catch { }



                        }
                        catch (Exception ex)
                        {
                            this.Err = "获得医嘱类型信息出错！" + ex.Message;
                            this.WriteErr();
                            return null;
                        }
                        #endregion
                    }
                    al.Add(objOrder);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得医嘱信息出错！" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        /// <summary>
        /// 查询患者医嘱【非药品】执行信息的select语句（无where条件）
        /// </summary>
        /// <returns></returns>
        private string ExecUndrugSelect()
        {
            string sql = "";
            if (this.Sql.GetSql("Order.ExecOrder.QueryOrder.Select.2", ref sql) == -1)
            {
                this.Err = "没有找到Order.ExecOrder.QueryOrder.Select.2字段!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            return sql;
        }

        /// <summary>
        /// 根据住院流水号和发票号查询所有医嘱【非药品】的执行信息
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="invoiceNO"></param>
        /// <returns></returns>
        public ArrayList QueryExecUndrugByInvoiceNo(string inpatientNO, string invoiceNO)
        {
            string sql = this.ExecUndrugSelect();
            if (string.IsNullOrEmpty(sql))
            {
                return null;
            }

            //WHERE语句
            string sqlWhere = @"
                                WHERE INPATIENT_NO =  '{0}'
                                AND EXEC_SQN IN (
                                SELECT f.MO_EXEC_SQN FROM FIN_IPB_ITEMLIST f
                                WHERE f.INPATIENT_NO = '{0}'
                                AND f.INVOICE_NO = '{1}'
                                GROUP BY f.MO_EXEC_SQN 
                                HAVING sum(f.TOT_COST) > 0 
                                )
                                ";

            sql = sql + " " + string.Format(sqlWhere, inpatientNO, invoiceNO);

            return this.myExecOrderQuery(sql);
        }




        /// <summary>
        /// 查询患者医嘱【药品】执行信息的select语句（无where条件）
        /// </summary>
        /// <returns></returns>
        private string ExecDrugSelect()
        {
            string sql = "";
            if (this.Sql.GetSql("Order.ExecOrder.QueryOrder.Select.1", ref sql) == -1)
            {
                this.Err = "没有找到Order.ExecOrder.QueryOrder.Select.1字段!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            return sql;
        }

        /// <summary>
        /// 根据住院流水号和发票号查询所有医嘱【药品】的执行信息
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="invoiceNO"></param>
        /// <returns></returns>
        public ArrayList QueryExecDrugByInvoiceNo(string inpatientNO, string invoiceNO)
        {
            string sql = this.ExecDrugSelect();
            if (string.IsNullOrEmpty(sql))
            {
                return null;
            }

            //WHERE语句
            string sqlWhere = @"
                                WHERE INPATIENT_NO =  '{0}'
                                AND EXEC_SQN IN (
                                SELECT f.MO_EXEC_SQN FROM FIN_IPB_MEDICINELIST f
                                WHERE f.INPATIENT_NO = '{0}'
                                AND f.INVOICE_NO = '{1}'
                                GROUP BY f.MO_EXEC_SQN 
                                HAVING sum(f.TOT_COST) > 0 
                                )
                                ";

            sql = sql + " " + string.Format(sqlWhere, inpatientNO, invoiceNO);

            return this.myExecOrderQuery(sql);
        }

        #endregion

        /// <summary>
        /// 查询待上传的住院患者
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public DataTable QueryNeedUploadDetail(string dtBegin, string dtEnd, string patientNo)
        {
            // {B7DA1304-1FCB-4c01-B96A-1BEABD23F79C}
            string sql = @"SELECT '0' 选择,
                                  '2' 类别,
                                   r.PATIENT_NO 病历号,
                                   r.NAME 姓名,
                                   r.IDENNO 证件号,
                                   decode(r.SEX_CODE,'F','女','男') 性别,
                                   (SELECT f.PACT_NAME FROM FIN_COM_PACTUNITINFO f WHERE f.PACT_CODE = t.PACT_CODE) 结算类型,
                                   t.INVOICE_NO 发票电脑号,
                                   '' 发票印刷号,
                                   t.TOT_COST 总金额,
                                   t.PUB_COST 统筹金额,
                                   (t.OWN_COST+t.PAY_COST) 自费金额,
                                   FUN_GET_EMPLOYEE_NAME(t.oper_code) 结算员,
                                   t.oper_date 结算日期,
                                   t.INPATIENT_NO,
                                   t.PACT_CODE,
                                 (SELECT f.upload_type
                                      FROM FS_UPLOAD_DISEASEPAY f
                                     WHERE f.PATIENT_TYPE = '2'
                                       AND f.INVOICE_NO = t.INVOICE_NO
                                       AND f.CLINIC_CODE = t.INPATIENT_NO
                                       and rownum = 1) upload_type,
                                   (select g.err
                                      from upload_err_log g
                                     where g.inpatient_no = r.inpatient_no
                                       and g.invoice_no = t.invoice_no
                                       and g.oper_date =
                                           (select max(a.oper_date)
                                              from upload_err_log a
                                             where a.inpatient_no = g.inpatient_no
                                               and a.invoice_no = g.invoice_no)
                                       and rownum = 1) err
                            FROM FIN_IPR_SIINMAININFO t,FIN_IPR_INMAININFO r
                            WHERE t.INPATIENT_NO = r.INPATIENT_NO 
                            AND t.PACT_CODE IN (SELECT c.PACT_CODE FROM FIN_COM_PACTUNITINFO c WHERE c.DLL_NAME = '{2}')
                            AND t.oper_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                            AND t.oper_date <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                            and t.balance_no = (select max(tt.balance_no) from FIN_IPR_SIINMAININFO tt
                            where t.inpatient_no = tt.inpatient_no and tt.valid_flag = '1')
                            AND NOT EXISTS(
                                SELECT 1 FROM FS_UPLOAD_DISEASEPAY f
                                WHERE f.PATIENT_TYPE = '2'
                                AND f.INVOICE_NO = t.INVOICE_NO
                                AND f.CLINIC_CODE = t.INPATIENT_NO
                                and f.upload_type = '3')
                             and r.dept_name not like '%家%床%'
                            and t.valid_flag = '1'
                            and (r.PATIENT_NO = '{3}' or '{3}' = 'ALL')
                            ";
            // {385E49F2-947B-43e3-931D-BE89481BA68C}
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
        /// 查询待上传的住院重症患者
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public DataTable QueryNeedUploaSevereCasedDetail(string dtBegin, string dtEnd)
        {

            string sql = @"SELECT '0' 选择,
                                   '2' 类别,
                                   r.PATIENT_NO 病历号,
                                   r.NAME 姓名,
                                   r.IDENNO 证件号,
                                   decode(r.SEX_CODE, 'F', '女', '男') 性别,
                                   (SELECT f.PACT_NAME
                                      FROM FIN_COM_PACTUNITINFO f
                                     WHERE f.PACT_CODE = t.PACT_CODE) 结算类型,
                                   t.INVOICE_NO 发票电脑号,
                                   t.PRINT_INVOICENO 发票印刷号,
                                   t.TOT_COST 总金额,
                                   t.PUB_COST 统筹金额,
                                   (t.OWN_COST + t.PAY_COST) 自费金额,
                                   FUN_GET_EMPLOYEE_NAME(t.BALANCE_OPERCODE) 结算员,
                                   t.BALANCE_DATE 结算日期,
                                   t.INPATIENT_NO,
                                   t.PACT_CODE,
                                    (SELECT f.upload_type
                                      FROM FS_UPLOAD_DISEASEPAY f
                                     WHERE f.PATIENT_TYPE = '2'
                                       AND f.INVOICE_NO = t.INVOICE_NO
                                       AND f.CLINIC_CODE = t.INPATIENT_NO
                                       and rownum = 1) upload_type,
                                   (select g.err
                                      from upload_err_log g
                                     where g.inpatient_no = r.inpatient_no
                                       and g.invoice_no = t.invoice_no
                                       and g.oper_date =
                                           (select max(a.oper_date)
                                              from upload_err_log a
                                             where a.inpatient_no = g.inpatient_no
                                               and a.invoice_no = g.invoice_no)) err
                              FROM FIN_IPB_BALANCEHEAD t, FIN_IPR_INMAININFO r,
                              (select i.inpatient_no
                              from DIAG_DIAGNOSE             diagnose0_,
                                   DIAG_TRADITIONAL_DIAGNOSE diagnose0_1_,
                                   DIAG_WESTERN_DIAGNOSE     diagnose0_2_,
                                   DIAG_SI_DIAGNOSE          diagnose0_3_,
                                   pt_inpatient_cure         b,
                                   diag_creation             c,
                                   DXT_WESTERN_DISEASE       e,
                                   fin_ipr_inmaininfo        i
                             where diagnose0_.ID = diagnose0_1_.ID(+)
                               and diagnose0_.ID = diagnose0_2_.ID(+)
                               and diagnose0_.ID = diagnose0_3_.ID(+)
                               and diagnose0_.VALID_STATE = 1
                               and b.id = diagnose0_.VISIT_ID
                               and b.patient_id = diagnose0_.PATIENT_ID
                               and b.patient_id = c.patient_id
                               and diagnose0_.DIAG_CREATION_ID = c.id
                               and diagnose0_2_.disease_id = e.id
                               and b.inpatient_code = i.inpatient_no
                               and c.diagnose_type_name = '出院诊断'
                               and diagnose0_.is_main_diagnose = 1
                               and e.icd10_code in (select y.code from com_dictionary y where y.type = 'NeedReminterDiagnoseICD')) cc
                             WHERE t.INPATIENT_NO = r.INPATIENT_NO
                               and cc.inpatient_no =  r.INPATIENT_NO
                               AND t.WASTE_FLAG = '1'
                               AND t.PACT_CODE IN (SELECT c.PACT_CODE
                                                     FROM FIN_COM_PACTUNITINFO c
                                                    WHERE c.DLL_NAME = '{2}')
                               AND t.BALANCE_DATE >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                               AND t.BALANCE_DATE <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                               AND NOT EXISTS (SELECT 1
                                      FROM FS_UPLOAD_DISEASEPAY f
                                     WHERE f.PATIENT_TYPE = '2'
                                       AND f.INVOICE_NO = t.INVOICE_NO
                                       AND f.CLINIC_CODE = t.INPATIENT_NO
                                    and f.upload_type = '3')
                               and r.dept_name not like '%家%床%'

                            ";
            // {385E49F2-947B-43e3-931D-BE89481BA68C}
            //佛山医保合同单位
            string strDllName = "FoShanSI.dll";
            if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
            {
                strDllName = "FoSiFoShanSI.dll";
            }

            sql = string.Format(sql, dtBegin, dtEnd, strDllName);
            DataSet dsSet = null;
            if (this.ExecQuery(sql, ref dsSet) == -1)
            {
                return null;
            }
            return dsSet.Tables[0];
        } 
        /// <summary>
        /// 查询待上传的住院非重症患者
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public DataTable QueryNeedUploaNoSevereCasedDetail(string dtBegin, string dtEnd)
        {

            string sql = @"SELECT '0' 选择,
                                   '2' 类别,
                                   r.PATIENT_NO 病历号,
                                   r.NAME 姓名,
                                   r.IDENNO 证件号,
                                   decode(r.SEX_CODE, 'F', '女', '男') 性别,
                                   (SELECT f.PACT_NAME
                                      FROM FIN_COM_PACTUNITINFO f
                                     WHERE f.PACT_CODE = t.PACT_CODE) 结算类型,
                                   t.INVOICE_NO 发票电脑号,
                                   t.PRINT_INVOICENO 发票印刷号,
                                   t.TOT_COST 总金额,
                                   t.PUB_COST 统筹金额,
                                   (t.OWN_COST + t.PAY_COST) 自费金额,
                                   FUN_GET_EMPLOYEE_NAME(t.BALANCE_OPERCODE) 结算员,
                                   t.BALANCE_DATE 结算日期,
                                   t.INPATIENT_NO,
                                   t.PACT_CODE,
                                    (SELECT f.upload_type
                                      FROM FS_UPLOAD_DISEASEPAY f
                                     WHERE f.PATIENT_TYPE = '2'
                                       AND f.INVOICE_NO = t.INVOICE_NO
                                       AND f.CLINIC_CODE = t.INPATIENT_NO
                                       and rownum = 1) upload_type,
                                   (select g.err
                                      from upload_err_log g
                                     where g.inpatient_no = r.inpatient_no
                                       and g.invoice_no = t.invoice_no
                                       and g.oper_date =
                                           (select max(a.oper_date)
                                              from upload_err_log a
                                             where a.inpatient_no = g.inpatient_no
                                               and a.invoice_no = g.invoice_no)) err
                              FROM FIN_IPB_BALANCEHEAD t, FIN_IPR_INMAININFO r,
                              (select i.inpatient_no
                              from DIAG_DIAGNOSE             diagnose0_,
                                   DIAG_TRADITIONAL_DIAGNOSE diagnose0_1_,
                                   DIAG_WESTERN_DIAGNOSE     diagnose0_2_,
                                   DIAG_SI_DIAGNOSE          diagnose0_3_,
                                   pt_inpatient_cure         b,
                                   diag_creation             c,
                                   DXT_WESTERN_DISEASE       e,
                                   fin_ipr_inmaininfo        i
                             where diagnose0_.ID = diagnose0_1_.ID(+)
                               and diagnose0_.ID = diagnose0_2_.ID(+)
                               and diagnose0_.ID = diagnose0_3_.ID(+)
                               and diagnose0_.VALID_STATE = 1
                               and b.id = diagnose0_.VISIT_ID
                               and b.patient_id = diagnose0_.PATIENT_ID
                               and b.patient_id = c.patient_id
                               and diagnose0_.DIAG_CREATION_ID = c.id
                               and diagnose0_2_.disease_id = e.id
                               and b.inpatient_code = i.inpatient_no
                               and c.diagnose_type_name = '出院诊断'
                               and diagnose0_.is_main_diagnose = 1
                               and e.icd10_code not in (select y.code from com_dictionary y where y.type = 'NeedReminterDiagnoseICD')) cc
                             WHERE t.INPATIENT_NO = r.INPATIENT_NO
                               and cc.inpatient_no =  r.INPATIENT_NO
                               AND t.WASTE_FLAG = '1'
                               AND t.PACT_CODE IN (SELECT c.PACT_CODE
                                                     FROM FIN_COM_PACTUNITINFO c
                                                    WHERE c.DLL_NAME = '{2}')
                               AND t.BALANCE_DATE >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                               AND t.BALANCE_DATE <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                               AND NOT EXISTS (SELECT 1
                                      FROM FS_UPLOAD_DISEASEPAY f
                                     WHERE f.PATIENT_TYPE = '2'
                                       AND f.INVOICE_NO = t.INVOICE_NO
                                       AND f.CLINIC_CODE = t.INPATIENT_NO
                                        and f.upload_type = '3')
                               and r.dept_name not like '%家%床%'

                            ";
            // {385E49F2-947B-43e3-931D-BE89481BA68C}
            //佛山医保合同单位
            string strDllName = "FoShanSI.dll";
            if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
            {
                strDllName = "FoSiFoShanSI.dll";
            }

            sql = string.Format(sql, dtBegin, dtEnd, strDllName);
            DataSet dsSet = null;
            if (this.ExecQuery(sql, ref dsSet) == -1)
            {
                return null;
            }
            return dsSet.Tables[0];
        }

        /// <summary>
        /// 查询已上传的住院患者
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public DataTable QueryHaveUploadedDetail(string dtBegin, string dtEnd, string patientNo)
        {

            string sql = @"SELECT '1' 选择,
                                   '2' 类别,
                                   r.PATIENT_NO 病历号,
                                   r.NAME 姓名,
                                   r.IDENNO 证件号,
                                   decode(r.SEX_CODE,'F','女','男') 性别,
                                   (SELECT f.PACT_NAME FROM FIN_COM_PACTUNITINFO f WHERE f.PACT_CODE = t.PACT_CODE) 结算类型,
                                   t.INVOICE_NO 发票电脑号,
                                   '' 发票印刷号,
                                   t.TOT_COST 总金额,
                                   t.PUB_COST 统筹金额,
                                   (t.OWN_COST+t.PAY_COST) 自费金额,
                                    FUN_GET_EMPLOYEE_NAME(t.oper_code) 结算员,
                                   t.oper_date 结算日期,
                                   t.INPATIENT_NO,
                                   t.PACT_CODE,
                                   FUN_GET_EMPLOYEE_NAME(fs.OPER_CODE) ,
                                   fs.OPER_DATE,
                                   fs.SFDJH,
                                   fs.WYLSH,
                                   fs.upload_type
                                   
                            FROM FIN_IPR_SIINMAININFO t,FIN_IPR_INMAININFO r,FS_UPLOAD_DISEASEPAY fs
                            WHERE t.INPATIENT_NO = r.INPATIENT_NO 
                            AND t.PACT_CODE IN (SELECT c.PACT_CODE FROM FIN_COM_PACTUNITINFO c WHERE c.DLL_NAME = '{2}')
                            and t.balance_no = (select max(tt.balance_no) from FIN_IPR_SIINMAININFO tt
                            where t.inpatient_no = tt.inpatient_no and tt.valid_flag = '1')
                            AND t.oper_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                            AND t.oper_date <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                            AND fs.PATIENT_TYPE = '2'
                            AND t.INVOICE_NO = fs.INVOICE_NO
                            AND EXISTS(
                                SELECT 1 FROM FS_UPLOAD_DISEASEPAY f
                                WHERE f.PATIENT_TYPE = '2'
                                AND f.INVOICE_NO = t.INVOICE_NO
                                AND f.CLINIC_CODE = t.INPATIENT_NO
                                )
                            and (r.PATIENT_NO = '{3}' or '{3}' = 'ALL')
                           order by t.BALANCE_DATE
                            ";

            //佛山医保合同单位
            string strDllName = "FoShanSI.dll";

            sql = string.Format(sql, dtBegin, dtEnd, strDllName, patientNo);
            DataSet dsSet = null;
            if (this.ExecQuery(sql, ref dsSet) == -1)
            {
                return null;
            }
            return dsSet.Tables[0];
        }

        #region 上传日志

        /// <summary>
        /// 保存上传日志
        /// </summary>
        /// <param name="patientType"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="patientNo"></param>
        /// <param name="clinicCode"></param>
        /// <param name="idenNo"></param>
        /// <param name="sfdjh"></param>
        /// <param name="wylsh"></param>
        /// <param name="operCode"></param>
        /// <returns></returns>
        public int SaveLog(string patientType, string invoiceNo, string patientNo, string clinicCode, string idenNo, string sfdjh, string wylsh, string operCode,string Uptype)
        {
            #region SQL

            string strSQL = @"INSERT INTO FS_UPLOAD_DISEASEPAY
	                        (
	                        PATIENT_TYPE,
	                        INVOICE_NO,
	                        PATIENT_NO,
	                        CLINIC_CODE,
	                        IDENNO,
	                        SFDJH,
	                        WYLSH,
	                        OPER_CODE,
	                        OPER_DATE,
                            UPLOAD_TYPE
	                        )
                        VALUES 
	                        (
	                        '{0}',
	                        '{1}',
	                        '{2}',
	                        '{3}',
	                        '{4}',
	                        '{5}',
	                        '{6}',
	                        '{7}',
	                        SYSDATE,
                            '{8}'
	                        )";

            #endregion

            try
            {
                strSQL = string.Format(strSQL,
                                       patientType, //0 患者类型：1门诊；2住院
                                       invoiceNo, //1 发票号
                                       patientNo, //2 住院号/门诊号
                                       clinicCode, //3 流水号
                                       idenNo, //4 身份证号码
                                       sfdjh, //5 收费单据号
                                       wylsh, //6 唯一流水号（主单号）
                                       operCode, //7 操作员
                                       Uptype //上传标志
                                       );
            }
            catch
            {
                Err = "保存上传日志，传入参数不对!";
                WriteErr();
                return -1;
            }
            return ExecNoQuery(strSQL);
        }
        /// <summary>
        /// 更新上传标志
        /// </summary>
        /// <param name="patientType"></param>
        /// <param name="patientID"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="UpType"></param>
        /// <returns></returns>
        public int UpdateLog(string patientType, string patientID, string invoiceNo,string UpType)
        {
            string sql = @"update FS_UPLOAD_DISEASEPAY
                           set upload_type = '{3}'
                         where PATIENT_TYPE = '{0}'
                           and INVOICE_NO = '{1}'
                           and CLINIC_CODE = '{2}'
                        ";
            sql = string.Format(sql, patientType, invoiceNo, patientID, UpType);

            return ExecNoQuery(sql);
        }

        /// <summary>
        /// 删除上传日志
        /// </summary>
        /// <param name="patientType"></param>
        /// <param name="idenNo"></param>
        /// <param name="patientNo"></param>
        /// <param name="wylsh"></param>
        /// <param name="sfdjh"></param>
        /// <returns></returns>
        public int DeleteLog(string patientType, string idenNo, string patientNo, string wylsh, string sfdjh)
        {
            #region SQL

            string strSQL = @"DELETE FROM FS_UPLOAD_DISEASEPAY t
                                WHERE t.PATIENT_TYPE = '{0}'
                                AND t.IDENNO = '{1}'
                                AND t.PATIENT_NO = '{2}'
                                AND t.WYLSH = '{3}'
                                AND t.SFDJH = '{4}'";

            #endregion

            try
            {
                strSQL = string.Format(strSQL,
                                       patientType, //0 患者类型：1门诊；2住院
                                       idenNo, //1 身份证号码
                                       patientNo, //2 住院号/门诊号
                                       wylsh, //3 唯一流水号（主单号）
                                       sfdjh //4 收费单据号
                                       );
            }
            catch
            {
                Err = "删除上传日志，传入参数不对!";
                WriteErr();
                return -1;
            }
            return ExecNoQuery(strSQL);
        }
        /// <summary>
        /// 删除上传日志
        /// </summary>
        /// <param name="invociNO"></param>
        /// <param name="patientID"></param>
        /// <param name="patientType"></param>
        /// <returns></returns>
        public int DeleteLog(string invociNO,string patientID ,string patientType)
        {
            #region SQL

            string strSQL = @"delete from FS_UPLOAD_DISEASEPAY y
                                where y.invoice_no = '{0}'
                                and y.clinic_code = '{1}'
                                and y.patient_type = '{2}'";

            #endregion

            try
            {
                strSQL = string.Format(strSQL,
                                       invociNO, 
                                       patientID, 
                                       patientType 
                                       );
            }
            catch
            {
                Err = "删除上传日志，传入参数不对!";
                WriteErr();
                return -1;
            }
            return ExecNoQuery(strSQL);
        }


        /// <summary>
        /// 获取结算号
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <param name="patientID"></param>
        /// <param name="patientType"></param>
        /// <returns></returns>
        public string GetSFDJH(string invoiceNo,string patientID,string patientType)
        {
            string sql = @"select y.sfdjh from FS_UPLOAD_DISEASEPAY y 
                            where y.invoice_no = '{0}'
                            and y.clinic_code = '{1}'
                            and y.patient_type = '{2}'";
            sql = string.Format(sql, invoiceNo, patientID, patientType);

            return this.ExecSqlReturnOne(sql);
        }
        #endregion

        /// <summary>
        /// 查询医师信息
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryDoctorInfo(DateTime dtBegin, DateTime dtEnd)
        {
            string strSql = @"SELECT E.EMPL_CODE,
                           E.EMPL_NAME,
                           DECODE(E.SEX_CODE, 'M', '男', '女'),
                           E.LEVL_CODE,
                           E.DEPT_CODE,
                           FUN_GET_DEPT_NAME(E.DEPT_CODE)
                      FROM COM_EMPLOYEE E
                     WHERE E.VALID_STATE = '1'
                       AND E.EMPL_TYPE = 'D'";

            strSql = string.Format(strSql, dtBegin.ToString(), dtEnd.ToString());
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            ArrayList alDoct = new ArrayList();
            FS.HISFC.Models.Base.Employee emp = null;
            try
            {
                while (this.Reader.Read())
                {
                    emp = new FS.HISFC.Models.Base.Employee();

                    emp.ID = this.Reader[0].ToString().Trim();
                    emp.Name = this.Reader[1].ToString().Trim();
                    emp.Sex.Name = this.Reader[2].ToString();
                    emp.Duty.Name = this.Reader[3].ToString();
                    emp.Dept.ID = this.Reader[4].ToString();
                    emp.Dept.Name = this.Reader[5].ToString();

                    alDoct.Add(emp);
                }
                return alDoct;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 查询医师信息
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryDoctorInfoNew(DateTime dtBegin, DateTime dtEnd)
        {
            string strSql = @"SELECT E.EMPL_CODE,
                           E.EMPL_NAME,
                           DECODE(E.SEX_CODE, 'M', '男', '女'),
                           E.LEVL_CODE,
                           nvl((select d.bz_dept_code from com_department d where d.dept_code = E.DEPT_CODE),'A05'),
                           FUN_GET_DEPT_NAME(E.DEPT_CODE),
                           E.EMPL_TYPE
                      FROM COM_EMPLOYEE E
                     WHERE --E.VALID_STATE = '1'
                       --AND 
                       E.EMPL_TYPE in ('D','N')
                       --and E.EMPL_CODE in ('0s5059','0s2707','0s5059','0s5162','0s2702','0s2701')
                       --and E.DEPT_CODE in (select d.DEPT_CODE from com_department d
                       --where d.dept_type in ('I','OP'))";

            strSql = string.Format(strSql, dtBegin.ToString(), dtEnd.ToString());
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            ArrayList alDoct = new ArrayList();
            FS.HISFC.Models.Base.Employee emp = null;
            try
            {
                while (this.Reader.Read())
                {
                    emp = new FS.HISFC.Models.Base.Employee();

                    emp.ID = this.Reader[0].ToString().Trim();
                    emp.Name = this.Reader[1].ToString().Trim();
                    emp.Sex.Name = this.Reader[2].ToString();
                    emp.Duty.Name = this.Reader[3].ToString();
                    emp.Dept.ID = this.Reader[4].ToString();
                    emp.Dept.Name = this.Reader[5].ToString();
                    emp.EmployeeType.ID = this.Reader[6].ToString();

                    alDoct.Add(emp);
                }
                return alDoct;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }
        /// <summary>
        /// 获取HIS与社保对照码
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,Const> QuerySiCompare()
        {
            string strSql = @"SELECT t.HIS_CODE,t.HIS_NAME,t.CENTER_CODE,t.CENTER_NAME
                              FROM FIN_COM_COMPARE t
                              WHERE t.PACT_CODE = '99'";

            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            Dictionary<string, Const> dict = new Dictionary<string, Const>();
            FS.HISFC.Models.Base.Const obj = null;
            try
            {
                while (this.Reader.Read())
                {
                    obj = new Const();

                    obj.ID = this.Reader[0].ToString().Trim();   //HIS编码
                    obj.Name = this.Reader[1].ToString().Trim();  //his名称
                    obj.UserCode = this.Reader[2].ToString();   //社保编码
                    obj.Memo = this.Reader[3].ToString();       //社保名称

                    if (!dict.ContainsKey(obj.ID))
                    {
                        dict.Add(obj.ID, obj);
                    }
                }
                return dict;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

        }

        /// <summary>
        /// 获取HIS与社保对照码
        /// </summary>
        /// <param name="hisCode"></param>
        /// <returns></returns>
        public Const QuerySiCompare(string hisCode)
        {
            string strSql = @"
                            SELECT t.HIS_CODE,t.HIS_NAME,t.CENTER_CODE,t.CENTER_NAME
                            FROM FIN_COM_COMPARE t
                            WHERE t.PACT_CODE = '99'
                            AND t.HIS_CODE = '{0}'
                            ";

            strSql = string.Format(strSql, hisCode);
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            FS.HISFC.Models.Base.Const obj = null;
            try
            {
                while (this.Reader.Read())
                {
                    obj = new Const();

                    obj.ID = this.Reader[0].ToString().Trim();   //HIS编码
                    obj.Name = this.Reader[1].ToString().Trim();  //his名称
                    obj.UserCode = this.Reader[2].ToString();   //社保编码
                    obj.Memo = this.Reader[3].ToString();       //社保名称
                }
                return obj;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

        }

        /// <summary>
        /// 取出院记录-【入院情况；诊疗过程；出院情况；出院医嘱】
        /// </summary>
        /// <param name="registerId">住院流水号</param>
        /// <returns></returns>
        public Const QueryPatientOutRecord(string registerId)
        {
            string strSql = @"SELECT FUN_GET_INPATIENT_INCONDITION('{0}')入院情况,
                                   FUN_GET_INPATIENT_INPROGRESS('{0}') 诊疗过程,
                                   FUN_GET_INPATIENT_OUTSUMMARY('{0}') 出院情况,
                                   FUN_GET_INPATIENT_OUTORDER('{0}') 出院医嘱
                             FROM DUAL ";

            strSql = string.Format(strSql, registerId);
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            FS.HISFC.Models.Base.Const obj = null;
            try
            {
                while (this.Reader.Read())
                {
                    obj = new Const();

                    obj.ID = this.Reader[0].ToString().Trim();   //入院情况
                    obj.Name = this.Reader[1].ToString().Trim();  //诊疗过程
                    obj.UserCode = this.Reader[2].ToString();   //出院情况
                    obj.Memo = this.Reader[3].ToString();       //出院医嘱
                }
                return obj;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 查询住院病案首页内容
        /// </summary>
        /// <param name="registerId">住院流水号</param>
        /// <returns></returns>
        public FS.HISFC.Models.HealthRecord.Base QueryInpatientCase(string registerId)
        {
            string strSql = @"select d.clinicNO,
                                d.IsDrugAllergy,
                                d.AllergyDrugCode,
                                d.AllergyDrugName,
                                d.IsPathologicalExamination,
                                d.PathologyCode,
                                d.IsHospitalInfected,
                                d.HospitalInfectedCode,
                                d.BloodTypeS,
                                d.BloodTypeE,
                                d.FLYFSBH,
                                d.FSTATUSBH,
                                d.Height,
                                d.Weight,
                                d.NewbornDate,
                                d.NewbornWeight,
                                d.BloodTransHistory,
                                d.AdmissionDate,
                                d.DischargeDate                                
                                from fsbasy d 
                                WHERE clinicNO = '{0}'";

            strSql = string.Format(strSql, registerId);//目前写死，以后修改 截取住院号8位+住院次数
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            int i = FoShanDiseasePay.FunctionSql.ExecuteQuery_DRGS(string.Format(strSql, registerId), ref ds, "DRGSReportList");
            if (ds.Tables["DRGSReportList"] != null && ds.Tables["DRGSReportList"].Rows.Count > 0)
            {
                dt = ds.Tables["DRGSReportList"];
            }
            else
            {
                return null;
            }

            FS.HISFC.Models.HealthRecord.Base metBase = null;
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    metBase = new FS.HISFC.Models.HealthRecord.Base();
                    
                    metBase.AnaphyFlag = dr[1].ToString();
                    metBase.FirstAnaphyPharmacy.ID = dr[2].ToString();
                    metBase.FirstAnaphyPharmacy.Name = dr[3].ToString();
                    metBase.YnFirst = dr[4].ToString();
                    metBase.PathNum = dr[5].ToString();
                    metBase.InfectionNum = NConvert.ToInt32(dr[6].ToString());  //感染次数
                    metBase.InfectionPosition.ID = dr[7].ToString();   //感染编码
                    metBase.ReactionBlood = dr[8].ToString();
                    metBase.RhBlood = dr[9].ToString();
                    metBase.Out_Type = dr[10].ToString();
                    metBase.PatientInfo.MaritalStatus.ID = dr[11].ToString();
                    metBase.PatientInfo.Height = dr[12].ToString();
                    metBase.PatientInfo.Weight = dr[13].ToString();
                    metBase.BabyBirthWeight = dr[14].ToString();//新生儿出生体重
                    metBase.BabyInWeight = dr[15].ToString();//新生儿入院体重
                    metBase.ReactionBlood = dr[16].ToString();//
                    metBase.PatientInfo.PVisit.InTime = NConvert.ToDateTime(dr[17].ToString());//
                    metBase.PatientInfo.PVisit.OutTime = NConvert.ToDateTime(dr[18].ToString());//
                }
                return metBase;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

      /// <summary>
        /// 获取诊断信息
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <returns></returns>
        public DataTable QueryCaseDiagnoseForinPatientNo(string inPatientNo)
        {
            string sql = @"select * from lgsb@emr1_dblink dd where HIS内部标识 = '{0}'";
            sql = string.Format(sql, inPatientNo);
            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sql, ref ds);
            return ds.Tables[0];
        }   
        /// <summary>
        /// 查询住院手术内容
        /// </summary>
        /// <param name="registerId">住院流水号</param>
        /// <returns></returns>
        public ArrayList QueryOperationByInpatientNo(string registerId, string inTimes)
        {
            string strSql = @"  select j.FID,
                                  j.FDOCBH,
                                  j.FDOCNAME,
                                  j.FOPDOCT1BH,
                                  j.FOPDOCT1,
                                  j.FOPDOCT2BH,
                                  j.FOPDOCT2,
                                  j.FMZDOCTBH,
                                  j.FMZDOCT,
                                  j.FOPDATE,
                                  j.FMAZUIBH,
                                  j.FQIEKOUBH,
                                  j.FYUHEBH,
                                  j.FSSJBBH,
                                  j.FOPCODE,
                                  j.FOP,
                                  j.FPX
                                   from bafzffss j 
                                   WHERE 
                                   j.FPRN = '{0}' 
                                   AND j.FTIMES = '{1}';";

            strSql = string.Format(strSql, registerId, inTimes);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            int i = FoShanDiseasePay.FunctionSql.ExecuteQuery_DRGS(string.Format(strSql, registerId), ref ds, "DRGSSSReportList");
            if (ds.Tables["DRGSSSReportList"] != null && ds.Tables["DRGSSSReportList"].Rows.Count > 0)
            {
                dt = ds.Tables["DRGSSSReportList"];
            }
            else
            {
                return null;
            }
            ArrayList al = new ArrayList();
            FS.HISFC.Models.HealthRecord.OperationDetail info1 = null;
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    info1 = new FS.HISFC.Models.HealthRecord.OperationDetail();                    
                    info1.FirDoctInfo.ID = dr[1].ToString();
                    info1.FirDoctInfo.ID = dr[1].ToString();
                    info1.FirDoctInfo.Name = dr[2].ToString();
                    info1.SecDoctInfo.ID = dr[3].ToString();
                    info1.SecDoctInfo.Name = dr[4].ToString();
                    info1.ThrDoctInfo.ID = dr[5].ToString();
                    info1.ThrDoctInfo.Name = dr[6].ToString();
                    info1.NarcDoctInfo.ID = dr[7].ToString();
                    info1.NarcDoctInfo.Name = dr[8].ToString();
                    info1.OperationDate = NConvert.ToDateTime(dr[9].ToString());  //
                    info1.MarcKind = dr[10].ToString();   //麻醉方式
                    info1.NickKind = dr[11].ToString();//手术切口分类
                    info1.CicaKind = dr[12].ToString();//手术切口愈合
                    info1.OperationNO = dr[13].ToString();//手术级别
                    info1.OperationInfo.ID = dr[14].ToString();//
                    info1.OperationInfo.Name = dr[15].ToString();
                    info1.SYNDFlag = "";
                    info1.StatFlag = dr[16].ToString();//主手术 1
                    al.Add(info1);
                }
                return al;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

       

        /// <summary>
        /// 获取病案系统上的诊断信息
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <returns></returns>
        public DataTable GetCaseDiagnoseByBASY(string patientNo,string inTime)
        {
            string strSql = "SELECT * FROM lgsb WHERE 住院号= '{0}' and 住院次数 = '{1}'";
            strSql = string.Format(strSql, patientNo, inTime);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            int i = FoShanDiseasePay.FunctionSql.ExecuteQuery_DRGS(strSql, ref ds, "DRGSSSDiagnoseList");
            if (ds.Tables["DRGSSSDiagnoseList"] != null && ds.Tables["DRGSSSDiagnoseList"].Rows.Count > 0)
            {
                dt = ds.Tables["DRGSSSDiagnoseList"];
            }
            else
            {
                return null;
            }

            return dt;

        }

        #region 病种分值付费上传升级改造

        /// <summary>
        /// 获取病案系统上的诊断信息
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <returns></returns>
        public DataTable GetCaseDiagnoseByBAZD(string patientNo, string inTime)
        {
            string strSql = "SELECT * FROM bafzffzd WHERE BAH= '{0}' and TIMES = '{1}'";
            strSql = string.Format(strSql, patientNo, inTime);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            int i = FoShanDiseasePay.FunctionSql.ExecuteQuery_DRGS(strSql, ref ds, "bazd");
            if (ds.Tables["bazd"] != null && ds.Tables["bazd"].Rows.Count > 0)
            {
                dt = ds.Tables["bazd"];
            }
            else
            {
                return null;
            }

            return dt;

        }
        /// <summary>
        /// 获取病案系统上的主要信息
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <returns></returns>
        public DataTable GetCaseMainInfo(string patientNo, string inTime)
        {
            string strSql = "SELECT * FROM bafzff WHERE BAH= '{0}' and TIMES = '{1}'";
            strSql = string.Format(strSql, patientNo, inTime);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            int i = FoShanDiseasePay.FunctionSql.ExecuteQuery_DRGS(strSql, ref ds, "CaseMainInfo");
            if (ds.Tables["CaseMainInfo"] != null && ds.Tables["CaseMainInfo"].Rows.Count > 0)
            {
                dt = ds.Tables["CaseMainInfo"];
            }
            else
            {
                return null;
            }

            return dt;

        }


        /// <summary>
        /// 获取基本信息主体门诊
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <returns></returns>
        public DataTable MZDRGSJBXX(string inPatientNo)
        {
            string sql = "select * from VIEW_DRGS_JBXX_MZ where QDLSH = '{0}'";
            sql = string.Format(sql, inPatientNo);
            DataSet dsSet = null;
            if (this.ExecQuery(sql, ref dsSet) == -1)
            {
                return null;
            }
            return dsSet.Tables[0];
        }

        /// <summary>
        /// 获取医疗收费信息主体门诊
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <returns></returns>
        public DataTable MZDRGSYYSFXX(string inPatientNo, string invoiceNo)
        {
            string sql = "  select * from VIEW_DRGS_YYSFXX_MZ where QDLSH =  '{0}' and YWLSH = '{1}'";
            sql = string.Format(sql, inPatientNo, invoiceNo);
            DataSet dsSet = null;
            if (this.ExecQuery(sql, ref dsSet) == -1)
            {
                return null;
            }
            return dsSet.Tables[0];
        }

        /// <summary>
        /// 获取医疗收费信息主体
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <returns></returns>
        public DataTable DRGSYYSFXX(string inPatientNo, string invoiceNo, string jydjh)
        {
            string sql = "  select * from VIEW_DRGS_YYSFXX where YWLSH =  '{0}' and PJDM = '{1}'and jydjh = '{2}'";
            sql = string.Format(sql, inPatientNo, invoiceNo, jydjh);
            DataSet dsSet = null;
            if (this.ExecQuery(sql, ref dsSet) == -1)
            {
                return null;
            }
            return dsSet.Tables[0];
        }


        /// <summary>
        /// 获取对照科室编码
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public string GetKB(string deptCode)
        {
            string sql = "select t.BZ_DEPT_CODE from com_department t where t.dept_code = '{0}'";
            sql = string.Format(sql, deptCode);
            string res = this.ExecSqlReturnOne(sql);
            if (string.IsNullOrEmpty(res) || res == "-1")
            {
                return deptCode;
            }

            return res;
        }

        /// <summary>
        /// 查询待上传的住院患者
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public DataTable QueryNeedUploadDetailNew(string dtBegin, string dtEnd, string patientNo)
        {
            // {B7DA1304-1FCB-4c01-B96A-1BEABD23F79C}
            string sql = @"SELECT '0' 选择,
                                  '2' 类别,
                                   r.PATIENT_NO 病历号,
                                   r.NAME 姓名,
                                   r.IDENNO 证件号,
                                   decode(r.SEX_CODE,'F','女','男') 性别,
                                   (SELECT f.PACT_NAME FROM FIN_COM_PACTUNITINFO f WHERE f.PACT_CODE = t.PACT_CODE) 结算类型,
                                   t.INVOICE_NO 发票电脑号,
                                   '' 发票印刷号,
                                   t.TOT_COST 总金额,
                                   t.PUB_COST 统筹金额,
                                   (t.OWN_COST+t.PAY_COST) 自费金额,
                                   FUN_GET_EMPLOYEE_NAME(t.oper_code) 结算员,
                                   t.oper_date 结算日期,
                                   t.INPATIENT_NO,
                                   t.PACT_CODE,
                                 (SELECT f.upload_type
                                      FROM FS_UPLOAD_DISEASEPAY f
                                     WHERE f.PATIENT_TYPE = '4'
                                       AND f.INVOICE_NO = t.INVOICE_NO
                                       AND f.CLINIC_CODE = t.INPATIENT_NO
                                       and rownum = 1) upload_type,
                                   (select g.err
                                      from upload_err_log g
                                     where g.inpatient_no = r.inpatient_no
                                       and g.invoice_no = t.invoice_no
                                       and g.oper_date =
                                           (select max(a.oper_date)
                                              from upload_err_log a
                                             where a.inpatient_no = g.inpatient_no
                                               and a.invoice_no = g.invoice_no)
                                       and rownum = 1) err
                            FROM FIN_IPR_SIINMAININFO t,FIN_IPR_INMAININFO r
                            WHERE t.INPATIENT_NO = r.INPATIENT_NO 
                            AND t.PACT_CODE IN (SELECT c.PACT_CODE FROM FIN_COM_PACTUNITINFO c WHERE c.DLL_NAME = '{2}')
                            AND t.oper_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                            AND t.oper_date <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                            and t.balance_no = (select max(tt.balance_no) from FIN_IPR_SIINMAININFO tt
                            where t.inpatient_no = tt.inpatient_no and tt.valid_flag = '1')
                            AND NOT EXISTS(
                                SELECT 1 FROM FS_UPLOAD_DISEASEPAY f
                                WHERE f.PATIENT_TYPE = '4'
                                AND f.INVOICE_NO = t.INVOICE_NO
                                AND f.CLINIC_CODE = t.INPATIENT_NO
                                and f.upload_type = '3')
                             and r.dept_name not like '%家%床%'
                            and t.valid_flag = '1'
                            and (r.PATIENT_NO = '{3}' or '{3}' = 'ALL')
                            ";
            // {385E49F2-947B-43e3-931D-BE89481BA68C}
            //佛山医保合同单位
            string strDllName = "FoShanSI.dll";

            sql = string.Format(sql, dtBegin, dtEnd, strDllName, patientNo);
            DataSet dsSet = null;
            if (this.ExecQuery(sql, ref dsSet) == -1)
            {
                return null;
            }
            return dsSet.Tables[0];
        }

        /// <summary>
        /// 查询已上传的住院患者
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public DataTable QueryHaveUploadedDetailNew(string dtBegin, string dtEnd, string patientNo)
        {

            string sql = @"SELECT '1' 选择,
                                   '2' 类别,
                                   r.PATIENT_NO 病历号,
                                   r.NAME 姓名,
                                   r.IDENNO 证件号,
                                   decode(r.SEX_CODE,'F','女','男') 性别,
                                   (SELECT f.PACT_NAME FROM FIN_COM_PACTUNITINFO f WHERE f.PACT_CODE = t.PACT_CODE) 结算类型,
                                   t.INVOICE_NO 发票电脑号,
                                   '' 发票印刷号,
                                   t.TOT_COST 总金额,
                                   t.PUB_COST 统筹金额,
                                   (t.OWN_COST+t.PAY_COST) 自费金额,
                                    FUN_GET_EMPLOYEE_NAME(t.oper_code) 结算员,
                                   t.oper_date 结算日期,
                                   t.INPATIENT_NO,
                                   t.PACT_CODE,
                                   FUN_GET_EMPLOYEE_NAME(fs.OPER_CODE) ,
                                   fs.OPER_DATE,
                                   fs.SFDJH,
                                   fs.WYLSH,
                                   fs.upload_type
                                   
                            FROM FIN_IPR_SIINMAININFO t,FIN_IPR_INMAININFO r,FS_UPLOAD_DISEASEPAY fs
                            WHERE t.INPATIENT_NO = r.INPATIENT_NO 
                            AND t.PACT_CODE IN (SELECT c.PACT_CODE FROM FIN_COM_PACTUNITINFO c WHERE c.DLL_NAME = '{2}')
                            and t.balance_no = (select max(tt.balance_no) from FIN_IPR_SIINMAININFO tt
                            where t.inpatient_no = tt.inpatient_no and tt.valid_flag = '1')
                            AND t.oper_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                            AND t.oper_date <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                            AND fs.PATIENT_TYPE = '4'
                            AND t.INVOICE_NO = fs.INVOICE_NO
                            AND EXISTS(
                                SELECT 1 FROM FS_UPLOAD_DISEASEPAY f
                                WHERE f.PATIENT_TYPE = '4'
                                AND f.INVOICE_NO = t.INVOICE_NO
                                AND f.CLINIC_CODE = t.INPATIENT_NO
                                )
                            and (r.PATIENT_NO = '{3}' or '{3}' = 'ALL')
                           order by t.BALANCE_DATE
                            ";

            //佛山医保合同单位
            string strDllName = "FoShanSI.dll";

            sql = string.Format(sql, dtBegin, dtEnd, strDllName, patientNo);
            DataSet dsSet = null;
            if (this.ExecQuery(sql, ref dsSet) == -1)
            {
                return null;
            }
            return dsSet.Tables[0];
        }

        /// <summary>
        /// 根据结算号查询门诊、住院的基本信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetBaseInfoList(string jsh)
        {
            string sql = @"
select a.inpatient_no,
       i.patient_no,
       a.invoice_no,
       i.idenno,
       a.oper_date,
       i.in_date,
       i.out_date,
       max(a.balance_no) xh,
       '2' person,
       (select g.err
          from upload_err_log g
         where g.inpatient_no = a.inpatient_no
           and g.invoice_no = a.invoice_no
           and g.oper_date =
               (select max(a.oper_date)
                  from upload_err_log a
                 where a.inpatient_no = g.inpatient_no
                   and a.invoice_no = g.invoice_no)
           and rownum = 1) err
  from fin_ipr_siinmaininfo a, fin_ipr_inmaininfo i
 where a.inpatient_no = i.inpatient_no
   and a.remark like '{0}%'
   and a.TYPE_CODE = '2'
 group by a.inpatient_no,
          i.patient_no,
          a.invoice_no,
          i.idenno,
          a.oper_date,
          i.in_date,
          i.out_date

union all

select a.inpatient_no,
       r.card_no,
       a.invoice_no,
       p.idenno,
       a.oper_date,
       r.reg_date,
       to_date('', 'yyyy-mm-dd hh24:mi:ss'),
       max(a.balance_no) xh,
       '1' person,
       (select g.err
          from upload_err_log g
         where g.inpatient_no = a.inpatient_no
           and g.invoice_no = a.invoice_no
           and g.oper_date =
               (select max(a.oper_date)
                  from upload_err_log a
                 where a.inpatient_no = g.inpatient_no
                   and a.invoice_no = g.invoice_no)
           and rownum = 1) err
  from fin_ipr_siinmaininfo a, fin_opr_register r, com_patientinfo p
 where a.inpatient_no = r.clinic_code
   and r.card_no = p.card_no
   and a.remark like '{0}%'
   and a.TYPE_CODE = '1'
 group by a.inpatient_no,
          r.card_no,
          a.invoice_no,
          p.idenno,
          a.oper_date,
          r.reg_date

";
            sql = string.Format(sql, jsh);
            DataSet dsSet = null;
            if (this.ExecQuery(sql, ref dsSet) == -1)
            {
                return null;
            }
            return dsSet.Tables[0];
        }


        #endregion

        #region 手工插入医保结算费用信息

        public int DeleteSiInmaininfo(string inPatientNo, string DJH)
        {
            string sql = @"delete from fin_ipr_siinmaininfo s where s.inpatient_no = '{0}' and s.remark = '{1}'";
            sql = string.Format(sql, inPatientNo, DJH);
            return this.ExecNoQuery(sql);

        }
        public int InsertSiInmaininfo(string inPatientNo, string DJH)
        {
            if (string.IsNullOrEmpty(inPatientNo) || string.IsNullOrEmpty(DJH))
            {
                this.Err = "住院信息为空！";
                return -1;
            }
            string sql = @"insert into fin_ipr_siinmaininfo s
                              (s.inpatient_no,
                               s.fee_times,
                               s.balance_no,
                               s.card_no,
                               s.name,
                               s.sex_code,
                               s.dept_code,
                               s.paykind_code,
                               s.pact_code,
                               s.pact_name,
                               s.in_date,
                               s.balance_date,
                               s.oper_code,
                               s.oper_date,
                               s.remark,
                               s.invoice_no ,
                               s.TYPE_CODE
                               ) values
                               (
                               '{0}',--住院流水号
                               '0',
                               '1',
                               (select i.card_no from fin_ipr_inmaininfo i where i.inpatient_no = '{0}'),--住院流水号
                               (select i.name from fin_ipr_inmaininfo i where i.inpatient_no = '{0}'),--住院流水号
                               (select i.sex_code from fin_ipr_inmaininfo i where i.inpatient_no = '{0}'),--住院流水号
                               (select i.dept_code from fin_ipr_inmaininfo i where i.inpatient_no = '{0}'),--住院流水号
                               '02',
                               (select i.pact_code from fin_ipr_inmaininfo i where i.inpatient_no = '{0}'),--住院流水号
                               (select i.pact_name from fin_ipr_inmaininfo i where i.inpatient_no = '{0}'),--住院流水号
                               (select i.in_date from fin_ipr_inmaininfo i where i.inpatient_no = '{0}'),--住院流水号
                               (select i.balance_date from fin_ipr_inmaininfo i where i.inpatient_no = '{0}'),--住院流水号
                               '009999',
                               sysdate ,
                               '{1}' ,--社保结算号   
                               (select d.invoice_no from fin_ipb_balancehead d where d.inpatient_no = '{0}' and rownum = '1'),
                               '2'
                               )";
            sql = string.Format(sql, inPatientNo, DJH);


            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 作废医保结算
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateBlanceSIPatient(string lsh, string regNo)
        {
            if (string.IsNullOrEmpty(lsh))
            {
                this.Err = "删除费用明细失败，患者流水号为空！";
                return -1;
            }

            if (string.IsNullOrEmpty(regNo))
            {
                this.Err = "删除费用明细失败，就诊登记号为空！";
                return -1;
            }
            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.UpdateBlanceSIInPatient", ref strSql) == -1)
            {
                this.Err = "获得[Fee.Interface.UpdateBlanceSIInPatient]对应sql语句出错";
                return -1;
            }
            strSql = string.Format(strSql, lsh, regNo);

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 插入结算后的信息
        /// </summary>
        /// <param name="balanceStr"></param>
        /// <returns></returns>
        public int SaveBlanceSIInPatient(FS.HISFC.Models.RADT.PatientInfo obj)
        {
            if (obj == null)
            {
                this.Err = "插入医保结算失败，患者信息为空！";
                return -1;
            }
            if (string.IsNullOrEmpty(obj.SIMainInfo.Memo))
            {
                this.Err = "获取医保结算信息失败！";
                return -1;
            }
            string[] balanceInfo = obj.SIMainInfo.Memo.Split('|');

            string strSql = "";
            //if (balanceInfo != null && balanceInfo.Length > 0)
            {
                string strBalanceNo = "";

                if (balanceInfo != null && balanceInfo.Length > 0)
                {
                    strBalanceNo = balanceInfo[0];
                }
                if (string.IsNullOrEmpty(strBalanceNo))
                {
                    this.Err = "插入医保结算失败，结算单号为空！";
                    return -1;
                }

                string delSQL = "delete from gzsi_his_fyjs where JSID = '{0}'";
                delSQL = string.Format(delSQL, strBalanceNo);
                this.ExecNoQuery(delSQL);


                if (this.Sql.GetSql("Fee.Interface.SaveBlanceSIInPatient.1", ref strSql) == -1)
                {
                    this.Err = "获得[Fee.Interface.SaveBlanceSIInPatient.1]对应sql语句出错";
                    return -1;
                }

                strSql = string.Format(strSql, obj.ID,//住院流水号
                    obj.SIMainInfo.InvoiceNo,//结算发票号
                    obj.SIMainInfo.RegNo,//就医登记号
                    obj.SIMainInfo.FeeTimes,//费用批次
                    string.IsNullOrEmpty(obj.SIMainInfo.HosNo) ? "44061157" : obj.SIMainInfo.HosNo,//医院编号
                    obj.IDCard,//5身份证号
                    obj.PID.PatientNO,// 门诊号/住院号
                    obj.PVisit.InTime.ToString(),//入院日期
                    obj.BalanceDate.ToString(),//结算日期
                    obj.SIMainInfo.TotCost.ToString(),//总金额
                    obj.SIMainInfo.PubCost.ToString(),//社保支付金额
                    obj.SIMainInfo.PayCost.ToString(),// 11账户支付金额
                    "0",//部分项目自付金额
                    "0",//个人起付金额
                    obj.SIMainInfo.OwnCost.ToString(),//14个人自费项目金额
                    obj.SIMainInfo.PayCost.ToString(),//个人自付金额  15
                    obj.SIMainInfo.PayCost.ToString(),//个人自负金额
                    "0",//超统筹支付限额个人自付金额
                    "",//自费原因
                    "0",//19医药机构分单金额
                    "",// 20 备注1,记录产生时间
                    "",//备注2
                    "",//备注3
                    "",//读入标志
                    this.Operator.ID,//操作员 24
                    strBalanceNo)// 25
                    ;

            }
            //else
            //{
            //    this.Err = "结算信息不正确！";
            //    return -1;
            //}

            return this.ExecNoQuery(strSql);

        }
        /// <summary>
        /// 删除医保上传的费用明细
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int DeletePatientUploadFeeDetail(string lsh, string regNo)
        {
            if (string.IsNullOrEmpty(lsh))
            {
                this.Err = "删除费用明细失败，患者流水号为空！";
                return -1;
            }

            if (string.IsNullOrEmpty(regNo))
            {
                this.Err = "删除费用明细失败，就诊登记号为空！";
                return -1;
            }

            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.DeleteInPatientUploadFeeDetail", ref strSql) == -1)
            {
                this.Err = "获得[Fee.Interface.DeleteInPatientUploadFeeDetail]对应sql语句出错";
                return -1;
            }
            strSql = string.Format(strSql, lsh, regNo);

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 查询是自费或报销编码// {EBC9E80A-CFAD-4e22-9AED-3C0628A788AE}
        /// </summary>
        /// <param name="moOrder"></param>
        /// <param name="personType">1门诊 2住院</param>
        /// <returns></returns>
        public string QueryFeeOwnOrPut(string moOrder, string itemCode, string personType)
        {
            string sql = "";
            if (personType == "1")
            {
                sql = @"select d.indications from met_ord_orderextend d,fin_opb_feedetail f
                        where d.mo_order = f.mo_order
                        and d.clinic_code = f.clinic_code
                        and f.mo_order = '{0}'";
                sql = string.Format(sql, moOrder);

                string res = this.ExecSqlReturnOne(sql);
                if (res == "0")
                {
                    string ownCode = this.QuerySiCompareByOwnFee(itemCode);
                    return ownCode;

                }
                else
                {
                    return "";
                }
            }
            else
            {
                sql = @"select d1.indications from met_ipm_orderextend d1, met_ipm_order o
                        where d1.mo_order = o.mo_order
                        and d1.inpatient_no = o.inpatient_no
                        and o.mo_order = '{0}' ";
                sql = string.Format(sql, moOrder);

                string res = this.ExecSqlReturnOne(sql);
                if (res == "0")
                {
                    string ownCode = this.QuerySiCompareByOwnFee(itemCode);
                    return ownCode;

                }
                else
                {
                    return "";
                }
            }

            return "";
        }

        /// <summary>
        /// 获取HIS与社保自费的对照码
        /// </summary>
        /// <param name="hisCode"></param>
        /// <returns></returns>
        public string QuerySiCompareByOwnFee(string hisCode)
        {
            string strSql = @"
                            SELECT t.CENTER_CODE
                            FROM FIN_COM_COMPARE t
                            WHERE t.PACT_CODE = '98'
                            AND t.HIS_CODE = '{0}'
                            and t.CENTER_CODE is not null
                            ";

            strSql = string.Format(strSql, hisCode);

            string res = this.ExecSqlReturnOne(strSql);
            if (res == "-1" || res == "0")
            {
                res = "";
            }

            return res;
        }
        /// <summary>
        /// 插入医保上传的费用明细信息
        /// </summary>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int InsertInPatientUploadFeeDetail(FS.HISFC.Models.RADT.PatientInfo obj, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            if (f == null)
            {
                this.Err = "插入医保费用明细失败，费用为空！";
                return -1;
            }
            if (obj == null)
            {
                this.Err = "插入医保费用明细失败，患者信息为空！";
                return -1;
            }

            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.InsertInPatientUploadFeeDetail.1", ref strSql) == -1)
            {
                this.Err = "获得[Fee.Interface.InsertInPatientUploadFeeDetail.1]对应sql语句出错";
                return -1;
            }
            string[] memoList = obj.SIMainInfo.Memo.Split('|');


            string strBalanceNo = "";
            if (memoList != null && memoList.Length > 0)
            {
                strBalanceNo = memoList[0];
            }

            try
            {
                strSql = string.Format(strSql, obj.SIMainInfo.RegNo,
                    string.IsNullOrEmpty(obj.SIMainInfo.HosNo) ? "44061157" : obj.SIMainInfo.HosNo,
                    obj.IDCard,
                    obj.PID.CardNO,
                    obj.PVisit.InTime.ToString(),
                    f.ChargeOper.OperTime.ToString(),//5
                    f.SequenceNO,
                    f.Item.UserCode,
                    f.Item.Name,
                    f.Item.MinFee.ID,
                    f.Item.Specs,
                    f.Item.SpecialFlag1,//剂型  11
                    f.Item.SpecialPrice.ToString(),
                    f.Item.Qty,
                    f.FT.TotCost.ToString(),//14
                    "",
                    "",
                    "",
                    "",
                    "",//19
                    obj.PID.PatientNO,// 20 
                    obj.ID,
                    obj.SIMainInfo.InvoiceNo,
                    this.Operator.ID,
                    f.Item.MinFee.ID,//24
                    strBalanceNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 获取住院需要上传的费用明细
        /// </summary>
        /// <param name="registerID"></param>
        /// <returns></returns>
        public System.Data.DataTable QueryInPatientNeedUploadFeeDetail(string registerID)
        {
            string sql = string.Format(@"select * from v_fin_ipb_fee_gzsi_bz t where t.inpatient_no='{0}'  and t.upload_flag<>'2' ", registerID);
            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sql, ref ds);
            return ds.Tables[0];
        }
        #endregion

        /// <summary>
        /// 根据名字获取医生工号信息
        /// </summary>
        /// <param name="doctName"></param>
        /// <returns></returns>
        public string GetDoctCodeByName(string doctName)
        {
            string sql = @"select e.empl_code from com_employee e
                    where e.empl_name = '{0}'";
            sql = string.Format(sql, doctName);
            string strName = this.ExecSqlReturnOne(sql);
            if (strName == "-1")
            {
                strName = "";
            }
            return strName;
        }

        /// <summary>
        /// 获取对照码
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="objStr"></param>
        /// <returns></returns>
        public string GetDictionaryCode(string typeName, string objStr)
        {
            string sql = @"select y.mark from com_dictionary y where y.type = '{0}' and y.code = '{1}' and y.valid_state = '1'";
            sql = string.Format(sql, typeName, objStr);
            string res = this.ExecSqlReturnOne(sql);
            if (res == "-1")
            {
                return "";
            }
            return res;
        }

        /// <summary>
        /// 获取麻醉方式对照
        /// </summary>
        /// <param name="doctName"></param>
        /// <returns></returns>
        public string GetAnaesthesiaType(string marcKind)
        {
            string sql = @" select fhiskh from basy_mzfsdz where fkh = '{0}'";
            sql = string.Format(sql, marcKind);
            string strName = this.ExecSqlReturnOne(sql);
            if (strName == "-1")
            {
                strName = "";
            }
            return strName;
        }
    }
}
