using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Registration
{
    /// <summary>
    /// 挂号费用明细
    /// </summary>
    [Serializable]
    public class RegisterFeeDetail : FS.FrameWork.Models.NeuObject
    {
        /// <summary>
        /// 患者信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            set
            {
                patient = value;
            }
            get
            {
                return patient;
            }
        }

        /// <summary>
        /// 门诊号
        /// </summary>
        private string clinicNo = string.Empty;

        /// <summary>
        /// 门诊号
        /// </summary>
        public string ClinicNO
        {
            get { return clinicNo; }
            set { clinicNo = value; }
        }

        private string sequenceNO = string.Empty;
        /// <summary>
        /// 序列号
        /// </summary>
        public string SequenceNO
        {
            get { return sequenceNO; }
            set { sequenceNO = value; }
        }

        /// <summary>
        /// 交易类型
        /// </summary>
        private TransTypes transType;

        /// <summary>
        /// 交易类型 
        /// </summary>
        public TransTypes TransType
        {
            get { return transType; }
            set { transType = value; }
        }

        /// <summary>
        /// 病历号
        /// </summary>
        public string CardNo
        {
            get { return this.patient.PID.CardNO; }
            set { this.patient.PID.CardNO = value; }
        }

        /// <summary>
        /// 卡类型
        /// </summary>
        private NeuObject markType = new NeuObject();

        /// <summary>
        /// 卡类型
        /// </summary>
        public NeuObject MarkType
        {
            get { return markType; }
            set { markType = value; }
        }

        /// <summary>
        /// 门诊卡号
        /// </summary>
        private string markNO = string.Empty;

        /// <summary>
        /// 门诊卡号
        /// </summary>
        public string MarkNO
        {
            get { return markNO; }
            set { markNO = value; }
        }

        /// <summary>
        /// 费用项目
        /// </summary>
        private Item feeItem;

        /// <summary>
        /// 费用项目
        /// </summary>
        public Item FeeItem
        {
            get { return this.feeItem; }
            set { this.feeItem = value; }
        }

        /// <summary>
        /// 原价
        /// </summary>
        private decimal tot_cost = 0;

        /// <summary>
        /// 费用总额
        /// </summary>
        public decimal Tot_cost
        {
            get { return tot_cost; }
            set { tot_cost = value; }
        }

        /// <summary>
        /// 数量
        /// </summary>
        private int qty = 0;

        /// <summary>
        /// 费用总额
        /// </summary>
        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        /// <summary>
        /// 实收
        /// </summary>
        private decimal real_cost = 0;

        /// <summary>
        /// 实收
        /// </summary>
        public decimal Real_cost
        {
            get { return real_cost; }
            set { real_cost = value; }
        }

        /// <summary>
        /// 赠送
        /// </summary>
        private decimal gift_cost = 0;

        /// <summary>
        /// 赠送
        /// </summary>
        public decimal Gift_cost
        {
            get { return gift_cost; }
            set { gift_cost = value; }
        }

        /// <summary>
        /// 优惠
        /// </summary>
        private decimal etc_cost = 0;

        /// <summary>
        /// 优惠
        /// </summary>
        public decimal Etc_cost
        {
            get { return etc_cost; }
            set { etc_cost = value; }
        }

        /// <summary>
        /// 发票号（流水号）
        /// </summary>
        string invoiceNo = string.Empty;

        /// <summary>
        /// 发票号（流水号）
        /// </summary>
        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }

        /// <summary>
        /// 打印发票号
        /// </summary>
        string print_invoiceNo = string.Empty;

        /// <summary>
        /// 打印发票号
        /// </summary>
        public string Print_InvoiceNo
        {
            get { return print_invoiceNo; }
            set { print_invoiceNo = value; }
        }

        /// <summary>
        /// 操作者
        /// </summary>
        private OperEnvironment oper = new OperEnvironment();

        /// <summary>
        /// 操作者
        /// </summary>
        public OperEnvironment Oper
        {
            get { return oper; }
            set { oper = value; }
        }

        /// <summary>
        /// 是否日结
        /// </summary>
        bool isBalance = false;

        /// <summary>
        /// 是否日结
        /// </summary>
        public bool IsBalance
        {
            get { return isBalance; }
            set { isBalance = value; }
        }

        /// <summary>
        /// 日结标识号
        /// </summary>
        string balanceNo = string.Empty;

        /// <summary>
        /// 日结标识号
        /// </summary>
        public string BalanceNo
        {
            get { return balanceNo; }
            set { balanceNo = value; }
        }

        /// <summary>
        /// 日结信息
        /// </summary>
        private OperEnvironment balanceOper = new OperEnvironment();

        /// <summary>
        /// 日结信息
        /// </summary>
        public OperEnvironment BalanceOper
        {
            get { return balanceOper; }
            set { balanceOper = value; }
        }

        /// <summary>
        /// 状态， 1 = 有效 0 = 无效 2 = 退费
        /// </summary>
        private int cancelFlag = 1;

        /// <summary>
        /// 状态， 1 = 有效 0 = 无效 2 = 退费
        /// </summary>
        public int CancelFlag
        {
            get { return cancelFlag; }
            set { cancelFlag = value; }
        }

        /// <summary>
        /// 取消人
        /// </summary>
        private OperEnvironment cancelOper = new OperEnvironment();

        /// <summary>
        /// 取消人
        /// </summary>
        public OperEnvironment CancelOper
        {
            get { return cancelOper; }
            set { cancelOper = value; }
        }



        /// <summary>
        /// 院区id{3515892E-1541-47de-8E0B-E306798A358C}
        /// </summary>
        string hospital_id = string.Empty;

        /// <summary>
        /// 院区id
        /// </summary>
        public string Hospital_id
        {
            get { return hospital_id; }
            set { hospital_id = value; }
        }


        /// <summary>
        /// 院区名
        /// </summary>
        string hospital_name = string.Empty;

        /// <summary>
        /// 院区名
        /// </summary>
        public string Hospital_name
        {
            get { return hospital_name; }
            set { hospital_name = value; }
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new RegisterFeeDetail Clone()
        {
            RegisterFeeDetail feeDetail = base.Clone() as RegisterFeeDetail;
            feeDetail.Patient = this.Patient.Clone();
            feeDetail.FeeItem = this.FeeItem.Clone();
            feeDetail.MarkType = this.MarkType.Clone();
            feeDetail.Oper = this.Oper.Clone();
            feeDetail.BalanceOper = this.BalanceOper.Clone();
            feeDetail.CancelOper = this.CancelOper.Clone();
            return feeDetail;
        }
    }
}
