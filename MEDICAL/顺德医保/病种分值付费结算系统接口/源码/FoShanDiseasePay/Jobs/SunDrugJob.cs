using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FoShanDiseasePay.DataBase;
using Neusoft.FrameWork.Function;

namespace FoShanDiseasePay.Jobs
{
    /// <summary>
    /// 佛山市【药品】阳光采购平台接口
    /// </summary>
    public class SunDrugJob : BaseJob
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SunDrugJob()
        {

        }

        /// <summary>
        /// 4.5	上报医疗机构药品信息(HD001)
        /// </summary>
        /// <param name="drugItem"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int UploadDrugBaseInfo(Neusoft.HISFC.Models.Pharmacy.Item drugItem, ref string error)
        {
            string data = string.Empty;

            string json = @"'ygDrugID':'{0}','hDrugID':'{1}','manuName':'{2}','salerName':'{3}','drugName':'{4}',
                            'productCode':'{5}','productName':'{6}','spellAbbr':'{7}','spec':'{8}','doseageFormName':'{9}',
                            'wrapName':'{10}','wrapDesc':'{11}','measure':'{12}','minUnit':'{13}','minUnitRate':'{14}',
                            'middleUnitRate':'{15}','middleUnit':'{16}','bigUnitRate':'{17}','bigUnit':'{18}','permitNumber':'{19}',
                            'origPermitNumber':'{20}','permitDate':'{21}','permitExpiryDate':'{22}','productSource':'{23}','importNumber':'{24}',
                            'origImportNumber':'{25}','importExpiryDate':'{26}','gmpFlag':'{27}','gmpExpiryDate':'{28}','remark':'{29}',
                            'typeFlag':'{30}','qualityType':'{31}','qualityCode':'{32}','qualityExpiryDate':'{33}','otcType':'{34}',
                            'insuranceType':'{35}','insuranceNumber':'{36}','isBaseDrug':'{37}','stockPrice':'{38}','stockUnit':'{39}'";


            data = string.Format(json,
                "",
                drugItem.ID,
                drugItem.Product.Producer.Name,
                drugItem.Product.Company.Name,
                drugItem.Name,
                drugItem.ID,
                drugItem.Name,
                drugItem.SpellCode,
                drugItem.Specs,
                drugItem.DosageForm.ID,
                drugItem.Wrapper,
                drugItem.WrapperDesc,
                drugItem.DoseUnit,
                drugItem.MinUnit,
                drugItem.BaseDose.ToString(),
                 "1",
                 "",
                 drugItem.PackQty.ToString(),
                 drugItem.PackUnit.Length > 8 ? drugItem.PackUnit.Substring(0, 8) : drugItem.PackUnit,
                 drugItem.Product.ApprovalInfo,
                "1",  /* drugItem.OldApproveInfo*/
               drugItem.ApproveDate.ToString("yyyy-MM-dd"),
               drugItem.ApproveExpDate.ToString("yyyy-MM-dd"),
               drugItem.DrugSource == "" ? "1" : drugItem.DrugSource,
               drugItem.ImportInfo,
                drugItem.OldImportInfo,
                drugItem.ImportExpDate.ToString("yyyy-MM-dd"),
                drugItem.IsGMP ? "1" : "0",
                drugItem.GmpExpdate.ToString("yyyy-MM-dd"),
                "佛山市",
                drugItem.DrugTypeFlag,
                (string.IsNullOrEmpty(drugItem.QualityType) ? "00" : drugItem.QualityType),   //质量标准类型 00：空（默认）
                drugItem.QualityCode,
                drugItem.QualityExpDate.ToString("yyyy-MM-dd"),
                string.IsNullOrEmpty(drugItem.OTCInfo) ? "1" : drugItem.OTCInfo,//35
                (string.IsNullOrEmpty(drugItem.Grade) ? "0" : drugItem.Grade),    //国家基本医疗保险药品类型 0：非国家基本医疗保险药品（默认）1：甲类 2：乙类3：民族药
                drugItem.UserCode,//37
                drugItem.SpecialFlag2,
                drugItem.PriceCollection.PurchasePrice,
                drugItem.PackUnit
                );

            return this.UploadInfo("HD001", data, ref error);
        }


