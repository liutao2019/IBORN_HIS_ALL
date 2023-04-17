using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Class
{
    public class JsonResult
    {
        public string code { get; set; }
        public string describe { get; set; }
        public List<Result> result { get; set; }
    }


    public class Result
    {
        public string address { get; set; }
        public string bankAccount { get; set; }
        public string checker { get; set; }
        public string clerk { get; set; }
        public string clerkId { get; set; }
        public string deptId { get; set; }
        public string imgUrls { get; set; }
        public string listFlag { get; set; }
        public string listName { get; set; }
        public string notifyEmail { get; set; }
        public string ofdUrl { get; set; }
        public string oldInvoiceCode { get; set; }
        public string oldInvoiceNo { get; set; }
        public string orderAmount { get; set; }
        public string payee { get; set; }
        public string phone { get; set; }
        public string productOilFlag { get; set; }
        public string remark { get; set; }
        public string saleName { get; set; }
        public string salerAccount { get; set; }
        public string salerAddress { get; set; }
        public string salerTaxNum { get; set; }
        public string salerTel { get; set; }
        public string telephone { get; set; }
        public string terminalNumber { get; set; }
        public string serialNo { get; set; }
        public string orderNo { get; set; }
        public string status { get; set; }
        public string statusMsg { get; set; }
        public string failCause { get; set; }
        public string pdfUrl { get; set; }
        public string pictureUrl { get; set; }
        public string invoiceTime { get; set; }
        public string invoiceCode { get; set; }
        public string invoiceNo { get; set; }
        public string exTaxAmount { get; set; }
        public string taxAmount { get; set; }
        public string payerName { get; set; }
        public string payerTaxNo { get; set; }
        public string invoiceKind { get; set; }
        public string checkCode { get; set; }
        public string qrCode { get; set; }
        public string machineCode { get; set; }
        public string cipherText { get; set; }

        public string invoiceSerialNum { get; set; }
    }
}
