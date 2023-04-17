using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Report
{
    public partial class ucCheckReportDetail : FS.SOC.Local.Pharmacy.Base.ucPrivePowerReport
    {
        public ucCheckReportDetail()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.Check.Detail";
            this.PriveClassTwos = "0305";
            this.MainTitle = "盘点明细查询";
            this.RightAdditionTitle = "";
            this.ShowTypeName = myDeptType.其他;
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20";
            this.SumColIndexs = "12,13,14,15,17,18";
        }

        private bool isLocalPrint = true;

        /// <summary>
        /// 是否启用本地化打印
        /// </summary>
        [Description("是否启用本地化打印"), Category("Print打印设置"), Browsable(true), DefaultValue(true)]
        public bool IsLocalPrint
        {
            get { return isLocalPrint; }
            set { isLocalPrint = value; }
        }
        
        //private void ucCheckReportDetail_Load(object sender, EventArgs e)
        //{
        //    this.SQLIndexs = "Pharmacy.NewReport.Check.Detail";
        //    this.PriveClassTwos = "0305";
        //    this.MainTitle = "盘点明细查询";
        //    this.RightAdditionTitle = "";
        //    this.ShowTypeName = myDeptType.其他;
        //    this.Init();
        //}

        public override int Print(object sender, object neuObject)
        {
            if (this.IsLocalPrint)
            {
                FS.SOC.HISFC.BizLogic.Pharmacy.Check checkMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Check();
                ArrayList alCheckStat = checkMgr.GetCheckStat(this.cmbDept.Tag.ToString(), this.dtStart.Value, this.dtEnd.Value);
                if (alCheckStat == null || alCheckStat.Count == 0)
                {
                    MessageBox.Show("没有要打印的信息");
                    return 0;
                }
                FS.SOC.Local.Pharmacy.ZhuHai.Print.BillPrintInterfaceImplement billPrint = new FS.SOC.Local.Pharmacy.ZhuHai.Print.BillPrintInterfaceImplement();
                billPrint.PrintBill("0306", "01", alCheckStat);
            }
            else
            {
                base.Print(sender, neuObject);
            }
            return 1;
        }
    }
}
