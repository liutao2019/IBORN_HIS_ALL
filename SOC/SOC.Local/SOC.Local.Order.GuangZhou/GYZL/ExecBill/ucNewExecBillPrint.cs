using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.GuangZhou.GYZL.ExecBill
{
    public partial class ucNewExecBillPrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucNewExecBillPrint()
        {
            InitializeComponent();
        }

        public void ResetSize()
        {
            int height = 0;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                height += (int)this.neuSpread1_Sheet1.Rows[i].Height;
            }
            this.Height = height;
            
            int width = 0;
            for(int i = 0; i < this.neuSpread1_Sheet1.ColumnCount; i++)
            {
                width += (int)this.neuSpread1_Sheet1.Columns[i].Width;
            }
            this.Width = width;
        }
    }
}
