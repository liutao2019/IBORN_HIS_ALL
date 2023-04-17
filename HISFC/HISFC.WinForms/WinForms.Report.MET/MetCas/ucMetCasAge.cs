using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.MET.MetCas
{
    public partial class ucMetCasAge : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucMetCasAge()
        {
            InitializeComponent();
            cmbSex.SelectedIndex = 0;
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }

            string strSex = string.Empty;

            if (cmbSex.SelectedIndex == 0)
            { 
                strSex = "ALL";
                this.dwMain.Modify("t_sex.text ='ȫ��'");
            }

            if (cmbSex.SelectedIndex == 1)
            { 
                strSex = "M";
                this.dwMain.Modify("t_sex.text ='��'");
            }

            if (cmbSex.SelectedIndex == 2)
            { 
                strSex = "F";
                this.dwMain.Modify("t_sex.text ='Ů'");
            }


            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value,strSex);
        }

    }
}
