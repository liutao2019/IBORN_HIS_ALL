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
    public partial class ucMonthlyReportQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMonthlyReportQuery()
        {
            InitializeComponent();
            this.fpSpread1.DoubleClick += new EventHandler(fpSpread1_DoubleClick);
        }

        ProvinceAcrossSI.Business.InPatient.InPatientService prService = new ProvinceAcrossSI.Business.InPatient.InPatientService();

        FS.FrameWork.Models.NeuObject obj = null;

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("申报结果", "提取月度结算申报处理结果", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolBarService.AddToolButton("申请提交情况", "申请提交情况说明", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Y预览, true, false, null);

            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "申报结果":
                    this.ApplyResultQuery();
                    break;
                case "申请提交情况":
                    this.ApplyCheckQuery();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        //月度结算申请提交情况查询
        protected override int OnQuery(object sender, object neuObject)
        {
            return base.OnQuery(sender, neuObject);
        }

        private void ApplyResultQuery()
        {
            obj = new FS.FrameWork.Models.NeuObject();

            obj.ID = this.dtMonth.Value.Date.ToString();

            ArrayList alResult =  prService.ExtractMonthlySettlementResults(obj);

            this.SetValueToFp(alResult);
        }

        private void SetValueToFp(ArrayList al)
        {
            int rowCount = 0;

            this.fpResult.RowCount = 0;

            foreach (FS.HISFC.Models.RADT.PatientInfo patient in al)
            {
                rowCount = this.fpResult.RowCount;
                this.fpResult.Rows.Add(rowCount, 1);
                
                this.fpResult.Cells[rowCount,0].Text = patient.FT.Memo ;// node.SelectSingleNode("seqno").InnerText;
                this.fpResult.Cells[rowCount, 1].Text = patient.Name;// node.SelectSingleNode("aac003").InnerText;
                this.fpResult.Cells[rowCount, 2].Text = patient.SSN;// node.SelectSingleNode("aac002").InnerText;
                this.fpResult.Cells[rowCount, 3].Text = patient.IDCardType.Name;// node.SelectSingleNode("aac044").InnerText;                
                this.fpResult.Cells[rowCount, 4].Text = patient.PID.Memo;// node.SelectSingleNode("aab301").InnerText;
                this.fpResult.Cells[rowCount, 5].Text = patient.PID.HealthNO;// node.SelectSingleNode("ykc700").InnerText;
                this.fpResult.Cells[rowCount, 6].Text = patient.PID.CaseNO;// node.SelectSingleNode("ykc618").InnerText;
                this.fpResult.Cells[rowCount, 7].Text = patient.FT.TotCost.ToString();// FS.FrameWork.Function.NConvert.ToDecimal(node.SelectSingleNode("akc264").InnerText);
                this.fpResult.Cells[rowCount, 8].Text = patient.FT.PubCost.ToString();// FS.FrameWork.Function.NConvert.ToDecimal(node.SelectSingleNode("akb068").InnerText);
                this.fpResult.Cells[rowCount, 9].Text = patient.Memo;// node.SelectSingleNode("yzz022").InnerText;
                this.fpResult.Cells[rowCount, 10].Text = patient.FT.OvertopCost.ToString();// FS.FrameWork.Function.NConvert.ToDecimal(node.SelectSingleNode("akc265").InnerText);
                this.fpResult.Cells[rowCount, 11].Text = patient.FT.OwnCost.ToString();// FS.FrameWork.Function.NConvert.ToDecimal(node.SelectSingleNode("akb081").InnerText);
                this.fpResult.Cells[rowCount, 12].Text = patient.FT.PayCost.ToString();// FS.FrameWork.Function.NConvert.ToDecimal(node.SelectSingleNode("yzz139").InnerText);
                this.fpResult.Cells[rowCount, 13].Text = patient.FT.Memo;// node.SelectSingleNode("yzz062").InnerText;
            }

        }

        void fpSpread1_DoubleClick(object sender, EventArgs e)
        {
            this.ApplyCheckQuery();
        }

        private void ApplyCheckQuery()
        {
            int sheetIndex = this.fpSpread1.ActiveSheetIndex;

            if (sheetIndex != 0)
            {
                return;
            }

            obj = new FS.FrameWork.Models.NeuObject();

            obj.ID = this.dtMonth.Value.Date.ToString();

            FS.FrameWork.Models.NeuObject objt = new FS.FrameWork.Models.NeuObject();

            int rowIndex = this.fpSpread1.ActiveSheet.ActiveRowIndex;

            ProvinceAcrossSI.Objects.SIPersonInfo patient = this.fpSpread1.ActiveSheet.Rows[rowIndex].Tag as ProvinceAcrossSI.Objects.SIPersonInfo;

            ArrayList alResult = prService.ExtractAuditInstructions(obj, patient, ref objt);

            this.SetSubInfoValueToFp(alResult);
        }

        private void SetSubInfoValueToFp(ArrayList al)
        {
            int rowCount = 0;

            this.fpCheck.RowCount = 0;
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in al)
            {
                rowCount = this.fpCheck.RowCount;
                this.fpCheck.Rows.Add(rowCount, 1);

                this.fpCheck.Cells[rowCount,0].Text =   patient.FT.ID;// node.SelectSingleNode("seqno").InnerText.Trim();
                this.fpCheck.Cells[rowCount, 1].Text = patient.Memo;// node.SelectSingleNode("akc220").InnerText.Trim();
                this.fpCheck.Cells[rowCount, 2].Text = patient.Patient.Memo;// node.SelectSingleNode("ykc610").InnerText.Trim();
                this.fpCheck.Cells[rowCount, 3].Text = patient.Pact.Name;// node.SelectSingleNode("aka075").InnerText.Trim();
                this.fpCheck.Cells[rowCount, 4].Text = patient.Pact.Memo;// node.SelectSingleNode("akc269").InnerText.Trim();
                this.fpCheck.Cells[rowCount, 5].Text = patient.FT.TotCost.ToString();// FS.FrameWork.Function.NConvert.ToDecimal(node.SelectSingleNode("yzz128").InnerText.Trim());
                this.fpCheck.Cells[rowCount, 6].Text = patient.FT.Memo;// node.SelectSingleNode("yzz092").InnerText.Trim();
                this.fpCheck.Cells[rowCount, 7].Text = patient.FT.OwnCost.ToString();// FS.FrameWork.Function.NConvert.ToDecimal(node.SelectSingleNode("yzz029").InnerText.Trim());
            }
        }
    }
}
