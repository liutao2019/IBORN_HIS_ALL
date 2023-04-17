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
    public partial class frmTextInput : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmTextInput()
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

        public delegate void OkButtonClink(string regNo);
        public event OkButtonClink OkButtonClickEvent;


        private void neuTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.textRegNO = this.neuTextBox1.Text;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.isOK=false;
            this.Close();
        }

        private void btnOK_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.neuTextBox1.Text))
	        {
		       this.isOK=true;
               if (OkButtonClickEvent!=null)
               {
                   OkButtonClickEvent(this.neuTextBox1.Text.Trim());
               }
	        }
            this.Close();
        }


    }
}
