using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.InpatientFee.Interface;
using System.Data;
namespace FS.SOC.Local.InpatientFee.ShenZhen.BinHai.IBillPrint
{
    class BlanceList : FS.SOC.HISFC.InpatientFee.Interface.IBillPrint
    {

        #region BlanceList 变量
        ucBlanceList uBlanceList = new ucBlanceList();
        FS.HISFC.BizLogic.Manager.PageSize pageSizeManager = new FS.HISFC.BizLogic.Manager.PageSize();
        private FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
        private   FS.HISFC.BizLogic.Fee.FeeReport ReportFee= new   FS.HISFC.BizLogic.Fee.FeeReport();

       // private FS.HISFC.BizLogic.Fee.InPatient inpatientFeeManager = new FS.HISFC.BizLogic.Fee.InPatient();
        #endregion
        #region IBillPrint 成员

        public void Print()
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
            print.PrintPreview(pageSize.Left, pageSize.Top, uBlanceList);
        }

        public int SetData(FS.HISFC.Models.RADT.PatientInfo patientInfo, EnumPrintType printType, object t, ref string errInfo, params object[] appendParams)
        {

            throw new NotImplementedException();
           
        }

        public int SetData(FS.HISFC.Models.RADT.PatientInfo patientInfo, object t, ref string errInfo, params object[] appendParams)
        { 
            string errorInfo= string.Empty;
            //FS.HISFC.Models.Fee.Inpatient.FeeItemList
            if (patientInfo.ID =="")
            {
                errInfo = "请选择要打印的患者！";
                return -1;
            }
            //没刷医保卡没获取患者信息
            FS.HISFC.Models.RADT.PatientInfo PatientInfo =  new FS.HISFC.Models.RADT.PatientInfo();
            FS.SOC.HISFC.InpatientFee.BizProcess.RADT radtMgr = new FS.SOC.HISFC.InpatientFee.BizProcess.RADT();
            PatientInfo = radtMgr.GetPatientInfo(patientInfo.ID);
            ArrayList alBalance,alFeeitemlist,alFeeitemlists,alFeeDetail,alPaylist;
            alBalance = this.GetBalanceBill(t, patientInfo.ID, appendParams);
           //非药品明细
           ArrayList alItemList = new ArrayList();
           //药品明细
           ArrayList alMedicineList = new ArrayList();

           if (appendParams.Length == 1)
           {
               if (patientInfo.PVisit.InState.ID.ToString() == "O")
               {
                   //alMedicineList = (t as FS.HISFC.BizLogic.Fee.InPatient).GetMedItemsForInpatient(patientInfo.ID, patientInfo.PVisit.InTime, patientInfo.PVisit.OutTime);
                   //alItemList  = (t as FS.HISFC.BizLogic.Fee.InPatient).QueryFeeItemLists(patientInfo.ID, patientInfo.PVisit.InTime, patientInfo.PVisit.OutTime);
                   alMedicineList = (t as FS.HISFC.BizLogic.Fee.InPatient).GetMedItemsForInpatient(patientInfo.ID, patientInfo.PVisit.InTime, patientInfo.BalanceDate);
                   alItemList = (t as FS.HISFC.BizLogic.Fee.InPatient).QueryFeeItemLists(patientInfo.ID, patientInfo.PVisit.InTime, patientInfo.BalanceDate);
               } 
               else
               { 
                   alItemList = (t as FS.HISFC.BizLogic.Fee.InPatient).QueryItemListsForBalance(patientInfo.ID);
                   alMedicineList = (t as FS.HISFC.BizLogic.Fee.InPatient).QueryMedicineListsForBalance(patientInfo.ID);
               }
           }
           else
           {
               alItemList = (t as FS.HISFC.BizLogic.Fee.InPatient).QueryItemListsForBalance(patientInfo.ID, (DateTime)appendParams[1], (DateTime)appendParams[2]);
               alMedicineList = (t as FS.HISFC.BizLogic.Fee.InPatient).QueryMedicineListsForBalance(patientInfo.ID, (DateTime)appendParams[1], (DateTime)appendParams[2]);

           }

               ////查询
               //if (appendParams.Length ==1 )
               //{
               //    alItemList = (t as FS.HISFC.BizLogic.Fee.InPatient).QueryItemListsForBalance(patientInfo.ID);
               //    alMedicineList = (t as FS.HISFC.BizLogic.Fee.InPatient).QueryMedicineListsForBalance(patientInfo.ID);
               //}
               //else
               //{
               //    alItemList = (t as FS.HISFC.BizLogic.Fee.InPatient).QueryItemListsForBalance(patientInfo.ID, (DateTime)appendParams[1], (DateTime)appendParams[2]);
               //    alMedicineList = (t as FS.HISFC.BizLogic.Fee.InPatient).QueryMedicineListsForBalance(patientInfo.ID, (DateTime)appendParams[1], (DateTime)appendParams[2]);
        
               //}

           if (alItemList == null)
           {
               errInfo = "查询患者非药品信息出错" + (t as FS.HISFC.BizLogic.Fee.InPatient).Err;
               return -1;
           }
           if (alMedicineList == null)
           {
               errInfo = "查询患者药品信息出错" + (t as FS.HISFC.BizLogic.Fee.InPatient).Err;
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
            this.uBlanceList.SetValueForPrint(PatientInfo, new FS.HISFC.Models.Fee.Inpatient.Balance(), alBalance, alFeeDetail, new ArrayList());
            //FrameWork.WinForms.Classes.Function.ShowControl(this.uBlanceList);  
            return 1;
        }

        #endregion
        #region 函数
        public FS.HISFC.Models.Base.PageSize GetPageSize()
        {
            FS.HISFC.Models.Base.PageSize pSize = pageSizeManager.GetPageSize("ZYJS");
            if (pSize == null)
            {
                pSize = new FS.HISFC.Models.Base.PageSize("ZYJS", 790, 1098);
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
            Dictionary<string, FS.HISFC.Models.Fee.Inpatient.FeeItemList> packageItem = new Dictionary<string, FS.HISFC.Models.Fee.Inpatient.FeeItemList>();
            string packageOrderID = string.Empty;
            decimal Price = 0;
            FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();
            FS.HISFC.BizLogic.Fee.UndrugPackAge packageManager = new FS.HISFC.BizLogic.Fee.UndrugPackAge();

            //将复合项目明细转换成复合项目
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeDetails)
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
                        itemList.Add(feeItemList);
                    }
                    else
                    {
                       
                        FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = feeItemList.Clone();
                        feeItem.Item = itemManager.GetUndrugByCode(feeItemList.UndrugComb.ID);
                        if (feeItem.Item == null)
                        {
                            errorInfo = "获取非药品项目：" + feeItemList.UndrugComb.ID + "失败，原因：" + itemManager.Err;
                            return null;
                        }
                        //计算数量
                        feeItem.Name = feeItem.Item.Name;
                        //取费用表当时发生金额
                        foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemLists in feeDetails)
                        {
                            if (feeItemLists.UndrugComb.ID == feeItem.UndrugComb.ID && feeItemLists.RecipeNO == feeItem.RecipeNO)
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
                        feeItem.Invoice.Type.Memo = "组套";
                        itemList.Add(feeItem);
                        packageItem[packageOrderID] = null;
                        itemList.Add(feeItemList);
                    }
                }
            }

            return itemList;
        }
        //将最小费用汇总按财务大类汇总合计
        private ArrayList GetBalanceBill(object t,string strPatientId, params object[] appendParams)
        {
            ArrayList al = new ArrayList();
            if (appendParams.Length == 1)
            {
                al = (t as FS.HISFC.BizLogic.Fee.InPatient).QueryFeeInfosAndChangeCostGroupByMinFeeByInpatientNO(strPatientId,appendParams[0].ToString());
            }
            else
            {
                al = (t as FS.HISFC.BizLogic.Fee.InPatient).QueryFeeInfosGroupByMinFeeByInpatientNO(strPatientId, (DateTime)appendParams[1], (DateTime)appendParams[2],appendParams[0].ToString());
            }
            ArrayList feeinfoSort = new ArrayList();
            Hashtable fee_stat_name;
            Hashtable fee_code =new Hashtable();
            int i=0;
            fee_stat_name = this.GetInvoiceTypeName();
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeItem in al)
                {
                    i++;
                    feeItem.Invoice.Type.Name = fee_stat_name[feeItem.Item.MinFee.ID].ToString();
                    if (fee_code.ContainsValue(feeItem.Invoice.Type.Name))
                    {
                        continue; 
                    }
                    else
                    {
                        foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeItemtmp in al)
                        {
                            feeItemtmp.Invoice.Type.Name = fee_stat_name[feeItemtmp.Item.MinFee.ID].ToString();
                            if (feeItemtmp.Item.MinFee.ID !=  feeItem.Item.MinFee.ID && feeItemtmp.Invoice.Type.Name ==  feeItem.Invoice.Type.Name)
                            {
                                feeItem.FT.TotCost = feeItem.FT.TotCost + feeItemtmp.FT.TotCost;
                            }
                        }
                        feeinfoSort.Add(feeItem);
                        fee_code.Add(i, feeItem.Invoice.Type.Name); 
                    }
                   
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
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in alFeeDetail)
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
                    if (InvoiceType == (alFeeDetail[j] as FS.HISFC.Models.Fee.Inpatient.FeeItemList).Invoice.Type.Name)
                    {
                        feeDetailSort.Add(alFeeDetail[j]);
                    }
                }
            }

            return feeDetailSort;

        }
        ///
        ///
        ///
        #endregion 
    }
}
