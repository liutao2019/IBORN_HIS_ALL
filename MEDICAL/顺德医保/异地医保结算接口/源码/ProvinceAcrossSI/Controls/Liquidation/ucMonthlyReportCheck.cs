using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace ProvinceAcrossSI.Controls.Liquidation
{
    public partial class ucMonthlyReportCheck : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMonthlyReportCheck()
        {
            InitializeComponent();

            this.fpSpread1.DoubleClick += new EventHandler(fpSpread1_DoubleClick);
        }



        ProvinceAcrossSI.Business.InPatient.InPatientService prService = new ProvinceAcrossSI.Business.InPatient.InPatientService();

        ProvinceAcrossSI.Business.Common.YDStatReport reportMgr = new ProvinceAcrossSI.Business.Common.YDStatReport();

        FS.HISFC.BizLogic.Manager.Person perMgr = new FS.HISFC.BizLogic.Manager.Person();

        FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();

        FS.FrameWork.Models.NeuObject commObj = null;

        int year = 0;
        int month = 0;
        string type = "";
        string errInfo = "";
        ArrayList alPatientInfo = null;

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("提交", "提交月度申报", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolBarService.AddToolButton("撤销", "撤销月度申报", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Y预览, true, false, null);
            
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "提交":
                    //this.SubmitApply();
                    break;
                case "撤销":
                    //this.CancelApply();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        //月度结算申请提交情况查询
        protected override int OnQuery(object sender, object neuObject)
        {
            year = this.dtMonth.Value.Date.Year;
            month = this.dtMonth.Value.Month;
            type = this.cmbCustomType.Tag.ToString();

            DateTime dtBegin = new DateTime(year, month, 1,0,0,0);
            DateTime dtEnd = dtBegin.AddMonths(1).AddSeconds(-1);

            alPatientInfo = reportMgr.QueryYDLiquidation(dtBegin, dtEnd,type);

            this.SetValueToFp(alPatientInfo);

            return 1;
        }

        private void SetValueToFp(ArrayList al)
        {
            this.fpTotal.RowCount = 0;

            int rowcount = 0;
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in al)
            {
                rowcount = this.fpTotal.RowCount;
                this.fpTotal.Rows.Add(rowcount,1);
                this.fpTotal.Cells[rowcount, 0].Text = patient.SSN;
                this.fpTotal.Cells[rowcount, 1].Text = patient.IDCard;
                this.fpTotal.Cells[rowcount, 2].Text = patient.Name;
                this.fpTotal.Cells[rowcount, 3].Text = patient.PID.HealthNO;
                this.fpTotal.Cells[rowcount, 4].Text = patient.SSN;

                this.fpTotal.Cells[rowcount, 5].Text = patient.SIMainInfo.RegNo;
                this.fpTotal.Cells[rowcount, 6].Text = patient.PVisit.InTime.ToString();
                this.fpTotal.Cells[rowcount, 7].Text = patient.PVisit.OutTime.ToString();
                this.fpTotal.Cells[rowcount, 8].Text = patient.BalanceDate.ToString();
                this.fpTotal.Cells[rowcount, 9].Text = patient.ClinicDiagnose;
                this.fpTotal.Cells[rowcount, 10].Text = patient.FT.TotCost.ToString();
                this.fpTotal.Cells[rowcount, 11].Text = patient.FT.PayCost.ToString();
                this.fpTotal.Cells[rowcount, 12].Text = patient.FT.OwnCost.ToString();
                this.fpTotal.Cells[rowcount, 13].Text = patient.SIMainInfo.ApplyType.Name;
                this.fpTotal.Cells[rowcount, 14].Text = patient.SIMainInfo.EmplType;
                this.fpTotal.Cells[rowcount, 15].Text = patient.PID.CardNO;

                this.fpTotal.Rows[rowcount].Tag = patient;
            }

        }

        private void QueryCheckInfo()
        {
            if (alPatientInfo != null)
            {
                
            }
        }

        void fpSpread1_DoubleClick(object sender, EventArgs e)
        {
            int rowIndex = this.fpTotal.ActiveRowIndex;

            FS.HISFC.Models.RADT.PatientInfo patientInfo = this.fpTotal.Rows[rowIndex].Tag as FS.HISFC.Models.RADT.PatientInfo;

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

            //prService.ExtractAuditInstructions(patientInfo,dtNow,ref obj);

        }
    }
}
