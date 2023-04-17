using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Windows.Forms
{
    public partial class MessageCheckBox : Form
    {
        public MessageCheckBox()
        {
            InitializeComponent();
            this.button1.Click += new EventHandler(button1_Click);
            this.button2.Click += new EventHandler(button2_Click);
        }

        void button2_Click(object sender, EventArgs e)
        {
            curDialogResult = DialogResult.No;
            this.Hide();
        }

        void button1_Click(object sender, EventArgs e)
        {
            curDialogResult = DialogResult.Yes;
            this.Hide();
        }

        private DialogResult curDialogResult = DialogResult.Cancel;

        public static CheckDialogResult Show(string text)
        {
            MessageCheckBox m = new MessageCheckBox();
            m.label1.Text = text;
            m.Width = 40 + m.label1.Width;
            m.ShowDialog();

            CheckDialogResult cr = CheckDialogResult.No;
            if (m.curDialogResult == DialogResult.Yes)
            {
                if (m.checkBox1.Checked)
                {
                    cr = CheckDialogResult.YesChecked;
                }
                else
                {
                    cr = CheckDialogResult.Yes;
                }
            }
            else
            {
                if (m.checkBox1.Checked)
                {
                    cr = CheckDialogResult.NoChecked;
                }
                else
                {
                    cr = CheckDialogResult.No;
                }
            }
            if (m.DialogResult != DialogResult.Cancel)
            {
                m.Close();
            }
            return cr;
        }
    }

    public enum CheckDialogResult
    {
        Yes = 0,
        YesChecked = 1,
        No = 2,
        NoChecked = 3,
    }
}
