using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Forms;

namespace FS.HISFC.Components.Privilege
{
    public partial class PermissionBaseForm : BaseStatusBar
    {
        public PermissionBaseForm()
        {
            InitializeComponent();
            if (this.MainToolStrip != null)
            {
                this.MainToolStrip.BackColor = FS.FrameWork.WinForms.Classes.Function.GetSysColor(FS.FrameWork.WinForms.Classes.EnumSysColor.Blue);
            }
        }

        private void PermissionBaseForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    }
}

