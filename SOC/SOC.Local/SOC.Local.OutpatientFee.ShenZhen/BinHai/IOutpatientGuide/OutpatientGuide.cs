using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Windows.Forms;
namespace FS.SOC.Local.OutpatientFee.ShenZhen.BinHai.IOutpatientGuide
{
   public class OutpatientGuide : FS.HISFC.BizProcess.Interface.Fee.IOutpatientGuide
    {

        /// <summary>
        /// 将复合项目明细费用转换成费用复合项目费用
        /// </summary>
        /// <param name="feeDetails"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public  ArrayList ConvertItemToPackage(ArrayList feeDetails, ref string errorInfo)
        {
            ArrayList itemList = new ArrayList();
            Dictionary<string, FS.HISFC.Models.Fee.Outpatient.FeeItemList> packageItem = new Dictionary<string, FS.HISFC.Models.Fee.Outpatient.FeeItemList>();
            string packageOrderID = string.Empty;
            decimal Price =0;
            FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();
            FS.HISFC.BizLogic.Fee.UndrugPackAge packageManager = new FS.HISFC.BizLogic.Fee.UndrugPackAge();

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
                        //取费用表当时发生金额
                        foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemLists in feeDetails)
                        {
                            if (feeItemLists.UndrugComb.ID == feeItem.UndrugComb.ID && feeItemLists.Order.ID == feeItem.Order.ID)
                            {
                                Price += feeItemLists.Item.Price; 
                            }
                        }
                        feeItem.Item.Price = Price;//packageManager.GetUndrugCombPrice(feeItemList.UndrugComb.ID);
                        Price = 0;
                        FS.HISFC.Models.Fee.Item.UndrugComb undrugComb = packageManager.GetUndrugComb(feeItemList.UndrugComb.ID, feeItemList.Item.ID);
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
                        itemList.Add(feeItem);
                        packageItem[packageOrderID] = null;
                    }
                }
            }

            return itemList;
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
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alFeeDetail)
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
                    //(alFeeDetail[j] as FS.HISFC.Models.Fee.Outpatient.FeeItemList).Invoice.Type.Name = fee_stat_name[(alFeeDetail[j] as FS.HISFC.Models.Fee.Outpatient.FeeItemList).Invoice.Type.ID].ToString();
                    if (InvoiceType == (alFeeDetail[j] as FS.HISFC.Models.Fee.Outpatient.FeeItemList).Invoice.Type.Name)
                    {
                        feeDetailSort.Add(alFeeDetail[j]); 
                    } 
                }
            }

            return feeDetailSort;

        }

        ucFeeDetailGuideList ucFeeDetail = new ucFeeDetailGuideList();
        FS.SOC.Local.OutpatientFee.ShenZhen.BinHai.IOutpatientGuide.ucFeeDetailPricedList ucPricedList = new ucFeeDetailPricedList();
        FS.HISFC.BizLogic.Manager.PageSize pageSizeManager = new FS.HISFC.BizLogic.Manager.PageSize();

        public FS.HISFC.Models.Base.PageSize GetPageSize()
        {
          //  FS.FrameWork.WinForms.Classes.Function.ShowControl(ucFeeDetail);
            FS.HISFC.Models.Base.PageSize pSize = pageSizeManager.GetPageSize("MZGuide");
            if (pSize == null)
            {
                pSize = new FS.HISFC.Models.Base.PageSize("MZGuide", 800, 600);
            }
            return pSize;
        }
        private FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();

        public Hashtable GetInvoiceTypeName()
        {
            Hashtable feecodestat = new Hashtable();
            string sql = "select  t.fee_code ,t.fee_stat_name from fin_com_feecodestat t where t.report_code='MZ01'";
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

            //return dbMgr.ExecSqlReturnOne(string.Format(sql, invoiceTypeID), "");
        }
       /// <summary>
       /// 是否收费
       /// </summary>
        private bool isCharge = false;
        #region IOutpatientGuide 成员

        public void Print()
        {
            if (isCharge)//收费打印
            {
                FS.HISFC.Models.Base.PageSize pageSize = this.GetPageSize();
                //使用FS默认打印方式
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize();//使用默认的A4纸张
                }
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.SetPageSize(pageSize);
                print.PrintPage(pageSize.Left, pageSize.Top, ucFeeDetail);
            }
            else//划价打印
            {
                FS.HISFC.BizLogic.Manager.PageSize pageSizeManager = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize pgSize = pageSizeManager.GetPageSize("MZJZD");
                if (pgSize == null)
                {
                    pgSize = new FS.HISFC.Models.Base.PageSize("MZJZD", 690, 980);//700,1000
                    pgSize.Top = 0;
                    pgSize.Left = 0;
                }
                FS.FrameWork.WinForms.Classes.Print printC = new FS.FrameWork.WinForms.Classes.Print();
                printC.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                printC.SetPageSize(pgSize);
                //print.PageLabel = label1;
                //print.PrintPage(pgSize.Left, pgSize.Top, ucPricedList);
                printC.PrintPreview(pgSize.Left, pgSize.Top, ucPricedList);
            }
        }

        public void SetValue(FS.HISFC.Models.Registration.Register rInfo, ArrayList invoices, ArrayList feeDetails)
        {
            if (invoices != null)//收费有发票
            {
                this.isCharge = true;
                string errorInfo = string.Empty;
                ArrayList itemList = this.ConvertItemToPackage(feeDetails, ref errorInfo);
                if (itemList == null)
                {
                    MessageBox.Show(errorInfo);
                    return;
                }
                ArrayList ItemListSort = GetLisGroupItem(itemList);
                ucFeeDetail.SetValue(rInfo, invoices, ItemListSort);
            }
            else//划价无发票
            {
                this.isCharge = false;
                string errorInfo = string.Empty;
                ArrayList itemList = this.ConvertItemToPackage(feeDetails, ref errorInfo);
                if (itemList == null)
                {
                    MessageBox.Show(errorInfo);
                    return;
                }
                ArrayList ItemListSort = GetLisGroupItem(itemList);

                ucPricedList.IsGuidePrint = false;
                ucPricedList.SetValue(rInfo, invoices, ItemListSort);
            }
        }

        #endregion
    }
}
