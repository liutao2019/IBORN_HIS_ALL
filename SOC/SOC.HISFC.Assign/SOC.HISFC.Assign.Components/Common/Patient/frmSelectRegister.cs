using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Assign.Components.Common.Patient
{
    /// <summary>
    /// [功能描述: 门诊分诊刷卡后选择挂号记录]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public partial class frmSelectRegister : BaseForm
    {
        public frmSelectRegister()
        {
            InitializeComponent();

            this.neuSpread1.KeyDown += new KeyEventHandler(neuSpread1_KeyDown);
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpread1_CellDoubleClick);
        }

        #region 变量

        /// <summary>
        /// 定义选择事件委托
        /// </summary>
        /// <param name="reg"></param>
        public delegate void GetRegister(FS.HISFC.Models.Registration.Register reg);

        /// <summary>
        /// 定义事件
        /// </summary>
        public event GetRegister SelectedRegister;

        #endregion

        #region 方法

        /// <summary>
        /// 显示挂号患者信息
        /// </summary>
        /// <param name="registerList"></param>
        public void SetRegisterInfo(ArrayList registerList)
        {
            //先清空
            this.neuSpread1_Sheet1.Rows.Count = 0;

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

                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);
                int index = this.neuSpread1_Sheet1.Rows.Count - 1;

                this.neuSpread1_Sheet1.SetValue(index, 0, reg.InvoiceNO, false);
                this.neuSpread1_Sheet1.SetValue(index, 1, reg.Name, false);
                this.neuSpread1_Sheet1.SetValue(index, 2, reg.DoctorInfo.SeeDate.ToString(), false);
                this.neuSpread1_Sheet1.SetValue(index, 3, reg.DoctorInfo.Templet.RegLevel.Name, false);
                this.neuSpread1_Sheet1.SetValue(index, 4, reg.DoctorInfo.Templet.Dept.Name, false);
                this.neuSpread1_Sheet1.SetValue(index, 5, reg.DoctorInfo.Templet.Doct.Name, false);
                this.neuSpread1_Sheet1.SetValue(index, 6, reg.RegLvlFee.RegFee + reg.RegLvlFee.OwnDigFee + reg.RegLvlFee.ChkFee + reg.RegLvlFee.OthFee, false);

                this.neuSpread1_Sheet1.Rows[index].Tag = reg;

            }

            //调整最适合列宽
            for (int j = 0; j < this.neuSpread1_Sheet1.Columns.Count; j++)
            {
                this.neuSpread1_Sheet1.Columns[j].Width = this.neuSpread1_Sheet1.Columns[j].GetPreferredWidth();
            }

            this.neuSpread1.Focus();
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.AddSelection(0, 1, 1, 1);
                this.neuSpread1_Sheet1.ActiveRowIndex = 0;
            }
        }

        /// <summary>
        /// 选择患者事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Row < 0 || e.Row > this.neuSpread1_Sheet1.Rows.Count)
            {
                return;
            }

            FS.HISFC.Models.Registration.Register reg = this.neuSpread1_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.Registration.Register;
            if (reg == null)
            {
                return;
            }

            SelectedRegister(reg);

            this.Close();

        }

        /// <summary>
        /// 选择患者事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
            else if(e.KeyData== Keys.Enter)
            {
                if (this.neuSpread1_Sheet1.ActiveRowIndex < 0 || this.neuSpread1_Sheet1.ActiveRowIndex > this.neuSpread1_Sheet1.Rows.Count)
                {
                    return;
                }

                FS.HISFC.Models.Registration.Register reg = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Registration.Register;
                if (reg == null)
                {
                    return;
                }

                SelectedRegister(reg);

                this.Close();

            }

        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape || keyData == Keys.Enter)
            {
                neuSpread1_KeyDown(null, new KeyEventArgs(keyData));
            }
            return base.ProcessDialogKey(keyData);
        }

        #endregion
    }
}
