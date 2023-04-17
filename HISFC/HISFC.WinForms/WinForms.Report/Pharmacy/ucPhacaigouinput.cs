using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.Pharmacy
{
    public partial class ucPhacaigouinput : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucPhacaigouinput()
        {
            InitializeComponent();
            this.neuComboBox1.AddItems(manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.PI));
        }

        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

        protected override int OnRetrieve(params object[] objects)
        {
            string str = this.neuComboBox1.Text;
            return base.OnRetrieve(this.dtpBeginTime.Value,this.dtpEndTime.Value,this.neuComboBox1.Tag,str);
        }
        string filterString = "1=1";

        private void neuComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.neuComboBox1.Tag = this.neuComboBox1.SelectedItem.ID.ToString ( );
        }
    }
}
