using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Report.Finance.FinIpb
{
    public partial class ucFinIpbUnbalanceByDept :FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        string reportCode=string.Empty;
        string reportName=string.Empty;

        public ucFinIpbUnbalanceByDept()
        {
            InitializeComponent();
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
                return -1;
            return base.OnRetrieve(base.beginTime, base.endTime, reportCode);
        }
        protected override void OnLoad()
        {
            this.isAcross = true;
            this.isSort = false;
            this.Init();
            base.OnLoad();

            FS.HISFC.BizProcess.Integrate.Manager manager=new FS.HISFC.BizProcess.Integrate.Manager();
            FS.HISFC.Models.Base.Const cons = new FS.HISFC.Models.Base.Const();
            cons.ID = "ALL";
            cons.Name = "È«²¿";
            this.neuComboBox1.Items.Add(cons);
            System.Collections.ArrayList arraylist = manager.GetConstantList("ITEMMINFEECODE");

            foreach (FS.HISFC.Models.Base.Const con in arraylist)
            {
                neuComboBox1.Items.Add(con);
            }
            if (neuComboBox1.Items.Count >= 0)
            {
                neuComboBox1.SelectedIndex = 0;
                reportCode = ((FS.HISFC.Models.Base.Const)neuComboBox1.Items[0]).ID;
                reportName = ((FS.HISFC.Models.Base.Const)neuComboBox1.Items[0]).Name;
            }
        }

        private void neuComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (neuComboBox1.SelectedIndex > -1)
            {
                reportCode = ((FS.HISFC.Models.Base.Const)neuComboBox1.Items[this.neuComboBox1.SelectedIndex]).ID;
                reportName = ((FS.HISFC.Models.Base.Const)neuComboBox1.Items[this.neuComboBox1.SelectedIndex]).Name;
            }
        }
    }
}
