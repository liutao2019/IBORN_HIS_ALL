using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Account.Controls.IBorn
{
    public partial class ucPrePayPrintIBorn : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        public ucPrePayPrintIBorn()
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
            FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("CZSJ", 787, 400);
            print.SetPageSize(ps);
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
        /// 赋值
        /// </summary>
        /// <param name="accountDetail"></param>
        /// <returns></returns>
        public int PrintSetValue(FS.HISFC.Models.Account.AccountDetail accountDetail)
        {
            // {90EE4859-CD33-413c-84B9-A1B3A7C16332}
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
            //this.lbHospitalName.Text = FS.FrameWork.Management.Connection.Hospital.Name + "充值/取现票据";
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
            this.lblPayType.Text = accountDetail.User03;// {AD405AA0-3101-46c0-A0B6-846DC3AAB10A}
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

            //{8AA716BA-0EBA-41ea-BC36-8BA310B5FCA0}
            this.lblFeeOper.Text = FS.FrameWork.Management.Connection.Operator.ID;

            return 0;
        }

        #endregion

     }
}
