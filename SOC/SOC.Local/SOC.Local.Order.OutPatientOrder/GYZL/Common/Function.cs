using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Order.OutPatientOrder.GYZL.Common
{
    public class Function : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 将复合项目明细费用转换成费用复合项目费用
        /// </summary>
        /// <param name="feeDetails"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public static ArrayList ConvertItemToPackage(ArrayList feeDetails, ref string errorInfo)
        {
            ArrayList itemList = new ArrayList();
            Dictionary<string, FS.HISFC.Models.Fee.Outpatient.FeeItemList> packageItem = new Dictionary<string, FS.HISFC.Models.Fee.Outpatient.FeeItemList>();
            string packageOrderID = string.Empty;

            FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();
            FS.HISFC.BizLogic.Fee.UndrugPackAge packageManager = new FS.HISFC.BizLogic.Fee.UndrugPackAge();
            //decimal priceTot = 0;
            //将复合项目明细转换成复合项目
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in feeDetails)
            {
                if (string.Empty.Equals(feeItemList.UndrugComb.ID))
                {
                    itemList.Add(feeItemList);
                }
                else//复合项目合并
                {
                    packageOrderID = feeItemList.Order.ID + feeItemList.UndrugComb.ID;
                    //医嘱号相同，复合项目编码相同合并
                    if (packageItem.ContainsKey(packageOrderID))
                    {
                        ((FS.HISFC.Models.Fee.Outpatient.FeeItemList)(packageItem[packageOrderID])).Item.Price += feeItemList.Item.Price;
                        continue;
                    }
                    else
                    {
                        FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem = feeItemList.Clone();
                        feeItem.Item = itemManager.GetUndrugByCode(feeItemList.UndrugComb.ID);
                        if (feeItem.Item == null)
                        {
                            errorInfo = "获取非药品项目：" + feeItemList.UndrugComb.ID + "失败，原因：" + itemManager.Err;
                            return null;
                        }
                        //计算数量
                        feeItem.Name = feeItem.Item.Name;
                        //feeItem.Item.Price = packageManager.GetUndrugCombPrice(feeItemList.UndrugComb.ID);
                        FS.HISFC.Models.Fee.Item.UndrugComb undrugComb = packageManager.GetUndrugComb(feeItemList.UndrugComb.ID, feeItemList.Item.ID);
                        feeItem.Item.Qty = feeItemList.Item.Qty / undrugComb.Qty;
                        feeItem.UndrugComb.ID = string.Empty;
                        feeItem.UndrugComb.Name = string.Empty;
                        feeItem.UndrugComb.Memo = "复合项";//标示为复合项，为了清单显示用
                        itemList.Add(feeItem);
                        packageItem[packageOrderID] = feeItemList;
                    }
                }
            }

            return itemList;
        }


        private string sql = @"select 
                                package_code, 
                                package_name, 
                                item_code, 
                                item_name, 
                                sort_id, 
                                qty, 
                                valid_state,
                                spell_code, 
                                wb_code, 
                                input_code, 
                                (select a.unit_price from fin_com_undruginfo a where a.item_code=fin_com_undrugztinfo.item_code) price,
                                (select b.unit_price1 from fin_com_undruginfo b where b.item_code=fin_com_undrugztinfo.item_code) childprice,
                                (select c.unit_price2 from fin_com_undruginfo c where c.item_code=fin_com_undrugztinfo.item_code) specialPrice,
                                (select f.empl_name from com_employee f where f.empl_code= fin_com_undrugztinfo.oper_code and f.valid_state='1'),
                                oper_date
                                from fin_com_undrugztinfo 
                                where  package_code='{0}' ";


        /// <summary>
        /// 获取非药品组套明细
        /// </summary>
        /// <param name="pcode">组套编码</param>
        /// <param name="pname">组套名称</param>
        /// <param name="listzt">结果集</param>
        /// <returns>1,成功; -1,失败</returns>
        public ArrayList QueryUnDrugztDetail(string packageCode)
        {
            string strsql = sql;

            try
            {
                strsql = String.Format(strsql, packageCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            if (this.ExecQuery(strsql) == -1)
            {
                return null;
            }
            try
            {
                ArrayList al = new ArrayList();
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Fee.Item.UndrugComb zt = new FS.HISFC.Models.Fee.Item.UndrugComb();
                    zt.Package.ID = this.Reader.IsDBNull(0) ? "" : this.Reader.GetString(0);
                    zt.Package.Name = this.Reader.IsDBNull(1) ? "" : this.Reader.GetString(1);
                    zt.ID = this.Reader.IsDBNull(2) ? "" : this.Reader.GetString(2);
                    zt.Name = this.Reader.IsDBNull(3) ? "" : this.Reader.GetString(3);
                    zt.SortID = this.Reader.IsDBNull(4) ? 0 : Convert.ToInt32(this.Reader.GetDecimal(4));
                    zt.Qty = this.Reader.IsDBNull(5) ? 0 : this.Reader.GetDecimal(5);
                    zt.ValidState = this.Reader.IsDBNull(6) ? "" : this.Reader.GetString(6);
                    zt.SpellCode = this.Reader.IsDBNull(7) ? "" : this.Reader.GetString(7);
                    zt.WBCode = this.Reader.IsDBNull(8) ? "" : this.Reader.GetString(8);
                    zt.UserCode = this.Reader.IsDBNull(9) ? "" : this.Reader.GetString(9);
                    zt.Memo = "11";//这是一个标志位,如果为11则,再操作时用update,否则用insert;
                    zt.Price = this.Reader.IsDBNull(10) ? 0 : Convert.ToDecimal(this.Reader.GetDecimal(10));
                    zt.ChildPrice = this.Reader.IsDBNull(11) ? 0 : Convert.ToDecimal(this.Reader.GetDecimal(11));
                    zt.SpecialPrice = this.Reader.IsDBNull(12) ? 0 : Convert.ToDecimal(this.Reader.GetDecimal(12));
                    zt.Oper.Name = this.Reader.IsDBNull(13) ? "" : this.Reader.GetString(13);
                    zt.Oper.OperTime = this.Reader.IsDBNull(14) ? DateTime.MinValue : this.Reader.GetDateTime(14);
                    al.Add(zt);
                }

                return al;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                if (this.Reader != null && this.Reader.IsClosed == false)
                {
                    this.Reader.Close();
                }
            }
        }

        #region 毒麻药品（麻醉、精一）流水号处理
        private string spSql = @"Update met_ord_recipedetail set APPLY_NO = '{2}'
                        where RECIPE_NO = '{0}' and RECIPE_SEQ = '{1}'";

        /// <summary>
        /// 毒麻药品获取年流水号与日流水号
        /// </summary>
        /// <returns></returns>
        private string ReturnSeqNo()
        {
            //开头 日期格式20120907
            string firstStr = string.Empty;
            DateTime dtTmp = System.DateTime.Now;
            firstStr = dtTmp.Year.ToString() + dtTmp.Month.ToString() + dtTmp.Day.ToString();

            //年流水号 一年内第几张毒麻处方
            string yearSeq = string.Empty;

            //日流水号 当日内第几张毒麻处方
            string daySeq = string.Empty;

            return "";
        }

        /// <summary>
        /// 更新毒麻药品流水号
        /// </summary>
        /// <param name="recipeNo"></param>
        /// <param name="recipeSeq"></param>
        /// <returns></returns>
        public int UpdateAnestheticsSeqNo(string recipeNo, string recipeSeq)
        {
            string atsSeqNo = this.ReturnSeqNo();
            string excSql = this.spSql;

            try
            {
                excSql = String.Format(excSql, recipeNo, recipeSeq, atsSeqNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecQuery(excSql);
        }
        #endregion
    }
}
