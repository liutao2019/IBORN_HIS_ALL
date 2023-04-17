using System;
using System.Data;
using System.Collections;
using System.Xml;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using FoShanDiseasePay.DataBase;

namespace FoShanDiseasePay.Jobs
{
    /// <summary>
    /// 佛山市【医用耗材】阳光采购平台接口
    /// </summary>
    public class MaterialJob : BaseJob
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MaterialJob()
        {

        }

        /// <summary>
        /// 4.17 上报医疗机构耗材信息(HH001)
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int UploadMatBaseInfo(Neusoft.HISFC.BizLogic.Material.Object.MatBase Item, ref string error)
        {
            string data = string.Empty;

            string json = @"'fsCmmdID':'{0}','ygCmmdID':'{1}','hCmmdID':'{2}','dcProductID':'{3}','productName':'{4}','regProductName':'{5}',
                            'manuName':'{6}','salerName':'{7}','senderName':'{8}','buyerName':'{9}','brand':'{10}','measure':'{11}',
                            'minSendMeasure':'{12}','minExchange':'{13}','midSendMeasure':'{14}','midExchange':'{15}','maxSendMeasure':'{16}',
                            'maxExchange':'{17}','packSpec':'{18}','packMater':'{19}','performance':'{20}','regID':'{21}','regNo':'{22}',
                            'regSpec':'{23}','regModel':'{24}','regValidDate':'{25}','productSpec':'{26}','productModel':'{27}','applyRange':'{28}',
                            'remark':'{29}','price':'{30}'";

            //商标
            if (string.IsNullOrEmpty(Item.Brand))
            {
                Item.Brand = Item.Factory.Name;
            }
            if (string.IsNullOrEmpty(Item.Brand))
            {
                Item.Brand = Item.Company.Name;
            }
            //生产厂家
            if (string.IsNullOrEmpty(Item.Factory.Name))
            {
                Item.Factory.Name = Item.Company.Name;
            }

            if (string.IsNullOrEmpty(Item.UserCode))
            {
                Item.UserCode = Item.ID;
            }
            if (string.IsNullOrEmpty(Item.SpellCode))
            {
                Item.SpellCode = Item.ID;
            }
            if (string.IsNullOrEmpty(Item.User03))
            {
                Item.User03 = Item.Name;
            }

            data = string.Format(json, 
                    Item.PlatMatCode,//平台耗材编码
                    Item.SunMatCode, //平台药品编码
                    Item.ID,
                    Item.CenterCode, 
                    Item.Name,
                    Item.User03,
                    Item.Factory.Name,
                    Item.Company.Name,
                    Item.Company.Name,
                    Manager.setObj.HospitalName,
                    Item.Brand, 
                    Item.MinUnit, 
                    Item.User01, 
                    Item.User02, 
                    "", 
                    "", 
                    Item.PackUnit,
                    Item.PackQty.ToString(),
                    Item.Specs, //包装规格 18
                    Item.Wrapper, //包装材质 19
                    Item.Perform, 
                    Item.RegisterCode, //21
                    Item.UserCode, //22
                    Item.SpellCode,//注册证规格23
                    Item.Storage.Memo,//注册证型号24 
                    Item.RegisterDate, 
                    Item.Specs, //26
                    Item.Specs, //27 产品型号
                    Item.Usage, 
                    Item.Memo, 
                    Item.InPrice.ToString());

            return this.UploadInfo("HH001", data, ref error);
        }

        /// <summary>
        /// 作废耗材基本信息
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int DeleteBaseInfo(Neusoft.HISFC.BizLogic.Material.Object.MatBase Item, ref string error)
        {
            string data = string.Empty;

            string json = @"'hCmmdID':'{0}'";

            data = string.Format(json, Item.ID);


            return this.UploadInfo("HH001D", data, ref error);


        }

        /// <summary>
        /// 4.18 医疗机构耗材信息查询(HH001A)
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public int QueryMatBaseInfo(ref string error)
        {
            string data = string.Empty;

            string json = @"'hCmmdID':'{0}'";

            return this.UploadInfo("HH001A", data, ref error);
        }

