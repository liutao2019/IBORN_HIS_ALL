using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace API.GZSI.UI
{
    public partial class frmQueryBaseInfoLocal : Form
    {
        public frmQueryBaseInfoLocal()
        {
            InitializeComponent();
        }

        List<FS.FrameWork.WinForms.Controls.NeuSpread> lstSpread = null;
        /// <summary>
        /// 医保数据操作
        /// </summary>
        private LocalManager localMgr = new LocalManager();
        private void btQuery_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = this.localMgr.QueryALLBaseInfoLoacl(this.dtBeginTime.Value.Date.ToString(), this.dtEndTime.Value.Date.AddDays(1).AddSeconds(-1).ToString());
            if (dt == null || dt.Rows.Count <= 0)
            {
                MessageBox.Show("没有信息！");
                return;
            }
            SetFP(dt);
        }
        private void SetFP(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }
            this.neuSpread1_Sheet1.RowCount = 0;
            int rowIndex = this.neuSpread1_Sheet1.Rows.Count;
            this.cbCheckAll.Checked = true;
            foreach (DataRow dRow in dt.Rows)
            {
                this.neuSpread1_Sheet1.Rows.Add(rowIndex, 1);
                this.neuSpread1_Sheet1.Cells[rowIndex, 0].Value = true;//FS.FrameWork.Function.NConvert.ToBoolean("1");
                this.neuSpread1_Sheet1.Cells[rowIndex, 1].Value = dRow["证件类型"].ToString();
                this.neuSpread1_Sheet1.Cells[rowIndex, 2].Value = dRow["证件号码"].ToString();
                this.neuSpread1_Sheet1.Cells[rowIndex, 3].Value = dRow["姓名"].ToString();
                this.neuSpread1_Sheet1.Cells[rowIndex, 4].Value = dRow["就诊日期"].ToString();
                this.neuSpread1_Sheet1.Cells[rowIndex, 5].Value = dRow["检测次数"].ToString();
                this.neuSpread1_Sheet1.Cells[rowIndex, 6].Value = dRow["医疗总额"].ToString();
                this.neuSpread1_Sheet1.Cells[rowIndex, 7].Value = dRow["医疗总额"].ToString();
                this.neuSpread1_Sheet1.Cells[rowIndex, 8].Value = dRow["核酸检测患者类型"].ToString();
                this.neuSpread1_Sheet1.Cells[rowIndex, 9].Value = dRow["项目信息"].ToString();
                this.neuSpread1_Sheet1.Cells[rowIndex, 10].Value = dRow["患者类型"].ToString();

                this.neuSpread1_Sheet1.Cells[rowIndex, 0].Tag = dRow;
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("无可导入信息!");
                return;
            }

            bool isSuc = true;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在导入!请稍后!");

            for (int k = 0; k < this.neuSpread1_Sheet1.Rows.Count; k++)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(k, this.neuSpread1_Sheet1.Rows.Count);

                bool isChoose = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[k, 0].Value);
                if (isChoose)
                {
                    DataRow dRow = this.neuSpread1_Sheet1.Cells[k, 0].Tag as DataRow;
                    if (dRow != null)
                    {
                        if (string.IsNullOrEmpty(dRow["证件类型"].ToString()))
                        {
                            isSuc = false;
                            continue;
                        }
                        if (string.IsNullOrEmpty(dRow["证件号码"].ToString()))
                        {
                            isSuc = false;
                            continue;
                        }
                        //if (string.IsNullOrEmpty(dRow["项目信息"].ToString()))
                        //{
                        //    continue;
                        //}
                        if (string.IsNullOrEmpty(dRow["就诊流水号"].ToString()))
                        {
                            isSuc = false;
                            continue;
                        }
                        FS.HISFC.Models.Registration.Register r = new FS.HISFC.Models.Registration.Register();
                        r.IDCardType.ID = dRow["证件类型"].ToString();
                        r.IDCard = dRow["证件号码"].ToString();
                        r.Name = dRow[2].ToString();
                        r.DoctorInfo.SeeDate = Convert.ToDateTime(dRow["就诊日期"].ToString());
                        r.InTimes = 1;
                        r.OwnCost = Convert.ToDecimal(dRow["医疗总额"].ToString());
                        r.PubCost = Convert.ToDecimal(dRow["医疗总额"].ToString());
                        if (r.OwnCost <= 30)
                        {
                            isSuc = false;
                            continue;
                        }
                        r.PatientType = dRow["核酸检测患者类型"].ToString();
                        r.Insurance.Name = dRow["项目信息"].ToString();
                        r.Insurance.Memo = (dRow["患者类型"].ToString() == "门诊" ? "1" : "2");
                        r.ID = dRow["就诊流水号"].ToString();
                        if (this.localMgr.InsertHSBaseInfo(r) <= 0)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("导入失败！" + this.localMgr.Err);
                            return;
                        }
                    }
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            if (isSuc)
            {
                MessageBox.Show("操作完成！");
                //btQuery_Click(null, null);
                this.Close();
            }
            else
            {
                MessageBox.Show("存在未导入成功的信息！");
                btQuery_Click(null, null);
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btOutPut_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("无可导出信息!");
                return;
            }

            //提示
            string strTips = "将导出全部查询信息，是否导出信息？";
            if (MessageBox.Show(strTips, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            lstSpread = new List<FS.FrameWork.WinForms.Controls.NeuSpread>();
            lstSpread.Add(this.neuSpread1);
            if (lstSpread != null && lstSpread.Count > 0)
            {
                if (lstSpread[0].Export() > 0)
                {
                    MessageBox.Show("导出成功！导出的Excel将被保护，取消保护请点：审阅-撤销保护工作表，取消后方可修改！");
                }
                else
                {
                    MessageBox.Show("导出失败！");
                }
            }
        }

        private void cbCheckAll_CheckedChanged(object sender, EventArgs e)
        {

            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                for (int k = 0; k < this.neuSpread1_Sheet1.Rows.Count; k++)
                {
                    if (this.cbCheckAll.Checked)
                    {
                        this.neuSpread1_Sheet1.Cells[k, 0].Value = true;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[k, 0].Value = false;
                    }
                }
            }
        }
    }
}
