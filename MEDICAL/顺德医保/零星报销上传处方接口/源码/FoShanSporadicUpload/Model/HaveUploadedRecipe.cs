using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoShanSporadicUpload.Model
{
    /// <summary>
    /// 已上传处方明细汇总信息
    /// </summary>
    public class HaveUploadedRecipe
    {
        /// <summary>
        /// 证件号码
        /// </summary>
        private string idNO;

        /// <summary>
        /// 证件号码
        /// </summary>
        public string IdNO
        {
            get { return idNO; }
            set { idNO = value; }
        }

        /// <summary>
        /// 医保编号
        /// </summary>
        private string medicalCardNO;

        /// <summary>
        /// 医保编号
        /// </summary>
        public string MedicalCardNO
        {
            get { return medicalCardNO; }
            set { medicalCardNO = value; }
        }

        /// <summary>
        /// 门诊住院标志：1-门诊、2-住院
        /// </summary>
        private string serviceType;

        /// <summary>
        /// 门诊住院标志：1-门诊、2-住院
        /// </summary>
        public string ServiceType
        {
            get { return serviceType; }
            set { serviceType = value; }
        }

        /// <summary>
        /// 处方号:门诊时，则为门诊号，住院时，则为住院号
        /// </summary>
        private string patientID;

        /// <summary>
        /// 处方号:门诊时，则为门诊号，住院时，则为住院号
        /// </summary>
        public string PatientID
        {
            get { return patientID; }
            set { patientID = value; }
        }

        /// <summary>
        /// 住院次数
        /// </summary>
        private string inTimes;

        /// <summary>
        /// 住院次数
        /// </summary>
        public string InTimes
        {
            get { return inTimes; }
            set { inTimes = value; }
        }

        /// <summary>
        /// 项目序号 明细回退时序号不变
        /// </summary>
        private string sequenceNO;

        /// <summary>
        /// 项目序号 明细回退时序号不变
        /// </summary>
        public string SequenceNO
        {
            get { return sequenceNO; }
            set { sequenceNO = value; }
        }

        /// <summary>
        /// 大类代码
        /// </summary>
        private string statCode;

        /// <summary>
        /// 大类代码
        /// </summary>
        public string StatCode
        {
            get { return statCode; }
            set { statCode = value; }
        }

        /// <summary>
        /// 大类名称
        /// </summary>
        private string statName;

        /// <summary>
        /// 大类名称
        /// </summary>
        public string StatName
        {
            get { return statName; }
            set { statName = value; }
        }

        /// <summary>
        /// 项目代码
        /// </summary>
        private string centerItemCode;

        /// <summary>
        /// 项目代码
        /// </summary>
        public string CenterItemCode
        {
            get { return centerItemCode; }
            set { centerItemCode = value; }
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        private string centerItemName;

        /// <summary>
        /// 项目名称
        /// </summary>
        public string CenterItemName
        {
            get { return centerItemName; }
            set { centerItemName = value; }
        }

        /// <summary>
        /// 药监局药品编码
        /// </summary>
        private string govermentItemCode;

        /// <summary>
        /// 药监局药品编码
        /// </summary>
        public string GovermentItemCode
        {
            get { return govermentItemCode; }
            set { govermentItemCode = value; }
        }

        /// <summary>
        /// 限制用药标记
        /// </summary>
        private string specialDrugFlag;

        /// <summary>
        /// 限制用药标记
        /// </summary>
        public string SpecialDrugFlag
        {
            get { return specialDrugFlag; }
            set { specialDrugFlag = value; }
        }

        /// <summary>
        /// 医用材料的注册证产品名称
        /// </summary>
        private string registerName;

        /// <summary>
        /// 医用材料的注册证产品名称
        /// </summary>
        public string RegisterName
        {
            get { return registerName; }
            set { registerName = value; }
        }

        /// <summary>
        /// 医用材料的食药监注册号
        /// </summary>
        private string registerNO;

        /// <summary>
        /// 医用材料的食药监注册号
        /// </summary>
        public string RegisterNO
        {
            get { return registerNO; }
            set { registerNO = value; }
        }
        
        /// <summary>
        /// 数量
        /// </summary>
        private decimal qty;

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        /// <summary>
        /// 单价
        /// </summary>
        private decimal itemPrice;

        /// <summary>
        /// 单价
        /// </summary>
        public decimal ItemPrice
        {
            get { return itemPrice; }
            set { itemPrice = value; }
        }

        /// <summary>
        /// 费用总额
        /// </summary>
        private decimal totCost;

        /// <summary>
        /// 费用总额
        /// </summary>
        public decimal TotCost
        {
            get { return totCost; }
            set { totCost = value; }
        }

        /// <summary>
        /// 产地
        /// </summary>
        private string productAddress;

        /// <summary>
        /// 产地
        /// </summary>
        public string ProductAddress
        {
            get { return productAddress; }
            set { productAddress = value; }
        }

        /// <summary>
        /// 规格型号
        /// </summary>
        private string spces;

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Spces
        {
            get { return spces; }
            set { spces = value; }
        }

        /// <summary>
        /// 计价单位
        /// </summary>
        private string itemUnit;

        /// <summary>
        /// 计价单位
        /// </summary>
        public string ItemUnit
        {
            get { return itemUnit; }
            set { itemUnit = value; }
        }

        /// <summary>
        /// 剂型
        /// </summary>
        private string doseForm;

        /// <summary>
        /// 剂型
        /// </summary>
        public string DoseForm
        {
            get { return doseForm; }
            set { doseForm = value; }
        }

        /// <summary>
        /// 使用情况
        /// </summary>
        private string useInfo;

        /// <summary>
        /// 使用情况
        /// </summary>
        public string UseInfo
        {
            get { return useInfo; }
            set { useInfo = value; }
        }

        /// <summary>
        /// 费用开始日期
        /// </summary>
        private string startTime;

        /// <summary>
        /// 费用开始日期
        /// </summary>
        public string StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        /// <summary>
        /// 费用终止日期
        /// </summary>
        private string endTime;

        /// <summary>
        /// 费用终止日期
        /// </summary>
        public string EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        /// <summary>
        /// 处方医生姓名
        /// </summary>
        private string doctName;

        /// <summary>
        /// 处方医生姓名
        /// </summary>
        public string DoctName
        {
            get { return doctName; }
            set { doctName = value; }
        }

        /// <summary>
        /// 科室名称
        /// </summary>
        private string deptName;

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }

        /// <summary>
        /// 收费时间
        /// </summary>
        private string feeDate;

        /// <summary>
        /// 收费时间
        /// </summary>
        public string FeeDate
        {
            get { return feeDate; }
            set { feeDate = value; }
        }

        /// <summary>
        /// 发票号
        /// </summary>
        private string invoiceNO;

        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNO
        {
            get { return invoiceNO; }
            set { invoiceNO = value; }
        }
        /// <summary>
        /// 工伤标志 1-工伤、0-医疗
        /// </summary>
        private string gsType;

        /// <summary>
        /// 工伤标志 1-工伤、0-医疗
        /// </summary>
        public string GSType
        {
            get { return gsType; }
            set { gsType = value; }
        }
    }
}
