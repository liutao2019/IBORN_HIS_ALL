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
namespace FS.SOC.Local.Account.ZhuHai.ZDWY.IPrintCancelVacancy
{
    public partial class PrintCancelVacancy : UserControl, FS.HISFC.BizProcess.Interface.Account.IPrintCancelVacancy
    {
        public PrintCancelVacancy()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 为打印UC赋值
        /// </summary>
        /// <param name="account">帐户实体</param>
        public void SetValue(FS.HISFC.Models.Account.AccountRecord accountRecord)
        {
            lbldt.Text = accountRecord.OperTime.ToString();
            lblreno.Text = accountRecord.ReMark;
            lblcost.Text = accountRecord.BaseCost.ToString("F2");
            lblresult.Text = "成功";
            lbltime.Text = System.DateTime.Now.ToString();
            lbltotcost.Text = accountRecord.BaseVacancy.ToString("F2");
            lbloperid.Text = accountRecord.Oper.ID.ToString();
            lblMcardno.Text = Function.GetMCardNoByCardNo(accountRecord.Patient.PID.CardNO);
            if (accountRecord.Memo == "1")
            {
                this.lblcd.Visible = true;
            }
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
