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
    public partial class InputBaseForm : BaseStatusBar
    {
        public InputBaseForm()
        {
            InitializeComponent();
            this.statusBar1.Visible = false;
        }
    }
}

