using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.InpatientFee.GuangZhou
{
    public partial class ucAdjustOverTop : UserControl
    {
        public ucAdjustOverTop()
        {
            InitializeComponent();
        }

        FS.HISFC.Models.RADT.PatientInfo pinfo = new FS.HISFC.Models.RADT.PatientInfo();
        FS.HISFC.BizLogic.RADT.InPatient pMgr = new FS.HISFC.BizLogic.RADT.InPatient();

        private void ucAdjustOverTop_Load(object sender, System.EventArgs e)
        {
            this.txtPatient.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

            this.richTextBox1.Text = "提示：\n1.本程序仅用于老系统数据导入时更改患者日限额；\n2.如果患者不超标，日限额超标部分应为负值。";
            this.richTextBox1.ForeColor = System.Drawing.Color.Red;
        }

        private void textBox1_myEvent()
        {
            if (this.txtPatient.InpatientNo == null || this.txtPatient.InpatientNo == "")
            {
                MessageBox.Show("该住院号不存在");
                return;
            }


            pinfo = pMgr.QueryPatientInfoByInpatientNO(this.txtPatient.InpatientNo);
            if (pinfo == null)
            {
                MessageBox.Show("查询患者基本信息出错");
                return;
            }
            this.txtTot.Text = pinfo.FT.DayLimitTotCost.ToString();
            this.txtOverTop.Text = pinfo.FT.OvertopCost.ToString();
            this.txtName.Text = pinfo.Name;
            this.txtPact.Text = pinfo.Pact.Name;
            if (pinfo.Pact.PayKind.ID != "03")
            {
                MessageBox.Show("非公费患者,无需调整");
                this.pinfo = null;
                this.txtPatient.TextBox.SelectAll();
                return;
            }
            this.txtOverTop.Focus();

        }
        /// <summary>
        /// 传入患者基本信息
        /// </summary>
        /// <param name="info"></param>
        public void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo info)
        {
            try
            {
                if (pinfo == null) return;

                this.pinfo = info;
                this.txtPatient.TextBox.Text = this.pinfo.Patient.PID.ID;
                this.txtTot.Text = pinfo.FT.DayLimitTotCost.ToString();
                this.txtOverTop.Text = pinfo.FT.OvertopCost.ToString();
                this.txtName.Text = pinfo.Name;
                this.txtPact.Text = pinfo.Patient.Pact.Name;
                if (pinfo.Pact.PayKind.ID != "03")
                {
                    MessageBox.Show("非公费患者,无需调整");
                    this.pinfo = null;
                    this.txtPatient.TextBox.SelectAll();
                    return;
                }
                this.txtOverTop.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (this.pinfo == null)
            {
                MessageBox.Show("请选则患者");
                return;
            }
            if (this.txtOverTop.Text == "" || this.txtOverTop.Text == "")
            {
                MessageBox.Show("请输入日限额超标部分");
                return;
            }
            decimal OverTop = 0m;
            decimal Tot = 0m;
            try
            {
                OverTop = decimal.Parse(this.txtOverTop.Text);
                Tot = decimal.Parse(this.txtTot.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("转换数据出错" + ex.Message);
                this.txtOverTop.Focus();
                return;
            }
            FS.HISFC.BizLogic.Fee.InPatient feeMgr = new FS.HISFC.BizLogic.Fee.InPatient();
            if (feeMgr.UpdateLimitOverTopAndTotFlag(this.pinfo.ID, OverTop, Tot, "M") < 1)
            {
                MessageBox.Show("更新失败" + feeMgr.Err);
                return;
            }
            else
            {

                MessageBox.Show("更新成功");
                this.pinfo = null;
                this.txtTot.Text = "";
                this.txtPact.Text = "";
                this.txtOverTop.Text = "";
                this.txtName.Text = "";
                return;
            }
        }
    }
}
