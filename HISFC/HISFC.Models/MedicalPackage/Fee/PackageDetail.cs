using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.MedicalPackage.Fee
{
    public class PackageDetail : FS.FrameWork.Models.NeuObject
    {
        ///ID为购买流水号

        /// <summary>
        /// 套餐项目内流水号
        /// </summary>
        private string sequenceNO = string.Empty;

        public string SequenceNO
        {
            get { return this.sequenceNO; }
            set { this.sequenceNO = value; }
        }

        /// <summary>
        /// 交易类型 1-正交易，2-逆交易
        /// </summary>
        private string trans_Type = "1";

        public string Trans_Type
        {
            get { return this.trans_Type; }
            set { this.trans_Type = value; }
        }

        /// <summary>
        /// 缴费标识
        /// </summary>
        private string payFlag = string.Empty;

        public string PayFlag
        {
            get { return this.payFlag; }
            set { this.payFlag = value; }
        }

        /// <summary>
        /// 患者卡号
        /// </summary>
        private string cardno = string.Empty;

        public string CardNO
        {
            get { return this.cardno; }
            set { this.cardno = value; }
        }

        /// <summary>
        /// 单位
        /// </summary>
        private string unit = string.Empty;

        public string Unit
        {
            get { return this.unit; }
            set { this.unit = value; }
        }

        /// <summary>
        /// 0 - 最小单位，1 - 包装单位
        /// </summary>
        private string unitFlag = "0";

        public string UnitFlag
        {
            get { return this.unitFlag; }
            set { this.unitFlag = value; }
        }

        /// <summary>
        /// 药品项目/非药品项目
        /// </summary>
        private FS.HISFC.Models.Base.Item item;

        public FS.HISFC.Models.Base.Item Item
        {
            get
            {
                if (item == null)
                {
                    item = new FS.HISFC.Models.Base.Item();
                }

                return this.item;
            }
            set
            {
                this.item = value;
            }
        }

        /// <summary>
        /// 执行科室
        /// </summary>
        private FS.FrameWork.Models.NeuObject execDept = null;

        public FS.FrameWork.Models.NeuObject ExecDept
        {
            get
            {
                if (this.execDept == null)
                {
                    this.execDept = new FS.FrameWork.Models.NeuObject();
                }
                return this.execDept;
            }
            set { this.execDept = value; }
        }

        private decimal detail_cost = 0;

        /// <summary>
        /// 套餐原价
        /// </summary>
        public decimal Detail_Cost
        {
            get { return this.detail_cost; }
            set { this.detail_cost = value; }
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

        private decimal rtnQTY = 0;

        /// <summary>
        /// 可退数量
        /// </summary>
        public decimal RtnQTY
        {
            get { return this.rtnQTY; }
            set { this.rtnQTY = value; }
        }

        private decimal confirmQTY = 0;

        /// <summary>
        /// 已做数量
        /// </summary>
        public decimal ConfirmQTY
        {
            get { return this.confirmQTY; }
            set { this.confirmQTY = value; }
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

        public string PackageName { set; get; }


        //{01C202AF-5D8A-4d89-9BA5-1910B5AA7607}
        //增加院区属性
        #region

        private string hospitalID;

        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospitalID
        {
            get { return this.hospitalID; }
            set { this.hospitalID = value; }
        }

        private string hospitalName;

        /// <summary>
        /// 院区名称
        /// </summary>
        public string HospitalName
        {
            get { return this.hospitalName; }
            set { this.hospitalName = value; }
        }

        #endregion

        //private string pactCode;

        ///// <summary>
        ///// {A777B7DF-AB62-4603-A0F6-B3643AD442F0}
        ///// 合同单位
        ///// </summary>
        //public string PactCode
        //{
        //    get { return pactCode; }
        //    set { pactCode = value; }
        //}

        private string specialFlag;

        /// <summary>
        /// {56809DCA-CD5A-435e-86F0-93DE99227DF4}
        /// 特殊折扣标记
        /// </summary>
        public string SpecialFlag
        {
            get { return specialFlag; }
            set { specialFlag = value; }
        }

        public new PackageDetail Clone()
        {
            PackageDetail packagedetail = new PackageDetail();
            packagedetail.ID = this.ID;
            packagedetail.PayFlag = this.PayFlag;
            packagedetail.Trans_Type = this.Trans_Type;
            packagedetail.SequenceNO = this.SequenceNO;
            packagedetail.Item = this.item.Clone();
            packagedetail.PayFlag = packagedetail.PayFlag;
            packagedetail.CardNO = this.CardNO;
            packagedetail.ExecDept = this.ExecDept;
            packagedetail.Unit = this.Unit;
            packagedetail.UnitFlag = this.UnitFlag;
            packagedetail.Detail_Cost = this.Detail_Cost;
            packagedetail.Real_Cost = this.Real_Cost;
            packagedetail.Gift_cost = this.Gift_cost;
            packagedetail.Etc_cost = this.Etc_cost;
            packagedetail.RtnQTY = this.RtnQTY;
            packagedetail.ConfirmQTY = this.ConfirmQTY;
            packagedetail.InvoiceNO = this.InvoiceNO;
            packagedetail.Oper = this.Oper;
            packagedetail.OperTime = this.OperTime;
            packagedetail.Memo = this.Memo;
            packagedetail.CancelOper = this.CancelOper;
            packagedetail.CancelTime = this.CancelTime;
            packagedetail.Cancel_Flag = this.Cancel_Flag;
            packagedetail.PackageName = this.PackageName;

            //{01C202AF-5D8A-4d89-9BA5-1910B5AA7607} 
            packagedetail.HospitalID = this.HospitalID;
            packagedetail.HospitalName = this.HospitalName;

            //{A777B7DF-AB62-4603-A0F6-B3643AD442F0}
            //{56809DCA-CD5A-435e-86F0-93DE99227DF4}
            //packagedetail.PactCode = this.PactCode;
            packagedetail.SpecialFlag = this.SpecialFlag;

            return packagedetail;
        }

        //{9D8048C5-1DC4-4dcd-9C2F-A3EF0B298C69}
        public PackageDetail CloneAll()
        {
            PackageDetail packagedetail = new PackageDetail();
            packagedetail.ID = this.ID;
            packagedetail.PayFlag = this.PayFlag;
            packagedetail.Trans_Type = this.Trans_Type;
            packagedetail.SequenceNO = this.SequenceNO;
            packagedetail.Item = this.item.Clone();

            if (packagedetail.Item == null)
            {
                packagedetail.Item = new Item();
            }

            if (this.item != null)
            {
                packagedetail.item.ID = this.item.ID;
                packagedetail.item.Name = this.item.Name;
                packagedetail.item.Specs = this.item.Specs;
            }

            packagedetail.PayFlag = packagedetail.PayFlag;
            packagedetail.CardNO = this.CardNO;
            packagedetail.ExecDept = this.ExecDept;
            packagedetail.Unit = this.Unit;
            packagedetail.UnitFlag = this.UnitFlag;
            packagedetail.Detail_Cost = this.Detail_Cost;
            packagedetail.Real_Cost = this.Real_Cost;
            packagedetail.Gift_cost = this.Gift_cost;
            packagedetail.Etc_cost = this.Etc_cost;
            packagedetail.RtnQTY = this.RtnQTY;
            packagedetail.ConfirmQTY = this.ConfirmQTY;
            packagedetail.InvoiceNO = this.InvoiceNO;
            packagedetail.Oper = this.Oper;
            packagedetail.OperTime = this.OperTime;
            packagedetail.Memo = this.Memo;
            packagedetail.CancelOper = this.CancelOper;
            packagedetail.CancelTime = this.CancelTime;
            packagedetail.Cancel_Flag = this.Cancel_Flag;
            packagedetail.PackageName = this.PackageName;

            //{01C202AF-5D8A-4d89-9BA5-1910B5AA7607} 
            packagedetail.HospitalID = this.HospitalID;
            packagedetail.HospitalName = this.HospitalName;

            //{A777B7DF-AB62-4603-A0F6-B3643AD442F0}
            //{56809DCA-CD5A-435e-86F0-93DE99227DF4}
            //packagedetail.PactCode = this.PactCode;
            packagedetail.SpecialFlag = this.SpecialFlag;

            return packagedetail;
        }
    }
}