        /// <summary>
        /// 作废药品基本信息
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int DeleteBaseInfo(Neusoft.HISFC.Models.Pharmacy.Item drugItem, ref string error)
        {
            string data = string.Empty;

            string json = @"'hDrugID':'{0}'";

            data = string.Format(json, drugItem.ID);


            return this.UploadInfo("HD001D", data, ref error);


        }
        /// <summary>
        /// 4.6	医疗机构药品信息查询(HD001A)
        /// </summary>
        /// <param name="drugCode"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int QueryDrugBaseInfo(string drugCode, ref string error)
        {
            string data = string.Empty;

            string json = @"hDrugID:'{0}'";

            data = string.Format(json, drugCode);

            return this.UploadInfo("HD001A", data, ref error);
        }

        /// <summary>
        /// 4.7	上报医疗机构药品库存信息(HD002)
        /// </summary>
        /// <param name="drugList"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int UploadDrugStoreInfo(ArrayList drugList, ref string error)
        {
            string data = string.Empty;
            string json = @"'ygDrugID':'{0}', 'hDrugID':'{1}','stockNum':'{2}'";

            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            foreach (Neusoft.FrameWork.Models.NeuObject drugStore in drugList)
            {
                sb.Append("{").Append(string.Format(json, "", drugStore.ID, drugStore.Memo)).Append("},");
            }

            data = sb.ToString().TrimEnd(',');
            sb = null;
            data += "]";

            return this.UploadInfo("HD002", data, ref error);
        }

        /// <summary>
        /// 4.8	医疗机构药品库存信息查询(HD002A)
        /// </summary>
        /// <param name="drugCode"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int QueryDrugStoreInfo(string drugCode, ref string error)
        {
            string data = string.Empty;

            string json = @"ygDrugID:'{0}',hDrugID:'{1}'";

            data = string.Format(json, "", drugCode);

            return this.UploadInfo("HD002A", data, ref error);
        }

        /// <summary>
        /// 4.9 上报医疗机构药品使用详情(HD003)
        /// </summary>
        /// <param name="output"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int UploadDrugUseInfo(Neusoft.HISFC.Models.Pharmacy.Output output, ref string error)
        {
            string data = string.Empty;

            string json = @"'ygDrugID':'{0}','hDrugID':'{1}','deptName':'{2}','doctorCode':'{3}','doctorName':'{4}',
                            'hisNumber':'{5}','patientInsuranceCode':'{6}','patientName':'{7}','diagnosisDate':'{8}','lotNo':'{9}',
                            'manuDate':'{10}','expiredDate':'{11}','useAmount':'{12}'";

            data = string.Format(json, output.ID, output.Item.ID, output.Operation.ApproveOper.Dept.Name, output.Operation.ApproveOper.ID, output.Operation.ApproveOper.Name,
                            output.Operation.Oper.ID, output.Operation.Oper.Memo, output.Operation.Oper.Name, output.OutDate.ToString("yyyy-MM-dd"), output.BatchNO,
                            "", output.ValidTime.ToString("yyyy-MM-dd"), output.Operation.ExamQty.ToString());

            return this.UploadInfo("HD003", data, ref error);
        }

        /// <summary>
        /// 4.10 医疗机构药品使用详情查询(HD003A)
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public int QueryDrugUseInfo(ref string error)
        {
            string data = string.Empty;

            string json = @"'ygDrugID':'{0}','hDrugID':'{1}'";

            return this.UploadInfo("HD003A", data, ref error);
        }

