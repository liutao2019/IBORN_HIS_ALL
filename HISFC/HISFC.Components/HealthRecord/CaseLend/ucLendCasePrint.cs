using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.CaseLend
{
    /// <summary>
    /// 借阅单打印
    /// </summary>
    public partial class ucLendCasePrint : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ucLendCasePrint()
        {
            InitializeComponent();
        }
        FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
        private List<FS.HISFC.Models.HealthRecord.Lend> lendCaseList = null;

        /// <summary>
        /// 借阅数组
        /// </summary>
        public List<FS.HISFC.Models.HealthRecord.Lend> LeadCaseList
        {
            get { return this.lendCaseList; }
            set
            {
                this.lendCaseList = value;
                this.SetInfo();
            }
        }

        private void SetInfo()
        {
            FS.HISFC.Models.HealthRecord.Lend leadObj = this.lendCaseList[0] as FS.HISFC.Models.HealthRecord.Lend;
            if (lendCaseList == null)
            {
                return;
            }
            this.lblLendTot.Text = lendCaseList.Count.ToString();
            for (int i = 0; i < this.lendCaseList.Count; i++)
            {
                FS.HISFC.Models.HealthRecord.Lend lendObj = this.lendCaseList[i] as FS.HISFC.Models.HealthRecord.Lend;
                if (i == 0)
                {
                    this.lblLendNo.Text = leadObj.CardNO;
                    this.lblLendType.Text = this.conMgr.GetConstant("CASE_LEND_TYPE", lendObj.LendKind.ToString()).Name;
                    if (lendObj.LendStus == "1")
                    {
                        this.lblLendState.Text = "借出";
                    }
                    else
                    {
                        this.lblLendState.Text = "归还";
                    }
                    this.lblOper.Text = lendObj.OperInfo.ID;
                    this.lblPerson.Text = lendObj.EmployeeInfo.Name;
                    this.lblPersonDept.Text = lendObj.EmployeeDept.Name;
                    this.lblLendDate.Text = lendObj.LendDate.ToShortDateString();
                    this.lblCaseNo1.Text = "";
                }
                if (i < this.lendCaseList.Count - 1)
                {
                    this.lblCaseNo1.Text += lendObj.CaseBase.CaseNO + "，";
                }
                else
                {
                    this.lblCaseNo1.Text += lendObj.CaseBase.CaseNO;
                }
            }
        }

        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            //FS.HISFC.Models.Base.PageSize page = new FS.HISFC.Models.Base.PageSize();
            //page.Height = 400;
            //page.Width = 900;
            //p.SetPageSize(page);
            p.PrintPage(5, 10, this);
        }
    }
}
