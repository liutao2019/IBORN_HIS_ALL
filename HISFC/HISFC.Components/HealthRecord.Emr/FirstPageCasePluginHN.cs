using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.Emr
{
    public partial class FirstPageCasePluginHN : FS.Emr.Record.UI.Internal.Controls.Plugin.FirstPagePlugin
    {
        public FirstPageCasePluginHN()
        {
            InitializeComponent();
        }

        protected override string GetChildPluginType()
        {
            return "FS.HISFC.Components.HealthRecord.Emr.FirstPageChildPluginHN,HISFC.Components.HealthRecord.Emr";
        }
    }
}
