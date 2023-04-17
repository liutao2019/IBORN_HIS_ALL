using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.InpatientFee.Components.Balance.SIBalance
{
    public partial class ucBaseChoose : UserControl
    {
        string selectedID;
        /// <summary>
        /// 选择的ID
        /// </summary>
        public string SelectedID { get { return selectedID; } }
        string selectedNO;
        /// <summary>
        /// 选择的NO
        /// </summary>
        public string SelectedNO { get { return selectedNO; } }
        string remarkNO;
        /// <summary>
        /// 选择的备注NO
        /// </summary>
        public string SelectedRemarkNO { get { return remarkNO; } }

        bool isOK = false;
        /// <summary>
        /// 是否确认
        /// </summary>
        public bool IsOK { get { return isOK; } }

        public ucBaseChoose(System.Data.DataTable dt)
        {
            InitializeComponent();
            this.fpSpread1_Sheet1.DataSource = dt;
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            try
            {
                int i = this.fpSpread1_Sheet1.ActiveRowIndex;
                if (this.fpSpread1_Sheet1.RowCount <= 0)
                {
                    return;
                }
                selectedID = fpSpread1_Sheet1.Cells[i, 0].Text;
                selectedNO = fpSpread1_Sheet1.Cells[i, 1].Text;
                remarkNO = fpSpread1_Sheet1.Cells[i, 2].Text;
                isOK = true;
                this.FindForm().Close();
            }
            catch (Exception)
            {
                isOK = false;
                throw;
            }            
        }
    }
}
