using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using FS.FrameWork.Function;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry
{
    class OutPatientBalanceFee
    {
        FS.HISFC.BizLogic.Fee.Outpatient outPatientMgr = new FS.HISFC.BizLogic.Fee.Outpatient();
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
        FS.HISFC.BizProcess.Integrate.Fee feeManage = new FS.HISFC.BizProcess.Integrate.Fee();
        FS.HISFC.BizProcess.Integrate.Manager myZt = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 门诊打包结算患者
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int Balance(FS.HISFC.Models.Registration.Register register, ArrayList alFeeDetail ,ref string errInfo)
        {
            if (register == null)
            {
                errInfo = "患者信息为空！";
                return -1;
            }

            FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            outPatientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            medcareProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            feeManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //先查找所有未收费的数据
            //if (alFeeDetail == null)
            //{
            //     alFeeDetail = outPatientMgr.QueryChargedFeeItemListsByClinicNO(register.ID);
            //}
            if (alFeeDetail == null)
            {
                return 1;
            }

            if (alFeeDetail.Count == 0)
            {
                return 1;
            }

            medcareProxy.BeginTranscation();
            medcareProxy.SetPactCode(register.Pact.ID);
            medcareProxy.IsLocalProcess = true;
            ArrayList noChargeFeeDetail = new ArrayList();

            int lngRes = medcareProxy.LocalBalanceOutpatient(register, ref alFeeDetail, noChargeFeeDetail);
            if (lngRes <= 0)
            {
                errInfo = "打包收费患者本地结算失败，原因：" + medcareProxy.ErrMsg;
                return -1;
            }

            if (alFeeDetail.Count > 0)
            {
                //拆分明细
                ArrayList arlFeeList = new ArrayList();
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in alFeeDetail)
                {
                    if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                    {
                        FS.HISFC.Models.Fee.Item.Undrug itemTemp = this.feeManage.GetUndrugByCode(f.Item.ID);

                        if (itemTemp != null && itemTemp.UnitFlag == "1")
                        {
                            ArrayList al = this.ChangeZtToSingle(f, register, register.Pact, ref errInfo);
                            if (al == null)
                            {
                                errInfo = "打包收费患者拆分组套时失败，原因：" + errInfo;
                                return -1;
                            }
                            if (al != null && al.Count > 0)
                            {
                                arlFeeList.AddRange(al);
                            }
                        }
                        else
                        {
                            arlFeeList.Add(f);
                        }
                    }
                    else
                    {
                        arlFeeList.Add(f);
                    }
                }

                if (arlFeeList.Count > 0)
                {
                    // 生成发票信息
                    FS.HISFC.Models.Fee.Outpatient.Balance invoiceInfo = null;
                    ArrayList alInvoiceDetial = null;

                    lngRes = this.BuildInvoiceInfo(employee, register, arlFeeList, out invoiceInfo, out alInvoiceDetial, out errInfo);
                    if (lngRes <= 0 || invoiceInfo == null || alInvoiceDetial == null || alInvoiceDetial.Count <= 0)
                    {
                        errInfo = "打包收费患者生成发票信息失败，原因：" + errInfo;
                        return -1;
                    }
                    ArrayList arlInvoices = new ArrayList();
                    ArrayList arlInvoiceDetial = new ArrayList();

                    ArrayList arlTemp2 = new ArrayList();
                    arlTemp2.Add(alInvoiceDetial);

                    arlInvoices.Add(invoiceInfo);
                    arlInvoiceDetial.Add(arlTemp2);

                    //支付方式
                    ArrayList lstPayModes = this.MakeInvoicePayModes(invoiceInfo, ref errInfo);
                    if (lstPayModes == null || lstPayModes.Count <= 0)
                    {
                        errInfo = "打包收费患者生成支付方式失败，原因：" + errInfo;
                        return -1;
                    }

                    //收费
                    if (feeManage.ClinicFee(FS.HISFC.Models.Base.ChargeTypes.Fee, "C", true, register, arlInvoices, arlInvoiceDetial, arlFeeList, new ArrayList(), lstPayModes, ref errInfo, employee) == false)
                    {
                        errInfo = "打包收费患者确认收费失败，原因：" + errInfo;
                        return -1;
                    }
                    //发送消息
                    new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.OrderControl().SendFeeInfo(register, arlFeeList, true);
                }
            }

            if (noChargeFeeDetail.Count > 0)
            {
                //划价
                if (feeManage.ClinicFee(FS.HISFC.Models.Base.ChargeTypes.Save, register, null, null, noChargeFeeDetail, null, null, ref errInfo) == false)
                {
                    errInfo = "打包收费患者划价收费失败，原因：" + errInfo;
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 生成发票信息。
        /// 发票信息、发票明细信息
        /// </summary>
        /// <param name="register">挂号信息</param>
        /// <param name="lstFeeItems">费用明细信息</param>
        /// <param name="invoiceInfo">发票信息</param>
        /// <param name="lstInvoiceDetial">发票明细信息</param>
        /// <param name="errText">错误信息</param>
        /// <returns>1 成功，-1 失败</returns>
        public int BuildInvoiceInfo(FS.HISFC.Models.Base.Employee employee, FS.HISFC.Models.Registration.Register register, ArrayList lstFeeItems, out FS.HISFC.Models.Fee.Outpatient.Balance invoiceInfo, out ArrayList lstInvoiceDetial, out string errText)
        {
            invoiceInfo = null;
            lstInvoiceDetial = null;
            errText = "";

            #region 获取发票号与打印发票号

            string invoiceNO = "";
            string realInvoiceNO = "";

            int iRes = this.feeManage.GetInvoiceNO(employee, "C", true, ref invoiceNO, ref realInvoiceNO, ref errText);
            if (iRes <= 0)
            {
                return iRes;
            }
            #endregion

            #region 获取发票大类
            DataSet dsInvoice = new DataSet();

            this.feeManage.GetInvoiceClass("MZ01", ref dsInvoice);
            if (dsInvoice == null || dsInvoice.Tables.Count <= 0)
            {
                errText = "没有维护发票大类!请维护";
                return -1;
            }

            if (dsInvoice.Tables[0].PrimaryKey.Length == 0)
            {
                dsInvoice.Tables[0].PrimaryKey = new DataColumn[] { dsInvoice.Tables[0].Columns["FEE_CODE"] };
            }
            #endregion

            invoiceInfo = MakeInvoiceInfo(lstFeeItems, register, invoiceNO, realInvoiceNO);
            if (invoiceInfo == null)
            {
                errText = "生成发票信息失败！";
                return -1;
            }

            lstInvoiceDetial = MakeInvoiceDetail(lstFeeItems, register, invoiceNO, dsInvoice.Tables[0], ref errText);
            if (lstInvoiceDetial == null)
            {
                return -1;
            }

            return 1;
        }

        #region 生成主发票信息
        /// <summary>
        /// 生成主发票信息
        /// </summary>
        /// <param name="feeItemLists">费用明细集合</param>
        /// <param name="register">挂号信息</param>
        /// <param name="invoiceNO">发票号</param>
        /// <param name="realInvoiceNO">实际发票号</param>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.Outpatient.Balance MakeInvoiceInfo(ArrayList lstFeeItems, FS.HISFC.Models.Registration.Register register, string invoiceNO, string realInvoiceNO)
        {
            FS.HISFC.Models.Fee.Outpatient.Balance invoice = new FS.HISFC.Models.Fee.Outpatient.Balance();
            decimal totCost = 0;//总金额
            decimal ownCost = 0;//自费金额
            decimal pubCost = 0;//记帐金额
            decimal payCost = 0;//自付金额
            decimal rebateCost = 0;//优惠价格

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in lstFeeItems)
            {
                ownCost += feeItem.FT.OwnCost;
                pubCost += feeItem.FT.PubCost;
                payCost += feeItem.FT.PayCost;
                rebateCost += feeItem.FT.RebateCost;
            }
            totCost = ownCost + pubCost + payCost;
            invoice.Invoice.ID = invoiceNO;
            invoice.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
            invoice.Patient = register.Clone();
            invoice.FT.OwnCost = ownCost;
            invoice.FT.PayCost = payCost;
            invoice.FT.PubCost = pubCost;
            invoice.FT.TotCost = totCost;
            invoice.FT.RebateCost = rebateCost;
            invoice.User01 = rebateCost.ToString();

            invoice.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
            invoice.CanceledInvoiceNO = "";
            invoice.IsDayBalanced = false;
            invoice.PrintTime = DateTime.Now;
            invoice.PrintedInvoiceNO = realInvoiceNO;
            invoice.IsAccount = true;

            return invoice;
        }
        #endregion

        #region 生成发票明细
        /// <summary>
        /// 生成发票明细
        /// </summary>
        /// <param name="feeItemLists">费用明细集合</param>
        /// <param name="register">挂号信息</param>
        /// <param name="invoiceNO">发票号</param>
        /// <param name="dtInvoice">发票大类</param>
        /// <param name="errText">错误信息</param>
        /// <returns>成功: 发票明细集合 失败 null</returns>
        private ArrayList MakeInvoiceDetail(ArrayList lstFeeItems, FS.HISFC.Models.Registration.Register register, string invoiceNO, DataTable dtInvoice, ref string errText)
        {
            ArrayList balanceLists = new ArrayList();

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in lstFeeItems)
            {
                DataRow rowFind = dtInvoice.Rows.Find(new object[] { f.Item.MinFee.ID });
                if (rowFind == null)
                {
                    errText = "最小费用为" + f.Item.MinFee.ID + "的最小费用没有在MZ01的发票大类中维护";
                    return null;
                }

                rowFind["TOT_COST"] = NConvert.ToDecimal(rowFind["TOT_COST"].ToString()) + f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                rowFind["OWN_COST"] = NConvert.ToDecimal(rowFind["OWN_COST"].ToString()) + f.FT.OwnCost;
                rowFind["PUB_COST"] = NConvert.ToDecimal(rowFind["PUB_COST"].ToString()) + f.FT.PubCost;
                rowFind["PAY_COST"] = NConvert.ToDecimal(rowFind["PAY_COST"].ToString()) + f.FT.PayCost;

                //费用表记录
                f.FeeCodeStat.ID = rowFind["FEE_STAT_CATE"].ToString();
                f.FeeCodeStat.Name = rowFind["FEE_STAT_NAME"].ToString();
                f.FeeCodeStat.SortID = NConvert.ToInt32(rowFind["SEQ"].ToString());
            }

            FS.HISFC.Models.Fee.Outpatient.BalanceList detail = null;//发票明细实体

            for (int i = 1; i < 100; i++)
            {
                //找到相同打印序号,即同一统计类别的费用集合
                DataRow[] rowFind = dtInvoice.Select("SEQ = " + i.ToString(), "SEQ ASC");
                //如果没有找到说明已经找过了最大的打印序号,所有费用已经累加完毕.
                if (rowFind.Length == 0)
                {

                }
                else
                {
                    detail = new FS.HISFC.Models.Fee.Outpatient.BalanceList();
                    detail.BalanceBase.Invoice.ID = invoiceNO;
                    detail.BalanceBase.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    detail.InvoiceSquence = i;
                    detail.FeeCodeStat.ID = rowFind[0]["FEE_STAT_CATE"].ToString();
                    detail.FeeCodeStat.Name = rowFind[0]["FEE_STAT_NAME"].ToString();

                    /// 保存打印序号到实体。
                    ///----------------------------------------------------
                    detail.FeeCodeStat.SortID = NConvert.ToInt32(rowFind[0]["SEQ"].ToString());
                    ///---------------------------------------------------
                    detail.BalanceBase.IsDayBalanced = false;
                    detail.BalanceBase.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                    foreach (DataRow row in rowFind)
                    {
                        detail.BalanceBase.FT.PubCost += NConvert.ToDecimal(row["PUB_COST"].ToString());
                        detail.BalanceBase.FT.OwnCost += NConvert.ToDecimal(row["OWN_COST"].ToString());
                        detail.BalanceBase.FT.PayCost += NConvert.ToDecimal(row["PAY_COST"].ToString());
                    }
                    detail.BalanceBase.FT.TotCost = detail.BalanceBase.FT.PubCost + detail.BalanceBase.FT.OwnCost + detail.BalanceBase.FT.PayCost;
                    //如果费用为0 说明次统计类别(打印序号)下没有费用
                    if (detail.BalanceBase.FT.TotCost <= 0)
                    {
                        continue;
                    }

                    balanceLists.Add(detail);
                }
            }

            return balanceLists;
        }
        #endregion

        #region 生成发票支付方式
        /// <summary>
        /// 生成发票支付方式
        /// 门诊账户支付
        /// </summary>
        /// <param name="invoiceInfo">发票信息</param>
        /// <param name="errText">错误信息</param>
        /// <returns></returns>
        public ArrayList MakeInvoicePayModes(FS.HISFC.Models.Fee.Outpatient.Balance invoiceInfo, ref string errText)
        {
            ArrayList lstBalancePay = new ArrayList();
            FS.HISFC.Models.Fee.Outpatient.BalancePay payMode = null;
            // 减免支付（优惠金额、二次减免、老年减免）
            payMode = new FS.HISFC.Models.Fee.Outpatient.BalancePay();
            payMode.PayType.ID = "RC";
            payMode.PayType.Name = "减免";
            payMode.FT.TotCost = invoiceInfo.FT.RebateCost;
            lstBalancePay.Add(payMode);

            return lstBalancePay;
        }

        #endregion

        #region 组套拆分成明细

        public ArrayList ChangeZtToSingle(FS.HISFC.Models.Fee.Outpatient.FeeItemList f, FS.HISFC.Models.Registration.Register reg, FS.HISFC.Models.Base.PactInfo pactInfo, ref string errInfo)
        {
            ArrayList alFee = new ArrayList();
            ArrayList alFeeItemList = new ArrayList();
            DateTime nowDate = this.outPatientMgr.GetDateTimeFromSysDateTime();
            int age = (int)((new TimeSpan(nowDate.Ticks - reg.Birthday.Ticks)).TotalDays / 365);

            ArrayList alZt = myZt.QueryUndrugPackageDetailByCode(f.Item.ID);

            if (alZt == null)
            {
                errInfo = myZt.Err;
                return null;
            }

            FS.HISFC.BizProcess.Integrate.Fee myItem = new FS.HISFC.BizProcess.Integrate.Fee();
            FS.HISFC.Models.Fee.Outpatient.FeeItemList feeitemlist;

            foreach (FS.HISFC.Models.Fee.Item.Undrug info in alZt)
            {
                FS.HISFC.Models.Fee.Item.Undrug item = feeManage.GetItem(info.ID);
                if (item == null)
                {
                    errInfo = feeManage.Err;
                    return null;
                }
                feeitemlist = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
                #region  转化为实体
                if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    feeitemlist.Item = new FS.HISFC.Models.Pharmacy.Item();
                }
                else
                {
                    feeitemlist.Item = new FS.HISFC.Models.Fee.Item.Undrug();
                }
                feeitemlist.User01 = f.RecipeNO;
                feeitemlist.User02 = f.SequenceNO.ToString();
                feeitemlist.User03 = f.Order.ID;
                feeitemlist.Order.ID = f.Order.ID;
                feeitemlist.Patient.PID.ID = f.Patient.ID;
                feeitemlist.Item.Price = myItem.GetPrice(pactInfo.PriceForm, age, item.Price, item.ChildPrice, item.SpecialPrice, item.Price);
                feeitemlist.Item.Qty = f.Item.Qty * info.Qty;
                feeitemlist.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                feeitemlist.Patient.ID = f.Patient.ID;//门诊流水号
                feeitemlist.Patient.PID.CardNO = reg.PID.CardNO;//门诊卡号 
                //feeitemlist.Order.ID = "";//医嘱流水号

                feeitemlist.ChargeOper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                feeitemlist.Order.CheckPartRecord = "";//order.CheckPartRecord;//检体 
                feeitemlist.Order.Combo.ID = f.Order.Combo.ID;//组合号
                feeitemlist.UndrugComb.ID = f.Item.ID;
                feeitemlist.UndrugComb.Name = f.Item.Name;
                //}

                if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug && !((FS.HISFC.Models.Pharmacy.Item)f.Item).IsSubtbl)
                {
                    feeitemlist.ExecOper.Dept.ID = f.Order.StockDept.Clone().ID;//传扣库科室
                    feeitemlist.ExecOper.Dept.Name = f.Order.StockDept.Clone().Name;
                }
                else
                {
                    feeitemlist.ExecOper.Dept.ID = f.ExecOper.Dept.ID;
                    feeitemlist.ExecOper.Dept.Name = f.ExecOper.Dept.Name;

                }
                feeitemlist.InjectCount = f.Order.InjectCount;//院内次数

                if (f.Order.Item.PackQty <= 0)
                {
                    feeitemlist.Item.PackQty = 1;
                }

                if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    feeitemlist.Item.SpecialFlag = f.Order.Item.SpecialFlag;
                    if (f.Item.PriceUnit == ((FS.HISFC.Models.Pharmacy.Item)f.Item).MinUnit || f.Item.PriceUnit == "")
                    {
                        //xingz
                        feeitemlist.Item.Qty = f.Item.PackQty * f.Item.Qty;//f.Order.Item.PackQty * order.Qty;
                        feeitemlist.FT.TotCost = f.Item.Qty * feeitemlist.Item.Price;// order.Qty * order.Item.Price;

                        feeitemlist.Order.Item.PriceUnit = item.PriceUnit;
                        feeitemlist.FeePack = "1";//开立单位 1:包装单位 0:最小单位
                    }
                    else
                    {
                        if (feeitemlist.Item.PackQty == 0)
                        {
                            feeitemlist.Item.PackQty = 1;
                        }
                        feeitemlist.FT.OwnCost = feeitemlist.Item.Qty * feeitemlist.Item.Price / feeitemlist.Item.PackQty; //order.Qty * order.Item.Price / order.Item.PackQty;

                        feeitemlist.FeePack = "0";//开立单位 1:包装单位 0:最小单位
                    }
                }
                else
                {
                    feeitemlist.FT.OwnCost = feeitemlist.Item.Qty * feeitemlist.Item.Price;
                    feeitemlist.FeePack = "1";
                }

                if (f.Order.HerbalQty == 0)
                {
                    feeitemlist.Order.HerbalQty = 1;
                }

                feeitemlist.Days = f.Days;//草药付数
                feeitemlist.RecipeOper.Dept = f.RecipeOper.Dept.Clone();//开方科室信息
                feeitemlist.RecipeOper.Name = f.RecipeOper.Name;//开方医生信息
                feeitemlist.RecipeOper.ID = f.RecipeOper.ID;
                feeitemlist.Order.DoseUnit = f.Order.DoseUnit;//用量单位
                feeitemlist.Order.Frequency = f.Order.Frequency;//频次信息
                feeitemlist.Order.Combo.IsMainDrug = f.Order.Combo.IsMainDrug;//是否主药
                feeitemlist.Item.ID = item.ID;
                feeitemlist.Item.Name = item.Name;

                if (f.Order.ReTidyInfo != "SUBTBL")
                {
                    feeitemlist.IsConfirmed = false;
                    feeitemlist.Item.PackQty = 1;//包装数量
                }
                else//附材中的复合项目
                {
                    feeitemlist.IsConfirmed = false;
                    feeitemlist.Item.PackQty = 1;
                }

                feeitemlist.Order.Item.ItemType = feeitemlist.Item.ItemType;//是否药品
                if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)//药品
                {
                    feeitemlist.Item.Specs = item.Specs;
                }
                feeitemlist.IsUrgent = f.Order.IsEmergency;//是否加急
                feeitemlist.Order.Sample = f.Order.Sample;//样本信息
                feeitemlist.Memo = f.Order.Memo;//备注
                feeitemlist.Item.MinFee = item.MinFee;//最小费用
                feeitemlist.PayType = FS.HISFC.Models.Base.PayTypes.Charged;//划价状态
                feeitemlist.Item.PriceUnit = f.Item.PriceUnit;//价格单位
                feeitemlist.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeitemlist.FT.TotCost, 2);
                feeitemlist.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(feeitemlist.FT.OwnCost, 2);

                feeitemlist.Item.SysClass = item.SysClass;//系统类别
                feeitemlist.TransType = FS.HISFC.Models.Base.TransTypes.Positive;//交易类型
                feeitemlist.Order.Usage = f.Order.Usage;//用法
                feeitemlist.RecipeSequence = f.RecipeSequence;//收费序列
                feeitemlist.SequenceNO = f.SequenceNO;//处方流水号
                feeitemlist.FTSource = "1";//来自医嘱
                if (f.Order.IsSubtbl)
                {
                    feeitemlist.Item.IsMaterial = true;//是附材
                }
                feeitemlist.Item.IsNeedConfirm = item.IsNeedConfirm;
                feeitemlist.NoBackQty = f.Item.Qty * feeitemlist.Item.Qty;
                #endregion

                alFeeItemList.Add(feeitemlist);
            }

            return alFeeItemList;
        }
        #endregion
    }
}
