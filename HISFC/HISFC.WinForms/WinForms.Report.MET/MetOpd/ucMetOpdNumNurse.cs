using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.Report.MET.MetOpd
{
    public partial class ucMetOpdNumNurse : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucMetOpdNumNurse()
        {
            InitializeComponent();
         }
        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            string nurseType = "";
            switch( cmbNurseType.SelectedIndex)
            {
                case 0: nurseType = "WashingHandNurse";  break;
                case 1: nurseType = "ItinerantNurse";    break;
                default: MessageBox.Show(Language.Msg("��ѡ��ʿ����")); return -1; break;
            }
            return base.OnRetrieve(beginTime, endTime,nurseType);
        }
    }
}
