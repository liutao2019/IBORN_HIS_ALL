using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.MedicalPackage.Fee
{
    public class Package : FS.FrameWork.Models.NeuObject
    {
        private string trans_Type = "1";

        /// <summary>
        /// 交易类型 1-正交易，2-逆交易
        /// </summary>
        public string Trans_Type
        {
            get { return this.trans_Type; }
            set { this.trans_Type = value; }
        }

        private string recipeno = string.Empty;

        /// <summary>
        /// 划价单号
        /// </summary>
        public string RecipeNO
        {
            get { return this.recipeno; }
            set { this.recipeno = value; }
        }

        private string sequenceNO = string.Empty;

        /// <summary>
        /// 单内序列号
        /// </summary>
        public string SequenceNO
        {
            get { return this.sequenceNO; }
            set { this.sequenceNO = value; }
        }

        //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
        private string packageSequenceNO = string.Empty;

        /// <summary>
        /// 单内套餐序列号
        /// </summary>
        public string PackageSequenceNO
        {
            get { return this.packageSequenceNO; }
            set { this.packageSequenceNO = value; }
        }

        private FS.HISFC.Models.MedicalPackage.Package packageInfo = null;

        /// <summary>
        /// 套餐信息
        /// </summary>
        public FS.HISFC.Models.MedicalPackage.Package PackageInfo
        {
            get
            {
                if (this.packageInfo == null)
                {
                    this.packageInfo = new FS.HISFC.Models.MedicalPackage.Package();
                }
                return this.packageInfo;
            }
            set { this.packageInfo = value; }
        }

        //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
        private FS.HISFC.Models.MedicalPackage.Package parentPackageInfo = null;

        /// <summary>
        /// 父套餐信息
        /// </summary>
        public FS.HISFC.Models.MedicalPackage.Package ParentPackageInfo
        {
            get
            {
                if (this.parentPackageInfo == null)
                {
                    this.parentPackageInfo = new FS.HISFC.Models.MedicalPackage.Package();
                } 
                return this.parentPackageInfo;
            }
            set { this.parentPackageInfo = value; }
        }

        private int packageNum = 1;

        /// <summary>
        /// 套餐数量
        /// </summary>
        public int PackageNum
        {
            get { return packageNum; }
            set { packageNum = value; }
        }

        private string pay_Flag = string.Empty;

        /// <summary>
        /// 缴费状态 0-未缴费，1-已缴费
        /// </summary>
        public string Pay_Flag
        {
            get { return this.pay_Flag; }
            set { this.pay_Flag = value; }
        }

        private HISFC.Models.RADT.Patient patient = null;

        /// <summary>
        /// 患者信息(姓名，性别，出生日期)
        /// </summary>
        public HISFC.Models.RADT.Patient Patient
        {
            get 
            {
                if (patient == null)
                {
                    patient = new FS.HISFC.Models.RADT.Patient();
                }
                return this.patient; 
            }
            set { this.patient = value; }
        }

        private string card_Level = string.Empty;

        /// <summary>
        /// 会员等级
        /// </summary>
        public string Card_Level
        {
            get { return this.card_Level; }
            set { this.card_Level = value; }
        }

        //private string payKind_Code = string.Empty;

        ///// <summary>
        ///// 合同单位
        ///// </summary>
        //public string PayKind_Code
        //{
        //    get { return this.payKind_Code; }
        //    set { this.payKind_Code = value; }
        //}

        private string specialFlag = "0";

        /// <summary>
        /// {56809DCA-CD5A-435e-86F0-93DE99227DF4}
        /// 特殊折扣标记
        /// </summary>
        public string SpecialFlag
        {
            get { return specialFlag; }
            set { specialFlag = value; }
        }

        private object package_Dept = null;

        /// <summary>
        /// 套餐科室
        /// </summary>
        public object Package_Dept
        {
            get { return this.package_Dept; }
            set { this.package_Dept = value; }
        }

        private object consultant = null;

        /// <summary>
        /// 客服
        /// </summary>
        public object Consultant
        {
            get { return this.consultant; }
            set { this.consultant = value; }
        }

        /// <summary>
        /// 划价人
        /// </summary>
        private OperEnvironment delimitOper = new OperEnvironment();

        /// <summary>
        /// 划价人
        /// </summary>
        public string DelimitOper
        {
            get
            {
                if (this.delimitOper == null)
                {
                    this.delimitOper = new OperEnvironment();
                }
                return this.delimitOper.ID;
            }
            set
            {
                if (this.delimitOper == null)
                {
                    this.delimitOper = new OperEnvironment();
                }
                this.delimitOper.ID = value;
            }
        }

        /// <summary>
        /// 划价时间
        /// </summary>
        public DateTime DelimitTime
        {
            get
            {
                if (this.delimitOper == null)
                {
                    this.delimitOper = new OperEnvironment();
                }
                return this.delimitOper.OperTime;
            }
            set
            {
                if (this.delimitOper == null)
                {
                    this.delimitOper = new OperEnvironment();
                }
                this.delimitOper.OperTime = value;
            }
        }

        private decimal package_cost = 0;

        /// <summary>
        /// 套餐原价
        /// </summary>
        public decimal Package_Cost
        {
            get { return this.package_cost; }
            set { this.package_cost = value; }
        }

        private decimal real_cost = 0;

        /// <summary>
        /// 实收原价
        /// </summary>
        public decimal Real_Cost
        {
            get { return this.real_cost; }
            set { this.real_cost = value; }
        }

        private decimal gift_cost = 0;

        /// <summary>
        /// 赠送金额
        /// </summary>
        public decimal Gift_cost
        {
            get { return this.gift_cost; }
            set { this.gift_cost = value; }
        }

        private decimal etc_cost = 0;

        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal Etc_cost
        {
            get { return this.etc_cost; }
            set { this.etc_cost = value; }
        }

        private string invoiceseq = string.Empty;

        /// <summary>
        /// 发票内序号
        /// </summary>
        public string Invoiceseq
        {
            get { return this.invoiceseq; }
            set { this.invoiceseq = value; }
        }

        private string invoiceNO = string.Empty;

        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNO
        {
            get { return this.invoiceNO; }
            set { this.invoiceNO = value; }
        }

        /// <summary>
        /// 结账人
        /// </summary>
        private OperEnvironment OperInfo = new OperEnvironment();

        /// <summary>
        /// 结账人
        /// </summary>
        public string Oper
        {
            get
            {
                if (this.OperInfo == null)
                {
                    this.OperInfo = new OperEnvironment();
                }
                return this.OperInfo.ID;
            }
            set
            {
                if (this.OperInfo == null)
                {
                    this.OperInfo = new OperEnvironment();
                }
                this.OperInfo.ID = value;
            }
        }

        /// <summary>
        /// 结账时间
        /// </summary>
        public DateTime OperTime
        {
            get
            {
                if (this.OperInfo == null)
                {
                    this.OperInfo = new OperEnvironment();
                }
                return this.OperInfo.OperTime;
            }
            set
            {
                if (this.OperInfo == null)
                {
                    this.OperInfo = new OperEnvironment();
                }
                this.OperInfo.OperTime = value;
            }
        }

        private string cancel_flag = string.Empty;

        /// <summary>
        /// 有效标志0-有效，1-退款，2-半退
        /// </summary>
        public string Cancel_Flag
        {
            get { return this.cancel_flag; }
            set { this.cancel_flag = value; }
        }

        /// <summary>
        /// 取消人
        /// </summary>
        private OperEnvironment CancelInfo = new OperEnvironment();

        /// <summary>
        /// 取消人
        /// </summary>
        public string CancelOper
        {
            get
            {
                if (this.CancelInfo == null)
                {
                    this.CancelInfo = new OperEnvironment();
                }
                return this.CancelInfo.ID;
            }
            set
            {
                if (this.CancelInfo == null)
                {
                    this.CancelInfo = new OperEnvironment();
                }
                this.CancelInfo.ID = value;
            }
        }

        /// <summary>
        /// 取消时间
        /// </summary>
        public DateTime CancelTime
        {
            get
            {
                if (this.CancelInfo == null)
                {
                    this.CancelInfo = new OperEnvironment();
                }
                return this.CancelInfo.OperTime;
            }
            set
            {
                if (this.CancelInfo == null)
                {
                    this.CancelInfo = new OperEnvironment();
                }
                this.CancelInfo.OperTime = value;
            }
        }

        private string original_code = string.Empty;

        /// <summary>
        /// 原单据号，发生在半退的时候
        /// </summary>
        public string Original_Code
        {
            get { return this.original_code; }
            set { this.original_code = value; }
        }

        //{2FB3E463-C00D-4ff4-974A-5D7AA0DB9CA0}
        private string cost_flag = "0";

        /// <summary>
        /// 消费标记
        /// </summary>
        public string Cost_Flag
        {
            get { return this.cost_flag; }
            set { this.cost_flag = value; }
        }

        //{2FB3E463-C00D-4ff4-974A-5D7AA0DB9CA0}
        private string cost_invoice;

        /// <summary>
        /// 套餐消费对应的发票
        /// </summary>
        public string Cost_Invoice
        {
            get { return this.cost_invoice; }
            set { this.cost_invoice = value; }
        }

      //  {01C202AF-5D8A-4d89-9BA5-1910B5AA7607}  添加分院id和分院名
        private string hospitalID;
        public string HospitalID
        {
            get { return this.hospitalID; }
            set { this.hospitalID = value; }
        }
        private string hospitalName;
        public string HospitalName
        {
            get { return this.hospitalName; }
            set { this.hospitalName = value; }
        }


        public new Package Clone()
        {
            Package package = new Package();
            package.patient = this.Patient.Clone();
            package.ID = this.ID;
            package.Trans_Type = this.Trans_Type;
            package.RecipeNO = this.RecipeNO;
            package.SequenceNO = this.SequenceNO;
            package.Invoiceseq = this.Invoiceseq;
            package.PackageInfo = this.PackageInfo.Clone();
            package.PackageNum = this.PackageNum;
            //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
            package.ParentPackageInfo = this.ParentPackageInfo.Clone();
            package.PackageSequenceNO = this.PackageSequenceNO;
            package.User01 = this.User01;
            package.Pay_Flag = this.Pay_Flag;
            package.Card_Level = this.Card_Level;
            //{56809DCA-CD5A-435e-86F0-93DE99227DF4}
            //package.PayKind_Code = this.PayKind_Code;
            package.SpecialFlag = this.SpecialFlag;
            package.Package_Dept = this.Package_Dept;
            package.Consultant = this.Consultant;
            package.DelimitOper = this.DelimitOper;
            package.DelimitTime = this.DelimitTime;
            package.Package_Cost = this.Package_Cost;
            package.Real_Cost = this.Real_Cost;
            package.Gift_cost = this.Gift_cost;
            package.Etc_cost = this.Etc_cost;
            package.InvoiceNO = this.InvoiceNO;
            package.Oper = this.Oper;
            package.OperTime = this.OperTime;
            package.Memo = this.Memo;
            package.CancelOper = this.CancelOper;
            package.CancelTime = this.CancelTime;
            package.Cancel_Flag = this.Cancel_Flag;
            package.Original_Code = this.Original_Code;
            //{2FB3E463-C00D-4ff4-974A-5D7AA0DB9CA0}
            package.Cost_Flag = this.Cost_Flag;
            package.Cost_Invoice = this.Cost_Invoice;
            //{01C202AF-5D8A-4d89-9BA5-1910B5AA7607} 
            package.HospitalID = this.HospitalID;
            package.HospitalName = this.HospitalName;
            return package;
        }

    }
}