        /// <summary>
        /// 4.19 上报医疗机构耗材库存信息(HH002)
        /// </summary>
        /// <param name="list"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int UploadMatStoreInfo(ArrayList list, ref string error)
        {
            string data = string.Empty;
            string json = @"'fsCmmdID':'{0}','ygCmmdID':'{1}','hCmmdID':'{2}','stockNum':'{3}'";

            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            foreach (Neusoft.FrameWork.Models.NeuObject store in list)
            {
                sb.Append("{").Append(string.Format(json, "", "", store.ID, store.Memo)).Append("},");
            }

            data = sb.ToString().TrimEnd(',');
            sb = null;
            data += "]";
            return this.UploadInfo("HH002", data, ref error);
        }

        /// <summary>
        /// 4.20	医疗机构耗材库存信息查询(HH002A)
        /// </summary>
        /// <returns></returns>
        public int QueryMatStoreInfo()
        {
            string data = string.Empty;
            string error = string.Empty;

            string json = @"'fsCmmdID':'{0}','ygCmmdID':'{1}','hCmmdID':'{2}'";

            return this.UploadInfo("HH002A", data, ref error);
        }

        /// <summary>
        /// 4.21	上报医疗机构耗材使用详情(HH003)
        /// </summary>
        /// <param name="output"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int UploadMatUseInfo(Neusoft.HISFC.BizLogic.Material.Object.Output output, ref string error)
        {
            string data = string.Empty;

            string json = @"'fsCmmdID':'{0}','ygCmmdID':'{1}','hCmmdID':'{2}','deptName':'{3}','doctorCode':'{4}',
                            'doctorName':'{5}','hisNumber':'{6}','patientInsuranceCode':'{7}','patientName':'{8}','diagnosisDate':'{9}',
                            'lotNo':'{10}','manuDate':'{11}','expiredDate':'{12}','useAmount':'{13}'";

            data = string.Format(json, output.BaseInfo.UserCode, output.BaseInfo.Memo, output.BaseInfo.ID, output.ApproveOper.Dept.Name, output.ApproveOper.Oper.ID,
                output.ApproveOper.Oper.Name, output.Oper.ID, output.Oper.Memo, output.Oper.Name, output.OutDate.ToString("yyyy-MM-dd"), output.StockBatchNo,
                output.ApplyOper.OperTime.ToString("yyyy-MM-dd"), output.StockValidDate.ToString("yyyy-MM-dd"), output.OutNum.ToString());

            return this.UploadInfo("HH003", data, ref error);
        }

        /// <summary>
        /// 4.22	医疗机构耗材使用详情查询(HH003A)
        /// </summary>
        /// <returns></returns>
        public int QueryMatUseInfo()
        {
            string data = string.Empty;
            string error = string.Empty;

            string json = @"'fsCmmdID':'{0}','ygCmmdID':'{1}','hCmmdID':'{2}'";

            return this.UploadInfo("HH003", data, ref error);
        }

