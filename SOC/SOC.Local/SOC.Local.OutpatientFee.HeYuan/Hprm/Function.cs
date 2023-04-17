using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.HISFC.Models.Base;

namespace FS.SOC.Local.OutpatientFee.HeYuan.Hprm
{
    /// <summary>
    /// 功能函数
    /// </summary>
    public class Function : FS.FrameWork.Management.Database
    {

        /// <summary>
        /// 返回项目比例
        /// </summary>
        /// <param name="pactId">合同单位编码</param>
        /// <param name="f">费用明细</param>
        /// <returns></returns>
        public static FS.HISFC.Models.Base.PactItemRate PactRate(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f, ref string errMsg)
        {
            FS.HISFC.Models.Base.PactItemRate pRate = new FS.HISFC.Models.Base.PactItemRate();
            pRate.Rate.RebateRate = f.Patient.Pact.Rate.RebateRate;
            return pRate;
        }


        /// <summary>
        /// 将复合项目明细费用转换成费用复合项目费用
        /// </summary>
        /// <param name="feeDetails"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public static ArrayList ConvertItemToPackage(ArrayList feeDetails,ref string errorInfo)
        {
            ArrayList itemList = new ArrayList();
            Dictionary<string, FS.HISFC.Models.Fee.Outpatient.FeeItemList> packageItem = new Dictionary<string, FS.HISFC.Models.Fee.Outpatient.FeeItemList>();
            string packageOrderID = string.Empty;

            FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();
            FS.HISFC.BizLogic.Fee.UndrugPackAge packageManager = new FS.HISFC.BizLogic.Fee.UndrugPackAge();
            //decimal priceTot = 0;
            ArrayList alFilled = new ArrayList();       //已经填充的项目
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
                       ( (FS.HISFC.Models.Fee.Outpatient.FeeItemList)(packageItem[packageOrderID])).Item.Price += feeItemList.Item.Price;
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
                        if (alFilled.Count > 0)
                        {
                            bool res = false;
                            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in alFilled)
                            {
                                if (item.RecipeNO == feeItemList.RecipeNO && item.UndrugComb.ID == feeItemList.UndrugComb.ID)
                                {
                                    res = true;
                                }
                            }
                            if (res)
                            {
                                continue;
                            }                            
                        }
                        alFilled.Add(feeItemList); 
                        //计算数量
                        feeItem.Name = feeItem.Item.Name;
                        //feeItem.Item.Price = packageManager.GetUndrugCombPrice(feeItemList.UndrugComb.ID);
                        FS.HISFC.Models.Fee.Item.UndrugComb undrugComb = packageManager.GetUndrugComb(feeItemList.UndrugComb.ID, feeItemList.Item.ID);
                        feeItem.Item.Qty = feeItemList.Item.Qty / undrugComb.Qty;
                        feeItem.UndrugComb.ID = string.Empty;
                        feeItem.UndrugComb.Name = string.Empty;
                        //feeItem.UndrugComb.Name = undrugComb.Name;
                        //feeItem.UndrugComb.ID = undrugComb.ID;
                        feeItem.UndrugComb.Memo = "复合项";//标示为复合项，为了清单显示用
                        itemList.Add(feeItem);
                        packageItem[packageOrderID] = feeItemList;
                    }
                }
            }                        
            return itemList;
        }
               

        private  string sql = @"select 
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
        public  ArrayList QueryUnDrugztDetail(string packageCode)
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

        public int RecomputeFeeItemListOutPatient(FS.HISFC.Models.Registration.Register register, ref FS.HISFC.Models.Fee.Outpatient.FeeItemList item,ref string errorInfo)
        {
            FS.HISFC.BizLogic.Fee.Interface mySIinterface = new FS.HISFC.BizLogic.Fee.Interface();
            int returnValue = 0;

            FS.HISFC.Models.SIInterface.Compare objCompare = new FS.HISFC.Models.SIInterface.Compare();

            returnValue = mySIinterface.GetCompareSingleItem(register.Pact.ID, item.Item.ID, ref objCompare);

            if (returnValue == -1)
            {
                errorInfo= mySIinterface.Err;
                return returnValue;
            }
            if (returnValue == -2)
            {
                objCompare.CenterItem.Rate = 1;
            }
            item.Compare.CenterItem.ItemGrade = objCompare.CenterItem.ItemGrade;
            item.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(item.FT.TotCost * objCompare.CenterItem.Rate, 2);
            item.FT.PubCost = item.FT.TotCost - item.FT.OwnCost;
            item.FT.PayCost = 0;
            item.FT.DerateCost = 0;//重算不考虑减免金额

            return 0;
        }


        public static bool IsDrug(EnumItemType itemType, FS.FrameWork.Models.NeuObject minFee)
        {
            if (itemType == EnumItemType.Drug)
            {
                return true;
            }
            else if (minFee == null)
            {
                return false;
            }
            else if ("001".Equals(minFee.ID) || "002".Equals(minFee.ID) || "003".Equals(minFee.ID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
