using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Report.Finance.FinIpb
{
    public partial class ucFinIpbUndrugUse :FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        string deptCode = string.Empty;
        string deptName = string.Empty;
        string feeCode = string.Empty;
        string feeName = string.Empty;
        string feeType = string.Empty;
        public ucFinIpbUndrugUse()
        {
            InitializeComponent();
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            System.Collections.ArrayList constantList = manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.I);

            FS.HISFC.Models.Base.Department top = new FS.HISFC.Models.Base.Department();
            top.ID = "ALL";
            top.Name = "全  部";
            this.neuComboBox3.Items.Add(top);
            foreach (FS.HISFC.Models.Base.Department con in constantList)
            {
                neuComboBox3.Items.Add(con);
            }
            this.neuComboBox3.alItems.Add(top);
            this.neuComboBox3.alItems.AddRange(constantList);
            if (neuComboBox3.Items.Count > 0)
            {
                neuComboBox3.SelectedIndex = 0;
                deptCode = ((FS.HISFC.Models.Base.Department)neuComboBox3.Items[this.neuComboBox3.SelectedIndex]).ID;
                deptName = ((FS.HISFC.Models.Base.Department)neuComboBox3.Items[this.neuComboBox3.SelectedIndex]).Name;
            }

            FS.HISFC.Models.Base.Const cons = new FS.HISFC.Models.Base.Const();
            cons.ID = "ALL";
            cons.Name = "全  部";
            this.neuComboBox2.Items.Add(cons);
            constantList = manager.GetConstantList("ITEMMINFEECODE");
            foreach (FS.HISFC.Models.Base.Const con in constantList)
            {
                neuComboBox2.Items.Add(con);
            }
            if (neuComboBox2.Items.Count >= 0)
            {
                neuComboBox2.SelectedIndex = 0;
                feeCode = ((FS.HISFC.Models.Base.Const)neuComboBox2.Items[0]).ID;
                feeName = ((FS.HISFC.Models.Base.Const)neuComboBox2.Items[0]).Name;
            }

            System.Collections.ArrayList al = new System.Collections.ArrayList();
            FS.HISFC.Models.Base.Const conn = new FS.HISFC.Models.Base.Const();
            conn.ID = "1";
            conn.Name = "门  诊"; 
            this.neuComboBox1.Items.Add(conn);
            conn.ID = "2";
            conn.Name = "住  院";
            this.neuComboBox1.Items.Add(conn);
           conn.ID = "ALL";
            conn.Name = "全　部";
            this.neuComboBox1.Items.Add(conn);
            if (neuComboBox1.Items.Count >= 0)
            {
                neuComboBox1.SelectedIndex = 0;
                feeType = ((FS.HISFC.Models.Base.Const)neuComboBox1.Items[0]).ID;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
                return -1;
            return base.OnRetrieve(this.neuTextBox1.Text,deptCode, base.beginTime, base.endTime, this.feeCode, this.feeType);
        }



        private void neuComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (neuComboBox1.SelectedIndex > -1)
            {
                feeType = ((FS.HISFC.Models.Base.Const)neuComboBox1.Items[this.neuComboBox1.SelectedIndex]).ID;
            }
        }

        private void neuComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (neuComboBox2.SelectedIndex > -1)
            {
                feeCode = ((FS.HISFC.Models.Base.Const)neuComboBox2.Items[this.neuComboBox2.SelectedIndex]).ID;
                feeName = ((FS.HISFC.Models.Base.Const)neuComboBox2.Items[this.neuComboBox2.SelectedIndex]).Name;
            }
        }

        private void neuComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (neuComboBox3.SelectedIndex > -1)
            {
                deptCode = ((FS.HISFC.Models.Base.Department)neuComboBox3.Items[this.neuComboBox3.SelectedIndex]).ID;
                deptName = ((FS.HISFC.Models.Base.Department)neuComboBox3.Items[this.neuComboBox3.SelectedIndex]).Name;
            }
        }
    }
}
