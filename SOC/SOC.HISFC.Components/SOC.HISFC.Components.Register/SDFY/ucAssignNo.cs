using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Register.SDFY
{
    public partial class ucAssignNo : Form
    {
        public ucAssignNo()
        {
            InitializeComponent();
        }

        public delegate void GetAssignID(string assignID);

        public event GetAssignID getAssinID;

        public void Init(ArrayList list)
        {
            foreach (string k in list)
            {
                this.neuSpread1_Sheet1.Rows.Count++;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 0].Text = k;
            }
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (getAssinID != null)
            {
                this.getAssinID(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text);
            }            
            this.Close();
        }

        private void ucAssignNo_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.neuSpread1_Sheet1.RowCount = 0;
        }



    }
}
