using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.OutpatientFee.Default
{
    public partial class ucKeeyAccountPatientDefend : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 变量
        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #endregion

        public ucKeeyAccountPatientDefend()
        {
            InitializeComponent();
        }

        private void ucKeeyAccountPatientDefend_Load(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.Columns[1].Width = 0;
        }

        public override FS.FrameWork.WinForms.Forms.ToolBarService Init(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("作废", "作废选中记录!", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z作废, true, false, null);
            toolBarService.AddToolButton("启用", "启用选中记录", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z召回, true, false, null);

            return this.toolBarService;
        }

        public override int Query(object sender, object neuObject)
        {
            this.neuSpread1_Sheet1.RowCount = 0;

            string strIdno = txtIDNO.Text.Trim();
            string strName = txtName.Text.Trim();

            if (string.IsNullOrEmpty(strIdno))
            {
                strIdno = "ALL";
            }
            if (string.IsNullOrEmpty(strName))
            {
                strName = "ALL";
            }
            else
            {
                strName += "%";
            }

            DataSet dsTemp = null;
            int iRes = accountManager.ExecQuery("Fee.FSlocal.KeepAccountPatient.Select.1", ref dsTemp, strIdno, strName);
            if (iRes > 0 && dsTemp != null && dsTemp.Tables.Count > 0)
            {
                DataTable dtTemp = dsTemp.Tables[0];

                SetSheetRows(this.neuSpread1_Sheet1, dtTemp);
            }

            
            return 1;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (txtEditName.Tag == null)
            {
                MessageBox.Show("请刷卡添加记录!");
                return -1;
            }

            DataRow drPatient = txtEditName.Tag as DataRow;
            if (drPatient == null)
            {
                MessageBox.Show("请刷卡添加记录!");
                return -1;
            }

            string seqNO = drPatient["seqno"].ToString().Trim();
            string CardNO = drPatient["card_no"].ToString().Trim();
            string Idno = this.txtEditIdno.Text.Trim();
            string strName = this.txtEditName.Text.Trim();
            string strSex = this.cmbSex.Tag.ToString();
            switch (strSex)
            {
                case "M":
                case "男":
                    strSex = "M";
                    break;

                case "F":
                case"女":
                    strSex = "F";
                    break;

                default:
                    strSex = "";
                    break;
            }

            string strSSDW = txtSSDW.Text.Trim();
            string strStartDate = startDate.Value.ToString("yyyy-MM-dd 00:00:00");
            string strEndDate = EndDate.Value.ToString("yyyy-MM-dd 23:59:59");


            FS.HISFC.Models.Base.Employee employee = accountManager.Operator as FS.HISFC.Models.Base.Employee;


            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            int iRes = accountManager.ExecNoQueryByIndex("Fee.FSlocal.KeepAccountPatient.Insert", CardNO, strName, strSex, Idno, strSSDW, employee.ID, strStartDate, strEndDate);
            if (iRes <= 0)
            {
                iRes = accountManager.ExecNoQueryByIndex("Fee.FSlocal.KeepAccountPatient.Update.1", CardNO, strName, strSex, Idno, strSSDW, employee.ID, strStartDate, strEndDate);

            }
            if (iRes <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("保存失败! " + accountManager.Err, "系统提示");
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("保存成功! ", "系统提示");

            return 1;
        }

        private void SetSheetRows(FarPoint.Win.Spread.SheetView sheet, DataTable dtResult)
        {
            if (sheet == null)
                return;
            sheet.RowCount = 0;
            if (dtResult == null || dtResult.Rows.Count <= 0)
            {
                return;
            }

            sheet.RowCount = dtResult.Rows.Count;
            int iRowIdx = 0;
            string strTemp = "";
            foreach (DataRow dr in dtResult.Rows)
            {
                sheet.Cells[iRowIdx, 0].Text = dr["seqno"].ToString().Trim();
                sheet.Cells[iRowIdx, 1].Text = dr["card_no"].ToString().Trim();
                sheet.Cells[iRowIdx, 2].Text = dr["idenno"].ToString().Trim();
                sheet.Cells[iRowIdx, 3].Text = dr["name"].ToString().Trim();
                sheet.Cells[iRowIdx, 4].Text = dr["ssdw"].ToString().Trim();

                strTemp = dr["btime"].ToString().Trim() + " -- " + dr["etime"].ToString().Trim();
                sheet.Cells[iRowIdx, 5].Text = strTemp;

                strTemp = dr["is_valid"].ToString().Trim();
                if (dr["is_valid"].ToString().Trim() == "1")
                {
                    strTemp = "有效";
                }
                else
                {
                    strTemp = "无效";
                    sheet.Rows[iRowIdx].ForeColor = Color.Red;
                }

                sheet.Cells[iRowIdx, 6].Text = strTemp;

                iRowIdx++;
            }
        }

        

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Row < 0 && e.Row > this.neuSpread1_Sheet1.RowCount)
                return;

            string cardNO = this.neuSpread1_Sheet1.Cells[e.Row, 1].Text;
            if (string.IsNullOrEmpty(cardNO))
                return;

            string state = this.neuSpread1_Sheet1.Cells[e.Row, 6].Text;
            if (state != "有效")
            {
                MessageBox.Show("该记录已无效,请重新启用!");
                return;
            }

            DataTable dtResult = null;
            int iRes = QueryPatientInfo(cardNO, out dtResult);
            if (iRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
            {
                SetPatientInfo(dtResult.Rows[0]);

                this.tabPageMain.SelectedIndex = 1;
                this.startDate.Focus();
            }
        }

        private void txtMarkNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            string MarkNO = txtMarkNO.Text.Trim();
            FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            int resultValue = accountManager.GetCardByRule(MarkNO, ref accountCard);
            if (resultValue <= 0 || accountCard == null || string.IsNullOrEmpty(accountCard.Patient.PID.CardNO))
            {
                MessageBox.Show("获取信息失败!");
            }



            DataTable dtResult = null;
            int iRes = QueryPatientInfo(accountCard.Patient.PID.CardNO, out dtResult);
            if (iRes >= 0 && dtResult != null && dtResult.Rows.Count > 0)
            {
                SetPatientInfo(dtResult.Rows[0]);

                this.tabPageMain.SelectedIndex = 1;
                this.startDate.Focus();
            }
        }

        private int QueryPatientInfo(string CardNo, out DataTable dtResult)
        {
            dtResult = null;
            DataSet dsTemp = null;
            int iRes = accountManager.ExecQuery("Fee.FSlocal.KeepAccountPatient.Select.2", ref dsTemp, CardNo);
            if (iRes >= 0 && dsTemp != null && dsTemp.Tables.Count > 0)
            {
                dtResult = dsTemp.Tables[0];
            }

            return 1;
        }

        private void SetPatientInfo(DataRow drPatient)
        {
            this.ClearPatientInfo();

            if (drPatient == null)
            {
                return;
            }

            txtEditName.Tag = drPatient;

            txtEditName.Text = drPatient["name"].ToString().Trim();
            string Sex = drPatient["sex_code"].ToString().Trim();
            switch (Sex)
            {
                case "男":
                case "M":
                case "1":
                    this.cmbSex.Text = "男";
                    this.cmbSex.Tag = "M";
                    break;

                case "女":
                case "F":
                case "2":
                    this.cmbSex.Text = "女";
                    this.cmbSex.Tag = "F";
                    break;

                default:
                    this.cmbSex.Text = "";
                    this.cmbSex.Tag = "";
                    break;
            }

            txtEditIdno.Text = drPatient["idenno"].ToString().Trim();
            txtSSDW.Text = drPatient["SSDW"].ToString().Trim();

            try
            {
                startDate.Value = DateTime.Parse(drPatient["btime"].ToString());
                EndDate.Value = DateTime.Parse(drPatient["etime"].ToString());
            }
            catch
            {
                startDate.Value = DateTime.Now;
                EndDate.Value = DateTime.Now;
            }
            
        }

        private void ClearPatientInfo()
        {
            txtEditName.Tag = null;
            txtEditName.Text = "";
            this.cmbSex.Text = "男";
            this.cmbSex.Tag = "M";
            txtEditIdno.Text = "";
            txtSSDW.Text = "";
            startDate.Value = DateTime.Now;
            EndDate.Value = DateTime.Now;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "作废")
            {
                if (this.tabPageMain.SelectedIndex == 1)
                {
                    MessageBox.Show("请选择需要作废的记录!");
                    return;
                }

                //
                SetRecordState("0");
            }
            else if (e.ClickedItem.Text == "启用")
            {
                if (this.tabPageMain.SelectedIndex == 1)
                {
                    MessageBox.Show("请选择需要启用的记录!");
                    return;
                }

                SetRecordState("1");
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// 作废启用记录
        /// </summary>
        /// <param name="operType">操作类型 1=启用,0=作废</param>
        private void SetRecordState(string operType)
        {
            if (operType == "1" || operType == "0")
            {
                if (this.neuSpread1_Sheet1.ActiveRow == null)
                {
                    MessageBox.Show("请选择记录");
                    return;
                }

                int iActive = this.neuSpread1_Sheet1.ActiveRowIndex;
                string cardNO = this.neuSpread1_Sheet1.Cells[iActive, 1].Text;

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                int iRes = accountManager.ExecNoQueryByIndex("Fee.FSlocal.KeepAccountPatient.Update.SetValid", cardNO, operType);
                if (iRes <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("操作失败, " + accountManager.Err, "系统提示");
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();

                this.neuSpread1_Sheet1.Cells[iActive, 6].Text = operType == "1" ? "有效" : "无效";
                this.neuSpread1_Sheet1.Rows[iActive].ForeColor = operType == "1" ? Color.Black : Color.Red;
            }

        }

        


        #region 业务层变量
        /// <summary>
        /// 门诊账户业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        #endregion

        
    }
}
