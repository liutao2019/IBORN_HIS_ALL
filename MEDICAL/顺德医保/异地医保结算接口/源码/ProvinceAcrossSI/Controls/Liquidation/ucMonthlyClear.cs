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
    public partial class ucMonthlyClear : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMonthlyClear()
        {
            InitializeComponent();
        }

        //FS.HISFC.BizLogic.Fee.YDStatReport ydReport = new FS.HISFC.BizLogic.Fee.YDStatReport();

        ProvinceAcrossSI.Business.InPatient.InPatientService prService = new ProvinceAcrossSI.Business.InPatient.InPatientService();

        FS.FrameWork.Models.NeuObject obj = null;

        ProvinceAcrossSI.ProvinceAcrossSIDatabase SIMgr = new ProvinceAcrossSIDatabase();

        private string settingFilePatch = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @"\Profile\KS\清分.xml";

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("清分确认", "清分确认", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolBarService.AddToolButton("清分确认取消", "清分确认取消", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Y预览, true, false, null);

            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "清分确认":
                    this.ApplyResult();
                    break;
                case "清分确认取消":
                    this.CancelApply();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override void OnLoad(EventArgs e)
        {
            //if (System.IO.File.Exists(this.settingFilePatch))
            //{
            //    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpResult, this.settingFilePatch);
            //}
            this.fpResult.RowCount = 0;

            base.OnLoad(e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.fpResult.RowCount = 0;

            obj = new FS.FrameWork.Models.NeuObject();

            obj.ID = this.dtMonth.Value.ToString();

            ArrayList al = prService.MonthlyReportClearDetail(obj);

            this.SetValueToFp(al);

            return base.OnQuery(sender, neuObject);
        }

        private void SetValueToFp(ArrayList al)
        {
            int rowCount = 0;
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in al)
            {
                rowCount = this.fpResult.RowCount;
                this.fpResult.Rows.Add(rowCount, 1);

                this.fpResult.Cells[rowCount, 0].Value = false; 
                this.fpResult.Cells[rowCount, 1].Text = patient.Memo;// node.SelectSingleNode("seqno").InnerText.Trim();
                this.fpResult.Cells[rowCount, 2].Text = patient.SSN;// node.SelectSingleNode("aac002").InnerText.Trim();
                this.fpResult.Cells[rowCount, 3].Text = patient.PID.HealthNO;// node.SelectSingleNode("ykc700").InnerText.Trim();
                this.fpResult.Cells[rowCount, 4].Text = patient.BalanceDate.ToString();// FS.FrameWork.Function.NConvert.ToDateTime(node.SelectSingleNode("akc194").InnerText.Trim());
                this.fpResult.Cells[rowCount, 5].Text = patient.SIMainInfo.RegNo;// node.SelectSingleNode("aaz216").InnerText.Trim();
                this.fpResult.Cells[rowCount, 6].Text = patient.VipFlag.ToString();// FS.FrameWork.Function.NConvert.ToBoolean(node.SelectSingleNode("ake105").InnerText.Trim());
                this.fpResult.Cells[rowCount, 7].Text = patient.FT.TotCost.ToString();// FS.FrameWork.Function.NConvert.ToDecimal(node.SelectSingleNode("akc264").InnerText.Trim());
                this.fpResult.Cells[rowCount, 8].Text = patient.FT.PubCost.ToString();// FS.FrameWork.Function.NConvert.ToDecimal(node.SelectSingleNode("ake149").InnerText.Trim());

                FS.HISFC.Models.RADT.PatientInfo patientTemp = this.SIMgr.GetPatientInfoByJZDJH( patient.PID.HealthNO,patient.SSN);
                if (patientTemp != null)
                {
                    this.fpResult.Cells[rowCount, 9].Text = patientTemp.Name;
                    this.fpResult.Cells[rowCount, 10].Text = patientTemp.Pact.Name;
                }
                else
                {
                    this.fpResult.Cells[rowCount, 9].Text = "";
                    this.fpResult.Cells[rowCount, 10].Text = "";
                }
                this.fpResult.Rows[rowCount].Tag = patient;
            }
        }

        private void ApplyResult()
        {
            obj = new FS.FrameWork.Models.NeuObject();

            obj.ID = this.dtMonth.Value.Date.ToString();

            ArrayList al = new ArrayList();
            FS.HISFC.Models.RADT.PatientInfo patient = null;
            for (int rowIndex = 0; rowIndex < this.fpResult.RowCount ; rowIndex++)
            {
                patient =  this.fpResult.Rows[rowIndex].Tag as FS.HISFC.Models.RADT.PatientInfo;

                //if (this.fpResult.Cells[rowIndex, 0].Value == "True")
                //{
                //    patient.PID.Memo = "1";
                //}
                //else
                //{
                //    patient.PID.Memo = "0";
                //}

                patient.PID.Memo = "1";

                al.Add(patient);
            }

            int result = prService.MonthlyReportClearResult(obj, al);
            if (result > 0)
            {
                MessageBox.Show("清分确认成功!");
            }
            else
            {
                MessageBox.Show("清分确认失败!");
            }
        }

        private void CancelApply()
        {
            DialogResult result = MessageBox.Show("是否取消当月清分操作？", "确认", MessageBoxButtons.OKCancel);

            if (result == DialogResult.Cancel)
            {
                return;
            }

            obj = new FS.FrameWork.Models.NeuObject();

            obj.ID = this.dtMonth.Value.ToString();

            int rtn  = prService.MonthlyReportClearRollBack(obj);

            if (rtn >= 0)
            {
                MessageBox.Show("取消清分成功！");
            }
            else
            {
                MessageBox.Show("取消清分失败！");
            }
        }

        private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column != 0)
            {
                return;
            }

            int rowIndex = e.Row;

            if (this.fpResult.Cells[e.Row, 0].Text == "True")
            {
                string regNo = this.fpResult.Cells[e.Row, 3].Text.Trim();//就诊登记号
                string busNo = this.fpResult.Cells[e.Row, 5].Text.Trim();//结算序号

                decimal totCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpResult.Cells[e.Row, 7].Text.Trim());//就诊登记号
                decimal pubCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpResult.Cells[e.Row, 8].Text.Trim());//结算序号

                //FS.FrameWork.Models.NeuObject obj = ydReport.QueryPatientBalanceInfoByRegNOAndBusNo(regNo, busNo);

                //if (FS.FrameWork.Function.NConvert.ToDecimal(obj.ID) != totCost || FS.FrameWork.Function.NConvert.ToDecimal(obj.Name) != pubCost)
                //{
                //    MessageBox.Show(string.Format("费用核对错误，请检查！系统总金额：{0} 社保记账金额：{1}", obj.ID, obj.Name));
                //    return;
                //}
            }
        }
    }
}
