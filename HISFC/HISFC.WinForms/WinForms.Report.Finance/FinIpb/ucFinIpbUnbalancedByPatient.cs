using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Report.Finance.FinIpb
{
    public partial class ucFinIpbUnbalancedByPatient :FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinIpbUnbalancedByPatient()
        {
            InitializeComponent();
        }
        private string deptcode = string.Empty;
        private string deptname = string.Empty;
        private string reportCode = string.Empty;
        private string reportName = string.Empty;

        protected override void OnLoad()
        {
            this.isAcross = true;
            this.isSort = false;
            this.Init();
            base.OnLoad();
            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            System.Collections.ArrayList constantList = manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.I);

            FS.HISFC.Models.Base.Department top = new FS.HISFC.Models.Base.Department();
            top.ID = "ALL";
            top.Name = "全  部";

            this.neuComboBox1.Items.Add(top);
            foreach (FS.HISFC.Models.Base.Department con in constantList)
            {
                neuComboBox1.Items.Add(con);
            }
            this.neuComboBox1.alItems.Add(top);
            this.neuComboBox1.alItems.AddRange(constantList);

            if (neuComboBox1.Items.Count > 0)
            {
                neuComboBox1.SelectedIndex = 0;
                deptcode = ((FS.HISFC.Models.Base.Department)neuComboBox1.Items[this.neuComboBox1.SelectedIndex]).ID;
                deptname = ((FS.HISFC.Models.Base.Department)neuComboBox1.Items[this.neuComboBox1.SelectedIndex]).Name;
            }

            FS.HISFC.Models.Base.Const cons = new FS.HISFC.Models.Base.Const();
            cons.ID = "ALL";
            cons.Name = "全部";
            this.neuComboBox2.Items.Add(cons);
            constantList = manager.GetConstantList("ITEMMINFEECODE");

            foreach (FS.HISFC.Models.Base.Const con in constantList)
            {
                neuComboBox2.Items.Add(con);
            }
            if (neuComboBox2.Items.Count >= 0)
            {
                neuComboBox2.SelectedIndex = 0;
                reportCode = ((FS.HISFC.Models.Base.Const)neuComboBox2.Items[0]).ID;
                reportName = ((FS.HISFC.Models.Base.Const)neuComboBox2.Items[0]).Name;
            }
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
                return -1;
            return base.OnRetrieve(base.beginTime, base.endTime, deptcode, reportCode);
        }

        private void neuComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (neuComboBox1.SelectedIndex > -1)
            {
                deptcode = ((FS.HISFC.Models.Base.Department)neuComboBox1.Items[this.neuComboBox1.SelectedIndex]).ID;
                deptname = ((FS.HISFC.Models.Base.Department)neuComboBox1.Items[this.neuComboBox1.SelectedIndex]).Name;
            }
        }

        private void neuComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (neuComboBox2.SelectedIndex > -1)
            {
                reportCode = ((FS.HISFC.Models.Base.Const)neuComboBox2.Items[this.neuComboBox2.SelectedIndex]).ID;
                reportName = ((FS.HISFC.Models.Base.Const)neuComboBox2.Items[this.neuComboBox2.SelectedIndex]).Name;
            }
        }
    }
}
