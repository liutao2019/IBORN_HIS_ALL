using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.MetCas
{
    public partial class ucMetHos : Common.ucQueryBaseForDataWindow
    {
        private FS.HISFC.Models.Base.Employee oper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

        private string bblx = string.Empty;

        public ucMetHos()
        {
            InitializeComponent();
        }
        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            bblx = "ҽԺ�������£�����";
            TimeSpan times=base.endTime - base.beginTime;
            int days=times.Days;
            if (days > 365)
            {
                bblx = "ҽԺ�������꣩����";
            }
            else if (days > 182)
            {
                bblx = "ҽԺ���������꣩����";
            }
            else if (days > 90)
            {
                bblx = "ҽԺ��������������";
            }
            int RetrieveRow = base.OnRetrieve(base.beginTime, base.endTime);
            dwMain.Modify("t_bblx.text = '" + bblx + "'");
            dwMain.Modify("t_zbr.text = '" + oper.Name + "'");
            return RetrieveRow;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            if (bblx.Equals("ҽԺ�������£�����"))
            {
                return base.OnPrint(sender, neuObject);
            }
            else
            {
                MessageBox.Show("ֻ�ܴ�ӡ�±���");
                return -1;
            }
            
        }

    }
}
