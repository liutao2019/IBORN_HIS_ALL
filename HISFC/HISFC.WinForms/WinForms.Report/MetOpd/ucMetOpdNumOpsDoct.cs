using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.WinForms.Report.MetOpd
{
    public partial class ucMetOpdNumOpsDoct : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucMetOpdNumOpsDoct()
        {
            InitializeComponent();
        }
        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            string DoctType = "";
            switch (cmdDoctType.SelectedIndex)
            {
                case 0: DoctType = "Helper1"; break;
                case 1: DoctType = "Helper2"; break;
                default: MessageBox.Show(Language.Msg("��ѡ������ҽ������")); return -1; break;
            }

            return base.OnRetrieve(beginTime, endTime, DoctType);
        }
    }
}
