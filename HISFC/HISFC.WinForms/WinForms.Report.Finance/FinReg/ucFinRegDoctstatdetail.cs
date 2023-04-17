using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.Finance.FinReg
{
    public partial class ucFinRegDoctstatdetail : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinRegDoctstatdetail()
        {
            InitializeComponent();
        }
        //ҽ��

        string doctCode = string.Empty;
        string doctName = string.Empty;
        protected override int OnRetrieve(params object[] objects)
        {
            return base.OnRetrieve(this.dtpBeginTime.Value,this.dtpEndTime.Value,this.doctCode);
        }

        protected override void OnLoad()
        {
            this.isAcross = true;
            this.isSort = false;
            this.Init();
            base.OnLoad();
            //�������
            FS.HISFC.BizProcess.Integrate.Manager doctMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            System.Collections.ArrayList al = doctMgr.QueryRegDepartment();
            al = doctMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            //this.cmbDoct.AddItems(al);    
            FS.HISFC.Models.Base.Employee top = new FS.HISFC.Models.Base.Employee();
            top.ID = "0";
            top.Name = "ȫ  ��";
            top.SpellCode = "QB";
            top.WBCode = "WU";
            this.neuComboBox1.Items.Add(top);
            foreach (FS.HISFC.Models.Base.Employee con in al)
            {
                neuComboBox1.Items.Add(con);
            }
            this.neuComboBox1.alItems.Add(top);
            this.neuComboBox1.alItems.AddRange(al);

            if (neuComboBox1.Items.Count > 0)
            {
                neuComboBox1.SelectedIndex = 0;
                doctCode = ((FS.HISFC.Models.Base.Employee)neuComboBox1.Items[0]).ID;
                doctName = ((FS.HISFC.Models.Base.Employee)neuComboBox1.Items[0]).Name;
            }
        }

        private void neuComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (neuComboBox1.SelectedIndex > -1)
            {
                doctCode = ((FS.HISFC.Models.Base.Employee)neuComboBox1.Items[this.neuComboBox1.SelectedIndex]).ID;
                doctName = ((FS.HISFC.Models.Base.Employee)neuComboBox1.Items[this.neuComboBox1.SelectedIndex]).Name;
            }
        }
       
    }
}
