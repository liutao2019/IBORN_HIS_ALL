using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Registration
{
    /// <summary>
    /// 显示挂号信息
    /// </summary>
    public partial class ucShowRegisterInfo : FS.FrameWork.WinForms.Controls.ucBaseControl 
    {
        /// <summary>
        /// 显示挂号信息
        /// </summary>
        public ucShowRegisterInfo()
        {
            InitializeComponent();
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

        /// <summary>
        /// 看诊流水号
        /// </summary>
        public string ClinicNO
        {
            get
            {
                if (this.neuSpread1_Sheet1.RowCount <= 0)
                {
                    return null;
                }

                int row = this.neuSpread1_Sheet1.ActiveRowIndex;

                return (this.neuSpread1_Sheet1.Rows[row].Tag as FS.HISFC.Models.Registration.Register).ID;
            }
        }

        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNO
        {
            get
            {
                if (this.neuSpread1_Sheet1.RowCount <= 0)
                {
                    return null;
                }

                int row = this.neuSpread1_Sheet1.ActiveRowIndex;

                return (this.neuSpread1_Sheet1.Rows[row].Tag as FS.HISFC.Models.Registration.Register).InvoiceNO;
            }
        }


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

            if (SelectedRegister != null)
            {
                SelectedRegister(reg);
            }
            this.FindForm().Close();

        }

        #endregion

        private void neuSpread1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int activeRow = this.neuSpread1_Sheet1.ActiveRowIndex;
                if (activeRow < 0 || activeRow > this.neuSpread1_Sheet1.Rows.Count)
                {
                    return;
                }

                FS.HISFC.Models.Registration.Register reg = this.neuSpread1_Sheet1.Rows[activeRow].Tag as FS.HISFC.Models.Registration.Register;
                if (reg == null)
                {
                    return;
                }

                if (SelectedRegister != null)
                {
                    SelectedRegister(reg);
                }
                this.FindForm().Close();
            }
        }

        


    }
}
