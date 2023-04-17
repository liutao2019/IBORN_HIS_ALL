using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Account.Controls
{
    public partial class ucPrintAccountPrePay : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.HISFC.BizProcess.Interface.Account.IAccountPrint
    {

        public ucPrintAccountPrePay()
        {
            InitializeComponent();
        }
        private FS.HISFC.Models.Account.Account account = null;
        public FS.HISFC.Models.Account.Account Account
        {
            get
            {
                return account;
            }
            set
            {
                account = value;
            }
        }

        #region IAccountPrint ≥…‘±

        public int Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPage(0, 0, this);
            return 0;
        }

        public int PrintSetValue(FS.HISFC.Models.Account.Account account)
        {
            this.lblCardNo.Text = account.AccountRecord[0].Patient.PID.CardNO;
            this.lbldate.Text = account.AccountRecord[0].OperTime.ToString();
            this.lblInvoiceNo.Text = account.AccountRecord[0].ReMark;
            this.lblMoney.Text = account.AccountRecord[0].BaseCost.ToString();
            this.lblMoneyUpper.Text = FS.FrameWork.Function.NConvert.ToCapital(account.AccountRecord[0].BaseCost);
            //if(account.Patient!=null)
            //    this.lblName.Text = account.Patient.DecryptName;
            this.lblOper.Text = account.AccountRecord[0].Oper.Name;
            this.lblInvoiceNo.Text = account.AccountRecord[0].ReMark;
            this.lblCardNo.Text = account.AccountRecord[0].Patient.PID.CardNO;
            this.lblPrePayMoney.Text = account.AccountRecord[0].BaseCost.ToString();
            return 1;
        }

        #endregion
    }
}
