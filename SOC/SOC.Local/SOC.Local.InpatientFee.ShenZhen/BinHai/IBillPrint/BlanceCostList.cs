using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using Neusoft.SOC.HISFC.InpatientFee.Interface;

namespace Neusoft.SOC.Local.InpatientFee.ShenZhen.BinHai.IBillPrint
{
    class BlanceCostList : Neusoft.SOC.HISFC.InpatientFee.Interface.IBillPrint
    {

        #region BalanceCostList 变量
        ucBlanceCostList uBalanceCostList = new ucBlanceCostList();
        Neusoft.HISFC.BizLogic.Manager.PageSize pageSizeManager = new Neusoft.HISFC.BizLogic.Manager.PageSize();
        private Neusoft.FrameWork.Management.DataBaseManger dbMgr = new Neusoft.FrameWork.Management.DataBaseManger();
        private Neusoft.HISFC.BizLogic.Fee.FeeReport ReportFee = new Neusoft.HISFC.BizLogic.Fee.FeeReport();
        #endregion

        #region IBillPrint 成员

        public void Print()
        {
            Neusoft.HISFC.Models.Base.PageSize pageSize = this.GetPageSize();
            //使用Neusoft默认打印方式
            if (pageSize == null)
            {
                pageSize = new Neusoft.HISFC.Models.Base.PageSize();//使用默认的A4纸张
            }
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.SetPageSize(pageSize);
            print.PrintPreview(pageSize.Left, pageSize.Top, uBlanceList);
        }

        public int SetData(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, Neusoft.SOC.HISFC.InpatientFee.Interface.EnumPrintType printType, object t, ref string errInfo, params object[] appendParams)
        {
            throw new NotImplementedException();
        }

        public int SetData(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, object t, ref string errInfo, params object[] appendParams)
        {
            string errorInfo = string.Empty;
            if (patientInfo.ID == "")
            {
                errInfo = "请选择要打印的患者！";
                return -1;
            }
            ArrayList alBalance, alFeeitemlist, alFeeitemlists, alFeeDetail, alPaylist;
            alBalance = this.GetBalanceBill(patientInfo.ID);
            //非药品明细
            ArrayList alItemList = new ArrayList();
            //药品明细
            ArrayList alMedicineList = new ArrayList();
            //查询
            alItemList = (t as Neusoft.HISFC.BizLogic.Fee.InPatient).QueryItemListsForBalance(patientInfo.ID);

            if (alItemList == null)
            {
                errInfo = "查询患者非药品信息出错" + (t as Neusoft.HISFC.BizLogic.Fee.InPatient).Err;
                return -1;
            }

            alMedicineList = (t as Neusoft.HISFC.BizLogic.Fee.InPatient).QueryMedicineListsForBalance(patientInfo.ID);
            if (alMedicineList == null)
            {
                errInfo = "查询患者药品信息出错" + (t as Neusoft.HISFC.BizLogic.Fee.InPatient).Err;
                return -1;
            }
            alFeeitemlist = new ArrayList();
            //添加汇总信息
            alFeeitemlist.AddRange(alItemList);
            alFeeitemlist.AddRange(alMedicineList);
            alFeeitemlists = this.ConvertItemToPackage(alFeeitemlist, ref errorInfo);
            if (alFeeitemlists == null)
            {
                MessageBox.Show(errorInfo);
                errInfo = errorInfo;
                return -1;
            }
            alFeeDetail = this.GetLisGroupItem(alFeeitemlists);
            //alFeeitemlist = (t as Neusoft.HISFC.BizLogic.Fee.InPatient).QueryFeeInfosGroupByMinFeeByInpatientNO(patientInfo.ID, patientInfo.PVisit.InTime, "0");

            this.uBalanceCostList.SetValueForPrintNew(patientInfo, new Neusoft.HISFC.Models.Fee.Inpatient.Balance(), alBalance, alFeeDetail, new ArrayList());
            FrameWork.WinForms.Classes.Function.ShowControl(this.uBalanceCostList);  
            return 1;        
        }