        /// <summary>
        /// 4.11	上报医疗机构药品采购订单(HD005)
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int UploadDrugOrderInfo(ArrayList inputList, ref string error)
        {
            string data = string.Empty;

            string json = @"'ygOrderID':'{0}','hOrderID':'{1}','buyerName':'{2}','salerName':'{3}','senderName':'{4}',
                            'storeName':'{5}','totalAccount':'{6}','overAccount':'{7}','state':'{8}','senderReadDate':'{9}',
                            'senderConfirmName':'{10}','senderConfirmDate':'{11}','closeDate':'{12}',list:[{13}]";

            string detailJoson = @"'ygOrderDetailID':'{0}','hOrderDetailID':'{1}','ygDrugID':'{2}','hDrugID':'{3}','price':'{4}',
                                    'account':'{5}','amount':'{6}','createDate':'{7}','overAccount':'{8}','overAmount':'{9}',
                                    'sendDate':'{10}','overDate':'{11}','lotNo':'{12}','manuDate':'{13}','expiredDate':'{14}',
                                    'state':'{15}'";

            Neusoft.HISFC.Models.Pharmacy.Input input0 = new Neusoft.HISFC.Models.Pharmacy.Input();
            input0.InListNO = "Last";
            inputList.Add(input0);

            StringBuilder sbInner = new StringBuilder();
            decimal retailCost = 0m;
            decimal purCost = 0m;

            input0 = inputList[0] as Neusoft.HISFC.Models.Pharmacy.Input;
            string key = input0.InListNO;
            int result = 0;

            string cc = string.Format(json, input0.DeliveryNO, input0.InListNO, Manager.setObj.HospitalName, input0.Company.Name, input0.TenderNO,
                input0.StockDept.Name, "AAAA", "BBBB", "80", "", "", "", "", "inner");

            foreach (Neusoft.HISFC.Models.Pharmacy.Input input in inputList)
            {
                if (input.InListNO != key)
                {
                    data = cc.Replace("AAAA", retailCost.ToString()).Replace("BBBB", purCost.ToString()).Replace("inner", sbInner.ToString().TrimEnd(','));

                    //这里进行上传数据
                    result = this.UploadInfo("HD005", data, ref error);

                    if (result <= 0)
                    {
                        LogManager.WriteLog("上传数据出错！" + error);
                    }

                    if (input.InListNO == "Last") //最后一条记录是增加的  不用进行处理了 
                    {
                        input0 = null;
                        sbInner = null;
                        break;
                    }

                    sbInner = new StringBuilder();
                    retailCost = 0m;
                    purCost = 0m;
                    cc = string.Format(json, input.DeliveryNO, input.InListNO, Manager.setObj.HospitalName, input.Company.Name, input.TenderNO,
                        input.StockDept.Name, "AAAA", "BBBB", "80", "", "", "", "", "inner");
                }

                #region 废弃
                //input.DeliveryNO = Reader[0].ToString(); //订单号
                //input.InListNO = Reader[1].ToString();//院内单据号
                //input.Company.Name = Reader[2].ToString();//供货公司
                //input.TenderNO = Reader[3].ToString();//送货公司
                //input.StockDept.Name = Reader[4].ToString();//库房
                //input.MedNO = Reader[5].ToString();//订单明细号
                //input.ID = Reader[6].ToString();//院内订单明细号
                //input.Item.UserCode = Reader[7].ToString();//阳光用药编码
                //input.Item.ID = Reader[8].ToString();//院内编码
                //input.PriceCollection.PurchasePrice = Convert.ToDecimal(Reader[9].ToString());//生产时间
                //input.Operation.ApproveQty = Convert.ToDecimal(Reader[10].ToString());//采购数量
                //input.User01 = Reader[11].ToString();//发货日期
                //input.User02 = Reader[12].ToString();//生产日期
                //input.User03 = Reader[13].ToString();//采购日期
                //input.Quantity = Convert.ToDecimal(Reader[14].ToString());//数量
                //input.InDate = Convert.ToDateTime(Reader[15].ToString());//数量
                //input.BatchNO = Reader[16].ToString();//采购日期
                //input.ValidTime = Convert.ToDateTime(Reader[17]);//有效期
                #endregion

                sbInner.Append("{");
                sbInner.Append(string.Format(detailJoson, input.MedNO, input.ID, input.Item.UserCode,
                                input.Item.ID, input.PriceCollection.PurchasePrice, input.RetailCost, input.Operation.ApproveQty, input.InDate.ToString(),
                                input.PurchaseCost.ToString(), input.Quantity.ToString(), input.InDate.ToString(), input.InDate.ToString("yyyy-MM-dd"), input.BatchNO,
                                input.User01, input.ValidTime, "70")).Append("}").Append(',');

                retailCost += input.RetailCost;
                purCost += input.PurchaseCost;
            }

            return 1;
        }

