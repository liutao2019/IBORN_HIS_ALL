using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.Report.Finance.FinReg
{
    public partial class ucFinRegDeptInfo : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinRegDeptInfo()
        {
            InitializeComponent();
        }
        protected override void OnLoad()
        {
            this.Init();
            base.OnLoad();

            DateTime now = DateTime.Now;

            this.dtpBeginTime.Value = new DateTime(now.Year, now.Month, now.Day, 00, 00, 00);
            this.dtpEndTime.Value = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
        }
        protected override int OnRetrieve(params object[] objects)
        {
            if (this.dtpBeginTime.Value > this.dtpEndTime.Value)
            {
                MessageBox.Show("����ʱ�䲻��С�ڿ�ʼʱ��");
            }
            
            dwMain.Retrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value);
            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value);

         
        }


    }
}
