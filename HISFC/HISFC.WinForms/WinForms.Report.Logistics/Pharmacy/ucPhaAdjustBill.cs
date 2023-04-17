using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.Report.Logistics.Pharmacy
{
    public partial class ucPhaAdjustBill : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucPhaAdjustBill()
        {
            InitializeComponent();
            
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (this.neuTextBox1.Text.Trim().Equals(""))
            {
                return -1;
            }
            //objects = new object[] { this.neuTextBox1.Text.Trim() };
            return base.OnRetrieve(this.neuTextBox1.Text.Trim());//this.dwMain.Retrieve(objects);
        }

        public void Print(string listCode)
        {
            base.OnRetrieve(listCode);
        }
    }
}
