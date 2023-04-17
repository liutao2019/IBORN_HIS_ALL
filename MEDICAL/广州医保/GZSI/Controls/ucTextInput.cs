using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GZSI.Controls
{
    public partial class ucTextInput : UserControl
    {
        public ucTextInput()
        {
            InitializeComponent();
        }

        private string textRegNO = "";
        private bool isOK = false;

        public string TextRegNO
        {
            get { return textRegNO; }
        }

        public bool IsOK
        {
            get { return this.isOK; }
        }

        private void neuTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.textRegNO = this.neuTextBox1.Text;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }




    }
}
