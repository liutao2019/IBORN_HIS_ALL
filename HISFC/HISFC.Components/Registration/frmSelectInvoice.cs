using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Registration
{
    /// <summary>
    /// 选择需要打印的发票
    /// </summary>
    public partial class frmSelectInvoice : Form
    {
        public frmSelectInvoice()
        {
            InitializeComponent();

            this.fpSpread1.KeyDown += new KeyEventHandler(fpSpread1_KeyDown);
            this.bnOK.Click += new EventHandler(bnOK_Click);
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellDoubleClick);
        }


        private void fpSpread1_KeyDown(object sender, KeyEventArgs e)
        {
            OK();
        }

        private void OK()
        {
            if (this.fpSpread1_Sheet1.ActiveRowIndex < 0 || this.fpSpread1_Sheet1.ActiveRowIndex > this.fpSpread1_Sheet1.Rows.Count)
            {
                return;
            }

            FS.HISFC.Models.Registration.Register reg = this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Registration.Register;
            if (reg == null)
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void bnOK_Click(object sender, EventArgs e)
        {
            this.OK();
        }

        /// <summary>
        /// 看诊流水号
        /// </summary>
        public string ClinicNO
        {
            get
            {
                if (this.fpSpread1_Sheet1.RowCount <= 0)
                {
                    return null;
                }

                int row = this.fpSpread1_Sheet1.ActiveRowIndex;

                return (this.fpSpread1_Sheet1.Rows[row].Tag as FS.HISFC.Models.Registration.Register).ID;
            }
        }

        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNO
        {
            get
            {
                if (this.fpSpread1_Sheet1.RowCount <= 0)
                {
                    return null;
                }

                int row = this.fpSpread1_Sheet1.ActiveRowIndex;

                return (this.fpSpread1_Sheet1.Rows[row].Tag as FS.HISFC.Models.Registration.Register).InvoiceNO;
            }
        }

        #region 方法

        /// <summary>
        /// 显示挂号患者信息
        /// </summary>
        /// <param name="registerList"></param>
        public void SetRegInfo(ArrayList registerList)
        {
            //先清空
            this.fpSpread1_Sheet1.Rows.Count = 0;

            if (registerList == null || registerList.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < registerList.Count; i++)
            {
                FS.HISFC.Models.Registration.Register reg = registerList[i] as FS.HISFC.Models.Registration.Register;
                if (reg == null)
                {
                    return;
                }

                this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.Rows.Count, 1);
                int index = this.fpSpread1_Sheet1.Rows.Count - 1;

                this.fpSpread1_Sheet1.SetValue(index, 0, reg.InvoiceNO, false);
                this.fpSpread1_Sheet1.SetValue(index, 1, reg.Name, false);
                this.fpSpread1_Sheet1.SetValue(index, 2, reg.DoctorInfo.SeeDate.ToString(), false);
                this.fpSpread1_Sheet1.SetValue(index, 3, reg.DoctorInfo.Templet.RegLevel.Name, false);
                this.fpSpread1_Sheet1.SetValue(index, 4, reg.DoctorInfo.Templet.Dept.Name, false);
                this.fpSpread1_Sheet1.SetValue(index, 5, reg.DoctorInfo.Templet.Doct.Name, false);
                this.fpSpread1_Sheet1.SetValue(index, 6, reg.RegLvlFee.RegFee + reg.RegLvlFee.OwnDigFee + reg.RegLvlFee.ChkFee + reg.RegLvlFee.OthFee, false);

                this.fpSpread1_Sheet1.Rows[index].Tag = reg;

            }

            //调整最适合列宽
            for (int j = 0; j < this.fpSpread1_Sheet1.Columns.Count; j++)
            {
                this.fpSpread1_Sheet1.Columns[j].Width = this.fpSpread1_Sheet1.Columns[j].GetPreferredWidth();
            }
            
        }

        /// <summary>
        /// 选择患者事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            OK();
        }

        #endregion

    }
}