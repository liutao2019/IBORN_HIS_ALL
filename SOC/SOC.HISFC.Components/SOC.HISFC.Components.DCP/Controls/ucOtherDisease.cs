using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.DCP.Controls
{
    public partial class ucOtherDisease : ucBaseAddition
    {
        public ucOtherDisease()
        {
            InitializeComponent();
        }

        public override void PrePrint()
        {
            this.gbOtherAddition.BackColor = Color.White;
            this.BackColor = Color.White;
            base.PrePrint();
        }

        public override void Printed()
        {
            this.gbOtherAddition.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.BackColor = System.Drawing.Color.FromArgb(158, 177, 201);
            base.Printed();
        }
    }
}
