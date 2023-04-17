using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.Case
{
    public partial class frmCaseStoreCancel : Form
    {
        public frmCaseStoreCancel()
        {
            InitializeComponent();
        }
        FS.HISFC.BizLogic.HealthRecord.Case.CaseStroe caseStoreMgr = new FS.HISFC.BizLogic.HealthRecord.Case.CaseStroe();

        private FS.HISFC.Models.HealthRecord.Case.CaseStore caseStore = new FS.HISFC.Models.HealthRecord.Case.CaseStore();
        private void btOK_Click(object sender, EventArgs e)
        {
            if (this.txtNewCaseNO.Text.Trim() == "")
            {
                this.txtNewCaseNO.Focus();
                MessageBox.Show("请输入“新病案号”", "提示");
                return;
            }
            if (this.txtNewInTimes.Text.Trim() == "")
            {
                this.txtNewInTimes.Focus();
                MessageBox.Show("请输入“新住院次数”", "提示");
                return;
            }

            string oldCaseNo = this.txtOldCaseNO.Text.PadLeft(10,'0');
            string oldCaseInTimes =this.txtOldInTimes.Text.Trim();
            string newCaseNo = this.txtNewCaseNO.Text.PadLeft(10,'0');
            string newCaseInTimes = this.txtNewInTimes.Text.Trim();

            if (this.caseStore != null && this.caseStore.PatientInfo.PID.PatientNO != "")
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.caseStoreMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.caseStore.IsVaild = false;
                if (this.caseStoreMgr.DeleteCaseStore(oldCaseNo,oldCaseInTimes) < 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("作废旧病案库房信息失败！", "提示");
                    return;

                }

                this.caseStore.IsVaild = true;
                this.caseStore.PatientInfo.PID.PatientNO = newCaseNo.PadLeft(10, '0');
                this.caseStore.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(newCaseInTimes);
                this.caseStore.CaseMemo = "修改病案号原住院号为：" + oldCaseNo + "-" + oldCaseInTimes;
                FS.HISFC.Models.HealthRecord.Case.CaseStore storeTemp = new FS.HISFC.Models.HealthRecord.Case.CaseStore();
                storeTemp = this.caseStoreMgr.QueryCaseStore(newCaseNo, newCaseInTimes);
                if (storeTemp != null && storeTemp.PatientInfo.PID.PatientNO != "")
                {
                    if (MessageBox.Show("已存在" + newCaseNo + "-" + newCaseInTimes + "病案记录,删除吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        this.caseStoreMgr.DeleteCaseStore(newCaseNo, newCaseInTimes);

                        if (this.caseStoreMgr.InsertCaseStore(this.caseStore) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存新病案库房信息失败！", "提示");
                            return;
                        }
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                
                MessageBox.Show("保存新病案库房信息成功！", "提示");
                this.ClearC();
            }
        }

        private void txtOldCaseNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                string caseNo = "";
                caseNo = this.txtOldCaseNO.Text;
                if (caseNo.IndexOf('-') > 0)
                {
                    string[] CaseNO = caseNo.Split('-');
                    this.txtOldCaseNO.Text = CaseNO[0].ToString().Trim();
                    this.txtOldInTimes.Text = CaseNO[1].ToString().Trim();
                }
                else if (caseNo.IndexOf('—') > 0)
                {
                    string[] CaseNO1 = caseNo.Split('—');
                    this.txtOldCaseNO.Text = CaseNO1[0].ToString().Trim();
                    this.txtOldInTimes.Text = CaseNO1[1].ToString().Trim();
                }
                caseStore = new FS.HISFC.Models.HealthRecord.Case.CaseStore();
                caseStore = this.caseStoreMgr.QueryCaseStore(this.txtOldCaseNO.Text.Trim().PadLeft(10, '0'), this.txtOldInTimes.Text.Trim());
                if (caseStore == null || caseStore.PatientInfo.PID.PatientNO == "")
                {
                    MessageBox.Show("库房不存在" + this.txtOldCaseNO.Text.Trim() +"-"+ this.txtOldInTimes.Text.Trim() + "的病案，无需修改住院号", "提示");
                    return;
                }
                this.txtNewCaseNO.Text = "";
                this.txtNewCaseNO.Focus();
            }
        }

        private void txtNewCaseNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                string caseNo = "";
                caseNo = this.txtNewCaseNO.Text;
                if (caseNo.IndexOf('-') > 0)
                {
                    string[] CaseNO = caseNo.Split('-');
                    this.txtNewCaseNO.Text = CaseNO[0].ToString().Trim();
                    this.txtNewInTimes.Text = CaseNO[1].ToString().Trim();
                }
                else if (caseNo.IndexOf('—') > 0)
                {
                    string[] CaseNO1 = caseNo.Split('—');
                    this.txtNewCaseNO.Text = CaseNO1[0].ToString().Trim();
                    this.txtNewInTimes.Text = CaseNO1[1].ToString().Trim();
                }
            }
        }

        private void btQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ClearC()
        {
            this.txtOldCaseNO.Text = "";
            this.txtOldInTimes.Text = "";
            this.txtNewCaseNO.Text = "";
            this.txtNewInTimes.Text = "";
        }

        private void frmCaseStoreCancel_Load(object sender, EventArgs e)
        {
            this.txtOldCaseNO.Text = "";
            this.txtOldCaseNO.Focus();
        }
    }
}