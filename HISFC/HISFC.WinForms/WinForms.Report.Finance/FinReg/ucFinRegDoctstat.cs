using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.Finance.FinReg
{
    public partial class ucFinRegDoctstat : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinRegDoctstat()
        {
            InitializeComponent();
        }
        //科室

        string deptCode = string.Empty;
        string deptName = string.Empty;
        protected override int OnRetrieve(params object[] objects)
        {
            return base.OnRetrieve(this.dtpBeginTime.Value,this.dtpEndTime.Value,this.deptCode);
        }

        protected override void OnLoad()
        {
            this.isAcross = true;
            this.isSort = false;
            this.Init();
            base.OnLoad();
            //填充数据
            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            System.Collections.ArrayList constantList = manager.GetDeptmentAllValid();

            FS.HISFC.Models.Base.Department top = new FS.HISFC.Models.Base.Department();
            top.ID = "0";
            top.Name = "全  部";
            top.SpellCode = "QB";
            top.WBCode = "WU";
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
                deptCode = ((FS.HISFC.Models.Base.Department)neuComboBox1.Items[0]).ID;
                deptName = ((FS.HISFC.Models.Base.Department)neuComboBox1.Items[0]).Name;
            }
        }

        private void neuComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (neuComboBox1.SelectedIndex > -1)
            {
                deptCode = ((FS.HISFC.Models.Base.Department)neuComboBox1.Items[this.neuComboBox1.SelectedIndex]).ID;
                deptName = ((FS.HISFC.Models.Base.Department)neuComboBox1.Items[this.neuComboBox1.SelectedIndex]).Name;
            }
        }
       
    }
}
