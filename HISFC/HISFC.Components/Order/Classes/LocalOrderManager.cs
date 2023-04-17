using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Order.Classes
{
    /// <summary>
    /// 本地医嘱管理
    /// </summary>
    public class LocalOrderManager : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 用于存储本次保存的药品累加总量
        /// </summary>
        private Dictionary<string, decimal> dicDrugQty = new Dictionary<string, decimal>();

        /// <summary>
        /// 用于存储本次保存的药品累加总量
        /// </summary>
        public Dictionary<string, decimal> DicDrugQty
        {
            get
            {
                return dicDrugQty;
            }
            set
            {
                dicDrugQty = value;
            }
        }

        /// <summary>
        /// 检查医嘱
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="isExec">是否执行档</param>
        /// <returns></returns>
        public int CheckOrder(FS.HISFC.Models.Order.Inpatient.Order inOrder, bool isExec)
        {
            if (inOrder.Item.ID == "999")
            {
                return 1;
            }

            if (!inOrder.OrderType.IsCharge)
            {
                return 1;
            }

            if (!isExec)
            {
                if (inOrder.Status != 0
                    && inOrder.Status != 5
                    && inOrder.Status != 6)
                {
                    return 1;
                }
            }

            string sql_drug = @"select f.drug_dept_code 药房,
                               f.drug_code 药品编码,
                               (select o.valid_state from pha_com_baseinfo o
                               where o.drug_code=f.drug_code) 全院停用,
                               f.valid_state 药房停用,
                               f.outpatient_use_flag 门诊使用,
                               f.inpatient_use_flag 住院使用,
                               f.lack_flag 门诊缺药,
                               f.lack_inpatient_flag 住院缺药,  
                               f.store_sum 库存数量
                               
                        from PHA_COM_STOCKINFO f
                        where f.drug_dept_code='{1}'--药房编码
                        and f.drug_code='{0}'--药品编码"
                            ;

            string sql_undrug = @"select f.item_code 项目编码,
                                       f.item_name 项目名称,
                                       f.valid_state 有效性       
                                from fin_com_undruginfo f
                                where f.item_code ='{0}'";

            try
            {
                if (inOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    sql_drug = string.Format(sql_drug, inOrder.Item.ID, inOrder.StockDept.ID);
                    ExecQuery(sql_drug);
                    FS.HISFC.Models.Pharmacy.Storage storage = null;
                    while (Reader.Read())
                    {
                        storage = new FS.HISFC.Models.Pharmacy.Storage();

                        storage.StockDept.ID = Reader[0].ToString();
                        storage.Item.ID = Reader[1].ToString();
                        storage.Item.ValidState = (Reader[2].ToString() == "1" ? EnumValidState.Valid :

EnumValidState.Invalid);
                        storage.ValidState = (Reader[3].ToString() == "1" ? EnumValidState.Valid :

EnumValidState.Invalid);

                        if (Reader[4].ToString() == "0")
                        {
                            storage.IsUseForOutpatient = false;
                        }
                        if (Reader[5].ToString() == "0")
                        {
                            storage.IsUseForInpatient = false;
                        }
                        if (Reader[6].ToString() == "0")
                        {
                            storage.IsLack = false;
                        }
                        if (Reader[7].ToString() == "0")
                        {
                            storage.IsLackForInpatient = false;
                        }
                        storage.StoreQty = FS.FrameWork.Function.NConvert.ToDecimal(Reader[8]);

                        break;
                    }
                    if (storage == null)
                    {
                        Err = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(inOrder.StockDept.ID) + "不存在该药品";
                        return -1;
                    }

                    if (storage.Item.ValidState == EnumValidState.Invalid)
                    {
                        Err = "药库已停用";
                        return -1;
                    }
                    if (storage.Item.ValidState == EnumValidState.Invalid)
                    {
                        Err = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(storage.StockDept.ID) + "已停用";
                        return -1;
                    }
                    if (!storage.IsUseForInpatient)
                    {
                        Err = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(storage.StockDept.ID) + " 设置不允许住院使用";
                        return -1;
                    }
                    if (storage.IsLackForInpatient)
                    {
                        Err = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(storage.StockDept.ID) + " 设置住院缺药";
                        return -1;
                    }

                    decimal qty = inOrder.Item.Qty;
                    if (dicDrugQty.ContainsKey(inOrder.Item.ID))
                    {
                        qty = dicDrugQty[inOrder.Item.ID] + inOrder.Item.Qty;
                    }

                    if (inOrder.OrderType.IsDecompose && !isExec)
                    {
                    }
                    else
                    {
                        if (storage.StoreQty < qty)
                        {
                            Err = "库存不足";
                            return -1;
                        }
                    }

                    if (!dicDrugQty.ContainsKey(inOrder.Item.ID))
                    {
                        dicDrugQty.Add(inOrder.Item.ID, inOrder.Item.Qty);
                    }
                    else
                    {
                        dicDrugQty[inOrder.Item.ID] += inOrder.Item.Qty;
                    }
                }
                else
                {
                    sql_undrug = string.Format(sql_undrug, inOrder.Item.ID);
                    ExecQuery(sql_undrug);

                    if (Reader.Read())
                    {
                        if (Reader[2].ToString() != "1")
                        {
                            Err = "物价已停用";
                            return -1;
                        }
                    }
                    else
                    {
                        Err = "不存在该项目";
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return -1;
            }

            return 1;
        }



        #region 更新执行档扣库科室

        /// <summary>
        /// 更新执行档的取药科室
        /// </summary>
        /// <param name="execOrder"></param>
        /// <returns></returns>
        public int UpdateExecOrderStockDept(FS.HISFC.Models.Order.ExecOrder execOrder)
        {
            string sql = @"update met_ipm_execdrug f
                        set f.pharmacy_code='{1}'
                        where f.exec_sqn='{0}'";

            sql = string.Format(sql, execOrder.ID, execOrder.Order.StockDept.ID);

            return ExecNoQuery(sql);
        }

        #endregion
    }
}
