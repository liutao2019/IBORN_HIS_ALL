using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.InpatientFee.FuYou
{
    public partial class TerminalFeeQuery : Base.ucDeptTimeBaseReport
    {
        public TerminalFeeQuery()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
           
            this.ColumnHeaderHeight = 24f;
            this.fpSpread1_Sheet1.ColumnHeader.Rows[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
           
            this.ucQueryInpatientNo1.txtInputCode.KeyUp += new KeyEventHandler(txtInputCode_KeyUp);

        }

        void txtInputCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                //this.tvMessageBaseTree1.FindPatient(this.ucQueryInpatientNo1.InpatientNo, this.DrugControl, this.IsDrugClassBillFirst);
            }
        }

    }
}