        /// <summary>
        /// 4.12	医疗机构药品采购订单查询(HD005A)
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public int QueryDrugOrderInfo(ref string error)
        {
            string data = string.Empty;

            string json = @"{hOrderID:'{0}'}";

            return this.UploadInfo("HD005A", data, ref error);
        }

        /// <summary>
        /// 4.13	上报医疗机构药品退货信息(HD006)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int UploadDrugReturnInfo(Neusoft.HISFC.Models.Pharmacy.Input input, ref string error)
        {
            string data = string.Empty;

            string json = @"'ygRGOrderID':'{0}','hRGOrderID':'{1}','ygOrderID':'{2}','hOrderID':'{3}','ygOrderDetailID':'{4}',
                            'hOrderDetailID':'{5}','ygDrugID':'{6}','hDrugID':'{7}','buyerName':'{8}','senderName':'{9}',
                            'totalAccount':'{10}','amount':'{11}','price':'{12}','lotNo':'{13}','manuDate':'{14}',
                            'expiredDate':'{15}','state':'{16}','returnReason':'{17}','senderRemark':'{18}','closeDate':'{19}'";

            #region 参考
            //input.DeliveryNO = Reader[0].ToString(); //订单号
            //input.InListNO = Reader[1].ToString();//院内单据号
            //input.Company.Name = Reader[2].ToString();//供货公司
            //input.TenderNO = Reader[3].ToString();//送货公司
            //input.StockDept.Name = Reader[4].ToString();//库房
            //input.MedNO = Reader[5].ToString();//订单明细号
            //input.ID = Reader[6].ToString();//院内订单明细号
            //input.Item.UserCode = Reader[7].ToString();//阳光用药编码
            //input.Item.ID = Reader[8].ToString();//院内编码
            //input.PriceCollection.PurchasePrice = Convert.ToDecimal(Reader[9].ToString());//生产时间
            //input.Operation.ApproveQty = Convert.ToDecimal(Reader[10].ToString());//采购数量
            //input.User01 = Reader[11].ToString();//发货日期
            //input.User02 = Reader[12].ToString();//生产日期
            //input.User03 = Reader[13].ToString();//采购日期
            //input.Quantity = Convert.ToDecimal(Reader[14].ToString());//数量
            //input.InDate = Convert.ToDateTime(Reader[15].ToString());//数量
            //input.BatchNO = Reader[16].ToString();//采购日期
            //input.ValidTime = Convert.ToDateTime(Reader[17]);//有效期
            #endregion


            //订单明细号
            if (string.IsNullOrEmpty(input.MedNO))
            {
                input.MedNO = input.DeliveryNO;
            }
            //院内订单明细号
            if (string.IsNullOrEmpty(input.ID))
            {
                input.ID = input.InListNO;
            }


            data = string.Format(json, input.DeliveryNO, input.InListNO, "", input.Operation.User01, "", input.Operation.User02, input.Item.UserCode, input.Item.ID,
                 Manager.setObj.HospitalName, input.TenderNO, input.PurchaseCost.ToString(), input.Quantity.ToString(), input.PriceCollection.PurchasePrice.ToString(),
                input.BatchNO, "", input.ValidTime.ToString("yyyy-MM-dd"), "40", "过期", "", "");

            return this.UploadInfo("HD006", data, ref error);
        }

        /// <summary>
        /// 4.14	医疗机构药品退货信息查询(HD006A)
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public int QueryDrugReturnInfo(ref string error)
        {
            string data = string.Empty;

            string json = @"{hRGOrderID:'{0}'}";

            return this.UploadInfo("HD006A", data, ref error);
        }

