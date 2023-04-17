using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Components.Common.Forms;

namespace FS.HISFC.Components.Material.In
{
    public partial class frmIn : frmIMABaseForm, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public frmIn()
        {
            InitializeComponent();
            this.Text = "物资入库";
        }
        In.ucMatIn uc = new ucMatIn();
        protected override void OnLoad(EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            try
            {
                
                this.AddIMABaseCompoent(uc);
            }
            catch
            {
            }
            base.OnLoad(e);
        }

        #region IPreArrange 成员

        public int PreArrange()
        {
            if (this.uc != null)
            {
                if (this.uc is FS.FrameWork.WinForms.Classes.IPreArrange)
                {
                    return (this.uc as FS.FrameWork.WinForms.Classes.IPreArrange).PreArrange();
                }
            }

            return 1;
        }

        #endregion
    }
}