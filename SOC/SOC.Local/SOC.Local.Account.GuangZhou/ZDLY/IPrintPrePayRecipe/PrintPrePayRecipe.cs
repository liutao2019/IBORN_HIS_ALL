using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.Local.Account.GuangZhou.Zdly;

//69CD1094-E3EC-4eb8-931F-7D4628C16378
namespace FS.SOC.Local.Account.GuangZhou.ZDLY.IPrintPrePayRecipe
{
    public partial class PrintPrePayRecipe : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.HISFC.BizProcess.Interface.Account.IPrintPrePayRecipe
    {
        public PrintPrePayRecipe()
        {
            InitializeComponent();
        }

         /// <summary>
        /// 为打印UC赋值
        /// </summary>
        /// <param name="account">帐户实体</param>
        public void SetValue(FS.HISFC.Models.Account.PrePay prepay)
        {

            switch (prepay.ValidState)
            {
                case FS.HISFC.Models.Base.EnumValidState.Invalid:
                    lbBsnType.Text = "诊疗卡退款";
                    //label7.Text = "退款方式：";
                    break; 
                case FS.HISFC.Models.Base.EnumValidState.Ignore:
                    lbBsnType.Text = "结清余额";
                    //label7.Text = "退款方式：";
                    break;
            }

            lblca.Text = prepay.PayType.ID == "CA" ? "现金" : "银行卡";
            lblBeforeVan.Text = (prepay.FT.TotCost - prepay.FT.PrepayCost).ToString("F2");
            lbldt.Text = prepay.PrePayOper.OperTime.ToString();
            lblreno.Text = prepay.InvoiceNO;
            lblcost.Text = prepay.FT.PrepayCost.ToString("F2");
            lbltime.Text = System.DateTime.Now.ToString();
            lbltotcost.Text = prepay.FT.TotCost.ToString("F2");
            lbloperid.Text = prepay.PrePayOper.ID;
            lblMcardno.Text = Function.GetMCardNoByCardNo(prepay.Memo); ;

            if (prepay.IsHostory)
            {
                lblcd.Visible = true;
            }
            else
            {
                lblcd.Visible = false;
            }
        }
        /// <summary>
        /// 打印
        /// </summary>
        public  void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
          

            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintDocument.DefaultPageSettings.Landscape = false;

            FS.HISFC.Models.Base.PageSize obj = new FS.HISFC.Models.Base.PageSize();
            obj.Printer = "ATMSLP";
            obj.Name = "ATMSLP";
            obj.ID = "ATMSLP";
            obj.HeightMM = 103f;
            obj.WidthMM = 150f;

            obj.Top = 0;
            obj.Left = 0;

            print.SetPageSize(obj);


            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
            }

        }

        private void PrintPrePayRecipe_Load(object sender, EventArgs e)
        {

        }
    }
}
