using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.CaseHistory
{
    public partial class ucCaseCallbackPercent : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCaseCallbackPercent()
        {
            InitializeComponent();
            this.neuBtnQuery.Click +=new EventHandler(neuBtnQuery_Click);
        }

        FS.HISFC.BizLogic.HealthRecord.CaseHistory.CallBack callbackMgr = new FS.HISFC.BizLogic.HealthRecord.CaseHistory.CallBack();
        ArrayList al = null;

        protected override void OnLoad(EventArgs e)
        {
            this.InitFp();
            base.OnLoad(e);
        }

        /// <summary>
        /// 初始化Fp
        /// </summary>
        private void InitFp()
        {
            for (int i = 1; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                this.neuSpread1_Sheet1.Columns[i].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            }

            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = Color.White;

            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].ColumnSpan = 7;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].RowSpan = 2;

            this.neuSpread1_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None, Color.White, Color.White, Color.White, 0);
            this.neuSpread1_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None, Color.White, Color.White, Color.White, 0);

            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //this.neuSpread1_Sheet1.ColumnHeader.Rows[1].HorizontalAlignment

            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Font = new Font("宋体", 12F, FontStyle.Bold);
            //this.neuSpread1_Sheet1.ColumnHeader.Rows[1].Font = new Font("宋体", 9F, FontStyle.Bold);

            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Text = "病案回收率";

            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Rows[0].Font = new Font("宋体", 9F, FontStyle.Bold);

            this.neuSpread1_Sheet1.Cells[0, 0].Text = "科室";
            this.neuSpread1_Sheet1.Cells[0, 1].Text = "总病历数";
            this.neuSpread1_Sheet1.Cells[0, 2].Text = "回收率";//"7天回收率";
            this.neuSpread1_Sheet1.Cells[0, 3].Text = "超期病历数及罚款金额";//"超7天病历数及罚款金额";
            this.neuSpread1_Sheet1.Cells[0, 4].Text = "10天回收率";
            this.neuSpread1_Sheet1.Cells[0, 5].Text = "超10天病历数及罚款金额";
            this.neuSpread1_Sheet1.Columns[4].Visible = false;
            this.neuSpread1_Sheet1.Columns[5].Visible = false; 

            this.neuSpread1_Sheet1.Cells[0, 6].Text = "总罚款金额";
        }

        /// <summary>
        /// 更改超时且未回收病历状态
        /// </summary>
        public void UpdateOutTimeCase()
        {
            //if (this.callbackMgr.UpdateOutTimeCaseFlagField() == -1)
            //{
            //    MessageBox.Show(this.callbackMgr.Err);
            //}
        }

        /// <summary>
        /// 查询病历信息
        /// </summary>
        public void QueryInfoToFP()
        {
            //al.Clear();
            this.neuSpread1_Sheet1.RowCount = 1;
            al = this.callbackMgr.GetCaseCallbackPercent(this.neuDateTimePicker1.Value, this.neuDateTimePicker2.Value);
            if (al == null || al.Count == 0)
            {
                return;
            }

            foreach (FS.HISFC.Models.HealthRecord.CaseHistory.CallBack cb in al)
            {
                //if (cb.PatientNO == "0") continue;
                if (cb.Patient.Name == "0") continue;
                this.neuSpread1_Sheet1.RowCount++;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = cb.Patient.ID;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = cb.Patient.Name;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = cb.Patient.Memo;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = cb.Patient.UserCode;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = cb.Patient.WBCode;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = cb.Patient.SpellCode;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = cb.Patient.PID.PatientNO;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].Text = cb.Patient.PVisit.PatientLocation.Dept.ID;
            }
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            al.Clear();
        }

        private void neuBtnExit_Click(object sender, EventArgs e)
        {
            //double diff = 0.001;
            //for (double i = 1.00 - diff; i >= 0; i -= diff)
            //{
            //    ((Form)this.Parent).Opacity = i;
            //    Application.DoEvents();
            //}
            ((Form)this.Parent).Hide();
        }

        private void neuBtnQuery_Click(object sender, EventArgs e)
        {
            //首先检索未回收的病历之中，有哪些病历室超时的，更新相关字段
            //this.UpdateOutTimeCase(); //暂时先注释掉 
            //查询
            this.QueryInfoToFP();
        }

        private void neuBtnPrint_Click(object sender, EventArgs e)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPage(this.neuPanel1.Left, this.neuPanel1.Top, this.neuPanel1);
            
        }

        private void neuBtnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDlg = new SaveFileDialog();
            saveFileDlg.Title = "导出到Excel";
            saveFileDlg.CheckFileExists = false;
            saveFileDlg.CheckPathExists = true;
            saveFileDlg.DefaultExt = "*.xls";
            saveFileDlg.Filter = "(*.xls)|*.xls";

            DialogResult dr = saveFileDlg.ShowDialog();
            if (dr == DialogResult.Cancel || string.IsNullOrEmpty(saveFileDlg.FileName))
            {
                return;
            }

            neuSpread1.SaveExcel(saveFileDlg.FileName);
        }


        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.fpSpread1_Sheet1.Visible = true;
            this.fpSpread1_Sheet1.RowCount = 0;
            ArrayList alDetail = new ArrayList();
            alDetail = this.callbackMgr.GetCaseCallbackPercentDetail(this.neuDateTimePicker1.Value, this.neuDateTimePicker2.Value, this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 7].Text);
            if (alDetail == null || alDetail.Count == 0)
            {
                return;
            }
            foreach (FS.HISFC.Models.HealthRecord.CaseHistory.CallBack cb in alDetail)
            {
                this.fpSpread1_Sheet1.RowCount++;
                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Text = cb.Patient.PID.PatientNO;
                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 1].Text = cb.Patient.Name;
                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 2].Text = cb.Patient.PVisit.AdmittingDoctor.Name;
                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 3].Text = cb.Patient.PVisit.OutTime.ToShortDateString();
                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 4].Text = cb.CallbackOper.ID.ToString();
                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 5].Text = cb.CallbackOper.OperTime.ToShortDateString();
                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 6].Text = cb.CallbackOper.Memo;
            }
            
        }

        private void btExportDetial_Click(object sender, EventArgs e)
        {
            if (this.fpSpread1_Sheet1.RowCount < 1)
            {
                return;
            }
            SaveFileDialog saveFileDlg = new SaveFileDialog();
            saveFileDlg.Title = "导出到Excel";
            saveFileDlg.CheckFileExists = false;
            saveFileDlg.CheckPathExists = true;
            saveFileDlg.DefaultExt = "*.xls";
            saveFileDlg.Filter = "(*.xls)|*.xls";

            DialogResult dr = saveFileDlg.ShowDialog();
            if (dr == DialogResult.Cancel || string.IsNullOrEmpty(saveFileDlg.FileName))
            {
                return;
            }

            this.fpSpread1.SaveExcel(saveFileDlg.FileName,FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
        }
    }
}
