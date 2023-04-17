using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.HISFC.Models.Fee.Inpatient;
using FS.FrameWork.Function;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.RADT;

namespace GZSI.Management
{
    public class InpatientFee:FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 获得患者的非药品信息
        /// </summary>
        /// <param name="inpatientNO">住院流水号</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="flag">"All"所有, "Yes"已上传 "No"未上传</param>
        /// <returns>成功:获得费用信息 失败:null 没有找到记录 ArrayList.Count = 0</returns>
        public ArrayList QueryFeeItemLists(string inpatientNO, DateTime beginTime, DateTime endTime, string flag)
        {
            string upload = string.Empty;//是否上传标记

            if (flag.ToUpper() == "ALL")//所有
            {
                upload = "%";
            }
            else if (flag.ToUpper() == "YES")
            {
                upload = "1";
            }
            else
            {
                upload = "0";
            }

            //医保上传sql
            #region 医保上传sql
            string sql = @"SELECT  recipe_no recipe_no, --处方号
                                   0 sequence_no, --处方内项目流水号
                                   '1' trans_type, --交易类型,1正交易，2反交易
                                   inpatient_no, --住院流水号
                                   name, --姓名
                                   paykind_code, --结算类别 01-自费  02-保险 03-公费在职 04-公费退休 05-公费高干
                                   pact_code, --合同单位
                                   min(update_sequenceno) update_sequenceno, --更新库存的流水号(物资)
                                   min(inhos_deptcode) inhos_deptcode, --在院科室代码
                                   min(nurse_cell_code) nurse_cell_code, --护士站代码
                                   min(recipe_deptcode) recipe_deptcode, --开立科室代码
                                   min(execute_deptcode) execute_deptcode, --执行科室代码
                                   min(stock_deptcode) stock_deptcode, --扣库科室代码
                                   min(recipe_doccode) recipe_doccode, --开立医师代码
                                   item_code, --项目代码
                                   fee_code, --最小费用代码
                                   center_code, --中心代码
                                   item_name, --项目名称
                                   unit_price, --单价
                                   sum(qty) qty, --数量
                                   current_unit, --当前单位
                                   package_code, --组套代码
                                   package_name, --组套名称
                                   sum(tot_cost) tot_cost, --费用金额
                                   sum(own_cost) own_cost, --自费金额
                                   sum(pay_cost) pay_cost, --自付金额
                                   sum(pub_cost) pub_cost, --公费金额
                                   sum(eco_cost) eco_cost, --优惠金额
                                   min(sendmat_sequence) sendmat_sequence, --出库单序列号
                                   min(send_flag) send_flag, --发放状态（0 划价 2发放（执行） 1 批费）
                                   min(baby_flag) baby_flag , --是否婴儿用 0 不是 1 是
                                   min(jzqj_flag) jzqj_flag, --急诊抢救标志
                                   min(brought_flag) brought_flag, --出院带疗标记 0 否 1 是
                                   min(invoice_no) invoice_no, --结算发票号
                                   min(balance_no) balance_no, --结算序号
                                   min(apprno) apprno, --审批号
                                   min(charge_opercode) charge_opercode, --划价人
                                   min(charge_date) charge_date, --划价日期
                                   sum(confirm_num) confirm_num, --已确认数
                                   min(machine_no) machine_no, --设备号
                                   min(exec_opercode) exec_opercode, --执行人代码
                                   min(exec_date) exec_date, --执行日期
                                   min(fee_opercode) fee_opercode, --计费人
                                   min(fee_date) fee_date, --计费日期
                                   min(check_opercode) check_opercode, --审核人
                                   min(check_no) check_no, --审核序号
                                   min(mo_order) mo_order, --医嘱流水号
                                   min(mo_exec_sqn) mo_exec_sqn, --医嘱执行单流水号
                                   sum(NOBACK_NUM) NOBACK_NUM,
                                   min(balance_state) balance_state,
                                   min(fee_rate) fee_rate,
                                   min(FEEOPER_DEPTCODE) FEEOPER_DEPTCODE,
                                   min(EXT_FLAG) EXT_FLAG, --扩展标记
                                   min(EXT_FLAG1) EXT_FLAG1, --扩展标记1
                                   min(EXT_FLAG2) EXT_FLAG2, --扩展标记2
                                   min(EXT_CODE)  EXT_CODE, --扩展编码
                                   min(EXT_OPERCODE) EXT_OPERCODE, --扩展人员编码
                                   min(EXT_DATE) EXT_DATE, --扩展日期
                                   min(ITEM_FLAG) ITEM_FLAG, --0非药品 2物资
                                   min(medicalTeam_code) medicalTeam_code,
                                   min(operationno) operationno,  -- 手术编码
                                   UPLOAD_FLAG, --上传标志
                                   min(EXT_FLAG4) EXT_FLAG4,--特批标记
                                   org_unit_price,
                                   sum(package_qty) package_qty,
                                   min(split_id) split_id,
                                   min(split_flag) split_flag,
                                   split_fee_flag split_fee_flag
                              FROM fin_ipb_itemlist --住院非药品明细表
                            where  inpatient_no = '{0}'
                                    and balance_state='0'
                                    and split_fee_flag='0'
	                                and charge_date >= to_date('{1}','YYYY-MM-DD hh24:mi:ss')
	                                and charge_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')
	                                and upload_flag like '{3}'
                                    and noback_num<>0 
                                                        
                            group by
                                   recipe_no, --处方号
                                   inpatient_no, --住院流水号
                                   name, --姓名
                                   paykind_code, --结算类别 01-自费  02-保险 03-公费在职 04-公费退休 05-公费高干
                                   pact_code, --合同单位
                                   item_code, --项目代码
                                   fee_code, --最小费用代码
                                   center_code, --中心代码
                                   item_name, --项目名称
                                   unit_price, --单价
                                   current_unit, --当前单位
                                   package_code, --组套代码
                                   package_name, --组套名称
                                   UPLOAD_FLAG,
                                   org_unit_price,
                                   split_fee_flag
                            having sum(tot_cost)<>0
                            order by  charge_date";
            #endregion

            return this.QueryFeeItemListsBySql(sql, inpatientNO, beginTime.ToString(), endTime.ToString(), upload);
        }

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
                    if (this.Reader.FieldCount > 63)
                    {
                        itemList.Item.DefPrice = NConvert.ToDecimal(this.Reader[63].ToString());
                        itemList.UndrugComb.Qty = NConvert.ToDecimal(this.Reader[64].ToString());
                        itemList.SplitID = this.Reader[65].ToString();
                        itemList.SplitFlag = NConvert.ToBoolean(this.Reader[66].ToString());
                        itemList.SplitFeeFlag = NConvert.ToBoolean(this.Reader[67].ToString());
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
        /// 获得患者的药品费用信息
        /// </summary>
        /// <param name="inpatientNO">住院流水号</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="flag">"All"所有, "Yes"已上传 "No"未上传</param>
        /// <returns>成功:获得药品费用信息 失败:null 没有找到记录 ArrayList.Count = 0</returns>
        public ArrayList QueryMedItemLists(string inpatientNO, DateTime beginTime, DateTime endTime, string flag)
        {
            string upload = string.Empty;//上传标志

            if (flag.ToUpper() == "ALL")//所有
            {
                upload = "%";
            }
            else if (flag.ToUpper() == "YES")
            {
                upload = "1";
            }
            else
            {
                upload = "0";
            }

            #region 医保上传sql
            
            string sql = @"SELECT  recipe_no recipe_no, --处方号
                                   min(sequence_no) sequence_no, --处方内项目流水号
                                   min(trans_type) trans_type, --交易类型,1正交易，2反交易
                                   inpatient_no, --住院流水号
                                   name, --姓名
                                   paykind_code, --结算类别 01-自费  02-保险 03-公费在职 04-公费退休 05-公费高干
                                   pact_code, --合同单位
                                   min(update_sequenceno) update_sequenceno, --更新库存的流水号  ---------7
                                   min(inhos_deptcode) inhos_deptcode, --在院科室代码
                                   min(nurse_cell_code) nurse_cell_code, --护士站代码
                                   min(recipe_deptcode) recipe_deptcode, --开立科室代码
                                   min(execute_deptcode) execute_deptcode, --执行科室代码
                                   min(medicine_deptcode) medicine_deptcode, --取药科室代码
                                   min(recipe_doccode) recipe_doccode, --开立医师代码
                                   drug_code, --药品编码
                                   fee_code, --最小费用代码
                                   center_code, --医疗中心项目代码
                                   drug_name, --药品名称-----------------------17
                                   round(sum(tot_cost)/sum(qty),4) unit_price,--单价
                                   --round(unit_price/pack_qty,4), --单价
                                   sum(qty) qty, --数量
                                   current_unit, --当前单位
                                   1 pack_qty, --包装数---------------------------
                                   days, --付数----------------------------
                                   sum(tot_cost) tot_cost, --费用金额
                                   sum(own_cost) own_cost, --自费金额
                                   sum(pay_cost) pay_cost, --自付金额
                                   sum(pub_cost) pub_cost, --公费金额
                                   sum(eco_cost) eco_cost, --优惠金额
                                   min(senddrug_sequence) senddrug_sequence, --发药单序列号
                                   min(senddrug_flag) senddrug_flag, --发药状态（0 划价 2摆药 1批费）
                                   min(baby_flag) baby_flag, --是否婴儿用药 0 不是 1 是
                                   min(jzqj_flag) jzqj_flag, --急诊抢救标志
                                   min(brought_flag) brought_flag, --出院带药标记 0 否 1 是
                                   min(invoice_no) invoice_no, --结算发票号
                                   min(balance_no) balance_no, --结算序号
                                   min(apprno) apprno, --审批号
                                   min(charge_opercode) charge_opercode, --划价人
                                   min(charge_date) charge_date, --划价日期---------------------------37
                                   min(home_made_flag) home_made_flag, --自制标识---------
                                   min(drug_quality) drug_quality, --药品性质-----------
                                   min(senddrug_opercode) senddrug_opercode, --发药人
                                   min(senddrug_date) senddrug_date, --发药日期
                                   min(fee_opercode) fee_opercode, --计费人
                                   min(fee_date) fee_date, --计费时间
                                   min(check_opercode) check_opercode, --审核人
                                   min(check_no) check_no, --审核序号
                                   min(mo_order) mo_order, --医嘱流水号
                                   min(mo_exec_sqn) mo_exec_sqn, --医嘱执行单流水号
                                   specs, --规格---------------
                                   drug_type, --药品类别----------------
                                   sum(NOBACK_NUM) NOBACK_NUM,
                                   min(balance_state) balance_state,
                                   min(fee_rate) fee_rate,
                                   min(FEEOPER_DEPTCODE) FEEOPER_DEPTCODE,
                                   min(EXT_FLAG) EXT_FLAG, --扩展标记
                                   min(EXT_FLAG1) EXT_FLAG1, --扩展标记1
                                   min(EXT_FLAG2) EXT_FLAG2, --扩展标记2
                                   min(EXT_CODE) EXT_CODE, --扩展编码
                                   min(EXT_OPERCODE) EXT_OPERCODE, --扩展人员编码
                                   min(EXT_DATE) EXT_DATE ,--扩展日期
                                   min(medicalteam_code) medicalteam_code,
                                   min(operationno) operationno,  --手术编码
                                   '',--默认价格总金额_不知谁要这个列,广四这边没有
                                   UPLOAD_FLAG, --上传标志 
                                   min(EXT_DATE) EXT_DATE , --应执行时间
                                   min(ext_flag4) ext_flag4,
    (SELECT d.indications FROM met_ipm_orderextend d WHERE d.inpatient_no = inpatient_no AND d.mo_order = t.mo_order and rownum = 1) limitFlag --AKA185
                              FROM fin_ipb_medicinelist t --住院药品明细
                              where  inpatient_no = '{0}'
                                    and balance_state='0'
	                                and charge_date >= to_date('{1}','YYYY-MM-DD hh24:mi:ss')
	                                and charge_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')
	                                and upload_flag like '{3}'
                                    and noback_num<>0
                              group by
                                   recipe_no,  --处方号
                                   inpatient_no, --住院流水号
                                   name, --姓名
                                   paykind_code, --结算类别 01-自费  02-保险 03-公费在职 04-公费退休 05-公费高干
                                   pact_code, --合同单位
                                   drug_code, --药品编码
                                   fee_code, --最小费用代码
                                   center_code, --医疗中心项目代码
                                   drug_name, --药品名称-----------------------17
                                   unit_price, --单价
                                   current_unit, --当前单位
                                   pack_qty, --包装数---------------------------
                                   days, --付数----------------------------
                                    specs, --规格---------------
                                   drug_type, --药品类别----------------
                                   UPLOAD_FLAG, --上传标志 
                                    t.mo_order
                            having sum(tot_cost)<>0
                            order by  charge_date";
            //{67C4F998-C669-4509-A392-33B3156A2C42} 新增limitFlag字段查询
            #endregion

            return this.QueryMedItemListsBySql(sql, inpatientNO, beginTime.ToString(), endTime.ToString(), upload);

        }

        /// <summary>
        /// 获得药品费用项目信息
        /// </summary>
        /// <param name="sql">SQl语句</param>
        /// <param name="args">参数</param>
        /// <returns>成功:获得药品费用项目信息 失败: null</returns>
        public ArrayList QueryMedItemListsBySql(string sql, params string[] args)
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
                    if (this.Reader.FieldCount >= 66 && !string.IsNullOrEmpty(this.Reader[65].ToString()))
                    {
                        itemList.Item.SpecialFlag4 = this.Reader[65].ToString(); //特批标记
                    }
                    //{67C4F998-C669-4509-A392-33B3156A2C42} 新增限制标志值获取
                    if (this.Reader.FieldCount >= 67)
                    {
                        //itemList.Order.OrdExtend.Extend3 = this.Reader[66].ToString();
                        itemList.Order.Item.User01 = this.Reader[66].ToString();      //借用标志 制标志值
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
    }
}
