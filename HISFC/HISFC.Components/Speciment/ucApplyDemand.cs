using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Speciment
{
    public partial class ucApplyDemand : UserControl
    {
        public ucApplyDemand()
        {
            InitializeComponent();
        }
        public void SetDemand(string orgName, string specType, string count, string disType,string tumorType,string outCount)
        {
            lblSpecType.Text = specType;
            lblCount.Text = count;
            lblDisType.Text = disType;
            lblTumorType.Text = tumorType;
            lblOutCount.Text = outCount;
        }
    }
    
}
