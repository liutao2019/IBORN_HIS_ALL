using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.SOC.Local.EMR.Controls
{
    public partial class InHospitalRecordPluginHN : Neusoft.Emr.Record.UI.Internal.Controls.Plugin.FirstPagePlugin
    {
        public InHospitalRecordPluginHN()
        {
            InitializeComponent();
        }

        protected virtual string GetChildPluginType()
        {
            return "Neusoft.Emr.Record.UI.Internal.Controls.Plugin.RecordPlugin,Neusoft.Emr.Record.UI";
        }
    }
}
