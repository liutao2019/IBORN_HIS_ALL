using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.Finance.FinIpb
{
    public partial class ucFinIpbDeptFeeTot :FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinIpbDeptFeeTot()
        {
            InitializeComponent();
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            string operName = string.Empty;
            //FS.FrameWork.Public.ObjectHelper oh = new FS.FrameWork.Public.ObjectHelper();
            //FS.HISFC.BizProcess.Integrate.Manager mg = new FS.HISFC.BizProcess.Integrate.Manager();
            //oh.ArrayObject =  mg.QueryEmployeeAll();
            operName = FS.FrameWork.Management.Connection.Operator.Name;
            return base.OnRetrieve(operName);
        }

    }
}
