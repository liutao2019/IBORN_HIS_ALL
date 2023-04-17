using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Account.Controls
{
    public partial class ucPrePayPrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        public ucPrePayPrint()
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

        #region IAccountPrint 成员

        /// <summary>
        /// 账户业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
        
        public int Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //print.IsLandScape = true;
            //print.PrintDocument.DefaultPageSettings.Landscape = true;
            //{3E7EFECA-5375-420b-A435-323463A0E56C}
            string printerName = FS.HISFC.Components.Common.Classes.Function.ChoosePrinter();
            if (string.IsNullOrEmpty(printerName))
            {
                return 1;
            }
            print.PrintDocument.PrinterSettings.PrinterName = printerName;
            if (FS.FrameWork.WinForms.Classes.Function.IsManager())
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
                //print.PrintPreview(5, 5, this);
            }
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public int PrintSetValue(FS.HISFC.Models.Account.AccountDetail accountDetail)
        {
            //{443B5198-4F31-48fe-9B99-679AEA5DD3E1}
            FS.HISFC.Models.Base.Department currDept = new FS.HISFC.Models.Base.Department();
            currDept = (FS.HISFC.Models.Base.Department)(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept);
            if (!string.IsNullOrEmpty(currDept.HospitalName))
            {
                this.lbHospitalName.Text = currDept.HospitalName + "充值/取现票据";
            }
            else
            {
                this.lbHospitalName.Text = "爱博恩妇产医院充值/取现票据";
            }

            this.lblInvoiceNo.Text = accountDetail.ID;//發票號
            //this.lblPrintTime.Text = accountManager.GetDateTimeFromSysDateTime().ToString();//系统时间
            this.lblPrintTime.Text = accountDetail.OperEnvironment.OperTime.ToString();
            this.lblCardNo.Text = accountDetail.CardNO;
            this.lblName.Text = accountDetail.Name;
            this.lblMoney.Text = accountDetail.BaseVacancy.ToString();//借用字段
            this.lblDonateCost.Text = accountDetail.DonateVacancy.ToString();//借用字段
            this.lblCouponAmout.Text = accountDetail.CouponAccumulate.ToString();//借用字段
            this.lblDonateAmout.Text = accountDetail.DonateAccumulate.ToString();//借用字段
            this.lblBaseAmout.Text = accountDetail.BaseAccumulate.ToString();//借用字段
            this.txtAccountType.Text = accountDetail.AccountType.Name;
            if (accountDetail.OperEnvironment.Memo == "0")
            {
                this.lbOperType.Text = "返还";
            }
            else if (accountDetail.OperEnvironment.Memo == "1")
            {
                this.lbOperType.Text = "收取";
            }
            else if (accountDetail.OperEnvironment.Memo == "2")
            {
                this.lbOperType.Text = "结清余额";
            }
            else
            {
                this.lbOperType.Text = "";
            }
            return 0;
        }

        public int PrintSetValue1(FS.HISFC.Models.Account.Account account)
        {
            HISFC.Models.Account.AccountRecord tempaccountRecord = new FS.HISFC.Models.Account.AccountRecord();
            tempaccountRecord = account.AccountRecord[0] as FS.HISFC.Models.Account.AccountRecord;
            this.lblInvoiceNo.Text = account.ID;//發票號
            //this.lblPrintTime.Text = accountManager.GetDateTimeFromSysDateTime().ToString();//系统时间
            this.lblPrintTime.Text = account.User01;
            this.lblCardNo.Text = account.CardNO;
            this.lblName.Text = account.Name;
            this.lblMoney.Text = tempaccountRecord.BaseCost.ToString();
            this.lblDonateCost.Text = tempaccountRecord.DonateCost.ToString();
            this.lblCouponAmout.Text = account.CouponVacancy.ToString();
            this.lblDonateAmout.Text = account.DonateVacancy.ToString();
            this.lblBaseAmout.Text = account.BaseVacancy.ToString();

            return 0;
        }
        #endregion

     }
}