        #endregion
        #region 函数
        public Neusoft.HISFC.Models.Base.PageSize GetPageSize()
        {
            Neusoft.HISFC.Models.Base.PageSize pSize = pageSizeManager.GetPageSize("ZYJS");
            if (pSize == null)
            {
                pSize = new Neusoft.HISFC.Models.Base.PageSize("ZYJS", 790, 1098);
            }
            return pSize;
        }
        /// <summary>
        /// 将复合项目明细费用转换成费用复合项目费用
        /// </summary>
        /// <param name="feeDetails"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        private ArrayList ConvertItemToPackage(ArrayList feeDetails, ref string errorInfo)
        {
            ArrayList itemList = new ArrayList();
            Dictionary<string, Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList> packageItem = new Dictionary<string, Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList>();
            string packageOrderID = string.Empty;
            decimal Price = 0;
            Neusoft.HISFC.BizLogic.Fee.Item itemManager = new Neusoft.HISFC.BizLogic.Fee.Item();
            Neusoft.HISFC.BizLogic.Fee.UndrugPackAge packageManager = new Neusoft.HISFC.BizLogic.Fee.UndrugPackAge();

            //将复合项目明细转换成复合项目
            foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeDetails)
            {
                if (string.Empty.Equals(feeItemList.UndrugComb.ID))
                {
                    itemList.Add(feeItemList);
                }
                else//复合项目合并
                {
                    packageOrderID = feeItemList.RecipeNO + feeItemList.UndrugComb.ID;
                    //医嘱号相同，复合项目编码相同合并
                    if (packageItem.ContainsKey(packageOrderID))
                    {
                        continue;
                    }
                    else
                    {

                        Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = feeItemList.Clone();
                        feeItem.Item = itemManager.GetUndrugByCode(feeItemList.UndrugComb.ID);
                        if (feeItem.Item == null)
                        {
                            errorInfo = "获取非药品项目：" + feeItemList.UndrugComb.ID + "失败，原因：" + itemManager.Err;
                            return null;
                        }
                        //计算数量
                        feeItem.Name = feeItem.Item.Name;
                        //取费用表当时发生金额
                        foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList feeItemLists in feeDetails)
                        {
                            if (feeItemLists.UndrugComb.ID == feeItem.UndrugComb.ID && feeItemLists.RecipeNO == feeItem.RecipeNO)
                            {
                                Price += feeItemLists.Item.Price;
                            }
                        }
                        feeItem.Item.Price = Price;//packageManager.GetUndrugCombPrice(feeItemList.UndrugComb.ID);
                        Price = 0;
                        Neusoft.HISFC.Models.Fee.Item.UndrugComb undrugComb = packageManager.GetUndrugComb(feeItemList.UndrugComb.ID, feeItemList.Item.ID);
                        try
                        {
                            feeItem.Item.Qty = feeItemList.Item.Qty / undrugComb.Qty;
                        }
                        catch
                        {
                            feeItem.Item.Qty = 1;
                        }
                        feeItem.UndrugComb.ID = string.Empty;
                        feeItem.UndrugComb.Name = string.Empty;
                        feeItem.Invoice.Type.Memo = "组套";
                        itemList.Add(feeItem);
                        packageItem[packageOrderID] = null;
                    }
                }
            }

            return itemList;
        }
        private ArrayList GetBalanceBill(string strPatientId)
        {
            ArrayList al = new ArrayList();
            DataSet dsItem;
            dsItem = this.ReportFee.GetTotBalanceBill(strPatientId);
            if (dsItem.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        Neusoft.HISFC.Models.Fee.Inpatient.FeeInfo info = new Neusoft.HISFC.Models.Fee.Inpatient.FeeInfo();
                        info = new Neusoft.HISFC.Models.Fee.Inpatient.FeeInfo();
                        info.FT.TotCost = Convert.ToDecimal(dsItem.Tables[0].Rows[i][0].ToString());//总费用
                        info.Item.MinFee.ID = dsItem.Tables[0].Rows[i][3].ToString();//最小费用  
                        al.Add(info);
                        info = null;
                    }
                    catch
                    {

                    }
                }
            }
            ArrayList feeinfoSort = new ArrayList();
            Hashtable fee_stat_name;
            fee_stat_name = this.GetInvoiceTypeName();
            foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeInfo feeItem in al)
            {

                feeItem.Invoice.Type.Name = fee_stat_name[feeItem.Item.MinFee.ID].ToString();
                feeinfoSort.Add(feeItem);
            }
            return feeinfoSort;

        }
        private Hashtable GetInvoiceTypeName()
        {
            Hashtable feecodestat = new Hashtable();
            string sql = "select  t.fee_code ,t.fee_stat_name from fin_com_feecodestat t where t.report_code='ZY01' order by t.print_order";
            DataSet dsItem = new DataSet();
            dbMgr.ExecQuery(sql, ref dsItem);
            if (dsItem.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        feecodestat.Add(dsItem.Tables[0].Rows[i][0].ToString(), dsItem.Tables[0].Rows[i][1].ToString());
                    }
                    catch
                    {

                    }
                }
            }
            return feecodestat;
        }
        /// <summary>
        /// 添加分组项目
        /// </summary>
        private ArrayList GetLisGroupItem(ArrayList alFeeDetail)
        {
            //分组显示
            int count = alFeeDetail.Count;
            int i = 0;
            string InvoiceType;
            ArrayList feeDetailSort = new ArrayList();
            Hashtable feeDetailType = new Hashtable();
            Hashtable fee_stat_name;
            fee_stat_name = this.GetInvoiceTypeName();
            foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in alFeeDetail)
            {
                feeItem.Invoice.Type.Name = fee_stat_name[feeItem.Item.MinFee.ID].ToString();
                if (feeDetailType.ContainsValue(feeItem.Invoice.Type.Name))  //(feeItem.Invoice.Type.ID))
                {
                    continue;
                }
                else
                {

                    feeDetailType.Add(i, feeItem.Invoice.Type.Name);
                    i++;
                }
            }
            for (i = 0; i < feeDetailType.Count; i++)
            {
                InvoiceType = feeDetailType[i].ToString();
                for (int j = 0; j < alFeeDetail.Count; j++)
                {
                    //(alFeeDetail[j] as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList).Invoice.Type.Name = fee_stat_name[(alFeeDetail[j] as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList).Invoice.Type.ID].ToString();
                    if (InvoiceType == (alFeeDetail[j] as Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList).Invoice.Type.Name)
                    {
                        feeDetailSort.Add(alFeeDetail[j]);
                    }
                }
            }

            return feeDetailSort;

        }
        /// <summary>
        /// 获取预结算信息
        /// </summary>
        private ArrayList GetbalanceList(Neusoft.HISFC.Models.RADT.PatientInfo PatientInfo,ArrayList feeinfo)
        {
            Neusoft.HISFC.Models.Fee.Inpatient.BalanceList tmpBalance = new Neusoft.HISFC.Models.Fee.Inpatient.BalanceList();
            ArrayList reBalanceList = new ArrayList();
            return reBalanceList;
        }
        #endregion 
    }
}