        /// <summary>
        /// 4.15	上报医疗机构药品采购订单发票(HD007)
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int UploadDrugInvoiceInfo(ArrayList inputList, ref string error)
        {
            string data = string.Empty;

            string json = @"'ygInvoiceID':'{0}','hInvoiceID':'{1}','buyerName':'{2}','senderName':'{3}','totalAccount':'{4}',
                            'invoiceDate':'{5}','invoiceType':'{6}','invoiceName':'{7}','invoiceNo':'{8}','state':'{9}',
                            'returnReason':'{10}', list: [{11}]";

            string detailJson = @"ygOrderID:'{0}',hOrderID:'{1}',ygOrderDetailID:'{2}',hOrderDetailID:'{3}'";

            Neusoft.HISFC.Models.Pharmacy.Input input0 = new Neusoft.HISFC.Models.Pharmacy.Input();
            input0.InListNO = "Last";
            inputList.Add(input0);

            StringBuilder sbInner = new StringBuilder();
            decimal retailCost = 0m;
            decimal purCost = 0m;

            input0 = inputList[0] as Neusoft.HISFC.Models.Pharmacy.Input;
            string key = input0.InListNO;
            int result = 0;

            string cc = string.Format(json, input0.Operation.ID, input0.InvoiceNO, Manager.setObj.HospitalName, input0.Company.Name, "AAAA",
               input0.InvoiceDate.ToString("yyyy-MM-dd"), input0.InvoiceType, input0.Name, input0.InvoiceNO, "20", "", "inner");

            foreach (Neusoft.HISFC.Models.Pharmacy.Input input in inputList)
            {
                if (input.InListNO != key)
                {
                    data = cc.Replace("AAAA", retailCost.ToString()).Replace("inner", sbInner.ToString().TrimEnd(','));

                    //这里进行上传数据
                    result = this.UploadInfo("HD007", data, ref error);

                    if (result <= 0)
                    {
                        LogManager.WriteLog("上传数据出错！" + error);
                    }

                    if (input.InListNO == "Last") //最后一条记录是增加的  不用进行处理了 
                    {
                        input0 = null;
                        sbInner = null;
                        break;
                    }

                    sbInner = new StringBuilder();
                    retailCost = 0m;
                    purCost = 0m;
                    cc = string.Format(json, input.Operation.ID, input.InvoiceNO, Manager.setObj.HospitalName, input.Company.Name, "AAAA",
                        input.InvoiceDate.ToString("yyyy-MM-dd"), input.InvoiceType, input.Name, input.InvoiceNO, "20", "", "inner");
                }

                //input.Operation.ID = Reader[0].ToString(); //采购平台发票
                //input.InvoiceNO = Reader[1].ToString(); //院内发票
                //input.Company.Name = Reader[2].ToString(); //配送企业
                //input.PurchaseCost = Convert.ToDecimal(Reader[3].ToString());//明细金额
                //input.InvoiceDate = Convert.ToDateTime(Reader[4].ToString()); //发票时间
                //input.InvoiceType = Reader[5].ToString();//发票类型
                //input.Name = Reader[6].ToString();//发票名称
                //input.Memo = Reader[7].ToString();//
                //input.Operation.Memo = Reader[8].ToString();
                //input.DeliveryNO = Reader[9].ToString();//平台订单号
                //input.InListNO = Reader[10].ToString();//院内订单编号
                //input.MedNO = Reader[11].ToString();//阳光采购平台明细编号
                //input.ID = Reader[12].ToString();//院内订单明细好

                sbInner.Append("{");
                sbInner.Append(string.Format(detailJson, input.DeliveryNO, input.InListNO, input.MedNO, input.ID)).Append("}").Append(',');

                retailCost += input.PurchaseCost;
            }


            return this.UploadInfo("HD007", data, ref error);
        }

