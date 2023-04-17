using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient
{
    public class LocalManager : FS.HISFC.BizLogic.Manager.DataBase
    {
        /// <summary>
        /// 获取执行单列表
        /// </summary>
        /// <param name="inpatientNos">所有患者住院号</param>
        /// <param name="execBillNos">执行单号</param>
        /// <param name="printType">打印类别：0 未打印，1 已打印 其他全部</param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="hsQuitFeeOrder">存储退费的执行档流水号</param>
        /// <param name="hsExecBill">存储执行档流水号对应的执行单号</param>
        /// <returns></returns>
        public ArrayList GetExecBill(string inpatientNos, string execBillNos, string printType, DateTime beginDate, DateTime endDate, ref Hashtable hsQuitFeeOrder, ref Dictionary<string, FS.FrameWork.Models.NeuObject> dicExecBill)
        {
            #region SQL

            //{CB77A6D3-A74C-4ba4-BDEC-027F992A198C}
            String sql = @"select distinct a.exec_sqn,
                               a.inpatient_no,
                               c.patient_no,
                               c.name,
                               c.bed_no,
                               f.type_code,
                               f.type_name,
                               f.decmps_state,
                               a.comb_no,
                               a.prn_exelist 是否打印,
                               a.drug_code,
                               a.drug_name,
                               a.specs,
                               a.dose_once,
                               a.dose_unit,
                               a.qty_tot,
                               a.price_unit,
                               a.usage_code,
                               a.use_name,
                               a.frequency_code,
                               a.frequency_name,
                               f.mo_date,
                               f.dc_date,
                               f.date_bgn,
                               f.date_end,
                               a.use_time,
                               a.charge_state,
                               (select sum(f.qty) from fin_ipb_medicinelist f
                                where f.mo_exec_sqn=a.exec_sqn
                                and f.inpatient_no=a.inpatient_no
                                and f.mo_order=a.mo_order) 收费表数量,
                               (select distinct g.drug_dept_code from pha_com_applyout g
                               where g.mo_order=a.mo_order
                               and g.exec_sqn=a.exec_sqn) 发药药房,
                               --a.pharmacy_code,
                               a.exec_dpcd,
                               a.exec_flag,
                               a.charge_flag,
                               a.mo_note2 备注,
                               f.subcombno,f.sort_id,
                               f.list_dpcd,f.doc_code,
                               b.bill_no,
                               b.bill_name,
                               a.item_price,
                               1 药品标记,
                               a.mo_order,
                               g.sort_id bed_sort,
                               f.mo_stat,
                               f.hypotest
                               --1、只要执行状态或收费的就打印出来，退费的显示“退”，作废的显示“废”，医嘱停止之后的显示“停”
                          from met_ipm_execdrug a, 
                               (select t2.item_flag,t2.bill_name, t1.*
                                  from met_ipm_drugbilldetail t1, met_ipm_execbill t2
                                 where t1.bill_no = t2.bill_no) b,
                               fin_ipr_inmaininfo c,met_ipm_order f,com_bedinfo g
                         where a.inpatient_no = c.inpatient_no
                           and a.inpatient_no in ({0})--住院号
                           and b.bill_no IN({1}) --执行单号
                           and a.type_code = b.type_code --医嘱类别
                           and a.drug_type = b.drug_type  --药品类别
                           and a.usage_code = b.usage_code --用法
                           and a.prn_exelist = '{2}'  --是否打印
                           and a.VALID_FLAG = fun_get_valid --有效医嘱状态
                           and a.mo_order=f.mo_order
                           and c.bed_no=g.bed_no
                           and ((a.exec_flag = '1' and a.decmps_state = 1) 
                               or a.decmps_state = 0) --执行标记（临嘱终端确认的项目不是执行标记）
                           and a.use_time >=
                               to_date('{3}', 'yyyy-mm-dd hh24:mi:ss') --使用时间
                           and a.use_time <=
                               to_date('{4}', 'yyyy-mm-dd hh24:mi:ss')
                        union all

                        select distinct a.exec_sqn,
                               c.inpatient_no,
                               c.patient_no,
                               c.name,
                               c.bed_no,
                               f.type_code,
                               f.type_name,
                               f.decmps_state,
                               a.comb_no,
                               a.prn_exelist 是否打印,
                               a.undrug_code,
                               a.undrug_name,
                               (select o.specs from fin_com_undruginfo o
                               where o.item_code=a.undrug_code) 规格,
                               a.qty_tot 每次量,
                               a.stock_unit 每次量单位,
                               a.qty_tot,
                               a.stock_unit,
                               '' 用法编码,
                               '' 用法,
                               a.dfq_freq,
                               a.dfq_cexp,
                               f.mo_date,
                               f.dc_date,
                               f.date_bgn,
                               f.date_end,
                               a.use_time,
                               a.charge_state,
                               (select sum(f.qty) from fin_ipb_itemlist f
                                where f.mo_exec_sqn=a.exec_sqn
                                and f.inpatient_no=a.inpatient_no
                                and f.mo_order=a.mo_order) 收费表数量,
                               (select distinct g.drug_dept_code from pha_com_applyout g
                               where g.mo_order=a.mo_order
                               and g.exec_sqn=a.exec_sqn) 发药药房,
                               --a.pharmacy_code,
                               a.exec_dpcd,
                               a.exec_flag,
                               a.charge_flag,
                               a.mo_note2 备注,
                               f.subcombno,f.sort_id,
                               f.list_dpcd,f.doc_code,
                               b.bill_no,
                               b.bill_name,
                               a.unit_price,
                               0 药品标记,
                               a.mo_order,
                               g.sort_id bed_sort,
                               f.mo_stat,
                               f.hypotest
                               --1、只要执行状态或收费的就打印出来，退费的显示“退”，作废的显示“废”，医嘱停止之后的显示“停”
                         from met_ipm_execundrug a,
                               (select t2.item_flag,t2.bill_name, t1.*
                                  from met_ipm_undrugbilldetail t1, met_ipm_execbill t2
                                 where t1.bill_no = t2.bill_no) b,
                               fin_ipr_inmaininfo c,met_ipm_order f,com_bedinfo g
                         where a.inpatient_no = c.inpatient_no
                           --and a.subtbl_flag = '0'
                           and a.inpatient_no in ({0})
                           and b.bill_no in({1})
                           and a.valid_flag = '1'
                           and a.type_code = b.type_code
                           and a.mo_order=f.mo_order
                           and c.bed_no=g.bed_no
                           and a.class_code = b.class_code
                           and ((((a.undrug_code = b.item_code and b.item_code!='999')
                               or (b.item_code='999' and b.item_name=a.undrug_name))
                               AND b.item_code IS NOT NULL) OR
                               (b.item_code IS NULL and not exists
                                (select g.item_code
                                    from met_ipm_undrugbilldetail g
                                   where ((g.item_code = a.undrug_code and g.item_code!='999')
                                         or (g.item_code='999' and g.item_name=a.undrug_name))
                                     and g.bill_no !=b.bill_no
                                     AND g.NURSE_CELL_CODE = c.NURSE_CELL_CODE)))
                           and a.use_time >=
                               to_date('{3}', 'yyyy-mm-dd hh24:mi:ss')
                           and a.use_time <=
                               to_date('{4}', 'yyyy-mm-dd hh24:mi:ss')
                           and a.prn_exelist = '{2}'
                           and a.VALID_FLAG = fun_get_valid
                           and ((a.exec_flag = '1' and a.decmps_state = 1) or a.decmps_state = 0)
                         order by bed_sort,patient_no,use_time,comb_no,sort_id
                            ";
            #endregion

            if (printType != "0" && printType != "1")
            {
                printType = "All";
            }

            try
            {
                sql = string.Format(sql, inpatientNos, execBillNos, printType, beginDate.ToString(), endDate.ToString());

                if (this.ExecQuery(sql) == -1)
                {
                    return null;
                }

                hsQuitFeeOrder = new Hashtable();

                ArrayList alExecOrder = new ArrayList();

                dicExecBill = new Dictionary<string, FS.FrameWork.Models.NeuObject>();

                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Order.ExecOrder execOrder = new FS.HISFC.Models.Order.ExecOrder();
                    execOrder.ID = Reader[0].ToString();
                    execOrder.Order.Patient.ID = Reader[1].ToString();
                    execOrder.Order.Patient.PID.PatientNO = Reader[2].ToString();
                    execOrder.Order.Patient.Name = Reader[3].ToString();
                    execOrder.Order.Patient.PVisit.PatientLocation.Bed.ID = Reader[4].ToString();
                    execOrder.Order.OrderType.ID = Reader[5].ToString();
                    execOrder.Order.OrderType.Name = Reader[6].ToString();
                    execOrder.Order.OrderType.IsDecompose = FS.FrameWork.Function.NConvert.ToBoolean(Reader[7]);
                    execOrder.Order.Combo.ID = Reader[8].ToString();
                    //是否打印执行单
                    execOrder.Order.Item.ID = Reader[10].ToString();
                    execOrder.Order.Item.Name = Reader[11].ToString();
                    execOrder.Order.Item.Specs = Reader[12].ToString();
                    execOrder.Order.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(Reader[13]);
                    execOrder.Order.DoseUnit = Reader[14].ToString();
                    execOrder.Order.Qty = FS.FrameWork.Function.NConvert.ToDecimal(Reader[15]);
                    execOrder.Order.Unit = Reader[16].ToString();
                    execOrder.Order.Usage.ID = Reader[17].ToString();
                    execOrder.Order.Usage.Name = Reader[18].ToString();
                    execOrder.Order.Frequency.ID = Reader[19].ToString();
                    execOrder.Order.Frequency.Name = Reader[20].ToString();
                    execOrder.Order.MOTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[21]);
                    execOrder.Order.DCOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[22]);
                    execOrder.Order.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[23]);
                    execOrder.Order.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[24]);
                    execOrder.DateUse = FS.FrameWork.Function.NConvert.ToDateTime(Reader[25]);
                    execOrder.Order.OrderType.IsCharge = FS.FrameWork.Function.NConvert.ToBoolean(Reader[26]);

                    execOrder.Order.StockDept.ID = Reader[28].ToString();
                    execOrder.Order.ExeDept.ID = Reader[29].ToString();
                    execOrder.IsExec = FS.FrameWork.Function.NConvert.ToBoolean(Reader[30]);
                    execOrder.IsCharge = FS.FrameWork.Function.NConvert.ToBoolean(Reader[31]);
                    execOrder.Order.Memo = Reader[32].ToString();
                    execOrder.Order.SubCombNO = FS.FrameWork.Function.NConvert.ToInt32(Reader[33]);
                    execOrder.Order.SortID = FS.FrameWork.Function.NConvert.ToInt32(Reader[34]);

                    execOrder.Order.ReciptDept.ID = Reader[35].ToString();
                    execOrder.Order.ReciptDoctor.ID = Reader[36].ToString();

                    if (!dicExecBill.ContainsKey(execOrder.ID))
                    {
                        dicExecBill.Add(execOrder.ID, new FS.FrameWork.Models.NeuObject(Reader[37].ToString(), Reader[38].ToString(), ""));
                    }

                    execOrder.Order.Item.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[39]);

                    if (FS.FrameWork.Function.NConvert.ToInt32(Reader[40]) == 1)
                    {
                        execOrder.Order.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.Drug;
                    }
                    else
                    {
                        execOrder.Order.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
                    }

                    execOrder.Order.ID = Reader[41].ToString();
                    execOrder.Order.Patient.PVisit.PatientLocation.Bed.SortID = FS.FrameWork.Function.NConvert.ToInt32(Reader[42]);
                    execOrder.Order.Status = FS.FrameWork.Function.NConvert.ToInt32(Reader[43]);
                    //{CB77A6D3-A74C-4ba4-BDEC-027F992A198C}
                    if (!string.IsNullOrEmpty((Reader[44]).ToString()))
                    {
                        execOrder.Order.HypoTest = (FS.HISFC.Models.Order.EnumHypoTest)Enum.Parse(typeof(FS.HISFC.Models.Order.EnumHypoTest), (Reader[44]).ToString());
                    }

                    //是否退费标记
                    //if (execOrder.Order.OrderType.IsCharge && execOrder.Order.Item.Price > 0)
                    //{
                    //    if (FS.FrameWork.Function.NConvert.ToDecimal(Reader[27]) <= 0)
                    //    {
                    //        if (!hsQuitFeeOrder.Contains(execOrder.ID))
                    //        {
                    //            hsQuitFeeOrder.Add(execOrder.ID, null);
                    //        }
                    //    }
                    //}

                    alExecOrder.Add(execOrder);
                }

                return alExecOrder;
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return null;
            }
        }



        public ArrayList GetZLPrint(string inpatientNos)
        {
//            string sql = @"   
//                        select 
//                           o.mo_order, 
//                           info.inpatient_no,
//                           info.patient_no,
//                           info.name,
//                           info.bed_no,
//                           info.DEPT_NAME,
//                           o.frequency_name,
//                           o.item_name,
//                           o.item_code,
//                           o.date_bgn 
//                        from met_ipm_order o
//                        left join fin_ipr_inmaininfo info on o.inpatient_no=info.inpatient_no
//                        left join com_dictionary t on t.type='TREATMENTPRINT' and o.item_code=t.code
//                        where o.INPATIENT_NO in ({0}) and t.code is not null";



            string sql = "";
            if (this.Sql.GetCommonSql("InPatient.LocalManager.PrintZL.Select", ref sql) == -1) return null;

            try
            {
                sql = string.Format(sql, inpatientNos);

                if (this.ExecQuery(sql) == -1)
                {
                    return null;
                }
                ArrayList alExecOrder = new ArrayList();

                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Order.ExecOrder execOrder = new FS.HISFC.Models.Order.ExecOrder();
                    execOrder.ID = Reader[0].ToString();
                    execOrder.Order.Patient.ID = Reader[1].ToString();
                    execOrder.Order.Patient.PID.PatientNO = Reader[2].ToString();
                    execOrder.Order.Patient.Name = Reader[3].ToString();
                    execOrder.Order.Patient.PVisit.PatientLocation.Bed.ID = Reader[4].ToString();
                    execOrder.Order.Patient.PVisit.PatientLocation.Dept.Name = Reader[5].ToString();
                    execOrder.Order.Frequency.Name = Reader[6].ToString();
                    execOrder.Order.Item.Name = Reader[7].ToString();
                    execOrder.Order.Item.ID = Reader[8].ToString();
                    execOrder.Order.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[9]);
                    execOrder.Order.Memo = Reader[10].ToString();
                    alExecOrder.Add(execOrder);
                }

                return alExecOrder;

            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return null;
            }
        }
    }
}
