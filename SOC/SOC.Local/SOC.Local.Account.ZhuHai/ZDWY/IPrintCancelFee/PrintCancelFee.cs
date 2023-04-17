using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.Local.Account.ZhuHai.ZDWY;

//69CD1094-E3EC-4eb8-931F-7D4628C16378
namespace FS.SOC.Local.Account.ZhuHai.ZDWY.IPrintCancelFee
{
    public partial class PrintCancelFee : UserControl, FS.HISFC.BizProcess.Interface.Fee.IPrintCancleFee
    {
        public PrintCancelFee()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 为打印UC赋值
        /// </summary>
        /// <param name="account">帐户实体</param>
        public void SetValue(FS.HISFC.Models.Account.AccountRecord CancelRecord, FS.HISFC.Models.Account.AccountRecord FeeRecord)
        {
            if (FeeRecord != null)
            {
                lblBeforeVan.Text = Math.Abs(FeeRecord.BaseVacancy - Math.Abs(Math.Abs(FeeRecord.BaseCost) - Math.Abs(CancelRecord.BaseCost))).ToString("F2");
                lbldt.Text = FeeRecord.OperTime.ToString();
                lblcost.Text = Math.Abs(Math.Abs(FeeRecord.BaseCost) - Math.Abs(CancelRecord.BaseCost)).ToString("F2");
                lbltotcost.Text = FeeRecord.BaseVacancy.ToString("F2");
                lblreno.Text = FeeRecord.ReMark;
            }
            else
            {
                lblBeforeVan.Text = Math.Abs(CancelRecord.BaseVacancy - Math.Abs(CancelRecord.BaseCost)).ToString("F2");
                lbldt.Text = CancelRecord.OperTime.ToString();
                lblcost.Text = Math.Abs(CancelRecord.BaseCost).ToString("F2");
                lbltotcost.Text = CancelRecord.BaseVacancy.ToString("F2");
                lblreno.Text = CancelRecord.ReMark;
            }

            lblMcardno.Text = Function.GetMCardNoByCardNo(CancelRecord.Patient.PID.CardNO);
            lblresult.Text = "成功";
            lbltime.Text = System.DateTime.Now.ToString();
            lbloperid.Text = CancelRecord.Oper.ID.ToString();
        }
        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
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
    }
}