        /// <summary>
        /// 4.16	医疗机构药品采购订单发票查询(HD007A)
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public int QueryDrugInvoiceInfo(ref string error)
        {
            string data = string.Empty;

            string json = @"hInvoiceID:'{0}'";

            return this.UploadInfo("HD007A", data, ref error);
        }

        /// <summary>
        /// 4.32	上报医疗机材采购订单发票附件(HV001)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int UploadDrugInvoiceFileInfo(Neusoft.HISFC.Models.Pharmacy.Input input, ref string error)
        {
            string data = string.Empty;

            string json = @"'ygInvoiceID':'{0}','hInvoiceID':'{1}','fileName':'{2}','uploadFile':'{3}'";

            data = string.Format(json, "", input.InvoiceNO, input.InvoiceType, input.Memo);

            return this.UploadInfo("HV001", data, ref error);
        }

        /// <summary>
        /// 4.34	上报医疗机构采购结算信息(HC001)
        /// </summary>
        /// <param name="alBalanceInfo"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int UploadBalanceInfo(ArrayList alBalanceInfo, ref string error)
        {
            string data = string.Empty;

            string json = @"'ygBalanceId':'{0}','hBalanceId':'{1}','buyerName':'{2}','salerName':'{3}','senderName':'{4}',
                            'total':'{5}','beginDate':'{6}','endDate':'{7}','remark':'{8}','state':'{9}','list':[{10}]";

            string detailJson = @"'ygBalanceDetailId':'{0}','hBalanceDetailId':'{1}','ygOrderType':'{2}','ygInvoiceID':'{3}','hInvoiceID':'{4}'";

            Hashtable ht = new Hashtable();
            ArrayList al = new ArrayList();

            string primarykey = string.Empty;
            foreach (DiseasePay.Models.BalanceHead balance in alBalanceInfo)
            {
                primarykey = balance.Company.Name + balance.OperDate.ToString("yyyyMM");
                if (ht.ContainsKey(primarykey))
                {
                    al = ht[primarykey] as ArrayList;
                    al.Add(balance);
                }
                else
                {
                    al = new ArrayList();
                    al.Add(balance);
                    ht.Add(primarykey, al);
                }
            }

            StringBuilder sb = null;
            DiseasePay.Models.BalanceHead pay0 = null;
            decimal totCost = 0m;
            string invoiceType = string.Empty;

            DateTime dtBegin = DateTime.Now;
            DateTime dtEnd = DateTime.Now;
            int result = -1;
            foreach (string key in ht.Keys)
            {
                al = ht[key] as ArrayList;

                sb = new StringBuilder();
                pay0 = al[0] as DiseasePay.Models.BalanceHead;
                totCost = 0m;
                string type = string.Empty;

                dtBegin = new DateTime(pay0.OperDate.Year, pay0.OperDate.Month, 1);
                dtEnd = dtBegin.AddMonths(1).AddSeconds(-1);

                foreach (DiseasePay.Models.BalanceHead pay in alBalanceInfo)
                {
                    totCost += pay.PayCost;
                    type = pay.PayState == "1" ? "10" : "20";
                    sb.Append("{").Append(string.Format(detailJson, "", pay.PayHeadNo, type, "", pay.InvoiceNo)).Append("},");
                }

                data = string.Format(json, "", pay0.PayDetailNo, Manager.setObj.HospitalName, pay0.Company.Name, pay0.Company.Name, totCost,
                    dtBegin.Date.ToString("yyyy-MM-dd"), dtEnd.Date.ToString("yyyy-MM-dd"), "", "30", sb.ToString().TrimEnd(','));

                result = this.UploadInfo("HC001", data, ref error);

                if (result < 0)
                {
                    return -1;
                }
            }

            return result;
        }

        /// <summary>
        /// 4.36	医疗机构采购结算信息查询(HC001A)
        /// </summary>
        /// <returns></returns>
        public int QueryMatBalanceInfo()
        {
            string data = string.Empty;
            string error = string.Empty;

            string json = @"'hBalanceId':'{0}'";

            return this.UploadInfo("HC001A", data, ref error);
        }
    }
}
