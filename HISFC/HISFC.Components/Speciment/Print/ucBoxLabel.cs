using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Speciment.Print
{
    public partial class ucBoxLabel : UserControl
    {
        public ucBoxLabel()
        {
            InitializeComponent();
        }

        public void SetBarCode(string location, string barCode,string encodeMsg,string specNum, DateTime date)
        {
            lblBarCode.Text = barCode;
            lblLoc.Text = location;
            lblLabel.Font = new Font("MW6 Matrix", 8, GraphicsUnit.Pixel);
            lblLabel.Text = encodeMsg;
            lblSpecNum.Text = specNum;
            lblDate.Text = date.ToShortDateString();
        }

        private void ucBoxLabel_Load(object sender, EventArgs e)
        {
            
        }
    }
}
