using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Account;
using FS.HISFC.BizProcess.Interface.Account;

namespace FS.HISFC.Components.Account.Controls
{
    /// <summary>
    /// 卡费用评证补打
    /// </summary>
    public partial class ucCardCertificateReprint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        FS.HISFC.Models.Account.AccountCard accountCard = null;
        /// <summary>
        /// 费用业务层
        /// </summary>
        private HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// 门诊帐户业务层
        /// </summary>
        private HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 综合业务层
        /// </summary>
        private HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        public ucCardCertificateReprint()
        {
            InitializeComponent();
        }

        private void txtMarkNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                int iRes = GetPatientByMarkNO();

                if(iRes <= 0)
                    return;
                
                QueryOperRecord();
            }
        }

        /// <summary>
        /// 根据就诊卡号查找患者信息
        /// </summary>
        private int GetPatientByMarkNO()
        {
            string markNO = this.txtMarkNO.Text.Trim();
            accountCard = new FS.HISFC.Models.Account.AccountCard();
            int resultValue = feeIntegrate.ValidMarkNO(markNO, ref accountCard);
            if (resultValue <= 0)
            {
                MessageBox.Show(feeIntegrate.Err);
                this.txtMarkNO.Focus();
                this.txtMarkNO.SelectAll();
                return -1;
            }
            this.txtMarkNO.Tag = this.accountCard.Patient.PID.CardNO;
            this.txtMarkNO.Text = this.accountCard.MarkNO;

            return 1;
        }

        /// <summary>
        /// 根据就诊卡号查找交易记录
        /// </summary>
        /// <param name="cardNO"></param>
        private void QueryOperRecord()
        {
            if (this.txtMarkNO.Tag == null)
            {
                MessageBox.Show("请输入就诊卡号！");
                this.txtMarkNO.Focus();
                return;
            }
            
            string cardNO = this.txtMarkNO.Tag.ToString();
            int count = this.neuSpread1_Sheet1.Rows.Count;
            if (count > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, count);
            }

            List<AccountCardFee> lstCardFee = null;
            int iRes = accountManager.QueryAccountCardFee(cardNO, out lstCardFee);
            if (iRes == -1 || lstCardFee == null)
            {
                MessageBox.Show("查找数据失败！");
                return;
            }

            int rowIdx = 0;
            foreach (AccountCardFee cardFee in lstCardFee)
            {
                cardFee.Patient = accountCard.Patient;
                neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);
                rowIdx = this.neuSpread1_Sheet1.Rows.Count - 1;

                this.neuSpread1_Sheet1.Rows[rowIdx].Tag = cardFee;
                this.neuSpread1_Sheet1.Cells[rowIdx, 0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.neuSpread1_Sheet1.Cells[rowIdx, 0].Text = cardFee.InvoiceNo;
                this.neuSpread1_Sheet1.Cells[rowIdx, 1].Text = cardFee.TransType == FS.HISFC.Models.Base.TransTypes.Positive ? "正交易" : "负交易";
                this.neuSpread1_Sheet1.Cells[rowIdx, 2].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.neuSpread1_Sheet1.Cells[rowIdx, 2].Text = cardFee.MarkNO;
                this.neuSpread1_Sheet1.Cells[rowIdx, 3].Text = cardFee.MarkType.ID;
                this.neuSpread1_Sheet1.Cells[rowIdx, 4].Text = cardFee.Tot_cost.ToString();
                this.neuSpread1_Sheet1.Cells[rowIdx, 5].Text = cardFee.Oper.Name;
                this.neuSpread1_Sheet1.Cells[rowIdx, 6].Text = cardFee.Oper.OperTime.ToString("yyyy-MM-dd HH:mm:ss");
            }

        }

        protected override int OnPrint(object sender, object neuObject)
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0) return -1;
            int rowIndex = this.neuSpread1_Sheet1.ActiveRowIndex;

            AccountCardFee cardFee = this.neuSpread1_Sheet1.Rows[rowIndex].Tag as AccountCardFee;
            if (cardFee == null) return -1;

            if (cardFee.TransType == FS.HISFC.Models.Base.TransTypes.Positive)
            {
                PrintCardFee(cardFee);
            }
            else
            {
                PrintReturnCardFee(cardFee);
            }

            return 1;
        }

        /// <summary>
        /// 打印卡费用凭证
        /// </summary>
        /// <param name="cardFee"></param>
        private void PrintReturnCardFee(AccountCardFee cardFee)
        {
            IPrintReturnCardFee iPrintReturn = FS.FrameWork.WinForms.Classes.
              UtilInterface.CreateObject(this.GetType(), typeof(IPrintReturnCardFee)) as IPrintReturnCardFee;
            if (iPrintReturn == null)
                return;

            cardFee.User01 = "补打";

            iPrintReturn.SetValue(cardFee);
            iPrintReturn.Print();
        }

        /// <summary>
        /// 打印卡费用凭证
        /// </summary>
        /// <param name="cardFee"></param>
        private void PrintCardFee(AccountCardFee cardFee)
        {
            IPrintCardFee iPrint = FS.FrameWork.WinForms.Classes.
              UtilInterface.CreateObject(this.GetType(), typeof(IPrintCardFee)) as IPrintCardFee;
            if (iPrint == null)
                return;

            cardFee.User01 = "补打";

            iPrint.SetValue(cardFee);
            iPrint.Print();
        }

    }
}
