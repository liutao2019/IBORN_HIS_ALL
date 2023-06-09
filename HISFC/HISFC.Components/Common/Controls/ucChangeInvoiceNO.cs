using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Common.Controls
{
    /// <summary>
    /// 初始化发票号
    /// </summary>
    public partial class ucChangeInvoiceNO : UserControl
    {
        public ucChangeInvoiceNO()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 发票实体
        /// </summary>
        private FS.HISFC.Models.Fee.Invoice invoice = new FS.HISFC.Models.Fee.Invoice();

        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        public event EventHandler my;

        #endregion

        #region 属性

        /// <summary>
        /// 传入发票实体
        /// </summary>
        public FS.HISFC.Models.Fee.Invoice Invoice
        {
            set
            {
                this.invoice = value;
                this.SetValue(this.invoice);
            }
            get
            {
                return this.invoice;
            }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 赋值
        /// </summary>
        private void SetValue(FS.HISFC.Models.Fee.Invoice invoiceJumpRecord)
        {
            this.lblBeginNO.Text = invoiceJumpRecord.BeginNO;
            this.lblEndNO.Text = invoiceJumpRecord.EndNO;
            this.lblInvoiceTypeID.Text = invoiceJumpRecord.Type.ID;
            this.lblInvoiceTypeName.Text = invoiceJumpRecord.Type.Name;
            this.lblUseNO.Text = invoiceJumpRecord.UsedNO;
            this.lblAcceptPerson.Text = invoiceJumpRecord.AcceptOper.ID;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            string nextInoviceNo = this.feeIntegrate.GetNewInvoiceNO(invoiceJumpRecord.Type.ID);
            if (nextInoviceNo == string.Empty)
            {
                this.lblNextNO.Text = "本号段已经使用完";
            }
            else
            {
                this.lblNextNO.Text = nextInoviceNo;
            }
            FS.FrameWork.Management.PublicTrans.RollBack();

            this.txtInput.Focus();

        }

        /// <summary>
        /// 校验输入号
        /// </summary>
        /// <param name="inputNO"></param>
        /// <returns></returns>
        private int ValidInputNO(string inputNO)
        {
            //2014-09-24 by han-zf 发票前缀长度计算,避免带非数字的发票跳号出错
            string prefixStr = string.Empty;
            this.GetPrefix(this.lblBeginNO.Text, this.lblEndNO.Text, ref prefixStr);
            int prefixLength = prefixStr.Length;

            inputNO = inputNO.Substring(prefixLength);
            Int64 intInutno;
            try
            {
                intInutno = Int64.Parse(inputNO);
            }
            catch (Exception)
            {

                MessageBox.Show("输入发票号应该为数字，请重新输入");
                this.txtInput.Focus();
                return -1;
            }

            Int64 intBegin = Int64.Parse(this.invoice.BeginNO.Substring(prefixLength));
            Int64 intEnd = Int64.Parse(this.invoice.EndNO.Substring(prefixLength));
            Int64 intUsedNO = Int64.Parse(this.invoice.UsedNO.Substring(prefixLength));
            Int64 intNextNO;
            try
            {
                intNextNO = Int64.Parse(this.lblNextNO.Text.Substring(prefixLength));
            }
            catch (Exception)
            {

                MessageBox.Show("该号段已经使用完，不能调号");
                return -1;
            }

            //该号段已经使用完，不能调号
            if (intEnd <= intUsedNO)
            {
                MessageBox.Show("该号段已经使用完，不能调号");
                return -1;
            }

            //Int64 intInputTemp = intInutno -1;

            //校验号在区间内

            if (!(intInutno >= intBegin && intInutno <= intEnd))
            {
                MessageBox.Show("输号应该在号段之间，请重新输入");
                return -1;
            }

            if (intInutno <= intUsedNO)
            {
                MessageBox.Show("输入号已经使用不能调号");
                return -1;
            }

            if (intInutno == intNextNO)
            {
                MessageBox.Show("输入号与下一号相同，无需调号");
                return -1;
            }

            return 1;
        }

        protected virtual int Save()
        {
            string inputNO = this.txtInput.Text;

            if (string.IsNullOrEmpty(inputNO))
            {
                MessageBox.Show("请输入发票号");
                return -1;
            }

            //inputNO = inputNO.PadLeft(12,'0');  
            //格式化发票
            this.txtInput.Text = this.txtInput.Text.PadLeft(10, '0');
            string prefixStr = string.Empty;
            this.GetPrefix(this.lblBeginNO.Text, this.lblEndNO.Text, ref prefixStr);
            int prefixLength = prefixStr.Length;//前缀长度
            int invoiceLength = this.lblBeginNO.Text.Length;//发票长度
            int suffixLength = invoiceLength - prefixLength;//后缀长度
            //发票=前缀+后缀
            this.txtInput.Text = prefixStr + this.txtInput.Text.Substring(this.txtInput.Text.Length - suffixLength, suffixLength);

            inputNO = this.txtInput.Text;

            int returnValue = this.ValidInputNO(inputNO);
            if (returnValue == -1)
            {
                this.txtInput.Focus();
                return -1;
            }

            FS.HISFC.Models.Fee.InvoiceJumpRecord invoiceJumpRecord = this.GetInvoiceChangeRecord();


            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            returnValue = this.feeIntegrate.InsertInvoiceJumpRecord(invoiceJumpRecord);
            if (returnValue < 0)
            {
                FS.FrameWork.Management.PublicTrans.Trans.Rollback();
                MessageBox.Show("设置出错" + this.feeIntegrate.Err);
                return -1;
            }
            //FS.FrameWork.Management.PublicTrans.Trans.Commit();
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("调号成功");
            this.SetValue(invoiceJumpRecord.Invoice);

            return 1;
        }

        /// <summary>
        /// 获取保存记录
        /// </summary>
        /// <returns></returns>
        protected virtual FS.HISFC.Models.Fee.InvoiceJumpRecord GetInvoiceChangeRecord()
        {
            string prefixStr = string.Empty;
            this.GetPrefix(this.lblBeginNO.Text, this.lblEndNO.Text, ref prefixStr);
            int prefixLength = prefixStr.Length;

            FS.HISFC.Models.Fee.InvoiceJumpRecord invoiceJumpRecord = new FS.HISFC.Models.Fee.InvoiceJumpRecord();

            invoiceJumpRecord.Invoice = this.Invoice;
            invoiceJumpRecord.OldUsedNO = this.Invoice.UsedNO;
            //invoiceJumpRecord.Invoice.UsedNO = (Int64.Parse(this.txtInput.Text) - 1).ToString().PadLeft(12, '0');
            //invoiceJumpRecord.Invoice.UsedNO = (Int64.Parse(this.txtInput.Text) - 1).ToString();
            invoiceJumpRecord.Invoice.UsedNO = prefixStr + (Int64.Parse(this.txtInput.Text.Substring(prefixLength)) - 1).ToString();

            //invoiceJumpRecord.NewUsedNO = (Int64.Parse(this.txtInput.Text) - 1).ToString().PadLeft(12, '0');
            //invoiceJumpRecord.NewUsedNO = (Int64.Parse(this.txtInput.Text) - 1).ToString();
            invoiceJumpRecord.NewUsedNO = prefixStr + (Int64.Parse(this.txtInput.Text.Substring(prefixLength)) - 1).ToString();
            invoiceJumpRecord.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;


            return invoiceJumpRecord;
        }

        /// <summary>
        /// 获得发票号段前缀
        /// </summary>
        /// <param name="bgn"></param>
        /// <param name="end"></param>
        /// <param name="prefix"></param>
        private void GetPrefix(string bgn, string end, ref string prefix)
        {
            prefix = string.Empty;//发票号相同的前缀
            int prefixLength = 0;
            for (int i = 0; i < bgn.Length; i++)
            {
                if (bgn[i] == end[i])
                {
                    prefix = prefix + bgn[i];
                }
            }
        }
        #endregion


        protected override void OnLoad(EventArgs e)
        {
            this.FindForm().Text = "发票调号";
            this.txtInput.Focus();
            base.OnLoad(e);
        }

        private void btOk_Click(object sender, EventArgs e)
        {

            int returnValue = this.Save();

            if (returnValue == -1)
            {
                return;
            }
            else
            {
                this.FindForm().DialogResult = DialogResult.OK;
                this.FindForm().Close();
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void neuTabControl1_Enter(object sender, EventArgs e)
        {
            this.txtInput.Focus();
        }

    }
}