        /// <summary>
        /// 4.23 上报医疗机构耗材采购订单(HH004)
        /// </summary>
        /// <param name="list"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int UploadMatOrderInfo(ArrayList list, ref string error)
        {
            string data = string.Empty;

            string json = @"'fsOrderID':'{0}','ygOrderID':'{1}','hOrderID':'{2}','fsOrderCode':'{3}','buyerName':'{4}','stockName':'{5}',
                            'senderName':'{6}','totalAccount':'{7}','overAccount':'{8}','state':'{9}','senderReadDate':'{10}',
                            'senderConfirmName':'{11}','senderConfirmDate':'{12}','closeDate':'{13}','list': [{14}]";

            string detailJson = @"'fsOrderDetailId':'{0}','ygOrderDetailID':'{1}','hOrderDetailID':'{2}','fsCmmdID':'{3}','ygCmmdID':'{4}',
                                'hCmmdID':'{5}','price':'{6}','account':'{7}','amount':'{8}','createDate':'{9}','overAccount':'{10}',
                                'overAmount':'{11}','state':'{12}','content': [{13}]";

            string conJson = @"'fsOrderContentId':'{0}','ygOrderContentID':'{1}','hOrderContentID':'{2}','fsCmmdID':'{3}','ygCmmdID':'{4}',
                                'hCmmdID':'{5}','price':'{6}','account':'{7}','amount':'{8}','createDate':'{9}','overAccount':'{10}',
                                'overAmount':'{11}','sendDate':'{12}','overDate':'{13}','lotNo':'{14}','manuDate':'{15}',
                                'expiredDate':'{16}','state':'{17}'";

            ArrayList al = null;
            Hashtable ht = new Hashtable();

            foreach (Neusoft.HISFC.BizLogic.Material.Object.Input input in list)
            {
                if (ht.ContainsKey(input.InListCode))
                {
                    al = ht[input.InListCode] as ArrayList;
                    al.Add(input);
                }
                else
                {
                    al = new ArrayList();
                    al.Add(input);

                    ht.Add(input.InListCode, al);
                }
            }

            StringBuilder sbDetail = null;
            StringBuilder sb = null;

            decimal planCost = 0m;
            decimal totPurCost = 0m;
            decimal totPlanCost = 0m;

            int result = 0;

            foreach (string key in ht.Keys)
            {
                al = ht[key] as ArrayList;
                sbDetail = new StringBuilder();

                foreach (Neusoft.HISFC.BizLogic.Material.Object.Input input in al)
                {
                    sb = new StringBuilder();

                    planCost = Math.Round(input.InPrice * input.StockInfo.LowNum, 2);

                    sb.Append("{").Append(string.Format(conJson, "", "", input.OutNo, input.StockInfo.BaseInfo.PlatMatCode, input.StockInfo.BaseInfo.SunMatCode,
                        input.StockInfo.BaseInfo.ID, input.InPrice.ToString(), planCost.ToString(), input.StockInfo.LowNum, input.ApplyOper.OperTime.ToString("yyyy-MM-dd"),
                        input.InCost, input.InNum.ToString(), "", "", input.StockInfo.BatchNo, "", input.StockInfo.ValidDate.ToString(), "40")).Append("}");

                    sbDetail.Append("{").Append(string.Format(detailJson, "", "", input.OutNo, input.StockInfo.BaseInfo.PlatMatCode, input.StockInfo.BaseInfo.SunMatCode,
                        input.StockInfo.BaseInfo.ID, input.InPrice.ToString(), planCost.ToString(), input.StockInfo.LowNum, input.ApplyOper.OperTime.ToString("yyyy-MM-dd"),
                        input.InCost, input.InNum.ToString(), "70", sb.ToString())).Append("}").Append(",");

                    totPlanCost += planCost;
                    totPurCost += input.InCost;

                }

                Neusoft.HISFC.BizLogic.Material.Object.Input input0 = al[0] as Neusoft.HISFC.BizLogic.Material.Object.Input;

                data = string.Format(json, "", "", key, "", Manager.setObj.HospitalName, input0.StockInfo.Storage.Name, input0.StockInfo.Company.Name,
                    totPlanCost.ToString(), totPurCost.ToString(), "80", "", "", "", "", sbDetail.ToString());

                al = null;
                sb = null;
                sbDetail = null;

                result = this.UploadInfo("HH004", data, ref error);
                if (result < 0)
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 4.24 医疗机构耗材采购订单查询(HH004A)
        /// </summary>
        /// <returns></returns>
        public int QueryMatOrderInfo()
        {
            string data = string.Empty;
            string error = string.Empty;

            string json = @"'hOrderID':'{0}'";

            return this.UploadInfo("HH004A", data, ref error);
        }

        /// <summary>
        /// 4.25 上报医疗机构耗材退货信息(HH005)
        /// </summary>
        /// <param name="list"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int UploadMatReturnInfo(ArrayList list, ref string error)
        {
            string data = string.Empty;

            string json = @"'fsRGOrderCode':'{0}','fsRGOrderID':'{1}','ygRGOrderID':'{2}','hRGOrderID':'{3}','fsOrderID':'{4}',
                            'fsOrderDetailId':'{5}','ygOrderID':'{6}','ygOrderDetailID':'{7}','hOrderID':'{8}','hOrderDetailID':'{9}',
                            'buyerName':'{10}','senderName':'{11}','totalAccount':'{12}','amount':'{13}','state':'{14}',
                            'returnReason':'{15}','senderRemark':'{16}','closeDate':'{17}',list:[{18}]";

            string detailJson = @"'fsRGOrderID':'{0}','ygRGOrderID':'{1}','hRGOrderID':'{2}','fsRGOrderDetailID':'{3}','ygRGOrderDetailID':'{4}',
                                'hRGOrderDetailID':'{5}','fsCmmdID':'{6}','ygCmmdID':'{7}','hCmmdID':'{8}','totalAccount':'{9}','amount':'{10}',
                                'lotNo':'{11}','manuDate':'{12}','expiredDate':'{13}','state':'{14}'";

            string detail = string.Empty;
            int result = 0;
            foreach (Neusoft.HISFC.BizLogic.Material.Object.Input input in list)
            {
                detail = string.Format(detailJson, "", "", input.InListCode, "", "", input.OutNo, input.StockInfo.BaseInfo.PlatMatCode,
                    input.StockInfo.BaseInfo.SunMatCode, input.StockInfo.BaseInfo.ID, Math.Abs(input.InCost).ToString(), Math.Abs(input.InNum).ToString(),
                    input.StockInfo.BatchNo, "", input.StockInfo.ValidDate.ToString("yyyy-MM-dd"), "40");

                data = string.Format(json, "", "", "", input.InListCode, "", "", "", "", input.ReturnInNo, input.Memo, Manager.setObj.HospitalName, input.StockInfo.Company.Name,
                     Math.Abs(input.InCost).ToString(), Math.Abs(input.InNum).ToString(), "40", "原因", "", "", "{" + detail + "}");

                result = this.UploadInfo("HH005", data, ref error);
                if (result < 0)
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 4.26	医疗机构耗材退货信息查询(HH005A)
        /// </summary>
        /// <returns></returns>
        public int QueryMatReturnInfo()
        {
            string data = string.Empty;
            string error = string.Empty;

            string json = @"'hRGOrderID':'{0}'";

            return this.UploadInfo("HH005A", data, ref error);
        }

        /// <summary>
        /// 4.27 上报医疗机构耗材采购订单发票(HH006)
        /// </summary>
        /// <param name="list"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int UploadMatInvoiceInfo(ArrayList list, ref string error)
        {
            string data = string.Empty;

            string json = @"'fsInvoiceID':'{0}','ygInvoiceID':'{1}','hInvoiceID':'{2}','buyerName':'{3}','senderName':'{4}',
                           'totalAccount':'{5}','invoiceDate':'{6}','invoiceType':'{7}','invoiceName':'{8}','invoiceNo':'{9}',
                            'state':'{10}','returnReason':'{11}',list:[{12}]";

            string detailJson = @"'ygOrderID':'{0}','ygOrderDetailID':'{1}','ygOrderContentID':'{2}','hOrderID':'{3}','hOrderDetailID':'{4}','hOrderContentID':'{5}'";

            Hashtable ht = new Hashtable();
            ArrayList al = new ArrayList();
            foreach (Neusoft.HISFC.BizLogic.Material.Object.Input input in list)
            {
                if (ht.ContainsKey(input.InvoiceNo))
                {
                    al = ht[input.InvoiceNo] as ArrayList;
                    al.Add(input);
                }
                else
                {
                    al = new ArrayList();
                    al.Add(input);
                    ht.Add(input.InvoiceNo, al);
                }
            }

            StringBuilder sb = null;
            Neusoft.HISFC.BizLogic.Material.Object.Input input0 = null;
            decimal totCost = 0m;
            string invoiceType = string.Empty;

            foreach (string key in ht.Keys)
            {
                al = ht[key] as ArrayList;

                sb = new StringBuilder();
                input0 = al[0] as Neusoft.HISFC.BizLogic.Material.Object.Input;
                totCost = 0m;
                foreach (Neusoft.HISFC.BizLogic.Material.Object.Input input in al)
                {
                    totCost += Math.Abs(input.InCost);
                    sb.Append("{").Append(string.Format(detailJson, "", "", "", input.InListCode, input.OutNo, input.OutNo)).Append("},");
                }

                data = string.Format(json, "", "", input0.InvoiceNo, Manager.setObj.HospitalName, input0.StockInfo.Company.Name, totCost, input0.InvoiceDate.ToString("yyyy-MM-dd"),
                    input0.Oper.Memo, input0.Name, input0.InvoiceNo, "20", "", sb.ToString().TrimEnd(','));
            }

            return this.UploadInfo("HH006", data, ref error);
        }

        /// <summary>
        /// 4.28 医疗机构耗材采购订单发票查询(HH006A)
        /// </summary>
        /// <returns></returns>
        public int QueryMatInvoiceInfo()
        {
            string data = string.Empty;
            string error = string.Empty;

            string json = @"'hInvoiceID':'{0}'";

            return this.UploadInfo("HH006A", data, ref error);
        }

        /// <summary>
        /// 4.32	上报医疗机材采购订单发票附件(HV001)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int UploadMatInvoiceFileInfo(Neusoft.HISFC.BizLogic.Material.Object.Input input, ref string error)
        {
            string data = string.Empty;

            string json = @"'ygInvoiceID':'{0}','hInvoiceID':'{1}','fileName':'{2}','uploadFile':'{3}'";

            data = string.Format(json, "", input.InvoiceNo, input.User01, input.Memo);

            return this.UploadInfo("HV001", data, ref error);
        }


        //4.33	上报医疗机构采购结算信息(HC001)
        public int UploadMatBalanceInfo(ArrayList list, ref string error)
        {
            string data = string.Empty;

            string json = @"'ygBalanceId':'{0}','hBalanceId':'{1}','buyerName':'{2}','salerName':'{3}','senderName':'{4}',
                            'total':'{5}','beginDate':'{6}','endDate':'{7}','remark':'{8}','state':'{9}','list':[{10}]";

            string detailJson = @"'ygBalanceDetailId':'{0}','hBalanceDetailId':'{1}','ygOrderType':'{2}','ygInvoiceID':'{3}','hInvoiceID':'{4}'";

            Hashtable ht = new Hashtable();
            ArrayList al = new ArrayList();

            string primarykey = string.Empty;
            foreach (Neusoft.HISFC.BizLogic.Material.Object.MatPay pay in list)
            {
                primarykey = pay.Company.Name + pay.Oper.OperTime.ToString("yyyyMM");
                if (ht.ContainsKey(primarykey))
                {
                    al = ht[primarykey] as ArrayList;
                    al.Add(pay);
                }
                else
                {
                    al = new ArrayList();
                    al.Add(pay);
                    ht.Add(primarykey, al);
                }
            }

            StringBuilder sb = null;
            Neusoft.HISFC.BizLogic.Material.Object.MatPay pay0 = null;
            decimal totCost = 0m;
            string invoiceType = string.Empty;

            DateTime dtBegin = DateTime.Now;
            DateTime dtEnd = DateTime.Now;
            int result = -1;
            foreach (string key in ht.Keys)
            {
                al = ht[key] as ArrayList;

                sb = new StringBuilder();
                pay0 = al[0] as Neusoft.HISFC.BizLogic.Material.Object.MatPay;
                totCost = 0m;
                string type = string.Empty;

                dtBegin = new DateTime(pay0.Oper.OperTime.Year, pay0.Oper.OperTime.Month, 1);
                dtEnd = dtBegin.AddMonths(1).AddSeconds(-1);

                foreach (Neusoft.HISFC.BizLogic.Material.Object.MatPay pay in al)
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

        //4.34	医疗机构采购结算信息查询(HC001A)
        public int QueryMatBalanceInfo()
        {
            string data = string.Empty;
            string error = string.Empty;

            string json = @"'hBalanceId':'{0}'";

            return this.UploadInfo("HC001A", data, ref error);
        }
    }
}
